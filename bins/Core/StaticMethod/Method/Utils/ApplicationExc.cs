using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 全局异常捕获
    /// </summary>
    public class ApplicationExc
    {
        /// <summary>
        /// 全局异常处理绑定类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            foreach (KeyValuePair<Type, string> kvp in ExceptionDic())
            {
               if (e.Exception.GetType().Equals(kvp.Key))
               {
                 MessageBox.Show(kvp.Value);
               }
            }
        }
        /// <summary>
        /// 异常对应的弹窗提示
        /// </summary>
        /// <returns></returns>
       private static Dictionary<Type, string> ExceptionDic()
       {
           Dictionary<Type, string> excDic = new Dictionary<Type, string>();
           excDic.Add(new NullReferenceException().GetType(),"空指针异常");
           excDic.Add(new KeyNotFoundException().GetType(), "字典对应异常");
           return excDic;
       }
    }
}
