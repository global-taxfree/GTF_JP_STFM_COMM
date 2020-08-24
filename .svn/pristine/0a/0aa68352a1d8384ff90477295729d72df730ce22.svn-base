using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework.Forms;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using log4net;

using GTF_STFM_COMM.Util;
using GTF_STFM_COMM.Tran;
using MetroFramework;

namespace GTF_STFM_COMM.Screen
{
    public partial class SearchShop : MetroFramework.Forms.MetroForm
    {
        ILog m_Logger = null;


        public JObject SelectInfo = null;

        public JArray m_ArrLastSearchData { get; set; }

        public SearchShop(ILog Logger = null)
        {
            m_Logger = Logger;
            InitializeComponent();
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;
            this.DialogResult = DialogResult.Cancel;
        }

        private void GRD_ITEMS_DoubleClick(object sender, EventArgs e)
        {

        }

        //매장검색버튼. 필요없을거 같은데..
        private void BTN_SEARCH_Click(object sender, EventArgs e)
        {
            if (TXT_SHOP_NAME.Text != null && !string.Empty.Equals(TXT_SHOP_NAME.Text))
            {
                SearchShopList(TXT_SHOP_NAME.Text);
            }
            else
            {
                MetroMessageBox.Show(this, "판매점명을 입력하시기 바랍니다.", "SearchShop", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TXT_SHOP_NAME.Focus();
            }
        }
        //매장정보를 메인화면에서 받아옴
        public void SetSearchShopList(JArray data)
        {
            JArray sorted = new JArray(data.OrderBy(obj => obj["MERCHANT_NO"]));
            m_ArrLastSearchData = new JArray();
            for(int i= sorted.Count-1; i >=0; i --)
            {
                m_ArrLastSearchData.Add(sorted[i]);
            }
            sorted = null;

            SearchShopList();//화면 초기 세팅
            TXT_SHOP_NAME.Focus();
        }
        public void SearchShopList (string strShop = null)
        {
            if (strShop == null)
            {
                //Transaction tran = new Transaction();
                //m_ArrLastSearchData = tran.SearchShopList("MERCHANT");

                GRD_ITEMS.Rows.Clear();//Clear 만 해주면 스크롤바가 남는 버그가 있다.Add 와 RemoveAt 를 셋트로 설정
                GRD_ITEMS.Rows.Add();
                GRD_ITEMS.Rows.RemoveAt(0);
                //초기에 로드 안하도록 변경
                //JObject tempObject = null;
                //if (m_ArrLastSearchData != null)
                //{
                //    for (int i = 0; i < m_ArrLastSearchData.Count; i++)
                //    {
                //        if (m_ArrLastSearchData[i] is JObject)
                //        {
                //            tempObject = (JObject)m_ArrLastSearchData[i];

                //            GRD_ITEMS.Rows.Add();
                //            GRD_ITEMS[0, GRD_ITEMS.Rows.Count - 1].Value = (i + 1) + "";
                //            GRD_ITEMS[1, GRD_ITEMS.Rows.Count - 1].Value = tempObject["PARTNER_JPNM"];
                //            GRD_ITEMS[2, GRD_ITEMS.Rows.Count - 1].Value = tempObject["MGROUP_JPNM"];
                //            GRD_ITEMS[3, GRD_ITEMS.Rows.Count - 1].Value = tempObject["MERCHANT_JPNM"];

                //        }
                //    }
                //}
            }
            else
            {
                GRD_ITEMS.Rows.Clear();//Clear 만 해주면 스크롤바가 남는 버그가 있다.Add 와 RemoveAt 를 셋트로 설정
                GRD_ITEMS.Rows.Add();
                GRD_ITEMS.Rows.RemoveAt(0);
                
                if (!string.Empty.Equals(strShop.Trim()))
                {
                    JObject tempObject = null;
                    int nNo = 1;
                    if (m_ArrLastSearchData != null)
                    {
                        for (int i = 0; i < m_ArrLastSearchData.Count; i++)
                        {
                            if (m_ArrLastSearchData[i] is JObject)
                            {
                                tempObject = (JObject)m_ArrLastSearchData[i];

                                if ( (tempObject["PARTNER_JPNM"] != null && tempObject["PARTNER_JPNM"].ToString().ToUpper().IndexOf(strShop.ToUpper()) >= 0 )
                                    || (tempObject["MGROUP_JPNM"] !=null && tempObject["MGROUP_JPNM"].ToString().ToUpper().IndexOf(strShop.ToUpper()) >= 0)
                                    || (tempObject["MERCHANT_JPNM"] != null && tempObject["MERCHANT_JPNM"].ToString().ToUpper().IndexOf(strShop.ToUpper()) >= 0))
                                {
                                    GRD_ITEMS.Rows.Add();
                                    GRD_ITEMS[0, GRD_ITEMS.Rows.Count - 1].Value = (nNo++) + "";
                                    GRD_ITEMS[1, GRD_ITEMS.Rows.Count - 1].Value = tempObject["PARTNER_JPNM"] == null ? "" : tempObject["PARTNER_JPNM"];
                                    GRD_ITEMS[2, GRD_ITEMS.Rows.Count - 1].Value = tempObject["MGROUP_JPNM"] == null ? "" : tempObject["MGROUP_JPNM"];
                                    GRD_ITEMS[3, GRD_ITEMS.Rows.Count - 1].Value = tempObject["MERCHANT_JPNM"] == null ? "" : tempObject["MERCHANT_JPNM"];
                                    GRD_ITEMS[4, GRD_ITEMS.Rows.Count - 1].Value = tempObject["MERCHANT_NO"] == null ? "" : tempObject["MERCHANT_NO"];
                                }
                            }
                        }
                    }
                }             
            }
//            GRD_ITEMS.Update();
        }

        private void GRD_ITEMS_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void GRD_ITEMS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SearchShop_Load(object sender, EventArgs e)
        {
            Activate();
            TXT_SHOP_NAME.Focus();
            this.Text = Constants.getScreenText("SEARCHSHOP");
        }

        //텍스트필드 입력시 매장 자동 검색
        private void TXT_SHOP_NAME_TextChanged(object sender, EventArgs e)
        {
            SearchShopList(TXT_SHOP_NAME.Text == null ? "":TXT_SHOP_NAME.Text.Trim());
        }

        //그리드 더블클릭 시 close
        private void GRD_ITEMS_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && GRD_ITEMS[4, e.RowIndex].Value != null)
            {
                int nDataIndex = -1;
                for(int i= 0; i < m_ArrLastSearchData.Count; i++)
                {
                    JObject tempObject = (JObject)m_ArrLastSearchData[i];
                    if(GRD_ITEMS[4, e.RowIndex].Value.ToString().Equals(tempObject["MERCHANT_NO"] == null ? "" : tempObject["MERCHANT_NO"].ToString()))
                    {
                        nDataIndex = i;
                        break;
                    }
                }
                if (nDataIndex >= 0)
                {
                    //JObject tempObject = (JObject)m_ArrLastSearchData[e.RowIndex];
                    JObject tempObject = (JObject)m_ArrLastSearchData[nDataIndex];
                    SelectInfo = new JObject();
                    SelectInfo.Add("MERCHANT_JPNM", tempObject["MERCHANT_JPNM"].ToString());
                    SelectInfo.Add("TAXOFFICE_ADDR", tempObject["TAXOFFICE_ADDR"].ToString());
                    SelectInfo.Add("BIZ_INDUSTRY_CODE", tempObject["BIZ_INDUSTRY_CODE"].ToString());
                    SelectInfo.Add("INDUSTRY_CODE", tempObject["INDUSTRY_CODE"].ToString());
                    SelectInfo.Add("SALE_MANAGER_CODE", tempObject["SALE_MANAGER_CODE"].ToString());
                    
                    SelectInfo.Add("JP_ADDR1", tempObject["JP_ADDR1"].ToString()) ;
                    SelectInfo.Add("JP_ADDR2", tempObject["JP_ADDR2"].ToString()) ;

                    SelectInfo.Add("TEL_NO", tempObject["TEL_NO"].ToString());
                    SelectInfo.Add("TAXOFFICE_NAME", tempObject["TAXOFFICE_NAME"].ToString());
                    SelectInfo.Add("COMBINED_USEYN", tempObject["COMBINED_USEYN"].ToString());
                    SelectInfo.Add("VOID_USEYN", tempObject["VOID_USEYN"].ToString());

                    SelectInfo.Add("MERCHANT_NO", tempObject["MERCHANT_NO"] == null? "": tempObject["MERCHANT_NO"].ToString());//매장번호

                    SelectInfo.Add("FEE_RATE", tempObject["FEE_RATE"].ToString());          //수수료
                    SelectInfo.Add("TAX_FORMULA", tempObject["TAX_FORMULA"].ToString()); //세금
                    SelectInfo.Add("SEND_CUSTOM_FLAG", tempObject["SEND_CUSTOM_FLAG"].ToString());    //전자화여부

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }

        private void TXT_SHOP_NAME_Click(object sender, EventArgs e)
        {

        }

        private void GRD_ITEMS_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

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
                    //case Keys.Enter:
                    //    Int32 selectedRowCount =
                    //        GRD_ITEMS.Rows.GetRowCount(DataGridViewElementStates.Selected);
                    //    if (selectedRowCount == 1)
                    //    {
                    //        DataGridViewCellMouseEventArgs e 
                    //            = new DataGridViewCellMouseEventArgs(1, GRD_ITEMS.SelectedRows[0].Index, 0,0, new MouseEventArgs(MouseButtons.Left, 0,0,0,0));
                    //        GRD_ITEMS_CellMouseDoubleClick(null, e);
                    //        return true;
                    //    }
                    //    break;
                    case Keys.Escape:
                        this.DialogResult = DialogResult.Cancel;
                        Close();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }
    }
}
