using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ui.ControlEventLibrary.TextBoxEvent {
    internal enum TextBoxEventTypeEnum {
        鼠标按下事件 = 0,
        获取焦点事件 = 1,
        失去焦点事件 = 2,
        内容改变事件 = 3,
        鼠标移过事件 = 4,
        鼠标松开事件 = 5,
        键盘按下事件 = 6,
        键盘松开事件 = 7,
        控件启用事件 = 8,
        鼠标移入事件 = 9,
        文件拖放完成事件 = 10
        
    }
}
