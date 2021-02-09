namespace Hana.REG
{
    partial class REG080101
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
			this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
			this.cdvPlanVersion = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
			this.cdvPlanWeek = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
			this.SS01 = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
			this.SS01_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtMatID = new System.Windows.Forms.TextBox();
			this.lblMatID = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)(this.SS01)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// lblTitle
			// 
			this.lblTitle.Size = new System.Drawing.Size(64, 13);
			this.lblTitle.Text = "PLAN 수정";
			// 
			// pnlMiddle
			// 
			this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
			// 
			// pnlCondition2
			// 
			this.pnlCondition2.Controls.Add(this.lblMatID);
			this.pnlCondition2.Controls.Add(this.txtMatID);
			// 
			// pnlCondition1
			// 
			this.pnlCondition1.Controls.Add(this.btnSave);
			this.pnlCondition1.Controls.Add(this.cdvPlanWeek);
			this.pnlCondition1.Controls.Add(this.cdvPlanVersion);
			this.pnlCondition1.Controls.Add(this.cdvFactory);
			// 
			// pnlWIPDetail
			// 
			this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
			// 
			// pnlMain
			// 
			this.pnlMain.Controls.Add(this.SS01);
			this.pnlMain.Location = new System.Drawing.Point(0, 179);
			this.pnlMain.Size = new System.Drawing.Size(800, 421);
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
			this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
			this.btnSort.Visible = false;
			// 
			// pnlRASDetail
			// 
			this.pnlRASDetail.Location = new System.Drawing.Point(0, 68);
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
			this.cdvFactory.Location = new System.Drawing.Point(4, 2);
			this.cdvFactory.MandatoryFlag = true;
			this.cdvFactory.Name = "cdvFactory";
			this.cdvFactory.ParentValue = "";
			this.cdvFactory.sCodeColumnName = "";
			this.cdvFactory.sDynamicQuery = "";
			this.cdvFactory.sFactory = "";
			this.cdvFactory.Size = new System.Drawing.Size(180, 21);
			this.cdvFactory.sTableName = "";
			this.cdvFactory.sValueColumnName = "";
			this.cdvFactory.TabIndex = 58;
			this.cdvFactory.VisibleValueButton = true;
			// 
			// cdvPlanVersion
			// 
			this.cdvPlanVersion.BackColor = System.Drawing.Color.Transparent;
			this.cdvPlanVersion.bMultiSelect = true;
			this.cdvPlanVersion.ConditionText = "Version";
			this.cdvPlanVersion.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
			this.cdvPlanVersion.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cdvPlanVersion.Location = new System.Drawing.Point(404, 2);
			this.cdvPlanVersion.MandatoryFlag = false;
			this.cdvPlanVersion.Name = "cdvPlanVersion";
			this.cdvPlanVersion.ParentValue = "";
			this.cdvPlanVersion.sCodeColumnName = "VERSION";
			this.cdvPlanVersion.sDynamicQuery = "";			
			this.cdvPlanVersion.Size = new System.Drawing.Size(180, 21);
			this.cdvPlanVersion.sTableName = "CWIPPLNWEK_N";
			this.cdvPlanVersion.sValueColumnName = "DATA";
			this.cdvPlanVersion.TabIndex = 59;
			this.cdvPlanVersion.VisibleValueButton = true;
			this.cdvPlanVersion.ValueButtonPress += new System.EventHandler(this.cdvPlanVersion_ValueButtonPress);
			// 
			// cdvPlanWeek
			// 
			this.cdvPlanWeek.BackColor = System.Drawing.Color.Transparent;
			this.cdvPlanWeek.bMultiSelect = true;
			this.cdvPlanWeek.ConditionText = "Plan Week";
			this.cdvPlanWeek.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
			this.cdvPlanWeek.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.cdvPlanWeek.Location = new System.Drawing.Point(204, 2);
			this.cdvPlanWeek.MandatoryFlag = false;
			this.cdvPlanWeek.Name = "cdvPlanWeek";
			this.cdvPlanWeek.ParentValue = "";
			this.cdvPlanWeek.sCodeColumnName = "PLAN_WEEK";
			this.cdvPlanWeek.sDynamicQuery = "";			
			this.cdvPlanWeek.Size = new System.Drawing.Size(180, 21);
			this.cdvPlanWeek.sTableName = "CWIPPLNWEK_N";
			this.cdvPlanWeek.sValueColumnName = "DATA";
			this.cdvPlanWeek.TabIndex = 60;
			this.cdvPlanWeek.VisibleValueButton = true;
			this.cdvPlanWeek.ValueButtonPress += new System.EventHandler(this.cdvPlanWeek_ValueButtonPress);
			// 
			// SS01
			// 
			this.SS01.About = "4.0.2001.2005";
			this.SS01.AccessibleDescription = "SS01, Sheet1, Row 0, Column 0, ";
			this.SS01.BackColor = System.Drawing.Color.White;
			this.SS01.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SS01.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.SS01.Location = new System.Drawing.Point(3, 3);
			this.SS01.Name = "SS01";
			this.SS01.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.SS01.RPT_IsPreCellsType = true;
			this.SS01.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
			this.SS01.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS01_Sheet1});
			this.SS01.Size = new System.Drawing.Size(794, 418);
			this.SS01.TabIndex = 2;
			this.SS01.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
			this.SS01.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.SS01_EditChange);
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
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
			this.btnSave.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSave.Location = new System.Drawing.Point(696, 1);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(100, 23);
			this.btnSave.TabIndex = 61;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtMatID
			// 
			this.txtMatID.Location = new System.Drawing.Point(84, 1);
			this.txtMatID.Name = "txtMatID";
			this.txtMatID.Size = new System.Drawing.Size(300, 21);
			this.txtMatID.TabIndex = 16;
			this.txtMatID.Text = "%";
			// 
			// lblMatID
			// 
			this.lblMatID.AutoSize = true;
			this.lblMatID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblMatID.Location = new System.Drawing.Point(18, 5);
			this.lblMatID.Name = "lblMatID";
			this.lblMatID.Size = new System.Drawing.Size(48, 13);
			this.lblMatID.TabIndex = 17;
			this.lblMatID.Text = "MAT ID";
			this.lblMatID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// REG080101
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
			this.ConditionCount = 2;
			this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
			this.FormStyle.FormName = null;
			this.Name = "REG080101";
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
			((System.ComponentModel.ISupportInitialize)(this.SS01)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.SS01_Sheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvPlanVersion;
		private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvPlanWeek;
		private Miracom.SmartWeb.UI.Controls.udcFarPoint SS01;
		private FarPoint.Win.Spread.SheetView SS01_Sheet1;
		protected System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtMatID;
		private System.Windows.Forms.Label lblMatID;

	}
}
