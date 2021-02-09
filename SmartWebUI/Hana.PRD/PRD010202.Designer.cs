namespace Hana.PRD
{
    partial class PRD010202
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010202));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.lblDate = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblLabel3 = new System.Windows.Forms.Label();
            this.lblLabel9 = new System.Windows.Forms.Label();
            this.lblLabel7 = new System.Windows.Forms.Label();
            this.lblLabel5 = new System.Windows.Forms.Label();
            this.lblLabel1 = new System.Windows.Forms.Label();
            this.lblLabel2 = new System.Windows.Forms.Label();
            this.lblLabel6 = new System.Windows.Forms.Label();
            this.lblLabel4 = new System.Windows.Forms.Label();
            this.lblLabel8 = new System.Windows.Forms.Label();
            this.lblLabel10 = new System.Windows.Forms.Label();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.lblYesterday = new System.Windows.Forms.Label();
            this.lblSyslsi = new System.Windows.Forms.Label();
            this.lblMagam = new System.Windows.Forms.Label();
            this.lblJindo = new System.Windows.Forms.Label();
            this.lblRemain = new System.Windows.Forms.Label();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblMemo = new System.Windows.Forms.Label();
            this.lblMagam2 = new System.Windows.Forms.Label();
            this.lblJindo2 = new System.Windows.Forms.Label();
            this.lblRemain2 = new System.Windows.Forms.Label();
            this.ckbRev = new System.Windows.Forms.CheckBox();
            this.ckbWeek = new System.Windows.Forms.CheckBox();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.ckbCOB = new System.Windows.Forms.CheckBox();
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
            this.lblTitle.Size = new System.Drawing.Size(165, 13);
            this.lblTitle.Text = "TEST Progress Management";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.lblMemo);
            this.pnlCondition3.Controls.Add(this.lblRemain2);
            this.pnlCondition3.Controls.Add(this.lblMagam2);
            this.pnlCondition3.Controls.Add(this.lblToday);
            this.pnlCondition3.Controls.Add(this.lblJindo2);
            this.pnlCondition3.Controls.Add(this.lblLabel10);
            this.pnlCondition3.Controls.Add(this.lblLabel2);
            this.pnlCondition3.Controls.Add(this.lblLabel8);
            this.pnlCondition3.Controls.Add(this.lblLabel6);
            this.pnlCondition3.Controls.Add(this.lblLabel4);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.lblRemain);
            this.pnlCondition2.Controls.Add(this.lblLabel1);
            this.pnlCondition2.Controls.Add(this.lblJindo);
            this.pnlCondition2.Controls.Add(this.lblMagam);
            this.pnlCondition2.Controls.Add(this.lblSyslsi);
            this.pnlCondition2.Controls.Add(this.lblLabel3);
            this.pnlCondition2.Controls.Add(this.lblLabel9);
            this.pnlCondition2.Controls.Add(this.lblYesterday);
            this.pnlCondition2.Controls.Add(this.lblLabel7);
            this.pnlCondition2.Controls.Add(this.lblLabel5);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.ckbCOB);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label7);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.ckbWeek);
            this.pnlCondition1.Controls.Add(this.ckbRev);
            this.pnlCondition1.Controls.Add(this.ckbKpcs);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 116);
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
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(285, 1);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 16;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 4, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(217, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(75, 13);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "standard date";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(207, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 18;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLabel3
            // 
            this.lblLabel3.AutoSize = true;
            this.lblLabel3.Location = new System.Drawing.Point(232, 6);
            this.lblLabel3.Name = "lblLabel3";
            this.lblLabel3.Size = new System.Drawing.Size(44, 13);
            this.lblLabel3.TabIndex = 0;
            this.lblLabel3.Text = "SYSLSI:";
            // 
            // lblLabel9
            // 
            this.lblLabel9.AutoSize = true;
            this.lblLabel9.Location = new System.Drawing.Point(683, 6);
            this.lblLabel9.Name = "lblLabel9";
            this.lblLabel9.Size = new System.Drawing.Size(82, 13);
            this.lblLabel9.TabIndex = 1;
            this.lblLabel9.Text = "Remaining days";
            // 
            // lblLabel7
            // 
            this.lblLabel7.AutoSize = true;
            this.lblLabel7.Location = new System.Drawing.Point(537, 6);
            this.lblLabel7.Name = "lblLabel7";
            this.lblLabel7.Size = new System.Drawing.Size(72, 13);
            this.lblLabel7.TabIndex = 2;
            this.lblLabel7.Text = "Progress rate";
            // 
            // lblLabel5
            // 
            this.lblLabel5.AutoSize = true;
            this.lblLabel5.Location = new System.Drawing.Point(365, 6);
            this.lblLabel5.Name = "lblLabel5";
            this.lblLabel5.Size = new System.Drawing.Size(58, 13);
            this.lblLabel5.TabIndex = 3;
            this.lblLabel5.Text = "Month end";
            // 
            // lblLabel1
            // 
            this.lblLabel1.AutoSize = true;
            this.lblLabel1.Location = new System.Drawing.Point(18, 6);
            this.lblLabel1.Name = "lblLabel1";
            this.lblLabel1.Size = new System.Drawing.Size(174, 13);
            this.lblLabel1.TabIndex = 4;
            this.lblLabel1.Text = "Based on yesterday\'s performance";
            // 
            // lblLabel2
            // 
            this.lblLabel2.AutoSize = true;
            this.lblLabel2.Location = new System.Drawing.Point(18, 6);
            this.lblLabel2.Name = "lblLabel2";
            this.lblLabel2.Size = new System.Drawing.Size(154, 13);
            this.lblLabel2.TabIndex = 1;
            this.lblLabel2.Text = "Today\'s performance standard";
            // 
            // lblLabel6
            // 
            this.lblLabel6.AutoSize = true;
            this.lblLabel6.Location = new System.Drawing.Point(365, 6);
            this.lblLabel6.Name = "lblLabel6";
            this.lblLabel6.Size = new System.Drawing.Size(58, 13);
            this.lblLabel6.TabIndex = 17;
            this.lblLabel6.Text = "Month end";
            // 
            // lblLabel4
            // 
            this.lblLabel4.AutoSize = true;
            this.lblLabel4.Location = new System.Drawing.Point(235, 6);
            this.lblLabel4.Name = "lblLabel4";
            this.lblLabel4.Size = new System.Drawing.Size(41, 13);
            this.lblLabel4.TabIndex = 18;
            this.lblLabel4.Text = "MEMO:";
            // 
            // lblLabel8
            // 
            this.lblLabel8.AutoSize = true;
            this.lblLabel8.Location = new System.Drawing.Point(537, 7);
            this.lblLabel8.Name = "lblLabel8";
            this.lblLabel8.Size = new System.Drawing.Size(72, 13);
            this.lblLabel8.TabIndex = 19;
            this.lblLabel8.Text = "Progress rate";
            // 
            // lblLabel10
            // 
            this.lblLabel10.AutoSize = true;
            this.lblLabel10.Location = new System.Drawing.Point(683, 7);
            this.lblLabel10.Name = "lblLabel10";
            this.lblLabel10.Size = new System.Drawing.Size(82, 13);
            this.lblLabel10.TabIndex = 20;
            this.lblLabel10.Text = "Remaining days";
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(613, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 12;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // lblYesterday
            // 
            this.lblYesterday.AutoSize = true;
            this.lblYesterday.Location = new System.Drawing.Point(101, 5);
            this.lblYesterday.Name = "lblYesterday";
            this.lblYesterday.Size = new System.Drawing.Size(0, 13);
            this.lblYesterday.TabIndex = 5;
            // 
            // lblSyslsi
            // 
            this.lblSyslsi.AutoSize = true;
            this.lblSyslsi.Location = new System.Drawing.Point(282, 6);
            this.lblSyslsi.Name = "lblSyslsi";
            this.lblSyslsi.Size = new System.Drawing.Size(0, 13);
            this.lblSyslsi.TabIndex = 6;
            // 
            // lblMagam
            // 
            this.lblMagam.AutoSize = true;
            this.lblMagam.Location = new System.Drawing.Point(413, 5);
            this.lblMagam.Name = "lblMagam";
            this.lblMagam.Size = new System.Drawing.Size(0, 13);
            this.lblMagam.TabIndex = 7;
            // 
            // lblJindo
            // 
            this.lblJindo.AutoSize = true;
            this.lblJindo.Location = new System.Drawing.Point(586, 4);
            this.lblJindo.Name = "lblJindo";
            this.lblJindo.Size = new System.Drawing.Size(0, 13);
            this.lblJindo.TabIndex = 8;
            // 
            // lblRemain
            // 
            this.lblRemain.AutoSize = true;
            this.lblRemain.Location = new System.Drawing.Point(739, 4);
            this.lblRemain.Name = "lblRemain";
            this.lblRemain.Size = new System.Drawing.Size(0, 13);
            this.lblRemain.TabIndex = 9;
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(101, 5);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(0, 13);
            this.lblToday.TabIndex = 21;
            // 
            // lblMemo
            // 
            this.lblMemo.AutoSize = true;
            this.lblMemo.Location = new System.Drawing.Point(282, 6);
            this.lblMemo.Name = "lblMemo";
            this.lblMemo.Size = new System.Drawing.Size(0, 13);
            this.lblMemo.TabIndex = 22;
            // 
            // lblMagam2
            // 
            this.lblMagam2.AutoSize = true;
            this.lblMagam2.Location = new System.Drawing.Point(413, 5);
            this.lblMagam2.Name = "lblMagam2";
            this.lblMagam2.Size = new System.Drawing.Size(0, 13);
            this.lblMagam2.TabIndex = 23;
            // 
            // lblJindo2
            // 
            this.lblJindo2.AutoSize = true;
            this.lblJindo2.Location = new System.Drawing.Point(586, 6);
            this.lblJindo2.Name = "lblJindo2";
            this.lblJindo2.Size = new System.Drawing.Size(0, 13);
            this.lblJindo2.TabIndex = 24;
            // 
            // lblRemain2
            // 
            this.lblRemain2.AutoSize = true;
            this.lblRemain2.Location = new System.Drawing.Point(739, 6);
            this.lblRemain2.Name = "lblRemain2";
            this.lblRemain2.Size = new System.Drawing.Size(0, 13);
            this.lblRemain2.TabIndex = 25;
            // 
            // ckbRev
            // 
            this.ckbRev.AutoSize = true;
            this.ckbRev.Checked = true;
            this.ckbRev.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRev.Location = new System.Drawing.Point(661, 4);
            this.ckbRev.Name = "ckbRev";
            this.ckbRev.Size = new System.Drawing.Size(45, 17);
            this.ckbRev.TabIndex = 97;
            this.ckbRev.Text = "Rev";
            this.ckbRev.UseVisualStyleBackColor = true;
            // 
            // ckbWeek
            // 
            this.ckbWeek.AutoSize = true;
            this.ckbWeek.Location = new System.Drawing.Point(710, 4);
            this.ckbWeek.Name = "ckbWeek";
            this.ckbWeek.Size = new System.Drawing.Size(59, 17);
            this.ckbWeek.TabIndex = 98;
            this.ckbWeek.Text = "weekly";
            this.ckbWeek.UseVisualStyleBackColor = true;
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(495, 1);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(100, 21);
            this.cdvLotType.TabIndex = 101;
            this.cdvLotType.Text = "ALL";
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(418, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 100;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(430, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 99;
            this.label1.Text = "Lot Type";
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(4, 1);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "Code";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "Data";
            this.cdvFactory.TabIndex = 102;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueButtonPress += new System.EventHandler(this.cdvFactory_ValueButtonPress);
            // 
            // ckbCOB
            // 
            this.ckbCOB.AutoSize = true;
            this.ckbCOB.Checked = true;
            this.ckbCOB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCOB.Location = new System.Drawing.Point(772, 5);
            this.ckbCOB.Name = "ckbCOB";
            this.ckbCOB.Size = new System.Drawing.Size(93, 17);
            this.ckbCOB.TabIndex = 103;
            this.ckbCOB.Text = "Excluded COB";
            this.ckbCOB.UseVisualStyleBackColor = true;
            // 
            // PRD010202
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010202";
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
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblLabel3;
        private System.Windows.Forms.Label lblLabel1;
        private System.Windows.Forms.Label lblLabel5;
        private System.Windows.Forms.Label lblLabel7;
        private System.Windows.Forms.Label lblLabel9;
        private System.Windows.Forms.Label lblLabel10;
        private System.Windows.Forms.Label lblLabel8;
        private System.Windows.Forms.Label lblLabel4;
        private System.Windows.Forms.Label lblLabel6;
        private System.Windows.Forms.Label lblLabel2;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblRemain;
        private System.Windows.Forms.Label lblJindo;
        private System.Windows.Forms.Label lblMagam;
        private System.Windows.Forms.Label lblSyslsi;
        private System.Windows.Forms.Label lblYesterday;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.Label lblRemain2;
        private System.Windows.Forms.Label lblJindo2;
        private System.Windows.Forms.Label lblMagam2;
        private System.Windows.Forms.Label lblMemo;
        private System.Windows.Forms.CheckBox ckbRev;
        private System.Windows.Forms.CheckBox ckbWeek;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.CheckBox ckbCOB;
    }
}
