namespace Miracom.SmartWeb.UI
{
    partial class TST1105
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.spdData = new FarPoint.Win.Spread.FpSpread();
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpDelayDate = new System.Windows.Forms.DateTimePicker();
            this.lable1 = new System.Windows.Forms.Label();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.udcFac = new Miracom.SmartWeb.UI.Controls.udcCondition();
            this.udcOper = new Miracom.SmartWeb.UI.Controls.udcCondition();
            this.udcFlow = new Miracom.SmartWeb.UI.Controls.udcCondition();
            this.udcMat = new Miracom.SmartWeb.UI.Controls.udcCondition();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.spdData, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "spdData, Sheet1, Row 0, Column 0, ";
            this.spdData.BackColor = System.Drawing.Color.White;
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Location = new System.Drawing.Point(3, 128);
            this.spdData.Name = "spdData";
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(794, 469);
            this.spdData.TabIndex = 4;
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
            this.spdData.SetActiveViewport(0, 1, 1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 25);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataclose;
            this.btnClose.Location = new System.Drawing.Point(776, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 20);
            this.btnClose.TabIndex = 16;
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
            this.label3.Size = new System.Drawing.Size(94, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Aged Lot Status";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dtpDelayDate);
            this.panel2.Controls.Add(this.lable1);
            this.panel2.Controls.Add(this.btnExcelExport);
            this.panel2.Controls.Add(this.btnView);
            this.panel2.Controls.Add(this.udcFac);
            this.panel2.Controls.Add(this.udcOper);
            this.panel2.Controls.Add(this.udcFlow);
            this.panel2.Controls.Add(this.udcMat);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 100);
            this.panel2.TabIndex = 1;
            // 
            // dtpDelayDate
            // 
            this.dtpDelayDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDelayDate.Location = new System.Drawing.Point(407, 11);
            this.dtpDelayDate.Name = "dtpDelayDate";
            this.dtpDelayDate.Size = new System.Drawing.Size(85, 21);
            this.dtpDelayDate.TabIndex = 48;
            // 
            // lable1
            // 
            this.lable1.AutoSize = true;
            this.lable1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lable1.Location = new System.Drawing.Point(318, 15);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(83, 13);
            this.lable1.TabIndex = 47;
            this.lable1.Text = "Delayed Date";
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelExport.Image = global::Miracom.SmartWeb.UI.Properties.Resources._006excel;
            this.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcelExport.Location = new System.Drawing.Point(720, 32);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(70, 21);
            this.btnExcelExport.TabIndex = 46;
            this.btnExcelExport.Text = "Export";
            this.btnExcelExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcelExport.UseVisualStyleBackColor = true;
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Image = global::Miracom.SmartWeb.UI.Properties.Resources._015view;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(720, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(70, 21);
            this.btnView.TabIndex = 45;
            this.btnView.Text = "View";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // udcFac
            // 
            this.udcFac.BackColor = System.Drawing.Color.White;
            this.udcFac.ConditionText = "Factory";
            this.udcFac.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FACTORY;
            this.udcFac.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcFac.Location = new System.Drawing.Point(6, 11);
            this.udcFac.MandatoryFlag = false;
            this.udcFac.Margin = new System.Windows.Forms.Padding(0);
            this.udcFac.Name = "udcFac";
            this.udcFac.ParentSubValue = "";
            this.udcFac.ParentValue = "";
            this.udcFac.sFactory = "";
            this.udcFac.Size = new System.Drawing.Size(265, 21);
            this.udcFac.TabIndex = 44;
            this.udcFac.VisibleSubValue = false;
            this.udcFac.VisibleSubValueButton = true;
            this.udcFac.VisibleValueButton = true;
            this.udcFac.ValueTextChanged += new System.EventHandler(this.udcFac_ValueTextChanged);
            // 
            // udcOper
            // 
            this.udcOper.BackColor = System.Drawing.Color.White;
            this.udcOper.ConditionText = "Operation";
            this.udcOper.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.OPER;
            this.udcOper.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcOper.Location = new System.Drawing.Point(316, 65);
            this.udcOper.MandatoryFlag = false;
            this.udcOper.Margin = new System.Windows.Forms.Padding(0);
            this.udcOper.Name = "udcOper";
            this.udcOper.ParentSubValue = "";
            this.udcOper.ParentValue = "";
            this.udcOper.sFactory = "";
            this.udcOper.Size = new System.Drawing.Size(265, 21);
            this.udcOper.TabIndex = 43;
            this.udcOper.VisibleSubValue = false;
            this.udcOper.VisibleSubValueButton = true;
            this.udcOper.VisibleValueButton = true;
            // 
            // udcFlow
            // 
            this.udcFlow.BackColor = System.Drawing.Color.White;
            this.udcFlow.ConditionText = "Flow";
            this.udcFlow.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.FLOW;
            this.udcFlow.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcFlow.Location = new System.Drawing.Point(6, 66);
            this.udcFlow.MandatoryFlag = false;
            this.udcFlow.Margin = new System.Windows.Forms.Padding(0);
            this.udcFlow.Name = "udcFlow";
            this.udcFlow.ParentSubValue = "";
            this.udcFlow.ParentValue = "";
            this.udcFlow.sFactory = "";
            this.udcFlow.Size = new System.Drawing.Size(265, 21);
            this.udcFlow.TabIndex = 42;
            this.udcFlow.VisibleSubValue = true;
            this.udcFlow.VisibleSubValueButton = true;
            this.udcFlow.VisibleValueButton = true;
            this.udcFlow.ValueTextChanged += new System.EventHandler(this.udcFlow_ValueTextChanged);
            this.udcFlow.SubValueChanged += new System.EventHandler(this.udcFlow_SubValueTextChanged);
            // 
            // udcMat
            // 
            this.udcMat.BackColor = System.Drawing.Color.White;
            this.udcMat.ConditionText = "Material";
            this.udcMat.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.MATERIAL;
            this.udcMat.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcMat.Location = new System.Drawing.Point(6, 39);
            this.udcMat.MandatoryFlag = false;
            this.udcMat.Margin = new System.Windows.Forms.Padding(0);
            this.udcMat.Name = "udcMat";
            this.udcMat.ParentSubValue = "";
            this.udcMat.ParentValue = "test";
            this.udcMat.sFactory = "";
            this.udcMat.Size = new System.Drawing.Size(265, 21);
            this.udcMat.TabIndex = 41;
            this.udcMat.VisibleSubValue = true;
            this.udcMat.VisibleSubValueButton = true;
            this.udcMat.VisibleValueButton = true;
            this.udcMat.ValueTextChanged += new System.EventHandler(this.udcMat_ValueTextChanged);
            this.udcMat.SubValueChanged += new System.EventHandler(this.udcMat_SubValueTextChanged);
            // 
            // TST1105
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "TST1105";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.TST1105_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private Miracom.SmartWeb.UI.Controls.udcCondition udcMat;
        private Miracom.SmartWeb.UI.Controls.udcCondition udcFlow;
        private Miracom.SmartWeb.UI.Controls.udcCondition udcOper;
        private Miracom.SmartWeb.UI.Controls.udcCondition udcFac;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Button btnView;
        private FarPoint.Win.Spread.FpSpread spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.DateTimePicker dtpDelayDate;
        private System.Windows.Forms.Label lable1;
    }
}
