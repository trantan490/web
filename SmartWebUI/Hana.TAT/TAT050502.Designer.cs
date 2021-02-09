﻿namespace Hana.TAT
{
    partial class TAT050502
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TAT050502));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTimeBase = new System.Windows.Forms.ComboBox();
            this.chkEQ = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkTime = new System.Windows.Forms.CheckBox();
            this.chkKcps = new System.Windows.Forms.CheckBox();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cdvOperGroup = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ckbBG = new System.Windows.Forms.CheckBox();
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
            this.lblTitle.Size = new System.Drawing.Size(81, 13);
            this.lblTitle.Text = "Dynamic TAT";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvOperGroup);
            this.pnlCondition2.Controls.Add(this.label10);
            this.pnlCondition2.Controls.Add(this.label11);
            this.pnlCondition2.Controls.Add(this.ckbBG);
            this.pnlCondition2.Controls.Add(this.chkKcps);
            this.pnlCondition2.Controls.Add(this.chkTime);
            this.pnlCondition2.Controls.Add(this.label7);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Controls.Add(this.txtSearchProduct);
            this.pnlCondition2.Controls.Add(this.chkEQ);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label8);
            this.pnlCondition1.Controls.Add(this.label9);
            this.pnlCondition1.Controls.Add(this.label6);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.cboTimeBase);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.cdvDate);
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
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
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
            this.cdvFactory.TabIndex = 43;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(7, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 34;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(18, 7);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 33;
            this.lblProduct.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(84, 2);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(100, 21);
            this.txtSearchProduct.TabIndex = 32;
            this.txtSearchProduct.Text = "HX%";
            // 
            // lblIcon
            // 
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(207, 5);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 62;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Date";
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(285, 1);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(100, 20);
            this.cdvDate.TabIndex = 60;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 2, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(610, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 65;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(619, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 64;
            this.label4.Text = "Time Base";
            // 
            // cboTimeBase
            // 
            this.cboTimeBase.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.cboTimeBase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeBase.FormattingEnabled = true;
            this.cboTimeBase.Items.AddRange(new object[] {
            "06 hours",
            "22 hours"});
            this.cboTimeBase.Location = new System.Drawing.Point(687, 0);
            this.cboTimeBase.Name = "cboTimeBase";
            this.cboTimeBase.Size = new System.Drawing.Size(58, 21);
            this.cboTimeBase.TabIndex = 63;
            // 
            // chkEQ
            // 
            this.chkEQ.AutoSize = true;
            this.chkEQ.Location = new System.Drawing.Point(218, 5);
            this.chkEQ.Name = "chkEQ";
            this.chkEQ.Size = new System.Drawing.Size(86, 17);
            this.chkEQ.TabIndex = 17;
            this.chkEQ.Text = "EQ standard";
            this.chkEQ.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(206, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 64;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(215, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(6, 17);
            this.label6.TabIndex = 65;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(7, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 35;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkTime
            // 
            this.chkTime.AutoSize = true;
            this.chkTime.Location = new System.Drawing.Point(291, 5);
            this.chkTime.Name = "chkTime";
            this.chkTime.Size = new System.Drawing.Size(69, 17);
            this.chkTime.TabIndex = 65;
            this.chkTime.Text = "Time unit";
            this.chkTime.UseVisualStyleBackColor = true;
            // 
            // chkKcps
            // 
            this.chkKcps.AutoSize = true;
            this.chkKcps.Checked = true;
            this.chkKcps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkKcps.Location = new System.Drawing.Point(478, 5);
            this.chkKcps.Name = "chkKcps";
            this.chkKcps.Size = new System.Drawing.Size(48, 17);
            this.chkKcps.TabIndex = 126;
            this.chkKcps.Text = "Kcps";
            this.chkKcps.UseVisualStyleBackColor = true;
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(498, 1);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(100, 21);
            this.cdvLotType.TabIndex = 68;
            this.cdvLotType.Text = "ALL";
            // 
            // label8
            // 
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(421, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(6, 17);
            this.label8.TabIndex = 67;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(433, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 66;
            this.label9.Text = "Lot Type";
            // 
            // cdvOperGroup
            // 
            this.cdvOperGroup.FormattingEnabled = true;
            this.cdvOperGroup.Items.AddRange(new object[] {
            "ALL",
            "SAW",
            "DA",
            "WB",
            "MOLD",
            "FINISH"});
            this.cdvOperGroup.Location = new System.Drawing.Point(684, 2);
            this.cdvOperGroup.Name = "cdvOperGroup";
            this.cdvOperGroup.Size = new System.Drawing.Size(64, 21);
            this.cdvOperGroup.TabIndex = 129;
            this.cdvOperGroup.Text = "ALL";
            // 
            // label10
            // 
            this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(607, 4);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 17);
            this.label10.TabIndex = 128;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(619, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 13);
            this.label11.TabIndex = 127;
            this.label11.Text = "Operation group";
            // 
            // ckbBG
            // 
            this.ckbBG.AutoSize = true;
            this.ckbBG.Location = new System.Drawing.Point(378, 5);
            this.ckbBG.Name = "ckbBG";
            this.ckbBG.Size = new System.Drawing.Size(108, 17);
            this.ckbBG.TabIndex = 126;
            this.ckbBG.Text = "Including BG WIP";
            this.ckbBG.UseVisualStyleBackColor = true;
            // 
            // TAT050502
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TAT050502";
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

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTimeBase;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.CheckBox chkEQ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkTime;
        private System.Windows.Forms.CheckBox chkKcps;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cdvOperGroup;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ckbBG;
    }
}
