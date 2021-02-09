namespace Miracom.SmartWeb.UI
{
    partial class TST1108
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
            this.cdvFuncGrp = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblFuncGrp = new System.Windows.Forms.Label();
            this.optPeriod = new System.Windows.Forms.RadioButton();
            this.optLastMonth = new System.Windows.Forms.RadioButton();
            this.optLastWeek = new System.Windows.Forms.RadioButton();
            this.optYesterday = new System.Windows.Forms.RadioButton();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.optThisMonth = new System.Windows.Forms.RadioButton();
            this.optThisWeek = new System.Windows.Forms.RadioButton();
            this.optToday = new System.Windows.Forms.RadioButton();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFuncGrp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(120, 13);
            this.lblTitle.Text = "Function Log Report";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.dtpFromDate);
            this.pnlCondition3.Controls.Add(this.optThisMonth);
            this.pnlCondition3.Controls.Add(this.dtpToDate);
            this.pnlCondition3.Controls.Add(this.optThisWeek);
            this.pnlCondition3.Controls.Add(this.optToday);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.optPeriod);
            this.pnlCondition2.Controls.Add(this.optLastMonth);
            this.pnlCondition2.Controls.Add(this.optLastWeek);
            this.pnlCondition2.Controls.Add(this.optYesterday);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFuncGrp);
            this.pnlCondition1.Controls.Add(this.lblFuncGrp);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 176);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Location = new System.Drawing.Point(0, 116);
            this.pnlMain.Size = new System.Drawing.Size(800, 484);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 263);
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
            this.splitContainer1.Size = new System.Drawing.Size(794, 481);
            this.splitContainer1.SplitterDistance = 237;
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
            this.spdData.Size = new System.Drawing.Size(794, 237);
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
            this.udcMSChart1.Size = new System.Drawing.Size(794, 240);
            this.udcMSChart1.TabIndex = 0;
            this.udcMSChart1.Text = "udcMSChart1";
            // 
            // cdvFuncGrp
            // 
            this.cdvFuncGrp.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvFuncGrp.BorderHotColor = System.Drawing.Color.Black;
            this.cdvFuncGrp.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvFuncGrp.BtnToolTipText = "";
            this.cdvFuncGrp.DescText = "";
            this.cdvFuncGrp.DisplaySubItemIndex = -1;
            this.cdvFuncGrp.DisplayText = "";
            this.cdvFuncGrp.Focusing = null;
            this.cdvFuncGrp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFuncGrp.Index = 0;
            this.cdvFuncGrp.IsViewBtnImage = false;
            this.cdvFuncGrp.Location = new System.Drawing.Point(128, 2);
            this.cdvFuncGrp.MaxLength = 32767;
            this.cdvFuncGrp.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvFuncGrp.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvFuncGrp.MultiSelect = false;
            this.cdvFuncGrp.Name = "cdvFuncGrp";
            this.cdvFuncGrp.ReadOnly = true;
            this.cdvFuncGrp.SearchSubItemIndex = 0;
            this.cdvFuncGrp.SelectedDescIndex = -1;
            this.cdvFuncGrp.SelectedDescToQueryText = "";
            this.cdvFuncGrp.SelectedSubItemIndex = -1;
            this.cdvFuncGrp.SelectedValueToQueryText = "";
            this.cdvFuncGrp.SelectionStart = 0;
            this.cdvFuncGrp.Size = new System.Drawing.Size(150, 21);
            this.cdvFuncGrp.SmallImageList = null;
            this.cdvFuncGrp.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvFuncGrp.TabIndex = 52;
            this.cdvFuncGrp.TextBoxToolTipText = "";
            this.cdvFuncGrp.TextBoxWidth = 150;
            this.cdvFuncGrp.VisibleButton = true;
            this.cdvFuncGrp.VisibleColumnHeader = false;
            this.cdvFuncGrp.VisibleDescription = false;
            this.cdvFuncGrp.ButtonPress += new System.EventHandler(this.cdvFuncGrp_ButtonPress);
            // 
            // lblFuncGrp
            // 
            this.lblFuncGrp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblFuncGrp.Location = new System.Drawing.Point(18, 5);
            this.lblFuncGrp.Name = "lblFuncGrp";
            this.lblFuncGrp.Size = new System.Drawing.Size(115, 21);
            this.lblFuncGrp.TabIndex = 51;
            this.lblFuncGrp.Text = "FUNCTION GROUP";
            // 
            // optPeriod
            // 
            this.optPeriod.AutoSize = true;
            this.optPeriod.Checked = true;
            this.optPeriod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optPeriod.Location = new System.Drawing.Point(248, 5);
            this.optPeriod.Name = "optPeriod";
            this.optPeriod.Size = new System.Drawing.Size(61, 18);
            this.optPeriod.TabIndex = 49;
            this.optPeriod.TabStop = true;
            this.optPeriod.Text = "Period";
            this.optPeriod.UseVisualStyleBackColor = true;
            this.optPeriod.CheckedChanged += new System.EventHandler(this.optPeriod_CheckedChanged);
            // 
            // optLastMonth
            // 
            this.optLastMonth.AutoSize = true;
            this.optLastMonth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optLastMonth.Location = new System.Drawing.Point(169, 5);
            this.optLastMonth.Name = "optLastMonth";
            this.optLastMonth.Size = new System.Drawing.Size(84, 18);
            this.optLastMonth.TabIndex = 48;
            this.optLastMonth.Text = "Last Month";
            this.optLastMonth.UseVisualStyleBackColor = true;
            // 
            // optLastWeek
            // 
            this.optLastWeek.AutoSize = true;
            this.optLastWeek.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optLastWeek.Location = new System.Drawing.Point(95, 5);
            this.optLastWeek.Name = "optLastWeek";
            this.optLastWeek.Size = new System.Drawing.Size(81, 18);
            this.optLastWeek.TabIndex = 47;
            this.optLastWeek.Text = "Last Week";
            this.optLastWeek.UseVisualStyleBackColor = true;
            // 
            // optYesterday
            // 
            this.optYesterday.AutoSize = true;
            this.optYesterday.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optYesterday.Location = new System.Drawing.Point(21, 5);
            this.optYesterday.Name = "optYesterday";
            this.optYesterday.Size = new System.Drawing.Size(80, 18);
            this.optYesterday.TabIndex = 46;
            this.optYesterday.Text = "Yesterday";
            this.optYesterday.UseVisualStyleBackColor = true;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(249, 3);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(90, 21);
            this.dtpFromDate.TabIndex = 50;
            // 
            // optThisMonth
            // 
            this.optThisMonth.AutoSize = true;
            this.optThisMonth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optThisMonth.Location = new System.Drawing.Point(169, 4);
            this.optThisMonth.Name = "optThisMonth";
            this.optThisMonth.Size = new System.Drawing.Size(83, 18);
            this.optThisMonth.TabIndex = 54;
            this.optThisMonth.Text = "This Month";
            this.optThisMonth.UseVisualStyleBackColor = true;
            // 
            // optThisWeek
            // 
            this.optThisWeek.AutoSize = true;
            this.optThisWeek.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optThisWeek.Location = new System.Drawing.Point(95, 4);
            this.optThisWeek.Name = "optThisWeek";
            this.optThisWeek.Size = new System.Drawing.Size(80, 18);
            this.optThisWeek.TabIndex = 53;
            this.optThisWeek.Text = "This Week";
            this.optThisWeek.UseVisualStyleBackColor = true;
            // 
            // optToday
            // 
            this.optToday.AutoSize = true;
            this.optToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optToday.Location = new System.Drawing.Point(21, 4);
            this.optToday.Name = "optToday";
            this.optToday.Size = new System.Drawing.Size(61, 18);
            this.optToday.TabIndex = 52;
            this.optToday.Text = "Today";
            this.optToday.UseVisualStyleBackColor = true;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(343, 3);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(90, 21);
            this.dtpToDate.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(327, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 13);
            this.label1.TabIndex = 50;
            this.label1.Text = "※ Click the screen name to display the daily trend chart.";
            // 
            // TST1108
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TST1108";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.cdvFuncGrp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvFuncGrp;
        private System.Windows.Forms.Label lblFuncGrp;
        private System.Windows.Forms.RadioButton optPeriod;
        private System.Windows.Forms.RadioButton optLastMonth;
        private System.Windows.Forms.RadioButton optLastWeek;
        private System.Windows.Forms.RadioButton optYesterday;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.RadioButton optThisMonth;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.RadioButton optThisWeek;
        private System.Windows.Forms.RadioButton optToday;
        private Controls.udcMSChart udcMSChart1;
        private System.Windows.Forms.Label label1;
    }
}
