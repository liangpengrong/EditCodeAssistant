using System.Windows.Forms;
using StaticDataLibrary;
using PubCacheArea;
using System;
using PubMethodLibrary;
using System.Collections.Generic;
using System.Text;

namespace CharsToolset
{
    public class TextBoxEventMet
    {
        private TextBoxEventMet() { }
        /// <summary>
        /// 将文本框数据放入缓冲区
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        /// <param name="mouse"></param>
        public static Boolean setTextBoxCache(Dictionary<Type, object> data) {
            TextBox t = (TextBox)data[typeof(TextBox)];
            // ControlsUtilsMet.timersEventMet(t, 1000, delegate{ 
                KeyEventArgs keys = (KeyEventArgs)data[typeof(KeyEventArgs)];
                MouseEventArgs mouse = (MouseEventArgs)data[typeof(MouseEventArgs)];
                TextBoxCache.addCacheFactory(t, keys, mouse);
            // });
            return true;
        }

        /// <summary>
        /// 将文本框打开的文件路径显示到文本框的父容器下目录下
        /// </summary>
        /// <param name="t"></param>
        public static void setParentTextByFileName(Dictionary<Type, object> data) {
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 获取文本框的父容器
            Control con = t.Parent;
            // 判断父容器是否为TabPage
            if(con.GetType().Equals(typeof(TabPage))) {
                ControlsUtilsMet.timersEventMet(t, 300, delegate{ 
                    // 判断Tag中是否存在保存路径
                    if(TextBoxUtilsMet.getDicTextTag(t).ContainsKey(TextBoxTagKey.saveFilePath)) {
                        String filepath = TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.saveFilePath].ToString();
                        TabPage page = (TabPage)t.Parent;
                        String[] pathArr = FileUtilsMet.getPathArr(filepath);
                        // 设置标签文本
                        page.Text = pathArr[1]+"."+pathArr[2];
                        // 设置提示文本
                        page.ToolTipText = filepath;
                    }
                });
                
            }
        }
        /// <summary>
        /// 设置状态栏的编码
        /// </summary>
        /// <param name="data"></param>
        public static void setToolSatrtEcoding(Dictionary<Type, object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,300, delegate{ 
                Dictionary<String, object> tag = TextBoxUtilsMet.getDicTextTag(t);
            
                Encoding ecoding = TextBoxDataLibcs.textBEcodingDef;
                // 获取文本框中Tag中存的编码
                if(tag.ContainsKey(TextBoxTagKey.textEcoding)) {
                
                    ecoding = (Encoding)TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.textEcoding];
                }
            
                // 全局单例控件工厂
                Dictionary<String, Control> single = ControlCache.getSingletonFactory();
                if(single.ContainsKey(DefaultNameCof.toolStart)) { 
                    // 状态栏
                    ToolStrip toolStrip = (ToolStrip)single[DefaultNameCof.toolStart];
                    // 获取编码Item
                    ToolStripItem labEcoding = toolStrip.Items[StrutsStripDateLib.ItemName.编码];
                    labEcoding.Text = ecoding.BodyName.ToUpper();
                }
            });
        }
   }
}
