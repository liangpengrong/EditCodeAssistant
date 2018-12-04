using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PubMethodLibrary
{
   public interface MenuItemAopInter
   {
       /// <summary>
       /// 当右键菜单有子项时的执行方法
       /// </summary>
      void haveDownItem(ToolStripMenuItem tool);
       /// <summary>
       /// 当右键菜单无子项时的执行方法
       /// </summary>
      void noDownItem(ToolStripMenuItem tool);
       /// <summary>
       /// 全部右键菜单的执行方法
       /// </summary>
      void allItem(ToolStripMenuItem tool);
   }
}
