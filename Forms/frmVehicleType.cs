using iParking.Databases;
using iParking.Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmVehicleType : Form
    {
        string ID = "";
        public frmVehicleType(string _ID)
        {
            InitializeComponent();
            this.ID = _ID;
        }
        private void frmVehicleType_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            if (this.ID != "")
            {
                txtID.Text = this.ID;
                VehicleType vehicleType = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(this.ID);
                if (vehicleType != null)
                {
                    txtCode.Text = vehicleType.Code;
                    txtDescription.Text = vehicleType.Description;
                    txtName.Text = vehicleType.Name;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckVehicleTypeName())
            {
                VehicleType vehicleType = new VehicleType();
                if (this.ID != "")
                {
                    vehicleType = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(this.ID);
                }
                vehicleType.Code = txtCode.Text;
                vehicleType.Description = txtDescription.Text;
                vehicleType.Name = txtName.Text;
                //Edit
                if (this.ID != "")
                {
                    if (!tblVehicleType.Modify(this.ID, txtCode.Text, txtDescription.Text, txtName.Text))
                    {
                        MessageBox.Show("Edit Vehicle Type infor error, try again later");
                    }
                }
                //Add
                else
                {
                    string vehicleID = tblVehicleType.InsertAndGetLastID(txtCode.Text, txtDescription.Text, txtName.Text);
                    if (vehicleID != "")
                    {
                        vehicleType.ID = vehicleID;
                        StaticPool.vehicleTypeCollection.Add(vehicleType);
                    }
                    else
                    {
                        MessageBox.Show("Add vehicle type infor error, try again later");
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("This vehicle type name is already used, please try another name!");
                return;
            }
        }
        private bool CheckVehicleTypeName()
        {
            if (this.ID != "")
            {
                foreach (VehicleType vehicleType in StaticPool.vehicleTypeCollection)
                {
                    if (vehicleType.ID != this.ID && vehicleType.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (VehicleType vehicleType in StaticPool.vehicleTypeCollection)
                {
                    if (vehicleType.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&!(e.KeyChar=='-'))
            {
                e.Handled = true;
            }
        }
    }
}
