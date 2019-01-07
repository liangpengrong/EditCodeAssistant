using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PublicMethodLibrary;

namespace ProgramOption
{
    public partial class ControlTree : UserControl
    {
        public ControlTree()
        {
            InitializeComponent();
        }

        // panel重绘事件
        private void ControPanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = (Panel)sender;
            //ControlsUtilsMet.paintConSide(panel.CreateGraphics(),panel.ClientRectangle,ButtonBorderStyle.Solid
            //,0, 1, 0, 0
            //,Color.Transparent,Color.FromArgb(100,100,100),Color.FromArgb(100,100,100),Color.FromArgb(100,100,100));
        }
    }
}
