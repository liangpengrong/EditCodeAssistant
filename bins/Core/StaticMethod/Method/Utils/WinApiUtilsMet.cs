using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
namespace Core.StaticMethod.Method.Utils
{
    /// <summary>
    /// 关于WindowsAPI的工具类
    /// </summary>
    public class WinApiUtilsMet
    {
        /// <summary>
        /// 设置滚动条得到最小 最大值
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nBar"></param>
        /// <param name="lpMinPos"></param>
        /// <param name="lpMaxPos"></param>
        /// <returns></returns>
        [DllImport("user32", CharSet = CharSet.Auto)] 
        public static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);
        /// <summary>
        /// 获取指定滚动条中滚动按钮的当前位置
        /// </summary>
        /// <param name="hWnd">带有标准滚动条控件的句柄</param>
        /// <param name="nBar">0：水平滚动条，1：垂直滚动条</param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetScrollPos")]
        public static extern int GetScrollPos(IntPtr hWnd, int nBar);

        /// <summary>
        /// 设置滚动条位置
        /// </summary>
        /// <param name="hWnd">带有标准滚动条控件的句柄</param>
        /// <param name="nBar">0：水平滚动条，1：垂直滚动条</param>
        /// <param name="nPos">位置</param>
        /// <param name="bRedraw">重绘标志，是否重绘</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        /// <summary>
        /// 向一个指定的窗口句柄的窗口发送消息
        /// </summary>
        /// <param name="hWnd">指定的窗口句柄</param>
        /// <param name="Msg">要发送的消息类型</param>
        /// <param name="wParam">第一个消息参数</param>
        /// <param name="lParam">第二个消息参数</param>
        /// <returns></returns>
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        [DllImport("user32.dll")] 
        public static extern bool PostMessage(IntPtr hWnd, int nBar, int wParam, int lParam); 
        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。而和函数PostMessage不同，PostMessage是将一个消息寄送到一个线程的消息队列后就立即返回。
        /// </summary>
        [DllImport("user32.dll ", EntryPoint = "SendMessage")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// 向一个指定的窗口句柄的窗口发送消息
        /// </summary>
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, ref Rectangle lParam);
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
            return SystemFonts.MessageBoxFont;
        }
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        /// <summary>
        /// 获取指定的键的状态 ,检取指定虚拟键的状态
        /// </summary>
        /// <param name="vKey">0x01-鼠标左键 0x02-鼠标右键</param>
        /// <returns> 大于0 - 按下 小于0 - 松开</returns>
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);
        /// <summary>
        /// 获取指定的键的状态 ,检取指定物理键的状态
        /// </summary>
        /// <param name="vKey">0x01-鼠标左键 0x02-鼠标右键</param>
        /// <returns> 大于0 - 按下 小于0 - 松开</returns>
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(int vKey);
        public static bool GetCapitalState() {
            byte[] bs = new byte[256];
            GetKeyboardState(bs);
            return (bs[0x14] == 1);
        }
        /// <summary>
        /// 设置键盘的大小写状态
        /// </summary>
        /// <param name="State">true为开启</param>
        public static void SetCapitalState(bool State){
            if (State != (GetKeyState(0x14) == 1))
            {
             keybd_event(0x14, 0x45, 0x1 | 0, 0);
             keybd_event(0x14, 0x45, 0x1 | 0x2, 0);
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
        /// <param name="isTop">true顶层, false非顶层</param>
        /// <param name="f"></param>
        public static void setFormTopNoFocus(bool isTop, Form form) {
            IntPtr HWND_TOPMOST = new IntPtr(-1);
            IntPtr HWND_NOTOPMOST = new IntPtr(-2);
            IntPtr HWND_TOP = new IntPtr(0);
            const uint SWP_NOSIZE = 0x0001;
            const uint SWP_NOMOVE = 0x0002;
            const uint SWP_NOACTIVATE = 0x0010;
            if(form != null && !form.IsDisposed) {
                if(isTop){
                    SetWindowPos(form.Handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
                } else {
                    SetWindowPos(form.Handle, HWND_NOTOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
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
        /// 模拟鼠标动作
        /// </summary>
        [DllImport("user32.dll")]
        public static extern void mouse_event(int flags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// 该函数把光标移到屏幕的指定位置。如果新位置不在由 ClipCursor函数设置的屏幕矩形区域之内，则系统自动调整坐标，使得光标在矩形之内。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]  
        public static extern int SetCursorPos(int x, int y);  
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
