namespace Hana.Ras
{
    partial class PQC030105
    {
        /// <summary>
        /// �ʼ� �����̳� �����Դϴ�.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ��� ���� ��� ���ҽ��� �����մϴ�.
        /// </summary>
        /// <param name="disposing">�����Ǵ� ���ҽ��� �����ؾ� �ϸ� true�̰�, �׷��� ������ false�Դϴ�.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form �����̳ʿ��� ������ �ڵ�

        /// <summary>
        /// �����̳� ������ �ʿ��� �޼����Դϴ�.
        /// �� �޼����� ������ �ڵ� ������� �������� ���ʽÿ�.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030105));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvKind = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvResult = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvPkg = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFromTo = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboGraph = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cdvApproval = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(111, 13);
            this.lblTitle.Text = "Equipment Evaluation Status Inquiry";
            // 
            // pnlMiddle
            // 
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
            this.pnlCondition3.Size = new System.Drawing.Size(798, 36);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvApproval);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.cboGraph);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.cdvFromTo);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvPkg);
            this.pnlCondition1.Controls.Add(this.cdvKind);
            this.pnlCondition1.Controls.Add(this.cdvResult);
            this.pnlCondition1.Controls.Add(this.cdvOper);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 152);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer);
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Size = new System.Drawing.Size(800, 508);
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
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.udcChartFX1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.spdData);
            this.splitContainer.Size = new System.Drawing.Size(794, 505);
            this.splitContainer.SplitterDistance = 270;
            this.splitContainer.TabIndex = 7;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Scrollable = true;
            this.udcChartFX1.Size = new System.Drawing.Size(794, 270);
            this.udcChartFX1.TabIndex = 23;
            this.udcChartFX1.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
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
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 231);
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
            // cdvKind
            // 
            this.cdvKind.BackColor = System.Drawing.Color.Transparent;
            this.cdvKind.bMultiSelect = true;
            this.cdvKind.ConditionText = "Evaluation Type";
            this.cdvKind.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvKind.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvKind.Location = new System.Drawing.Point(4, 2);
            this.cdvKind.MandatoryFlag = false;
            this.cdvKind.Name = "cdvKind";
            this.cdvKind.ParentValue = "";
            this.cdvKind.sCodeColumnName = "KEY_1";
            this.cdvKind.sDynamicQuery = "";            
            this.cdvKind.Size = new System.Drawing.Size(180, 21);
            this.cdvKind.sTableName = "H_PARITY_CLASS";
            this.cdvKind.sValueColumnName = "DATA_1";
            this.cdvKind.TabIndex = 53;
            this.cdvKind.VisibleValueButton = true;
            // 
            // cdvResult
            // 
            this.cdvResult.BackColor = System.Drawing.Color.Transparent;
            this.cdvResult.bMultiSelect = true;
            this.cdvResult.ConditionText = "Evaluation results";
            this.cdvResult.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvResult.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvResult.Location = new System.Drawing.Point(404, 2);
            this.cdvResult.MandatoryFlag = false;
            this.cdvResult.Name = "cdvResult";
            this.cdvResult.ParentValue = "";
            this.cdvResult.sCodeColumnName = "KEY_1";
            this.cdvResult.sDynamicQuery = "";            
            this.cdvResult.Size = new System.Drawing.Size(180, 21);
            this.cdvResult.sTableName = "H_PARITY_RESULT";
            this.cdvResult.sValueColumnName = "DATA_1";
            this.cdvResult.TabIndex = 1;
            this.cdvResult.VisibleValueButton = true;
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvOper.bMultiSelect = true;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(204, 2);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sCodeColumnName = "";
            this.cdvOper.sDynamicQuery = "";            
            this.cdvOper.Size = new System.Drawing.Size(180, 21);
            this.cdvOper.sTableName = "";
            this.cdvOper.sValueColumnName = "";
            this.cdvOper.TabIndex = 2;
            this.cdvOper.VisibleValueButton = true;
            // 
            // cdvPkg
            // 
            this.cdvPkg.BackColor = System.Drawing.Color.Transparent;
            this.cdvPkg.bMultiSelect = true;
            this.cdvPkg.ConditionText = "PKG Type";
            this.cdvPkg.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvPkg.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvPkg.Location = new System.Drawing.Point(604, 2);
            this.cdvPkg.MandatoryFlag = false;
            this.cdvPkg.Name = "cdvPkg";
            this.cdvPkg.ParentValue = "";
            this.cdvPkg.sCodeColumnName = "MAT_GRP_3";
            this.cdvPkg.sDynamicQuery = "SELECT DISTINCT MAT_GRP_3 AS MAT_GRP_3, \' \' AS NONE FROM MWIPMATDEF";
            this.cdvPkg.sFactory = "";
            this.cdvPkg.Size = new System.Drawing.Size(180, 21);
            this.cdvPkg.sTableName = "";
            this.cdvPkg.sValueColumnName = "NONE";
            this.cdvPkg.TabIndex = 3;
            this.cdvPkg.VisibleValueButton = true;
            // 
            // cdvFromTo
            // 
            this.cdvFromTo.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromTo.Location = new System.Drawing.Point(84, 3);
            this.cdvFromTo.Name = "cdvFromTo";
            this.cdvFromTo.RestrictedDayCount = 60;
            this.cdvFromTo.Size = new System.Drawing.Size(275, 21);
            this.cdvFromTo.TabIndex = 0;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(8, 5);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 53;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "Date";
            // 
            // cboGraph
            // 
            this.cboGraph.FormattingEnabled = true;
            this.cboGraph.Items.AddRange(new object[] {
            "Evaluation by type",
            "Evaluation by operation",
            "Evaluation status by period",
            "Percentage by evaluation result"});
            this.cboGraph.Location = new System.Drawing.Point(662, 2);
            this.cboGraph.Name = "cboGraph";
            this.cboGraph.Size = new System.Drawing.Size(121, 21);
            this.cboGraph.TabIndex = 54;
            this.cboGraph.SelectedIndexChanged += new System.EventHandler(this.cboGraph_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(607, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 56;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 55;
            this.label3.Text = "Graph";
            // 
            // cdvApproval
            // 
            this.cdvApproval.BackColor = System.Drawing.Color.Transparent;
            this.cdvApproval.bMultiSelect = true;
            this.cdvApproval.ConditionText = "Customer Approval";
            this.cdvApproval.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvApproval.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvApproval.Location = new System.Drawing.Point(404, 1);
            this.cdvApproval.MandatoryFlag = false;
            this.cdvApproval.Name = "cdvApproval";
            this.cdvApproval.ParentValue = "";
            this.cdvApproval.sCodeColumnName = "";
            this.cdvApproval.sDynamicQuery = "";
            this.cdvApproval.sFactory = "";
            this.cdvApproval.Size = new System.Drawing.Size(180, 21);
            this.cdvApproval.sTableName = "";
            this.cdvApproval.sValueColumnName = "";
            this.cdvApproval.TabIndex = 57;
            this.cdvApproval.VisibleValueButton = true;
            // 
            // PQC030105
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030105";
            this.pnlMiddle.ResumeLayout(false);
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
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvKind;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOper;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvResult;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvPkg;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromTo;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvApproval;
    }
}
