namespace Hana.PRD
{
    partial class PRD011012
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD011012));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label3 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rdbLipas1 = new System.Windows.Forms.RadioButton();
            this.rdbLipas2 = new System.Windows.Forms.RadioButton();
            this.rdbVolpas = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboVersion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
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
            this.lblTitle.Size = new System.Drawing.Size(145, 13);
            this.lblTitle.Text = "Daily performance index";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(1199, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.rdbVolpas);
            this.pnlCondition2.Controls.Add(this.rdbLipas2);
            this.pnlCondition2.Controls.Add(this.rdbLipas1);
            this.pnlCondition2.Controls.Add(this.txtProduct);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Size = new System.Drawing.Size(1197, 24);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.comboBox1);
            this.pnlCondition1.Controls.Add(this.label8);
            this.pnlCondition1.Controls.Add(this.label9);
            this.pnlCondition1.Controls.Add(this.cboVersion);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.label7);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            this.pnlCondition1.Controls.Add(this.ckbKpcs);
            this.pnlCondition1.Controls.Add(this.label6);
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Size = new System.Drawing.Size(1197, 24);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
            this.pnlWIPDetail.Size = new System.Drawing.Size(1199, 87);
            // 
            // pnlDetailCondition2
            // 
            this.pnlDetailCondition2.Size = new System.Drawing.Size(1197, 24);
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
            // pnlDetailCondition1
            // 
            this.pnlDetailCondition1.Size = new System.Drawing.Size(1197, 24);
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
            this.pnlMain.Size = new System.Drawing.Size(1199, 421);
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
            // pnlDetailCondition3
            // 
            this.pnlDetailCondition3.Size = new System.Drawing.Size(1197, 24);
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
            this.spdData.Size = new System.Drawing.Size(1193, 418);
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
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 79;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(19, 5);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 78;
            this.lblProduct.Text = "Product";
            // 
            // txtProduct
            // 
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Location = new System.Drawing.Point(82, 1);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(102, 21);
            this.txtProduct.TabIndex = 77;
            this.txtProduct.Text = "%";
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Checked = true;
            this.ckbKpcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbKpcs.Location = new System.Drawing.Point(405, 5);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 48;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(281, 1);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(88, 20);
            this.cdvDate.TabIndex = 49;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2012, 10, 30, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(740, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Version";
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(727, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 79;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdbLipas1
            // 
            this.rdbLipas1.AutoSize = true;
            this.rdbLipas1.Checked = true;
            this.rdbLipas1.Location = new System.Drawing.Point(210, 4);
            this.rdbLipas1.Name = "rdbLipas1";
            this.rdbLipas1.Size = new System.Drawing.Size(77, 17);
            this.rdbLipas1.TabIndex = 80;
            this.rdbLipas1.TabStop = true;
            this.rdbLipas1.Text = "Lipas(item)";
            this.rdbLipas1.UseVisualStyleBackColor = true;
            this.rdbLipas1.CheckedChanged += new System.EventHandler(this.rdbLipas1_CheckedChanged);
            // 
            // rdbLipas2
            // 
            this.rdbLipas2.AutoSize = true;
            this.rdbLipas2.Location = new System.Drawing.Point(294, 4);
            this.rdbLipas2.Name = "rdbLipas2";
            this.rdbLipas2.Size = new System.Drawing.Size(91, 17);
            this.rdbLipas2.TabIndex = 81;
            this.rdbLipas2.TabStop = true;
            this.rdbLipas2.Text = "Lipas(Volume)";
            this.rdbLipas2.UseVisualStyleBackColor = true;
            // 
            // rdbVolpas
            // 
            this.rdbVolpas.AutoSize = true;
            this.rdbVolpas.Location = new System.Drawing.Point(390, 4);
            this.rdbVolpas.Name = "rdbVolpas";
            this.rdbVolpas.Size = new System.Drawing.Size(56, 17);
            this.rdbVolpas.TabIndex = 82;
            this.rdbVolpas.Text = "Volpas";
            this.rdbVolpas.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(474, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "LIPAS : Line Item Performance Against Schedule";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(474, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(238, 13);
            this.label4.TabIndex = 80;
            this.label4.Text = "VOLPAS : Volume Performance Against Schedule";
            // 
            // cboVersion
            // 
            this.cboVersion.Enabled = false;
            this.cboVersion.FormattingEnabled = true;
            this.cboVersion.Items.AddRange(new object[] {
            "Sales plan",
            "Rev Plan",
            "Fixed plan"});
            this.cboVersion.Location = new System.Drawing.Point(793, 4);
            this.cboVersion.Name = "cboVersion";
            this.cboVersion.Size = new System.Drawing.Size(80, 21);
            this.cboVersion.TabIndex = 81;
            this.cboVersion.Text = "Rev Plan";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(224, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 78;
            this.label6.Text = "standard date";
            // 
            // label7
            // 
            this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(211, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(6, 17);
            this.label7.TabIndex = 79;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(912, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(6, 17);
            this.label8.TabIndex = 83;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(925, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 82;
            this.label9.Text = "StartDate";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = true;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Saturday",
            "Monday"});
            this.comboBox1.Location = new System.Drawing.Point(985, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(80, 21);
            this.comboBox1.TabIndex = 84;
            this.comboBox1.Text = "Saturday";
            // 
            // PRD011012
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD011012";
            this.Size = new System.Drawing.Size(1199, 600);
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
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.Label label5;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbVolpas;
        private System.Windows.Forms.RadioButton rdbLipas2;
        private System.Windows.Forms.RadioButton rdbLipas1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboVersion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}
