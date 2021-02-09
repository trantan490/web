namespace Miracom.SmartWeb.UI
{
    partial class STD1607
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
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cdvMatVer = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvMat = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvOper = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.txtMatGrp = new System.Windows.Forms.TextBox();
            this.cdvMatGrpList = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvMatGrp = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblFactory = new System.Windows.Forms.Label();
            this.cdvFactory = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatVer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatGrpList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatGrp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFactory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 25);
            this.panel2.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataclose;
            this.btnClose.Location = new System.Drawing.Point(777, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 20);
            this.btnClose.TabIndex = 15;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Current Cycle Time";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.spdData, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // spdData
            // 
            this.spdData.About = "2.5.2008.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(3, 118);
            this.spdData.Name = "spdData";
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 479);
            this.spdData.TabIndex = 30;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.spdData.TextTipAppearance = tipAppearance1;
            this.spdData.Visible = false;
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
            this.spdData.SetActiveViewport(1, 1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cdvMatVer);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cdvMat);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cdvOper);
            this.panel1.Controls.Add(this.txtMatGrp);
            this.panel1.Controls.Add(this.cdvMatGrpList);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cdvMatGrp);
            this.panel1.Controls.Add(this.lblFactory);
            this.panel1.Controls.Add(this.cdvFactory);
            this.panel1.Controls.Add(this.btnExcelExport);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 90);
            this.panel1.TabIndex = 0;
            // 
            // cdvMatVer
            // 
            this.cdvMatVer.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvMatVer.BtnToolTipText = "";
            this.cdvMatVer.DisplaySubItemIndex = -1;
            this.cdvMatVer.DisplayText = "";
            this.cdvMatVer.Focusing = null;
            this.cdvMatVer.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatVer.Index = 0;
            this.cdvMatVer.IsViewBtnImage = false;
            this.cdvMatVer.Location = new System.Drawing.Point(563, 10);
            this.cdvMatVer.MaxLength = 32767;
            this.cdvMatVer.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvMatVer.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvMatVer.Name = "cdvMatVer";
            this.cdvMatVer.ReadOnly = false;
            this.cdvMatVer.SearchSubItemIndex = 0;
            this.cdvMatVer.SelectedDescIndex = -1;
            this.cdvMatVer.SelectedSubItemIndex = -1;
            this.cdvMatVer.SelectionStart = 0;
            this.cdvMatVer.Size = new System.Drawing.Size(50, 21);
            this.cdvMatVer.SmallImageList = null;
            this.cdvMatVer.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvMatVer.TabIndex = 37;
            this.cdvMatVer.TextBoxToolTipText = "";
            this.cdvMatVer.TextBoxWidth = 50;
            this.cdvMatVer.VisibleButton = true;
            this.cdvMatVer.VisibleColumnHeader = false;
            this.cdvMatVer.ButtonPress += new System.EventHandler(this.cdvMatVer_ButtonPress);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(310, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Material";
            // 
            // cdvMat
            // 
            this.cdvMat.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvMat.BtnToolTipText = "";
            this.cdvMat.DisplaySubItemIndex = -1;
            this.cdvMat.DisplayText = "";
            this.cdvMat.Focusing = null;
            this.cdvMat.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMat.Index = 0;
            this.cdvMat.IsViewBtnImage = false;
            this.cdvMat.Location = new System.Drawing.Point(410, 10);
            this.cdvMat.MaxLength = 32767;
            this.cdvMat.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvMat.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvMat.Name = "cdvMat";
            this.cdvMat.ReadOnly = false;
            this.cdvMat.SearchSubItemIndex = 0;
            this.cdvMat.SelectedDescIndex = -1;
            this.cdvMat.SelectedSubItemIndex = -1;
            this.cdvMat.SelectionStart = 0;
            this.cdvMat.Size = new System.Drawing.Size(150, 21);
            this.cdvMat.SmallImageList = null;
            this.cdvMat.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvMat.TabIndex = 35;
            this.cdvMat.TextBoxToolTipText = "";
            this.cdvMat.TextBoxWidth = 150;
            this.cdvMat.VisibleButton = true;
            this.cdvMat.VisibleColumnHeader = false;
            this.cdvMat.TextBoxTextChanged += new System.EventHandler(this.cdvMat_TextBoxTextChanged);
            this.cdvMat.ButtonPress += new System.EventHandler(this.cdvMat_ButtonPress);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(310, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 30;
            this.label4.Text = "Operation";
            // 
            // cdvOper
            // 
            this.cdvOper.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvOper.BtnToolTipText = "";
            this.cdvOper.DisplaySubItemIndex = -1;
            this.cdvOper.DisplayText = "";
            this.cdvOper.Focusing = null;
            this.cdvOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOper.Index = 0;
            this.cdvOper.IsViewBtnImage = false;
            this.cdvOper.Location = new System.Drawing.Point(410, 36);
            this.cdvOper.MaxLength = 32767;
            this.cdvOper.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvOper.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvOper.Name = "cdvOper";
            this.cdvOper.ReadOnly = false;
            this.cdvOper.SearchSubItemIndex = 0;
            this.cdvOper.SelectedDescIndex = -1;
            this.cdvOper.SelectedSubItemIndex = -1;
            this.cdvOper.SelectionStart = 0;
            this.cdvOper.Size = new System.Drawing.Size(150, 21);
            this.cdvOper.SmallImageList = null;
            this.cdvOper.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvOper.TabIndex = 29;
            this.cdvOper.TextBoxToolTipText = "";
            this.cdvOper.TextBoxWidth = 150;
            this.cdvOper.VisibleButton = true;
            this.cdvOper.VisibleColumnHeader = false;
            this.cdvOper.ButtonPress += new System.EventHandler(this.cdvOper_ButtonPress);
            // 
            // txtMatGrp
            // 
            this.txtMatGrp.Location = new System.Drawing.Point(262, 61);
            this.txtMatGrp.Name = "txtMatGrp";
            this.txtMatGrp.Size = new System.Drawing.Size(47, 21);
            this.txtMatGrp.TabIndex = 28;
            this.txtMatGrp.Visible = false;
            // 
            // cdvMatGrpList
            // 
            this.cdvMatGrpList.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvMatGrpList.BtnToolTipText = "";
            this.cdvMatGrpList.DisplaySubItemIndex = -1;
            this.cdvMatGrpList.DisplayText = "";
            this.cdvMatGrpList.Focusing = null;
            this.cdvMatGrpList.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatGrpList.Index = 0;
            this.cdvMatGrpList.IsViewBtnImage = false;
            this.cdvMatGrpList.Location = new System.Drawing.Point(110, 61);
            this.cdvMatGrpList.MaxLength = 32767;
            this.cdvMatGrpList.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvMatGrpList.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvMatGrpList.Name = "cdvMatGrpList";
            this.cdvMatGrpList.ReadOnly = false;
            this.cdvMatGrpList.SearchSubItemIndex = 0;
            this.cdvMatGrpList.SelectedDescIndex = -1;
            this.cdvMatGrpList.SelectedSubItemIndex = -1;
            this.cdvMatGrpList.SelectionStart = 0;
            this.cdvMatGrpList.Size = new System.Drawing.Size(150, 21);
            this.cdvMatGrpList.SmallImageList = null;
            this.cdvMatGrpList.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvMatGrpList.TabIndex = 27;
            this.cdvMatGrpList.TextBoxToolTipText = "";
            this.cdvMatGrpList.TextBoxWidth = 150;
            this.cdvMatGrpList.VisibleButton = true;
            this.cdvMatGrpList.VisibleColumnHeader = false;
            this.cdvMatGrpList.ButtonPress += new System.EventHandler(this.cdvMatGrpList_ButtonPress);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(12, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Group Value";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Material Group";
            // 
            // cdvMatGrp
            // 
            this.cdvMatGrp.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvMatGrp.BtnToolTipText = "";
            this.cdvMatGrp.DisplaySubItemIndex = -1;
            this.cdvMatGrp.DisplayText = "";
            this.cdvMatGrp.Focusing = null;
            this.cdvMatGrp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatGrp.Index = 0;
            this.cdvMatGrp.IsViewBtnImage = false;
            this.cdvMatGrp.Location = new System.Drawing.Point(110, 36);
            this.cdvMatGrp.MaxLength = 32767;
            this.cdvMatGrp.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvMatGrp.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvMatGrp.Name = "cdvMatGrp";
            this.cdvMatGrp.ReadOnly = false;
            this.cdvMatGrp.SearchSubItemIndex = 0;
            this.cdvMatGrp.SelectedDescIndex = -1;
            this.cdvMatGrp.SelectedSubItemIndex = -1;
            this.cdvMatGrp.SelectionStart = 0;
            this.cdvMatGrp.Size = new System.Drawing.Size(150, 21);
            this.cdvMatGrp.SmallImageList = null;
            this.cdvMatGrp.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvMatGrp.TabIndex = 23;
            this.cdvMatGrp.TextBoxToolTipText = "";
            this.cdvMatGrp.TextBoxWidth = 150;
            this.cdvMatGrp.VisibleButton = true;
            this.cdvMatGrp.VisibleColumnHeader = false;
            this.cdvMatGrp.TextBoxTextChanged += new System.EventHandler(this.cdvMatGrp_TextBoxTextChanged);
            this.cdvMatGrp.ButtonPress += new System.EventHandler(this.cdvMatGrp_ButtonPress);
            this.cdvMatGrp.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvMatGrp_SelectedItemChanged);
            // 
            // lblFactory
            // 
            this.lblFactory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFactory.Location = new System.Drawing.Point(12, 14);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(90, 13);
            this.lblFactory.TabIndex = 16;
            this.lblFactory.Text = "Factory";
            // 
            // cdvFactory
            // 
            this.cdvFactory.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvFactory.BtnToolTipText = "";
            this.cdvFactory.DisplaySubItemIndex = -1;
            this.cdvFactory.DisplayText = "";
            this.cdvFactory.Focusing = null;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFactory.Index = 0;
            this.cdvFactory.IsViewBtnImage = false;
            this.cdvFactory.Location = new System.Drawing.Point(110, 10);
            this.cdvFactory.MaxLength = 32767;
            this.cdvFactory.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvFactory.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ReadOnly = false;
            this.cdvFactory.SearchSubItemIndex = 0;
            this.cdvFactory.SelectedDescIndex = -1;
            this.cdvFactory.SelectedSubItemIndex = -1;
            this.cdvFactory.SelectionStart = 0;
            this.cdvFactory.Size = new System.Drawing.Size(150, 21);
            this.cdvFactory.SmallImageList = null;
            this.cdvFactory.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvFactory.TabIndex = 15;
            this.cdvFactory.TextBoxToolTipText = "";
            this.cdvFactory.TextBoxWidth = 150;
            this.cdvFactory.VisibleButton = true;
            this.cdvFactory.VisibleColumnHeader = false;
            this.cdvFactory.ButtonPress += new System.EventHandler(this.cdvFactory_ButtonPress);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelExport.Image = global::Miracom.SmartWeb.UI.Properties.Resources._006excel;
            this.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcelExport.Location = new System.Drawing.Point(721, 10);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(70, 21);
            this.btnExcelExport.TabIndex = 7;
            this.btnExcelExport.Text = "Export";
            this.btnExcelExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcelExport.UseVisualStyleBackColor = true;
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Image = global::Miracom.SmartWeb.UI.Properties.Resources._015view;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(648, 10);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(70, 21);
            this.btnView.TabIndex = 4;
            this.btnView.Text = "View";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // STD1607
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "STD1607";
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.STD1607_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatVer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatGrpList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatGrp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFactory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFactory;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label label1;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatGrp;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatGrpList;
        private System.Windows.Forms.TextBox txtMatGrp;
        private System.Windows.Forms.Label label4;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvOper;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatVer;
        private System.Windows.Forms.Label label2;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMat;
        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label label5;

    }
}
