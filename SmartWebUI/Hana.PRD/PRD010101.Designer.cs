namespace Hana.PRD
{
    partial class PRD010101
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
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("ko-KR", false);
            FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType4 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType5 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType6 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType7 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType8 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType9 = new FarPoint.Win.Spread.CellType.TextCellType();
            FarPoint.Win.Spread.CellType.TextCellType textCellType10 = new FarPoint.Win.Spread.CellType.TextCellType();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010101));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.spdAssy = new FarPoint.Win.Spread.FpSpread();
            this.spdAssy_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.spdTest = new FarPoint.Win.Spread.FpSpread();
            this.spdTest_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlInformation = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cdvDate = new Miracom.SmartWeb.UI.Controls.udcDateTimePicker(this.components);
            this.cdvFactory = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlTitle.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdAssy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdAssy_Sheet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdTest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdTest_Sheet1)).BeginInit();
            this.pnlInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Size = new System.Drawing.Size(75, 13);
            this.lblSubTitle.Text = "Production weather map";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.splMain);
            this.pnlMain.Controls.Add(this.pnlInformation);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 26);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(800, 574);
            this.pnlMain.TabIndex = 8;
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.IsSplitterFixed = true;
            this.splMain.Location = new System.Drawing.Point(0, 55);
            this.splMain.Margin = new System.Windows.Forms.Padding(0);
            this.splMain.Name = "splMain";
            this.splMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.AutoScroll = true;
            this.splMain.Panel1.Controls.Add(this.spdAssy);
            this.splMain.Panel1MinSize = 250;
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.AutoScroll = true;
            this.splMain.Panel2.Controls.Add(this.spdTest);
            this.splMain.Panel2MinSize = 20;
            this.splMain.Size = new System.Drawing.Size(800, 519);
            this.splMain.SplitterDistance = 250;
            this.splMain.SplitterWidth = 2;
            this.splMain.TabIndex = 1;
            // 
            // spdAssy
            // 
            this.spdAssy.About = "4.0.2001.2005";
            this.spdAssy.AccessibleDescription = "spdAssembly, Sheet1, Row 0, Column 0, Samsung (LSI)";
            this.spdAssy.BackColor = System.Drawing.Color.White;
            this.spdAssy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdAssy.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.spdAssy.Location = new System.Drawing.Point(0, 0);
            this.spdAssy.Name = "spdAssy";
            this.spdAssy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdAssy.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdAssy_Sheet1});
            this.spdAssy.Size = new System.Drawing.Size(800, 250);
            this.spdAssy.TabIndex = 0;
            this.spdAssy.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // spdAssy_Sheet1
            // 
            this.spdAssy_Sheet1.Reset();
            this.spdAssy_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdAssy_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdAssy_Sheet1.ColumnCount = 6;
            this.spdAssy_Sheet1.ColumnHeader.RowCount = 2;
            this.spdAssy_Sheet1.RowCount = 7;
            this.spdAssy_Sheet1.Cells.Get(0, 0).Value = "Samsung (LSI)";
            this.spdAssy_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(0, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.spdAssy_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.spdAssy_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.spdAssy_Sheet1.Cells.Get(0, 1).ParseFormatString = "n";
            this.spdAssy_Sheet1.Cells.Get(0, 1).Value = "3.0";
            this.spdAssy_Sheet1.Cells.Get(0, 2).Value = "3.2";
            this.spdAssy_Sheet1.Cells.Get(0, 3).Value = "94%";
            this.spdAssy_Sheet1.Cells.Get(0, 5).Value = "3.2";
            this.spdAssy_Sheet1.Cells.Get(1, 0).Value = "IML";
            this.spdAssy_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(1, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.spdAssy_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.spdAssy_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.spdAssy_Sheet1.Cells.Get(1, 1).ParseFormatString = "n";
            this.spdAssy_Sheet1.Cells.Get(1, 1).Value = "3.0";
            this.spdAssy_Sheet1.Cells.Get(1, 2).Value = "3.6";
            this.spdAssy_Sheet1.Cells.Get(1, 3).Value = "83%";
            this.spdAssy_Sheet1.Cells.Get(1, 5).Value = "3.6";
            this.spdAssy_Sheet1.Cells.Get(2, 0).Value = "Others (LSI)";
            this.spdAssy_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(2, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.spdAssy_Sheet1.Cells.Get(2, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.spdAssy_Sheet1.Cells.Get(2, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.spdAssy_Sheet1.Cells.Get(2, 1).ParseFormatString = "n";
            this.spdAssy_Sheet1.Cells.Get(2, 1).Value = "3.0";
            this.spdAssy_Sheet1.Cells.Get(2, 2).Value = "3.0";
            this.spdAssy_Sheet1.Cells.Get(2, 3).Value = "100%";
            this.spdAssy_Sheet1.Cells.Get(2, 5).Value = "3.0";
            this.spdAssy_Sheet1.Cells.Get(3, 0).Tag = "";
            this.spdAssy_Sheet1.Cells.Get(3, 0).Value = "Samsung (M)";
            this.spdAssy_Sheet1.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(3, 1).Value = "2.0";
            this.spdAssy_Sheet1.Cells.Get(3, 2).Value = "2.1";
            this.spdAssy_Sheet1.Cells.Get(3, 3).Value = "95%";
            this.spdAssy_Sheet1.Cells.Get(3, 5).Value = "2.1";
            this.spdAssy_Sheet1.Cells.Get(4, 0).Tag = "";
            this.spdAssy_Sheet1.Cells.Get(4, 0).Value = "HYNIX";
            this.spdAssy_Sheet1.Cells.Get(4, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(4, 1).Value = "2.0";
            this.spdAssy_Sheet1.Cells.Get(4, 2).Value = "2.2";
            this.spdAssy_Sheet1.Cells.Get(4, 3).Value = "91%";
            this.spdAssy_Sheet1.Cells.Get(4, 5).Value = "2.2";
            this.spdAssy_Sheet1.Cells.Get(5, 0).Value = "FBGA";
            this.spdAssy_Sheet1.Cells.Get(5, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(5, 1).Value = "5.0";
            this.spdAssy_Sheet1.Cells.Get(5, 2).Value = "3.5";
            this.spdAssy_Sheet1.Cells.Get(5, 3).Value = "143%";
            this.spdAssy_Sheet1.Cells.Get(5, 5).Value = "3.5";
            this.spdAssy_Sheet1.Cells.Get(6, 0).Value = "SAWN";
            this.spdAssy_Sheet1.Cells.Get(6, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Cells.Get(6, 1).Value = "1.0";
            this.spdAssy_Sheet1.Cells.Get(6, 2).Value = "1.3";
            this.spdAssy_Sheet1.Cells.Get(6, 3).Value = "77%";
            this.spdAssy_Sheet1.Cells.Get(6, 5).Value = "1.3";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2;
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "구 분";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(0, 1).ColumnSpan = 5;
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Assembly";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "Target";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(1, 2).Value = "TAT cumulative";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "Achievement";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "Weather map";
            this.spdAssy_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "Yesterday TAT";
            this.spdAssy_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.Gainsboro;
            this.spdAssy_Sheet1.Columns.Get(0).Width = 92F;
            this.spdAssy_Sheet1.Columns.Get(1).CellType = textCellType1;
            this.spdAssy_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(1).Label = "Target";
            this.spdAssy_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(2).CellType = textCellType2;
            this.spdAssy_Sheet1.Columns.Get(2).ForeColor = System.Drawing.Color.Blue;
            this.spdAssy_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(2).Label = "TAT cumulative";
            this.spdAssy_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(3).CellType = textCellType3;
            this.spdAssy_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(3).Label = "Achievement";
            this.spdAssy_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(4).CellType = textCellType4;
            this.spdAssy_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(4).Label = "Weather map";
            this.spdAssy_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(5).CellType = textCellType5;
            this.spdAssy_Sheet1.Columns.Get(5).ForeColor = System.Drawing.Color.Blue;
            this.spdAssy_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdAssy_Sheet1.Columns.Get(5).Label = "Yesterday TAT";
            this.spdAssy_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdAssy_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.spdAssy_Sheet1.RowHeader.Visible = false;
            this.spdAssy_Sheet1.Rows.Get(0).Height = 40F;
            this.spdAssy_Sheet1.Rows.Get(1).Height = 40F;
            this.spdAssy_Sheet1.Rows.Get(2).Height = 40F;
            this.spdAssy_Sheet1.Rows.Get(3).Height = 40F;
            this.spdAssy_Sheet1.Rows.Get(4).Height = 40F;
            this.spdAssy_Sheet1.Rows.Get(5).Height = 40F;
            this.spdAssy_Sheet1.Rows.Get(6).Height = 40F;
            this.spdAssy_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // spdTest
            // 
            this.spdTest.About = "4.0.2001.2005";
            this.spdTest.AccessibleDescription = "spdTest, Sheet1, Row 0, Column 0, Samsung (LSI)";
            this.spdTest.BackColor = System.Drawing.Color.White;
            this.spdTest.Dock = System.Windows.Forms.DockStyle.Top;
            this.spdTest.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.spdTest.Location = new System.Drawing.Point(0, 0);
            this.spdTest.Name = "spdTest";
            this.spdTest.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.spdTest.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdTest_Sheet1});
            this.spdTest.Size = new System.Drawing.Size(800, 264);
            this.spdTest.TabIndex = 3;
            this.spdTest.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            // 
            // spdTest_Sheet1
            // 
            this.spdTest_Sheet1.Reset();
            this.spdTest_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdTest_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdTest_Sheet1.ColumnCount = 6;
            this.spdTest_Sheet1.ColumnHeader.RowCount = 2;
            this.spdTest_Sheet1.RowCount = 3;
            this.spdTest_Sheet1.Cells.Get(0, 0).Value = "Samsung (LSI)";
            this.spdTest_Sheet1.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Cells.Get(0, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.spdTest_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.spdTest_Sheet1.Cells.Get(0, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.spdTest_Sheet1.Cells.Get(0, 1).ParseFormatString = "n";
            this.spdTest_Sheet1.Cells.Get(0, 1).Value = "2.0";
            this.spdTest_Sheet1.Cells.Get(0, 2).Value = "2.2";
            this.spdTest_Sheet1.Cells.Get(0, 3).Value = "91%";
            this.spdTest_Sheet1.Cells.Get(0, 5).Value = "2.2";
            this.spdTest_Sheet1.Cells.Get(1, 0).Value = "IML";
            this.spdTest_Sheet1.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Cells.Get(1, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.spdTest_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.spdTest_Sheet1.Cells.Get(1, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.spdTest_Sheet1.Cells.Get(1, 1).ParseFormatString = "n";
            this.spdTest_Sheet1.Cells.Get(1, 1).Value = "2.0";
            this.spdTest_Sheet1.Cells.Get(1, 2).Value = "2.5";
            this.spdTest_Sheet1.Cells.Get(1, 3).Value = "80%";
            this.spdTest_Sheet1.Cells.Get(1, 5).Value = "2.5";
            this.spdTest_Sheet1.Cells.Get(2, 0).Value = "Others (LSI)";
            this.spdTest_Sheet1.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Cells.Get(2, 1).ParseFormatInfo = ((System.Globalization.NumberFormatInfo)(cultureInfo.NumberFormat.Clone()));
            ((System.Globalization.NumberFormatInfo)(this.spdTest_Sheet1.Cells.Get(2, 1).ParseFormatInfo)).NumberDecimalDigits = 0;
            ((System.Globalization.NumberFormatInfo)(this.spdTest_Sheet1.Cells.Get(2, 1).ParseFormatInfo)).NumberGroupSizes = new int[] {
        0};
            this.spdTest_Sheet1.Cells.Get(2, 1).ParseFormatString = "n";
            this.spdTest_Sheet1.Cells.Get(2, 1).Value = "2.0";
            this.spdTest_Sheet1.Cells.Get(2, 2).Value = "1.8";
            this.spdTest_Sheet1.Cells.Get(2, 3).Value = "1.8%";
            this.spdTest_Sheet1.Cells.Get(2, 5).Value = "1.8";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(0, 0).RowSpan = 2;
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "구 분";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(0, 1).ColumnSpan = 5;
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "Test";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(1, 1).Value = "Target";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(1, 2).Value = "TAT cumulative";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(1, 3).Value = "Achievement";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(1, 4).Value = "Weather map";
            this.spdTest_Sheet1.ColumnHeader.Cells.Get(1, 5).Value = "Yesterday TAT";
            this.spdTest_Sheet1.Columns.Get(0).BackColor = System.Drawing.Color.Gainsboro;
            this.spdTest_Sheet1.Columns.Get(0).Width = 92F;
            this.spdTest_Sheet1.Columns.Get(1).CellType = textCellType6;
            this.spdTest_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(1).Label = "Target";
            this.spdTest_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(2).CellType = textCellType7;
            this.spdTest_Sheet1.Columns.Get(2).ForeColor = System.Drawing.Color.Maroon;
            this.spdTest_Sheet1.Columns.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(2).Label = "TAT cumulative";
            this.spdTest_Sheet1.Columns.Get(2).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(3).CellType = textCellType8;
            this.spdTest_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(3).Label = "Achievement";
            this.spdTest_Sheet1.Columns.Get(3).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(4).CellType = textCellType9;
            this.spdTest_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(4).Label = "Weather map";
            this.spdTest_Sheet1.Columns.Get(4).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(5).CellType = textCellType10;
            this.spdTest_Sheet1.Columns.Get(5).ForeColor = System.Drawing.Color.Maroon;
            this.spdTest_Sheet1.Columns.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.spdTest_Sheet1.Columns.Get(5).Label = "Yesterday TAT";
            this.spdTest_Sheet1.Columns.Get(5).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.spdTest_Sheet1.RowHeader.Columns.Default.Resizable = true;
            this.spdTest_Sheet1.RowHeader.Visible = false;
            this.spdTest_Sheet1.Rows.Get(0).Height = 40F;
            this.spdTest_Sheet1.Rows.Get(1).Height = 40F;
            this.spdTest_Sheet1.Rows.Get(2).Height = 40F;
            this.spdTest_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // pnlInformation
            // 
            this.pnlInformation.Controls.Add(this.lblIcon);
            this.pnlInformation.Controls.Add(this.lblDate);
            this.pnlInformation.Controls.Add(this.cdvDate);
            this.pnlInformation.Controls.Add(this.cdvFactory);
            this.pnlInformation.Controls.Add(this.pictureBox1);
            this.pnlInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInformation.Location = new System.Drawing.Point(0, 0);
            this.pnlInformation.Name = "pnlInformation";
            this.pnlInformation.Size = new System.Drawing.Size(800, 55);
            this.pnlInformation.TabIndex = 0;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(264, 20);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 21;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(274, 22);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(55, 13);
            this.lblDate.TabIndex = 20;
            this.lblDate.Text = "standard date";
            // 
            // cdvDate
            // 
            this.cdvDate.CustomFormat = "yyyy-MM-dd";
            this.cdvDate.Font = new System.Drawing.Font("Verdana", 8F);
            this.cdvDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdvDate.Location = new System.Drawing.Point(342, 17);
            this.cdvDate.Name = "cdvDate";
            this.cdvDate.Size = new System.Drawing.Size(106, 20);
            this.cdvDate.TabIndex = 19;
            this.cdvDate.Type = Miracom.SmartWeb.UI.Controls.DataTimePickerType.General;
            this.cdvDate.Value = new System.DateTime(2008, 12, 31, 0, 0, 0, 0);
            // 
            // cdvFactory
            // 
            this.cdvFactory.BackColor = System.Drawing.Color.White;
            this.cdvFactory.bMultiSelect = true;
            this.cdvFactory.ConditionText = "Factory";
            this.cdvFactory.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.cdvFactory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.cdvFactory.Location = new System.Drawing.Point(26, 14);
            this.cdvFactory.MandatoryFlag = false;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ParentValue = "";
            this.cdvFactory.sCodeColumnName = "";
            this.cdvFactory.sDynamicQuery = "";
            this.cdvFactory.sFactory = "";
            this.cdvFactory.Size = new System.Drawing.Size(210, 21);
            this.cdvFactory.sTableName = "";
            this.cdvFactory.sValueColumnName = "";
            this.cdvFactory.TabIndex = 1;
            this.cdvFactory.VisibleValueButton = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = global::Hana.PRD.Properties.Resources.WeatherLegend;
            this.pictureBox1.Location = new System.Drawing.Point(588, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(208, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // PRD010101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.pnlMain);
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PRD010101";
            this.Load += new System.EventHandler(this.PRD010101_Load);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            this.splMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdAssy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdAssy_Sheet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdTest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdTest_Sheet1)).EndInit();
            this.pnlInformation.ResumeLayout(false);
            this.pnlInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.SplitContainer splMain;
        private FarPoint.Win.Spread.FpSpread spdAssy;
        private FarPoint.Win.Spread.SheetView spdAssy_Sheet1;
        private System.Windows.Forms.Panel pnlInformation;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FarPoint.Win.Spread.FpSpread spdTest;
        private FarPoint.Win.Spread.SheetView spdTest_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcCUSCondition cdvFactory;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblDate;
        private Miracom.SmartWeb.UI.Controls.udcDateTimePicker cdvDate;
    }
}
