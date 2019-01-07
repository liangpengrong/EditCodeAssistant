using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacheFactory {
    /// <summary>
    /// 数据表格缓存类型枚举
    /// </summary>
    public enum DataCacheTypeEnum {
        插入 = 0,
        删除 = 1,
        调整大小 = 2,
        修改内容 = 3,
    }
}
