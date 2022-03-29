
namespace iParking.Forms
{
    partial class frmOutputDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOutputDetail));
            this.clbUnUsedZone = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clbInUsedZone = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbOutputDevice = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnUnselect = new System.Windows.Forms.Button();
            this.cbRelayIndex = new System.Windows.Forms.ComboBox();
            this.lblRelayIndex = new System.Windows.Forms.Label();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // clbUnUsedZone
            // 
            this.clbUnUsedZone.CheckOnClick = true;
            this.clbUnUsedZone.FormattingEnabled = true;
            this.clbUnUsedZone.Location = new System.Drawing.Point(12, 95);
            this.clbUnUsedZone.Name = "clbUnUsedZone";
            this.clbUnUsedZone.Size = new System.Drawing.Size(200, 184);
            this.clbUnUsedZone.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Unused";
            // 
            // clbInUsedZone
            // 
            this.clbInUsedZone.CheckOnClick = true;
            this.clbInUsedZone.FormattingEnabled = true;
            this.clbInUsedZone.Location = new System.Drawing.Point(285, 95);
            this.clbInUsedZone.Name = "clbInUsedZone";
            this.clbInUsedZone.Size = new System.Drawing.Size(200, 184);
            this.clbInUsedZone.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(285, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Inused";
            // 
            // btnSelect
            // 
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(218, 132);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(60, 31);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            this.btnSelect.MouseHover += new System.EventHandler(this.btnSelect_MouseHover);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::iParking.Properties.Resources.Actions_application_exit_icon_24;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(412, 285);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 31;
            this.button2.Text = "Cancel";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::iParking.Properties.Resources.icons8_save_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(344, 285);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 30);
            this.btnSave.TabIndex = 30;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbOutputDevice
            // 
            this.cbOutputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputDevice.FormattingEnabled = true;
            this.cbOutputDevice.Location = new System.Drawing.Point(106, 11);
            this.cbOutputDevice.Name = "cbOutputDevice";
            this.cbOutputDevice.Size = new System.Drawing.Size(379, 23);
            this.cbOutputDevice.TabIndex = 32;
            this.cbOutputDevice.SelectedIndexChanged += new System.EventHandler(this.cbOutputDevice_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(11, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 15);
            this.label3.TabIndex = 33;
            this.label3.Text = "Output Device";
            // 
            // btnUnselect
            // 
            this.btnUnselect.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselect.Image")));
            this.btnUnselect.Location = new System.Drawing.Point(218, 211);
            this.btnUnselect.Name = "btnUnselect";
            this.btnUnselect.Size = new System.Drawing.Size(60, 31);
            this.btnUnselect.TabIndex = 2;
            this.btnUnselect.UseVisualStyleBackColor = true;
            this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
            this.btnUnselect.MouseHover += new System.EventHandler(this.btnUnselect_MouseHover);
            // 
            // cbRelayIndex
            // 
            this.cbRelayIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRelayIndex.FormattingEnabled = true;
            this.cbRelayIndex.Location = new System.Drawing.Point(106, 40);
            this.cbRelayIndex.Name = "cbRelayIndex";
            this.cbRelayIndex.Size = new System.Drawing.Size(379, 23);
            this.cbRelayIndex.TabIndex = 35;
            this.cbRelayIndex.SelectedIndexChanged += new System.EventHandler(this.cbRelayIndex_SelectedIndexChanged);
            // 
            // lblRelayIndex
            // 
            this.lblRelayIndex.AutoSize = true;
            this.lblRelayIndex.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblRelayIndex.Location = new System.Drawing.Point(11, 43);
            this.lblRelayIndex.Name = "lblRelayIndex";
            this.lblRelayIndex.Size = new System.Drawing.Size(72, 15);
            this.lblRelayIndex.TabIndex = 34;
            this.lblRelayIndex.Text = "Relay Index";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(219, 95);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(60, 31);
            this.btnSelectAll.TabIndex = 36;
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            this.btnSelectAll.MouseHover += new System.EventHandler(this.btnSelectAll_MouseHover);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselectAll.Image")));
            this.btnUnselectAll.Location = new System.Drawing.Point(218, 248);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(60, 31);
            this.btnUnselectAll.TabIndex = 36;
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            this.btnUnselectAll.MouseHover += new System.EventHandler(this.btnUnselectAll_MouseHover);
            // 
            // frmOutputDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 325);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.cbRelayIndex);
            this.Controls.Add(this.lblRelayIndex);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOutputDevice);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUnselect);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clbInUsedZone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clbUnUsedZone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutputDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Output Detail";
            this.Load += new System.EventHandler(this.frmOutputDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbUnUsedZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbInUsedZone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cbOutputDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUnselect;
        private System.Windows.Forms.ComboBox cbRelayIndex;
        private System.Windows.Forms.Label lblRelayIndex;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}