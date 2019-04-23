using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.ComponentLibrary.MethodLibrary.Interface {
    interface IComponentInitMode<T> {
        /// <summary>
        /// 实例化多例模式的对象
        /// </summary>
        /// <param name="isShowTop">是否为顶层对象</param>
        /// <returns></returns>
        T initPrototypeExample(bool isShowTop);
        /// <summary>
        /// 实例化单例模式的对象
        /// </summary>
        /// <param name="isShowTop">是否为顶层对象</param>
        /// <returns></returns>
        T initSingleExample(bool isShowTop);
    }
}
