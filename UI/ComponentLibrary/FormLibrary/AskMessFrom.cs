using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UI.ComponentLibrary.FormLibrary {
    /// <summary>
    /// 消息提示窗体
    /// </summary>
    public partial class AskMessFrom : Form {
        public AskMessFrom() {
            InitializeComponent();
        }

        private void 取消_but_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
