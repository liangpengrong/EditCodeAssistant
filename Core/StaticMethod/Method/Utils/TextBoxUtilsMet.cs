using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 关于文本框操作的公有工具类
    /// </summary>
    public partial class TextBoxUtilsMet
    {
        /// <summary>
        /// 空参构造函数
        /// </summary>
        private TextBoxUtilsMet(){}
        /// <summary>
        /// 实现文本框全选
        /// </summary>
        /// <param name="t">传入的文本框</param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textAllSelect(TextBox t)
        {
            t.SelectAll();//全选文本
            return null;
        }

        /// <summary>
        /// 实现文本框剪切
        /// </summary>
        /// <param name="t">传入的文本框</param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textSelectCut(TextBox t)
        {
            //判断选中的文本长度是否为0
            if (!t.SelectionLength.Equals(0)){
                t.Cut();
            }
            return null;
        }
        /// <summary>
        /// 实现文本框粘贴
        /// </summary>
        /// <param name="t">传入的文本框</param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textPaste(TextBox t)
        {
            // 判断剪切板内容是否为空
            if (Clipboard.GetText().Length.Equals(0)){
                MessageBox.Show("剪切版为空,请将内容复制入剪切版");
                return null;
            }
            string pasteText = Clipboard.GetText();
            // 判断文本框选中文字长度是否为0
            if( !t.SelectionLength.Equals(0)){
                t.Paste(pasteText);
            } else {
                
                int start = t.SelectionStart;
                if(t.TextLength.Equals(0)) {
                    t.Text = pasteText;
                } else { 
                    t.Text = t.Text.Insert(start, pasteText);
                }
                // 重新设置选择起始位置
                t.SelectionStart = start + pasteText.Length;
            }

            return null;
        }
        /// <summary>
        /// 实现文本框复制
        /// </summary>
        /// <param name="t">传入的文本框</param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textCopy(TextBox t)
        {
            //判断文本框选中文字长度是否不为0
            if (!t.SelectionLength.Equals(0)) {
                t.Copy();
            }
            return null;
        }
        /// <summary>
        /// 实现文本框选中删除
        /// </summary>
        /// <param name="t">传入的文本框</param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textSelectDelect(TextBox t)
        {
            if(!t.SelectionLength.Equals(0))
            {//判断文本框选中
                int start = t.SelectionStart;
                t.Text = t.Text.Remove(t.SelectionStart, t.SelectionLength);
                t.SelectionStart = start;
            }
            return null;
        }
        /// <summary>
        /// 实现文本框清空文本
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textClear(TextBox t)
        {
            t.Clear();
            return null;
        }
        /// <summary>
        /// 实现文本框去除空格
        /// </summary>
        /// <param name="t">传入的文本框</param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textDelSpace(TextBox t)
        {
            int tIndex = t.SelectionStart;
            int tselLength = t.SelectionLength;
            int tLength = t.TextLength;
            string text = t.SelectionLength > 0? t.SelectedText : t.Text;
            text = text.Replace(" ", "").Replace("　", "");
            if(tselLength > 0) {
                t.SelectedText = text;
                t.SelectionStart = t.SelectionStart - text.Length;
                t.SelectionLength = text.Length;
            } else {
                t.Text = text;
                int tempStart = tIndex - (tLength - text.Length);
                t.SelectionStart = tempStart>0?tempStart:0;
            }
            return null;
        }
        /// <summary>
        /// 实现文本框去处行首空格
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textDelRowFirstSpace(TextBox t)
        {
            int tIndex = t.SelectionStart;
            int tselLength = t.SelectionLength;
            int tLength = t.TextLength;
            string text = t.SelectionLength > 0? t.SelectedText : t.Text;
            string[] lineArr = null;
            if(text.Length > 0) { 
                lineArr = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                StringUtilsMet.charsTrim(lineArr, " ", 1);
                text = string.Join(Environment.NewLine, lineArr);
                if(tselLength > 0) {
                    t.SelectedText = text;
                    t.SelectionStart = t.SelectionStart - text.Length;
                    t.SelectionLength = text.Length;
                } else { 
                    t.Text = text;
                    int tempStart = tIndex - (tLength - text.Length);
                    t.SelectionStart = tempStart>0?tempStart:0;
                }
            }
            return null;
        }
        /// <summary>
        /// 实现文本框去除行尾空格
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textDelRowTailSpace(TextBox t)
        {
            int tIndex = t.SelectionStart;
            int tselLength = t.SelectionLength;
            int tLength = t.TextLength;
            string text = t.SelectionLength > 0? t.SelectedText : t.Text;
            string[] lineArr = null;
            if(text.Length > 0) { 
                lineArr = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                StringUtilsMet.charsTrim(lineArr, " ", 2);
                text = string.Join(Environment.NewLine, lineArr);
                if(tselLength > 0) {
                    t.SelectedText = text;
                    t.SelectionStart = t.SelectionStart - text.Length;
                    t.SelectionLength = text.Length;
                } else { 
                    t.Text = text;
                    int tempStart = tIndex - (tLength - text.Length);
                    t.SelectionStart = tempStart>0?tempStart:0;
                }
            }
            return null;
        }
        /// <summary>
        /// 实现文本框去除空行
        /// </summary>
        /// <param name="t"></param>
        /// <returns>返回需要的数据，可为null</returns>
        public static object textDelBlankLine(TextBox t)
        {
            int index = t.SelectionStart;
            StringBuilder strB = new StringBuilder();
            string text = t.Text;
            if(!t.SelectionLength.Equals(0)) text = t.SelectedText;

            string[] sAll = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string s in sAll)
            {
                if (!s.Equals(""))
                {
                    strB.AppendLine(s);
                }
            }

            if (!t.SelectionLength.Equals(0)) {
                t.Text=strB.ToString().Remove(strB.ToString().Length - System.Environment.NewLine.Length);
            } else { 
                t.SelectedText=strB.ToString().Remove(strB.ToString().Length - System.Environment.NewLine.Length);
            }
            

            t.SelectionStart =  index;
            return null;
        }
        /// <summary>
        /// 实现文本框去处换行符
        /// </summary>
        /// <param name="t"></param>
        public static object textPlaceNewline(TextBox t)
        {
            int index = t.SelectionStart;
            int length = t.TextLength;
            int selLength = t.SelectionLength;
            string text = "";
            if(selLength > 0) { 
                text = t.SelectedText;
                text = text.Replace(Environment.NewLine, "");
                t.SelectedText = text;
            } else { 
                text = t.Text;    
                text = text.Replace(Environment.NewLine, "");
                t.Text = text;
            }
            t.SelectionStart = index;
            t.SelectionLength = text.Length;

            return null;
        }
        /// <summary>
        /// 实现文本框去处制表符
        /// </summary>
        /// <param name="t"></param>
        public static object textPlaceTabs(TextBox t)
        {
            int index = t.SelectionStart;
            int length = t.TextLength;
            int selLength = t.SelectionLength;
            string text = "";
            if(selLength > 0) { 
                text = t.SelectedText;
                text = text.Replace("\t", "");
                t.SelectedText = text;
            } else { 
                text = t.Text;    
                text = text.Replace("\t", "");
                t.Text = text;
            }
            t.SelectionStart = index;
            t.SelectionLength = text.Length;
            return null;
        }
        /// <summary>
        /// 实现文本框根据选择情况转化为大写
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object textToUpper(TextBox t) 
        {
            int index = t.SelectionStart;
            int selLen = t.SelectionLength;
            if (t.SelectionLength.Equals(0)) {
                t.Text = t.Text.ToUpperInvariant();
            } else  {
                t.SelectedText = t.SelectedText.ToUpperInvariant();
            }
            t.SelectionStart =  index;
            t.SelectionLength = selLen;
            return null;
        }
        /// <summary>
        /// 实现文本框根据选择情况转化为小写
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object textToLower(TextBox t)
        {
            int index = t.SelectionStart;
            int selLen = t.SelectionLength;
            if(t.SelectionLength.Equals(0)) {
                t.Text = t.Text.ToLowerInvariant();
            } else {
                t.SelectedText = t.SelectedText.ToLowerInvariant();
            }
            t.SelectionStart =  index;
            t.SelectionLength = selLen;
            return null;
        }
        /// <summary>
        /// 将文本框的内容转化为驼峰形式
        /// </summary>
        /// <param name="t">文本框</param>
        /// <returns></returns>
        public static object textToHump(TextBox t) {
            int start = t.SelectionStart;
            int selLen = t.SelectionLength;
            if(t.SelectionLength > 0){
               t.SelectedText = StringUtilsMet.charsToHump(t.SelectedText);
            } else { 
                t.Text = StringUtilsMet.charsToHump(t.Text);
            }
            t.Select(start, selLen);
            return null;
        }
        /// <summary>
        /// 实现文本框插入当前日期
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object textInsertDate(TextBox t)
        {
            int start = t.SelectionStart;
            string dateStr = MessyUtilsMet.getGenerateDate("yyyy/MM/dd HH:mm:ss");
            t.Text=t.Text.Insert(t.SelectionStart, dateStr);
            t.SelectionStart = start + dateStr.Length;
            return null;
        }
        /// <summary>
        /// 将键值对的数据写入到文本框的Tag中
        /// </summary>
        /// <param name="t">要写入的文本框</param>
        /// <param name="key">写入的key</param>
        /// <param name="val">写入的value</param>
        public static void textAddTag(TextBox t, string key, object val)
        {
            Dictionary<string, object> tagDic = null;
            if (t.Tag == null){
                tagDic = new Dictionary<string, object>();
                tagDic.Add(key, val);
            } else{
                tagDic=(Dictionary<string, object>)t.Tag;
                if (tagDic.ContainsKey(key)) {
                    tagDic[key] = val;
                } else  {
                    tagDic.Add(key,val);
                }
            }
            t.Tag = tagDic;
        }
        /// <summary>
        /// 获取文本框的Tag数据并强制转化为Dictionary格式
        /// </summary>
        /// <param name="t">要获取tag的文本框</param>
        /// <returns></returns>
        public static Dictionary<string, object> getDicTextTag(TextBox t)
        {
            Dictionary<string, object> tagD=new Dictionary<string,object>();
            try {
                tagD = (Dictionary<string, object>)t.Tag;
            } catch (Exception e){
                MessageBox.Show(e.ToString());
            }
            return tagD;
        }
        /// <summary>
        /// 统计文本框总行数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static long getTextBoxTotalRow(TextBox t)
        {
            long row = t.Lines.LongLength;
            return row==0?1:row;
        }
        /// <summary>
        /// 统计字符串总行数
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static long getTextBoxTotalRow(string text)
        {
            var regex = new Regex(Environment.NewLine);
            var matches = regex.Matches(text);
            long row = matches.Count;
            return row+1;
        }
        /// <summary>
        /// 获取文本框的总字符数
        /// </summary>
        /// <param name="t">文本框</param>
        /// <param name="isLine">是否包含换行符</param>
        /// <returns></returns>
        public static int getTextBoxChars(TextBox t,bool isLine) 
        {
            if (isLine) {
                return t.TextLength;
            } else {
                return t.Text.Replace(System.Environment.NewLine,"").Length;
            }
        }
        /// <summary>
        /// 获取文本框选中的字符数
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int getTextBoxSelChars(TextBox t) 
        {
            return t.SelectedText.Replace(Environment.NewLine, "").Length;
        }
        /// <summary>
        /// 获取文本框当前光标所处的行与列
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static int[] getTextBoxRowColumn(TextBox t)
        {
            int[] rowColum = new int[] {1,1};
            // 得到总行数。该行数会随着文本框的大小改变而改变；若只认回车符为一行(不考虑排版变化)请用 总行数=textBox1.Lines.Length;(记事本是这种方式)
            int totalline = t.Lines.Length + 1;
            // 得到当前选中位置的行第一个字符的索引
            int index = t.GetFirstCharIndexOfCurrentLine();
            // 得到当前行的行号,从0开始，习惯是从1开始，所以+1.
            int line = t.GetLineFromCharIndex(index) + 1;
            // SelectionStart得到光标所在位置的索引 减去 当前行第一个字符的索引 = 光标所在的列数（从0开始)
            int col = 1;
            if (t.SelectionStart - index > -1) {
                col = t.SelectionStart - index + 1;
            }else {
                col = (t.SelectionStart+t.SelectionLength) - index + 1;
            }
            rowColum[0] = line;
            rowColum[1] = col;
            return rowColum;
        }

    }
}
