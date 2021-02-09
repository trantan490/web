namespace Miracom.SmartWeb.UI.Controls
{
    partial class udcPeriod
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
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.cboPeriod = new System.Windows.Forms.ComboBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.dtpFromYear = new System.Windows.Forms.DateTimePicker();
            this.numFromWeek = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numFromWeek)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpToDate
            // 
            this.dtpToDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(195, 0);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(85, 21);
            this.dtpToDate.TabIndex = 24;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(106, 0);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(85, 21);
            this.dtpFromDate.TabIndex = 23;
            // 
            // cboPeriod
            // 
            this.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriod.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboPeriod.FormattingEnabled = true;
            this.cboPeriod.Items.AddRange(new object[] {
            "Period",
            "Date",
            "Week",
            "Current Day",
            "Previous Day",
            "Current Week",
            "Previous Week",
            "Current Month",
            "Previous Month"});
            this.cboPeriod.Location = new System.Drawing.Point(0, 0);
            this.cboPeriod.Name = "cboPeriod";
            this.cboPeriod.Size = new System.Drawing.Size(100, 21);
            this.cboPeriod.TabIndex = 22;
            this.cboPeriod.SelectedIndexChanged += new System.EventHandler(this.cboPeriod_SelectedIndexChanged);
            // 
            // lblPeriod
            // 
            this.lblPeriod.Location = new System.Drawing.Point(0, 4);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(100, 12);
            this.lblPeriod.TabIndex = 25;
            this.lblPeriod.Text = "Period";
            // 
            // dtpFromYear
            // 
            this.dtpFromYear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromYear.CustomFormat = "yyyy";
            this.dtpFromYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromYear.Location = new System.Drawing.Point(106, 0);
            this.dtpFromYear.Name = "dtpFromYear";
            this.dtpFromYear.ShowUpDown = true;
            this.dtpFromYear.Size = new System.Drawing.Size(60, 21);
            this.dtpFromYear.TabIndex = 26;
            // 
            // numFromWeek
            // 
            this.numFromWeek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numFromWeek.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.numFromWeek.Location = new System.Drawing.Point(171, 0);
            this.numFromWeek.Maximum = new decimal(new int[] {
            53,
            0,
            0,
            0});
            this.numFromWeek.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFromWeek.Name = "numFromWeek";
            this.numFromWeek.Size = new System.Drawing.Size(40, 21);
            this.numFromWeek.TabIndex = 28;
            this.numFromWeek.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFromWeek.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // udcPeriod
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.numFromWeek);
            this.Controls.Add(this.dtpFromYear);
            this.Controls.Add(this.dtpToDate);
            this.Controls.Add(this.dtpFromDate);
            this.Controls.Add(this.cboPeriod);
            this.Controls.Add(this.lblPeriod);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "udcPeriod";
            this.Size = new System.Drawing.Size(280, 21);
            ((System.ComponentModel.ISupportInitialize)(this.numFromWeek)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.ComboBox cboPeriod;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.DateTimePicker dtpFromYear;
        private System.Windows.Forms.NumericUpDown numFromWeek;

    }
}
