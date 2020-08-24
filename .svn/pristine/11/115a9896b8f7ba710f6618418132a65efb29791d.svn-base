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

using GTF_STFM_COMM.Util;
using GTF_STFM_COMM.Tran;
using log4net;

namespace GTF_STFM_COMM.Screen
{
    public partial class SlipDetailInfo : MetroForm
    {
        ILog m_Logger = null;
        string m_SlipNo = String.Empty;
        Int64 m_nPrintCnt = 0;
        public SlipDetailInfo(string strSlipNo , ILog logger = null)
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
            try {
                Transaction tran = new Transaction();//거래내용 조회
                m_Logger.Debug("SlipDetailInfo_Load 1");
                JArray arrRetSlip = tran.SearchSlips("", "", "", "", m_SlipNo);
                JArray arrRetSlipDetail = tran.SearchSlipDetail(m_SlipNo); ;
                m_Logger.Debug("SlipDetailInfo_Load 2");

                if (arrRetSlip.Count > 0)
                {
                    JObject tempObj = (JObject)arrRetSlip[0];
                    TXT_PASS_NAME.Text = tempObj["BUYER_NAME"].ToString();
                    if (tempObj["PASSPORT_SERIAL_NO"].ToString().Length > 0) TXT_PASS_NO.Text = tempObj["PASSPORT_SERIAL_NO"].ToString();
                    else
                    {
                        TXT_PASS_NO.Text = tempObj["PERMIT_NO"].ToString();
                    }
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

                    TXT_PASS_RES.Text = tempObj["RESIDENCE"].ToString();
                    TXT_PASS_TYPE.Text = tempObj["PASSPORT_TYPE_NAME"].ToString();

                    TXT_PARTNER_NAME.Text = tempObj["ENTRYDT"].ToString();

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
                    TXT_SLIP_NO.Text = tempObj["SLIP_NO"].ToString();

                    TXT_SLIP_STATUS.Text = "02".Equals(tempObj["SLIP_STATUS_CODE"].ToString()) ? Constants.getScreenText("COMBO_ITEM_PRINT") : Constants.getScreenText("COMBO_ITEM_NO_PRINT");
                    TXT_REFUND_DATE.Text = tempObj["SALEDT"].ToString();
                    //TXT_REFUND_DATE.Text = tempObj["REFUNDDT"].ToString();
                    TXT_REFUND_STATUS.Text = "02".Equals(tempObj["SLIP_STATUS_CODE"].ToString()) ? Constants.getScreenText("COMBO_ITEM_REFUND") : Constants.getScreenText("COMBO_ITEM_UNREFUND");

                    //if("02".Equals(TXT_SLIP_STATUS.Text.ToString() ))
                    if ("02".Equals(tempObj["SLIP_STATUS_CODE"].ToString()))
                    {
                        BTN_REFUND.Text = tempObj["REFUNDDT"].ToString();
                        BTN_REFUND.Enabled = false;
                    }
                    else
                    {
                        if (tempObj["REG_DTM"] != null && !Constants.OPEN_DATE.Equals(tempObj["REG_DTM"].ToString().Trim()) )
                        {
                            BTN_REFUND.Visible = false;
                        }
                    }
                    string strRefundWayText = "";
                    if("01".Equals(tempObj["REFUND_WAY_CODE"].ToString()))
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
                    TXT_SLIP_USER.Text = tempObj["USERID"].ToString();

                    this.m_nPrintCnt = Int64.Parse(tempObj[Constants.PRINT_CNT].ToString());

                    Int64 nGoodsBuyAmt = 0;
                    Int64 nGoodsTaxAmt = 0;
                    Int64 nGoodsRefundAmt = 0;
                    Int64 nGoodsFeeAmt = 0;

                    Int64 nConsumsBuyAmt = 0;
                    Int64 nConsumsTaxAmt = 0;
                    Int64 nConsumsRefundAmt = 0;
                    Int64 nConsumsFeeAmt = 0;

                    nGoodsBuyAmt = Int64.Parse(tempObj["GOODS_BUY_AMT"].ToString());
                    nGoodsTaxAmt = Int64.Parse(tempObj["GOODS_TAX_AMT"].ToString());
                    nGoodsRefundAmt = Int64.Parse(tempObj["GOODS_REFUND_AMT"].ToString());
                    nGoodsFeeAmt = nGoodsTaxAmt - nGoodsRefundAmt;

                    nConsumsBuyAmt = Int64.Parse(tempObj["CONSUMS_BUY_AMT"].ToString());
                    nConsumsTaxAmt = Int64.Parse(tempObj["CONSUMS_TAX_AMT"].ToString());
                    nConsumsRefundAmt = Int64.Parse(tempObj["CONSUMS_REFUND_AMT"].ToString());
                    nConsumsFeeAmt = nConsumsTaxAmt - nConsumsRefundAmt;

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
                        TXT_RCT_NO.Text = tempObj["RCT_NO"].ToString();

                        for (int i = 0; i < arrRetSlipDetail.Count; i++)
                        {
                            tempObj = (JObject)arrRetSlipDetail[i];
                            GRD_ITEMS[0, i].Value = tempObj["ITEM_NO"].ToString();//물품번호
                            GRD_ITEMS[1, i].Value = tempObj["ITEM_TYPE_TEXT"].ToString();//물품 종류

                            GRD_ITEMS[2, i].Value = tempObj["MAIN_CAT_TEXT"].ToString();
                            GRD_ITEMS[3, i].Value = tempObj["MID_CAT_TEXT"].ToString();
                            GRD_ITEMS[4, i].Value = tempObj["ITEM_NAME"].ToString();
                            GRD_ITEMS[5, i].Value = Int64.Parse(tempObj["QTY"].ToString());
                            if ("02".Equals(tempObj["TAX_TYPE"].ToString()))//내세
                            {
                                GRD_ITEMS[6, i].Value = (Int64.Parse(tempObj["BUY_AMT"].ToString()) - Int64.Parse(tempObj["TAX_AMT"].ToString()));
                            }
                            else//외세
                            {
                                GRD_ITEMS[6, i].Value = Int64.Parse(tempObj["BUY_AMT"].ToString());
                            }

                            GRD_ITEMS[7, i].Value = Int64.Parse(tempObj["TAX_AMT"].ToString());
                            GRD_ITEMS[8, i].Value = Int64.Parse(tempObj["FEE_AMT"].ToString());
                            GRD_ITEMS[9, i].Value = Int64.Parse(tempObj["REFUND_AMT"].ToString());

                            // 8% 10%
                            if (tempObj["TAX_CAL_TYPE"].ToString() == null || tempObj["TAX_CAL_TYPE"].ToString() == "" || tempObj["TAX_CAL_TYPE"].ToString() == "1")
                            {
                                GRD_ITEMS[10, i].Value = "8%";
                            }
                            else if (tempObj["TAX_CAL_TYPE"].ToString() == "2")
                            {
                                GRD_ITEMS[10, i].Value = "10%";
                            }
                            else
                            {
                                GRD_ITEMS[10, i].Value = "8%";
                            }
                            tempObj = null;
                        }
                    }
                    
                  
                    /*
                    GRD_SLIP[1, i].Value = tempObj[Constants.SEND_FLAG];
                    GRD_SLIP[2, i].Value = tempObj["SLIP_NO"].ToString();
                    GRD_SLIP[3, i].Value = tempObj["SHOP_NAME"].ToString();
                    GRD_SLIP[4, i].Value = Int64.Parse(tempObj["GOODS_BUY_AMT"].ToString());
                    GRD_SLIP[5, i].Value = Int64.Parse(tempObj["CONSUMS_BUY_AMT"].ToString());
                    GRD_SLIP[6, i].Value = Int64.Parse(tempObj["GOODS_REFUND_AMT"].ToString());
                    GRD_SLIP[7, i].Value = Int64.Parse(tempObj["CONSUMS_REFUND_AMT"].ToString());
                    GRD_SLIP[8, i].Value = Int64.Parse(tempObj["GOODS_TAX_AMT"].ToString());
                    GRD_SLIP[9, i].Value = Int64.Parse(tempObj["CONSUMS_TAX_AMT"].ToString());
                    GRD_SLIP[10, i].Value = tempObj["SLIP_STATUS_CODE"].ToString();
                    */
                    tempObj = null;
                }
                m_Logger.Debug("SlipDetailInfo_Load 3");
            }
            catch(Exception ex)
            {
                m_Logger.Error(ex.StackTrace);
            }
        }

        private void BTN_PRINT_Click(object sender, EventArgs e)
        {

        }
    }
}
