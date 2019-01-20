namespace UI.ComponentLibrary.FormLibrary
{
    partial class FindAndReplace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.查找内容L = new System.Windows.Forms.Label();
            this.查找内容T = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.替换内容T = new System.Windows.Forms.TextBox();
            this.方向G = new System.Windows.Forms.GroupBox();
            this.向下R = new System.Windows.Forms.RadioButton();
            this.向上R = new System.Windows.Forms.RadioButton();
            this.文本范围G = new System.Windows.Forms.GroupBox();
            this.选定内容R = new System.Windows.Forms.RadioButton();
            this.当前文档R = new System.Windows.Forms.RadioButton();
            this.选项G = new System.Windows.Forms.GroupBox();
            this.区分大小写C = new System.Windows.Forms.CheckBox();
            this.到达末尾C = new System.Windows.Forms.CheckBox();
            this.查找B = new System.Windows.Forms.Button();
            this.替换B = new System.Windows.Forms.Button();
            this.全部替换B = new System.Windows.Forms.Button();
            this.关闭B = new System.Windows.Forms.Button();
            this.查找历史B = new System.Windows.Forms.Button();
            this.替换历史B = new System.Windows.Forms.Button();
            this.方向G.SuspendLayout();
            this.文本范围G.SuspendLayout();
            this.选项G.SuspendLayout();
            this.SuspendLayout();
            // 
            // 查找内容L
            // 
            this.查找内容L.AutoSize = true;
            this.查找内容L.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.查找内容L.Location = new System.Drawing.Point(20, 32);
            this.查找内容L.Name = "查找内容L";
            this.查找内容L.Size = new System.Drawing.Size(63, 17);
            this.查找内容L.TabIndex = 0;
            this.查找内容L.Text = "查找内容: ";
            // 
            // 查找内容T
            // 
            this.查找内容T.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.查找内容T.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.查找内容T.Location = new System.Drawing.Point(82, 27);
            this.查找内容T.MaxLength = 99999999;
            this.查找内容T.Multiline = true;
            this.查找内容T.Name = "查找内容T";
            this.查找内容T.Size = new System.Drawing.Size(213, 25);
            this.查找内容T.TabIndex = 1;
            this.查找内容T.WordWrap = false;
            this.查找内容T.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.查找内容T.Enter += new System.EventHandler(this.TextBox_Enter);
            this.查找内容T.KeyDown += new System.Windows.Forms.KeyEventHandler(this.查找内容T_KeyDown);
            this.查找内容T.Leave += new System.EventHandler(this.TextBox_Leave);
            this.查找内容T.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label1.Location = new System.Drawing.Point(20, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "替换为: ";
            // 
            // 替换内容T
            // 
            this.替换内容T.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.替换内容T.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.替换内容T.Location = new System.Drawing.Point(82, 80);
            this.替换内容T.MaxLength = 99999999;
            this.替换内容T.Multiline = true;
            this.替换内容T.Name = "替换内容T";
            this.替换内容T.Size = new System.Drawing.Size(213, 25);
            this.替换内容T.TabIndex = 3;
            this.替换内容T.WordWrap = false;
            this.替换内容T.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.替换内容T.Enter += new System.EventHandler(this.TextBox_Enter);
            this.替换内容T.KeyDown += new System.Windows.Forms.KeyEventHandler(this.替换内容T_KeyDown);
            this.替换内容T.Leave += new System.EventHandler(this.TextBox_Leave);
            this.替换内容T.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TextBox_MouseDown);
            // 
            // 方向G
            // 
            this.方向G.Controls.Add(this.向下R);
            this.方向G.Controls.Add(this.向上R);
            this.方向G.Location = new System.Drawing.Point(15, 119);
            this.方向G.Name = "方向G";
            this.方向G.Size = new System.Drawing.Size(99, 107);
            this.方向G.TabIndex = 4;
            this.方向G.TabStop = false;
            this.方向G.Text = "方向";
            // 
            // 向下R
            // 
            this.向下R.Checked = true;
            this.向下R.Location = new System.Drawing.Point(15, 63);
            this.向下R.Name = "向下R";
            this.向下R.Size = new System.Drawing.Size(70, 30);
            this.向下R.TabIndex = 1;
            this.向下R.TabStop = true;
            this.向下R.Text = "向下";
            this.向下R.UseVisualStyleBackColor = true;
            this.向下R.CheckedChanged += new System.EventHandler(this.向下R_CheckedChanged);
            // 
            // 向上R
            // 
            this.向上R.Location = new System.Drawing.Point(15, 21);
            this.向上R.Name = "向上R";
            this.向上R.Size = new System.Drawing.Size(70, 30);
            this.向上R.TabIndex = 0;
            this.向上R.Text = "向上";
            this.向上R.UseVisualStyleBackColor = true;
            this.向上R.CheckedChanged += new System.EventHandler(this.向上R_CheckedChanged);
            // 
            // 文本范围G
            // 
            this.文本范围G.Controls.Add(this.选定内容R);
            this.文本范围G.Controls.Add(this.当前文档R);
            this.文本范围G.Location = new System.Drawing.Point(15, 233);
            this.文本范围G.Name = "文本范围G";
            this.文本范围G.Size = new System.Drawing.Size(99, 115);
            this.文本范围G.TabIndex = 5;
            this.文本范围G.TabStop = false;
            this.文本范围G.Text = "文本范围";
            // 
            // 选定内容R
            // 
            this.选定内容R.Location = new System.Drawing.Point(13, 72);
            this.选定内容R.Name = "选定内容R";
            this.选定内容R.Size = new System.Drawing.Size(80, 30);
            this.选定内容R.TabIndex = 3;
            this.选定内容R.Text = "选定内容";
            this.选定内容R.UseVisualStyleBackColor = true;
            this.选定内容R.CheckedChanged += new System.EventHandler(this.选定内容R_CheckedChanged);
            // 
            // 当前文档R
            // 
            this.当前文档R.Checked = true;
            this.当前文档R.Location = new System.Drawing.Point(13, 30);
            this.当前文档R.Name = "当前文档R";
            this.当前文档R.Size = new System.Drawing.Size(80, 30);
            this.当前文档R.TabIndex = 2;
            this.当前文档R.TabStop = true;
            this.当前文档R.Text = "当前文档";
            this.当前文档R.UseVisualStyleBackColor = true;
            this.当前文档R.CheckedChanged += new System.EventHandler(this.当前文档R_CheckedChanged);
            // 
            // 选项G
            // 
            this.选项G.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.选项G.Controls.Add(this.区分大小写C);
            this.选项G.Controls.Add(this.到达末尾C);
            this.选项G.Location = new System.Drawing.Point(128, 119);
            this.选项G.Name = "选项G";
            this.选项G.Size = new System.Drawing.Size(192, 107);
            this.选项G.TabIndex = 5;
            this.选项G.TabStop = false;
            this.选项G.Text = "选项";
            // 
            // 区分大小写C
            // 
            this.区分大小写C.Location = new System.Drawing.Point(20, 66);
            this.区分大小写C.Name = "区分大小写C";
            this.区分大小写C.Size = new System.Drawing.Size(135, 30);
            this.区分大小写C.TabIndex = 1;
            this.区分大小写C.TabStop = false;
            this.区分大小写C.Text = "区分大小写";
            this.区分大小写C.UseVisualStyleBackColor = true;
            this.区分大小写C.CheckedChanged += new System.EventHandler(this.区分大小写C_CheckedChanged);
            // 
            // 到达末尾C
            // 
            this.到达末尾C.Location = new System.Drawing.Point(20, 24);
            this.到达末尾C.Name = "到达末尾C";
            this.到达末尾C.Size = new System.Drawing.Size(135, 30);
            this.到达末尾C.TabIndex = 0;
            this.到达末尾C.TabStop = false;
            this.到达末尾C.Text = "到达末尾重新开始";
            this.到达末尾C.UseVisualStyleBackColor = true;
            this.到达末尾C.CheckedChanged += new System.EventHandler(this.到达末尾C_CheckedChanged);
            // 
            // 查找B
            // 
            this.查找B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.查找B.Location = new System.Drawing.Point(128, 252);
            this.查找B.Name = "查找B";
            this.查找B.Size = new System.Drawing.Size(90, 35);
            this.查找B.TabIndex = 6;
            this.查找B.Text = "查找";
            this.查找B.UseVisualStyleBackColor = true;
            this.查找B.Click += new System.EventHandler(this.查找B_Click);
            // 
            // 替换B
            // 
            this.替换B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.替换B.Location = new System.Drawing.Point(230, 252);
            this.替换B.Name = "替换B";
            this.替换B.Size = new System.Drawing.Size(90, 35);
            this.替换B.TabIndex = 7;
            this.替换B.Text = "替换";
            this.替换B.UseVisualStyleBackColor = true;
            this.替换B.Click += new System.EventHandler(this.替换B_Click);
            // 
            // 全部替换B
            // 
            this.全部替换B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.全部替换B.Location = new System.Drawing.Point(128, 300);
            this.全部替换B.Name = "全部替换B";
            this.全部替换B.Size = new System.Drawing.Size(90, 35);
            this.全部替换B.TabIndex = 8;
            this.全部替换B.Text = "全部替换";
            this.全部替换B.UseVisualStyleBackColor = true;
            this.全部替换B.Click += new System.EventHandler(this.全部替换B_Click);
            // 
            // 关闭B
            // 
            this.关闭B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.关闭B.Location = new System.Drawing.Point(230, 300);
            this.关闭B.Name = "关闭B";
            this.关闭B.Size = new System.Drawing.Size(90, 35);
            this.关闭B.TabIndex = 9;
            this.关闭B.Text = "关闭";
            this.关闭B.UseVisualStyleBackColor = true;
            this.关闭B.Click += new System.EventHandler(this.关闭B_Click);
            // 
            // 查找历史B
            // 
            this.查找历史B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.查找历史B.BackColor = System.Drawing.Color.Transparent;
            this.查找历史B.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.查找历史B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.查找历史B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.查找历史B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.查找历史B.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.查找历史B.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.查找历史B.Location = new System.Drawing.Point(292, 27);
            this.查找历史B.Name = "查找历史B";
            this.查找历史B.Size = new System.Drawing.Size(25, 25);
            this.查找历史B.TabIndex = 10;
            this.查找历史B.TabStop = false;
            this.查找历史B.Text = "▾";
            this.查找历史B.UseVisualStyleBackColor = false;
            this.查找历史B.Click += new System.EventHandler(this.historyBut_Click);
            // 
            // 替换历史B
            // 
            this.替换历史B.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.替换历史B.BackColor = System.Drawing.Color.Transparent;
            this.替换历史B.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.替换历史B.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.替换历史B.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.替换历史B.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.替换历史B.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.替换历史B.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.替换历史B.Location = new System.Drawing.Point(292, 80);
            this.替换历史B.Name = "替换历史B";
            this.替换历史B.Size = new System.Drawing.Size(25, 25);
            this.替换历史B.TabIndex = 11;
            this.替换历史B.TabStop = false;
            this.替换历史B.Text = "▾";
            this.替换历史B.UseVisualStyleBackColor = false;
            this.替换历史B.Click += new System.EventHandler(this.historyBut_Click);
            // 
            // FindAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 357);
            this.Controls.Add(this.替换历史B);
            this.Controls.Add(this.查找历史B);
            this.Controls.Add(this.关闭B);
            this.Controls.Add(this.全部替换B);
            this.Controls.Add(this.替换B);
            this.Controls.Add(this.查找B);
            this.Controls.Add(this.文本范围G);
            this.Controls.Add(this.选项G);
            this.Controls.Add(this.方向G);
            this.Controls.Add(this.替换内容T);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.查找内容T);
            this.Controls.Add(this.查找内容L);
            this.MinimizeBox = false;
            this.Name = "FindAndReplace";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "查找和替换";
            this.Activated += new System.EventHandler(this.FindAndReplace_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FindAndReplace_FormClosed);
            this.Load += new System.EventHandler(this.FindAndReplace_Load);
            this.方向G.ResumeLayout(false);
            this.文本范围G.ResumeLayout(false);
            this.选项G.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 查找内容L;
        private System.Windows.Forms.TextBox 查找内容T;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox 替换内容T;
        private System.Windows.Forms.GroupBox 方向G;
        private System.Windows.Forms.GroupBox 文本范围G;
        private System.Windows.Forms.GroupBox 选项G;
        private System.Windows.Forms.Button 查找B;
        private System.Windows.Forms.Button 替换B;
        private System.Windows.Forms.Button 全部替换B;
        private System.Windows.Forms.Button 关闭B;
        private System.Windows.Forms.RadioButton 向上R;
        private System.Windows.Forms.RadioButton 向下R;
        private System.Windows.Forms.RadioButton 选定内容R;
        private System.Windows.Forms.RadioButton 当前文档R;
        private System.Windows.Forms.CheckBox 区分大小写C;
        private System.Windows.Forms.CheckBox 到达末尾C;
        private System.Windows.Forms.Button 查找历史B;
        private System.Windows.Forms.Button 替换历史B;
    }
}