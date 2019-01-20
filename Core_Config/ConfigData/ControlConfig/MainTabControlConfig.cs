using Core.DefaultData.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core_Config.ConfigData.ControlConfig {
    public static class MainTabControlConfig {
        /// <summary>
        /// 标签宽度
        /// </summary>
        public static int ITEM_WIDTH = TabControlDataLib.DEF_ITEM_WIDTH;
        /// <summary>
        /// 标签高度
        /// </summary>
        public static int ITEM_HEIGHT = TabControlDataLib.DEF_ITEM_HEIGHT;
        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public static bool IS_SHOW_DEL_BUTTON = true; 
        /// <summary>
        /// 按下滚轮是否关闭标签
        /// </summary>
        public static bool IS_CLICK_MIDDLE_DEL_PAGE = false;
        /// <summary>
        /// 关闭标签后向(左-0 右-1)显示标签 
        /// </summary>
        public static int DEL_PAGE_SELECT_MODE = 1;

    }
}
