using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Florentis;
using MetroFramework;
using MetroFramework.Forms;

using GTF_Passport;
using GTF_Printer;
using GTF_STFM_COMM.Util;



namespace GTF_STFM_COMM.Screen
{
    public partial class PreferencesPanel : UserControl
    {
        ControlManager m_CtlSizeManager = null;
        const long OPOS_SUCCESS = 0;
        const long OPOS_E_CLOSED = 101;
        const long OPOS_E_CLAIMED = 102;
        const long OPOS_E_NOTCLAIMED = 103;
        const long OPOS_E_NOSERVICE = 104;
        const long OPOS_E_DISABLED = 105;
        const long OPOS_E_ILLEGAL = 106;
        const long OPOS_E_NOHARDWARE = 107;
        const long OPOS_E_OFFLINE = 108;
        const long OPOS_E_NOEXIST = 109;
        const long OPOS_E_EXISTS = 110;
        const long OPOS_E_FAILURE = 111;
        const long OPOS_E_TIMEOUT = 112;
        const long OPOS_E_BUSY = 113;
        const long OPOS_E_EXTENDED = 114;
        const int PTR_S_RECEIPT = 2;

        public PreferencesPanel(ILog Logger = null)
        {
            InitializeComponent();
            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            m_CtlSizeManager = new ControlManager(this);
            //횡이동
            m_CtlSizeManager.addControlMove(BTN_SAVE, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_DOWNLOAD, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_HELP, true, false, false, false);
            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
        }

        private void metroLabel9_Click(object sender, EventArgs e)
        {

        }

        private void PreferencesPanel_Load(object sender, EventArgs e)
        {
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;

            TXT_TML_ID.Text = Constants.TML_ID;
            //COM_PC_NO.SelectedIndex = (Constants.PC_NO - 1) < 0 ?  0 : (Constants.PC_NO - 1);
            //여권스캐너 설정
            COM_PASS_SCAN.SelectedIndex = Constants.PASSPORT_TYPE < 0 ? 0 : Constants.PASSPORT_TYPE;

            //프린터 설정
            COM_PRINTER.Items.Clear();
            PrinterSettings settings = new PrinterSettings();
            string strDeaultPrinter = "";
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                COM_PRINTER.Items.Add(printer);
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)   // 기본 설정 여부
                    strDeaultPrinter = printer;
            }
            
            if(Constants.PRINTER_TYPE != null && !string.Empty.Equals(Constants.PRINTER_TYPE.Trim()))
            {
                COM_PRINTER.SelectedItem = Constants.PRINTER_TYPE;
            }
            else if (!string.Empty.Equals(strDeaultPrinter))//미설정 시엔 기본프린터 설정
            {
                COM_PRINTER.SelectedItem = strDeaultPrinter;
            }

            //OPOS 프린터 설정
            COM_OPOS.SelectedItem = Constants.PRINTER_OPOS_TYPE;

            //SLIP TYPE 설정
            if (Constants.SLIP_TYPE == null || string.Empty.Equals(Constants.SLIP_TYPE))
            {
                COM_SLIP_TYPE.SelectedIndex = 0;
            }
            else
            {
                COM_SLIP_TYPE.SelectedItem = Constants.SLIP_TYPE;
            }

            if(Constants.SIGNPAD_USE == null || string.Empty.Equals(Constants.SIGNPAD_USE))
            {
                COM_SIGNPAD_USE.SelectedIndex = 0;
            }
            else
            {
                COM_SIGNPAD_USE.SelectedItem = Constants.SIGNPAD_USE;
            }

            if (Constants.PRINT_TYPE == null || string.Empty.Equals(Constants.PRINT_TYPE))
            {
                COM_PRINT_TYPE.SelectedIndex = 0;
            }
            else
            {
                COM_PRINT_TYPE.SelectedItem = Constants.PRINT_TYPE;
            }

            if (Constants.RCT_ADD == null || string.Empty.Equals(Constants.RCT_ADD))
            {
                COM_RCT_ADD.SelectedIndex = 0;
            }
            else
            {
                COM_RCT_ADD.SelectedItem = Constants.RCT_ADD;
            }

            if (Constants.PRINT_SETTING == null || string.Empty.Equals(Constants.PRINT_SETTING))
            {
                COM_PRINT_SETTING.SelectedIndex = 0;
            }
            else
            {
                COM_PRINT_SETTING.SelectedIndex = Convert.ToInt32(Constants.PRINT_SETTING);
            }


        }

        private void BTN_SCAN_TEST_Click(object sender, EventArgs e)
        {
            try
            {
                if(COM_PASS_SCAN.SelectedIndex <0)
                {
                    return;
                }
                setWaitCursor(true);

                GTF_PassportScanner passScan = GTF_PassportScanner.Instance(null, Constants.PATH_TEMP);
                
                int nRet = 0;
                //nRet = passScan.open(COM_PASS_SCAN.SelectedIndex);
                if ("GTF-PS01(GTF)".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(0);
                }
                else if ("NP-1000(OKPOS)".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(3);
                }
                else if ("WISESCAN420".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(1);
                }
                else if ("COMBOSMART(DAWIN)".Equals(COM_PASS_SCAN.SelectedItem))
                {
                    nRet = passScan.open(2);
                }

                if (nRet > 0)
                {
                    Constants.PASSPORT_SCAN_OPEN = true;
                    //COM_PASS_SCAN.Enabled = false; 
                    int strmrz = passScan.scan(30);
                    if (strmrz > 0)
                    {
                        string strTempData = passScan.getMRZ1() + "\n" + passScan.getMRZ2();
                        MetroMessageBox.Show(this, strTempData /*+ "\n"+passScan.GetPassportFirstName()+"\n"+ passScan.GetPassportLastName() +"\n"+ passScan.GetPassportName()*/
                            , "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("PASSPORT_REMOVE"), "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    passScan.close();
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.getMessage("PASSPORT_ERROR"), "Passport Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                setWaitCursor(false);
                BTN_SCAN_TEST.Focus();
            }
        }

        private void BTN_TID_CONFIRM_Click(object sender, EventArgs e)
        {

        }

        private void BTN_PRINT_TEST_Click(object sender, EventArgs e)
        {
            try
            {
                setWaitCursor(true);
                if (COM_PRINTER.SelectedItem == null || string.Empty.Equals(COM_PRINTER.SelectedItem.ToString().Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("PRINTER_NOTHING"), "Print Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else {
                    GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                    printer.setPrinter(COM_PRINTER.SelectedItem.ToString());
                    printer.PrintSlip_Test();
                }
            }
            finally
            {
                setWaitCursor(false);
                BTN_PRINT_TEST.Focus();
            }
            /*
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = COM_PRINTER.SelectedItem.ToString();
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            printDoc.Print();
            */
        }

        private void setWaitCursor(Boolean bWait)
        {
            if (bWait)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.UseWaitCursor = true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                this.UseWaitCursor = false;
            }
        }


        private void axOPOSPOSPrinter1_Enter(object sender, EventArgs e)
        {

        }

        private void PreferencesPanel_SizeChanged(object sender, EventArgs e)
        {
            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
            this.Refresh();
        }

        private void BTN_OPOS_TEST_Click(object sender, EventArgs e)
        {
            try
            {
                setWaitCursor(true);
                if(COM_OPOS.SelectedItem != null && !string.Empty.Equals(COM_OPOS.SelectedItem.ToString()))
                {
                    GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                    printer.PrintSlip_sg(COM_OPOS.SelectedItem.ToString());
                }
            }
            finally
            {
                setWaitCursor(false);
                BTN_OPOS_TEST.Focus();
            }
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                setWaitCursor(true);

                if(TXT_TML_ID.Text == null || string.Empty.Equals(TXT_TML_ID.Text.ToString()) || TXT_TML_ID.Text.ToString().Length < TXT_TML_ID.MaxLength)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("TXT_TML_ID"), "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TXT_TML_ID.Focus();
                    return;
                }

                if(COM_PRINTER.SelectedItem == null || COM_PRINTER.SelectedIndex < 0)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("COM_PRINTER"), "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    COM_PRINTER.Focus();
                    return;
                }
                if (COM_SLIP_TYPE.SelectedIndex < 0)
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", "COM_SLIP_TYPE"), "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    COM_SLIP_TYPE.Focus();
                    setWaitCursor(false);
                    return;
                }

                /*
                if(COM_PASS_SCAN.SelectedIndex <0)
                {
                    MetroMessageBox.Show(this, "여권스캐너를 선택해 주세요.", "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    COM_PASS_SCAN.Focus();
                    return;
                }
                if (COM_PRINTER.SelectedIndex < 0)
                {
                    MetroMessageBox.Show(this, "저장이 완료되었습니다..", "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (COM_OPOS.SelectedIndex < 0)
                {
                    MetroMessageBox.Show(this, "저장이 완료되었습니다..", "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                */
                /*
                if (COM_SLIP_TYPE.SelectedIndex < 0)
                {
                    MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", "COM_SLIP_TYPE"), "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    COM_SLIP_TYPE.Focus();
                    setWaitCursor(false);
                    return;
                }
                */

                //Constants 저장

                //Constants.PC_NO = Int32.Parse(COM_PC_NO.Text.ToString());
                Constants.TML_ID= TXT_TML_ID.Text.ToString();
                Constants.PASSPORT_TYPE = COM_PASS_SCAN.SelectedIndex;
                Constants.PRINTER_TYPE = COM_PRINTER.SelectedItem.ToString();
                Constants.PRINTER_OPOS_TYPE = COM_OPOS.SelectedItem == null? "" :COM_OPOS.SelectedItem.ToString();
                Constants.SIGNPAD_USE = COM_SIGNPAD_USE.SelectedItem.ToString();
                Constants.PRINT_TYPE = COM_PRINT_TYPE.SelectedItem.ToString();
                Constants.RCT_ADD = COM_RCT_ADD.SelectedItem.ToString();
                Constants.SLIP_TYPE = COM_SLIP_TYPE.SelectedItem.ToString();
                Constants.PRINT_SETTING = COM_PRINT_SETTING.SelectedIndex.ToString();

                //LOCAL DB 저장
                Constants.LDB_MANAGER.updateConfigData("TML_ID", "" + Constants.TML_ID, "터미널번호");
                Constants.LDB_MANAGER.updateConfigData("PASSPORT_TYPE", "" + Constants.PASSPORT_TYPE, "여권스캐너");
                Constants.LDB_MANAGER.updateConfigData("PRINTER_TYPE", Constants.PRINTER_TYPE, "영수증프린터");
                Constants.LDB_MANAGER.updateConfigData("PRINTER_OPOS_TYPE", Constants.PRINTER_OPOS_TYPE, "영수증프린터");
                Constants.LDB_MANAGER.updateConfigData("SIGNPAD_USE", Constants.SIGNPAD_USE, "서명패스 사용여부");
                Constants.LDB_MANAGER.updateConfigData("PRINT_TYPE", Constants.PRINT_TYPE, "영수증 출력용지");
                Constants.LDB_MANAGER.updateConfigData("RCT_ADD", Constants.RCT_ADD, "영수증 추가기능");
                Constants.LDB_MANAGER.updateConfigData("SLIP_TYPE", "" + Constants.SLIP_TYPE, "전표타입");
                Constants.LDB_MANAGER.updateConfigData("PRINT_SETTING", Constants.PRINT_SETTING, "전자화관련 프린트설정");
                //MetroMessageBox.Show(this, "저장 완료되었습니다.", "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MetroMessageBox.Show(this, Constants.getMessage("CONFIG_SAVE_SUCCESS"), "Config Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                setWaitCursor(false);
            }
                
        }

        private void COM_SIGNPAD_USE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(COM_SIGNPAD_USE.SelectedItem.ToString() == "YES" )
            {
                BTN_SIGNPAD_CHECK.Enabled = true;
                COM_PRINT_TYPE.Enabled = true;
            }
            else
            {
                BTN_SIGNPAD_CHECK.Enabled = false ;
                COM_PRINT_TYPE.SelectedIndex = 0;
                COM_PRINT_TYPE.Enabled = false;
            }
        }

        private void BTN_SIGNPAD_CHECK_Click(object sender, EventArgs e)
        {
            Boolean signColumn =  Constants.LDB_MANAGER.ExitsTableColumn("SLIP_PRINT_DOCS", "SIGN");

            if(!signColumn)
            {
                Constants.LDB_MANAGER.alterTable("SLIP_PRINT_DOCS", "SIGN");
            }


            SigCtl sigCtl = new SigCtl();
            sigCtl.InkColor = 0x000000;
            sigCtl.Licence = "AgAkAEy2cKydAQVXYWNvbQ1TaWduYXR1cmUgU0RLAgKBAgJkAACIAwEDZQA";
            DynamicCapture dc = new DynamicCapture();
            DynamicCaptureResult res = dc.Capture(sigCtl, "TEST SIGN", "SIGN TEST", null, null);
            if (res == DynamicCaptureResult.DynCaptOK)
            {
                SigObj sigObj = (SigObj)sigCtl.Signature;
                sigObj.set_ExtraData("AdditionalData", "C# test: Additional data");
            }
            else
            {
                switch (res)
                {
                    case DynamicCaptureResult.DynCaptCancel:
                        MessageBox.Show("signature cancelled");
                        break;
                    case DynamicCaptureResult.DynCaptError:
                        MessageBox.Show("no capture service available");
                        break;
                    case DynamicCaptureResult.DynCaptPadError:
                        MessageBox.Show("signing device error");
                        break;
                    default:
                        MessageBox.Show("Unexpected error code ");
                        break;
                }
            }
        }

        private void BTN_A4PRINT_TEST_Click(object sender, EventArgs e)
        {
            /*
            GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
            printer.setPrinter(Constants.PRINTER_TYPE);
            printer.A4PrintTicket();
            */
        }
    }
}
