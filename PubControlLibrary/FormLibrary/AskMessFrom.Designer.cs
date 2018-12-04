namespace PubControlLibrary {
    partial class AskMessFrom {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.确定_but = new System.Windows.Forms.Button();
            this.取消_but = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.消息_lab = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // 确定_but
            // 
            this.确定_but.Location = new System.Drawing.Point(85, 86);
            this.确定_but.Name = "确定_but";
            this.确定_but.Size = new System.Drawing.Size(75, 28);
            this.确定_but.TabIndex = 0;
            this.确定_but.Text = "确定";
            this.确定_but.UseVisualStyleBackColor = true;
            // 
            // 取消_but
            // 
            this.取消_but.Location = new System.Drawing.Point(181, 86);
            this.取消_but.Name = "取消_but";
            this.取消_but.Size = new System.Drawing.Size(75, 28);
            this.取消_but.TabIndex = 1;
            this.取消_but.Text = "取消";
            this.取消_but.UseVisualStyleBackColor = true;
            this.取消_but.Click += new System.EventHandler(this.取消_but_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PubControlLibrary.Properties.Resources.警告;
            this.pictureBox1.Location = new System.Drawing.Point(18, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // 消息_lab
            // 
            this.消息_lab.AutoEllipsis = true;
            this.消息_lab.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.消息_lab.Location = new System.Drawing.Point(58, 18);
            this.消息_lab.Name = "消息_lab";
            this.消息_lab.Size = new System.Drawing.Size(230, 51);
            this.消息_lab.TabIndex = 3;
            this.消息_lab.Text = "示例文本";
            // 
            // AskMessFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 129);
            this.Controls.Add(this.消息_lab);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.取消_but);
            this.Controls.Add(this.确定_but);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AskMessFrom";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button 确定_but;
        public System.Windows.Forms.Button 取消_but;
        public System.Windows.Forms.Label 消息_lab;
    }
}