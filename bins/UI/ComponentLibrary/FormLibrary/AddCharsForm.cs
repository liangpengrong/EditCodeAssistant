using Core.CacheLibrary.FormCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary;
using UI.ComponentLibrary.ControlLibrary.RightMenu;
using UI.ComponentLibrary.MethodLibrary.Interface;
using UI.ComponentLibrary.MethodLibrary.Util;

namespace UI.ComponentLibrary.FormLibrary {
    /// <summary>
    /// 添加字符窗体
    /// </summary>
    public partial class AddCharsForm : Form, IComponentInitMode<Form> {
        // 显示结果的文本框
        private RedrawTextBox resultTextBox;
        
        // 要操作的字符串
        private string text_val = "";
        
        // 当前要操作的模式(0普通 1高级)
        private int type = 0;

        // 添加字符文本框的历史
        private Dictionary<int, string[]> textHistory = new Dictionary<int, string[]>();

        // 要添加到行首的字符
        private string head_text = "";

        // 要添加到行尾部的字符
        private string end_text = "";

        // 匹配空行
        private bool isMatchBlack = true;

        // 匹配末尾
        private bool isMatchEnd = true;

        internal AddCharsForm() {
            initFormDefConfig();
            InitializeComponent();
            initResultTextBox();
        }
        /// <summary>
        /// 打开单例模式下的添加字符窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initSingleExample(bool isShowTop) {
            AddCharsForm addChars = null;
            Form form = FormCacheFactory.getSingletonCache(DefaultNameEnum.ADD_CHARS_FORM);
            if(form == null || form.IsDisposed || !(form is AddCharsForm)) { 
                addChars = this;
                addChars.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.ADD_CHARS_FORM);
                addChars = FormCacheFactory.ininSingletonForm(addChars, false);
            } else {
                addChars = (AddCharsForm)form;
                addChars.Activate();
            }
            if(isShowTop) FormCacheFactory.addTopFormCache(addChars);
            addChars.Visible = false;
            return addChars;
        }
        /// <summary>
        /// 打开多例模式下的添加字符窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Form initPrototypeExample(bool isShowTop) {
            AddCharsForm addChars = this;
            addChars.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.ADD_CHARS_FORM)+DateTime.Now.Ticks.ToString();;
            // 加入到顶层窗体集合
            if(isShowTop) FormCacheFactory.addTopFormCache(addChars);
            // 加入到多例工厂
            FormCacheFactory.addPrototypeCache(DefaultNameEnum.ADD_CHARS_FORM, addChars);
            addChars.Activate();
            addChars.Visible = false;
            return addChars;
        }
        // 初始化窗体默认配置
        private void initFormDefConfig() { 
            this.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.ADD_CHARS_FORM);    
        }
        /// <summary>
        /// 初始化结果文本框
        /// </summary>
        private void initResultTextBox() { 
            Control parent = 普通_操作容器.Parent;
            if(parent != null) { 
                resultTextBox = new RedrawTextBox();
                resultTextBox.Location = new Point(1, 普通_操作容器.Location.Y + 普通_操作容器.Height);
                resultTextBox.Size = new Size(parent.ClientSize.Width-2, (parent.Height-普通_操作容器.Height));
                resultTextBox.ReadOnly = false;
                parent.Controls.Add(resultTextBox);
                resultTextBox.BringToFront();
            }
        }
        /// <summary>
        /// 添加字符的执行方法
        /// </summary>
        private void ordinaryAddChars() {
            // 按照换行符分割
            string[] strArr = StringUtilsMet.SplitStrToArray(text_val
                ,new string[]{ Environment.NewLine}, true, false);
            StringBuilder stringBuilder = new StringBuilder();
            // 遍历数组并添加字符
            foreach(string s in strArr) {
                // 判断是否匹配空行
                if(isMatchBlack || (!isMatchBlack && s.Length > 0)) {
                    stringBuilder.Append(head_text + s + end_text).AppendLine();
                } else { 
                    stringBuilder.AppendLine();
                }
            }
            string textVal = stringBuilder.ToString();
            // 去除最后一个换行符
            textVal = textVal.Substring(0, textVal.Length - Environment.NewLine.Length);
            // 判断是否不匹配末尾
            if (!isMatchEnd) {
                int noneLength = 0;
                // 匹配空行
                if (isMatchBlack) {
                    noneLength = 0;
                } else {
                    for (int i = strArr.Length; i>0; i--) {
                        if (strArr[i-1].Length == 0) {
                            noneLength = noneLength + Environment.NewLine.Length;
                        } else { 
                            break;    
                        }   
                    }
                }
                textVal = textVal.Substring(0, textVal.Length - (end_text.Length+noneLength));
            }
            int start = resultTextBox.SelectionStart;
            resultTextBox.Text = textVal;
            resultTextBox.SelectionStart = start;
            resultTextBox.ScrollToCaret();
        }
        /// <summary>
        /// 添加字符的总执行方法
        /// </summary>
        private void addCharsMet() {
            // 获取当前标签的文本框
            TextBox textBox = ControlsUtilsMet.GetSelectPageTextBox();
            textBox = textBox != null? textBox : new TextBox();
            // 普通模式
            if(0.Equals(type)) {
                text_val = textBox.SelectionLength > 0?textBox.SelectedText : textBox.Text;
                // 添加字符
                ordinaryAddChars();
                // 将首尾字符添加到历史纪录中
                addTextHistory();
            }
        }
        /// <summary>
        /// 导出数据到新标签
        /// </summary>
        public void exportNewPage() { 
            string s = resultTextBox.SelectionLength == 0? resultTextBox.Text : resultTextBox.SelectedText;
            MainTabControlUtils.ExportNewPage(s);
        }
        /// <summary>
        /// 导出数据到当前标签
        /// </summary>
        public void exportThisPage() {
            string s = resultTextBox.SelectionLength == 0? resultTextBox.Text : resultTextBox.SelectedText;
            ControlsUtilsMet.ExportThisPage(s);
        }
        /// <summary>
        /// 导出数据到记事本
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="excNoHaveTabs">不包含tab符号</param>
        public void exportNotepad() {
            if(resultTextBox.SelectionLength > 0) { 
                FileUtilsMet.TurnOnNotepad(resultTextBox.SelectedText);
            } else { 
                FileUtilsMet.TurnOnNotepad(resultTextBox.Text);
            }
            
        }
        /// <summary>
        /// 初始化消息提示
        /// </summary>
        private void initToolTip() { 
            string mess1 = "在每行的开头处添加字符";
            RedrawPromptMessBut but1 = new RedrawPromptMessBut();
            but1.ButtonMess = mess1;
            but1.Location = new Point(普通_行首text.Location.X - but1.Width - 3, 普通_行首text.Location.Y+2);
            普通_行首text.Parent.Controls.Add(but1);
        }
        /// <summary>
        /// 初始化导出按钮
        /// </summary>
        private void initExportCombox() {
            // 实例化导出下拉框
            ComboBox comboBox = new ExportComBox(new ExportComBoxValEnum[]{ExportComBoxValEnum.EXPORT_EXCEL_VAL
                ,ExportComBoxValEnum.EXPORT_JAVA_VAL});;
            // 绑定点击事件
            comboBox.SelectedIndexChanged += (object sender, EventArgs e) =>{
                ComboBox box = (ComboBox)sender;
                ExportComBoxValEnum val = ExportComBox.stringToEnum(box.SelectedValue.ToString());
                switch(val) {
                    case ExportComBoxValEnum.EXPORT_NEW_PAGE_VAL: // 导出到新标签
                        exportNewPage();
                    break;
                    case ExportComBoxValEnum.EXPORT_THIS_PAGE_VAL: // 导出到当前标签
                        exportThisPage();
                    break;
                    case ExportComBoxValEnum.EXPORT_NOTEBOOK_VAL: // 导出到记事本
                        exportNotepad();
                    break;
                }
            };
            // 加入到容器中
            comboBox.Location = new Point(普通_操作容器.Width - comboBox.Width - 10
                , 普通_确定添加but.Location.Y - 1);
            comboBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            普通_操作容器.Controls.Add(comboBox);
        }
        /// <summary>
        /// 将文本内容追加到历史纪录中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        private void addTextHistory() {
            // 判断是否存在行头的key
            if(textHistory.ContainsKey(0)) { 
                List<string> strList = textHistory[0].ToList();
                // 确定list中不存在该元素
                if( !strList.Contains(head_text) && head_text.Length > 0) { 
                    strList.Add(head_text);
                    textHistory[0] = strList.ToArray();
                }
            } else {
                if(head_text.Length > 0) { 
                    textHistory.Add(0, new string[]{head_text});
                }
            }
            // 判断是否存在行头的key
            if(textHistory.ContainsKey(1)) { 
                List<string> strList = textHistory[1].ToList();
                // 确定list中不存在该元素
                if( !strList.Contains(end_text) && end_text.Length > 0) { 
                    strList.Add(end_text);
                    textHistory[1] = strList.ToArray();
                }
            } else {
                if(end_text.Length > 0) { 
                    textHistory.Add(1, new string[]{end_text});
                }
            }
        }
        /// <summary>
        /// 根据点击的历史记录按钮生成文本框的历史记录
        /// </summary>
        /// <param name="butName"></param>
        private void textHistoryRecord(string butName) {
            if (普通_行首历史but.Name.Equals(butName)) {
                if (textHistory.ContainsKey(0) && textHistory[0].Length > 0) {
                    Panel p = ControlsUtilsMet.GetHistoricalPanel(普通_行首text
                        , 普通_行首历史but.FindForm().Controls
                        , true
                        , textHistory[0]
                        , 普通_行首text.Width + 普通_行首历史but.Width
                        , 18);
                    p.Location = new Point(tab容器.Location.X + 普通_行首text.Location.X
                        , tab容器.Location.Y + tab容器.ItemSize.Height + 普通_行首text.Height + 12);
                }
            }
            if (普通_行尾历史but.Name.Equals(butName)) {
                if (textHistory.ContainsKey(1) && textHistory[1].Length > 0) {
                    Panel p = ControlsUtilsMet.GetHistoricalPanel(普通_行尾text
                        , 普通_行尾历史but.FindForm().Controls
                        , true
                        , textHistory[1]
                        , 普通_行尾text.Width + 普通_行尾历史but.Width
                        , 18);
                    p.Location = new Point(tab容器.Location.X + 普通_行尾text.Location.X
                        , tab容器.Location.Y + tab容器.ItemSize.Height + 普通_行尾text.Height + 12);
                }

            }
        }

        // 窗体加载事件
        private void AddCharsForm_Load(object sender, EventArgs e) {
            // 读取窗体默认配置
            formDefConfig();
            // 初始化消息提示控件
            // initToolTip();
            // 初始化导出按钮
            initExportCombox();
            // 行首文本框获取焦点
            普通_行首text.Focus();
        }
        /// <summary>
        /// 窗体默认配置
        /// </summary>
        private void formDefConfig() { 
            // 设置图标
            this.AutoScaleMode = AutoScaleMode.None;
            // 调节窗口位置
            this.Location = FormUtislMet.MiddleForm(this);
        }
        // 操作区容器重绘事件
        private void 普通_操作容器_Paint(object sender, PaintEventArgs e) {
            Panel panel = (Panel)sender;
            ControlsUtilsMet.SetControlBorderStyle(e.Graphics, panel.ClientRectangle
                ,ButtonBorderStyle.Solid
                ,0,0,0,1
                , ColorTranslator.FromHtml("#D9D9D9"));
        }
        // 复选框选项改变事件
        private void checkBox_CheckedChanged(object sender, EventArgs e) {
            CheckBox chk = (CheckBox)sender;
            // 
            if(this.不匹配空行_chk.Equals(chk)) { 
                isMatchBlack = !chk.Checked;
            }
            // 
            if(this.不匹配末尾_chk.Equals(chk)) {
                isMatchEnd = !chk.Checked;
            }
            // 
            if(this.文本框自动换行_chk.Equals(chk)) { 
                this.resultTextBox.WordWrap = chk.Checked;
            }
        }
        // 确定按钮的点击事件
        private void 普通_确定添加but_Click(object sender, EventArgs e) {
            // 执行添加方法
            addCharsMet();
        }
        // 文本框键盘按下事件
        private void Textbox_KeyDown(object sender, KeyEventArgs e) {
            TextBox t = (TextBox)sender;
            // 全选
            if(e.Control && Keys.A.Equals(e.KeyCode)) { 
                t.SelectAll();
            }
        }
        // 选项卡选项id改变事件
        private void tab容器_SelectedIndexChanged(object sender, EventArgs e) {
            TabControl tab = (TabControl)sender;
            switch(tab.SelectedIndex){
                case 0:
                    type = 0;
                    文本框自动换行_chk.Enabled = true;
                    break; 
                case 1:
                    type = 1;
                    文本框自动换行_chk.Enabled = false;
                    break;
            } 
            type = tab.SelectedIndex;
        }
        // 历史记录的点击事件
        private void 历史but_Click(object sender, EventArgs e) {
            Button but = (Button)sender;
            textHistoryRecord(but.Name);
        }
        // 文本框鼠标移入事件
        private void Textbox_MouseEnter(object sender, EventArgs e) {
            TextBox textBox = (TextBox)sender;
            // 绑定右键菜单
            Control ccc = UIComponentFactory.getSingleControl(DefaultNameEnum.TEXT_RIGHT_MENU, true);
        }

        private void TextBox_TextChanged(object sender, EventArgs e) {
            TextBox textBox = (TextBox)sender;
            if(普通_行首text.Equals(textBox)) { 
                head_text = textBox.Text;
            }else if (普通_行尾text.Equals(textBox)) { 
                end_text = textBox.Text;
            }
        }
    }
}
