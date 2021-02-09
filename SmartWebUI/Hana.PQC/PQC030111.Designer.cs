namespace Hana.RAS
{
    partial class PQC030111
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
            SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030111));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcPkgType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboChart = new System.Windows.Forms.ComboBox();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udcQcType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(123, 13);
            this.lblTitle.Text = "ASSY Operation Capability";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.AccessibleDescription = "";
            this.pnlMiddle.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 188);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 164);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 140);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 116);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 92);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 62);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.cboChart);
            this.pnlCondition2.Controls.Add(this.udcFromToDate);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.udcQcType);
            this.pnlCondition1.Controls.Add(this.udcPkgType);
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
            // pnlDetailCondition1
            // 
            this.pnlDetailCondition1.AutoSize = true;
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
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.pnlMain.Location = new System.Drawing.Point(0, 152);
            this.pnlMain.Size = new System.Drawing.Size(800, 448);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // btnView
            // 
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.udcChartFX1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spdData);
            this.splitContainer1.Size = new System.Drawing.Size(794, 445);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 0;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Scrollable = true;
            this.udcChartFX1.Size = new System.Drawing.Size(794, 235);
            this.udcChartFX1.TabIndex = 24;
            this.udcChartFX1.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
            this.udcChartFX1.ToolBar = true;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(0, 0);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 206);
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
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition1;
            this.cdvFactory.Location = new System.Drawing.Point(4, 0);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 57;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // udcPkgType
            // 
            this.udcPkgType.BackColor = System.Drawing.Color.Transparent;
            this.udcPkgType.bMultiSelect = true;
            this.udcPkgType.ConditionText = "PKG Type";
            this.udcPkgType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcPkgType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcPkgType.Location = new System.Drawing.Point(204, 0);
            this.udcPkgType.MandatoryFlag = false;
            this.udcPkgType.Name = "udcPkgType";
            this.udcPkgType.ParentValue = "";
            this.udcPkgType.sCodeColumnName = "KEY_1";
            this.udcPkgType.sDynamicQuery = "";            
            this.udcPkgType.Size = new System.Drawing.Size(180, 21);
            this.udcPkgType.sTableName = "H_PKG_TYPE";
            this.udcPkgType.sValueColumnName = "DA";
            this.udcPkgType.TabIndex = 59;
            this.udcPkgType.VisibleValueButton = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(407, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 70;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(420, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Graph";
            // 
            // cboChart
            // 
            this.cboChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboChart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChart.FormattingEnabled = true;
            this.cboChart.Items.AddRange(new object[] {
            "View all",
            "Items Cpk per Cycle",
            "Cpk by item"});
            this.cboChart.Location = new System.Drawing.Point(484, 6);
            this.cboChart.Name = "cboChart";
            this.cboChart.Size = new System.Drawing.Size(165, 21);
            this.cboChart.TabIndex = 68;
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(83, 6);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(275, 22);
            this.udcFromToDate.TabIndex = 67;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(8, 6);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 66;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Date";
            // 
            // udcQcType
            // 
            this.udcQcType.BackColor = System.Drawing.Color.Transparent;
            this.udcQcType.bMultiSelect = true;
            this.udcQcType.ConditionText = "Gubun";
            this.udcQcType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcQcType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcQcType.Location = new System.Drawing.Point(404, 0);
            this.udcQcType.MandatoryFlag = false;
            this.udcQcType.Name = "udcQcType";
            this.udcQcType.ParentValue = "";
            this.udcQcType.sCodeColumnName = "QC_TYPE";
            this.udcQcType.sDynamicQuery = "SELECT DISTINCT QC_TYPE , \' \'  AS sValue FROM CPQCLOTHIS@RPTTOMES WHERE HIST_DEL_" +
                "FLAG <>\'Y\' ";
            this.udcQcType.sFactory = "";
            this.udcQcType.Size = new System.Drawing.Size(180, 21);
            this.udcQcType.sTableName = "";
            this.udcQcType.sValueColumnName = "sValue";
            this.udcQcType.TabIndex = 75;
            this.udcQcType.VisibleValueButton = true;
            // 
            // PQC030111
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030111";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
            this.pnlCondition1.ResumeLayout(false);
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlWIPDetail.PerformLayout();
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;        
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcPkgType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboChart;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcQcType;
    }
}
