namespace Hana.RAS
{
    partial class RAS020107
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAS020107));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcMSChart1 = new Miracom.SmartWeb.UI.Controls.udcMSChart();
            this.label3 = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.chkUseStateN = new System.Windows.Forms.CheckBox();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(162, 13);
            this.lblTitle.Text = "Equipment UPEH VS Output(Simple)";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.txtProduct);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label2);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.chkUseStateN);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.ckbKpcs);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 239);
            this.pnlMain.Size = new System.Drawing.Size(800, 361);
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
            // udcRASCondition6
            // 
            this.udcRASCondition6.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition5
            // 
            this.udcRASCondition5.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition4
            // 
            this.udcRASCondition4.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition3
            // 
            this.udcRASCondition3.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition2
            // 
            this.udcRASCondition2.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition1
            // 
            this.udcRASCondition1.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition7
            // 
            this.udcRASCondition7.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 152);
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
            this.spdData.Size = new System.Drawing.Size(794, 176);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(4, 1);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";            
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 10;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(498, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 12;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(559, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 20;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(569, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Product";
            this.label2.Visible = false;
            // 
            // txtProduct
            // 
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Location = new System.Drawing.Point(636, 1);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(100, 21);
            this.txtProduct.TabIndex = 21;
            this.txtProduct.Text = "%";
            this.txtProduct.Visible = false;
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
            this.splitContainer1.Size = new System.Drawing.Size(794, 358);
            this.splitContainer1.SplitterDistance = 178;
            this.splitContainer1.TabIndex = 2;
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
            this.udcMSChart1.Size = new System.Drawing.Size(794, 178);
            this.udcMSChart1.TabIndex = 27;
            this.udcMSChart1.Text = "udcMSChart1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(18, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(274, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "※ Click the cell to display the data in the chart.";
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(204, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(287, 21);
            this.cdvFromToDate.TabIndex = 16;
            // 
            // chkUseStateN
            // 
            this.chkUseStateN.AutoSize = true;
            this.chkUseStateN.Location = new System.Drawing.Point(570, 4);
            this.chkUseStateN.Name = "chkUseStateN";
            this.chkUseStateN.Size = new System.Drawing.Size(97, 17);
            this.chkUseStateN.TabIndex = 17;
            this.chkUseStateN.Text = "USE_STATE(N)";
            this.chkUseStateN.UseVisualStyleBackColor = true;
            // 
            // RAS020107
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.RAS_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "RAS020107";
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udcMSChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Miracom.SmartWeb.UI.Controls.udcMSChart udcMSChart1;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.CheckBox chkUseStateN;
    }
}
