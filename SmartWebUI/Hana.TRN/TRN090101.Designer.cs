namespace Hana.TRN
{
    partial class TRN090101
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TRN090101));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.lblDate = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblLabel3 = new System.Windows.Forms.Label();
            this.lblLabel9 = new System.Windows.Forms.Label();
            this.lblLabel7 = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblJindo = new System.Windows.Forms.Label();
            this.lblLastDay = new System.Windows.Forms.Label();
            this.lbl = new System.Windows.Forms.Label();
            this.lblRemain = new System.Windows.Forms.Label();
            this.cdvLotType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(111, 13);
            this.lblTitle.Text = "ASSY, TEST achievement ratio";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 179);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 155);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 131);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 107);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 83);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 62);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 21);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.lblRemain);
            this.pnlCondition2.Controls.Add(this.lbl);
            this.pnlCondition2.Controls.Add(this.lblLastDay);
            this.pnlCondition2.Controls.Add(this.lblJindo);
            this.pnlCondition2.Controls.Add(this.lblToday);
            this.pnlCondition2.Controls.Add(this.lblLabel3);
            this.pnlCondition2.Controls.Add(this.lblLabel9);
            this.pnlCondition2.Controls.Add(this.lblLabel7);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Controls.Add(this.cdvDate);
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
            this.btnSort.Enabled = false;
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
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
            this.spdData.Size = new System.Drawing.Size(794, 184);
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
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(285, 1);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 16;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(217, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 13);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "standard date";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(207, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 18;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLabel3
            // 
            this.lblLabel3.AutoSize = true;
            this.lblLabel3.Location = new System.Drawing.Point(18, 6);
            this.lblLabel3.Name = "lblLabel3";
            this.lblLabel3.Size = new System.Drawing.Size(35, 13);
            this.lblLabel3.TabIndex = 0;
            this.lblLabel3.Text = "today";
            // 
            // lblLabel9
            // 
            this.lblLabel9.AutoSize = true;
            this.lblLabel9.Location = new System.Drawing.Point(207, 6);
            this.lblLabel9.Name = "lblLabel9";
            this.lblLabel9.Size = new System.Drawing.Size(48, 13);
            this.lblLabel9.TabIndex = 1;
            this.lblLabel9.Text = "workday";
            // 
            // lblLabel7
            // 
            this.lblLabel7.AutoSize = true;
            this.lblLabel7.Location = new System.Drawing.Point(574, 6);
            this.lblLabel7.Name = "lblLabel7";
            this.lblLabel7.Size = new System.Drawing.Size(67, 13);
            this.lblLabel7.TabIndex = 2;
            this.lblLabel7.Text = "Standard progress rate";
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(85, 9);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(0, 13);
            this.lblToday.TabIndex = 6;
            // 
            // lblJindo
            // 
            this.lblJindo.AutoSize = true;
            this.lblJindo.Location = new System.Drawing.Point(647, 6);
            this.lblJindo.Name = "lblJindo";
            this.lblJindo.Size = new System.Drawing.Size(0, 13);
            this.lblJindo.TabIndex = 8;
            // 
            // lblLastDay
            // 
            this.lblLastDay.AutoSize = true;
            this.lblLastDay.Location = new System.Drawing.Point(286, 6);
            this.lblLastDay.Name = "lblLastDay";
            this.lblLastDay.Size = new System.Drawing.Size(0, 13);
            this.lblLastDay.TabIndex = 9;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(375, 6);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(42, 13);
            this.lbl.TabIndex = 10;
            this.lbl.Text = "Remain";
            // 
            // lblRemain
            // 
            this.lblRemain.AutoSize = true;
            this.lblRemain.Location = new System.Drawing.Point(427, 6);
            this.lblRemain.Name = "lblRemain";
            this.lblRemain.Size = new System.Drawing.Size(0, 13);
            this.lblRemain.TabIndex = 11;
            // 
            // cdvLotType
            // 
            this.cdvLotType.BackColor = System.Drawing.Color.Transparent;
            this.cdvLotType.bMultiSelect = true;
            this.cdvLotType.ConditionText = "Lot Type";
            this.cdvLotType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvLotType.ControlRef = true;
            this.cdvLotType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLotType.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvLotType.Location = new System.Drawing.Point(417, 1);
            this.cdvLotType.MandatoryFlag = false;
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.ParentValue = "";
            this.cdvLotType.sCodeColumnName = "";
            this.cdvLotType.sDynamicQuery = "";            
            this.cdvLotType.Size = new System.Drawing.Size(180, 21);
            this.cdvLotType.sTableName = "H_TYPE";
            this.cdvLotType.sValueColumnName = "";
            this.cdvLotType.TabIndex = 9;
            this.cdvLotType.VisibleValueButton = true;
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
            this.cdvFactory.Location = new System.Drawing.Point(4, 3);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 44;
            this.cdvFactory.VisibleValueButton = true;
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
            this.udcMSChart1.Size = new System.Drawing.Size(794, 230);
            this.udcMSChart1.TabIndex = 2;
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
            this.splitContainer1.Panel2.Controls.Add(this.spdData);
            this.splitContainer1.Size = new System.Drawing.Size(794, 418);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 3;
            // 
            // TRN090101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TRN090101";
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblLabel3;
        private System.Windows.Forms.Label lblLabel7;
        private System.Windows.Forms.Label lblLabel9;
        private System.Windows.Forms.Label lblLastDay;
        private System.Windows.Forms.Label lblJindo;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblRemain;
        private System.Windows.Forms.Label lbl;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLotType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
