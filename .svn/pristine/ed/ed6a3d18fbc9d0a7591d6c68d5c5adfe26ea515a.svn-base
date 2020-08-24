using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using GTF_Config;
using System.Windows.Forms;
using GTF_LocalDB;
//using GTF_SQLiteDB;
using Newtonsoft.Json.Linq;
using MetroFramework;

namespace GTF_STFM_COMM.Util
{
    public class Constants
    {
        //경로
        public static string PATH_ROOT = "/../";
        public static string PATH_CONFIG = "/./Config/";
        public static string PATH_DB = "/./DB/";
        public static string PATH_TEMP = "/./TEMP/";

        public static string PATH_CONFIG_FILE = "Config.xml";   //CONFIG 파일명
        public static string PATH_DB_FILE = "STFM.accdb";       //LOCAL DB 파일명
        //public static string PATH_DB_FILE = "STFM.mdb";       //LOCAL DB 파일명
        //public static string PATH_SQLITE_FILE = "STFM.sqlite";    //SQLITE DB 파일명

        public static string SYSTEM_LANGUAGE = "KO";            //기본 설정 언어
        //public static int PC_NO = -1;                           //일본(PC번호)
        public static int PASSPORT_TYPE = -1;                   //여권스캐너
        public static string PRINTER_TYPE = string.Empty;       //프린터 명
        public static string PRINTER_OPOS_TYPE = string.Empty;  //OPOS 연결 프린터 명
        public static string SLIP_TYPE = string.Empty;                       //출력전표 종류

        public static string TML_ID = string.Empty;             //단말기 ID
        public static string USER_ID = string.Empty;            //사용자 ID
        public static string DESK_ID = string.Empty;            //데스크 ID
        public static string SIGNPAD_USE = string.Empty;        //전사서명 사용여부
        public static string PRINT_TYPE = string.Empty;         //프린트 설정
        public static string RCT_ADD = string.Empty;            //영수증 정보 입력 여부
        public static string TAX_TYPE = string.Empty;            //세금구분
        public static string PRINT_SETTING = string.Empty;

        public static string COMPANY_ID = "000001";             //회사 ID
        public static string LANGUAGECD = "jp";                 //언어구분
        public static string USER_AUTH = string.Empty;          //사용자권한
        public static string MERCHANT_NO = string.Empty;        //매장번호
        public static string OPEN_DATE = string.Empty;          //로그인 날짜

        public static Boolean IS_DEV { get; set; }              //개발여부

        public static string SERVER_URL { get; set; }           //서버 URL
        public static string SERVER_IP { get; set; }            //서버 IP
        public static string SERVER_PORT { get; set; }          //서버 PORT(TCP/IP)

        //logger 생성
        public static ILog LOGGER_DOC { get; set; }             //전문 로그
        public static ILog LOGGER_SCREEN { get; set; }          //화면 액션 로그
        public static ILog LOGGER_MAIN { get; set; }            //전체 로그
        public static GTF_ConfigManager CONF_MANAGER;           //Config Manager
        public static GTF_LocalDBManager LDB_MANAGER;           //Local DB Manager
        //public static GTF_SQLiteDBManager SQLITE_MANAGER;       //SQLITE DB Manager

        public static Boolean LOGIN_YN { get; set; }            //로그인여부
        public static Boolean CODE_DOWNLOAD { get; set; }       //코드데이터 다운로드 여부

        public static int SLIP_SEQ = 0;                         //전표 발행 순번

        public static string TABLE_REFUND_SLIP = "REFUNDSLIP";
        public static string TABLE_SALES_GOODS = "SALES_GOODS";
        public static string SEND_FLAG = "SEND_FLAG";
        public static string PRINT_CNT = "PRINT_CNT";
        public static string FLAG_ALL = "ALL";
        public static string PRINT_GOODS_TYPE ="0"; 


        public static Boolean CLOSING_YN = false;          //마감여부

        public static Boolean PASSPORT_SCAN_OPEN = false;
        public Constants(Control cParent)
        {
            try {
                CLOSING_YN = false;
                LOGIN_YN = false;
                CODE_DOWNLOAD = false;
                string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
                PATH_ROOT = appPath + PATH_ROOT;
                PATH_CONFIG = PATH_ROOT + PATH_CONFIG;
                PATH_DB = PATH_ROOT + PATH_DB;
                ////로컬 DB 폴더
                if (!System.IO.Directory.Exists(PATH_DB))
                    System.IO.Directory.CreateDirectory(PATH_DB);
                //TEMP 폴더
                PATH_TEMP = PATH_ROOT + PATH_TEMP;
                if (!System.IO.Directory.Exists(PATH_TEMP))
                    System.IO.Directory.CreateDirectory(PATH_TEMP);

                PATH_CONFIG_FILE = PATH_CONFIG + PATH_CONFIG_FILE;
                PATH_DB_FILE = PATH_DB + PATH_DB_FILE;
                //PATH_SQLITE_FILE = PATH_DB + PATH_SQLITE_FILE;

                LOGGER_DOC = log4net.LogManager.GetLogger("DOC");
                LOGGER_SCREEN = log4net.LogManager.GetLogger("SCREEN");
                LOGGER_MAIN = log4net.LogManager.GetLogger("MAIN");
                log4net.Config.BasicConfigurator.Configure();

                CONF_MANAGER = GTF_ConfigManager.Instance(LOGGER_MAIN);
                //Boolean AccessExists = true;
                //string Migration_Flag = string.Empty;
                if (!CONF_MANAGER.loadAppConfig(PATH_CONFIG_FILE))
                {
                    MessageBox.Show(cParent, "初期化エラーです。プログラムを再起動してください。", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.ExitThread();
                    Environment.Exit(0);
                    return;
                }
                LDB_MANAGER = GTF_LocalDBManager.Instance(LOGGER_MAIN);
                if (!LDB_MANAGER.dbOpen(PATH_DB_FILE))
                {
                    //AccessExists = false;
                    MessageBox.Show(cParent, "初期化エラーです。プログラムを再起動してください。", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Application.ExitThread();
                    Environment.Exit(0);
                    return;
                }
                //1.SQLite DB 파일이 존재하는지 체크한다.
                /*******************************************************************************************************
                SQLITE_MANAGER = GTF_SQLiteDBManager.Instance(LOGGER_MAIN);
                //if (SQLITE_MANAGER.SQLite_Exists(PATH_SQLITE_FILE))
                if (!SQLITE_MANAGER.dbOpen(PATH_SQLITE_FILE)) MessageBox.Show(cParent, "SQLite DBアクセスに失敗しました！", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //2. Migration Check 테이블의 플래그를 체크한다.
                if (!AccessExists)
                {
                    if (!SQLITE_MANAGER.updateMigration_Flag("Y")) MessageBox.Show(cParent, "Migrationコードの更新に失敗しました！ITチームにお問い合わせください！", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else Migration_Flag = SQLITE_MANAGER.getMigration_Flag();
                if (Migration_Flag.Equals("N"))      //Migration 작업을 시작한다.
                {
                    //JArray에 TML_CONFIG 데이터를 가지고 와서 담아온다.
                    string tablename = string.Empty;
                    string str_FailedTalbeName = string.Empty;
                    for (int i = 0; i < 8; i++)
                    {
                        switch (i)
                        {
                            case 0: tablename = "TML_CONFIG"; break;
                            case 1: tablename = "ID_CREATION_RULE"; break;
                            case 2: tablename = "BIN"; break;
                            //case 3: tablename = "CODE"; break;
                            //case 4: tablename = "ITEM"; break;
                            //case 5: tablename = "MERCHANT"; break;
                            case 3: tablename = "REFUNDSLIP"; break;
                            case 4: tablename = "SALES_GOODS"; break;
                            case 5: tablename = "SLIP_PRINT_DOCS"; break;
                            case 6: tablename = "TAXOFFICE"; break;
                            case 7: tablename = "USERINFO"; break;
                        }
                        if (tablename.Length > 1)
                        {
                            JArray arrTemp = LDB_MANAGER.get_data_AccessDB(tablename);

                            if (!(SQLITE_MANAGER.data_Migration(arrTemp, tablename))) str_FailedTalbeName = tablename;
                            //MessageBox.Show(cParent, tablename + " コピーに失敗しました！", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        }
                    }
                    if (!SQLITE_MANAGER.updateMigration_Flag("Y")) MessageBox.Show(cParent, "Migrationに失敗しました！ITチームにお問い合わせください！", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else MessageBox.Show(cParent, "Migration 成功！!", "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                *****************************************************************************************************************************************/

                TML_ID = LDB_MANAGER.selectConfigData("TML_ID");
                PASSPORT_TYPE = string.Empty.Equals(LDB_MANAGER.selectConfigData("PASSPORT_TYPE")) ? -1 : Int32.Parse(LDB_MANAGER.selectConfigData("PASSPORT_TYPE"));
                PRINTER_TYPE = LDB_MANAGER.selectConfigData("PRINTER_TYPE");
                PRINTER_OPOS_TYPE = LDB_MANAGER.selectConfigData("PRINTER_OPOS_TYPE");
                SLIP_TYPE = LDB_MANAGER.selectConfigData("SLIP_TYPE");

                SYSTEM_LANGUAGE = CONF_MANAGER.getAppValue("DEFAULT_LANG"); //터미널 기본언어 세팅
                IS_DEV = "Y".Equals(CONF_MANAGER.getAppValue("DEV"));       //개발여부
                SERVER_URL = CONF_MANAGER.getAppValue("SERVER_URL");
                SERVER_IP = CONF_MANAGER.getAppValue("SERVER_IP");
                SERVER_PORT = CONF_MANAGER.getAppValue("SERVER_PORT");
                SIGNPAD_USE = LDB_MANAGER.selectConfigData("SIGNPAD_USE");
                RCT_ADD = LDB_MANAGER.selectConfigData("RCT_ADD");
                PRINT_TYPE = LDB_MANAGER.selectConfigData("PRINT_TYPE");
                TAX_TYPE = LDB_MANAGER.selectConfigData("TAX_TYPE");
                //국가별 화면 디스크립트 load
                CONF_MANAGER.loadCustomConfig("ScreenText", PATH_CONFIG + "ScreenText.xml");
                //국가별Message load
                CONF_MANAGER.loadCustomConfig("Message", PATH_CONFIG + "Message.xml");

                Boolean taxCalTypeColumn = Constants.LDB_MANAGER.ExitsTableColumn("SALES_GOODS", "TAX_CAL_TYPE");

                if (!taxCalTypeColumn)
                {
                    Constants.LDB_MANAGER.alterTable("SALES_GOODS", "TAX_CAL_TYPE");
                }

                if (!string.Empty.Equals(LDB_MANAGER.selectConfigData("PRINT_SETTING")))
                {
                    PRINT_SETTING = LDB_MANAGER.selectConfigData("PRINT_SETTING");
                }
                else
                {
                    PRINT_SETTING = "0";    // PRINT_ALL :: 0   EXCEPT_DIGITIZE :: 1
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(cParent, "初期化エラーです。プログラムを再起動してください。\n" + ex.Message, "Config", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.ExitThread();
                Environment.Exit(0);
                return;
            }
        }
        public static string getMessage(string strKey)
        {
           return CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/" + strKey).Replace("\\n", System.Environment.NewLine);
        }

        public static string getScreenText(string strKey)
        {
            return CONF_MANAGER.getCustomValue("ScreenText", Constants.SYSTEM_LANGUAGE + "/" + strKey).Replace("\\n", System.Environment.NewLine);
        }
    }
}
