namespace UI.ComponentLibrary.ControlLibrary.RightMenu {
    partial class DataGridViewRightMenu {
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.table_rightStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制选中Item = new System.Windows.Forms.ToolStripMenuItem();
            this.复制全部Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.导出到记事本Item = new System.Windows.Forms.ToolStripMenuItem();
            this.导出到Excel_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.选中此列Item = new System.Windows.Forms.ToolStripMenuItem();
            this.选中此行Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.同步选中单元格数Item = new System.Windows.Forms.ToolStripMenuItem();
            this.同步选中单元格_该列Item = new System.Windows.Forms.ToolStripMenuItem();
            this.同步选中单元格_该行Item = new System.Windows.Forms.ToolStripMenuItem();
            this.同步选中单元格_行和列Item = new System.Windows.Forms.ToolStripMenuItem();
            this.table_rightStrip.SuspendLayout();
            // 
            // table_rightStrip
            // 
            this.table_rightStrip.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.table_rightStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制选中Item,
            this.复制全部Item,
            this.toolStripSeparator2,
            this.导出到记事本Item,
            this.导出到Excel_Item,
            this.toolStripSeparator1,
            this.选中此列Item,
            this.选中此行Item,
            this.toolStripSeparator3,
            this.同步选中单元格数Item});
            this.table_rightStrip.Name = "table_rightStrip";
            this.table_rightStrip.Size = new System.Drawing.Size(197, 176);
            this.table_rightStrip.Opening += new System.ComponentModel.CancelEventHandler(this.table_rightStrip_Opening);
            // 
            // 复制选中Item
            // 
            this.复制选中Item.Name = "复制选中Item";
            this.复制选中Item.Size = new System.Drawing.Size(196, 22);
            this.复制选中Item.Text = "复制选中";
            // 
            // 复制全部Item
            // 
            this.复制全部Item.Name = "复制全部Item";
            this.复制全部Item.Size = new System.Drawing.Size(196, 22);
            this.复制全部Item.Text = "复制全部";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(193, 6);
            // 
            // 导出到记事本Item
            // 
            this.导出到记事本Item.Name = "导出到记事本Item";
            this.导出到记事本Item.Size = new System.Drawing.Size(196, 22);
            this.导出到记事本Item.Text = "导出到记事本";
            // 
            // 导出到Excel_Item
            // 
            this.导出到Excel_Item.Name = "导出到Excel_Item";
            this.导出到Excel_Item.Size = new System.Drawing.Size(196, 22);
            this.导出到Excel_Item.Text = "导出到Excel";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(193, 6);
            // 
            // 选中此列Item
            // 
            this.选中此列Item.Name = "选中此列Item";
            this.选中此列Item.Size = new System.Drawing.Size(196, 22);
            this.选中此列Item.Text = "选中此列";
            // 
            // 选中此行Item
            // 
            this.选中此行Item.Name = "选中此行Item";
            this.选中此行Item.Size = new System.Drawing.Size(196, 22);
            this.选中此行Item.Text = "选中此行";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // 同步选中单元格数Item
            // 
            this.同步选中单元格数Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.同步选中单元格_该列Item,
            this.同步选中单元格_该行Item,
            this.同步选中单元格_行和列Item});
            this.同步选中单元格数Item.Name = "同步选中单元格数Item";
            this.同步选中单元格数Item.Size = new System.Drawing.Size(196, 22);
            this.同步选中单元格数Item.Text = "同步选中单元格数据到";
            // 
            // 同步选中单元格_该列Item
            // 
            this.同步选中单元格_该列Item.Name = "同步选中单元格_该列Item";
            this.同步选中单元格_该列Item.Size = new System.Drawing.Size(112, 22);
            this.同步选中单元格_该列Item.Text = "该列";
            // 
            // 同步选中单元格_该行Item
            // 
            this.同步选中单元格_该行Item.Name = "同步选中单元格_该行Item";
            this.同步选中单元格_该行Item.Size = new System.Drawing.Size(112, 22);
            this.同步选中单元格_该行Item.Text = "该行";
            // 
            // 同步选中单元格_行和列Item
            // 
            this.同步选中单元格_行和列Item.Name = "同步选中单元格_行和列Item";
            this.同步选中单元格_行和列Item.Size = new System.Drawing.Size(112, 22);
            this.同步选中单元格_行和列Item.Text = "行和列";
            this.table_rightStrip.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ContextMenuStrip table_rightStrip;
        private System.Windows.Forms.ToolStripMenuItem 复制选中Item;
        private System.Windows.Forms.ToolStripMenuItem 复制全部Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 导出到记事本Item;
        private System.Windows.Forms.ToolStripMenuItem 导出到Excel_Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 选中此列Item;
        private System.Windows.Forms.ToolStripMenuItem 选中此行Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 同步选中单元格数Item;
        private System.Windows.Forms.ToolStripMenuItem 同步选中单元格_该列Item;
        private System.Windows.Forms.ToolStripMenuItem 同步选中单元格_该行Item;
        private System.Windows.Forms.ToolStripMenuItem 同步选中单元格_行和列Item;
    }
}
