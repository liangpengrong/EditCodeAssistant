using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Security.Permissions;

namespace PubMethodLibrary
{
    /// <summary>
    /// 关于文件操作的工具类
    /// </summary>
    public class FileUtilsMet
    {
        private static FileStream fileStream = null;
        private static StreamWriter streamWriter = null;
        private static StreamReader streamReader = null;
        public static class FileRead {
            /// <summary>
            /// 按照指定的编码读取文件内容返回读取到的内容
            /// </summary>
            /// <param name="fileUrl">读取的文件路径</param>
            /// <param name="encoding">指定的编码</param>
            /// <returns></returns>
            public static String read(String fileUrl, Encoding encoding)
            {
                string str = "";
                try
                {
                    fileStream = new FileStream(fileUrl, FileMode.Open);
                    streamReader = new StreamReader(fileStream, encoding);
                    str = streamReader.ReadToEnd();
                    streamReader.Close();
                    fileStream.Close();
                }
                catch (IOException e)
                {
                    System.Console.WriteLine(e.ToString());
                }
                return str;
            }
            /// <summary>
            /// 按照指定的编码读取文件内容按行返回读取到的内容
            /// </summary>
            /// <param name="fileUrl"></param>
            /// <param name="encoding"></param>
            /// <returns></returns>
            public static List<String> rowRead(String fileUrl, Encoding encoding)
            {
                List<string> rowList = new List<string>();
                try
                {
                    fileStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
                    streamReader = new StreamReader(fileStream, encoding);
                    streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
                    string strLine = streamReader.ReadLine();
                    while (strLine != null)
                    {
                        rowList.Add(strLine);
                        strLine = streamReader.ReadLine();
                    }
                    //关闭此StreamReader对象
                    streamReader.Close();
                    fileStream.Close();
                }
                catch (IOException e)
                {
                    System.Console.WriteLine(e.ToString());//打印错误信息
                    return rowList;
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
            public static Boolean writeFile(String path, String content, Encoding encoding)
        {
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                streamWriter = new StreamWriter(fileStream, encoding);
                //开始写入
                streamWriter.Write(content);
                return true;
            }
            catch (IOException e)
            {
                return false;
                throw e;
            }
            finally 
            {
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
        public static Boolean addWriteFile(String path, String content, Encoding encoding)
        {//将内容追加到指定路径的文件
            try
            {
                streamWriter = new StreamWriter(path,true, encoding);
                //开始写入
                streamWriter.Write(content);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
            finally 
            {
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
        public static Boolean rowWriteFile(String path, String[] strLine)
        {
            try
            {
                fileStream = new FileStream(path, FileMode.Create);
                streamWriter = new StreamWriter(fileStream);
                //开始写入
                foreach (String s in strLine)
                {
                    streamWriter.WriteLine(s);
                }
                return true;
            }
            catch (IOException e)
            {
                return false;
                throw e;
            }
            finally
            {
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
        public static void turnOnNotepad(String text) { 
            Process process = Process.Start("notepad.exe");//打开记事本
            while(process.MainWindowHandle.Equals(IntPtr.Zero))
            {//循环判断打开的记事本的窗口句柄不为0
                process.Refresh();
            }
            //得到文本框主窗体中的类名为Edit的窗口句柄
            IntPtr vHandle = WinApiUtilsMet.FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
            //向指定窗口句柄发送设置文本的消息
            WinApiUtilsMet.SendMessage(vHandle, 0x000C, 0, text);        
        }
        /// <summary>
        /// 判断指定的文件路径是否存在
        /// </summary>
        /// <param name="path">指定的文件路径</param>
        /// <returns></returns>
        public static Boolean isFileUrl(String path)
        {
            return File.Exists(@path);
        }
        /// <summary>
        /// 判断指定文件是否为只读
        /// </summary>
        /// <param name="fileUrl">指定的文件路径</param>
        /// <returns></returns>
        public static Boolean isFileReadOnly(String fileUrl)
        {
            FileInfo fileInfo = new FileInfo(fileUrl);
            return (fileInfo.Attributes == FileAttributes.ReadOnly);
        }
        /// <summary>
        ///  获取一个目录下的路径，文件名，扩展名
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static String[] getPathArr(String path) { 
            String[] pathArr = new string[3];
            int pathI = path.LastIndexOf('\\');
            int fileI = path.LastIndexOf('.');
            if((-1).Equals(pathI)) {
                pathI = path.LastIndexOf('/');
            }
            // 目录名
            String pathName = path.Substring(0, pathI);
            // 文件名
            String fileName = path.Substring(pathI+1, (fileI-(pathI+1)));
            // 扩展名
            String filterName = path.Substring(fileI+1, (path.Length - (fileI+1) ));

            pathArr[0] = pathName;
            pathArr[1] = fileName;
            pathArr[2] = filterName;
            return pathArr;
        }
        /// <summary>
        /// 通过给定的文件名判断编码
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <returns></returns>
        public static Encoding isFileEncoding(String filePath) { 
            Encoding enc = Encoding.UTF8;
            if(File.Exists(@filePath)) { 
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read); 
                Encoding r = isFileEncoding(fs); 
                fs.Close(); 
            }
            return enc;
        }
        /// <summary>
        /// 通过给定的文件流判断编码
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static Encoding isFileEncoding(FileStream fs) { 
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 }; 
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 }; 
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 
            Encoding reVal = Encoding.Default; 

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default); 
            int i; 
            int.TryParse(fs.Length.ToString(), out i); 
            byte[] ss = r.ReadBytes(i); 
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF)) 
            { 
                reVal = Encoding.UTF8; 
            } else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00) { 
                reVal = Encoding.BigEndianUnicode; 
            } 
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41) { 
                reVal = Encoding.Unicode; 
            } 
            r.Close(); 
            return reVal; 

        }
        /// <summary> 
        /// 判断是否是不带 BOM 的 UTF8 格式 
        /// </summary> 
        /// <param name=“data“></param> 
        /// <returns></returns> 
        private static bool IsUTF8Bytes(byte[] data) { 
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数 
            byte curByte; //当前分析的字节. 
            for (int i = 0; i < data.Length; i++) 
            { 
                curByte = data[i]; 
                if (charByteCounter == 1) { 
                    if (curByte >= 0x80) { 
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0) 
                        { 
                            charByteCounter++; 
                        } 
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6) 
                        { 
                            return false; 
                        } 
                    } 
                } else { 
                //若是UTF-8 此时第一位必须为1 
                if ((curByte & 0xC0) != 0x80) { 
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
        // [PermissionSetAttribute(SecurityAction.Demand, Name ="FullTrust")]
        public static FileSystemWatcher fileMonitor(String path, String filter, NotifyFilters NotifyFilter, FileSystemEventHandler delEvent
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
