namespace Hana.PRD
{
    partial class PRD010312
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010312));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvQual = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHold = new System.Windows.Forms.Label();
            this.cboHolddiv = new System.Windows.Forms.ComboBox();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.chkViewAO = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboTimeBase = new System.Windows.Forms.ComboBox();
            this.lblNotice = new System.Windows.Forms.Label();
            this.chkResCnt = new System.Windows.Forms.CheckBox();
            this.ckbSOP = new System.Windows.Forms.CheckBox();
            this.ckbRepair = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.ckbOper = new System.Windows.Forms.CheckBox();
            this.ckbMcpKit = new System.Windows.Forms.CheckBox();
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
            this.lblTitle.Size = new System.Drawing.Size(98, 13);
            this.lblTitle.Text = "Inquiry by PART";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 114);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Controls.Add(this.ckbMcpKit);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.ckbOper);
            this.pnlCondition3.Controls.Add(this.ckbRepair);
            this.pnlCondition3.Controls.Add(this.chkResCnt);
            this.pnlCondition3.Controls.Add(this.label3);
            this.pnlCondition3.Controls.Add(this.lblProduct);
            this.pnlCondition3.Controls.Add(this.txtProduct);
            this.pnlCondition3.Controls.Add(this.ckbSOP);
            this.pnlCondition3.Controls.Add(this.chkViewAO);
            this.pnlCondition3.Controls.Add(this.ckbKpcs);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cboTimeBase);
            this.pnlCondition2.Controls.Add(this.lblNotice);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.label6);
            this.pnlCondition2.Controls.Add(this.cdvDate);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.cboHolddiv);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.lblHold);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label7);
            this.pnlCondition1.Controls.Add(this.cdvQual);
            this.pnlCondition1.Controls.Add(this.label8);
            this.pnlCondition1.Controls.Add(this.cdvOper);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
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
            this.spdData.TabIndex = 1;
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
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.ForcInitControl2 = this.cdvQual;
            this.cdvFactory.Location = new System.Drawing.Point(5, 3);
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
            // cdvQual
            // 
            this.cdvQual.BackColor = System.Drawing.Color.Transparent;
            this.cdvQual.bMultiSelect = true;
            this.cdvQual.ConditionText = "QUAL classification";
            this.cdvQual.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.CUSTOM;
            this.cdvQual.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvQual.Location = new System.Drawing.Point(709, 2);
            this.cdvQual.MandatoryFlag = false;
            this.cdvQual.Name = "cdvQual";
            this.cdvQual.ParentValue = "";
            this.cdvQual.RefFactory = this.cdvFactory;
            this.cdvQual.sCodeColumnName = "";
            this.cdvQual.sDynamicQuery = "";
            this.cdvQual.sFactory = "";
            this.cdvQual.Size = new System.Drawing.Size(180, 21);
            this.cdvQual.sTableName = "H_HANA_LOT_TYPE";
            this.cdvQual.sValueColumnName = "";
            this.cdvQual.TabIndex = 46;
            this.cdvQual.VisibleValueButton = true;
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition.eFromToType.OPER;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(404, 2);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(290, 21);
            this.cdvOper.sTableName = "";
            this.cdvOper.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(207, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 50;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHold
            // 
            this.lblHold.AutoSize = true;
            this.lblHold.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHold.Location = new System.Drawing.Point(219, 7);
            this.lblHold.Name = "lblHold";
            this.lblHold.Size = new System.Drawing.Size(91, 13);
            this.lblHold.TabIndex = 49;
            this.lblHold.Text = "Hold classification";
            // 
            // cboHolddiv
            // 
            this.cboHolddiv.FormattingEnabled = true;
            this.cboHolddiv.Items.AddRange(new object[] {
            "ALL",
            "Hold",
            "Non Hold"});
            this.cboHolddiv.Location = new System.Drawing.Point(284, 3);
            this.cboHolddiv.Name = "cboHolddiv";
            this.cboHolddiv.Size = new System.Drawing.Size(100, 21);
            this.cboHolddiv.TabIndex = 53;
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(206, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 71;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // chkViewAO
            // 
            this.chkViewAO.AutoSize = true;
            this.chkViewAO.Location = new System.Drawing.Point(268, 4);
            this.chkViewAO.Name = "chkViewAO";
            this.chkViewAO.Size = new System.Drawing.Size(124, 17);
            this.chkViewAO.TabIndex = 72;
            this.chkViewAO.Text = "Performance Inquiry";
            this.chkViewAO.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(9, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 76;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(22, 7);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 75;
            this.lblProduct.Text = "Product";
            // 
            // txtProduct
            // 
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Location = new System.Drawing.Point(85, 3);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(102, 21);
            this.txtProduct.TabIndex = 74;
            this.txtProduct.Text = "%";
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(85, 4);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(101, 20);
            this.cdvDate.TabIndex = 75;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 11, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 74;
            this.label2.Text = "Date";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 73;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(407, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 78;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(418, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 77;
            this.label6.Text = "Time Base";
            // 
            // cboTimeBase
            // 
            this.cboTimeBase.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cboTimeBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeBase.FormattingEnabled = true;
            this.cboTimeBase.Items.AddRange(new object[] {
            "00 hours",
            "04 hours",
            "06 hours",
            "14 hours",
            "22 hours"});
            this.cboTimeBase.Location = new System.Drawing.Point(484, 3);
            this.cboTimeBase.Name = "cboTimeBase";
            this.cboTimeBase.Size = new System.Drawing.Size(58, 21);
            this.cboTimeBase.TabIndex = 76;
            this.cboTimeBase.SelectedIndexChanged += new System.EventHandler(this.cboTimeBase_SelectedIndexChanged);
            // 
            // lblNotice
            // 
            this.lblNotice.AutoSize = true;
            this.lblNotice.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNotice.Location = new System.Drawing.Point(548, 6);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(323, 13);
            this.lblNotice.TabIndex = 77;
            this.lblNotice.Text = "※ The 0 and 4 o\'clock standard is only available for WIP.";
            // 
            // chkResCnt
            // 
            this.chkResCnt.AutoSize = true;
            this.chkResCnt.Location = new System.Drawing.Point(427, 4);
            this.chkResCnt.Name = "chkResCnt";
            this.chkResCnt.Size = new System.Drawing.Size(171, 17);
            this.chkResCnt.TabIndex = 79;
            this.chkResCnt.Text = "Arrange Number of Equipment";
            this.chkResCnt.UseVisualStyleBackColor = true;
            // 
            // ckbSOP
            // 
            this.ckbSOP.AutoSize = true;
            this.ckbSOP.Location = new System.Drawing.Point(353, 4);
            this.ckbSOP.Name = "ckbSOP";
            this.ckbSOP.Size = new System.Drawing.Size(82, 17);
            this.ckbSOP.TabIndex = 72;
            this.ckbSOP.Text = "SOP display";
            this.ckbSOP.UseVisualStyleBackColor = true;
            // 
            // ckbRepair
            // 
            this.ckbRepair.AutoSize = true;
            this.ckbRepair.Location = new System.Drawing.Point(547, 4);
            this.ckbRepair.Name = "ckbRepair";
            this.ckbRepair.Size = new System.Drawing.Size(159, 17);
            this.ckbRepair.TabIndex = 80;
            this.ckbRepair.Text = "Apply Repair / Return Stock";
            this.ckbRepair.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(207, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 80;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(218, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 79;
            this.label8.Text = "Lot Type";
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(284, 2);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(100, 21);
            this.cdvLotType.TabIndex = 81;
            this.cdvLotType.Text = "ALL";
            // 
            // ckbOper
            // 
            this.ckbOper.AutoSize = true;
            this.ckbOper.Checked = true;
            this.ckbOper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbOper.Location = new System.Drawing.Point(703, 4);
            this.ckbOper.Name = "ckbOper";
            this.ckbOper.Size = new System.Drawing.Size(177, 17);
            this.ckbOper.TabIndex = 81;
            this.ckbOper.Text = "Not Found Operation Exception";
            this.ckbOper.UseVisualStyleBackColor = true;
            // 
            // ckbMcpKit
            // 
            this.ckbMcpKit.AutoSize = true;
            this.ckbMcpKit.Location = new System.Drawing.Point(206, 4);
            this.ckbMcpKit.Name = "ckbMcpKit";
            this.ckbMcpKit.Size = new System.Drawing.Size(108, 17);
            this.ckbMcpKit.TabIndex = 0;
            this.ckbMcpKit.Text = "Samsung MCP Kit";
            this.ckbMcpKit.UseVisualStyleBackColor = true;
            this.ckbMcpKit.CheckedChanged += new System.EventHandler(this.ckbMcpKit_CheckedChanged);
            // 
            // PRD010312
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 4;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010312";
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
        private Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition cdvOper;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHold;
        private System.Windows.Forms.ComboBox cboHolddiv;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.CheckBox chkViewAO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboTimeBase;
        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.CheckBox chkResCnt;
        private System.Windows.Forms.CheckBox ckbSOP;
        private System.Windows.Forms.CheckBox ckbRepair;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvQual;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox ckbOper;
        private System.Windows.Forms.CheckBox ckbMcpKit;
    }
}
