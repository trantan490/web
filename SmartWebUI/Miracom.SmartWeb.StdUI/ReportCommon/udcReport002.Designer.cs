namespace Miracom.SmartWeb.UI.BaseFormControls
{
    partial class udcReport002
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
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.pnlData = new System.Windows.Forms.Panel();
            this.lblExecution = new System.Windows.Forms.Label();
            this.pnlCondition8.SuspendLayout();
            this.pnlCondition7.SuspendLayout();
            this.pnlCondition6.SuspendLayout();
            this.pnlCondition5.SuspendLayout();
            this.pnlCondition4.SuspendLayout();
            this.pnlCondition3.SuspendLayout();
            this.pnlCondition2.SuspendLayout();
            this.pnlCondition1.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.pnlData.SuspendLayout();
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
            // spdData
            // 
            this.spdData.About = "2.5.2008.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.Black;
            this.spdData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(0, 4);
            this.spdData.Name = "spdData";
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(800, 347);
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
            this.spdData_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
            this.spdData_Sheet1.RowHeader.Columns.Default.Resizable = false;
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            this.spdData.SetActiveViewport(1, 1);
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.spdData);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(0, 249);
            this.pnlData.Margin = new System.Windows.Forms.Padding(0);
            this.pnlData.Name = "pnlData";
            this.pnlData.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.pnlData.Size = new System.Drawing.Size(800, 351);
            this.pnlData.TabIndex = 31;
            // 
            // lblExecution
            // 
            this.lblExecution.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExecution.Location = new System.Drawing.Point(0, 235);
            this.lblExecution.Name = "lblExecution";
            this.lblExecution.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblExecution.Size = new System.Drawing.Size(800, 14);
            this.lblExecution.TabIndex = 32;
            this.lblExecution.Text = "Execution Time : 0  Data Count : 0";
            // 
            // udcReport002
            // 
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.lblExecution);
            this.Name = "udcReport002";
            this.Load += new System.EventHandler(this.udcReport002_Load);
            this.ButtonViewClick += new System.EventHandler(this.udcReport002_ButtonViewClick);
            this.ButtonExcelExportClick += new System.EventHandler(this.udcReport002_ButtonExcelExportClick);
            this.Controls.SetChildIndex(this.pnlMiddle, 0);
            this.Controls.SetChildIndex(this.lblExecution, 0);
            this.Controls.SetChildIndex(this.pnlData, 0);
            this.pnlCondition8.ResumeLayout(false);
            this.pnlCondition7.ResumeLayout(false);
            this.pnlCondition6.ResumeLayout(false);
            this.pnlCondition5.ResumeLayout(false);
            this.pnlCondition4.ResumeLayout(false);
            this.pnlCondition3.ResumeLayout(false);
            this.pnlCondition2.ResumeLayout(false);
            this.pnlCondition1.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.pnlData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.Label lblExecution;
    }
}
