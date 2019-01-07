using CacheFactory;
using StaticDataLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SingleComponentFactory {
    /// <summary>
    /// 初始化单例控件
    /// </summary>
   public class InitSingleControl {
        private InitSingleControl() { }
        /// <summary>
        /// 初始化主Tab容器
        /// </summary>
        /// <returns>该Tab容器</returns>
        public static TabControl initMainTab()
        {
            // 获取主Tab容器
            TabControl tab = null;
            Control con = ControlCache.getSingletonCache(DefaultNameCof.tabContent);
            if(con == null) {
                tab = new TabControl();
                // 主窗体
                Form rootF = FormCache.getSingletonCache(DefaultNameCof.rootForm);
                // 顶部菜单
                Control topMenu = ControlCache.getSingletonCache(DefaultNameCof.topMenu);
                // 状态栏
                Control toolStart = ControlCache.getSingletonCache(DefaultNameCof.toolStart);
                // Name
                tab.Name = DefaultNameCof.tabContent;
                // 字体
                tab.Font = new Font("微软雅黑",8,FontStyle.Regular);
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
                // Tab容器宽
                tab.Width = rootF != null?rootF.ClientSize.Width:100;
                if(rootF != null && topMenu != null && toolStart != null) { 
                    // Tab容器高
                    tab.Height = rootF != null?rootF.ClientSize.Height - topMenu.Height - toolStart.Height : 100;
                    // Tab容器相对于窗体的位置
                    tab.Location = new Point(1, topMenu.Height);
                } else { 
                    tab.Height = 100;
                }
                ControlCache.addSingletonCache(tab);
            } else { 
                tab = (TabControl)con;
            }
            
            return tab;
        }

   }
}
