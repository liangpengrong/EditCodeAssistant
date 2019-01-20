namespace UI.ComponentLibrary.FormLibrary {
    partial class AddCharsForm {
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
            this.选项区容器 = new System.Windows.Forms.GroupBox();
            this.文本框自动换行_chk = new System.Windows.Forms.CheckBox();
            this.不匹配空行_chk = new System.Windows.Forms.CheckBox();
            this.不匹配末尾_chk = new System.Windows.Forms.CheckBox();
            this.tab容器 = new System.Windows.Forms.TabControl();
            this.普通_page = new System.Windows.Forms.TabPage();
            this.普通_操作容器 = new System.Windows.Forms.Panel();
            this.普通_行尾text = new System.Windows.Forms.TextBox();
            this.普通_行首text = new System.Windows.Forms.TextBox();
            this.普通_行尾历史but = new System.Windows.Forms.Button();
            this.普通_行首历史but = new System.Windows.Forms.Button();
            this.普通_行尾lab = new System.Windows.Forms.Label();
            this.普通_行首lab = new System.Windows.Forms.Label();
            this.普通_确定添加but = new System.Windows.Forms.Button();
            this.高级_page = new System.Windows.Forms.TabPage();
            this.选项区容器.SuspendLayout();
            this.tab容器.SuspendLayout();
            this.普通_page.SuspendLayout();
            this.普通_操作容器.SuspendLayout();
            this.SuspendLayout();
            // 
            // 选项区容器
            // 
            this.选项区容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.选项区容器.Controls.Add(this.文本框自动换行_chk);
            this.选项区容器.Controls.Add(this.不匹配空行_chk);
            this.选项区容器.Controls.Add(this.不匹配末尾_chk);
            this.选项区容器.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.选项区容器.Location = new System.Drawing.Point(2, 6);
            this.选项区容器.Name = "选项区容器";
            this.选项区容器.Size = new System.Drawing.Size(120, 430);
            this.选项区容器.TabIndex = 9999;
            this.选项区容器.TabStop = false;
            this.选项区容器.Text = "选项区";
            // 
            // 文本框自动换行_chk
            // 
            this.文本框自动换行_chk.AutoSize = true;
            this.文本框自动换行_chk.Location = new System.Drawing.Point(10, 91);
            this.文本框自动换行_chk.Name = "文本框自动换行_chk";
            this.文本框自动换行_chk.Size = new System.Drawing.Size(108, 16);
            this.文本框自动换行_chk.TabIndex = 7;
            this.文本框自动换行_chk.TabStop = false;
            this.文本框自动换行_chk.Text = "文本框自动换行";
            this.文本框自动换行_chk.UseVisualStyleBackColor = true;
            this.文本框自动换行_chk.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // 不匹配空行_chk
            // 
            this.不匹配空行_chk.AutoSize = true;
            this.不匹配空行_chk.Location = new System.Drawing.Point(11, 28);
            this.不匹配空行_chk.Name = "不匹配空行_chk";
            this.不匹配空行_chk.Size = new System.Drawing.Size(84, 16);
            this.不匹配空行_chk.TabIndex = 5;
            this.不匹配空行_chk.TabStop = false;
            this.不匹配空行_chk.Text = "不匹配空行";
            this.不匹配空行_chk.UseVisualStyleBackColor = true;
            this.不匹配空行_chk.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // 不匹配末尾_chk
            // 
            this.不匹配末尾_chk.AutoSize = true;
            this.不匹配末尾_chk.Location = new System.Drawing.Point(11, 60);
            this.不匹配末尾_chk.Name = "不匹配末尾_chk";
            this.不匹配末尾_chk.Size = new System.Drawing.Size(84, 16);
            this.不匹配末尾_chk.TabIndex = 6;
            this.不匹配末尾_chk.TabStop = false;
            this.不匹配末尾_chk.Text = "不匹配末尾";
            this.不匹配末尾_chk.UseVisualStyleBackColor = true;
            this.不匹配末尾_chk.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // tab容器
            // 
            this.tab容器.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab容器.Controls.Add(this.普通_page);
            this.tab容器.Controls.Add(this.高级_page);
            this.tab容器.HotTrack = true;
            this.tab容器.ItemSize = new System.Drawing.Size(80, 22);
            this.tab容器.Location = new System.Drawing.Point(126, 12);
            this.tab容器.Margin = new System.Windows.Forms.Padding(0);
            this.tab容器.Name = "tab容器";
            this.tab容器.SelectedIndex = 0;
            this.tab容器.Size = new System.Drawing.Size(614, 425);
            this.tab容器.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tab容器.TabIndex = 1111;
            this.tab容器.TabStop = false;
            this.tab容器.SelectedIndexChanged += new System.EventHandler(this.tab容器_SelectedIndexChanged);
            // 
            // 普通_page
            // 
            this.普通_page.Controls.Add(this.普通_操作容器);
            this.普通_page.Location = new System.Drawing.Point(4, 26);
            this.普通_page.Margin = new System.Windows.Forms.Padding(0);
            this.普通_page.Name = "普通_page";
            this.普通_page.Size = new System.Drawing.Size(606, 395);
            this.普通_page.TabIndex = 0;
            this.普通_page.Text = "普通";
            this.普通_page.ToolTipText = "普通模式";
            this.普通_page.UseVisualStyleBackColor = true;
            // 
            // 普通_操作容器
            // 
            this.普通_操作容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.普通_操作容器.Controls.Add(this.普通_行尾text);
            this.普通_操作容器.Controls.Add(this.普通_行首text);
            this.普通_操作容器.Controls.Add(this.普通_行尾历史but);
            this.普通_操作容器.Controls.Add(this.普通_行首历史but);
            this.普通_操作容器.Controls.Add(this.普通_行尾lab);
            this.普通_操作容器.Controls.Add(this.普通_行首lab);
            this.普通_操作容器.Controls.Add(this.普通_确定添加but);
            this.普通_操作容器.Location = new System.Drawing.Point(-4, -3);
            this.普通_操作容器.Margin = new System.Windows.Forms.Padding(0);
            this.普通_操作容器.Name = "普通_操作容器";
            this.普通_操作容器.Size = new System.Drawing.Size(614, 40);
            this.普通_操作容器.TabIndex = 5;
            this.普通_操作容器.Paint += new System.Windows.Forms.PaintEventHandler(this.普通_操作容器_Paint);
            // 
            // 普通_行尾text
            // 
            this.普通_行尾text.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.普通_行尾text.Location = new System.Drawing.Point(221, 10);
            this.普通_行尾text.Multiline = true;
            this.普通_行尾text.Name = "普通_行尾text";
            this.普通_行尾text.Size = new System.Drawing.Size(99, 23);
            this.普通_行尾text.TabIndex = 5;
            this.普通_行尾text.WordWrap = false;
            this.普通_行尾text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_KeyDown);
            // 
            // 普通_行首text
            // 
            this.普通_行首text.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.普通_行首text.Location = new System.Drawing.Point(52, 10);
            this.普通_行首text.Multiline = true;
            this.普通_行首text.Name = "普通_行首text";
            this.普通_行首text.Size = new System.Drawing.Size(99, 23);
            this.普通_行首text.TabIndex = 2;
            this.普通_行首text.WordWrap = false;
            this.普通_行首text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_KeyDown);
            // 
            // 普通_行尾历史but
            // 
            this.普通_行尾历史but.BackColor = System.Drawing.Color.Transparent;
            this.普通_行尾历史but.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.普通_行尾历史but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.普通_行尾历史but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.普通_行尾历史but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.普通_行尾历史but.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.普通_行尾历史but.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.普通_行尾历史but.Location = new System.Drawing.Point(319, 10);
            this.普通_行尾历史but.Name = "普通_行尾历史but";
            this.普通_行尾历史but.Size = new System.Drawing.Size(23, 23);
            this.普通_行尾历史but.TabIndex = 12;
            this.普通_行尾历史but.TabStop = false;
            this.普通_行尾历史but.Text = "▾";
            this.普通_行尾历史but.UseVisualStyleBackColor = false;
            this.普通_行尾历史but.Click += new System.EventHandler(this.历史but_Click);
            // 
            // 普通_行首历史but
            // 
            this.普通_行首历史but.BackColor = System.Drawing.Color.Transparent;
            this.普通_行首历史but.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.普通_行首历史but.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.普通_行首历史but.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.普通_行首历史but.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.普通_行首历史but.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.普通_行首历史but.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.普通_行首历史but.Location = new System.Drawing.Point(150, 10);
            this.普通_行首历史but.Name = "普通_行首历史but";
            this.普通_行首历史but.Size = new System.Drawing.Size(23, 23);
            this.普通_行首历史but.TabIndex = 11;
            this.普通_行首历史but.TabStop = false;
            this.普通_行首历史but.Text = "▾";
            this.普通_行首历史but.UseVisualStyleBackColor = false;
            this.普通_行首历史but.Click += new System.EventHandler(this.历史but_Click);
            // 
            // 普通_行尾lab
            // 
            this.普通_行尾lab.AutoSize = true;
            this.普通_行尾lab.Location = new System.Drawing.Point(182, 17);
            this.普通_行尾lab.Name = "普通_行尾lab";
            this.普通_行尾lab.Size = new System.Drawing.Size(41, 12);
            this.普通_行尾lab.TabIndex = 6;
            this.普通_行尾lab.Text = "行尾：";
            // 
            // 普通_行首lab
            // 
            this.普通_行首lab.AutoSize = true;
            this.普通_行首lab.Location = new System.Drawing.Point(13, 17);
            this.普通_行首lab.Name = "普通_行首lab";
            this.普通_行首lab.Size = new System.Drawing.Size(41, 12);
            this.普通_行首lab.TabIndex = 4;
            this.普通_行首lab.Text = "行首：";
            // 
            // 普通_确定添加but
            // 
            this.普通_确定添加but.Location = new System.Drawing.Point(357, 10);
            this.普通_确定添加but.Name = "普通_确定添加but";
            this.普通_确定添加but.Size = new System.Drawing.Size(75, 23);
            this.普通_确定添加but.TabIndex = 3;
            this.普通_确定添加but.Text = "确定";
            this.普通_确定添加but.UseVisualStyleBackColor = true;
            this.普通_确定添加but.Click += new System.EventHandler(this.普通_确定添加but_Click);
            // 
            // 高级_page
            // 
            this.高级_page.Location = new System.Drawing.Point(4, 26);
            this.高级_page.Name = "高级_page";
            this.高级_page.Padding = new System.Windows.Forms.Padding(3);
            this.高级_page.Size = new System.Drawing.Size(606, 395);
            this.高级_page.TabIndex = 1;
            this.高级_page.Text = "高级";
            this.高级_page.UseVisualStyleBackColor = true;
            // 
            // AddCharsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 439);
            this.Controls.Add(this.tab容器);
            this.Controls.Add(this.选项区容器);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "AddCharsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加字符";
            this.Load += new System.EventHandler(this.AddCharsForm_Load);
            this.选项区容器.ResumeLayout(false);
            this.选项区容器.PerformLayout();
            this.tab容器.ResumeLayout(false);
            this.普通_page.ResumeLayout(false);
            this.普通_操作容器.ResumeLayout(false);
            this.普通_操作容器.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox 选项区容器;
        private System.Windows.Forms.TabControl tab容器;
        private System.Windows.Forms.TabPage 普通_page;
        private System.Windows.Forms.TabPage 高级_page;
        private System.Windows.Forms.Button 普通_确定添加but;
        private System.Windows.Forms.Panel 普通_操作容器;
        private System.Windows.Forms.CheckBox 不匹配末尾_chk;
        private System.Windows.Forms.CheckBox 不匹配空行_chk;
        private System.Windows.Forms.CheckBox 文本框自动换行_chk;
        private System.Windows.Forms.TextBox 普通_行首text;
        private System.Windows.Forms.Label 普通_行首lab;
        private System.Windows.Forms.Label 普通_行尾lab;
        private System.Windows.Forms.TextBox 普通_行尾text;
        private System.Windows.Forms.Button 普通_行首历史but;
        private System.Windows.Forms.Button 普通_行尾历史but;
    }
}