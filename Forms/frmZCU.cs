using iParking.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using iParking.Databases;
using System.Windows.Forms;
using iParking.Database;

namespace iParking.Forms
{
    public partial class frmZCU : Form
    {
        private string id;
        private int defauleHeight;
        public frmZCU(string _id)
        {
            InitializeComponent();
            this.id = _id;
            defauleHeight = this.Size.Height;
        }
        private void frmZCU_Load(object sender, EventArgs e)
        {
            cb_TMA_Cam_Type.SelectedIndex = 0;
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                btnCancel.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            StaticPool.LoadCCUData(cbCCU);
            cbCCU.SelectedIndex = 0;
            for (int i = 0; i < 30; i++)
            {
                cbAddress.Items.Add(i);
                cbComport.Items.Add("COM" + i);
            }
            cbAddress.SelectedIndex = 0;
            cbComport.SelectedIndex = 0;
            StaticPool.LoadZcuTypes(cbType);
            StaticPool.LoadTMA(cbTMA_NAME);
            for (int i = 1; i <= 100; i++)
            {
                cbTMA_INDEX.Items.Add(i);
            }
            List<int> deleteList = new List<int>();
            if (cbTMA_NAME.Items.Count > 0)
            {
                foreach (ZCU zcu in StaticPool.zcuCollection)
                {
                    if (zcu.Id != this.id)
                    {
                        if (zcu.TMA_ID == ((ListItem)cbTMA_NAME.SelectedItem).Value)
                        {
                            deleteList.Add(zcu.TMA_Index);
                        }
                    }
                }
                foreach (int index in deleteList)
                {
                    cbTMA_INDEX.Items.Remove(index);
                }
            }

            cbTMA_INDEX.SelectedIndex = 0;
            cbCommunication.SelectedIndex = 0;
            if (this.id != "")
            {
                ZCU zcu = StaticPool.zcuCollection.GetZCUById(this.id);
                txtCode.Text = zcu.Code;
                txtDescription.Text = zcu.Description;
                cbCCU.SelectedIndex = 0;
                cbType.SelectedIndex = zcu.Type;
                cbAddress.SelectedIndex = zcu.Address;
                txtIP.Text = zcu.IPAddress;
                txtPort.Text = zcu.Port + "";
                txtName.Text = zcu.ZcuName;
                txtUsername.Text = zcu.Username;
                txtPassword.Text = zcu.Password;

                txtCam_TMA_IP.Text = zcu.IPAddress;
                txtCam_TMA_Port.Text = zcu.Port + "";
                txtCamTMA_Username.Text = zcu.Username;
                txtCam_TMA_Password.Text = zcu.Password;


                cbCommunication.SelectedIndex = zcu.CommunicationType;
                cb_TMA_Cam_Type.SelectedIndex = zcu.TMA_Cam_Type;
                if (zcu.TMA_ID != "")
                {
                    for (int i = 0; i < cbTMA_NAME.Items.Count; i++)
                    {
                        if (((ListItem)cbTMA_NAME.Items[i]).Value == zcu.TMA_ID)
                        {
                            cbTMA_NAME.SelectedIndex = i;
                        }
                    }
                    for (int i = 0; i < cbTMA_INDEX.Items.Count; i++)
                    {
                        if (cbTMA_INDEX.Items[i].ToString() == zcu.TMA_Index.ToString())
                        {
                            cbTMA_INDEX.SelectedIndex = i;
                        }
                    }
                }
                else
                {
                    if (zcu.CommunicationType != (int)EM_CommunicationType.TCP_IP)
                    {
                        txtIP.Text = "";
                        for (int i = 0; i < cbComport.Items.Count; i++)
                        {
                            if (cbComport.Items[i].ToString() == zcu.IPAddress)
                            {
                                cbComport.SelectedIndex = i;
                            }
                        }
                    }

                }

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckZCUName())
            {
                int type = cbType.SelectedIndex;
                ZCU zcu = new ZCU();
                if (this.id != "")
                {
                    zcu = StaticPool.zcuCollection.GetZCUById(this.id);
                }
                zcu.ZcuName = txtName.Text;
                zcu.Code = txtCode.Text;
                zcu.Description = txtDescription.Text;
                zcu.CCUId = ((ListItem)cbCCU.SelectedItem).Value;
                zcu.Type = type;
                if (cbType.SelectedIndex == (int)EM_ZcuTypes.AI_TMA)
                {
                    zcu.IPAddress = txtCam_TMA_IP.Text;
                    zcu.Port = Convert.ToInt32(txtCam_TMA_Port.Text);
                    zcu.CommunicationType = (int)EM_CommunicationType.TCP_IP;
                    zcu.Username = txtCamTMA_Username.Text;
                    zcu.Password = txtCam_TMA_Password.Text;
                    zcu.Address = 0;
                    zcu.TMA_ID = ((ListItem)cbTMA_NAME.SelectedItem).Value;
                    zcu.TMA_Index = Convert.ToInt32(cbTMA_INDEX.Text);
                    zcu.TMA_Cam_Type = cb_TMA_Cam_Type.SelectedIndex;
                }
                else
                {
                    zcu.Address = cbAddress.SelectedIndex; ;
                    zcu.IPAddress = cbCommunication.SelectedIndex == (int)EM_CommunicationType.TCP_IP ? txtIP.Text : cbComport.Text;
                    zcu.Port = txtPort.Text == "" ? 0 : Convert.ToInt32(txtPort.Text);
                    zcu.CommunicationType = cbCommunication.SelectedIndex;
                    zcu.Username = txtUsername.Text;
                    zcu.Password = txtPassword.Text;
                    zcu.TMA_ID = "";
                    zcu.TMA_Index = 0;
                    zcu.TMA_Cam_Type = 0;
                }

                //Edit
                if (this.id != "")
                {
                    if (!tblZCU.Modify(zcu))
                    {
                        MessageBox.Show("Edit zcu infor error, try again later");
                        return;
                    }
                }
                //Add
                else
                {
                    string zcuID = tblZCU.InsertAndGetLastID(zcu);
                    if (zcuID != "")
                    {
                        zcu.Id = zcuID;
                        StaticPool.zcuCollection.Add(zcu);
                    }
                    else
                    {
                        MessageBox.Show("Add ZCU infor error, try again later");
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this zcu name is already used, please try another name!");
                return;
            }

        }
        private bool CheckZCUName()
        {
            if (this.id != "")
            {
                foreach (ZCU zcu in StaticPool.zcuCollection)
                {
                    if (zcu.Id != this.id && zcu.ZcuName == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (ZCU zcu in StaticPool.zcuCollection)
                {
                    if (zcu.ZcuName == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbType.SelectedIndex)
            {
                case ((int)EM_ZcuTypes.AI_TMA):
                    this.Size = new Size(this.Size.Width, defauleHeight);
                    panelTMA_Infor.BringToFront();
                    panelTMA_Infor.Visible = true;
                    panel_IP_COM_INFOR.SendToBack();
                    btnSave.BringToFront();
                    btnCancel.BringToFront();
                    break;
                case ((int)EM_ZcuTypes.Camera):
                    this.Size = new Size(this.Size.Width, defauleHeight * 94 / 100);
                    cbCommunication.SelectedIndex = 1;
                    panelTMA_Infor.SendToBack();
                    panelTMA_Infor.Visible = false;
                    panel_IP_COM_INFOR.BringToFront();
                    break;
                case ((int)EM_ZcuTypes.FuC02):
                    this.Size = new Size(this.Size.Width, defauleHeight * 94 / 100);
                    cbCommunication.SelectedIndex = 0;
                    panelTMA_Infor.Visible = false;
                    panelTMA_Infor.SendToBack();
                    panel_IP_COM_INFOR.BringToFront();
                    break;
            }
        }
        private void cbCommunication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCommunication.SelectedIndex == 1)
            {
                lblIP.Text = "IPAddress";
                lblPort.Text = "Port";
                txtIP.BringToFront();
            }
            else
            {
                lblIP.Text = "ComPort";
                lblPort.Text = "Baudrate";
                cbComport.BringToFront();
            }
        }

    }
}
