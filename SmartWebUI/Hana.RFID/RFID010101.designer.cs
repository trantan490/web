namespace Hana.PRD
{
    partial class RFID010101
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
            this.lbl_Product = new System.Windows.Forms.Label();
            this.txtResourceID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb06 = new System.Windows.Forms.RadioButton();
            this.rb05 = new System.Windows.Forms.RadioButton();
            this.rb04 = new System.Windows.Forms.RadioButton();
            this.rb03 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.rb02 = new System.Windows.Forms.RadioButton();
            this.rb01 = new System.Windows.Forms.RadioButton();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label3 = new System.Windows.Forms.Label();
            this.cdvFromToTime = new Miracom.SmartWeb.UI.Controls.udcDurationDate2();
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
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(89, 13);
            this.lblTitle.Text = "RFID Operationing Status";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.groupBox1);
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition1, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.groupBox1, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition2, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition3, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition4, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition5, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition6, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition7, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition8, 0);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 177);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 153);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 129);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 105);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 81);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 57);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 11);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvFromToTime);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.txtResourceID);
            this.pnlCondition2.Controls.Add(this.lbl_Product);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFactory);
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
            this.SS01.TabIndex = 1;
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
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(14, 5);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(66, 13);
            this.lbl_Product.TabIndex = 16;
            this.lbl_Product.Text = "Resource ID";
            this.lbl_Product.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtResourceID
            // 
            this.txtResourceID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResourceID.Location = new System.Drawing.Point(86, 2);
            this.txtResourceID.Name = "txtResourceID";
            this.txtResourceID.Size = new System.Drawing.Size(143, 21);
            this.txtResourceID.TabIndex = 15;
            this.txtResourceID.Text = "%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb06);
            this.groupBox1.Controls.Add(this.rb05);
            this.groupBox1.Controls.Add(this.rb04);
            this.groupBox1.Controls.Add(this.rb03);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rb02);
            this.groupBox1.Controls.Add(this.rb01);
            this.groupBox1.Location = new System.Drawing.Point(13, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 30);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // rb06
            // 
            this.rb06.AutoSize = true;
            this.rb06.Location = new System.Drawing.Point(321, 9);
            this.rb06.Name = "rb06";
            this.rb06.Size = new System.Drawing.Size(43, 17);
            this.rb06.TabIndex = 76;
            this.rb06.Text = "SST";
            this.rb06.UseVisualStyleBackColor = true;
            // 
            // rb05
            // 
            this.rb05.AutoSize = true;
            this.rb05.Location = new System.Drawing.Point(271, 9);
            this.rb05.Name = "rb05";
            this.rb05.Size = new System.Drawing.Size(44, 17);
            this.rb05.TabIndex = 75;
            this.rb05.Text = "SBA";
            this.rb05.UseVisualStyleBackColor = true;
            // 
            // rb04
            // 
            this.rb04.AutoSize = true;
            this.rb04.Location = new System.Drawing.Point(222, 9);
            this.rb04.Name = "rb04";
            this.rb04.Size = new System.Drawing.Size(43, 17);
            this.rb04.TabIndex = 74;
            this.rb04.Text = "M/K";
            this.rb04.UseVisualStyleBackColor = true;
            // 
            // rb03
            // 
            this.rb03.AutoSize = true;
            this.rb03.Location = new System.Drawing.Point(172, 9);
            this.rb03.Name = "rb03";
            this.rb03.Size = new System.Drawing.Size(44, 17);
            this.rb03.TabIndex = 73;
            this.rb03.Text = "M/D";
            this.rb03.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "Operation Category:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rb02
            // 
            this.rb02.AutoSize = true;
            this.rb02.Location = new System.Drawing.Point(121, 9);
            this.rb02.Name = "rb02";
            this.rb02.Size = new System.Drawing.Size(45, 17);
            this.rb02.TabIndex = 71;
            this.rb02.Text = "W/B";
            this.rb02.UseVisualStyleBackColor = true;
            // 
            // rb01
            // 
            this.rb01.AutoSize = true;
            this.rb01.Checked = true;
            this.rb01.Location = new System.Drawing.Point(72, 9);
            this.rb01.Name = "rb01";
            this.rb01.Size = new System.Drawing.Size(43, 17);
            this.rb01.TabIndex = 70;
            this.rb01.TabStop = true;
            this.rb01.Text = "D/A";
            this.rb01.UseVisualStyleBackColor = true;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(390, 2);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 18;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(316, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "inquiry period:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cdvFromToTime
            // 
            this.cdvFromToTime.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToTime.Location = new System.Drawing.Point(386, 2);
            this.cdvFromToTime.Name = "cdvFromToTime";
            this.cdvFromToTime.RestrictedDayCount = 60;
            this.cdvFromToTime.Size = new System.Drawing.Size(349, 21);
            this.cdvFromToTime.TabIndex = 81;
            // 
            // RFID010101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "RFID010101";
            this.Load += new System.EventHandler(this.RFID010101_Load);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
            this.pnlCondition1.ResumeLayout(false);
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
        private FarPoint.Win.Spread.SheetView SS01_Sheet1;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txtResourceID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rb02;
        private System.Windows.Forms.RadioButton rb01;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.RadioButton rb06;
        private System.Windows.Forms.RadioButton rb05;
        private System.Windows.Forms.RadioButton rb04;
        private System.Windows.Forms.RadioButton rb03;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate2 cdvFromToTime;
    }
}
