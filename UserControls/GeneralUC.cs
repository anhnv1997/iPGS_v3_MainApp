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
    public partial class GeneralUC : UserControl
    {
        private Type t;
        public GeneralUC()
        {
            InitializeComponent();
        }
        public void SetType(Type currentType)
        {
            t = currentType;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(t == typeof(frmFloor))
            {
                MessageBox.Show("OK");
            }
        }
    }
}
