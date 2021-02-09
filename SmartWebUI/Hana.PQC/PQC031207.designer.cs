namespace Hana.PQC
{
    partial class PQC031207
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
            SoftwareFX.ChartFX.TitleDockable titleDockable16 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable17 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable18 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable19 = new SoftwareFX.ChartFX.TitleDockable();
            SoftwareFX.ChartFX.TitleDockable titleDockable20 = new SoftwareFX.ChartFX.TitleDockable();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.scCenter = new System.Windows.Forms.SplitContainer();
            this.scLeft = new System.Windows.Forms.SplitContainer();
            this.cf01 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.scRight = new System.Windows.Forms.SplitContainer();
            this.cf02 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cf03 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cf04 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cf05 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cdvBaseDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.lblBaseDate = new System.Windows.Forms.Label();
            this.cbMemory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.groupBox3.SuspendLayout();
            this.scCenter.Panel1.SuspendLayout();
            this.scCenter.Panel2.SuspendLayout();
            this.scCenter.SuspendLayout();
            this.scLeft.Panel1.SuspendLayout();
            this.scLeft.Panel2.SuspendLayout();
            this.scLeft.SuspendLayout();
            this.scRight.Panel1.SuspendLayout();
            this.scRight.Panel2.SuspendLayout();
            this.scRight.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(96, 13);
            this.lblTitle.Text = "Quality Issue Status";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.groupBox3);
            this.pnlMiddle.Size = new System.Drawing.Size(800, 42);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition1, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.groupBox3, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition2, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition3, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition4, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition5, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition6, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition7, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition8, 0);
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
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvBaseDate);
            this.pnlCondition1.Controls.Add(this.lblBaseDate);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 128);
            // 
            // udcWIPCondition8
            // 
            this.udcWIPCondition8.Enabled = false;
            this.udcWIPCondition8.RefFactory = this.cdvFactory;
            this.udcWIPCondition8.Visible = false;
            // 
            // udcWIPCondition7
            // 
            this.udcWIPCondition7.Enabled = false;
            this.udcWIPCondition7.RefFactory = this.cdvFactory;
            this.udcWIPCondition7.Visible = false;
            // 
            // udcWIPCondition6
            // 
            this.udcWIPCondition6.Enabled = false;
            this.udcWIPCondition6.RefFactory = this.cdvFactory;
            this.udcWIPCondition6.Visible = false;
            // 
            // udcWIPCondition5
            // 
            this.udcWIPCondition5.Enabled = false;
            this.udcWIPCondition5.RefFactory = this.cdvFactory;
            this.udcWIPCondition5.Visible = false;
            // 
            // pnlDetailCondition1
            // 
            this.pnlDetailCondition1.Controls.Add(this.cbMemory);
            this.pnlDetailCondition1.Controls.Add(this.label3);
            this.pnlDetailCondition1.Controls.SetChildIndex(this.udcWIPCondition1, 0);
            this.pnlDetailCondition1.Controls.SetChildIndex(this.udcWIPCondition2, 0);
            this.pnlDetailCondition1.Controls.SetChildIndex(this.udcWIPCondition3, 0);
            this.pnlDetailCondition1.Controls.SetChildIndex(this.udcWIPCondition4, 0);
            this.pnlDetailCondition1.Controls.SetChildIndex(this.label3, 0);
            this.pnlDetailCondition1.Controls.SetChildIndex(this.cbMemory, 0);
            // 
            // udcWIPCondition4
            // 
            this.udcWIPCondition4.Enabled = false;
            this.udcWIPCondition4.RefFactory = this.cdvFactory;
            this.udcWIPCondition4.Visible = false;
            // 
            // udcWIPCondition3
            // 
            this.udcWIPCondition3.Enabled = false;
            this.udcWIPCondition3.RefFactory = this.cdvFactory;
            this.udcWIPCondition3.Visible = false;
            // 
            // udcWIPCondition2
            // 
            this.udcWIPCondition2.Enabled = false;
            this.udcWIPCondition2.RefFactory = this.cdvFactory;
            this.udcWIPCondition2.Visible = false;
            // 
            // udcWIPCondition1
            // 
            this.udcWIPCondition1.RefFactory = this.cdvFactory;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.scCenter);
            this.pnlMain.Location = new System.Drawing.Point(0, 68);
            this.pnlMain.Size = new System.Drawing.Size(800, 532);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDetail.Location = new System.Drawing.Point(85, 2);
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
            this.btnSort.Location = new System.Drawing.Point(11, 2);
            this.btnSort.Visible = false;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(4, 2);
            this.cdvFactory.MandatoryFlag = false;
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
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(330, 77);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(194, 30);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "standard Time :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.scCenter.Panel1.Controls.Add(this.scLeft);
            // 
            // scCenter.Panel2
            // 
            this.scCenter.Panel2.Controls.Add(this.splitContainer1);
            this.scCenter.Size = new System.Drawing.Size(794, 529);
            this.scCenter.SplitterDistance = 278;
            this.scCenter.TabIndex = 1;
            // 
            // scLeft
            // 
            this.scLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scLeft.Location = new System.Drawing.Point(0, 0);
            this.scLeft.Name = "scLeft";
            // 
            // scLeft.Panel1
            // 
            this.scLeft.Panel1.Controls.Add(this.cf01);
            // 
            // scLeft.Panel2
            // 
            this.scLeft.Panel2.Controls.Add(this.scRight);
            this.scLeft.Size = new System.Drawing.Size(794, 278);
            this.scLeft.SplitterDistance = 200;
            this.scLeft.TabIndex = 25;
            // 
            // cf01
            // 
            this.cf01.AxisX.Staggered = true;
            this.cf01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf01.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cf01.Location = new System.Drawing.Point(0, 0);
            this.cf01.Name = "cf01";
            this.cf01.Scrollable = true;
            this.cf01.Size = new System.Drawing.Size(200, 278);
            this.cf01.TabIndex = 24;
            this.cf01.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable16});
            this.cf01.ToolBar = true;
            // 
            // scRight
            // 
            this.scRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRight.Location = new System.Drawing.Point(0, 0);
            this.scRight.Name = "scRight";
            // 
            // scRight.Panel1
            // 
            this.scRight.Panel1.Controls.Add(this.cf02);
            // 
            // scRight.Panel2
            // 
            this.scRight.Panel2.Controls.Add(this.cf03);
            this.scRight.Size = new System.Drawing.Size(590, 278);
            this.scRight.SplitterDistance = 198;
            this.scRight.TabIndex = 0;
            // 
            // cf02
            // 
            this.cf02.AxisX.Staggered = true;
            this.cf02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf02.Location = new System.Drawing.Point(0, 0);
            this.cf02.Name = "cf02";
            this.cf02.Scrollable = true;
            this.cf02.Size = new System.Drawing.Size(198, 278);
            this.cf02.TabIndex = 25;
            this.cf02.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable17});
            this.cf02.ToolBar = true;
            // 
            // cf03
            // 
            this.cf03.AxisX.Staggered = true;
            this.cf03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf03.Location = new System.Drawing.Point(0, 0);
            this.cf03.Name = "cf03";
            this.cf03.Scrollable = true;
            this.cf03.Size = new System.Drawing.Size(388, 278);
            this.cf03.TabIndex = 26;
            this.cf03.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable18});
            this.cf03.ToolBar = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cf04);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cf05);
            this.splitContainer1.Size = new System.Drawing.Size(794, 247);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 27;
            // 
            // cf04
            // 
            this.cf04.AxisX.Staggered = true;
            this.cf04.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf04.Location = new System.Drawing.Point(0, 0);
            this.cf04.Name = "cf04";
            this.cf04.Scrollable = true;
            this.cf04.Size = new System.Drawing.Size(414, 247);
            this.cf04.TabIndex = 25;
            this.cf04.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable19});
            this.cf04.ToolBar = true;
            // 
            // cf05
            // 
            this.cf05.AxisX.Staggered = true;
            this.cf05.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf05.Location = new System.Drawing.Point(0, 0);
            this.cf05.Name = "cf05";
            this.cf05.Scrollable = true;
            this.cf05.Size = new System.Drawing.Size(376, 247);
            this.cf05.TabIndex = 26;
            this.cf05.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable20});
            this.cf05.ToolBar = true;
            // 
            // cdvBaseDate
            // 
            this.cdvBaseDate.CustomFormat = "yyyy-MM-dd";
            this.cdvBaseDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvBaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvBaseDate.Location = new System.Drawing.Point(272, 3);
            this.cdvBaseDate.Name = "cdvBaseDate";
            this.cdvBaseDate.Size = new System.Drawing.Size(100, 20);
            this.cdvBaseDate.TabIndex = 75;
            this.cdvBaseDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvBaseDate.Value = new System.DateTime(2008, 12, 2, 0, 0, 0, 0);
            this.cdvBaseDate.Visible = false;
            // 
            // lblBaseDate
            // 
            this.lblBaseDate.AutoSize = true;
            this.lblBaseDate.Location = new System.Drawing.Point(204, 6);
            this.lblBaseDate.Name = "lblBaseDate";
            this.lblBaseDate.Size = new System.Drawing.Size(62, 13);
            this.lblBaseDate.TabIndex = 76;
            this.lblBaseDate.Text = "Standard date :";
            this.lblBaseDate.Visible = false;
            // 
            // cbMemory
            // 
            this.cbMemory.FormattingEnabled = true;
            this.cbMemory.Items.AddRange(new object[] {
            "ALL",
            "MEMORY",
            "S-LSI"});
            this.cbMemory.Location = new System.Drawing.Point(279, 0);
            this.cbMemory.Name = "cbMemory";
            this.cbMemory.Size = new System.Drawing.Size(117, 21);
            this.cbMemory.TabIndex = 79;
            this.cbMemory.Text = "ALL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "MEMORY Classification";
            // 
            // PQC031207
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 1;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC031207";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlDetailCondition1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlDetailCondition3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.scCenter.Panel1.ResumeLayout(false);
            this.scCenter.Panel2.ResumeLayout(false);
            this.scCenter.ResumeLayout(false);
            this.scLeft.Panel1.ResumeLayout(false);
            this.scLeft.Panel2.ResumeLayout(false);
            this.scLeft.ResumeLayout(false);
            this.scRight.Panel1.ResumeLayout(false);
            this.scRight.Panel2.ResumeLayout(false);
            this.scRight.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
		private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer scCenter;
		private System.Windows.Forms.SplitContainer scLeft;
        private System.Windows.Forms.SplitContainer scRight;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf03;
		private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvBaseDate;
        private System.Windows.Forms.Label lblBaseDate;
        private System.Windows.Forms.ComboBox cbMemory;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf01;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf02;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf05;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf04;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
