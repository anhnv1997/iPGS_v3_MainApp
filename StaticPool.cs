using System;
using System.Collections.Generic;
using iParking.Object;
using iParking.Database;
using iParking.Databases;
using System.Windows.Forms;
using iParking.Enums;
using Kztek.KZIO.Object;
using Kztek.KZIO.IO_Controllers;
using Kztek.LedController;

namespace iParking
{
    public static class StaticPool
    {
        #region:Properties
        // CSDL
        public static MDB mdb = null;

        public static bool isChangeCCU = false;
        public static bool isChangeFloor = false;
        public static bool isChangeLed = false;
        public static bool isChangeLedDetail = false;
        public static bool isChangeMap = false;
        public static bool isChangeMapDetail = false;
        public static bool isChangeOutput = false;
        public static bool isChangeOutputDetail = false;
        public static bool isChangeTMA = false;
        public static bool isChangeVehicleType = false;
        public static bool isChangeZoneGroup = false;
        public static bool isChangeZone = false;
        public static bool isChangeZcu = false;

        public static string selectedMAPID = Properties.Settings.Default.selectedMAP;
        public static int zoneWidth = Properties.Settings.Default.zoneWidth;
        public static int zoneHeight = Properties.Settings.Default.zoneHeight;

        public static bool isChangeSetting = false;

        public static ZCUCollection zcuCollection = new ZCUCollection();
        public static CCUCollection ccuCollection = new CCUCollection();
        public static ZONECollection zoneCollection = new ZONECollection();
        public static ZoneGroupCollection zoneGroupCollection = new ZoneGroupCollection();
        public static FloorCollection floorCollection = new FloorCollection();
        public static MapCollection mapCollection = new MapCollection();
        public static OutputCollection outputCollection = new OutputCollection();
        public static OutputDetailCollection outputDetailCollection = new OutputDetailCollection();
        public static MapDetailCollection mapDetailCollection = new MapDetailCollection();
        public static LedCollection ledCollection = new LedCollection();
        public static LedDetailCollection ledDetailCollection = new LedDetailCollection();
        public static VehicleTypeCollection vehicleTypeCollection = new VehicleTypeCollection();
        public static TMA_ServerCollection TMACollection = new TMA_ServerCollection();
        public static VehicleZoneDetailCollection vehicleZoneDetails = new VehicleZoneDetailCollection();

        public static List<MapUpdateInfor> mapUpdateInfors = new List<MapUpdateInfor>();
        #endregion
        #region:Datagridview
        public static string Id(DataGridView dgv)
        {
            string _lcma = "";
            DataGridViewRow _drv = dgv.CurrentRow;
            try
            {
                _lcma = _drv.Cells["_ID"].Value.ToString();
            }
            catch
            {
                _lcma = "";
            }
            return _lcma;
        }
        #endregion

        #region:Load
        public static void LoadFloorData(FloorCollection floorCollection)
        {
            tblFloor.LoadDataFloor(floorCollection);
            tblZoneGroup.LoadDataGroup(StaticPool.zoneGroupCollection);
            foreach (Floor floor in StaticPool.floorCollection)
            {
                ZoneGroupCollection zoneGroupCollection = new ZoneGroupCollection();
                foreach (ZoneGroup zg in StaticPool.zoneGroupCollection)
                {
                    if (zg.FloorID == floor.Id)
                    {
                        zoneGroupCollection.Add(zg);
                    }
                }
                floor.ZgCollection = zoneGroupCollection;
            }
        }
        public static void LoadColorTypes(ComboBox comboBox)
        {
            foreach (int i in Enum.GetValues(typeof(iParking.Enums.EM_LedColor)))
            {
                comboBox.Items.Add(Enum.GetName(typeof(iParking.Enums.EM_LedColor), i));
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadLedType(ComboBox comboBox)
        {
            foreach (int i in Enum.GetValues(typeof(EM_LedTypes)))
            {
                comboBox.Items.Add(Enum.GetName(typeof(EM_LedTypes), i));
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadArrowType(ComboBox comboBox)
        {
            foreach (int i in Enum.GetValues(typeof(EM_LedArrowDirection)))
            {
                comboBox.Items.Add(Enum.GetName(typeof(EM_LedArrowDirection), i));
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadGroupData(ComboBox comboBox)
        {
            foreach (ZoneGroup group in StaticPool.zoneGroupCollection)
            {
                ListItem listGroup = new ListItem();
                listGroup.Value = group.Id;
                listGroup.Name = group.Name;
                comboBox.Items.Add(listGroup);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadVehicleTypeData(ComboBox comboBox)
        {
            foreach (VehicleType vehicleType in StaticPool.vehicleTypeCollection)
            {
                ListItem listVehicleType = new ListItem();
                listVehicleType.Value = vehicleType.ID;
                listVehicleType.Name = vehicleType.Name;
                comboBox.Items.Add(listVehicleType);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
            //foreach (int i in Enum.GetValues(typeof(iParking.Enums.EM_VehicleTypes)))
            //{
            //    comboBox.Items.Add(Enum.GetName(typeof(iParking.Enums.EM_VehicleTypes), i));
            //}
            //if (comboBox.Items.Count > 0)
            //{
            //    comboBox.SelectedIndex = 0;
            //}
        }
        public static void LoadZCUData(ComboBox comboBox)
        {
            foreach (ZCU zcu in StaticPool.zcuCollection)
            {
                ListItem listZCU = new ListItem();
                listZCU.Value = zcu.Id;
                listZCU.Name = zcu.ZcuName;
                comboBox.Items.Add(listZCU);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadFloorData(ComboBox comboBox)
        {
            foreach (Floor floor in StaticPool.floorCollection)
            {
                ListItem listFloor = new ListItem();
                listFloor.Value = floor.Id;
                listFloor.Name = floor.Name;
                comboBox.Items.Add(listFloor);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadCCUData(ComboBox comboBox)
        {
            foreach (CCU ccu in StaticPool.ccuCollection)
            {
                ListItem listCCU = new ListItem();
                listCCU.Value = ccu.Id;
                listCCU.Name = ccu.Name;
                comboBox.Items.Add(listCCU);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadZcuTypes(ComboBox comboBox)
        {
            foreach (int i in Enum.GetValues(typeof(EM_ZcuTypes)))
            {
                comboBox.Items.Add(Enum.GetName(typeof(EM_ZcuTypes), i));
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadOutputType(ComboBox comboBox)
        {
            foreach (int i in Enum.GetValues(typeof(EM_KZIO_TYPES)))
            {
                comboBox.Items.Add(Enum.GetName(typeof(EM_KZIO_TYPES), i));
            }
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadLedDevice(ComboBox comboBox)
        {
            foreach (Led led in StaticPool.ledCollection)
            {
                ListItem lisLEd = new ListItem();
                lisLEd.Value = led.Id;
                lisLEd.Name = led.Name;
                comboBox.Items.Add(lisLEd);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadOutput(ComboBox comboBox)
        {
            foreach (Output output in StaticPool.outputCollection)
            {
                ListItem listOutput = new ListItem();
                listOutput.Value = output.ID;
                listOutput.Name = output.Name;
                comboBox.Items.Add(listOutput);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        public static void LoadTMA(ComboBox comboBox)
        {
            foreach (TMA_Server _TMA_SERVER in StaticPool.TMACollection)
            {
                ListItem listTMA = new ListItem();
                listTMA.Value = _TMA_SERVER.Id;
                listTMA.Name = _TMA_SERVER.Name;
                comboBox.Items.Add(listTMA);
            }
            comboBox.DisplayMember = "Name";
            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }
        #endregion

        #region:Math
        public static long HexToDec(string hexNumber)
        {
            return Convert.ToInt64(hexNumber, 16);
        }
        public static string HexToBin(string hexNumber, int binaryLength)
        {
            string binData = Convert.ToString(HexToDec(hexNumber), 2);
            while (binData.Length < 8)
            {
                binData = "0" + binData;
            }
            return binData;
        }
        #endregion

        #region:output
        public static void UpdateOutputSlotInfo(Output output, int oldStatus, int Status, int outputIndex)
        {
            bool isUnOccupiedEvent = oldStatus == (int)EM_ZoneStatusType.OCCUPIED && Status == (int)EM_ZoneStatusType.UN_OCCUPIED;
            bool isOccupiedEvent = oldStatus == (int)EM_ZoneStatusType.UN_OCCUPIED && Status == (int)EM_ZoneStatusType.OCCUPIED;
            bool isOrderSuccessEvent = oldStatus == (int)EM_ZoneStatusType.ORDER && Status == (int)EM_ZoneStatusType.OCCUPIED;
            if (isUnOccupiedEvent)
            {
                output.SlotCounts[outputIndex]++;
            }
            else if (isOccupiedEvent || isOrderSuccessEvent)
            {
                if (output.SlotCounts[outputIndex] > 0)
                    output.SlotCounts[outputIndex]--;
            }
            else
            {
                return;
            }
            SetOutputColor(output, outputIndex);

        }
        public static void InitOutputColor()
        {
            foreach (Output output in StaticPool.outputCollection)
            {
                GetOutputSlotCount(output);
                StaticPool.SetOutputColor(output);
            }
        }

        public static void GetOutputSlotCount(Output output)
        {
            foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
            {
                if (output.ID == outputDetail.OutputID)
                {
                    ZONE zone = StaticPool.zoneCollection.GetZONE(outputDetail.ZoneID);
                    if (zone != null)
                    {
                        switch (zone.Status)
                        {
                            case (int)EM_ZoneStatusType.UN_OCCUPIED:
                            case (int)EM_ZoneStatusType.DISCONNECT:
                            case (int)EM_ZoneStatusType.ORDER:
                                break;
                            default:
                                if (output.SlotCounts[outputDetail.RelayIndex] > 0)
                                    output.SlotCounts[outputDetail.RelayIndex]--;
                                break;
                        }
                    }
                }
            }
        }
        public static void SetOutputColor(Output output)
        {
            I_KZIO i_KZIO = KZIO_factory.GetKZIO((EM_KZIO_TYPES)output.OutputType, output.IPAddress);
            if (i_KZIO != null)
            {
                foreach (KeyValuePair<int, int> slotCountInfor in output.SlotCounts)
                {
                    if (slotCountInfor.Value == 0)
                    {
                        if (!i_KZIO.SetLedState(slotCountInfor.Key, EM_LEDSTATE.RED))
                        {
                            if (!i_KZIO.SetLedState(slotCountInfor.Key, EM_LEDSTATE.RED))
                            {
                                LogHelper.Logger_IO_Info(output.Name + ":" + slotCountInfor.Key + "==> RED: FAIL");
                                return;
                            }
                        }
                        LogHelper.Logger_IO_Info(output.Name + ":" + slotCountInfor.Key + "==> RED: SUCCESS");
                    }
                    else
                    {
                        if (!i_KZIO.SetLedState(slotCountInfor.Key, EM_LEDSTATE.GREEN))
                        {
                            if (!i_KZIO.SetLedState(slotCountInfor.Key, EM_LEDSTATE.GREEN))
                            {
                                LogHelper.Logger_IO_Info(output.Name + ":" + slotCountInfor.Key + "==> RED: FAIL");
                                return;
                            }
                        }
                        LogHelper.Logger_IO_Info(output.Name + ":" + slotCountInfor.Key + "==> RED: SUCCESS");
                    }
                }
            }
            else
            {
                LogHelper.Logger_IO_Error($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}-KZIO NULL");
            }
        }
        public static int status = 0;
        public static void SetOutputArrayColor(Output output)
        {
            I_KZIO i_KZIO = KZIO_factory.GetKZIO((EM_KZIO_TYPES)output.OutputType, output.IPAddress);
            if (i_KZIO != null)
            {
                //status = status == 0 ? 1 : 0;
                int[] states = new int[output.SlotCounts.Count];
                string stateStr = "";
                foreach (KeyValuePair<int, int> slotCountInfor in output.SlotCounts)
                {
                    //states[slotCountInfor.Key - 1] = status;
                    states[slotCountInfor.Key - 1] = slotCountInfor.Value == 0 ? (int)EM_LEDSTATE.RED : (int)EM_LEDSTATE.GREEN;
                    stateStr = slotCountInfor.Value == 0 ? stateStr + EM_LEDSTATE.RED.ToString() + "," : stateStr + EM_LEDSTATE.GREEN.ToString() + ",";
                }
                bool result = false;
                switch (output.OutputType)
                {

                    case (int)EM_OutputTypes.KZ_IO0808:
                        result = i_KZIO.SetLedArray8State(states);
                        if (!result)
                        {
                            result = i_KZIO.SetLedArray8State(states);
                            if (!result)
                            {
                                LogHelper.Logger_IO_Info($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}- Set Output Array Error: {stateStr}");
                                return;
                            }
                        }
                        else
                        {
                            LogHelper.Logger_IO_Info($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}- Set Output Array Success: {stateStr}");
                        }
                        break;
                    case (int)EM_OutputTypes.KZ_IO1616:
                        result = i_KZIO.SetLedArray16State(states);
                        if (!result)
                        {
                            result = i_KZIO.SetLedArray16State(states);
                            if (!result)
                            {
                                LogHelper.Logger_IO_Info($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}- Set Output Array Error: {stateStr}");
                                return;
                            }
                        }
                        else
                        {
                            LogHelper.Logger_IO_Info($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}- Set Output Array Success: {stateStr}");
                        }
                        break;
                    default:
                        LogHelper.Logger_IO_Error($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}- Output Type Error");
                        break;
                }
            }
            else
            {
                LogHelper.Logger_IO_Error($"{output.IPAddress }- {((EM_KZIO_TYPES)output.OutputType).ToString()}-KZIO NULL");
            }
        }

        public static void SetOutputColor(Output output, int relayIndex)
        {
            I_KZIO i_KZIO = KZIO_factory.GetKZIO((EM_KZIO_TYPES)output.OutputType, output.IPAddress);
            if (i_KZIO != null)
            {

                if (output.SlotCounts[relayIndex] == 0)
                {
                    if (!i_KZIO.SetLedState(relayIndex, EM_LEDSTATE.RED))
                    {
                        if (!i_KZIO.SetLedState(relayIndex, EM_LEDSTATE.RED))
                        {
                            if (!i_KZIO.SetLedState(relayIndex, EM_LEDSTATE.RED))
                            {
                                LogHelper.Logger_IO_Info(output.Name + ":" + relayIndex + "==> RED: FAIL");
                                return;
                            }
                        }
                    }
                    LogHelper.Logger_IO_Info(output.Name + ":" + relayIndex + "==> RED: SUCCESS");
                }
                else
                {
                    if (!i_KZIO.SetLedState(relayIndex, EM_LEDSTATE.GREEN))
                    {
                        if (!i_KZIO.SetLedState(relayIndex, EM_LEDSTATE.GREEN))
                        {
                            if (!i_KZIO.SetLedState(relayIndex, EM_LEDSTATE.GREEN))
                            {
                                LogHelper.Logger_IO_Info(output.Name + ":" + relayIndex + "==> GREEN: FAIL");
                                return;
                            }
                        }
                    }
                    LogHelper.Logger_IO_Info(output.Name + ":" + relayIndex + "==> GREEN: SUCCESS");
                }
            }
        }
        #endregion

        #region:Led
        public static void UpdateLedSlotInfo(Led led, ZONE zone)
        {
            bool isUnOccupiedEvent = zone.OldStatus == (int)EM_ZoneStatusType.OCCUPIED && zone.Status == (int)EM_ZoneStatusType.UN_OCCUPIED;
            bool isOccupiedEvent = zone.OldStatus == (int)EM_ZoneStatusType.UN_OCCUPIED && zone.Status == (int)EM_ZoneStatusType.OCCUPIED;
            bool isOrderEvent = zone.OldStatus == (int)EM_ZoneStatusType.UN_OCCUPIED && zone.OldStatus == (int)EM_ZoneStatusType.ORDER;
            //New ZOne
            if (zone.OldStatus == (int)EM_ZoneStatusType.DISCONNECT)
            {
                if (zone.Status == (int)EM_ZoneStatusType.OCCUPIED || zone.Status == (int)EM_ZoneStatusType.ORDER)
                {
                    led.SlotCount--;
                }
                else
                {
                    led.SlotCount++;
                }
            }
            // Old Zone
            else
            {
                if (isUnOccupiedEvent)
                {
                    led.SlotCount++;
                }
                else if (isOccupiedEvent || isOrderEvent)
                {
                    led.SlotCount--;
                }
                else
                {
                    return;
                }
            }
            SetLedDisplay(led);
        }
        public static void SetLedDisplay(Led led)
        {
            if (led.LedType != (int)EM_LedTypes.Global_Led)
            {
                int ledType = led.LedType;
                ILED iLed = LedFactory.GetLedController((EM_ModuleType)ledType, led.IPAddress, led.Port);
                if (iLed != null)
                {
                    if (led.SlotCount == 0)
                    {
                        iLed.SetParkingSlot(led.SlotCount, led.LedArrow, led.ZeroColor);
                        return;
                    }
                    iLed.SetParkingSlot(led.SlotCount, led.LedArrow, led.Color);
                }
            }
        }
        public static void InitLedDisplay()
        {
            foreach (Led led in StaticPool.ledCollection)
            {
                foreach (LedDetail ledDetail in StaticPool.ledDetailCollection)
                {
                    if (led.Id == ledDetail.LedID)
                    {
                        ZONE zone = StaticPool.zoneCollection.GetZONE(ledDetail.ZoneID);
                        if (zone != null)
                        {
                            switch (zone.Status)
                            {
                                case (int)EM_ZoneStatusType.UN_OCCUPIED:
                                case (int)EM_ZoneStatusType.DISCONNECT:
                                    break;
                                default:
                                    led.SlotCount--;
                                    break;
                            }
                        }
                    }
                }
                StaticPool.SetLedDisplay(led);
            }
        }
        #endregion
    }
}
