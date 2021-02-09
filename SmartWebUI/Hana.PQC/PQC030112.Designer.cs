﻿namespace Hana.PQC
{
    partial class PQC030112
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030112));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcGubun = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcFlag = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcInterval = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcUser = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcDept = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblCalib = new System.Windows.Forms.Label();
            this.txtMeasure = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboGraph = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.udcStatus = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.lblTitle.Size = new System.Drawing.Size(117, 13);
            this.lblTitle.Text = "Instrument Calibration Status";
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
            this.pnlCondition3.Controls.Add(this.lblCalib);
            this.pnlCondition3.Controls.Add(this.udcFromToDate);
            this.pnlCondition3.Controls.Add(this.txtMeasure);
            this.pnlCondition3.Controls.Add(this.label3);
            this.pnlCondition3.Controls.Add(this.lblIcon);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.udcStatus);
            this.pnlCondition2.Controls.Add(this.udcGubun);
            this.pnlCondition2.Controls.Add(this.udcInterval);
            this.pnlCondition2.Controls.Add(this.udcFlag);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cboGraph);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.udcDept);
            this.pnlCondition1.Controls.Add(this.udcUser);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 176);
            this.pnlMain.Size = new System.Drawing.Size(800, 424);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
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
            this.splitContainer1.Size = new System.Drawing.Size(794, 421);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.TabIndex = 0;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Scrollable = true;
            this.udcChartFX1.Size = new System.Drawing.Size(794, 222);
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
            this.spdData.Size = new System.Drawing.Size(794, 195);
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
            // udcGubun
            // 
            this.udcGubun.BackColor = System.Drawing.Color.Transparent;
            this.udcGubun.bMultiSelect = true;
            this.udcGubun.ConditionText = "calibration strategy";
            this.udcGubun.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcGubun.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcGubun.Location = new System.Drawing.Point(563, 2);
            this.udcGubun.MandatoryFlag = false;
            this.udcGubun.Name = "udcGubun";
            this.udcGubun.ParentValue = "";
            this.udcGubun.sCodeColumnName = "KEY_1";
            this.udcGubun.sDynamicQuery = "";            
            this.udcGubun.Size = new System.Drawing.Size(180, 21);
            this.udcGubun.sTableName = "H_CALIB_CLASS";
            this.udcGubun.sValueColumnName = "DA";
            this.udcGubun.TabIndex = 71;
            this.udcGubun.VisibleValueButton = true;
            // 
            // udcFlag
            // 
            this.udcFlag.BackColor = System.Drawing.Color.Transparent;
            this.udcFlag.bMultiSelect = true;
            this.udcFlag.ConditionText = "Calibration status";
            this.udcFlag.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcFlag.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcFlag.Location = new System.Drawing.Point(190, 2);
            this.udcFlag.MandatoryFlag = false;
            this.udcFlag.Name = "udcFlag";
            this.udcFlag.ParentValue = "";
            this.udcFlag.sCodeColumnName = "KEY_1";
            this.udcFlag.sDynamicQuery = "";            
            this.udcFlag.Size = new System.Drawing.Size(180, 21);
            this.udcFlag.sTableName = "H_CALIB_FLAG";
            this.udcFlag.sValueColumnName = "DATA_1";
            this.udcFlag.TabIndex = 70;
            this.udcFlag.VisibleValueButton = true;
            // 
            // udcInterval
            // 
            this.udcInterval.BackColor = System.Drawing.Color.Transparent;
            this.udcInterval.bMultiSelect = true;
            this.udcInterval.ConditionText = "Calibration Interval";
            this.udcInterval.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcInterval.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcInterval.Location = new System.Drawing.Point(4, 2);
            this.udcInterval.MandatoryFlag = false;
            this.udcInterval.Name = "udcInterval";
            this.udcInterval.ParentValue = "";
            this.udcInterval.sCodeColumnName = "KEY_1";
            this.udcInterval.sDynamicQuery = "";            
            this.udcInterval.Size = new System.Drawing.Size(180, 21);
            this.udcInterval.sTableName = "H_CALIB_INTERVAL";
            this.udcInterval.sValueColumnName = "DATA_1";
            this.udcInterval.TabIndex = 69;
            this.udcInterval.VisibleValueButton = true;
            // 
            // udcUser
            // 
            this.udcUser.BackColor = System.Drawing.Color.Transparent;
            this.udcUser.bMultiSelect = true;
            this.udcUser.ConditionText = "the person in charge";
            this.udcUser.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcUser.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcUser.Location = new System.Drawing.Point(377, 0);
            this.udcUser.MandatoryFlag = false;
            this.udcUser.Name = "udcUser";
            this.udcUser.ParentValue = "";
            this.udcUser.sCodeColumnName = "USER_ID";
            this.udcUser.sDynamicQuery = "SELECT USER_ID, USER_DESC FROM RWEBUSRDEF";            
            this.udcUser.Size = new System.Drawing.Size(180, 21);
            this.udcUser.sTableName = "";
            this.udcUser.sValueColumnName = "USER_DESC";
            this.udcUser.TabIndex = 79;
            this.udcUser.VisibleValueButton = true;
            // 
            // udcDept
            // 
            this.udcDept.BackColor = System.Drawing.Color.Transparent;
            this.udcDept.bMultiSelect = true;
            this.udcDept.ConditionText = "Use department";
            this.udcDept.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcDept.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcDept.Location = new System.Drawing.Point(190, 0);
            this.udcDept.MandatoryFlag = false;
            this.udcDept.Name = "udcDept";
            this.udcDept.ParentValue = "";
            this.udcDept.sCodeColumnName = "KEY_1";
            this.udcDept.sDynamicQuery = "";            
            this.udcDept.Size = new System.Drawing.Size(180, 21);
            this.udcDept.sTableName = "H_DEPARTMENT";
            this.udcDept.sValueColumnName = "DATA_1";
            this.udcDept.TabIndex = 78;
            this.udcDept.VisibleValueButton = true;
            // 
            // lblCalib
            // 
            this.lblCalib.AutoSize = true;
            this.lblCalib.Location = new System.Drawing.Point(392, 8);
            this.lblCalib.Name = "lblCalib";
            this.lblCalib.Size = new System.Drawing.Size(55, 13);
            this.lblCalib.TabIndex = 77;
            this.lblCalib.Text = "Instrument Name";
            // 
            // txtMeasure
            // 
            this.txtMeasure.Location = new System.Drawing.Point(457, 4);
            this.txtMeasure.Name = "txtMeasure";
            this.txtMeasure.Size = new System.Drawing.Size(279, 21);
            this.txtMeasure.TabIndex = 76;
            this.txtMeasure.Text = "%";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(380, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 80;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboGraph
            // 
            this.cboGraph.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGraph.FormattingEnabled = true;
            this.cboGraph.Items.AddRange(new object[] {
            "View all",
            "Calibration status by department",
            "Calibration Status by Calibration Status",
            "Department Status by Period",
            "Whether the calibration ratio",
            "Percentage of Instrument by Department",
            "Status by Instrument type",
            "Calibration status ratio"});
            this.cboGraph.Location = new System.Drawing.Point(622, 0);
            this.cboGraph.Name = "cboGraph";
            this.cboGraph.Size = new System.Drawing.Size(148, 21);
            this.cboGraph.TabIndex = 76;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(566, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(6, 17);
            this.label2.TabIndex = 75;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(579, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 74;
            this.label4.Text = "Graph";
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(84, 5);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(275, 21);
            this.udcFromToDate.TabIndex = 73;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 8);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 72;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "Date";
            // 
            // udcStatus
            // 
            this.udcStatus.BackColor = System.Drawing.Color.Transparent;
            this.udcStatus.bMultiSelect = true;
            this.udcStatus.ConditionText = "Calibration status";
            this.udcStatus.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcStatus.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcStatus.Location = new System.Drawing.Point(377, 2);
            this.udcStatus.MandatoryFlag = false;
            this.udcStatus.Name = "udcStatus";
            this.udcStatus.ParentValue = "";
            this.udcStatus.sCodeColumnName = "KEY_1";
            this.udcStatus.sDynamicQuery = "";            
            this.udcStatus.Size = new System.Drawing.Size(180, 21);
            this.udcStatus.sTableName = "H_CALIB_STATUS";
            this.udcStatus.sValueColumnName = "DATA_1";
            this.udcStatus.TabIndex = 72;
            this.udcStatus.VisibleValueButton = true;
            // 
            // PQC030112
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030112";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
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
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcGubun;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcFlag;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcInterval;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcUser;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcDept;
        private System.Windows.Forms.Label lblCalib;
        private System.Windows.Forms.TextBox txtMeasure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGraph;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcStatus;
    }
}