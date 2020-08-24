using System;
using log4net;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GTF_SQLiteDB
{
    public class GTF_SQLiteDBManager
    {
        SQLiteConnection dbCon = null;
        SQLiteConnection m_ObjCon
        {
            get
            {
                if (dbCon == null && !string.Empty.Equals(m_Provider))
                {
                    dbCon = new SQLiteConnection();
                    dbCon.ConnectionString = m_Provider;
                    dbCon.SetPassword(m_DBPassword);

                }
                return dbCon;
            }
            set
            {
                dbCon = value;
            }
        }
        Boolean m_dbOpen = false;
        ILog m_logger = null;

        private string m_DBPassword = "gtfstfm12345";
        private static GTF_SQLiteDBManager instance;

        private string m_Provider = string.Empty;

        private static object sync_LocalDB = new Object();
        public static GTF_SQLiteDBManager Instance(ILog logger = null)
        {
            lock (sync_LocalDB)
            {
                if (instance == null)
                {
                    instance = new GTF_SQLiteDBManager(logger);

                }
                return instance;
            }
        }

        //생성자 private 처리
        private GTF_SQLiteDBManager(ILog logger = null)
        {
            m_logger = logger;
        }

        //SQLite DB가 있는지 없는지 검사하기
        public Boolean SQLite_Exists(string strFilePath)
        {
            Boolean DB_Flag = false;
            try
            {
                System.IO.FileInfo dbFile = new System.IO.FileInfo(strFilePath);
                if (dbFile.Exists) DB_Flag = true;
                else DB_Flag = false;

            }
            catch (Exception ex)
            {
                DB_Flag = false;
                Console.WriteLine(ex.Message);
            }
            return DB_Flag;
        }

        public Boolean dbOpen(string strFilePath)
        {
            Boolean bCreate = false;

            m_Provider = "Data Source=" + strFilePath + ";Version=3";

            try
            {
                System.IO.FileInfo dbFile = new System.IO.FileInfo(strFilePath);
                if (!dbFile.Exists)
                {
                    try
                    {
                        MessageBox.Show("Make SQLiteDB Success");
                        bCreate = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Make SQLiteDB Error");
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
                m_ObjCon.Open();
                m_dbOpen = true;
                if (bCreate)
                {
                    if (!createBasicTable())
                    {
                        m_ObjCon.Close();
                        m_ObjCon = null;
                        if (System.IO.File.Exists(strFilePath))
                        {
                            System.IO.File.Delete(strFilePath);
                        }
                        return false;
                    }
                }
                else
                {

                    // table data 삭제
                    DeleteTableData("REFUNDSLIP");
                    DeleteTableData("SALES_GOODS");
                    DeleteTableData("SLIP_PRINT_DOCS");
                    DeleteTableData("REFUND_SLIP_SIGN");

                }

            }
            catch (Exception e)
            {
                m_dbOpen = false;
                e.ToString();
            }
            return m_dbOpen;
        }

        public void DeleteTableData(string strTableName)
        {
            bool tableExists = false;

            DataTable dt = m_ObjCon.GetSchema("tables");
            string old_date = DateTime.Now.AddDays(-15).ToString("yyyyMMdd"); //오늘날짜에서 15일 전
            string to_date = DateTime.Now.ToString("yyyyMMdd"); //오늘날짜에서 15일 전
            string strQuery = "";
            to_date += "%";
            foreach (DataRow row in dt.Rows)
            {
                if (strTableName.Equals(row["TABLE_NAME"].ToString()))
                {
                    tableExists = true;
                    break;
                }
            }

            if (tableExists)
            {
                strQuery = "DELETE FROM ";
                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);
                if (strTableName.Equals("REFUNDSLIP"))
                {
                    strQuery += strTableName + Environment.NewLine + " WHERE REG_DTM < @REG_DTM";
                    cmd.CommandText = strQuery;
                    cmd.Parameters.Add(new SQLiteParameter("@" + "REG_DTM", old_date));
                    cmd.ExecuteNonQuery();

                }
                else if (strTableName.Equals("SALES_GOODS"))
                {
                    strQuery += strTableName + Environment.NewLine + " WHERE REG_DTM < @REG_DTM";
                    cmd.CommandText = strQuery;
                    cmd.Parameters.Add(new SQLiteParameter("@" + "REG_DTM", old_date));
                    cmd.ExecuteNonQuery();
                }
                else if (strTableName.Equals("SLIP_PRINT_DOCS"))
                {
                    Boolean regColumn = ExitsTableColumn("SLIP_PRINT_DOCS", "REG_DTM");
                    if (!regColumn)
                    {
                        alterTable("SLIP_PRINT_DOCS", "REG_DTM");
                    }
                    strQuery += strTableName + Environment.NewLine + " WHERE REG_DTM NOT LIKE @REG_DTM";
                    cmd.CommandText = strQuery;
                    cmd.Parameters.Add(new SQLiteParameter("@" + "REG_DTM", to_date));
                    cmd.ExecuteNonQuery();
                }
                else if (strTableName.Equals("REFUND_SLIP_SIGN"))
                {
                    strQuery += strTableName + Environment.NewLine + " WHERE REG_DTM < @REG_DTM";
                    cmd.CommandText = strQuery;
                    cmd.Parameters.Add(new SQLiteParameter("@" + "REG_DTM", old_date));
                    cmd.ExecuteNonQuery();
                }
                else
                {

                }
                cmd = null;

            }
        }

        public string selectConfigData(string strKey)
        {
            string strRet = string.Empty;
            string strQuery = string.Empty;
            try
            {
                strQuery = "SELECT ITEM_VALUE FROM TML_CONFIG WHERE ITEM_KEY=@ITEM_KEY";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.Parameters.Add(new SQLiteParameter("@ITEM_KEY", strKey));
                SQLiteDataReader oleData = connCmd.ExecuteReader();
                while (oleData.Read())
                {
                    strRet = (string)oleData["ITEM_VALUE"];

                }
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return strRet;
        }
        /*public Boolean Create_Sqlite_Database(string strFilePath)
        {
            Boolean DB_Flag = false;
            try
            {
                //SQLite DB를 생성한다.

                //1. TML_CONFIG 테이블의 생성
                string sql = "CREATE TABLE IF NOT EXISTS TML_CONFIG ( "   +
                             "  ITEM_KEY    varchar(20)     PRIMARY KEY " + 
                             ", ITEM_DESC   varchar(100) "  + 
                             ", ITEM_VALUE  varchar(40) "   +
                             ");";
                if (!Execute_SQLiteStr(strFilePath, sql)) DB_Flag = false;
          
              
                //2. USER_INFO 테이블의 생성
                sql = "CREATE TABLE IF NOT EXISTS USER_INFO ( " +
                             "  USERID      varchar(200) " +
                             ", USERNAME    varchar(200) " +
                             ", MERCHANTNO  varchar(200) " +
                             ", OPENDATE    varchar(200) " +
                             ", DESKID      varchar(200) " +
                             ")";
                if (!Execute_SQLiteStr(strFilePath, sql)) DB_Flag = false;


            }
            catch (Exception e)
            {
                DB_Flag = false;
            }
            return DB_Flag;
        }*/
        /*
        public Boolean Execute_SQLiteStr(string strFilePath, string sql)
        {
            Boolean DB_Flag = false;
            try
            {
                string ConnectionStr = "Data Source=" + strFilePath + ";Version=3";
                conn = new SQLiteConnection(ConnectionStr);
                conn.Open();
                SQLiteCommand command = new SQLiteCommand(sql, conn);
                int result = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                DB_Flag = false;
            }
            return DB_Flag;
        }
        */
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public JArray SelectTableOrderCounry(string strTableName, JObject jsonWhere = null, JObject jsonOrder = null)
        {
            JArray arrRet = new JArray();
            JObject jsonRet = null;

            try
            {
                //IList<string> keys = null;
                //IList<string> keyOrders = null;
                string strQuery = " SELECT * FROM CODE WHERE CODEDIV ='COUNTRY_CODE' ORDER BY COMCODE ";


                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);

                SQLiteDataReader oleData = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (oleData.Read())
                {
                    jsonRet = new JObject();
                    for (int i = 0; i < oleData.FieldCount; i++)
                    {
                        jsonRet.Add(oleData.GetName(i), oleData[oleData.GetName(i)].ToString());
                    }
                    arrRet.Add(jsonRet);
                }
            }

            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return arrRet;
        }

        public Boolean createBasicTable()
        {
            Boolean bRet = true;
            //1. 환경설정 저장 Table 생성
            try
            {
                String strQuery = string.Empty;
                strQuery = "CREATE TABLE IF NOT EXISTS TML_CONFIG " +
                "(ITEM_KEY varchar(20), ITEM_DESC varchar(100), ITEM_VALUE varchar(40), " +
                "CONSTRAINT TML_SETUP_PrimaryKey PRIMARY KEY (ITEM_KEY))";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.ExecuteNonQuery();
                connCmd = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bRet = false;
            }

            //4. 채번 Table 생성
            try
            {
                String strQuery = string.Empty;
                strQuery = "CREATE TABLE IF NOT EXISTS ID_CREATION_RULE " +
                "( ITEM_KEY varchar(20), ITEM_DESC varchar(100), ITEM_VALUE varchar(40), " +
                "CONSTRAINT TML_SETUP_PrimaryKey PRIMARY KEY (ITEM_KEY))";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.ExecuteNonQuery();
                connCmd = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bRet = false;
            }

            try
            {
                String strQuery = string.Empty;
                string strDate = string.Empty;
                string strTime = string.Empty;

                strQuery = "CREATE TABLE IF NOT EXISTS MIGRATION_CHECK " +
                "(MG_ID INTEGER PRIMARY KEY AUTOINCREMENT, MG_DATE varchar(8), MG_TIME varchar(6),MG_FLAG varchar(2))";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.ExecuteNonQuery();

                connCmd.CommandText = "insert into MIGRATION_CHECK(MG_DATE, MG_TIME, MG_FLAG) values(@MG_DATE, @MG_TIME, 'N')";
                strDate = DateTime.Now.ToString("yyyyMMdd");
                strTime = DateTime.Now.ToString("hhmmss");
                connCmd.Parameters.Add(new SQLiteParameter("@MG_DATE", strDate));
                connCmd.Parameters.Add(new SQLiteParameter("@MG_TIME", strTime));
                int result = connCmd.ExecuteNonQuery();

                connCmd = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bRet = false;
            }
            return bRet;
        }

        public string getMigration_Flag()
        {
            string strRet = string.Empty;
            string strQuery = string.Empty;
            try
            {
                strQuery = "SELECT MG_FLAG FROM MIGRATION_CHECK WHERE MG_ID IN (SELECT MAX(MG_ID) FROM MIGRATION_CHECK)";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                SQLiteDataReader oleData = connCmd.ExecuteReader();
                while (oleData.Read())
                {
                    strRet = (string)oleData["MG_FLAG"];

                }
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return strRet;
        }
        /*
        public Boolean ExitsTable(string strTableName)
        { 
            Boolean Exist_Flag = false;
            string strRet = string.Empty;
            string strQuery = string.Empty;
            try
            {
                strQuery = "SELECT COUNT(*) AS COUNT FROM SQLITE_MASTER WHERE NAME = '" + strTableName + "'";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                SQLiteDataReader oleData = connCmd.ExecuteReader();
                while (oleData.Read())
                {
                    strRet = (string)oleData["COUNT"];
                    if (strRet.Equals("1")) Exist_Flag = true;

                }
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return Exist_Flag;
        }
        */
        public Boolean ExitsTable(string strTableName)
        {

            Boolean tableExists = false;
            DataTable dt = m_ObjCon.GetSchema("tables");

            foreach (DataRow row in dt.Rows)
            {
                if (strTableName.Equals(row["TABLE_NAME"].ToString()))
                {
                    tableExists = true;
                    break;
                }
            }
            return tableExists;
        }

        public JArray SelectTable(string strTableName, JObject jsonWhere = null)
        {
            JArray arrRet = new JArray();
            JObject jsonRet = null;
            bool tableExists = false;

            try
            {
                tableExists = ExitsTable(strTableName);

                if (!tableExists)
                {
                    return arrRet;
                }

                IList<string> keys = null;
                string strQuery = " SELECT * FROM " + strTableName;
                if (jsonWhere != null && jsonWhere.Count > 0)
                {
                    strQuery += " WHERE " + Environment.NewLine + "";

                    keys = jsonWhere.Properties().Select(p => p.Name).ToList();

                    for (int i = 0; i < keys.Count; i++)
                    {
                        strQuery += " " + keys[i] + " = @" + keys[i];

                        if (i != (keys.Count - 1))
                            strQuery += " and ";
                        strQuery += " " + Environment.NewLine + " ";
                    }
                }

                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);
                if (keys != null)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@" + keys[i], jsonWhere[keys[i]].ToString()));

                    }
                }
                SQLiteDataReader oleData = cmd.ExecuteReader();
                while (oleData.Read())
                {
                    jsonRet = new JObject();
                    for (int i = 0; i < oleData.FieldCount; i++)
                    {
                        jsonRet.Add(oleData.GetName(i), oleData[oleData.GetName(i)].ToString());
                    }
                    arrRet.Add(jsonRet);
                }
            }

            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return arrRet;
        }

        public Boolean updateTable(JObject jsonReq, JObject jsonWhereKey, string strTableName)
        {
            Boolean bRet = true;
            try
            {
                IList<string> keys = jsonReq.Properties().Select(p => p.Name).ToList();

                IList<string> whereKeys = jsonWhereKey.Properties().Select(p => p.Name).ToList();

                string strQuery = "UPDATE " + strTableName + " SET " + Environment.NewLine + "";
                for (int i = 0; i < keys.Count; i++)
                {
                    strQuery += " " + keys[i] + " = @" + keys[i];

                    if (i != (keys.Count - 1))
                        strQuery += " , ";
                    strQuery += " " + Environment.NewLine + " ";
                }

                strQuery += " WHERE " + Environment.NewLine + " ";
                for (int i = 0; i < whereKeys.Count; i++)
                {
                    strQuery += " " + whereKeys[i] + " = @" + whereKeys[i];
                    if (i != (whereKeys.Count - 1))
                        strQuery += " and ";
                    strQuery += " " + Environment.NewLine + " ";
                }

                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);

                for (int i = 0; i < keys.Count; i++)
                {
                    cmd.Parameters.Add(new SQLiteParameter("@" + keys[i], jsonReq[keys[i]].ToString()));
                }
                for (int i = 0; i < whereKeys.Count; i++)
                {
                    cmd.Parameters.Add(new SQLiteParameter("@" + whereKeys[i], jsonWhereKey[whereKeys[i]].ToString()));
                }


                int updated_records_count = cmd.ExecuteNonQuery();
                if (updated_records_count > 0)
                {
                    bRet = true;
                }
                else
                {
                    bRet = false;
                }
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
                bRet = false;
            }

            return bRet;
        }

        public Boolean updateMigration_Flag(string Flag)
        {
            Boolean Success_flag = false;
            string strQuery = string.Empty;
            try
            {
                strQuery = "UPDATE MIGRATION_CHECK SET MG_FLAG = @MG_FLAG WHERE MG_ID IN (SELECT MAX(MG_ID) FROM MIGRATION_CHECK)";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.Parameters.Add(new SQLiteParameter("@MG_FLAG", Flag));
                connCmd.ExecuteNonQuery();
                connCmd = null;
                Success_flag = true;
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return Success_flag;
        }

        //table 삭제
        public void DropTable(string strTableName)
        {

            bool tableExists = false;

            DataTable dt = m_ObjCon.GetSchema("tables");

            foreach (DataRow row in dt.Rows)
            {
                if (strTableName.Equals(row["TABLE_NAME"].ToString()))
                {
                    tableExists = true;
                    break;
                }
            }

            //table 있으면
            if (tableExists)
            {
                //table 데이터 삭제
                SQLiteCommand cmd = new SQLiteCommand(string.Format("DELETE FROM {0}", strTableName), m_ObjCon);
                cmd.ExecuteNonQuery();

                //table 삭제
                SQLiteCommand cmd2 = new SQLiteCommand(string.Format("DROP TABLE {0}", strTableName), m_ObjCon);
                cmd2.ExecuteNonQuery();

            }
            else
            {
                //MessageBox.Show(string.Format("Table {0} not exists", tableToDelete));
            }
        }

        public Boolean createTable(JObject jsonReq, string strTableName)
        {
            Boolean bRet = true;

            IList<string> keys = jsonReq.Properties().Select(p => p.Name).ToList();

            try
            {
                String strQuery = string.Empty;
                strQuery = "CREATE TABLE IF NOT EXISTS " + strTableName + " ( ";
                for (int i = 0; i < keys.Count; i++)
                {
                    //strQuery += keys[i] + " " + jsonReq[keys[i]].ToString() + ", "; //컬럼 데이터형을 가변적으로 주는것도 가능하나, 범용성을 위해 MEMO로 고정
                    if (strTableName.Equals("REFUND_SLIP_SIGN") && keys[i].Equals("SLIP_SIGN_DATA"))
                    {
                        strQuery += keys[i] + " varchar(4000) ";
                    }
                    else
                    {
                        strQuery += keys[i] + " varchar(200) ";

                    }

                    if (i != keys.Count - 1)
                    {
                        strQuery += " , ";
                    }
                }
                //strQuery += " CONSTRAINT " + strTableName + "_PrimaryKey PRIMARY KEY (" + strKeyName + ") "; //PK 지정도 가능하나, 로컬 DB에서 유의미한 퍼포먼스 차이를 기대할 수 없음
                strQuery += " )";

                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.ExecuteNonQuery();
                //connCmd.Dispose();
                connCmd = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bRet = false;
            }
            return bRet;
        }

        public Boolean InsertTable(JObject jsonReq, string strTableName, int insert_Flag)
        {
            try
            {
                IList<string> keys = jsonReq.Properties().Select(p => p.Name).ToList();

                string strQuery = "INSERT INTO " + strTableName + " ( ";
                for (int i = 0; i < keys.Count; i++)
                {
                    strQuery += " " + keys[i] + " ";
                    if (i != (keys.Count - 1))
                        strQuery += " , ";
                    strQuery += " " + Environment.NewLine + " ";
                }
                strQuery += " ) ";

                strQuery += " VALUES( ";

                for (int i = 0; i < keys.Count; i++)
                {
                    strQuery += " @" + keys[i] + " ";
                    if (i != (keys.Count - 1))
                        strQuery += " , ";
                    strQuery += " " + Environment.NewLine + " ";

                }
                strQuery += " ) ";

                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);

                for (int i = 0; i < keys.Count; i++)
                {
                    cmd.Parameters.Add(new SQLiteParameter("@" + keys[i], jsonReq[keys[i]].ToString()));
                    //if(System.Text.ASCIIEncoding.Unicode.GetByteCount(jsonReq[keys[i]].ToString()) > 256)
                    //{
                    //    Console.WriteLine("data is too large >> "+ keys[i]+":"+ jsonReq[keys[i]].ToString());
                    //}
                }
                if (insert_Flag == 0) BeginTran(m_ObjCon);
                cmd.ExecuteNonQuery();
                if (insert_Flag == 1) CommitTran(m_ObjCon);
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
                return false;
            }

            return true;
        }

        private void BeginTran(SQLiteConnection conn)
        {
            SQLiteCommand command = new SQLiteCommand("Begin", conn);
            command.ExecuteNonQuery();
            command.Dispose();
        }
        private void CommitTran(SQLiteConnection conn)
        {
            SQLiteCommand command = new SQLiteCommand("Commit", conn);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public JArray getSlipList(string strUserID, string strTableName, string strStartDate, string strEndDate, string strSendFlag, string strStatus, string strSlipNO = "")
        {
            JArray arrRet = new JArray();
            //return arrRet;
            try
            {
                String Query = string.Empty;
                // 2017.09.19 이원규 전체 거래내역 조회 가능하도록 수정
                //Query = String.Format("SELECT * FROM " + strTableName + " WHERE USERID= '"+ strUserID+"' ");
                Query = String.Format("SELECT * FROM " + strTableName + " WHERE 1 = 1");
                if (!string.Empty.Equals(strUserID))
                {
                    Query += " AND USERID = '" + strUserID + "'";
                }
                if (!string.Empty.Equals(strSendFlag))
                {
                    Query += " AND SEND_FLAG = '" + strSendFlag + "'";
                }
                if (!string.Empty.Equals(strStatus))
                {
                    Query += " AND SLIP_STATUS_CODE = '" + strStatus + "'";
                }

                if (!string.Empty.Equals(strStartDate))
                {
                    Query += " AND REG_DTM >= '" + strStartDate + "'";
                }

                if (!string.Empty.Equals(strEndDate))
                {
                    Query += " AND REG_DTM <= '" + strEndDate + "'";
                }
                if (!string.Empty.Equals(strSlipNO))
                {
                    Query += " AND SLIP_NO = '" + strSlipNO + "'";
                }

                Query += " ORDER BY SALEDT DESC ";

                SQLiteCommand OLECmd = new SQLiteCommand(Query, m_ObjCon);
                OLECmd.CommandType = CommandType.Text;
                SQLiteDataReader OLEReader = OLECmd.ExecuteReader(CommandBehavior.Default);

                JObject jsonRet = null;

                while (OLEReader.Read())
                {
                    jsonRet = new JObject();
                    for (int i = 0; i < OLEReader.FieldCount; i++)
                    {
                        jsonRet.Add(OLEReader.GetName(i), OLEReader[OLEReader.GetName(i)].ToString());
                    }
                    arrRet.Add(jsonRet);
                }

                OLECmd.Dispose();
                OLEReader.Dispose();
            }
            catch (Exception ex)
            {
                m_logger.Error(ex.StackTrace);
            }
            return arrRet;
        }

        public JArray SelectUpiBinTable(string strTableName, string strCardNo)
        {
            JArray arrRet = new JArray();
            JObject jsonRet = null;
            try
            {
                string strQuery = " SELECT * FROM BIN WHERE '" + strCardNo + "' LIKE BIN + '%'  ";
                strQuery += " AND PAN_LENGTH ='" + strCardNo.Length + "' ";
                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);
                SQLiteDataReader oleData = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (oleData.Read())
                {
                    jsonRet = new JObject();
                    for (int i = 0; i < oleData.FieldCount; i++)
                    {
                        jsonRet.Add(oleData.GetName(i), oleData[oleData.GetName(i)].ToString());
                    }
                    arrRet.Add(jsonRet);
                }
            }

            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return arrRet;
        }
        public Boolean ExistsConfigData(string strKey)
        {
            string strRet = string.Empty;
            string strQuery = string.Empty;
            Boolean bExists = false;
            try
            {
                strQuery = "SELECT ITEM_VALUE FROM TML_CONFIG WHERE ITEM_KEY=@ITEM_KEY";
                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.Parameters.Add(new SQLiteParameter("@ITEM_KEY", strKey));
                SQLiteDataReader oleData = connCmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (oleData.Read())
                {
                    bExists = true;
                    strRet = oleData["ITEM_VALUE"].ToString();
                }
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return bExists;
        }

        public string updateConfigData(string strKey, string strValue, string strDesc)
        {
            string strRet = string.Empty;
            String strQuery = string.Empty;
            try
            {
                //if (string.Empty.Equals(ExistsConfigData(strKey)))
                if (ExistsConfigData(strKey))
                {
                    strQuery = "UPDATE TML_CONFIG SET ITEM_DESC= '" + strDesc + "', ITEM_VALUE='" + strValue + "' WHERE ITEM_KEY = '" + strKey + "'";
                }
                else
                {
                    strQuery = "INSERT INTO TML_CONFIG(ITEM_KEY,ITEM_DESC,ITEM_VALUE)VALUES('" + strKey + "','" + strDesc + "','" + strValue + "')";
                }

                SQLiteCommand cmd = new SQLiteCommand(strQuery, m_ObjCon);
                cmd.ExecuteNonQuery();
                //cmd.Dispose();
                cmd = null;
            }
            catch (Exception e)
            {
                m_logger.Error(e.StackTrace);
            }

            return strRet;
        }
        public string deleteConfigData(string strKey)
        {
            string strRet = string.Empty;
            return strRet;
        }

        public Boolean ExitsTableColumn(string strTableName, string strColumn)
        {

            Boolean tableExists = false;
            String[] columnRestrictions = new String[4];
            columnRestrictions[2] = strTableName;
            columnRestrictions[3] = strColumn;

            DataTable dt = m_ObjCon.GetSchema("Columns", columnRestrictions);

            foreach (DataRow row in dt.Rows)
            {
                tableExists = true;
            }
            return tableExists;
        }

        public Boolean alterTable(string strTableName, string strColumnName)
        {
            Boolean bRet = true;
            try
            {
                String strQuery = string.Empty;
                strQuery = "ALTER TABLE " + strTableName + " ADD COLUMN " + strColumnName + " MEMO ";

                SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                connCmd.ExecuteNonQuery();
                connCmd = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                bRet = false;
            }
            return bRet;
        }

        public JObject getRCTInfo(string strSlipNO = "")
        {
            JObject jsonRet = null;
            //return arrRet;
            try
            {
                String Query = string.Empty;

                Query = String.Format("SELECT DISTINCT(RCT_NO) AS RCT_NO FROM SALES_GOODS  WHERE 1 = 1 ");

                if (!string.Empty.Equals(strSlipNO))
                {
                    Query += " AND SLIP_NO = '" + strSlipNO + "'";
                }


                SQLiteCommand OLECmd = new SQLiteCommand(Query, m_ObjCon);
                OLECmd.CommandType = CommandType.Text;
                SQLiteDataReader OLEReader = OLECmd.ExecuteReader(CommandBehavior.Default);



                while (OLEReader.Read())
                {
                    jsonRet = new JObject();
                    for (int i = 0; i < OLEReader.FieldCount; i++)
                    {
                        jsonRet.Add(OLEReader.GetName(i), OLEReader[OLEReader.GetName(i)].ToString());
                    }
                }

                OLECmd.Dispose();
                OLEReader.Dispose();
            }
            catch (Exception ex)
            {
                m_logger.Error(ex.StackTrace);
            }
            return jsonRet;
        }

        public Boolean data_Migration(JArray arrTemp, string tablename)
        {
            Boolean Success_Flag = false;
            string strQuery = string.Empty;
            string strDate = string.Empty;
            string strTime = string.Empty;
            try
            {
                if (arrTemp != null && arrTemp.Count > 0)
                {
                    BeginTran(m_ObjCon);
                    strDate = DateTime.Now.ToString("yyyyMMdd");
                    strTime = DateTime.Now.ToString("hhmmss"); m_logger.Info(tablename + "  START::" + strDate + " " + strTime);

                    strQuery = "CREATE TABLE IF NOT EXISTS BIN " +
                                                    "(  CARD_TYPE varchar(200),  PAN_LENGTH varchar(200), ISSUER_NAME varchar(200), FILE_GENERATION_TIME varchar(200), BIN varchar(200), " +
                                                    "   ISSUER_IIN varchar(200), BIN_LENGTH varchar(200), CARD_NAME varchar(200), REG_DTM varchar(200),  MOD_DTM varchar(200));";
                    SQLiteCommand createCmd = new SQLiteCommand(strQuery, m_ObjCon);
                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS CODE " +
                                            "(  ATTRIB01 varchar(200),  ATTRIB02 varchar(200), ATTRIB03 varchar(200), ATTRIB04 varchar(200), ATTRIB05 varchar(200), " +
                                            "   ATTRIB06 varchar(200),  ATTRIB07 varchar(200), ATTRIB08 varchar(200), ATTRIB09 varchar(200), ATTRIB10 varchar(200), " +
                                            "   ATTRIB11 varchar(200),  ATTRIB12 varchar(200), " +
                                            "   ATTRIBDESC01 varchar(200),  ATTRIBDESC02 varchar(200), ATTRIBDESC03 varchar(200), ATTRIBDESC04 varchar(200), ATTRIBDESC05 varchar(200), " +
                                            "   ATTRIBDESC06 varchar(200),  ATTRIBDESC07 varchar(200), ATTRIBDESC08 varchar(200), ATTRIBDESC09 varchar(200), ATTRIBDESC10 varchar(200), " +
                                            "   ATTRIBDESC11 varchar(200),  ATTRIBDESC12 varchar(200), " +
                                            "   COMCODE varchar(200),  REMARK varchar(200), CODENAME varchar(200), ACTIVEFLG varchar(200), SEQ varchar(200), " +
                                            "   CODEDIV varchar(200),  CODEDESC varchar(200),  USEYN varchar(200));";
                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS ITEM " +
                                            "(CATEGORY_CODE varchar(200),  CATEGORY_NAME varchar(200), SEQ varchar(200), P_CODE varchar(200));";
                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS MERCHANT " +
                                            "(TAXOFFICE_ADDR varchar(200),  PARTNER_NO varchar(200), SALEGOODS_USEYN varchar(200), MERCHANT_ENNM varchar(200), MERCHANT_NO varchar(200), " +
                                            " TAX_FORMULA varchar(200),  TAX_PROC_TIME_CODE_DESC varchar(200), MGROUP_NO varchar(200), FEE_PROC_TIME_CODE varchar(200), NATIONALITY_MAPPING_USEYN varchar(200), " +
                                            " FEE_TYPE varchar(200),  MGROUP_JPNM varchar(200), TAX_TYPE varchar(200), TAXOFFICE_NO varchar(200) ); ";

                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS REFUNDSLIP " +
                                            "(BUYER_NAME varchar(200),  PASSPORT_SERIAL_NO varchar(200), NATIONALITY_CODE varchar(200), PERMIT_NO varchar(200), REFUND_NOTE varchar(200), " +
                                            " NATIONALITY_NAME varchar(200),  GENDER_CODE varchar(200), BUYER_BIRTH varchar(200), PASS_EXPIRYDT varchar(200), INPUT_WAY_CODE varchar(200), " +
                                            " RESIDENCE_NO varchar(200),  RESIDENCE varchar(200), ENTRYDT varchar(200), PASSPORT_TYPE varchar(200), PASSPORT_TYPE_NAME varchar(200), " +
                                            " SLIP_NO varchar(200), MERCHANT_NO varchar(200), SHOP_NAME varchar(200), OUT_DIV_CODE varchar(200), REFUND_WAY_CODE varchar(200), " +
                                            " REFUND_WAY_CODE_NAME varchar(200), SLIP_STATUS_CODE varchar(200), TML_ID varchar(200), REFUND_CARDNO varchar(200), REFUND_CARD_CODE varchar(200), " +
                                            " TOTAL_SLIPSEQ varchar(200), TAX_PROC_TIME_CODE varchar(200), TAX_POINT_PROC_CODE varchar(200), GOODS_BUY_AMT varchar(200), GOODS_TAX_AMT varchar(200), " +
                                            " GOODS_REFUND_AMT varchar(200), CONSUMS_BUY_AMT varchar(200), CONSUMS_TAX_AMT varchar(200), CONSUMS_REFUND_AMT varchar(200), TOTAL_EXCOMM_IN_TAX_SALE_AMT varchar(200), " +
                                            " TOTAL_COMM_IN_TAX_SALE_AMT varchar(200), UNIKEY varchar(200), SALEDT varchar(200), REFUNDDT varchar(200), USERID varchar(200), " +
                                            " MERCHANTNO varchar(200), DESKID varchar(200), COMPANYID varchar(200), SEND_FLAG varchar(200), PRINT_CNT varchar(200), " +
                                            " REG_DTM varchar(200) ); ";
                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS SALES_GOODS " +
                                             "(ITEM_NAME varchar(200),  UNIT_AMT varchar(200), QTY varchar(200), BUY_AMT varchar(200), TAX_AMT varchar(200), " +
                                             " REFUND_AMT varchar(200),  FEE_AMT varchar(200), TAX_TYPE varchar(200), ITEM_TYPE varchar(200), MAIN_CAT varchar(200), " +
                                             " MID_CAT varchar(200),  ITEM_TYPE_TEXT varchar(200), MAIN_CAT_TEXT varchar(200), MID_CAT_TEXT varchar(200), RCT_NO varchar(200), " +
                                             " ITEM_NO varchar(200),  SLIP_NO varchar(200), USERID varchar(200), REG_DTM varchar(200), SHOP_NO varchar(200), " +
                                             " TAX_CAL_TYPE varchar(200) ); ";

                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS SLIP_PRINT_DOCS " +
                                            "(SLIP_NO varchar(200),  DOCID varchar(200), RETAILER varchar(200), GOODS varchar(200), TOURIST varchar(200), " +
                                            " ADSINFO varchar(200),  PREVIEW varchar(200), SIGN varchar(4000), REG_DTM varchar(200) ); ";

                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS TAXOFFICE " +
                                            "(API_USING_YN varchar(200),  NEW_USING_YN varchar(200), APPL_DATE varchar(200), APPL_EX_RATE varchar(200), DELETE_YN varchar(200), " +
                                            " STD_EX_RATE varchar(200),  TRUNC_NUM varchar(200), CHARGERATE varchar(200), USING_YN varchar(200), CY_CODE varchar(200), " +
                                            " BUY_EX_RATE varchar(200) ); ";

                    createCmd.ExecuteNonQuery();

                    createCmd.CommandText = "CREATE TABLE IF NOT EXISTS USERINFO " +
                                            "(userId varchar(200),  userName varchar(200), merchantNo varchar(200), openDate varchar(200), deskId varchar(200) ); ";

                    createCmd.ExecuteNonQuery();

                    createCmd = null;

                    for (int i = 0; i < arrTemp.Count; i++)
                    {
                        JObject tempObj = (JObject)arrTemp[i];

                        if (tablename.Equals("TML_CONFIG") || tablename.Equals("ID_CREATION_RULE"))
                        {
                            strQuery = "insert into " + tablename + "(ITEM_KEY, ITEM_DESC, ITEM_VALUE) values( '" +
                                            tempObj["ITEM_KEY"].ToString() + "', '" +
                                            tempObj["ITEM_DESC"].ToString() + "', '" +
                                            tempObj["ITEM_VALUE"].ToString() + "');";
                        }
                        else
                        {

                            if (tablename.Equals("BIN"))
                            {
                                strQuery = "insert into " + tablename + "(CARD_TYPE, PAN_LENGTH, ISSUER_NAME, FILE_GENERATION_TIME, BIN, " +
                                            " ISSUER_IIN, BIN_LENGTH, CARD_NAME, REG_DTM, MOD_DTM ) values( '" +
                                            tempObj["CARD_TYPE"].ToString() + "', '" +  //1
                                            tempObj["PAN_LENGTH"].ToString() + "', '" +  //2
                                            tempObj["ISSUER_NAME"].ToString() + "', '" +  //3
                                            tempObj["FILE_GENERATION_TIME"].ToString() + "', '" +  //4
                                            tempObj["BIN"].ToString() + "', '" +  //5
                                            tempObj["ISSUER_IIN"].ToString() + "', '" +  //6
                                            tempObj["BIN_LENGTH"].ToString() + "', '" +  //7
                                            tempObj["CARD_NAME"].ToString() + "', '" +  //8
                                            tempObj["REG_DTM"].ToString() + "', '" +  //9
                                            tempObj["MOD_DTM"].ToString() + "');";    //10
                            }

                            if (tablename.Equals("CODE"))
                            {
                                strQuery = "insert into " + tablename + "(ATTRIB01, ATTRIB02, ATTRIB03, ATTRIB04, ATTRIB05, " +
                                            "ATTRIB06, ATTRIB07, ATTRIB08, ATTRIB09, ATTRIB10, ATTRIB11, ATTRIB12," +
                                            "ATTRIBDESC01, ATTRIBDESC02, ATTRIBDESC03, ATTRIBDESC04, ATTRIBDESC05, " +
                                            "ATTRIBDESC06, ATTRIBDESC07, ATTRIBDESC08, ATTRIBDESC09, ATTRIBDESC10, " +
                                            "ATTRIBDESC11, ATTRIBDESC12, " +
                                            "COMCODE, REMARK, CODENAME, ACTIVEFLG, SEQ, " +
                                            "CODEDIV, CODEDESC, USEYN) values( '" +
                                            tempObj["ATTRIB01"].ToString() + "', '" +  //1
                                            tempObj["ATTRIB02"].ToString() + "', '" +  //2
                                            tempObj["ATTRIB03"].ToString() + "', '" +  //3
                                            tempObj["ATTRIB04"].ToString() + "', '" +  //4
                                            tempObj["ATTRIB05"].ToString() + "', '" +  //5
                                            tempObj["ATTRIB06"].ToString() + "', '" +  //6
                                            tempObj["ATTRIB07"].ToString() + "', '" +  //7
                                            tempObj["ATTRIB08"].ToString() + "', '" +  //8
                                            tempObj["ATTRIB09"].ToString() + "', '" +  //9
                                            tempObj["ATTRIB10"].ToString() + "', '" +  //10
                                            tempObj["ATTRIB11"].ToString() + "', '" +  //11
                                            tempObj["ATTRIB12"].ToString() + "', '" +  //12
                                            tempObj["ATTRIBDESC01"].ToString() + "', '" +  //13
                                            tempObj["ATTRIBDESC02"].ToString() + "', '" +  //14
                                            tempObj["ATTRIBDESC03"].ToString() + "', '" +  //15
                                            tempObj["ATTRIBDESC04"].ToString() + "', '" +  //16
                                            tempObj["ATTRIBDESC05"].ToString() + "', '" +  //17
                                            tempObj["ATTRIBDESC06"].ToString() + "', '" +  //18
                                            tempObj["ATTRIBDESC07"].ToString() + "', '" +  //19
                                            tempObj["ATTRIBDESC08"].ToString() + "', '" +  //20
                                            tempObj["ATTRIBDESC09"].ToString() + "', '" +  //21
                                            tempObj["ATTRIBDESC10"].ToString() + "', '" +  //22
                                            tempObj["ATTRIBDESC11"].ToString() + "', '" +  //23
                                            tempObj["ATTRIBDESC12"].ToString() + "', '" +  //24
                                            tempObj["COMCODE"].ToString() + "', '" +  //25
                                            tempObj["REMARK"].ToString() + "', '" +  //26
                                            tempObj["CODENAME"].ToString() + "', '" +  //27
                                            tempObj["ACTIVEFLG"].ToString() + "', '" +  //28
                                            tempObj["SEQ"].ToString() + "', '" +  //29
                                            tempObj["CODEDIV"].ToString() + "', '" +  //30
                                            tempObj["CODEDESC"].ToString() + "', '" +  //31
                                            tempObj["USEYN"].ToString() + "');";    //32

                            }

                            if (tablename.Equals("ITEM"))
                            {
                                strQuery = "insert into " + tablename + "(CATEGORY_CODE, CATEGORY_NAME, SEQ, P_CODE) values( '" +
                                            tempObj["CATEGORY_CODE"].ToString() + "', '" +  //1
                                            tempObj["CATEGORY_NAME"].ToString() + "', '" +  //2
                                            tempObj["SEQ"].ToString() + "', '" +  //3
                                            tempObj["P_CODE"].ToString() + "');";    //4
                            }

                            if (tablename.Equals("MERCHANT"))
                            {
                                strQuery = "insert into " + tablename + "(TAXOFFICE_ADDR, PARTNER_NO, SALEGOODS_USEYN, MERCHANT_ENNM, MERCHANT_NO, " +
                                            " TAX_FORMULA, TAX_PROC_TIME_CODE_DESC, MGROUP_NO, FEE_PROC_TIME_CODE, NATIONALITY_MAPPING_USEYN, " +
                                            " FEE_TYPE, MGROUP_JPNM, TAX_TYPE, TAXOFFICE_NO ) values( '" +
                                            tempObj["TAXOFFICE_ADDR"].ToString() + "', '" +  //1
                                            tempObj["PARTNER_NO"].ToString() + "', '" +  //2
                                            tempObj["SALEGOODS_USEYN"].ToString() + "', '" +  //3
                                            tempObj["MERCHANT_ENNM"].ToString() + "', '" +  //4
                                            tempObj["MERCHANT_NO"].ToString() + "', '" +  //5
                                            tempObj["TAX_FORMULA"].ToString() + "', '" +  //6
                                            tempObj["TAX_PROC_TIME_CODE_DESC"].ToString() + "', '" +  //7
                                            tempObj["MGROUP_NO"].ToString() + "', '" +  //8
                                            tempObj["FEE_PROC_TIME_CODE"].ToString() + "', '" +  //9
                                            tempObj["NATIONALITY_MAPPING_USEYN"].ToString() + "', '" +  //10
                                            tempObj["FEE_TYPE"].ToString() + "', '" +  //11
                                            tempObj["MGROUP_JPNM"].ToString() + "', '" +  //12
                                            tempObj["TAX_TYPE"].ToString() + "', '" +  //13
                                            tempObj["TAXOFFICE_NO"].ToString() + "');";    //14

                            }

                            if (tablename.Equals("REFUNDSLIP"))
                            {
                                //strQuery = "insert into " + tablename + "(BUYER_NAME, PASSPORT_SERIAL_NO, NATIONALITY_CODE, PERMIT_NO, REFUND_NOTE, " +
                                strQuery = "insert into " + tablename + "(BUYER_NAME, PASSPORT_SERIAL_NO, NATIONALITY_CODE, " +
                                            " NATIONALITY_NAME, GENDER_CODE, BUYER_BIRTH, PASS_EXPIRYDT, INPUT_WAY_CODE, " +
                                            " RESIDENCE_NO, RESIDENCE, ENTRYDT, PASSPORT_TYPE, PASSPORT_TYPE_NAME, " +
                                            " SLIP_NO, MERCHANT_NO, SHOP_NAME, OUT_DIV_CODE, REFUND_WAY_CODE, " +
                                            " REFUND_WAY_CODE_NAME, SLIP_STATUS_CODE, TML_ID, REFUND_CARDNO, REFUND_CARD_CODE, " +
                                            " TOTAL_SLIPSEQ, TAX_PROC_TIME_CODE, TAX_POINT_PROC_CODE, GOODS_BUY_AMT, GOODS_TAX_AMT, " +
                                            " GOODS_REFUND_AMT, CONSUMS_BUY_AMT, CONSUMS_TAX_AMT, CONSUMS_REFUND_AMT, TOTAL_EXCOMM_IN_TAX_SALE_AMT, " +
                                            " TOTAL_COMM_IN_TAX_SALE_AMT, UNIKEY, SALEDT, REFUNDDT, USERID, " +
                                            " MERCHANTNO, DESKID, COMPANYID, SEND_FLAG, PRINT_CNT, " +
                                            " REG_DTM ) values( '" +
                                            tempObj["BUYER_NAME"].ToString() + "', '" +  //1
                                            tempObj["PASSPORT_SERIAL_NO"].ToString() + "', '" +  //2
                                            tempObj["NATIONALITY_CODE"].ToString() + "', '" +  //3
                                                                                               //tempObj["PERMIT_NO"].ToString() + "', '" +  //4
                                                                                               //tempObj["REFUND_NOTE"].ToString() + "', '" +  //5
                                            tempObj["NATIONALITY_NAME"].ToString() + "', '" +  //6
                                            tempObj["GENDER_CODE"].ToString() + "', '" +  //7
                                            tempObj["BUYER_BIRTH"].ToString() + "', '" +  //8
                                            tempObj["PASS_EXPIRYDT"].ToString() + "', '" +  //9
                                            tempObj["INPUT_WAY_CODE"].ToString() + "', '" +  //10
                                            tempObj["RESIDENCE_NO"].ToString() + "', '" +  //11
                                            tempObj["RESIDENCE"].ToString() + "', '" +  //12
                                            tempObj["ENTRYDT"].ToString() + "', '" +  //13
                                            tempObj["PASSPORT_TYPE"].ToString() + "', '" +  //14
                                            tempObj["PASSPORT_TYPE_NAME"].ToString() + "', '" +  //15
                                            tempObj["SLIP_NO"].ToString() + "', '" +  //16
                                            tempObj["MERCHANT_NO"].ToString() + "', '" +  //17
                                            tempObj["SHOP_NAME"].ToString() + "', '" +  //18
                                            tempObj["OUT_DIV_CODE"].ToString() + "', '" +  //19
                                            tempObj["REFUND_WAY_CODE"].ToString() + "', '" +  //20
                                            tempObj["REFUND_WAY_CODE_NAME"].ToString() + "', '" +  //21
                                            tempObj["SLIP_STATUS_CODE"].ToString() + "', '" +  //22
                                            tempObj["TML_ID"].ToString() + "', '" +  //23
                                            tempObj["REFUND_CARDNO"].ToString() + "', '" +  //24
                                            tempObj["REFUND_CARD_CODE"].ToString() + "', '" +  //25
                                            tempObj["TOTAL_SLIPSEQ"].ToString() + "', '" +  //26
                                            tempObj["TAX_PROC_TIME_CODE"].ToString() + "', '" +  //27
                                            tempObj["TAX_POINT_PROC_CODE"].ToString() + "', '" +  //28
                                            tempObj["GOODS_BUY_AMT"].ToString() + "', '" +  //29
                                            tempObj["GOODS_TAX_AMT"].ToString() + "', '" +  //30
                                            tempObj["GOODS_REFUND_AMT"].ToString() + "', '" +  //31
                                            tempObj["CONSUMS_BUY_AMT"].ToString() + "', '" +  //32
                                            tempObj["CONSUMS_TAX_AMT"].ToString() + "', '" +  //33
                                            tempObj["CONSUMS_REFUND_AMT"].ToString() + "', '" +  //34
                                            tempObj["TOTAL_EXCOMM_IN_TAX_SALE_AMT"].ToString() + "', '" +  //35
                                            tempObj["TOTAL_COMM_IN_TAX_SALE_AMT"].ToString() + "', '" +  //36
                                            tempObj["UNIKEY"].ToString() + "', '" +  //37
                                            tempObj["SALEDT"].ToString() + "', '" +  //38
                                            tempObj["REFUNDDT"].ToString() + "', '" +  //39
                                            tempObj["USERID"].ToString() + "', '" +  //40
                                            tempObj["MERCHANTNO"].ToString() + "', '" +  //41
                                            tempObj["DESKID"].ToString() + "', '" +  //42
                                            tempObj["COMPANYID"].ToString() + "', '" +  //43
                                            tempObj["SEND_FLAG"].ToString() + "', '" +  //44
                                            tempObj["PRINT_CNT"].ToString() + "', '" +  //45
                                            tempObj["REG_DTM"].ToString() + "');";    //46
                                string test = strQuery;
                            }

                            if (tablename.Equals("SALES_GOODS"))
                            {
                                strQuery = "insert into " + tablename + "(ITEM_NAME, UNIT_AMT, QTY, BUY_AMT, TAX_AMT, " +
                                            " REFUND_AMT, FEE_AMT, TAX_TYPE, ITEM_TYPE, MAIN_CAT, " +
                                            " MID_CAT, ITEM_TYPE_TEXT, MAIN_CAT_TEXT, MID_CAT_TEXT, RCT_NO, " +
                                            //" ITEM_NO, SLIP_NO, USERID, REG_DTM, SHOP_NO, " +
                                            " ITEM_NO, SLIP_NO, USERID, REG_DTM, " +
                                            " TAX_CAL_TYPE ) values( '" +
                                            tempObj["ITEM_NAME"].ToString() + "', '" +  //1
                                            tempObj["UNIT_AMT"].ToString() + "', '" +  //2
                                            tempObj["QTY"].ToString() + "', '" +  //3
                                            tempObj["BUY_AMT"].ToString() + "', '" +  //4
                                            tempObj["TAX_AMT"].ToString() + "', '" +  //5
                                            tempObj["REFUND_AMT"].ToString() + "', '" +  //6
                                            tempObj["FEE_AMT"].ToString() + "', '" +  //7
                                            tempObj["TAX_TYPE"].ToString() + "', '" +  //8
                                            tempObj["ITEM_TYPE"].ToString() + "', '" +  //9
                                            tempObj["MAIN_CAT"].ToString() + "', '" +  //10
                                            tempObj["MID_CAT"].ToString() + "', '" +  //11
                                            tempObj["ITEM_TYPE_TEXT"].ToString() + "', '" +  //12
                                            tempObj["MAIN_CAT_TEXT"].ToString() + "', '" +  //13
                                            tempObj["MID_CAT_TEXT"].ToString() + "', '" +  //14
                                            tempObj["RCT_NO"].ToString() + "', '" +  //15
                                            tempObj["ITEM_NO"].ToString() + "', '" +  //16
                                            tempObj["SLIP_NO"].ToString() + "', '" +  //17
                                            tempObj["USERID"].ToString() + "', '" +  //18
                                            tempObj["REG_DTM"].ToString() + "', '" +  //19
                                                                                      //tempObj["SHOP_NO"].ToString() + "', '" +  //20
                                            tempObj["TAX_CAL_TYPE"].ToString() + "');";    //21
                            }

                            if (tablename.Equals("SLIP_PRINT_DOCS"))
                            {
                                strQuery = "insert into " + tablename + "(SLIP_NO, DOCID, RETAILER, GOODS, TOURIST, " +
                                            " ADSINFO, PREVIEW, SIGN, REG_DTM ) values( '" +
                                            tempObj["SLIP_NO"].ToString() + "', '" +  //1
                                            tempObj["DOCID"].ToString() + "', '" +  //2
                                            tempObj["RETAILER"].ToString() + "', '" +  //3
                                            tempObj["GOODS"].ToString() + "', '" +  //4
                                            tempObj["TOURIST"].ToString() + "', '" +  //5
                                            tempObj["ADSINFO"].ToString() + "', '" +  //6
                                            tempObj["PREVIEW"].ToString() + "', '" +  //7
                                            tempObj["SIGN"].ToString() + "', '" +  //8
                                            tempObj["REG_DTM"].ToString() + "');";    //9
                            }

                            if (tablename.Equals("TAXOFFICE"))
                            {
                                strQuery = "insert into " + tablename + "(API_USING_YN, NEW_USING_YN, APPL_DATE, APPL_EX_RATE, DELETE_YN, " +
                                            " STD_EX_RATE, TRUNC_NUM, CHARGERATE, USING_YN, CY_CODE, " +
                                            " BUY_EX_RATE ) values( '" +
                                            tempObj["API_USING_YN"].ToString() + "', '" +  //1
                                            tempObj["NEW_USING_YN"].ToString() + "', '" +  //2
                                            tempObj["APPL_DATE"].ToString() + "', '" +  //3
                                            tempObj["APPL_EX_RATE"].ToString() + "', '" +  //4
                                            tempObj["DELETE_YN"].ToString() + "', '" +  //5
                                            tempObj["STD_EX_RATE"].ToString() + "', '" +  //6
                                            tempObj["TRUNC_NUM"].ToString() + "', '" +  //7
                                            tempObj["CHARGERATE"].ToString() + "', '" +  //8
                                            tempObj["USING_YN"].ToString() + "', '" +  //9
                                            tempObj["CY_CODE"].ToString() + "', '" +  //10
                                            tempObj["BUY_EX_RATE"].ToString() + "');";    //11
                            }

                            if (tablename.Equals("USERINFO"))
                            {
                                strQuery = "insert into " + tablename + "(userId, userName, merchantNo, openDate, deskId ) values( '" +
                                            tempObj["userId"].ToString() + "', '" +  //1
                                            tempObj["userName"].ToString() + "', '" +  //2
                                            tempObj["merchantNo"].ToString() + "', '" +  //3
                                            tempObj["openDate"].ToString() + "', '" +  //4
                                            tempObj["deskId"].ToString() + "');";    //5
                            }

                        }
                        SQLiteCommand connCmd = new SQLiteCommand(strQuery, m_ObjCon);
                        connCmd.ExecuteNonQuery();
                        connCmd = null;

                    }
                    CommitTran(m_ObjCon);
                    strDate = DateTime.Now.ToString("yyyyMMdd");
                    strTime = DateTime.Now.ToString("hhmmss");
                    m_logger.Info(tablename + "  END::" + strDate + " " + strTime);
                }
                Success_Flag = true;
            }
            catch (Exception e)
            {
                CommitTran(m_ObjCon);
                m_logger.Error(e.StackTrace);
                Success_Flag = false;
            }

            return Success_Flag;
        }
    }

}