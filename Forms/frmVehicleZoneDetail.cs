using iParking.Database;
using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using iParking.Databases;
using System.Drawing;

namespace iParking.Forms
{
    public partial class frmVehicleZoneDetail : Form
    {
        string selectID = "";
        List<ZONE> InUsedZones = new List<ZONE>();
        List<ZONE> UnUsedZones = new List<ZONE>();
        List<VehicleZoneDetail> InitVehicleZoneDetails = new List<VehicleZoneDetail>();
        List<VehicleZoneDetail> AddVehicleZoneDetails = new List<VehicleZoneDetail>();
        List<VehicleZoneDetail> ModifyVehicleZoneDetails = new List<VehicleZoneDetail>();
        List<VehicleZoneDetail> DeleteVehicleZoneDetails = new List<VehicleZoneDetail>();
        public frmVehicleZoneDetail()
        {
            InitializeComponent();
        }

        private void frmVehicleZoneDetail_Load(object sender, System.EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                btnCancel.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            StaticPool.LoadVehicleTypeData(cbVehicleType);
        }
        private void cbVehicleType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (AddVehicleZoneDetails.Count > 0 || DeleteVehicleZoneDetails.Count > 0 || ModifyVehicleZoneDetails.Count > 0)
            {
                if (MessageBox.Show("Do you want to save change?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    SaveAddData();
                    SaveDeleteData();
                    SaveModifyData();
                }
            }
            Refresh();
            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                UnUsedZones.Add(zone);
            }
            selectID = ((ListItem)cbVehicleType.SelectedItem).Value;
            DataTable dtInUsedZone = StaticPool.mdb.FillData($"Select tblvehicleZoneDetail.ID,tblvehicleZoneDetail.ZoneID,tblvehicleZoneDetail.VehicleID,tblvehicleZoneDetail.Priority from tblvehicleZoneDetail,tblZone where VehicleID = '{selectID}' AND tblZone.ID = tblvehicleZoneDetail.ZoneID order by tblZone.Sort");
            if (dtInUsedZone != null)
            {
                if (dtInUsedZone.Rows.Count > 0)
                {
                    foreach (DataRow row in dtInUsedZone.Rows)
                    {
                        string zoneID = row["ZoneID"].ToString();
                        int priority = Convert.ToInt32(row["Priority"].ToString());
                        ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
                        if (zone != null)
                        {
                            InUsedZones.Add(zone);
                            UnUsedZones.Remove(zone);
                            VehicleZoneDetail vehicleZoneDetail = new VehicleZoneDetail()
                            {
                                ID = row["ID"].ToString(),
                                ZoneID = row["ZoneID"].ToString(),
                                VehicleTypeID = row["VehicleID"].ToString(),
                                PiorityLevel = priority,
                            };
                            InitVehicleZoneDetails.Add(vehicleZoneDetail);
                        }
                    }
                }
            }

            foreach (ZONE zone in UnUsedZones)
            {
                string zoneID = zone.Id;
                string zoneName = zone.zoneName;
                string zoneGroupName = "";
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                zoneGroupName = zoneGroup == null ? "" : zoneGroup.Name;
                dgvUnUsedZone.Rows.Add(zoneID, dgvUnUsedZone.Rows.Count + 1, zoneName, zoneGroupName);
            }


            foreach (VehicleZoneDetail vehicleZoneDetail in InitVehicleZoneDetails)
            {
                string zoneId = vehicleZoneDetail.ZoneID;
                ZONE zone = StaticPool.zoneCollection.GetZONE(zoneId);
                string zoneName = "";
                string groupName = "";
                if (zone != null)
                {
                    zoneName = zone.zoneName;
                    ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                    groupName = zoneGroup == null ? "" : zoneGroup.Name;
                }
                dgvInUsedZone.Rows.Add(zoneId, dgvInUsedZone.Rows.Count + 1, zoneName, groupName, vehicleZoneDetail.PiorityLevel);
            }
        }

        private void Refresh()
        {
            AddVehicleZoneDetails.Clear();
            DeleteVehicleZoneDetails.Clear();
            ModifyVehicleZoneDetails.Clear();
            InitVehicleZoneDetails.Clear();
            InUsedZones.Clear();
            UnUsedZones.Clear();
            dgvInUsedZone.Rows.Clear();
            dgvUnUsedZone.Rows.Clear();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            string vehicleID = selectID;
            string zoneID = StaticPool.Id(dgvUnUsedZone);
            if (zoneID == "")
            {
                MessageBox.Show("Please Select an record!");
                return;
            }
            ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
            if (zone != null)
            {
                string zoneName = zone.zoneName;
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                string zoneGroupName = zoneGroup == null ? "" : zoneGroup.Name;
                string vehicleType = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(selectID).Name;
                frmEditVehicleZonePriority frm = new frmEditVehicleZonePriority();
                frm.ZoneName = zoneName;
                frm.GroupName = zoneGroupName;
                frm.VehicleType = vehicleType;
                frm.PriorityLevel = Properties.Settings.Default.currentVehicleZonePriority;
                frm.ShowDialog();
                //Edit Priority
                if (frm.DialogResult == DialogResult.OK)
                {
                    SelectOneZone(zoneID, zone, zoneName, zoneGroupName);
                    UnUsedZones.Remove(zone);
                    InUsedZones.Add(zone);
                }
            }
        }
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ZONE zone in UnUsedZones)
            {
                string zoneName = zone.zoneName;
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                string zoneGroupName = zoneGroup == null ? "" : zoneGroup.Name;
                SelectOneZone(zone.Id, zone, zoneName, zoneGroupName);
                InUsedZones.Add(zone);
            }
            UnUsedZones.Clear();
            dgvUnUsedZone.Rows.Clear();
        }
        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            string zoneID = dgvInUsedZone.CurrentRow.Cells[0].Value.ToString();
            if (zoneID == "")
            {
                MessageBox.Show("Please Select an record in InUsedList!");
                return;
            }
            ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
            if (zone != null)
            {
                VehicleZoneDetail vehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, InitVehicleZoneDetails);
                if (vehicleZoneDetail != null)
                {
                    //Add to DeletevehicleZoneDetails
                    if (GetVehicleZoneDetail(zoneID, selectID, DeleteVehicleZoneDetails) == null)
                    {
                        DeleteVehicleZoneDetails.Add(vehicleZoneDetail);
                    }
                }
                //Remove In ModifyvehicleZoneDetails
                vehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, ModifyVehicleZoneDetails);
                if (vehicleZoneDetail != null)
                {
                    ModifyVehicleZoneDetails.Remove(vehicleZoneDetail);
                }
                //Remove In AddvehicleZoneDetails
                vehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, AddVehicleZoneDetails);
                if (vehicleZoneDetail != null)
                {
                    AddVehicleZoneDetails.Remove(vehicleZoneDetail);
                }
                //Update View
                dgvInUsedZone.Rows.Remove(dgvInUsedZone.CurrentRow);
                AddDataToUnUsedView(zoneID);
                UnUsedZones.Add(zone);
                InUsedZones.Remove(zone);
            }
        }
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            AddVehicleZoneDetails.Clear();
            ModifyVehicleZoneDetails.Clear();
            DeleteVehicleZoneDetails.Clear();
            foreach (VehicleZoneDetail vehicleZoneDetail in InitVehicleZoneDetails)
            {
                DeleteVehicleZoneDetails.Add(vehicleZoneDetail);
            }
            foreach (ZONE zone in InUsedZones)
            {
                UnUsedZones.Add(zone);
                AddDataToUnUsedView(zone.Id);
            }
            //Update View
            dgvInUsedZone.Rows.Clear();
            InUsedZones.Clear();
        }

        private void dgvInUsedZone_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string zoneID = dgvInUsedZone.CurrentRow.Cells[0].Value.ToString();
            if (zoneID == "")
            {
                MessageBox.Show("Please Select an Record in InUsedList");
                return;
            }
            ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
            if (zone != null)
            {
                string zoneName = zone.zoneName;
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                string zoneGroupName = zoneGroup == null ? "" : zoneGroup.Name;
                string vehicleType = StaticPool.vehicleTypeCollection.GetVehicleTypeByID(selectID).Name;
                frmEditVehicleZonePriority frm = new frmEditVehicleZonePriority();
                frm.ZoneName = zoneName;
                frm.GroupName = zoneGroupName;
                frm.VehicleType = vehicleType;
                frm.PriorityLevel = Properties.Settings.Default.currentVehicleZonePriority;
                frm.ShowDialog();
                //Edit Priority
                if (frm.DialogResult == DialogResult.OK)
                {
                    VehicleZoneDetail vehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, InitVehicleZoneDetails);
                    if (vehicleZoneDetail == null)
                    {
                        VehicleZoneDetail addVehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, AddVehicleZoneDetails);
                        if (addVehicleZoneDetail != null)
                        {
                            addVehicleZoneDetail.PiorityLevel = Properties.Settings.Default.currentVehicleZonePriority;
                        }
                    }
                    else
                    {
                        if (vehicleZoneDetail.PiorityLevel != Properties.Settings.Default.currentVehicleZonePriority)
                        {
                            VehicleZoneDetail updateVehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, ModifyVehicleZoneDetails);
                            if (updateVehicleZoneDetail != null)
                            {
                                updateVehicleZoneDetail.PiorityLevel = Properties.Settings.Default.currentVehicleZonePriority;
                            }
                            else
                            {
                                updateVehicleZoneDetail = new VehicleZoneDetail()
                                {
                                    ID = vehicleZoneDetail.ID,
                                    ZoneID = vehicleZoneDetail.ZoneID,
                                    VehicleTypeID = selectID,
                                    PiorityLevel = Properties.Settings.Default.currentVehicleZonePriority
                                };
                                ModifyVehicleZoneDetails.Add(updateVehicleZoneDetail);
                            }
                        }
                    }
                }
            }
        }

        #region:Search
        private void txtZoneName_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtZoneName.Text;
            try
            {
                var re = from zone in UnUsedZones
                         where zone.zoneName.ToLower().Contains(searchValue.ToLower())
                         select zone;
                dgvUnUsedZone.Rows.Clear();
                foreach (ZONE zone in re)
                {
                    string zoneID = zone.Id;
                    string zoneName = zone.zoneName;
                    string zoneGroupName = "";
                    ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                    zoneGroupName = zoneGroup == null ? "" : zoneGroup.Name;
                    dgvUnUsedZone.Rows.Add(zoneID, dgvUnUsedZone.Rows.Count + 1, zoneName, zoneGroupName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            string searchValue = txtGroupName.Text;
            try
            {
                var re = from zone in UnUsedZones
                         where StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId) != null && StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId).Name.ToLower().Contains(searchValue.ToLower())
                         select zone;
                dgvUnUsedZone.Rows.Clear();
                foreach (ZONE zone in re)
                {
                    string zoneID = zone.Id;
                    string zoneName = zone.zoneName;
                    string zoneGroupName = "";
                    ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                    zoneGroupName = zoneGroup == null ? "" : zoneGroup.Name;
                    dgvUnUsedZone.Rows.Add(zoneID, dgvUnUsedZone.Rows.Count + 1, zoneName, zoneGroupName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region:Internal
        private void AddDataToUnUsedView(string zoneID)
        {
            ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
            if (zone != null)
            {
                string zoneName = zone.zoneName;
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                string groupName = zoneGroup == null ? "" : zoneGroup.Name;
                dgvUnUsedZone.Rows.Add(zoneID, dgvUnUsedZone.Rows.Count + 1, zoneName, groupName);
            }
        }

        private VehicleZoneDetail GetVehicleZoneDetail(string zoneID, string vehicleID, List<VehicleZoneDetail> vehicleZoneDetails)
        {
            foreach (VehicleZoneDetail vehicleZoneDetail in vehicleZoneDetails)
            {
                if (vehicleZoneDetail.ZoneID == zoneID && vehicleZoneDetail.VehicleTypeID == vehicleID)
                {
                    return vehicleZoneDetail;
                }
            }
            return null;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAddData();
            SaveModifyData();
            SaveDeleteData();
            this.DialogResult = DialogResult.OK;
        }
        private void SaveModifyData()
        {
            foreach (VehicleZoneDetail vehicleZoneDetail in ModifyVehicleZoneDetails)
            {
                if (tblVehicleZoneDetail.Modify(vehicleZoneDetail.ID, vehicleZoneDetail.ZoneID, vehicleZoneDetail.VehicleTypeID, vehicleZoneDetail.PiorityLevel))
                {
                    VehicleZoneDetail updateVehicleZoneDetail = StaticPool.vehicleZoneDetails.GetVehicleZoneDetail(vehicleZoneDetail.VehicleTypeID, vehicleZoneDetail.ZoneID);
                    if (updateVehicleZoneDetail != null)
                    {
                        updateVehicleZoneDetail.PiorityLevel = vehicleZoneDetail.PiorityLevel;
                    }
                }
            }
        }
        private void SaveDeleteData()
        {
            foreach (VehicleZoneDetail vehicleZoneDetail in DeleteVehicleZoneDetails)
            {
                if (tblVehicleZoneDetail.Delete(vehicleZoneDetail.ZoneID, vehicleZoneDetail.VehicleTypeID))
                {
                    StaticPool.vehicleZoneDetails.Remove(vehicleZoneDetail);
                }
            }
            DeleteVehicleZoneDetails.Clear();
        }
        private void SaveAddData()
        {
            foreach (VehicleZoneDetail vehicleZoneDetail in AddVehicleZoneDetails)
            {
                string vehicleZoneDetailID = tblVehicleZoneDetail.InsertAndGetLastID(vehicleZoneDetail.ZoneID, vehicleZoneDetail.VehicleTypeID, vehicleZoneDetail.PiorityLevel);
                if (vehicleZoneDetailID != "")
                {
                    VehicleZoneDetail addVehicleZoneDetail = new VehicleZoneDetail()
                    {
                        ID = vehicleZoneDetailID,
                        ZoneID = vehicleZoneDetail.ZoneID,
                        VehicleTypeID = vehicleZoneDetail.VehicleTypeID,
                        PiorityLevel = vehicleZoneDetail.PiorityLevel
                    };
                    StaticPool.vehicleZoneDetails.Add(vehicleZoneDetail);
                }
            }
            AddVehicleZoneDetails.Clear();
        }

        private void SelectOneZone(string zoneID, ZONE zone, string zoneName, string zoneGroupName)
        {
            //Check Is In InitVehicleZoneDetails
            VehicleZoneDetail vehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, InitVehicleZoneDetails);
            if (vehicleZoneDetail != null)
            {
                //Check Co Thong Tin Thay Doi Khong
                if (vehicleZoneDetail.PiorityLevel != Properties.Settings.Default.currentVehicleZonePriority)
                {
                    //Co Thong Tin Thay Doi==>Add Data To UpdateVehicleZoneDetails
                    //Kiểm tra đã có dữ liệu trong UpdateVehicleZoneDetails
                    VehicleZoneDetail updateVehicleZoneDetail = GetVehicleZoneDetail(zoneID, selectID, ModifyVehicleZoneDetails);
                    //Có ==> Update lại thông tin trong ModifyList
                    if (updateVehicleZoneDetail != null)
                    {
                        updateVehicleZoneDetail.PiorityLevel = Properties.Settings.Default.currentVehicleZonePriority;
                    }
                    //Chưa có ==> Thêm thông tin vào Modify List
                    else
                    {
                        VehicleZoneDetail _updateVehicleZoneDetail = new VehicleZoneDetail()
                        {
                            ID = vehicleZoneDetail.ID,
                            ZoneID = vehicleZoneDetail.ZoneID,
                            VehicleTypeID = vehicleZoneDetail.VehicleTypeID,
                            PiorityLevel = Properties.Settings.Default.currentVehicleZonePriority
                        };
                        ModifyVehicleZoneDetails.Add(_updateVehicleZoneDetail);
                    }
                }

            }
            //Chưa có trong InitVehicleZoneDetails
            else
            {
                //Thêm VehicleZoneDetail vào Danh Sách Dữ Liệu Cần Thêm
                VehicleZoneDetail addedVehicleDetail = new VehicleZoneDetail()
                {
                    ID = Guid.NewGuid().ToString(),
                    ZoneID = zone.Id,
                    VehicleTypeID = selectID,
                    PiorityLevel = Properties.Settings.Default.currentVehicleZonePriority
                };
                AddVehicleZoneDetails.Add(addedVehicleDetail);
                //Xóa trong ModifyList
                VehicleZoneDetail vehicleZoneDetail1 = GetVehicleZoneDetail(zoneID, selectID, ModifyVehicleZoneDetails);
                if (vehicleZoneDetail1 != null)
                {
                    ModifyVehicleZoneDetails.Remove(vehicleZoneDetail1);
                }
            }
            //Xóa thông tin trong DeleteList nếu có
            foreach (VehicleZoneDetail deleteVehicleZoneDetail in DeleteVehicleZoneDetails)
            {
                if (deleteVehicleZoneDetail.ZoneID == zoneID && deleteVehicleZoneDetail.VehicleTypeID == selectID)
                {
                    DeleteVehicleZoneDetails.Remove(deleteVehicleZoneDetail);
                    break;
                }
            }
            //Cập nhật hiển thị
            dgvInUsedZone.Rows.Add(zoneID, dgvInUsedZone.Rows.Count + 1, zoneName, zoneGroupName, Properties.Settings.Default.currentVehicleZonePriority);
            dgvUnUsedZone.Rows.Remove(dgvUnUsedZone.CurrentRow);

        }

    }
}
