using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Core.StaticMethod.Method.Utils;

namespace UI.ComponentLibrary.ControlLibrary {
    /// <summary>
    /// 下拉按钮
    /// </summary>
    public partial class DropdownBut : Component {

        /// <summary>
        /// 历史纪录Panel
        /// </summary>
        private Panel historicalPanel = null;
        /// <summary>
        /// 历史纪录
        /// </summary>
        private string[] history;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="history">历史纪录</param>
        /// <param name="text">按钮文本</param>
        /// <param name="name">按钮Name</param>
        public DropdownBut(string[] history){
            this.history = history;
            InitializeComponent();
            assembleBut();
        }

        /// <summary>
        /// 生成下拉按钮组合
        /// </summary>
        /// <param name="text">按钮文本</param>
        /// <param name="name">按钮Name</param>
        private void assembleBut() { 
            Button b1 = this.下拉按钮_but;
            b1.Location = new Point(0, 0);
            Button b2 = this.button1;
            // 设置相对位置
            b2.Location = new Point(b1.Location.X+b1.Width-1, b1.Location.Y);
            b2.Size = new Size(25, b1.Height);
            this.下拉按钮_pan.Controls.Add(b1);
            this.下拉按钮_pan.Controls.Add(b2);

        }
        // 按钮鼠标移入事件
        private void but_MouseEnter(object sender, EventArgs e) {
            Button but = this.下拉按钮_but;
            Button but2 = this.button1;
            // 边框颜色
            but.FlatAppearance.BorderColor =ColorTranslator.FromHtml("#0078D7");
            but.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E5F1FB");
            but.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#D4E8F8");
            // 边框颜色
            but2.FlatAppearance.BorderColor =ColorTranslator.FromHtml("#0078D7");
            but2.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E5F1FB");
            but2.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#D4E8F8");
        }
        // 按钮鼠标移出事件
        private void but_MouseLeave(object sender, EventArgs e){

            Button but = this.下拉按钮_but;
            Button but2 = this.button1;
            WinApiUtilsMet.AnimateWindow(but.Handle, 3000, 0x80000);
            // 边框颜色
            but.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#ADADAD");
            but.BackColor = ColorTranslator.FromHtml("#E1E1E1");
            // 边框颜色
            but2.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#ADADAD");
            but2.BackColor = ColorTranslator.FromHtml("#E1E1E1");
        }
        // 下拉按钮的点击事件
        private void but_Click(object sender, EventArgs e) {
            Button but = (Button)sender;
            int x = ControlsUtilsMet.LocationOnClient(this.下拉按钮_pan).X;
            
            if(this.button1.Name.Equals(but.Name)) {
                if(historicalPanel == null || historicalPanel.IsDisposed) { 
                  historicalPanel = ControlsUtilsMet.getHistoricalPanel(下拉按钮_but
                     , 下拉按钮_pan.Parent.FindForm().Controls
                     , false
                     , history
                     , 下拉按钮_but.Width + this.button1.Width, 23);
                    historicalPanel.Location = new Point(x, 下拉按钮_pan.Location.Y + 下拉按钮_pan.Height + 5);
                } else {
                    historicalPanel.Dispose();
                    historicalPanel = null;
                }
            }
        }
        // 按钮失去焦点事件
        private void but_Leave(object sender, EventArgs e) {
            Button but = (Button)sender;
            if(this.button1.Name.Equals(but.Name)) {
                if(historicalPanel != null && !historicalPanel.IsDisposed) { 
                    historicalPanel.Dispose();
                }
            }
        }
    }
}
