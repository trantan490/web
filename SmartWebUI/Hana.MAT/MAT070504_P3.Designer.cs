namespace Hana.MAT
{
    partial class MAT070504_P3
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
            FarPoint.Win.Spread.CellType.EnhancedColumnHeaderRenderer enhancedColumnHeaderRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedColumnHeaderRenderer();
            FarPoint.Win.Spread.CellType.EnhancedRowHeaderRenderer enhancedRowHeaderRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedRowHeaderRenderer();
            FarPoint.Win.Spread.NamedStyle namedStyle5 = new FarPoint.Win.Spread.NamedStyle("Style1");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType2 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.SpreadSkin spreadSkin2 = new FarPoint.Win.Spread.SpreadSkin();
            FarPoint.Win.Spread.NamedStyle namedStyle6 = new FarPoint.Win.Spread.NamedStyle("ColumnHeaderEnhanced");
            FarPoint.Win.Spread.NamedStyle namedStyle7 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer2 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            FarPoint.Win.Spread.EnhancedInterfaceRenderer enhancedInterfaceRenderer2 = new FarPoint.Win.Spread.EnhancedInterfaceRenderer();
            FarPoint.Win.Spread.NamedStyle namedStyle8 = new FarPoint.Win.Spread.NamedStyle("RowHeaderEnhanced");
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer2 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            this.spdData = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdData_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            enhancedColumnHeaderRenderer1.Name = "enhancedColumnHeaderRenderer1";
            enhancedColumnHeaderRenderer1.TextRotationAngle = 0;
            enhancedRowHeaderRenderer1.Name = "enhancedRowHeaderRenderer1";
            enhancedRowHeaderRenderer1.TextRotationAngle = 0;
            // 
            // spdData
            // 
            this.spdData.About = "4.0.2001.2005";
            this.spdData.AccessibleDescription = "";
            this.spdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spdData.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.spdData.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
            this.spdData.Location = new System.Drawing.Point(0, 0);
            this.spdData.Name = "spdData";
            namedStyle5.BackColor = System.Drawing.SystemColors.Window;
            namedStyle5.CellType = generalCellType2;
            namedStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            namedStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle5.Locked = false;
            namedStyle5.Renderer = generalCellType2;
            this.spdData.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle5});
            this.spdData.RPT_IsPreCellsType = true;
            this.spdData.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdData.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdData_Sheet1});
            this.spdData.Size = new System.Drawing.Size(667, 361);
            namedStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle6.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle6.Renderer = enhancedColumnHeaderRenderer1;
            namedStyle6.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spreadSkin2.ColumnHeaderDefaultStyle = namedStyle6;
            namedStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle7.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle7.Renderer = enhancedCornerRenderer2;
            namedStyle7.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spreadSkin2.CornerDefaultStyle = namedStyle7;
            spreadSkin2.DefaultStyle = namedStyle5;
            enhancedInterfaceRenderer2.ScrollBoxBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(186)))), ((int)(((byte)(221)))));
            enhancedInterfaceRenderer2.SheetTabLowerActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(210)))), ((int)(((byte)(244)))));
            enhancedInterfaceRenderer2.SheetTabLowerNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(231)))), ((int)(((byte)(249)))));
            enhancedInterfaceRenderer2.SheetTabUpperActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(244)))));
            enhancedInterfaceRenderer2.SheetTabUpperNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(249)))));
            enhancedInterfaceRenderer2.TabStripBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(186)))), ((int)(((byte)(221)))));
            spreadSkin2.InterfaceRenderer = enhancedInterfaceRenderer2;
            spreadSkin2.Name = "CustomSkin1";
            namedStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle8.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle8.Renderer = enhancedRowHeaderRenderer1;
            namedStyle8.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spreadSkin2.RowHeaderDefaultStyle = namedStyle8;
            spreadSkin2.ScrollBarRenderer = enhancedScrollBarRenderer2;
            spreadSkin2.SelectionRenderer = new FarPoint.Win.Spread.DefaultSelectionRenderer();
            this.spdData.Skin = spreadSkin2;
            this.spdData.TabIndex = 1;
            this.spdData.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.spdData_CellDoubleClick);
            // 
            // spdData_Sheet1
            // 
            this.spdData_Sheet1.Reset();
            this.spdData_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdData_Sheet1.DefaultStyle.Parent = "Style1";
            this.spdData_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
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
            this.splitContainer1.Panel1.Controls.Add(this.spdData);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Panel2.Controls.Add(this.btnExcel);
            this.splitContainer1.Size = new System.Drawing.Size(667, 407);
            this.splitContainer1.SplitterDistance = 361;
            this.splitContainer1.TabIndex = 2;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(600, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(64, 25);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Okay";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnExcel
            // 
            this.btnExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcel.Location = new System.Drawing.Point(531, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(64, 25);
            this.btnExcel.TabIndex = 0;
            this.btnExcel.Text = "Save";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // MAT070504_P3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 407);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "MAT070504_P3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "당주 + 차주 PCB 부족 세부 데이터";
            ((System.ComponentModel.ISupportInitialize)(this.spdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdData_Sheet1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdData;
        private FarPoint.Win.Spread.SheetView spdData_Sheet1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnExcel;
    }
}