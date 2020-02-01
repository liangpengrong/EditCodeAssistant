namespace EditCodeAssistant
{
    partial class RootDisplayForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RootDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(384, 273);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(400, 85);
            this.Name = "RootDisplayForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RootDisplayForm";
            this.Activated += new System.EventHandler(this.RootDisplayForm_Activated);
            this.Deactivate += new System.EventHandler(this.RootDisplayForm_Deactivate);
            this.Load += new System.EventHandler(this.RootDisplayForm_Load);
            this.VisibleChanged += new System.EventHandler(this.RootDisplayForm_VisibleChanged);
            this.Resize += new System.EventHandler(this.RootDisplayForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}