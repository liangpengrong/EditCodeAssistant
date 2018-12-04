namespace PubControlLibrary
{
    partial class EditorArea
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTextBox = new System.Windows.Forms.TextBox();
            // this.SuspendLayout();
            // 
            // mainTextBox
            // 
            this.mainTextBox.AllowDrop = true;
            this.mainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mainTextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mainTextBox.HideSelection = false;
            this.mainTextBox.Location = new System.Drawing.Point(0, 0);
            this.mainTextBox.MaxLength = 999999999;
            this.mainTextBox.Multiline = true;
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mainTextBox.Size = new System.Drawing.Size(289, 254);
            this.mainTextBox.TabIndex = 0;
            this.mainTextBox.WordWrap = false;
            // 
            // EditorArea
            // 
            //this.BackColor = System.Drawing.Color.White;
            //this.Controls.Add(this.mainTextBox);
            //this.Name = "EditorArea";
            //this.Size = new System.Drawing.Size(292, 273);
            //this.ResumeLayout(false);
            //this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox mainTextBox;

    }
}

