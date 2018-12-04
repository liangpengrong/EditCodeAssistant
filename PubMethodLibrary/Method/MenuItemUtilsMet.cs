using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
namespace PubMethodLibrary
{
    /// <summary>
    /// 关于菜单控件的工具类
    /// </summary>
   public class MenuItemUtilsMet
   {
       /// <summary>
       /// 递归遍历右键菜单选项判断其有无子项,并且实现在有子菜单项，无子菜单，全部菜单下的方法执行
       /// </summary>
       /// <param name="menuItem">只是递归需要实际为null就行了</param>
       /// <param name="aopInter">实现了MenuItemAopInter接口的实现类</param>
       public void isDownItemAop(ToolStripMenuItem menuItem,MenuItemAopInter aopInter)
       {
           aopInter.allItem(menuItem);//执行全部右键菜单的执行方法
           if (menuItem.HasDropDownItems)//判断有无子项
           {//判断该ToolStripMenuItem是否还包含ToolStripMenuItem
               aopInter.haveDownItem(menuItem);//执行当右键菜单有子项时的执行方法
               foreach (ToolStripMenuItem mi in menuItem.DropDownItems.OfType<ToolStripMenuItem>())
               {//递归调用自身将有有子项的子项循环执行判断
                   this.isDownItemAop(mi, aopInter);
               }
           }
           else
           {//无子项
               aopInter.noDownItem(menuItem);//执行当右键菜单无子项时的执行方法
           }
       }
       /// <summary>
       /// 居中显示右键菜单文字
       /// </summary>
       /// <param name="b"></param>
       public static void fontCentered(ToolStripItemCollection tools)
       {//实现MenuStrip中的文本居中
           foreach (ToolStripMenuItem tool in tools.OfType<ToolStripMenuItem>())
           {
               if (tool.GetType().Equals(new ToolStripMenuItem().GetType()))
               {

                   tool.Text = MessyUtilsMet.centerCharacter(
                   tool.Text
                   , " "
                   , 220
                   , (int)new ContextMenuStrip().CreateGraphics().MeasureString(tool.Text, tool.Font).Width
                   , (int)new ContextMenuStrip().CreateGraphics().MeasureString(" ", tool.Font).Width);
               }
               
           }

       }
       /// <summary>
       /// 右键菜单的关闭绑定事件，实现鼠标不在其范围内关闭，在其范围内不关闭
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       public static void moveOutClosing(object sender, ToolStripDropDownClosingEventArgs e)
       {
           ContextMenuStrip menu = (ContextMenuStrip)sender;
           Point mousePoint;//初始化一个接受鼠标位置的Point
           WinApiUtilsMet.GetCursorPos(out mousePoint);//赋值
           if (menu.ClientRectangle.Contains(mousePoint.X - menu.Left, mousePoint.Y - menu.Top))
           {
               e.Cancel = true;//阻止关闭
           }
           else 
           {

               e.Cancel = false;//关闭
           }
       }
       
   }
}
