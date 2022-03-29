using iParking.CMD;
using iParking.Device;
using iParking.Enums;
using iParking.Events;
using iParking.Tools;
using System;
using System.Threading;

namespace iParking.ZCU_Controller
{
    public class Camera : IZCU
    {
        private Thread thread = null;
        private ManualResetEvent stopEvent = null;
        private ZCU _ZCU;
        #region:Properties
        private string ipAddress = "192.168.1.1";
        private int port = 100;
        private string username = "admin";
        private string password = "admin";
        private int delay_time = 10;
        private string id = "";
        public event ZoneEvent zoneEvent;
        public event WarningEvent warningEvent;
        public event StatusChangeEvent statusChangeEvent;

        private const string CHECK_START_EVENT = "01";
        #endregion
        public Camera(string _ipAddress, int _port, string _username, string _password)
        {
            this.IpAddress = _ipAddress;
            this.Port = _port;
            this.Username = _username;
            this.password = _password;
        }
        public Camera(ZCU zcu)
        {
            _ZCU = zcu;
            this.IpAddress = zcu.IPAddress;
            this.Port = zcu.Port;
            this.Username = zcu.Username;
            this.password = zcu.Password;
            this.ID = zcu.Id;
        }
        #region:Getter,setter
        public string IpAddress { get => ipAddress; set => ipAddress = value; }
        public int Port { get => port; set => port = value; }
        public string Username { get => username; set => username = value; }
        public string ID { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }

        public int DelayTime { get => delay_time; set => delay_time = value; }
        #endregion
        public bool Connect()
        {
            if (NetWorkTools.IsPingSuccess(this.IpAddress, 500))
            {
                return true;
            }
            return false;
        }
        // Polling Start
        public void PollingStart()
        {
            if (thread == null)
            {
                // create events
                stopEvent = new ManualResetEvent(false);

                // start thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Start();
            }
        }
        // is Running
        public bool Running
        {
            get
            {
                if (thread != null)
                {
                    if (thread.Join(0) == false)
                        return true;

                    // the thread is not running, so free resources
                    Free();
                }
                return false;
            }
        }
        // Signal thread to stop work
        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
            }
        }
        // Wait for thread stop
        public void WaitForStop()
        {
            if (thread != null)
            {
                // wait for thread stop
                thread.Join();

                Free();
            }
        }
        // Free resources
        private void Free()
        {
            thread = null;

            // release events
            stopEvent.Close();
            stopEvent = null;
        }
        // Stop
        public void PollingStop()
        {
            if (this.Running)
            {
                SignalToStop();
                while (thread.IsAlive)
                {
                    if (WaitHandle.WaitAll(
                        (new ManualResetEvent[] { stopEvent }),
                        100,
                        true))
                    {
                        WaitForStop();
                        break;
                    }
                }
            }
        }

        public void WorkerThread()
        {
            while (!stopEvent.WaitOne(0, true))
            {
                try
                {
                    string viewraw = "";
                    string[] message = null;

                    #region: Nhận và xử lý dữ liệu từ ZCU, update dữ liệu vào ZoneEventAtgs
                    //addressArray = new string[96];
                    //spaceStatusEachCam = new int[96];
                    string GetStateCMD = "GetStateAllSensor?/";
                    string response = TCPTools.ExecuteCommand_Ascii(this.IpAddress, 100, null, GetStateCMD, ref viewraw, ref message, delay_time, TCPTools.STX);
                    //EventData : Array[34] <=>STX EventData ETX
                    //Format: HexData qua convert ==>"01"+EventStatus1+EventStatus2+EventStatus3
                    if (message != null && message.Length >= 34)
                    {
                        if (_ZCU.IsConnect == false)
                        {
                            _ZCU.IsConnect = true;
                            ZCUStatusEventArgs e = new ZCUStatusEventArgs();
                            e.ZCUID = _ZCU.Id;
                            e.Status = "Connected";
                            statusChangeEvent?.Invoke(this, e);
                        }
                        for (int i = 1; i <= 32; i++)
                        {
                            ExcecuteEventData(message, i);
                        }
                    }
                    else
                    {
                        if (_ZCU.IsConnect == true)
                        {
                            _ZCU.IsConnect = false;
                            ZCUStatusEventArgs e = new ZCUStatusEventArgs();
                            e.ZCUID = _ZCU.Id;
                            e.Status = "Disconnected";
                            statusChangeEvent?.Invoke(this, e);
                        }
                        foreach (ZONE zone in StaticPool.zoneCollection)
                        {
                            if(zone.ZCUId == this.id)
                            {
                                if(zone.Status != (int)EM_ZoneStatusType.DISCONNECT)
                                {
                                    zone.OldStatus = zone.Status;
                                    zone.Status = (int)EM_ZoneStatusType.DISCONNECT;
                                    ZoneEventArgs e = new ZoneEventArgs();
                                    e.ZoneID = zone.Id;
                                    e.ZoneStatus = EM_ZoneStatusType.DISCONNECT;
                                    zoneEvent?.Invoke(this, e);
                                }
                            }
                        }
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    _ZCU.IsConnect = false;
                }
                finally
                {
                    Thread.Sleep(1000);
                }
            }
        }
        //Status: Disconnect, Occupied, UnOccupied, Order
        //OldStatus : Disconnect, Occupied, UnOccupied, Order
        //Event Status: Occupied, UnOccupied,Disconnect
        private void ExcecuteEventData(string[] message, int i)
        {
            string eventData = StaticPool.HexToBin(message[i], 8);
            string stx = eventData.Substring(0, 2);
            if (stx == CHECK_START_EVENT)
            {
                string status1 = eventData.Substring(2, 2);
                string status2 = eventData.Substring(4, 2);
                string status3 = eventData.Substring(6, 2);
                string[] statuses = new string[3] { status1, status2, status3 };
                for (int statusIndex = 0; statusIndex < statuses.Length; statusIndex++)
                {
                    foreach (ZONE zone in StaticPool.zoneCollection)
                    {
                        if (zone.ZCUId == this.id && zone.ZcuIndex == (3 * (i - 1)) + (statusIndex + 1))
                        {
                            ZoneEventArgs e = new ZoneEventArgs();
                            e.ZoneID = zone.Id;
                            e.ZoneStatus = ZoneStatus.GetZoneStatus(statuses[statusIndex]);
                            if (zone.Status != (int)e.ZoneStatus)
                            {
                                //Order
                                if (zone.Status == (int)EM_ZoneStatusType.ORDER)
                                {
                                    if (e.ZoneStatus != EM_ZoneStatusType.UN_OCCUPIED)
                                    {
                                        zone.OldStatus = zone.Status;
                                        zone.Status = (int)e.ZoneStatus;
                                        if (e.ZoneStatus == EM_ZoneStatusType.OCCUPIED)
                                        {
                                            #region:Check order plate number
                                            #endregion
                                        }
                                        this.zoneEvent?.Invoke(this, e);
                                    }
                                }
                                else if (zone.OldStatus == (int)EM_ZoneStatusType.ORDER)
                                {
                                    if (zone.Status == (int)EM_ZoneStatusType.DISCONNECT)
                                    {
                                        if (e.ZoneStatus == EM_ZoneStatusType.OCCUPIED)
                                        {
                                            zone.Status = (int)e.ZoneStatus;
                                            this.zoneEvent?.Invoke(this, e);
                                        }
                                    }
                                    else if (zone.Status == (int)EM_ZoneStatusType.OCCUPIED)
                                    {
                                        zone.OldStatus = zone.Status;
                                        zone.Status = (int)e.ZoneStatus;
                                        this.zoneEvent?.Invoke(this, e);
                                    }
                                }
                                //Normal
                                else
                                {
                                    if (zone.Status != (int)EM_ZoneStatusType.DISCONNECT)
                                    {
                                        zone.OldStatus = zone.Status;
                                    }
                                    zone.Status = (int)e.ZoneStatus;
                                    this.zoneEvent?.Invoke(this, e);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void GetZoneDetail(int zoneIndex, ref string plateNum, ref string imagePath)
        {
            try
            {
                string GetDetailCMD = CameraCMD.GetZoneDetail(this.IpAddress, zoneIndex);
                //"No valid"
                //$"ImagePath{imagePath},{currentStatus},{currentPlateNum}"
                string response = TCPTools.ExecuteCommand_Ascii(this.IpAddress, this.port, GetDetailCMD, delay_time);

                if (response != null)
                {
                    if (response.Contains("ImagePath"))
                    {
                        imagePath = response.Substring(9, response.IndexOf(",") - 9);
                        string status = response.Substring(response.IndexOf(",") + 1);
                        plateNum = status.Substring(status.IndexOf(",") + 1);
                    }
                }
                else
                {
                    //Log
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Thread.Sleep(500);
            }
        }
    }
}
