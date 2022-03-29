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
    public partial class ucZCU : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucZCU()
        {
            InitializeComponent();
            dgvZCU.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadZcuData()
        {
            tblZCU.LoadDataZCU(StaticPool.zcuCollection);
        }
        public void ShowDataInGridView()
        {
            dgvZCU.Rows.Clear();
            foreach (ZCU zcu in StaticPool.zcuCollection)
            {
                string type = Enum.GetName(typeof(EM_ZcuTypes), zcu.Type);
                string ccuName = StaticPool.ccuCollection.Getccu(zcu.CCUId).Name;
                dgvZCU.Rows.Add(zcu.Id, dgvZCU.Rows.Count + 1, zcu.ZcuName, zcu.Code, zcu.Description, ccuName, type, Enum.GetName(typeof(EM_CommunicationType), zcu.CommunicationType), zcu.Address, zcu.IPAddress, zcu.Port, zcu.Username, zcu.Password);
            }
            if (dgvZCU.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvZCU.Rows[currentRow].Selected = true;
                    dgvZCU.CurrentCell = dgvZCU.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvZCU.Rows[dgvZCU.Rows.Count - 1].Selected = true;
                    dgvZCU.CurrentCell = dgvZCU.Rows[dgvZCU.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmZCU frm = new frmZCU("");
            frm.Text = "Add New ZCU";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvZCU);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvZCU.CurrentRow.Index;
            string oldZCUName = StaticPool.zcuCollection.GetZCUById(id).ZcuName;
            frmZCU frm = new frmZCU(id);
            frm.Text = "Edit ZCU";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                string currentZCUName = StaticPool.zcuCollection.GetZCUById(id).ZcuName;
                StaticPool.isChangeZone = currentZCUName != oldZCUName;
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
                
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvZCU);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvZCU.CurrentRow.Index > 0 ? dgvZCU.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblZCU.Delete(id))
                {
                    MessageBox.Show("Delete ZCU error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                ZCU zcu = StaticPool.zcuCollection.GetZCUById(id);
                StaticPool.zcuCollection.Remove(zcu);
                isEditorDelete = true;
                ShowDataInGridView();

                foreach (ZONE zone in StaticPool.zoneCollection)
                {
                    if (zone.ZCUId == id)
                    {
                        zone.ZCUId = "";
                    }
                }
                StaticPool.isChangeZone = true;
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadZcuData();
            ShowDataInGridView();
        }

        private void dgvZCU_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 12 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
