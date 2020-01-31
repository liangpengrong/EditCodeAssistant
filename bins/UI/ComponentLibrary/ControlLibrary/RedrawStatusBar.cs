using Core.CacheLibrary.ControlCache;
using Core.ComponentlRedraw;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using UI.ComponentLibrary.FormLibrary;
using UI.ComponentLibrary.MethodLibrary.Interface;
using UI.ComponentLibrary.MethodLibrary.Util;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawStatusBar : StatusStrip, IComponentInitMode<Control> {
        /// <summary>
        /// 状态栏的全部数据源控件
        /// </summary>
        public Control[] SourceControlArr { get; private set; } = new Control[]{ };
        // 定义状态栏子菜单的事件的委托
        private delegate object methodDelegate(Control con);
        // 状态栏右键菜单默认字体
        private Font rifMenuFont = new Font("微软雅黑", 9, FontStyle.Regular);
        // 状态栏默认字体
        private Font startsFont = new Font("微软雅黑", 8, FontStyle.Regular);
        // 状态栏默认背景色
        private Color startsDefBackColor = ColorTranslator.FromHtml("#007ACC");
        // 状态栏的子项鼠标移入颜色
        private Color startsItemMouseEnter = ColorTranslator.FromHtml("#3395D6");

        internal RedrawStatusBar() {
            SetStyle(  
                 // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁
                 ControlStyles.OptimizedDoubleBuffer |
                 // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁
                 ControlStyles.AllPaintingInWmPaint |
                 // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明
                 ControlStyles.SupportsTransparentBackColor,
             true);// 设置以上值为 true  
            SetStyle(  
                 // 控件不可接收焦点
                 ControlStyles.Selectable ,
             false);// 设置以上值为 false  
                 
            // 加载状态栏默认配置
            initControlDefConfig();
            // 将子状态栏加载到状态栏容器
            addStrutsBarItem();
            // 清理数据源控件集合中的已释放控件
            clearDeaControlTimers();
            // 设置右键菜单
            setStrutsBarRightMenu();
        }
        
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            RedrawStatusBar conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TOOL_START);
            if(con == null || !(con is RedrawStatusBar)) {
                conThis = this;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.TOOL_START);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (RedrawStatusBar)con;
            }
            if(isShowTop) conThis.BringToFront();
            return conThis;
        }
        /// <summary>
        /// 打开多例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initPrototypeExample(bool isShowTop) {
            RedrawStatusBar conThis = this;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.TOOL_START)+DateTime.Now.Ticks.ToString();;
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.TOOL_START, conThis);
            return conThis;
        }
        /// <summary>
        /// 设置源数据控件
        /// </summary>
        /// <param name="control"></param>
        public void SetSourceControl(Control control) {
            if(control == null) return;
            doISItemHideByControl(control);
            // 绑定事件
            bindControlEvent(control);
            // 将控件添加到源控件集合中
            List<Control> list = new List<Control>(SourceControlArr);
            list.Add(control);
            SourceControlArr = list.ToArray();
        }
        // 加载状态栏默认配置
        private void initControlDefConfig() {
            this.Name = EnumUtils.GetDescription(DefaultNameEnum.TOOL_START);
            this.AutoSize = false;
            this.BackColor = startsDefBackColor;
            this.Padding = new Padding(0,0,0,0);
            this.Font = startsFont;
            this.AutoSize = false;
            this.Height = 20;
            this.LayoutStyle = ToolStripLayoutStyle.StackWithOverflow;
            this.Dock = DockStyle.Bottom;
            this.ShowItemToolTips = true;
        }
        // 判断控件是否为状态栏允许绑定的控件类型
        private Type diIsStatusBarAllowBind(Control con) {
            if(con ==  null) return null;
            if(con is TextBox) { 
                return typeof(TextBox);
            } else if(con is DataGridView) { 
                return typeof(DataGridView);
            }
            return null;
        }
        // 根据源控件取判断需要隐藏的选项
        private void doISItemHideByControl(Control con) {
            List<string> hideName = new List<string>();
            if(con is TextBox) {
                hideName.Clear();
                hideName.Add(StrutsStripDataLib.ItemName.选中列);
                hideName.Add(StrutsStripDataLib.ItemName.选中元素);
                hideName.Add(StrutsStripDataLib.ItemName.最大列);
                
            } else if (con is DataGridView) { 
                hideName.Clear();
                hideName.Add(StrutsStripDataLib.ItemName.编码);
                //hideName.Add(StrutsStripDataLib.ItemName.大小写状态);
                //hideName.Add(StrutsStripDataLib.ItemName.只读状态);
            }
            ToolStripStatusLabel[] tools = Items.OfType<ToolStripStatusLabel>().ToArray();
            // 隐藏右键菜单元素 和状态栏元素
            ContextMenuStrip strip = this.ContextMenuStrip;
            foreach(ToolStripMenuItem item in strip.Items.OfType<ToolStripMenuItem>()) {
                string s1 = hideName.Find((s)=>item.Name.IndexOf(s) >= 0);
                if(s1 != null) {
                    strip.Items[item.Name].Visible = false;
                    tools.First((t) => t.Name.IndexOf(s1) >= 0).Visible = false;
                } else {
                    strip.Items[item.Name].Visible = true;
                    tools.First((t) => item.Name.IndexOf(t.Name)>= 0).Visible = true && item.Checked;
                }
            }
        }
        // 绑定控件事件
        private void bindControlEvent(Control con) {
            // 文本改变事件
            EventHandler h1 = new EventHandler((object sender, EventArgs e)=>{ 
                if(!((Control)sender).Focused) return;
                /*============赋值给状态栏总行数与字符数===================*/
                setRowChars(con);
                /*============赋值给状态栏当前行列数===================*/
                setRowColumn(con);
            });
            MouseEventHandler h2 = new MouseEventHandler((object sender, MouseEventArgs e)=>{
                if(!((Control)sender).Focused) return;
                /*============赋值给状态栏选中字符数===================*/
                setSelectChars(con);
                /*============赋值给状态栏当前行列数===================*/
                setRowColumn(con);
                setSelectColumn(con);
                setMaxColumn(con);
                setSelectItem(con);
            });
            MouseEventHandler h3 = new MouseEventHandler((object sender, MouseEventArgs e)=>{
                if(!((Control)sender).Focused) return;
                /*============赋值给状态栏选中字符数===================*/
                setSelectChars(con);
            });

            EventHandler h4 = new EventHandler((object sender, EventArgs e)=>{
                // 根据源控件取判断需要隐藏的选项
                doISItemHideByControl(con);
                /*============设置文本框中的编码到状态栏中===================*/
                setToolSatrtEcoding(con);
                /*============赋值给状态栏总行数与字符数===================*/
                setRowChars(con);
                /*============赋值给状态栏当前行列数===================*/
                setRowColumn(con);
                /*============赋值给状态栏选中字符数===================*/
                setSelectChars(con);
                /*============赋值给状态栏只读状态===================*/
                refreshTextReadOnly(con);
                setSelectColumn(con);
                setMaxColumn(con);
                setSelectItem(con);
                setSelectRow(con);
            });
            KeyEventHandler h5 = new KeyEventHandler((object sender, KeyEventArgs e)=>{
                if(!((Control)sender).Focused) return;
                /*============赋值给状态栏当前行列数===================*/
                setRowColumn(con);
                /*============赋值给状态栏总行数与字符数===================*/
                setRowChars(con);
                setSelectColumn(con);
                setMaxColumn(con);
                setSelectItem(con);
            });
            KeyEventHandler h6 = new KeyEventHandler((object sender, KeyEventArgs e)=>{
                if(!((Control)sender).Focused) return;
                /*============赋值给状态栏当前行列数===================*/
                setRowColumn(con);
                /*============赋值给状态栏选中字符数===================*/
                setSelectChars(con);
                /*============将大小写状态赋值给状态栏===================*/
                setCaseKey(con);

                setSelectRow(con);
            });
            MouseEventHandler h7 = new MouseEventHandler((object sender, MouseEventArgs e)=>{
                if(!((Control)sender).Focused) return;
                /*============赋值给状态栏选中字符数===================*/
                setSelectChars(con);
                /*============赋值给状态栏当前行列数===================*/
                setRowColumn(con);

                setSelectColumn(con);
                setMaxColumn(con);
                setSelectItem(con);
                setSelectRow(con);
            });
            // 判断要绑定的控件类型
            if(con is TextBox) { 
                TextBox t = (TextBox)con;
                t.TextChanged += h1;
                t.MouseDown += h2;
                t.MouseUp += h3;
                t.GotFocus += h4;
                t.KeyDown += h5;
                t.KeyUp += h6;
                t.MouseMove += h7;
            }else if(con is DataGridView) { 
                DataGridView g = (DataGridView)con;
                g.TextChanged += h1;
                g.MouseDown += h2;
                g.MouseUp += h3;
                g.GotFocus += h4;
                g.KeyDown += h5;
                g.KeyUp += h6;
                g.MouseMove += h7;
            }
            
        }
        /// <summary>
        /// 将子状态栏信息添加到状态栏容器中
        /// </summary>
        /// <param name="strutsBar">状态栏容器</param>
        /// <param name="itemAll">要添加的字状态栏信息</param>
        private void addStrutsBarItem() {
            List<string[]> itemAll = new List<string[]>();
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.总字符数, "总字符：0", "总字符" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.选中字符数, "选中字符：0", "选中字符" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.选中行, "选中行：0", "选中行" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.选中列, "选中列：0", "选中列" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.选中元素, "选中元素：0", "选中元素" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.总行数, "总行：1", "总行" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.最大列, "最大列：0", "最大列" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.行列数, "行：1，列：1", "行：{1},列：{2}" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.编码, Encoding.UTF8.BodyName.ToUpper(), "字符编码" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.大小写状态, "小写", "大小写" });
            itemAll.Add(new string[] { StrutsStripDataLib.ItemName.只读状态, "只渎：否", "只读" });

            List<ToolStripStatusLabel> listlable = new List<ToolStripStatusLabel>();
            foreach(string[] strAll in itemAll) {
                listlable.Add(getDefStatusLabel(this, strAll[0],strAll[1],strAll[2]));
            }
            this.Items.AddRange(listlable.ToArray());
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
            // 鼠标移入事件
            label.MouseEnter += toolLabelMouseEnter;
            // 鼠标移出事件
            label.MouseLeave += toolLabelMouseLeave;
            return label;
        }

        /// <summary>
        /// 获取将状态栏所有子状态栏姓名添加入右键菜单的右键菜单
        /// </summary>
        /// <param name="strutsbar">类型为ToolStrip状态栏</param>
        /// <returns>添加完毕的状态栏</returns>
        private void setStrutsBarRightMenu() {
            ContextMenuStrip rigMenu = new ContextMenuStrip();
            ToolStripMenuItem item = null;
            // 使用自定义的右键菜单
            rigMenu.Renderer = new RightStripRenderer();
            rigMenu.BackColor = this.BackColor;
            rigMenu.ShowItemToolTips = true;
            rigMenu.TabStop = true;
            rigMenu.Font = rifMenuFont;
            rigMenu.DropShadowEnabled = true;
            rigMenu.Opened += (object sender, EventArgs e)=>{
                Point point = MousePosition;
                point.X = point.X + 15;
                point.Y = point.Y - rigMenu.Height/2;
                rigMenu.Show(point);
            };
            // 将右键菜单设置为鼠标在内时不关闭
            ToolStripUtils.ToolStripMoveOutClosing(rigMenu);
            ToolStripItemCollection tempArr = this.Items;
            foreach(ToolStripItem stripL in tempArr) {
                item = new ToolStripMenuItem();
                item.Name = stripL.Name + "Item";
                item.AutoSize = true;
                item.BackColor = this.BackColor;
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
            this.ContextMenuStrip = rigMenu;
        }
        /// <summary>
        /// 判断状态栏中的各个子栏的显隐性
        /// </summary>
        /// <param name="item"></param>
        /// <param name="toolStrip"></param>
        /// <param name="addName"></param>
        private void isLabelVisible(ToolStripMenuItem item) {
            if(null != item.Tag) { 
                ToolStripItem stripItem = this.Items[item.Tag.ToString()];
                stripItem.Visible = item.Checked;
            }
        }
        /// <summary>
        /// 重绘状态栏的边框
        /// </summary>
        /// <param name="menu">需要重绘的菜单</param>
        private void paintStrutsBarFrame(object sender, PaintEventArgs e) {
            StatusStrip toolStrip = (StatusStrip)sender;
            ControlsUtils.SetControlBorderStyle(e.Graphics, toolStrip.ClientRectangle
                , ButtonBorderStyle.Solid
                , 0, 1, 0, 0
                , Color.FromArgb(160, 160, 160));
        }
        /// <summary>
        /// 右键菜单选项的选项事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItemCheckedChanged(object sender, EventArgs e) {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            isLabelVisible(item);
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
        /// 状态栏的鼠标点击公用事件
        private void toolLabelMouseDown(object sender, MouseEventArgs e) {
            ToolStripStatusLabel lable = (ToolStripStatusLabel)sender;
            List<Control> textList = new List<Control>();
            ControlsUtils.GetAllControlByType(ref textList,ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT).Controls);
            TextBox textBox = textList[0] is TextBox? (TextBox)textList[0] : new TextBox();
            //判断当前控件是否有与其关联的句柄并且按下的是左键
            if (this.IsHandleCreated&&e.Button.Equals(MouseButtons.Left)){
                // 改变背景色
                lable.BackColor = ColorTranslator.FromHtml("#6CB8F4");
            }
        }
        // 状态栏的双击公用事件
        private void toolLabelDoubleClick(object sender, EventArgs e) {
            ToolStripLabel label = (ToolStripLabel)sender;
            Delegate myDelegate = null;
            doubleClickEventBinding().TryGetValue(label.Name, out myDelegate);
            if (myDelegate!=null) {
                Dictionary<Type, object> data = new Dictionary<Type, object>();
                // 获取源控件中获取焦点的控件
                Control focusCon = ControlsUtils.GetFocueControlByType(SourceControlArr);
                this.Invoke(myDelegate,new object[]{focusCon});
            }
        }
        // 状态栏的双击事件对应关系
        private Dictionary<string, Delegate> doubleClickEventBinding() {
            Dictionary<string, Delegate> toolBindingDic = new Dictionary<string, Delegate>();
            toolBindingDic.Add(StrutsStripDataLib.ItemName.总字符数, new methodDelegate((Control con)=>{
                if(con == null) { MessageBox.Show("无法获取控件");}
                // 获取控件
                if (con is TextBox) { 
                    TextBox t = (TextBox)con;
                    CharsStatistics.openCharsStatistics(t);
                }
                return null;
            }));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.总行数, new methodDelegate((Control con)=>{ 
                Form ff = UIComponentFactory.getSingleForm(DefaultNameEnum.ROW_GOTO_FORM, false);
                ff.ShowDialog();
                return null;
            }));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.行列数, new methodDelegate((Control con)=>{ 
                if(con == null) { MessageBox.Show("无法获取控件");}
                // 获取控件
                if(con is TextBox) { 
                    TextBox t = (TextBox)con;
                    CharsStatistics.openCharsStatistics(t);
                }
                return null;
            }));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.选中字符数, new methodDelegate((Control con)=>{ 
                if(con == null) { MessageBox.Show("无法获取控件");}
                // 获取控件
                if(con is TextBox) { 
                    TextBox t = (TextBox)con;
                    CharsStatistics.openCharsStatistics(t);
                }
                return null;
            }));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.编码, new methodDelegate((Control con)=>{ 
                Form ff = UIComponentFactory.getSingleForm(DefaultNameEnum.SET_CODING_FORM, false);
                ff.ShowDialog();
                return null;
            }));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.大小写状态, new methodDelegate((Control con)=>{ 
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this, 1, delegate{ 
                    ToolStripLabel lable = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.大小写状态];
                    byte[] bs = new byte[256];
                    //判断当前大小写1为大写
                    WinApiUtils.GetKeyboardState(bs);
                    if (bs[0x14].Equals(1)) {// 判断当前为大写
                        //设置为小写
                        WinApiUtils.SetCapitalState(false);
                        lable.Text = "小写";
                    } else {
                        WinApiUtils.SetCapitalState(true);
                        lable.Text = "大写";
                    }
                });
                return null;
            }));
            toolBindingDic.Add(StrutsStripDataLib.ItemName.只读状态, new methodDelegate(setTextReadOnly));
            return toolBindingDic;
        }
        /// <summary>
        /// 设置文本框只读
        /// </summary>
        private object setTextReadOnly(Control con) {
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is TextBox) { // 文本框
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    // 获取只读lable
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.只读状态];
                    t.ReadOnly = !t.ReadOnly;
                    bool only = t.ReadOnly;
                    string tag1 = lable1.Tag != null? lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1 + (only?"是" : "否");
                });
            }else if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    // 获取只读lable
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.只读状态];
                    g.ReadOnly = !g.ReadOnly;
                    bool only = g.ReadOnly;
                    string tag1 = lable1.Tag != null? lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1 + (only?"是" : "否");
                });
                
            }
            return null;
        }

        // 设置状态栏的编码
        private object setToolSatrtEcoding(Control con) {
            if(con == null) {MessageBox.Show("无法获取控件");}
            // 获取文本框
            if(con is TextBox) { 
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(t,300, delegate{ 
                    Dictionary<string, object> tag = TextBoxUtils.GetTextTagToMap(t);
                    Encoding ecoding = TextBoxDataLibcs.TEXTBOX_ECODING_DEF;
                    // 获取文本框中Tag中存的编码
                    if(tag.ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING)) {
                        ecoding = (Encoding)TextBoxUtils.GetTextTagToMap(t)[TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING];
                    }
                    // 全局单例控件工厂
                    Dictionary<string, Control> single = ControlCacheFactory.getSingletonCache();
                    if(single.ContainsKey(EnumUtils.GetDescription(DefaultNameEnum.TOOL_START))) { 
                        // 状态栏
                        ToolStrip toolStrip = (ToolStrip)single[EnumUtils.GetDescription(DefaultNameEnum.TOOL_START)];
                        // 获取编码Item
                        ToolStripItem labEcoding = toolStrip.Items[StrutsStripDataLib.ItemName.编码];
                        labEcoding.Text = ecoding.BodyName.ToUpper();
                    }
                });
            }
            return null;
        }

        // 将大小写状态赋值给状态栏
        private void setCaseKey(Control con) {
            // 获取文本框
            // 开辟新线程执行方法
            ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                ToolStripLabel lable = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.大小写状态];
                // 判断当前为大写
                if (WinApiUtils.GetCapitalState()){
                    lable.Text = "大写";
                } else {
                    lable.Text = "小写";
                }
            });
        }

        // 将总行数与总字符数赋值给状态栏
        private void setRowChars(Control con) {
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is TextBox) { // 获取文本框
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.总行数];
                    ToolStripLabel lable2 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.总字符数];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    string tag2 = lable2.Tag != null?lable2.Tag.ToString()+"：":"";
                    lable1.Text = tag1+TextBoxUtils.GetTextBoxTotalRow(t).ToString();
                    lable2.Text = tag2+TextBoxUtils.GetTextBoxChars(t, false).ToString();
                });
            }else if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.总行数];
                    ToolStripLabel lable2 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.总字符数];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    string tag2 = lable2.Tag != null?lable2.Tag.ToString()+"：":"";
                    lable1.Text = tag1+g.RowCount.ToString();
                    lable2.Text = tag2+DataGridViewUtilMet.getDatatabelSelText(g,true)
                    .Replace("\t", "").Replace(Environment.NewLine, "").Length.ToString();
                });
                
            }
        }
        // 将选中字符数赋值给状态栏
        private void setSelectChars(Control con) {
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is TextBox) { // 获取文本框
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.选中字符数];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1+t.SelectionLength.ToString();
                });
            }else if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.选中字符数];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1+DataGridViewUtilMet.getDatatabelSelText(g,false)
                    .Replace("\t", "").Replace(Environment.NewLine, "").Length.ToString();
                });
            }
        }
        // 将选中列数赋值给状态栏
        private void setSelectColumn(Control con) { 
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.选中列];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    int selColum = DataGridViewUtilMet.getSelCellRowsColns(g)[1].Length;
                    // 给状态栏赋值
                    lable1.Text = tag1+selColum.ToString();
                });
            }
        }
        // 将最大列数赋值给状态栏
        private void setMaxColumn(Control con) { 
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.最大列];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1+g.ColumnCount.ToString();
                });
            }
        }
        // 将选中的项目数赋值给状态栏
        private void setSelectItem(Control con) { 
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.选中元素];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1+g.SelectedCells.Count.ToString();
                });
            }
        }
        // 将当前的行列数赋值给状态栏
        private void setRowColumn(Control con) {
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is TextBox) { // 获取文本框
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.行列数];
                    int[] val = TextBoxUtils.GetTextBoxRowColumn(t);
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString():"";
                    if (val != null) {
                        //将行与列赋值给label
                        lable1.Text = tag1.Replace("{1}",val[0].ToString())
                                .Replace("{2}", val[1].ToString());
                    }
                });
            }else if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.行列数];
                    int[] val = new int[]{ 
                        g.SelectedCells.Count>0? g.SelectedCells[0].RowIndex+1: 0, 
                        g.SelectedCells.Count>0? g.SelectedCells[0].ColumnIndex+1: 0
                    };
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString():"";
                    if (val != null) {
                        //将行与列赋值给label
                        lable1.Text = tag1.Replace("{1}",val[0].ToString())
                                .Replace("{2}", val[1].ToString());
                    }
                });
            }
        }
        // 将选中的行数赋值给状态栏
        private void setSelectRow(Control con) { 
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is TextBox) { // 获取文本框
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.选中行];
                        int val = 0;
                        if(t.SelectedText.Length >0) { 
                            val = StringUtils.SplitStrToArray(t.SelectedText, 
                                new string[]{Environment.NewLine}, true, false).Length; 
                        }
                        // 将选中行赋值给label
                        lable1.Text = lable1.Text.Split('：')[0]+"："+val;
                });
            }else if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.选中行];
                    int val = DataGridViewUtilMet.getSelCellRowsColns(g)[0].Length;
                    // 将选中行赋值给label
                    lable1.Text = lable1.Text.Split('：')[0]+"："+val;
                });
            }
        } 
                // 刷新控件只读状态
        private object refreshTextReadOnly(Control con) { 
            if(con == null) {MessageBox.Show("无法获取控件");}
            if(con is TextBox) { // 获取文本框
                TextBox t = (TextBox)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    // 获取只读lable
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.只读状态];
                    bool only = t.ReadOnly;
                    string tag1 = lable1.Tag != null? lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1 + (only?"是" : "否");
                });
            }else if(con is DataGridView) { // 表格
                DataGridView g = (DataGridView)con;
                // 开辟新线程执行方法
                ControlsUtils.AsynchronousMethod(this,1, delegate{ 
                    // 获取只读lable
                    ToolStripLabel lable1 = (ToolStripLabel)this.Items[StrutsStripDataLib.ItemName.只读状态];
                    bool only = g.ReadOnly;
                    string tag1 = lable1.Tag != null? lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1 + (only?"是" : "否");
                });
            }
            return null;
        }
        // 清理数据源控件集合中的已释放控件
        private void clearDeaControlTimers() { 
            // 实例化Timer类，设置时间间隔
            System.Timers.Timer timer = new System.Timers.Timer(600000);
            // 定义清除已释放控件的内部方法
            void clear() { 
                List<Control> list = new List<Control>(SourceControlArr);
                for (int i=SourceControlArr.Length; i > 0; i--) { 
                    Control c = SourceControlArr[i];
                    if(c == null || c.IsDisposed) { 
                        list.RemoveAt(i);
                    }
                }
                SourceControlArr = list.ToArray();
            }
            // 立即执行一次
            clear();
            // 到达时间的时候执行事件
            timer.Elapsed += (object sender, ElapsedEventArgs e)=>{
                clear();
            };
            // 设置是执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            // 是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
        }
    }
    internal enum StatusBarBindControlEventType {
        鼠标按下事件 = 0,
        获取焦点事件 = 1,
        失去焦点事件 = 2,
        内容改变事件 = 3,
        鼠标移动事件 = 4,
        鼠标松开事件 = 5,
        键盘按下事件 = 6,
        键盘松开事件 = 7,
        控件启用事件 = 8,
        鼠标移入事件 = 9,
        文件拖放完成事件 = 10

    }
}
