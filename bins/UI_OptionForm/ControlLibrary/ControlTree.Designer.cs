namespace UI_OptionForm.ControlLibrary
{
    partial class ControlTree
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点23");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点24");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("常规", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点25");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点26");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("标签", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点27");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点28");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("快捷键", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            this.treeView = new System.Windows.Forms.TreeView();
            this.ControPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView.ItemHeight = 17;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode1.Name = "节点23";
            treeNode1.Text = "节点23";
            treeNode2.Name = "节点24";
            treeNode2.Text = "节点24";
            treeNode3.Name = "常规Tree";
            treeNode3.Text = "常规";
            treeNode3.ToolTipText = "常规";
            treeNode4.Name = "节点25";
            treeNode4.Text = "节点25";
            treeNode5.Name = "节点26";
            treeNode5.Text = "节点26";
            treeNode6.Name = "标签Tree";
            treeNode6.Text = "标签";
            treeNode6.ToolTipText = "标签";
            treeNode7.Name = "节点27";
            treeNode7.Text = "节点27";
            treeNode8.Name = "节点28";
            treeNode8.Text = "节点28";
            treeNode9.Name = "快捷键Tree";
            treeNode9.Text = "快捷键";
            treeNode9.ToolTipText = "快捷键";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode9});
            this.treeView.ShowLines = false;
            this.treeView.ShowNodeToolTips = true;
            this.treeView.Size = new System.Drawing.Size(263, 424);
            this.treeView.TabIndex = 0;
            // 
            // ControPanel
            // 
            this.ControPanel.Location = new System.Drawing.Point(279, 3);
            this.ControPanel.Name = "ControPanel";
            this.ControPanel.Size = new System.Drawing.Size(465, 421);
            this.ControPanel.TabIndex = 1;
            this.ControPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ControPanel_Paint);
            // 
            // ControlTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ControPanel);
            this.Controls.Add(this.treeView);
            this.Name = "ControlTree";
            this.Size = new System.Drawing.Size(1247, 781);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel ControPanel;
        public System.Windows.Forms.TreeView treeView;
    }
}
