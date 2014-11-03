using System;
using System.IO;
using System.Windows.Forms;

namespace FolderWatcher_WF
{
    internal class NodeUpdater
    {
        internal void AddDirectoriesToNode(TreeNode nodeToAdd)
        {
            var dirsInfo = (DirectoryInfo)nodeToAdd.Tag;

            if (nodeToAdd.GetNodeCount(false) == 0)
            {
                foreach (var subDir in dirsInfo.GetDirectories())
                {
                    var node = new TreeNode(subDir.Name, 0, 0)
                    {
                        Name = subDir.FullName,
                        Tag = subDir,
                        ImageKey = @"folder"
                    };
                    try
                    {
                        if (subDir.GetDirectories().Length > 0) node.Nodes.Add(new TreeNode { Name = "Loading..." });
                        nodeToAdd.Nodes.Add(node);
                    }
                    catch (Exception)
                    {
                        nodeToAdd.Nodes.Add(node);
                    }
                }
            }
        }
    }
}
