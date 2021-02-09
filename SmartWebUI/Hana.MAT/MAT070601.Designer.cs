namespace Hana.MAT
{
    partial class MAT070601
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MAT070601));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lbl_Product = new System.Windows.Forms.Label();
            this.txtSearchProduct = new System.Windows.Forms.TextBox();
            this.lblIcon = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.cdvLotType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.lblTitle.Size = new System.Drawing.Size(133, 13);
            this.lblTitle.Text = "Performance Status by Material Operation";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(792, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.ckbKpcs);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.lbl_Product);
            this.pnlCondition2.Controls.Add(this.txtSearchProduct);
            this.pnlCondition2.Controls.Add(this.cdvFromToDate);
            this.pnlCondition2.Size = new System.Drawing.Size(790, 24);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.cdvOper);
            this.pnlCondition1.Controls.Add(this.label4);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Size = new System.Drawing.Size(790, 24);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 152);
            this.pnlWIPDetail.Size = new System.Drawing.Size(800, 94);
            // 
            // pnlDetailCondition2
            // 
            this.pnlDetailCondition2.Size = new System.Drawing.Size(798, 30);
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
            this.pnlMain.Size = new System.Drawing.Size(792, 508);
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
            // pnlDetailCondition3
            // 
            this.pnlDetailCondition3.Location = new System.Drawing.Point(0, 62);
            this.pnlDetailCondition3.Size = new System.Drawing.Size(798, 25);
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
            this.cdvFactory.ForcInitControl2 = this.cdvOper;
            this.cdvFactory.Location = new System.Drawing.Point(4, 3);
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
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvOper.bMultiSelect = true;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(204, 3);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sCodeColumnName = "CODE";
            this.cdvOper.sDynamicQuery = "";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(180, 21);
            this.cdvOper.sTableName = "";
            this.cdvOper.sValueColumnName = "DATA";
            this.cdvOper.TabIndex = 11;
            this.cdvOper.VisibleValueButton = true;
            this.cdvOper.ValueButtonPress += new System.EventHandler(this.cdvOper_ValueButtonPress);
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
            this.spdData.Size = new System.Drawing.Size(786, 505);
            this.spdData.TabIndex = 0;
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
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(19, 7);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(44, 13);
            this.lbl_Product.TabIndex = 13;
            this.lbl_Product.Text = "Product";
            // 
            // txtSearchProduct
            // 
            this.txtSearchProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSearchProduct.Location = new System.Drawing.Point(84, 2);
            this.txtSearchProduct.Name = "txtSearchProduct";
            this.txtSearchProduct.Size = new System.Drawing.Size(100, 21);
            this.txtSearchProduct.TabIndex = 12;
            this.txtSearchProduct.Text = "%";
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(8, 6);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(10, 17);
            this.lblIcon.TabIndex = 14;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(284, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(271, 21);
            this.cdvFromToDate.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(207, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 17);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(218, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Date";
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(739, 4);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 18;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            this.ckbKpcs.Visible = false;
            // 
            // cdvLotType
            // 
            this.cdvLotType.FormattingEnabled = true;
            this.cdvLotType.Items.AddRange(new object[] {
            "ALL",
            "P%",
            "E%"});
            this.cdvLotType.Location = new System.Drawing.Point(494, 3);
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.Size = new System.Drawing.Size(83, 21);
            this.cdvLotType.TabIndex = 21;
            this.cdvLotType.Text = "ALL";
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(416, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 20;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(427, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "LOT TYPE";
            // 
            // MAT070601
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "MAT070601";
            this.Size = new System.Drawing.Size(792, 600);
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
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txtSearchProduct;
        private System.Windows.Forms.Label lblIcon;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.ComboBox cdvLotType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
