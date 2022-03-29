using _Extensions;
using iParking.Databases;
using iParking.Enums;
using iParking.Forms;
using iParking.Object;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iParking.UserControls
{
    public partial class ucZone : UserControl
    {
        public static int currentRow = 0;
        public static bool isEditorDelete = false;
        public ucZone()
        {
            InitializeComponent();
            dgvZone.ToggleDoubleBuffered(true);
        }
        #region:Load
        public void LoadZoneData()
        {
            tblZone.LoadDataZone(StaticPool.zoneCollection);
        }
        public void ShowDataInGridView()
        {
            dgvZone.Rows.Clear();
            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                ZoneGroup zg = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                ZCU zcu = StaticPool.zcuCollection.GetZCUById(zone.ZCUId);
                string groupName = "";
                string zcuName = "";
                if (zg != null)
                {
                    groupName = zg.Name;
                }
                if (zcu != null)
                {
                    zcuName = zcu.ZcuName;
                }
                dgvZone.Rows.Add(zone.Id, dgvZone.Rows.Count + 1, zone.zoneName, zone.Code, zone.Description, zcuName, groupName, zone.ZcuIndex);
            }
            if (dgvZone.Rows.Count > 0)
            {
                if (isEditorDelete)
                {
                    dgvZone.Rows[currentRow].Selected = true;
                    dgvZone.CurrentCell = dgvZone.Rows[currentRow].Cells[1];
                    isEditorDelete = false;
                }
                else
                {
                    dgvZone.Rows[dgvZone.Rows.Count - 1].Selected = true;
                    dgvZone.CurrentCell = dgvZone.Rows[dgvZone.Rows.Count - 1].Cells[1];
                }
            }
        }
        #endregion
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmZone frm = new frmZone("");
            frm.Text = "Add new zone";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string id = StaticPool.Id(dgvZone);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvZone.CurrentRow.Index;
            frmZone frm = new frmZone(id);
            frm.Text = "Edit Zone";
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
            string id = StaticPool.Id(dgvZone);
            if (id == "")
            {
                MessageBox.Show("Please select a record");
                return;
            }
            currentRow = dgvZone.CurrentRow.Index > 0 ? dgvZone.CurrentRow.Index - 1 : 0;
            if (MessageBox.Show("Do you want to delete this record", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (!tblZone.Delete(id))
                {
                    MessageBox.Show("Delete zone error, try again later!");
                    return;
                }
                //Update hien thi sau xoa               
                ZONE zone = StaticPool.zoneCollection.GetZONE(id);

                DeleteLinkedMapDetail(id);

                //DeleteLinkedLedDetail(id, zone);

                DeleteLinkedOutputDetail(id, zone);

                StaticPool.zoneCollection.Remove(zone);
                isEditorDelete = true;
                ShowDataInGridView();
                StaticPool.isChangeSetting = true;
            }
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            LoadZoneData();
            ShowDataInGridView();
        }

        #region:Internal
        private static void DeleteLinkedMapDetail(string id)
        {
            List<MapDetail> deleteMapList = new List<MapDetail>();
            foreach (MapDetail mapDetail in StaticPool.mapDetailCollection)
            {
                if (mapDetail.ZONEId == id)
                {
                    deleteMapList.Add(mapDetail);
                }
            }
            foreach (MapDetail _mapDetail in deleteMapList)
            {
                StaticPool.mapDetailCollection.Remove(_mapDetail);
            }
            StaticPool.isChangeMapDetail = true;
        }
        private static void DeleteLinkedLedDetail(string id, ZONE zone)
        {
            List<LedDetail> deleteLedList = new List<LedDetail>();
            foreach (LedDetail ledDetail in StaticPool.ledDetailCollection)
            {
                if (ledDetail.ZoneID == id)
                {
                    deleteLedList.Add(ledDetail);
                }
            }
            foreach (LedDetail ledDetail in deleteLedList)
            {
                Led led = StaticPool.ledCollection.GetLed(ledDetail.LedID);
                if (led != null)
                {
                    if (zone.Status == (int)EM_ZoneStatusType.UN_OCCUPIED)
                    {
                        led.SlotCount--;
                    }
                    else if (zone.Status == (int)EM_ZoneStatusType.DISCONNECT && zone.OldStatus == (int)EM_ZoneStatusType.UN_OCCUPIED)
                    {
                        led.SlotCount--;
                    }
                }
                StaticPool.ledDetailCollection.Remove(ledDetail);
            }
            StaticPool.isChangeLedDetail = true;
        }
        private static void DeleteLinkedOutputDetail(string id, ZONE zone)
        {
            List<OutputDetail> deleteOutputList = new List<OutputDetail>();
            foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
            {
                if (outputDetail.ZoneID == id)
                {
                    deleteOutputList.Add(outputDetail);
                }
            }
            foreach (OutputDetail outputDetail in deleteOutputList)
            {
                Output output = StaticPool.outputCollection.GetOutput(outputDetail.OutputID);
                if (output != null)
                {
                    if (zone.Status == (int)EM_ZoneStatusType.UN_OCCUPIED)
                    {
                        if (output.SlotCounts[outputDetail.RelayIndex] > 0)
                            output.SlotCounts[outputDetail.RelayIndex]--;
                    }
                    else if (zone.Status == (int)EM_ZoneStatusType.DISCONNECT && zone.OldStatus == (int)EM_ZoneStatusType.UN_OCCUPIED)
                    {
                        if (output.SlotCounts[outputDetail.RelayIndex] > 0)
                            output.SlotCounts[outputDetail.RelayIndex]--;
                    }

                }
                StaticPool.outputDetailCollection.Remove(outputDetail);
            }
            StaticPool.isChangeOutputDetail = true;
        }
        #endregion
    }
}
