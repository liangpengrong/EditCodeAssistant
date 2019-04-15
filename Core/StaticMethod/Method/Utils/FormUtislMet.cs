using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 关于窗体的工具类
    /// </summary>
    public class FormUtislMet
    {
        /// <summary>
        /// 退出主程序，结束进程
        /// </summary>
        public static void dropOut() { 
            Environment.Exit(0);    
        }
        /// <summary>
        /// 将传入的窗体生成相对与屏幕中间的位置
        /// </summary>
        /// <param name="f1"></param>
        /// <returns></returns>
        public static Point middleForm(Form f1) {
            f1.StartPosition = FormStartPosition.Manual;
            Point point;
            int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
            int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;
            point = new Point((iActulaWidth - f1.Width) / 2, (iActulaHeight - f1.Height) / 2);
            return point;
        }
        /// <summary>
        /// 将传入的第一个窗体生成相对与第二个窗体中间的位置
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static Point middleForm(Form f1, Form f2) {
            Point point;
            if(f2.Width >= f1.Width && f2.Height > f1.Height){
                point = new Point((f2.Width - f1.Width) / 2 + f2.Location.X, (f2.Height - f1.Height) / 2 + f2.Location.Y);
            } else{
                point = new Point(f2.Width+f2.Location.X,f2.Location.Y);
            }
            return point;
        }
        /// <summary>
        /// 将窗体置于最顶层而不获取焦点
        /// </summary>
        /// <param name="b">true最顶层，false非顶层</param>
        /// <param name="f">要操作的窗口</param>
        public static void topFormNoFocus(Boolean isTop, Form form)
        {
            if(form == null || form.IsDisposed) return;
            if (form != null) {
                if (isTop) {
                    WinApiUtilsMet.setFormTopNoFocus(true, form);
                } else {
                    if (!WinApiUtilsMet.GetForegroundWindow().Equals(form.Handle)) {
                        WinApiUtilsMet.setFormTopNoFocus(false, form);
                    }
                }
            }
        }
        /// <summary>
        /// 将窗体置于最顶层而不获取焦点
        /// </summary>
        /// <param name="b">true最顶层，false非顶层</param>
        /// <param name="fAll">要操作的窗口的集合</param>
        public static void topFormNoFocus(Boolean isTop, Form[] formArr){
            // 判断传入的窗体集合为null或大小为0
            if(formArr == null || 0.Equals(formArr.Length)) return;
            // 遍历
            foreach(Form form in formArr) {
                // 判断窗体不为null或窗体没有被释放
                if (form != null && !form.IsDisposed){
                    // 判断是否要设置为顶层
                    if(isTop) {
                        // 设为顶层
                        WinApiUtilsMet.setFormTopNoFocus(true, form);
                    } else {
                        // 判断当前前台窗口是否为要设置的窗口
                        if(form.TopLevel && !WinApiUtilsMet.GetForegroundWindow().Equals(form.Handle)) {
                            // 将窗口设置为非顶层
                            WinApiUtilsMet.setFormTopNoFocus(false, form);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 通过窗体标题名获取进程中的窗体句柄
        /// </summary>
        /// <param name="name">窗体标题名</param>
        /// <returns></returns>
        public static IntPtr getProcessFormByName(string headName) {
            IntPtr intPtr = WinApiUtilsMet.FindWindow(null, headName);
            return intPtr;
        }

    }
}
