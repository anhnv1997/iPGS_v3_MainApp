using iParking.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblOutput
    {
        public static string TBL_COL_NAME = "Name";
        public static string TBL_NAME = "tblOutput";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_CODE = "Code";
        public static string TBL_COL_DESCRIPTION = "Description";
        public static string TBL_COL_IP = "IP";
        public static string TBL_COL_Port = "Port";
        public static string TBL_COL_TYPE = "Type";

        public static string GetCMD = $@"Select {TBL_COL_ID},{TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_NAME},{TBL_COL_IP},{TBL_COL_Port},{TBL_COL_TYPE} 
                                                        from dbo.{TBL_NAME} order by {TBL_COL_SORT}";

        public string ID;
        public int Soft { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }

        public static OutputCollection LoadDataOutput(OutputCollection outputCollection)
        {
            DataTable dtOutput = StaticPool.mdb.FillData(GetCMD);
            outputCollection.Clear();
            if (dtOutput != null)
            {
                if (dtOutput.Rows.Count > 0)
                {
                    foreach (DataRow row in dtOutput.Rows)
                    {
                        Output output = new Output(Convert.ToInt32(row[TBL_COL_TYPE].ToString()));
                        output.ID = row[TBL_COL_ID].ToString();
                        output.Code = row[TBL_COL_CODE].ToString();
                        output.Description = row[TBL_COL_DESCRIPTION].ToString();
                        output.Name = row[TBL_COL_NAME].ToString();
                        output.IPAddress = row[TBL_COL_IP].ToString();
                        output.Port = Convert.ToInt32(row[TBL_COL_Port].ToString());
                        output.OutputType = Convert.ToInt32(row[TBL_COL_TYPE].ToString());
                        outputCollection.Add(output);
                    }
                    dtOutput.Dispose();
                    DataTable dtSlotCount = StaticPool.mdb.FillData($@"SELECT COUNT({tblOutputDetail.TBL_COL_ZONEID}) as SlotCount,{tblOutputDetail.TBL_COL_OUTPUTID},{tblOutputDetail.TBL_COL_RELAY_INDEX}
                                                               FROM {tblOutputDetail.TBL_NAME}
                                                               Group by {tblOutputDetail.TBL_COL_OUTPUTID},{tblOutputDetail.TBL_COL_RELAY_INDEX} ");
                    if (dtSlotCount != null)
                    {
                        if (dtSlotCount.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtSlotCount.Rows)
                            {
                                string outputID = row[tblOutputDetail.TBL_COL_OUTPUTID].ToString();
                                Output output = outputCollection.GetOutput(outputID);
                                if (output != null)
                                {
                                    output.SlotCounts[Convert.ToInt32(row["RelayIndex"].ToString())] = Convert.ToInt32(row["SlotCount"].ToString());
                                }

                            }
                        }
                    }
                    return outputCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string code, string description, string outputName, string ip, int port, int outputType)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_CODE},{TBL_COL_DESCRIPTION},{TBL_COL_NAME},{TBL_COL_IP},{TBL_COL_Port},{TBL_COL_TYPE})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  values(N'{code}',N'{description}',N'{outputName}','{ip}',{port},{outputType})
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
        public static bool Modify(string id, string code, string description, string outputName, string ip, int port, int outputType)
        {
            string modifyCMD = $@"update {TBL_NAME} 
                                  set {TBL_COL_CODE}=N'{code}',
                                      {TBL_COL_DESCRIPTION}=N'{description}',
                                      {TBL_COL_NAME}=N'{outputName}',
                                      {TBL_COL_IP} ='{ip}',
                                      {TBL_COL_Port} = {port},
                                      {TBL_COL_TYPE} = {outputType}
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
        public static bool Delete(string outputID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ID}='{outputID}'";
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
