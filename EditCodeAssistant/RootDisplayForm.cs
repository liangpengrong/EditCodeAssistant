using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.FormCache;
using Core.DefaultData.DataLibrary;
using Core_Config.ConfigData.FormConfig;
using UI.ComponentLibrary.MethodLibrary.Util;
using UI;
using UI_TopMenuBar;

namespace EditCodeAssistant {
    public partial class RootDisplayForm : Form {
        private string[] loadPath = null;
        public RootDisplayForm() {
            InitializeComponent();
        }
        public RootDisplayForm(string[] loadPath) {
            InitializeComponent();
            this.loadPath = loadPath;
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
            controlCombination();
            // 打开拖入的文件到新标签中
            loadOpenFile();
            // 调节窗口位置
            Location = FormUtisl.MiddleForm(this);
        }
        /// <summary>
        /// 设置窗体的默认启动配置
        /// </summary>
        private void initRootForm()
        {
            this.Name = EnumUtils.GetDescription(DefaultNameEnum.ROOT_FORM_NAME);
            this.Size = RootFormCongfig.ROOT_SIZE;
            this.BackColor = Color.White;
            this.Text = RootFormDataLib.ROOT_FORM_TEXT;
            this.IsMdiContainer = false;
            // 图标
            this.Icon = Properties.Resources.编辑器适配;
        }
        /// <summary>
        /// 将控件组合到一起并添加到窗体中
        /// </summary>
        private void controlCombination() {
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            /*---------------------控件实例-------------------------*/
            // 获得顶部菜单
            MenuStrip topMenu = initTopMenuStrip();
            // 获得主容器
            ToolStripContainer stripContainer = initMainContainer();
            // 获得文本框状态栏
            StatusStrip strutsBar = initMainStrtusBar();
            // 获得主Tab容器
            TabControl mainTab = initMainTab();
            initFormLayout(this, topMenu, strutsBar, stripContainer, mainTab);
        }
        // 窗口加载时判断是否有拖动到上面的文件
        private void loadOpenFile() {
          if(loadPath != null && loadPath.Length>0) {
              foreach (string p in loadPath) {// 遍历路径
                 FileUtils.SetTextBoxValByPath(MainTabControlUtils.GetNewPageTextBox(), p, Encoding.UTF8);
              }
          }
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
        private ToolStripContainer initMainContainer() { 
            Control ccc = UIComponentFactory.getSingleControl(DefaultNameEnum.MAIN_CONTAINER, true);
            ToolStripContainer container = null;
            if(ccc != null && ccc is ToolStripContainer) container = (ToolStripContainer)ccc;
            return container;
        }
        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        private TabControl initMainTab() {
            // 获取主Tab容器
            TabControl tab = null;
            Control ccc = UIComponentFactory.getSingleControl(DefaultNameEnum.TAB_CONTENT, true);
            if(ccc != null && ccc is TabControl) tab = (TabControl)ccc;
            return tab;
        }

        /// <summary>
        /// 实例化顶部菜单
        /// </summary>
        /// <returns>返回该顶部菜单</returns>
        private MenuStrip initTopMenuStrip() {
            MenuStrip topMenu = TopMenuContainer.getTopMenuStrip();
            return topMenu;
        }
        /// <summary>
        /// 实例化状态栏
        /// </summary>
        /// <returns></returns>
        private StatusStrip initMainStrtusBar() {
            Control ccc = UIComponentFactory.getSingleControl(DefaultNameEnum.TOOL_START, true);
            StatusStrip statusStrip = null;
            if(ccc != null && ccc is StatusStrip) statusStrip = (StatusStrip)ccc;
            return statusStrip;
        }
        // 根据窗体是否为最小化判断窗体的显示隐藏
        private void doIsTopFormVisible(){
            Form[] topFormArr = FormCacheFactory.getTopFormCache().Values.ToArray();
            for(int i=0; i<topFormArr.Length; i++) { 
                Form f = topFormArr[i];
                if (!f.IsDisposed) {
                    if (this.WindowState.Equals(FormWindowState.Minimized)) {
                        f.Visible = false;
                    } else {
                        f.Visible = true;
                    }
                }
            }
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
        private void initFormLayout(Form form, MenuStrip topMenu, StatusStrip strtusBar, ToolStripContainer container,
            TabControl tab) {
            /*==============设置控件的tab顺序======================*/
            topMenu.TabIndex = 1;
            container.TabIndex = 2;
            tab.TabIndex = 3;
            strtusBar.TabIndex = 5;

            /*==============将Tab容器加入到主容器======================*/
            container.LeftToolStripPanelVisible = false;
            container.TopToolStripPanelVisible = true;
            container.RightToolStripPanelVisible = false;
            container.BottomToolStripPanelVisible = false;
            container.ContentPanel.Controls.Add(tab);
            tab.Location = new Point(-1, -3);
            tab.Size = new Size(container.ContentPanel.ClientSize.Width+5, 
                container.ContentPanel.ClientSize.Height - tab.Location.Y - container.BottomToolStripPanel.Height);

            /*==============将顶部加入到窗体======================*/
            form.Controls.Add(topMenu);
            topMenu.Width = form.ClientSize.Width;
            topMenu.Location = new Point(0,0);

            /*==============将状态栏加入到窗体中======================*/
            form.Controls.Add(strtusBar);
            strtusBar.Width = form.ClientSize.Width;

            /*==============将主容器加入到窗体======================*/
            form.Controls.Add(container);
            container.Location = new Point(0, topMenu.Location.Y+topMenu.Height);
            container.Width = form.ClientSize.Width;
            container.Height = form.ClientSize.Height - container.Location.Y - strtusBar.Height;
        }

        // 窗体得到焦点事件
        private void RootDisplayForm_Activated(object sender, EventArgs e) {
            FormUtisl.TopFormNoFocus(true, FormCacheFactory.getTopFormCache().Values.ToArray());
        }
        // 窗体失去焦点事件
        private void RootDisplayForm_Deactivate(object sender, EventArgs e) {
            FormUtisl.TopFormNoFocus(false, FormCacheFactory.getTopFormCache().Values.ToArray());
        }

        private void RootDisplayForm_VisibleChanged(object sender, EventArgs e) {
            
        }
        // 窗体大小调整事件
        private void RootDisplayForm_Resize(object sender, EventArgs e) {
            doIsTopFormVisible();
        }
    }
}
