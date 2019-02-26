using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Core.StaticMethod.Method.Utils;
using Core.DefaultData.DataLibrary;

namespace Ui.ControlEventLibrary {
   /// <summary>
   /// 公共的事件绑定方法
   /// </summary>
   public class PublicEventMet {
        private PublicEventMet() { }
        /// <summary>
        /// 实例化文本选择对话框
        /// </summary>
        /// <returns>返回该对话框</returns>
        public static object openFileMethod(TextBox t)
        {
            Encoding encoding = Encoding.Default;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.CheckFileExists = true;
            openFile.DefaultExt = "txt";
            openFile.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFile.DereferenceLinks = true;
            openFile.Multiselect = true;
            openFile.RestoreDirectory = false;
            if (openFile.ShowDialog() == DialogResult.OK)
            {//判断是否点击确定
                // 判断编码
                //string[] pathArr = FileUtilsMet.getPathArr(openFile.FileName);
                //if(!"txt".Equals(pathArr[2].ToLower())) { 
                //    encoding = FileUtilsMet.isFileEncoding(openFile.FileName);
                //}
                // 将文件内容赋值到文本框中
                t.Text = FileUtilsMet.FileRead.read(openFile.FileName, encoding);
                t.SelectionStart = t.TextLength;

                // 将文件路径写入到文本框tag数据中
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.SAVE_FILE_PATH, openFile.FileName);
                // 将文件编码写入到文本框tag数据中
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, encoding);
                // 监听文件变化并弹窗提醒
                PublicEventMet.monitorFileShowMess(openFile.FileName, t);
                t.Focus();
            }
            return openFile;
        }

        /// <summary>
        /// 实例化文件保存对话框保存文本
        /// </summary>
        /// <param name="t">要保存内容的文本框</param>
        /// <returns></returns>
        public static object saveFileMethod(TextBox t)
        {
            SaveFileDialog newSaveFile = new SaveFileDialog();
            newSaveFile.RestoreDirectory = false;
            newSaveFile.ValidateNames = true;
            newSaveFile.DefaultExt = "txt";
            newSaveFile.Filter = "文本文档(*.txt)|*.txt|所有文件(*.*)|*.*";
            //判断是否点击确定
            if (newSaveFile.ShowDialog() == DialogResult.OK) {
                // 调用方法写入文件内容
                FileUtilsMet.FileWrite.writeFile(newSaveFile.FileName.ToString(), t.Text, Encoding.Default);
                // 将保存路径加入到文本框的Tag属性
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.SAVE_FILE_PATH , newSaveFile.FileName.ToString());
                // 监听文件变化并弹窗提醒
                monitorFileShowMess(newSaveFile.FileName, t);
            }
            return newSaveFile;
        }
        /// <summary>
        /// 实例化字体对话框
        /// </summary>
        /// <returns></returns>
        public static object fontDialogMethod(TextBox t) {
            Dictionary<string, object> retDic = new Dictionary<string, object>();
            FontDialog fontD = new FontDialog();
            fontD.AllowSimulations = true;
            fontD.AllowVectorFonts = true;
            fontD.AllowScriptChange = true;
            fontD.ScriptsOnly = true;
            fontD.Font = t.Font;
            fontD.ShowApply = true;
            fontD.ShowEffects = false;
            if(!retDic.ContainsKey("1")) retDic.Add("1", fontD);
            // 判断是否点击了应用
            fontD.Apply += (object sender, EventArgs e) =>{
                t.Font = fontD.Font;
                if(!retDic.ContainsKey("2")) retDic.Add("2", DialogResult.OK);
                
            };
            DialogResult dialogResult = fontD.ShowDialog();
            // 判断是否点击确定
            if(dialogResult == DialogResult.OK) {
                t.Font = fontD.Font;
                if(!retDic.ContainsKey("2"))  retDic.Add("2", DialogResult.OK);
            }
            return retDic;    
        }
        /// <summary>
        /// 监听文件变化并弹出提示框提示重新加载或另存为
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="t"></param>
        public static void monitorFileShowMess(string filepath, TextBox t) {
            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), t);

            // 监听文件变化
            try { 
                FileSystemWatcher wat = null;
                string[] pathArr = FileUtilsMet.getPathArr(filepath);
                Encoding encoding = Encoding.Default;
                if(!"txt".Equals(pathArr[2].ToLower())) {
                    encoding = FileUtilsMet.isFileEncoding(filepath);
                }
                // 判断文本框的Tag中是否纯在一个监听,存在就销毁他
                if(TextBoxUtilsMet.getDicTextTag(t).ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR)){
                    Object obj = TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR];
                    if(obj.GetType().Equals(typeof(FileSystemWatcher))) { 
                        wat =(FileSystemWatcher) TextBoxUtilsMet.getDicTextTag(t)[TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR];
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
                                    string text = FileUtilsMet.FileRead.read(filepath, encoding);
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
                                    saveFileMethod(t);
                                }));
                            }    
                        },null); 

                });
                // 加入到文本框的tag数据中
                TextBoxUtilsMet.textAddTag(t, TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR, wat);
            } catch {
                MessageBox.Show("监听文件状态时发生异常");
            }
        }
   }
}
