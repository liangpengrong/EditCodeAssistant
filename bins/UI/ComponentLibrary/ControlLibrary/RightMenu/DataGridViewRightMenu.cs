using Core.CacheLibrary.ControlCache;
using Core.ComponentlRedraw;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Inter;
using Core.StaticMethod.Method.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UI.ComponentLibrary.MethodLibrary.Interface;
using UI.ComponentLibrary.MethodLibrary.Util;

namespace UI.ComponentLibrary.ControlLibrary.RightMenu {
    internal partial class DataGridViewRightMenu : Component ,MenuItemAopInter, IComponentInitMode<Control> {
        private DataGridView gridView;
        internal DataGridViewRightMenu() {
            InitializeComponent();
            // 加载右键菜单配置
            menuDefaultConfig();
        }
        /// <summary>
        /// 打开单例模式下的对象
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            ContextMenuStrip conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.DATA_VIEW_RIGHT_MENU);
            if(con == null || !(con is ContextMenuStrip)) {
                conThis = table_rightStrip;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.DATA_VIEW_RIGHT_MENU);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (ContextMenuStrip)con;
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
            ContextMenuStrip conThis = table_rightStrip;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.DATA_VIEW_RIGHT_MENU)+DateTime.Now.Ticks.ToString();;
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.DATA_VIEW_RIGHT_MENU, conThis);
            return conThis;
        }
        /// <summary>
        /// 右键菜单的默认配置
        /// </summary>
        private void menuDefaultConfig() {
            table_rightStrip.Name = EnumUtils.GetDescription(DefaultNameEnum.DATA_VIEW_RIGHT_MENU);
            // 使用自定义的样式
            table_rightStrip.Renderer = new RightStripRenderer();
            // 设置不具有Tab焦点
            table_rightStrip.TabStop = false;
            // 字体
            table_rightStrip.Font = new Font("微软雅黑",9,FontStyle.Regular);
            //不显示图像边距
            table_rightStrip.ShowImageMargin = true;
            //不显示选中边距
            table_rightStrip.ShowCheckMargin = false;
            //显示信息提示
            table_rightStrip.ShowItemToolTips = true;
            foreach (ToolStripMenuItem tool in table_rightStrip.Items.OfType<ToolStripMenuItem>())
            {//遍历右键菜单下所有的一级ToolStripMenuItem选项
                ToolStripUtils.doIsDownItemAop(tool, this);
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
                Clipboard.SetDataObject(DataGridViewUtilMet.getDatatabelSelText(gridView, true), true);
            }
            if(复制选中Item.Equals(item)) { 
                Clipboard.SetDataObject(DataGridViewUtilMet.getDatatabelSelText(gridView, false), true);
            }
            if(导出到记事本Item.Equals(item)) { 
                DataGridViewUtilMet.exportNotepad(gridView, true);
            }
            if(导出到Excel_Item.Equals(item)) { 
                DataGridViewUtilMet.exportExcel(gridView, true);
            }

            if (选中此列Item.Equals(item)) {
                if(gridView != null) {
                    DataGridViewSelectedCellCollection selCells = gridView.SelectedCells;
                    foreach(DataGridViewCell cell in selCells) { 
                        DataGridViewUtilMet.selectAllCellBySingle(gridView,cell.ColumnIndex,-1,true,false);
                    }
                    
                }
            }
            if (选中此行Item.Equals(item)) {
                if(gridView != null) {
                    DataGridViewSelectedCellCollection selCells = gridView.SelectedCells;
                    foreach(DataGridViewCell cell in selCells) { 
                        DataGridViewUtilMet.selectAllCellBySingle(gridView,-1,cell.RowIndex,true,false);
                    }
                }
            }
            if(同步选中单元格_该列Item.Equals(item)) { 
                DataGridViewUtilMet.cellsDataCopyToRowOrCol(gridView, 1);
            }
            if(同步选中单元格_该行Item.Equals(item)) { 
                DataGridViewUtilMet.cellsDataCopyToRowOrCol(gridView, 0);
            }
            if(同步选中单元格_行和列Item.Equals(item)) { 
                DataGridViewUtilMet.cellsDataCopyToRowOrCol(gridView, 2);
            }
        }

        public void haveDownItem(ToolStripMenuItem tool) {
            
        }

        public void noDownItem(ToolStripMenuItem tool) {
            tool.MouseDown += new MouseEventHandler(rightStripMenuItem_MouseDown);
        }

        public void allItem(ToolStripMenuItem tool) {
            
        }
        /// <summary>
        /// 获取右键菜单
        /// </summary>
        /// <returns></returns>
        public static ContextMenuStrip getTextRightMenu() { 
            DataGridViewRightMenu dataGridViewRightMenu = new DataGridViewRightMenu();
            ControlCacheFactory.addSingletonCache(dataGridViewRightMenu.table_rightStrip);
            return dataGridViewRightMenu.table_rightStrip;
        }
        /// <summary>
        /// 获取单例的右键菜单
        /// </summary>
        /// <returns></returns>
        public static ContextMenuStrip getSingleTextRightMenu() { 
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.DATA_VIEW_RIGHT_MENU);
            if(con == null) {
                DataGridViewRightMenu dataGridViewRightMenu = new DataGridViewRightMenu();
                ControlCacheFactory.addSingletonCache(dataGridViewRightMenu.table_rightStrip);
                return dataGridViewRightMenu.table_rightStrip;
            } else { 
                return (ContextMenuStrip)con;
            }
        }
        /// <summary>
        /// 为该右键菜单绑定表格
        /// </summary>
        /// <param name="t"></param>
        public static void bindingDataGridView(DataGridView gridView) {
            if(gridView != null) {
                gridView.ContextMenuStrip = getSingleTextRightMenu();
            }
        }
        // 右键菜单弹出事件
        private void table_rightStrip_Opening(object sender, CancelEventArgs e) {
            //将右键菜单的源控件赋值给全局变量SourceControl
            Control obj = ((ContextMenuStrip)sender).SourceControl;
            if(obj is DataGridView) { 
                gridView = (DataGridView)obj;
            }
        }
    }
}
