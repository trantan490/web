
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Infragistics.Win.UltraWinEditors;
//using FMBUI.Enums;
//using FMBUI.Controls;
using com.miracom.transceiverx.session;
using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : frmFMBCreateResource.vb
//   Description : Create/Update/Delete Resource Location
//
//   FMB Version : 1.0.0
//
//   Function List
//       - init() : Initialize Controls
//       - Set_ReadOnly() : Set Controls ReadOnly
//       - CheckCondition() : Check the conditions before transaction
//       - View_LayOut() : View Layout Information
//       - View_UDR_Group() : View User Define Resource Group Information
//       - Update_Resource_Location() : Create/Update/Delete Resource Location
//       - ViewResourceList() : View Resource List
//       - View_Resource() : View Resource Information
//       - ViewUDRResourceList() : View User Define Resource List
//       - View_UDR_Resource() : View User Define Resource Information
//       - Update_UDR_ResLoc() : Create/Update/Delete User Define Resource Information
//
//   Detail Description
//       -
//
//   History
//       - 2005-02-02 : Created by H.K.Kim
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
	public class frmFMBCreateResource : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBCreateResource()
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			
		}
		
		public frmFMBCreateResource(char sProcStep)
		{
			
			//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			InitializeComponent();
			
			//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
			ProcStep = sProcStep;
			
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
		internal System.Windows.Forms.Panel pnlMid;
		internal System.Windows.Forms.GroupBox grpInfo;
		internal UltraTextEditor txtY;
		internal System.Windows.Forms.Label lblY;
		internal UltraTextEditor txtHeight;
		internal System.Windows.Forms.Label lblHeight;
		internal UltraTextEditor txtWidth;
		internal UltraTextEditor txtX;
		internal System.Windows.Forms.Label lblWidth;
		internal System.Windows.Forms.Label lblX;
		internal System.Windows.Forms.Label lblBackColor;
		internal UltraComboEditor cboTextStyle;
		internal System.Windows.Forms.Label lblStyle;
		internal System.Windows.Forms.Label lblColor;
		internal UltraComboEditor cboSize;
		internal UltraTextEditor txtText;
		internal System.Windows.Forms.Label txtSize;
		internal System.Windows.Forms.Label lblText;
		internal System.Windows.Forms.GroupBox grpRes;
		internal System.Windows.Forms.Label lblResID;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Button btnCreate;
		internal System.Windows.Forms.Label lblLayOut;
		internal System.Windows.Forms.Label lblFactory;
		internal UltraTextEditor txtDesc;
		internal System.Windows.Forms.Label lblDesc;
		internal UltraTextEditor txtProcMode;
		internal System.Windows.Forms.Label lblProcMode;
		internal System.Windows.Forms.Label lblSubArea;
		internal UltraTextEditor txtSubArea;
		internal UltraTextEditor txtArea;
		internal System.Windows.Forms.Label lblArea;
		internal System.Windows.Forms.Label lblUpDown;
		internal UltraTextEditor txtUpDown;
		internal UltraTextEditor txtPriSts;
		internal System.Windows.Forms.Label lblPriSts;
		internal System.Windows.Forms.Label lblEventTime;
		internal UltraTextEditor txtLastEventTime;
		internal UltraTextEditor txtLastEvent;
		internal System.Windows.Forms.Label lblLastEvent;
		internal System.Windows.Forms.Label lblLastEnd;
		internal UltraTextEditor txtLastEnd;
		internal UltraTextEditor txtLastStart;
		internal System.Windows.Forms.Label lblLastStartTime;
		internal UltraTextEditor txtLastDown;
		internal System.Windows.Forms.Label lblDown;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcText;
		internal Infragistics.Win.UltraWinEditors.UltraColorPicker utcBack;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtCtrlMode;
		internal System.Windows.Forms.Label lblCtrlMode;
		internal System.Windows.Forms.Label lblImage;
		internal System.Windows.Forms.PictureBox pctImage;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvResID;
		public Infragistics.Win.UltraWinEditors.UltraTextEditor txtFactory;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayOut;
		internal System.Windows.Forms.Label lblResTagFlag;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtResTagFlag;
		internal Infragistics.Win.UltraWinEditors.UltraTextEditor txtResourceType;
		internal System.Windows.Forms.Label lblResourceType;
		internal System.Windows.Forms.Label lblSignalFlag;
		internal Infragistics.Win.UltraWinEditors.UltraCheckEditor chkSignalFlag;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.pnlBottom = new System.Windows.Forms.Panel();
			base.Load += new System.EventHandler(frmFMBCreateResource_Load);
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnCreate.Click += new System.EventHandler(btnCreate_Click);
			this.pnlMid = new System.Windows.Forms.Panel();
			this.grpInfo = new System.Windows.Forms.GroupBox();
			this.lblSignalFlag = new System.Windows.Forms.Label();
			this.chkSignalFlag = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
			this.pctImage = new System.Windows.Forms.PictureBox();
			this.lblImage = new System.Windows.Forms.Label();
			this.utcBack = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.utcText = new Infragistics.Win.UltraWinEditors.UltraColorPicker();
			this.cdvLayOut = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayOut.ButtonPress += new System.EventHandler(cdvLayOut_ButtonPress);
			this.txtFactory = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblFactory = new System.Windows.Forms.Label();
			this.lblLayOut = new System.Windows.Forms.Label();
			this.txtY = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtY_KeyPress);
			this.lblY = new System.Windows.Forms.Label();
			this.txtHeight = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtHeight_KeyPress);
			this.lblHeight = new System.Windows.Forms.Label();
			this.txtWidth = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtWidth_KeyPress);
			this.txtX = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtX.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtX_KeyPress);
			this.lblWidth = new System.Windows.Forms.Label();
			this.lblX = new System.Windows.Forms.Label();
			this.lblResTagFlag = new System.Windows.Forms.Label();
			this.txtResTagFlag = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblBackColor = new System.Windows.Forms.Label();
			this.cboTextStyle = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.lblStyle = new System.Windows.Forms.Label();
			this.lblColor = new System.Windows.Forms.Label();
			this.cboSize = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.txtText = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtSize = new System.Windows.Forms.Label();
			this.lblText = new System.Windows.Forms.Label();
			this.grpRes = new System.Windows.Forms.GroupBox();
			this.txtCtrlMode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblCtrlMode = new System.Windows.Forms.Label();
			this.txtLastDown = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblDown = new System.Windows.Forms.Label();
			this.lblEventTime = new System.Windows.Forms.Label();
			this.txtLastEventTime = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtLastEvent = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblLastEvent = new System.Windows.Forms.Label();
			this.lblLastEnd = new System.Windows.Forms.Label();
			this.txtLastEnd = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtLastStart = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblLastStartTime = new System.Windows.Forms.Label();
			this.lblUpDown = new System.Windows.Forms.Label();
			this.txtUpDown = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtPriSts = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblPriSts = new System.Windows.Forms.Label();
			this.lblSubArea = new System.Windows.Forms.Label();
			this.txtSubArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtArea = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblArea = new System.Windows.Forms.Label();
			this.lblProcMode = new System.Windows.Forms.Label();
			this.txtProcMode = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.txtResourceType = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblResourceType = new System.Windows.Forms.Label();
			this.txtDesc = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			this.lblDesc = new System.Windows.Forms.Label();
			this.lblResID = new System.Windows.Forms.Label();
			this.cdvResID = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvResID.ButtonPress += new System.EventHandler(cdvResID_ButtonPress);
			this.cdvResID.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvResID_SelectedItemChanged);
			this.pnlBottom.SuspendLayout();
			this.pnlMid.SuspendLayout();
			this.grpInfo.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.utcBack).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.utcText).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayOut).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtFactory).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtY).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtHeight).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtWidth).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtX).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtResTagFlag).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cboTextStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cboSize).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtText).BeginInit();
			this.grpRes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.txtCtrlMode).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastDown).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastEventTime).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastEvent).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastEnd).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastStart).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtUpDown).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtPriSts).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtSubArea).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtArea).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtProcMode).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtResourceType).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.txtDesc).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvResID).BeginInit();
			this.SuspendLayout();
			//
			//pnlBottom
			//
			this.pnlBottom.Controls.Add(this.btnClose);
			this.pnlBottom.Controls.Add(this.btnCreate);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 408);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(458, 40);
			this.pnlBottom.TabIndex = 3;
			//
			//btnClose
			//
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(374, 9);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(74, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			//
			//btnCreate
			//
			this.btnCreate.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCreate.Location = new System.Drawing.Point(294, 9);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(74, 23);
			this.btnCreate.TabIndex = 0;
			this.btnCreate.Text = "Create";
			//
			//pnlMid
			//
			this.pnlMid.Controls.Add(this.grpInfo);
			this.pnlMid.Controls.Add(this.grpRes);
			this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMid.DockPadding.Left = 3;
			this.pnlMid.DockPadding.Right = 3;
			this.pnlMid.Location = new System.Drawing.Point(0, 0);
			this.pnlMid.Name = "pnlMid";
			this.pnlMid.Size = new System.Drawing.Size(458, 408);
			this.pnlMid.TabIndex = 1;
			//
			//grpInfo
			//
			this.grpInfo.Controls.Add(this.lblSignalFlag);
			this.grpInfo.Controls.Add(this.chkSignalFlag);
			this.grpInfo.Controls.Add(this.pctImage);
			this.grpInfo.Controls.Add(this.lblImage);
			this.grpInfo.Controls.Add(this.utcBack);
			this.grpInfo.Controls.Add(this.utcText);
			this.grpInfo.Controls.Add(this.cdvLayOut);
			this.grpInfo.Controls.Add(this.txtFactory);
			this.grpInfo.Controls.Add(this.lblFactory);
			this.grpInfo.Controls.Add(this.lblLayOut);
			this.grpInfo.Controls.Add(this.txtY);
			this.grpInfo.Controls.Add(this.lblY);
			this.grpInfo.Controls.Add(this.txtHeight);
			this.grpInfo.Controls.Add(this.lblHeight);
			this.grpInfo.Controls.Add(this.txtWidth);
			this.grpInfo.Controls.Add(this.txtX);
			this.grpInfo.Controls.Add(this.lblWidth);
			this.grpInfo.Controls.Add(this.lblX);
			this.grpInfo.Controls.Add(this.lblResTagFlag);
			this.grpInfo.Controls.Add(this.txtResTagFlag);
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
			this.grpInfo.Location = new System.Drawing.Point(3, 210);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.Size = new System.Drawing.Size(452, 198);
			this.grpInfo.TabIndex = 1;
			this.grpInfo.TabStop = false;
			//
			//lblSignalFlag
			//
			this.lblSignalFlag.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblSignalFlag.Location = new System.Drawing.Point(248, 46);
			this.lblSignalFlag.Name = "lblSignalFlag";
			this.lblSignalFlag.Size = new System.Drawing.Size(88, 14);
			this.lblSignalFlag.TabIndex = 57;
			this.lblSignalFlag.Text = "View Signal";
			//
			//chkSignalFlag
			//
			this.chkSignalFlag.Checked = true;
			this.chkSignalFlag.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSignalFlag.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.chkSignalFlag.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkSignalFlag.Location = new System.Drawing.Point(231, 46);
			this.chkSignalFlag.Name = "chkSignalFlag";
			this.chkSignalFlag.Size = new System.Drawing.Size(117, 14);
			this.chkSignalFlag.TabIndex = 56;
			//
			//pctImage
			//
			this.pctImage.Location = new System.Drawing.Point(396, 52);
			this.pctImage.Name = "pctImage";
			this.pctImage.Size = new System.Drawing.Size(32, 32);
			this.pctImage.TabIndex = 55;
			this.pctImage.TabStop = false;
			//
			//lblImage
			//
			this.lblImage.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblImage.Location = new System.Drawing.Point(323, 61);
			this.lblImage.Name = "lblImage";
			this.lblImage.Size = new System.Drawing.Size(60, 14);
			this.lblImage.TabIndex = 54;
			this.lblImage.Text = "Image";
			//
			//utcBack
			//
			this.utcBack.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcBack.Location = new System.Drawing.Point(323, 117);
			this.utcBack.Name = "utcBack";
			this.utcBack.Size = new System.Drawing.Size(120, 19);
			this.utcBack.TabIndex = 7;
			this.utcBack.Text = "Control";
			//
			//utcText
			//
			this.utcText.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.utcText.Location = new System.Drawing.Point(104, 117);
			this.utcText.Name = "utcText";
			this.utcText.Size = new System.Drawing.Size(120, 19);
			this.utcText.TabIndex = 6;
			this.utcText.Text = "Control";
			//
			//cdvLayOut
			//
			this.cdvLayOut.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayOut.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayOut.BtnToolTipText = "";
			this.cdvLayOut.Focusing = null;
			this.cdvLayOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayOut.Index = 0;
			this.cdvLayOut.IsViewBtnImage = false;
			this.cdvLayOut.Location = new System.Drawing.Point(323, 20);
			this.cdvLayOut.MaxLength = 20;
			this.cdvLayOut.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayOut.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayOut.Name = "cdvLayOut";
			this.cdvLayOut.ReadOnly = false;
			this.cdvLayOut.SelectedSubItemIndex = - 1;
			this.cdvLayOut.SelectionStart = 0;
			this.cdvLayOut.Size = new System.Drawing.Size(120, 20);
			this.cdvLayOut.SmallImageList = null;
			this.cdvLayOut.TabIndex = 1;
			this.cdvLayOut.TextBoxToolTipText = "";
			this.cdvLayOut.VisibleButton = true;
			this.cdvLayOut.VisibleColumnHeader = false;
			//
			//txtFactory
			//
			this.txtFactory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtFactory.Location = new System.Drawing.Point(104, 20);
			this.txtFactory.MaxLength = 10;
			this.txtFactory.Name = "txtFactory";
			this.txtFactory.ReadOnly = true;
			this.txtFactory.Size = new System.Drawing.Size(120, 19);
			this.txtFactory.TabIndex = 0;
			//
			//lblFactory
			//
			this.lblFactory.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory.Location = new System.Drawing.Point(12, 23);
			this.lblFactory.Name = "lblFactory";
			this.lblFactory.Size = new System.Drawing.Size(88, 14);
			this.lblFactory.TabIndex = 53;
			this.lblFactory.Text = "Factory";
			//
			//lblLayOut
			//
			this.lblLayOut.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayOut.Location = new System.Drawing.Point(231, 23);
			this.lblLayOut.Name = "lblLayOut";
			this.lblLayOut.Size = new System.Drawing.Size(88, 14);
			this.lblLayOut.TabIndex = 51;
			this.lblLayOut.Text = "LayOut ID";
			//
			//txtY
			//
			this.txtY.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtY.Location = new System.Drawing.Point(323, 141);
			this.txtY.MaxLength = 6;
			this.txtY.Name = "txtY";
			this.txtY.Size = new System.Drawing.Size(120, 19);
			this.txtY.TabIndex = 9;
			//
			//lblY
			//
			this.lblY.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblY.Location = new System.Drawing.Point(231, 145);
			this.lblY.Name = "lblY";
			this.lblY.Size = new System.Drawing.Size(88, 14);
			this.lblY.TabIndex = 47;
			this.lblY.Text = "Location Y";
			//
			//txtHeight
			//
			this.txtHeight.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtHeight.Location = new System.Drawing.Point(323, 165);
			this.txtHeight.MaxLength = 6;
			this.txtHeight.Name = "txtHeight";
			this.txtHeight.Size = new System.Drawing.Size(120, 19);
			this.txtHeight.TabIndex = 11;
			//
			//lblHeight
			//
			this.lblHeight.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblHeight.Location = new System.Drawing.Point(231, 169);
			this.lblHeight.Name = "lblHeight";
			this.lblHeight.Size = new System.Drawing.Size(88, 14);
			this.lblHeight.TabIndex = 45;
			this.lblHeight.Text = "Height";
			//
			//txtWidth
			//
			this.txtWidth.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtWidth.Location = new System.Drawing.Point(104, 165);
			this.txtWidth.MaxLength = 6;
			this.txtWidth.Name = "txtWidth";
			this.txtWidth.Size = new System.Drawing.Size(120, 19);
			this.txtWidth.TabIndex = 10;
			//
			//txtX
			//
			this.txtX.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtX.Location = new System.Drawing.Point(104, 141);
			this.txtX.MaxLength = 6;
			this.txtX.Name = "txtX";
			this.txtX.Size = new System.Drawing.Size(120, 19);
			this.txtX.TabIndex = 8;
			//
			//lblWidth
			//
			this.lblWidth.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblWidth.Location = new System.Drawing.Point(12, 169);
			this.lblWidth.Name = "lblWidth";
			this.lblWidth.Size = new System.Drawing.Size(88, 14);
			this.lblWidth.TabIndex = 42;
			this.lblWidth.Text = "Width";
			//
			//lblX
			//
			this.lblX.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblX.Location = new System.Drawing.Point(12, 145);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(88, 14);
			this.lblX.TabIndex = 41;
			this.lblX.Text = "Location X ";
			//
			//lblResTagFlag
			//
			this.lblResTagFlag.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblResTagFlag.Location = new System.Drawing.Point(12, 48);
			this.lblResTagFlag.Name = "lblResTagFlag";
			this.lblResTagFlag.Size = new System.Drawing.Size(88, 14);
			this.lblResTagFlag.TabIndex = 40;
			this.lblResTagFlag.Text = "Type";
			//
			//txtResTagFlag
			//
			this.txtResTagFlag.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtResTagFlag.Location = new System.Drawing.Point(104, 44);
			this.txtResTagFlag.Name = "txtResTagFlag";
			this.txtResTagFlag.ReadOnly = true;
			this.txtResTagFlag.Size = new System.Drawing.Size(120, 19);
			this.txtResTagFlag.TabIndex = 2;
			this.txtResTagFlag.Text = "R";
			//
			//lblBackColor
			//
			this.lblBackColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblBackColor.Location = new System.Drawing.Point(231, 120);
			this.lblBackColor.Name = "lblBackColor";
			this.lblBackColor.Size = new System.Drawing.Size(88, 14);
			this.lblBackColor.TabIndex = 37;
			this.lblBackColor.Text = "Back Color";
			//
			//cboTextStyle
			//
			this.cboTextStyle.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboTextStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.cboTextStyle.Location = new System.Drawing.Point(323, 92);
			this.cboTextStyle.Name = "cboTextStyle";
			this.cboTextStyle.Size = new System.Drawing.Size(120, 19);
			this.cboTextStyle.TabIndex = 5;
			//
			//lblStyle
			//
			this.lblStyle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblStyle.Location = new System.Drawing.Point(231, 95);
			this.lblStyle.Name = "lblStyle";
			this.lblStyle.Size = new System.Drawing.Size(88, 14);
			this.lblStyle.TabIndex = 34;
			this.lblStyle.Text = "Text Style";
			//
			//lblColor
			//
			this.lblColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblColor.Location = new System.Drawing.Point(12, 120);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(88, 14);
			this.lblColor.TabIndex = 33;
			this.lblColor.Text = "Text Color";
			//
			//cboSize
			//
			this.cboSize.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboSize.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.cboSize.Location = new System.Drawing.Point(104, 92);
			this.cboSize.Name = "cboSize";
			this.cboSize.Size = new System.Drawing.Size(120, 19);
			this.cboSize.TabIndex = 4;
			//
			//txtText
			//
			this.txtText.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtText.Location = new System.Drawing.Point(104, 68);
			this.txtText.MaxLength = 40;
			this.txtText.Name = "txtText";
			this.txtText.Size = new System.Drawing.Size(200, 19);
			this.txtText.TabIndex = 3;
			//
			//txtSize
			//
			this.txtSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.txtSize.Location = new System.Drawing.Point(12, 96);
			this.txtSize.Name = "txtSize";
			this.txtSize.Size = new System.Drawing.Size(88, 14);
			this.txtSize.TabIndex = 30;
			this.txtSize.Text = "Text Font Size";
			//
			//lblText
			//
			this.lblText.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblText.Location = new System.Drawing.Point(12, 72);
			this.lblText.Name = "lblText";
			this.lblText.Size = new System.Drawing.Size(88, 14);
			this.lblText.TabIndex = 29;
			this.lblText.Text = "Text";
			//
			//grpRes
			//
			this.grpRes.Controls.Add(this.txtCtrlMode);
			this.grpRes.Controls.Add(this.lblCtrlMode);
			this.grpRes.Controls.Add(this.txtLastDown);
			this.grpRes.Controls.Add(this.lblDown);
			this.grpRes.Controls.Add(this.lblEventTime);
			this.grpRes.Controls.Add(this.txtLastEventTime);
			this.grpRes.Controls.Add(this.txtLastEvent);
			this.grpRes.Controls.Add(this.lblLastEvent);
			this.grpRes.Controls.Add(this.lblLastEnd);
			this.grpRes.Controls.Add(this.txtLastEnd);
			this.grpRes.Controls.Add(this.txtLastStart);
			this.grpRes.Controls.Add(this.lblLastStartTime);
			this.grpRes.Controls.Add(this.lblUpDown);
			this.grpRes.Controls.Add(this.txtUpDown);
			this.grpRes.Controls.Add(this.txtPriSts);
			this.grpRes.Controls.Add(this.lblPriSts);
			this.grpRes.Controls.Add(this.lblSubArea);
			this.grpRes.Controls.Add(this.txtSubArea);
			this.grpRes.Controls.Add(this.txtArea);
			this.grpRes.Controls.Add(this.lblArea);
			this.grpRes.Controls.Add(this.lblProcMode);
			this.grpRes.Controls.Add(this.txtProcMode);
			this.grpRes.Controls.Add(this.txtResourceType);
			this.grpRes.Controls.Add(this.lblResourceType);
			this.grpRes.Controls.Add(this.txtDesc);
			this.grpRes.Controls.Add(this.lblDesc);
			this.grpRes.Controls.Add(this.lblResID);
			this.grpRes.Controls.Add(this.cdvResID);
			this.grpRes.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpRes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpRes.Location = new System.Drawing.Point(3, 0);
			this.grpRes.Name = "grpRes";
			this.grpRes.Size = new System.Drawing.Size(452, 210);
			this.grpRes.TabIndex = 0;
			this.grpRes.TabStop = false;
			//
			//txtCtrlMode
			//
			this.txtCtrlMode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtCtrlMode.Location = new System.Drawing.Point(323, 136);
			this.txtCtrlMode.MaxLength = 10;
			this.txtCtrlMode.Name = "txtCtrlMode";
			this.txtCtrlMode.ReadOnly = true;
			this.txtCtrlMode.Size = new System.Drawing.Size(120, 19);
			this.txtCtrlMode.TabIndex = 9;
			//
			//lblCtrlMode
			//
			this.lblCtrlMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCtrlMode.Location = new System.Drawing.Point(231, 138);
			this.lblCtrlMode.Name = "lblCtrlMode";
			this.lblCtrlMode.Size = new System.Drawing.Size(88, 14);
			this.lblCtrlMode.TabIndex = 80;
			this.lblCtrlMode.Text = "Control Mode";
			//
			//txtLastDown
			//
			this.txtLastDown.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtLastDown.Location = new System.Drawing.Point(104, 183);
			this.txtLastDown.MaxLength = 20;
			this.txtLastDown.Name = "txtLastDown";
			this.txtLastDown.ReadOnly = true;
			this.txtLastDown.Size = new System.Drawing.Size(120, 19);
			this.txtLastDown.TabIndex = 12;
			//
			//lblDown
			//
			this.lblDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDown.Location = new System.Drawing.Point(8, 187);
			this.lblDown.Name = "lblDown";
			this.lblDown.Size = new System.Drawing.Size(88, 14);
			this.lblDown.TabIndex = 78;
			this.lblDown.Text = "Last Down Time";
			//
			//lblEventTime
			//
			this.lblEventTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblEventTime.Location = new System.Drawing.Point(231, 162);
			this.lblEventTime.Name = "lblEventTime";
			this.lblEventTime.Size = new System.Drawing.Size(89, 14);
			this.lblEventTime.TabIndex = 77;
			this.lblEventTime.Text = "Last Event Time";
			//
			//txtLastEventTime
			//
			this.txtLastEventTime.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtLastEventTime.Location = new System.Drawing.Point(323, 160);
			this.txtLastEventTime.MaxLength = 20;
			this.txtLastEventTime.Name = "txtLastEventTime";
			this.txtLastEventTime.ReadOnly = true;
			this.txtLastEventTime.Size = new System.Drawing.Size(120, 19);
			this.txtLastEventTime.TabIndex = 11;
			//
			//txtLastEvent
			//
			this.txtLastEvent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtLastEvent.Location = new System.Drawing.Point(104, 136);
			this.txtLastEvent.MaxLength = 12;
			this.txtLastEvent.Name = "txtLastEvent";
			this.txtLastEvent.ReadOnly = true;
			this.txtLastEvent.Size = new System.Drawing.Size(120, 19);
			this.txtLastEvent.TabIndex = 8;
			//
			//lblLastEvent
			//
			this.lblLastEvent.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLastEvent.Location = new System.Drawing.Point(9, 140);
			this.lblLastEvent.Name = "lblLastEvent";
			this.lblLastEvent.Size = new System.Drawing.Size(88, 14);
			this.lblLastEvent.TabIndex = 74;
			this.lblLastEvent.Text = "Last Event";
			//
			//lblLastEnd
			//
			this.lblLastEnd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLastEnd.Location = new System.Drawing.Point(231, 185);
			this.lblLastEnd.Name = "lblLastEnd";
			this.lblLastEnd.Size = new System.Drawing.Size(88, 14);
			this.lblLastEnd.TabIndex = 73;
			this.lblLastEnd.Text = "Last End Time";
			//
			//txtLastEnd
			//
			this.txtLastEnd.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtLastEnd.Location = new System.Drawing.Point(323, 183);
			this.txtLastEnd.MaxLength = 20;
			this.txtLastEnd.Name = "txtLastEnd";
			this.txtLastEnd.ReadOnly = true;
			this.txtLastEnd.Size = new System.Drawing.Size(120, 19);
			this.txtLastEnd.TabIndex = 13;
			//
			//txtLastStart
			//
			this.txtLastStart.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtLastStart.Location = new System.Drawing.Point(104, 160);
			this.txtLastStart.MaxLength = 20;
			this.txtLastStart.Name = "txtLastStart";
			this.txtLastStart.ReadOnly = true;
			this.txtLastStart.Size = new System.Drawing.Size(120, 19);
			this.txtLastStart.TabIndex = 10;
			//
			//lblLastStartTime
			//
			this.lblLastStartTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLastStartTime.Location = new System.Drawing.Point(9, 164);
			this.lblLastStartTime.Name = "lblLastStartTime";
			this.lblLastStartTime.Size = new System.Drawing.Size(88, 14);
			this.lblLastStartTime.TabIndex = 70;
			this.lblLastStartTime.Text = "Last Start Time";
			//
			//lblUpDown
			//
			this.lblUpDown.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblUpDown.Location = new System.Drawing.Point(231, 68);
			this.lblUpDown.Name = "lblUpDown";
			this.lblUpDown.Size = new System.Drawing.Size(88, 14);
			this.lblUpDown.TabIndex = 69;
			this.lblUpDown.Text = "Up / Down ";
			//
			//txtUpDown
			//
			this.txtUpDown.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtUpDown.Location = new System.Drawing.Point(323, 65);
			this.txtUpDown.MaxLength = 5;
			this.txtUpDown.Name = "txtUpDown";
			this.txtUpDown.ReadOnly = true;
			this.txtUpDown.Size = new System.Drawing.Size(120, 19);
			this.txtUpDown.TabIndex = 3;
			//
			//txtPriSts
			//
			this.txtPriSts.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtPriSts.Location = new System.Drawing.Point(104, 65);
			this.txtPriSts.MaxLength = 30;
			this.txtPriSts.Name = "txtPriSts";
			this.txtPriSts.ReadOnly = true;
			this.txtPriSts.Size = new System.Drawing.Size(120, 19);
			this.txtPriSts.TabIndex = 2;
			//
			//lblPriSts
			//
			this.lblPriSts.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblPriSts.Location = new System.Drawing.Point(9, 68);
			this.lblPriSts.Name = "lblPriSts";
			this.lblPriSts.Size = new System.Drawing.Size(88, 14);
			this.lblPriSts.TabIndex = 66;
			this.lblPriSts.Text = "Primary Status";
			//
			//lblSubArea
			//
			this.lblSubArea.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblSubArea.Location = new System.Drawing.Point(231, 92);
			this.lblSubArea.Name = "lblSubArea";
			this.lblSubArea.Size = new System.Drawing.Size(88, 14);
			this.lblSubArea.TabIndex = 65;
			this.lblSubArea.Text = "Sub Area ID";
			//
			//txtSubArea
			//
			this.txtSubArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtSubArea.Location = new System.Drawing.Point(323, 89);
			this.txtSubArea.MaxLength = 20;
			this.txtSubArea.Name = "txtSubArea";
			this.txtSubArea.ReadOnly = true;
			this.txtSubArea.Size = new System.Drawing.Size(120, 19);
			this.txtSubArea.TabIndex = 5;
			//
			//txtArea
			//
			this.txtArea.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtArea.Location = new System.Drawing.Point(104, 89);
			this.txtArea.MaxLength = 20;
			this.txtArea.Name = "txtArea";
			this.txtArea.ReadOnly = true;
			this.txtArea.Size = new System.Drawing.Size(120, 19);
			this.txtArea.TabIndex = 4;
			//
			//lblArea
			//
			this.lblArea.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblArea.Location = new System.Drawing.Point(9, 92);
			this.lblArea.Name = "lblArea";
			this.lblArea.Size = new System.Drawing.Size(88, 14);
			this.lblArea.TabIndex = 62;
			this.lblArea.Text = "Area ID";
			//
			//lblProcMode
			//
			this.lblProcMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblProcMode.Location = new System.Drawing.Point(231, 116);
			this.lblProcMode.Name = "lblProcMode";
			this.lblProcMode.Size = new System.Drawing.Size(88, 14);
			this.lblProcMode.TabIndex = 61;
			this.lblProcMode.Text = "Proc Mode";
			//
			//txtProcMode
			//
			this.txtProcMode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtProcMode.Location = new System.Drawing.Point(323, 113);
			this.txtProcMode.MaxLength = 10;
			this.txtProcMode.Name = "txtProcMode";
			this.txtProcMode.ReadOnly = true;
			this.txtProcMode.Size = new System.Drawing.Size(120, 19);
			this.txtProcMode.TabIndex = 7;
			//
			//txtResourceType
			//
			this.txtResourceType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtResourceType.Location = new System.Drawing.Point(104, 113);
			this.txtResourceType.MaxLength = 20;
			this.txtResourceType.Name = "txtResourceType";
			this.txtResourceType.ReadOnly = true;
			this.txtResourceType.Size = new System.Drawing.Size(120, 19);
			this.txtResourceType.TabIndex = 6;
			//
			//lblResourceType
			//
			this.lblResourceType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblResourceType.Location = new System.Drawing.Point(9, 116);
			this.lblResourceType.Name = "lblResourceType";
			this.lblResourceType.Size = new System.Drawing.Size(88, 14);
			this.lblResourceType.TabIndex = 57;
			this.lblResourceType.Text = "Resource Type";
			//
			//txtDesc
			//
			this.txtDesc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			this.txtDesc.Location = new System.Drawing.Point(104, 41);
			this.txtDesc.MaxLength = 50;
			this.txtDesc.Name = "txtDesc";
			this.txtDesc.ReadOnly = true;
			this.txtDesc.Size = new System.Drawing.Size(340, 19);
			this.txtDesc.TabIndex = 1;
			//
			//lblDesc
			//
			this.lblDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblDesc.Location = new System.Drawing.Point(9, 44);
			this.lblDesc.Name = "lblDesc";
			this.lblDesc.Size = new System.Drawing.Size(88, 14);
			this.lblDesc.TabIndex = 55;
			this.lblDesc.Text = "Description";
			//
			//lblResID
			//
			this.lblResID.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblResID.Location = new System.Drawing.Point(9, 20);
			this.lblResID.Name = "lblResID";
			this.lblResID.Size = new System.Drawing.Size(88, 14);
			this.lblResID.TabIndex = 28;
			this.lblResID.Text = "Resource ID";
			//
			//cdvResID
			//
			this.cdvResID.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvResID.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvResID.BtnToolTipText = "";
			this.cdvResID.Focusing = null;
			this.cdvResID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvResID.Index = 0;
			this.cdvResID.IsViewBtnImage = false;
			this.cdvResID.Location = new System.Drawing.Point(104, 17);
			this.cdvResID.MaxLength = 20;
			this.cdvResID.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvResID.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvResID.Name = "cdvResID";
			this.cdvResID.ReadOnly = false;
			this.cdvResID.SelectedSubItemIndex = - 1;
			this.cdvResID.SelectionStart = 0;
			this.cdvResID.Size = new System.Drawing.Size(200, 20);
			this.cdvResID.SmallImageList = null;
			this.cdvResID.TabIndex = 0;
			this.cdvResID.TextBoxToolTipText = "";
			this.cdvResID.VisibleButton = true;
			this.cdvResID.VisibleColumnHeader = false;
			//
			//frmFMBCreateResource
			//
			this.AcceptButton = this.btnCreate;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(458, 448);
			this.Controls.Add(this.pnlMid);
			this.Controls.Add(this.pnlBottom);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFMBCreateResource";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create Resource";
			this.pnlBottom.ResumeLayout(false);
			this.pnlMid.ResumeLayout(false);
			this.grpInfo.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.utcBack).EndInit();
			((System.ComponentModel.ISupportInitialize) this.utcText).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayOut).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtFactory).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtY).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtHeight).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtWidth).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtX).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtResTagFlag).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cboTextStyle).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cboSize).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtText).EndInit();
			this.grpRes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.txtCtrlMode).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastDown).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastEventTime).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastEvent).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastEnd).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtLastStart).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtUpDown).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtPriSts).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtSubArea).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtArea).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtProcMode).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtResourceType).EndInit();
			((System.ComponentModel.ISupportInitialize) this.txtDesc).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvResID).EndInit();
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		#region " Variable Definition"
		private ImageList imlRes;
		public int iImageIndex = - 1;
		private int sWidth = 0;
		private int sHeight = 0;
		#endregion
		
		#region " Property Implements"
		
		private char m_sProcStep = ' ';
		
		public char ProcStep
		{
			get
			{
				return m_sProcStep;
			}
			set
			{

                if (m_sProcStep.Equals(value) == false)
				{
                    m_sProcStep = value;
				}
			}
		}
		
		#endregion
		
		#region " Function Implementations"
		
		// init()
		//       - Initialize Controls
		// Return Value
		//       -
		// Arguments
		//       -
		//
		private void init()
		{
			
			try
			{
                Miracom.FMBUI.Controls.udcCtrlResource ctrlRes = new Miracom.FMBUI.Controls.udcCtrlResource(modGlobalVariable.gimlResource);
				imlRes = ctrlRes.imlResource;
				
				modCommonFunction.SetEnumList(cboTextStyle, typeof(FontStyle));
				modCommonFunction.SetFontSize(cboSize);
				cboSize.Text = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(txtFactory.Text, clsOptionData.Options.DefaultTextSize));
				cboTextStyle.SelectedIndex = 0;
				utcText.Color =   (System.Drawing.Color)modGlobalVariable.gGlobalOptions.GetOptions(txtFactory.Text, clsOptionData.Options.DefaultTextColor);
                utcBack.Color = (System.Drawing.Color)modGlobalVariable.gGlobalOptions.GetOptions(txtFactory.Text, clsOptionData.Options.DefaultBackColor);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.init()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		// Set_ReadOnly()
		//       - Set Controls ReadOnly
		// Return Value
		//       -
		// Arguments
		//       -
		//
		private void Set_ReadOnly()
		{
			
			try
			{
                if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
				{
					lblLayOut.Text = "LayOut ID";
				}
                else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
				{
					lblLayOut.Text = "Group ID";
				}
				
				if (ProcStep == modGlobalConstant.MP_STEP_CREATE)
				{
					cdvLayOut.ReadOnly = true;
					cdvLayOut.BackColor = SystemColors.Control;
					cdvLayOut.VisibleButton = false;
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_UPDATE)
				{
					cdvResID.ReadOnly = true;
					cdvResID.BackColor = SystemColors.Control;
					cdvResID.VisibleButton = false;
                    if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
					}
					else
					{
						cdvLayOut.ReadOnly = true;
						cdvLayOut.BackColor = SystemColors.Control;
						cdvLayOut.VisibleButton = false;
					}
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_DELETE)
				{
					cdvResID.ReadOnly = true;
					cdvResID.BackColor = SystemColors.Control;
					cdvResID.VisibleButton = false;
					
					cdvLayOut.ReadOnly = true;
					cdvLayOut.BackColor = SystemColors.Control;
					cdvLayOut.VisibleButton = false;
					
					txtText.ReadOnly = true;
					cboSize.ReadOnly = true;
					cboTextStyle.ReadOnly = true;
					utcText.ReadOnly = true;
					utcBack.ReadOnly = true;
					txtX.ReadOnly = true;
					txtY.ReadOnly = true;
					txtWidth.ReadOnly = true;
					txtHeight.ReadOnly = true;
					chkSignalFlag.Enabled = false;
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_VIEW)
				{
					cdvResID.ReadOnly = true;
					cdvResID.BackColor = SystemColors.Control;
					cdvResID.VisibleButton = false;
					
					cdvLayOut.ReadOnly = true;
					cdvLayOut.BackColor = SystemColors.Control;
					cdvLayOut.VisibleButton = false;
					
					txtText.ReadOnly = true;
					cboSize.ReadOnly = true;
					cboTextStyle.ReadOnly = true;
					utcText.ReadOnly = true;
					utcBack.ReadOnly = true;
					txtX.ReadOnly = true;
					txtY.ReadOnly = true;
					txtWidth.ReadOnly = true;
					txtHeight.ReadOnly = true;
					btnCreate.Visible = false;
					chkSignalFlag.Enabled = false;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.Set_ReadOnly()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
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
				if (cdvResID.Text == "")
				{
					CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
					cdvResID.Focus();
					return false;
				}

                switch (CmnFunction.RTrim(FuncName))
				{
					case "CREATE":

                        if (Convert.ToInt32(txtWidth.Text) < modGlobalConstant.CTRL_MININUM_SIZE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(7) + " " + modGlobalConstant.CTRL_MININUM_SIZE, "FMB Client", MessageBoxButtons.OK, 1);
							txtWidth.Focus();
							return false;
						}
                        if (Convert.ToInt32(txtHeight.Text) < modGlobalConstant.CTRL_MININUM_SIZE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(7) + " " + modGlobalConstant.CTRL_MININUM_SIZE, "FMB Client", MessageBoxButtons.OK, 1);
							txtHeight.Focus();
							return false;
						}
                        if (Convert.ToInt32(txtWidth.Text) > modGlobalConstant.CTRL_MAXIMUM_SIZE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(8) + " " + modGlobalConstant.CTRL_MAXIMUM_SIZE, "FMB Client", MessageBoxButtons.OK, 1);
							txtWidth.Focus();
							return false;
						}
                        if (Convert.ToInt32(txtHeight.Text) > modGlobalConstant.CTRL_MAXIMUM_SIZE)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(8) + " " + modGlobalConstant.CTRL_MAXIMUM_SIZE, "FMB Client", MessageBoxButtons.OK, 1);
							txtHeight.Focus();
							return false;
						}
						if (Convert.ToInt32(txtX.Text) < 0)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(10), "FMB Client", MessageBoxButtons.OK, 1);
							txtX.Focus();
							return false;
						}
                        if (Convert.ToInt32(txtY.Text) < 0)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(10), "FMB Client", MessageBoxButtons.OK, 1);
							txtY.Focus();
							return false;
						}
						
						if (Microsoft.VisualBasic.Information.IsNumeric(txtX.Text) == false)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(17), "FMB Client", MessageBoxButtons.OK, 1);
							txtX.Focus();
							return false;
						}

                        if (Microsoft.VisualBasic.Information.IsNumeric(txtY.Text) == false)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(17), "FMB Client", MessageBoxButtons.OK, 1);
							txtY.Focus();
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
						
						
					case "DELETE":
						
						break;
						
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.CheckCondition()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_Resource_Location()
		//       - Create/Update/Delete Resource Location
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep as String  : Proc Step
		//
		private bool Update_Resource_Location(char sStep)
		{
			
			try
			{
                FMB_Update_ResLoc_In_Tag Update_ResLoc_In = new FMB_Update_ResLoc_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				
				Update_ResLoc_In._C.h_factory = txtFactory.Text;
				Update_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
				Update_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_ResLoc_In._C.h_proc_step = sStep;
				Update_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
				Update_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
				
				Update_ResLoc_In._C.res_id = cdvResID.Text;
				Update_ResLoc_In._C.res_type = txtResTagFlag.Text[0];
				Update_ResLoc_In._C.text = txtText.Text;
                Update_ResLoc_In._C.no_mouse_event = ' ';
                Update_ResLoc_In._C.img_idx = 0;


				if (utcText.Color.IsSystemColor == true || utcText.Color.IsKnownColor == true)
				{
					Update_ResLoc_In._C.text_color = CmnFunction.ToInt(utcText.Color.ToKnownColor());
				}
				else
				{
					Update_ResLoc_In._C.text_color = utcText.Color.ToArgb();
				}
				
				Update_ResLoc_In._C.text_size = CmnFunction.ToInt(cboSize.Text);
				Update_ResLoc_In._C.text_style = Convert.ToChar(cboTextStyle.SelectedIndex + '0');
				
				if (utcBack.Color.IsSystemColor == true || utcBack.Color.IsKnownColor == true)
				{
					Update_ResLoc_In._C.back_color = CmnFunction.ToInt(utcBack.Color.ToKnownColor());
				}
				else
				{
					Update_ResLoc_In._C.back_color = utcBack.Color.ToArgb();
				}
				
				Update_ResLoc_In._C.layout_id = cdvLayOut.Text;
				Update_ResLoc_In._C.loc_x = CmnFunction.ToInt(txtX.Text);
				Update_ResLoc_In._C.loc_y = CmnFunction.ToInt(txtY.Text);
				Update_ResLoc_In._C.loc_width = CmnFunction.ToInt(txtWidth.Text);
				Update_ResLoc_In._C.loc_height = CmnFunction.ToInt(txtHeight.Text);
				
				Update_ResLoc_In._C.tag_type = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Resource);
				
				if (chkSignalFlag.Checked == true)
				{
					Update_ResLoc_In._C.signal_flag = 'Y';
				}
				else
				{
					Update_ResLoc_In._C.signal_flag = ' ';
				}
				
				
				if (sStep != modGlobalConstant.MP_STEP_DELETE)
				{
					if (View_LayOut() == true)
					{
						if (CmnFunction.ToInt(txtX.Text) + CmnFunction.ToInt(txtWidth.Text) > sWidth)
						{
							if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(20), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
							{
								Update_ResLoc_In._C.loc_x = sWidth - CmnFunction.ToInt(txtWidth.Text);
								txtX.Text = System.Convert.ToString(sWidth - CmnFunction.ToInt(txtWidth.Text));
							}
							else
							{
								Update_ResLoc_In._C.loc_x = CmnFunction.ToInt(txtX.Text);
							}
						}
						if (CmnFunction.ToInt(txtY.Text) + CmnFunction.ToInt(txtHeight.Text) > sHeight)
						{
							if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(21), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
							{
								Update_ResLoc_In._C.loc_y = sHeight - CmnFunction.ToInt(txtHeight.Text);
								txtY.Text = System.Convert.ToString(sHeight - CmnFunction.ToInt(txtHeight.Text));
							}
							else
							{
								Update_ResLoc_In._C.loc_y = CmnFunction.ToInt(txtY.Text);
							}
						}
					}
					else
					{
						return false;
					}
				}
                if (FMBSender.FMB_Update_Resource_Location(Update_ResLoc_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.Update_Resource_Location()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// ViewResourceList()
		//       - View Resource List
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - control as Control : Control
		//       - sStep as String    : Proc Step
		//
		private bool ViewResourceList(Control control, string sStep)
		{
			
			try
			{
                FMB_View_Resource_List_In_Tag View_Resource_List_In = new FMB_View_Resource_List_In_Tag();
                FMB_View_Resource_List_Out_Tag View_Resource_List_Out = new FMB_View_Resource_List_Out_Tag();
				
				int i;
				ListViewItem itmX;
				
				if (control is ListView)
				{
                    CmnInitFunction.InitListView((ListView)control);
				}
				
				View_Resource_List_In.h_proc_step = '1';
				View_Resource_List_In.h_passport = GlobalVariable.gsPassport;
				View_Resource_List_In.h_language = GlobalVariable.gcLanguage;
				View_Resource_List_In.h_user_id = GlobalVariable.gsUserID;
				View_Resource_List_In.h_password = GlobalVariable.gsPassword;
				View_Resource_List_In.h_factory = txtFactory.Text;
				
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
							if (View_Resource_List_Out.res_list[i].attached_flag == 'Y')
							{
							}
							else if (View_Resource_List_Out.res_list[i].attached_flag == 'N')
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
								}
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.ViewResourceList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// View_Resource()
		//       - View Resource Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep as String    : Proc Step
		//
		private bool View_Resource(char sStep)
		{
			
			try
			{
                FMB_View_Resource_In_Tag View_Resource_In = new FMB_View_Resource_In_Tag();
                FMB_View_Resource_Out_Tag View_Resource_Out = new FMB_View_Resource_Out_Tag();
				
				View_Resource_In.h_passport = GlobalVariable.gsPassport;
				View_Resource_In.h_language = GlobalVariable.gcLanguage;
				View_Resource_In.h_factory = txtFactory.Text;
				View_Resource_In.h_user_id = GlobalVariable.gsUserID;
				View_Resource_In.h_password = GlobalVariable.gsPassword;
				View_Resource_In.h_proc_step = sStep;
				View_Resource_In.res_id = cdvResID.Text;
                View_Resource_In.res_type = ' ';

                if (FMBSender.FMB_View_Resource(View_Resource_In, ref View_Resource_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
              
				if (View_Resource_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_Resource_Out.h_msg_code, View_Resource_Out.h_msg, View_Resource_Out.h_db_err_msg, View_Resource_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				if (sStep == modGlobalConstant.MP_STEP_CREATE)
				{
					txtDesc.Text = CmnFunction.RTrim(View_Resource_Out.res_desc);
					txtResourceType.Text = CmnFunction.RTrim(View_Resource_Out.res_type);

                    if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode) != "")
                    {
                        if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OL")
                        {
                            txtCtrlMode.Text = "ON LINE";
                        }
                        else if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OR")
                        {
                            txtCtrlMode.Text = "ON LINE REAL";
                        }
                        else if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OF")
                        {
                            txtCtrlMode.Text = "OFF LINE";
                        }
                    }
                    if (CmnFunction.Trim(View_Resource_Out.res_proc_mode) != "")
                    {
                        if (View_Resource_Out.res_proc_mode.Substring(0, 1) == "M")
                        {
                            txtProcMode.Text = "MANUAL";
                        }
                        else if (View_Resource_Out.res_proc_mode.Substring(0, 1) == "S")
                        {
                            txtProcMode.Text = "SEMI AUTO";
                        }
                        else if (View_Resource_Out.res_proc_mode.Substring(0, 1) == "F")
                        {
                            txtProcMode.Text = "FULL AUTO";
                        }
                    }
					txtArea.Text = CmnFunction.RTrim(View_Resource_Out.area_id);
					txtSubArea.Text = CmnFunction.RTrim(View_Resource_Out.sub_area_id);
					
					if (CmnFunction.RTrim(View_Resource_Out.res_up_down_flag) == "U")
					{
						txtUpDown.Text = "UP";
					}
					else if (CmnFunction.RTrim(View_Resource_Out.res_up_down_flag) == "D")
					{
						txtUpDown.Text = "DOWN";
					}
					txtPriSts.Text = CmnFunction.RTrim(View_Resource_Out.res_pri_sts);
					txtLastStart.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_start_time, DATE_TIME_FORMAT.NONE);
					txtLastEnd.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_end_time, DATE_TIME_FORMAT.NONE);
					txtLastEvent.Text = CmnFunction.RTrim(View_Resource_Out.last_event);
					txtLastEventTime.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_event_time, DATE_TIME_FORMAT.NONE);
					txtLastDown.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_down_time, DATE_TIME_FORMAT.NONE);
					
					txtText.Text = cdvResID.Text;
					
					if (View_Resource_Out.img_idx != - 1 && imlRes.Images.Count > View_Resource_Out.img_idx)
					{
						iImageIndex = View_Resource_Out.img_idx;
						pctImage.Image = imlRes.Images[iImageIndex];
					}
					
				}
				else
				{
					txtDesc.Text = CmnFunction.RTrim(View_Resource_Out.res_desc);
					txtResourceType.Text = CmnFunction.RTrim(View_Resource_Out.res_type);
					
					if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OL")
					{
						txtCtrlMode.Text = "ON LINE";
					}
					else if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OR")
					{
						txtCtrlMode.Text = "ON LINE REAL";
					}
					else if (CmnFunction.Trim(View_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OF")
					{
						txtCtrlMode.Text = "OFF LINE";
					}
                    if (CmnFunction.Trim(View_Resource_Out.res_proc_mode) != "")
                    { 
					    if (View_Resource_Out.res_proc_mode.Substring(0, 1) == "M")
					    {
						    txtProcMode.Text = "MANUAL";
					    }
					    else if (View_Resource_Out.res_proc_mode.Substring(0, 1) == "S")
					    {
						    txtProcMode.Text = "SEMI AUTO";
					    }
					    else if (View_Resource_Out.res_proc_mode.Substring(0, 1) == "F")
					    {
						    txtProcMode.Text = "FULL AUTO";
					    }
                    }
					txtArea.Text = CmnFunction.RTrim(View_Resource_Out.area_id);
					txtSubArea.Text = CmnFunction.RTrim(View_Resource_Out.sub_area_id);
					
					if (CmnFunction.RTrim(View_Resource_Out.res_up_down_flag) == "U")
					{
						txtUpDown.Text = "UP";
					}
					else if (CmnFunction.RTrim(View_Resource_Out.res_up_down_flag) == "D")
					{
						txtUpDown.Text = "DOWN";
					}
					txtPriSts.Text = CmnFunction.RTrim(View_Resource_Out.res_pri_sts);
					txtLastStart.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_start_time, DATE_TIME_FORMAT.NONE);
					txtLastEnd.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_end_time, DATE_TIME_FORMAT.NONE);
					txtLastEvent.Text = CmnFunction.RTrim(View_Resource_Out.last_event);
					txtLastEventTime.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_event_time, DATE_TIME_FORMAT.NONE);
					txtLastDown.Text = CmnFunction.MakeDateFormat(View_Resource_Out.last_down_time, DATE_TIME_FORMAT.NONE);
					
					cdvLayOut.Text = CmnFunction.RTrim(View_Resource_Out.layout_id);
                    txtText.Text = CmnFunction.RTrim(View_Resource_Out.text);
					cboSize.Text = CmnFunction.Trim(View_Resource_Out.text_size);
					cboTextStyle.SelectedIndex = CmnFunction.ToInt(View_Resource_Out.text_style.ToString());
					
					if (View_Resource_Out.text_color < 0)
					{
						utcText.Color = Color.FromArgb(View_Resource_Out.text_color);
                       
					}
					else if (View_Resource_Out.text_color > 0)
					{
                        utcText.Color = Color.FromKnownColor((KnownColor)View_Resource_Out.text_color);
					}
					else
					{
						utcText.Color = SystemColors.Control;
					}
					
					if (View_Resource_Out.back_color < 0)
					{
						//utcBack.Color = utcBack.Color.FromArgb(View_Resource_Out.back_color);
                        utcText.Color = Color.FromArgb(View_Resource_Out.back_color);
					}
					else if (View_Resource_Out.back_color > 0)
					{
                        //utcBack.Color = utcBack.Color.FromKnownColor((KnownColor)View_Resource_Out.back_color);
                        utcBack.Color = Color.FromKnownColor((KnownColor)View_Resource_Out.back_color);
					}
					else
					{
						utcBack.Color = SystemColors.Control;
					}
					if (View_Resource_Out.img_idx != - 1 && imlRes.Images.Count > View_Resource_Out.img_idx)
					{
						iImageIndex = View_Resource_Out.img_idx;
						pctImage.Image = imlRes.Images[iImageIndex];
					}
					
					if (View_Resource_Out.signal_flag == 'Y')
					{
						chkSignalFlag.Checked = true;
					}
					else
					{
						chkSignalFlag.Checked = false;
					}
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.View_Resource()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
		private bool ViewUDRResourceList(Control control, string sStep)
		{
			
			try
			{
                FMB_View_UDR_Resource_List_In_Tag View_UDR_Resource_List_In = new FMB_View_UDR_Resource_List_In_Tag();
                FMB_View_UDR_Resource_List_Out_Tag View_UDR_Resource_List_Out = new FMB_View_UDR_Resource_List_Out_Tag();
				
				int i;
				ListViewItem itmX;
				
				if (control is ListView)
				{
                    CmnInitFunction.InitListView((ListView)control);
				}
				
				View_UDR_Resource_List_In.h_proc_step = '1';
				View_UDR_Resource_List_In.h_passport = GlobalVariable.gsPassport;
				View_UDR_Resource_List_In.h_language = GlobalVariable.gcLanguage;
				View_UDR_Resource_List_In.h_user_id = GlobalVariable.gsUserID;
				View_UDR_Resource_List_In.h_password = GlobalVariable.gsPassword;
				View_UDR_Resource_List_In.h_factory = GlobalVariable.gsFactory;
				View_UDR_Resource_List_In.next_res_id = "";
				View_UDR_Resource_List_In.group = cdvLayOut.Text;
				
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
							if (View_UDR_Resource_List_Out.udr_res_list[i].attached_flag == 'Y')
							{
							}
							else if (View_UDR_Resource_List_Out.udr_res_list[i].attached_flag == 'N')
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
								}
								((ListView) control).Items.Add(itmX);
							}
						}
					}
					
					View_UDR_Resource_List_In.next_res_id = View_UDR_Resource_List_Out.next_res_id;
                } while (string.IsNullOrEmpty(View_UDR_Resource_List_Out.next_res_id) == false);
				
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.ViewUDRResourceList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// View_UDR_Resource()
		//       - View User Define Resource Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep as String    : Proc Step
		//
		private bool View_UDR_Resource(char sStep)
		{
			
			try
			{
                FMB_View_UDR_Resource_In_Tag View_UDR_Resource_In = new FMB_View_UDR_Resource_In_Tag();
                FMB_View_UDR_Resource_Out_Tag View_UDR_Resource_Out = new FMB_View_UDR_Resource_Out_Tag();
				
				View_UDR_Resource_In.h_passport = GlobalVariable.gsPassport;
				View_UDR_Resource_In.h_language = GlobalVariable.gcLanguage;
				View_UDR_Resource_In.h_factory = GlobalVariable.gsFactory;
				View_UDR_Resource_In.h_user_id = GlobalVariable.gsUserID;
				View_UDR_Resource_In.h_password = GlobalVariable.gsPassword;
				View_UDR_Resource_In.h_proc_step = sStep;
				View_UDR_Resource_In.res_id = cdvResID.Text;
				View_UDR_Resource_In.group_id = cdvLayOut.Text;


                if (FMBSender.FMB_View_UDR_Resource(View_UDR_Resource_In, ref View_UDR_Resource_Out) == false)
                {
                    CmnFunction.ShowMsgBox(h101stub.StatusMessage);
                    return false;
                }
                
				if (View_UDR_Resource_Out.h_status_value != modGlobalConstant.MP_SUCCESS_STATUS)
				{
					CmnFunction.ShowMsgBox(CmnFunction.MakeErrorMsg(View_UDR_Resource_Out.h_msg_code, View_UDR_Resource_Out.h_msg, View_UDR_Resource_Out.h_db_err_msg, View_UDR_Resource_Out.h_field_msg), "FMB Client", MessageBoxButtons.OK, 1);
					return false;
				}
				
				if (sStep == modGlobalConstant.MP_STEP_CREATE)
				{
					txtDesc.Text = CmnFunction.RTrim(View_UDR_Resource_Out.res_desc);
					txtResourceType.Text = CmnFunction.RTrim(View_UDR_Resource_Out.res_type);
                    if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode) != "")
                    {
                        if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OL")
                        {
                            txtCtrlMode.Text = "ON LINE";
                        }
                        else if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OR")
                        {
                            txtCtrlMode.Text = "ON LINE REAL";
                        }
                        else if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OF")
                        {
                            txtCtrlMode.Text = "OFF LINE";
                        }
                    }
                    if (CmnFunction.Trim(View_UDR_Resource_Out.res_proc_mode) != "")
                    {
                        if (View_UDR_Resource_Out.res_proc_mode.Substring(0, 1) == "M")
                        {
                            txtProcMode.Text = "MANUAL";
                        }
                        else if (View_UDR_Resource_Out.res_proc_mode.Substring(0, 1) == "S")
                        {
                            txtProcMode.Text = "SEMI AUTO";
                        }
                        else if (View_UDR_Resource_Out.res_proc_mode.Substring(0, 1) == "F")
                        {
                            txtProcMode.Text = "FULL AUTO";
                        }
                    }
					txtArea.Text = CmnFunction.RTrim(View_UDR_Resource_Out.area_id);
					txtSubArea.Text = CmnFunction.RTrim(View_UDR_Resource_Out.sub_area_id);
					
					if (CmnFunction.RTrim(View_UDR_Resource_Out.res_up_down_flag) == "U")
					{
						txtUpDown.Text = "UP";
					}
					else if (CmnFunction.RTrim(View_UDR_Resource_Out.res_up_down_flag) == "D")
					{
						txtUpDown.Text = "DOWN";
					}
					txtPriSts.Text = CmnFunction.RTrim(View_UDR_Resource_Out.res_pri_sts);
					txtLastStart.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_start_time, DATE_TIME_FORMAT.NONE);
					txtLastEnd.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_end_time, DATE_TIME_FORMAT.NONE);
					txtLastEvent.Text = CmnFunction.RTrim(View_UDR_Resource_Out.last_event);
					txtLastEventTime.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_event_time, DATE_TIME_FORMAT.NONE);
					txtLastDown.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_down_time, DATE_TIME_FORMAT.NONE);
					
					txtText.Text = cdvResID.Text;
					
					if (View_UDR_Resource_Out.img_idx != - 1)
					{
						iImageIndex = View_UDR_Resource_Out.img_idx;
						pctImage.Image = imlRes.Images[iImageIndex];
					}
					
				}
				else
				{
					txtDesc.Text = CmnFunction.RTrim(View_UDR_Resource_Out.res_desc);
					txtResourceType.Text = CmnFunction.RTrim(View_UDR_Resource_Out.res_type);
					if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OL")
					{
						txtCtrlMode.Text = "ON LINE";
					}
					else if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OR")
					{
						txtCtrlMode.Text = "ON LINE REAL";
					}
					else if (CmnFunction.Trim(View_UDR_Resource_Out.res_ctrl_mode).PadLeft(2, ' ').Substring(0, 2) == "OF")
					{
						txtCtrlMode.Text = "OFF LINE";
					}
					if (View_UDR_Resource_Out.res_proc_mode.Substring(0, 1) == "M")
					{
						txtProcMode.Text = "MANUAL";
					}
					else if (View_UDR_Resource_Out.res_proc_mode.Substring(0, 1) == "S")
					{
						txtProcMode.Text = "SEMI AUTO";
					}
					else if (View_UDR_Resource_Out.res_proc_mode.Substring(0, 1) == "F")
					{
						txtProcMode.Text = "FULL AUTO";
					}
					txtArea.Text = CmnFunction.RTrim(View_UDR_Resource_Out.area_id);
					txtSubArea.Text = CmnFunction.RTrim(View_UDR_Resource_Out.sub_area_id);
					
					if (CmnFunction.RTrim(View_UDR_Resource_Out.res_up_down_flag) == "U")
					{
						txtUpDown.Text = "UP";
					}
					else if (CmnFunction.RTrim(View_UDR_Resource_Out.res_up_down_flag) == "D")
					{
						txtUpDown.Text = "DOWN";
					}
					txtPriSts.Text = CmnFunction.RTrim(View_UDR_Resource_Out.res_pri_sts);
					txtLastStart.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_start_time, DATE_TIME_FORMAT.NONE);
					txtLastEnd.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_end_time, DATE_TIME_FORMAT.NONE);
					txtLastEvent.Text = CmnFunction.RTrim(View_UDR_Resource_Out.last_event);
					txtLastEventTime.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_event_time, DATE_TIME_FORMAT.NONE);
					txtLastDown.Text = CmnFunction.MakeDateFormat(View_UDR_Resource_Out.last_down_time, DATE_TIME_FORMAT.NONE);
					
					cdvLayOut.Text = CmnFunction.RTrim(View_UDR_Resource_Out.group_id);
                    txtText.Text = CmnFunction.RTrim(View_UDR_Resource_Out.text);
					cboSize.Text = CmnFunction.Trim(View_UDR_Resource_Out.text_size);
                    cboTextStyle.SelectedIndex = CmnFunction.ToInt(View_UDR_Resource_Out.text_style.ToString());
					
					if (View_UDR_Resource_Out.text_color < 0)
					{
						utcText.Color = Color.FromArgb(View_UDR_Resource_Out.text_color);
					}
					else if (View_UDR_Resource_Out.text_color > 0)
					{
                        utcText.Color = Color.FromKnownColor((KnownColor)View_UDR_Resource_Out.text_color);
					}
					else
					{
						utcText.Color = SystemColors.Control;
					}
					
					if (View_UDR_Resource_Out.back_color < 0)
					{
						utcBack.Color = Color.FromArgb(View_UDR_Resource_Out.back_color);
					}
					else if (View_UDR_Resource_Out.back_color > 0)
					{
                        utcBack.Color = Color.FromKnownColor((KnownColor)View_UDR_Resource_Out.back_color);
					}
					else
					{
						utcBack.Color = SystemColors.Control;
					}
					if (View_UDR_Resource_Out.img_idx != - 1)
					{
						iImageIndex = View_UDR_Resource_Out.img_idx;
						pctImage.Image = imlRes.Images[iImageIndex];
					}
					
					if (View_UDR_Resource_Out.signal_flag == 'Y')
					{
						chkSignalFlag.Checked = true;
					}
					else
					{
						chkSignalFlag.Checked = false;
					}
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.View_UDR_Resource()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_UDR_ResLoc()
		//       - Create/Update/Delete User Define Resource Information
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep as String    : Proc Step
		//
		private object Update_UDR_ResLoc(char sStep)
		{
			
			try
			{
                FMB_Update_UDR_ResLoc_In_Tag Update_UDR_ResLoc_In = new FMB_Update_UDR_ResLoc_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();
				
				Update_UDR_ResLoc_In._C.h_factory = GlobalVariable.gsFactory;
				Update_UDR_ResLoc_In._C.h_language = GlobalVariable.gcLanguage;
				Update_UDR_ResLoc_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_UDR_ResLoc_In._C.h_proc_step = sStep;
				Update_UDR_ResLoc_In._C.h_passport = GlobalVariable.gsPassport;
				Update_UDR_ResLoc_In._C.h_password = GlobalVariable.gsPassword;
				Update_UDR_ResLoc_In._C.group = cdvLayOut.Text;
				
				Update_UDR_ResLoc_In._C.res_id = cdvResID.Text;
				Update_UDR_ResLoc_In._C.res_type = txtResTagFlag.Text[0];
				Update_UDR_ResLoc_In._C.text = txtText.Text;
				
				if (utcText.Color.IsSystemColor == true || utcText.Color.IsKnownColor == true)
				{
					Update_UDR_ResLoc_In._C.text_color = CmnFunction.ToInt(utcText.Color.ToKnownColor());
				}
				else
				{
					Update_UDR_ResLoc_In._C.text_color = utcText.Color.ToArgb();
				}
				
				Update_UDR_ResLoc_In._C.text_size = CmnFunction.ToInt(cboSize.Text);
                Update_UDR_ResLoc_In._C.text_style = Convert.ToChar(cboTextStyle.SelectedIndex + '0');
				
				if (utcBack.Color.IsSystemColor == true || utcBack.Color.IsKnownColor == true)
				{
					Update_UDR_ResLoc_In._C.back_color = CmnFunction.ToInt(utcBack.Color.ToKnownColor());
				}
				else
				{
					Update_UDR_ResLoc_In._C.back_color = utcBack.Color.ToArgb();
				}
				Update_UDR_ResLoc_In._C.loc_x = CmnFunction.ToInt(txtX.Text);
				Update_UDR_ResLoc_In._C.loc_y = CmnFunction.ToInt(txtY.Text);
				Update_UDR_ResLoc_In._C.loc_width = CmnFunction.ToInt(txtWidth.Text);
				Update_UDR_ResLoc_In._C.loc_height = CmnFunction.ToInt(txtHeight.Text);
				Update_UDR_ResLoc_In._C.tag_type = CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Resource);
				
				if (chkSignalFlag.Checked == true)
				{
					Update_UDR_ResLoc_In._C.signal_flag = 'Y';
				}
				else
				{
					Update_UDR_ResLoc_In._C.signal_flag = ' ';
				}
				
				if (sStep != modGlobalConstant.MP_STEP_DELETE)
				{
					if (View_UDR_Group() == true)
					{
						if (CmnFunction.ToInt(txtX.Text) + CmnFunction.ToInt(txtWidth.Text) > sWidth)
						{
							if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(20), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
							{
								Update_UDR_ResLoc_In._C.loc_x = sWidth - CmnFunction.ToInt(txtWidth.Text);
								txtX.Text = System.Convert.ToString(sWidth - CmnFunction.ToInt(txtWidth.Text));
							}
							else
							{
								Update_UDR_ResLoc_In._C.loc_x = CmnFunction.ToInt(txtX.Text);
							}
						}
						if (CmnFunction.ToInt(txtY.Text) + CmnFunction.ToInt(txtHeight.Text) > sHeight)
						{
							if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(21), "FMB Client", MessageBoxButtons.YesNo, 1) == DialogResult.Yes)
							{
								Update_UDR_ResLoc_In._C.loc_y = sHeight - CmnFunction.ToInt(txtHeight.Text);
								txtY.Text = System.Convert.ToString(sHeight - CmnFunction.ToInt(txtHeight.Text));
							}
							else
							{
								Update_UDR_ResLoc_In._C.loc_y = CmnFunction.ToInt(txtY.Text);
							}
						}
					}
					else
					{
						return false;
					}
				}

                if (FMBSender.FMB_Update_UDR_ResLoc(Update_UDR_ResLoc_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.Update_UDR_ResLoc()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
                FMB_View_LayOut_Out_Tag View_LayOut_Out = new FMB_View_LayOut_Out_Tag();
				
				View_LayOut_In.h_passport = GlobalVariable.gsPassport;
				View_LayOut_In.h_language = GlobalVariable.gcLanguage;
				View_LayOut_In.h_factory = txtFactory.Text;
				View_LayOut_In.h_user_id = GlobalVariable.gsUserID;
				View_LayOut_In.h_password = GlobalVariable.gsPassword;
				View_LayOut_In.h_proc_step = '1';
				View_LayOut_In.layout_id = cdvLayOut.Text;

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
				
				sWidth = View_LayOut_Out.width;
				sHeight = View_LayOut_Out.height;
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateLayOut.View_LayOut()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
				View_UDR_Group_In.group_id = cdvLayOut.Text;

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
				
				sWidth = View_UDR_Group_Out.width;
				sHeight = View_UDR_Group_Out.height;
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateUDRGroup.View_UDR_Group()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		#endregion
		
		#region " Event Implementations"
		
		private void btnClose_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Dispose(false);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvResID_ButtonPress(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				cdvResID.Init();
				cdvResID.Columns.Add("ResID", 100, HorizontalAlignment.Left);
				cdvResID.Columns.Add("Desc", 100, HorizontalAlignment.Left);
				cdvResID.SelectedSubItemIndex = 0;
				//cdvResID.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
                if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
				{
					ViewResourceList(cdvResID.GetListView, "1");
				}
                else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
				{
					ViewUDRResourceList(cdvResID.GetListView, "1");
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.cdvResID_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnCreate_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (ProcStep == modGlobalConstant.MP_STEP_CREATE)
				{
					if (CheckCondition("CREATE") == false)
					{
						this.DialogResult = DialogResult.None;
						return;
					}
					else
					{
                        if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
						{
							if (Update_Resource_Location(modGlobalConstant.MP_STEP_CREATE) == false)
							{
								this.DialogResult = DialogResult.None;
								return;
							}
							if (modInterface.gIMdiForm.RefreshDesignList("2", txtFactory.Text, cdvLayOut.Text, cdvResID.Text, "") == false)
							{
								return;
							}
						}
                        else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
						{
                            if ((bool)Update_UDR_ResLoc(modGlobalConstant.MP_STEP_CREATE) == false)
							{
								this.DialogResult = DialogResult.None;
								return;
							}
						}
						CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
					}
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_UPDATE)
				{
					if (CheckCondition("CREATE") == false)
					{
						this.DialogResult = DialogResult.None;
						return;
					}
					else
					{
                        if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
						{
							if (Update_Resource_Location(modGlobalConstant.MP_STEP_UPDATE) == false)
							{
								this.DialogResult = DialogResult.None;
								return;
							}
						}
                        else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
						{
                            if ((bool)Update_UDR_ResLoc(modGlobalConstant.MP_STEP_UPDATE) == false)
							{
								this.DialogResult = DialogResult.None;
								return;
							}
						}
						CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
					}
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_DELETE)
				{
					if (CheckCondition("DELETE") == false)
					{
						this.DialogResult = DialogResult.None;
						return;
					}
					else
					{
                        if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
						{
							if (Update_Resource_Location(modGlobalConstant.MP_STEP_DELETE) == false)
							{
								this.DialogResult = DialogResult.None;
								return;
							}
							if (modInterface.gIMdiForm.RefreshDesignList("3", txtFactory.Text, cdvLayOut.Text, cdvResID.Text, "") == false)
							{
								return;
							}
						}
                        else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
						{
                            if ((bool)Update_UDR_ResLoc(modGlobalConstant.MP_STEP_DELETE) == false)
							{
								this.DialogResult = DialogResult.None;
								return;
							}
						}
						CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(4), "FMB Client", MessageBoxButtons.OK, 1);
					}
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_NOTHING)
				{
					this.DialogResult = DialogResult.None;
					return;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.btnCreate_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvResID_SelectedItemChanged(System.Object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
		{
			
			try
			{
				if (ProcStep == modGlobalConstant.MP_STEP_CREATE)
				{
                    if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						View_Resource(modGlobalConstant.MP_STEP_CREATE);
					}
                    else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						View_UDR_Resource(modGlobalConstant.MP_STEP_CREATE);
					}
				}
				else
				{
                    if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						View_Resource(modGlobalConstant.MP_STEP_UPDATE);
					}
                    else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						View_UDR_Resource(modGlobalConstant.MP_STEP_UPDATE);
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.cdvResID_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBCreateResource_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				modCommonFunction.GetTextboxStyle(this.Controls);
				Set_ReadOnly();
				init();
				
				if (cdvResID.Text != "")
				{
					cdvResID.ReadOnly = true;
					cdvResID.BackColor = SystemColors.Control;
					cdvResID.VisibleButton = false;
					if (ProcStep == modGlobalConstant.MP_STEP_CREATE)
					{
                        if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
						{
							View_Resource(modGlobalConstant.MP_STEP_CREATE);
						}
						else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
						{
							View_UDR_Resource(modGlobalConstant.MP_STEP_CREATE);
						}
					}
					else
					{
						if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
						{
							View_Resource(modGlobalConstant.MP_STEP_UPDATE);
						}
						else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
						{
							View_UDR_Resource(modGlobalConstant.MP_STEP_UPDATE);
						}
					}
				}
				
				if (ProcStep == modGlobalConstant.MP_STEP_CREATE)
				{
					if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						this.Text = "Create Resource";
					}
					else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						this.Text = "Create Resource by User Define Group";
					}
					btnCreate.Text = "Create";
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_UPDATE)
				{
					if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						this.Text = "Update Resource";
					}
					else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						this.Text = "Update Resource by User Define Group";
					}
					btnCreate.Text = "Update";
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_DELETE)
				{
					if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						this.Text = "Delete Resource";
					}
					else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						this.Text = "Delete Resource by User Define Group";
					}
					btnCreate.Text = "Delete";
				}
				else if (ProcStep == modGlobalConstant.MP_STEP_VIEW)
				{
					if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						this.Text = "View Resource";
					}
					else if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						this.Text = "View Resource by User Define Group";
					}
					btnCreate.Text = "Create";
				}
				
				modLanguageFunction.ToClientLanguage(this);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.frmFMBCreateResource_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvLayOut_ButtonPress(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				cdvLayOut.Init();
				cdvLayOut.Columns.Add("LayoutID", 100, HorizontalAlignment.Left);
				cdvLayOut.Columns.Add("Desc", 100, HorizontalAlignment.Left);
				cdvLayOut.SelectedSubItemIndex = 0;
				//cdvLayOut.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
				if (System.Convert.ToString(this.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
				{
                    modListRoutine.ViewLayOutList(cdvLayOut.GetListView, '1', txtFactory.Text);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBCreateResource.cdvLayOut_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void txtX_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.txtX_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void txtY_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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
				CmnFunction.ShowMsgBox("frmFMBCreateResource.txtY_KeyPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
		
		#endregion
		
	}
	
}
