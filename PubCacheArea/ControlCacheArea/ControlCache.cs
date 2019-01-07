using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace CacheFactory {
    /// <summary>
    /// 控件缓存工厂类
    /// </summary>
    public class ControlCache {
        /// <summary>
        /// 单例控件缓存
        /// </summary>
        private static Dictionary<string, Control> singletonCache = new Dictionary<string, Control>();
        /// <summary>
        /// 多例控件缓存
        /// </summary>
        private static Dictionary<string, Control[]> prototypeCache = new Dictionary<string, Control[]>();
        
        /// <summary>
        /// 将控件添加到单例工厂中
        /// </summary>
        /// <param name="singCon"></param>
        public static void addSingletonCache(Control singCon) { 
            string fNamer = singCon.Name;
            if(singletonCache.ContainsKey(fNamer)) { 
                singletonCache[fNamer] = singCon;
            } else { 
                singletonCache.Add(fNamer, singCon);
            }
        }
        /// <summary>
        /// 根据控件名获取单例工厂中的对应控件,无法获取则返回null
        /// </summary>
        /// <param name="singFormName">控件名</param>
        /// <returns>获取到的控件</returns>
        public static Control getSingletonCache(string singConName) { 
            if (singletonCache.ContainsKey(singConName)) { 
                return singletonCache[singConName];
            } else { 
                return null;    
            }   
        }
        /// <summary>
        /// 根据控件名获取单例工厂中的对应控件,无法获取则返回null
        /// </summary>
        /// <param name="type">控件类型</param>
        /// <returns></returns>
        public static Control getSingletonCache(Type type) { 
            // 获取全部的单例控件
            Control[] conAll = singletonCache.Values.ToArray();
            conAll = conAll.Where(con => con.GetType().Equals(type)).ToArray();
            if(conAll.Length == 1) { 
                return conAll[0];
            } else { 
                return null;    
            }
        }
        /// <summary>
        /// 获取单例控件工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Control> getSingletonCache() { 
            return singletonCache; 
        }
        /// <summary>
        /// 获取多例控件工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Control[]> getPrototypeCache() { 
            return prototypeCache;
        }
    }
}
