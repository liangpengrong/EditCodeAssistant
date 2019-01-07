using System;
using System.Windows.Forms;
using PublicMethodLibrary;
using CacheFactory;
using StaticDataLibrary;
using System.Collections.Generic;
using ProgramStatusBar;
using SingleComponentFactory;

namespace SingleComponentLibrary
{
    public class TextBoxBind
    {

        /// <summary>
        /// 按下的键盘按键
        /// </summary>
        public KeyEventArgs downKeys = null;
        /// <summary>
        /// 按下的鼠标按钮
        /// </summary>
        public MouseEventArgs downMouseB = null;
        /// <summary>
        /// 文本框文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxChanged(object sender, EventArgs e)
        {
            try {
                TextBox t = (TextBox)sender;
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);
                data.Add(typeof(KeyEventArgs), downKeys);
                data.Add(typeof(MouseEventArgs), downMouseB);

                /*============赋值给状态栏总行数与字符数===================*/
                TextStatusBarEventMet.setRowChars(data);
                /*============赋值给状态栏当前行列数===================*/
                TextStatusBarEventMet.setRowColumn(data);
                /*============文本框数据改变时放入到缓存区===================*/
                if(TextBoxEventMet.setTextBoxCache(data)){ 
                    downKeys = null; downMouseB = null;
                };
            } catch (Exception){ 
                throw;
            }
        }

        /// <summary>
        /// 文本框鼠标移过事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxMouseMove(object sender, MouseEventArgs e)
        {
            try {
                TextBox t = (TextBox)sender;
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);
                // 判断是否按下了左键
                if(e.Button.Equals(MouseButtons.Left)){ 
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);
                    /*============赋值给状态栏当前行列数===================*/
                    TextStatusBarEventMet.setRowColumn(data);
                }
            } catch (Exception) {
                throw;
            }
            

        }
        /// <summary>
        /// 文本框的鼠标在组件内并释放按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxMouseUp(object sender, MouseEventArgs e)
        {
            try {
                TextBox t = (TextBox)sender;
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);

                /*============赋值给状态栏选中字符数===================*/
                TextStatusBarEventMet.setSelectChars(data);
            } catch (Exception) {

                throw;
            }
            
        }
        /// <summary>
        /// 文本框的鼠标按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxMouseDown(object sender, MouseEventArgs e)
        {
            try {
                // 设置按下的按钮
                setDownMoustB(e);

                TextBox t = (TextBox)sender;
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);

                /*============赋值给状态栏当前行列数===================*/
                TextStatusBarEventMet.setRowColumn(data);
                /*============赋值给状态栏选中字符数===================*/
                TextStatusBarEventMet.setSelectChars(data);

                //判断是否按下了左键
                if (e.Button.Equals(MouseButtons.Left)) {
                    /*============赋值给状态栏选中字符数===================*/
                    TextStatusBarEventMet.setSelectChars(data);

                }
            } catch (Exception) {

                throw;
            }
            
            
        }
        /// <summary>
        /// 文本框启用事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxEnter(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            /*============记录到获取焦点文本框缓存中===================*/
            TextBoxCache.saveFocusTextBox(t);
        }
        /// <summary>
        /// 文本框获得焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxGotFocus(object sender, EventArgs e) {
            TextBox t = (TextBox)sender;
            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), t);

            /*============将文本框中的文件路径放到父容器的Text中===================*/
            TextBoxEventMet.setParentTextByFileName(data);
            /*============设置文本框中的编码到状态栏中===================*/
            TextBoxEventMet.setToolSatrtEcoding(data);
        }
        /// <summary>
        /// 文本框的键盘按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxKeyDown(object sender, KeyEventArgs e) {
            try {
                TextBox t = (TextBox) sender;
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);

                /*============赋值给状态栏当前行列数===================*/
                TextStatusBarEventMet.setRowColumn(data);
                /*============绑定文本框按键按下事件执行方法===================*/
                textBoxkeyDownBinding(e, t);
            } catch (Exception) {

                throw;
            }
            
        }
        /// <summary>
        /// 文本框的键盘松开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mainTextBoxKeyUp(object sender, KeyEventArgs e) {
            try {
                TextBox t = (TextBox)sender;
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);

                /*============赋值给状态栏当前行列数===================*/
                TextStatusBarEventMet.setRowColumn(data);
                /*============赋值给状态栏选中字符数===================*/
                TextStatusBarEventMet.setSelectChars(data);
                textBoxkeyUpBinding(e, t);
            } catch (Exception) {

                throw;
            }
            
        }
        /// <summary>
        /// 文本框键盘按下事件绑定
        /// </summary>
        /// <param name="e"></param>
        /// <param name="t"></param>
        private void textBoxkeyDownBinding(KeyEventArgs e,TextBox t) {
            try {
                // 设置键盘按下的按键
                setDownKeys(e);
                // 全选
                if (e.Control && e.KeyCode.Equals(Keys.A)) {
                    TextBoxUtilsMet.textAllSelect(t);
                }
                // 查找和替换
                if (e.Control && e.KeyCode.Equals(Keys.F)) {
                    InitSingleForm.initFindAndReplace(t, true);
                }
            } catch (Exception) {

                throw;
            }
            
        }

        /// <summary>
        /// 文本框松开事件绑定
        /// </summary>
        /// <param name="e"></param>
        /// <param name="t"></param>
        private void textBoxkeyUpBinding(KeyEventArgs e, TextBox t) {
            try {
                StatusStrip toolStrip = ControlCache.getSingletonCache().ContainsKey(DefaultNameCof.toolStart) ?
                (StatusStrip)ControlCache.getSingletonCache()[DefaultNameCof.toolStart]:null;

                Dictionary<Type, object> data = new Dictionary<Type, object>();
                data.Add(typeof(TextBox), t);
                data.Add(typeof(StatusStrip), toolStrip);
                // 大写按键
                if (e.KeyCode.Equals(Keys.CapsLock)) {
                    TextStatusBarEventMet.setCaseKey(data);
                }
            } catch (Exception) {

                throw;
            }
            
        }
        /// <summary>
        /// 设置按下的键
        /// </summary>
        /// <param name="e"></param>
        private void setDownKeys(KeyEventArgs e){
            try {
                e.SuppressKeyPress = false;
                downKeys = e;
            } catch (Exception) {

                throw;
            }
            
        }
        /// <summary>
        /// 设置按下的鼠标按钮
        /// </summary>
        /// <param name="e"></param>
        private void setDownMoustB(MouseEventArgs e){
            try {
                 downMouseB = e;
            } catch (Exception) {

                throw;
            }
           
        }
    }
}
