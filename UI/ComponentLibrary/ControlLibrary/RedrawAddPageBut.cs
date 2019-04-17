using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawAddPageBut : Panel{
        private RedrawAddPageBut() { 
            initMainAddPageButton();
        }
        // 鼠标是否进入
        private bool mouse = false;
        // 鼠标进入颜色
        private Color mouseEnterColor = ColorTranslator.FromHtml("#007ACC");
        // 鼠标离开颜色
        private Color mouseLeaveColor = ColorTranslator.FromHtml("#5A5A5A");
        /// <summary>
        /// 初始化添加标签按钮
        /// </summary>
        /// <returns></returns>
        public void initMainAddPageButton() { 
            // 初始背景色
            Color backColor = Color.Red;
            this.Name = EnumUtilsMet.GetDescription(DefaultNameEnum.ADD_PAGE_BUTTON);
            this.TabStop = false;
            this.Size = new Size(16,16);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            // but.BackColor = backColor;
            this.Paint += (object sender, PaintEventArgs e) =>{ 
                Panel panel = (Panel)sender;
                // 边距
                int margin = 2;
                // 线得高
                int height = 2;
                // 线的宽
                int width = panel.ClientSize.Width - margin*2;
                // 中间加号颜色
                Pen pen = null;
                if(mouse) {
                    pen = new Pen(mouseEnterColor, height);
                } else { 
                    pen = new Pen(mouseLeaveColor, height);
                }
                // 第一条线的y坐标
                int y1 = (panel.ClientSize.Height-height)/2+(height/2);
                // 第二条线的x坐标
                int x2 = (panel.ClientSize.Width-height)/2+(height/2);
                e.Graphics.DrawRectangle(new Pen(Color.Transparent), e.ClipRectangle);
                // 绘制中间的线
                e.Graphics.DrawLine(pen, margin, y1, margin+width, y1);
                e.Graphics.DrawLine(pen, x2, margin, x2, margin+width);
                // 重绘边框
            };
            this.MouseEnter += (object sender, EventArgs e)=>{
                mouse = true;
                this.Refresh();
            };
            this.MouseLeave += (object sender, EventArgs e)=>{
                mouse = false;
                this.Refresh();
            };
        }

        /// <summary>
        /// 初始化单例模式下的添加标签按钮
        /// </summary>
        /// <returns></returns>
        public static Panel initSingleAddPageButton() { 
            Panel panel = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.ADD_PAGE_BUTTON);
            if(con == null || !(con is Panel)) {
                panel = new RedrawAddPageBut();
                ControlCacheFactory.addSingletonCache(panel);
            } else { 
                panel = (Panel)con;
            }
            return panel;
        }
    }
}
