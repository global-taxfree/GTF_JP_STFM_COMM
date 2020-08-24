using GTF_STFM_COMM.Tran;
using GTF_STFM_COMM.Util;
using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;

namespace GTF_STFM_COMM
{
    public partial class ItemForm : MetroFramework.Forms.MetroForm
    {
        ILog m_Logger = null;
        string[] m_arrGoodsDivision = null;
        string[] m_arrGoodsGroup = null;

        Decimal m_Tax = 0;
        Decimal m_Fee = 0;
        Boolean m_bSaleGoods = false;

        Boolean m_bTaxOut = true;
        int m_nAlreadyGoodsItemCnt = 0;
        int m_nAlreadyConsumsItemCnt = 0;
        int max_goods = 0;


        string m_TaxProcType = string.Empty;
        string m_FeeProcType = string.Empty;
        string m_tax_type = string.Empty;
        string print_goods_type = string.Empty;

        public JArray RetArray = null;
        public ItemForm(ILog Logger = null)
        {
            //Logger 세팅
            m_Logger = Logger;
            if (m_Logger == null)
                m_Logger = LogManager.GetLogger("");
            InitializeComponent();
            RetArray = new JArray();//결과 값 반환 JArray
            this.DialogResult = DialogResult.Cancel;
        }


        /*
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ItemForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Name = "ItemForm";
            this.Load += new System.EventHandler(this.ItemForm_Load);
            this.ResumeLayout(false);

        }
        */
        private void ItemForm_Load(object sender, EventArgs e)
        {
            BTN_ADD_GOODS.Focus();
            this.Text = Constants.getScreenText("ITMES_FORM");
            ControlManager CtlSizeManager = new ControlManager(this);
            CtlSizeManager.ChageLabel(this);
            CtlSizeManager = null;
        }

        //일반물품/소비품 각각 카운트
        //public void Init(int nItemCnt)
        public void Init(int nGoodsCount, int nConsumsCount)
        {
            try {
                m_nAlreadyGoodsItemCnt = nGoodsCount;
                m_nAlreadyConsumsItemCnt = nConsumsCount;
                Transaction tran = new Transaction();
                JObject tempObj = new JObject();
                tempObj.Add("MERCHANT_NO", TXT_SHOP_NO.Text.ToString());
                JArray arrRet = tran.SearchShopList(tempObj);

                if (Constants.SLIP_TYPE == "80mm")
                {
                    max_goods = 50;
                }
                else
                {
                    max_goods = 6;
                }

                //코드 입력
                if (arrRet.Count > 0)
                {
                    string strGOODS_GROUP_CODE = ((JObject)arrRet[0])["GOODS_GROUP_CODE"].ToString();       //대분류
                    m_arrGoodsGroup = strGOODS_GROUP_CODE.Split(',');
                    string strGODDS_DIVISION = ((JObject)arrRet[0])["GODDS_DIVISION"].ToString();           //중분류
                    m_arrGoodsDivision = strGODDS_DIVISION.Split(',');
                    m_Tax = decimal.Parse(((JObject)arrRet[0])["TAX_FORMULA"].ToString());                 //세율
                    m_Fee = decimal.Parse(((JObject)arrRet[0])["FEE_RATE"].ToString());                    //수수료 비율
                    //m_bSaleGoods = "M".Equals(((JObject)arrRet[0])["SALEGOODS_USEYN"].ToString().Trim());  //중분류 선택 여부
                    if(((JObject)arrRet[0])["PRINT_GOODS_TYPE"].ToString().Trim().Equals("0"))
                    {
                        m_bSaleGoods = false;
                    }
                    else
                    {
                        m_bSaleGoods = true;
                    }
                    //m_bSaleGoods = "1".Equals(((JObject)arrRet[0])["PRINT_GOODS_TYPE"].ToString().Trim());  //중분류 선택 여부
                    print_goods_type = ((JObject)arrRet[0])["PRINT_GOODS_TYPE"].ToString().Trim();
                    m_bTaxOut = "01".Equals(((JObject)arrRet[0])["TAX_PROC_TIME_CODE"].ToString().Trim()); //내/외세 구분

                    m_TaxProcType = ((JObject)arrRet[0])["TAX_POINT_PROC_CODE"].ToString().Trim();        //세금 처리 구분
                    m_FeeProcType = ((JObject)arrRet[0])["FEE_POINT_PROC_CODE"].ToString().Trim();        //수수료 처리 구분
                    m_tax_type = ((JObject)arrRet[0])["TAX_TYPE"].ToString();                         //세금 구분

                    if (m_bSaleGoods)//중분류 선택 가능한 경우에는 대분류 선택 기능 Block 처리
                    {
                        ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[1]).ReadOnly = true;
                        ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[1]).DisplayStyleForCurrentCellOnly = true;
                        ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[1]).ReadOnly = true;
                        ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[1]).DisplayStyleForCurrentCellOnly = true;
                    }
                    else //그외엔 중분류를 다 막자
                    {
                        ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[2]).ReadOnly = true;
                        ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[2]).DisplayStyleForCurrentCellOnly = true;
                        ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[2]).ReadOnly = true;
                        ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[2]).DisplayStyleForCurrentCellOnly = true;
                    }

                    //내/외세 콤보 값 생성
                    Dictionary<string, string> tax_list = new Dictionary<string, string>();
                    tax_list.Add("01", Constants.getScreenText("COMBO_ITEM_OUT_TAX"));
                    tax_list.Add("02", Constants.getScreenText("COMBO_ITEM_IN_TAX"));


                    Dictionary<string, string> tax_type_list = new Dictionary<string, string>();
                    if (m_tax_type == "0")
                    {
                        tax_type_list.Add("1", "8%");
                        tax_type_list.Add("2", "10%");

                        if (Constants.TAX_TYPE == null || string.Empty.Equals(Constants.TAX_TYPE) || Constants.TAX_TYPE == "10%")
                        {
                            m_Tax = decimal.Parse("0.1");                 //세율
                        }
                        else
                        {
                            m_Tax = decimal.Parse("0.08");                 //세율
                        }

                    }
                    else if (m_tax_type == "1")
                    {
                        m_Tax = decimal.Parse("0.08");                 //세율
                        tax_type_list.Add("1", "8%");
                    }
                    else if (m_tax_type == "2")
                    {
                        m_Tax = decimal.Parse("0.1");                 //세율
                        tax_type_list.Add("2", "10%");
                    }


                    ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[8]).DataSource = new BindingSource(tax_list, null);
                    ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[8]).DisplayMember = "Value";
                    ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[8]).ValueMember = "Key";

                    ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[8]).DataSource = new BindingSource(tax_list, null);
                    ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[8]).DisplayMember = "Value";
                    ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[8]).ValueMember = "Key";

                    ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[9]).DataSource = new BindingSource(tax_type_list, null);
                    ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[9]).DisplayMember = "Value";
                    ((DataGridViewComboBoxColumn)GRD_GOODS.Columns[9]).ValueMember = "Key";

                    ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[9]).DataSource = new BindingSource(tax_type_list, null);
                    ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[9]).DisplayMember = "Value";
                    ((DataGridViewComboBoxColumn)GRD_CONSUM.Columns[9]).ValueMember = "Key";

                    //grdComboCell.Value = grdComboCell.Items[0];
                    //((DataGridViewComboBoxColumn)GRD_GOODS.Columns[8]).Value = "1";
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void BTN_CLOSE_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

    

        private void TXT_QTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자,백스페이스,마이너스,소숫점 만 입력받는다.
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46) //8:백스페이스,45:마이너스,46:소수점
            {
                e.Handled = true;
            }
        }

        private void TXT_ITEM_AMOUNT_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자,백스페이스,마이너스,소숫점 만 입력받는다.
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8 && e.KeyChar != 45 && e.KeyChar != 46) //8:백스페이스,45:마이너스,46:소수점
            {
                e.Handled = true;
            }
        }
    

        private void TXT_QTY_TextChanged(object sender, EventArgs e)
        {
            //if (!string.Empty.Equals(TXT_ITEM_AMOUNT.Text) && !string.Empty.Equals(TXT_QTY.Text))
            //{
            //    TXT_BUY_AMOUNT.Text = String.Format("{0:0,0.##}", (Int32.Parse(TXT_ITEM_AMOUNT.Text) * Int32.Parse(TXT_QTY.Text)));
            //    TXT_TAX_AMOUNT.Text = String.Format("{0:0,0.##}", (Int32.Parse(TXT_ITEM_AMOUNT.Text) * Int32.Parse(TXT_QTY.Text) * 0.07));
            //    TXT_REFUND_AMOUNT.Text = String.Format("{0:0,0.##}", (Int32.Parse(TXT_ITEM_AMOUNT.Text) * Int32.Parse(TXT_QTY.Text) * 0.05));
            //}
        }

      
        private void BTN_SAVE_Click(object sender, EventArgs e)
        {
            if(GRD_GOODS.RowCount <= 0 && GRD_CONSUM.RowCount <= 0)
            {
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("NO_ITEM"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BTN_SAVE.Focus();
                return;
            }
            
            if (string.Empty.Equals(TXT_TOTAL_REFUND_AMT.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("NO_ITEM"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BTN_SAVE.Focus();
                return;
            }
            if (string.Empty.Equals(TXT_TOTAL_BUY_AMT.Text))
            {
                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("NO_ITEM"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BTN_SAVE.Focus();
                return;
            }
           
            if(!Validation_Check(GRD_GOODS) || !Validation_Check(GRD_CONSUM))
            {
                BTN_SAVE.Focus();
                return;
            }

            if (RetArray == null)
                RetArray = new JArray();
            RetArray.Clear();

            //일반물품
            for (int i = 0; GRD_GOODS.RowCount > i; i++)
            {
                JObject tempObj = new JObject();
                tempObj.Add("COL1", GRD_GOODS[0, i].Value == null ? "" : GRD_GOODS[0, i].Value.ToString());//순번
                tempObj.Add("COL2", GRD_GOODS[1, i].Value == null ? "" : GRD_GOODS[1, i].Value.ToString());//대분류
                tempObj.Add("COL3", GRD_GOODS[2, i].Value == null ? "" : GRD_GOODS[2, i].Value.ToString());//중분류
                tempObj.Add("COL4", GRD_GOODS[3, i].Value == null ? "" : GRD_GOODS[3, i].Value.ToString());//상품명
                tempObj.Add("COL5", GRD_GOODS[4, i].Value == null ? "" : GRD_GOODS[4, i].Value.ToString());//갯수
                tempObj.Add("COL6", GRD_GOODS[5, i].Value == null ? "" : GRD_GOODS[5, i].Value.ToString());//품목가격
                tempObj.Add("COL7", GRD_GOODS[6, i].Value == null ? "" : GRD_GOODS[6, i].Value.ToString());//총 판매액
                tempObj.Add("COL8", GRD_GOODS[7, i].Value == null ? "" : GRD_GOODS[7, i].Value.ToString());//세금
                tempObj.Add("COL9", GRD_GOODS[8, i].Value == null ? "" : GRD_GOODS[8, i].Value.ToString());//세금종류
                tempObj.Add("COL10", GRD_GOODS[9, i].Value == null ? "" : GRD_GOODS[9, i].Value.ToString());//소비세 8% 10%
                tempObj.Add("COL11", GRD_GOODS[10, i].Value == null ? "" : GRD_GOODS[10, i].Value.ToString());//환급액
                tempObj.Add("COL12", "1");                                                                    //물품종류

                RetArray.Add(tempObj);
            }
            //소비용품
            for (int i = 0; GRD_CONSUM.RowCount > i; i++)
            {
                JObject tempObj = new JObject();
                tempObj.Add("COL1", GRD_CONSUM[0, i].Value == null ? "" : GRD_CONSUM[0, i].Value.ToString());//순번
                tempObj.Add("COL2", GRD_CONSUM[1, i].Value == null ? "" : GRD_CONSUM[1, i].Value.ToString());//대분류
                tempObj.Add("COL3", GRD_CONSUM[2, i].Value == null ? "" : GRD_CONSUM[2, i].Value.ToString());//중분류
                tempObj.Add("COL4", GRD_CONSUM[3, i].Value == null ? "" : GRD_CONSUM[3, i].Value.ToString());//상품명
                tempObj.Add("COL5", GRD_CONSUM[4, i].Value == null ? "" : GRD_CONSUM[4, i].Value.ToString());//갯수
                tempObj.Add("COL6", GRD_CONSUM[5, i].Value == null ? "" : GRD_CONSUM[5, i].Value.ToString());//품목가격
                tempObj.Add("COL7", GRD_CONSUM[6, i].Value == null ? "" : GRD_CONSUM[6, i].Value.ToString());//총 판매액
                tempObj.Add("COL8", GRD_CONSUM[7, i].Value == null ? "" : GRD_CONSUM[7, i].Value.ToString());//세금
                tempObj.Add("COL9", GRD_CONSUM[8, i].Value == null ? "" : GRD_CONSUM[8, i].Value.ToString());//세금종류
                tempObj.Add("COL10", GRD_CONSUM[9, i].Value == null ? "" : GRD_CONSUM[9, i].Value.ToString());//소비세 8% 10%
                tempObj.Add("COL11", GRD_CONSUM[10, i].Value == null ? "" : GRD_CONSUM[10, i].Value.ToString());//환급액
                tempObj.Add("COL12", "2");                                                                            //물품종류
                RetArray.Add(tempObj);
            }

            this.DialogResult = DialogResult.OK;
            Close();
        }


        private void BTN_INIT_Click(object sender, EventArgs e)
        {
            GRD_GOODS.Rows.Clear();
            GRD_CONSUM.Rows.Clear();
            TXT_GOODS_TOTAL_BUY_AMT.Text = "";
            TXT_CONSUM_TOTAL_BUY_AMT.Text = "";
            TXT_GOODS_TOTAL_TAX_AMT.Text = "";
            TXT_CONSUM_TOTAL_TAX_AMT.Text = "";
            TXT_TOTAL_BUY_AMT.Text = "";
            TXT_TOTAL_REFUND_AMT.Text = "";
            TXT_TOTAL_TAX_AMT.Text = "";
        }

        private void BTN_ADD_GOODS_Click(object sender, EventArgs e)
        {
            //갯수제한 체크
            if(m_nAlreadyGoodsItemCnt + GRD_GOODS.Rows.Count >= max_goods)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ITEM_MAX"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GRD_GOODS.Rows.Add();
            GRD_GOODS[0, GRD_GOODS.Rows.Count - 1].Value = GRD_GOODS.Rows.Count;
            //GRD_GOODS[8, GRD_GOODS.Rows.Count - 1].Value = "In";
            
            GRD_GOODS[8, GRD_GOODS.Rows.Count - 1].Value = m_bTaxOut ? "01": "02";

            if (m_tax_type == "0")
            {
                GRD_GOODS[9, GRD_GOODS.Rows.Count - 1].Value = "2";

            }
            else
            {
                GRD_GOODS[9, GRD_GOODS.Rows.Count - 1].Value = m_tax_type == "1" ? "1" : "2";
            }

            DataGridViewComboBoxCell cCell = ((DataGridViewComboBoxCell)GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[1]);
            if (!m_bSaleGoods)
            {
                UpdateComboItems("A0001", "GROUP", cCell);
            }
            else {
                DataGridViewComboBoxCell cCell2 = ((DataGridViewComboBoxCell)GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[2]);
                UpdateComboItems_SaleGoods("A0001", cCell, cCell2);
            }

            GRD_GOODS.Rows[GRD_GOODS.Rows.Count-1].Selected = true;
            GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[0].Selected = true;
            GRD_GOODS.FirstDisplayedScrollingRowIndex=GRD_GOODS.Rows.Count - 1;
        }

        private void BTN_ADD_CONSUM_Click(object sender, EventArgs e)
        {
            //갯수제한 체크
            if (m_nAlreadyConsumsItemCnt + GRD_CONSUM.Rows.Count >= max_goods)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ITEM_MAX"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GRD_CONSUM.Rows.Add();
            GRD_CONSUM[0, GRD_CONSUM.Rows.Count - 1].Value = GRD_CONSUM.Rows.Count;
            //GRD_CONSUM[8, GRD_CONSUM.Rows.Count - 1].Value = "In";
            GRD_CONSUM[8, GRD_CONSUM.Rows.Count - 1].Value = m_bTaxOut ? "01" : "02";

            if (m_tax_type == "0")
            {
                GRD_CONSUM[9, GRD_CONSUM.Rows.Count - 1].Value = "2";
            }
            else
            {
                GRD_CONSUM[9, GRD_CONSUM.Rows.Count - 1].Value = m_tax_type == "1" ? "1" : "2";
            }

            DataGridViewComboBoxCell cCell = ((DataGridViewComboBoxCell)GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[1]);
            if (!m_bSaleGoods)
            {
                UpdateComboItems("A0002", "GROUP", cCell);
            }
            else {
                DataGridViewComboBoxCell cCell2 = ((DataGridViewComboBoxCell)GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[2]);
                UpdateComboItems_SaleGoods("A0002", cCell, cCell2);
            }
            GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Selected = true;
            GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[0].Selected = true;
            GRD_CONSUM.FirstDisplayedScrollingRowIndex = GRD_CONSUM.Rows.Count - 1;
        }

        private void CAL_AMOUNT(DataGridViewCellEventArgs ev = null, MetroFramework.Controls.MetroGrid ctlGrd = null)
        {
            Boolean bTexIn = false;//내세, 외세 구분
            Boolean bTexUp = false;// 8% , 10% 구분

            decimal goodQty = 0;
            decimal goodAmt = 0;
            decimal goodsTax = 0;
            decimal goodsBuy = 0;
            //decimal goodsRefund = 0;
            decimal goodsTotalTax = 0;
            decimal goodsTotalBuy = 0;
            decimal goodsTotalRefund = 0;

            decimal consumQty = 0;
            decimal consumAmt = 0;
            decimal consumTax = 0;
            decimal consumBuy = 0;
            //decimal consumRefund = 0;
            decimal consumTotalTax = 0;
            decimal consumTotalBuy = 0;
            decimal consumTotalRefund = 0;

            long ret = 0;

            decimal tempAmt;

            //수정대상 컬럼만 데이터 변경

            //수량, 물품액을 수정했으면 대상 row 업데이트
            //총액만 수정하면 세액만 변경
            //세금만 변경하면 아무것도 변경안함.
            if (ev != null && ctlGrd != null)
            {
                if (  ev.ColumnIndex == 4 ||ev.ColumnIndex == 5 || ev.ColumnIndex == 6 || ev.ColumnIndex == 7)
                {
                    if(ctlGrd[ev.ColumnIndex, ev.RowIndex].Value != null && ctlGrd[ev.ColumnIndex, ev.RowIndex].Value is string)
                    {
                        ctlGrd[ev.ColumnIndex, ev.RowIndex].Value = decimal.Parse(ctlGrd[ev.ColumnIndex, ev.RowIndex].Value.ToString().Equals("") 
                            ? "0" : ctlGrd[ev.ColumnIndex, ev.RowIndex].Value.ToString());
                        return;
                    }
                }
                //if (ev.ColumnIndex == 4 || ev.ColumnIndex == 5 || ev.ColumnIndex == 6 || ev.ColumnIndex == 8)
                if (ev.ColumnIndex == 4 || ev.ColumnIndex == 5 || ev.ColumnIndex == 7 || ev.ColumnIndex == 8 || ev.ColumnIndex == 9)
                {
                    //수량
                    if (ctlGrd[4, ev.RowIndex].Value != null && long.TryParse(ctlGrd[4, ev.RowIndex].Value.ToString(), out ret))
                    {
                        goodQty = decimal.Parse(ctlGrd[4, ev.RowIndex].Value.ToString().Equals("") ? "0" : ctlGrd[4, ev.RowIndex].Value.ToString());
                        if (goodQty > 9999)
                        {
                            MetroMessageBox.Show(this, Constants.getMessage("ERROR_GOODS_QTY"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goodQty = 0;
                            ctlGrd[4, ev.RowIndex].Value = 0;
                        }
                    }

                    //물품액
                    if (ctlGrd[5, ev.RowIndex].Value != null && long.TryParse(ctlGrd[5, ev.RowIndex].Value.ToString(), out ret))
                        goodAmt = decimal.Parse(ctlGrd[5, ev.RowIndex].Value.ToString().Equals("") ? "0" : ctlGrd[5, ev.RowIndex].Value.ToString());
                    //물품 총액
                    if (ctlGrd[7, ev.RowIndex].Value != null && long.TryParse(ctlGrd[7, ev.RowIndex].Value.ToString(), out ret))
                        goodsBuy = decimal.Parse(ctlGrd[7, ev.RowIndex].Value.ToString().Equals("") ? "0" : ctlGrd[7, ev.RowIndex].Value.ToString());
                    //내/외세
                    if (ctlGrd[8, ev.RowIndex].Value != null && long.TryParse(ctlGrd[8, ev.RowIndex].Value.ToString(), out ret))
                        bTexIn = ctlGrd[8, ev.RowIndex].Value.ToString().Equals("02");
                    //8% 10%
                    if (ctlGrd[9, ev.RowIndex].Value != null && long.TryParse(ctlGrd[9, ev.RowIndex].Value.ToString(), out ret))
                        bTexUp = ctlGrd[9, ev.RowIndex].Value.ToString().Equals("1");

                    if (ev.ColumnIndex == 4 || ev.ColumnIndex == 5)
                    {
                        ctlGrd[7, ev.RowIndex].Value = goodQty * goodAmt;
                        goodsBuy = (goodQty * goodAmt);
                    }

                    if (ev.ColumnIndex == 4 || ev.ColumnIndex == 5 || ev.ColumnIndex == 7 || ev.ColumnIndex == 8 || ev.ColumnIndex == 9)
                    {
                        if (bTexUp)
                        {
                            m_Tax = decimal.Parse("0.08");
                        }
                        else
                        {
                            m_Tax = decimal.Parse("0.1");
                        }

                        //세금
                        if (bTexIn)
                        {
                            //tempAmt = (goodsBuy * (m_Tax / (m_Tax + 1)));//내세 시 세금 제외 금액
                            tempAmt = getFixAmt(m_TaxProcType, (goodsBuy / (m_Tax + 1))); //내세 시 세금 제외 금액
                            tempAmt = goodsBuy - tempAmt;
                        }
                        else
                        {
                            tempAmt = (goodsBuy * m_Tax);//외세 시 세금
                            tempAmt = getFixAmt(m_TaxProcType, tempAmt);
                        }

                        //m_TaxProcType = ((JObject)arrRet[0])["TAX_POINT_PROC_CODE"].ToString().Trim();        //세금 처리 구분
                        //m_FeeProcType = ((JObject)arrRet[0])["FEE_PROC_TIME_CODE"].ToString().Trim();           //수수료 처리 구분
                        ctlGrd[6, ev.RowIndex].Value = tempAmt;
                    }
                }
            }


            for (int i = 0; i < GRD_GOODS.RowCount; i++)
            {
                GRD_GOODS[0, i].Value = (i + 1);
                goodQty = 0;
                goodAmt = 0;
                goodsBuy = 0;
                goodsTax = 0;
                bTexIn = false;
                //수량
                if (GRD_GOODS[4, i].Value != null && long.TryParse(GRD_GOODS[4, i].Value.ToString(), out ret))
                    goodQty = decimal.Parse(GRD_GOODS[4, i].Value.ToString().Equals("") ? "0" : GRD_GOODS[4, i].Value.ToString());
                //물품액
                if (GRD_GOODS[5, i].Value != null && long.TryParse(GRD_GOODS[5, i].Value.ToString(), out ret))
                    goodAmt = decimal.Parse(GRD_GOODS[5, i].Value.ToString().Equals("") ? "0" : GRD_GOODS[5, i].Value.ToString());
                //물품 총액 
                if (GRD_GOODS[7, i].Value != null && long.TryParse(GRD_GOODS[7, i].Value.ToString(), out ret))
                    goodsBuy = decimal.Parse(GRD_GOODS[7, i].Value.ToString().Equals("") ? "0" : GRD_GOODS[7, i].Value.ToString());
                //물품 총액 세금
                if (GRD_GOODS[6, i].Value != null && long.TryParse(GRD_GOODS[6, i].Value.ToString(), out ret))
                    goodsTax = decimal.Parse(GRD_GOODS[6, i].Value.ToString().Equals("") ? "0" : GRD_GOODS[6, i].Value.ToString());
                //내/외세
                if (GRD_GOODS[8, i].Value != null && long.TryParse(GRD_GOODS[8, i].Value.ToString(), out ret))
                    bTexIn = GRD_GOODS[8, i].Value.ToString().Equals("02") ;

                //GRD_GOODS[6, i].Value = goodQty * goodAmt;
                //goodsTotalBuy += goodQty * goodAmt;
                goodsTotalBuy += goodsBuy;

                //세금

                //if (bTexIn)
                //{
                //    tempAmt = (goodQty * goodAmt * (m_Tax / (m_Tax +1)));//내세
                //}
                //else
                //{
                //    tempAmt = (goodQty * goodAmt * m_Tax);//외세
                //}
                //GRD_GOODS[7, i].Value = Math.Round(tempAmt).ToString();
                goodsTotalTax += Convert.ToInt64(goodsTax);

                //환급액
                tempAmt = (Convert.ToInt64((goodsTax)) *  m_Fee);
                tempAmt = goodsTax - getFixAmt(m_FeeProcType, tempAmt);
                GRD_GOODS[10, i].Value = tempAmt;
                goodsTotalRefund += Convert.ToInt64(Math.Round(tempAmt));
            }

            for (int i = 0; i < GRD_CONSUM.RowCount; i++)
            {
                GRD_CONSUM[0, i].Value = (i + 1);
                consumQty = 0;
                consumAmt = 0;
                consumBuy = 0;
                consumTax = 0;
                //수량
                if (GRD_CONSUM[4, i].Value != null && long.TryParse(GRD_CONSUM[4, i].Value.ToString(), out ret))
                    consumQty = Int32.Parse(GRD_CONSUM[4, i].Value.ToString().Equals("") ? "0" : GRD_CONSUM[4, i].Value.ToString());
                //물품액
                if (GRD_CONSUM[5, i].Value != null && long.TryParse(GRD_CONSUM[5, i].Value.ToString(), out ret))
                    consumAmt = long.Parse(GRD_CONSUM[5, i].Value.ToString().Equals("") ? "0" : GRD_CONSUM[5, i].Value.ToString());
                //물품 총액
                if (GRD_CONSUM[7, i].Value != null && long.TryParse(GRD_CONSUM[7, i].Value.ToString(), out ret))
                    consumBuy = decimal.Parse(GRD_CONSUM[7, i].Value.ToString().Equals("") ? "0" : GRD_CONSUM[7, i].Value.ToString());
                //물품 총액 세금
                if (GRD_CONSUM[6, i].Value != null && long.TryParse(GRD_CONSUM[6, i].Value.ToString(), out ret))
                    consumTax = decimal.Parse(GRD_CONSUM[6, i].Value.ToString().Equals("") ? "0" : GRD_CONSUM[6, i].Value.ToString());
                //내/외세
                if (GRD_CONSUM[8, i].Value != null && long.TryParse(GRD_CONSUM[8, i].Value.ToString(), out ret))
                    bTexIn = GRD_CONSUM[8, i].Value.ToString().Equals("02");

                //총액
                //GRD_CONSUM[6, i].Value = consumQty * consumAmt;
                //consumBuy += consumQty * consumAmt;
                consumTotalBuy += consumBuy;

                //세금
                //if (bTexIn)
                //{
                //    tempAmt = (consumQty * consumAmt * (m_Tax / (m_Tax + 1)));//내세
                //}
                //else
                //{
                //    tempAmt = (consumQty * consumAmt * m_Tax);//외세
                //}

                //GRD_CONSUM[7, i].Value = Math.Round(tempAmt).ToString();
                consumTotalTax += Convert.ToInt64(consumTax);

                //환급액
                tempAmt = (Convert.ToInt64(consumTax) *  m_Fee);
                tempAmt = consumTax - getFixAmt(m_FeeProcType, tempAmt);
                GRD_CONSUM[10, i].Value = tempAmt;
                consumTotalRefund += Convert.ToInt64(Math.Round(tempAmt));
            }

            TXT_GOODS_TOTAL_BUY_AMT.Text = goodsTotalBuy.ToString("N0");
            //TXT_GOODS_TOTAL_TAX_AMT.Text = goodsTotalRefund.ToString("N0");
            TXT_GOODS_TOTAL_TAX_AMT.Text = goodsTotalTax.ToString("N0");
            TXT_CONSUM_TOTAL_BUY_AMT.Text = consumTotalBuy.ToString("N0");
            //TXT_CONSUM_TOTAL_TAX_AMT.Text = consumTotalRefund.ToString("N0");
            TXT_CONSUM_TOTAL_TAX_AMT.Text = consumTotalTax.ToString("N0");
            TXT_TOTAL_BUY_AMT.Text = (goodsTotalBuy + consumTotalBuy).ToString("N0");
            TXT_TOTAL_REFUND_AMT.Text = (goodsTotalRefund + consumTotalRefund).ToString("N0");
            TXT_TOTAL_TAX_AMT.Text = (goodsTotalTax + consumTotalTax).ToString("N0");
        }

        private void GRD_GOODS_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ItemForm_GRD_CellValueChanged(sender, e, GRD_GOODS);
        }

        private void GRD_CONSUM_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            ItemForm_GRD_CellValueChanged(sender, e, GRD_CONSUM);
        }

        private void GRD_CONSUM_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 11 && e.RowIndex >= 0)
            {
                GRD_CONSUM.Rows.RemoveAt(e.RowIndex);
                if (GRD_CONSUM.Rows.Count == 0)
                {
                    GRD_CONSUM.Rows.Clear();
                    GRD_CONSUM.Rows.Add();
                    GRD_CONSUM.Rows.RemoveAt(0);
                }
                else
                {
                    ((MetroScrollBar)GRD_CONSUM.Controls[1]).Height = 0;

                    GRD_CONSUM.Rows[0].Selected = true;
                    GRD_CONSUM.Rows[0].Cells[0].Selected = true;
                    GRD_CONSUM.FirstDisplayedScrollingRowIndex = 0;

                    GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Selected = true;
                    GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[0].Selected = true;
                    GRD_CONSUM.FirstDisplayedScrollingRowIndex = GRD_CONSUM.Rows.Count - 1;
                }

                GRD_CONSUM.Update();
                CAL_AMOUNT();
            }
        }

        private void GRD_GOODS_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 11 && e.RowIndex >= 0)
            {
                GRD_GOODS.Rows.RemoveAt(e.RowIndex);

                if(GRD_GOODS.Rows.Count == 0)
                {
                    GRD_GOODS.Rows.Clear();
                    GRD_GOODS.Rows.Add();
                    GRD_GOODS.Rows.RemoveAt(0);
                }
                else
                {
                    ((MetroScrollBar)GRD_GOODS.Controls[1]).Height = 0;

                    GRD_GOODS.Rows[0].Selected = true;
                    GRD_GOODS.Rows[0].Cells[0].Selected = true;
                    GRD_GOODS.FirstDisplayedScrollingRowIndex = 0;

                    GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Selected = true;
                    GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[0].Selected = true;
                    GRD_GOODS.FirstDisplayedScrollingRowIndex = GRD_GOODS.Rows.Count - 1;
                }
                GRD_GOODS.Update();
                CAL_AMOUNT();
            }
        }

        private void UpdateComboItems(string strCode, string strType, DataGridViewComboBoxCell grdComboCell, DataGridViewCell itemNameCell = null)
        {
            try {
                if (strCode == null || string.Empty.Equals(strCode))
                {
                    return;
                }
                Transaction tran = new Transaction();
                JObject tempCode = new JObject();
                string[] filerArr = null;
                if ("GROUP".Equals(strType))
                {
                    filerArr = m_arrGoodsGroup;
                }
                else if ("DIVISION".Equals(strType))
                {
                    filerArr = m_arrGoodsDivision;
                }
                else
                {
                    return;
                }
                //Parent 로 찾음
                tempCode.Add("P_CODE", strCode);
                JArray arrCode = tran.SearchItemList(tempCode);

                if (arrCode != null && arrCode.Count > 0)
                {
                    string firstData = "";
                    Dictionary<string, string> products_list = new Dictionary<string, string>();
                    for (int i = 0; i < arrCode.Count; i++)
                    {
                        tempCode = ((JObject)arrCode[i]);
                        for (int j = 0; j < filerArr.Length; j++)
                        {
                            if (filerArr[j].Equals(tempCode["CATEGORY_CODE"].ToString()))
                            {
                                if (itemNameCell == null)
                                {
                                    products_list.Add(tempCode["CATEGORY_CODE"].ToString(), tempCode["CATEGORY_NAME"].ToString());
                                }
                                else
                                {
                                    products_list.Add(tempCode["CATEGORY_CODE"].ToString(), "その他");
                                }

                                if (string.Empty.Equals(firstData))
                                    firstData = tempCode["CATEGORY_CODE"].ToString();
                                break;
                            }
                        }
                    }
                    if (products_list.Count > 0)
                    {
                        grdComboCell.DataSource = new BindingSource(products_list, null);
                        grdComboCell.DisplayMember = "Value";
                        grdComboCell.ValueMember = "Key";
                        //grdComboCell.Value = grdComboCell.Items[0];
                        grdComboCell.Value = firstData;
                        if (itemNameCell != null)
                            itemNameCell.Value = products_list[firstData];
                    }
                    //if (tempGrid != null)
                    //{
                    //    tempGrid[grdComboCell.ColumnIndex, grdComboCell.RowIndex].Value = ((JObject)sorted[0])["CATEGORY_CODE"].ToString();
                    //}
                    //grdComboCell.DataGridView[grdComboCell.ColumnIndex, grdComboCell.RowIndex].Value = ((JObject)sorted[0])["CATEGORY_CODE"];
                    products_list = null;
                }
                arrCode.Clear();
                tempCode = null;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void UpdateComboItems_SaleGoods(string strGoods, DataGridViewComboBoxCell grdParentComboCell, DataGridViewComboBoxCell grdComboCell)
        {
            try
            {
                Transaction tran = new Transaction();
                JObject tempCode = new JObject();
                
                Dictionary<string, string> products_list_Parent = new Dictionary<string, string>();
                Dictionary<string, string> products_list = new Dictionary<string, string>();
                string firstData = "";
                //string firstParentData = "";

                //대분류  세팅
                for (int i = 0; i < m_arrGoodsGroup.Length; i++)
                {
                    tempCode = new JObject();
                    //Parent 로 찾음
                    tempCode.Add("CATEGORY_CODE", m_arrGoodsGroup[i]);
                    tempCode.Add("P_CODE", strGoods);
                    JArray arrCode = tran.SearchItemList(tempCode);
                    if (arrCode != null)
                    {
                        for (int j = 0; j < arrCode.Count; j++)
                        {
                            tempCode = ((JObject)arrCode[j]);
                            products_list_Parent.Add(tempCode["CATEGORY_CODE"].ToString(), tempCode["CATEGORY_NAME"].ToString());
                            if (string.Empty.Equals(firstData))
                            {
                                firstData = tempCode["CATEGORY_CODE"].ToString();
                            }
                            tempCode = null;
                        }
                    }
                    tempCode = null;
                }
                if (products_list_Parent.Count > 0)
                {
                    grdParentComboCell.DataSource = new BindingSource(products_list_Parent, null);
                    grdParentComboCell.DisplayMember = "Value";
                    grdParentComboCell.ValueMember = "Key";
                    grdParentComboCell.Value = firstData;
                    
                }
                firstData = "";

                List<string> keyList = new List<string>(products_list_Parent.Keys);

                //중분류 세팅
                for (int i = 0; i < keyList.Count; i++)
                {
                    
                    //Parent 로 찾음
                    
                    for (int j = 0; j < m_arrGoodsDivision.Length ; j++)
                    {
                        tempCode = new JObject();
                        tempCode.Add("P_CODE", keyList[i]);
                        tempCode.Add("CATEGORY_CODE", m_arrGoodsDivision[j]);
                        JArray arrCode = tran.SearchItemList(tempCode);
                        if (arrCode != null)
                        {
                            for (int k = 0; k < arrCode.Count; k++)
                            {
                                tempCode = ((JObject)arrCode[k]);
                                products_list.Add(tempCode["CATEGORY_CODE"].ToString(), tempCode["CATEGORY_NAME"].ToString());
                                if (string.Empty.Equals(firstData))
                                {
                                    firstData = tempCode["CATEGORY_CODE"].ToString();
                                    //firstParentData = tempCode["P_CODE"].ToString();
                                }
                                tempCode = null;
                            }
                        }
                    }
                    tempCode = null;
                }
                if (products_list.Count > 0)
                {
                    grdComboCell.DataSource = new BindingSource(products_list, null);
                    grdComboCell.DisplayMember = "Value";
                    grdComboCell.ValueMember = "Key";
                    //grdParentComboCell.Value = firstParentData;
                    grdComboCell.Value = firstData;
                    //grdComboCell.Value = grdComboCell.Items[0];
                    //grdComboCell.Value = firstData;
                }
                tran = null;
                tempCode = null;

                products_list_Parent.Clear();
                products_list.Clear();
                firstData = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void GRD_GOODS_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Console.WriteLine("GRD_GOODS_DataError");
        }

        private void GRD_GOODS_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (GRD_GOODS.IsCurrentCellDirty)
            {
                GRD_GOODS.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void GRD_CONSUM_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (GRD_CONSUM.IsCurrentCellDirty)
            {
                GRD_CONSUM.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void GRD_GOODS_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 11)
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


        private void GRD_CONSUM_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 11)
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

        private void GRD_GOODS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private Boolean Validation_Check(MetroFramework.Controls.MetroGrid ctlGrid)
        {
            for (int i = 0; ctlGrid.RowCount > i; i++)
            {
                if (ctlGrid[4, i].Value == null || string.Empty.Equals(ctlGrid[4, i].Value.ToString()) || "0".Equals(ctlGrid[4, i].Value.ToString()))
                {
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_QTY"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (ctlGrid[5, i].Value == null || string.Empty.Equals(ctlGrid[5, i].Value.ToString()) || "0".Equals(ctlGrid[5, i].Value.ToString()))
                {
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_ITEM_AMT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (ctlGrid[6, i].Value == null || string.Empty.Equals(ctlGrid[6, i].Value.ToString()) || "0".Equals(ctlGrid[6, i].Value.ToString()))
                {
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_TAX_AMT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    return false;
                }
                if (ctlGrid[7, i].Value == null || string.Empty.Equals(ctlGrid[7, i].Value.ToString()) || "0".Equals(ctlGrid[7, i].Value.ToString()))
                {
                    MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_BUY_AMT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }
        private void ItemForm_GRD_CellValueChanged(object sender, DataGridViewCellEventArgs e, MetroFramework.Controls.MetroGrid ctlGrid)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 1)//대분류 변경 시 중분류 변경.
                {
                    if (!m_bSaleGoods)
                    {
                        DataGridViewComboBoxCell cCell_data = ((DataGridViewComboBoxCell)ctlGrid.Rows[e.RowIndex].Cells[1]);
                        if (cCell_data.Value != null)
                        {
                            Object tempObj = cCell_data.Value;
                            DataGridViewComboBoxCell cCell = ((DataGridViewComboBoxCell)ctlGrid.Rows[e.RowIndex].Cells[2]);
                            DataGridViewCell cCell2 = ctlGrid.Rows[e.RowIndex].Cells[3];
                            if (cCell_data != null && cCell_data.Value != null && !string.Empty.Equals(cCell_data.Value.ToString().Trim()))
                            {
                                cCell.Value = "";
                                cCell2.Value = "";
                                UpdateComboItems(cCell_data.Value.ToString(), "DIVISION", cCell, cCell2);
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 2)//중분류 변경 시 품목명 변경.
                {

                    DataGridViewComboBoxCell cCell_data = ((DataGridViewComboBoxCell)ctlGrid.Rows[e.RowIndex].Cells[2]);
                    if (cCell_data.Value != null)
                    {
                        DataGridViewCell cCell_data3 = (DataGridViewCell)ctlGrid.Rows[e.RowIndex].Cells[3];
                        if (m_bSaleGoods)
                        {
                            Transaction tran = new Transaction();
                            JObject paramObj = new JObject();
                            paramObj.Add("CATEGORY_CODE", cCell_data.Value.ToString());

                            JArray arrRet = tran.SearchItemList(paramObj);
                            if (arrRet != null && arrRet.Count > 0)
                            {
                                DataGridViewComboBoxCell cCell_data1 = ((DataGridViewComboBoxCell)ctlGrid.Rows[e.RowIndex].Cells[1]);


                                JObject tempObj = (JObject)arrRet[0];
                                cCell_data1.Value = tempObj["P_CODE"].ToString();
                                cCell_data3.Value = tempObj["CATEGORY_NAME"].ToString();
                            }
                        }
                        else
                        {
                            cCell_data3.Value = cCell_data.FormattedValue.ToString();
                        }
                    }
                    if (e.ColumnIndex == 4)//수량체크
                    {
                        if (ctlGrid[e.ColumnIndex, e.RowIndex].Value != null)
                        {
                            Decimal tempDec = Decimal.Parse(ctlGrid[e.ColumnIndex, e.RowIndex].Value.ToString());
                            if (tempDec.ToString().Length > 12)
                            {
                                tempDec = 0;
                                MetroFramework.MetroMessageBox.Show(this, Constants.getMessage("ERROR_QTY_CNT"), "Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ctlGrid[e.ColumnIndex, e.RowIndex].Value = "";
                                return;
                            }
                        }
                    }
                }
                CAL_AMOUNT( e, ctlGrid );
            }
        }
        Decimal getFixAmt(string strType, Decimal nAmt)
        {
            if (strType == "03")
            {
                return Math.Round(nAmt);
            }
            else if (strType == "04")
            {
                return Math.Ceiling(nAmt);
            }
            else {
                return Math.Floor(nAmt);
            }
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
                    case Keys.Escape:
                        BTN_CLOSE.Focus();
                        BTN_CLOSE_Click(null, null);
                        return true;
                    case Keys.Alt | Keys.A:
                        BTN_SAVE.Focus();
                        BTN_SAVE_Click(null, null);
                        return true;
                    case Keys.F1:
                        BTN_ADD_GOODS.Focus();
                        BTN_ADD_GOODS_Click(null, null);
                        return true;
                    case Keys.F2:
                        BTN_ADD_CONSUM.Focus();
                        BTN_ADD_CONSUM_Click(null, null);
                        return true;
                    case Keys.F3:
                        BTN_ADD_GOODS_8.Focus();
                        BTN_ADD_GOODS_8_Click(null, null);
                        return true;
                    case Keys.F4:
                        BTN_ADD_CONSUM_8.Focus();
                        BTN_ADD_CONSUM_8_Click(null, null);
                        return true;
                    case Keys.F5:
                        BTN_INIT.Focus();
                        BTN_INIT_Click(null, null);
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        private void GRD_GOODS_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int nEditColInjdex =  (GRD_GOODS.CurrentCell.OwningColumn.Index) ;
            if (nEditColInjdex < 4)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
            else
            {
                e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
        }

        private void txtCheckNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)) )
                e.Handled = true;
        }

        private void GRD_CONSUM_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int nEditColInjdex = (GRD_CONSUM.CurrentCell.OwningColumn.Index);
            if (nEditColInjdex < 4)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
            else
            {
                e.Control.KeyPress += new KeyPressEventHandler(txtCheckNumeric_KeyPress);
            }
        }

        private void BTN_ADD_GOODS_8_Click(object sender, EventArgs e)
        {
            //갯수제한 체크
            if (m_nAlreadyGoodsItemCnt + GRD_GOODS.Rows.Count >= 50)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ITEM_MAX"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GRD_GOODS.Rows.Add();
            GRD_GOODS[0, GRD_GOODS.Rows.Count - 1].Value = GRD_GOODS.Rows.Count;
            //GRD_GOODS[8, GRD_GOODS.Rows.Count - 1].Value = "In";
            GRD_GOODS[8, GRD_GOODS.Rows.Count - 1].Value = m_bTaxOut ? "01" : "02";

            if (m_tax_type == "0")
            {
                GRD_GOODS[9, GRD_GOODS.Rows.Count - 1].Value = "1";

            }
            else
            {
                GRD_GOODS[9, GRD_GOODS.Rows.Count - 1].Value = m_tax_type == "1" ? "1" : "2";
            }

            DataGridViewComboBoxCell cCell = ((DataGridViewComboBoxCell)GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[1]);
            if (!m_bSaleGoods)
            {
                UpdateComboItems("A0001", "GROUP", cCell);
            }
            else
            {
                DataGridViewComboBoxCell cCell2 = ((DataGridViewComboBoxCell)GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[2]);
                UpdateComboItems_SaleGoods("A0001", cCell, cCell2);
            }

            GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Selected = true;
            GRD_GOODS.Rows[GRD_GOODS.Rows.Count - 1].Cells[0].Selected = true;
            GRD_GOODS.FirstDisplayedScrollingRowIndex = GRD_GOODS.Rows.Count - 1;
        }

        private void BTN_ADD_CONSUM_8_Click(object sender, EventArgs e)
        {
            //갯수제한 체크
            if (m_nAlreadyConsumsItemCnt + GRD_CONSUM.Rows.Count >= 50)
            {
                MetroMessageBox.Show(this, Constants.getMessage("ITEM_MAX"), "Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            GRD_CONSUM.Rows.Add();
            GRD_CONSUM[0, GRD_CONSUM.Rows.Count - 1].Value = GRD_CONSUM.Rows.Count;
            //GRD_CONSUM[8, GRD_CONSUM.Rows.Count - 1].Value = "In";
            GRD_CONSUM[8, GRD_CONSUM.Rows.Count - 1].Value = m_bTaxOut ? "01" : "02";


            if (m_tax_type == "0")
            {
                GRD_CONSUM[9, GRD_CONSUM.Rows.Count - 1].Value = "1";

            }
            else
            {
                GRD_CONSUM[9, GRD_CONSUM.Rows.Count - 1].Value = m_tax_type == "1" ? "1" : "2";
            }

            DataGridViewComboBoxCell cCell = ((DataGridViewComboBoxCell)GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[1]);
            if (!m_bSaleGoods)
            {
                UpdateComboItems("A0002", "GROUP", cCell);
            }
            else
            {
                DataGridViewComboBoxCell cCell2 = ((DataGridViewComboBoxCell)GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[2]);
                UpdateComboItems_SaleGoods("A0002", cCell, cCell2);
            }
            GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Selected = true;
            GRD_CONSUM.Rows[GRD_CONSUM.Rows.Count - 1].Cells[0].Selected = true;
            GRD_CONSUM.FirstDisplayedScrollingRowIndex = GRD_CONSUM.Rows.Count - 1;
        }
    }

}
