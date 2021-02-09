namespace Miracom.SmartWeb.UI
{
    partial class STD1210
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
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement2 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect2 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.UltraChart.Resources.Appearance.View3DAppearance view3DAppearance2 = new Infragistics.UltraChart.Resources.Appearance.View3DAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance2 = new FarPoint.Win.Spread.TipAppearance();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.ultraChart1 = new Infragistics.Win.UltraWinChart.UltraChart();
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cdvMatVer = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optLot = new System.Windows.Forms.RadioButton();
            this.optQty2 = new System.Windows.Forms.RadioButton();
            this.optQty1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvMat = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.txtOperGrp = new System.Windows.Forms.TextBox();
            this.optThisMonth = new System.Windows.Forms.RadioButton();
            this.optThisWeek = new System.Windows.Forms.RadioButton();
            this.optToday = new System.Windows.Forms.RadioButton();
            this.cdvOperGrpList = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.optPeriod = new System.Windows.Forms.RadioButton();
            this.optLastMonth = new System.Windows.Forms.RadioButton();
            this.optLastWeek = new System.Windows.Forms.RadioButton();
            this.optYesterday = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvOperGrp = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblFactory = new System.Windows.Forms.Label();
            this.cdvFactory = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatVer)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperGrpList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperGrp)).BeginInit();
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
            this.label3.Size = new System.Drawing.Size(108, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Operation Movement";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 140F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 168);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.ultraChart1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.spdData);
            this.splitContainer.Size = new System.Drawing.Size(794, 429);
            this.splitContainer.SplitterDistance = 212;
            this.splitContainer.TabIndex = 4;
            // 
            // ultraChart1
            // 
            this.ultraChart1.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement2.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement2.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChart1.Axis.PE = paintElement2;
            this.ultraChart1.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X.LineThickness = 1;
            this.ultraChart1.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X.Visible = true;
            this.ultraChart1.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChart1.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.X2.LineThickness = 1;
            this.ultraChart1.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.X2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.X2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.X2.Visible = false;
            this.ultraChart1.Axis.Y.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChart1.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y.LineThickness = 1;
            this.ultraChart1.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Y.TickmarkInterval = 100;
            this.ultraChart1.Axis.Y.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Y.Visible = true;
            this.ultraChart1.Axis.Y2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChart1.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.ultraChart1.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Y2.LineThickness = 1;
            this.ultraChart1.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Y2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Y2.TickmarkInterval = 100;
            this.ultraChart1.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Y2.Visible = false;
            this.ultraChart1.Axis.Z.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z.Labels.ItemFormatString = "";
            this.ultraChart1.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z.LineThickness = 1;
            this.ultraChart1.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Z.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Z.Visible = false;
            this.ultraChart1.Axis.Z2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChart1.Axis.Z2.Labels.ItemFormatString = "";
            this.ultraChart1.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChart1.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChart1.Axis.Z2.LineThickness = 1;
            this.ultraChart1.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChart1.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z2.MajorGridLines.Visible = true;
            this.ultraChart1.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChart1.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChart1.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChart1.Axis.Z2.MinorGridLines.Visible = false;
            this.ultraChart1.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChart1.Axis.Z2.Visible = false;
            this.ultraChart1.ColorModel.AlphaLevel = ((byte)(150));
            this.ultraChart1.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.ultraChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraChart1.Effects.Effects.Add(gradientEffect2);
            this.ultraChart1.Legend.SpanPercentage = 20;
            this.ultraChart1.Location = new System.Drawing.Point(0, 0);
            this.ultraChart1.Name = "ultraChart1";
            this.ultraChart1.Size = new System.Drawing.Size(794, 212);
            this.ultraChart1.TabIndex = 29;
            this.ultraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            view3DAppearance2.ZRotation = 4F;
            this.ultraChart1.Transform3D = view3DAppearance2;
            this.ultraChart1.Visible = false;
            // 
            // spdData
            // 
            this.spdData.About = "2.5.2008.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(0, 0);
            this.spdData.Name = "spdData";
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 213);
            this.spdData.TabIndex = 29;
            tipAppearance2.BackColor = System.Drawing.SystemColors.Info;
            tipAppearance2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            tipAppearance2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.spdData.TextTipAppearance = tipAppearance2;
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
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.cdvMatVer);
            this.panel1.Controls.Add(this.cdvMat);
            this.panel1.Controls.Add(this.txtOperGrp);
            this.panel1.Controls.Add(this.cdvOperGrpList);
            this.panel1.Controls.Add(this.cdvOperGrp);
            this.panel1.Controls.Add(this.cdvFactory);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.optThisMonth);
            this.panel1.Controls.Add(this.optThisWeek);
            this.panel1.Controls.Add(this.optToday);
            this.panel1.Controls.Add(this.optPeriod);
            this.panel1.Controls.Add(this.optLastMonth);
            this.panel1.Controls.Add(this.optLastWeek);
            this.panel1.Controls.Add(this.optYesterday);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblFactory);
            this.panel1.Controls.Add(this.btnExcelExport);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 140);
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
            this.cdvMatVer.Location = new System.Drawing.Point(262, 37);
            this.cdvMatVer.MaxLength = 32767;
            this.cdvMatVer.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvMatVer.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvMatVer.Name = "cdvMatVer";
            this.cdvMatVer.ReadOnly = false;
            this.cdvMatVer.SearchSubItemIndex = 0;
            this.cdvMatVer.SelectedDescIndex = -1;
            this.cdvMatVer.SelectedSubItemIndex = -1;
            this.cdvMatVer.SelectionStart = 0;
            this.cdvMatVer.Size = new System.Drawing.Size(45, 21);
            this.cdvMatVer.SmallImageList = null;
            this.cdvMatVer.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvMatVer.TabIndex = 37;
            this.cdvMatVer.TextBoxToolTipText = "";
            this.cdvMatVer.TextBoxWidth = 45;
            this.cdvMatVer.VisibleButton = true;
            this.cdvMatVer.VisibleColumnHeader = false;
            this.cdvMatVer.ButtonPress += new System.EventHandler(this.cdvMatVer_ButtonPress);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optLot);
            this.groupBox1.Controls.Add(this.optQty2);
            this.groupBox1.Controls.Add(this.optQty1);
            this.groupBox1.Location = new System.Drawing.Point(409, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 37);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            // 
            // optLot
            // 
            this.optLot.AutoSize = true;
            this.optLot.Checked = true;
            this.optLot.Location = new System.Drawing.Point(7, 13);
            this.optLot.Name = "optLot";
            this.optLot.Size = new System.Drawing.Size(40, 17);
            this.optLot.TabIndex = 33;
            this.optLot.TabStop = true;
            this.optLot.Text = "Lot";
            this.optLot.UseVisualStyleBackColor = true;
            // 
            // optQty2
            // 
            this.optQty2.AutoSize = true;
            this.optQty2.Location = new System.Drawing.Point(96, 13);
            this.optQty2.Name = "optQty2";
            this.optQty2.Size = new System.Drawing.Size(49, 17);
            this.optQty2.TabIndex = 35;
            this.optQty2.Text = "Qty2";
            this.optQty2.UseVisualStyleBackColor = true;
            // 
            // optQty1
            // 
            this.optQty1.AutoSize = true;
            this.optQty1.Location = new System.Drawing.Point(53, 13);
            this.optQty1.Name = "optQty1";
            this.optQty1.Size = new System.Drawing.Size(49, 17);
            this.optQty1.TabIndex = 34;
            this.optQty1.Text = "Qty1";
            this.optQty1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 32;
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
            this.cdvMat.Location = new System.Drawing.Point(110, 37);
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
            this.cdvMat.TabIndex = 31;
            this.cdvMat.TextBoxToolTipText = "";
            this.cdvMat.TextBoxWidth = 150;
            this.cdvMat.VisibleButton = true;
            this.cdvMat.VisibleColumnHeader = false;
            this.cdvMat.TextBoxTextChanged += new System.EventHandler(this.cdvMat_TextBoxTextChanged);
            this.cdvMat.ButtonPress += new System.EventHandler(this.cdvMat_ButtonPress);
            // 
            // txtOperGrp
            // 
            this.txtOperGrp.Location = new System.Drawing.Point(562, 10);
            this.txtOperGrp.Name = "txtOperGrp";
            this.txtOperGrp.Size = new System.Drawing.Size(47, 21);
            this.txtOperGrp.TabIndex = 30;
            this.txtOperGrp.Visible = false;
            // 
            // optThisMonth
            // 
            this.optThisMonth.AutoSize = true;
            this.optThisMonth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optThisMonth.Location = new System.Drawing.Point(256, 111);
            this.optThisMonth.Name = "optThisMonth";
            this.optThisMonth.Size = new System.Drawing.Size(83, 18);
            this.optThisMonth.TabIndex = 29;
            this.optThisMonth.Text = "This Month";
            this.optThisMonth.UseVisualStyleBackColor = true;
            // 
            // optThisWeek
            // 
            this.optThisWeek.AutoSize = true;
            this.optThisWeek.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optThisWeek.Location = new System.Drawing.Point(181, 111);
            this.optThisWeek.Name = "optThisWeek";
            this.optThisWeek.Size = new System.Drawing.Size(80, 18);
            this.optThisWeek.TabIndex = 28;
            this.optThisWeek.Text = "This Week";
            this.optThisWeek.UseVisualStyleBackColor = true;
            // 
            // optToday
            // 
            this.optToday.AutoSize = true;
            this.optToday.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optToday.Location = new System.Drawing.Point(108, 111);
            this.optToday.Name = "optToday";
            this.optToday.Size = new System.Drawing.Size(61, 18);
            this.optToday.TabIndex = 27;
            this.optToday.Text = "Today";
            this.optToday.UseVisualStyleBackColor = true;
            // 
            // cdvOperGrpList
            // 
            this.cdvOperGrpList.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvOperGrpList.BtnToolTipText = "";
            this.cdvOperGrpList.DisplaySubItemIndex = -1;
            this.cdvOperGrpList.DisplayText = "";
            this.cdvOperGrpList.Focusing = null;
            this.cdvOperGrpList.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOperGrpList.Index = 0;
            this.cdvOperGrpList.IsViewBtnImage = false;
            this.cdvOperGrpList.Location = new System.Drawing.Point(410, 37);
            this.cdvOperGrpList.MaxLength = 32767;
            this.cdvOperGrpList.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvOperGrpList.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvOperGrpList.Name = "cdvOperGrpList";
            this.cdvOperGrpList.ReadOnly = false;
            this.cdvOperGrpList.SearchSubItemIndex = 0;
            this.cdvOperGrpList.SelectedDescIndex = -1;
            this.cdvOperGrpList.SelectedSubItemIndex = -1;
            this.cdvOperGrpList.SelectionStart = 0;
            this.cdvOperGrpList.Size = new System.Drawing.Size(150, 21);
            this.cdvOperGrpList.SmallImageList = null;
            this.cdvOperGrpList.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvOperGrpList.TabIndex = 26;
            this.cdvOperGrpList.TextBoxToolTipText = "";
            this.cdvOperGrpList.TextBoxWidth = 150;
            this.cdvOperGrpList.VisibleButton = true;
            this.cdvOperGrpList.VisibleColumnHeader = false;
            this.cdvOperGrpList.ButtonPress += new System.EventHandler(this.cdvOperGrpList_ButtonPress);
            // 
            // optPeriod
            // 
            this.optPeriod.AutoSize = true;
            this.optPeriod.Checked = true;
            this.optPeriod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optPeriod.Location = new System.Drawing.Point(335, 87);
            this.optPeriod.Name = "optPeriod";
            this.optPeriod.Size = new System.Drawing.Size(61, 18);
            this.optPeriod.TabIndex = 25;
            this.optPeriod.TabStop = true;
            this.optPeriod.Text = "Period";
            this.optPeriod.UseVisualStyleBackColor = true;
            this.optPeriod.CheckedChanged += new System.EventHandler(this.optPeriod_CheckedChanged);
            // 
            // optLastMonth
            // 
            this.optLastMonth.AutoSize = true;
            this.optLastMonth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optLastMonth.Location = new System.Drawing.Point(256, 87);
            this.optLastMonth.Name = "optLastMonth";
            this.optLastMonth.Size = new System.Drawing.Size(84, 18);
            this.optLastMonth.TabIndex = 24;
            this.optLastMonth.Text = "Last Month";
            this.optLastMonth.UseVisualStyleBackColor = true;
            // 
            // optLastWeek
            // 
            this.optLastWeek.AutoSize = true;
            this.optLastWeek.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optLastWeek.Location = new System.Drawing.Point(181, 87);
            this.optLastWeek.Name = "optLastWeek";
            this.optLastWeek.Size = new System.Drawing.Size(81, 18);
            this.optLastWeek.TabIndex = 23;
            this.optLastWeek.Text = "Last Week";
            this.optLastWeek.UseVisualStyleBackColor = true;
            // 
            // optYesterday
            // 
            this.optYesterday.AutoSize = true;
            this.optYesterday.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optYesterday.Location = new System.Drawing.Point(108, 87);
            this.optYesterday.Name = "optYesterday";
            this.optYesterday.Size = new System.Drawing.Size(80, 18);
            this.optYesterday.TabIndex = 22;
            this.optYesterday.Text = "Yesterday";
            this.optYesterday.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(310, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Group Value";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(310, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Operation Group";
            // 
            // cdvOperGrp
            // 
            this.cdvOperGrp.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvOperGrp.BtnToolTipText = "";
            this.cdvOperGrp.DisplaySubItemIndex = -1;
            this.cdvOperGrp.DisplayText = "";
            this.cdvOperGrp.Focusing = null;
            this.cdvOperGrp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvOperGrp.Index = 0;
            this.cdvOperGrp.IsViewBtnImage = false;
            this.cdvOperGrp.Location = new System.Drawing.Point(410, 10);
            this.cdvOperGrp.MaxLength = 32767;
            this.cdvOperGrp.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvOperGrp.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvOperGrp.Name = "cdvOperGrp";
            this.cdvOperGrp.ReadOnly = false;
            this.cdvOperGrp.SearchSubItemIndex = 0;
            this.cdvOperGrp.SelectedDescIndex = -1;
            this.cdvOperGrp.SelectedSubItemIndex = -1;
            this.cdvOperGrp.SelectionStart = 0;
            this.cdvOperGrp.Size = new System.Drawing.Size(150, 21);
            this.cdvOperGrp.SmallImageList = null;
            this.cdvOperGrp.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvOperGrp.TabIndex = 20;
            this.cdvOperGrp.TextBoxToolTipText = "";
            this.cdvOperGrp.TextBoxWidth = 150;
            this.cdvOperGrp.VisibleButton = true;
            this.cdvOperGrp.VisibleColumnHeader = false;
            this.cdvOperGrp.TextBoxTextChanged += new System.EventHandler(this.cdvOperGrp_TextBoxTextChanged);
            this.cdvOperGrp.ButtonPress += new System.EventHandler(this.cdvOperGrp_ButtonPress);
            this.cdvOperGrp.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvOperGrp_SelectedItemChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(443, 110);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(90, 21);
            this.dtpToDate.TabIndex = 19;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(336, 110);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(90, 21);
            this.dtpFromDate.TabIndex = 17;
            // 
            // lblFactory
            // 
            this.lblFactory.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFactory.Location = new System.Drawing.Point(12, 14);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(100, 13);
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
            // STD1210
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "STD1210";
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.STD1210_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraChart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatVer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperGrpList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvOperGrp)).EndInit();
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
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvOperGrp;
        private System.Windows.Forms.RadioButton optThisWeek;
        private System.Windows.Forms.RadioButton optToday;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvOperGrpList;
        private System.Windows.Forms.RadioButton optPeriod;
        private System.Windows.Forms.RadioButton optLastMonth;
        private System.Windows.Forms.RadioButton optLastWeek;
        private System.Windows.Forms.RadioButton optYesterday;
        private System.Windows.Forms.RadioButton optThisMonth;
        private System.Windows.Forms.TextBox txtOperGrp;
        private System.Windows.Forms.Label label2;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMat;
        private System.Windows.Forms.RadioButton optQty2;
        private System.Windows.Forms.RadioButton optQty1;
        private System.Windows.Forms.RadioButton optLot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChart1;
        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatVer;
        private System.Windows.Forms.Label label4;

    }
}
