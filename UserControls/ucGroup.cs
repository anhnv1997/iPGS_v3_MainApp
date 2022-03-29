using _Extensions;
using iParking.Databases;
using iParking.Forms;
using iParking.Object;
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
    public partial class ucGroup : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucGroup()
        {
            InitializeComponent();
            dgvGroup.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadGroupData()
        {
            tblZoneGroup.LoadDataGroup(StaticPool.zoneGroupCollection);
        }
        public void ShowDataInGridView()
        {
            dgvGroup.Rows.Clear();
            foreach (ZoneGroup zoneGroup in StaticPool.zoneGroupCollection)
            {
                string floorName = "";
                Floor floor = StaticPool.floorCollection.GetFloor(zoneGroup.FloorID);
                if (floor != null)
                {
                    floorName = floor.Name;
                }
                dgvGroup.Rows.Add(zoneGroup.Id, dgvGroup.Rows.Count + 1, zoneGroup.Name, zoneGroup.Code, zoneGroup.Description, floorName);
            }
            if (dgvGroup.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvGroup.Rows[currentRow].Selected = true;
                    dgvGroup.CurrentCell = dgvGroup.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvGroup.Rows[dgvGroup.Rows.Count - 1].Selected = true;
                    dgvGroup.CurrentCell = dgvGroup.Rows[dgvGroup.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmZoneGroup frm = new frmZoneGroup("");
            frm.Text = "Add New Zone Group";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvGroup);

            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvGroup.CurrentRow.Index;
            string oldGroupname = StaticPool.zoneGroupCollection.GetzgById(id).Name;
            frmZoneGroup frm = new frmZoneGroup(id);
            frm.Text = "Edit Zone Group";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                string currenGroupName = StaticPool.zoneGroupCollection.GetzgById(id).Name;
                StaticPool.isChangeZone = currenGroupName != oldGroupname;
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvGroup);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvGroup.CurrentRow.Index > 0 ? dgvGroup.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblZoneGroup.Delete(id))
                {
                    MessageBox.Show("Delete Zone Group error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                ZoneGroup zg = StaticPool.zoneGroupCollection.GetzgById(id);
                StaticPool.zoneGroupCollection.Remove(zg);
                isEditorDelete = true;
                ShowDataInGridView();

                foreach (ZONE zone in StaticPool.zoneCollection)
                {
                    if (zone.ZoneGroupId == id)
                    {
                        zone.ZoneGroupId = "";
                    }
                }
                StaticPool.isChangeZone = true;
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadGroupData();
            ShowDataInGridView();
        }
    }
}
