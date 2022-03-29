using _Extensions;
using iParking.Databases;
using iParking.Forms;
using iParking.Object;
using System;
using System.Windows.Forms;



namespace iParking.UserControls
{
    public partial class ucVehicleType : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucVehicleType()
        {
            InitializeComponent();
            dgvVehicleType.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadVehicleData()
        {
            tblVehicleType.LoadVehicleType(StaticPool.vehicleTypeCollection);
        }
        public void ShowDataInGridView()
        {
            dgvVehicleType.Rows.Clear();
            foreach (VehicleType vehicleType in StaticPool.vehicleTypeCollection)
            {
                dgvVehicleType.Rows.Add(vehicleType.ID, dgvVehicleType.Rows.Count + 1, vehicleType.Name, vehicleType.Code, vehicleType.Description);
            }
            if (dgvVehicleType.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvVehicleType.Rows[currentRow].Selected = true;
                    dgvVehicleType.CurrentCell = dgvVehicleType.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvVehicleType.Rows[dgvVehicleType.Rows.Count - 1].Selected = true;
                    dgvVehicleType.CurrentCell = dgvVehicleType.Rows[dgvVehicleType.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmVehicleType frm = new frmVehicleType("");
            frm.Text = "Add New Vehicle Type";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvVehicleType);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvVehicleType.CurrentRow.Index;
            string oldVehicleName = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(id).Name;
            frmVehicleType frm = new frmVehicleType(id);
            frm.Text = "Edit vehicle Type";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                string currentvehicleName = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(id).Name;
                StaticPool.isChangeZone = currentvehicleName != oldVehicleName;
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;

            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvVehicleType);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvVehicleType.CurrentRow.Index > 0 ? dgvVehicleType.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblVehicleType.Delete(id))
                {
                    MessageBox.Show("Delete Vehicle Type error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                VehicleType vehicleType = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(id);
                StaticPool.vehicleTypeCollection.Remove(vehicleType);
                isEditorDelete = true;
                ShowDataInGridView();

                //foreach (ZONE zone in StaticPool.zoneCollection)
                //{
                //    if (zone.VehicleTypeID == id)
                //    {
                //        zone.VehicleTypeID = "";
                //    }
                //}
                StaticPool.isChangeZone = true;
                StaticPool.isChangeSetting = true;
            }

        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadVehicleData();
            ShowDataInGridView();
        }
    }
}
