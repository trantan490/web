namespace Hana.YLD
{
    partial class YLD040501
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YLD040501));
            this.udcDurationDate1 = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.cboPcs = new System.Windows.Forms.ComboBox();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvLossCode = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.menu_select = new System.Windows.Forms.ComboBox();
            this.lbl_select = new System.Windows.Forms.Label();
            this.cdvDepart = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.label1 = new System.Windows.Forms.Label();
            this.lblLot = new System.Windows.Forms.Label();
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
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(75, 13);
            this.lblTitle.Text = "Defect Code";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.lblLot);
            this.pnlCondition2.Controls.Add(this.cdvDepart);
            this.pnlCondition2.Controls.Add(this.lbl_select);
            this.pnlCondition2.Controls.Add(this.menu_select);
            this.pnlCondition2.Controls.Add(this.cdvLossCode);
            this.pnlCondition2.Controls.Add(this.cboPcs);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.cdvOper);
            this.pnlCondition1.Controls.Add(this.udcDurationDate1);
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
            this.pnlMain.Controls.Add(this.splitContainer1);
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
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
            // 
            // udcDurationDate1
            // 
            this.udcDurationDate1.BackColor = System.Drawing.Color.Transparent;
            this.udcDurationDate1.Location = new System.Drawing.Point(498, 2);
            this.udcDurationDate1.Name = "udcDurationDate1";
            this.udcDurationDate1.RestrictedDayCount = 60;
            this.udcDurationDate1.Size = new System.Drawing.Size(280, 21);
            this.udcDurationDate1.TabIndex = 18;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.spdData);
            this.splitContainer1.Size = new System.Drawing.Size(794, 418);
            this.splitContainer1.SplitterDistance = 225;
            this.splitContainer1.TabIndex = 0;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(0, 0);
            this.spdData.Name = "spdData";
            this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 189);
            this.spdData.TabIndex = 2;
            this.spdData.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spdData_CellDoubleClick);
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
            // cboPcs
            // 
            this.cboPcs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPcs.FormattingEnabled = true;
            this.cboPcs.Items.AddRange(new object[] {
            "PPM",
            "EA",
            "MPPM"});
            this.cboPcs.Location = new System.Drawing.Point(270, 2);
            this.cboPcs.Name = "cboPcs";
            this.cboPcs.Size = new System.Drawing.Size(97, 21);
            this.cboPcs.TabIndex = 10;
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition.eFromToType.OPER;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(190, 1);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(290, 21);
            this.cdvOper.sTableName = "";
            this.cdvOper.TabIndex = 19;
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
            this.cdvFactory.TabIndex = 20;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvLossCode
            // 
            this.cdvLossCode.BackColor = System.Drawing.Color.Transparent;
            this.cdvLossCode.bMultiSelect = true;
            this.cdvLossCode.ConditionText = "Loss Code";
            this.cdvLossCode.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvLossCode.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLossCode.Location = new System.Drawing.Point(4, 2);
            this.cdvLossCode.MandatoryFlag = false;
            this.cdvLossCode.Name = "cdvLossCode";
            this.cdvLossCode.ParentValue = "";
            this.cdvLossCode.RefFactory = this.cdvFactory;
            this.cdvLossCode.sCodeColumnName = "";
            this.cdvLossCode.sDynamicQuery = "";
            this.cdvLossCode.sFactory = "";
            this.cdvLossCode.Size = new System.Drawing.Size(180, 21);
            this.cdvLossCode.sTableName = "LOSS_CODE";
            this.cdvLossCode.sValueColumnName = "";
            this.cdvLossCode.TabIndex = 11;
            this.cdvLossCode.VisibleValueButton = true;
            // 
            // menu_select
            // 
            this.menu_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menu_select.FormattingEnabled = true;
            this.menu_select.Items.AddRange(new object[] {
            "By customer",
            "By team",
            "By operation"});
            this.menu_select.Location = new System.Drawing.Point(631, 2);
            this.menu_select.Name = "menu_select";
            this.menu_select.Size = new System.Drawing.Size(103, 21);
            this.menu_select.TabIndex = 12;
            this.menu_select.SelectedIndexChanged += new System.EventHandler(this.menu_select_SelectedIndexChanged);
            // 
            // lbl_select
            // 
            this.lbl_select.AutoSize = true;
            this.lbl_select.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_select.Location = new System.Drawing.Point(554, 6);
            this.lbl_select.Name = "lbl_select";
            this.lbl_select.Size = new System.Drawing.Size(73, 13);
            this.lbl_select.TabIndex = 13;
            this.lbl_select.Text = "View Option";
            // 
            // cdvDepart
            // 
            this.cdvDepart.BackColor = System.Drawing.Color.Transparent;
            this.cdvDepart.bMultiSelect = true;
            this.cdvDepart.ConditionText = "Depart";
            this.cdvDepart.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvDepart.Enabled = false;
            this.cdvDepart.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvDepart.Location = new System.Drawing.Point(370, 2);
            this.cdvDepart.MandatoryFlag = false;
            this.cdvDepart.Name = "cdvDepart";
            this.cdvDepart.ParentValue = "";
            this.cdvDepart.RefFactory = this.cdvFactory;
            this.cdvDepart.sCodeColumnName = "";
            this.cdvDepart.sDynamicQuery = "";
            this.cdvDepart.sFactory = "";
            this.cdvDepart.Size = new System.Drawing.Size(180, 21);
            this.cdvDepart.sTableName = "H_DEPARTMENT";
            this.cdvDepart.sValueColumnName = "";
            this.cdvDepart.TabIndex = 14;
            this.cdvDepart.VisibleValueButton = true;
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(194, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 17;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLot
            // 
            this.lblLot.AutoSize = true;
            this.lblLot.Location = new System.Drawing.Point(206, 5);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(26, 13);
            this.lblLot.TabIndex = 16;
            this.lblLot.Text = "LOT";
            // 
            // YLD040501
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "YLD040501";
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
            this.pnlDetailCondition3.ResumeLayout(false);
            this.pnlBUMPDetail.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcDurationDate1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.ComboBox cboPcs;
        private Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition cdvOper;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLossCode;
        private System.Windows.Forms.ComboBox menu_select;
        private System.Windows.Forms.Label lbl_select;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvDepart;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblLot;        
        
    }
}
