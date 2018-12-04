namespace PubControlLibrary
{
    partial class RowGoToForm
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
            this.components = new System.ComponentModel.Container();
            this.ok_but = new System.Windows.Forms.Button();
            this.exc_but = new System.Windows.Forms.Button();
            this.行号L = new System.Windows.Forms.Label();
            this.行号Num_L = new System.Windows.Forms.Label();
            this.行号T = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ok_but
            // 
            this.ok_but.Location = new System.Drawing.Point(71, 86);
            this.ok_but.Name = "ok_but";
            this.ok_but.Size = new System.Drawing.Size(75, 27);
            this.ok_but.TabIndex = 1;
            this.ok_but.Text = "确定";
            this.ok_but.UseVisualStyleBackColor = true;
            this.ok_but.Click += new System.EventHandler(this.ok_but_Click);
            // 
            // exc_but
            // 
            this.exc_but.Location = new System.Drawing.Point(179, 86);
            this.exc_but.Name = "exc_but";
            this.exc_but.Size = new System.Drawing.Size(75, 27);
            this.exc_but.TabIndex = 2;
            this.exc_but.Text = "取消";
            this.exc_but.UseVisualStyleBackColor = true;
            this.exc_but.Click += new System.EventHandler(this.exc_but_Click);
            // 
            // 行号L
            // 
            this.行号L.AutoSize = true;
            this.行号L.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.行号L.Location = new System.Drawing.Point(20, 16);
            this.行号L.Name = "行号L";
            this.行号L.Size = new System.Drawing.Size(42, 14);
            this.行号L.TabIndex = 2;
            this.行号L.Text = "行号:";
            // 
            // 行号Num_L
            // 
            this.行号Num_L.AutoSize = true;
            this.行号Num_L.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.行号Num_L.Location = new System.Drawing.Point(60, 16);
            this.行号Num_L.Name = "行号Num_L";
            this.行号Num_L.Size = new System.Drawing.Size(63, 14);
            this.行号Num_L.TabIndex = 3;
            this.行号Num_L.Text = "11111111";
            // 
            // 行号T
            // 
            this.行号T.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.行号T.Location = new System.Drawing.Point(25, 42);
            this.行号T.Name = "行号T";
            this.行号T.Size = new System.Drawing.Size(243, 23);
            this.行号T.TabIndex = 0;
            this.行号T.Text = "1";
            this.行号T.TextChanged += new System.EventHandler(this.行号T_TextChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkRate = 0;
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // RowGoToForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 122);
            this.Controls.Add(this.行号T);
            this.Controls.Add(this.行号Num_L);
            this.Controls.Add(this.行号L);
            this.Controls.Add(this.exc_but);
            this.Controls.Add(this.ok_but);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RowGoToForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "转到行";
            this.Load += new System.EventHandler(this.RowGoToForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ok_but;
        private System.Windows.Forms.Button exc_but;
        private System.Windows.Forms.Label 行号L;
        private System.Windows.Forms.Label 行号Num_L;
        public System.Windows.Forms.TextBox 行号T;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}