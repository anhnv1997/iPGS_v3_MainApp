using iParking.Database;
using iParking.Databases;
using iParking.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmZone : Form
    {
        private string id;
        public static int oldSelectedZCU;
        public frmZone(string _id)
        {
            InitializeComponent();
            this.id = _id;
        }
        private void frmZone_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            StaticPool.LoadGroupData(cbGroup);
            StaticPool.LoadZCUData(cbZCU);

            if (this.id != "")
            {
                txtID.Text = this.id;
                ZONE zone = StaticPool.zoneCollection.GetZONE(this.id);
                chbInUsed.Checked = zone.Status == -1 ? false : true;
                txtCode.Text = zone.Code;
                txtDescription.Text = zone.Description;
                txtName.Text = zone.zoneName;
                chbIsAutoOrder.Checked = zone.isAutoOrder;
                for (int i = 0; i < cbGroup.Items.Count; i++)
                {
                    if (((ListItem)cbGroup.Items[i]).Value == (zone.ZoneGroupId + ""))
                    {
                        cbGroup.SelectedIndex = i;
                        break;
                    }
                }
                for (int i = 0; i < cbZCU.Items.Count; i++)
                {
                    if (((ListItem)cbZCU.Items[i]).Value == (zone.ZCUId + ""))
                    {
                        cbZCU.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                if (cbZCU.Items.Count >= oldSelectedZCU)
                {
                    cbZCU.SelectedIndex = oldSelectedZCU;
                }
                chbIsAutoOrder.Checked = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckZoneName())
            {
                ZONE zone;
                if (this.id != "")
                {
                    zone = StaticPool.zoneCollection.GetZONE(this.id);
                }
                else
                {
                    zone = new ZONE();
                }
                string zcuID = ((ListItem)cbZCU.SelectedItem).Value;
                string groupID = ((ListItem)cbGroup.SelectedItem).Value;
                //int vehicleTypeID = cbVehicleType.SelectedIndex - 1;
                zone.Code = txtCode.Text;
                zone.Description = txtDescription.Text;
                zone.ZCUId = zcuID;
                zone.ZoneGroupId = groupID;
                zone.zoneName = txtName.Text;
                zone.ZcuIndex = Convert.ToInt32(cbZcuIndex.Text);
                zone.isAutoOrder = chbIsAutoOrder.Checked;
                int isAutoOrder = chbIsAutoOrder.Checked == true ? 1 : 0;
                if (chbInUsed.Checked)
                {
                    zone.Status = (int)EM_ZoneStatusType.DISCONNECT;
                }
                else
                {
                    zone.Status = -1;
                }
                //Edit
                if (this.id != "")
                {
                    if (!tblZone.Modify(this.id, txtCode.Text, txtDescription.Text, zcuID, groupID, zone.Status, zone.ImagePath, zone.PlateNum, zone.zoneName, zone.ZcuIndex, "",isAutoOrder))
                    {
                        MessageBox.Show("Edit zone infor error, try again later");
                        return;
                    }
                }
                //Add
                else
                {
                    //string zoneID = tblZone.InsertAndGetLastID(txtCode.Text, txtDescription.Text, zcuID, groupID, -1, "", "", txtName.Text, zone.ZcuIndex, "", vehicleTypeID);
                    string zoneID = tblZone.InsertAndGetLastID(txtCode.Text, txtDescription.Text, zcuID, groupID, zone.Status, "", "", txtName.Text, zone.ZcuIndex, "",isAutoOrder);
                    if (zoneID != "")
                    {
                        zone.Id = zoneID;
                        StaticPool.zoneCollection.Add(zone);
                    }
                    else
                    {
                        MessageBox.Show("Add zone infor error, try again later");
                        return;
                    }
                }

                if (isAutoOrder == 1)
                {
                    tblZoneGroup.UpdateIsAutoOrder(true, zone.ZoneGroupId);
                    foreach (ZoneGroup zg in StaticPool.zoneGroupCollection)
                    {
                        if (zg.Id == zone.ZoneGroupId)
                        {
                            zg.isAutoOrder = true;
                        }
                    }
                }
                else
                {
                    tblZoneGroup.CheckZoneAutoOrder(zone.ZoneGroupId);
                }

                oldSelectedZCU = cbZCU.SelectedIndex;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this zone name is already used, please try another name!");
                return;
            }
        }

        private bool CheckZoneName()
        {
            if (this.id != "")
            {
                foreach (ZONE zone in StaticPool.zoneCollection)
                {
                    if (zone.Id != this.id && zone.zoneName == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (ZONE zone in StaticPool.zoneCollection)
                {
                    if (zone.zoneName == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private void cbZCU_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<int> zcuIndexs = new List<int>();
            string selectedZCUId = ((ListItem)cbZCU.SelectedItem).Value;

            for (int i = 1; i <= 96; i++)
            {
                zcuIndexs.Add(i);
            }

            List<int> assignIndexs = new List<int>();

            if (this.id != "")
            {
                foreach (ZONE zone in StaticPool.zoneCollection)
                {
                    if (zone.ZCUId == selectedZCUId && zone.Id != this.id)
                        assignIndexs.Add(zone.ZcuIndex);
                }
            }
            else
            {
                foreach (ZONE zone in StaticPool.zoneCollection)
                {
                    if (zone.ZCUId == selectedZCUId && zone.Id != this.id)
                        assignIndexs.Add(zone.ZcuIndex);
                }
            }

            foreach (int assignIndex in assignIndexs)
            {
                zcuIndexs.Remove(assignIndex);
            }

            cbZcuIndex.DataSource = zcuIndexs;
            if (cbZcuIndex.Items.Count > 0)
            {
                cbZcuIndex.SelectedIndex = 0;
            }
        }
    }
}
