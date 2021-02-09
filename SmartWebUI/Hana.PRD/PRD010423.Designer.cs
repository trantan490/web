namespace Hana.PRD
{
    partial class PRD010423
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010423));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.rdbConvert = new System.Windows.Forms.RadioButton();
            this.rdbMain = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rdbEpoxy = new System.Windows.Forms.RadioButton();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.cdvOperGroup = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvShift = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbPreG = new System.Windows.Forms.CheckBox();
            this.ckbDispatch = new System.Windows.Forms.CheckBox();
            this.ckbCuWire = new System.Windows.Forms.CheckBox();
            this.ckbSawCut = new System.Windows.Forms.CheckBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(257, 13);
            this.lblTitle.Text = "Performance monitoring by major operation";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.label1);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.ckbSawCut);
            this.pnlCondition2.Controls.Add(this.ckbCuWire);
            this.pnlCondition2.Controls.Add(this.ckbDispatch);
            this.pnlCondition2.Controls.Add(this.ckbPreG);
            this.pnlCondition2.Controls.Add(this.cdvOperGroup);
            this.pnlCondition2.Controls.Add(this.rdbEpoxy);
            this.pnlCondition2.Controls.Add(this.rdbConvert);
            this.pnlCondition2.Controls.Add(this.rdbMain);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.txtProduct);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvType);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.cdvShift);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 116);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 203);
            this.pnlMain.Size = new System.Drawing.Size(800, 397);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 263);
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
            this.spdData.Size = new System.Drawing.Size(794, 394);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Enabled = false;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition2;
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
            this.cdvFactory.TabIndex = 12;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(6, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 79;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(19, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 78;
            this.lblProduct.Text = "Product";
            // 
            // txtProduct
            // 
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Location = new System.Drawing.Point(82, 2);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(102, 21);
            this.txtProduct.TabIndex = 77;
            this.txtProduct.Text = "%";
            // 
            // rdbConvert
            // 
            this.rdbConvert.AutoSize = true;
            this.rdbConvert.Location = new System.Drawing.Point(262, 4);
            this.rdbConvert.Name = "rdbConvert";
            this.rdbConvert.Size = new System.Drawing.Size(79, 17);
            this.rdbConvert.TabIndex = 86;
            this.rdbConvert.Text = "Conversion";
            this.rdbConvert.UseVisualStyleBackColor = true;
            // 
            // rdbMain
            // 
            this.rdbMain.AutoSize = true;
            this.rdbMain.Location = new System.Drawing.Point(207, 4);
            this.rdbMain.Name = "rdbMain";
            this.rdbMain.Size = new System.Drawing.Size(54, 17);
            this.rdbMain.TabIndex = 85;
            this.rdbMain.Text = "Basics";
            this.rdbMain.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(348, 13);
            this.label1.TabIndex = 129;
            this.label1.Text = "※ If you click the number of facilities, detailed information is displayed.";
            // 
            // rdbEpoxy
            // 
            this.rdbEpoxy.AutoSize = true;
            this.rdbEpoxy.Checked = true;
            this.rdbEpoxy.Location = new System.Drawing.Point(317, 4);
            this.rdbEpoxy.Name = "rdbEpoxy";
            this.rdbEpoxy.Size = new System.Drawing.Size(117, 17);
            this.rdbEpoxy.TabIndex = 86;
            this.rdbEpoxy.TabStop = true;
            this.rdbEpoxy.Text = "Conversion(Epoxy)";
            this.rdbEpoxy.UseVisualStyleBackColor = true;
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(205, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(275, 21);
            this.cdvFromToDate.TabIndex = 81;
            // 
            // cdvOperGroup
            // 
            this.cdvOperGroup.BackColor = System.Drawing.Color.Transparent;
            this.cdvOperGroup.bMultiSelect = true;
            this.cdvOperGroup.ConditionText = "Operation group";
            this.cdvOperGroup.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvOperGroup.ControlRef = true;
            this.cdvOperGroup.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOperGroup.Location = new System.Drawing.Point(404, 2);
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
            this.cdvOperGroup.TabIndex = 87;
            this.cdvOperGroup.VisibleValueButton = true;
            this.cdvOperGroup.ValueButtonPress += new System.EventHandler(this.cdvOperGroup_ValueButtonPress);
            // 
            // cdvShift
            // 
            this.cdvShift.BackColor = System.Drawing.Color.Transparent;
            this.cdvShift.bMultiSelect = true;
            this.cdvShift.ConditionText = "Shift";
            this.cdvShift.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvShift.ControlRef = true;
            this.cdvShift.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvShift.Location = new System.Drawing.Point(450, 1);
            this.cdvShift.MandatoryFlag = false;
            this.cdvShift.Name = "cdvShift";
            this.cdvShift.ParentValue = "";
            this.cdvShift.sCodeColumnName = "Code";
            this.cdvShift.sDynamicQuery = "";
            this.cdvShift.sFactory = "";
            this.cdvShift.Size = new System.Drawing.Size(180, 21);
            this.cdvShift.sTableName = "";
            this.cdvShift.sValueColumnName = "Data";
            this.cdvShift.TabIndex = 87;
            this.cdvShift.VisibleValueButton = true;
            this.cdvShift.ValueButtonPress += new System.EventHandler(this.cdvShift_ValueButtonPress);
            // 
            // cdvType
            // 
            this.cdvType.FormattingEnabled = true;
            this.cdvType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvType.Location = new System.Drawing.Point(717, 1);
            this.cdvType.Name = "cdvType";
            this.cdvType.Size = new System.Drawing.Size(70, 21);
            this.cdvType.TabIndex = 91;
            this.cdvType.Text = "P%";
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(639, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 17);
            this.label4.TabIndex = 90;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(651, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 89;
            this.label2.Text = "Lot Type";
            // 
            // ckbPreG
            // 
            this.ckbPreG.AutoSize = true;
            this.ckbPreG.Checked = true;
            this.ckbPreG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbPreG.Location = new System.Drawing.Point(604, 5);
            this.ckbPreG.Name = "ckbPreG";
            this.ckbPreG.Size = new System.Drawing.Size(112, 17);
            this.ckbPreG.TabIndex = 88;
            this.ckbPreG.Text = "Contains PreGrind";
            this.ckbPreG.UseVisualStyleBackColor = true;
            // 
            // ckbDispatch
            // 
            this.ckbDispatch.AutoSize = true;
            this.ckbDispatch.Checked = true;
            this.ckbDispatch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDispatch.Location = new System.Drawing.Point(705, 5);
            this.ckbDispatch.Name = "ckbDispatch";
            this.ckbDispatch.Size = new System.Drawing.Size(93, 17);
            this.ckbDispatch.TabIndex = 88;
            this.ckbDispatch.Text = "DISPATCH (Y)";
            this.ckbDispatch.UseVisualStyleBackColor = true;
            // 
            // ckbCuWire
            // 
            this.ckbCuWire.AutoSize = true;
            this.ckbCuWire.Location = new System.Drawing.Point(802, 5);
            this.ckbCuWire.Name = "ckbCuWire";
            this.ckbCuWire.Size = new System.Drawing.Size(62, 17);
            this.ckbCuWire.TabIndex = 89;
            this.ckbCuWire.Text = "Cu wire";
            this.ckbCuWire.UseVisualStyleBackColor = true;
            // 
            // ckbSawCut
            // 
            this.ckbSawCut.AutoSize = true;
            this.ckbSawCut.Location = new System.Drawing.Point(869, 5);
            this.ckbSawCut.Name = "ckbSawCut";
            this.ckbSawCut.Size = new System.Drawing.Size(82, 17);
            this.ckbSawCut.TabIndex = 89;
            this.ckbSawCut.Text = "Saw Cut(m)";
            this.ckbSawCut.UseVisualStyleBackColor = true;
            // 
            // PRD010423
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010423";
            this.pnlMiddle.ResumeLayout(false);
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

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.RadioButton rdbConvert;
        private System.Windows.Forms.RadioButton rdbMain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rdbEpoxy;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOperGroup;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvShift;
        private System.Windows.Forms.CheckBox ckbPreG;
        private System.Windows.Forms.ComboBox cdvType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbDispatch;
        private System.Windows.Forms.CheckBox ckbCuWire;
        private System.Windows.Forms.CheckBox ckbSawCut;
    }
}
