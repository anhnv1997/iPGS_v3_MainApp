using iParking.Databases;
using iParking.Object;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmMAP : Form
    {
        private string id;
        public frmMAP(string _id)
        {
            InitializeComponent();
            this.id = _id;
        }
        private void frmMAP_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSelectMap.Image = Image.FromFile(@".\Icon\GeneralIcon\My-Pictures-icon.png");
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            if (this.id != "")
            {
                Map map = StaticPool.mapCollection.GetMap(this.id);
                txtCode.Text = map.Code;
                txtDescription.Text = map.Description;
                txtName.Text = map.Name;
                txtImagePath.Text = map.ImagePath;
                string fullPath = Path.GetFullPath(map.ImagePath,Application.StartupPath);
                if (File.Exists(fullPath))
                {
                    picMAP.Image = Image.FromFile(fullPath);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckMapName())
            {
                Map map = new Map();
                if (this.id != "")
                {
                    map = StaticPool.mapCollection.GetMap(this.id);
                }
                map.Code = txtCode.Text;
                map.Description = txtDescription.Text;
                map.Name = txtName.Text;
                map.ImagePath = txtImagePath.Text;
                //Edit
                if (this.id != "")
                {
                    if (!tblMAP.Modify(this.id, txtCode.Text, txtDescription.Text, txtName.Text, txtImagePath.Text))
                    {
                        MessageBox.Show("Edit map infor error, try again later");
                    }
                }
                //Add
                else
                {
                    string mapID = tblMAP.InsertAndGetLastID(txtCode.Text, txtDescription.Text, txtName.Text, txtImagePath.Text);
                    if (mapID != "")
                    {
                        map.ID = mapID;
                        StaticPool.mapCollection.Add(map);
                    }
                    else
                    {
                        MessageBox.Show("Add map infor error, try again later");
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this map name is already used, please try another name!");
                return;
            }
            
        }

        private bool CheckMapName()
        {
            if (this.id != "")
            {
                foreach (Map map in StaticPool.mapCollection)
                {
                    if (map.ID != this.id && map.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (Map map in StaticPool.mapCollection)
                {
                    if (map.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void btnSelectMap_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (File.Exists(ofd.FileName))
                    {
                        picMAP.Image = Image.FromFile(ofd.FileName);
                        txtImagePath.Text = ofd.FileName;
                        txtImagePath.Text = Path.GetRelativePath(Application.StartupPath, ofd.FileName);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
