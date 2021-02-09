namespace Hana.PRD
{
    partial class PRD010411
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
            this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblDate = new System.Windows.Forms.Label();
            this.gbBaseGubn = new System.Windows.Forms.GroupBox();
            this.rbBaseTime06 = new System.Windows.Forms.RadioButton();
            this.rbBaseTime22 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cdvFromDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.txtLotType = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtMatID = new System.Windows.Forms.TextBox();
            this.lblLotType = new System.Windows.Forms.Label();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvToDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label1 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
            this.gbBaseGubn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(134, 13);
            this.lblTitle.Text = "Inter-operation receipts and disbursements (Detailed operation)";
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
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Controls.Add(this.txtLotType);
            this.pnlCondition2.Controls.Add(this.lblLotType);
            this.pnlCondition2.Controls.Add(this.cdvFactory);
            this.pnlCondition2.Controls.Add(this.txtMatID);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cdvToDate);
            this.pnlCondition1.Controls.Add(this.gbBaseGubn);
            this.pnlCondition1.Controls.Add(this.cdvFromDate);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.SS01);
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
            // SS01
            // 
            this.SS01.About = "4.0.2001.2005";
            this.SS01.AccessibleDescription = "SS01, Sheet1, Row 0, Column 0, ";
            this.SS01.BackColor = System.Drawing.Color.White;
            this.SS01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SS01.Location = new System.Drawing.Point(3, 3);
            this.SS01.Name = "SS01";
            this.SS01.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SS01.RPT_IsPreCellsType = true;
            this.SS01.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.SS01.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS01_Sheet1});
            this.SS01.Size = new System.Drawing.Size(794, 445);
            this.SS01.TabIndex = 2;
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
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(16, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(62, 13);
            this.lblDate.TabIndex = 68;
            this.lblDate.Text = "receipts and disbursements period:";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbBaseGubn
            // 
            this.gbBaseGubn.Controls.Add(this.rbBaseTime06);
            this.gbBaseGubn.Controls.Add(this.rbBaseTime22);
            this.gbBaseGubn.Controls.Add(this.label6);
            this.gbBaseGubn.Location = new System.Drawing.Point(335, -7);
            this.gbBaseGubn.Name = "gbBaseGubn";
            this.gbBaseGubn.Size = new System.Drawing.Size(185, 30);
            this.gbBaseGubn.TabIndex = 73;
            this.gbBaseGubn.TabStop = false;
            // 
            // rbBaseTime06
            // 
            this.rbBaseTime06.AutoSize = true;
            this.rbBaseTime06.Location = new System.Drawing.Point(72, 9);
            this.rbBaseTime06.Name = "rbBaseTime06";
            this.rbBaseTime06.Size = new System.Drawing.Size(49, 17);
            this.rbBaseTime06.TabIndex = 68;
            this.rbBaseTime06.Text = "06시";
            this.rbBaseTime06.UseVisualStyleBackColor = true;
            // 
            // rbBaseTime22
            // 
            this.rbBaseTime22.AutoSize = true;
            this.rbBaseTime22.Checked = true;
            this.rbBaseTime22.Location = new System.Drawing.Point(127, 9);
            this.rbBaseTime22.Name = "rbBaseTime22";
            this.rbBaseTime22.Size = new System.Drawing.Size(49, 17);
            this.rbBaseTime22.TabIndex = 67;
            this.rbBaseTime22.TabStop = true;
            this.rbBaseTime22.Text = "22시";
            this.rbBaseTime22.UseVisualStyleBackColor = true;
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
            // cdvFromDate
            // 
            this.cdvFromDate.CustomFormat = "yyyy-MM-dd";
            this.cdvFromDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvFromDate.Location = new System.Drawing.Point(79, 2);
            this.cdvFromDate.Name = "cdvFromDate";
            this.cdvFromDate.Size = new System.Drawing.Size(100, 20);
            this.cdvFromDate.TabIndex = 80;
            this.cdvFromDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.기간일;
            this.cdvFromDate.Value = new System.DateTime(2011, 1, 17, 0, 0, 0, 0);
            // 
            // txtLotType
            // 
            this.txtLotType.Location = new System.Drawing.Point(401, 2);
            this.txtLotType.Name = "txtLotType";
            this.txtLotType.Size = new System.Drawing.Size(119, 21);
            this.txtLotType.TabIndex = 73;
            this.txtLotType.Text = "P%";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(16, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(62, 13);
            this.lblProduct.TabIndex = 72;
            this.lblProduct.Text = "PRODUCT :";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMatID
            // 
            this.txtMatID.Location = new System.Drawing.Point(79, 2);
            this.txtMatID.Name = "txtMatID";
            this.txtMatID.Size = new System.Drawing.Size(227, 21);
            this.txtMatID.TabIndex = 71;
            this.txtMatID.Text = "%";
            this.txtMatID.TextChanged += new System.EventHandler(this.txtMatID_TextChanged);
            // 
            // lblLotType
            // 
            this.lblLotType.AutoSize = true;
            this.lblLotType.Location = new System.Drawing.Point(335, 6);
            this.lblLotType.Name = "lblLotType";
            this.lblLotType.Size = new System.Drawing.Size(60, 13);
            this.lblLotType.TabIndex = 74;
            this.lblLotType.Text = "LOT TYPE :";
            this.lblLotType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(596, 3);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(188, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 75;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // cdvToDate
            // 
            this.cdvToDate.CustomFormat = "yyyy-MM-dd";
            this.cdvToDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvToDate.Location = new System.Drawing.Point(206, 2);
            this.cdvToDate.Name = "cdvToDate";
            this.cdvToDate.Size = new System.Drawing.Size(100, 20);
            this.cdvToDate.TabIndex = 81;
            this.cdvToDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.기간일;
            this.cdvToDate.Value = new System.DateTime(2011, 1, 17, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 82;
            this.label1.Text = "~";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PRD010411
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010411";
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            this.gbBaseGubn.ResumeLayout(false);
            this.gbBaseGubn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
        private System.Windows.Forms.Label label1;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvToDate;
        private FarPoint.Win.Spread.SheetView SS01_Sheet1;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox gbBaseGubn;
        private System.Windows.Forms.RadioButton rbBaseTime06;
        private System.Windows.Forms.RadioButton rbBaseTime22;
        private System.Windows.Forms.Label label6;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvFromDate;
        private System.Windows.Forms.TextBox txtLotType;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtMatID;
        private System.Windows.Forms.Label lblLotType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
    }
}
