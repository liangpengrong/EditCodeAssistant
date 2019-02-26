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
            this.复制信息_but = new System.Windows.Forms.Button();
            this.系统信息_but = new System.Windows.Forms.Button();
            this.系统信息_lab = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.github_but = new System.Windows.Forms.Button();
            this.head_textB = new System.Windows.Forms.TextBox();
            this.描述_lab = new System.Windows.Forms.Label();
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
            this.programName.Size = new System.Drawing.Size(699, 58);
            this.programName.TabIndex = 1;
            this.programName.Text = "关于 CharsToolSet";
            this.programName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // 复制信息_but
            // 
            this.复制信息_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.复制信息_but.Location = new System.Drawing.Point(604, 257);
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
            this.系统信息_but.Location = new System.Drawing.Point(604, 294);
            this.系统信息_but.Name = "系统信息_but";
            this.系统信息_but.Size = new System.Drawing.Size(75, 23);
            this.系统信息_but.TabIndex = 5;
            this.系统信息_but.Text = "系统信息";
            this.系统信息_but.UseVisualStyleBackColor = true;
            this.系统信息_but.Click += new System.EventHandler(this.系统信息_but_Click);
            // 
            // 系统信息_lab
            // 
            this.系统信息_lab.AutoSize = true;
            this.系统信息_lab.Location = new System.Drawing.Point(5, 201);
            this.系统信息_lab.Name = "系统信息_lab";
            this.系统信息_lab.Size = new System.Drawing.Size(65, 12);
            this.系统信息_lab.TabIndex = 6;
            this.系统信息_lab.Text = "系统信息：";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Location = new System.Drawing.Point(8, 220);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(592, 331);
            this.tabControl1.TabIndex = 7;
            // 
            // github_but
            // 
            this.github_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.github_but.Location = new System.Drawing.Point(604, 329);
            this.github_but.Name = "github_but";
            this.github_but.Size = new System.Drawing.Size(75, 23);
            this.github_but.TabIndex = 8;
            this.github_but.Text = "复制GitHub";
            this.github_but.UseVisualStyleBackColor = true;
            this.github_but.Click += new System.EventHandler(this.github_but_Click);
            // 
            // head_textB
            // 
            this.head_textB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.head_textB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.head_textB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.head_textB.ForeColor = System.Drawing.Color.LightCoral;
            this.head_textB.Location = new System.Drawing.Point(9, 82);
            this.head_textB.MaxLength = 9999999;
            this.head_textB.Multiline = true;
            this.head_textB.Name = "head_textB";
            this.head_textB.ReadOnly = true;
            this.head_textB.Size = new System.Drawing.Size(670, 110);
            this.head_textB.TabIndex = 9;
            this.head_textB.TabStop = false;
            this.head_textB.WordWrap = false;
            // 
            // 描述_lab
            // 
            this.描述_lab.AutoSize = true;
            this.描述_lab.Location = new System.Drawing.Point(6, 63);
            this.描述_lab.Name = "描述_lab";
            this.描述_lab.Size = new System.Drawing.Size(41, 12);
            this.描述_lab.TabIndex = 10;
            this.描述_lab.Text = "描述：";
            // 
            // ThereofForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 558);
            this.Controls.Add(this.描述_lab);
            this.Controls.Add(this.head_textB);
            this.Controls.Add(this.github_but);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.系统信息_lab);
            this.Controls.Add(this.系统信息_but);
            this.Controls.Add(this.复制信息_but);
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
        private System.Windows.Forms.Button 复制信息_but;
        private System.Windows.Forms.Button 系统信息_but;
        private System.Windows.Forms.Label 系统信息_lab;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button github_but;
        private System.Windows.Forms.TextBox head_textB;
        private System.Windows.Forms.Label 描述_lab;
    }
}