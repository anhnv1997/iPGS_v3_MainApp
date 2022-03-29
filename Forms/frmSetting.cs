using System;
using System.Drawing;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        private void frmSetting_Load(object sender, EventArgs e)
        {
            if (Screen.PrimaryScreen.Bounds.Width == 3840)
            {
                btnSave.Image = Image.FromFile(@".\Icon\GeneralIcon\Save-icon_64.png");
                btnCancel.Image = Image.FromFile(@".\Icon\GeneralIcon\Actions-application-exit-icon_64.png");
            }
            ucFloor1.ShowDataInGridView();

            ucGroup1.ShowDataInGridView();

            uczcu1.ShowDataInGridView();

            ucZone1.ShowDataInGridView();

            ucmap1.ShowDataInGridView();

            ucOutput1.ShowDataInGridView();

            ucLed1.ShowDataInGridView();

            ucVehicleType1.ShowDataInGridView();

            uctma1.ShowDataInGridView();

            numOrderStateHoldTime.Value = Properties.Settings.Default.maxOrderStateKeepTime;
            numRefreshStateTime.Value = Properties.Settings.Default.minRefreshStateTime;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabOption.SelectedTab.Name)
            {
                case "tabGroup":
                    if (StaticPool.isChangeZoneGroup)
                    {
                        ucGroup1.ShowDataInGridView();
                        StaticPool.isChangeZoneGroup = false;
                    }
                    break;
                case "tabMAP":
                    if (StaticPool.isChangeMap)
                    {
                        ucmap1.ShowDataInGridView();
                        StaticPool.isChangeMap = false;
                    }
                    break;
                case "tabTMA":
                    if (StaticPool.isChangeTMA)
                    {
                        uctma1.ShowDataInGridView();
                        StaticPool.isChangeTMA = false;
                    }
                    break;
                case "tabZCU":
                    if (StaticPool.isChangeZcu)
                    {
                        uczcu1.ShowDataInGridView();
                        StaticPool.isChangeZcu = false;
                    }
                    break;
                case "tabZONE":
                    if (StaticPool.isChangeZone)
                    {
                        ucZone1.ShowDataInGridView();
                        StaticPool.isChangeZone = false;
                    }
                    break;
                case "tabOutput":
                    if (StaticPool.isChangeOutput)
                    {
                        ucOutput1.ShowDataInGridView();
                        StaticPool.isChangeOutput = false;
                    }
                    break;
                case "tabLED":
                    if (StaticPool.isChangeLed)
                    {
                        ucLed1.ShowDataInGridView();
                        StaticPool.isChangeLed = false;
                    }
                    break;
                case "tabVehicleType":
                    if (StaticPool.isChangeVehicleType)
                    {
                        ucVehicleType1.ShowDataInGridView();
                        StaticPool.isChangeVehicleType = false;
                    }
                    break;
                //case "tabTest":
                //    GeneralUC uc = new GeneralUC();
                //    uc.SetType(typeof(frmFloor));
                //    uc.Dock = DockStyle.Fill;
                //    tabTest.Controls.Add(uc);
                //    break;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.maxOrderStateKeepTime = (int)numOrderStateHoldTime.Value;
            Properties.Settings.Default.minRefreshStateTime = (int)numRefreshStateTime.Value;
            Properties.Settings.Default.Save();
        }
    }
}




