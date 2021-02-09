namespace Hana.PRD
{
    partial class PRD010103
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
            this.txtMatID = new System.Windows.Forms.TextBox();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.txtLotType = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlWIPDetail.SuspendLayout();
            this.pnlDetailCondition2.SuspendLayout();
            this.pnlDetailCondition1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRASDetail.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.pnlDetailCondition3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(109, 13);
            this.lblTitle.Text = "GCT WIP REPORTS";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 42);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 182);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 158);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 134);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 110);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 86);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 62);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Location = new System.Drawing.Point(0, 38);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.ckbKpcs);
            this.pnlCondition1.Controls.Add(this.txtLotType);
            this.pnlCondition1.Controls.Add(this.txtMatID);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.lbl_Product);
            this.pnlCondition1.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 68);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.SS01);
            this.pnlMain.Location = new System.Drawing.Point(0, 155);
            this.pnlMain.Size = new System.Drawing.Size(800, 445);
            // 
            // btnDetail
            // 
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
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnSort.Visible = false;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
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
            this.SS01.Size = new System.Drawing.Size(794, 442);
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
            this.SS01_Sheet1.ColumnHeader.Rows.Get(0).Height = 30F;
            this.SS01_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.SS01_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.SS01.SetActiveViewport(0, 1, 1);
            // 
            // lbl_Product
            // 
            this.lbl_Product.AutoSize = true;
            this.lbl_Product.Location = new System.Drawing.Point(14, 7);
            this.lbl_Product.Name = "lbl_Product";
            this.lbl_Product.Size = new System.Drawing.Size(62, 13);
            this.lbl_Product.TabIndex = 16;
            this.lbl_Product.Text = "PRODUCT :";
            this.lbl_Product.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMatID
            // 
            this.txtMatID.Location = new System.Drawing.Point(75, 4);
            this.txtMatID.Name = "txtMatID";
            this.txtMatID.Size = new System.Drawing.Size(232, 21);
            this.txtMatID.TabIndex = 15;
            this.txtMatID.Text = "%";
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(747, 5);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 17;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            this.ckbKpcs.Visible = false;
            // 
            // txtLotType
            // 
            this.txtLotType.Location = new System.Drawing.Point(401, 4);
            this.txtLotType.Name = "txtLotType";
            this.txtLotType.Size = new System.Drawing.Size(112, 21);
            this.txtLotType.TabIndex = 19;
            this.txtLotType.Text = "%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(340, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "LOT TYPE :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // PRD010103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 1;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010103";
            this.pnlMiddle.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
        private FarPoint.Win.Spread.SheetView SS01_Sheet1;
        private System.Windows.Forms.Label lbl_Product;
        private System.Windows.Forms.TextBox txtMatID;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.TextBox txtLotType;
        private System.Windows.Forms.Label label5;
    }
}
