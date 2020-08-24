using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using ZXing.Rendering;
using System.Windows.Forms;

namespace GTF_Printer.GTF_JP
{
    partial class GTF_JPETRS
    {

        private int a4Width;
        private int a4Height;       
        private int leftMargin = 60;
        public int leftStart = 0;

        public double totalPageNumber = 0;
        public double currentPageNumber = 0;

        StringFormat strFormat = new StringFormat();
        StringFormat strSubFormat = new StringFormat();
        StringFormat numFormat = new StringFormat();
        StringFormat leftFormat = new StringFormat();

        private int total_comm_unit_amt = 0;
        private int total_excomm_unit_amt = 0;

        private int total_comm_price_amt = 0;
        private int total_excomm_price_amt = 0;

        //public int PrintA4Recipt()
        public int PrintA4Recipt(string docid, string retailer, string goods, string tourist, string adsinfo, string signinfo = null)
        {
            int nRet = 0;
            
            m_docid = docid;
            m_retailer = retailer;
            m_tourist = tourist;
            m_goods = goods;
            m_adsinfo = adsinfo;
            m_sign = signinfo;
            setClearParseMap();
            
            Renderer = typeof(BitmapRenderer);

            PrinterSettings a4PrinterSettings = new PrinterSettings();
            PageSettings a4PageSettings = new PageSettings();

            try
            {
                
                if (!ParseParam(docid, retailer, tourist, goods, adsinfo))
                {
                    nRet = -1;
                    return nRet;
                }
                

                PrintDocument printDoc = new PrintDocument();

                printDoc.PrintPage += new PrintPageEventHandler(DrawReciptA4);
                //printDoc.PrintPage += new PrintPageEventHandler(DrawReciptImage);

                IEnumerable<PaperSize> paperSizes = a4PrinterSettings.PaperSizes.Cast<PaperSize>();
                PaperSize sizeA4 = paperSizes.First<PaperSize>(size => size.Kind == PaperKind.A4); // setting paper size to A4 size
                printDoc.DefaultPageSettings.PaperSize = sizeA4;

                a4PageSettings.Landscape = false;

                // 1188 * 880
                this.a4Width = sizeA4.Height;
                this.a4Height = sizeA4.Width;

                printDoc.DefaultPageSettings = a4PageSettings;
                if (printer_name != null && !string.Empty.Equals(printer_name.Trim()))
                {
                    printDoc.PrinterSettings.PrinterName = printer_name;
                }
                printDoc.Print(); // PRINT
                printDoc.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                nRet = -1;
            }

            return nRet;
        }

        public void DrawReciptImage(object sender, PrintPageEventArgs e)
        {
            try
            {

                float yPos = 0;

                yPos = 45;

                PrintReceiptImageInfo(e, ref yPos);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceiptImageInfo(PrintPageEventArgs e, ref float yPos)
        {
            try
            {
                Bitmap jpn_logo = (Bitmap)Properties.Resources.jpn_logo;
                e.Graphics.DrawImage((Image)jpn_logo, new Rectangle(0, (int)yPos, jpn_logo.Width, jpn_logo.Height));
                yPos = jpn_logo.Height;

                Bitmap imgAds = getDataImage(Convert.ToString("iVBORw0KGgoAAAANSUhEUgAAAMgAAACWAQMAAAChElVaAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAGUExURQAAAP///6XZn90AAAAJcEhZcwAACjQAAAo0AWdDenAAAAO7SURBVFjD7dY9bhs5FADgx9AwXQiiSxWC6MVewKUr0YAvkkUuoHQuDHEMAZnSV6Iyha/BQS7AkgEIvn3kjORRQhVJtlp4YBiyPg+H74dPAjx3vcufSmTnxIE5IwC6LhGcqouX8Yw4mWRdGom/LP43pJeoxlVP5DvuZcoCAOpEnPmqslgSdiJWf1FRUyLypafSqCIWhAeQJyJnOpgEHB13bCpzEl8K0XQRTuRy+Qgmr8M7bPRE5IcFwIJuibLDoRwHaW5oUwIxiFcMcip23K7jLUYxkRnAuoRoSRKfCEUeSxxAEtibJIA+5N5J8HeL7k0oKZtPjpdX3yTaj0exIN2DzYt53kl89mYUWos7XhrRCRJxlEDPdxfskNkkj2IvAOyQYcjVUE6PApsbuC9VSYBfVNAHiczdw2XZCkUy197YUbzc27HGXuBMOzyI1bumZLO08FLvaUV8zQIoGpuFCq3T0nQ4V8jp7cijKuJzR8U706a5ilm8DJqEl77JItNKhSK0GeaA5wp9UOHRyLhWrojy+OwoVDBurvwmqbBVts3C+yy5onalvI/aJ8WyBHjAJj/DIGxVT+LSSkiSBBd66ILEkur7YPZpIWWJFEaJPOmOZJdu16pkB3Re7VHT8da73qOIN9siCVhLNz1R8qNuv/koI6PuL5Kvq7VqTNSy80EHPki8HWTJMK5l13sTZDBZwuf8mNnqmiPF+No7Cn6UfxJcA8wpqeFJt/0zeuOHyvXpelPSiZS1tudocZQu3vr7kp6wMa2jhsO+SN/Fv/zHkp7g8MUp6vjuIMx76iy6fY+SohKjdG0QPrCG+trv6CTlQdMexfnSI9Q7tOrNQdo2SOubPFV8SQe9koO8eGV7B28XG+VFet30cSKwUkUk9X7TDXWlTDzS78uDOIRuyDglPoKwuf1JlKL0iMBY/ouipKYbhPpuT5IPXQQqADZY5kuRXbyTzYNVHuiApByXKbJWPNBjW9r33RXoPFsGiVmeaELQcaUf7WmlMMpK+ARCptz1c2VNHriDLIVLQL1nVYLt/PABRbJdiOeYe2+YM+IoYbvIyzzpUdRE7hpeem94w0zkFkQUYXynw6kwmvwef7iyXNNZRleRpGYkfVWu6On7n4VOZd7PriYyTyNeFUpIEhVBQf8fZWUHpkx9XZWxVj9LnscWa0LdjKwmGg8fEjUJoiL0tWBHA7QmkgRURSh6G8DUhCFNcqwIVZ6maFUaABBVcW+f/D9IKEOiJlhb6/1b57v8f0WcFf6fCr7LLwrivxwn33rMRPUVAAAAAElFTkSuQmCC"));
                if (imgAds != null)
                {
                    e.Graphics.DrawImage((Image)imgAds, new Rectangle(40, (int)yPos, imgAds.Width, imgAds.Height));
                    yPos += imgAds.Height;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }

        }


        public void DrawReciptA4(object sender, PrintPageEventArgs e)
        {
            try
            {

                float yPos = 0;

                yPos = 30;

                PrintReceiptHeadInfo(e, ref yPos);

                PrintReceiptTaxSellerInfo(e, ref yPos);

                PrintReceiptPassportInfo(e, ref yPos);

                PrintReceiptPassportNameInfo(e, ref yPos);

                PrintReceipGoogsCommInfo(e, ref yPos);

                PrintReceipGoogsEXCommInfo(e, ref yPos);

                PrintReceipGoogsTotalInfo(e, ref yPos);

                PrintA4Line(e, ref yPos);

                PrintReceiptGuidanceInfo(e, ref yPos);

                PrintA4Line(e, ref yPos);

                PrintReceiptExportHeadInfo(e, ref yPos);

                PrintReceiptNoticeSellerNameInfo(e, ref yPos);

                PrintReceiptPassportInfo(e, ref yPos);

                PrintReceiptPassportNameInfo(e, ref yPos);

                PrintReceipGoogsCommInfo(e, ref yPos);

                PrintReceipGoogsEXCommInfo(e, ref yPos);

                PrintReceipGoogsTotalExportInfo(e, ref yPos);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public void PrintReceiptHeadInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            

            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                printFont = new Font("Meiryo", 7);

                Rectangle rect;
                headerHeight = printFont.Height;
                strIdx = 0;

                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle((strIdx+5) , (int)yPos, 100, headerHeight);
                e.Graphics.DrawString("パスポート添付用", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 420;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("輸　出　免　税　物　品　購　入　記　録　票", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);


                printFont = new Font("Meiryo", 5);
                strFormat.Alignment = StringAlignment.Near;

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(String.Format("{0} : {1}",  "伝票番号",MapDocid[DocID.SlipNo]) , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);



                
                strFormat.Alignment = StringAlignment.Center;
                strIdx = 140;
                strIdx += leftStart + leftMargin;
                headerSize = 420;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("Record of Purchase of Consumption Tax-Exempt for Export", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);


                printFont = new Font("Meiryo", 5);
                strFormat.Alignment = StringAlignment.Near;

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("Ref.No.", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics) ;

                strFormat.Alignment = StringAlignment.Near;
                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 700;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("本邦から出国する際又は居住者となる際に、その出港地を所轄する税関長又はその住所若しくは居所の所在地を所轄する税務署長に購入記録票を提出しなければならない", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);
                strFormat.Alignment = StringAlignment.Near;
                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 700;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("When departing Japan, or if becoming a resident of Japan, you are required to submit　your ”Record of Purchase Card” to either the Director of Customs that has jurisdiction", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);
                strFormat.Alignment = StringAlignment.Near;
                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 700;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("over your departure location or the head of the tax office that has jurisdiction over your place of residence or address", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics) + 5 ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceiptTaxSellerInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            SolidBrush sbr = new SolidBrush(Color.LightGray);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize =  140 ;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("所 轄 税 務 署", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140  ;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("納    税    地", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("販売場所在地", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("販売者氏名", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Tax office concerned", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Place for Tax Payment", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Selling Place", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Seller's Name", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics);

                headerHeight = printFont.Height * 2;
                

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(SubstringTaxOffice(MapRetailer[Retailer.TaxOffice]) , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                String strTaxPlace = MapRetailer[Retailer.TaxPlace1] + ' ' + MapRetailer[Retailer.TaxPlace2];
                e.Graphics.DrawString(strTaxPlace, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                String strSellerAddr = MapRetailer[Retailer.SellerAddr1] + ' ' + MapRetailer[Retailer.SellerAddr2];
                SizeF stringSize = new SizeF();
                stringSize = e.Graphics.MeasureString(strSellerAddr, printFont);

                e.Graphics.DrawString(strSellerAddr, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(MapRetailer[Retailer.Seller] , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics) * 2;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceiptPassportInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            SolidBrush sbr = new SolidBrush(Color.LightGray);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {

                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("旅券等の種類", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("番号", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("国籍", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("購入年月日", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("購入者氏名(活字体)", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Passport etc.", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("No.", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Nationality", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Date of Purchase", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Name in Full(in block ｌetters)", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics);

                headerHeight = printFont.Height * 4;

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos +1, headerSize, headerHeight);
                e.Graphics.DrawString(MapTourist[Tourist.PassportType] , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos+1, headerSize, headerHeight);
                e.Graphics.DrawString(MapTourist[Tourist.PassportNo] , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos + 1, headerSize, (headerHeight - (printFont.Height * 2)));
                e.Graphics.DrawString(MapTourist[Tourist.National] , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos + 1, headerSize, (headerHeight - (printFont.Height * 2)));

                string saleDate = MapGoods[Goods.SaleDate].ToString().Substring(5, 2) + " 月 " + MapGoods[Goods.SaleDate].ToString().Substring(8, 2) + " 日 " + MapGoods[Goods.SaleDate].ToString().Substring(0, 4) + " 年" + Environment.NewLine; ;
                saleDate += " Month  Date  Year " + Environment.NewLine; ;

                e.Graphics.DrawString(saleDate, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos+1, headerSize, headerHeight );
                e.Graphics.DrawString(MapTourist[Tourist.Name], printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics) * 4;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public void PrintReceiptPassportNameInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            SolidBrush sbr = new SolidBrush(Color.LightGray);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("上陸年月日", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, ((int)yPos - (headerHeight * 2 )), headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("在留資格", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);



                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("生年月日", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Date of Landing", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, ((int)yPos - printFont.Height * 2 ), headerSize , headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Status of Residence", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Date of Birth of Purchaser", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics);

                headerHeight = printFont.Height * 2;

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                string LandingDate = MapTourist[Tourist.LandingDate].Substring(5, 2) + " 月 " + MapTourist[Tourist.LandingDate].Substring(8, 2) + " 日 " + MapTourist[Tourist.LandingDate].Substring(0, 4) + "年 " + Environment.NewLine; ;
                LandingDate += " Month  Date  Year " + Environment.NewLine; ;

                e.Graphics.DrawString(LandingDate, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, ((int)yPos - headerHeight), headerSize, (headerHeight * 2));
                e.Graphics.DrawString(MapTourist[Tourist.Residence] , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight );
                string name_brithday =  MapTourist[Tourist.Birth].Substring(5, 2) + " 月 "+ MapTourist[Tourist.Birth].Substring(8, 2) + " 日 " + MapTourist[Tourist.Birth].Substring(0, 4) + " 年 "  + Environment.NewLine; ; 
                name_brithday += " Month  Date  Year " + Environment.NewLine; ;

                e.Graphics.DrawString(name_brithday, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics) * 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public void PrintReceipGoogsCommInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;
            total_comm_unit_amt = 0;
            total_comm_price_amt = 0;

            SolidBrush sbr = new SolidBrush(Color.LightYellow);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("消耗品／Consumable Commodities", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("単価", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("数量", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("販売価額", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("品名／Name of  Commodity", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Unit Price", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Quantity", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Price", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics);

                headerHeight = printFont.Height ;
                int comm_cnt = 0;
                for (int i = 0; i < Convert.ToInt32(MapGoods[Goods.ItemTypeSeq]); i++)
                {
                    string ItemMapKey = String.Format("ItemsMap_{0}", i);
                    Dictionary<string, object> ItemMap = (Dictionary<string, object>)MapGoods[ItemMapKey];

                    if (ItemMap[Goods.ItemTypeCode].Equals("01"))
                    {
                        for (int j = 0; j < Convert.ToInt32(ItemMap[Goods.GoodsSeq]); j++)
                        {

                            string GoodsMapKey = String.Format("GoodsMap_{0}", j);
                            Dictionary<string, object> GoodsMap = (Dictionary<string, object>)ItemMap[GoodsMapKey];

                            comm_cnt++;

                            strIdx = 0;
                            strIdx = leftStart + leftMargin;
                            headerSize = 280;
                            endIdx = strIdx + headerSize;
                            strFormat.Alignment = StringAlignment.Center;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            e.Graphics.DrawString((string)GoodsMap[Goods.Name] , printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);


                            strIdx = endIdx;
                            headerSize = 140;
                            endIdx = strIdx + headerSize;
                            strFormat.Alignment = StringAlignment.Far;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.UnitPrice]).ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);
                            total_comm_unit_amt += Convert.ToInt32(GoodsMap[Goods.UnitPrice]);

                            strIdx = endIdx;
                            headerSize = 140;
                            endIdx = strIdx + headerSize;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.Qty]).ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);

                            strIdx = endIdx;
                            headerSize = 140;
                            endIdx = strIdx + headerSize;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.Amt]).ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);
                            total_comm_price_amt += Convert.ToInt32(GoodsMap[Goods.Amt]);


                            yPos += printFont.GetHeight(e.Graphics);
                        }
                    }

                    
                }

                for (int f= comm_cnt ; f<=5; f++)
                {
                    strIdx = 0;
                    strIdx = leftStart + leftMargin;
                    headerSize = 280;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);


                    strIdx = endIdx;
                    headerSize = 140;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);


                    strIdx = endIdx;
                    headerSize = 140;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    strIdx = endIdx;
                    headerSize = 140;
                    endIdx = strIdx + headerSize;
                    strFormat.Alignment = StringAlignment.Far;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("0", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    yPos += printFont.GetHeight(e.Graphics);
                }

                


                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                strFormat.Alignment = StringAlignment.Center;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("消耗品合計金額／Total Amount", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(String.Format("{0,13}", total_comm_unit_amt.ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Center;
                e.Graphics.DrawString("-", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(String.Format("{0,13}", total_comm_price_amt.ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceipGoogsEXCommInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;
            total_excomm_unit_amt = 0;
            total_excomm_price_amt = 0;

            SolidBrush sbr = new SolidBrush(Color.LightYellow);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("一般物品／Non Consumable Commodities", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("単価", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("数量", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("販売価額", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("品名／Name of  Commodity", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Unit Price", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Quantity", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Price", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics);

                headerHeight = printFont.Height;
                int excomm_cnt = 0;
                for (int i = 0; i < Convert.ToInt32(MapGoods[Goods.ItemTypeSeq]); i++)
                {
                    string ItemMapKey = String.Format("ItemsMap_{0}", i);
                    Dictionary<string, object> ItemMap = (Dictionary<string, object>)MapGoods[ItemMapKey];



                    if (ItemMap[Goods.ItemTypeCode].Equals("02"))
                    {
                        for (int j = 0; j < Convert.ToInt32(ItemMap[Goods.GoodsSeq]); j++)
                        {
                        
                            string GoodsMapKey = String.Format("GoodsMap_{0}", j);
                            Dictionary<string, object> GoodsMap = (Dictionary<string, object>)ItemMap[GoodsMapKey];

                            excomm_cnt++;

                            strIdx = 0;
                            strIdx = leftStart + leftMargin;
                            headerSize = 280;
                            endIdx = strIdx + headerSize;
                            strFormat.Alignment = StringAlignment.Center;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            e.Graphics.DrawString((string)GoodsMap[Goods.Name], printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);


                            strIdx = endIdx;
                            headerSize = 140;
                            endIdx = strIdx + headerSize;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.UnitPrice]).ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);
                            total_excomm_unit_amt += Convert.ToInt32(GoodsMap[Goods.UnitPrice]);
                        

                            strIdx = endIdx;
                            headerSize = 140;
                            endIdx = strIdx + headerSize;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.Qty]).ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);

                            strIdx = endIdx;
                            headerSize = 140;
                            endIdx = strIdx + headerSize;
                            rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(String.Format("{0}", Convert.ToInt32(GoodsMap[Goods.Amt]).ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                            e.Graphics.DrawRectangle(Pens.Black, rect);
                            total_excomm_price_amt += Convert.ToInt32(GoodsMap[Goods.Amt]);

                            yPos += printFont.GetHeight(e.Graphics);
                        }
                    }


                }
                for (int f = excomm_cnt; f <= 5; f++)
                {

                
                    strIdx = 0;
                    strIdx = leftStart + leftMargin;
                    headerSize = 280;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);


                    strIdx = endIdx;
                    headerSize = 140;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);


                    strIdx = endIdx;
                    headerSize = 140;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    e.Graphics.DrawString("", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    strIdx = endIdx;
                    headerSize = 140;
                    endIdx = strIdx + headerSize;
                    rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString("0", printFont, Brushes.Black, rect, strFormat);
                    e.Graphics.DrawRectangle(Pens.Black, rect);

                    yPos += printFont.GetHeight(e.Graphics);

                }

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Center;
                e.Graphics.DrawString("一般物品合計金額／Total Amount", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(String.Format("{0}", total_excomm_unit_amt.ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Center;
                e.Graphics.DrawString("-", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(String.Format("{0}", total_excomm_price_amt.ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceipGoogsTotalInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            SolidBrush sbr = new SolidBrush(Color.LightGray);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Center;
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("総合計金額／Total Amount", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("-", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("-", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                int total_amt = total_comm_price_amt + total_excomm_price_amt;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(String.Format("{0}", total_amt.ToString("#,##0")) , printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics) * 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }



        public void PrintA4Line(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            strIdx = leftStart + leftMargin;
            try
            {
                e.Graphics.DrawLine(Pens.Black, strIdx , (int)yPos , strIdx + 700, ((float)(yPos + 0.5)));
                yPos += printFont.GetHeight(e.Graphics);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void PrintReceiptGuidanceInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;



            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                printFont = new Font("Meiryo", 5);

                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc01, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc02, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc03, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);
                strFormat.LineAlignment = StringAlignment.Near;

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, (headerHeight * 2));
                e.Graphics.DrawString(Properties.Resources.StringSlipType01Desc04, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics) * 3;
                strFormat.LineAlignment = StringAlignment.Center;
                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc01), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc02), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc03), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);
                strFormat.LineAlignment = StringAlignment.Near;
                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 720;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight * 3);
                e.Graphics.DrawString(getLangString(Properties.ResourceString.SlipType01Desc04), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics) * 4;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceiptExportHeadInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;



            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {

                printFont = new Font("Meiryo", 7);

                Rectangle rect;
                headerHeight = printFont.Height;
                strIdx = 0;

                strIdx = leftStart + leftMargin;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle((strIdx + 5), (int)yPos, 100, headerHeight);
                e.Graphics.DrawString("店舗控用", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 420;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("最終的に輸出となる物品の消費税免税購入についての購入者誓約書", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);


                printFont = new Font("Meiryo", 5);
                strFormat.Alignment = StringAlignment.Near;

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString(String.Format("{0} : {1}", "伝票番号", MapDocid[DocID.SlipNo]), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics);




                strFormat.Alignment = StringAlignment.Center;
                strIdx = 140;
                strIdx += leftStart + leftMargin;
                headerSize = 420;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("Convenant of Purchaser of Consumption Tax-Exempt of Ultimate Export", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);


                printFont = new Font("Meiryo", 5);
                strFormat.Alignment = StringAlignment.Near;

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("Ref.No.", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.White, rect);

                yPos += printFont.GetHeight(e.Graphics) * 2 ;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public void PrintReceiptNoticeSellerNameInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            SolidBrush sbr = new SolidBrush(Color.LightGray);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                // 전자서명이 있는경우 와 없는경우 공간 확보
                if (m_sign != "")
                {
                    Bitmap imgSign = getDataImage(Convert.ToString(m_sign));
                    if (imgSign != null)
                    {
                        int sign_start = leftStart + leftMargin + 280;
                        rect = new Rectangle(sign_start, ((int)yPos + headerHeight * 6), imgSign.Width, imgSign.Height);
                        e.Graphics.DrawImage((Image)imgSign, rect);
                    }

                }


                string notice = "";
                notice += "   ・当該消耗品を、購入した日から30日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。" + Environment.NewLine ;
                notice += "   I certify that the goods listed as “consumable commodities ” on this card were purchased by me for export from Japan within 30days from the purchase date and will not be disposed of within Japan." + Environment.NewLine;
                notice += "   ・当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。" + Environment.NewLine ;
                notice += "   I certify that the goods listed as “commodities except consumables ” on this card were purchased by me for ultimate export from Japan and will not be disposed of within Japan." + Environment.NewLine ;
                notice += "                                                                                署名" + Environment.NewLine;
                notice += "                                                                                Signature" + Environment.NewLine;
                strIdx = leftStart + leftMargin;
                headerSize = 560;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, (headerHeight* 11)+3);
                e.Graphics.DrawString(notice, printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);
                
                strFormat.Alignment = StringAlignment.Center;
                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight*5 +1);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("販売者氏名", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics) * 5;

                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 140;
                rect = new Rectangle(strIdx + 560, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("Seller's Name", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                yPos += printFont.GetHeight(e.Graphics);

                headerHeight = printFont.Height * 5;


                strIdx = 0;
                strIdx = leftStart + leftMargin;
                headerSize = 140;
                rect = new Rectangle(strIdx + 560, (int)yPos, headerSize, headerHeight + 1);
                e.Graphics.DrawString(MapRetailer[Retailer.Seller], printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics) * 5;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public void PrintReceipGoogsTotalExportInfo(PrintPageEventArgs e, ref float yPos)
        {
            int strIdx = 0;
            int endIdx = 0;
            int headerSize = 0;
            int headerHeight = 0;

            SolidBrush sbr = new SolidBrush(Color.LightGray);
            printFont = new Font("Meiryo", 5);
            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            try
            {
                Rectangle rect;
                headerHeight = printFont.Height;

                strIdx = leftStart + leftMargin;
                headerSize = 280;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.FillRectangle(sbr, rect);
                e.Graphics.DrawString("総合計金額／Total Amount", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("-", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);


                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                e.Graphics.DrawString("-", printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                strIdx = endIdx;
                headerSize = 140;
                endIdx = strIdx + headerSize;
                rect = new Rectangle(strIdx, (int)yPos, headerSize, headerHeight);
                strFormat.Alignment = StringAlignment.Far;
                int total_amt = total_comm_price_amt + total_excomm_price_amt;
                e.Graphics.DrawString(String.Format("{0}", total_amt.ToString("#,##0")), printFont, Brushes.Black, rect, strFormat);
                e.Graphics.DrawRectangle(Pens.Black, rect);

                yPos += printFont.GetHeight(e.Graphics);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}