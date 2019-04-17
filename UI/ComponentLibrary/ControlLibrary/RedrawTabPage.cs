﻿using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawTabPage : TabPage{

        public RedrawTabPage() {
            // 设置控件样式
            setThisStyles();
            // 加载默认配置
            initTabPageConfig();
            setPageDispLayout();
        }
        /// <summary>
        /// 设置控件样式
        /// </summary>
        private void setThisStyles() { 
            SetStyle(
               ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁  
               ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁  
               ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件  
               ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明  
               true);                                         // 设置以上值为 true  
            UpdateStyles(); 
        }

        // 调整大小事件
        protected override void OnResize(EventArgs e) {
            setPageDispLayout();
            base.OnResize(e);
        }
        // 获得焦点事件
        protected override void OnGotFocus(EventArgs e) {
            // 设置文本的pading属性
            setPageDispLayout();
            base.OnGotFocus(e);
        }
        // 大小改变事件
        protected override void OnSizeChanged(EventArgs e) {
            setPageDispLayout();
            base.OnSizeChanged(e);
        }
        // 控件添加事件
        protected override void OnControlAdded(ControlEventArgs e) {
            Control con = e.Control;
            if(con.Location.Y < 2) { 
                con.Location = new Point(con.Location.X, con.Location.Y+2);
                con.Size = new Size(con.Width, con.Height-2);
            }
            setPageDispLayout();
            base.OnControlAdded(e);
        }
        // 设置文本框的默认配置
        private void initTabPageConfig() { 
            // 实例化一个Page
            RedrawTabPage page = this;
            // 设置Page的背景颜色为白色
            string timeStr = DateTime.Now.ToUniversalTime().Ticks.ToString();
            page.BackColor = Color.White;
            page.Name = TabControlDataLib.PAGE_NAME_DEF + timeStr;
            page.Text = TabControlDataLib.PAGE_TEXT;
            page.UseVisualStyleBackColor = true;
            page.Padding = new Padding(0,20,0,0);
            page.Margin = new Padding(0,0,0,0);
            page.ToolTipText = page.Text;
            // 设置Page的大小
            page.Size = new Size(1, 1);
            // 进入控件事件
            page.Enter += (object sender, EventArgs e) =>{ 
                TabPage pp = (TabPage)sender;
                ControlsUtilsMet.timersMet(200, (object sender1, ElapsedEventArgs e1)=>{
                    Form f = pp.FindForm();
                    if(f != null) { 
                        if(f.InvokeRequired) { 
                            f.Invoke(new EventHandler(delegate {
                                if(pp.Controls.Count > 0) { 
                                    Control con = pp.Controls[pp.Controls.Count-1];
                                    f.ActiveControl = con;
                                    ((System.Timers.Timer)sender1).Dispose();
                                }
                            }));
                        }
                        }
                    
                });
            };
        }
        /// <summary>
        /// 设置文本具有pading属性
        /// </summary>
        private void setPageDispLayout() {
            Rectangle rect = new Rectangle();
            WinApiUtilsMet.SendMessage(Handle, 178, (IntPtr)0, ref rect);
            int top = Padding != Padding.Empty? Padding.Top : 1;
            int bottom = Padding != Padding.Empty? Padding.Bottom : 1;
            int left = Padding != Padding.Empty? Padding.Left : 1;
            int right = Padding != Padding.Empty? Padding.Right : 1;
            rect.Y = top;
            rect.X = left;
            rect.Height = ClientSize.Height - bottom;
            rect.Width = ClientSize.Width - right;
            WinApiUtilsMet.SendMessage(Handle, 179, IntPtr.Zero, ref rect);
            this.Refresh();
        }
    }
}