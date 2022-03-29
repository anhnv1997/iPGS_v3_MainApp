using iParking.Databases;
using iParking.Forms;
using iParking.Object;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using iParking.Enums;
using System.Collections.Generic;
using iParking.Device;
using iParking.Events;
using System.Threading;
using iParking.UserControls;
using System.Linq;
using Kztek.LedController;
using System.Threading.Tasks;
using System.ComponentModel;

namespace iParking
{
    public partial class Form1 : Form
    {
        #region:Properties
        static Form1 frm;
        private string mapID = Properties.Settings.Default.selectedMAP;
        int startWidth = 0;
        int startHeight = 0;
        List<Control> DeleteControls = new List<Control>();
        static ChangeLedDisplayEventArgs[] ledDisplayDatas = new ChangeLedDisplayEventArgs[3];
        #endregion
        private SynchronizationContext _syncRoot;
        public static List<IZCU> iZCUs = new List<IZCU>();
        public List<Led> OrderVehicleLed = new List<Led>();

        #region:Multi Thread
        private class UISync
        {
            private static ISynchronizeInvoke Sync;
            public static void Init(ISynchronizeInvoke sync)
            {
                Sync = sync;
            }
            public static void Execute(Action action)
            {
                Sync.BeginInvoke(action, null);
            }
        }
        #endregion

        #region:Form
        public Form1()
        {
            LogHelper.Logger_Info("Start IParking PGS");
            InitializeComponent();
            UISync.Init(this);
            startWidth = picMAP.Width;
            startHeight = picMAP.Height;
            frm = this;
            _syncRoot = SynchronizationContext.Current;
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            ledDisplayDatas[0] = new ChangeLedDisplayEventArgs() { ZoneName = "", PlateNumber = "" };
            ledDisplayDatas[1] = new ChangeLedDisplayEventArgs() { ZoneName = "", PlateNumber = "" };
            ledDisplayDatas[2] = new ChangeLedDisplayEventArgs() { ZoneName = "", PlateNumber = "" };

            panelMainContent.Enabled = false;
            mainMenuStrip.Enabled = false;
            if (frmLoading.isLoadingSuccess)
            {
                this.Cursor = Cursors.WaitCursor;
                LogHelper.Logger_Info("Start Load Main Form IParking PGS");
                await Task.Run(() =>
                {
                    LoadData();
                    LogHelper.Logger_Info("Load Data IParking PGS Success");
                });
                AssignEvent();

                panelMainContent.Enabled = true;
                mainMenuStrip.Enabled = true;
                this.Cursor = Cursors.Default;
            }

            Task.Run(new Action(() => ControlIO()));
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            bool cursorNotInBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
                this.Hide();
            }
            else
            {
                this.ShowInTaskbar = true;
            }
            //Responsive
            foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
            {
                ucZoneInMap _ucZoneInMap = (ucZoneInMap)control;
                MapDetail mapDetail = StaticPool.mapDetailCollection.GetMapDetail(_ucZoneInMap.MapID, _ucZoneInMap.ZoneID);
                if (mapDetail != null)
                {
                    int posX = (int)mapDetail.PosX;
                    int posY = (int)mapDetail.PosY;
                    int zoneWidth = mapDetail.zoneWidth;
                    int zoneHeight = mapDetail.zoneHeight;

                    StandardizedLocation(ref posX, ref posY, ref zoneWidth, ref zoneHeight, mapDetail.picMapWidth, mapDetail.picMapHeight);
                    _ucZoneInMap.Location = new Point(posX, posY);
                    zoneWidth = zoneWidth <= 0 ? 1 : zoneWidth;
                    zoneHeight = zoneHeight <= 0 ? 1 : zoneHeight;
                    _ucZoneInMap.Size = new Size(zoneWidth, zoneHeight);
                }
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        #endregion: END FORM

        #region:Load
        //Slot table
        private void LoadSlotTable()
        {
            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                ZoneGroup zoneGroup = StaticPool.zoneGroupCollection.GetzgById(zone.ZoneGroupId);
                string floorName = "";
                if (zoneGroup != null)
                {
                    Floor floor = StaticPool.floorCollection.GetFloor(zoneGroup.FloorID);
                    if (floor != null)
                    {
                        floorName = floor.Name;
                    }
                }
                ucSlotTable slot = new ucSlotTable(zone.Id, zone.zoneName, floorName, zone.Status);
                slot.Dock = DockStyle.Top;
                panelSlotTableContent?.Invoke(new Action(() =>
                {
                    panelSlotTableContent.Controls.Add(slot);
                }));
            }
        }
        #endregion

        #region:Control In Form
        //MenuStrip
        private void tsmSetting_Click(object sender, EventArgs e)
        {
            foreach (IZCU zCU in iZCUs)
            {
                zCU.zoneEvent -= IZCU_zoneEvent;
            }
            frmSetting frm = new frmSetting();
            frm.ShowDialog();
            if (StaticPool.isChangeSetting)
            {
                if (MessageBox.Show("Restart App To Apply Change!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
            }
        }
        private void tsmMapDetail_Click(object sender, EventArgs e)
        {
            frmMapDetail frm = new frmMapDetail(StaticPool.selectedMAPID);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                if (this.mapID != StaticPool.selectedMAPID)
                {
                    this.mapID = StaticPool.selectedMAPID;
                    ClearOldMapData();
                    ShowMap(this.mapID);
                    return;
                }

                List<MapUpdateInfor> mapModifyInfors = frm.GetUpdateInfor();
                List<MapUpdateInfor> mapAddInfors = frm.GetAddInfor();
                List<MapUpdateInfor> mapDeleteInfors = frm.GetDeleteInfo();

                foreach (MapUpdateInfor mapAddInfor in mapAddInfors)
                {
                    ZONE zone = StaticPool.zoneCollection.GetZONE(mapAddInfor.ZoneID);
                    if (zone != null)
                    {
                        int posX = (int)mapAddInfor.PosX;
                        int posY = (int)mapAddInfor.PosY;
                        int zoneWidth = mapAddInfor.Width;
                        int zoneHeight = mapAddInfor.Height;
                        StandardizedLocation(ref posX, ref posY, ref zoneWidth, ref zoneHeight, mapAddInfor.mapWidth, mapAddInfor.MapHeight);
                        CreateNewZoneInMap(zone, posX, posY, zoneWidth, zoneHeight);
                    }
                }
                foreach (MapUpdateInfor mapUpdateInfo in mapModifyInfors)
                {
                    int posX = (int)mapUpdateInfo.PosX;
                    int posY = (int)mapUpdateInfo.PosY;
                    int zoneWidth = mapUpdateInfo.Width;
                    int zoneHeight = mapUpdateInfo.Height;
                    StandardizedLocation(ref posX, ref posY, ref zoneWidth, ref zoneHeight, mapUpdateInfo.mapWidth, mapUpdateInfo.MapHeight);
                    foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
                    {
                        if (control.Name == ("uc:Zone :" + mapUpdateInfo.ZoneID))
                        {
                            control.Location = new Point(posX, posY);
                            control.Size = new Size(zoneWidth, zoneHeight);
                        }
                    }
                }
                foreach (MapUpdateInfor mapDeleteInfo in mapDeleteInfors)
                {
                    foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
                    {
                        if (control.Name == ("uc:Zone :" + mapDeleteInfo.ZoneID))
                        {
                            DeleteControls.Add(control);
                        }
                    }
                }
                foreach (Control deleteControl in DeleteControls)
                {
                    panelMap.Controls.Remove(deleteControl);
                }
                DeleteControls.Clear();
                mapDeleteInfors.Clear();
                mapAddInfors.Clear();
                mapModifyInfors.Clear();
            }
        }
        private void tsmZoneDetail_Click(object sender, EventArgs e)
        {
            frmVehicleZoneDetail frm = new frmVehicleZoneDetail();
            frm.ShowDialog();

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
                        ShowMap(this.mapID);
                    }
                }
            }
        }
        private void tsmLedDetail_Click(object sender, EventArgs e)
        {
            frmLedDetail frm = new frmLedDetail();
            frm.ShowDialog();
            StaticPool.InitLedDisplay();
        }
        private void tsmOutputDetail_Click(object sender, EventArgs e)
        {
            frmOutputDetail frm = new frmOutputDetail();
            frm.ShowDialog();
            tblOutput.LoadDataOutput(StaticPool.outputCollection);
            StaticPool.InitOutputColor();
        }
        private void tsmFindVehicle_Click(object sender, EventArgs e)
        {
            frmFindVehicle frm = new frmFindVehicle();
            frm.ShowDialog();
        }
        private void tsmReport_Click(object sender, EventArgs e)
        {
            frmReport frm = new frmReport();
            frm.ShowDialog();
        }
        //Change Map
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
        #endregion

        #region:ZoneInMap
        //Create
        private void CreateNewZoneInMap(ZONE zone, int posX, int posY, int width, int height)
        {
            ucZoneInMap _ucZoneInMap = new ucZoneInMap(zone.zoneName);
            _ucZoneInMap.AsignSetZoneByHand();
            _ucZoneInMap.SetZoneStatusByHand += _ucZoneInMap_SetZoneStatusByHand;
            _ucZoneInMap.ReleaseOrderEvent += _ucZoneInMap_ReleaseOrderEvent;
            //Name
            SetUcZoneInMapInfor(zone, _ucZoneInMap);

            //Size
            _ucZoneInMap.Size = new Size(width, height);
            //Locatiom
            _ucZoneInMap.Location = new Point(posX, posY);

            //Information
            UISync.Execute(() =>
            {
                panelMap.Controls.Add(_ucZoneInMap);
                _ucZoneInMap.BringToFront();
                lbMAPName.BringToFront();
            });
            _ucZoneInMap.AssignHover();
            _ucZoneInMap.ShowMapDetailInforEvent += _ucZoneInMap_ShowMapDetailInforEvent;
        }
        //Event
        private void _ucZoneInMap_ReleaseOrderEvent(object sender, ReleaseOrderEventArgs e)
        {
            //foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
            //{
            //    ucZoneInMap _ucZoneInMap = (ucZoneInMap)control;
            //    if (_ucZoneInMap.ZoneID == e.ZoneID)
            //    {
            //        _ucZoneInMap.Invoke(new Action(() =>
            //        {
            //            _ucZoneInMap.SetStatus(Color.DarkGreen);
            //        }));
            //        break;
            //    }
            //}

            //foreach (ZONE _selectZone in StaticPool.zoneCollection)
            //{
            //    if (_selectZone.Id == e.ZoneID)
            //    {
            //        _selectZone.Status = (int)EM_ZoneStatusType.UN_OCCUPIED;
            //        _selectZone.OldStatus = (int)EM_ZoneStatusType.ORDER;
            //        tblZoneEvent.Insert(_selectZone, DateTime.Now);

            //        break;
            //    }
            //}
            //foreach (Control control in panelSlotTableContent.Controls.OfType<ucSlotTable>())
            //{
            //    if (control.Name.Contains(e.ZoneID))
            //    {
            //        ucSlotTable uc = (ucSlotTable)control;
            //        uc?.Invoke(new Action(() =>
            //        {
            //            uc.UpdateStatus((int)EM_ZoneStatusType.UN_OCCUPIED);
            //        }));
            //    }
            //}
        }

        private void _ucZoneInMap_SetZoneStatusByHand(object sender, SetZoneStatusByHandEventArgs e)
        {
            foreach (IZCU zCU in iZCUs)
            {
                zCU.zoneEvent -= IZCU_zoneEvent;
            }
            foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
            {
                ucZoneInMap _ucZoneInMap = (ucZoneInMap)control;
                if (_ucZoneInMap.ZoneID == e.ZoneID)
                {
                    switch (e.Status)
                    {
                        case EM_ZoneStatusType.UN_OCCUPIED:
                            _ucZoneInMap.Invoke(new Action(() =>
                            {
                                _ucZoneInMap.SetStatus(Color.DarkGreen);
                            }));
                            break;
                        case EM_ZoneStatusType.OCCUPIED:
                            _ucZoneInMap.Invoke(new Action(() =>
                            {
                                _ucZoneInMap.SetStatus(Color.DarkRed);
                            }));
                            break;
                        case EM_ZoneStatusType.ORDER:
                            _ucZoneInMap.Invoke(new Action(() =>
                            {
                                _ucZoneInMap.SetStatus(Color.Yellow);
                            }));
                            break;
                    }
                }
            }

            foreach (ZONE _selectZone in StaticPool.zoneCollection)
            {
                if (_selectZone.Id == e.ZoneID)
                {
                    if (_selectZone.Status != (int)e.Status)
                    {
                        _selectZone.OldStatus = _selectZone.Status;
                        _selectZone.Status = (int)e.Status;
                        if (_selectZone.OldStatus == (int)EM_ZoneStatusType.OCCUPIED && _selectZone.Status == (int)EM_ZoneStatusType.ORDER)
                        {
                            return;
                        }
                        foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
                        {
                            if (outputDetail.ZoneID == e.ZoneID)
                            {
                                Output output = StaticPool.outputCollection.GetOutput(outputDetail.OutputID);
                                if (output != null)
                                {
                                    StaticPool.UpdateOutputSlotInfo(output, _selectZone.OldStatus, _selectZone.Status, outputDetail.RelayIndex);
                                }
                            }
                        }
                    }
                    _selectZone.isUseAI = e.isUseAI;
                    tblZoneEvent.Insert(_selectZone, DateTime.Now);

                    break;
                }
            }
            foreach (Control control in panelSlotTableContent.Controls.OfType<ucSlotTable>())
            {
                if (control.Name.Contains(e.ZoneID))
                {
                    ucSlotTable uc = (ucSlotTable)control;
                    uc?.Invoke(new Action(() =>
                    {
                        uc.UpdateStatus((int)e.Status);
                    }));
                }
            }
            int isUseAi = e.isUseAI == true ? 1 : 0;
            string updateCMD = $"Update tblZone set isUseAI = {isUseAi} , Status = {(int)e.Status} where ID ='{e.ZoneID}'";
            StaticPool.mdb.ExecuteCommand(updateCMD);
            foreach (ZONE zone in StaticPool.zoneCollection)
            {

            }
            foreach (IZCU zCU in iZCUs)
            {
                zCU.zoneEvent += IZCU_zoneEvent;
            }
        }

        private void _ucZoneInMap_ShowMapDetailInforEvent(object sender, Events.ShowMapDetailInforEventArgs e)
        {
            //frmShowZoneDetail frm = new frmShowZoneDetail(StaticPool.zoneCollection.GetZONE(e.zoneID), e.imagePath, e.plateNum);
            //frm.ShowDialog();
        }
        #endregion

        #region:Event
        #region:Assign
        private void AssignEvent()
        {
            tblZoneEvent.changeLedDisplayEvent += TblZoneEvent_changeLedDisplayEvent;
            AssignTMAEvent();
            AssignZCUEvent();
        }
        private void AssignZCUEvent()
        {
            foreach (ZCU zcu in StaticPool.zcuCollection)
            {
                if (zcu.Type != (int)EM_ZcuTypes.AI_TMA)
                {
                    IZCU iZCU = ZcuFactory.GetZCU(zcu);
                    if (iZCU != null)
                    {
                        LogHelper.Logger_Info($"Assign ZCU_IP:{zcu.IPAddress};Username:{zcu.Username};Password:{zcu.Password} Event");
                        iZCU.Connect();
                        iZCU.zoneEvent += IZCU_zoneEvent;
                        //iZCU.statusChangeEvent += IZCU_statusChangeEvent;
                        iZCU.PollingStart();
                        iZCUs.Add(iZCU);
                    }
                }
            }
        }
        private async void AssignTMAEvent()
        {
            try
            {
                foreach (TMA_Server _TMA_Server in StaticPool.TMACollection)
                {
                    ZCU zcu = new ZCU();
                    zcu.IPAddress = _TMA_Server.Ip;
                    zcu.Port = _TMA_Server.Port;
                    zcu.Username = _TMA_Server.Username;
                    zcu.Password = _TMA_Server.Password;
                    zcu.Type = (int)EM_ZcuTypes.AI_TMA;
                    zcu.Id = _TMA_Server.Id;
                    IZCU iZCU = ZcuFactory.GetZCU(zcu);
                    if (iZCU != null)
                    {
                        LogHelper.Logger_Info($"Assign TMA_:IP:{zcu.IPAddress};Username:{zcu.Username};Password:{zcu.Password} Event");
                        iZCU.Connect();
                        iZCU.zoneEvent += IZCU_zoneEvent;
                        //iZCU.statusChangeEvent += IZCU_statusChangeEvent;
                        await Task.Run(() =>
                        {
                            iZCU.PollingStart();
                            iZCUs.Add(iZCU);
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Assign TMA ERROR: " + ex.Message);
            }

        }
        #endregion

        #region: Excecute Event
        private void TblZoneEvent_changeLedDisplayEvent(object sender, ChangeLedDisplayEventArgs e)
        {
            try
            {
                bool isNotSame = true;
                foreach (ChangeLedDisplayEventArgs data in ledDisplayDatas)
                {
                    if (data != null)
                    {
                        if (data.PlateNumber == e.PlateNumber)
                        {
                            data.ZoneName = e.ZoneName;
                            isNotSame = false;
                        }
                    }
                }
                if (isNotSame)
                {
                    //Change Display Led
                    ledDisplayDatas[2] = ledDisplayDatas[1] == null ? new ChangeLedDisplayEventArgs() { PlateNumber = "", ZoneName = "" } : ledDisplayDatas[1];
                    ledDisplayDatas[1] = ledDisplayDatas[0] == null ? new ChangeLedDisplayEventArgs() { PlateNumber = "", ZoneName = "" } : ledDisplayDatas[0];
                    ledDisplayDatas[0] = e;
                }
                Led led1, led2, led3;
                ILED iLed1, iLed2, iLed3;
                switch (StaticPool.ledCollection.Count)
                {
                    case 0:
                        break;
                    case 1:
                        led1 = StaticPool.ledCollection[0];
                        iLed1 = LedFactory.GetLedController((EM_ModuleType)led1.LedType, led1.IPAddress, led1.Port);
                        if (iLed1 != null)
                        {
                            iLed1.DisplayOrderVehicle(e.ZoneName, e.PlateNumber, led1.Color, led1.LedArrow);
                        }
                        break;
                    case 2:
                        led1 = StaticPool.ledCollection[1];
                        led2 = StaticPool.ledCollection[0];
                        iLed1 = LedFactory.GetLedController((EM_ModuleType)led1.LedType, led1.IPAddress, led1.Port);
                        if (iLed1 != null)
                        {
                            if (ledDisplayDatas[0] != null)
                                iLed1.DisplayOrderVehicle(ledDisplayDatas[0].ZoneName, ledDisplayDatas[0].PlateNumber, led1.Color, led1.LedArrow);
                        }
                        iLed2 = LedFactory.GetLedController((EM_ModuleType)led2.LedType, led2.IPAddress, led2.Port);
                        if (iLed2 != null)
                        {
                            if (ledDisplayDatas[1] != null)
                                iLed2.DisplayOrderVehicle(ledDisplayDatas[1].ZoneName, ledDisplayDatas[1].PlateNumber, led2.Color, led2.LedArrow);
                        }
                        break;
                    case 3:
                        try
                        {
                            led1 = StaticPool.ledCollection[2];
                            led2 = StaticPool.ledCollection[1];
                            led3 = StaticPool.ledCollection[0];
                            iLed1 = LedFactory.GetLedController((EM_ModuleType)led1.LedType, led1.IPAddress, led1.Port);
                            if (iLed1 != null)
                            {
                                LogHelper.Logger_LEDInfo($"Send LED {iLed1.ComPort} Data Zone: {ledDisplayDatas[0].ZoneName}, Plate: {ledDisplayDatas[0].PlateNumber}");
                                iLed1.DisplayOrderVehicle(ledDisplayDatas[0].ZoneName, ledDisplayDatas[0].PlateNumber, led1.Color, led1.LedArrow);
                            }
                            else
                            {
                                LogHelper.Logger_LedError($"Send LED {iLed1.ComPort} null ");
                            }
                            iLed2 = LedFactory.GetLedController((EM_ModuleType)led2.LedType, led2.IPAddress, led2.Port);
                            if (iLed2 != null)
                            {
                                LogHelper.Logger_LEDInfo($"Send LED {iLed2.ComPort} Data Zone: {ledDisplayDatas[1].ZoneName}, Plate: {ledDisplayDatas[1].PlateNumber}");
                                iLed2.DisplayOrderVehicle(ledDisplayDatas[1].ZoneName, ledDisplayDatas[1].PlateNumber, led2.Color, led2.LedArrow);
                            }
                            else
                            {
                                LogHelper.Logger_LedError($"Send LED {iLed2.ComPort} null ");
                            }
                            iLed3 = LedFactory.GetLedController((EM_ModuleType)led3.LedType, led3.IPAddress, led3.Port);
                            if (iLed3 != null)
                            {
                                LogHelper.Logger_LEDInfo($"Send LED {iLed3.ComPort} Data Zone: {ledDisplayDatas[2].ZoneName}, Plate: {ledDisplayDatas[2].PlateNumber}");
                                iLed3.DisplayOrderVehicle(ledDisplayDatas[2].ZoneName, ledDisplayDatas[2].PlateNumber, led3.Color, led3.LedArrow);
                            }
                            else
                            {
                                LogHelper.Logger_LedError($"Send LED {iLed3.ComPort} null ");
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Logger_LedError("Send LED CMD error" + ex.Message);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void IZCU_zoneEvent(object sender, Events.ZoneEventArgs e)
        {
            UpdateFormDisplay(e);
            //UpdateLedDisplay(e);
            //UpdateKZIODisplay(e);
            UpdateSlotTable(e);
            SaveToDB(e);

        }
        #endregion
        private void UpdateFormDisplay(ZoneEventArgs e)
        {
            foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
            {
                ucZoneInMap _ucZoneInMap = (ucZoneInMap)control;
                if (_ucZoneInMap.ZoneID == e.ZoneID)
                {
                    switch (e.ZoneStatus)
                    {
                        case EM_ZoneStatusType.UN_OCCUPIED:
                            _ucZoneInMap.Invoke(new Action(() =>
                            {
                                _ucZoneInMap.SetStatus(Color.DarkGreen);
                            }));
                            break;
                        case EM_ZoneStatusType.OCCUPIED:
                            _ucZoneInMap.Invoke(new Action(() =>
                            {
                                _ucZoneInMap.SetStatus(Color.DarkRed);
                            }));
                            break;
                        case EM_ZoneStatusType.ORDER:
                            _ucZoneInMap.Invoke(new Action(() =>
                            {
                                _ucZoneInMap.SetStatus(Color.Yellow);
                            }));
                            break;
                    }
                }
            }
        }
        public static void UpdateLedDisplay(ZoneEventArgs e)
        {
            if (e.ZoneStatus == EM_ZoneStatusType.DISCONNECT)
                return;
            foreach (LedDetail ledDetail in StaticPool.ledDetailCollection)
            {
                if (ledDetail.ZoneID == e.ZoneID)
                {
                    Led led = StaticPool.ledCollection.GetLed(ledDetail.LedID);
                    if (led != null)
                    {
                        ZONE zone = StaticPool.zoneCollection.GetZONE(e.ZoneID);
                        if (zone != null)
                        {
                            StaticPool.UpdateLedSlotInfo(led, zone);
                        }
                    }
                }
            }
        }

        public static void UpdateKZIODisplay(string zoneID, EM_ZoneStatusType zoneStatus, EM_ZoneStatusType oldStatus)
        {
            if (zoneStatus == EM_ZoneStatusType.DISCONNECT)
                return;
            ZONE zone = StaticPool.zoneCollection.GetZONE(zoneID);
            if (zone == null)
            {
                return;
            }
            if (zone.OldStatus == (int)EM_ZoneStatusType.OCCUPIED && zone.Status == (int)EM_ZoneStatusType.ORDER)
            {
                return;
            }
            foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
            {
                if (outputDetail.ZoneID == zoneID)
                {
                    Output output = StaticPool.outputCollection.GetOutput(outputDetail.OutputID);
                    if (output != null)
                    {
                        StaticPool.UpdateOutputSlotInfo(output, zone.OldStatus, zone.Status, outputDetail.RelayIndex);
                    }
                }
            }
        }

        private void UpdateSlotTable(ZoneEventArgs e)
        {
            foreach (Control control in panelSlotTableContent.Controls.OfType<ucSlotTable>())
            {
                if (control.Name.Contains(e.ZoneID))
                {
                    ucSlotTable uc = (ucSlotTable)control;
                    uc?.Invoke(new Action(() =>
                    {
                        uc.UpdateStatus((int)e.ZoneStatus);
                    }));
                }
            }
        }
        public static void SaveToDB(ZoneEventArgs e)
        {
            ZONE zone = StaticPool.zoneCollection.GetZONE(e.ZoneID);
            if (zone != null)
            {
                ZCU zcu = StaticPool.zcuCollection.GetZCUById(zone.ZCUId);
                IZCU iZCU = ZcuFactory.GetZCU(zcu);
                if (iZCU != null)
                {
                    string plateNum = "";
                    string imagePath = "";
                    iZCU.GetZoneDetail(zone.ZcuIndex, ref plateNum, ref imagePath);
                    zone.PlateNum = plateNum;
                    zone.ImagePath = imagePath;
                }
                if (zone.Status == (int)EM_ZoneStatusType.ORDER)
                {
                    tblZone.UpdateZoneStatus(zone.Status, zone.OrderPlateNumber, "", zone.Id);
                }
                else
                {
                    tblZone.UpdateZoneStatus(zone.Status, zone.PlateNum, zone.ImagePath, zone.Id);
                }
                tblZoneEvent.Insert(zone, DateTime.Now);
            }
        }
        public static void Update_OrderEvent_FormDisplay(ZoneEventArgs e)
        {
            foreach (Control control in frm.panelMap.Controls.OfType<ucZoneInMap>())
            {
                ucZoneInMap _ucZoneInMap = (ucZoneInMap)control;
                if (_ucZoneInMap.ZoneID == e.ZoneID)
                {
                    _ucZoneInMap.Invoke(new Action(() =>
                    {
                        _ucZoneInMap.SetStatus(e.ZoneStatus == EM_ZoneStatusType.ORDER ? Color.Yellow : Color.DarkGreen);
                    }));
                }
            }
            foreach (Control control in frm.panelSlotTableContent.Controls)
            {
                if (control.Name.Contains(e.ZoneID))
                {
                    ucSlotTable uc = (ucSlotTable)control;
                    uc?.Invoke(new Action(() =>
                    {
                        uc.UpdateStatus((int)e.ZoneStatus);
                    }));
                }
            }
        }
        #endregion

        #region: Internal
        //Load
        private void LoadData()
        {
            LogHelper.Logger_Info("Load Selected Map");
            ShowMap(this.mapID);
            LogHelper.Logger_Info("Load Slot Table");
            LoadSlotTable();
            //LoadZCUTable();
            LogHelper.Logger_Info("Set KZIO Zone Old State");
            StaticPool.InitOutputColor();
            //StaticPool.InitLedDisplay();
        }
        #region:Internal Map Function
        private void ClearOldMapData()
        {
            foreach (Control control in panelMap.Controls.OfType<ucZoneInMap>())
            {
                DeleteControls.Add(control);
            }
            foreach (Control deleteControl in DeleteControls)
            {
                panelMap.Controls.Remove(deleteControl);
            }
            DeleteControls.Clear();
            txtMapPage.Text = (GetMapIndex(this.mapID) + 1) + "";
        }
        //Select Map
        private void SetNormalMap_Direction()
        {
            UISync.Execute(() =>
            {
                btnNextMap.Enabled = true;
                btnLastMap.Enabled = true;
                btnPreviusMap.Enabled = true;
                btnPreviusMap.Enabled = true;
            });
        }
        private void SetFirstMap_Direction()
        {
            UISync.Execute(() =>
            {
                btnNextMap.Enabled = true;
                btnLastMap.Enabled = true;
                btnPreviusMap.Enabled = false;
                btnFirstMap.Enabled = false;
            });
        }
        private void SetLastMap_Direction()
        {
            UISync.Execute(() =>
            {
                btnNextMap.Enabled = false;
                btnLastMap.Enabled = false;
                btnPreviusMap.Enabled = true;
                btnFirstMap.Enabled = true;
            });
        }
        //End Select Map

        //Show Map
        public void ShowNextMap(string currentMapID)
        {
            ClearOldMapData();
            int currenMapIndex = GetMapIndex(currentMapID);
            int lastMapIndex = StaticPool.mapCollection.Count - 1;
            if (currenMapIndex + 1 == lastMapIndex)
            {
                SetLastMap_Direction();
            }
            else
            {
                SetNormalMap_Direction();
            }
            this.mapID = StaticPool.mapCollection[currenMapIndex + 1].ID;
            StaticPool.selectedMAPID = this.mapID;
            ShowMap(this.mapID);
        }
        public void ShowLastMap()
        {
            ClearOldMapData();
            SetLastMap_Direction();
            this.mapID = StaticPool.mapCollection[StaticPool.mapCollection.Count - 1].ID;
            StaticPool.selectedMAPID = this.mapID;
            ShowMap(this.mapID);
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
            StaticPool.selectedMAPID = this.mapID;
            ShowMap(this.mapID);
        }
        public void ShowFirstMap()
        {
            ClearOldMapData();
            SetFirstMap_Direction();
            this.mapID = StaticPool.mapCollection[0].ID;
            StaticPool.selectedMAPID = this.mapID;
            ShowMap(this.mapID);
        }
        //End Show Map

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
                            int posX = (int)mapDetail.PosX;
                            int posY = (int)mapDetail.PosY;
                            int zoneWidth = mapDetail.zoneWidth;
                            int zoneHeight = mapDetail.zoneHeight;
                            StandardizedLocation(ref posX, ref posY, ref zoneWidth, ref zoneHeight, mapDetail.picMapWidth, mapDetail.picMapHeight);
                            CreateNewZoneInMap(zone, posX, posY, zoneWidth, zoneHeight);
                        }
                    }
                }
            }
        }
        public void ShowMap(string mapID)
        {
            LoadZoneInMap(mapID);
            Map selectedMap = StaticPool.mapCollection.GetMap(mapID);
            if (selectedMap != null)
            {
                if (File.Exists(selectedMap.ImagePath))
                {
                    picMAP?.Invoke(new Action(() =>
                    {
                        picMAP.Image = Image.FromFile(selectedMap.ImagePath);
                    }));
                    lbMAPName?.Invoke(new Action(() =>
                    {
                        lbMAPName.Text = selectedMap.Name;
                        lbMAPName.BackColor = Color.White;
                    }));
                }
            }

            int currenMapIndex = GetMapIndex(mapID);
            UISync.Execute(() =>
            {
                txtMapPage.Text = (currenMapIndex + 1) + "";
                lblMaxMapPage.Text = (StaticPool.mapCollection.Count) + "";
            });
            if (StaticPool.mapCollection.Count > 1)
            {
                int lastMapIndex = StaticPool.mapCollection.Count - 1;
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
            }
            else
            {
                UISync.Execute(() =>
                {
                    btnNextMap.Enabled = false;
                    btnLastMap.Enabled = false;
                    btnPreviusMap.Enabled = false;
                    btnPreviusMap.Enabled = false;
                });
            }
            UISync.Execute(() =>
            {
                lbMAPName.BringToFront();
            });
        }
        private void StandardizedLocation(ref int posX, ref int posY, ref int width, ref int height, int mapWidth, int mapheight)
        {
            int mapDetailWidth = mapWidth;
            int mapDetailHeight = mapheight;
            int mapDetailPosX = Properties.Settings.Default.picMapDetailPosX;
            int mapDetailPosY = Properties.Settings.Default.picMapDetailPosY;

            int mapMainWidth = picMAP.Width;
            int mapMainHeight = picMAP.Height;
            int mapMainPosX = picMAP.Location.X;
            int mapMainPosY = picMAP.Location.Y;

            //UISync.Execute(() =>
            //{
            //    Rectangle screenRectangle = this.RectangleToScreen(this.ClientRectangle);
            //    int titleHeight = screenRectangle.Top - this.Top;
            //});
            posX = (posX - mapDetailPosX) * mapMainWidth / mapDetailWidth + mapMainPosX;
            posY = (posY - mapDetailPosY) * mapMainHeight / mapDetailHeight + mapMainPosY;//+ mainMenuStrip.Height;
            width = width * mapMainWidth / mapDetailWidth > 0 ? width * mapMainWidth / mapDetailWidth : 1;
            height = height * mapMainHeight / mapDetailHeight > 0 ? height * mapMainHeight / mapDetailHeight : 1;
        }
        private void SetUcZoneInMapInfor(ZONE zone, ucZoneInMap _ucZoneInMap)
        {
            _ucZoneInMap.Name = "uc:Zone :" + zone.Id;
            _ucZoneInMap.ZoneID = zone.Id;
            _ucZoneInMap.MapID = this.mapID;
        }
        #endregion

        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);

        }


        #region: CONTROL IO
        private async Task ControlIO()
        {
            while (true)
            {
                await Task.Delay(5000);
                {
                    try
                    {
                        tblOutput.LoadDataOutput(StaticPool.outputCollection);
                        foreach (Output output in StaticPool.outputCollection)
                        {
                            StaticPool.GetOutputSlotCount(output);
                            LogHelper.Logger_IO_Info($"Timer SEND: {output.Name}");
                            StaticPool.SetOutputArrayColor(output);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Logger_Error(ex.Message);
                    }

                }
            }

        }
        #endregion

        private void tétToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> relays = new List<int>();
            for (int i = 1; i <= 63; i++)
            {
                relays.Add(i);
            }
            relays = relays.OrderBy(x => x).ToList();
            int[] bytes = new int[8];
            for (int byteIndex = 0; byteIndex < bytes.Length; byteIndex++)
            {
                bytes[byteIndex] = 0;
            }
            for (int i = 0; i < relays.Count; i++)
            {
                //1,2,3,4,5,6,7,8||9,10,11,12,13,14,15,16||...64
                int byteIndex = relays[i] % 8 == 0 ? (relays[i] / 8) - 1 : (relays[i] / 8);
                bytes[byteIndex] += (int)Math.Pow(2, relays[i] % 8 == 0 ? 7 : relays[i] % 8 - 1);
            }

            List<string> hexValues = bytes.Select(x => x.ToString("X2")).ToList();
            MessageBox.Show(String.Join(".", hexValues));
        }
    }
}

