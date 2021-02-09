namespace Hana.PRD
{
    partial class PRD010909
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010909));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFromDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.lblDate = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.rdbLot = new System.Windows.Forms.RadioButton();
            this.rdbSlip = new System.Windows.Forms.RadioButton();
            this.txtTime1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTime2 = new System.Windows.Forms.TextBox();
            this.cboTime1 = new System.Windows.Forms.ComboBox();
            this.cdvToDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cboTime2 = new System.Windows.Forms.ComboBox();
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(104, 13);
            this.lblTitle.Text = "Create Semifinished Product File";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cboTime2);
            this.pnlCondition2.Controls.Add(this.cboTime1);
            this.pnlCondition2.Controls.Add(this.txtTime2);
            this.pnlCondition2.Controls.Add(this.cdvToDate);
            this.pnlCondition2.Controls.Add(this.cdvFromDate);
            this.pnlCondition2.Controls.Add(this.txtTime1);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.lblDate);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.label3);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.rdbSlip);
            this.pnlCondition1.Controls.Add(this.rdbLot);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 68);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Size = new System.Drawing.Size(800, 508);
            // 
            // btnDetail
            // 
            this.btnDetail.Enabled = false;
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
            this.btnSort.Enabled = false;
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
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
            this.spdData.Size = new System.Drawing.Size(794, 505);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Enabled = false;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(4, 0);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";            
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 10;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvFromDate
            // 
            this.cdvFromDate.CustomFormat = "yyyy-MM-dd";
            this.cdvFromDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvFromDate.Location = new System.Drawing.Point(53, 2);
            this.cdvFromDate.Name = "cdvFromDate";
            this.cdvFromDate.Size = new System.Drawing.Size(106, 20);
            this.cdvFromDate.TabIndex = 16;
            this.cdvFromDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvFromDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(16, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(31, 13);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "From";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(6, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 18;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdbLot
            // 
            this.rdbLot.AutoSize = true;
            this.rdbLot.Checked = true;
            this.rdbLot.Location = new System.Drawing.Point(216, 3);
            this.rdbLot.Name = "rdbLot";
            this.rdbLot.Size = new System.Drawing.Size(44, 17);
            this.rdbLot.TabIndex = 19;
            this.rdbLot.TabStop = true;
            this.rdbLot.Text = "LOT";
            this.rdbLot.UseVisualStyleBackColor = true;
            // 
            // rdbSlip
            // 
            this.rdbSlip.AutoSize = true;
            this.rdbSlip.Location = new System.Drawing.Point(266, 3);
            this.rdbSlip.Name = "rdbSlip";
            this.rdbSlip.Size = new System.Drawing.Size(46, 17);
            this.rdbSlip.TabIndex = 20;
            this.rdbSlip.Text = "SLIP";
            this.rdbSlip.UseVisualStyleBackColor = true;
            // 
            // txtTime1
            // 
            this.txtTime1.Location = new System.Drawing.Point(216, 1);
            this.txtTime1.MaxLength = 2;
            this.txtTime1.Name = "txtTime1";
            this.txtTime1.Size = new System.Drawing.Size(34, 21);
            this.txtTime1.TabIndex = 21;
            this.txtTime1.Text = "00";
            this.txtTime1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "~";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(285, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "To";
            // 
            // txtTime2
            // 
            this.txtTime2.Location = new System.Drawing.Point(478, 1);
            this.txtTime2.MaxLength = 2;
            this.txtTime2.Name = "txtTime2";
            this.txtTime2.Size = new System.Drawing.Size(34, 21);
            this.txtTime2.TabIndex = 21;
            this.txtTime2.Text = "00";
            this.txtTime2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cboTime1
            // 
            this.cboTime1.FormattingEnabled = true;
            this.cboTime1.Items.AddRange(new object[] {
            "00 hours",
            "01 hours",
            "02 hours",
            "03 hours",
            "04 hours",
            "05 hours",
            "06 hours",
            "07 hours",
            "08 hours",
            "09 hours",
            "10 hours",
            "11 hours",
            "12 hours",
            "13 hours",
            "14 hours",
            "15 hours",
            "16 hours",
            "17 hours",
            "18 hours",
            "19 hours",
            "20 hours",
            "21 hours",
            "22 hours",
            "23 hours"});
            this.cboTime1.Location = new System.Drawing.Point(165, 1);
            this.cboTime1.Name = "cboTime1";
            this.cboTime1.Size = new System.Drawing.Size(47, 21);
            this.cboTime1.TabIndex = 23;
            this.cboTime1.Text = "22 hours";
            // 
            // cdvToDate
            // 
            this.cdvToDate.CustomFormat = "yyyy-MM-dd";
            this.cdvToDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvToDate.Location = new System.Drawing.Point(314, 2);
            this.cdvToDate.Name = "cdvToDate";
            this.cdvToDate.Size = new System.Drawing.Size(106, 20);
            this.cdvToDate.TabIndex = 16;
            this.cdvToDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvToDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // cboTime2
            // 
            this.cboTime2.FormattingEnabled = true;
            this.cboTime2.Items.AddRange(new object[] {
            "00 hours",
            "01 hours",
            "02 hours",
            "03 hours",
            "04 hours",
            "05 hours",
            "06 hours",
            "07 hours",
            "08 hours",
            "09 hours",
            "10 hours",
            "11 hours",
            "12 hours",
            "13 hours",
            "14 hours",
            "15 hours",
            "16 hours",
            "17 hours",
            "18 hours",
            "19 hours",
            "20 hours",
            "21 hours",
            "22 hours",
            "23 hours"});
            this.cboTime2.Location = new System.Drawing.Point(426, 1);
            this.cboTime2.Name = "cboTime2";
            this.cboTime2.Size = new System.Drawing.Size(47, 21);
            this.cboTime2.TabIndex = 23;
            this.cboTime2.Text = "22 hours";
            // 
            // PRD010909
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010909";
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
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvFromDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.RadioButton rdbSlip;
        private System.Windows.Forms.RadioButton rdbLot;
        private System.Windows.Forms.TextBox txtTime1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTime2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboTime1;
        private System.Windows.Forms.ComboBox cboTime2;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvToDate;
    }
}
