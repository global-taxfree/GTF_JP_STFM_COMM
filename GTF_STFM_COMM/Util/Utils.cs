using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GTF_STFM_COMM.Util
{
    public class Utils
    {
        public class ComboItem
        {
            //public ComboItem(string value, string text, string parent = "") { Value = value; Text = text; Parent = parent; }
            public ComboItem(string value, string text) { Value = value; Text = text;  }
            public string Value { get; set; }
            public string Text { get; set; }
            //public string Parent { get; set; }
            public override string ToString() { return Text; }
        }


        public void createSlipNo(long nSeq, string strTermNo, out string strSlipNo)
        {
            strSlipNo = "2" + strTermNo.PadLeft(5, '0') + DateTime.Now.ToString("yyMMddHHmmss") + (nSeq % 100).ToString("D2");        
        }

        public string getFullDate(string strBirth)
        {
            string strRet = "20000101";
            int nBirth = Int32.Parse(strBirth.Substring(0,2));
            if((nBirth+2000) >(Int32.Parse(DateTime.Now.ToString("yyyy"))-10))
            {
                strRet = "19" + strBirth;
            }
            else
            {
                strRet = "20" + strBirth;
            }
            return strRet;
        }

        public Bitmap MakeGrayscale(Bitmap original)
        {
            //create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            //get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            //create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][]
               {
         new float[] {.3f, .3f, .3f, 0, 0},
         new float[] {.59f, .59f, .59f, 0, 0},
         new float[] {.11f, .11f, .11f, 0, 0},
         new float[] {0, 0, 0, 1, 0},
         new float[] {0, 0, 0, 0, 1}
               });

            //create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            //set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            //draw the original image on the new image
            //using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }

        public ArrayList GetFixOrderCountryList()
        {
            ArrayList temp = new ArrayList();
            temp.Add("CHN");
            temp.Add("KOR");
            temp.Add("TWN");
            temp.Add("THA");
            temp.Add("SGP");
            temp.Add("USA");
            temp.Add("JPN");
            temp.Add("PHL");
            temp.Add("MYS");
            temp.Add("NPL");
            temp.Add("DEU");
            temp.Add("ISL");
            temp.Add("VNM");
            temp.Add("MNG");
            return temp;
        }

        public Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        public string FormatConvertDate(string value)
        {
            string result = "";
            
            result = value.Replace("-", "/");
            result = result.Substring(0, 10);
            
            return result;
        }
    }
}
