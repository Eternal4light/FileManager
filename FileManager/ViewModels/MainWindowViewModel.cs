using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using FileSystemHelper;
using FileManager.ViewModels.Commands;
using FileManager.Resources;
using System.IO;

namespace FileManager.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<NodeEntity> nodeChildrenListBoxContent;  // main Listbox content
        private NodeEntity selectedNodeChild;                                 // its selected item
        private ObservableCollection<NodeEntity> nodeHierarchy;
        private ObservableCollection<NodeEntity> languages;
        private NodeEntity selectedLanguage;

        private string keyWord; // for searching

        // Commands:
        private JumpToNodeChildren jumpToNodeChildren;
        private Search search;
        private ToDrives toDrives;
        private ToNode0 toNode0;
        private ToNode1 toNode1;
        private ToNode2 toNode2;
        private ToNode3 toNode3;

        private bool inChildrenNodes;
        private int foldersChecked;

        public DataBaseHelper.AppContext db;

        // Localisation:
        private string localToDrives;
        private string localInChildNodes;
        private string localSearch;
        private string localFoldersChecked;

        public string LocalFolderPath { get; set; }

        public MainWindowViewModel()
        {
            Start();
        }

        private void Start()
        {
            EnsureLocalFolderCreated();
            SetLanguages();
            LocalizeView();
            db = new DataBaseHelper.AppContext();
            db.Database.EnsureCreated();
            GetToDrives();
            NodeHierarchy = new ObservableCollection<NodeEntity>();
        }

        private void LocalizeView()
        {
            LocalToDrives = Localization.ToDrives;
            LocalInChildNodes = Localization.InChildNodes;
            LocalSearch = Localization.Search;
            LocalFoldersChecked = Localization.FoldersChecked;
        }
        private async void EnsureLocalFolderCreated()
        {
            var path =  Directory.GetCurrentDirectory();
            path = Path.Combine(path, "languages");
            LocalFolderPath = path;

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, "russian.lang");

            if (!File.Exists(filePath))
            {
                string rus = "@AvailableFreeSpace   #Свободного места доступно:\r\n" +
                    "@DriveFormat                 #Файловая система:\r\n" +
                    "@DriveType                     #Тип диска:\r\n" +
                    "@TotalFreeSpace         #Объем свободного места:\r\n" +
                    "@TotalSize                      #Общий размер:\r\n" +
                    "@VolumeLabel               #Метка тома:\r\n\r\n" +
                    "@ElementsContained    #Содержит элементов:\r\n" +
                    "@LastWriteTime             #Дата изменения:\r\n" +
                    "@LastAccessTime         #Дата доступа:\r\n" +
                    "@CreationTime               #Дата создания:\r\n" +
                    "@Length                           #Размер:\r\n\r\n" +
                    "@ToDrives                        #К дискам\r\n" +
                    "@InChildNodes                #В дочерних узлах\r\n" +
                    "@Search                            #Поиск\r\n" +
                    "@FoldersChecked           #Папок проверено: ";

                await File.WriteAllTextAsync(filePath, rus);
            }
        }

        private void SetLanguages()
        {
            Languages = new ObservableCollection<NodeEntity>();
            Languages.Add(new NodeEntity() { Name = "English" });
            SelectedLanguage = Languages[0];

            var fsHandler = new FSHandler();
            var langFolderContent = fsHandler.GetNodeEntitiesChildren(LocalFolderPath);

            foreach (var el in langFolderContent)
            {
                if (el.Type == NodeType.File && el.Name.ToUpper().Contains("LANG"))
                {
                    Languages.Add(el);
                }
            }
        }

        public void GetToDrives()
        {
            var fsHandler = new FSHandler();
            var rootEntities = fsHandler.GetRootNodeEntities();
            Localization.LocalizeDriveInfos(rootEntities);
            NodeChildrenListBoxContent = new ObservableCollection<NodeEntity>(rootEntities);
        }

        public ObservableCollection<NodeEntity> NodeChildrenListBoxContent
        {
            get { return nodeChildrenListBoxContent; }
            set
            {
                nodeChildrenListBoxContent = value;
                OnPropertyChanged("NodeChildrenListBoxContent");
            }
        }

        public ObservableCollection<NodeEntity> NodeHierarchy
        {
            get { return nodeHierarchy; }
            set
            {
                nodeHierarchy = value;
                OnPropertyChanged("NodeHierarchy");
            }
        }

        public ObservableCollection<NodeEntity> Languages
        {
            get { return languages; }
            set
            {
                languages = value;
                OnPropertyChanged("Languages");
            }
        }

        public NodeEntity SelectedNodeChild
        {
            get { return selectedNodeChild; }
            set
            {
                selectedNodeChild = value;
                OnPropertyChanged("SelectedNodeChild");
            }
        }

        public NodeEntity SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
                Localization.ChangeLanguage(selectedLanguage.FullPath);
                LocalizeView();
            }
        }

        public string KeyWord
        {
            get { return keyWord; }
            set
            {
                keyWord = value;
                OnPropertyChanged("KeyWord");
            }
        }

        public int FoldersChecked
        {
            get { return foldersChecked; }
            set
            {
                foldersChecked = value;
                OnPropertyChanged("FoldersChecked");
            }
        }

        public bool InChildrenNodes
        {
            get { return inChildrenNodes; }
            set
            {
                inChildrenNodes = value;
                OnPropertyChanged("InChildrenNodes");
            }
        }

        public string LocalToDrives
        {
            get { return localToDrives; }
            set
            {
                localToDrives = value;
                OnPropertyChanged("LocalToDrives");
            }
        }

        public string LocalInChildNodes
        {
            get { return localInChildNodes; }
            set
            {
                localInChildNodes = value;
                OnPropertyChanged("LocalInChildNodes");
            }
        }

        public string LocalSearch
        {
            get { return localSearch; }
            set
            {
                localSearch = value;
                OnPropertyChanged("LocalSearch");
            }
        }

        public string LocalFoldersChecked
        {
            get { return localFoldersChecked; }
            set
            {
                localFoldersChecked = value;
                OnPropertyChanged("LocalFoldersChecked");
            }
        }

        public Search Search
        {
            get { return search ?? (search = new Search(this)); }
        }
        
        public JumpToNodeChildren JumpToNodeChildren
        {
            get { return jumpToNodeChildren ?? (jumpToNodeChildren = new JumpToNodeChildren(this)); }
        }

        public ToDrives ToDrives
        {
            get { return toDrives ?? (toDrives = new ToDrives(this)); }
        }

        public ToNode0 ToNode0
        {
            get { return toNode0 ?? (toNode0 = new ToNode0(this)); }
        }

        public ToNode1 ToNode1
        {
            get { return toNode1 ?? (toNode1 = new ToNode1(this)); }
        }

        public ToNode2 ToNode2
        {
            get { return toNode2 ?? (toNode2 = new ToNode2(this)); }
        }

        public ToNode3 ToNode3
        {
            get { return toNode3 ?? (toNode3 = new ToNode3(this)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
