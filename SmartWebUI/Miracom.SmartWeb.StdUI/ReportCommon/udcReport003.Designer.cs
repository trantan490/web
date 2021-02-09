namespace Miracom.SmartWeb.UI.BaseFormControls
{
    partial class udcReport003
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
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect1 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.UltraChart.Resources.Appearance.View3DAppearance view3DAppearance1 = new Infragistics.UltraChart.Resources.Appearance.View3DAppearance();
            FarPoint.Win.Spread.TipAppearance tipAppearance1 = new FarPoint.Win.Spread.TipAppearance();
            this.lblExecution = new System.Windows.Forms.Label();
            this.sptData = new System.Windows.Forms.SplitContainer();
            this.utrChart = new Infragistics.Win.UltraWinChart.UltraChart();
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlCondition8.SuspendLayout();
            this.pnlCondition7.SuspendLayout();
            this.pnlCondition6.SuspendLayout();
            this.pnlCondition5.SuspendLayout();
            this.pnlCondition4.SuspendLayout();
            this.pnlCondition3.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.sptData.Panel1.SuspendLayout();
            this.sptData.Panel2.SuspendLayout();
            this.sptData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.utrChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // btnView
            // 
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            // 
            // lblExecution
            // 
            this.lblExecution.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExecution.Location = new System.Drawing.Point(0, 235);
            this.lblExecution.Name = "lblExecution";
            this.lblExecution.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblExecution.Size = new System.Drawing.Size(800, 14);
            this.lblExecution.TabIndex = 33;
            this.lblExecution.Text = "Execution Time : 0  Data Count : 0";
            // 
            // sptData
            // 
            this.sptData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sptData.Location = new System.Drawing.Point(0, 249);
            this.sptData.Name = "sptData";
            this.sptData.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sptData.Panel1
            // 
            this.sptData.Panel1.Controls.Add(this.utrChart);
            // 
            // sptData.Panel2
            // 
            this.sptData.Panel2.Controls.Add(this.spdData);
            this.sptData.Size = new System.Drawing.Size(800, 351);
            this.sptData.SplitterDistance = 174;
            this.sptData.SplitterWidth = 5;
            this.sptData.TabIndex = 34;
            // 
            // utrChart
            // 
            this.utrChart.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.utrChart.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.utrChart.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.utrChart.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.utrChart.Axis.X.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.utrChart.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.X.LineThickness = 1;
            this.utrChart.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.utrChart.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.X.MajorGridLines.Visible = true;
            this.utrChart.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.utrChart.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.X.MinorGridLines.Visible = false;
            this.utrChart.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.utrChart.Axis.X.Visible = true;
            this.utrChart.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.utrChart.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.utrChart.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.utrChart.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.utrChart.Axis.X2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.utrChart.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.X2.Labels.Visible = false;
            this.utrChart.Axis.X2.LineThickness = 1;
            this.utrChart.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.utrChart.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.X2.MajorGridLines.Visible = true;
            this.utrChart.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.utrChart.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.X2.MinorGridLines.Visible = false;
            this.utrChart.Axis.X2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.utrChart.Axis.X2.Visible = false;
            this.utrChart.Axis.Y.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray;
            this.utrChart.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.utrChart.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.utrChart.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.Y.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.utrChart.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.utrChart.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.utrChart.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.utrChart.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Y.LineThickness = 1;
            this.utrChart.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.utrChart.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Y.MajorGridLines.Visible = true;
            this.utrChart.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.utrChart.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Y.MinorGridLines.Visible = false;
            this.utrChart.Axis.Y.TickmarkInterval = 50;
            this.utrChart.Axis.Y.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.utrChart.Axis.Y.Visible = true;
            this.utrChart.Axis.Y2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray;
            this.utrChart.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.utrChart.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.Y2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.utrChart.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.utrChart.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.utrChart.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Y2.Labels.Visible = false;
            this.utrChart.Axis.Y2.LineThickness = 1;
            this.utrChart.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.utrChart.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Y2.MajorGridLines.Visible = true;
            this.utrChart.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.utrChart.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Y2.MinorGridLines.Visible = false;
            this.utrChart.Axis.Y2.TickmarkInterval = 50;
            this.utrChart.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.utrChart.Axis.Y2.Visible = false;
            this.utrChart.Axis.Z.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray;
            this.utrChart.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.Z.Labels.ItemFormatString = "";
            this.utrChart.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.Z.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.utrChart.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Z.Labels.Visible = false;
            this.utrChart.Axis.Z.LineThickness = 1;
            this.utrChart.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.utrChart.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Z.MajorGridLines.Visible = true;
            this.utrChart.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.utrChart.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Z.MinorGridLines.Visible = false;
            this.utrChart.Axis.Z.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.utrChart.Axis.Z.Visible = false;
            this.utrChart.Axis.Z2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray;
            this.utrChart.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.Z2.Labels.ItemFormatString = "";
            this.utrChart.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.Z2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.utrChart.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.utrChart.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.utrChart.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.utrChart.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.utrChart.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.utrChart.Axis.Z2.Labels.Visible = false;
            this.utrChart.Axis.Z2.LineThickness = 1;
            this.utrChart.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.utrChart.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Z2.MajorGridLines.Visible = true;
            this.utrChart.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.utrChart.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.utrChart.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.utrChart.Axis.Z2.MinorGridLines.Visible = false;
            this.utrChart.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.utrChart.Axis.Z2.Visible = false;
            this.utrChart.ColorModel.AlphaLevel = ((byte)(150));
            this.utrChart.ColorModel.ColorBegin = System.Drawing.Color.DarkOrange;
            this.utrChart.ColorModel.ColorEnd = System.Drawing.Color.LightPink;
            this.utrChart.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.utrChart.ColorModel.Scaling = Infragistics.UltraChart.Shared.Styles.ColorScaling.Increasing;
            this.utrChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.utrChart.Effects.Effects.Add(gradientEffect1);
            this.utrChart.Legend.SpanPercentage = 20;
            this.utrChart.Location = new System.Drawing.Point(0, 0);
            this.utrChart.Name = "utrChart";
            this.utrChart.Size = new System.Drawing.Size(800, 174);
            this.utrChart.TabIndex = 29;
            this.utrChart.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.utrChart.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            this.utrChart.Tooltips.TooltipControl = null;
            view3DAppearance1.ZRotation = 4F;
            this.utrChart.Transform3D = view3DAppearance1;
            this.utrChart.Visible = false;
            // 
            // spdData
            // 
            this.spdData.About = "2.5.2008.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.Black;
            this.spdData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(0, 0);
            this.spdData.Name = "spdData";
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(800, 172);
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
            // udcReport003
            // 
            this.Controls.Add(this.sptData);
            this.Controls.Add(this.lblExecution);
            this.Name = "udcReport003";
            this.Load += new System.EventHandler(this.udcReport003_Load);
            this.ButtonViewClick += new System.EventHandler(this.udcReport003_ButtonViewClick);
            this.ButtonExcelExportClick += new System.EventHandler(this.udcReport003_ButtonExcelExportClick);
            this.Controls.SetChildIndex(this.pnlMiddle, 0);
            this.Controls.SetChildIndex(this.lblExecution, 0);
            this.Controls.SetChildIndex(this.sptData, 0);
            this.pnlCondition8.ResumeLayout(false);
            this.pnlCondition7.ResumeLayout(false);
            this.pnlCondition6.ResumeLayout(false);
            this.pnlCondition5.ResumeLayout(false);
            this.pnlCondition4.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            this.sptData.Panel1.ResumeLayout(false);
            this.sptData.Panel2.ResumeLayout(false);
            this.sptData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.utrChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblExecution;
        private System.Windows.Forms.SplitContainer sptData;
        private Infragistics.Win.UltraWinChart.UltraChart utrChart;
        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
    }
}
