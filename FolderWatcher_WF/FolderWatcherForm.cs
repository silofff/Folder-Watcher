using System;
using System.IO;
using System.Windows.Forms;

namespace FolderWatcher_WF
{
    public partial class FolderWatcherForm : Form
    {
        private string _rootDirectory;
        private readonly NodeUpdater _nodeUpdater = new NodeUpdater();
        private readonly ListViewItemBuilder _listViewBuilder = new ListViewItemBuilder();
        public FolderWatcherForm()
        {
            InitializeComponent();
        }
        private void PopulateTreeView()
        {
            var directoryInfo = new DirectoryInfo(_rootDirectory);
            if (directoryInfo.Exists)
            {
                var rootNode = new TreeNode(directoryInfo.Name)
                {
                    Tag = directoryInfo
                };
                if (directoryInfo.GetDirectories().Length > 0)
                {
                    rootNode.Nodes.Add(new TreeNode { Name = "Loading..." });
                }
                treeView1.Nodes.Add(rootNode);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            UpdateInfoWindow(e.Node);
        }

        private void UpdateInfoWindow(TreeNode node)
        {
            listView1.Items.Clear();
            try
            {
                _nodeUpdater.AddDirectoriesToNode(node); //loading directories selected node and addition them to it
                var listViewItems = _listViewBuilder.GetListViewItem((DirectoryInfo)node.Tag);//loading info selected directory
                listView1.Items.AddRange(listViewItems);// and addition it to ListView
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            listView1.Items.Clear();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _rootDirectory = folderBrowserDialog1.SelectedPath;

                if (Controls.Contains(Open_button))
                {
                    Controls.Remove(Open_button);
                    Open_button.Anchor = (AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right;
                    Open_button.Location = new System.Drawing.Point(-1, 0);
                    Open_button.Size = new System.Drawing.Size(177, 27);
                    splitContainer1.Panel1.Controls.Add(Open_button);
                }
                AddInfoToWindow();
                PopulateTreeView();
            }
        }
        private void AddInfoToWindow()
        {
            Controls.Add(splitContainer1);
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.RemoveByKey("Loading...");//Remove the temporary node
        }
    }
}
