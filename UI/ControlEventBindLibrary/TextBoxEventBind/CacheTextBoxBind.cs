using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Ui.ControlEventLibrary.TextBoxEvent;
using Core.StaticMethod.Method.Utils;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using UI.ComponentLibrary.ControlLibrary.RightMenu;

namespace UI.ControlEventBindLibrary.TextBoxEventBind
{
    internal class CacheTextBoxBind
    {
        /// <summary>
        /// 文本框文本改变事件
        /// </summary>
        internal void mainTextBoxChanged(object sender, EventArgs e){
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.内容改变事件, textBox);
        }

        /// <summary>
        /// 文本框鼠标移过事件
        /// </summary>
        internal void mainTextBoxMouseMove(object sender, MouseEventArgs e){
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.鼠标移过事件, textBox);
        }
        /// <summary>
        /// 文本框的鼠标在组件内并释放鼠标按键事件
        /// </summary>
        internal void mainTextBoxMouseUp(object sender, MouseEventArgs e){
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.鼠标按下事件, textBox);
        }
        /// <summary>
        /// 文本框的鼠标按下事件
        /// </summary>
        internal void mainTextBoxMouseDown(object sender, MouseEventArgs e){
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.鼠标按下事件, textBox);
        }
        /// <summary>
        /// 文本框的鼠标移入事件
        /// </summary>
        internal void mainTextBoxMouseEnter(object sender, EventArgs e) {
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.鼠标移入事件, textBox);
        }
        /// <summary>
        /// 文本框启用事件
        /// </summary>
        internal void mainTextBoxEnter(object sender, EventArgs e){
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.控件启用事件, textBox);
        }
        /// <summary>
        /// 文本框获得焦点事件
        /// </summary>
        internal void mainTextBoxGotFocus(object sender, EventArgs e) {
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.获取焦点事件, textBox);
        }
        /// <summary>
        /// 文本框的键盘按下事件
        /// </summary>
        internal void mainTextBoxKeyDown(object sender, KeyEventArgs e) {
            TextBox textBox = (TextBox) sender;
            setEventBindMethod(TextBoxEventTypeEnum.键盘按下事件, textBox);
            /*============绑定文本框按键按下事件执行方法===================*/
            textBoxkeyDownBinding(e, textBox);
            
        }
        /// <summary>
        /// 文本框的键盘松开事件
        /// </summary>
        internal void mainTextBoxKeyUp(object sender, KeyEventArgs e) {
            TextBox textBox = (TextBox)sender;
            setEventBindMethod(TextBoxEventTypeEnum.键盘松开事件, textBox);
            /*============绑定文本框按键松开事件执行方法===================*/
            textBoxkeyUpBinding(e, textBox);
            
        }
        /// <summary>
        /// 文本框键盘按下事件绑定
        /// </summary>
        private void textBoxkeyDownBinding(KeyEventArgs e,TextBox t) {
            try {
                // 全选
                if (e.Control && e.KeyCode.Equals(Keys.A)) {
                    TextBoxUtilsMet.textAllSelect(t);
                }
                //// 查找和替换
                //if (e.Control && e.KeyCode.Equals(Keys.F)) {
                //    InitSingleForm.initFindAndReplace(t, true);
                //}
            } catch(Exception exc) {
                Console.WriteLine(exc.StackTrace);
            }
            
        }

        /// <summary>
        /// 文本框松开事件绑定
        /// </summary>
        private void textBoxkeyUpBinding(KeyEventArgs e, TextBox t) {
            try {
                
            } catch(Exception exc) {
                Console.WriteLine(exc.StackTrace);
            }
            
        }
        /// <summary>
        /// 文本框事件类型对应的执行方法
        /// </summary>
        private void setEventBindMethod(TextBoxEventTypeEnum eventType, TextBox textBox) { 
            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), textBox);
            // 菜单项事件
            // OnTopMenuEvent.setOnTopMenuEventBind(eventType, textBox);
            switch(eventType) { 
                case TextBoxEventTypeEnum.内容改变事件 : 
                    /*============将文本数据放入缓冲区===================*/
                    MainTextBoxEventMet.setTextBoxCache(data);
                break;
                case TextBoxEventTypeEnum.鼠标移入事件 :
                    /*============绑定右键菜单===================*/
                    TextRightMenu.bindingTextBox(textBox);
                break;
                case TextBoxEventTypeEnum.控件启用事件 :
                    /*============记录到获取焦点文本框缓存中===================*/
                    TextBoxCache.saveFocusTextBox(textBox);
                break;
            }    
        }
    }
}
