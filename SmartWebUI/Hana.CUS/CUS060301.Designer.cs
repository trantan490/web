namespace Hana.CUS
{
    partial class CUS060301
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CUS060301));
            this.udcCUSCondition1 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.cboDevice = new System.Windows.Forms.ComboBox();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcDurationDate1 = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
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
            this.pnlLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Size = new System.Drawing.Size(83, 13);
            this.lblTitle.Text = "Material 현황";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.udcDurationDate1);
            this.pnlMiddle.Size = new System.Drawing.Size(800, 66);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition1, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition2, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition3, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition4, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition5, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition6, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition7, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition8, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.udcDurationDate1, 0);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cboDevice);
            this.pnlCondition2.Controls.Add(this.pnlLabel);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.udcCUSCondition1);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 92);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.spdData);
            this.pnlMain.Location = new System.Drawing.Point(0, 152);
            this.pnlMain.Size = new System.Drawing.Size(800, 448);
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
            // udcCUSCondition1
            // 
            this.udcCUSCondition1.BackColor = System.Drawing.Color.White;
            this.udcCUSCondition1.bMultiSelect = true;
            this.udcCUSCondition1.ConditionText = "Material";
            this.udcCUSCondition1.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL;
            this.udcCUSCondition1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCUSCondition1.Location = new System.Drawing.Point(4, 0);
            this.udcCUSCondition1.MandatoryFlag = false;
            this.udcCUSCondition1.Name = "udcCUSCondition1";
            this.udcCUSCondition1.ParentValue = "";
            this.udcCUSCondition1.sFactory = "";
            this.udcCUSCondition1.Size = new System.Drawing.Size(180, 21);
            this.udcCUSCondition1.sTableName = "";
            this.udcCUSCondition1.TabIndex = 1;
            this.udcCUSCondition1.VisibleValueButton = true;
            // 
            // pnlLabel
            // 
            this.pnlLabel.BackColor = System.Drawing.Color.White;
            this.pnlLabel.Controls.Add(this.lblIcon);
            this.pnlLabel.Controls.Add(this.lblDevice);
            this.pnlLabel.Location = new System.Drawing.Point(4, 3);
            this.pnlLabel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(80, 21);
            this.pnlLabel.TabIndex = 18;
            // 
            // lblIcon
            // 
            this.lblIcon.BackColor = System.Drawing.Color.White;
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(5, 2);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 1;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDevice
            // 
            this.lblDevice.BackColor = System.Drawing.Color.White;
            this.lblDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDevice.Location = new System.Drawing.Point(18, 2);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(60, 17);
            this.lblDevice.TabIndex = 0;
            this.lblDevice.Text = "Device";
            this.lblDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboDevice
            // 
            this.cboDevice.FormattingEnabled = true;
            this.cboDevice.Location = new System.Drawing.Point(88, 3);
            this.cboDevice.Name = "cboDevice";
            this.cboDevice.Size = new System.Drawing.Size(97, 21);
            this.cboDevice.TabIndex = 19;
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
            this.spdData.Size = new System.Drawing.Size(794, 445);
            this.spdData.TabIndex = 3;
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
            // udcDurationDate1
            // 
            this.udcDurationDate1.BackColor = System.Drawing.Color.Transparent;
            this.udcDurationDate1.Location = new System.Drawing.Point(204, 8);
            this.udcDurationDate1.Name = "udcDurationDate1";
            this.udcDurationDate1.RestrictedDayCount = 60;
            this.udcDurationDate1.Size = new System.Drawing.Size(280, 21);
            this.udcDurationDate1.TabIndex = 18;
            // 
            // CUS060301
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.WIP_BASE;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "CUS060301";
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.pnlLabel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCUSCondition1;
        private System.Windows.Forms.Panel pnlLabel;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox cboDevice;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcDurationDate1;
    }
}
