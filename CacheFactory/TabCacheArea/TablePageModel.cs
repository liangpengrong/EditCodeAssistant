using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheFactory {
    /// <summary>
    /// 选项卡缓存对象
    /// </summary>
    public class TablePageModel {
        /// <summary>
        /// 选项卡最大索引
        /// </summary>
        public int MaxPageIndex { get; set; } = 0;
        /// <summary>
        /// 当前选项卡索引
        /// </summary>
        public int PageIndex { get; set; } = 0;
        /// <summary>
        /// 一共多少选项卡
        /// </summary>
        public int PageCount { get; set; } = 0;
    }
}
