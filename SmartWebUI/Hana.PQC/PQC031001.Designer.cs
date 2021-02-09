namespace Hana.PQC
{
    partial class PQC031001
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splMain = new System.Windows.Forms.SplitContainer();
            this.splLeft = new System.Windows.Forms.SplitContainer();
            this.splStatus = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.udcFarPointStatus = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.udcFarPointStatus_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splList = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.udcFarPointList = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.udcFarPointList_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.udcChartFX1 = new Miracom.SmartWeb.UI.Controls.udcChartFX(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlInformation = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblRefresh = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTimer = new System.Windows.Forms.ComboBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlTitle.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.splMain.Panel1.SuspendLayout();
            this.splMain.Panel2.SuspendLayout();
            this.splMain.SuspendLayout();
            this.splLeft.Panel1.SuspendLayout();
            this.splLeft.Panel2.SuspendLayout();
            this.splLeft.SuspendLayout();
            this.splStatus.Panel1.SuspendLayout();
            this.splStatus.Panel2.SuspendLayout();
            this.splStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointStatus_Sheet1)).BeginInit();
            this.splList.Panel1.SuspendLayout();
            this.splList.Panel2.SuspendLayout();
            this.splList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointList_Sheet1)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Size = new System.Drawing.Size(94, 13);
            this.lblSubTitle.Text = "Oven Monitoring";
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.AutoScrollMinSize = new System.Drawing.Size(1, 1);
            this.pnlMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlMain.Controls.Add(this.splMain);
            this.pnlMain.Controls.Add(this.pnlInformation);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(800, 600);
            this.pnlMain.TabIndex = 7;
            // 
            // splMain
            // 
            this.splMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splMain.Location = new System.Drawing.Point(0, 54);
            this.splMain.Name = "splMain";
            // 
            // splMain.Panel1
            // 
            this.splMain.Panel1.Controls.Add(this.splLeft);
            // 
            // splMain.Panel2
            // 
            this.splMain.Panel2.Controls.Add(this.udcChartFX1);
            this.splMain.Panel2.Controls.Add(this.panel2);
            this.splMain.Size = new System.Drawing.Size(800, 546);
            this.splMain.SplitterDistance = 263;
            this.splMain.SplitterWidth = 2;
            this.splMain.TabIndex = 1;
            // 
            // splLeft
            // 
            this.splLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splLeft.Location = new System.Drawing.Point(0, 0);
            this.splLeft.Name = "splLeft";
            this.splLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splLeft.Panel1
            // 
            this.splLeft.Panel1.Controls.Add(this.splStatus);
            // 
            // splLeft.Panel2
            // 
            this.splLeft.Panel2.Controls.Add(this.splList);
            this.splLeft.Size = new System.Drawing.Size(263, 546);
            this.splLeft.SplitterDistance = 273;
            this.splLeft.SplitterWidth = 2;
            this.splLeft.TabIndex = 0;
            // 
            // splStatus
            // 
            this.splStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splStatus.Location = new System.Drawing.Point(0, 0);
            this.splStatus.Name = "splStatus";
            this.splStatus.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splStatus.Panel1
            // 
            this.splStatus.Panel1.Controls.Add(this.label1);
            // 
            // splStatus.Panel2
            // 
            this.splStatus.Panel2.Controls.Add(this.udcFarPointStatus);
            this.splStatus.Size = new System.Drawing.Size(263, 273);
            this.splStatus.SplitterDistance = 25;
            this.splStatus.SplitterWidth = 2;
            this.splStatus.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Oven Status Status";
            // 
            // udcFarPointStatus
            // 
            this.udcFarPointStatus.About = "4.0.2001.2005";
            this.udcFarPointStatus.AccessibleDescription = "";
            this.udcFarPointStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.udcFarPointStatus.Location = new System.Drawing.Point(0, 0);
            this.udcFarPointStatus.Name = "udcFarPointStatus";
            this.udcFarPointStatus.RPT_IsPreCellsType = true;
            this.udcFarPointStatus.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.udcFarPointStatus_Sheet1});
            this.udcFarPointStatus.Size = new System.Drawing.Size(263, 246);
            this.udcFarPointStatus.TabIndex = 0;
            this.udcFarPointStatus.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.udcFarPointStatus_CellClick);
            // 
            // udcFarPointStatus_Sheet1
            // 
            this.udcFarPointStatus_Sheet1.Reset();
            this.udcFarPointStatus_Sheet1.SheetName = "Sheet1";
            // 
            // splList
            // 
            this.splList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splList.Location = new System.Drawing.Point(0, 0);
            this.splList.Name = "splList";
            this.splList.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splList.Panel1
            // 
            this.splList.Panel1.Controls.Add(this.label2);
            // 
            // splList.Panel2
            // 
            this.splList.Panel2.Controls.Add(this.udcFarPointList);
            this.splList.Size = new System.Drawing.Size(263, 271);
            this.splList.SplitterDistance = 25;
            this.splList.SplitterWidth = 2;
            this.splList.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "LotList";
            // 
            // udcFarPointList
            // 
            this.udcFarPointList.About = "4.0.2001.2005";
            this.udcFarPointList.AccessibleDescription = "";
            this.udcFarPointList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcFarPointList.Location = new System.Drawing.Point(0, 0);
            this.udcFarPointList.Name = "udcFarPointList";
            this.udcFarPointList.RPT_IsPreCellsType = true;
            this.udcFarPointList.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.udcFarPointList_Sheet1});
            this.udcFarPointList.Size = new System.Drawing.Size(263, 244);
            this.udcFarPointList.TabIndex = 1;
            // 
            // udcFarPointList_Sheet1
            // 
            this.udcFarPointList_Sheet1.Reset();
            this.udcFarPointList_Sheet1.SheetName = "Sheet1";
            // 
            // udcChartFX1
            // 
            this.udcChartFX1.AutoSize = true;
            this.udcChartFX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.udcChartFX1.Location = new System.Drawing.Point(0, 25);
            this.udcChartFX1.Name = "udcChartFX1";
            this.udcChartFX1.NValues = 15;
            this.udcChartFX1.Size = new System.Drawing.Size(535, 521);
            this.udcChartFX1.TabIndex = 33;
            this.udcChartFX1.DoubleClick += new SoftwareFX.ChartFX.MouseEventHandlerX(this.udcChartFX1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 25);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Oven number";
            // 
            // pnlInformation
            // 
            this.pnlInformation.Controls.Add(this.btnRefresh);
            this.pnlInformation.Controls.Add(this.lblRefresh);
            this.pnlInformation.Controls.Add(this.label7);
            this.pnlInformation.Controls.Add(this.cboTimer);
            this.pnlInformation.Controls.Add(this.radioButton3);
            this.pnlInformation.Controls.Add(this.radioButton2);
            this.pnlInformation.Controls.Add(this.radioButton1);
            this.pnlInformation.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInformation.Location = new System.Drawing.Point(0, 0);
            this.pnlInformation.Name = "pnlInformation";
            this.pnlInformation.Size = new System.Drawing.Size(800, 54);
            this.pnlInformation.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnRefresh.Location = new System.Drawing.Point(727, 29);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 21);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblRefresh
            // 
            this.lblRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefresh.AutoSize = true;
            this.lblRefresh.Location = new System.Drawing.Point(428, 32);
            this.lblRefresh.Name = "lblRefresh";
            this.lblRefresh.Size = new System.Drawing.Size(142, 13);
            this.lblRefresh.TabIndex = 74;
            this.lblRefresh.Text = "Refresh time remaining: 300 seconds";
            this.lblRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 33);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Refresh Time";
            // 
            // cboTimer
            // 
            this.cboTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTimer.FormattingEnabled = true;
            this.cboTimer.Items.AddRange(new object[] {
            "1 minute",
            "3 minute",
            "5 minute",
            "10 minute"});
            this.cboTimer.Location = new System.Drawing.Point(650, 29);
            this.cboTimer.Name = "cboTimer";
            this.cboTimer.Size = new System.Drawing.Size(70, 21);
            this.cboTimer.TabIndex = 3;
            this.cboTimer.Text = "5 minute";
            this.cboTimer.SelectedIndexChanged += new System.EventHandler(this.btnRefresh_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(183, 26);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(87, 17);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Visible = false;
            this.radioButton3.Click += new System.EventHandler(this.RadioBtnClick);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(96, 26);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(87, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Visible = false;
            this.radioButton2.Click += new System.EventHandler(this.RadioBtnClick);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(10, 26);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(87, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.Click += new System.EventHandler(this.RadioBtnClick);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PQC031001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlMain);
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "PQC031001";
            this.Load += new System.EventHandler(this.PQC0000_Load);
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.Controls.SetChildIndex(this.pnlTitle, 0);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.splMain.Panel1.ResumeLayout(false);
            this.splMain.Panel2.ResumeLayout(false);
            this.splMain.Panel2.PerformLayout();
            this.splMain.ResumeLayout(false);
            this.splLeft.Panel1.ResumeLayout(false);
            this.splLeft.Panel2.ResumeLayout(false);
            this.splLeft.ResumeLayout(false);
            this.splStatus.Panel1.ResumeLayout(false);
            this.splStatus.Panel1.PerformLayout();
            this.splStatus.Panel2.ResumeLayout(false);
            this.splStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointStatus_Sheet1)).EndInit();
            this.splList.Panel1.ResumeLayout(false);
            this.splList.Panel1.PerformLayout();
            this.splList.Panel2.ResumeLayout(false);
            this.splList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.udcFarPointList_Sheet1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlInformation.ResumeLayout(false);
            this.pnlInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlInformation;
        private System.Windows.Forms.SplitContainer splMain;
        private System.Windows.Forms.SplitContainer splLeft;
        private System.Windows.Forms.SplitContainer splStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splList;
        private System.Windows.Forms.Label label2;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint udcFarPointStatus;
        private FarPoint.Win.Spread.SheetView udcFarPointStatus_Sheet1;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint udcFarPointList;
        private FarPoint.Win.Spread.SheetView udcFarPointList_Sheet1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ComboBox cboTimer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblRefresh;
        private Miracom.SmartWeb.UI.Controls.udcChartFX udcChartFX1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefresh;
    }
}

