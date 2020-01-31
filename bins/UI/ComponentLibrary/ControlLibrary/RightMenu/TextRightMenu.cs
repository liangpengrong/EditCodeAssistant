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
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.ComponentlRedraw;
using UI.ComponentLibrary.MethodLibrary.Interface;
using System.Text.RegularExpressions;

namespace UI.ComponentLibrary.ControlLibrary.RightMenu
{
    internal partial class TextRightMenu : Component, MenuItemAopInter, IComponentInitMode<Control>
    {
        internal TextRightMenu() { 
            InitializeComponent();
            menuDefaultConfig(); //加载右键菜单默认配置
        }
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            ContextMenuStrip conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TEXT_RIGHT_MENU);
            if(con == null || !(con is ContextMenuStrip)) {
                conThis = rightMenuStrip;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.TEXT_RIGHT_MENU);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (ContextMenuStrip)con;
            }
            if(isShowTop) conThis.BringToFront();
            return conThis;
        }
        /// <summary>
        /// 打开多例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initPrototypeExample(bool isShowTop) {
            ContextMenuStrip conThis = rightMenuStrip;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.TEXT_RIGHT_MENU)+DateTime.Now.Ticks.ToString();;
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.TEXT_RIGHT_MENU, conThis);
            return conThis;
        }
        // 用来弹出右键菜单的文本框
        private TextBox menuSourceTextBox = null;
        // 定义右键菜单的执行方法的委托
        private delegate object methodDelegate(Dictionary<Type , object> data);
        // 右键菜单的背景色
        public Color allBackColor = Color.White;
        // 右键菜单项的宽
        public int menuWidth = 200;
        // 右键菜单项的高
        public int itemHeigth = 22;
       
        /// <summary>
        /// 判断文本框的右键菜单那些需要禁用那些需要启用
        /// </summary>
        private void doIsMenuEnabled() {
            TextBox t = menuSourceTextBox;
            try {
                if (t != null) {
                    bool readOnly = t.ReadOnly && t.Name.IndexOf(EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF)) >= 0;
                    全选Item.Enabled = !0.Equals(t.TextLength);
                    选中整行Item.Enabled = !0.Equals(t.TextLength);
                    智能选择Item.Enabled = !0.Equals(t.TextLength);
                    剪切Item.Enabled = !0.Equals(t.TextLength) && !readOnly;
                    复制Item.Enabled = !0.Equals(t.TextLength) && !0.Equals(t.SelectionLength);
                    粘贴Item.Enabled = !0.Equals(Clipboard.GetText().Length) && !readOnly;
                    删除Item.Enabled = !0.Equals(t.SelectionLength) && !readOnly;
                    去除Item.Enabled = !0.Equals(t.TextLength) && !readOnly;
                    转化为Item.Enabled = !0.Equals(t.TextLength) && !readOnly;
                    清空文本框Item.Enabled = !0.Equals(t.TextLength) && !readOnly;
                } else { 
                    // 文本框NUll时禁用右键菜单
                    rightMenuStrip.Enabled = !(t == null);
                }
            } catch(Exception ie){
                MessageBox.Show(ie.ToString());
            }
        }
        /// <summary>
        /// 用于获取右键菜单事件源头
        /// （由于二级以下菜单无法获取事件源头，此处记录以供使用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightMenuStrip_Opening(object sender, CancelEventArgs e) {
            // 将右键菜单的源控件赋值给全局变量SourceControl
            Control obj = ((ContextMenuStrip)sender).SourceControl;
            if(obj is TextBox) { 
                menuSourceTextBox = (TextBox)obj;
            }
            // 判断哪些选项需要隐藏
            doIsMenuEnabled();
        }
        /// <summary>
        /// 右键菜单的默认配置
        /// </summary>
        private void menuDefaultConfig() {
            rightMenuStrip.Name = EnumUtils.GetDescription(DefaultNameEnum.TEXT_RIGHT_MENU);
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
            rightMenuStrip.BackColor = Color.White;
            // 遍历右键菜单下所有的一级ToolStripMenuItem选项
            foreach (ToolStripMenuItem tool in rightMenuStrip.Items.OfType<ToolStripMenuItem>()) {
                ToolStripUtils.doIsDownItemAop(tool, this);
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
                // setItemImage(item);
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
            // 为所有的子选项的选项绑定总的点击事件
            tool.Click += new EventHandler(this.MenuItem_Click);
        }
        /// <summary>
        /// 当右键菜单无子项时的执行方法
        /// </summary>
        public void noDownItem(ToolStripMenuItem tool)
        {   
            
        }
        /// <summary>
        /// 全部右键菜单的执行方法
        /// </summary>
        public void allItem(ToolStripMenuItem tool)
        {
            // 为所有的子选项的选项绑定总的点击事件
            tool.Click += new EventHandler(this.MenuItem_Click);
            // 设置背景色
            if (allBackColor == null){
                tool.BackColor = Color.White;//设置所有的选项背景色为白色
            } else {
                tool.BackColor = allBackColor;//设置所有的选项背景色为指定颜色
            }
            // 设置每项的Name
            tool.Name = getItemNameDic(tool);
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
                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), menuSourceTextBox);
                foreach (KeyValuePair<string, Delegate> kvp in eventBinding())
                {//遍历对应关系字典
                    if(kvp.Key.Equals(tool.Name))
                    {//判断当前点击的选项名是否与关系字典中的选项名对应，对应则执行关系字典中的对应方法
                        if (rightMenuStrip.IsHandleCreated)
                        {//判断当前控件是否有与其关联的句柄
                            rightMenuStrip.Invoke(kvp.Value, new object[] {data});
                        }
                    }
                }
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
            /**===============委托区域============*/
            methodDelegate allTextToLower = new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED, true);
                TextBoxUtils.TextBoxToLower(t, 0);
                return null;
            });
            methodDelegate allTextToUpper = new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED, true);
                TextBoxUtils.TextBoxToUpper(t, 0);
                return null;
            });
            methodDelegate clearAllSpaces = new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxDeleteSpace(t);
                return null;
            });
            /**===============绑定区域============*/
            toolBindingDic.Add(this.全选Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxAllSelect(t);
                return null;
            }));
            toolBindingDic.Add(this.选中整行Item.Name, new methodDelegate((Dictionary<Type, object> data) => {
                TextBox t = (TextBox)data[typeof(TextBox)];
                if (t is RedrawTextBox) {
                    Point mousrP = ((RedrawTextBox)t).MouseMoveLocation;
                    t.SelectionStart = t.GetCharIndexFromPosition(new Point(mousrP.X, mousrP.Y-6));
                }
                int i = t.GetFirstCharIndexOfCurrentLine();
                int len = t.Lines[t.GetLineFromCharIndex(i)].Length;
                t.Select(i, len);
                return null;
            }));
            toolBindingDic.Add(this.智能选择Item.Name, new methodDelegate((Dictionary<Type, object> data) => {
                TextBox t = (TextBox)data[typeof(TextBox)];
                if (t is RedrawTextBox) {
                    Point mousrP = ((RedrawTextBox)t).MouseMoveLocation;
                    t.SelectionStart = t.GetCharIndexFromPosition(new Point(mousrP.X, mousrP.Y-6));
                }
                intelligentSelectText(t);
                return null;
            }));
            toolBindingDic.Add(this.剪切Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxCuttingText(t);
                return null;
            }));
            toolBindingDic.Add(this.复制Item.Name,new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxCopyText(t);
                return null;
            }));
            toolBindingDic.Add(this.粘贴Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED, true);
                TextBoxUtils.TextBoxPasteText(t);
                return null;
            }));
            toolBindingDic.Add(this.删除Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxDeleteText(t);
                return null;
            }));
            toolBindingDic.Add(this.去除_空格_Item.Name, new methodDelegate((Dictionary<Type, object> data) => {
                clearAllSpaces.Invoke(data);
                rightMenuStrip.Close();
                return null;
            }));
            toolBindingDic.Add(this.去除_空格_全部空格_Item.Name, clearAllSpaces);
            toolBindingDic.Add(this.去除_空格_行首空格_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxDelRowFirstSpace(t);
                return null;
            }));
            toolBindingDic.Add(this.去除_空格_行尾空格_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxDelRowTailSpace(t);
                return null;
            }));
            toolBindingDic.Add(this.去除_空行_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxDelBlankLine(t);
                return null;
            }));
            toolBindingDic.Add(this.去除_换行符_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxPlaceNewline(t);
                return null;
            }));
            toolBindingDic.Add(this.去除_制表符_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxPlaceTabs(t);
                return null;
            }));
            toolBindingDic.Add(this.清空文本框Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxClearText(t);
                return null;
            }));
            toolBindingDic.Add(this.转化为_大写形式_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                allTextToUpper.Invoke(data);
                rightMenuStrip.Close();
                return null;
            }));
            toolBindingDic.Add(this.转化为_大写形式_全部_Item.Name, allTextToUpper);
            toolBindingDic.Add(this.转化为_大写形式_行首_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxToUpper(t, 1);
                return null;
            }));
            toolBindingDic.Add(this.转化为_大写形式_行尾_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxToUpper(t, 2);
                return null;
            }));
            toolBindingDic.Add(this.转化为_大写形式_自定义_Item.Name, new methodDelegate((Dictionary<Type, object> data) => {

                return null;
            }));

            toolBindingDic.Add(this.转化为小写形式_Item.Name, new methodDelegate((Dictionary<Type, object> data) => {
                allTextToLower.Invoke(data);
                rightMenuStrip.Close();
                return null;
            }));
            toolBindingDic.Add(this.转化为_小写形式_全部_Item.Name, allTextToLower);
            toolBindingDic.Add(this.转化为_小写形式_行首_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxToLower(t, 1);
                return null;
            }));
            toolBindingDic.Add(this.转化为_小写形式_行尾_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxToLower(t, 2);
                return null;
            }));
            toolBindingDic.Add(this.转化为_小写形式_自定义_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                
                return null;
            }));

            toolBindingDic.Add(this.转化为_驼峰形式_大驼峰_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED, true);
                TextBoxUtils.TextBoxToHump(t, 0);
                return null;
            }));
            toolBindingDic.Add(this.转化为_驼峰形式_小驼峰_Item.Name, new methodDelegate((Dictionary<Type , object> data)=>{ 
                TextBox t = (TextBox)data[typeof(TextBox)];
                TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_EMPTY_NOT_CACHED, true);
                TextBoxUtils.TextBoxToHump(t, 1);
                return null;
            }));
            return toolBindingDic;


        }
        /// <summary>
        /// 右键菜单项对应Image的字典
        /// </summary>
        /// <returns></returns>
        private Image getItemImageDic(string name) { 
            
            Dictionary<string, Image> toolImageDic = new Dictionary<string, System.Drawing.Image>();
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
        /// 右键菜单项对应Name的字典
        /// </summary>
        /// <returns></returns>
        private string getItemNameDic(ToolStripMenuItem item) { 
            Dictionary<ToolStripMenuItem, string> toolImageDic = new Dictionary<ToolStripMenuItem, string>();
            toolImageDic.Add(this.全选Item, MainTextBRightMenuDataLib.ItemDataLib.全选_ITEM_NAME);
            toolImageDic.Add(this.选中整行Item, MainTextBRightMenuDataLib.ItemDataLib.选中整行_ITEM_NAME);
            toolImageDic.Add(this.智能选择Item, MainTextBRightMenuDataLib.ItemDataLib.智能选择_ITEM_NAME);
            toolImageDic.Add(this.剪切Item, MainTextBRightMenuDataLib.ItemDataLib.剪切_ITEM_NAME);
            toolImageDic.Add(this.复制Item, MainTextBRightMenuDataLib.ItemDataLib.复制_ITEM_NAME);
            toolImageDic.Add(this.粘贴Item, MainTextBRightMenuDataLib.ItemDataLib.粘贴_ITEM_NAME);
            toolImageDic.Add(this.删除Item, MainTextBRightMenuDataLib.ItemDataLib.删除_ITEM_NAME);
            toolImageDic.Add(this.去除Item, MainTextBRightMenuDataLib.ItemDataLib.去除_ITEM_NAME);
            toolImageDic.Add(this.转化为Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_ITEM_NAME);
            toolImageDic.Add(this.清空文本框Item, MainTextBRightMenuDataLib.ItemDataLib.清空文本框_ITEM_NAME);

            toolImageDic.Add(this.去除_空格_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_空格_ITEM_NAME);
            toolImageDic.Add(this.去除_空格_全部空格_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_空格_全部空格_ITEM_NAME);
            toolImageDic.Add(this.去除_空格_行首空格_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_空格_行首空格_ITEM_NAME);
            toolImageDic.Add(this.去除_空格_行尾空格_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_空格_行尾空格_ITEM_NAME);
            toolImageDic.Add(this.去除_空行_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_空行_ITEM_NAME);
            toolImageDic.Add(this.去除_换行符_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_换行符_ITEM_NAME);
            toolImageDic.Add(this.去除_制表符_Item, MainTextBRightMenuDataLib.ItemDataLib.去除_制表符_ITEM_NAME);

            toolImageDic.Add(this.转化为_大写形式_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_大写形式_ITEM_NAME);
            toolImageDic.Add(this.转化为_大写形式_全部_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_大写形式_全部_ITEM_NAME);
            toolImageDic.Add(this.转化为_大写形式_行首_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_大写形式_行首_ITEM_NAME);
            toolImageDic.Add(this.转化为_大写形式_行尾_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_大写形式_行尾_ITEM_NAME);
            toolImageDic.Add(this.转化为_大写形式_自定义_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_大写形式_自定义_ITEM_NAME);

            toolImageDic.Add(this.转化为小写形式_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_小写形式_ITEM_NAME);
            toolImageDic.Add(this.转化为_小写形式_全部_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_小写形式_全部_ITEM_NAME);
            toolImageDic.Add(this.转化为_小写形式_行首_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_小写形式_行首_ITEM_NAME);
            toolImageDic.Add(this.转化为_小写形式_行尾_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_小写形式_行尾_ITEM_NAME);
            toolImageDic.Add(this.转化为_小写形式_自定义_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_小写形式_自定义_ITEM_NAME);

            toolImageDic.Add(this.转化为_驼峰形式_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_驼峰形式_ITEM_NAME);
            toolImageDic.Add(this.转化为_驼峰形式_大驼峰_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_驼峰形式_大驼峰_ITEM_NAME);
            toolImageDic.Add(this.转化为_驼峰形式_小驼峰_Item, MainTextBRightMenuDataLib.ItemDataLib.转化为_驼峰形式_小驼峰_ITEM_NAME);

            if (toolImageDic.ContainsKey(item)) { 
                return toolImageDic[item];
            } else { 
                return "";    
            }
        }
        // 智能选择
        private void intelligentSelectText(TextBox t) { 
            int mouseIndex = t.SelectionStart;
            // 是否处于行的末尾
            bool isMouseLineEnd = TextBoxUtils.IsPointLineEnd(t, mouseIndex);
            if(isMouseLineEnd) { 
                // 选中整行
                int i = t.GetFirstCharIndexOfCurrentLine();
                t.Select(i, mouseIndex-i);
            } else { 
                int headMatch = 0;
                int tailMatch = 0;
                for (int i = 1; i <= t.TextLength; i++) {
                    if (headMatch.Equals(0)) {
                        if (t.SelectionStart - i >= 0) {
                            if(Regex.IsMatch(t.Text[t.SelectionStart - i].ToString(), @"\W")) {
                                headMatch = (t.SelectionStart - i)+1;
                            }
                        } else {
                            headMatch = 0;
                        }
                    }
                    if (tailMatch.Equals(0)) {
                        if (t.SelectionStart + i < t.TextLength) {
                            if (Regex.IsMatch(t.Text[t.SelectionStart + i].ToString(), @"\W")) {
                                tailMatch = t.SelectionStart + i;
                            }
                        } else {
                            tailMatch = (t.SelectionStart +i);
                        }
                    }
                    if (!headMatch.Equals(0) && !tailMatch.Equals(0)) { break; }
                }
                t.SelectionStart = headMatch;
                t.SelectionLength = tailMatch - t.SelectionStart;
            }
        }
    }
}
