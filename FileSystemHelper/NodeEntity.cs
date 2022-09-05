namespace FileSystemHelper
{
    public class NodeEntity
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public string[] Info { get; set; }
        public NodeType Type { get; set; }

        public NodeEntity() { }

        public NodeEntity(string path)
        {
            string name = Path.GetFileName(path);
            Type = NodeType.Directory;

            if (File.Exists(path))
            {
                Type = NodeType.File;
            }

            if (String.IsNullOrEmpty(name))
            {
                name = path;
                Type = NodeType.Drive;
            }

            Name = name;
            FullPath = path;
            SetInfo();
        }

        public void SetInfo()
        {
            switch (this.Type)
            {
                case NodeType.Directory: GetDirectoryInfo(); break;
                case NodeType.File:      GetFileInfo();      break;
                case NodeType.Drive:                         break;
            }
                
        }

        public void GetDriveInfo(DriveInfo drInfo)
        {
            var one   = "@AvailableFreeSpace" + GetRoundedFileLength(drInfo.AvailableFreeSpace).ToString();
            var two   = "@DriveFormat" + drInfo.DriveFormat;
            var three = "@DriveType" + drInfo.DriveType.ToString();
            var four  = "@TotalFreeSpace" + GetRoundedFileLength(drInfo.TotalFreeSpace).ToString();
            var five  = "@TotalSize" + GetRoundedFileLength(drInfo.TotalSize).ToString();
            var six   = "@VolumeLabel" + drInfo.VolumeLabel;

            Info = new string[] {one, two, three, four, five, six };
        }

        public void GetDirectoryInfo()
        {
            try
            {
                var one = $"@ElementsContained {Directory.GetFileSystemEntries(FullPath).Length}";
                var two = $"@LastWriteTime {Directory.GetLastWriteTime(FullPath)}";
                var three = $"@LastAccessTime {Directory.GetLastAccessTime(FullPath)}";
                var four = $"@CreationTime {Directory.GetCreationTime(FullPath)}";
                Info = new string[] { one, two, three, four };
            }
            catch (Exception e)
            {
                Info = new string[] { e.Message };
            }   
        }

        public void GetFileInfo()
        {
            var fileInfo = new FileInfo(FullPath);

            var one = $"@Length {GetRoundedFileLength(fileInfo.Length)} ";
            var two = $"@CreationTime {fileInfo.CreationTime.ToString()}";
            var three = $"@LastAccessTime {fileInfo.LastAccessTime.ToString()}";

            Info = new string[] { one, two, three };
        }

        public string GetRoundedFileLength(long longLength)
        {
            decimal length = (decimal)longLength;
            decimal dozen = 1024;
            decimal factor = dozen;
            decimal[] factorArray = new decimal[3] { Exponentiate(dozen, 2), Exponentiate(dozen, 3), Exponentiate(dozen, 4)};

            if (length < dozen)
            {
                return $"{Math.Round(length), 2} byte";
            }

            if (length < factorArray[0])
            {
                var lol = length / dozen;
                return $"{Math.Round((length / dozen),2, MidpointRounding.ToEven)} KB";
            }


            if (length < factorArray[1])
            {
                var lol = length / factorArray[0];
                return $"{Math.Round(length / factorArray[0],2, MidpointRounding.ToEven)} MB";
            }


            if (length < factorArray[2])
            {
                return $"{Math.Round(length / factorArray[1],2, MidpointRounding.ToEven)} GB";
            }

            if (length < factorArray[3])
            {
                return $"{Math.Round(length / factorArray[2], 2, MidpointRounding.ToEven)} TB";
            }

            return "Error";
        }

        public decimal Exponentiate(decimal number, int exp)
        {
            decimal result = 1;

            for (int i = 0; i < exp; i++)
            {
                result = result * number;
            }

            return result;
        }
    }

    public enum NodeType : byte
    {
        Directory = 0,
        File = 1,
        Drive = 2
    }
}
