namespace Hana.TAT
{
    partial class TRN090105
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
			this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.udcMSChart1 = new Miracom.SmartWeb.UI.Controls.udcMSChart();
			this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
			this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
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
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Size = new System.Drawing.Size(114, 13);
            this.lblTitle.Text = "Samsung Warehousing Status (KIT)";
			// 
			// pnlMiddle
			// 
			this.pnlMiddle.Size = new System.Drawing.Size(800, 42);
			// 
			// pnlCondition1
			// 
			this.pnlCondition1.Controls.Add(this.cdvDate);
			this.pnlCondition1.Controls.Add(this.cdvFactory);
			// 
			// pnlWIPDetail
			// 
			this.pnlWIPDetail.Location = new System.Drawing.Point(0, 128);
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
			this.udcWIPCondition1.bMultiSelect = false;
			this.udcWIPCondition1.RefFactory = this.cdvFactory;
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.splitContainer1);
			this.pnlMain.Location = new System.Drawing.Point(0, 68);
			this.pnlMain.Size = new System.Drawing.Size(800, 532);
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
			this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
			// 
			// udcWIPCondition9
			// 
			this.udcWIPCondition9.RefFactory = this.cdvFactory;
			// 
			// pnlBUMPDetail
			// 
			this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 215);
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
			this.splitContainer1.Panel2.Controls.Add(this.udcMSChart1);
			this.splitContainer1.Size = new System.Drawing.Size(794, 529);
			this.splitContainer1.SplitterDistance = 265;
			this.splitContainer1.TabIndex = 0;
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
			this.spdData.Size = new System.Drawing.Size(794, 265);
			this.spdData.TabIndex = 1;
			this.spdData.EnterCell += new FarPoint.Win.Spread.EnterCellEventHandler(this.spdData_EnterCell);
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
			this.udcMSChart1.Size = new System.Drawing.Size(794, 260);
			this.udcMSChart1.TabIndex = 0;
			this.udcMSChart1.Text = "udcMSChart1";
			// 
			// cdvFactory
			// 
			this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
			this.cdvFactory.bMultiSelect = false;
			this.cdvFactory.ConditionText = "Factory";
			this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
			this.cdvFactory.ControlRef = true;
			this.cdvFactory.Enabled = false;
			this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
			this.cdvFactory.Location = new System.Drawing.Point(6, 0);
			this.cdvFactory.MandatoryFlag = true;
			this.cdvFactory.Name = "cdvFactory";
			this.cdvFactory.ParentValue = "";
			this.cdvFactory.sCodeColumnName = "CODE";
			this.cdvFactory.sDynamicQuery = "";			
			this.cdvFactory.Size = new System.Drawing.Size(180, 21);
			this.cdvFactory.sTableName = "";
			this.cdvFactory.sValueColumnName = "DATA";
			this.cdvFactory.TabIndex = 19;
			this.cdvFactory.VisibleValueButton = true;
			// 
			// cdvDate
			// 
			this.cdvDate.CustomFormat = "yyyy-MM-dd";
			this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
			this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.cdvDate.Location = new System.Drawing.Point(197, 1);
			this.cdvDate.Name = "cdvDate";
			this.cdvDate.Size = new System.Drawing.Size(111, 20);
			this.cdvDate.TabIndex = 20;
			this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
			this.cdvDate.Value = new System.DateTime(2010, 2, 9, 0, 0, 0, 0);
			// 
			// TRN090105
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
			this.ConditionCount = 1;
			this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
			this.FormStyle.FormName = null;
			this.Name = "TRN090105";
			this.pnlMiddle.ResumeLayout(false);
			this.pnlCondition1.ResumeLayout(false);
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
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        //private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
    }
}
