using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Core.StaticMethod.Inter;
using Core.StaticMethod.Method.Redraw;
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;

namespace UI.ComponentLibrary.ControlLibrary.RightMenu
{
    public partial class TextRightMenu : Component, MenuItemAopInter
    {
        private TextRightMenu() { 
            InitializeComponent();
            //MenuItemUtilsMet.fontCentered(this.rightMenuStrip.Items);//使右键菜单文本居中显示
            menuDefaultConfig(); //加载右键菜单默认配置
        }
        // 用来弹出右键菜单的文本框
        private TextBox menuSourceTextBox = null;
        // 定义右键菜单的执行方法的委托
        private delegate object methodDelegate(TextBox t);
        // 右键菜单的背景色
        public Color allBackColor = Color.White;
        // 右键菜单项的宽
        public int menuWidth = 200;
        // 右键菜单项的高
        public int itemHeigth = 22;
       
        /// <summary>
        /// 判断文本框的右键菜单那些需要禁用那些需要启用
        /// </summary>
        private void isMenuEnabled()
        {
            try
            {
                TextBox t = menuSourceTextBox;
                this.isMenuTextNull(t);
                // 判断粘贴板内容是否为空
                this.粘贴Item.Enabled = !0.Equals(Clipboard.GetText().Length);
                this.删除Item.Enabled = !t.SelectionLength.Equals(0);
            }catch(InvalidCastException ie){//强制类型转化异常
                MessageBox.Show(ie.ToString());
            }
        }
        /// <summary>
        /// 判断文本框内容为空时需要隐藏的右键菜单选项
        /// </summary>
        /// <param name="t">判断的文本框</param>
        /// <param name="l">需要隐藏的右键菜单选项</param>
        /// <returns>方法执行完毕返回的信息</returns>
        private string isMenuTextNull(TextBox t)
        {
            // 在文本框为空时需要停用的Item
            List<ToolStripMenuItem> listNull = new List<ToolStripMenuItem>();
            listNull.Add(this.复制Item);
            listNull.Add(this.全选Item);
            listNull.Add(this.剪切Item);
            listNull.Add(this.删除Item);
            listNull.Add(this.去除Item);
            listNull.Add(this.清空文本框Item);
            listNull.Add(this.转化为Item);
            if (t != null) {
                foreach (ToolStripMenuItem tool in listNull)
                {
                    tool.Enabled = !t.TextLength.Equals(0);
                }
                return null;
            } else {
                return "文本框为null";
            }
        }
        /// <summary>
        /// 用于获取右键菜单事件源头
        /// （由于二级以下菜单无法获取事件源头，此处记录以供使用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightMenuStrip_Opening(object sender, CancelEventArgs e)
        {//右键菜单的弹出事件
            //将右键菜单的源控件赋值给全局变量SourceControl
            menuSourceTextBox = (TextBox)((ContextMenuStrip)sender).SourceControl;
            this.isMenuEnabled();//判断哪些选项需要隐藏
        }
        /// <summary>
        /// 右键菜单的默认配置
        /// </summary>
        private void menuDefaultConfig()
        {
            rightMenuStrip.Name = DefaultNameCof.TEXT_RIGHT_MENU;
            // 使用自定义的样式
            rightMenuStrip.Renderer = new RightStripRenderer();
            // 设置不具有Tab焦点
            rightMenuStrip.TabStop = false;
            // 字体
            rightMenuStrip.Font = new Font("微软雅黑",9,FontStyle.Regular);
            //不显示图像边距
            rightMenuStrip.ShowImageMargin = true;
            //不显示选中边距
            rightMenuStrip.ShowCheckMargin = false;
            //显示信息提示
            rightMenuStrip.ShowItemToolTips = true;
            foreach (ToolStripMenuItem tool in this.rightMenuStrip.Items.OfType<ToolStripMenuItem>())
            {//遍历右键菜单下所有的一级ToolStripMenuItem选项
                new MenuItemUtilsMet().isDownItemAop(tool, this);
            }
            // 调整右键菜单配置
            menuConfig();
        }
        /// <summary>
        /// 调整右键菜单配置
        /// </summary>
        private void menuConfig() { 
            rightMenuStrip.AutoSize = false;
            // 设置右键菜单的宽
            rightMenuStrip.Width = menuWidth;
            // 获取右键菜单中的所有ToolStripMenuItem
            ToolStripMenuItem[] itemAll = rightMenuStrip.Items.OfType<ToolStripMenuItem>().ToArray();
            // 获取右键菜单中的所有分割
            ToolStripSeparator[] sepAll = rightMenuStrip.Items.OfType<ToolStripSeparator>().ToArray();
            for(int i = 0; i < itemAll.Length;i++) { 
                ToolStripMenuItem item = itemAll[i];
                item.AutoSize = false;
                // 设置为右键菜单项的宽
                item.Width = menuWidth;
                // 设置为右键菜单项的高
                item.Height = itemHeigth;
                // 设置为右键菜单项的字体
                item.Font = rightMenuStrip.Font;

                // 设置图像
                setItemImage(item);
            }
            rightMenuStrip.Height = (itemHeigth + 2) * itemAll.Length + sepAll.Length * 2;
        }
        /// <summary>
        /// 设置菜单项的Image
        /// </summary>
        /// <param name="item"></param>
        private void setItemImage(ToolStripMenuItem item) {
            System.Drawing.Image image = getItemImageDic(item.Name);
            if(image != null) { 
                item.Image = image;
                item.ImageScaling = ToolStripItemImageScaling.None;
                item.ImageAlign = ContentAlignment.MiddleRight;
            }
        }
        /// <summary>
        /// 当右键菜单有子项时的执行方法
        /// </summary>
        public void haveDownItem(ToolStripMenuItem tool)
        {
        
        }
        /// <summary>
        /// 当右键菜单无子项时的执行方法
        /// </summary>
        public void noDownItem(ToolStripMenuItem tool)
        {
            tool.Click += new EventHandler(this.MenuItem_Click);//为所有的子选项的选项绑定总的点击事件
        }
        /// <summary>
        /// 全部右键菜单的执行方法
        /// </summary>
        public void allItem(ToolStripMenuItem tool)
        {
            if (this.allBackColor == null)
            {
                tool.BackColor = Color.White;//设置所有的选项背景色为白色
            }
            else 
            {
                tool.BackColor = allBackColor;//设置所有的选项背景色为指定颜色
            }
        }
        /// <summary>
        /// 右键菜单选项的总绑定类，执行选项name对应的绑定类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // 将object强制转化为右键菜单选项
                ToolStripMenuItem tool = (ToolStripMenuItem)sender;
                // 获取弹出右键菜单的控件并强制转化为文本框
                TextBox textB = menuSourceTextBox;
                foreach (KeyValuePair<string, Delegate> kvp in this.eventBinding())
                {//遍历对应关系字典
                    if(kvp.Key.Equals(tool.Name))
                    {//判断当前点击的选项名是否与关系字典中的选项名对应，对应则执行关系字典中的对应方法
                        if (rightMenuStrip.IsHandleCreated)
                        {//判断当前控件是否有与其关联的句柄
                            rightMenuStrip.Invoke(kvp.Value, new object[] {textB});
                        }
                    }
                }
                //dLLLoad.TextBoxUtilsMet.textFixStartIndex(t, TextBoxDataLibcs.ExpTextLength, TextBoxDataLibcs.ExpStartIndex);//确定文本框光标位置
            }
            catch (InvalidCastException ie)
            {//强制类型转化异常
                MessageBox.Show(ie.ToString());
            }
        }

        /// <summary>
        /// 右键菜单选项对应的执行类
        /// </summary>
        /// <param name="t">需要操作的文本框</param>
        /// <returns>Dictionary的对应关系，key为右键菜单选项的name,value为委托类</returns>
        private Dictionary<string, Delegate> eventBinding()
        {
            Dictionary<string, Delegate> toolBindingDic = new Dictionary<string, Delegate>();
            toolBindingDic.Add(this.全选Item.Name, new methodDelegate(TextBoxUtilsMet.textAllSelect));
            toolBindingDic.Add(this.剪切Item.Name, new methodDelegate(TextBoxUtilsMet.textSelectCut));
            toolBindingDic.Add(this.复制Item.Name,new methodDelegate(TextBoxUtilsMet.textCopy));
            toolBindingDic.Add(this.粘贴Item.Name, new methodDelegate(TextBoxUtilsMet.textPaste));
            toolBindingDic.Add(this.删除Item.Name, new methodDelegate(TextBoxUtilsMet.textSelectDelect));
            toolBindingDic.Add(this.全部空格Item.Name, new methodDelegate(TextBoxUtilsMet.textDelSpace));
            toolBindingDic.Add(this.行首空格Item.Name, new methodDelegate(TextBoxUtilsMet.textDelRowFirstSpace));
            toolBindingDic.Add(this.行尾空格Item.Name, new methodDelegate(TextBoxUtilsMet.textDelRowTailSpace));
            toolBindingDic.Add(this.空行Item.Name, new methodDelegate(TextBoxUtilsMet.textDelBlankLine));
            toolBindingDic.Add(this.换行符Item.Name, new methodDelegate(TextBoxUtilsMet.textPlaceNewline));
            toolBindingDic.Add(this.制表符Item.Name, new methodDelegate(TextBoxUtilsMet.textPlaceTabs));
            toolBindingDic.Add(this.清空文本框Item.Name, new methodDelegate(TextBoxUtilsMet.textClear));
            toolBindingDic.Add(this.大写形式Item.Name, new methodDelegate(TextBoxUtilsMet.textToUpper));
            toolBindingDic.Add(this.小写形式Item.Name, new methodDelegate(TextBoxUtilsMet.textToLower));
            toolBindingDic.Add(this.驼峰形式Item.Name, new methodDelegate(TextBoxUtilsMet.textToHump));
            return toolBindingDic;
        }
        /// <summary>
        /// 右键菜单项对应Image的字典
        /// </summary>
        /// <returns></returns>
        private System.Drawing.Image getItemImageDic(string name) { 
            
            Dictionary<string, System.Drawing.Image> toolImageDic = new Dictionary<string, System.Drawing.Image>();
            toolImageDic.Add(this.全选Item.Name, Core.ImageResource.全选_反相);
            toolImageDic.Add(this.剪切Item.Name, Core.ImageResource.裁剪);
            toolImageDic.Add(this.复制Item.Name, Core.ImageResource.复制);
            toolImageDic.Add(this.粘贴Item.Name, Core.ImageResource.粘贴);
            toolImageDic.Add(this.删除Item.Name, Core.ImageResource.删除del);
            toolImageDic.Add(this.去除Item.Name, Core.ImageResource.擦除);
            toolImageDic.Add(this.转化为Item.Name, Core.ImageResource.转化);
            toolImageDic.Add(this.清空文本框Item.Name, Core.ImageResource.清空);

            if (toolImageDic.ContainsKey(name)) { 
                return toolImageDic[name];
            } else { 
                return null;    
            }
        }
        /// <summary>
        /// 获取右键菜单
        /// </summary>
        /// <returns></returns>
        public static ContextMenuStrip getTextRightMenu() { 
            Control con = ControlCache.getSingletonCache(DefaultNameCof.TEXT_RIGHT_MENU);
            if(con == null) {
                TextRightMenu textRightMenu = new TextRightMenu();
                ControlCache.addSingletonCache(textRightMenu.rightMenuStrip);
                return textRightMenu.rightMenuStrip;
            } else { 
                return (ContextMenuStrip)con;
            }
        }
        /// <summary>
        /// 为该右键菜单绑定文本框
        /// </summary>
        /// <param name="t"></param>
        public static void bindingTextBox(TextBox t) {
            if(t != null) {
                t.ContextMenuStrip = getTextRightMenu();
            }
        }
    }
}
