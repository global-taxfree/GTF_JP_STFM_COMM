using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;
using log4net;
using OposPOSPrinter_CCO;
using System.Windows.Forms;
using GTF_Printer.GTF_JP;

namespace GTF_Printer
{
    public class GTF_ReceiptPrinter
    {
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

        OposPOSPrinter_CCO.OPOSPOSPrinter axOPOSPOSPrinter1 = null;
        
        ILog m_logger = null;

        PrintDocument m_printDoc = null;
        Control m_parent = null;
        string m_PrinterName = "";

        //생성자
        public GTF_ReceiptPrinter(ILog logger = null, Control parent =null)
        {
            m_printDoc = new PrintDocument();
            m_logger = logger;
            m_parent = parent;
            if (m_logger == null)
                m_logger = LogManager.GetLogger("");
        }

        public void setPrinter(string strPrinterName)
        {
            m_PrinterName = strPrinterName;
            m_printDoc.PrinterSettings.PrinterName = strPrinterName;
        }

        public void PrintSlip_jp()
        {
            
        }

        public void PrintSlip_sg(string strPrinterName = "SRP-350III")
        {
            string CRLF = "\r\n";
            string ESC = "\x1b";
            string strOutputData;
            strOutputData = ESC + "|cA" + ESC + "|2C" + ESC + "|bC" + "* Cafe Blue *" + CRLF + CRLF;
            strOutputData = strOutputData + ESC + "|N" + "   3000 Spring Street, Rancho," + CRLF;
            strOutputData = strOutputData + "   California 10093," + CRLF;
            strOutputData = strOutputData + "   Tel) 858-519-3698 Fax) 3852" + CRLF + CRLF;
            strOutputData = strOutputData + "Orange Juice                   5.00" + CRLF;
            strOutputData = strOutputData + "6 Bufalo Wing                 24.00" + CRLF;
            strOutputData = strOutputData + "Potato Skin                   12.00" + CRLF;
            strOutputData = strOutputData + ESC + "|bC" + ESC + "|2rC" + "Subtotal                      41.00" + CRLF + CRLF;
            strOutputData = strOutputData + ESC + "|N" + "Tax 6%                         2.46" + CRLF;
            strOutputData = strOutputData + ESC + "|bC" + ESC + "|2rC" + "Member Discount                2.30" + CRLF + CRLF;
            strOutputData = strOutputData + ESC + "|N" + ESC + "|bC" + "Cash                         100.00" + CRLF;
            strOutputData = strOutputData + ESC + "|N" + "Amt. Paid                     41.16" + CRLF;
            strOutputData = strOutputData + ESC + "|bC" + ESC + "|2rC" + "Change Due                    58.84" + CRLF + CRLF;
            strOutputData = strOutputData + ESC + "|N" + "Member Number : 452331949" + CRLF + CRLF;
            strOutputData = strOutputData + ESC + "|bC" + ESC + "|cA" + "Have a nice day !" + CRLF + CRLF + CRLF;
            strOutputData = strOutputData + ESC + "|N" + ESC + "|cA" + "Sale Date : 2017/06/02" + CRLF;
            strOutputData = strOutputData + ESC + "|N" + ESC + "|cA" + "Time : 11:23:45" + CRLF + CRLF + CRLF + CRLF + CRLF + CRLF + CRLF + CRLF + CRLF + CRLF;
            PrintOPOS(strPrinterName, strOutputData);
        }

        public void PrintSlip_kr()
        {

        }

        public void PrintSlip_ja(string docid, string retailer, string goods, string tourist, string adsinfo, Boolean bPreview = true, string signInfo = "", string print_goods_type = "0")
        {
            GTF_JPETRS dd = new GTF_JPETRS();
            dd.printer_name = m_PrinterName;
            if (bPreview)
            {
                dd.JPNPrintPreview(docid, retailer, goods, tourist, adsinfo, signInfo);
            }
            else
            {
                dd.JPNPrintTicket(docid, retailer, goods, tourist, adsinfo, signInfo, print_goods_type);
            }
            dd = null;
        }

        public void PrintSlipNoGoods_ja(string docid, string retailer, string tourist, string adsinfo, Boolean bPreview = true, string signInfo = "" )
        {
            GTF_JPETRS dd = new GTF_JPETRS();
            dd.printer_name = m_PrinterName;
            if (bPreview)
            {
                dd.JPNPrintPreview(docid, retailer,  tourist, adsinfo, signInfo);
            }
            else
            {
                dd.JPNPrintTicket(docid, retailer, tourist, adsinfo, signInfo);
            }
            dd = null;
        }


        public void PrintSlip_ja_summary(string total_slip_seq, string total_sum_amt, string total_tax_amt, string total_fee_amt, string total_refund_amt)
        {
            GTF_JPETRS dd = new GTF_JPETRS();
            dd.printer_name = m_PrinterName;
            dd.JPNPrintSummaryTicket(total_slip_seq, total_sum_amt, total_tax_amt, total_fee_amt, total_refund_amt);
            dd = null;
        }

        public void PrintSlip_Test()
        {
            m_printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            m_printDoc.Print();
        }

        private void printDoc_PrintPage(Object sender, PrintPageEventArgs e)
        {
            StringBuilder sbToPrint = new StringBuilder();
            sbToPrint.Append("GTF STFM Test Printing 1\n");
            sbToPrint.Append("GTF STFM Test Printing 2\n");
            sbToPrint.Append("GTF STFM Test Printing 3\n");
            sbToPrint.Append("GTF STFM Test Printing 4\n");
            Font printFont = new Font("Courier New", 6);
            e.Graphics.DrawString(sbToPrint.ToString(), printFont, Brushes.Black, 0, 0);
        }

        public Boolean PrintOPOS(string strPrinterName, string strData)
        {
            Boolean bRet = true;
            string strErr = "";
            try
            {
                if (axOPOSPOSPrinter1 == null)
                    axOPOSPOSPrinter1 = new OposPOSPrinter_CCO.OPOSPOSPrinter();
                int lRet = axOPOSPOSPrinter1.Open(strPrinterName); // LDN

                if (lRet == OPOS_SUCCESS)
                {
                    lRet = axOPOSPOSPrinter1.ClaimDevice(0);
                    if (lRet == OPOS_SUCCESS)
                    {
                        axOPOSPOSPrinter1.DeviceEnabled = true;
                        axOPOSPOSPrinter1.FlagWhenIdle = true;

                        string strCharLists = axOPOSPOSPrinter1.RecBarCodeRotationList;
                        //StrCharLists is pair (Font A, Font B)//F312 48,64
                        //m_ctlPosPrinter1.SetRecLineChars(48); //select a Font A
                        axOPOSPOSPrinter1.RecLineChars = 64; //select a Font B

                        axOPOSPOSPrinter1.AsyncMode = false;
                        axOPOSPOSPrinter1.PrintNormal(PTR_S_RECEIPT, strData);

                        axOPOSPOSPrinter1.CutPaper(95);
                    }
                    else {
                        strErr = "OPOSPrinter Claim Error : " + axOPOSPOSPrinter1.ErrorString;
                        //AfxMessageBox(strErr);
                        // return FALSE;
                    }
                    axOPOSPOSPrinter1.Close();
                }
                else {
                    strErr = "OPOSPrinter Open Error : " + axOPOSPOSPrinter1.ErrorString;
                    // AfxMessageBox(strErr);
                    //return FALSE;
                }
            }
            catch (Exception e)
            {
                if (axOPOSPOSPrinter1 != null)
                    strErr = "OPOSPrinter Open Error : " + axOPOSPOSPrinter1.ErrorString;
                else
                    strErr = "OPOSPrinter is null Error :" + e.Message;
            }
            return bRet;
        }


        public int JPNPrintTicket(string docid, string retailer, string tourist, string goods, string adsinfo)
        {
            return 0;
        }

        public int A4PrintTicket(string docid, string retailer, string goods, string tourist, string adsinfo, string signInfo = "")
        {
            GTF_JPETRS dd = new GTF_JPETRS();
            dd.printer_name = m_PrinterName;
            dd.PrintA4Recipt(docid,  retailer,  goods,  tourist,  adsinfo, signInfo);
            dd = null;
            return 0;
        }
        public int A4PrintTicket()
        {
            GTF_JPETRS dd = new GTF_JPETRS();
            dd.printer_name = m_PrinterName;
            //dd.PrintA4Recipt();
            dd = null;
            return 0;
        }

    }

    
}
