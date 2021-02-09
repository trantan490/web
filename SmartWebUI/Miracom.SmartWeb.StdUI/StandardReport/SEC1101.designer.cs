namespace Miracom.SmartWeb.UI
{
	partial class SEC1101
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEC1101));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lisUser = new Miracom.UI.Controls.MCListView.MCListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colDescription = new System.Windows.Forms.ColumnHeader();
            this.grpInfoExt = new System.Windows.Forms.GroupBox();
            this.chkClearLoginFailCount = new System.Windows.Forms.CheckBox();
            this.dtpExpireDate = new System.Windows.Forms.DateTimePicker();
            this.chkClearOldPassword = new System.Windows.Forms.CheckBox();
            this.chkChangePasswrdNextLogin = new System.Windows.Forms.CheckBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtPasswordChangePeriod = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.lblExpireDate = new System.Windows.Forms.Label();
            this.grpDateTime = new System.Windows.Forms.GroupBox();
            this.txtUpdateTime = new System.Windows.Forms.TextBox();
            this.txtCreateTime = new System.Windows.Forms.TextBox();
            this.txtUpdateUser = new System.Windows.Forms.TextBox();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.txtCreateUser = new System.Windows.Forms.TextBox();
            this.lblCreate = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.cdvCustomer = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.cdvSecGrpId = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.dtpRetireDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEnterDate = new System.Windows.Forms.DateTimePicker();
            this.rbnFemale = new System.Windows.Forms.RadioButton();
            this.rbnMale = new System.Windows.Forms.RadioButton();
            this.chkChangePassFlag = new System.Windows.Forms.CheckBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhoneOther = new System.Windows.Forms.TextBox();
            this.txtPhoneHome = new System.Windows.Forms.TextBox();
            this.txtPhoneMobile = new System.Windows.Forms.TextBox();
            this.txtPhoneOffice = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPhoneBirthday = new System.Windows.Forms.Label();
            this.lblRetireDate = new System.Windows.Forms.Label();
            this.lblEnterDate = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblPhoneEmail = new System.Windows.Forms.Label();
            this.lblPhoneOther = new System.Windows.Forms.Label();
            this.lblPhoneHome = new System.Windows.Forms.Label();
            this.lblPhoneMobile = new System.Windows.Forms.Label();
            this.lblPhoneOffice = new System.Windows.Forms.Label();
            this.lblSecGrpId = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.txtUserDesc = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.TxtUsrName = new System.Windows.Forms.TextBox();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpInfoExt.SuspendLayout();
            this.grpDateTime.SuspendLayout();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvSecGrpId)).BeginInit();
            this.grpUser.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.pnlTitle, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlBottom, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlTitle.Controls.Add(this.lblSubTitle);
            this.pnlTitle.Controls.Add(this.btnClose);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(800, 25);
            this.pnlTitle.TabIndex = 5;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Location = new System.Drawing.Point(3, 7);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(102, 13);
            this.lblSubTitle.TabIndex = 0;
            this.lblSubTitle.Text = "Security Setup User";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(777, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 19);
            this.btnClose.TabIndex = 15;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lisUser);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpInfoExt);
            this.splitContainer1.Panel2.Controls.Add(this.grpDateTime);
            this.splitContainer1.Panel2.Controls.Add(this.grpInfo);
            this.splitContainer1.Panel2.Controls.Add(this.grpUser);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(800, 537);
            this.splitContainer1.SplitterDistance = 240;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // lisUser
            // 
            this.lisUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colDescription});
            this.lisUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisUser.EnableSort = true;
            this.lisUser.EnableSortIcon = true;
            this.lisUser.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lisUser.FullRowSelect = true;
            this.lisUser.Location = new System.Drawing.Point(3, 3);
            this.lisUser.MultiSelect = false;
            this.lisUser.Name = "lisUser";
            this.lisUser.Size = new System.Drawing.Size(232, 529);
            this.lisUser.TabIndex = 1;
            this.lisUser.UseCompatibleStateImageBehavior = false;
            this.lisUser.View = System.Windows.Forms.View.Details;
            this.lisUser.SelectedIndexChanged += new System.EventHandler(this.lisUser_SelectedIndexChanged);
            // 
            // colName
            // 
            this.colName.Text = "User ID";
            this.colName.Width = 100;
            // 
            // colDescription
            // 
            this.colDescription.Text = "Description";
            this.colDescription.Width = 200;
            // 
            // grpInfoExt
            // 
            this.grpInfoExt.Controls.Add(this.chkClearLoginFailCount);
            this.grpInfoExt.Controls.Add(this.dtpExpireDate);
            this.grpInfoExt.Controls.Add(this.chkClearOldPassword);
            this.grpInfoExt.Controls.Add(this.chkChangePasswrdNextLogin);
            this.grpInfoExt.Controls.Add(this.Label2);
            this.grpInfoExt.Controls.Add(this.txtPasswordChangePeriod);
            this.grpInfoExt.Controls.Add(this.Label1);
            this.grpInfoExt.Controls.Add(this.lblExpireDate);
            this.grpInfoExt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfoExt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpInfoExt.Location = new System.Drawing.Point(3, 323);
            this.grpInfoExt.Name = "grpInfoExt";
            this.grpInfoExt.Size = new System.Drawing.Size(549, 142);
            this.grpInfoExt.TabIndex = 2;
            this.grpInfoExt.TabStop = false;
            // 
            // chkClearLoginFailCount
            // 
            this.chkClearLoginFailCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkClearLoginFailCount.Location = new System.Drawing.Point(304, 70);
            this.chkClearLoginFailCount.Name = "chkClearLoginFailCount";
            this.chkClearLoginFailCount.Size = new System.Drawing.Size(142, 13);
            this.chkClearLoginFailCount.TabIndex = 38;
            this.chkClearLoginFailCount.Text = "Clear Login Fail Count";
            // 
            // dtpExpireDate
            // 
            this.dtpExpireDate.CustomFormat = " ";
            this.dtpExpireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpireDate.Location = new System.Drawing.Point(126, 14);
            this.dtpExpireDate.Name = "dtpExpireDate";
            this.dtpExpireDate.Size = new System.Drawing.Size(110, 21);
            this.dtpExpireDate.TabIndex = 22;
            // 
            // chkClearOldPassword
            // 
            this.chkClearOldPassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkClearOldPassword.Location = new System.Drawing.Point(304, 43);
            this.chkClearOldPassword.Name = "chkClearOldPassword";
            this.chkClearOldPassword.Size = new System.Drawing.Size(130, 13);
            this.chkClearOldPassword.TabIndex = 37;
            this.chkClearOldPassword.Text = "Clear Old Password";
            // 
            // chkChangePasswrdNextLogin
            // 
            this.chkChangePasswrdNextLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkChangePasswrdNextLogin.Location = new System.Drawing.Point(14, 70);
            this.chkChangePasswrdNextLogin.Name = "chkChangePasswrdNextLogin";
            this.chkChangePasswrdNextLogin.Size = new System.Drawing.Size(218, 13);
            this.chkChangePasswrdNextLogin.TabIndex = 36;
            this.chkChangePasswrdNextLogin.Text = "Must Change Password At Next Login";
            // 
            // Label2
            // 
            this.Label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Label2.Location = new System.Drawing.Point(252, 43);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(34, 13);
            this.Label2.TabIndex = 41;
            this.Label2.Text = "days";
            // 
            // txtPasswordChangePeriod
            // 
            this.txtPasswordChangePeriod.Location = new System.Drawing.Point(126, 39);
            this.txtPasswordChangePeriod.MaxLength = 6;
            this.txtPasswordChangePeriod.Name = "txtPasswordChangePeriod";
            this.txtPasswordChangePeriod.Size = new System.Drawing.Size(110, 21);
            this.txtPasswordChangePeriod.TabIndex = 35;
            // 
            // Label1
            // 
            this.Label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Label1.Location = new System.Drawing.Point(14, 39);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(100, 25);
            this.Label1.TabIndex = 40;
            this.Label1.Text = "Password Change Period";
            // 
            // lblExpireDate
            // 
            this.lblExpireDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblExpireDate.Location = new System.Drawing.Point(14, 18);
            this.lblExpireDate.Name = "lblExpireDate";
            this.lblExpireDate.Size = new System.Drawing.Size(100, 13);
            this.lblExpireDate.TabIndex = 39;
            this.lblExpireDate.Text = "Expire Date";
            // 
            // grpDateTime
            // 
            this.grpDateTime.Controls.Add(this.txtUpdateTime);
            this.grpDateTime.Controls.Add(this.txtCreateTime);
            this.grpDateTime.Controls.Add(this.txtUpdateUser);
            this.grpDateTime.Controls.Add(this.lblUpdate);
            this.grpDateTime.Controls.Add(this.txtCreateUser);
            this.grpDateTime.Controls.Add(this.lblCreate);
            this.grpDateTime.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpDateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpDateTime.Location = new System.Drawing.Point(3, 465);
            this.grpDateTime.Name = "grpDateTime";
            this.grpDateTime.Size = new System.Drawing.Size(549, 67);
            this.grpDateTime.TabIndex = 3;
            this.grpDateTime.TabStop = false;
            // 
            // txtUpdateTime
            // 
            this.txtUpdateTime.Location = new System.Drawing.Point(289, 38);
            this.txtUpdateTime.MaxLength = 20;
            this.txtUpdateTime.Name = "txtUpdateTime";
            this.txtUpdateTime.ReadOnly = true;
            this.txtUpdateTime.Size = new System.Drawing.Size(160, 21);
            this.txtUpdateTime.TabIndex = 5;
            this.txtUpdateTime.TabStop = false;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.Location = new System.Drawing.Point(289, 13);
            this.txtCreateTime.MaxLength = 20;
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.ReadOnly = true;
            this.txtCreateTime.Size = new System.Drawing.Size(160, 21);
            this.txtCreateTime.TabIndex = 2;
            this.txtCreateTime.TabStop = false;
            // 
            // txtUpdateUser
            // 
            this.txtUpdateUser.Location = new System.Drawing.Point(126, 38);
            this.txtUpdateUser.MaxLength = 20;
            this.txtUpdateUser.Name = "txtUpdateUser";
            this.txtUpdateUser.ReadOnly = true;
            this.txtUpdateUser.Size = new System.Drawing.Size(160, 21);
            this.txtUpdateUser.TabIndex = 4;
            this.txtUpdateUser.TabStop = false;
            // 
            // lblUpdate
            // 
            this.lblUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUpdate.Location = new System.Drawing.Point(17, 42);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(86, 12);
            this.lblUpdate.TabIndex = 3;
            this.lblUpdate.Text = "Update User/Time";
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Location = new System.Drawing.Point(126, 13);
            this.txtCreateUser.MaxLength = 20;
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.ReadOnly = true;
            this.txtCreateUser.Size = new System.Drawing.Size(160, 21);
            this.txtCreateUser.TabIndex = 1;
            this.txtCreateUser.TabStop = false;
            // 
            // lblCreate
            // 
            this.lblCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCreate.Location = new System.Drawing.Point(17, 17);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(86, 12);
            this.lblCreate.TabIndex = 0;
            this.lblCreate.Text = "Create User/Time";
            this.lblCreate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.cdvCustomer);
            this.grpInfo.Controls.Add(this.lblCustomer);
            this.grpInfo.Controls.Add(this.cdvSecGrpId);
            this.grpInfo.Controls.Add(this.dtpBirthDate);
            this.grpInfo.Controls.Add(this.dtpRetireDate);
            this.grpInfo.Controls.Add(this.dtpEnterDate);
            this.grpInfo.Controls.Add(this.rbnFemale);
            this.grpInfo.Controls.Add(this.rbnMale);
            this.grpInfo.Controls.Add(this.chkChangePassFlag);
            this.grpInfo.Controls.Add(this.txtEmail);
            this.grpInfo.Controls.Add(this.txtPhoneOther);
            this.grpInfo.Controls.Add(this.txtPhoneHome);
            this.grpInfo.Controls.Add(this.txtPhoneMobile);
            this.grpInfo.Controls.Add(this.txtPhoneOffice);
            this.grpInfo.Controls.Add(this.txtPassword);
            this.grpInfo.Controls.Add(this.lblPhoneBirthday);
            this.grpInfo.Controls.Add(this.lblRetireDate);
            this.grpInfo.Controls.Add(this.lblEnterDate);
            this.grpInfo.Controls.Add(this.lblSex);
            this.grpInfo.Controls.Add(this.lblPhoneEmail);
            this.grpInfo.Controls.Add(this.lblPhoneOther);
            this.grpInfo.Controls.Add(this.lblPhoneHome);
            this.grpInfo.Controls.Add(this.lblPhoneMobile);
            this.grpInfo.Controls.Add(this.lblPhoneOffice);
            this.grpInfo.Controls.Add(this.lblSecGrpId);
            this.grpInfo.Controls.Add(this.lblPassword);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpInfo.Location = new System.Drawing.Point(3, 74);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(549, 249);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            // 
            // cdvCustomer
            // 
            this.cdvCustomer.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvCustomer.BorderHotColor = System.Drawing.Color.Black;
            this.cdvCustomer.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvCustomer.BtnToolTipText = "";
            this.cdvCustomer.DescText = "";
            this.cdvCustomer.DisplaySubItemIndex = -1;
            this.cdvCustomer.DisplayText = "";
            this.cdvCustomer.Enabled = false;
            this.cdvCustomer.Focusing = null;
            this.cdvCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvCustomer.Index = 0;
            this.cdvCustomer.IsViewBtnImage = false;
            this.cdvCustomer.Location = new System.Drawing.Point(126, 208);
            this.cdvCustomer.MaxLength = 20;
            this.cdvCustomer.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvCustomer.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvCustomer.MultiSelect = false;
            this.cdvCustomer.Name = "cdvCustomer";
            this.cdvCustomer.ReadOnly = true;
            this.cdvCustomer.SearchSubItemIndex = 0;
            this.cdvCustomer.SelectedDescIndex = -1;
            this.cdvCustomer.SelectedSubItemIndex = -1;
            this.cdvCustomer.SelectedValueToQueryText = "";
            this.cdvCustomer.SelectionStart = 0;
            this.cdvCustomer.Size = new System.Drawing.Size(160, 21);
            this.cdvCustomer.SmallImageList = null;
            this.cdvCustomer.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvCustomer.TabIndex = 24;
            this.cdvCustomer.TextBoxToolTipText = "";
            this.cdvCustomer.TextBoxWidth = 160;
            this.cdvCustomer.VisibleButton = true;
            this.cdvCustomer.VisibleColumnHeader = false;
            this.cdvCustomer.VisibleDescription = false;
            this.cdvCustomer.TextBoxTextChanged += new System.EventHandler(this.cdvCustomer_TextBoxTextChanged);
            this.cdvCustomer.ButtonPress += new System.EventHandler(this.cdvCustomer_ButtonPress);
            // 
            // lblCustomer
            // 
            this.lblCustomer.Enabled = false;
            this.lblCustomer.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomer.ForeColor = System.Drawing.Color.Red;
            this.lblCustomer.Location = new System.Drawing.Point(17, 212);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(100, 13);
            this.lblCustomer.TabIndex = 23;
            this.lblCustomer.Text = "Customer Code";
            // 
            // cdvSecGrpId
            // 
            this.cdvSecGrpId.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvSecGrpId.BorderHotColor = System.Drawing.Color.Black;
            this.cdvSecGrpId.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvSecGrpId.BtnToolTipText = "";
            this.cdvSecGrpId.DescText = "";
            this.cdvSecGrpId.DisplaySubItemIndex = -1;
            this.cdvSecGrpId.DisplayText = "";
            this.cdvSecGrpId.Focusing = null;
            this.cdvSecGrpId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvSecGrpId.Index = 0;
            this.cdvSecGrpId.IsViewBtnImage = false;
            this.cdvSecGrpId.Location = new System.Drawing.Point(126, 39);
            this.cdvSecGrpId.MaxLength = 20;
            this.cdvSecGrpId.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvSecGrpId.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvSecGrpId.MultiSelect = false;
            this.cdvSecGrpId.Name = "cdvSecGrpId";
            this.cdvSecGrpId.ReadOnly = false;
            this.cdvSecGrpId.SearchSubItemIndex = 0;
            this.cdvSecGrpId.SelectedDescIndex = -1;
            this.cdvSecGrpId.SelectedSubItemIndex = -1;
            this.cdvSecGrpId.SelectedValueToQueryText = "";
            this.cdvSecGrpId.SelectionStart = 0;
            this.cdvSecGrpId.Size = new System.Drawing.Size(160, 21);
            this.cdvSecGrpId.SmallImageList = null;
            this.cdvSecGrpId.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvSecGrpId.TabIndex = 3;
            this.cdvSecGrpId.TextBoxToolTipText = "";
            this.cdvSecGrpId.TextBoxWidth = 160;
            this.cdvSecGrpId.VisibleButton = true;
            this.cdvSecGrpId.VisibleColumnHeader = false;
            this.cdvSecGrpId.VisibleDescription = false;
            this.cdvSecGrpId.ButtonPress += new System.EventHandler(this.cdvSecGrpId_ButtonPress);
            this.cdvSecGrpId.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvSecGrpId_SelectedItemChanged);
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.CustomFormat = "  yyyy-MM-dd";
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBirthDate.Location = new System.Drawing.Point(416, 108);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(110, 21);
            this.dtpBirthDate.TabIndex = 22;
            // 
            // dtpRetireDate
            // 
            this.dtpRetireDate.CustomFormat = "  yyyy-MM-dd";
            this.dtpRetireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRetireDate.Location = new System.Drawing.Point(416, 158);
            this.dtpRetireDate.Name = "dtpRetireDate";
            this.dtpRetireDate.Size = new System.Drawing.Size(110, 21);
            this.dtpRetireDate.TabIndex = 22;
            // 
            // dtpEnterDate
            // 
            this.dtpEnterDate.CustomFormat = "  yyyy-MM-dd";
            this.dtpEnterDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnterDate.Location = new System.Drawing.Point(416, 133);
            this.dtpEnterDate.Name = "dtpEnterDate";
            this.dtpEnterDate.Size = new System.Drawing.Size(110, 21);
            this.dtpEnterDate.TabIndex = 22;
            // 
            // rbnFemale
            // 
            this.rbnFemale.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnFemale.Location = new System.Drawing.Point(201, 90);
            this.rbnFemale.Name = "rbnFemale";
            this.rbnFemale.Size = new System.Drawing.Size(100, 13);
            this.rbnFemale.TabIndex = 6;
            this.rbnFemale.Text = "Female";
            // 
            // rbnMale
            // 
            this.rbnMale.Checked = true;
            this.rbnMale.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.rbnMale.Location = new System.Drawing.Point(126, 90);
            this.rbnMale.Name = "rbnMale";
            this.rbnMale.Size = new System.Drawing.Size(100, 13);
            this.rbnMale.TabIndex = 5;
            this.rbnMale.TabStop = true;
            this.rbnMale.Text = "Male";
            // 
            // chkChangePassFlag
            // 
            this.chkChangePassFlag.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkChangePassFlag.Location = new System.Drawing.Point(304, 18);
            this.chkChangePassFlag.Name = "chkChangePassFlag";
            this.chkChangePassFlag.Size = new System.Drawing.Size(130, 13);
            this.chkChangePassFlag.TabIndex = 2;
            this.chkChangePassFlag.Text = "Change Password Flag";
            this.chkChangePassFlag.Visible = false;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(126, 63);
            this.txtEmail.MaxLength = 50;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 21);
            this.txtEmail.TabIndex = 4;
            // 
            // txtPhoneOther
            // 
            this.txtPhoneOther.Location = new System.Drawing.Point(126, 183);
            this.txtPhoneOther.MaxLength = 20;
            this.txtPhoneOther.Name = "txtPhoneOther";
            this.txtPhoneOther.Size = new System.Drawing.Size(160, 21);
            this.txtPhoneOther.TabIndex = 10;
            // 
            // txtPhoneHome
            // 
            this.txtPhoneHome.Location = new System.Drawing.Point(126, 133);
            this.txtPhoneHome.MaxLength = 20;
            this.txtPhoneHome.Name = "txtPhoneHome";
            this.txtPhoneHome.Size = new System.Drawing.Size(160, 21);
            this.txtPhoneHome.TabIndex = 8;
            // 
            // txtPhoneMobile
            // 
            this.txtPhoneMobile.Location = new System.Drawing.Point(126, 158);
            this.txtPhoneMobile.MaxLength = 20;
            this.txtPhoneMobile.Name = "txtPhoneMobile";
            this.txtPhoneMobile.Size = new System.Drawing.Size(160, 21);
            this.txtPhoneMobile.TabIndex = 9;
            // 
            // txtPhoneOffice
            // 
            this.txtPhoneOffice.Location = new System.Drawing.Point(126, 108);
            this.txtPhoneOffice.MaxLength = 20;
            this.txtPhoneOffice.Name = "txtPhoneOffice";
            this.txtPhoneOffice.Size = new System.Drawing.Size(160, 21);
            this.txtPhoneOffice.TabIndex = 7;
            // 
            // txtPassword
            // 
            this.txtPassword.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPassword.Location = new System.Drawing.Point(126, 14);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ReadOnly = true;
            this.txtPassword.Size = new System.Drawing.Size(160, 21);
            this.txtPassword.TabIndex = 1;
            // 
            // lblPhoneBirthday
            // 
            this.lblPhoneBirthday.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPhoneBirthday.Location = new System.Drawing.Point(322, 112);
            this.lblPhoneBirthday.Name = "lblPhoneBirthday";
            this.lblPhoneBirthday.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneBirthday.TabIndex = 17;
            this.lblPhoneBirthday.Text = "Birthday";
            this.lblPhoneBirthday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRetireDate
            // 
            this.lblRetireDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblRetireDate.Location = new System.Drawing.Point(322, 162);
            this.lblRetireDate.Name = "lblRetireDate";
            this.lblRetireDate.Size = new System.Drawing.Size(100, 13);
            this.lblRetireDate.TabIndex = 21;
            this.lblRetireDate.Text = "Retire Date";
            this.lblRetireDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEnterDate
            // 
            this.lblEnterDate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblEnterDate.Location = new System.Drawing.Point(322, 137);
            this.lblEnterDate.Name = "lblEnterDate";
            this.lblEnterDate.Size = new System.Drawing.Size(100, 13);
            this.lblEnterDate.TabIndex = 19;
            this.lblEnterDate.Text = "Enter Date";
            this.lblEnterDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSex
            // 
            this.lblSex.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSex.Location = new System.Drawing.Point(17, 90);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(100, 13);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "Sex";
            // 
            // lblPhoneEmail
            // 
            this.lblPhoneEmail.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPhoneEmail.Location = new System.Drawing.Point(17, 67);
            this.lblPhoneEmail.Name = "lblPhoneEmail";
            this.lblPhoneEmail.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneEmail.TabIndex = 7;
            this.lblPhoneEmail.Text = "Email";
            // 
            // lblPhoneOther
            // 
            this.lblPhoneOther.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPhoneOther.Location = new System.Drawing.Point(17, 186);
            this.lblPhoneOther.Name = "lblPhoneOther";
            this.lblPhoneOther.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneOther.TabIndex = 15;
            this.lblPhoneOther.Text = "Phone Other";
            // 
            // lblPhoneHome
            // 
            this.lblPhoneHome.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPhoneHome.Location = new System.Drawing.Point(17, 137);
            this.lblPhoneHome.Name = "lblPhoneHome";
            this.lblPhoneHome.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneHome.TabIndex = 13;
            this.lblPhoneHome.Text = "Phone Home";
            // 
            // lblPhoneMobile
            // 
            this.lblPhoneMobile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPhoneMobile.Location = new System.Drawing.Point(17, 162);
            this.lblPhoneMobile.Name = "lblPhoneMobile";
            this.lblPhoneMobile.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneMobile.TabIndex = 11;
            this.lblPhoneMobile.Text = "Phone Mobile";
            // 
            // lblPhoneOffice
            // 
            this.lblPhoneOffice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPhoneOffice.Location = new System.Drawing.Point(17, 112);
            this.lblPhoneOffice.Name = "lblPhoneOffice";
            this.lblPhoneOffice.Size = new System.Drawing.Size(100, 13);
            this.lblPhoneOffice.TabIndex = 9;
            this.lblPhoneOffice.Text = "Phone Office";
            // 
            // lblSecGrpId
            // 
            this.lblSecGrpId.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblSecGrpId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSecGrpId.Location = new System.Drawing.Point(17, 43);
            this.lblSecGrpId.Name = "lblSecGrpId";
            this.lblSecGrpId.Size = new System.Drawing.Size(100, 13);
            this.lblSecGrpId.TabIndex = 3;
            this.lblSecGrpId.Text = "Security Group";
            // 
            // lblPassword
            // 
            this.lblPassword.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.Location = new System.Drawing.Point(17, 18);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 13);
            this.lblPassword.TabIndex = 0;
            this.lblPassword.Text = "Password";
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.lblDesc);
            this.grpUser.Controls.Add(this.txtUserId);
            this.grpUser.Controls.Add(this.txtUserDesc);
            this.grpUser.Controls.Add(this.lblUserId);
            this.grpUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpUser.Location = new System.Drawing.Point(3, 3);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(549, 71);
            this.grpUser.TabIndex = 4;
            this.grpUser.TabStop = false;
            // 
            // lblDesc
            // 
            this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDesc.Location = new System.Drawing.Point(17, 44);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(100, 13);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Description";
            // 
            // txtUserId
            // 
            this.txtUserId.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserId.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserId.Location = new System.Drawing.Point(126, 17);
            this.txtUserId.MaxLength = 20;
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.ReadOnly = true;
            this.txtUserId.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserId.Size = new System.Drawing.Size(200, 21);
            this.txtUserId.TabIndex = 1;
            // 
            // txtUserDesc
            // 
            this.txtUserDesc.Location = new System.Drawing.Point(126, 42);
            this.txtUserDesc.MaxLength = 50;
            this.txtUserDesc.Name = "txtUserDesc";
            this.txtUserDesc.ReadOnly = true;
            this.txtUserDesc.Size = new System.Drawing.Size(400, 21);
            this.txtUserDesc.TabIndex = 3;
            // 
            // lblUserId
            // 
            this.lblUserId.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserId.Location = new System.Drawing.Point(17, 21);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(100, 13);
            this.lblUserId.TabIndex = 0;
            this.lblUserId.Text = "User ID";
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBottom.Controls.Add(this.txtSearchName);
            this.pnlBottom.Controls.Add(this.TxtUsrName);
            this.pnlBottom.Controls.Add(this.btnExcel);
            this.pnlBottom.Controls.Add(this.btnDelete);
            this.pnlBottom.Controls.Add(this.btnUpdate);
            this.pnlBottom.Controls.Add(this.btnCreate);
            this.pnlBottom.Controls.Add(this.label3);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(0, 565);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(800, 35);
            this.pnlBottom.TabIndex = 2;
            // 
            // TxtUsrName
            // 
            this.TxtUsrName.Location = new System.Drawing.Point(122, 5);
            this.TxtUsrName.Name = "TxtUsrName";
            this.TxtUsrName.Size = new System.Drawing.Size(113, 21);
            this.TxtUsrName.TabIndex = 17;
            this.TxtUsrName.TextChanged += new System.EventHandler(this.TxtUsrName_TextChanged);
            // 
            // btnExcel
            // 
            this.btnExcel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnExcel.FlatAppearance.BorderSize = 0;
            this.btnExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcel.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExcel.Image")));
            this.btnExcel.Location = new System.Drawing.Point(5, 7);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(21, 19);
            this.btnExcel.TabIndex = 16;
            this.btnExcel.TabStop = false;
            this.btnExcel.UseVisualStyleBackColor = true;
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Miracom.SmartWeb.UI.Properties.Resources._014datadel;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(720, 5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Image = global::Miracom.SmartWeb.UI.Properties.Resources._013datasaveas;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(646, 5);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataopen;
            this.btnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreate.Location = new System.Drawing.Point(572, 5);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(70, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "Create";
            this.btnCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Visible = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(76, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "User ID";
            // 
            // txtSearchName
            // 
            this.txtSearchName.AcceptsReturn = true;
            this.txtSearchName.Location = new System.Drawing.Point(250, 5);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(110, 21);
            this.txtSearchName.TabIndex = 18;
            this.txtSearchName.TextChanged += new System.EventHandler(this.txtSearchName_TextChanged);
            // 
            // SEC1101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SEC1101";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.SEC1101_Load);
            this.Resize += new System.EventHandler(this.SEC1101_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpInfoExt.ResumeLayout(false);
            this.grpInfoExt.PerformLayout();
            this.grpDateTime.ResumeLayout(false);
            this.grpDateTime.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.grpInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cdvSecGrpId)).EndInit();
            this.grpUser.ResumeLayout(false);
            this.grpUser.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblSubTitle;
		private System.Windows.Forms.SplitContainer splitContainer1;
		internal System.Windows.Forms.TextBox txtUserDesc;
		internal System.Windows.Forms.TextBox txtUserId;
		internal System.Windows.Forms.Label lblDesc;
		internal System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Panel pnlBottom;
		internal Miracom.UI.Controls.MCListView.MCListView lisUser;
		internal System.Windows.Forms.ColumnHeader colName;
        internal System.Windows.Forms.ColumnHeader colDescription;
		internal System.Windows.Forms.GroupBox grpInfoExt;
		internal System.Windows.Forms.CheckBox chkClearLoginFailCount;
		private System.Windows.Forms.DateTimePicker dtpExpireDate;
		internal System.Windows.Forms.CheckBox chkClearOldPassword;
		internal System.Windows.Forms.CheckBox chkChangePasswrdNextLogin;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox txtPasswordChangePeriod;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Label lblExpireDate;
		internal System.Windows.Forms.GroupBox grpDateTime;
		internal System.Windows.Forms.TextBox txtUpdateTime;
		internal System.Windows.Forms.TextBox txtCreateTime;
		internal System.Windows.Forms.TextBox txtUpdateUser;
		internal System.Windows.Forms.Label lblUpdate;
		internal System.Windows.Forms.TextBox txtCreateUser;
		internal System.Windows.Forms.Label lblCreate;
		internal System.Windows.Forms.GroupBox grpInfo;
		private System.Windows.Forms.DateTimePicker dtpBirthDate;
		private System.Windows.Forms.DateTimePicker dtpRetireDate;
		private System.Windows.Forms.DateTimePicker dtpEnterDate;
		internal System.Windows.Forms.RadioButton rbnFemale;
		internal System.Windows.Forms.RadioButton rbnMale;
		internal System.Windows.Forms.CheckBox chkChangePassFlag;
		internal Miracom.UI.Controls.MCCodeView.MCCodeView cdvSecGrpId;
		internal System.Windows.Forms.TextBox txtEmail;
		internal System.Windows.Forms.TextBox txtPhoneOther;
		internal System.Windows.Forms.TextBox txtPhoneHome;
		internal System.Windows.Forms.TextBox txtPhoneMobile;
		internal System.Windows.Forms.TextBox txtPhoneOffice;
		internal System.Windows.Forms.TextBox txtPassword;
		internal System.Windows.Forms.Label lblPhoneBirthday;
		internal System.Windows.Forms.Label lblRetireDate;
		internal System.Windows.Forms.Label lblEnterDate;
		internal System.Windows.Forms.Label lblSex;
		internal System.Windows.Forms.Label lblPhoneEmail;
		internal System.Windows.Forms.Label lblPhoneOther;
		internal System.Windows.Forms.Label lblPhoneHome;
		internal System.Windows.Forms.Label lblPhoneMobile;
		internal System.Windows.Forms.Label lblPhoneOffice;
		internal System.Windows.Forms.Label lblSecGrpId;
        internal System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnExcel;
        internal Miracom.UI.Controls.MCCodeView.MCCodeView cdvCustomer;
        internal System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox TxtUsrName;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchName;
	}
}
