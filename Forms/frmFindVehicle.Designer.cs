
namespace iParking.Forms
{
    partial class frmFindVehicle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFindVehicle));
            this.picMap = new System.Windows.Forms.PictureBox();
            this.panelSearchInfor = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFindVehicle = new System.Windows.Forms.Button();
            this.txtPlateNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelFindVehicle = new System.Windows.Forms.TableLayoutPanel();
            this.panelMap = new System.Windows.Forms.TableLayoutPanel();
            this.panelSelectMap = new System.Windows.Forms.TableLayoutPanel();
            this.btnFirstMap = new FontAwesome.Sharp.IconButton();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblMaxMapPage = new System.Windows.Forms.Label();
            this.txtMapPage = new System.Windows.Forms.TextBox();
            this.btnPreviusMap = new FontAwesome.Sharp.IconButton();
            this.btnLastMap = new FontAwesome.Sharp.IconButton();
            this.btnNextMap = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.panelSearchInfor.SuspendLayout();
            this.panelFindVehicle.SuspendLayout();
            this.panelMap.SuspendLayout();
            this.panelSelectMap.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // picMap
            // 
            this.picMap.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.picMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMap.Location = new System.Drawing.Point(0, 0);
            this.picMap.Margin = new System.Windows.Forms.Padding(0);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(1016, 487);
            this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            // 
            // panelSearchInfor
            // 
            this.panelSearchInfor.Controls.Add(this.label2);
            this.panelSearchInfor.Controls.Add(this.btnFindVehicle);
            this.panelSearchInfor.Controls.Add(this.txtPlateNum);
            this.panelSearchInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchInfor.Location = new System.Drawing.Point(0, 0);
            this.panelSearchInfor.Margin = new System.Windows.Forms.Padding(0);
            this.panelSearchInfor.Name = "panelSearchInfor";
            this.panelSearchInfor.Size = new System.Drawing.Size(1016, 40);
            this.panelSearchInfor.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Plate Number";
            // 
            // btnFindVehicle
            // 
            this.btnFindVehicle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnFindVehicle.Image = ((System.Drawing.Image)(resources.GetObject("btnFindVehicle.Image")));
            this.btnFindVehicle.Location = new System.Drawing.Point(248, 3);
            this.btnFindVehicle.Name = "btnFindVehicle";
            this.btnFindVehicle.Size = new System.Drawing.Size(75, 32);
            this.btnFindVehicle.TabIndex = 2;
            this.btnFindVehicle.Text = "Find";
            this.btnFindVehicle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFindVehicle.UseVisualStyleBackColor = true;
            this.btnFindVehicle.Click += new System.EventHandler(this.btnFindVehicle_Click);
            // 
            // txtPlateNum
            // 
            this.txtPlateNum.Location = new System.Drawing.Point(109, 9);
            this.txtPlateNum.Name = "txtPlateNum";
            this.txtPlateNum.Size = new System.Drawing.Size(133, 23);
            this.txtPlateNum.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(38, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "/";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFindVehicle
            // 
            this.panelFindVehicle.ColumnCount = 1;
            this.panelFindVehicle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelFindVehicle.Controls.Add(this.panelSearchInfor, 0, 0);
            this.panelFindVehicle.Controls.Add(this.panelMap, 0, 1);
            this.panelFindVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFindVehicle.Location = new System.Drawing.Point(0, 0);
            this.panelFindVehicle.Margin = new System.Windows.Forms.Padding(0);
            this.panelFindVehicle.Name = "panelFindVehicle";
            this.panelFindVehicle.RowCount = 2;
            this.panelFindVehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.panelFindVehicle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelFindVehicle.Size = new System.Drawing.Size(1016, 557);
            this.panelFindVehicle.TabIndex = 1;
            // 
            // panelMap
            // 
            this.panelMap.ColumnCount = 1;
            this.panelMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelMap.Controls.Add(this.picMap, 0, 0);
            this.panelMap.Controls.Add(this.panelSelectMap, 0, 1);
            this.panelMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMap.Location = new System.Drawing.Point(0, 40);
            this.panelMap.Margin = new System.Windows.Forms.Padding(0);
            this.panelMap.Name = "panelMap";
            this.panelMap.RowCount = 2;
            this.panelMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.panelMap.Size = new System.Drawing.Size(1016, 517);
            this.panelMap.TabIndex = 2;
            // 
            // panelSelectMap
            // 
            this.panelSelectMap.ColumnCount = 7;
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.panelSelectMap.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panelSelectMap.Controls.Add(this.btnFirstMap, 1, 0);
            this.panelSelectMap.Controls.Add(this.tableLayoutPanel4, 3, 0);
            this.panelSelectMap.Controls.Add(this.btnPreviusMap, 2, 0);
            this.panelSelectMap.Controls.Add(this.btnLastMap, 5, 0);
            this.panelSelectMap.Controls.Add(this.btnNextMap, 4, 0);
            this.panelSelectMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSelectMap.Location = new System.Drawing.Point(0, 487);
            this.panelSelectMap.Margin = new System.Windows.Forms.Padding(0);
            this.panelSelectMap.Name = "panelSelectMap";
            this.panelSelectMap.RowCount = 1;
            this.panelSelectMap.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelSelectMap.Size = new System.Drawing.Size(1016, 30);
            this.panelSelectMap.TabIndex = 3;
            // 
            // btnFirstMap
            // 
            this.btnFirstMap.FlatAppearance.BorderSize = 0;
            this.btnFirstMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstMap.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleLeft;
            this.btnFirstMap.IconColor = System.Drawing.Color.Black;
            this.btnFirstMap.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFirstMap.IconSize = 32;
            this.btnFirstMap.Location = new System.Drawing.Point(412, 3);
            this.btnFirstMap.Name = "btnFirstMap";
            this.btnFirstMap.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnFirstMap.Size = new System.Drawing.Size(21, 24);
            this.btnFirstMap.TabIndex = 4;
            this.btnFirstMap.UseVisualStyleBackColor = true;
            this.btnFirstMap.Click += new System.EventHandler(this.btnFirstMap_Click);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblMaxMapPage, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.txtMapPage, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(459, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(90, 30);
            this.tableLayoutPanel4.TabIndex = 5;
            // 
            // lblMaxMapPage
            // 
            this.lblMaxMapPage.AutoSize = true;
            this.lblMaxMapPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaxMapPage.Enabled = false;
            this.lblMaxMapPage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMaxMapPage.Location = new System.Drawing.Point(59, 1);
            this.lblMaxMapPage.Margin = new System.Windows.Forms.Padding(0);
            this.lblMaxMapPage.Name = "lblMaxMapPage";
            this.lblMaxMapPage.Size = new System.Drawing.Size(30, 28);
            this.lblMaxMapPage.TabIndex = 2;
            this.lblMaxMapPage.Text = "99";
            this.lblMaxMapPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMapPage
            // 
            this.txtMapPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMapPage.Enabled = false;
            this.txtMapPage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtMapPage.Location = new System.Drawing.Point(1, 1);
            this.txtMapPage.Margin = new System.Windows.Forms.Padding(0);
            this.txtMapPage.Name = "txtMapPage";
            this.txtMapPage.Size = new System.Drawing.Size(36, 29);
            this.txtMapPage.TabIndex = 3;
            this.txtMapPage.Text = "99";
            this.txtMapPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMapPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMapPage_KeyDown);
            // 
            // btnPreviusMap
            // 
            this.btnPreviusMap.FlatAppearance.BorderSize = 0;
            this.btnPreviusMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviusMap.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            this.btnPreviusMap.IconColor = System.Drawing.Color.Black;
            this.btnPreviusMap.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPreviusMap.IconSize = 32;
            this.btnPreviusMap.Location = new System.Drawing.Point(439, 3);
            this.btnPreviusMap.Name = "btnPreviusMap";
            this.btnPreviusMap.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnPreviusMap.Size = new System.Drawing.Size(17, 24);
            this.btnPreviusMap.TabIndex = 4;
            this.btnPreviusMap.UseVisualStyleBackColor = true;
            this.btnPreviusMap.Click += new System.EventHandler(this.btnPreviusMap_Click);
            // 
            // btnLastMap
            // 
            this.btnLastMap.FlatAppearance.BorderSize = 0;
            this.btnLastMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastMap.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleRight;
            this.btnLastMap.IconColor = System.Drawing.Color.Black;
            this.btnLastMap.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLastMap.IconSize = 32;
            this.btnLastMap.Location = new System.Drawing.Point(579, 3);
            this.btnLastMap.Name = "btnLastMap";
            this.btnLastMap.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnLastMap.Size = new System.Drawing.Size(24, 24);
            this.btnLastMap.TabIndex = 4;
            this.btnLastMap.UseVisualStyleBackColor = true;
            this.btnLastMap.Click += new System.EventHandler(this.btnLastMap_Click);
            // 
            // btnNextMap
            // 
            this.btnNextMap.FlatAppearance.BorderSize = 0;
            this.btnNextMap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextMap.IconChar = FontAwesome.Sharp.IconChar.AngleRight;
            this.btnNextMap.IconColor = System.Drawing.Color.Black;
            this.btnNextMap.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNextMap.IconSize = 32;
            this.btnNextMap.Location = new System.Drawing.Point(552, 3);
            this.btnNextMap.Name = "btnNextMap";
            this.btnNextMap.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnNextMap.Size = new System.Drawing.Size(21, 24);
            this.btnNextMap.TabIndex = 4;
            this.btnNextMap.UseVisualStyleBackColor = true;
            this.btnNextMap.Click += new System.EventHandler(this.btnNextMap_Click);
            // 
            // frmFindVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 557);
            this.Controls.Add(this.panelFindVehicle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFindVehicle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find Vehicle";
            this.Load += new System.EventHandler(this.frmFindVehicle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.panelSearchInfor.ResumeLayout(false);
            this.panelSearchInfor.PerformLayout();
            this.panelFindVehicle.ResumeLayout(false);
            this.panelMap.ResumeLayout(false);
            this.panelSelectMap.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.Panel panelSearchInfor;
        private System.Windows.Forms.Button btnFindVehicle;
        private System.Windows.Forms.TextBox txtPlateNum;
        private System.Windows.Forms.TableLayoutPanel panelFindVehicle;
        private System.Windows.Forms.TableLayoutPanel panelMap;
        private System.Windows.Forms.TableLayoutPanel panelSelectMap;
        private FontAwesome.Sharp.IconButton btnFirstMap;
        private FontAwesome.Sharp.IconButton btnPreviusMap;
        private FontAwesome.Sharp.IconButton btnNextMap;
        private FontAwesome.Sharp.IconButton btnLastMap;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMaxMapPage;
        private System.Windows.Forms.TextBox txtMapPage;
        private System.Windows.Forms.Label label2;
    }
}