namespace Hana.MAT
{
    partial class MAT070205
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAT070205));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvMatType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cboMonth = new System.Windows.Forms.ComboBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lbl_Month = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLotQty = new System.Windows.Forms.Label();
            this.cboLotQty = new System.Windows.Forms.ComboBox();
            this.cboTranCode = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtMatID = new System.Windows.Forms.TextBox();
            this.cdvVendor = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvStorage = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.lblTitle.Size = new System.Drawing.Size(143, 13);
            this.lblTitle.Text = "Inquiry receipts and disbursements by Material between Warehouses";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvStorage);
            this.pnlCondition2.Controls.Add(this.cdvVendor);
            this.pnlCondition2.Controls.Add(this.label6);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Controls.Add(this.txtMatID);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.lblLotQty);
            this.pnlCondition1.Controls.Add(this.cboLotQty);
            this.pnlCondition1.Controls.Add(this.cboYear);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cboTranCode);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.lblYear);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.cboMonth);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.lbl_Month);
            this.pnlCondition1.Controls.Add(this.cdvMatType);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
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
            // cdvMatType
            // 
            this.cdvMatType.BackColor = System.Drawing.Color.Transparent;
            this.cdvMatType.bMultiSelect = false;
            this.cdvMatType.ConditionText = "Material type";
            this.cdvMatType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvMatType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatType.Location = new System.Drawing.Point(204, 4);
            this.cdvMatType.MandatoryFlag = false;
            this.cdvMatType.Name = "cdvMatType";
            this.cdvMatType.ParentValue = "";
            this.cdvMatType.sCodeColumnName = "KEY_1";
            this.cdvMatType.sDynamicQuery = "";           
            this.cdvMatType.Size = new System.Drawing.Size(180, 21);
            this.cdvMatType.sTableName = "";
            this.cdvMatType.sValueColumnName = "DATA_1";
            this.cdvMatType.TabIndex = 0;
            this.cdvMatType.VisibleValueButton = true;
            this.cdvMatType.ValueButtonPress += new System.EventHandler(this.cdvMatType_ValueButtonPress);
            // 
            // cboMonth
            // 
            this.cboMonth.FormattingEnabled = true;
            this.cboMonth.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cboMonth.Location = new System.Drawing.Point(576, 2);
            this.cboMonth.Name = "cboMonth";
            this.cboMonth.Size = new System.Drawing.Size(57, 21);
            this.cboMonth.TabIndex = 59;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(522, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 58;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Month
            // 
            this.lbl_Month.AutoSize = true;
            this.lbl_Month.Location = new System.Drawing.Point(533, 6);
            this.lbl_Month.Name = "lbl_Month";
            this.lbl_Month.Size = new System.Drawing.Size(37, 13);
            this.lbl_Month.TabIndex = 57;
            this.lbl_Month.Text = "Month";
            // 
            // cboYear
            // 
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Location = new System.Drawing.Point(461, 2);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(57, 21);
            this.cboYear.TabIndex = 65;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(414, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 64;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(425, 6);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(29, 13);
            this.lblYear.TabIndex = 63;
            this.lblYear.Text = "Year";
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(667, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 68;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLotQty
            // 
            this.lblLotQty.AutoSize = true;
            this.lblLotQty.Location = new System.Drawing.Point(678, 6);
            this.lblLotQty.Name = "lblLotQty";
            this.lblLotQty.Size = new System.Drawing.Size(44, 13);
            this.lblLotQty.TabIndex = 67;
            this.lblLotQty.Text = "View By";
            // 
            // cboLotQty
            // 
            this.cboLotQty.FormattingEnabled = true;
            this.cboLotQty.Items.AddRange(new object[] {
            "LOT",
            "QTY"});
            this.cboLotQty.Location = new System.Drawing.Point(725, 2);
            this.cboLotQty.Name = "cboLotQty";
            this.cboLotQty.Size = new System.Drawing.Size(57, 21);
            this.cboLotQty.TabIndex = 66;
            // 
            // cboTranCode
            // 
            this.cboTranCode.FormattingEnabled = true;
            this.cboTranCode.Items.AddRange(new object[] {
            "warehousing",
            "release",
            "input",
            "Subsidiary material release"});
            this.cboTranCode.Location = new System.Drawing.Point(84, 4);
            this.cboTranCode.Name = "cboTranCode";
            this.cboTranCode.Size = new System.Drawing.Size(91, 21);
            this.cboTranCode.TabIndex = 69;
            this.cboTranCode.Text = "warehousing";
            this.cboTranCode.SelectedIndexChanged += new System.EventHandler(this.cboTranCode_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 70;
            this.label4.Text = "Classification code";
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(8, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 71;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(207, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(6, 17);
            this.label6.TabIndex = 74;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(218, 9);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(55, 13);
            this.lblProduct.TabIndex = 73;
            this.lblProduct.Text = "Material code";
            // 
            // txtMatID
            // 
            this.txtMatID.Location = new System.Drawing.Point(284, 5);
            this.txtMatID.Name = "txtMatID";
            this.txtMatID.Size = new System.Drawing.Size(100, 21);
            this.txtMatID.TabIndex = 72;
            this.txtMatID.Text = "%";
            // 
            // cdvVendor
            // 
            this.cdvVendor.BackColor = System.Drawing.Color.Transparent;
            this.cdvVendor.bMultiSelect = true;
            this.cdvVendor.ConditionText = "Vendor code";
            this.cdvVendor.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvVendor.ControlRef = true;
            this.cdvVendor.Enabled = false;
            this.cdvVendor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvVendor.Location = new System.Drawing.Point(411, 5);
            this.cdvVendor.MandatoryFlag = false;
            this.cdvVendor.Name = "cdvVendor";
            this.cdvVendor.ParentValue = "";
            this.cdvVendor.sCodeColumnName = "Code";
            this.cdvVendor.sDynamicQuery = "";            
            this.cdvVendor.Size = new System.Drawing.Size(180, 21);
            this.cdvVendor.sTableName = "";
            this.cdvVendor.sValueColumnName = "NAME";
            this.cdvVendor.TabIndex = 89;
            this.cdvVendor.VisibleValueButton = true;
            // 
            // cdvStorage
            // 
            this.cdvStorage.BackColor = System.Drawing.Color.Transparent;
            this.cdvStorage.bMultiSelect = false;
            this.cdvStorage.ConditionText = "Storage location";
            this.cdvStorage.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvStorage.ControlRef = true;
            this.cdvStorage.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvStorage.Location = new System.Drawing.Point(4, 5);
            this.cdvStorage.MandatoryFlag = false;
            this.cdvStorage.Name = "cdvStorage";
            this.cdvStorage.ParentValue = "";
            this.cdvStorage.sCodeColumnName = "Code";
            this.cdvStorage.sDynamicQuery = "";            
            this.cdvStorage.Size = new System.Drawing.Size(180, 21);
            this.cdvStorage.sTableName = "";
            this.cdvStorage.sValueColumnName = "NAME";
            this.cdvStorage.TabIndex = 90;
            this.cdvStorage.VisibleValueButton = true;
            this.cdvStorage.ValueButtonPress += new System.EventHandler(this.cdvStorage_ValueButtonPress);
            // 
            // MAT070205
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "MAT070205";
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

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvMatType;
        private System.Windows.Forms.ComboBox cboMonth;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lbl_Month;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLotQty;
        private System.Windows.Forms.ComboBox cboLotQty;
        private System.Windows.Forms.ComboBox cboTranCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtMatID;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvVendor;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStorage;
    }
}
