namespace Hana.PQC
{
    partial class PQC030109
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
			SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
			this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
			this.label1 = new System.Windows.Forms.Label();
			this.lbl_Product = new System.Windows.Forms.Label();
			this.txtSearchProduct = new System.Windows.Forms.TextBox();
			this.ckbKpcs = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.rbtSite02 = new System.Windows.Forms.RadioButton();
			this.rbtSite01 = new System.Windows.Forms.RadioButton();
			this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.scCenter = new System.Windows.Forms.SplitContainer();
			this.cfUser = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
			this.scBottom = new System.Windows.Forms.SplitContainer();
			this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
			this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.SS02 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
			this.SS02_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.cdvQCType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
			this.cdvQCOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
			this.cdvFromToDate.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.scCenter.Panel1.SuspendLayout();
			this.scCenter.Panel2.SuspendLayout();
			this.scCenter.SuspendLayout();
			this.scBottom.Panel1.SuspendLayout();
			this.scBottom.Panel2.SuspendLayout();
			this.scBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SS02)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SS02_Sheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Size = new System.Drawing.Size(130, 13);
            this.lblTitle.Text = "Quality Team Defect Detection Status";
			// 
			// pnlMiddle
			// 
			this.pnlMiddle.Controls.Add(this.groupBox3);
			this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
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
			this.pnlCondition2.Controls.Add(this.cdvQCOper);
			this.pnlCondition2.Controls.Add(this.cdvQCType);
			this.pnlCondition2.Controls.Add(this.txtSearchProduct);
			this.pnlCondition2.Controls.Add(this.lbl_Product);
			this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
			// 
			// pnlCondition1
			// 
			this.pnlCondition1.Controls.Add(this.ckbKpcs);
			this.pnlCondition1.Controls.Add(this.groupBox2);
			this.pnlCondition1.Controls.Add(this.cdvFromToDate);
			this.pnlCondition1.Controls.Add(this.label3);
			this.pnlCondition1.Controls.Add(this.cdvFactory);
			this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
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
			this.pnlMain.Controls.Add(this.scCenter);
			this.pnlMain.Location = new System.Drawing.Point(0, 152);
			this.pnlMain.Size = new System.Drawing.Size(800, 448);
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
			// cdvFromToDate
			// 
			this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
			this.cdvFromToDate.Controls.Add(this.label1);
			this.cdvFromToDate.Location = new System.Drawing.Point(259, 2);
			this.cdvFromToDate.Name = "cdvFromToDate";
			this.cdvFromToDate.RestrictedDayCount = 60;
			this.cdvFromToDate.Size = new System.Drawing.Size(232, 21);
			this.cdvFromToDate.TabIndex = 10;
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
			// lbl_Product
			// 
			this.lbl_Product.AutoSize = true;
			this.lbl_Product.Location = new System.Drawing.Point(409, 5);
			this.lbl_Product.Name = "lbl_Product";
			this.lbl_Product.Size = new System.Drawing.Size(62, 13);
			this.lbl_Product.TabIndex = 16;
			this.lbl_Product.Text = "PRODUCT :";
			this.lbl_Product.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtSearchProduct
			// 
			this.txtSearchProduct.Location = new System.Drawing.Point(470, 2);
			this.txtSearchProduct.Name = "txtSearchProduct";
			this.txtSearchProduct.Size = new System.Drawing.Size(232, 21);
			this.txtSearchProduct.TabIndex = 15;
			this.txtSearchProduct.Text = "%";
			this.txtSearchProduct.Leave += new System.EventHandler(this.txtSearchProduct_Leave);
			// 
			// ckbKpcs
			// 
			this.ckbKpcs.AutoSize = true;
			this.ckbKpcs.Location = new System.Drawing.Point(747, 3);
			this.ckbKpcs.Name = "ckbKpcs";
			this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
			this.ckbKpcs.TabIndex = 17;
			this.ckbKpcs.Text = "Kpcs";
			this.ckbKpcs.UseVisualStyleBackColor = true;
			this.ckbKpcs.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(60, 13);
			this.label2.TabIndex = 66;
			this.label2.Text = "Assembly Site:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(198, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 67;
			this.label3.Text = "Search Period:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.rbtSite02);
			this.groupBox2.Controls.Add(this.rbtSite01);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(650, 52);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(194, 30);
			this.groupBox2.TabIndex = 71;
			this.groupBox2.TabStop = false;
			// 
			// rbtSite02
			// 
			this.rbtSite02.AutoSize = true;
			this.rbtSite02.Location = new System.Drawing.Point(139, 11);
			this.rbtSite02.Name = "rbtSite02";
			this.rbtSite02.Size = new System.Drawing.Size(49, 17);
			this.rbtSite02.TabIndex = 68;
			this.rbtSite02.Text = "other companies";
			this.rbtSite02.UseVisualStyleBackColor = true;
			// 
			// rbtSite01
			// 
			this.rbtSite01.AutoSize = true;
			this.rbtSite01.Checked = true;
			this.rbtSite01.Location = new System.Drawing.Point(65, 11);
			this.rbtSite01.Name = "rbtSite01";
			this.rbtSite01.Size = new System.Drawing.Size(72, 17);
			this.rbtSite01.TabIndex = 67;
			this.rbtSite01.TabStop = true;
			this.rbtSite01.Text = "Own company (HM)";
			this.rbtSite01.UseVisualStyleBackColor = true;
			// 
			// cdvFactory
			// 
			this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
			this.cdvFactory.bMultiSelect = true;
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
			this.scCenter.Panel1.Controls.Add(this.cfUser);
			// 
			// scCenter.Panel2
			// 
			this.scCenter.Panel2.Controls.Add(this.scBottom);
			this.scCenter.Size = new System.Drawing.Size(794, 445);
			this.scCenter.SplitterDistance = 235;
			this.scCenter.TabIndex = 1;
			// 
			// cfUser
			// 
			this.cfUser.AxisX.Staggered = true;
			this.cfUser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cfUser.Location = new System.Drawing.Point(0, 0);
			this.cfUser.Name = "cfUser";
			this.cfUser.Scrollable = true;
			this.cfUser.Size = new System.Drawing.Size(794, 235);
			this.cfUser.TabIndex = 24;
			this.cfUser.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
			this.cfUser.ToolBar = true;
			// 
			// scBottom
			// 
			this.scBottom.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scBottom.Location = new System.Drawing.Point(0, 0);
			this.scBottom.Name = "scBottom";
			// 
			// scBottom.Panel1
			// 
			this.scBottom.Panel1.Controls.Add(this.SS01);
			// 
			// scBottom.Panel2
			// 
			this.scBottom.Panel2.Controls.Add(this.SS02);
			this.scBottom.Size = new System.Drawing.Size(794, 206);
			this.scBottom.SplitterDistance = 400;
			this.scBottom.TabIndex = 2;
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
			this.SS01.Size = new System.Drawing.Size(400, 206);
			this.SS01.TabIndex = 1;
			this.SS01.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.SS01_CellClick);
			// 
			// SS01_Sheet1
			// 
			this.SS01_Sheet1.Reset();
			this.SS01_Sheet1.SheetName = "Sheet1";
			// Formulas and custom names must be loaded with R1C1 reference style
			this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
			this.SS01_Sheet1.ColumnCount = 0;
			this.SS01_Sheet1.RowCount = 0;
			this.SS01_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
			this.SS01.SetActiveViewport(0, 1, 1);
			// 
			// SS02
			// 
			this.SS02.About = "4.0.2001.2005";
			this.SS02.AccessibleDescription = "SS02, Sheet1, Row 0, Column 0, ";
			this.SS02.BackColor = System.Drawing.Color.White;
			this.SS02.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SS02.Location = new System.Drawing.Point(0, 0);
			this.SS02.Name = "SS02";
			this.SS02.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.SS02.RPT_IsPreCellsType = true;
			this.SS02.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS02_Sheet1});
			this.SS02.Size = new System.Drawing.Size(390, 206);
			this.SS02.TabIndex = 3;
			// 
			// SS02_Sheet1
			// 
			this.SS02_Sheet1.Reset();
			this.SS02_Sheet1.SheetName = "Sheet1";
			// Formulas and custom names must be loaded with R1C1 reference style
			this.SS02_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
			this.SS02_Sheet1.ColumnCount = 0;
			this.SS02_Sheet1.RowCount = 0;
			this.SS02_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.SS02_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
			this.SS02.SetActiveViewport(0, 1, 1);
			// 
			// cdvQCType
			// 
			this.cdvQCType.BackColor = System.Drawing.Color.Transparent;
			this.cdvQCType.bMultiSelect = true;
			this.cdvQCType.ConditionText = "Purpose of inspection";
			this.cdvQCType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
			this.cdvQCType.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cdvQCType.Location = new System.Drawing.Point(197, 1);
			this.cdvQCType.MandatoryFlag = false;
			this.cdvQCType.Name = "cdvQCType";
			this.cdvQCType.ParentValue = "";
			this.cdvQCType.sCodeColumnName = "KEY_1";
			this.cdvQCType.sDynamicQuery = "SELECT KEY_1, \' \' AS DATA_1 FROM MGCMTBLDAT WHERE FACTORY = \'HMKA1\' AND TABLE_NAM" +
				"E = \'ABRLOT_PURPOSE\' ORDER BY KEY_1 ASC";
			this.cdvQCType.sFactory = "";
			this.cdvQCType.Size = new System.Drawing.Size(180, 21);
			this.cdvQCType.sTableName = "";
			this.cdvQCType.sValueColumnName = "DATA_1";
			this.cdvQCType.TabIndex = 72;
			this.cdvQCType.VisibleValueButton = true;
			// 
			// cdvQCOper
			// 
			this.cdvQCOper.BackColor = System.Drawing.Color.Transparent;
			this.cdvQCOper.bMultiSelect = true;
			this.cdvQCOper.ConditionText = "Inspection operation";
			this.cdvQCOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
			this.cdvQCOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cdvQCOper.Location = new System.Drawing.Point(4, 2);
			this.cdvQCOper.MandatoryFlag = false;
			this.cdvQCOper.Name = "cdvQCOper";
			this.cdvQCOper.ParentValue = "";
			this.cdvQCOper.sCodeColumnName = "OPER_DESC";
			this.cdvQCOper.sDynamicQuery = "SELECT OPER_DESC, \' \' AS DATA_1 FROM MWIPOPRDEF@RPTTOMES WHERE FACTORY = \'HMKA1\' " +
				"AND OPER LIKE \'A%\' ORDER BY OPER ASC";
			this.cdvQCOper.sFactory = "";
			this.cdvQCOper.Size = new System.Drawing.Size(180, 21);
			this.cdvQCOper.sTableName = "";
			this.cdvQCOper.sValueColumnName = "DATA_1";
			this.cdvQCOper.TabIndex = 73;
			this.cdvQCOper.VisibleValueButton = true;
			// 
			// PQC030109
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
			this.ConditionCount = 2;
			this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
			this.FormStyle.FormName = null;
			this.Name = "PQC030109";
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
			this.cdvFromToDate.ResumeLayout(false);
			this.cdvFromToDate.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.scCenter.Panel1.ResumeLayout(false);
			this.scCenter.Panel2.ResumeLayout(false);
			this.scCenter.ResumeLayout(false);
			this.scBottom.Panel1.ResumeLayout(false);
			this.scBottom.Panel2.ResumeLayout(false);
			this.scBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SS02)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SS02_Sheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtSite02;
        private System.Windows.Forms.RadioButton rbtSite01;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
		private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label6;
		private System.Windows.Forms.SplitContainer scCenter;
		private Miracom.SmartWeb.UI.Controls.udcChartFX cfUser;
		private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
		private FarPoint.Win.Spread.SheetView SS01_Sheet1;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvQCType;
		private System.Windows.Forms.SplitContainer scBottom;
		private Miracom.SmartWeb.UI.Controls.udcFarPoint SS02;
		private FarPoint.Win.Spread.SheetView SS02_Sheet1;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvQCOper;
    }
}
