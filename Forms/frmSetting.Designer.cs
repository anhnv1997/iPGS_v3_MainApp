
namespace iParking.Forms
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.tabOption = new System.Windows.Forms.TabControl();
            this.tabFloor = new System.Windows.Forms.TabPage();
            this.ucFloor1 = new iParking.UserControls.ucFloor();
            this.tabGroup = new System.Windows.Forms.TabPage();
            this.ucGroup1 = new iParking.UserControls.ucGroup();
            this.tabMAP = new System.Windows.Forms.TabPage();
            this.ucmap1 = new iParking.UserControls.ucMAP();
            this.tabTMA = new System.Windows.Forms.TabPage();
            this.uctma1 = new iParking.UserControls.ucTMA();
            this.tabZCU = new System.Windows.Forms.TabPage();
            this.uczcu1 = new iParking.UserControls.ucZCU();
            this.tabZONE = new System.Windows.Forms.TabPage();
            this.ucZone1 = new iParking.UserControls.ucZone();
            this.tabOutput = new System.Windows.Forms.TabPage();
            this.ucOutput1 = new iParking.UserControls.ucOutput();
            this.tabLED = new System.Windows.Forms.TabPage();
            this.ucLed1 = new iParking.UserControls.ucLed();
            this.tabVehicleType = new System.Windows.Forms.TabPage();
            this.ucVehicleType1 = new iParking.UserControls.ucVehicleType();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabOption_ = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.numOrderStateHoldTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numRefreshStateTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabOption.SuspendLayout();
            this.tabFloor.SuspendLayout();
            this.tabGroup.SuspendLayout();
            this.tabMAP.SuspendLayout();
            this.tabTMA.SuspendLayout();
            this.tabZCU.SuspendLayout();
            this.tabZONE.SuspendLayout();
            this.tabOutput.SuspendLayout();
            this.tabLED.SuspendLayout();
            this.tabVehicleType.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabOption_.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderStateHoldTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshStateTime)).BeginInit();
            this.SuspendLayout();
            // 
            // tabOption
            // 
            this.tabOption.Controls.Add(this.tabFloor);
            this.tabOption.Controls.Add(this.tabGroup);
            this.tabOption.Controls.Add(this.tabMAP);
            this.tabOption.Controls.Add(this.tabTMA);
            this.tabOption.Controls.Add(this.tabZCU);
            this.tabOption.Controls.Add(this.tabZONE);
            this.tabOption.Controls.Add(this.tabOutput);
            this.tabOption.Controls.Add(this.tabLED);
            this.tabOption.Controls.Add(this.tabVehicleType);
            this.tabOption.Controls.Add(this.tabOption_);
            this.tabOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOption.Location = new System.Drawing.Point(3, 3);
            this.tabOption.Name = "tabOption";
            this.tabOption.SelectedIndex = 0;
            this.tabOption.Size = new System.Drawing.Size(601, 395);
            this.tabOption.TabIndex = 0;
            this.tabOption.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabFloor
            // 
            this.tabFloor.Controls.Add(this.ucFloor1);
            this.tabFloor.Location = new System.Drawing.Point(4, 24);
            this.tabFloor.Name = "tabFloor";
            this.tabFloor.Padding = new System.Windows.Forms.Padding(3);
            this.tabFloor.Size = new System.Drawing.Size(593, 367);
            this.tabFloor.TabIndex = 1;
            this.tabFloor.Text = "Floor";
            this.tabFloor.UseVisualStyleBackColor = true;
            // 
            // ucFloor1
            // 
            this.ucFloor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFloor1.Location = new System.Drawing.Point(3, 3);
            this.ucFloor1.Name = "ucFloor1";
            this.ucFloor1.Size = new System.Drawing.Size(587, 361);
            this.ucFloor1.TabIndex = 0;
            // 
            // tabGroup
            // 
            this.tabGroup.Controls.Add(this.ucGroup1);
            this.tabGroup.Location = new System.Drawing.Point(4, 24);
            this.tabGroup.Name = "tabGroup";
            this.tabGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tabGroup.Size = new System.Drawing.Size(593, 367);
            this.tabGroup.TabIndex = 2;
            this.tabGroup.Text = "Group";
            this.tabGroup.UseVisualStyleBackColor = true;
            // 
            // ucGroup1
            // 
            this.ucGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGroup1.Location = new System.Drawing.Point(3, 3);
            this.ucGroup1.Name = "ucGroup1";
            this.ucGroup1.Size = new System.Drawing.Size(587, 361);
            this.ucGroup1.TabIndex = 0;
            // 
            // tabMAP
            // 
            this.tabMAP.Controls.Add(this.ucmap1);
            this.tabMAP.Location = new System.Drawing.Point(4, 24);
            this.tabMAP.Name = "tabMAP";
            this.tabMAP.Size = new System.Drawing.Size(593, 367);
            this.tabMAP.TabIndex = 3;
            this.tabMAP.Text = "MAP";
            this.tabMAP.UseVisualStyleBackColor = true;
            // 
            // ucmap1
            // 
            this.ucmap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucmap1.Location = new System.Drawing.Point(0, 0);
            this.ucmap1.Name = "ucmap1";
            this.ucmap1.Size = new System.Drawing.Size(593, 367);
            this.ucmap1.TabIndex = 0;
            // 
            // tabTMA
            // 
            this.tabTMA.Controls.Add(this.uctma1);
            this.tabTMA.Location = new System.Drawing.Point(4, 24);
            this.tabTMA.Name = "tabTMA";
            this.tabTMA.Size = new System.Drawing.Size(593, 367);
            this.tabTMA.TabIndex = 10;
            this.tabTMA.Text = "TMA Server";
            this.tabTMA.UseVisualStyleBackColor = true;
            // 
            // uctma1
            // 
            this.uctma1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uctma1.Location = new System.Drawing.Point(0, 0);
            this.uctma1.Name = "uctma1";
            this.uctma1.Size = new System.Drawing.Size(593, 367);
            this.uctma1.TabIndex = 0;
            // 
            // tabZCU
            // 
            this.tabZCU.Controls.Add(this.uczcu1);
            this.tabZCU.Location = new System.Drawing.Point(4, 24);
            this.tabZCU.Name = "tabZCU";
            this.tabZCU.Size = new System.Drawing.Size(593, 367);
            this.tabZCU.TabIndex = 5;
            this.tabZCU.Text = "ZCU";
            this.tabZCU.UseVisualStyleBackColor = true;
            // 
            // uczcu1
            // 
            this.uczcu1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uczcu1.Location = new System.Drawing.Point(0, 0);
            this.uczcu1.Name = "uczcu1";
            this.uczcu1.Size = new System.Drawing.Size(593, 367);
            this.uczcu1.TabIndex = 0;
            // 
            // tabZONE
            // 
            this.tabZONE.Controls.Add(this.ucZone1);
            this.tabZONE.Location = new System.Drawing.Point(4, 24);
            this.tabZONE.Name = "tabZONE";
            this.tabZONE.Size = new System.Drawing.Size(593, 367);
            this.tabZONE.TabIndex = 6;
            this.tabZONE.Text = "Zone";
            this.tabZONE.UseVisualStyleBackColor = true;
            // 
            // ucZone1
            // 
            this.ucZone1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucZone1.Location = new System.Drawing.Point(0, 0);
            this.ucZone1.Name = "ucZone1";
            this.ucZone1.Size = new System.Drawing.Size(593, 367);
            this.ucZone1.TabIndex = 0;
            // 
            // tabOutput
            // 
            this.tabOutput.Controls.Add(this.ucOutput1);
            this.tabOutput.Location = new System.Drawing.Point(4, 24);
            this.tabOutput.Name = "tabOutput";
            this.tabOutput.Size = new System.Drawing.Size(593, 367);
            this.tabOutput.TabIndex = 7;
            this.tabOutput.Text = "Output";
            this.tabOutput.UseVisualStyleBackColor = true;
            // 
            // ucOutput1
            // 
            this.ucOutput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOutput1.Location = new System.Drawing.Point(0, 0);
            this.ucOutput1.Name = "ucOutput1";
            this.ucOutput1.Size = new System.Drawing.Size(593, 367);
            this.ucOutput1.TabIndex = 0;
            // 
            // tabLED
            // 
            this.tabLED.Controls.Add(this.ucLed1);
            this.tabLED.Location = new System.Drawing.Point(4, 24);
            this.tabLED.Name = "tabLED";
            this.tabLED.Size = new System.Drawing.Size(593, 367);
            this.tabLED.TabIndex = 8;
            this.tabLED.Text = "LED";
            this.tabLED.UseVisualStyleBackColor = true;
            // 
            // ucLed1
            // 
            this.ucLed1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLed1.Location = new System.Drawing.Point(0, 0);
            this.ucLed1.Name = "ucLed1";
            this.ucLed1.Size = new System.Drawing.Size(593, 367);
            this.ucLed1.TabIndex = 0;
            // 
            // tabVehicleType
            // 
            this.tabVehicleType.Controls.Add(this.ucVehicleType1);
            this.tabVehicleType.Location = new System.Drawing.Point(4, 24);
            this.tabVehicleType.Name = "tabVehicleType";
            this.tabVehicleType.Size = new System.Drawing.Size(593, 367);
            this.tabVehicleType.TabIndex = 9;
            this.tabVehicleType.Text = "Vehicle Type";
            this.tabVehicleType.UseVisualStyleBackColor = true;
            // 
            // ucVehicleType1
            // 
            this.ucVehicleType1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVehicleType1.Location = new System.Drawing.Point(0, 0);
            this.ucVehicleType1.Name = "ucVehicleType1";
            this.ucVehicleType1.Size = new System.Drawing.Size(593, 367);
            this.ucVehicleType1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tabOption, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(607, 431);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 401);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(607, 30);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = global::iParking.Properties.Resources.Actions_application_exit_icon_24;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(532, 0);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Image = global::iParking.Properties.Resources.icons8_save_24;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(461, 0);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 30);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabOption_
            // 
            this.tabOption_.Controls.Add(this.label3);
            this.tabOption_.Controls.Add(this.label2);
            this.tabOption_.Controls.Add(this.numRefreshStateTime);
            this.tabOption_.Controls.Add(this.numOrderStateHoldTime);
            this.tabOption_.Controls.Add(this.label4);
            this.tabOption_.Controls.Add(this.label1);
            this.tabOption_.Location = new System.Drawing.Point(4, 24);
            this.tabOption_.Name = "tabOption_";
            this.tabOption_.Padding = new System.Windows.Forms.Padding(3);
            this.tabOption_.Size = new System.Drawing.Size(593, 367);
            this.tabOption_.TabIndex = 11;
            this.tabOption_.Text = "Option";
            this.tabOption_.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order State Hold Time";
            // 
            // numOrderStateHoldTime
            // 
            this.numOrderStateHoldTime.Location = new System.Drawing.Point(136, 15);
            this.numOrderStateHoldTime.Name = "numOrderStateHoldTime";
            this.numOrderStateHoldTime.Size = new System.Drawing.Size(76, 23);
            this.numOrderStateHoldTime.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "minute";
            // 
            // numRefreshStateTime
            // 
            this.numRefreshStateTime.Location = new System.Drawing.Point(136, 44);
            this.numRefreshStateTime.Name = "numRefreshStateTime";
            this.numRefreshStateTime.Size = new System.Drawing.Size(76, 23);
            this.numRefreshStateTime.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "second";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Refresh State Time";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 431);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSetting";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.tabOption.ResumeLayout(false);
            this.tabFloor.ResumeLayout(false);
            this.tabGroup.ResumeLayout(false);
            this.tabMAP.ResumeLayout(false);
            this.tabTMA.ResumeLayout(false);
            this.tabZCU.ResumeLayout(false);
            this.tabZONE.ResumeLayout(false);
            this.tabOutput.ResumeLayout(false);
            this.tabLED.ResumeLayout(false);
            this.tabVehicleType.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabOption_.ResumeLayout(false);
            this.tabOption_.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOrderStateHoldTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRefreshStateTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabOption;
        private System.Windows.Forms.TabPage tabFloor;
        private UserControls.ucFloor ucFloor1;
        private System.Windows.Forms.TabPage tabGroup;
        private UserControls.ucGroup ucGroup1;
        private System.Windows.Forms.TabPage tabMAP;
        private UserControls.ucMAP ucmap1;
        private System.Windows.Forms.TabPage tabZCU;
        private System.Windows.Forms.TabPage tabZONE;
        private UserControls.ucZone ucZone1;
        private UserControls.ucZCU uczcu1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabPage tabOutput;
        private UserControls.ucOutput ucOutput1;
        private System.Windows.Forms.TabPage tabLED;
        private UserControls.ucLed ucLed1;
        private System.Windows.Forms.TabPage tabVehicleType;
        private UserControls.ucVehicleType ucVehicleType1;
        private System.Windows.Forms.TabPage tabTMA;
        private UserControls.ucTMA uctma1;
        private System.Windows.Forms.TabPage tabOption_;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numRefreshStateTime;
        private System.Windows.Forms.NumericUpDown numOrderStateHoldTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
    }
}