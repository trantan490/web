namespace Hana.TRN
{
    partial class TRN090201
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
            this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.SC01 = new System.Windows.Forms.SplitContainer();
            this.lblBaseDate = new System.Windows.Forms.Label();
            this.cdvBaseDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvres_grp_3 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.gbTrand = new System.Windows.Forms.GroupBox();
            this.rb02 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rb01 = new System.Windows.Forms.RadioButton();
            this.udcMSChart1 = new Miracom.SmartWeb.UI.Controls.udcMSChart();
            this.pnlMiddle.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
            this.SC01.Panel1.SuspendLayout();
            this.SC01.Panel2.SuspendLayout();
            this.SC01.SuspendLayout();
            this.gbTrand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(176, 13);
            this.lblTitle.Text = "Equipment Index (Utilization Rate / Down Time)";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 42);
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
            this.pnlCondition2.Location = new System.Drawing.Point(0, 38);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.gbTrand);
            this.pnlCondition1.Controls.Add(this.cdvres_grp_3);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.lblBaseDate);
            this.pnlCondition1.Controls.Add(this.cdvBaseDate);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 68);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.SC01);
            this.pnlMain.Location = new System.Drawing.Point(0, 155);
            this.pnlMain.Size = new System.Drawing.Size(800, 445);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDetail.Visible = false;
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
            this.btnSort.Visible = false;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 215);
            // 
            // SS01
            // 
            this.SS01.About = "4.0.2001.2005";
            this.SS01.AccessibleDescription = "SS01, Sheet1, Row 0, Column 0, ";
            this.SS01.BackColor = System.Drawing.Color.White;
            this.SS01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SS01.Location = new System.Drawing.Point(0, 0);
            this.SS01.Name = "SS01";
            this.SS01.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SS01.RPT_IsPreCellsType = true;
            this.SS01.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.SS01.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS01_Sheet1});
            this.SS01.Size = new System.Drawing.Size(794, 99);
            this.SS01.TabIndex = 1;
            // 
            // SS01_Sheet1
            // 
            this.SS01_Sheet1.Reset();
            this.SS01_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SS01_Sheet1.ColumnCount = 0;
            this.SS01_Sheet1.RowCount = 0;
            this.SS01_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.SS01_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.SS01.SetActiveViewport(0, 1, 1);
            // 
            // SC01
            // 
            this.SC01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SC01.Location = new System.Drawing.Point(3, 3);
            this.SC01.Name = "SC01";
            this.SC01.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SC01.Panel1
            // 
            this.SC01.Panel1.Controls.Add(this.udcMSChart1);
            // 
            // SC01.Panel2
            // 
            this.SC01.Panel2.Controls.Add(this.SS01);
            this.SC01.Size = new System.Drawing.Size(794, 442);
            this.SC01.SplitterDistance = 339;
            this.SC01.TabIndex = 2;
            // 
            // lblBaseDate
            // 
            this.lblBaseDate.AutoSize = true;
            this.lblBaseDate.Location = new System.Drawing.Point(214, 7);
            this.lblBaseDate.Name = "lblBaseDate";
            this.lblBaseDate.Size = new System.Drawing.Size(62, 13);
            this.lblBaseDate.TabIndex = 78;
            this.lblBaseDate.Text = "Standard date :";
            // 
            // cdvBaseDate
            // 
            this.cdvBaseDate.CustomFormat = "yyyy-MM-dd";
            this.cdvBaseDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvBaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvBaseDate.Location = new System.Drawing.Point(280, 4);
            this.cdvBaseDate.Name = "cdvBaseDate";
            this.cdvBaseDate.Size = new System.Drawing.Size(104, 20);
            this.cdvBaseDate.TabIndex = 77;
            this.cdvBaseDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvBaseDate.Value = new System.DateTime(2008, 12, 2, 0, 0, 0, 0);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(4, 3);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 79;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // cdvres_grp_3
            // 
            this.cdvres_grp_3.BackColor = System.Drawing.Color.Transparent;
            this.cdvres_grp_3.bMultiSelect = false;
            this.cdvres_grp_3.ConditionText = "Equipment Group 3";
            this.cdvres_grp_3.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvres_grp_3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvres_grp_3.Location = new System.Drawing.Point(404, 3);
            this.cdvres_grp_3.MandatoryFlag = false;
            this.cdvres_grp_3.Name = "cdvres_grp_3";
            this.cdvres_grp_3.ParentValue = "";
            this.cdvres_grp_3.sCodeColumnName = "KEY_1";
            this.cdvres_grp_3.sDynamicQuery = "SELECT \' \' AS KEY_1, \' \' AS DATA_1 FROM DUAL UNION ALL SELECT KEY_1 AS KEY_1, \' \'" +
                " AS DATA_1 FROM MGCMTBLDAT WHERE FACTORY = \'HMKA1\' AND TABLE_NAME =\'H_EQP_GRP\' A" +
                "ND DATA_2 = \'Y\' ORDER BY KEY_1 ASC";
            this.cdvres_grp_3.sFactory = "";
            this.cdvres_grp_3.Size = new System.Drawing.Size(180, 21);
            this.cdvres_grp_3.sTableName = "";
            this.cdvres_grp_3.sValueColumnName = "DATA_1";
            this.cdvres_grp_3.TabIndex = 80;
            this.cdvres_grp_3.VisibleValueButton = true;
            // 
            // gbTrand
            // 
            this.gbTrand.Controls.Add(this.rb02);
            this.gbTrand.Controls.Add(this.label1);
            this.gbTrand.Controls.Add(this.rb01);
            this.gbTrand.Location = new System.Drawing.Point(596, -3);
            this.gbTrand.Name = "gbTrand";
            this.gbTrand.Size = new System.Drawing.Size(206, 30);
            this.gbTrand.TabIndex = 81;
            this.gbTrand.TabStop = false;
            // 
            // rb02
            // 
            this.rb02.AutoSize = true;
            this.rb02.Location = new System.Drawing.Point(105, 9);
            this.rb02.Name = "rb02";
            this.rb02.Size = new System.Drawing.Size(97, 17);
            this.rb02.TabIndex = 73;
            this.rb02.Text = "Down Time (minutes)";
            this.rb02.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "classification :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rb01
            // 
            this.rb01.AutoSize = true;
            this.rb01.Checked = true;
            this.rb01.Location = new System.Drawing.Point(44, 9);
            this.rb01.Name = "rb01";
            this.rb01.Size = new System.Drawing.Size(61, 17);
            this.rb01.TabIndex = 70;
            this.rb01.TabStop = true;
            this.rb01.Text = "operating rate";
            this.rb01.UseVisualStyleBackColor = true;
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
            this.udcMSChart1.Size = new System.Drawing.Size(794, 339);
            this.udcMSChart1.TabIndex = 1;
            this.udcMSChart1.Text = "udcMSChart1";
            // 
            // TRN090201
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 1;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TRN090201";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
            this.pnlWIPDetail.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            this.SC01.Panel1.ResumeLayout(false);
            this.SC01.Panel2.ResumeLayout(false);
            this.SC01.ResumeLayout(false);
            this.gbTrand.ResumeLayout(false);
            this.gbTrand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
        private FarPoint.Win.Spread.SheetView SS01_Sheet1;
        private System.Windows.Forms.SplitContainer SC01;
        private System.Windows.Forms.Label lblBaseDate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvBaseDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvres_grp_3;
        private System.Windows.Forms.GroupBox gbTrand;
        private System.Windows.Forms.RadioButton rb02;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb01;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
    }
}
