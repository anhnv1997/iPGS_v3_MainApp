using iParking.Database;
using iParking.Databases;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmZoneGroup : Form
    {
        private string id;
        private bool currentisAutoOrderStatus = false;
        public frmZoneGroup(string _id)
        {
            InitializeComponent();
            this.id = _id;
        }
        private void frmZoneGroup_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            StaticPool.LoadFloorData(cbFloor);
            if (this.id != "")
            {
                txtID.Text = this.id;
                ZoneGroup zg = StaticPool.zoneGroupCollection.GetzgById(this.id);
                txtCode.Text = zg.Code;
                txtDescription.Text = zg.Description;
                txtName.Text = zg.Name;
                chbIsAutoOrder.Checked = zg.isAutoOrder;
                currentisAutoOrderStatus = zg.isAutoOrder;
                for (int i = 0; i < cbFloor.Items.Count; i++)
                {
                    if (((ListItem)cbFloor.Items[i]).Value == (zg.FloorID + ""))
                    {
                        cbFloor.SelectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                chbIsAutoOrder.Checked = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckZoneGroupName())
            {
                ZoneGroup zg = new ZoneGroup();
                if (this.id != "")
                {
                    zg = StaticPool.zoneGroupCollection.GetzgById(this.id);
                }
                zg.Code = txtCode.Text;
                zg.Description = txtDescription.Text;
                zg.FloorID = ((ListItem)cbFloor.SelectedItem).Value;
                zg.Name = txtName.Text;
                int isAutoOrder = chbIsAutoOrder.Checked == true ? 1 : 0;
                //Edit
                if (this.id != "")
                {
                    if (!tblZoneGroup.Modify(this.id, txtCode.Text, txtDescription.Text, ((ListItem)cbFloor.SelectedItem).Value, txtName.Text,isAutoOrder))
                    {
                        MessageBox.Show("Edit group infor error, try again later");
                        return;
                    }
                    if (chbIsAutoOrder.Checked != currentisAutoOrderStatus)
                    {
                        tblZone.UpdateOrderStatus(isAutoOrder, this.id);
                        foreach(ZONE zone in StaticPool.zoneCollection)
                        {
                            if(zone.ZoneGroupId == this.id)
                            {
                                zone.isAutoOrder = chbIsAutoOrder.Checked;
                            }
                        }
                    }
                }
                //Add
                else
                {
                    string zgID = tblZoneGroup.InsertAndGetLastID(txtCode.Text, txtDescription.Text, ((ListItem)cbFloor.SelectedItem).Value, txtName.Text,isAutoOrder);
                    if (zgID != "")
                    {
                        zg.Id = zgID;
                        StaticPool.zoneGroupCollection.Add(zg);
                    }
                    else
                    {
                        MessageBox.Show("Add group infor error, try again later");
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this group name is already used, please try another name!");
                return;
            }
           
        }
        private bool CheckZoneGroupName()
        {
            if (this.id != "")
            {
                foreach (ZoneGroup zoneGroup in StaticPool.zoneGroupCollection)
                {
                    if (zoneGroup.Id != this.id && zoneGroup.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (ZoneGroup zoneGroup in StaticPool.zoneGroupCollection)
                {
                    if (zoneGroup.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
