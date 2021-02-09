
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

using com.miracom.transceiverx.session;
using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : frmFMBAddMultiResources.vb
//   Description : Add Multi Resources to Layout
//
//   FMB Version : 1.0.0
//
//   Function List
//       - ViewResourceList() : View Resource List
//       - ViewUDRResourceList() : View User Define Group Resource List
//       - View_Resource() : View Resource Information
//       - Save_Resource() : Save Resource Information
//       - Init() : Initialize form
//       - Update_Resource_List() : Update Resource Llst
//       - Update_UDRRes_List() : Update User Define Resource Liset
//
//   Detail Description
//       - 2005-11-15 : Created by H.K.Kim
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
	public class frmFMBAddMultiResources : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBAddMultiResources()
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			
		}
		
		public frmFMBAddMultiResources(string sFactory, string sGroupID, string sLayOut)
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			sSelectFactory = sFactory;
			sSelectGroup = sGroupID;
			sSelectLayOut = sLayOut;
			
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
		internal System.Windows.Forms.Panel pnlBottom;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Button btnOK;
		internal System.Windows.Forms.Panel pnlCenter;
		internal System.Windows.Forms.Panel pnlDesign;
		internal System.Windows.Forms.GroupBox grpInfo;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcBack;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcText;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtY;
		internal System.Windows.Forms.Label lblY;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtHeight;
		internal System.Windows.Forms.Label lblHeight;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtWidth;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtX;
		internal System.Windows.Forms.Label lblWidth;
		internal System.Windows.Forms.Label lblX;
		internal System.Windows.Forms.Label lblBackColor;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboTextStyle;
		internal System.Windows.Forms.Label lblStyle;
		internal System.Windows.Forms.Label lblColor;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboSize;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtText;
		internal System.Windows.Forms.Label txtSize;
		internal System.Windows.Forms.Label lblText;
		internal System.Windows.Forms.Panel pnlResource;
		internal Miracom.UI.Controls.MCListView.MCListView lisResourceList;
		internal System.Windows.Forms.ColumnHeader colRes;
		internal System.Windows.Forms.ColumnHeader colDesc;
		internal System.Windows.Forms.ColumnHeader colLocX;
		internal System.Windows.Forms.ColumnHeader colLocY;
		internal System.Windows.Forms.ColumnHeader colWidth;
		internal System.Windows.Forms.ColumnHeader colHeight;
		internal System.Windows.Forms.ColumnHeader colText;
		internal System.Windows.Forms.ColumnHeader colTextSize;
		internal System.Windows.Forms.ColumnHeader colTextColor;
		internal System.Windows.Forms.ColumnHeader colTextStyle;
		internal System.Windows.Forms.ColumnHeader colBackColor;
		internal System.Windows.Forms.Button btnSave;
		internal System.Windows.Forms.ColumnHeader colLastEvent;
		internal System.Windows.Forms.ColumnHeader colPriSts;
		internal System.Windows.Forms.ColumnHeader colProc;
		internal System.Windows.Forms.ColumnHeader colCtrl;
		internal System.Windows.Forms.ColumnHeader colUpDown;
		internal System.Windows.Forms.ColumnHeader colResType;
		internal System.Windows.Forms.Button btnSelect;
		internal System.Windows.Forms.ColumnHeader colArea;
		internal System.Windows.Forms.ColumnHeader colSubArea;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.pnlBottom = new System.Windows.Forms.Panel();
			base.Load += new System.EventHandler(frmFMBAddMultiResources_Load);
			this.btnSelect = new System.Windows.Forms.Button();
			this.btnSelect.Click += new System.EventHandler(btnSelect_Click);
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.btnOK = new System.Windows.Forms.Button();
			this.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.btnSave = new System.Windows.Forms.Button();
			this.btnSave.Click += new System.EventHandler(btnSave_Click);
			this.pnlCenter = new System.Windows.Forms.Panel();
			this.pnlResource = new System.Windows.Forms.Panel();
			this.lisResourceList = new Miracom.UI.Controls.MCListView.MCListView();
			this.lisResourceList.SelectedIndexChanged += new System.EventHandler(lisResourceList_SelectedIndexChanged);
			this.colRes = new System.Windows.Forms.ColumnHeader();
			this.colDesc = new System.Windows.Forms.ColumnHeader();
			this.colText = new System.Windows.Forms.ColumnHeader();
			this.colTextColor = new System.Windows.Forms.ColumnHeader();
			this.colBackColor = new System.Windows.Forms.ColumnHeader();
			this.colTextSize = new System.Windows.Forms.ColumnHeader();
			this.colTextStyle = new System.Windows.Forms.ColumnHeader();
			this.colWidth = new System.Windows.Forms.ColumnHeader();
			this.colHeight = new System.Windows.Forms.ColumnHeader();
			this.colLocX = new System.Windows.Forms.ColumnHeader();
			this.colLocY = new System.Windows.Forms.ColumnHeader();
			this.colLastEvent = new System.Windows.Forms.ColumnHeader();
			this.colPriSts = new System.Windows.Forms.ColumnHeader();
			this.colProc = new System.Windows.Forms.ColumnHeader();
			this.colCtrl = new System.Windows.Forms.ColumnHeader();
			this.colUpDown = new System.Windows.Forms.ColumnHeader();
			this.colResType = new System.Windows.Forms.ColumnHeader();
			this.pnlDesign = new System.Windows.Forms.Panel();
			this.grpInfo = new System.Windows.Forms.GroupBox();
			this.utcBack = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.utcText = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.txtY = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblY = new System.Windows.Forms.Label();
			this.txtHeight = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblHeight = new System.Windows.Forms.Label();
			this.txtWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtX = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblWidth = new System.Windows.Forms.Label();
			this.lblX = new System.Windows.Forms.Label();
			this.lblBackColor = new System.Windows.Forms.Label();
			this.cboTextStyle = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.lblStyle = new System.Windows.Forms.Label();
			this.lblColor = new System.Windows.Forms.Label();
			this.cboSize = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.txtText = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtSize = new System.Windows.Forms.Label();
			this.lblText = new System.Windows.Forms.Label();
			this.colArea = new System.Windows.Forms.ColumnHeader();
			this.colSubArea = new System.Windows.Forms.ColumnHeader();
			this.pnlBottom.SuspendLayout();
			this.pnlCenter.SuspendLayout();
			this.pnlResource.SuspendLayout();
			this.pnlDesign.SuspendLayout();
			this.grpInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.utcBack).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.utcText).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtY).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtHeight).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtWidth).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtX).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cboTextStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cboSize).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtText).BeginInit();
			this.SuspendLayout();
			//
			//pnlBottom
			//
			this.pnlBottom.Controls.Add(this.btnSelect);
			this.pnlBottom.Controls.Add(this.btnClose);
			this.pnlBottom.Controls.Add(this.btnOK);
			this.pnlBottom.Controls.Add(this.btnSave);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 342);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(677, 40);
			this.pnlBottom.TabIndex = 1;
			//
			//btnSelect
			//
			this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSelect.Location = new System.Drawing.Point(8, 9);
			this.btnSelect.Name = "btnSelect";
			this.btnSelect.Size = new System.Drawing.Size(74, 23);
			this.btnSelect.TabIndex = 3;
			this.btnSelect.Tag = "S";
			this.btnSelect.Text = "Select All";
			//
			//btnClose
			//
			this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(593, 9);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(74, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			//
			//btnOK
			//
			this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(513, 9);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(74, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			//
			//btnSave
			//
			this.btnSave.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(433, 9);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(74, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			//
			//pnlCenter
			//
			this.pnlCenter.Controls.Add(this.pnlResource);
			this.pnlCenter.Controls.Add(this.pnlDesign);
			this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlCenter.Location = new System.Drawing.Point(0, 0);
			this.pnlCenter.Name = "pnlCenter";
			this.pnlCenter.Size = new System.Drawing.Size(677, 342);
			this.pnlCenter.TabIndex = 0;
			//
			//pnlResource
			//
			this.pnlResource.Controls.Add(this.lisResourceList);
			this.pnlResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlResource.DockPadding.Left = 3;
			this.pnlResource.DockPadding.Right = 3;
			this.pnlResource.DockPadding.Top = 3;
			this.pnlResource.Location = new System.Drawing.Point(0, 0);
			this.pnlResource.Name = "pnlResource";
			this.pnlResource.Size = new System.Drawing.Size(677, 246);
			this.pnlResource.TabIndex = 0;
			//
			//lisResourceList
			//
			this.lisResourceList.CheckBoxes = true;
			this.lisResourceList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.colRes, this.colDesc, this.colArea, this.colSubArea, this.colText, this.colTextColor, this.colBackColor, this.colTextSize, this.colTextStyle, this.colWidth, this.colHeight, this.colLocX, this.colLocY, this.colLastEvent, this.colPriSts, this.colProc, this.colCtrl, this.colUpDown, this.colResType });
			this.lisResourceList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lisResourceList.EnableSort = true;
			this.lisResourceList.EnableSortIcon = true;
			this.lisResourceList.FullRowSelect = true;
			this.lisResourceList.Location = new System.Drawing.Point(3, 3);
			this.lisResourceList.Name = "lisResourceList";
			this.lisResourceList.Size = new System.Drawing.Size(671, 243);
			this.lisResourceList.TabIndex = 2;
			this.lisResourceList.View = System.Windows.Forms.View.Details;
			//
			//colRes
			//
			this.colRes.Text = "Resource ID";
			this.colRes.Width = 100;
			//
			//colDesc
			//
			this.colDesc.Text = "Desc ";
			this.colDesc.Width = 115;
			//
			//colText
			//
			this.colText.Text = "Text";
			this.colText.Width = 120;
			//
			//colTextColor
			//
			this.colTextColor.Text = "Text Color";
			this.colTextColor.Width = 120;
			//
			//colBackColor
			//
			this.colBackColor.Text = "Back Color";
			this.colBackColor.Width = 120;
			//
			//colTextSize
			//
			this.colTextSize.Text = "Text Size";
			this.colTextSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			//
			//colTextStyle
			//
			this.colTextStyle.Text = "Text Style";
			this.colTextStyle.Width = 100;
			//
			//colWidth
			//
			this.colWidth.Text = "Width";
			this.colWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			//
			//colHeight
			//
			this.colHeight.Text = "Height";
			this.colHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			//
			//colLocX
			//
			this.colLocX.Text = "Loc X";
			this.colLocX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			//
			//colLocY
			//
			this.colLocY.Text = "Loc Y";
			this.colLocY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			//
			//colLastEvent
			//
			this.colLastEvent.Width = 0;
			//
			//colPriSts
			//
			this.colPriSts.Width = 0;
			//
			//colProc
			//
			this.colProc.Width = 0;
			//
			//colCtrl
			//
			this.colCtrl.Width = 0;
			//
			//colUpDown
			//
			this.colUpDown.Width = 0;
			//
			//colResType
			//
			this.colResType.Width = 0;
			//
			//pnlDesign
			//
			this.pnlDesign.Controls.Add(this.grpInfo);
			this.pnlDesign.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlDesign.DockPadding.Left = 3;
			this.pnlDesign.DockPadding.Right = 3;
			this.pnlDesign.Location = new System.Drawing.Point(0, 246);
			this.pnlDesign.Name = "pnlDesign";
			this.pnlDesign.Size = new System.Drawing.Size(677, 96);
			this.pnlDesign.TabIndex = 0;
			//
			//grpInfo
			//
			this.grpInfo.Controls.Add(this.utcBack);
			this.grpInfo.Controls.Add(this.utcText);
			this.grpInfo.Controls.Add(this.txtY);
			this.grpInfo.Controls.Add(this.lblY);
			this.grpInfo.Controls.Add(this.txtHeight);
			this.grpInfo.Controls.Add(this.lblHeight);
			this.grpInfo.Controls.Add(this.txtWidth);
			this.grpInfo.Controls.Add(this.txtX);
			this.grpInfo.Controls.Add(this.lblWidth);
			this.grpInfo.Controls.Add(this.lblX);
			this.grpInfo.Controls.Add(this.lblBackColor);
			this.grpInfo.Controls.Add(this.cboTextStyle);
			this.grpInfo.Controls.Add(this.lblStyle);
			this.grpInfo.Controls.Add(this.lblColor);
			this.grpInfo.Controls.Add(this.cboSize);
			this.grpInfo.Controls.Add(this.txtText);
			this.grpInfo.Controls.Add(this.txtSize);
			this.grpInfo.Controls.Add(this.lblText);
			this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpInfo.Location = new System.Drawing.Point(3, 0);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.Size = new System.Drawing.Size(671, 96);
			this.grpInfo.TabIndex = 2;
			this.grpInfo.TabStop = false;
			//
			//utcBack
			//
            this.utcBack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcBack.Location = new System.Drawing.Point(544, 16);
			this.utcBack.Name = "utcBack";
			this.utcBack.Size = new System.Drawing.Size(120, 19);
			this.utcBack.TabIndex = 2;
			this.utcBack.Text = "Control";
			//
			//utcText
			//
			this.utcText.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcText.Location = new System.Drawing.Point(323, 16);
			this.utcText.Name = "utcText";
			this.utcText.Size = new System.Drawing.Size(120, 19);
			this.utcText.TabIndex = 1;
			this.utcText.Text = "Control";
			//
			//txtY
			//
			this.txtY.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtY.Location = new System.Drawing.Point(323, 66);
			this.txtY.MaxLength = 6;
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(120, 19);
			this.txtY.TabIndex = 7;
			//
			//lblY
			//
			this.lblY.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblY.Location = new System.Drawing.Point(231, 68);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(88, 14);
			this.lblY.TabIndex = 47;
			this.lblY.Text = "Location Y";
			//
			//txtHeight
			//
			this.txtHeight.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtHeight.Location = new System.Drawing.Point(544, 66);
			this.txtHeight.MaxLength = 6;
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(120, 19);
			this.txtHeight.TabIndex = 8;
			//
			//lblHeight
			//
			this.lblHeight.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblHeight.Location = new System.Drawing.Point(452, 68);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(88, 14);
			this.lblHeight.TabIndex = 45;
			this.lblHeight.Text = "Height";
			//
			//txtWidth
			//
			this.txtWidth.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtWidth.Location = new System.Drawing.Point(544, 40);
			this.txtWidth.MaxLength = 6;
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(120, 19);
			this.txtWidth.TabIndex = 5;
			//
			//txtX
			//
			this.txtX.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtX.Location = new System.Drawing.Point(104, 66);
			this.txtX.MaxLength = 6;
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(120, 19);
			this.txtX.TabIndex = 6;
			//
			//lblWidth
			//
			this.lblWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblWidth.Location = new System.Drawing.Point(452, 42);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(88, 14);
			this.lblWidth.TabIndex = 42;
			this.lblWidth.Text = "Width";
			//
			//lblX
			//
			this.lblX.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblX.Location = new System.Drawing.Point(12, 68);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(88, 14);
			this.lblX.TabIndex = 41;
			this.lblX.Text = "Location X ";
			//
			//lblBackColor
			//
			this.lblBackColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblBackColor.Location = new System.Drawing.Point(452, 18);
			this.lblBackColor.Name = "lblBackColor";
			this.lblBackColor.Size = new System.Drawing.Size(88, 14);
			this.lblBackColor.TabIndex = 37;
			this.lblBackColor.Text = "Back Color";
			//
			//cboTextStyle
			//
			this.cboTextStyle.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboTextStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.cboTextStyle.Location = new System.Drawing.Point(323, 40);
			this.cboTextStyle.Name = "cboTextStyle";
			this.cboTextStyle.Size = new System.Drawing.Size(120, 19);
			this.cboTextStyle.TabIndex = 4;
			//
			//lblStyle
			//
			this.lblStyle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblStyle.Location = new System.Drawing.Point(231, 44);
			this.lblStyle.Name = "lblStyle";
			this.lblStyle.Size = new System.Drawing.Size(88, 14);
			this.lblStyle.TabIndex = 34;
			this.lblStyle.Text = "Text Style";
			//
			//lblColor
			//
			this.lblColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblColor.Location = new System.Drawing.Point(231, 18);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(88, 14);
			this.lblColor.TabIndex = 33;
			this.lblColor.Text = "Text Color";
			//
			//cboSize
			//
			this.cboSize.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboSize.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.cboSize.Location = new System.Drawing.Point(104, 40);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(120, 19);
			this.cboSize.TabIndex = 3;
			//
			//txtText
			//
			this.txtText.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtText.Location = new System.Drawing.Point(104, 16);
			this.txtText.MaxLength = 40;
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(120, 19);
			this.txtText.TabIndex = 0;
			//
			//txtSize
			//
			this.txtSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.txtSize.Location = new System.Drawing.Point(12, 44);
			this.txtSize.Name = "txtSize";
			this.txtSize.Size = new System.Drawing.Size(88, 14);
			this.txtSize.TabIndex = 30;
			this.txtSize.Text = "Text Font Size";
			//
			//lblText
			//
			this.lblText.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblText.Location = new System.Drawing.Point(12, 20);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(88, 14);
			this.lblText.TabIndex = 29;
			this.lblText.Text = "Text";
			//
			//colArea
			//
			this.colArea.Text = "Area ID";
			this.colArea.Width = 100;
			//
			//colSubArea
			//
			this.colSubArea.Text = "Sub Area ID";
			this.colSubArea.Width = 100;
			//
			//frmFMBAddMultiResources
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(677, 382);
			this.Controls.Add(this.pnlCenter);
			this.Controls.Add(this.pnlBottom);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFMBAddMultiResources";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Multi Resources";
			this.pnlBottom.ResumeLayout(false);
			this.pnlCenter.ResumeLayout(false);
			this.pnlResource.ResumeLayout(false);
			this.pnlDesign.ResumeLayout(false);
			this.grpInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.utcBack).EndInit();
			((System.ComponentModel.ISupportInitialize) this.utcText).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtY).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtHeight).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtWidth).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtX).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cboTextStyle).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cboSize).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtText).EndInit();
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		#region " Constant Definition"
		
		private const int MAX_UPDATE_COUNT = 500;
		
		#endregion
		
		#region " Variable Definition"
		
		private string sSelectFactory = "";
		private string sSelectGroup = "";
		private string sSelectLayOut = "";
		
		#endregion
		
		#region " Function Implementations"
		
		// ViewResourceList()
		//       - View Resource List
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - control as Control : Control
		//       - sStep as String    : Proc Step
		//
		private bool ViewResourceList(Control control, char sStep)
		{
			
			try
			{
                FMB_View_Resource_List_In_Tag View_Resource_List_In = new FMB_View_Resource_List_In_Tag();
                FMB_View_Resource_List_Out_Tag View_Resource_List_Out = new FMB_View_Resource_List_Out_Tag(); ;
				
				int i;
				string sColorName;
				ListViewItem itmX;
				
				if (control is ListView)
				{
                    CmnInitFunction.InitListView((ListView)control);
				}
				
				View_Resource_List_In.h_proc_step = sStep;
				View_Resource_List_In.h_passport = GlobalVariable.gsPassport;
				View_Resource_List_In.h_language = GlobalVariable.gcLanguage;
				View_Resource_List_In.h_user_id = GlobalVariable.gsUserID;
				View_Resource_List_In.h_password = GlobalVariable.gsPassword;
				View_Resource_List_In.h_factory = sSelectFactory;
				
				View_Resource_List_In.next_res_id = "";
				
				do
				{

                    if (FMBSender.FMB_View_Resource_List(View_Resource_List_In, ref View_Resource_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                  					
					if (View_Resource_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
					{
						CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Resource_List_Out.h_msg_code, View_Resource_List_Out.h_msg, View_Resource_List_Out.h_db_err_msg, View_Resource_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
						return false;
					}
					
					for (i = 0; i <= View_Resource_List_Out.count - 1; i++)
					{
						
						if (control is ListView)
						{
							if (View_Resource_List_Out.res_list[i].res_up_down_flag == 'U')
							{
                                itmX = new ListViewItem(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_id), Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP));
							}
							else
							{
                                itmX = new ListViewItem(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_id), Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP_DOWN));
							}
							if (((ListView) control).Columns.Count > 1)
							{
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_desc));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].area_id));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].sub_area_id));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_id));
								if (((Color) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultTextColor)).IsKnownColor == true)
								{
									itmX.SubItems.Add(((Color) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultTextColor)).Name);
								}
								else
								{
									sColorName = modCommonFunction.ConvertColorToString((Color) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultTextColor));
									itmX.SubItems.Add(sColorName);
								}
								if (((Color) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultBackColor)).IsKnownColor == true)
								{
									itmX.SubItems.Add(((Color) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultBackColor)).Name);
								}
								else
								{
									sColorName = modCommonFunction.ConvertColorToString((Color) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultBackColor));
									itmX.SubItems.Add(sColorName);
								}
								itmX.SubItems.Add(CmnFunction.ToInt(modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultTextSize)).ToString());
								itmX.SubItems.Add(FontStyle.Regular.ToString());
								itmX.SubItems.Add(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultResourceSize)).Width.ToString());
								itmX.SubItems.Add(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sSelectFactory, clsOptionData.Options.DefaultResourceSize)).Height.ToString());
								itmX.SubItems.Add("10");
								itmX.SubItems.Add("10");
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].last_event));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_pri_sts));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_proc_mode));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_ctrl_mode));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_up_down_flag));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].res_type));
								itmX.SubItems.Add(CmnFunction.Trim(View_Resource_List_Out.res_list[i].img_idx));
								
								((ListView) control).Items.Add(itmX);
							}
						}
					}
					
					View_Resource_List_In.next_res_id = View_Resource_List_Out.next_res_id;
				} while (string.IsNullOrEmpty(View_Resource_List_Out.next_res_id) == false);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.ViewResourceList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// ViewUDRResourceList()
		//       - View User Define Resource List
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - control as Control : Control
		//       - sStep as String    : Proc Step
		//
		private bool ViewUDRResourceList(Control control, char sStep)
		{
			
			try
			{
                FMB_View_UDR_Resource_List_In_Tag View_UDR_Resource_List_In = new FMB_View_UDR_Resource_List_In_Tag();
                FMB_View_UDR_Resource_List_Out_Tag View_UDR_Resource_List_Out = new FMB_View_UDR_Resource_List_Out_Tag();
				
				int i;
				ListViewItem itmX;
				string sColorName;
				
				if (control is ListView)
				{
                    CmnInitFunction.InitListView((ListView)control);
				}
				
				View_UDR_Resource_List_In.h_proc_step = sStep;
				View_UDR_Resource_List_In.h_passport = GlobalVariable.gsPassport;
				View_UDR_Resource_List_In.h_language = GlobalVariable.gcLanguage;
				View_UDR_Resource_List_In.h_user_id = GlobalVariable.gsUserID;
				View_UDR_Resource_List_In.h_password = GlobalVariable.gsPassword;
				View_UDR_Resource_List_In.h_factory = GlobalVariable.gsFactory;
				View_UDR_Resource_List_In.next_res_id = "";
                View_UDR_Resource_List_In.group = CmnFunction.RTrim(sSelectGroup);
				
				do
				{

                    if (FMBSender.FMB_View_UDR_Resource_List(View_UDR_Resource_List_In, ref View_UDR_Resource_List_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                					
					if (View_UDR_Resource_List_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
					{
						CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_UDR_Resource_List_Out.h_msg_code, View_UDR_Resource_List_Out.h_msg, View_UDR_Resource_List_Out.h_db_err_msg, View_UDR_Resource_List_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
						return false;
					}
					
					for (i = 0; i <= View_UDR_Resource_List_Out.count - 1; i++)
					{
						
						if (control is ListView)
						{
							
							if (View_UDR_Resource_List_Out.udr_res_list[i].res_up_down_flag == 'U')
							{
                                itmX = new ListViewItem(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_id), Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP));
							}
							else
							{
                                itmX = new ListViewItem(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_id), Convert.ToInt32(SMALLICON_INDEX.IDX_EQUIP_DOWN));
							}
							if (((ListView) control).Columns.Count > 1)
							{
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_desc));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].area_id));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].sub_area_id));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_id));
								if (((Color) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultTextColor)).IsKnownColor == true)
								{
									itmX.SubItems.Add(((Color) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultTextColor)).Name);
								}
								else
								{
									sColorName = modCommonFunction.ConvertColorToString((Color) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultTextColor));
									itmX.SubItems.Add(sColorName);
								}
								if (((Color) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultBackColor)).IsKnownColor == true)
								{
									itmX.SubItems.Add(((Color) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultBackColor)).Name);
								}
								else
								{
									sColorName = modCommonFunction.ConvertColorToString((Color) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultBackColor));
									itmX.SubItems.Add(sColorName);
								}
								itmX.SubItems.Add(CmnFunction.ToInt(modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultTextSize)).ToString());
								itmX.SubItems.Add(FontStyle.Regular.ToString());
								itmX.SubItems.Add(((Size) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultResourceSize)).Width.ToString());
								itmX.SubItems.Add(((Size) modGlobalVariable.gGlobalOptions.GetOptions(GlobalVariable.gsFactory, clsOptionData.Options.DefaultResourceSize)).Height.ToString());
								itmX.SubItems.Add("10");
								itmX.SubItems.Add("10");
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].last_event));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_pri_sts));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_proc_mode));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_ctrl_mode));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_up_down_flag));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].res_type));
								itmX.SubItems.Add(CmnFunction.Trim(View_UDR_Resource_List_Out.udr_res_list[i].img_idx));
								
							}
							((ListView) control).Items.Add(itmX);
						}
					}
					
					View_UDR_Resource_List_In.next_res_id = View_UDR_Resource_List_Out.next_res_id;
				} while (string.IsNullOrEmpty(View_UDR_Resource_List_Out.next_res_id) == false);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.ViewUDRResourceList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// View_Resource()
		//       - View Resource Information
		// Return Value
		//       -
		// Arguments
		//       - ByVal idx As Integer
		//
		private void View_Resource(int idx)
		{
			
			try
			{
				
				txtText.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[4].Text);
				
				if (Color.FromName(lisResourceList.Items[idx].SubItems[5].Text).ToKnownColor() > 0)
				{
					utcText.Color = Color.FromName(lisResourceList.Items[idx].SubItems[5].Text);
				}
				else
				{
					utcText.Color = Color.FromArgb(modCommonFunction.ConvertStringToColor(lisResourceList.Items[idx].SubItems[5].Text));
				}
				if (Color.FromName(lisResourceList.Items[idx].SubItems[6].Text).ToKnownColor() > 0)
				{
					utcBack.Color = Color.FromName(lisResourceList.Items[idx].SubItems[6].Text);
				}
				else
				{
					utcBack.Color = Color.FromArgb(modCommonFunction.ConvertStringToColor(lisResourceList.Items[idx].SubItems[6].Text));
				}
				
				cboSize.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[7].Text);
				cboTextStyle.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[8].Text);
				txtWidth.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[9].Text);
				txtHeight.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[10].Text);
				txtX.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[11].Text);
				txtY.Text = CmnFunction.Trim(lisResourceList.Items[idx].SubItems[12].Text);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.View_Resource()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		// Save_Resource()
		//       - Save Resource Information
		// Return Value
		//       -
		// Arguments
		//       - ByVal idx As Integer
		//
		private void Save_Resource()
		{
			
			try
			{
				int i;
				for (i = 0; i <= lisResourceList.SelectedItems.Count - 1; i++)
				{
					lisResourceList.SelectedItems[i].Checked = true;
					
					if (lisResourceList.SelectedItems.Count == 1)
					{
						lisResourceList.SelectedItems[i].SubItems[4].Text = txtText.Text;
					}
					
					if (utcText.Color.IsKnownColor == true)
					{
						lisResourceList.SelectedItems[i].SubItems[5].Text = utcText.Color.Name;
					}
					else
					{
						lisResourceList.SelectedItems[i].SubItems[5].Text = modCommonFunction.ConvertColorToString(utcText.Color);
					}
					
					if (utcBack.Color.IsKnownColor == true)
					{
						lisResourceList.SelectedItems[i].SubItems[6].Text = utcBack.Color.Name;
					}
					else
					{
						lisResourceList.SelectedItems[i].SubItems[6].Text = modCommonFunction.ConvertColorToString(utcBack.Color);
					}
					lisResourceList.SelectedItems[i].SubItems[7].Text = cboSize.Text;
					lisResourceList.SelectedItems[i].SubItems[8].Text = cboTextStyle.Text;
					if (CmnFunction.RTrim(txtWidth.Text) != "")
					{
						lisResourceList.SelectedItems[i].SubItems[9].Text = txtWidth.Text;
					}
					if (CmnFunction.RTrim(txtHeight.Text) != "")
					{
						lisResourceList.SelectedItems[i].SubItems[10].Text = txtHeight.Text;
					}
					if (CmnFunction.RTrim(txtX.Text) != "")
					{
						lisResourceList.SelectedItems[i].SubItems[11].Text = txtX.Text;
					}
                    if (CmnFunction.RTrim(txtY.Text) != "")
					{
						lisResourceList.SelectedItems[i].SubItems[12].Text = txtY.Text;
					}
					int iWidth = new int();
					int iHeight = new int();
					if (this.Tag.ToString() == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
                        if (modCommonFunction.ViewLayOut(sSelectFactory, sSelectLayOut, ref iWidth, ref iHeight) == true)
						{
							if (CmnFunction.ToInt(txtX.Text) + CmnFunction.ToInt(txtWidth.Text) > iWidth)
							{
								if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(20), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
								{
									lisResourceList.SelectedItems[i].SubItems[11].Text = System.Convert.ToString(iWidth - System.Convert.ToDouble(txtWidth.Text));
                                    txtX.Text = System.Convert.ToString(iWidth - System.Convert.ToDouble(txtWidth.Text));
								}
								else
								{
                                    lisResourceList.SelectedItems[i].SubItems[11].Text = txtX.Text;
								}
							}
							if (CmnFunction.ToInt(txtY.Text) + CmnFunction.ToInt(txtHeight.Text) > iHeight)
							{
								if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(21), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
								{
                                    lisResourceList.SelectedItems[i].SubItems[12].Text = System.Convert.ToString(iHeight - System.Convert.ToDouble(txtHeight.Text));
                                    txtY.Text = System.Convert.ToString(iHeight - System.Convert.ToDouble(txtHeight.Text));
								}
								else
								{
                                    lisResourceList.SelectedItems[i].SubItems[12].Text = txtY.Text;
								}
							}
						}
					}
					else if (this.Tag.ToString() == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
                        if (modCommonFunction.ViewUDRGroup(sSelectGroup, ref iWidth, ref iHeight) == true)
						{
							if (CmnFunction.ToInt(txtX.Text) + CmnFunction.ToInt(txtWidth.Text) > iWidth)
							{
								if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(20), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
								{
                                    lisResourceList.SelectedItems[i].SubItems[11].Text = System.Convert.ToString(iWidth - System.Convert.ToDouble(txtWidth.Text));
                                    txtX.Text = System.Convert.ToString(iWidth - System.Convert.ToDouble(txtWidth.Text));
								}
								else
								{
                                    lisResourceList.SelectedItems[i].SubItems[11].Text = txtX.Text;
								}
							}
							if (CmnFunction.ToInt(txtY.Text) + CmnFunction.ToInt(txtHeight.Text) > iHeight)
							{
								if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(21), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
								{
                                    lisResourceList.SelectedItems[i].SubItems[12].Text = System.Convert.ToString(iHeight - System.Convert.ToDouble(txtHeight.Text));
                                    txtY.Text = System.Convert.ToString(iHeight - System.Convert.ToDouble(txtHeight.Text));
								}
								else
								{
                                    lisResourceList.SelectedItems[i].SubItems[12].Text = txtY.Text;
								}
							}
						}
					}
				}
				
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.Save_Resource()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		// Init()
		//       - Initialize form
		// Return Value
		//       -
		// Arguments
		//       -
		//
		private void Init()
		{
			
			try
			{
				modCommonFunction.GetTextboxStyle(this.Controls);
				modCommonFunction.SetEnumList(cboTextStyle, typeof(FontStyle));
				modCommonFunction.SetFontSize(cboSize);
				cboTextStyle.SelectedIndex = 0;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.Init()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		// Update_Resource_List()
		//       - Create/Update/Delete Resource Location
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool Update_Resource_List()
		{
			
			try
			{
                FMB_Update_ResLoc_List_In_Tag Update_ResLoc_In = new FMB_Update_ResLoc_List_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				int i;
				int j;
				int k;
				int iCount;
				
				Update_ResLoc_In._C.h_factory = sSelectFactory;
				Update_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
				Update_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_ResLoc_In._C.h_proc_step = modGlobalConstant.MP_STEP_CREATE;
				Update_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
				Update_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
				Update_ResLoc_In._C.count = 0;
				Update_ResLoc_In._C._size_res_list = 0;
                Update_ResLoc_In._C.res_list = new FMB_UPDATE_RESLOC_LIST_IN_TAG_res_list[1];
				j = 0;
				do
				{
					k = lisResourceList.CheckedItems.Count -(MAX_UPDATE_COUNT +(MAX_UPDATE_COUNT * j));
					if (k > 0)
					{
						k = 0;
					}
					iCount = 0;
					for (i = MAX_UPDATE_COUNT * j; i <= (MAX_UPDATE_COUNT +(MAX_UPDATE_COUNT * j) + k) - 1; i++)
					{
                        Array.Resize<FMB_UPDATE_RESLOC_LIST_IN_TAG_res_list>(ref Update_ResLoc_In._C.res_list, iCount + 1);
                        Update_ResLoc_In._C.res_list[iCount] = new FMB_UPDATE_RESLOC_LIST_IN_TAG_res_list();

                        Update_ResLoc_In._C.res_list[iCount].res_id = lisResourceList.CheckedItems[i].Text;
						Update_ResLoc_In._C.res_list[iCount].text = lisResourceList.CheckedItems[i].SubItems[4].Text;
						Update_ResLoc_In._C.res_list[iCount].res_type = 'R';
						if (Color.FromName(lisResourceList.CheckedItems[i].SubItems[5].Text).ToKnownColor() > 0)
						{
							Update_ResLoc_In._C.res_list[iCount].text_color = CmnFunction.ToInt(Color.FromName(lisResourceList.CheckedItems[i].SubItems[5].Text).ToKnownColor());
						}
						else
						{
							Update_ResLoc_In._C.res_list[iCount].text_color = modCommonFunction.ConvertStringToColor(lisResourceList.CheckedItems[i].SubItems[5].Text);
						}
						
						if (Color.FromName(lisResourceList.CheckedItems[i].SubItems[6].Text).ToKnownColor() > 0)
						{
                            Update_ResLoc_In._C.res_list[iCount].back_color = CmnFunction.ToInt(Color.FromName(lisResourceList.CheckedItems[i].SubItems[6].Text).ToKnownColor());
						}
						else
						{
							Update_ResLoc_In._C.res_list[iCount].back_color = modCommonFunction.ConvertStringToColor(lisResourceList.CheckedItems[i].SubItems[6].Text);
						}
						Update_ResLoc_In._C.res_list[iCount].text_size = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[7].Text);
                        Update_ResLoc_In._C.res_list[iCount].text_style = CmnFunction.ToChar(CmnFunction.ToInt(@Enum.Parse(typeof(FontStyle), CmnFunction.Trim(lisResourceList.CheckedItems[i].SubItems[8].Text))));
						
						Update_ResLoc_In._C.res_list[iCount].loc_width = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[9].Text);
						Update_ResLoc_In._C.res_list[iCount].loc_height = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[10].Text);
						Update_ResLoc_In._C.res_list[iCount].loc_x = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[11].Text);
						Update_ResLoc_In._C.res_list[iCount].loc_y = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[12].Text);
                        Update_ResLoc_In._C.res_list[iCount].tag_type = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Resource);
						Update_ResLoc_In._C.res_list[iCount].signal_flag = 'Y';
						iCount++;
					}
					
					Update_ResLoc_In._C.count = iCount;
					Update_ResLoc_In._C._size_res_list = iCount;
					Update_ResLoc_In._C.layout_id = sSelectLayOut;

                    if (FMBSender.FMB_Update_ResLoc_List(Update_ResLoc_In, ref Cmn_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
    					
					if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
					{
						CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
						return false;
					}
					
					j++;
				} while (!(k < 0));
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.Update_Resource_Location()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_UDRRes_List()
		//       - Create/Update/Delete Resource Location
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool Update_UDRRes_List()
		{
			
			try
			{
                FMB_Update_UDRLoc_List_In_Tag Update_UDRLoc_In = new FMB_Update_UDRLoc_List_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				int i;
				int j;
				int k;
				int iCount;
				
				Update_UDRLoc_In._C.h_factory = GlobalVariable.gsFactory;
				Update_UDRLoc_In._C.h_language = GlobalVariable.gcLanguage;
				Update_UDRLoc_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_UDRLoc_In._C.h_proc_step = modGlobalConstant.MP_STEP_CREATE;
				Update_UDRLoc_In._C.h_passport = GlobalVariable.gsPassport;
				Update_UDRLoc_In._C.h_password = GlobalVariable.gsPassword;
				Update_UDRLoc_In._C.group = sSelectGroup;
				Update_UDRLoc_In._C.count = 0;
				Update_UDRLoc_In._C._size_res_list = 0;
                Update_UDRLoc_In._C.res_list = new FMB_UPDATE_UDRLOC_LIST_IN_TAG_res_list[1];

                
				j = 0;
				do
				{
					k = lisResourceList.CheckedItems.Count -(MAX_UPDATE_COUNT +(MAX_UPDATE_COUNT * j));
					if (k > 0)
					{
						k = 0;
					}
					iCount = 0;
					for (i = MAX_UPDATE_COUNT * j; i <= (MAX_UPDATE_COUNT +(MAX_UPDATE_COUNT * j) + k) - 1; i++)
					{
                        Array.Resize<FMB_UPDATE_UDRLOC_LIST_IN_TAG_res_list>(ref Update_UDRLoc_In._C.res_list, iCount + 1);
                        Update_UDRLoc_In._C.res_list[iCount] = new FMB_UPDATE_UDRLOC_LIST_IN_TAG_res_list();

                        Update_UDRLoc_In._C.res_list[iCount].res_id = lisResourceList.CheckedItems[i].Text;
						Update_UDRLoc_In._C.res_list[iCount].text = lisResourceList.CheckedItems[i].SubItems[4].Text;
						Update_UDRLoc_In._C.res_list[iCount].res_type = 'R';
						if (Color.FromName(lisResourceList.CheckedItems[i].SubItems[5].Text).ToKnownColor() > 0)
						{
							Update_UDRLoc_In._C.res_list[iCount].text_color = CmnFunction.ToInt(Color.FromName(lisResourceList.CheckedItems[i].SubItems[5].Text).ToKnownColor());
						}
						else
						{
							Update_UDRLoc_In._C.res_list[iCount].text_color = modCommonFunction.ConvertStringToColor(lisResourceList.CheckedItems[i].SubItems[5].Text);
						}
						
						if (Color.FromName(lisResourceList.CheckedItems[i].SubItems[6].Text).ToKnownColor() > 0)
						{
							Update_UDRLoc_In._C.res_list[iCount].back_color = CmnFunction.ToInt(Color.FromName(lisResourceList.CheckedItems[i].SubItems[6].Text).ToKnownColor());
						}
						else
						{
							Update_UDRLoc_In._C.res_list[iCount].back_color = modCommonFunction.ConvertStringToColor(lisResourceList.CheckedItems[i].SubItems[6].Text);
						}
						Update_UDRLoc_In._C.res_list[iCount].text_size = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[7].Text);
                        Update_UDRLoc_In._C.res_list[iCount].text_style = CmnFunction.ToChar(CmnFunction.ToInt(@Enum.Parse(typeof(FontStyle), CmnFunction.Trim(lisResourceList.CheckedItems[i].SubItems[8].Text))));
						
						Update_UDRLoc_In._C.res_list[iCount].loc_width = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[9].Text);
						Update_UDRLoc_In._C.res_list[iCount].loc_height = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[10].Text);
						Update_UDRLoc_In._C.res_list[iCount].loc_x = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[11].Text);
						Update_UDRLoc_In._C.res_list[iCount].loc_y = CmnFunction.ToInt(lisResourceList.CheckedItems[i].SubItems[12].Text);
						Update_UDRLoc_In._C.res_list[iCount].tag_type = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Resource);
						Update_UDRLoc_In._C.res_list[iCount].signal_flag = 'Y';
						iCount++;
					}
					
					Update_UDRLoc_In._C.count = iCount;
					Update_UDRLoc_In._C._size_res_list = iCount;


                    if (FMBSender.FMB_Update_UDRLoc_List(Update_UDRLoc_In, ref Cmn_Out) == false)
                    {
                        CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                        return false;
                    }
                  					
					if (Cmn_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
					{
						CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(Cmn_Out.h_msg_code, Cmn_Out.h_msg, Cmn_Out.h_db_err_msg, Cmn_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
						return false;
					}
					j++;
				} while (!(k < 0));
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.Update_UDRRes_List()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		#endregion
		
		#region " Event Implementations"
		
		private void frmFMBAddMultiResources_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				Init();
				if (this.Tag.ToString() == modGlobalConstant.FMB_CATEGORY_LAYOUT)
				{
					ViewResourceList(lisResourceList, '4');
				}
                else if (this.Tag.ToString() == modGlobalConstant.FMB_CATEGORY_GROUP)
				{
					ViewUDRResourceList(lisResourceList, '4');
				}
				
				modCommonFunction.GetTextboxStyle(this.Controls);
				modLanguageFunction.ToClientLanguage(this);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.frmFMBAddMultiResources_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void lisResourceList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				if (lisResourceList.SelectedItems.Count == 0)
				{
					return;
				}
				
				View_Resource(lisResourceList.SelectedItems[0].Index);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.lisResourceList_SelectedIndexChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnSave_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				//int i;
				if (lisResourceList.SelectedItems.Count == 0)
				{
					return;
				}
				Save_Resource();
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.btnSave_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				if (lisResourceList.CheckedItems.Count == 0 || lisResourceList.Items.Count == 0)
				{
					return;
				}
                if (this.Tag.ToString() == modGlobalConstant.FMB_CATEGORY_LAYOUT)
				{
					Update_Resource_List();
				}
                else if (this.Tag.ToString() == modGlobalConstant.FMB_CATEGORY_GROUP)
				{
					Update_UDRRes_List();
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.btnOK_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnSelect_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				int i;
				if (lisResourceList.Items.Count == 0)
				{
					return;
				}
                if (btnSelect.Tag.ToString() == "S")
				{
					for (i = 0; i <= lisResourceList.Items.Count - 1; i++)
					{
						lisResourceList.Items[i].Checked = true;
					}
					btnSelect.Text = modLanguageFunction.FindLanguage("Deselect All", 1);
					btnSelect.Tag = "D";
				}
                else if (btnSelect.Tag.ToString() == "D")
				{
					for (i = 0; i <= lisResourceList.Items.Count - 1; i++)
					{
						lisResourceList.Items[i].Checked = false;
					}
					btnSelect.Text = modLanguageFunction.FindLanguage("Select All", 1);
					btnSelect.Tag = "S";
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.btnSelect_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				//Me.Dispose(False)
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBAddMultiResources.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		#endregion
		
	}
	
}
