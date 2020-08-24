using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using log4net;
using Newtonsoft.Json.Linq;
using GTF_Printer;
using GTF_STFM_COMM.Util;
using GTF_STFM_COMM.Tran;

namespace GTF_STFM_COMM.Screen
{
    public partial class SearchPanel : UserControl
    {
        ControlManager m_CtlSizeManager = null;
        JArray m_ArrShopList = null;
        string print_goods_type = "0";
        ILog m_Logger = null;
        string m_strLastSearchDate = "";
        public SearchPanel(ILog Logger = null)
        {
            m_Logger = Logger;
               InitializeComponent();
            //최초생성시 좌표, 크기 조정여부 등록함. 화면별로 Manager 를 가진다. 
            m_CtlSizeManager = new ControlManager(this);
            //횡늘림
            m_CtlSizeManager.addControlMove(TIL_1, false, false, true, false);
            m_CtlSizeManager.addControlMove(LAY_SEARCH, false, false, true, false);

            //종횡 늘림
            m_CtlSizeManager.addControlMove(GRD_SLIP, false, false, true, true);

            //횡이동
            m_CtlSizeManager.addControlMove(BTN_SEARCH, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_SEND, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_CLOSING, true, false, false, false);
            m_CtlSizeManager.addControlMove(BTN_CLOSING_CANCEL, true, false, false, false);
            
            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
        }


        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 13 && e.RowIndex >=0 )
            {
                if (!Constants.OPEN_DATE.Equals(m_strLastSearchDate))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PRINT_DATE"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                
                string strSlipNo = GRD_SLIP[2, e.RowIndex].Value.ToString();
                string printCnt = (GRD_SLIP[13, e.RowIndex].Value == null || string.Empty.Equals(GRD_SLIP[13, e.RowIndex].Value.ToString().Trim()) )
                    ? "0":GRD_SLIP[13, e.RowIndex].Value.ToString();
                Transaction tran = new Transaction();
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

                if(langForm.DialogResult != DialogResult.OK)
                {
                    return;
                }
                JObject jsonDocs = tran.GetPrintDocs(strSlipNo);
                if(jsonDocs != null)//출력
                {
                    if (Constants.RCT_ADD == "YES")
                    {
                        string docid, retailer, goods, tourist, adsinfo, signInfo, preview = string.Empty;
                        docid = jsonDocs["DOCID"].ToString();
                        retailer = jsonDocs["RETAILER"].ToString();
                        goods = jsonDocs["GOODS"].ToString();
                        tourist = jsonDocs["TOURIST"].ToString();
                        adsinfo = jsonDocs["ADSINFO"].ToString();
                        preview = jsonDocs["PREVIEW"].ToString();
                        signInfo = jsonDocs["SIGN"].ToString();

                        docid = strSlipLang + docid.Substring(2);
                        try
                        {
                            GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(m_Logger);
                            printer.setPrinter(Constants.PRINTER_TYPE);
                            if (Constants.SLIP_TYPE == "80mm")
                            {
                                /*
#if DEBUG
                                printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, true, signInfo);
#else
                        printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, "Y".Equals(preview), signInfo);
#endif
*/
                                printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, "Y".Equals(preview), signInfo, print_goods_type);
                            }
                            else
                            {
                                printer.A4PrintTicket(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, signInfo);
                            }
                        }
                        catch (Exception ex)
                        {
                            m_Logger.Error(ex.StackTrace);
                        }

                        JObject tempObj = new JObject();
                        JObject tempObj2 = new JObject();
                        tempObj.Add("SLIP_NO", strSlipNo);
                        tempObj2.Add(Constants.PRINT_CNT, (Int32.Parse(printCnt) + 1).ToString());

                        if (GRD_SLIP[14, e.RowIndex].Value == null || !"02".Equals(GRD_SLIP[14, e.RowIndex].Value.ToString()))
                        {
                            MessageForm msgForm = new MessageForm(Constants.getMessage("SLIP_CONFIRM"), null, MessageBoxButtons.YesNo);
                            msgForm.ShowDialog(this);
                            if (msgForm.DialogResult == DialogResult.Yes)
                            {
                                string refunddt = System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss");
                                tempObj2.Add("SLIP_STATUS_CODE", "02");
                                tempObj2.Add("REFUNDDT", refunddt);
                                refunddt = "";
                            }
                        }

                        tran.UpdateSlip(tempObj, tempObj2);
                        tempObj.RemoveAll();
                        tempObj2.RemoveAll();
                        tempObj = null;
                        tempObj2 = null;
                        BTN_SEARCH_Click(null, null);
                    }
                    else
                    {
                        string docid, retailer, tourist, adsinfo, signInfo, preview = string.Empty;
                        docid = jsonDocs["DOCID"].ToString();
                        retailer = jsonDocs["RETAILER"].ToString();
                        tourist = jsonDocs["TOURIST"].ToString();
                        adsinfo = jsonDocs["ADSINFO"].ToString();
                        preview = jsonDocs["PREVIEW"].ToString();
                        signInfo = jsonDocs["SIGN"].ToString();

                        docid = strSlipLang + docid.Substring(2);
                        try
                        {
                            GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(m_Logger);
                            printer.setPrinter(Constants.PRINTER_TYPE);
                            printer.PrintSlipNoGoods_ja(docid.Replace("[REPUBLISH]", "1"), retailer, tourist, adsinfo, "Y".Equals(preview), signInfo);
                        }
                        catch (Exception ex)
                        {
                            m_Logger.Error(ex.StackTrace);
                        }

                        JObject tempObj = new JObject();
                        JObject tempObj2 = new JObject();
                        tempObj.Add("SLIP_NO", strSlipNo);
                        tempObj2.Add(Constants.PRINT_CNT, (Int32.Parse(printCnt) + 1).ToString());

                        if (GRD_SLIP[14, e.RowIndex].Value == null || !"02".Equals(GRD_SLIP[14, e.RowIndex].Value.ToString()))
                        {
                            MessageForm msgForm = new MessageForm(Constants.getMessage("SLIP_CONFIRM"), null, MessageBoxButtons.YesNo);
                            msgForm.ShowDialog(this);
                            if (msgForm.DialogResult == DialogResult.Yes)
                            {
                                string refunddt = System.DateTime.Now.ToString("yy'/'MM'/'dd HH:mm:ss");
                                tempObj2.Add("SLIP_STATUS_CODE", "02");
                                tempObj2.Add("REFUNDDT", refunddt);
                                refunddt = "";
                            }
                        }

                        tran.UpdateSlip(tempObj, tempObj2);
                        tempObj.RemoveAll();
                        tempObj2.RemoveAll();
                        tempObj = null;
                        tempObj2 = null;
                        BTN_SEARCH_Click(null, null);
                    }
                    
                }
                else
                {
                    MetroMessageBox.Show(this, "Print Error", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void SearchPanel_Load(object sender, EventArgs e)
        {
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;
            Dictionary<string, string> item_list = new Dictionary<string, string>();
            item_list.Add("ALL", Constants.getScreenText("COMBO_ITEM_ALL"));
            item_list.Add("01", Constants.getScreenText("COMBO_ITEM_UNREFUND"));
            item_list.Add("02", Constants.getScreenText("COMBO_ITEM_REFUND"));

            COM_REFUND_STATUS.DataSource = new BindingSource(item_list, null);
            COM_REFUND_STATUS.DisplayMember = "Value";
            COM_REFUND_STATUS.ValueMember = "Key";
            COM_REFUND_STATUS.SelectedIndex = 0;

            Dictionary<string, string> item_list_send = new Dictionary<string, string>();
            item_list_send.Add("ALL", Constants.getScreenText("COMBO_ITEM_ALL"));
            item_list_send.Add("Y", Constants.getScreenText("COMBO_ITEM_SEND"));
            item_list_send.Add("N", Constants.getScreenText("COMBO_ITEM_NO_SEND"));

            COM_SEND.DataSource = new BindingSource(item_list_send, null);
            COM_SEND.DisplayMember = "Value";
            COM_SEND.ValueMember = "Key";
            COM_SEND.SelectedIndex = 0;

            COM_SEND.SelectedIndex = 0;
            //업무 마감 상태에서는 전표 발행 불가
            if (Constants.CLOSING_YN)
            {
                BTN_SEARCH.Visible = false;
                return;
            }

            Transaction tran = new Transaction();
            m_ArrShopList = tran.SearchShopList();
            if (m_ArrShopList.Count == 1)
            {
                JObject tempObj = ((JObject)m_ArrShopList[0]);
                print_goods_type = tempObj["PRINT_GOODS_TYPE"].ToString();
            }
        }

        private void SearchPanel_SizeChanged(object sender, EventArgs e)
        {
            if(m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
            this.Refresh();
        }

        private void GRD_SLIP_Scroll(object sender, ScrollEventArgs e)
        {
            int firstDisplayed = GRD_SLIP.FirstDisplayedScrollingRowIndex;
            int displayed = GRD_SLIP.DisplayedRowCount(true);
            int lastVisible = (firstDisplayed + displayed) - 1;
            int lastIndex = GRD_SLIP.RowCount - 1;

            if (lastVisible == lastIndex)//마지막행으로 스크롤되면 자동으로 추가 조회 하도록 하는 기능을..
            {

            }
        }


        private void BTN_QR_SCAN_Click(object sender, EventArgs e)
        {
            TXT_SLIP_NO.Focus();
        }

        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            try {
                GRD_SLIP.Rows.Clear();
                Transaction tran = new Transaction();//거래내용 조회
                bool sales_goods_exits = tran.checkTable("SALES_GOODS");
                string strSearchDate = TXT_REFUND_DATE.Value.ToString("yyyyMMdd");
                JArray arrRet = tran.SearchSlips(strSearchDate, strSearchDate, COM_SEND.SelectedValue.ToString(), COM_REFUND_STATUS.SelectedValue.ToString(), TXT_SLIP_NO.Text);
                if (arrRet == null || arrRet.Count == 0)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("NO_SERACH_DATA"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    m_strLastSearchDate = strSearchDate;
                    GRD_SLIP.Rows.Add(arrRet.Count);
                    for (int i = 0; i < arrRet.Count; i++)
                    {
                        JObject tempObj = (JObject)arrRet[i];
                        GRD_SLIP[0, i].Value = (i + 1).ToString();
                        if ("Y".Equals(tempObj[Constants.SEND_FLAG].ToString().Trim()))
                        {
                            GRD_SLIP[1, i].Value = Constants.getScreenText("COMBO_ITEM_SEND");
                        }
                        else if ("N".Equals(tempObj[Constants.SEND_FLAG].ToString().Trim()))
                        {
                            GRD_SLIP[1, i].Value = Constants.getScreenText("COMBO_ITEM_NO_SEND");
                        }
                        GRD_SLIP[2, i].Value = tempObj["SLIP_NO"].ToString();
                        if(sales_goods_exits)
                        {
                            JObject rctObj = tran.getRCTInfo(tempObj["SLIP_NO"].ToString());
                            if (rctObj != null)
                            {
                                GRD_SLIP[3, i].Value = rctObj["RCT_NO"].ToString();
                            }
                            else
                            {
                                GRD_SLIP[3, i].Value = "";
                            }
                        }
                        else
                        {
                            GRD_SLIP[3, i].Value = "";
                        }
                        GRD_SLIP[4, i].Value = tempObj["SALEDT"].ToString();
                        GRD_SLIP[5, i].Value = tempObj["SHOP_NAME"].ToString();
                        GRD_SLIP[6, i].Value = Int64.Parse(tempObj["GOODS_BUY_AMT"].ToString());
                        GRD_SLIP[7, i].Value = Int64.Parse(tempObj["GOODS_TAX_AMT"].ToString());
                        GRD_SLIP[8, i].Value = Int64.Parse(tempObj["GOODS_REFUND_AMT"].ToString());
                        GRD_SLIP[9, i].Value = Int64.Parse(tempObj["CONSUMS_BUY_AMT"].ToString());
                        GRD_SLIP[10, i].Value = Int64.Parse(tempObj["CONSUMS_TAX_AMT"].ToString());
                        GRD_SLIP[11, i].Value = Int64.Parse(tempObj["CONSUMS_REFUND_AMT"].ToString());
                        GRD_SLIP[12, i].Value = "02".Equals(tempObj["SLIP_STATUS_CODE"].ToString()) ? Constants.getScreenText("COMBO_ITEM_REFUND") : Constants.getScreenText("COMBO_ITEM_UNREFUND");
                        if (tempObj.GetValue(Constants.PRINT_CNT) == null || string.Empty.Equals(tempObj[Constants.PRINT_CNT].ToString().Trim()))
                        {
                            GRD_SLIP[13, i].Value = 0;
                        }
                        else
                        {
                            GRD_SLIP[13, i].Value = Int64.Parse(tempObj[Constants.PRINT_CNT].ToString());
                        }
                        GRD_SLIP[14, i].Value = tempObj["SLIP_STATUS_CODE"].ToString();
                        
                        tempObj = null;
                    }
                }
                tran = null;
            }catch(Exception ex)
            {
                m_Logger.Error(ex.StackTrace);
            }
        }

        private void BTN_CLOSING_Click(object sender, EventArgs e)
        {
            try {
                Transaction tran = new Transaction();//거래내용 조회
                //JArray arrRet = tran.SearchSlips(Constants.OPEN_DATE, Constants.OPEN_DATE, "N" , "02");
                JArray arrRet = tran.SearchUserSlips("", "", "N", "02");
                MessageForm msgForm = new MessageForm("");
                if (arrRet != null && arrRet.Count > 0)
                {
                    msgForm = new MessageForm(Constants.getMessage("CLOSING_FAIL"), null, MessageBoxButtons.OK);
                    msgForm.ShowDialog(this);
                    msgForm = null;
                }
                else
                {
                    if(arrRet != null)
                    {
                        arrRet.RemoveAll();
                        arrRet.Clear();
                        arrRet = null;
                    }
                    arrRet = tran.SearchUserSlips(Constants.OPEN_DATE, Constants.OPEN_DATE, "Y" , "02");

                    Int64 saleCnt = 0, saleAmt = 0, taxAmt =0, feeAmt = 0;
                    if (arrRet != null)
                    {
                        saleCnt = arrRet.Count;
                        for (int i = 0; i < arrRet.Count; i++)
                        {
                            JObject tempObj = (JObject)arrRet[i];
                            saleAmt += Int64.Parse(tempObj["GOODS_BUY_AMT"].ToString());
                            taxAmt += Int64.Parse(tempObj["GOODS_TAX_AMT"].ToString());
                            feeAmt += Int64.Parse(tempObj["GOODS_TAX_AMT"].ToString()) - Int64.Parse(tempObj["GOODS_REFUND_AMT"].ToString());

                            saleAmt += Int64.Parse(tempObj["CONSUMS_BUY_AMT"].ToString());
                            taxAmt += Int64.Parse(tempObj["CONSUMS_TAX_AMT"].ToString());
                            feeAmt += Int64.Parse(tempObj["CONSUMS_TAX_AMT"].ToString()) - Int64.Parse(tempObj["CONSUMS_REFUND_AMT"].ToString());

                            tempObj.RemoveAll();
                            tempObj = null;
                        }
                        arrRet.RemoveAll();
                        arrRet.Clear();
                        arrRet = null;
                    }

                    msgForm = new MessageForm(Constants.getMessage("CLOSING"), null, MessageBoxButtons.YesNo);
                    msgForm.ShowDialog(this);
                    if (msgForm.DialogResult == DialogResult.Yes)
                    {
                        //업무마감 거래 실행
                        if (tran.Closing(Constants.USER_ID, Constants.OPEN_DATE, saleCnt.ToString(), saleAmt.ToString(), taxAmt.ToString(), feeAmt.ToString()))
                        {
                            msgForm = null;
                            msgForm = new MessageForm(Constants.getMessage("CLOSING_YES"), null, MessageBoxButtons.OK);
                            msgForm.ShowDialog(this);
                            Application.ExitThread();
                            Environment.Exit(0);
                        }
                        else
                        {
                            msgForm = null;
                            msgForm = new MessageForm(Constants.getMessage("CLOSING_NO"), null, MessageBoxButtons.OK);
                            msgForm.ShowDialog(this);
                        }
                    }
                    msgForm = null;
                }
            }catch(Exception ex)
            {
                m_Logger.Error(ex.StackTrace);
            }
        }

        private void GRD_SLIP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != 13)
            {
                try {
                    setWaitCursor(true);
                    string strSlipNo = GRD_SLIP[2, e.RowIndex].Value.ToString();
                    SlipDetailInfo slipForm = new SlipDetailInfo(strSlipNo, m_Logger);
                    slipForm.ShowDialog(this);
                    slipForm = null;
                }
                catch(Exception ex)
                {
                    m_Logger.Error(ex.StackTrace);
                }
                finally
                {
                    setWaitCursor(false);
                }
            }
        }

        private void BTN_CLOSING_CANCEL_Click(object sender, EventArgs e)
        {
            MessageForm msgForm = new MessageForm(Constants.getMessage("CLOSING_CANCEL"), null, MessageBoxButtons.YesNo);
            msgForm.ShowDialog(this);
            if (msgForm.DialogResult == DialogResult.Yes)
            {
                Transaction tran = new Transaction();
                //마감 취소 처리
                if (tran.ClosingCancel(Constants.USER_ID, Constants.OPEN_DATE))
                {
                    msgForm = null;
                    msgForm = new MessageForm(Constants.getMessage("CLOSING_CANCEL_SUCCESS"), null, MessageBoxButtons.OK);
                    msgForm.ShowDialog(this);

                    //업무 마감 취소 시 재조회 가능. flag 변경
                    Constants.CLOSING_YN = false;
                    BTN_SEARCH.Visible = true;
                }
                else
                {
                    msgForm = null;
                    msgForm = new MessageForm(Constants.getMessage("CLOSING_CANCEL_FAIL"), null, MessageBoxButtons.OK);
                    msgForm.ShowDialog(this);
                }
            }
            msgForm = null;
        }
        private void setWaitCursor(Boolean bWait)
        {
            if (bWait)
            {
                Cursor.Current = Cursors.WaitCursor;
                this.UseWaitCursor = true;
            }
            else
            {
                Cursor.Current = Cursors.Default;
                this.UseWaitCursor = false;
            }
        }

    }
}
