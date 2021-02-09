namespace Hana.RAS
{
    partial class PQC030108
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030108));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcItem = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udcPkgType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcVendor = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCurrentTestLevel = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.udcMatSize = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcLeadCount = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChart = new System.Windows.Forms.ComboBox();
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
            this.lblTitle.Size = new System.Drawing.Size(140, 13);
            this.lblTitle.Text = "Import Inspection Sampling Level";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
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
            this.pnlCondition4.Location = new System.Drawing.Point(0, 86);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.label1);
            this.pnlCondition3.Controls.Add(this.label3);
            this.pnlCondition3.Controls.Add(this.cboChart);
            this.pnlCondition3.Controls.Add(this.cdvFactory);
            this.pnlCondition3.Location = new System.Drawing.Point(0, 59);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 27);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.udcMatSize);
            this.pnlCondition2.Controls.Add(this.udcFromToDate);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.udcCurrentTestLevel);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 27);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.udcLeadCount);
            this.pnlCondition1.Controls.Add(this.udcVendor);
            this.pnlCondition1.Controls.Add(this.udcPkgType);
            this.pnlCondition1.Controls.Add(this.udcItem);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 176);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Controls.Add(this.udcChartFX1);
            this.pnlMain.Location = new System.Drawing.Point(0, 116);
            this.pnlMain.Size = new System.Drawing.Size(800, 484);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
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
            this.spdData.Size = new System.Drawing.Size(794, 255);
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
            this.cdvFactory.Location = new System.Drawing.Point(606, 3);
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
            // udcItem
            // 
            this.udcItem.BackColor = System.Drawing.Color.Transparent;
            this.udcItem.bMultiSelect = true;
            this.udcItem.ConditionText = "Item";
            this.udcItem.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcItem.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcItem.Location = new System.Drawing.Point(4, 2);
            this.udcItem.MandatoryFlag = false;
            this.udcItem.Name = "udcItem";
            this.udcItem.ParentValue = "";
            this.udcItem.sCodeColumnName = "KEY_1";
            this.udcItem.sDynamicQuery = "";          
            this.udcItem.Size = new System.Drawing.Size(180, 21);
            this.udcItem.sTableName = "MATERIAL_TYPE";
            this.udcItem.sValueColumnName = "DATA_1";
            this.udcItem.TabIndex = 49;
            this.udcItem.VisibleValueButton = true;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(405, 9);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 50;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 49;
            this.label2.Text = "Date";
            // 
            // udcPkgType
            // 
            this.udcPkgType.BackColor = System.Drawing.Color.Transparent;
            this.udcPkgType.bMultiSelect = true;
            this.udcPkgType.ConditionText = "PKG Type";
            this.udcPkgType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcPkgType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcPkgType.Location = new System.Drawing.Point(402, 3);
            this.udcPkgType.MandatoryFlag = false;
            this.udcPkgType.Name = "udcPkgType";
            this.udcPkgType.ParentValue = "";
            this.udcPkgType.sCodeColumnName = "KEY_1";
            this.udcPkgType.sDynamicQuery = "";            
            this.udcPkgType.Size = new System.Drawing.Size(180, 21);
            this.udcPkgType.sTableName = "H_PKG_TYPE";
            this.udcPkgType.sValueColumnName = "DA";
            this.udcPkgType.TabIndex = 50;
            this.udcPkgType.VisibleValueButton = true;
            // 
            // udcVendor
            // 
            this.udcVendor.BackColor = System.Drawing.Color.Transparent;
            this.udcVendor.bMultiSelect = true;
            this.udcVendor.ConditionText = "Vendor";
            this.udcVendor.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcVendor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcVendor.Location = new System.Drawing.Point(203, 2);
            this.udcVendor.MandatoryFlag = false;
            this.udcVendor.Name = "udcVendor";
            this.udcVendor.ParentValue = "";
            this.udcVendor.sCodeColumnName = "KEY_1";
            this.udcVendor.sDynamicQuery = "";            
            this.udcVendor.Size = new System.Drawing.Size(180, 21);
            this.udcVendor.sTableName = "H_VENDOR";
            this.udcVendor.sValueColumnName = "DATA_1";
            this.udcVendor.TabIndex = 51;
            this.udcVendor.VisibleValueButton = true;
            // 
            // udcCurrentTestLevel
            // 
            this.udcCurrentTestLevel.BackColor = System.Drawing.Color.Transparent;
            this.udcCurrentTestLevel.bMultiSelect = true;
            this.udcCurrentTestLevel.ConditionText = "Inspection level";
            this.udcCurrentTestLevel.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCurrentTestLevel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCurrentTestLevel.Location = new System.Drawing.Point(203, 6);
            this.udcCurrentTestLevel.MandatoryFlag = false;
            this.udcCurrentTestLevel.Name = "udcCurrentTestLevel";
            this.udcCurrentTestLevel.ParentValue = "";
            this.udcCurrentTestLevel.sCodeColumnName = "KEY_1";
            this.udcCurrentTestLevel.sDynamicQuery = "";            
            this.udcCurrentTestLevel.Size = new System.Drawing.Size(180, 21);
            this.udcCurrentTestLevel.sTableName = "QC_LEVEL";
            this.udcCurrentTestLevel.sValueColumnName = "BLANK";
            this.udcCurrentTestLevel.TabIndex = 53;
            this.udcCurrentTestLevel.VisibleValueButton = true;
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(482, 6);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(275, 21);
            this.udcFromToDate.TabIndex = 54;
            // 
            // udcMatSize
            // 
            this.udcMatSize.BackColor = System.Drawing.Color.Transparent;
            this.udcMatSize.bMultiSelect = true;
            this.udcMatSize.ConditionText = "Standard";
            this.udcMatSize.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcMatSize.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcMatSize.Location = new System.Drawing.Point(4, 6);
            this.udcMatSize.MandatoryFlag = false;
            this.udcMatSize.Name = "udcMatSize";
            this.udcMatSize.ParentValue = "";
            this.udcMatSize.sCodeColumnName = "KEY_1";
            this.udcMatSize.sDynamicQuery = "";            
            this.udcMatSize.Size = new System.Drawing.Size(180, 21);
            this.udcMatSize.sTableName = "BOM_SIZE";
            this.udcMatSize.sValueColumnName = "DATA_1";
            this.udcMatSize.TabIndex = 55;
            this.udcMatSize.VisibleValueButton = true;
            // 
            // udcLeadCount
            // 
            this.udcLeadCount.BackColor = System.Drawing.Color.Transparent;
            this.udcLeadCount.bMultiSelect = true;
            this.udcLeadCount.ConditionText = "Lead Count";
            this.udcLeadCount.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcLeadCount.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcLeadCount.Location = new System.Drawing.Point(606, 2);
            this.udcLeadCount.MandatoryFlag = false;
            this.udcLeadCount.Name = "udcLeadCount";
            this.udcLeadCount.ParentValue = "";
            this.udcLeadCount.sCodeColumnName = "KEY_1";
            this.udcLeadCount.sDynamicQuery = "";            
            this.udcLeadCount.Size = new System.Drawing.Size(180, 21);
            this.udcLeadCount.sTableName = "H_LEAD_COUNT";
            this.udcLeadCount.sValueColumnName = "DATA_1";
            this.udcLeadCount.TabIndex = 56;
            this.udcLeadCount.VisibleValueButton = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(8, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 55;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(21, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Graph";
            // 
            // cboChart
            // 
            this.cboChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChart.FormattingEnabled = true;
            this.cboChart.Items.AddRange(new object[] {
            "Inspection status by level",
            "Inspection level by product",
            "Percentage by Inspection Level",
            "Inspection level by product"});
            this.cboChart.Location = new System.Drawing.Point(83, 5);
            this.cboChart.Name = "cboChart";
            this.cboChart.Size = new System.Drawing.Size(101, 21);
            this.cboChart.TabIndex = 53;
            this.cboChart.SelectedIndexChanged += new System.EventHandler(this.cboChart_SelectedIndexChanged);
            // 
            // PQC030108
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030108";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
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
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCurrentTestLevel;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcVendor;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcPkgType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcItem;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcMatSize;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcLeadCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChart;
    }
}
