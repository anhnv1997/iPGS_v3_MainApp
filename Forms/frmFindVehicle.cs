using iParking.Object;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmFindVehicle : Form
    {
        List<string> mapIDs = new List<string>();
        string zoneID = "";
        string mapID = "";
        public frmFindVehicle()
        {
            InitializeComponent();
        }

        private void btnFindVehicle_Click(object sender, EventArgs e)
        {
            if(txtPlateNum.Text == "")
            {
                MessageBox.Show("Please Type Plate Number To Find");
                return;
            }

            bool isFound = false;
            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                if (zone.PlateNum == txtPlateNum.Text)
                {
                    //Load Map Which Has Vehicle
                    mapIDs.Clear();
                    foreach (MapDetail mapDetail in StaticPool.mapDetailCollection)
                    {
                        if (mapDetail.ZONEId == zone.Id)
                        {
                            mapIDs.Add(mapDetail.MapId);
                        }
                    }
                    //Show Result
                    this.zoneID = zone.Id;
                    txtMapPage.Enabled = true;
                    ShowResult(mapIDs);
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
            {
                this.mapID = "";
                this.zoneID = "";
                txtMapPage.Text = "0";
                lblMaxMapPage.Text = "0";
                btnNextMap.Enabled = false;
                btnLastMap.Enabled = false;
                btnPreviusMap.Enabled = false;
                btnPreviusMap.Enabled = false;
                picMap.Image = null;
                txtMapPage.Enabled = false;
                MessageBox.Show("Cant Find Vehicle");
            }
        }

        public void ShowResult(List<string> mapIds)
        {
            this.mapID = mapIDs[0];
            //Load All MAP
            int currenMapIndex = GetMapIndex(mapID);
            txtMapPage.Text = (currenMapIndex + 1) + "";
            lblMaxMapPage.Text = (mapIDs.Count) + "";
            if (mapIDs.Count >= 1)
            {
                int lastMapIndex = mapIDs.Count - 1;
                if (currenMapIndex == lastMapIndex)
                {
                    SetLastMap_Direction();
                }
                else if (currenMapIndex == 0)
                {
                    SetFirstMap_Direction();
                }
                else if (currenMapIndex > 0 && currenMapIndex < lastMapIndex - 1)
                {
                    SetNormalMap_Direction();
                }
                //Show First Map
                ShowMap(this.zoneID, mapIDs[0]);
            }
            else
            {
                btnNextMap.Enabled = false;
                btnLastMap.Enabled = false;
                btnPreviusMap.Enabled = false;
                btnPreviusMap.Enabled = false;
            }
            
        }
        public void ShowMap(string zoneid,string mapID)
        {
            LoadZoneInMap(mapID);
            Map selectedMap = StaticPool.mapCollection.GetMap(mapID);
            if (selectedMap != null)
            {
                if (File.Exists(selectedMap.ImagePath))
                {
                    picMap.Image = Image.FromFile(selectedMap.ImagePath);
                }
            }
        }

        #region:Internal
        private void SetNormalMap_Direction()
        {
            btnNextMap.Enabled = true;
            btnLastMap.Enabled = true;
            btnPreviusMap.Enabled = true;
            btnPreviusMap.Enabled = true;
        }
        private void SetFirstMap_Direction()
        {
            btnNextMap.Enabled = true;
            btnLastMap.Enabled = true;
            btnPreviusMap.Enabled = false;
            btnFirstMap.Enabled = false;
        }
        private void SetLastMap_Direction()
        {
            btnNextMap.Enabled = false;
            btnLastMap.Enabled = false;
            btnPreviusMap.Enabled = true;
            btnFirstMap.Enabled = true;
        }
        public void ShowNextMap(string currentMapID)
        {
            ClearOldMapData();
            int currenMapIndex = GetMapIndex(currentMapID);
            int lastMapIndex = mapIDs.Count - 1;
            if (currenMapIndex + 1 == lastMapIndex)
            {
                SetLastMap_Direction();
            }
            else
            {
                SetNormalMap_Direction();
            }
            this.mapID = StaticPool.mapCollection[currenMapIndex + 1].ID;
            ShowMap(this.zoneID,this.mapID);
        }
        public void ShowLastMap()
        {
            ClearOldMapData();
            SetLastMap_Direction();
            this.mapID = StaticPool.mapCollection[mapIDs.Count - 1].ID;
            ShowMap(this.zoneID,this.mapID);

        }
        public void ShowPreviousMap(string currentMapID)
        {
            ClearOldMapData();
            int currenMapIndex = GetMapIndex(currentMapID);
            int firstMapIndex = 0;
            if (currenMapIndex - 1 == firstMapIndex)
            {
                SetFirstMap_Direction();
            }
            else
            {
                SetNormalMap_Direction();
            }
            this.mapID = StaticPool.mapCollection[currenMapIndex - 1].ID;
            ShowMap(this.zoneID,this.mapID);
        }
        public void ShowFirstMap()
        {
            ClearOldMapData();
            SetFirstMap_Direction();
            this.mapID = StaticPool.mapCollection[0].ID;
            ShowMap(this.zoneID,this.mapID);
        }
        public int GetMapIndex(string mapID)
        {
            for (int i = 0; i < StaticPool.mapCollection.Count; i++)
            {
                if (StaticPool.mapCollection[i].ID == mapID)
                {
                    return i;
                }
            }
            return -1;
        }
        List<Control> DeleteControls = new List<Control>();
        private void ClearOldMapData()
        {
            foreach (Control control in this.Controls.OfType<ucZoneInMap>())
            {
                DeleteControls.Add(control);
            }
            foreach (Control deleteControl in DeleteControls)
            {
                this.Controls.Remove(deleteControl);
            }
            DeleteControls.Clear();
            txtMapPage.Text = (GetMapIndex(this.mapID) + 1) + "";
        }
        private void LoadZoneInMap(string mapID)
        {
            Map selectedMap = StaticPool.mapCollection.GetMap(mapID);
            if (selectedMap != null)
            {
                foreach (MapDetail mapDetail in StaticPool.mapDetailCollection)
                {
                    if (mapDetail.MapId == selectedMap.ID)
                    {
                        ZONE zone = StaticPool.zoneCollection.GetZONE(mapDetail.ZONEId);
                        if (zone != null)
                        {
                            if(zone.Id == zoneID)
                            {
                                int posX = (int)mapDetail.PosX;
                                int posY = (int)mapDetail.PosY;
                                int zoneWidth = mapDetail.zoneWidth;
                                int zoneHeight = mapDetail.zoneHeight;
                                StandardizedLocation(ref posX, ref posY, ref zoneWidth, ref zoneHeight);
                                CreateNewZoneInMap(zone, posX, posY, zoneWidth, zoneHeight);
                            }
                        }
                    }
                }
            }
        }
        private void StandardizedLocation(ref int posX, ref int posY, ref int width, ref int height)
        {
            int mapDetailWidth = Properties.Settings.Default.picMapDetailWidth;
            int mapDetailHeight = Properties.Settings.Default.picMapDetailHeght;
            int mapDetailPosX = Properties.Settings.Default.picMapDetailPosX;
            int mapDetailPosY = Properties.Settings.Default.picMapDetailPosY;

            int mapMainWidth = picMap.Width;
            int mapMainHeight = picMap.Height;
            int mapMainPosX = picMap.Location.X;
            int mapMainPosY = picMap.Location.Y;

            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;

            posX = (posX - mapDetailPosX) * mapMainWidth / mapDetailWidth + mapMainPosX;
            posY = (posY - mapDetailPosY) * mapMainHeight / mapDetailHeight + mapMainPosY + 40;
            width = width * mapMainWidth / mapDetailWidth;
            height = height * mapMainHeight / mapDetailHeight;
        }
        private void CreateNewZoneInMap(ZONE zone, int posX, int posY, int width, int height)
        {
            ucZoneInMap _ucZoneInMap = new ucZoneInMap(zone.zoneName);
            //Name
            SetUcZoneInMapInfor(zone, _ucZoneInMap);

            //Size
            _ucZoneInMap.Size = new Size(width, height);
            //Locatiom
            _ucZoneInMap.Location = new Point(posX, posY);

            //Information     
            if(this.zoneID == zone.Id)
            {
                this.Controls.Add(_ucZoneInMap);
                _ucZoneInMap.BringToFront();
                _ucZoneInMap.AssignBlink();
            }
        }
        private void SetUcZoneInMapInfor(ZONE zone, ucZoneInMap _ucZoneInMap)
        {
            _ucZoneInMap.Name = "uc:Zone :" + zone.Id;
            _ucZoneInMap.ZoneID = zone.Id;
            _ucZoneInMap.MapID = this.mapID;
        }

        #endregion

        private void btnNextMap_Click(object sender, EventArgs e)
        {
            ShowNextMap(this.mapID);
        }
        private void btnLastMap_Click(object sender, EventArgs e)
        {
            ShowLastMap();
        }
        private void btnPreviusMap_Click(object sender, EventArgs e)
        {
            ShowPreviousMap(this.mapID);
        }
        private void btnFirstMap_Click(object sender, EventArgs e)
        {
            ShowFirstMap();
        }

        private void txtMapPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(txtMapPage.Text, "[^0-9]"))
                {
                    MessageBox.Show("Hãy nhập số");
                    txtMapPage.Text = "1";
                }
                else
                {
                    if (Convert.ToInt32(txtMapPage.Text) <= 1)
                    {
                        txtMapPage.Text = "1";
                        ShowFirstMap();
                    }
                    else if (Convert.ToInt32(txtMapPage.Text) >= StaticPool.mapCollection.Count)
                    {
                        txtMapPage.Text = StaticPool.mapCollection.Count + "";
                        ShowLastMap();

                    }
                    else
                    {
                        ClearOldMapData();
                        SetNormalMap_Direction();
                        int mapIndex = Convert.ToInt32(txtMapPage.Text);
                        this.mapID = StaticPool.mapCollection[mapIndex].ID;
                        StaticPool.selectedMAPID = this.mapID;
                        ShowMap(this.zoneID,this.mapID);
                    }
                }
            }
        }

        private void frmFindVehicle_Load(object sender, EventArgs e)
        {

        }
    }
}

