using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AllDllLoad;
using PubMethodLibrary;
using StaticDataLibrary;
using PubStaticUtils;
using PubControlLibrary;
using PubCacheArea;

namespace CharsToolset
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
            ToolStrip toolStrip = (ToolStrip)data[typeof(ToolStrip)];

            IEnumerable<ToolStripLabel> ieToolLabel = toolStrip.Items.OfType<ToolStripLabel>();
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                ToolStripLabel lable = null;
                foreach (ToolStripLabel labelItem in ieToolLabel){
                    if (StrutsStripDateLib.ItemName.大小写状态.Equals(labelItem.Name)){
                        lable = labelItem;
                    }
                }
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
            ToolStrip toolStrip = (ToolStrip)data[typeof(ToolStrip)];

            IEnumerable<ToolStripLabel> ieToolLabel = toolStrip.Items.OfType<ToolStripLabel>();
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                ToolStripLabel lable = null;
                foreach (ToolStripLabel labelItem in ieToolLabel){
                    if (StrutsStripDateLib.ItemName.大小写状态.Equals(labelItem.Name)){
                        lable = labelItem;
                    }
                }
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
            ToolStrip toolStrip = (ToolStrip)data[typeof(ToolStrip)];

            // 改变状态栏的总行与总字符数
            Dictionary<string, string> retDiv = new Dictionary<string, string>();
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                retDiv.Add(StrutsStripDateLib.ItemName.总行数, TextBoxUtilsMet.getTextBoxTotalRow(t).ToString());
                retDiv.Add(StrutsStripDateLib.ItemName.总字符数, TextBoxUtilsMet.getTextBoxChars(t, false).ToString());
                // 给状态栏赋值
                setBarLable(retDiv, toolStrip);
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
            ToolStrip toolStrip = (ToolStrip)data[typeof(ToolStrip)];

            // 改变状态栏的总行与总字符数
            Dictionary<string, string> retDiv = new Dictionary<string, string>();
            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(t,1, delegate{ 
                retDiv.Add(StrutsStripDateLib.ItemName.选中字符数, TextBoxUtilsMet.getTextBoxSelChars(t).ToString());
                // 给状态栏赋值
                setBarLable(retDiv, toolStrip);
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
                ToolStrip toolStrip = ControlCache.getSingletonFactory().ContainsKey(DefaultNameCof.toolStart) ?
                    (ToolStrip)ControlCache.getSingletonFactory()[DefaultNameCof.toolStart]:null;
                // 改变状态栏的总行与总字符数
                Dictionary<string, int[]> retDiv = new Dictionary<string, int[]>();
                retDiv.Add(StrutsStripDateLib.ItemName.行列数, TextBoxUtilsMet.getTextBoxRowColumn(t));
                //获得状态栏中全部的label
                IEnumerable<ToolStripLabel> ieToolLabel = toolStrip.Items.OfType<ToolStripLabel>();
                foreach (ToolStripLabel label in ieToolLabel)
                {
                    int[] val = null;
                    //获取传入的label名与label值的字典中的Key为该label名的value
                    retDiv.TryGetValue(label.Name, out val);
                    if (val != null)
                    {
                        //将该value赋值给label
                        label.Text = val[0]+":"+val[1];
                    }
                }
            });
        }

        /// <summary>
        /// 根据传入的Map各状态栏中的文本赋值
        /// </summary>
        /// <param name="retDiv"></param>
        private static void setBarLable(Dictionary<string, string> retDiv, ToolStrip toolStrip) {

            //获得状态栏中全部的label
            IEnumerable<ToolStripLabel> ieToolLabel = toolStrip.Items.OfType<ToolStripLabel>();
            foreach (ToolStripLabel label in ieToolLabel)
            {
                String val = null;
                //获取传入的label名与label值的字典中的Key为该label名的value
                retDiv.TryGetValue(label.Name, out val);
                if (val != null)
                {
                    //将该value赋值给label
                    label.Text = val;
                }
            }
        }
        /// <summary>
        /// 打开统计字符窗口
        /// </summary>
        /// <returns></returns>
        public static CharsStatistics openCharsStatistics(Dictionary<Type , object> data)
        {
            // 获取控件
            TextBox t = (TextBox)data[typeof(TextBox)];

            CharsStatistics rowGoToForm = PublicEventMet.openCharsStatistics(t, false);
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

            RowGoToForm rowGoToForm = PublicEventMet.openRowGoToForm(t, false);
            rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
    }
}
