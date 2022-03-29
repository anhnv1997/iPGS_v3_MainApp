
namespace iParking.Forms
{
    partial class frmReport
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
            this.ucPanigationsql1 = new iParking.UserControls.ucPanigationSQL();
            this.SuspendLayout();
            // 
            // ucPanigationsql1
            // 
            this.ucPanigationsql1.Location = new System.Drawing.Point(-2, 9);
            this.ucPanigationsql1.Margin = new System.Windows.Forms.Padding(0);
            this.ucPanigationsql1.Name = "ucPanigationsql1";
            this.ucPanigationsql1.Size = new System.Drawing.Size(682, 442);
            this.ucPanigationsql1.TabIndex = 0;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 450);
            this.Controls.Add(this.ucPanigationsql1);
            this.Name = "frmReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmReport";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.ucPanigationSQL ucPanigationsql1;
    }
}