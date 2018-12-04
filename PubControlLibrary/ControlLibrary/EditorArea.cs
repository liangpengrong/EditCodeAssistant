using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllDllLoad;
using StaticDataLibrary;
using PubMethodLibrary;
namespace PubControlLibrary
{
    public partial class EditorArea : Component
    {
        public EditorArea()
        {
            InitializeComponent();
            this.mainText = this.mainTextBox;//窗体启动时将主文本框实例化
            this.textMenuStrip = new ContextMenuStrip();
            //this.textDefaultConfig(this.mainText);//窗体启动时加载文本框默认配置
        }
        private TextBox mainText;//代码里要使用的主要文本框
        private ContextMenuStrip textMenuStrip;//文本框的右键菜单
        private DLLLoad dllLoad = new DLLLoad();//加载全局DLl



        /// <summary>
        /// 文本框的默认配置，用来设置主要文本框的启动状态
        /// </summary>
        private void textDefaultConfig(TextBox t)
        {
            int leftOffset=0;//定义文本框的左偏移量
            int topOffset = 0;//定义文本框的上偏移量
            //t.ScrollBars = ScrollBars.Vertical;//设置文本框显示的滚动条类型
            t.Location = new Point(leftOffset,topOffset);//设置文本框的相对位置
            //设置文本框的大小
            t.Size = new Size(this.mainText.ClientSize.Width -1- leftOffset, this.mainText.ClientSize.Height-1 - topOffset);
            //设置文本框四周锚定到窗体
            t.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            //设置文本框的右键菜单
            ContextMenuStrip textContextMenu = this.textMenuStrip;
            textContextMenu.BackColor = Color.White;//背景色
            textContextMenu.ShowImageMargin = false;//显示图像边距
            textContextMenu.ShowItemToolTips = true;//显示信息提示
            t.ContextMenuStrip = textContextMenu;
        }
    }
}
