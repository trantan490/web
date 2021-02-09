namespace Hana.PQC
{
    partial class PQC031002
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC031002));
            this.cdvStep = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvOven = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.SS = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtLotID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            ((System.ComponentModel.ISupportInitialize)(this.SS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(110, 13);
            this.lblTitle.Text = "Oven Temp Profile";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 181);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 157);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 133);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 109);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 85);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 61);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label6);
            this.pnlCondition2.Controls.Add(this.label7);
            this.pnlCondition2.Controls.Add(this.txtLotID);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.lblProduct);
            this.pnlCondition2.Controls.Add(this.txtProduct);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 37);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.cdvOven);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
            this.pnlCondition1.Controls.Add(this.cdvStep);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 29);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 152);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.SS);
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Size = new System.Drawing.Size(800, 508);
            // 
            // btnDetail
            // 
            this.btnDetail.Enabled = false;
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDetail.Visible = false;
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
            this.btnSort.Visible = false;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
            // 
            // cdvStep
            // 
            this.cdvStep.BackColor = System.Drawing.Color.Transparent;
            this.cdvStep.bMultiSelect = false;
            this.cdvStep.ConditionText = "Operation";
            this.cdvStep.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvStep.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvStep.ForcInitControl1 = this.cdvOven;
            this.cdvStep.Location = new System.Drawing.Point(200, 4);
            this.cdvStep.MandatoryFlag = false;
            this.cdvStep.Name = "cdvStep";
            this.cdvStep.ParentValue = "";
            this.cdvStep.sCodeColumnName = "OPER_CODE";
            this.cdvStep.sDynamicQuery = "";
            this.cdvStep.sFactory = "";
            this.cdvStep.Size = new System.Drawing.Size(180, 21);
            this.cdvStep.sTableName = "";
            this.cdvStep.sValueColumnName = "OPER_DESC";
            this.cdvStep.TabIndex = 17;
            this.cdvStep.VisibleValueButton = true;
            this.cdvStep.ValueButtonPress += new System.EventHandler(this.cdvStep_ValueButtonPress);
            // 
            // cdvOven
            // 
            this.cdvOven.BackColor = System.Drawing.Color.Transparent;
            this.cdvOven.bMultiSelect = false;
            this.cdvOven.ConditionText = "Oven Code";
            this.cdvOven.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvOven.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOven.Location = new System.Drawing.Point(394, 4);
            this.cdvOven.MandatoryFlag = false;
            this.cdvOven.Name = "cdvOven";
            this.cdvOven.ParentValue = "";
            this.cdvOven.sCodeColumnName = "RES_ID";
            this.cdvOven.sDynamicQuery = "";
            this.cdvOven.sFactory = "";
            this.cdvOven.Size = new System.Drawing.Size(180, 21);
            this.cdvOven.sTableName = "";
            this.cdvOven.sValueColumnName = "RES_DESC";
            this.cdvOven.TabIndex = 66;
            this.cdvOven.VisibleValueButton = true;
            this.cdvOven.ValueButtonPress += new System.EventHandler(this.cdvOven_ValueButtonPress);
            // 
            // SS
            // 
            this.SS.About = "4.0.2001.2005";
            this.SS.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.SS.BackColor = System.Drawing.Color.White;
            this.SS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SS.Location = new System.Drawing.Point(3, 3);
            this.SS.Name = "SS";
            this.SS.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SS.RPT_IsPreCellsType = true;
            this.SS.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.SS.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS_Sheet1});
            this.SS.Size = new System.Drawing.Size(794, 505);
            this.SS.TabIndex = 26;
            this.SS.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.SS_CellClick);
            this.SS.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.SS_CellDoubleClick);
            // 
            // SS_Sheet1
            // 
            this.SS_Sheet1.Reset();
            this.SS_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.SS_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.SS_Sheet1.ColumnCount = 0;
            this.SS_Sheet1.RowCount = 0;
            this.SS_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.SS_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.SS.SetActiveViewport(0, 1, 1);
            // 
            // label6
            // 
            this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(202, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(6, 17);
            this.label6.TabIndex = 58;
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(212, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 57;
            this.label7.Text = "Lot ID";
            // 
            // txtLotID
            // 
            this.txtLotID.Location = new System.Drawing.Point(278, 2);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Size = new System.Drawing.Size(101, 21);
            this.txtLotID.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(8, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(6, 17);
            this.label2.TabIndex = 55;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.Location = new System.Drawing.Point(21, 6);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 54;
            this.lblProduct.Text = "Product";
            // 
            // txtProduct
            // 
            this.txtProduct.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtProduct.Location = new System.Drawing.Point(84, 2);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(102, 21);
            this.txtProduct.TabIndex = 53;
            this.txtProduct.Text = "%";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(601, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 17);
            this.label1.TabIndex = 20;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(612, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Date";
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(678, 5);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(271, 21);
            this.cdvFromToDate.TabIndex = 18;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(6, 5);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 67;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // PQC031002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC031002";
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
            ((System.ComponentModel.ISupportInitialize)(this.SS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS;
        private FarPoint.Win.Spread.SheetView SS_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStep;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtLotID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOven;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
    }
}
