using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using Core_Config.ConfigData.ControlConfig;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Ui.ControlEventLibrary.TextBoxEvent;
using UI.ComponentLibrary.ControlLibrary.RightMenu;
using UI.ControlEventLibrary.StatusBarEvent.TextStatusBarEvent;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawTextBox : TextBox{
        /// <summary>
        /// 当前鼠标按下的鼠标按钮
        /// </summary>
        public MouseButtons MouseDownButton { get; private set; } = MouseButtons.None;
        /// <summary>
        /// 是否按下过ctrl键
        /// </summary>
        public bool IsKeysCtrl { get; private set; } = false;
        /// <summary>
        /// 是否按下过Shift键
        /// </summary>
        public bool IsKeysShift { get; private set; } = false;
        /// <summary>
        /// 是否按下过Alt键
        /// </summary>
        public bool IsKeysAlt { get; private set; } = false;
        /// <summary>
        /// 当前键盘按下的键
        /// </summary>
        public Keys KeysDown { get; private set; } = Keys.None;
        /// <summary>
        /// 当前键盘按下的ctrl shift alt键的组合
        /// </summary>
        public Keys KeysModifiers { get; private set; } = Keys.None;
        /// <summary>
        /// Padding属性
        /// </summary>
        public Padding TextPadding { get ; set; } = Padding.Empty;
        /// <summary>
        /// 是否绑定右键菜单
        /// </summary>
        public bool IsBindingTextRightMenu { get; set; } = true;
        /// <summary>
        /// 是否加载默认配置信息
        /// </summary>
        public bool IsInitMainTextBoxConfig { get; set; } = true;
        /// <summary>
        /// 复原缓存的快捷键
        /// </summary>
        public Keys[] UndoCacheKeys  { get; set; } = new Keys[]{Keys.Control, Keys.Y};
        /// <summary>
        /// 撤销缓存的快捷键
        /// </summary>
        public Keys[] CancelCacheKeys  { get; set; } = new Keys[]{Keys.Control, Keys.Z};
        /// <summary>
        /// 是否启用文本缓存 默认快捷键为 ctrl-z ctrl-y
        /// </summary>
        public bool IsEnabledCache { get; set; } = true;
        /// <summary>
        /// 是否启用状态栏
        /// </summary>
        public bool IsEnabledStatusBar { get; set; } = true;
        /// <summary>
        /// 是否启用默认快捷键设置
        /// </summary>
        public bool IsEnabledDefaultKeys { get; set; } = true;
        /// <summary>
        /// 允许改变文本框父容器的text
        /// </summary>
        public bool IsSetParentText { get; set; } = true;

        public RedrawTextBox() { 
            // 设置控件样式
            setThisStyles();
            // 是否加载默认配置信息
            if (IsInitMainTextBoxConfig) initMainTextBoxConfig();
        }
        /// <summary>
        /// 设置控件样式
        /// </summary>
        private void setThisStyles() { 
            SetStyle(  
               //ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
               ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
               ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
               ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
               ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
               true);                                         // 设置以上值为 true  
            UpdateStyles(); 
        }
        // 设置文本框的默认配置
        private void initMainTextBoxConfig() { 
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            // 文本框姓名
            this.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF) + timeStr;
            this.TabStop = true;
            this.AllowDrop = true;
            this.BorderStyle = BorderStyle.None;
            this.Font = MainTextBConfig.TEXTBOX_FONT;
            this.ReadOnly = TextBoxDataLibcs.TEXTBOX_READ_ONLY_DEF;
            this.HideSelection = false;
            this.Location = new Point(0, 0);
            this.MaxLength = 999999999;
            this.Multiline = true;
            this.ScrollBars = ScrollBars.Both;
            //ControlsUtilsMet.timersMet(100, (object sender, ElapsedEventArgs e)=>{
            //    if(this.InvokeRequired) { 
            //        this.Invoke(new EventHandler(delegate {
            //            if(this.Parent != null) { 
            //               this.Size = Parent.ClientSize;
            //                // 设置文本框四周锚定
            //                
            //               ((System.Timers.Timer)sender).Dispose();
            //            }
            //        }));
            //    }
                
            //});
            this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.TabIndex = 0;
            this.WordWrap = MainTextBConfig.AUTO_WORDWRAP;
            this.TextPadding = new Padding(5);
            // 将文件默认编码写入到文本框tag数据中
            TextBoxUtilsMet.textAddTag(this, TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, TextBoxDataLibcs.TEXTBOX_ECODING_DEF);
            
            // 消除控件重绘闪烁
            ControlsUtilsMet.clearRedrawFlashing(this);
        }
        // 调整大小事件
        protected override void OnResize(EventArgs e) {
            if(TextPadding != Padding.Empty) setTextDispLayout();
            base.OnResize(e);
        }
        // 获得焦点事件
        protected override void OnGotFocus(EventArgs e) {
            // 将文本框打开的文件路径显示到文本框的父容器下目录下
            if(IsSetParentText) setParentTextByFileName();
            // 设置文本的pading属性
            if(TextPadding != Padding.Empty) setTextDispLayout();
            base.OnGotFocus(e);
        }
        // 文本改变事件
        protected override void OnTextChanged(EventArgs e) {
            // 放入缓存
            if(IsEnabledCache) { 
                if(!this.ReadOnly) TextBoxCache.addCacheFactory(this);    
            }
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.内容改变事件, this);
            base.OnTextChanged(e);
        }
        // 大小改变事件
        protected override void OnSizeChanged(EventArgs e) {
            if(TextPadding != Padding.Empty) setTextDispLayout();
            base.OnSizeChanged(e);
        }
        // 鼠标移动事件
        protected override void OnMouseMove(MouseEventArgs e) {
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.鼠标移过事件, this);
            base.OnMouseMove(e);
        }
        // 鼠标按下事件
        protected override void OnMouseDown(MouseEventArgs e) {
            MouseDownButton = e.Button;
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.鼠标按下事件, this);
            base.OnMouseDown(e);
        }
        // 鼠标松开事件
        protected override void OnMouseUp(MouseEventArgs e) {
            MouseDownButton = MouseButtons.None;
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.鼠标松开事件, this);
            base.OnMouseUp(e);
        }
        // 鼠标移入事件
        protected override void OnMouseEnter(EventArgs e) {
            // 绑定右键菜单
            if(IsBindingTextRightMenu && this.ContextMenuStrip == null) { 
                TextRightMenu.bindingTextBox(this);    
            }
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.鼠标移入事件, this);
            base.OnMouseEnter(e);
        }
        // 键盘按键按下事件
        protected override void OnKeyDown(KeyEventArgs e) {
            KeysDown = e.KeyCode;
            KeysModifiers = e.Modifiers;
            IsKeysCtrl = e.Control;
            IsKeysShift = e.Shift;
            IsKeysAlt = e.Alt;
            textBoxkeyDownBinding();
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.键盘按下事件, this);
            base.OnKeyDown(e);
        }
        // 键盘按键松开事件
        protected override void OnKeyUp(KeyEventArgs e) {
            // 设置状态栏
            if(IsEnabledStatusBar) setOnStatusBarEventBind(TextBoxEventTypeEnum.键盘松开事件, this);
            base.OnKeyUp(e);
        }
        // 将文件拖入边界事件
        protected override void OnDragEnter(DragEventArgs drgevent) {
            if (this.AllowDrop) { 
                if (drgevent.Data.GetDataPresent(DataFormats.FileDrop)){
                    drgevent.Effect = DragDropEffects.All;
                } else {
                    drgevent.Effect = DragDropEffects.None;
                }
            }
            base.OnDragEnter(drgevent);
        }
        // 文本框拖放完成事件
        protected override void OnDragDrop(DragEventArgs e) {
            // 获取拖放文件的文件名
            if (this.AllowDrop) { 
                Array arrFile = (Array)e.Data.GetData(DataFormats.FileDrop);
                string path = arrFile != null && arrFile.Length > 0? arrFile.GetValue(0).ToString() : null;
                FileUtilsMet.setTextBoxValByPath(this, path, Encoding.UTF8);
            }
            base.OnDragDrop(e);
        }
        /// <summary>
        /// 设置文本具有pading属性
        /// </summary>
        private void setTextDispLayout() {
            Rectangle rect = new Rectangle();
            WinApiUtilsMet.SendMessage(Handle, 178, (IntPtr)0, ref rect);
            int top = TextPadding != Padding.Empty? TextPadding.Top : 1;
            int bottom = TextPadding != Padding.Empty? TextPadding.Bottom : 1;
            int left = TextPadding != Padding.Empty? TextPadding.Left : 1;
            int right = TextPadding != Padding.Empty? TextPadding.Right : 1;
            rect.Y = top;
            rect.X = left;
            rect.Height = ClientSize.Height - bottom;
            rect.Width = ClientSize.Width - right;

            WinApiUtilsMet.SendMessage(Handle, 179, IntPtr.Zero, ref rect);
        }
        /// <summary>
        /// 文本框键盘按下事件绑定
        /// </summary>
        private void textBoxkeyDownBinding() {
            try {
                TextBox t = this;
                if(t.ReadOnly) MessageBox.Show("文本框为只读的");
                // 全选
                if(IsKeysCtrl && KeysDown.Equals(Keys.A)) {
                    TextBoxUtilsMet.textAllSelect(t);
                }
                // 启用缓存
                if(IsEnabledCache) { 
                    // 撤销
                    if(IsKeysCtrl && KeysDown.Equals(Keys.Z)) { 
                        // 将文本框置于撤销状态
                        if (!t.ReadOnly) { 
                            // 将文本框置于撤销状态
                            TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_IS_CANCEL, true);
                            TextBoxCache.cancelCache(t);
                        }
                    }
                    // 恢复
                    if(IsKeysCtrl && KeysDown.Equals(Keys.Y)) { 
                        // 将文本框置于恢复状态
                        // 非只读才能撤销
                        if (!t.ReadOnly) { 
                            // 将文本框置于恢复状态
                            TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_IS_RESTORE, true);
                            TextBoxCache.restoreCache(t);
                        }
                    }
                }
                
            } catch(Exception exc) {
                Console.WriteLine(exc.StackTrace);
            }
        }
        /// <summary>
        /// 将文本框打开的文件路径显示到文本框的父容器下目录下
        /// </summary>
        public void setParentTextByFileName() {
            TextBox t = this;
            // 获取文本框的父容器
            Control con = t.Parent;
            // 判断父容器是否为TabPage
            if(con.GetType().Equals(typeof(TabPage))) {
                ControlsUtilsMet.asynchronousMet(t, 300, delegate{ 
                    // 判断Tag中是否存在保存路径
                    if(TextBoxUtilsMet.getDicTextTag(t).ContainsKey(TextBoxTagKey.SAVE_FILE_PATH)) {
                        string filepath = TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.SAVE_FILE_PATH].ToString();
                        TabPage page = (TabPage)t.Parent;
                        string[] pathArr = FileUtilsMet.getPathArr(filepath);
                        page.ResetText();
                        
                        // 设置标签文本
                        page.Text = pathArr[1];
                        // 设置提示文本
                        page.ToolTipText = filepath;
                    }
                });
                
            }
        }
        /// <summary>
        /// 关于状态栏的事件绑定
        /// </summary>
        private static void setOnStatusBarEventBind(TextBoxEventTypeEnum eventType, TextBox textBox) { 
            StatusStrip toolStrip = ControlCacheFactory.getSingletonCache().ContainsKey(EnumUtilsMet.GetDescription(DefaultNameEnum.TOOL_START)) ?
            (StatusStrip)ControlCacheFactory.getSingletonCache()[EnumUtilsMet.GetDescription(DefaultNameEnum.TOOL_START)]:null;

            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), textBox);
            data.Add(typeof(StatusStrip), toolStrip);
            switch(eventType) { 
                case TextBoxEventTypeEnum.内容改变事件 : 
                    /*============赋值给状态栏总行数与字符数===================*/
                    TextStatusBarEventMet.setRowChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                break;
                case TextBoxEventTypeEnum.鼠标移过事件 :
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                break;
                case TextBoxEventTypeEnum.鼠标松开事件 :
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                break;
                case TextBoxEventTypeEnum.鼠标按下事件 :
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                break;
                case TextBoxEventTypeEnum.获取焦点事件 :
                    /*============设置文本框中的编码到状态栏中===================*/
                    TextStatusBarEventMet.setToolSatrtEcoding(data);
                    /*============赋值给状态栏总行数与字符数===================*/
                    TextStatusBarEventMet.setRowChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                    /*============赋值给状态栏只读状态===================*/
                    TextStatusBarEventMet.refreshTextReadOnly(data);
                break;
                case TextBoxEventTypeEnum.键盘按下事件 :
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                break;
                case TextBoxEventTypeEnum.键盘松开事件 : 
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                    /*============将大小写状态赋值给状态栏===================*/
                    TextStatusBarEventMet.setCaseKey(data);
                break;
            }
        }
    }
    internal enum TextBoxEventTypeEnum {
        鼠标按下事件 = 0,
        获取焦点事件 = 1,
        失去焦点事件 = 2,
        内容改变事件 = 3,
        鼠标移过事件 = 4,
        鼠标松开事件 = 5,
        键盘按下事件 = 6,
        键盘松开事件 = 7,
        控件启用事件 = 8,
        鼠标移入事件 = 9,
        文件拖放完成事件 = 10
        
    }
}
