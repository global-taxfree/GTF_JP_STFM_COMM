using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

using System.Drawing;
using System.Resources;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using System.Reflection;
using System.Globalization;
using System.Threading;

namespace GTF_Printer.GTF_JP
{
    partial class GTF_JPETRS
    {

        public int JPNPrintTicket(string docid, string retailer, string tourist, string adsinfo, string signinfo = null)
        {
            m_docid = docid;
            m_retailer = retailer;
            m_tourist = tourist;
            m_adsinfo = adsinfo;
            m_sign = signinfo;
            setClearParseMap();

            int nRet = 0;

            try
            {
                if (!ParseParam(docid, retailer, tourist, adsinfo))
                {
                    nRet = -1;
                    return nRet;
                }

                List<int> PublishTypeList = (List<int>)MapDocid[DocID.PublishType];
                foreach (int Type in PublishTypeList)
                {
                    SlipType = Type;
                    UserPrintDocument printDocObj = new UserPrintDocument();
                    printDocObj.UserPrintPageEvent += new UserPrintDocument.UserPrintPageEventHandler(eventPrintDocNG);
                    if (printer_name != null && !string.Empty.Equals(printer_name.Trim()))
                    {
                        printDocObj.PrinterSettings.PrinterName = printer_name;
                    }

                    printDocObj.Print();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(null, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nRet = -1;
            }

            return nRet;
        }

        public void eventPrintDocNG(object sender, PrintPageEventArgs e)
        {
            float yPos = 0;


            PrintHeader(e, ref yPos, SlipType);

            PrintRetailer(e, ref yPos, SlipType);

            PrintRefundDetails(e, ref yPos);

            PrintTouristDetails(e, ref yPos);

            PrintAddReceiptSpace(e, ref yPos, SlipType);

            PrintFooter(e, ref yPos, SlipType);

            e.HasMorePages = false;
        }
    }
}
