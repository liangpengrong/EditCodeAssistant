using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PubCacheArea {
    /// <summary>
    /// 选项卡缓存对象
    /// </summary>
    public class TablePageModel {
        private int maxPageIndex = 0;
        private int pageIndex = 0;
        private int pageCount = 0;

        /// <summary>
        /// 选项卡最大索引
        /// </summary>
        public int MaxPageIndex { get => maxPageIndex; set => maxPageIndex = value; }
        /// <summary>
        /// 当前选项卡索引
        /// </summary>
        public int PageIndex { get => pageIndex; set => pageIndex = value; }
        /// <summary>
        /// 一共多少选项卡
        /// </summary>
        public int PageCount { get => pageCount; set => pageCount = value; }
    }
}
