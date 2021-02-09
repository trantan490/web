namespace Miracom.SmartWeb.UI.Controls
{
    partial class udcCUSReportUpload001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcCUSReportUpload001));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lisUploadData = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imlSmallIcon = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpPlanMonth = new System.Windows.Forms.DateTimePicker();
            this.dtpPlanYear = new System.Windows.Forms.DateTimePicker();
            this.lblPlanMonth = new System.Windows.Forms.Label();
            this.grpUploadData = new System.Windows.Forms.GroupBox();
            this.spdUploadData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdUploadData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlFile = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.grpFunc = new System.Windows.Forms.GroupBox();
            this.dtpPlanDate = new System.Windows.Forms.DateTimePicker();
            this.lblPlanDate = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpUploadData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdUploadData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdUploadData_Sheet1)).BeginInit();
            this.pnlFile.SuspendLayout();
            this.grpFunc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlBottom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 1;
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
            this.pnlTitle.TabIndex = 8;
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
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(3, 7);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(63, 13);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Plan Upload";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.btnDelete);
            this.pnlBottom.Controls.Add(this.btnCreate);
            this.pnlBottom.Controls.Add(this.btnUpdate);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBottom.Location = new System.Drawing.Point(0, 565);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(800, 35);
            this.pnlBottom.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Miracom.SmartWeb.UI.Properties.Resources._014datadel;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(720, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataopen;
            this.btnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreate.Location = new System.Drawing.Point(646, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(70, 23);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "Create";
            this.btnCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Image = global::Miracom.SmartWeb.UI.Properties.Resources._013datasaveas;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(646, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 23);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lisUploadData);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpUploadData);
            this.splitContainer1.Panel2.Controls.Add(this.grpFunc);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(800, 537);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 3;
            // 
            // lisUploadData
            // 
            this.lisUploadData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader2});
            this.lisUploadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisUploadData.EnableSort = true;
            this.lisUploadData.EnableSortIcon = true;
            this.lisUploadData.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lisUploadData.FullRowSelect = true;
            this.lisUploadData.HideSelection = false;
            this.lisUploadData.Location = new System.Drawing.Point(3, 48);
            this.lisUploadData.MultiSelect = false;
            this.lisUploadData.Name = "lisUploadData";
            this.lisUploadData.Size = new System.Drawing.Size(192, 484);
            this.lisUploadData.SmallImageList = this.imlSmallIcon;
            this.lisUploadData.TabIndex = 1;
            this.lisUploadData.UseCompatibleStateImageBehavior = false;
            this.lisUploadData.View = System.Windows.Forms.View.Details;
            this.lisUploadData.SelectedIndexChanged += new System.EventHandler(this.lisUploadData_SelectedIndexChanged);
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Plan Date";
            this.ColumnHeader2.Width = 150;
            // 
            // imlSmallIcon
            // 
            this.imlSmallIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmallIcon.ImageStream")));
            this.imlSmallIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imlSmallIcon.Images.SetKeyName(0, "");
            this.imlSmallIcon.Images.SetKeyName(1, "");
            this.imlSmallIcon.Images.SetKeyName(2, "");
            this.imlSmallIcon.Images.SetKeyName(3, "");
            this.imlSmallIcon.Images.SetKeyName(4, "");
            this.imlSmallIcon.Images.SetKeyName(5, "");
            this.imlSmallIcon.Images.SetKeyName(6, "");
            this.imlSmallIcon.Images.SetKeyName(7, "");
            this.imlSmallIcon.Images.SetKeyName(8, "");
            this.imlSmallIcon.Images.SetKeyName(9, "");
            this.imlSmallIcon.Images.SetKeyName(10, "");
            this.imlSmallIcon.Images.SetKeyName(11, "");
            this.imlSmallIcon.Images.SetKeyName(12, "");
            this.imlSmallIcon.Images.SetKeyName(13, "");
            this.imlSmallIcon.Images.SetKeyName(14, "");
            this.imlSmallIcon.Images.SetKeyName(15, "");
            this.imlSmallIcon.Images.SetKeyName(16, "");
            this.imlSmallIcon.Images.SetKeyName(17, "");
            this.imlSmallIcon.Images.SetKeyName(18, "");
            this.imlSmallIcon.Images.SetKeyName(19, "");
            this.imlSmallIcon.Images.SetKeyName(20, "");
            this.imlSmallIcon.Images.SetKeyName(21, "");
            this.imlSmallIcon.Images.SetKeyName(22, "");
            this.imlSmallIcon.Images.SetKeyName(23, "");
            this.imlSmallIcon.Images.SetKeyName(24, "");
            this.imlSmallIcon.Images.SetKeyName(25, "");
            this.imlSmallIcon.Images.SetKeyName(26, "");
            this.imlSmallIcon.Images.SetKeyName(27, "");
            this.imlSmallIcon.Images.SetKeyName(28, "");
            this.imlSmallIcon.Images.SetKeyName(29, "");
            this.imlSmallIcon.Images.SetKeyName(30, "");
            this.imlSmallIcon.Images.SetKeyName(31, "");
            this.imlSmallIcon.Images.SetKeyName(32, "");
            this.imlSmallIcon.Images.SetKeyName(33, "");
            this.imlSmallIcon.Images.SetKeyName(34, "");
            this.imlSmallIcon.Images.SetKeyName(35, "");
            this.imlSmallIcon.Images.SetKeyName(36, "");
            this.imlSmallIcon.Images.SetKeyName(37, "");
            this.imlSmallIcon.Images.SetKeyName(38, "");
            this.imlSmallIcon.Images.SetKeyName(39, "");
            this.imlSmallIcon.Images.SetKeyName(40, "");
            this.imlSmallIcon.Images.SetKeyName(41, "");
            this.imlSmallIcon.Images.SetKeyName(42, "");
            this.imlSmallIcon.Images.SetKeyName(43, "");
            this.imlSmallIcon.Images.SetKeyName(44, "");
            this.imlSmallIcon.Images.SetKeyName(45, "");
            this.imlSmallIcon.Images.SetKeyName(46, "");
            this.imlSmallIcon.Images.SetKeyName(47, "");
            this.imlSmallIcon.Images.SetKeyName(48, "");
            this.imlSmallIcon.Images.SetKeyName(49, "");
            this.imlSmallIcon.Images.SetKeyName(50, "");
            this.imlSmallIcon.Images.SetKeyName(51, "");
            this.imlSmallIcon.Images.SetKeyName(52, "");
            this.imlSmallIcon.Images.SetKeyName(53, "");
            this.imlSmallIcon.Images.SetKeyName(54, "");
            this.imlSmallIcon.Images.SetKeyName(55, "");
            this.imlSmallIcon.Images.SetKeyName(56, "");
            this.imlSmallIcon.Images.SetKeyName(57, "");
            this.imlSmallIcon.Images.SetKeyName(58, "");
            this.imlSmallIcon.Images.SetKeyName(59, "");
            this.imlSmallIcon.Images.SetKeyName(60, "");
            this.imlSmallIcon.Images.SetKeyName(61, "");
            this.imlSmallIcon.Images.SetKeyName(62, "");
            this.imlSmallIcon.Images.SetKeyName(63, "");
            this.imlSmallIcon.Images.SetKeyName(64, "");
            this.imlSmallIcon.Images.SetKeyName(65, "");
            this.imlSmallIcon.Images.SetKeyName(66, "");
            this.imlSmallIcon.Images.SetKeyName(67, "");
            this.imlSmallIcon.Images.SetKeyName(68, "");
            this.imlSmallIcon.Images.SetKeyName(69, "");
            this.imlSmallIcon.Images.SetKeyName(70, "");
            this.imlSmallIcon.Images.SetKeyName(71, "");
            this.imlSmallIcon.Images.SetKeyName(72, "");
            this.imlSmallIcon.Images.SetKeyName(73, "");
            this.imlSmallIcon.Images.SetKeyName(74, "");
            this.imlSmallIcon.Images.SetKeyName(75, "");
            this.imlSmallIcon.Images.SetKeyName(76, "");
            this.imlSmallIcon.Images.SetKeyName(77, "");
            this.imlSmallIcon.Images.SetKeyName(78, "");
            this.imlSmallIcon.Images.SetKeyName(79, "");
            this.imlSmallIcon.Images.SetKeyName(80, "");
            this.imlSmallIcon.Images.SetKeyName(81, "");
            this.imlSmallIcon.Images.SetKeyName(82, "");
            this.imlSmallIcon.Images.SetKeyName(83, "");
            this.imlSmallIcon.Images.SetKeyName(84, "");
            this.imlSmallIcon.Images.SetKeyName(85, "");
            this.imlSmallIcon.Images.SetKeyName(86, "");
            this.imlSmallIcon.Images.SetKeyName(87, "");
            this.imlSmallIcon.Images.SetKeyName(88, "");
            this.imlSmallIcon.Images.SetKeyName(89, "");
            this.imlSmallIcon.Images.SetKeyName(90, "");
            this.imlSmallIcon.Images.SetKeyName(91, "");
            this.imlSmallIcon.Images.SetKeyName(92, "");
            this.imlSmallIcon.Images.SetKeyName(93, "");
            this.imlSmallIcon.Images.SetKeyName(94, "");
            this.imlSmallIcon.Images.SetKeyName(95, "");
            this.imlSmallIcon.Images.SetKeyName(96, "");
            this.imlSmallIcon.Images.SetKeyName(97, "");
            this.imlSmallIcon.Images.SetKeyName(98, "");
            this.imlSmallIcon.Images.SetKeyName(99, "");
            this.imlSmallIcon.Images.SetKeyName(100, "");
            this.imlSmallIcon.Images.SetKeyName(101, "");
            this.imlSmallIcon.Images.SetKeyName(102, "");
            this.imlSmallIcon.Images.SetKeyName(103, "");
            this.imlSmallIcon.Images.SetKeyName(104, "White Image");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpPlanMonth);
            this.groupBox1.Controls.Add(this.dtpPlanYear);
            this.groupBox1.Controls.Add(this.lblPlanMonth);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 45);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // dtpPlanMonth
            // 
            this.dtpPlanMonth.CustomFormat = "MM";
            this.dtpPlanMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPlanMonth.Location = new System.Drawing.Point(147, 16);
            this.dtpPlanMonth.Name = "dtpPlanMonth";
            this.dtpPlanMonth.ShowUpDown = true;
            this.dtpPlanMonth.Size = new System.Drawing.Size(39, 21);
            this.dtpPlanMonth.TabIndex = 4;
            this.dtpPlanMonth.ValueChanged += new System.EventHandler(this.dtpPlanMonth_ValueChanged);
            // 
            // dtpPlanYear
            // 
            this.dtpPlanYear.CustomFormat = "yyyy";
            this.dtpPlanYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPlanYear.Location = new System.Drawing.Point(92, 16);
            this.dtpPlanYear.Name = "dtpPlanYear";
            this.dtpPlanYear.ShowUpDown = true;
            this.dtpPlanYear.Size = new System.Drawing.Size(52, 21);
            this.dtpPlanYear.TabIndex = 3;
            this.dtpPlanYear.ValueChanged += new System.EventHandler(this.dtpPlanYear_ValueChanged);
            // 
            // lblPlanMonth
            // 
            this.lblPlanMonth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPlanMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanMonth.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPlanMonth.Location = new System.Drawing.Point(12, 20);
            this.lblPlanMonth.Name = "lblPlanMonth";
            this.lblPlanMonth.Size = new System.Drawing.Size(86, 13);
            this.lblPlanMonth.TabIndex = 2;
            this.lblPlanMonth.Text = "Plan Month";
            this.lblPlanMonth.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpUploadData
            // 
            this.grpUploadData.Controls.Add(this.spdUploadData);
            this.grpUploadData.Controls.Add(this.pnlFile);
            this.grpUploadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUploadData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpUploadData.Location = new System.Drawing.Point(3, 48);
            this.grpUploadData.Name = "grpUploadData";
            this.grpUploadData.Size = new System.Drawing.Size(589, 484);
            this.grpUploadData.TabIndex = 3;
            this.grpUploadData.TabStop = false;
            this.grpUploadData.Text = "Upload Data";
            // 
            // spdUploadData
            // 
            this.spdUploadData.About = "4.0.2001.2005";
            this.spdUploadData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdUploadData.BackColor = System.Drawing.Color.White;
            this.spdUploadData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdUploadData.Location = new System.Drawing.Point(3, 52);
            this.spdUploadData.Name = "spdUploadData";
            this.spdUploadData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdUploadData.RPT_IsPreCellsType = true;
            this.spdUploadData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdUploadData_Sheet1});
            this.spdUploadData.Size = new System.Drawing.Size(583, 429);
            this.spdUploadData.TabIndex = 35;
            // 
            // spdUploadData_Sheet1
            // 
            this.spdUploadData_Sheet1.Reset();
            this.spdUploadData_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdUploadData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdUploadData_Sheet1.ColumnCount = 0;
            this.spdUploadData_Sheet1.RowCount = 0;
            this.spdUploadData_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdUploadData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdUploadData.SetActiveViewport(0, 1, 1);
            // 
            // pnlFile
            // 
            this.pnlFile.Controls.Add(this.txtFilePath);
            this.pnlFile.Controls.Add(this.btnOpenFile);
            this.pnlFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFile.Location = new System.Drawing.Point(3, 17);
            this.pnlFile.Name = "pnlFile";
            this.pnlFile.Size = new System.Drawing.Size(583, 35);
            this.pnlFile.TabIndex = 34;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.txtFilePath.Location = new System.Drawing.Point(17, 6);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(473, 21);
            this.txtFilePath.TabIndex = 5;
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnOpenFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFile.Image = global::Miracom.SmartWeb.UI.Properties.Resources._010dataimp;
            this.btnOpenFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenFile.Location = new System.Drawing.Point(496, 5);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(80, 23);
            this.btnOpenFile.TabIndex = 4;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // grpFunc
            // 
            this.grpFunc.Controls.Add(this.dtpPlanDate);
            this.grpFunc.Controls.Add(this.lblPlanDate);
            this.grpFunc.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFunc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpFunc.Location = new System.Drawing.Point(3, 3);
            this.grpFunc.Name = "grpFunc";
            this.grpFunc.Size = new System.Drawing.Size(589, 45);
            this.grpFunc.TabIndex = 2;
            this.grpFunc.TabStop = false;
            // 
            // dtpPlanDate
            // 
            this.dtpPlanDate.CustomFormat = "yyyyMMdd";
            this.dtpPlanDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPlanDate.Location = new System.Drawing.Point(126, 16);
            this.dtpPlanDate.Name = "dtpPlanDate";
            this.dtpPlanDate.Size = new System.Drawing.Size(90, 21);
            this.dtpPlanDate.TabIndex = 3;
            // 
            // lblPlanDate
            // 
            this.lblPlanDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPlanDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlanDate.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblPlanDate.Location = new System.Drawing.Point(17, 20);
            this.lblPlanDate.Name = "lblPlanDate";
            this.lblPlanDate.Size = new System.Drawing.Size(86, 13);
            this.lblPlanDate.TabIndex = 2;
            this.lblPlanDate.Text = "Plan Date";
            this.lblPlanDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // udcCUSReportUpload001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "udcCUSReportUpload001";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.udcCUSReportUpload001_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grpUploadData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdUploadData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdUploadData_Sheet1)).EndInit();
            this.pnlFile.ResumeLayout(false);
            this.pnlFile.PerformLayout();
            this.grpFunc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        internal Miracom.UI.Controls.MCListView.MCListView lisUploadData;
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.GroupBox grpFunc;
        internal System.Windows.Forms.Label lblPlanDate;
        private System.Windows.Forms.GroupBox grpUploadData;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        protected System.Windows.Forms.Button btnCreate;
        protected System.Windows.Forms.DateTimePicker dtpPlanDate;
        internal System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.DateTimePicker dtpPlanYear;
        internal System.Windows.Forms.Label lblPlanMonth;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Panel pnlFile;
        protected System.Windows.Forms.DateTimePicker dtpPlanMonth;
        protected System.Windows.Forms.ImageList imlSmallIcon;
        private FarPoint.Win.Spread.SheetView spdUploadData_Sheet1;
        protected udcFarPoint spdUploadData;
        protected System.Windows.Forms.Label lblTitle;
    }
}
