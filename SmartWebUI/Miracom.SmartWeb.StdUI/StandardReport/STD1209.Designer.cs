namespace Miracom.SmartWeb.UI
{
    partial class STD1209
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
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect1 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.UltraChart.Resources.Appearance.View3DAppearance view3DAppearance1 = new Infragistics.UltraChart.Resources.Appearance.View3DAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.ultraChart1 = new Infragistics.Win.UltraWinChart.UltraChart();
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.optQty2 = new System.Windows.Forms.RadioButton();
            this.optQty1 = new System.Windows.Forms.RadioButton();
            this.optLot = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.cdvOper = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.txtMatGrp = new System.Windows.Forms.TextBox();
            this.cdvMatGrpList = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.label2 = new System.Windows.Forms.Label();
            this.cdvMatType = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cdvMatGrp = new Miracom.UI.Controls.MCCodeView.MCCodeView();
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
            ((System.ComponentModel.ISupportInitialize)(this.cdvOper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatGrpList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatType)).BeginInit();
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
            this.label3.Size = new System.Drawing.Size(161, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Material Group Movement Trend";
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 115F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(3, 143);
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
            this.splitContainer.Size = new System.Drawing.Size(794, 454);
            this.splitContainer.SplitterDistance = 224;
            this.splitContainer.TabIndex = 4;
            // 
            // ultraChart1
            // 
            this.ultraChart1.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement1.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChart1.Axis.PE = paintElement1;
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
            this.ultraChart1.Effects.Effects.Add(gradientEffect1);
            this.ultraChart1.Legend.SpanPercentage = 20;
            this.ultraChart1.Location = new System.Drawing.Point(0, 0);
            this.ultraChart1.Name = "ultraChart1";
            this.ultraChart1.Size = new System.Drawing.Size(794, 224);
            this.ultraChart1.TabIndex = 29;
            this.ultraChart1.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChart1.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            view3DAppearance1.ZRotation = 4F;
            this.ultraChart1.Transform3D = view3DAppearance1;
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
            this.spdData.Size = new System.Drawing.Size(794, 226);
            this.spdData.TabIndex = 29;
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
            this.panel1.Controls.Add(this.dtpToDate);
            this.panel1.Controls.Add(this.dtpFromDate);
            this.panel1.Controls.Add(this.cdvOper);
            this.panel1.Controls.Add(this.cdvMatGrpList);
            this.panel1.Controls.Add(this.cdvMatType);
            this.panel1.Controls.Add(this.cdvMatGrp);
            this.panel1.Controls.Add(this.cdvFactory);
            this.panel1.Controls.Add(this.optQty2);
            this.panel1.Controls.Add(this.optQty1);
            this.panel1.Controls.Add(this.optLot);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtMatGrp);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblFactory);
            this.panel1.Controls.Add(this.btnExcelExport);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 115);
            this.panel1.TabIndex = 0;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(503, 10);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(90, 21);
            this.dtpToDate.TabIndex = 39;
            // 
            // optQty2
            // 
            this.optQty2.AutoSize = true;
            this.optQty2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optQty2.Location = new System.Drawing.Point(215, 90);
            this.optQty2.Name = "optQty2";
            this.optQty2.Size = new System.Drawing.Size(55, 18);
            this.optQty2.TabIndex = 38;
            this.optQty2.Text = "Qty2";
            this.optQty2.UseVisualStyleBackColor = true;
            // 
            // optQty1
            // 
            this.optQty1.AutoSize = true;
            this.optQty1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optQty1.Location = new System.Drawing.Point(159, 90);
            this.optQty1.Name = "optQty1";
            this.optQty1.Size = new System.Drawing.Size(55, 18);
            this.optQty1.TabIndex = 37;
            this.optQty1.Text = "Qty1";
            this.optQty1.UseVisualStyleBackColor = true;
            // 
            // optLot
            // 
            this.optLot.AutoSize = true;
            this.optLot.Checked = true;
            this.optLot.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.optLot.Location = new System.Drawing.Point(110, 90);
            this.optLot.Name = "optLot";
            this.optLot.Size = new System.Drawing.Size(46, 18);
            this.optLot.TabIndex = 36;
            this.optLot.TabStop = true;
            this.optLot.Text = "Lot";
            this.optLot.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(310, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 32;
            this.label5.Text = "Period";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(410, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(90, 21);
            this.dtpFromDate.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(12, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
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
            this.cdvOper.Location = new System.Drawing.Point(110, 36);
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
            this.txtMatGrp.Location = new System.Drawing.Point(561, 62);
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
            this.cdvMatGrpList.Location = new System.Drawing.Point(410, 62);
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
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(310, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Material Type";
            // 
            // cdvMatType
            // 
            this.cdvMatType.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvMatType.BtnToolTipText = "";
            this.cdvMatType.DisplaySubItemIndex = -1;
            this.cdvMatType.DisplayText = "";
            this.cdvMatType.Focusing = null;
            this.cdvMatType.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvMatType.Index = 0;
            this.cdvMatType.IsViewBtnImage = false;
            this.cdvMatType.Location = new System.Drawing.Point(410, 36);
            this.cdvMatType.MaxLength = 32767;
            this.cdvMatType.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvMatType.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvMatType.Name = "cdvMatType";
            this.cdvMatType.ReadOnly = false;
            this.cdvMatType.SearchSubItemIndex = 0;
            this.cdvMatType.SelectedDescIndex = -1;
            this.cdvMatType.SelectedSubItemIndex = -1;
            this.cdvMatType.SelectionStart = 0;
            this.cdvMatType.Size = new System.Drawing.Size(150, 21);
            this.cdvMatType.SmallImageList = null;
            this.cdvMatType.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvMatType.TabIndex = 25;
            this.cdvMatType.TextBoxToolTipText = "";
            this.cdvMatType.TextBoxWidth = 150;
            this.cdvMatType.VisibleButton = true;
            this.cdvMatType.VisibleColumnHeader = false;
            this.cdvMatType.ButtonPress += new System.EventHandler(this.cdvMatType_ButtonPress);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(310, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Group Value";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(12, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
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
            this.cdvMatGrp.Location = new System.Drawing.Point(110, 62);
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
            // STD1209
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "STD1209";
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.STD1209_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.cdvOper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatGrpList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvMatType)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatType;
        private System.Windows.Forms.Label label1;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatGrp;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvMatGrpList;
        private System.Windows.Forms.TextBox txtMatGrp;
        private System.Windows.Forms.Label label4;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvOper;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.RadioButton optQty2;
        private System.Windows.Forms.RadioButton optQty1;
        private System.Windows.Forms.RadioButton optLot;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.SplitContainer splitContainer;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChart1;
        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Label label6;

    }
}
