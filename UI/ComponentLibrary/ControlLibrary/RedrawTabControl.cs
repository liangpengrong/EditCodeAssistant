using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.TabContentLibrary.MainTabContent;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawTabControl : TabControl{
        // 删除按钮的大小
        private Size delPageButSize = new Size(8,8);
        // 当前鼠标下的索引
        private int mousePageIndex = 0;
        // 当前鼠标下的位置
        private Point mousePagePoint = Point.Empty;
        // 当前鼠标按下的键
        private MouseButtons mouseClickButtons = MouseButtons.None;
        // 当前鼠标位于删除按钮下的标签索引
        private int mouseDelPageIndex = -1;
        // 取消标签的选定
        private bool cancelSelectPage = false;
        /// <summary>
        /// 当前鼠标下的索引
        /// </summary>
        public int MousePage { get => mousePageIndex;}
        /// <summary>
        /// 当前鼠标下的位置
        /// </summary>
        public Point MousePagePoint { get => mousePagePoint;}
        /// <summary>
        /// 当前鼠标按下的键
        /// </summary>
        public MouseButtons MouseClickButtons { get => mouseClickButtons;}
        /// <summary>
        /// 是否显示关闭按钮
        /// </summary>
        public bool IsShowDelPageBut { get; set; } = true;
        /// <summary>
        /// 按下鼠标滚轮是否关闭标签
        /// </summary>
        public bool IsClickMiddleDelPage { get; set; } = true;
        /// <summary>
        /// 是否显示删除标签提示
        /// </summary>
        public bool IsDelPageAsk { get; set ; } = false;

        public RedrawTabControl() { 
             SetStyle(  
             ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
             ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
             ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
             ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
             ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
             true);                                         // 设置以上值为 true  
            UpdateStyles(); 
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
        private Color lable_sel_color = ColorTranslator.FromHtml("#007ACC");
        // 标签鼠标选中背景色
        private Color lable_mouse_sel_color = ColorTranslator.FromHtml("#1C97EA");

        // 标签选中前景色
        private Color lable_sel_font_color = ColorTranslator.FromHtml("#fff");
        // 标签不选中背景色
        private Color lable_nosel_color = ColorTranslator.FromHtml("#fff");
        // 标签不选中前景色
        private Color lable_nosel_font_color = ColorTranslator.FromHtml("#000");
        // 标签边框色
        private Color lable_border_color = ColorTranslator.FromHtml("#BFBFBF");
        // 重绘事件
        protected override void OnPaint(PaintEventArgs e) {
            TabControl tabc_draw = this;
            Graphics g = e.Graphics;
            //设置高质量,低速度呈现平滑程度   
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
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
                    if (i == mousePageIndex) { 
                        // 鼠标当前标签样式
                        DrawMouseSelectPage(g, tabc_draw, mousePageIndex);
                    } else { 
                        // 未选中样式
                        DrawNoSelectPage(g, tabc_draw, i);
                    }
                }
                if((i == tabc_draw.SelectedIndex || i == mousePageIndex)
                    && IsShowDelPageBut && tabc_draw.TabCount > 1) { 
                    // 绘制当前鼠标下选项卡的关闭按钮
                    PageDrawDeleteBut(g, tabc_draw, mousePageIndex);
                    // 绘制选中选项卡的关闭按钮
                    PageDrawDeleteBut(g, tabc_draw, tabc_draw.SelectedIndex);
                }
            }
        }
        // 鼠标移动事件
        protected override void OnMouseMove(MouseEventArgs e) {
            // 赋值当前鼠标下的位置
            mousePagePoint = e.Location;
            // 当前鼠标下的标签索引
            mousePageIndex = getMouseLocPage(this, e.Location);
            // 判断当前鼠标下的删除标签按钮样式
            if (IsShowDelPageBut) { 
                int delIndex = getMouseDelPageIndex();
                if(delIndex >= 0) { 
                    PageDrawDeleteBut(this.CreateGraphics(),this, delIndex);
                } else { 
                    delIndex = getMouseLocPage(this, e.Location);
                    if(delIndex >= 0) PageDrawDeleteBut(this.CreateGraphics(),this, delIndex);
                }
            }
            
            base.OnMouseMove(e);
        }
        protected override void OnMouseEnter(EventArgs e) {
            base.OnMouseEnter(e);
        }
        // 设置鼠标移出事件
        protected override void OnMouseLeave(EventArgs e) {
            // 恢复变量默认值
            mousePageIndex = -1;
            mousePagePoint = Point.Empty;
            base.OnMouseLeave(e);
        }
        // 鼠标点击事件
        protected override void OnMouseClick(MouseEventArgs e) {
            mouseClickButtons = e.Button;
            // 判断是否显示移除标签对话框
            doIsDelPage(e.Button);
            base.OnMouseClick(e);
        }
        // 鼠标松开事件
        protected override void OnMouseUp(MouseEventArgs e) {
            mouseClickButtons = MouseButtons.None;
            base.OnMouseUp(e);
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
                int delIndex = getMouseLocPage(this, mousePagePoint);
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
        protected override void OnControlAdded(ControlEventArgs e) {
            base.OnControlAdded(e);
        }
        // 控件移除事件
        protected override void OnControlRemoved(ControlEventArgs e) {
            if(e.Control is TabPage) { 
                TabPage ppp = (TabPage)e.Control;
                // 确定移除标签后的显示模式
                delPageIsPageMode(ppp);
            }
            base.OnControlRemoved(e);
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
        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
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
                    if(rectangle.Contains(mousePagePoint)) { 
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

            Brush backbrush = new SolidBrush(lable_sel_color);// 标签背景色
            Brush fontbrush = new SolidBrush(lable_sel_font_color);// 标签字体颜色
            Color bordercolor = lable_sel_color;// 边框颜色
            // tab字体
            Font tabFont = new Font(tab.Font.SystemFontName, tab.Font.Size, tab.Font.Style);;// 标签字体
            // 绘制标签背景
            g.FillRectangle(backbrush, backrect);
            // 绘制标签边框
            //ControlsUtilsMet.setControlBorderStyle(g, backrect, ButtonBorderStyle.Solid
            //    ,1,1,1,0,bordercolor);
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

            Brush backbrush = new SolidBrush(lable_mouse_sel_color);// 标签背景色
            Brush fontbrush = new SolidBrush(lable_sel_font_color);// 标签字体颜色
            Color bordercolor = lable_mouse_sel_color;// 边框颜色
            // tab字体
            Font tabFont = new Font(tab.Font.SystemFontName, tab.Font.Size, tab.Font.Style);;// 标签字体
            // 绘制标签背景
            g.FillRectangle(backbrush, backrect);
            // 绘制标签边框
            //ControlsUtilsMet.setControlBorderStyle(g, backrect, ButtonBorderStyle.Solid
            //    ,1,1,1,0,bordercolor);
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

            Brush backbrush = new SolidBrush(lable_nosel_color);// 标签背景色
            Brush fontbrush = new SolidBrush(lable_nosel_font_color);// 标签字体颜色
            Color bordercolor = lable_border_color;// 边框颜色
            // tab字体
            Font tabFont = new Font(tab.Font.SystemFontName, tab.Font.Size, tab.Font.Style);;// 标签字体
            // 绘制标签背景
            g.FillRectangle(backbrush, backrect);
            // 绘制标签边框
            //ControlsUtilsMet.setControlBorderStyle(g, backrect, ButtonBorderStyle.Solid
            //    ,1,1,1,0,bordercolor);
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
                backrectStr.Y = backrectStr.Y + 0;
            } else { 
                _StringFlags.LineAlignment = StringAlignment.Center;
                backrectStr.Y = backrectStr.Y + 3;
            }
            g.DrawString(text, font, fontbrush, backrectStr, new StringFormat(_StringFlags));
            //char[] chars = text.ToCharArray();
            //for(int i=0; i< chars.Length; i++) { 
            //    Console.WriteLine(i);
            //    char c = chars[i];
            //    backrectStr.X = backrectStr.X +(4*i)+ i;
            //    g.DrawString(c.ToString(), font, fontbrush, backrectStr, new StringFormat(_StringFlags));
            //}
            
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
    }
}
