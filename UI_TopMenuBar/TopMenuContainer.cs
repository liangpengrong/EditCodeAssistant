using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Core.StaticMethod.Inter;
using Core.StaticMethod.Method.Redraw;
using Core.StaticMethod.Method.Utils;
using Ui.ControlEventLibrary;
using UI.ComponentLibrary.FormLibrary;
using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using UI_TopMenuBar.TopMenuEvent;
using UI_OptionForm;
using Core.DefaultData.DataLibrary;
using UI.TabContentLibrary.MainTabContent;
using Core_Config.ConfigData.ControlConfig;

namespace UI_TopMenuBar
{
    public partial class TopMenuContainer : UserControl, MenuItemAopInter
    {
        // 默认背景色
        private Color topBarBackColor = ColorTranslator.FromHtml("#fff");
        private TopMenuContainer()
        {
            // 初始化
            InitializeComponent();
            // 初始化配置
            initTopMenuConfig();
            //遍历顶部菜单的子菜单并执行对应的AOP方法
            this.traverseItems();
        }
        // 定义菜单的执行方法的委托
        private delegate object methodDelegate(Dictionary<Type , object> data);
        /// <summary>
        /// 遍历顶部菜单的子菜单并执行对应的AOP方法
        /// </summary>
        private void traverseItems()
        {
            foreach(ToolStripMenuItem tool in this.topMenuStrip.Items.OfType<ToolStripMenuItem>())
            {
                ToolStripUtilsMet.isDownItemAop(tool,this);
            }
        }
        /// <summary>
        /// 当菜单项有子项时的执行方法
        /// </summary>
        public virtual void haveDownItem(ToolStripMenuItem tool)
        {
            
        }
        /// <summary>
        /// 当菜单项无子项时的执行方法
        /// </summary>
        public virtual void noDownItem(ToolStripMenuItem tool)
        {
            tool.Click+=new EventHandler(this.MenuItem_Click);
            //MenuItemUtilsMet.fontCentered(tool);
        }
        /// <summary>
        /// 全部菜单项的执行方法
        /// </summary>
        
        public virtual void allItem(ToolStripMenuItem tool)
        {
            // 绑定Name
            tool.Name = getItemNameDic(tool);
            // 设置背景色
            tool.BackColor = Color.White;
            tool.AutoToolTip = true;
            tool.ToolTipText = tool.Text;
            // tool.Font = topMenuStrip.Font;
            // 绑定Image
            setItemImage(tool);
            // 判断是否选中
            doISItemChecked(tool);
        }
        /// <summary>
        /// 初始化顶部菜单配置
        /// </summary>
        private void initTopMenuConfig() {
            topMenuStrip.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.TOP_MENU);
            topMenuStrip.TabStop = false;
            // 设置大小
            topMenuStrip.Size = new Size(this.Size.Width,10);
            // 设置停靠到顶端
            topMenuStrip.Dock = DockStyle.Top;
            // 设置相对距离
            topMenuStrip.Location = new Point(0,0);
            // 设置背景颜色
            topMenuStrip.BackColor = topBarBackColor;
            // 字体
            topMenuStrip.Font = new Font("微软雅黑",9,FontStyle.Regular);
            // 绑定重绘函数
            topMenuStrip.Renderer =  new TopMenuRenderer();    
        }
        /// <summary>
        /// 菜单子选项的总绑定类，执行选项name对应的绑定类
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, EventArgs e) {
            try {
                ToolStripMenuItem tool = (ToolStripMenuItem)sender;
                //获取当前主Tab容器中的文本框
                TextBox t = null;
                Control control = null;
                // 获得主tab容器
                Control tabCon = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TAB_CONTENT);
                if(tabCon != null && tabCon is TabControl) { 
                    TabControl tab = (TabControl)tabCon;
                    if (t == null) { 
                        control = ControlsUtilsMet.getControlByName(tab.SelectedTab.Controls, EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF), true);
                        if (control != null && control is TextBox) { 
                            t = (TextBox)control;
                        } 
                    } 
                    if(t == null) { 
                        control = ControlsUtilsMet.getFocueControlByType(tab.SelectedTab.Controls);
                        if (control != null && control is TextBox) { 
                            t = (TextBox)control;
                        }
                    }
                }
                // 遍历对应关系字典
                foreach (KeyValuePair<string, Delegate> kvp in eventBinding()) {
                    // 判断当前点击的选项名是否与关系字典中的选项名对应，对应则执行关系字典中的对应方法
                    if (kvp.Key.Equals(tool.Name)) {
                        // 判断当前控件是否有与其关联的句柄
                        if (topMenuStrip.IsHandleCreated) {
                            Dictionary<Type, object> data = new Dictionary<Type, object>();
                            data.Add(typeof(TextBox), t);
                            data.Add(typeof(ToolStripMenuItem), tool);

                            topMenuStrip.Invoke(kvp.Value, new object[]{data});
                        }
                    }
                }
            }
            catch (Exception ee) {
                MessageBox.Show(ee.ToString());
            }
        }
        // 判断子项是否选中
        private void doISItemChecked(ToolStripMenuItem tool) {
            if(自动换行Item.Equals(tool)) { 
                tool.Checked = MainTextBConfig.AUTO_WORDWRAP;
            }
            
        }
        /// <summary>
        /// 设置菜单项的Image
        /// </summary>
        /// <param name="item"></param>
        private void setItemImage(ToolStripMenuItem item) {
            Image image = getItemImageDic(item.Name);
            if(image != null) { 
                item.Image = image;
                item.ImageScaling = ToolStripItemImageScaling.None;
                item.ImageAlign = ContentAlignment.MiddleRight;
            }
        }
        /// <summary>
        /// 菜单项对应Image的字典
        /// </summary>
        /// <returns></returns>
        private Image getItemImageDic(string name) { 
            
            Dictionary<string, Image> toolImageDic = new Dictionary<string, Image>();
            toolImageDic.Add(this.打开Item.Name, Core.ImageResource.打开);
            toolImageDic.Add(this.保存Item.Name, Core.ImageResource.保存);
            toolImageDic.Add(this.另存为Item.Name, Core.ImageResource.另存为);
            toolImageDic.Add(this.用记事本打开Item.Name, Core.ImageResource.记事本);
            toolImageDic.Add(this.退出Item.Name, Core.ImageResource.退出);
            
            toolImageDic.Add(this.撤销Item.Name, Core.ImageResource.撤销);
            toolImageDic.Add(this.恢复Item.Name, Core.ImageResource.重做);
            
            toolImageDic.Add(this.全选Item.Name, Core.ImageResource.全选_反相);
            toolImageDic.Add(this.剪切Item.Name, Core.ImageResource.裁剪);
            toolImageDic.Add(this.复制Item.Name, Core.ImageResource.复制);
            toolImageDic.Add(this.粘贴Item.Name, Core.ImageResource.粘贴);
            toolImageDic.Add(this.删除Item.Name, Core.ImageResource.删除del);

            toolImageDic.Add(this.查找替换Item.Name, Core.ImageResource.查找替换);
            toolImageDic.Add(this.转到行Item.Name, Core.ImageResource.转到行);
            toolImageDic.Add(this.统计字符Item.Name, Core.ImageResource.统计);

            toolImageDic.Add(this.分列Item.Name, Core.ImageResource.分列);
            toolImageDic.Add(this.代码工具Item.Name, Core.ImageResource.代码);
            toolImageDic.Add(this.首选项Item.Name, Core.ImageResource.设置);

            toolImageDic.Add(this.字体Item.Name, Core.ImageResource.字体);
            toolImageDic.Add(this.关于Item.Name, Core.ImageResource.关于);
            

            if (toolImageDic.ContainsKey(name)) { 
                return toolImageDic[name];
            } else { 
                return null;    
            }
        }
        /// <summary>
        /// 菜单项对应Name的字典
        /// </summary>
        /// <returns></returns>
        private string getItemNameDic(ToolStripMenuItem item) { 
            Dictionary<ToolStripMenuItem, string> toolImageDic = new Dictionary<ToolStripMenuItem, string>();
            toolImageDic.Add(this.文件Item, TopMenuDataLib.ItemDataLib.文件_ITEM);
            toolImageDic.Add(this.打开Item, TopMenuDataLib.ItemDataLib.打开_ITEM);
            toolImageDic.Add(this.保存Item, TopMenuDataLib.ItemDataLib.保存_ITEM);
            toolImageDic.Add(this.另存为Item, TopMenuDataLib.ItemDataLib.另存为_ITEM);
            toolImageDic.Add(this.用记事本打开Item, TopMenuDataLib.ItemDataLib.用记事本打开_ITEM);
            toolImageDic.Add(this.退出Item, TopMenuDataLib.ItemDataLib.退出_ITEM);
            toolImageDic.Add(this.编辑Item, TopMenuDataLib.ItemDataLib.编辑_ITEM);
            toolImageDic.Add(this.撤销Item, TopMenuDataLib.ItemDataLib.撤销_ITEM);
            toolImageDic.Add(this.恢复Item, TopMenuDataLib.ItemDataLib.恢复_ITEM);
            toolImageDic.Add(this.剪切Item, TopMenuDataLib.ItemDataLib.剪切_ITEM);
            toolImageDic.Add(this.复制Item, TopMenuDataLib.ItemDataLib.复制_ITEM);
            toolImageDic.Add(this.粘贴Item, TopMenuDataLib.ItemDataLib.粘贴_ITEM);
            toolImageDic.Add(this.删除Item, TopMenuDataLib.ItemDataLib.删除_ITEM);
            toolImageDic.Add(this.全选Item, TopMenuDataLib.ItemDataLib.全选_ITEM);
            toolImageDic.Add(this.查找替换Item, TopMenuDataLib.ItemDataLib.查找替换_ITEM);
            toolImageDic.Add(this.转到行Item, TopMenuDataLib.ItemDataLib.转到行_ITEM);
            toolImageDic.Add(this.统计字符Item, TopMenuDataLib.ItemDataLib.统计字符_ITEM);
            toolImageDic.Add(this.时间日期Item, TopMenuDataLib.ItemDataLib.时间日期_ITEM);
            toolImageDic.Add(this.工具Item, TopMenuDataLib.ItemDataLib.工具_ITEM);
            toolImageDic.Add(this.分列Item, TopMenuDataLib.ItemDataLib.分列_ITEM);
            toolImageDic.Add(this.添加字符Item, TopMenuDataLib.ItemDataLib.添加字符_ITEM);
            toolImageDic.Add(this.删除字符Item, TopMenuDataLib.ItemDataLib.删除字符_ITEM);
            toolImageDic.Add(this.代码工具Item, TopMenuDataLib.ItemDataLib.代码工具_ITEM);
            toolImageDic.Add(this.代码工具_java_Item, TopMenuDataLib.ItemDataLib.代码工具_java_ITEM);
            toolImageDic.Add(this.代码工具_java_生成JAVA实体类, TopMenuDataLib.ItemDataLib.代码工具_java_生成JAVA实体类_ITEM);

            toolImageDic.Add(this.首选项Item, TopMenuDataLib.ItemDataLib.首选项_ITEM);
            toolImageDic.Add(this.查看Item, TopMenuDataLib.ItemDataLib.查看_ITEM);
            toolImageDic.Add(this.自动换行Item, TopMenuDataLib.ItemDataLib.自动换行_ITEM);
            toolImageDic.Add(this.状态栏Item, TopMenuDataLib.ItemDataLib.状态栏_ITEM);
            toolImageDic.Add(this.字体Item, TopMenuDataLib.ItemDataLib.字体_ITEM);
            toolImageDic.Add(this.字体_设置字体Item, TopMenuDataLib.ItemDataLib.字体_设置字体_ITEM);
            toolImageDic.Add(this.字体_恢复默认Item, TopMenuDataLib.ItemDataLib.字体_恢复默认_ITEM);
            toolImageDic.Add(this.帮助Item, TopMenuDataLib.ItemDataLib.帮助_ITEM);
            toolImageDic.Add(this.关于Item, TopMenuDataLib.ItemDataLib.关于_ITEM);

            if (toolImageDic.ContainsKey(item)) { 
                return toolImageDic[item];
            } else { 
                return "";    
            }
        }
        /// <summary>
        /// 顶部菜单选项对应的执行类
        /// </summary>
        /// <param name="t">需要操作的文本框</param>
        /// <returns>Dictionary的对应关系，key为右键菜单选项的name,value为委托类</returns>
        private Dictionary<string, Delegate> eventBinding()
        {
            Dictionary<string, Delegate> toolBindingDic = new Dictionary<string, Delegate>();
            toolBindingDic.Add(this.打开Item.Name, new methodDelegate(TopMenuEventMet.openFileMethod));
            toolBindingDic.Add(this.另存为Item.Name, new methodDelegate(TopMenuEventMet.saveFileMethod));
            toolBindingDic.Add(this.保存Item.Name, new methodDelegate(TopMenuEventMet.saveOrSaveas));
            toolBindingDic.Add(this.用记事本打开Item.Name, new methodDelegate(TopMenuEventMet.notepadOpenFile));
            toolBindingDic.Add(this.退出Item.Name, new methodDelegate(TopMenuEventMet.exitProgram));

            toolBindingDic.Add(this.撤销Item.Name, new methodDelegate(TopMenuEventMet.cancelTextBoxCache));
            toolBindingDic.Add(this.恢复Item.Name, new methodDelegate(TopMenuEventMet.restoreTextBoxCache));

            toolBindingDic.Add(this.全选Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    TextBoxUtilsMet.textAllSelect(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.剪切Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    TextBoxUtilsMet.textSelectCut(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.复制Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    TextBoxUtilsMet.textCopy(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.粘贴Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    TextBoxUtilsMet.textPaste(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.删除Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null 
                    && data[typeof(TextBox)] is TextBox) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    TextBoxUtilsMet.textSelectDelect(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.查找替换Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                FindAndReplace.initSingleFindAndReplace(true);
                return null;}));
            toolBindingDic.Add(this.转到行Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null 
                    && data[typeof(TextBox)] is TextBox) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    RowGoToForm.openRowGoToForm(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.统计字符Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null 
                    && data[typeof(TextBox)] is TextBox) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    CharsStatistics.openCharsStatistics(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));
            toolBindingDic.Add(this.时间日期Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                if(data.ContainsKey(typeof(TextBox)) && data[typeof(TextBox)] != null 
                    && data[typeof(TextBox)] is TextBox) {
                    TextBox t = (TextBox)data[typeof(TextBox)];
                    TextBoxUtilsMet.textInsertDate(t);
                } else { 
                    MessageBox.Show("无法获取文本框");    
                }
                return null;
            }));

            toolBindingDic.Add(this.自动换行Item.Name, new methodDelegate(TopMenuEventMet.isAutoLine));
            toolBindingDic.Add(this.状态栏Item.Name, new methodDelegate(TopMenuEventMet.isStartBarDisplay));

            toolBindingDic.Add(this.分列Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                SplitCharsForm ff = SplitCharsForm.initSingleForm(true);
                ff.Show();
                //SplitCharsForm ff = SplitCharsForm.initPrototypeForm(false);
                //MainTabContent.addControlsToPage(ff, true, true);
                return null;}));
            toolBindingDic.Add(this.添加字符Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                AddCharsForm ff = AddCharsForm.initSingleForm(true);
                ff.Show();
                //AddCharsForm ff = AddCharsForm.initPrototypeForm(false);
                //MainTabContent.addControlsToPage(ff, true, true);
                return null;}));

            toolBindingDic.Add(this.代码工具_java_生成JAVA实体类.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                CreadJavaEntity ff = CreadJavaEntity.initPrototypeForm(false);
                MainTabContent.addControlsToPage(ff, true, true);
                return null;}));

            toolBindingDic.Add(this.首选项Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                SetUpMain setUpMain = new SetUpMain();
                setUpMain.ShowDialog();
                return null;}));

            toolBindingDic.Add(字体_设置字体Item.Name, new methodDelegate(TopMenuEventMet.fontDialogMethod));
            toolBindingDic.Add(字体_恢复默认Item.Name, new methodDelegate(TopMenuEventMet.textBoxFontReset));
            // 实例化关于窗体
            toolBindingDic.Add(this.关于Item.Name, new methodDelegate((Dictionary<Type , object> data) =>{
                ThereofForm.openThereofForm();
                return null;
            }));
            return toolBindingDic;
        }
        /// <summary>
        /// 重绘菜单的边框
        /// </summary>
        /// <param name="menu">需要重绘的菜单</param>
        private static void paintMenuFrame(object sender, PaintEventArgs e)
        {
            MenuStrip menu = (MenuStrip)sender;
            ControlsUtilsMet.setControlBorderStyle(e.Graphics, menu.ClientRectangle
                ,ButtonBorderStyle.Solid
                ,0,0,0,0
                , Color.FromArgb(160, 160, 160));
        }
        /// <summary>
        /// 获取右键菜单
        /// </summary>
        /// <returns></returns>
        public static MenuStrip getTopMenuStrip() { 
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TOP_MENU);
            if(con == null) {
                TopMenuContainer topMenuContainer = new TopMenuContainer();
                ControlCacheFactory.addSingletonCache(topMenuContainer.topMenuStrip);
                return topMenuContainer.topMenuStrip;
            } else { 
                return (MenuStrip)con;
            }
        }
        
    }
}
