using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PubMethodLibrary;

namespace AllDllLoad
{
    public partial class DLLLoad
    {
        /// <summary>
        /// 空产构造函数
        /// </summary>
        public DLLLoad(){}
        //实例化MiscellaneousUtils
        private MessyUtilsMet messyUtilsMet = new MessyUtilsMet();
        //实例化WindowsApiUtils
        private WinApiUtilsMet winApiUtilsMet = new WinApiUtilsMet();
        //实例化MenuItemAop
        private MenuItemUtilsMet menuItemUtilsMet = new MenuItemUtilsMet();
        /// <summary>
        /// 封装实例化后的MenuItemAop
        /// </summary>
        public MenuItemUtilsMet MenuItemUtilsMet
        {
            get { return menuItemUtilsMet; }
            set { menuItemUtilsMet = value; }
        }
        /// <summary>
        /// 封装实例化后的WindowsApiUtils
        /// </summary>
        public WinApiUtilsMet WinApiUtilsMet
        {
            get { return winApiUtilsMet; }
            set { winApiUtilsMet = value; }
        }
        /// <summary>
        /// 封装实例化后的MiscellaneousUtils
        /// </summary>
        public MessyUtilsMet MessyUtilsMet
        {
            get { return messyUtilsMet; }
        }
    }
}
