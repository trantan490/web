namespace Hana.PQC
{
    partial class PQC030102
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030102));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGraph = new System.Windows.Forms.ComboBox();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvCp = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvLoss = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvFromTo = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.cdvStep = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.lblTitle.Size = new System.Drawing.Size(62, 13);
            this.lblTitle.Text = "Customer complaints";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.cdvFromTo);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.cboGraph);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvStep);
            this.pnlCondition1.Controls.Add(this.cdvCp);
            this.pnlCondition1.Controls.Add(this.cdvLoss);
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
            this.splitContainer1.Size = new System.Drawing.Size(794, 418);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Scrollable = true;
            this.udcChartFX1.Size = new System.Drawing.Size(794, 221);
            this.udcChartFX1.TabIndex = 24;
            this.udcChartFX1.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable1});
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
            this.spdData.Size = new System.Drawing.Size(794, 193);
            this.spdData.TabIndex = 1;
            this.spdData.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spdData_CellDoubleClick);
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
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(407, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 63;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 62;
            this.label3.Text = "Graph";
            // 
            // cboGraph
            // 
            this.cboGraph.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGraph.FormattingEnabled = true;
            this.cboGraph.Items.AddRange(new object[] {
            "Occurrence by customer",
            "CAR TAT by customer",
            "Ratio according to defect name",
            "Occurrence by PKG type",
            "Occurrence by defect type",
            "Occurrence by inquiry cycle"});
            this.cboGraph.Location = new System.Drawing.Point(481, 2);
            this.cboGraph.Name = "cboGraph";
            this.cboGraph.Size = new System.Drawing.Size(183, 21);
            this.cboGraph.TabIndex = 61;
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
            // cdvCp
            // 
            this.cdvCp.BackColor = System.Drawing.Color.Transparent;
            this.cdvCp.bMultiSelect = true;
            this.cdvCp.ConditionText = "Type of complaint";
            this.cdvCp.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvCp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvCp.Location = new System.Drawing.Point(204, 0);
            this.cdvCp.MandatoryFlag = false;
            this.cdvCp.Name = "cdvCp";
            this.cdvCp.ParentValue = "";
            this.cdvCp.sCodeColumnName = "KEY_1";
            this.cdvCp.sDynamicQuery = "";            
            this.cdvCp.Size = new System.Drawing.Size(180, 21);
            this.cdvCp.sTableName = "ABRLOT_CLAIM";
            this.cdvCp.sValueColumnName = "DATA_1";
            this.cdvCp.TabIndex = 59;
            this.cdvCp.VisibleValueButton = true;
            // 
            // cdvLoss
            // 
            this.cdvLoss.BackColor = System.Drawing.Color.Transparent;
            this.cdvLoss.bMultiSelect = true;
            this.cdvLoss.ConditionText = "Defect name";
            this.cdvLoss.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvLoss.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLoss.Location = new System.Drawing.Point(404, 0);
            this.cdvLoss.MandatoryFlag = false;
            this.cdvLoss.Name = "cdvLoss";
            this.cdvLoss.ParentValue = "";
            this.cdvLoss.sCodeColumnName = "";
            this.cdvLoss.sDynamicQuery = "";            
            this.cdvLoss.Size = new System.Drawing.Size(180, 22);
            this.cdvLoss.sTableName = "DEFECT_CODE";
            this.cdvLoss.sValueColumnName = "";
            this.cdvLoss.TabIndex = 58;
            this.cdvLoss.VisibleValueButton = true;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 66;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Date";
            // 
            // cdvFromTo
            // 
            this.cdvFromTo.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromTo.Location = new System.Drawing.Point(83, 2);
            this.cdvFromTo.Name = "cdvFromTo";
            this.cdvFromTo.RestrictedDayCount = 60;
            this.cdvFromTo.Size = new System.Drawing.Size(275, 21);
            this.cdvFromTo.TabIndex = 64;
            // 
            // cdvStep
            // 
            this.cdvStep.BackColor = System.Drawing.Color.Transparent;
            this.cdvStep.bMultiSelect = true;
            this.cdvStep.ConditionText = "Step";
            this.cdvStep.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvStep.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvStep.Location = new System.Drawing.Point(604, 0);
            this.cdvStep.MandatoryFlag = false;
            this.cdvStep.Name = "cdvStep";
            this.cdvStep.ParentValue = "";
            this.cdvStep.sCodeColumnName = "CUR_STEP";
            this.cdvStep.sDynamicQuery = "SELECT DISTINCT CUR_STEP,  \' \' AS NONE FROM CIQCABRLOT ORDER BY CUR_STEP";
            this.cdvStep.sFactory = "";
            this.cdvStep.Size = new System.Drawing.Size(180, 21);
            this.cdvStep.sTableName = "";
            this.cdvStep.sValueColumnName = "NONE";
            this.cdvStep.TabIndex = 66;
            this.cdvStep.VisibleValueButton = true;
            // 
            // PQC030102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030102";
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
            this.pnlDetailCondition3.ResumeLayout(false);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboGraph;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvCp;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLoss;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromTo;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStep;
    }
}
