using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FolderWatcher_WF
{
    internal class ListViewItemBuilder
    {
        internal ListViewItem[] GetListViewItem(DirectoryInfo directoryInfo)
        {
            var results = new List<ListViewItem>();
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item;
            foreach (var subDir in directoryInfo.GetDirectories())
            {
                item = new ListViewItem(subDir.Name, 0);
                subItems = new[]
                   {
                       new ListViewItem.ListViewSubItem(item, "Directory"),
                       new ListViewItem.ListViewSubItem(item, subDir.LastAccessTime.ToShortDateString())
                   };
                item.SubItems.AddRange(subItems);
                results.Add(item);
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                subItems = new[]
                   {
                       new ListViewItem.ListViewSubItem(item, "File"),
                       new ListViewItem.ListViewSubItem(item, file.LastAccessTime.ToShortDateString())
                   };
                item.SubItems.AddRange(subItems);
                results.Add(item);
            }
            return results.ToArray();
        }
    }
}
