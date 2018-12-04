using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PubCacheArea
{
    /// <summary>
    /// 文本框缓存对象
    /// </summary>
    public class TextBoxCacheModel
    {
        private String textBName;
        private int selectStart;
        private int selectLegth;
        private String text;
        private TextCacheTypeEnum textType;
        private String createTime;
        private MouseEventArgs mouseEvent;
        private KeyEventArgs keysEvent;

        /// <summary>
        /// 文本框起始选择位置
        /// </summary>
        public int SelectStart { get => selectStart; set => selectStart = value; }
        /// <summary>
        /// 文本框选中长度
        /// </summary>
        public int SelectLegth { get => selectLegth; set => selectLegth = value; }
        /// <summary>
        /// 文本框内容
        /// </summary>
        public string Text { get => text; set => text = value; }
        /// <summary>
        /// 修改类型
        /// </summary>
        public TextCacheTypeEnum TextType { get => textType; set => textType = value; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get => createTime; set => createTime = value; }
        /// <summary>
        /// 按下的鼠标按钮
        /// </summary>
        public MouseEventArgs MouseEvent { get => mouseEvent; set => mouseEvent = value; }
        /// <summary>
        /// 按下的键盘按键
        /// </summary>
        public KeyEventArgs KeysEvent { get => keysEvent; set => keysEvent = value; }
        /// <summary>
        /// 文本框的Name
        /// </summary>
        public string TextBName { get => textBName; set => textBName = value; }
    }
}
