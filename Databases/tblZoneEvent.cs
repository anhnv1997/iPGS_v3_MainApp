using iParking.Enums;
using iParking.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblZoneEvent
    {
        public static event ChangeLedDisplayEvent changeLedDisplayEvent;
        public static string TBL_NAME = "tblZoneEvent";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_DATE = "Date";
        public static string TBL_COL_ZONEID = "ZoneID";
        public static string TBL_COL_ZCUID = "ZCUID";
        public static string TBL_COL_OLD_STATUS = "OldStatus";
        public static string TBL_COL_STATUS = "Status";
        public static string TBL_COL_PLATENUM = "PlateNum";
        public static string TBL_COL_IMAGEPATH = "ImagePath";
        //Add
        public static bool Insert(ZONE zone, DateTime eventDateTime)
        {
            string insertCMD = "";
            if ((zone.Status == (int)EM_ZoneStatusType.ORDER))
            {
                insertCMD = $@"Insert into {TBL_NAME}({TBL_COL_DATE},{TBL_COL_ZONEID},{TBL_COL_ZCUID},{TBL_COL_OLD_STATUS},{TBL_COL_STATUS},{TBL_COL_PLATENUM},{TBL_COL_IMAGEPATH})
                                  values('{eventDateTime.ToString("yyyy/MM/dd HH:mm:ss")}','{zone.Id}','{zone.ZCUId}',{zone.OldStatus},{zone.Status},'{zone.OrderPlateNumber}',N'{zone.ImagePath}')";

            }
            else
            {
                insertCMD = $@"Insert into {TBL_NAME}({TBL_COL_DATE},{TBL_COL_ZONEID},{TBL_COL_ZCUID},{TBL_COL_OLD_STATUS},{TBL_COL_STATUS},{TBL_COL_PLATENUM},{TBL_COL_IMAGEPATH})
                                  values('{eventDateTime.ToString("yyyy/MM/dd HH:mm:ss")}','{zone.Id}','{zone.ZCUId}',{zone.OldStatus},{zone.Status},'{zone.PlateNum}',N'{zone.ImagePath}')";
            }

            if (!StaticPool.mdb.ExecuteCommand(insertCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(insertCMD))
                {
                    return false;
                }
            }
            if ((zone.Status == (int)EM_ZoneStatusType.ORDER))
            {
                ChangeLedDisplayEventArgs e = new ChangeLedDisplayEventArgs()
                {
                    ZoneName = zone.Code,
                    PlateNumber = zone.OrderPlateNumber
                };
                changeLedDisplayEvent?.Invoke(null, e);
            }

            return true;
        }
    }
}
