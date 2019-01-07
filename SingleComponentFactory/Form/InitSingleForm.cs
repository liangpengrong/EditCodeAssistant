using CacheFactory;
using ComponentLibrary;
using StaticDataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SingleComponentFactory {
    /// <summary>
    /// 初始化单例窗体
    /// </summary>
    public class InitSingleForm {
        private InitSingleForm() { }
        /// <summary>
        /// 打开查找和替换窗口
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// </summary>
        /// <returns></returns>
        public static FindAndReplace initFindAndReplace(TextBox t, bool isShow)
        {
            FindAndReplace findAndReplace = new FindAndReplace(t, t.FindForm());
            findAndReplace.Name = DefaultNameCof.findForm;
            // 将窗体放入单例窗体工厂中
            findAndReplace = ininSingletonForm(findAndReplace, isShow);
            findAndReplace.MinimumSize = findAndReplace.Size;
            return findAndReplace;
        }
        /// <summary>
        /// 打开分列窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static SplitCharsForm initSplitCharsForm(TextBox t, bool isShow)
        {
            SplitCharsForm splitChars = new SplitCharsForm(t);
            splitChars.Name = DefaultNameCof.splitCharsForm;
            splitChars = ininSingletonForm(splitChars, isShow);
            return splitChars;
        }
        /// <summary>
        /// 打开添加字符窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static AddCharsForm initAddCharsForm(TextBox t, bool isShow) {

            AddCharsForm addCharsForm = new AddCharsForm(t);
            addCharsForm.Name = DefaultNameCof.addCharsForm;
            addCharsForm = ininSingletonForm(addCharsForm, isShow);
            return addCharsForm;
        }
        /// <summary>
        /// 打开转到行窗体
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static RowGoToForm initRowGoToForm(TextBox t, bool isShow)
        {
            RowGoToForm rowGoToForm = new RowGoToForm(t);
            if(isShow) rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
        /// <summary>
        /// 打开统计字符窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static CharsStatistics initCharsStatistics(TextBox t, bool isShow)
        {
            CharsStatistics rowGoToForm = new CharsStatistics(t);
            if(isShow) rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
        /// <summary>
        /// 打开设置文本框编码窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static SetCodingForm initSetCodingForm(TextBox t, bool isShow) {
            SetCodingForm setCodingForm = new SetCodingForm(t);
            if(isShow) setCodingForm.ShowDialog();
            return setCodingForm;
        }
        /// <summary>
        /// 单例窗体的实例化方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form">实例化后的单例窗体</param>
        /// <param name="isShow">是否show</param>
        /// <returns></returns>
        private static T ininSingletonForm<T> (T form, bool isShow)where T:Form { 
            try {
                // 判断单例工厂中是否不存在该窗体
                if (!FormCache.getSingletonCache().ContainsKey(form.Name)) {
                    if(isShow) form.Show();
                    // 添加到缓存工厂中
                    FormCache.addSingletonCache(form);
                    return form;
                }
                // 如果存在判断是否为null
                if (FormCache.getSingletonCache()[form.Name] == null) { 
                    if(isShow) form.Show();
                    // 添加到缓存工厂中
                    FormCache.addSingletonCache(form);
                    return form;
                } else { 
                    T tt = (T)FormCache.getSingletonCache()[form.Name];
                    // 判断窗口是否已经关闭
                    if(tt.IsDisposed) { 
                        if(isShow) form.Show();
                        // 添加到缓存工厂中
                        FormCache.addSingletonCache(form);
                        return form;
                    }
                }
                form = (T)FormCache.getSingletonCache()[form.Name];
                form.Activate();
            } catch {  
                
            }
            return form;
        }
    }
}
