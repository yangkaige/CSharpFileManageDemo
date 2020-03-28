using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace FileManageApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string folder;
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowDialog();
            folder = fbd.SelectedPath;
            if (string.IsNullOrEmpty(folder))
            {
                return;
            }
            treeView.Nodes.Clear();
            txtContent.Text = "";
            LoadData(folder, treeView.Nodes);
        }
        /// <summary>
        /// 获取目录信息
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="nodes"></param>
        private void LoadData(string folder, TreeNodeCollection nodes)
        {
            string[] dics = Directory.GetDirectories(folder);
            string[] filePaths = Directory.GetFiles(folder);
            //加载目录
            foreach (var item in dics)
            {
                TreeNode treeNode = nodes.Add(Path.GetFileName(item));
                LoadData(item, treeNode.Nodes);
            }
            //加载文件
            foreach (var item in filePaths)
            {
                TreeNode treeNode = nodes.Add(Path.GetFileName(item));
                treeNode.Tag = item;
            }
        }
        /// <summary>
        /// 双击树控件节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode treeNode = treeView.SelectedNode;
            string path = treeNode.Tag.ToString();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            txtContent.Text = File.ReadAllText(path,Encoding.Default);
        }
    }
}
