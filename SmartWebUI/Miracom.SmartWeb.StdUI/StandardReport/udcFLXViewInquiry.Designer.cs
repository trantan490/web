namespace Miracom.SmartWeb.UI
{
    partial class udcFLXViewInquiry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcFLXViewInquiry));
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlCondtion = new System.Windows.Forms.Panel();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.cdvInqName = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblInquiryName = new System.Windows.Forms.Label();
            this.cdvFactory = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblFac = new System.Windows.Forms.Label();
            this.cdvInqGrp = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblInqGrp = new System.Windows.Forms.Label();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlData = new System.Windows.Forms.Panel();
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlCondition = new System.Windows.Forms.Panel();
            this.grpFilter = new System.Windows.Forms.GroupBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.txtGrpItem = new System.Windows.Forms.TextBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblGrpItem = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tblPanel.SuspendLayout();
            this.pnlCondtion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvInqName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFactory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvInqGrp)).BeginInit();
            this.pnlData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.pnlCondition.SuspendLayout();
            this.grpFilter.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tblPanel
            // 
            this.tblPanel.BackColor = System.Drawing.Color.White;
            this.tblPanel.ColumnCount = 1;
            this.tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanel.Controls.Add(this.pnlCondtion, 0, 1);
            this.tblPanel.Controls.Add(this.pnlData, 0, 2);
            this.tblPanel.Controls.Add(this.pnlTitle, 0, 0);
            this.tblPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPanel.Location = new System.Drawing.Point(0, 0);
            this.tblPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tblPanel.Name = "tblPanel";
            this.tblPanel.RowCount = 3;
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblPanel.Size = new System.Drawing.Size(800, 600);
            this.tblPanel.TabIndex = 9;
            // 
            // pnlCondtion
            // 
            this.pnlCondtion.BackColor = System.Drawing.Color.Transparent;
            this.pnlCondtion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCondtion.Controls.Add(this.dtpDateTime);
            this.pnlCondtion.Controls.Add(this.cdvInqName);
            this.pnlCondtion.Controls.Add(this.lblInquiryName);
            this.pnlCondtion.Controls.Add(this.cdvFactory);
            this.pnlCondtion.Controls.Add(this.lblFac);
            this.pnlCondtion.Controls.Add(this.cdvInqGrp);
            this.pnlCondtion.Controls.Add(this.lblInqGrp);
            this.pnlCondtion.Controls.Add(this.btnExcelExport);
            this.pnlCondtion.Controls.Add(this.btnView);
            this.pnlCondtion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCondtion.Location = new System.Drawing.Point(0, 25);
            this.pnlCondtion.Margin = new System.Windows.Forms.Padding(0);
            this.pnlCondtion.Name = "pnlCondtion";
            this.pnlCondtion.Size = new System.Drawing.Size(800, 70);
            this.pnlCondtion.TabIndex = 0;
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.Location = new System.Drawing.Point(410, 10);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(160, 21);
            this.dtpDateTime.TabIndex = 15;
            // 
            // cdvInqName
            // 
            this.cdvInqName.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvInqName.BtnToolTipText = "";
            this.cdvInqName.DisplaySubItemIndex = -1;
            this.cdvInqName.DisplayText = "";
            this.cdvInqName.Focusing = null;
            this.cdvInqName.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvInqName.Index = 0;
            this.cdvInqName.IsViewBtnImage = false;
            this.cdvInqName.Location = new System.Drawing.Point(410, 36);
            this.cdvInqName.MaxLength = 32767;
            this.cdvInqName.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvInqName.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvInqName.Name = "cdvInqName";
            this.cdvInqName.ReadOnly = false;
            this.cdvInqName.SearchSubItemIndex = 0;
            this.cdvInqName.SelectedDescIndex = -1;
            this.cdvInqName.SelectedSubItemIndex = -1;
            this.cdvInqName.SelectionStart = 0;
            this.cdvInqName.Size = new System.Drawing.Size(160, 21);
            this.cdvInqName.SmallImageList = null;
            this.cdvInqName.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvInqName.TabIndex = 13;
            this.cdvInqName.TextBoxToolTipText = "";
            this.cdvInqName.TextBoxWidth = 160;
            this.cdvInqName.VisibleButton = true;
            this.cdvInqName.VisibleColumnHeader = false;
            this.cdvInqName.ButtonPress += new System.EventHandler(this.cdvInqName_ButtonPress);
            // 
            // lblInquiryName
            // 
            this.lblInquiryName.BackColor = System.Drawing.Color.White;
            this.lblInquiryName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblInquiryName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInquiryName.Location = new System.Drawing.Point(310, 40);
            this.lblInquiryName.Name = "lblInquiryName";
            this.lblInquiryName.Size = new System.Drawing.Size(100, 13);
            this.lblInquiryName.TabIndex = 14;
            this.lblInquiryName.Text = "Inquiry Name";
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
            this.cdvFactory.Size = new System.Drawing.Size(160, 21);
            this.cdvFactory.SmallImageList = null;
            this.cdvFactory.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvFactory.TabIndex = 12;
            this.cdvFactory.TextBoxToolTipText = "";
            this.cdvFactory.TextBoxWidth = 160;
            this.cdvFactory.VisibleButton = true;
            this.cdvFactory.VisibleColumnHeader = false;
            // 
            // lblFac
            // 
            this.lblFac.BackColor = System.Drawing.Color.White;
            this.lblFac.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFac.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFac.Location = new System.Drawing.Point(12, 14);
            this.lblFac.Name = "lblFac";
            this.lblFac.Size = new System.Drawing.Size(100, 13);
            this.lblFac.TabIndex = 11;
            this.lblFac.Text = "Factory";
            // 
            // cdvInqGrp
            // 
            this.cdvInqGrp.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvInqGrp.BtnToolTipText = "";
            this.cdvInqGrp.DisplaySubItemIndex = -1;
            this.cdvInqGrp.DisplayText = "";
            this.cdvInqGrp.Focusing = null;
            this.cdvInqGrp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvInqGrp.Index = 0;
            this.cdvInqGrp.IsViewBtnImage = false;
            this.cdvInqGrp.Location = new System.Drawing.Point(110, 36);
            this.cdvInqGrp.MaxLength = 32767;
            this.cdvInqGrp.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvInqGrp.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvInqGrp.Name = "cdvInqGrp";
            this.cdvInqGrp.ReadOnly = false;
            this.cdvInqGrp.SearchSubItemIndex = 0;
            this.cdvInqGrp.SelectedDescIndex = -1;
            this.cdvInqGrp.SelectedSubItemIndex = -1;
            this.cdvInqGrp.SelectionStart = 0;
            this.cdvInqGrp.Size = new System.Drawing.Size(160, 21);
            this.cdvInqGrp.SmallImageList = null;
            this.cdvInqGrp.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvInqGrp.TabIndex = 9;
            this.cdvInqGrp.TextBoxToolTipText = "";
            this.cdvInqGrp.TextBoxWidth = 160;
            this.cdvInqGrp.VisibleButton = true;
            this.cdvInqGrp.VisibleColumnHeader = false;
            this.cdvInqGrp.ButtonPress += new System.EventHandler(this.cdvInqGrp_ButtonPress);
            // 
            // lblInqGrp
            // 
            this.lblInqGrp.BackColor = System.Drawing.Color.White;
            this.lblInqGrp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblInqGrp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblInqGrp.Location = new System.Drawing.Point(12, 40);
            this.lblInqGrp.Name = "lblInqGrp";
            this.lblInqGrp.Size = new System.Drawing.Size(100, 13);
            this.lblInqGrp.TabIndex = 10;
            this.lblInqGrp.Text = "Inquiry Group";
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExcelExport.Image")));
            this.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcelExport.Location = new System.Drawing.Point(721, 10);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(70, 21);
            this.btnExcelExport.TabIndex = 7;
            this.btnExcelExport.Text = "Export";
            this.btnExcelExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcelExport.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Image = ((System.Drawing.Image)(resources.GetObject("btnView.Image")));
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
            // pnlData
            // 
            this.pnlData.Controls.Add(this.spdData);
            this.pnlData.Controls.Add(this.pnlCondition);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(3, 98);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(794, 499);
            this.pnlData.TabIndex = 3;
            // 
            // spdData
            // 
            this.spdData.About = "2.5.2008.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(0, 74);
            this.spdData.Name = "spdData";
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 425);
            this.spdData.TabIndex = 0;
            tipAppearance1.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.spdData.TextTipAppearance = tipAppearance1;
            // 
            // spdData_Sheet1
            // 
            this.spdData_Sheet1.Reset();
            this.spdData_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdData_Sheet1.ColumnCount = 0;
            this.spdData_Sheet1.RowCount = 0;
            this.spdData_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.spdData_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdData.SetActiveViewport(1, 1);
            // 
            // pnlCondition
            // 
            this.pnlCondition.Controls.Add(this.grpFilter);
            this.pnlCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCondition.Location = new System.Drawing.Point(0, 0);
            this.pnlCondition.Name = "pnlCondition";
            this.pnlCondition.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlCondition.Size = new System.Drawing.Size(794, 74);
            this.pnlCondition.TabIndex = 5;
            // 
            // grpFilter
            // 
            this.grpFilter.BackColor = System.Drawing.Color.White;
            this.grpFilter.Controls.Add(this.txtValue);
            this.grpFilter.Controls.Add(this.lblValue);
            this.grpFilter.Controls.Add(this.txtFilter);
            this.grpFilter.Controls.Add(this.txtGrpItem);
            this.grpFilter.Controls.Add(this.lblFilter);
            this.grpFilter.Controls.Add(this.lblGrpItem);
            this.grpFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpFilter.Location = new System.Drawing.Point(0, 0);
            this.grpFilter.Name = "grpFilter";
            this.grpFilter.Size = new System.Drawing.Size(794, 71);
            this.grpFilter.TabIndex = 4;
            this.grpFilter.TabStop = false;
            this.grpFilter.Text = "Condition";
            // 
            // txtValue
            // 
            this.txtValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValue.BackColor = System.Drawing.Color.White;
            this.txtValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(408, 16);
            this.txtValue.MaxLength = 50;
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(381, 20);
            this.txtValue.TabIndex = 21;
            // 
            // lblValue
            // 
            this.lblValue.BackColor = System.Drawing.Color.White;
            this.lblValue.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblValue.Location = new System.Drawing.Point(307, 20);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(100, 13);
            this.lblValue.TabIndex = 20;
            this.lblValue.Text = "Select Item : Value";
            // 
            // txtFilter
            // 
            this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilter.BackColor = System.Drawing.Color.White;
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(108, 41);
            this.txtFilter.MaxLength = 50;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.ReadOnly = true;
            this.txtFilter.Size = new System.Drawing.Size(681, 20);
            this.txtFilter.TabIndex = 19;
            // 
            // txtGrpItem
            // 
            this.txtGrpItem.BackColor = System.Drawing.Color.White;
            this.txtGrpItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrpItem.Location = new System.Drawing.Point(108, 16);
            this.txtGrpItem.MaxLength = 50;
            this.txtGrpItem.Name = "txtGrpItem";
            this.txtGrpItem.ReadOnly = true;
            this.txtGrpItem.Size = new System.Drawing.Size(160, 20);
            this.txtGrpItem.TabIndex = 17;
            // 
            // lblFilter
            // 
            this.lblFilter.BackColor = System.Drawing.Color.White;
            this.lblFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFilter.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblFilter.Location = new System.Drawing.Point(10, 44);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(100, 13);
            this.lblFilter.TabIndex = 18;
            this.lblFilter.Text = "Filter Query";
            // 
            // lblGrpItem
            // 
            this.lblGrpItem.BackColor = System.Drawing.Color.White;
            this.lblGrpItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblGrpItem.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblGrpItem.Location = new System.Drawing.Point(10, 20);
            this.lblGrpItem.Name = "lblGrpItem";
            this.lblGrpItem.Size = new System.Drawing.Size(100, 13);
            this.lblGrpItem.TabIndex = 16;
            this.lblGrpItem.Text = "Group Item";
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(800, 25);
            this.pnlTitle.TabIndex = 2;
            this.pnlTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlTitle_MouseDown);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(777, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 19);
            this.btnClose.TabIndex = 15;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(4, 6);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(135, 12);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Flexible Inquiry";
            // 
            // udcFLXViewInquiry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tblPanel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "udcFLXViewInquiry";
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.udcFLXViewInquiry_Load);
            this.tblPanel.ResumeLayout(false);
            this.pnlCondtion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdvInqName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFactory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvInqGrp)).EndInit();
            this.pnlData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.pnlCondition.ResumeLayout(false);
            this.grpFilter.ResumeLayout(false);
            this.grpFilter.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblPanel;
        private System.Windows.Forms.Panel pnlCondtion;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlData;
        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory;
        private System.Windows.Forms.Label lblFac;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvInqGrp;
        private System.Windows.Forms.Label lblInqGrp;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvInqName;
        private System.Windows.Forms.Label lblInquiryName;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
        private System.Windows.Forms.Label lblGrpItem;
        private System.Windows.Forms.TextBox txtGrpItem;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.GroupBox grpFilter;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Panel pnlCondition;
    }
}
