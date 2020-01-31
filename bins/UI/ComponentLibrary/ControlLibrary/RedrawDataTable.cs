using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.OperateCache.DataViewOperateCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary.RightMenu;
using UI.ComponentLibrary.MethodLibrary.Interface;
using UI.ComponentLibrary.MethodLibrary.Util;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawDataTable : DataGridView, IComponentInitMode<Control>{
        internal RedrawDataTable() { 
            setThisStyles();
            initDatatableDefConfig();
        }
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            DataGridView conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.DATA_GRID_VIEW_REDRAW);
            if(con == null || !(con is DataGridView)) {
                conThis = this;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.DATA_GRID_VIEW_REDRAW);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (DataGridView)con;
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
            RedrawDataTable conThis = this;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.DATA_GRID_VIEW_REDRAW)+DateTime.Now.Ticks.ToString();;
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.DATA_GRID_VIEW_REDRAW, conThis);
            return conThis;
        }
        private void setThisStyles() { 
            SetStyle(
               ControlStyles.UserMouse |
               ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
               ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
               ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
               true);
            SetStyle(
               ControlStyles.ResizeRedraw,
               false);
            UpdateStyles();
        }
        /**=================公有属性设置区======================*/
        
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
        /// 当前鼠标按下的列索引
        /// </summary>
        public int MouseDownColumnIndex { get; private set; } = -1;
        /// <summary>
        /// 当前鼠标按下的行索引
        /// </summary>
        public int MouseDownRowIndex { get; private set; } = -1;
        /// <summary>
        /// 单元格默认宽度
        /// </summary>
        public int CellDefaultWidth { get; set; } = 100;
        /// <summary>
        /// 单元格默认高度
        /// </summary>
        public int CellDefaultHeight { get; set; } = 25;
        /// <summary>
        /// 列标题的默认高度
        /// </summary>
        public int ColumnHeadDefaultHeight { get; set; } = 23;
        /// <summary>
        /// 列是否排序
        /// </summary>
        public bool ColumnSortMode { get; set; } = false;
        /// <summary>
        /// 选中单元格的标题的背景色
        /// </summary>
        public Color DefaultCellHeadBackCor { get; set; } = ColorTranslator.FromHtml("#6CADE1");
        /// <summary>
        /// 选中单元格的标题的前景色
        /// </summary>
        public Color DefaultCellHeadFontCor { get; set; } = Color.Black;
        /// <summary>
        /// 是否显示行号
        /// </summary>
        public bool IsShowLineNumber { get; set; } = true;


        /// <summary>
        /// 表格默认配置
        /// </summary>
        /// <param name="cellHeight">单元格高</param>
        /// <param name="colHeadersHeight">列标题高</param>
        private void initDatatableDefConfig() {
            ControlsUtils.AsynchronousMethod(this, 1, delegate{ 
                // 设置单元格默认样式
                setDefaultCellStyle();
                // 设置列标题默认样式
                setDefaultColumnHeadStyle();
                // 设置行标题默认样式
                setDefaultRowHeadStyle();
            });
            // 设置基本样式
            setDataTableStyle();
            // 绑定右键菜单并使用自定义的样式
            DataGridViewRightMenu.bindingDataGridView(this);
        }
        private void setDataTableStyle() {    
            Name = EnumUtils.GetDescription(DefaultNameEnum.DATA_GRID_VIEW_REDRAW);
            Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            // 边框
            BackgroundColor = Color.White;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            BorderStyle = BorderStyle.None;
            GridColor = ColorTranslator.FromHtml("#D4D4D4");
            ColumnSortMode = false;
            EnableHeadersVisualStyles = false;
            // 标题调整模式
            RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            SelectionMode = DataGridViewSelectionMode.CellSelect;
            
            ShowCellToolTips = true;
            TabIndex = 0;
            TabStop = false;
            StandardTab = false;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToOrderColumns = false;
            AllowUserToResizeColumns = true;
            AllowUserToResizeRows = true;

            EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            ScrollBars = ScrollBars.Both;
        }
        /// <summary>
        /// 设置单元格默认样式
        /// </summary>
        private void setDefaultCellStyle() { 
            DataGridViewCellStyle style3 = new DataGridViewCellStyle();
            style3.Alignment = DataGridViewContentAlignment.TopLeft;
            style3.BackColor = Color.White;
            style3.Font = new Font("微软雅黑", 9F);
            style3.ForeColor = Color.Black;
            style3.SelectionBackColor = ColorTranslator.FromHtml("#0078D7");
            style3.SelectionForeColor = Color.White;
            this.RowTemplate.Height = CellDefaultHeight;
            this.RowsDefaultCellStyle = style3;
        }
        /// <summary>
        /// 设置列标题默认样式
        /// </summary>
        private void setDefaultColumnHeadStyle() { 
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            style1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            style1.BackColor = SystemColors.Control;
            style1.Font = new Font("微软雅黑", 9F);
            style1.ForeColor = Color.Black;
            style1.SelectionBackColor = DefaultCellHeadBackCor;
            style1.SelectionForeColor = DefaultCellHeadFontCor;
            style1.WrapMode = DataGridViewTriState.True;
            // 列标题的高
            ColumnHeadersHeight = ColumnHeadDefaultHeight;
            this.ColumnHeadersDefaultCellStyle = style1;
        }
        /// <summary>
        /// 设置行标题默认样式
        /// </summary>
        private void setDefaultRowHeadStyle() { 
            DataGridViewCellStyle style2 = new DataGridViewCellStyle();
            style2.Alignment = DataGridViewContentAlignment.MiddleRight;
            style2.BackColor = SystemColors.Control;
            style2.Font = new Font("微软雅黑", 9F);
            style2.ForeColor = Color.Black;
            style2.Padding = new Padding(1);
            style2.SelectionBackColor = DefaultCellHeadBackCor;
            style2.SelectionForeColor = DefaultCellHeadFontCor;
            style2.WrapMode = DataGridViewTriState.True;
            if(IsShowLineNumber) RowHeadersWidth = 60;
            this.RowHeadersDefaultCellStyle = style2;
        }
        // 单元格鼠标按下事件
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e) {
            MouseDownButton = e.Button;
            MouseDownColumnIndex = e.ColumnIndex;
            MouseDownRowIndex = e.RowIndex;
            // 当要调整列宽或行高时不选中行
            if(!Cursor.Equals(Cursors.SizeWE) && !Cursor.Equals(Cursors.SizeNS)
                && e.Button.Equals(MouseButtons.Left)) {
                // 全选该列或行
                DataGridViewUtilMet.selectAllCellBySingle(this, e.ColumnIndex, e.RowIndex, true, true);
            }
            base.OnCellMouseDown(e);
        }
        // 单元格鼠标释放事件
        protected override void OnCellMouseUp(DataGridViewCellMouseEventArgs e) {
            MouseDownButton = MouseButtons.None;
            MouseDownColumnIndex = -1;
            MouseDownRowIndex = -1;
            base.OnCellMouseUp(e);
        }
        // 键盘按键按下事件
        protected override void OnKeyDown(KeyEventArgs e) {
            KeysDown = e.KeyCode;
            KeysModifiers = e.Modifiers;
            IsKeysCtrl = e.Control;
            IsKeysShift = e.Shift;
            IsKeysAlt = e.Alt;
            MyKeyDown();
            base.OnKeyDown(e);
        }
        // 行添加事件
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e) {
            for (int i = 0; i< e.RowCount; i++) { 
                // 添加行号
                if(IsShowLineNumber) Rows[i+e.RowIndex].HeaderCell.Value = (i+1+e.RowIndex).ToString();
                // 改变行高度
                Rows[i+e.RowIndex].Height = CellDefaultHeight;
            }
            base.OnRowsAdded(e);
        }
        // 列添加事件
        protected override void OnColumnAdded(DataGridViewColumnEventArgs e) {
            // 改变列宽
            e.Column.Width = CellDefaultWidth;
            if(!ColumnSortMode) e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            base.OnColumnAdded(e);
        }
        // 单元格文本改变事件
        protected override void OnCellValueChanged(DataGridViewCellEventArgs e) {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0) { 
                DataGridViewCell cell = this.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if(!"".Equals(cell.ToolTipText)) cell.ToolTipText = cell.Value.ToString();
            }
            
            base.OnCellValueChanged(e);
        }
        // 键盘按下事件
        private void MyKeyDown() {
            // ctrl+ c
            if(IsKeysCtrl && Keys.C.Equals(KeysDown)) {
                DataGridViewUtilMet.copySelectCellText(this);
            }
            // ctrl+ v
            if(IsKeysCtrl && Keys.V.Equals(KeysDown)) {
                DataGridViewUtilMet.pasteTextToSelCell(this);
            }
            // del
            if(Keys.Delete.Equals(KeysDown)) { 
                DataGridViewUtilMet.delSelectCellText(this);
            }
        }
    }
}
