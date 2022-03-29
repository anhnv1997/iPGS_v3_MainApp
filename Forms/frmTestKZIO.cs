using Kztek.KZIO.IO_Controllers;
using System;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmTestKZIO : Form
    {
        I_KZIO i_KZIO;
        public frmTestKZIO()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(i_KZIO!=null)
                MessageBox.Show(i_KZIO.SetLedState((int)numSlot.Value, (Kztek.KZIO.Object.EM_LEDSTATE)cbColor.SelectedIndex) + "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            i_KZIO = KZIO_factory.GetKZIO(Kztek.KZIO.Object.EM_KZIO_TYPES.KZ_IO1616, txtIP.Text);
        }
    }
}
