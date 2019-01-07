using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using PublicMethodLibrary;

namespace ComponentLibrary {
    /// <summary>
    /// 导出功能下拉框
    /// </summary>
    public partial class ExportComBox : Component {

        // <summary>
        /// 要显示的属性的key
        /// </summary>
        public const string COMBOX_NAME = "Name";
        /// <summary>
        /// 实际的值的key
        /// </summary>
        public const string COMBOX_VALUE = "Value";

        /// <summary>
        /// 导出到文本框Name
        /// </summary>
        public const string EXPORT_TEXT_NAME = "导出到文本框";
        /// <summary>
        /// 导出到记事本Name
        /// </summary>
        public const string EXPORT_NOTEBOOK_NAME = "导出到记事本";
        /// <summary>
        /// 导出到ExcelName
        /// </summary>
        public const string EXPORT_EXCEL_NAME = "导出到Excel";

        /// <summary>
        /// 导出到文本框val码
        /// </summary>
        public const int EXPORT_TEXT_VAL = 0;
        /// <summary>
        /// 导出到记事本val码
        /// </summary>
        public const int EXPORT_NOTEBOOK_VAL = 1;
        /// <summary>
        /// 导出到Excelval码
        /// </summary>
        public const int EXPORT_EXCEL_VAL = 2;
        /// <summary>
        /// 无参构造器
        /// </summary>
        public ExportComBox() {
            InitializeComponent();
            setExportCombox(getDataSourcs(null));
        }
        /// <summary>
        /// 有参构造器
        /// </summary>
        /// <param name="notHideCode">需要隐藏的项的val码集合</param>
        public ExportComBox(int[] notHideCode) {
            InitializeComponent();
            setExportCombox(getDataSourcs(notHideCode));
        }

        /// <summary>
        /// 设置导出按钮
        /// </summary>
        private void setExportCombox(Dictionary<string, int> dataDic) {
            // 绑定数据源
            DataTable table = new DataTable();
            DataColumn column;
            DataRow row;
            column = new DataColumn(EXPORT_TEXT_NAME);
            table.Columns.Add(column);
            column = new DataColumn(EXPORT_NOTEBOOK_NAME);
            table.Columns.Add(column);
            // 设置数据源
            foreach(KeyValuePair<string, int> kvp in dataDic) { 
                string key = kvp.Key.ToUpper();
                int val = kvp.Value;
                row = table.NewRow();
                row[EXPORT_TEXT_NAME] = key;
                row[EXPORT_NOTEBOOK_NAME] = val;
                table.Rows.Add(row);
            }
            export_combox.Width = 110;
            export_combox.Height = 30;
            export_combox.Font = new Font("微软雅黑", 9, FontStyle.Regular);
            export_combox.ItemHeight = 25;

            // 绑定数据源
            export_combox.DisplayMember = EXPORT_TEXT_NAME;
            export_combox.ValueMember = EXPORT_NOTEBOOK_NAME;
            export_combox.DataSource = table;
        }
        /// <summary>
        /// 获取下拉菜单的数据源
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, int> getDataSourcs(int[] notHideCode) { 
            Dictionary<string, int> dataDic = new Dictionary<string, int>();
            if(notHideCode == null) notHideCode = new int[]{-1};
            // 判断是否需要隐藏导出文本框
            if(!notHideCode.Contains(EXPORT_TEXT_VAL)) { 
                dataDic.Add(EXPORT_TEXT_NAME, EXPORT_TEXT_VAL);
            } 
            // 判断是否需要隐藏导出记事本
            if(!notHideCode.Contains(EXPORT_NOTEBOOK_VAL)) { 
                dataDic.Add(EXPORT_NOTEBOOK_NAME, EXPORT_NOTEBOOK_VAL);
            }
            // 判断是否需要隐藏导出Excel
            if(!notHideCode.Contains(EXPORT_EXCEL_VAL)) { 
                dataDic.Add(EXPORT_EXCEL_NAME, EXPORT_EXCEL_VAL);
            }
            return dataDic;
        }
    }
}
