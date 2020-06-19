using Core.CacheLibrary.FormCache;
using Core.StaticMethod.Method.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace EditCodeAssistant
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static void Main(string[] args)
        {
            if (!isSetupNetFramework()) {
                MessageBox.Show("当前系统未检测到.Net Framework库，请安装大于或等于v4.0版本的.Net Framework库"); 
                return; 
            }
            mainStartClass(args);
        }
        // 程序的主启动方法
        private static void mainStartClass(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // 判断窗口是否已经打开
            Process process = RunningInstance();
            if (process != null){
                IntPtr handle = process.MainWindowHandle;
                //System.Media.SystemSounds.Asterisk.Play();
                WindowsApiUtils.ShowWindow(handle, 5);
                WindowsApiUtils.SetForegroundWindow(handle);
                WindowsApiUtils.FlashWindow(handle, true);
                return;
            }
            if (args.Length > 0) {
                Application.Run(new RootDisplayForm(args));
            } else { 
                Application.Run(new RootDisplayForm());
            }
            FormCacheFactory.clearDeaFormTimers();
        }
        // 通过进程名称获得已打开窗体的句柄
        private static Process RunningInstance()
        {
            string proc = Process.GetCurrentProcess().ProcessName;
            // 得到全部的进程
            Process[] processes = Process.GetProcesses();
            try {
                foreach(Process p in processes) {
                    if(p.ProcessName.Equals(proc) && !p.Id.Equals(getProcessPID()) ) {
                        return p;
                    }
                }
            } catch(Exception e) {
                MessageBox.Show("获取已打开的窗口的句柄失败，错误原因:"+e.ToString());
            }
            return null;
        }
        // 获取当前进程的PID
        private static int getProcessPID() {
            return Process.GetCurrentProcess().Id;
        }
        // 集中调用WindowsAPI和静态变量的静态内部类
        private static class winAPIMethods {
            [DllImport("User32.dll", CharSet = CharSet.Unicode, EntryPoint = "FlashWindow")]
            internal static extern void FlashWindow(IntPtr hwnd, bool bInvert);
        }

        // 判断是否安装了.net环境
        private static bool isSetupNetFramework() { 
　　         RegistryKey key = Registry.LocalMachine;
　　         string[] subkeyNames = key.OpenSubKey("").OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy").GetSubKeyNames();
　　         //在这里我是判断test表项是否存在
　　         foreach (string keyName in subkeyNames) {
　　　　          if (keyName.IndexOf("v4") >= 0) {
　　　　　　          key.Close();
　　　　　　          return true;
　　　　          }
　　         }
　　         key.Close();
　　         return false;
        }
    }
}
