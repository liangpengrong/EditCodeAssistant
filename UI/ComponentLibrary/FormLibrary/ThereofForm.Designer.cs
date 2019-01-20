namespace UI.ComponentLibrary.FormLibrary {
    partial class ThereofForm {
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
            this.programName = new System.Windows.Forms.Label();
            this.version = new System.Windows.Forms.Label();
            this.复制信息_but = new System.Windows.Forms.Button();
            this.系统信息_but = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.github_but = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // programName
            // 
            this.programName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.programName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.programName.Font = new System.Drawing.Font("微软雅黑", 18F);
            this.programName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(174)))));
            this.programName.Location = new System.Drawing.Point(-6, -6);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(546, 58);
            this.programName.TabIndex = 1;
            this.programName.Text = "关于 CharsToolSet";
            this.programName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // version
            // 
            this.version.AutoSize = true;
            this.version.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.version.Location = new System.Drawing.Point(5, 70);
            this.version.Name = "version";
            this.version.Size = new System.Drawing.Size(43, 17);
            this.version.TabIndex = 2;
            this.version.Text = "label2";
            // 
            // 复制信息_but
            // 
            this.复制信息_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.复制信息_but.Location = new System.Drawing.Point(451, 243);
            this.复制信息_but.Name = "复制信息_but";
            this.复制信息_but.Size = new System.Drawing.Size(75, 23);
            this.复制信息_but.TabIndex = 4;
            this.复制信息_but.Text = "复制信息";
            this.复制信息_but.UseVisualStyleBackColor = true;
            this.复制信息_but.Click += new System.EventHandler(this.复制信息_but_Click);
            // 
            // 系统信息_but
            // 
            this.系统信息_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.系统信息_but.Location = new System.Drawing.Point(451, 280);
            this.系统信息_but.Name = "系统信息_but";
            this.系统信息_but.Size = new System.Drawing.Size(75, 23);
            this.系统信息_but.TabIndex = 5;
            this.系统信息_but.Text = "系统信息";
            this.系统信息_but.UseVisualStyleBackColor = true;
            this.系统信息_but.Click += new System.EventHandler(this.系统信息_but_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "系统信息如下：";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(8, 206);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(439, 331);
            this.tabControl1.TabIndex = 7;
            // 
            // github_but
            // 
            this.github_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.github_but.Location = new System.Drawing.Point(451, 315);
            this.github_but.Name = "github_but";
            this.github_but.Size = new System.Drawing.Size(75, 23);
            this.github_but.TabIndex = 8;
            this.github_but.Text = "复制GitHub";
            this.github_but.UseVisualStyleBackColor = true;
            this.github_but.Click += new System.EventHandler(this.github_but_Click);
            // 
            // ThereofForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 540);
            this.Controls.Add(this.github_but);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.系统信息_but);
            this.Controls.Add(this.复制信息_but);
            this.Controls.Add(this.version);
            this.Controls.Add(this.programName);
            this.MinimizeBox = false;
            this.Name = "ThereofForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ThereofForm";
            this.Load += new System.EventHandler(this.ThereofForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label programName;
        private System.Windows.Forms.Label version;
        private System.Windows.Forms.Button 复制信息_but;
        private System.Windows.Forms.Button 系统信息_but;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button github_but;
    }
}