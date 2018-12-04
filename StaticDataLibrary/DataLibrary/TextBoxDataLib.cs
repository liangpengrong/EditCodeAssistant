using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticDataLibrary
{
    /// <summary>
    /// 文本框相关的全局静态数据
    /// </summary>
    public static class TextBoxDataLibcs
    { 
        /// <summary>
        /// 文本框的默认命名
        /// </summary>
        public const string textBNameDef = "mainTextB";
        /// <summary>
        /// 文本框的默认编码
        /// </summary>
        public static Encoding textBEcodingDef = Encoding.UTF8;
    }
    /// <summary>
    /// 文本框Tag数据的Key
    /// </summary>
    public static class TextBoxTagKey
    {
        /// <summary>
        /// 文本框Tag数据中的保存文件的Key
        /// </summary>
        public const string saveFilePath = "saveFilePath";
        /// <summary>
        /// 文本框Tag数据中的父控件的Text的Key
        /// </summary>
        public const string fControlText = "fControlText";
        /// <summary>
        /// 文本框Tag数据中文件监听的Key
        /// </summary>
        public const string fileMonitor = "fFileMonitor";
        /// <summary>
        /// 文本框Tag数据中文本框编码的Key
        /// </summary>
        public const string textEcoding = "textEcoding";
        /// <summary>
        /// 文本框是否处于撤销状态
        /// </summary>
        public const string textIsCancel = "textIsCancel";
        /// <summary>
        /// 文本框是否处于恢复状态
        /// </summary>
        public const string textIsRestore = "textIsRestore";
    }
}
