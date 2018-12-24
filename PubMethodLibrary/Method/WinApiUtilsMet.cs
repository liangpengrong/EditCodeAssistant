using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
namespace PubMethodLibrary
{
    /// <summary>
    /// 关于WindowsAPI的工具类
    /// </summary>
    public class WinApiUtilsMet
    {
        /// <summary>
        /// 向一个指定的窗口句柄的窗口发送消息
        /// </summary>
        /// <param name="hWnd">指定的窗口句柄</param>
        /// <param name="Msg">要发送的消息类型</param>
        /// <param name="wParam">第一个消息参数</param>
        /// <param name="lParam">第二个消息参数</param>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd,
            uint Msg, int wParam, string lParam);
        /// <summary>
        /// 在窗口列表中寻找与指定条件相符的第一个子窗口 。
        /// 该函数获得一个窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配。这个函数查找子窗口，从排在给定的子窗口后面的下一个子窗口开始。在查找时不区分大小写。
        /// </summary>
        /// <param name="hwndParent">主窗口的窗口句柄</param>
        /// <param name="hwndChildAfter">子窗口的句柄</param>
        /// <param name="lpszClass">指向一个指定的类名</param>
        /// <param name="lpszWindow">指向一个指定的窗口标题名</param>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent,
            IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        /// <summary>
        /// 该函数获得一个顶层窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配。这个函数不查找子窗口。在查找时不区分大小写。
        /// </summary>
        /// <param name="lpClassName">指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针。如果该参数为一个成员，则它必须为前次调用theGlobafAddAtom函 数产生的全局成员。该成员为16位，必须位于IpClassName的低 16位，高位必须为 0。</param>
        /// <param name="lpWindowName">指向一个指定了窗口名（窗口标题）的空结束字符串。如果该参数为空，则为所有窗口全匹配。返回值：如果函数成功，返回值为具有指定类名和窗口名的窗口句柄；如果函数失败，返回值为NULL。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// 获取当前鼠标位置对应的Point
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point p);
        /// <summary>
        /// 判断当前键盘的大小写
        /// </summary>
        /// <param name="pbKeyState"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetKeyboardState")]
        public static extern int GetKeyboardState(byte[] pbKeyState);
        /// <summary>
        /// 获取当前Windows的默认字体
        /// </summary>
        /// <returns></returns>
        public static Font GetWinDefaultFont()
        {
            return System.Drawing.SystemFonts.MessageBoxFont;
        }
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        public static Boolean GetCapitalState() {
            byte[] bs = new byte[256];
            GetKeyboardState(bs);
            return (bs[0x14] == 1);
        }
        /// <summary>
        /// 设置键盘的大小写状态
        /// </summary>
        /// <param name="State">true为开启</param>
        public static void SetCapitalState(bool State){
            if (State != (GetKeyState((int)0x14) == 1))
            {
             keybd_event((byte)0x14, 0x45, 0x1 | 0, 0);
             keybd_event((byte)0x14, 0x45, 0x1 | 0x2, 0);
            }
        }
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint wFlags);
        /// <summary>
        /// 获得当前前台窗体句柄
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        /// <summary>
        /// 将窗口置于最顶层而不获取焦点
        /// </summary>
        /// <param name="b">true顶层, false非顶层</param>
        /// <param name="f"></param>
        public static void topFormNoFocus(Boolean b, Form f) {
            IntPtr HWND_TOPMOST = new IntPtr(-1);
            IntPtr HWND_NOTOPMOST = new IntPtr(-2);
            IntPtr HWND_TOP = new IntPtr(0);
            const UInt32 SWP_NOSIZE = 0x0001;
            const UInt32 SWP_NOMOVE = 0x0002;
            const UInt32 SWP_NOACTIVATE = 0x0010;
            if(f != null && !f.IsDisposed) {
                if(b){
                    SetWindowPos(f.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                } else {
                    SetWindowPos(f.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                }
            }
         }
        /// <summary>
        /// 隐藏鼠标光标
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32", EntryPoint = "HideCaret")]
        public static extern bool HideCaret(IntPtr hWnd);
        /// <summary>
        /// 窗体动画函数
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>
        /// <param name="dwTime">指定动画持续的时间</param>
        /// <param name="dwFlags">指定动画类型，可以是一个或多个标志的组合。</param>
        /// <returns></returns>
        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        /// <summary>
        /// 闪烁指定窗体
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="invert"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr handle, bool invert);

        /// <summary>
        /// 闪烁指定窗体
        /// </summary>
        /// <param name="handle">窗体句柄</param>
        /// <param name="countTime">闪烁的间隔，单位毫秒</param>
        /// <param name="countTime">闪烁的总时长，单位秒</param>
        /// <param name="isMus">是否播放提示音</param>
        public static void flashWindesTime(IntPtr handle, int interval, int countTime, bool isMus) {
            // 是否播放提示音
            if(isMus) System.Media.SystemSounds.Asterisk.Play();
            // 定时器
            System.Timers.Timer myTimer = new System.Timers.Timer();
            int counter = 0;
            myTimer.AutoReset = true;
            myTimer.Interval = interval;
            myTimer.Enabled = true;
            myTimer.Elapsed += (sender, e) =>
            {
                FlashWindow(handle, true);
                counter = counter + 1;
                if(counter.Equals((int)Math.Floor((double)(countTime * 1000 / interval)))){
                    myTimer.Enabled = false;
                    myTimer.Dispose();
                }
            };
        }
    }
}
