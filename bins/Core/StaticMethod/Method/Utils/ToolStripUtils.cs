using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using Core.StaticMethod.Inter;

namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 关于菜单控件的工具类
    /// </summary>
    public static class ToolStripUtils
    {
        /// <summary>
        /// 递归遍历右键菜单选项判断其有无子项,并且实现在有子菜单项，无子菜单，全部菜单下的方法执行
        /// </summary>
        /// <param name="menuItem">只是递归需要实际为null就行了</param>
        /// <param name="aopInter">实现了MenuItemAopInter接口的实现类</param>
        public static void doIsDownItemAop(ToolStripMenuItem menuItem, MenuItemAopInter aopInter)
        {
            aopInter.allItem(menuItem);//执行全部右键菜单的执行方法
            if (menuItem.HasDropDownItems)//判断有无子项
            {//判断该ToolStripMenuItem是否还包含ToolStripMenuItem
                aopInter.haveDownItem(menuItem);//执行当右键菜单有子项时的执行方法
                foreach (ToolStripMenuItem mi in menuItem.DropDownItems.OfType<ToolStripMenuItem>())
                {//递归调用自身将有有子项的子项循环执行判断
                    doIsDownItemAop(mi, aopInter);
                }
            }
            else
            {//无子项
                aopInter.noDownItem(menuItem);//执行当右键菜单无子项时的执行方法
            }
        }
        /// <summary>
        /// 右键菜单的关闭绑定事件，实现鼠标不在其范围内关闭，在其范围内不关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ToolStripMoveOutClosing(ToolStripDropDown tool) {
            if(tool == null) return;
            tool.Closing += new ToolStripDropDownClosingEventHandler((object sender, ToolStripDropDownClosingEventArgs e)=>{ 
                ToolStripDropDown menu = (ToolStripDropDown)sender;
                Point mousePoint;//初始化一个接受鼠标位置的Point
                WindowsApiUtils.GetCursorPos(out mousePoint);//赋值
                if (menu.ClientRectangle.Contains(mousePoint.X - menu.Left, mousePoint.Y - menu.Top)){
                    e.Cancel = true;//阻止关闭
                } else {
                    e.Cancel = false;//关闭
                }
            });
        }
        /// <summary>
        /// 获取指定工具栏集中的指定姓名的子项
        /// </summary>
        /// <param name="tab">指定的控件集</param>
        /// <param name="cName">指定的控件姓名</param>
        /// <returns>获得的控件，如果没获得，则返回null</returns>
        public static ToolStrip GetControlByName(ToolStripItemCollection tAll, string tName) {
            ToolStrip toolStrip = null;
            // 循环判断给定控件集中的全部控件
            foreach(ToolStrip tool in tAll) {
                // 判断控件名是否为给定控件名相同名
                if(toolStrip.Name.Equals(tName)) {
                    // 将控件赋值
                    toolStrip = tool;
                    break;
                }
                if(tool.Items.Count > 0){ 
                    GetControlByName(tool.Items, tName);
                }
            }
            return toolStrip;
        }
       
    }
}
