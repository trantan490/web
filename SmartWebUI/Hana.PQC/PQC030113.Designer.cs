namespace Hana.RAS
{
    partial class PQC030113
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030113));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCustomer = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboChart = new System.Windows.Forms.ComboBox();
            this.udcVendor = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcLotType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcPkgType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(176, 13);
            this.lblTitle.Text = "Plating import inspection status (history) inquiry";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 179);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 155);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 131);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 107);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 83);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.cdvFactory);
            this.pnlCondition3.Location = new System.Drawing.Point(0, 59);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.udcFromToDate);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.cboChart);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 27);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.udcPkgType);
            this.pnlCondition1.Controls.Add(this.udcLotType);
            this.pnlCondition1.Controls.Add(this.udcVendor);
            this.pnlCondition1.Controls.Add(this.udcCustomer);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 152);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Controls.Add(this.udcChartFX1);
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Size = new System.Drawing.Size(800, 508);
            // 
            // btnDetail
            // 
            this.btnDetail.Enabled = false;
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
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spdData.Location = new System.Drawing.Point(3, 229);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 279);
            this.spdData.TabIndex = 25;
            this.spdData.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
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
            // udcChartFX1
            // 
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Top;
            this.udcChartFX1.Location = new System.Drawing.Point(3, 3);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Size = new System.Drawing.Size(794, 226);
            this.udcChartFX1.TabIndex = 26;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(603, 3);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 14;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // udcCustomer
            // 
            this.udcCustomer.BackColor = System.Drawing.Color.Transparent;
            this.udcCustomer.bMultiSelect = true;
            this.udcCustomer.ConditionText = "Customer";
            this.udcCustomer.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCustomer.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCustomer.Location = new System.Drawing.Point(4, 3);
            this.udcCustomer.MandatoryFlag = false;
            this.udcCustomer.Name = "udcCustomer";
            this.udcCustomer.ParentValue = "";
            this.udcCustomer.sCodeColumnName = "KEY_1";
            this.udcCustomer.sDynamicQuery = "";           
            this.udcCustomer.Size = new System.Drawing.Size(180, 21);
            this.udcCustomer.sTableName = "H_CUSTOMER";
            this.udcCustomer.sValueColumnName = "DATA_1";
            this.udcCustomer.TabIndex = 51;
            this.udcCustomer.VisibleValueButton = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(405, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 67;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(418, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 66;
            this.label5.Text = "Graph";
            // 
            // cboChart
            // 
            this.cboChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChart.FormattingEnabled = true;
            this.cboChart.Items.AddRange(new object[] {
            "Judgment status",
            "Inspection status (company)",
            "Inspection status (PKG)",
            "Judgment ratio by PKG",
            "Defect rate by company",
            "Status by defect type",
            "Percentage by defect type"});
            this.cboChart.Location = new System.Drawing.Point(482, 5);
            this.cboChart.Name = "cboChart";
            this.cboChart.Size = new System.Drawing.Size(101, 21);
            this.cboChart.TabIndex = 65;
            // 
            // udcVendor
            // 
            this.udcVendor.BackColor = System.Drawing.Color.Transparent;
            this.udcVendor.bMultiSelect = true;
            this.udcVendor.ConditionText = "Use department";
            this.udcVendor.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcVendor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcVendor.Location = new System.Drawing.Point(203, 3);
            this.udcVendor.MandatoryFlag = false;
            this.udcVendor.Name = "udcVendor";
            this.udcVendor.ParentValue = "";
            this.udcVendor.sCodeColumnName = "KEY_1";
            this.udcVendor.sDynamicQuery = "";            
            this.udcVendor.Size = new System.Drawing.Size(180, 21);
            this.udcVendor.sTableName = "H_DEPARTMENT";
            this.udcVendor.sValueColumnName = "DATA_1";
            this.udcVendor.TabIndex = 61;
            this.udcVendor.VisibleValueButton = true;
            // 
            // udcLotType
            // 
            this.udcLotType.BackColor = System.Drawing.Color.Transparent;
            this.udcLotType.bMultiSelect = true;
            this.udcLotType.ConditionText = "Lot Division";
            this.udcLotType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcLotType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcLotType.Location = new System.Drawing.Point(403, 3);
            this.udcLotType.MandatoryFlag = false;
            this.udcLotType.Name = "udcLotType";
            this.udcLotType.ParentValue = "";
            this.udcLotType.sCodeColumnName = "KEY_1";
            this.udcLotType.sDynamicQuery = "";            
            this.udcLotType.Size = new System.Drawing.Size(180, 21);
            this.udcLotType.sTableName = "QC_REQ_TYPE";
            this.udcLotType.sValueColumnName = "DATA_1";
            this.udcLotType.TabIndex = 62;
            this.udcLotType.VisibleValueButton = true;
            // 
            // udcPkgType
            // 
            this.udcPkgType.BackColor = System.Drawing.Color.Transparent;
            this.udcPkgType.bMultiSelect = true;
            this.udcPkgType.ConditionText = "PKG Type";
            this.udcPkgType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcPkgType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcPkgType.Location = new System.Drawing.Point(603, 3);
            this.udcPkgType.MandatoryFlag = false;
            this.udcPkgType.Name = "udcPkgType";
            this.udcPkgType.ParentValue = "";
            this.udcPkgType.sCodeColumnName = "KEY_1";
            this.udcPkgType.sDynamicQuery = "";            
            this.udcPkgType.Size = new System.Drawing.Size(180, 21);
            this.udcPkgType.sTableName = "H_PKG_TYPE";
            this.udcPkgType.sValueColumnName = "DA";
            this.udcPkgType.TabIndex = 63;
            this.udcPkgType.VisibleValueButton = true;
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(84, 8);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(275, 21);
            this.udcFromToDate.TabIndex = 70;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 9);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 69;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Inspection period";
            // 
            // PQC030113
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030113";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
            this.pnlCondition1.ResumeLayout(false);
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCustomer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboChart;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcPkgType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcLotType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcVendor;
    }
}
