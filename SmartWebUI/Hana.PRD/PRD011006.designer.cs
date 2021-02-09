namespace Hana.PRD
{
    partial class PRD011006
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD011006));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.lblDate = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbRev = new System.Windows.Forms.CheckBox();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.lbl = new System.Windows.Forms.Label();
            this.lblLabel3 = new System.Windows.Forms.Label();
            this.lblLabel9 = new System.Windows.Forms.Label();
            this.lblLabel7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rdbSales = new System.Windows.Forms.RadioButton();
            this.rdbQuantity = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lblJindo = new System.Windows.Forms.Label();
            this.lblRemain = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblLastDay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.cdvLotType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblNumericSum = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLotType = new System.Windows.Forms.TextBox();
            this.cdvType = new System.Windows.Forms.ComboBox();
            this.rdbTurnkey = new System.Windows.Forms.RadioButton();
            this.pnlMiddle.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(104, 13);
            this.lblTitle.Text = "Daily production sale UI";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.cdvLotType);
            this.pnlCondition3.Controls.Add(this.lblLastDay);
            this.pnlCondition3.Controls.Add(this.lblToday);
            this.pnlCondition3.Controls.Add(this.lblRemain);
            this.pnlCondition3.Controls.Add(this.lblJindo);
            this.pnlCondition3.Controls.Add(this.ckbRev);
            this.pnlCondition3.Controls.Add(this.lbl);
            this.pnlCondition3.Controls.Add(this.lblLabel3);
            this.pnlCondition3.Controls.Add(this.lblLabel9);
            this.pnlCondition3.Controls.Add(this.lblLabel7);
            this.pnlCondition3.Controls.Add(this.label1);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvType);
            this.pnlCondition2.Controls.Add(this.lblNumericSum);
            this.pnlCondition2.Controls.Add(this.ckbKpcs);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.rdbTurnkey);
            this.pnlCondition2.Controls.Add(this.rdbSales);
            this.pnlCondition2.Controls.Add(this.rdbQuantity);
            this.pnlCondition2.Controls.Add(this.label4);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.label6);
            this.pnlCondition1.Controls.Add(this.lblProduct);
            this.pnlCondition1.Controls.Add(this.label7);
            this.pnlCondition1.Controls.Add(this.txtSearchProduct);
            this.pnlCondition1.Controls.Add(this.txtLotType);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
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
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 179);
            this.pnlMain.Size = new System.Drawing.Size(800, 421);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
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
            this.spdData.Size = new System.Drawing.Size(794, 418);
            this.spdData.TabIndex = 1;
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
            this.cdvFactory.Location = new System.Drawing.Point(4, 0);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";            
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 10;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(285, 1);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 16;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(217, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 13);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "standard date";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(207, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 18;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "단위 : PKG (pcs), COB (wafer)";
            // 
            // ckbRev
            // 
            this.ckbRev.AutoSize = true;
            this.ckbRev.Checked = true;
            this.ckbRev.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRev.Location = new System.Drawing.Point(737, 4);
            this.ckbRev.Name = "ckbRev";
            this.ckbRev.Size = new System.Drawing.Size(45, 17);
            this.ckbRev.TabIndex = 106;
            this.ckbRev.Text = "Rev";
            this.ckbRev.UseVisualStyleBackColor = true;
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Checked = true;
            this.ckbKpcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbKpcs.Location = new System.Drawing.Point(331, 5);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 105;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(154, 5);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(42, 13);
            this.lbl.TabIndex = 104;
            this.lbl.Text = "Remain";
            // 
            // lblLabel3
            // 
            this.lblLabel3.AutoSize = true;
            this.lblLabel3.Location = new System.Drawing.Point(16, 6);
            this.lblLabel3.Name = "lblLabel3";
            this.lblLabel3.Size = new System.Drawing.Size(35, 13);
            this.lblLabel3.TabIndex = 101;
            this.lblLabel3.Text = "today";
            // 
            // lblLabel9
            // 
            this.lblLabel9.AutoSize = true;
            this.lblLabel9.Location = new System.Drawing.Point(83, 6);
            this.lblLabel9.Name = "lblLabel9";
            this.lblLabel9.Size = new System.Drawing.Size(48, 13);
            this.lblLabel9.TabIndex = 102;
            this.lblLabel9.Text = "workday";
            // 
            // lblLabel7
            // 
            this.lblLabel7.AutoSize = true;
            this.lblLabel7.Location = new System.Drawing.Point(232, 6);
            this.lblLabel7.Name = "lblLabel7";
            this.lblLabel7.Size = new System.Drawing.Size(67, 13);
            this.lblLabel7.TabIndex = 103;
            this.lblLabel7.Text = "Standard progress rate";
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(7, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 93;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdbSales
            // 
            this.rdbSales.AutoSize = true;
            this.rdbSales.Location = new System.Drawing.Point(147, 5);
            this.rdbSales.Name = "rdbSales";
            this.rdbSales.Size = new System.Drawing.Size(61, 17);
            this.rdbSales.TabIndex = 91;
            this.rdbSales.Text = "Sales Price";
            this.rdbSales.UseVisualStyleBackColor = true;
            // 
            // rdbQuantity
            // 
            this.rdbQuantity.AutoSize = true;
            this.rdbQuantity.Checked = true;
            this.rdbQuantity.Location = new System.Drawing.Point(87, 5);
            this.rdbQuantity.Name = "rdbQuantity";
            this.rdbQuantity.Size = new System.Drawing.Size(49, 17);
            this.rdbQuantity.TabIndex = 90;
            this.rdbQuantity.TabStop = true;
            this.rdbQuantity.Text = "quantity";
            this.rdbQuantity.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 92;
            this.label4.Text = "Display standard";
            // 
            // lblJindo
            // 
            this.lblJindo.AutoSize = true;
            this.lblJindo.Location = new System.Drawing.Point(399, 6);
            this.lblJindo.Name = "lblJindo";
            this.lblJindo.Size = new System.Drawing.Size(0, 13);
            this.lblJindo.TabIndex = 110;
            // 
            // lblRemain
            // 
            this.lblRemain.AutoSize = true;
            this.lblRemain.Location = new System.Drawing.Point(407, 14);
            this.lblRemain.Name = "lblRemain";
            this.lblRemain.Size = new System.Drawing.Size(0, 13);
            this.lblRemain.TabIndex = 111;
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(0, 0);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(0, 13);
            this.lblToday.TabIndex = 112;
            // 
            // lblLastDay
            // 
            this.lblLastDay.AutoSize = true;
            this.lblLastDay.Location = new System.Drawing.Point(8, 8);
            this.lblLastDay.Name = "lblLastDay";
            this.lblLastDay.Size = new System.Drawing.Size(0, 13);
            this.lblLastDay.TabIndex = 113;
            // 
            // label2
            // 
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(440, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 17);
            this.label2.TabIndex = 112;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(456, 5);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 111;
            this.lblProduct.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(511, 1);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(94, 21);
            this.txtSearchProduct.TabIndex = 110;
            this.txtSearchProduct.Text = "%";
            // 
            // cdvLotType
            // 
            this.cdvLotType.BackColor = System.Drawing.Color.Transparent;
            this.cdvLotType.bMultiSelect = true;
            this.cdvLotType.ConditionText = "Lot Type";
            this.cdvLotType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvLotType.ControlRef = true;
            this.cdvLotType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLotType.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvLotType.Location = new System.Drawing.Point(309, 2);
            this.cdvLotType.MandatoryFlag = false;
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.ParentValue = "";
            this.cdvLotType.sCodeColumnName = "";
            this.cdvLotType.sDynamicQuery = "";           
            this.cdvLotType.Size = new System.Drawing.Size(180, 21);
            this.cdvLotType.sTableName = "H_TYPE";
            this.cdvLotType.sValueColumnName = "";
            this.cdvLotType.TabIndex = 114;
            this.cdvLotType.VisibleValueButton = true;
            // 
            // lblNumericSum
            // 
            this.lblNumericSum.AutoSize = true;
            this.lblNumericSum.Location = new System.Drawing.Point(709, 7);
            this.lblNumericSum.Name = "lblNumericSum";
            this.lblNumericSum.Size = new System.Drawing.Size(75, 13);
            this.lblNumericSum.TabIndex = 128;
            this.lblNumericSum.Text = "lblNumericSum";
            // 
            // label6
            // 
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(618, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(6, 17);
            this.label6.TabIndex = 131;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(633, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 130;
            this.label7.Text = "Lot Type";
            this.label7.Visible = false;
            // 
            // txtLotType
            // 
            this.txtLotType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLotType.Location = new System.Drawing.Point(690, 2);
            this.txtLotType.Name = "txtLotType";
            this.txtLotType.Size = new System.Drawing.Size(94, 21);
            this.txtLotType.TabIndex = 129;
            this.txtLotType.Text = "P%";
            this.txtLotType.Visible = false;
            // 
            // cdvType
            // 
            this.cdvType.FormattingEnabled = true;
            this.cdvType.Items.AddRange(new object[] {
            "basics",
            "Stack Product",
            "Detail"});
            this.cdvType.Location = new System.Drawing.Point(405, 2);
            this.cdvType.Name = "cdvType";
            this.cdvType.Size = new System.Drawing.Size(98, 21);
            this.cdvType.TabIndex = 132;
            this.cdvType.Text = "기본";
            // 
            // rdbTurnkey
            // 
            this.rdbTurnkey.AutoSize = true;
            this.rdbTurnkey.Location = new System.Drawing.Point(211, 5);
            this.rdbTurnkey.Name = "rdbTurnkey";
            this.rdbTurnkey.Size = new System.Drawing.Size(106, 17);
            this.rdbTurnkey.TabIndex = 91;
            this.rdbTurnkey.Text = "Sales (turnkey)";
            this.rdbTurnkey.UseVisualStyleBackColor = true;
            // 
            // PRD011006
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD011006";
            this.pnlMiddle.ResumeLayout(false);
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
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbRev;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label lblLabel3;
        private System.Windows.Forms.Label lblLabel9;
        private System.Windows.Forms.Label lblLabel7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbSales;
        private System.Windows.Forms.RadioButton rdbQuantity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblRemain;
        private System.Windows.Forms.Label lblJindo;
        private System.Windows.Forms.Label lblLastDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLotType;
        private System.Windows.Forms.Label lblNumericSum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLotType;
        private System.Windows.Forms.ComboBox cdvType;
        private System.Windows.Forms.RadioButton rdbTurnkey;
    }
}
