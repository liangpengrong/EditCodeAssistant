namespace ProgramOption
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
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点23");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点24");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("常规", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点25");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("节点26");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("标签", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("节点27");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("节点28");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("快捷键", new System.Windows.Forms.TreeNode[] {
            treeNode16,
            treeNode17});
            this.treeView = new System.Windows.Forms.TreeView();
            this.ControPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            treeNode10.Name = "节点23";
            treeNode10.Text = "节点23";
            treeNode11.Name = "节点24";
            treeNode11.Text = "节点24";
            treeNode12.Name = "常规Tree";
            treeNode12.Text = "常规";
            treeNode12.ToolTipText = "常规";
            treeNode13.Name = "节点25";
            treeNode13.Text = "节点25";
            treeNode14.Name = "节点26";
            treeNode14.Text = "节点26";
            treeNode15.Name = "标签Tree";
            treeNode15.Text = "标签";
            treeNode15.ToolTipText = "标签";
            treeNode16.Name = "节点27";
            treeNode16.Text = "节点27";
            treeNode17.Name = "节点28";
            treeNode17.Text = "节点28";
            treeNode18.Name = "快捷键Tree";
            treeNode18.Text = "快捷键";
            treeNode18.ToolTipText = "快捷键";
            this.treeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode15,
            treeNode18});
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
