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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary.RightMenu;
using UI.ComponentLibrary.MethodLibrary.Interface;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawTextBox : TextBox, IComponentInitMode<Control> {
        /// <summary>
        /// 当前鼠标松开时的位置
        /// </summary>
        public Point MouseUpLocation { get; private set; } = Point.Empty;
        /// <summary>
        /// 当前鼠标按下的位置
        /// </summary>
        public Point MouseDownLocation { get; private set; } = Point.Empty;
        /// <summary>
        /// 当前鼠标的位置
        /// </summary>
        public Point MouseMoveLocation { get; private set; } = Point.Empty;
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
        /// 是否启用默认快捷键设置
        /// </summary>
        public bool IsEnabledDefaultKeys { get; set; } = true;
        /// <summary>
        /// 允许改变文本框父容器的text
        /// </summary>
        public bool IsSetParentText { get; set; } = true;
        // 启用拖放选中文本时的鼠标样式
        private Cursor selectTextDragCur = Cursors.SizeAll;
        internal RedrawTextBox() { 
            // 设置控件样式
            setThisStyles();
            // 加载配置信息
            initControlDefConfig();
        }
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            RedrawTextBox conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.TEXTBOX_NAME_DEF);
            if(con == null || !(con is RedrawTextBox)) {
                conThis = this;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (RedrawTextBox)con;
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
            RedrawTextBox conThis = this;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF)+DateTime.Now.Ticks.ToString();;
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.TEXTBOX_NAME_DEF, conThis);
            return conThis;
        }
        /// <summary>
        /// 设置控件样式
        /// </summary>
        private void setThisStyles() { 
            SetStyle(  
               ControlStyles.ContainerControl |         // 控件将自行绘制，而不是通过操作系统来绘制  
               ControlStyles.OptimizedDoubleBuffer |    // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
               ControlStyles.AllPaintingInWmPaint |     // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
               ControlStyles.ResizeRedraw |             // 在调整控件大小时重绘控件  
               ControlStyles.SupportsTransparentBackColor // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明
               //ControlStyles.UserMouse // 自身处理鼠标事件
               ,
               true);
            UpdateStyles(); 
        }
        // 设置文本框的默认配置
        private void initControlDefConfig() { 
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            // 文本框姓名
            this.Name = EnumUtils.GetDescription(DefaultNameEnum.TEXTBOX_NAME_DEF) + timeStr;
            this.TabStop = true;
            this.AllowDrop = true;
            this.BorderStyle = BorderStyle.None;
            this.Font = MainTextBConfig.TEXTBOX_FONT;
            this.ReadOnly = TextBoxDataLibcs.TEXTBOX_READ_ONLY_DEF;
            this.HideSelection = false;
            this.Location = new Point(0, 0);
            this.MaxLength = 999999999;
            this.Multiline = true;
            this.ShortcutsEnabled = false;
            this.ScrollBars = ScrollBars.Both;
            this.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.TabIndex = 0;
            this.WordWrap = MainTextBConfig.AUTO_WORDWRAP;
            this.AcceptsTab = false;
            this.TextPadding = new Padding(3);
            // 将文件默认编码写入到文本框tag数据中
            TextBoxUtils.TextBoxAddTag(this, TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, TextBoxDataLibcs.TEXTBOX_ECODING_DEF);
            // 消除控件重绘闪烁
            ControlsUtils.ClearRedrawFlashing(this);
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
            base.OnTextChanged(e);
        }
        // 大小改变事件
        protected override void OnSizeChanged(EventArgs e) {
            if(TextPadding != Padding.Empty) setTextDispLayout();
            base.OnSizeChanged(e);
        }
        // 鼠标按下事件
        protected override void OnMouseDown(MouseEventArgs e) {
            MouseDownLocation = e.Location;
            MouseDownButton = e.Button;
            // 开启或关闭鼠标选中文本的拖放
            //diIsSelectTextDrag();
            // 鼠标按钮按下选中文本
            //mouseDownSelectText();
            
            base.OnMouseDown(e);
        }
        // 鼠标松开事件
        protected override void OnMouseUp(MouseEventArgs e) {
            MouseUpLocation = e.Location;
            this.MouseDownLocation = Point.Empty;
            // 鼠标松开取消文本选中
            doIsMouseUpSelText();
            MouseDownButton = MouseButtons.None;
            this.Cursor = Cursors.IBeam;
            base.OnMouseUp(e);
        }
        // 鼠标移入事件
        protected override void OnMouseEnter(EventArgs e) {
            // 绑定右键菜单
            if(IsBindingTextRightMenu && this.ContextMenuStrip == null) { 
                Control ccc = UIComponentFactory.getSingleControl(DefaultNameEnum.TEXT_RIGHT_MENU, true);
                if(ccc != null && ccc is ContextMenuStrip) this.ContextMenuStrip = (ContextMenuStrip)ccc; 
            }
            // 设置状态栏
            base.OnMouseEnter(e);
        }
        // 鼠标移动事件
        protected override void OnMouseMove(MouseEventArgs e) {
            MouseMoveLocation = e.Location;
            if(e.Button.Equals(MouseButtons.Left)) {
                // 选中文本
                //mouseMoveSelectText();
            }
            base.OnMouseMove(e);
        }
        // 鼠标双击事件
        protected override void OnMouseDoubleClick(MouseEventArgs e) {
            // 双击选中文本
            //doubleClickSelectText();
            base.OnMouseDoubleClick(e);
        }
        // 键盘按键按下事件
        protected override void OnKeyDown(KeyEventArgs e) {
            KeysDown = e.KeyCode;
            KeysModifiers = e.Modifiers;
            IsKeysCtrl = e.Control;
            IsKeysShift = e.Shift;
            IsKeysAlt = e.Alt;
            textBoxkeyDownBinding();
            base.OnKeyDown(e);
        }
        // 键盘按键松开事件
        protected override void OnKeyUp(KeyEventArgs e) {
            KeysDown = Keys.None;
            KeysModifiers = Keys.None;
            IsKeysCtrl = false;
            IsKeysShift = false;
            IsKeysAlt = false;
            base.OnKeyUp(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e) {
            if(Keys.Tab.Equals(KeysDown) || (IsKeysShift && Keys.Tab.Equals(KeysDown))) { 
                outTabTotextbox();
                e.Handled = true;
            }
            base.OnKeyPress(e);
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
                FileUtils.SetTextBoxValByPath(this, path, Encoding.UTF8);
            }
            base.OnDragDrop(e);
        }
        
        /// <summary>
        /// 设置文本具有pading属性
        /// </summary>
        private void setTextDispLayout() {
            Rectangle rect = new Rectangle();
            WinApiUtils.SendMessage(Handle, 178, (IntPtr)0, ref rect);
            int top = TextPadding != Padding.Empty? TextPadding.Top : 1;
            int bottom = TextPadding != Padding.Empty? TextPadding.Bottom : 1;
            int left = TextPadding != Padding.Empty? TextPadding.Left : 1;
            int right = TextPadding != Padding.Empty? TextPadding.Right : 1;
            rect.Y = top;
            rect.X = left;
            rect.Height = ClientSize.Height - bottom;
            rect.Width = ClientSize.Width - right;

            WinApiUtils.SendMessage(Handle, 179, IntPtr.Zero, ref rect);
        }
        // 重写GetCharIndexFromPosition方法
        public new int GetCharIndexFromPosition(Point p) { 
            var charIndex = base.GetCharIndexFromPosition(p);
            var charPosition = base.GetPositionFromCharIndex(charIndex);
            if (p.X > charPosition.X && charIndex == TextLength-1) charIndex++;
            return charIndex;
        }
        /// <summary>
        /// 文本框键盘按下事件绑定
        /// </summary>
        private void textBoxkeyDownBinding() {
            try {
                TextBox t = this;
                if(t.ReadOnly) MessageBox.Show("文本框为只读的");
                // 全选
                /*if(IsKeysCtrl && KeysDown.Equals(Keys.A)) {
                    TextBoxUtils.TextBoxAllSelect(t);
                }*/
                // 启用缓存
                /*if(IsEnabledCache) { 
                    // 撤销
                    if(IsKeysCtrl && KeysDown.Equals(Keys.Z)) { 
                        // 将文本框置于撤销状态
                        if (!t.ReadOnly) { 
                            // 将文本框置于撤销状态
                            TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_IS_CANCEL, true);
                            TextBoxCache.cancelCache(t);
                        }
                    }
                    // 恢复
                    if(IsKeysCtrl && KeysDown.Equals(Keys.Y)) { 
                        // 将文本框置于恢复状态
                        // 非只读才能撤销
                        if (!t.ReadOnly) { 
                            // 将文本框置于恢复状态
                            TextBoxUtils.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_IS_RESTORE, true);
                            TextBoxCache.restoreCache(t);
                        }
                    }
                }*/
                
            } catch(Exception exc) {
                Console.WriteLine(exc.StackTrace);
            }
        }
        /// <summary>
        /// 将文本框打开的文件路径显示到文本框的父容器下目录下
        /// </summary>
        private void setParentTextByFileName() {
            TextBox t = this;
            // 获取文本框的父容器
            Control con = t.Parent;
            // 判断父容器是否为TabPage
            if(con.GetType().Equals(typeof(TabPage))) {
                ControlsUtils.AsynchronousMethod(t, 300, delegate{ 
                    // 判断Tag中是否存在保存路径
                    if(TextBoxUtils.GetTextTagToMap(t).ContainsKey(TextBoxTagKey.SAVE_FILE_PATH)) {
                        string filepath = TextBoxUtils.GetTextTagToMap(t)[TextBoxTagKey.SAVE_FILE_PATH].ToString();
                        TabPage page = (TabPage)t.Parent;
                        string[] pathArr = FileUtils.GetPathArr(filepath);
                        page.ResetText();
                        
                        // 设置标签文本
                        page.Text = pathArr[1];
                        // 设置提示文本
                        page.ToolTipText = filepath;
                    }
                });
                
            }
        }
        private bool isMouseEnterSelectText() { 
            return isMouseEnterSelectText(MouseMoveLocation);
        }
        // 判断鼠标是否处于文本框选中文本范围内
        private bool isMouseEnterSelectText(Point p) { 
            bool retBool = false;
            Point p2 = GetPositionFromCharIndex(SelectionStart);
            Point p3 = GetPositionFromCharIndex(SelectionStart + SelectionLength);
            int x = p.X, y = p.Y;
            if(x > p2.X && y > p2.Y && x < p3.X && y < p3.Y) { 
                retBool = true;
            };
            return retBool;
        }
        // 双击选中文本
        private void doubleClickSelectText() { 
            int mouseIndex = SelectionStart;
            // 是否处于行的末尾
            bool isMouseLineEnd = TextBoxUtils.IsPointLineEnd(this, mouseIndex);
            if(isMouseLineEnd) { 
                // 选中整行
                int i = GetFirstCharIndexOfCurrentLine();
                Select(i, mouseIndex-i);
            } else { 
                int headMatch = 0;
                int tailMatch = 0;
                for (int i = 1; i <= TextLength; i++) {
                    if (headMatch.Equals(0)) {
                        if (SelectionStart - i >= 0) {
                            if(Regex.IsMatch(Text[SelectionStart - i].ToString(), @"\W")) {
                                headMatch = (SelectionStart - i)+1;
                            }
                        } else {
                            headMatch = 0;
                        }
                    }
                    if (tailMatch.Equals(0)) {
                        if (SelectionStart + i < TextLength) {
                            if (Regex.IsMatch(Text[SelectionStart + i].ToString(), @"\W")) {
                                tailMatch = SelectionStart + i;
                            }
                        } else {
                            tailMatch = (SelectionStart +i);
                        }
                    }
                    if (!headMatch.Equals(0) && !tailMatch.Equals(0)) { break; }
                }
                SelectionStart = headMatch;
                SelectionLength = tailMatch - SelectionStart;
            }
        }
        // 鼠标松开时选中文本
        private void doIsMouseUpSelText() {
            // 判断鼠标不在文本选中区域并且当前鼠标不为拖放选中字符串类型的鼠标
            if(isMouseEnterSelectText(MouseUpLocation) && !Cursor.Equals(selectTextDragCur)) { 
                //SelectionLength = 0;
            } else if(Cursor.Equals(selectTextDragCur)){ 
                //int selIndex = this.SelectionStart;
                //string selText = this.SelectedText;
                //int mouseIndex = GetCharIndexFromPosition(MouseUpLocation);
                //Select(mouseIndex,1);
                //Paste(selText+Text.ToArray()[mouseIndex+1]);
                //Select();
                //Paste("");
            }
        }
        // 开启或关闭鼠标选中文本的拖放
        private void diIsSelectTextDrag() { 
            // 判断是否要启用拖放操作
            ControlsUtils.AsynchronousMethod(this, 100, new EventHandler((object sender1, EventArgs e1)=>{ 
                Point p = MouseDownLocation;
                if(p.X <0) p.X = 0;
                if(p.Y <0) p.Y = 0;
                if(MouseDownButton.Equals(MouseButtons.Left) && isMouseEnterSelectText(p)) { 
                    Cursor = selectTextDragCur;
                };    
            }));
        }
        // 鼠标按钮按下选中文本
        private void mouseDownSelectText() { 
            if(MouseButtons.Left.Equals(MouseDownButton)) {// 点击的左键
                Point p = MouseDownLocation;
                if(p.X <0) p.X = 0;
                if(p.Y <0) p.Y = 0;
                int mouseIndex = this.GetCharIndexFromPosition(p);
                int selIndex = this.SelectionStart;
                if(KeysDown.Equals(Keys.ShiftKey)) {// 按下了shift 
                    if(mouseIndex < selIndex) {
                        this.SelectionStart = mouseIndex;
                    } else { 
                        this.SelectionStart = SelectionStart;
                    }
                    this.SelectionLength = Math.Abs(mouseIndex-selIndex);
                } else { 
                    if(isMouseEnterSelectText(p) /*|| SelectionLength == 0*/) {
                        ControlsUtils.AsynchronousMethod(this, 120, new EventHandler((object sender1, EventArgs e1) => {
                            if (!Cursor.Equals(selectTextDragCur) && MouseDownButton.Equals(MouseButtons.None)) {
                                SelectionLength = 0;
                                SelectionStart = mouseIndex;
                            }
                        }));
                    } else { 
                        SelectionLength = 0;
                        SelectionStart = mouseIndex;
                    }
                }
            }
        }
        private void setScrollOfMouse() {
            int MinH, MaxH, MinV, MaxV;
            int SB_HORZ = 0x0;
            int SB_VERT = 0x1;
            int WM_HSCROLL = 0x114;
            int WM_VSCROLL = 0x115;
            int SB_THUMBPOSITION = 4;
            // 获得鼠标在X和Y两个方向上的移动量。除以10是为是让移动页面的速度变慢一点。而前面的负号则是用来调节页面移动方向的。
            WinApiUtils.GetScrollRange(Handle, 0, out MinH, out MaxH); 
            WinApiUtils.GetScrollRange(Handle, 1, out MinV, out MaxV);
            int MoveX = 0;
            Console.WriteLine(MaxH);
            if(MouseMoveLocation.X > MouseDownLocation.X) { //向左移动
                MoveX = MouseMoveLocation.X-(this.FindForm().Width/3);
                MoveX = MoveX <0? 0 : MoveX;
            } else { // 向右移动鼠标
                MoveX = MaxH;
            }
            int MoveY = Math.Abs(MouseMoveLocation.Y - MouseDownLocation.Y); 
            ////获取滚动条的最大最小位置和当前位置 

            Console.WriteLine(MouseDownLocation);
            WinApiUtils.SetScrollPos(Handle, SB_HORZ, MoveX, true);// 水平滚动栏
            WinApiUtils.PostMessage(Handle, WM_HSCROLL, SB_THUMBPOSITION + 0x10000 * MoveX, 0);//告诉控件移动页面内容到相应的位置上 
            //WinApiUtilsMet.SetScrollPos(Handle, SB_VERT, MoveY, true);// 垂直滚动栏
            //WinApiUtilsMet.PostMessage(Handle, WM_VSCROLL, SB_THUMBPOSITION + 0x10000 * MoveY, 0); 
        }
        // 鼠标移动选中文本
        private void mouseMoveSelectText() { 
            Point p = MouseMoveLocation;
            if(p.X < 0) p.X = 0;
            if(p.Y < 0) p.Y = 0; 
            // 判断是否启用了选中文本拖放
            if(Cursor.Equals(selectTextDragCur)) {
                if(!isMouseEnterSelectText()) { // 判断鼠标不在当前选定文本的范围内
                    
                }
            } else {
                // 设置滚动条跟随鼠标翻页
                //setScrollOfMouse();
                int mouseIndex = this.GetCharIndexFromPosition(p);
                int selIndex = this.GetCharIndexFromPosition(MouseDownLocation);
                this.SelectionLength = Math.Abs(mouseIndex-selIndex);
                if(mouseIndex >= selIndex) {
                    this.SelectionStart = selIndex;
                } else { 
                    this.SelectionStart = mouseIndex;
                }
            }
        }
        // 设置tab键到文本框
        private void outTabTotextbox() { 
            string chars = "    ";
            // string chars = "\t";
            int index = this.SelectionStart;
            int selLen = this.SelectionLength;
            string selText = this.SelectedText;
            int tabLen = "\t".Length;
            if(IsKeysShift && Keys.Tab.Equals(KeysDown)) {
                if (selLen > 0) { 
                    string s = StringUtils.CharTrimByLine(selText, chars, 1, 1);
                    this.Paste(s);
                    this.Select(index, s.Length);
                }
            } else if(Keys.Tab.Equals(KeysDown)){
                if(selLen > 0) {
                    string s = StringUtils.InsertLineFirstAndLast(selText, chars, "", true);
                    this.Paste(s);
                    this.Select(index, s.Length);
                } else { 
                    this.Paste(chars);
                }
                
            }
            
        }
        
    }
}
