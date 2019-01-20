using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.ToolStripContainerLibrary {
    public class MainToolStripContainer {
        /// <summary>
        /// 初始化主容器
        /// </summary>
        /// <returns></returns>
        public static ToolStripContainer initToolStripContainer() { 
            ToolStripContainer stripContainer = null;
            Control con = ControlCache.getSingletonCache(DefaultNameCof.MAIN_CONTAINER);
            if(con == null) {
                stripContainer = new ToolStripContainer();
                stripContainer.Name = DefaultNameCof.MAIN_CONTAINER;
                stripContainer.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                stripContainer.Dock = DockStyle.None;
                // 设置不被焦点选中
                stripContainer.TabStop = false;
                // 容器的大小
                stripContainer.Size = new Size(1,1);
                stripContainer.Location = new Point(1,1);
                ControlCache.addSingletonCache(stripContainer);
            } else { 
                stripContainer = (ToolStripContainer)con;
            }
            return stripContainer;
        }
    }
}
