namespace PubControlLibrary
{
    partial class CharsStatistics
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
            this.统计信息G = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // 统计信息G
            // 
            this.统计信息G.Location = new System.Drawing.Point(8, 1);
            this.统计信息G.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.统计信息G.Name = "统计信息G";
            this.统计信息G.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.统计信息G.Size = new System.Drawing.Size(245, 256);
            this.统计信息G.TabIndex = 0;
            this.统计信息G.TabStop = false;
            this.统计信息G.Text = "统计信息";
            // 
            // CharsStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 260);
            this.Controls.Add(this.统计信息G);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharsStatistics";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "字数统计";
            this.Load += new System.EventHandler(this.CharsStatistics_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox 统计信息G;
    }
}