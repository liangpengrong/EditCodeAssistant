using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using Ui.ControlEventLibrary;
using UI.ComponentLibrary.FormLibrary;

namespace UI.ControlEventLibrary.StatusBarEvent.TextStatusBarEvent
{
    public class TextStatusBarEventMet
    {
        private TextStatusBarEventMet() { }
        /// <summary>
        /// 将大小写状态赋值给状态栏
        /// </summary>
        public static object setCaseMouse(Dictionary<Type , object> data) {
            // 获取控件
            if (data.ContainsKey(typeof(StatusStrip)) && data[typeof(StatusStrip)] is StatusStrip) { 
                StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(toolStrip, 1, delegate{ 
                    ToolStripLabel lable = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.大小写状态];
                    byte[] bs = new byte[256];
                    //判断当前大小写1为大写
                    WinApiUtilsMet.GetKeyboardState(bs);
                    if (bs[0x14].Equals(1)) 
                    {//判断当前为大写
                        //设置为小写
                        WinApiUtilsMet.SetCapitalState(false);
                        lable.Text = "小写";
                    } else {
                        WinApiUtilsMet.SetCapitalState(true);
                        lable.Text = "大写";
                    }
                
                });
            } else { 
                MessageBox.Show("无法获取状态栏");    
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
        /// <summary>
        /// 打开统计字符窗体
        /// </summary>
        /// <returns></returns>
        public static object openCharsStatistics(Dictionary<Type , object> data) {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                CharsStatistics.openCharsStatistics(t);
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            
            return null;
        }
        /// <summary>
        /// 打开转到行窗体
        /// </summary>
        /// <returns></returns>
        public static object openRowGoToForm(Dictionary<Type , object> data) {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                RowGoToForm.openRowGoToForm(t);
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 打开设置编码窗体
        /// </summary>
        /// <returns></returns>
        public static object openSetCodingForm(Dictionary<Type , object> data) {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                SetCodingForm.openSetCodingForm(t);
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 将大小写状态赋值给状态栏
        /// </summary>
        public static void setCaseKey(Dictionary<Type , object> data)
        {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(t,1, delegate{ 
                    ToolStripLabel lable = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.大小写状态];
                    // 判断当前为大写
                    if (WinApiUtilsMet.GetCapitalState()){
                        lable.Text = "大写";
                    } else {
                        lable.Text = "小写";
                    }
                });
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            
        }

        /// <summary>
        /// 将总行数与总字符数赋值给状态栏
        /// </summary>
        public static void setRowChars(Dictionary<Type , object> data) {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(t,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.总行数];
                    ToolStripLabel lable2 = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.总字符数];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    string tag2 = lable2.Tag != null?lable2.Tag.ToString()+"：":"";
                    lable1.Text = tag1+TextBoxUtilsMet.getTextBoxTotalRow(t).ToString();
                    lable2.Text = tag2+TextBoxUtilsMet.getTextBoxChars(t, false).ToString();
                });
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            
        }
        /// <summary>
        /// 将选中字符数赋值给状态栏
        /// </summary>
        public static void setSelectChars(Dictionary<Type , object> data)
        {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(t,1, delegate{ 
                    ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.选中字符数];
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1+t.SelectionLength.ToString();
                });
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            
        }
        /// <summary>
        /// 设置文本框只读
        /// </summary>
        public static object setTextReadOnly(Dictionary<Type , object> data) { 
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(t,1, delegate{ 
                    // 获取状态栏
                    StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                    // 获取只读lable
                    ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.只读状态];
                    t.ReadOnly = !t.ReadOnly;
                    bool only = t.ReadOnly;
                    int[] val = TextBoxUtilsMet.getTextBoxRowColumn(t);
                    string tag1 = lable1.Tag != null? lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1 + (only?"是" : "否");
                });
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 刷新文本框只读状态
        /// </summary>
        public static object refreshTextReadOnly(Dictionary<Type , object> data) { 
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(t,1, delegate{ 
                    // 获取状态栏
                    StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                    // 获取只读lable
                    ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.只读状态];
                    bool only = t.ReadOnly;
                    int[] val = TextBoxUtilsMet.getTextBoxRowColumn(t);
                    string tag1 = lable1.Tag != null? lable1.Tag.ToString()+"：":"";
                    // 给状态栏赋值
                    lable1.Text = tag1 + (only?"是" : "否");
                });
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
        /// <summary>
        /// 将当前的行列数赋值给状态栏
        /// </summary>
        public static object setRowColumn(Dictionary<Type , object> data) {
            // 获取控件
            if (data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] is TextBox) { 
                TextBox t = (TextBox)data[typeof(TextBox)];
                // 开辟新线程执行方法
                ControlsUtilsMet.asynchronousMet(t,1, delegate{ 
                    StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                    ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDataLib.ItemName.行列数];
                        int[] val = TextBoxUtilsMet.getTextBoxRowColumn(t);
                        string tag1 = lable1.Tag != null?lable1.Tag.ToString():"";
                        if (val != null) {
                            //将行与列赋值给label
                            lable1.Text = tag1.Replace("{1}",val[0].ToString())
                                    .Replace("{2}", val[1].ToString());
                        }
                });
            } else { 
                MessageBox.Show("无法获取文本框");    
            }
            return null;
        }
    }
}
