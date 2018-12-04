using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace PubCacheArea {
    /// <summary>
    /// 控件缓存工厂类
    /// </summary>
    public class ControlCache {
        /// <summary>
        /// 单例控件工厂
        /// </summary>
        private static Dictionary<string, Control> singletonFactory = new Dictionary<string, Control>();
        /// <summary>
        /// 多例控件工厂
        /// </summary>
        private static Dictionary<string, Control[]> prototypeFactory = new Dictionary<string, Control[]>();
        
        /// <summary>
        /// 将控件添加到单例工厂中
        /// </summary>
        /// <param name="singCon"></param>
        public static void addSingletonFac(Control singCon) { 
            String fNamer = singCon.Name;
            if(singletonFactory.ContainsKey(fNamer)) { 
                singletonFactory[fNamer] = singCon;
            } else { 
                singletonFactory.Add(fNamer, singCon);
            }
        }
        /// <summary>
        /// 根据控件名获取单例工厂中的对应控件,无法获取则返回null
        /// </summary>
        /// <param name="singFormName">控件名</param>
        /// <returns>获取到的控件</returns>
        public static Control getSingletonCon(String singConName) { 
            if (singletonFactory.ContainsKey(singConName)) { 
                return singletonFactory[singConName];
            } else { 
                return null;    
            }   
        }
        /// <summary>
        /// 获取单例控件工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Control> getSingletonFactory() { 
                return singletonFactory;    
        }
        /// <summary>
        /// 获取多例控件工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Control[]> getPrototypeFactory() { 
                return prototypeFactory;    
        }
    }
}
