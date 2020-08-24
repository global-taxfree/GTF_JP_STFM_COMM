using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using Florentis;
using System.IO;
using GTF_STFM_COMM.Util;

namespace GTF_STFM_COMM.Screen
{
    public partial class SigCap : MetroFramework.Forms.MetroForm
    {
        ILog m_Logger = null;
        public string name  = "";
        public string clause = "";
        public string signdata  = "";
        Image signImage;
        public SigCap(ILog Logger = null)
        {
            m_Logger = Logger;
            if (m_Logger == null)
                m_Logger = LogManager.GetLogger("");
            InitializeComponent();
            signdata = "";
            this.DialogResult = DialogResult.Cancel;
        }

        private void BTN_SIGNREQ_Click(object sender, EventArgs e)
        {
            print("Request Sign");
            SigCtl sigCtl = new SigCtl();
            sigCtl.Licence = "AgAkAEy2cKydAQVXYWNvbQ1TaWduYXR1cmUgU0RLAgKBAgJkAACIAwEDZQA";
            DynamicCapture dc = new DynamicCapture();
            DynamicCaptureResult res = dc.Capture(sigCtl, name, clause, null, null);
            if (res == DynamicCaptureResult.DynCaptOK)
            {
                SigObj sigObj = (SigObj)sigCtl.Signature;
                sigObj.set_ExtraData("AdditionalData", "C# test: Additional data");
                
                try
                {
                    signdata = (string) sigObj.RenderBitmap("", 200, 150, "image/png", 1.4f, 0xff0000, 0xffffff, 10.0f, 10.0f, RBFlags.RenderOutputBase64 | RBFlags.RenderColor1BPP);
                    signImage = Base64ToImage(signdata);
                    PB_SIGIMAGE.Image = signImage;
                    print("Sign Ok");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                print("Signature capture error res=" + (int)res + "  ( " + res + " )");
                switch (res)
                {
                    case DynamicCaptureResult.DynCaptCancel: print("signature cancelled"); break;
                    case DynamicCaptureResult.DynCaptError: print("no capture service available"); break;
                    case DynamicCaptureResult.DynCaptPadError: print("signing device error"); break;
                    default: print("Unexpected error code "); break;
                }
            }
            
        }

        private void BTN_SIGNSAVE_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void SigCap_Load(object sender, EventArgs e)
        {

        }

        private void print(string txt)
        {
            TXT_TXTDISPLAY.Text += txt + "\r\n";
            TXT_TXTDISPLAY.SelectionStart = TXT_TXTDISPLAY.TextLength;
            TXT_TXTDISPLAY.ScrollToCaret();
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

        private void BTN_SIGNCANCEL_Click(object sender, EventArgs e)
        {
            signdata = "";
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
