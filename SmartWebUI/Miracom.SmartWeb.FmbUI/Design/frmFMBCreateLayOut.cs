
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Infragistics.Win.UltraWinEditors;
using com.miracom.transceiverx.session;
using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;
//using Miracom.MESCore;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : frmFMBCreateLayOut.vb
//   Description : Create/Update/Delete LayOut
//
//   FMB Version : 1.0.0
//
//   Function List
//       - CheckCondition() : Check the conditions before transaction
//       - View_LayOut() : View Layout Information
//       - Update_LayOut() : Create/Update/Delete Layout
//       - Exist_Control_LayOut() : Check Exist Control Out of the LayOut
//       - Move_Controls_LayOut() : Move Controls by Force
//
//   Detail Description
//       - 2005-02-11 : Created by H.K.Kim
//
//   History
//       -
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
	public class frmFMBCreateLayOut : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBCreateLayOut()
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			
		}
		
		public frmFMBCreateLayOut(string sLayout)
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			
			sSelectedLayout = sLayout;
			
		}
		
		//Form?Ä DisposeÎ•??¨Ï†ï?òÌïò??Íµ¨ÏÑ± ?îÏÜå Î™©Î°ù???ïÎ¶¨?©Îãà??
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!(components == null))
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		//Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
		private System.ComponentModel.Container components = null;
		
		//Ï∞∏Í≥†: ?§Ïùå ?ÑÎ°ú?úÏ???Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
		//Windows Form ?îÏûê?¥ÎÑàÎ•??¨Ïö©?òÏó¨ ?òÏ†ï?????àÏäµ?àÎã§.
		//ÏΩîÎìú ?∏ÏßëÍ∏∞Î? ?¨Ïö©?òÏó¨ ?òÏ†ï?òÏ? ÎßàÏã≠?úÏò§.
		internal System.Windows.Forms.Panel pnlList;
		internal System.Windows.Forms.Panel pnlBottom;
		internal Miracom.UI.Controls.MCListView.MCListView lisLayOutList;
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
		internal System.Windows.Forms.Panel pnlMid;
		internal System.Windows.Forms.GroupBox grpLayOut;
		internal UltraTextEditor txtLayOutID;
		internal UltraTextEditor txtDesc;
		internal System.Windows.Forms.Label lblLayOutID;
		internal System.Windows.Forms.Label lblDesc;
		internal System.Windows.Forms.Label lblUpdateUser;
		internal System.Windows.Forms.Label lblCreateUser;
		internal System.Windows.Forms.Label lblUpdateTime;
		internal UltraTextEditor txtCreateTime;
		internal System.Windows.Forms.Label lblCreateTime;
		internal System.Windows.Forms.Button btnUpdate;
		internal System.Windows.Forms.Button btnCreate;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Button btnDelete;
		internal UltraTextEditor txtUpdateUser;
		internal UltraTextEditor txtCreateUser;
		internal UltraTextEditor txtUpdateTime;
		internal System.Windows.Forms.Label lblHeight;
		internal UltraTextEditor txtHeight;
		internal UltraTextEditor txtWidth;
		internal System.Windows.Forms.Label lblWidth;
		internal System.Windows.Forms.ColumnHeader ColumnHeader3;
		internal System.Windows.Forms.ColumnHeader ColumnHeader4;
		internal System.Windows.Forms.Label lblFactory;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.pnlList = new System.Windows.Forms.Panel();
            this.lisLayOutList = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.grpLayOut = new System.Windows.Forms.GroupBox();
            this.cdvFactory = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblFactory = new System.Windows.Forms.Label();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblUpdateUser = new System.Windows.Forms.Label();
            this.txtUpdateUser = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtCreateUser = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblCreateUser = new System.Windows.Forms.Label();
            this.lblUpdateTime = new System.Windows.Forms.Label();
            this.txtUpdateTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtCreateTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblCreateTime = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblLayOutID = new System.Windows.Forms.Label();
            this.txtDesc = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtLayOutID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.pnlList.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMid.SuspendLayout();
            this.grpLayOut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFactory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayOutID)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.lisLayOutList);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlList.Location = new System.Drawing.Point(0, 0);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlList.Size = new System.Drawing.Size(458, 144);
            this.pnlList.TabIndex = 0;
            // 
            // lisLayOutList
            // 
            this.lisLayOutList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader4});
            this.lisLayOutList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisLayOutList.EnableSort = true;
            this.lisLayOutList.EnableSortIcon = true;
            this.lisLayOutList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lisLayOutList.FullRowSelect = true;
            this.lisLayOutList.Location = new System.Drawing.Point(3, 3);
            this.lisLayOutList.MultiSelect = false;
            this.lisLayOutList.Name = "lisLayOutList";
            this.lisLayOutList.Size = new System.Drawing.Size(452, 141);
            this.lisLayOutList.TabIndex = 0;
            this.lisLayOutList.UseCompatibleStateImageBehavior = false;
            this.lisLayOutList.View = System.Windows.Forms.View.Details;
            this.lisLayOutList.SelectedIndexChanged += new System.EventHandler(this.lisLayOutList_SelectedIndexChanged);
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "LayOut ID";
            this.ColumnHeader1.Width = 170;
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Description";
            this.ColumnHeader2.Width = 170;
            // 
            // ColumnHeader3
            // 
            this.ColumnHeader3.Text = "Width";
            this.ColumnHeader3.Width = 45;
            // 
            // ColumnHeader4
            // 
            this.ColumnHeader4.Text = "Height";
            this.ColumnHeader4.Width = 45;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnDelete);
            this.pnlBottom.Controls.Add(this.btnUpdate);
            this.pnlBottom.Controls.Add(this.btnCreate);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 312);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(458, 40);
            this.pnlBottom.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Location = new System.Drawing.Point(374, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDelete.Location = new System.Drawing.Point(294, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 23);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUpdate.Location = new System.Drawing.Point(214, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(74, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.GotFocus += new System.EventHandler(this.btnUpdate_GotFocus);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreate.Location = new System.Drawing.Point(134, 9);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(74, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            this.btnCreate.GotFocus += new System.EventHandler(this.btnCreate_GotFocus);
            // 
            // pnlMid
            // 
            this.pnlMid.Controls.Add(this.grpLayOut);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMid.Location = new System.Drawing.Point(0, 144);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlMid.Size = new System.Drawing.Size(458, 168);
            this.pnlMid.TabIndex = 1;
            // 
            // grpLayOut
            // 
            this.grpLayOut.Controls.Add(this.cdvFactory);
            this.grpLayOut.Controls.Add(this.lblFactory);
            this.grpLayOut.Controls.Add(this.lblHeight);
            this.grpLayOut.Controls.Add(this.txtHeight);
            this.grpLayOut.Controls.Add(this.txtWidth);
            this.grpLayOut.Controls.Add(this.lblWidth);
            this.grpLayOut.Controls.Add(this.lblUpdateUser);
            this.grpLayOut.Controls.Add(this.txtUpdateUser);
            this.grpLayOut.Controls.Add(this.txtCreateUser);
            this.grpLayOut.Controls.Add(this.lblCreateUser);
            this.grpLayOut.Controls.Add(this.lblUpdateTime);
            this.grpLayOut.Controls.Add(this.txtUpdateTime);
            this.grpLayOut.Controls.Add(this.txtCreateTime);
            this.grpLayOut.Controls.Add(this.lblCreateTime);
            this.grpLayOut.Controls.Add(this.lblDesc);
            this.grpLayOut.Controls.Add(this.lblLayOutID);
            this.grpLayOut.Controls.Add(this.txtDesc);
            this.grpLayOut.Controls.Add(this.txtLayOutID);
            this.grpLayOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLayOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpLayOut.Location = new System.Drawing.Point(3, 0);
            this.grpLayOut.Name = "grpLayOut";
            this.grpLayOut.Size = new System.Drawing.Size(452, 168);
            this.grpLayOut.TabIndex = 0;
            this.grpLayOut.TabStop = false;
            // 
            // cdvFactory
            // 
            this.cdvFactory.BorderColor = System.Drawing.Color.DarkGray;
            this.cdvFactory.BorderHotColor = System.Drawing.Color.Black;
            this.cdvFactory.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvFactory.BtnToolTipText = "";
            this.cdvFactory.DescText = "";
            this.cdvFactory.DisplaySubItemIndex = -1;
            this.cdvFactory.DisplayText = "";
            this.cdvFactory.Focusing = null;
            this.cdvFactory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvFactory.Index = 0;
            this.cdvFactory.IsViewBtnImage = false;
            this.cdvFactory.Location = new System.Drawing.Point(103, 17);
            this.cdvFactory.MaxLength = 10;
            this.cdvFactory.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvFactory.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvFactory.Name = "cdvFactory";
            this.cdvFactory.ReadOnly = false;
            this.cdvFactory.SearchSubItemIndex = 0;
            this.cdvFactory.SelectedDescIndex = -1;
            this.cdvFactory.SelectedSubItemIndex = -1;
            this.cdvFactory.SelectionStart = 0;
            this.cdvFactory.Size = new System.Drawing.Size(136, 20);
            this.cdvFactory.SmallImageList = null;
            this.cdvFactory.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cdvFactory.TabIndex = 0;
            this.cdvFactory.TextBoxToolTipText = "";
            this.cdvFactory.TextBoxWidth = 136;
            this.cdvFactory.VisibleButton = true;
            this.cdvFactory.VisibleColumnHeader = false;
            this.cdvFactory.VisibleDescription = false;
            this.cdvFactory.ButtonPress += new System.EventHandler(this.cdvFactory_ButtonPress);
            this.cdvFactory.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(this.cdvFactory_SelectedItemChanged);
            // 
            // lblFactory
            // 
            this.lblFactory.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFactory.Location = new System.Drawing.Point(9, 20);
            this.lblFactory.Name = "lblFactory";
            this.lblFactory.Size = new System.Drawing.Size(88, 14);
            this.lblFactory.TabIndex = 78;
            this.lblFactory.Text = "Factory";
            // 
            // lblHeight
            // 
            this.lblHeight.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblHeight.Location = new System.Drawing.Point(231, 93);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(88, 14);
            this.lblHeight.TabIndex = 77;
            this.lblHeight.Text = "Height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(323, 89);
            this.txtHeight.MaxLength = 5;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(120, 19);
            this.txtHeight.TabIndex = 4;
            this.txtHeight.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHeight_KeyPress);
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(103, 89);
            this.txtWidth.MaxLength = 5;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(120, 19);
            this.txtWidth.TabIndex = 3;
            this.txtWidth.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWidth_KeyPress);
            // 
            // lblWidth
            // 
            this.lblWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblWidth.Location = new System.Drawing.Point(8, 93);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(88, 14);
            this.lblWidth.TabIndex = 74;
            this.lblWidth.Text = "Width";
            // 
            // lblUpdateUser
            // 
            this.lblUpdateUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUpdateUser.Location = new System.Drawing.Point(231, 117);
            this.lblUpdateUser.Name = "lblUpdateUser";
            this.lblUpdateUser.Size = new System.Drawing.Size(88, 14);
            this.lblUpdateUser.TabIndex = 73;
            this.lblUpdateUser.Text = "Update User ID";
            // 
            // txtUpdateUser
            // 
            this.txtUpdateUser.Location = new System.Drawing.Point(323, 113);
            this.txtUpdateUser.MaxLength = 20;
            this.txtUpdateUser.Name = "txtUpdateUser";
            this.txtUpdateUser.ReadOnly = true;
            this.txtUpdateUser.Size = new System.Drawing.Size(120, 19);
            this.txtUpdateUser.TabIndex = 6;
            this.txtUpdateUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.Location = new System.Drawing.Point(103, 113);
            this.txtCreateUser.MaxLength = 20;
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.ReadOnly = true;
            this.txtCreateUser.Size = new System.Drawing.Size(120, 19);
            this.txtCreateUser.TabIndex = 5;
            this.txtCreateUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // lblCreateUser
            // 
            this.lblCreateUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCreateUser.Location = new System.Drawing.Point(8, 117);
            this.lblCreateUser.Name = "lblCreateUser";
            this.lblCreateUser.Size = new System.Drawing.Size(88, 14);
            this.lblCreateUser.TabIndex = 70;
            this.lblCreateUser.Text = "Create User ID";
            // 
            // lblUpdateTime
            // 
            this.lblUpdateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUpdateTime.Location = new System.Drawing.Point(231, 141);
            this.lblUpdateTime.Name = "lblUpdateTime";
            this.lblUpdateTime.Size = new System.Drawing.Size(88, 14);
            this.lblUpdateTime.TabIndex = 69;
            this.lblUpdateTime.Text = "Update Time";
            // 
            // txtUpdateTime
            // 
            this.txtUpdateTime.Location = new System.Drawing.Point(323, 137);
            this.txtUpdateTime.MaxLength = 20;
            this.txtUpdateTime.Name = "txtUpdateTime";
            this.txtUpdateTime.ReadOnly = true;
            this.txtUpdateTime.Size = new System.Drawing.Size(120, 19);
            this.txtUpdateTime.TabIndex = 8;
            this.txtUpdateTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.Location = new System.Drawing.Point(103, 137);
            this.txtCreateTime.MaxLength = 20;
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.ReadOnly = true;
            this.txtCreateTime.Size = new System.Drawing.Size(120, 19);
            this.txtCreateTime.TabIndex = 7;
            this.txtCreateTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // lblCreateTime
            // 
            this.lblCreateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCreateTime.Location = new System.Drawing.Point(8, 141);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new System.Drawing.Size(88, 14);
            this.lblCreateTime.TabIndex = 66;
            this.lblCreateTime.Text = "Create Time";
            // 
            // lblDesc
            // 
            this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDesc.Location = new System.Drawing.Point(9, 68);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(88, 14);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Description";
            // 
            // lblLayOutID
            // 
            this.lblLayOutID.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblLayOutID.Location = new System.Drawing.Point(9, 44);
            this.lblLayOutID.Name = "lblLayOutID";
            this.lblLayOutID.Size = new System.Drawing.Size(88, 14);
            this.lblLayOutID.TabIndex = 4;
            this.lblLayOutID.Text = "LayOut ID";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(103, 65);
            this.txtDesc.MaxLength = 50;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(341, 19);
            this.txtDesc.TabIndex = 2;
            this.txtDesc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtLayOutID
            // 
            this.txtLayOutID.Location = new System.Drawing.Point(103, 41);
            this.txtLayOutID.MaxLength = 20;
            this.txtLayOutID.Name = "txtLayOutID";
            this.txtLayOutID.Size = new System.Drawing.Size(136, 19);
            this.txtLayOutID.TabIndex = 1;
            this.txtLayOutID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // frmFMBCreateLayOut
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(458, 352);
            this.Controls.Add(this.pnlMid);
            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFMBCreateLayOut";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "FMB4001";
            this.Text = "Create LayOut";
            this.Load += new System.EventHandler(this.frmFMBCreateLayOut_Load);
            this.Activated += new System.EventHandler(this.frmFMBCreateLayOut_Activated);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmFMBCreateLayOut_KeyPress);
            this.pnlList.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.pnlMid.ResumeLayout(false);
            this.grpLayOut.ResumeLayout(false);
            this.grpLayOut.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvFactory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLayOutID)).EndInit();
            this.ResumeLayout(false);

		}
		
		#endregion
		
		#region " Variable Definition"
		private bool bLoadFlag = false;
		private string sExistFlag = "";
		private string sSelectedLayout = "";
		#endregion
		
		#region " Function Implementations"
		
		// CheckCondition()
		//       - Check the conditions before transaction
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - FuncName as String  : Fuction Name
		//
		private bool CheckCondition(string FuncName)
		{
			
			try
			{
				if (txtLayOutID.Text == "")
				{
					CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
					txtLayOutID.Focus();
					return false;
				}
                switch (CmnFunction.RTrim(FuncName))
				{
					case "CREATE":
						
						if (CmnFunction.ToInt(txtWidth.Text) < modGlobalConstant.MIN_VALUE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(7) + modGlobalConstant.MIN_VALUE, "FMB Client", MessageBoxButtons.OK, 1);
							txtWidth.Focus();
							return false;
						}
						if (CmnFunction.ToInt(txtHeight.Text) < modGlobalConstant.MIN_VALUE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(7) + modGlobalConstant.MIN_VALUE, "FMB Client", MessageBoxButtons.OK, 1);
							txtHeight.Focus();
							return false;
						}
						if (CmnFunction.ToInt(txtWidth.Text) > modGlobalConstant.MAX_VALUE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(8) + modGlobalConstant.MAX_VALUE, "FMB Client", MessageBoxButtons.OK, 1);
							txtWidth.Focus();
							return false;
						}
						if (CmnFunction.ToInt(txtHeight.Text) > modGlobalConstant.MAX_VALUE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(8) + modGlobalConstant.MAX_VALUE, "FMB Client", MessageBoxButtons.OK, 1);
							txtHeight.Focus();
							return false;
						}
                        if (Microsoft.VisualBasic.Information.IsNumeric(txtWidth.Text) == false)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(17), "FMB Client", MessageBoxButtons.OK, 1);
							txtWidth.Focus();
							return false;
						}

                        if (Microsoft.VisualBasic.Information.IsNumeric(txtHeight.Text) == false)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(17), "FMB Client", MessageBoxButtons.OK, 1);
							txtHeight.Focus();
							return false;
						}
						break;
						
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.CheckCondition()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// View_LayOut()
		//       - View Layout Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool View_LayOut()
		{
			
			try
			{
                FMB_View_LayOut_In_Tag View_LayOut_In = new FMB_View_LayOut_In_Tag();
                FMB_View_LayOut_Out_Tag View_LayOut_Out = new FMB_View_LayOut_Out_Tag(); ;
				
				View_LayOut_In.h_passport = GlobalVariable.gsPassport;
				View_LayOut_In.h_language = GlobalVariable.gcLanguage;
				View_LayOut_In.h_factory = cdvFactory.Text;
				View_LayOut_In.h_user_id = GlobalVariable.gsUserID;
				View_LayOut_In.h_password = GlobalVariable.gsPassword;
				View_LayOut_In.h_proc_step = '1';
				View_LayOut_In.layout_id = lisLayOutList.SelectedItems[0].Text;
                
                if (FMBSender.FMB_View_LayOut(View_LayOut_In, ref View_LayOut_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
            
				if (View_LayOut_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_LayOut_Out.h_msg_code, View_LayOut_Out.h_msg, View_LayOut_Out.h_db_err_msg, View_LayOut_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				txtLayOutID.Text = CmnFunction.RTrim(View_LayOut_Out.layout_id);
				txtDesc.Text = CmnFunction.RTrim(View_LayOut_Out.layout_desc);
				txtCreateUser.Text = CmnFunction.RTrim(View_LayOut_Out.create_user_id);
				txtCreateTime.Text = CmnFunction.MakeDateFormat(View_LayOut_Out.create_time, DATE_TIME_FORMAT.NONE);
                txtUpdateUser.Text = CmnFunction.RTrim(View_LayOut_Out.update_user_id);
				txtUpdateTime.Text = CmnFunction.MakeDateFormat(View_LayOut_Out.update_time, DATE_TIME_FORMAT.NONE);
				txtWidth.Text = System.Convert.ToString(View_LayOut_Out.width);
				txtHeight.Text = System.Convert.ToString(View_LayOut_Out.height);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.View_LayOut()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_LayOut()
		//       - Create/Update/Delete Layout
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep   : Process Step
		//
		private bool Update_LayOut(char sStep)
		{
			
			try
			{
                FMB_Update_LayOut_In_Tag Update_LayOut_In = new FMB_Update_LayOut_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				//ListViewItem itm;
				
				Update_LayOut_In._C.h_passport = GlobalVariable.gsPassport;
				Update_LayOut_In._C.h_language = GlobalVariable.gcLanguage;
				Update_LayOut_In._C.h_factory = cdvFactory.Text;
				Update_LayOut_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_LayOut_In._C.h_password = GlobalVariable.gsPassword;
				Update_LayOut_In._C.h_proc_step = sStep;
				Update_LayOut_In._C.layout_id = txtLayOutID.Text;
				Update_LayOut_In._C.layout_desc = txtDesc.Text;
				
				Update_LayOut_In._C.width = CmnFunction.ToInt(txtWidth.Text);
				Update_LayOut_In._C.height = CmnFunction.ToInt(txtHeight.Text);


                if (FMBSender.FMB_Update_LayOut(Update_LayOut_In, ref Cmn_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
               
				if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.Update_LayOut()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Exist_Control_LayOut()
		//       - Check Exist Control Out of the LayOut
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		public bool Exist_Control_LayOut()
		{
			
			try
			{
                FMB_Exist_Control_In_Tag Exist_Control_In = new FMB_Exist_Control_In_Tag();
                FMB_Exist_Control_Out_Tag Exist_Control_Out = new FMB_Exist_Control_Out_Tag();
				
				Exist_Control_In._C.h_proc_step = '1';
				Exist_Control_In._C.h_passport = GlobalVariable.gsPassport;
				Exist_Control_In._C.h_language = GlobalVariable.gcLanguage;
				Exist_Control_In._C.h_user_id = GlobalVariable.gsUserID;
				Exist_Control_In._C.h_password = GlobalVariable.gsPassword;
				Exist_Control_In._C.h_factory = cdvFactory.Text;
				Exist_Control_In._C.layout_group_flag = 'L';
				Exist_Control_In._C.layout_id = txtLayOutID.Text;
				Exist_Control_In._C.width = CmnFunction.ToInt(txtWidth.Text);
				Exist_Control_In._C.height = CmnFunction.ToInt(txtHeight.Text);

                if (FMBSender.FMB_Exist_Control(Exist_Control_In, ref Exist_Control_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
       			
				if (Exist_Control_Out._C.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Exist_Control_Out._C.h_msg_code, Exist_Control_Out._C.h_msg, Exist_Control_Out._C.h_db_err_msg, Exist_Control_Out._C.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				if (Exist_Control_Out._C.exist_flag == 'Y')
				{
					sExistFlag = "Y";
				}
				else
				{
					sExistFlag = "";
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.Exist_Control_LayOut()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Move_Controls_LayOut()
		//       - Move Controls by Force
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		public bool Move_Controls_LayOut()
		{
			
			try
			{
                FMB_Moving_by_Force_In_Tag Moving_by_Force_In = new FMB_Moving_by_Force_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				
				Moving_by_Force_In._C.h_proc_step = '1';
				Moving_by_Force_In._C.h_passport = GlobalVariable.gsPassport;
				Moving_by_Force_In._C.h_language = GlobalVariable.gcLanguage;
				Moving_by_Force_In._C.h_user_id = GlobalVariable.gsUserID;
				Moving_by_Force_In._C.h_password = GlobalVariable.gsPassword;
				Moving_by_Force_In._C.h_factory = cdvFactory.Text;
				Moving_by_Force_In._C.layout_group_flag = 'L';
				Moving_by_Force_In._C.layout_id = txtLayOutID.Text;
				Moving_by_Force_In._C.width = CmnFunction.ToInt(txtWidth.Text);
				Moving_by_Force_In._C.height = CmnFunction.ToInt(txtHeight.Text);

                if (FMBSender.FMB_Moving_by_Force(Moving_by_Force_In, ref Cmn_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
            				
				if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.Move_Controls_LayOut()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		#endregion
		
		#region " Event Implementations"
		
		private void frmFMBCreateLayOut_Activated(object sender, System.EventArgs e)
		{
			
			try
			{
				if (bLoadFlag == false)
				{
					if (cdvFactory.Text == "")
					{
						cdvFactory.Text = GlobalVariable.gsFactory;
					}
					lisLayOutList.Focus();
                    if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == true)
					{
						if (lisLayOutList.Items.Count > 0)
						{
							if (sSelectedLayout == "")
							{
								lisLayOutList.Items[0].Selected = true;
							}
							else
							{
                                CmnFunction.FindListItem(lisLayOutList, sSelectedLayout, false);
							}
						}
					}
					else
					{
						return;
					}
					bLoadFlag = true;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.frmFMBCreateLayOut_Activated()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBCreateLayOut_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				modCommonFunction.CheckSecurityFormControl(this);
				modCommonFunction.GetTextboxStyle(this.Controls);
				modLanguageFunction.ToClientLanguage(this);
				CmnInitFunction.InitListView(lisLayOutList);
				
				modCommonFunction.CheckAllFactoryOption(cdvFactory);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.frmFMBCreateLayOut_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void lisLayOutList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				CmnFunction.FieldClear(grpLayOut, cdvFactory, null, null, null, null, false);
				if (lisLayOutList.SelectedItems.Count > 0)
				{
					txtLayOutID.Text = lisLayOutList.SelectedItems[0].Text;
					View_LayOut();
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.lisLayOutList_SelectedIndexChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				if (CheckCondition("CREATE") == true)
				{
					if (Update_LayOut(modGlobalConstant.MP_STEP_CREATE) == false)
					{
						return;
					}
					if (modInterface.gIMdiForm.RefreshDesignList("6", cdvFactory.Text, txtLayOutID.Text, "", "") == false)
					{
						return;
					}
                    if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == false)
					{
						return;
					}
					if (lisLayOutList.Items.Count > 0)
					{
                        CmnFunction.FindListItem(lisLayOutList, CmnFunction.RTrim(txtLayOutID.Text), false);
					}
					CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.btnCreate_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				string sDeletedFormName = "";
				DialogResult result;
				if (CheckCondition("CREATE") == true)
				{
					//Î≥ÄÍ≤ΩÌïò?§Îäî LayOut??Width?Ä Height Î∞ñÏúºÎ°?Ïª®Ìä∏Î°§Ïù¥ Ï°¥Ïû¨?òÎ©¥ Í∞ïÏ†úÎ°??¥Îèô ?úÌÇ¨Í≤ÉÏù∏ÏßÄ Î¨ºÏñ¥Î≥∏Îã§.
					if (Exist_Control_LayOut() == false)
					{
						return;
					}
					if (sExistFlag == "Y")
					{
						result = CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(9), "FMB Client", MessageBoxButtons.YesNoCancel, 1);
						//Í∞ïÏ†ú ?¥Îèô
						if (result == DialogResult.Yes)
						{
							if (Move_Controls_LayOut() == false)
							{
								return;
							}
							if (Update_LayOut(modGlobalConstant.MP_STEP_UPDATE) == false)
							{
								return;
							}
							sDeletedFormName = cdvFactory.Text + ":" + txtLayOutID.Text;
                            //foreach (System.Windows.Forms.Form tempLoopVar_frmChild in GlobalVariable.gfrmMDI.MdiChildren)
                            //{
                            //    frmChild = tempLoopVar_frmChild;
                            //    if (frmChild.Name == sDeletedFormName)
                            //    {
                            //        ((frmFMBDesign) frmChild).OriginalDesignSize = new Size(CmnFunction.ToInt(txtWidth.Text) + 20, CmnFunction.ToInt(txtHeight.Text) + 20);
                            //        double dScale = modCommonFunction.GetScale(((frmFMBDesign) frmChild).ZoomScale);
                            //        ((frmFMBDesign) frmChild).DesignSize = new Size(Convert.ToInt32(((frmFMBDesign) frmChild).OriginalDesignSize.Width * dScale), Convert.ToInt32(((frmFMBDesign) frmChild).OriginalDesignSize.Height * dScale));
                            //        if (((frmFMBDesign) frmChild).RefreshResourceListDetail() == false)
                            //        {
                            //            return;
                            //        }
                            goto endOfForLoop;
                            //    }
                            //}
endOfForLoop:
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
                            if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == false)
							{
								return;
							}
							if (lisLayOutList.Items.Count > 0)
							{
                                CmnFunction.FindListItem(lisLayOutList, CmnFunction.RTrim(txtLayOutID.Text), false);
							}
							//Í∞ïÏ†úÎ°??¥Îèô?úÌÇ§ÏßÄ ?äÍ≥† ?ÖÎ†•??Í∞íÏúºÎ°?Update
						}
						else if (result == DialogResult.No)
						{
							if (Update_LayOut(modGlobalConstant.MP_STEP_UPDATE) == false)
							{
								return;
							}
							sDeletedFormName = cdvFactory.Text + ":" + txtLayOutID.Text;
                            //foreach (System.Windows.Forms.Form tempLoopVar_frmChild in GlobalVariable.gfrmMDI.MdiChildren)
                            //{
                            //    frmChild = tempLoopVar_frmChild;
                            //    if (frmChild.Name == sDeletedFormName)
                            //    {
                            //        ((frmFMBDesign) frmChild).OriginalDesignSize = new Size(CmnFunction.ToInt(txtWidth.Text) + 20, CmnFunction.ToInt(txtHeight.Text) + 20);
                            //        double dScale = modCommonFunction.GetScale(((frmFMBDesign) frmChild).ZoomScale);
                            //        ((frmFMBDesign) frmChild).DesignSize = new Size(Convert.ToInt32(((frmFMBDesign) frmChild).OriginalDesignSize.Width * dScale), Convert.ToInt32(((frmFMBDesign) frmChild).OriginalDesignSize.Height * dScale));
                            goto endOfForLoop1;
                            //    }
                            //}
endOfForLoop1:
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
                            if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == false)
							{
								return;
							}
							if (lisLayOutList.Items.Count > 0)
							{
                                CmnFunction.FindListItem(lisLayOutList, CmnFunction.RTrim(txtLayOutID.Text), false);
							}
							//Update Cancle -> Do Nothihg
						}
						else if (result == DialogResult.Cancel)
						{
							View_LayOut();
							return;
						}
						
						//Î≥ÄÍ≤ΩÌïò?§Îäî LayOut??Width?Ä Height Î∞ñÏúºÎ°?Ïª®Ìä∏Î°§Ïù¥ Ï°¥Ïû¨?òÏ? ?äÏúºÎ©?Í∑∏ÎÉ• Update
					}
					else
					{
						if (Update_LayOut(modGlobalConstant.MP_STEP_UPDATE) == false)
						{
							return;
						}
						sDeletedFormName = cdvFactory.Text + ":" + txtLayOutID.Text;
                        //foreach (System.Windows.Forms.Form tempLoopVar_frmChild in GlobalVariable.gfrmMDI.MdiChildren)
                        //{
                        //    frmChild = tempLoopVar_frmChild;
                        //    if (frmChild.Name == sDeletedFormName)
                        //    {
                        //        ((frmFMBDesign) frmChild).OriginalDesignSize = new Size(CmnFunction.ToInt(txtWidth.Text) + 20, CmnFunction.ToInt(txtHeight.Text) + 20);
                        //        double dScale = modCommonFunction.GetScale(((frmFMBDesign) frmChild).ZoomScale);
                        //        ((frmFMBDesign) frmChild).DesignSize = new Size(Convert.ToInt32(((frmFMBDesign) frmChild).OriginalDesignSize.Width * dScale), Convert.ToInt32(((frmFMBDesign) frmChild).OriginalDesignSize.Height * dScale));
                        goto endOfForLoop2;
                        //    }
                        //}
endOfForLoop2:
						
						CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
                        if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == false)
						{
							return;
						}
						if (lisLayOutList.Items.Count > 0)
						{
                            CmnFunction.FindListItem(lisLayOutList, CmnFunction.RTrim(txtLayOutID.Text), false);
						}
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.btnUpdate_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnDelete_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				string sDeletedFormName = "";
				
				if (CheckCondition("DELETE") == true)
				{
					if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(5), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.No)
					{
						return;
					}
					if (Update_LayOut(modGlobalConstant.MP_STEP_DELETE) == true)
					{
						if (modInterface.gIMdiForm.RefreshDesignList("1", cdvFactory.Text, txtLayOutID.Text, "", "") == false)
						{
							return;
						}
						sDeletedFormName = cdvFactory.Text + ":" + txtLayOutID.Text;
                        //foreach (System.Windows.Forms.Form tempLoopVar_frmChild in GlobalVariable.gfrmMDI.MdiChildren)
                        //{
                        //    frmChild = tempLoopVar_frmChild;
                        //    if (frmChild is frmFMBDesign)
                        //    {
                        //        if (frmChild.Name == sDeletedFormName)
                        //        {
                        //            ((frmFMBDesign) frmChild).DeleteFlag = true;
                        //            frmChild.Close();
                        goto endOfForLoop;
                        //        }
                        //    }
                        //}
endOfForLoop:
                        if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == true)
						{
							if (lisLayOutList.Items.Count > 0)
							{
								lisLayOutList.Items[0].Selected = true;
							}
						}
						else
						{
							return;
						}
					}
					CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.btnDelete_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnClose_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Dispose(false);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnCreate_GotFocus(object sender, System.EventArgs e)
		{
			
			try
			{
				if (CmnFunction.ToInt(txtWidth.Text) == 0)
				{
					txtWidth.Text = System.Convert.ToString(((Size) modGlobalVariable.gGlobalOptions.GetOptions(CmnFunction.RTrim(cdvFactory.Text), clsOptionData.Options.DefaultLayoutSize)).Width);
				}
				if (CmnFunction.ToInt(txtHeight.Text) == 0)
				{
                    txtHeight.Text = System.Convert.ToString(((Size)modGlobalVariable.gGlobalOptions.GetOptions(CmnFunction.RTrim(cdvFactory.Text), clsOptionData.Options.DefaultLayoutSize)).Height);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.btnCreate_GotFocus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnUpdate_GotFocus(object sender, System.EventArgs e)
		{
			
			try
			{
				if (CmnFunction.ToInt(txtWidth.Text) == 0)
				{
					txtWidth.Text = System.Convert.ToString(((Size) modGlobalVariable.gGlobalOptions.GetOptions(CmnFunction.RTrim(cdvFactory.Text), clsOptionData.Options.DefaultLayoutSize)).Width);
				}
				if (CmnFunction.ToInt(txtHeight.Text) == 0)
				{
                    txtHeight.Text = System.Convert.ToString(((Size)modGlobalVariable.gGlobalOptions.GetOptions(CmnFunction.RTrim(cdvFactory.Text), clsOptionData.Options.DefaultLayoutSize)).Height);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.btnUpdate_GotFocus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_ButtonPress(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				cdvFactory.Init();
				cdvFactory.Columns.Add("LayoutID", 100, HorizontalAlignment.Left);
				cdvFactory.Columns.Add("Desc", 100, HorizontalAlignment.Left);
				cdvFactory.SelectedSubItemIndex = 0;
				//cdvFactory.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
                //WIPLIST.ViewFactoryList(cdvFactory.GetListView, '1',null);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.cdvFactory_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_SelectedItemChanged(System.Object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
		{
			
			try
			{
				CmnFunction.FieldClear(this, cdvFactory, null, null, null, null, false);
                if (modListRoutine.ViewLayOutList(lisLayOutList, '1', cdvFactory.Text) == true)
				{
					if (lisLayOutList.Items.Count > 0)
					{
						lisLayOutList.Items[0].Selected = true;
					}
				}
				else
				{
					return;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.cdvFactory_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBCreateLayOut_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			try
			{
				if (!(this.ActiveControl == null))
				{
					if (this.ActiveControl is TextBox || this.ActiveControl is UltraTextEditor || this.ActiveControl is Miracom.UI.Controls.MCCodeView.MCCodeView)
					{
						if (e.KeyChar == Microsoft.VisualBasic.Strings.Chr(58))
						{
							e.Handled = true;
						}
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.frmFMBCreateLayOut_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void txtHeight_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			try
			{
				if (e.KeyChar != Microsoft.VisualBasic.Strings.Chr(13) && e.KeyChar != Microsoft.VisualBasic.Strings.Chr(8))
				{
					if (e.KeyChar < Microsoft.VisualBasic.Strings.Chr(48) || e.KeyChar > Microsoft.VisualBasic.Strings.Chr(57))
					{
						e.Handled = true;
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.txtHeight_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void txtWidth_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			try
			{
				if (e.KeyChar != Microsoft.VisualBasic.Strings.Chr(13) && e.KeyChar != Microsoft.VisualBasic.Strings.Chr(8))
				{
					if (e.KeyChar < Microsoft.VisualBasic.Strings.Chr(48) || e.KeyChar > Microsoft.VisualBasic.Strings.Chr(57))
					{
						e.Handled = true;
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.txtWidth_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		#endregion
		
	}
	
}
