namespace Hana.MAT
{
    partial class MAT070503
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAT070503));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvMatType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtMatCode = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.lblUpdateTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ckbDetail = new System.Windows.Forms.CheckBox();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.ckbOverShort = new System.Windows.Forms.CheckBox();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.rdbMonth = new System.Windows.Forms.RadioButton();
            this.rdbWeek = new System.Windows.Forms.RadioButton();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition5.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(117, 13);
            this.lblTitle.Text = "Raw materials input check";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 138);
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
            this.pnlCondition5.Controls.Add(this.label14);
            this.pnlCondition5.Location = new System.Drawing.Point(0, 105);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Controls.Add(this.label13);
            this.pnlCondition4.Location = new System.Drawing.Point(0, 81);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.label10);
            this.pnlCondition3.Controls.Add(this.label9);
            this.pnlCondition3.Controls.Add(this.label6);
            this.pnlCondition3.Controls.Add(this.label7);
            this.pnlCondition3.Controls.Add(this.txtProduct);
            this.pnlCondition3.Controls.Add(this.lblUpdateTime);
            this.pnlCondition3.Controls.Add(this.label1);
            this.pnlCondition3.Controls.Add(this.lblProduct);
            this.pnlCondition3.Controls.Add(this.txtMatCode);
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvMatType);
            this.pnlCondition2.Controls.Add(this.cdvLotType);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.rdbMonth);
            this.pnlCondition2.Controls.Add(this.cdvDate);
            this.pnlCondition2.Controls.Add(this.rdbWeek);
            this.pnlCondition2.Controls.Add(this.label8);
            this.pnlCondition2.Controls.Add(this.label11);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.label12);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.ckbOverShort);
            this.pnlCondition1.Controls.Add(this.ckbKpcs);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.ckbDetail);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 164);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 251);
            this.pnlMain.Size = new System.Drawing.Size(800, 349);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 164);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spdData.Location = new System.Drawing.Point(3, 3);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 346);
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
            // cdvMatType
            // 
            this.cdvMatType.BackColor = System.Drawing.Color.Transparent;
            this.cdvMatType.bMultiSelect = true;
            this.cdvMatType.ConditionText = "Mat_Type";
            this.cdvMatType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvMatType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatType.Location = new System.Drawing.Point(19, 3);
            this.cdvMatType.MandatoryFlag = false;
            this.cdvMatType.Name = "cdvMatType";
            this.cdvMatType.ParentValue = "";
            this.cdvMatType.sCodeColumnName = "Code";
            this.cdvMatType.sDynamicQuery = "";
            this.cdvMatType.sFactory = "";
            this.cdvMatType.Size = new System.Drawing.Size(187, 21);
            this.cdvMatType.sTableName = "";
            this.cdvMatType.sValueColumnName = "Data";
            this.cdvMatType.TabIndex = 72;
            this.cdvMatType.VisibleValueButton = true;
            this.cdvMatType.ValueButtonPress += new System.EventHandler(this.cdvMatType_ValueButtonPress);
            // 
            // lblIcon
            // 
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(17, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 75;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(231, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(6, 17);
            this.label2.TabIndex = 75;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(34, 5);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 74;
            this.lblProduct.Text = "Product";
            // 
            // txtMatCode
            // 
            this.txtMatCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMatCode.Location = new System.Drawing.Point(690, 2);
            this.txtMatCode.Name = "txtMatCode";
            this.txtMatCode.Size = new System.Drawing.Size(101, 21);
            this.txtMatCode.TabIndex = 73;
            this.txtMatCode.Text = "%";
            // 
            // txtProduct
            // 
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Location = new System.Drawing.Point(93, 2);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(113, 21);
            this.txtProduct.TabIndex = 73;
            this.txtProduct.Text = "%";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(22, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 75;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(312, 1);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(98, 21);
            this.cdvLotType.TabIndex = 79;
            this.cdvLotType.Text = "P%";
            // 
            // lblUpdateTime
            // 
            this.lblUpdateTime.AutoSize = true;
            this.lblUpdateTime.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblUpdateTime.Location = new System.Drawing.Point(430, 5);
            this.lblUpdateTime.Name = "lblUpdateTime";
            this.lblUpdateTime.Size = new System.Drawing.Size(0, 13);
            this.lblUpdateTime.TabIndex = 77;
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(232, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 78;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(244, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Lot Type";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(242, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 13);
            this.label6.TabIndex = 79;
            this.label6.Text = "Raw and subsidiary materials Summary Time :";
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(232, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 78;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(429, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 17);
            this.label5.TabIndex = 82;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(440, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 81;
            this.label8.Text = "Date";
            // 
            // label9
            // 
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(617, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(6, 17);
            this.label9.TabIndex = 83;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(629, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 84;
            this.label10.Text = "Material code";
            // 
            // ckbDetail
            // 
            this.ckbDetail.AutoSize = true;
            this.ckbDetail.Location = new System.Drawing.Point(235, 4);
            this.ckbDetail.Name = "ckbDetail";
            this.ckbDetail.Size = new System.Drawing.Size(77, 17);
            this.ckbDetail.TabIndex = 80;
            this.ckbDetail.Text = "Detailed inquiry";
            this.ckbDetail.UseVisualStyleBackColor = true;
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Checked = true;
            this.ckbKpcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbKpcs.Location = new System.Drawing.Point(323, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 81;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // ckbOverShort
            // 
            this.ckbOverShort.AutoSize = true;
            this.ckbOverShort.Location = new System.Drawing.Point(381, 4);
            this.ckbOverShort.Name = "ckbOverShort";
            this.ckbOverShort.Size = new System.Drawing.Size(89, 17);
            this.ckbOverShort.TabIndex = 83;
            this.ckbOverShort.Text = "Over and Under Inquiry";
            this.ckbOverShort.UseVisualStyleBackColor = true;
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(481, 2);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(103, 20);
            this.cdvDate.TabIndex = 84;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2012, 5, 14, 0, 0, 0, 0);
            // 
            // label11
            // 
            this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(616, 4);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 17);
            this.label11.TabIndex = 86;
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(627, 5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(55, 13);
            this.label12.TabIndex = 85;
            this.label12.Text = "Display standard";
            // 
            // rdbMonth
            // 
            this.rdbMonth.AutoSize = true;
            this.rdbMonth.Location = new System.Drawing.Point(724, 4);
            this.rdbMonth.Name = "rdbMonth";
            this.rdbMonth.Size = new System.Drawing.Size(37, 17);
            this.rdbMonth.TabIndex = 88;
            this.rdbMonth.Text = "월";
            this.rdbMonth.UseVisualStyleBackColor = true;
            // 
            // rdbWeek
            // 
            this.rdbWeek.AutoSize = true;
            this.rdbWeek.Checked = true;
            this.rdbWeek.Location = new System.Drawing.Point(684, 4);
            this.rdbWeek.Name = "rdbWeek";
            this.rdbWeek.Size = new System.Drawing.Size(37, 17);
            this.rdbWeek.TabIndex = 87;
            this.rdbWeek.TabStop = true;
            this.rdbWeek.Text = "Week";
            this.rdbWeek.UseVisualStyleBackColor = true;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Enabled = false;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(19, 1);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";            
            this.cdvFactory.Size = new System.Drawing.Size(187, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 17;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(18, 6);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(389, 13);
            this.label13.TabIndex = 85;
            this.label13.Text = "* If you click on the remaining order amount, you can check the warehousing plan and performance.";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(18, 6);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(454, 13);
            this.label14.TabIndex = 86;
            this.label14.Text = "* You can check the expiration date by clicking on the status. (WIP)";
            // 
            // MAT070503
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 5;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "MAT070503";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition5.ResumeLayout(false);
            this.pnlCondition5.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvMatType;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.TextBox txtMatCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblUpdateTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.CheckBox ckbDetail;
        private System.Windows.Forms.CheckBox ckbOverShort;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rdbMonth;
        private System.Windows.Forms.RadioButton rdbWeek;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}
