using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary.ControlLibrary;
using UI.TabContentLibrary.MainTabContent;

namespace UI.ComponentLibrary.ControlMethod {
    public static class DataGridViewUtilMet {
        /// <summary>
        /// 根据传入的二维数组获取列的最大宽度
        /// </summary>
        /// <param name="rowColuArr">填充到表格中的二维数组</param>
        /// <param name="view">表格</param>
        /// <returns></returns>
        public static int getMaxColumnWidth(string[][] rowColuArr, DataGridView view) { 
            List<int> intList = new List<int>();
            foreach(string[] strArr1 in rowColuArr) {
                foreach(string str1 in strArr1) { 
                   intList.Add((int) view.CreateGraphics().MeasureString(str1 , view.Font).Width);
                }
            }
            return intList.Max()+10;
        }
        /// <summary>
        /// 设置单元格对应的列的值
        /// </summary>
        /// <param name="view"></param>
        /// <param name="columnI"></param>
        /// <param name="val"></param>
        public static void setDataGridViewColunmVal(DataGridView view, int columnI, object[] vals) { 
            for(int i=0; i < view.Rows.Count; i++) {
                if(i <= vals.Length-1) { 
                    view.Rows[i].Cells[columnI].Value = vals[i];
                }
            }
        }
        /// <summary>
        /// 单元格选中后改变对应的行标题,列标题颜色
        /// </summary>
        /// <param name="colIndex">选中单元格列索引</param>
        /// <param name="rowIndex">选中单元格行索引</param>
        /// <param name="view">表格</param>
        /// <param name="back">背景色</param>
        /// <param name="fontC">前景色</param>
        public static void selectCallHeandColor(int colIndex, int rowIndex, DataGridView view, Color back, Color fontC){
            // 默认列标题背景色
            Color colHeadBack = view.ColumnHeadersDefaultCellStyle.BackColor;
            // 默认列标题前景色
            Color colHeadColor = view.ColumnHeadersDefaultCellStyle.ForeColor;
            // 默认列标题字体
            Font colHeadFont = view.ColumnHeadersDefaultCellStyle.Font;

            // 默认行标题背景色
            Color rowHeadBack = view.RowHeadersDefaultCellStyle.BackColor;
            // 默认行标题前景色
            Color rowHeadColor = view.RowHeadersDefaultCellStyle.ForeColor;
            // 默认行标题字体
            Font rowHeadFont = view.RowHeadersDefaultCellStyle.Font;

            // 开辟新线程执行方法
            ControlsUtilsMet.asynchronousMet(view, 1, new EventHandler(delegate {
                //获取该单元格
                DataGridViewCell cell = view.Rows[rowIndex].Cells[colIndex];
                // 判断该列索引不为-1
                if(colIndex != -1) { 
                    // 改变列标题的样式
                    // 判断当前单元格是否选中
                    if(cell.Selected) { 
                        // 改变样式
                        DataGridViewCellStyle cellSty = view.RowsDefaultCellStyle;
                        // 设置选中列头和行头的样式
                        view.Columns[colIndex].HeaderCell.Style.BackColor = back;
                        view.Columns[colIndex].HeaderCell.Style.ForeColor = fontC;
                        // 文本加粗
                        view.Columns[colIndex].HeaderCell.Style.Font 
                        = new Font(colHeadFont, FontStyle.Bold);
                    } else {
                        // 恢复默认
                        view.Columns[colIndex].HeaderCell.Style.BackColor = colHeadBack;
                        view.Columns[colIndex].HeaderCell.Style.ForeColor = colHeadColor;
                        view.Columns[colIndex].HeaderCell.Style.Font = colHeadFont;
                    }
                }
                
                // 改变行标题的样式
                // 判断该行索引不为-1
                if(rowIndex != -1) {
                    if (cell.Selected) {
                        // 单元格样式
                        DataGridViewCellStyle cellSty = view.RowsDefaultCellStyle;
                        // 设置选中列头和行头的样式
                        view.Rows[rowIndex].HeaderCell.Style.BackColor = back;
                        view.Rows[rowIndex].HeaderCell.Style.ForeColor = fontC;
                        // 文本加粗
                        view.Rows[rowIndex].HeaderCell.Style.Font 
                        = new Font(colHeadFont, FontStyle.Bold);
                    } else {
                        view.Rows[rowIndex].HeaderCell.Style.BackColor = colHeadBack;
                        view.Rows[rowIndex].HeaderCell.Style.ForeColor = colHeadColor;
                        view.Rows[rowIndex].HeaderCell.Style.Font = rowHeadFont;
                    }
                }
            }));
        }
        /// <summary>
        /// 全选或反选某单列或某单行,为-1表示不选中
        /// </summary>
        /// <param name="view">单元格</param>
        /// <param name="colInxe">列索引</param>
        /// <param name="rowIndex">行索引</param>
        /// <param name="isSelet">全选或反选</param>
        /// <param name="isClearSel">是否清除之前的单元格选中</param>
        public static void selectAllCellBySingle(DataGridView view, int colInxe, int rowIndex, bool isSelet, bool isClearSel){
            if(view != null) { 
                // 是否需要清楚之前的选中
                if(isClearSel) view.ClearSelection();
                // 是否需要清楚之前的选中
                if(isClearSel) view.ClearSelection();
                if(colInxe >= 0) {
                    // 全选某列
                    foreach(DataGridViewColumn column in view.Columns) { 
                        column.SortMode = DataGridViewColumnSortMode.NotSortable;
                    }
                    view.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    view.Columns[colInxe].HeaderCell.SortGlyphDirection = SortOrder.None;
                    view.Columns[colInxe].Selected = isSelet;
                }
                if(rowIndex >= 0) { 
                    // 全选某行
                    view.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
                    view.Rows[rowIndex].Selected = isSelet;
                }
            }
        }
        /// <summary>
        /// 复制选定单元格内容到剪贴板
        /// </summary>
        /// <param name="view">表格</param>
        public static void copySelectCellText(DataGridView view) {
            string text = view.GetClipboardContent().GetText();
            Clipboard.SetDataObject(text, true);
        }
        /// <summary>
        /// 粘贴内容到选定单元格
        /// </summary>
        /// <param name="view">表格</param>
        public static void pasteTextToSelCell(DataGridView view) {
            string text = Clipboard.GetText();
            DataGridViewSelectedCellCollection selCell =  view.SelectedCells;
            // 选中单元格的行与列索引
            int[][] rowsColns = getSelCellRowsColns(view);
            if(rowsColns.Length == 0 || selCell.Count == 0) return;
            int minRow = rowsColns[0].Min<int>();
            int minColn = rowsColns[1].Min<int>();

            // 判断剪贴板内容不为空并且选中单元格不为0
            if(text != null && text.Length > 0 && selCell.Count > 0) {
                string[][] rowColArr = StringUtilsMet.splitStrToArr(text, 
                    new string[]{Environment.NewLine},new string[]{"\t"},true,true,true,true);
                string[] endstrArr = rowColArr[rowColArr.Length - 1];
                // 去除末尾的空行
                if(1.Equals(endstrArr.Length) && 0.Equals(endstrArr[0].Length)) {
                    Array.Resize(ref rowColArr, rowColArr.Length - 1);
                }

                // 获取要填充数据的列的最大值
                int maxColu = rowColArr.Max(x=>x.Length);
                // 判断要粘贴的内容的行与列大于选中的单元格的行与列
                if(rowColArr.Length > rowsColns[0].Length || 
                    maxColu > rowsColns[1].Length) { 
                    // 弹出对话框
                    ControlsUtilsMet.showAskMessBox("当前选定单元格与粘贴板的内容不一致,是否继续",
                        "提示"
                    ,delegate{
                        // 判断列是否需要添加
                        if(view.Rows[0].Cells.Count - minColn < maxColu) {
                            int cellWidth = view.Rows[0].Cells[0].Size.Width;
                            // 添加列头并且返回添加的索引
                            for(int i =0, len = (maxColu - (view.Rows[0].Cells.Count - minColn));i<len;i++) { 
                                int index = view.Columns.Add("列" + (view.Columns.Count+1), "列" + (view.Columns.Count+1));
                                // 不包含排序
                                view.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                                // 设置列的宽度
                                view.Columns[index].Width = cellWidth;
                            }
                        }
                        // 判断行是否需要添加
                        if((view.Rows.Count - minRow) < rowColArr.Length) {
                            int rowCount = view.Rows.Count;
                            int addI = rowColArr.Length - (view.Rows.Count - minRow);
                            // 添加行
                            view.Rows.Add(addI);
                            //将序列赋值给行头
                            for(;rowCount < view.Rows.Count;rowCount++) { 
                                view.Rows[rowCount].HeaderCell.Value = (rowCount+1).ToString();
                            }
                        }
                        // 清除选定
                        view.ClearSelection();
                        // 填充数据
                        for (int i = 0, len = rowColArr.Length; i < len; i++) {
                            for (int j = 0, len2 = rowColArr[i].Length; j < len2; j++) {
                                view.Rows[minRow + i].Cells[minColn + j].Value = rowColArr[i][j];
                                view.Rows[minRow + i].Cells[minColn + j].Selected = true;
                            }
                        }
                    },null);
                } else {
                    // 判断要粘贴的内容为一列和一行
                    if(rowColArr.Length == 1 && maxColu == 1) {
                        // 改变所有选中单元格内容为该内容
                        foreach(DataGridViewCell cell in view.SelectedCells) { 
                            cell.Value = rowColArr[0][0];
                        }
                    } else {
                        // 将粘贴内容按照行列依次放入表格中
                        for(int i = 0, len=rowColArr.Length; i<len ;i++) {
                            for(int j = 0, len2=rowColArr[i].Length; j<len2; j++) { 
                                view.Rows[minRow+i].Cells[minColn+j].Value = rowColArr[i][j];
                                view.Rows[minRow+i].Cells[minColn+j].Selected = true;
                            }
                        }
                    }
                }

            }
            
        }
        /// <summary>
        /// 获取所选单元格的不重复的行与列
        /// </summary>
        /// <param name="view">表格</param>
        /// <returns>行与列的数组集合，0位为行1位为列</returns>
        public static int[][] getSelCellRowsColns(DataGridView view) {
            int[][] retints = new int[2][];
            List<int> rows = new List<int>();
            List<int> colns = new List<int>();
            DataGridViewSelectedCellCollection selCell =  view.SelectedCells;
            foreach(DataGridViewCell cell in selCell) {
                if( !rows.Contains(cell.RowIndex)) rows.Add(cell.RowIndex);
                if( !colns.Contains(cell.ColumnIndex)) colns.Add(cell.ColumnIndex);
            }
            retints[0] = rows.ToArray();
            retints[1] = colns.ToArray();
            return retints;
        }
        /// <summary>
        /// 删除选定单元格的内容
        /// </summary>
        public static void delSelectCellText(DataGridView view) { 
            DataGridViewSelectedCellCollection selCell =  view.SelectedCells;
            foreach(DataGridViewCell cell in selCell) { 
                cell.Value = null;
            } 
        }
        /// <summary>
        /// 将单元格的数据同步到该单元格所属的行或列
        /// </summary>
        /// <param name="cells">单元格集合</param>
        /// <param name="type">0-行 1-列 2全部</param>
        public static void cellsDataCopyToRowOrCol(DataGridViewCell[] cells, int type){
            if(cells != null && cells.Length > 0) { 
                DataGridView dataGrid = cells[0].DataGridView;
                List<int> columnIndexs = new List<int>();
                string val = null;
                object[] vals = null;
                int count = 0;
                // 同步行
                if(type == 2 || type == 0) {
                    // 遍历选中的单元格
                    foreach(DataGridViewCell cell in cells) {
                        val = cell.Value.ToString();
                        // 该单元格对应行的单元格数
                        count = cell.OwningRow.Cells.Count;
                        vals = new object[count];
                        for(int i=0; i<vals.Length; i++) { 
                            vals[i] = val;
                        }
                        cell.OwningRow.SetValues(vals);
                    }
                }
                // 同步列
                if(type == 2 || type == 1) {
                    vals = new object[dataGrid.RowCount];
                    foreach(DataGridViewCell cell in cells) {
                        if(!columnIndexs.Contains(cell.ColumnIndex)) { 
                            val = cell.Value.ToString();
                            for(int i = 0;i< dataGrid.RowCount;i++) { 
                                vals[i] = val;
                            }
                            // 设置该列的值
                            setDataGridViewColunmVal(dataGrid,cell.ColumnIndex,vals);
                            columnIndexs.Add(cell.ColumnIndex);
                        }
                    }
                }
                
            }
            
        }
        /// <summary>
        /// 将选中的单元格的数据同步到该单元格所属的行或列
        /// </summary>
        /// <param name="cells">单元格集合</param>
        /// <param name="type">0-行 1-列 2全部</param>
        public static void cellsDataCopyToRowOrCol(DataGridView dataGrid, int type){
            DataGridViewSelectedCellCollection selCells = dataGrid.SelectedCells;
            // 将选中单元格集合变为数组
            DataGridViewCell[] cells = new DataGridViewCell[selCells.Count];
            selCells.CopyTo(cells,0);
            // 执行同步
            cellsDataCopyToRowOrCol(cells, type);
        }
        /// <summary>
        /// 获取表格中选定单元格的数据
        /// </summary>
        /// <param name="isSelectAll">表格</param>
        /// <param name="isSelectAll">是否选中全部</param>
        /// <returns></returns>
        public static string getDatatabelSelText(DataGridView view, bool isSelectAll) {
            string tableText = "";
            if(view != null) { 
                // 选定的单元格集合
                DataGridViewSelectedCellCollection selCell = view.SelectedCells;
                if(isSelectAll) view.SelectAll();
                tableText = view.GetClipboardContent() != null? view.GetClipboardContent().GetText() : "";
                // 还原
                if(isSelectAll) { 
                    view.ClearSelection();
                    foreach(DataGridViewCell cell in selCell) { 
                        cell.Selected = true;
                    }
                }
            }
            return tableText;
        }
        /// <summary>
        /// 导出表格数据到新标签
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="excNoHaveTabs">不包含tab符号</param>
        public static void exportNewPage(DataGridView view, bool excNoHaveTabs) {
            // 将表格内容转化为字符串
            string s = getDatatabelSelText(view, false);
            // 判断是否包含制表符
            if(excNoHaveTabs) {s = s.Replace("\t", "");}
            MainTabContent.exportNewPage(s);
        }
        /// <summary>
        /// 导出表格数据到当前标签
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="excNoHaveTabs">不包含tab符号</param>
        public static void exportThisPage(DataGridView view, bool excNoHaveTabs) {
            // 将表格内容转化为字符串
            string s = getDatatabelSelText(view, false);
            // 判断是否包含制表符
            if(excNoHaveTabs) {s = s.Replace("\t", "");}
            ControlsUtilsMet.exportThisPage(s);
        }
        /// <summary>
        /// 导出表格数据到记事本
        /// </summary>
        /// <param name="view">表格</param>
        /// <param name="excNoHaveTabs">不包含tab符号</param>
        public static void exportNotepad(DataGridView view, bool excNoHaveTabs) {
            string s = getDatatabelSelText(view, false);
            // 不包含制表符
            if(excNoHaveTabs) {s = s.Replace("\t", "");}
            FileUtilsMet.turnOnNotepad(s);
        }
        /// <summary>
        /// 导出表格数据到Excel
        /// </summary>
        /// <param name="view"></param>
        public static void exportExcel(DataGridView view, bool excNoHaveTabs) { 
                
        }
    }
}
