using PubCacheArea;
using PubControlLibrary;
using PubMethodLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StaticDataLibrary;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace CharsToolset {
   /// <summary>
   /// 公共的事件绑定方法
   /// </summary>
   public class PublicEventMet {
        private PublicEventMet() { }

        /// <summary>
        /// 撤销缓存区文本
        /// </summary>
        /// <param name="t"></param>
        /// <param name="keys"></param>
        public static void cancelTextBoxCache (Dictionary<Type, object> data){
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 将文本框置于撤销状态
            TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.textIsCancel, true);
            TextBoxCache.cancelCache(t);
        }
        /// <summary>
        /// 恢复缓存区文本
        /// </summary>
        /// <param name="t"></param>
        public static void restoreTextBoxCache(Dictionary<Type, object> data){
            TextBox t = (TextBox)data[typeof(TextBox)];
            // 将文本框置于恢复状态
            TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.textIsRestore, true);
            TextBoxCache.restoreCache(t);
        }
        /// <summary>
        /// 打开查找和替换窗口
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// </summary>
        /// <returns></returns>
        public static FindAndReplace openFindAndReplace(TextBox t, bool isShow)
        {
            
            FindAndReplace findAndReplace = new FindAndReplace(t, t.FindForm());
            try {
                // 判断单例工厂中是否不存在该窗体
                if (!FormCache.getSingletonFactory().ContainsKey(findAndReplace.Name)) { 
                    if(isShow) findAndReplace.Show();
                    // 添加到缓存工厂中
                    FormCache.addSingletonFac(findAndReplace);
                    return findAndReplace;
                } 
                // 如果存在判断是否为null
                if (FormCache.getSingletonFactory()[findAndReplace.Name] == null) { 
                    if(isShow) findAndReplace.Show();
                    // 添加到缓存工厂中
                    FormCache.addSingletonFac(findAndReplace);
                    return findAndReplace;
                }
                findAndReplace = (FindAndReplace)FormCache.getSingletonFactory()[findAndReplace.Name];
                findAndReplace.Activate();
            } catch {  
                
            }
            return findAndReplace;
        }
        /// <summary>
        /// 打开分列窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static SplitCharsForm openSplitCharsForm(TextBox t, bool isShow)
        {
            SplitCharsForm splitChars = new SplitCharsForm(t);
            try {
                // 判断单例工厂中是否存在该窗体
                if (!FormCache.getSingletonFactory().ContainsKey(splitChars.Name)) { 
                    if(isShow) splitChars.Show();
                    // 添加到缓存工厂中
                    FormCache.addSingletonFac(splitChars);
                    return splitChars;
                } 
                // 如果存在判断是否为null
                if (FormCache.getSingletonFactory()[splitChars.Name] == null) { 
                    if(isShow) splitChars.Show();
                    // 添加到缓存工厂中
                    FormCache.addSingletonFac(splitChars);
                    return splitChars;
                }
                splitChars = (SplitCharsForm)FormCache.getSingletonFactory()[splitChars.Name];
                splitChars.Activate();
            } catch {  
                
            }
            return splitChars;
        }
        /// <summary>
        /// 打开转到行窗体
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static RowGoToForm openRowGoToForm(TextBox t, bool isShow)
        {
            RowGoToForm rowGoToForm = new RowGoToForm(t);
            if(isShow) rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
        /// <summary>
        /// 打开统计字符窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static CharsStatistics openCharsStatistics(TextBox t, bool isShow)
        {
            CharsStatistics rowGoToForm = new CharsStatistics(t);
            if(isShow) rowGoToForm.ShowDialog();
            return rowGoToForm;
        }
        /// <summary>
        /// 打开设置文本框编码窗口
        /// </summary>
        /// <param name="t">所需文本框</param>
        /// <param name="isShow">是否显示窗体</param>
        /// <returns></returns>
        public static SetCodingForm openSetCodingForm(TextBox t, bool isShow){
            SetCodingForm setCodingForm = new SetCodingForm(t);
            if(isShow) setCodingForm.ShowDialog();
            return setCodingForm;
        }
        /// <summary>
        /// 监听文件变化并弹出提示框提示重新加载或另存为
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="t"></param>
        public static void monitorFileShowMess(String filepath, TextBox t) {
            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), t);

            // 监听文件变化
            try { 
                FileSystemWatcher wat = null;
                String[] pathArr = FileUtilsMet.getPathArr(filepath);
                Encoding encoding = Encoding.Default;
                if(!"txt".Equals(pathArr[2].ToLower())) {
                    encoding = FileUtilsMet.isFileEncoding(filepath);
                }
                // 判断文本框的Tag中是否纯在一个监听,存在就销毁他
                if(TextBoxUtilsMet.getDicTextTag(t).ContainsKey(TextBoxTagKey.fileMonitor)){
                    Object obj = TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.fileMonitor];
                    if(obj.GetType().Equals(typeof(FileSystemWatcher))) { 
                        wat =(FileSystemWatcher) TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.fileMonitor];
                        wat.EnableRaisingEvents = false;
                        wat.Dispose();
                    }
                }
                
                // 获取一个新的文件监听
                wat = FileUtilsMet.fileMonitor(pathArr[0], pathArr[1]+"."+pathArr[2],
                    NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Size
                    ,null,null
                    ,delegate(object sender, FileSystemEventArgs e){
                        FileSystemWatcher watcher = (FileSystemWatcher)sender;
                        // 闪烁窗体
                        Form f = t.FindForm();
                        if(f.InvokeRequired) { 
                            f.Invoke(new EventHandler(delegate{ 
                                WinApiUtilsMet.flashWindesTime(f.Handle, 400, 3, true);
                            }));
                        }
                        
                        // 弹出对话框
                        ControlsUtilsMet.showAskMessBox("文件内容已经更改,是否要重新加载文件", "提示"
                        ,delegate{
                            if (t.InvokeRequired) {
                                t.Invoke(new EventHandler(delegate {
                                    // 获取内容
                                    String text = FileUtilsMet.FileRead.read(filepath, encoding);
                                    t.Text = text;
                                }));
                            }    
                        },null);
                        watcher.EnableRaisingEvents = false;
                    }
                    , delegate{ 
                        // 弹出对话框
                        ControlsUtilsMet.showAskMessBox("文件在磁盘上已经被删除或重命名, 是否立刻另存为", "提示"
                        ,delegate{
                            if (t.InvokeRequired) {
                                t.Invoke(new EventHandler(delegate {
                                    // 调用另存为方法
                                    TopMenuEventMet.saveFileMethod(data);
                                }));
                            }    
                        },null); 

                });
                // 加入到文本框的tag数据中
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.fileMonitor, wat);
            } catch {
                MessageBox.Show("监听文件状态时发生异常");
            }
        }
   }
}
