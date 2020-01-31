using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using static Core.StaticMethod.Method.Utils.EnumUtilsMet;

namespace UI.ComponentLibrary.ControlLibrary {
    /// <summary>
    /// 导出功能下拉框
    /// </summary>
    internal partial class ExportComBox : ComboBox {

        // <summary>
        /// 要显示的属性的key
        /// </summary>
        private const string COMBOX_NAME = "Name";
        /// <summary>
        /// 实际的值的key
        /// </summary>
        private const string COMBOX_VALUE = "Value";

        /// <summary>
        /// 导出到新标签
        /// </summary>
        private const string EXPORT_THIS_PAGE = "导出到当前标签";
        /// <summary>
        /// 导出到新标签
        /// </summary>
        private const string EXPORT_NEW_PAGE = "导出到新标签";
        /// <summary>
        /// 导出到记事本
        /// </summary>
        private const string EXPORT_NOTEBOOK_NAME = "导出到记事本";
        /// <summary>
        /// 导出到Excel
        /// </summary>
        private const string EXPORT_EXCEL_NAME = "导出到Excel";
        /// <summary>
        /// 导出到JAVA文件
        /// </summary>
        private const string EXPORT_JAVA_NAME = "导出到JAVA文件";

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
        public ExportComBox(ExportComBoxValEnum[] notHideCode) {
            InitializeComponent();
            setExportCombox(getDataSourcs(notHideCode));
        }

        /// <summary>
        /// 设置导出按钮
        /// </summary>
        private void setExportCombox(Dictionary<string, ExportComBoxValEnum> dataDic) {
            // 绑定数据源
            DataTable dt = new DataTable();
            DataColumn dc1 = new DataColumn("id");
            DataColumn dc2 = new DataColumn("name");
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            // 设置数据源
            DataRow row = null;
            foreach(KeyValuePair<string, ExportComBoxValEnum> kvp in dataDic) { 
                string key = kvp.Key.ToUpper();
                int val = int.Parse(GetDescription(kvp.Value));
                row = dt.NewRow();
                row["name"] = key;
                row["id"] = val;
                dt.Rows.Add(row);
            }
            this.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Width = 130;
            this.Height = 30;
            this.Font = new Font("微软雅黑", 9, FontStyle.Regular);
            this.ItemHeight = 25;

            // 绑定数据源
            this.DisplayMember = "name";
            this.ValueMember = "id";
            this.DataSource = dt;
        }
        /// <summary>
        /// 获取下拉菜单的数据源
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ExportComBoxValEnum> getDataSourcs(ExportComBoxValEnum[] notHideCode) { 
            Dictionary<string, ExportComBoxValEnum> dataDic = new Dictionary<string, ExportComBoxValEnum>();
            if(notHideCode == null) notHideCode = new ExportComBoxValEnum[]{};
            // 判断是否需要隐藏导出到新建标签
            if(!notHideCode.Contains(ExportComBoxValEnum.EXPORT_NEW_PAGE_VAL)) {
                dataDic.Add(EXPORT_NEW_PAGE, ExportComBoxValEnum.EXPORT_NEW_PAGE_VAL);
            } 
            // 判断是否需要隐藏导出到当前标签
            if(!notHideCode.Contains(ExportComBoxValEnum.EXPORT_THIS_PAGE_VAL)) { 
                dataDic.Add(EXPORT_THIS_PAGE, ExportComBoxValEnum.EXPORT_THIS_PAGE_VAL);
            } 
            // 判断是否需要隐藏导出记事本
            if(!notHideCode.Contains(ExportComBoxValEnum.EXPORT_NOTEBOOK_VAL)) { 
                dataDic.Add(EXPORT_NOTEBOOK_NAME, ExportComBoxValEnum.EXPORT_NOTEBOOK_VAL);
            }
            // 判断是否需要隐藏导出Excel
            if(!notHideCode.Contains(ExportComBoxValEnum.EXPORT_EXCEL_VAL)) { 
                dataDic.Add(EXPORT_EXCEL_NAME, ExportComBoxValEnum.EXPORT_EXCEL_VAL);
            }
            // 判断是否需要隐藏导出Excel
            if(!notHideCode.Contains(ExportComBoxValEnum.EXPORT_JAVA_VAL)) { 
                dataDic.Add(EXPORT_JAVA_NAME, ExportComBoxValEnum.EXPORT_JAVA_VAL);
            }
            return dataDic;
        }
        /// <summary>
        /// 通过字符串值获取对应的枚举
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ExportComBoxValEnum stringToEnum(string val) { 
            ExportComBoxValEnum retEnum = ExportComBoxValEnum.Empty;
            foreach (ExportComBoxValEnum item in Enum.GetValues(typeof(ExportComBoxValEnum))) {
                if (val != null && val.Equals(GetDescription(item))) { 
                    retEnum = item;
                }
            }
            return retEnum;
        }
    }
    /// <summary>
    /// 导出控件的val枚举类
    /// </summary>
    public enum ExportComBoxValEnum{
        /// <summary>
        /// 导出到文本框的val码
        /// </summary>
        [EnumDescription("0")] EXPORT_THIS_PAGE_VAL, 
        /// <summary>
        /// 导出到文本框的val码
        /// </summary>
        [EnumDescription("1")] EXPORT_NEW_PAGE_VAL,
        /// <summary>
        /// 导出到记事本的val码
        /// </summary>
        [EnumDescription("2")] EXPORT_NOTEBOOK_VAL,
        /// <summary>
        /// 导出到Excel的val码
        /// </summary>
        [EnumDescription("3")] EXPORT_EXCEL_VAL,
        /// <summary>
        /// 导出到JAVA文件的val码
        /// </summary>
        [EnumDescription("4")] EXPORT_JAVA_VAL,
        /// <summary>
        /// 空
        /// </summary>
        [EnumDescription("")] Empty,
    }
}
