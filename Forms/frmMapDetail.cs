using iParking.Databases;
using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace iParking.Forms
{
    public partial class frmMapDetail : Form
    {
        #region:Properties
        private string mapID = "";
        private List<MapUpdateInfor> mapModifyInfors = new List<MapUpdateInfor>();
        private List<MapUpdateInfor> mapAddInfors = new List<MapUpdateInfor>();
        private List<MapUpdateInfor> mapDeleteInfors = new List<MapUpdateInfor>();
        private List<ucZoneInMap> ucZoneInMaps = new List<ucZoneInMap>();

        private const int IMAGE_HOME_INDEX = 0;
        private const int IMAGE_CAR_ADD_INDEX = 1;
        private const int IMAGE_CAR_DELETE_INDEX = 2;

        #endregion

        #region: Form
        public frmMapDetail(string _mapID)
        {
            InitializeComponent();
            this.TopLevel = true;
            this.mapID = _mapID;
            picMap.AllowDrop = true;
            Image image1 = Image.FromFile(Application.StartupPath + @"\Icon\MapIcon\home_64.png");
            Image image2 = Image.FromFile(Application.StartupPath + @"\Icon\MapIcon\icons8_Add_car_64.png");
            Image image3 = Image.FromFile(Application.StartupPath + @"\Icon\MapIcon\icons8_Delete_car_64px.png");
            imageMapList.Images.Add(image1);
            imageMapList.Images.Add(image2);
            imageMapList.Images.Add(image3);
            treeView_zone.ImageList = imageMapList;
            treeView_zone.ExpandAll();
            treeView_zone.ItemDrag += TreeView_zone_ItemDrag;
            treeView_zone.AllowDrop = false;
            picMap.DragEnter += PicMap_DragEnter;
            picMap.DragDrop += PicMap_DragDrop; ;
        }
        private void frmMapDetail_Load(object sender, EventArgs e)
        {
            LoadZoneTreeview();
            ShowMapDataInGridview();
            ShowSelectedMap();
            ShowZoneInMap(this.mapID);
        }
        private void frmMapDetail_Resize(object sender, EventArgs e)
        {
            //Responsive
            foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
            {
                ucZoneInMap _ucZoneInMap = (ucZoneInMap)control;
                bool isHaveUpdateAddData = false;
                foreach (MapUpdateInfor mapDetail in mapAddInfors)
                {
                    if (mapDetail.ZoneID == _ucZoneInMap.ZoneID && mapDetail.MapID == _ucZoneInMap.MapID)
                    {
                        int zoneWidth = mapDetail.Width;
                        int zoneHeight = mapDetail.Height;
                        int mapHeight = mapDetail.MapHeight;
                        int mapWidth = mapDetail.mapWidth;
                        double ratioWidth = (double)panelMap.Width / (double)mapWidth;
                        double ratioHeight = (double)panelMap.Height / (double)mapHeight;
                        _ucZoneInMap.Size = new Size((int)Math.Round(zoneWidth * ratioWidth), (int)Math.Round(zoneHeight * ratioHeight));
                        //Location
                        _ucZoneInMap.Location = new Point((int)Math.Round(mapDetail.PosX * ratioWidth), (int)Math.Round(mapDetail.PosY * ratioHeight));
                        _ucZoneInMap.BringToFront();
                        isHaveUpdateAddData = true;
                        break;
                    }
                }
                if (!isHaveUpdateAddData)
                {
                    bool isHaveUpdateModifyData = false;
                    foreach (MapUpdateInfor mapDetail in mapModifyInfors)
                    {
                        if (mapDetail.ZoneID == _ucZoneInMap.ZoneID && mapDetail.MapID == _ucZoneInMap.MapID)
                        {
                            int zoneWidth = mapDetail.Width;
                            int zoneHeight = mapDetail.Height;
                            int mapHeight = mapDetail.MapHeight;
                            int mapWidth = mapDetail.mapWidth;

                            double ratioWidth = (double)panelMap.Width / (double)mapWidth;
                            double ratioHeight = (double)panelMap.Height / (double)mapHeight;
                            _ucZoneInMap.Size = new Size((int)Math.Round(zoneWidth * ratioWidth), (int)Math.Round(zoneHeight * ratioHeight));
                            //Location
                            _ucZoneInMap.Location = new Point((int)Math.Round(mapDetail.PosX * ratioWidth), (int)Math.Round(mapDetail.PosY * ratioHeight));
                            isHaveUpdateModifyData = true;
                            break;
                        }
                    }

                    if (!isHaveUpdateModifyData)
                    {
                        foreach (MapDetail mapDetail in StaticPool.mapDetailCollection)
                        {
                            if (mapDetail.ZONEId == _ucZoneInMap.ZoneID && mapDetail.MapId == _ucZoneInMap.MapID)
                            {
                                int zoneWidth = mapDetail.zoneWidth;
                                int zoneHeight = mapDetail.zoneHeight;
                                int mapHeight = mapDetail.picMapHeight;
                                int mapWidth = mapDetail.picMapWidth;
                                double ratioWidth = (double)panelMap.Width / (double)mapWidth;
                                double ratioHeight = (double)panelMap.Height / (double)mapHeight;
                                _ucZoneInMap.Size = new Size((int)Math.Round(zoneWidth * ratioWidth), (int)Math.Round(zoneHeight * ratioHeight));
                                //Location
                                _ucZoneInMap.Location = new Point((int)Math.Round(mapDetail.PosX * ratioWidth), (int)Math.Round(mapDetail.PosY * ratioHeight));
                                break;
                            }
                        }

                    }

                }
            }
        }

        #endregion

        #region: Controls In Form
        private void dgvMap_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (dgvMap.CurrentRow.Cells["_ID"].Value.ToString() != this.mapID)
                {
                    bool isHaveChange = mapAddInfors.Count > 0 || mapModifyInfors.Count > 0 || mapDeleteInfors.Count > 0;
                    if (isHaveChange)
                    {
                        if (MessageBox.Show("Do you want to save change in this map?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            Save();
                        }
                    }
                    List<Control> deleteControls = new List<Control>();
                    foreach (Control control in panelMap.Controls)
                    {
                        if (control.GetType() == typeof(ucZoneInMap))
                        {
                            deleteControls.Add(control);
                        }
                    }
                    foreach (Control deltetControl in deleteControls)
                    {
                        panelMap.Controls.Remove(deltetControl);
                    }
                    foreach (TreeNode floorNode in treeView_zone.Nodes)
                    {
                        foreach (TreeNode zoneNode in floorNode.Nodes)
                        {
                            zoneNode.ImageIndex = IMAGE_CAR_ADD_INDEX;
                        }
                    }

                    this.mapID = dgvMap.CurrentRow.Cells["_ID"].Value.ToString();
                    StaticPool.selectedMAPID = this.mapID;
                    mapAddInfors.Clear();
                    mapModifyInfors.Clear();
                    mapDeleteInfors.Clear();
                    ucZoneInMaps.Clear();
                    //Show Selected Map Data
                    ShowSelectedMap();
                    //Load Zone Location
                    ShowZoneInMap(this.mapID);

                }
            }
        }
        private void PicMap_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void PicMap_DragDrop(object sender, DragEventArgs e)
        {
            if (picMap.Image == null)
            {
                MessageBox.Show("Select a map first!");
                return;
            }
            e.Effect = DragDropEffects.Move;
            TreeNode selectedZoneNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            foreach (TreeNode floor in treeView_zone.Nodes)
            {
                foreach (TreeNode zoneNode in floor.Nodes)
                {
                    if (zoneNode.Name.Equals(selectedZoneNode.Name))
                    {
                        zoneNode.ImageIndex = IMAGE_CAR_DELETE_INDEX;
                        break;
                    }
                }
            }
            string zoneID = selectedZoneNode.Name.Substring(selectedZoneNode.Name.IndexOf("Zone: ") + 6).Trim();
            ZONE selectedZone = StaticPool.zoneCollection.GetZONE(zoneID);
            AddZoneToMap(selectedZone, e.X, e.Y, panelMap.Width, panelMap.Height);
            foreach (MapUpdateInfor map in mapDeleteInfors)
            {
                if (map.ZoneID == zoneID)
                {
                    mapDeleteInfors.Remove(map);
                    break;
                }
            }
        }
        private void TreeView_zone_ItemDrag(object sender, ItemDragEventArgs e)
        {
            bool isInMap = false;
            if (e.Button == MouseButtons.Left)
            {
                TreeNode tree = new TreeNode();
                tree = (TreeNode)e.Item;
                if (tree.Name.Contains("Floor"))
                {
                    return;
                }
                foreach (ucZoneInMap _ucZoneInMap in ucZoneInMaps)
                {
                    string _ucZoneInMapID = _ucZoneInMap.Name.Split(":").Length > 2 ? _ucZoneInMap.Name.Split(":")[2].Trim() : "";
                    string zoneID = tree.Name.Substring(tree.Name.IndexOf("Zone: ") + 6).Trim();

                    if (_ucZoneInMapID == zoneID)
                    {
                        isInMap = true;
                        break;
                    }
                }
                if (!isInMap)
                {
                    DoDragDrop(tree.Clone(), DragDropEffects.Move);
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            UpdatePropertySetting();
            this.DialogResult = DialogResult.OK;
        }
        #endregion

        #region:Load Data
        private void LoadZoneTreeview()
        {
            foreach (Floor floor in StaticPool.floorCollection)
            {
                TreeNode floorNode = new TreeNode();
                floorNode.ImageIndex = 0;
                SetFloorNodeInfor(floor, floorNode);
                treeView_zone.Nodes.Add(floorNode);
                DataTable dtZone = StaticPool.mdb.FillData(@$"Select tblZone.ID as zoneID, tblZone.Name as zoneName
                                                         from tblFloor, tblZone, tblZoneGroup
                                                         Where tblZone.GroupID = tblZoneGroup.ID And tblZoneGroup.FloorID = tblFloor.ID And tblFloor.ID = '{floor.Id}'
                                                         order by tblZone.Sort");
                if (dtZone != null)
                {
                    if (dtZone.Rows.Count > 0)
                    {
                        foreach (DataRow row in dtZone.Rows)
                        {
                            TreeNode zoneNode = new TreeNode();
                            zoneNode.ImageIndex = IMAGE_CAR_ADD_INDEX;
                            SetZoneNodeInfor(row, zoneNode);
                            floorNode.Nodes.Add(zoneNode);
                        }
                    }
                }
            }
            treeView_zone.ExpandAll();
        }
        private void ShowMapDataInGridview()
        {
            dgvMap.Rows.Clear();
            foreach (Map map in StaticPool.mapCollection)
            {
                dgvMap.Rows.Add(map.ID, dgvMap.Rows.Count + 1, map.Name, map.ImagePath);
            }
            for (int i = 0; i < dgvMap.Rows.Count; i++)
            {
                DataGridViewRow row = dgvMap.Rows[i];
                if (row.Cells["_ID"].Value.ToString() == StaticPool.selectedMAPID)
                {
                    dgvMap.Rows[i].Selected = true;
                    break;
                }
            }
        }
        private void ShowSelectedMap()
        {
            Map selectedMap = StaticPool.mapCollection.GetMap(this.mapID);
            if (selectedMap != null)
            {
                //Load image
                if (File.Exists(selectedMap.ImagePath))
                {
                    picMap.Image = Image.FromFile(selectedMap.ImagePath);
                }
            }
        }
        private void ShowZoneInMap(string mapID)
        {
            foreach (MapDetail mapDetail in StaticPool.mapDetailCollection)
            {
                if (mapDetail.MapId == mapID)
                {
                    ZONE zone = StaticPool.zoneCollection.GetZONE(mapDetail.ZONEId);
                    if (zone != null)
                    {
                        CreateNewZoneInMap(zone, (int)mapDetail.PosX, (int)mapDetail.PosY, mapDetail.zoneWidth, mapDetail.zoneHeight, mapDetail.picMapWidth, mapDetail.picMapHeight);
                        LoadNodeInUse(mapDetail);
                    }
                }
            }
        }
        private void LoadNodeInUse(MapDetail mapDetail)
        {
            foreach (TreeNode floorNode in treeView_zone.Nodes)
            {
                bool isFound = false;
                foreach (TreeNode zoneNode in floorNode.Nodes)
                {
                    if (zoneNode.Name == "Zone: " + mapDetail.ZONEId)
                    {
                        zoneNode.ImageIndex = IMAGE_CAR_DELETE_INDEX;
                        isFound = true;
                        break;
                    }
                }
                if (!isFound)
                {
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        #endregion

        #region: Save to SQL
        private void Save()
        {
            SaveUpdateData();
            SaveAddData();
            SaveDeleteData();
        }
        public List<MapUpdateInfor> GetUpdateInfor()
        {
            return this.mapModifyInfors;
        }
        public List<MapUpdateInfor> GetAddInfor()
        {
            return this.mapAddInfors;
        }
        public List<MapUpdateInfor> GetDeleteInfo()
        {
            return this.mapDeleteInfors;
        }
        private void SaveAddData()
        {
            foreach (MapUpdateInfor mapAddInfor in mapAddInfors)
            {
                string mapID = mapAddInfor.MapID;
                string zoneID = mapAddInfor.ZoneID;
                int ZonePosX = mapAddInfor.PosX >= 0 ? mapAddInfor.PosX : 0;
                int ZonePosY = mapAddInfor.PosY >= 0 ? mapAddInfor.PosY : 0;
                int ZoneWith = mapAddInfor.Width;
                int ZoneHeight = mapAddInfor.Height;
                int mapWidth = mapAddInfor.mapWidth;
                int mapHeight = mapAddInfor.MapHeight;
                string insertID = tblMapDetail.InsertAndGetLastID(mapID, zoneID, ZonePosX, ZonePosY, ZoneWith, ZoneHeight, mapWidth, mapHeight);
                if (insertID != null)
                {
                    MapDetail mapDetail = new MapDetail();
                    mapDetail.Id = insertID;
                    mapDetail.MapId = mapID;
                    mapDetail.ZONEId = zoneID;
                    mapDetail.PosX = ZonePosX;
                    mapDetail.PosY = ZonePosY;
                    mapDetail.zoneWidth = ZoneWith;
                    mapDetail.zoneHeight = ZoneHeight;
                    mapDetail.picMapWidth = mapWidth;
                    mapDetail.picMapHeight = mapHeight;
                    StaticPool.mapDetailCollection.Add(mapDetail);
                }
            }
        }
        private void SaveUpdateData()
        {
            foreach (MapUpdateInfor mapUpdateInfor in mapModifyInfors)
            {
                string mapID = mapUpdateInfor.MapID;
                string zoneID = mapUpdateInfor.ZoneID;
                int ZonePosX = mapUpdateInfor.PosX >= 0 ? mapUpdateInfor.PosX : 0;
                int ZonePosY = mapUpdateInfor.PosY >= 0 ? mapUpdateInfor.PosY : 0;
                int ZoneWith = mapUpdateInfor.Width;
                int ZoneHeight = mapUpdateInfor.Height;
                int mapWidth = mapUpdateInfor.mapWidth;
                int mapHeight = mapUpdateInfor.MapHeight;
                bool result = tblMapDetail.Modify(mapID, zoneID, ZonePosX, ZonePosY, ZoneWith, ZoneHeight, mapWidth, mapHeight);
                if (result)
                {
                    MapDetail mapDetail = StaticPool.mapDetailCollection.GetMapDetail(mapID, zoneID);
                    if (mapDetail != null)
                    {
                        mapDetail.zoneWidth = ZoneWith;
                        mapDetail.zoneHeight = ZoneHeight;
                        mapDetail.PosX = ZonePosX;
                        mapDetail.PosY = ZonePosY;
                        mapDetail.picMapWidth = mapWidth;
                        mapDetail.picMapHeight = mapHeight;
                    }
                }
            }
        }
        private void SaveDeleteData()
        {
            foreach (MapUpdateInfor mapDeleteInfor in mapDeleteInfors)
            {
                string mapID = mapDeleteInfor.MapID;
                string zoneID = mapDeleteInfor.ZoneID;
                tblMapDetail.Delete(zoneID, mapID);
                List<MapDetail> deleteMaps = new List<MapDetail>();
                MapDetail mapDetail = StaticPool.mapDetailCollection.GetMapDetail(mapID, zoneID);
                if (mapDetail != null)
                {
                    deleteMaps.Add(mapDetail);
                }
                foreach (MapDetail _deleteMap in deleteMaps)
                {
                    StaticPool.mapDetailCollection.Remove(_deleteMap);
                }
            }
        }
        #endregion

        #region: ucZoneInMap Event
        #region:ADD
        private void AddZoneToMap(ZONE zone, int posX, int posY, int mapWidth, int mapHeight)
        {
            int width = StaticPool.zoneWidth;
            int height = StaticPool.zoneHeight;
            StandardizedLocation(ref posX, ref posY);
            MapUpdateInfor addInfor = new MapUpdateInfor(this.mapID, zone.Id, posX, posY, width, height, mapHeight, mapWidth);
            mapAddInfors.Add(addInfor);
            foreach (MapUpdateInfor map in this.mapDeleteInfors)
            {
                if (map.ZoneID == zone.Id && map.MapID == this.mapID)
                {
                    bool isInMapUpdate = false;
                    foreach (MapUpdateInfor mapModify in this.mapModifyInfors)
                    {
                        if (mapModify.ZoneID == zone.Id && mapModify.MapID == this.mapID)
                        {
                            isInMapUpdate = true;
                            mapModify.PosX = posX;
                            mapModify.PosY = posY;
                            mapModify.Width = width;
                            mapModify.Height = height;
                            mapModify.mapWidth = mapWidth;
                            mapModify.MapHeight = mapHeight;
                            break;
                        }
                    }
                    if (!isInMapUpdate)
                    {
                        this.mapModifyInfors.Add(addInfor);
                    }
                    mapDeleteInfors.Remove(map);
                    mapAddInfors.Remove(addInfor);
                    break;
                }
            }
            CreateNewZoneInMap(zone, posX, posY, width, height, mapWidth, mapHeight);
        }
        private void CreateNewZoneInMap(ZONE zone, int posX, int posY, int width, int height, int mapWidth, int mapHeight)
        {
            ucZoneInMap _ucZoneInMap = new ucZoneInMap(zone.zoneName);
            _ucZoneInMap.AsignDeleteZoneInMap();
            //Name
            SetUcZoneInMapName(zone, _ucZoneInMap);
            _ucZoneInMap.ZoneID = zone.Id;
            _ucZoneInMap.MapID = this.mapID;

            //Size
            double ratioWidth = (double)panelMap.Width / (double)mapWidth;
            double ratioHeight = (double)panelMap.Height / (double)mapHeight;
            _ucZoneInMap.Size = new Size((int)Math.Round(width * ratioWidth)>0? (int)Math.Round(width * ratioWidth):1, (int)Math.Round(height * ratioHeight)>0? (int)Math.Round(height * ratioHeight):1);
            //Location
            _ucZoneInMap.Location = new Point((int)Math.Round(posX * ratioWidth), (int)Math.Round(posY* ratioHeight));

            //_ucZoneInMap.Size = new Size(width * panelMap.Width / mapWidth, height * panelMap.Height / mapHeight);
            ////Location
            //_ucZoneInMap.Location = new Point(posX * panelMap.Width / mapWidth, posY * panelMap.Height / mapHeight);
            ////Information     
            //this.Controls.Add(_ucZoneInMap);
            panelMap.Controls.Add(_ucZoneInMap);
            _ucZoneInMap.BringToFront();
            ucZoneInMaps.Add(_ucZoneInMap);
            //Event
            _ucZoneInMap.SizeChanged += _ucZoneInMap_updateInfor;
            _ucZoneInMap.LocationChanged += _ucZoneInMap_updateInfor;
            _ucZoneInMap.DeleteZoneEvent += _ucZoneInMap_DeleteZoneEvent;
            _ucZoneInMap.AsignAllowDrag();
        }
        #endregion
        #region:Event
        //Delete
        private void _ucZoneInMap_DeleteZoneEvent(object sender, Events.DeleteZoneInMapEventArgs e)
        {
            ucZoneInMap deletedUC = (ucZoneInMap)sender;
            DeleteZoneInMap(e.ZoneID, e.MapID, deletedUC);
        }
        private void DeleteZoneInMap(string zoneID, string MapID, ucZoneInMap deletedUC)
        {
            panelMap.Controls.Remove(deletedUC);
            ucZoneInMaps.Remove(deletedUC);

            MapUpdateInfor deleteMap = new MapUpdateInfor(MapID, zoneID);
            this.mapDeleteInfors.Add(deleteMap);
            foreach (MapUpdateInfor map in this.mapModifyInfors)
            {
                if (map.ZoneID == zoneID && map.MapID == MapID)
                {
                    mapModifyInfors.Remove(map);
                    break;
                }
            }
            foreach (MapUpdateInfor map in this.mapAddInfors)
            {
                if (map.ZoneID == zoneID && map.MapID == MapID)
                {
                    mapAddInfors.Remove(map);
                    break;
                }
            }
            foreach (TreeNode floorNode in treeView_zone.Nodes)
            {
                bool isFound = false;
                foreach (TreeNode zoneNode in floorNode.Nodes)
                {
                    string zoneNodeID = zoneNode.Name.Substring(zoneNode.Name.IndexOf("Zone: ") + 6).Trim();
                    if (zoneNodeID == zoneID)
                    {
                        zoneNode.ImageIndex = IMAGE_CAR_ADD_INDEX;
                        isFound = true;
                        break;
                    }
                }
                if (isFound) break;
                else continue;
            }
        }
        //Update Location
        private void _ucZoneInMap_updateInfor(object sender, EventArgs e)
        {
            ucZoneInMap _ucZOneInMap = (ucZoneInMap)sender;

            StaticPool.zoneWidth = _ucZOneInMap.Width;
            StaticPool.zoneHeight = _ucZOneInMap.Height;

            bool isAddedToUpdateList = false;
            //Kiểm tra ZONE mới được đăng ký trong lượt setting này hay ZONE đã được thêm vào từ trước
            bool isUpdateLocationnEvent = true;

            int current_posX = _ucZOneInMap.Location.X;
            int current_posY = _ucZOneInMap.Location.Y;
            int current_width = _ucZOneInMap.Width;
            int current_height = _ucZOneInMap.Height;
            int current_mapHeight = panelMap.Height;
            int current_mapWidth = panelMap.Width;
            #region:Mới dược thêm vào trong lượt setting này ==> UPDATE thông tin trong AddLIST
            string zoneID = _ucZOneInMap.Name.Split(":").Length > 2 ? _ucZOneInMap.Name.Split(":")[2].Trim() : "";
            if (zoneID != "")
            {
                foreach (MapUpdateInfor mapAddInfor in mapAddInfors)
                {
                    if (mapAddInfor.MapID == this.mapID && mapAddInfor.ZoneID == zoneID)
                    {
                        isUpdateLocationnEvent = false;
                        mapAddInfor.PosX = current_posX;
                        mapAddInfor.PosY = current_posY;
                        mapAddInfor.Width = current_width;
                        mapAddInfor.Height = current_height;
                        mapAddInfor.mapWidth = current_mapWidth;
                        mapAddInfor.MapHeight = current_mapHeight;
                        return;
                    }
                }
            }
            #endregion

            #region:Được thêm vào từ trước
            if (isUpdateLocationnEvent)
            {
                if (zoneID != "")
                {
                    //Kiểm tra: Đã được thêm vào UpdateList Hay chưa
                    #region:Đã được thêm vào UpdateList
                    foreach (MapUpdateInfor mapUpdateInfor in mapModifyInfors)
                    {
                        if (mapUpdateInfor.MapID == this.mapID && mapUpdateInfor.ZoneID == zoneID)
                        {
                            isAddedToUpdateList = true;
                            mapUpdateInfor.PosX = current_posX;
                            mapUpdateInfor.PosY = current_posY;
                            mapUpdateInfor.Width = current_width;
                            mapUpdateInfor.Height = current_height;
                            mapUpdateInfor.mapWidth = current_mapWidth;
                            mapUpdateInfor.MapHeight = current_mapHeight;
                            return;
                        }
                    }
                    #endregion

                    #region:Chưa được thêm vào UpdateList
                    if (!isAddedToUpdateList)
                    {
                        MapUpdateInfor updateInfor = new MapUpdateInfor(this.mapID, zoneID, current_posX, current_posY, current_width, current_height, current_mapHeight, current_mapWidth);
                        mapModifyInfors.Add(updateInfor);
                    }
                    #endregion
                }
            }
            #endregion
        }
        #endregion
        #endregion

        #region: Internal
        private int GetTittleHeight()
        {
            Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            int titleHeight = screenRectangle.Top - this.Top;
            return titleHeight;
        }
        private void UpdatePropertySetting()
        {
            StaticPool.selectedMAPID = dgvMap.SelectedRows[0].Cells["_ID"].Value.ToString();
            Properties.Settings.Default.selectedMAP = StaticPool.selectedMAPID;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.zoneWidth = StaticPool.zoneWidth;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.zoneHeight = StaticPool.zoneHeight;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.picMapDetailWidth = picMap.Width;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.picMapDetailHeght = picMap.Height;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.picMapDetailPosX = picMap.Location.X;
            Properties.Settings.Default.Save();

            Properties.Settings.Default.picMapDetailPosY = picMap.Location.Y;
            Properties.Settings.Default.Save();

            StaticPool.mdb.ExecuteCommand($"Update tblMapSettingInfo set Width = {picMap.Width}, Height = {picMap.Height}, PosX = {picMap.Location.X},PosY = {picMap.Location.Y}");
        }
        private void StandardizedLocation(ref int posX, ref int posY)
        {
            posX = posX - this.Location.X - 6 - panelMap.Location.X;
            posY = posY - this.Location.Y - GetTittleHeight() - panelMap.Location.Y;
        }
        private static void SetUcZoneInMapName(ZONE zone, ucZoneInMap _ucZoneInMap)
        {
            _ucZoneInMap.Name = "uc:Zone :" + zone.Id;
        }
        private static void SetZoneNodeInfor(DataRow zoneRowData, TreeNode zoneNode)
        {
            zoneNode.Name = "Zone: " + zoneRowData["zoneID"].ToString();
            zoneNode.Text = zoneRowData["zoneName"].ToString();
        }
        private static void SetFloorNodeInfor(Floor floor, TreeNode floorNode)
        {
            floorNode.Name = "Floor: " + floor.Id;
            floorNode.Text = floor.Name;
        }
        #endregion

    }
}
