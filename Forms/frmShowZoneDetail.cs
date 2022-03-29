using iParking.Enums;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmShowZoneDetail : Form
    { 
        public frmShowZoneDetail(ZONE _zone,string _imagePath, string plateNum)
        {
            InitializeComponent();
            if (File.Exists(_imagePath))
            {
                try
                {
                    picZone.Image = Image.FromFile(_imagePath);
                }
                catch (Exception)
                {

                }
            }
            lblPlateNum.Text = plateNum;
            if (_zone != null)
            {
                lblStatus.Text = Enum.GetName(typeof(EM_ZoneStatusType), _zone.Status);
                lblZoneName.Text = _zone.zoneName;
            }
            else
            {
                lblStatus.Text = "";
                lblZoneName.Text = "";
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
