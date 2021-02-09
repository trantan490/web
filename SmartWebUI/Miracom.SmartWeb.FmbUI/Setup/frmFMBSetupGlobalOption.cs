
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using com.miracom.transceiverx;
using com.miracom.transceiverx.session;
using com.miracom.transceiverx.message;

using Miracom.UI;
using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;
//using Miracom.MESCore;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : frmFMBSetupGlobalOption.vb
//   Description : Update Global Option
//
//   FMB Version : 1.0.0
//
//   Function List
//       - CheckCondition() : Check the conditions before transaction
//       - Update_Environment() : Update Global Option
//       - View_Environment() : View Global Option Information
//       - Update_Event_Color() : Update Resource Color
//       - ViewGlobalOption() : Set Global Options
//
//   Detail Description
//       -
//
//   History
//       - 2005-03-21 : Created by H.K.Kim
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------
namespace Miracom.SmartWeb.UI
{
	public class frmFMBSetupGlobalOption : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBSetupGlobalOption()
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			
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
		internal System.Windows.Forms.Panel pnlTop;
		internal System.Windows.Forms.GroupBox grpFactory;
		internal Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory;
		internal System.Windows.Forms.Label lblFactory;
		internal System.Windows.Forms.Panel pnlBottom;
		internal System.Windows.Forms.Button btnUpdate;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Panel pnlMid;
		internal System.Windows.Forms.GroupBox grpCreateInfo;
		internal System.Windows.Forms.Label lblUpdateTime;
		internal System.Windows.Forms.Label lblUpdateUser;
		internal System.Windows.Forms.Label lblCreateTime;
		internal System.Windows.Forms.Label lblCreateUser;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtUpdateTime;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtCreateTime;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtUpdateUser;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtCreateUser;
		internal FarPoint.Win.Spread.FpSpread spdSize;
		internal FarPoint.Win.Spread.SheetView spdSize_Sheet1;
		internal System.Windows.Forms.GroupBox grpSize;
		internal System.Windows.Forms.GroupBox grpOption;
		internal Infragistics.Win.UltraWinEditors.UltraCheckEditor chkEventColor;
		internal System.Windows.Forms.RadioButton rbnControl;
		internal System.Windows.Forms.RadioButton rbnProc;
		internal System.Windows.Forms.GroupBox grpSignal;
		internal System.Windows.Forms.Button btnSetting;
		internal System.Windows.Forms.Button btnView;
		internal Infragistics.Win.UltraWinEditors.UltraFontNameEditor ftnFontName;
		internal System.Windows.Forms.Label lblTextFontName;
		internal System.Windows.Forms.Panel pnlResColorSetting;
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
		internal System.Windows.Forms.ImageList imlOptions;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcBack;
		internal Miracom.UI.Controls.MCListView.MCListView lisEvent;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcTextColor;
		internal System.Windows.Forms.Label lblTextColor;
		internal System.Windows.Forms.Label lblBackColor;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcBackColor;
		internal System.Windows.Forms.Label lblSize;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboSize;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			base.Load += new System.EventHandler(frmFMBSetupGlobalOption_Load);
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmFMBSetupGlobalOption));
			this.pnlTop = new System.Windows.Forms.Panel();
			this.grpFactory = new System.Windows.Forms.GroupBox();
			this.cdvFactory = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.cdvFactory.TextBoxTextChanged += new System.EventHandler(cdvFactory_TextBoxTextChanged);
			this.lblFactory = new System.Windows.Forms.Label();
			this.pnlBottom = new System.Windows.Forms.Panel();
			this.btnView = new System.Windows.Forms.Button();
			this.btnView.Click += new System.EventHandler(btnView_Click);
			this.btnUpdate = new System.Windows.Forms.Button();
			this.btnUpdate.Click += new System.EventHandler(btnUpdate_Click);
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.pnlMid = new System.Windows.Forms.Panel();
			this.grpOption = new System.Windows.Forms.GroupBox();
			this.pnlResColorSetting = new System.Windows.Forms.Panel();
			this.utcBack = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.utcBack.ColorChanged += new System.EventHandler(utcBack_ColorChanged);
			this.lisEvent = new Miracom.UI.Controls.MCListView.MCListView();
			this.lisEvent.SelectedIndexChanged += new System.EventHandler(lisEvent_SelectedIndexChanged);
			this.lisEvent.VisibleChanged += new System.EventHandler(lisEvent_VisibleChanged);
			this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.imlOptions = new System.Windows.Forms.ImageList(this.components);
			this.cboSize = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.lblSize = new System.Windows.Forms.Label();
			this.lblBackColor = new System.Windows.Forms.Label();
			this.utcBackColor = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.lblTextColor = new System.Windows.Forms.Label();
			this.utcTextColor = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.lblTextFontName = new System.Windows.Forms.Label();
			this.ftnFontName = new Infragistics.Win.UltraWinEditors.UltraFontNameEditor();
			this.btnSetting = new System.Windows.Forms.Button();
			this.btnSetting.Click += new System.EventHandler(btnSetting_Click);
			this.grpSignal = new System.Windows.Forms.GroupBox();
			this.rbnControl = new System.Windows.Forms.RadioButton();
			this.rbnProc = new System.Windows.Forms.RadioButton();
			this.chkEventColor = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
			this.chkEventColor.CheckedChanged += new System.EventHandler(chkEventColor_CheckedChanged);
			this.grpSize = new System.Windows.Forms.GroupBox();
			this.spdSize = new FarPoint.Win.Spread.FpSpread();
			this.spdSize_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.grpCreateInfo = new System.Windows.Forms.GroupBox();
			this.lblUpdateTime = new System.Windows.Forms.Label();
			this.lblUpdateUser = new System.Windows.Forms.Label();
			this.lblCreateTime = new System.Windows.Forms.Label();
			this.lblCreateUser = new System.Windows.Forms.Label();
			this.txtUpdateTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtCreateTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtUpdateUser = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtCreateUser = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.pnlTop.SuspendLayout();
			this.grpFactory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory).BeginInit();
			this.pnlBottom.SuspendLayout();
			this.pnlMid.SuspendLayout();
			this.grpOption.SuspendLayout();
			this.pnlResColorSetting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.utcBack).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cboSize).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.utcBackColor).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.utcTextColor).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.ftnFontName).BeginInit();
			this.grpSignal.SuspendLayout();
			this.grpSize.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.spdSize).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.spdSize_Sheet1).BeginInit();
			this.grpCreateInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.txtUpdateTime).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtCreateTime).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtUpdateUser).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtCreateUser).BeginInit();
			this.SuspendLayout();
			//
			//pnlTop
			//
			this.pnlTop.Controls.Add(this.grpFactory);
			this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlTop.DockPadding.Left = 3;
			this.pnlTop.DockPadding.Right = 3;
			this.pnlTop.Location = new System.Drawing.Point(0, 0);
			this.pnlTop.Name = "pnlTop";
			this.pnlTop.Size = new System.Drawing.Size(742, 52);
			this.pnlTop.TabIndex = 0;
			//
			//grpFactory
			//
			this.grpFactory.Controls.Add(this.cdvFactory);
			this.grpFactory.Controls.Add(this.lblFactory);
			this.grpFactory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpFactory.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpFactory.Location = new System.Drawing.Point(3, 0);
			this.grpFactory.Name = "grpFactory";
			this.grpFactory.Size = new System.Drawing.Size(736, 52);
			this.grpFactory.TabIndex = 1;
			this.grpFactory.TabStop = false;
			//
			//cdvFactory
			//
			this.cdvFactory.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory.BtnToolTipText = "";
			this.cdvFactory.DescText = "";
			this.cdvFactory.DisplaySubItemIndex = - 1;
			this.cdvFactory.DisplayText = "";
			this.cdvFactory.Focusing = null;
			this.cdvFactory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory.Index = 0;
			this.cdvFactory.IsViewBtnImage = false;
			this.cdvFactory.Location = new System.Drawing.Point(116, 17);
			this.cdvFactory.MaxLength = 10;
			this.cdvFactory.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory.Name = "cdvFactory";
			this.cdvFactory.ReadOnly = false;
			this.cdvFactory.SearchSubItemIndex = 0;
			this.cdvFactory.SelectedDescIndex = - 1;
			this.cdvFactory.SelectedSubItemIndex = - 1;
			this.cdvFactory.SelectionStart = 0;
			this.cdvFactory.Size = new System.Drawing.Size(200, 20);
			this.cdvFactory.SmallImageList = null;
			this.cdvFactory.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory.TabIndex = 0;
			this.cdvFactory.TextBoxToolTipText = "";
			this.cdvFactory.TextBoxWidth = 200;
			this.cdvFactory.VisibleButton = true;
			this.cdvFactory.VisibleColumnHeader = false;
			this.cdvFactory.VisibleDescription = false;
			//
			//lblFactory
			//
			this.lblFactory.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory.Location = new System.Drawing.Point(15, 20);
			this.lblFactory.Name = "lblFactory";
			this.lblFactory.Size = new System.Drawing.Size(100, 14);
			this.lblFactory.TabIndex = 50;
			this.lblFactory.Text = "Factory";
			//
			//pnlBottom
			//
			this.pnlBottom.Controls.Add(this.btnView);
			this.pnlBottom.Controls.Add(this.btnUpdate);
			this.pnlBottom.Controls.Add(this.btnClose);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 511);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(742, 40);
			this.pnlBottom.TabIndex = 2;
			//
			//btnView
			//
			this.btnView.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnView.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnView.Location = new System.Drawing.Point(500, 9);
			this.btnView.Name = "btnView";
			this.btnView.Size = new System.Drawing.Size(74, 23);
			this.btnView.TabIndex = 0;
			this.btnView.Text = "View";
			//
			//btnUpdate
			//
			this.btnUpdate.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnUpdate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnUpdate.Location = new System.Drawing.Point(579, 9);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.Size = new System.Drawing.Size(74, 23);
			this.btnUpdate.TabIndex = 1;
			this.btnUpdate.Text = "Update";
			//
			//btnClose
			//
			this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnClose.Location = new System.Drawing.Point(659, 9);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(74, 23);
			this.btnClose.TabIndex = 2;
			this.btnClose.Text = "Close";
			//
			//pnlMid
			//
			this.pnlMid.Controls.Add(this.grpOption);
			this.pnlMid.Controls.Add(this.grpSize);
			this.pnlMid.Controls.Add(this.grpCreateInfo);
			this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMid.DockPadding.Left = 3;
			this.pnlMid.DockPadding.Right = 3;
			this.pnlMid.Location = new System.Drawing.Point(0, 52);
			this.pnlMid.Name = "pnlMid";
			this.pnlMid.Size = new System.Drawing.Size(742, 459);
			this.pnlMid.TabIndex = 1;
			//
			//grpOption
			//
			this.grpOption.Controls.Add(this.pnlResColorSetting);
			this.grpOption.Controls.Add(this.cboSize);
			this.grpOption.Controls.Add(this.lblSize);
			this.grpOption.Controls.Add(this.lblBackColor);
			this.grpOption.Controls.Add(this.utcBackColor);
			this.grpOption.Controls.Add(this.lblTextColor);
			this.grpOption.Controls.Add(this.utcTextColor);
			this.grpOption.Controls.Add(this.lblTextFontName);
			this.grpOption.Controls.Add(this.ftnFontName);
			this.grpOption.Controls.Add(this.btnSetting);
			this.grpOption.Controls.Add(this.grpSignal);
			this.grpOption.Controls.Add(this.chkEventColor);
			this.grpOption.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpOption.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpOption.Location = new System.Drawing.Point(352, 0);
			this.grpOption.Name = "grpOption";
			this.grpOption.Size = new System.Drawing.Size(387, 383);
			this.grpOption.TabIndex = 1;
			this.grpOption.TabStop = false;
			//
			//pnlResColorSetting
			//
			this.pnlResColorSetting.BackColor = System.Drawing.SystemColors.Control;
			this.pnlResColorSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlResColorSetting.Controls.Add(this.utcBack);
			this.pnlResColorSetting.Controls.Add(this.lisEvent);
			this.pnlResColorSetting.Location = new System.Drawing.Point(8, 148);
			this.pnlResColorSetting.Name = "pnlResColorSetting";
			this.pnlResColorSetting.Size = new System.Drawing.Size(340, 212);
			this.pnlResColorSetting.TabIndex = 4;
			this.pnlResColorSetting.Visible = false;
			//
			//utcBack
			//
			this.utcBack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcBack.Location = new System.Drawing.Point(190, 19);
			this.utcBack.Name = "utcBack";
			this.utcBack.Size = new System.Drawing.Size(126, 19);
			this.utcBack.TabIndex = 0;
			this.utcBack.Text = "Control";
			//
			//lisEvent
			//
			this.lisEvent.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lisEvent.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.ColumnHeader1, this.ColumnHeader2 });
			this.lisEvent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lisEvent.EnableSort = true;
			this.lisEvent.EnableSortIcon = true;
			this.lisEvent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lisEvent.FullRowSelect = true;
			this.lisEvent.GridLines = true;
			this.lisEvent.HideSelection = false;
			this.lisEvent.Location = new System.Drawing.Point(0, 0);
			this.lisEvent.Name = "lisEvent";
			this.lisEvent.Size = new System.Drawing.Size(338, 210);
			this.lisEvent.SmallImageList = this.imlOptions;
			this.lisEvent.TabIndex = 1;
			this.lisEvent.View = System.Windows.Forms.View.Details;
			//
			//ColumnHeader1
			//
			this.ColumnHeader1.Text = "Event";
			this.ColumnHeader1.Width = 190;
			//
			//ColumnHeader2
			//
			this.ColumnHeader2.Text = "Color";
			this.ColumnHeader2.Width = 126;
			//
			//imlOptions
			//
			this.imlOptions.ImageSize = new System.Drawing.Size(16, 16);
			this.imlOptions.ImageStream = (System.Windows.Forms.ImageListStreamer) resources.GetObject("imlOptions.ImageStream");
			this.imlOptions.TransparentColor = System.Drawing.Color.Transparent;
			//
			//cboSize
			//
			this.cboSize.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.cboSize.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.cboSize.Location = new System.Drawing.Point(116, 152);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(126, 19);
			this.cboSize.TabIndex = 4;
			//
			//lblSize
			//
			this.lblSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblSize.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSize.Location = new System.Drawing.Point(8, 154);
			this.lblSize.Name = "lblSize";
			this.lblSize.Size = new System.Drawing.Size(100, 14);
			this.lblSize.TabIndex = 0;
			this.lblSize.Text = "Text Size";
			//
			//lblBackColor
			//
			this.lblBackColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblBackColor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblBackColor.Location = new System.Drawing.Point(8, 201);
			this.lblBackColor.Name = "lblBackColor";
			this.lblBackColor.Size = new System.Drawing.Size(100, 14);
			this.lblBackColor.TabIndex = 9;
			this.lblBackColor.Text = "Back Color";
			//
			//utcBackColor
			//
            this.utcBackColor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcBackColor.Location = new System.Drawing.Point(116, 199);
			this.utcBackColor.Name = "utcBackColor";
			this.utcBackColor.Size = new System.Drawing.Size(126, 19);
			this.utcBackColor.TabIndex = 6;
			this.utcBackColor.Text = "Control";
			//
			//lblTextColor
			//
			this.lblTextColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTextColor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTextColor.Location = new System.Drawing.Point(8, 177);
			this.lblTextColor.Name = "lblTextColor";
			this.lblTextColor.Size = new System.Drawing.Size(100, 14);
			this.lblTextColor.TabIndex = 8;
			this.lblTextColor.Text = "Text Color";
			//
			//utcTextColor
			//
            this.utcTextColor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcTextColor.Location = new System.Drawing.Point(116, 175);
			this.utcTextColor.Name = "utcTextColor";
			this.utcTextColor.Size = new System.Drawing.Size(126, 19);
			this.utcTextColor.TabIndex = 5;
			this.utcTextColor.Text = "Control";
			//
			//lblTextFontName
			//
			this.lblTextFontName.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblTextFontName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTextFontName.Location = new System.Drawing.Point(12, 97);
			this.lblTextFontName.Name = "lblTextFontName";
			this.lblTextFontName.Size = new System.Drawing.Size(100, 14);
			this.lblTextFontName.TabIndex = 11;
			this.lblTextFontName.Text = "Text Font Name";
			//
			//ftnFontName
			//
			this.ftnFontName.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.ftnFontName.Location = new System.Drawing.Point(116, 93);
			this.ftnFontName.Name = "ftnFontName";
			this.ftnFontName.Size = new System.Drawing.Size(212, 21);
			this.ftnFontName.TabIndex = 1;
			//
			//btnSetting
			//
			this.btnSetting.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSetting.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnSetting.Location = new System.Drawing.Point(178, 122);
			this.btnSetting.Name = "btnSetting";
			this.btnSetting.Size = new System.Drawing.Size(74, 23);
			this.btnSetting.TabIndex = 3;
			this.btnSetting.Text = "Setting";
			this.btnSetting.Visible = false;
			//
			//grpSignal
			//
			this.grpSignal.Controls.Add(this.rbnControl);
			this.grpSignal.Controls.Add(this.rbnProc);
			this.grpSignal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.grpSignal.Location = new System.Drawing.Point(8, 16);
			this.grpSignal.Name = "grpSignal";
			this.grpSignal.Size = new System.Drawing.Size(204, 68);
			this.grpSignal.TabIndex = 0;
			this.grpSignal.TabStop = false;
			this.grpSignal.Text = "Blue Signal";
			//
			//rbnControl
			//
			this.rbnControl.Location = new System.Drawing.Point(8, 44);
			this.rbnControl.Name = "rbnControl";
			this.rbnControl.Size = new System.Drawing.Size(168, 15);
			this.rbnControl.TabIndex = 1;
			this.rbnControl.Text = "Control Mode";
			//
			//rbnProc
			//
			this.rbnProc.Location = new System.Drawing.Point(8, 20);
			this.rbnProc.Name = "rbnProc";
			this.rbnProc.Size = new System.Drawing.Size(168, 15);
			this.rbnProc.TabIndex = 0;
			this.rbnProc.Text = "Proc Mode";
			//
			//chkEventColor
			//
            this.chkEventColor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.chkEventColor.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkEventColor.Location = new System.Drawing.Point(8, 127);
			this.chkEventColor.Name = "chkEventColor";
			this.chkEventColor.Size = new System.Drawing.Size(156, 14);
			this.chkEventColor.TabIndex = 2;
			this.chkEventColor.Text = "Changing Resource Color";
			//
			//grpSize
			//
			this.grpSize.Controls.Add(this.spdSize);
			this.grpSize.Dock = System.Windows.Forms.DockStyle.Left;
			this.grpSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpSize.Location = new System.Drawing.Point(3, 0);
			this.grpSize.Name = "grpSize";
			this.grpSize.Size = new System.Drawing.Size(349, 383);
			this.grpSize.TabIndex = 0;
			this.grpSize.TabStop = false;
			this.grpSize.Text = "Default Size";
			//
			//spdSize
			//
			this.spdSize.BackColor = System.Drawing.Color.White;
			this.spdSize.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spdSize.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;
			this.spdSize.Location = new System.Drawing.Point(3, 16);
			this.spdSize.Name = "spdSize";
			this.spdSize.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {this.spdSize_Sheet1});
			this.spdSize.Size = new System.Drawing.Size(343, 364);
			this.spdSize.TabIndex = 0;
			//
			//spdSize_Sheet1
			//
			this.spdSize_Sheet1.Reset();
			this.spdSize_Sheet1.ColumnCount = 2;
			this.spdSize_Sheet1.RowCount = 12;
			this.spdSize_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "Width";
			this.spdSize_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "Height";
			this.spdSize_Sheet1.Columns.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.spdSize_Sheet1.Columns.Get(0).Label = "Width";
			this.spdSize_Sheet1.Columns.Get(0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.Columns.Get(0).Width = 100.0F;
			this.spdSize_Sheet1.Columns.Get(1).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.spdSize_Sheet1.Columns.Get(1).Label = "Height";
			this.spdSize_Sheet1.Columns.Get(1).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.Columns.Get(1).Width = 100.0F;
			this.spdSize_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
			this.spdSize_Sheet1.RowHeader.Cells.Get(0, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(0, 0).Text = "LayOut";
			this.spdSize_Sheet1.RowHeader.Cells.Get(0, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(1, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(1, 0).Text = "UDR Group";
			this.spdSize_Sheet1.RowHeader.Cells.Get(1, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(2, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(2, 0).Text = "Resource";
			this.spdSize_Sheet1.RowHeader.Cells.Get(2, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(3, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(3, 0).Text = "Rectangle";
			this.spdSize_Sheet1.RowHeader.Cells.Get(3, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(4, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(4, 0).Text = "Ellipse";
			this.spdSize_Sheet1.RowHeader.Cells.Get(4, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(5, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(5, 0).Text = "Triangle";
			this.spdSize_Sheet1.RowHeader.Cells.Get(5, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(6, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(6, 0).Text = "Vertical Line";
			this.spdSize_Sheet1.RowHeader.Cells.Get(6, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(7, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(7, 0).Text = "Horizontal Line";
			this.spdSize_Sheet1.RowHeader.Cells.Get(7, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(8, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(8, 0).Text = "Pie 1 Type";
			this.spdSize_Sheet1.RowHeader.Cells.Get(8, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(9, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(9, 0).Text = "Pie 2 Type";
			this.spdSize_Sheet1.RowHeader.Cells.Get(9, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(10, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(10, 0).Text = "Pie 3 Type";
			this.spdSize_Sheet1.RowHeader.Cells.Get(10, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(11, 0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Cells.Get(11, 0).Text = "Pie 4 Type";
			this.spdSize_Sheet1.RowHeader.Cells.Get(11, 0).VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
			this.spdSize_Sheet1.RowHeader.Columns.Default.Resizable = true;
			this.spdSize_Sheet1.RowHeader.Columns.Get(0).Width = 116.0F;
			this.spdSize_Sheet1.Rows.Get(0).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(0).Label = "LayOut";
			this.spdSize_Sheet1.Rows.Get(2).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(2).Label = "Resource";
			this.spdSize_Sheet1.Rows.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(3).Label = "Rectangle";
			this.spdSize_Sheet1.Rows.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(4).Label = "Ellipse";
			this.spdSize_Sheet1.Rows.Get(5).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(5).Label = "Triangle";
			this.spdSize_Sheet1.Rows.Get(6).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(6).Label = "Vertical Line";
			this.spdSize_Sheet1.Rows.Get(7).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(7).Label = "Horizontal Line";
			this.spdSize_Sheet1.Rows.Get(8).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(8).Label = "Pie 1 Type";
			this.spdSize_Sheet1.Rows.Get(9).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(9).Label = "Pie 2 Type";
			this.spdSize_Sheet1.Rows.Get(10).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(10).Label = "Pie 3 Type";
			this.spdSize_Sheet1.Rows.Get(11).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.General;
			this.spdSize_Sheet1.Rows.Get(11).Label = "Pie 4 Type";
			this.spdSize_Sheet1.SheetName = "Sheet1";
			//
			//grpCreateInfo
			//
			this.grpCreateInfo.Controls.Add(this.lblUpdateTime);
			this.grpCreateInfo.Controls.Add(this.lblUpdateUser);
			this.grpCreateInfo.Controls.Add(this.lblCreateTime);
			this.grpCreateInfo.Controls.Add(this.lblCreateUser);
			this.grpCreateInfo.Controls.Add(this.txtUpdateTime);
			this.grpCreateInfo.Controls.Add(this.txtCreateTime);
			this.grpCreateInfo.Controls.Add(this.txtUpdateUser);
			this.grpCreateInfo.Controls.Add(this.txtCreateUser);
			this.grpCreateInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.grpCreateInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpCreateInfo.Location = new System.Drawing.Point(3, 383);
			this.grpCreateInfo.Name = "grpCreateInfo";
			this.grpCreateInfo.Size = new System.Drawing.Size(736, 76);
			this.grpCreateInfo.TabIndex = 2;
			this.grpCreateInfo.TabStop = false;
			this.grpCreateInfo.Text = "Create / Update Info";
			//
			//lblUpdateTime
			//
			this.lblUpdateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUpdateTime.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblUpdateTime.Location = new System.Drawing.Point(392, 45);
			this.lblUpdateTime.Name = "lblUpdateTime";
			this.lblUpdateTime.Size = new System.Drawing.Size(100, 14);
			this.lblUpdateTime.TabIndex = 52;
			this.lblUpdateTime.Text = "Update Time";
			//
			//lblUpdateUser
			//
			this.lblUpdateUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUpdateUser.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblUpdateUser.Location = new System.Drawing.Point(12, 45);
			this.lblUpdateUser.Name = "lblUpdateUser";
			this.lblUpdateUser.Size = new System.Drawing.Size(100, 14);
			this.lblUpdateUser.TabIndex = 50;
			this.lblUpdateUser.Text = "Update User";
			//
			//lblCreateTime
			//
			this.lblCreateTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCreateTime.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCreateTime.Location = new System.Drawing.Point(392, 21);
			this.lblCreateTime.Name = "lblCreateTime";
			this.lblCreateTime.Size = new System.Drawing.Size(100, 14);
			this.lblCreateTime.TabIndex = 48;
			this.lblCreateTime.Text = "Create Time";
			//
			//lblCreateUser
			//
			this.lblCreateUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCreateUser.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCreateUser.Location = new System.Drawing.Point(12, 21);
			this.lblCreateUser.Name = "lblCreateUser";
			this.lblCreateUser.Size = new System.Drawing.Size(100, 14);
			this.lblCreateUser.TabIndex = 46;
			this.lblCreateUser.Text = "Create User";
			//
			//txtUpdateTime
			//
            this.txtUpdateTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtUpdateTime.Location = new System.Drawing.Point(504, 43);
			this.txtUpdateTime.MaxLength = 20;
			this.txtUpdateTime.Name = "txtUpdateTime";
			this.txtUpdateTime.ReadOnly = true;
			this.txtUpdateTime.Size = new System.Drawing.Size(152, 19);
			this.txtUpdateTime.TabIndex = 3;
			//
			//txtCreateTime
			//
            this.txtCreateTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtCreateTime.Location = new System.Drawing.Point(504, 19);
			this.txtCreateTime.MaxLength = 20;
			this.txtCreateTime.Name = "txtCreateTime";
			this.txtCreateTime.ReadOnly = true;
			this.txtCreateTime.Size = new System.Drawing.Size(152, 19);
			this.txtCreateTime.TabIndex = 2;
			//
			//txtUpdateUser
			//
            this.txtUpdateUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtUpdateUser.Location = new System.Drawing.Point(120, 43);
			this.txtUpdateUser.MaxLength = 20;
			this.txtUpdateUser.Name = "txtUpdateUser";
			this.txtUpdateUser.ReadOnly = true;
			this.txtUpdateUser.Size = new System.Drawing.Size(152, 19);
			this.txtUpdateUser.TabIndex = 1;
			//
			//txtCreateUser
			//
            this.txtCreateUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtCreateUser.Location = new System.Drawing.Point(120, 19);
			this.txtCreateUser.MaxLength = 20;
			this.txtCreateUser.Name = "txtCreateUser";
			this.txtCreateUser.ReadOnly = true;
			this.txtCreateUser.Size = new System.Drawing.Size(152, 19);
			this.txtCreateUser.TabIndex = 0;
			//
			//frmFMBSetupGlobalOption
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(742, 551);
			this.Controls.Add(this.pnlMid);
			this.Controls.Add(this.pnlBottom);
			this.Controls.Add(this.pnlTop);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.MinimumSize = new System.Drawing.Size(750, 580);
			this.Name = "frmFMBSetupGlobalOption";
			this.Tag = "FMB1001";
			this.Text = "Global Option Setup";
			this.pnlTop.ResumeLayout(false);
			this.grpFactory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cdvFactory).EndInit();
			this.pnlBottom.ResumeLayout(false);
			this.pnlMid.ResumeLayout(false);
			this.grpOption.ResumeLayout(false);
			this.pnlResColorSetting.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.utcBack).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cboSize).EndInit();
			((System.ComponentModel.ISupportInitialize) this.utcBackColor).EndInit();
			((System.ComponentModel.ISupportInitialize) this.utcTextColor).EndInit();
			((System.ComponentModel.ISupportInitialize) this.ftnFontName).EndInit();
			this.grpSignal.ResumeLayout(false);
			this.grpSize.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.spdSize).EndInit();
			((System.ComponentModel.ISupportInitialize) this.spdSize_Sheet1).EndInit();
			this.grpCreateInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.txtUpdateTime).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtCreateTime).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtUpdateUser).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtCreateUser).EndInit();
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		#region " Constant Definition"
		private const int COL_WIDTH = 0;
		private const int COL_HEIGHT = 1;
		#endregion
		
		#region " Variable Definition"
		#endregion
		
		#region " Function Implementations"
		
		// CheckCondition()
		//       - Check the conditions before transaction
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep as String  : Step
		//
		private bool CheckCondition(string sStep)
		{
			
			try
			{
				int i;
				int j;
                switch (CmnFunction.RTrim(sStep))
				{
					case "1":
						
						if (cdvFactory.Text == "")
						{
                            CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
							cdvFactory.Focus();
							return false;
						}
						break;
					case "2":
						
						if (cdvFactory.Text == "")
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
							cdvFactory.Focus();
							return false;
						}
						for (i = 0; i <= spdSize.Sheets[0].RowCount - 1; i++)
						{
							for (j = 0; j <= spdSize.Sheets[0].ColumnCount - 1; j++)
							{
								if (System.Convert.ToString(spdSize.Sheets[0].GetValue(i, j)) == "")
								{
									CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
									spdSize.Sheets[0].SetActiveCell(i, j);
									spdSize.Select();
									return false;
								}
								else if (System.Convert.ToString(spdSize.Sheets[0].GetValue(i, j)) != "")
								{
                                    if (CmnFunction.CheckNumeric(spdSize.Sheets[0].GetValue(i, j).ToString()) == false)
									{
										CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(17), "FMB Client", MessageBoxButtons.OK, 1);
										spdSize.Sheets[0].SetActiveCell(i, j);
										spdSize.Select();
										return false;
									}
								}
							}
						}
						if (ftnFontName.Text == "")
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
							ftnFontName.Focus();
							return false;
						}
						break;
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.CheckCondition()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_Environment()
		//       - Update Environment Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//

		private bool Update_Environment()
		{
			
			try
			{
                FMB_Update_Environment_In_Tag Update_Environment_In = new FMB_Update_Environment_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				
				Update_Environment_In._C.h_factory = cdvFactory.Text;
				Update_Environment_In._C.h_language = GlobalVariable.gcLanguage;
                Update_Environment_In._C.h_user_id = GlobalVariable.gsUserID;
                Update_Environment_In._C.h_proc_step = modGlobalConstant.MP_STEP_UPDATE;
				Update_Environment_In._C.h_passport = GlobalVariable.gsPassport;
				Update_Environment_In._C.h_password = GlobalVariable.gsPassword;
				
				FarPoint.Win.Spread.SheetView with_1 = spdSize.Sheets[0];
				Update_Environment_In._C.layout_width = CmnFunction.ToInt(with_1.GetValue(0, COL_WIDTH));
				Update_Environment_In._C.layout_height = CmnFunction.ToInt(with_1.GetValue(0, COL_HEIGHT));
				Update_Environment_In._C.udr_width = CmnFunction.ToInt(with_1.GetValue(1, COL_WIDTH));
				Update_Environment_In._C.udr_height = CmnFunction.ToInt(with_1.GetValue(1, COL_HEIGHT));
				Update_Environment_In._C.res_width = CmnFunction.ToInt(with_1.GetValue(2, COL_WIDTH));
				Update_Environment_In._C.res_height = CmnFunction.ToInt(with_1.GetValue(2, COL_HEIGHT));
				Update_Environment_In._C.rtg_width = CmnFunction.ToInt(with_1.GetValue(3, COL_WIDTH));
				Update_Environment_In._C.rtg_height = CmnFunction.ToInt(with_1.GetValue(3, COL_HEIGHT));
				Update_Environment_In._C.elp_width = CmnFunction.ToInt(with_1.GetValue(4, COL_WIDTH));
				Update_Environment_In._C.elp_height = CmnFunction.ToInt(with_1.GetValue(4, COL_HEIGHT));
				Update_Environment_In._C.tri_width = CmnFunction.ToInt(with_1.GetValue(5, COL_WIDTH));
				Update_Environment_In._C.tri_height = CmnFunction.ToInt(with_1.GetValue(5, COL_HEIGHT));
				Update_Environment_In._C.ver_width = CmnFunction.ToInt(with_1.GetValue(6, COL_WIDTH));
				Update_Environment_In._C.ver_height = CmnFunction.ToInt(with_1.GetValue(6, COL_HEIGHT));
				Update_Environment_In._C.hor_width = CmnFunction.ToInt(with_1.GetValue(7, COL_WIDTH));
				Update_Environment_In._C.hor_height = CmnFunction.ToInt(with_1.GetValue(7, COL_HEIGHT));
				Update_Environment_In._C.pie1_width = CmnFunction.ToInt(with_1.GetValue(8, COL_WIDTH));
				Update_Environment_In._C.pie1_height = CmnFunction.ToInt(with_1.GetValue(8, COL_HEIGHT));
				Update_Environment_In._C.pie2_width = CmnFunction.ToInt(with_1.GetValue(9, COL_WIDTH));
				Update_Environment_In._C.pie2_height = CmnFunction.ToInt(with_1.GetValue(9, COL_HEIGHT));
				Update_Environment_In._C.pie3_width = CmnFunction.ToInt(with_1.GetValue(10, COL_WIDTH));
				Update_Environment_In._C.pie3_height = CmnFunction.ToInt(with_1.GetValue(10, COL_HEIGHT));
				Update_Environment_In._C.pie4_width = CmnFunction.ToInt(with_1.GetValue(11, COL_WIDTH));
				Update_Environment_In._C.pie4_height = CmnFunction.ToInt(with_1.GetValue(11, COL_HEIGHT));
				
				Update_Environment_In._C.event_color_flag = chkEventColor.Checked == true ? 'Y' : ' ';
				Update_Environment_In._C.signal_flag = rbnProc.Checked == true ? 'P' : 'C';
				Update_Environment_In._C.font_family = ftnFontName.Text;
				
				if (utcTextColor.Color.IsKnownColor == true)
				{
					Update_Environment_In._C.text_color = (int)utcTextColor.Color.ToKnownColor();
				}
				else
				{
					Update_Environment_In._C.text_color = utcTextColor.Color.ToArgb();
				}
				if (utcBackColor.Color.IsKnownColor == true)
				{
                    Update_Environment_In._C.back_color = (int)utcBackColor.Color.ToKnownColor();
				}
				else
				{
					Update_Environment_In._C.back_color = utcBackColor.Color.ToArgb();
				}
				Update_Environment_In._C.text_size = Convert.ToInt32(cboSize.Text);

                if (FMBSender.FMB_Update_Environment(Update_Environment_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.Update_Environment()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// View_Environment()
		//       - View Environment Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool View_Environment()
		{
			
			try
			{
                FMB_View_Environment_In_Tag View_Environment_In = new FMB_View_Environment_In_Tag();
                FMB_View_Environment_Out_Tag View_Environment_Out = new FMB_View_Environment_Out_Tag();
				
				View_Environment_In.h_factory = cdvFactory.Text;
				View_Environment_In.h_language = GlobalVariable.gcLanguage;
				View_Environment_In.h_user_id = GlobalVariable.gsUserID;
				View_Environment_In.h_proc_step = modGlobalConstant.MP_STEP_UPDATE;
				View_Environment_In.h_passport = GlobalVariable.gsPassport;
				View_Environment_In.h_password = GlobalVariable.gsPassword;

                if (FMBSender.FMB_View_Environment(View_Environment_In, ref View_Environment_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
				
				if (View_Environment_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Environment_Out.h_msg_code, View_Environment_Out.h_msg, View_Environment_Out.h_db_err_msg, View_Environment_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				FarPoint.Win.Spread.SheetView with_1 = spdSize.Sheets[0];
				with_1.SetValue(0, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.layout_width));
				with_1.SetValue(0, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.layout_height));
				with_1.SetValue(1, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.udr_width));
				with_1.SetValue(1, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.udr_height));
				with_1.SetValue(2, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.res_width));
				with_1.SetValue(2, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.res_height));
				with_1.SetValue(3, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.rtg_width));
				with_1.SetValue(3, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.rtg_height));
				with_1.SetValue(4, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.elp_width));
				with_1.SetValue(4, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.elp_height));
				with_1.SetValue(5, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.tri_width));
				with_1.SetValue(5, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.tri_height));
				with_1.SetValue(6, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.ver_width));
				with_1.SetValue(6, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.ver_height));
				with_1.SetValue(7, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.hor_width));
				with_1.SetValue(7, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.hor_height));
				with_1.SetValue(8, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.pie1_width));
				with_1.SetValue(8, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.pie1_height));
				with_1.SetValue(9, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.pie2_width));
				with_1.SetValue(9, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.pie2_height));
				with_1.SetValue(10, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.pie3_width));
				with_1.SetValue(10, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.pie3_height));
				with_1.SetValue(11, COL_WIDTH, CmnFunction.ToInt(View_Environment_Out.pie4_width));
				with_1.SetValue(11, COL_HEIGHT, CmnFunction.ToInt(View_Environment_Out.pie4_height));
				chkEventColor.Checked = (CmnFunction.RTrim(View_Environment_Out.event_color_flag) == "Y") ? true : false;
				if (View_Environment_Out.signal_flag == 'P')
				{
					rbnProc.Checked = true;
				}
				else
				{
					rbnControl.Checked = true;
				}
				
				if (View_Environment_Out.text_color < 0)
				{
					utcTextColor.Color = System.Drawing.Color.FromArgb(View_Environment_Out.text_color);
				}
				else if (View_Environment_Out.text_color > 0)
				{
					utcTextColor.Color = System.Drawing.Color.FromKnownColor((KnownColor)View_Environment_Out.text_color);
				}
				else
				{
					utcTextColor.Color = SystemColors.Control;
				}
				
				if (View_Environment_Out.back_color < 0)
				{
					utcBackColor.Color = System.Drawing.Color.FromArgb(View_Environment_Out.back_color);
				}
				else if (View_Environment_Out.back_color > 0)
				{
					utcBackColor.Color = System.Drawing.Color.FromKnownColor((KnownColor)View_Environment_Out.back_color);
				}
				else
				{
					utcBackColor.Color = SystemColors.Control;
				}
				cboSize.Text = Convert.ToString(View_Environment_Out.text_size);
				
				ftnFontName.Text = CmnFunction.RTrim(View_Environment_Out.font_family);
				txtCreateUser.Text = CmnFunction.RTrim(View_Environment_Out.create_user_id);
                txtCreateTime.Text = CmnFunction.MakeDateFormat(View_Environment_Out.create_time, DATE_TIME_FORMAT.NONE);
                txtUpdateUser.Text = CmnFunction.RTrim(View_Environment_Out.update_user_id);
				txtUpdateTime.Text = CmnFunction.MakeDateFormat(View_Environment_Out.update_time, DATE_TIME_FORMAT.NONE);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.Update_Environment()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_Event_Color()
		//       - Update Resource Color
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool Update_Event_Color()
		{
			
			try
			{
                FMB_Update_Event_Color_In_Tag Update_Event_Color_In = new FMB_Update_Event_Color_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				int i;
				
				Update_Event_Color_In._C.h_factory = cdvFactory.Text;
				Update_Event_Color_In._C.h_language = GlobalVariable.gcLanguage;
				Update_Event_Color_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_Event_Color_In._C.h_proc_step = modGlobalConstant.MP_STEP_UPDATE;
				Update_Event_Color_In._C.h_passport = GlobalVariable.gsPassport;
				Update_Event_Color_In._C.h_password = GlobalVariable.gsPassword;
				
				Update_Event_Color_In._C.count = 0;
				Update_Event_Color_In._C._size_event_list = 0;

                Update_Event_Color_In._C.event_list = new FMB_UPDATE_EVENT_COLOR_IN_TAG_event_list[lisEvent.Items.Count + 1];
				
				for (i = 0; i <= lisEvent.Items.Count - 1; i++)
				{
					Update_Event_Color_In._C.event_list[i].event_id = lisEvent.Items[i].Text;
					if (lisEvent.Items[i].SubItems[0].BackColor.IsKnownColor == true)
					{
						Update_Event_Color_In._C.event_list[i].color = (int)lisEvent.Items[i].SubItems[0].BackColor.ToKnownColor();
					}
					else
					{
						Update_Event_Color_In._C.event_list[i].color = lisEvent.Items[i].SubItems[0].BackColor.ToArgb();
					}
				}
				
				Update_Event_Color_In._C.count = i;
				Update_Event_Color_In._C._size_event_list = i;

                if (FMBSender.FMB_Update_Event_Color(Update_Event_Color_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.Update_Event_Color()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
			}
			
		}
		
		#endregion
		
		#region " Event Implementations"
		
		private void cdvFactory_ButtonPress(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				cdvFactory.Init();
				cdvFactory.Columns.Add("Factory", 100, HorizontalAlignment.Left);
				cdvFactory.Columns.Add("Desc", 100, HorizontalAlignment.Left);
				cdvFactory.SelectedSubItemIndex = 0;
				//cdvFactory.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
                //WIPLIST.ViewFactoryList(cdvFactory.GetListView, '1',null);
				cdvFactory.AddEmptyRow(1);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.cdvFactory_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnUpdate_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (CheckCondition("2") == false)
				{
					return;
				}
				if (Update_Environment() == false)
				{
					return;
				}
				if (chkEventColor.Checked == true)
				{
					if (Update_Event_Color() == false)
					{
						return;
					}
				}
				
				if (modCommonFunction.ViewGlobalOption(cdvFactory.Text) == false)
				{
					return;
				}
				
				CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.btnUpdate_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnClose_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Dispose();
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void chkEventColor_CheckedChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				if (GlobalVariable.gcLanguage == '2')
				{
					btnSetting.Text = "º≥¡§";
				}
				else
				{
					btnSetting.Text = "Setting";
				}
				
				if (chkEventColor.Checked == true)
				{
					btnSetting.Visible = true;
				}
				else
				{
					btnSetting.Visible = false;
					pnlResColorSetting.Visible = false;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.chkEventColor_CheckedChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
		{
			
			try
			{
				spdSize_Sheet1.ClearRange(0, 0, 12, 2, true);
				lisEvent.Items.Clear();
                CmnFunction.FieldClear(grpOption, null, null, null, null, null, false);
				CmnFunction.FieldClear(grpCreateInfo, null, null, null, null, null, false);
				rbnProc.Checked = true;
                if (modListRoutine.ViewEventColorList(lisEvent, '1', CmnFunction.RTrim(cdvFactory.Text)) == false)
				{
					return;
				}
				btnView.PerformClick();
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.cdvFactory_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnView_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (CheckCondition("1") == false)
				{
					return;
				}
				if (View_Environment() == false)
				{
					return;
				}
                if (modListRoutine.ViewEventColorList(lisEvent, '1', CmnFunction.RTrim(cdvFactory.Text)) == false)
				{
					return;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.btnView_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnSetting_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				pnlResColorSetting.Visible = ! pnlResColorSetting.Visible;
				
				if (pnlResColorSetting.Visible == true)
				{
					if (GlobalVariable.gcLanguage == '2')
					{
						btnSetting.Text = "¥›±‚";
					}
					else
					{
						btnSetting.Text = "Close";
					}
				}
				else
				{
					if (GlobalVariable.gcLanguage == '2')
					{
						btnSetting.Text = "º≥¡§";
					}
					else
					{
						btnSetting.Text = "Setting";
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.btnSetting_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void lisEvent_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (lisEvent.SelectedItems.Count < 1)
				{
					utcBack.Visible = false;
					return;
				}
				
				utcBack.Visible = true;
				utcBack.Color = lisEvent.SelectedItems[0].SubItems[0].BackColor;
				Rectangle rect = lisEvent.GetItemRect(lisEvent.SelectedItems[0].Index);
				
				utcBack.Location = new Point(utcBack.Left, rect.Y - 1);
				
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.lisEvent_SelectedIndexChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void lisEvent_VisibleChanged(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (lisEvent.SelectedItems.Count > 0)
				{
					utcBack.Visible = true;
				}
				else
				{
					utcBack.Visible = false;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.lisEvent_VisibleChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void utcBack_ColorChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				lisEvent.SelectedItems[0].SubItems[0].BackColor = utcBack.Color;
				lisEvent.SelectedItems[0].SubItems[1].Text = utcBack.Text;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.utcBack_ColorChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_TextBoxTextChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				//spdSize_Sheet1.ClearRange(0, 0, 12, 2, True)
				lisEvent.Items.Clear();
				CmnFunction.FieldClear(grpOption, null, null, null, null, null, false);
				CmnFunction.FieldClear(grpCreateInfo, null, null, null, null, null, false);
				rbnProc.Checked = true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.cdvFactory_TextBoxTextChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBSetupGlobalOption_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				modCommonFunction.CheckSecurityFormControl(this);
				modCommonFunction.GetTextboxStyle(this.Controls);
				modLanguageFunction.ToClientLanguage(this);
				modCommonFunction.SetFontSize(cboSize);
				cboSize.SelectedIndex = 2;
				
				modCommonFunction.CheckAllFactoryOption(cdvFactory);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupGlobalOption.frmFMBSetupGlobalOption_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		#endregion
		
	}
	
}
