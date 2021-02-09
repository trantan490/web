namespace Hana.PRD
{
    partial class PRD010904
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010904));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvTime = new System.Windows.Forms.ComboBox();
            this.rdbSLSI = new System.Windows.Forms.RadioButton();
            this.rdbETC = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboPer = new System.Windows.Forms.ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(62, 13);
            this.lblTitle.Text = "Input management";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label7);
            this.pnlCondition2.Controls.Add(this.label6);
            this.pnlCondition2.Controls.Add(this.cboPer);
            this.pnlCondition2.Controls.Add(this.rdbETC);
            this.pnlCondition2.Controls.Add(this.rdbSLSI);
            this.pnlCondition2.Controls.Add(this.label9);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.label8);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.cdvTime);
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.lblProduct);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.txtSearchProduct);
            this.pnlCondition1.Controls.Add(this.label2);
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
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(207, 5);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 45;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Lot Type";
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(407, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 34;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(418, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 33;
            this.lblProduct.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.Location = new System.Drawing.Point(484, 2);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(100, 21);
            this.txtSearchProduct.TabIndex = 32;
            this.txtSearchProduct.Text = "%";
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(285, 2);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(99, 21);
            this.cdvLotType.TabIndex = 46;
            this.cdvLotType.Text = "ALL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(618, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "TimeBase";
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(607, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 45;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvTime
            // 
            this.cdvTime.FormattingEnabled = true;
            this.cdvTime.Items.AddRange(new object[] {
            "2",
            "4"});
            this.cdvTime.Location = new System.Drawing.Point(685, 1);
            this.cdvTime.Name = "cdvTime";
            this.cdvTime.Size = new System.Drawing.Size(99, 21);
            this.cdvTime.TabIndex = 46;
            this.cdvTime.Text = "4";
            // 
            // rdbSLSI
            // 
            this.rdbSLSI.AutoSize = true;
            this.rdbSLSI.Checked = true;
            this.rdbSLSI.Location = new System.Drawing.Point(84, 4);
            this.rdbSLSI.Name = "rdbSLSI";
            this.rdbSLSI.Size = new System.Drawing.Size(46, 17);
            this.rdbSLSI.TabIndex = 0;
            this.rdbSLSI.TabStop = true;
            this.rdbSLSI.Text = "SLSI";
            this.rdbSLSI.UseVisualStyleBackColor = true;
            // 
            // rdbETC
            // 
            this.rdbETC.AutoSize = true;
            this.rdbETC.Location = new System.Drawing.Point(136, 4);
            this.rdbETC.Name = "rdbETC";
            this.rdbETC.Size = new System.Drawing.Size(44, 17);
            this.rdbETC.TabIndex = 0;
            this.rdbETC.Text = "ETC";
            this.rdbETC.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "Type";
            // 
            // label6
            // 
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(7, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(6, 17);
            this.label6.TabIndex = 34;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(400, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(274, 13);
            this.label7.TabIndex = 35;
            this.label7.Text = "※ Click H + 0 to display the target LOT.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(218, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Percentage of allowance(%)";
            // 
            // label9
            // 
            this.label9.Image = ((System.Drawing.Image)(resources.GetObject("label9.Image")));
            this.label9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Location = new System.Drawing.Point(207, 4);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(6, 17);
            this.label9.TabIndex = 45;
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboPer
            // 
            this.cboPer.FormattingEnabled = true;
            this.cboPer.Items.AddRange(new object[] {
            "115",
            "130",
            "145",
            "160",
            "175",
            "200"});
            this.cboPer.Location = new System.Drawing.Point(285, 1);
            this.cboPer.Name = "cboPer";
            this.cboPer.Size = new System.Drawing.Size(99, 21);
            this.cboPer.TabIndex = 46;
            this.cboPer.Text = "115";
            // 
            // PRD010904
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010904";
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
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.ComboBox cdvTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rdbETC;
        private System.Windows.Forms.RadioButton rdbSLSI;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPer;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}
