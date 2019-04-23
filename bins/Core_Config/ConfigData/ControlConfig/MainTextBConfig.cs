using Core.DefaultData.DataLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Config.ConfigData.ControlConfig {
    public class MainTextBConfig {
        /// <summary>
        /// 自动换行
        /// </summary>
        public static bool AUTO_WORDWRAP = TextBoxDataLibcs.TEXTBOX_AUTO_WRAP_DEF;
        /// <summary>
        /// 文本框的默认字体
        /// </summary>
        public static Font TEXTBOX_FONT = TextBoxDataLibcs.TEXTBOX_FONT_DEF;
    }
}
