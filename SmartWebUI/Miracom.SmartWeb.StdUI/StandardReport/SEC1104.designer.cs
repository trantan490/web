namespace Miracom.SmartWeb.UI
{
	partial class SEC1104
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SEC1104));
            this.lblDesc = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.grpMenu = new System.Windows.Forms.GroupBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lisAvailFunc = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.pnlRightTop = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtMenu = new System.Windows.Forms.TextBox();
            this.lblMenuName = new System.Windows.Forms.Label();
            this.pnlAttachFuncMid = new System.Windows.Forms.Panel();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.tvMenu = new Miracom.UI.Controls.MCTreeView.MCTreeView();
            this.pnlGroup = new System.Windows.Forms.Panel();
            this.lblSecGrp = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.cdvSecGrp = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.pnlExpand = new System.Windows.Forms.Panel();
            this.btnExpandAll = new System.Windows.Forms.Button();
            this.btnCollapseAll = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpMenu.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.pnlRightTop.SuspendLayout();
            this.pnlAttachFuncMid.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvSecGrp)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlExpand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDesc
            // 
            this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.Location = new System.Drawing.Point(579, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(58, 12);
            this.lblDesc.TabIndex = 3;
            this.lblDesc.Text = "Description";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.grpMenu, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pnlGroup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // grpMenu
            // 
            this.grpMenu.BackColor = System.Drawing.Color.White;
            this.grpMenu.Controls.Add(this.pnlRight);
            this.grpMenu.Controls.Add(this.pnlAttachFuncMid);
            this.grpMenu.Controls.Add(this.pnlLeft);
            this.grpMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMenu.Location = new System.Drawing.Point(3, 73);
            this.grpMenu.Name = "grpMenu";
            this.grpMenu.Size = new System.Drawing.Size(794, 524);
            this.grpMenu.TabIndex = 0;
            this.grpMenu.TabStop = false;
            this.grpMenu.Resize += new System.EventHandler(this.grpMenu_Resize);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.lisAvailFunc);
            this.pnlRight.Controls.Add(this.pnlRightTop);
            this.pnlRight.Location = new System.Drawing.Point(425, 19);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(360, 493);
            this.pnlRight.TabIndex = 2;
            // 
            // lisAvailFunc
            // 
            this.lisAvailFunc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader5,
            this.ColumnHeader6});
            this.lisAvailFunc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisAvailFunc.EnableSort = true;
            this.lisAvailFunc.EnableSortIcon = true;
            this.lisAvailFunc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.lisAvailFunc.FullRowSelect = true;
            this.lisAvailFunc.Location = new System.Drawing.Point(0, 62);
            this.lisAvailFunc.Name = "lisAvailFunc";
            this.lisAvailFunc.Size = new System.Drawing.Size(360, 431);
            this.lisAvailFunc.TabIndex = 5;
            this.lisAvailFunc.UseCompatibleStateImageBehavior = false;
            this.lisAvailFunc.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader5
            // 
            this.ColumnHeader5.Text = "Available Function";
            this.ColumnHeader5.Width = 120;
            // 
            // ColumnHeader6
            // 
            this.ColumnHeader6.Text = "Description";
            this.ColumnHeader6.Width = 170;
            // 
            // pnlRightTop
            // 
            this.pnlRightTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlRightTop.Controls.Add(this.btnDelete);
            this.pnlRightTop.Controls.Add(this.btnUpdate);
            this.pnlRightTop.Controls.Add(this.btnCreate);
            this.pnlRightTop.Controls.Add(this.txtMenu);
            this.pnlRightTop.Controls.Add(this.lblMenuName);
            this.pnlRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRightTop.Location = new System.Drawing.Point(0, 0);
            this.pnlRightTop.Name = "pnlRightTop";
            this.pnlRightTop.Size = new System.Drawing.Size(360, 62);
            this.pnlRightTop.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::Miracom.SmartWeb.UI.Properties.Resources._014datadel;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(256, 31);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 23);
            this.btnDelete.TabIndex = 8;
            this.btnDelete.Text = "Detach";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Image = global::Miracom.SmartWeb.UI.Properties.Resources._013datasaveas;
            this.btnUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpdate.Location = new System.Drawing.Point(182, 31);
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
            this.btnCreate.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreate.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataopen;
            this.btnCreate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCreate.Location = new System.Drawing.Point(108, 31);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(70, 23);
            this.btnCreate.TabIndex = 6;
            this.btnCreate.Text = "Attach";
            this.btnCreate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtMenu
            // 
            this.txtMenu.Location = new System.Drawing.Point(108, 6);
            this.txtMenu.Name = "txtMenu";
            this.txtMenu.Size = new System.Drawing.Size(237, 21);
            this.txtMenu.TabIndex = 2;
            // 
            // lblMenuName
            // 
            this.lblMenuName.BackColor = System.Drawing.Color.White;
            this.lblMenuName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblMenuName.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblMenuName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblMenuName.Location = new System.Drawing.Point(15, 10);
            this.lblMenuName.Name = "lblMenuName";
            this.lblMenuName.Size = new System.Drawing.Size(86, 13);
            this.lblMenuName.TabIndex = 1;
            this.lblMenuName.Text = "Menu Name";
            this.lblMenuName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlAttachFuncMid
            // 
            this.pnlAttachFuncMid.Controls.Add(this.btnUp);
            this.pnlAttachFuncMid.Controls.Add(this.btnDown);
            this.pnlAttachFuncMid.Controls.Add(this.btnDel);
            this.pnlAttachFuncMid.Controls.Add(this.btnAdd);
            this.pnlAttachFuncMid.Location = new System.Drawing.Point(370, 140);
            this.pnlAttachFuncMid.Name = "pnlAttachFuncMid";
            this.pnlAttachFuncMid.Size = new System.Drawing.Size(47, 236);
            this.pnlAttachFuncMid.TabIndex = 1;
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUp.Location = new System.Drawing.Point(2, 186);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(24, 24);
            this.btnUp.TabIndex = 4;
            this.btnUp.Text = "▲";
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDown.Location = new System.Drawing.Point(2, 210);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 24);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "▼";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDel
            // 
            this.btnDel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDel.Location = new System.Drawing.Point(11, 109);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(24, 24);
            this.btnDel.TabIndex = 3;
            this.btnDel.Text = ">";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Location = new System.Drawing.Point(11, 80);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "<";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.tvMenu);
            this.pnlLeft.Controls.Add(this.pnlExpand);
            this.pnlLeft.Location = new System.Drawing.Point(0, 20);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(360, 493);
            this.pnlLeft.TabIndex = 0;
            // 
            // tvMenu
            // 
            this.tvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMenu.FullRowSelect = true;
            this.tvMenu.HideSelection = false;
            this.tvMenu.ItemHeight = 20;
            this.tvMenu.Location = new System.Drawing.Point(0, 36);
            this.tvMenu.Name = "tvMenu";
            this.tvMenu.SelectedNodes = ((System.Collections.ArrayList)(resources.GetObject("tvMenu.SelectedNodes")));
            this.tvMenu.Size = new System.Drawing.Size(360, 457);
            this.tvMenu.TabIndex = 1;
            this.tvMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvMenu_AfterSelect);
            this.tvMenu.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvMenu_NodeMouseClick);
            // 
            // pnlGroup
            // 
            this.pnlGroup.BackColor = System.Drawing.Color.Transparent;
            this.pnlGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGroup.Controls.Add(this.lblSecGrp);
            this.pnlGroup.Controls.Add(this.txtDesc);
            this.pnlGroup.Controls.Add(this.cdvSecGrp);
            this.pnlGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGroup.Location = new System.Drawing.Point(3, 28);
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Size = new System.Drawing.Size(794, 39);
            this.pnlGroup.TabIndex = 14;
            // 
            // lblSecGrp
            // 
            this.lblSecGrp.BackColor = System.Drawing.Color.White;
            this.lblSecGrp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblSecGrp.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSecGrp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblSecGrp.Location = new System.Drawing.Point(8, 12);
            this.lblSecGrp.Name = "lblSecGrp";
            this.lblSecGrp.Size = new System.Drawing.Size(86, 13);
            this.lblSecGrp.TabIndex = 0;
            this.lblSecGrp.Text = "Security Group";
            this.lblSecGrp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtDesc.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.txtDesc.Location = new System.Drawing.Point(328, 8);
            this.txtDesc.MaxLength = 50;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.Size = new System.Drawing.Size(458, 21);
            this.txtDesc.TabIndex = 3;
            this.txtDesc.TabStop = false;
            // 
            // cdvSecGrp
            // 
            this.cdvSecGrp.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvSecGrp.BorderHotColor = System.Drawing.Color.Black;
            this.cdvSecGrp.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvSecGrp.BtnToolTipText = "";
            this.cdvSecGrp.DescText = "";
            this.cdvSecGrp.DisplaySubItemIndex = -1;
            this.cdvSecGrp.DisplayText = "";
            this.cdvSecGrp.Focusing = null;
            this.cdvSecGrp.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.cdvSecGrp.Index = 0;
            this.cdvSecGrp.IsViewBtnImage = false;
            this.cdvSecGrp.Location = new System.Drawing.Point(154, 8);
            this.cdvSecGrp.MaxLength = 20;
            this.cdvSecGrp.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvSecGrp.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvSecGrp.MultiSelect = false;
            this.cdvSecGrp.Name = "cdvSecGrp";
            this.cdvSecGrp.ReadOnly = false;
            this.cdvSecGrp.SearchSubItemIndex = 0;
            this.cdvSecGrp.SelectedDescIndex = -1;
            this.cdvSecGrp.SelectedSubItemIndex = -1;
            this.cdvSecGrp.SelectedValueToQueryText = "";
            this.cdvSecGrp.SelectionStart = 0;
            this.cdvSecGrp.Size = new System.Drawing.Size(168, 21);
            this.cdvSecGrp.SmallImageList = null;
            this.cdvSecGrp.StyleBorder = System.Windows.Forms.BorderStyle.Fixed3D;
            this.cdvSecGrp.TabIndex = 1;
            this.cdvSecGrp.TextBoxToolTipText = "";
            this.cdvSecGrp.TextBoxWidth = 168;
            this.cdvSecGrp.VisibleButton = true;
            this.cdvSecGrp.VisibleColumnHeader = false;
            this.cdvSecGrp.VisibleDescription = false;
            this.cdvSecGrp.ButtonPress += new System.EventHandler(this.cdvSecGrp_ButtonPress);
            this.cdvSecGrp.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvSecGrp_SelectedItemChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.lblSubTitle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 25);
            this.panel2.TabIndex = 15;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnClose.Image = global::Miracom.SmartWeb.UI.Properties.Resources._011dataclose;
            this.btnClose.Location = new System.Drawing.Point(777, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 19);
            this.btnClose.TabIndex = 16;
            this.btnClose.TabStop = false;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Location = new System.Drawing.Point(3, 7);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(184, 13);
            this.lblSubTitle.TabIndex = 5;
            this.lblSubTitle.Text = "Security Setup Group Function Setup";
            // 
            // pnlExpand
            // 
            this.pnlExpand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlExpand.Controls.Add(this.btnCollapseAll);
            this.pnlExpand.Controls.Add(this.btnExpandAll);
            this.pnlExpand.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlExpand.Location = new System.Drawing.Point(0, 0);
            this.pnlExpand.Name = "pnlExpand";
            this.pnlExpand.Size = new System.Drawing.Size(360, 36);
            this.pnlExpand.TabIndex = 2;
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnExpandAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpandAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandAll.Image")));
            this.btnExpandAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExpandAll.Location = new System.Drawing.Point(9, 5);
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(90, 23);
            this.btnExpandAll.TabIndex = 7;
            this.btnExpandAll.Text = "Expand All";
            this.btnExpandAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExpandAll.UseVisualStyleBackColor = true;
            this.btnExpandAll.Click += new System.EventHandler(this.btnExpandAll_Click);
            // 
            // btnCollapseAll
            // 
            this.btnCollapseAll.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
            this.btnCollapseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCollapseAll.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapseAll.Image")));
            this.btnCollapseAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCollapseAll.Location = new System.Drawing.Point(103, 5);
            this.btnCollapseAll.Name = "btnCollapseAll";
            this.btnCollapseAll.Size = new System.Drawing.Size(90, 23);
            this.btnCollapseAll.TabIndex = 8;
            this.btnCollapseAll.Text = "Collapse All";
            this.btnCollapseAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCollapseAll.UseVisualStyleBackColor = true;
            this.btnCollapseAll.Click += new System.EventHandler(this.btnCollapseAll_Click);
            // 
            // SEC1104
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormStyle.BackColor = System.Drawing.SystemColors.Control;
            this.FormStyle.FormName = null;
            this.Name = "SEC1104";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.SEC1104_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpMenu.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.pnlRightTop.ResumeLayout(false);
            this.pnlRightTop.PerformLayout();
            this.pnlAttachFuncMid.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlGroup.ResumeLayout(false);
            this.pnlGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvSecGrp)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlExpand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        internal System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpMenu;
        private System.Windows.Forms.Panel pnlRight;
        internal Miracom.UI.Controls.MCListView.MCListView lisAvailFunc;
        internal System.Windows.Forms.ColumnHeader ColumnHeader5;
        internal System.Windows.Forms.ColumnHeader ColumnHeader6;
        private System.Windows.Forms.Panel pnlRightTop;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtMenu;
        internal System.Windows.Forms.Label lblMenuName;
        private System.Windows.Forms.Panel pnlAttachFuncMid;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlLeft;
        private Miracom.UI.Controls.MCTreeView.MCTreeView tvMenu;
        private System.Windows.Forms.Panel pnlGroup;
        internal System.Windows.Forms.Label lblSecGrp;
        internal System.Windows.Forms.TextBox txtDesc;
        internal Miracom.UI.Controls.MCCodeView.MCCodeView cdvSecGrp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlExpand;
        private System.Windows.Forms.Button btnCollapseAll;
        private System.Windows.Forms.Button btnExpandAll;
	}
}
