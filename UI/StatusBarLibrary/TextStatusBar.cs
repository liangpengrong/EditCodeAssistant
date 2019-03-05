using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.StaticMethod.Method.Redraw;
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.ControlCache;
using UI.ControlEventLibrary.StatusBarEvent.TextStatusBarEvent;
using Core.DefaultData.DataLibrary;

namespace UI.StatusBarLibrary
{
    public partial class TextStatusBar
    {
        // 定义状态栏子菜单的事件的委托
        private delegate object methodDelegate(Dictionary<Type, object> data);
        // 状态栏右键菜单默认字体
        private Font rifMenuFont = new Font("微软雅黑", 9, FontStyle.Regular);
        // 状态栏默认字体
        private Font startsFont = new Font("微软雅黑", 8, FontStyle.Regular);
        // 状态栏默认背景色
        private Color startsDefBackColor = ColorTranslator.FromHtml("#007ACC");
        // 状态栏的子项鼠标移入颜色
        private Color startsItemMouseEnter = ColorTranslator.FromHtml("#3395D6");
        // 状态栏
        private StatusStrip statusStrip = new StatusStrip();
         
        private TextStatusBar()
        {
            // 加载状态栏默认配置
            initStatusStripConfig();
            // 将子状态栏加载到状态栏容器中
            addStrutsBarItem(statusStrip, itemsDateAll());
            // 设置右键菜单
            statusStrip.ContextMenuStrip = getStrutsBarRightMenu(statusStrip);
        }
        /// <summary>
        /// 加载状态栏默认配置
        /// </summary>
        private void initStatusStripConfig() {
            statusStrip.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.TOOL_START);
            statusStrip.AutoSize = false;
            statusStrip.BackColor = startsDefBackColor;
            statusStrip.Padding = new Padding(0,0,0,0);
            statusStrip.Font = startsFont;
            statusStrip.AutoSize = false;
            statusStrip.Height = 20;
            statusStrip.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
            statusStrip.Dock = DockStyle.Bottom;
            statusStrip.ShowItemToolTips = true;
        }
        /// <summary>
        /// 将子状态栏信息添加到状态栏容器中
        /// </summary>
        /// <param name="strutsBar">状态栏容器</param>
        /// <param name="itemAll">要添加的字状态栏信息</param>
        private void addStrutsBarItem(StatusStrip strutsBar, List<string[]> itemAll)
        {
            List<ToolStripStatusLabel> listlable = new List<ToolStripStatusLabel>();
            foreach(string[] strAll in itemAll) {
                listlable.Add(getDefStatusLabel(strutsBar, strAll[0],strAll[1],strAll[2]));
            }
            strutsBar.Items.AddRange(listlable.ToArray());
        }
        /// <summary>
        /// 获取默认的状态栏label子项
        /// </summary>
        /// <param name="strutsBar">状态栏</param>
        /// <param name="name">labelName</param>
        /// <param name="text">文本内容</param>
        /// <param name="tag">tag数据</param>
        /// <returns></returns>
        private ToolStripStatusLabel getDefStatusLabel(StatusStrip strutsBar, string name, string text, object tag) { 
            ToolStripStatusLabel label = new ToolStripStatusLabel();
            label.Margin = new Padding(0,0,0,0);
            label.BackColor = startsDefBackColor;
            label.Name = name;
            label.Text = text;
            label.Width = 100;
            label.Padding = new Padding(5,0,5,0);
            label.Height = strutsBar.Height;
            label.Font = strutsBar.Font;
            label.ForeColor = Color.White;
            label.AutoToolTip = true;
            label.Tag = tag;
            label.AutoSize = true;
            label.AutoToolTip = true;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = strutsBar.Font;
            label.Visible = true;
            // 启用双击事件
            label.DoubleClickEnabled = true;
            // 分割线
            label.BorderSides = ToolStripStatusLabelBorderSides.None;
            label.BorderStyle = Border3DStyle.Sunken;
            // 绑定鼠标单击事件
            label.MouseDown += toolLabelMouseDown;
            // 绑定双击击事件
            label.DoubleClick += toolLabelDoubleClick;
            // 绑定文本改变事件
            label.TextChanged += toolLabelTextChanged;
            // 鼠标移入事件
            label.MouseEnter += toolLabelMouseEnter;
            // 鼠标移出事件
            label.MouseLeave += toolLabelMouseLeave;
            return label;
        }

        /// <summary>
        /// 状态栏集合的数据列表
        /// </summary>
        /// <returns></returns>
        private List<string[]> itemsDateAll()
        {
            List<string[]> listStrAll = new List<string[]>();
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.总字符数, "总字符：0", "总字符" });
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.选中字符数, "已选中字符：0", "已选中字符" });
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.总行数, "总行：1", "总行" });
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.行列数, "行：1，列：1", "行：{1},列：{2}" });
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.编码, Encoding.UTF8.BodyName.ToUpper(), "字符编码" });
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.大小写状态, "小写", "大小写" });
            listStrAll.Add(new string[] { StrutsStripDataLib.ItemName.只读状态, "只渎：否", "只读" });
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
            ToolStripMenuItem item = null;
            // 使用自定义的右键菜单
            rigMenu.Renderer = new RightStripRenderer();
            rigMenu.BackColor = strutsbar.BackColor;
            rigMenu.ShowItemToolTips = true;
            rigMenu.TabStop = true;
            rigMenu.Font = rifMenuFont;
            rigMenu.Closing += new ToolStripDropDownClosingEventHandler(ToolStripUtilsMet.moveOutClosing);
            ToolStripItemCollection tempArr = strutsbar.Items;
            foreach(ToolStripItem stripL in tempArr) {
                item = new ToolStripMenuItem();
                item.Name = stripL.Name + "Item";
                item.AutoSize = true;
                item.BackColor = strutsbar.BackColor;
                item.Text = stripL.Name;
                item.CheckOnClick = true;
                item.AutoToolTip = true;
                item.CheckedChanged += menuItemCheckedChanged;
                // 将右键菜单对应的状态栏子项Name放入Tag数据中
                item.Font = rigMenu.Font;
                item.Tag = stripL.Name;
                rigMenu.Items.Add(item);
                item.Checked = stripL.Visible;
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
            ControlsUtilsMet.setControlBorderStyle(e.Graphics, toolStrip.ClientRectangle
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
        /// 状态栏的鼠标点击公用事件
        private void toolLabelMouseDown(object sender, MouseEventArgs e)
        {
            ToolStripStatusLabel lable = (ToolStripStatusLabel)sender;
            List<Control> textList = new List<Control>();
            ControlsUtilsMet.getAllControlByType(ref textList,ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT).Controls);
            TextBox textBox = textList[0] is TextBox? (TextBox)textList[0] : new TextBox();
            //判断当前控件是否有与其关联的句柄并且按下的是左键
            if (this.statusStrip.IsHandleCreated&&e.Button.Equals(MouseButtons.Left)){
                // 改变背景色
                lable.BackColor = ColorTranslator.FromHtml("#6CB8F4");
                
            }
        }
        // 状态栏的双击事件对应关系
        private Dictionary<string, Delegate> doubleClickEventBinding()
        {
            Dictionary<string, Delegate> toolBindingDic = new Dictionary<string, Delegate>();
            toolBindingDic.Add(StrutsStripDataLib.ItemName.总字符数, new methodDelegate(TextStatusBarEventMet.openCharsStatistics));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.总行数, new methodDelegate(TextStatusBarEventMet.openRowGoToForm));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.行列数, new methodDelegate(TextStatusBarEventMet.openCharsStatistics));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.选中字符数, new methodDelegate(TextStatusBarEventMet.openCharsStatistics));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.编码, new methodDelegate(TextStatusBarEventMet.openSetCodingForm));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.大小写状态, new methodDelegate(TextStatusBarEventMet.setCaseMouse));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.只读状态, new methodDelegate(TextStatusBarEventMet.setTextReadOnly));
            return toolBindingDic;
        }

        // 状态栏的双击公用事件
        private void toolLabelDoubleClick(object sender, EventArgs e)
        {
            ToolStripLabel label = (ToolStripLabel)sender;
            //获取当前主Tab容器中的文本框
            TextBox textBox = null;
            List<Control> controls = new List<Control>();
            // 获得主tab容器
            Control tabCon = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(tabCon != null && tabCon is TabControl) { 
                TabControl tab = (TabControl)tabCon;
                ControlsUtilsMet.getAllControlByType(ref controls, tab.SelectedTab.Controls);
                if (controls.Count > 0 && controls[0] is TextBox) { 
                    textBox = (TextBox)controls[0];
                }
            }
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
        // 状态栏文本改变公用事件
        private void toolLabelTextChanged(object sender, EventArgs e) {
            ToolStripLabel lab = (ToolStripLabel)sender;
            //if(StrutsStripDataLib.ItemName.大小写状态.Equals(lab.Name)) {
            //    if("大写".Equals(lab.Text)) {
            //        lab.ForeColor = Color.Red;
            //    } else { 
            //        lab.ForeColor = Color.Black;
            //    }
            
            //}
        }
        // 状态栏鼠标移入公用事件
        private void toolLabelMouseEnter(object sender, EventArgs e) {
            ToolStripStatusLabel lab = (ToolStripStatusLabel)sender;
            // 改变背景色
            if(lab.Enabled) { 
                lab.BackColor = startsItemMouseEnter;
            }
        }
        // 状态栏鼠标移出公用事件
        private void toolLabelMouseLeave(object sender, EventArgs e) {
            ToolStripStatusLabel lab = (ToolStripStatusLabel)sender;
            lab.BackColor = lab.Owner.BackColor;
        }
        /// <summary>
        /// 获取状态栏
        /// </summary>
        /// <returns></returns>
        public static StatusStrip getToolStripStatus() { 
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TOOL_START);
            if(con == null) {
                TextStatusBar textStatusBar = new TextStatusBar();
                ControlCacheFactory.addSingletonCache(textStatusBar.statusStrip);
                return textStatusBar.statusStrip;
            } else { 
                return (StatusStrip)con;
            }
        }
    }
}
