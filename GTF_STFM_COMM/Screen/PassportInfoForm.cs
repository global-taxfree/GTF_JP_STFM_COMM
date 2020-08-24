using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using GTF_STFM_COMM.Util;
using GTF_STFM_COMM.Tran;
using log4net;
using Newtonsoft.Json.Linq;
using MetroFramework;

namespace GTF_STFM_COMM.Screen
{
    public partial class PassportInfoForm : MetroFramework.Forms.MetroForm
    {
        private ILog m_logger=null;
        //public string m_strRetVal = string.Empty;
        public PassportInfoForm(ILog Logger = null, JArray ArrNationalList =null)
        {
            InitializeComponent();
            m_logger = Logger;
            if (ArrNationalList != null)
                init(ArrNationalList);
        }
        public void init(JArray ArrNationalList)
        {
            COM_PASSPORT_NAT.Items.Clear();

            if (ArrNationalList != null && ArrNationalList.Count > 0)
            {
                Dictionary<string, string> item_list = new Dictionary<string, string>();
                for (int i = 0; i < ArrNationalList.Count; i++)
                {
                    JObject tempObj = (JObject)ArrNationalList[i];
                    IList<string> keys = tempObj.Properties().Select(p => p.Name).ToList();
                    for (int j = 0; j < keys.Count; j++)
                    {
                        if (!item_list.Keys.Contains(keys[j].ToString()))
                        {
                            item_list.Add(keys[j].ToString(), tempObj[keys[j].ToString()].ToString() + "(" + keys[j].ToString() + ")");
                        }
                    }
                }
                COM_PASSPORT_NAT.DataSource = new BindingSource(item_list, null);
                COM_PASSPORT_NAT.DisplayMember = "Value";
                COM_PASSPORT_NAT.ValueMember = "Key";
                COM_PASSPORT_NAT.SelectedIndex = 0;
            }

            COM_PASSPORT_RES.Items.Clear();
            //체재 사유
            for (int i = 1; i <= 29; i++)
            {
                COM_PASSPORT_RES.Items.Add(new Utils.ComboItem(i.ToString("D8"), Properties.Resources.ResourceManager.GetString(string.Format("StringResidence{0}", i.ToString("D2")))));
            }
            COM_PASSPORT_RES.SelectedIndex = 0;

            //여권 종류
            COM_PASSPORT_TYPE.Items.Clear();
            for (int i = 1; i <= 6; i++)
            {
                if (i != 2) COM_PASSPORT_TYPE.Items.Add(new Utils.ComboItem(i.ToString("D2"), Properties.Resources.ResourceManager.GetString(string.Format("StringPassportEtc{0}", i.ToString("D2")+"1"))));
            }
            COM_PASSPORT_TYPE.SelectedIndex = 0;


            Dictionary<string, string> gender_list = new Dictionary<string, string>();
            gender_list.Add("M", Constants.getScreenText("COMBO_ITEM_MALE"));
            gender_list.Add("F", Constants.getScreenText("COMBO_ITEM_FEMALE"));
            COM_PASSPORT_SEX.Items.Clear();
            COM_PASSPORT_SEX.DataSource = new BindingSource(gender_list, null);
            COM_PASSPORT_SEX.DisplayMember = "Value";
            COM_PASSPORT_SEX.ValueMember = "Key";
            COM_PASSPORT_SEX.SelectedIndex = 0;

            //화면 디스크립트 변경
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;
            //Clear();
        }

        private void PassportInfoForm_Load(object sender, EventArgs e)
        {
            this.Text = Constants.getScreenText("PASSPORTINFO_FORM");
        }
   
        private void metroLabel7_Click(object sender, EventArgs e)
        {
            
        }

        private void BTN_OK_Click(object sender, EventArgs e)
        {
            if (COM_PASSPORT_TYPE.SelectedIndex == 0)
            {
                if (COM_PASSPORT_RES.SelectedIndex == 27)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_TYPE"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    COM_PASSPORT_RES.Focus();
                    return;
                }
            }
            else
            {
                if (COM_PASSPORT_RES.SelectedIndex != 27)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_TYPE"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    COM_PASSPORT_RES.Focus();
                    return;
                }
            }
            if (COM_PASSPORT_TYPE.SelectedIndex == 0)
            {
                if (string.Empty.Equals(TXT_PASSPORT_NO.Text) || TXT_PASSPORT_NO.Text.Length > 11)
                {
                    //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("TXT_PASSPORT_NO"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PASSPORT_NO.Focus();
                    return;
                }

                if (string.Empty.Equals(TXT_PASSPORT_EXP.Text))
                {
                    //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("TXT_PASSPORT_EXP"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PASSPORT_EXP.Focus();
                    return;
                }
            }
            else
            {
                if (string.Empty.Equals(TXT_PERMIT_NO.Text) || TXT_PERMIT_NO.Text.Length > 15)
                {
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PERMIT_NO"), "Permit_no", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PERMIT_NO.Focus();
                    return;
                }
            }

            if (string.Empty.Equals(TXT_PASSPORT_NAME.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("TXT_PASSPORT_NAME"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_PASSPORT_NAME.Focus();
                return;
            }
            
            if (string.Empty.Equals(COM_PASSPORT_NAT.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("COM_PASSPORT_NAT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                COM_PASSPORT_NAT.Focus();
                return;
            }
            if (string.Empty.Equals(COM_PASSPORT_SEX.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("COM_PASSPORT_SEX"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                COM_PASSPORT_SEX.Focus();
                return;
            }

            if (string.Empty.Equals(TXT_PASSPORT_BIRTH.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("TXT_PASSPORT_BIRTH"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_PASSPORT_BIRTH.Focus();
                return;
            }
            
            if (string.Empty.Equals(COM_PASSPORT_TYPE.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("COM_PASSPORT_TYPE"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                COM_PASSPORT_TYPE.Focus();
                return;
            }
            if (string.Empty.Equals(COM_PASSPORT_RES.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("COM_PASSPORT_RES"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                COM_PASSPORT_RES.Focus();
                return;
            }
           
            if (string.Empty.Equals(TXT_DATE_LAND.Text))
            {
                //MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("TXT_DATE_LAND"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_DATE_LAND.Focus();
                return;
            }

            

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Clear();
            this.Close(); 
        }

        public void Clear()
        {
            TXT_PASSPORT_NAME.Text = "";
            TXT_PASSPORT_NO.Text = "";
            COM_PASSPORT_NAT.Text = "";
            COM_PASSPORT_SEX.Text = "";
            //TXT_PASSPORT_BIRTH.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            //TXT_PASSPORT_EXP.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            TXT_PASSPORT_BIRTH.Value = System.DateTime.Now;
            TXT_PASSPORT_EXP.Value = System.DateTime.Now;
            COM_PASSPORT_TYPE.Text = "";
            COM_PASSPORT_RES.Text = "";
            TXT_DATE_LAND.Value = System.DateTime.Now;
            //TXT_DATE_LAND.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            TXT_PERMIT_NO.Text = "";
            TXT_NOTE.Text = "";
        }

        private void PassportInfoForm_Activated(object sender, EventArgs e)
        {
            if( this.Parent != null)

            {
                this.Parent.Focus();
                this.Focus();
            }
            Console.Write("PassportInfoForm_Activated");
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            //Keys key = keyData & ~(Keys.Shift | Keys.Control);
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                switch (keyData)
                {
                    case Keys.Enter:
                        BTN_OK.Focus();
                        BTN_OK_Click(null, null);
                        return true;
                    case Keys.Escape:
                        BTN_CLOSE.Focus();
                        BTN_CLOSE_Click(null, null);
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TXT_DATE_LAND_Leave(object sender, EventArgs e)
        {
            string curDate = System.DateTime.Now.ToString("yyyyMMdd");
            string selectDate = TXT_DATE_LAND.Value.ToString("yyyyMMdd").Replace("-", "").Replace("/", "");
            //현재일 체크
            if (Int32.Parse(curDate) < Int32.Parse(selectDate))
            {
                MetroMessageBox.Show(this, Constants.getMessage("ERROR_DATE"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_DATE_LAND.Value = System.DateTime.Now;
                return;
            }
            DateTime limitDate = System.DateTime.Now;
            limitDate = limitDate.AddMonths(-6).AddDays(1);

            if (Int32.Parse(limitDate.ToString("yyyyMMdd")) > Int32.Parse(selectDate))
            {
                MetroMessageBox.Show(this, Constants.getMessage("ERROR_SIX_MONTH"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_DATE_LAND.Value = System.DateTime.Now;
                return;
            }
        }

        private void TXT_PERMIT_NO_Leave(object sender, EventArgs e)
        {
            if (TXT_PERMIT_NO.Text.Length > 0)
            {
                Boolean ismatch = Regex.IsMatch(TXT_PERMIT_NO.Text, @"^[a-zA-Z0-9]+$");
                if (!ismatch)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PERMIT_NO_TEXT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PERMIT_NO.Text = "";
                    TXT_PERMIT_NO.Focus();
                }
                else if (TXT_PERMIT_NO.Text.Length > 16)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PERMIT_NO_LENGTH"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PERMIT_NO.Focus();
                }
            }
        }

        private void TXT_NOTE_Leave(object sender, EventArgs e)
        {
            if (TXT_NOTE.Text.Length > 30)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ERROR_NOTE_LENGTH"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_NOTE.Focus();
            }
        }

        private void COM_PASSPORT_TYPE_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (COM_PASSPORT_TYPE.SelectedIndex == 0)
            {
                TXT_PERMIT_NO.Text = "";
                TXT_PERMIT_NO.Enabled = false;
            }
            else
            {
                TXT_PERMIT_NO.Enabled = true;
            }
        }

        private void COM_PASSPORT_RES_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int Residence_Type = (COM_PASSPORT_RES.SelectedIndex + 1);

            //재류자격이 일본관세청 전송 API에 정의되지 않은 항목이 선택될 경우는 비고란에 해당 설명이 입력되도록 한다.
            TXT_NOTE.Text = "";

            switch (Residence_Type)
            {
                case 3: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 5: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 7: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 10: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 11: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 12: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 13: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 14: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 16: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                case 17: TXT_NOTE.Text = COM_PASSPORT_RES.SelectedItem.ToString(); break;
                default: TXT_NOTE.Text = ""; break;
            }
        }

        private void TXT_PASSPORT_NAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(Char.IsLetter(e.KeyChar)) && e.KeyChar != 8) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }

        private void TXT_PASSPORT_NO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!(Char.IsLetter(e.KeyChar)) && e.KeyChar != 8) && e.KeyChar != ' ' && (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8))
            {
                e.Handled = true;
            }
        }
    }

}
