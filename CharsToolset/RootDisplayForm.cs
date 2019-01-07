using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StaticDataLibrary;
using ComponentLibrary;
using ProSetUpForm;
using PublicMethodLibrary;
using CacheFactory;
using System.IO;
using ProgramTopMenu;
using ProgramStatusBar;
using ProgramMainTextBox;

namespace CharsToolset
{
    public partial class RootDisplayForm : Form
    {
        public RootDisplayForm()
        {
            InitializeComponent();
        }
        private TextBoxBind fromDelegate = new TextBoxBind();
        //tab容器
        private static TabControl mainTab=null;
        //标签页
        private TabPage tabPage = null;
        //文本框
        private TextBox mainTextBox = null;
        //右键菜单
        private static ContextMenuStrip textRightMenu = null;
        //顶部菜单
        private static MenuStrip topMenu = null;
        //状态栏
        private static StatusStrip strutsBar = null;

        // private static Label sizeLab = null;
        /// <summary>
        /// 窗体的启动函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootDisplayForm_Load(object sender, EventArgs e)
        {
            // 加载默认配置
            initRootForm("我的程序");
            // 将窗体加入到单例窗体工厂
            addSingletonAllForm();
            // 将控件组合到一起并添加到窗体中
            this.ControlCombination();
            // 调节窗口位置
            this.Location = FormUtislMet.middleForm(this);
        }
        /// <summary>
        /// 设置窗体的默认启动配置
        /// </summary>
        private void initRootForm(string formText)
        {
            this.Name = DefaultNameCof.rootForm;
            this.Size = new Size(840, 540);
            this.BackColor = Color.White;
            this.Text = formText;
            // 图标
            this.Icon = Properties.Resources.编辑器16x16;
            // 加载调整大小图标
            // sizeLab = addSize();
        }
        /// <summary>
        /// 将控件组合到一起并添加到窗体中
        /// </summary>
        private void ControlCombination()
        {
            /*---------------------控件实例-------------------------*/
             // 获得顶部菜单
            topMenu = initTopMenuStrip();
            // 获得文本框状态栏
            strutsBar = initTextStrtusBar();
            // 获得主Tab容器
            mainTab = this.initMainTab();
            // 获得Page
            tabPage = this.initMainTabPage(TabDataLib.pageNameDef + "_0", TabDataLib.pageText);
            // 获得右键菜单
            textRightMenu = this.initRightMenu();
            // 获得主编辑文本框
            mainTextBox = this.initEditorText(TextBoxDataLibcs.textBNameDef + "_0");
            
            // 将顶部加入窗口
            this.Controls.Add(topMenu);
            // 将状态栏加入到窗体
            this.Controls.Add(strutsBar);
            // 将文本框添加到标签中
            tabPage.Controls.Add(mainTextBox);
            mainTextBox.BringToFront();
            // 将标签加入Tab容器中
            mainTab.TabPages.Add(tabPage);
            // 将Tab容器加入窗口
            this.Controls.Add(mainTab);
        }
        /// <summary>
        /// 将窗体添加到单例窗体工厂中
        /// </summary>
        private void addSingletonAllForm() {
            // 本窗体
            FormCache.addSingletonCache(this);
        }

        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public TabControl initMainTab()
        {
            // 获取主Tab容器
            TabControl tab = SingleComponentFactory.InitSingleControl.initMainTab();
            return tab;
        }

        /// <summary>
        /// 初始化主Tab容器中的Page
        /// </summary>
        /// <param name="c">传入的确定大小用的控件</param>
        /// <param name="pageText">标签上显示的文本</param>
        /// <returns></returns>
        public TabPage initMainTabPage(string pageName, string pageText)
        {
            // 实例化一个Page
            TabPage page = new TabPage();
            // 设置Page的背景颜色为白色
            page.BackColor = Color.White;
            page.Name = pageName;
            page.Text = pageText;
            page.UseVisualStyleBackColor = true;
            // 设置Page的大小
            page.Size = new Size(mainTab.ClientSize.Width, mainTab.ClientSize.Height);
            return page;
        }


        /// <summary>
        /// 初始化主编辑文本框
        /// </summary>
        /// <param name="c">传入的确定文本框信息用的控件</param>
        /// <returns>返回该文本框</returns>
        public TextBox initEditorText(string textBName)
        {
            // 获取主编辑文本框
            TextBox textB = new MainTextBoxTemplate().mainTextBox;
            // 文本框姓名
            textB.Name = textBName;
            // 将右键菜单绑定到文本框
            textB.ContextMenuStrip = textRightMenu;
            // 设置文本框大小
            textB.Size = new Size(tabPage.ClientSize.Width, tabPage.ClientSize.Height);
            return textB;
        }

        /// <summary>
        /// 实例化文本框右键菜单
        /// </summary>
        /// <returns>返回该文本框右键菜单</returns>
        public ContextMenuStrip initRightMenu()
        {
            // 实例化右键菜单
            ContextMenuStrip textContextMenu = TextRightMenu.getTextRightMenu();
            return textContextMenu;
        }
        /// <summary>
        /// 实例化顶部菜单
        /// </summary>
        /// <returns>返回该顶部菜单</returns>
        public MenuStrip initTopMenuStrip()
        {
            MenuStrip topMenu = TopMenuContainer.getTopMenuStrip();
            return topMenu;
        }
        /// <summary>
        /// 实例化文本框状态栏
        /// </summary>
        /// <returns></returns>
        public StatusStrip initTextStrtusBar()
        {
            // 获取文本框状态栏
            StatusStrip strtusBar = TextStatusBar.getTextRightMenu();
            return strtusBar;
        }

        /// <summary>
        /// 要在顶层与非顶层直接切换的窗体
        /// </summary>
        /// <returns></returns>
        private Form[] toTopListAdd()
        {
            //定义要判断顶层和非顶层的窗体集合
            List<Form> fToMostList = new List<Form>();
            // 判断窗口是否在单例窗口工厂中存在
            if(FormCache.getSingletonCache().ContainsKey(DefaultNameCof.findForm)) { 
                // 查找和替换窗体
                fToMostList.Add(FormCache.getSingletonCache()[DefaultNameCof.findForm]);
            }

            // 判断窗口是否在单例窗口工厂中存在
            if(FormCache.getSingletonCache().ContainsKey(DefaultNameCof.splitCharsForm)) { 
                // 分列窗体
                fToMostList.Add(FormCache.getSingletonCache()[DefaultNameCof.splitCharsForm]);
            }

            // 判断窗口是否在单例窗口工厂中存在
            if(FormCache.getSingletonCache().ContainsKey(DefaultNameCof.addCharsForm)) { 
                // 添加字符窗体
                fToMostList.Add(FormCache.getSingletonCache()[DefaultNameCof.addCharsForm]);
            }
            return fToMostList.ToArray();
        }
        // 窗体得到焦点事件
        private void RootDisplayForm_Activated(object sender, EventArgs e)
        {
            FormUtislMet.topFormNoFocus(true, toTopListAdd());
        }
        // 窗体失去焦点事件
        private void RootDisplayForm_Deactivate(object sender, EventArgs e)
        {
            FormUtislMet.topFormNoFocus(false, toTopListAdd());
        }
    }
}
