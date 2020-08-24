using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Printing;

namespace GTF_Printer
{
    class UserPrintDocument : PrintDocument
    {
        //public delegate void UserPrintPageEventHandler(object sender, PrintPageEventArgs e, object docid, object retailer, object tourist, object goods, object adsinfo);
        public delegate void UserPrintPageEventHandler(object sender, PrintPageEventArgs e);
        public event UserPrintPageEventHandler UserPrintPageEvent;

        public UserPrintDocument()
        {
            // Added by AsCarion [2015.01.26]
            // Print Page Size Set.
            // Paper Size : 80mm X 297 mm (302px X 1122px) 에서 길이만 3배로 늘린다.
            //PageSettings pa = new PageSettings();
            //pa.Margins = new Margins(0, 0, 0, 0);
            //this.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("roll", 284, 3000);
            //this.DefaultPageSettings.Margins = pa.Margins;
        }

        protected override void OnPrintPage(PrintPageEventArgs args)
        {
            // raise my version of PrintPageEventHandler with added m_context            
            this.UserPrintPageEvent(this, args);
        }
    }
}
