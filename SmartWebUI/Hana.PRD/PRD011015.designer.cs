namespace Hana.PRD
{
    partial class PRD011015
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
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD011015));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cdvGroup = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.cdvType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cdvTime = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ckbCOB = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cboTimeBase = new System.Windows.Forms.ComboBox();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.ckdPlan = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cdvCure = new System.Windows.Forms.ComboBox();
            this.ckbCmold = new System.Windows.Forms.CheckBox();
            this.ckdHideOper = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.holdFlag = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition4.SuspendLayout();
            this.pnlCondition3.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlWIPDetail.SuspendLayout();
            this.pnlDetailCondition2.SuspendLayout();
            this.pnlDetailCondition1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRASDetail.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlDetailCondition3.SuspendLayout();
            this.pnlBUMPDetail.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(167, 13);
            this.lblTitle.Text = "Planning balance monitoring";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 114);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 182);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 158);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 134);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 110);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Controls.Add(this.holdFlag);
            this.pnlCondition4.Controls.Add(this.label19);
            this.pnlCondition4.Controls.Add(this.label16);
            this.pnlCondition4.Controls.Add(this.label15);
            this.pnlCondition4.Controls.Add(this.label17);
            this.pnlCondition4.Controls.Add(this.label14);
            this.pnlCondition4.Controls.Add(this.label18);
            this.pnlCondition4.Location = new System.Drawing.Point(0, 86);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.ckbCmold);
            this.pnlCondition3.Controls.Add(this.label6);
            this.pnlCondition3.Controls.Add(this.ckbCOB);
            this.pnlCondition3.Controls.Add(this.cdvCure);
            this.pnlCondition3.Controls.Add(this.cdvTime);
            this.pnlCondition3.Controls.Add(this.label13);
            this.pnlCondition3.Controls.Add(this.label8);
            this.pnlCondition3.Controls.Add(this.label7);
            this.pnlCondition3.Controls.Add(this.label12);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.ckdHideOper);
            this.pnlCondition2.Controls.Add(this.ckdPlan);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.cdvType);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.ckbKpcs);
            this.pnlCondition2.Controls.Add(this.cdvGroup);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.lbl_Product);
            this.pnlCondition2.Controls.Add(this.txtSearchProduct);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label11);
            this.pnlCondition1.Controls.Add(this.lblProduct);
            this.pnlCondition1.Controls.Add(this.label9);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.label10);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cboTimeBase);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 140);
            // 
            // udcWIPCondition8
            // 
            this.udcWIPCondition8.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition7
            // 
            this.udcWIPCondition7.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition6
            // 
            this.udcWIPCondition6.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition5
            // 
            this.udcWIPCondition5.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition4
            // 
            this.udcWIPCondition4.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition3
            // 
            this.udcWIPCondition3.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition2
            // 
            this.udcWIPCondition2.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition1
            // 
            this.udcWIPCondition1.RefFactory = this.cdvFactory;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 227);
            this.pnlMain.Size = new System.Drawing.Size(800, 373);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // btnView
            // 
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExcelExport.Text = "Save";
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // btnSort
            // 
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 140);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 287);
            // 
            // udcBUMPCondition9
            // 
            this.udcBUMPCondition9.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition1
            // 
            this.udcBUMPCondition1.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition2
            // 
            this.udcBUMPCondition2.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition3
            // 
            this.udcBUMPCondition3.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition4
            // 
            this.udcBUMPCondition4.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition5
            // 
            this.udcBUMPCondition5.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition6
            // 
            this.udcBUMPCondition6.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition7
            // 
            this.udcBUMPCondition7.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition8
            // 
            this.udcBUMPCondition8.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition12
            // 
            this.udcBUMPCondition12.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition11
            // 
            this.udcBUMPCondition11.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition10
            // 
            this.udcBUMPCondition10.RefFactory = this.cdvFactory;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(3, 3);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 370);
            this.spdData.TabIndex = 0;
            this.spdData.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spdData_CellClick);
            // 
            // spdData_Sheet1
            // 
            this.spdData_Sheet1.Reset();
            this.spdData_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdData_Sheet1.ColumnCount = 0;
            this.spdData_Sheet1.RowCount = 0;
            this.spdData_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdData.SetActiveViewport(0, 1, 1);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY_ASSEMBLY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.Location = new System.Drawing.Point(4, 3);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 12;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(431, 7);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(44, 13);
            this.lbl_Product.TabIndex = 15;
            this.lbl_Product.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(496, 2);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(101, 21);
            this.txtSearchProduct.TabIndex = 14;
            this.txtSearchProduct.Text = "%";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(420, 5);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 31;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(208, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 21;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(218, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(75, 13);
            this.lblDate.TabIndex = 20;
            this.lblDate.Text = "standard date";
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(267, 2);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 19;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            this.cdvDate.ValueChanged += new System.EventHandler(this.cdvDate_ValueChanged);
            // 
            // cdvGroup
            // 
            this.cdvGroup.BackColor = System.Drawing.Color.Transparent;
            this.cdvGroup.bMultiSelect = true;
            this.cdvGroup.ConditionText = "Select Residual Criteria";
            this.cdvGroup.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvGroup.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvGroup.Location = new System.Drawing.Point(4, 1);
            this.cdvGroup.MandatoryFlag = false;
            this.cdvGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cdvGroup.Name = "cdvGroup";
            this.cdvGroup.ParentValue = "";
            this.cdvGroup.sCodeColumnName = "Code";
            this.cdvGroup.sDynamicQuery = "";
            this.cdvGroup.sFactory = "";
            this.cdvGroup.Size = new System.Drawing.Size(181, 21);
            this.cdvGroup.sTableName = "";
            this.cdvGroup.sValueColumnName = "Data";
            this.cdvGroup.TabIndex = 108;
            this.cdvGroup.VisibleValueButton = true;
            this.cdvGroup.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvGroup_ValueSelectedItemChanged);
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Checked = true;
            this.ckbKpcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbKpcs.Location = new System.Drawing.Point(617, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 109;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // cdvType
            // 
            this.cdvType.FormattingEnabled = true;
            this.cdvType.Items.AddRange(new object[] {
            "Warehousing perspective",
            "Mold perspective",
            "Front perspective",
            "Flipchip perspective",
            "DP Total",
            "DP Normal",
            "DP WLCSP",
            "Finish perspective"});
            this.cdvType.Location = new System.Drawing.Point(301, 1);
            this.cdvType.Name = "cdvType";
            this.cdvType.Size = new System.Drawing.Size(98, 21);
            this.cdvType.TabIndex = 133;
            this.cdvType.Text = "Warehousing perspective";
            this.cdvType.SelectedIndexChanged += new System.EventHandler(this.cdvType_ValueMemberChanged);
            // 
            // label2
            // 
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(208, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(6, 17);
            this.label2.TabIndex = 23;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(218, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Work in operation display Standards";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(870, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 13);
            this.label4.TabIndex = 134;
            this.label4.Text = "W / B Cut-off: 06 o\'clock / PMC Cut-off: 13 o\'clock";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(870, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(185, 13);
            this.label5.TabIndex = 135;
            this.label5.Text = "(W / B: 11:00 / PMC 18:00 for Hynix)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(613, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(353, 13);
            this.label6.TabIndex = 135;
            this.label6.Text = "Detailed information is displayed when you click WIP or Units.";
            // 
            // cdvTime
            // 
            this.cdvTime.FormattingEnabled = true;
            this.cdvTime.Items.AddRange(new object[] {
            "MIX",
            "22 hours",
            "06 hours"});
            this.cdvTime.Location = new System.Drawing.Point(84, 4);
            this.cdvTime.Name = "cdvTime";
            this.cdvTime.Size = new System.Drawing.Size(100, 21);
            this.cdvTime.TabIndex = 0;
            this.cdvTime.Text = "MIX";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Performance standard";
            // 
            // label8
            // 
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(8, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(6, 17);
            this.label8.TabIndex = 23;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbCOB
            // 
            this.ckbCOB.AutoSize = true;
            this.ckbCOB.Checked = true;
            this.ckbCOB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCOB.Location = new System.Drawing.Point(390, 7);
            this.ckbCOB.Name = "ckbCOB";
            this.ckbCOB.Size = new System.Drawing.Size(93, 17);
            this.ckbCOB.TabIndex = 24;
            this.ckbCOB.Text = "Excluded COB";
            this.ckbCOB.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(420, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(6, 17);
            this.label9.TabIndex = 138;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(431, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 137;
            this.label10.Text = "Time Base";
            // 
            // cboTimeBase
            // 
            this.cboTimeBase.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cboTimeBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeBase.FormattingEnabled = true;
            this.cboTimeBase.Items.AddRange(new object[] {
            "Now",
            "00 hours",
            "01 hours",
            "02 hours",
            "03 hours",
            "04 hours",
            "05 hours",
            "06 hours",
            "07 hours",
            "08 hours",
            "09 hours",
            "10 hours",
            "11 hours",
            "12 hours",
            "13 hours",
            "14 hours",
            "15 hours",
            "16 hours",
            "17 hours",
            "18 hours",
            "19 hours",
            "20 hours",
            "21 hours",
            "22 hours",
            "23 hours"});
            this.cboTimeBase.Location = new System.Drawing.Point(497, 0);
            this.cboTimeBase.Name = "cboTimeBase";
            this.cboTimeBase.Size = new System.Drawing.Size(58, 21);
            this.cboTimeBase.TabIndex = 136;
            this.cboTimeBase.SelectedIndexChanged += new System.EventHandler(this.cboTimeBase_SelectedIndexChanged);
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(683, 1);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(61, 21);
            this.cdvLotType.TabIndex = 141;
            this.cdvLotType.Text = "P%";
            // 
            // label11
            // 
            this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(605, 3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(6, 17);
            this.label11.TabIndex = 140;
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(617, 4);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(49, 13);
            this.lblProduct.TabIndex = 139;
            this.lblProduct.Text = "Lot Type";
            // 
            // ckdPlan
            // 
            this.ckdPlan.AutoSize = true;
            this.ckdPlan.Location = new System.Drawing.Point(672, 4);
            this.ckdPlan.Name = "ckdPlan";
            this.ckdPlan.Size = new System.Drawing.Size(66, 17);
            this.ckdPlan.TabIndex = 136;
            this.ckdPlan.Text = "Planning";
            this.ckdPlan.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(219, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(123, 13);
            this.label12.TabIndex = 137;
            this.label12.Text = "Including cure operation";
            // 
            // label13
            // 
            this.label13.Image = ((System.Drawing.Image)(resources.GetObject("label13.Image")));
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label13.Location = new System.Drawing.Point(208, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(6, 17);
            this.label13.TabIndex = 138;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvCure
            // 
            this.cdvCure.FormattingEnabled = true;
            this.cdvCure.Items.AddRange(new object[] {
            "DA",
            "WB"});
            this.cdvCure.Location = new System.Drawing.Point(309, 4);
            this.cdvCure.Name = "cdvCure";
            this.cdvCure.Size = new System.Drawing.Size(46, 21);
            this.cdvCure.TabIndex = 0;
            this.cdvCure.Text = "DA";
            // 
            // ckbCmold
            // 
            this.ckbCmold.AutoSize = true;
            this.ckbCmold.Location = new System.Drawing.Point(483, 6);
            this.ckbCmold.Name = "ckbCmold";
            this.ckbCmold.Size = new System.Drawing.Size(100, 17);
            this.ckbCmold.TabIndex = 139;
            this.ckbCmold.Text = "C-MOLD inquiry";
            this.ckbCmold.UseVisualStyleBackColor = true;
            // 
            // ckdHideOper
            // 
            this.ckdHideOper.AutoSize = true;
            this.ckdHideOper.Location = new System.Drawing.Point(745, 4);
            this.ckdHideOper.Name = "ckdHideOper";
            this.ckdHideOper.Size = new System.Drawing.Size(146, 17);
            this.ckdHideOper.TabIndex = 137;
            this.ckdHideOper.Text = "Hide unworked operation";
            this.ckdHideOper.UseVisualStyleBackColor = true;
            this.ckdHideOper.CheckedChanged += new System.EventHandler(this.ckdHideOper_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(16, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(131, 13);
            this.label14.TabIndex = 136;
            this.label14.Text = "SEC Mem. SOP Cut-off:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.Red;
            this.label15.Location = new System.Drawing.Point(151, 6);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(250, 13);
            this.label15.TabIndex = 136;
            this.label15.Text = "[Wednesday: BOC / DDP / TDP / QDP / ODP]";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(352, 6);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(143, 13);
            this.label16.TabIndex = 136;
            this.label16.Text = "[Thursday: eMCP / MCP]";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(475, 6);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 13);
            this.label17.TabIndex = 136;
            this.label17.Text = "[Friday: eMMC]";
            // 
            // label18
            // 
            this.label18.Image = ((System.Drawing.Image)(resources.GetObject("label18.Image")));
            this.label18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label18.Location = new System.Drawing.Point(8, 4);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(6, 17);
            this.label18.TabIndex = 23;
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // holdFlag
            // 
            this.holdFlag.FormattingEnabled = true;
            this.holdFlag.Items.AddRange(new object[] {
            "ALL",
            "NONE",
            "ONLY"});
            this.holdFlag.Location = new System.Drawing.Point(736, 3);
            this.holdFlag.Name = "holdFlag";
            this.holdFlag.Size = new System.Drawing.Size(63, 21);
            this.holdFlag.TabIndex = 140;
            this.holdFlag.Text = "ALL";
            this.holdFlag.Visible = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(698, 7);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(34, 13);
            this.label19.TabIndex = 141;
            this.label19.Text = "HOLD";
            this.label19.Visible = false;
            // 
            // PRD011015
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 4;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD011015";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition4.ResumeLayout(false);
            this.pnlCondition4.PerformLayout();
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlDetailCondition3.ResumeLayout(false);
            this.pnlBUMPDetail.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvGroup;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.ComboBox cdvType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox ckbCOB;
        private System.Windows.Forms.ComboBox cdvTime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboTimeBase;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.CheckBox ckdPlan;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cdvCure;
        private System.Windows.Forms.CheckBox ckbCmold;
        private System.Windows.Forms.CheckBox ckdHideOper;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox holdFlag;
        private System.Windows.Forms.Label label19;
    }
}
