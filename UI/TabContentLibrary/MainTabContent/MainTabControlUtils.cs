﻿using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.TabContentLibrary.MainTabContent {
    public static class MainTabControlUtils {
        /// <summary>
        /// 确定添加标签后标签显示的模式
        /// </summary>
        /// <param name="tab"></param>
        public static void doIsAddPageSizeMode(TabControl tab) {
            if(tab.TabCount > 0 && tab.SelectedIndex > 0) { 
                int itemW = TabControlDataLib.DEF_ITEM_WIDTH;
                int itemCount = tab.TabCount;
                int rightMargin = 20;
                Size itemSize = Size.Empty;
                if(itemW * itemCount + rightMargin < tab.Width) { 
                    itemSize = new Size(itemW, tab.ItemSize.Height);
                    if(itemSize != Size.Empty && (!itemSize.Width.Equals(tab.ItemSize.Width) 
                        || !itemSize.Height.Equals(tab.ItemSize.Height))) { 
                        tab.ItemSize = itemSize;
                        tab.SizeMode = TabSizeMode.Fixed;
                    }
                    
                } else { 
                    int itemWW = ((tab.Width-rightMargin) / itemCount);
                    itemSize = new Size(itemWW, tab.ItemSize.Height);
                    if(itemSize != Size.Empty && (!itemSize.Width.Equals(tab.ItemSize.Width) 
                        || !itemSize.Height.Equals(tab.ItemSize.Height))) { 
                        tab.ItemSize = itemSize;
                        tab.SizeMode = TabSizeMode.Fixed;
                    }
                }
                // 确定每个标签的删除按钮的位置
                doIsDelPageButLocation(tab);
            }
            
        }
        /// <summary>
        /// 确定添加按钮的位置
        /// </summary>
        public static void doIsAddPageButLocation(Control addCon, TabControl tab) {
            if(addCon != null && tab != null) { 
                int itemW = tab.GetTabRect(tab.SelectedIndex).Width;
                int itemCount = tab.TabCount;
                int y = tab.Location.Y + (tab.ItemSize.Height+3 - addCon.ClientSize.Height)/2;
                int x = itemCount*itemW+8;
                if(x+addCon.Width+itemCount < tab.Width) { 
                    addCon.Location = new Point(x, y);
                } else { 
                    addCon.Location = new Point(tab.Width-addCon.Width-2, y);
                }
            }
        }
        /// <summary>
        /// 确定删除标签按钮的位置
        /// </summary>
        /// <param name="tab"></param>
        public static void doIsDelPageButLocation(TabControl tab) {
            int itemW = -1;
            int itemH = -1;
            int x = -1;
            int y = -1;
            for(int i=0,len=tab.TabCount; i<len;i++) {
                TabPage page = tab.TabPages[i];
                Dictionary<string,object> tag = ControlsUtilsMet.getControlTagToDic(page);
                if(tag != null && tag.ContainsKey(EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY)) && tab.TabCount > 0) { 
                    Control con = (Control) tag[EnumUtilsMet.GetDescription(DefaultNameEnum.DEF_BUTTON_TAG_KEY)];
                    // 确定关闭按钮的位置
                    itemW = tab.GetTabRect(i).Width;
                    itemH = tab.GetTabRect(i).Height;
                    if(con.Width + 10 > itemW || tab.TabCount == 1) { 
                        con.Visible = false;
                    }
                    x = tab.GetTabRect(i).X+itemW-con.Width-4;
                    y = tab.Location.Y + (itemH-con.Height)/2+con.Height/2;
                    con.Location = new Point(x,y);
                    con.BringToFront();
                }
            }
        }
        /// <summary>
        /// 移除TabPage
        /// </summary>
        /// <param name="tabPage"></param>
        public static void deleteTabPage(TabControl tab,TabPage tabPage) {
            if(tabPage != null && tabPage.Parent != null 
                && typeof(TabControl).Equals(tabPage.Parent.GetType())) { 
                TabControl tabControl = (TabControl)tabPage.Parent;
                if(tabControl.TabCount >1) { 
                    int selIndex = tab.SelectedIndex;
                    int delIndex = getTabIndex(tabControl, tabPage);
                    tabControl.Controls.Remove(tabPage);
                    // 确定移除标签后的要显示的标签
                    if(selIndex != -1 && selIndex.Equals(delIndex)) { 
                        isdelPageSelMode(tabControl, delIndex);
                    }
                    // 移除page标签所带有的删除按钮
                    Dictionary<string,object> tag = ControlsUtilsMet.getControlTagToDic(tabPage);
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
        public static void deleteTabPage(TabPage tabPage) {
            if(tabPage != null && tabPage.Parent != null && typeof(TabControl).Equals(tabPage.Parent.GetType())) { 
                TabControl tabControl = (TabControl)tabPage.Parent;
                deleteTabPage(tabControl, tabPage);
            }
        }
        /// <summary>
        /// 移除当前鼠标位置的TabPage
        /// </summary>
        /// <param name="tabPage"></param>
        public static void deleteTabPage(TabControl tab, Point mouseLoc) {
            // 获取当前鼠标位置的Page索引
            if(tab != null) { 
                int index = getMouseLocPage(tab, mouseLoc);
                if(index >= 0 && index < tab.TabCount) {
                    TabPage tabPage = tab.TabPages[index];
                    deleteTabPage(tab, tabPage);
                }
            }
        }
        /// <summary>
        /// 确定移除标签后将要显示的标签
        /// </summary>
        /// <param name="tab">Tab容器</param>
        /// <param name="delPageIndex">要删除的标签索引</param>
        private static void isdelPageSelMode(TabControl tab, int delPageIndex) { 
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
        public static int getMouseLocPage(TabControl tab, Point mouseLoc) {
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
        public static Control getDelPageButByPageTag(TabPage tabPage) { 
            Control retCon = null;
            if(tabPage != null) { 
                Dictionary<string,object> tag = ControlsUtilsMet.getControlTagToDic(tabPage);
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
        public static int getTabIndex(TabControl tabControl, Point point){
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
        public static int getTabIndex(TabControl tab, string pageName) { 
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
        public static int getTabIndex(TabControl tab, TabPage page) { 
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
            int index = getTabIndex(tab, mouseLoc);
            if (index != -1) {
                Control del = null;
                Rectangle tabRect = Rectangle.Empty;
                TabPage page = null;
                tabRect = tab.GetTabRect(index);
                page = tab.TabPages[index];
                del = getDelPageButByPageTag(page);
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
    }
}