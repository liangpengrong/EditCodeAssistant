using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PubMethodLibrary;
using System.Windows.Forms;

namespace ProSetUpForm
{
    public partial class SetUpMain : Form
    {
        // 左边的选项树
        private TreeView treeView = null;
        // 右边的控件容器
        private Panel controPanel = null;
        // 总容器
        private SplitContainer totalPanel = null;
        public SetUpMain()
        {
            InitializeComponent();
        }
        private void SetUpMain_Load(object sender, EventArgs e)
        {
            // 初始化控件
           loadInitControl();
        }

        /// <summary>
        /// 窗体初始化时加载控件
        /// </summary>
        private void loadInitControl() {
            // 初始化左边的选项树
            treeView = initTreeView();
            // 初始化右边的控件容器
            controPanel = initControlPanel();
            // 初始化总容器
            totalPanel = initSplitContainer();

            // 初始化控件布局
            initControlLayout();
        }

        /// <summary>
        /// 初始化控件布局
        /// </summary>
        private void initControlLayout() {
            totalPanel.Panel1.Controls.Add(treeView);
            //totalPanel.Panel2.Controls.Add(controPanel);
            this.Controls.Add(totalPanel);
        }
        /// <summary>
        /// 初始化左边的选项树
        /// </summary>
        /// <returns></returns>
        private TreeView initTreeView() { 
            TreeView tree = new global::ProSetUpForm.ControlLibrary.ControlTree().treeView;
            // 字体
            Font font = new Font(FontFamily.GenericSerif, 10, FontStyle.Regular);
            tree.Font = font;
            // 大小
            tree.Size =new Size(150, this.ClientSize.Height);
            // 边框
            tree.BorderStyle = BorderStyle.None;
            // 相对位置
            tree.Location = new Point(0,0);
            // 锚定
            tree.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            return tree;
        }
        /// <summary>
        /// 初始化右边的控件容器
        /// </summary>
        /// <returns></returns>
        private Panel initControlPanel() { 
            Panel panel = new global::ProSetUpForm.ControlLibrary.ControlTree().ControPanel;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Location = new Point(treeView.Location.X+treeView.Width-1,0);
            panel.Size = new Size(600, treeView.Height);
            return panel;
        }

        private SplitContainer initSplitContainer() { 
            SplitContainer splitContainer = new SplitContainer();
            // 相对位置
            splitContainer.Location = new Point(1,1);
            // 锚定
            splitContainer.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            // 大小
            splitContainer.Size = this.ClientSize;
            // 边框
            splitContainer.BorderStyle = BorderStyle.FixedSingle;

            splitContainer.Dock = DockStyle.None;
            splitContainer.SplitterWidth = 2;
            splitContainer.Panel1MinSize = 100;
            return splitContainer;
        }
    }
}
