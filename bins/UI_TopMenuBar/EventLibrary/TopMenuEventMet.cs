using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using Core.DefaultData.DataLibrary;
using Core_Config.ConfigData.ControlConfig;
using System.Drawing;
using Ui.ComponentLibrary.EventLibrary;

namespace UI_TopMenuBar.TopMenuEvent
{
    /// <summary>
    /// TopMenu控件的委托类
    /// </summary>
    public class TopMenuEventMet
    {
        private TopMenuEventMet(){}
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <returns></returns>
        public static object exitProgram(Dictionary<Type , object> data){ 
            FormUtisl.DropOut();
            return null;
        }
        /// <summary>
        /// 实例化文本选择对话框
        /// </summary>
        /// <returns>返回该对话框</returns>
        public static object openFileMethod(Dictionary<Type , object> data) {
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                // 获取新的文本框
                PublicEventMet.openFileMethod(null);
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 撤销缓存区文本
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        public static object cancelTextBoxCache (Dictionary<Type, object> data){
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                // 非只读状态才能撤销
                if (!t.ReadOnly) { 
                    // 将文本框置于撤销状态
                    TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_IS_CANCEL, true);
                    TextBoxCache.cancelCache(t);
                }
                
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 恢复缓存区文本
        /// </summary>
        /// <param name="t"></param>
        public static object restoreTextBoxCache(Dictionary<Type, object> data){
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                // 非只读状态才能恢复
                if (!t.ReadOnly) { 
                    // 将文本框置于恢复状态
                    TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_IS_RESTORE, true);
                    TextBoxCache.restoreCache(t);
                }
                
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 实例化文件保存对话框保存文本
        /// </summary>
        /// <param name="t">要保存内容的文本框</param>
        /// <returns></returns>
        public static object saveFileMethod(Dictionary<Type , object> data)
        {
            // 获取文本框
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                PublicEventMet.saveFileMethod(t);
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 判断文件应该是保存还是另存为
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object saveOrSaveas(Dictionary<Type , object> data)
        {
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                // 获取文本框
                TextBox t = (TextBox)data[typeof(TextBox)];
                // 获取文本框的Tag数据
                Dictionary<string,object> textDic = TextBoxUtils.GetTextTagToMap(t);
                // 定义初始化的路径
                object path = null;
                // 赋值路径
                if(textDic.ContainsKey(TextBoxTagKey.SAVE_FILE_PATH)) { 
                    textDic.TryGetValue(TextBoxTagKey.SAVE_FILE_PATH, out path);
                } else { path = "";}
                // 赋值编码
                Encoding encoding = Encoding.Default;
                if(textDic.ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING)) {
                    object ee = null;
                    textDic.TryGetValue(TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, out ee);
                    if(ee != null && ee is Encoding) encoding = (Encoding)ee;
                } else { path = "";}
                // 判断路径是否存在
                if (path!=null&&FileUtils.isFileUrl(path.ToString())) {
                    //if (path.ToString().Split('.')[path.ToString().Split('.').Length - 1].ToLower().Equals("txt"))
                    //{//判断文件后缀名是否为txt格式
                    if (MessageBox.Show(
                            "确定要保存并覆盖吗？" + System.Environment.NewLine + "文件路径为：" + path
                        , "保存提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        // 调用方法写入文件内容
                        FileUtils.FileWrite.WriteFile(path.ToString(), t.Text , encoding);
                    }
                    //} else {
                    //    MessageBox.Show("只保存TXT格式的文件");
                    //}
                }
                else{
                    saveFileMethod(data);
                }
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            
            return null;
        }
        /// <summary>
        /// 恢复文本框默认字体
        /// </summary>
        /// <returns></returns>
        public static object textBoxFontReset(Dictionary<Type , object> data) { 
            ControlsUtils.ShowAskMessBox("是否恢复全部文本框的默认字体", "恢复默认字体",
            delegate { 
                MainTextBConfig.TEXTBOX_FONT = TextBoxDataLibcs.TEXTBOX_FONT_DEF;
                // 获取tab容器中的全部的主文本框
                TextBox[] textAll = ControlCacheFactory.getSingletonChildCon<TextBox>(DefaultNameEnum.TAB_CONTENT);
                if(textAll != null) {
                    foreach(TextBox textB in textAll) {
                        if(textB.Name.IndexOf(EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF)) >= 0) { 
                            // 设置字体
                            textB.Font = TextBoxDataLibcs.TEXTBOX_FONT_DEF;
                        }
                    }
                }
            }, null);
            return null;
        }
        /// <summary>
        /// 实例化字体对话框
        /// </summary>
        /// <returns></returns>
        public static object fontDialogMethod(Dictionary<Type , object> data) {
            // 判断是否存在文本框
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                object obj = PublicEventMet.fontDialogMethod(t);
                if(obj != null && obj is Dictionary<string, object>) { 
                    Dictionary<string, object> dic = (Dictionary<string, object>)obj;
                    // 获取字体设置对话框
                    FontDialog fontD = dic.ContainsKey("1") && dic["1"] is FontDialog? (FontDialog)dic["1"] : null;
                    // 是否点击了确定
                    DialogResult ok = dic.ContainsKey("2") && dic["2"] is DialogResult? (DialogResult)dic["2"] : DialogResult.None;
                    if(DialogResult.OK.Equals(ok) && fontD !=  null) {
                        // 询问是否将该字体应用到全部的文本框
                        ControlsUtils.ShowAskMessBox("是否将该字体应用到全部的文本框中", "设置字体", 
                        delegate{ 
                            MainTextBConfig.TEXTBOX_FONT = fontD.Font;
                            // 获取tab容器中的全部的主文本框
                            TextBox[] textAll = ControlCacheFactory.getSingletonChildCon<TextBox>(DefaultNameEnum.TAB_CONTENT);
                            if(textAll != null) {
                                foreach(TextBox textB in textAll) {
                                    if(textB.Name.IndexOf(EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF)) >= 0) { 
                                        // 设置字体
                                        textB.Font = fontD.Font;
                                    }
                                }
                            }
                        }, null);
                    }
                }
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;    
        }

        /// <summary>
        /// 实现用记事本打开文本框内容
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object notepadOpenFile(Dictionary<Type , object> data) {
            // 获取文本框
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                FileUtils.TurnOnNotepad(t.Text);
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 设置是否自动换行
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object isAutoLine(Dictionary<Type , object> data) { 
            // 获取文本框
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                ToolStripMenuItem item = (ToolStripMenuItem)data[typeof(ToolStripMenuItem)];
                // 设置状态栏显示与隐藏
                bool check = item.Checked;
                t.WordWrap = check;
                MainTextBConfig.AUTO_WORDWRAP = check;
                // 获取tab容器中的全部的主文本框
                TextBox[] textAll = ControlCacheFactory.getSingletonChildCon<TextBox>(DefaultNameEnum.TAB_CONTENT);
                if(textAll != null) {
                    foreach(TextBox textB in textAll) {
                        if(textB.Name.IndexOf(EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF)) >= 0) { 
                            // 设置自动换行
                            textB.WordWrap = check;
                        }
                    }
                }
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 设置状态栏的显示与隐藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object isStartBarDisplay(Dictionary<Type , object> data) {
            // 获取文本框
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                ToolStripMenuItem item = (ToolStripMenuItem)data[typeof(ToolStripMenuItem)];
                // 全局单例控件工厂
                Dictionary<string, Control> single = ControlCacheFactory.getSingletonCache();
                if(single.ContainsKey(EnumUtils.GetDescription(DefaultNameEnum.TOOL_START)) && single.ContainsKey(EnumUtils.GetDescription(DefaultNameEnum.TAB_CONTENT))) { 
                    // 状态栏
                    Control toolStrip = single[EnumUtils.GetDescription(DefaultNameEnum.TOOL_START)];
                    // 标签容器的父容器
                    Control tabParent = single[EnumUtils.GetDescription(DefaultNameEnum.MAIN_CONTAINER)];
                    // 设置状态栏显示与隐藏
                    bool check = item.Checked;
                    toolStrip.Visible = check;
                    // 调整标签容器的位置
                    if(check) { 
                        tabParent.Height = tabParent.Height - toolStrip.Height;
                    } else {
                        tabParent.Height = tabParent.Height + toolStrip.Height;
                    }
                }
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
    }
}
