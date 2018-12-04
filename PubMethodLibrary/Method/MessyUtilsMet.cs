using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PubMethodLibrary
{
    /// <summary>
    /// 杂项工具类
    /// </summary>
   public class MessyUtilsMet
   {
       /// <summary>
       /// 当前环境下的换行符的长度
       /// </summary>
       /// <returns></returns>
       public static int lineLength()
       {
           return System.Environment.NewLine.Length;
       }
       /// <summary>
       /// 获取指定的正则表达式的实例
       /// </summary>
       /// <param name="input">传递的正则表达式字符串</param>
       /// <returns></returns>
       public static Regex getSpecifiedRegex(String input)
       {
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
       public static String[] splitAtChar(String str, String delimiter, StringSplitOptions splitFormat)
       {
           return str.Split(new String[] { delimiter }, splitFormat);
       }
       public static int getTextRow(String str, StringSplitOptions splitFormat)
       {//根据字符串数组获取字符串的行数
           return str.Split(new String[] { System.Environment.NewLine }, splitFormat).Length;
       }
       public static String[] getTextRowSet(String str, StringSplitOptions splitFormat)
       {//根据字符串数组获取字符串的数组
           return str.Split(new String[] { System.Environment.NewLine }, splitFormat);
       }
       public static int getColumn(String str, int selectionStart)
       {//根据字符串和开始位置判断在第几列
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

       public static List<String> splitcharacters(String str, Char cha)
       {//将字符串数据按指定符号分割返回List
           List<String> strArray = new List<string>();
           foreach (String s in str.Split(cha))
           {
               strArray.Add(s);
           }
           return strArray;
       }
       public static String dicConversionStr(Dictionary<String, String> dic, String joiner)
       {//将传入的Dictionarykey value按指定连接符连接
           StringBuilder strB = new StringBuilder();
           foreach (KeyValuePair<String, String> kv in dic)
           {
               strB.Append(kv.Key + joiner + kv.Value);
               strB.Append(System.Environment.NewLine);
           }
           return strB.ToString().Remove(strB.ToString().Length - lineLength());
       }
       public static String dicConversionStr(List<String> l1, List<Dictionary<String, String>> dic, String joiner)
       {//将传入的两个List中的一个Dictionary key value按指定连接符连接
           StringBuilder strB = new StringBuilder();
           if (l1.Count != dic.Count) { return ""; }
           for (int i = 0; i < l1.Count; i++)
           {
               strB.Append(l1[i] + System.Environment.NewLine);
               foreach (KeyValuePair<String, String> kv in dic[i])
               {
                   strB.Append(kv.Key + joiner + kv.Value);
                   strB.Append(System.Environment.NewLine);
               }
           }
           return strB.ToString().Remove(strB.ToString().Length - lineLength());
       }
       /// <summary>
       /// 获取去除文件后缀名的文件名
       /// </summary>
       /// <param name="fileName">文件名</param>
       /// <returns>去除文件后缀名的文件名</returns>
       public static String delSuffixName(String fileName)
       {
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
       public static String centerCharacter(String content, 
           String addChar, 
           int totalSize, 
           int singleSize, 
           int addCharSize)
       {
           int a = (((((totalSize) / 2) - (singleSize / 2)) / 2) - 10) / addCharSize;
           String str = content;
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
       public static String getGenerateDate(String format)
       {//按照指定格式生成日期和时间
           String dt = "";
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
       public static int[] getResolvingpower() { 
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            return new int[]{rect.Width, rect.Height};
       }
   }
}
