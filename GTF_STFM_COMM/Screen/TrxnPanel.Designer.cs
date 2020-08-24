using System;

namespace GTF_STFM_COMM.Screen
{
    partial class TrxnPanel
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LBL_REFUND_DATE = new MetroFramework.Controls.MetroLabel();
            this.TXT_REFUND_FROMDATE = new MetroFramework.Controls.MetroDateTime();
            this.BTN_CANCEL = new MetroFramework.Controls.MetroButton();
            this.TXT_SLIP_NO = new MetroFramework.Controls.MetroTextBox();
            this.LBL_SLIP_NO = new MetroFramework.Controls.MetroLabel();
            this.GRD_SLIP = new MetroFramework.Controls.MetroGrid();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LAY_SEARCH = new System.Windows.Forms.TableLayoutPanel();
            this.COM_DATE_COND = new MetroFramework.Controls.MetroComboBox();
            this.COM_REFUND_STATUS = new MetroFramework.Controls.MetroComboBox();
            this.TXT_TOTAL_SLIPSEQ = new MetroFramework.Controls.MetroTextBox();
            this.LBL_TOTAL_SLIPSEQ = new MetroFramework.Controls.MetroLabel();
            this.TXT_REFUND_TODATE = new MetroFramework.Controls.MetroDateTime();
            this.LBL_REFUND_STATUS = new MetroFramework.Controls.MetroLabel();
            this.TIL_1 = new MetroFramework.Controls.MetroTile();
            this.BTN_NEXT = new MetroFramework.Controls.MetroButton();
            this.BTN_PREV = new MetroFramework.Controls.MetroButton();
            this.BTN_SEARCH = new MetroFramework.Controls.MetroButton();
            this.LBL_TOTAL_CNT = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.LAY_PAGE = new System.Windows.Forms.TableLayoutPanel();
            this.cmbbox_Page = new System.Windows.Forms.ComboBox();
            this.lbl_Page = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.GRD_SLIP)).BeginInit();
            this.LAY_SEARCH.SuspendLayout();
            this.LAY_PAGE.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBL_REFUND_DATE
            // 
            this.LBL_REFUND_DATE.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_REFUND_DATE.AutoSize = true;
            this.LBL_REFUND_DATE.Location = new System.Drawing.Point(3, 12);
            this.LBL_REFUND_DATE.Name = "LBL_REFUND_DATE";
            this.LBL_REFUND_DATE.Size = new System.Drawing.Size(38, 20);
            this.LBL_REFUND_DATE.TabIndex = 45;
            this.LBL_REFUND_DATE.Text = "Date";
            // 
            // TXT_REFUND_FROMDATE
            // 
            this.TXT_REFUND_FROMDATE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_REFUND_FROMDATE.CalendarFont = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TXT_REFUND_FROMDATE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TXT_REFUND_FROMDATE.Location = new System.Drawing.Point(357, 4);
            this.TXT_REFUND_FROMDATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_REFUND_FROMDATE.MinimumSize = new System.Drawing.Size(0, 30);
            this.TXT_REFUND_FROMDATE.Name = "TXT_REFUND_FROMDATE";
            this.TXT_REFUND_FROMDATE.Size = new System.Drawing.Size(233, 30);
            this.TXT_REFUND_FROMDATE.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_REFUND_FROMDATE.TabIndex = 0;
            // 
            // BTN_CANCEL
            // 
            this.BTN_CANCEL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTN_CANCEL.Location = new System.Drawing.Point(1218, 18);
            this.BTN_CANCEL.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_CANCEL.Name = "BTN_CANCEL";
            this.BTN_CANCEL.Size = new System.Drawing.Size(171, 36);
            this.BTN_CANCEL.TabIndex = 3;
            this.BTN_CANCEL.Text = "Cancel";
            this.BTN_CANCEL.UseSelectable = true;
            this.BTN_CANCEL.Click += new System.EventHandler(this.BTN_CANCEL_Click);
            // 
            // TXT_SLIP_NO
            // 
            this.TXT_SLIP_NO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LAY_SEARCH.SetColumnSpan(this.TXT_SLIP_NO, 2);
            // 
            // 
            // 
            this.TXT_SLIP_NO.CustomButton.Image = null;
            this.TXT_SLIP_NO.CustomButton.Location = new System.Drawing.Point(350, 1);
            this.TXT_SLIP_NO.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_SLIP_NO.CustomButton.Name = "";
            this.TXT_SLIP_NO.CustomButton.Size = new System.Drawing.Size(35, 35);
            this.TXT_SLIP_NO.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_SLIP_NO.CustomButton.TabIndex = 1;
            this.TXT_SLIP_NO.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_SLIP_NO.CustomButton.UseSelectable = true;
            this.TXT_SLIP_NO.CustomButton.Visible = false;
            this.TXT_SLIP_NO.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_SLIP_NO.Lines = new string[0];
            this.TXT_SLIP_NO.Location = new System.Drawing.Point(89, 49);
            this.TXT_SLIP_NO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_SLIP_NO.MaxLength = 32767;
            this.TXT_SLIP_NO.Name = "TXT_SLIP_NO";
            this.TXT_SLIP_NO.PasswordChar = '\0';
            this.TXT_SLIP_NO.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_SLIP_NO.SelectedText = "";
            this.TXT_SLIP_NO.SelectionLength = 0;
            this.TXT_SLIP_NO.SelectionStart = 0;
            this.TXT_SLIP_NO.ShortcutsEnabled = true;
            this.TXT_SLIP_NO.Size = new System.Drawing.Size(386, 37);
            this.TXT_SLIP_NO.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_SLIP_NO.TabIndex = 3;
            this.TXT_SLIP_NO.UseSelectable = true;
            this.TXT_SLIP_NO.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_SLIP_NO.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // LBL_SLIP_NO
            // 
            this.LBL_SLIP_NO.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_SLIP_NO.AutoSize = true;
            this.LBL_SLIP_NO.Location = new System.Drawing.Point(3, 57);
            this.LBL_SLIP_NO.Name = "LBL_SLIP_NO";
            this.LBL_SLIP_NO.Size = new System.Drawing.Size(60, 20);
            this.LBL_SLIP_NO.TabIndex = 87;
            this.LBL_SLIP_NO.Text = "SLIP NO";
            // 
            // GRD_SLIP
            // 
            this.GRD_SLIP.AllowUserToAddRows = false;
            this.GRD_SLIP.AllowUserToDeleteRows = false;
            this.GRD_SLIP.AllowUserToResizeColumns = false;
            this.GRD_SLIP.AllowUserToResizeRows = false;
            this.GRD_SLIP.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GRD_SLIP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.GRD_SLIP.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GRD_SLIP.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(133)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GRD_SLIP.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GRD_SLIP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GRD_SLIP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column8,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column9,
            this.Column2,
            this.Column15,
            this.Column3,
            this.Column4,
            this.Column16,
            this.Column19,
            this.Column17,
            this.Column18});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 8F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(133)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GRD_SLIP.DefaultCellStyle = dataGridViewCellStyle12;
            this.GRD_SLIP.EnableHeadersVisualStyles = false;
            this.GRD_SLIP.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.GRD_SLIP.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.GRD_SLIP.Location = new System.Drawing.Point(14, 210);
            this.GRD_SLIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GRD_SLIP.Name = "GRD_SLIP";
            this.GRD_SLIP.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(133)))), ((int)(((byte)(72)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GRD_SLIP.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.GRD_SLIP.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GRD_SLIP.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.GRD_SLIP.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.GRD_SLIP.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.GRD_SLIP.RowTemplate.Height = 23;
            this.GRD_SLIP.RowTemplate.ReadOnly = true;
            this.GRD_SLIP.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GRD_SLIP.ShowEditingIcon = false;
            this.GRD_SLIP.Size = new System.Drawing.Size(1398, 691);
            this.GRD_SLIP.Style = MetroFramework.MetroColorStyle.Orange;
            this.GRD_SLIP.TabIndex = 2;
            this.GRD_SLIP.UseStyleColors = true;
            this.GRD_SLIP.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.metroGrid1_CellContentClick);
            this.GRD_SLIP.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GRD_SLIP_CellDoubleClick);
            this.GRD_SLIP.Scroll += new System.Windows.Forms.ScrollEventHandler(this.GRD_SLIP_Scroll);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "No";
            this.Column1.Name = "Column1";
            this.Column1.Width = 35;
            // 
            // Column8
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column8.HeaderText = "Slip No";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 170;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Sale Date";
            this.Column5.Name = "Column5";
            this.Column5.Width = 115;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "Shop Name";
            this.Column6.MinimumWidth = 80;
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = "0";
            this.Column7.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column7.HeaderText = "EXCOMM BUY";
            this.Column7.Name = "Column7";
            this.Column7.Width = 85;
            // 
            // Column10
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = "0";
            this.Column10.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column10.HeaderText = "EXCOMM TAX";
            this.Column10.Name = "Column10";
            this.Column10.Width = 80;
            // 
            // Column11
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = "0";
            this.Column11.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column11.HeaderText = "EXCOMM REFUND";
            this.Column11.Name = "Column11";
            this.Column11.Width = 75;
            // 
            // Column12
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = "0";
            this.Column12.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column12.HeaderText = "COMM BUY";
            this.Column12.Name = "Column12";
            this.Column12.Width = 80;
            // 
            // Column13
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = "0";
            this.Column13.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column13.HeaderText = "COMM REFUND";
            this.Column13.Name = "Column13";
            this.Column13.Width = 75;
            // 
            // Column9
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = "0";
            this.Column9.DefaultCellStyle = dataGridViewCellStyle9;
            this.Column9.HeaderText = "COMM TAX";
            this.Column9.Name = "Column9";
            this.Column9.Width = 80;
            // 
            // Column2
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle10;
            this.Column2.HeaderText = "Refund State";
            this.Column2.Name = "Column2";
            this.Column2.Width = 70;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "TOTAL SLIPSEQ";
            this.Column15.Name = "Column15";
            this.Column15.Width = 85;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "WORKERID";
            this.Column3.Name = "Column3";
            this.Column3.Width = 90;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Digitize result";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column16
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.NullValue = "0";
            this.Column16.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column16.HeaderText = "Print Count";
            this.Column16.Name = "Column16";
            this.Column16.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column16.Visible = false;
            this.Column16.Width = 60;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "RefundSateCode";
            this.Column19.Name = "Column19";
            this.Column19.Visible = false;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "RefundWayCode";
            this.Column17.Name = "Column17";
            this.Column17.Visible = false;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "ProgressStatusCode";
            this.Column18.Name = "Column18";
            this.Column18.Visible = false;
            // 
            // LAY_SEARCH
            // 
            this.LAY_SEARCH.ColumnCount = 6;
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.252632F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.99744F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.93706F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.93797F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.17896F));
            this.LAY_SEARCH.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.93206F));
            this.LAY_SEARCH.Controls.Add(this.COM_DATE_COND, 1, 0);
            this.LAY_SEARCH.Controls.Add(this.LBL_REFUND_DATE, 0, 0);
            this.LAY_SEARCH.Controls.Add(this.LBL_SLIP_NO, 0, 1);
            this.LAY_SEARCH.Controls.Add(this.TXT_SLIP_NO, 1, 1);
            this.LAY_SEARCH.Controls.Add(this.TXT_REFUND_FROMDATE, 2, 0);
            this.LAY_SEARCH.Controls.Add(this.COM_REFUND_STATUS, 6, 0);
            this.LAY_SEARCH.Controls.Add(this.TXT_TOTAL_SLIPSEQ, 6, 1);
            this.LAY_SEARCH.Controls.Add(this.LBL_TOTAL_SLIPSEQ, 4, 1);
            this.LAY_SEARCH.Controls.Add(this.TXT_REFUND_TODATE, 3, 0);
            this.LAY_SEARCH.Controls.Add(this.LBL_REFUND_STATUS, 4, 0);
            this.LAY_SEARCH.Location = new System.Drawing.Point(14, 62);
            this.LAY_SEARCH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.LAY_SEARCH.Name = "LAY_SEARCH";
            this.LAY_SEARCH.RowCount = 2;
            this.LAY_SEARCH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LAY_SEARCH.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LAY_SEARCH.Size = new System.Drawing.Size(1379, 90);
            this.LAY_SEARCH.TabIndex = 1;
            // 
            // COM_DATE_COND
            // 
            this.COM_DATE_COND.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_DATE_COND.FormattingEnabled = true;
            this.COM_DATE_COND.ItemHeight = 24;
            this.COM_DATE_COND.Items.AddRange(new object[] {
            "Sales Date",
            "Publish Date",
            "Refund Date",
            "Register Date"});
            this.COM_DATE_COND.Location = new System.Drawing.Point(89, 4);
            this.COM_DATE_COND.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_DATE_COND.Name = "COM_DATE_COND";
            this.COM_DATE_COND.Size = new System.Drawing.Size(241, 30);
            this.COM_DATE_COND.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_DATE_COND.TabIndex = 133;
            this.COM_DATE_COND.UseSelectable = true;
            // 
            // COM_REFUND_STATUS
            // 
            this.COM_REFUND_STATUS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_REFUND_STATUS.FormattingEnabled = true;
            this.COM_REFUND_STATUS.ItemHeight = 24;
            this.COM_REFUND_STATUS.Items.AddRange(new object[] {
            "ALL",
            "Refund Before",
            "Refund End"});
            this.COM_REFUND_STATUS.Location = new System.Drawing.Point(1023, 4);
            this.COM_REFUND_STATUS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_REFUND_STATUS.Name = "COM_REFUND_STATUS";
            this.COM_REFUND_STATUS.Size = new System.Drawing.Size(353, 30);
            this.COM_REFUND_STATUS.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_REFUND_STATUS.TabIndex = 1;
            this.COM_REFUND_STATUS.UseSelectable = true;
            // 
            // TXT_TOTAL_SLIPSEQ
            // 
            this.TXT_TOTAL_SLIPSEQ.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Image = null;
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Location = new System.Drawing.Point(317, 1);
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Name = "";
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Size = new System.Drawing.Size(35, 35);
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_TOTAL_SLIPSEQ.CustomButton.TabIndex = 1;
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_TOTAL_SLIPSEQ.CustomButton.UseSelectable = true;
            this.TXT_TOTAL_SLIPSEQ.CustomButton.Visible = false;
            this.TXT_TOTAL_SLIPSEQ.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_TOTAL_SLIPSEQ.Lines = new string[0];
            this.TXT_TOTAL_SLIPSEQ.Location = new System.Drawing.Point(1023, 49);
            this.TXT_TOTAL_SLIPSEQ.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_TOTAL_SLIPSEQ.MaxLength = 32767;
            this.TXT_TOTAL_SLIPSEQ.Name = "TXT_TOTAL_SLIPSEQ";
            this.TXT_TOTAL_SLIPSEQ.PasswordChar = '\0';
            this.TXT_TOTAL_SLIPSEQ.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_TOTAL_SLIPSEQ.SelectedText = "";
            this.TXT_TOTAL_SLIPSEQ.SelectionLength = 0;
            this.TXT_TOTAL_SLIPSEQ.SelectionStart = 0;
            this.TXT_TOTAL_SLIPSEQ.ShortcutsEnabled = true;
            this.TXT_TOTAL_SLIPSEQ.Size = new System.Drawing.Size(353, 37);
            this.TXT_TOTAL_SLIPSEQ.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_TOTAL_SLIPSEQ.TabIndex = 137;
            this.TXT_TOTAL_SLIPSEQ.UseSelectable = true;
            this.TXT_TOTAL_SLIPSEQ.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_TOTAL_SLIPSEQ.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // LBL_TOTAL_SLIPSEQ
            // 
            this.LBL_TOTAL_SLIPSEQ.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_TOTAL_SLIPSEQ.AutoSize = true;
            this.LBL_TOTAL_SLIPSEQ.Location = new System.Drawing.Point(856, 57);
            this.LBL_TOTAL_SLIPSEQ.Name = "LBL_TOTAL_SLIPSEQ";
            this.LBL_TOTAL_SLIPSEQ.Size = new System.Drawing.Size(105, 20);
            this.LBL_TOTAL_SLIPSEQ.TabIndex = 135;
            this.LBL_TOTAL_SLIPSEQ.Text = "TOTAL_SLIPSEQ";
            // 
            // TXT_REFUND_TODATE
            // 
            this.TXT_REFUND_TODATE.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TXT_REFUND_TODATE.CalendarFont = new System.Drawing.Font("Gulim", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.TXT_REFUND_TODATE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TXT_REFUND_TODATE.Location = new System.Drawing.Point(596, 4);
            this.TXT_REFUND_TODATE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_REFUND_TODATE.MinimumSize = new System.Drawing.Size(0, 30);
            this.TXT_REFUND_TODATE.Name = "TXT_REFUND_TODATE";
            this.TXT_REFUND_TODATE.Size = new System.Drawing.Size(233, 30);
            this.TXT_REFUND_TODATE.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_REFUND_TODATE.TabIndex = 134;
            // 
            // LBL_REFUND_STATUS
            // 
            this.LBL_REFUND_STATUS.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_REFUND_STATUS.AutoSize = true;
            this.LBL_REFUND_STATUS.Location = new System.Drawing.Point(856, 12);
            this.LBL_REFUND_STATUS.Name = "LBL_REFUND_STATUS";
            this.LBL_REFUND_STATUS.Size = new System.Drawing.Size(93, 20);
            this.LBL_REFUND_STATUS.TabIndex = 112;
            this.LBL_REFUND_STATUS.Text = "Refund Status";
            // 
            // TIL_1
            // 
            this.TIL_1.ActiveControl = null;
            this.TIL_1.Enabled = false;
            this.TIL_1.Location = new System.Drawing.Point(0, 160);
            this.TIL_1.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.TIL_1.Name = "TIL_1";
            this.TIL_1.Size = new System.Drawing.Size(1411, 2);
            this.TIL_1.Style = MetroFramework.MetroColorStyle.Orange;
            this.TIL_1.TabIndex = 105;
            this.TIL_1.TabStop = false;
            this.TIL_1.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Regular;
            this.TIL_1.UseSelectable = true;
            // 
            // BTN_NEXT
            // 
            this.BTN_NEXT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTN_NEXT.Location = new System.Drawing.Point(496, 4);
            this.BTN_NEXT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_NEXT.Name = "BTN_NEXT";
            this.BTN_NEXT.Size = new System.Drawing.Size(86, 36);
            this.BTN_NEXT.TabIndex = 108;
            this.BTN_NEXT.Text = "NEXT";
            this.BTN_NEXT.UseSelectable = true;
            this.BTN_NEXT.Visible = false;
            // 
            // BTN_PREV
            // 
            this.BTN_PREV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BTN_PREV.Location = new System.Drawing.Point(403, 4);
            this.BTN_PREV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_PREV.Name = "BTN_PREV";
            this.BTN_PREV.Size = new System.Drawing.Size(86, 36);
            this.BTN_PREV.TabIndex = 109;
            this.BTN_PREV.Text = "PREV";
            this.BTN_PREV.UseSelectable = true;
            this.BTN_PREV.Visible = false;
            // 
            // BTN_SEARCH
            // 
            this.BTN_SEARCH.Location = new System.Drawing.Point(1040, 18);
            this.BTN_SEARCH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_SEARCH.Name = "BTN_SEARCH";
            this.BTN_SEARCH.Size = new System.Drawing.Size(171, 36);
            this.BTN_SEARCH.TabIndex = 0;
            this.BTN_SEARCH.Text = "Search";
            this.BTN_SEARCH.UseSelectable = true;
            this.BTN_SEARCH.Click += new System.EventHandler(this.BTN_SEARCH_Click);
            // 
            // LBL_TOTAL_CNT
            // 
            this.LBL_TOTAL_CNT.AutoSize = true;
            this.LBL_TOTAL_CNT.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.LBL_TOTAL_CNT.Location = new System.Drawing.Point(302, 18);
            this.LBL_TOTAL_CNT.Name = "LBL_TOTAL_CNT";
            this.LBL_TOTAL_CNT.Size = new System.Drawing.Size(120, 20);
            this.LBL_TOTAL_CNT.TabIndex = 106;
            this.LBL_TOTAL_CNT.Text = "Total Count : 100";
            this.LBL_TOTAL_CNT.UseCustomBackColor = true;
            this.LBL_TOTAL_CNT.Visible = false;
            // 
            // metroLabel3
            // 
            this.metroLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(15, 0);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(126, 45);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.White;
            this.metroLabel3.TabIndex = 132;
            this.metroLabel3.Text = "SEARCH";
            this.metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseStyleColors = true;
            // 
            // LAY_PAGE
            // 
            this.LAY_PAGE.ColumnCount = 2;
            this.LAY_PAGE.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.LAY_PAGE.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.LAY_PAGE.Controls.Add(this.cmbbox_Page, 1, 0);
            this.LAY_PAGE.Controls.Add(this.lbl_Page, 0, 0);
            this.LAY_PAGE.Location = new System.Drawing.Point(14, 162);
            this.LAY_PAGE.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LAY_PAGE.Name = "LAY_PAGE";
            this.LAY_PAGE.RowCount = 1;
            this.LAY_PAGE.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.LAY_PAGE.Size = new System.Drawing.Size(1398, 41);
            this.LAY_PAGE.TabIndex = 133;
            // 
            // cmbbox_Page
            // 
            this.cmbbox_Page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbbox_Page.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbox_Page.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbox_Page.FormattingEnabled = true;
            this.cmbbox_Page.Location = new System.Drawing.Point(72, 10);
            this.cmbbox_Page.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbbox_Page.Name = "cmbbox_Page";
            this.cmbbox_Page.Size = new System.Drawing.Size(89, 29);
            this.cmbbox_Page.TabIndex = 111;
            this.cmbbox_Page.SelectedIndexChanged += new System.EventHandler(this.cmbbox_Page_SelectedIndexChanged);
            // 
            // lbl_Page
            // 
            this.lbl_Page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Page.Location = new System.Drawing.Point(3, 0);
            this.lbl_Page.Name = "lbl_Page";
            this.lbl_Page.Size = new System.Drawing.Size(63, 41);
            this.lbl_Page.TabIndex = 112;
            this.lbl_Page.Text = "Page :";
            this.lbl_Page.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TrxnPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.LAY_PAGE);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.BTN_CANCEL);
            this.Controls.Add(this.BTN_PREV);
            this.Controls.Add(this.BTN_NEXT);
            this.Controls.Add(this.LBL_TOTAL_CNT);
            this.Controls.Add(this.TIL_1);
            this.Controls.Add(this.LAY_SEARCH);
            this.Controls.Add(this.BTN_SEARCH);
            this.Controls.Add(this.GRD_SLIP);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "TrxnPanel";
            this.Size = new System.Drawing.Size(1411, 882);
            this.Load += new System.EventHandler(this.TrxnPanel_Load);
            this.SizeChanged += new System.EventHandler(this.TrxnPanel_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.GRD_SLIP)).EndInit();
            this.LAY_SEARCH.ResumeLayout(false);
            this.LAY_SEARCH.PerformLayout();
            this.LAY_PAGE.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void BTN_QR_SCAN_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private MetroFramework.Controls.MetroLabel LBL_REFUND_DATE;
        private MetroFramework.Controls.MetroDateTime TXT_REFUND_FROMDATE;
        private MetroFramework.Controls.MetroButton BTN_CANCEL;
        private MetroFramework.Controls.MetroTextBox TXT_SLIP_NO;
        private MetroFramework.Controls.MetroLabel LBL_SLIP_NO;
        private MetroFramework.Controls.MetroGrid GRD_SLIP;
        private System.Windows.Forms.TableLayoutPanel LAY_SEARCH;
        private MetroFramework.Controls.MetroTile TIL_1;
        private MetroFramework.Controls.MetroButton BTN_NEXT;
        private MetroFramework.Controls.MetroButton BTN_PREV;
        private MetroFramework.Controls.MetroComboBox COM_REFUND_STATUS;
        private MetroFramework.Controls.MetroLabel LBL_REFUND_STATUS;
        private MetroFramework.Controls.MetroButton BTN_SEARCH;
        private MetroFramework.Controls.MetroLabel LBL_TOTAL_CNT;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroComboBox COM_DATE_COND;
        private MetroFramework.Controls.MetroDateTime TXT_REFUND_TODATE;
        private System.Windows.Forms.TableLayoutPanel LAY_PAGE;
        private MetroFramework.Controls.MetroLabel lbl_Page;
        private System.Windows.Forms.ComboBox cmbbox_Page;
        private MetroFramework.Controls.MetroTextBox TXT_TOTAL_SLIPSEQ;
        private MetroFramework.Controls.MetroLabel LBL_TOTAL_SLIPSEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewButtonColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
    }
}
