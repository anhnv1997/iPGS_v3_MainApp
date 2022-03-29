using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iParking.Databases
{
    public class tblTMA_Server
    {
        public static string TBL_NAME = "tblTMA_Server";
        public static string TBL_COL_SORT = "Sort";
        public static string TBL_COL_NAME = "Name";
        public static string TBL_COL_ID = "ID";
        public static string TBL_COL_CODE = "Code";
        public static string TBL_COL_DESCRIPTION = "Description";
        public static string TBL_COL_IP = "IP";
        public static string TBL_COL_PORT = "Port";
        public static string TBL_COL_USERNAME = "_Username";
        public static string TBL_COL_PASSWORD = "_Password";
        public static string GetCMD = @$"Select {TBL_COL_ID},{TBL_COL_CODE},{TBL_COL_DESCRIPTION}, 
                                                                {TBL_COL_IP},{TBL_COL_PORT},{TBL_COL_NAME},
                                                                {TBL_COL_USERNAME},{TBL_COL_PASSWORD}
                                                        from dbo.{TBL_NAME} 
                                                        order by {TBL_COL_SORT}";

        public string ID { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }
        public string _Username { get; set; }
        public string _Password { get; set; }

        //Get
        public static TMA_ServerCollection LoadDataTMA(TMA_ServerCollection _TMA_ServerCollection)
        {
            
            DataTable dtTMA_ServerCollection = StaticPool.mdb.FillData(GetCMD);
            _TMA_ServerCollection.Clear();
            if (dtTMA_ServerCollection != null)
            {
                if (dtTMA_ServerCollection.Rows.Count > 0)
                {
                    foreach (DataRow row in dtTMA_ServerCollection.Rows)
                    {
                        TMA_Server _TMA_Server = new TMA_Server();
                        _TMA_Server.Id = row[TBL_COL_ID].ToString();
                        _TMA_Server.Code = row[TBL_COL_CODE].ToString();
                        _TMA_Server.Description = row[TBL_COL_DESCRIPTION].ToString();
                        _TMA_Server.Ip = row[TBL_COL_IP].ToString();
                        _TMA_Server.Port = Convert.ToInt32(row[TBL_COL_PORT].ToString());
                        _TMA_Server.Name = row[TBL_COL_NAME].ToString();
                        _TMA_Server.Username = row[TBL_COL_USERNAME].ToString();
                        _TMA_Server.Password = row[TBL_COL_PASSWORD].ToString();
                        _TMA_ServerCollection.Add(_TMA_Server);
                    }
                    dtTMA_ServerCollection.Dispose();
                    return _TMA_ServerCollection;
                }
            }
            return null;
        }
        //Add
        public static string InsertAndGetLastID(string code, string description, string ipAddr, int port, string name, string username, string password)
        {
            string insertCMD = $@"DECLARE @generated_keys table([{TBL_COL_ID}] varchar(150))
                                  Insert into {TBL_NAME}({TBL_COL_CODE},{TBL_COL_DESCRIPTION},
                                                   {TBL_COL_IP},{TBL_COL_PORT},
                                                   {TBL_COL_NAME},{TBL_COL_USERNAME},{TBL_COL_PASSWORD})
                                  OUTPUT inserted.{TBL_COL_ID} 
                                  Into @generated_keys
                                  values(N'{code}',N'{description}','{ipAddr}',{port},
                                   N'{name}','{username}','{password}')
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
        public static bool Modify(string id, string code, string description, string ipAddr, int port, string name, string username, string password)
        {
            string cmd = @$"update {TBL_NAME} 
                            set {TBL_COL_CODE}=N'{code}',
                                {TBL_COL_DESCRIPTION}=N'{description}',
                                {TBL_COL_IP}='{ipAddr}',
                                {TBL_COL_PORT}={port},
                                {TBL_COL_NAME}=N'{name}',
                                {TBL_COL_USERNAME}='{username}',
                                {TBL_COL_PASSWORD}='{password}'
                            Where {TBL_COL_ID} = '{id}'";
            if (!StaticPool.mdb.ExecuteCommand(cmd))
            {
                if (!StaticPool.mdb.ExecuteCommand(cmd))
                {
                    return false;
                }
            }
            return true;
        }
        //Delete
        public static bool Delete(string tmaID)
        {
            string deleteCMD = $"Delete {TBL_NAME} where {TBL_COL_ID} = '{tmaID}'";
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
