namespace Hana.PRD
{
    partial class PRD010313
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010313));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcDateTimePicker2 = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvStep = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvHoldCode = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.rbtHoldHis = new System.Windows.Forms.RadioButton();
            this.rbtHoldIn = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate2();
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
            this.pnlDetailCondition3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(66, 13);
            this.lblTitle.Text = "HOLD Inquiry";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvStep);
            this.pnlCondition2.Controls.Add(this.rbtHoldHis);
            this.pnlCondition2.Controls.Add(this.cdvHoldCode);
            this.pnlCondition2.Controls.Add(this.rbtHoldIn);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.label6);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.lblProduct);
            this.pnlCondition1.Controls.Add(this.txtSearchProduct);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
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
            this.spdData.Size = new System.Drawing.Size(794, 418);
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
            this.cdvHoldCode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvHoldCode.Location = new System.Drawing.Point(204, 2);
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
            this.cdvHoldCode.TabIndex = 23;
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
            // rbtHoldHis
            // 
            this.rbtHoldHis.AutoSize = true;
            this.rbtHoldHis.Location = new System.Drawing.Point(503, 6);
            this.rbtHoldHis.Name = "rbtHoldHis";
            this.rbtHoldHis.Size = new System.Drawing.Size(79, 17);
            this.rbtHoldHis.TabIndex = 31;
            this.rbtHoldHis.Text = "HOLD History";
            this.rbtHoldHis.UseVisualStyleBackColor = true;
            // 
            // rbtHoldIn
            // 
            this.rbtHoldIn.AutoSize = true;
            this.rbtHoldIn.Checked = true;
            this.rbtHoldIn.Location = new System.Drawing.Point(409, 6);
            this.rbtHoldIn.Name = "rbtHoldIn";
            this.rbtHoldIn.Size = new System.Drawing.Size(79, 17);
            this.rbtHoldIn.TabIndex = 30;
            this.rbtHoldIn.TabStop = true;
            this.rbtHoldIn.Text = "HOLD WIP";
            this.rbtHoldIn.UseVisualStyleBackColor = true;
            this.rbtHoldIn.CheckedChanged += new System.EventHandler(this.ckbHold_CheckChanged);
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
            this.cdvLotType.TabIndex = 45;
            this.cdvLotType.Text = "ALL";
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(207, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 44;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 43;
            this.label6.Text = "Lot Type";
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(604, 4);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(349, 21);
            this.cdvFromToDate.TabIndex = 46;
            // 
            // PRD010313
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010313";
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
        private System.Windows.Forms.RadioButton rbtHoldHis;
        private System.Windows.Forms.RadioButton rbtHoldIn;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStep;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvHoldCode;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate2 cdvFromToDate;

    }
}
