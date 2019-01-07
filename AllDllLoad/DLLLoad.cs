using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PublicMethodLibrary;

namespace AllDllLoad
{
    public partial class DLLLoad
    {
        /// <summary>
        /// 空产构造函数
        /// </summary>
        public DLLLoad(){}
        /// <summary>
        /// 封装实例化后的MenuItemAop
        /// </summary>
        public MenuItemUtilsMet MenuItemUtilsMet { get; set; } = new MenuItemUtilsMet();
        /// <summary>
        /// 封装实例化后的WindowsApiUtils
        /// </summary>
        public WinApiUtilsMet WinApiUtilsMet { get; set; } = new WinApiUtilsMet();
        /// <summary>
        /// 封装实例化后的MiscellaneousUtils
        /// </summary>
        public MessyUtilsMet MessyUtilsMet { get; } = new MessyUtilsMet();
    }
}
