namespace Hana.PRD
{
    partial class PRD010422
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010422));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.rdbMain = new System.Windows.Forms.RadioButton();
            this.rdbConvert = new System.Windows.Forms.RadioButton();
            this.cdvOperGroup = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.ckdDP = new System.Windows.Forms.CheckBox();
            this.cdvType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvBigOperGroup = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(72, 13);
            this.lblTitle.Text = "TREND Performance";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(792, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvBigOperGroup);
            this.pnlCondition2.Controls.Add(this.ckdDP);
            this.pnlCondition2.Controls.Add(this.cdvOperGroup);
            this.pnlCondition2.Controls.Add(this.rdbConvert);
            this.pnlCondition2.Controls.Add(this.ckbKpcs);
            this.pnlCondition2.Controls.Add(this.rdbMain);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.lbl_Product);
            this.pnlCondition2.Controls.Add(this.txtSearchProduct);
            this.pnlCondition2.Size = new System.Drawing.Size(790, 24);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvType);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Size = new System.Drawing.Size(790, 24);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
            this.pnlWIPDetail.Size = new System.Drawing.Size(792, 94);
            // 
            // pnlDetailCondition2
            // 
            this.pnlDetailCondition2.Size = new System.Drawing.Size(790, 30);
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
            this.pnlDetailCondition1.Size = new System.Drawing.Size(790, 24);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 186);
            this.pnlMain.Size = new System.Drawing.Size(792, 414);
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
            // pnlDetailCondition3
            // 
            this.pnlDetailCondition3.Location = new System.Drawing.Point(0, 62);
            this.pnlDetailCondition3.Size = new System.Drawing.Size(790, 25);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 186);
            this.pnlBUMPDetail.Size = new System.Drawing.Size(792, 87);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(790, 24);
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
            // panel6
            // 
            this.panel6.Size = new System.Drawing.Size(790, 24);
            // 
            // panel7
            // 
            this.panel7.Size = new System.Drawing.Size(790, 24);
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
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY_ASSEMBLY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.Location = new System.Drawing.Point(4, 1);
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
            this.spdData.Size = new System.Drawing.Size(786, 411);
            this.spdData.TabIndex = 0;
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
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(19, 7);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(44, 13);
            this.lbl_Product.TabIndex = 13;
            this.lbl_Product.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(84, 2);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(100, 21);
            this.txtSearchProduct.TabIndex = 12;
            this.txtSearchProduct.Text = "%";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 5);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(10, 17);
            this.lblIcon.TabIndex = 14;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(278, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(271, 21);
            this.cdvFromToDate.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(207, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 17);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Date";
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(291, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 18;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // rdbMain
            // 
            this.rdbMain.AutoSize = true;
            this.rdbMain.Checked = true;
            this.rdbMain.Location = new System.Drawing.Point(193, 3);
            this.rdbMain.Name = "rdbMain";
            this.rdbMain.Size = new System.Drawing.Size(49, 17);
            this.rdbMain.TabIndex = 83;
            this.rdbMain.TabStop = true;
            this.rdbMain.Text = "기본";
            this.rdbMain.UseVisualStyleBackColor = true;
            // 
            // rdbConvert
            // 
            this.rdbConvert.AutoSize = true;
            this.rdbConvert.Location = new System.Drawing.Point(242, 3);
            this.rdbConvert.Name = "rdbConvert";
            this.rdbConvert.Size = new System.Drawing.Size(49, 17);
            this.rdbConvert.TabIndex = 84;
            this.rdbConvert.Text = "Conversion";
            this.rdbConvert.UseVisualStyleBackColor = true;
            // 
            // cdvOperGroup
            // 
            this.cdvOperGroup.BackColor = System.Drawing.Color.Transparent;
            this.cdvOperGroup.bMultiSelect = true;
            this.cdvOperGroup.ConditionText = "Operation group";
            this.cdvOperGroup.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvOperGroup.ControlRef = true;
            this.cdvOperGroup.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOperGroup.Location = new System.Drawing.Point(604, 1);
            this.cdvOperGroup.MandatoryFlag = false;
            this.cdvOperGroup.Name = "cdvOperGroup";
            this.cdvOperGroup.ParentValue = "";
            this.cdvOperGroup.RefFactory = this.cdvFactory;
            this.cdvOperGroup.sCodeColumnName = "Code";
            this.cdvOperGroup.sDynamicQuery = "";
            this.cdvOperGroup.sFactory = "";
            this.cdvOperGroup.Size = new System.Drawing.Size(180, 21);
            this.cdvOperGroup.sTableName = "";
            this.cdvOperGroup.sValueColumnName = "Data";
            this.cdvOperGroup.TabIndex = 86;
            this.cdvOperGroup.VisibleValueButton = true;
            this.cdvOperGroup.ValueButtonPress += new System.EventHandler(this.cdvOperGroup_ValueButtonPress);
            // 
            // ckdDP
            // 
            this.ckdDP.AutoSize = true;
            this.ckdDP.Location = new System.Drawing.Point(339, 4);
            this.ckdDP.Name = "ckdDP";
            this.ckdDP.Size = new System.Drawing.Size(66, 17);
            this.ckdDP.TabIndex = 87;
            this.ckdDP.Text = "DP Performance";
            this.ckdDP.UseVisualStyleBackColor = true;
            // 
            // cdvType
            // 
            this.cdvType.FormattingEnabled = true;
            this.cdvType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvType.Location = new System.Drawing.Point(685, 1);
            this.cdvType.Name = "cdvType";
            this.cdvType.Size = new System.Drawing.Size(70, 21);
            this.cdvType.TabIndex = 88;
            this.cdvType.Text = "ALL";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Lot Type";
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(607, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 17);
            this.label4.TabIndex = 14;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvBigOperGroup
            // 
            this.cdvBigOperGroup.BackColor = System.Drawing.Color.Transparent;
            this.cdvBigOperGroup.bMultiSelect = true;
            this.cdvBigOperGroup.ConditionText = "Operation large group";
            this.cdvBigOperGroup.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvBigOperGroup.ControlRef = true;
            this.cdvBigOperGroup.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvBigOperGroup.Location = new System.Drawing.Point(404, 1);
            this.cdvBigOperGroup.MandatoryFlag = false;
            this.cdvBigOperGroup.Name = "cdvBigOperGroup";
            this.cdvBigOperGroup.ParentValue = "";
            this.cdvBigOperGroup.RefFactory = this.cdvFactory;
            this.cdvBigOperGroup.sCodeColumnName = "Code";
            this.cdvBigOperGroup.sDynamicQuery = "";
            this.cdvBigOperGroup.sFactory = "";
            this.cdvBigOperGroup.Size = new System.Drawing.Size(180, 21);
            this.cdvBigOperGroup.sTableName = "";
            this.cdvBigOperGroup.sValueColumnName = "Data";
            this.cdvBigOperGroup.TabIndex = 88;
            this.cdvBigOperGroup.VisibleValueButton = true;
            this.cdvBigOperGroup.ValueButtonPress += new System.EventHandler(this.cdvBigOperGroup_ValueButtonPress);
            // 
            // PRD010422
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010422";
            this.Size = new System.Drawing.Size(792, 600);
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
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label lblIcon;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.RadioButton rdbConvert;
        private System.Windows.Forms.RadioButton rdbMain;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOperGroup;
        private System.Windows.Forms.CheckBox ckdDP;
        private System.Windows.Forms.ComboBox cdvType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvBigOperGroup;
    }
}
