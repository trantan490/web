namespace Hana.YLD
{
    partial class YLD041001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YLD041001));
            this.lblFactory = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            //this.spdList = new Miracom.SmartWeb.UI.udcSpread(this.components);
            //this.spdList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            //this.ctsStrip1 = new System.Windows.Forms.CustomToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            //this.spdData = new Miracom.SmartWeb.UI.udcSpread(this.components);
            //this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            //this.ctsStrip2 = new System.Windows.Forms.CustomToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbExcel = new System.Windows.Forms.ToolStripButton();
            this.txtFactory = new System.Windows.Forms.TextBox();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpTimeTo = new System.Windows.Forms.DateTimePicker();
            this.cdvDevice = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.dtpTimeFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvCustomer = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvOperation = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.rdoQty = new System.Windows.Forms.RadioButton();
            this.rdoRate = new System.Windows.Forms.RadioButton();
            this.rdoOper = new System.Windows.Forms.RadioButton();
            this.rdoMMG = new System.Windows.Forms.RadioButton();
            this.txtLotID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveExcel = new System.Windows.Forms.SaveFileDialog();
            this.btnLotListExcel = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.spdList)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.spdList_Sheet1)).BeginInit();
            //this.ctsStrip1.SuspendLayout();
            //((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            //this.ctsStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvDevice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Size = new System.Drawing.Size(800, 62);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splitContainer1);
            this.pnlMain.Location = new System.Drawing.Point(0, 88);
            this.pnlMain.Size = new System.Drawing.Size(800, 512);
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
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlCondition2
            // 
            this.pnlCondition2.Controls.Add(this.cdvCustomer);
            this.pnlCondition2.Controls.Add(this.cdvOperation);
            this.pnlCondition2.Controls.Add(this.rdoOper);
            this.pnlCondition2.Controls.Add(this.rdoMMG);
            this.pnlCondition2.Controls.Add(this.label4);
            this.pnlCondition2.Controls.Add(this.label2);
            this.pnlCondition2.Controls.Add(this.lblDevice);
            this.pnlCondition2.Controls.Add(this.cdvDevice);
            // 
            // pnlCondition1
            // 
            this.pnlCondition1.Controls.Add(this.txtLotID);
            this.pnlCondition1.Controls.Add(this.label5);
            this.pnlCondition1.Controls.Add(this.rdoRate);
            this.pnlCondition1.Controls.Add(this.rdoQty);
            this.pnlCondition1.Controls.Add(this.label3);
            this.pnlCondition1.Controls.Add(this.txtFactory);
            this.pnlCondition1.Controls.Add(this.lblFactory);
            this.pnlCondition1.Controls.Add(this.dtpTimeTo);
            this.pnlCondition1.Controls.Add(this.dtpDateTo);
            this.pnlCondition1.Controls.Add(this.dtpDateFrom);
            this.pnlCondition1.Controls.Add(this.dtpTimeFrom);
            this.pnlCondition1.Controls.Add(this.label1);
            // 
            // btnDelete
            // 
            //this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            //this.btnDelete.FlatAppearance.BorderSize = 0;
            // 
            // btnSave
            // 
            //this.btnSave.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            //this.btnSave.FlatAppearance.BorderSize = 0;
            // 
            // lblFactory
            // 
            this.lblFactory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFactory.Location = new System.Drawing.Point(538, 6);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(90, 13);
            this.lblFactory.TabIndex = 72;
            this.lblFactory.Text = "     Factory";
            this.lblFactory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFactory.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            //this.splitContainer1.Panel1.Controls.Add(this.spdList);
            //this.splitContainer1.Panel1.Controls.Add(this.ctsStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnLotListExcel);
            //this.splitContainer1.Panel2.Controls.Add(this.spdData);
            //this.splitContainer1.Panel2.Controls.Add(this.ctsStrip2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 509);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // spdList
            // 
            //this.spdList.AccessibleDescription = "spdList, Sheet1, Row 0, Column 0, ";
            //this.spdList.BackColor = System.Drawing.Color.White;
            //this.spdList.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.spdList.Location = new System.Drawing.Point(0, 25);
            //this.spdList.Name = "spdList";
            //this.spdList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            //this.spdList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            //this.spdList_Sheet1});
            //this.spdList.Size = new System.Drawing.Size(800, 283);
            //this.spdList.TabIndex = 12;
            //this.spdList.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spdList_CellClick);
            // 
            // spdList_Sheet1
            // 
            //this.spdList_Sheet1.Reset();
            //spdList_Sheet1.SheetName = "Sheet1";
            //// Formulas and custom names must be loaded with R1C1 reference style
            //this.spdList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            //this.spdList_Sheet1.ColumnHeader.Rows.Get(0).Height = 21F;
            //this.spdList_Sheet1.RowHeader.Columns.Default.Resizable = false;
            //this.spdList_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ctsStrip1
            // 
            //this.ctsStrip1.ForeColor = System.Drawing.Color.Black;
            //this.ctsStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            //this.ctsStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //this.toolStripLabel1});
            //this.ctsStrip1.Location = new System.Drawing.Point(0, 0);
            //this.ctsStrip1.Name = "ctsStrip1";
            //this.ctsStrip1.Size = new System.Drawing.Size(800, 25);
            //this.ctsStrip1.TabIndex = 10;
            //this.ctsStrip1.Text = "customToolStrip1";
            //this.ctsStrip1.ToolStripBorder = System.Drawing.Color.Black;
            //this.ctsStrip1.ToolStripContentPanelGradientBegin = System.Drawing.Color.Silver;
            //this.ctsStrip1.ToolStripContentPanelGradientEnd = System.Drawing.Color.LightGray;
            //this.ctsStrip1.ToolStripDropDownBackground = System.Drawing.Color.LightGray;
            //this.ctsStrip1.ToolStripForeColor = System.Drawing.Color.Black;
            //this.ctsStrip1.ToolStripGradientBegin = System.Drawing.Color.Silver;
            //this.ctsStrip1.ToolStripGradientEnd = System.Drawing.Color.White;
            //this.ctsStrip1.ToolStripGradientMiddle = System.Drawing.Color.LightGray;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(155, 22);
            this.toolStripLabel1.Text = "Bin Data Summary Per Lot";
            // 
            // spdData
            // 
            //this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            //this.spdData.BackColor = System.Drawing.Color.White;
            //this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            //this.spdData.Location = new System.Drawing.Point(0, 25);
            //this.spdData.Name = "spdData";
            //this.spdData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            //this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            //this.spdData_Sheet1});
            //this.spdData.Size = new System.Drawing.Size(800, 170);
            //this.spdData.TabIndex = 11;
            // 
            // spdData_Sheet1
            // 
            //this.spdData_Sheet1.Reset();
            //spdData_Sheet1.SheetName = "Sheet1";
            //// Formulas and custom names must be loaded with R1C1 reference style
            //this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            //this.spdData_Sheet1.ColumnHeader.Rows.Get(0).Height = 21F;
            //this.spdData_Sheet1.RowHeader.Columns.Default.Resizable = false;
            //this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // ctsStrip2
            // 
            //this.ctsStrip2.ForeColor = System.Drawing.Color.Black;
            //this.ctsStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            //this.ctsStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            //this.toolStripLabel2,
            //this.tsbExcel});
            //this.ctsStrip2.Location = new System.Drawing.Point(0, 0);
            //this.ctsStrip2.Name = "ctsStrip2";
            //this.ctsStrip2.Size = new System.Drawing.Size(800, 25);
            //this.ctsStrip2.TabIndex = 10;
            //this.ctsStrip2.Text = "customToolStrip2";
            //this.ctsStrip2.ToolStripBorder = System.Drawing.Color.Black;
            //this.ctsStrip2.ToolStripContentPanelGradientBegin = System.Drawing.Color.Silver;
            //this.ctsStrip2.ToolStripContentPanelGradientEnd = System.Drawing.Color.LightGray;
            //this.ctsStrip2.ToolStripDropDownBackground = System.Drawing.Color.LightGray;
            //this.ctsStrip2.ToolStripForeColor = System.Drawing.Color.Black;
            //this.ctsStrip2.ToolStripGradientBegin = System.Drawing.Color.Silver;
            //this.ctsStrip2.ToolStripGradientEnd = System.Drawing.Color.White;
            //this.ctsStrip2.ToolStripGradientMiddle = System.Drawing.Color.LightGray;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(171, 22);
            this.toolStripLabel2.Text = "Bin Data Summary Per Wafer";
            // 
            // tsbExcel
            // 
            this.tsbExcel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExcel.Name = "tsbExcel";
            this.tsbExcel.Size = new System.Drawing.Size(23, 22);
            this.tsbExcel.Text = "Excel";
            // 
            // txtFactory
            // 
            this.txtFactory.BackColor = System.Drawing.Color.White;
            this.txtFactory.Location = new System.Drawing.Point(630, 1);
            this.txtFactory.Name = "txtFactory";
            this.txtFactory.ReadOnly = true;
            this.txtFactory.Size = new System.Drawing.Size(147, 21);
            this.txtFactory.TabIndex = 73;
            this.txtFactory.Visible = false;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.CustomFormat = "yyyy-MM-dd";
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.Location = new System.Drawing.Point(93, 1);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(82, 21);
            this.dtpDateFrom.TabIndex = 0;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.CustomFormat = "yyyy-MM-dd";
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.Location = new System.Drawing.Point(250, 1);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(82, 21);
            this.dtpDateTo.TabIndex = 2;
            // 
            // dtpTimeTo
            // 
            this.dtpTimeTo.CustomFormat = "HH:mm";
            this.dtpTimeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeTo.Location = new System.Drawing.Point(334, 1);
            this.dtpTimeTo.Name = "dtpTimeTo";
            this.dtpTimeTo.ShowUpDown = true;
            this.dtpTimeTo.Size = new System.Drawing.Size(52, 21);
            this.dtpTimeTo.TabIndex = 3;
            this.dtpTimeTo.Value = new System.DateTime(2011, 9, 15, 23, 59, 59, 0);
            // 
            // cdvDevice
            // 
            this.cdvDevice.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvDevice.BorderHotColor = System.Drawing.Color.Black;
            this.cdvDevice.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvDevice.BtnToolTipText = "";
            this.cdvDevice.DescText = "";
            this.cdvDevice.DisplaySubItemIndex = -1;
            this.cdvDevice.DisplayText = "";
            this.cdvDevice.Focusing = null;
            this.cdvDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvDevice.Index = 0;
            this.cdvDevice.IsViewBtnImage = true;
            this.cdvDevice.Location = new System.Drawing.Point(446, 3);
            this.cdvDevice.MaxLength = 32767;
            this.cdvDevice.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvDevice.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
            this.cdvDevice.MultiSelect = false;
            this.cdvDevice.Name = "cdvDevice";
            this.cdvDevice.ReadOnly = false;
            this.cdvDevice.SearchSubItemIndex = 0;
            this.cdvDevice.SelectedDescIndex = -1;
            //this.cdvDevice.SelectedDescToQueryText = "";
            this.cdvDevice.SelectedSubItemIndex = -1;
            this.cdvDevice.SelectedValueToQueryText = "";
            this.cdvDevice.SelectionStart = 0;
            this.cdvDevice.Size = new System.Drawing.Size(130, 21);
            this.cdvDevice.SmallImageList = null;
            this.cdvDevice.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvDevice.TabIndex = 1;
            this.cdvDevice.TextBoxToolTipText = "";
            this.cdvDevice.TextBoxWidth = 130;
            this.cdvDevice.VisibleButton = true;
            this.cdvDevice.VisibleColumnHeader = false;
            this.cdvDevice.VisibleDescription = false;
            this.cdvDevice.ButtonPress += new System.EventHandler(this.cdvDevice_ButtonPress);
            // 
            // dtpTimeFrom
            // 
            this.dtpTimeFrom.CustomFormat = "HH:mm";
            this.dtpTimeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTimeFrom.Location = new System.Drawing.Point(177, 1);
            this.dtpTimeFrom.Name = "dtpTimeFrom";
            this.dtpTimeFrom.ShowUpDown = true;
            this.dtpTimeFrom.Size = new System.Drawing.Size(52, 21);
            this.dtpTimeFrom.TabIndex = 1;
            this.dtpTimeFrom.Value = new System.DateTime(2011, 9, 15, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(232, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "~";
            // 
            // lblDevice
            // 
            this.lblDevice.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblDevice.Location = new System.Drawing.Point(374, 6);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(66, 13);
            this.lblDevice.TabIndex = 75;
            this.lblDevice.Text = "     Device";
            this.lblDevice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(580, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "     Customer";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvCustomer
            // 
            this.cdvCustomer.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvCustomer.BorderHotColor = System.Drawing.Color.Black;
            this.cdvCustomer.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvCustomer.BtnToolTipText = "";
            this.cdvCustomer.DescText = "";
            this.cdvCustomer.DisplaySubItemIndex = -1;
            this.cdvCustomer.DisplayText = "";
            this.cdvCustomer.Focusing = null;
            this.cdvCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvCustomer.Index = 0;
            this.cdvCustomer.IsViewBtnImage = true;
            this.cdvCustomer.Location = new System.Drawing.Point(647, 3);
            this.cdvCustomer.MaxLength = 32767;
            this.cdvCustomer.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvCustomer.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
            this.cdvCustomer.MultiSelect = false;
            this.cdvCustomer.Name = "cdvCustomer";
            this.cdvCustomer.ReadOnly = false;
            this.cdvCustomer.SearchSubItemIndex = 0;
            this.cdvCustomer.SelectedDescIndex = -1;
            //this.cdvCustomer.SelectedDescToQueryText = "";
            this.cdvCustomer.SelectedSubItemIndex = -1;
            this.cdvCustomer.SelectedValueToQueryText = "";
            this.cdvCustomer.SelectionStart = 0;
            this.cdvCustomer.Size = new System.Drawing.Size(130, 21);
            this.cdvCustomer.SmallImageList = null;
            this.cdvCustomer.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvCustomer.TabIndex = 76;
            this.cdvCustomer.TextBoxToolTipText = "";
            this.cdvCustomer.TextBoxWidth = 130;
            this.cdvCustomer.VisibleButton = true;
            this.cdvCustomer.VisibleColumnHeader = false;
            this.cdvCustomer.VisibleDescription = false;
            this.cdvCustomer.ButtonPress += new System.EventHandler(this.cdvCustomer_ButtonPress);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 81;
            this.label3.Text = "     Period";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(198, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "     Oper";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdvOperation
            // 
            this.cdvOperation.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvOperation.BorderHotColor = System.Drawing.Color.Black;
            this.cdvOperation.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvOperation.BtnToolTipText = "";
            this.cdvOperation.DescText = "";
            this.cdvOperation.DisplaySubItemIndex = -1;
            this.cdvOperation.DisplayText = "";
            this.cdvOperation.Focusing = null;
            this.cdvOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvOperation.Index = 0;
            this.cdvOperation.IsViewBtnImage = true;
            this.cdvOperation.Location = new System.Drawing.Point(274, 3);
            this.cdvOperation.MaxLength = 32767;
            this.cdvOperation.MCViewStyle.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvOperation.MCViewStyle.BorderHotColor = System.Drawing.Color.Black;
            this.cdvOperation.MultiSelect = false;
            this.cdvOperation.Name = "cdvOperation";
            this.cdvOperation.ReadOnly = false;
            this.cdvOperation.SearchSubItemIndex = 0;
            this.cdvOperation.SelectedDescIndex = -1;
            //this.cdvOperation.SelectedDescToQueryText = "";
            this.cdvOperation.SelectedSubItemIndex = -1;
            this.cdvOperation.SelectedValueToQueryText = "";
            this.cdvOperation.SelectionStart = 0;
            this.cdvOperation.Size = new System.Drawing.Size(94, 21);
            this.cdvOperation.SmallImageList = null;
            this.cdvOperation.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvOperation.TabIndex = 78;
            this.cdvOperation.TextBoxToolTipText = "";
            this.cdvOperation.TextBoxWidth = 94;
            this.cdvOperation.VisibleButton = true;
            this.cdvOperation.VisibleColumnHeader = false;
            this.cdvOperation.VisibleDescription = false;
            this.cdvOperation.ButtonPress += new System.EventHandler(this.cdvOperation_ButtonPress);
            // 
            // rdoQty
            // 
            this.rdoQty.AutoSize = true;
            this.rdoQty.Checked = true;
            this.rdoQty.Location = new System.Drawing.Point(415, 3);
            this.rdoQty.Name = "rdoQty";
            this.rdoQty.Size = new System.Drawing.Size(49, 17);
            this.rdoQty.TabIndex = 82;
            this.rdoQty.TabStop = true;
            this.rdoQty.Text = "quantity";
            this.rdoQty.UseVisualStyleBackColor = true;
            // 
            // rdoRate
            // 
            this.rdoRate.AutoSize = true;
            this.rdoRate.Location = new System.Drawing.Point(470, 3);
            this.rdoRate.Name = "rdoRate";
            this.rdoRate.Size = new System.Drawing.Size(36, 17);
            this.rdoRate.TabIndex = 83;
            this.rdoRate.Text = "%";
            this.rdoRate.UseVisualStyleBackColor = true;
            // 
            // rdoOper
            // 
            this.rdoOper.AutoSize = true;
            this.rdoOper.Location = new System.Drawing.Point(101, 4);
            this.rdoOper.Name = "rdoOper";
            this.rdoOper.Size = new System.Drawing.Size(73, 17);
            this.rdoOper.TabIndex = 85;
            this.rdoOper.Text = "Operation standard";
            this.rdoOper.UseVisualStyleBackColor = true;
            // 
            // rdoMMG
            // 
            this.rdoMMG.AutoSize = true;
            this.rdoMMG.Checked = true;
            this.rdoMMG.Location = new System.Drawing.Point(16, 4);
            this.rdoMMG.Name = "rdoMMG";
            this.rdoMMG.Size = new System.Drawing.Size(79, 17);
            this.rdoMMG.TabIndex = 84;
            this.rdoMMG.TabStop = true;
            this.rdoMMG.Text = "Merge standard";
            this.rdoMMG.UseVisualStyleBackColor = true;
            // 
            // txtLotID
            // 
            this.txtLotID.BackColor = System.Drawing.Color.White;
            this.txtLotID.Location = new System.Drawing.Point(610, 1);
            this.txtLotID.Name = "txtLotID";
            this.txtLotID.Size = new System.Drawing.Size(147, 21);
            this.txtLotID.TabIndex = 85;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(518, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 84;
            this.label5.Text = "Lot ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // saveExcel
            // 
            this.saveExcel.DefaultExt = "xls";
            this.saveExcel.Filter = "Excel files|*.xls";
            // 
            // btnLotListExcel
            // 
            this.btnLotListExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLotListExcel.BackColor = System.Drawing.SystemColors.Control;
            this.btnLotListExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLotListExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnLotListExcel.Image")));
            this.btnLotListExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnLotListExcel.Location = new System.Drawing.Point(740, 3);
            this.btnLotListExcel.Name = "btnLotListExcel";
            this.btnLotListExcel.Size = new System.Drawing.Size(24, 21);
            this.btnLotListExcel.TabIndex = 150;
            this.btnLotListExcel.UseVisualStyleBackColor = false;
            this.btnLotListExcel.Click += new System.EventHandler(this.btnLotListExcel_Click);
            // 
            // YLD041001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ConditionCount = 2;
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "YLD041001";
            this.Load += new System.EventHandler(this.YLD041001_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition2.PerformLayout();
            this.pnlCondition1.ResumeLayout(false);
            this.pnlCondition1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            //((System.ComponentModel.ISupportInitialize)(this.spdList)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.spdList_Sheet1)).EndInit();
            //this.ctsStrip1.ResumeLayout(false);
            //this.ctsStrip1.PerformLayout();
            //((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            //this.ctsStrip2.ResumeLayout(false);
            //this.ctsStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvDevice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFactory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        //private Miracom.SmartWeb.UI.udcSpread spdData;
        //private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.TextBox txtFactory;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpTimeTo;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvDevice;
        //private Miracom.SmartWeb.UI.udcSpread spdList;
        //private FarPoint.Win.Spread.SheetView spdList_Sheet1;
        private System.Windows.Forms.DateTimePicker dtpTimeFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDevice;
        //private System.Windows.Forms.CustomToolStrip ctsStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        //private System.Windows.Forms.CustomToolStrip ctsStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton tsbExcel;
        private System.Windows.Forms.Label label2;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvOperation;
        private System.Windows.Forms.RadioButton rdoRate;
        private System.Windows.Forms.RadioButton rdoQty;
        private System.Windows.Forms.RadioButton rdoOper;
        private System.Windows.Forms.RadioButton rdoMMG;
        private System.Windows.Forms.TextBox txtLotID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.SaveFileDialog saveExcel;
        protected System.Windows.Forms.Button btnLotListExcel;
    }
}
