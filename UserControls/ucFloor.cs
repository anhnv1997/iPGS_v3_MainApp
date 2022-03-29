using _Extensions;
using iParking.Databases;
using iParking.Forms;
using iParking.Object;
using System;
using System.Windows.Forms;

namespace iParking.UserControls
{
    public partial class ucFloor : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucFloor()
        {
            InitializeComponent();
            dgvFloor.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadFloorData()
        {
            StaticPool.LoadFloorData(StaticPool.floorCollection);
        }
        public void ShowDataInGridView()
        {
            dgvFloor.Rows.Clear();
            foreach (Floor floor in StaticPool.floorCollection)
            {
                dgvFloor.Rows.Add(floor.Id, dgvFloor.Rows.Count + 1, floor.Name, floor.Code, floor.Description);
            }
            if (dgvFloor.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvFloor.Rows[currentRow].Selected = true;
                    dgvFloor.CurrentCell = dgvFloor.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvFloor.Rows[dgvFloor.Rows.Count - 1].Selected = true;
                    dgvFloor.CurrentCell = dgvFloor.Rows[dgvFloor.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, System.EventArgs e)
        {
            frmFloor frm = new frmFloor("");
            frm.Text = "Add New Floor";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, System.EventArgs e)
        {
            string id = StaticPool.Id(dgvFloor);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvFloor.CurrentRow.Index;
            string oldFloorname = StaticPool.floorCollection.GetFloor(id).Name;
            frmFloor frm = new frmFloor(id);
            frm.Text = "Edit Floor";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                string currentFloorname = StaticPool.floorCollection.GetFloor(id).Name;
                StaticPool.isChangeZoneGroup = currentFloorname != oldFloorname;
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbDelete_Click(object sender, System.EventArgs e)
        {
            string id = StaticPool.Id(dgvFloor);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvFloor.CurrentRow.Index > 0 ? dgvFloor.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblFloor.Delete(id))
                {
                    MessageBox.Show("Delete Floor error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                Floor floor = StaticPool.floorCollection.GetFloor(id);
                StaticPool.floorCollection.Remove(floor);
                isEditorDelete = true;
                ShowDataInGridView();

                foreach (ZoneGroup zg in StaticPool.zoneGroupCollection)
                {
                    if (zg.FloorID == id)
                    {
                        zg.FloorID = "";
                    }
                }
                StaticPool.isChangeZoneGroup = true;
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, System.EventArgs e)
        {
            LoadFloorData();
            ShowDataInGridView();
        }
    }
}
