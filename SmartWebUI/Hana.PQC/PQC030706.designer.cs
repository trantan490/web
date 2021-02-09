namespace Hana.PQC
{
    partial class PQC030706
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030706));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvVendor = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvMatType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvModel = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvDesc = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.rdb1 = new System.Windows.Forms.RadioButton();
            this.rdb2 = new System.Windows.Forms.RadioButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.lblLotType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDay = new System.Windows.Forms.ComboBox();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(117, 13);
            this.lblTitle.Text = "Raw Material Recurrence Rate";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
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
            this.pnlCondition5.Location = new System.Drawing.Point(0, 105);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 81);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.cdvModel);
            this.pnlCondition3.Controls.Add(this.cdvDesc);
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cboDay);
            this.pnlCondition2.Controls.Add(this.lblLotType);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.cdvMatType);
            this.pnlCondition2.Controls.Add(this.cdvVendor);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.rdb2);
            this.pnlCondition1.Controls.Add(this.rdb1);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 152);
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
            this.pnlMain.Controls.Add(this.splitContainer1);
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
            this.spdData.Size = new System.Drawing.Size(794, 265);
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
            // cdvVendor
            // 
            this.cdvVendor.BackColor = System.Drawing.Color.Transparent;
            this.cdvVendor.bMultiSelect = true;
            this.cdvVendor.ConditionText = "account";
            this.cdvVendor.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvVendor.ControlRef = true;
            this.cdvVendor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvVendor.Location = new System.Drawing.Point(203, 1);
            this.cdvVendor.MandatoryFlag = false;
            this.cdvVendor.Name = "cdvVendor";
            this.cdvVendor.ParentValue = "";
            this.cdvVendor.sCodeColumnName = "Code";
            this.cdvVendor.sDynamicQuery = "";            
            this.cdvVendor.Size = new System.Drawing.Size(180, 21);
            this.cdvVendor.sTableName = "";
            this.cdvVendor.sValueColumnName = "Data";
            this.cdvVendor.TabIndex = 68;
            this.cdvVendor.VisibleValueButton = true;
            this.cdvVendor.ValueButtonPress += new System.EventHandler(this.cdvVendor_ValueButtonPress);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.Enabled = false;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.Location = new System.Drawing.Point(4, 1);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 71;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // cdvMatType
            // 
            this.cdvMatType.BackColor = System.Drawing.Color.Transparent;
            this.cdvMatType.bMultiSelect = true;
            this.cdvMatType.ConditionText = "Mat_Type";
            this.cdvMatType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL_TYPE;
            this.cdvMatType.ControlRef = true;
            this.cdvMatType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatType.ForcInitControl1 = this.cdvModel;
            this.cdvMatType.ForcInitControl2 = this.cdvDesc;
            this.cdvMatType.Location = new System.Drawing.Point(4, 1);
            this.cdvMatType.MandatoryFlag = false;
            this.cdvMatType.Name = "cdvMatType";
            this.cdvMatType.ParentValue = "";
            this.cdvMatType.sCodeColumnName = "";
            this.cdvMatType.sDynamicQuery = "";            
            this.cdvMatType.Size = new System.Drawing.Size(180, 21);
            this.cdvMatType.sTableName = "";
            this.cdvMatType.sValueColumnName = "";
            this.cdvMatType.TabIndex = 72;
            this.cdvMatType.VisibleValueButton = true;
            // 
            // cdvModel
            // 
            this.cdvModel.BackColor = System.Drawing.Color.Transparent;
            this.cdvModel.bMultiSelect = true;
            this.cdvModel.ConditionText = "Model";
            this.cdvModel.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvModel.ControlRef = true;
            this.cdvModel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvModel.Location = new System.Drawing.Point(4, 2);
            this.cdvModel.MandatoryFlag = false;
            this.cdvModel.Name = "cdvModel";
            this.cdvModel.ParentValue = "";
            this.cdvModel.RefControl1 = this.cdvMatType;
            this.cdvModel.sCodeColumnName = "Code";
            this.cdvModel.sDynamicQuery = "";            
            this.cdvModel.Size = new System.Drawing.Size(180, 21);
            this.cdvModel.sTableName = "";
            this.cdvModel.sValueColumnName = "Data";
            this.cdvModel.TabIndex = 73;
            this.cdvModel.Visible = false;
            this.cdvModel.VisibleValueButton = true;
            this.cdvModel.ValueButtonPress += new System.EventHandler(this.cdvModel_ValueButtonPress);
            // 
            // cdvDesc
            // 
            this.cdvDesc.BackColor = System.Drawing.Color.Transparent;
            this.cdvDesc.bMultiSelect = true;
            this.cdvDesc.ConditionText = "Standard";
            this.cdvDesc.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvDesc.ControlRef = true;
            this.cdvDesc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvDesc.Location = new System.Drawing.Point(211, 2);
            this.cdvDesc.MandatoryFlag = false;
            this.cdvDesc.Name = "cdvDesc";
            this.cdvDesc.ParentValue = "";
            this.cdvDesc.RefControl1 = this.cdvMatType;
            this.cdvDesc.sCodeColumnName = "Code";
            this.cdvDesc.sDynamicQuery = "";            
            this.cdvDesc.Size = new System.Drawing.Size(180, 21);
            this.cdvDesc.sTableName = "";
            this.cdvDesc.sValueColumnName = "Data";
            this.cdvDesc.TabIndex = 68;
            this.cdvDesc.Visible = false;
            this.cdvDesc.VisibleValueButton = true;
            this.cdvDesc.ValueButtonPress += new System.EventHandler(this.cdvDesc_ValueButtonPress);
            // 
            // rdb1
            // 
            this.rdb1.AutoSize = true;
            this.rdb1.Checked = true;
            this.rdb1.Location = new System.Drawing.Point(207, 4);
            this.rdb1.Name = "rdb1";
            this.rdb1.Size = new System.Drawing.Size(73, 17);
            this.rdb1.TabIndex = 1;
            this.rdb1.TabStop = true;
            this.rdb1.Text = "Import inspection";
            this.rdb1.UseVisualStyleBackColor = true;
            // 
            // rdb2
            // 
            this.rdb2.AutoSize = true;
            this.rdb2.Location = new System.Drawing.Point(280, 4);
            this.rdb2.Name = "rdb2";
            this.rdb2.Size = new System.Drawing.Size(73, 17);
            this.rdb2.TabIndex = 2;
            this.rdb2.Text = "Operation inspection";
            this.rdb2.UseVisualStyleBackColor = true;
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
            this.splitContainer1.Panel1.Controls.Add(this.spdData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.udcChartFX1);
            this.splitContainer1.Size = new System.Drawing.Size(794, 505);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.TabIndex = 2;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Scrollable = true;
            this.udcChartFX1.Size = new System.Drawing.Size(794, 236);
            this.udcChartFX1.TabIndex = 26;
            this.udcChartFX1.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable1});
            this.udcChartFX1.ToolBar = true;
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(390, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(275, 21);
            this.cdvFromToDate.TabIndex = 72;
            // 
            // lblLotType
            // 
            this.lblLotType.AutoSize = true;
            this.lblLotType.Location = new System.Drawing.Point(419, 6);
            this.lblLotType.Name = "lblLotType";
            this.lblLotType.Size = new System.Drawing.Size(117, 13);
            this.lblLotType.TabIndex = 26;
            this.lblLotType.Text = "Recurrence Rate Management Criteria (Month)";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(406, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 25;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDay
            // 
            this.cboDay.FormattingEnabled = true;
            this.cboDay.Items.AddRange(new object[] {
            "3",
            "6",
            "9",
            "12"});
            this.cboDay.Location = new System.Drawing.Point(541, 1);
            this.cboDay.Name = "cboDay";
            this.cboDay.Size = new System.Drawing.Size(44, 21);
            this.cboDay.TabIndex = 73;
            this.cboDay.Text = "3";
            // 
            // PQC030706
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030706";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvVendor;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvMatType;
        private System.Windows.Forms.RadioButton rdb2;
        private System.Windows.Forms.RadioButton rdb1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvDesc;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvModel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.Label lblLotType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDay;
    }
}
