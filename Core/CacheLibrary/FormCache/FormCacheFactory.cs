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
        private static Dictionary<string, Form[]> prototypeCahce = new Dictionary<string, Form[]>();
        // 顶层窗口缓存
        private static Dictionary<string, Form> topFormCahce = new Dictionary<string, Form>();

        // 清除缓冲工厂中的僵死窗体的定时任务
        public static void clearDeaFormTimers() { 
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
        /// 清除缓冲工厂中的僵死窗体
        /// </summary>
        public static void clearDeaForm() {
            // 清除单例模式的僵死窗体
            for (int i=singletonCache.Count-1; i>=0; i--) { 
                string key = singletonCache.Keys.ToArray()[i];
                Form vla = singletonCache[key];
                if(vla == null || vla.IsDisposed){ 
                    singletonCache.Remove(key);
                }
            }
            // 清除多例模式的僵死窗体
            for (int i=prototypeCahce.Count-1; i>=0; i--) { 
                string key = prototypeCahce.Keys.ToArray()[i];
                List<Form> formArr = prototypeCahce[key].ToList();
                for (int j= formArr.Count-1; j >= 0; j--) { 
                    Form f = formArr[j];
                    if(f == null || f.IsDisposed){ 
                        formArr.Remove(f);
                    } 
                }
                if(formArr == null || formArr.Count == 0) {
                    prototypeCahce.Remove(key);
                } else { 
                    prototypeCahce[key] = formArr.ToArray();
                }
            }
            // 清除顶层窗口缓存中僵死窗体
            for (int i=topFormCahce.Count-1; i>=0; i--) { 
                string key = topFormCahce.Keys.ToArray()[i];
                Form vla = topFormCahce[key];
                if(vla == null || vla.IsDisposed){ 
                    topFormCahce.Remove(key);
                } 
            }
        }
        /// <summary>
        /// 将窗体添加到单例工厂中
        /// </summary>
        public static void addSingletonCache(Form singForm) { 
            if(singForm == null) return;
            string fNamer = singForm.Name;
            if(singletonCache.ContainsKey(fNamer)) { 
                singletonCache[fNamer] = singForm;
            } else { 
                singletonCache.Add(fNamer, singForm);
            }
        }
        /// <summary>
        /// 将窗体添加到多例工厂中
        /// </summary>
        public static void addPrototypeCache(DefaultNameEnum name, Form protForm) {
            string key = EnumUtilsMet.GetDescription(name);
            if(protForm == null) return;
            if(prototypeCahce.ContainsKey(key)) { 
                Form[] formArr = prototypeCahce[key];
                List<Form> formL = formArr.ToList();
                formL.Add(protForm);
                prototypeCahce[key] = formL.ToArray();
            } else { 
                prototypeCahce.Add(key, new Form[]{protForm});
            }
        }
        /// <summary>
        /// 将窗体添加到顶层窗口缓存
        /// </summary>
        public static void addTopFormCahce(Form topform) { 
            if(topform == null) return;
            string fNamer = topform.Name;
            if(topFormCahce.ContainsKey(fNamer)) { 
                topFormCahce[fNamer] = topform;
            } else { 
                topFormCahce.Add(fNamer, topform);
            }
        }
        /// <summary>
        /// 根据窗口名获取单例工厂中的对应窗口
        /// </summary>
        /// <param name="singFormName">窗口名</param>
        /// <returns>获取到的窗体</returns>
        public static Form getSingletonCache(DefaultNameEnum singFormName) { 
            string key = EnumUtilsMet.GetDescription(singFormName);
            if (singletonCache.ContainsKey(key)) { 
                return singletonCache[key];
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
            if(prototypeCahce.ContainsKey(key)) { 
                Form[] formArr = prototypeCahce[key];
                List<Form> formL = formArr.ToList();
                if(formL.Contains(protForm)) formL.Remove(protForm);
                prototypeCahce[key] = formL.ToArray();
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
        public static Dictionary<string, Form> getTopFormCahce() { 
            return topFormCahce;    
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
