namespace Hana.PRD
{
    partial class PRD010305
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010305));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcDateTimePicker2 = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvStep = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvHoldCode = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.rbtOper = new System.Windows.Forms.RadioButton();
            this.rbtCreate = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cdvHoldKind = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHoldDate = new System.Windows.Forms.Label();
            this.txtHoldDate = new System.Windows.Forms.TextBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.rbtIssue = new System.Windows.Forms.RadioButton();
            this.ckbTime = new System.Windows.Forms.CheckBox();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbMulti = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(119, 13);
            this.lblTitle.Text = "stagnation, HOLD LOT inquiry";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.ckbMulti);
            this.pnlCondition3.Controls.Add(this.ckbTime);
            this.pnlCondition3.Controls.Add(this.rbtIssue);
            this.pnlCondition3.Controls.Add(this.rbtCreate);
            this.pnlCondition3.Controls.Add(this.rbtOper);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvStep);
            this.pnlCondition2.Controls.Add(this.cdvHoldCode);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.lblHoldDate);
            this.pnlCondition2.Controls.Add(this.txtHoldDate);
            this.pnlCondition2.Controls.Add(this.cdvHoldKind);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.label6);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label7);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.lblProduct);
            this.pnlCondition1.Controls.Add(this.txtSearchProduct);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 116);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 203);
            this.pnlMain.Size = new System.Drawing.Size(800, 397);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
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
            this.spdData.Location = new System.Drawing.Point(3, 3);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 394);
            this.spdData.TabIndex = 0;
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
            // udcDateTimePicker2
            // 
            this.udcDateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.udcDateTimePicker2.Font = new System.Drawing.Font("Verdana", 8F);
            this.udcDateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.udcDateTimePicker2.Location = new System.Drawing.Point(21, 62);
            this.udcDateTimePicker2.Name = "udcDateTimePicker2";
            this.udcDateTimePicker2.Size = new System.Drawing.Size(163, 20);
            this.udcDateTimePicker2.TabIndex = 0;
            this.udcDateTimePicker2.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.udcDateTimePicker2.Value = new System.DateTime(2008, 12, 2, 0, 0, 0, 0);
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
            this.cdvFactory.ForcInitControl2 = this.cdvStep;
            this.cdvFactory.ForcInitControl3 = this.cdvHoldCode;
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
            this.cdvFactory.TabIndex = 22;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvStep
            // 
            this.cdvStep.BackColor = System.Drawing.Color.Transparent;
            this.cdvStep.bMultiSelect = true;
            this.cdvStep.ConditionText = "Operation";
            this.cdvStep.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.cdvStep.ControlRef = true;
            this.cdvStep.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvStep.Location = new System.Drawing.Point(4, 3);
            this.cdvStep.MandatoryFlag = false;
            this.cdvStep.Name = "cdvStep";
            this.cdvStep.ParentValue = "";
            this.cdvStep.RefFactory = this.cdvFactory;
            this.cdvStep.sCodeColumnName = "";
            this.cdvStep.sDynamicQuery = "";
            this.cdvStep.sFactory = "";
            this.cdvStep.Size = new System.Drawing.Size(180, 21);
            this.cdvStep.sTableName = "";
            this.cdvStep.sValueColumnName = "";
            this.cdvStep.TabIndex = 43;
            this.cdvStep.VisibleValueButton = true;
            // 
            // cdvHoldCode
            // 
            this.cdvHoldCode.BackColor = System.Drawing.Color.Transparent;
            this.cdvHoldCode.bMultiSelect = true;
            this.cdvHoldCode.ConditionText = "Hold Code";
            this.cdvHoldCode.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.CUSTOM;
            this.cdvHoldCode.ControlRef = true;
            this.cdvHoldCode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvHoldCode.Location = new System.Drawing.Point(604, 3);
            this.cdvHoldCode.MandatoryFlag = false;
            this.cdvHoldCode.Name = "cdvHoldCode";
            this.cdvHoldCode.ParentValue = "";
            this.cdvHoldCode.RefFactory = this.cdvFactory;
            this.cdvHoldCode.sCodeColumnName = "";
            this.cdvHoldCode.sDynamicQuery = "";
            this.cdvHoldCode.sFactory = "";
            this.cdvHoldCode.Size = new System.Drawing.Size(180, 21);
            this.cdvHoldCode.sTableName = "HOLD_CODE";
            this.cdvHoldCode.sValueColumnName = "";
            this.cdvHoldCode.TabIndex = 43;
            this.cdvHoldCode.VisibleValueButton = true;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(418, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 25;
            this.lblProduct.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(484, 3);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(100, 21);
            this.txtSearchProduct.TabIndex = 24;
            this.txtSearchProduct.Text = "%";
            // 
            // rbtOper
            // 
            this.rbtOper.AutoSize = true;
            this.rbtOper.Location = new System.Drawing.Point(197, 4);
            this.rbtOper.Name = "rbtOper";
            this.rbtOper.Size = new System.Drawing.Size(85, 17);
            this.rbtOper.TabIndex = 31;
            this.rbtOper.Text = "Operation date";
            this.rbtOper.UseVisualStyleBackColor = true;
            // 
            // rbtCreate
            // 
            this.rbtCreate.AutoSize = true;
            this.rbtCreate.Checked = true;
            this.rbtCreate.Location = new System.Drawing.Point(10, 4);
            this.rbtCreate.Name = "rbtCreate";
            this.rbtCreate.Size = new System.Drawing.Size(85, 17);
            this.rbtCreate.TabIndex = 30;
            this.rbtCreate.TabStop = true;
            this.rbtCreate.Text = "As of our daily basis";
            this.rbtCreate.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(408, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 31;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(207, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 35;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 34;
            this.label6.Text = "Hold Kind";
            // 
            // cdvHoldKind
            // 
            this.cdvHoldKind.FormattingEnabled = true;
            this.cdvHoldKind.Items.AddRange(new object[] {
            "ALL",
            "Hold",
            "Non Hold"});
            this.cdvHoldKind.Location = new System.Drawing.Point(284, 3);
            this.cdvHoldKind.Name = "cdvHoldKind";
            this.cdvHoldKind.Size = new System.Drawing.Size(100, 21);
            this.cdvHoldKind.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(408, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 42;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblHoldDate
            // 
            this.lblHoldDate.AutoSize = true;
            this.lblHoldDate.Location = new System.Drawing.Point(417, 7);
            this.lblHoldDate.Name = "lblHoldDate";
            this.lblHoldDate.Size = new System.Drawing.Size(43, 13);
            this.lblHoldDate.TabIndex = 41;
            this.lblHoldDate.Text = "Stagnant days";
            // 
            // txtHoldDate
            // 
            this.txtHoldDate.Location = new System.Drawing.Point(484, 3);
            this.txtHoldDate.MaxLength = 5;
            this.txtHoldDate.Name = "txtHoldDate";
            this.txtHoldDate.Size = new System.Drawing.Size(100, 21);
            this.txtHoldDate.TabIndex = 40;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(607, 3);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 42;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(616, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 41;
            this.label2.Text = "Date";
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(684, 2);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(100, 20);
            this.cdvDate.TabIndex = 40;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 2, 0, 0, 0, 0);
            // 
            // rbtIssue
            // 
            this.rbtIssue.AutoSize = true;
            this.rbtIssue.Location = new System.Drawing.Point(101, 4);
            this.rbtIssue.Name = "rbtIssue";
            this.rbtIssue.Size = new System.Drawing.Size(90, 17);
            this.rbtIssue.TabIndex = 44;
            this.rbtIssue.Text = "Standard issue days";
            this.rbtIssue.UseVisualStyleBackColor = true;
            // 
            // ckbTime
            // 
            this.ckbTime.AutoSize = true;
            this.ckbTime.Location = new System.Drawing.Point(404, 5);
            this.ckbTime.Name = "ckbTime";
            this.ckbTime.Size = new System.Drawing.Size(74, 17);
            this.ckbTime.TabIndex = 45;
            this.ckbTime.Text = "Based on time";
            this.ckbTime.UseVisualStyleBackColor = true;
            this.ckbTime.CheckedChanged += new System.EventHandler(this.ckbTime_CheckedChanged);
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
            this.cdvLotType.TabIndex = 46;
            this.cdvLotType.Text = "ALL";
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(207, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 45;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(219, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Lot Type";
            // 
            // ckbMulti
            // 
            this.ckbMulti.AutoSize = true;
            this.ckbMulti.Location = new System.Drawing.Point(494, 5);
            this.ckbMulti.Name = "ckbMulti";
            this.ckbMulti.Size = new System.Drawing.Size(116, 17);
            this.ckbMulti.TabIndex = 45;
            this.ckbMulti.Text = "Except W / B Multi Lot";
            this.ckbMulti.UseVisualStyleBackColor = true;
            this.ckbMulti.CheckedChanged += new System.EventHandler(this.ckbTime_CheckedChanged);
            // 
            // PRD010305
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010305";
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker udcDateTimePicker2;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.RadioButton rbtOper;
        private System.Windows.Forms.RadioButton rbtCreate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cdvHoldKind;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHoldDate;
        private System.Windows.Forms.TextBox txtHoldDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStep;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvHoldCode;
        private System.Windows.Forms.RadioButton rbtIssue;
        private System.Windows.Forms.CheckBox ckbTime;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbMulti;

    }
}
