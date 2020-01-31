namespace UI.ComponentLibrary.ControlLibrary.RightMenu {
    partial class TextRightMenu {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.rightMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选Item = new System.Windows.Forms.ToolStripMenuItem();
            this.选中整行Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.剪切Item = new System.Windows.Forms.ToolStripMenuItem();
            this.复制Item = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴Item = new System.Windows.Forms.ToolStripMenuItem();
            this.删除Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.去除Item = new System.Windows.Forms.ToolStripMenuItem();
            this.空格Item = new System.Windows.Forms.ToolStripMenuItem();
            this.全部空格Item = new System.Windows.Forms.ToolStripMenuItem();
            this.行首空格Item = new System.Windows.Forms.ToolStripMenuItem();
            this.行尾空格Item = new System.Windows.Forms.ToolStripMenuItem();
            this.空行Item = new System.Windows.Forms.ToolStripMenuItem();
            this.换行符Item = new System.Windows.Forms.ToolStripMenuItem();
            this.制表符Item = new System.Windows.Forms.ToolStripMenuItem();
            this.转化为Item = new System.Windows.Forms.ToolStripMenuItem();
            this.大写形式Item = new System.Windows.Forms.ToolStripMenuItem();
            this.大写形式_全部_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.大写形式_行首_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.大写形式_行尾_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.大写形式_自定义_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.小写形式Item = new System.Windows.Forms.ToolStripMenuItem();
            this.小写形式_全部_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.小写形式_行首_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.小写形式_行尾_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.小写形式_自定义_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.驼峰形式Item = new System.Windows.Forms.ToolStripMenuItem();
            this.驼峰形式_大驼峰_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.驼峰形式_小驼峰_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.清空文本框Item = new System.Windows.Forms.ToolStripMenuItem();
            this.智能选择Item = new System.Windows.Forms.ToolStripMenuItem();
            this.rightMenuStrip.SuspendLayout();
            // 
            // rightMenuStrip
            // 
            this.rightMenuStrip.AutoSize = false;
            this.rightMenuStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rightMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选Item,
            this.选中整行Item,
            this.智能选择Item,
            this.toolStripSeparator1,
            this.剪切Item,
            this.复制Item,
            this.粘贴Item,
            this.删除Item,
            this.toolStripSeparator2,
            this.去除Item,
            this.转化为Item,
            this.toolStripSeparator3,
            this.清空文本框Item});
            this.rightMenuStrip.Name = "contextMenuStrip1";
            this.rightMenuStrip.Size = new System.Drawing.Size(142, 198);
            this.rightMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.rightMenuStrip_Opening);
            // 
            // 全选Item
            // 
            this.全选Item.Name = "全选Item";
            this.全选Item.Size = new System.Drawing.Size(136, 22);
            this.全选Item.Text = "全选";
            // 
            // 选中整行Item
            // 
            this.选中整行Item.Name = "选中整行Item";
            this.选中整行Item.Size = new System.Drawing.Size(136, 22);
            this.选中整行Item.Text = "选中整行";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // 剪切Item
            // 
            this.剪切Item.Name = "剪切Item";
            this.剪切Item.Size = new System.Drawing.Size(136, 22);
            this.剪切Item.Text = "剪切";
            // 
            // 复制Item
            // 
            this.复制Item.Name = "复制Item";
            this.复制Item.Size = new System.Drawing.Size(136, 22);
            this.复制Item.Text = "复制";
            // 
            // 粘贴Item
            // 
            this.粘贴Item.Name = "粘贴Item";
            this.粘贴Item.Size = new System.Drawing.Size(136, 22);
            this.粘贴Item.Text = "粘贴";
            // 
            // 删除Item
            // 
            this.删除Item.Name = "删除Item";
            this.删除Item.Size = new System.Drawing.Size(136, 22);
            this.删除Item.Text = "删除";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // 去除Item
            // 
            this.去除Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.空格Item,
            this.空行Item,
            this.换行符Item,
            this.制表符Item});
            this.去除Item.Name = "去除Item";
            this.去除Item.Size = new System.Drawing.Size(136, 22);
            this.去除Item.Text = "去除";
            // 
            // 空格Item
            // 
            this.空格Item.BackColor = System.Drawing.SystemColors.Control;
            this.空格Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全部空格Item,
            this.行首空格Item,
            this.行尾空格Item});
            this.空格Item.Name = "空格Item";
            this.空格Item.Size = new System.Drawing.Size(112, 22);
            this.空格Item.Text = "空格";
            this.空格Item.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // 全部空格Item
            // 
            this.全部空格Item.BackColor = System.Drawing.SystemColors.Control;
            this.全部空格Item.Name = "全部空格Item";
            this.全部空格Item.Size = new System.Drawing.Size(100, 22);
            this.全部空格Item.Text = "全部";
            // 
            // 行首空格Item
            // 
            this.行首空格Item.BackColor = System.Drawing.SystemColors.Control;
            this.行首空格Item.Name = "行首空格Item";
            this.行首空格Item.Size = new System.Drawing.Size(100, 22);
            this.行首空格Item.Text = "行首";
            // 
            // 行尾空格Item
            // 
            this.行尾空格Item.BackColor = System.Drawing.SystemColors.Control;
            this.行尾空格Item.Name = "行尾空格Item";
            this.行尾空格Item.Size = new System.Drawing.Size(100, 22);
            this.行尾空格Item.Text = "行尾";
            // 
            // 空行Item
            // 
            this.空行Item.BackColor = System.Drawing.SystemColors.Control;
            this.空行Item.Name = "空行Item";
            this.空行Item.Size = new System.Drawing.Size(112, 22);
            this.空行Item.Text = "空行";
            // 
            // 换行符Item
            // 
            this.换行符Item.BackColor = System.Drawing.SystemColors.Control;
            this.换行符Item.Name = "换行符Item";
            this.换行符Item.Size = new System.Drawing.Size(112, 22);
            this.换行符Item.Text = "换行符";
            // 
            // 制表符Item
            // 
            this.制表符Item.BackColor = System.Drawing.SystemColors.Control;
            this.制表符Item.Name = "制表符Item";
            this.制表符Item.Size = new System.Drawing.Size(112, 22);
            this.制表符Item.Text = "制表符";
            // 
            // 转化为Item
            // 
            this.转化为Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.大写形式Item,
            this.小写形式Item,
            this.驼峰形式Item});
            this.转化为Item.Name = "转化为Item";
            this.转化为Item.Size = new System.Drawing.Size(136, 22);
            this.转化为Item.Text = "转化为";
            // 
            // 大写形式Item
            // 
            this.大写形式Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.大写形式_全部_Item,
            this.大写形式_行首_Item,
            this.大写形式_行尾_Item,
            this.大写形式_自定义_Item});
            this.大写形式Item.Name = "大写形式Item";
            this.大写形式Item.Size = new System.Drawing.Size(124, 22);
            this.大写形式Item.Text = "大写形式";
            // 
            // 大写形式_全部_Item
            // 
            this.大写形式_全部_Item.Name = "大写形式_全部_Item";
            this.大写形式_全部_Item.Size = new System.Drawing.Size(118, 22);
            this.大写形式_全部_Item.Text = "全部";
            // 
            // 大写形式_行首_Item
            // 
            this.大写形式_行首_Item.Name = "大写形式_行首_Item";
            this.大写形式_行首_Item.Size = new System.Drawing.Size(118, 22);
            this.大写形式_行首_Item.Text = "行首";
            // 
            // 大写形式_行尾_Item
            // 
            this.大写形式_行尾_Item.Name = "大写形式_行尾_Item";
            this.大写形式_行尾_Item.Size = new System.Drawing.Size(118, 22);
            this.大写形式_行尾_Item.Text = "行尾";
            // 
            // 大写形式_自定义_Item
            // 
            this.大写形式_自定义_Item.Name = "大写形式_自定义_Item";
            this.大写形式_自定义_Item.Size = new System.Drawing.Size(118, 22);
            this.大写形式_自定义_Item.Text = "自定义..";
            // 
            // 小写形式Item
            // 
            this.小写形式Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.小写形式_全部_Item,
            this.小写形式_行首_Item,
            this.小写形式_行尾_Item,
            this.小写形式_自定义_Item});
            this.小写形式Item.Name = "小写形式Item";
            this.小写形式Item.Size = new System.Drawing.Size(124, 22);
            this.小写形式Item.Text = "小写形式";
            // 
            // 小写形式_全部_Item
            // 
            this.小写形式_全部_Item.Name = "小写形式_全部_Item";
            this.小写形式_全部_Item.Size = new System.Drawing.Size(118, 22);
            this.小写形式_全部_Item.Text = "全部";
            // 
            // 小写形式_行首_Item
            // 
            this.小写形式_行首_Item.Name = "小写形式_行首_Item";
            this.小写形式_行首_Item.Size = new System.Drawing.Size(118, 22);
            this.小写形式_行首_Item.Text = "行首";
            // 
            // 小写形式_行尾_Item
            // 
            this.小写形式_行尾_Item.Name = "小写形式_行尾_Item";
            this.小写形式_行尾_Item.Size = new System.Drawing.Size(118, 22);
            this.小写形式_行尾_Item.Text = "行尾";
            // 
            // 小写形式_自定义_Item
            // 
            this.小写形式_自定义_Item.Name = "小写形式_自定义_Item";
            this.小写形式_自定义_Item.Size = new System.Drawing.Size(118, 22);
            this.小写形式_自定义_Item.Text = "自定义..";
            // 
            // 驼峰形式Item
            // 
            this.驼峰形式Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.驼峰形式_大驼峰_Item,
            this.驼峰形式_小驼峰_Item});
            this.驼峰形式Item.Name = "驼峰形式Item";
            this.驼峰形式Item.Size = new System.Drawing.Size(124, 22);
            this.驼峰形式Item.Text = "驼峰形式";
            // 
            // 驼峰形式_大驼峰_Item
            // 
            this.驼峰形式_大驼峰_Item.Name = "驼峰形式_大驼峰_Item";
            this.驼峰形式_大驼峰_Item.Size = new System.Drawing.Size(112, 22);
            this.驼峰形式_大驼峰_Item.Text = "大驼峰";
            // 
            // 驼峰形式_小驼峰_Item
            // 
            this.驼峰形式_小驼峰_Item.Name = "驼峰形式_小驼峰_Item";
            this.驼峰形式_小驼峰_Item.Size = new System.Drawing.Size(112, 22);
            this.驼峰形式_小驼峰_Item.Text = "小驼峰";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(138, 6);
            // 
            // 清空文本框Item
            // 
            this.清空文本框Item.Name = "清空文本框Item";
            this.清空文本框Item.Size = new System.Drawing.Size(136, 22);
            this.清空文本框Item.Text = "清空文本框";
            // 
            // 智能选择Item
            // 
            this.智能选择Item.Name = "智能选择Item";
            this.智能选择Item.Size = new System.Drawing.Size(136, 22);
            this.智能选择Item.Text = "智能选择";
            this.rightMenuStrip.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem 全选Item;
        private System.Windows.Forms.ToolStripMenuItem 剪切Item;
        private System.Windows.Forms.ToolStripMenuItem 复制Item;
        private System.Windows.Forms.ToolStripMenuItem 粘贴Item;
        private System.Windows.Forms.ToolStripMenuItem 删除Item;
        private System.Windows.Forms.ToolStripMenuItem 清空文本框Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 去除Item;
        private System.Windows.Forms.ToolStripMenuItem 空格Item;
        private System.Windows.Forms.ToolStripMenuItem 全部空格Item;
        private System.Windows.Forms.ToolStripMenuItem 行首空格Item;
        private System.Windows.Forms.ToolStripMenuItem 行尾空格Item;
        private System.Windows.Forms.ToolStripMenuItem 空行Item;
        private System.Windows.Forms.ToolStripMenuItem 换行符Item;
        private System.Windows.Forms.ToolStripMenuItem 制表符Item;
        private System.Windows.Forms.ToolStripMenuItem 转化为Item;
        private System.Windows.Forms.ToolStripMenuItem 大写形式Item;
        private System.Windows.Forms.ToolStripMenuItem 小写形式Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 驼峰形式Item;
        private System.Windows.Forms.ContextMenuStrip rightMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 大写形式_全部_Item;
        private System.Windows.Forms.ToolStripMenuItem 大写形式_行首_Item;
        private System.Windows.Forms.ToolStripMenuItem 大写形式_行尾_Item;
        private System.Windows.Forms.ToolStripMenuItem 大写形式_自定义_Item;
        private System.Windows.Forms.ToolStripMenuItem 小写形式_全部_Item;
        private System.Windows.Forms.ToolStripMenuItem 小写形式_行首_Item;
        private System.Windows.Forms.ToolStripMenuItem 小写形式_行尾_Item;
        private System.Windows.Forms.ToolStripMenuItem 小写形式_自定义_Item;
        private System.Windows.Forms.ToolStripMenuItem 驼峰形式_大驼峰_Item;
        private System.Windows.Forms.ToolStripMenuItem 驼峰形式_小驼峰_Item;
        private System.Windows.Forms.ToolStripMenuItem 选中整行Item;
        private System.Windows.Forms.ToolStripMenuItem 智能选择Item;
    }
}
