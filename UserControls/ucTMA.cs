using _Extensions;
using iParking.Databases;
using iParking.Enums;
using iParking.Forms;
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
    public partial class ucTMA : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucTMA()
        {
            InitializeComponent();
            dgvTMA.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadTMAData()
        {
            tblTMA_Server.LoadDataTMA(StaticPool.TMACollection);
        }
        public void ShowDataInGridView()
        {
            dgvTMA.Rows.Clear();
            foreach (TMA_Server _TMA_Server in StaticPool.TMACollection)
            {
                dgvTMA.Rows.Add(_TMA_Server.Id, dgvTMA.Rows.Count + 1, _TMA_Server.Name, _TMA_Server.Code, _TMA_Server.Description,  _TMA_Server.Ip, _TMA_Server.Port, _TMA_Server.Username, _TMA_Server.Password);
            }
            if (dgvTMA.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvTMA.Rows[currentRow].Selected = true;
                    dgvTMA.CurrentCell = dgvTMA.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvTMA.Rows[dgvTMA.Rows.Count - 1].Selected = true;
                    dgvTMA.CurrentCell = dgvTMA.Rows[dgvTMA.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmTMA frm = new frmTMA("");
            frm.Text = "Add New PGS Server";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvTMA);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvTMA.CurrentRow.Index;
            string oldTMAName = StaticPool.TMACollection.GetTMA_Server(id).Name;
            frmTMA frm = new frmTMA(id);
            frm.Text = "Edit PGS Server";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                string currentTMAName = StaticPool.TMACollection.GetTMA_Server(id).Name;
                StaticPool.isChangeZcu = oldTMAName != currentTMAName;
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }

        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvTMA);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvTMA.CurrentRow.Index > 0 ? dgvTMA.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblTMA_Server.Delete(id))
                {
                    MessageBox.Show("Delete PGS Server error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                TMA_Server _TMA_SERVER = StaticPool.TMACollection.GetTMA_Server(id);
                StaticPool.TMACollection.Remove(_TMA_SERVER);
                isEditorDelete = true;
                ShowDataInGridView();

                foreach (ZCU zcu in StaticPool.zcuCollection)
                {
                    if (zcu.TMA_ID == id)
                    {
                        zcu.TMA_ID = "";
                    }
                }
                StaticPool.isChangeZcu = true;
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadTMAData();
            ShowDataInGridView();
        }

        private void dgvTMA_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 8 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
