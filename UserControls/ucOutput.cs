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
    public partial class ucOutput : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucOutput()
        {
            InitializeComponent();
            dgvOutput.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void ShowDataInGridView()
        {
            dgvOutput.Rows.Clear();
            foreach (Output output in StaticPool.outputCollection)
            {
                dgvOutput.Rows.Add(output.ID, dgvOutput.Rows.Count + 1, output.Name, output.Code, output.Description, output.IPAddress, output.Port, Enum.GetName(typeof(EM_OutputTypes),output.OutputType));
            }
            if (dgvOutput.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvOutput.Rows[currentRow].Selected = true;
                    dgvOutput.CurrentCell = dgvOutput.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvOutput.Rows[dgvOutput.Rows.Count - 1].Selected = true;
                    dgvOutput.CurrentCell = dgvOutput.Rows[dgvOutput.Rows.Count - 1].Cells[1];
                }
            }
        }
        public void LoadOutputData()
        {
            tblOutput.LoadDataOutput(StaticPool.outputCollection);
        }
        #endregion
        #region:Control In Form
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmOutputDevice frm = new frmOutputDevice("");
            frm.Text = "Add New Output Device";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvOutput);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvOutput.CurrentRow.Index;
            frmOutputDevice frm = new frmOutputDevice(id);

            frm.Text = "Edit Output Device";
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
            string id = StaticPool.Id(dgvOutput);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvOutput.CurrentRow.Index > 0 ? dgvOutput.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblOutput.Delete(id))
                {
                    MessageBox.Show("Delete Output error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                Output output = StaticPool.outputCollection.GetOutput(id);
                StaticPool.outputCollection.Remove(output);
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.outputDetailCollection.RemoveByOutputID(id);
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadOutputData();
            ShowDataInGridView();
        }
        #endregion
    }
}
