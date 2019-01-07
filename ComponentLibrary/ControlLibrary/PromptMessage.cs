using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using PublicMethodLibrary;

namespace ComponentLibrary {
    /// <summary>
    /// 疑问消息提示控件
    /// </summary>
    public partial class PromptMessage : Component {
        /// <summary>
        /// 消息控件
        /// </summary>
        public PromptMessage(string mess){
            InitializeComponent();
        }

        public static Button getMessBut(string name, string mess) { 
            Button but = new Button();
            but.Name = name;
            but.Size = new Size(15,15);
            but.FlatStyle = FlatStyle.Flat;
            but.BackColor = Color.Transparent;
            but.ForeColor = but.BackColor;
            but.TabStop = false;
            but.FlatAppearance.BorderColor = Color.White;
            but.FlatAppearance.BorderSize = 0;
            but.FlatAppearance.CheckedBackColor = Color.Transparent;
            but.FlatAppearance.MouseDownBackColor = Color.Transparent;
            but.FlatAppearance.MouseOverBackColor = Color.Transparent;
            but.Image = Properties.Resources.疑问;
            // 鼠标移入事件
            but.MouseEnter += (object sender, EventArgs e) =>{ 
                Button  b = (Button)sender;
                ToolTip toolTip = ControlsUtilsMet.getControlMessTip(b, mess,
                but.Width +2, -4, 10000, Color.White, Color.Black);
                b.Tag = toolTip;
            };
            // 鼠标移出事件
            but.MouseLeave += (object sender, EventArgs e) =>{
                Button  b = (Button)sender;
                if(b.Tag != null) {
                    ToolTip toolTip = (ToolTip)b.Tag;
                    toolTip.Dispose();
                    toolTip = null;
                    b.Tag = null;
                }
            };
            return but;
            
        }
    }
}
