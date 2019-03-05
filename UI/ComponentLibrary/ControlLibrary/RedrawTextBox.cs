using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawTextBox : TextBox{
        //设置Rect消息
        private const int EM_SETRECT = 179;
        //获取Rect消息
        private const int EM_GETRECT = 178;
        //粘贴消息
        private const int WM_PASTE = 0x0302;
        // 私有Padding属性
        private Padding _textpadding=new Padding(1);
        // 公有Padding属性
        public Padding TextPadding { get { return _textpadding; } set { _textpadding = value; SetTextDispLayout(); } }
        
        public RedrawTextBox() { 
             SetStyle(  
             //ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制  
             ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
             ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
             ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
             ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
             true);                                         // 设置以上值为 true  
            UpdateStyles(); 
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetTextDispLayout();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            SetTextDispLayout();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetTextDispLayout();
        }
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref Rectangle lParam);
        /// <summary>
        /// 设置文本具有pading属性
        /// </summary>
        public void SetTextDispLayout()
        {
            Rectangle rect = new Rectangle();
            SendMessage(this.Handle, EM_GETRECT, (IntPtr)0, ref rect);
            rect.Y = TextPadding.Top;
            rect.X = TextPadding.Left;
            rect.Height = Height;
            rect.Width = Width - TextPadding.Right - TextPadding.Left;
            SendMessage(this.Handle, EM_SETRECT, IntPtr.Zero, ref rect);
        }
    }
}
