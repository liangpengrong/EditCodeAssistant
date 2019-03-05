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
    public class MainTabContent{
        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public static TabControl initMainTab() {
            // 获取单例模式的主Tab容器
            RedrawTabControl tab = null;
            // 获取主容器
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(con == null) {
                tab = new RedrawTabControl();
                // 主容器
                ToolStripContainer rootContainer = (ToolStripContainer)ControlCacheFactory.getSingletonCache(DefaultNameEnum.MAIN_CONTAINER);
                // Name
                tab.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.TAB_CONTENT);
                // 字体
                tab.Font = new Font("Microsoft YaHei Mono", 10, FontStyle.Regular);
                // 标签大小
                tab.ItemSize = new Size(TabControlDataLib.DEF_ITEM_WIDTH, TabControlDataLib.DEF_ITEM_HEIGHT);
                // 显示关闭按钮
                tab.IsShowDelPageBut = MainTabControlConfig.IS_SHOW_DEL_BUTTON;
                //按下滚轮关闭按钮
                tab.IsClickMiddleDelPage = MainTabControlConfig.IS_CLICK_MIDDLE_DEL_PAGE;
                // 删除按钮时是否询问
                tab.IsDelPageAsk =  MainTabControlConfig.IS_DEL_ASK;
                tab.Padding = new Point(0,0);
                tab.Margin = new Padding(0,0,0,0);
                // 设置不被焦点选中
                tab.TabStop = false;
                //设置主Tab容器的四周锚定
                tab.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                //设置主Tab容器标签的宽高
                tab.ItemSize = new Size(MainTabControlConfig.ITEM_WIDTH, MainTabControlConfig.ITEM_HEIGHT);
                // 选项卡方式
                tab.SizeMode = TabSizeMode.Fixed;
                // 显示工具提示
                tab.ShowToolTips = true;
                // Tab容器宽
                tab.Size = new Size(0,0);
                // 添加控件事件
                tab.ControlAdded += (object sender, ControlEventArgs e) =>{ 
                    // 确定显示模式
                    MainTabControlUtils.doIsAddPageSizeMode(tab);
                };
                // 移除控件事件
                tab.ControlRemoved += (object sender, ControlEventArgs e) =>{
                    if(e.Control is TabPage) { 
                        TabPage ppp = (TabPage)e.Control;
                        // 确定显示模式
                        MainTabControlUtils.doIsAddPageSizeMode(tab);
                        // 确定添加按钮的位置
                        MainTabControlUtils.doIsAddPageButLocation(ControlCacheFactory.getSingletonCache(DefaultNameEnum.ADD_PAGE_BUTTON) ,tab);
                    }
                };
                // 调整大小事件
                tab.Resize += (object sender, EventArgs e) =>{
                    ControlsUtilsMet.asynchronousMet(tab, 100, delegate{ 
                        // 确定显示模式
                        MainTabControlUtils.doIsAddPageSizeMode(tab);
                        // 确定添加按钮的位置
                        MainTabControlUtils.doIsAddPageButLocation(ControlCacheFactory.getSingletonCache(DefaultNameEnum.ADD_PAGE_BUTTON) ,tab);
                    });
                    
                };
                // 加入到缓存中
                ControlCacheFactory.addSingletonCache(tab);
            } else { 
                tab = (RedrawTabControl)con;
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
            int index = 1;
            // 获取主标签容器
            Control mainTab = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(mainTab != null && mainTab is TabControl) { 
                index = ((TabControl)mainTab).TabCount+1;
            }
            // 设置Page的背景颜色为白色
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            page.BackColor = Color.White;
            page.Name = TabControlDataLib.PAGE_NAME_DEF + timeStr;
            page.Text = TabControlDataLib.PAGE_TEXT+"_"+index;
            page.UseVisualStyleBackColor = true;
            page.Padding = new Padding(0,0,0,0);
            page.Margin = new Padding(0,0,0,0);
            page.ToolTipText = page.Text;
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
            string tagKey = EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY);
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
                    // 按钮的点击关闭标签事件
                    del.MouseClick += (object sender, MouseEventArgs e) =>{
                        Panel pp = (Panel)sender;
                        if(MouseButtons.Left.Equals(e.Button)) {
                            if(page.Parent != null && tab.TabCount > 1) { 
                                // 删除是否提示
                                if (MainTabControlConfig.IS_DEL_ASK) {
                                    ControlsUtilsMet.showAskMessBox("是否删除该标签", "删除标签", delegate{ 
                                        TabControl tabControl1 = (TabControl)page.Parent;
                                        // 删除标签
                                        MainTabControlUtils.deleteTabPage(page);
                                        // 确定删除标签按钮的位置
                                        MainTabControlUtils.doIsDelPageButLocation(tabControl1);
                                    }, null);
                                } else {
                                    TabControl tabControl1 = (TabControl)page.Parent;
                                    // 删除标签
                                    MainTabControlUtils.deleteTabPage(page);
                                    // 确定删除标签按钮的位置
                                    MainTabControlUtils.doIsDelPageButLocation(tabControl1);
                                }
                            }
                        }
                    
                    };
                }
                // 当按钮超出标签的宽度或当前只有一个标签时隐藏删除按钮否则显示按钮
                if(del.Width+10 > itemW || tab.TabCount == 1) { 
                    del.Visible = false;
                } else { 
                    del.Visible = true;
                }
                del.BackColor = page.BackColor;
            }
        }
        /// <summary>
        /// 将控件数组添加到Tab容器中
        /// </summary>
        /// <param name="cons">控件数组</param>
        /// <param name="isSynSize">是否同步大小</param>
        /// <param name="isAnchor">是否锚定</param>
        public static void addControlsToPage(Control con, bool isSynSize, bool isAnchor) { 
            // 获得主Tab容器
            TabControl mainTab = initMainTab();
            // 获得Page
            TabPage tabPage = null;
            tabPage = initMainTabPage();
            tabPage.Controls.Add(con);
            mainTab.TabPages.Add(tabPage);
            mainTab.SelectedTab = tabPage;
            mainTab.FindForm().ActiveControl = con;
            if(isSynSize) con.Size = new Size(tabPage.ClientSize.Width - con.Location.X, tabPage.ClientSize.Height - con.Location.Y);
            // 设置文本框四周锚定
            if(isAnchor) con.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            Control addBut = ControlCacheFactory.getSingletonCache(DefaultNameEnum.ADD_PAGE_BUTTON);
            // 确定按钮的位置
            MainTabControlUtils.doIsAddPageButLocation(addBut, mainTab);
            addBut.BringToFront();
        }
        /// <summary>
        /// 将窗体数组添加到Tab容器中
        /// </summary>
        /// <param name="forms">窗体数组</param>
        /// <param name="isSynSize">是否同步大小</param>
        /// <param name="isAnchor">是否锚定</param>
        public static void addControlsToPage(Form form, bool isSynSize, bool isAnchor) { 
            // 获得主Tab容器
            TabControl mainTab = initMainTab();
            // 获得Page
            TabPage tabPage = initMainTabPage();
            form.TopLevel = false;
            form.Parent = tabPage;
            mainTab.TabPages.Add(tabPage);
            if(isSynSize) form.Size = new Size(tabPage.ClientSize.Width, tabPage.ClientSize.Height);
            if(isAnchor) form.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            mainTab.FindForm().ActiveControl = form;
            mainTab.SelectedTab = tabPage;
                
            Control addBut = ControlCacheFactory.getSingletonCache(DefaultNameEnum.ADD_PAGE_BUTTON);
            // 确定按钮的位置
            MainTabControlUtils.doIsAddPageButLocation(addBut, mainTab);
            addBut.BringToFront();
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
            panel.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.DEL_PAGE_BUTTON)+DateTime.Now.ToUniversalTime().Ticks;
            // 鼠标是否移入
            bool mouseMove = false;
            // 消除控件重绘闪烁
            ControlsUtilsMet.clearRedrawFlashing(panel);
            panel.Paint += (object sender, PaintEventArgs e) =>{ 
                Panel ppp = (Panel)sender;
                Graphics g = e.Graphics;
                Color lineColor = ColorTranslator.FromHtml("#fff");
                // 线得高
                int height = 2;
                // 线的宽
                int width = (int) Math.Sqrt((panel.Height*panel.Height)*(panel.Width*panel.Width));
                // 鼠标移入移出
                if(mouseMove) {
                    lineColor = ColorTranslator.FromHtml("#DD5252");

                } else {
                    lineColor = ColorTranslator.FromHtml("#fff");
                }
                // 中间加号颜色
                Pen pen = new Pen(lineColor, height);
                // 重绘背景
                // 绘制第一条线
                g.DrawLine(pen, 0, 0, panel.Width-height, panel.Height-height);
                // 绘制第二条线
                g.DrawLine(pen, panel.Width-height, 0, 0, panel.Height-height);
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
    }
}
