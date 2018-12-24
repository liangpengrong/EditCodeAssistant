using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using PubMethodLibrary;
using PubCacheArea;

namespace PubControlLibrary {
    public partial class MyDataTable : Component{
        public MyDataTable() {
            // 初始化控件
            InitializeComponent();
            // 加载数据表格配置
            dataViewConfig();
        }
        /// <summary>
        /// 存放当前鼠标所在单元格
        /// </summary>
        private DataGridViewCell mouseCell = null;
        /// <summary>
        /// 单元格默认宽度
        /// </summary>
        public int cellDefWidth = 100;
        /// <summary>
        /// 单元格默认高度
        /// </summary>
        public int cellDefHeight = 25;
        /// <summary>
        /// 列标题的默认高度
        /// </summary>
        public int colHeadersHeight = 20;
        
        /// <summary>
        /// 选中单元格的标题的背景色
        /// </summary>
        public Color selCellHeadBack = ColorTranslator.FromHtml("#6CADE1");
        /// <summary>
        /// 选中单元格的标题的前景色
        /// </summary>
        public Color selCellHeadFontC = Color.Black;

        // 点击的单元格的列
        private int clickColIndex = 0;
        // 点击的单元格的行
        private int clickRowIndex = 0;
        // 当前鼠标位置的单元格的列
        private int mouseColIndex = 0;
        // 当前鼠标位置的单元格的行
        private int mouseRowIndex = 0;

        /// <summary>
        /// 撤销 恢复的缓存工厂
        /// </summary>
        private Dictionary<string, List<DataViewCacheModel>> cacheFactory = new Dictionary<string, List<DataViewCacheModel>>();
        // 调整大小之前的行
        private List<DataGridViewRow> sizeRow = new List<DataGridViewRow>();
        // 调整大小之前的列
        private List<DataGridViewColumn>  sizeColumn = new List<DataGridViewColumn>();
        // 单元格调整大小时是否和上一次调整大小属于同一操作
        private bool sizeIsJoin = false;
        // 单元格修改内容时是否和上一次修改属于同一操作
        private bool editIsJoin = false;
        /// <summary>
        /// 数据表格的配置
        /// </summary>
        private void dataViewConfig() {
            // 边框
            数据表格.BorderStyle = BorderStyle.None;
            数据表格.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            数据表格.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            数据表格.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            数据表格.SelectionMode = DataGridViewSelectionMode.CellSelect;
            数据表格.TabStop = false;
            数据表格.StandardTab = false;
            数据表格.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            数据表格.AllowUserToAddRows = false;
            数据表格.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            数据表格.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;

            // 列头的高
            数据表格.ColumnHeadersHeight = colHeadersHeight;
            // 默认单元格样式背景色
            数据表格.RowsDefaultCellStyle.BackColor = Color.White;
            // 默认单元格样式前景色
            数据表格.RowsDefaultCellStyle.ForeColor = Color.Black;
            // 默认单元格样式选中背景色
            数据表格.RowsDefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#0078D7");
            // 默认单元格样式选中前景色
            数据表格.RowsDefaultCellStyle.SelectionForeColor = Color.White;
            // 默认选中单元格的列标题背景色
            数据表格.ColumnHeadersDefaultCellStyle.SelectionBackColor = selCellHeadBack;
            // 默认选中单元格的列标题前景色
            数据表格.ColumnHeadersDefaultCellStyle.SelectionForeColor = selCellHeadFontC;
            // 默认选中单元格的行标题背景色
            数据表格.RowHeadersDefaultCellStyle.SelectionBackColor = selCellHeadBack;
            // 默认选中单元格的行标题前景色
            数据表格.RowHeadersDefaultCellStyle.SelectionForeColor = selCellHeadFontC;

            // 绑定单元格鼠标按下事件
            数据表格.CellMouseDown += 数据表格_CellMouseDown;
            // 绑定单元格进入编辑模式事件
            数据表格.EditingControlShowing += 数据表格_EditingControlShowing;
            // 绑定单元格离开编辑模式事件
            数据表格.CellEndEdit += 数据表格_CellEndEdit;
            // 绑定离开编辑模式单元格内容改变事件
            数据表格.CellParsing +=  数据表格_CellParsing;
            // 绑定单元格内容更改事件
            数据表格.CellValueChanged += 数据表格_CellValueChanged;
            // 绑定单元格鼠标移入事件
            数据表格.CellMouseEnter += 数据表格_CellMouseEnter;
            // 绑定列宽调整事件
            数据表格.ColumnWidthChanged += 数据表格_ColumnWidthChanged;
            // 绑定行高调整事件
            数据表格.RowHeightChanged += 数据表格_RowHeightChanged;
            // 绑定添加行事件
            数据表格.RowsAdded += 数据表格_RowsAdded;
            // 绑定添加列事件
            数据表格.ColumnAdded += 数据表格_ColumnAdded;
            // 绑定右键菜单并使用自定义的样式
            数据表格.ContextMenuStrip = table_rightStrip;
            table_rightStrip.Renderer = new MyToolStripRenderer();
        }


        /// <summary>
        /// 赋值点击的单元格的行与列
        /// </summary>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        private void setClickCellIndex(int colIndex, int rowIndex) { 
            clickColIndex = colIndex;
            clickRowIndex = rowIndex;
        }
        /// <summary>
        /// 赋值鼠标当前的单元格的行与列
        /// </summary>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        private void setMouseCellIndex(int colIndex, int rowIndex) { 
            mouseColIndex = colIndex;
            mouseRowIndex = rowIndex;
        }
        /// <summary>
        /// 判断要调整大小的行与列
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        private void isSizeRowColumn(int rowIndex, int colIndex) {
            if(rowIndex > -1) {
                if(rowIndex - 1 > -1) sizeRow.Add(数据表格.Rows[rowIndex - 1]);
                if(rowIndex + 1 < 数据表格.Rows.Count) sizeRow.Add(数据表格.Rows[rowIndex + 1]);
                sizeRow.Add(数据表格.Rows[rowIndex]);
            }
            if(colIndex > -1) { 
                if(colIndex - 1 > -1) sizeColumn.Add(数据表格.Columns[colIndex - 1]);
                if(colIndex + 1 < 数据表格.Columns.Count) sizeColumn.Add(数据表格.Columns[colIndex + 1]);
                sizeColumn.Add(数据表格.Columns[colIndex]);
            }
        }
        /// <summary>
        /// 修改单元格内容时放入缓存区
        /// </summary>
        private void setCacheByEdit(DataGridViewCell cell) {
            EditMode editMode = null;
            if(cell != null) {
                Console.WriteLine(editIsJoin);
                editMode = new EditMode();
                editMode.ColumnIndex = cell.ColumnIndex;
                editMode.RowIndex = cell.RowIndex;
                editMode.CellValue = cell.Value != null? cell.Value.ToString() : null;
                editMode.IsJoin = editIsJoin;
            }
            DataViewCache.addCacheFactory(cacheFactory, DataCacheTypeEnum.修改内容, null , editMode , 数据表格);
            
        }

        /// <summary>
        /// 调整大小时放入撤销缓存区
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void setCacheBySize(DataGridViewRow row, DataGridViewColumn column) {
            AdjustSizeMode sizeMode = new AdjustSizeMode();
            
            sizeMode.ColumnIndex = column!= null? column.Index : -1;
            sizeMode.RowIndex = row!= null? row.Index : -1;

            if(sizeRow.Count > 0 && row!= null) { 
                sizeMode.BeforeHeight = sizeRow.FirstOrDefault(s=>s.Index.Equals(row.Index)).Height;
            } else { 
                sizeMode.BeforeHeight = -1;
            }
            if(sizeColumn.Count > 0 && column!= null) { 
                sizeMode.BeforeWidth = sizeColumn.FirstOrDefault(s=>s.Index.Equals(column.Index)).Width;
            } else { 
                sizeMode.BeforeWidth = -1;
            }

            sizeMode.RearHeight = row!= null? row.Height : -1;
            sizeMode.RearWidth = column!= null? column.Width : -1;
            sizeMode.IsJoin = sizeIsJoin;

            DataViewCache.addCacheFactory(cacheFactory, DataCacheTypeEnum.调整大小, sizeMode,null,数据表格);
            
        }

        // 单元格鼠标按下事件
        private void 数据表格_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridView dataView = (DataGridView)sender;
            setClickCellIndex(e.ColumnIndex, e.RowIndex);
            // 当要调整列宽或行高时不选中行
            if(!数据表格.Cursor.Equals(Cursors.SizeWE) && !数据表格.Cursor.Equals(Cursors.SizeNS)
                && e.Button.Equals(MouseButtons.Left)) {
                // 全选该列或行
                DataGridViewUtilMet.selectAllCellBySingle(数据表格, e.ColumnIndex, e.RowIndex, true, true);
            }
            if(MouseButtons.Left.Equals(e.Button)  && (Cursors.SizeNS.Equals(数据表格.Cursor) 
                || Cursors.SizeWE.Equals(数据表格.Cursor)) ) { 
                isSizeRowColumn(e.RowIndex , e.ColumnIndex);
            }
        }
        // 进入编辑模式时发生
        private void 数据表格_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            DataGridView view = (DataGridView)sender;
        }
        // 离开编辑模式事件
        private void 数据表格_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            DataGridView view = (DataGridView)sender;
        }
        // 离开编辑模式并改变了内容事件
        private void 数据表格_CellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            DataGridView view = (DataGridView)sender;
            // 设置背景色
            // e.InheritedCellStyle.BackColor = ColorTranslator.FromHtml("#DB9C9E");
        }
        // 单元格内容发生改变时发生
        private void 数据表格_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            DataGridView view = (DataGridView)sender;
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0) {
                // 改变提示文本
                DataGridViewCell cell = view.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = cell.Value == null ? null : cell.Value.ToString();
                // 记录到缓冲区
                if(数据表格.Focused) {
                    setCacheByEdit(cell);
                }
                
            }
        }
        // 鼠标进入单元格事件
        private void 数据表格_CellMouseEnter(object sender, DataGridViewCellEventArgs e) {
            DataGridView data = (DataGridView)sender;
            if(e.ColumnIndex != -1 && e.RowIndex != -1) { 
                mouseCell = data.Rows[e.RowIndex].Cells[e.ColumnIndex];

            }else if(e.RowIndex == -1 && e.ColumnIndex != -1) {
                mouseCell = data.Columns[e.ColumnIndex].HeaderCell;

            }else if(e.RowIndex != -1 && e.ColumnIndex == -1) {
                mouseCell = data.Rows[e.RowIndex].HeaderCell;
            }
        }
        // 键盘按下事件
        private void 数据表格_KeyDown(object sender, KeyEventArgs e) {
            // ctrl+ c
            if(e.Control && Keys.C.Equals(e.KeyCode)) {
                DataGridViewUtilMet.copySelectCellText(数据表格);
            }
            // ctrl+ v
            if(e.Control && Keys.V.Equals(e.KeyCode)) {
                editIsJoin = true;
                DataGridViewUtilMet.pasteTextToSelCell(数据表格);
                editIsJoin = false;
            }
            // ctrl+ z
            if(e.Control && Keys.Z.Equals(e.KeyCode)) { 
                Console.WriteLine("");
            }
            // ctrl+ y
            if(e.Control && Keys.Y.Equals(e.KeyCode)) { 
                
            }
            // del
            if(Keys.Delete.Equals(e.KeyCode)) { 
                DataGridViewUtilMet.delSelectCellText(数据表格);
            }
        }
        // 添加列事件
        private void 数据表格_ColumnAdded(object sender, DataGridViewColumnEventArgs e) {
            
        }
        // 添加行事件
        private void 数据表格_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            
        }
        // 行高调整事件
        private void 数据表格_RowHeightChanged(object sender, DataGridViewRowEventArgs e) {
            if(数据表格.Focused) {
                // 放入撤销缓存区
                setCacheBySize(e.Row, null);
            }
        }
            
        // 列宽调整事件
        private void 数据表格_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e) {
            if(数据表格.Focused) {
                // 放入撤销缓存区
                setCacheBySize(null, e.Column);
            }
            
        }
        /// <summary>
        /// 右键菜单鼠标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightStripMenuItem_MouseDown(object sender, MouseEventArgs e) {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if(复制全部Item.Equals(item)) { 
                Clipboard.SetDataObject(DataGridViewUtilMet.getDatatabelSelText(数据表格, true), true);
            }
            if(复制选中Item.Equals(item)) { 
                Clipboard.SetDataObject(DataGridViewUtilMet.getDatatabelSelText(数据表格, false), true);
            }
            if(导出到记事本Item.Equals(item)) { 
                DataGridViewUtilMet.exportNotepad(数据表格, true);
            }
            if(导出到Excel_Item.Equals(item)) { 
                DataGridViewUtilMet.exportExcel(数据表格, true);
            }
            if(选中此列Item.Equals(item)) {
                if(mouseCell != null) { 
                    // 全选某列
                    数据表格.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    数据表格.Columns[mouseCell.ColumnIndex].HeaderCell.SortGlyphDirection = SortOrder.None;

                    mouseCell.OwningColumn.Selected = true;
                    }
            }
            if(选中此行Item.Equals(item)) {
                if(mouseCell != null) { 
                    // 全选某行
                    数据表格.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    mouseCell.OwningRow.Selected = true;
                }
            }
        }
    }
}
