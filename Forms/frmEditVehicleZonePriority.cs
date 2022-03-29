using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmEditVehicleZonePriority : Form
    {
        public string VehicleType { get; set; }
        public string ZoneName { get; set; }
        public string GroupName { get; set; }
        public int PriorityLevel { get; set; }
        public frmEditVehicleZonePriority()
        {
            InitializeComponent();
        }

        private void frmEditVehicleZonePriority_Load(object sender, EventArgs e)
        {
            txtGroupName.Text = this.GroupName;
            txtVehicleType.Text = this.VehicleType;
            txtZoneName.Text = this.ZoneName;
            numPriority.Value = this.PriorityLevel;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.currentVehicleZonePriority = (int)numPriority.Value;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }
    }
}
