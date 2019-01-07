using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace CacheFactory {
    /// <summary>
    /// 窗体缓存工厂类
    /// </summary>
    public class FormCache {
        /// <summary>
        /// 单例窗口工厂
        /// </summary>
        private static Dictionary<string, Form> singletonCache = new Dictionary<string, Form>();
        /// <summary>
        /// 多例窗口工厂
        /// </summary>
        private static Dictionary<string, Form[]> prototypeCahce = new Dictionary<string, Form[]>();
        
        /// <summary>
        /// 将窗体添加到单例工厂中
        /// </summary>
        /// <param name="singForm"></param>
        public static void addSingletonCache(Form singForm) { 
            string fNamer = singForm.Name;
            if(singletonCache.ContainsKey(fNamer)) { 
                singletonCache[fNamer] = singForm;
            } else { 
                singletonCache.Add(fNamer, singForm);
            }
        }
        /// <summary>
        /// 根据窗口名获取单例工厂中的对应窗口
        /// </summary>
        /// <param name="singFormName">窗口名</param>
        /// <returns>获取到的窗体</returns>
        public static Form getSingletonCache(string singFormName) { 
            if (singletonCache.ContainsKey(singFormName)) { 
                return singletonCache[singFormName];
            } else { 
                return null;    
            }   
        }
        
        
        
        
        
        
        
        
        /// <summary>
        /// 获取单例窗口工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form> getSingletonCache() { 
            return singletonCache;    
        }
        /// <summary>
        /// 设置单例窗口工厂
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">val</param>
        public static void setSingletonFactory(string key, Form val) {
            if(singletonCache.ContainsKey(key)) { 
                singletonCache[key] = val;
            }     
        }
        /// <summary>
        /// 获取多例窗口工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form[]> getPrototypeCache() { 
            return prototypeCahce;    
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}
