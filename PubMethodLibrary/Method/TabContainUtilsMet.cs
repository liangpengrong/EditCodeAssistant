using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace PubMethodLibrary
{
    /// <summary>
    /// 关于Tab容器操作的公有工具类
    /// </summary>
   public class TabContainUtilsMet
   {
       /// <summary>
       /// 获取指定Tab容器中的指定索引处的Page中的指定姓名的窗口
       /// </summary>
       /// <param name="tab">指定的Tab容器</param>
       /// <param name="pageIndex">指定的page索引</param>
       /// <param name="fName">指定的窗体姓名</param>
       /// <returns>获取到的窗体，如果没获取到任何窗体则返回null</returns>
       public static Form getFormByName(TabControl tab, int pageIndex, String fName) 
       {//
           Form f = null;
           try
           {
               foreach (Form con in tab.TabPages[pageIndex].Controls.OfType<Form>())
               {//循环判断给定索引的page的全部控件
                   if (con.Name.Equals(fName))
                   {//判断控件名是否为给定控件名相同名
                       f = con;//将控件赋值
                       break;
                   }
               }
           }catch(Exception e)
           {
               throw e;
           }
           return f;
       }
       /// <summary>
       /// //获取指定Tab容器中的指定姓名的Page的索引
       /// </summary>
       /// <param name="tab">指定的Tab容器</param>
       /// <param name="pName">指定的page姓名</param>
       /// <returns>返回获得的标签索引，如果没获得，则返回0</returns>
       public static int getPageIndexByName(TabControl tab, String pName)
       {
           int index = 0;
           for (int i = 0; i < tab.TabPages.Count; i++)
           {//循环判断给定索引的page的全部控件
               if (tab.TabPages[i].Name.Equals(pName))
               {//判断控件名是否为给定控件名相同名
                   index=i;//将控件赋值
               }
           }
           if (index.Equals(0)) { return 0; }
           else { return index; }
       }
       /// <summary>
       /// 获取指定Tab容器中的指定索引处的Page中的指定姓名的控件
       /// </summary>
       /// <param name="tab">指定的Tab容器</param>
       /// <param name="pageIndex">指定的page索引</param>
       /// <param name="cName">指定的控件姓名</param>
       /// <returns>获得的控件，如果没获得，则返回null</returns>
       public static Control getControlByName(TabControl tab, int pageIndex, String cName)
       {
           Control control = null;
           foreach (Control con in tab.TabPages[pageIndex].Controls)
           {//循环判断给定索引的page的全部控件
               if (con.Name.Equals(cName))
               {//判断控件名是否为给定控件名相同名
                   control = con;//将控件赋值
                   break;
               }
           }
           return control;
       }
       /// <summary>
       /// 获取指定Tab容器中的指定索引处的Page中的指定类型的获得焦点的控件
       /// </summary>
       /// <param name="tab">指定的Tab容器</param>
       /// <param name="pageIndex">指定的page索引</param>
       /// <param name="type">指定的类型</param>
       /// <returns>获得的控件，如果没获得，则返回null</returns>
       public static Control getFocueControlByType(TabControl tab, int pageIndex, Type type)
       {
           Control control = null;
           foreach (Control con in tab.TabPages[pageIndex].Controls)
           {//循环判断给定索引的page的全部控件
               if (con.GetType().Equals(type))
               {//判断控件类型是否为给定控件类型
                   if (con.Focused)
                   {
                       control = con;//将控件赋值
                       break;
                   }
               }
           }
           return control;
       }
       /// <summary>
       /// 获取指定Tab容器中的指定索引处的Page中的指定类型的所有控件
       /// </summary>
       /// <param name="tab">指定的Tab容器</param>
       /// <param name="pageIndex">指定的page索引</param>
       /// <param name="type">指定的类型</param>
       /// <returns>获得的控件列表，如果没获得，则返回空列表</returns>
       public static List<Control> getAllControlByType(TabControl tab, int pageIndex, Type type)
       {
           List<Control> controlAll = new List<Control>();
           foreach (Control con in tab.TabPages[pageIndex].Controls)
           {//循环判断给定索引的page的全部控件
               if (con.GetType().Equals(type))
               {//判断控件类型是否为给定控件类型
                   controlAll.Add(con);
               }
           }
           return controlAll;
       }


   }
}
