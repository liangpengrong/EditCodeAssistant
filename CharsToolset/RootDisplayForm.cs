using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ProgramTextBoxLibrary;
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.FormCache;
using UI.ToolStripContainerLibrary;
using UI.TabContentLibrary.MainTabContent;
using UI.ComponentLibrary;
using UI.ComponentLibrary.ControlLibrary.RightMenu;
using UI_TopMenuBar;
using UI.StatusBarLibrary;
using UI.ComponentLibrary.ControlLibrary;
using Core.DefaultData.DataLibrary;
using Core_Config.ConfigData.FormConfig;

namespace CharsToolset
{
    public partial class RootDisplayForm : Form
    {
        // 状态栏默认背景色
        private Color formBackColor = ColorTranslator.FromHtml("#fff");
        public RootDisplayForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体的启动函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootDisplayForm_Load(object sender, EventArgs e)
        {
            // 加载默认配置
            initRootForm();
            // 将窗体加入到单例窗体工厂
            addSingletonAllForm();
            // 将控件组合到一起并添加到窗体中
            ControlCombination();
            // 调节窗口位置
            Location = FormUtislMet.middleForm(this);
        }
        /// <summary>
        /// 设置窗体的默认启动配置
        /// </summary>
        private void initRootForm()
        {
            this.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.ROOT_FORM_NAME);
            this.Size = RootFormCongfig.ROOT_SIZE;
            this.BackColor = Color.White;
            this.Text = RootFormDataLib.ROOT_FORM_TEXT;
            this.BackColor = formBackColor;
            // 图标
            this.Icon = Properties.Resources.编辑器适配;
        }
        /// <summary>
        /// 将控件组合到一起并添加到窗体中
        /// </summary>
        private void ControlCombination()
        {
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            /*---------------------控件实例-------------------------*/
            // 获得添加标签按钮
            Panel addPageBut = initAddPageButton();
            // 获得顶部菜单
            MenuStrip topMenu = initTopMenuStrip();
            // 获得主容器
            ToolStripContainer stripContainer = initMainContainer();
            // 获得文本框状态栏
            StatusStrip strutsBar = initTextStrtusBar();
            // 获得主Tab容器
            TabControl mainTab = initMainTab();
            // 获得Page
            TabPage tabPage = initMainTabPage();
            // 获得右键菜单
            ContextMenuStrip textRightMenu = initRightMenu();
            // 获得主编辑文本框
            TextBox mainTextBox = initEditorText();
            initFormLayout(this, topMenu, strutsBar, stripContainer, mainTab, tabPage, addPageBut, mainTextBox, textRightMenu);
        }
        /// <summary>
        /// 将窗体添加到单例窗体工厂中
        /// </summary>
        private void addSingletonAllForm() {
            // 本窗体
            FormCacheFactory.addSingletonCache(this);
        }
        /// <summary>
        /// 初始化主容器
        /// </summary>
        /// <returns></returns>
        public static ToolStripContainer initMainContainer() { 
            ToolStripContainer toolStripContainer = MainToolStripContainer.initToolStripContainer();
            return toolStripContainer;
        }
        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public static TabControl initMainTab()
        {
            // 获取主Tab容器
            TabControl tab = MainTabContent.initMainTab();
            return tab;
        }

        /// <summary>
        /// 初始化主Tab容器中的Page
        /// </summary>
        /// <param name="c">传入的确定大小用的控件</param>
        /// <param name="pageText">标签上显示的文本</param>
        /// <returns></returns>
        public static TabPage initMainTabPage()
        {
            // 实例化一个Page
            TabPage page = MainTabContent.initMainTabPage();
            return page;
        }
        /// <summary>
        /// 初始化单例模式下的添加标签按钮
        /// </summary>
        /// <returns></returns>
        public static Panel initAddPageButton() { 
            Panel but = AddPageButton.initSingleMainAddPageButton();
            but.MouseClick += (object sender, MouseEventArgs e) =>{ 
                Panel panel = (Panel)sender;
                if(MouseButtons.Left.Equals(e.Button)) { 
                    addPageDefMethod(panel);
                }
            };
            return but;
        }

        /// <summary>
        /// 初始化主编辑文本框
        /// </summary>
        /// <param name="c">传入的确定文本框信息用的控件</param>
        /// <returns>返回该文本框</returns>
        public static TextBox initEditorText()
        {
            // 获取主编辑文本框
            TextBox textB = MainTextBoxTemplate.getMainTextBox();
            return textB;
        }

        /// <summary>
        /// 实例化文本框右键菜单
        /// </summary>
        /// <returns>返回该文本框右键菜单</returns>
        public static ContextMenuStrip initRightMenu()
        {
            // 实例化右键菜单
            ContextMenuStrip textContextMenu = TextRightMenu.getSingleTextRightMenu();
            return textContextMenu;
        }
        /// <summary>
        /// 实例化顶部菜单
        /// </summary>
        /// <returns>返回该顶部菜单</returns>
        public static MenuStrip initTopMenuStrip()
        {
            MenuStrip topMenu = TopMenuContainer.getTopMenuStrip();
            return topMenu;
        }
        /// <summary>
        /// 实例化文本框状态栏
        /// </summary>
        /// <returns></returns>
        public static StatusStrip initTextStrtusBar()
        {
            // 获取文本框状态栏
            StatusStrip strtusBar = TextStatusBar.getToolStripStatus();
            return strtusBar;
        }
        /// <summary>
        /// 初始化窗体的布局
        /// </summary>
        /// <param name="form">主窗体</param>
        /// <param name="topMenu">顶部菜单</param>
        /// <param name="strtusBar">状态栏</param>
        /// <param name="container">主容器</param>
        /// <param name="tab">tab容器</param>
        /// <param name="page">page页</param>
        /// <param name="text">文本框</param>
        /// <param name="textStrip">文本框的右键菜单</param>
        public static void initFormLayout(Form form, MenuStrip topMenu, StatusStrip strtusBar, ToolStripContainer container,
            TabControl tab, TabPage page,Control addpageBut, TextBox textBox, ContextMenuStrip textStrip) {
            /*==============设置控件的tab顺序======================*/
            textBox.TabIndex = 0;
            topMenu.TabIndex = 1;
            container.TabIndex = 2;
            tab.TabIndex = 3;
            textBox.TabIndex = 4;
            strtusBar.TabIndex = 5;
            /*==============将文本框添加到标签中======================*/
            page.Controls.Add(textBox);
            textBox.Size = page.ClientSize;
            textBox.Location = new Point(1,1);
            // 绑定文本框右键菜单
            textBox.ContextMenuStrip = textStrip;

            /*==============将标签加入Tab容器中======================*/
            tab.TabPages.Add(page);
            page.Size = tab.ClientSize;

            /*==============将Tab容器加入到主容器======================*/
            container.LeftToolStripPanelVisible = false;
            container.TopToolStripPanelVisible = true;
            container.RightToolStripPanelVisible = false;
            container.BottomToolStripPanelVisible = false;

            container.ContentPanel.Controls.Add(tab);
            tab.Location = new Point(0, 0);
            tab.Size = new Size(container.ContentPanel.ClientSize.Width, 
                container.ContentPanel.ClientSize.Height - tab.Location.Y - container.BottomToolStripPanel.Height);
            /*==============将添加标签按钮加入到主容器======================*/
            container.ContentPanel.Controls.Add(addpageBut);
            addpageBut.BringToFront();

            /*==============将顶部加入到窗体======================*/
            form.Controls.Add(topMenu);
            topMenu.Width = form.ClientSize.Width;
            topMenu.Location = new Point(0,0);

            /*==============将状态栏加入到窗体中======================*/
            form.Controls.Add(strtusBar);
            strtusBar.Width = form.ClientSize.Width;

            /*==============将主容器加入到窗体======================*/
            form.Controls.Add(container);
            container.Location = new Point(1, topMenu.Location.Y+topMenu.Height);
            container.Width = form.ClientSize.Width-0;
            container.Height = form.ClientSize.Height - container.Location.Y - strtusBar.Height;
            /*==============将一个按钮加入到窗体，为了防止page页点击出现内边框======================*/
            Button bbb = new Button();
            bbb.TabIndex = int.MaxValue;
            bbb.TabStop = false;
            bbb.Size = new Size(1,1);
            bbb.SendToBack();
            form.Controls.Add(bbb);
            // 确定添加按钮的位置
            MainTabControlUtils.doIsAddPageButLocation(addpageBut, tab);
            // 使文本框获取焦点
            form.ActiveControl = textBox;
        }
        /// <summary>
        /// 添加新标签的方法
        /// </summary>
        /// <param name="addBut"></param>
        public static void addPageDefMethod(Control addBut) {
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            // 获得右键菜单
            ContextMenuStrip textRightMenu = initRightMenu();
            // 获得主编辑文本框
            TextBox mainTextBox = initEditorText();
            MainTabContent.addControlsToPage(mainTextBox, true, true);
            mainTextBox.Location = new Point(1,1);
            mainTextBox.ContextMenuStrip = textRightMenu;
        }

        // 窗体得到焦点事件
        private void RootDisplayForm_Activated(object sender, EventArgs e)
        {
            FormUtislMet.topFormNoFocus(true, FormCacheFactory.getTopFormCahce().Values.ToArray());
        }
        // 窗体失去焦点事件
        private void RootDisplayForm_Deactivate(object sender, EventArgs e)
        {
            FormUtislMet.topFormNoFocus(false, FormCacheFactory.getTopFormCahce().Values.ToArray());
        }
    }
}
