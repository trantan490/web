namespace Hana.MAT
{
    partial class MAT070301
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
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rbtEoh02 = new System.Windows.Forms.RadioButton();
            this.rbtEoh01 = new System.Windows.Forms.RadioButton();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.txtLotType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbTime02 = new System.Windows.Forms.RadioButton();
            this.rbTime03 = new System.Windows.Forms.RadioButton();
            this.rbTime01 = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.cdvMatType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label3 = new System.Windows.Forms.Label();
            this.udcMat_Code = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbHist_Del = new System.Windows.Forms.CheckBox();
            this.ckbPrd_View = new System.Windows.Forms.CheckBox();
            this.ChkLot = new System.Windows.Forms.CheckBox();
            this.ChkEmc = new System.Windows.Forms.CheckBox();
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
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.cdvFromToDate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(62, 13);
            this.lblTitle.Text = "Material Settlement";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.groupBox3);
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition1, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition2, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition3, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition4, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition5, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition6, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition7, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition8, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.groupBox3, 0);
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
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.ChkEmc);
            this.pnlCondition2.Controls.Add(this.ckbHist_Del);
            this.pnlCondition2.Controls.Add(this.udcMat_Code);
            this.pnlCondition2.Controls.Add(this.cdvMatType);
            this.pnlCondition2.Controls.Add(this.ckbKpcs);
            this.pnlCondition2.Controls.Add(this.txtLotType);
            this.pnlCondition2.Controls.Add(this.label5);
            this.pnlCondition2.Controls.Add(this.cdvFactory);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.ChkLot);
            this.pnlCondition1.Controls.Add(this.ckbPrd_View);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.groupBox1);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
            this.pnlWIPDetail.Size = new System.Drawing.Size(800, 66);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 158);
            this.pnlMain.Size = new System.Drawing.Size(800, 442);
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
            this.spdData.Size = new System.Drawing.Size(794, 439);
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
            // ckbKpcs
            // 
            this.ckbKpcs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(650, 5);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 17;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            this.ckbKpcs.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.rbtEoh02);
            this.groupBox1.Controls.Add(this.rbtEoh01);
            this.groupBox1.Location = new System.Drawing.Point(628, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 30);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 72;
            this.label4.Text = "EOH Classification:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbtEoh02
            // 
            this.rbtEoh02.AutoSize = true;
            this.rbtEoh02.Location = new System.Drawing.Point(116, 9);
            this.rbtEoh02.Name = "rbtEoh02";
            this.rbtEoh02.Size = new System.Drawing.Size(49, 17);
            this.rbtEoh02.TabIndex = 71;
            this.rbtEoh02.Text = "today";
            this.rbtEoh02.UseVisualStyleBackColor = true;
            // 
            // rbtEoh01
            // 
            this.rbtEoh01.AutoSize = true;
            this.rbtEoh01.Checked = true;
            this.rbtEoh01.Location = new System.Drawing.Point(64, 9);
            this.rbtEoh01.Name = "rbtEoh01";
            this.rbtEoh01.Size = new System.Drawing.Size(49, 17);
            this.rbtEoh01.TabIndex = 70;
            this.rbtEoh01.TabStop = true;
            this.rbtEoh01.Text = "normality";
            this.rbtEoh01.UseVisualStyleBackColor = true;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(17, 3);
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
            // txtLotType
            // 
            this.txtLotType.Location = new System.Drawing.Point(387, 1);
            this.txtLotType.Name = "txtLotType";
            this.txtLotType.Size = new System.Drawing.Size(112, 21);
            this.txtLotType.TabIndex = 19;
            this.txtLotType.Text = "P%";
            this.txtLotType.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(377, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "LOT TYPE :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbTime02);
            this.groupBox3.Controls.Add(this.rbTime03);
            this.groupBox3.Controls.Add(this.rbTime01);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(337, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(247, 30);
            this.groupBox3.TabIndex = 72;
            this.groupBox3.TabStop = false;
            // 
            // rbTime02
            // 
            this.rbTime02.AutoSize = true;
            this.rbTime02.Checked = true;
            this.rbTime02.Location = new System.Drawing.Point(67, 12);
            this.rbTime02.Name = "rbTime02";
            this.rbTime02.Size = new System.Drawing.Size(49, 17);
            this.rbTime02.TabIndex = 68;
            this.rbTime02.TabStop = true;
            this.rbTime02.Text = "06시";
            this.rbTime02.UseVisualStyleBackColor = true;
            // 
            // rbTime03
            // 
            this.rbTime03.AutoSize = true;
            this.rbTime03.Location = new System.Drawing.Point(122, 12);
            this.rbTime03.Name = "rbTime03";
            this.rbTime03.Size = new System.Drawing.Size(49, 17);
            this.rbTime03.TabIndex = 67;
            this.rbTime03.Text = "14시";
            this.rbTime03.UseVisualStyleBackColor = true;
            // 
            // rbTime01
            // 
            this.rbTime01.AutoSize = true;
            this.rbTime01.Location = new System.Drawing.Point(181, 12);
            this.rbTime01.Name = "rbTime01";
            this.rbTime01.Size = new System.Drawing.Size(49, 17);
            this.rbTime01.TabIndex = 67;
            this.rbTime01.Text = "22시";
            this.rbTime01.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 66;
            this.label6.Text = "standard Time :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cdvMatType
            // 
            this.cdvMatType.BackColor = System.Drawing.Color.Transparent;
            this.cdvMatType.bMultiSelect = false;
            this.cdvMatType.ConditionText = "MAT TYPE";
            this.cdvMatType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvMatType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatType.Location = new System.Drawing.Point(14, 2);
            this.cdvMatType.MandatoryFlag = false;
            this.cdvMatType.Name = "cdvMatType";
            this.cdvMatType.ParentValue = "";
            this.cdvMatType.sCodeColumnName = "KEY_1";
            this.cdvMatType.sDynamicQuery = "";            
            this.cdvMatType.Size = new System.Drawing.Size(180, 21);
            this.cdvMatType.sTableName = "";
            this.cdvMatType.sValueColumnName = "DATA_1";
            this.cdvMatType.TabIndex = 72;
            this.cdvMatType.VisibleValueButton = true;
            this.cdvMatType.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvMatType_ValueSelectedItemChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 67;
            this.label3.Text = "Settlement Period:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // udcMat_Code
            // 
            this.udcMat_Code.BackColor = System.Drawing.Color.Transparent;
            this.udcMat_Code.bMultiSelect = false;
            this.udcMat_Code.ConditionText = "MAT CODE";
            this.udcMat_Code.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcMat_Code.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcMat_Code.Location = new System.Drawing.Point(227, 2);
            this.udcMat_Code.MandatoryFlag = false;
            this.udcMat_Code.Name = "udcMat_Code";
            this.udcMat_Code.ParentValue = "";
            this.udcMat_Code.sCodeColumnName = "MATCODE";
            this.udcMat_Code.sDynamicQuery = "";            
            this.udcMat_Code.Size = new System.Drawing.Size(305, 21);
            this.udcMat_Code.sTableName = "";
            this.udcMat_Code.sValueColumnName = "RESV_FIELD_2";
            this.udcMat_Code.TabIndex = 72;
            this.udcMat_Code.VisibleValueButton = true;
            this.udcMat_Code.ValueButtonPress += new System.EventHandler(this.udcMat_Code_ValueButtonPress);
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Controls.Add(this.label1);
            this.cdvFromToDate.Location = new System.Drawing.Point(82, 3);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(232, 21);
            this.cdvFromToDate.TabIndex = 73;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Period";
            // 
            // ckbHist_Del
            // 
            this.ckbHist_Del.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbHist_Del.AutoSize = true;
            this.ckbHist_Del.Location = new System.Drawing.Point(706, 4);
            this.ckbHist_Del.Name = "ckbHist_Del";
            this.ckbHist_Del.Size = new System.Drawing.Size(89, 17);
            this.ckbHist_Del.TabIndex = 73;
            this.ckbHist_Del.Text = "Hist_Del_Qty";
            this.ckbHist_Del.UseVisualStyleBackColor = true;
            this.ckbHist_Del.Visible = false;
            // 
            // ckbPrd_View
            // 
            this.ckbPrd_View.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbPrd_View.AutoSize = true;
            this.ckbPrd_View.Location = new System.Drawing.Point(706, 5);
            this.ckbPrd_View.Name = "ckbPrd_View";
            this.ckbPrd_View.Size = new System.Drawing.Size(63, 17);
            this.ckbPrd_View.TabIndex = 73;
            this.ckbPrd_View.Text = "Product";
            this.ckbPrd_View.UseVisualStyleBackColor = true;
            this.ckbPrd_View.Visible = false;
            // 
            // ChkLot
            // 
            this.ChkLot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChkLot.AutoSize = true;
            this.ChkLot.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ChkLot.Location = new System.Drawing.Point(669, 5);
            this.ChkLot.Name = "ChkLot";
            this.ChkLot.Size = new System.Drawing.Size(124, 17);
            this.ChkLot.TabIndex = 17;
            this.ChkLot.Text = "BOH / EOH By LOT";
            this.ChkLot.UseVisualStyleBackColor = true;
            // 
            // ChkEmc
            // 
            this.ChkEmc.AutoSize = true;
            this.ChkEmc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.ChkEmc.Location = new System.Drawing.Point(538, 4);
            this.ChkEmc.Name = "ChkEmc";
            this.ChkEmc.Size = new System.Drawing.Size(120, 17);
            this.ChkEmc.TabIndex = 17;
            this.ChkEmc.Text = "Including EMC restocking";
            this.ChkEmc.UseVisualStyleBackColor = true;
            this.ChkEmc.Visible = false;
            // 
            // MAT070301
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "MAT070301";
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.cdvFromToDate.ResumeLayout(false);
            this.cdvFromToDate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbtEoh02;
        private System.Windows.Forms.RadioButton rbtEoh01;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.TextBox txtLotType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbTime02;
        private System.Windows.Forms.RadioButton rbTime01;
        private System.Windows.Forms.Label label6;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvMatType;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcMat_Code;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckbHist_Del;
        private System.Windows.Forms.CheckBox ckbPrd_View;
        private System.Windows.Forms.RadioButton rbTime03;
        private System.Windows.Forms.CheckBox ChkLot;
        private System.Windows.Forms.CheckBox ChkEmc;
    }
}
