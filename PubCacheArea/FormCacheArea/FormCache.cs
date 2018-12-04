using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;

namespace PubCacheArea {
    /// <summary>
    /// 窗体缓存工厂类
    /// </summary>
    public class FormCache {
        /// <summary>
        /// 单例窗口工厂
        /// </summary>
        private static Dictionary<string, Form> singletonFactory = new Dictionary<string, Form>();
        /// <summary>
        /// 多例窗口工厂
        /// </summary>
        private static Dictionary<string, Form[]> prototypeFactory = new Dictionary<string, Form[]>();
        
        /// <summary>
        /// 将窗体添加到单例工厂中
        /// </summary>
        /// <param name="singForm"></param>
        public static void addSingletonFac(Form singForm) { 
            String fNamer = singForm.Name;
            if(singletonFactory.ContainsKey(fNamer)) { 
                singletonFactory[fNamer] = singForm;
            } else { 
                singletonFactory.Add(fNamer, singForm);
            }
        }
        /// <summary>
        /// 根据窗口名获取单例工厂中的对应窗口
        /// </summary>
        /// <param name="singFormName">窗口名</param>
        /// <returns>获取到的窗体</returns>
        public static Form getSingletonForm(String singFormName) { 
            if (singletonFactory.ContainsKey(singFormName)) { 
                return singletonFactory[singFormName];
            } else { 
                return null;    
            }   
        }
        
        
        
        
        
        
        
        
        /// <summary>
        /// 获取单例窗口工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form> getSingletonFactory() { 
            return singletonFactory;    
        }
        /// <summary>
        /// 设置单例窗口工厂
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">val</param>
        public static void setSingletonFactory(String key, Form val) {
            if(singletonFactory.ContainsKey(key)) { 
                singletonFactory[key] = val;
            }     
        }
        /// <summary>
        /// 获取多例窗口工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form[]> getPrototypeFactory() { 
            return prototypeFactory;    
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }
}
