using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;
using UI.ControlEventBindLibrary.TextBoxEventBind;

namespace ProgramTextBoxLibrary {
    /// <summary>
    /// 编辑文本框
    /// </summary>
    public partial class CacheTextBoxTemplate
    {
        private CacheTextBoxTemplate(){}
        /// <summary>
        /// 初始化主编辑文本框
        /// </summary>
        /// <param name="c">传入的确定文本框信息用的控件</param>
        /// <returns>返回该文本框</returns>
        private static TextBox initEditorText()
        {
            // 获取主编辑文本框
            TextBox textB = new TextBox();
            // 文本框默认事件绑定
            textDefaultEventBinding(textB);
            // 文本框的默认配置
            textDefaultConfig(textB);
            return textB;
        }
        /// <summary>
        /// 文本框的默认事件绑定
        /// </summary>
        private static void textDefaultEventBinding(TextBox textB) { 
            CacheTextBoxBind fromDelegate = new CacheTextBoxBind();
            // 绑定文本改变事件
            textB.TextChanged += fromDelegate.mainTextBoxChanged;
            // 绑定鼠标移过事件
            textB.MouseMove += fromDelegate.mainTextBoxMouseMove;
            // 绑定鼠标在组件内并释放鼠标按键事件
            textB.MouseUp += fromDelegate.mainTextBoxMouseUp;
            // 绑定鼠标在组件内并按下鼠标事件
            textB.MouseDown += fromDelegate.mainTextBoxMouseDown;
            // 鼠标移入事件
            textB.MouseEnter += fromDelegate.mainTextBoxMouseEnter;
            // 绑定键盘按下事件
            textB.KeyDown += fromDelegate.mainTextBoxKeyDown;
            // 绑定键盘松开事件
            textB.KeyUp += fromDelegate.mainTextBoxKeyUp;
            // 绑定控件启用事件
            textB.Enter += fromDelegate.mainTextBoxEnter;
            // 绑定控件获得焦点事件
            textB.GotFocus += fromDelegate.mainTextBoxGotFocus;
        }
        /// <summary>
        /// 文本框的默认配置，用来设置主要文本框的启动状态
        /// </summary>
        private static void textDefaultConfig(TextBox textB) {
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            // 文本框姓名
            textB.Name = TextBoxDataLibcs.TEXTBOX_NAME_DEF + timeStr;
            textB.TabStop = true;
            textB.AllowDrop = true;
            textB.BorderStyle = BorderStyle.None;
            textB.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            textB.HideSelection = false;
            textB.Location = new Point(0, 0);
            textB.MaxLength = 999999999;
            textB.Multiline = true;
            textB.ScrollBars = ScrollBars.Both;
            textB.Size = new Size(289, 254);
            textB.TabIndex = 0;
            textB.WordWrap = false;
            // 将文件默认编码写入到文本框tag数据中
            TextBoxUtilsMet.textAddTag(textB, TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, TextBoxDataLibcs.TEXTBOX_ECODING_DEF);
            // 设置文本框四周锚定
            textB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            // 消除控件重绘闪烁
            ControlsUtilsMet.clearRedrawFlashing(textB);
        }
        /// <summary>
        /// 获取主文本框
        /// </summary>
        /// <param name="name">文本框姓名</param>
        /// <returns></returns>
        public static TextBox getCacheTextBox() { 
            TextBox textBox = initEditorText();
            return textBox;
        }
    }
}
