using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.CacheLibrary.OperateCache.TextBoxOperateCache
{
    /// <summary>
    /// 文本框缓存对象
    /// </summary>
    public class TextBoxCacheModel
    {

        /// <summary>
        /// 文本框起始选择位置
        /// </summary>
        public int SelectStart { get; set; }
        /// <summary>
        /// 文本框选中长度
        /// </summary>
        public int SelectLegth { get; set; }
        /// <summary>
        /// 文本框内容
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 修改类型
        /// </summary>
        public TextCacheTypeEnum TextType { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 文本框的Name
        /// </summary>
        public string TextBName { get; set; }
    }
}
