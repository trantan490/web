namespace Miracom.SmartWeb.UI
{
	partial class SEC1103
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEC1103));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExcel = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lisFunc = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpFunction = new System.Windows.Forms.GroupBox();
            this.txtLang3 = new System.Windows.Forms.TextBox();
            this.txtLang2 = new System.Windows.Forms.TextBox();
            this.txtLang1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDeleteTime = new System.Windows.Forms.TextBox();
            this.txtUpdateTime = new System.Windows.Forms.TextBox();
            this.txtDeleteUser = new System.Windows.Forms.TextBox();
            this.txtCreateTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUpdateUser = new System.Windows.Forms.TextBox();
            this.lblUpdate = new System.Windows.Forms.Label();
            this.txtCreateUser = new System.Windows.Forms.TextBox();
            this.lblCreate = new System.Windows.Forms.Label();
            this.grpFunc = new System.Windows.Forms.GroupBox();
            this.txtRequester = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtFunc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFunc = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.txtLang5 = new System.Windows.Forms.TextBox();
            this.txtLang4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpFunction.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.grpFunc.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlTitle, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnExcel);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnUpdate);
            this.panel1.Controls.Add(this.btnCreate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 565);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 35);
            this.panel1.TabIndex = 3;
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
            this.btnExcel.TabIndex = 18;
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
            this.btnDelete.Location = new System.Drawing.Point(720, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 8;
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
            this.btnUpdate.Location = new System.Drawing.Point(646, 4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 23);
            this.btnUpdate.TabIndex = 7;
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
            this.btnCreate.Location = new System.Drawing.Point(572, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(70, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Create";
            this.btnCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.lisFunc);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpFunction);
            this.splitContainer1.Panel2.Controls.Add(this.GroupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.grpFunc);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer1.Size = new System.Drawing.Size(800, 537);
            this.splitContainer1.SplitterDistance = 264;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // lisFunc
            // 
            this.lisFunc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2});
            this.lisFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisFunc.EnableSort = true;
            this.lisFunc.EnableSortIcon = true;
            this.lisFunc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lisFunc.FullRowSelect = true;
            this.lisFunc.HideSelection = false;
            this.lisFunc.Location = new System.Drawing.Point(3, 3);
            this.lisFunc.MultiSelect = false;
            this.lisFunc.Name = "lisFunc";
            this.lisFunc.Size = new System.Drawing.Size(256, 529);
            this.lisFunc.TabIndex = 1;
            this.lisFunc.UseCompatibleStateImageBehavior = false;
            this.lisFunc.View = System.Windows.Forms.View.Details;
            this.lisFunc.SelectedIndexChanged += new System.EventHandler(this.lisFunc_SelectedIndexChanged);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Function";
            this.ColumnHeader1.Width = 100;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Description";
            this.ColumnHeader2.Width = 300;
            // 
            // grpFunction
            // 
            this.grpFunction.Controls.Add(this.txtLang5);
            this.grpFunction.Controls.Add(this.txtLang4);
            this.grpFunction.Controls.Add(this.label7);
            this.grpFunction.Controls.Add(this.label8);
            this.grpFunction.Controls.Add(this.txtLang3);
            this.grpFunction.Controls.Add(this.txtLang2);
            this.grpFunction.Controls.Add(this.txtLang1);
            this.grpFunction.Controls.Add(this.label5);
            this.grpFunction.Controls.Add(this.label4);
            this.grpFunction.Controls.Add(this.label3);
            this.grpFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFunction.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpFunction.Location = new System.Drawing.Point(3, 98);
            this.grpFunction.Name = "grpFunction";
            this.grpFunction.Size = new System.Drawing.Size(525, 333);
            this.grpFunction.TabIndex = 2;
            this.grpFunction.TabStop = false;
            // 
            // txtLang3
            // 
            this.txtLang3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLang3.Location = new System.Drawing.Point(126, 69);
            this.txtLang3.MaxLength = 50;
            this.txtLang3.Name = "txtLang3";
            this.txtLang3.Size = new System.Drawing.Size(380, 21);
            this.txtLang3.TabIndex = 3;
            // 
            // txtLang2
            // 
            this.txtLang2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLang2.Location = new System.Drawing.Point(126, 42);
            this.txtLang2.MaxLength = 50;
            this.txtLang2.Name = "txtLang2";
            this.txtLang2.Size = new System.Drawing.Size(380, 21);
            this.txtLang2.TabIndex = 3;
            // 
            // txtLang1
            // 
            this.txtLang1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLang1.Location = new System.Drawing.Point(126, 15);
            this.txtLang1.MaxLength = 50;
            this.txtLang1.Name = "txtLang1";
            this.txtLang1.Size = new System.Drawing.Size(380, 21);
            this.txtLang1.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label5.Location = new System.Drawing.Point(9, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Language3 (Vietnam)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label4.Location = new System.Drawing.Point(9, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Language2 (Korea)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label3.Location = new System.Drawing.Point(9, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Language1 (English)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtDeleteTime);
            this.GroupBox1.Controls.Add(this.txtUpdateTime);
            this.GroupBox1.Controls.Add(this.txtDeleteUser);
            this.GroupBox1.Controls.Add(this.txtCreateTime);
            this.GroupBox1.Controls.Add(this.label2);
            this.GroupBox1.Controls.Add(this.txtUpdateUser);
            this.GroupBox1.Controls.Add(this.lblUpdate);
            this.GroupBox1.Controls.Add(this.txtCreateUser);
            this.GroupBox1.Controls.Add(this.lblCreate);
            this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.GroupBox1.Location = new System.Drawing.Point(3, 431);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(525, 101);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            // 
            // txtDeleteTime
            // 
            this.txtDeleteTime.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtDeleteTime.Location = new System.Drawing.Point(289, 67);
            this.txtDeleteTime.MaxLength = 20;
            this.txtDeleteTime.Name = "txtDeleteTime";
            this.txtDeleteTime.ReadOnly = true;
            this.txtDeleteTime.Size = new System.Drawing.Size(160, 21);
            this.txtDeleteTime.TabIndex = 10;
            this.txtDeleteTime.TabStop = false;
            // 
            // txtUpdateTime
            // 
            this.txtUpdateTime.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtUpdateTime.Location = new System.Drawing.Point(289, 42);
            this.txtUpdateTime.MaxLength = 20;
            this.txtUpdateTime.Name = "txtUpdateTime";
            this.txtUpdateTime.ReadOnly = true;
            this.txtUpdateTime.Size = new System.Drawing.Size(160, 21);
            this.txtUpdateTime.TabIndex = 10;
            this.txtUpdateTime.TabStop = false;
            // 
            // txtDeleteUser
            // 
            this.txtDeleteUser.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtDeleteUser.Location = new System.Drawing.Point(126, 67);
            this.txtDeleteUser.MaxLength = 20;
            this.txtDeleteUser.Name = "txtDeleteUser";
            this.txtDeleteUser.ReadOnly = true;
            this.txtDeleteUser.Size = new System.Drawing.Size(160, 21);
            this.txtDeleteUser.TabIndex = 9;
            this.txtDeleteUser.TabStop = false;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtCreateTime.Location = new System.Drawing.Point(289, 17);
            this.txtCreateTime.MaxLength = 20;
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.ReadOnly = true;
            this.txtCreateTime.Size = new System.Drawing.Size(160, 21);
            this.txtCreateTime.TabIndex = 7;
            this.txtCreateTime.TabStop = false;
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label2.Location = new System.Drawing.Point(17, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "Delete User/Time";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUpdateUser
            // 
            this.txtUpdateUser.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtUpdateUser.Location = new System.Drawing.Point(126, 42);
            this.txtUpdateUser.MaxLength = 20;
            this.txtUpdateUser.Name = "txtUpdateUser";
            this.txtUpdateUser.ReadOnly = true;
            this.txtUpdateUser.Size = new System.Drawing.Size(160, 21);
            this.txtUpdateUser.TabIndex = 9;
            this.txtUpdateUser.TabStop = false;
            // 
            // lblUpdate
            // 
            this.lblUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblUpdate.Location = new System.Drawing.Point(17, 46);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(86, 12);
            this.lblUpdate.TabIndex = 8;
            this.lblUpdate.Text = "Update User/Time";
            this.lblUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtCreateUser.Location = new System.Drawing.Point(126, 17);
            this.txtCreateUser.MaxLength = 20;
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.ReadOnly = true;
            this.txtCreateUser.Size = new System.Drawing.Size(160, 21);
            this.txtCreateUser.TabIndex = 6;
            this.txtCreateUser.TabStop = false;
            // 
            // lblCreate
            // 
            this.lblCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCreate.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lblCreate.Location = new System.Drawing.Point(17, 21);
            this.lblCreate.Name = "lblCreate";
            this.lblCreate.Size = new System.Drawing.Size(86, 12);
            this.lblCreate.TabIndex = 5;
            this.lblCreate.Text = "Create User/Time";
            this.lblCreate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpFunc
            // 
            this.grpFunc.Controls.Add(this.txtRequester);
            this.grpFunc.Controls.Add(this.txtDesc);
            this.grpFunc.Controls.Add(this.txtFunc);
            this.grpFunc.Controls.Add(this.label1);
            this.grpFunc.Controls.Add(this.lblFunc);
            this.grpFunc.Controls.Add(this.Label6);
            this.grpFunc.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFunc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpFunc.Location = new System.Drawing.Point(3, 3);
            this.grpFunc.Name = "grpFunc";
            this.grpFunc.Size = new System.Drawing.Size(525, 95);
            this.grpFunc.TabIndex = 1;
            this.grpFunc.TabStop = false;
            // 
            // txtRequester
            // 
            this.txtRequester.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtRequester.Location = new System.Drawing.Point(126, 66);
            this.txtRequester.MaxLength = 50;
            this.txtRequester.Name = "txtRequester";
            this.txtRequester.Size = new System.Drawing.Size(101, 21);
            this.txtRequester.TabIndex = 3;
            // 
            // txtDesc
            // 
            this.txtDesc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtDesc.Location = new System.Drawing.Point(126, 41);
            this.txtDesc.MaxLength = 50;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(380, 21);
            this.txtDesc.TabIndex = 3;
            // 
            // txtFunc
            // 
            this.txtFunc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtFunc.Location = new System.Drawing.Point(126, 17);
            this.txtFunc.MaxLength = 12;
            this.txtFunc.Name = "txtFunc";
            this.txtFunc.Size = new System.Drawing.Size(200, 21);
            this.txtFunc.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label1.Location = new System.Drawing.Point(17, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Requester";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFunc
            // 
            this.lblFunc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFunc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFunc.Location = new System.Drawing.Point(17, 21);
            this.lblFunc.Name = "lblFunc";
            this.lblFunc.Size = new System.Drawing.Size(86, 12);
            this.lblFunc.TabIndex = 0;
            this.lblFunc.Text = "Function Name";
            this.lblFunc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label6
            // 
            this.Label6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Label6.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Label6.Location = new System.Drawing.Point(17, 45);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(86, 12);
            this.Label6.TabIndex = 2;
            this.Label6.Text = "Description";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlTitle
            // 
            this.pnlTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlTitle.Controls.Add(this.button1);
            this.pnlTitle.Controls.Add(this.lblSubTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Margin = new System.Windows.Forms.Padding(0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(800, 25);
            this.pnlTitle.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Arial Black", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(777, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 19);
            this.button1.TabIndex = 15;
            this.button1.TabStop = false;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Location = new System.Drawing.Point(3, 7);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(121, 13);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Security Function Setup";
            // 
            // txtLang5
            // 
            this.txtLang5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLang5.Location = new System.Drawing.Point(126, 123);
            this.txtLang5.MaxLength = 50;
            this.txtLang5.Name = "txtLang5";
            this.txtLang5.Size = new System.Drawing.Size(380, 21);
            this.txtLang5.TabIndex = 6;
            // 
            // txtLang4
            // 
            this.txtLang4.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtLang4.Location = new System.Drawing.Point(126, 96);
            this.txtLang4.MaxLength = 50;
            this.txtLang4.Name = "txtLang4";
            this.txtLang4.Size = new System.Drawing.Size(380, 21);
            this.txtLang4.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label7.Location = new System.Drawing.Point(9, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Language5 (Etc)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label8.Location = new System.Drawing.Point(9, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "Language4 (Brazil)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SEC1103
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SEC1103";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.SEC1103_Load);
            this.Resize += new System.EventHandler(this.SEC1103_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpFunction.ResumeLayout(false);
            this.grpFunction.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.grpFunc.ResumeLayout(false);
            this.grpFunc.PerformLayout();
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label lblSubTitle;
		private System.Windows.Forms.SplitContainer splitContainer1;
		internal Miracom.UI.Controls.MCListView.MCListView lisFunc;
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.TextBox txtUpdateTime;
		internal System.Windows.Forms.TextBox txtCreateTime;
		internal System.Windows.Forms.TextBox txtUpdateUser;
		internal System.Windows.Forms.Label lblUpdate;
		internal System.Windows.Forms.TextBox txtCreateUser;
        internal System.Windows.Forms.Label lblCreate;
		internal System.Windows.Forms.TextBox txtDesc;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.TextBox txtFunc;
		internal System.Windows.Forms.Label lblFunc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Button button1;
        internal System.Windows.Forms.GroupBox grpFunc;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnExcel;
        private System.Windows.Forms.GroupBox grpFunction;
        internal System.Windows.Forms.TextBox txtRequester;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtDeleteTime;
        internal System.Windows.Forms.TextBox txtDeleteUser;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox txtLang3;
        internal System.Windows.Forms.TextBox txtLang2;
        internal System.Windows.Forms.TextBox txtLang1;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtLang5;
        internal System.Windows.Forms.TextBox txtLang4;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
	}
}
