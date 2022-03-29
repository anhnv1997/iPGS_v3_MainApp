using _Extensions;
using iParking.Databases;
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
    public partial class ucMAP : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucMAP()
        {
            InitializeComponent();
            dgvMap.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadMapData()
        {
            tblMAP.LoadDataMap(StaticPool.mapCollection);
        }
        public void ShowDataInGridView()
        {
            dgvMap.Rows.Clear();
            foreach (Map map in StaticPool.mapCollection)
            {
                dgvMap.Rows.Add(map.ID, dgvMap.Rows.Count + 1, map.Name, map.Code, map.Description, map.ImagePath);
            }
            if (dgvMap.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvMap.Rows[currentRow].Selected = true;
                    dgvMap.CurrentCell = dgvMap.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvMap.Rows[dgvMap.Rows.Count - 1].Selected = true;
                    dgvMap.CurrentCell = dgvMap.Rows[dgvMap.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmMAP frm = new frmMAP("");
            frm.Text = "Add New Map";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeMapDetail = true;
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvMap);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }

            currentRow = dgvMap.CurrentRow.Index;
            string oldMapName = StaticPool.mapCollection.GetMap(id).Name;
            frmMAP frm = new frmMAP(id);
            frm.Text = "Edit Map";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                string currentMapName = StaticPool.mapCollection.GetMap(id).Name;
                StaticPool.isChangeMapDetail = currentMapName != oldMapName;
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvMap);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvMap.CurrentRow.Index > 0 ? dgvMap.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblMAP.Delete(id))
                {
                    MessageBox.Show("Delete Map error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                Map map = StaticPool.mapCollection.GetMap(id);
                StaticPool.mapCollection.Remove(map);
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.mapDetailCollection.RemoveByMapID(id);
                StaticPool.isChangeMapDetail = true;
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadMapData();
            ShowDataInGridView();
        }
    }
}
