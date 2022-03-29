using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iParking.UserControls
{
    public partial class ucPanigationSQL : UserControl
    {
        private string tblName;
        private int count;
        private int maxRowPerPage = 10;
        private const int firstPage = 1;
        private int lastPage = 0;
        private int currentPage = 0;
        public ucPanigationSQL()
        {
            InitializeComponent();
        }
        public void SetTableInfor(string _tblName, int _maxRowPerPage)
        {
            maxRowPerPage = _maxRowPerPage;
            tblName = _tblName;
            DataTable dtCount = StaticPool.mdb.FillData($"SELECT COUNT(*) as count FROM {_tblName}");
            if (dtCount != null)
            {
                if (dtCount.Rows.Count > 0)
                {
                    try
                    {
                        count = Convert.ToInt32(dtCount.Rows[0]["count"].ToString());
                    }
                    catch
                    {
                        count = 0;
                    }
                    finally
                    {
                        lblMaxPage.Text = count + "";
                        txtCurrentPage.Text = "1";
                        lastPage = count / maxRowPerPage;
                        currentPage = 1;
                        if (count % maxRowPerPage != 0)
                        {
                            lastPage++;
                        }
                    }
                }
            }
            if (count == 0)
            {
                btnFirstPage.Enabled = false;
                btnLastPage.Enabled = false;
                btnPreviousPage.Enabled = false;
                btnNextPage.Enabled = false;
            }
            else
            {
                SetFirstPageSelect();
            }
        }

        private void SetFirstPageSelect()
        {
            btnPreviousPage.Enabled = false;
            btnFirstPage.Enabled = false;
            btnNextPage.Enabled = true;
            btnLastPage.Enabled = true;
            txtCurrentPage.Text = firstPage + "";
            currentPage = Convert.ToInt32(txtCurrentPage.Text);
            GetData(firstPage);
        }

        private void SetLastPageSelect()
        {
            btnPreviousPage.Enabled = true;
            btnFirstPage.Enabled = true;
            btnNextPage.Enabled = false;
            btnLastPage.Enabled = false;

            txtCurrentPage.Text = lastPage + "";
            currentPage = Convert.ToInt32(txtCurrentPage.Text);
            GetData(lastPage);
        }

        private void GetData(int currentPage)
        {
            string GetFirstPage = @$"Select * from (SELECT *,ROW_NUMBER() OVER(ORDER BY Sort) as RowNumber
                                                                        FROM {tblName}) a
                                                                        Where RowNumber between {(currentPage - 1) * maxRowPerPage + 1} And {currentPage * maxRowPerPage}";
            DataTable dtData = StaticPool.mdb.FillData(GetFirstPage);
            dgvData.DataSource = dtData;
        }

        private void SetNormalPageSelect()
        {
            btnPreviousPage.Enabled = true;
            btnFirstPage.Enabled = true;
            btnNextPage.Enabled = true;
            btnLastPage.Enabled = true;

            GetData(currentPage);
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            SetFirstPageSelect();
        }

        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(txtCurrentPage.Text);
            if (Convert.ToInt16(txtCurrentPage.Text) == firstPage + 1)
            {
                SetFirstPageSelect();
            }
            else
            {
                SetNormalPageSelect();
                txtCurrentPage.Text = (currentPage - 1) + "";
                currentPage = Convert.ToInt32(txtCurrentPage.Text);
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            currentPage = Convert.ToInt32(txtCurrentPage.Text);
            if (Convert.ToInt16(txtCurrentPage.Text) == lastPage - 1)
            {
                SetLastPageSelect();
            }
            else
            {
                SetLastPageSelect();
                txtCurrentPage.Text = (currentPage) + "";
                currentPage = Convert.ToInt32(txtCurrentPage.Text);
            }
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            SetLastPageSelect();
        }
    }
}
