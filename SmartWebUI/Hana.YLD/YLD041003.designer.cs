namespace Hana.YLD
{
    partial class YLD041003
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panel11 = new System.Windows.Forms.Panel();
            this.btnMapView = new System.Windows.Forms.Button();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtLotID = new System.Windows.Forms.TextBox();
            this.lblLotID = new System.Windows.Forms.Label();
            this.rdoMMG = new System.Windows.Forms.RadioButton();
            this.rdoOper = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvOperation = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.lblFactory = new System.Windows.Forms.Label();
            this.saveExcel = new System.Windows.Forms.SaveFileDialog();
            this.spdWafBin = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdWafBin_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlMiddle.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(1059, 90);
            // 
            // pnlCondition3
            // 
            this.pnlCondition3.Location = new System.Drawing.Point(0, 74);
            this.pnlCondition3.Size = new System.Drawing.Size(1057, 10);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvOperation);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.txtLotID);
            this.pnlCondition2.Controls.Add(this.lblLotID);
            this.pnlCondition2.Location = new System.Drawing.Point(0, 41);
            this.pnlCondition2.Size = new System.Drawing.Size(1057, 33);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.txtFactory);
            this.pnlCondition1.Controls.Add(this.lblFactory);
            this.pnlCondition1.Controls.Add(this.rdoOper);
            this.pnlCondition1.Controls.Add(this.rdoMMG);
            this.pnlCondition1.Size = new System.Drawing.Size(1057, 33);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Location = new System.Drawing.Point(0, 116);
            this.pnlMain.Size = new System.Drawing.Size(1059, 484);
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
            // btnView
            // 
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.FlatAppearance.BorderSize = 0;
            this.btnView.Location = new System.Drawing.Point(234, 1);
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.spdWafBin);
            this.splitContainer1.Panel1.Controls.Add(this.panel11);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1053, 481);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(274, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
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
            this.btnMapView.Text = "Map Merge View";
            this.btnMapView.UseVisualStyleBackColor = true;
            this.btnMapView.Click += new System.EventHandler(this.btnMapView_Click);
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
            this.label4.Location = new System.Drawing.Point(299, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 139;
            this.label4.Text = "Oper";
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
            this.cdvOperation.Location = new System.Drawing.Point(344, 6);
            this.cdvOperation.MaxLength = 32767;
            this.cdvOperation.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvOperation.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
            this.cdvOperation.MultiSelect = false;
            this.cdvOperation.Name = "cdvOperation";
            this.cdvOperation.ReadOnly = false;
            this.cdvOperation.SearchSubItemIndex = 0;
            this.cdvOperation.SelectedDescIndex = -1;
            this.cdvOperation.SelectedSubItemIndex = -1;
            this.cdvOperation.SelectedValueToQueryText = "";
            this.cdvOperation.SelectionStart = 0;
            this.cdvOperation.Size = new System.Drawing.Size(130, 21);
            this.cdvOperation.SmallImageList = null;
            this.cdvOperation.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvOperation.TabIndex = 140;
            this.cdvOperation.TextBoxToolTipText = "";
            this.cdvOperation.TextBoxWidth = 130;
            this.cdvOperation.VisibleButton = true;
            this.cdvOperation.VisibleColumnHeader = false;
            this.cdvOperation.VisibleDescription = false;
            this.cdvOperation.ButtonPress += new System.EventHandler(this.cdvOperation_ButtonPress);
            // 
            // txtFactory
            // 
            this.txtFactory.BackColor = System.Drawing.Color.White;
            this.txtFactory.Location = new System.Drawing.Point(692, 2);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.ReadOnly = true;
            this.txtFactory.Size = new System.Drawing.Size(147, 21);
            this.txtFactory.TabIndex = 80;
            this.txtFactory.Visible = false;
            // 
            // lblFactory
            // 
            this.lblFactory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFactory.Location = new System.Drawing.Point(600, 7);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(90, 13);
            this.lblFactory.TabIndex = 79;
            this.lblFactory.Text = "     Factory";
            this.lblFactory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFactory.Visible = false;
            // 
            // saveExcel
            // 
            this.saveExcel.DefaultExt = "xls";
            this.saveExcel.Filter = "Excel files|*.xls";
            // 
            // spdWafBin
            // 
            this.spdWafBin.About = "4.0.2001.2005";
            this.spdWafBin.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdWafBin.BackColor = System.Drawing.Color.White;
            this.spdWafBin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdWafBin.Location = new System.Drawing.Point(0, 60);
            this.spdWafBin.Name = "spdWafBin";
            this.spdWafBin.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdWafBin.RPT_IsPreCellsType = true;
            this.spdWafBin.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdWafBin.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdWafBin_Sheet1});
            this.spdWafBin.Size = new System.Drawing.Size(274, 421);
            this.spdWafBin.TabIndex = 28;
            // 
            // spdWafBin_Sheet1
            // 
            this.spdWafBin_Sheet1.Reset();
            this.spdWafBin_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdWafBin_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdWafBin_Sheet1.ColumnCount = 0;
            this.spdWafBin_Sheet1.RowCount = 0;
            this.spdWafBin_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdWafBin_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdWafBin.SetActiveViewport(0, 1, 1);
            // 
            // YLD041003
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ConditionCount = 3;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "YLD041003";
            this.Size = new System.Drawing.Size(1059, 600);
            this.Load += new System.EventHandler(this.YLD041003_Load);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdWafBin_Sheet1)).EndInit();
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
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.Label lblFactory;
        //private System.Windows.Forms.CustomToolStrip ctsStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        //private UI.udcSpread spdWafBin;
        //private FarPoint.Win.Spread.SheetView spdWafBin_Sheet1;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Button btnMapView;
        private System.Windows.Forms.SaveFileDialog saveExcel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdWafBin;
        private FarPoint.Win.Spread.SheetView spdWafBin_Sheet1;
    }
}
