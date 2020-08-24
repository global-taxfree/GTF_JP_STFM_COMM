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
    public partial class TrxnPanel : UserControl
    {
        ControlManager m_CtlSizeManager = null;
        ILog m_Logger = null;
        string m_strLastSearchDate = "";
        public TrxnPanel(ILog Logger = null)
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
            m_CtlSizeManager.addControlMove(BTN_CANCEL, true, false, false, false);

            if (m_CtlSizeManager != null)
                m_CtlSizeManager.MoveControls();
        }


        private void metroGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 14 && e.RowIndex >=0 )
            {
                if (!Constants.OPEN_DATE.Equals(m_strLastSearchDate))
                {
                    MetroMessageBox.Show(this, Constants.getMessage("ERROR_PRINT_DATE"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                
                string strSlipNo = GRD_SLIP[1, e.RowIndex].Value.ToString();
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
                    string docid, retailer, goods, tourist, adsinfo, signInfo, preview = string.Empty;
                    docid = jsonDocs["DOCID"].ToString();
                    retailer = jsonDocs["RETAILER"].ToString();
                    goods = jsonDocs["GOODS"].ToString();
                    tourist = jsonDocs["TOURIST"].ToString();
                    adsinfo = jsonDocs["ADSINFO"].ToString();
                    preview = jsonDocs["PREVIEW"].ToString();
                    signInfo = jsonDocs["SIGN"].ToString();

                    docid = strSlipLang + docid.Substring(2);
                    try {
                        GTF_ReceiptPrinter printer = new GTF_ReceiptPrinter(m_Logger);
                        printer.setPrinter(Constants.PRINTER_TYPE);
#if DEBUG
                        printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, true, signInfo);
#else
                        printer.PrintSlip_ja(docid.Replace("[REPUBLISH]", "1"), retailer, goods, tourist, adsinfo, "Y".Equals(preview), signInfo);
#endif
                    }
                    catch (Exception ex)
                    {
                        m_Logger.Error(ex.StackTrace);
                    }
                    
                    JObject tempObj = new JObject();
                    JObject tempObj2 = new JObject();
                    tempObj.Add("SLIP_NO", strSlipNo);
                    tempObj2.Add(Constants.PRINT_CNT, (Int32.Parse(printCnt) + 1).ToString());

                    if (GRD_SLIP[13, e.RowIndex].Value == null || !"02".Equals(GRD_SLIP[13, e.RowIndex].Value.ToString()))
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
                    MetroMessageBox.Show(this, "Print Error", "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void TrxnPanel_Load(object sender, EventArgs e)
        {
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;

            Dictionary<string, string> date_list = new Dictionary<string, string>();
            date_list.Add("01", Constants.getScreenText("COMBO_DATE_SALE"));
            date_list.Add("02", Constants.getScreenText("COMBO_DATE_PRINT"));
            date_list.Add("03", Constants.getScreenText("COMBO_DATE_REFUND"));
            date_list.Add("04", Constants.getScreenText("COMBO_DATE_REG"));

            COM_DATE_COND.DataSource = new BindingSource(date_list, null);
            COM_DATE_COND.DisplayMember = "Value";
            COM_DATE_COND.ValueMember = "Key";
            COM_DATE_COND.SelectedIndex = 0;

            Dictionary<string, string> item_list = new Dictionary<string, string>();
            item_list.Add("ALL", Constants.getScreenText("COMBO_ITEM_ALL"));
            item_list.Add("01", Constants.getScreenText("COMBO_ITEM_UNREFUND"));
            item_list.Add("02", Constants.getScreenText("COMBO_ITEM_REFUND"));

            COM_REFUND_STATUS.DataSource = new BindingSource(item_list, null);
            COM_REFUND_STATUS.DisplayMember = "Value";
            COM_REFUND_STATUS.ValueMember = "Key";
            COM_REFUND_STATUS.SelectedIndex = 0;

            //업무 마감 상태에서는 전표 발행 불가
            /*
            if (Constants.CLOSING_YN)
            {
                BTN_SEARCH.Visible = false;
                return;
            }
            */
        }

        private void TrxnPanel_SizeChanged(object sender, EventArgs e)
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


        int display_num = 20;
        int init_num = 0;
        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            if (!BTN_SEARCH.Enabled)
            {
                return;
            }

            BTN_SEARCH.Enabled = false;
            JObject jsonReq = new JObject();
            init_num = 1;

            int nRow = GRD_SLIP.RowCount;
            for (int i = nRow - 1; i >= 0; i--)
            {
                GRD_SLIP.Rows.RemoveAt(i);
            }
            GRD_SLIP.Refresh();

            try
            {
                setWaitCursor(true);
                /*
                TimeSpan between_date = TXT_REFUND_TODATE.Value - TXT_REFUND_FROMDATE.Value;
                int date_cnt = between_date.Days;

                if (DateTime.Compare(TXT_REFUND_FROMDATE.Value, TXT_REFUND_TODATE.Value) > 0)
                {
                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ValidateSearchDate"));
                    return;
                }
                else if (date_cnt > 10)
                {

                    MessageBox.Show(Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/ExceedSearchDate"));
                    return;
                }
                */

                Transaction tran = new Transaction();
                Utils util = new Utils();

                string start_date = util.FormatConvertDate(TXT_REFUND_FROMDATE.Value.ToString());
                string end_date = util.FormatConvertDate(TXT_REFUND_TODATE.Value.ToString());

                jsonReq.Add("companyID", "000001");
                jsonReq.Add("languageCD", "jp");
                jsonReq.Add("user_desk_id", Constants.DESK_ID);
                jsonReq.Add("merchant_no", Constants.MERCHANT_NO);
                jsonReq.Add("dateFrom", start_date);
                jsonReq.Add("dateTo", end_date);

                if (!TXT_SLIP_NO.Text.Equals(""))
                {
                    jsonReq.Add("slip_no", TXT_SLIP_NO.Text);
                }

                if (!TXT_TOTAL_SLIPSEQ.Text.Equals("")) //TOTAL_SLIPSEQ
                {
                    jsonReq.Add("total_slipseq", TXT_TOTAL_SLIPSEQ.Text);
                }

                if (!COM_DATE_COND.SelectedValue.ToString().Equals(""))
                {
                    jsonReq.Add("dateCond", COM_DATE_COND.SelectedValue.ToString());
                }

                string refund_status_code = COM_REFUND_STATUS.SelectedValue.ToString();
                if (!refund_status_code.Equals(""))
                {
                    if (COM_REFUND_STATUS.SelectedValue.ToString().Equals("ALL"))
                    {
                        refund_status_code = "";
                    }
                    else
                    {
                        jsonReq.Add("refund_status_code", refund_status_code);
                    }
                }

                string page = "1";
                int total_cnt = tran.onlineSearchCount(jsonReq.ToString());
                if (total_cnt > 0)
                {
                    if (cmbbox_Page.Items.Count > 0)
                        cmbbox_Page.Items.Clear();

                    int i = 0;
                    double quotient = total_cnt / display_num;
                    double remainder = total_cnt % display_num;

                    double tot_page = 0;
                    if (remainder != 0)
                    {
                        tot_page = System.Math.Truncate(quotient);
                    }
                    else
                    {
                        tot_page = System.Math.Truncate(quotient) - 1;
                    }

                    for (i = 0; i <= tot_page; i++)
                    {
                        cmbbox_Page.Items.Add(i + 1);
                    }

                    searchList(page);
                }
                else
                {
                    MetroMessageBox.Show(this, Constants.getMessage("NO_SERACH_DATA"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                setWaitCursor(false);
                BTN_SEARCH.Enabled = true;
            }

        }

        private void BTN_CANCEL_Click(object sender, EventArgs e)
        {
            Transaction tran = new Transaction();
            string strSlipNo = "";
            string strTotalSlipNo = "";
            string strRefundWayCode = "";
            string strProgressStatus_code = "";
            string alert_msg = "";
            string strRefundStatusCode = ""; 
            if (GRD_SLIP.SelectedRows.Count != 0)
            {
                DataGridViewRow row = this.GRD_SLIP.SelectedRows[0];
                strSlipNo = row.Cells[1].Value.ToString();
                strTotalSlipNo = row.Cells[11].Value.ToString();
                strRefundStatusCode = row.Cells[14].Value.ToString();
                strRefundWayCode = row.Cells[15].Value.ToString();
                strProgressStatus_code = row.Cells[16].Value.ToString();
                /*
                string alert_test;
                alert_test = "strSlipNo[" + strSlipNo + "]" + Environment.NewLine;
                alert_test += "strTotalSlipNo[" + strTotalSlipNo + "]" + Environment.NewLine;
                alert_test += "strRefundWayCode[" + strRefundWayCode + "]" + Environment.NewLine;
                alert_test += "strProgressStatus_code[" + strProgressStatus_code + "]" + Environment.NewLine;
                MetroMessageBox.Show(this, alert_test, "info", MessageBoxButtons.OK);
                *
                * */
            }
            else
            {
                MetroMessageBox.Show(this, Constants.getMessage("SELECTCANCELSLIP"), "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if(strRefundStatusCode.Equals("04"))
            {
                MetroMessageBox.Show(this, Constants.getMessage("ALREADYCANCELREFUND"), "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (strRefundWayCode != "01")
            {
                if (strProgressStatus_code != "99")
                {
                    alert_msg = Constants.getMessage("REMMITYETSEND")  + Environment.NewLine;
                }
                else
                {
                    alert_msg = Constants.getMessage("REMMITSENDCOM")  + Environment.NewLine; 
                }
            }

            if (strTotalSlipNo != "")
            {

                alert_msg += Constants.getMessage("TOTAL_SLIP_ISSUED") + Environment.NewLine + Constants.getMessage("CANCELREQUEST");
                MessageForm msgForm = new MessageForm(alert_msg, null, MessageBoxButtons.YesNo);
                msgForm.ShowDialog(this);
                if (msgForm.DialogResult == DialogResult.Yes)
                {
                    MessageForm msgForm2 = new MessageForm(Constants.getMessage("TOTALSLIPALLCANCEL"), null, MessageBoxButtons.YesNo);
                    msgForm2.ShowDialog(this);
                    if (msgForm2.DialogResult == DialogResult.Yes)
                    {
                        if (tran.CancelAll(Constants.USER_ID, strSlipNo, strTotalSlipNo))
                        {
                            msgForm = null;
                            
                            msgForm = new MessageForm(Constants.getMessage("REFUNDCANCELCOM"), null, MessageBoxButtons.OK);
                            msgForm.ShowDialog(this);
                            searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
                        }
                        else
                        {
                            msgForm = null;
                            
                            msgForm = new MessageForm(Constants.getMessage("REFUNDCANCELFAIL"),  null, MessageBoxButtons.OK);
                            msgForm.ShowDialog(this);
                        }
                    }
                    else
                    {
                        if (tran.Cancel(Constants.USER_ID, strSlipNo, strTotalSlipNo))
                        {
                            msgForm = null;
                            msgForm = new MessageForm(Constants.getMessage("REFUNDCANCELCOM"), null, MessageBoxButtons.OK);
                            msgForm.ShowDialog(this);
                            searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
                        }
                        else
                        {
                            msgForm = null;
                            msgForm = new MessageForm(Constants.getMessage("REFUNDCANCELFAIL"), null, MessageBoxButtons.OK);
                            msgForm.ShowDialog(this);
                        }
                    }
                }
                else
                {
                    
                    MetroMessageBox.Show(this, Constants.getMessage("CONFIRMCANCEL"), "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                alert_msg += "[" + strSlipNo + "] " + Constants.getMessage("CANCELREQUEST")  ;
                MessageForm msgForm = new MessageForm(alert_msg, null, MessageBoxButtons.YesNo);
                msgForm.ShowDialog(this);
                if (msgForm.DialogResult == DialogResult.Yes)
                {
                    if (tran.Cancel(Constants.USER_ID, strSlipNo, strTotalSlipNo))
                    {
                        msgForm = null;
                        msgForm = new MessageForm(Constants.getMessage("REFUNDCANCELCOM"), null, MessageBoxButtons.OK);
                        msgForm.ShowDialog(this);
                        searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
                    }
                    else
                    {
                        msgForm = null;
                        msgForm = new MessageForm(Constants.getMessage("REFUNDCANCELFAIL"), null, MessageBoxButtons.OK);
                        msgForm.ShowDialog(this);
                    }

                }
                else
                {
                    MetroMessageBox.Show(this, Constants.getMessage("CONFIRMCANCEL"), "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void GRD_SLIP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex != 12)
            {
                try {
                    setWaitCursor(true);
                    string strSlipNo = GRD_SLIP[1, e.RowIndex].Value.ToString();
                    TrxnDetailInfo slipForm = new TrxnDetailInfo(strSlipNo, m_Logger);
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

        private void cmbbox_Page_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (init_num == 0)
            {
                return;
            }
            if (!BTN_SEARCH.Enabled)
            {
                return;
            }

            searchList(((cmbbox_Page.SelectedIndex) + 1).ToString());
        }


        private void searchList(string page_num)
        {
            JObject jsonReq = new JObject();

            try
            {

                cmbbox_Page.SelectedIndex = int.Parse(page_num) - 1;

                Transaction tran = new Transaction();
                Utils util = new Utils();

                string start_date = util.FormatConvertDate(TXT_REFUND_FROMDATE.Value.ToString());
                string end_date = util.FormatConvertDate(TXT_REFUND_TODATE.Value.ToString());

                jsonReq.Add("displayNum", display_num);
                jsonReq.Add("pageNum", page_num);

                jsonReq.Add("companyID", "000001");
                jsonReq.Add("languageCD", "jp");
                jsonReq.Add("user_desk_id", Constants.DESK_ID);
                jsonReq.Add("merchant_no", Constants.MERCHANT_NO);

                jsonReq.Add("dateFrom", start_date);
                jsonReq.Add("dateTo", end_date);

                if (!TXT_SLIP_NO.Text.Equals(""))
                {
                    jsonReq.Add("slip_no", TXT_SLIP_NO.Text);
                }

                if (!TXT_TOTAL_SLIPSEQ.Text.Equals("")) //TOTAL_SLIPSEQ
                {
                    jsonReq.Add("total_slipseq", TXT_TOTAL_SLIPSEQ.Text); 
                }

                if (!COM_DATE_COND.SelectedValue.ToString().Equals(""))
                {
                    jsonReq.Add("dateCond", COM_DATE_COND.SelectedValue.ToString());
                }

                string refund_status_code = COM_REFUND_STATUS.SelectedValue.ToString();
                if (!refund_status_code.Equals(""))
                {
                    if (COM_REFUND_STATUS.SelectedValue.ToString().Equals("ALL"))
                    {
                        refund_status_code = null;
                    }
                    jsonReq.Add("refund_status_code", refund_status_code);
                }

                string strResult = tran.onlineSearch(jsonReq.ToString());

                JArray a = JArray.Parse(strResult);

                int nRow = GRD_SLIP.RowCount;
                for (int i = nRow - 1; i >= 0; i--)
                {
                    GRD_SLIP.Rows.RemoveAt(i);
                }
                GRD_SLIP.Refresh();

                if (a == null || a.Count == 0)
                {
                    MetroMessageBox.Show(this, Constants.getMessage("NO_SERACH_DATA"), "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    m_strLastSearchDate = start_date;
                    GRD_SLIP.Rows.Add(a.Count);
                    for (int i = 0; i < a.Count; i++)
                    {
                        JObject tempObj = (JObject)a[i];
                        GRD_SLIP[0, i].Value = (i + 1).ToString();
                        GRD_SLIP[1, i].Value = tempObj["SLIP_NO"].ToString();
                        GRD_SLIP[2, i].Value = tempObj["SALEDT"].ToString();
                        GRD_SLIP[3, i].Value = tempObj["MERCHANT_JPNM"].ToString();
                        GRD_SLIP[4, i].Value = Int64.Parse(tempObj["TOTAL_EXCOMM_SALE_AMT"].ToString());
                        GRD_SLIP[5, i].Value = Int64.Parse(tempObj["TOTAL_EXCOMM_TAX_AMT"].ToString());
                        GRD_SLIP[6, i].Value = Int64.Parse(tempObj["TOTAL_EXCOMM_REFUND_AMT"].ToString());
                        GRD_SLIP[7, i].Value = Int64.Parse(tempObj["TOTAL_COMM_SALE_AMT"].ToString());
                        GRD_SLIP[8, i].Value = Int64.Parse(tempObj["TOTAL_COMM_TAX_AMT"].ToString());
                        GRD_SLIP[9, i].Value = Int64.Parse(tempObj["TOTAL_COMM_REFUND_AMT"].ToString());
                        GRD_SLIP[10, i].Value = tempObj["REFUND_STATUS_CODE_DESC"].ToString();
                        GRD_SLIP[11, i].Value = tempObj["TOTAL_SLIPSEQ"].ToString();
                        GRD_SLIP[12, i].Value = tempObj["WORKERID"].ToString();
                        GRD_SLIP[13, i].Value = tempObj["SEND_FLAG"].ToString();
                        //GRD_SLIP[13, i].Value = Int64.Parse(tempObj["RETRY_CNT"].ToString());
                        GRD_SLIP[14, i].Value = tempObj["REFUND_STATUS_CODE"].ToString();
                        GRD_SLIP[15, i].Value = tempObj["REFUND_WAY_CODE"].ToString();
                        GRD_SLIP[16, i].Value = tempObj["PROGRESS_STATUS_CODE"].ToString();
                        tempObj = null;
                    }
                }
                tran = null;
               
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.StackTrace);
                MetroMessageBox.Show(this, Constants.CONF_MANAGER.getCustomValue("Message", Constants.SYSTEM_LANGUAGE + "/SearchFaild"),
                       "Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
