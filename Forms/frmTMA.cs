using iParking.Databases;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmTMA : Form
    {
        string id ="";
        public frmTMA(string _id)
        {
            InitializeComponent();
            this.id = _id;
        }
        private void frmTMA_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                btnCancel.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            if (this.id != "")
            {
                txtID.Text = this.id;
                TMA_Server _TMA_Server = StaticPool.TMACollection.GetTMA_Server(this.id);
                if (_TMA_Server != null)
                {
                    txtCode.Text = _TMA_Server.Code;
                    txtDescription.Text = _TMA_Server.Description;
                    txtName.Text = _TMA_Server.Name;
                    txtIP.Text = _TMA_Server.Ip;
                    txtPort.Text = _TMA_Server.Port + "";
                    txtUsername.Text = _TMA_Server.Username;
                    txtPassword.Text = _TMA_Server.Password;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckTMAName())
            {
                TMA_Server _TMA_Server = new TMA_Server();
                if (this.id != "")
                {
                    _TMA_Server = StaticPool.TMACollection.GetTMA_Server(this.id);
                }
                _TMA_Server.Code = txtCode.Text;
                _TMA_Server.Description = txtDescription.Text;
                _TMA_Server.Name = txtName.Text;
                _TMA_Server.Ip = txtIP.Text;
                _TMA_Server.Port = Convert.ToInt32(txtPort.Text);
                _TMA_Server.Username = txtUsername.Text;
                _TMA_Server.Password = txtPassword.Text;
                //Edit
                if (this.id != "")
                {
                    if(!tblTMA_Server.Modify(this.id,_TMA_Server.Code, _TMA_Server.Description, _TMA_Server.Ip, _TMA_Server.Port, _TMA_Server.Name, _TMA_Server.Username, _TMA_Server.Password))
                    {
                        MessageBox.Show("Edit TMA Server infor error, try again later");
                    }
                }
                //Add
                else
                {
                    string insertID = tblTMA_Server.InsertAndGetLastID(_TMA_Server.Code, _TMA_Server.Description, _TMA_Server.Ip, _TMA_Server.Port, _TMA_Server.Name, _TMA_Server.Username, _TMA_Server.Password);
                    if (insertID != "")
                    {
                        _TMA_Server.Id = insertID;
                        StaticPool.TMACollection.Add(_TMA_Server);
                    }
                    else
                    {
                        MessageBox.Show("Add TMA Server infor error, try again later");
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this TMA Server name is already used, please try another name!");
                return;
            }
        }
        private bool CheckTMAName()
        {
            if (this.id != "")
            {
                foreach (TMA_Server _TMA_Server in StaticPool.TMACollection)
                {
                    if (_TMA_Server.Id != this.id && _TMA_Server.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (TMA_Server _TMA_Server in StaticPool.TMACollection)
                {
                    if (_TMA_Server.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
