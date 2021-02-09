﻿namespace Hana.PQC
{
    partial class PQC030703
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030703));
            SoftwareFX.ChartFX.TitleDockable titleDockable1 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvVendor = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvMatType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvModel = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvDesc = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblQCType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cdvQCType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.udcChartFX2 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.cdvRcvType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(146, 13);
            this.lblTitle.Text = "Import Inspection Monthly Defect Status";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
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
            this.pnlCondition3.Controls.Add(this.cdvRcvType);
            this.pnlCondition3.Controls.Add(this.cdvQCType);
            this.pnlCondition3.Controls.Add(this.lblQCType);
            this.pnlCondition3.Controls.Add(this.label3);
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvModel);
            this.pnlCondition2.Controls.Add(this.cdvMatType);
            this.pnlCondition2.Controls.Add(this.cdvDesc);
            this.pnlCondition2.Controls.Add(this.cdvVendor);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.udcFromToDate);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 176);
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
            this.pnlMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlMain.Location = new System.Drawing.Point(0, 116);
            this.pnlMain.Size = new System.Drawing.Size(800, 484);
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
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spdData.Location = new System.Drawing.Point(3, 3);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(788, 209);
            this.spdData.TabIndex = 1;
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
            // cdvVendor
            // 
            this.cdvVendor.BackColor = System.Drawing.Color.Transparent;
            this.cdvVendor.bMultiSelect = true;
            this.cdvVendor.ConditionText = "account";
            this.cdvVendor.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvVendor.ControlRef = true;
            this.cdvVendor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvVendor.Location = new System.Drawing.Point(199, 1);
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
            this.cdvMatType.ConditionText = "Material classification";
            this.cdvMatType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
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
            this.cdvMatType.sTableName = "MATERIAL_TYPE";
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
            this.cdvModel.Location = new System.Drawing.Point(400, 1);
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
            this.cdvDesc.Location = new System.Drawing.Point(607, 1);
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
            this.cdvDesc.VisibleValueButton = true;
            this.cdvDesc.ValueButtonPress += new System.EventHandler(this.cdvDesc_ValueButtonPress);
            // 
            // lblQCType
            // 
            this.lblQCType.AutoSize = true;
            this.lblQCType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCType.Location = new System.Drawing.Point(18, 5);
            this.lblQCType.Name = "lblQCType";
            this.lblQCType.Size = new System.Drawing.Size(58, 13);
            this.lblQCType.TabIndex = 80;
            this.lblQCType.Text = "Inspection Type";
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(7, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 79;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvQCType
            // 
            this.cdvQCType.FormattingEnabled = true;
            this.cdvQCType.Items.AddRange(new object[] {
            "ALL",
            "Visual",
            "Dimension",
            "Measure"});
            this.cdvQCType.Location = new System.Drawing.Point(84, 1);
            this.cdvQCType.Name = "cdvQCType";
            this.cdvQCType.Size = new System.Drawing.Size(100, 21);
            this.cdvQCType.TabIndex = 81;
            this.cdvQCType.Text = "ALL";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.89421F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 397F));
            this.tableLayoutPanel1.Controls.Add(this.spdData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.90644F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.09356F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(794, 481);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 218);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.udcChartFX1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.udcChartFX2);
            this.splitContainer1.Size = new System.Drawing.Size(788, 260);
            this.splitContainer1.SplitterDistance = 392;
            this.splitContainer1.TabIndex = 27;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AxisX.Staggered = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Size = new System.Drawing.Size(392, 260);
            this.udcChartFX1.TabIndex = 25;
            this.udcChartFX1.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable1});
            this.udcChartFX1.ToolBar = true;
            // 
            // udcChartFX2
            // 
            this.udcChartFX2.AxisX.Staggered = true;
            this.udcChartFX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX2.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX2.Name = "udcChartFX2";
            this.udcChartFX2.Size = new System.Drawing.Size(392, 260);
            this.udcChartFX2.TabIndex = 26;
            this.udcChartFX2.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
            this.udcChartFX2.ToolBar = true;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(202, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 79;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(213, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 80;
            this.label2.Text = "Reception period";
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(233, 2);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(246, 21);
            this.udcFromToDate.TabIndex = 82;
            // 
            // cdvRcvType
            // 
            this.cdvRcvType.BackColor = System.Drawing.Color.Transparent;
            this.cdvRcvType.bMultiSelect = true;
            this.cdvRcvType.ConditionText = "Reception classification";
            this.cdvRcvType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvRcvType.ControlRef = true;
            this.cdvRcvType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvRcvType.Location = new System.Drawing.Point(199, 0);
            this.cdvRcvType.MandatoryFlag = false;
            this.cdvRcvType.Name = "cdvRcvType";
            this.cdvRcvType.ParentValue = "";
            this.cdvRcvType.sCodeColumnName = "Code";
            this.cdvRcvType.sDynamicQuery = "";            
            this.cdvRcvType.Size = new System.Drawing.Size(180, 21);
            this.cdvRcvType.sTableName = "";
            this.cdvRcvType.sValueColumnName = "Data";
            this.cdvRcvType.TabIndex = 83;
            this.cdvRcvType.VisibleValueButton = true;
            this.cdvRcvType.ValueButtonPress += new System.EventHandler(this.cdvRcvType_ValueButtonPress);
            // 
            // PQC030703
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030703";
            this.Load += new System.EventHandler(this.PQC030703_Load);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
            this.pnlCondition2.ResumeLayout(false);
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
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.ComboBox cdvQCType;
        private System.Windows.Forms.Label lblQCType;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvDesc;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvModel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvRcvType;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX2;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
