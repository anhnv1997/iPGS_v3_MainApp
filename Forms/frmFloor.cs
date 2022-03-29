using iParking.Databases;
using iParking.Object;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace iParking.Forms
{
    public partial class frmFloor : Form
    {
        private string id;
        public frmFloor(string _id)
        {
            InitializeComponent();
            this.id = _id;
        }
        private void frmFloor_Load(object sender, EventArgs e)
        {
            if(Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                button2.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }

            if (this.id != "")
            {
                txtID.Text = this.id;
                Floor floor = StaticPool.floorCollection.GetFloor(this.id);
                if (floor != null)
                {
                    txtCode.Text = floor.Code;
                    txtDescription.Text = floor.Description;
                    txtName.Text = floor.Name;
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckFloorName())
            {
                Floor floor = new Floor();
                if (this.id != "")
                {
                    floor = StaticPool.floorCollection.GetFloor(this.id);
                }
                floor.Code = txtCode.Text;
                floor.Description = txtDescription.Text;
                floor.Name = txtName.Text;
                floor.ZgCollection = new ZoneGroupCollection();
                //Edit
                if (this.id != "")
                {
                    if (!tblFloor.Modify(this.id, txtCode.Text, txtDescription.Text, txtName.Text))
                    {
                        MessageBox.Show("Edit floor infor error, try again later");
                    }
                }
                //Add
                else
                {
                    string floorID = tblFloor.InsertAndGetLastID(txtCode.Text, txtDescription.Text, txtName.Text);
                    if (floorID != "")
                    {
                        floor.Id = floorID;
                        StaticPool.floorCollection.Add(floor);
                    }
                    else
                    {
                        MessageBox.Show("Add floor infor error, try again later");
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("this floor name is already used, please try another name!");
                return;
            }
            
        }
        private bool CheckFloorName()
        {
            if (this.id != "")
            {
                foreach(Floor floor in StaticPool.floorCollection)
                {
                    if(floor.Id!=this.id&&floor.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                foreach (Floor floor in StaticPool.floorCollection)
                {
                    if (floor.Name == txtName.Text)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnSave.Image = Image.FromFile(txtDescription.Text);
        }
    }
}
