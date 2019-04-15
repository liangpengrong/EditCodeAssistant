using Core.CacheLibrary.FormCache;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace CharsToolset
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            mainStartClass(args);
        }
        // 程序的主启动方法
        private static void mainStartClass(string[] args)
        {
            //Application.ThreadException += ApplicationExc.Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length > 0) {
                Application.Run(new RootDisplayForm(args));
            } else { 
                Application.Run(new RootDisplayForm());
            }
            // 
            FormCacheFactory.clearDeaFormTimers();
        }
        // 获取程序集的GUID
        private static string getGUID()
        {
          Attribute guid_attr = Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute));
          string guid = ((GuidAttribute)guid_attr).Value;
          return guid;
        }
        // 判断窗体是否重复打开
        private static bool isFormOnly(string guid)
        {
            bool b = true;
            winAPIMethods._mutex = new System.Threading.Mutex(true, guid, out b);
            return b;
        }
        // 通过进程名称获得已打开窗体的句柄
        private static IntPtr pidObtainHandle()
        {
            string proc = Process.GetCurrentProcess().ProcessName;
            // 得到全部的进程
            Process[] processes = Process.GetProcesses();
            IntPtr handle = IntPtr.Zero;
            try {
                foreach(Process p in processes) {// 遍历进程
                    if(p.ProcessName.Equals(proc) &&!p.Id.Equals(getProcessPID()) ) {
                        handle = p.MainWindowHandle;
                    }
                }
            } catch(Exception e) {
              MessageBox.Show("获取已打开的窗口的句柄失败，错误原因:"+e.ToString());
            }
            return handle;
        }
        // 多次闪烁窗体
        private static void winFlashs(IntPtr intPtr,int countTime) {
            System.Timers.Timer myTimer = new System.Timers.Timer();
            int counter = 0;
            myTimer.AutoReset = true;
            myTimer.Interval = 300;
            myTimer.Enabled = true;
            myTimer.Elapsed += (sender, e) => {
                winAPIMethods.FlashWindow(intPtr, true);
                counter = counter + 1;
                if (counter.Equals((int)Math.Floor((double)(countTime * 1000 / 300)))) {
                    myTimer.Enabled = false;
                    myTimer.Dispose();
                }
            };
        }
        // 获取当前进程的PID
        private static int getProcessPID() {
            return Process.GetCurrentProcess().Id;
        }
        // 集中调用WindowsAPI和静态变量的静态内部类
        private static class winAPIMethods {
            internal static System.Threading.Mutex _mutex;
            [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "FlashWindow")]
            internal static extern void FlashWindow(IntPtr hwnd, bool bInvert);
        }
    }
}
