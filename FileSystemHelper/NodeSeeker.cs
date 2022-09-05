using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemHelper
{
    public static class NodeSeeker
    {
        public static FSHandler FsHandler = new FSHandler();

        public static int CheckedFolders = 0;
        public static List<NodeEntity> FindByName(NodeEntity directory, string keyWord, Func<int, int> func)
        {
            var result = new List<NodeEntity>();
            var children = FsHandler.GetNodeEntitiesChildren(directory.FullPath);

            foreach (var child in children)
            {
                if (child.Name.ToUpper().Contains(keyWord.ToUpper()))
                    result.Add(child);

                if (child.Type == NodeType.Directory)
                    result.AddRange(FindByName(child, keyWord, func));
            }

            CheckedFolders++;

            func.Invoke(CheckedFolders);
            return result;
        }
    }
}
