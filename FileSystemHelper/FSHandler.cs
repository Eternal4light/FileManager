namespace FileSystemHelper
{
    public class FSHandler
    {
        public DriveInfo[] GetDrives()
        {
            return DriveInfo.GetDrives();
        }

        public string GetRootFolder(DriveInfo driveInfo)
        {
            if (driveInfo.IsReady)
            {
                if (Directory.Exists(driveInfo.Name))
                    return driveInfo.Name;
            }

            return String.Empty;
        }

        public string GetRootPath(DriveInfo driveInfo)
        {
            var path = GetRootFolder(driveInfo);

            if (!String.IsNullOrEmpty(path))
                return path;
            else
                return String.Empty;
        }
      
        public List<string> GetRootPaths(DriveInfo[] driveInfos)
        {
            var resultPaths = new List<string>();

            foreach (var drInfo in driveInfos)
            {
                resultPaths.Add(GetRootPath(drInfo));
            }

            return resultPaths;
        }

        public List<string> GetRootPaths()
        {
            return GetRootPaths(GetDrives());
        }

        public string[] GetNodeChildren(string path)
        {
            try
            {
                return Directory.GetFileSystemEntries(path);
            }
            catch (Exception e)
            {
                return new string[0];
            }
        }

        public List<NodeEntity> GetRootNodeEntities()
        {
            var result = new List<NodeEntity>();
            var drives = GetDrives();

            foreach (var drive in drives)
            {
                if (!drive.IsReady)
                    continue;

                var nodeEntity = new NodeEntity(drive.Name);
                nodeEntity.GetDriveInfo(drive);

                result.Add(nodeEntity);
            }

            return result;
        }
        public List<NodeEntity> GetNodeEntitiesChildren(string path)
        {
            var result = new List<NodeEntity>();
            var nodes = GetNodeChildren(path);

            foreach (var node in nodes)
            {
                result.Add(new NodeEntity(node));
            }

            return result;
        }
    }
}
