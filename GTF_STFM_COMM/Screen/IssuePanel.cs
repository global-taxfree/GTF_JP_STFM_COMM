using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using Florentis;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Controls;
using Newtonsoft.Json.Linq;
using log4net;
using GTF_Comm;
using GTF_Passport;
using GTF_STFM_COMM.Tran;
using GTF_STFM_COMM.Util;
using GTF_Printer;
using System.IO;

namespace GTF_STFM_COMM.Screen
{
    public partial class IssuePanel : UserControl
    {
        string sign_data = "";
        string send_custom_flag = "";
        string print_goods_type = "0" ;
        ControlManager m_CtlSizeManager = null; 
        GTF_PassportScanner m_passScan = null;
        JArray m_ArrShopList = null;
        JArray ArrNationalList = null;
        bool birth_validate = false;
        bool expire_validate = false;
        JArray m_ArrNationalList
        {
            get
            {
                if (ArrNationalList == null || ArrNationalList.Count ==0)
                {
                    if (Constants.CODE_DOWNLOAD)
                    {
                        JObject jsonObj = new JObject();
                        jsonObj.Add("CODEDIV", "COUNTRY_CODE");
                        Transaction tran = new Transaction();
                        ArrNationalList = tran.SearchContryList(jsonObj);
                        jsonObj.RemoveAll();
                        jsonObj = null;
                        tran = null;
                        if (ArrNationalList == null)
                        {
                            return null;
                        }
                    }
                }
                return ArrNationalList;
            }
            set
            {
                ArrNationalList = value;
            }
        }
        ILog m_Logger = null;

        public IssuePanel(ILog Logger = null)
        {
            m_Logger = Logger;
            InitializeComponent();
            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            m_CtlSizeManager = new ControlManager(this);

            //횡좌표이동
            m_CtlSizeManager.addControlMove(BTN_SCAN, true, false,false,false);
            m_CtlSizeManager.addControlMove(BTN_PASSPORT_MANUAL, true, false, false, false);
            //m_CtlSizeManager.addControlMove(BTN_SUBMIT, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_CLEAR, true, false, false, false);
            //m_CtlSizeManager.addControlMove(BTN_ITEM_ADD, true, false, false, false);

            //종횡좌표이동
            m_CtlSizeManager.addControlMove(BTN_SUBMIT, true, true, false, false);
            m_CtlSizeManager.addControlMove(CHK_SUM, true, true, false, false);
            m_CtlSizeManager.addControlMove(LBL_CHECKSUM, true, true, false, false);

            m_CtlSizeManager.addControlMove(LAY_TOTAL, true, true, false, false);            
            
            //횡늘림
            m_CtlSizeManager.addControlMove(LAY_REFUND, false, false, true, false);
            m_CtlSizeManager.addControlMove(TIL_1, false, false, true, false);
            m_CtlSizeManager.addControlMove(LAY_PASSPORT, false, false, true, false);
            m_CtlSizeManager.addControlMove(TIL_2, false, false, true, false);
            m_CtlSizeManager.addControlMove(LAY_SHOP, false, false, true, false);
            m_CtlSizeManager.addControlMove(TIL_3, false, false, true, false);
            //m_CtlSizeManager.addControlMove(LAY_AMOUNT, false, false, true, false);

            //종횡 늘림
            m_CtlSizeManager.addControlMove(GRD_ITEMS, false, false, true, true);

            //종이동 횡늘림
            m_CtlSizeManager.addControlMove(GRD_TOTAL_AMT, false, true, true, false);

            //화면 디스크립트 변경
            m_CtlSizeManager.ChageLabel(this);

            GRD_TOTAL_AMT.Rows.Clear();
            GRD_TOTAL_AMT.Rows.Add();
            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();

            if (Constants.RCT_ADD =="NO")
            {
                BTN_ITEM_ADD.Enabled = false;
                TXT_RCT_NO.Enabled = false;
            }
            else
            {
                BTN_ITEM_ADD.Enabled = true;
                TXT_RCT_NO.Enabled = true;
            }
        }


        public void init()
        {
            BTN_SCAN.Focus();

        }

        private void IssuePanel_Load(object sender, EventArgs e)
        {
            m_passScan = GTF_PassportScanner.Instance(null, Constants.PATH_TEMP);
            //metroDateTime1.Format = DateTimePickerFormat.Custom;
            //metroDateTime1.CustomFormat = "MM/dd/yyyy";

            COM_REFUND_TYPE.Items.Clear();
            COM_REFUND_TYPE.Items.Add(new Utils.ComboItem("01", Constants.getScreenText("COMBO_ITEM_CASH")));
            COM_REFUND_TYPE.Items.Add(new Utils.ComboItem("04", Constants.getScreenText("COMBO_ITEM_CARD")));
            COM_REFUND_TYPE.Items.Add(new Utils.ComboItem("06", Constants.getScreenText("COMBO_ITEM_QQ")));
            COM_REFUND_TYPE.SelectedIndex = 0;
            //일반판매장의 경우는 판매점 고정
            if (Constants.USER_AUTH.Equals("02"))
            {
                BTN_SHOP_SEARCH.Visible = false;//판매점 선택 버튼 hide
                CHK_SUM.Visible = false;
                LBL_CHECKSUM.Visible = false;
                COM_REFUND_TYPE.Enabled = false;
            }
            //위탁형은 판매점 변경
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
                //COM_PASSPORT_TYPE.Items.Add(new Utils.ComboItem(i.ToString("D2"), Properties.Resources.ResourceManager.GetString(string.Format("StringPassPortEtc{0}", i.ToString("D2")))));
                if (i != 2)  COM_PASSPORT_TYPE.Items.Add(new Utils.ComboItem(i.ToString("D2"), Properties.Resources.ResourceManager.GetString(string.Format("StringPassportEtc{0}", i.ToString("D2")+"1"))));
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
            TXT_DATE_LAND.Value = System.DateTime.Now;
            COM_PASSPORT_SEX.DropDownStyle = ComboBoxStyle.Simple;

            Transaction tran = new Transaction();
            m_ArrShopList = tran.SearchShopList();

            //대상매장이 없으면 강제 종료
            if (m_ArrShopList == null || m_ArrShopList.Count == 0)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ERROR_SHOP_CNT"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.ExitThread();
                Environment.Exit(0);
                return;
            }
            SHOP_CLEAR();
            GRD_ITEMS_COLUMN_INIT();

            tran = null;
        }

        private void BTN_SCAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_ArrNationalList == null || m_ArrNationalList.Count == 0)
                {
                    MetroMessageBox.Show(this, "パスポート国の情報をロードしています。しばらくしてもう一度お試しください！", "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Constants.PASSPORT_TYPE < 0)//여권스캐너 미 선택시 경고창
                {
                    MetroMessageBox.Show(this, Constants.getMessage("PASSPORT_NOTHING"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Wait 커서 상태면 return 처리
                //if (Cursor.Current == Cursors.WaitCursor)
                if (this.UseWaitCursor)
                    return;

                setWaitCursor(true);
                if (m_passScan.open(Constants.PASSPORT_TYPE) > 0)
                {
                    int strmrz = m_passScan.scan(7);
                    if (strmrz > 0)
                    {
                        //공용
                        String CheckStr = m_passScan.GetPassportName();
                        TXT_PASSPORT_NAME.Text = Regex.Replace(CheckStr, @"[^A-Z ]", "");
                        //TXT_PASSPORT_NAME.Text = m_passScan.GetPassportName();

                        //TXT_PASSPORT_NO.Text = m_passScan.GetPassportNo();
                        CheckStr = m_passScan.GetPassportNo();
                        TXT_PASSPORT_NO.Text = Regex.Replace(CheckStr, @"[^0-9A-Z]", "");
                        TXT_PASSPORT_NAT.Text = m_passScan.GetNationality();
                        if (TXT_PASSPORT_NAT.Text == "D")
                        {
                            //TXT_PASSPORT_NAT.Text = "DEU";
                            //TXT_PASSPORT_NAT_NAME.Text = this.searchNatonalityName("DEU");
                        }
                        else
                        {
                            TXT_PASSPORT_NAT_NAME.Text = this.searchNatonalityName(m_passScan.GetNationality());
                        }

                        //COM_PASSPORT_SEX.Text = m_passScan.GetSex();
                        COM_PASSPORT_SEX.SelectedValue = m_passScan.GetSex();

                        Utils gtfUtil = new Utils();
                        string strBirth = gtfUtil.getFullDate(m_passScan.GetBirthDate());
                        strBirth = strBirth.Substring(0, 4) + "/" + strBirth.Substring(4, 2) + "/" + strBirth.Substring(6, 2);
                        //string strExp = gtfUtil.getFullDate(m_passScan.GetExpireDate());
                        string strExp = "20" + m_passScan.GetExpireDate();
                        strExp = strExp.Substring(0, 4) + "/" + strExp.Substring(4, 2) + "/" + strExp.Substring(6, 2);
                        //TXT_PASSPORT_BIRTH.Text = gtfUtil.getFullDate(m_passScan.GetBirthDate());
                        //TXT_PASSPORT_EXP.Text = gtfUtil.getFullDate(m_passScan.GetExpireDate());

                        TXT_PASSPORT_BIRTH.Text = strBirth;
                        TXT_PASSPORT_EXP.Text = strExp;

                        DateTime dt;
                        birth_validate = DateTime.TryParse(strBirth, out dt);
                        expire_validate = DateTime.TryParse(strExp, out dt);

                        if (!birth_validate || !expire_validate)
                        {
                            MetroMessageBox.Show(this, "パスポートスキャン情報が正しくありません。 日付を確認してください。", "Passport Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            if (!birth_validate)
                            {
                                TXT_PASSPORT_BIRTH.Text = "";
                            }
                            else if (!expire_validate)
                            {
                                TXT_PASSPORT_EXP.Text = "";
                            }
                            else if (!birth_validate && !expire_validate)
                            {
                                TXT_PASSPORT_BIRTH.Text = "";
                                TXT_PASSPORT_EXP.Text = "";
                            }
                        }

                        if (BTN_SHOP_SEARCH.Visible)
                        {
                            BTN_SHOP_SEARCH.Focus();
                        }
                        else
                        {
                            TXT_RCT_NO.Focus();
                        }

                        if (TXT_PASSPORT_NAT_NAME.Text == "")
                        {
                            MetroMessageBox.Show(this, "パスポートスキャン情報が正しくありません。 国籍を確認してください。", "Passport Scan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            BTN_PASSPORT_MANUAL_Click(sender, e);

                        }

                    }
                    else
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("PASSPORT_REMOVE"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            finally
            {
                setWaitCursor(false);
            }
        }

        private void BTN_SUBMIT_Click(object sender, EventArgs e)
        {
            try {
                
                    
                //처리중에 이중 입력 방지
                if (this.UseWaitCursor)
                    return;
                setWaitCursor(true);
                Utils util = new Utils();
                Transaction tran = new Transaction();
                if (Constants.PRINTER_TYPE == null || string.Empty.Equals(Constants.PRINTER_TYPE.Trim()))
                {
                    
                    MetroMessageBox.Show(this, Constants.getMessage("PASSPORT_NOTHING"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    setWaitCursor(false);
                    BTN_SUBMIT.Focus();
                    return;
                }

                if (COM_PASSPORT_TYPE.SelectedIndex == 0)
                {
                    if (COM_PASSPORT_RES.SelectedIndex == 27)
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_TYPE"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        setWaitCursor(false);
                        COM_PASSPORT_RES.Focus();
                        return;
                    }
                }
                else
                {
                    if (COM_PASSPORT_RES.SelectedIndex != 27)
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_TYPE"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        setWaitCursor(false);
                        COM_PASSPORT_RES.Focus();
                        return;
                    }
                }

                if (Constants.RCT_ADD == "YES")
                {
                    //정합성 체크
                    if (COM_PASSPORT_TYPE.SelectedIndex == 0)
                    {
                        if (!validationCheck(true, true))
                        {
                            setWaitCursor(false);
                            BTN_SUBMIT.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!validationCheck(false, true))
                        {
                            setWaitCursor(false);
                            BTN_SUBMIT.Focus();
                            return;
                        }
                    }
                }
                else
                {
                    //정합성 체크
                    if (COM_PASSPORT_TYPE.SelectedIndex == 0)
                    {
                        if (!validationCheck(true, false))
                        {
                            setWaitCursor(false);
                            BTN_SUBMIT.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (!validationCheck(false, false))
                        {
                            setWaitCursor(false);
                            BTN_SUBMIT.Focus();
                            return;
                        }
                    }
                }

                

                //전표발행여부 확인
                DialogResult dRet = MetroMessageBox.Show(this, Constants.getMessage("REFUND_CONFIRM"), "Issue", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dRet != DialogResult.Yes)
                {
                    setWaitCursor(false);
                    BTN_SUBMIT.Focus();
                    return;
                }
                if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("04"))//카드 환급방법
                {
                    if (TXT_REFUND_TYPE_KEY.Text == null || string.Empty.Equals(TXT_REFUND_TYPE_KEY.Text.ToString().Trim()))
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("ERROR_CARD_EMPTY"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TXT_REFUND_TYPE_KEY.Focus();
                        TXT_REFUND_TYPE_KEY.SelectAll();
                        setWaitCursor(false);
                        return;
                    }

                    if (!tran.Upi_BinCHeck(TXT_REFUND_TYPE_KEY.Text.ToString()))
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("ERROR_BIN"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TXT_REFUND_TYPE_KEY.Focus();
                        TXT_REFUND_TYPE_KEY.SelectAll();
                        setWaitCursor(false);
                        return;
                    }
                }
                else if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("06"))//QQ 환급방법
                {
                    if (TXT_REFUND_TYPE_KEY.Text == null || string.Empty.Equals(TXT_REFUND_TYPE_KEY.Text.ToString().Trim()) )
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("ERROR_QQ_EMPTY"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TXT_REFUND_TYPE_KEY.Focus();
                        TXT_REFUND_TYPE_KEY.SelectAll();
                        setWaitCursor(false);
                        return;
                    }

                    if (!tran.QQ_Check(TXT_REFUND_TYPE_KEY.Text.ToString()))
                    {
                        MetroMessageBox.Show(this, Constants.getMessage("ERROR_QQ_ID"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TXT_REFUND_TYPE_KEY.Focus();
                        TXT_REFUND_TYPE_KEY.SelectAll();
                        setWaitCursor(false);
                        return;
                    } 
                }
                if(Constants.SIGNPAD_USE == "YES")
                {
                    Sign_Req();
                    if(sign_data == null || string.Empty.Equals(sign_data) )
                    {
                        MetroMessageBox.Show(this, "電子署名を確認してください", "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        setWaitCursor(false);
                        return;
                    }
                }
              
                try
                {
                    setWaitCursor(true);
                    //여권정보 체크
                    string tourist = "";

                    string strSlipNo = "";//전표번호
                    string strUnikey = "";//unikey
                    string totalSeq = ""; //totalseq

                    strUnikey = Constants.TML_ID.PadLeft(5, '0') + (tran.getSeq("UNIQ_KEY") % 1000).ToString("D3");

                    if (COM_PASSPORT_TYPE.SelectedItem is Utils.ComboItem)//여권종류
                    {
                        tourist += ((Utils.ComboItem)COM_PASSPORT_TYPE.SelectedItem).Text;
                    }else
                    {
                        tourist += COM_PASSPORT_TYPE.SelectedItem.ToString();
                    }

                    if (COM_PASSPORT_TYPE.SelectedIndex == 0) tourist += "|" + TXT_PASSPORT_NO.Text;      //여권번호
                    else tourist += "|" + TXT_PERMIT_NO.Text;      //여권번호
                    tourist += "|" + TXT_PASSPORT_NAME.Text;    //이름
                    //tourist += "|" + TXT_PASSPORT_NAT.Text;     
                    tourist += "|" + searchNatonalityName(TXT_PASSPORT_NAT.Text);//국가명
                    tourist += "|" + TXT_PASSPORT_BIRTH.Text.Replace("-", "").Replace("/", "");      //생일
                    tourist += "|" + COM_PASSPORT_RES.SelectedItem.ToString();//체재유형
                    tourist += "|" + TXT_DATE_LAND.Text.Replace("-","").Replace("/", "");        //상륙연월일
                    tourist += "|" + TXT_PERMIT_NO.Text.ToString();//허가서번호
                    tourist += "|" + TXT_NOTE.Text.ToString();//체재유형
                    tourist += "|" ;        //상륙연월일

                    if(Constants.RCT_ADD == "YES")
                    {
                        //소비세 50만 제한 체크 위해 변수 추가
                        JArray arrTemp = tran.SearchSlips(DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"), "");
                        Dictionary<string, long> shop_consum_amt = new Dictionary<string, long>();

                        if (arrTemp != null && arrTemp.Count > 0)
                        {
                            for (int i = 0; i < arrTemp.Count; i++)
                            {
                                JObject tempObj = (JObject)arrTemp[i];
                                if (tempObj["CONSUMS_BUY_AMT"] != null)
                                {
                                    if (shop_consum_amt.ContainsKey(tempObj["MERCHANTNO"].ToString() + tempObj["PASSPORT_SERIAL_NO"].ToString() + tempObj["NATIONALITY_CODE"].ToString()))
                                    {
                                        shop_consum_amt[tempObj["MERCHANTNO"].ToString() + tempObj["PASSPORT_SERIAL_NO"].ToString() + tempObj["NATIONALITY_CODE"].ToString()]
                                            = shop_consum_amt[tempObj["MERCHANTNO"].ToString() + tempObj["PASSPORT_SERIAL_NO"].ToString() + tempObj["NATIONALITY_CODE"].ToString()] + Int64.Parse(tempObj["CONSUMS_BUY_AMT"].ToString());
                                    }
                                    else
                                    {
                                        shop_consum_amt[tempObj["MERCHANTNO"].ToString() + tempObj["PASSPORT_SERIAL_NO"].ToString() + tempObj["NATIONALITY_CODE"].ToString()] = Int64.Parse(tempObj["CONSUMS_BUY_AMT"].ToString());
                                    }
                                }
                            }
                        }

                        ArrayList arrShop = new ArrayList();            //shop 별 전표 출력을 위해
                        ArrayList arrShop_RCT = new ArrayList();        //shop + 영수증별 전표 출력을 위해

                        for (int i = 0; i < GRD_ITEMS.Rows.Count; i++)
                        {
                            if (!arrShop_RCT.Contains(GRD_ITEMS.Rows[i].Cells["GRD_TXT_SHOP_NO"].Value.ToString() + "+" + GRD_ITEMS.Rows[i].Cells["GRD_TXT_RCT_NO"].Value.ToString()))
                            {
                                arrShop_RCT.Add(GRD_ITEMS.Rows[i].Cells["GRD_TXT_SHOP_NO"].Value.ToString() + "+" + GRD_ITEMS.Rows[i].Cells["GRD_TXT_RCT_NO"].Value.ToString());
                                arrShop.Add(GRD_ITEMS.Rows[i].Cells["GRD_TXT_SHOP_NO"].Value.ToString());
                            }
                        }

                        //합산여부 체크
                        JObject selectShop_sum = null;
                        Boolean bSum = true;
                        string strtempShopName = "";
                        for (int j = 0; j < arrShop.Count; j++)
                        {
                            for (int k = 0; k < m_ArrShopList.Count; k++)
                            {
                                if (m_ArrShopList[k] is JObject)
                                {
                                    selectShop_sum = (JObject)m_ArrShopList[k];
                                    if (arrShop[j].Equals(selectShop_sum["MERCHANT_NO"].ToString()))
                                    {
                                        bSum = selectShop_sum["COMBINED_USEYN"] == null ? false : !"N".Equals(selectShop_sum["COMBINED_USEYN"].ToString().Trim());
                                        if (bSum)
                                        {
                                            bSum = selectShop_sum["VOID_USEYN"] == null ? false : !"N".Equals(selectShop_sum["VOID_USEYN"].ToString().Trim());
                                        }
                                        strtempShopName = selectShop_sum["MERCHANT_JPNM"].ToString();
                                        break;
                                    }
                                    selectShop_sum = null;
                                }
                            }
                            if (!bSum)
                                break;
                        }

                        if (!bSum && arrShop.Count > 1)
                        {
                            MetroMessageBox.Show(this, Constants.getMessage("ERROR_SUM") + " " + strtempShopName, "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            setWaitCursor(false);
                            return;
                        }

                        //if (CHK_SUM.Checked && arrShop.Count > 1)//합산전표 발행
                        if (arrShop.Count > 1)//합산전표 발행
                        {
                            totalSeq = Constants.TML_ID.PadLeft(5, '0') + (tran.getSeq("COMBINE_SLIP") % 1000).ToString("D5");
                        }
                        //전표 전체 금액
                        long nSlipTotalTaxAmt = 0;
                        long nSlipTotalBuyAmt = 0;
                        long nSlipTotalRefundAmt = 0;
                        long nSlipTotalFeeAmt = 0;


                        long nSlipTotalGoodsBuyAmt = 0;
                        long nSlipTotalConsumsBuyAmt = 0;

                        long nSlipTotalGoodsNoTaxBuyAmt = 0;
                        long nSlipTotalConsumsNoTaxBuyAmt = 0;

                        JArray arrSlips = new JArray();
                        GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                        //매장+영수증별 전표 출력
                        for (int j = 0; j < arrShop_RCT.Count; j++)
                        //for (int j = 0; j < arrShop.Count; j++)
                        {
                            string strTXT_TAX_OFFICE = "";
                            string strTXT_TAX_ADDR1 = "";
                            string strTXT_TAX_ADDR2 = "";
                            string strTXT_SHOP_NAME = "";
                            string strTXT_SHOP_ADDR1 = "";
                            string strTXT_SHOP_ADDR2 = "";
                            string strTXT_SHOP_NAME_EN = "";
                            string strOPT_CORP_JPNM = "";
                            string strOutputSlipType = "";
                            string strPreviewUse = "";
                            string[] arrOutputSlipType = null;

                            string strTAX_PROC_TIME_CODE = "";
                            string strTAX_POINT_PROC_CODE = string.Empty;

                            util.createSlipNo(tran.getSeq("REFUNDSLIP"), Constants.TML_ID, out strSlipNo);

                            JObject selectShop = null;
                            for (int k = 0; k < m_ArrShopList.Count; k++)
                            {
                                if (m_ArrShopList[k] is JObject)
                                {
                                    selectShop = (JObject)m_ArrShopList[k];
                                    if (arrShop[j].Equals(selectShop["MERCHANT_NO"].ToString()))
                                    {
                                        strTXT_TAX_OFFICE = selectShop["TAXOFFICE_NAME"].ToString();
                                        //strTXT_TAX_OFFICE_ADDR = selectShop["TAXOFFICE_ADDR"].ToString();
                                        strTXT_TAX_ADDR1 = selectShop["TAX_ADDR1"].ToString();
                                        strTXT_TAX_ADDR2 = selectShop["TAX_ADDR2"].ToString();

                                        strOPT_CORP_JPNM = selectShop["OPT_CORP_JPNM"].ToString();
                                        strTXT_SHOP_NAME = selectShop["MERCHANT_JPNM"].ToString();
                                        strTXT_SHOP_ADDR1 = selectShop["JP_ADDR1"].ToString();
                                        strTXT_SHOP_ADDR2 = selectShop["JP_ADDR2"].ToString();
                                        strTXT_SHOP_NAME_EN = selectShop["MERCHANT_ENNM"].ToString();
                                        strOutputSlipType = selectShop["OUTPUT_SLIP_TYPE"].ToString();
                                        arrOutputSlipType = strOutputSlipType.Split('/');
                                        strPreviewUse = selectShop["PREVIEW_USEYN"].ToString();
                                        strTAX_PROC_TIME_CODE = selectShop["TAX_PROC_TIME_CODE"].ToString();
                                        strTAX_POINT_PROC_CODE = selectShop["TAX_POINT_PROC_CODE"].ToString();
                                        break;
                                    }
                                    selectShop = null;
                                }
                            }

                            //매장정보 체크
                            string retailer = "";
                            retailer += strTXT_TAX_OFFICE;            //세관명
                            retailer += "|" + strTXT_TAX_ADDR1;       //세관주소1
                            retailer += "|" + strTXT_TAX_ADDR2;       //세관주소 2

                            retailer += "|" + strTXT_SHOP_NAME;       //매장명
                            retailer += "|" + strTXT_SHOP_ADDR1;      //매장주소1
                            retailer += "|" + strTXT_SHOP_ADDR2;      //매장주소2
                            retailer += "|" + strOPT_CORP_JPNM;       //회사영문명


                            //전표 내용
                            string docid = "";
                            string strSEND_CUSTOM_FLAG = "N";
                            strSEND_CUSTOM_FLAG = selectShop["SEND_CUSTOM_FLAG"].ToString();

                            string strSlipLang = "EN";
                            if (selectShop["NATIONALITY_MAPPING_USEYN"] != null && !"Y".Equals(selectShop["NATIONALITY_MAPPING_USEYN"].ToString()))
                            {
                                PrintSlipLangForm langForm = new PrintSlipLangForm();
                                langForm.ShowDialog(this);
                                if (langForm.m_SelectLang != null && !string.Empty.Equals(langForm.m_SelectLang.Trim()))
                                {
                                    strSlipLang = langForm.m_SelectLang;
                                }
                            }
                            else
                            {
                                if (TXT_PASSPORT_NAT.Text != null)
                                {
                                    if (TXT_PASSPORT_NAT.Text.Equals("CHN") || TXT_PASSPORT_NAT.Text.Equals("TWN"))
                                    {
                                        strSlipLang = "CN";
                                    }
                                    else if (TXT_PASSPORT_NAT.Text.Equals("KOR"))
                                    {
                                        strSlipLang = "KR";
                                    }
                                }
                            }

                            docid += strSlipLang;                         //출력언어


                            if (Constants.PRINT_TYPE != "ALL")
                            {
                                docid += "|" + 1;           //출력전표 갯수
                                docid += "|" + "01";     //출력
                            }
                            else
                            {
                                docid += "|" + 2;           //출력전표 갯수
                                docid += "|" + "01/02";     //출력
                            }

                            docid += "|" + "[REPUBLISH]";                 //출력유형

                            docid += "|" + strSlipNo;                //전표번호(Slip No)
                            if (!string.Empty.Equals(totalSeq))//합산전표 발행
                            {
                                docid += "|" + "Y";                         //합산전표 출력여부
                            }
                            else
                            {
                                docid += "|" + "N";                         //합산전표 출력여부
                            }
                            docid += "|" + ((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value;//환급방법(CODE)
                            docid += "|" + ((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Text; //환급방법(NAME)

                            if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("01"))//환급방법
                            {
                                docid += "|" + "";//환급방법
                            }
                            else
                            {
                                if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("04"))//카드환급
                                {
                                    docid += "|" + "****-****-****" + TXT_REFUND_TYPE_KEY.Text.Substring(14);//카드번호 마스킹
                                }
                                else if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("06") && TXT_REFUND_TYPE_KEY.Text != null
                                    && TXT_REFUND_TYPE_KEY.Text.Length >= 4)//QQ 번호 마스킹
                                {
                                    docid += "|" + new String('*', TXT_REFUND_TYPE_KEY.Text.Length - 4) + TXT_REFUND_TYPE_KEY.Text.Substring(TXT_REFUND_TYPE_KEY.Text.Length - 4);//카드번호 마스킹
                                }
                                else
                                    docid += "|" + TXT_REFUND_TYPE_KEY.Text;
                            }
                            docid += "|" + strUnikey;//전표단축번호

                            //물품부 체크

                            //매장별로 물품 묶어서 팔면 되겠네
                            //1.일반/소비품 둘다 있는지 확인
                            //2. 매장별로 일반물품 목록 저장
                            //3. 일반물품 sum 데이터 추출
                            //4. 매장별로 소비품 목록 저장
                            //5. 소비품 sum 데이터 추출 
                            string goods = "";
                            goods += TXT_REFUND_DATE.Text.Replace("-", "").Replace("/", "");             //판매일

                            JArray arrGoods = new JArray();
                            JArray arrConsums = new JArray();

                            JArray arrTotalGoodsList = new JArray();      //로컬 DB Insert 용 전표 물품 정보


                            long nGoods_Sum_Tax = 0;
                            long nGoods_Sum_Buy_InTax = 0;
                            long nGoods_Sum_Buy_NoTax = 0;
                            long nGoods_Sum_Buy = 0;
                            long nGoods_Sum_Refund = 0;

                            long nConsums_Sum_Tax = 0;
                            long nConsums_Sum_Buy_InTax = 0;
                            long nConsums_Sum_Buy_NoTax = 0;
                            long nConsums_Sum_Buy = 0;
                            long nConsums_Sum_Refund = 0;



                            Boolean bInTax = false;
                            Boolean bOutTax = false;
                            //전표 물품 데이터 등록
                            for (int i = 0; i < GRD_ITEMS.Rows.Count; i++)
                            {

                                //if (arrShop[j].Equals(GRD_ITEMS.Rows[i].Cells["GRD_TXT_SHOP_NO"].Value.ToString()))//현재 대상 매장이면 처리
                                if (arrShop_RCT[j].Equals(GRD_ITEMS.Rows[i].Cells["GRD_TXT_SHOP_NO"].Value.ToString() + "+" + GRD_ITEMS.Rows[i].Cells["GRD_TXT_RCT_NO"].Value.ToString()))//현재 대상 매장 + 영수증번호면 처리
                                {
                                    JObject tempObj = new JObject(); //출력용

                                    //1.출력용
                                    //물품순번 (제외) // 물품명 // 물품단가 //물품갯수 // 물품 총액 //세금 // 환급액 //세금종류
                                    tempObj.Add("ITEM_NAME", GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_NAME"].Value.ToString());
                                    //tempObj.Add("UNIT_AMT", (Int64.Parse(GRD_ITEMS.Rows[i].Cells["GRD_TXT_BUY_AMT"].Value.ToString())
                                    //    / Int64.Parse(GRD_ITEMS.Rows[i].Cells["GRD_TXT_QTY"].Value.ToString())).ToString());
                                    tempObj.Add("UNIT_AMT", GRD_ITEMS.Rows[i].Cells["GRD_TXT_UNIT_AMT"].Value.ToString());

                                    tempObj.Add("QTY", GRD_ITEMS.Rows[i].Cells["GRD_TXT_QTY"].Value.ToString());
                                    tempObj.Add("BUY_AMT", GRD_ITEMS.Rows[i].Cells["GRD_TXT_BUY_AMT"].Value.ToString());

                                    tempObj.Add("TAX_AMT", GRD_ITEMS.Rows[i].Cells["GRD_TXT_TAX_AMT"].Value.ToString());
                                    tempObj.Add("REFUND_AMT", GRD_ITEMS.Rows[i].Cells["GRD_TXT_REFUND_AMT"].Value.ToString());

                                    tempObj.Add("FEE_AMT", (Int64.Parse(tempObj["TAX_AMT"].ToString()) - Int64.Parse(tempObj["REFUND_AMT"].ToString())).ToString());

                                    tempObj.Add("TAX_TYPE", GRD_ITEMS.Rows[i].Cells["GRD_COM_TAX_TYPE"].Value.ToString());  //내/외세 구분

                                    tempObj.Add("ITEM_TYPE", "1".Equals(GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_TYPE"].Value.ToString()) ? "A0001" : "A0002"); //일반물품/소비품 코드
                                    tempObj.Add("MAIN_CAT", GRD_ITEMS.Rows[i].Cells["GRD_TXT_MAIN_CAT"].Value.ToString());  //대분류 코드
                                    tempObj.Add("MID_CAT", GRD_ITEMS.Rows[i].Cells["GRD_TXT_MID_CAT"].Value.ToString());   //중분류 코드

                                    tempObj.Add("ITEM_TYPE_TEXT", ((DataGridViewComboBoxCell)GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_TYPE"]).FormattedValue.ToString()); //일반물품/소비품 코드
                                    tempObj.Add("MAIN_CAT_TEXT", ((DataGridViewComboBoxCell)GRD_ITEMS.Rows[i].Cells["GRD_TXT_MAIN_CAT"]).FormattedValue.ToString());  //대분류 코드
                                    tempObj.Add("MID_CAT_TEXT", ((DataGridViewComboBoxCell)GRD_ITEMS.Rows[i].Cells["GRD_TXT_MID_CAT"]).FormattedValue.ToString());   //중분류 코드

                                    tempObj.Add("RCT_NO", GRD_ITEMS.Rows[i].Cells["GRD_TXT_RCT_NO"].Value.ToString());      //영수증번호
                                    tempObj.Add("ITEM_NO", GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_NO"].Value.ToString());    //물품 번호                               
                                    tempObj.Add("SLIP_NO", strSlipNo);                                                      //전표번호                      
                                    tempObj.Add("USERID", Constants.USER_ID);                                               //등록자

                                    tempObj.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd"));                       //등록일

                                    tempObj.Add("SHOP_NO", GRD_ITEMS.Rows[i].Cells["GRD_TXT_SHOP_NO"].Value.ToString());    //매장번호
                                    tempObj.Add("TAX_CAL_TYPE", GRD_ITEMS.Rows[i].Cells["GRD_TXT_TAX_CAL_TYPE"].Value.ToString());

                                    if ("1".Equals(GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_TYPE"].Value.ToString()))//일반물품
                                    {
                                        arrGoods.Add(tempObj);
                                    }
                                    else if ("2".Equals(GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_TYPE"].Value.ToString()))//소비물품 
                                    {
                                        arrConsums.Add(tempObj);
                                    }
                                    arrTotalGoodsList.Add(tempObj);
                                }
                            }

                            if (arrGoods.Count > 0 && arrConsums.Count > 0)
                            {
                                goods += "|" + "2";        //물품 판매 갯수(일반/소비)
                            }
                            else if (arrGoods.Count > 0 || arrConsums.Count > 0)
                            {
                                goods += "|" + "1";        //물품 판매 갯수(일반/소비)
                            }
                            else
                            {
                                MetroMessageBox.Show(this, Constants.getMessage("ERROR_ISSUE"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                setWaitCursor(false);
                                return;
                            }

                            //소비
                            if (arrConsums != null && arrConsums.Count > 0)
                            {
                                goods += "|" + "01";           //물품종류(일반/소비)
                                goods += "|" + arrConsums.Count; //물품갯수
                                for (int i = 0; i < arrConsums.Count; i++)
                                {
                                    JObject tempObj = (JObject)arrConsums[i];

                                    Int64 nQty = Int64.Parse(tempObj["QTY"].ToString());
                                    Int64 nBuy = Int64.Parse(tempObj["BUY_AMT"].ToString());
                                    Int64 nTax = Int64.Parse(tempObj["TAX_AMT"].ToString());

                                    Int64 nUnit = (nBuy - nTax) / nQty;

                                    if (print_goods_type.Equals("1"))
                                    {
                                        goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString() + " - " + tempObj["MID_CAT_TEXT"].ToString();
                                    }
                                    else if (print_goods_type.Equals("2"))
                                    {
                                        goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString() + " - " + tempObj["MID_CAT_TEXT"].ToString() + " - " + tempObj["ITEM_NAME"].ToString();
                                    }
                                    else
                                    {
                                        goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString();    //물품명 
                                    }

                                    //goods += "|" + tempObj["BUY_AMT"].ToString();               //물품 총액 
                                    //물품 총액 & 총판매액
                                    if ("02".Equals(tempObj["TAX_TYPE"].ToString()))             //내세
                                    {
                                        bInTax = true;
                                        //goods += "|" + tempObj["UNIT_AMT"].ToString();              //물품단가 
                                        //goods += "|" + nUnit.ToString();              //물품단가 
                                        //goods += "|" + (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));//물품단가 
                                        //goods += "|" + nUnit.ToString();                            //물품단가 
                                        if (Int64.Parse(tempObj["UNIT_AMT"].ToString()) == Int64.Parse(tempObj["BUY_AMT"].ToString()))
                                        {
                                            goods += "|" + (Int64.Parse(tempObj["UNIT_AMT"].ToString()) - nTax); //물품단가 
                                        }
                                        else
                                        {
                                            goods += "|" + nUnit.ToString(); //물품단가 
                                        }
                                        goods += "|" + tempObj["QTY"].ToString();                   //물품갯수 
                                        goods += "|" + (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                                        nConsums_Sum_Buy_NoTax += (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                                        //nConsums_Sum_Buy += (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                                        nConsums_Sum_Buy_InTax += Int64.Parse(tempObj["BUY_AMT"].ToString());
                                    }
                                    else                                                            //외세
                                    {
                                        bOutTax = true;
                                        goods += "|" + tempObj["UNIT_AMT"].ToString();              //물품단가 
                                                                                                    //goods += "|" + tempObj["BUY_AMT"].ToString();              //물품단가 
                                        goods += "|" + tempObj["QTY"].ToString();                   //물품갯수 
                                        goods += "|" + tempObj["BUY_AMT"].ToString();
                                        //nConsums_Sum_Buy += Int64.Parse(tempObj["BUY_AMT"].ToString());
                                        nConsums_Sum_Buy_NoTax += Int64.Parse(tempObj["BUY_AMT"].ToString());
                                    }

                                    nConsums_Sum_Buy += Int64.Parse(tempObj["BUY_AMT"].ToString());     //총판매액
                                    nConsums_Sum_Tax += Int64.Parse(tempObj["TAX_AMT"].ToString());     //총세금
                                    nConsums_Sum_Refund += Int64.Parse(tempObj["REFUND_AMT"].ToString());//총환급액
                                    tempObj = null;
                                }

                                //goods += "|" + nConsums_Sum_Buy.ToString();         //총판매액
                                goods += "|" + nConsums_Sum_Buy_NoTax.ToString();         //총판매액
                                goods += "|" + nConsums_Sum_Tax.ToString();         //총세금
                                goods += "|" + nConsums_Sum_Refund.ToString();      //총환급액
                                goods += "|[COMSUMS_TOTAL]";      //AllStoresTotalAmt
                            }

                            //매장별 소비품 금액 확인
                            if (shop_consum_amt.ContainsKey(arrShop[j].ToString() + TXT_PASSPORT_NO.Text + TXT_PASSPORT_NAT.Text))
                            {

                                shop_consum_amt[arrShop[j].ToString() + TXT_PASSPORT_NO.Text + TXT_PASSPORT_NAT.Text]
                                    = shop_consum_amt[arrShop[j].ToString() + TXT_PASSPORT_NO.Text + TXT_PASSPORT_NAT.Text] + nConsums_Sum_Buy_NoTax;
                            }
                            else
                            {
                                shop_consum_amt[arrShop[j].ToString() + TXT_PASSPORT_NO.Text + TXT_PASSPORT_NAT.Text] = nConsums_Sum_Buy_NoTax;
                            }
                            //if(nConsums_Sum_Buy >= 500000)
                            //if (nConsums_Sum_Buy_NoTax >= 500000)

                            //일반
                            if (arrGoods != null && arrGoods.Count > 0)
                            {
                                goods += "|" + "02";           //물품종류(일반/소비)
                                goods += "|" + arrGoods.Count; //물품갯수

                                for (int i = 0; i < arrGoods.Count; i++)
                                {
                                    JObject tempObj = (JObject)arrGoods[i];

                                    Int64 nQty = Int64.Parse(tempObj["QTY"].ToString());
                                    Int64 nBuy = Int64.Parse(tempObj["BUY_AMT"].ToString());
                                    Int64 nTax = Int64.Parse(tempObj["TAX_AMT"].ToString());
                                    Int64 nUnit = (nBuy - nTax) / nQty;


                                    if (print_goods_type.Equals("1"))
                                    {
                                        goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString() + " - " + tempObj["MID_CAT_TEXT"].ToString() ;
                                    }
                                    else if (print_goods_type.Equals("2"))
                                    {
                                        goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString() + " - " + tempObj["MID_CAT_TEXT"].ToString() + " - " + tempObj["ITEM_NAME"].ToString();
                                    }
                                    else
                                    {
                                        goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString();    //물품명 
                                    }
                                    //goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString()+ "-" + tempObj["MID_CAT_TEXT"].ToString() + "-" +tempObj["ITEM_NAME"].ToString();              //물품명 
                                    
                                    //goods += "|" + tempObj["MAIN_CAT_TEXT"].ToString();    //물품명 
                                                                                                                                                                                        //goods += "|" + tempObj["UNIT_AMT"].ToString();              //물품단가 

                                    //물품 총액 & 총판매액
                                    if ("02".Equals(tempObj["TAX_TYPE"].ToString()))             //내세
                                    {
                                        bInTax = true;
                                        //goods += "|" + nUnit.ToString();                            //물품단가 
                                        if (Int64.Parse(tempObj["UNIT_AMT"].ToString()) == Int64.Parse(tempObj["BUY_AMT"].ToString()))
                                        {
                                            goods += "|" + (Int64.Parse(tempObj["UNIT_AMT"].ToString()) - nTax); //물품단가 
                                        }
                                        else
                                        {
                                            goods += "|" + nUnit.ToString(); //물품단가 
                                        }

                                        goods += "|" + tempObj["QTY"].ToString();                   //물품갯수 
                                        goods += "|" + (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));

                                        nGoods_Sum_Buy_NoTax += (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));

                                        //nGoods_Sum_Buy_InTax+= (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                                        nGoods_Sum_Buy_InTax += (Int64.Parse(tempObj["BUY_AMT"].ToString()));
                                        //nGoods_Sum_Buy += (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                                    }
                                    else                                                         //외세
                                    {
                                        bOutTax = true;
                                        goods += "|" + tempObj["UNIT_AMT"].ToString();              //물품단가 
                                                                                                    //goods += "|" + tempObj["BUY_AMT"].ToString();              //물품단가 
                                        goods += "|" + tempObj["QTY"].ToString();                   //물품갯수 
                                        goods += "|" + tempObj["BUY_AMT"].ToString();
                                        nGoods_Sum_Buy_NoTax += Int64.Parse(tempObj["BUY_AMT"].ToString());
                                        //nGoods_Sum_Buy += Int64.Parse(tempObj["BUY_AMT"].ToString());
                                    }
                                    nGoods_Sum_Buy += Int64.Parse(tempObj["BUY_AMT"].ToString());
                                    nGoods_Sum_Tax += Int64.Parse(tempObj["TAX_AMT"].ToString());     //총세금
                                    nGoods_Sum_Refund += Int64.Parse(tempObj["REFUND_AMT"].ToString());//총환급액
                                }

                                //goods += "|" + nGoods_Sum_Buy.ToString();         //총판매액
                                goods += "|" + nGoods_Sum_Buy_NoTax.ToString();         //총판매액
                                goods += "|" + nGoods_Sum_Tax.ToString();         //총세금
                                goods += "|" + nGoods_Sum_Refund.ToString();      //총환급액
                                goods += "|[GOODS_TOTAL]";      //AllStoresTotalAmt
                            }

                            goods += "|" + (nGoods_Sum_Tax + nConsums_Sum_Tax).ToString();        //(일반+소비)총세금
                            goods += "|" + (nGoods_Sum_Buy + nConsums_Sum_Buy).ToString();        //(일반+소비)총판매액
                            goods += "|" + (nGoods_Sum_Tax + nConsums_Sum_Tax                   //(일반+소비)수수료
                                - nGoods_Sum_Refund - nConsums_Sum_Refund).ToString();
                            goods += "|" + (nGoods_Sum_Refund + nConsums_Sum_Refund).ToString();  //(일반+소비)총환급액

                            nSlipTotalGoodsBuyAmt += nGoods_Sum_Buy;
                            nSlipTotalConsumsBuyAmt += nConsums_Sum_Buy;

                            nSlipTotalGoodsNoTaxBuyAmt += nGoods_Sum_Buy_NoTax;
                            nSlipTotalConsumsNoTaxBuyAmt += nConsums_Sum_Buy_NoTax;

                            nSlipTotalTaxAmt += nGoods_Sum_Tax + nConsums_Sum_Tax;
                            nSlipTotalBuyAmt += nGoods_Sum_Buy + nConsums_Sum_Buy;
                            nSlipTotalRefundAmt += nGoods_Sum_Refund + nConsums_Sum_Refund;
                            nSlipTotalFeeAmt += nGoods_Sum_Tax + nConsums_Sum_Tax - nGoods_Sum_Refund - nConsums_Sum_Refund;

                            JObject jsonSlip = new JObject();   //로컬 DB Insert 용 전표 정보
                            jsonSlip.Add("BUYER_NAME", TXT_PASSPORT_NAME.Text);
                            jsonSlip.Add("PASSPORT_SERIAL_NO", TXT_PASSPORT_NO.Text);
                            jsonSlip.Add("NATIONALITY_CODE", TXT_PASSPORT_NAT.Text);
                            jsonSlip.Add("PERMIT_NO", TXT_PERMIT_NO.Text);
                            jsonSlip.Add("REFUND_NOTE", TXT_NOTE.Text);

                            jsonSlip.Add("NATIONALITY_NAME", searchNatonalityName(TXT_PASSPORT_NAT.Text));

                            jsonSlip.Add("GENDER_CODE", COM_PASSPORT_SEX.SelectedValue.ToString());//성별을 코드값으로 받게끔 변경
                            jsonSlip.Add("BUYER_BIRTH", TXT_PASSPORT_BIRTH.Text.Replace("-", "").Replace("/", ""));
                            jsonSlip.Add("PASS_EXPIRYDT", TXT_PASSPORT_EXP.Text.Replace("-", "").Replace("/", ""));
                            jsonSlip.Add("INPUT_WAY_CODE", "02"); //여권스캐너 입력
                            jsonSlip.Add("RESIDENCE_NO", (COM_PASSPORT_RES.SelectedIndex + 1).ToString("D8"));
                            jsonSlip.Add("RESIDENCE", COM_PASSPORT_RES.SelectedItem.ToString());
                            jsonSlip.Add("ENTRYDT", TXT_DATE_LAND.Text.Replace("-", "").Replace("/", ""));                       //상륙연월일
                            jsonSlip.Add("PASSPORT_TYPE", (COM_PASSPORT_TYPE.SelectedIndex + 1).ToString("D2"));
                            jsonSlip.Add("PASSPORT_TYPE_NAME", COM_PASSPORT_TYPE.Text.ToString());
                            jsonSlip.Add("SLIP_NO", strSlipNo);
                            jsonSlip.Add("MERCHANT_NO", arrShop[j].ToString());
                            jsonSlip.Add("SHOP_NAME", strTXT_SHOP_NAME);
                            jsonSlip.Add("OUT_DIV_CODE", "00");
                            jsonSlip.Add("REFUND_WAY_CODE", ((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value);
                            jsonSlip.Add("REFUND_WAY_CODE_NAME", COM_REFUND_TYPE.Text);
                            //jsonSlip.Add("SLIP_STATUS_CODE", "02"); 
                            jsonSlip.Add("SLIP_STATUS_CODE", "01");//최초 등록시에는 01
                            jsonSlip.Add("TML_ID", Constants.TML_ID);
                            jsonSlip.Add("REFUND_CARDNO", TXT_REFUND_TYPE_KEY.Text.Replace("-", "").Trim());
                            jsonSlip.Add("REFUND_CARD_CODE", "");
                            jsonSlip.Add("TOTAL_SLIPSEQ", totalSeq);

                            if (bInTax && bOutTax)
                                strTAX_PROC_TIME_CODE = "03";
                            else if (bOutTax)
                                strTAX_PROC_TIME_CODE = "01";
                            else if (bInTax)
                                strTAX_PROC_TIME_CODE = "02";

                            jsonSlip.Add("TAX_PROC_TIME_CODE", strTAX_PROC_TIME_CODE);

                            jsonSlip.Add("TAX_POINT_PROC_CODE", strTAX_POINT_PROC_CODE);

                            //jsonSlip.Add("GOODS_BUY_AMT", nGoods_Sum_Buy.ToString());
                            jsonSlip.Add("GOODS_BUY_AMT", nGoods_Sum_Buy_NoTax.ToString());
                            jsonSlip.Add("GOODS_TAX_AMT", nGoods_Sum_Tax.ToString());
                            jsonSlip.Add("GOODS_REFUND_AMT", nGoods_Sum_Refund.ToString());
                            //jsonSlip.Add("CONSUMS_BUY_AMT", nConsums_Sum_Buy.ToString());
                            jsonSlip.Add("CONSUMS_BUY_AMT", nConsums_Sum_Buy_NoTax.ToString());
                            jsonSlip.Add("CONSUMS_TAX_AMT", nConsums_Sum_Tax.ToString());
                            jsonSlip.Add("CONSUMS_REFUND_AMT", nConsums_Sum_Refund.ToString());
                            jsonSlip.Add("TOTAL_EXCOMM_IN_TAX_SALE_AMT", nGoods_Sum_Buy_InTax.ToString());
                            jsonSlip.Add("TOTAL_COMM_IN_TAX_SALE_AMT", nConsums_Sum_Buy_InTax.ToString());

                            jsonSlip.Add("UNIKEY", strUnikey);
                            jsonSlip.Add("SALEDT", System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss"));
                            jsonSlip.Add("REFUNDDT", "");
                            jsonSlip.Add("USERID", Constants.USER_ID);
                            jsonSlip.Add("MERCHANTNO", arrShop[j].ToString());
                            jsonSlip.Add("DESKID", Constants.DESK_ID);
                            jsonSlip.Add("COMPANYID", Constants.COMPANY_ID);
                            jsonSlip.Add(Constants.SEND_FLAG, "N");
                            jsonSlip.Add(Constants.PRINT_CNT, "0");
                            jsonSlip.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd")); //등록일

                            //광고 체크 
                            string adsinfo = "";

                            //광고이미지 가져옴
                            try
                            {

                                JObject tempCode = new JObject();
                                JArray arrAds = tran.SearchAdsList(tempCode);
                                if (arrAds != null || arrAds.Count > 0)
                                {
                                    adsinfo = arrAds.Count.ToString("D2");
                                    for (int i = 0; i < arrAds.Count; i++)
                                    {
                                        JObject tempObj = (JObject)arrAds[i];
                                        string filePath = tempObj["FILE_PATH"] != null ? tempObj["FILE_PATH"].ToString() : "";
                                        string fileData = tempObj["BASE64ENCRIPT"] != null ? tempObj["BASE64ENCRIPT"].ToString() : "";

                                        if (filePath != null && fileData != null && filePath.IndexOf('.') > 0 && !string.Empty.Equals(fileData.Trim()))
                                        {
                                            adsinfo += "|" + "01|3|01|02|03";
                                            byte[] arr = System.Convert.FromBase64String(fileData);
                                            string strFileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
                                            MemoryStream ms = new MemoryStream(arr);
                                            MemoryStream stream = new MemoryStream();
                                            Image image = Image.FromStream(ms);
                                            Bitmap bmp = new Bitmap(image);
                                            bmp = util.MakeGrayscale(bmp);
                                            bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                                            string convertData = System.Convert.ToBase64String(stream.ToArray());
                                            adsinfo += "|" + convertData;
                                        }
                                    }
                                }
                                arrAds = null;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.StackTrace);
                            }

                            // sign data
                            string singinfo = "";

                            if (Constants.SIGNPAD_USE == "YES")
                            {
                                singinfo = sign_data;
                            }


                            //전표 출력데이터 등록
                            JObject slipDocs = new JObject();
                            slipDocs.Add("SLIP_NO", strSlipNo);
                            slipDocs.Add("DOCID", docid);
                            slipDocs.Add("RETAILER", retailer);
                            slipDocs.Add("GOODS", goods);
                            slipDocs.Add("TOURIST", tourist);
                            slipDocs.Add("ADSINFO", adsinfo);
                            slipDocs.Add("PREVIEW", strPreviewUse);
                            slipDocs.Add("SIGN", singinfo);

                            JObject PrintSet = new JObject();
                            PrintSet.Add("SEND_CUSTOM_FLAG", strSEND_CUSTOM_FLAG);

                            //통합 등록
                            JObject TotalInfo = new JObject();
                            TotalInfo.Add("SLIP", jsonSlip);
                            TotalInfo.Add("ITEMS", arrTotalGoodsList);
                            TotalInfo.Add("DOCS", slipDocs);
                            TotalInfo.Add("DIGITIZE", PrintSet);

                            arrSlips.Add(TotalInfo);

                            arrGoods.Clear();
                            arrConsums.Clear();
                            //jsonSlip.RemoveAll();
                            arrTotalGoodsList = null;
                            jsonSlip = null;
                        }

                        //거래일 소비물품 금액 제한 체크
                        foreach (KeyValuePair<string, long> kv in shop_consum_amt)
                        {
                            if (kv.Value > 500000)//50만엔 초과하면 alert 호출
                            {
                                for (int k = 0; k < m_ArrShopList.Count; k++)
                                {
                                    if (m_ArrShopList[k] is JObject)
                                    {
                                        selectShop_sum = (JObject)m_ArrShopList[k];
                                        if (kv.Key.Substring(0, 8).Equals(selectShop_sum["MERCHANT_NO"].ToString()))
                                        {
                                            strtempShopName = selectShop_sum["MERCHANT_JPNM"].ToString();
                                            selectShop_sum = null;
                                            break;
                                        }
                                    }
                                }
                                MetroMessageBox.Show(this, strtempShopName + ":" + Constants.getMessage("ERROR_CONSUMS_MAX_AMT"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                setWaitCursor(false);
                                return;
                            }
                        }


                        //금액 체크
                        if (nSlipTotalGoodsBuyAmt == 0 && nSlipTotalConsumsBuyAmt == 0)
                        {
                            MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_GOODS_AMT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            setWaitCursor(false);
                            return;
                        }

                        if (nSlipTotalGoodsNoTaxBuyAmt > 0 && nSlipTotalGoodsNoTaxBuyAmt < 5000)
                        {
                            MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_GOODS_AMT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            setWaitCursor(false);
                            return;
                        }
                        if (nSlipTotalConsumsNoTaxBuyAmt > 0 && nSlipTotalConsumsNoTaxBuyAmt < 5000)
                        {
                            MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_COMSUMS_AMT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            setWaitCursor(false);
                            return;
                        }


                        printer.setPrinter(Constants.PRINTER_TYPE);
                        for (int i = 0; i < arrSlips.Count; i++)
                        {
                            string tmpDocid, tmpRetailer, tmpGoods, tmpTourist, tmpDdsinfo, strTmpPreviewUse = "", tmpSignInfo = "", tmpCUSTOM_SEND_FLAG = "";
                            JObject tmpObj = (JObject)arrSlips[i];
                            JObject tmpSlip = (JObject)tmpObj["SLIP"];
                            JArray tmpItems = (JArray)tmpObj["ITEMS"];
                            JObject tmpDocs = (JObject)tmpObj["DOCS"];
                            JObject tmpPrintSet = (JObject)tmpObj["DIGITIZE"];

                            tmpDocid = tmpDocs["DOCID"].ToString();
                            tmpRetailer = tmpDocs["RETAILER"].ToString();
                            tmpGoods = tmpDocs["GOODS"].ToString();
                            //tmpGoods = tmpGoods.Replace("[COMSUMS_TOTAL]", nSlipTotalConsumsBuyAmt.ToString());//치환
                            //tmpGoods = tmpGoods.Replace("[GOODS_TOTAL]", nSlipTotalGoodsBuyAmt.ToString());//치환
                            tmpGoods = tmpGoods.Replace("[COMSUMS_TOTAL]", nSlipTotalConsumsNoTaxBuyAmt.ToString());//치환. 세금제외 금액으로.
                            tmpGoods = tmpGoods.Replace("[GOODS_TOTAL]", nSlipTotalGoodsNoTaxBuyAmt.ToString());//치환. 세금제외 금액으로.
                            tmpDocs["GOODS"] = tmpGoods;
                            tmpTourist = tmpDocs["TOURIST"].ToString();
                            tmpDdsinfo = tmpDocs["ADSINFO"].ToString();
                            tmpSignInfo = tmpDocs["SIGN"].ToString();
                            strTmpPreviewUse = tmpDocs["PREVIEW"].ToString();
                            tmpCUSTOM_SEND_FLAG = tmpPrintSet["SEND_CUSTOM_FLAG"].ToString();
                            Boolean insert_check = tran.InsertSlip(tmpSlip, tmpItems, tmpDocs);
                            if (Constants.SIGNPAD_USE == "YES")
                            {
                                JObject tmpSign = new JObject();
                                tmpSign.Add("SLIP_NO", tmpSlip["SLIP_NO"]);
                                tmpSign.Add("SLIP_SIGN_DATA", sign_data);
                                tmpSign.Add("SEND_YN", "N");
                                tmpSign.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd")); //등록일
                                tran.InsertSlipSign(tmpSign);
                            }


                            if (insert_check)
                            {
                                if (Constants.SLIP_TYPE == "80mm")
                                {
                                    //Constant.PRINT_SETTING의 값----------- PRINT_ALL :: 0   EXCEPT_DIGITIZE :: 1
                                    if ((tmpCUSTOM_SEND_FLAG.Equals("Y")) && (Constants.PRINT_SETTING == "1"))
                                        MetroFramework.MetroMessageBox.Show(this, "購入記録情報が正常に登録されました。", "DIGITIZE_DATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    else
                                        printer.PrintSlip_ja(tmpDocid.Replace("[REPUBLISH]", "0"), tmpRetailer, tmpGoods, tmpTourist, tmpDdsinfo, "Y".Equals(strTmpPreviewUse), tmpSignInfo, print_goods_type);
                                }
                                else
                                {
                                    //Constant.PRINT_SETTING의 값----------- PRINT_ALL :: 0   EXCEPT_DIGITIZE :: 1
                                    if ((tmpCUSTOM_SEND_FLAG.Equals("Y")) && (Constants.PRINT_SETTING == "1"))
                                        MetroFramework.MetroMessageBox.Show(this, "購入記録情報が正常に登録されました。", "DIGITIZE_DATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    else
                                        printer.A4PrintTicket(tmpDocid.Replace("[REPUBLISH]", "0"), tmpRetailer, tmpGoods, tmpTourist, tmpDdsinfo, tmpSignInfo);
                                }
                            }
                            else
                            {
                                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ASK_IT"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                setWaitCursor(false);
                                return;
                            }
                        }

                        if (CHK_SUM.Checked && !string.Empty.Equals(totalSeq))//합산전표 발행
                        {
                            //long nSumSlip = tran.getSeq("COMBINE_SLIP");
                            //string seq = "1234567890";
                            string seq = totalSeq;
                            //string sum = nSlipTotalBuyAmt.ToString();
                            string sum = (nSlipTotalGoodsNoTaxBuyAmt + nSlipTotalConsumsNoTaxBuyAmt).ToString();//세금제외금액으로 출력
                            string tax = nSlipTotalTaxAmt.ToString();
                            string fee = nSlipTotalFeeAmt.ToString();
                            string refund = nSlipTotalRefundAmt.ToString();
                            printer.PrintSlip_ja_summary(seq, sum, tax, fee, refund);
                        }

                        MessageForm msgForm = new MessageForm(Constants.getMessage("SLIP_CONFIRM"), null, MessageBoxButtons.OK);
                        msgForm.ShowDialog(this);
                        //if (msgForm.DialogResult == DialogResult.Yes)
                        {
                            //현금
                            if ("01".Equals(((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.ToString()))
                            {
                                MessageSmallForm msgSmallForm = new MessageSmallForm(Constants.getMessage("REFUND_CASH") + " : " + nSlipTotalRefundAmt, null);
                                msgSmallForm.ShowDialog(this);
                            }
                            //카드
                            else if ("04".Equals(((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.ToString()))
                            {
                                MessageForm msgCompleteForm = new MessageForm(Constants.getMessage("REFUND_CARD") + " : " + nSlipTotalRefundAmt, null);
                                msgCompleteForm.ShowDialog(this);
                            }
                            //QQ
                            else if ("06".Equals(((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.ToString()))
                            {
                                MessageForm msgCompleteForm = new MessageForm(Constants.getMessage("REFUND_QQ") + " : " + nSlipTotalRefundAmt, null);
                                msgCompleteForm.ShowDialog(this);
                            }

                            //전표상태업데이트
                            for (int i = 0; i < arrSlips.Count; i++)
                            {
                                JObject tmpObj_total = (JObject)arrSlips[i];
                                JObject tmpSlip = (JObject)tmpObj_total["SLIP"];

                                JObject tempObj = new JObject();
                                JObject tempObj2 = new JObject();
                                tempObj.Add("SLIP_NO", tmpSlip["SLIP_NO"]);
                                tempObj2.Add("SLIP_STATUS_CODE", "02");
                                tempObj2.Add("REFUNDDT", System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss"));
                                tempObj2.Add(Constants.PRINT_CNT, "1");
                                tran.UpdateSlip(tempObj, tempObj2);
                                tempObj.RemoveAll();
                                tempObj2.RemoveAll();
                                tempObj = null;
                                tempObj2 = null;
                            }
                        }
                        SCREEN_CLEAR();
                    }
                    else
                    {
                        // 영수증 첨부 개발
                        string strTXT_TAX_OFFICE = "";
                        string strTXT_TAX_ADDR1 = "";
                        string strTXT_TAX_ADDR2 = "";
                        string strTXT_SHOP_NAME = "";
                        string strTXT_SHOP_ADDR1 = "";
                        string strTXT_SHOP_ADDR2 = "";
                        string strTXT_SHOP_NAME_EN = "";
                        string strOPT_CORP_JPNM = "";
                        string strOutputSlipType = "";
                        string strPreviewUse = "";
                        string[] arrOutputSlipType = null;
                        string strTAX_PROC_TIME_CODE = "";
                        string strTAX_POINT_PROC_CODE = string.Empty;
                        JArray arrSlips = new JArray();

                        util.createSlipNo(tran.getSeq("REFUNDSLIP"), Constants.TML_ID, out strSlipNo);
                        JObject selectShop = null;

                        ArrayList arrShop = new ArrayList();

                        arrShop.Add(TXT_SHOP_NO.Text);

                        for (int k = 0; k < m_ArrShopList.Count; k++)
                        {
                            if (m_ArrShopList[k] is JObject)
                            {
                                selectShop = (JObject)m_ArrShopList[k];
                                if (arrShop[0].Equals(selectShop["MERCHANT_NO"].ToString()))
                                {
                                    strTXT_TAX_OFFICE = selectShop["TAXOFFICE_NAME"].ToString();
                                    //strTXT_TAX_OFFICE_ADDR = selectShop["TAXOFFICE_ADDR"].ToString();
                                    strTXT_TAX_ADDR1 = selectShop["TAX_ADDR1"].ToString();
                                    strTXT_TAX_ADDR2 = selectShop["TAX_ADDR2"].ToString();

                                    strOPT_CORP_JPNM = selectShop["OPT_CORP_JPNM"].ToString();
                                    strTXT_SHOP_NAME = selectShop["MERCHANT_JPNM"].ToString();
                                    strTXT_SHOP_ADDR1 = selectShop["JP_ADDR1"].ToString();
                                    strTXT_SHOP_ADDR2 = selectShop["JP_ADDR2"].ToString();
                                    strTXT_SHOP_NAME_EN = selectShop["MERCHANT_ENNM"].ToString();
                                    strOutputSlipType = selectShop["OUTPUT_SLIP_TYPE"].ToString();
                                    arrOutputSlipType = strOutputSlipType.Split('/');
                                    strPreviewUse = selectShop["PREVIEW_USEYN"].ToString();
                                    strTAX_PROC_TIME_CODE = selectShop["TAX_PROC_TIME_CODE"].ToString();
                                    strTAX_POINT_PROC_CODE = selectShop["TAX_POINT_PROC_CODE"].ToString();
                                    break;
                                }
                                selectShop = null;
                            }
                        }

                        string retailer = "";
                        retailer += strTXT_TAX_OFFICE;            //세관명
                        retailer += "|" + strTXT_TAX_ADDR1;       //세관주소1
                        retailer += "|" + strTXT_TAX_ADDR2;       //세관주소 2

                        retailer += "|" + strTXT_SHOP_NAME;       //매장명
                        retailer += "|" + strTXT_SHOP_ADDR1;      //매장주소1
                        retailer += "|" + strTXT_SHOP_ADDR2;      //매장주소2
                        retailer += "|" + strOPT_CORP_JPNM;       //회사영문명


                        //전표 내용
                        string docid = "";
                        string strSlipLang = "EN";
                        if (selectShop["NATIONALITY_MAPPING_USEYN"] != null && !"Y".Equals(selectShop["NATIONALITY_MAPPING_USEYN"].ToString()))
                        {
                            PrintSlipLangForm langForm = new PrintSlipLangForm();
                            langForm.ShowDialog(this);
                            if (langForm.m_SelectLang != null && !string.Empty.Equals(langForm.m_SelectLang.Trim()))
                            {
                                strSlipLang = langForm.m_SelectLang;
                            }
                        }
                        else
                        {
                            if (TXT_PASSPORT_NAT.Text != null)
                            {
                                if (TXT_PASSPORT_NAT.Text.Equals("CHN") || TXT_PASSPORT_NAT.Text.Equals("TWN"))
                                {
                                    strSlipLang = "CN";
                                }
                                else if (TXT_PASSPORT_NAT.Text.Equals("KOR"))
                                {
                                    strSlipLang = "KR";
                                }
                            }
                        }

                        docid += strSlipLang;                         //출력언어


                        if (Constants.PRINT_TYPE != "ALL")
                        {
                            docid += "|" + 1;           //출력전표 갯수
                            docid += "|" + "01";     //출력
                        }
                        else
                        {
                            docid += "|" + 2;           //출력전표 갯수
                            docid += "|" + "01/02";     //출력
                        }

                        docid += "|" + "[REPUBLISH]";                 //출력유형

                        docid += "|" + strSlipNo;                //전표번호(Slip No)
                        docid += "|" + "N";                         //합산전표 출력여부
                        docid += "|" + ((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value;//환급방법(CODE)
                        docid += "|" + ((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Text; //환급방법(NAME)
                        docid += "|" + "";//환급방법

                        docid += "|" + strUnikey;//전표단축번호

                        JObject jsonSlip = new JObject();   //로컬 DB Insert 용 전표 정보
                        jsonSlip.Add("BUYER_NAME", TXT_PASSPORT_NAME.Text);
                        jsonSlip.Add("PASSPORT_SERIAL_NO", TXT_PASSPORT_NO.Text);
                        jsonSlip.Add("NATIONALITY_CODE", TXT_PASSPORT_NAT.Text);
                        jsonSlip.Add("NATIONALITY_NAME", searchNatonalityName(TXT_PASSPORT_NAT.Text));
                        jsonSlip.Add("GENDER_CODE", COM_PASSPORT_SEX.SelectedValue.ToString());//성별을 코드값으로 받게끔 변경
                        jsonSlip.Add("BUYER_BIRTH", TXT_PASSPORT_BIRTH.Text.Replace("-", "").Replace("/", ""));
                        jsonSlip.Add("PASS_EXPIRYDT", TXT_PASSPORT_EXP.Text.Replace("-", "").Replace("/", ""));
                        jsonSlip.Add("INPUT_WAY_CODE", "02"); //여권스캐너 입력
                        jsonSlip.Add("RESIDENCE_NO", (COM_PASSPORT_RES.SelectedIndex + 1).ToString("D8"));
                        jsonSlip.Add("RESIDENCE", COM_PASSPORT_RES.SelectedItem.ToString());
                        jsonSlip.Add("ENTRYDT", TXT_DATE_LAND.Text.Replace("-", "").Replace("/", ""));                       //상륙연월일
                        jsonSlip.Add("PASSPORT_TYPE", (COM_PASSPORT_TYPE.SelectedIndex + 1).ToString("D2"));
                        jsonSlip.Add("PASSPORT_TYPE_NAME", COM_PASSPORT_TYPE.Text.ToString());
                        jsonSlip.Add("SLIP_NO", strSlipNo);
                        jsonSlip.Add("MERCHANT_NO", arrShop[0].ToString());
                        jsonSlip.Add("SHOP_NAME", strTXT_SHOP_NAME);
                        jsonSlip.Add("OUT_DIV_CODE", "00");
                        jsonSlip.Add("REFUND_WAY_CODE", ((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value);
                        jsonSlip.Add("REFUND_WAY_CODE_NAME", COM_REFUND_TYPE.Text);
                        
                        jsonSlip.Add("SLIP_STATUS_CODE", "01");//최초 등록시에는 01
                        jsonSlip.Add("TML_ID", Constants.TML_ID);
                        jsonSlip.Add("REFUND_CARDNO", TXT_REFUND_TYPE_KEY.Text.Replace("-", "").Trim());
                        jsonSlip.Add("REFUND_CARD_CODE", "");
                        jsonSlip.Add("TOTAL_SLIPSEQ", totalSeq);
                        jsonSlip.Add("TAX_PROC_TIME_CODE", strTAX_PROC_TIME_CODE);

                        jsonSlip.Add("TAX_POINT_PROC_CODE", strTAX_POINT_PROC_CODE);

                        
                        jsonSlip.Add("GOODS_BUY_AMT", "0");
                        jsonSlip.Add("GOODS_TAX_AMT", "0");
                        jsonSlip.Add("GOODS_REFUND_AMT", "0");
                        jsonSlip.Add("CONSUMS_BUY_AMT", "0");
                        jsonSlip.Add("CONSUMS_TAX_AMT", "0");
                        jsonSlip.Add("CONSUMS_REFUND_AMT", "0");
                        jsonSlip.Add("TOTAL_EXCOMM_IN_TAX_SALE_AMT", "0");
                        jsonSlip.Add("TOTAL_COMM_IN_TAX_SALE_AMT", "0");

                        jsonSlip.Add("UNIKEY", strUnikey);
                        jsonSlip.Add("SALEDT", System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss"));
                        jsonSlip.Add("REFUNDDT", "");
                        jsonSlip.Add("USERID", Constants.USER_ID);
                        jsonSlip.Add("MERCHANTNO", arrShop[0].ToString());
                        jsonSlip.Add("DESKID", Constants.DESK_ID);
                        jsonSlip.Add("COMPANYID", Constants.COMPANY_ID);
                        jsonSlip.Add(Constants.SEND_FLAG, "N");
                        jsonSlip.Add(Constants.PRINT_CNT, "0");
                        jsonSlip.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd")); //등록일
                        
                        //광고 체크 
                        string adsinfo = "";
                        try
                        {

                            JObject tempCode = new JObject();
                            JArray arrAds = tran.SearchAdsList(tempCode);
                            if (arrAds != null || arrAds.Count > 0)
                            {
                                adsinfo = arrAds.Count.ToString("D2");
                                for (int i = 0; i < arrAds.Count; i++)
                                {
                                    JObject tempObj = (JObject)arrAds[i];
                                    string filePath = tempObj["FILE_PATH"] != null ? tempObj["FILE_PATH"].ToString() : "";
                                    string fileData = tempObj["BASE64ENCRIPT"] != null ? tempObj["BASE64ENCRIPT"].ToString() : "";

                                    if (filePath != null && fileData != null && filePath.IndexOf('.') > 0 && !string.Empty.Equals(fileData.Trim()))
                                    {
                                        adsinfo += "|" + "01|3|01|02|03";
                                        byte[] arr = System.Convert.FromBase64String(fileData);
                                        string strFileName = filePath.Substring(filePath.LastIndexOf('/') + 1);
                                        MemoryStream ms = new MemoryStream(arr);
                                        MemoryStream stream = new MemoryStream();
                                        Image image = Image.FromStream(ms);
                                        Bitmap bmp = new Bitmap(image);
                                        bmp = util.MakeGrayscale(bmp);
                                        bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                                        string convertData = System.Convert.ToBase64String(stream.ToArray());
                                        adsinfo += "|" + convertData;
                                    }
                                }
                            }
                            arrAds = null;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.StackTrace);
                        }

                        // sign data
                        string singinfo = "";

                        if (Constants.SIGNPAD_USE == "YES")
                        {
                            singinfo = sign_data;
                        }

                        //전표 출력데이터 등록
                        JObject slipDocs = new JObject();
                        slipDocs.Add("SLIP_NO", strSlipNo);
                        slipDocs.Add("DOCID", docid);
                        slipDocs.Add("RETAILER", retailer);
                        
                        slipDocs.Add("TOURIST", tourist);
                        slipDocs.Add("ADSINFO", adsinfo);
                        slipDocs.Add("PREVIEW", strPreviewUse);
                        slipDocs.Add("SIGN", singinfo);

                        //통합 등록
                        JObject TotalInfo = new JObject();
                        TotalInfo.Add("SLIP", jsonSlip);
                        TotalInfo.Add("DOCS", slipDocs);

                        arrSlips.Add(TotalInfo);

                        jsonSlip = null;
                        GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                        printer.setPrinter(Constants.PRINTER_TYPE);

                        string tmpDocid, tmpRetailer, tmpTourist, tmpDdsinfo, strTmpPreviewUse = "", tmpSignInfo = "";
                        JObject tmpObj = (JObject)arrSlips[0];
                        JObject tmpSlip = (JObject)tmpObj["SLIP"];
                        
                        JObject tmpDocs = (JObject)tmpObj["DOCS"];

                        tmpDocid = tmpDocs["DOCID"].ToString();
                        tmpRetailer = tmpDocs["RETAILER"].ToString();
                        tmpTourist = tmpDocs["TOURIST"].ToString();
                        tmpDdsinfo = tmpDocs["ADSINFO"].ToString();
                        tmpSignInfo = tmpDocs["SIGN"].ToString();

                        strTmpPreviewUse = tmpDocs["PREVIEW"].ToString();
                        Boolean insert_check = tran.InsertSlip(tmpSlip, tmpDocs);
                        if (Constants.SIGNPAD_USE == "YES")
                        {
                            JObject tmpSign = new JObject();
                            tmpSign.Add("SLIP_NO", tmpSlip["SLIP_NO"]);
                            tmpSign.Add("SLIP_SIGN_DATA", sign_data);
                            tmpSign.Add("SEND_YN", "N");
                            tmpSign.Add("REG_DTM", System.DateTime.Now.ToString("yyyyMMdd")); //등록일
                            tran.InsertSlipSign(tmpSign);
                        }
                        if (insert_check)
                        {
                            printer.PrintSlipNoGoods_ja(tmpDocid.Replace("[REPUBLISH]", "0"), tmpRetailer, tmpTourist, tmpDdsinfo, "Y".Equals(strTmpPreviewUse), tmpSignInfo);
                        }
                        else
                        {
                            MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ASK_IT"), "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            setWaitCursor(false);
                            return;
                        }

                        MessageForm msgForm = new MessageForm(Constants.getMessage("SLIP_CONFIRM"), null, MessageBoxButtons.OK);
                        msgForm.ShowDialog(this);
                        {

                            //전표상태업데이트
                            for (int i = 0; i < arrSlips.Count; i++)
                            {
                                JObject tmpObj_total = (JObject)arrSlips[i];
                                JObject tmpSlipUdate = (JObject)tmpObj_total["SLIP"];

                                JObject tempObj = new JObject();
                                JObject tempObj2 = new JObject();
                                tempObj.Add("SLIP_NO", tmpSlipUdate["SLIP_NO"]);
                                tempObj2.Add("SLIP_STATUS_CODE", "02");
                                tempObj2.Add("REFUNDDT", System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss"));
                                tempObj2.Add(Constants.PRINT_CNT, "1");
                                tran.UpdateSlip(tempObj, tempObj2);
                                tempObj.RemoveAll();
                                tempObj2.RemoveAll();
                                tempObj = null;
                                tempObj2 = null;
                            }
                        }
                        SCREEN_CLEAR();
                    } 
                    
                }catch(Exception ex)
                {
                    m_Logger.Error(ex.Message);
                    m_Logger.Error(ex.StackTrace);
                }
                finally
                {
                    tran = null;
                    setWaitCursor(false);
                    if (this.Parent != null && this.Parent is MetroForm)
                    {
                        ((MetroForm)this.Parent).Activate();
                    }
                }
            }
            catch(Exception e2)
            {
                Constants.LOGGER_MAIN.Info(e2.Message);
            }
            setWaitCursor(false);
        }

        private void IssuePanel_SizeChanged(object sender, EventArgs e)
        {
            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
            this.Refresh();
        }

        private void BTN_PASSPORT_MANUAL_Click(object sender, EventArgs e)
        {
            if (m_ArrNationalList == null || m_ArrNationalList.Count == 0)
            {
                MetroMessageBox.Show(this, "パスポート国の情報をロードしています。しばらくしてもう一度お試しください！", "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            PassportInfoForm passForm = new PassportInfoForm(null, m_ArrNationalList);

            //passForm.init(m_ArrNationalList);

            passForm.Controls["TXT_PASSPORT_NAME"].Text = TXT_PASSPORT_NAME.Text ;
            passForm.Controls["TXT_PASSPORT_NO"].Text = TXT_PASSPORT_NO.Text;

            Control tmpCrl = passForm.Controls["COM_PASSPORT_NAT"];
            if (tmpCrl is MetroComboBox)
            {
                if (TXT_PASSPORT_NAT.Text != null && !string.Empty.Equals(TXT_PASSPORT_NAT.Text.ToString()))
                {
                    ((MetroComboBox)tmpCrl).SelectedValue = TXT_PASSPORT_NAT.Text;
                }
            }

            //passForm.Controls["COM_PASSPORT_NAT"]. = TXT_PASSPORT_NAT.Text; 
            if (COM_PASSPORT_SEX.Text != null && !string.Empty.Equals(COM_PASSPORT_SEX.Text.ToString()))
            {
                passForm.Controls["COM_PASSPORT_SEX"].Text = COM_PASSPORT_SEX.Text;
            }
            //passForm.Controls["TXT_PASSPORT_BIRTH"].Text = (string.Empty.Equals(TXT_PASSPORT_BIRTH.Text) || TXT_PASSPORT_BIRTH.Text.Length <8) ? "": TXT_PASSPORT_BIRTH.Text.Substring(0, 4) + "-" + TXT_PASSPORT_BIRTH.Text.Substring(4, 2) + "-" + TXT_PASSPORT_BIRTH.Text.Substring(6);
            //passForm.Controls["TXT_PASSPORT_EXP"].Text = (string.Empty.Equals(TXT_PASSPORT_EXP.Text) || TXT_PASSPORT_EXP.Text.Length < 8) ? "" :TXT_PASSPORT_EXP.Text.Substring(0, 4) + "-" + TXT_PASSPORT_EXP.Text.Substring(4, 2) + "-" + TXT_PASSPORT_EXP.Text.Substring(6);

            passForm.Controls["TXT_PASSPORT_BIRTH"].Text = (string.Empty.Equals(TXT_PASSPORT_BIRTH.Text) || TXT_PASSPORT_BIRTH.Text.Length < 10) ? "" : TXT_PASSPORT_BIRTH.Text.Replace("-", "/");
            passForm.Controls["TXT_PASSPORT_EXP"].Text = (string.Empty.Equals(TXT_PASSPORT_EXP.Text) || TXT_PASSPORT_EXP.Text.Length < 10) ? "" : TXT_PASSPORT_EXP.Text.Replace("-", "/");

            passForm.Controls["COM_PASSPORT_TYPE"].Text = COM_PASSPORT_TYPE.Text;
            passForm.Controls["COM_PASSPORT_RES"].Text = COM_PASSPORT_RES.Text ;
            passForm.Controls["TXT_DATE_LAND"].Text = TXT_DATE_LAND.Text;
            passForm.Controls["TXT_PERMIT_NO"].Text = TXT_PERMIT_NO.Text;
            passForm.Controls["TXT_NOTE"].Text = TXT_NOTE.Text;

            DialogResult bResult = passForm.ShowDialog(this);
            if (bResult == DialogResult.OK)
            {
                //팝업에서 얻어온 정보를 화면에 세팅
                //TXT_PASSPORT_NAME.Text = passForm.Controls["TXT_PASSPORT_NAME"].Text;
                String CheckStr = passForm.Controls["TXT_PASSPORT_NAME"].Text;
                TXT_PASSPORT_NAME.Text = Regex.Replace(CheckStr, @"[^A-Z ]", "");

                //TXT_PASSPORT_NO.Text = passForm.Controls["TXT_PASSPORT_NO"].Text;
                CheckStr = passForm.Controls["TXT_PASSPORT_NO"].Text;
                TXT_PASSPORT_NO.Text = Regex.Replace(CheckStr, @"[^0-9A-Z]", "");

                tmpCrl = passForm.Controls["COM_PASSPORT_NAT"];
                if (tmpCrl is MetroComboBox)
                {
                    TXT_PASSPORT_NAT.Text = ((MetroComboBox)tmpCrl).SelectedValue.ToString();
                    TXT_PASSPORT_NAT_NAME.Text = this.searchNatonalityName(((MetroComboBox)tmpCrl).SelectedValue.ToString());
                }
                COM_PASSPORT_SEX.Text = passForm.Controls["COM_PASSPORT_SEX"].Text;
                TXT_PASSPORT_BIRTH.Text = passForm.Controls["TXT_PASSPORT_BIRTH"].Text;//.Replace("-","").Replace("/", "");
                TXT_PASSPORT_EXP.Text = passForm.Controls["TXT_PASSPORT_EXP"].Text;//.Replace("-", "").Replace("/", "");
                COM_PASSPORT_TYPE.Text = passForm.Controls["COM_PASSPORT_TYPE"].Text;
                COM_PASSPORT_RES.Text = passForm.Controls["COM_PASSPORT_RES"].Text;
                TXT_DATE_LAND.Text = passForm.Controls["TXT_DATE_LAND"].Text;
                //TXT_PERMIT_NO.Text = passForm.Controls["TXT_PERMIT_NO"].Text;
                CheckStr = passForm.Controls["TXT_PERMIT_NO"].Text;
                TXT_PERMIT_NO.Text = Regex.Replace(CheckStr, @"[^0-9a-zA-Z]", "");
                TXT_NOTE.Text = passForm.Controls["TXT_NOTE"].Text;
                BTN_SHOP_SEARCH.Focus();
            }
        }

        private void BTN_ITEM_ADD_Click(object sender, EventArgs e)
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
                if (!validationCheck(true, false))    //여권 번호 필수값 체크
                {
                    BTN_SCAN.Focus();
                    return;
                }

                if (TXT_PASSPORT_NO.Text.Length < 7)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_LENGTH"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BTN_SCAN.Focus();
                    return;
                }
            }
            else
            {
                if (!validationCheck(false, false))   //허가서 번호 필수값 체크
                {
                    BTN_SCAN.Focus();
                    return;
                }
            }

            if (Constants.RCT_ADD == "NO")
            {
                return;
            }
            
            Boolean bEmpty = false;
            if(TXT_SHOP_NAME.Text == null || string.Empty.Equals(TXT_SHOP_NAME.Text))
            {
                MetroMessageBox.Show(this, Constants.getMessage("TXT_SHOP_NAME"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BTN_SHOP_SEARCH.Focus();
                return;
            }

            ////품목 등록 갯수 제한
            //if(GRD_ITEMS.Rows.Count >10)
            //{
            //    MetroMessageBox.Show(this, Constants.getMessage("ITEM_MAX"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    TXT_RCT_NO.Focus();
            //    return;
            //}

            if (TXT_RCT_NO.Text == null || string.Empty.Equals(TXT_RCT_NO.Text))
            {
                MessageForm msgForm = new MessageForm(Constants.getMessage("RCT_CONFIRM"), null, MessageBoxButtons.YesNo);
                msgForm.ShowDialog(this);
                if (msgForm.DialogResult != DialogResult.Yes)
                {
                    TXT_RCT_NO.Focus();
                    return;
                }
                TXT_RCT_NO.Text =  "G"+ TXT_SHOP_NO.Text+ DateTime.Now.ToString("yyyyMMddHHmmss");
                bEmpty = true;
            }

            ItemForm itemForm = new ItemForm();
            if (itemForm.Controls["LAY_SHOP"] != null && itemForm.Controls["LAY_SHOP"].Controls["TXT_SHOP_NAME"] != null)
            {
                itemForm.Controls["LAY_SHOP"].Controls["TXT_SHOP_NAME"].Text = TXT_SHOP_NAME.Text;
            }
            if (itemForm.Controls["TXT_SHOP_NO"] != null )
            {
                itemForm.Controls["TXT_SHOP_NO"].Text = TXT_SHOP_NO.Text;
            }

            int nGoodsCnt = 0;
            int nConsumsCnt = 0;
            for (int i = 0; i < GRD_ITEMS.Rows.Count; i++)
            {
                if("1".Equals(GRD_ITEMS.Rows[i].Cells["GRD_TXT_ITEM_TYPE"].Value.ToString()))
                {
                    nGoodsCnt++;
                }
                else
                {
                    nConsumsCnt++;
                }
            }

            itemForm.Init(nGoodsCnt , nConsumsCnt);

            DialogResult result =itemForm.ShowDialog(this);
            
            if (DialogResult.OK == result)
            {

                int nSeq = 1;
                //for(int i= 0; i < GRD_ITEMS.Rows.Count; i ++)
                //{
                //    if (GRD_ITEMS[14, i].Value != null && TXT_SHOP_NO.Text != null 
                //        && TXT_SHOP_NO.Text.Equals(GRD_ITEMS[14, i].Value.ToString()))
                //    {
                //        GRD_ITEMS[1, i].Value = nSeq.ToString();                                  //순번
                //        nSeq++;
                //    }
                //}

                //2017.09.06 동일 영수증 번호 있는 경우 순번 증가
                for (int i = 0; i < GRD_ITEMS.Rows.Count; i++)
                {
                    if (GRD_ITEMS[0, i].Value != null && TXT_RCT_NO.Text != null
                        && TXT_RCT_NO.Text.Equals(GRD_ITEMS[0, i].Value.ToString())
                        )
                    {
                        nSeq++; 
                    }
                }

                int nRow = GRD_ITEMS.RowCount;

                for (int i=0;  itemForm.RetArray.Count > i; i ++)
                {
                    GRD_ITEMS.Rows.Add();
                    GRD_ITEMS[0, nRow].Value = TXT_RCT_NO.Text;//영수증번호
                    //GRD_ITEMS[1, nRow].Value = ((JObject)itemForm.RetArray[i])["COL1"].ToString();//물품순번
                    GRD_ITEMS[1, nRow].Value = nSeq.ToString();//물품순번
                    GRD_ITEMS[2, nRow].Value = TXT_SHOP_NAME.Text;//매장명
                    GRD_ITEMS[3, nRow].Value = ((JObject)itemForm.RetArray[i])["COL12"].ToString(); //물품종류
                    GRD_ITEMS[4, nRow].Value = ((JObject)itemForm.RetArray[i])["COL2"].ToString(); //대분류
                    GRD_ITEMS[5, nRow].Value = ((JObject)itemForm.RetArray[i])["COL3"].ToString(); //중분류
                    GRD_ITEMS[6, nRow].Value = ((JObject)itemForm.RetArray[i])["COL4"].ToString(); //상품명
                    GRD_ITEMS[7, nRow].Value = ((JObject)itemForm.RetArray[i])["COL9"].ToString(); //세금종류 
                    GRD_ITEMS[8, nRow].Value = ((JObject)itemForm.RetArray[i])["COL5"].ToString(); //갯수
                    GRD_ITEMS[9, nRow].Value = Int64.Parse(((JObject)itemForm.RetArray[i])["COL6"].ToString()); //품목당가격
                    GRD_ITEMS[10, nRow].Value = Int64.Parse(((JObject)itemForm.RetArray[i])["COL7"].ToString()); //세금
                    GRD_ITEMS[11, nRow].Value = Int64.Parse(((JObject)itemForm.RetArray[i])["COL8"].ToString());//판매액
                    GRD_ITEMS[12, nRow].Value = Int64.Parse(((JObject)itemForm.RetArray[i])["COL11"].ToString());//환급액
                    GRD_ITEMS[14, nRow].Value = TXT_SHOP_NO.Text;                                  //매장번호
                    GRD_ITEMS[15, nRow].Value = ((JObject)itemForm.RetArray[i])["COL10"].ToString();//소비세 8% 10%
                    nRow++;
                    nSeq++;
                }



                GRD_ITEMS_TOTAL_AMT();

                SHOP_CLEAR();
            }
            if (bEmpty)
                TXT_RCT_NO.Text = string.Empty;
        }       

        private void setWaitCursor(Boolean bWait)
        {
            FocusDummy(bWait);
            if (bWait)
            {
                //Cursor.Current = Cursors.WaitCursor;
                this.UseWaitCursor = true;
            }
            else
            {
                //Cursor.Current = Cursors.Default;
                this.UseWaitCursor = false;
            }
        }

        private void BTN_ITEM_DEL_Click(object sender, EventArgs e)
        {
            int nRow = GRD_ITEMS.RowCount;
            for(int i= nRow-1; i>=0; i --)
            {
                if(GRD_ITEMS[0, i].Value != null && "True".Equals(GRD_ITEMS[0, i].Value.ToString()))
                {
                    GRD_ITEMS.Rows.RemoveAt(i);
                }
            }
        }



        private void COM_REFUND_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("04"))//카드 환급방법
            {
                //LBL_REFUND_TYPE_KEY.Text = Constants.getScreenText("COMBO_ITEM_CARD_NO");
                TXT_REFUND_TYPE_KEY.Clear();
                TXT_REFUND_TYPE_KEY.Mask = "CCCC-CCCC-CCCC-CCCCCCC";
                TXT_REFUND_TYPE_KEY.Enabled = true;
                TXT_REFUND_TYPE_KEY.Visible = true;
                TXT_REFUND_TYPE_KEY.Focus();
            }
            else if (((Utils.ComboItem)COM_REFUND_TYPE.SelectedItem).Value.Equals("06"))//카드 환급방법
            {
                //LBL_REFUND_TYPE_KEY.Text = "QQ";
                TXT_REFUND_TYPE_KEY.Clear();
                TXT_REFUND_TYPE_KEY.Mask = "";
                TXT_REFUND_TYPE_KEY.Enabled = true;
                TXT_REFUND_TYPE_KEY.Visible = true;
                TXT_REFUND_TYPE_KEY.Focus();
            }
            else
            {
                //LBL_REFUND_TYPE_KEY.Text = "";
                TXT_REFUND_TYPE_KEY.Enabled = false;
                TXT_REFUND_TYPE_KEY.Visible = false;
                TXT_REFUND_TYPE_KEY.Text = "";

            }
        }

        //매장검색
        private void BTN_SHOP_SERACH_Click(object sender, EventArgs e)
        {
            if(!validationCheck(true, false))
            {
                BTN_SCAN.Focus();
                return;
            }
            SearchShop shopForm = new SearchShop();
            shopForm.SetSearchShopList(m_ArrShopList);
            shopForm.ShowDialog();
            if (shopForm != null && shopForm.DialogResult == DialogResult.OK)
            {
                TXT_SHOP_NO.Text = shopForm.SelectInfo["MERCHANT_NO"] == null ? "":shopForm.SelectInfo["MERCHANT_NO"].ToString();   //매장번호
                TXT_SHOP_NAME.Text = shopForm.SelectInfo["MERCHANT_JPNM"].ToString();                                               //매장명
                TXT_SHOP_TYPE1.Text = shopForm.SelectInfo["BIZ_INDUSTRY_CODE"].ToString();                                          //상업시설분류코드
                TXT_SHOP_TYPE2.Text = shopForm.SelectInfo["INDUSTRY_CODE"].ToString();                                              //분류코드
                TXT_SHOP_MANAGER.Text = shopForm.SelectInfo["SALE_MANAGER_CODE"].ToString();                                        //매니저
                TXT_SHOP_ADDR.Text = shopForm.SelectInfo["JP_ADDR1"].ToString()+ " "+ shopForm.SelectInfo["JP_ADDR2"].ToString(); ; //주소
                TXT_SHOP_PHONE_NO.Text = shopForm.SelectInfo["TEL_NO"].ToString();                                                  //전화번호
                TXT_TAX_OFFICE.Text = shopForm.SelectInfo["TAXOFFICE_NAME"].ToString();                                             //세관명
                COM_SUM.Text = shopForm.SelectInfo["COMBINED_USEYN"].ToString();                                                    //합산가능
                COM_CANCEL.Text = shopForm.SelectInfo["VOID_USEYN"].ToString();                                                     //?
                TXT_TAX_OFFICE_ADDR.Text = shopForm.SelectInfo["TAXOFFICE_ADDR"].ToString();                                        //세관주소
                TXT_SHOP_ADDR1.Text = shopForm.SelectInfo["JP_ADDR1"].ToString();                                                   //매장주소1
                TXT_SHOP_ADDR2.Text = shopForm.SelectInfo["JP_ADDR2"].ToString();                                                   //매장주소2
                send_custom_flag = shopForm.SelectInfo["SEND_CUSTOM_FLAG"] == null ? "" : shopForm.SelectInfo["SEND_CUSTOM_FLAG"].ToString();   //전자화여부
                TXT_RCT_NO.Focus();
            }
        }

        //Background 로 거래 처리 될 수 있도록
        public Boolean ASyncTran(string strType)
        {
            Boolean bRet = true;
            if ("code".Equals(strType))
            {
                Transaction tran = new Transaction();
                tran.SearchUserInfo(Constants.USER_ID, Constants.MERCHANT_NO, Constants.DESK_ID , "code");
            }
            else if ("bin".Equals(strType))
            {
                Transaction tran = new Transaction();
                tran.SearchUserInfo(Constants.USER_ID, Constants.MERCHANT_NO, Constants.DESK_ID, "bin");
            }
            return bRet;
        }

        private void TXT_RCT_NO_Enter(object sender, EventArgs e)
        {
            TXT_RCT_NO.ImeMode = ImeMode.Off;
        }

        private void TXT_RCT_NO_Leave(object sender, EventArgs e)
        {
            //TXT_RCT_NO.Text = string.Format(TXT_RCT_NO.Text, "#####0");
        }

        private void GRD_ITEMS_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex == 13 && e.RowIndex >=0)
            {
                GRD_ITEMS.Rows.RemoveAt(e.RowIndex);
                GRD_ITEMS_TOTAL_AMT();
            }
        }

        private void GRD_ITEMS_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 13)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.trash4.Width;
                var h = Properties.Resources.trash4.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.trash4, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private Boolean validationCheck(Boolean bPassPort  , Boolean bItem )
        {
            //데이터 미입력
            if (bPassPort)
            {
                if (TXT_PASSPORT_NAME.Text == null || string.Empty.Equals(TXT_PASSPORT_NAME.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_NAME"), "Passport_name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (TXT_PASSPORT_NO.Text == null || string.Empty.Equals(TXT_PASSPORT_NO.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT"), "Passport", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (TXT_PASSPORT_NAT.Text == null || string.Empty.Equals(TXT_PASSPORT_NAT.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_NATIONALITY"), "Nationality", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (COM_PASSPORT_SEX.Text == null || string.Empty.Equals(COM_PASSPORT_SEX.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_SEX"), "Sex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (TXT_PASSPORT_BIRTH.Text == null || string.Empty.Equals(TXT_PASSPORT_BIRTH.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_BIRTHDAY"), "Birthday", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (TXT_PASSPORT_EXP.Text == null || string.Empty.Equals(TXT_PASSPORT_EXP.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_EXP"), "Expiry_date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                //허가서 정보로 발행을 시도한 경우, 데이터 필수값 미입력 체크하기.

                if (TXT_PERMIT_NO.Text == null || string.Empty.Equals(TXT_PERMIT_NO.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PERMIT_NO"), "Permit_no", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (TXT_PASSPORT_NAME.Text == null || string.Empty.Equals(TXT_PASSPORT_NAME.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PASSPORT_NAME"), "Passport_name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (TXT_PASSPORT_NAT.Text == null || string.Empty.Equals(TXT_PASSPORT_NAT.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_NATIONALITY"), "Nationality", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (COM_PASSPORT_SEX.Text == null || string.Empty.Equals(COM_PASSPORT_SEX.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_SEX"), "Sex", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (TXT_PASSPORT_BIRTH.Text == null || string.Empty.Equals(TXT_PASSPORT_BIRTH.Text.Trim()))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_BIRTHDAY"), "Birthday", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (bItem)
            {
                if(GRD_ITEMS.Rows.Count <= 0)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("NO_ITEM"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    BTN_ITEM_ADD.Focus();
                    return false ;
                }
            }

            //갯수제한 체크. 입력시 체크하고 있기 때문에 여기서는 제외
            //if (GRD_ITEMS.Rows.Count > 10)
            //{
            //    MetroMessageBox.Show(this, Constants.getMessage("ITEM_MAX"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            //데이터 체크
            return true;

        }

        private void BTN_CLEAR_Click(object sender, EventArgs e)
        {
            SCREEN_CLEAR();
        }

        private void SCREEN_CLEAR()
        {
            //초기 정보
            TXT_REFUND_DATE.Value = System.DateTime.Now;
            TXT_DATE_LAND.Value = System.DateTime.Now;
            COM_REFUND_TYPE.SelectedIndex = 0;
            TXT_REFUND_TYPE_KEY.Text = string.Empty;
            //여권정보 CLEAR
            TXT_PASSPORT_NAME.Text = string.Empty;
            TXT_PASSPORT_NO.Text = string.Empty;
            TXT_PASSPORT_NAT.Text = string.Empty;
            //COM_PASSPORT_SEX.Text = string.Empty;
            COM_PASSPORT_SEX.SelectedIndex = 0;
            TXT_PASSPORT_BIRTH.Text = string.Empty;
            TXT_PASSPORT_EXP.Text = string.Empty;
            COM_PASSPORT_RES.SelectedIndex = 0;
            COM_PASSPORT_TYPE.SelectedIndex = 0;
            TXT_PASSPORT_NAT_NAME.Text = string.Empty;
            TXT_PERMIT_NO.Text = "";
            TXT_NOTE.Text = "";
            //SHOP 정보 CLEAR
            SHOP_CLEAR();

            //GRID CLEAR
            GRD_ITEMS.Rows.Clear(); //CLEAR 만 할 경우 우측 스크롤바가 갱신 안되는 버그가 있다.
            GRD_ITEMS.Rows.Add();
            GRD_ITEMS.Rows.RemoveAt(0);
            BTN_SCAN.Enabled = true;
            BTN_SCAN.Visible = true;
            GRD_ITEMS_TOTAL_AMT();
            CHK_SUM.Checked = false;
            BTN_SCAN.Focus();

        }

        private void GRD_ITEMS_COLUMN_INIT()
        {
            Transaction tran = new Transaction();


            //일반/소비품 구분
            Dictionary<string, string> item_list = new Dictionary<string, string>();
            item_list.Add("1", Constants.getScreenText("COMBO_ITEM_GOODS"));
            item_list.Add("2", Constants.getScreenText("COMBO_ITEM_CONSUMS"));
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[3]).DataSource = new BindingSource(item_list, null);
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[3]).DisplayMember = "Value";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[3]).ValueMember = "Key";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[3]).ReadOnly = true;
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[3]).DisplayStyleForCurrentCellOnly = true;

            //대분류
            item_list.Clear();
            item_list = new Dictionary<string, string>();
            JObject tempCode = new JObject();
            tempCode.Add("P_CODE", "A0001");
            JArray arrCode = tran.SearchItemList(tempCode);
            for (int i = 0; i < arrCode.Count; i++)
            {
                JObject temp = (JObject)arrCode[i];
                item_list.Add(temp["CATEGORY_CODE"].ToString(), temp["CATEGORY_NAME"].ToString());

            }
            arrCode.Clear();
            tempCode.RemoveAll();
            tempCode.Add("P_CODE", "A0002");
            arrCode = tran.SearchItemList(tempCode);
            for (int i = 0; i < arrCode.Count; i++)
            {
                JObject temp = (JObject)arrCode[i];
                item_list.Add(temp["CATEGORY_CODE"].ToString(), temp["CATEGORY_NAME"].ToString());
            }
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[4]).DataSource = new BindingSource(item_list, null);
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[4]).DisplayMember = "Value";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[4]).ValueMember = "Key";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[4]).ReadOnly = true;
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[4]).DisplayStyleForCurrentCellOnly = true;

            //중분류
            arrCode.Clear();
            tempCode.RemoveAll();
            item_list.Clear();
            item_list = new Dictionary<string, string>();
            arrCode.Clear();
            arrCode = tran.SearchItemList();
            if(print_goods_type.Equals("0"))
            {
                for (int i = 0; i < arrCode.Count; i++)
                {
                    JObject temp = (JObject)arrCode[i];
                    //item_list.Add(temp["CATEGORY_CODE"].ToString(), temp["CATEGORY_NAME"].ToString());
                    item_list.Add(temp["CATEGORY_CODE"].ToString(), "その他");//아이템명은 기타로 밀어버림
                }
            }
            else
            {
                for (int i = 0; i < arrCode.Count; i++)
                {
                    JObject temp = (JObject)arrCode[i];
                    item_list.Add(temp["CATEGORY_CODE"].ToString(), temp["CATEGORY_NAME"].ToString());
                    //item_list.Add(temp["CATEGORY_CODE"].ToString(), "その他");//아이템명은 기타로 밀어버림
                }
            }            

            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[5]).DataSource = new BindingSource(item_list, null);
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[5]).DisplayMember = "Value";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[5]).ValueMember = "Key";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[5]).ReadOnly = true;
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[5]).DisplayStyleForCurrentCellOnly = true;

            //내외세
            arrCode.Clear();
            tempCode.RemoveAll();
            item_list.Clear();
            item_list = new Dictionary<string, string>();
            item_list.Add("01", "Out");
            item_list.Add("02", "In");
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[7]).DataSource = new BindingSource(item_list, null);
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[7]).DisplayMember = "Value";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[7]).ValueMember = "Key";
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[7]).ReadOnly = true;
            ((DataGridViewComboBoxColumn)GRD_ITEMS.Columns[7]).DisplayStyleForCurrentCellOnly = true;
            arrCode.Clear();
            tempCode.RemoveAll();
        }

        private void GRD_ITEMS_TOTAL_AMT()
        {
            long n_qty = 0;
            long n_unit = 0;
            long n_tax = 0;
            long n_buy = 0;
            long n_refund = 0;
            for (int i = 0; i < GRD_ITEMS.Rows.Count; i++)
            {
                n_qty +=Int64.Parse(GRD_ITEMS[8, i].Value.ToString());
                n_unit += Int64.Parse(GRD_ITEMS[9, i].Value.ToString());
                n_tax += Int64.Parse(GRD_ITEMS[10, i].Value.ToString());
                n_buy += Int64.Parse(GRD_ITEMS[11, i].Value.ToString());
                n_refund += Int64.Parse(GRD_ITEMS[12, i].Value.ToString());
            }

            //TXT_TOTAL_QTY.Text          = n_qty.ToString();
            //TXT_TOTAL_TAX_AMT.Text      = n_tax.ToString();
            //TXT_TOTAL_BUY_AMT.Text      = n_buy.ToString();
            //TXT_TOTAL_REFUND_AMT.Text   = n_refund.ToString();

            GRD_TOTAL_AMT[8, 0].Value = n_qty;
            GRD_TOTAL_AMT[9, 0].Value = n_unit;
            GRD_TOTAL_AMT[10, 0].Value = n_tax;
            GRD_TOTAL_AMT[11, 0].Value = n_buy;
            GRD_TOTAL_AMT[12, 0].Value = n_refund;

        }

        private void SHOP_CLEAR()
        {
            TXT_SHOP_NAME.Text = string.Empty;
            TXT_SHOP_TYPE1.Text = string.Empty;
            TXT_SHOP_TYPE2.Text = string.Empty;
            TXT_SHOP_ADDR.Text = string.Empty;
            TXT_SHOP_MANAGER.Text = string.Empty;
            TXT_SHOP_PHONE_NO.Text = string.Empty;
            TXT_TAX_OFFICE.Text = string.Empty;
            TXT_SHOP_NO.Text = string.Empty;
            COM_SUM.SelectedIndex = -1;
            COM_CANCEL.SelectedIndex = -1;
            TXT_RCT_NO.Text = string.Empty;
            //shop 대상이 1개뿐이면 shop을 자동 선택
            if (m_ArrShopList.Count == 1)
            {
                JObject tempObj = ((JObject)m_ArrShopList[0]);
                TXT_SHOP_NO.Text = tempObj["MERCHANT_NO"].ToString();                                                   //매장번호
                TXT_SHOP_NAME.Text = tempObj["MERCHANT_JPNM"].ToString();                                               //매장명
                TXT_SHOP_TYPE1.Text = tempObj["BIZ_INDUSTRY_CODE"].ToString();                                          //상업시설분류코드
                TXT_SHOP_TYPE2.Text = tempObj["INDUSTRY_CODE"].ToString();                                              //분류코드
                TXT_SHOP_MANAGER.Text = tempObj["SALE_MANAGER_CODE"].ToString();                                        //매니저
                TXT_SHOP_ADDR.Text = tempObj["JP_ADDR1"].ToString() + " " + tempObj["JP_ADDR2"].ToString(); ;           //주소
                TXT_SHOP_PHONE_NO.Text = tempObj["TEL_NO"].ToString();                                                  //전화번호
                TXT_TAX_OFFICE.Text = tempObj["TAXOFFICE_NAME"].ToString();                                             //세관명
                COM_SUM.Text = tempObj["COMBINED_USEYN"].ToString();                                                    //합산가능
                COM_CANCEL.Text = tempObj["VOID_USEYN"].ToString();                                                     //?
                TXT_TAX_OFFICE_ADDR.Text = tempObj["TAXOFFICE_ADDR"].ToString();                                        //세관주소
                TXT_SHOP_ADDR1.Text = tempObj["JP_ADDR1"].ToString();                                                   //매장주소1
                TXT_SHOP_ADDR2.Text = tempObj["JP_ADDR2"].ToString();                                                   //매장주소2
                print_goods_type = tempObj["PRINT_GOODS_TYPE"].ToString();
                send_custom_flag = tempObj["SEND_CUSTOM_FLAG"].ToString();
                BTN_SHOP_SEARCH.Visible = false;//매장검색버튼 hide 처리
                tempObj = null;
            }
        }

        private void TXT_REFUND_TYPE_KEY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 0x0d && TXT_REFUND_TYPE_KEY.Text != null && 
                (TXT_REFUND_TYPE_KEY.Text.ToString().IndexOf("=") >0 || TXT_REFUND_TYPE_KEY.Text.ToString().IndexOf("^") > 0))
            {
                string tempData = TXT_REFUND_TYPE_KEY.Text.ToString();
                //2017.09.8 ms card read 개선
                if (TXT_REFUND_TYPE_KEY.Text.ToString().IndexOf("^") > 0) //Track 1 까지 읽히는 경우
                {
                    tempData = tempData.Substring(0, tempData.IndexOf("^"));
                    if (tempData.IndexOf("B") >= 0)
                    {
                        tempData = tempData.Substring(tempData.IndexOf("B") + 1);
                    }
                }else if (TXT_REFUND_TYPE_KEY.Text.ToString().IndexOf("=") > 0) //Track 2 읽히는 경우
                {
                    tempData = tempData.Substring(0, tempData.IndexOf("="));
                }

                TXT_REFUND_TYPE_KEY.Text = tempData;
                BTN_SCAN.Focus();
            }
        }

        private string searchNatonalityName(string strNatCode)
        {
            string strRet = "";
            if (m_ArrNationalList != null)
            {
                for (int i = 0; i < m_ArrNationalList.Count; i++)
                {
                    JObject tempObj = (JObject)m_ArrNationalList[i];
                    IList<string> keys = tempObj.Properties().Select(p => p.Name).ToList();
                    for (int j = 0; j < keys.Count; j++)
                    {
                        if (strNatCode.Equals(keys[j].ToString()))
                        {
                            strRet = tempObj[keys[j].ToString()].ToString();
                        }
                    }
                }
            }
            return strRet;
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
                    case Keys.Alt | Keys.Q:
                        BTN_SHOP_SERACH_Click(null, null);
                        return true;
                    case Keys.Alt | Keys.W:
                        BTN_ITEM_ADD_Click(null, null);
                        return true;
                    case Keys.Alt | Keys.A:
                        BTN_SUBMIT_Click(null, null);
                        return true;
                    case Keys.F1:
                        BTN_SCAN_Click(null, null);
                        return true;
                    case Keys.F2:
                        BTN_PASSPORT_MANUAL_Click(null, null);
                        return true;
                    case Keys.F5:
                        SCREEN_CLEAR();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void LBL_CHECKSUM_Click(object sender, EventArgs e)
        {
            CHK_SUM.Checked = !CHK_SUM.Checked;
        }

        private void TXT_DATE_LAND_Leave(object sender, EventArgs e)
        {
            string curDate = System.DateTime.Now.ToString("yyyyMMdd");
            string selectDate = TXT_DATE_LAND.Text.ToString().Replace("-", "").Replace("/", "");
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

        private void GRD_ITEMS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void FocusDummy(Boolean bFocus)
        {
            if (bFocus)
            {
                TXT_DUMMY.Visible = true;
                TXT_DUMMY.Focus();
            }else
            {
                TXT_DUMMY.Visible = false;
            }
        }

        private void BTN_TESTSIGN_Click()
        {
            SigCap sigPanel = new SigCap();
            
            sigPanel.name = TXT_PASSPORT_NAME.Text;
            if(TXT_PASSPORT_NAT.Text == "KOR")
            {
                sigPanel.clause = "当該消耗品を、購入した日から３０日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。";
                sigPanel.clause += "\n해당 소모품을 구입한 날로부터 30일 이내에 수출하는 것으로 구입해 일본에서 처분하지 않는 것을 서약합니다. ";
                sigPanel.clause += "\n当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。";
                sigPanel.clause += "\n해당 일반 물품을 일본에서 최종적으로는 수출하는 것으로 구입해 일본에서 처분하지 않기로 서약합니다.";
            }
            else if (TXT_PASSPORT_NAT.Text.Equals("CHN") || TXT_PASSPORT_NAT.Text.Equals("TWN"))
            {
                sigPanel.clause = "当該消耗品を、購入した日から３０日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。";
                sigPanel.clause += "\n从购买日起30天内搬出为前提购买的消耗品，誓约日方将不给予处分。 ";
                sigPanel.clause += "\n当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。";
                sigPanel.clause += "\n购买的一般商品最终从日本搬出，誓约日方不会给予处分。";
            }
            else
            {
                sigPanel.clause = "当該消耗品を、購入した日から３０日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。";
                sigPanel.clause += "\nI certify that the goods listed as 'consumable commodities'on this card were purchased by me for export from Japan within 30days from the purchase date and will not be disposed of within Japan. ";
                sigPanel.clause += "\n当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。";
                sigPanel.clause += "\nI certify that the goods listed as 'commodities except consumable' on this card were purchased by me for ultimate export from Japan and will not be disposed of within Japan.";
            }

            sigPanel.ShowDialog();
            sign_data = sigPanel.signdata;
        }

        private Boolean Sign_Req()
        {
            Boolean return_val = false;
            sign_data = "";
            string name = TXT_PASSPORT_NAME.Text;
            string clause = "";
            if (TXT_PASSPORT_NAT.Text == "KOR")
            {
                clause = "当該消耗品を、購入した日から３０日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。";
                clause += "\n해당 소모품을 구입한 날로부터 30일 이내에 수출하는 것으로 구입해 일본에서 처분하지 않는 것을 서약합니다. ";
                clause += "\n当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。";
                clause += "\n해당 일반 물품을 일본에서 최종적으로는 수출하는 것으로 구입해 일본에서 처분하지 않기로 서약합니다.";
            }
            else if (TXT_PASSPORT_NAT.Text.Equals("CHN") || TXT_PASSPORT_NAT.Text.Equals("TWN"))
            {
                clause = "当該消耗品を、購入した日から３０日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。";
                clause += "\n从购买日起30天内搬出为前提购买的消耗品，誓约日方将不给予处分。 ";
                clause += "\n当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。";
                clause += "\n购买的一般商品最终从日本搬出，誓约日方不会给予处分。";
            }
            else
            {
                clause = "当該消耗品を、購入した日から３０日以内に輸出されるものとして購入し、日本で処分しないことを誓約します。";
                clause += "\nI certify that the goods listed as 'consumable commodities'on this card were purchased by me for export from Japan within 30days from the purchase date and will not be disposed of within Japan. ";
                clause += "\n当該一般物品を、日本から最終的には輸出されるものとして購入し、日本で処分しないことを誓約します。";
                clause += "\nI certify that the goods listed as 'commodities except consumable' on this card were purchased by me for ultimate export from Japan and will not be disposed of within Japan.";
            }

            SigCtl sigCtl = new SigCtl();
            sigCtl.InkColor = 0x000000 ;
            sigCtl.Licence = "AgAkAEy2cKydAQVXYWNvbQ1TaWduYXR1cmUgU0RLAgKBAgJkAACIAwEDZQA";
            DynamicCapture dc = new DynamicCapture();
            DynamicCaptureResult res = dc.Capture(sigCtl, name, clause, null, null);
            if (res == DynamicCaptureResult.DynCaptOK)
            {
                SigObj sigObj = (SigObj)sigCtl.Signature;
                sigObj.set_ExtraData("AdditionalData", "C# test: Additional data");

                try
                {
                    if (Constants.SLIP_TYPE == "80mm")
                    {
                        sign_data = (string) sigObj.RenderBitmap("", 200, 150, "image/png", 1.4f, 0x000000, 0xffffff, 10.0f, 10.0f, RBFlags.RenderOutputBase64 | RBFlags.RenderColor1BPP);
                    }
                    else
                    {
                        sign_data = (string)sigObj.RenderBitmap("", 160, 60, "image/bmp", 1.4f, 0x000000, 0xffffff, 10.0f, 10.0f, RBFlags.RenderOutputBase64 | RBFlags.RenderColor1BPP);
                    }
                    return_val = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
            return return_val;
        }

        private void BTN_TESTPRINT_Click(object sender, EventArgs e)
        {
            GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
            printer.setPrinter(Constants.PRINTER_TYPE);
            printer.A4PrintTicket();
        }

        private void TXT_PERMIT_NO_Leave(object sender, EventArgs e)
        {
            if (TXT_PERMIT_NO.Text.Length > 0)
            {
                Boolean ismatch = Regex.IsMatch(TXT_PERMIT_NO.Text, @"^[a-zA-Z0-9]+$");
                if (!ismatch)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PERMIT_NO_TEXT"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PERMIT_NO.Text = "";
                    TXT_PERMIT_NO.Focus();
                }
                else if (TXT_PERMIT_NO.Text.Length > 16)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PERMIT_NO_LENGTH"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TXT_PERMIT_NO.Focus();
                }
            }
        }

        private void TXT_NOTE_Leave(object sender, EventArgs e)
        {
            if (TXT_NOTE.Text.Length > 30)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ERROR_NOTE_LENGTH"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_NOTE.Focus();
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

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
