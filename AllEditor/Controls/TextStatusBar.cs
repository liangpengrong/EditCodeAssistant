using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubMethodLibrary;
using StaticDataLibrary;
using PubCacheArea;

namespace CharsToolset
{
    public partial class TextStatusBar : UserControl
    {
        //定义状态栏子菜单的事件的委托
        private delegate object methodDelegate(Dictionary<Type, object> data);
        // 默认字体
        private Font defFont = new Font("微软雅黑", 9, FontStyle.Regular);
        // 启动窗体
        private RootDisplayForm rootDisplayForm = null;
        public TextStatusBar(RootDisplayForm rootDisplayForm)
        {
            //启动窗体
            this.rootDisplayForm = rootDisplayForm;
            InitializeComponent();
            // 将子状态栏加载到状态栏容器中
            this.addStrutsBarItem(this.statusStrip,this.itemsDateAll());
        }
        /// <summary>
        /// 将子状态栏信息添加到状态栏容器中
        /// </summary>
        /// <param name="strutsBar">状态栏容器</param>
        /// <param name="itemAll">要添加的字状态栏信息</param>
        private void addStrutsBarItem(StatusStrip strutsBar, List<string[]> itemAll)
        {
            strutsBar.AutoSize = false;
            strutsBar.Padding = new Padding(0,1,0,1);
            foreach(string[] strAll in itemAll) {

                ToolStripStatusLabel label = new ToolStripStatusLabel();
                label.Margin = new Padding(0,0,0,0);
                label.Name = strAll[0];
                label.Text = strAll[1];
                label.Width = 65;
                label.Height = strutsBar.Height;
                label.Font = strutsBar.Font;
                label.ToolTipText = strAll[2];
                label.AutoSize = false;
                label.TextAlign = ContentAlignment.MiddleCenter;
                // 分割线
                label.BorderSides = ToolStripStatusLabelBorderSides.Right;
                label.BorderStyle = Border3DStyle.Sunken;
                // 绑定鼠标单击事件
                label.MouseDown += new MouseEventHandler(toolLabelMouseDown);
                // 绑定双击击事件
                label.DoubleClick += new EventHandler(toolLabelDoubleClick);
                // 绑定文本改变事件
                label.TextChanged += new EventHandler(toolLabelTextChanged);
                // 鼠标移入事件
                label.MouseEnter += (object sender, EventArgs e) =>{
                    ToolStripStatusLabel lab = (ToolStripStatusLabel)sender;
                    lab.ToolTipText = lab.ToolTipText.Split('：')[0] + "：" + lab.Text;
                };
                // 启用双击事件
                label.DoubleClickEnabled = true;
                strutsBar.Items.Add(label);
            }
        }

        /// <summary>
        /// 状态栏集合的数据列表
        /// </summary>
        /// <returns></returns>
        private List<string[]> itemsDateAll()
        {
            List<string[]> listStrAll = new List<string[]>();
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.总字符数, "0", "总字符数" });
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.选中字符数, "0", "选中字符数" });
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.总行数, "1", "总行数" });
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.行列数, "1:1", "行与列" });
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.编码, Encoding.UTF8.BodyName.ToUpper(), "字符编码" });
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.大小写状态, "小写", "大小写状态" });
            listStrAll.Add(new string[] { StrutsStripDateLib.ItemName.只读状态, "否", "只读状态" });
            return listStrAll;
        }
        /// <summary>
        /// 获取将状态栏所有子状态栏姓名添加入右键菜单的右键菜单
        /// </summary>
        /// <param name="strutsbar">类型为ToolStrip状态栏</param>
        /// <returns>添加完毕的状态栏</returns>
        public ContextMenuStrip getStrutsBarRightMenu(StatusStrip strutsbar)
        {
            ContextMenuStrip rigMenu = new ContextMenuStrip();
            // 使用自定义的右键菜单
            rigMenu.Renderer = new MyToolStripRenderer();
            rigMenu.BackColor = strutsbar.BackColor;
            rigMenu.ShowItemToolTips = true;
            rigMenu.TabStop = false;
            rigMenu.Font = defFont;
            rigMenu.Closing += new ToolStripDropDownClosingEventHandler(MenuItemUtilsMet.moveOutClosing);
            foreach(ToolStripStatusLabel stripL in strutsbar.Items.OfType<ToolStripStatusLabel>())
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = stripL.Name + "Item";
                item.AutoSize = true;
                item.BackColor = strutsbar.BackColor;
                item.Text = stripL.Name;
                item.CheckOnClick = true;
                item.Checked = stripL.Visible;
                item.ToolTipText = stripL.ToolTipText;
                item.CheckedChanged += menuItemCheckedChanged;
                // 将右键菜单对应的状态栏子项Name放入Tag数据中
                item.Font = rigMenu.Font;
                item.Tag = stripL.Name;
                rigMenu.Items.Add(item);
            }
            return rigMenu;
        }

        /// <summary>
        /// 判断状态栏中的各个子栏的显隐性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toolStrip"></param>
        /// <param name="addName"></param>
        private void isLabelVisible(ToolStripMenuItem item)
        {
            if(null != item.Tag) { 
                ToolStripItem stripItem = statusStrip.Items[item.Tag.ToString()];
                stripItem.Visible = item.Checked;
            }
        }
        /// <summary>
        /// 重绘状态栏的边框
        /// </summary>
        /// <param name="menu">需要重绘的菜单</param>
        public static void paintStrutsBarFrame(object sender, PaintEventArgs e)
        {
            StatusStrip toolStrip = (StatusStrip)sender;
            ControlsUtilsMet.setCOntrolBorderStyle(e.Graphics, toolStrip.ClientRectangle
                , ButtonBorderStyle.Solid
                , 0, 1, 0, 0
                , Color.FromArgb(160, 160, 160));
        }
        /// <summary>
        /// 右键菜单选项的选项事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void menuItemCheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            isLabelVisible(item);
        }
        /// <summary>
        /// 状态栏的鼠标点击事件对应关系
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Delegate> mouseDownEventBinding()
        {
            Dictionary<string, Delegate> toolBindingDic = new Dictionary<string, Delegate>();
            //toolBindingDic.Add(StrutsStripDateLib.ItemName.大小写状态, new methodDelegate(TextStatusBarEventMet.setCaseMouse));
            return toolBindingDic;
        }
        /// <summary>
        /// 状态栏的鼠标点击公用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolLabelMouseDown(object sender, MouseEventArgs e)
        {
            ToolStripStatusLabel lable = (ToolStripStatusLabel)sender;

            List<TextBox> textList = new List<TextBox>();
            ControlsUtilsMet.getAllControlByType<TextBox>(textList,ControlCache.getSingletonCon(DefaultNameCof.tabContent).Controls);
            TextBox textBox = textList[0];
            //判断当前控件是否有与其关联的句柄并且按下的是左键
            if (this.statusStrip.IsHandleCreated&&e.Button.Equals(MouseButtons.Left))
            {
                Delegate myDelegate=null;
                mouseDownEventBinding().TryGetValue(lable.Name, out myDelegate);
                if (myDelegate!=null)
                {
                    Dictionary<Type, object> data = new Dictionary<Type, object>();
                    data.Add(typeof(TextBox), textBox);
                    data.Add(typeof(StatusStrip), this.statusStrip);

                    this.statusStrip.Invoke(myDelegate,new object[]{data});
                }
            }
        }

        /// <summary>
        /// 状态栏的双击事件对应关系
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, Delegate> doubleClickEventBinding()
        {
            Dictionary<string, Delegate> toolBindingDic = new Dictionary<string, Delegate>();
            toolBindingDic.Add(StrutsStripDateLib.ItemName.总字符数, new methodDelegate(TextStatusBarEventMet.openCharsStatistics));
            toolBindingDic.Add(StrutsStripDateLib.ItemName.总行数, new methodDelegate(TextStatusBarEventMet.openRowGoToForm));
            toolBindingDic.Add(StrutsStripDateLib.ItemName.行列数, new methodDelegate(TextStatusBarEventMet.openCharsStatistics));
            toolBindingDic.Add(StrutsStripDateLib.ItemName.选中字符数, new methodDelegate(TextStatusBarEventMet.openCharsStatistics));
            toolBindingDic.Add(StrutsStripDateLib.ItemName.编码, new methodDelegate(TextStatusBarEventMet.openSetCodingForm));
            toolBindingDic.Add(StrutsStripDateLib.ItemName.大小写状态, new methodDelegate(TextStatusBarEventMet.setCaseMouse));
            return toolBindingDic;
        }

        /// <summary>
        /// 状态栏的双击公用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolLabelDoubleClick(object sender, EventArgs e)
        {
            ToolStripLabel label = (ToolStripLabel)sender;

            List<TextBox> textList = new List<TextBox>();
            ControlsUtilsMet.getAllControlByType<TextBox>(textList,ControlCache.getSingletonCon(DefaultNameCof.tabContent).Controls);
            TextBox textBox = textList[0];

            Delegate myDelegate = null;
            doubleClickEventBinding().TryGetValue(label.Name, out myDelegate);
            if (myDelegate!=null)
            {
                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), textBox);
                data.Add(typeof(StatusStrip), this.statusStrip);

                this.statusStrip.Invoke(myDelegate,new object[]{data});
            }
        }
        /// <summary>
        /// 状态栏文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolLabelTextChanged(object sender, EventArgs e) {
            ToolStripLabel lab = (ToolStripLabel)sender;
            if(StrutsStripDateLib.ItemName.大小写状态.Equals(lab.Name)) {
                if("大写".Equals(lab.Text)) {
                    lab.ForeColor = Color.Red;
                } else { 
                    lab.ForeColor = Color.Black;
                }
            
            }
        }
    }
}
