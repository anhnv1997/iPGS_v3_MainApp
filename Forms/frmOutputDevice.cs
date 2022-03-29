using iParking.Databases;
using iParking.Object;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmOutputDevice : Form
    {
        private string id;
        public frmOutputDevice(string _ID)
        {
            InitializeComponent();
            this.id = _ID;
        }
        private void frmOutputDevice_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            StaticPool.LoadOutputType(cbOutputType);
            if (this.id != "")
            {
                Output output = StaticPool.outputCollection.GetOutput(this.id);
                txtCode.Text = output.Code;
                txtDescription.Text = output.Description;
                txtName.Text = output.Name;
                txtIP.Text = output.IPAddress;
                txtPort.Text = output.Port + "";
                cbOutputType.SelectedIndex = (output.OutputType);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckOutputName())
            {
                int type = cbOutputType.SelectedIndex;
                Output output = new Output(cbOutputType.SelectedIndex);
                if (this.id != "")
                {
                    output = StaticPool.outputCollection.GetOutput(this.id);
                }
                output.Name = txtName.Text;
                output.Code = txtCode.Text;
                output.Description = txtDescription.Text;
                output.IPAddress = txtIP.Text;
                output.Port = Convert.ToInt32(txtPort.Text);
                output.OutputType = cbOutputType.SelectedIndex;
                //Edit
                if (this.id != "")
                {
                    if (!tblOutput.Modify(this.id, txtCode.Text, txtDescription.Text, txtName.Text, txtIP.Text, Convert.ToInt32(txtPort.Text), cbOutputType.SelectedIndex))
                    {
                        MessageBox.Show("Edit output infor error, try again later");
                        return;
                    }
                }
                //Add
                else
                {
                    string outputID = tblOutput.InsertAndGetLastID(txtCode.Text, txtDescription.Text, txtName.Text, txtIP.Text, Convert.ToInt32(txtPort.Text), cbOutputType.SelectedIndex);
                    if (outputID != "")
                    {
                        output.ID = outputID;
                        StaticPool.outputCollection.Add(output);
                    }
                    else
                    {
                        MessageBox.Show("Add Output infor error, try again later");
                        return;
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this output name is already used, please try another name!");
                return;
            }
            
        }
        private bool CheckOutputName()
        {
            if (this.id != "")
            {
                foreach (Output output in StaticPool.outputCollection)
                {
                    if (output.ID != this.id && output.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (Output output in StaticPool.outputCollection)
                {
                    if (output.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
