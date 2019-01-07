using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CacheFactory {
    public class DataViewCache {
        /// <summary>
        /// 存放缓存的缓存工厂
        /// </summary>
        private static Dictionary<string, List<DataViewCacheModel>> cacheFactory = new Dictionary<string, List<DataViewCacheModel>>();
        /// <summary>
        /// 当前缓存索引
        /// </summary>
        private static Dictionary<string, int> cacheIndexs = new Dictionary<string, int>();

        /// <summary>
        /// 将缓存追加到缓存工厂中
        /// </summary>
        /// <param name="cacheFactory">缓存工厂</param>
        /// <param name="type">缓存类型</param>
        /// <param name="adjustSize">调整大小</param>
        /// <param name="editMode">修改内容</param>
        /// <param name="gridView">DataGridView</param>
        public static void addCacheFactory(DataCacheTypeEnum type, AdjustSizeMode adjustSize,
            EditMode editMode, bool isCancel, bool isRestore, DataGridView gridView) {
            string key = gridView.Name;
            if(isCancel != true && isRestore != true) { 
                // 刷新缓存索引
                refreshCacheIndex(cacheFactory, key);
                if(cacheFactory.ContainsKey(key)) {
                    addCacheHaveKey(cacheFactory, type, adjustSize, editMode, gridView);
                } else {
                    addCacheNotKey(cacheFactory, type, adjustSize, editMode, gridView);
                }
                // 同步缓存索引
                synchronizeIndex(cacheFactory, key);
            }
        }
        /// <summary>
        /// 移除缓存工厂中对应的缓存
        /// </summary>
        public static void removeCacheFactory(string key) {
            if(cacheFactory.ContainsKey(key)) { 
                cacheFactory.Remove(key);
            }

        }
        /// <summary>
        /// 撤销缓存区域
        /// </summary>
        /// <param name="dataGrid">要撤销的表格</param>
        public static void cancelCache(DataGridView dataGrid) {
            string key = dataGrid != null?dataGrid.Name:"";
            int index = -1;
            List<DataViewCacheModel> keyList = null;
            DataViewCacheModel keyMode = null;
            if(cacheIndexs.Count > 0 && cacheFactory.Count > 0 && cacheFactory.ContainsKey(key)) {
                keyList = cacheFactory[key];
                index = cacheIndexs[key];
                if(index >= 0) { 
                    keyMode = keyList[index];
                    // 修改内容
                    if(DataCacheTypeEnum.修改内容.Equals(keyMode.Type)) { 
                        修改内容(dataGrid, keyMode, index);
                    }
                    // 调整大小
                    if(DataCacheTypeEnum.调整大小.Equals(keyMode.Type)) {
                        调整大小(dataGrid, keyMode, index);
                    }
                    index -- ;
                    cacheIndexs[key] = index;
                }
            }
        }
        /// <summary>
        /// 撤销时类型为调整大小时的执行方法
        /// </summary>
        private static void 调整大小(DataGridView dataGrid, DataViewCacheModel keyMode, int index) {
            // 初始化参数
            string key = dataGrid != null?dataGrid.Name:"";
            List<AdjustSizeMode> sizeList = null;
            // 索引
            int dataIndex = -1;
            if(index >= 0 && keyMode != null) { 
                sizeList = keyMode.Size;
                foreach(AdjustSizeMode model in sizeList) { 
                    dataIndex = model.Index;
                    // 为行
                    if(0.Equals(model.SizeType)) {
                        dataGrid.Rows[dataIndex].Height = model.BeforeSize.Height;
                    }else if(1.Equals(model.SizeType)) { // 为列
                        dataGrid.Columns[dataIndex].Width = model.BeforeSize.Width;
                    } 
                }
            }
        }
        /// <summary>
        /// 撤销时类型为修改内容时的执行方法
        /// </summary>
        private static void 修改内容(DataGridView dataGrid, DataViewCacheModel keyMode, int index) { 
            // 初始化参数
            string key = dataGrid != null?dataGrid.Name:"";
            List<EditMode> editList = null;
            if(index >= 0 && keyMode != null) { 
                editList = keyMode.EditCells;
                foreach(EditMode model in editList) {
                    if(model != null) { 
                        dataGrid.Rows[model.RowIndex].Cells[model.ColumnIndex].Value = model.BeforeText;
                    }
                }
            }
        }
        /// <summary>
        /// 刷新缓存索引
        /// </summary>
        private static void refreshCacheIndex(Dictionary<string, List<DataViewCacheModel>> cacheFactory, string key) { 
            int index = -1;
            List<DataViewCacheModel> modelList = null;
            if(cacheFactory.ContainsKey(key) && cacheIndexs.ContainsKey(key)) {
                index = cacheIndexs[key];
                modelList = cacheFactory[key];
                // 移除缓存工厂中指定索引后的全部对象
                if(modelList != null && index < modelList.Count-1 && index >= 0) {
                    int count = modelList.Count-1;
                    for(;count>index;count--) { 
                        modelList.RemoveAt(count);
                    }
                    // 将移除后的list赋值给缓存工厂
                    cacheFactory[key] = modelList;
                }
            }
            
        }
        /// <summary>
        /// 同步缓存索引
        /// </summary>
        private static void synchronizeIndex(Dictionary<string, List<DataViewCacheModel>> cacheFactory, string key) {
            int index = -1;
            
            if(cacheFactory.ContainsKey(key)) {
                if(cacheIndexs.ContainsKey(key)) {
                    index = cacheIndexs[key];
                    index = cacheFactory[key].Count-1;
                    cacheIndexs[key] = index;
                } else { 
                    cacheIndexs.Add(key, cacheFactory[key].Count-1);
                }
            }
        }
        /// <summary>
        /// 在不存在key的时候将缓存追加到缓存工厂中
        /// </summary>
        /// <param name="cacheFactory">缓存工厂</param>
        /// <param name="type">缓存类型</param>
        /// <param name="adjustSize">调整大小</param>
        /// <param name="editMode">修改内容</param>
        /// <param name="gridView">DataGridView</param>
        private static void addCacheNotKey(Dictionary<string, List<DataViewCacheModel>> cacheFactory, DataCacheTypeEnum type
            , AdjustSizeMode adjustSize, EditMode editMode, DataGridView gridView){ 
            try {
                List<DataViewCacheModel> listCacheM = null;
                List<AdjustSizeMode> listSize = null;
                List<EditMode> listEditM = null;
                List<SelectCellMode> listSeletM = null;
                SelectCellMode selectMode = null;
                DataViewCacheModel model = null;
                string key = gridView.Name;

                // 初始化缓存实体类
                model = new DataViewCacheModel();
                // 类型
                model.Type = type;
                // 总行与总列
                model.ColumnCount = gridView.ColumnCount;
                model.RowsCount = gridView.RowCount;

                // 选中单元格
                listSeletM = new List<SelectCellMode>();
                foreach(DataGridViewCell selcell in gridView.SelectedCells) { 
                    selectMode = new SelectCellMode();
                    selectMode.CellSize = selcell.Size;
                    selectMode.CellValue =  selcell.Value.ToString();
                    selectMode.ColumnIndex = selcell.ColumnIndex;
                    selectMode.RowIndex = selcell.RowIndex;
                    listSeletM.Add(selectMode);
                }
                model.SelectCell = listSeletM;

                // 调整大小
                listSize = new List<AdjustSizeMode>();
                listSize.Add(adjustSize);
                model.Size = listSize;

                // 修改单元格内容
                listEditM = new List<EditMode>();
                listEditM.Add(editMode);
                model.EditCells = listEditM;

                // 将实体类装入List
                listCacheM = new List<DataViewCacheModel>();
                listCacheM.Add(model);
                cacheFactory.Add(key, listCacheM);
            } catch { 
                throw new Exception();    
            }
        }
        /// <summary>
        /// 在存在key的时候将缓存追加到缓存工厂中
        /// </summary>
        /// <param name="cacheFactory">缓存工厂</param>
        /// <param name="type">缓存类型</param>
        /// <param name="adjustSize">调整大小</param>
        /// <param name="editMode">修改内容</param>
        /// <param name="gridView">DataGridView</param>
        private static void addCacheHaveKey(Dictionary<string, List<DataViewCacheModel>> cacheFactory, DataCacheTypeEnum type
            , AdjustSizeMode adjustSize, EditMode editMode, DataGridView gridView){
            string key = gridView.Name;
            List<DataViewCacheModel> listCacheM = cacheFactory[key];
            List<AdjustSizeMode> listSize = null;
            List<EditMode> listEditM  = null;
            List<SelectCellMode> listSeletM = null;
            SelectCellMode selectMode = null;
            DataViewCacheModel model = null;

            // // 初始化缓存实体类
            model = new DataViewCacheModel();
            // 类型
            model.Type = type;
            // 总行与总列
            model.ColumnCount = gridView.ColumnCount;
            model.RowsCount = gridView.RowCount;

            // 选中单元格
            listSeletM = new List<SelectCellMode>();
            foreach(DataGridViewCell selcell in gridView.SelectedCells) { 
                selectMode = new SelectCellMode();
                selectMode.CellSize = selcell.Size;
                selectMode.CellValue =  selcell.Value.ToString();
                selectMode.ColumnIndex = selcell.ColumnIndex;
                selectMode.RowIndex = selcell.RowIndex;
                listSeletM.Add(selectMode);
            }
            model.SelectCell = listSeletM;

            // 调整大小
            if(adjustSize != null && adjustSize.IsJoin && listCacheM.Count > 0) {
                listSize = listCacheM[listCacheM.Count - 1].Size;
            } else { 
                listSize = new List<AdjustSizeMode>();
            }
            listSize.Add(adjustSize);
            model.Size = listSize;

            // 修改单元格内容
            if(editMode != null && editMode.IsJoin && listCacheM.Count > 0) {
                listEditM = listCacheM[listCacheM.Count - 1].EditCells;
            } else { 
                listEditM = new List<EditMode>();
            }
            listEditM.Add(editMode);
            model.EditCells = listEditM;


            if( (DataCacheTypeEnum.调整大小.Equals(type) && adjustSize != null && adjustSize.IsJoin) 
                || (DataCacheTypeEnum.修改内容.Equals(type) && editMode != null && editMode.IsJoin) ) {
                listCacheM[listCacheM.Count - 1] = model;
            } else { 
                listCacheM.Add(model);
            }
            cacheFactory[key] = listCacheM;

        }
    }
}
