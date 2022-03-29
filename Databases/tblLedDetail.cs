using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblLedDetail
    {
        public static string TBL_NAME = "tblLedDetail";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_ZONEID = "ZONEId";
        public static string TBL_COL_LedID = "LEDId";
           
        public static string GetCMD = $"Select {TBL_COL_ID},{TBL_COL_LedID},{TBL_COL_ZONEID} from dbo.{TBL_NAME} order by {TBL_COL_ID}";

        public string Id { get; set; }
        public string LedID { get; set; }
        public string ZoneID { get; set; }

        //Get
        public static LedDetailCollection LoadLedDetail(LedDetailCollection ledDetailCollection)
        {
            DataTable dtLedDetailCollection = StaticPool.mdb.FillData(GetCMD);
            ledDetailCollection.Clear();
            if (dtLedDetailCollection != null)
            {
                if (dtLedDetailCollection.Rows.Count > 0)
                {
                    foreach (DataRow row in dtLedDetailCollection.Rows)
                    {
                        LedDetail ledDetail = new LedDetail();
                        ledDetail.ID = row[TBL_COL_ID].ToString();
                        ledDetail.LedID = row[TBL_COL_LedID].ToString();
                        ledDetail.ZoneID = row[TBL_COL_ZONEID].ToString();
                        ledDetailCollection.Add(ledDetail);
                    }
                    dtLedDetailCollection.Dispose();
                    return ledDetailCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string ledID, string zoneID)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_LedID},{TBL_COL_ZONEID})
                                  OUTPUT inserted.{TBL_COL_ID}
                                  Into @generated_keys
                                  Values('{ledID}','{zoneID}')
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
        public static bool Modify(string ledID, string zoneID)
        {
            string modifyCMD = @$"update {TBL_NAME} 
                                  set {TBL_COL_LedID} = '{ledID}',
                                      {TBL_COL_ZONEID} = '{zoneID}',
                                  where {TBL_COL_LedID} = '{ledID}' AND {TBL_COL_ZONEID} = '{zoneID}'";
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
        public static bool Delete(string zoneID, string ledID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ZONEID} = '{zoneID}' AND {TBL_COL_LedID}='{ledID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DeleteByZoneID(string zoneID)
        {
            string deleteCMD = $"delete {tblLedDetail.TBL_NAME} where {tblLedDetail.TBL_NAME}.{tblLedDetail.TBL_COL_ZONEID} = '{zoneID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DeleteByLedID(string ledID)
        {
            string deleteCMD = $"delete {tblLedDetail.TBL_NAME} where {tblLedDetail.TBL_NAME}.{tblLedDetail.TBL_COL_LedID} = '{ledID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
    }
}