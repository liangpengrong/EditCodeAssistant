using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PublicMethodLibrary
{
    /// <summary>
    /// 键盘按键的工具类
    /// </summary>
    public class KeysUtil
    {
        private KeysUtil (){}

        public const int KEY_A = 65;
        public const int KEY_B = 66;
        public const int KEY_C = 67;
        public const int KEY_D = 68;
        public const int KEY_E = 69;
        public const int KEY_F = 70;
        public const int KEY_G = 71;
        public const int KEY_H = 72;
        public const int KEY_I = 73;
        public const int KEY_J = 74;
        public const int KEY_K = 75;
        public const int KEY_L = 76;
        public const int KEY_M = 77;
        public const int KEY_N = 78;
        public const int KEY_O = 79;
        public const int KEY_P = 80;
        public const int KEY_Q = 81;
        public const int KEY_R = 82;
        public const int KEY_S = 83;
        public const int KEY_T = 84;
        public const int KEY_U = 85;
        public const int KEY_V = 86;
        public const int KEY_W = 87;
        public const int KEY_X = 88;
        public const int KEY_Y = 89;
        public const int KEY_Z = 90;
        public const int KEY_0 = 48;
        public const int KEY_1 = 49;
        public const int KEY_2 = 50;
        public const int KEY_3 = 51;
        public const int KEY_4 = 52;
        public const int KEY_5 = 53;
        public const int KEY_6 = 54;
        public const int KEY_7 = 55;
        public const int KEY_8 = 56;
        public const int KEY_9 = 57;
        public const int KEY_X_1 = 96;
        public const int KEY_X_2 = 97;
        public const int KEY_X_3 = 98;
        public const int KEY_X_4 = 99;
        public const int KEY_X_5 = 100;
        public const int KEY_X_6 = 101;
        public const int KEY_X_7 = 102;
        public const int KEY_X_8 = 103;
        public const int KEY_X_9 = 104;
        public const int KEY_X_0 = 105;
        public const int KEY_X_星 = 106;
        public const int KEY_X_加 = 107;
        public const int KEY_X_Enter = 108;
        public const int KEY_X_减 = 109;
        public const int KEY_X_点 = 110;
        public const int KEY_X_除 = 111;
        public const int KEY_F1 = 112;
        public const int KEY_F2 = 113;
        public const int KEY_F3 = 114;
        public const int KEY_F4 = 115;
        public const int KEY_F5 = 116;
        public const int KEY_F6 = 117;
        public const int KEY_F7 = 118;
        public const int KEY_F8 = 119;
        public const int KEY_F9 = 120;
        public const int KEY_F10 = 121;
        public const int KEY_F11 = 122;
        public const int KEY_F12 = 123;
        public const int KEY_F13 = 124;
        public const int KEY_F14 = 125;
        public const int KEY_F15 = 126;
        public const int KEY_Backspace = 8;
        public const int KEY_Tab = 9;
        public const int KEY_Clear = 12;
        public const int KEY_Enter = 13;
        public const int KEY_Shift = 16;
        public const int KEY_Control = 17;
        public const int KEY_Alt = 18;
        public const int KEY_CapsLock = 20;
        public const int KEY_Esc = 27;
        public const int KEY_空格键 = 32;
        public const int KEY_PageUp = 33;
        public const int KEY_PageDown = 34;
        public const int KEY_End = 35;
        public const int KEY_Home = 36;
        public const int KEY_左箭头 = 37;
        public const int KEY_向上箭头 = 38;
        public const int KEY_右箭头 = 39;
        public const int KEY_向下箭头 = 40;
        public const int KEY_Insert = 45;
        public const int KEY_Delete = 46;
        public const int KEY_Help = 47;
        public const int KEY_NumLock = 144;
        public const int KEY_除 = 191;
        public const int KEY_飘过 = 192;
        public const int KEY_大括号 = 219;
        public const int KEY_反斜杠 = 220;
        public const int KEY_反大括号 = 221;

        /// <summary>
        /// 将传入的字符串按照指定的分隔符分割并转化为Int[]
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public static int[] strCassKeyValue(string str, char sp) {
            int[] retIntArr = null;
            string[] strArr = str.Split(sp);
            retIntArr = new int[strArr.Length];
            for(int i = 0,len = strArr.Length; i < len; i++) {
                string s = strArr[i];
                retIntArr[i] = Convert.ToInt32(s);
            }
            return retIntArr;
        }
   }
}
