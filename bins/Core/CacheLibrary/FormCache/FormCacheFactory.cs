using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;

namespace Core.CacheLibrary.FormCache {
    /// <summary>
    /// 窗体缓存工厂类
    /// </summary>
    public static class FormCacheFactory {
        // 单例窗口缓存
        private static Dictionary<string, Form> singletonCache = new Dictionary<string, Form>();
        // 多例窗口缓存
        private static Dictionary<string, Form[]> prototypeCache = new Dictionary<string, Form[]>();
        // 顶层窗口缓存
        private static Dictionary<string, Form> topFormCache = new Dictionary<string, Form>();

        // 清除缓冲工厂中的僵死窗体的定时任务
        public static void clearDeaFormTimers() { 
            void clearDeaForm() {
                // 清除单例模式的僵死窗体
                for (int i=singletonCache.Count-1; i>=0; i--) { 
                    string key = singletonCache.Keys.ToArray()[i];
                    Form vla = singletonCache[key];
                    if(vla == null || vla.IsDisposed){ 
                        singletonCache.Remove(key);
                    }
                }
                // 清除多例模式的僵死窗体
                for (int i=prototypeCache.Count-1; i>=0; i--) { 
                    string key = prototypeCache.Keys.ToArray()[i];
                    List<Form> formArr = prototypeCache[key].ToList();
                    for (int j= formArr.Count-1; j >= 0; j--) { 
                        Form f = formArr[j];
                        if(f == null || f.IsDisposed){ 
                            formArr.Remove(f);
                        } 
                    }
                    if(formArr == null || formArr.Count == 0) {
                        prototypeCache.Remove(key);
                    } else { 
                        prototypeCache[key] = formArr.ToArray();
                    }
                }
                // 清除顶层窗口缓存中僵死窗体
                for (int i=topFormCache.Count-1; i>=0; i--) { 
                    string key = topFormCache.Keys.ToArray()[i];
                    Form vla = topFormCache[key];
                    if(vla == null || vla.IsDisposed){ 
                        topFormCache.Remove(key);
                    } 
                }
            }
            // 实例化Timer类，设置时间间隔
            System.Timers.Timer timer = new System.Timers.Timer(600000);
            // 到达时间的时候执行事件
            timer.Elapsed += new System.Timers.ElapsedEventHandler(delegate{ 
                clearDeaForm();
            });
            // 设置是执行一次（false）还是一直执行(true)
            timer.AutoReset = true;
            // 是否执行System.Timers.Timer.Elapsed事件
            timer.Enabled = true;
        }
        /// <summary>
        /// 将窗体添加到单例工厂中
        /// </summary>
        public static bool addSingletonCache(Form singForm) { 
            bool retBool = false;
            try {
                if(singForm == null) return retBool;
                string fNamer = singForm.Name;
                if(singletonCache.ContainsKey(fNamer)) { 
                    singletonCache[fNamer] = singForm;
                } else { 
                    singletonCache.Add(fNamer, singForm);
                }
                retBool = true;
            } catch(Exception ex) { 
                retBool = false;
            }
            return retBool;
            
        }
        /// <summary>
        /// 将窗体添加到多例工厂中
        /// </summary>
        public static bool addPrototypeCache(DefaultNameEnum name, Form protForm) {
            bool retBool = false;
            try {
                string key = EnumUtilsMet.GetDescription(name);
                if(protForm == null) return retBool;
                if(prototypeCache.ContainsKey(key)) { 
                    Form[] formArr = prototypeCache[key];
                    List<Form> formL = formArr.ToList();
                    formL.Add(protForm);
                    prototypeCache[key] = formL.ToArray();
                } else { 
                    prototypeCache.Add(key, new Form[]{protForm});
                }
                retBool = true;
            } catch(Exception ex) { 
                retBool = false;
            }
            return retBool;
            
        }
        /// <summary>
        /// 将窗体添加到顶层窗口缓存
        /// </summary>
        public static void addTopFormCache(Form topform) { 
            if(topform == null) return;
            string fNamer = topform.Name;
            if(topFormCache.ContainsKey(fNamer)) { 
                topFormCache[fNamer] = topform;
            } else { 
                topFormCache.Add(fNamer, topform);
            }
        }
        /// <summary>
        /// 根据窗口名获取单例工厂中的对应窗口
        /// </summary>
        /// <param name="name">窗口名</param>
        /// <returns>获取到的窗体</returns>
        public static Form getSingletonCache(DefaultNameEnum name) { 
            string key = EnumUtilsMet.GetDescription(name);
            if (singletonCache.ContainsKey(key)) { 
                return singletonCache[key];
            } else { 
                return null;    
            }   
        }
        /// <summary>
        /// 根据窗口名获取多例工厂中的对应窗口
        /// </summary>
        /// <param name="name">窗口名</param>
        /// <returns>获取到的窗体</returns>
        public static Form[] getPrototypeCache(DefaultNameEnum name) { 
            string key = EnumUtilsMet.GetDescription(name);
            if (prototypeCache.ContainsKey(key)) { 
                return prototypeCache[key];
            } else { 
                return null;    
            }   
        }
        /// <summary>
        /// 移除在多例窗口缓存的指定窗体
        /// </summary>
        /// <param name="key"></param>
        /// <param name="protForm"></param>
        public static void remPrototypeCache(DefaultNameEnum name, Form protForm) { 
            string key = EnumUtilsMet.GetDescription(name);
            if(protForm == null) return;
            if(prototypeCache.ContainsKey(key)) { 
                Form[] formArr = prototypeCache[key];
                List<Form> formL = formArr.ToList();
                if(formL.Contains(protForm)) formL.Remove(protForm);
                prototypeCache[key] = formL.ToArray();
            }
        }
        /// <summary>
        /// 单例窗体的实例化方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form">实例化后的单例窗体</param>
        /// <param name="isShow">是否show</param>
        /// <returns></returns>
        public static T ininSingletonForm<T> (T form, bool isShow)where T:Form {
            if (form != null) { 
                // 判断单例工厂中是否不存在该窗体
                if (!getSingletonCache().ContainsKey(form.Name)) {
                    // 添加到缓存工厂中
                    addSingletonCache(form);
                }else if (getSingletonCache()[form.Name] == null) { // 如果存在判断是否为null
                    // 添加到缓存工厂中
                    addSingletonCache(form);
                } else { 
                    Form ff = getSingletonCache()[form.Name];
                    // 判断窗口是否已经关闭
                    if(ff.IsDisposed) { 
                        // 添加到缓存工厂中
                        addSingletonCache(form);
                    }
                }
                if(isShow) form.Show();
                form.Activate();
            }
            return form;
        }
        /// <summary>
        /// 获取单例窗口工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form> getSingletonCache() { 
            return singletonCache;    
        }
        /// <summary>
        /// 顶层和非顶层窗口缓存
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form> getTopFormCache() { 
            return topFormCache;    
        }
        /// <summary>
        /// 获取多例窗口工厂
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, Form[]> getPrototypeCache() { 
            return prototypeCache;    
        }
    }
}
