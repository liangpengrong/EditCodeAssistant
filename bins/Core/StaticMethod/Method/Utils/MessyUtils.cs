using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 杂项工具类
    /// </summary>
    public static class MessyUtils
    {
        /// <summary>
        /// 当前环境下的换行符的长度
        /// </summary>
        /// <returns></returns>
        public static int GetLineLength() {
            return Environment.NewLine.Length;
        }
        /// <summary>
        /// 获取指定的正则表达式的实例
        /// </summary>
        /// <param name="input">传递的正则表达式字符串</param>
        /// <returns></returns>
        public static Regex GetSpecifiedRegex(string input) {
            Regex regex = new Regex(input);
            return regex;
        }
        /// <summary>
        /// //获取按照指定字符串分割后的数组
        /// </summary>
        /// <param name="str">要分割的字符串</param>
        /// <param name="delimiter">分割的字符</param>
        /// <param name="splitFormat">返回的数组是否包含空字符串</param>
        /// <returns>分割后的数组</returns>
        public static string[] SplitAtChar(string str, string delimiter, StringSplitOptions splitFormat) {
            return str.Split(new string[] { delimiter }, splitFormat);
        }
        /// <summary>
        /// 根据字符串数组获取字符串的行数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitFormat"></param>
        /// <returns></returns>
        public static int GetTextRow(string str, StringSplitOptions splitFormat) {
            return str.Split(new string[] { System.Environment.NewLine }, splitFormat).Length;
        }
        /// <summary>
        /// 根据字符串数组获取字符串的数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitFormat"></param>
        /// <returns></returns>
        public static string[] GetTextRowSet(string str, StringSplitOptions splitFormat) {
            return str.Split(new string[] { System.Environment.NewLine }, splitFormat);
        }
        // 根据字符串和开始位置判断在第几列
        public static int GetColumn(string str, int selectionStart) {
            int rowBefore = 0;
            if (selectionStart == 0) return 0;
            for (int i = selectionStart - 1; i >= 0; i--)
            {
                if (str[i].Equals('\n'))
                {
                    rowBefore = i + 1;
                    break;
                }
            }
            return selectionStart - rowBefore;
        }
        /// <summary>
        /// 将字符串数据按指定符号分割返回List
        /// </summary>
        /// <param name="str"></param>
        /// <param name="cha"></param>
        /// <returns></returns>
        public static List<string> Splitcharacters(string str, Char cha) {
            List<string> strArray = new List<string>();
            foreach (string s in str.Split(cha))
            {
                strArray.Add(s);
            }
            return strArray;
        }
        /// <summary>
        /// 将传入的Dictionarykey value按指定连接符连接
        /// </summary>
        /// <param name="dic"></param>
        /// <param name="joiner"></param>
        /// <returns></returns>
        public static string DicConversionStr(Dictionary<string, string> dic, string joiner) {
            StringBuilder strB = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in dic)
            {
                strB.Append(kv.Key + joiner + kv.Value);
                strB.Append(System.Environment.NewLine);
            }
            return strB.ToString().Remove(strB.ToString().Length - GetLineLength());
        }
        /// <summary>
        /// 将传入的两个List中的一个Dictionary key value按指定连接符连接
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="dic"></param>
        /// <param name="joiner"></param>
        /// <returns></returns>
        public static string DicConversionStr(List<string> l1, List<Dictionary<string, string>> dic, string joiner){
            StringBuilder strB = new StringBuilder();
            if (l1.Count != dic.Count) { return ""; }
            for (int i = 0; i < l1.Count; i++)
            {
                strB.Append(l1[i] + System.Environment.NewLine);
                foreach (KeyValuePair<string, string> kv in dic[i])
                {
                    strB.Append(kv.Key + joiner + kv.Value);
                    strB.Append(System.Environment.NewLine);
                }
            }
            return strB.ToString().Remove(strB.ToString().Length - GetLineLength());
        }
        /// <summary>
        /// 获取去除文件后缀名的文件名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>去除文件后缀名的文件名</returns>
        public static string DelSuffixName(string fileName) {
            int index1, index2 = 0;
            index1 = fileName.LastIndexOf(".");//获取字符串最后一个.的位置
            index2 = fileName.LastIndexOf("\\");//获取字符串最后一个\的位置
            if (index1 < 0 || index2 < 0)
            {
                return fileName;
            }
            return fileName.Remove(index1).Substring(index2 + 1);
        }
        /// <summary>
        /// 通过传入的字符像素大小与总大小实现居中
        /// </summary>
        /// <param name="content"></param>
        /// <param name="addChar"></param>
        /// <param name="totalSize"></param>
        /// <param name="singleSize"></param>
        /// <param name="addCharSize"></param>
        /// <returns></returns>
        public static string CenterCharacter(string content, string addChar, int totalSize, int singleSize, int addCharSize) {
            int a = (((((totalSize) / 2) - (singleSize / 2)) / 2) - 10) / addCharSize;
            string str = content;
            for (int i = 0; i < a; i++)
            {
                str = addChar + str;
            }
            return str;
        }
        /// <summary>
        /// 获取指定格式的日期字符串
        /// </summary>
        /// <param name="format">指定的日期格式，如yyyy/MM/dd HH:mm:ss</param>
        /// <returns></returns>
        public static string GetGenerateDate(string format)
        {//按照指定格式生成日期和时间
            string dt = "";
            try
            {
                dt = DateTime.Now.ToString(format);
            }
            catch(FormatException fe)
            {
                throw fe;
            }
            return dt;
        }
    
        /// <summary>
        /// 获取屏幕的宽高
        /// </summary>
        /// <returns></returns>
        public static int[] GetResolvingpower() { 
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            return new int[]{rect.Width, rect.Height};
        }
        /// <summary>
        /// 将Iamge转化为Icon
        /// </summary>
        /// <param name="image">要转换为图标的Image对象</param>
        /// <param name="nullTonull">当image为null时是否返回null。false则抛空引用异常</param>
        /// <returns></returns>
        public static Icon IamgeToIcon(Image image, bool nullTonull = false){
          if (image == null)
          {
            if (nullTonull) { return null; }
            throw new ArgumentNullException("image");
          }

          using (MemoryStream msImg = new MemoryStream()
                   , msIco = new MemoryStream())
          {
            image.Save(msImg, ImageFormat.Png);

            using (var bin = new BinaryWriter(msIco))
            {
              //写图标头部
              bin.Write((short)0);      //0-1保留
              bin.Write((short)1);      //2-3文件类型。1=图标, 2=光标
              bin.Write((short)1);      //4-5图像数量（图标可以包含多个图像）

              bin.Write((byte)image.Width); //6图标宽度
              bin.Write((byte)image.Height); //7图标高度
              bin.Write((byte)0);      //8颜色数（若像素位深>=8，填0。这是显然的，达到8bpp的颜色数最少是256，byte不够表示）
              bin.Write((byte)0);      //9保留。必须为0
              bin.Write((short)0);      //10-11调色板
              bin.Write((short)32);     //12-13位深
              bin.Write((int)msImg.Length); //14-17位图数据大小
              bin.Write(22);         //18-21位图数据起始字节

              //写图像数据
              bin.Write(msImg.ToArray());

              bin.Flush();
              bin.Seek(0, SeekOrigin.Begin);
              return new Icon(msIco);
            }
          }
        }

    }
}
