namespace ComponentLibrary {
    partial class DropdownBut {
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
            this.button1 = new System.Windows.Forms.Button();
            this.下拉按钮_but = new System.Windows.Forms.Button();
            this.下拉按钮_pan = new System.Windows.Forms.Panel();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(172, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 25);
            this.button1.TabIndex = 11;
            this.button1.Text = "▾";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.but_Click);
            this.button1.Leave += new System.EventHandler(this.but_Leave);
            this.button1.MouseEnter += new System.EventHandler(this.but_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.but_MouseLeave);
            // 
            // 下拉按钮_but
            // 
            this.下拉按钮_but.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(225)))), ((int)(((byte)(225)))));
            this.下拉按钮_but.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.下拉按钮_but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.下拉按钮_but.Location = new System.Drawing.Point(77, 19);
            this.下拉按钮_but.Name = "下拉按钮_but";
            this.下拉按钮_but.Size = new System.Drawing.Size(97, 25);
            this.下拉按钮_but.TabIndex = 10;
            this.下拉按钮_but.Text = "文字";
            this.下拉按钮_but.UseVisualStyleBackColor = false;
            this.下拉按钮_but.MouseEnter += new System.EventHandler(this.but_MouseEnter);
            this.下拉按钮_but.MouseLeave += new System.EventHandler(this.but_MouseLeave);
            // 
            // 下拉按钮_pan
            // 
            this.下拉按钮_pan.Location = new System.Drawing.Point(0, 0);
            this.下拉按钮_pan.Name = "下拉按钮_pan";
            this.下拉按钮_pan.Size = new System.Drawing.Size(200, 100);
            this.下拉按钮_pan.TabIndex = 0;

        }

        #endregion

        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button 下拉按钮_but;
        public System.Windows.Forms.Panel 下拉按钮_pan;
    }
}
