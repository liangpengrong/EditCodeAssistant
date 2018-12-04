using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using PubMethodLibrary;

namespace PubControlLibrary {
    public partial class MyDataTable : Component{
        public MyDataTable() {
            // 初始化控件
            InitializeComponent();
            // 加载数据表格配置
            dataViewConfig();
        }
        /// <summary>
        /// 单元格默认宽度
        /// </summary>
        public int cellDefWidth = 100;
        /// <summary>
        /// 单元格默认高度
        /// </summary>
        public int cellDefHeight = 30;
        /// <summary>
        /// 列标题的默认高度
        /// </summary>
        public int colHeadersHeight = 25;
        
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
        /// 数据表格的配置
        /// </summary>
        private void dataViewConfig() {
            // 边框
            数据表格.BorderStyle = BorderStyle.FixedSingle;
            数据表格.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            数据表格.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            数据表格.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            数据表格.SelectionMode = DataGridViewSelectionMode.CellSelect;
            数据表格.TabStop = false;
            数据表格.StandardTab = false;
            数据表格.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
            数据表格.AllowUserToAddRows = false;



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

            // 绑定单元格鼠标移过事件
            数据表格.CellMouseMove += 数据表格_CellMouseMove;
            // 绑定单元格鼠标按下事件
            数据表格.CellMouseDown += 数据表格_CellMouseDown;
            // 绑定单元格进入编辑模式事件
            数据表格.EditingControlShowing += 数据表格_EditingControlShowing;
            // 绑定离开编辑模式单元格内容改变事件
            数据表格.CellParsing +=  数据表格_CellParsing;
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
        /// 根据传入的二维数组获取列的最大宽度
        /// </summary>
        /// <param name="rowColuArr"></param>
        /// <returns></returns>
        private int getMaxColumnWidth(String[][] rowColuArr) { 
            List<int> intList = new List<int>();
            foreach(String[] strArr1 in rowColuArr) {
                foreach(String str1 in strArr1) { 
                   intList.Add((int)this.数据表格.CreateGraphics().MeasureString(str1 , 数据表格.Font).Width);
                }
            }
            return intList.Max()+10;
        }
        /// <summary>
        /// 单元格选中后改变对应的行,列头颜色
        /// </summary>
        private void selectCallHeandColor(int colIndex, int rowIndex){
            // 默认列标题背景色
            Color colHeadBack = 数据表格.ColumnHeadersDefaultCellStyle.BackColor;
            // 默认列标题前景色
            Color colHeadColor = 数据表格.ColumnHeadersDefaultCellStyle.ForeColor;
            // 默认列标题字体
            Font colHeadFont = 数据表格.ColumnHeadersDefaultCellStyle.Font;

            // 默认行标题背景色
            Color rowHeadBack = 数据表格.RowHeadersDefaultCellStyle.BackColor;
            // 默认行标题前景色
            Color rowHeadColor = 数据表格.RowHeadersDefaultCellStyle.ForeColor;
            // 默认行标题字体
            Font rowHeadFont = 数据表格.RowHeadersDefaultCellStyle.Font;

            // 开辟新线程执行方法
            ControlsUtilsMet.timersEventMet(数据表格, 1, new EventHandler(delegate {
                //获取该单元格
                DataGridViewCell cell = 数据表格.Rows[rowIndex].Cells[colIndex];
                // 判断该列索引不为-1
                if(colIndex != -1) { 
                    // 改变列标题的样式
                    // 判断当前单元格是否选中
                    if(cell.Selected) { 
                        // 改变样式
                        DataGridViewCellStyle cellSty = 数据表格.RowsDefaultCellStyle;
                        // 设置选中列头和行头的样式
                        数据表格.Columns[colIndex].HeaderCell.Style.BackColor = selCellHeadBack;
                        数据表格.Columns[colIndex].HeaderCell.Style.ForeColor = selCellHeadFontC;
                        // 文本加粗
                        数据表格.Columns[colIndex].HeaderCell.Style.Font 
                        = new Font(colHeadFont, FontStyle.Bold);
                    } else {
                        // 恢复默认
                        数据表格.Columns[colIndex].HeaderCell.Style.BackColor = colHeadBack;
                        数据表格.Columns[colIndex].HeaderCell.Style.ForeColor = colHeadColor;
                        数据表格.Columns[colIndex].HeaderCell.Style.Font = colHeadFont;
                    }
                }
                
                // 改变行标题的样式
                // 判断该行索引不为-1
                if(rowIndex != -1) {
                    if (cell.Selected) {
                        // 单元格样式
                        DataGridViewCellStyle cellSty = 数据表格.RowsDefaultCellStyle;
                        // 设置选中列头和行头的样式
                        数据表格.Rows[rowIndex].HeaderCell.Style.BackColor = selCellHeadBack;
                        数据表格.Rows[rowIndex].HeaderCell.Style.ForeColor = selCellHeadFontC;
                        // 文本加粗
                        数据表格.Rows[rowIndex].HeaderCell.Style.Font 
                        = new Font(colHeadFont, FontStyle.Bold);
                    } else {
                        数据表格.Rows[rowIndex].HeaderCell.Style.BackColor = colHeadBack;
                        数据表格.Rows[rowIndex].HeaderCell.Style.ForeColor = colHeadColor;
                        数据表格.Rows[rowIndex].HeaderCell.Style.Font = rowHeadFont;
                    }
                }
            }));
        }
        /// <summary>
        /// 全选或反选某单列或某单行,为-1表示不选中
        /// </summary>
        /// <param name="colInxe">列索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="isClearSel">是否清楚清楚之前的单元格选中</param>
        private void selectAllCellBySingle(int colInxe, int rowIndex, Boolean isSelet, Boolean isClearSel){
            // 是否需要清楚之前的选中
            if(isClearSel) 数据表格.ClearSelection();
            if(colInxe >= 0) {
                // 全选某列
                数据表格.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                数据表格.Columns[colInxe].HeaderCell.SortGlyphDirection = SortOrder.None;
                数据表格.Columns[colInxe].Selected = isSelet;
            }
            if(rowIndex >= 0) { 
                // 全选某行
                数据表格.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                数据表格.Rows[rowIndex].Selected = isSelet;
            }
        }
        
        /// <summary>
        /// 是否禁止用户调整大小
        /// </summary>
        /// <param name="boo"></param>
        private void isNoResizing(Boolean boo) { 
            数据表格.AllowUserToResizeColumns = !boo;
            数据表格.AllowUserToResizeRows = !boo;
        }

        // 鼠标移过单元格事件
        private void 数据表格_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e) {

            DataGridView dataView = (DataGridView)sender;
            // 判断该行或该列是否全部选中
            // isRowOrColSelAll(e.ColumnIndex, e.RowIndex, true);
            if (e.Button.Equals(MouseButtons.Left)) { 
                // 判断行标题与列标题的颜色
              //  selectCallHeandColor(e.ColumnIndex, e.RowIndex);
            }

        }
        
        // 单元格鼠标按下事件
        private void 数据表格_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e) {
            DataGridView dataView = (DataGridView)sender;
            setClickCellIndex(e.ColumnIndex, e.RowIndex);
            // 当要调整列宽或行高时不选中行
            if(!数据表格.Cursor.Equals(Cursors.SizeWE) && !数据表格.Cursor.Equals(Cursors.SizeNS)
                && e.Button.Equals(MouseButtons.Left)) {
                // 全选该列或行
                selectAllCellBySingle(e.ColumnIndex, e.RowIndex, true, true);
            }
            // 判断行标题与列标题的颜色
           // selectCallHeandColor(e.ColumnIndex, e.RowIndex);
        }
        // 单元格内容发生改变时发生
        private void 数据表格_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) {
            // 背景色
            e.CellStyle.BackColor = ColorTranslator.FromHtml("#DB9C9E");
        }
        // 离开编辑模式事件
        private void 数据表格_CellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            DataGridView view = (DataGridView)sender;
            view.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor 
                = ColorTranslator.FromHtml("#DB9C9E");
        }
    }
}
