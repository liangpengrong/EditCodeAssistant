using Core.CacheLibrary.ControlCache;
using Core.DefaultData.DataLibrary;
using Core.StaticMethod.Method.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UI.ComponentLibrary.MethodLibrary.Interface;

namespace UI.ComponentLibrary.ControlLibrary {
    public class RedrawAddPageBut : Panel, IComponentInitMode<Control>{
        internal RedrawAddPageBut() { 
            initMainAddPageButton();
        }
        // 鼠标是否进入
        private bool mouse = false;
        // 鼠标离开颜色
        private Color mouseLeaveColor = ColorTranslator.FromHtml("#5A5A5A");
        
        /// <summary>
        /// 打开单例模式下的添加字符窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initSingleExample(bool isShowTop) {
            RedrawAddPageBut conThis = null;
            Control con = ControlCacheFactory.getSingletonCache(DefaultNameEnum.ADD_PAGE_BUTTON);
            if(con == null || !(con is RedrawAddPageBut)) {
                conThis = this;
                conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.ADD_CHARS_FORM);
                ControlCacheFactory.addSingletonCache(conThis);
            } else { 
                conThis = (RedrawAddPageBut)con;
            }
            if(isShowTop) conThis.BringToFront();
            return conThis;
        }
        /// <summary>
        /// 打开多例模式下的添加字符窗口
        /// </summary>
        /// <param name="isShowTop">是否显示为顶层窗体</param>
        /// <returns></returns>
        public Control initPrototypeExample(bool isShowTop) {
            RedrawAddPageBut conThis = this;
            conThis.Name = EnumUtils.GetDescription(DefaultNameEnum.ADD_CHARS_FORM)+DateTime.Now.Ticks.ToString();
            if(isShowTop) conThis.BringToFront();
            // 加入到多例工厂
            ControlCacheFactory.addPrototypeCache(DefaultNameEnum.ADD_CHARS_FORM, conThis);
            return conThis;
        }
        /// <summary>
        /// 初始化添加标签按钮
        /// </summary>
        /// <returns></returns>
        private void initMainAddPageButton() {
            this.Name = EnumUtils.GetDescription(DefaultNameEnum.ADD_PAGE_BUTTON);
            this.TabStop = false;
            this.Size = new Size(16,16);
            this.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            this.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            // but.BackColor = backColor;
            this.Paint += (object sender, PaintEventArgs e) =>{ 
                Panel panel = (Panel)sender;
                Graphics g = e.Graphics;
                Rectangle r = e.ClipRectangle;
                // 边距
                int margin = 2;
                // 线得高
                int height = 2;
                // 线的宽
                int width = panel.ClientSize.Width - margin*2;
                // 中间加号颜色
                if(mouse) {
                    Rectangle newr = new Rectangle(r.X-2,r.Y-2, r.Width+2,r.Height+2);
                    g.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#D0D0D0")), newr);
                }
                Pen pen = new Pen(mouseLeaveColor, height);
                // 第一条线的y坐标
                int y1 = (panel.ClientSize.Height-height)/2+(height/2);
                // 第二条线的x坐标
                int x2 = (panel.ClientSize.Width-height)/2+(height/2);
                g.DrawRectangle(new Pen(Color.Transparent), e.ClipRectangle);
                // 绘制中间的线
                g.DrawLine(pen, margin, y1, margin+width, y1);
                g.DrawLine(pen, x2, margin, x2, margin+width);
                
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
    }
}
