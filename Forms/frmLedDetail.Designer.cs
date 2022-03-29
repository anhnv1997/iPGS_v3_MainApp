
namespace iParking.Forms
{
    partial class frmLedDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedDetail));
            this.label3 = new System.Windows.Forms.Label();
            this.cbLedDevice = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUnselect = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.clbInUsedZone = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clbUnUsedZone = new System.Windows.Forms.CheckedListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnselectAll = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(11, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Led Device";
            // 
            // cbLedDevice
            // 
            this.cbLedDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLedDevice.FormattingEnabled = true;
            this.cbLedDevice.Location = new System.Drawing.Point(86, 4);
            this.cbLedDevice.Name = "cbLedDevice";
            this.cbLedDevice.Size = new System.Drawing.Size(399, 23);
            this.cbLedDevice.TabIndex = 42;
            this.cbLedDevice.SelectedIndexChanged += new System.EventHandler(this.cbLedDevice_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::iParking.Properties.Resources.Actions_application_exit_icon_24;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.button2.Location = new System.Drawing.Point(412, 243);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 30);
            this.button2.TabIndex = 41;
            this.button2.Text = "Cancel";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::iParking.Properties.Resources.icons8_save_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(344, 243);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 30);
            this.btnSave.TabIndex = 40;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUnselect
            // 
            this.btnUnselect.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselect.Image")));
            this.btnUnselect.Location = new System.Drawing.Point(218, 169);
            this.btnUnselect.Name = "btnUnselect";
            this.btnUnselect.Size = new System.Drawing.Size(60, 31);
            this.btnUnselect.TabIndex = 38;
            this.btnUnselect.UseVisualStyleBackColor = true;
            this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
            this.btnUnselect.MouseHover += new System.EventHandler(this.btnUnselect_MouseHover);
            // 
            // btnSelect
            // 
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.Location = new System.Drawing.Point(219, 90);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(60, 31);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            this.btnSelect.MouseHover += new System.EventHandler(this.btnSelect_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(285, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "Inused";
            // 
            // clbInUsedZone
            // 
            this.clbInUsedZone.CheckOnClick = true;
            this.clbInUsedZone.FormattingEnabled = true;
            this.clbInUsedZone.Location = new System.Drawing.Point(285, 53);
            this.clbInUsedZone.Name = "clbInUsedZone";
            this.clbInUsedZone.Size = new System.Drawing.Size(200, 184);
            this.clbInUsedZone.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 15);
            this.label1.TabIndex = 37;
            this.label1.Text = "Unused";
            // 
            // clbUnUsedZone
            // 
            this.clbUnUsedZone.CheckOnClick = true;
            this.clbUnUsedZone.FormattingEnabled = true;
            this.clbUnUsedZone.Location = new System.Drawing.Point(12, 53);
            this.clbUnUsedZone.Name = "clbUnUsedZone";
            this.clbUnUsedZone.Size = new System.Drawing.Size(200, 184);
            this.clbUnUsedZone.TabIndex = 35;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectAll.Image")));
            this.btnSelectAll.Location = new System.Drawing.Point(219, 53);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(60, 31);
            this.btnSelectAll.TabIndex = 39;
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            this.btnSelectAll.MouseHover += new System.EventHandler(this.btnSelectAll_MouseHover);
            // 
            // btnUnselectAll
            // 
            this.btnUnselectAll.Image = ((System.Drawing.Image)(resources.GetObject("btnUnselectAll.Image")));
            this.btnUnselectAll.Location = new System.Drawing.Point(218, 206);
            this.btnUnselectAll.Name = "btnUnselectAll";
            this.btnUnselectAll.Size = new System.Drawing.Size(60, 31);
            this.btnUnselectAll.TabIndex = 38;
            this.btnUnselectAll.UseVisualStyleBackColor = true;
            this.btnUnselectAll.Click += new System.EventHandler(this.btnUnselectAll_Click);
            this.btnUnselectAll.MouseHover += new System.EventHandler(this.btnUnselectAll_MouseHover);
            // 
            // frmLedDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 278);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbLedDevice);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnUnselectAll);
            this.Controls.Add(this.btnUnselect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.clbInUsedZone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clbUnUsedZone);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLedDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLedDetail";
            this.Load += new System.EventHandler(this.frmLedDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbLedDevice;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUnselect;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox clbInUsedZone;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox clbUnUsedZone;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnselectAll;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}