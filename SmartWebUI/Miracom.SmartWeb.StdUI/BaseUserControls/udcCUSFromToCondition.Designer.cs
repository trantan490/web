namespace Miracom.SmartWeb.UI.Controls
{
    partial class udcCUSFromToCondition
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(udcCUSFromToCondition));
            this.lblConditionLabel = new System.Windows.Forms.Label();
            this.pnlLabel = new System.Windows.Forms.Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.pnlValue = new System.Windows.Forms.Panel();
            this.cdvToValue = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.cdvFromValue = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.imlSmallIcon = new System.Windows.Forms.ImageList(this.components);
            this.pnlLabel.SuspendLayout();
            this.pnlValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvToValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFromValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConditionLabel
            // 
            this.lblConditionLabel.BackColor = System.Drawing.Color.Transparent;
            this.lblConditionLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblConditionLabel.Location = new System.Drawing.Point(14, 3);
            this.lblConditionLabel.Name = "lblConditionLabel";
            this.lblConditionLabel.Size = new System.Drawing.Size(64, 17);
            this.lblConditionLabel.TabIndex = 0;
            this.lblConditionLabel.Text = "Condition";
            this.lblConditionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLabel
            // 
            this.pnlLabel.Controls.Add(this.lblIcon);
            this.pnlLabel.Controls.Add(this.lblConditionLabel);
            this.pnlLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlLabel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLabel.Name = "pnlLabel";
            this.pnlLabel.Size = new System.Drawing.Size(80, 21);
            this.pnlLabel.TabIndex = 8;
            // 
            // lblIcon
            // 
            this.lblIcon.Image = ((System.Drawing.Image)(resources.GetObject("lblIcon.Image")));
            this.lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblIcon.Location = new System.Drawing.Point(3, 2);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(6, 17);
            this.lblIcon.TabIndex = 1;
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlValue
            // 
            this.pnlValue.Controls.Add(this.cdvToValue);
            this.pnlValue.Controls.Add(this.cdvFromValue);
            this.pnlValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlValue.Location = new System.Drawing.Point(80, 0);
            this.pnlValue.Margin = new System.Windows.Forms.Padding(0);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.Size = new System.Drawing.Size(210, 21);
            this.pnlValue.TabIndex = 9;
            // 
            // cdvToValue
            // 
            this.cdvToValue.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvToValue.BorderHotColor = System.Drawing.Color.Black;
            this.cdvToValue.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvToValue.BtnToolTipText = "";
            this.cdvToValue.DescText = "";
            this.cdvToValue.DisplaySubItemIndex = -1;
            this.cdvToValue.DisplayText = "";
            this.cdvToValue.Focusing = null;
            this.cdvToValue.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvToValue.Index = 0;
            this.cdvToValue.IsViewBtnImage = false;
            this.cdvToValue.Location = new System.Drawing.Point(109, 0);
            this.cdvToValue.MaxLength = 32767;
            this.cdvToValue.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvToValue.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvToValue.MultiSelect = false;
            this.cdvToValue.Name = "cdvToValue";
            this.cdvToValue.ReadOnly = true;
            this.cdvToValue.SearchSubItemIndex = 0;
            this.cdvToValue.SelectedDescIndex = -1;
            this.cdvToValue.SelectedSubItemIndex = -1;
            this.cdvToValue.SelectedValueToQueryText = "";
            this.cdvToValue.SelectionStart = 0;
            this.cdvToValue.Size = new System.Drawing.Size(100, 21);
            this.cdvToValue.SmallImageList = null;
            this.cdvToValue.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvToValue.TabIndex = 3;
            this.cdvToValue.TextBoxToolTipText = "";
            this.cdvToValue.TextBoxWidth = 100;
            this.cdvToValue.VisibleButton = true;
            this.cdvToValue.VisibleColumnHeader = false;
            this.cdvToValue.VisibleDescription = false;
            this.cdvToValue.ButtonPress += new System.EventHandler(this.cdvValue_ButtonPress);
            this.cdvToValue.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvValue_SelectedItemChanged);
            // 
            // cdvFromValue
            // 
            this.cdvFromValue.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvFromValue.BorderHotColor = System.Drawing.Color.Black;
            this.cdvFromValue.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvFromValue.BtnToolTipText = "";
            this.cdvFromValue.DescText = "";
            this.cdvFromValue.DisplaySubItemIndex = -1;
            this.cdvFromValue.DisplayText = "";
            this.cdvFromValue.Focusing = null;
            this.cdvFromValue.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvFromValue.Index = 0;
            this.cdvFromValue.IsViewBtnImage = false;
            this.cdvFromValue.Location = new System.Drawing.Point(0, 0);
            this.cdvFromValue.MaxLength = 32767;
            this.cdvFromValue.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvFromValue.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvFromValue.MultiSelect = false;
            this.cdvFromValue.Name = "cdvFromValue";
            this.cdvFromValue.ReadOnly = true;
            this.cdvFromValue.SearchSubItemIndex = 0;
            this.cdvFromValue.SelectedDescIndex = -1;
            this.cdvFromValue.SelectedSubItemIndex = -1;
            this.cdvFromValue.SelectedValueToQueryText = "";
            this.cdvFromValue.SelectionStart = 0;
            this.cdvFromValue.Size = new System.Drawing.Size(100, 21);
            this.cdvFromValue.SmallImageList = null;
            this.cdvFromValue.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvFromValue.TabIndex = 2;
            this.cdvFromValue.TextBoxToolTipText = "";
            this.cdvFromValue.TextBoxWidth = 100;
            this.cdvFromValue.VisibleButton = true;
            this.cdvFromValue.VisibleColumnHeader = false;
            this.cdvFromValue.VisibleDescription = false;
            this.cdvFromValue.ButtonPress += new System.EventHandler(this.cdvValue_ButtonPress);
            this.cdvFromValue.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvValue_SelectedItemChanged);
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
            // udcCUSFromToCondition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pnlValue);
            this.Controls.Add(this.pnlLabel);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "udcCUSFromToCondition";
            this.Size = new System.Drawing.Size(290, 21);
            this.pnlLabel.ResumeLayout(false);
            this.pnlValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdvToValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFromValue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblConditionLabel;
        private System.Windows.Forms.Panel pnlLabel;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvFromValue;
        private System.Windows.Forms.Panel pnlValue;
        protected System.Windows.Forms.ImageList imlSmallIcon;
        private Miracom.UI.Controls.MCCodeView.MCCodeView cdvToValue;
    }
}
