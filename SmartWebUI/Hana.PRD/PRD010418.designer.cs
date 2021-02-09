namespace Hana.PRD
{
    partial class PRD010418
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010418));
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtMatID = new System.Windows.Forms.TextBox();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboTime = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSapOrderId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBaseMatId = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
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
            this.lblTitle.Size = new System.Drawing.Size(149, 13);
            this.lblTitle.Text = "Production Performance by SAP Manufacturing Order";
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
            this.pnlCondition2.Controls.Add(this.label10);
            this.pnlCondition2.Controls.Add(this.label8);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.txtBaseMatId);
            this.pnlCondition2.Controls.Add(this.label9);
            this.pnlCondition2.Controls.Add(this.txtOrderId);
            this.pnlCondition2.Controls.Add(this.label7);
            this.pnlCondition2.Controls.Add(this.txtSapOrderId);
            this.pnlCondition2.Controls.Add(this.label6);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.txtMatID);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 33);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 25);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label11);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cboTime);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.lblDate);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.label3);
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
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 152);
            this.pnlMain.Size = new System.Drawing.Size(800, 448);
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
            this.spdData.AccessibleDescription = "SS01, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(3, 3);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 445);
            this.spdData.TabIndex = 2;
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
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(17, 7);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 13);
            this.lblDate.TabIndex = 68;
            this.lblDate.Text = "standard date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(657, 7);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(52, 13);
            this.lblProduct.TabIndex = 72;
            this.lblProduct.Text = "MAT_ID :";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMatID
            // 
            this.txtMatID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMatID.Location = new System.Drawing.Point(716, 3);
            this.txtMatID.Name = "txtMatID";
            this.txtMatID.Size = new System.Drawing.Size(108, 21);
            this.txtMatID.TabIndex = 71;
            this.txtMatID.Text = "%";
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
            this.cdvFactory.Location = new System.Drawing.Point(604, 1);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(179, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 75;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // label2
            // 
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(649, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(6, 17);
            this.label2.TabIndex = 83;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(8, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 84;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(292, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(6, 17);
            this.label5.TabIndex = 85;
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTime
            // 
            this.cboTime.FormattingEnabled = true;
            this.cboTime.Items.AddRange(new object[] {
            "22 hours",
            "06 hours"});
            this.cboTime.Location = new System.Drawing.Point(365, 1);
            this.cboTime.Name = "cboTime";
            this.cboTime.Size = new System.Drawing.Size(47, 21);
            this.cboTime.TabIndex = 86;
            this.cboTime.Text = "22 hours";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(304, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "standard Time";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(38, 2);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(275, 21);
            this.cdvFromToDate.TabIndex = 88;
            // 
            // label4
            // 
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(8, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(6, 17);
            this.label4.TabIndex = 86;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSapOrderId
            // 
            this.txtSapOrderId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSapOrderId.Location = new System.Drawing.Point(111, 3);
            this.txtSapOrderId.Name = "txtSapOrderId";
            this.txtSapOrderId.Size = new System.Drawing.Size(108, 21);
            this.txtSapOrderId.TabIndex = 84;
            this.txtSapOrderId.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 85;
            this.label6.Text = "SAP_ORDER_ID :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(240, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 85;
            this.label7.Text = "ORDER_ID :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtOrderId
            // 
            this.txtOrderId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtOrderId.Location = new System.Drawing.Point(312, 3);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(108, 21);
            this.txtOrderId.TabIndex = 84;
            this.txtOrderId.Text = "%";
            // 
            // label8
            // 
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Location = new System.Drawing.Point(232, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(6, 17);
            this.label8.TabIndex = 86;
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(441, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 85;
            this.label9.Text = "BASE_MAT_ID :";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtBaseMatId
            // 
            this.txtBaseMatId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBaseMatId.Location = new System.Drawing.Point(530, 3);
            this.txtBaseMatId.Name = "txtBaseMatId";
            this.txtBaseMatId.Size = new System.Drawing.Size(108, 21);
            this.txtBaseMatId.TabIndex = 84;
            this.txtBaseMatId.Text = "%";
            // 
            // label10
            // 
            this.label10.Image = ((System.Drawing.Image)(resources.GetObject("label10.Image")));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label10.Location = new System.Drawing.Point(433, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(6, 17);
            this.label10.TabIndex = 86;
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(436, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(288, 13);
            this.label11.TabIndex = 89;
            this.label11.Text = "※ Click ORDER ID to display the relevant lot.";
            // 
            // PRD010418
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010418";
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
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtMatID;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboTime;
        private System.Windows.Forms.Label label1;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBaseMatId;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtOrderId;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSapOrderId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;        
    }
}
