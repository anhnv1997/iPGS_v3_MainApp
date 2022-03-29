using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblZone
    {
        public static string TBL_NAME = "tblZone";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_CODE = "Code";
        public static string TBL_COL_DESCRIPTION = "Description";
        public static string TBL_COL_ZCUID = "ZCUID";
        public static string TBL_COL_GROUP_ID = "GroupID";
        public static string TBL_COL_STATUS = "Status";
        public static string TBL_COL_IMG_SAVEPATH = "ImageSavePath";
        public static string TBL_COL_NAME = "Name";
        public static string TBL_COL_PLATENUM = "PlateNum";
        public static string TBL_COL_ZCU_INDEX = "zcuIndex";
        public static string TBL_COL_ORDER_PLATENUM = "OrderPlateNum";
        public static string TBL_COL_ISAUTO_ORDER = "isAutoOrder";
        public static string TBL_COL_ISUSEAI = "isUseAI";

        public static string GetCMD = @$"Select {TBL_COL_ID},{TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_ZCUID}, 
                                                               {TBL_COL_GROUP_ID},{TBL_COL_STATUS},{TBL_COL_IMG_SAVEPATH},
                                                               {TBL_COL_PLATENUM},{TBL_COL_NAME},{TBL_COL_ZCU_INDEX},
                                                               {TBL_COL_ORDER_PLATENUM},{TBL_COL_ISAUTO_ORDER},{TBL_COL_ISUSEAI}
                                                          from dbo.{TBL_NAME}
                                                          order by {TBL_COL_SORT}";

        //Get
        public static ZONECollection LoadDataZone(ZONECollection zoneCollection)
        {
            DataTable dtZone = StaticPool.mdb.FillData(GetCMD);
            zoneCollection.Clear();
            if (dtZone != null)
            {
                if (dtZone.Rows.Count > 0)
                {
                    foreach (DataRow row in dtZone.Rows)
                    {
                        ZONE zone = new ZONE();
                        zone.Id = row[TBL_COL_ID].ToString();
                        zone.Code = row[TBL_COL_CODE].ToString();
                        zone.Description = row[TBL_COL_DESCRIPTION].ToString();
                        zone.ZCUId = row[TBL_COL_ZCUID].ToString();
                        zone.ZoneGroupId = row[TBL_COL_GROUP_ID].ToString();
                        zone.Status = Convert.ToInt32(row[TBL_COL_STATUS].ToString());
                        zone.OldStatus = zone.Status;
                        zone.ImagePath = row[TBL_COL_IMG_SAVEPATH].ToString();
                        zone.PlateNum = row[TBL_COL_PLATENUM].ToString();
                        zone.zoneName = row[TBL_COL_NAME].ToString();
                        zone.ZcuIndex = Convert.ToInt32(row[TBL_COL_ZCU_INDEX].ToString());
                        zone.OrderPlateNumber = row[TBL_COL_ORDER_PLATENUM].ToString();
                        zone.isAutoOrder = Convert.ToBoolean(row[TBL_COL_ISAUTO_ORDER].ToString());
                        zone.isUseAI = Convert.ToBoolean(row[TBL_COL_ISUSEAI].ToString());
                        zoneCollection.Add(zone);
                    }
                    dtZone.Dispose();
                    return zoneCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string code, string description, string zcuID, string zoneGroupID, int status, string imagePath, string plateNum, string zoneName, int zcuIndex, string orderPlateNumber, int isAutoOrder)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_ZCUID},
                                                         {TBL_COL_GROUP_ID},{TBL_COL_STATUS},{TBL_COL_IMG_SAVEPATH},
                                                         {TBL_COL_PLATENUM},{TBL_COL_NAME},{TBL_COL_ZCU_INDEX},
                                                         {TBL_COL_ORDER_PLATENUM},{TBL_COL_ISAUTO_ORDER})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  values(N'{code}',N'{description}','{zcuID}','{zoneGroupID}',{status},
                                         N'{imagePath}','{plateNum}',N'{zoneName}',{zcuIndex},
                                          '{orderPlateNumber}',{isAutoOrder})
                                  Select * from @generated_keys";
            DataTable dtbLastID = StaticPool.mdb.FillData(insertCMD);
            if ((dtbLastID == null))
            {
                dtbLastID = StaticPool.mdb.FillData(insertCMD);
                if ((dtbLastID == null))
                {
                    return "";
                }
            }
            UpdateTblCompatision();
            return dtbLastID.Rows[0][TBL_COL_ID].ToString();
        }

        //Modify
        public static bool Modify(string id, string code, string description, string zcuID, string zoneGroupID, int status, string imagePath, string plateNum, string zoneName, int zcuIndex, string orderPlateNum, int isAutoOrder)
        {
            string modifyCMD = $@"update {TBL_NAME} 
                                  set {TBL_COL_CODE} = N'{code}',
                                      {TBL_COL_DESCRIPTION} = N'{description}',
                                      {TBL_COL_ZCUID} = '{zcuID}',
                                      {TBL_COL_GROUP_ID} = '{zoneGroupID}',
                                      {TBL_COL_STATUS} = {status},
                                      {TBL_COL_IMG_SAVEPATH}=N'{imagePath}',
                                      {TBL_COL_PLATENUM}='{plateNum}',
                                      {TBL_COL_NAME}=N'{zoneName}',
                                      {TBL_COL_ZCU_INDEX}={zcuIndex},
                                      {TBL_COL_ORDER_PLATENUM}='{orderPlateNum}',
                                      {TBL_COL_ISAUTO_ORDER} = {isAutoOrder}
                                  Where {TBL_COL_ID} = '{id}'";
            if (!StaticPool.mdb.ExecuteCommand(modifyCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(modifyCMD))
                {
                    return false;
                }
            }
            return true;
        }

        //Delete
        public static bool Delete(string zoneID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ID} = '{zoneID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            UpdateTblCompatision();
            return true;
        }

        private static void UpdateTblCompatision()
        {
            //Update bảng so sánh vs parking
            //Get CurrentOccupiedSlot
            int currentOccupiedPgsNumber = 0;
            currentOccupiedPgsNumber = GetCurrentOccupiedSlot(currentOccupiedPgsNumber);
            int totalSlot = 0;
            totalSlot = GetTotalSlot(totalSlot);
            //Get Last Compare Event
            int lastEvent_ParkingNumber = 0;
            lastEvent_ParkingNumber = GetLastEventParkingNumber(lastEvent_ParkingNumber);
            int different = lastEvent_ParkingNumber - currentOccupiedPgsNumber;
            int eventCOde = 0;
            if (different == 0)
            {
                if (lastEvent_ParkingNumber == totalSlot)
                {
                    eventCOde = 4;
                }
                else
                {
                    eventCOde = 0;
                }
            }
            else if (different > 0)
            {
                eventCOde = 1;
            }
            else if (different < 0)
            {
                if (lastEvent_ParkingNumber <= totalSlot)
                    eventCOde = 2;
                else
                    eventCOde = 3;
            }

            string insertCMD = $@"insert into [tblParkingComparisons]
                                                        ([EventCode],[CreatedDate],[ParkingNumber],[PGSNumber],[Different],[ZoneImage1],[ZoneImage2],[PGS_Total_Number])
                                             values({eventCOde},GETDATE(),{lastEvent_ParkingNumber},{currentOccupiedPgsNumber},{different},'','',{totalSlot})";
            StaticPool.mdb.ExecuteCommand(insertCMD);
        }

        private static int GetLastEventParkingNumber(int lastEvent_ParkingNumber)
        {
            DataTable dtLastCompareEvent = StaticPool.mdb.FillData("SELECT TOP (1) [ParkingNumber] from [tblParkingComparisons] order by CreatedDate desc");
            if (dtLastCompareEvent != null)
            {
                if (dtLastCompareEvent.Rows.Count > 0)
                {
                    lastEvent_ParkingNumber = Convert.ToInt32(dtLastCompareEvent.Rows[0]["ParkingNumber"].ToString());
                }
            }

            return lastEvent_ParkingNumber;
        }

        private static int GetTotalSlot(int totalSlot)
        {
            string cmdGetTotalCount = $"SELECT COUNT(ID) as Count FROM tblZone";
            DataTable dtTotalSlots = StaticPool.mdb.FillData(cmdGetTotalCount);
            if (dtTotalSlots != null)
            {
                totalSlot = Convert.ToInt32(dtTotalSlots.Rows[0]["Count"].ToString());
            }

            return totalSlot;
        }

        private static int GetCurrentOccupiedSlot(int currentOccupiedPgsNumber)
        {
            string cmd = $"SELECT COUNT(ID) as Count FROM tblZone WHERE Status = 1";
            DataTable dtSlots = StaticPool.mdb.FillData(cmd);
            if (dtSlots != null)
            {
                if (dtSlots.Rows.Count > 0)
                {
                    currentOccupiedPgsNumber = Convert.ToInt32(dtSlots.Rows[0]["Count"].ToString());
                }
            }

            return currentOccupiedPgsNumber;
        }

        //Update zone status
        public static bool UpdateZoneStatus(int status, string plateNum, string imagePath, string zoneID)
        {
            string updateStatusCMD = $@"update {TBL_NAME}
                                        set {TBL_COL_STATUS}={status},
                                            {TBL_COL_PLATENUM}='{plateNum}',
                                            {TBL_COL_IMG_SAVEPATH}='{imagePath}'
                                        where {TBL_COL_ID}='{zoneID}'";
            if (!StaticPool.mdb.ExecuteCommand(updateStatusCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(updateStatusCMD))
                {
                    return false;
                }
            }
            //Update bảng so sánh vs parking
            //Get CurrentOccupiedSlot
            int currentOccupiedPgsNumber = 0;
            currentOccupiedPgsNumber = GetCurrentOccupiedSlot(currentOccupiedPgsNumber);
            int totalSlot = 0;
            totalSlot = GetTotalSlot(totalSlot);
            //Get Last Compare Event
            int lastEvent_ParkingNumber = 0;
            int lastEvent_PGSNumber = 0;
            DataTable dtLastCompareEvent = StaticPool.mdb.FillData("SELECT TOP (1) [PGSNumber],[ParkingNumber] from [tblParkingComparisons] order by CreatedDate desc");
            if (dtLastCompareEvent != null)
            {
                if (dtLastCompareEvent.Rows.Count > 0)
                {
                    lastEvent_ParkingNumber = Convert.ToInt32(dtLastCompareEvent.Rows[0]["ParkingNumber"].ToString());
                    lastEvent_PGSNumber = Convert.ToInt32(dtLastCompareEvent.Rows[0]["PGSNumber"].ToString());
                }
            }
            if (lastEvent_PGSNumber == currentOccupiedPgsNumber)
            {
                return true;
            }
            int different = lastEvent_ParkingNumber - currentOccupiedPgsNumber;
            int eventCOde = 0;
            if (different == 0)
            {
                if (lastEvent_ParkingNumber == totalSlot)
                {
                    eventCOde = 4;
                }
                else
                {
                    eventCOde = 0;
                }
            }
            else if (different > 0)
            {
                eventCOde = 1;
            }
            else if (different < 0)
            {
                if (lastEvent_ParkingNumber <= totalSlot)
                    eventCOde = 2;
                else
                    eventCOde = 3;
            }

            string insertCMD = $@"insert into [tblParkingComparisons]
                                                        ([EventCode],[CreatedDate],[ParkingNumber],[PGSNumber],[Different],[ZoneImage1],[ZoneImage2],[PGS_Total_Number])
                                             values({eventCOde},GETDATE(),{lastEvent_ParkingNumber},{currentOccupiedPgsNumber},{different},'','',{totalSlot})";
            StaticPool.mdb.ExecuteCommand(insertCMD);
            return true;
        }

        public static bool UpdateOrderStatus(int isAutoOrder, string groupID)
        {
            string updateCMd = $@"Update {TBL_NAME} set {TBL_COL_ISAUTO_ORDER}={isAutoOrder} where {TBL_COL_GROUP_ID}='{groupID}'";
            if (!StaticPool.mdb.ExecuteCommand(updateCMd))
            {
                if (!StaticPool.mdb.ExecuteCommand(updateCMd))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
