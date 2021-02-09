namespace Hana.PRD
{
    partial class PRD010603
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010603));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvTestSite = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate2();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbWafer = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboShipFactory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckbSite = new System.Windows.Forms.CheckBox();
            this.ckbSecMerge = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cdvTAT = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.ckbTime = new System.Windows.Forms.CheckBox();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition4.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(99, 13);
            this.lblTitle.Text = "Shipped lot inquiry";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 114);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Controls.Add(this.label3);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.label7);
            this.pnlCondition3.Controls.Add(this.ckbTime);
            this.pnlCondition3.Controls.Add(this.ckbSecMerge);
            this.pnlCondition3.Controls.Add(this.cdvTAT);
            this.pnlCondition3.Controls.Add(this.cdvTestSite);
            this.pnlCondition3.Controls.Add(this.label6);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.ckbSite);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.cdvFromToDate);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Controls.Add(this.txtSearchProduct);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label8);
            this.pnlCondition1.Controls.Add(this.label9);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.cboShipFactory);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.ckbWafer);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 140);
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
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 227);
            this.pnlMain.Size = new System.Drawing.Size(800, 373);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 140);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 287);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition1;
            this.cdvFactory.ForcInitControl3 = this.cdvTestSite;
            this.cdvFactory.Location = new System.Drawing.Point(4, 2);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 6;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvTestSite
            // 
            this.cdvTestSite.BackColor = System.Drawing.Color.Transparent;
            this.cdvTestSite.bMultiSelect = true;
            this.cdvTestSite.ConditionText = "Test Site";
            this.cdvTestSite.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.CUSTOM;
            this.cdvTestSite.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvTestSite.Location = new System.Drawing.Point(4, 2);
            this.cdvTestSite.MandatoryFlag = false;
            this.cdvTestSite.Name = "cdvTestSite";
            this.cdvTestSite.ParentValue = "";
            this.cdvTestSite.RefFactory = this.cdvFactory;
            this.cdvTestSite.sCodeColumnName = "";
            this.cdvTestSite.sDynamicQuery = "";
            this.cdvTestSite.sFactory = "";
            this.cdvTestSite.Size = new System.Drawing.Size(180, 21);
            this.cdvTestSite.sTableName = "H_DEST_AREA";
            this.cdvTestSite.sValueColumnName = "";
            this.cdvTestSite.TabIndex = 13;
            this.cdvTestSite.VisibleValueButton = true;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(3, 3);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 370);
            this.spdData.TabIndex = 0;
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
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(84, 1);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(113, 21);
            this.txtSearchProduct.TabIndex = 10;
            this.txtSearchProduct.Text = "%";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(18, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 11;
            this.lblProduct.Text = "Product";
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(284, 3);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(348, 21);
            this.cdvFromToDate.TabIndex = 12;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 13;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(207, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 28;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "DATE";
            // 
            // ckbWafer
            // 
            this.ckbWafer.AutoSize = true;
            this.ckbWafer.Location = new System.Drawing.Point(650, 5);
            this.ckbWafer.Name = "ckbWafer";
            this.ckbWafer.Size = new System.Drawing.Size(104, 17);
            this.ckbWafer.TabIndex = 14;
            this.ckbWafer.Text = "Wafer Qty inpuiry";
            this.ckbWafer.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(18, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(380, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "* If you click the Loss quantity, you can check the Loss details.";
            // 
            // cboShipFactory
            // 
            this.cboShipFactory.Enabled = false;
            this.cboShipFactory.FormattingEnabled = true;
            this.cboShipFactory.Items.AddRange(new object[] {
            "ALL",
            "Customer standard",
            "Based on our own TEST"});
            this.cboShipFactory.Location = new System.Drawing.Point(495, 2);
            this.cboShipFactory.Name = "cboShipFactory";
            this.cboShipFactory.Size = new System.Drawing.Size(102, 21);
            this.cboShipFactory.TabIndex = 15;
            this.cboShipFactory.Text = "ALL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Ship Factory";
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(406, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 28;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckbSite
            // 
            this.ckbSite.AutoSize = true;
            this.ckbSite.Location = new System.Drawing.Point(650, 5);
            this.ckbSite.Name = "ckbSite";
            this.ckbSite.Size = new System.Drawing.Size(83, 17);
            this.ckbSite.TabIndex = 29;
            this.ckbSite.Text = "Stored and released Site";
            this.ckbSite.UseVisualStyleBackColor = true;
            // 
            // ckbSecMerge
            // 
            this.ckbSecMerge.AutoSize = true;
            this.ckbSecMerge.Location = new System.Drawing.Point(650, 5);
            this.ckbSecMerge.Name = "ckbSecMerge";
            this.ckbSecMerge.Size = new System.Drawing.Size(105, 17);
            this.ckbSecMerge.TabIndex = 14;
            this.ckbSecMerge.Text = "Samsung Run Merge";
            this.ckbSecMerge.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "TAT Standard";
            // 
            // cdvTAT
            // 
            this.cdvTAT.FormattingEnabled = true;
            this.cdvTAT.Items.AddRange(new object[] {
            "Receive standard",
            "Create standard",
            "Standard issue"});
            this.cdvTAT.Location = new System.Drawing.Point(284, 2);
            this.cdvTAT.Name = "cdvTAT";
            this.cdvTAT.Size = new System.Drawing.Size(100, 21);
            this.cdvTAT.TabIndex = 15;
            this.cdvTAT.Text = "Receive standard";
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(207, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 28;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(284, 0);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(100, 21);
            this.cdvLotType.TabIndex = 46;
            this.cdvLotType.Text = "ALL";
            // 
            // label8
            // 
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(207, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(6, 17);
            this.label8.TabIndex = 45;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(219, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Lot Type";
            // 
            // ckbTime
            // 
            this.ckbTime.AutoSize = true;
            this.ckbTime.Location = new System.Drawing.Point(393, 5);
            this.ckbTime.Name = "ckbTime";
            this.ckbTime.Size = new System.Drawing.Size(77, 17);
            this.ckbTime.TabIndex = 14;
            this.ckbTime.Text = "Based on time";
            this.ckbTime.UseVisualStyleBackColor = true;
            // 
            // PRD010603
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 4;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010603";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition4.ResumeLayout(false);
            this.pnlCondition4.PerformLayout();
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate2 cdvFromToDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbWafer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboShipFactory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckbSite;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvTestSite;
        private System.Windows.Forms.CheckBox ckbSecMerge;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cdvTAT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox ckbTime;
    }
}
