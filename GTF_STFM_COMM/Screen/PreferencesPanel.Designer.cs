namespace GTF_STFM_COMM.Screen
{
    partial class PreferencesPanel
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
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.TXT_TML_ID = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.COM_PASS_SCAN = new MetroFramework.Controls.MetroComboBox();
            this.COM_PRINTER = new MetroFramework.Controls.MetroComboBox();
            this.COM_SLIP_TYPE = new MetroFramework.Controls.MetroComboBox();
            this.BTN_HELP = new MetroFramework.Controls.MetroButton();
            this.BTN_DOWNLOAD = new MetroFramework.Controls.MetroButton();
            this.BTN_PRINT_TEST = new MetroFramework.Controls.MetroButton();
            this.BTN_SCAN_TEST = new MetroFramework.Controls.MetroButton();
            this.BTN_TID_CONFIRM = new MetroFramework.Controls.MetroButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.COM_PRINT_SETTING = new MetroFramework.Controls.MetroComboBox();
            this.LBL_PRINT_SETTING = new MetroFramework.Controls.MetroLabel();
            this.COM_PRINT_TYPE = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.BTN_SIGNPAD_CHECK = new MetroFramework.Controls.MetroButton();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.COM_RCT_ADD = new MetroFramework.Controls.MetroComboBox();
            this.COM_SIGNPAD_USE = new MetroFramework.Controls.MetroComboBox();
            this.BTN_A4PRINT_TEST = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.COM_PC_NO = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.COM_OPOS = new MetroFramework.Controls.MetroComboBox();
            this.BTN_OPOS_TEST = new MetroFramework.Controls.MetroButton();
            this.BTN_SAVE = new MetroFramework.Controls.MetroButton();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel9
            // 
            this.metroLabel9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(3, 9);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(78, 20);
            this.metroLabel9.TabIndex = 58;
            this.metroLabel9.Text = "Terminal ID";
            this.metroLabel9.Click += new System.EventHandler(this.metroLabel9_Click);
            // 
            // TXT_TML_ID
            // 
            this.TXT_TML_ID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.TXT_TML_ID.CustomButton.Image = null;
            this.TXT_TML_ID.CustomButton.Location = new System.Drawing.Point(202, 2);
            this.TXT_TML_ID.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_TML_ID.CustomButton.Name = "";
            this.TXT_TML_ID.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.TXT_TML_ID.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_TML_ID.CustomButton.TabIndex = 1;
            this.TXT_TML_ID.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_TML_ID.CustomButton.UseSelectable = true;
            this.TXT_TML_ID.CustomButton.Visible = false;
            this.TXT_TML_ID.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_TML_ID.Lines = new string[0];
            this.TXT_TML_ID.Location = new System.Drawing.Point(180, 4);
            this.TXT_TML_ID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_TML_ID.MaxLength = 5;
            this.TXT_TML_ID.Name = "TXT_TML_ID";
            this.TXT_TML_ID.PasswordChar = '\0';
            this.TXT_TML_ID.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_TML_ID.SelectedText = "";
            this.TXT_TML_ID.SelectionLength = 0;
            this.TXT_TML_ID.SelectionStart = 0;
            this.TXT_TML_ID.ShortcutsEnabled = true;
            this.TXT_TML_ID.Size = new System.Drawing.Size(230, 30);
            this.TXT_TML_ID.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_TML_ID.TabIndex = 0;
            this.TXT_TML_ID.UseSelectable = true;
            this.TXT_TML_ID.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_TML_ID.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(3, 237);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(64, 20);
            this.metroLabel6.TabIndex = 70;
            this.metroLabel6.Text = "Slip Type";
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(3, 85);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(50, 20);
            this.metroLabel7.TabIndex = 72;
            this.metroLabel7.Text = "Printer";
            // 
            // metroLabel8
            // 
            this.metroLabel8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(3, 47);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(115, 20);
            this.metroLabel8.TabIndex = 74;
            this.metroLabel8.Text = "Passport Scanner";
            // 
            // COM_PASS_SCAN
            // 
            this.COM_PASS_SCAN.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_PASS_SCAN.FormattingEnabled = true;
            this.COM_PASS_SCAN.ItemHeight = 24;
            this.COM_PASS_SCAN.Items.AddRange(new object[] {
            "GTF-PS01(GTF)",
            "WISESCAN420",
            "COMBOSMART(DAWIN)",
            "NP-1000(OKPOS)"});
            this.COM_PASS_SCAN.Location = new System.Drawing.Point(180, 42);
            this.COM_PASS_SCAN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PASS_SCAN.Name = "COM_PASS_SCAN";
            this.COM_PASS_SCAN.Size = new System.Drawing.Size(230, 30);
            this.COM_PASS_SCAN.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PASS_SCAN.TabIndex = 1;
            this.COM_PASS_SCAN.UseSelectable = true;
            // 
            // COM_PRINTER
            // 
            this.COM_PRINTER.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_PRINTER.FormattingEnabled = true;
            this.COM_PRINTER.ItemHeight = 24;
            this.COM_PRINTER.Location = new System.Drawing.Point(180, 80);
            this.COM_PRINTER.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PRINTER.Name = "COM_PRINTER";
            this.COM_PRINTER.Size = new System.Drawing.Size(230, 30);
            this.COM_PRINTER.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PRINTER.TabIndex = 3;
            this.COM_PRINTER.UseSelectable = true;
            // 
            // COM_SLIP_TYPE
            // 
            this.COM_SLIP_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_SLIP_TYPE.FormattingEnabled = true;
            this.COM_SLIP_TYPE.ItemHeight = 24;
            this.COM_SLIP_TYPE.Items.AddRange(new object[] {
            "80mm",
            "A4"});
            this.COM_SLIP_TYPE.Location = new System.Drawing.Point(180, 232);
            this.COM_SLIP_TYPE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_SLIP_TYPE.Name = "COM_SLIP_TYPE";
            this.COM_SLIP_TYPE.Size = new System.Drawing.Size(230, 30);
            this.COM_SLIP_TYPE.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_SLIP_TYPE.TabIndex = 5;
            this.COM_SLIP_TYPE.UseSelectable = true;
            // 
            // BTN_HELP
            // 
            this.BTN_HELP.Location = new System.Drawing.Point(741, 149);
            this.BTN_HELP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_HELP.Name = "BTN_HELP";
            this.BTN_HELP.Size = new System.Drawing.Size(143, 36);
            this.BTN_HELP.TabIndex = 3;
            this.BTN_HELP.Text = "Help";
            this.BTN_HELP.UseSelectable = true;
            this.BTN_HELP.Visible = false;
            // 
            // BTN_DOWNLOAD
            // 
            this.BTN_DOWNLOAD.Location = new System.Drawing.Point(741, 105);
            this.BTN_DOWNLOAD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_DOWNLOAD.Name = "BTN_DOWNLOAD";
            this.BTN_DOWNLOAD.Size = new System.Drawing.Size(143, 36);
            this.BTN_DOWNLOAD.TabIndex = 2;
            this.BTN_DOWNLOAD.Text = "Download";
            this.BTN_DOWNLOAD.UseSelectable = true;
            this.BTN_DOWNLOAD.Visible = false;
            // 
            // BTN_PRINT_TEST
            // 
            this.BTN_PRINT_TEST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_PRINT_TEST.Location = new System.Drawing.Point(416, 80);
            this.BTN_PRINT_TEST.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_PRINT_TEST.Name = "BTN_PRINT_TEST";
            this.BTN_PRINT_TEST.Size = new System.Drawing.Size(172, 30);
            this.BTN_PRINT_TEST.TabIndex = 4;
            this.BTN_PRINT_TEST.Text = "Print Test";
            this.BTN_PRINT_TEST.UseSelectable = true;
            this.BTN_PRINT_TEST.Click += new System.EventHandler(this.BTN_PRINT_TEST_Click);
            // 
            // BTN_SCAN_TEST
            // 
            this.BTN_SCAN_TEST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_SCAN_TEST.Location = new System.Drawing.Point(416, 42);
            this.BTN_SCAN_TEST.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_SCAN_TEST.Name = "BTN_SCAN_TEST";
            this.BTN_SCAN_TEST.Size = new System.Drawing.Size(172, 30);
            this.BTN_SCAN_TEST.TabIndex = 2;
            this.BTN_SCAN_TEST.Text = "Scan Test";
            this.BTN_SCAN_TEST.UseSelectable = true;
            this.BTN_SCAN_TEST.Click += new System.EventHandler(this.BTN_SCAN_TEST_Click);
            // 
            // BTN_TID_CONFIRM
            // 
            this.BTN_TID_CONFIRM.Location = new System.Drawing.Point(400, 615);
            this.BTN_TID_CONFIRM.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_TID_CONFIRM.Name = "BTN_TID_CONFIRM";
            this.BTN_TID_CONFIRM.Size = new System.Drawing.Size(171, 36);
            this.BTN_TID_CONFIRM.TabIndex = 1;
            this.BTN_TID_CONFIRM.Text = "ID Confirm";
            this.BTN_TID_CONFIRM.UseSelectable = true;
            this.BTN_TID_CONFIRM.Visible = false;
            this.BTN_TID_CONFIRM.Click += new System.EventHandler(this.BTN_TID_CONFIRM_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.COM_PRINT_SETTING, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.LBL_PRINT_SETTING, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel9, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.COM_PRINTER, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.COM_PASS_SCAN, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TXT_TML_ID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BTN_PRINT_TEST, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.BTN_SCAN_TEST, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.COM_SLIP_TYPE, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.COM_PRINT_TYPE, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.BTN_SIGNPAD_CHECK, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel10, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.COM_RCT_ADD, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.COM_SIGNPAD_USE, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.BTN_A4PRINT_TEST, 2, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(16, 61);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.49918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50167F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.50328F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(591, 311);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // COM_PRINT_SETTING
            // 
            this.COM_PRINT_SETTING.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_PRINT_SETTING.FormattingEnabled = true;
            this.COM_PRINT_SETTING.ItemHeight = 24;
            this.COM_PRINT_SETTING.Items.AddRange(new object[] {
            "Print ALL",
            "Except Digitize"});
            this.COM_PRINT_SETTING.Location = new System.Drawing.Point(180, 270);
            this.COM_PRINT_SETTING.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PRINT_SETTING.Name = "COM_PRINT_SETTING";
            this.COM_PRINT_SETTING.Size = new System.Drawing.Size(230, 30);
            this.COM_PRINT_SETTING.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PRINT_SETTING.TabIndex = 137;
            this.COM_PRINT_SETTING.UseSelectable = true;
            // 
            // LBL_PRINT_SETTING
            // 
            this.LBL_PRINT_SETTING.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.LBL_PRINT_SETTING.AutoSize = true;
            this.LBL_PRINT_SETTING.Location = new System.Drawing.Point(3, 278);
            this.LBL_PRINT_SETTING.Name = "LBL_PRINT_SETTING";
            this.LBL_PRINT_SETTING.Size = new System.Drawing.Size(83, 20);
            this.LBL_PRINT_SETTING.TabIndex = 136;
            this.LBL_PRINT_SETTING.Text = "Print Setting";
            // 
            // COM_PRINT_TYPE
            // 
            this.COM_PRINT_TYPE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_PRINT_TYPE.FormattingEnabled = true;
            this.COM_PRINT_TYPE.ItemHeight = 24;
            this.COM_PRINT_TYPE.Items.AddRange(new object[] {
            "ALL",
            "購入記録表"});
            this.COM_PRINT_TYPE.Location = new System.Drawing.Point(180, 194);
            this.COM_PRINT_TYPE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PRINT_TYPE.Name = "COM_PRINT_TYPE";
            this.COM_PRINT_TYPE.Size = new System.Drawing.Size(230, 30);
            this.COM_PRINT_TYPE.TabIndex = 132;
            this.COM_PRINT_TYPE.UseSelectable = true;
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(3, 199);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(84, 20);
            this.metroLabel5.TabIndex = 78;
            this.metroLabel5.Text = "Print Choice";
            // 
            // BTN_SIGNPAD_CHECK
            // 
            this.BTN_SIGNPAD_CHECK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_SIGNPAD_CHECK.Location = new System.Drawing.Point(416, 156);
            this.BTN_SIGNPAD_CHECK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_SIGNPAD_CHECK.Name = "BTN_SIGNPAD_CHECK";
            this.BTN_SIGNPAD_CHECK.Size = new System.Drawing.Size(172, 29);
            this.BTN_SIGNPAD_CHECK.TabIndex = 77;
            this.BTN_SIGNPAD_CHECK.Text = "Signpad Check";
            this.BTN_SIGNPAD_CHECK.UseSelectable = true;
            this.BTN_SIGNPAD_CHECK.Click += new System.EventHandler(this.BTN_SIGNPAD_CHECK_Click);
            // 
            // metroLabel10
            // 
            this.metroLabel10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(3, 123);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(84, 20);
            this.metroLabel10.TabIndex = 133;
            this.metroLabel10.Text = "Receipt Add";
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(3, 161);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(86, 20);
            this.metroLabel4.TabIndex = 75;
            this.metroLabel4.Text = "Signpad Use";
            // 
            // COM_RCT_ADD
            // 
            this.COM_RCT_ADD.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_RCT_ADD.FormattingEnabled = true;
            this.COM_RCT_ADD.ItemHeight = 24;
            this.COM_RCT_ADD.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.COM_RCT_ADD.Location = new System.Drawing.Point(180, 118);
            this.COM_RCT_ADD.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_RCT_ADD.Name = "COM_RCT_ADD";
            this.COM_RCT_ADD.Size = new System.Drawing.Size(230, 30);
            this.COM_RCT_ADD.TabIndex = 134;
            this.COM_RCT_ADD.UseSelectable = true;
            // 
            // COM_SIGNPAD_USE
            // 
            this.COM_SIGNPAD_USE.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.COM_SIGNPAD_USE.FormattingEnabled = true;
            this.COM_SIGNPAD_USE.ItemHeight = 24;
            this.COM_SIGNPAD_USE.Items.AddRange(new object[] {
            "NO",
            "YES"});
            this.COM_SIGNPAD_USE.Location = new System.Drawing.Point(180, 156);
            this.COM_SIGNPAD_USE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_SIGNPAD_USE.Name = "COM_SIGNPAD_USE";
            this.COM_SIGNPAD_USE.Size = new System.Drawing.Size(230, 30);
            this.COM_SIGNPAD_USE.TabIndex = 76;
            this.COM_SIGNPAD_USE.UseSelectable = true;
            this.COM_SIGNPAD_USE.SelectedIndexChanged += new System.EventHandler(this.COM_SIGNPAD_USE_SelectedIndexChanged);
            // 
            // BTN_A4PRINT_TEST
            // 
            this.BTN_A4PRINT_TEST.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN_A4PRINT_TEST.Location = new System.Drawing.Point(416, 232);
            this.BTN_A4PRINT_TEST.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_A4PRINT_TEST.Name = "BTN_A4PRINT_TEST";
            this.BTN_A4PRINT_TEST.Size = new System.Drawing.Size(172, 30);
            this.BTN_A4PRINT_TEST.TabIndex = 135;
            this.BTN_A4PRINT_TEST.Text = "A4 Test";
            this.BTN_A4PRINT_TEST.UseSelectable = true;
            this.BTN_A4PRINT_TEST.Visible = false;
            this.BTN_A4PRINT_TEST.Click += new System.EventHandler(this.BTN_A4PRINT_TEST_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(64, 650);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(52, 20);
            this.metroLabel2.TabIndex = 76;
            this.metroLabel2.Text = "PC NO";
            this.metroLabel2.Visible = false;
            // 
            // COM_PC_NO
            // 
            this.COM_PC_NO.FormattingEnabled = true;
            this.COM_PC_NO.ItemHeight = 24;
            this.COM_PC_NO.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.COM_PC_NO.Location = new System.Drawing.Point(165, 615);
            this.COM_PC_NO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PC_NO.Name = "COM_PC_NO";
            this.COM_PC_NO.Size = new System.Drawing.Size(207, 30);
            this.COM_PC_NO.TabIndex = 75;
            this.COM_PC_NO.UseSelectable = true;
            this.COM_PC_NO.Visible = false;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(51, 726);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(95, 20);
            this.metroLabel1.TabIndex = 88;
            this.metroLabel1.Text = "(OPOS)Printer";
            this.metroLabel1.Visible = false;
            // 
            // COM_OPOS
            // 
            this.COM_OPOS.FormattingEnabled = true;
            this.COM_OPOS.ItemHeight = 24;
            this.COM_OPOS.Items.AddRange(new object[] {
            "SRP-350III",
            "SRP-350II  Plus",
            "SRP-350III Plus"});
            this.COM_OPOS.Location = new System.Drawing.Point(165, 691);
            this.COM_OPOS.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_OPOS.Name = "COM_OPOS";
            this.COM_OPOS.Size = new System.Drawing.Size(207, 30);
            this.COM_OPOS.TabIndex = 88;
            this.COM_OPOS.UseSelectable = true;
            this.COM_OPOS.Visible = false;
            // 
            // BTN_OPOS_TEST
            // 
            this.BTN_OPOS_TEST.Location = new System.Drawing.Point(400, 694);
            this.BTN_OPOS_TEST.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_OPOS_TEST.Name = "BTN_OPOS_TEST";
            this.BTN_OPOS_TEST.Size = new System.Drawing.Size(171, 36);
            this.BTN_OPOS_TEST.TabIndex = 6;
            this.BTN_OPOS_TEST.Text = "OPOS Print Test";
            this.BTN_OPOS_TEST.UseSelectable = true;
            this.BTN_OPOS_TEST.Visible = false;
            this.BTN_OPOS_TEST.Click += new System.EventHandler(this.BTN_OPOS_TEST_Click);
            // 
            // BTN_SAVE
            // 
            this.BTN_SAVE.Location = new System.Drawing.Point(741, 61);
            this.BTN_SAVE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_SAVE.Name = "BTN_SAVE";
            this.BTN_SAVE.Size = new System.Drawing.Size(143, 36);
            this.BTN_SAVE.TabIndex = 1;
            this.BTN_SAVE.Text = "Save";
            this.BTN_SAVE.UseSelectable = true;
            this.BTN_SAVE.Click += new System.EventHandler(this.BTN_SAVE_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(119)))), ((int)(((byte)(53)))));
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel3.Location = new System.Drawing.Point(15, 0);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(126, 45);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.White;
            this.metroLabel3.TabIndex = 131;
            this.metroLabel3.Text = "CONFIG";
            this.metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseStyleColors = true;
            // 
            // PreferencesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.COM_OPOS);
            this.Controls.Add(this.COM_PC_NO);
            this.Controls.Add(this.BTN_TID_CONFIRM);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.BTN_SAVE);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.BTN_DOWNLOAD);
            this.Controls.Add(this.BTN_HELP);
            this.Controls.Add(this.BTN_OPOS_TEST);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PreferencesPanel";
            this.Size = new System.Drawing.Size(1411, 882);
            this.Load += new System.EventHandler(this.PreferencesPanel_Load);
            this.SizeChanged += new System.EventHandler(this.PreferencesPanel_SizeChanged);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroTextBox TXT_TML_ID;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroComboBox COM_PASS_SCAN;
        private MetroFramework.Controls.MetroComboBox COM_PRINTER;
        private MetroFramework.Controls.MetroComboBox COM_SLIP_TYPE;
        private MetroFramework.Controls.MetroButton BTN_HELP;
        private MetroFramework.Controls.MetroButton BTN_DOWNLOAD;
        private MetroFramework.Controls.MetroButton BTN_PRINT_TEST;
        private MetroFramework.Controls.MetroButton BTN_SCAN_TEST;
        private MetroFramework.Controls.MetroButton BTN_TID_CONFIRM;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MetroFramework.Controls.MetroButton BTN_SAVE;
        private MetroFramework.Controls.MetroButton BTN_OPOS_TEST;
        private MetroFramework.Controls.MetroComboBox COM_OPOS;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroComboBox COM_PC_NO;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox COM_SIGNPAD_USE;
        private MetroFramework.Controls.MetroButton BTN_SIGNPAD_CHECK;
        private MetroFramework.Controls.MetroComboBox COM_PRINT_TYPE;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox COM_RCT_ADD;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroButton BTN_A4PRINT_TEST;
        private MetroFramework.Controls.MetroComboBox COM_PRINT_SETTING;
        private MetroFramework.Controls.MetroLabel LBL_PRINT_SETTING;
    }
}
