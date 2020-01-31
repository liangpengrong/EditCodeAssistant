using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Core.CacheLibrary.ControlCache {
    /// <summary>
    /// 控件缓存工厂类
    /// </summary>
    public class ControlCacheFactory {
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
        public static bool addSingletonCache(Control singCon) { 
            bool retBool = false;
            try {
                string fNamer = singCon.Name;
                if(singletonCache.ContainsKey(fNamer)) { 
                    singletonCache[fNamer] = singCon;
                } else { 
                    singletonCache.Add(fNamer, singCon);
                }
                retBool = true;
            } catch(Exception ex) { 
                retBool = false;
            }
            return retBool;
        }
        /// <summary>
        /// 将控件添加到单多例工厂中
        /// </summary>
        /// <param name="singCon"></param>
        public static bool addPrototypeCache(DefaultNameEnum name, Control protCon) { 
            bool retBool = false;
            try { 
                string key = EnumUtils.GetDescription(name);
                if(protCon == null) return retBool;
                if(prototypeCache.ContainsKey(key)) { 
                    Control[] arr = prototypeCache[key];
                    List<Control> list = arr.ToList();
                    list.Add(protCon);
                    prototypeCache[key] = list.ToArray();
                } else { 
                    prototypeCache.Add(key, new Control[]{protCon});
                }
                retBool = true;
            } catch(Exception ex) { 
                retBool = false;
            }
            
            return retBool;
        }
        /// <summary>
        /// 根据控件名获取单例工厂中的对应控件,无法获取则返回null
        /// </summary>
        /// <param name="singFormName">控件名</param>
        /// <returns>获取到的控件</returns>
        public static Control getSingletonCache(DefaultNameEnum singConName) { 
            string key = EnumUtils.GetDescription(singConName);
            if (singletonCache.ContainsKey(key)) { 
                return singletonCache[key];
            } else { 
                return null;    
            }
        }
        /// <summary>
        /// 获取指定单例控件中的某种类型的全部子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="singConName"></param>
        /// <returns></returns>
        public static T[] getSingletonChildCon<T>(DefaultNameEnum singConName) where T:Control{
            T[] retAll = null;
            // 全局单例控件工厂
            if(singletonCache.ContainsKey(EnumUtils.GetDescription(singConName))) { 
                // 全局单例控件工厂
                Dictionary<string, Control> single = ControlCacheFactory.getSingletonCache();
                if(single.ContainsKey(EnumUtils.GetDescription(DefaultNameEnum.TOOL_START)) && single.ContainsKey(EnumUtils.GetDescription(DefaultNameEnum.TAB_CONTENT))) { 
                    // 获取指定姓名的控件
                    Control tabParent = single[EnumUtils.GetDescription(singConName)];
                    if(tabParent != null) { 
                        List<T> conList = new List<T>();
                        ControlsUtils.GetAllControlByType(ref conList, tabParent.Controls);
                        if (conList != null && conList.Count > 0) { 
                            retAll = conList.ToArray();
                        }
                    }
                }
            }
            return retAll;
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
