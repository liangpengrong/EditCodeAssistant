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
using UI.ComponentLibrary.ControlLibrary;
using UI.ComponentLibrary.ControlLibrary.RightMenu;

namespace UI.ComponentLibrary.MethodLibrary.Util {
    public static class MainTabControlUtils {
        /// <summary>
        /// 将控件添加到Tab容器中
        /// </summary>
        /// <param name="con">控件</param>
        /// <param name="isSynSize">是否同步大小</param>
        /// <param name="isAnchor">是否锚定</param>
        public static void AddControlsToPage(TabControl tab, TabPage page, Control con, bool isSynSize, bool isAnchor) {
            // 获得Page
            page.Controls.Add(con);
            tab.TabPages.Add(page);
            tab.SelectedTab = page;
            tab.FindForm().ActiveControl = con;
            if (isSynSize) con.Size = new Size(page.ClientSize.Width - con.Location.X, page.ClientSize.Height - con.Location.Y);
            // 设置文本框四周锚定
            if (isAnchor) con.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }
        /// <summary>
        /// 将窗体数组添加到Tab容器中
        /// </summary>
        /// <param name="forms">窗体数组</param>
        /// <param name="isSynSize">是否同步大小</param>
        /// <param name="isAnchor">是否锚定</param>
        public static void AddControlsToPage(TabControl tab, TabPage page, Form form, bool isSynSize, bool isAnchor) {
            tab.TabPages.Add(page);
            Size minSize = form.MinimumSize;
            // 判断是否设置了最小大小
            if (page.FindForm().Size.Width < minSize.Width) {
                page.FindForm().Width = minSize.Width;
            }
            if (page.FindForm().Size.Height < minSize.Height) {
                page.FindForm().Height = minSize.Height;
            }
            page.Text = form.Text;
            page.ToolTipText = form.Text;

            if (isSynSize) form.ClientSize = new Size(page.Size.Width, page.Size.Height - 2);
            if (isAnchor) form.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            form.FormBorderStyle = FormBorderStyle.None;
            form.AutoScaleMode = AutoScaleMode.None;
            form.TopLevel = false;
            form.Parent = page;
            tab.FindForm().ActiveControl = form;
            tab.SelectedTab = page;

            form.BringToFront();
            form.Show();
            // 设置窗体属性
            form.Location = new Point(0, 2);
        }
        /// <summary>
        /// 获取新标签的文本框
        /// </summary>
        public static TextBox GetNewPageTextBox() { 
            Control con = null;
            con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(con is RedrawTabControl) { 
                RedrawTabControl redrawTab = (RedrawTabControl)con;
                redrawTab.TransferDddPageInvoke();
                // 获取标签容器中当前标签的文本框
                con = ControlsUtilsMet.GetControlByName(redrawTab.SelectedTab.Controls, EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF), true);
            }
            // 转化为文本框
            TextBox textBox = con != null && con is TextBox?(TextBox)con : null;
            return textBox;
        }
        /// <summary>
        /// 导出数据到新标签
        /// </summary>
        public static void ExportNewPage(string s) { 
            // 获取当前程序的标签容器
            Control con = null;
            con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
            if(con is RedrawTabControl) { 
                RedrawTabControl redrawTab = (RedrawTabControl)con;
                redrawTab.TransferDddPageInvoke();
                // 获取标签容器中当前标签的文本框
                con = ControlsUtilsMet.GetControlByName(redrawTab.SelectedTab.Controls, EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF), true);
            }
            // 转化为文本框
            TextBox textBox = con != null && con is TextBox?(TextBox)con : null;
            ControlsUtilsMet.ExportTextBox(textBox, s);
        }
        /// <summary>
        /// 移除TabPage
        /// </summary>
        /// <param name="tabPage"></param>
        public static void DeleteTabPage(TabControl tab,TabPage tabPage) {
            if(tabPage != null && tab != null) { 
                if(tab.TabCount >1) { 
                    int selIndex = tab.SelectedIndex;
                    int delIndex = GetTabIndex(tab, tabPage);
                    tab.TabPages.Remove(tabPage);
                    // 移除page标签所带有的删除按钮
                    Dictionary<string,object> tag = ControlsUtilsMet.GetControlTagToDic(tabPage);
                    if(tag != null && tag.ContainsKey(EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY))) { 
                        Control con = (Control)tag[EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY)];
                        if(con != null && !con.IsDisposed) { 
                            con.Dispose();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 移除TabPage
        /// </summary>
        /// <param name="tabPage"></param>
        public static void DeleteTabPage(TabPage tabPage) {
            if(tabPage != null && tabPage.Parent != null && typeof(TabControl).Equals(tabPage.Parent.GetType())) { 
                TabControl tabControl = (TabControl)tabPage.Parent;
                DeleteTabPage(tabControl, tabPage);
            }
        }
        /// <summary>
        /// 移除当前鼠标位置的TabPage
        /// </summary>
        /// <param name="tabPage"></param>
        public static void DeleteTabPage(TabControl tab, Point mouseLoc) {
            // 获取当前鼠标位置的Page索引
            if(tab != null) { 
                int index = GetMouseLocPage(tab, mouseLoc);
                if(index >= 0 && index < tab.TabCount) {
                    TabPage tabPage = tab.TabPages[index];
                    DeleteTabPage(tab, tabPage);
                }
            }
        }
        /// <summary>
        /// 确定移除标签后将要显示的标签
        /// </summary>
        /// <param name="tab">Tab容器</param>
        /// <param name="delPageIndex">要删除的标签索引</param>
        public static void DoIsdelPageSelMode(TabControl tab, int delPageIndex) { 
            int type = MainTabControlConfig.DEL_PAGE_SELECT_MODE;
            switch (type) { 
            // 向左
            case 0 :
                if(delPageIndex-1 >= 0 && delPageIndex-1 < tab.TabCount) { 
                    tab.SelectedIndex = delPageIndex-1;
                } else { 
                    if(tab.TabCount > 0) tab.SelectedIndex = 0;
                }
                break;
            // 向右
            case 1 : 
                if(delPageIndex >= 0 && delPageIndex < tab.TabCount) { 
                    tab.SelectedIndex = delPageIndex;
                } else { 
                    if(tab.TabCount > 0) tab.SelectedIndex = tab.TabCount-1;
                }
                break;
            }
        }
        /// <summary>
        /// 获取当前鼠标位置下的标签索引
        /// </summary>
        public static int GetMouseLocPage(TabControl tab, Point mouseLoc) {
            int index = -1;
            for (int i = 0; i < tab.TabPages.Count; i++) {
                if(tab.GetTabRect(i).Contains(mouseLoc)) {
                    index  = i;
                }
            }
            return index;
        }
        /// <summary>
        /// 获取保存在Page的Tag数据中的删除按钮
        /// </summary>
        /// <param name="tabPage"></param>
        /// <returns></returns>
        public static Control GetDelPageButByPageTag(TabPage tabPage) { 
            Control retCon = null;
            if(tabPage != null) { 
                Dictionary<string,object> tag = ControlsUtilsMet.GetControlTagToDic(tabPage);
                if(tag != null && tag.ContainsKey(EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY))) { 
                    retCon = (Control)tag[EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY)];
                }
            }
            return retCon;
            
        }
        /// <summary>
        /// 获取tab选项卡执行坐标处的page索引
        /// </summary>
        /// <param name="tabControl"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static int GetTabIndex(TabControl tabControl, Point point){
            int retInt = -1;
            for (int i = 0; i < tabControl.TabPages.Count; i++) {
                if (tabControl.GetTabRect(i).Contains(point)) {
                    retInt = i;
                    break;
                }
            }
            return retInt;
        }
        /// <summary>
        /// 通过标签名称获取当前标签的索引
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static int GetTabIndex(TabControl tab, string pageName) { 
            int index = -1;
            for(int i=0,len=tab.TabCount; i< len; i++) {
                TabPage pp = tab.TabPages[i];
                if(pp.Name.Equals(pageName)) { 
                    index = i;
                    break;
                }
            }
            return index;
            
       }
        /// <summary>
        /// 通过标签名称获取当前标签的索引
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static int GetTabIndex(TabControl tab, TabPage page) { 
            int index = -1;
            for(int i=0,len=tab.TabCount; i< len; i++) {
                TabPage pp = tab.TabPages[i];
                if(pp.Equals(page)) { 
                    index = i;
                    break;
                }
            }
            return index;
        }
        public static void doIsMouselocHideDelBut(TabControl tab, Point mouseLoc) { 
            int index = GetTabIndex(tab, mouseLoc);
            if (index != -1) {
                Control del = null;
                Rectangle tabRect = Rectangle.Empty;
                TabPage page = null;
                tabRect = tab.GetTabRect(index);
                page = tab.TabPages[index];
                del = GetDelPageButByPageTag(page);
                if(del != null && tab.TabCount > 1) { 
                    if(tabRect.Location.X + tabRect.Width*0.1 >= mouseLoc.X
                        || tabRect.Location.Y+tabRect.Height*0.1 >= mouseLoc.Y  
                        || tabRect.Right-tabRect.Width*0.05 <= mouseLoc.X
                        || tabRect.Bottom-tabRect.Height*0.1 <= mouseLoc.Y 
                        ) { 
                        del.Visible = false;
                    } else { 
                        del.Visible = true;
                    }
                } else { 
                    if(del != null) del.Visible = false;
                }
            }
        }
        /// <summary>
        /// 将标签转化为窗体
        /// </summary>
        /// <param name="tab">标签容器</param>
        /// <param name="index">索引</param>
        /// <param name="isRestore">关闭窗体是否还原标签</param>
        public static void PageToForm(TabControl tab, int index, bool isRestore) { 
            if(tab == null) return;
            TabPage page = tab.TabPages[index];
            Dictionary<string, object> tagDuc = new Dictionary<string, object>();
            Control[] conArr = new Control[page.Controls.Count];
            Form form = new Form();
            form.Show();
            form.Name = DateTime.Now.Ticks.ToString()+"_Form";
            form.Size = page.Size;
            form.Icon = tab.FindForm().Icon;
            form.Location = tab.FindForm().Location;
            form.Text = tab.FindForm().Text+"_"+page.Text;
            tagDuc.Add("page", page);
            tagDuc.Add("index", index);
            form.Tag = tagDuc;
            // 窗口关闭后还原到tab容器中
            if(isRestore) { 
                form.FormClosing += (object sender, FormClosingEventArgs e)=>{
                    object objTag = form.Tag;
                    if(objTag != null && objTag is Dictionary<string, object>) { 
                        Dictionary<string, object> dic = (Dictionary<string, object>)objTag;
                        TabPage pp = (TabPage)dic["page"];
                        int i = (int)dic["index"];
                        form.Controls.CopyTo(conArr, 0);
                        pp.Controls.AddRange(conArr);
                        tab.TabPages.Insert(i, pp);
                    }
                };
            }
            page.Controls.CopyTo(conArr, 0);
            form.Controls.AddRange(conArr);
            tab.TabPages.Remove(page);
            form.Activate();
        }
        /// <summary>
        /// 添加文本框到标签页的方法
        /// </summary>
        public static void AddMainTextToPage(TabPage page, TextBox t) {
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            // 判断要添加的标签是否为null，为null则新建一个标签并添加
            // t.Location = new Point(0, 2);
            t.Size = new Size(page.ClientSize.Width - t.Location.X, page.ClientSize.Height - t.Location.Y);
            if (page == null) { 
                Control ccc = page.Parent;
                if(ccc != null && ccc is TabControl)
                AddControlsToPage((TabControl)ccc, page, t, true, true);
            } else { 
                page.Controls.Add(t);
                t.BringToFront();
            }
            ControlsUtilsMet.TimersMethod(200, 1000, t, (object sender, ElapsedEventArgs e)=>{
                if(t.FindForm() !=  null) { 
                    t.FindForm().ActiveControl = t;
                    ((System.Timers.Timer)sender).Dispose();
                }
            });
        }
    }
}
