namespace UI.ComponentLibrary.FormLibrary {
    partial class FieldToJavaEntity {
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
            this.tab容器 = new System.Windows.Forms.TabControl();
            this.输入_page = new System.Windows.Forms.TabPage();
            this.输入_包名_textB = new System.Windows.Forms.TextBox();
            this.包名_lab = new System.Windows.Forms.Label();
            this.输入_类名_textB = new System.Windows.Forms.TextBox();
            this.类名_lab = new System.Windows.Forms.Label();
            this.输入_生成到_lab = new System.Windows.Forms.Label();
            this.输入_生成到_comB = new System.Windows.Forms.ComboBox();
            this.连接数据库_page = new System.Windows.Forms.TabPage();
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
            this.输入_编码_lab = new System.Windows.Forms.Label();
            this.输入_编码_comB = new System.Windows.Forms.ComboBox();
            this.tab容器.SuspendLayout();
            this.输入_page.SuspendLayout();
            this.选项区容器.SuspendLayout();
            this.结果选项容器.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab容器
            // 
            this.tab容器.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab容器.Controls.Add(this.输入_page);
            this.tab容器.Controls.Add(this.连接数据库_page);
            this.tab容器.HotTrack = true;
            this.tab容器.ItemSize = new System.Drawing.Size(80, 22);
            this.tab容器.Location = new System.Drawing.Point(171, 6);
            this.tab容器.Margin = new System.Windows.Forms.Padding(0);
            this.tab容器.Name = "tab容器";
            this.tab容器.SelectedIndex = 0;
            this.tab容器.Size = new System.Drawing.Size(743, 447);
            this.tab容器.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tab容器.TabIndex = 10000;
            this.tab容器.TabStop = false;
            this.tab容器.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tab容器_Selecting);
            // 
            // 输入_page
            // 
            this.输入_page.Controls.Add(this.输入_编码_lab);
            this.输入_page.Controls.Add(this.输入_编码_comB);
            this.输入_page.Controls.Add(this.输入_包名_textB);
            this.输入_page.Controls.Add(this.包名_lab);
            this.输入_page.Controls.Add(this.输入_类名_textB);
            this.输入_page.Controls.Add(this.类名_lab);
            this.输入_page.Controls.Add(this.输入_生成到_lab);
            this.输入_page.Controls.Add(this.输入_生成到_comB);
            this.输入_page.Location = new System.Drawing.Point(4, 26);
            this.输入_page.Margin = new System.Windows.Forms.Padding(0);
            this.输入_page.Name = "输入_page";
            this.输入_page.Size = new System.Drawing.Size(735, 417);
            this.输入_page.TabIndex = 0;
            this.输入_page.Text = "手动输入";
            this.输入_page.ToolTipText = "通过输入字段生成实体类";
            this.输入_page.UseVisualStyleBackColor = true;
            // 
            // 输入_包名_textB
            // 
            this.输入_包名_textB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.输入_包名_textB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_包名_textB.Location = new System.Drawing.Point(226, 8);
            this.输入_包名_textB.Name = "输入_包名_textB";
            this.输入_包名_textB.Size = new System.Drawing.Size(116, 23);
            this.输入_包名_textB.TabIndex = 4;
            this.输入_包名_textB.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // 包名_lab
            // 
            this.包名_lab.AutoSize = true;
            this.包名_lab.Location = new System.Drawing.Point(181, 14);
            this.包名_lab.Name = "包名_lab";
            this.包名_lab.Size = new System.Drawing.Size(41, 12);
            this.包名_lab.TabIndex = 5;
            this.包名_lab.Text = "包名：";
            // 
            // 输入_类名_textB
            // 
            this.输入_类名_textB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.输入_类名_textB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_类名_textB.Location = new System.Drawing.Point(59, 7);
            this.输入_类名_textB.Name = "输入_类名_textB";
            this.输入_类名_textB.Size = new System.Drawing.Size(116, 23);
            this.输入_类名_textB.TabIndex = 1;
            this.输入_类名_textB.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // 类名_lab
            // 
            this.类名_lab.AutoSize = true;
            this.类名_lab.Location = new System.Drawing.Point(16, 13);
            this.类名_lab.Name = "类名_lab";
            this.类名_lab.Size = new System.Drawing.Size(41, 12);
            this.类名_lab.TabIndex = 3;
            this.类名_lab.Text = "类名：";
            // 
            // 输入_生成到_lab
            // 
            this.输入_生成到_lab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.输入_生成到_lab.AutoSize = true;
            this.输入_生成到_lab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_生成到_lab.Location = new System.Drawing.Point(546, 10);
            this.输入_生成到_lab.Name = "输入_生成到_lab";
            this.输入_生成到_lab.Size = new System.Drawing.Size(56, 17);
            this.输入_生成到_lab.TabIndex = 2;
            this.输入_生成到_lab.Text = "生成到：";
            // 
            // 输入_生成到_comB
            // 
            this.输入_生成到_comB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.输入_生成到_comB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.输入_生成到_comB.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.输入_生成到_comB.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_生成到_comB.FormattingEnabled = true;
            this.输入_生成到_comB.IntegralHeight = false;
            this.输入_生成到_comB.ItemHeight = 20;
            this.输入_生成到_comB.Items.AddRange(new object[] {
            "选定文本框",
            "JAVA文件",
            "记事本"});
            this.输入_生成到_comB.Location = new System.Drawing.Point(605, 6);
            this.输入_生成到_comB.Name = "输入_生成到_comB";
            this.输入_生成到_comB.Size = new System.Drawing.Size(121, 28);
            this.输入_生成到_comB.TabIndex = 1;
            this.输入_生成到_comB.TabStop = false;
            this.输入_生成到_comB.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SelectedIndexChanged);
            this.输入_生成到_comB.MouseEnter += new System.EventHandler(this.ComboBox_MouseEnter);
            // 
            // 连接数据库_page
            // 
            this.连接数据库_page.Location = new System.Drawing.Point(4, 26);
            this.连接数据库_page.Name = "连接数据库_page";
            this.连接数据库_page.Padding = new System.Windows.Forms.Padding(3);
            this.连接数据库_page.Size = new System.Drawing.Size(642, 417);
            this.连接数据库_page.TabIndex = 1;
            this.连接数据库_page.Text = "连接数据库";
            this.连接数据库_page.ToolTipText = "通过连接数据获取字段生成实体类";
            this.连接数据库_page.UseVisualStyleBackColor = true;
            // 
            // 选项区容器
            // 
            this.选项区容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.选项区容器.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.选项区容器.Controls.Add(this.结果选项容器);
            this.选项区容器.Controls.Add(this.全选反选_check);
            this.选项区容器.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.选项区容器.Location = new System.Drawing.Point(6, 7);
            this.选项区容器.Name = "选项区容器";
            this.选项区容器.Size = new System.Drawing.Size(158, 446);
            this.选项区容器.TabIndex = 10001;
            this.选项区容器.TabStop = false;
            this.选项区容器.Text = "选项区";
            // 
            // 结果选项容器
            // 
            this.结果选项容器.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
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
            this.结果选项容器.Size = new System.Drawing.Size(154, 399);
            this.结果选项容器.TabIndex = 18;
            // 
            // 生成序列化_check
            // 
            this.生成序列化_check.AutoSize = true;
            this.生成序列化_check.Location = new System.Drawing.Point(9, 365);
            this.生成序列化_check.Name = "生成序列化_check";
            this.生成序列化_check.Size = new System.Drawing.Size(84, 16);
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
            this.生成get_set_check.Size = new System.Drawing.Size(90, 16);
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
            this.生成ToString_check.Size = new System.Drawing.Size(120, 16);
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
            this.生成注释_check.Size = new System.Drawing.Size(72, 16);
            this.生成注释_check.TabIndex = 4;
            this.生成注释_check.Text = "生成注释";
            this.生成注释_check.UseVisualStyleBackColor = true;
            this.生成注释_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.生成注释_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 生成深拷贝方法_check
            // 
            this.生成深拷贝方法_check.AutoSize = true;
            this.生成深拷贝方法_check.Checked = true;
            this.生成深拷贝方法_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.生成深拷贝方法_check.Location = new System.Drawing.Point(9, 315);
            this.生成深拷贝方法_check.Name = "生成深拷贝方法_check";
            this.生成深拷贝方法_check.Size = new System.Drawing.Size(108, 16);
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
            this.生成get_set注释_check.Size = new System.Drawing.Size(114, 16);
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
            this.先set后get_red.Size = new System.Drawing.Size(83, 16);
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
            this.get_set对中_red.Size = new System.Drawing.Size(89, 16);
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
            this.先get后set_red.Size = new System.Drawing.Size(83, 16);
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
            this.构造器生成注释_check.Size = new System.Drawing.Size(120, 16);
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
            this.只生成set_rad.Size = new System.Drawing.Size(77, 16);
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
            this.生成get_set_red.Size = new System.Drawing.Size(89, 16);
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
            this.只生成get_rad.Size = new System.Drawing.Size(77, 16);
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
            this.生成字段注释_check.Size = new System.Drawing.Size(96, 16);
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
            this.get_set规则_check.Size = new System.Drawing.Size(114, 16);
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
            this.生成构造函数_check.Size = new System.Drawing.Size(96, 16);
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
            this.全选反选_check.Size = new System.Drawing.Size(78, 16);
            this.全选反选_check.TabIndex = 18;
            this.全选反选_check.Text = "全选/反选";
            this.全选反选_check.UseVisualStyleBackColor = false;
            this.全选反选_check.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            this.全选反选_check.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CheckBox_MouseDown);
            // 
            // 输入_编码_lab
            // 
            this.输入_编码_lab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.输入_编码_lab.AutoSize = true;
            this.输入_编码_lab.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_编码_lab.Location = new System.Drawing.Point(368, 11);
            this.输入_编码_lab.Name = "输入_编码_lab";
            this.输入_编码_lab.Size = new System.Drawing.Size(44, 17);
            this.输入_编码_lab.TabIndex = 7;
            this.输入_编码_lab.Text = "编码：";
            // 
            // 输入_编码_comB
            // 
            this.输入_编码_comB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.输入_编码_comB.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.输入_编码_comB.FormattingEnabled = true;
            this.输入_编码_comB.IntegralHeight = false;
            this.输入_编码_comB.ItemHeight = 17;
            this.输入_编码_comB.Location = new System.Drawing.Point(414, 7);
            this.输入_编码_comB.Name = "输入_编码_comB";
            this.输入_编码_comB.Size = new System.Drawing.Size(121, 25);
            this.输入_编码_comB.TabIndex = 6;
            this.输入_编码_comB.TabStop = false;
            this.输入_编码_comB.SelectedIndexChanged += new System.EventHandler(this.输入_编码_comB_SelectedIndexChanged);
            // 
            // FieldToJavaEntity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 460);
            this.Controls.Add(this.tab容器);
            this.Controls.Add(this.选项区容器);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "FieldToJavaEntity";
            this.ShowInTaskbar = false;
            this.Text = "将字段转化为JAVA实体类";
            this.Load += new System.EventHandler(this.FieldToJavaEntity_Load);
            this.tab容器.ResumeLayout(false);
            this.输入_page.ResumeLayout(false);
            this.输入_page.PerformLayout();
            this.选项区容器.ResumeLayout(false);
            this.选项区容器.PerformLayout();
            this.结果选项容器.ResumeLayout(false);
            this.结果选项容器.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage 输入_page;
        private System.Windows.Forms.TabPage 连接数据库_page;
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
        private System.Windows.Forms.ComboBox 输入_生成到_comB;
        private System.Windows.Forms.Label 输入_生成到_lab;
        private System.Windows.Forms.Label 类名_lab;
        private System.Windows.Forms.TextBox 输入_类名_textB;
        private System.Windows.Forms.CheckBox 生成ToString_check;
        public System.Windows.Forms.TabControl tab容器;
        private System.Windows.Forms.CheckBox 生成序列化_check;
        private System.Windows.Forms.Panel 结果选项容器;
        private System.Windows.Forms.CheckBox 全选反选_check;
        private System.Windows.Forms.TextBox 输入_包名_textB;
        private System.Windows.Forms.Label 包名_lab;
        private System.Windows.Forms.Label 输入_编码_lab;
        private System.Windows.Forms.ComboBox 输入_编码_comB;
    }
}