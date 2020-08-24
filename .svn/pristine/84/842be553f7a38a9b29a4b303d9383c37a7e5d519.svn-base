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
namespace GTF_STFM_COMM.Screen
{
    public partial class MessageSmallForm : MetroFramework.Forms.MetroForm
    {
        ILog m_Logger = null;

        public MessageBoxButtons m_MessageType { get; set;}

        public MessageSmallForm(String strMessage , ILog Logger = null)
        {
            InitializeComponent();
            m_Logger = Logger;
            LBL_MSG.Text = strMessage;
        }

        private void MessageForm_Load(object sender, EventArgs e)
        {
        }

        private void BTN_NO_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void MessageForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
