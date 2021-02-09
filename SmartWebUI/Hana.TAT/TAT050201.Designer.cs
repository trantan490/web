namespace Hana.TAT
{
    partial class TAT050201
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TAT050201));
            this.udcDurationDate1 = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.udcChartFX2 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.chkGraph2 = new System.Windows.Forms.CheckBox();
            this.chkGraph1 = new System.Windows.Forms.CheckBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblType2 = new System.Windows.Forms.Label();
            this.cdvGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbOutWait = new System.Windows.Forms.CheckBox();
            this.ckbInWait = new System.Windows.Forms.CheckBox();
            this.ckbTime = new System.Windows.Forms.CheckBox();
            this.cbLotType = new System.Windows.Forms.ComboBox();
            this.lblLotType = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
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
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.pnlLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(57, 13);
            this.lblTitle.Text = "Trend(1)";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.ckbTime);
            this.pnlCondition2.Controls.Add(this.ckbOutWait);
            this.pnlCondition2.Controls.Add(this.ckbInWait);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.cdvGroup);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.pnlLabel);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.lblLotType);
            this.pnlCondition1.Controls.Add(this.cbLotType);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.udcDurationDate1);
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
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // udcDurationDate1
            // 
            this.udcDurationDate1.BackColor = System.Drawing.Color.Transparent;
            this.udcDurationDate1.Location = new System.Drawing.Point(203, 2);
            this.udcDurationDate1.Name = "udcDurationDate1";
            this.udcDurationDate1.RestrictedDayCount = 60;
            this.udcDurationDate1.Size = new System.Drawing.Size(280, 21);
            this.udcDurationDate1.TabIndex = 17;
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel1.Enabled = false;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spdData);
            this.splitContainer1.Size = new System.Drawing.Size(794, 418);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.udcChartFX1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.udcChartFX2);
            this.splitContainer3.Size = new System.Drawing.Size(794, 221);
            this.splitContainer3.SplitterDistance = 104;
            this.splitContainer3.TabIndex = 0;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AutoSize = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Size = new System.Drawing.Size(794, 104);
            this.udcChartFX1.TabIndex = 27;
            // 
            // udcChartFX2
            // 
            this.udcChartFX2.AutoSize = true;
            this.udcChartFX2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX2.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX2.Name = "udcChartFX2";
            this.udcChartFX2.Size = new System.Drawing.Size(794, 113);
            this.udcChartFX2.TabIndex = 27;
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
            this.spdData.Size = new System.Drawing.Size(794, 193);
            this.spdData.TabIndex = 1;
            this.spdData.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spdData_CellDoubleClick);
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
            this.cdvFactory.TabIndex = 18;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // pnlLabel
            // 
            this.pnlLabel.BackColor = System.Drawing.Color.Transparent;
            this.pnlLabel.Controls.Add(this.chkGraph2);
            this.pnlLabel.Controls.Add(this.chkGraph1);
            this.pnlLabel.Controls.Add(this.lblIcon);
            this.pnlLabel.Controls.Add(this.lblType2);
            this.pnlLabel.Location = new System.Drawing.Point(203, 2);
            this.pnlLabel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(216, 21);
            this.pnlLabel.TabIndex = 20;
            // 
            // chkGraph2
            // 
            this.chkGraph2.Location = new System.Drawing.Point(142, 2);
            this.chkGraph2.Name = "chkGraph2";
            this.chkGraph2.Size = new System.Drawing.Size(69, 17);
            this.chkGraph2.TabIndex = 3;
            this.chkGraph2.Text = "Graph(2)";
            this.chkGraph2.UseVisualStyleBackColor = true;
            // 
            // chkGraph1
            // 
            this.chkGraph1.Checked = true;
            this.chkGraph1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGraph1.Location = new System.Drawing.Point(67, 2);
            this.chkGraph1.Name = "chkGraph1";
            this.chkGraph1.Size = new System.Drawing.Size(69, 17);
            this.chkGraph1.TabIndex = 2;
            this.chkGraph1.Text = "Graph(1)";
            this.chkGraph1.UseVisualStyleBackColor = true;
            // 
            // lblIcon
            // 
            this.lblIcon.BackColor = System.Drawing.Color.White;
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(5, 2);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 1;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblType2
            // 
            this.lblType2.BackColor = System.Drawing.Color.Transparent;
            this.lblType2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblType2.Location = new System.Drawing.Point(17, 1);
            this.lblType2.Name = "lblType2";
            this.lblType2.Size = new System.Drawing.Size(60, 17);
            this.lblType2.TabIndex = 0;
            this.lblType2.Text = "Graph";
            this.lblType2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvGroup
            // 
            this.cdvGroup.FormattingEnabled = true;
            this.cdvGroup.Items.AddRange(new object[] {
            "ASSY_TAT",
            "FRONT",
            "BACK_END",
            "HMK2A",
            "SAW",
            "D/A",
            "W/B",
            "GATE",
            "MOLD",
            "FINISH",
            "HMK3A"});
            this.cdvGroup.Location = new System.Drawing.Point(84, 2);
            this.cdvGroup.Name = "cdvGroup";
            this.cdvGroup.Size = new System.Drawing.Size(100, 21);
            this.cdvGroup.TabIndex = 21;
            this.cdvGroup.Text = "ASSY_TAT";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(6, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(6, 17);
            this.label2.TabIndex = 5;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(18, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Group";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbOutWait
            // 
            this.ckbOutWait.AutoSize = true;
            this.ckbOutWait.Checked = true;
            this.ckbOutWait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbOutWait.Location = new System.Drawing.Point(539, 4);
            this.ckbOutWait.Name = "ckbOutWait";
            this.ckbOutWait.Size = new System.Drawing.Size(101, 17);
            this.ckbOutWait.TabIndex = 23;
            this.ckbOutWait.Text = "Including shipping wait";
            this.ckbOutWait.UseVisualStyleBackColor = true;
            // 
            // ckbInWait
            // 
            this.ckbInWait.AutoSize = true;
            this.ckbInWait.Checked = true;
            this.ckbInWait.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbInWait.Location = new System.Drawing.Point(438, 4);
            this.ckbInWait.Name = "ckbInWait";
            this.ckbInWait.Size = new System.Drawing.Size(101, 17);
            this.ckbInWait.TabIndex = 22;
            this.ckbInWait.Text = "Include wait for input";
            this.ckbInWait.UseVisualStyleBackColor = true;
            // 
            // ckbTime
            // 
            this.ckbTime.AutoSize = true;
            this.ckbTime.Location = new System.Drawing.Point(646, 4);
            this.ckbTime.Name = "ckbTime";
            this.ckbTime.Size = new System.Drawing.Size(74, 17);
            this.ckbTime.TabIndex = 19;
            this.ckbTime.Text = "Time unit";
            this.ckbTime.UseVisualStyleBackColor = true;
            // 
            // cbLotType
            // 
            this.cbLotType.FormattingEnabled = true;
            this.cbLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cbLotType.Location = new System.Drawing.Point(512, 2);
            this.cbLotType.Name = "cbLotType";
            this.cbLotType.Size = new System.Drawing.Size(98, 21);
            this.cbLotType.TabIndex = 30;
            // 
            // lblLotType
            // 
            this.lblLotType.AutoSize = true;
            this.lblLotType.Location = new System.Drawing.Point(457, 4);
            this.lblLotType.Name = "lblLotType";
            this.lblLotType.Size = new System.Drawing.Size(49, 13);
            this.lblLotType.TabIndex = 31;
            this.lblLotType.Text = "Lot Type";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(447, -20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 24;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(445, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 32;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TAT050201
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TAT050201";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.pnlLabel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcDurationDate1;
        private System.Windows.Forms.SplitContainer splitContainer1;        
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlLabel;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblType2;
        private System.Windows.Forms.CheckBox chkGraph2;
        private System.Windows.Forms.CheckBox chkGraph1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cdvGroup;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbOutWait;
        private System.Windows.Forms.CheckBox ckbInWait;
        private System.Windows.Forms.CheckBox ckbTime;
        private System.Windows.Forms.ComboBox cbLotType;
        private System.Windows.Forms.Label lblLotType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}
