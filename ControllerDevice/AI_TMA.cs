using iParking.Events;
using iParking.Tools;
using System.Threading;
using iParking.Enums;
using System.Windows;
using System.Collections.Generic;
using AI_TMA;
using iParking.Object;
using RestSharp;
using System;
using System.Linq;

namespace iParking.Device
{
    public class AI_TMA : IZCU
    {
        private ZCU _ZCU;
        string id = "";
        public AI_TMA(string _ipAddress, int _port, string _username, string _password, string _id)
        {
            this.IpAddress = _ipAddress;
            this.Port = _port;
            this.Username = _username;
            this.password = _password;
            this.id = _id;
        }
        public AI_TMA(ZCU zcu)
        {
            this._ZCU = zcu;
            this.IpAddress = zcu.IPAddress;
            this.Port = zcu.Port;
            this.Username = zcu.Username;
            this.password = zcu.Password;
            this.id = zcu.Id;
        }
        #region:Properties
        private string ipAddress = "192.168.1.1";
        private int port = 100;
        private string username = "admin";
        private string password = "admin";
        public event ZoneEvent zoneEvent;
        public event StatusChangeEvent statusChangeEvent;

        private ParkingLot parkingLot = null;
        private static List<int> streamIDs = new List<int>();
        #endregion

        #region:Getter,setter
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public int Port { get => port; set => port = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        #endregion

        public bool Connect()
        {
            if (NetWorkTools.IsPingSuccess(this.IpAddress, 500))
            {
                if (parkingLot == null)
                {
                    parkingLot = new ParkingLot(this.ipAddress, this.username, this.password);
                    LogHelper.Logger_Info("Connect MQTT: " + this.ipAddress + this.username + this.password);
                }
                return true;
            }
            return false;
        }
        public void PollingStart()
        {
            LogHelper.Logger_Info(this.IpAddress);
            try
            {
                while (true)
                {
                    if (NetWorkTools.IsPingSuccess(this.IpAddress, 500))
                    {
                        if (parkingLot == null)
                        {
                            parkingLot = new ParkingLot(this.ipAddress, this.username, this.password);
                            LogHelper.Logger_Info("Connect MQTT: " + this.ipAddress + this.username + this.password);
                        }
                        parkingLot.OnDataReceived += ParkingLot_OnDataReceived;
                        parkingLot.Start();
                        for (int i = 1; i <= StaticPool.zcuCollection.Count; i++)
                        {
                            streamIDs.Add(i);
                        }
                        LogHelper.Logger_Info("Start MQTT");
                        break;
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (System.Exception ex)
            {
                LogHelper.Logger_Error("Error " + ex.Message);
            }
        }

        private void ParkingLot_OnDataReceived(object sender, ParkingLotRecResponseEventArg e)
        {
            //string dataInput = "";

            //LogHelper.Logger_Info("Have New TMA Event Data");
            ////Get Disconnect List
            //foreach (ParkingLotRec_Result rec_Result in e.Data.result)
            //{
            //     dataInput += rec_Result.stream_id+ "";

            //    streamIDs.Remove(rec_Result.stream_id + 1);
            //}
            //string disconnectList = "";
            //foreach(int id   in streamIDs)
            //{
            //    disconnectList += id;
            //}
            //LogHelper.Logger_Error("Input: " + dataInput);
            //LogHelper.Logger_Error("disconnect: " + disconnectList);

            //foreach (int streamID in streamIDs)
            //{
            //    foreach (ZCU zcu in StaticPool.zcuCollection)
            //    {
            //        if (zcu.TMA_ID == this.id)
            //        {
            //            if (zcu.TMA_Index == streamID)
            //            {
            //                int[] statuses = new int[6] { (int)EM_ZoneStatusType.DISCONNECT, (int)EM_ZoneStatusType.DISCONNECT, (int)EM_ZoneStatusType.DISCONNECT, (int)EM_ZoneStatusType.DISCONNECT, (int)EM_ZoneStatusType.DISCONNECT, (int)EM_ZoneStatusType.DISCONNECT };
            //                for (int i = 0; i < statuses.Length; i++)
            //                {
            //                    foreach (ZONE zone in StaticPool.zoneCollection)
            //                    {
            //                        if (zone.ZCUId == zcu.Id && zone.ZcuIndex == (i + 1))
            //                        {
            //                            ExcecuteZoneEvent(statuses, i, zone);
            //                            break;
            //                        }
            //                    }
            //                }
            //                break;
            //            }
            //        }
            //    }

            //}
            //streamIDs.Clear();
            //for (int i = 1; i <= StaticPool.zcuCollection.Count; i++)
            //{
            //    streamIDs.Add(i);
            //}
            List<ParkingLotRec_Result> results = (from  result in e.Data.result
                                                 orderby result.stream_id
                                                 select result).ToList();
            foreach (ParkingLotRec_Result rec_Result in results)
            {
                int TMA_INDEX = rec_Result.stream_id + 1;
                foreach (ZCU zcu in StaticPool.zcuCollection)
                {
                    if (zcu.TMA_ID == this.id)
                    {
                        if (zcu.TMA_Index == TMA_INDEX)
                        {
                            int[] statuses = new int[rec_Result.slot_status.Count];
                            for (int i = 0; i < rec_Result.slot_status.Count; i++)
                            {
                                statuses[i] = rec_Result.slot_status[i].is_empty == true ? (int)EM_ZoneStatusType.UN_OCCUPIED : (int)EM_ZoneStatusType.OCCUPIED;
                            }
                            for (int i = 0; i < statuses.Length; i++)
                            {
                                foreach (ZONE zone in StaticPool.zoneCollection)
                                {
                                    if (zone.ZCUId == zcu.Id && zone.ZcuIndex == (i + 1)&&zone.isUseAI)
                                    {
                                        ExcecuteZoneEvent(statuses, i, zone);
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }

        }


        //Status    : Disconnect, Occupied, UnOccupied, Order
        //OldStatus : Disconnect, Occupied, UnOccupied, Order
        //Event Status: Occupied, UnOccupied
        private void ExcecuteZoneEvent(int[] statuses, int i, ZONE zone)
        {
            try
            {
                LogHelper.Logger_Info($@"Update Data {zone.zoneName} 
                                        \r\n From OldStatus:{((EM_ZoneStatusType)zone.OldStatus).ToString()}, CurrentStatus: {((EM_ZoneStatusType)zone.Status).ToString()}
                                        \r\n To EventStatus:{((EM_ZoneStatusType)statuses[i]).ToString()} 
                                      ");
                //if (zone.Status != (int)EM_ZoneStatusType.ORDER && zone.Status != -1)
                if (zone.Status != -1)
                {
                    ZoneEventArgs zoneEventArgs = new ZoneEventArgs();
                    zoneEventArgs.ZoneID = zone.Id;
                    zoneEventArgs.ZoneStatus = (EM_ZoneStatusType)statuses[i];

                    //if (zone.Status == (int)EM_ZoneStatusType.ORDER)
                    //{
                    //    foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
                    //    {
                    //        if (outputDetail.ZoneID == zoneEventArgs.ZoneID)
                    //        {
                    //            Output output = StaticPool.outputCollection.GetOutput(outputDetail.OutputID);
                    //            if (output != null)
                    //            {
                    //                StaticPool.UpdateOutputSlotInfo(output, zone.OldStatus, (int)zoneEventArgs.ZoneStatus, outputDetail.RelayIndex);
                    //            }
                    //        }
                    //    }
                    //}
                    //else if (zone.Status != (int)zoneEventArgs.ZoneStatus)
                    //{
                    //    foreach (OutputDetail outputDetail in StaticPool.outputDetailCollection)
                    //    {
                    //        if (outputDetail.ZoneID == zoneEventArgs.ZoneID)
                    //        {
                    //            Output output = StaticPool.outputCollection.GetOutput(outputDetail.OutputID);
                    //            if (output != null)
                    //            {
                    //                StaticPool.UpdateOutputSlotInfo(output, zone.Status, (int)zoneEventArgs.ZoneStatus, outputDetail.RelayIndex);
                    //            }
                    //        }
                    //    }
                    //}

                    if (zone.Status != (int)zoneEventArgs.ZoneStatus)
                    {
                        //Order
                        if (zone.Status == (int)EM_ZoneStatusType.ORDER)
                        {
                            if (zoneEventArgs.ZoneStatus == EM_ZoneStatusType.OCCUPIED)
                            {
                                zone.OldStatus = zone.Status;
                                zone.Status = (int)zoneEventArgs.ZoneStatus;
                                #region:Check order plate number
                                #endregion
                                this.zoneEvent?.Invoke(this, zoneEventArgs);
                                //Send API Thien Van Status thay đổi từ Order Sang Occupied
                                // MessageBox.Show("Send API Thien Van Status thay đổi từ Order Sang Occupied ");
                                TCS_Infor tCS_Infor = new TCS_Infor()
                                {
                                    EventType = "IN_SLOT",
                                    EventDate = DateTime.Now,
                                    SlotNumber = zone.Code,
                                    SlotName = zone.zoneName,
                                    VehicleNumber = zone.OrderPlateNumber
                                };
                                string result = SendAPI_TCS(tCS_Infor);
                                if (result != "")
                                {
                                    LogHelper.Logger_TCSError($@"Send API TCS IN_SLOT: Plate Number: {zone.OrderPlateNumber} Error: {result}");
                                }
                                else
                                {
                                    LogHelper.Logger_TCSInfo($@"Send API TCS IN_SLOT: Plate Number: {zone.OrderPlateNumber} Success");
                                }
                            }
                            else
                            {
                                zone.OldStatus = (int)zoneEventArgs.ZoneStatus;
                            }
                        }
                        else if (zone.OldStatus == (int)EM_ZoneStatusType.ORDER)
                        {
                            if (zone.Status == (int)EM_ZoneStatusType.DISCONNECT)
                            {
                                if (zoneEventArgs.ZoneStatus == EM_ZoneStatusType.OCCUPIED)
                                {
                                    zone.Status = (int)zoneEventArgs.ZoneStatus;
                                    this.zoneEvent?.Invoke(this, zoneEventArgs);
                                }
                            }
                            else if (zone.Status == (int)EM_ZoneStatusType.OCCUPIED)
                            {
                                zone.OldStatus = zone.Status;
                                zone.Status = (int)zoneEventArgs.ZoneStatus;
                                this.zoneEvent?.Invoke(this, zoneEventArgs);
                                // MessageBox.Show("Send API THIEN VAN: Slot Change Occupied=> UnOccupied");
                                //TCS_Infor tCS_Infor = new TCS_Infor()
                                //{
                                //    EventType = "OUT_SLOT",
                                //    EventDate = DateTime.Now,
                                //    SlotNumber = zone.Code,
                                //    SlotName = zone.zoneName,
                                //    VehicleNumber = zone.OrderPlateNumber
                                //};
                                //string result = SendAPI_TCS(tCS_Infor);
                                //if (result != "")
                                //{
                                //    LogHelper.Logger_TCSError($@"Send API TCS OUT_SLOT: Plate Number: {zone.OrderPlateNumber} Error: {result}");
                                //}
                                //else
                                //{
                                //    LogHelper.Logger_TCSInfo($@"Send API TCS OUT_SLOT: Plate Number: {zone.OrderPlateNumber} Success");
                                //}
                            }
                        }
                        //Normal
                        else
                        {
                            if (zone.Status != (int)EM_ZoneStatusType.DISCONNECT)
                            {
                                zone.OldStatus = zone.Status;
                            }
                            zone.Status = (int)zoneEventArgs.ZoneStatus;
                            this.zoneEvent?.Invoke(this, zoneEventArgs);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Logger_Error(ex.Message);
            }


        }

        public string SendAPI_TCS(TCS_Infor tCS_Infor)
        {
            try
            {
                var client = new RestClient("http://192.168.14.82:8094/api/EventInfo");
                client.Timeout = 500;
                var request = new RestRequest(Method.POST);
                request.RequestFormat = RestSharp.DataFormat.Json;
                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(tCS_Infor);
                request.AddJsonBody(jsonBody);
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    client = new RestClient("http://192.168.14.82:8094/api/EventInfo");
                    client.Timeout = 500;
                    request = new RestRequest(Method.POST);
                    request.RequestFormat = RestSharp.DataFormat.Json;
                    jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(tCS_Infor);
                    request.AddJsonBody(jsonBody);
                    response = client.Execute(request);
                    if (!response.IsSuccessful)
                    {
                        return response.ErrorMessage;
                    }
                }
                return "";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }


        }

        public void PollingStop()
        {

        }
        public bool DisConnect()
        {
            return false;
        }
        public void GetZoneDetail(int zoneIndex, ref string plateNum, ref string imagePath)
        {
            plateNum = "";
            imagePath = "";
        }
    }
}
