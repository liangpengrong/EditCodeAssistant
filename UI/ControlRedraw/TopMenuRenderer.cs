using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.StaticMethod.Method.Redraw {
    /// <summary>
    /// 顶部菜单重绘类
    /// </summary>
    public class TopMenuRenderer : ToolStripRenderer{
        // 整个背景色
        private Color backColor = ColorTranslator.FromHtml("#fff");
        // 菜单项选中背景色
        private Color selectColor = ColorTranslator.FromHtml("#91C9F7");
        // 复选框背景色
        private Color checkBoxColor = ColorTranslator.FromHtml("#90C8F6");
        // 复选框所处菜单项选中背景色
        private Color checkBoxSelColor = ColorTranslator.FromHtml("#4EA8F2");

        // 整个背景
        protected override void OnRenderToolStripBackground (ToolStripRenderEventArgs e) {
            Brush b = new SolidBrush(backColor);
            RectangleF rectF = e.Graphics.ClipBounds;
            e.Graphics.FillRectangle(b, rectF);
            base.OnRenderToolStripBackground(e);
        }
        // 边框
        protected override void OnRenderToolStripBorder (ToolStripRenderEventArgs e) {
            Rectangle rec = new Rectangle(new Point(e.AffectedBounds.X,e.AffectedBounds.Y)
                ,new Size(e.AffectedBounds.Width,e.AffectedBounds.Height));
            // 顶部项
            if(e.ToolStrip is MenuStrip) {
                ControlPaint.DrawBorder(e.Graphics, rec,
                ColorTranslator.FromHtml("#CCCCCC"), 0, ButtonBorderStyle.Solid, //左边
                ColorTranslator.FromHtml("#CCCCCC"), 0, ButtonBorderStyle.Solid, //上边
                ColorTranslator.FromHtml("#CCCCCC"), 0, ButtonBorderStyle.Solid, //右边
                ColorTranslator.FromHtml("#CCCCCC"), 0, ButtonBorderStyle.Solid //下边
                );
            } 
            // 下拉项
            else if(e.ToolStrip is ToolStripDropDown) { 
                ControlPaint.DrawBorder(e.Graphics, rec,
                ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid, //左边
                ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid, //上边
                ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid, //右边
                ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid //下边
                );
            }
            
            base.OnRenderToolStripBorder(e);
        }
        // 分隔栏
        protected override void OnRenderSeparator (ToolStripSeparatorRenderEventArgs e){
            Pen pen = new Pen(ColorTranslator.FromHtml("#D7D7D7"));
            RectangleF rectF = e.Graphics.VisibleClipBounds;
            float y = rectF.Y + 2;
            e.Graphics.DrawLine(pen ,rectF.X + 25 ,y , rectF.Right, y);
            base.OnRenderSeparator(e);
        }
        // 菜单项
        protected override void OnRenderMenuItemBackground (ToolStripItemRenderEventArgs e) {
            Color backC = selectColor;
            RectangleF rectF = e.Graphics.VisibleClipBounds;
            // 顶级菜单
            if(e.ToolStrip is MenuStrip) {
                //如果被选中或被按下
                if (e.Item.Selected){
                    if(e.Item.Enabled) { 
                        backC = selectColor;
                    } else { 
                        backC = ColorTranslator.FromHtml("#DDDDDD");
                    }
                    Brush b = new SolidBrush(backC);
                    e.Graphics.FillRectangle(b, rectF);
                }
                else
                    base.OnRenderMenuItemBackground(e);
            }
            // 下拉项
            else if (e.ToolStrip is ToolStripDropDown)
            {
                // 判断是否选中
                if (e.Item.Selected){
                    e.Item.OwnerItem.Select();
                    if(e.Item.Enabled) {
                        backC = selectColor;
                    } else { 
                        backC = ColorTranslator.FromHtml("#DDDDDD");
                    }
                    Brush brush = new SolidBrush(backC);
                    e.Graphics.FillRectangle(brush, rectF);
                }
            } 
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
        // 箭头
        protected override void OnRenderArrow (ToolStripArrowRenderEventArgs e) {
            if (e.Item.Enabled){ 
              e.ArrowColor = ColorTranslator.FromHtml("#3E3E3E");
            } else{ 
              e.ArrowColor = ColorTranslator.FromHtml("#C3C3C3");
            }
            RectangleF rectF = e.Graphics.VisibleClipBounds;

            e.ArrowRectangle = new Rectangle(
                (int)(rectF.Right /1) - 15
                ,(int)(rectF.Y/1) + (((int)rectF.Height/2)-5) ,10,10);
            base.OnRenderArrow(e);
        }
        // 复选框
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e) {
            Color col = checkBoxColor;
            Rectangle imageRec = e.ImageRectangle;
            Rectangle rec = new Rectangle(imageRec.X-2 , imageRec.Y-2, imageRec.Width+4, imageRec.Height+4);
            if(e.Item.Selected) {
                col = checkBoxSelColor;
            } else { 
                col = checkBoxColor;
            }
            Brush bru = new SolidBrush(col);
            e.Graphics.FillRectangle(bru, rec);
            base.OnRenderItemCheck(e);
        }
        // 文本
        protected override void OnRenderItemText (ToolStripItemTextRenderEventArgs e) {
            e.TextColor = e.Item.ForeColor;
            e.TextFont = e.ToolStrip.Font;
            base.OnRenderItemText(e);
        }
        // 左边图像和复选框区域
        protected override void OnRenderImageMargin (ToolStripRenderEventArgs e) {
            Brush b = new SolidBrush(backColor);
            e.Graphics.FillRectangle(b,e.AffectedBounds);
            base.OnRenderImageMargin(e);
        }
    }
}
