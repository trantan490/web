namespace Hana.TRN
{
    partial class TRN090102
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
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.scCenter = new System.Windows.Forms.SplitContainer();
            this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvCustomer = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvPackage = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.gbGubun = new System.Windows.Forms.GroupBox();
            this.rbSearch04 = new System.Windows.Forms.RadioButton();
            this.rbSearch03 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rbSearch02 = new System.Windows.Forms.RadioButton();
            this.rbSearch01 = new System.Windows.Forms.RadioButton();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblImform = new System.Windows.Forms.Label();
            this.chkKpcs = new System.Windows.Forms.CheckBox();
            this.udcMSChart1 = new Miracom.SmartWeb.UI.Controls.udcMSChart();
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
            this.pnlBUMPDetail.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.scCenter.Panel1.SuspendLayout();
            this.scCenter.Panel2.SuspendLayout();
            this.scCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
            this.gbGubun.SuspendLayout();
            this.cdvFromToDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(166, 13);
            this.lblTitle.Text = "Status of Hana Micron work-in-operation transfer";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(820, 66);
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
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvFactory);
            this.pnlCondition2.Controls.Add(this.cdvFromToDate);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.cdvCustomer);
            this.pnlCondition2.Controls.Add(this.cdvPackage);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            this.pnlCondition2.Size = new System.Drawing.Size(818, 24);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.gbGubun);
            this.pnlCondition1.Controls.Add(this.chkKpcs);
            this.pnlCondition1.Controls.Add(this.lblImform);
            this.pnlCondition1.Size = new System.Drawing.Size(818, 25);
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
            this.pnlMain.Controls.Add(this.scCenter);
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Size = new System.Drawing.Size(820, 508);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(316, 1);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(160, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 18;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // scCenter
            // 
            this.scCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scCenter.Location = new System.Drawing.Point(3, 3);
            this.scCenter.Name = "scCenter";
            this.scCenter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scCenter.Panel1
            // 
            this.scCenter.Panel1.Controls.Add(this.udcMSChart1);
            // 
            // scCenter.Panel2
            // 
            this.scCenter.Panel2.Controls.Add(this.SS01);
            this.scCenter.Size = new System.Drawing.Size(814, 505);
            this.scCenter.SplitterDistance = 253;
            this.scCenter.TabIndex = 1;
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
            this.SS01.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS01_Sheet1});
            this.SS01.Size = new System.Drawing.Size(814, 248);
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
            // cdvCustomer
            // 
            this.cdvCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cdvCustomer.bMultiSelect = false;
            this.cdvCustomer.ConditionText = "Customer";
            this.cdvCustomer.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvCustomer.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvCustomer.Location = new System.Drawing.Point(484, 2);
            this.cdvCustomer.MandatoryFlag = false;
            this.cdvCustomer.Name = "cdvCustomer";
            this.cdvCustomer.ParentValue = "";
            this.cdvCustomer.sCodeColumnName = "CUST_CODE";
            this.cdvCustomer.sDynamicQuery = "SELECT DECODE(KEY_1, \'-\', \' \', KEY_1) AS CUST_CODE, DECODE(DATA_1, \'미확인\', \'전체\', D" +
                "ATA_1) AS CUST_DESC FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = \'HMKA1\' AND TABLE_N" +
                "AME = \'H_CUSTOMER\' ORDER BY KEY_1 ASC";
            this.cdvCustomer.sFactory = "";
            this.cdvCustomer.Size = new System.Drawing.Size(160, 21);
            this.cdvCustomer.sTableName = "";
            this.cdvCustomer.sValueColumnName = "CUST_DESC";
            this.cdvCustomer.TabIndex = 73;
            this.cdvCustomer.VisibleValueButton = true;
            // 
            // cdvPackage
            // 
            this.cdvPackage.BackColor = System.Drawing.Color.Transparent;
            this.cdvPackage.bMultiSelect = false;
            this.cdvPackage.ConditionText = "Package";
            this.cdvPackage.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvPackage.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvPackage.Location = new System.Drawing.Point(650, 2);
            this.cdvPackage.MandatoryFlag = false;
            this.cdvPackage.Name = "cdvPackage";
            this.cdvPackage.ParentValue = "";
            this.cdvPackage.sCodeColumnName = "PKG";
            this.cdvPackage.sDynamicQuery = "SELECT DECODE(KEY_1, \'-\', \' \', KEY_1) AS PKG, DATA_1 AS PKG_DESC FROM MGCMTBLDAT@" +
                "RPTTOMES WHERE FACTORY = \'HMKA1\' AND TABLE_NAME = \'H_PACKAGE\' ORDER BY KEY_1 ASC" +
                "";
            this.cdvPackage.sFactory = "";
            this.cdvPackage.Size = new System.Drawing.Size(160, 21);
            this.cdvPackage.sTableName = "";
            this.cdvPackage.sValueColumnName = "PKG_DESC";
            this.cdvPackage.TabIndex = 74;
            this.cdvPackage.VisibleValueButton = true;
            // 
            // gbGubun
            // 
            this.gbGubun.Controls.Add(this.rbSearch04);
            this.gbGubun.Controls.Add(this.rbSearch03);
            this.gbGubun.Controls.Add(this.label4);
            this.gbGubun.Controls.Add(this.rbSearch02);
            this.gbGubun.Controls.Add(this.rbSearch01);
            this.gbGubun.Location = new System.Drawing.Point(10, -6);
            this.gbGubun.Name = "gbGubun";
            this.gbGubun.Size = new System.Drawing.Size(486, 30);
            this.gbGubun.TabIndex = 76;
            this.gbGubun.TabStop = false;
            // 
            // rbSearch04
            // 
            this.rbSearch04.AutoSize = true;
            this.rbSearch04.Location = new System.Drawing.Point(400, 9);
            this.rbSearch04.Name = "rbSearch04";
            this.rbSearch04.Size = new System.Drawing.Size(81, 17);
            this.rbSearch04.TabIndex = 74;
            this.rbSearch04.Text = "Monthly (total)";
            this.rbSearch04.UseVisualStyleBackColor = true;
            this.rbSearch04.CheckedChanged += new System.EventHandler(this.rbSearch_CheckedChanged);
            // 
            // rbSearch03
            // 
            this.rbSearch03.AutoSize = true;
            this.rbSearch03.Location = new System.Drawing.Point(302, 9);
            this.rbSearch03.Name = "rbSearch03";
            this.rbSearch03.Size = new System.Drawing.Size(93, 17);
            this.rbSearch03.TabIndex = 73;
            this.rbSearch03.Text = "By date (total)";
            this.rbSearch03.UseVisualStyleBackColor = true;
            this.rbSearch03.CheckedChanged += new System.EventHandler(this.rbSearch_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "inquiry  Category:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbSearch02
            // 
            this.rbSearch02.AutoSize = true;
            this.rbSearch02.Location = new System.Drawing.Point(192, 9);
            this.rbSearch02.Name = "rbSearch02";
            this.rbSearch02.Size = new System.Drawing.Size(105, 17);
            this.rbSearch02.TabIndex = 71;
            this.rbSearch02.Text = "Monthly (search condition)";
            this.rbSearch02.UseVisualStyleBackColor = true;
            this.rbSearch02.CheckedChanged += new System.EventHandler(this.rbSearch_CheckedChanged);
            // 
            // rbSearch01
            // 
            this.rbSearch01.AutoSize = true;
            this.rbSearch01.Checked = true;
            this.rbSearch01.Location = new System.Drawing.Point(70, 9);
            this.rbSearch01.Name = "rbSearch01";
            this.rbSearch01.Size = new System.Drawing.Size(117, 17);
            this.rbSearch01.TabIndex = 70;
            this.rbSearch01.TabStop = true;
            this.rbSearch01.Text = "By date (search condition)";
            this.rbSearch01.UseVisualStyleBackColor = true;
            this.rbSearch01.CheckedChanged += new System.EventHandler(this.rbSearch_CheckedChanged);
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Controls.Add(this.label1);
            this.cdvFromToDate.Location = new System.Drawing.Point(78, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(232, 21);
            this.cdvFromToDate.TabIndex = 77;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Period";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 78;
            this.label3.Text = "inquiry period:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblImform
            // 
            this.lblImform.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblImform.AutoSize = true;
            this.lblImform.ForeColor = System.Drawing.Color.Red;
            this.lblImform.Location = new System.Drawing.Point(439, 5);
            this.lblImform.Name = "lblImform";
            this.lblImform.Size = new System.Drawing.Size(375, 13);
            this.lblImform.TabIndex = 77;
            this.lblImform.Text = "When you select the package with the client, the chart is displayed normally.";
            this.lblImform.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkKpcs
            // 
            this.chkKpcs.AutoSize = true;
            this.chkKpcs.Location = new System.Drawing.Point(502, 4);
            this.chkKpcs.Name = "chkKpcs";
            this.chkKpcs.Size = new System.Drawing.Size(47, 17);
            this.chkKpcs.TabIndex = 79;
            this.chkKpcs.Text = "kpcs";
            this.chkKpcs.UseVisualStyleBackColor = true;
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
            this.udcMSChart1.Size = new System.Drawing.Size(814, 253);
            this.udcMSChart1.TabIndex = 1;
            this.udcMSChart1.Text = "udcMSChart1";
            // 
            // TRN090102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TRN090102";
            this.Size = new System.Drawing.Size(820, 600);
            this.pnlMiddle.ResumeLayout(false);
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
            this.pnlDetailCondition3.ResumeLayout(false);
            this.pnlBUMPDetail.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.scCenter.Panel1.ResumeLayout(false);
            this.scCenter.Panel2.ResumeLayout(false);
            this.scCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            this.gbGubun.ResumeLayout(false);
            this.gbGubun.PerformLayout();
            this.cdvFromToDate.ResumeLayout(false);
            this.cdvFromToDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
		private System.Windows.Forms.SplitContainer scCenter;
		private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
		private FarPoint.Win.Spread.SheetView SS01_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvCustomer;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvPackage;
        private System.Windows.Forms.GroupBox gbGubun;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbSearch02;
        private System.Windows.Forms.RadioButton rbSearch01;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblImform;
        private System.Windows.Forms.RadioButton rbSearch04;
        private System.Windows.Forms.RadioButton rbSearch03;
        private System.Windows.Forms.CheckBox chkKpcs;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
    }
}
