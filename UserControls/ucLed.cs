using _Extensions;
using iParking.Databases;
using iParking.Enums;
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
    public partial class ucLed : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucLed()
        {
            InitializeComponent();
            dgvLED.ToggleDoubleBuffered(true);
        }

        #region:Load
        public void LoadLedData()
        {
            tblLed.LoadDataLed(StaticPool.ledCollection);
        }
        public void ShowDataInGridView()
        {
            dgvLED.Rows.Clear();
            foreach (Led led in StaticPool.ledCollection)
            {
                string name = led.Name;
                string code = led.Code;
                string description = led.Description;
                string type = Enum.GetName(typeof(EM_LedTypes), led.LedType);
                string ip = led.IPAddress;
                int port = led.Port;
                int address = led.Address;
                string arrow = LedArrowDirection.GetLedArrowName(led.LedArrow);
                string color = LedColor.GetLedColorName(led.Color);
                string zeroColor = LedColor.GetLedColorName(led.ZeroColor);
                dgvLED.Rows.Add(led.Id, dgvLED.Rows.Count + 1, name, code, description, type,Enum.GetName(typeof(EM_CommunicationType),led.CommunicationType), ip, port, address, arrow, color, zeroColor);
            }
            if (dgvLED.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvLED.Rows[currentRow].Selected = true;
                    dgvLED.CurrentCell = dgvLED.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvLED.Rows[dgvLED.Rows.Count - 1].Selected = true;
                    dgvLED.CurrentCell = dgvLED.Rows[dgvLED.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmLED frm = new frmLED("");
            frm.Text = "Add New Led";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvLED);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvLED.CurrentRow.Index;
            frmLED frm = new frmLED(id);
            frm.Text = "Edit Led";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvLED);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvLED.CurrentRow.Index > 0 ? dgvLED.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblLed.Delete(id))
                {
                    MessageBox.Show("Delete Led error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                Led led = StaticPool.ledCollection.GetLed(id);
                StaticPool.ledCollection.Remove(led);
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.ledDetailCollection.RemoveByLedID(id);
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadLedData();
            ShowDataInGridView();
        }
    }
}
