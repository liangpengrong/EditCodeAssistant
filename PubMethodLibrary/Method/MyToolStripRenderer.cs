using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PubMethodLibrary {
    public class MyToolStripRenderer : ToolStripRenderer{
        // 整个背景
        protected override void OnRenderToolStripBackground (ToolStripRenderEventArgs e) {
            Brush b = new SolidBrush(ColorTranslator.FromHtml("#F2F2F2"));
            
            RectangleF rectF = e.Graphics.ClipBounds;
            e.Graphics.FillRectangle(b, rectF);
            base.OnRenderToolStripBackground(e);
        }
        // 边框
        protected override void OnRenderToolStripBorder (ToolStripRenderEventArgs e) { 
            
            Rectangle rec = new Rectangle(new Point(e.AffectedBounds.X,e.AffectedBounds.Y)
                ,new Size(e.AffectedBounds.Width,e.AffectedBounds.Height));

            ControlPaint.DrawBorder(e.Graphics, rec,
            ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid, //左边
            ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid, //左边
            ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid, //左边
            ColorTranslator.FromHtml("#CCCCCC"), 1, ButtonBorderStyle.Solid //左边
            );
            base.OnRenderToolStripBackground(e);
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
            e.Item.ImageAlign = ContentAlignment.MiddleRight;
            Color backC = ColorTranslator.FromHtml("#91C9F7");
            RectangleF rectF = e.Graphics.VisibleClipBounds;
            // 顶级菜单
            if(e.ToolStrip is MenuStrip) {
                //如果被选中或被按下
                if (e.Item.Selected){
                    if(e.Item.Enabled) { 
                        backC = ColorTranslator.FromHtml("#91C9F7");
                    } else { 
                        backC = ColorTranslator.FromHtml("#DDDDDD");
                    }
                    Brush b = new SolidBrush(backC);
                    e.Graphics.FillRectangle(b, rectF);
                }
                else
                    base.OnRenderMenuItemBackground(e);
            }
            else if (e.ToolStrip is ToolStrip)
            {
                // 判断是否选中
                if (e.Item.Selected){
                    if(e.Item.Enabled) { 
                        backC = ColorTranslator.FromHtml("#91C9F7");
                    } else { 
                        backC = ColorTranslator.FromHtml("#DDDDDD");
                    }
                    Brush b = new SolidBrush(backC);
                    e.Graphics.FillRectangle(b, rectF);
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
            Brush bru = new SolidBrush(ColorTranslator.FromHtml("#90C8F6"));
            Rectangle imageRec = e.ImageRectangle;
            // 顶级菜单
            Rectangle rec = new Rectangle(imageRec.X-2 , imageRec.Y-2, imageRec.Width+4, imageRec.Height+4);
            if(e.Item.Selected) {
                bru = new SolidBrush(ColorTranslator.FromHtml("#228BE2"));
            } else { 
                bru = new SolidBrush(ColorTranslator.FromHtml("#90C8F6"));
            }
            e.Graphics.FillRectangle(bru, rec);
            base.OnRenderItemCheck(e);
        }
        // 文本
        protected override void OnRenderItemText (ToolStripItemTextRenderEventArgs e) { 
            e.TextColor = Color.Black;

            base.OnRenderItemText(e);
        }
        // 左边图像和复选框区域
        protected override void OnRenderImageMargin (ToolStripRenderEventArgs e) {
            Brush b = new SolidBrush(ColorTranslator.FromHtml("#F2F2F2"));
            e.Graphics.FillRectangle(b,e.AffectedBounds);
            base.OnRenderToolStripBackground(e);
        }
    }
}
