
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
//   File Name   : frmFMBCreateUDRGroup.vb
//   Description : Create/Update/Delete UDR Group
//
//   FMB Version : 1.0.0
//
//   Function List
//       - CheckCondition() : Check the conditions before transaction
//       - View_UDR_Group() : View User Define Resource Group Information
//       - Update_UDR_Group() : Create/Update/Delete User Define Resource Group
//       - Exist_Control_UDR_Group() : Check Exist Control Out of the User Define Group
//       - Move_Controls_UDR_Group() : Move Controls by Force
//
//   Detail Description
//       -
//
//   History
//       - 2005-02-11 : Created by H.K.Kim
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
	public class frmFMBCreateUDRGroup : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBCreateUDRGroup()
		{
			
			//???¸ì¶œ?€ Windows Form ?”ìž?´ë„ˆ???„ìš”?©ë‹ˆ??
			InitializeComponent();
			
			//InitializeComponent()ë¥??¸ì¶œ???¤ìŒ??ì´ˆê¸°???‘ì—…??ì¶”ê??˜ì‹­?œì˜¤.
			
		}
		
		public frmFMBCreateUDRGroup(string sUserGroup)
		{
			
			//???¸ì¶œ?€ Windows Form ?”ìž?´ë„ˆ???„ìš”?©ë‹ˆ??
			InitializeComponent();
			
			//InitializeComponent()ë¥??¸ì¶œ???¤ìŒ??ì´ˆê¸°???‘ì—…??ì¶”ê??˜ì‹­?œì˜¤.
			
			sSelectedUserGroup = sUserGroup;
			
		}
		
		//Form?€ Disposeë¥??¬ì •?˜í•˜??êµ¬ì„± ?”ì†Œ ëª©ë¡???•ë¦¬?©ë‹ˆ??
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
		
		//Windows Form ?”ìž?´ë„ˆ???„ìš”?©ë‹ˆ??
		private System.ComponentModel.Container components = null;
		
		//ì°¸ê³ : ?¤ìŒ ?„ë¡œ?œì???Windows Form ?”ìž?´ë„ˆ???„ìš”?©ë‹ˆ??
		//Windows Form ?”ìž?´ë„ˆë¥??¬ìš©?˜ì—¬ ?˜ì •?????ˆìŠµ?ˆë‹¤.
		//ì½”ë“œ ?¸ì§‘ê¸°ë? ?¬ìš©?˜ì—¬ ?˜ì •?˜ì? ë§ˆì‹­?œì˜¤.
        internal System.Windows.Forms.ColumnHeader ColumnHeader2;
        internal System.Windows.Forms.ColumnHeader ColumnHeader1;
        internal System.Windows.Forms.Panel pnlBottom;
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.Button btnDelete;
        internal System.Windows.Forms.Button btnUpdate;
        internal System.Windows.Forms.Button btnCreate;
        internal System.Windows.Forms.Panel pnlList;
        internal Miracom.UI.Controls.MCListView.MCListView lisGroupList;
        internal System.Windows.Forms.Panel pnlMid;
        internal System.Windows.Forms.ColumnHeader ColumnHeader3;
        internal System.Windows.Forms.ColumnHeader ColumnHeader4;
        internal System.Windows.Forms.TabPage tbpGeneral;
        internal System.Windows.Forms.TabPage tbpCopy;
        internal System.Windows.Forms.GroupBox grpGroup;
        internal System.Windows.Forms.Label lblHeight;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtHeight;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtWidth;
        internal System.Windows.Forms.Label lblWidth;
        internal System.Windows.Forms.Label lblUpdateTime;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtUpdateTime;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtCreateTime;
        internal System.Windows.Forms.Label lblCreateTime;
        internal System.Windows.Forms.Label lblDesc;
        internal System.Windows.Forms.Label lblGroupID;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtDesc;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtGroupID;
        internal System.Windows.Forms.GroupBox grpCopy;
        internal System.Windows.Forms.Label lblToGroup;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtToGroup;
        internal System.Windows.Forms.Button btnCopy;
        internal System.Windows.Forms.TabControl tabUDR;
        internal System.Windows.Forms.Splitter spdMid;
        internal System.Windows.Forms.TabPage tbpUser;
        internal System.Windows.Forms.Panel pnlUserMid;
        internal System.Windows.Forms.Panel pnlUserMidRight;
        internal Miracom.UI.Controls.MCListView.MCListView lisUserlist;
        internal System.Windows.Forms.Panel pnlGroup;
        internal Miracom.UI.Controls.MCCodeView.MCCodeView cdvGroup;
        internal System.Windows.Forms.Label lblGroup;
        internal System.Windows.Forms.Label lblUserList;
        internal System.Windows.Forms.Panel pnlUserMidLeft;
        internal Miracom.UI.Controls.MCListView.MCListView lisAttachUser;
        internal System.Windows.Forms.Label lblAttachUser;
        internal System.Windows.Forms.ColumnHeader ColumnHeader11;
        internal System.Windows.Forms.ColumnHeader ColumnHeader12;
        internal System.Windows.Forms.ColumnHeader ColumnHeader7;
        internal System.Windows.Forms.ColumnHeader ColumnHeader8;
        internal System.Windows.Forms.Panel pnlUserAttach;
        internal System.Windows.Forms.Button btnDel;
        internal System.Windows.Forms.Button btnAdd;
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.pnlList = new System.Windows.Forms.Panel();
            this.lisGroupList = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.pnlMid = new System.Windows.Forms.Panel();
            this.tabUDR = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.grpGroup = new System.Windows.Forms.GroupBox();
            this.lblHeight = new System.Windows.Forms.Label();
            this.txtHeight = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblUpdateTime = new System.Windows.Forms.Label();
            this.txtUpdateTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtCreateTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblCreateTime = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblGroupID = new System.Windows.Forms.Label();
            this.txtDesc = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.txtGroupID = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.tbpCopy = new System.Windows.Forms.TabPage();
            this.grpCopy = new System.Windows.Forms.GroupBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.lblToGroup = new System.Windows.Forms.Label();
            this.txtToGroup = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.tbpUser = new System.Windows.Forms.TabPage();
            this.pnlUserMid = new System.Windows.Forms.Panel();
            this.pnlUserAttach = new System.Windows.Forms.Panel();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlUserMidRight = new System.Windows.Forms.Panel();
            this.lisUserlist = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.pnlGroup = new System.Windows.Forms.Panel();
            this.cdvGroup = new Miracom.UI.Controls.MCCodeView.MCCodeView();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblUserList = new System.Windows.Forms.Label();
            this.pnlUserMidLeft = new System.Windows.Forms.Panel();
            this.lisAttachUser = new Miracom.UI.Controls.MCListView.MCListView();
            this.ColumnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.ColumnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.lblAttachUser = new System.Windows.Forms.Label();
            this.spdMid = new System.Windows.Forms.Splitter();
            this.pnlBottom.SuspendLayout();
            this.pnlList.SuspendLayout();
            this.pnlMid.SuspendLayout();
            this.tabUDR.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.grpGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupID)).BeginInit();
            this.tbpCopy.SuspendLayout();
            this.grpCopy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToGroup)).BeginInit();
            this.tbpUser.SuspendLayout();
            this.pnlUserMid.SuspendLayout();
            this.pnlUserAttach.SuspendLayout();
            this.pnlUserMidRight.SuspendLayout();
            this.pnlGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cdvGroup)).BeginInit();
            this.pnlUserMidLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // ColumnHeader2
            // 
            this.ColumnHeader2.Text = "Description";
            this.ColumnHeader2.Width = 120;
            // 
            // ColumnHeader1
            // 
            this.ColumnHeader1.Text = "Group ID";
            this.ColumnHeader1.Width = 100;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnDelete);
            this.pnlBottom.Controls.Add(this.btnUpdate);
            this.pnlBottom.Controls.Add(this.btnCreate);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 408);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(682, 40);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Location = new System.Drawing.Point(600, 11);
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
            this.btnDelete.Location = new System.Drawing.Point(520, 11);
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
            this.btnUpdate.Location = new System.Drawing.Point(440, 11);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(74, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCreate.Location = new System.Drawing.Point(360, 11);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(74, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "Create";
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.lisGroupList);
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlList.Location = new System.Drawing.Point(0, 0);
            this.pnlList.Name = "pnlList";
            this.pnlList.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlList.Size = new System.Drawing.Size(216, 408);
            this.pnlList.TabIndex = 0;
            // 
            // lisGroupList
            // 
            this.lisGroupList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader1,
            this.ColumnHeader2,
            this.ColumnHeader3,
            this.ColumnHeader4});
            this.lisGroupList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisGroupList.EnableSort = true;
            this.lisGroupList.EnableSortIcon = true;
            this.lisGroupList.FullRowSelect = true;
            this.lisGroupList.Location = new System.Drawing.Point(3, 3);
            this.lisGroupList.MultiSelect = false;
            this.lisGroupList.Name = "lisGroupList";
            this.lisGroupList.Size = new System.Drawing.Size(210, 405);
            this.lisGroupList.TabIndex = 0;
            this.lisGroupList.UseCompatibleStateImageBehavior = false;
            this.lisGroupList.View = System.Windows.Forms.View.Details;
            this.lisGroupList.SelectedIndexChanged += new System.EventHandler(this.lisGroupList_SelectedIndexChanged);
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
            // pnlMid
            // 
            this.pnlMid.Controls.Add(this.tabUDR);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMid.Location = new System.Drawing.Point(216, 0);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.pnlMid.Size = new System.Drawing.Size(466, 408);
            this.pnlMid.TabIndex = 1;
            // 
            // tabUDR
            // 
            this.tabUDR.Controls.Add(this.tbpGeneral);
            this.tabUDR.Controls.Add(this.tbpCopy);
            this.tabUDR.Controls.Add(this.tbpUser);
            this.tabUDR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabUDR.Location = new System.Drawing.Point(3, 3);
            this.tabUDR.Name = "tabUDR";
            this.tabUDR.SelectedIndex = 0;
            this.tabUDR.Size = new System.Drawing.Size(460, 405);
            this.tabUDR.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.grpGroup);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Size = new System.Drawing.Size(452, 379);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            // 
            // grpGroup
            // 
            this.grpGroup.Controls.Add(this.lblHeight);
            this.grpGroup.Controls.Add(this.txtHeight);
            this.grpGroup.Controls.Add(this.txtWidth);
            this.grpGroup.Controls.Add(this.lblWidth);
            this.grpGroup.Controls.Add(this.lblUpdateTime);
            this.grpGroup.Controls.Add(this.txtUpdateTime);
            this.grpGroup.Controls.Add(this.txtCreateTime);
            this.grpGroup.Controls.Add(this.lblCreateTime);
            this.grpGroup.Controls.Add(this.lblDesc);
            this.grpGroup.Controls.Add(this.lblGroupID);
            this.grpGroup.Controls.Add(this.txtDesc);
            this.grpGroup.Controls.Add(this.txtGroupID);
            this.grpGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpGroup.Location = new System.Drawing.Point(0, 0);
            this.grpGroup.Name = "grpGroup";
            this.grpGroup.Size = new System.Drawing.Size(452, 379);
            this.grpGroup.TabIndex = 1;
            this.grpGroup.TabStop = false;
            // 
            // lblHeight
            // 
            this.lblHeight.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblHeight.Location = new System.Drawing.Point(231, 69);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(88, 14);
            this.lblHeight.TabIndex = 81;
            this.lblHeight.Text = "Height";
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(323, 65);
            this.txtHeight.MaxLength = 5;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(120, 19);
            this.txtHeight.TabIndex = 3;
            this.txtHeight.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(103, 65);
            this.txtWidth.MaxLength = 5;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(120, 19);
            this.txtWidth.TabIndex = 2;
            this.txtWidth.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // lblWidth
            // 
            this.lblWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblWidth.Location = new System.Drawing.Point(9, 69);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(88, 14);
            this.lblWidth.TabIndex = 78;
            this.lblWidth.Text = "Width";
            // 
            // lblUpdateTime
            // 
            this.lblUpdateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUpdateTime.Location = new System.Drawing.Point(231, 93);
            this.lblUpdateTime.Name = "lblUpdateTime";
            this.lblUpdateTime.Size = new System.Drawing.Size(88, 14);
            this.lblUpdateTime.TabIndex = 69;
            this.lblUpdateTime.Text = "Update Time";
            // 
            // txtUpdateTime
            // 
            this.txtUpdateTime.Location = new System.Drawing.Point(323, 89);
            this.txtUpdateTime.MaxLength = 20;
            this.txtUpdateTime.Name = "txtUpdateTime";
            this.txtUpdateTime.ReadOnly = true;
            this.txtUpdateTime.Size = new System.Drawing.Size(120, 19);
            this.txtUpdateTime.TabIndex = 5;
            this.txtUpdateTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtCreateTime
            // 
            this.txtCreateTime.Location = new System.Drawing.Point(103, 89);
            this.txtCreateTime.MaxLength = 20;
            this.txtCreateTime.Name = "txtCreateTime";
            this.txtCreateTime.ReadOnly = true;
            this.txtCreateTime.Size = new System.Drawing.Size(120, 19);
            this.txtCreateTime.TabIndex = 4;
            this.txtCreateTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // lblCreateTime
            // 
            this.lblCreateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCreateTime.Location = new System.Drawing.Point(8, 93);
            this.lblCreateTime.Name = "lblCreateTime";
            this.lblCreateTime.Size = new System.Drawing.Size(88, 14);
            this.lblCreateTime.TabIndex = 66;
            this.lblCreateTime.Text = "Create Time";
            // 
            // lblDesc
            // 
            this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblDesc.Location = new System.Drawing.Point(9, 44);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(88, 14);
            this.lblDesc.TabIndex = 5;
            this.lblDesc.Text = "Description";
            // 
            // lblGroupID
            // 
            this.lblGroupID.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblGroupID.Location = new System.Drawing.Point(9, 20);
            this.lblGroupID.Name = "lblGroupID";
            this.lblGroupID.Size = new System.Drawing.Size(88, 14);
            this.lblGroupID.TabIndex = 4;
            this.lblGroupID.Text = "Group ID";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(103, 41);
            this.txtDesc.MaxLength = 50;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(341, 19);
            this.txtDesc.TabIndex = 1;
            this.txtDesc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // txtGroupID
            // 
            this.txtGroupID.Location = new System.Drawing.Point(103, 17);
            this.txtGroupID.MaxLength = 20;
            this.txtGroupID.Name = "txtGroupID";
            this.txtGroupID.Size = new System.Drawing.Size(136, 19);
            this.txtGroupID.TabIndex = 0;
            this.txtGroupID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // tbpCopy
            // 
            this.tbpCopy.Controls.Add(this.grpCopy);
            this.tbpCopy.Location = new System.Drawing.Point(4, 22);
            this.tbpCopy.Name = "tbpCopy";
            this.tbpCopy.Size = new System.Drawing.Size(452, 379);
            this.tbpCopy.TabIndex = 1;
            this.tbpCopy.Text = "Copy Group";
            // 
            // grpCopy
            // 
            this.grpCopy.Controls.Add(this.btnCopy);
            this.grpCopy.Controls.Add(this.lblToGroup);
            this.grpCopy.Controls.Add(this.txtToGroup);
            this.grpCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpCopy.Location = new System.Drawing.Point(0, 0);
            this.grpCopy.Name = "grpCopy";
            this.grpCopy.Size = new System.Drawing.Size(452, 379);
            this.grpCopy.TabIndex = 2;
            this.grpCopy.TabStop = false;
            // 
            // btnCopy
            // 
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCopy.Location = new System.Drawing.Point(252, 15);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(74, 23);
            this.btnCopy.TabIndex = 5;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // lblToGroup
            // 
            this.lblToGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblToGroup.Location = new System.Drawing.Point(9, 20);
            this.lblToGroup.Name = "lblToGroup";
            this.lblToGroup.Size = new System.Drawing.Size(88, 14);
            this.lblToGroup.TabIndex = 4;
            this.lblToGroup.Text = "To Group ID";
            // 
            // txtToGroup
            // 
            this.txtToGroup.Location = new System.Drawing.Point(103, 17);
            this.txtToGroup.MaxLength = 20;
            this.txtToGroup.Name = "txtToGroup";
            this.txtToGroup.Size = new System.Drawing.Size(136, 19);
            this.txtToGroup.TabIndex = 0;
            this.txtToGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            // 
            // tbpUser
            // 
            this.tbpUser.Controls.Add(this.pnlUserMid);
            this.tbpUser.Location = new System.Drawing.Point(4, 22);
            this.tbpUser.Name = "tbpUser";
            this.tbpUser.Size = new System.Drawing.Size(452, 379);
            this.tbpUser.TabIndex = 2;
            this.tbpUser.Text = "User Setup";
            // 
            // pnlUserMid
            // 
            this.pnlUserMid.Controls.Add(this.pnlUserAttach);
            this.pnlUserMid.Controls.Add(this.pnlUserMidRight);
            this.pnlUserMid.Controls.Add(this.pnlUserMidLeft);
            this.pnlUserMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUserMid.Location = new System.Drawing.Point(0, 0);
            this.pnlUserMid.Name = "pnlUserMid";
            this.pnlUserMid.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pnlUserMid.Size = new System.Drawing.Size(452, 379);
            this.pnlUserMid.TabIndex = 1;
            this.pnlUserMid.Resize += new System.EventHandler(this.pnlRight_Resize);
            // 
            // pnlUserAttach
            // 
            this.pnlUserAttach.Controls.Add(this.btnDel);
            this.pnlUserAttach.Controls.Add(this.btnAdd);
            this.pnlUserAttach.Location = new System.Drawing.Point(200, 112);
            this.pnlUserAttach.Name = "pnlUserAttach";
            this.pnlUserAttach.Size = new System.Drawing.Size(43, 120);
            this.pnlUserAttach.TabIndex = 20;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(9, 63);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(24, 24);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = ">";
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(9, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "<";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // pnlUserMidRight
            // 
            this.pnlUserMidRight.Controls.Add(this.lisUserlist);
            this.pnlUserMidRight.Controls.Add(this.pnlGroup);
            this.pnlUserMidRight.Controls.Add(this.lblUserList);
            this.pnlUserMidRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUserMidRight.Location = new System.Drawing.Point(252, 0);
            this.pnlUserMidRight.Name = "pnlUserMidRight";
            this.pnlUserMidRight.Size = new System.Drawing.Size(200, 376);
            this.pnlUserMidRight.TabIndex = 19;
            // 
            // lisUserlist
            // 
            this.lisUserlist.AllowDrop = true;
            this.lisUserlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader11,
            this.ColumnHeader12});
            this.lisUserlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisUserlist.EnableSort = true;
            this.lisUserlist.EnableSortIcon = true;
            this.lisUserlist.FullRowSelect = true;
            this.lisUserlist.Location = new System.Drawing.Point(0, 48);
            this.lisUserlist.Name = "lisUserlist";
            this.lisUserlist.Size = new System.Drawing.Size(200, 328);
            this.lisUserlist.TabIndex = 2;
            this.lisUserlist.TabStop = false;
            this.lisUserlist.UseCompatibleStateImageBehavior = false;
            this.lisUserlist.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader11
            // 
            this.ColumnHeader11.Text = "User ID";
            this.ColumnHeader11.Width = 80;
            // 
            // ColumnHeader12
            // 
            this.ColumnHeader12.Text = "Description";
            this.ColumnHeader12.Width = 150;
            // 
            // pnlGroup
            // 
            this.pnlGroup.Controls.Add(this.cdvGroup);
            this.pnlGroup.Controls.Add(this.lblGroup);
            this.pnlGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlGroup.Location = new System.Drawing.Point(0, 14);
            this.pnlGroup.Name = "pnlGroup";
            this.pnlGroup.Size = new System.Drawing.Size(200, 34);
            this.pnlGroup.TabIndex = 1;
            // 
            // cdvGroup
            // 
            this.cdvGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cdvGroup.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cdvGroup.BtnToolTipText = "";
            this.cdvGroup.DescText = "";
            this.cdvGroup.DisplaySubItemIndex = -1;
            this.cdvGroup.DisplayText = "";
            this.cdvGroup.Focusing = null;
            this.cdvGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cdvGroup.Index = 0;
            this.cdvGroup.IsViewBtnImage = false;
            this.cdvGroup.Location = new System.Drawing.Point(93, 7);
            this.cdvGroup.MaxLength = 20;
            this.cdvGroup.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.cdvGroup.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
            this.cdvGroup.Name = "cdvGroup";
            this.cdvGroup.ReadOnly = true;
            this.cdvGroup.SearchSubItemIndex = 0;
            this.cdvGroup.SelectedDescIndex = -1;
            this.cdvGroup.SelectedSubItemIndex = -1;
            this.cdvGroup.SelectionStart = 0;
            this.cdvGroup.Size = new System.Drawing.Size(99, 20);
            this.cdvGroup.SmallImageList = null;
            this.cdvGroup.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cdvGroup.TabIndex = 1;
            this.cdvGroup.TextBoxToolTipText = "";
            this.cdvGroup.TextBoxWidth = 99;
            this.cdvGroup.VisibleButton = true;
            this.cdvGroup.VisibleColumnHeader = false;
            this.cdvGroup.VisibleDescription = false;
            this.cdvGroup.ButtonPress += new System.EventHandler(this.cdvGroup_ButtonPress);
            // 
            // lblGroup
            // 
            this.lblGroup.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblGroup.Location = new System.Drawing.Point(4, 10);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(82, 14);
            this.lblGroup.TabIndex = 0;
            this.lblGroup.Text = "Security Group";
            // 
            // lblUserList
            // 
            this.lblUserList.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUserList.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUserList.Location = new System.Drawing.Point(0, 0);
            this.lblUserList.Name = "lblUserList";
            this.lblUserList.Size = new System.Drawing.Size(200, 14);
            this.lblUserList.TabIndex = 0;
            this.lblUserList.Text = " All User List";
            // 
            // pnlUserMidLeft
            // 
            this.pnlUserMidLeft.Controls.Add(this.lisAttachUser);
            this.pnlUserMidLeft.Controls.Add(this.lblAttachUser);
            this.pnlUserMidLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlUserMidLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlUserMidLeft.Name = "pnlUserMidLeft";
            this.pnlUserMidLeft.Size = new System.Drawing.Size(192, 376);
            this.pnlUserMidLeft.TabIndex = 16;
            // 
            // lisAttachUser
            // 
            this.lisAttachUser.AllowDrop = true;
            this.lisAttachUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader7,
            this.ColumnHeader8});
            this.lisAttachUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lisAttachUser.EnableSort = true;
            this.lisAttachUser.EnableSortIcon = true;
            this.lisAttachUser.FullRowSelect = true;
            this.lisAttachUser.Location = new System.Drawing.Point(0, 14);
            this.lisAttachUser.Name = "lisAttachUser";
            this.lisAttachUser.Size = new System.Drawing.Size(192, 362);
            this.lisAttachUser.TabIndex = 1;
            this.lisAttachUser.TabStop = false;
            this.lisAttachUser.UseCompatibleStateImageBehavior = false;
            this.lisAttachUser.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeader7
            // 
            this.ColumnHeader7.Text = "User ID";
            this.ColumnHeader7.Width = 80;
            // 
            // ColumnHeader8
            // 
            this.ColumnHeader8.Text = "Description";
            this.ColumnHeader8.Width = 150;
            // 
            // lblAttachUser
            // 
            this.lblAttachUser.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAttachUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblAttachUser.Location = new System.Drawing.Point(0, 0);
            this.lblAttachUser.Name = "lblAttachUser";
            this.lblAttachUser.Size = new System.Drawing.Size(192, 14);
            this.lblAttachUser.TabIndex = 0;
            this.lblAttachUser.Text = "Attached User List";
            // 
            // spdMid
            // 
            this.spdMid.Location = new System.Drawing.Point(216, 0);
            this.spdMid.Name = "spdMid";
            this.spdMid.Size = new System.Drawing.Size(3, 408);
            this.spdMid.TabIndex = 3;
            this.spdMid.TabStop = false;
            // 
            // frmFMBCreateUDRGroup
            // 
            this.AcceptButton = this.btnCreate;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(682, 448);
            this.Controls.Add(this.spdMid);
            this.Controls.Add(this.pnlMid);
            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(690, 482);
            this.Name = "frmFMBCreateUDRGroup";
            this.ShowInTaskbar = false;
            this.Tag = "FMB4002";
            this.Text = "Create UDR Group";
            this.Activated += new System.EventHandler(this.frmFMBCreateUDRGroup_Activated);
            this.Load += new System.EventHandler(this.frmFMBCreateUDRGroup_Load);
            this.pnlBottom.ResumeLayout(false);
            this.pnlList.ResumeLayout(false);
            this.pnlMid.ResumeLayout(false);
            this.tabUDR.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.grpGroup.ResumeLayout(false);
            this.grpGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUpdateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCreateTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGroupID)).EndInit();
            this.tbpCopy.ResumeLayout(false);
            this.grpCopy.ResumeLayout(false);
            this.grpCopy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToGroup)).EndInit();
            this.tbpUser.ResumeLayout(false);
            this.pnlUserMid.ResumeLayout(false);
            this.pnlUserAttach.ResumeLayout(false);
            this.pnlUserMidRight.ResumeLayout(false);
            this.pnlGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cdvGroup)).EndInit();
            this.pnlUserMidLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

		#endregion
		
		#region " Variable Definition"
		private bool bLoadFlag = false;
		private string sExistFlag = "";
		private string sSelectedUserGroup = "";
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
				if (txtGroupID.Text == "")
				{
					CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
					txtGroupID.Focus();
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
					case "COPY":
						
						if (lisGroupList.SelectedItems.Count == 0)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(27), "FMB Client", MessageBoxButtons.OK, 1);
							lisGroupList.Focus();
							return false;
						}
						if (txtToGroup.Text == "")
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
							lisGroupList.Focus();
							return false;
						}
						break;
                    case "USER_ATTACH":

                        if (lisGroupList.SelectedItems.Count == 0)
                        {
                            CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(27), "FMB Client", MessageBoxButtons.OK, 1);
                            lisGroupList.Focus();
                            return false;
                        }
                        if (lisUserlist.SelectedItems.Count == 0)
                        {
                            CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(27), "FMB Client", MessageBoxButtons.OK, 1);
                            lisUserlist.Focus();
                            return false;
                        }
                        break;
                    case "USER_DETACH":

                        if (lisGroupList.SelectedItems.Count == 0)
                        {
                            CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(27), "FMB Client", MessageBoxButtons.OK, 1);
                            lisGroupList.Focus();
                            return false;
                        }
                        if (lisAttachUser.SelectedItems.Count == 0)
                        {
                            CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(27), "FMB Client", MessageBoxButtons.OK, 1);
                            lisAttachUser.Focus();
                            return false;
                        }
                        break;
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.CheckCondition()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// View_UDR_Group()
		//       - View User Define Resource Group Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool View_UDR_Group()
		{
			
			try
			{
                FMB_View_UDR_Group_In_Tag View_UDR_Group_In = new FMB_View_UDR_Group_In_Tag();
                FMB_View_UDR_Group_Out_Tag View_UDR_Group_Out = new FMB_View_UDR_Group_Out_Tag();
				
				View_UDR_Group_In.h_passport = GlobalVariable.gsPassport;
				View_UDR_Group_In.h_language = GlobalVariable.gcLanguage;
				View_UDR_Group_In.h_factory = GlobalVariable.gsFactory;
				View_UDR_Group_In.h_user_id = GlobalVariable.gsUserID;
				View_UDR_Group_In.h_password = GlobalVariable.gsPassword;
				View_UDR_Group_In.h_proc_step = '1';
				View_UDR_Group_In.group_id = lisGroupList.SelectedItems[0].Text;

                if (FMBSender.FMB_View_UDR_Group(View_UDR_Group_In, ref View_UDR_Group_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }

				if (View_UDR_Group_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_UDR_Group_Out.h_msg_code, View_UDR_Group_Out.h_msg, View_UDR_Group_Out.h_db_err_msg, View_UDR_Group_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				txtGroupID.Text = CmnFunction.RTrim(View_UDR_Group_Out.group_id);
                txtDesc.Text = CmnFunction.RTrim(View_UDR_Group_Out.group_desc);
				txtCreateTime.Text = CmnFunction.MakeDateFormat(View_UDR_Group_Out.create_time, DATE_TIME_FORMAT.NONE);
				txtUpdateTime.Text = CmnFunction.MakeDateFormat(View_UDR_Group_Out.update_time, DATE_TIME_FORMAT.NONE);
				txtWidth.Text = System.Convert.ToString(View_UDR_Group_Out.width);
				txtHeight.Text = System.Convert.ToString(View_UDR_Group_Out.height);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.View_UDR_Group()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_UDR_Group()
		//       - Create/Update/Delete User Define Group
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep   : Process Step
		//
		private bool Update_UDR_Group(char sStep)
		{
			
			try
			{
                FMB_Update_UDR_Group_In_Tag Update_UDR_Group_In = new FMB_Update_UDR_Group_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				//ListViewItem itm;
				
				Update_UDR_Group_In._C.h_passport = GlobalVariable.gsPassport;
				Update_UDR_Group_In._C.h_language = GlobalVariable.gcLanguage;
				Update_UDR_Group_In._C.h_factory = GlobalVariable.gsFactory;
				Update_UDR_Group_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_UDR_Group_In._C.h_password = GlobalVariable.gsPassword;
				Update_UDR_Group_In._C.h_proc_step = sStep;
				Update_UDR_Group_In._C.group = txtGroupID.Text;
				Update_UDR_Group_In._C.group_desc = txtDesc.Text;
				Update_UDR_Group_In._C.width = CmnFunction.ToInt(txtWidth.Text);
				Update_UDR_Group_In._C.height = CmnFunction.ToInt(txtHeight.Text);

                if (FMBSender.FMB_Update_UDR_Group(Update_UDR_Group_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.Update_UDR_Group()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}

        private bool Attach_User(char sStep, string sUser, string sGroup)
        {

            try
            {
                FMB_Update_FMB_Group_In_Tag Attach_User_In = new FMB_Update_FMB_Group_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
                //string sRuleCheck;

                Attach_User_In._C.h_passport = GlobalVariable.gsPassport;
                Attach_User_In._C.h_language = GlobalVariable.gcLanguage;
                Attach_User_In._C.h_factory = GlobalVariable.gsFactory;
                Attach_User_In._C.h_user_id = GlobalVariable.gsUserID;
                Attach_User_In._C.h_password = GlobalVariable.gsPassword;
                Attach_User_In._C.h_proc_step = sStep;
                Attach_User_In._C.group = sGroup;
                Attach_User_In._C.user = sUser;

                if (FMBSender.FMB_Update_FMB_Group(Attach_User_In, ref Cmn_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
                if ((Cmn_Out.h_status_value != GlobalConstant.MP_SUCCESS_STATUS))
                {
                    CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg));
                    return false;
                }

                return true;
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmSPCAttachUserToChart.Attach_User()" + "\r\n" + ex.Message);
                return false;
            }

        }

        public void SelectClear(ListView list)
        {

            try
            {
                int i;
                for (i = 0; i <= list.Items.Count - 1; i++)
                {
                    list.Items[i].Selected = false;
                }
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("modCommonFunction.SelectClear()" + "\r\n" + ex.Message);
            }

        }

		// Update_UDR_Group()
		//       - Create/Update/Delete User Define Group
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep   : Process Step
		//
		private bool Copy_UDR_Group()
		{
			
			try
			{
                FMB_Copy_UDR_Group_In_Tag Copy_UDr_Group_In = new FMB_Copy_UDR_Group_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
                //ListViewItem itm;
				
				Copy_UDr_Group_In._C.h_passport = GlobalVariable.gsPassport;
				Copy_UDr_Group_In._C.h_language = GlobalVariable.gcLanguage;
				Copy_UDr_Group_In._C.h_factory = GlobalVariable.gsFactory;
				Copy_UDr_Group_In._C.h_user_id = GlobalVariable.gsUserID;
				Copy_UDr_Group_In._C.h_password = GlobalVariable.gsPassword;
				Copy_UDr_Group_In._C.h_proc_step = '1';
				Copy_UDr_Group_In._C.from_group = CmnFunction.Trim(lisGroupList.SelectedItems[0].Text);
                Copy_UDr_Group_In._C.to_group = CmnFunction.RTrim(txtToGroup.Text);

                if (FMBSender.FMB_Copy_UDR_Group(Copy_UDr_Group_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.Copy_UDr_Group()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Exist_Control_UDR_Group()
		//       - Check Exist Control Out of the User Define Group
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool Exist_Control_UDR_Group()
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
				Exist_Control_In._C.h_factory = GlobalVariable.gsFactory;
				Exist_Control_In._C.layout_group_flag = 'G';
				Exist_Control_In._C.group_id = txtGroupID.Text;
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
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.Exist_Control_UDR_Group()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Move_Controls_UDR_Group()
		//       - Move Controls by Force
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool Move_Controls_UDR_Group()
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
				Moving_by_Force_In._C.h_factory = GlobalVariable.gsFactory;
				Moving_by_Force_In._C.layout_group_flag = 'G';
				Moving_by_Force_In._C.group_id = txtGroupID.Text;
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
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.Move_Controls_UDR_Group()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		#endregion
		
		#region " Event Implementations"
		
		private void frmFMBCreateUDRGroup_Activated(object sender, System.EventArgs e)
		{
			
			try
			{
				if (bLoadFlag == false)
				{
					lisGroupList.Focus();
                    if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == true)
					{
						if (lisGroupList.Items.Count > 0)
						{
							if (sSelectedUserGroup == "")
							{
								lisGroupList.Items[0].Selected = true;
							}
							else
							{
								CmnFunction.FindListItem(lisGroupList, sSelectedUserGroup,false);
							}
						}
					}
					else
					{
						return;
					}
                    //SECLIST.ViewSECUserList(lisUserlist);
					bLoadFlag = true;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.frmFMBCreateUDRGroup_Activated()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBCreateUDRGroup_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				modCommonFunction.CheckSecurityFormControl(this);
				modCommonFunction.GetTextboxStyle(this.Controls);
				modLanguageFunction.ToClientLanguage(this);
				CmnInitFunction.InitListView(lisGroupList);
                CmnInitFunction.InitListView(lisAttachUser);
                CmnInitFunction.InitListView(lisUserlist);

                this.pnlRight_Resize(null, null);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.frmFMBCreateUDRGroup_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void lisGroupList_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				CmnFunction.FieldClear(grpGroup, null, null, null, null, null, false);
                CmnInitFunction.InitListView(lisAttachUser);
				if (lisGroupList.SelectedItems.Count > 0)
				{
					lisGroupList.Text = lisGroupList.SelectedItems[0].Text;
                    if (View_UDR_Group() == true)
                    {
                        modListRoutine.ViewFMBGroupList(lisAttachUser, '1', lisGroupList.SelectedItems[0].Text,"");
                    }
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.lisGroupList_SelectedIndexChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			
			try
			{
                if (CmnFunction.ToInt(txtWidth.Text) == 0)
                {
                    txtWidth.Text = ((Size)modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultUDRSize)).Width.ToString();
                }
                if (CmnFunction.ToInt(txtHeight.Text) == 0)
                {
                    txtHeight.Text = ((Size)modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultUDRSize)).Height.ToString();
                }
				if (CheckCondition("CREATE") == true)
				{
					if (Update_UDR_Group(modGlobalConstant.MP_STEP_CREATE) == false)
					{
						return;
					}
					if (modInterface.gIMdiForm.RefreshUdrGroupList() == false)
					{
						return;
					}
                    if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == false)
					{
						return;
					}
					if (lisGroupList.Items.Count > 0)
					{
                        CmnFunction.FindListItem(lisGroupList, CmnFunction.RTrim(txtGroupID.Text), false);
					}
					CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.btnCreate_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				string sDeletedFormName = "";
				DialogResult result;

                if (CmnFunction.ToInt(txtWidth.Text) == 0)
                {
                    txtWidth.Text = ((Size)modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultUDRSize)).Width.ToString();
                }
                if (CmnFunction.ToInt(txtHeight.Text) == 0)
                {
                    txtHeight.Text = ((Size)modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultUDRSize)).Height.ToString();
                }
				if (CheckCondition("CREATE") == true)
				{
					//ë³€ê²½í•˜?¤ëŠ” LayOut??Width?€ Height ë°–ìœ¼ë¡?ì»¨íŠ¸ë¡¤ì´ ì¡´ìž¬?˜ë©´ ê°•ì œë¡??´ë™ ?œí‚¬ê²ƒì¸ì§€ ë¬¼ì–´ë³¸ë‹¤.
					if (Exist_Control_UDR_Group() == false)
					{
						return;
					}
					if (sExistFlag == "Y")
					{
						result = CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(11), "FMB Client", MessageBoxButtons.YesNoCancel, 1);
						//ê°•ì œ ?´ë™
						if (result == DialogResult.Yes)
						{
							if (Move_Controls_UDR_Group() == false)
							{
								return;
							}
							if (Update_UDR_Group(modGlobalConstant.MP_STEP_UPDATE) == false)
							{
								return;
							}
							sDeletedFormName = txtGroupID.Text;
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
                            if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == false)
							{
								return;
							}
							if (lisGroupList.Items.Count > 0)
							{
                                CmnFunction.FindListItem(lisGroupList, CmnFunction.RTrim(txtGroupID.Text), false);
							}
							//ê°•ì œë¡??´ë™?œí‚¤ì§€ ?Šê³  ?…ë ¥??ê°’ìœ¼ë¡?Update
						}
						else if (result == DialogResult.No)
						{
							if (Update_UDR_Group(modGlobalConstant.MP_STEP_UPDATE) == false)
							{
								return;
							}
							sDeletedFormName = txtGroupID.Text;
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
                            if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == false)
							{
								return;
							}
							if (lisGroupList.Items.Count > 0)
							{
                                CmnFunction.FindListItem(lisGroupList, CmnFunction.RTrim(txtGroupID.Text), false);
							}
						}
						else if (result == DialogResult.Cancel)
						{
							View_UDR_Group();
							return;
						}
						//ë³€ê²½í•˜?¤ëŠ” LayOut??Width?€ Height ë°–ìœ¼ë¡?ì»¨íŠ¸ë¡¤ì´ ì¡´ìž¬?˜ì? ?Šìœ¼ë©?ê·¸ëƒ¥ Update
					}
					else
					{
						if (Update_UDR_Group(modGlobalConstant.MP_STEP_UPDATE) == false)
						{
							return;
						}
						sDeletedFormName = txtGroupID.Text;
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
                        if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == false)
						{
							return;
						}
						if (lisGroupList.Items.Count > 0)
						{
                            CmnFunction.FindListItem(lisGroupList, CmnFunction.RTrim(txtGroupID.Text), false);
						}
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.btnUpdate_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
					if (Update_UDR_Group(modGlobalConstant.MP_STEP_DELETE) == true)
					{
						if (modInterface.gIMdiForm.RefreshUdrGroupList() == false)
						{
							return;
						}
						sDeletedFormName = txtGroupID.Text;
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
                        if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == true)
						{
							if (lisGroupList.Items.Count > 0)
							{
								lisGroupList.Items[0].Selected = true;
							}
						}
						else
						{
							return;
						}
						CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.btnDelete_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
				
		
		private void frmFMBCreateUDRGroup_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			
			try
			{
				if (!(this.ActiveControl == null))
				{
					if (this.ActiveControl is TextBox || this.ActiveControl is UltraTextEditor)
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
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.frmFMBCreateUDRGroup_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.txtHeight_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.txtWidth_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnCopy_Click(System.Object sender, System.EventArgs e)
		{
			try
			{
				if (CheckCondition("COPY") == false)
				{
					return;
				}
				if (Copy_UDR_Group() == false)
				{
					return;
				}
				if (modInterface.gIMdiForm.RefreshUdrGroupList() == false)
				{
					return;
				}
                if (modListRoutine.ViewUDR_GroupList(lisGroupList, '1') == false)
				{
					return;
				}
				if (lisGroupList.Items.Count > 0)
				{
                    CmnFunction.FindListItem(lisGroupList, CmnFunction.RTrim(txtGroupID.Text), false);
				}
				CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.btnCopy_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
		}

        private void cdvGroup_ButtonPress(object sender, System.EventArgs e)
        {

            try
            {
                cdvGroup.Init();
                CmnInitFunction.InitListView(cdvGroup.GetListView);
                cdvGroup.Columns.Add("UserGroup", 100, HorizontalAlignment.Left);
                cdvGroup.Columns.Add("Desc", 100, HorizontalAlignment.Left);
                //cdvGroup.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
                cdvGroup.SelectedSubItemIndex = 0;
                //if (SECLIST.ViewSecGroupList(cdvGroup.GetListView, '1',null,"") == false) return; // TODO: might not be correct. Was : Exit Sub
                cdvGroup.AddEmptyRow(1);
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmSPCAttachUserToChart.cdvGroup_ButtonPress()" + "\r\n" + ex.Message);
            }

        }

        private void cdvGroup_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {

	        try 
            {

		        if (cdvGroup.Text == "")
		        {
                    //if (SECLIST.ViewSECUserList(lisUserlist) == false) return; // TODO: might not be correct. Was : Exit Sub
         
			        if (lisUserlist.Items.Count > 0)
			        {
				        lisUserlist.Items[0].Selected = true;
			        }
		        }
		        else
		        {
                    //if (SECLIST.ViewSECUserList(lisUserlist, '2',-1,null,"", (cdvGroup.Text)) == false) return; // TODO: might not be correct. Was : Exit Sub
         
			        if (lisUserlist.Items.Count > 0)
			        {
				        lisUserlist.Items[0].Selected = true;
			        }
		        }
	        }

	        catch (Exception ex) 
            {
                CmnFunction.ShowMsgBox("frmSPCAttachUserToChart.cdvGroup_SelectedItemChanged()" + "\r\n" + ex.Message);
	        }

        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {

            try
            {
                string sUser;
                string sGroup;
                string[] sSelect = new string[lisUserlist.SelectedItems.Count];
                //ListViewItem itmX;
                int i;
                int j;
                int iIdx = 0;
                

                SelectClear(lisAttachUser);
                if (CheckCondition("USER_ATTACH") == false) return; // TODO: might not be correct. Was : Exit Sub

                for (i = 0; i <= lisUserlist.SelectedItems.Count - 1; i++)
                {
                    sUser = lisUserlist.SelectedItems[i].Text;
                    sGroup = lisGroupList.SelectedItems[0].Text;
                    if (CmnFunction.FindListItem(lisAttachUser, sUser, false) == false)
                    {
                        if (Attach_User(GlobalConstant.MP_STEP_CREATE, sUser, sGroup) == true)
                        {
                            sSelect[i] = sUser;
                        }
                        else
                        {
                            for (j = 0; j <= sSelect.Length - 1; j++)
                            {
                                CmnFunction.FindListItem(lisAttachUser, sSelect[j], false);
                            }
                            SelectClear(lisUserlist);
                            return; // TODO: might not be correct. Was : Exit Sub
                        }
                    }
                    else
                    {
                        sSelect[i] = sUser;
                        iIdx = lisUserlist.SelectedItems[i].Index;
                    }
                }
                if (modListRoutine.ViewFMBGroupList(lisAttachUser, '1', lisGroupList.SelectedItems[0].Text,"") == false) return; // TODO: might not be correct. Was : Exit Sub

                SelectClear(lisUserlist);
                if (sSelect.Length == 1)
                {
                    if (iIdx < lisUserlist.Items.Count - 1)
                    {
                        lisUserlist.Items[iIdx + 1].Selected = true;
                    }
                }
                for (i = 0; i <= sSelect.Length - 1; i++)
                {
                    CmnFunction.FindListItem(lisAttachUser, sSelect[i], false);
                }

                if ( modInterface.gIMdiForm.RefreshUdrGroupList() == false) return; // TODO: might not be correct. Was : Exit Sub

            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmSPCAttachUserToChart.btnAdd_Click()" + "\r\n" + ex.Message);
            }

        }

        private void btnDel_Click(object sender, System.EventArgs e)
        {

            try
            {
                string sUser;
                string sGroup;
                int iIdx = 0;
                int i;
                int iCount;

                if (CheckCondition("USER_DETACH") == false) return; // TODO: might not be correct. Was : Exit Sub

                iCount = lisAttachUser.SelectedItems.Count;
                SelectClear(lisUserlist);
                for (i = lisAttachUser.SelectedItems.Count - 1; i >= 0; i += -1)
                {
                    sUser = lisAttachUser.SelectedItems[i].Text;
                    sGroup = lisGroupList.SelectedItems[0].Text;

                    if (Attach_User(GlobalConstant.MP_STEP_DELETE, sUser, sGroup) == true)
                    {
                        iIdx = lisAttachUser.SelectedItems[i].Index;
                        lisAttachUser.Items.RemoveAt(iIdx);
                        CmnFunction.FindListItem(lisUserlist, sUser, false);
                    }
                }
                if (iCount == 1)
                {
                    if (iIdx < lisAttachUser.Items.Count)
                    {
                        lisAttachUser.Items[iIdx].Selected = true;
                    }
                }

                if (modInterface.gIMdiForm.RefreshUdrGroupList() == false) return; // TODO: might not be correct. Was : Exit Sub

            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmSPCAttachUserToChart.btnDel_Click()" + "\r\n" + ex.Message);
            }

        }

        private void pnlRight_Resize(object sender, System.EventArgs e)
        {

            try
            {
                CmnFunction.FieldAdjust(pnlUserMid, pnlUserMidLeft, pnlUserMidRight, pnlUserAttach, 40);
            }

            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox("frmSPCAttachUserToChart.pnlRight_Resize()" + "\r\n" + ex.Message);
            }

        }

		#endregion
		
		
	}
	
}
