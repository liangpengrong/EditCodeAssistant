using System;
using System.Collections.Generic;
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
        /// 文本框的默认命名
        /// </summary>
        public const string TEXTBOX_NAME_DEF = "mainTextB";
        /// <summary>
        /// 文本框的默认编码
        /// </summary>
        public static Encoding TEXTBOX_ECODING_DEF { get => Encoding.UTF8; }
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
        /// 是否触发了文件监听更改事件
        /// </summary>
        public const string IS_FILE_CHANG_EVENT = "isFileChangEvent";
    }
}
