using iParking.Enums;
using iParking.Events;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iParking.Object
{
    [Microsoft.AspNetCore.Mvc.Route("/")]
    [ApiController]
    public class OrderZoneController : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.Route("hello")]
        [HttpGet]
        public IActionResult Get()
        {
            var greeting = "Hello ";
            return new JsonResult(greeting);
        }


        [Route("GetZonesByVehicle/{vehicleID}")]
        [HttpGet]
        public async Task<IActionResult> GetZoneList(string vehicleID)
        {
            await Task.CompletedTask;
            List<ZoneInfo> zones = new List<ZoneInfo>();
            var vehicleZoneDetails = (from VehicleZoneDetail vehicleDetail in StaticPool.vehicleZoneDetails where vehicleDetail.VehicleTypeID == vehicleID select vehicleDetail);
            foreach (VehicleZoneDetail vehicleZoneDetail in vehicleZoneDetails)
            {
                ZONE zone = StaticPool.zoneCollection.GetZONE(vehicleZoneDetail.ZoneID);
                if (zone != null)
                {
                    if (zone.Status != -1)
                    {
                        ZoneInfo zoneInfo = new ZoneInfo()
                        {
                            ID = zone.Id,
                            Name = zone.Code
                        };
                        zones.Add(zoneInfo);
                    }
                }
            }
            return new JsonResult(zones);
        }




        [Route("GetAllZones")]
        [HttpGet]
        public async Task<IActionResult> GetAllZoneList()
        {
            await Task.CompletedTask;
            List<ZoneInfo> zones = new List<ZoneInfo>();
            foreach (ZONE zone in StaticPool.zoneCollection)
            {
                if (zone.Status != -1)
                {
                    ZoneInfo zoneInfo = new ZoneInfo()
                    {
                        ID = zone.Id,
                        Name = zone.Code,
                        Status = zone.Status

                    };
                    zones.Add(zoneInfo);
                }
            }
            return new JsonResult(zones);
        }



        public class ZoneInfo
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public int Status { get; set; }
        }

        [Route("OrderZone")]
        [HttpPost]
        public async Task<IActionResult> OrderZone(OrderRequest orderRequest)
        {
            await Task.CompletedTask;
            //Auto Order
            if (orderRequest.ZoneID == "")
            {
                var vehicleZoneDetails = (from VehicleZoneDetail vehicleDetail in StaticPool.vehicleZoneDetails where vehicleDetail.VehicleTypeID == orderRequest.VehicleTypeID select vehicleDetail);
                List<VehicleZoneDetail> SortedList = vehicleZoneDetails.OrderBy(o => o.PiorityLevel).ToList();

                foreach (VehicleZoneDetail vehicleZoneDetail in SortedList)
                {
                    ZONE zone = StaticPool.zoneCollection.GetZONE(vehicleZoneDetail.ZoneID);
                    if (zone != null)
                    {
                        if (zone.Status != -1 && zone.isAutoOrder)
                        {
                            if (zone.Status == (int)EM_ZoneStatusType.UN_OCCUPIED || (zone.OldStatus == (int)EM_ZoneStatusType.UN_OCCUPIED && zone.Status == (int)EM_ZoneStatusType.DISCONNECT))
                            {
                                ZoneEventArgs e = new ZoneEventArgs();
                                e.ZoneID = zone.Id;
                                e.ZoneStatus = EM_ZoneStatusType.ORDER;
                                orderRequest.ZoneID = zone.Id;
                                if (zone.Status != (int)EM_ZoneStatusType.DISCONNECT)
                                {
                                    zone.OldStatus = zone.Status;
                                }
                                zone.Status = (int)EM_ZoneStatusType.ORDER;
                                zone.OrderPlateNumber = orderRequest.PlateNumber;
                                //Form1.UpdateLedDisplay(e);
                                DisplayEvent(e);
                                orderRequest.ZoneID = zone.Id;
                                orderRequest.Result = true;

                                TCS_Infor tCS_Infor = new TCS_Infor()
                                {
                                    EventType = "IN_BARRIE",
                                    EventDate = DateTime.Now,
                                    SlotNumber = zone.Code,
                                    SlotName = zone.zoneName,
                                    VehicleNumber = zone.OrderPlateNumber
                                };
                                string result = SendAPI_TCS(tCS_Infor);
                                if (result != "")
                                {
                                    LogHelper.Logger_TCSError($@"Send API TCS: Plate Number: {zone.OrderPlateNumber} Error: {result}");
                                }
                                else
                                {
                                    LogHelper.Logger_TCSInfo($@"Send API TCS: Plate Number: {zone.OrderPlateNumber} Success");
                                }
                                return new JsonResult(orderRequest);
                            }
                        }
                    }
                }
                orderRequest.ZoneID = "";
                orderRequest.Result = false;
                return new JsonResult(orderRequest);
            }
            //Order bằng cơm
            else
            {
                ZONE zone = StaticPool.zoneCollection.GetZONE(orderRequest.ZoneID);
                if (zone != null)
                {
                    ZoneEventArgs e = new ZoneEventArgs();
                    e.ZoneID = zone.Id;
                    e.ZoneStatus = EM_ZoneStatusType.ORDER;
                    orderRequest.ZoneID = zone.Id;
                    if (zone.Status != (int)EM_ZoneStatusType.DISCONNECT)
                    {
                        zone.OldStatus = zone.Status;
                    }
                    zone.Status = (int)EM_ZoneStatusType.ORDER;
                    zone.OrderPlateNumber = orderRequest.PlateNumber;
                    //Form1.UpdateLedDisplay(e);
                    DisplayEvent(e);
                    orderRequest.ZoneID = zone.Id;
                    orderRequest.Result = true;

                    TCS_Infor tCS_Infor = new TCS_Infor()
                    {
                        EventType = "IN_BARRIE",
                        EventDate = DateTime.Now,
                        SlotNumber = zone.Code,
                        SlotName = zone.zoneName,
                        VehicleNumber = zone.OrderPlateNumber
                    };
                    string result = SendAPI_TCS(tCS_Infor);
                    if (result != "")
                    {
                        LogHelper.Logger_TCSError($@"Send API TCS: Plate Number: {zone.OrderPlateNumber} Error: {result}");
                    }
                    else
                    {
                        LogHelper.Logger_TCSInfo($@"Send API TCS: Plate Number: {zone.OrderPlateNumber} Success");
                    }

                    return new JsonResult(orderRequest);
                }
            }
            orderRequest.Result = false;
            return new JsonResult(orderRequest);
        }
        public string SendAPI_TCS(TCS_Infor tCS_Infor)
        {
            try
            {
                var client = new RestClient("http://192.168.14.82:8094/api/EventInfo");
                client.Timeout = 500;
                var request = new RestRequest(Method.POST);
                string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(tCS_Infor);
                request.AddJsonBody(jsonBody);
                var response = client.Execute(request);
                if (!response.IsSuccessful)
                {
                    client = new RestClient("http://192.168.14.82:8094/api/EventInfo");
                    client.Timeout = 500;
                    request = new RestRequest(Method.POST);
                    request.RequestFormat = DataFormat.Json;
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
        [Route("SetZoneStatus")]
        [HttpPost]
        public async Task<IActionResult> SetZoneStatus(ZoneStatus zoneStatus)
        {
            await Task.CompletedTask;
            if (zoneStatus.ID != "")
            {
                ZONE zone = StaticPool.zoneCollection.GetZONE(zoneStatus.ID);
                if (zone != null)
                {
                    if (zone.Status == -1)
                    {
                        zoneStatus.Result = false;
                        return new JsonResult(zoneStatus);
                    }
                    ZoneEventArgs e = new ZoneEventArgs();
                    e.ZoneID = zone.Id;
                    e.ZoneStatus = (EM_ZoneStatusType)zoneStatus.Status;
                    zone.OldStatus = zone.Status;
                    zone.Status = zoneStatus.Status;
                    //Form1.UpdateLedDisplay(e);
                    DisplayEvent(e);
                    zoneStatus.Result = true;

                    if(zone.Status == (int)EM_ZoneStatusType.UN_OCCUPIED)
                    {
                        TCS_Infor tCS_Infor = new TCS_Infor()
                        {
                            EventType = "OUT_SLOT",
                            EventDate = DateTime.Now,
                            SlotNumber = zone.Code,
                            SlotName = zone.zoneName,
                            VehicleNumber = zone.OrderPlateNumber
                        };
                        string result = SendAPI_TCS(tCS_Infor);
                        if (result != "")
                        {
                            LogHelper.Logger_TCSError($@"Send API TCS OUT_SLOT: Plate Number: {zone.OrderPlateNumber} Error: {result}");
                        }
                        else
                        {
                            LogHelper.Logger_TCSInfo($@"Send API TCS OUT_SLOT: Plate Number: {zone.OrderPlateNumber} Success");
                        }
                    }

                    return new JsonResult(zoneStatus);
                }
            }
            zoneStatus.Result = false;
            return new JsonResult(zoneStatus);
        }


        private static async void DisplayEvent(ZoneEventArgs e)
        {
            //Form1.UpdateKZIODisplay(e);
            Form1.SaveToDB(e);
            Form1.Update_OrderEvent_FormDisplay(e);
        }

        public class OrderRequest
        {
            public string VehicleTypeID { get; set; }
            public string PlateNumber { get; set; }
            public string ZoneID { get; set; }
            public bool Result { get; set; }
        }
        public class ZoneStatus
        {
            public string ID { get; set; }
            public int Status { get; set; }
            public bool Result { get; set; }
        }
    }
}
