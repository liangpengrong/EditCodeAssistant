using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PublicMethodLibrary;
using StaticDataLibrary;
using ComponentLibrary;
using CacheFactory;
using SingleComponentFactory;

namespace SingleComponentLibrary
{
    public class TextStatusBarEventMet
    {
        private TextStatusBarEventMet() { }
        /// <summary>
        /// 将大小写状态赋值给状态栏
        /// </summary>
        public static object setCaseMouse(Dictionary<Type , object> data) {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];
            StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                ToolStripLabel lable = (ToolStripLabel)toolStrip.Items[StrutsStripDateLib.ItemName.大小写状态];
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
            
            return null;
        }
        /// <summary>
        /// 将大小写状态赋值给状态栏
        /// </summary>
        public static void setCaseKey(Dictionary<Type , object> data)
        {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];
            StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                ToolStripLabel lable = (ToolStripLabel)toolStrip.Items[StrutsStripDateLib.ItemName.大小写状态];
                // 判断当前为大写
                if (WinApiUtilsMet.GetCapitalState()){
                    lable.Text = "大写";
                } else {
                    lable.Text = "小写";
                }
            });
        }

        /// <summary>
        /// 将总行数与总字符数赋值给状态栏
        /// </summary>
        /// <param name="t"></param>
        public static void setRowChars(Dictionary<Type , object> data) {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];
            StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDateLib.ItemName.总行数];
                ToolStripLabel lable2 = (ToolStripLabel)toolStrip.Items[StrutsStripDateLib.ItemName.总字符数];
                string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                string tag2 = lable2.Tag != null?lable2.Tag.ToString()+"：":"";
                lable1.Text = tag1+TextBoxUtilsMet.getTextBoxTotalRow(t).ToString();
                lable2.Text = tag2+TextBoxUtilsMet.getTextBoxChars(t, false).ToString();
            });
        }
        /// <summary>
        /// 将选中字符数赋值给状态栏
        /// </summary>
        /// <param name="t"></param>
        public static void setSelectChars(Dictionary<Type , object> data)
        {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];
            StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDateLib.ItemName.选中字符数];
                string tag1 = lable1.Tag != null?lable1.Tag.ToString()+"：":"";
                // 给状态栏赋值
                lable1.Text = tag1+t.SelectionLength.ToString();
            });
        }

        /// <summary>
        /// 将当前的行列数赋值给状态栏
        /// </summary>
        /// <param name="t"></param>
        public static void setRowColumn(Dictionary<Type , object> data)
        {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];

            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                StatusStrip toolStrip = (StatusStrip)data[typeof(StatusStrip)];
                ToolStripLabel lable1 = (ToolStripLabel)toolStrip.Items[StrutsStripDateLib.ItemName.行列数];
                    int[] val = TextBoxUtilsMet.getTextBoxRowColumn(t);
                    string tag1 = lable1.Tag != null?lable1.Tag.ToString():"";
                    if (val != null) {
                        //将行与列赋值给label
                        lable1.Text = tag1.Replace("{1}",val[0].ToString())
                                .Replace("{2}", val[1].ToString());
                    }
            });
        }
        /// <summary>
        /// 打开统计字符窗口
        /// </summary>
        /// <returns></returns>
        public static CharsStatistics openCharsStatistics(Dictionary<Type , object> data)
        {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];

            CharsStatistics rowGoToForm = InitSingleForm.initCharsStatistics(t, false);
            rowGoToForm.ShowDialog();
            return null;
        }
        /// <summary>
        /// 打开转到行窗体
        /// </summary>
        /// <returns></returns>
        public static object openRowGoToForm(Dictionary<Type , object> data)
        {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];

            RowGoToForm rowGoToForm = InitSingleForm.initRowGoToForm(t, false);
            rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
        /// <summary>
        /// 打开设置文本框编码窗体
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object openSetCodingForm(Dictionary<Type , object> data)
        { 
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];
            SetCodingForm setCodingForm = InitSingleForm.initSetCodingForm(t, false);
            setCodingForm.ShowDialog();
            return setCodingForm;
        }
    }
}
