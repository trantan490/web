namespace Hana.PQC
{
    partial class PQC030115
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
            SoftwareFX.ChartFX.TitleDockable titleDockable1 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable3 = new SoftwareFX.ChartFX.TitleDockable();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.scCenter = new System.Windows.Forms.SplitContainer();
            this.scLeft = new System.Windows.Forms.SplitContainer();
            this.cf01 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.scRight = new System.Windows.Forms.SplitContainer();
            this.cf02 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cf03 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvCustomer = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.rbS03 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbS02 = new System.Windows.Forms.RadioButton();
            this.rbS01 = new System.Windows.Forms.RadioButton();
            this.cdvPackage = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvBaseDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.lblBaseDate = new System.Windows.Forms.Label();
            this.lblDisplayCount = new System.Windows.Forms.Label();
            this.cmbDisplayCount = new System.Windows.Forms.ComboBox();
            this.chkPrevFlag = new System.Windows.Forms.CheckBox();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlWIPDetail.SuspendLayout();
            this.pnlDetailCondition2.SuspendLayout();
            this.pnlDetailCondition1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRASDetail.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.scCenter.Panel1.SuspendLayout();
            this.scCenter.Panel2.SuspendLayout();
            this.scCenter.SuspendLayout();
            this.scLeft.Panel1.SuspendLayout();
            this.scLeft.Panel2.SuspendLayout();
            this.scLeft.SuspendLayout();
            this.scRight.Panel1.SuspendLayout();
            this.scRight.Panel2.SuspendLayout();
            this.scRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
            this.gbSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(224, 13);
            this.lblTitle.Text = "제품별 공정불량현황(전월불량 TOP 5)";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 177);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 153);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 129);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 105);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 81);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvOper);
            this.pnlCondition2.Controls.Add(this.cmbDisplayCount);
            this.pnlCondition2.Controls.Add(this.lblDisplayCount);
            this.pnlCondition2.Controls.Add(this.cdvPackage);
            this.pnlCondition2.Controls.Add(this.cdvCustomer);
            this.pnlCondition2.Controls.Add(this.ckbKpcs);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvBaseDate);
            this.pnlCondition1.Controls.Add(this.lblBaseDate);
            this.pnlCondition1.Controls.Add(this.chkPrevFlag);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.gbSearch);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
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
            this.pnlMain.Controls.Add(this.scCenter);
            this.pnlMain.Location = new System.Drawing.Point(0, 152);
            this.pnlMain.Size = new System.Drawing.Size(800, 448);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDetail.Visible = false;
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
            this.btnSort.Visible = false;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(747, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 17;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            this.ckbKpcs.Visible = false;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(4, 2);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 18;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // scCenter
            // 
            this.scCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCenter.Location = new System.Drawing.Point(3, 3);
            this.scCenter.Name = "scCenter";
            this.scCenter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCenter.Panel1
            // 
            this.scCenter.Panel1.Controls.Add(this.scLeft);
            // 
            // scCenter.Panel2
            // 
            this.scCenter.Panel2.Controls.Add(this.SS01);
            this.scCenter.Size = new System.Drawing.Size(794, 445);
            this.scCenter.SplitterDistance = 224;
            this.scCenter.TabIndex = 1;
            // 
            // scLeft
            // 
            this.scLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLeft.Location = new System.Drawing.Point(0, 0);
            this.scLeft.Name = "scLeft";
            // 
            // scLeft.Panel1
            // 
            this.scLeft.Panel1.Controls.Add(this.cf01);
            // 
            // scLeft.Panel2
            // 
            this.scLeft.Panel2.Controls.Add(this.scRight);
            this.scLeft.Size = new System.Drawing.Size(794, 224);
            this.scLeft.SplitterDistance = 200;
            this.scLeft.TabIndex = 26;
            // 
            // cf01
            // 
            this.cf01.AxisX.Staggered = true;
            this.cf01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf01.Location = new System.Drawing.Point(0, 0);
            this.cf01.Name = "cf01";
            this.cf01.Size = new System.Drawing.Size(200, 224);
            this.cf01.TabIndex = 24;
            this.cf01.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable1});
            this.cf01.ToolBar = true;
            // 
            // scRight
            // 
            this.scRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRight.Location = new System.Drawing.Point(0, 0);
            this.scRight.Name = "scRight";
            // 
            // scRight.Panel1
            // 
            this.scRight.Panel1.Controls.Add(this.cf02);
            // 
            // scRight.Panel2
            // 
            this.scRight.Panel2.Controls.Add(this.cf03);
            this.scRight.Size = new System.Drawing.Size(590, 224);
            this.scRight.SplitterDistance = 300;
            this.scRight.TabIndex = 0;
            // 
            // cf02
            // 
            this.cf02.AxisX.Staggered = true;
            this.cf02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf02.Location = new System.Drawing.Point(0, 0);
            this.cf02.Name = "cf02";
            this.cf02.Size = new System.Drawing.Size(300, 224);
            this.cf02.TabIndex = 25;
            this.cf02.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
            this.cf02.ToolBar = true;
            // 
            // cf03
            // 
            this.cf03.AxisX.Staggered = true;
            this.cf03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf03.Location = new System.Drawing.Point(0, 0);
            this.cf03.Name = "cf03";
            this.cf03.Size = new System.Drawing.Size(286, 224);
            this.cf03.TabIndex = 26;
            this.cf03.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable3});
            this.cf03.ToolBar = true;
            // 
            // SS01
            // 
            this.SS01.About = "4.0.2001.2005";
            this.SS01.AccessibleDescription = "SS01, Sheet1, Row 0, Column 0, ";
            this.SS01.BackColor = System.Drawing.Color.White;
            this.SS01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SS01.Location = new System.Drawing.Point(0, 0);
            this.SS01.Name = "SS01";
            this.SS01.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SS01.RPT_IsPreCellsType = true;
            this.SS01.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS01_Sheet1});
            this.SS01.Size = new System.Drawing.Size(794, 217);
            this.SS01.TabIndex = 1;
            this.SS01.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.SS01_CellClick);
            // 
            // SS01_Sheet1
            // 
            this.SS01_Sheet1.Reset();
            this.SS01_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SS01_Sheet1.ColumnCount = 0;
            this.SS01_Sheet1.RowCount = 0;
            this.SS01_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.SS01.SetActiveViewport(0, 1, 1);
            // 
            // cdvCustomer
            // 
            this.cdvCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cdvCustomer.bMultiSelect = false;
            this.cdvCustomer.ConditionText = "고객사";
            this.cdvCustomer.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvCustomer.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvCustomer.Location = new System.Drawing.Point(4, 2);
            this.cdvCustomer.MandatoryFlag = false;
            this.cdvCustomer.Name = "cdvCustomer";
            this.cdvCustomer.ParentValue = "";
            this.cdvCustomer.sCodeColumnName = "CUST_CODE";
            this.cdvCustomer.sDynamicQuery = "SELECT DECODE(KEY_1, \'-\', \' \', KEY_1) AS CUST_CODE, DECODE(DATA_1, \'미확인\', \'전체\', D" +
                "ATA_1) AS CUST_DESC FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = \'HMKA1\' AND TABLE_N" +
                "AME = \'H_CUSTOMER\' ORDER BY KEY_1 ASC";
            this.cdvCustomer.sFactory = "";
            this.cdvCustomer.Size = new System.Drawing.Size(180, 21);
            this.cdvCustomer.sTableName = "";
            this.cdvCustomer.sValueColumnName = "CUST_DESC";
            this.cdvCustomer.TabIndex = 73;
            this.cdvCustomer.VisibleValueButton = true;
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.rbS03);
            this.gbSearch.Controls.Add(this.label4);
            this.gbSearch.Controls.Add(this.rbS02);
            this.gbSearch.Controls.Add(this.rbS01);
            this.gbSearch.Location = new System.Drawing.Point(204, -6);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(235, 30);
            this.gbSearch.TabIndex = 72;
            this.gbSearch.TabStop = false;
            // 
            // rbS03
            // 
            this.rbS03.AutoSize = true;
            this.rbS03.Location = new System.Drawing.Point(175, 9);
            this.rbS03.Name = "rbS03";
            this.rbS03.Size = new System.Drawing.Size(53, 17);
            this.rbS03.TabIndex = 73;
            this.rbS03.Text = "YIELD";
            this.rbS03.UseVisualStyleBackColor = true;
            this.rbS03.CheckedChanged += new System.EventHandler(this.rbS_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "검색구분 :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbS02
            // 
            this.rbS02.AutoSize = true;
            this.rbS02.Location = new System.Drawing.Point(123, 9);
            this.rbS02.Name = "rbS02";
            this.rbS02.Size = new System.Drawing.Size(44, 17);
            this.rbS02.TabIndex = 71;
            this.rbS02.Text = "LRR";
            this.rbS02.UseVisualStyleBackColor = true;
            this.rbS02.CheckedChanged += new System.EventHandler(this.rbS_CheckedChanged);
            // 
            // rbS01
            // 
            this.rbS01.AutoSize = true;
            this.rbS01.Checked = true;
            this.rbS01.Location = new System.Drawing.Point(70, 9);
            this.rbS01.Name = "rbS01";
            this.rbS01.Size = new System.Drawing.Size(45, 17);
            this.rbS01.TabIndex = 70;
            this.rbS01.TabStop = true;
            this.rbS01.Text = "PPM";
            this.rbS01.UseVisualStyleBackColor = true;
            this.rbS01.CheckedChanged += new System.EventHandler(this.rbS_CheckedChanged);
            // 
            // cdvPackage
            // 
            this.cdvPackage.BackColor = System.Drawing.Color.Transparent;
            this.cdvPackage.bMultiSelect = false;
            this.cdvPackage.ConditionText = "Package";
            this.cdvPackage.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvPackage.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvPackage.Location = new System.Drawing.Point(204, 2);
            this.cdvPackage.MandatoryFlag = false;
            this.cdvPackage.Name = "cdvPackage";
            this.cdvPackage.ParentValue = "";
            this.cdvPackage.sCodeColumnName = "PKG";
            this.cdvPackage.sDynamicQuery = "SELECT DECODE(KEY_1, \'-\', \' \', KEY_1) AS PKG, DATA_1 AS PKG_DESC FROM MGCMTBLDAT@" +
                "RPTTOMES WHERE FACTORY = \'HMKA1\' AND TABLE_NAME = \'H_PACKAGE\' ORDER BY KEY_1 ASC" +
                "";
            this.cdvPackage.sFactory = "";
            this.cdvPackage.Size = new System.Drawing.Size(235, 21);
            this.cdvPackage.sTableName = "";
            this.cdvPackage.sValueColumnName = "PKG_DESC";
            this.cdvPackage.TabIndex = 74;
            this.cdvPackage.VisibleValueButton = true;
            // 
            // cdvBaseDate
            // 
            this.cdvBaseDate.CustomFormat = "yyyy-MM-dd";
            this.cdvBaseDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvBaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvBaseDate.Location = new System.Drawing.Point(529, 3);
            this.cdvBaseDate.Name = "cdvBaseDate";
            this.cdvBaseDate.Size = new System.Drawing.Size(97, 20);
            this.cdvBaseDate.TabIndex = 74;
            this.cdvBaseDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.FromDate;
            this.cdvBaseDate.Value = new System.DateTime(2009, 7, 3, 0, 0, 0, 0);
            // 
            // lblBaseDate
            // 
            this.lblBaseDate.AutoSize = true;
            this.lblBaseDate.Location = new System.Drawing.Point(467, 6);
            this.lblBaseDate.Name = "lblBaseDate";
            this.lblBaseDate.Size = new System.Drawing.Size(65, 13);
            this.lblBaseDate.TabIndex = 73;
            this.lblBaseDate.Text = "기준일자 : ";
            // 
            // lblDisplayCount
            // 
            this.lblDisplayCount.AutoSize = true;
            this.lblDisplayCount.Location = new System.Drawing.Point(672, 6);
            this.lblDisplayCount.Name = "lblDisplayCount";
            this.lblDisplayCount.Size = new System.Drawing.Size(65, 13);
            this.lblDisplayCount.TabIndex = 75;
            this.lblDisplayCount.Text = "표시건수 : ";
            this.lblDisplayCount.Visible = false;
            // 
            // cmbDisplayCount
            // 
            this.cmbDisplayCount.FormattingEnabled = true;
            this.cmbDisplayCount.Items.AddRange(new object[] {
            "ALL",
            "5",
            "10",
            "15",
            "20"});
            this.cmbDisplayCount.Location = new System.Drawing.Point(733, 2);
            this.cmbDisplayCount.Name = "cmbDisplayCount";
            this.cmbDisplayCount.Size = new System.Drawing.Size(62, 21);
            this.cmbDisplayCount.TabIndex = 76;
            this.cmbDisplayCount.Text = "5";
            this.cmbDisplayCount.Visible = false;
            // 
            // chkPrevFlag
            // 
            this.chkPrevFlag.AutoSize = true;
            this.chkPrevFlag.Checked = true;
            this.chkPrevFlag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrevFlag.Location = new System.Drawing.Point(662, 3);
            this.chkPrevFlag.Name = "chkPrevFlag";
            this.chkPrevFlag.Size = new System.Drawing.Size(133, 17);
            this.chkPrevFlag.TabIndex = 76;
            this.chkPrevFlag.Text = "전월불량 TOP 5 기준";
            this.chkPrevFlag.UseVisualStyleBackColor = true;
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.White;
            this.cdvOper.bMultiSelect = false;
            this.cdvOper.ConditionText = "공정";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(470, 1);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sCodeColumnName = "OPER";
            this.cdvOper.sDynamicQuery = "SELECT \' \' AS OPER, \'전체\' AS OPER_DESC FROM DUAL UNION ALL SELECT OPER AS OPER, OP" +
                "ER_DESC AS OPER_DESC FROM MESMGR.MWIPOPRDEF@RPTTOMES WHERE FACTORY = \'HMKA1\' AND" +
                " OPER LIKE \'A%\' ORDER BY OPER ASC";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(156, 22);
            this.cdvOper.sTableName = "";
            this.cdvOper.sValueColumnName = "OPER_DESC";
            this.cdvOper.TabIndex = 77;
            this.cdvOper.VisibleValueButton = true;
            // 
            // PQC030115
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030115";
            this.pnlMiddle.ResumeLayout(false);
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
            this.scCenter.Panel1.ResumeLayout(false);
            this.scCenter.Panel2.ResumeLayout(false);
            this.scCenter.ResumeLayout(false);
            this.scLeft.Panel1.ResumeLayout(false);
            this.scLeft.Panel2.ResumeLayout(false);
            this.scLeft.ResumeLayout(false);
            this.scRight.Panel1.ResumeLayout(false);
            this.scRight.Panel2.ResumeLayout(false);
            this.scRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.CheckBox ckbKpcs;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
		private System.Windows.Forms.SplitContainer scCenter;
		private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
		private FarPoint.Win.Spread.SheetView SS01_Sheet1;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvCustomer;
		private System.Windows.Forms.GroupBox gbSearch;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton rbS02;
		private System.Windows.Forms.RadioButton rbS01;
		private System.Windows.Forms.RadioButton rbS03;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvPackage;
		private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvBaseDate;
		private System.Windows.Forms.Label lblBaseDate;
		private System.Windows.Forms.Label lblDisplayCount;
		private System.Windows.Forms.ComboBox cmbDisplayCount;
		private System.Windows.Forms.CheckBox chkPrevFlag;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOper;
		private System.Windows.Forms.SplitContainer scLeft;
		private Miracom.SmartWeb.UI.Controls.udcChartFX cf01;
		private System.Windows.Forms.SplitContainer scRight;
		private Miracom.SmartWeb.UI.Controls.udcChartFX cf02;
		private Miracom.SmartWeb.UI.Controls.udcChartFX cf03;
    }
}
