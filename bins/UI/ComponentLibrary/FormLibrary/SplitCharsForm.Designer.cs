namespace UI.ComponentLibrary.FormLibrary {
    partial class SplitCharsForm {
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
            this.分割设置容器 = new System.Windows.Forms.GroupBox();
            this.点_chk = new System.Windows.Forms.CheckBox();
            this.逗号_chk = new System.Windows.Forms.CheckBox();
            this.字符_textB = new System.Windows.Forms.TextBox();
            this.字符个数_textB = new System.Windows.Forms.TextBox();
            this.字符个数_rad = new System.Windows.Forms.RadioButton();
            this.字符_rad = new System.Windows.Forms.RadioButton();
            this.空格_chk = new System.Windows.Forms.CheckBox();
            this.冒号_chk = new System.Windows.Forms.CheckBox();
            this.分号_chk = new System.Windows.Forms.CheckBox();
            this.制表符_chk = new System.Windows.Forms.CheckBox();
            this.分割格式容器 = new System.Windows.Forms.GroupBox();
            this.整个文本_rad = new System.Windows.Forms.RadioButton();
            this.单个行_rad = new System.Windows.Forms.RadioButton();
            this.保留空列_chk = new System.Windows.Forms.CheckBox();
            this.分割_but = new System.Windows.Forms.Button();
            this.选项区容器 = new System.Windows.Forms.GroupBox();
            this.不包含制表符_chk = new System.Windows.Forms.CheckBox();
            this.不区分大小写_chk = new System.Windows.Forms.CheckBox();
            this.操作区容器 = new System.Windows.Forms.GroupBox();
            this.表格内容_label = new System.Windows.Forms.Label();
            this.分割设置容器.SuspendLayout();
            this.分割格式容器.SuspendLayout();
            this.选项区容器.SuspendLayout();
            this.操作区容器.SuspendLayout();
            this.SuspendLayout();
            // 
            // 分割设置容器
            // 
            this.分割设置容器.Controls.Add(this.点_chk);
            this.分割设置容器.Controls.Add(this.逗号_chk);
            this.分割设置容器.Controls.Add(this.字符_textB);
            this.分割设置容器.Controls.Add(this.字符个数_textB);
            this.分割设置容器.Controls.Add(this.字符个数_rad);
            this.分割设置容器.Controls.Add(this.字符_rad);
            this.分割设置容器.Controls.Add(this.空格_chk);
            this.分割设置容器.Controls.Add(this.冒号_chk);
            this.分割设置容器.Controls.Add(this.分号_chk);
            this.分割设置容器.Controls.Add(this.制表符_chk);
            this.分割设置容器.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.分割设置容器.Location = new System.Drawing.Point(8, 4);
            this.分割设置容器.Name = "分割设置容器";
            this.分割设置容器.Size = new System.Drawing.Size(164, 237);
            this.分割设置容器.TabIndex = 0;
            this.分割设置容器.TabStop = false;
            this.分割设置容器.Text = "分割设置";
            // 
            // 点_chk
            // 
            this.点_chk.AutoSize = true;
            this.点_chk.Location = new System.Drawing.Point(10, 75);
            this.点_chk.Name = "点_chk";
            this.点_chk.Size = new System.Drawing.Size(39, 21);
            this.点_chk.TabIndex = 17;
            this.点_chk.Text = "点";
            this.点_chk.UseVisualStyleBackColor = true;
            // 
            // 逗号_chk
            // 
            this.逗号_chk.AutoSize = true;
            this.逗号_chk.Location = new System.Drawing.Point(78, 76);
            this.逗号_chk.Name = "逗号_chk";
            this.逗号_chk.Size = new System.Drawing.Size(51, 21);
            this.逗号_chk.TabIndex = 16;
            this.逗号_chk.Text = "逗号";
            this.逗号_chk.UseVisualStyleBackColor = true;
            // 
            // 字符_textB
            // 
            this.字符_textB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.字符_textB.Location = new System.Drawing.Point(10, 117);
            this.字符_textB.MaxLength = 9999999;
            this.字符_textB.Multiline = true;
            this.字符_textB.Name = "字符_textB";
            this.字符_textB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.字符_textB.Size = new System.Drawing.Size(135, 63);
            this.字符_textB.TabIndex = 15;
            // 
            // 字符个数_textB
            // 
            this.字符个数_textB.Enabled = false;
            this.字符个数_textB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.字符个数_textB.Location = new System.Drawing.Point(10, 205);
            this.字符个数_textB.Name = "字符个数_textB";
            this.字符个数_textB.Size = new System.Drawing.Size(135, 26);
            this.字符个数_textB.TabIndex = 8;
            // 
            // 字符个数_rad
            // 
            this.字符个数_rad.AutoSize = true;
            this.字符个数_rad.Location = new System.Drawing.Point(10, 184);
            this.字符个数_rad.Name = "字符个数_rad";
            this.字符个数_rad.Size = new System.Drawing.Size(86, 21);
            this.字符个数_rad.TabIndex = 7;
            this.字符个数_rad.TabStop = true;
            this.字符个数_rad.Text = "字符个数：";
            this.字符个数_rad.UseVisualStyleBackColor = true;
            this.字符个数_rad.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // 字符_rad
            // 
            this.字符_rad.AutoSize = true;
            this.字符_rad.Checked = true;
            this.字符_rad.Location = new System.Drawing.Point(9, 96);
            this.字符_rad.Name = "字符_rad";
            this.字符_rad.Size = new System.Drawing.Size(62, 21);
            this.字符_rad.TabIndex = 6;
            this.字符_rad.TabStop = true;
            this.字符_rad.Text = "字符：";
            this.字符_rad.UseVisualStyleBackColor = true;
            this.字符_rad.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // 空格_chk
            // 
            this.空格_chk.AutoSize = true;
            this.空格_chk.Location = new System.Drawing.Point(78, 49);
            this.空格_chk.Name = "空格_chk";
            this.空格_chk.Size = new System.Drawing.Size(51, 21);
            this.空格_chk.TabIndex = 3;
            this.空格_chk.Text = "空格";
            this.空格_chk.UseVisualStyleBackColor = true;
            // 
            // 冒号_chk
            // 
            this.冒号_chk.AutoSize = true;
            this.冒号_chk.Location = new System.Drawing.Point(10, 48);
            this.冒号_chk.Name = "冒号_chk";
            this.冒号_chk.Size = new System.Drawing.Size(51, 21);
            this.冒号_chk.TabIndex = 2;
            this.冒号_chk.Text = "冒号";
            this.冒号_chk.UseVisualStyleBackColor = true;
            // 
            // 分号_chk
            // 
            this.分号_chk.AutoSize = true;
            this.分号_chk.Location = new System.Drawing.Point(78, 22);
            this.分号_chk.Name = "分号_chk";
            this.分号_chk.Size = new System.Drawing.Size(51, 21);
            this.分号_chk.TabIndex = 1;
            this.分号_chk.Text = "分号";
            this.分号_chk.UseVisualStyleBackColor = true;
            // 
            // 制表符_chk
            // 
            this.制表符_chk.AutoSize = true;
            this.制表符_chk.Location = new System.Drawing.Point(10, 22);
            this.制表符_chk.Name = "制表符_chk";
            this.制表符_chk.Size = new System.Drawing.Size(63, 21);
            this.制表符_chk.TabIndex = 0;
            this.制表符_chk.Text = "制表符";
            this.制表符_chk.UseVisualStyleBackColor = true;
            // 
            // 分割格式容器
            // 
            this.分割格式容器.Controls.Add(this.整个文本_rad);
            this.分割格式容器.Controls.Add(this.单个行_rad);
            this.分割格式容器.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.分割格式容器.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.分割格式容器.Location = new System.Drawing.Point(8, 247);
            this.分割格式容器.Name = "分割格式容器";
            this.分割格式容器.Size = new System.Drawing.Size(164, 50);
            this.分割格式容器.TabIndex = 6;
            this.分割格式容器.TabStop = false;
            this.分割格式容器.Text = "分割格式";
            // 
            // 整个文本_rad
            // 
            this.整个文本_rad.AutoSize = true;
            this.整个文本_rad.Location = new System.Drawing.Point(78, 22);
            this.整个文本_rad.Name = "整个文本_rad";
            this.整个文本_rad.Size = new System.Drawing.Size(74, 21);
            this.整个文本_rad.TabIndex = 1;
            this.整个文本_rad.Text = "整个文本";
            this.整个文本_rad.UseVisualStyleBackColor = true;
            this.整个文本_rad.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // 单个行_rad
            // 
            this.单个行_rad.AutoSize = true;
            this.单个行_rad.Checked = true;
            this.单个行_rad.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.单个行_rad.Location = new System.Drawing.Point(8, 22);
            this.单个行_rad.Name = "单个行_rad";
            this.单个行_rad.Size = new System.Drawing.Size(62, 21);
            this.单个行_rad.TabIndex = 0;
            this.单个行_rad.TabStop = true;
            this.单个行_rad.Text = "单个行";
            this.单个行_rad.UseVisualStyleBackColor = true;
            this.单个行_rad.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
            // 
            // 保留空列_chk
            // 
            this.保留空列_chk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.保留空列_chk.Location = new System.Drawing.Point(8, 16);
            this.保留空列_chk.Name = "保留空列_chk";
            this.保留空列_chk.Size = new System.Drawing.Size(136, 25);
            this.保留空列_chk.TabIndex = 7;
            this.保留空列_chk.Text = "保留空列";
            this.保留空列_chk.UseVisualStyleBackColor = true;
            this.保留空列_chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // 分割_but
            // 
            this.分割_but.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.分割_but.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(94)))), ((int)(((byte)(55)))));
            this.分割_but.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(215)))), ((int)(((byte)(215)))));
            this.分割_but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(94)))), ((int)(((byte)(55)))));
            this.分割_but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(83)))), ((int)(((byte)(44)))));
            this.分割_but.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.分割_but.ForeColor = System.Drawing.Color.White;
            this.分割_but.Location = new System.Drawing.Point(440, 16);
            this.分割_but.Name = "分割_but";
            this.分割_but.Size = new System.Drawing.Size(70, 25);
            this.分割_but.TabIndex = 8;
            this.分割_but.Text = "确认分割";
            this.分割_but.UseVisualStyleBackColor = false;
            this.分割_but.Click += new System.EventHandler(this.分列_but_Click);
            // 
            // 选项区容器
            // 
            this.选项区容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.选项区容器.Controls.Add(this.不包含制表符_chk);
            this.选项区容器.Controls.Add(this.不区分大小写_chk);
            this.选项区容器.Controls.Add(this.保留空列_chk);
            this.选项区容器.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.选项区容器.Location = new System.Drawing.Point(8, 299);
            this.选项区容器.Name = "选项区容器";
            this.选项区容器.Size = new System.Drawing.Size(164, 125);
            this.选项区容器.TabIndex = 13;
            this.选项区容器.TabStop = false;
            this.选项区容器.Text = "选项区";
            // 
            // 不包含制表符_chk
            // 
            this.不包含制表符_chk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.不包含制表符_chk.Location = new System.Drawing.Point(8, 72);
            this.不包含制表符_chk.Name = "不包含制表符_chk";
            this.不包含制表符_chk.Size = new System.Drawing.Size(136, 25);
            this.不包含制表符_chk.TabIndex = 9;
            this.不包含制表符_chk.Text = "导出时不包含制表符";
            this.不包含制表符_chk.UseVisualStyleBackColor = true;
            this.不包含制表符_chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // 不区分大小写_chk
            // 
            this.不区分大小写_chk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.不区分大小写_chk.Location = new System.Drawing.Point(8, 43);
            this.不区分大小写_chk.Name = "不区分大小写_chk";
            this.不区分大小写_chk.Size = new System.Drawing.Size(136, 25);
            this.不区分大小写_chk.TabIndex = 8;
            this.不区分大小写_chk.Text = "分隔符不区分大小写";
            this.不区分大小写_chk.UseVisualStyleBackColor = true;
            this.不区分大小写_chk.CheckedChanged += new System.EventHandler(this.Chk_CheckedChanged);
            // 
            // 操作区容器
            // 
            this.操作区容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.操作区容器.Controls.Add(this.表格内容_label);
            this.操作区容器.Controls.Add(this.分割_but);
            this.操作区容器.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.操作区容器.Location = new System.Drawing.Point(178, 4);
            this.操作区容器.Name = "操作区容器";
            this.操作区容器.Size = new System.Drawing.Size(520, 50);
            this.操作区容器.TabIndex = 14;
            this.操作区容器.TabStop = false;
            this.操作区容器.Text = "操作区";
            // 
            // 表格内容_label
            // 
            this.表格内容_label.AutoSize = true;
            this.表格内容_label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.表格内容_label.Location = new System.Drawing.Point(12, 21);
            this.表格内容_label.Name = "表格内容_label";
            this.表格内容_label.Size = new System.Drawing.Size(68, 17);
            this.表格内容_label.TabIndex = 9;
            this.表格内容_label.Text = "表格内容：";
            // 
            // SplitCharsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(707, 431);
            this.Controls.Add(this.分割格式容器);
            this.Controls.Add(this.操作区容器);
            this.Controls.Add(this.选项区容器);
            this.Controls.Add(this.分割设置容器);
            this.MinimizeBox = false;
            this.Name = "SplitCharsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "分割字符";
            this.Load += new System.EventHandler(this.SplitOrAddChars_Load);
            this.分割设置容器.ResumeLayout(false);
            this.分割设置容器.PerformLayout();
            this.分割格式容器.ResumeLayout(false);
            this.分割格式容器.PerformLayout();
            this.选项区容器.ResumeLayout(false);
            this.操作区容器.ResumeLayout(false);
            this.操作区容器.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox 分割设置容器;
        private System.Windows.Forms.CheckBox 制表符_chk;
        private System.Windows.Forms.CheckBox 分号_chk;
        private System.Windows.Forms.CheckBox 冒号_chk;
        private System.Windows.Forms.CheckBox 空格_chk;
        private System.Windows.Forms.GroupBox 分割格式容器;
        private System.Windows.Forms.RadioButton 单个行_rad;
        private System.Windows.Forms.RadioButton 整个文本_rad;
        private System.Windows.Forms.CheckBox 保留空列_chk;
        private System.Windows.Forms.Button 分割_but;
        private System.Windows.Forms.GroupBox 选项区容器;
        private System.Windows.Forms.RadioButton 字符_rad;
        private System.Windows.Forms.RadioButton 字符个数_rad;
        private System.Windows.Forms.TextBox 字符个数_textB;
        private System.Windows.Forms.CheckBox 不区分大小写_chk;
        private System.Windows.Forms.GroupBox 操作区容器;
        private System.Windows.Forms.CheckBox 不包含制表符_chk;
        private System.Windows.Forms.TextBox 字符_textB;
        private System.Windows.Forms.Label 表格内容_label;
        private System.Windows.Forms.CheckBox 逗号_chk;
        private System.Windows.Forms.CheckBox 点_chk;
    }
}