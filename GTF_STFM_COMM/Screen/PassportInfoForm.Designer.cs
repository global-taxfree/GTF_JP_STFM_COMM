namespace GTF_STFM_COMM.Screen
{
    partial class PassportInfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BTN_OK = new MetroFramework.Controls.MetroButton();
            this.BTN_CLOSE = new MetroFramework.Controls.MetroButton();
            this.TXT_PASSPORT_NO = new MetroFramework.Controls.MetroTextBox();
            this.TXT_PASSPORT_NAME = new MetroFramework.Controls.MetroTextBox();
            this.LBL_PASSPORT_NAME = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_NO = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_BIRTH = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_SEX = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_RES = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_TYPE = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_NAT = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_LAND = new MetroFramework.Controls.MetroLabel();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.LBL_PASSPORT_EXP = new MetroFramework.Controls.MetroLabel();
            this.COM_PASSPORT_NAT = new MetroFramework.Controls.MetroComboBox();
            this.COM_PASSPORT_TYPE = new MetroFramework.Controls.MetroComboBox();
            this.COM_PASSPORT_RES = new MetroFramework.Controls.MetroComboBox();
            this.COM_PASSPORT_SEX = new MetroFramework.Controls.MetroComboBox();
            this.TXT_PASSPORT_BIRTH = new System.Windows.Forms.DateTimePicker();
            this.TXT_PASSPORT_EXP = new System.Windows.Forms.DateTimePicker();
            this.TXT_DATE_LAND = new System.Windows.Forms.DateTimePicker();
            this.LBL_PERMIT_NO = new MetroFramework.Controls.MetroLabel();
            this.TXT_PERMIT_NO = new MetroFramework.Controls.MetroTextBox();
            this.LBL_NOTE = new MetroFramework.Controls.MetroLabel();
            this.TXT_NOTE = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // BTN_OK
            // 
            this.BTN_OK.Location = new System.Drawing.Point(247, 380);
            this.BTN_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_OK.Name = "BTN_OK";
            this.BTN_OK.Size = new System.Drawing.Size(158, 36);
            this.BTN_OK.TabIndex = 9;
            this.BTN_OK.Text = "OK";
            this.BTN_OK.UseSelectable = true;
            this.BTN_OK.Click += new System.EventHandler(this.BTN_OK_Click);
            // 
            // BTN_CLOSE
            // 
            this.BTN_CLOSE.Location = new System.Drawing.Point(462, 380);
            this.BTN_CLOSE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BTN_CLOSE.Name = "BTN_CLOSE";
            this.BTN_CLOSE.Size = new System.Drawing.Size(158, 36);
            this.BTN_CLOSE.TabIndex = 10;
            this.BTN_CLOSE.Text = "CLOSE";
            this.BTN_CLOSE.UseSelectable = true;
            this.BTN_CLOSE.Click += new System.EventHandler(this.BTN_CLOSE_Click);
            // 
            // TXT_PASSPORT_NO
            // 
            this.TXT_PASSPORT_NO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.TXT_PASSPORT_NO.CustomButton.Image = null;
            this.TXT_PASSPORT_NO.CustomButton.Location = new System.Drawing.Point(195, 2);
            this.TXT_PASSPORT_NO.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PASSPORT_NO.CustomButton.Name = "";
            this.TXT_PASSPORT_NO.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.TXT_PASSPORT_NO.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_PASSPORT_NO.CustomButton.TabIndex = 1;
            this.TXT_PASSPORT_NO.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_PASSPORT_NO.CustomButton.UseSelectable = true;
            this.TXT_PASSPORT_NO.CustomButton.Visible = false;
            this.TXT_PASSPORT_NO.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_PASSPORT_NO.Lines = new string[0];
            this.TXT_PASSPORT_NO.Location = new System.Drawing.Point(606, 101);
            this.TXT_PASSPORT_NO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PASSPORT_NO.MaxLength = 20;
            this.TXT_PASSPORT_NO.Name = "TXT_PASSPORT_NO";
            this.TXT_PASSPORT_NO.PasswordChar = '\0';
            this.TXT_PASSPORT_NO.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_PASSPORT_NO.SelectedText = "";
            this.TXT_PASSPORT_NO.SelectionLength = 0;
            this.TXT_PASSPORT_NO.SelectionStart = 0;
            this.TXT_PASSPORT_NO.ShortcutsEnabled = true;
            this.TXT_PASSPORT_NO.Size = new System.Drawing.Size(229, 36);
            this.TXT_PASSPORT_NO.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_PASSPORT_NO.TabIndex = 1;
            this.TXT_PASSPORT_NO.UseSelectable = true;
            this.TXT_PASSPORT_NO.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_PASSPORT_NO.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.TXT_PASSPORT_NO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_PASSPORT_NO_KeyPress);
            // 
            // TXT_PASSPORT_NAME
            // 
            this.TXT_PASSPORT_NAME.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.TXT_PASSPORT_NAME.CustomButton.Image = null;
            this.TXT_PASSPORT_NAME.CustomButton.Location = new System.Drawing.Point(251, 2);
            this.TXT_PASSPORT_NAME.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PASSPORT_NAME.CustomButton.Name = "";
            this.TXT_PASSPORT_NAME.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.TXT_PASSPORT_NAME.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_PASSPORT_NAME.CustomButton.TabIndex = 1;
            this.TXT_PASSPORT_NAME.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_PASSPORT_NAME.CustomButton.UseSelectable = true;
            this.TXT_PASSPORT_NAME.CustomButton.Visible = false;
            this.TXT_PASSPORT_NAME.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_PASSPORT_NAME.Lines = new string[0];
            this.TXT_PASSPORT_NAME.Location = new System.Drawing.Point(157, 101);
            this.TXT_PASSPORT_NAME.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PASSPORT_NAME.MaxLength = 32767;
            this.TXT_PASSPORT_NAME.Name = "TXT_PASSPORT_NAME";
            this.TXT_PASSPORT_NAME.PasswordChar = '\0';
            this.TXT_PASSPORT_NAME.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_PASSPORT_NAME.SelectedText = "";
            this.TXT_PASSPORT_NAME.SelectionLength = 0;
            this.TXT_PASSPORT_NAME.SelectionStart = 0;
            this.TXT_PASSPORT_NAME.ShortcutsEnabled = true;
            this.TXT_PASSPORT_NAME.Size = new System.Drawing.Size(285, 36);
            this.TXT_PASSPORT_NAME.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_PASSPORT_NAME.TabIndex = 0;
            this.TXT_PASSPORT_NAME.UseSelectable = true;
            this.TXT_PASSPORT_NAME.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_PASSPORT_NAME.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.TXT_PASSPORT_NAME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_PASSPORT_NAME_KeyPress);
            // 
            // LBL_PASSPORT_NAME
            // 
            this.LBL_PASSPORT_NAME.AutoSize = true;
            this.LBL_PASSPORT_NAME.Location = new System.Drawing.Point(14, 114);
            this.LBL_PASSPORT_NAME.Name = "LBL_PASSPORT_NAME";
            this.LBL_PASSPORT_NAME.Size = new System.Drawing.Size(103, 20);
            this.LBL_PASSPORT_NAME.TabIndex = 16;
            this.LBL_PASSPORT_NAME.Text = "Passport Name";
            // 
            // LBL_PASSPORT_NO
            // 
            this.LBL_PASSPORT_NO.AutoSize = true;
            this.LBL_PASSPORT_NO.Location = new System.Drawing.Point(471, 114);
            this.LBL_PASSPORT_NO.Name = "LBL_PASSPORT_NO";
            this.LBL_PASSPORT_NO.Size = new System.Drawing.Size(84, 20);
            this.LBL_PASSPORT_NO.TabIndex = 19;
            this.LBL_PASSPORT_NO.Text = "Passport No";
            // 
            // LBL_PASSPORT_BIRTH
            // 
            this.LBL_PASSPORT_BIRTH.AutoSize = true;
            this.LBL_PASSPORT_BIRTH.Location = new System.Drawing.Point(471, 201);
            this.LBL_PASSPORT_BIRTH.Name = "LBL_PASSPORT_BIRTH";
            this.LBL_PASSPORT_BIRTH.Size = new System.Drawing.Size(87, 20);
            this.LBL_PASSPORT_BIRTH.TabIndex = 20;
            this.LBL_PASSPORT_BIRTH.Text = "Date of birth";
            // 
            // LBL_PASSPORT_SEX
            // 
            this.LBL_PASSPORT_SEX.AutoSize = true;
            this.LBL_PASSPORT_SEX.Location = new System.Drawing.Point(471, 158);
            this.LBL_PASSPORT_SEX.Name = "LBL_PASSPORT_SEX";
            this.LBL_PASSPORT_SEX.Size = new System.Drawing.Size(30, 20);
            this.LBL_PASSPORT_SEX.TabIndex = 21;
            this.LBL_PASSPORT_SEX.Text = "Sex";
            // 
            // LBL_PASSPORT_RES
            // 
            this.LBL_PASSPORT_RES.AutoSize = true;
            this.LBL_PASSPORT_RES.Location = new System.Drawing.Point(14, 245);
            this.LBL_PASSPORT_RES.Name = "LBL_PASSPORT_RES";
            this.LBL_PASSPORT_RES.Size = new System.Drawing.Size(126, 20);
            this.LBL_PASSPORT_RES.TabIndex = 22;
            this.LBL_PASSPORT_RES.Text = "Status of residence";
            // 
            // LBL_PASSPORT_TYPE
            // 
            this.LBL_PASSPORT_TYPE.AutoSize = true;
            this.LBL_PASSPORT_TYPE.Location = new System.Drawing.Point(14, 201);
            this.LBL_PASSPORT_TYPE.Name = "LBL_PASSPORT_TYPE";
            this.LBL_PASSPORT_TYPE.Size = new System.Drawing.Size(92, 20);
            this.LBL_PASSPORT_TYPE.TabIndex = 23;
            this.LBL_PASSPORT_TYPE.Text = "Passport type";
            // 
            // LBL_PASSPORT_NAT
            // 
            this.LBL_PASSPORT_NAT.AutoSize = true;
            this.LBL_PASSPORT_NAT.Location = new System.Drawing.Point(14, 158);
            this.LBL_PASSPORT_NAT.Name = "LBL_PASSPORT_NAT";
            this.LBL_PASSPORT_NAT.Size = new System.Drawing.Size(74, 20);
            this.LBL_PASSPORT_NAT.TabIndex = 24;
            this.LBL_PASSPORT_NAT.Text = "Nationality";
            this.LBL_PASSPORT_NAT.Click += new System.EventHandler(this.metroLabel7_Click);
            // 
            // LBL_PASSPORT_LAND
            // 
            this.LBL_PASSPORT_LAND.AutoSize = true;
            this.LBL_PASSPORT_LAND.Location = new System.Drawing.Point(471, 289);
            this.LBL_PASSPORT_LAND.Name = "LBL_PASSPORT_LAND";
            this.LBL_PASSPORT_LAND.Size = new System.Drawing.Size(103, 20);
            this.LBL_PASSPORT_LAND.TabIndex = 25;
            this.LBL_PASSPORT_LAND.Text = "Date of landing";
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.Location = new System.Drawing.Point(471, 245);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(0, 0);
            this.metroLabel9.TabIndex = 26;
            // 
            // LBL_PASSPORT_EXP
            // 
            this.LBL_PASSPORT_EXP.AutoSize = true;
            this.LBL_PASSPORT_EXP.Location = new System.Drawing.Point(471, 245);
            this.LBL_PASSPORT_EXP.Name = "LBL_PASSPORT_EXP";
            this.LBL_PASSPORT_EXP.Size = new System.Drawing.Size(96, 20);
            this.LBL_PASSPORT_EXP.TabIndex = 27;
            this.LBL_PASSPORT_EXP.Text = "Date of expiry";
            // 
            // COM_PASSPORT_NAT
            // 
            this.COM_PASSPORT_NAT.FormattingEnabled = true;
            this.COM_PASSPORT_NAT.IntegralHeight = false;
            this.COM_PASSPORT_NAT.ItemHeight = 24;
            this.COM_PASSPORT_NAT.Items.AddRange(new object[] {
            "KOR",
            "CHN",
            "JPN",
            "TWN",
            "USA",
            "SGP"});
            this.COM_PASSPORT_NAT.Location = new System.Drawing.Point(157, 145);
            this.COM_PASSPORT_NAT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PASSPORT_NAT.Name = "COM_PASSPORT_NAT";
            this.COM_PASSPORT_NAT.Size = new System.Drawing.Size(284, 30);
            this.COM_PASSPORT_NAT.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PASSPORT_NAT.TabIndex = 2;
            this.COM_PASSPORT_NAT.UseSelectable = true;
            // 
            // COM_PASSPORT_TYPE
            // 
            this.COM_PASSPORT_TYPE.FormattingEnabled = true;
            this.COM_PASSPORT_TYPE.ItemHeight = 24;
            this.COM_PASSPORT_TYPE.Items.AddRange(new object[] {
            "PASSPORT",
            "2",
            "3",
            "4"});
            this.COM_PASSPORT_TYPE.Location = new System.Drawing.Point(157, 189);
            this.COM_PASSPORT_TYPE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PASSPORT_TYPE.Name = "COM_PASSPORT_TYPE";
            this.COM_PASSPORT_TYPE.Size = new System.Drawing.Size(284, 30);
            this.COM_PASSPORT_TYPE.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PASSPORT_TYPE.TabIndex = 4;
            this.COM_PASSPORT_TYPE.UseSelectable = true;
            this.COM_PASSPORT_TYPE.SelectionChangeCommitted += new System.EventHandler(this.COM_PASSPORT_TYPE_SelectionChangeCommitted);
            // 
            // COM_PASSPORT_RES
            // 
            this.COM_PASSPORT_RES.FormattingEnabled = true;
            this.COM_PASSPORT_RES.IntegralHeight = false;
            this.COM_PASSPORT_RES.ItemHeight = 24;
            this.COM_PASSPORT_RES.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.COM_PASSPORT_RES.Location = new System.Drawing.Point(157, 232);
            this.COM_PASSPORT_RES.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PASSPORT_RES.Name = "COM_PASSPORT_RES";
            this.COM_PASSPORT_RES.Size = new System.Drawing.Size(284, 30);
            this.COM_PASSPORT_RES.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PASSPORT_RES.TabIndex = 6;
            this.COM_PASSPORT_RES.UseSelectable = true;
            this.COM_PASSPORT_RES.SelectionChangeCommitted += new System.EventHandler(this.COM_PASSPORT_RES_SelectionChangeCommitted);
            // 
            // COM_PASSPORT_SEX
            // 
            this.COM_PASSPORT_SEX.FormattingEnabled = true;
            this.COM_PASSPORT_SEX.ItemHeight = 24;
            this.COM_PASSPORT_SEX.Items.AddRange(new object[] {
            "M",
            "F"});
            this.COM_PASSPORT_SEX.Location = new System.Drawing.Point(606, 145);
            this.COM_PASSPORT_SEX.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.COM_PASSPORT_SEX.Name = "COM_PASSPORT_SEX";
            this.COM_PASSPORT_SEX.Size = new System.Drawing.Size(228, 30);
            this.COM_PASSPORT_SEX.Style = MetroFramework.MetroColorStyle.Orange;
            this.COM_PASSPORT_SEX.TabIndex = 3;
            this.COM_PASSPORT_SEX.UseSelectable = true;
            // 
            // TXT_PASSPORT_BIRTH
            // 
            this.TXT_PASSPORT_BIRTH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_PASSPORT_BIRTH.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TXT_PASSPORT_BIRTH.Location = new System.Drawing.Point(606, 189);
            this.TXT_PASSPORT_BIRTH.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PASSPORT_BIRTH.Name = "TXT_PASSPORT_BIRTH";
            this.TXT_PASSPORT_BIRTH.Size = new System.Drawing.Size(228, 34);
            this.TXT_PASSPORT_BIRTH.TabIndex = 5;
            // 
            // TXT_PASSPORT_EXP
            // 
            this.TXT_PASSPORT_EXP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_PASSPORT_EXP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TXT_PASSPORT_EXP.Location = new System.Drawing.Point(606, 232);
            this.TXT_PASSPORT_EXP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PASSPORT_EXP.Name = "TXT_PASSPORT_EXP";
            this.TXT_PASSPORT_EXP.Size = new System.Drawing.Size(228, 34);
            this.TXT_PASSPORT_EXP.TabIndex = 7;
            // 
            // TXT_DATE_LAND
            // 
            this.TXT_DATE_LAND.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_DATE_LAND.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.TXT_DATE_LAND.Location = new System.Drawing.Point(606, 276);
            this.TXT_DATE_LAND.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_DATE_LAND.Name = "TXT_DATE_LAND";
            this.TXT_DATE_LAND.Size = new System.Drawing.Size(228, 34);
            this.TXT_DATE_LAND.TabIndex = 8;
            this.TXT_DATE_LAND.Leave += new System.EventHandler(this.TXT_DATE_LAND_Leave);
            // 
            // LBL_PERMIT_NO
            // 
            this.LBL_PERMIT_NO.AutoSize = true;
            this.LBL_PERMIT_NO.Location = new System.Drawing.Point(14, 290);
            this.LBL_PERMIT_NO.Name = "LBL_PERMIT_NO";
            this.LBL_PERMIT_NO.Size = new System.Drawing.Size(71, 20);
            this.LBL_PERMIT_NO.TabIndex = 29;
            this.LBL_PERMIT_NO.Text = "Permit No";
            // 
            // TXT_PERMIT_NO
            // 
            this.TXT_PERMIT_NO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.TXT_PERMIT_NO.CustomButton.Image = null;
            this.TXT_PERMIT_NO.CustomButton.Location = new System.Drawing.Point(251, 2);
            this.TXT_PERMIT_NO.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PERMIT_NO.CustomButton.Name = "";
            this.TXT_PERMIT_NO.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.TXT_PERMIT_NO.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_PERMIT_NO.CustomButton.TabIndex = 1;
            this.TXT_PERMIT_NO.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_PERMIT_NO.CustomButton.UseSelectable = true;
            this.TXT_PERMIT_NO.CustomButton.Visible = false;
            this.TXT_PERMIT_NO.Enabled = false;
            this.TXT_PERMIT_NO.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_PERMIT_NO.Lines = new string[0];
            this.TXT_PERMIT_NO.Location = new System.Drawing.Point(157, 277);
            this.TXT_PERMIT_NO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_PERMIT_NO.MaxLength = 15;
            this.TXT_PERMIT_NO.Name = "TXT_PERMIT_NO";
            this.TXT_PERMIT_NO.PasswordChar = '\0';
            this.TXT_PERMIT_NO.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_PERMIT_NO.SelectedText = "";
            this.TXT_PERMIT_NO.SelectionLength = 0;
            this.TXT_PERMIT_NO.SelectionStart = 0;
            this.TXT_PERMIT_NO.ShortcutsEnabled = true;
            this.TXT_PERMIT_NO.Size = new System.Drawing.Size(285, 36);
            this.TXT_PERMIT_NO.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_PERMIT_NO.TabIndex = 28;
            this.TXT_PERMIT_NO.UseSelectable = true;
            this.TXT_PERMIT_NO.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_PERMIT_NO.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.TXT_PERMIT_NO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_PASSPORT_NO_KeyPress);
            this.TXT_PERMIT_NO.Leave += new System.EventHandler(this.TXT_PERMIT_NO_Leave);
            // 
            // LBL_NOTE
            // 
            this.LBL_NOTE.AutoSize = true;
            this.LBL_NOTE.Location = new System.Drawing.Point(14, 341);
            this.LBL_NOTE.Name = "LBL_NOTE";
            this.LBL_NOTE.Size = new System.Drawing.Size(40, 20);
            this.LBL_NOTE.TabIndex = 31;
            this.LBL_NOTE.Text = "Note";
            // 
            // TXT_NOTE
            // 
            this.TXT_NOTE.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.TXT_NOTE.CustomButton.Image = null;
            this.TXT_NOTE.CustomButton.Location = new System.Drawing.Point(644, 2);
            this.TXT_NOTE.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_NOTE.CustomButton.Name = "";
            this.TXT_NOTE.CustomButton.Size = new System.Drawing.Size(31, 31);
            this.TXT_NOTE.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TXT_NOTE.CustomButton.TabIndex = 1;
            this.TXT_NOTE.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TXT_NOTE.CustomButton.UseSelectable = true;
            this.TXT_NOTE.CustomButton.Visible = false;
            this.TXT_NOTE.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TXT_NOTE.Lines = new string[0];
            this.TXT_NOTE.Location = new System.Drawing.Point(157, 325);
            this.TXT_NOTE.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TXT_NOTE.MaxLength = 30;
            this.TXT_NOTE.Name = "TXT_NOTE";
            this.TXT_NOTE.PasswordChar = '\0';
            this.TXT_NOTE.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TXT_NOTE.SelectedText = "";
            this.TXT_NOTE.SelectionLength = 0;
            this.TXT_NOTE.SelectionStart = 0;
            this.TXT_NOTE.ShortcutsEnabled = true;
            this.TXT_NOTE.Size = new System.Drawing.Size(678, 36);
            this.TXT_NOTE.Style = MetroFramework.MetroColorStyle.Orange;
            this.TXT_NOTE.TabIndex = 30;
            this.TXT_NOTE.UseSelectable = true;
            this.TXT_NOTE.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TXT_NOTE.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.TXT_NOTE.Leave += new System.EventHandler(this.TXT_NOTE_Leave);
            // 
            // PassportInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 438);
            this.Controls.Add(this.LBL_NOTE);
            this.Controls.Add(this.TXT_NOTE);
            this.Controls.Add(this.LBL_PERMIT_NO);
            this.Controls.Add(this.TXT_PERMIT_NO);
            this.Controls.Add(this.TXT_DATE_LAND);
            this.Controls.Add(this.TXT_PASSPORT_EXP);
            this.Controls.Add(this.TXT_PASSPORT_BIRTH);
            this.Controls.Add(this.COM_PASSPORT_SEX);
            this.Controls.Add(this.COM_PASSPORT_RES);
            this.Controls.Add(this.COM_PASSPORT_TYPE);
            this.Controls.Add(this.COM_PASSPORT_NAT);
            this.Controls.Add(this.LBL_PASSPORT_EXP);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.LBL_PASSPORT_LAND);
            this.Controls.Add(this.LBL_PASSPORT_NAT);
            this.Controls.Add(this.LBL_PASSPORT_TYPE);
            this.Controls.Add(this.LBL_PASSPORT_RES);
            this.Controls.Add(this.LBL_PASSPORT_SEX);
            this.Controls.Add(this.LBL_PASSPORT_BIRTH);
            this.Controls.Add(this.LBL_PASSPORT_NO);
            this.Controls.Add(this.LBL_PASSPORT_NAME);
            this.Controls.Add(this.TXT_PASSPORT_NAME);
            this.Controls.Add(this.TXT_PASSPORT_NO);
            this.Controls.Add(this.BTN_CLOSE);
            this.Controls.Add(this.BTN_OK);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "PassportInfoForm";
            this.Padding = new System.Windows.Forms.Padding(23, 75, 23, 25);
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Passport Infomation";
            this.Activated += new System.EventHandler(this.PassportInfoForm_Activated);
            this.Load += new System.EventHandler(this.PassportInfoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton BTN_OK;
        private MetroFramework.Controls.MetroButton BTN_CLOSE;
        private MetroFramework.Controls.MetroTextBox TXT_PASSPORT_NAME;
        private MetroFramework.Controls.MetroTextBox TXT_PASSPORT_NO;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_NO;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_NAME;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_LAND;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_NAT;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_TYPE;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_RES;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_SEX;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_BIRTH;
        private MetroFramework.Controls.MetroLabel LBL_PASSPORT_EXP;
        private MetroFramework.Controls.MetroComboBox COM_PASSPORT_SEX;
        private MetroFramework.Controls.MetroComboBox COM_PASSPORT_RES;
        private MetroFramework.Controls.MetroComboBox COM_PASSPORT_TYPE;
        private MetroFramework.Controls.MetroComboBox COM_PASSPORT_NAT;
        private System.Windows.Forms.DateTimePicker TXT_PASSPORT_BIRTH;
        private System.Windows.Forms.DateTimePicker TXT_PASSPORT_EXP;
        private System.Windows.Forms.DateTimePicker TXT_DATE_LAND;
        private MetroFramework.Controls.MetroLabel LBL_PERMIT_NO;
        private MetroFramework.Controls.MetroTextBox TXT_PERMIT_NO;
        private MetroFramework.Controls.MetroLabel LBL_NOTE;
        private MetroFramework.Controls.MetroTextBox TXT_NOTE;
    }
}