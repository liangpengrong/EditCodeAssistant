using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Core.DefaultData.DataLibrary
{
    /// <summary>
    /// 文本框相关的全局静态数据
    /// </summary>
    public static class TextBoxDataLibcs
    { 
        /// <summary>
        /// 文本框的默认编码
        /// </summary>
        public static readonly Encoding TEXTBOX_ECODING_DEF = Encoding.UTF8;
        /// <summary>
        /// 文本框的默认自动换行
        /// </summary>
        public static readonly bool TEXTBOX_AUTO_WRAP_DEF = false;
        /// <summary>
        /// 文本框的默认字体
        /// </summary>
        public static readonly Font TEXTBOX_FONT_DEF = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
        /// <summary>
        /// 文本框默认只读状态
        /// </summary>
        public const bool TEXTBOX_READ_ONLY_DEF = false;
    }
    /// <summary>
    /// 文本框Tag数据的Key
    /// </summary>
    public static class TextBoxTagKey
    {
        /// <summary>
        /// 文本框Tag数据中的保存文件的Key
        /// </summary>
        public const string SAVE_FILE_PATH = "saveFilePath";
        /// <summary>
        /// 文本框Tag数据中文件监听的Key
        /// </summary>
        public const string TEXTBOX_TAG_KEY_FILEMONITOR = "fFileMonitor";
        /// <summary>
        /// 文本框Tag数据中文本框编码的Key
        /// </summary>
        public const string TEXTBOX_TAG_KEY_ECODING = "textEcoding";
        /// <summary>
        /// 文本框是否处于撤销状态
        /// </summary>
        public const string TEXTBOX_IS_CANCEL = "textIsCancel";
        /// <summary>
        /// 文本框是否处于恢复状态
        /// </summary>
        public const string TEXTBOX_IS_RESTORE = "textIsRestore";
        /// <summary>
        /// 文本框不缓存空
        /// </summary>
        public const string TEXTBOX_EMPTY_NOT_CACHED = "textboxEmptyNotCached";
        /// <summary>
        /// 是否触发了文件监听更改事件
        /// </summary>
        public const string IS_FILE_CHANG_EVENT = "isFileChangEvent";
    }
}
