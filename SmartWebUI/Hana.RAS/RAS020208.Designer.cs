namespace Hana.RAS
{
    partial class RAS020208
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAS020208));
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.lblIcon = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcFromToDate = new Miracom.SmartWeb.UI.Controls.udcDurationDatePlus();
            this.ckdUse = new System.Windows.Forms.CheckBox();
            this.ckbConv = new System.Windows.Forms.CheckBox();
            this.cdvEquipType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblEquip = new System.Windows.Forms.Label();
            this.cdvCare = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbUPEH = new System.Windows.Forms.CheckBox();
            this.cdvMonth = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
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
            this.lblTitle.Size = new System.Drawing.Size(225, 13);
            this.lblTitle.Text = "Capacity-specific details by equipment";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(1400, 66);
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
            this.pnlCondition2.Controls.Add(this.cdvCare);
            this.pnlCondition2.Controls.Add(this.cdvMonth);
            this.pnlCondition2.Controls.Add(this.udcFromToDate);
            this.pnlCondition2.Controls.Add(this.label1);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.label3);
            this.pnlCondition2.Controls.Add(this.lblIcon);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 37);
            this.pnlCondition2.Size = new System.Drawing.Size(1398, 24);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvEquipType);
            this.pnlCondition1.Controls.Add(this.label11);
            this.pnlCondition1.Controls.Add(this.lblEquip);
            this.pnlCondition1.Controls.Add(this.ckbUPEH);
            this.pnlCondition1.Controls.Add(this.ckbConv);
            this.pnlCondition1.Controls.Add(this.ckdUse);
            this.pnlCondition1.Controls.Add(this.cdvOper);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Size = new System.Drawing.Size(1398, 29);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 152);
            this.pnlWIPDetail.Size = new System.Drawing.Size(1400, 87);
            // 
            // pnlDetailCondition2
            // 
            this.pnlDetailCondition2.Size = new System.Drawing.Size(1398, 24);
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
            // pnlDetailCondition1
            // 
            this.pnlDetailCondition1.Size = new System.Drawing.Size(1398, 24);
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
            this.pnlMain.Location = new System.Drawing.Point(0, 326);
            this.pnlMain.Size = new System.Drawing.Size(1400, 274);
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
            this.pnlRASDetail.Size = new System.Drawing.Size(1400, 60);
            // 
            // panel3
            // 
            this.panel3.Size = new System.Drawing.Size(1398, 24);
            // 
            // udcRASCondition6
            // 
            this.udcRASCondition6.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition5
            // 
            this.udcRASCondition5.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition4
            // 
            this.udcRASCondition4.RefFactory = this.cdvFactory;
            // 
            // panel4
            // 
            this.panel4.Size = new System.Drawing.Size(1398, 24);
            // 
            // udcRASCondition3
            // 
            this.udcRASCondition3.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition2
            // 
            this.udcRASCondition2.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition1
            // 
            this.udcRASCondition1.RefFactory = this.cdvFactory;
            // 
            // udcRASCondition7
            // 
            this.udcRASCondition7.RefFactory = this.cdvFactory;
            // 
            // pnlDetailCondition3
            // 
            this.pnlDetailCondition3.Size = new System.Drawing.Size(1398, 24);
            // 
            // udcWIPCondition9
            // 
            this.udcWIPCondition9.RefFactory = this.cdvFactory;
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 239);
            this.pnlBUMPDetail.Size = new System.Drawing.Size(1400, 87);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(1398, 24);
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
            // panel6
            // 
            this.panel6.Size = new System.Drawing.Size(1398, 24);
            // 
            // panel7
            // 
            this.panel7.Size = new System.Drawing.Size(1398, 24);
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
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcRASCondition1;
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition1;
            this.cdvFactory.ForcInitControl3 = this.cdvOper;
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
            this.cdvFactory.TabIndex = 13;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvOper.bMultiSelect = true;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(204, 3);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sCodeColumnName = "";
            this.cdvOper.sDynamicQuery = "";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(180, 21);
            this.cdvOper.sTableName = "";
            this.cdvOper.sValueColumnName = "";
            this.cdvOper.TabIndex = 17;
            this.cdvOper.VisibleValueButton = true;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(8, 7);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(126, 17);
            this.lblIcon.TabIndex = 47;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Date";
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
            this.spdData.Size = new System.Drawing.Size(1394, 271);
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
            this.udcFromToDate.Location = new System.Drawing.Point(84, 3);
            this.udcFromToDate.Name = "udcFromToDate";
            this.udcFromToDate.RestrictedDayCount = 60;
            this.udcFromToDate.Size = new System.Drawing.Size(464, 21);
            this.udcFromToDate.TabIndex = 48;
            this.udcFromToDate.yesterday_flag = false;
            // 
            // ckdUse
            // 
            this.ckdUse.AutoSize = true;
            this.ckdUse.Checked = true;
            this.ckdUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckdUse.Location = new System.Drawing.Point(413, 6);
            this.ckdUse.Name = "ckdUse";
            this.ckdUse.Size = new System.Drawing.Size(96, 17);
            this.ckdUse.TabIndex = 18;
            this.ckdUse.Text = "USE STATE (Y)";
            this.ckdUse.UseVisualStyleBackColor = true;
            // 
            // ckbConv
            // 
            this.ckbConv.AutoSize = true;
            this.ckbConv.Location = new System.Drawing.Point(515, 6);
            this.ckbConv.Name = "ckbConv";
            this.ckbConv.Size = new System.Drawing.Size(142, 17);
            this.ckbConv.TabIndex = 18;
            this.ckbConv.Text = "Performance conversion";
            this.ckbConv.UseVisualStyleBackColor = true;
            // 
            // cdvEquipType
            // 
            this.cdvEquipType.FormattingEnabled = true;
            this.cdvEquipType.Items.AddRange(new object[] {
            "EQUIP/LINE",
            "Sub equipment",
            "Line Components equipment"});
            this.cdvEquipType.Location = new System.Drawing.Point(682, 3);
            this.cdvEquipType.Name = "cdvEquipType";
            this.cdvEquipType.Size = new System.Drawing.Size(102, 21);
            this.cdvEquipType.TabIndex = 144;
            this.cdvEquipType.Text = "EQUIP/LINE";
            // 
            // label11
            // 
            this.label11.Image = ((System.Drawing.Image)(resources.GetObject("label11.Image")));
            this.label11.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label11.Location = new System.Drawing.Point(604, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(6, 17);
            this.label11.TabIndex = 143;
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEquip
            // 
            this.lblEquip.AutoSize = true;
            this.lblEquip.Location = new System.Drawing.Point(616, 6);
            this.lblEquip.Name = "lblEquip";
            this.lblEquip.Size = new System.Drawing.Size(77, 13);
            this.lblEquip.TabIndex = 142;
            this.lblEquip.Text = "Resource type";
            // 
            // cdvCare
            // 
            this.cdvCare.FormattingEnabled = true;
            this.cdvCare.Items.AddRange(new object[] {
            "ALL",
            "Designated Equipment",
            "Unspecified equipment"});
            this.cdvCare.Location = new System.Drawing.Point(682, 2);
            this.cdvCare.Name = "cdvCare";
            this.cdvCare.Size = new System.Drawing.Size(102, 21);
            this.cdvCare.TabIndex = 147;
            this.cdvCare.Text = "ALL";
            // 
            // label1
            // 
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(604, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(6, 17);
            this.label1.TabIndex = 146;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(616, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 145;
            this.label3.Text = "Designated Equipment";
            // 
            // ckbUPEH
            // 
            this.ckbUPEH.AutoSize = true;
            this.ckbUPEH.Location = new System.Drawing.Point(806, 5);
            this.ckbUPEH.Name = "ckbUPEH";
            this.ckbUPEH.Size = new System.Drawing.Size(142, 17);
            this.ckbUPEH.TabIndex = 18;
            this.ckbUPEH.Text = "Reflecting on past UPEH";
            this.ckbUPEH.UseVisualStyleBackColor = true;
            this.ckbUPEH.CheckedChanged += new System.EventHandler(this.ckbUPEH_CheckedChanged);
            // 
            // cdvMonth
            // 
            this.cdvMonth.BackColor = System.Drawing.Color.Transparent;
            this.cdvMonth.Location = new System.Drawing.Point(760, 2);
            this.cdvMonth.Name = "cdvMonth";
            this.cdvMonth.RestrictedDayCount = 60;
            this.cdvMonth.Size = new System.Drawing.Size(139, 21);
            this.cdvMonth.TabIndex = 148;
            this.cdvMonth.Visible = false;
            // 
            // RAS020208
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "RAS020208";
            this.Size = new System.Drawing.Size(1400, 600);
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
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcDurationDatePlus udcFromToDate;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOper;
        private System.Windows.Forms.CheckBox ckdUse;
        private System.Windows.Forms.CheckBox ckbConv;
        private System.Windows.Forms.ComboBox cdvEquipType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblEquip;
        private System.Windows.Forms.ComboBox cdvCare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbUPEH;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate cdvMonth;
    }
}
