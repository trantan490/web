namespace Hana.RAS
{
    partial class RAS020502
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAS020502));
            SoftwareFX.ChartFX.TitleDockable titleDockable1 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.chkGroup = new System.Windows.Forms.CheckBox();
            this.cdvBaseDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cf01 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.spdData2 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData2_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cf02 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cdvResMaker = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData2_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(101, 13);
            this.lblTitle.Text = "Momentary stop share";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 42);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.chkGroup);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvResMaker);
            this.pnlCondition1.Controls.Add(this.cdvBaseDate);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.label2);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcRASCondition1;
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition1;
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
            this.cdvFactory.TabIndex = 13;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(206, 6);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 47;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Date";
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spdData.Location = new System.Drawing.Point(0, 0);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 115);
            this.spdData.TabIndex = 25;
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
            // chkGroup
            // 
            this.chkGroup.AutoSize = true;
            this.chkGroup.Location = new System.Drawing.Point(21, 6);
            this.chkGroup.Name = "chkGroup";
            this.chkGroup.Size = new System.Drawing.Size(62, 17);
            this.chkGroup.TabIndex = 50;
            this.chkGroup.Text = "By group";
            this.chkGroup.UseVisualStyleBackColor = true;
            // 
            // cdvBaseDate
            // 
            this.cdvBaseDate.CustomFormat = "yyyy-MM-dd";
            this.cdvBaseDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvBaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvBaseDate.Location = new System.Drawing.Point(251, 2);
            this.cdvBaseDate.Name = "cdvBaseDate";
            this.cdvBaseDate.Size = new System.Drawing.Size(106, 20);
            this.cdvBaseDate.TabIndex = 48;
            this.cdvBaseDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvBaseDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(794, 529);
            this.splitContainer1.SplitterDistance = 233;
            this.splitContainer1.TabIndex = 27;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.cf01);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.spdData);
            this.splitContainer2.Size = new System.Drawing.Size(794, 233);
            this.splitContainer2.SplitterDistance = 114;
            this.splitContainer2.TabIndex = 0;
            // 
            // cf01
            // 
            this.cf01.AxisX.Staggered = true;
            this.cf01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf01.Location = new System.Drawing.Point(0, 0);
            this.cf01.Name = "cf01";
            this.cf01.Scrollable = true;
            this.cf01.Size = new System.Drawing.Size(794, 114);
            this.cf01.TabIndex = 28;
            this.cf01.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable1});
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.spdData2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.cf02);
            this.splitContainer3.Size = new System.Drawing.Size(794, 292);
            this.splitContainer3.SplitterDistance = 264;
            this.splitContainer3.TabIndex = 0;
            // 
            // spdData2
            // 
            this.spdData2.About = "4.0.2001.2005";
            this.spdData2.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData2.BackColor = System.Drawing.Color.White;
            this.spdData2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData2.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            this.spdData2.Location = new System.Drawing.Point(0, 0);
            this.spdData2.Name = "spdData2";
            this.spdData2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData2.RPT_IsPreCellsType = true;
            this.spdData2.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData2_Sheet1});
            this.spdData2.Size = new System.Drawing.Size(264, 292);
            this.spdData2.TabIndex = 26;
            this.spdData2.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            // 
            // spdData2_Sheet1
            // 
            this.spdData2_Sheet1.Reset();
            this.spdData2_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdData2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdData2_Sheet1.ColumnCount = 0;
            this.spdData2_Sheet1.RowCount = 0;
            this.spdData2_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdData2_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdData2.SetActiveViewport(0, 1, 1);
            // 
            // cf02
            // 
            this.cf02.AxisX.Staggered = true;
            this.cf02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf02.Location = new System.Drawing.Point(0, 0);
            this.cf02.Name = "cf02";
            this.cf02.Scrollable = true;
            this.cf02.Size = new System.Drawing.Size(526, 292);
            this.cf02.TabIndex = 29;
            this.cf02.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
            // 
            // cdvResMaker
            // 
            this.cdvResMaker.BackColor = System.Drawing.Color.Transparent;
            this.cdvResMaker.bMultiSelect = false;
            this.cdvResMaker.ConditionText = "Maker";
            this.cdvResMaker.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvResMaker.ControlRef = true;
            this.cdvResMaker.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvResMaker.Location = new System.Drawing.Point(372, 2);
            this.cdvResMaker.MandatoryFlag = false;
            this.cdvResMaker.Name = "cdvResMaker";
            this.cdvResMaker.ParentValue = "";
            this.cdvResMaker.sCodeColumnName = "Code";
            this.cdvResMaker.sDynamicQuery = "";            
            this.cdvResMaker.Size = new System.Drawing.Size(180, 21);
            this.cdvResMaker.sTableName = "";
            this.cdvResMaker.sValueColumnName = "Data";
            this.cdvResMaker.TabIndex = 85;
            this.cdvResMaker.VisibleValueButton = true;
            this.cdvResMaker.ValueButtonPress += new System.EventHandler(this.cdvResMaker_ValueButtonPress);
            // 
            // RAS020502
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 1;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "RAS020502";
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData2_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.CheckBox chkGroup;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvBaseDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf01;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData2;
        private FarPoint.Win.Spread.SheetView spdData2_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf02;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvResMaker;
    }
}
