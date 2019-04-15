using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.StaticMethod.Method.Utils {
    /// <summary>
    /// 字符串操作工具类
    /// </summary>
    public static class StringUtilsMet {
        /// <summary>
        /// 将字符串中的指定字符串替换为某个字符串(是否区分大小写)
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="findS">被替换的字符串</param>
        /// <param name="repS">要替换的字符串</param>
        /// <param name="sensitive">是否匹配大小写</param>
        /// <returns></returns>
        public static string repCaseText(string text, string findS, string repS ,bool sensitive) {
            string retS = text;
            if(sensitive) {
                retS = Strings.Replace(text, findS, repS, 1, -1, CompareMethod.Binary);
            } else { 
                retS = Strings.Replace(text, findS, repS, 1, -1, CompareMethod.Text);
            }
            return retS;
        }
        private static string[] splitNotSensitive(string[] splitArr, string sip) {
            // 要返回的List
            List<string> retList = new List<string>();
            // 找到的索引数组
            int[] indexArr = null;
            // 遍历分隔符
            foreach(string str in splitArr) {
                // 判断分隔符不为''就执行查找
                if(sip.Length > 0) indexArr = getCharsIndexOf(str, sip, false);

                // 判断找到的索引集合不为null并且不为空
                if(indexArr != null && indexArr.Length > 0){
                    // 将字符串按照指定索引分割为数组
                    for(int i = 0, len = indexArr.Length; i< len; i++){
                        // 获取当前索引
                        int index = indexArr[i];

                        // 判断索引是否大于等于0并且小于字符串总长
                        if(index >=0 && index < str.Length) {
                            if(i.Equals(0)) { 
                                retList.Add(str.Substring(0,index));
                            } else {
                                // 获取上一个索引加分割符长度
                                int upIndex = indexArr[i-1] + sip.Length;
                                // 获取上一个索引加分割符长度到当前索引的字符串
                                retList.Add(str.Substring(upIndex, index - upIndex));
                            }
                        }
                    }
                    // 获取末尾的索引
                    int entIdex = indexArr[indexArr.Length-1] + sip.Length;
                    // 判断最后一个索引是否符合规范
                    if(entIdex >= 0 && entIdex <= str.Length) {
                        // 将末尾索引到字符串末尾的数据加到集合中
                        retList.Add(str.Substring(entIdex, str.Length - entIdex));
                    } else { 
                        retList.Add(str);
                    }
                } else { 
                    retList.Add(str);
                }
            }
            return retList.ToArray();
        }
        /// <summary>
        /// 将字符串按照指定的分隔符分割为数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="sip">分隔符字符组</param>
        /// <param name="isNone">是否保留空行</param>
        /// <param name="sensitive">是否区分大小写</param>
        /// <returns></returns>
        public static string[] splitStrToArr(string str, string[] sip, bool isNone, bool sensitive) { 
            string[] retArr = null;
            StringSplitOptions sipEnum = StringSplitOptions.None;
            // 是否不包含空行
            if(!isNone) { 
                sipEnum = StringSplitOptions.RemoveEmptyEntries;
            }
            // 是否区分大小写
            if(sensitive) {
               retArr = str.Split(sip, sipEnum);
            } else {
                foreach (string s in sip) {
                    if (retArr == null || 0.Equals(retArr.Length)) {
                        retArr = splitNotSensitive(new string[] { str }, sip[0]);
                    } else {
                        retArr = splitNotSensitive(retArr, s);
                    }
                }
                // 是否不包含空行
                if(!isNone) { 
                    retArr = retArr.Where(s=>!string.IsNullOrEmpty(s)).ToArray();
                }
            }
            return retArr;
        }

        /// <summary>
        /// 将字符串按照指定的索引分割为数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="sipIndex">指定的索引</param>
        /// <param name="isNone">是否保留空行</param>
        /// <param name="isSort">是否对数组排序</param>
        /// <returns></returns>
        public static string[] splitStrToArr(string str, int[] sipIndex, bool isNone, bool isSort){
            string[] retArr = null;
            List<string> tempList = new List<string>();
            if(sipIndex != null && sipIndex.Length > 0) { 
                // 排序数组
                if(isSort) Array.Sort(sipIndex);
                // 将字符串按照指定索引分割为数组
                for(int i = 0, len = sipIndex.Length; i< len; i++){
                    // 获取当前索引
                    int index = sipIndex[i];
                    
                    // 判断索引是否大于等于0并且小于字符串总长
                    if(index >=0 && index < str.Length) {
                        // 获取上一个索引
                        int upIndex = 0.Equals(i)? 0 : sipIndex[i-1];
                       
                        tempList.Add(str.Substring(upIndex, index-upIndex));
                    }
                    
                }
                // 获取末尾的索引
                int entIdex = sipIndex[sipIndex.Length-1];
                // 判断最后一个索引是否符合规范
                if(entIdex >= 0 && entIdex < str.Length) {
                    // 将末尾索引到字符串末尾的数据加到集合中
                    tempList.Add(str.Substring(entIdex, str.Length - entIdex));
                } else { 
                    tempList.Add(str);
                }
            } else { 
                tempList.Add(str);
            }
            retArr = tempList.ToArray();
            // 是否不包含空行
            if( !isNone) { 
                retArr = retArr.Where(s=>!string.IsNullOrEmpty(s)).ToArray();
            }
            return retArr;
        }

        /// <summary>
        /// 将字符串按照指定的分隔符分割为二维数组
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="oneSip">第一个分隔符字符组</param>
        /// <param name="twoSip">第二个分隔符字符组</param>
        /// <param name="oneSpiOtp">第一个分割是否保留空行</param>
        /// <param name="twoSpiOtp">第二个分割是否保留空行</param>
        /// <param name="sensitive1">第一个分割是否区分大小写</param>
        /// <param name="sensitive2">第二个分割是否区分大小写</param>
        /// <returns></returns>
        public static string[][] splitStrToArr(string str, string[] oneSip, string[] twoSip, bool oneIsNone
            , bool twoIsNone, bool sensitive1, bool sensitive2) {
            // 验证参数的合法性
            if(str == null || oneSip == null || twoSip == null) { 
                return null;
            }
            List<string[]> tempList = new List<string[]>();
            string[] twoSpiArr;
            // 按照第一个分隔符分割
            string[] oneSpiArr = splitStrToArr(str, oneSip, oneIsNone, sensitive1);
            foreach (string s in oneSpiArr) { 
                // 按照第二个分隔符分割
                twoSpiArr = splitStrToArr(s, twoSip, twoIsNone, sensitive2);
                tempList.Add(twoSpiArr);
            }
            return tempList.ToArray();
        }

        /// <summary>
        /// 获取子字符串在父字符串中出现的次数
        /// </summary>
        /// <param name="str">父字符串</param>
        /// <param name="findChars">子字符串</param>
        /// <param name="isCase">是否区分大小写</param>
        /// <returns></returns>
        public static int getIndexOfAllCount(string str, string findChars, bool isCase) { 
            int count = 0;
            if(isCase) {
              count = str.Split(new string[] {findChars}, StringSplitOptions.None).Length-1;
            }else { 
              count = Regex.Split(str,findChars,RegexOptions.IgnoreCase).Length-1;
            }
            return count;
        }

        /// <summary>
        /// 返回子字符串在父字符串中出现的全部索引
        /// </summary>
        /// <param name="text">父字符串</param>
        /// <param name="findChars">子字符串</param>
        /// <param name="isCase">是否匹配大小写</param>
        public static int[] getCharsIndexOf(string text, string[] findCharsArr, bool isCase) {
            List<int> retIndex = new List<int>();
            foreach(string s1 in findCharsArr) { 
                int[] tempArr = getCharsIndexOf(text, s1, isCase);
                foreach(int i in tempArr) {
                    if( !retIndex.Contains(i)) { 
                        retIndex.Add(i);
                    } 
                }
            }
            return retIndex.ToArray();
        }

        /// <summary>
        /// 返回子字符串在父字符串中出现的全部索引
        /// </summary>
        /// <param name="text">父字符串</param>
        /// <param name="findChars">字字符串</param>
        /// <param name="isCase">是否匹配大小写</param>
        public static int[] getCharsIndexOf(string text, string findChars, bool isCase) {
            List<int> subIndex = new List<int>();
            int ii = getCharsIndexOf(text, findChars, 0, isCase);
            while(ii >= 0 && ii < text.Length)
            {
                subIndex.Add(ii);
                ii = getCharsIndexOf(text, findChars, ii+findChars.Length, isCase);
            }
            return subIndex.ToArray();
        }

        /// <summary>
        /// 返回子字符串在父字符串中第一次出现的索引
        /// </summary>
        /// <param name="text">父字符串</param>
        /// <param name="findStr">子字符串</param>
        /// <param name="findNextI">开始搜索位置, 起始索引从0开始</param>
        /// <param name="isCase">是否区分大小写</param>
        /// <returns></returns>
        public static int getCharsIndexOf(string text, string findStr, int findNextI, bool isCase) {
            int index = 0;
            // 判断匹配大小写
            if(isCase) { 
                // 匹配第一个出现的索引
                index = text.IndexOf(findStr, findNextI);
            } else {
                // 匹配第一个出现的索引
                index = Strings.InStr(findNextI+1,text,findStr,CompareMethod.Text) -1;
            }
            return index;
        }

        /// <summary>
        /// 判断字符串是否为中文
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool isStrChines(string str) {

            return Regex.IsMatch(str, @"[\u4e00-\u9fbb]+$");
        }
        /// <summary>
        /// 将字符串转化为驼峰形式
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type">0-大写类型 1-小写类型</param>
        /// <returns></returns>
        public static string charsToHump(string str, int type) { 
            char[] splitArr = new char[]{'_', '|', '\\', '/', ':', ','};
            // 先按换行符分割
            string[] splLine = str.Replace(" ", "").Split(new string[]{Environment.NewLine}, StringSplitOptions.None);
            // 存放每行中要变为大写的索引
            int[][] upperIndex = new int[splLine.Length][];
            char[] linestrArr = str.ToCharArray();
            // 判断要转化为大写的索引
            for(int i=0; i<splLine.Length;i++) { 
                linestrArr = splLine[i].ToCharArray();
                if(linestrArr != null && linestrArr.Length >= 3) { 
                    List<int> tempLineIndexs = new List<int>();
                    for(int j=2; j<linestrArr.Length; j++) {
                        // 判断前一个字符是否为小写并且当前字符为大写或者前一个字符存在于splitArr中
                        if(Char.IsUpper(linestrArr[j]) && Char.IsLower(linestrArr[j-1])
                            || splitArr.Contains(linestrArr[j-1])) { 
                            tempLineIndexs.Add(j);
                        }
                    }
                    upperIndex[i] = tempLineIndexs.ToArray();
                }  
            }
            
            StringBuilder retStr = new StringBuilder();
            // 遍历行
            for (int i=0; i<splLine.Length; i++) { 
                // 每行的小写形式
                string s = splLine[i].ToLower();
                if(s.Length > 0) { 
                    // 每行要转化为大写的索引
                    int[] indexs = upperIndex[i];
                    if (type == 0) {
                        s = s.Substring(0,1).ToUpper() + s.Substring(1,s.Length-1);
                    }else if (type == 1) { 
                        s = s.Substring(0,1).ToLower() + s.Substring(1,s.Length-1);
                    }
                    // 将该转化为大写形式的地方转化为大写形式
                    if (indexs != null && indexs.Length > 0) {
                        char[] linechar = s.ToCharArray();
                        foreach(int lineIndex in indexs) { 
                            linechar[lineIndex] = Char.ToUpperInvariant(linechar[lineIndex]);
                        }
                        s = new string(linechar);
                    }

                    // 替换掉要替换的字符
                    foreach (char split in splitArr) { 
                        s = s.Replace(split.ToString(), "");
                    }
                }
                retStr.Append(s);
                // 最后一行不添加换行符
                if(i < splLine.Length-1) retStr.AppendLine();
            }
            return retStr.ToString();
        }
        /// <summary>
        /// 去除字符串数组中的每个元素中的指定匹配项
        /// </summary>
        /// <param name="strArr">源字符串数组</param>
        /// <param name="trim">要去除的匹配</param>
        /// <param name="type">去除类型(0全部 1行首 2行尾)</param>
        /// <returns></returns>
        public static string[] charsTrim(string[] strArr, string trim, int type) {
            int index = -1,starts = -1;
            switch (type) { 
                case 0:
                    for(int i = 0,len = strArr.Length; i< len; i++) { 
                        strArr[i] = repCaseText(strArr[i], trim, "", true);
                    }
                    break;
                case 1:
                    for(int i = 0,len = strArr.Length; i< len; i++) {
                        string s = strArr[i];
                        int[] indexs = getCharsIndexOf(s, trim, true);
                        for(int j = 0,len2=indexs.Length;j<len2;j++) {
                            if(j>0){
                                if(indexs[j-1]+trim.Length == indexs[j]){ 
                                    index = indexs[j];
                                } else { 
                                  break;    
                                }
                            } else { 
                                index = indexs[j];
                            }
                        }
                        if(index > -1) {
                            starts = index+trim.Length;
                            strArr[i] = s.Substring(starts, s.Length-starts);
                        }
                    }
                    break;
                case 2:
                    for(int i = 0,len = strArr.Length; i< len; i++) {
                        string s = strArr[i];
                        int[] indexs = getCharsIndexOf(s, trim, true);
                        // 反转数组
                        Array.Reverse(indexs);
                        for(int j = 0,len2=indexs.Length;j<len2;j++) {
                            if(j>0){
                                if(indexs[j-1]-trim.Length == indexs[j]){ 
                                    index = indexs[j];
                                } else { 
                                  break;    
                                }
                            } else { 
                                index = indexs[j];
                            }
                        }
                        if(index > -1) {
                            starts = index;
                            strArr[i] = s.Substring(0, starts);
                        }
                    }
                    break;
            }
            return strArr;
        }
        /// <summary>
        /// 去除尾部的换行符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string trimEndNewLine(string str) { 
            string retStr = "";
            // 去除最后一个换行符
            if(str.Length >= Environment.NewLine.Length) { 
                retStr = str.Substring(0, str.Length - Environment.NewLine.Length);    
            }
            return retStr;
        }
        /// <summary>
        /// 获取字符串包含的中文个数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int getChineseLength(string str) { 
            if(str == null) return 0;
            int retInt = 0;
            retInt = str.Length - Regex.Replace(str, @"[\u4e00-\u9fa5]","").Length;
            return retInt;
        }
    }
}
