namespace Hana.YLD
{
    partial class BUM010102
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
            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel12 = new System.Windows.Forms.Panel();
            this.spdWafBin = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdWafBin_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnMapView = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtLotID = new System.Windows.Forms.TextBox();
            this.lblLotID = new System.Windows.Forms.Label();
            this.rdoMMG = new System.Windows.Forms.RadioButton();
            this.rdoOper = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvOperation = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.saveExcel = new System.Windows.Forms.SaveFileDialog();
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.cdvOper = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin_Sheet1)).BeginInit();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(1059, 90);
            // 
            // pnlCondition8
            // 
            this.pnlCondition8.Location = new System.Drawing.Point(0, 180);
            // 
            // pnlCondition7
            // 
            this.pnlCondition7.Location = new System.Drawing.Point(0, 156);
            // 
            // pnlCondition6
            // 
            this.pnlCondition6.Location = new System.Drawing.Point(0, 132);
            // 
            // pnlCondition5
            // 
            this.pnlCondition5.Location = new System.Drawing.Point(0, 108);
            // 
            // pnlCondition4
            // 
            this.pnlCondition4.Location = new System.Drawing.Point(0, 84);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 74);
            this.pnlCondition3.Size = new System.Drawing.Size(1057, 10);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvOper);
            this.pnlCondition2.Controls.Add(this.cdvOperation);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.txtLotID);
            this.pnlCondition2.Controls.Add(this.lblLotID);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 41);
            this.pnlCondition2.Size = new System.Drawing.Size(1057, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.cdvFactory);
            this.pnlCondition1.Controls.Add(this.rdoOper);
            this.pnlCondition1.Controls.Add(this.rdoMMG);
            this.pnlCondition1.Size = new System.Drawing.Size(1057, 33);
            // 
            // pnlWIPDetail
            // 
            this.pnlWIPDetail.Location = new System.Drawing.Point(0, 176);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Location = new System.Drawing.Point(0, 203);
            this.pnlMain.Size = new System.Drawing.Size(1059, 397);
            // 
            // btnDetail
            // 
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // btnView
            // 
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExcelExport.FlatAppearance.BorderSize = 0;
            this.btnExcelExport.Visible = false;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSort
            // 
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // pnlRASDetail
            // 
            this.pnlRASDetail.Location = new System.Drawing.Point(0, 116);
            // 
            // pnlBUMPDetail
            // 
            this.pnlBUMPDetail.Location = new System.Drawing.Point(0, 116);
            this.pnlBUMPDetail.Size = new System.Drawing.Size(1059, 87);
            // 
            // panel5
            // 
            this.panel5.Size = new System.Drawing.Size(1057, 24);
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
            this.panel6.Size = new System.Drawing.Size(1057, 24);
            // 
            // panel7
            // 
            this.panel7.Size = new System.Drawing.Size(1057, 24);
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel12);
            this.splitContainer1.Panel1.Controls.Add(this.panel11);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1053, 394);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.spdWafBin);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(0, 60);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(274, 334);
            this.panel12.TabIndex = 17;
            // 
            // spdWafBin
            // 
            this.spdWafBin.About = "4.0.2001.2005";
            this.spdWafBin.AccessibleDescription = "spdWafBin, Sheet1, Row 0, Column 0, ";
            this.spdWafBin.BackColor = System.Drawing.Color.White;
            this.spdWafBin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdWafBin.Location = new System.Drawing.Point(0, 0);
            this.spdWafBin.Name = "spdWafBin";
            this.spdWafBin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdWafBin.RPT_IsPreCellsType = true;
            this.spdWafBin.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdWafBin.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdWafBin_Sheet1});
            this.spdWafBin.Size = new System.Drawing.Size(274, 334);
            this.spdWafBin.TabIndex = 27;
            // 
            // spdWafBin_Sheet1
            // 
            this.spdWafBin_Sheet1.Reset();
            this.spdWafBin_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdWafBin_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdWafBin_Sheet1.ColumnCount = 8;
            this.spdWafBin_Sheet1.RowCount = 0;
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "Sel";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "No";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "Device";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "Wafer ID";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "T Qty";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "G Qty";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "F Qty";
            this.spdWafBin_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "Yield";
            this.spdWafBin_Sheet1.Columns.Get(0).CellType = checkBoxCellType1;
            this.spdWafBin_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
            this.spdWafBin_Sheet1.Columns.Get(0).Label = "Sel";
            this.spdWafBin_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.General;
            this.spdWafBin_Sheet1.Columns.Get(0).Width = 24F;
            this.spdWafBin_Sheet1.Columns.Get(1).Label = "No";
            this.spdWafBin_Sheet1.Columns.Get(1).Width = 40F;
            this.spdWafBin_Sheet1.Columns.Get(2).Label = "Device";
            this.spdWafBin_Sheet1.Columns.Get(2).Width = 80F;
            this.spdWafBin_Sheet1.Columns.Get(3).Label = "Wafer ID";
            this.spdWafBin_Sheet1.Columns.Get(3).Width = 90F;
            this.spdWafBin_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.spdWafBin_Sheet1.Columns.Get(4).Label = "T Qty";
            this.spdWafBin_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.spdWafBin_Sheet1.Columns.Get(5).Label = "G Qty";
            this.spdWafBin_Sheet1.Columns.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.spdWafBin_Sheet1.Columns.Get(6).Label = "F Qty";
            this.spdWafBin_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdWafBin_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdWafBin.SetActiveViewport(0, 1, 0);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.SystemColors.Control;
            this.panel11.Controls.Add(this.btnMapView);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 25);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(274, 35);
            this.panel11.TabIndex = 16;
            // 
            // btnMapView
            // 
            this.btnMapView.Location = new System.Drawing.Point(6, 3);
            this.btnMapView.Name = "btnMapView";
            this.btnMapView.Size = new System.Drawing.Size(104, 29);
            this.btnMapView.TabIndex = 0;
            this.btnMapView.Text = "Map Gallery View";
            this.btnMapView.UseVisualStyleBackColor = true;
            this.btnMapView.Click += new System.EventHandler(this.btnMapView_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(274, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(64, 22);
            this.toolStripLabel1.Text = "Wafer List";
            // 
            // txtLotID
            // 
            this.txtLotID.BackColor = System.Drawing.Color.White;
            this.txtLotID.Location = new System.Drawing.Point(97, 6);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Size = new System.Drawing.Size(188, 21);
            this.txtLotID.TabIndex = 78;
            this.txtLotID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLotID_KeyPress);
            // 
            // lblLotID
            // 
            this.lblLotID.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLotID.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLotID.Location = new System.Drawing.Point(15, 10);
            this.lblLotID.Name = "lblLotID";
            this.lblLotID.Size = new System.Drawing.Size(72, 13);
            this.lblLotID.TabIndex = 77;
            this.lblLotID.Text = "     Lot ID";
            this.lblLotID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rdoMMG
            // 
            this.rdoMMG.AutoSize = true;
            this.rdoMMG.Checked = true;
            this.rdoMMG.Location = new System.Drawing.Point(31, 7);
            this.rdoMMG.Name = "rdoMMG";
            this.rdoMMG.Size = new System.Drawing.Size(79, 17);
            this.rdoMMG.TabIndex = 137;
            this.rdoMMG.TabStop = true;
            this.rdoMMG.Text = "Merge standard";
            this.rdoMMG.UseVisualStyleBackColor = true;
            this.rdoMMG.CheckedChanged += new System.EventHandler(this.rdoMMG_CheckedChanged);
            // 
            // rdoOper
            // 
            this.rdoOper.AutoSize = true;
            this.rdoOper.Location = new System.Drawing.Point(127, 7);
            this.rdoOper.Name = "rdoOper";
            this.rdoOper.Size = new System.Drawing.Size(73, 17);
            this.rdoOper.TabIndex = 138;
            this.rdoOper.Text = "Operation standard";
            this.rdoOper.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(864, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 139;
            this.label4.Text = "Oper";
            this.label4.Visible = false;
            // 
            // cdvOperation
            // 
            this.cdvOperation.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvOperation.BorderHotColor = System.Drawing.Color.Black;
            this.cdvOperation.BtnFlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cdvOperation.BtnToolTipText = "";
            this.cdvOperation.DescText = "";
            this.cdvOperation.DisplaySubItemIndex = -1;
            this.cdvOperation.DisplayText = "";
            this.cdvOperation.Enabled = false;
            this.cdvOperation.Focusing = null;
            this.cdvOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvOperation.Index = 0;
            this.cdvOperation.IsViewBtnImage = true;
            this.cdvOperation.Location = new System.Drawing.Point(909, 5);
            this.cdvOperation.MaxLength = 32767;
            this.cdvOperation.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvOperation.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
            this.cdvOperation.MultiSelect = false;
            this.cdvOperation.Name = "cdvOperation";
            this.cdvOperation.ReadOnly = false;
            this.cdvOperation.SearchSubItemIndex = 0;
            this.cdvOperation.SelectedDescIndex = -1;
            this.cdvOperation.SelectedDescToQueryText = "";
            this.cdvOperation.SelectedSubItemIndex = -1;
            this.cdvOperation.SelectedValueToQueryText = "";
            this.cdvOperation.SelectionStart = 0;
            this.cdvOperation.Size = new System.Drawing.Size(130, 21);
            this.cdvOperation.SmallImageList = null;
            this.cdvOperation.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvOperation.TabIndex = 140;
            this.cdvOperation.TextBoxToolTipText = "";
            this.cdvOperation.TextBoxWidth = 130;
            this.cdvOperation.Visible = false;
            this.cdvOperation.VisibleButton = true;
            this.cdvOperation.VisibleColumnHeader = false;
            this.cdvOperation.VisibleDescription = false;
            // 
            // saveExcel
            // 
            this.saveExcel.DefaultExt = "xls";
            this.saveExcel.Filter = "Excel files|*.xls";
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.Transparent;
            this.cdvFactory.bMultiSelect = false;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.ControlRef = true;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Location = new System.Drawing.Point(302, 6);
            this.cdvFactory.MandatoryFlag = true;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(180, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 140;
            this.cdvFactory.Visible = false;
            this.cdvFactory.VisibleValueButton = true;
            this.cdvFactory.ValueSelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_ValueSelectedItemChanged);
            // 
            // cdvOper
            // 
            this.cdvOper.BackColor = System.Drawing.Color.Transparent;
            this.cdvOper.bMultiSelect = false;
            this.cdvOper.ConditionText = "Operation";
            this.cdvOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Location = new System.Drawing.Point(302, 6);
            this.cdvOper.MandatoryFlag = false;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ParentValue = "";
            this.cdvOper.sCodeColumnName = "";
            this.cdvOper.sDynamicQuery = "";
            this.cdvOper.sFactory = "";
            this.cdvOper.Size = new System.Drawing.Size(180, 21);
            this.cdvOper.sTableName = "";
            this.cdvOper.sValueColumnName = "";
            this.cdvOper.TabIndex = 142;
            this.cdvOper.VisibleValueButton = true;
            // 
            // BUM010102
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BaseFormType = Miracom.SmartWeb.UI.Controls.udcCUSReport002.eBaseFormType.BUMP_BASE;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "BUM010102";
            this.Size = new System.Drawing.Size(1059, 600);
            this.Load += new System.EventHandler(this.YLD041002_Load);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin_Sheet1)).EndInit();
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtLotID;
        private System.Windows.Forms.Label lblLotID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoOper;
        private System.Windows.Forms.RadioButton rdoMMG;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvOperation;
        //private System.Windows.Forms.CustomToolStrip ctsStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        //private UI.udcSpread spdWafBin;
        //private FarPoint.Win.Spread.SheetView spdWafBin_Sheet1;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnMapView;
        private System.Windows.Forms.SaveFileDialog saveExcel;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdWafBin;
        private FarPoint.Win.Spread.SheetView spdWafBin_Sheet1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvOper;
    }
}
