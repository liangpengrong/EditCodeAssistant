using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StaticDataLibrary;
using PublicMethodLibrary;
using ComponentLibrary;

namespace SingleComponentLibrary
{
    /// <summary>
    /// 编辑文本框
    /// </summary>
    public partial class MainTextBoxTemplate : UserControl
    {
        public MainTextBoxTemplate()
        {
            InitializeComponent();
            // 窗体启动时将主文本框实例化
            this.mainText = initEditorText("asd");
            // 窗体启动时加载文本框默认配置
            // this.textDefaultConfig(this.mainText);
        }
        // 代码里要使用的主要文本框
        private TextBox mainText;
        /// <summary>
        /// 初始化主编辑文本框
        /// </summary>
        /// <param name="c">传入的确定文本框信息用的控件</param>
        /// <returns>返回该文本框</returns>
        private TextBox initEditorText(string textBName)
        {
            // 获取主编辑文本框
            TextBox textB = mainTextBox;
            TextBoxBind fromDelegate = new TextBoxBind();
            // 文本框姓名
            textB.Name = textBName;
            // 绑定文本改变事件
            textB.TextChanged += new EventHandler(fromDelegate.mainTextBoxChanged);
            // 绑定鼠标移过事件
            textB.MouseMove += new MouseEventHandler(fromDelegate.mainTextBoxMouseMove);
            // 绑定鼠标在组件内并释放按键事件
            textB.MouseUp += new MouseEventHandler(fromDelegate.mainTextBoxMouseUp);
            // 绑定鼠标在组件内并按下鼠标事件
            textB.MouseDown += new MouseEventHandler(fromDelegate.mainTextBoxMouseDown);
            // 绑定键盘按下事件
            textB.KeyDown += new KeyEventHandler(fromDelegate.mainTextBoxKeyDown);
            // 绑定键盘松开事件
            textB.KeyUp += new KeyEventHandler(fromDelegate.mainTextBoxKeyUp);
            // 绑定控件启用事件
            textB.Enter += new EventHandler(fromDelegate.mainTextBoxEnter);
            // 绑定控件获得焦点事件
            textB.GotFocus += new EventHandler(fromDelegate.mainTextBoxGotFocus);

            textB.TabStop = true;
            this.DoubleBuffered = true;
            // 设置文本框的相对位置
            textB.Location = new Point(1,1);
            // 将文件默认编码写入到文本框tag数据中
            TextBoxUtilsMet.textAddTag(textB, TextBoxTagKey.textEcoding, TextBoxDataLibcs.textBEcodingDef);
            // 设置文本框四周锚定
            textB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            return textB;
        }
        /// <summary>
        /// 文本框的默认配置，用来设置主要文本框的启动状态
        /// </summary>
        private void textDefaultConfig(TextBox t)
        {
            int leftOffset=0;//定义文本框的左偏移量
            int topOffset = 0;//定义文本框的上偏移量
            t.Location = new Point(leftOffset,topOffset);//设置文本框的相对位置
            //设置文本框的大小
            t.Size = new Size(this.mainText.ClientSize.Width -1- leftOffset, this.mainText.ClientSize.Height-1 - topOffset);
            //设置文本框四周锚定到窗体
            t.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            //设置文本框的右键菜单
            ContextMenuStrip textContextMenu = TextRightMenu.getTextRightMenu();
            // 背景色
            textContextMenu.BackColor = Color.White;
            // 显示图像边距
            textContextMenu.ShowImageMargin = false;
            // 显示信息提示
            textContextMenu.ShowItemToolTips = true;
            t.ContextMenuStrip = textContextMenu;
        }
    }
}
