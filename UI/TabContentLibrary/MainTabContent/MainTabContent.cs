using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary;
using UI.ComponentLibrary.ControlLibrary;

namespace UI.TabContentLibrary.MainTabContent {
    public class MainTabContent {
        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public static TabControl initMainTab() {
            // 获取单例模式的主Tab容器
            TabControl tab = null;
            // 获取主容器
            Control con = ControlCache.getSingletonCache(DefaultNameCof.TAB_CONTENT);
            if(con == null) {
                tab = new TabControl();
                // 主容器
                ToolStripContainer rootContainer = (ToolStripContainer)ControlCache.getSingletonCache(DefaultNameCof.MAIN_CONTAINER);
                // Name
                tab.Name = DefaultNameCof.TAB_CONTENT;
                // 字体
                tab.Font = new Font("微软雅黑",8,FontStyle.Regular);
                // 标签大小
                tab.ItemSize = new Size(TabControlDataLib.DEF_ITEM_WIDTH, TabControlDataLib.DEF_ITEM_HEIGHT);
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
                tab.DrawMode = TabDrawMode.Normal;
                // Tab容器宽
                tab.Size = new Size(1,1);
                // 重绘
                tab.DrawItem += Tab_DrawItem;
                // 添加控件事件
                tab.ControlAdded += (object sender, ControlEventArgs e) =>{ 
                    // 确定显示模式
                    MainTabControlUtils.doIsAddPageSizeMode(tab);
                    // 添加关闭按钮
                    if(MainTabControlConfig.IS_SHOW_DEL_BUTTON) addDelPageBut(tab);

                };
                // 移除控件事件
                tab.ControlRemoved += (object sender, ControlEventArgs e) =>{ 
                    tab.Controls.Remove(e.Control);
                    // 确定显示模式
                    MainTabControlUtils.doIsAddPageSizeMode(tab);
                    // 确定添加按钮的位置
                    MainTabControlUtils.doIsAddPageButLocation(ControlCache.getSingletonCache(DefaultNameCof.ADD_PAGE_BUTTON) ,tab);
                };
                // 鼠标按下事件
                tab.MouseClick += (object sender, MouseEventArgs e) =>{
                    // 按下滚轮关闭标签
                    if(MouseButtons.Middle.Equals(e.Button) && MainTabControlConfig.IS_CLICK_MIDDLE_DEL_PAGE 
                        && tab.TabCount > 1) { 
                        MainTabControlUtils.deleteTabPage(tab, e.Location);
                    }
                };
                // 调整大小事件
                tab.Resize += (object sender, EventArgs e) =>{
                    ControlsUtilsMet.asynchronousMet(tab, 100, delegate{ 
                        // 确定显示模式
                        MainTabControlUtils.doIsAddPageSizeMode(tab);
                        // 确定添加按钮的位置
                        MainTabControlUtils.doIsAddPageButLocation(ControlCache.getSingletonCache(DefaultNameCof.ADD_PAGE_BUTTON) ,tab);
                    });
                    
                };
                // 鼠标移动事件
                tab.MouseMove += (object sender, MouseEventArgs e) =>{
                    // 当鼠标移出标签栏是隐藏删除按钮
                    ControlsUtilsMet.asynchronousMet(tab,50,delegate{
                        MainTabControlUtils.doIsMouselocHideDelBut(tab, e.Location);
                    });
                };
                // 加入到缓存中
                ControlCache.addSingletonCache(tab);
            } else { 
                tab = (TabControl)con;
            }
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
            TabPage page = new TabPage();
            // 设置Page的背景颜色为白色
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            page.BackColor = Color.White;
            page.Name = TabDataLib.PAGE_NAME_DEF + timeStr;
            page.Text = TabDataLib.PAGE_TEXT;
            page.UseVisualStyleBackColor = true;
            // 设置Page的大小
            page.Size = new Size(1, 1);
            // 进入控件事件
            page.Enter += (object sender, EventArgs e) =>{ 
                TabPage pp = (TabPage)sender;
                Control con = null;
                con = page.Controls[page.Controls.Count-1];
                pp.FindForm().ActiveControl = con;
            };
            return page;
        }
         /// <summary>
        /// 添加关闭按钮
        /// </summary>
        /// <param name="tab"></param>
        public static void addDelPageBut(TabControl tab) { 
            string tagKey = TabControlDataLib.DEF_BUTTON_TAG_KEY;
            Panel del = null;
            foreach(TabPage page in tab.TabPages) {
                int selIndex = tab.TabCount-1;
                int itemW = tab.GetTabRect(selIndex).Width;
                int itemH = tab.GetTabRect(selIndex).Height;
                // 获取page页保存的tag数据
                Dictionary<string, object> tag = ControlsUtilsMet.getControlTagToDic(page);
                // 判断tag中是否存在删除按钮
                if(page.Tag != null && tag != null && tag.ContainsKey(tagKey) && tag[tagKey] != null) {
                    del = (Panel)tag[tagKey];
                    // 判断删除按钮是否不存在其父窗体
                    if(tab.Parent != null && !tab.Parent.Controls.Contains(del)) { 
                        tab.Parent.Controls.Add(del);
                        del.BringToFront();
                    }
                } else {
                    // 创建一个新的删除按钮
                    del = getDelPageBut();
                    // 确定关闭按钮的位置
                    int x = tab.GetTabRect(selIndex).X+itemW-del.Width-4;
                    int y = tab.Location.Y + (itemH-del.Height)/2+del.Height/2;
                    del.Location = new Point(x,y);
                    ControlsUtilsMet.setControlTag(page, tagKey, del);
                    if(tab.Parent != null){ 
                        tab.Parent.Controls.Add(del);
                        del.BringToFront();
                    }
                }
                // 当按钮超出标签的宽度或当前只有一个标签时隐藏删除按钮否则显示按钮
                if(del.Width+10 > itemW || tab.TabCount == 1) { 
                    del.Visible = false;
                } else { 
                    del.Visible = true;
                }
                del.Visible = false;
                del.BackColor = page.BackColor;
                // 按钮的点击关闭标签事件
                del.MouseClick += (object sender, MouseEventArgs e) =>{
                    Panel pp = (Panel)sender;
                    if(MouseButtons.Left.Equals(e.Button)) {
                        if(page.Parent != null && tab.TabCount>1) { 
                            TabControl tabControl1 = (TabControl)page.Parent;
                            // 删除标签
                            MainTabControlUtils.deleteTabPage(page);
                            // 确定删除标签按钮的位置
                            MainTabControlUtils.doIsDelPageButLocation(tabControl1);
                            
                        }
                    }
                    
                };
                
            }
        }
        /// <summary>
        /// 获取关闭按钮
        /// </summary>
        /// <returns></returns>
        private static Panel getDelPageBut() { 
            Panel panel = new Panel();
            panel.Size = new Size(9,9);
            panel.TabStop = false;
            panel.TabIndex = int.MaxValue;
            panel.BackColor = Color.Transparent;
            panel.Name = DefaultNameCof.DEL_PAGE_BUTTON+DateTime.Now.ToUniversalTime().Ticks;
            // 鼠标是否移入
            bool mouseMove = false;
            // 消除控件重绘闪烁
            ControlsUtilsMet.clearRedrawFlashing(panel);
            panel.Paint += (object sender, PaintEventArgs e) =>{ 
                
                Panel ppp = (Panel)sender;
                Color lineColor = ColorTranslator.FromHtml("#808080");
                // 线得高
                int height = 2;
                // 线的宽
                int width = (int) Math.Sqrt((panel.Height*panel.Height)*(panel.Width*panel.Width));
                // 鼠标移入移出
                if(mouseMove) {
                    lineColor = ColorTranslator.FromHtml("#DD5252");
                } else {
                    lineColor = ColorTranslator.FromHtml("#808080");
                }
                // 中间加号颜色
                Pen pen = new Pen(lineColor, height);
                // 绘制第一条线
                e.Graphics.DrawLine(pen, 0, 0, panel.Width-height, panel.Height-height);
                // 绘制第二条线
                e.Graphics.DrawLine(pen, panel.Width-height, 0, 0, panel.Height-height);
            };
            // 鼠标移入事件
            panel.MouseEnter += (object sender, EventArgs e) =>{
                Panel ppp = (Panel)sender;
                mouseMove = true;
                ppp.Invalidate();
                
            };
            // 鼠标移出事件
            panel.MouseLeave += (object sender, EventArgs e) =>{ 
                Panel ppp = (Panel)sender;
                mouseMove = false;
                ppp.Invalidate();
            };
            return panel;
        }
        // 重绘
        private static void Tab_DrawItem(object sender, DrawItemEventArgs e) {
            TabControl tabControl = (TabControl)sender;
            TabPage page = tabControl.TabPages[e.Index];
            // tabControl.BorderStyle = BorderStyle.FixedSingle;
            e.Graphics.Clear(page.BackColor);
            // 获得选项卡标题区域
            Rectangle pageRec = tabControl.GetTabRect(e.Index);
            // 文本内容
            e.Graphics.DrawString(tabControl.TabPages[e.Index].Text,
            tabControl.Font,SystemBrushes.ControlText, pageRec.X + 4, pageRec.Y + 2);
            // 边框
            tabDrawBorder(tabControl ,e);
            e.Graphics.Dispose();
        }
        private static void tabDrawBorder(TabControl sender, DrawItemEventArgs e) {
            Pen pen = new Pen(ColorTranslator.FromHtml("#9F9F9F"), 1);
            // sender.TabPages.Clear();
            // 左边框
            e.Graphics.DrawLine(pen,0,0,0,sender.ClientSize.Height); 
            // 上边框
            // e.Graphics.DrawLine(pen,0,0,0,sender.Height); 
            // 右边框
            //e.Graphics.DrawLine(pen,sender.Width,0,sender.Width,sender.Height); 
            // 下边框
            //e.Graphics.DrawLine(pen,0,sender.ClientSize.Height,sender.ClientSize.Width,sender.ClientSize.Height);
        }
    }
}
