namespace Hana.RAS
{
    partial class PQC030104
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030104));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGraph = new System.Windows.Forms.ComboBox();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcItem = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcMatSize = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCurrentTestLevel = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.lblTitle.Size = new System.Drawing.Size(120, 13);
            this.lblTitle.Text = "Product evaluation status inquiry";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
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
            this.pnlCondition3.Location = new System.Drawing.Point(0, 62);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.udcFromToDate);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.cboGraph);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.udcMatSize);
            this.pnlCondition1.Controls.Add(this.udcCurrentTestLevel);
            this.pnlCondition1.Controls.Add(this.udcItem);
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
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 0;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Scrollable = true;
            this.udcChartFX1.Size = new System.Drawing.Size(794, 236);
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
            this.spdData.Size = new System.Drawing.Size(794, 205);
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
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(407, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 63;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(425, 6);
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
            "View all"});
            this.cboGraph.Location = new System.Drawing.Point(481, 4);
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
            // udcItem
            // 
            this.udcItem.BackColor = System.Drawing.Color.Transparent;
            this.udcItem.bMultiSelect = true;
            this.udcItem.ConditionText = "Item";
            this.udcItem.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcItem.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcItem.Location = new System.Drawing.Point(204, 0);
            this.udcItem.MandatoryFlag = false;
            this.udcItem.Name = "udcItem";
            this.udcItem.ParentValue = "";
            this.udcItem.sCodeColumnName = "KEY_1";
            this.udcItem.sDynamicQuery = "";           
            this.udcItem.Size = new System.Drawing.Size(180, 21);
            this.udcItem.sTableName = "MATERIAL_TYPE";
            this.udcItem.sValueColumnName = "DATA_1";
            this.udcItem.TabIndex = 58;
            this.udcItem.VisibleValueButton = true;
            // 
            // udcMatSize
            // 
            this.udcMatSize.BackColor = System.Drawing.Color.Transparent;
            this.udcMatSize.bMultiSelect = true;
            this.udcMatSize.ConditionText = "Standard";
            this.udcMatSize.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcMatSize.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcMatSize.Location = new System.Drawing.Point(404, 0);
            this.udcMatSize.MandatoryFlag = false;
            this.udcMatSize.Name = "udcMatSize";
            this.udcMatSize.ParentValue = "";
            this.udcMatSize.sCodeColumnName = "KEY_1";
            this.udcMatSize.sDynamicQuery = "";            
            this.udcMatSize.Size = new System.Drawing.Size(180, 21);
            this.udcMatSize.sTableName = "BOM_SIZE";
            this.udcMatSize.sValueColumnName = "DATA_1";
            this.udcMatSize.TabIndex = 60;
            this.udcMatSize.VisibleValueButton = true;
            // 
            // udcCurrentTestLevel
            // 
            this.udcCurrentTestLevel.BackColor = System.Drawing.Color.Transparent;
            this.udcCurrentTestLevel.bMultiSelect = true;
            this.udcCurrentTestLevel.ConditionText = "Inspection level";
            this.udcCurrentTestLevel.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCurrentTestLevel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCurrentTestLevel.Location = new System.Drawing.Point(604, 0);
            this.udcCurrentTestLevel.MandatoryFlag = false;
            this.udcCurrentTestLevel.Name = "udcCurrentTestLevel";
            this.udcCurrentTestLevel.ParentValue = "";
            this.udcCurrentTestLevel.sCodeColumnName = "KEY_1";
            this.udcCurrentTestLevel.sDynamicQuery = "";            
            this.udcCurrentTestLevel.Size = new System.Drawing.Size(180, 21);
            this.udcCurrentTestLevel.sTableName = "QC_LEVEL";
            this.udcCurrentTestLevel.sValueColumnName = "BLANK";
            this.udcCurrentTestLevel.TabIndex = 59;
            this.udcCurrentTestLevel.VisibleValueButton = true;
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(84, 4);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(275, 21);
            this.udcFromToDate.TabIndex = 66;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 6);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 65;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 64;
            this.label2.Text = "Date";
            // 
            // PQC030104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030104";
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboGraph;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcItem;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcMatSize;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCurrentTestLevel;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
    }
}
