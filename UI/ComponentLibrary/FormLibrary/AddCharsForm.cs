using Core.CacheLibrary.FormCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using ProgramTextBoxLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary;
using UI.ComponentLibrary.ControlLibrary.RightMenu;

namespace UI.ComponentLibrary.FormLibrary {
    /// <summary>
    /// 添加字符窗体
    /// </summary>
    public partial class AddCharsForm : Form {
        // 显示结果的文本框
        private TextBox resultTextBox;
        
        // 要操作的文本框
        private TextBox textBox;
        
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

        public AddCharsForm(TextBox textBox) {
            InitializeComponent();
            this.textBox = textBox;
            initResultTextBox();
            // if(textBox != null) 普通_textbox.Font = textBox.Font;
        }
        /// <summary>
        /// 打开添加字符窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static AddCharsForm initSingleAddCharsForm(TextBox t) {
            AddCharsForm addChars = null;
            Form form = FormCache.getSingletonCache(DefaultNameCof.ADD_CHARS_FORM);
            if(form == null || !(form is SplitCharsForm)) { 
                addChars = new AddCharsForm(t);
                addChars.Name = DefaultNameCof.SPLIT_CHARS_FORM;
                addChars = FormCache.ininSingletonForm(addChars, true);
            } else {
                addChars = (AddCharsForm)form;
            }
            return addChars;
        }
        /// <summary>
        /// 验证
        /// </summary>
        private bool isCheck() {
            if(textBox == null) { 
                MessageBox.Show("源文本框不能为null");
                return false;
            }
            //if(textBox.TextLength == 0) { 
            //    MessageBox.Show("源文本框文本不能为空");
            //    return false;
            //}
            return true;
        }
        /// <summary>
        /// 初始化结果文本框
        /// </summary>
        private void initResultTextBox() { 
            Control parent = 普通_操作容器.Parent;
            if(parent != null) { 
                resultTextBox = CacheTextBoxTemplate.getCacheTextBox(); 
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
            // 验证
            if(!isCheck()) return;
            // 按照换行符分割
            string[] strArr = StringUtilsMet.splitStrToArr(text_val
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
                textVal = textVal.Substring(0, textVal.Length - end_text.Length);
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
            // 普通模式
            if(0.Equals(type)) {
                ordinaryAddChars();
                // 将首尾字符添加到历史纪录中
                addTextHistory();
            }
        }
        /// <summary>
        /// 普通模式下的数据初始化
        /// </summary>
        private void ordinaryInitData() {
            head_text = 普通_行首text.Text;
            end_text = 普通_行尾text.Text;
            if(textBox != null) {
                if(textBox.SelectionLength == 0) { 
                    text_val = textBox.Text;
                } else { 
                    text_val = textBox.SelectedText;    
                }
            } 
        }

        /// <summary>
        /// 初始化消息提示
        /// </summary>
        private void initToolTip() { 
            string mess1 = "在每行的开头处添加字符";
            Button but1 = PromptMessage.getMessBut(普通_行首text.Name+"mess_but", mess1);
            but1.Location = new Point(普通_行首text.Location.X - but1.Width - 3, 普通_行首text.Location.Y+2);
            普通_行首text.Parent.Controls.Add(but1);
        }
        /// <summary>
        /// 初始化导出按钮
        /// </summary>
        private void initExportCombox() {
            // 实例化导出下拉框
            ExportComBox exportComBox = new ExportComBox(new int[]{ExportComBox.EXPORT_EXCEL_VAL});
            ComboBox comboBox = exportComBox.export_combox;
            // 绑定点击事件
            comboBox.SelectedIndexChanged += (object sender, EventArgs e) =>{
                ComboBox box = (ComboBox)sender;
                int val = int.Parse(box.SelectedValue.ToString());
                switch(val) {
                    case ExportComBox.EXPORT_TEXT_VAL:
                        exportText();
                        break;
                    case ExportComBox.EXPORT_NOTEBOOK_VAL:
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
        /// 导出数据到文本框
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="t">文本框</param>
        /// <param name="excNoHaveTabs">不包含tab符号</param>
        public void exportText() {
            if(textBox != null) {
                int start = textBox.SelectionStart;
                string s = resultTextBox.SelectionLength == 0? resultTextBox.Text : resultTextBox.SelectedText;
                if(textBox.SelectionLength == 0) { 
                    textBox.Text = s;
                } else { 
                    textBox.SelectedText = s;
                }
                textBox.SelectionStart = start;
                textBox.SelectionLength = s.Length;
            }
        }
        /// <summary>
        /// 导出数据到记事本
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="excNoHaveTabs">不包含tab符号</param>
        public void exportNotepad() {
            if(resultTextBox.SelectionLength > 0) { 
                FileUtilsMet.turnOnNotepad(resultTextBox.SelectedText);
            } else { 
                FileUtilsMet.turnOnNotepad(resultTextBox.Text);
            }
            
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
                    Panel p = ControlsUtilsMet.getHistoricalPanel(普通_行首text
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
                    Panel p = ControlsUtilsMet.getHistoricalPanel(普通_行尾text
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
            // 初始化消息提示控件
            // initToolTip();
            // 初始化导出按钮
            initExportCombox();
            // 行首文本框获取焦点
            普通_行首text.Focus();
        }
        // 操作区容器重绘事件
        private void 普通_操作容器_Paint(object sender, PaintEventArgs e) {
            Panel panel = (Panel)sender;
            ControlsUtilsMet.setCOntrolBorderStyle(e.Graphics, panel.ClientRectangle
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
            // 初始化数据
            ordinaryInitData();
            // 执行添加方法
            addCharsMet();
        }
        // 文本框键盘按下事件
        private void textbox_KeyDown(object sender, KeyEventArgs e) {
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
        private void 普通_textbox_MouseEnter(object sender, EventArgs e) {
            TextBox textBox = (TextBox)sender;
            // 绑定右键菜单
            TextRightMenu.bindingTextBox(textBox);
        }
    }
}
