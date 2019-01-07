using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CacheFactory {
    /// <summary>
    /// 数据表格缓存对象
    /// </summary>
    public class DataViewCacheModel {
        /// <summary>
        /// 缓存的类型
        /// </summary>
        public DataCacheTypeEnum Type { get; set; }
        /// <summary>
        /// 调整大小
        /// </summary>
        public List<AdjustSizeMode> Size { get; set; }
        /// <summary>
        /// 修改单元格内容
        /// </summary>
        public List<EditMode> EditCells { get; set; }
        /// <summary>
        /// 选中的单元格的索引
        /// </summary>
        public List<SelectCellMode> SelectCell { get; set; }
        /// <summary>
        /// 总的行数
        /// </summary>
        public int RowsCount { get; set; }
        /// <summary>
        /// 总的列数
        /// </summary>
        public int ColumnCount { get; set; }

    }
    /// <summary>
    /// 调整大小时的对象
    /// </summary>
    public class AdjustSizeMode {
        /// <summary>
        /// 调整大小的类型(0-行 1-列)
        /// </summary>
        public int SizeType { get; set; }
        /// <summary>
        /// 调整之前的大小
        /// </summary>
        public Size BeforeSize { get; set; }
        /// <summary>
        /// 调整之后的大小
        /// </summary>
        public Size EndSize { get; set; }
        /// <summary>
        /// 调整的索引
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 是否连接上一个
        /// </summary>
        public bool IsJoin { get; set; }
    }
    /// <summary>
    /// 修改单元格内容对象
    /// </summary>
    public class EditMode { 
        /// <summary>
        /// 修改之前的单元格值
        /// </summary>
        public string BeforeText { get; set; }
        /// <summary>
        /// 修改之后的单元格值
        /// </summary>
        public string EndText { get; set; }
        /// <summary>
        /// 修改的行索引
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 修改的列索引
        /// </summary>
        public int ColumnIndex { get; set; }
        /// <summary>
        /// 是否连接上一个
        /// </summary>
        public bool IsJoin { get; set; }
    }
    /// <summary>
    /// 选中的单元格对象
    /// </summary>
    public class SelectCellMode{
        /// <summary>
        /// 行索引
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 列索引
        /// </summary>
        public int ColumnIndex { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string CellValue { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public Size CellSize { get; set; }
    }
}
