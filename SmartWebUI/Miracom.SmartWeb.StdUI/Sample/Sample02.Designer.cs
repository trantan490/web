namespace Miracom.SmartWeb.UI
{
    partial class Sample02
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
            this.cdvLotType = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcDurationDate1 = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcDurationDate2 = new Miracom.SmartWeb.UI.Controls.udcDurationDate();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.udcCUSFromToCondition1 = new Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition3.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlWIPDetail.SuspendLayout();
            this.pnlDetailCondition2.SuspendLayout();
            this.pnlDetailCondition1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlRASDetail.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.cdvOper);
            this.pnlMiddle.Size = new System.Drawing.Size(800, 90);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition1, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition2, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition3, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition4, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition5, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition6, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition7, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.pnlCondition8, 0);
            this.pnlMiddle.Controls.SetChildIndex(this.cdvOper, 0);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Controls.Add(this.udcCUSFromToCondition1);
            this.pnlCondition3.Controls.Add(this.checkBox1);
            this.pnlCondition3.Controls.Add(this.udcDurationDate2);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.button3);
            this.pnlCondition2.Controls.Add(this.button2);
            this.pnlCondition2.Controls.Add(this.button1);
            this.pnlCondition2.Controls.Add(this.udcDurationDate1);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvLotType);
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 176);
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
            this.pnlMain.Controls.Add(this.splitContainer);
            this.pnlMain.Location = new System.Drawing.Point(0, 236);
            this.pnlMain.Size = new System.Drawing.Size(800, 364);
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
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
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
            // cdvLotType
            // 
            this.cdvLotType.BackColor = System.Drawing.Color.Transparent;
            this.cdvLotType.bMultiSelect = true;
            this.cdvLotType.ConditionText = "Lot Type";
            this.cdvLotType.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.HQ_TABLE;
            this.cdvLotType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvLotType.Location = new System.Drawing.Point(204, 0);
            this.cdvLotType.MandatoryFlag = false;
            this.cdvLotType.Name = "cdvLotType";
            this.cdvLotType.ParentValue = "";
            this.cdvLotType.sCodeColumnName = "";
            this.cdvLotType.sDynamicQuery = "";
            this.cdvLotType.sFactory = "FABBLW";
            this.cdvLotType.Size = new System.Drawing.Size(180, 21);
            this.cdvLotType.sTableName = "LOT_TYPE";
            this.cdvLotType.sValueColumnName = "";
            this.cdvLotType.TabIndex = 5;
            this.cdvLotType.VisibleValueButton = true;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.ForcInitControl1 = this.udcRASCondition1;
            this.cdvFactory.ForcInitControl2 = this.udcWIPCondition1;
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
            this.cdvFactory.TabIndex = 3;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.Load += new System.EventHandler(this.cdvFactory_Load);
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.udcChartFX1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.spdData);
            this.splitContainer.Size = new System.Drawing.Size(794, 361);
            this.splitContainer.SplitterDistance = 193;
            this.splitContainer.TabIndex = 7;
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 0);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.Size = new System.Drawing.Size(794, 193);
            this.udcChartFX1.TabIndex = 23;
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
            this.spdData.Size = new System.Drawing.Size(794, 164);
            this.spdData.TabIndex = 0;
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
            // udcDurationDate1
            // 
            this.udcDurationDate1.BackColor = System.Drawing.Color.Transparent;
            this.udcDurationDate1.Location = new System.Drawing.Point(82, 3);
            this.udcDurationDate1.Name = "udcDurationDate1";
            this.udcDurationDate1.RestrictedDayCount = 60;
            this.udcDurationDate1.Size = new System.Drawing.Size(275, 21);
            this.udcDurationDate1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(356, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 25);
            this.button1.TabIndex = 1;
            this.button1.Text = "Header one line";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(461, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 24);
            this.button2.TabIndex = 2;
            this.button2.Text = "Header two line";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(605, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(114, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Demo";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.White;
            this.cdvOper.bMultiSelect = true;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(490, 6);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sCodeColumnName = "Code";
            this.cdvOper.sDynamicQuery = "1";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(214, 22);
            this.cdvOper.sTableName = "";
            this.cdvOper.sValueColumnName = "Data";
            this.cdvOper.TabIndex = 17;
            this.cdvOper.VisibleValueButton = true;
            this.cdvOper.Load += new System.EventHandler(this.cdvOper_Load);
            // 
            // udcDurationDate2
            // 
            this.udcDurationDate2.BackColor = System.Drawing.Color.Transparent;
            this.udcDurationDate2.Location = new System.Drawing.Point(82, 6);
            this.udcDurationDate2.Name = "udcDurationDate2";
            this.udcDurationDate2.RestrictedDayCount = 60;
            this.udcDurationDate2.Size = new System.Drawing.Size(155, 21);
            this.udcDurationDate2.TabIndex = 0;
            this.udcDurationDate2.Type = Miracom.SmartWeb.UI.Controls.DateType.OneDate;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(639, 11);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(77, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // udcCUSFromToCondition1
            // 
            this.udcCUSFromToCondition1.BackColor = System.Drawing.Color.White;
            this.udcCUSFromToCondition1.ConditionText = "Operation";
            this.udcCUSFromToCondition1.CondtionType = Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition.eFromToType.OPER;
            this.udcCUSFromToCondition1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCUSFromToCondition1.Location = new System.Drawing.Point(282, 4);
            this.udcCUSFromToCondition1.MandatoryFlag = false;
            this.udcCUSFromToCondition1.Name = "udcCUSFromToCondition1";
            this.udcCUSFromToCondition1.ParentValue = "";
            this.udcCUSFromToCondition1.sFactory = "";
            this.udcCUSFromToCondition1.Size = new System.Drawing.Size(302, 29);
            this.udcCUSFromToCondition1.sTableName = "";
            this.udcCUSFromToCondition1.TabIndex = 2;
            // 
            // Sample02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "Sample02";
            this.Load += new System.EventHandler(this.Sample02_Load);
            this.Click += new System.EventHandler(this.Sample02_Load);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition3.PerformLayout();
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlWIPDetail.ResumeLayout(false);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlRASDetail.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvLotType;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcDurationDate1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOper;
        private Miracom.SmartWeb.UI.Controls.udcDurationDate udcDurationDate2;
        private System.Windows.Forms.CheckBox checkBox1;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private Miracom.SmartWeb.UI.Controls.udcCUSFromToCondition udcCUSFromToCondition1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
