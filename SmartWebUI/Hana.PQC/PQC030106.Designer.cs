namespace Hana.PQC
{
    partial class PQC030106
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030106));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcMSChart1 = new Miracom.SmartWeb.UI.Controls.udcMSChart();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvCp = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvLoss = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboGraph = new System.Windows.Forms.ComboBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvFromTo = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvStep = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.ckbEndData = new System.Windows.Forms.CheckBox();
            this.cboUserCnt = new System.Windows.Forms.ComboBox();
            this.cdvFromOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvToOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.pnlDetailCondition3.SuspendLayout();
            this.pnlBUMPDetail.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(75, 13);
            this.lblTitle.Text = "Status of nonconformance";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.cboUserCnt);
            this.pnlCondition3.Controls.Add(this.ckbEndData);
            this.pnlCondition3.Controls.Add(this.label3);
            this.pnlCondition3.Controls.Add(this.label1);
            this.pnlCondition3.Controls.Add(this.cboGraph);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvFromOper);
            this.pnlCondition2.Controls.Add(this.cdvToOper);
            this.pnlCondition2.Controls.Add(this.cdvStep);
            this.pnlCondition2.Controls.Add(this.cdvLoss);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFromTo);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.cdvCp);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 203);
            this.pnlMain.Size = new System.Drawing.Size(800, 397);
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
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 263);
            // 
            // udcBUMPCondition9
            // 
            this.udcBUMPCondition9.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition1
            // 
            this.udcBUMPCondition1.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition2
            // 
            this.udcBUMPCondition2.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition3
            // 
            this.udcBUMPCondition3.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition4
            // 
            this.udcBUMPCondition4.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition5
            // 
            this.udcBUMPCondition5.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition6
            // 
            this.udcBUMPCondition6.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition7
            // 
            this.udcBUMPCondition7.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition8
            // 
            this.udcBUMPCondition8.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition12
            // 
            this.udcBUMPCondition12.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition11
            // 
            this.udcBUMPCondition11.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition10
            // 
            this.udcBUMPCondition10.RefFactory = this.cdvFactory;
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
            this.splitContainer1.Panel1.Controls.Add(this.udcMSChart1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spdData);
            this.splitContainer1.Size = new System.Drawing.Size(794, 394);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 0;
            // 
            // udcMSChart1
            // 
            chartArea1.Name = "ChartArea1";
            this.udcMSChart1.ChartAreas.Add(chartArea1);
            this.udcMSChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.udcMSChart1.Legends.Add(legend1);
            this.udcMSChart1.Location = new System.Drawing.Point(0, 0);
            this.udcMSChart1.Name = "udcMSChart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.udcMSChart1.Series.Add(series1);
            this.udcMSChart1.Size = new System.Drawing.Size(794, 202);
            this.udcMSChart1.TabIndex = 26;
            this.udcMSChart1.Text = "udcMSChart1";
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
            this.spdData.Size = new System.Drawing.Size(794, 188);
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
            // cdvCp
            // 
            this.cdvCp.BackColor = System.Drawing.Color.Transparent;
            this.cdvCp.bMultiSelect = true;
            this.cdvCp.ConditionText = "Abnormal";
            this.cdvCp.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvCp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvCp.Location = new System.Drawing.Point(204, 0);
            this.cdvCp.MandatoryFlag = false;
            this.cdvCp.Name = "cdvCp";
            this.cdvCp.ParentValue = "";
            this.cdvCp.sCodeColumnName = "KEY_1";
            this.cdvCp.sDynamicQuery = "";            
            this.cdvCp.Size = new System.Drawing.Size(180, 21);
            this.cdvCp.sTableName = "ABRLOT_TYPE";
            this.cdvCp.sValueColumnName = "DATA_1";
            this.cdvCp.TabIndex = 54;
            this.cdvCp.VisibleValueButton = true;
            // 
            // cdvLoss
            // 
            this.cdvLoss.BackColor = System.Drawing.Color.Transparent;
            this.cdvLoss.bMultiSelect = true;
            this.cdvLoss.ConditionText = "Defect name";
            this.cdvLoss.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvLoss.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLoss.Location = new System.Drawing.Point(4, 2);
            this.cdvLoss.MandatoryFlag = false;
            this.cdvLoss.Name = "cdvLoss";
            this.cdvLoss.ParentValue = "";
            this.cdvLoss.sCodeColumnName = "Code";
            this.cdvLoss.sDynamicQuery = "";
            this.cdvLoss.sFactory = "";
            this.cdvLoss.Size = new System.Drawing.Size(180, 21);
            this.cdvLoss.sTableName = "";
            this.cdvLoss.sValueColumnName = "Data";
            this.cdvLoss.TabIndex = 56;
            this.cdvLoss.VisibleValueButton = true;
            this.cdvLoss.ValueButtonPress += new System.EventHandler(this.cdvLoss_ValueButtonPress);
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(207, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 63;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(220, 5);
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
            "Accidents by Nonconformity Type&amp;TAT",
            "Nonconformities by Period",
            "Pareto by Defect code",
            "Nonconformity occurrence by defect&amp;output",
            "Nonconformity product occurrence by defect",
            "Nonconforming occurrence by STEP",
            "Nonconformities by Operator"});
            this.cboGraph.Location = new System.Drawing.Point(284, 2);
            this.cboGraph.Name = "cboGraph";
            this.cboGraph.Size = new System.Drawing.Size(183, 21);
            this.cboGraph.TabIndex = 61;
            this.cboGraph.SelectedIndexChanged += new System.EventHandler(this.cboGraph_SelectedIndexChanged);
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(407, 2);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 60;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Date";
            // 
            // cdvFromTo
            // 
            this.cdvFromTo.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromTo.Location = new System.Drawing.Point(484, 2);
            this.cdvFromTo.Name = "cdvFromTo";
            this.cdvFromTo.RestrictedDayCount = 60;
            this.cdvFromTo.Size = new System.Drawing.Size(275, 21);
            this.cdvFromTo.TabIndex = 58;
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
            this.cdvFactory.ForcInitControl2 = this.cdvLoss;
            this.cdvFactory.ForcInitControl3 = this.cdvStep;
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
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvStep
            // 
            this.cdvStep.BackColor = System.Drawing.Color.Transparent;
            this.cdvStep.bMultiSelect = true;
            this.cdvStep.ConditionText = "Step";
            this.cdvStep.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvStep.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvStep.Location = new System.Drawing.Point(204, 2);
            this.cdvStep.MandatoryFlag = false;
            this.cdvStep.Name = "cdvStep";
            this.cdvStep.ParentValue = "";
            this.cdvStep.sCodeColumnName = "";
            this.cdvStep.sDynamicQuery = "";            
            this.cdvStep.Size = new System.Drawing.Size(180, 21);
            this.cdvStep.sTableName = "H_ABR_STEP";
            this.cdvStep.sValueColumnName = "";
            this.cdvStep.TabIndex = 65;
            this.cdvStep.VisibleValueButton = true;
            this.cdvStep.ValueButtonPress += new System.EventHandler(this.cdvStep_ValueButtonPress);
            // 
            // ckbEndData
            // 
            this.ckbEndData.AutoSize = true;
            this.ckbEndData.Checked = true;
            this.ckbEndData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbEndData.Location = new System.Drawing.Point(21, 4);
            this.ckbEndData.Name = "ckbEndData";
            this.ckbEndData.Size = new System.Drawing.Size(89, 17);
            this.ckbEndData.TabIndex = 64;
            this.ckbEndData.Text = "Include close";
            this.ckbEndData.UseVisualStyleBackColor = true;
            // 
            // cboUserCnt
            // 
            this.cboUserCnt.FormattingEnabled = true;
            this.cboUserCnt.Items.AddRange(new object[] {
            "전체",
            "10명",
            "20명"});
            this.cboUserCnt.Location = new System.Drawing.Point(484, 2);
            this.cboUserCnt.Name = "cboUserCnt";
            this.cboUserCnt.Size = new System.Drawing.Size(100, 21);
            this.cboUserCnt.TabIndex = 65;
            this.cboUserCnt.Text = "전체";
            this.cboUserCnt.Visible = false;
            // 
            // cdvFromOper
            // 
            this.cdvFromOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromOper.bMultiSelect = false;
            this.cdvFromOper.ConditionText = "Operation";
            this.cdvFromOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvFromOper.ControlRef = true;
            this.cdvFromOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFromOper.Location = new System.Drawing.Point(403, 2);
            this.cdvFromOper.MandatoryFlag = true;
            this.cdvFromOper.Name = "cdvFromOper";
            this.cdvFromOper.ParentValue = "";
            this.cdvFromOper.sCodeColumnName = "Code";
            this.cdvFromOper.sDynamicQuery = "";            
            this.cdvFromOper.Size = new System.Drawing.Size(180, 21);
            this.cdvFromOper.sTableName = "";
            this.cdvFromOper.sValueColumnName = "Data";
            this.cdvFromOper.TabIndex = 84;
            this.cdvFromOper.VisibleValueButton = true;
            this.cdvFromOper.ValueButtonPress += new System.EventHandler(this.cdvFromOper_ValueButtonPress);
            // 
            // cdvToOper
            // 
            this.cdvToOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvToOper.bMultiSelect = false;
            this.cdvToOper.ConditionText = "Table";
            this.cdvToOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvToOper.ControlRef = true;
            this.cdvToOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvToOper.Location = new System.Drawing.Point(513, 2);
            this.cdvToOper.MandatoryFlag = false;
            this.cdvToOper.Name = "cdvToOper";
            this.cdvToOper.ParentValue = "";
            this.cdvToOper.sCodeColumnName = "Code";
            this.cdvToOper.sDynamicQuery = "";            
            this.cdvToOper.Size = new System.Drawing.Size(180, 21);
            this.cdvToOper.sTableName = "";
            this.cdvToOper.sValueColumnName = "Data";
            this.cdvToOper.TabIndex = 85;
            this.cdvToOper.VisibleValueButton = true;
            this.cdvToOper.ValueButtonPress += new System.EventHandler(this.cdvToOper_ValueButtonPress);
            // 
            // PQC030106
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030106";
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
            this.pnlDetailCondition3.ResumeLayout(false);
            this.pnlBUMPDetail.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;        
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvCp;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLoss;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboGraph;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromTo;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStep;
        private System.Windows.Forms.CheckBox ckbEndData;
        private System.Windows.Forms.ComboBox cboUserCnt;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFromOper;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvToOper;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
    }
}
