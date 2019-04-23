namespace UI.ComponentLibrary.FormLibrary {
    partial class CreadJavaEntity {
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
            this.输入_编码_lab = new System.Windows.Forms.Label();
            this.输入_编码_comB = new System.Windows.Forms.ComboBox();
            this.输入_包名_textB = new System.Windows.Forms.TextBox();
            this.包名_lab = new System.Windows.Forms.Label();
            this.输入_类名_textB = new System.Windows.Forms.TextBox();
            this.类名_lab = new System.Windows.Forms.Label();
            this.输入_生成到_lab = new System.Windows.Forms.Label();
            this.选项区容器 = new System.Windows.Forms.GroupBox();
            this.结果选项容器 = new System.Windows.Forms.Panel();
            this.生成序列化_check = new System.Windows.Forms.CheckBox();
            this.生成get_set_check = new System.Windows.Forms.CheckBox();
            this.生成ToString_check = new System.Windows.Forms.CheckBox();
            this.生成注释_check = new System.Windows.Forms.CheckBox();
            this.生成深拷贝方法_check = new System.Windows.Forms.CheckBox();
            this.生成get_set注释_check = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.先set后get_red = new System.Windows.Forms.RadioButton();
            this.get_set对中_red = new System.Windows.Forms.RadioButton();
            this.先get后set_red = new System.Windows.Forms.RadioButton();
            this.构造器生成注释_check = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.只生成set_rad = new System.Windows.Forms.RadioButton();
            this.生成get_set_red = new System.Windows.Forms.RadioButton();
            this.只生成get_rad = new System.Windows.Forms.RadioButton();
            this.生成字段注释_check = new System.Windows.Forms.CheckBox();
            this.get_set规则_check = new System.Windows.Forms.CheckBox();
            this.生成构造函数_check = new System.Windows.Forms.CheckBox();
            this.全选反选_check = new System.Windows.Forms.CheckBox();
            this.操作区_容器 = new System.Windows.Forms.GroupBox();
            this.输入_类型规则_comB = new System.Windows.Forms.ComboBox();
            this.输入_类型规则_lab = new System.Windows.Forms.Label();
            this.选项区容器.SuspendLayout();
            this.结果选项容器.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.操作区_容器.SuspendLayout();
            this.SuspendLayout();
            // 
            // 输入_编码_lab
            // 
            this.输入_编码_lab.AutoSize = true;
            this.输入_编码_lab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_编码_lab.Location = new System.Drawing.Point(231, 18);
            this.输入_编码_lab.Name = "输入_编码_lab";
            this.输入_编码_lab.Size = new System.Drawing.Size(44, 17);
            this.输入_编码_lab.TabIndex = 7;
            this.输入_编码_lab.Text = "编码：";
            // 
            // 输入_编码_comB
            // 
            this.输入_编码_comB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_编码_comB.FormattingEnabled = true;
            this.输入_编码_comB.IntegralHeight = false;
            this.输入_编码_comB.ItemHeight = 17;
            this.输入_编码_comB.Location = new System.Drawing.Point(279, 14);
            this.输入_编码_comB.Name = "输入_编码_comB";
            this.输入_编码_comB.Size = new System.Drawing.Size(121, 25);
            this.输入_编码_comB.TabIndex = 6;
            this.输入_编码_comB.TabStop = false;
            this.输入_编码_comB.SelectedIndexChanged += new System.EventHandler(this.输入_编码_comB_SelectedIndexChanged);
            // 
            // 输入_包名_textB
            // 
            this.输入_包名_textB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.输入_包名_textB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_包名_textB.Location = new System.Drawing.Point(56, 45);
            this.输入_包名_textB.Name = "输入_包名_textB";
            this.输入_包名_textB.Size = new System.Drawing.Size(116, 23);
            this.输入_包名_textB.TabIndex = 4;
            this.输入_包名_textB.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // 包名_lab
            // 
            this.包名_lab.AutoSize = true;
            this.包名_lab.Location = new System.Drawing.Point(9, 51);
            this.包名_lab.Name = "包名_lab";
            this.包名_lab.Size = new System.Drawing.Size(44, 17);
            this.包名_lab.TabIndex = 5;
            this.包名_lab.Text = "包名：";
            // 
            // 输入_类名_textB
            // 
            this.输入_类名_textB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.输入_类名_textB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_类名_textB.Location = new System.Drawing.Point(56, 16);
            this.输入_类名_textB.Name = "输入_类名_textB";
            this.输入_类名_textB.Size = new System.Drawing.Size(116, 23);
            this.输入_类名_textB.TabIndex = 1;
            this.输入_类名_textB.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // 类名_lab
            // 
            this.类名_lab.AutoSize = true;
            this.类名_lab.Location = new System.Drawing.Point(9, 22);
            this.类名_lab.Name = "类名_lab";
            this.类名_lab.Size = new System.Drawing.Size(44, 17);
            this.类名_lab.TabIndex = 3;
            this.类名_lab.Text = "类名：";
            // 
            // 输入_生成到_lab
            // 
            this.输入_生成到_lab.AutoSize = true;
            this.输入_生成到_lab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_生成到_lab.Location = new System.Drawing.Point(418, 18);
            this.输入_生成到_lab.Name = "输入_生成到_lab";
            this.输入_生成到_lab.Size = new System.Drawing.Size(56, 17);
            this.输入_生成到_lab.TabIndex = 2;
            this.输入_生成到_lab.Text = "生成到：";
            // 
            // 选项区容器
            // 
            this.选项区容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.选项区容器.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.选项区容器.Controls.Add(this.结果选项容器);
            this.选项区容器.Controls.Add(this.全选反选_check);
            this.选项区容器.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.选项区容器.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.选项区容器.Location = new System.Drawing.Point(6, 7);
            this.选项区容器.Name = "选项区容器";
            this.选项区容器.Size = new System.Drawing.Size(165, 439);
            this.选项区容器.TabIndex = 10001;
            this.选项区容器.TabStop = false;
            this.选项区容器.Text = "选项区";
            // 
            // 结果选项容器
            // 
            this.结果选项容器.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.结果选项容器.Controls.Add(this.生成序列化_check);
            this.结果选项容器.Controls.Add(this.生成get_set_check);
            this.结果选项容器.Controls.Add(this.生成ToString_check);
            this.结果选项容器.Controls.Add(this.生成注释_check);
            this.结果选项容器.Controls.Add(this.生成深拷贝方法_check);
            this.结果选项容器.Controls.Add(this.生成get_set注释_check);
            this.结果选项容器.Controls.Add(this.panel2);
            this.结果选项容器.Controls.Add(this.构造器生成注释_check);
            this.结果选项容器.Controls.Add(this.panel1);
            this.结果选项容器.Controls.Add(this.生成字段注释_check);
            this.结果选项容器.Controls.Add(this.get_set规则_check);
            this.结果选项容器.Controls.Add(this.生成构造函数_check);
            this.结果选项容器.Location = new System.Drawing.Point(2, 43);
            this.结果选项容器.Name = "结果选项容器";
            this.结果选项容器.Size = new System.Drawing.Size(161, 392);
            this.结果选项容器.TabIndex = 18;
            // 
            // 生成序列化_check
            // 
            this.生成序列化_check.AutoSize = true;
            this.生成序列化_check.Location = new System.Drawing.Point(9, 365);
            this.生成序列化_check.Name = "生成序列化_check";
            this.生成序列化_check.Size = new System.Drawing.Size(87, 21);
            this.生成序列化_check.TabIndex = 17;
            this.生成序列化_check.Text = "生成序列化";
            this.生成序列化_check.UseVisualStyleBackColor = true;
            this.生成序列化_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成序列化_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成get_set_check
            // 
            this.生成get_set_check.AutoSize = true;
            this.生成get_set_check.Checked = true;
            this.生成get_set_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成get_set_check.Location = new System.Drawing.Point(9, 7);
            this.生成get_set_check.Name = "生成get_set_check";
            this.生成get_set_check.Size = new System.Drawing.Size(100, 21);
            this.生成get_set_check.TabIndex = 1;
            this.生成get_set_check.TabStop = false;
            this.生成get_set_check.Text = "生成GET/SET";
            this.生成get_set_check.UseVisualStyleBackColor = true;
            this.生成get_set_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成get_set_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成ToString_check
            // 
            this.生成ToString_check.AutoSize = true;
            this.生成ToString_check.Checked = true;
            this.生成ToString_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成ToString_check.Location = new System.Drawing.Point(9, 340);
            this.生成ToString_check.Name = "生成ToString_check";
            this.生成ToString_check.Size = new System.Drawing.Size(124, 21);
            this.生成ToString_check.TabIndex = 16;
            this.生成ToString_check.Text = "生成ToString方法";
            this.生成ToString_check.UseVisualStyleBackColor = true;
            this.生成ToString_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成ToString_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成注释_check
            // 
            this.生成注释_check.AutoSize = true;
            this.生成注释_check.Checked = true;
            this.生成注释_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成注释_check.Location = new System.Drawing.Point(9, 217);
            this.生成注释_check.Name = "生成注释_check";
            this.生成注释_check.Size = new System.Drawing.Size(75, 21);
            this.生成注释_check.TabIndex = 4;
            this.生成注释_check.Text = "生成注释";
            this.生成注释_check.UseVisualStyleBackColor = true;
            this.生成注释_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成注释_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成深拷贝方法_check
            // 
            this.生成深拷贝方法_check.AutoSize = true;
            this.生成深拷贝方法_check.Location = new System.Drawing.Point(9, 315);
            this.生成深拷贝方法_check.Name = "生成深拷贝方法_check";
            this.生成深拷贝方法_check.Size = new System.Drawing.Size(111, 21);
            this.生成深拷贝方法_check.TabIndex = 15;
            this.生成深拷贝方法_check.Text = "生成深拷贝方法";
            this.生成深拷贝方法_check.UseVisualStyleBackColor = true;
            this.生成深拷贝方法_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成深拷贝方法_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成get_set注释_check
            // 
            this.生成get_set注释_check.AutoSize = true;
            this.生成get_set注释_check.Checked = true;
            this.生成get_set注释_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成get_set注释_check.Location = new System.Drawing.Point(27, 265);
            this.生成get_set注释_check.Name = "生成get_set注释_check";
            this.生成get_set注释_check.Size = new System.Drawing.Size(124, 21);
            this.生成get_set注释_check.TabIndex = 5;
            this.生成get_set注释_check.Text = "生成GET/SET注释";
            this.生成get_set注释_check.UseVisualStyleBackColor = true;
            this.生成get_set注释_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成get_set注释_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.先set后get_red);
            this.panel2.Controls.Add(this.get_set对中_red);
            this.panel2.Controls.Add(this.先get后set_red);
            this.panel2.Location = new System.Drawing.Point(9, 119);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(140, 70);
            this.panel2.TabIndex = 14;
            // 
            // 先set后get_red
            // 
            this.先set后get_red.AutoSize = true;
            this.先set后get_red.Location = new System.Drawing.Point(18, 49);
            this.先set后get_red.Name = "先set后get_red";
            this.先set后get_red.Size = new System.Drawing.Size(94, 21);
            this.先set后get_red.TabIndex = 9;
            this.先set后get_red.Text = "先SET后GET";
            this.先set后get_red.UseVisualStyleBackColor = true;
            this.先set后get_red.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.先set后get_red.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // get_set对中_red
            // 
            this.get_set对中_red.AutoSize = true;
            this.get_set对中_red.Checked = true;
            this.get_set对中_red.Location = new System.Drawing.Point(18, 5);
            this.get_set对中_red.Name = "get_set对中_red";
            this.get_set对中_red.Size = new System.Drawing.Size(99, 21);
            this.get_set对中_red.TabIndex = 7;
            this.get_set对中_red.TabStop = true;
            this.get_set对中_red.Text = "GET/SET对中";
            this.get_set对中_red.UseVisualStyleBackColor = true;
            this.get_set对中_red.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.get_set对中_red.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // 先get后set_red
            // 
            this.先get后set_red.AutoSize = true;
            this.先get后set_red.Location = new System.Drawing.Point(18, 27);
            this.先get后set_red.Name = "先get后set_red";
            this.先get后set_red.Size = new System.Drawing.Size(94, 21);
            this.先get后set_red.TabIndex = 8;
            this.先get后set_red.Text = "先GET后SET";
            this.先get后set_red.UseVisualStyleBackColor = true;
            this.先get后set_red.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.先get后set_red.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // 构造器生成注释_check
            // 
            this.构造器生成注释_check.AutoSize = true;
            this.构造器生成注释_check.Checked = true;
            this.构造器生成注释_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.构造器生成注释_check.Location = new System.Drawing.Point(27, 289);
            this.构造器生成注释_check.Name = "构造器生成注释_check";
            this.构造器生成注释_check.Size = new System.Drawing.Size(123, 21);
            this.构造器生成注释_check.TabIndex = 6;
            this.构造器生成注释_check.Text = "生成构造函数注释";
            this.构造器生成注释_check.UseVisualStyleBackColor = true;
            this.构造器生成注释_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.构造器生成注释_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.只生成set_rad);
            this.panel1.Controls.Add(this.生成get_set_red);
            this.panel1.Controls.Add(this.只生成get_rad);
            this.panel1.Location = new System.Drawing.Point(9, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(140, 70);
            this.panel1.TabIndex = 2;
            // 
            // 只生成set_rad
            // 
            this.只生成set_rad.AutoSize = true;
            this.只生成set_rad.Location = new System.Drawing.Point(18, 49);
            this.只生成set_rad.Name = "只生成set_rad";
            this.只生成set_rad.Size = new System.Drawing.Size(83, 21);
            this.只生成set_rad.TabIndex = 2;
            this.只生成set_rad.Text = "只生成SET";
            this.只生成set_rad.UseVisualStyleBackColor = true;
            this.只生成set_rad.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.只生成set_rad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // 生成get_set_red
            // 
            this.生成get_set_red.AutoSize = true;
            this.生成get_set_red.Checked = true;
            this.生成get_set_red.Location = new System.Drawing.Point(18, 5);
            this.生成get_set_red.Name = "生成get_set_red";
            this.生成get_set_red.Size = new System.Drawing.Size(99, 21);
            this.生成get_set_red.TabIndex = 0;
            this.生成get_set_red.TabStop = true;
            this.生成get_set_red.Text = "生成GET/SET";
            this.生成get_set_red.UseVisualStyleBackColor = true;
            this.生成get_set_red.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.生成get_set_red.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // 只生成get_rad
            // 
            this.只生成get_rad.AutoSize = true;
            this.只生成get_rad.Location = new System.Drawing.Point(18, 27);
            this.只生成get_rad.Name = "只生成get_rad";
            this.只生成get_rad.Size = new System.Drawing.Size(85, 21);
            this.只生成get_rad.TabIndex = 1;
            this.只生成get_rad.Text = "只生成GET";
            this.只生成get_rad.UseVisualStyleBackColor = true;
            this.只生成get_rad.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            this.只生成get_rad.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RadioButton_MouseDown);
            // 
            // 生成字段注释_check
            // 
            this.生成字段注释_check.AutoSize = true;
            this.生成字段注释_check.Checked = true;
            this.生成字段注释_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成字段注释_check.Location = new System.Drawing.Point(27, 240);
            this.生成字段注释_check.Name = "生成字段注释_check";
            this.生成字段注释_check.Size = new System.Drawing.Size(99, 21);
            this.生成字段注释_check.TabIndex = 10;
            this.生成字段注释_check.Text = "生成字段注释";
            this.生成字段注释_check.UseVisualStyleBackColor = true;
            this.生成字段注释_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成字段注释_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // get_set规则_check
            // 
            this.get_set规则_check.AutoCheck = false;
            this.get_set规则_check.AutoSize = true;
            this.get_set规则_check.Checked = true;
            this.get_set规则_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.get_set规则_check.Location = new System.Drawing.Point(9, 101);
            this.get_set规则_check.Name = "get_set规则_check";
            this.get_set规则_check.Size = new System.Drawing.Size(124, 21);
            this.get_set规则_check.TabIndex = 12;
            this.get_set规则_check.TabStop = false;
            this.get_set规则_check.Text = "GET/SET生成规则";
            this.get_set规则_check.UseVisualStyleBackColor = true;
            this.get_set规则_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.get_set规则_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成构造函数_check
            // 
            this.生成构造函数_check.AutoSize = true;
            this.生成构造函数_check.Checked = true;
            this.生成构造函数_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成构造函数_check.Location = new System.Drawing.Point(9, 194);
            this.生成构造函数_check.Name = "生成构造函数_check";
            this.生成构造函数_check.Size = new System.Drawing.Size(99, 21);
            this.生成构造函数_check.TabIndex = 11;
            this.生成构造函数_check.Text = "生成构造函数";
            this.生成构造函数_check.UseVisualStyleBackColor = true;
            this.生成构造函数_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成构造函数_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 全选反选_check
            // 
            this.全选反选_check.AutoSize = true;
            this.全选反选_check.BackColor = System.Drawing.Color.White;
            this.全选反选_check.Checked = true;
            this.全选反选_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.全选反选_check.ForeColor = System.Drawing.Color.Tomato;
            this.全选反选_check.Location = new System.Drawing.Point(9, 25);
            this.全选反选_check.Name = "全选反选_check";
            this.全选反选_check.Size = new System.Drawing.Size(80, 21);
            this.全选反选_check.TabIndex = 18;
            this.全选反选_check.Text = "全选/反选";
            this.全选反选_check.UseVisualStyleBackColor = false;
            this.全选反选_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.全选反选_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 操作区_容器
            // 
            this.操作区_容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.操作区_容器.Controls.Add(this.输入_类型规则_comB);
            this.操作区_容器.Controls.Add(this.输入_编码_comB);
            this.操作区_容器.Controls.Add(this.输入_类名_textB);
            this.操作区_容器.Controls.Add(this.输入_包名_textB);
            this.操作区_容器.Controls.Add(this.输入_类型规则_lab);
            this.操作区_容器.Controls.Add(this.输入_编码_lab);
            this.操作区_容器.Controls.Add(this.输入_生成到_lab);
            this.操作区_容器.Controls.Add(this.包名_lab);
            this.操作区_容器.Controls.Add(this.类名_lab);
            this.操作区_容器.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.操作区_容器.Location = new System.Drawing.Point(177, 7);
            this.操作区_容器.Name = "操作区_容器";
            this.操作区_容器.Size = new System.Drawing.Size(612, 74);
            this.操作区_容器.TabIndex = 10002;
            this.操作区_容器.TabStop = false;
            this.操作区_容器.Text = "操作区";
            // 
            // 输入_类型规则_comB
            // 
            this.输入_类型规则_comB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.输入_类型规则_comB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.输入_类型规则_comB.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.输入_类型规则_comB.FormattingEnabled = true;
            this.输入_类型规则_comB.IntegralHeight = false;
            this.输入_类型规则_comB.ItemHeight = 17;
            this.输入_类型规则_comB.Items.AddRange(new object[] {
            "数据库",
            "JAVA对象"});
            this.输入_类型规则_comB.Location = new System.Drawing.Point(279, 43);
            this.输入_类型规则_comB.Name = "输入_类型规则_comB";
            this.输入_类型规则_comB.Size = new System.Drawing.Size(121, 25);
            this.输入_类型规则_comB.TabIndex = 8;
            this.输入_类型规则_comB.TabStop = false;
            this.输入_类型规则_comB.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            // 
            // 输入_类型规则_lab
            // 
            this.输入_类型规则_lab.AutoSize = true;
            this.输入_类型规则_lab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_类型规则_lab.Location = new System.Drawing.Point(184, 47);
            this.输入_类型规则_lab.Name = "输入_类型规则_lab";
            this.输入_类型规则_lab.Size = new System.Drawing.Size(92, 17);
            this.输入_类型规则_lab.TabIndex = 9;
            this.输入_类型规则_lab.Text = "类型匹配规则：";
            // 
            // CreadJavaEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 453);
            this.Controls.Add(this.操作区_容器);
            this.Controls.Add(this.选项区容器);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(812, 492);
            this.Name = "CreadJavaEntity";
            this.ShowInTaskbar = false;
            this.Text = "生成JAVA实体类";
            this.Load += new System.EventHandler(this.CreadJavaEntity_Load);
            this.选项区容器.ResumeLayout(false);
            this.选项区容器.PerformLayout();
            this.结果选项容器.ResumeLayout(false);
            this.结果选项容器.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.操作区_容器.ResumeLayout(false);
            this.操作区_容器.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox 选项区容器;
        private System.Windows.Forms.RadioButton 只生成set_rad;
        private System.Windows.Forms.RadioButton 只生成get_rad;
        private System.Windows.Forms.RadioButton 生成get_set_red;
        private System.Windows.Forms.CheckBox 生成get_set_check;
        private System.Windows.Forms.CheckBox 生成注释_check;
        private System.Windows.Forms.CheckBox 生成get_set注释_check;
        private System.Windows.Forms.CheckBox 构造器生成注释_check;
        private System.Windows.Forms.RadioButton 先get后set_red;
        private System.Windows.Forms.RadioButton get_set对中_red;
        private System.Windows.Forms.RadioButton 先set后get_red;
        private System.Windows.Forms.CheckBox 生成字段注释_check;
        private System.Windows.Forms.CheckBox 生成构造函数_check;
        private System.Windows.Forms.CheckBox get_set规则_check;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox 生成深拷贝方法_check;
        private System.Windows.Forms.Label 输入_生成到_lab;
        private System.Windows.Forms.Label 类名_lab;
        private System.Windows.Forms.TextBox 输入_类名_textB;
        private System.Windows.Forms.CheckBox 生成ToString_check;
        private System.Windows.Forms.CheckBox 生成序列化_check;
        private System.Windows.Forms.Panel 结果选项容器;
        private System.Windows.Forms.CheckBox 全选反选_check;
        private System.Windows.Forms.TextBox 输入_包名_textB;
        private System.Windows.Forms.Label 包名_lab;
        private System.Windows.Forms.Label 输入_编码_lab;
        private System.Windows.Forms.ComboBox 输入_编码_comB;
        private System.Windows.Forms.GroupBox 操作区_容器;
        private System.Windows.Forms.ComboBox 输入_类型规则_comB;
        private System.Windows.Forms.Label 输入_类型规则_lab;
    }
}