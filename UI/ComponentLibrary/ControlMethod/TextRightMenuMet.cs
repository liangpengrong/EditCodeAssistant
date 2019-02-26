using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.ComponentLibrary.ControlMethod {
    public class TextRightMenuMet {
        public static object 全选ItemMethod(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textAllSelect(t);
            return null;
        }
        public static object 剪切ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textSelectCut(t);
            return null;
        }
        public static object 复制ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textCopy(t);
            return null;
        }
        public static object 粘贴ItemMethod(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textPaste(t);
            return null;
        }
        public static object 删除ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textSelectDelect(t);
            return null;
        }
        public static object 全部空格ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textDelSpace(t);
            return null;
        }
        public static object 行首空格ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textDelRowFirstSpace(t);
            return null;
        }
        public static object 行尾空格ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textDelRowTailSpace(t);
            return null;
        }
        public static object 空行ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textDelBlankLine(t);
            return null;
        }
        public static object 换行符ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textPlaceNewline(t);
            return null;
        }
        public static object 制表符ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textPlaceTabs(t);
            return null;
        }
        public static object 清空文本框ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textClear(t);
            return null;
        }
        public static object 大写形式_全部_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToUpper(t, 0);
            return null;
        }
        public static object 大写形式_行首_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToUpper(t, 1);
            return null;
        }
        public static object 大写形式_行尾_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToUpper(t, 2);
            return null;
        }
        public static object 大写形式_自定义_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            
            return null;
        }

        public static object 小写形式_全部_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToLower(t, 0);
            return null;
        }
        public static object 小写形式_行首_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToLower(t, 1);
            return null;
        }
        public static object 小写形式_行尾_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToLower(t, 2);
            return null;
        }
        public static object 小写形式_自定义_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            
            return null;
        }

        public static object 驼峰形式_大写_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToHump(t, 0);
            return null;
        }
        public static object 驼峰形式_小写_ItemMethod(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            TextBoxUtilsMet.textToHump(t, 1);
            return null;
        }
    }
}
