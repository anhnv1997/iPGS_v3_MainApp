using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblVehicleZoneDetail
    {
        public static string TBL_NAME = "tblVehicleZoneDetail";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_ZONEID = "ZoneID";
        public static string TBL_COL_VEHICLE_ID = "VehicleID";
        public static string TBL_COL_PRIORITY = "Priority";

        public static string GetCMD = $@"SELECT {TBL_COL_ID},{TBL_COL_ZONEID},{TBL_COL_VEHICLE_ID},{TBL_COL_PRIORITY}
                                         FROM {TBL_NAME}
                                         ORDER BY {TBL_COL_ID} ";
        //Get
        public static VehicleZoneDetailCollection LoadVehicleZoneDetail(VehicleZoneDetailCollection vehicleZoneDetailCollection)
        {
            DataTable dtVehicleZoneDetailCollection = StaticPool.mdb.FillData(GetCMD);
            vehicleZoneDetailCollection.Clear();
            if (dtVehicleZoneDetailCollection != null)
            {
                if (dtVehicleZoneDetailCollection.Rows.Count > 0)
                {
                    foreach (DataRow row in dtVehicleZoneDetailCollection.Rows)
                    {
                        VehicleZoneDetail vehicleZoneDetail = new VehicleZoneDetail();
                        vehicleZoneDetail.ID = row[TBL_COL_ID].ToString();
                        vehicleZoneDetail.ZoneID = row[TBL_COL_ZONEID].ToString();
                        vehicleZoneDetail.VehicleTypeID = row[TBL_COL_VEHICLE_ID].ToString();
                        vehicleZoneDetail.PiorityLevel = Convert.ToInt32(row[TBL_COL_PRIORITY].ToString());
                        vehicleZoneDetailCollection.Add(vehicleZoneDetail);
                    }
                    dtVehicleZoneDetailCollection.Dispose();
                    return vehicleZoneDetailCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string zoneID, string vehicleID, int priorityLevel)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_ZONEID},{TBL_COL_VEHICLE_ID},{TBL_COL_PRIORITY})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  values(N'{zoneID}',N'{vehicleID}',{priorityLevel})
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
            return dtbLastID.Rows[0][TBL_COL_ID].ToString();
        }
        //Modify
        public static bool Modify(string id, string zoneID, string vehicleID, int priorityLevel)
        {
            string modifyCMD = $@"update {TBL_NAME} 
                                  set {TBL_COL_ZONEID}=N'{zoneID}',
                                      {TBL_COL_VEHICLE_ID}=N'{vehicleID}',
                                      {TBL_COL_PRIORITY}={priorityLevel} 
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
        public static bool Delete(string vehicleZoneDetailID)
        {
            if (!StaticPool.mdb.ExecuteCommand($"Delete {TBL_NAME} where {TBL_COL_ID}='{vehicleZoneDetailID}'"))
            {
                if (!StaticPool.mdb.ExecuteCommand($"Delete {TBL_NAME} where {TBL_COL_ID}='{vehicleZoneDetailID}'"))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Delete(string zoneID, string vehicleID)
        {
            if (!StaticPool.mdb.ExecuteCommand($"Delete {TBL_NAME} where {TBL_COL_ZONEID}='{zoneID}' AND {TBL_COL_VEHICLE_ID}='{vehicleID}'"))
            {
                if (!StaticPool.mdb.ExecuteCommand($"Delete {TBL_NAME} where {TBL_COL_ZONEID}='{zoneID}' AND {TBL_COL_VEHICLE_ID} = '{vehicleID}'"))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
