namespace Hana.PQC
{
    partial class PQC030202
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PQC030202));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvRasId = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.cdvCharID = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.rdbDay = new System.Windows.Forms.RadioButton();
            this.rdbWeek = new System.Windows.Forms.RadioButton();
            this.rdbMonth = new System.Windows.Forms.RadioButton();
            this.rdbQuarter = new System.Windows.Forms.RadioButton();
            this.cdvOperID = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvSpecID = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(86, 13);
            this.lblTitle.Text = "View CPK Results";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvSpecID);
            this.pnlCondition2.Controls.Add(this.cdvOperID);
            this.pnlCondition2.Controls.Add(this.cdvRasId);
            this.pnlCondition2.Controls.Add(this.cdvCharID);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.label2);
            this.pnlCondition1.Controls.Add(this.rdbQuarter);
            this.pnlCondition1.Controls.Add(this.rdbMonth);
            this.pnlCondition1.Controls.Add(this.rdbWeek);
            this.pnlCondition1.Controls.Add(this.rdbDay);
            this.pnlCondition1.Controls.Add(this.udcFromToDate);
            this.pnlCondition1.Controls.Add(this.lblIcon);
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
            this.udcWIPCondition5.Enabled = false;
            this.udcWIPCondition5.RefFactory = this.cdvFactory;
            // 
            // pnlDetailCondition1
            // 
            this.pnlDetailCondition1.AutoSize = true;
            // 
            // udcWIPCondition4
            // 
            this.udcWIPCondition4.Enabled = false;
            this.udcWIPCondition4.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition3
            // 
            this.udcWIPCondition3.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition2
            // 
            this.udcWIPCondition2.Enabled = false;
            this.udcWIPCondition2.RefFactory = this.cdvFactory;
            // 
            // udcWIPCondition1
            // 
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
            this.udcWIPCondition9.Enabled = false;
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
            // 
            // udcBUMPCondition9
            // 
            this.udcBUMPCondition9.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition1
            // 
            this.udcBUMPCondition1.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition2
            // 
            this.udcBUMPCondition2.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition3
            // 
            this.udcBUMPCondition3.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition4
            // 
            this.udcBUMPCondition4.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition5
            // 
            this.udcBUMPCondition5.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition6
            // 
            this.udcBUMPCondition6.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition7
            // 
            this.udcBUMPCondition7.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition8
            // 
            this.udcBUMPCondition8.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition12
            // 
            this.udcBUMPCondition12.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition11
            // 
            this.udcBUMPCondition11.RefFactory = this.cdvFactory;
            // 
            // udcBUMPCondition10
            // 
            this.udcBUMPCondition10.RefFactory = this.cdvFactory;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY_ASSEMBLY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition1;
            this.cdvFactory.ForcInitControl3 = this.cdvRasId;
            this.cdvFactory.Location = new System.Drawing.Point(4, 0);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 57;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvRasId
            // 
            this.cdvRasId.BackColor = System.Drawing.Color.Transparent;
            this.cdvRasId.bMultiSelect = true;
            this.cdvRasId.ConditionText = "Resource";
            this.cdvRasId.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.RESOURCE;
            this.cdvRasId.ControlRef = true;
            this.cdvRasId.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvRasId.Location = new System.Drawing.Point(204, 2);
            this.cdvRasId.MandatoryFlag = false;
            this.cdvRasId.Name = "cdvRasId";
            this.cdvRasId.ParentValue = "";
            this.cdvRasId.RefFactory = this.cdvFactory;
            this.cdvRasId.sCodeColumnName = "";
            this.cdvRasId.sDynamicQuery = "";
            this.cdvRasId.sFactory = "";
            this.cdvRasId.Size = new System.Drawing.Size(180, 21);
            this.cdvRasId.sTableName = "";
            this.cdvRasId.sValueColumnName = "";
            this.cdvRasId.TabIndex = 72;
            this.cdvRasId.VisibleValueButton = true;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(208, 3);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 69;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 68;
            this.label2.Text = "Date";
            // 
            // udcFromToDate
            // 
            this.udcFromToDate.BackColor = System.Drawing.Color.Transparent;
            this.udcFromToDate.Location = new System.Drawing.Point(238, 1);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(275, 21);
            this.udcFromToDate.TabIndex = 71;
            // 
            // cdvCharID
            // 
            this.cdvCharID.BackColor = System.Drawing.Color.Transparent;
            this.cdvCharID.bMultiSelect = true;
            this.cdvCharID.ConditionText = "Character id";
            this.cdvCharID.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvCharID.ControlRef = true;
            this.cdvCharID.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvCharID.Location = new System.Drawing.Point(4, 2);
            this.cdvCharID.MandatoryFlag = false;
            this.cdvCharID.Name = "cdvCharID";
            this.cdvCharID.ParentValue = "";
            this.cdvCharID.sCodeColumnName = "Code";
            this.cdvCharID.sDynamicQuery = "";
            this.cdvCharID.sFactory = "";
            this.cdvCharID.Size = new System.Drawing.Size(180, 21);
            this.cdvCharID.sTableName = "";
            this.cdvCharID.sValueColumnName = "data";
            this.cdvCharID.TabIndex = 71;
            this.cdvCharID.VisibleValueButton = true;
            this.cdvCharID.ValueButtonPress += new System.EventHandler(this.cdvCharID_ValueButtonPress);
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
            this.spdData.TabIndex = 2;
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
            // rdbDay
            // 
            this.rdbDay.AutoSize = true;
            this.rdbDay.Checked = true;
            this.rdbDay.Location = new System.Drawing.Point(493, 3);
            this.rdbDay.Name = "rdbDay";
            this.rdbDay.Size = new System.Drawing.Size(37, 17);
            this.rdbDay.TabIndex = 72;
            this.rdbDay.TabStop = true;
            this.rdbDay.Text = "일";
            this.rdbDay.UseVisualStyleBackColor = true;
            // 
            // rdbWeek
            // 
            this.rdbWeek.AutoSize = true;
            this.rdbWeek.Location = new System.Drawing.Point(536, 3);
            this.rdbWeek.Name = "rdbWeek";
            this.rdbWeek.Size = new System.Drawing.Size(37, 17);
            this.rdbWeek.TabIndex = 73;
            this.rdbWeek.Text = "Week";
            this.rdbWeek.UseVisualStyleBackColor = true;
            // 
            // rdbMonth
            // 
            this.rdbMonth.AutoSize = true;
            this.rdbMonth.Location = new System.Drawing.Point(579, 3);
            this.rdbMonth.Name = "rdbMonth";
            this.rdbMonth.Size = new System.Drawing.Size(37, 17);
            this.rdbMonth.TabIndex = 74;
            this.rdbMonth.Text = "월";
            this.rdbMonth.UseVisualStyleBackColor = true;
            // 
            // rdbQuarter
            // 
            this.rdbQuarter.AutoSize = true;
            this.rdbQuarter.Location = new System.Drawing.Point(622, 3);
            this.rdbQuarter.Name = "rdbQuarter";
            this.rdbQuarter.Size = new System.Drawing.Size(49, 17);
            this.rdbQuarter.TabIndex = 75;
            this.rdbQuarter.Text = "quarter";
            this.rdbQuarter.UseVisualStyleBackColor = true;
            // 
            // cdvOperID
            // 
            this.cdvOperID.BackColor = System.Drawing.Color.Transparent;
            this.cdvOperID.bMultiSelect = true;
            this.cdvOperID.ConditionText = "Operation";
            this.cdvOperID.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.cdvOperID.ControlRef = true;
            this.cdvOperID.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOperID.ForcInitControl1 = this.cdvSpecID;
            this.cdvOperID.Location = new System.Drawing.Point(4, 2);
            this.cdvOperID.MandatoryFlag = false;
            this.cdvOperID.Name = "cdvOperID";
            this.cdvOperID.ParentValue = "";
            this.cdvOperID.RefFactory = this.cdvFactory;
            this.cdvOperID.sCodeColumnName = "";
            this.cdvOperID.sDynamicQuery = "";
            this.cdvOperID.sFactory = "";
            this.cdvOperID.Size = new System.Drawing.Size(180, 21);
            this.cdvOperID.sTableName = "";
            this.cdvOperID.sValueColumnName = "";
            this.cdvOperID.TabIndex = 73;
            this.cdvOperID.VisibleValueButton = true;
            // 
            // cdvSpecID
            // 
            this.cdvSpecID.BackColor = System.Drawing.Color.Transparent;
            this.cdvSpecID.bMultiSelect = true;
            this.cdvSpecID.ConditionText = "Spec ID";
            this.cdvSpecID.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvSpecID.ControlRef = true;
            this.cdvSpecID.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvSpecID.Location = new System.Drawing.Point(204, 2);
            this.cdvSpecID.MandatoryFlag = false;
            this.cdvSpecID.Name = "cdvSpecID";
            this.cdvSpecID.ParentValue = "";
            this.cdvSpecID.sCodeColumnName = "Code";
            this.cdvSpecID.sDynamicQuery = "";
            this.cdvSpecID.sFactory = "";
            this.cdvSpecID.Size = new System.Drawing.Size(180, 21);
            this.cdvSpecID.sTableName = "";
            this.cdvSpecID.sValueColumnName = "data";
            this.cdvSpecID.TabIndex = 74;
            this.cdvSpecID.VisibleValueButton = true;
            this.cdvSpecID.ValueButtonPress += new System.EventHandler(this.cdvSpecID_ValueButtonPress);
            // 
            // PQC030202
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC030202";
            this.Load += new System.EventHandler(this.PQC030202_Load);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlWIPDetail.PerformLayout();
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
                
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvCharID;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvRasId;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.RadioButton rdbDay;
        private System.Windows.Forms.RadioButton rdbQuarter;
        private System.Windows.Forms.RadioButton rdbMonth;
        private System.Windows.Forms.RadioButton rdbWeek;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvSpecID;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOperID;
    }
}
