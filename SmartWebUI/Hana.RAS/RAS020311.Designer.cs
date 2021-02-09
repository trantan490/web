namespace Hana.RAS
{
    partial class RAS020311
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAS020311));
            this.lblIcon = new System.Windows.Forms.Label();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDatePlus();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvStep = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvRes_ID = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvAlarm_ID = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvModel = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.lblTitle.Size = new System.Drawing.Size(120, 13);
            this.lblTitle.Text = "Action Time for momentary stop";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.udcFromToDate);
            this.pnlCondition2.Controls.Add(this.cdvAlarm_ID);
            this.pnlCondition2.Controls.Add(this.cdvStep);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Controls.Add(this.cdvFactory);
            this.pnlCondition2.Controls.Add(this.label2);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvModel);
            this.pnlCondition1.Controls.Add(this.cdvRes_ID);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
            this.pnlWIPDetail.Size = new System.Drawing.Size(800, 508);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 92);
            this.pnlMain.Size = new System.Drawing.Size(800, 508);
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
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 92);
            this.pnlRASDetail.Size = new System.Drawing.Size(800, 508);
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(23, 7);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 47;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.spdData.Size = new System.Drawing.Size(794, 505);
            this.spdData.TabIndex = 26;
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
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(76, 3);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(499, 21);
            this.udcFromToDate.TabIndex = 48;
            this.udcFromToDate.yesterday_flag = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Period";
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
            this.cdvFactory.Location = new System.Drawing.Point(615, 3);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 87;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvStep
            // 
            this.cdvStep.BackColor = System.Drawing.Color.Transparent;
            this.cdvStep.bMultiSelect = false;
            this.cdvStep.ConditionText = "Operation";
            this.cdvStep.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.cdvStep.ControlRef = true;
            this.cdvStep.Enabled = false;
            this.cdvStep.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvStep.Location = new System.Drawing.Point(574, 3);
            this.cdvStep.MandatoryFlag = false;
            this.cdvStep.Name = "cdvStep";
            this.cdvStep.ParentValue = "";
            this.cdvStep.RefFactory = this.cdvFactory;
            this.cdvStep.sCodeColumnName = "";
            this.cdvStep.sDynamicQuery = "";
            this.cdvStep.sFactory = "";
            this.cdvStep.Size = new System.Drawing.Size(180, 21);
            this.cdvStep.sTableName = "";
            this.cdvStep.sValueColumnName = "";
            this.cdvStep.TabIndex = 44;
            this.cdvStep.Visible = false;
            this.cdvStep.VisibleValueButton = true;
            this.cdvStep.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvStep_ValueSelectedItemChanged);
            // 
            // cdvRes_ID
            // 
            this.cdvRes_ID.BackColor = System.Drawing.Color.Transparent;
            this.cdvRes_ID.bMultiSelect = true;
            this.cdvRes_ID.ConditionText = "RES_ID";
            this.cdvRes_ID.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvRes_ID.ControlRef = true;
            this.cdvRes_ID.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvRes_ID.Location = new System.Drawing.Point(232, 0);
            this.cdvRes_ID.MandatoryFlag = false;
            this.cdvRes_ID.Name = "cdvRes_ID";
            this.cdvRes_ID.ParentValue = "";
            this.cdvRes_ID.sCodeColumnName = "RES_ID";
            this.cdvRes_ID.sDynamicQuery = "";
            this.cdvRes_ID.sFactory = "";
            this.cdvRes_ID.Size = new System.Drawing.Size(180, 21);
            this.cdvRes_ID.sTableName = "";
            this.cdvRes_ID.sValueColumnName = "RES_GRP_5";
            this.cdvRes_ID.TabIndex = 73;
            this.cdvRes_ID.VisibleValueButton = true;
            this.cdvRes_ID.ValueButtonPress += new System.EventHandler(this.cdvRes_ID_ValueButtonPress);
            // 
            // cdvAlarm_ID
            // 
            this.cdvAlarm_ID.BackColor = System.Drawing.Color.Transparent;
            this.cdvAlarm_ID.bMultiSelect = false;
            this.cdvAlarm_ID.ConditionText = "ALARM_ID";
            this.cdvAlarm_ID.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvAlarm_ID.ControlRef = true;
            this.cdvAlarm_ID.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvAlarm_ID.Location = new System.Drawing.Point(563, 6);
            this.cdvAlarm_ID.MandatoryFlag = false;
            this.cdvAlarm_ID.Name = "cdvAlarm_ID";
            this.cdvAlarm_ID.ParentValue = "";
            this.cdvAlarm_ID.sCodeColumnName = "KEY_2";
            this.cdvAlarm_ID.sDynamicQuery = "";
            this.cdvAlarm_ID.sFactory = "";
            this.cdvAlarm_ID.Size = new System.Drawing.Size(180, 21);
            this.cdvAlarm_ID.sTableName = "";
            this.cdvAlarm_ID.sValueColumnName = "DATA_1";
            this.cdvAlarm_ID.TabIndex = 73;
            this.cdvAlarm_ID.Visible = false;
            this.cdvAlarm_ID.VisibleValueButton = true;
            // 
            // cdvModel
            // 
            this.cdvModel.BackColor = System.Drawing.Color.Transparent;
            this.cdvModel.bMultiSelect = true;
            this.cdvModel.ConditionText = "MODEL";
            this.cdvModel.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvModel.ControlRef = true;
            this.cdvModel.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvModel.Location = new System.Drawing.Point(20, 0);
            this.cdvModel.MandatoryFlag = false;
            this.cdvModel.Name = "cdvModel";
            this.cdvModel.ParentValue = "";
            this.cdvModel.sCodeColumnName = "RES_GRP_6";
            this.cdvModel.sDynamicQuery = "";
            this.cdvModel.sFactory = "";
            this.cdvModel.Size = new System.Drawing.Size(180, 21);
            this.cdvModel.sTableName = "";
            this.cdvModel.sValueColumnName = "BB";
            this.cdvModel.TabIndex = 73;
            this.cdvModel.VisibleValueButton = true;
            this.cdvModel.ValueButtonPress += new System.EventHandler(this.cdvModel_ValueButtonPress);
            // 
            // RAS020311
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "RAS020311";
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIcon;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcDurationDatePlus udcFromToDate;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvStep;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvRes_ID;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvAlarm_ID;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvModel;
    }
}
