namespace GTF_STFM_COMM.Screen
{
    partial class SigCap
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
            this.PB_SIGIMAGE = new System.Windows.Forms.PictureBox();
            this.TXT_TXTDISPLAY = new System.Windows.Forms.TextBox();
            this.BTN_SIGNREQ = new MetroFramework.Controls.MetroButton();
            this.BTN_SIGNSAVE = new MetroFramework.Controls.MetroButton();
            this.BTN_SIGNCANCEL = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.PB_SIGIMAGE)).BeginInit();
            this.SuspendLayout();
            // 
            // PB_SIGIMAGE
            // 
            this.PB_SIGIMAGE.Location = new System.Drawing.Point(23, 63);
            this.PB_SIGIMAGE.Name = "PB_SIGIMAGE";
            this.PB_SIGIMAGE.Size = new System.Drawing.Size(331, 210);
            this.PB_SIGIMAGE.TabIndex = 0;
            this.PB_SIGIMAGE.TabStop = false;
            // 
            // TXT_TXTDISPLAY
            // 
            this.TXT_TXTDISPLAY.Location = new System.Drawing.Point(23, 292);
            this.TXT_TXTDISPLAY.Multiline = true;
            this.TXT_TXTDISPLAY.Name = "TXT_TXTDISPLAY";
            this.TXT_TXTDISPLAY.Size = new System.Drawing.Size(571, 150);
            this.TXT_TXTDISPLAY.TabIndex = 1;
            // 
            // BTN_SIGNREQ
            // 
            this.BTN_SIGNREQ.Location = new System.Drawing.Point(425, 63);
            this.BTN_SIGNREQ.Name = "BTN_SIGNREQ";
            this.BTN_SIGNREQ.Size = new System.Drawing.Size(169, 37);
            this.BTN_SIGNREQ.TabIndex = 2;
            this.BTN_SIGNREQ.Text = "Sign Request";
            this.BTN_SIGNREQ.UseSelectable = true;
            this.BTN_SIGNREQ.Click += new System.EventHandler(this.BTN_SIGNREQ_Click);
            // 
            // BTN_SIGNSAVE
            // 
            this.BTN_SIGNSAVE.Location = new System.Drawing.Point(425, 130);
            this.BTN_SIGNSAVE.Name = "BTN_SIGNSAVE";
            this.BTN_SIGNSAVE.Size = new System.Drawing.Size(169, 37);
            this.BTN_SIGNSAVE.TabIndex = 3;
            this.BTN_SIGNSAVE.Text = "Issue Receipt";
            this.BTN_SIGNSAVE.UseSelectable = true;
            this.BTN_SIGNSAVE.Click += new System.EventHandler(this.BTN_SIGNSAVE_Click);
            // 
            // BTN_SIGNCANCEL
            // 
            this.BTN_SIGNCANCEL.Location = new System.Drawing.Point(425, 200);
            this.BTN_SIGNCANCEL.Name = "BTN_SIGNCANCEL";
            this.BTN_SIGNCANCEL.Size = new System.Drawing.Size(169, 37);
            this.BTN_SIGNCANCEL.TabIndex = 4;
            this.BTN_SIGNCANCEL.Text = "Cancel";
            this.BTN_SIGNCANCEL.UseSelectable = true;
            this.BTN_SIGNCANCEL.Click += new System.EventHandler(this.BTN_SIGNCANCEL_Click);
            // 
            // SigCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 478);
            this.Controls.Add(this.BTN_SIGNCANCEL);
            this.Controls.Add(this.BTN_SIGNSAVE);
            this.Controls.Add(this.BTN_SIGNREQ);
            this.Controls.Add(this.TXT_TXTDISPLAY);
            this.Controls.Add(this.PB_SIGIMAGE);
            this.Name = "SigCap";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Sign Capture";
            this.Load += new System.EventHandler(this.SigCap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PB_SIGIMAGE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PB_SIGIMAGE;
        private System.Windows.Forms.TextBox TXT_TXTDISPLAY;
        private MetroFramework.Controls.MetroButton BTN_SIGNREQ;
        private MetroFramework.Controls.MetroButton BTN_SIGNSAVE;
        private MetroFramework.Controls.MetroButton BTN_SIGNCANCEL;
    }
}