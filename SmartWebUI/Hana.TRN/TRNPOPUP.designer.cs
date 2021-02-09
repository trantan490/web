namespace Hana.TRN
{
    partial class TRNPOPUP
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
            this.SS = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.SS_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnExit = new System.Windows.Forms.Button();
            this.bntExcelExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS_Sheet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SS
            // 
            this.SS.About = "4.0.2001.2005";
            this.SS.AccessibleDescription = "";
            this.SS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SS.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.SS.Location = new System.Drawing.Point(0, 0);
            this.SS.Name = "SS";
            this.SS.RPT_IsPreCellsType = true;
            this.SS.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.SS.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.SS_Sheet1});
            this.SS.Size = new System.Drawing.Size(548, 368);
            this.SS.TabIndex = 1;
            // 
            // SS_Sheet1
            // 
            this.SS_Sheet1.Reset();
            this.SS_Sheet1.SheetName = "Sheet1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.SS);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnExit);
            this.splitContainer1.Panel2.Controls.Add(this.bntExcelExport);
            this.splitContainer1.Size = new System.Drawing.Size(548, 413);
            this.splitContainer1.SplitterDistance = 368;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(461, 10);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Okay";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // bntExcelExport
            // 
            this.bntExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bntExcelExport.Location = new System.Drawing.Point(380, 10);
            this.bntExcelExport.Name = "bntExcelExport";
            this.bntExcelExport.Size = new System.Drawing.Size(75, 23);
            this.bntExcelExport.TabIndex = 0;
            this.bntExcelExport.Text = "Save";
            this.bntExcelExport.UseVisualStyleBackColor = true;
            this.bntExcelExport.Click += new System.EventHandler(this.bntExcelExport_Click);
            // 
            // TRNPOPUP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 413);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TRNPOPUP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Popup Windows";
            ((System.ComponentModel.ISupportInitialize)(this.SS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SS_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint SS;
        private FarPoint.Win.Spread.SheetView SS_Sheet1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button bntExcelExport;
    }
}