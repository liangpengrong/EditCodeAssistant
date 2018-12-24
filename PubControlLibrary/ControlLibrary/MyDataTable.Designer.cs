namespace PubControlLibrary {
    partial class MyDataTable {
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.数据表格 = new System.Windows.Forms.DataGridView();
            this.table_rightStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制选中Item = new System.Windows.Forms.ToolStripMenuItem();
            this.复制全部Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.导出到记事本Item = new System.Windows.Forms.ToolStripMenuItem();
            this.导出到Excel_Item = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.选中此列Item = new System.Windows.Forms.ToolStripMenuItem();
            this.选中此行Item = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.数据表格)).BeginInit();
            this.table_rightStrip.SuspendLayout();
            // 
            // 数据表格
            // 
            this.数据表格.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.数据表格.BackgroundColor = System.Drawing.Color.White;
            this.数据表格.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.数据表格.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(198)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.数据表格.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.数据表格.ColumnHeadersHeight = 28;
            this.数据表格.ContextMenuStrip = this.table_rightStrip;
            this.数据表格.EnableHeadersVisualStyles = false;
            this.数据表格.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(212)))), ((int)(((byte)(212)))));
            this.数据表格.Location = new System.Drawing.Point(188, 12);
            this.数据表格.Name = "数据表格";
            this.数据表格.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(1);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(198)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.数据表格.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(198)))), ((int)(((byte)(181)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.数据表格.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.数据表格.RowTemplate.Height = 23;
            this.数据表格.Size = new System.Drawing.Size(503, 398);
            this.数据表格.TabIndex = 0;
            this.数据表格.KeyDown += new System.Windows.Forms.KeyEventHandler(this.数据表格_KeyDown);
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
            this.选中此行Item});
            this.table_rightStrip.Name = "table_rightStrip";
            this.table_rightStrip.Size = new System.Drawing.Size(149, 148);
            // 
            // 复制选中Item
            // 
            this.复制选中Item.Name = "复制选中Item";
            this.复制选中Item.Size = new System.Drawing.Size(148, 22);
            this.复制选中Item.Text = "复制选中";
            this.复制选中Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightStripMenuItem_MouseDown);
            // 
            // 复制全部Item
            // 
            this.复制全部Item.Name = "复制全部Item";
            this.复制全部Item.Size = new System.Drawing.Size(148, 22);
            this.复制全部Item.Text = "复制全部";
            this.复制全部Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightStripMenuItem_MouseDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // 导出到记事本Item
            // 
            this.导出到记事本Item.Name = "导出到记事本Item";
            this.导出到记事本Item.Size = new System.Drawing.Size(148, 22);
            this.导出到记事本Item.Text = "导出到记事本";
            this.导出到记事本Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightStripMenuItem_MouseDown);
            // 
            // 导出到Excel_Item
            // 
            this.导出到Excel_Item.Name = "导出到Excel_Item";
            this.导出到Excel_Item.Size = new System.Drawing.Size(148, 22);
            this.导出到Excel_Item.Text = "导出到Excel";
            this.导出到Excel_Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightStripMenuItem_MouseDown);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 选中此列Item
            // 
            this.选中此列Item.Name = "选中此列Item";
            this.选中此列Item.Size = new System.Drawing.Size(148, 22);
            this.选中此列Item.Text = "选中此列";
            this.选中此列Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightStripMenuItem_MouseDown);
            // 
            // 选中此行Item
            // 
            this.选中此行Item.Name = "选中此行Item";
            this.选中此行Item.Size = new System.Drawing.Size(148, 22);
            this.选中此行Item.Text = "选中此行";
            this.选中此行Item.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rightStripMenuItem_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.数据表格)).EndInit();
            this.table_rightStrip.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView 数据表格;
        private System.Windows.Forms.ContextMenuStrip table_rightStrip;
        private System.Windows.Forms.ToolStripMenuItem 复制选中Item;
        private System.Windows.Forms.ToolStripMenuItem 复制全部Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 导出到记事本Item;
        private System.Windows.Forms.ToolStripMenuItem 导出到Excel_Item;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 选中此列Item;
        private System.Windows.Forms.ToolStripMenuItem 选中此行Item;
    }
}
