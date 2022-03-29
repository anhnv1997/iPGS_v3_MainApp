using iParking.Databases;
using iParking.Enums;
using iParking.Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmLED : Form
    {
        private string ID;
        public frmLED(string _ID)
        {
            InitializeComponent();
            this.ID = _ID;
        }
        private void frmLED_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            StaticPool.LoadArrowType(cbArrowDirection);
            for (int i = 0; i < 30; i++)
            {
                cbComport.Items.Add("COM" + i);
                cbAddress.Items.Add(i);
            }
            cbComport.SelectedIndex = 0;
            cbAddress.SelectedIndex = 0;
            StaticPool.LoadLedType(cbLedType);
            StaticPool.LoadColorTypes(cbColor);
            StaticPool.LoadColorTypes(cbZeroColor);
            if (this.ID != "")
            {
                Led led = StaticPool.ledCollection.GetLed(this.ID);
                if (led != null)
                {
                    txtID.Text = led.Id;
                    txtCode.Text = led.Code;
                    txtDescription.Text = led.Description;
                    txtName.Text = led.Name;
                    txtIP.Text = led.IPAddress;
                    txtPort.Text = led.Port + "";
                    cbLedType.SelectedIndex = led.LedType;
                    cbAddress.SelectedIndex = led.Address;
                    cbArrowDirection.SelectedIndex = (int)LedArrowDirection.GetLedArrowDirection(led.LedArrow);
                    cbColor.SelectedIndex = (int)LedColor.GetLedColor(led.Color);
                    cbZeroColor.SelectedIndex = (int)LedColor.GetLedColor(led.ZeroColor);
                    if (led.CommunicationType != (int)EM_CommunicationType.TCP_IP)
                    {
                        txtIP.Text = "";
                        for (int i = 0; i < cbComport.Items.Count; i++)
                        {
                            if (cbComport.Items[i].ToString() == led.IPAddress)
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
            if (CheckLedName())
            {
                string code = txtCode.Text;
                string description = txtDescription.Text;
                string name = txtName.Text;
                string ip = cbCommunication.SelectedIndex == (int)EM_CommunicationType.TCP_IP ? txtIP.Text : cbComport.Text;
                int port = Convert.ToInt32(txtPort.Text);
                int type = cbLedType.SelectedIndex;
                int address = cbAddress.SelectedIndex;
                byte arrow = LedArrowDirection.GetLedArrowDirection((EM_LedArrowDirection)cbArrowDirection.SelectedIndex);
                byte color = LedColor.GetLedColor((EM_LedColor)cbColor.SelectedIndex);
                byte zeroColor = LedColor.GetLedColor((EM_LedColor)cbZeroColor.SelectedIndex);
                Led led = new Led();
                if (this.ID != "")
                {
                    led = StaticPool.ledCollection.GetLed(this.ID);
                }
                led.Code = code;
                led.Description = description;
                led.Name = name;
                led.IPAddress = cbCommunication.SelectedIndex == (int)EM_CommunicationType.TCP_IP ? txtIP.Text : cbComport.Text;
                led.Port = port;
                led.LedType = type;
                led.Address = address;
                led.LedArrow = arrow;
                led.Color = color;
                led.ZeroColor = zeroColor;
                led.CommunicationType = cbCommunication.SelectedIndex;
                //Edit
                if (this.ID != "")
                {
                    if (!tblLed.Modify(this.ID, code, description, name, ip, port, type, address, arrow, color, zeroColor, cbCommunication.SelectedIndex))
                    {
                        MessageBox.Show("Edit led infor error, try again later");
                    }
                }
                //Add
                else
                {
                    string ledID = tblLed.InsertAndGetLastID(code, description, name, ip, port, type, address, arrow, color, zeroColor, cbCommunication.SelectedIndex);
                    if (ledID != "")
                    {
                        led.Id = ledID;

                        StaticPool.ledCollection.Add(led);
                    }
                    else
                    {
                        MessageBox.Show("Add led infor error, try again later");
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("This led name is already used, please try another name!");
                return;
            }
            
        }
        private bool CheckLedName()
        {
            if (this.ID != "")
            {
                foreach (Led led in StaticPool.ledCollection)
                {
                    if (led.Id != this.ID && led.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (Led led in StaticPool.ledCollection)
                {
                    if (led.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        private void cbCommunicaition_SelectedIndexChanged(object sender, EventArgs e)
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
        private void cbLedType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLedType.SelectedIndex)
            {
                case ((int)EM_LedTypes.LedMatrix_Serial):
                    cbCommunication.SelectedIndex = 0;
                    break;
                default:
                    cbCommunication.SelectedIndex = 1;
                    break;
            }
        }
    }
}

