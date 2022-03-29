using iParking.Database;
using iParking.Databases;
using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmLedDetail : Form
    {
        #region:Properties
        List<ZONE> UnUsedZones;
        List<ZONE> InUsedZones;
        List<ZONE> AddZones = new List<ZONE>();
        List<ZONE> RemoveZones = new List<ZONE>();
        List<ZONE> InitZones = new List<ZONE>();
        string selectedLEDID = "";
        #endregion
        public frmLedDetail()
        {
            InitializeComponent();
        }
        private void frmLedDetail_Load(object sender, EventArgs e)
        {
            StaticPool.LoadLedDevice(cbLedDevice);
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
                RemoveFromRemoveZones(zone);
                AddToAddZones(zone);
                UnUsedZones.Remove(zone);
                InUsedZones.Add(zone);
            }
            ReloadData();
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
            ReloadData();
        }
        private void cbLedDevice_SelectedIndexChanged(object sender, EventArgs e)
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

            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                UnUsedZones.Add(zone);
            }
            selectedLEDID = ((ListItem)cbLedDevice.SelectedItem).Value;
            string getCMD = $@"Select {tblLedDetail.TBL_NAME}.{tblLedDetail.TBL_COL_ZONEID} 
                               from {tblLedDetail.TBL_NAME}
                               where {tblLedDetail.TBL_NAME}.{tblLedDetail.TBL_COL_LedID} = '{selectedLEDID}' ";
            DataTable dtLedDetail = StaticPool.mdb.FillData(getCMD);
            if (dtLedDetail != null)
            {
                if (dtLedDetail.Rows.Count > 0)
                {
                    foreach (DataRow row in dtLedDetail.Rows)
                    {
                        ZONE zone = StaticPool.zoneCollection.GetZONE(row[tblLedDetail.TBL_COL_ZONEID].ToString());
                        if (zone != null)
                        {
                            UnUsedZones.Remove(zone);
                            InUsedZones.Add(zone);
                            InitZones.Add(zone);
                        }
                    }
                }
            }
            ReloadData();
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
            foreach (ZONE zONE in AddZones)
            {
                string id = tblLedDetail.InsertAndGetLastID(selectedLEDID, zONE.Id);
                if (id != "")
                {
                    LedDetail ledDetail = new LedDetail();
                    ledDetail.ID = id;
                    ledDetail.LedID = selectedLEDID;
                    ledDetail.ZoneID = zONE.Id;
                    StaticPool.ledDetailCollection.Add(ledDetail);
                }
                else
                {
                    //Log
                }
            }
            foreach (ZONE zONE in RemoveZones)
            {
                if (tblLedDetail.Delete(zONE.Id, selectedLEDID))
                {
                    foreach (LedDetail ledDetail in StaticPool.ledDetailCollection)
                    {
                        if (ledDetail.LedID == selectedLEDID && ledDetail.ZoneID == zONE.Id)
                        {
                            StaticPool.ledDetailCollection.Remove(ledDetail);
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
            Led led = StaticPool.ledCollection.GetLed(selectedLEDID);
            led.SlotCount = clbInUsedZone.Items.Count;
        }
        private void ReloadData()
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
        private void AddToAddZones(ZONE zone)
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
        private void RemoveFromRemoveZones(ZONE zone)
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
            //RemoveZones.Add(zone);
        }


        #endregion

        private void btnSelect_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("Select Checked Zone", btnSelect);
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
                RemoveFromRemoveZones(zone);
                AddToAddZones(zone);
                UnUsedZones.Remove(zone);
                InUsedZones.Add(zone);
            }
            ReloadData();
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
            ReloadData();
        }
    }
}
