using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using UI.ComponentLibrary;
using UI.ComponentLibrary.ControlLibrary;

namespace UI.TabContentLibrary.MainTabContent {
    public class MainTabContent{
        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public static RedrawTabControl initMainTab() {
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
                tab.Font = new Font("Microsoft YaHei", 10, FontStyle.Regular);
                // 标签大小
                tab.ItemSize = new Size(TabControlDataLib.DEF_ITEM_WIDTH, TabControlDataLib.DEF_ITEM_HEIGHT);
                // 显示关闭按钮
                tab.IsShowDelPageBut = MainTabControlConfig.IS_SHOW_DEL_BUTTON;
                //按下滚轮关闭按钮
                tab.IsClickMiddleDelPage = MainTabControlConfig.IS_CLICK_MIDDLE_DEL_PAGE;
                // 删除按钮时是否询问
                tab.IsDelPageAsk =  MainTabControlConfig.IS_DEL_ASK;
                tab.Padding = new Padding(0,20,0,0);
                tab.Margin = new Padding(0,0,0,0);
                // 显示添加按钮
                tab.showAddPageButtton(true, true, delegate(){
                    TabPage page = initMainTabPage();
                    tab.TabPages.Add(page);
                    RedrawTextBox t = new RedrawTextBox();
                    MainTabControlUtils.addMainTextToPage(page , t);
                    tab.SelectedTab = page;
                });
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
        public static RedrawTabPage initMainTabPage() {
            // 实例化一个Page
            RedrawTabPage page = new RedrawTabPage();
            return page;
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
            mainTab.TabPages.Add(tabPage);
            Size minSize = form.MinimumSize;
            // 判断是否设置了最小大小
            if(tabPage.FindForm().Size.Width < minSize.Width) { 
                tabPage.FindForm().Width = minSize.Width;
            }
            if(tabPage.FindForm().Size.Height < minSize.Height) { 
                tabPage.FindForm().Height = minSize.Height;
            }
            tabPage.Text = form.Text;
            tabPage.ToolTipText = form.Text;
            
            if(isSynSize) form.ClientSize = new Size(tabPage.Size.Width, tabPage.Size.Height-2);
            if(isAnchor) form.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            form.FormBorderStyle = FormBorderStyle.None;
            form.AutoScaleMode = AutoScaleMode.None;
            form.TopLevel = false;
            form.Parent = tabPage;
            mainTab.FindForm().ActiveControl = form;
            mainTab.SelectedTab = tabPage;

            form.BringToFront();
            form.Show();
            // 设置窗体属性
            form.Location = new Point(0, 2);
        }
        /// <summary>
        /// 获取新标签的文本框
        /// </summary>
        public static TextBox getNewPageTextBox() { 
            Control con = null;
            con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(con is RedrawTabControl) { 
                RedrawTabControl redrawTab = (RedrawTabControl)con;
                redrawTab.addPageInvoke();
                // 获取标签容器中当前标签的文本框
                con = ControlsUtilsMet.getControlByName(redrawTab.SelectedTab.Controls, EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF), true);
            }
            // 转化为文本框
            TextBox textBox = con != null && con is TextBox?(TextBox)con : null;
            return textBox;
        }
        /// <summary>
        /// 导出数据到新标签
        /// </summary>
        public static void exportNewPage(string s) { 
            // 获取当前程序的标签容器
            Control con = null;
            con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(con is RedrawTabControl) { 
                RedrawTabControl redrawTab = (RedrawTabControl)con;
                redrawTab.addPageInvoke();
                // 获取标签容器中当前标签的文本框
                con = ControlsUtilsMet.getControlByName(redrawTab.SelectedTab.Controls, EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF), true);
            }
            // 转化为文本框
            TextBox textBox = con != null && con is TextBox?(TextBox)con : null;
            ControlsUtilsMet.exportTextBox(textBox, s);
        }
    }
}
