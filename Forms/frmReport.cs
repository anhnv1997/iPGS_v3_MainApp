using System;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmReport : Form
    {
        public frmReport()
        {
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            ucPanigationsql1.SetTableInfor("tblZone", 20);
        }
    }
}
