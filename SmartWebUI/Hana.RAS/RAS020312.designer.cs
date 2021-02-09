namespace Hana.RAS
{
    partial class RAS020312
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAS020312));
            SoftwareFX.ChartFX.TitleDockable titleDockable2 = new SoftwareFX.ChartFX.TitleDockable();
            this.lblToday = new System.Windows.Forms.Label();
            this.lblJindo = new System.Windows.Forms.Label();
            this.lblLastDay = new System.Windows.Forms.Label();
            this.lblRemain = new System.Windows.Forms.Label();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cf01 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cdvRes = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvModel = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvBlock = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.lblTitle.Size = new System.Drawing.Size(143, 13);
            this.lblTitle.Text = "Momentary stop Time Series Graph";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 42);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 179);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 155);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 131);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 107);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 83);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 62);
            this.pnlCondition3.Size = new System.Drawing.Size(798, 21);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.lblRemain);
            this.pnlCondition2.Controls.Add(this.lblLastDay);
            this.pnlCondition2.Controls.Add(this.lblJindo);
            this.pnlCondition2.Controls.Add(this.lblToday);
            this.pnlCondition2.Size = new System.Drawing.Size(798, 30);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvBlock);
            this.pnlCondition1.Controls.Add(this.label1);
            this.pnlCondition1.Controls.Add(this.cdvModel);
            this.pnlCondition1.Controls.Add(this.cdvRes);
            this.pnlCondition1.Controls.Add(this.cdvDate);
            this.pnlCondition1.Controls.Add(this.lblIcon);
            this.pnlCondition1.Controls.Add(this.lblDate);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 128);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.cf01);
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 68);
            this.pnlMain.Size = new System.Drawing.Size(800, 532);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
            // 
            // lblToday
            // 
            this.lblToday.AutoSize = true;
            this.lblToday.Location = new System.Drawing.Point(85, 9);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(0, 13);
            this.lblToday.TabIndex = 6;
            // 
            // lblJindo
            // 
            this.lblJindo.AutoSize = true;
            this.lblJindo.Location = new System.Drawing.Point(647, 6);
            this.lblJindo.Name = "lblJindo";
            this.lblJindo.Size = new System.Drawing.Size(0, 13);
            this.lblJindo.TabIndex = 8;
            // 
            // lblLastDay
            // 
            this.lblLastDay.AutoSize = true;
            this.lblLastDay.Location = new System.Drawing.Point(286, 6);
            this.lblLastDay.Name = "lblLastDay";
            this.lblLastDay.Size = new System.Drawing.Size(0, 13);
            this.lblLastDay.TabIndex = 9;
            // 
            // lblRemain
            // 
            this.lblRemain.AutoSize = true;
            this.lblRemain.Location = new System.Drawing.Point(427, 6);
            this.lblRemain.Name = "lblRemain";
            this.lblRemain.Size = new System.Drawing.Size(0, 13);
            this.lblRemain.TabIndex = 11;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(7, 4);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 18;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(17, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 13);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "standard date";
            // 
            // cf01
            // 
            this.cf01.AxisX.Staggered = true;
            this.cf01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cf01.Location = new System.Drawing.Point(3, 3);
            this.cf01.Name = "cf01";
            this.cf01.Scrollable = true;
            this.cf01.Size = new System.Drawing.Size(794, 529);
            this.cf01.TabIndex = 27;
            this.cf01.Titles.AddRange(new SoftwareFX.ChartFX.TitleDockable[] {
            titleDockable2});
            this.cf01.ToolBar = true;
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(79, 2);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 45;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // cdvRes
            // 
            this.cdvRes.BackColor = System.Drawing.Color.Transparent;
            this.cdvRes.bMultiSelect = true;
            this.cdvRes.ConditionText = "Equipment";
            this.cdvRes.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvRes.ControlRef = true;
            this.cdvRes.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvRes.Location = new System.Drawing.Point(565, 0);
            this.cdvRes.MandatoryFlag = true;
            this.cdvRes.Name = "cdvRes";
            this.cdvRes.ParentValue = "";
            this.cdvRes.RefControl1 = this.cdvModel;
            this.cdvRes.sCodeColumnName = "Code";
            this.cdvRes.sDynamicQuery = "";            
            this.cdvRes.Size = new System.Drawing.Size(180, 21);
            this.cdvRes.sTableName = "";
            this.cdvRes.sValueColumnName = "Data";
            this.cdvRes.TabIndex = 85;
            this.cdvRes.VisibleValueButton = true;
            this.cdvRes.ValueButtonPress += new System.EventHandler(this.cdvRes_ButtonPress);
            // 
            // cdvModel
            // 
            this.cdvModel.BackColor = System.Drawing.Color.Transparent;
            this.cdvModel.bMultiSelect = true;
            this.cdvModel.ConditionText = "Model";
            this.cdvModel.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvModel.ControlRef = true;
            this.cdvModel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvModel.Location = new System.Drawing.Point(191, 0);
            this.cdvModel.MandatoryFlag = false;
            this.cdvModel.Name = "cdvModel";
            this.cdvModel.ParentValue = "";
            this.cdvModel.sCodeColumnName = "Code";
            this.cdvModel.sDynamicQuery = "";            
            this.cdvModel.Size = new System.Drawing.Size(180, 21);
            this.cdvModel.sTableName = "";
            this.cdvModel.sValueColumnName = "Data";
            this.cdvModel.TabIndex = 86;
            this.cdvModel.VisibleValueButton = true;
            this.cdvModel.ValueButtonPress += new System.EventHandler(this.cdvModel_ButtonPress);
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
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 529);
            this.spdData.TabIndex = 28;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(756, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 13);
            this.label1.TabIndex = 87;
            this.label1.Text = "* Up to 30 equipment can be selected";
            // 
            // cdvBlock
            // 
            this.cdvBlock.BackColor = System.Drawing.Color.Transparent;
            this.cdvBlock.bMultiSelect = false;
            this.cdvBlock.ConditionText = "Block";
            this.cdvBlock.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvBlock.ControlRef = true;
            this.cdvBlock.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvBlock.Location = new System.Drawing.Point(379, 0);
            this.cdvBlock.MandatoryFlag = false;
            this.cdvBlock.Name = "cdvBlock";
            this.cdvBlock.ParentValue = "";
            this.cdvBlock.sCodeColumnName = "Code";
            this.cdvBlock.sDynamicQuery = "";            
            this.cdvBlock.Size = new System.Drawing.Size(180, 21);
            this.cdvBlock.sTableName = "";
            this.cdvBlock.sValueColumnName = "Data";
            this.cdvBlock.TabIndex = 88;
            this.cdvBlock.VisibleValueButton = true;
            this.cdvBlock.ValueButtonPress += new System.EventHandler(this.cdvBlock_ButtonPress);
            // 
            // RAS020312
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoSize = true;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.NONE;
            this.ConditionCount = 1;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "RAS020312";
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

        private System.Windows.Forms.Label lblLastDay;
        private System.Windows.Forms.Label lblJindo;
        private System.Windows.Forms.Label lblToday;
        private System.Windows.Forms.Label lblRemain;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblDate;
        private Miracom.SmartWeb.UI.Controls.udcChartFX cf01;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvRes;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvModel;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label label1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvBlock;
    }
}
