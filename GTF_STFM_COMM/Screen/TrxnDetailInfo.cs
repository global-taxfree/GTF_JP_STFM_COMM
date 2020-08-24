using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using Newtonsoft.Json.Linq;
using MetroFramework;
using GTF_STFM_COMM.Util;
using GTF_STFM_COMM.Tran;
using log4net;
using GTF_Printer;

namespace GTF_STFM_COMM.Screen
{
    public partial class TrxnDetailInfo : MetroForm
    {
        ILog m_Logger = null;
        string m_SlipNo = String.Empty;
        Int64 m_nPrintCnt = 0;
        string strRefundStatusCode = String.Empty;
        JArray m_ArrShopList = null;
        string print_goods_type = "0";

        public TrxnDetailInfo(string strSlipNo , ILog logger = null)
        {
            m_Logger = logger;
            m_SlipNo = strSlipNo;
            InitializeComponent();
            this.Text = Constants.getScreenText("SLIPDETAILINFO");
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BTN_REFUND_Click(object sender, EventArgs e)
        {
            MessageSmallForm msgSmallForm = new MessageSmallForm(Constants.getMessage("REFUND_COMPLETE") , null);
            msgSmallForm.ShowDialog(this);
            string refunddt = System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss");
            Transaction tran = new Transaction();//거래내용 조회
            JObject tempObj = new JObject();
            JObject tempObj2 = new JObject();
            tempObj.Add("SLIP_NO", m_SlipNo);
            tempObj2.Add("SLIP_STATUS_CODE", "02");
            tempObj2.Add("REFUNDDT", refunddt);

            if(m_nPrintCnt == 0)
            {
                tempObj2.Add(Constants.PRINT_CNT, "1"); 
            }

            tran.UpdateSlip(tempObj, tempObj2);
            tempObj.RemoveAll();
            tempObj2.RemoveAll();
            tempObj = null;
            tempObj2 = null;
            tran = null;
            
            TXT_REFUND_STATUS.Text = Constants.getScreenText("COMBO_ITEM_PRINT");
            TXT_SLIP_STATUS.Text = Constants.getScreenText("COMBO_ITEM_REFUND");

            BTN_REFUND.Text = refunddt;
            BTN_REFUND.Enabled = false;
            //BTN_REFUND.Visible = false;
        }

        private void SlipDetailInfo_Load(object sender, EventArgs e)
        {
            JObject jsonReq = new JObject();

            try {
                if (Constants.RCT_ADD == "YES")
                {
                    Transaction tran = new Transaction();//거래내용 조회
                    m_ArrShopList = tran.SearchShopList();
                    if (m_ArrShopList.Count == 1)
                    {
                        JObject tempObj = ((JObject)m_ArrShopList[0]);
                        print_goods_type = tempObj["PRINT_GOODS_TYPE"].ToString();
                    }
                    jsonReq.Add("companyID", "000001");
                    jsonReq.Add("languageCD", "jp");
                    jsonReq.Add("slip_no", m_SlipNo);

                    m_Logger.Debug("SlipDetailInfo_Load 1");
                    String slipResult = tran.onlineSearchDetail(jsonReq.ToString());
                    JArray arrRetSlip = JArray.Parse(slipResult);

                    String goodsResult = tran.onlineSearchGoodsDetail(jsonReq.ToString());
                    JArray arrRetSlipDetail = JArray.Parse(goodsResult);

                    m_Logger.Debug("SlipDetailInfo_Load 2");

                    //if (arrRetSlip.Count > 0 && arrRetSlipDetail.Count > 0)
                    if (arrRetSlip.Count > 0)
                    {
                        JObject tempObj = (JObject)arrRetSlip[0];
                        TXT_PASS_NAME.Text = tempObj["BUYER_NAME"].ToString();
                        TXT_PASS_NO.Text = tempObj["PASSPORT_SERIAL_NO"].ToString();
                        if (TXT_PASS_NO.Text.Length == 0) TXT_PASS_NO.Text = tempObj["PERMIT_NO"].ToString();
                        TXT_PASS_NAT.Text = tempObj["NATIONALITY_NAME"].ToString();
                        TXT_PASS_SEX.Text = tempObj["GENDER_CODE"].ToString();

                        string strBirth = tempObj["BUYER_BIRTH"].ToString();
                        string strExp = tempObj["PASS_EXPIRYDT"].ToString();
                        string strLand = tempObj["ENTRYDT"].ToString();
                        strBirth = strBirth.Length < 6 ? strBirth : strBirth.Substring(0, 4) + "/" + strBirth.Substring(4, 2) + "/" + strBirth.Substring(6);
                        strExp = strExp.Length < 6 ? strExp : strExp.Substring(0, 4) + "/" + strExp.Substring(4, 2) + "/" + strExp.Substring(6);
                        strLand = strLand.Length < 6 ? strLand : strLand.Substring(0, 4) + "/" + strLand.Substring(4, 2) + "/" + strLand.Substring(6);

                        TXT_PASS_BIRTH.Text = strBirth;
                        TXT_PASS_EXP.Text = strExp;
                        TXT_PASS_LAND.Text = strLand;

                        TXT_PASS_RES.Text = tempObj["RESIDENCE_NAME"].ToString();
                        TXT_PASS_TYPE.Text = tempObj["PASSPORT_TYPE"].ToString();

                        TXT_PARTNER_NAME.Text = tempObj["ENTRYDT"].ToString();

                        TXT_PARTNER_NAME.Text = tempObj["PARTNER_JPNM"].ToString();
                        TXT_SHOP_GROUP.Text = tempObj["MGROUP_JPNM"] == null ? "" : tempObj["MGROUP_JPNM"].ToString();
                        TXT_SHOP_NAME.Text = tempObj["MERCHANT_JPNM"].ToString();

                        /*
                        JObject tempObj2 = new JObject();
                        tempObj2.Add("MERCHANT_NO", tempObj["MERCHANT_NO"].ToString());
                        JArray arrRet = tran.SearchShopList(tempObj2);
                        tempObj2.RemoveAll();
                        tempObj2 = null;
                        //코드 입력
                        if (arrRet.Count > 0)
                        {
                            tempObj2 = (JObject)arrRet[0];
                            TXT_PARTNER_NAME.Text = tempObj2["PARTNER_JPNM"].ToString();
                            TXT_SHOP_GROUP.Text = tempObj2["MGROUP_JPNM"] == null ? "" : tempObj2["MGROUP_JPNM"].ToString();
                            TXT_SHOP_NAME.Text = tempObj2["MERCHANT_JPNM"].ToString();
                            tempObj2.RemoveAll();
                            tempObj2 = null;
                        }
                        */

                        TXT_SLIP_NO.Text = tempObj["SLIP_NO"].ToString();
                        
                        TXT_SLIP_STATUS.Text = tempObj["SLIP_STATUS_CODE"].ToString();
                        TXT_REFUND_DATE.Text = tempObj["SALEDT"].ToString();
                        //TXT_REFUND_DATE.Text = tempObj["REFUNDDT"].ToString();
                        TXT_REFUND_STATUS.Text = tempObj["REFUND_STATUS_CODE"].ToString();
                        strRefundStatusCode = tempObj["REFUND_STATUS"].ToString();
                        //if("02".Equals(TXT_SLIP_STATUS.Text.ToString() ))
                        if ("02".Equals(tempObj["PUBLISH_STATUS"].ToString()))
                        {
                            BTN_REFUND.Text = tempObj["REFUNDDT"].ToString();
                            BTN_REFUND.Enabled = false;
                        }
                        else
                        {
                            if (tempObj["PUBLISHDT"] != null && !Constants.OPEN_DATE.Equals(tempObj["PUBLISHDT"].ToString().Trim()))
                            {
                                BTN_REFUND.Visible = false;
                            }
                        }
                        string strRefundWayText = "";
                        if ("01".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
                        {
                            strRefundWayText = Constants.getScreenText("COMBO_ITEM_CASH");
                        }
                        else if ("04".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
                        {
                            strRefundWayText = Constants.getScreenText("COMBO_ITEM_CARD");
                        }
                        else if ("06".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
                        {
                            strRefundWayText = Constants.getScreenText("COMBO_ITEM_QQ");
                        }

                        TXT_SLIP_PAY.Text = strRefundWayText;
                        TXT_SLIP_USER.Text = tempObj["WORKERID"].ToString();

                        this.m_nPrintCnt = Int64.Parse(tempObj["RETRY_CNT"].ToString());

                        Int64 nGoodsBuyAmt = 0;
                        Int64 nGoodsTaxAmt = 0;
                        Int64 nGoodsRefundAmt = 0;
                        Int64 nGoodsFeeAmt = 0;

                        Int64 nConsumsBuyAmt = 0;
                        Int64 nConsumsTaxAmt = 0;
                        Int64 nConsumsRefundAmt = 0;
                        Int64 nConsumsFeeAmt = 0;
                        /*
                        nGoodsBuyAmt = Int64.Parse(tempObj["TOTAL_COMM_SALE_AMT"].ToString());
                        nGoodsTaxAmt = Int64.Parse(tempObj["TOTAL_COMM_TAX_AMT"].ToString());
                        nGoodsRefundAmt = Int64.Parse(tempObj["TOTAL_COMM_REFUND_AMT"].ToString());
                        nGoodsFeeAmt = Int64.Parse(tempObj["TOTAL_COMM_FEE_AMT"].ToString());

                        nConsumsBuyAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_SALE_AMT"].ToString());
                        nConsumsTaxAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_TAX_AMT"].ToString());
                        nConsumsRefundAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_REFUND_AMT"].ToString());
                        nConsumsFeeAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_FEE_AMT"].ToString());
                        */

                        nGoodsBuyAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_SALE_AMT"].ToString());
                        nGoodsTaxAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_TAX_AMT"].ToString());
                        nGoodsRefundAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_REFUND_AMT"].ToString());
                        nGoodsFeeAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_FEE_AMT"].ToString());

                        nConsumsBuyAmt = Int64.Parse(tempObj["TOTAL_COMM_SALE_AMT"].ToString());
                        nConsumsTaxAmt = Int64.Parse(tempObj["TOTAL_COMM_TAX_AMT"].ToString());
                        nConsumsRefundAmt = Int64.Parse(tempObj["TOTAL_COMM_REFUND_AMT"].ToString());
                        nConsumsFeeAmt = Int64.Parse(tempObj["TOTAL_COMM_FEE_AMT"].ToString());


                        TXT_GOODS_BUY.Text = nGoodsBuyAmt.ToString("N0");
                        TXT_GOODS_TAX.Text = nGoodsTaxAmt.ToString("N0");
                        TXT_GOODS_FEE.Text = nGoodsFeeAmt.ToString("N0");
                        TXT_GOODS_REFUND.Text = nGoodsRefundAmt.ToString("N0");

                        TXT_CONSUM_BUY.Text = nConsumsBuyAmt.ToString("N0");
                        TXT_CONSUM_TAX.Text = nConsumsTaxAmt.ToString("N0");
                        TXT_CONSUM_FEE.Text = nConsumsFeeAmt.ToString("N0");
                        TXT_CONSUM_REFUND.Text = nConsumsRefundAmt.ToString("N0");

                        TXT_TOTAL_BUY.Text = (nGoodsBuyAmt + nConsumsBuyAmt).ToString("N0");
                        TXT_TOTAL_TAX.Text = (nGoodsTaxAmt + nConsumsTaxAmt).ToString("N0");
                        TXT_TOTAL_FEE.Text = (nGoodsFeeAmt + nConsumsFeeAmt).ToString("N0");
                        TXT_TOTAL_REFUND.Text = (nGoodsRefundAmt + nConsumsRefundAmt).ToString("N0");


                        GRD_ITEMS.Rows.Clear();
                        if(arrRetSlipDetail.Count > 0)
                        {
                            GRD_ITEMS.Rows.Add(arrRetSlipDetail.Count);

                            tempObj = (JObject)arrRetSlipDetail[0];
                            TXT_RCT_NO.Text = tempObj["REC_NO"].ToString();

                            for (int i = 0; i < arrRetSlipDetail.Count; i++)
                            {
                                tempObj = (JObject)arrRetSlipDetail[i];
                                GRD_ITEMS[0, i].Value = tempObj["SALE_SEQ"].ToString();//물품번호
                                GRD_ITEMS[1, i].Value = tempObj["GOODS_ITEMS_CODE"].ToString();//물품 종류

                                GRD_ITEMS[2, i].Value = tempObj["GOODS_GROUP_CODE"].ToString();
                                GRD_ITEMS[3, i].Value = tempObj["GOODS_DIVISION"].ToString();
                                GRD_ITEMS[4, i].Value = tempObj["NAME"].ToString();
                                GRD_ITEMS[5, i].Value = Int64.Parse(tempObj["GOODS_QTY"].ToString());
                                if ("02".Equals(tempObj["TAX_PROC_TIME_CODE"].ToString()))//내세
                                {
                                    GRD_ITEMS[6, i].Value = (Int64.Parse(tempObj["GOODS_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                                }
                                else//외세
                                {
                                    GRD_ITEMS[6, i].Value = Int64.Parse(tempObj["GOODS_AMT"].ToString());
                                }

                                GRD_ITEMS[7, i].Value = Int64.Parse(tempObj["TAX_AMT"].ToString());
                                GRD_ITEMS[8, i].Value = Int64.Parse(tempObj["FEE_AMT"].ToString());
                                GRD_ITEMS[9, i].Value = Int64.Parse(tempObj["REFUND_AMT"].ToString());
                                if (tempObj["TAX_TYPE"] == null || tempObj["TAX_TYPE"].ToString() == "")
                                {
                                    /*
                                    long tax_type = Int64.Parse(tempObj["GOODS_AMT"].ToString()) / Int64.Parse(tempObj["TAX_AMT"].ToString());
                                    string tax_type_string = "";

                                    if (tax_type == 10 || tax_type == 9 || tax_type == 11)
                                    {
                                        tax_type_string = "10";
                                    }
                                    else
                                    {
                                        tax_type_string = "8";
                                    }

                                    GRD_ITEMS[10, i].Value = tax_type_string + "%";
                                    */
                                    GRD_ITEMS[10, i].Value = "8%";
                                }
                                else if (tempObj["TAX_TYPE"].ToString() == "1")
                                {
                                    GRD_ITEMS[10, i].Value = "8%";
                                }
                                else if (tempObj["TAX_TYPE"].ToString() == "2")
                                {
                                    GRD_ITEMS[10, i].Value = "10%";
                                }
                                else
                                {
                                    GRD_ITEMS[10, i].Value = "8%";
                                }
                                tempObj = null;
                            }
                            tempObj = null;
                        }

                    }
                    m_Logger.Debug("SlipDetailInfo_Load 3");
                }
                else
                {
                    Transaction tran = new Transaction();//거래내용 조회

                    jsonReq.Add("companyID", "000001");
                    jsonReq.Add("languageCD", "jp");
                    jsonReq.Add("slip_no", m_SlipNo);

                    m_Logger.Debug("SlipDetailInfo_Load 1");
                    String slipResult = tran.onlineSearchDetail(jsonReq.ToString());
                    JArray arrRetSlip = JArray.Parse(slipResult);
                    m_Logger.Debug("SlipDetailInfo_Load 2");

                    if (arrRetSlip.Count > 0 )
                    {
                        JObject tempObj = (JObject)arrRetSlip[0];
                        TXT_PASS_NAME.Text = tempObj["BUYER_NAME"].ToString();
                        TXT_PASS_NO.Text = tempObj["PASSPORT_SERIAL_NO"].ToString();
                        TXT_PASS_NAT.Text = tempObj["NATIONALITY_NAME"].ToString();
                        TXT_PASS_SEX.Text = tempObj["GENDER_CODE"].ToString();

                        string strBirth = tempObj["BUYER_BIRTH"].ToString();
                        string strExp = tempObj["PASS_EXPIRYDT"].ToString();
                        string strLand = tempObj["ENTRYDT"].ToString();
                        strBirth = strBirth.Length < 6 ? strBirth : strBirth.Substring(0, 4) + "/" + strBirth.Substring(4, 2) + "/" + strBirth.Substring(6);
                        strExp = strExp.Length < 6 ? strExp : strExp.Substring(0, 4) + "/" + strExp.Substring(4, 2) + "/" + strExp.Substring(6);
                        strLand = strLand.Length < 6 ? strLand : strLand.Substring(0, 4) + "/" + strLand.Substring(4, 2) + "/" + strLand.Substring(6);

                        TXT_PASS_BIRTH.Text = strBirth;
                        TXT_PASS_EXP.Text = strExp;
                        TXT_PASS_LAND.Text = strLand;

                        TXT_PASS_RES.Text = tempObj["RESIDENCE_NAME"].ToString();
                        TXT_PASS_TYPE.Text = tempObj["PASSPORT_TYPE"].ToString();

                        TXT_PARTNER_NAME.Text = tempObj["ENTRYDT"].ToString();

                        TXT_PARTNER_NAME.Text = tempObj["PARTNER_JPNM"].ToString();
                        TXT_SHOP_GROUP.Text = tempObj["MGROUP_JPNM"] == null ? "" : tempObj["MGROUP_JPNM"].ToString();
                        TXT_SHOP_NAME.Text = tempObj["MERCHANT_JPNM"].ToString();



                        TXT_SLIP_NO.Text = tempObj["SLIP_NO"].ToString();
                        TXT_RCT_NO.Text = tempObj["TOTAL_SLIPSEQ"].ToString();
                        TXT_SLIP_STATUS.Text = tempObj["SLIP_STATUS_CODE"].ToString();
                        TXT_REFUND_DATE.Text = tempObj["SALEDT"].ToString();
                        //TXT_REFUND_DATE.Text = tempObj["REFUNDDT"].ToString();
                        TXT_REFUND_STATUS.Text = tempObj["REFUND_STATUS_CODE"].ToString();
                        strRefundStatusCode = tempObj["REFUND_STATUS"].ToString();
                        //if("02".Equals(TXT_SLIP_STATUS.Text.ToString() ))
                        if ("02".Equals(tempObj["PUBLISH_STATUS"].ToString()))
                        {
                            BTN_REFUND.Text = tempObj["REFUNDDT"].ToString();
                            BTN_REFUND.Enabled = false;
                        }
                        else
                        {
                            if (tempObj["PUBLISHDT"] != null && !Constants.OPEN_DATE.Equals(tempObj["PUBLISHDT"].ToString().Trim()))
                            {
                                BTN_REFUND.Visible = false;
                            }
                        }
                        string strRefundWayText = "";
                        if ("01".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
                        {
                            strRefundWayText = Constants.getScreenText("COMBO_ITEM_CASH");
                        }
                        else if ("04".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
                        {
                            strRefundWayText = Constants.getScreenText("COMBO_ITEM_CARD");
                        }
                        else if ("06".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
                        {
                            strRefundWayText = Constants.getScreenText("COMBO_ITEM_QQ");
                        }

                        TXT_SLIP_PAY.Text = strRefundWayText;
                        TXT_SLIP_USER.Text = tempObj["WORKERID"].ToString();

                        this.m_nPrintCnt = Int64.Parse(tempObj["RETRY_CNT"].ToString());

                        Int64 nGoodsBuyAmt = 0;
                        Int64 nGoodsTaxAmt = 0;
                        Int64 nGoodsRefundAmt = 0;
                        Int64 nGoodsFeeAmt = 0;

                        Int64 nConsumsBuyAmt = 0;
                        Int64 nConsumsTaxAmt = 0;
                        Int64 nConsumsRefundAmt = 0;
                        Int64 nConsumsFeeAmt = 0;


                        /*
                        nGoodsBuyAmt = Int64.Parse(tempObj["TOTAL_COMM_SALE_AMT"].ToString());
                        nGoodsTaxAmt = Int64.Parse(tempObj["TOTAL_COMM_TAX_AMT"].ToString());
                        nGoodsRefundAmt = Int64.Parse(tempObj["TOTAL_COMM_REFUND_AMT"].ToString());
                        nGoodsFeeAmt = Int64.Parse(tempObj["TOTAL_COMM_FEE_AMT"].ToString());

                        nConsumsBuyAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_SALE_AMT"].ToString());
                        nConsumsTaxAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_TAX_AMT"].ToString());
                        nConsumsRefundAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_REFUND_AMT"].ToString());
                        nConsumsFeeAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_FEE_AMT"].ToString());
                        */
                        nGoodsBuyAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_SALE_AMT"].ToString());
                        nGoodsTaxAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_TAX_AMT"].ToString());
                        nGoodsRefundAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_REFUND_AMT"].ToString());
                        nGoodsFeeAmt = Int64.Parse(tempObj["TOTAL_EXCOMM_FEE_AMT"].ToString());

                        nConsumsBuyAmt = Int64.Parse(tempObj["TOTAL_COMM_SALE_AMT"].ToString());
                        nConsumsTaxAmt = Int64.Parse(tempObj["TOTAL_COMM_TAX_AMT"].ToString());
                        nConsumsRefundAmt = Int64.Parse(tempObj["TOTAL_COMM_REFUND_AMT"].ToString());
                        nConsumsFeeAmt = Int64.Parse(tempObj["TOTAL_COMM_FEE_AMT"].ToString());

                        TXT_GOODS_BUY.Text = nGoodsBuyAmt.ToString("N0");
                        TXT_GOODS_TAX.Text = nGoodsTaxAmt.ToString("N0");
                        TXT_GOODS_FEE.Text = nGoodsFeeAmt.ToString("N0");
                        TXT_GOODS_REFUND.Text = nGoodsRefundAmt.ToString("N0");

                        TXT_CONSUM_BUY.Text = nConsumsBuyAmt.ToString("N0");
                        TXT_CONSUM_TAX.Text = nConsumsTaxAmt.ToString("N0");
                        TXT_CONSUM_FEE.Text = nConsumsFeeAmt.ToString("N0");
                        TXT_CONSUM_REFUND.Text = nConsumsRefundAmt.ToString("N0");

                        TXT_TOTAL_BUY.Text = (nGoodsBuyAmt + nConsumsBuyAmt).ToString("N0");
                        TXT_TOTAL_TAX.Text = (nGoodsTaxAmt + nConsumsTaxAmt).ToString("N0");
                        TXT_TOTAL_FEE.Text = (nGoodsFeeAmt + nConsumsFeeAmt).ToString("N0");
                        TXT_TOTAL_REFUND.Text = (nGoodsRefundAmt + nConsumsRefundAmt).ToString("N0");


                        GRD_ITEMS.Rows.Clear();
                        tempObj = null;
                    }
                    m_Logger.Debug("SlipDetailInfo_Load 3");
                }
               
            }
            catch(Exception ex)
            {
                m_Logger.Error(ex.StackTrace);
            }
        }

        private void BTN_PRINT_Click(object sender, EventArgs e)
        {
            if(strRefundStatusCode.Equals("04"))
            {
                MetroMessageBox.Show(this, Constants.getMessage("ALREADYCANCELREFUND"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Constants.PRINTER_TYPE == null || string.Empty.Equals(Constants.PRINTER_TYPE.Trim()))
            {

                MetroMessageBox.Show(this, Constants.getMessage("PASSPORT_NOTHING"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                setWaitCursor(false);
                return;
            }

            PrintSlipLangForm langForm = new PrintSlipLangForm(m_Logger);
            langForm.ShowDialog(this);
            string strSlipLang = string.Empty;

            if (langForm.m_SelectLang != null && !string.Empty.Equals(langForm.m_SelectLang.Trim()))
            {
                strSlipLang = langForm.m_SelectLang;
            }
            else
            {
                MetroMessageBox.Show(this, "Select Lang.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (langForm.DialogResult != DialogResult.OK)
            {
                return;
            }
            if (Constants.RCT_ADD == "YES")
            {
                Transaction tran = new Transaction();//거래내용 조회
                GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                printer.setPrinter(Constants.PRINTER_TYPE);
                string strSlip_no = m_SlipNo;
                string retailer = "";
                string docid = "";
                string goods = "";
                string tourist = "";
                string singinfo = "";
                string adsinfo = "";

                JArray arrComm = new JArray();
                JArray arrExComm = new JArray();
                JToken signToken;

                string strRes = tran.getSlipPrintInfo(strSlip_no);
                JObject result = JObject.Parse(strRes);
                JToken retailerToken = result["retailerInfo"].DeepClone();
                JToken touristToken = result["touristInfo"].DeepClone();
                JToken refundToken = result["refundInfo"].DeepClone();
                JToken saleToken = result["saleInfo"].DeepClone();
                if (result["signInfo"] != null)
                {
                    signToken = result["signInfo"].DeepClone();
                }
                else
                {
                    signToken = null;
                }
                JArray goodsList = JArray.Parse(result["goodsList"].ToString());

                tourist = touristToken["PASSPORT_TYPE"].ToString() + "|";
                tourist += touristToken["PASSPORT_SERIAL_NO"].ToString() + "|";
                tourist += touristToken["BUYER_NAME"].ToString() + "|";
                tourist += touristToken["NATIONALITY"].ToString() + "|";
                tourist += touristToken["BUYER_BIRTH"].ToString() + "|";
                tourist += touristToken["RESIDENCE"].ToString() + "|";
                tourist += touristToken["ENTRYDT"].ToString() + "|";

                retailer = retailerToken["TAXOFFICE_NAME"].ToString() + "|";
                retailer += retailerToken["TAX_ADDR1"].ToString() + "|";
                retailer += retailerToken["TAX_ADDR2"].ToString() + "|";

                retailer += retailerToken["MERCHANT_JPNM"].ToString() + "|";
                retailer += retailerToken["JP_ADDR1"].ToString() + "|";
                retailer += retailerToken["JP_ADDR2"].ToString() + "|";
                retailer += retailerToken["OPT_CORP_JPNM"].ToString() + "|";

                docid += strSlipLang + "|";                         //출력언어
                if (Constants.PRINT_TYPE != "ALL")
                {
                    docid += "1" + "|";           //출력전표 갯수
                    docid += "01" + "|";     //출력
                }
                else
                {
                    docid += "2" + "|";            //출력전표 갯수
                    docid += "01/02" + "|";     //출력
                }
                docid += "[REPUBLISH]" + "|";                 //출력유형
                docid += strSlip_no + "|";

                docid += "N" + "|";                         //합산전표 출력여부

                docid += refundToken["REFUND_WAY_CODE"].ToString() + "|"; // 
                docid += refundToken["REFUND_WAY_CODE_DESC"].ToString() + "|";
                docid += refundToken["MASK_REMIT_NO"].ToString() + "|";
                docid += retailerToken["UNIKEY"].ToString();

                goods = retailerToken["SALEDT"].ToString() + "|";
                long com_cnt = Int64.Parse(saleToken["COM_COUNT"].ToString());
                long excom_cnt = Int64.Parse(saleToken["EXCOM_COUNT"].ToString());

                if (com_cnt > 0 && excom_cnt > 0)
                {
                    goods += "2" + "|";
                }
                else if (com_cnt > 0 || excom_cnt > 0)
                {
                    goods += "1" + "|";
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_ISSUE"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //setWaitCursor(false);
                    return;
                }

                for (int i = 0; i < goodsList.Count; i++)
                {
                    JObject tempObj = (JObject)goodsList[i];
                    string item_code = tempObj["GOODS_ITEMS_CODE"].ToString();

                    if (item_code.Equals("A0002"))
                    {
                        arrComm.Add(tempObj);
                    }
                    else
                    {
                        arrExComm.Add(tempObj);
                    }
                }

                if (arrComm != null && arrComm.Count > 0)
                {
                    goods += "01" + "|";           //물품종류(소비)
                    goods += arrComm.Count + "|"; //물품갯수
                    for (int i = 0; i < arrComm.Count; i++)
                    {
                        JObject tempObj = (JObject)arrComm[i];

                        if(print_goods_type.Equals("1"))
                        {
                            goods += tempObj["GOODS_GROUP_CODE"].ToString() + " - " + tempObj["GOODS_DIVISION"].ToString() + "|";
                        }
                        else if(print_goods_type.Equals("2"))
                        {
                            goods += tempObj["GOODS_GROUP_CODE"].ToString() + " - " + tempObj["GOODS_DIVISION"].ToString() + " - " + tempObj["GOODS_NAME"].ToString() +  "|";
                        }
                        else
                        {
                            goods += tempObj["GOODS_GROUP_CODE"].ToString() + "|";
                        }
                        goods += tempObj["GOODS_UNIT_PRICE"].ToString() + "|";
                        goods += tempObj["GOODS_QTY"].ToString() + "|";
                        goods += tempObj["GOODS_AMT"].ToString() + "|";
                    }

                    goods += saleToken["TOTAL_COMM_SALE_AMT"].ToString() + "|";
                    goods += saleToken["TOTAL_COMM_TAX_AMT"].ToString() + "|";
                    goods += saleToken["TOTAL_COMM_REFUND_AMT"].ToString() + "|";
                    goods += "|"; ;
                }

                if (arrExComm != null && arrExComm.Count > 0)
                {
                    goods += "02" + "|";           //물품종류(일반)
                    goods += arrExComm.Count + "|"; //물품갯수
                    for (int i = 0; i < arrExComm.Count; i++)
                    {
                        JObject tempObj = (JObject)arrExComm[i];

                        if (print_goods_type.Equals("1"))
                        {
                            goods += tempObj["GOODS_GROUP_CODE"].ToString() + " - " + tempObj["GOODS_DIVISION"].ToString() + "|";
                        }
                        else if (print_goods_type.Equals("2"))
                        {
                            goods += tempObj["GOODS_GROUP_CODE"].ToString() + " - " + tempObj["GOODS_DIVISION"].ToString() + " - " + tempObj["GOODS_NAME"].ToString() + "|";
                        }
                        else
                        {
                            goods += tempObj["GOODS_GROUP_CODE"].ToString() + "|";
                        }
                        goods += tempObj["GOODS_UNIT_PRICE"].ToString() + "|";
                        goods += tempObj["GOODS_QTY"].ToString() + "|";
                        goods += tempObj["GOODS_AMT"].ToString() + "|";
                    }

                    goods += saleToken["TOTAL_EXCOMM_SALE_AMT"].ToString() + "|";
                    goods += saleToken["TOTAL_EXCOMM_TAX_AMT"].ToString() + "|";
                    goods += saleToken["TOTAL_EXCOMM_REFUND_AMT"].ToString() + "|";
                    goods += "|";
                }
                goods += Int64.Parse(saleToken["TOTAL_EXCOMM_TAX_AMT"].ToString()) + Int64.Parse(saleToken["TOTAL_COMM_TAX_AMT"].ToString()) + "|";
                goods += Int64.Parse(saleToken["TOTAL_EXCOMM_SALE_AMT"].ToString()) + Int64.Parse(saleToken["TOTAL_COMM_SALE_AMT"].ToString()) + "|";
                goods += ((Int64.Parse(saleToken["TOTAL_EXCOMM_TAX_AMT"].ToString()) + Int64.Parse(saleToken["TOTAL_COMM_TAX_AMT"].ToString())) -
                       (Int64.Parse(saleToken["TOTAL_EXCOMM_REFUND_AMT"].ToString()) + Int64.Parse(saleToken["TOTAL_COMM_REFUND_AMT"].ToString()))) + "|";
                goods += Int64.Parse(saleToken["TOTAL_EXCOMM_REFUND_AMT"].ToString()) + Int64.Parse(saleToken["TOTAL_COMM_REFUND_AMT"].ToString()) + "|";


                if (signToken != null)
                {
                    singinfo = signToken["SIGN_DATA"].ToString();
                }


                if (Constants.SLIP_TYPE == "80mm")
                {
                    /*
#if DEBUG
                    printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, true, singinfo);
#else
            printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "0"), retailer, goods, tourist, adsinfo, false, singinfo);
#endif
*/
                    printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "0"), retailer, goods, tourist, adsinfo, false, singinfo, print_goods_type);
                }
                else
                {
                    printer.A4PrintTicket(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, singinfo);
                }

            }
            else
            {
                Transaction tran = new Transaction();//거래내용 조회
                GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(null);
                printer.setPrinter(Constants.PRINTER_TYPE);
                string strSlip_no = m_SlipNo;
                string retailer = "";
                string docid = "";
                string tourist = "";
                string singinfo = "";
                string adsinfo = "";

                JArray arrComm = new JArray();
                JArray arrExComm = new JArray();
                JToken signToken;

                string strRes = tran.getSlipPrintInfo(strSlip_no);
                JObject result = JObject.Parse(strRes);
                JToken retailerToken = result["retailerInfo"].DeepClone();
                JToken touristToken = result["touristInfo"].DeepClone();
                JToken refundToken = result["refundInfo"].DeepClone();
                JToken saleToken = result["saleInfo"].DeepClone();
                if (result["signInfo"] != null)
                {
                    signToken = result["signInfo"].DeepClone();
                }
                else
                {
                    signToken = null;
                }
                JArray goodsList = JArray.Parse(result["goodsList"].ToString());

                tourist = touristToken["PASSPORT_TYPE"].ToString() + "|";
                tourist += touristToken["PASSPORT_SERIAL_NO"].ToString() + "|";
                tourist += touristToken["BUYER_NAME"].ToString() + "|";
                tourist += touristToken["NATIONALITY"].ToString() + "|";
                tourist += touristToken["BUYER_BIRTH"].ToString() + "|";
                tourist += touristToken["RESIDENCE"].ToString() + "|";
                tourist += touristToken["ENTRYDT"].ToString() + "|";

                retailer = retailerToken["TAXOFFICE_NAME"].ToString() + "|";
                retailer += retailerToken["TAX_ADDR1"].ToString() + "|";
                retailer += retailerToken["TAX_ADDR2"].ToString() + "|";

                retailer += retailerToken["MERCHANT_JPNM"].ToString() + "|";
                retailer += retailerToken["JP_ADDR1"].ToString() + "|";
                retailer += retailerToken["JP_ADDR2"].ToString() + "|";
                retailer += retailerToken["OPT_CORP_JPNM"].ToString() + "|";

                docid += strSlipLang + "|";                         //출력언어
                if (Constants.PRINT_TYPE != "ALL")
                {
                    docid += "1" + "|";           //출력전표 갯수
                    docid += "01" + "|";     //출력
                }
                else
                {
                    docid += "2" + "|";            //출력전표 갯수
                    docid += "01/02" + "|";     //출력
                }
                docid += "[REPUBLISH]" + "|";                 //출력유형
                docid += strSlip_no + "|";

                docid += "N" + "|";                         //합산전표 출력여부

                docid += refundToken["REFUND_WAY_CODE"].ToString() + "|"; // 
                docid += refundToken["REFUND_WAY_CODE_DESC"].ToString() + "|";
                docid += refundToken["MASK_REMIT_NO"].ToString() + "|";
                docid += retailerToken["UNIKEY"].ToString();
                


                if (signToken != null)
                {
                    singinfo = signToken["SIGN_DATA"].ToString();
                }
                printer.PrintSlipNoGoods_ja(docid.Replace("[REPUBLISH]", "0"), retailer, tourist, adsinfo, false, singinfo);

            }


        }

        private void setWaitCursor(Boolean bWait)
        {
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


    }

}
