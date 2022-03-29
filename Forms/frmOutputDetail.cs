using iParking.Database;
using iParking.Databases;
using iParking.Enums;
using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmOutputDetail : Form
    {
        #region:Properties
        List<ZONE> UnUsedZones;
        List<ZONE> InUsedZones;
        List<ZONE> AddZones = new List<ZONE>();
        List<ZONE> RemoveZones = new List<ZONE>();
        List<ZONE> InitZones = new List<ZONE>();
        string selectedOutputID = "";
        int selectedRelayIndex = 1;
        #endregion
        public frmOutputDetail()
        {
            InitializeComponent();
        }
        private void frmOutputDetail_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
                btnSelect.Image = Image.FromFile(@".\Icon\SelectIcon\icons8_fast_forward_64px.png");
                btnSelectAll.Image = Image.FromFile(@".\Icon\SelectIcon\icons8_end_64px.png");
                btnUnselect.Image = Image.FromFile(@".\Icon\SelectIcon\icons8_back_forward_64px.png");
                btnUnselectAll.Image = Image.FromFile(@".\Icon\SelectIcon\icons8_start_64px.png");

            }
            StaticPool.LoadOutput(cbOutputDevice);
            clbUnUsedZone.DisplayMember = "Text";
            clbUnUsedZone.ValueMember = "Value";
            clbInUsedZone.DisplayMember = "Text";
            clbInUsedZone.ValueMember = "Value";
        }
        #region:Controls in Form
        private void btnSelect_Click(object sender, EventArgs e)
        {
            foreach (dynamic item in clbUnUsedZone.CheckedItems)
            {
                var zone = item.Value as ZONE;
                RemoveFromRemoceZones(zone);
                AddToAddzones(zone);
                UnUsedZones.Remove(zone);
                InUsedZones.Add(zone);
            }
            ShowSelectedOutputDetail();
        }
        private void btnUnselect_Click(object sender, EventArgs e)
        {
            foreach (dynamic item in clbInUsedZone.CheckedItems)
            {
                var zone = item.Value as ZONE;
                AddToRemoveZones(zone);
                RemoveFromAddZones(zone);
                InUsedZones.Remove(zone);
                UnUsedZones.Add(zone);
            }
            ShowSelectedOutputDetail();
        }
        private void cbOutputDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddZones.Count > 0 || RemoveZones.Count > 0)
            {
                if (MessageBox.Show("Do you want to save change", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Save();
                }
            }
            AddZones = new List<ZONE>();
            RemoveZones = new List<ZONE>();
            UnUsedZones = new List<ZONE>();
            InUsedZones = new List<ZONE>();
            InitZones = new List<ZONE>();
            selectedOutputID = ((ListItem)cbOutputDevice.SelectedItem).Value;
            LoadOutputRelayIndex();
        }
        private void LoadSelectedOutputDetail()
        {
            AddZones = new List<ZONE>();
            RemoveZones = new List<ZONE>();
            UnUsedZones = new List<ZONE>();
            InUsedZones = new List<ZONE>();
            InitZones = new List<ZONE>();

            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                UnUsedZones.Add(zone);
            }
            string getCMD = $@"Select {tblOutputDetail.TBL_COL_ZONEID},{tblOutputDetail.TBL_COL_RELAY_INDEX}
                               from   {tblOutputDetail.TBL_NAME}
                               where  {tblOutputDetail.TBL_COL_OUTPUTID}    = '{selectedOutputID}'";// AND 
                                     // {tblOutputDetail.TBL_COL_RELAY_INDEX} =  {selectedRelayIndex}";
            DataTable dtOutputDetail = StaticPool.mdb.FillData(getCMD);
            if (dtOutputDetail != null)
            {
                if (dtOutputDetail.Rows.Count > 0)
                {
                    foreach (DataRow row in dtOutputDetail.Rows)
                    {
                        ZONE zone = StaticPool.zoneCollection.GetZONE(row[tblOutputDetail.TBL_COL_ZONEID].ToString());
                        if (zone != null)
                        {
                            if (Convert.ToInt32(row[tblOutputDetail.TBL_COL_RELAY_INDEX]) == selectedRelayIndex)
                            {
                                UnUsedZones.Remove(zone);
                                InUsedZones.Add(zone);
                                InitZones.Add(zone);
                            }
                            //else
                            //{
                            //    UnUsedZones.Remove(zone);
                            //}
                        }
                    }
                }
            }
        }
        private void ShowSelectedOutputDetail()
        {
            clbUnUsedZone.Items.Clear();
            clbInUsedZone.Items.Clear();
            foreach (var item in UnUsedZones)
            {
                clbUnUsedZone.Items.Add(new { Text = item.zoneName, Value = item });
            }
            foreach (var item in InUsedZones)
            {
                clbInUsedZone.Items.Add(new { Text = item.zoneName, Value = item });
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region:Internal
        private void Save()
        {
            Output output = StaticPool.outputCollection.GetOutput(selectedOutputID);
            output.SlotCounts[selectedRelayIndex] = clbInUsedZone.Items.Count;
            foreach (ZONE zone in AddZones)
            {
                string id = tblOutputDetail.InsertAndGetLastID(selectedOutputID, zone.Id, selectedRelayIndex);
                if (id != "")
                {
                    OutputDetail outputDetail = new OutputDetail();
                    outputDetail.ID = id;
                    outputDetail.OutputID = selectedOutputID;
                    outputDetail.ZoneID = zone.Id;
                    outputDetail.RelayIndex = selectedRelayIndex;
                    StaticPool.outputDetailCollection.Add(outputDetail);
                }
                else
                {
                    //Log
                }
            }
            foreach (ZONE zone in RemoveZones)
            {
                if (tblOutputDetail.Delete(zone.Id, selectedOutputID,selectedRelayIndex))
                {
                    foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
                    {
                        if (outputDetail.OutputID == selectedOutputID && outputDetail.ZoneID == zone.Id)
                        {
                            StaticPool.outputDetailCollection.Remove(outputDetail);
                            break;
                        }
                    }
                }
                else
                {
                    //Log
                }
            }
            foreach (ZONE zone in AddZones)
            {
                InitZones.Add(zone);
            }
            foreach (ZONE zone in RemoveZones)
            {
                InitZones.Remove(zone);
            }
            AddZones.Clear();
            RemoveZones.Clear();
        }
        //Add Zone List
        private void RemoveFromAddZones(ZONE zone)
        {
            foreach (ZONE _zone in AddZones)
            {
                if (_zone.Id == zone.Id)
                {
                    AddZones.Remove(_zone);
                    break;
                }
            }
        }
        private void AddToAddzones(ZONE zone)
        {
            bool isInInit = false;
            foreach (ZONE _zone in InitZones)
            {
                if (_zone.Id == zone.Id)
                {
                    isInInit = true;
                    break;
                }
            }
            if (!isInInit)
            {
                AddZones.Add(zone);
            }
        }
        //Remove Zone List
        private void RemoveFromRemoceZones(ZONE zone)
        {
            foreach (ZONE _zone in RemoveZones)
            {
                if (_zone.Id == zone.Id)
                {
                    RemoveZones.Remove(zone);
                    break;
                }
            }
        }
        private void AddToRemoveZones(ZONE zone)
        {
            bool isInInit = false;
            foreach (ZONE _zone in InitZones)
            {
                if (_zone.Id == zone.Id)
                {
                    isInInit = true;
                    break;
                }
            }
            if (isInInit)
            {
                RemoveZones.Add(zone);
            }
        }
        private void LoadOutputRelayIndex()
        {
            Output output = StaticPool.outputCollection.GetOutput(selectedOutputID);
            if (output != null)
            {
                int relayCount = 0;
                switch (output.OutputType)
                {
                    case (int)EM_OutputTypes.KZ_IO0808:
                        relayCount = 8;
                        break;
                    case (int)EM_OutputTypes.KZ_IO1616:
                        relayCount = 16;
                        break;
                }
                cbRelayIndex.Items.Clear();
                for (int i = 0; i < relayCount; i++)
                {
                    cbRelayIndex.Items.Add(i + 1);
                }
                cbRelayIndex.SelectedIndex = selectedRelayIndex - 1;
            }
        }
        #endregion

        private void cbRelayIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AddZones.Count > 0 || RemoveZones.Count > 0)
            {
                if (MessageBox.Show("Do you want to save change", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Save();
                }
            }
            AddZones = new List<ZONE>();
            RemoveZones = new List<ZONE>();
            UnUsedZones = new List<ZONE>();
            InUsedZones = new List<ZONE>();
            InitZones = new List<ZONE>();
            selectedRelayIndex = cbRelayIndex.SelectedIndex + 1;
            LoadSelectedOutputDetail();

            ShowSelectedOutputDetail();
            //if (AddZones.Count > 0 || RemoveZones.Count > 0)
            //{
            //    if (MessageBox.Show("Do you want to save change", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            //    {
            //        Save();
            //    }
            //}
            //AddZones = new List<ZONE>();
            //RemoveZones = new List<ZONE>();
            //UnUsedZones = new List<ZONE>();
            //InUsedZones = new List<ZONE>();
            //InitZones = new List<ZONE>();

            //foreach (ZONE zone in StaticPool.zoneCollection)
            //{
            //    UnUsedZones.Add(zone);
            //}
            //selectedOutputID = ((ListItem)cbOutputDevice.SelectedItem).Value;
            //LoadOutputRelayIndex();
            //selectedRelayIndex = cbRelayIndex.SelectedIndex + 1;
            //string getCMD = $@"Select {tblOutputDetail.TBL_COL_ZONEID} 
            //                   from   {tblOutputDetail.TBL_NAME}
            //                   where  {tblOutputDetail.TBL_COL_OUTPUTID}    = '{selectedOutputID}' AND 
            //                          {tblOutputDetail.TBL_COL_RELAY_INDEX} =  {selectedRelayIndex}";
            //DataTable dtOutputDetail = StaticPool.mdb.FillData(getCMD);
            //if (dtOutputDetail != null)
            //{
            //    if (dtOutputDetail.Rows.Count > 0)
            //    {
            //        foreach (DataRow row in dtOutputDetail.Rows)
            //        {
            //            ZONE zone = StaticPool.zoneCollection.GetZONE(row[tblOutputDetail.TBL_COL_ZONEID].ToString());
            //            if (zone != null)
            //            {
            //                UnUsedZones.Remove(zone);
            //                InUsedZones.Add(zone);
            //                InitZones.Add(zone);
            //            }
            //        }
            //    }
            //}
            //ReloadData();
        }

        private void btnSelect_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Select Checked Zone",btnSelect);
        }

        private void btnSelectAll_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Select All Zone", btnSelectAll);
        }

        private void btnUnselect_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Unselect Checked Zone", btnUnselect);
        }

        private void btnUnselectAll_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Unselect All Assigned Zone", btnUnselectAll);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (dynamic item in clbUnUsedZone.Items)
            {
                var zone = item.Value as ZONE;
                RemoveFromRemoceZones(zone);
                AddToAddzones(zone);
                UnUsedZones.Remove(zone);
                InUsedZones.Add(zone);
            }
            ShowSelectedOutputDetail();
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (dynamic item in clbInUsedZone.Items)
            {
                var zone = item.Value as ZONE;
                AddToRemoveZones(zone);
                RemoveFromAddZones(zone);
                InUsedZones.Remove(zone);
                UnUsedZones.Add(zone);
            }
            ShowSelectedOutputDetail();
        }
    }
}
