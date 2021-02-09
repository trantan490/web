namespace Hana.TAT
{
    partial class TAT050801
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TAT050801));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.udcFarPoint1 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.udcFarPoint1_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.ckbTime = new System.Windows.Forms.CheckBox();
            this.lblBaseDate = new System.Windows.Forms.Label();
            this.cdvBaseDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblLotType = new System.Windows.Forms.Label();
            this.cbLotType = new System.Windows.Forms.ComboBox();
            this.ckbHMK3A = new System.Windows.Forms.CheckBox();
            this.ckbHMK2A = new System.Windows.Forms.CheckBox();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPoint1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPoint1_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(47, 13);
            this.lblTitle.Text = "Trend3";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.ckbHMK3A);
            this.pnlCondition2.Controls.Add(this.ckbHMK2A);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.lblLotType);
            this.pnlCondition1.Controls.Add(this.cbLotType);
            this.pnlCondition1.Controls.Add(this.lblBaseDate);
            this.pnlCondition1.Controls.Add(this.cdvBaseDate);
            this.pnlCondition1.Controls.Add(this.ckbTime);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.lblIcon);
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
            this.udcWIPCondition3.bMultiSelect = false;
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
            this.pnlMain.Location = new System.Drawing.Point(0, 179);
            this.pnlMain.Size = new System.Drawing.Size(800, 421);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDetail.Location = new System.Drawing.Point(85, 3);
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
            this.btnSort.Location = new System.Drawing.Point(6, 2);
            this.btnSort.Visible = false;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
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
            this.splitContainer1.Panel1.Controls.Add(this.udcChartFX1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.udcFarPoint1);
            this.splitContainer1.Panel2.Controls.Add(this.spdData);
            this.splitContainer1.Size = new System.Drawing.Size(794, 418);
            this.splitContainer1.SplitterDistance = 185;
            this.splitContainer1.TabIndex = 0;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Size = new System.Drawing.Size(794, 185);
            this.udcChartFX1.TabIndex = 26;
            // 
            // udcFarPoint1
            // 
            this.udcFarPoint1.About = "4.0.2001.2005";
            this.udcFarPoint1.AccessibleDescription = "udcFarPoint1, Sheet1, Row 0, Column 0, ";
            this.udcFarPoint1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.udcFarPoint1.Location = new System.Drawing.Point(25, 16);
            this.udcFarPoint1.Name = "udcFarPoint1";
            this.udcFarPoint1.RPT_IsPreCellsType = true;
            this.udcFarPoint1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.udcFarPoint1_Sheet1});
            this.udcFarPoint1.Size = new System.Drawing.Size(279, 158);
            this.udcFarPoint1.TabIndex = 1;
            this.udcFarPoint1.Visible = false;
            // 
            // udcFarPoint1_Sheet1
            // 
            this.udcFarPoint1_Sheet1.Reset();
            this.udcFarPoint1_Sheet1.SheetName = "Sheet1";
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
            this.spdData.Size = new System.Drawing.Size(794, 229);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.Location = new System.Drawing.Point(6, 0);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 19;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // lblIcon
            // 
            this.lblIcon.BackColor = System.Drawing.Color.White;
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(206, 5);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 1;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbTime
            // 
            this.ckbTime.AutoSize = true;
            this.ckbTime.Location = new System.Drawing.Point(567, 4);
            this.ckbTime.Name = "ckbTime";
            this.ckbTime.Size = new System.Drawing.Size(77, 17);
            this.ckbTime.TabIndex = 21;
            this.ckbTime.Text = "Time unit";
            this.ckbTime.UseVisualStyleBackColor = true;
            // 
            // lblBaseDate
            // 
            this.lblBaseDate.AutoSize = true;
            this.lblBaseDate.Location = new System.Drawing.Point(218, 5);
            this.lblBaseDate.Name = "lblBaseDate";
            this.lblBaseDate.Size = new System.Drawing.Size(62, 13);
            this.lblBaseDate.TabIndex = 78;
            this.lblBaseDate.Text = "Standard date:";
            // 
            // cdvBaseDate
            // 
            this.cdvBaseDate.CustomFormat = "yyyy-MM-dd";
            this.cdvBaseDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvBaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvBaseDate.Location = new System.Drawing.Point(283, 1);
            this.cdvBaseDate.Name = "cdvBaseDate";
            this.cdvBaseDate.Size = new System.Drawing.Size(100, 20);
            this.cdvBaseDate.TabIndex = 77;
            this.cdvBaseDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvBaseDate.Value = new System.DateTime(2008, 12, 2, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(394, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 81;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLotType
            // 
            this.lblLotType.AutoSize = true;
            this.lblLotType.Location = new System.Drawing.Point(406, 4);
            this.lblLotType.Name = "lblLotType";
            this.lblLotType.Size = new System.Drawing.Size(49, 13);
            this.lblLotType.TabIndex = 80;
            this.lblLotType.Text = "Lot Type";
            // 
            // cbLotType
            // 
            this.cbLotType.FormattingEnabled = true;
            this.cbLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cbLotType.Location = new System.Drawing.Point(461, 2);
            this.cbLotType.Name = "cbLotType";
            this.cbLotType.Size = new System.Drawing.Size(98, 21);
            this.cbLotType.TabIndex = 79;
            // 
            // ckbHMK3A
            // 
            this.ckbHMK3A.AutoSize = true;
            this.ckbHMK3A.Checked = true;
            this.ckbHMK3A.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHMK3A.Location = new System.Drawing.Point(122, 6);
            this.ckbHMK3A.Name = "ckbHMK3A";
            this.ckbHMK3A.Size = new System.Drawing.Size(101, 17);
            this.ckbHMK3A.TabIndex = 3;
            this.ckbHMK3A.Text = "Including shipping wait";
            this.ckbHMK3A.UseVisualStyleBackColor = true;
            // 
            // ckbHMK2A
            // 
            this.ckbHMK2A.AutoSize = true;
            this.ckbHMK2A.Checked = true;
            this.ckbHMK2A.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHMK2A.Location = new System.Drawing.Point(21, 6);
            this.ckbHMK2A.Name = "ckbHMK2A";
            this.ckbHMK2A.Size = new System.Drawing.Size(101, 17);
            this.ckbHMK2A.TabIndex = 2;
            this.ckbHMK2A.Text = "Include wait for input";
            this.ckbHMK2A.UseVisualStyleBackColor = true;
            // 
            // TAT050801
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TAT050801";
            this.Load += new System.EventHandler(this.TAT050801_Load);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPoint1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPoint1_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint udcFarPoint1;
        private FarPoint.Win.Spread.SheetView udcFarPoint1_Sheet1;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.CheckBox ckbTime;
        private System.Windows.Forms.Label lblBaseDate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvBaseDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLotType;
        private System.Windows.Forms.ComboBox cbLotType;
        private System.Windows.Forms.CheckBox ckbHMK3A;
        private System.Windows.Forms.CheckBox ckbHMK2A;
    }
}
