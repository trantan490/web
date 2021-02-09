namespace Miracom.SmartWeb.UI.Controls
{
    partial class udcCondition
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcCondition));
            this.lblConditionLabel = new System.Windows.Forms.Label();
            this.cdvValue = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.imlSmallIcon = new System.Windows.Forms.ImageList(this.components);
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.pnlValue = new System.Windows.Forms.Panel();
            this.pnlSubValue = new System.Windows.Forms.Panel();
            this.cdvSubValue = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            ((System.ComponentModel.ISupportInitialize)(this.cdvValue)).BeginInit();
            this.pnlLabel.SuspendLayout();
            this.pnlValue.SuspendLayout();
            this.pnlSubValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvSubValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConditionLabel
            // 
            this.lblConditionLabel.Location = new System.Drawing.Point(0, 2);
            this.lblConditionLabel.Name = "lblConditionLabel";
            this.lblConditionLabel.Size = new System.Drawing.Size(84, 17);
            this.lblConditionLabel.TabIndex = 0;
            this.lblConditionLabel.Text = "Condition";
            // 
            // cdvValue
            // 
            this.cdvValue.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvValue.BorderHotColor = System.Drawing.Color.Black;
            this.cdvValue.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvValue.BtnToolTipText = "";
            this.cdvValue.DescText = "";
            this.cdvValue.DisplaySubItemIndex = -1;
            this.cdvValue.DisplayText = "";
            this.cdvValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cdvValue.Focusing = null;
            this.cdvValue.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvValue.Index = 0;
            this.cdvValue.IsViewBtnImage = false;
            this.cdvValue.Location = new System.Drawing.Point(0, 0);
            this.cdvValue.MaxLength = 32767;
            this.cdvValue.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvValue.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvValue.Name = "cdvValue";
            this.cdvValue.ReadOnly = false;
            this.cdvValue.SearchSubItemIndex = 0;
            this.cdvValue.SelectedDescIndex = -1;
            this.cdvValue.SelectedSubItemIndex = -1;
            this.cdvValue.SelectionStart = 0;
            this.cdvValue.Size = new System.Drawing.Size(86, 22);
            this.cdvValue.SmallImageList = this.imlSmallIcon;
            this.cdvValue.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvValue.TabIndex = 2;
            this.cdvValue.TextBoxToolTipText = "";
            this.cdvValue.TextBoxWidth = 86;
            this.cdvValue.VisibleButton = true;
            this.cdvValue.VisibleColumnHeader = false;
            this.cdvValue.VisibleDescription = false;
            this.cdvValue.TextBoxTextChanged += new System.EventHandler(this.cdvValue_TextBoxTextChanged);
            this.cdvValue.TextBoxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cdvValue_TextBoxKeyPress);
            this.cdvValue.ButtonPress += new System.EventHandler(this.cdvValue_ButtonPress);
            this.cdvValue.TextBoxLostFocus += new System.EventHandler(this.cdvValue_TextBoxLostFocus);
            this.cdvValue.TextBoxGotFocus += new System.EventHandler(this.cdvValue_TextBoxGotFocus);
            this.cdvValue.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvValue_SelectedItemChanged);
            // 
            // imlSmallIcon
            // 
            this.imlSmallIcon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlSmallIcon.ImageStream")));
            this.imlSmallIcon.TransparentColor = System.Drawing.Color.Transparent;
            this.imlSmallIcon.Images.SetKeyName(0, "");
            this.imlSmallIcon.Images.SetKeyName(1, "");
            this.imlSmallIcon.Images.SetKeyName(2, "");
            this.imlSmallIcon.Images.SetKeyName(3, "");
            this.imlSmallIcon.Images.SetKeyName(4, "");
            this.imlSmallIcon.Images.SetKeyName(5, "");
            this.imlSmallIcon.Images.SetKeyName(6, "");
            this.imlSmallIcon.Images.SetKeyName(7, "");
            this.imlSmallIcon.Images.SetKeyName(8, "");
            this.imlSmallIcon.Images.SetKeyName(9, "");
            this.imlSmallIcon.Images.SetKeyName(10, "");
            this.imlSmallIcon.Images.SetKeyName(11, "");
            this.imlSmallIcon.Images.SetKeyName(12, "");
            this.imlSmallIcon.Images.SetKeyName(13, "");
            this.imlSmallIcon.Images.SetKeyName(14, "");
            this.imlSmallIcon.Images.SetKeyName(15, "");
            this.imlSmallIcon.Images.SetKeyName(16, "");
            this.imlSmallIcon.Images.SetKeyName(17, "");
            this.imlSmallIcon.Images.SetKeyName(18, "");
            this.imlSmallIcon.Images.SetKeyName(19, "");
            this.imlSmallIcon.Images.SetKeyName(20, "");
            this.imlSmallIcon.Images.SetKeyName(21, "");
            this.imlSmallIcon.Images.SetKeyName(22, "");
            this.imlSmallIcon.Images.SetKeyName(23, "");
            this.imlSmallIcon.Images.SetKeyName(24, "");
            this.imlSmallIcon.Images.SetKeyName(25, "");
            this.imlSmallIcon.Images.SetKeyName(26, "");
            this.imlSmallIcon.Images.SetKeyName(27, "");
            this.imlSmallIcon.Images.SetKeyName(28, "");
            this.imlSmallIcon.Images.SetKeyName(29, "");
            this.imlSmallIcon.Images.SetKeyName(30, "");
            this.imlSmallIcon.Images.SetKeyName(31, "");
            this.imlSmallIcon.Images.SetKeyName(32, "");
            this.imlSmallIcon.Images.SetKeyName(33, "");
            this.imlSmallIcon.Images.SetKeyName(34, "");
            this.imlSmallIcon.Images.SetKeyName(35, "");
            this.imlSmallIcon.Images.SetKeyName(36, "");
            this.imlSmallIcon.Images.SetKeyName(37, "");
            this.imlSmallIcon.Images.SetKeyName(38, "");
            this.imlSmallIcon.Images.SetKeyName(39, "");
            this.imlSmallIcon.Images.SetKeyName(40, "");
            this.imlSmallIcon.Images.SetKeyName(41, "");
            this.imlSmallIcon.Images.SetKeyName(42, "");
            this.imlSmallIcon.Images.SetKeyName(43, "");
            this.imlSmallIcon.Images.SetKeyName(44, "");
            this.imlSmallIcon.Images.SetKeyName(45, "");
            this.imlSmallIcon.Images.SetKeyName(46, "");
            this.imlSmallIcon.Images.SetKeyName(47, "");
            this.imlSmallIcon.Images.SetKeyName(48, "");
            this.imlSmallIcon.Images.SetKeyName(49, "");
            this.imlSmallIcon.Images.SetKeyName(50, "");
            this.imlSmallIcon.Images.SetKeyName(51, "");
            this.imlSmallIcon.Images.SetKeyName(52, "");
            this.imlSmallIcon.Images.SetKeyName(53, "");
            this.imlSmallIcon.Images.SetKeyName(54, "");
            this.imlSmallIcon.Images.SetKeyName(55, "");
            this.imlSmallIcon.Images.SetKeyName(56, "");
            this.imlSmallIcon.Images.SetKeyName(57, "");
            this.imlSmallIcon.Images.SetKeyName(58, "");
            this.imlSmallIcon.Images.SetKeyName(59, "");
            this.imlSmallIcon.Images.SetKeyName(60, "");
            this.imlSmallIcon.Images.SetKeyName(61, "");
            this.imlSmallIcon.Images.SetKeyName(62, "");
            this.imlSmallIcon.Images.SetKeyName(63, "");
            this.imlSmallIcon.Images.SetKeyName(64, "");
            this.imlSmallIcon.Images.SetKeyName(65, "");
            this.imlSmallIcon.Images.SetKeyName(66, "");
            this.imlSmallIcon.Images.SetKeyName(67, "");
            this.imlSmallIcon.Images.SetKeyName(68, "");
            this.imlSmallIcon.Images.SetKeyName(69, "");
            this.imlSmallIcon.Images.SetKeyName(70, "");
            this.imlSmallIcon.Images.SetKeyName(71, "");
            this.imlSmallIcon.Images.SetKeyName(72, "");
            this.imlSmallIcon.Images.SetKeyName(73, "");
            this.imlSmallIcon.Images.SetKeyName(74, "");
            this.imlSmallIcon.Images.SetKeyName(75, "");
            this.imlSmallIcon.Images.SetKeyName(76, "");
            this.imlSmallIcon.Images.SetKeyName(77, "");
            this.imlSmallIcon.Images.SetKeyName(78, "");
            this.imlSmallIcon.Images.SetKeyName(79, "");
            this.imlSmallIcon.Images.SetKeyName(80, "");
            this.imlSmallIcon.Images.SetKeyName(81, "");
            this.imlSmallIcon.Images.SetKeyName(82, "");
            this.imlSmallIcon.Images.SetKeyName(83, "");
            this.imlSmallIcon.Images.SetKeyName(84, "");
            this.imlSmallIcon.Images.SetKeyName(85, "");
            this.imlSmallIcon.Images.SetKeyName(86, "");
            this.imlSmallIcon.Images.SetKeyName(87, "");
            this.imlSmallIcon.Images.SetKeyName(88, "");
            this.imlSmallIcon.Images.SetKeyName(89, "");
            this.imlSmallIcon.Images.SetKeyName(90, "");
            this.imlSmallIcon.Images.SetKeyName(91, "");
            this.imlSmallIcon.Images.SetKeyName(92, "");
            this.imlSmallIcon.Images.SetKeyName(93, "");
            this.imlSmallIcon.Images.SetKeyName(94, "");
            this.imlSmallIcon.Images.SetKeyName(95, "");
            this.imlSmallIcon.Images.SetKeyName(96, "");
            this.imlSmallIcon.Images.SetKeyName(97, "");
            this.imlSmallIcon.Images.SetKeyName(98, "");
            this.imlSmallIcon.Images.SetKeyName(99, "");
            this.imlSmallIcon.Images.SetKeyName(100, "");
            this.imlSmallIcon.Images.SetKeyName(101, "");
            this.imlSmallIcon.Images.SetKeyName(102, "");
            this.imlSmallIcon.Images.SetKeyName(103, "");
            this.imlSmallIcon.Images.SetKeyName(104, "White Image");
            // 
            // pnlLabel
            // 
            this.pnlLabel.Controls.Add(this.lblConditionLabel);
            this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlLabel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(79, 22);
            this.pnlLabel.TabIndex = 3;
            // 
            // pnlValue
            // 
            this.pnlValue.Controls.Add(this.cdvValue);
            this.pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlValue.Location = new System.Drawing.Point(79, 0);
            this.pnlValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.Size = new System.Drawing.Size(86, 22);
            this.pnlValue.TabIndex = 4;
            // 
            // pnlSubValue
            // 
            this.pnlSubValue.Controls.Add(this.cdvSubValue);
            this.pnlSubValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSubValue.Location = new System.Drawing.Point(165, 0);
            this.pnlSubValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlSubValue.Name = "pnlSubValue";
            this.pnlSubValue.Size = new System.Drawing.Size(45, 22);
            this.pnlSubValue.TabIndex = 5;
            // 
            // cdvSubValue
            // 
            this.cdvSubValue.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvSubValue.BorderHotColor = System.Drawing.Color.Black;
            this.cdvSubValue.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvSubValue.BtnToolTipText = "";
            this.cdvSubValue.DescText = "";
            this.cdvSubValue.DisplaySubItemIndex = -1;
            this.cdvSubValue.DisplayText = "";
            this.cdvSubValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cdvSubValue.Focusing = null;
            this.cdvSubValue.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvSubValue.Index = 0;
            this.cdvSubValue.IsViewBtnImage = false;
            this.cdvSubValue.Location = new System.Drawing.Point(0, 0);
            this.cdvSubValue.MaxLength = 32767;
            this.cdvSubValue.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvSubValue.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvSubValue.Name = "cdvSubValue";
            this.cdvSubValue.ReadOnly = false;
            this.cdvSubValue.SearchSubItemIndex = 0;
            this.cdvSubValue.SelectedDescIndex = -1;
            this.cdvSubValue.SelectedSubItemIndex = -1;
            this.cdvSubValue.SelectionStart = 0;
            this.cdvSubValue.Size = new System.Drawing.Size(45, 22);
            this.cdvSubValue.SmallImageList = this.imlSmallIcon;
            this.cdvSubValue.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvSubValue.TabIndex = 2;
            this.cdvSubValue.TextBoxToolTipText = "";
            this.cdvSubValue.TextBoxWidth = 45;
            this.cdvSubValue.VisibleButton = true;
            this.cdvSubValue.VisibleColumnHeader = false;
            this.cdvSubValue.VisibleDescription = false;
            this.cdvSubValue.TextBoxTextChanged += new System.EventHandler(this.cdvSubValue_TextBoxTextChanged);
            this.cdvSubValue.TextBoxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cdvSubValue_TextBoxKeyPress);
            this.cdvSubValue.ButtonPress += new System.EventHandler(this.cdvSubValue_ButtonPress);
            this.cdvSubValue.TextBoxLostFocus += new System.EventHandler(this.cdvSubValue_TextBoxLostFocus);
            this.cdvSubValue.TextBoxGotFocus += new System.EventHandler(this.cdvSubValue_TextBoxGotFocus);
            this.cdvSubValue.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvSubValue_SelectedItemChanged);
            // 
            // udcCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlValue);
            this.Controls.Add(this.pnlSubValue);
            this.Controls.Add(this.pnlLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "udcCondition";
            this.Size = new System.Drawing.Size(210, 22);
            ((System.ComponentModel.ISupportInitialize)(this.cdvValue)).EndInit();
            this.pnlLabel.ResumeLayout(false);
            this.pnlValue.ResumeLayout(false);
            this.pnlSubValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdvSubValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblConditionLabel;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvValue;
        private System.Windows.Forms.Panel pnlLabel;
        private System.Windows.Forms.Panel pnlValue;
        private System.Windows.Forms.Panel pnlSubValue;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvSubValue;
        protected System.Windows.Forms.ImageList imlSmallIcon;
    }
}
