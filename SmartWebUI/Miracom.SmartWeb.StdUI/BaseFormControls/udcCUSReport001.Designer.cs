namespace Miracom.SmartWeb.UI.Controls
{
    partial class udcCUSReport001
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcCUSReport001));
            this.pnlDetailCondition2 = new System.Windows.Forms.Panel();
            this.udcCondition8 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCondition7 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCondition6 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCondition5 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.pnlDetailCondition1 = new System.Windows.Forms.Panel();
            this.udcCondition4 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCondition3 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCondition2 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.udcCondition1 = new Miracom.SmartWeb.UI.Controls.udcCUSCondition();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSort = new Miracom.SmartWeb.UI.Controls.udcButton();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlDetailCondition2.SuspendLayout();
            this.pnlDetailCondition1.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDetailCondition2
            // 
            this.pnlDetailCondition2.Controls.Add(this.udcCondition8);
            this.pnlDetailCondition2.Controls.Add(this.udcCondition7);
            this.pnlDetailCondition2.Controls.Add(this.udcCondition6);
            this.pnlDetailCondition2.Controls.Add(this.udcCondition5);
            this.pnlDetailCondition2.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailCondition2.Location = new System.Drawing.Point(0, 32);
            this.pnlDetailCondition2.Name = "pnlDetailCondition2";
            this.pnlDetailCondition2.Size = new System.Drawing.Size(800, 24);
            this.pnlDetailCondition2.TabIndex = 10;
            // 
            // udcCondition8
            // 
            this.udcCondition8.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition8.bMultiSelect = true;
            this.udcCondition8.ConditionText = "Generation";
            this.udcCondition8.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition8.Location = new System.Drawing.Point(604, 0);
            this.udcCondition8.MandatoryFlag = false;
            this.udcCondition8.Name = "udcCondition8";
            this.udcCondition8.ParentValue = "";
            this.udcCondition8.sCodeColumnName = "";
            this.udcCondition8.sDynamicQuery = "";
            this.udcCondition8.sFactory = "";
            this.udcCondition8.Size = new System.Drawing.Size(180, 21);
            this.udcCondition8.sTableName = "H_GENERATION";
            this.udcCondition8.sValueColumnName = "";
            this.udcCondition8.TabIndex = 7;
            this.udcCondition8.VisibleValueButton = true;
            // 
            // udcCondition7
            // 
            this.udcCondition7.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition7.bMultiSelect = true;
            this.udcCondition7.ConditionText = "Density";
            this.udcCondition7.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition7.Location = new System.Drawing.Point(404, 0);
            this.udcCondition7.MandatoryFlag = false;
            this.udcCondition7.Name = "udcCondition7";
            this.udcCondition7.ParentValue = "";
            this.udcCondition7.sCodeColumnName = "";
            this.udcCondition7.sDynamicQuery = "";
            this.udcCondition7.sFactory = "";
            this.udcCondition7.Size = new System.Drawing.Size(180, 21);
            this.udcCondition7.sTableName = "H_DENSITY";
            this.udcCondition7.sValueColumnName = "";
            this.udcCondition7.TabIndex = 6;
            this.udcCondition7.VisibleValueButton = true;
            // 
            // udcCondition6
            // 
            this.udcCondition6.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition6.bMultiSelect = true;
            this.udcCondition6.ConditionText = "LD Count";
            this.udcCondition6.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition6.Location = new System.Drawing.Point(204, 0);
            this.udcCondition6.MandatoryFlag = false;
            this.udcCondition6.Name = "udcCondition6";
            this.udcCondition6.ParentValue = "";
            this.udcCondition6.sCodeColumnName = "";
            this.udcCondition6.sDynamicQuery = "";
            this.udcCondition6.sFactory = "";
            this.udcCondition6.Size = new System.Drawing.Size(180, 21);
            this.udcCondition6.sTableName = "H_LEAD_COUNT";
            this.udcCondition6.sValueColumnName = "";
            this.udcCondition6.TabIndex = 5;
            this.udcCondition6.VisibleValueButton = true;
            // 
            // udcCondition5
            // 
            this.udcCondition5.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition5.bMultiSelect = true;
            this.udcCondition5.ConditionText = "TYPE2";
            this.udcCondition5.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition5.Location = new System.Drawing.Point(4, 0);
            this.udcCondition5.MandatoryFlag = false;
            this.udcCondition5.Name = "udcCondition5";
            this.udcCondition5.ParentValue = "";
            this.udcCondition5.sCodeColumnName = "";
            this.udcCondition5.sDynamicQuery = "";
            this.udcCondition5.sFactory = "";
            this.udcCondition5.Size = new System.Drawing.Size(180, 21);
            this.udcCondition5.sTableName = "H_DEV_TYPE_2";
            this.udcCondition5.sValueColumnName = "";
            this.udcCondition5.TabIndex = 4;
            this.udcCondition5.VisibleValueButton = true;
            // 
            // pnlDetailCondition1
            // 
            this.pnlDetailCondition1.Controls.Add(this.udcCondition4);
            this.pnlDetailCondition1.Controls.Add(this.udcCondition3);
            this.pnlDetailCondition1.Controls.Add(this.udcCondition2);
            this.pnlDetailCondition1.Controls.Add(this.udcCondition1);
            this.pnlDetailCondition1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetailCondition1.Location = new System.Drawing.Point(0, 8);
            this.pnlDetailCondition1.Name = "pnlDetailCondition1";
            this.pnlDetailCondition1.Size = new System.Drawing.Size(800, 24);
            this.pnlDetailCondition1.TabIndex = 9;
            // 
            // udcCondition4
            // 
            this.udcCondition4.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition4.bMultiSelect = true;
            this.udcCondition4.ConditionText = "TYPE1";
            this.udcCondition4.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition4.Location = new System.Drawing.Point(604, 0);
            this.udcCondition4.MandatoryFlag = false;
            this.udcCondition4.Name = "udcCondition4";
            this.udcCondition4.ParentValue = "";
            this.udcCondition4.sCodeColumnName = "";
            this.udcCondition4.sDynamicQuery = "";
            this.udcCondition4.sFactory = "";
            this.udcCondition4.Size = new System.Drawing.Size(180, 21);
            this.udcCondition4.sTableName = "H_DEV_TYPE_1";
            this.udcCondition4.sValueColumnName = "";
            this.udcCondition4.TabIndex = 3;
            this.udcCondition4.VisibleValueButton = true;
            // 
            // udcCondition3
            // 
            this.udcCondition3.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition3.bMultiSelect = true;
            this.udcCondition3.ConditionText = "Package";
            this.udcCondition3.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition3.Location = new System.Drawing.Point(404, 0);
            this.udcCondition3.MandatoryFlag = false;
            this.udcCondition3.Name = "udcCondition3";
            this.udcCondition3.ParentValue = "";
            this.udcCondition3.sCodeColumnName = "";
            this.udcCondition3.sDynamicQuery = "";
            this.udcCondition3.sFactory = "";
            this.udcCondition3.Size = new System.Drawing.Size(180, 21);
            this.udcCondition3.sTableName = "H_PACKAGE";
            this.udcCondition3.sValueColumnName = "";
            this.udcCondition3.TabIndex = 2;
            this.udcCondition3.VisibleValueButton = true;
            // 
            // udcCondition2
            // 
            this.udcCondition2.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition2.bMultiSelect = true;
            this.udcCondition2.ConditionText = "Family";
            this.udcCondition2.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition2.Location = new System.Drawing.Point(204, 0);
            this.udcCondition2.MandatoryFlag = false;
            this.udcCondition2.Name = "udcCondition2";
            this.udcCondition2.ParentValue = "";
            this.udcCondition2.sCodeColumnName = "";
            this.udcCondition2.sDynamicQuery = "";
            this.udcCondition2.sFactory = "";
            this.udcCondition2.Size = new System.Drawing.Size(180, 21);
            this.udcCondition2.sTableName = "H_FAMILY";
            this.udcCondition2.sValueColumnName = "";
            this.udcCondition2.TabIndex = 1;
            this.udcCondition2.VisibleValueButton = true;
            // 
            // udcCondition1
            // 
            this.udcCondition1.BackColor = System.Drawing.Color.Transparent;
            this.udcCondition1.bMultiSelect = true;
            this.udcCondition1.ConditionText = "Customer";
            this.udcCondition1.CondtionType = Miracom.SmartWeb.UI.Controls.udcCondition.eConditionype.TABLE;
            this.udcCondition1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.udcCondition1.Location = new System.Drawing.Point(4, 0);
            this.udcCondition1.MandatoryFlag = false;
            this.udcCondition1.Name = "udcCondition1";
            this.udcCondition1.ParentValue = "";
            this.udcCondition1.sCodeColumnName = "";
            this.udcCondition1.sDynamicQuery = "";
            this.udcCondition1.sFactory = "";
            this.udcCondition1.Size = new System.Drawing.Size(180, 21);
            this.udcCondition1.sTableName = "H_CUSTOMER";
            this.udcCondition1.sValueColumnName = "";
            this.udcCondition1.TabIndex = 0;
            this.udcCondition1.VisibleValueButton = true;
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.White;
            this.pnlDetail.Controls.Add(this.pnlDetailCondition2);
            this.pnlDetail.Controls.Add(this.pnlDetailCondition1);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDetail.Location = new System.Drawing.Point(0, 26);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.pnlDetail.Size = new System.Drawing.Size(800, 57);
            this.pnlDetail.TabIndex = 3;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 83);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlMain.Size = new System.Drawing.Size(800, 517);
            this.pnlMain.TabIndex = 5;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.White;
            this.pnlTitle.BackgroundImage = global::Miracom.SmartWeb.UI.Properties.Resources.Panel;
            this.pnlTitle.Controls.Add(this.panel1);
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(800, 26);
            this.pnlTitle.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSort);
            this.panel1.Controls.Add(this.btnDetail);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.btnExcelExport);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(414, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(386, 26);
            this.panel1.TabIndex = 17;
            // 
            // btnSort
            // 
            this.btnSort.AutoBindingMultiButtonType = Miracom.SmartWeb.UI.Controls.AutoBindingBtnType.S_Group;
            this.btnSort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSort.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSort.Image = ((System.Drawing.Image)(resources.GetObject("btnSort.Image")));
            this.btnSort.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSort.Location = new System.Drawing.Point(85, 2);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(70, 23);
            this.btnSort.TabIndex = 22;
            this.btnSort.Text = "Group";
            this.btnSort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSort.UseVisualStyleBackColor = true;
            // 
            // btnDetail
            // 
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.Image = global::Miracom.SmartWeb.UI.Properties.Resources._021prjdown;
            this.btnDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetail.Location = new System.Drawing.Point(8, 2);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(70, 23);
            this.btnDetail.TabIndex = 21;
            this.btnDetail.Text = "Detail";
            this.btnDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.Image = global::Miracom.SmartWeb.UI.Properties.Resources._015view;
            this.btnView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnView.Location = new System.Drawing.Point(161, 2);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(70, 23);
            this.btnView.TabIndex = 17;
            this.btnView.Text = "View";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnView_MouseUp);
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExcelExport.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExcelExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelExport.Image = global::Miracom.SmartWeb.UI.Properties.Resources._006excel;
            this.btnExcelExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcelExport.Location = new System.Drawing.Point(237, 2);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(70, 23);
            this.btnExcelExport.TabIndex = 18;
            this.btnExcelExport.Text = "Export";
            this.btnExcelExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcelExport.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnClose.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataclose;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(313, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 19;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(19, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(32, 13);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Title";
            // 
            // udcCUSReport001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlDetail);
            this.Controls.Add(this.pnlTitle);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "udcCUSReport001";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.udcCUSReport001_Load);
            this.pnlDetailCondition2.ResumeLayout(false);
            this.pnlDetailCondition1.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel pnlDetailCondition2;
        protected System.Windows.Forms.Panel pnlDetailCondition1;
        private System.Windows.Forms.Panel pnlTitle;
        protected System.Windows.Forms.Panel pnlDetail;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition4;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition3;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition2;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition1;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition8;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition7;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition6;
        protected Miracom.SmartWeb.UI.Controls.udcCUSCondition udcCondition5;
        protected System.Windows.Forms.Label lblTitle;
        protected System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel panel1;
        protected System.Windows.Forms.Button btnView;
        protected System.Windows.Forms.Button btnExcelExport;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Button btnDetail;
        private udcButton btnSort;
    }
}
