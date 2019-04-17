using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using UI.TabContentLibrary.MainTabContent;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawTabControl : TabControl{
        // 添加标签的按钮
        public Control AddPageBut { get => RedrawAddPageBut.initSingleAddPageButton();}
        // 添加标签按钮的代理对象
        public delegate void AddPageButInvoker();
        // 添加标签按钮的实际代理对象
        private AddPageButInvoker addPageButInvoker = null;
        // 判断是否已经成功将标签转化为窗体
        private bool isPageSuccessToForm = false;
        // 删除按钮的大小
        private Size delPageButSize = new Size(8,8);

        // 当前鼠标位于删除按钮下的标签索引
        private int mouseDelPageIndex = -1;

        // 取消标签的选定
        private bool cancelSelectPage = false;

        public new Padding Padding { get; set; } = new Padding(0,0,0,0);
        /// <summary>
        /// 当前鼠标下的索引
        /// </summary>
        public int MousePage { get; private set; } = -1;
        /// <summary>
        /// 当前鼠标下的位置
        /// </summary>
        public Point MousePagePoint { get; private set; } = Point.Empty;
        /// <summary>
        /// 当前鼠标按下的键
        /// </summary>
        public MouseButtons MouseClickButtons { get; private set; } = MouseButtons.None;
        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool IsShowDelPageBut { get; set; } = true;
        /// <summary>
        /// 是否显示添加标签按钮
        /// </summary>
        public bool IsShowAddPageBut { get; set; } = false;
        /// <summary>
        /// 按下鼠标滚轮是否关闭标签
        /// </summary>
        public bool IsClickMiddleDelPage { get; set; } = true;
        /// <summary>
        /// 是否显示删除标签提示
        /// </summary>
        public bool IsDelPageAsk { get; set ; } = false;
        /// <summary>
        /// 标签是否可以转化为独立的窗口
        /// </summary>
        public bool IsPageToAbsoluteForm { get; set ; } = true;

        public RedrawTabControl() { 
             SetStyle(  
             ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
             ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
             ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
             ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
             ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
             true);                                         // 设置以上值为 true  
            UpdateStyles();
            // 初始化添加标签按钮
            initAddPageButton();
        }
        // 去除左边和上边的空白
        public override Rectangle DisplayRectangle {
            get {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 2, rect.Top +0, rect.Width + 2, rect.Height + 1);
            }
        }
        // 标签背景色
        private Color page_back_color = ColorTranslator.FromHtml("#007ACC");
        // 标签选中背景色
        private Color page_sel_color = ColorTranslator.FromHtml("#007ACC");
        // 标签鼠标选中背景色
        //private Color page_mouse_sel_color = ColorTranslator.FromHtml("#1C97EA");
        private Color page_mouse_sel_color = ColorTranslator.FromHtml("#007ACC");

        // 标签选中前景色
        private Color page_sel_font_color = ColorTranslator.FromHtml("#fff");
        // 标签不选中背景色
        private Color page_nosel_color = ColorTranslator.FromHtml("#fff");
        // 标签不选中前景色
        private Color page_nosel_font_color = ColorTranslator.FromHtml("#000");
        // 标签边框色
        private Color page_border_color = ColorTranslator.FromHtml("#A3A3A3");
        // 重绘事件
        protected override void OnPaint(PaintEventArgs e) {
            TabControl tabc_draw = this;
            Graphics g = e.Graphics;
            //设置高质量,低速度呈现平滑程度   
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            // 重绘选项卡
            for (int i = 0; i < tabc_draw.TabCount; i++) {
                // 当前处理标签
                TabPage changedpage = tabc_draw.TabPages[i];
                // 设置标签背景色
                changedpage.BackColor = page_back_color;
                // 选中
                if (i == tabc_draw.SelectedIndex) {
                    // 选中样式
                    DrawSelectPage(g, tabc_draw, i);
                } else {
                    if (i == MousePage && MouseButtons.None.Equals(MouseClickButtons)) { 
                        // 鼠标移入标签样式
                        DrawMouseSelectPage(g, tabc_draw, MousePage);
                    } else { 
                        // 未选中样式
                        DrawNoSelectPage(g, tabc_draw, i);
                    }
                }
                if((i == tabc_draw.SelectedIndex || i == MousePage)
                    && IsShowDelPageBut && tabc_draw.TabCount > 1) { 
                    // 绘制当前鼠标下选项卡的关闭按钮
                    PageDrawDeleteBut(g, tabc_draw, MousePage);
                    // 绘制选中选项卡的关闭按钮
                    PageDrawDeleteBut(g, tabc_draw, tabc_draw.SelectedIndex);
                }
            }
        }
        // 鼠标移动事件
        protected override void OnMouseMove(MouseEventArgs e) {
            // 赋值当前鼠标下的位置
            MousePagePoint = e.Location;
            // 当前鼠标下的标签索引
            MousePage = getMouseLocPage(this, e.Location);
            // 判断当前鼠标下的删除标签按钮样式
            diIsDelPageMouseStyle();
            // 判断标签是否要转化为独立的窗口
            // doIsPageToForm();
            base.OnMouseMove(e);
        }
        
        // 控件启用事件
        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
        }
        // 设置鼠标移出事件
        protected override void OnMouseLeave(EventArgs e) {
            // 恢复变量默认值
            MousePage = -1;
            MousePagePoint = Point.Empty;
            base.OnMouseLeave(e);
        }
        // 鼠标按下事件
        protected override void OnMouseDown(MouseEventArgs e) {
            isPageSuccessToForm = false;
            MouseClickButtons = e.Button;
            base.OnMouseDown(e);
        }
        // 鼠标点击事件
        protected override void OnMouseClick(MouseEventArgs e) {
            // 判断是否显示移除标签对话框
            doIsDelPage(e.Button);
            base.OnMouseClick(e);
        }
        // 鼠标松开事件
        protected override void OnMouseUp(MouseEventArgs e) {
            MouseClickButtons = MouseButtons.None;
            base.OnMouseUp(e);
        }
        // 控件添加事件
        protected override void OnControlAdded(ControlEventArgs e) {
            doIsAddPageSizeMode(this);
            doIsAddPageButLocation(AddPageBut, this);
            base.OnControlAdded(e);
        }
        // 调整大小事件
        protected override void OnResize(EventArgs e) {
            ControlsUtilsMet.asynchronousMet(this, 0, delegate{
                doIsAddPageSizeMode(this);
                doIsAddPageButLocation(AddPageBut, this);
            });
            base.OnResize(e);
        }
        // 选中标签事件
        protected override void OnSelecting(TabControlCancelEventArgs e) {
            // 鼠标位于关闭按钮时取消选中事件
            int delIndex = getMouseDelPageIndex();
            if(delIndex >= 0 && cancelSelectPage && IsShowDelPageBut) { 
                e.Cancel = true;
            } else { 
                cancelSelectPage = true;
            }
            base.OnSelecting(e);
        }
        // 控件移除事件
        protected override void OnControlRemoved(ControlEventArgs e) {
            if(e.Control is TabPage) { 
                TabPage ppp = (TabPage)e.Control;
                // 确定移除标签后的显示模式
                delPageIsPageMode(ppp);
                ControlsUtilsMet.asynchronousMet(this, 0, delegate{
                    // 确定显示模式
                    doIsAddPageSizeMode(this);
                    // 确定添加按钮的位置
                    doIsAddPageButLocation(AddPageBut, this);
                });
            }
            
            base.OnControlRemoved(e);
        }
        /// <summary>
        /// 判断标签是否要转化为独立的窗口
        /// </summary>
        private void doIsPageToForm() {
            // 已设置可以转化为独立的窗口 并且 一直按下了鼠标左键 
            TabPage selPage = this.SelectedTab;
            if (IsPageToAbsoluteForm && !isPageSuccessToForm 
                && MouseButtons.Left.Equals(MouseClickButtons) && selPage != null) {
                Rectangle rectangle = this.GetTabRect(this.SelectedIndex);
                int y = MousePagePoint.Y;
                if (y < -10 || y > rectangle.Height + 10) { 
                    MainTabControlUtils.pageToForm(this, this.SelectedIndex, true);
                    isPageSuccessToForm = true;
                }
            }
        }
        // 判断是否要关闭标签
        private void doIsDelPage(MouseButtons mouse) { 
            if (mouse.Equals(MouseButtons.Left)) {
                int delIndex = getMouseDelPageIndex();
                // 点击关闭按钮时移除标签
                if(IsShowDelPageBut && delIndex >= 0) { 
                    if(isDelPageAskMethod()) MainTabControlUtils.deleteTabPage(this, this.TabPages[delIndex]);
                }
            }else if (mouse.Equals(MouseButtons.Middle)) { // 按下鼠标滚轮 
                int delIndex = getMouseLocPage(this, MousePagePoint);
                if(IsClickMiddleDelPage && delIndex >= 0) { 
                    if(isDelPageAskMethod()) MainTabControlUtils.deleteTabPage(this, this.TabPages[delIndex]);
                } 
            }
        }
        private bool isDelPageAskMethod() {
            bool retBool = false;
            // 删除是否提示
            if (IsDelPageAsk) {
                retBool = DialogResult.OK.Equals(ControlsUtilsMet.showAskMessBox("是否删除该标签", "删除标签", null, null));
            } else { 
                retBool = true;
            }
            return retBool;
        }
        /// <summary>
        /// 判断当前鼠标下的删除标签按钮样式
        /// </summary>
        private void diIsDelPageMouseStyle() { 
            if (IsShowDelPageBut) { 
                int delIndex = getMouseDelPageIndex();
                if(delIndex >= 0) { 
                    PageDrawDeleteBut(this.CreateGraphics(),this, delIndex);
                } else { 
                    delIndex = getMouseLocPage(this, MousePagePoint);
                    if(delIndex >= 0) PageDrawDeleteBut(this.CreateGraphics(),this, delIndex);
                }
            }
        }
        // 判断删除标签后的显示模式
        private void delPageIsPageMode(TabPage page) { 
            int selIndex = this.SelectedIndex;
            // 获取要删除的标签的索引
            int delIndex =  MainTabControlUtils.getTabIndex(this, page);
            this.TabPages.Remove(page);
            // 确定移除标签后的要显示的标签
            if(selIndex >=0) {
                cancelSelectPage = false;
                if (selIndex.Equals(delIndex)) { 
                    MainTabControlUtils.isdelPageSelMode(this, delIndex);
                } else { 
                    this.SelectedIndex = selIndex;
                }
            }
        }
        // 重绘标签区域
        private Rectangle DrawPageRec(TabControl tab, int index) { 
            // 标签背景区域
            Rectangle backrect = tab.GetTabRect(index);
            backrect.Location = new Point(backrect.X, backrect.Y+2);
            backrect.Width = backrect.Width;
            backrect.Height = backrect.Height;
            return backrect;
        }
        // 获取当前鼠标位于删除按钮下的标签索引
        public int getMouseDelPageIndex() { 
            TabControl tab = this;
            if(MainTabControlConfig.IS_SHOW_DEL_BUTTON && tab.TabCount > 1) { 
                mouseDelPageIndex = -1;
                int delwidth = delPageButSize.Width;
                int delheight = delPageButSize.Height;
                int x1 = -1;
                int y1 = -1;
                Rectangle rectangle = Rectangle.Empty;
                for(int i = 0; i< tab.TabPages.Count ;i++) { 
                    TabPage page = tab.TabPages[i];
                    x1 = (tab.GetTabRect(i).Right - delwidth)-4;
                    y1 = (tab.GetTabRect(i).Bottom - delheight)/2+4;
                    rectangle = new Rectangle(x1,y1,delwidth,delheight);
                    if(rectangle.Contains(MousePagePoint)) { 
                        mouseDelPageIndex = i;
                        break;
                    }
                }
            } else { 
                mouseDelPageIndex = -1;
            }
            return mouseDelPageIndex;
        }
        // 绘制选中的标签
        private void DrawSelectPage(Graphics g, TabControl tab, int index) { 
            if(index < 0 || index >= tab.TabCount) return;
            // 当前处理标签
            TabPage changedpage = tab.TabPages[index];
            // 标签背景区域
            Rectangle backrect = DrawPageRec(tab, index);

            Brush backbrush = new SolidBrush(page_sel_color);// 标签背景色
            Brush fontbrush = new SolidBrush(page_sel_font_color);// 标签字体颜色
            Color bordercolor = page_sel_color;// 边框颜色
            // tab字体
            Font tabFont = new Font(tab.Font.SystemFontName, tab.Font.Size, tab.Font.Style);;// 标签字体
            // 绘制标签背景
            g.FillRectangle(backbrush, backrect);
            // 绘制标签边框
            ControlsUtilsMet.setControlBorderStyle(g, backrect, ButtonBorderStyle.Solid
                ,1 , 1, 1, 1, page_sel_color);
            // 绘制标签文本
            PageDrawString(g, backrect, changedpage.Text, tabFont, fontbrush);
        }
        // 绘制鼠标当前位置的标签
        private void DrawMouseSelectPage(Graphics g, TabControl tab, int index) { 
            if(index < 0 || index >= tab.TabCount) return;
            // 当前处理标签
            TabPage changedpage = tab.TabPages[index];
            // 标签背景区域
            Rectangle backrect = DrawPageRec(tab, index);

            Brush backbrush = new SolidBrush(page_mouse_sel_color);// 标签背景色
            Brush fontbrush = new SolidBrush(page_sel_font_color);// 标签字体颜色
            Color bordercolor = page_mouse_sel_color;// 边框颜色
            // tab字体
            Font tabFont = new Font(tab.Font.SystemFontName, tab.Font.Size, tab.Font.Style);// 标签字体
            // 绘制标签背景
            g.FillRectangle(backbrush, backrect);
            // 绘制标签边框
            ControlsUtilsMet.setControlBorderStyle(g, backrect, ButtonBorderStyle.Solid
                ,1, 1, 1, 1, page_mouse_sel_color);
            // 绘制标签文本
            PageDrawString(g, backrect, changedpage.Text, tabFont, fontbrush);
        }
        // 绘制未选中的标签
        private void DrawNoSelectPage(Graphics g,TabControl tab, int index) { 
            if(index < 0 || index >= tab.TabCount) return;
            // 当前处理标签
            TabPage changedpage = tab.TabPages[index];
            // 标签背景区域
            Rectangle backrect = DrawPageRec(tab, index);
            // 用来零时制定偏移量的标签背景区域
            Rectangle offsetBackrect = backrect;
            // 向下的偏移量
            int offset = 4; 
            offsetBackrect.Y = offsetBackrect.Y + offset;
            offsetBackrect.Height = offsetBackrect.Height - offset*2;
            Brush backbrush = new SolidBrush(page_nosel_color);// 标签背景色
            Brush fontbrush = new SolidBrush(page_nosel_font_color);// 标签字体颜色
            Color bordercolor = page_border_color;// 边框颜色
            // tab字体
            Font tabFont = new Font(tab.Font.SystemFontName, tab.Font.Size, tab.Font.Style);// 标签字体
            // 绘制标签背景
            g.FillRectangle(backbrush, backrect);
            // 不为0 且不为选中标签 且不为当前鼠标下的标签 且不为当前鼠标下的标签的后一个标签
            if(index != tab.SelectedIndex && index != tab.SelectedIndex -1
                && ((index != MousePage && index != MousePage-1) || (!MouseButtons.None.Equals(MouseClickButtons))
                )) { 
                // 绘制标签边框
                ControlsUtilsMet.setControlBorderStyle(g, offsetBackrect, ButtonBorderStyle.Solid
                , 1, 1, 1, 1, page_nosel_color, page_nosel_color, bordercolor, page_nosel_color);
            }
            // 绘制标签文本
            PageDrawString(g, backrect, changedpage.Text, tabFont, fontbrush);
        }
        // 标签关闭按钮绘制
        private void PageDrawDeleteBut(Graphics g, TabControl tab, int index) {
            if(index < 0 || index >= tab.TabCount) return;
            // 当前处理标签
            TabPage changedpage = tab.TabPages[index];
            // 标签背景区域
            Rectangle backrect = DrawPageRec(tab, index);

            int width = delPageButSize.Width;
            int height = delPageButSize.Height;
            int x1 = (backrect.Right-width)-6;
            int y1 = (backrect.Bottom-height)/2+4;
            Color lineColor = Color.Empty;
            if(getMouseDelPageIndex() == index) { // 判断要绘制的关闭按钮是否位于鼠标下

                lineColor = ColorTranslator.FromHtml("#DF585D");
            } else { 
                lineColor = ColorTranslator.FromHtml("#FFF");
            }
            // 重绘关闭背景和标签背景重叠
            // g.FillRectangle(new SolidBrush(lable_mouse_sel_color), new Rectangle(backrect.Right,backrect.Y,delPageButSize.Width+4,backrect.Height));
            // 线得高
            int lineheight = 2;
            // 线的宽
            int linewidth = (int) Math.Sqrt((height*height)*(width*width));
            // 颜色
            Pen pen = new Pen(lineColor, lineheight);
            // 绘制第一条线
            g.DrawLine(pen, x1, y1, x1+width, y1+height);
            // 绘制第二条线
            g.DrawLine(pen, x1+width, y1, x1, y1+height);
        }
        // 标签文本绘制
        private void PageDrawString(Graphics g, Rectangle backrect, string text, Font font, Brush fontbrush) { 
            // 绘制标签字体
            StringFormat _StringFlags = new StringFormat();
            Rectangle backrectStr = new Rectangle(backrect.X,backrect.Y,backrect.Width,backrect.Height);
            _StringFlags.Alignment = StringAlignment.Center;
            _StringFlags.FormatFlags = StringFormatFlags.NoWrap;
            _StringFlags.Trimming = StringTrimming.EllipsisCharacter;
            

            backrectStr.Height = backrectStr.Height;
            backrectStr.Width = backrectStr.Width - (delPageButSize.Width+5);
            backrectStr.X = backrectStr.X + 0;
            if (StringUtilsMet.getChineseLength(text) > 0) {// 包含中文
                _StringFlags.LineAlignment = StringAlignment.Far;
                backrectStr.Y = backrectStr.Y + -2;
            } else { 
                _StringFlags.LineAlignment = StringAlignment.Center;
                backrectStr.Y = backrectStr.Y + 0;
            }
            Font tempFont = new Font("微软雅黑", 9, FontStyle.Bold);
            g.DrawString(text, tempFont, fontbrush, backrectStr, new StringFormat(_StringFlags));
        }

        /// <summary>
        /// 获取当前鼠标位置下的标签索引
        /// </summary>
        private int getMouseLocPage(TabControl tab, Point mouseLoc) {
            int index = -1;
            for (int i = 0; i < tab.TabPages.Count; i++) {
                if(tab.GetTabRect(i).Contains(mouseLoc)) {
                    index  = i;
                }
            }
            return index;
        }
        /// <summary>
        /// 初始化单例模式下的添加标签按钮
        /// </summary>
        private void initAddPageButton() { 
            Control but = this.AddPageBut;
            // 刚启动时父控件可能为空  循环判断tab的父控件是否为空
            ControlsUtilsMet.timersMet(200, (object sender, ElapsedEventArgs e)=>{
                Control parent = this.Parent;
                if(parent != null) {
                    if(parent.InvokeRequired) { 
                        parent.Invoke(new EventHandler(delegate {
                            AddPageBut.Visible = IsShowAddPageBut;
                            parent.Controls.Add(but);
                            but.BringToFront();
                            ((System.Timers.Timer)sender).Dispose();
                        }));
                    }
                } 
            });
        }

        /// <summary>
        /// 确定添加标签后标签显示的模式
        /// </summary>
        /// <param name="tab"></param>
        private void doIsAddPageSizeMode(TabControl tab) {
            if(tab.TabCount > 0 && tab.SelectedIndex > 0) { 
                int itemW = TabControlDataLib.DEF_ITEM_WIDTH;
                int itemCount = tab.TabCount;
                int rightMargin = 20;
                Size itemSize = Size.Empty;
                int tabWidth = tab.Width - 15;
                if(itemW * itemCount + rightMargin < tabWidth) { 
                    itemSize = new Size(itemW, tab.ItemSize.Height);
                    if(itemSize != Size.Empty && (!itemSize.Width.Equals(tab.ItemSize.Width) 
                        || !itemSize.Height.Equals(tab.ItemSize.Height))) { 
                        tab.ItemSize = itemSize;
                        tab.SizeMode = TabSizeMode.Fixed;
                    }
                } else { 
                    int itemWW = ((tabWidth-rightMargin) / itemCount);
                    itemSize = new Size(itemWW, tab.ItemSize.Height);
                    if(itemSize != Size.Empty && (!itemSize.Width.Equals(tab.ItemSize.Width) 
                        || !itemSize.Height.Equals(tab.ItemSize.Height))) { 
                        tab.ItemSize = itemSize;
                        tab.SizeMode = TabSizeMode.Fixed;
                    }
                }
            }
            
        }
        /// <summary>
        /// 显示添加标签按钮
        /// </summary>
        /// <param name="isShow">是否显示</param>
        /// <param name="transfer">是否立即调用一次点击事件</param>
        /// <param name="mouse">点击事件</param>
        public void showAddPageButtton(bool isShow, bool transfer, AddPageButInvoker mouse) { 
            IsShowAddPageBut = isShow;
            addPageButInvoker = mouse;
            AddPageBut.MouseClick += (object sender, MouseEventArgs e)=> {
                if(MouseButtons.Left.Equals(e.Button)) {
                    mouse.Invoke();
                }
            };
            if(transfer) { 
                mouse.Invoke();
            }
        }
        /// <summary>
        /// 调用一次添加标签的方法
        /// </summary>
        public void addPageInvoke() { 
            addPageButInvoker.Invoke();
        }
        /// <summary>
        /// 确定添加按钮的位置
        /// </summary>
        private void doIsAddPageButLocation(Control addCon, TabControl tab) {
            if(addCon != null && tab != null && tab.TabCount >= 0) { 
                // 标签的宽
                int itemW = tab.TabCount > 0? tab.GetTabRect(0).Width : 0;
                // 标签个数
                int itemCount = tab.TabCount;
                // 按钮的x坐标
                int y = tab.Location.Y + tab.ItemSize.Height - addCon.ClientSize.Height-2;
                // 按钮的x坐标
                int x = itemCount*itemW+6;
                
                if(x+ itemW + addCon.Width < tab.Width) { 
                    addCon.Location = new Point(x, y);
                } else { 
                    addCon.Location = new Point(tab.ClientSize.Width-addCon.Width-10, y);
                }
            }
        }
    }
}
