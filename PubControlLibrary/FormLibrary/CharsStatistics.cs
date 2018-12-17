using PubMethodLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PubControlLibrary
{
    public partial class CharsStatistics : Form
    {
        // 要查看的文本框
        private TextBox textBox = null;
        // 控件默认的与信息栏左边的距离
        private int conDefLocX = 7;
        // 控件与上一个控件默认的高度
        private int conNextConH = 4;
        // 标签默认的大小
        private int[] labDefSize = new int[]{120, 25};
        // 文本框默认的大小
        private int[] textBDefSize = new int[]{80,25};
        // 复选框默认的大小
        private int[] chkBDefSize = new int[]{75,25};

        private String 字符总数TName = "字符总数T";
        private String 字数BJ换行符TName = "字数BJ换行符T";
        private String 字数BJ空格TName = "字数BJ空格T";
        private String 行数TName = "行数T";
        private String 非中文数TName = "非中文数T";
        private String 中文数TName = "中文数T";
        public CharsStatistics(TextBox textBox)
        {
            this.textBox = textBox;
            InitializeComponent();
        }
        // 窗体加载事件
        private void CharsStatistics_Load(object sender, EventArgs e)
        {
            // 设置图标
            this.Icon = MessyUtilsMet.IamgeToIcon(StaticDataLibrary.Image.统计,true);
            // 获取待添加的控件集合
            List<Control[]> conList = getControlList();
            // 为选中字符控件绑定事件
            CheckBox chk1 = getTempCheckB("选中字符CHK","选中字符");
            chk1.CheckedChanged += Chk1_CheckedChanged;
            // 添加到控件集合中
            conList.Add(new Control[]{ chk1});
            // 将标签和文本框放入信息栏中并调整标签文本框位置和信息栏的大小
            labAndTextLayout(conList);
            // 获取文本框与值的对应关系
            Dictionary<String,String> nameAndVal = getTextNameValDic(textBox.Text);
            // 根据传入的文本框name和值的对应关系给文本框赋值
            textBoxAssignment(nameAndVal);
            // 调整窗口大小
            formSizeLayout();
        }

        /// <summary>
        /// 获取待添加的控件集合
        /// </summary>
        /// <returns></returns>
        private List<Control[]> getControlList() {
            List<Control[]> conList = new List<Control[]>();
            // 控件的集合
            conList.Add(new Control[]{ getTempLab("字数总数L","字符总数："), getTempTextB(字符总数TName, "0")});
            conList.Add(new Control[]{ getTempLab("字数BJ换行符L","字符数(不计换行符)："), getTempTextB(字数BJ换行符TName, "0")});
            conList.Add(new Control[]{ getTempLab("字数BJ空格L","字符数(不计空格)："), getTempTextB(字数BJ空格TName, "0")});
            conList.Add(new Control[]{ getTempLab("行数L","行数："), getTempTextB(行数TName, "0") });
            conList.Add(new Control[]{ getTempLab("非中文数L","非中文单词数："), getTempTextB(非中文数TName, "0")});
            conList.Add(new Control[]{ getTempLab("中文数L","中文单词数："), getTempTextB(中文数TName, "0")});
            return conList;
        }
        /// <summary>
        /// 为选中字符复选框绑定选项改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chk1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkTemp = (CheckBox)sender;
            String t = textBox.Text;
            if(chkTemp.Checked) { 
                t = textBox.SelectedText;
            } else{ 
                t = textBox.Text;
            }
            // 获取文本框与值的对应关系
            Dictionary<String,String> nameAndVal = getTextNameValDic(t);
            // 根据传入的文本框name和值的对应关系给文本框赋值
            textBoxAssignment(nameAndVal);
        }

        /// <summary>
        /// 获取文本框与值的对应关系
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private  Dictionary<String,String> getTextNameValDic(String text) { 
            // 文本框对应的文本框
            String 字符总数 = text.Length.ToString();
            String 字符数不计空格 = text.Replace(" ", "").Replace(" ", "").Length.ToString();
            String 字符数不计换行符 = text.Replace(Environment.NewLine, "").Length.ToString();
            String 行数 = text.Length == 0?"0" : PubMethodLibrary.TextBoxUtilsMet.getTextBoxTotalRow(text).ToString();
            String 非中文数 = Regex.Replace(text,@"[\u4e00-\u9fa5]","").Length.ToString();
            String 中文数 = (text.Length - int.Parse(非中文数)).ToString();

            Dictionary<String,String> nameAndVal = new Dictionary<string, string>();
            nameAndVal.Add(字符总数TName, 字符总数);
            nameAndVal.Add(字数BJ换行符TName, 字符数不计换行符);
            nameAndVal.Add(字数BJ空格TName, 字符数不计空格);
            nameAndVal.Add(行数TName, 行数);
            nameAndVal.Add(非中文数TName, 非中文数);
            nameAndVal.Add(中文数TName, 中文数);
            return nameAndVal;
            
        }
        /// <summary>
        /// 根据传入的文本框name和值的对应关系给文本框赋值
        /// </summary>
        /// <param name="nameAndVal"></param>
        private void textBoxAssignment(Dictionary<String,String> nameAndVal){
            Dictionary<String,String> dic = nameAndVal;
            foreach(KeyValuePair<String,String> kvp in dic){
                String name = kvp.Key;
                String textVal = kvp.Value;
                Control con = PubMethodLibrary.ControlsUtilsMet.getControlByName(统计信息G.Controls, name);
                con.Text = textVal;
            }
        }
        /// <summary>
        /// 获取模板标签
        /// </summary>
        /// <param name="name">NAME</param>
        /// <param name="val">文本</param>
        /// <returns></returns>
        private Label getTempLab(String name, String val) { 
            Label label = new Label();
            label.Name = name;
            label.Text = val;
            label.AutoSize = false;
            label.Size = new Size(labDefSize[0], labDefSize[1]);
            label.Font = this.Font;
            return label;
        }

        /// <summary>
        /// 获取模板文本框
        /// </summary>
        /// <param name="name">NAME</param>
        /// <param name="val">文本</param>
        /// <returns></returns>
        private TextBox getTempTextB(String name, String val) { 
            TextBox textBox = new TextBox();
            textBox.Name = name;
            textBox.Size = new Size(textBDefSize[0], textBDefSize[1]);
            textBox.Text = val;
            textBox.Enabled = true;
            textBox.ReadOnly = true;
            textBox.Font = this.Font;
            textBox.ForeColor = Color.Black;
            textBox.BackColor = Color.White;
            textBox.TabStop = false;
            textBox.MouseDown += (object sender, MouseEventArgs e)=>{
                PubMethodLibrary.WinApiUtilsMet.HideCaret(((TextBox)sender).Handle);
            };
            textBox.BorderStyle = BorderStyle.None;
            return textBox;
        }

        private CheckBox getTempCheckB(String name, String val){ 
            CheckBox cheB = new CheckBox();
            cheB.Name = name;
            cheB.Text = val;
            cheB.AutoSize = false;
            cheB.Size = new Size(chkBDefSize[0], chkBDefSize[1]);
            return cheB;
        }

        /// <summary>
        /// 对Label和TextBox的组合进行布局调整
        /// </summary>
        /// <param name="dic"></param>
        private void labAndTextLayout(List<Control[]> conList) {
            int conW = 0;
            int conH = 0;
            for(int i = 0,len = conList.Count;i < len; i++) {
                int conWtemp = 0;
                if(conList.Count <= 0) break;
                Control[] conArr = conList[i];
                if(conArr.Length <= 0) break;
                Control con1 = conArr[0];
                // 确定每个控件的高
                if(this.统计信息G.Controls.Count == 0) { 
                    con1.Location = new Point(conDefLocX, this.统计信息G.Location.Y+26);
                } else{ 
                    con1.Location = new Point(conDefLocX
                        ,conList[i-1][0].Location.Y + conNextConH + con1.Height);
                }
                conWtemp = conWtemp + con1.Width;
                this.统计信息G.Controls.Add(con1);
                // 确定第i组控件集合中除第一个控件后其余控件的位置
                for(int j = 1,len2 = conArr.Length;j < len2; j++) { 
                    Control tempCon = conArr[j];
                    Control temoCon2 = conArr[j-1];
                    tempCon.Location = new Point(temoCon2.Location.X + temoCon2.Width, con1.Location.Y - 1);
                    if(tempCon.GetType().Equals(new TextBox().GetType())) ((TextBox)tempCon).Select(0, 0);
                    conWtemp = conWtemp + tempCon.Width;
                    this.统计信息G.Controls.Add(tempCon);
                 }
                if(conWtemp >= conW) conW = conWtemp;
                conH = con1.Location.Y + con1 .Height;
            }
            this.统计信息G.Width = conDefLocX + conW + conDefLocX;
            this.统计信息G.Height = conH + 3;
        }
        /// <summary>
        /// 对窗体大小进行调整
        /// </summary>
        private void formSizeLayout() {
            this.ClientSize = new Size(
                统计信息G.Location.X + 统计信息G.Width + 统计信息G.Location.X, 
                统计信息G.Location.Y + 统计信息G.Height + 统计信息G.Location.Y + conDefLocX);
        }
    }
}
