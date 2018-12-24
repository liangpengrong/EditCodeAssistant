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
using PubControlLibrary;
using ProSetUpForm;
using PubMethodLibrary;
using PubCacheArea;
using PubControlLibrary.ControlLibrary;
using System.IO;

namespace CharsToolset
{
    public partial class RootDisplayForm : Form
    {
        public RootDisplayForm()
        {
            InitializeComponent();
        }
        //实例化全局DLL加载类
        private DLLLoad dllLoad = new DLLLoad();
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
            // 将控件组合到一起并添加到窗体中
            this.ControlCombination();
            // 加载默认配置
            initRootForm("我的程序");
            // 将窗体加入到单例窗体工厂
            addSingletonAllForm();
            // 将控件加入到单例控件工厂
            addSingletonAllCon();
            // 调节窗口位置
            this.Location = FormUtislMet.middleForm(this);
        }
        /// <summary>
        /// 设置窗体的默认启动配置
        /// </summary>
        private void initRootForm(String formText)
        {
            this.Size = new Size(840, 540);
            this.BackColor = Color.White;
            this.Text = formText;
            // 图标
            this.Icon = Properties.Resources.编辑器32x32;
            // 加载调整大小图标
            // sizeLab = addSize();
        }
        /// <summary>
        /// 添加调整大小角标
        /// </summary>
        private Label addSize() { 
            Label lab = new Label();
            lab.Name = DefaultNameCof.sizeSubscript;
            lab.Size = new Size(9,9);
            lab.AutoSize = false;
            lab.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            lab.Image = Properties.Resources.调整大小;
            lab.Text = "";
            lab.BringToFront();
            lab.Location = new Point(this.ClientSize.Width - lab.Width - 2
                , this.ClientSize.Height - lab.Height - 2);
            this.Controls.Add(lab);
            lab.BringToFront();
            lab.Cursor = Cursors.SizeNWSE;
            return lab;
        }
        /// <summary>
        /// 将控件组合到一起并添加到窗体中
        /// </summary>
        private void ControlCombination()
        {
            /*---------------------控件实例-------------------------*/
             // 获得菜单
            topMenu = this.initTopMenuStrip();
            // 获得文本框状态栏
            strutsBar = this.initTextStrtusBar();
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
            FormCache.addSingletonFac(this);
        }
        /// <summary>
        /// 将控件添加到单例控件工厂中
        /// </summary>
        private void addSingletonAllCon() {
            // 控件容器
            ControlCache.addSingletonFac(mainTab);
            // 文本框右键菜单
            ControlCache.addSingletonFac(textRightMenu);
            // 顶部菜单
            ControlCache.addSingletonFac(topMenu);
            // 状态栏
            ControlCache.addSingletonFac(strutsBar);
            // 调整大小角标
            // ControlCache.addSingletonFac(sizeLab);
        }

        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public TabControl initMainTab()
        {
            // 获取主Tab容器
            TabControl tab =new TabControl();
            // Name
            tab.Name = DefaultNameCof.tabContent;
            // 字体
            tab.Font = new Font("微软雅黑",8,FontStyle.Regular);
            // 设置不被焦点选中
            tab.TabStop = false;
            //设置主Tab容器的四周锚定
            tab.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //设置主Tab容器标签的宽高
            tab.ItemSize = new Size(100,20);
            // 选项卡方式
            tab.SizeMode = TabSizeMode.Fixed;
            // 显示工具提示
            tab.ShowToolTips = true;
            // Tab容器宽
            tab.Width = this.ClientSize.Width;
            // Tab容器高
            tab.Height = this.ClientSize.Height-1 - topMenu.Height - strutsBar.Height;;
            // Tab容器相对于窗体的位置
            tab.Location = new Point(1, 1 + topMenu.Height);
            return tab;
        }

        /// <summary>
        /// 初始化主Tab容器中的Page
        /// </summary>
        /// <param name="c">传入的确定大小用的控件</param>
        /// <param name="pageText">标签上显示的文本</param>
        /// <returns></returns>
        public TabPage initMainTabPage(String pageName, String pageText)
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
        public TextBox initEditorText(String textBName)
        {
            TextBox textB = new EditorArea().mainTextBox;//获取主编辑文本框
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

            // 将右键菜单绑定到文本框
            textB.ContextMenuStrip = textRightMenu;
            this.DoubleBuffered = true;
            // 设置文本框的相对位置
            textB.Location = new Point(1,1);
            // 将文件默认编码写入到文本框tag数据中
            TextBoxUtilsMet.textAddTag(textB, TextBoxTagKey.textEcoding, TextBoxDataLibcs.textBEcodingDef);
            // 设置文本框大小
            textB.Size = new Size(tabPage.ClientSize.Width, tabPage.ClientSize.Height);
            // 设置文本框四周锚定
            textB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //将文本框的父控件的text写入到文本框的tag中
            TextBoxUtilsMet.textAddTag(textB, TextBoxTagKey.fControlText, tabPage.Text);
            return textB;
        }

        /// <summary>
        /// 实例化文本框右键菜单
        /// </summary>
        /// <returns>返回该文本框右键菜单</returns>
        public ContextMenuStrip initRightMenu()
        {
            // 实例化右键菜单
            ContextMenuStrip textContextMenu = new ControlRightMenu().rightMenuStrip;
            // 设置不具有Tab焦点
            textContextMenu.TabStop = false;
            // 字体
            textContextMenu.Font = new Font("微软雅黑",9,FontStyle.Regular);
            //不显示图像边距
            textContextMenu.ShowImageMargin = true;
            //不显示选中边距
            textContextMenu.ShowCheckMargin = false;
            //显示信息提示
            textContextMenu.ShowItemToolTips = true;
            // 使用自定义的样式
            textContextMenu.Renderer = new MyToolStripRenderer();
            return textContextMenu;
        }
        /// <summary>
        /// 实例化顶部菜单
        /// </summary>
        /// <returns>返回该顶部菜单</returns>
        public MenuStrip initTopMenuStrip()
        {
            MenuStrip topMenu = new TopMenuContainer(this).topMenuStrip;
            topMenu.TabStop = false;
            // 设置大小
            topMenu.Size = new Size(this.Size.Width,10);
            // 设置停靠到顶端
            topMenu.Dock = DockStyle.Top;
            // 设置相对距离
            topMenu.Location = new Point(0,0);
            // 设置背景颜色
            topMenu.BackColor = Color.White;
            // 绑定重绘函数
            topMenu.Paint += new PaintEventHandler(TopMenuContainer.paintMenuFrame);
            // 字体
            topMenu.Font = new Font("微软雅黑",9,FontStyle.Regular);

            topMenu.Renderer =  new MyToolStripRenderer();
            return topMenu;
        }
        /// <summary>
        /// 实例化文本框状态栏
        /// </summary>
        /// <returns></returns>
        public StatusStrip initTextStrtusBar()
        {
            // 获取TextStatusBar实例
            TextStatusBar textStatusBar = new TextStatusBar(this);
            // 获取文本框状态栏
            StatusStrip strtusBar = textStatusBar.statusStrip;
            strtusBar.AutoSize = false;
            strtusBar.Height = 20;
            strtusBar.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
            strtusBar.BackColor = Color.White;
            strtusBar.Dock = DockStyle.Bottom;
            // 设置右键菜单
            strtusBar.ContextMenuStrip = textStatusBar.getStrutsBarRightMenu(strtusBar);
            // 绑定状态栏的重绘事件
            // strtusBar.Paint += new PaintEventHandler(TextStatusBar.paintStrutsBarFrame);
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
            if(FormCache.getSingletonFactory().ContainsKey(DefaultNameCof.findForm)) { 
                // 查找和替换窗体
                fToMostList.Add(FormCache.getSingletonFactory()[DefaultNameCof.findForm]);
            }

            // 判断窗口是否在单例窗口工厂中存在
            if(FormCache.getSingletonFactory().ContainsKey(DefaultNameCof.splitCharsForm)) { 
                // 分列窗体
                fToMostList.Add(FormCache.getSingletonFactory()[DefaultNameCof.splitCharsForm]);
            }

            // 判断窗口是否在单例窗口工厂中存在
            if(FormCache.getSingletonFactory().ContainsKey(DefaultNameCof.addCharsForm)) { 
                // 添加字符窗体
                fToMostList.Add(FormCache.getSingletonFactory()[DefaultNameCof.addCharsForm]);
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
