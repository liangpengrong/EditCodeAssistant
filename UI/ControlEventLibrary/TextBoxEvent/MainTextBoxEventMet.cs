using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ui.ControlEventLibrary.TextBoxEvent {
    public class MainTextBoxEventMet {
        private MainTextBoxEventMet() { }
        /// <summary>
        /// 将文本框数据放入缓冲区
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        /// <param name="mouse"></param>
        public static bool setTextBoxCache(Dictionary<Type, object> data) {
            TextBox t = (TextBox)data[typeof(TextBox)];
            // ControlsUtilsMet.timersEventMet(t, 1000, delegate{ 
                if(!t.ReadOnly) TextBoxCache.addCacheFactory(t);
            // });
            return true;
        }
        /// <summary>
        /// 撤销缓存区文本
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        public static object cancelTextBoxCache (Dictionary<Type, object> data){
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 非只读才能撤销
            if (!t.ReadOnly) { 
                // 将文本框置于撤销状态
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_IS_CANCEL, true);
                TextBoxCache.cancelCache(t);
            }
            
            return null;
        }
        /// <summary>
        /// 恢复缓存区文本
        /// </summary>
        /// <param name="t"></param>
        public static object restoreTextBoxCache(Dictionary<Type, object> data){
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 非只读才能撤销
            if (!t.ReadOnly) { 
                // 将文本框置于恢复状态
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_IS_RESTORE, true);
                TextBoxCache.restoreCache(t);
            }
            
            return null;
        }
        /// <summary>
        /// 将文本框打开的文件路径显示到文本框的父容器下目录下
        /// </summary>
        /// <param name="t"></param>
        public static object setParentTextByFileName(Dictionary<Type, object> data) {
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 获取文本框的父容器
            Control con = t.Parent;
            // 判断父容器是否为TabPage
            if(con.GetType().Equals(typeof(TabPage))) {
                ControlsUtilsMet.asynchronousMet(t, 300, delegate{ 
                    // 判断Tag中是否存在保存路径
                    if(TextBoxUtilsMet.getDicTextTag(t).ContainsKey(TextBoxTagKey.SAVE_FILE_PATH)) {
                        string filepath = TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.SAVE_FILE_PATH].ToString();
                        TabPage page = (TabPage)t.Parent;
                        string[] pathArr = FileUtilsMet.getPathArr(filepath);
                        page.ResetText();
                        
                        // 设置标签文本
                        page.Text = pathArr[1]+"."+pathArr[2];
                        // 设置提示文本
                        page.ToolTipText = filepath;
                    }
                });
                
            }
            return null;
        }
        /// <summary>
        /// 设置状态栏的编码
        /// </summary>
        /// <param name="data"></param>
        public static object setToolSatrtEcoding(Dictionary<Type, object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 开辟新线程执行方法
            ControlsUtilsMet.asynchronousMet(t,300, delegate{ 
                Dictionary<string, object> tag = TextBoxUtilsMet.getDicTextTag(t);
                Encoding ecoding = TextBoxDataLibcs.TEXTBOX_ECODING_DEF;
                // 获取文本框中Tag中存的编码
                if(tag.ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING)) {
                    ecoding = (Encoding)TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING];
                }
                // 全局单例控件工厂
                Dictionary<string, Control> single = ControlCacheFactory.getSingletonCache();
                if(single.ContainsKey(EnumUtilsMet.GetDescription(DefaultNameEnum.TOOL_START))) { 
                    // 状态栏
                    ToolStrip toolStrip = (ToolStrip)single[EnumUtilsMet.GetDescription(DefaultNameEnum.TOOL_START)];
                    // 获取编码Item
                    ToolStripItem labEcoding = toolStrip.Items[StrutsStripDataLib.ItemName.编码];
                    labEcoding.Text = ecoding.BodyName.ToUpper();
                }
            });
            return null;
        }
    }
}
