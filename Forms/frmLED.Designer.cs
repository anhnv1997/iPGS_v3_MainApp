
namespace iParking.Forms
{
    partial class frmLED
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLED));
            this.cbLedType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.lblIP = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbAddress = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbArrowDirection = new System.Windows.Forms.ComboBox();
            this.cbColor = new System.Windows.Forms.ComboBox();
            this.cbZeroColor = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbCommunication = new System.Windows.Forms.ComboBox();
            this.cbComport = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbLedType
            // 
            this.cbLedType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLedType.FormattingEnabled = true;
            this.cbLedType.Location = new System.Drawing.Point(119, 125);
            this.cbLedType.Name = "cbLedType";
            this.cbLedType.Size = new System.Drawing.Size(261, 23);
            this.cbLedType.TabIndex = 41;
            this.cbLedType.SelectedIndexChanged += new System.EventHandler(this.cbLedType_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 128);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 15);
            this.label7.TabIndex = 49;
            this.label7.Text = "Type";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(119, 37);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(261, 23);
            this.txtName.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Name";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Image = global::iParking.Properties.Resources.Actions_application_exit_icon_24;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(305, 358);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 28);
            this.button2.TabIndex = 47;
            this.button2.Text = "Cancel";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::iParking.Properties.Resources.icons8_save_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(237, 358);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 28);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(118, 213);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(261, 23);
            this.txtPort.TabIndex = 40;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(119, 95);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(261, 23);
            this.txtDescription.TabIndex = 38;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(6, 216);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 15);
            this.lblPort.TabIndex = 42;
            this.lblPort.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 43;
            this.label3.Text = "Description";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(118, 184);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(261, 23);
            this.txtIP.TabIndex = 39;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(6, 187);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(58, 15);
            this.lblIP.TabIndex = 45;
            this.lblIP.Text = "IPAddress";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(119, 66);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(261, 23);
            this.txtCode.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 46;
            this.label2.Text = "Code";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(119, 8);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(261, 23);
            this.txtID.TabIndex = 35;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 15);
            this.label1.TabIndex = 47;
            this.label1.Text = "ID";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 51;
            this.label8.Text = "Address";
            // 
            // cbAddress
            // 
            this.cbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddress.FormattingEnabled = true;
            this.cbAddress.Location = new System.Drawing.Point(119, 242);
            this.cbAddress.Name = "cbAddress";
            this.cbAddress.Size = new System.Drawing.Size(261, 23);
            this.cbAddress.TabIndex = 42;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 274);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 15);
            this.label9.TabIndex = 51;
            this.label9.Text = "Arrow";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 332);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 15);
            this.label10.TabIndex = 52;
            this.label10.Text = "Zero Color";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 303);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 15);
            this.label11.TabIndex = 52;
            this.label11.Text = "Color";
            // 
            // cbArrowDirection
            // 
            this.cbArrowDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbArrowDirection.FormattingEnabled = true;
            this.cbArrowDirection.Location = new System.Drawing.Point(119, 271);
            this.cbArrowDirection.Name = "cbArrowDirection";
            this.cbArrowDirection.Size = new System.Drawing.Size(261, 23);
            this.cbArrowDirection.TabIndex = 43;
            // 
            // cbColor
            // 
            this.cbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColor.FormattingEnabled = true;
            this.cbColor.Location = new System.Drawing.Point(119, 300);
            this.cbColor.Name = "cbColor";
            this.cbColor.Size = new System.Drawing.Size(261, 23);
            this.cbColor.TabIndex = 44;
            // 
            // cbZeroColor
            // 
            this.cbZeroColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbZeroColor.FormattingEnabled = true;
            this.cbZeroColor.Location = new System.Drawing.Point(118, 329);
            this.cbZeroColor.Name = "cbZeroColor";
            this.cbZeroColor.Size = new System.Drawing.Size(261, 23);
            this.cbZeroColor.TabIndex = 45;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 158);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 15);
            this.label12.TabIndex = 53;
            this.label12.Text = "Communication";
            // 
            // cbCommunication
            // 
            this.cbCommunication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommunication.FormattingEnabled = true;
            this.cbCommunication.Items.AddRange(new object[] {
            "COM",
            "TCP_IP",
            "USB"});
            this.cbCommunication.Location = new System.Drawing.Point(119, 155);
            this.cbCommunication.Name = "cbCommunication";
            this.cbCommunication.Size = new System.Drawing.Size(261, 23);
            this.cbCommunication.TabIndex = 54;
            this.cbCommunication.SelectedIndexChanged += new System.EventHandler(this.cbCommunicaition_SelectedIndexChanged);
            // 
            // cbComport
            // 
            this.cbComport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComport.FormattingEnabled = true;
            this.cbComport.Location = new System.Drawing.Point(118, 184);
            this.cbComport.Name = "cbComport";
            this.cbComport.Size = new System.Drawing.Size(262, 23);
            this.cbComport.TabIndex = 55;
            // 
            // frmLED
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 392);
            this.Controls.Add(this.cbComport);
            this.Controls.Add(this.cbCommunication);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbZeroColor);
            this.Controls.Add(this.cbColor);
            this.Controls.Add(this.cbArrowDirection);
            this.Controls.Add(this.cbAddress);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cbLedType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.lblIP);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLED";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LED";
            this.Load += new System.EventHandler(this.frmLED_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLedType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbArrowDirection;
        private System.Windows.Forms.ComboBox cbColor;
        private System.Windows.Forms.ComboBox cbZeroColor;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbCommunication;
        private System.Windows.Forms.ComboBox cbComport;
    }
}