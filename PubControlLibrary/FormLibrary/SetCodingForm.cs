using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubMethodLibrary;
using StaticDataLibrary;

namespace PubControlLibrary {
    public partial class SetCodingForm : Form {
        // 要操作的文本框
        private TextBox textBox;
        // 文本框的编码
        private Encoding textCoding = null;

        public SetCodingForm(TextBox textBox) {
            this.textBox = textBox;
            InitializeComponent();
        }

        // 窗体加载事件
        private void SetCodingForm_Load(object sender, EventArgs e) {
            // 将文本框的编码赋值到label中
            textCodinCopyLab();
            setCodingSet(getEncoder());
        }
        
        /// <summary>
        /// 将文本框的编码赋值到label中
        /// </summary>
        private void textCodinCopyLab() {
            Dictionary<string, object> textDic = TextBoxUtilsMet.getDicTextTag(textBox);
            if(textDic.ContainsKey(TextBoxTagKey.textEcoding)){ 
                Encoding coding = (Encoding)textDic[TextBoxTagKey.textEcoding];
                textCoding = coding;
                get_coding.Text = coding.BodyName.ToUpper();    
            } else { 
                get_coding.Text = Encoding.UTF8.BodyName.ToUpper();
                textCoding = Encoding.UTF8;
            }
            
        }
        /// <summary>
        /// 设置下拉列表框的值
        /// </summary>
        private void setCodingSet(Dictionary<string, int> items) {
            int selIndex = 0;
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;
            column = new DataColumn("Name");
            table.Columns.Add(column);
            column = new DataColumn("Value");
            table.Columns.Add(column);
            // 设置数据源
            foreach(KeyValuePair<string, int> kvp in items) { 
                string key = kvp.Key.ToUpper();
                int val = kvp.Value;
                if(textCoding != null && val.Equals(textCoding.CodePage)) {
                    selIndex = table.Rows.Count;
                }
                row = table.NewRow();
                row["Name"] = key;
                row["Value"] = val;
                table.Rows.Add(row);
            }
            
            // 绑定数据源
            this.coding_set.DisplayMember = "Name";
            this.coding_set.ValueMember = "Value";
            this.coding_set.DataSource = table;
            this.coding_set.SelectedIndex = selIndex;
            // 设置自动匹配
            this.coding_set.AutoCompleteMode = AutoCompleteMode.Suggest;
            this.coding_set.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        /// <summary>
        /// 获取下拉框要显示的编码
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> getEncoder() { 
            Dictionary<string, int> retDic = new Dictionary<string, int>();
            EncodingInfo[] codings = Encoding.GetEncodings();
            foreach(EncodingInfo coding in codings) {
                if( !retDic.ContainsKey(coding.Name.ToUpper())) { 
                    retDic.Add(coding.Name.ToUpper(), coding.CodePage);
                }
            }
            // 升序排序
            retDic = retDic.OrderBy(p=>p.Key).ToDictionary(p => p.Key, o => o.Value);

            return retDic;
        }
        /// <summary>
        /// 将文本框编码设置为指定编码格式
        /// </summary>
        private void setTextByEncoding() {
            if(textBox == null) MessageBox.Show("要操作的文本框为NULL"); 
            // 获取起始选中位置和选中长度
            int index = textBox.SelectionStart;
            int selLen = textBox.SelectionLength;
            // 获取选中的项的编码页码
            int codingInt = int.Parse(((DataRowView)this.coding_set.SelectedItem).Row.ItemArray[1].ToString());
            // 获取选择项的编码
            Encoding coding = Encoding.GetEncoding(codingInt);
            // 获取文本框的文本
            string text = textBox.Text;
            
            // 将文本框的文本设置为指定编码格式

            byte[] textBoxBytes = textCoding.GetBytes(text);

            byte[] asciiBytes = Encoding.Convert(textCoding, coding, textBoxBytes);

            //char[] asciiChars = new char[coding.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            //coding.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            textBox.Text = coding.GetString(asciiBytes);




            //byte[] codingByte = coding.GetBytes(text);
            //String a= coding.GetString(codingByte);
            //textBox.Text = coding.GetString(codingByte);
            // 恢复文本框的起始位置和选中长度
            textBox.SelectionStart = index;
            textBox.SelectionLength = selLen;
            // 设置保持在Tag数据中的文本框编码
            TextBoxUtilsMet.textAddTag(textBox, TextBoxTagKey.textEcoding, coding);
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool isCheck() {
            if(coding_set.Text.Length == 0) { 
                MessageBox.Show("不能输入空");
                return false;
            }
            if(coding_set.SelectedItem == null) { 
                MessageBox.Show("只能输入下拉框范围的值");
                return false;
            }
            return true;
        }
        // 确定按钮的点击事件
        private void button1_Click(object sender, EventArgs e) {
            // 验证
            if(!isCheck()) return;
            // 设置编码
            setTextByEncoding();
            this.Close();
        }
    }
}
