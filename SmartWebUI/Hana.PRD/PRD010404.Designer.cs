namespace Hana.PRD
{
    partial class PRD010404
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
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblFromToDate = new System.Windows.Forms.Label();
            this.lblLotType = new System.Windows.Forms.Label();
            this.txtLotType = new System.Windows.Forms.TextBox();
            this.gbBaseGubn = new System.Windows.Forms.GroupBox();
            this.rbBaseTime02 = new System.Windows.Forms.RadioButton();
            this.rbBaseTime01 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.gbSearchGubn = new System.Windows.Forms.GroupBox();
            this.rbSearchGubn02 = new System.Windows.Forms.RadioButton();
            this.rbSearchGubn01 = new System.Windows.Forms.RadioButton();
            this.lblSearchTitle = new System.Windows.Forms.Label();
            this.cbCutoff_DT = new System.Windows.Forms.ComboBox();
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
            this.gbBaseGubn.SuspendLayout();
            this.gbSearchGubn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(75, 13);
            this.lblTitle.Text = "Interoperation receipts and disbursements";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 178);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 154);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 130);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 106);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 82);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 58);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.gbBaseGubn);
            this.pnlCondition2.Controls.Add(this.txtLotType);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Controls.Add(this.cdvFactory);
            this.pnlCondition2.Controls.Add(this.txtProduct);
            this.pnlCondition2.Controls.Add(this.lblLotType);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cbCutoff_DT);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.lblFromToDate);
            this.pnlCondition1.Controls.Add(this.gbSearchGubn);
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
            this.pnlMain.Controls.Add(this.spdData);
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
            this.cdvFromToDate.Location = new System.Drawing.Point(291, 3);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(234, 21);
            this.cdvFromToDate.TabIndex = 13;
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
            this.spdData.Size = new System.Drawing.Size(794, 445);
            this.spdData.TabIndex = 2;
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
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(222, 7);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(62, 13);
            this.lblProduct.TabIndex = 18;
            this.lblProduct.Text = "PRODUCT :";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(287, 3);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(249, 21);
            this.txtProduct.TabIndex = 17;
            this.txtProduct.Text = "%";
            this.txtProduct.TextChanged += new System.EventHandler(this.txtProduct_TextChanged);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(16, 3);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(188, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 19;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // lblFromToDate
            // 
            this.lblFromToDate.AutoSize = true;
            this.lblFromToDate.Location = new System.Drawing.Point(222, 5);
            this.lblFromToDate.Name = "lblFromToDate";
            this.lblFromToDate.Size = new System.Drawing.Size(62, 13);
            this.lblFromToDate.TabIndex = 68;
            this.lblFromToDate.Text = "receipts and disbursements period:";
            this.lblFromToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLotType
            // 
            this.lblLotType.AutoSize = true;
            this.lblLotType.Location = new System.Drawing.Point(554, 6);
            this.lblLotType.Name = "lblLotType";
            this.lblLotType.Size = new System.Drawing.Size(60, 13);
            this.lblLotType.TabIndex = 70;
            this.lblLotType.Text = "LOT TYPE :";
            this.lblLotType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLotType
            // 
            this.txtLotType.Location = new System.Drawing.Point(618, 3);
            this.txtLotType.Name = "txtLotType";
            this.txtLotType.Size = new System.Drawing.Size(121, 21);
            this.txtLotType.TabIndex = 69;
            this.txtLotType.Text = "P%";
            // 
            // gbBaseGubn
            // 
            this.gbBaseGubn.Controls.Add(this.rbBaseTime02);
            this.gbBaseGubn.Controls.Add(this.rbBaseTime01);
            this.gbBaseGubn.Controls.Add(this.label6);
            this.gbBaseGubn.Location = new System.Drawing.Point(19, -5);
            this.gbBaseGubn.Name = "gbBaseGubn";
            this.gbBaseGubn.Size = new System.Drawing.Size(185, 30);
            this.gbBaseGubn.TabIndex = 73;
            this.gbBaseGubn.TabStop = false;
            // 
            // rbBaseTime02
            // 
            this.rbBaseTime02.AutoSize = true;
            this.rbBaseTime02.Location = new System.Drawing.Point(129, 11);
            this.rbBaseTime02.Name = "rbBaseTime02";
            this.rbBaseTime02.Size = new System.Drawing.Size(49, 17);
            this.rbBaseTime02.TabIndex = 68;
            this.rbBaseTime02.Text = "06시";
            this.rbBaseTime02.UseVisualStyleBackColor = true;
            // 
            // rbBaseTime01
            // 
            this.rbBaseTime01.AutoSize = true;
            this.rbBaseTime01.Checked = true;
            this.rbBaseTime01.Location = new System.Drawing.Point(71, 11);
            this.rbBaseTime01.Name = "rbBaseTime01";
            this.rbBaseTime01.Size = new System.Drawing.Size(49, 17);
            this.rbBaseTime01.TabIndex = 67;
            this.rbBaseTime01.TabStop = true;
            this.rbBaseTime01.Text = "22시";
            this.rbBaseTime01.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "standard Time :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbSearchGubn
            // 
            this.gbSearchGubn.Controls.Add(this.rbSearchGubn02);
            this.gbSearchGubn.Controls.Add(this.rbSearchGubn01);
            this.gbSearchGubn.Controls.Add(this.lblSearchTitle);
            this.gbSearchGubn.Location = new System.Drawing.Point(19, -6);
            this.gbSearchGubn.Name = "gbSearchGubn";
            this.gbSearchGubn.Size = new System.Drawing.Size(185, 30);
            this.gbSearchGubn.TabIndex = 74;
            this.gbSearchGubn.TabStop = false;
            // 
            // rbSearchGubn02
            // 
            this.rbSearchGubn02.AutoSize = true;
            this.rbSearchGubn02.Location = new System.Drawing.Point(129, 11);
            this.rbSearchGubn02.Name = "rbSearchGubn02";
            this.rbSearchGubn02.Size = new System.Drawing.Size(49, 17);
            this.rbSearchGubn02.TabIndex = 68;
            this.rbSearchGubn02.Text = "Now";
            this.rbSearchGubn02.UseVisualStyleBackColor = true;
            this.rbSearchGubn02.CheckedChanged += new System.EventHandler(this.rbSearchGubn_CheckedChanged);
            // 
            // rbSearchGubn01
            // 
            this.rbSearchGubn01.AutoSize = true;
            this.rbSearchGubn01.Checked = true;
            this.rbSearchGubn01.Location = new System.Drawing.Point(71, 11);
            this.rbSearchGubn01.Name = "rbSearchGubn01";
            this.rbSearchGubn01.Size = new System.Drawing.Size(49, 17);
            this.rbSearchGubn01.TabIndex = 67;
            this.rbSearchGubn01.TabStop = true;
            this.rbSearchGubn01.Text = "과거";
            this.rbSearchGubn01.UseVisualStyleBackColor = true;
            this.rbSearchGubn01.CheckedChanged += new System.EventHandler(this.rbSearchGubn_CheckedChanged);
            // 
            // lblSearchTitle
            // 
            this.lblSearchTitle.AutoSize = true;
            this.lblSearchTitle.Location = new System.Drawing.Point(4, 12);
            this.lblSearchTitle.Name = "lblSearchTitle";
            this.lblSearchTitle.Size = new System.Drawing.Size(62, 13);
            this.lblSearchTitle.TabIndex = 66;
            this.lblSearchTitle.Text = "Search criteria:";
            this.lblSearchTitle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbCutoff_DT
            // 
            this.cbCutoff_DT.FormattingEnabled = true;
            this.cbCutoff_DT.Location = new System.Drawing.Point(287, 3);
            this.cbCutoff_DT.Name = "cbCutoff_DT";
            this.cbCutoff_DT.Size = new System.Drawing.Size(452, 21);
            this.cbCutoff_DT.TabIndex = 75;
            // 
            // PRD010404
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010404";
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
            this.gbBaseGubn.ResumeLayout(false);
            this.gbBaseGubn.PerformLayout();
            this.gbSearchGubn.ResumeLayout(false);
            this.gbSearchGubn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblFromToDate;
        private System.Windows.Forms.Label lblLotType;
        private System.Windows.Forms.TextBox txtLotType;
        private System.Windows.Forms.GroupBox gbBaseGubn;
        private System.Windows.Forms.RadioButton rbBaseTime02;
        private System.Windows.Forms.RadioButton rbBaseTime01;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbSearchGubn;
        private System.Windows.Forms.RadioButton rbSearchGubn02;
        private System.Windows.Forms.RadioButton rbSearchGubn01;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.ComboBox cbCutoff_DT;
    }
}
