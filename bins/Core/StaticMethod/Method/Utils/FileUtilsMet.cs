using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;
using System.Windows.Forms;
using Core.DefaultData.DataLibrary;

namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 关于文件操作的工具类
    /// </summary>
    public static class FileUtilsMet
    {
        private static FileStream fileStream = null;
        private static StreamWriter streamWriter = null;
        private static StreamReader streamReader = null;
        private static BinaryWriter binaryWriter = null;
        private static BinaryReader binaryReader = null;
        public static class FileRead {
            /// <summary>
            /// 按照指定的编码读取文件内容返回读取到的内容
            /// </summary>
            /// <param name="fileUrl">读取的文件路径</param>
            /// <param name="encoding">指定的编码</param>
            /// <returns></returns>
            public static string Read(string fileUrl, Encoding encoding)
            {
                string str = "";
                try {
                    // 判断文件是否为文本文件
                    bool isTextFile = CheckIsTextFile(fileUrl);
                    fileStream = new FileStream(fileUrl, FileMode.Open);
                    if(isTextFile) { 
                        streamReader = new StreamReader(fileStream, encoding);
                        str = streamReader.ReadToEnd();
                    } else { 
                        binaryReader = new BinaryReader(fileStream);
                        str = binaryReader.ReadString();
                    }
                    
                } catch (IOException e) {
                    Console.WriteLine(e.StackTrace);
                    throw e;
                } finally { 
                    if(streamReader != null) streamReader.Close();
                    if(fileStream != null) fileStream.Close();
                    if(binaryReader != null) binaryReader.Close();
                }
                return str;
            }
            /// <summary>
            /// 按照指定的编码读取文件内容按行返回读取到的内容
            /// </summary>
            /// <param name="fileUrl"></param>
            /// <param name="encoding"></param>
            /// <returns></returns>
            public static List<string> RowRead(string fileUrl, Encoding encoding) {
                List<string> rowList = new List<string>();
                try {
                    fileStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
                    streamReader = new StreamReader(fileStream, encoding);
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string strLine = streamReader.ReadLine();
                    while (strLine != null)
                    {
                        rowList.Add(strLine);
                        strLine = streamReader.ReadLine();
                    }
                }
                catch (IOException e) {
                    // 打印错误信息
                    Console.WriteLine(e.ToString());
                    throw e;
                } finally { 
                    if(streamReader != null) streamReader.Close();
                    if(fileStream != null) fileStream.Close();
                }
                return rowList;
            }
        }

        public static class FileWrite {
        /// <summary>
        /// 以指定的编码将指定的内容写入到指定的文件
        /// </summary>
        /// <param name="path">要写入的文件路径</param>
        /// <param name="content">写入的内容</param>
        /// <param name="encoding">写入的编码</param>
        /// <returns>是否写入成功</returns>
        public static bool WriteFile(string path, string content, Encoding encoding) {
            try {
                fileStream = new FileStream(path, FileMode.Create);
                streamWriter = new StreamWriter(fileStream, encoding);
                //开始写入
                streamWriter.Write(content);
                return true;
            } catch (IOException e) {
                return false;
                throw e;
            } finally {
                //清空缓冲区
                streamWriter.Flush();
                //关闭流
                streamWriter.Close();
                fileStream.Close();
            }
        }
        /// <summary>
        /// 以指定的编码将指定的内容追加到指定的文件
        /// </summary>
        /// <param name="path">要写入的文件路径</param>
        /// <param name="content">写入的内容</param>
        /// <param name="addToBoo"></param>
        /// <param name="encoding">写入的编码</param>
        /// <returns>是否写入成功</returns>
        public static bool AddWriteFile(string path, string content, Encoding encoding) {//将内容追加到指定路径的文件
            try {
                streamWriter = new StreamWriter(path,true, encoding);
                //开始写入
                streamWriter.Write(content);
                return true;
            } catch (Exception e) {
                return false;
                throw e;
            } finally {
                streamWriter.Close();//关闭流
                //清空缓冲区
                streamWriter.Flush();
            }
        }
        /// <summary>
        /// 将内容按行写入指定路径的文件
        /// </summary>
        /// <param name="path">指定路径的文件</param>
        /// <param name="strLine">要写入的文本数组</param>
        /// <returns></returns>
        public static bool RowWriteFile(string path, string[] strLine) {
            try {
                fileStream = new FileStream(path, FileMode.Create);
                streamWriter = new StreamWriter(fileStream);
                //开始写入
                foreach (string s in strLine)
                {
                    streamWriter.WriteLine(s);
                }
                return true;
            } catch (IOException e) {
                return false;
                throw e;
            } finally {
                //清空缓冲区
                streamWriter.Flush();
                //关闭流
                streamWriter.Close();
                fileStream.Close();
            }
        }
        
        
        }
        /// <summary>
        /// 用记事本打开文本
        /// </summary>
        /// <param name="text"></param>
        public static void TurnOnNotepad(string text) { 
            Process process = Process.Start("notepad.exe");//打开记事本
            // 循环判断打开的记事本的窗口句柄不为0
            while(process.MainWindowHandle.Equals(IntPtr.Zero))
            {
                process.Refresh();
            }
            // 得到文本框主窗体中的类名为Edit的窗口句柄
            IntPtr vHandle = WinApiUtilsMet.FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
            // 向指定窗口句柄发送设置文本的消息
            WinApiUtilsMet.SendMessage(vHandle, 0x000C, 0, text);        
        }
        
        // 保存为JAVA文件
        public static void SaveJavaFile(string str, string name, Encoding encoding) { 
            SaveFileDialog newSaveFile = new SaveFileDialog();
            newSaveFile.RestoreDirectory = false;
            newSaveFile.ValidateNames = true;
            newSaveFile.DefaultExt = "txt";
            newSaveFile.FileName = name+".java";
            newSaveFile.Filter = "java文档(*.java)|*.java|所有文件(*.*)|*.*";
            //判断是否点击确定
            if (newSaveFile.ShowDialog() == DialogResult.OK) {
                string path = newSaveFile.FileName;
                // 调用方法写入文件内容
                FileWrite.WriteFile(path, str, encoding);
            }    
        }
        /// <summary>
        /// 判断指定的文件路径是否存在
        /// </summary>
        /// <param name="path">指定的文件路径</param>
        /// <returns></returns>
        public static bool isFileUrl(string path) {
            return File.Exists(@path);
        }
        /// <summary>
        /// 判断指定文件是否为只读
        /// </summary>
        /// <param name="fileUrl">指定的文件路径</param>
        /// <returns></returns>
        public static bool IsFileReadOnly(string fileUrl) {
            FileInfo fileInfo = new FileInfo(fileUrl);
            return (fileInfo.Attributes == FileAttributes.ReadOnly);
        }
        /// <summary>
        ///  获取一个目录下的路径，文件名，扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetPathArr(string path) { 
            string[] pathArr = new string[3];
            int pathI = path.LastIndexOf('\\');
            int fileI = path.LastIndexOf('.');
            if((-1).Equals(pathI)) {
                pathI = path.LastIndexOf('/');
            }
            // 目录名
            string pathName = path.Substring(0, pathI);
            // 文件名
            string fileName = path.Substring(pathI+1, (fileI-(pathI+1)));
            // 扩展名
            string filterName = path.Substring(fileI+1, (path.Length - (fileI+1) ));

            pathArr[0] = pathName;
            pathArr[1] = fileName;
            pathArr[2] = filterName;
            return pathArr;
        }
        /// <summary> 
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型 
        /// </summary> 
        /// <param name=“FILE_NAME“>文件路径</param> 
        /// <returns>文件的编码类型</returns> 
        public static Encoding GetType(string FILE_NAME) {
            Encoding encoding = Encoding.Default;
            if (File.Exists(FILE_NAME)) { 
                FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read); 
                encoding = GetType(fs); 
                fs.Close(); 
            }
            return encoding; 
        } 
        /// <summary>
        /// 获取文件的全部编码 key为编码代表的code value为name
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetAllFileEncodingInfo() { 
            Dictionary<int, string> retDic = new Dictionary<int, string>();
            EncodingInfo[] codings = Encoding.GetEncodings();
            foreach (EncodingInfo coding in codings) {
                if (!retDic.ContainsKey(coding.CodePage)) {
                    retDic.Add(coding.CodePage, coding.Name.ToUpper());
                }
            }
            // 升序排序
            retDic = retDic.OrderBy(p=>p.Value).ToDictionary(p => p.Key, o => o.Value);
            return retDic;
        }
        /// <summary>
        /// 获取文件的全部编码 key为编码代表的code value为name
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> GetBrieflyFileEncodingInfo() { 
            Dictionary<int, string> retDic = new Dictionary<int, string>();
            retDic.Add(Encoding.ASCII.CodePage, Encoding.ASCII.BodyName.ToUpper());
            retDic.Add(Encoding.Default.CodePage, Encoding.Default.BodyName.ToUpper());
            retDic.Add(Encoding.Unicode.CodePage, Encoding.Unicode.BodyName.ToUpper());
            retDic.Add(Encoding.UTF32.CodePage, Encoding.UTF32.BodyName.ToUpper());
            retDic.Add(Encoding.UTF7.CodePage, Encoding.UTF7.BodyName.ToUpper());
            retDic.Add(Encoding.UTF8.CodePage, Encoding.UTF8.BodyName.ToUpper());
            // 升序排序
            retDic = retDic.OrderBy(p=>p.Value).ToDictionary(p => p.Key, o => o.Value);
            return retDic;
        }
        /// <summary> 
        /// 通过给定的文件流，判断文件的编码类型 
        /// </summary> 
        /// <param name=fs>文件流</param> 
        /// <returns>文件的编码类型</returns> 
        public static Encoding GetType(FileStream fs) { 
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 }; 
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 }; 
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 
            Encoding reVal = Encoding.Default; 

            BinaryReader r = new BinaryReader(fs, Encoding.Default); 
            int i; 
            int.TryParse(fs.Length.ToString(), out i); 
            byte[] ss = r.ReadBytes(i); 
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)) 
            { 
                reVal = Encoding.UTF8; 
            } 
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00) 
            { 
                reVal = Encoding.BigEndianUnicode; 
            } 
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41) 
            { 
                reVal = Encoding.Unicode; 
            } 
            r.Close(); 
            return reVal;
        }
        /// <summary>
        /// 判断文件时文本文件还是二进制文件
        /// </summary>
        /// <param name="fileName">文件路径</param>
        /// <returns>true 文本文件 false 二进制文件</returns>
        public static bool CheckIsTextFile(string fileName) {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            bool isTextFile = true;
            try {
                int i = 0;
                int length = (int)fs.Length;
                byte data;
                while (i < length && isTextFile)
                {
                    data = (byte)fs.ReadByte();
                    isTextFile = (data != 0);
                    i++;
                }
            } catch (Exception ex) {
                throw ex;
            }
            finally {
                if (fs != null) fs.Close();
            }
            return isTextFile;
        }

        /// <summary> 
        /// 判断是否是不带 BOM 的 UTF8 格式 
        /// </summary> 
        /// <param name=“data“></param> 
        /// <returns></returns> 
        private static bool IsUTF8Bytes(byte[] data) { 
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数 
            byte curByte; //当前分析的字节. 
            for (int i = 0; i < data.Length; i++)  { 
                curByte = data[i]; 
                if (charByteCounter == 1) { 
                    if (curByte >= 0x80) { 
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0) 
                        { 
                            charByteCounter++; 
                        } 
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6) { 
                            return false; 
                        } 
                    } 
                } else { 
                
                    if ((curByte & 0xC0) != 0x80) {  //若是UTF-8 此时第一位必须为1 
                        return false; 
                    } 
                    charByteCounter--; 
                } 
            } 
            if (charByteCounter > 1) {
                throw new Exception("非预期的byte格式"); 
            } 
            return true; 
        }
        /// <summary>
        /// 将文件内容设置到文本框中
        /// </summary>
        /// <param name="textBox">文本框 为null则打开一个新标签</param>
        /// <param name="path">文本框路径</param>
        /// <param name="encoding">编码</param>
        public static void SetTextBoxValByPath(TextBox textBox, string path, Encoding encoding) {
            TextBox t = textBox;
            // 传入的文本框为null则新建一个标签
            if(path == null) return;
            // 判断是否为二进制文件
            if (!CheckIsTextFile(path)) { 
                DialogResult dr = MessageBox.Show("该文件可能为二进制文件，是否继续读取？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel) {
                    return;
                }
                if (dr == DialogResult.No) {
                    return;
                }
            }
            // 将文件内容赋值到文本框中
            t.Text = FileRead.Read(path, encoding);
            t.SelectionStart = t.TextLength;

            // 将文件路径写入到文本框tag数据中
            TextBoxUtilsMet.TextBoxAddTag(t, TextBoxTagKey.SAVE_FILE_PATH, path);
            // 将文件编码写入到文本框tag数据中
            TextBoxUtilsMet.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_TAG_KEY_ECODING, encoding);
            // 监听文件变化并弹窗提醒
            monitorFileTextShowMess(path, t);
            t.Focus();
        }
        /// <summary>
        /// 监听文件变化并弹出提示框提示重新加载或另存为
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="t"></param>
        public static void monitorFileTextShowMess(string filepath, TextBox t) {
            Dictionary<Type, object> data = new Dictionary<Type, object>();
            data.Add(typeof(TextBox), t);

            // 监听文件变化
            try { 
                FileSystemWatcher wat = null;
                string[] pathArr = GetPathArr(filepath);
                Encoding encoding = Encoding.Default;
                if(!"txt".Equals(pathArr[2].ToLower())) {
                    encoding = GetType(filepath);
                }
                // 判断文本框的Tag中是否纯在一个监听,存在就销毁他
                if(TextBoxUtilsMet.GetTextTagToMap(t).ContainsKey(TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR)){
                    object obj = TextBoxUtilsMet.GetTextTagToMap(t)[TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR];
                    if(obj.GetType().Equals(typeof(FileSystemWatcher))) { 
                        wat =(FileSystemWatcher) TextBoxUtilsMet.GetTextTagToMap(t)[TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR];
                        wat.EnableRaisingEvents = false;
                        wat.Dispose();
                    }
                }
                
                // 获取一个新的文件监听
                wat = fileMonitor(pathArr[0], pathArr[1]+"."+pathArr[2],
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
                        ControlsUtilsMet.ShowAskMessBox("文件内容已经更改,是否要重新加载文件", "提示"
                        ,delegate{
                            if (t.InvokeRequired) {
                                t.Invoke(new EventHandler(delegate {
                                    // 获取内容
                                    string text = FileRead.Read(filepath, encoding);
                                    t.Text = text;
                                }));
                            }    
                        },null);
                        watcher.EnableRaisingEvents = false;
                    }
                    , delegate{ // 弹出对话框
                        MessageBox.Show("文件在磁盘上已经被删除或重命名！");
                });
                // 加入到文本框的tag数据中
                TextBoxUtilsMet.TextBoxAddTag(t, TextBoxTagKey.TEXTBOX_TAG_KEY_FILEMONITOR, wat);
            } catch {
                MessageBox.Show("监听文件状态时发生异常");
            }
        }
        /// <summary>
        /// 监听文件的改动并执行对应事件
        /// </summary>
        /// <param name="path">文件目录</param>
        /// <param name="filter">后缀名</param>
        /// <param name="NotifyFilter">要监听的类型</param>
        /// <param name="delEvent">目录或目录文件删除时发生</param>
        /// <param name="creatEvent">目录或创建时发生</param>
        /// <param name="changedEvent">目录或目录文件内容改变时发生</param>
        /// <param name="renamedEvent">目录或目录文件重命名时发生</param>
        /// <returns></returns>
        public static FileSystemWatcher fileMonitor(string path, string filter, NotifyFilters NotifyFilter, FileSystemEventHandler delEvent
            , FileSystemEventHandler creatEvent, FileSystemEventHandler changedEvent, RenamedEventHandler renamedEvent){ 
            FileSystemWatcher watcher = new FileSystemWatcher();
            // 设置监听的路径
            watcher.Path = path;
            // 监听的过滤器
            watcher.Filter = filter;
            // 要监听的类型
            watcher.NotifyFilter = NotifyFilter;
            if(delEvent != null) {
                watcher.EnableRaisingEvents = true;
                // 删除时发生
                watcher.Deleted += delEvent;
            }
            if(creatEvent != null) { 
                watcher.EnableRaisingEvents = true;
                // 创建时发生
                watcher.Created += creatEvent;
            }
            if(changedEvent != null) {
                watcher.EnableRaisingEvents = true;
                // 更改文件内容时发生
                watcher.Changed += changedEvent;
            }
            if(renamedEvent != null) {
                watcher.EnableRaisingEvents = true;
                // 重命名发生
                watcher.Renamed += renamedEvent;
            }
            // watcher.EnableRaisingEvents = true;
            return watcher;
        }
    }
}
