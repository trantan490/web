namespace Hana.PRD
{
    partial class PRD011010
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
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType36 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType37 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType38 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType39 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType40 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType41 = new FarPoint.Win.Spread.CellType.NumberCellType();
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType42 = new FarPoint.Win.Spread.CellType.NumberCellType();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.chkKcps = new System.Windows.Forms.CheckBox();
            this.pnlEqp = new System.Windows.Forms.Panel();
            this.lblEqp = new System.Windows.Forms.Label();
            this.spdData_Eqp_Pos = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Eqp_Pos_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbConvert = new System.Windows.Forms.RadioButton();
            this.rdbMain = new System.Windows.Forms.RadioButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.pnlEqp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Eqp_Pos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Eqp_Pos_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(207, 13);
            this.lblTitle.Text = "Performance against Capa by DA / WB degree";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 182);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 158);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 134);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 110);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 86);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.rdbConvert);
            this.pnlCondition2.Controls.Add(this.rdbMain);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.chkKcps);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.lblProduct);
            this.pnlCondition1.Controls.Add(this.txtSearchProduct);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
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
            this.pnlMain.Controls.Add(this.pnlEqp);
            this.pnlMain.Controls.Add(this.spdData);
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
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
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
            this.spdData.Size = new System.Drawing.Size(794, 418);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.Location = new System.Drawing.Point(5, 3);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(179, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 2;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // lblIcon
            // 
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(210, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 124;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(220, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 13);
            this.lblDate.TabIndex = 123;
            this.lblDate.Text = "standard date";
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(288, 2);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 122;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(478, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 17);
            this.label2.TabIndex = 121;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(494, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 120;
            this.lblProduct.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(549, 2);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(94, 21);
            this.txtSearchProduct.TabIndex = 119;
            this.txtSearchProduct.Text = "%";
            // 
            // chkKcps
            // 
            this.chkKcps.AutoSize = true;
            this.chkKcps.Checked = true;
            this.chkKcps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKcps.Location = new System.Drawing.Point(125, 5);
            this.chkKcps.Name = "chkKcps";
            this.chkKcps.Size = new System.Drawing.Size(200, 17);
            this.chkKcps.TabIndex = 125;
            this.chkKcps.Text = "Kcps [The number of facilities is not affected.]";
            this.chkKcps.UseVisualStyleBackColor = true;
            // 
            // pnlEqp
            // 
            this.pnlEqp.BackColor = System.Drawing.Color.Wheat;
            this.pnlEqp.Controls.Add(this.lblEqp);
            this.pnlEqp.Controls.Add(this.spdData_Eqp_Pos);
            this.pnlEqp.Location = new System.Drawing.Point(129, 49);
            this.pnlEqp.Name = "pnlEqp";
            this.pnlEqp.Size = new System.Drawing.Size(564, 214);
            this.pnlEqp.TabIndex = 4;
            this.pnlEqp.Visible = false;
            // 
            // lblEqp
            // 
            this.lblEqp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEqp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblEqp.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lblEqp.Location = new System.Drawing.Point(6, 5);
            this.lblEqp.Name = "lblEqp";
            this.lblEqp.Size = new System.Drawing.Size(552, 23);
            this.lblEqp.TabIndex = 5;
            this.lblEqp.Text = "Arrange and Status of Equipment";
            this.lblEqp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblEqp.Click += new System.EventHandler(this.lblEqp_Click);
            // 
            // spdData_Eqp_Pos
            // 
            this.spdData_Eqp_Pos.About = "4.0.2001.2005";
            this.spdData_Eqp_Pos.AccessibleDescription = "spdData_Eqp_Pos, Sheet1";
            this.spdData_Eqp_Pos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spdData_Eqp_Pos.BackColor = System.Drawing.Color.White;
            this.spdData_Eqp_Pos.Location = new System.Drawing.Point(6, 33);
            this.spdData_Eqp_Pos.Name = "spdData_Eqp_Pos";
            this.spdData_Eqp_Pos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData_Eqp_Pos.RPT_IsPreCellsType = true;
            this.spdData_Eqp_Pos.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Eqp_Pos_Sheet1});
            this.spdData_Eqp_Pos.Size = new System.Drawing.Size(552, 178);
            this.spdData_Eqp_Pos.TabIndex = 3;
            // 
            // spdData_Eqp_Pos_Sheet1
            // 
            this.spdData_Eqp_Pos_Sheet1.Reset();
            this.spdData_Eqp_Pos_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdData_Eqp_Pos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdData_Eqp_Pos_Sheet1.ColumnCount = 8;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.RowCount = 2;
            this.spdData_Eqp_Pos_Sheet1.RowCount = 0;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Equipment model";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 1).RowSpan = 2;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "count";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 2).RowSpan = 2;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "UPEH";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 3).RowSpan = 2;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "CAPA";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 4).ColumnSpan = 4;
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "Equipment Status";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "Wait";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "Run";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(1, 6).Value = "Down";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Cells.Get(1, 7).Value = "Replacement of product";
            this.spdData_Eqp_Pos_Sheet1.ColumnHeader.Rows.Get(0).Height = 21F;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(0).CellType = textCellType6;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(1).CellType = numberCellType36;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(2).CellType = numberCellType37;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(3).CellType = numberCellType38;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(4).CellType = numberCellType39;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(4).Label = "Wait";
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(5).CellType = numberCellType40;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(5).Label = "Run";
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(6).CellType = numberCellType41;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(6).Label = "Down";
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(7).CellType = numberCellType42;
            this.spdData_Eqp_Pos_Sheet1.Columns.Get(7).Label = "Replacement of product";
            this.spdData_Eqp_Pos_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.spdData_Eqp_Pos_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdData_Eqp_Pos.SetActiveViewport(0, 1, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(353, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 13);
            this.label1.TabIndex = 127;
            this.label1.Text = "※ Details are displayed when you click the number of facilities or CAPA.";
            // 
            // rdbConvert
            // 
            this.rdbConvert.AutoSize = true;
            this.rdbConvert.Location = new System.Drawing.Point(66, 4);
            this.rdbConvert.Name = "rdbConvert";
            this.rdbConvert.Size = new System.Drawing.Size(49, 17);
            this.rdbConvert.TabIndex = 129;
            this.rdbConvert.Text = "Conversion";
            this.rdbConvert.UseVisualStyleBackColor = true;
            // 
            // rdbMain
            // 
            this.rdbMain.AutoSize = true;
            this.rdbMain.Checked = true;
            this.rdbMain.Location = new System.Drawing.Point(11, 4);
            this.rdbMain.Name = "rdbMain";
            this.rdbMain.Size = new System.Drawing.Size(49, 17);
            this.rdbMain.TabIndex = 128;
            this.rdbMain.TabStop = true;
            this.rdbMain.Text = "기본";
            this.rdbMain.UseVisualStyleBackColor = true;
            // 
            // PRD011010
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD011010";
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.pnlEqp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Eqp_Pos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Eqp_Pos_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblDate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.CheckBox chkKcps;
        private System.Windows.Forms.Panel pnlEqp;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData_Eqp_Pos;
        private FarPoint.Win.Spread.SheetView spdData_Eqp_Pos_Sheet1;
        private System.Windows.Forms.Label lblEqp;        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbConvert;
        private System.Windows.Forms.RadioButton rdbMain;
    }
}
