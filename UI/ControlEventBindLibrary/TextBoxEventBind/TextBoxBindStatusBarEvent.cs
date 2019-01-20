using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ui.ControlEventLibrary.TextBoxEvent;
using UI.ControlEventLibrary.StatusBarEvent.TextStatusBarEvent;

namespace UI.ControlEventBindLibrary.TextBoxEventBind {
    internal class TextBoxBindStatusBarEvent {
        /// <summary>
        /// 关于状态栏的事件绑定
        /// </summary>
        internal static void setOnStatusBarEventBind(TextBoxEventTypeEnum eventType, TextBox textBox) { 
            StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.TOOL_START) ?
            (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.TOOL_START]:null;

            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), textBox);
            data.Add(typeof(StatusStrip), toolStrip);
            switch(eventType) { 
                case TextBoxEventTypeEnum.内容改变事件 : 
                    /*============赋值给状态栏总行数与字符数===================*/
                    TextStatusBarEventMet.setRowChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                break;
                case TextBoxEventTypeEnum.鼠标移过事件 :
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                break;
                case TextBoxEventTypeEnum.鼠标松开事件 :
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                break;
                case TextBoxEventTypeEnum.鼠标按下事件 :
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                break;
                case TextBoxEventTypeEnum.获取焦点事件 :
                    /*============将文本框中的文件路径放到父容器的Text中===================*/
                    MainTextBoxEventMet.setParentTextByFileName(data);
                    /*============设置文本框中的编码到状态栏中===================*/
                    MainTextBoxEventMet.setToolSatrtEcoding(data);
                    /*============赋值给状态栏总行数与字符数===================*/
                    TextStatusBarEventMet.setRowChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                break;
                case TextBoxEventTypeEnum.键盘按下事件 :
                        /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                break;
                case TextBoxEventTypeEnum.键盘松开事件 : 
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                    /*============将大小写状态赋值给状态栏===================*/
                    TextStatusBarEventMet.setCaseKey(data);
                break;
            }
        }
    }
}
