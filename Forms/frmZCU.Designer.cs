
namespace iParking.Forms
{
    partial class frmZCU
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZCU));
            this.txtName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbAddress = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbCCU = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbCommunication = new System.Windows.Forms.ComboBox();
            this.cbComport = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbTMA_NAME = new System.Windows.Forms.ComboBox();
            this.cbTMA_INDEX = new System.Windows.Forms.ComboBox();
            this.panelTMA_Infor = new System.Windows.Forms.Panel();
            this.txtCam_TMA_Password = new System.Windows.Forms.TextBox();
            this.txtCamTMA_Username = new System.Windows.Forms.TextBox();
            this.txtCam_TMA_IP = new System.Windows.Forms.TextBox();
            this.txtCam_TMA_Port = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cb_TMA_Cam_Type = new System.Windows.Forms.ComboBox();
            this.panel_IP_COM_INFOR = new System.Windows.Forms.Panel();
            this.panelTMA_Infor.SuspendLayout();
            this.panel_IP_COM_INFOR.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 44);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(222, 23);
            this.txtName.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 15);
            this.label6.TabIndex = 40;
            this.label6.Text = "Name";
            // 
            // cbAddress
            // 
            this.cbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAddress.FormattingEnabled = true;
            this.cbAddress.Location = new System.Drawing.Point(109, 29);
            this.cbAddress.Name = "cbAddress";
            this.cbAddress.Size = new System.Drawing.Size(222, 23);
            this.cbAddress.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 32);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 38;
            this.label5.Text = "Address      ";
            // 
            // cbCCU
            // 
            this.cbCCU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCCU.FormattingEnabled = true;
            this.cbCCU.Location = new System.Drawing.Point(120, 131);
            this.cbCCU.Name = "cbCCU";
            this.cbCCU.Size = new System.Drawing.Size(222, 23);
            this.cbCCU.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 36;
            this.label1.Text = "CCUID";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::iParking.Properties.Resources.Actions_application_exit_icon_24;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(267, 392);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 59;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Image = global::iParking.Properties.Resources.icons8_save_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(199, 392);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 28);
            this.btnSave.TabIndex = 58;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 102);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(222, 23);
            this.txtDescription.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Description";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(120, 73);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(222, 23);
            this.txtCode.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
            this.label2.TabIndex = 29;
            this.label2.Text = "Code";
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(120, 15);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(222, 23);
            this.txtID.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 15);
            this.label4.TabIndex = 30;
            this.label4.Text = "ID";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(3, 61);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(58, 15);
            this.lblIP.TabIndex = 42;
            this.lblIP.Text = "IPAddress";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(1, 90);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 15);
            this.lblPort.TabIndex = 42;
            this.lblPort.Text = "Port";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 42;
            this.label9.Text = "Type";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(109, 58);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(222, 23);
            this.txtIP.TabIndex = 9;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(109, 87);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(222, 23);
            this.txtPort.TabIndex = 10;
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(120, 160);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(222, 23);
            this.cbType.TabIndex = 6;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(1, 119);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 15);
            this.label10.TabIndex = 43;
            this.label10.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(109, 116);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(222, 23);
            this.txtUsername.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 15);
            this.label11.TabIndex = 45;
            this.label11.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(109, 145);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(222, 23);
            this.txtPassword.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 15);
            this.label12.TabIndex = 47;
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
            this.cbCommunication.Location = new System.Drawing.Point(109, 0);
            this.cbCommunication.Name = "cbCommunication";
            this.cbCommunication.Size = new System.Drawing.Size(222, 23);
            this.cbCommunication.TabIndex = 7;
            this.cbCommunication.SelectedIndexChanged += new System.EventHandler(this.cbCommunication_SelectedIndexChanged);
            // 
            // cbComport
            // 
            this.cbComport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbComport.FormattingEnabled = true;
            this.cbComport.Location = new System.Drawing.Point(109, 58);
            this.cbComport.Name = "cbComport";
            this.cbComport.Size = new System.Drawing.Size(222, 23);
            this.cbComport.TabIndex = 48;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 15);
            this.label7.TabIndex = 49;
            this.label7.Text = "PGS_Server";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 15);
            this.label8.TabIndex = 50;
            this.label8.Text = "TMA_Index";
            // 
            // cbTMA_NAME
            // 
            this.cbTMA_NAME.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTMA_NAME.FormattingEnabled = true;
            this.cbTMA_NAME.Location = new System.Drawing.Point(121, 0);
            this.cbTMA_NAME.Name = "cbTMA_NAME";
            this.cbTMA_NAME.Size = new System.Drawing.Size(222, 23);
            this.cbTMA_NAME.TabIndex = 51;
            // 
            // cbTMA_INDEX
            // 
            this.cbTMA_INDEX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTMA_INDEX.FormattingEnabled = true;
            this.cbTMA_INDEX.Location = new System.Drawing.Point(121, 29);
            this.cbTMA_INDEX.Name = "cbTMA_INDEX";
            this.cbTMA_INDEX.Size = new System.Drawing.Size(222, 23);
            this.cbTMA_INDEX.TabIndex = 52;
            // 
            // panelTMA_Infor
            // 
            this.panelTMA_Infor.Controls.Add(this.txtCam_TMA_Password);
            this.panelTMA_Infor.Controls.Add(this.txtCamTMA_Username);
            this.panelTMA_Infor.Controls.Add(this.txtCam_TMA_IP);
            this.panelTMA_Infor.Controls.Add(this.txtCam_TMA_Port);
            this.panelTMA_Infor.Controls.Add(this.label17);
            this.panelTMA_Infor.Controls.Add(this.label16);
            this.panelTMA_Infor.Controls.Add(this.label15);
            this.panelTMA_Infor.Controls.Add(this.label14);
            this.panelTMA_Infor.Controls.Add(this.label13);
            this.panelTMA_Infor.Controls.Add(this.cb_TMA_Cam_Type);
            this.panelTMA_Infor.Controls.Add(this.cbTMA_NAME);
            this.panelTMA_Infor.Controls.Add(this.cbTMA_INDEX);
            this.panelTMA_Infor.Controls.Add(this.label7);
            this.panelTMA_Infor.Controls.Add(this.label8);
            this.panelTMA_Infor.Location = new System.Drawing.Point(-1, 189);
            this.panelTMA_Infor.Name = "panelTMA_Infor";
            this.panelTMA_Infor.Size = new System.Drawing.Size(343, 200);
            this.panelTMA_Infor.TabIndex = 53;
            // 
            // txtCam_TMA_Password
            // 
            this.txtCam_TMA_Password.Location = new System.Drawing.Point(121, 174);
            this.txtCam_TMA_Password.Name = "txtCam_TMA_Password";
            this.txtCam_TMA_Password.PasswordChar = '*';
            this.txtCam_TMA_Password.Size = new System.Drawing.Size(222, 23);
            this.txtCam_TMA_Password.TabIndex = 57;
            this.txtCam_TMA_Password.Text = "admin";
            this.txtCam_TMA_Password.UseSystemPasswordChar = true;
            // 
            // txtCamTMA_Username
            // 
            this.txtCamTMA_Username.Location = new System.Drawing.Point(121, 145);
            this.txtCamTMA_Username.Name = "txtCamTMA_Username";
            this.txtCamTMA_Username.Size = new System.Drawing.Size(222, 23);
            this.txtCamTMA_Username.TabIndex = 56;
            this.txtCamTMA_Username.Text = "admin";
            // 
            // txtCam_TMA_IP
            // 
            this.txtCam_TMA_IP.Location = new System.Drawing.Point(121, 88);
            this.txtCam_TMA_IP.Name = "txtCam_TMA_IP";
            this.txtCam_TMA_IP.PlaceholderText = "192.168.0.10";
            this.txtCam_TMA_IP.Size = new System.Drawing.Size(222, 23);
            this.txtCam_TMA_IP.TabIndex = 54;
            // 
            // txtCam_TMA_Port
            // 
            this.txtCam_TMA_Port.Location = new System.Drawing.Point(121, 117);
            this.txtCam_TMA_Port.Name = "txtCam_TMA_Port";
            this.txtCam_TMA_Port.Size = new System.Drawing.Size(222, 23);
            this.txtCam_TMA_Port.TabIndex = 55;
            this.txtCam_TMA_Port.Text = "80";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 177);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 15);
            this.label17.TabIndex = 58;
            this.label17.Text = "Password";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(13, 148);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 15);
            this.label16.TabIndex = 57;
            this.label16.Text = "Username";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 120);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 15);
            this.label15.TabIndex = 56;
            this.label15.Text = "Port";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 91);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 15);
            this.label14.TabIndex = 55;
            this.label14.Text = "IP";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 62);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(75, 15);
            this.label13.TabIndex = 54;
            this.label13.Text = "Camera Type";
            // 
            // cb_TMA_Cam_Type
            // 
            this.cb_TMA_Cam_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TMA_Cam_Type.FormattingEnabled = true;
            this.cb_TMA_Cam_Type.Items.AddRange(new object[] {
            "Hanse"});
            this.cb_TMA_Cam_Type.Location = new System.Drawing.Point(121, 59);
            this.cb_TMA_Cam_Type.Name = "cb_TMA_Cam_Type";
            this.cb_TMA_Cam_Type.Size = new System.Drawing.Size(222, 23);
            this.cb_TMA_Cam_Type.TabIndex = 53;
            // 
            // panel_IP_COM_INFOR
            // 
            this.panel_IP_COM_INFOR.Controls.Add(this.cbCommunication);
            this.panel_IP_COM_INFOR.Controls.Add(this.label5);
            this.panel_IP_COM_INFOR.Controls.Add(this.cbComport);
            this.panel_IP_COM_INFOR.Controls.Add(this.cbAddress);
            this.panel_IP_COM_INFOR.Controls.Add(this.lblIP);
            this.panel_IP_COM_INFOR.Controls.Add(this.label12);
            this.panel_IP_COM_INFOR.Controls.Add(this.lblPort);
            this.panel_IP_COM_INFOR.Controls.Add(this.txtPassword);
            this.panel_IP_COM_INFOR.Controls.Add(this.txtIP);
            this.panel_IP_COM_INFOR.Controls.Add(this.label11);
            this.panel_IP_COM_INFOR.Controls.Add(this.txtPort);
            this.panel_IP_COM_INFOR.Controls.Add(this.txtUsername);
            this.panel_IP_COM_INFOR.Controls.Add(this.label10);
            this.panel_IP_COM_INFOR.Location = new System.Drawing.Point(11, 189);
            this.panel_IP_COM_INFOR.Name = "panel_IP_COM_INFOR";
            this.panel_IP_COM_INFOR.Size = new System.Drawing.Size(331, 168);
            this.panel_IP_COM_INFOR.TabIndex = 54;
            // 
            // frmZCU
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 423);
            this.Controls.Add(this.panelTMA_Infor);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.cbType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbCCU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel_IP_COM_INFOR);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmZCU";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmZCU";
            this.Load += new System.EventHandler(this.frmZCU_Load);
            this.panelTMA_Infor.ResumeLayout(false);
            this.panelTMA_Infor.PerformLayout();
            this.panel_IP_COM_INFOR.ResumeLayout(false);
            this.panel_IP_COM_INFOR.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbCCU;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbCommunication;
        private System.Windows.Forms.ComboBox cbComport;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbTMA_NAME;
        private System.Windows.Forms.ComboBox cbTMA_INDEX;
        private System.Windows.Forms.Panel panelTMA_Infor;
        private System.Windows.Forms.Panel panel_IP_COM_INFOR;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtCam_TMA_Password;
        private System.Windows.Forms.TextBox txtCamTMA_Username;
        private System.Windows.Forms.TextBox txtCam_TMA_IP;
        private System.Windows.Forms.TextBox txtCam_TMA_Port;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cb_TMA_Cam_Type;
    }
}