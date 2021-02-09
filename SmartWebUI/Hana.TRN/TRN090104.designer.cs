namespace Hana.TRN
{
    partial class TRN090104
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
            this.cdvCustomer = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvPackage = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblDate = new System.Windows.Forms.Label();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.gbGubun = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbS05 = new System.Windows.Forms.RadioButton();
            this.rbS04 = new System.Windows.Forms.RadioButton();
            this.rbS03 = new System.Windows.Forms.RadioButton();
            this.rbS02 = new System.Windows.Forms.RadioButton();
            this.rbS01 = new System.Windows.Forms.RadioButton();
            this.chkKpcs = new System.Windows.Forms.CheckBox();
            this.udcMSChart1 = new Miracom.SmartWeb.UI.Controls.udcMSChart();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
            this.gbGubun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(133, 13);
            this.lblTitle.Text = "Performance Status by Major Operation";
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
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.chkKpcs);
            this.pnlCondition2.Controls.Add(this.cdvCustomer);
            this.pnlCondition2.Controls.Add(this.cdvPackage);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.gbGubun);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            this.pnlCondition1.Controls.Add(this.lblDate);
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
            this.btnExcelExport.Visible = false;
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
            this.cdvFactory.Location = new System.Drawing.Point(640, 0);
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
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // cdvCustomer
            // 
            this.cdvCustomer.BackColor = System.Drawing.Color.Transparent;
            this.cdvCustomer.bMultiSelect = false;
            this.cdvCustomer.ConditionText = "Customer";
            this.cdvCustomer.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvCustomer.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvCustomer.Location = new System.Drawing.Point(1, 1);
            this.cdvCustomer.MandatoryFlag = false;
            this.cdvCustomer.Name = "cdvCustomer";
            this.cdvCustomer.ParentValue = "";
            this.cdvCustomer.sCodeColumnName = "CUST_CODE";
            this.cdvCustomer.sDynamicQuery = "SELECT DECODE(KEY_1, \'-\', \' \', KEY_1) AS CUST_CODE, DECODE(DATA_1, \'미확인\', \'전체\', D" +
                "ATA_1) AS CUST_DESC FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = \'HMKA1\' AND TABLE_N" +
                "AME = \'H_CUSTOMER\' ORDER BY KEY_1 ASC";
            this.cdvCustomer.sFactory = "";
            this.cdvCustomer.Size = new System.Drawing.Size(200, 21);
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
            this.cdvPackage.Location = new System.Drawing.Point(223, 1);
            this.cdvPackage.MandatoryFlag = false;
            this.cdvPackage.Name = "cdvPackage";
            this.cdvPackage.ParentValue = "";
            this.cdvPackage.sCodeColumnName = "PKG";
            this.cdvPackage.sDynamicQuery = "SELECT DECODE(KEY_1, \'-\', \' \', KEY_1) AS PKG, DATA_1 AS PKG_DESC FROM MGCMTBLDAT@" +
                "RPTTOMES WHERE FACTORY = \'HMKA1\' AND TABLE_NAME = \'H_PACKAGE\' ORDER BY KEY_1 ASC" +
                "";
            this.cdvPackage.sFactory = "";
            this.cdvPackage.Size = new System.Drawing.Size(200, 21);
            this.cdvPackage.sTableName = "";
            this.cdvPackage.sValueColumnName = "PKG_DESC";
            this.cdvPackage.TabIndex = 74;
            this.cdvPackage.VisibleValueButton = true;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(13, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(77, 16);
            this.lblDate.TabIndex = 78;
            this.lblDate.Text = "Reference year:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(80, 2);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.ShowUpDown = true;
            this.cdvDate.Size = new System.Drawing.Size(90, 20);
            this.cdvDate.TabIndex = 80;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.SpinedYM;
            this.cdvDate.UseWaitCursor = true;
            this.cdvDate.Value = new System.DateTime(2011, 3, 1, 0, 0, 0, 0);
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
            this.SS01.Size = new System.Drawing.Size(794, 308);
            this.SS01.TabIndex = 26;
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
            // gbGubun
            // 
            this.gbGubun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbGubun.Controls.Add(this.label1);
            this.gbGubun.Controls.Add(this.rbS05);
            this.gbGubun.Controls.Add(this.rbS04);
            this.gbGubun.Controls.Add(this.rbS03);
            this.gbGubun.Controls.Add(this.rbS02);
            this.gbGubun.Controls.Add(this.rbS01);
            this.gbGubun.Location = new System.Drawing.Point(184, -6);
            this.gbGubun.Name = "gbGubun";
            this.gbGubun.Size = new System.Drawing.Size(610, 30);
            this.gbGubun.TabIndex = 81;
            this.gbGubun.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(395, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "The chart is displayed by comparing the data of the month selected from the base month.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbS05
            // 
            this.rbS05.AutoSize = true;
            this.rbS05.Location = new System.Drawing.Point(243, 10);
            this.rbS05.Name = "rbS05";
            this.rbS05.Size = new System.Drawing.Size(55, 17);
            this.rbS05.TabIndex = 75;
            this.rbS05.Tag = "-5";
            this.rbS05.Text = "5 months";
            this.rbS05.UseVisualStyleBackColor = true;
            this.rbS05.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbS04
            // 
            this.rbS04.AutoSize = true;
            this.rbS04.Location = new System.Drawing.Point(184, 10);
            this.rbS04.Name = "rbS04";
            this.rbS04.Size = new System.Drawing.Size(55, 17);
            this.rbS04.TabIndex = 74;
            this.rbS04.Tag = "-4";
            this.rbS04.Text = "4 months";
            this.rbS04.UseVisualStyleBackColor = true;
            this.rbS04.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbS03
            // 
            this.rbS03.AutoSize = true;
            this.rbS03.Location = new System.Drawing.Point(125, 10);
            this.rbS03.Name = "rbS03";
            this.rbS03.Size = new System.Drawing.Size(55, 17);
            this.rbS03.TabIndex = 73;
            this.rbS03.Tag = "-3";
            this.rbS03.Text = "3 months";
            this.rbS03.UseVisualStyleBackColor = true;
            this.rbS03.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbS02
            // 
            this.rbS02.AutoSize = true;
            this.rbS02.Location = new System.Drawing.Point(66, 10);
            this.rbS02.Name = "rbS02";
            this.rbS02.Size = new System.Drawing.Size(55, 17);
            this.rbS02.TabIndex = 71;
            this.rbS02.Tag = "-2";
            this.rbS02.Text = "2 months";
            this.rbS02.UseVisualStyleBackColor = true;
            this.rbS02.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // rbS01
            // 
            this.rbS01.AutoSize = true;
            this.rbS01.Checked = true;
            this.rbS01.Location = new System.Drawing.Point(7, 10);
            this.rbS01.Name = "rbS01";
            this.rbS01.Size = new System.Drawing.Size(55, 17);
            this.rbS01.TabIndex = 70;
            this.rbS01.TabStop = true;
            this.rbS01.Tag = "-1";
            this.rbS01.Text = "1 month";
            this.rbS01.UseVisualStyleBackColor = true;
            this.rbS01.CheckedChanged += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // chkKpcs
            // 
            this.chkKpcs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkKpcs.AutoSize = true;
            this.chkKpcs.Location = new System.Drawing.Point(740, 3);
            this.chkKpcs.Name = "chkKpcs";
            this.chkKpcs.Size = new System.Drawing.Size(47, 17);
            this.chkKpcs.TabIndex = 80;
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
            this.udcMSChart1.Size = new System.Drawing.Size(794, 193);
            this.udcMSChart1.TabIndex = 27;
            this.udcMSChart1.Text = "udcMSChart1";
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
            this.splitContainer1.Panel2.Controls.Add(this.SS01);
            this.splitContainer1.Size = new System.Drawing.Size(794, 505);
            this.splitContainer1.SplitterDistance = 193;
            this.splitContainer1.TabIndex = 28;
            // 
            // TRN090104
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TRN090104";
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            this.gbGubun.ResumeLayout(false);
            this.gbGubun.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvCustomer;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvPackage;
        private System.Windows.Forms.Label lblDate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
        private FarPoint.Win.Spread.SheetView SS01_Sheet1;
        private System.Windows.Forms.GroupBox gbGubun;
        private System.Windows.Forms.RadioButton rbS04;
        private System.Windows.Forms.RadioButton rbS03;
        private System.Windows.Forms.RadioButton rbS02;
        private System.Windows.Forms.RadioButton rbS01;
        private System.Windows.Forms.RadioButton rbS05;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkKpcs;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
