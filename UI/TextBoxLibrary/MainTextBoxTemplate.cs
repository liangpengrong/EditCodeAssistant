using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary;
using UI.ControlEventBindLibrary.TextBoxEventBind;

namespace ProgramTextBoxLibrary
{
    /// <summary>
    /// 编辑文本框
    /// </summary>
    public partial class MainTextBoxTemplate
    {
        private MainTextBoxTemplate(){}
        /// <summary>
        /// 初始化主编辑文本框
        /// </summary>
        /// <param name="c">传入的确定文本框信息用的控件</param>
        /// <returns>返回该文本框</returns>
        private static TextBox initEditorText()
        {
            // 获取主编辑文本框
            RedrawTextBox textB = new RedrawTextBox();
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
            MainTextBoxBind fromDelegate = new MainTextBoxBind();
            // 绑定文本改变事件
            textB.TextChanged += fromDelegate.textBoxChanged;
            // 绑定鼠标移过事件
            textB.MouseMove += fromDelegate.textBoxMouseMove;
            // 绑定鼠标在组件内并释放鼠标按键事件
            textB.MouseUp += fromDelegate.textBoxMouseUp;
            // 绑定鼠标在组件内并按下鼠标事件
            textB.MouseDown += fromDelegate.textBoxMouseDown;
            // 鼠标移入事件
            textB.MouseEnter += fromDelegate.textBoxMouseEnter;
            // 绑定键盘按下事件
            textB.KeyDown += fromDelegate.textBoxKeyDown;
            // 绑定键盘松开事件
            textB.KeyUp += fromDelegate.textBoxKeyUp;
            // 绑定控件启用事件
            textB.Enter += fromDelegate.textBoxEnter;
            // 绑定控件获得焦点事件
            textB.GotFocus += fromDelegate.textBoxGotFocus;
        }
        /// <summary>
        /// 文本框的默认配置，用来设置主要文本框的启动状态
        /// </summary>
        private static void textDefaultConfig(RedrawTextBox textB) {
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            // 文本框姓名
            textB.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF) + timeStr;
            textB.TabStop = true;
            textB.AllowDrop = true;
            textB.BorderStyle = BorderStyle.None;
            textB.Font = MainTextBConfig.TEXTBOX_FONT;
            textB.ReadOnly = TextBoxDataLibcs.TEXTBOX_READ_ONLY_DEF;
            textB.HideSelection = false;
            textB.Location = new Point(0, 0);
            textB.MaxLength = 999999999;
            textB.Multiline = true;
            textB.ScrollBars = ScrollBars.Both;
            textB.Size = new Size(289, 254);
            textB.TabIndex = 0;
            textB.WordWrap = MainTextBConfig.AUTO_WORDWRAP;
            textB.TextPadding = new Padding(3,3,4,4);
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
        public static TextBox getMainTextBox() { 
            TextBox textBox = initEditorText();
            return textBox;
        }
    }
}
