namespace UI.ComponentLibrary.ControlLibrary {
    partial class ExportComBox {
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
            this.export_combox = new System.Windows.Forms.ComboBox();
            // 
            // export_combox
            // 
            this.export_combox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.export_combox.FormattingEnabled = true;
            this.export_combox.ItemHeight = 12;
            this.export_combox.Location = new System.Drawing.Point(7, 20);
            this.export_combox.Name = "export_combox";
            this.export_combox.Size = new System.Drawing.Size(121, 20);
            this.export_combox.TabIndex = 9;

        }

        #endregion

        public System.Windows.Forms.ComboBox export_combox;
    }
}
