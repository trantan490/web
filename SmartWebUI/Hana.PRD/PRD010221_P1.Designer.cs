namespace Hana.PRD
{
    partial class PRD010221_P1
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
            FarPoint.Win.Spread.NamedStyle namedStyle1 = new FarPoint.Win.Spread.NamedStyle("Style1");
            FarPoint.Win.Spread.CellType.GeneralCellType generalCellType1 = new FarPoint.Win.Spread.CellType.GeneralCellType();
            FarPoint.Win.Spread.SpreadSkin spreadSkin1 = new FarPoint.Win.Spread.SpreadSkin();
            FarPoint.Win.Spread.NamedStyle namedStyle2 = new FarPoint.Win.Spread.NamedStyle("ColumnHeaderEnhanced");
            FarPoint.Win.Spread.NamedStyle namedStyle3 = new FarPoint.Win.Spread.NamedStyle("CornerEnhanced");
            FarPoint.Win.Spread.CellType.EnhancedCornerRenderer enhancedCornerRenderer1 = new FarPoint.Win.Spread.CellType.EnhancedCornerRenderer();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PRD010221_P1));
            FarPoint.Win.Spread.EnhancedInterfaceRenderer enhancedInterfaceRenderer1 = new FarPoint.Win.Spread.EnhancedInterfaceRenderer();
            FarPoint.Win.Spread.NamedStyle namedStyle4 = new FarPoint.Win.Spread.NamedStyle("RowHeaderEnhanced");
            FarPoint.Win.Spread.EnhancedScrollBarRenderer enhancedScrollBarRenderer1 = new FarPoint.Win.Spread.EnhancedScrollBarRenderer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.spdDataOperInfo = new Miracom.SmartWeb.UI.Controls.udcFarPoint(this.components);
            this.spdDataOperInfo_Sheet1 = new FarPoint.Win.Spread.SheetView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnExcel = new System.Windows.Forms.Button();
            this.txtLot = new System.Windows.Forms.TextBox();
            this.txtPartNo = new System.Windows.Forms.TextBox();
            this.grpbLotInfo = new System.Windows.Forms.GroupBox();
            this.lblLot = new System.Windows.Forms.Label();
            this.lblPartNo = new System.Windows.Forms.Label();
            this.lblQty = new System.Windows.Forms.Label();
            this.lblPlace = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.lblEngineer = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblBusiness = new System.Windows.Forms.Label();
            this.lblNotice = new System.Windows.Forms.Label();
            this.txtPlace = new System.Windows.Forms.TextBox();
            this.txtEngineer = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.txtBusiness = new System.Windows.Forms.TextBox();
            this.txtNotice = new System.Windows.Forms.TextBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spdDataOperInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdDataOperInfo_Sheet1)).BeginInit();
            this.grpbLotInfo.SuspendLayout();
            this.SuspendLayout();
            enhancedColumnHeaderRenderer1.Name = "enhancedColumnHeaderRenderer1";
            enhancedColumnHeaderRenderer1.TextRotationAngle = 0;
            enhancedRowHeaderRenderer1.Name = "enhancedRowHeaderRenderer1";
            enhancedRowHeaderRenderer1.TextRotationAngle = 0;
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
            this.splitContainer1.Panel1.Controls.Add(this.grpbLotInfo);
            this.splitContainer1.Panel1.Controls.Add(this.spdDataOperInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnOk);
            this.splitContainer1.Panel2.Controls.Add(this.btnExcel);
            this.splitContainer1.Size = new System.Drawing.Size(943, 439);
            this.splitContainer1.SplitterDistance = 389;
            this.splitContainer1.TabIndex = 2;
            // 
            // spdDataOperInfo
            // 
            this.spdDataOperInfo.About = "4.0.2001.2005";
            this.spdDataOperInfo.AccessibleDescription = "spdDataOperInfo, Sheet1, Row 0, Column 0, ";
            this.spdDataOperInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spdDataOperInfo.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.spdDataOperInfo.Location = new System.Drawing.Point(0, 113);
            this.spdDataOperInfo.Name = "spdDataOperInfo";
            namedStyle1.BackColor = System.Drawing.SystemColors.Window;
            namedStyle1.CellType = generalCellType1;
            namedStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            namedStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            namedStyle1.Locked = false;
            namedStyle1.Renderer = generalCellType1;
            this.spdDataOperInfo.NamedStyles.AddRange(new FarPoint.Win.Spread.NamedStyle[] {
            namedStyle1});
            this.spdDataOperInfo.RPT_IsPreCellsType = true;
            this.spdDataOperInfo.ScrollBarTrackPolicy = FarPoint.Win.Spread.ScrollBarTrackPolicy.Both;
            this.spdDataOperInfo.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
            this.spdDataOperInfo_Sheet1});
            this.spdDataOperInfo.Size = new System.Drawing.Size(940, 273);
            namedStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle2.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle2.Renderer = enhancedColumnHeaderRenderer1;
            namedStyle2.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spreadSkin1.ColumnHeaderDefaultStyle = namedStyle2;
            namedStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle3.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle3.Renderer = enhancedCornerRenderer1;
            namedStyle3.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spreadSkin1.CornerDefaultStyle = namedStyle3;
            spreadSkin1.DefaultStyle = namedStyle1;
            spreadSkin1.FocusRenderer = ((FarPoint.Win.Spread.IFocusIndicatorRenderer)(resources.GetObject("spreadSkin1.FocusRenderer")));
            enhancedInterfaceRenderer1.ScrollBoxBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(186)))), ((int)(((byte)(221)))));
            enhancedInterfaceRenderer1.SheetTabLowerActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(210)))), ((int)(((byte)(244)))));
            enhancedInterfaceRenderer1.SheetTabLowerNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(231)))), ((int)(((byte)(249)))));
            enhancedInterfaceRenderer1.SheetTabUpperActiveColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(244)))));
            enhancedInterfaceRenderer1.SheetTabUpperNormalColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(229)))), ((int)(((byte)(249)))));
            enhancedInterfaceRenderer1.TabStripBackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(186)))), ((int)(((byte)(221)))));
            spreadSkin1.InterfaceRenderer = enhancedInterfaceRenderer1;
            spreadSkin1.Name = "CustomSkin1";
            namedStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            namedStyle4.HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            namedStyle4.Renderer = enhancedRowHeaderRenderer1;
            namedStyle4.VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spreadSkin1.RowHeaderDefaultStyle = namedStyle4;
            spreadSkin1.ScrollBarRenderer = enhancedScrollBarRenderer1;
            spreadSkin1.SelectionRenderer = new FarPoint.Win.Spread.DefaultSelectionRenderer();
            this.spdDataOperInfo.Skin = spreadSkin1;
            this.spdDataOperInfo.TabIndex = 2;
            // 
            // spdDataOperInfo_Sheet1
            // 
            this.spdDataOperInfo_Sheet1.Reset();
            this.spdDataOperInfo_Sheet1.SheetName = "Sheet1";
            // Formulas and custom names must be loaded with R1C1 reference style
            this.spdDataOperInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.R1C1;
            this.spdDataOperInfo_Sheet1.DefaultStyle.Parent = "Style1";
            this.spdDataOperInfo_Sheet1.ReferenceStyle = FarPoint.Win.Spread.Model.ReferenceStyle.A1;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(839, 3);
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
            this.btnExcel.Location = new System.Drawing.Point(770, 3);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(64, 25);
            this.btnExcel.TabIndex = 0;
            this.btnExcel.Text = "Save";
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // txtLot
            // 
            this.txtLot.Location = new System.Drawing.Point(89, 18);
            this.txtLot.Name = "txtLot";
            this.txtLot.ReadOnly = true;
            this.txtLot.Size = new System.Drawing.Size(100, 21);
            this.txtLot.TabIndex = 3;
            // 
            // txtPartNo
            // 
            this.txtPartNo.Location = new System.Drawing.Point(255, 18);
            this.txtPartNo.Name = "txtPartNo";
            this.txtPartNo.ReadOnly = true;
            this.txtPartNo.Size = new System.Drawing.Size(267, 21);
            this.txtPartNo.TabIndex = 4;
            // 
            // grpbLotInfo
            // 
            this.grpbLotInfo.Controls.Add(this.txtNotice);
            this.grpbLotInfo.Controls.Add(this.txtBusiness);
            this.grpbLotInfo.Controls.Add(this.txtProduct);
            this.grpbLotInfo.Controls.Add(this.txtEngineer);
            this.grpbLotInfo.Controls.Add(this.txtPlace);
            this.grpbLotInfo.Controls.Add(this.lblNotice);
            this.grpbLotInfo.Controls.Add(this.lblBusiness);
            this.grpbLotInfo.Controls.Add(this.lblProduct);
            this.grpbLotInfo.Controls.Add(this.lblEngineer);
            this.grpbLotInfo.Controls.Add(this.txtQty);
            this.grpbLotInfo.Controls.Add(this.lblPlace);
            this.grpbLotInfo.Controls.Add(this.lblQty);
            this.grpbLotInfo.Controls.Add(this.lblPartNo);
            this.grpbLotInfo.Controls.Add(this.lblLot);
            this.grpbLotInfo.Controls.Add(this.txtPartNo);
            this.grpbLotInfo.Controls.Add(this.txtLot);
            this.grpbLotInfo.Location = new System.Drawing.Point(3, 3);
            this.grpbLotInfo.Name = "grpbLotInfo";
            this.grpbLotInfo.Size = new System.Drawing.Size(937, 104);
            this.grpbLotInfo.TabIndex = 5;
            this.grpbLotInfo.TabStop = false;
            this.grpbLotInfo.Text = "LOT INFO";
            // 
            // lblLot
            // 
            this.lblLot.AutoSize = true;
            this.lblLot.Location = new System.Drawing.Point(57, 21);
            this.lblLot.Name = "lblLot";
            this.lblLot.Size = new System.Drawing.Size(26, 13);
            this.lblLot.TabIndex = 5;
            this.lblLot.Text = "LOT";
            this.lblLot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPartNo
            // 
            this.lblPartNo.AutoSize = true;
            this.lblPartNo.Location = new System.Drawing.Point(195, 21);
            this.lblPartNo.Name = "lblPartNo";
            this.lblPartNo.Size = new System.Drawing.Size(54, 13);
            this.lblPartNo.TabIndex = 6;
            this.lblPartNo.Text = "PART_NO";
            this.lblPartNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Location = new System.Drawing.Point(556, 21);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(27, 13);
            this.lblQty.TabIndex = 7;
            this.lblQty.Text = "QTY";
            this.lblQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPlace
            // 
            this.lblPlace.AutoSize = true;
            this.lblPlace.Location = new System.Drawing.Point(40, 48);
            this.lblPlace.Name = "lblPlace";
            this.lblPlace.Size = new System.Drawing.Size(43, 13);
            this.lblPlace.TabIndex = 8;
            this.lblPlace.Text = "the place of shipment";
            this.lblPlace.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(589, 18);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(100, 21);
            this.txtQty.TabIndex = 9;
            this.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblEngineer
            // 
            this.lblEngineer.AutoSize = true;
            this.lblEngineer.Location = new System.Drawing.Point(213, 48);
            this.lblEngineer.Name = "lblEngineer";
            this.lblEngineer.Size = new System.Drawing.Size(36, 13);
            this.lblEngineer.TabIndex = 10;
            this.lblEngineer.Text = "ENG\'R";
            this.lblEngineer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(361, 48);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(55, 13);
            this.lblProduct.TabIndex = 11;
            this.lblProduct.Text = "Production operation";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBusiness
            // 
            this.lblBusiness.AutoSize = true;
            this.lblBusiness.Location = new System.Drawing.Point(528, 48);
            this.lblBusiness.Name = "lblBusiness";
            this.lblBusiness.Size = new System.Drawing.Size(55, 13);
            this.lblBusiness.TabIndex = 12;
            this.lblBusiness.Text = "Sales Manager";
            this.lblBusiness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNotice
            // 
            this.lblNotice.AutoSize = true;
            this.lblNotice.Location = new System.Drawing.Point(10, 75);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(73, 13);
            this.lblNotice.TabIndex = 13;
            this.lblNotice.Text = "Special Notice";
            this.lblNotice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPlace
            // 
            this.txtPlace.Location = new System.Drawing.Point(89, 45);
            this.txtPlace.Name = "txtPlace";
            this.txtPlace.ReadOnly = true;
            this.txtPlace.Size = new System.Drawing.Size(100, 21);
            this.txtPlace.TabIndex = 14;
            // 
            // txtEngineer
            // 
            this.txtEngineer.Location = new System.Drawing.Point(255, 45);
            this.txtEngineer.Name = "txtEngineer";
            this.txtEngineer.ReadOnly = true;
            this.txtEngineer.Size = new System.Drawing.Size(100, 21);
            this.txtEngineer.TabIndex = 15;
            // 
            // txtProduct
            // 
            this.txtProduct.Location = new System.Drawing.Point(422, 45);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.ReadOnly = true;
            this.txtProduct.Size = new System.Drawing.Size(100, 21);
            this.txtProduct.TabIndex = 16;
            // 
            // txtBusiness
            // 
            this.txtBusiness.Location = new System.Drawing.Point(589, 45);
            this.txtBusiness.Name = "txtBusiness";
            this.txtBusiness.ReadOnly = true;
            this.txtBusiness.Size = new System.Drawing.Size(100, 21);
            this.txtBusiness.TabIndex = 17;
            // 
            // txtNotice
            // 
            this.txtNotice.Location = new System.Drawing.Point(89, 72);
            this.txtNotice.Name = "txtNotice";
            this.txtNotice.ReadOnly = true;
            this.txtNotice.Size = new System.Drawing.Size(600, 21);
            this.txtNotice.TabIndex = 18;
            // 
            // PRD010221_P1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 439);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "PRD010221_P1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Progress management";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spdDataOperInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spdDataOperInfo_Sheet1)).EndInit();
            this.grpbLotInfo.ResumeLayout(false);
            this.grpbLotInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnExcel;
        private Miracom.SmartWeb.UI.Controls.udcFarPoint spdDataOperInfo;
        private FarPoint.Win.Spread.SheetView spdDataOperInfo_Sheet1;
        private System.Windows.Forms.TextBox txtPartNo;
        private System.Windows.Forms.TextBox txtLot;
        private System.Windows.Forms.GroupBox grpbLotInfo;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.Label lblPartNo;
        private System.Windows.Forms.Label lblLot;
        private System.Windows.Forms.Label lblPlace;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.Label lblBusiness;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblEngineer;
        private System.Windows.Forms.TextBox txtBusiness;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.TextBox txtEngineer;
        private System.Windows.Forms.TextBox txtPlace;
        private System.Windows.Forms.TextBox txtNotice;
    }
}