namespace Hana.PRD
{
    partial class PRD010610
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010610));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvLotType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.ckbKpcs = new System.Windows.Forms.CheckBox();
            this.cdvTimeBase = new System.Windows.Forms.ComboBox();
            this.lblQCType = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
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
            this.lblTitle.Size = new System.Drawing.Size(98, 13);
            this.lblTitle.Text = "Performance Trend";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvTimeBase);
            this.pnlCondition2.Controls.Add(this.lblQCType);
            this.pnlCondition2.Controls.Add(this.cdvLotType);
            this.pnlCondition2.Controls.Add(this.label3);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.ckbKpcs);
            this.pnlCondition1.Controls.Add(this.cdvFromToDate);
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
            this.udcWIPCondition1.bMultiSelect = false;
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
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcWIPCondition1;
            this.cdvFactory.ForcInitControl2 = this.cdvLotType;
            this.cdvFactory.Location = new System.Drawing.Point(5, 1);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(179, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 1;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // cdvLotType
            // 
            this.cdvLotType.BackColor = System.Drawing.Color.Transparent;
            this.cdvLotType.bMultiSelect = true;
            this.cdvLotType.ConditionText = "Lot Type";
            this.cdvLotType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvLotType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLotType.Location = new System.Drawing.Point(5, 2);
            this.cdvLotType.MandatoryFlag = false;
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.ParentValue = "";
            this.cdvLotType.sCodeColumnName = "";
            this.cdvLotType.sDynamicQuery = "";           
            this.cdvLotType.Size = new System.Drawing.Size(180, 21);
            this.cdvLotType.sTableName = "H_TYPE";
            this.cdvLotType.sValueColumnName = "";
            this.cdvLotType.TabIndex = 2;
            this.cdvLotType.VisibleValueButton = true;
            // 
            // cdvFromToDate
            // 
            this.cdvFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.cdvFromToDate.Location = new System.Drawing.Point(214, 1);
            this.cdvFromToDate.Name = "cdvFromToDate";
            this.cdvFromToDate.RestrictedDayCount = 60;
            this.cdvFromToDate.Size = new System.Drawing.Size(275, 21);
            this.cdvFromToDate.TabIndex = 3;
            // 
            // ckbKpcs
            // 
            this.ckbKpcs.AutoSize = true;
            this.ckbKpcs.Location = new System.Drawing.Point(472, 3);
            this.ckbKpcs.Name = "ckbKpcs";
            this.ckbKpcs.Size = new System.Drawing.Size(48, 17);
            this.ckbKpcs.TabIndex = 87;
            this.ckbKpcs.Text = "Kpcs";
            this.ckbKpcs.UseVisualStyleBackColor = true;
            // 
            // cdvTimeBase
            // 
            this.cdvTimeBase.FormattingEnabled = true;
            this.cdvTimeBase.Items.AddRange(new object[] {
            "SE",
            "4HR",
            "6HR",
            "8HR",
            "12HR"});
            this.cdvTimeBase.Location = new System.Drawing.Point(284, 2);
            this.cdvTimeBase.Name = "cdvTimeBase";
            this.cdvTimeBase.Size = new System.Drawing.Size(100, 21);
            this.cdvTimeBase.TabIndex = 84;
            this.cdvTimeBase.Text = "SE";
            // 
            // lblQCType
            // 
            this.lblQCType.AutoSize = true;
            this.lblQCType.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQCType.Location = new System.Drawing.Point(220, 6);
            this.lblQCType.Name = "lblQCType";
            this.lblQCType.Size = new System.Drawing.Size(59, 13);
            this.lblQCType.TabIndex = 83;
            this.lblQCType.Text = "TIME BASE";
            // 
            // label3
            // 
            this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(209, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(6, 17);
            this.label3.TabIndex = 82;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // PRD010610
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010610";
            this.Load += new System.EventHandler(this.PRD010610_Load);
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

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLotType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvFromToDate;
        private System.Windows.Forms.CheckBox ckbKpcs;
        private System.Windows.Forms.ComboBox cdvTimeBase;
        private System.Windows.Forms.Label lblQCType;
        private System.Windows.Forms.Label label3;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
    }
}
