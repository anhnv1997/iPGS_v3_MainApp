using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblOutputDetail
    {
        public static string TBL_NAME = "tblOutputDetail";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_ZONEID = "ZoneID";
        public static string TBL_COL_OUTPUTID = "OutputID";
        public static string TBL_COL_RELAY_INDEX = "RelayIndex";
        public static string GetCMD = $"Select {TBL_COL_ID},{TBL_COL_OUTPUTID},{TBL_COL_ZONEID},{TBL_COL_RELAY_INDEX} from dbo.{TBL_NAME} order by {TBL_COL_ID}";

        public string ID { get; set; }
        public string ZoneID { get; set; }
        public string OutputID { get; set; }
        public int RelayIndex { get; set; }

        //Get
        public static OutputDetailCollection LoadOutputDetail(OutputDetailCollection outputDetailCollection)
        {
            
            DataTable dtOutputDetail = StaticPool.mdb.FillData(GetCMD);
            outputDetailCollection.Clear();
            if (dtOutputDetail != null)
            {
                if (dtOutputDetail.Rows.Count > 0)
                {

                    foreach (DataRow row in dtOutputDetail.Rows)
                    {
                        OutputDetail outputDetail = new OutputDetail();
                        outputDetail.ID = row[TBL_COL_ID].ToString();
                        outputDetail.OutputID = row[TBL_COL_OUTPUTID].ToString();
                        outputDetail.ZoneID = row[TBL_COL_ZONEID].ToString();
                        outputDetail.RelayIndex = Convert.ToInt32(row[TBL_COL_RELAY_INDEX].ToString());
                        outputDetailCollection.Add(outputDetail);
                    }
                    dtOutputDetail.Dispose();
                    return outputDetailCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string outputID, string zoneID, int relayIndex)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_OUTPUTID},{TBL_COL_ZONEID},{TBL_COL_RELAY_INDEX})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  Values('{outputID}','{zoneID}',{relayIndex})
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
        public static bool Modify(string outputID, string zoneID, int relayIndex)
        {
            string modifyCMD = @$"update {TBL_NAME} 
                                  set {TBL_COL_OUTPUTID} = '{outputID}',
                                      {TBL_COL_ZONEID} = '{zoneID}',
                                      {TBL_COL_RELAY_INDEX} = {relayIndex}
                                  where {TBL_COL_OUTPUTID} = '{outputID}' AND {TBL_COL_ZONEID} = '{zoneID}'";
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
        public static bool Delete(string zoneID, string outputID, int relayIndex)
        {

            string deleteCMD = $@"Delete {TBL_NAME} 
                                  where {TBL_COL_ZONEID} = '{zoneID}' AND {TBL_COL_OUTPUTID}='{outputID}' AND {TBL_COL_RELAY_INDEX}={relayIndex}";
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
            string deleteCMD = $"delete {TBL_NAME} where {TBL_NAME}.{TBL_COL_ZONEID} = '{zoneID}'";
            if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
            {
                if (!StaticPool.mdb.ExecuteCommand(deleteCMD))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool DeleteByOutputID(string outputID)
        {
            string deleteCMD = $"delete {TBL_NAME} where {TBL_NAME}.{TBL_COL_OUTPUTID} = '{outputID}'";
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