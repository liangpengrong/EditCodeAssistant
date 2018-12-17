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
    public partial class RowGoToForm : Form
    {
        // 是否验证通过
        private Boolean isCheck = false;
        // 要操作的文本框
        public TextBox textBox;

        // 可跳转到的行的最大值
        private long maxTextLings = 0;
        // 要跳转到的行
        private long goTextLings = 0;

        public RowGoToForm(TextBox textBox)
        {
            this.textBox = textBox;
            InitializeComponent();
        }

        // 窗体加载事件
        private void RowGoToForm_Load(object sender, EventArgs e)
        {
            // 设置图标
            this.Icon = MessyUtilsMet.IamgeToIcon(StaticDataLibrary.Image.转到行,true);

            if(textBox.TextLength != 0){
                maxTextLings = this.textBox.Lines.Length;
                //改变行号范围
                this.行号Num_L.Text = "(1 - "+(maxTextLings != 0?maxTextLings:1)+")";
                // 将当前文本框的起始位置赋值到行号文本框中
                this.行号T.Text = (textBox.GetLineFromCharIndex(textBox.SelectionStart) + 1).ToString();
            } else { 
                this.行号Num_L.Text = "(0 - 0)";
                this.行号T.Text = "0";
            }
        }

        //取消按钮的点击事件
        private void exc_but_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //确定按钮的点击事件
        private void ok_but_Click(object sender, EventArgs e)
        {
            // 判断验证是否通过
            if( 0.Equals(textBox.TextLength)) { 
                MessageBox.Show("要操作的文本框内容不能为空");
            } else if( !isCheck){ 
                MessageBox.Show("验证未通过");
            } else{ 
                goTextLings = long.Parse(this.行号T.Text);
                // 转到行
                goToRow();
            }
        }
        //跳转到指定行
        private void goToRow() {
            // 验证
            if(!isCheck){ 
                MessageBox.Show("验证未通过");
                return;
            }
            // 获得当前文本框的行数组
            String[] lineArr = textBox.Lines;
            // 要选择的行的起始位置
            int rowIndex = 0;
            // 要选择行的长度
            int selectI = lineArr[0].Length;
            for(long i = 0, len = lineArr.Length; i < len; i++) {
                if(i.Equals(goTextLings - 1)) break;
                // 将当前行的文本加上换行符
                String s = lineArr[i] + Environment.NewLine;
                rowIndex = rowIndex + s.Length;
            }
            selectI = lineArr[goTextLings - 1].Length;
            textBox.Select(rowIndex, selectI);
            if(0.Equals(selectI)){MessageBox.Show("该行为空行所以无法显示选中效果");}
        }

        //文本框验证
        private Boolean textBoxCheck(TextBox t) {
            try {
                this.errorProvider.Clear();
                if(t.TextLength == 0) { 
                    errorMes(t,"行号必须为数字");
                    return false;
                };
                if(! Regex.IsMatch(t.Text, @"^[+-]?\d*[.]?\d*$")) { 
                    errorMes(t,"行号必须为数字");
                    return false;
                }
                int row = int.Parse(t.Text);
                if(row <=0 || row > maxTextLings) { 
                    errorMes(t,"行号必须为大于0小于"+maxTextLings+"的数字");
                    return false;
                }
            } catch{
                errorMes(t,"行号必须为大于0小于"+maxTextLings+"的数字");
                return false;
            }
            
            return true;
        }
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="c"></param>
        /// <param name="mes"></param>
        private void errorMes(Control c, String mes){ 
            this.errorProvider.SetError(c, mes);
            this.errorProvider.SetIconAlignment(c,ErrorIconAlignment.MiddleRight);
            this.errorProvider.SetIconPadding(c, -20);
        }
        // 文本框文本改变事件
        private void 行号T_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            // 判断是否验证通过
            isCheck = textBoxCheck(t); 
        }
    }
}
