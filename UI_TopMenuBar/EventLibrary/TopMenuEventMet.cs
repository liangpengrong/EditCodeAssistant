using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;
using Core.StaticMethod.Method.Utils;
using Ui.ControlEventLibrary;
using Core.CacheLibrary.ControlCache;
using Core.CacheLibrary.OperateCache.TextBoxOperateCache;
using Core.DefaultData.DataLibrary;

namespace UI_TopMenuBar.TopMenuEvent
{
    /// <summary>
    /// TopMenu控件的委托类
    /// </summary>
    public class TopMenuEventMet
    {
        private TopMenuEventMet(){}
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <returns></returns>
        public static object exitProgram(Dictionary<Type , object> data){ 
            FormUtislMet.dropOut();
            return null;
        }
        /// <summary>
        /// 实例化文本选择对话框
        /// </summary>
        /// <returns>返回该对话框</returns>
        public static object openFileMethod(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            PublicEventMet.openFileMethod(t);
            return null;
        }
        /// <summary>
        /// 撤销缓存区文本
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        public static object cancelTextBoxCache (Dictionary<Type, object> data){
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 将文本框置于撤销状态
            TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_IS_CANCEL, true);
            TextBoxCache.cancelCache(t);
            return null;
        }
        /// <summary>
        /// 恢复缓存区文本
        /// </summary>
        /// <param name="t"></param>
        public static object restoreTextBoxCache(Dictionary<Type, object> data){
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 将文本框置于恢复状态
            TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_IS_RESTORE, true);
            TextBoxCache.restoreCache(t);
            return null;
        }
        /// <summary>
        /// 实例化文件保存对话框保存文本
        /// </summary>
        /// <param name="t">要保存内容的文本框</param>
        /// <returns></returns>
        public static object saveFileMethod(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            PublicEventMet.saveFileMethod(t);
            return null;
        }
        /// <summary>
        /// 判断文件应该是保存还是另存为
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object saveOrSaveas(Dictionary<Type , object> data)
        {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 定义初始化的路径
            object path = null;
            TextBoxUtilsMet.getDicTextTag(t).TryGetValue(TextBoxTagKey.SAVE_FILE_PATH, out path);//赋值路径
            if (path!=null&&FileUtilsMet.isFileUrl(path.ToString()))//判断路径是否存在
            {
                if (path.ToString().Split('.')[path.ToString().Split('.').Length - 1].ToLower().Equals("txt"))
                {//判断文件后缀名是否为txt格式
                    if (MessageBox.Show(
                         "该文件为本地文件,确定要保存并覆盖吗？"
                         + System.Environment.NewLine
                         + "文件路径为：" + path
                        , "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        FileUtilsMet.FileWrite.writeFile(t.Tag.ToString()//调用方法写入文件内容
                            , t.Text//获取选中标签中的主文本框的值
                            , Encoding.Default);
                    }
                } else {
                    MessageBox.Show("只保存TXT格式的文件");
                }
            }
            else{
                saveFileMethod(data);
            }
            return null;
        }
        /// <summary>
        /// 实例化字体对话框
        /// </summary>
        /// <returns></returns>
        public static object fontDialogMethod(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            PublicEventMet.fontDialogMethod(t);
            return null;    
        }
                /// <summary>
        /// 实现用记事本打开文本框内容
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object notepadOpenFile(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            FileUtilsMet.turnOnNotepad(t.Text);
            return null;
        }
        /// <summary>
        /// 设置是否自动换行
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object isAutoLine(Dictionary<Type , object> data) { 
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            ToolStripMenuItem item = (ToolStripMenuItem)data[typeof(ToolStripMenuItem)];
            // 设置状态栏显示与隐藏
            bool check = item.Checked;
            t.WordWrap = check;
            return null;
        }
        /// <summary>
        /// 设置状态栏的显示与隐藏
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object isStartBarDisplay(Dictionary<Type , object> data) {
            // 获取文本框
            TextBox t = (TextBox)data[typeof(TextBox)];
            ToolStripMenuItem item = (ToolStripMenuItem)data[typeof(ToolStripMenuItem)];
            // 全局单例控件工厂
            Dictionary<string, Control> single = ControlCache.getSingletonCache();
            if(single.ContainsKey(DefaultNameCof.TOOL_START) && single.ContainsKey(DefaultNameCof.TAB_CONTENT)) { 
                // 状态栏
                Control toolStrip = single[DefaultNameCof.TOOL_START];
                // 标签容器的父容器
                Control tabParent = single[DefaultNameCof.MAIN_CONTAINER];
                // 设置状态栏显示与隐藏
                bool check = item.Checked;
                toolStrip.Visible = check;
                // 调整标签容器的位置
                if(check) { 
                    tabParent.Height = tabParent.Height - toolStrip.Height;
                } else {
                    tabParent.Height = tabParent.Height + toolStrip.Height;
                }
            }
            return null;
        }
        
    }
}
