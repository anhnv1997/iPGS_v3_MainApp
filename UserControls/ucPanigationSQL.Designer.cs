
namespace iParking.UserControls
{
    partial class ucPanigationSQL
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tablePanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tablePanelSelectPage = new System.Windows.Forms.TableLayoutPanel();
            this.btnFirstPage = new FontAwesome.Sharp.IconButton();
            this.btnLastPage = new FontAwesome.Sharp.IconButton();
            this.btnNextPage = new FontAwesome.Sharp.IconButton();
            this.btnPreviousPage = new FontAwesome.Sharp.IconButton();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.lblMaxPage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.tablePanelMain.SuspendLayout();
            this.tablePanelSelectPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(0, 0);
            this.dgvData.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowHeadersVisible = false;
            this.dgvData.RowTemplate.Height = 25;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(682, 410);
            this.dgvData.TabIndex = 0;
            // 
            // tablePanelMain
            // 
            this.tablePanelMain.ColumnCount = 1;
            this.tablePanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelMain.Controls.Add(this.tablePanelSelectPage, 0, 1);
            this.tablePanelMain.Controls.Add(this.dgvData, 0, 0);
            this.tablePanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelMain.Location = new System.Drawing.Point(0, 0);
            this.tablePanelMain.Name = "tablePanelMain";
            this.tablePanelMain.RowCount = 2;
            this.tablePanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tablePanelMain.Size = new System.Drawing.Size(682, 442);
            this.tablePanelMain.TabIndex = 1;
            // 
            // tablePanelSelectPage
            // 
            this.tablePanelSelectPage.ColumnCount = 9;
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tablePanelSelectPage.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tablePanelSelectPage.Controls.Add(this.btnFirstPage, 1, 0);
            this.tablePanelSelectPage.Controls.Add(this.btnLastPage, 7, 0);
            this.tablePanelSelectPage.Controls.Add(this.btnNextPage, 6, 0);
            this.tablePanelSelectPage.Controls.Add(this.btnPreviousPage, 2, 0);
            this.tablePanelSelectPage.Controls.Add(this.txtCurrentPage, 3, 0);
            this.tablePanelSelectPage.Controls.Add(this.lblMaxPage, 5, 0);
            this.tablePanelSelectPage.Controls.Add(this.label1, 4, 0);
            this.tablePanelSelectPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanelSelectPage.Location = new System.Drawing.Point(0, 413);
            this.tablePanelSelectPage.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanelSelectPage.Name = "tablePanelSelectPage";
            this.tablePanelSelectPage.RowCount = 1;
            this.tablePanelSelectPage.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tablePanelSelectPage.Size = new System.Drawing.Size(682, 29);
            this.tablePanelSelectPage.TabIndex = 6;
            // 
            // btnFirstPage
            // 
            this.btnFirstPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFirstPage.Enabled = false;
            this.btnFirstPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFirstPage.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleLeft;
            this.btnFirstPage.IconColor = System.Drawing.Color.Black;
            this.btnFirstPage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnFirstPage.IconSize = 32;
            this.btnFirstPage.Location = new System.Drawing.Point(236, 0);
            this.btnFirstPage.Margin = new System.Windows.Forms.Padding(0);
            this.btnFirstPage.Name = "btnFirstPage";
            this.btnFirstPage.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnFirstPage.Size = new System.Drawing.Size(32, 29);
            this.btnFirstPage.TabIndex = 5;
            this.btnFirstPage.UseVisualStyleBackColor = true;
            this.btnFirstPage.Click += new System.EventHandler(this.btnFirstPage_Click);
            // 
            // btnLastPage
            // 
            this.btnLastPage.Enabled = false;
            this.btnLastPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLastPage.IconChar = FontAwesome.Sharp.IconChar.AngleDoubleRight;
            this.btnLastPage.IconColor = System.Drawing.Color.Black;
            this.btnLastPage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnLastPage.IconSize = 32;
            this.btnLastPage.Location = new System.Drawing.Point(414, 0);
            this.btnLastPage.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnLastPage.Name = "btnLastPage";
            this.btnLastPage.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnLastPage.Size = new System.Drawing.Size(32, 29);
            this.btnLastPage.TabIndex = 5;
            this.btnLastPage.UseVisualStyleBackColor = true;
            this.btnLastPage.Click += new System.EventHandler(this.btnLastPage_Click);
            // 
            // btnNextPage
            // 
            this.btnNextPage.Enabled = false;
            this.btnNextPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNextPage.IconChar = FontAwesome.Sharp.IconChar.AngleRight;
            this.btnNextPage.IconColor = System.Drawing.Color.Black;
            this.btnNextPage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnNextPage.IconSize = 32;
            this.btnNextPage.Location = new System.Drawing.Point(379, 0);
            this.btnNextPage.Margin = new System.Windows.Forms.Padding(0);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnNextPage.Size = new System.Drawing.Size(32, 29);
            this.btnNextPage.TabIndex = 5;
            this.btnNextPage.UseVisualStyleBackColor = true;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            // 
            // btnPreviousPage
            // 
            this.btnPreviousPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPreviousPage.Enabled = false;
            this.btnPreviousPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviousPage.IconChar = FontAwesome.Sharp.IconChar.AngleLeft;
            this.btnPreviousPage.IconColor = System.Drawing.Color.Black;
            this.btnPreviousPage.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnPreviousPage.IconSize = 32;
            this.btnPreviousPage.Location = new System.Drawing.Point(271, 0);
            this.btnPreviousPage.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btnPreviousPage.Size = new System.Drawing.Size(30, 29);
            this.btnPreviousPage.TabIndex = 5;
            this.btnPreviousPage.UseVisualStyleBackColor = true;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrentPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentPage.Enabled = false;
            this.txtCurrentPage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.txtCurrentPage.Location = new System.Drawing.Point(304, 0);
            this.txtCurrentPage.Margin = new System.Windows.Forms.Padding(0);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(27, 27);
            this.txtCurrentPage.TabIndex = 4;
            this.txtCurrentPage.Text = "99";
            this.txtCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMaxPage
            // 
            this.lblMaxPage.AutoSize = true;
            this.lblMaxPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMaxPage.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblMaxPage.Location = new System.Drawing.Point(348, 0);
            this.lblMaxPage.Name = "lblMaxPage";
            this.lblMaxPage.Size = new System.Drawing.Size(28, 29);
            this.lblMaxPage.TabIndex = 2;
            this.lblMaxPage.Text = "99";
            this.lblMaxPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(331, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label1.Size = new System.Drawing.Size(14, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "/";
            // 
            // ucPanigationSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tablePanelMain);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucPanigationSQL";
            this.Size = new System.Drawing.Size(682, 442);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.tablePanelMain.ResumeLayout(false);
            this.tablePanelSelectPage.ResumeLayout(false);
            this.tablePanelSelectPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TableLayoutPanel tablePanelMain;
        private System.Windows.Forms.Label lblMaxPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private FontAwesome.Sharp.IconButton btnFirstPage;
        private System.Windows.Forms.TableLayoutPanel tablePanelSelectPage;
        private FontAwesome.Sharp.IconButton btnLastPage;
        private FontAwesome.Sharp.IconButton btnNextPage;
        private FontAwesome.Sharp.IconButton btnPreviousPage;
    }
}
