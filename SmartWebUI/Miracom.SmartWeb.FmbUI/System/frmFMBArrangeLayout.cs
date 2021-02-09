
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Infragistics.Win.UltraWinEditors;
using Miracom.UI.Controls;
//using Miracom.FMBClientCore;
using Miracom.SmartWeb.FWX;
//using Miracom.MESCore;
namespace Miracom.SmartWeb.UI
{
	public class frmFMBArrangeLayout : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBArrangeLayout()
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
		internal System.Windows.Forms.Panel pnlBottom;
		internal System.Windows.Forms.Button btnOK;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Panel pnlLeft;
		internal System.Windows.Forms.GroupBox grpLayout;
		internal System.Windows.Forms.PictureBox pbxCell_9;
		internal System.Windows.Forms.PictureBox pbxCell_6;
		internal System.Windows.Forms.PictureBox pbxCell_4;
		internal System.Windows.Forms.PictureBox pbxCell_2;
		internal System.Windows.Forms.RadioButton rbn6;
		internal System.Windows.Forms.RadioButton rbn4;
		internal System.Windows.Forms.RadioButton rbn9;
		internal System.Windows.Forms.RadioButton rbn2;
		internal System.Windows.Forms.Panel pnlFill;
		internal System.Windows.Forms.Panel pnlLayout1;
		internal System.Windows.Forms.GroupBox grpSelectLayout;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory1;
		internal System.Windows.Forms.Label lblFactory1;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout1;
		internal System.Windows.Forms.Label lblLayout1;
		internal System.Windows.Forms.Label lblCategory1;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory1;
		internal System.Windows.Forms.Label lblCategory2;
		internal System.Windows.Forms.Label lblFactory2;
		internal System.Windows.Forms.Label lblCategory3;
		internal System.Windows.Forms.Label lblFactory3;
		internal System.Windows.Forms.Label lblCategory4;
		internal System.Windows.Forms.Label lblFactory4;
		internal System.Windows.Forms.Label lblCategory5;
		internal System.Windows.Forms.Label lblFactory5;
		internal System.Windows.Forms.Label lblCategory6;
		internal System.Windows.Forms.Label lblFactory6;
		internal System.Windows.Forms.Label lblCategory7;
		internal System.Windows.Forms.Label lblFactory7;
		internal System.Windows.Forms.Label lblCategory8;
		internal System.Windows.Forms.Label lblFactory8;
		internal System.Windows.Forms.Label lblCategory9;
		internal System.Windows.Forms.Label lblFactory9;
		internal System.Windows.Forms.Panel pnlLayout2;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory2;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout2;
		internal System.Windows.Forms.Label lblLayout2;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory2;
		internal System.Windows.Forms.Panel pnlLayout3;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory3;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout3;
		internal System.Windows.Forms.Label lblLayout3;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory3;
		internal System.Windows.Forms.Panel pnlLayout4;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory4;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout4;
		internal System.Windows.Forms.Label lblLayout4;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory4;
		internal System.Windows.Forms.Panel pnlLayout5;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory5;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout5;
		internal System.Windows.Forms.Label lblLayout5;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory5;
		internal System.Windows.Forms.Panel pnlLayout6;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory6;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout6;
		internal System.Windows.Forms.Label lblLayout6;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory6;
		internal System.Windows.Forms.Panel pnlLayout7;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory7;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout7;
		internal System.Windows.Forms.Label lblLayout7;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory7;
		internal System.Windows.Forms.Panel pnlLayout8;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory8;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout8;
		internal System.Windows.Forms.Label lblLayout8;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory8;
		internal System.Windows.Forms.Panel pnlLayout9;
		internal Infragistics.Win.UltraWinEditors.UltraComboEditor cboCategory9;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout9;
		internal System.Windows.Forms.Label lblLayout9;
		public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory9;
		internal System.Windows.Forms.Button btnClear;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmFMBArrangeLayout));
			Infragistics.Win.ValueListItem ValueListItem1 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem2 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem3 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem4 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem5 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem6 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem7 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem8 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem9 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem10 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem11 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem12 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem13 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem14 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem15 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem16 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem17 = new Infragistics.Win.ValueListItem();
			Infragistics.Win.ValueListItem ValueListItem18 = new Infragistics.Win.ValueListItem();
			this.pnlBottom = new System.Windows.Forms.Panel();
			base.Load += new System.EventHandler(frmFMBArrangeLayout_Load);
			this.btnOK = new System.Windows.Forms.Button();
			this.btnOK.Click += new System.EventHandler(btnOK_Click);
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.pnlLeft = new System.Windows.Forms.Panel();
			this.grpLayout = new System.Windows.Forms.GroupBox();
			this.pbxCell_9 = new System.Windows.Forms.PictureBox();
			this.pbxCell_6 = new System.Windows.Forms.PictureBox();
			this.pbxCell_4 = new System.Windows.Forms.PictureBox();
			this.pbxCell_2 = new System.Windows.Forms.PictureBox();
			this.rbn6 = new System.Windows.Forms.RadioButton();
			this.rbn6.CheckedChanged += new System.EventHandler(rbnCount_CheckedChanged);
			this.rbn4 = new System.Windows.Forms.RadioButton();
			this.rbn4.CheckedChanged += new System.EventHandler(rbnCount_CheckedChanged);
			this.rbn9 = new System.Windows.Forms.RadioButton();
			this.rbn9.CheckedChanged += new System.EventHandler(rbnCount_CheckedChanged);
			this.rbn2 = new System.Windows.Forms.RadioButton();
			this.rbn2.CheckedChanged += new System.EventHandler(rbnCount_CheckedChanged);
			this.pnlFill = new System.Windows.Forms.Panel();
			this.grpSelectLayout = new System.Windows.Forms.GroupBox();
			this.pnlLayout9 = new System.Windows.Forms.Panel();
			this.cboCategory9 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory9.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory9 = new System.Windows.Forms.Label();
			this.cdvLayout9 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout9.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout9.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout9 = new System.Windows.Forms.Label();
			this.cdvFactory9 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory9.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory9.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory9 = new System.Windows.Forms.Label();
			this.pnlLayout8 = new System.Windows.Forms.Panel();
			this.cboCategory8 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory8.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory8 = new System.Windows.Forms.Label();
			this.cdvLayout8 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout8.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout8.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout8 = new System.Windows.Forms.Label();
			this.cdvFactory8 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory8.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory8.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory8 = new System.Windows.Forms.Label();
			this.pnlLayout7 = new System.Windows.Forms.Panel();
			this.cboCategory7 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory7.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory7 = new System.Windows.Forms.Label();
			this.cdvLayout7 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout7.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout7.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout7 = new System.Windows.Forms.Label();
			this.cdvFactory7 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory7.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory7.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory7 = new System.Windows.Forms.Label();
			this.pnlLayout6 = new System.Windows.Forms.Panel();
			this.cboCategory6 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory6.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory6 = new System.Windows.Forms.Label();
			this.cdvLayout6 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout6.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout6.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout6 = new System.Windows.Forms.Label();
			this.cdvFactory6 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory6.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory6.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory6 = new System.Windows.Forms.Label();
			this.pnlLayout5 = new System.Windows.Forms.Panel();
			this.cboCategory5 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory5.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory5 = new System.Windows.Forms.Label();
			this.cdvLayout5 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout5.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout5.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout5 = new System.Windows.Forms.Label();
			this.cdvFactory5 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory5.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory5.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory5 = new System.Windows.Forms.Label();
			this.pnlLayout4 = new System.Windows.Forms.Panel();
			this.cboCategory4 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory4.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory4 = new System.Windows.Forms.Label();
			this.cdvLayout4 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout4.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout4.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout4 = new System.Windows.Forms.Label();
			this.cdvFactory4 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory4.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory4.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory4 = new System.Windows.Forms.Label();
			this.pnlLayout3 = new System.Windows.Forms.Panel();
			this.cboCategory3 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory3.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory3 = new System.Windows.Forms.Label();
			this.cdvLayout3 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout3.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout3.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout3 = new System.Windows.Forms.Label();
			this.cdvFactory3 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory3.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory3.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory3 = new System.Windows.Forms.Label();
			this.pnlLayout2 = new System.Windows.Forms.Panel();
			this.cboCategory2 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory2.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory2 = new System.Windows.Forms.Label();
			this.cdvLayout2 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout2.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout2.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout2 = new System.Windows.Forms.Label();
			this.cdvFactory2 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory2.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory2.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory2 = new System.Windows.Forms.Label();
			this.pnlLayout1 = new System.Windows.Forms.Panel();
			this.cboCategory1 = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
			this.cboCategory1.SelectionChanged += new System.EventHandler(cboCategory_SelectionChanged);
			this.lblCategory1 = new System.Windows.Forms.Label();
			this.cdvLayout1 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvLayout1.ButtonPress += new System.EventHandler(cdvLayout_ButtonPress);
			this.cdvLayout1.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvLayout_SelectedItemChanged);
			this.lblLayout1 = new System.Windows.Forms.Label();
			this.cdvFactory1 = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory1.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory1.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.lblFactory1 = new System.Windows.Forms.Label();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnClear.Click += new System.EventHandler(btnClear_Click);
			this.pnlBottom.SuspendLayout();
			this.pnlLeft.SuspendLayout();
			this.grpLayout.SuspendLayout();
			this.pnlFill.SuspendLayout();
			this.grpSelectLayout.SuspendLayout();
			this.pnlLayout9.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory9).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout9).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory9).BeginInit();
			this.pnlLayout8.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory8).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout8).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory8).BeginInit();
			this.pnlLayout7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory7).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout7).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory7).BeginInit();
			this.pnlLayout6.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory6).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout6).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory6).BeginInit();
			this.pnlLayout5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory5).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout5).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory5).BeginInit();
			this.pnlLayout4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory4).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout4).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory4).BeginInit();
			this.pnlLayout3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory3).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout3).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory3).BeginInit();
			this.pnlLayout2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory2).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout2).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory2).BeginInit();
			this.pnlLayout1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cboCategory1).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout1).BeginInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory1).BeginInit();
			this.SuspendLayout();
			//
			//pnlBottom
			//
			this.pnlBottom.Controls.Add(this.btnClear);
			this.pnlBottom.Controls.Add(this.btnOK);
			this.pnlBottom.Controls.Add(this.btnClose);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.pnlBottom.Location = new System.Drawing.Point(0, 526);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(580, 40);
			this.pnlBottom.TabIndex = 3;
			//
			//btnOK
			//
			this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnOK.Location = new System.Drawing.Point(342, 9);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(74, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			//
			//btnClose
			//
			this.btnClose.Anchor = (System.Windows.Forms.AnchorStyles) (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnClose.Location = new System.Drawing.Point(496, 9);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(74, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			//
			//pnlLeft
			//
			this.pnlLeft.Controls.Add(this.grpLayout);
			this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnlLeft.DockPadding.Left = 3;
			this.pnlLeft.DockPadding.Right = 3;
			this.pnlLeft.DockPadding.Top = 3;
			this.pnlLeft.Location = new System.Drawing.Point(0, 0);
			this.pnlLeft.Name = "pnlLeft";
			this.pnlLeft.Size = new System.Drawing.Size(144, 526);
			this.pnlLeft.TabIndex = 5;
			//
			//grpLayout
			//
			this.grpLayout.Controls.Add(this.pbxCell_9);
			this.grpLayout.Controls.Add(this.pbxCell_6);
			this.grpLayout.Controls.Add(this.pbxCell_4);
			this.grpLayout.Controls.Add(this.pbxCell_2);
			this.grpLayout.Controls.Add(this.rbn6);
			this.grpLayout.Controls.Add(this.rbn4);
			this.grpLayout.Controls.Add(this.rbn9);
			this.grpLayout.Controls.Add(this.rbn2);
			this.grpLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpLayout.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpLayout.Location = new System.Drawing.Point(3, 3);
			this.grpLayout.Name = "grpLayout";
			this.grpLayout.Size = new System.Drawing.Size(138, 523);
			this.grpLayout.TabIndex = 2;
			this.grpLayout.TabStop = false;
			this.grpLayout.Text = "Select Layout Count";
			//
			//pbxCell_9
			//
			this.pbxCell_9.Image = (System.Drawing.Image) resources.GetObject("pbxCell_9.Image");
			this.pbxCell_9.Location = new System.Drawing.Point(52, 212);
			this.pbxCell_9.Name = "pbxCell_9";
			this.pbxCell_9.Size = new System.Drawing.Size(40, 40);
			this.pbxCell_9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbxCell_9.TabIndex = 7;
			this.pbxCell_9.TabStop = false;
			//
			//pbxCell_6
			//
			this.pbxCell_6.Image = (System.Drawing.Image) resources.GetObject("pbxCell_6.Image");
			this.pbxCell_6.Location = new System.Drawing.Point(52, 152);
			this.pbxCell_6.Name = "pbxCell_6";
			this.pbxCell_6.Size = new System.Drawing.Size(40, 40);
			this.pbxCell_6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbxCell_6.TabIndex = 6;
			this.pbxCell_6.TabStop = false;
			//
			//pbxCell_4
			//
			this.pbxCell_4.Image = (System.Drawing.Image) resources.GetObject("pbxCell_4.Image");
			this.pbxCell_4.Location = new System.Drawing.Point(52, 92);
			this.pbxCell_4.Name = "pbxCell_4";
			this.pbxCell_4.Size = new System.Drawing.Size(40, 40);
			this.pbxCell_4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbxCell_4.TabIndex = 5;
			this.pbxCell_4.TabStop = false;
			//
			//pbxCell_2
			//
			this.pbxCell_2.Image = (System.Drawing.Image) resources.GetObject("pbxCell_2.Image");
			this.pbxCell_2.Location = new System.Drawing.Point(52, 32);
			this.pbxCell_2.Name = "pbxCell_2";
			this.pbxCell_2.Size = new System.Drawing.Size(40, 40);
			this.pbxCell_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbxCell_2.TabIndex = 4;
			this.pbxCell_2.TabStop = false;
			//
			//rbn6
			//
			this.rbn6.Location = new System.Drawing.Point(24, 152);
			this.rbn6.Name = "rbn6";
			this.rbn6.Size = new System.Drawing.Size(20, 24);
			this.rbn6.TabIndex = 2;
			//
			//rbn4
			//
			this.rbn4.Location = new System.Drawing.Point(24, 92);
			this.rbn4.Name = "rbn4";
			this.rbn4.Size = new System.Drawing.Size(20, 24);
			this.rbn4.TabIndex = 1;
			//
			//rbn9
			//
			this.rbn9.Location = new System.Drawing.Point(24, 212);
			this.rbn9.Name = "rbn9";
			this.rbn9.Size = new System.Drawing.Size(20, 24);
			this.rbn9.TabIndex = 3;
			//
			//rbn2
			//
			this.rbn2.Location = new System.Drawing.Point(24, 32);
			this.rbn2.Name = "rbn2";
			this.rbn2.Size = new System.Drawing.Size(20, 24);
			this.rbn2.TabIndex = 0;
			//
			//pnlFill
			//
			this.pnlFill.Controls.Add(this.grpSelectLayout);
			this.pnlFill.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlFill.DockPadding.Left = 3;
			this.pnlFill.DockPadding.Right = 3;
			this.pnlFill.DockPadding.Top = 3;
			this.pnlFill.Location = new System.Drawing.Point(144, 0);
			this.pnlFill.Name = "pnlFill";
			this.pnlFill.Size = new System.Drawing.Size(436, 526);
			this.pnlFill.TabIndex = 6;
			//
			//grpSelectLayout
			//
			this.grpSelectLayout.Controls.Add(this.pnlLayout9);
			this.grpSelectLayout.Controls.Add(this.pnlLayout8);
			this.grpSelectLayout.Controls.Add(this.pnlLayout7);
			this.grpSelectLayout.Controls.Add(this.pnlLayout6);
			this.grpSelectLayout.Controls.Add(this.pnlLayout5);
			this.grpSelectLayout.Controls.Add(this.pnlLayout4);
			this.grpSelectLayout.Controls.Add(this.pnlLayout3);
			this.grpSelectLayout.Controls.Add(this.pnlLayout2);
			this.grpSelectLayout.Controls.Add(this.pnlLayout1);
			this.grpSelectLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpSelectLayout.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpSelectLayout.Location = new System.Drawing.Point(3, 3);
			this.grpSelectLayout.Name = "grpSelectLayout";
			this.grpSelectLayout.Size = new System.Drawing.Size(430, 523);
			this.grpSelectLayout.TabIndex = 0;
			this.grpSelectLayout.TabStop = false;
			this.grpSelectLayout.Text = "Select Layout";
			//
			//pnlLayout9
			//
			this.pnlLayout9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout9.Controls.Add(this.cboCategory9);
			this.pnlLayout9.Controls.Add(this.lblCategory9);
			this.pnlLayout9.Controls.Add(this.cdvLayout9);
			this.pnlLayout9.Controls.Add(this.lblLayout9);
			this.pnlLayout9.Controls.Add(this.cdvFactory9);
			this.pnlLayout9.Controls.Add(this.lblFactory9);
			this.pnlLayout9.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout9.Location = new System.Drawing.Point(3, 464);
			this.pnlLayout9.Name = "pnlLayout9";
			this.pnlLayout9.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout9.TabIndex = 8;
			//
			//cboCategory9
			//
			this.cboCategory9.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory9.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem1.DataValue = "ValueListItem0";
			ValueListItem1.DisplayText = "Layout";
			ValueListItem2.DataValue = "ValueListItem1";
			ValueListItem2.DisplayText = "User Group";
			this.cboCategory9.Items.Add(ValueListItem1);
			this.cboCategory9.Items.Add(ValueListItem2);
			this.cboCategory9.Location = new System.Drawing.Point(8, 32);
			this.cboCategory9.Name = "cboCategory9";
			this.cboCategory9.Size = new System.Drawing.Size(120, 19);
			this.cboCategory9.TabIndex = 56;
			//
			//lblCategory9
			//
			this.lblCategory9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory9.Location = new System.Drawing.Point(8, 10);
			this.lblCategory9.Name = "lblCategory9";
			this.lblCategory9.Size = new System.Drawing.Size(75, 14);
			this.lblCategory9.TabIndex = 55;
			this.lblCategory9.Text = "Category";
			//
			//cdvLayout9
			//
			this.cdvLayout9.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout9.BtnToolTipText = "";
			this.cdvLayout9.DescText = "";
			this.cdvLayout9.DisplaySubItemIndex = - 1;
			this.cdvLayout9.DisplayText = "";
			this.cdvLayout9.Focusing = null;
			this.cdvLayout9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout9.Index = 0;
			this.cdvLayout9.IsViewBtnImage = false;
			this.cdvLayout9.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout9.MaxLength = 10;
			this.cdvLayout9.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout9.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout9.Name = "cdvLayout9";
			this.cdvLayout9.ReadOnly = false;
			this.cdvLayout9.SearchSubItemIndex = 0;
			this.cdvLayout9.SelectedDescIndex = - 1;
			this.cdvLayout9.SelectedSubItemIndex = - 1;
			this.cdvLayout9.SelectionStart = 0;
			this.cdvLayout9.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout9.SmallImageList = null;
			this.cdvLayout9.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout9.TabIndex = 53;
			this.cdvLayout9.TextBoxToolTipText = "";
			this.cdvLayout9.TextBoxWidth = 168;
			this.cdvLayout9.VisibleButton = true;
			this.cdvLayout9.VisibleColumnHeader = false;
			this.cdvLayout9.VisibleDescription = false;
			//
			//lblLayout9
			//
			this.lblLayout9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout9.Location = new System.Drawing.Point(140, 34);
			this.lblLayout9.Name = "lblLayout9";
			this.lblLayout9.Size = new System.Drawing.Size(100, 14);
			this.lblLayout9.TabIndex = 54;
			this.lblLayout9.Text = "Layout";
			//
			//cdvFactory9
			//
			this.cdvFactory9.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory9.BtnToolTipText = "";
			this.cdvFactory9.DescText = "";
			this.cdvFactory9.DisplaySubItemIndex = - 1;
			this.cdvFactory9.DisplayText = "";
			this.cdvFactory9.Focusing = null;
			this.cdvFactory9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory9.Index = 0;
			this.cdvFactory9.IsViewBtnImage = false;
			this.cdvFactory9.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory9.MaxLength = 10;
			this.cdvFactory9.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory9.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory9.Name = "cdvFactory9";
			this.cdvFactory9.ReadOnly = false;
			this.cdvFactory9.SearchSubItemIndex = 0;
			this.cdvFactory9.SelectedDescIndex = - 1;
			this.cdvFactory9.SelectedSubItemIndex = - 1;
			this.cdvFactory9.SelectionStart = 0;
			this.cdvFactory9.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory9.SmallImageList = null;
			this.cdvFactory9.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory9.TabIndex = 51;
			this.cdvFactory9.TextBoxToolTipText = "";
			this.cdvFactory9.TextBoxWidth = 168;
			this.cdvFactory9.VisibleButton = true;
			this.cdvFactory9.VisibleColumnHeader = false;
			this.cdvFactory9.VisibleDescription = false;
			//
			//lblFactory9
			//
			this.lblFactory9.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory9.Location = new System.Drawing.Point(140, 10);
			this.lblFactory9.Name = "lblFactory9";
			this.lblFactory9.Size = new System.Drawing.Size(100, 14);
			this.lblFactory9.TabIndex = 52;
			this.lblFactory9.Text = "Factory";
			//
			//pnlLayout8
			//
			this.pnlLayout8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout8.Controls.Add(this.cboCategory8);
			this.pnlLayout8.Controls.Add(this.lblCategory8);
			this.pnlLayout8.Controls.Add(this.cdvLayout8);
			this.pnlLayout8.Controls.Add(this.lblLayout8);
			this.pnlLayout8.Controls.Add(this.cdvFactory8);
			this.pnlLayout8.Controls.Add(this.lblFactory8);
			this.pnlLayout8.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout8.Location = new System.Drawing.Point(3, 408);
			this.pnlLayout8.Name = "pnlLayout8";
			this.pnlLayout8.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout8.TabIndex = 7;
			//
			//cboCategory8
			//
			this.cboCategory8.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory8.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem3.DataValue = "ValueListItem0";
			ValueListItem3.DisplayText = "Layout";
			ValueListItem4.DataValue = "ValueListItem1";
			ValueListItem4.DisplayText = "User Group";
			this.cboCategory8.Items.Add(ValueListItem3);
			this.cboCategory8.Items.Add(ValueListItem4);
			this.cboCategory8.Location = new System.Drawing.Point(8, 32);
			this.cboCategory8.Name = "cboCategory8";
			this.cboCategory8.Size = new System.Drawing.Size(120, 19);
			this.cboCategory8.TabIndex = 56;
			//
			//lblCategory8
			//
			this.lblCategory8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory8.Location = new System.Drawing.Point(8, 10);
			this.lblCategory8.Name = "lblCategory8";
			this.lblCategory8.Size = new System.Drawing.Size(75, 14);
			this.lblCategory8.TabIndex = 55;
			this.lblCategory8.Text = "Category";
			//
			//cdvLayout8
			//
			this.cdvLayout8.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout8.BtnToolTipText = "";
			this.cdvLayout8.DescText = "";
			this.cdvLayout8.DisplaySubItemIndex = - 1;
			this.cdvLayout8.DisplayText = "";
			this.cdvLayout8.Focusing = null;
			this.cdvLayout8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout8.Index = 0;
			this.cdvLayout8.IsViewBtnImage = false;
			this.cdvLayout8.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout8.MaxLength = 10;
			this.cdvLayout8.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout8.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout8.Name = "cdvLayout8";
			this.cdvLayout8.ReadOnly = false;
			this.cdvLayout8.SearchSubItemIndex = 0;
			this.cdvLayout8.SelectedDescIndex = - 1;
			this.cdvLayout8.SelectedSubItemIndex = - 1;
			this.cdvLayout8.SelectionStart = 0;
			this.cdvLayout8.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout8.SmallImageList = null;
			this.cdvLayout8.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout8.TabIndex = 53;
			this.cdvLayout8.TextBoxToolTipText = "";
			this.cdvLayout8.TextBoxWidth = 168;
			this.cdvLayout8.VisibleButton = true;
			this.cdvLayout8.VisibleColumnHeader = false;
			this.cdvLayout8.VisibleDescription = false;
			//
			//lblLayout8
			//
			this.lblLayout8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout8.Location = new System.Drawing.Point(140, 34);
			this.lblLayout8.Name = "lblLayout8";
			this.lblLayout8.Size = new System.Drawing.Size(100, 14);
			this.lblLayout8.TabIndex = 54;
			this.lblLayout8.Text = "Layout";
			//
			//cdvFactory8
			//
			this.cdvFactory8.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory8.BtnToolTipText = "";
			this.cdvFactory8.DescText = "";
			this.cdvFactory8.DisplaySubItemIndex = - 1;
			this.cdvFactory8.DisplayText = "";
			this.cdvFactory8.Focusing = null;
			this.cdvFactory8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory8.Index = 0;
			this.cdvFactory8.IsViewBtnImage = false;
			this.cdvFactory8.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory8.MaxLength = 10;
			this.cdvFactory8.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory8.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory8.Name = "cdvFactory8";
			this.cdvFactory8.ReadOnly = false;
			this.cdvFactory8.SearchSubItemIndex = 0;
			this.cdvFactory8.SelectedDescIndex = - 1;
			this.cdvFactory8.SelectedSubItemIndex = - 1;
			this.cdvFactory8.SelectionStart = 0;
			this.cdvFactory8.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory8.SmallImageList = null;
			this.cdvFactory8.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory8.TabIndex = 51;
			this.cdvFactory8.TextBoxToolTipText = "";
			this.cdvFactory8.TextBoxWidth = 168;
			this.cdvFactory8.VisibleButton = true;
			this.cdvFactory8.VisibleColumnHeader = false;
			this.cdvFactory8.VisibleDescription = false;
			//
			//lblFactory8
			//
			this.lblFactory8.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory8.Location = new System.Drawing.Point(140, 10);
			this.lblFactory8.Name = "lblFactory8";
			this.lblFactory8.Size = new System.Drawing.Size(100, 14);
			this.lblFactory8.TabIndex = 52;
			this.lblFactory8.Text = "Factory";
			//
			//pnlLayout7
			//
			this.pnlLayout7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout7.Controls.Add(this.cboCategory7);
			this.pnlLayout7.Controls.Add(this.lblCategory7);
			this.pnlLayout7.Controls.Add(this.cdvLayout7);
			this.pnlLayout7.Controls.Add(this.lblLayout7);
			this.pnlLayout7.Controls.Add(this.cdvFactory7);
			this.pnlLayout7.Controls.Add(this.lblFactory7);
			this.pnlLayout7.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout7.Location = new System.Drawing.Point(3, 352);
			this.pnlLayout7.Name = "pnlLayout7";
			this.pnlLayout7.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout7.TabIndex = 6;
			//
			//cboCategory7
			//
			this.cboCategory7.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory7.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem5.DataValue = "ValueListItem0";
			ValueListItem5.DisplayText = "Layout";
			ValueListItem6.DataValue = "ValueListItem1";
			ValueListItem6.DisplayText = "User Group";
			this.cboCategory7.Items.Add(ValueListItem5);
			this.cboCategory7.Items.Add(ValueListItem6);
			this.cboCategory7.Location = new System.Drawing.Point(8, 32);
			this.cboCategory7.Name = "cboCategory7";
			this.cboCategory7.Size = new System.Drawing.Size(120, 19);
			this.cboCategory7.TabIndex = 56;
			//
			//lblCategory7
			//
			this.lblCategory7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory7.Location = new System.Drawing.Point(8, 10);
			this.lblCategory7.Name = "lblCategory7";
			this.lblCategory7.Size = new System.Drawing.Size(75, 14);
			this.lblCategory7.TabIndex = 55;
			this.lblCategory7.Text = "Category";
			//
			//cdvLayout7
			//
			this.cdvLayout7.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout7.BtnToolTipText = "";
			this.cdvLayout7.DescText = "";
			this.cdvLayout7.DisplaySubItemIndex = - 1;
			this.cdvLayout7.DisplayText = "";
			this.cdvLayout7.Focusing = null;
			this.cdvLayout7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout7.Index = 0;
			this.cdvLayout7.IsViewBtnImage = false;
			this.cdvLayout7.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout7.MaxLength = 10;
			this.cdvLayout7.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout7.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout7.Name = "cdvLayout7";
			this.cdvLayout7.ReadOnly = false;
			this.cdvLayout7.SearchSubItemIndex = 0;
			this.cdvLayout7.SelectedDescIndex = - 1;
			this.cdvLayout7.SelectedSubItemIndex = - 1;
			this.cdvLayout7.SelectionStart = 0;
			this.cdvLayout7.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout7.SmallImageList = null;
			this.cdvLayout7.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout7.TabIndex = 53;
			this.cdvLayout7.TextBoxToolTipText = "";
			this.cdvLayout7.TextBoxWidth = 168;
			this.cdvLayout7.VisibleButton = true;
			this.cdvLayout7.VisibleColumnHeader = false;
			this.cdvLayout7.VisibleDescription = false;
			//
			//lblLayout7
			//
			this.lblLayout7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout7.Location = new System.Drawing.Point(140, 34);
			this.lblLayout7.Name = "lblLayout7";
			this.lblLayout7.Size = new System.Drawing.Size(100, 14);
			this.lblLayout7.TabIndex = 54;
			this.lblLayout7.Text = "Layout";
			//
			//cdvFactory7
			//
			this.cdvFactory7.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory7.BtnToolTipText = "";
			this.cdvFactory7.DescText = "";
			this.cdvFactory7.DisplaySubItemIndex = - 1;
			this.cdvFactory7.DisplayText = "";
			this.cdvFactory7.Focusing = null;
			this.cdvFactory7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory7.Index = 0;
			this.cdvFactory7.IsViewBtnImage = false;
			this.cdvFactory7.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory7.MaxLength = 10;
			this.cdvFactory7.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory7.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory7.Name = "cdvFactory7";
			this.cdvFactory7.ReadOnly = false;
			this.cdvFactory7.SearchSubItemIndex = 0;
			this.cdvFactory7.SelectedDescIndex = - 1;
			this.cdvFactory7.SelectedSubItemIndex = - 1;
			this.cdvFactory7.SelectionStart = 0;
			this.cdvFactory7.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory7.SmallImageList = null;
			this.cdvFactory7.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory7.TabIndex = 51;
			this.cdvFactory7.TextBoxToolTipText = "";
			this.cdvFactory7.TextBoxWidth = 168;
			this.cdvFactory7.VisibleButton = true;
			this.cdvFactory7.VisibleColumnHeader = false;
			this.cdvFactory7.VisibleDescription = false;
			//
			//lblFactory7
			//
			this.lblFactory7.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory7.Location = new System.Drawing.Point(140, 10);
			this.lblFactory7.Name = "lblFactory7";
			this.lblFactory7.Size = new System.Drawing.Size(100, 14);
			this.lblFactory7.TabIndex = 52;
			this.lblFactory7.Text = "Factory";
			//
			//pnlLayout6
			//
			this.pnlLayout6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout6.Controls.Add(this.cboCategory6);
			this.pnlLayout6.Controls.Add(this.lblCategory6);
			this.pnlLayout6.Controls.Add(this.cdvLayout6);
			this.pnlLayout6.Controls.Add(this.lblLayout6);
			this.pnlLayout6.Controls.Add(this.cdvFactory6);
			this.pnlLayout6.Controls.Add(this.lblFactory6);
			this.pnlLayout6.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout6.Location = new System.Drawing.Point(3, 296);
			this.pnlLayout6.Name = "pnlLayout6";
			this.pnlLayout6.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout6.TabIndex = 5;
			//
			//cboCategory6
			//
			this.cboCategory6.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory6.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem7.DataValue = "ValueListItem0";
			ValueListItem7.DisplayText = "Layout";
			ValueListItem8.DataValue = "ValueListItem1";
			ValueListItem8.DisplayText = "User Group";
			this.cboCategory6.Items.Add(ValueListItem7);
			this.cboCategory6.Items.Add(ValueListItem8);
			this.cboCategory6.Location = new System.Drawing.Point(8, 32);
			this.cboCategory6.Name = "cboCategory6";
			this.cboCategory6.Size = new System.Drawing.Size(120, 19);
			this.cboCategory6.TabIndex = 56;
			//
			//lblCategory6
			//
			this.lblCategory6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory6.Location = new System.Drawing.Point(8, 10);
			this.lblCategory6.Name = "lblCategory6";
			this.lblCategory6.Size = new System.Drawing.Size(75, 14);
			this.lblCategory6.TabIndex = 55;
			this.lblCategory6.Text = "Category";
			//
			//cdvLayout6
			//
			this.cdvLayout6.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout6.BtnToolTipText = "";
			this.cdvLayout6.DescText = "";
			this.cdvLayout6.DisplaySubItemIndex = - 1;
			this.cdvLayout6.DisplayText = "";
			this.cdvLayout6.Focusing = null;
			this.cdvLayout6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout6.Index = 0;
			this.cdvLayout6.IsViewBtnImage = false;
			this.cdvLayout6.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout6.MaxLength = 10;
			this.cdvLayout6.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout6.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout6.Name = "cdvLayout6";
			this.cdvLayout6.ReadOnly = false;
			this.cdvLayout6.SearchSubItemIndex = 0;
			this.cdvLayout6.SelectedDescIndex = - 1;
			this.cdvLayout6.SelectedSubItemIndex = - 1;
			this.cdvLayout6.SelectionStart = 0;
			this.cdvLayout6.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout6.SmallImageList = null;
			this.cdvLayout6.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout6.TabIndex = 53;
			this.cdvLayout6.TextBoxToolTipText = "";
			this.cdvLayout6.TextBoxWidth = 168;
			this.cdvLayout6.VisibleButton = true;
			this.cdvLayout6.VisibleColumnHeader = false;
			this.cdvLayout6.VisibleDescription = false;
			//
			//lblLayout6
			//
			this.lblLayout6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout6.Location = new System.Drawing.Point(140, 34);
			this.lblLayout6.Name = "lblLayout6";
			this.lblLayout6.Size = new System.Drawing.Size(100, 14);
			this.lblLayout6.TabIndex = 54;
			this.lblLayout6.Text = "Layout";
			//
			//cdvFactory6
			//
			this.cdvFactory6.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory6.BtnToolTipText = "";
			this.cdvFactory6.DescText = "";
			this.cdvFactory6.DisplaySubItemIndex = - 1;
			this.cdvFactory6.DisplayText = "";
			this.cdvFactory6.Focusing = null;
			this.cdvFactory6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory6.Index = 0;
			this.cdvFactory6.IsViewBtnImage = false;
			this.cdvFactory6.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory6.MaxLength = 10;
			this.cdvFactory6.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory6.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory6.Name = "cdvFactory6";
			this.cdvFactory6.ReadOnly = false;
			this.cdvFactory6.SearchSubItemIndex = 0;
			this.cdvFactory6.SelectedDescIndex = - 1;
			this.cdvFactory6.SelectedSubItemIndex = - 1;
			this.cdvFactory6.SelectionStart = 0;
			this.cdvFactory6.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory6.SmallImageList = null;
			this.cdvFactory6.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory6.TabIndex = 51;
			this.cdvFactory6.TextBoxToolTipText = "";
			this.cdvFactory6.TextBoxWidth = 168;
			this.cdvFactory6.VisibleButton = true;
			this.cdvFactory6.VisibleColumnHeader = false;
			this.cdvFactory6.VisibleDescription = false;
			//
			//lblFactory6
			//
			this.lblFactory6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory6.Location = new System.Drawing.Point(140, 10);
			this.lblFactory6.Name = "lblFactory6";
			this.lblFactory6.Size = new System.Drawing.Size(100, 14);
			this.lblFactory6.TabIndex = 52;
			this.lblFactory6.Text = "Factory";
			//
			//pnlLayout5
			//
			this.pnlLayout5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout5.Controls.Add(this.cboCategory5);
			this.pnlLayout5.Controls.Add(this.lblCategory5);
			this.pnlLayout5.Controls.Add(this.cdvLayout5);
			this.pnlLayout5.Controls.Add(this.lblLayout5);
			this.pnlLayout5.Controls.Add(this.cdvFactory5);
			this.pnlLayout5.Controls.Add(this.lblFactory5);
			this.pnlLayout5.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout5.Location = new System.Drawing.Point(3, 240);
			this.pnlLayout5.Name = "pnlLayout5";
			this.pnlLayout5.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout5.TabIndex = 4;
			//
			//cboCategory5
			//
			this.cboCategory5.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory5.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem9.DataValue = "ValueListItem0";
			ValueListItem9.DisplayText = "Layout";
			ValueListItem10.DataValue = "ValueListItem1";
			ValueListItem10.DisplayText = "User Group";
			this.cboCategory5.Items.Add(ValueListItem9);
			this.cboCategory5.Items.Add(ValueListItem10);
			this.cboCategory5.Location = new System.Drawing.Point(8, 32);
			this.cboCategory5.Name = "cboCategory5";
			this.cboCategory5.Size = new System.Drawing.Size(120, 19);
			this.cboCategory5.TabIndex = 56;
			//
			//lblCategory5
			//
			this.lblCategory5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory5.Location = new System.Drawing.Point(8, 10);
			this.lblCategory5.Name = "lblCategory5";
			this.lblCategory5.Size = new System.Drawing.Size(75, 14);
			this.lblCategory5.TabIndex = 55;
			this.lblCategory5.Text = "Category";
			//
			//cdvLayout5
			//
			this.cdvLayout5.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout5.BtnToolTipText = "";
			this.cdvLayout5.DescText = "";
			this.cdvLayout5.DisplaySubItemIndex = - 1;
			this.cdvLayout5.DisplayText = "";
			this.cdvLayout5.Focusing = null;
			this.cdvLayout5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout5.Index = 0;
			this.cdvLayout5.IsViewBtnImage = false;
			this.cdvLayout5.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout5.MaxLength = 10;
			this.cdvLayout5.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout5.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout5.Name = "cdvLayout5";
			this.cdvLayout5.ReadOnly = false;
			this.cdvLayout5.SearchSubItemIndex = 0;
			this.cdvLayout5.SelectedDescIndex = - 1;
			this.cdvLayout5.SelectedSubItemIndex = - 1;
			this.cdvLayout5.SelectionStart = 0;
			this.cdvLayout5.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout5.SmallImageList = null;
			this.cdvLayout5.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout5.TabIndex = 53;
			this.cdvLayout5.TextBoxToolTipText = "";
			this.cdvLayout5.TextBoxWidth = 168;
			this.cdvLayout5.VisibleButton = true;
			this.cdvLayout5.VisibleColumnHeader = false;
			this.cdvLayout5.VisibleDescription = false;
			//
			//lblLayout5
			//
			this.lblLayout5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout5.Location = new System.Drawing.Point(140, 34);
			this.lblLayout5.Name = "lblLayout5";
			this.lblLayout5.Size = new System.Drawing.Size(100, 14);
			this.lblLayout5.TabIndex = 54;
			this.lblLayout5.Text = "Layout";
			//
			//cdvFactory5
			//
			this.cdvFactory5.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory5.BtnToolTipText = "";
			this.cdvFactory5.DescText = "";
			this.cdvFactory5.DisplaySubItemIndex = - 1;
			this.cdvFactory5.DisplayText = "";
			this.cdvFactory5.Focusing = null;
			this.cdvFactory5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory5.Index = 0;
			this.cdvFactory5.IsViewBtnImage = false;
			this.cdvFactory5.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory5.MaxLength = 10;
			this.cdvFactory5.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory5.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory5.Name = "cdvFactory5";
			this.cdvFactory5.ReadOnly = false;
			this.cdvFactory5.SearchSubItemIndex = 0;
			this.cdvFactory5.SelectedDescIndex = - 1;
			this.cdvFactory5.SelectedSubItemIndex = - 1;
			this.cdvFactory5.SelectionStart = 0;
			this.cdvFactory5.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory5.SmallImageList = null;
			this.cdvFactory5.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory5.TabIndex = 51;
			this.cdvFactory5.TextBoxToolTipText = "";
			this.cdvFactory5.TextBoxWidth = 168;
			this.cdvFactory5.VisibleButton = true;
			this.cdvFactory5.VisibleColumnHeader = false;
			this.cdvFactory5.VisibleDescription = false;
			//
			//lblFactory5
			//
			this.lblFactory5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory5.Location = new System.Drawing.Point(140, 10);
			this.lblFactory5.Name = "lblFactory5";
			this.lblFactory5.Size = new System.Drawing.Size(100, 14);
			this.lblFactory5.TabIndex = 52;
			this.lblFactory5.Text = "Factory";
			//
			//pnlLayout4
			//
			this.pnlLayout4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout4.Controls.Add(this.cboCategory4);
			this.pnlLayout4.Controls.Add(this.lblCategory4);
			this.pnlLayout4.Controls.Add(this.cdvLayout4);
			this.pnlLayout4.Controls.Add(this.lblLayout4);
			this.pnlLayout4.Controls.Add(this.cdvFactory4);
			this.pnlLayout4.Controls.Add(this.lblFactory4);
			this.pnlLayout4.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout4.Location = new System.Drawing.Point(3, 184);
			this.pnlLayout4.Name = "pnlLayout4";
			this.pnlLayout4.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout4.TabIndex = 3;
			//
			//cboCategory4
			//
			this.cboCategory4.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory4.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem11.DataValue = "ValueListItem0";
			ValueListItem11.DisplayText = "Layout";
			ValueListItem12.DataValue = "ValueListItem1";
			ValueListItem12.DisplayText = "User Group";
			this.cboCategory4.Items.Add(ValueListItem11);
			this.cboCategory4.Items.Add(ValueListItem12);
			this.cboCategory4.Location = new System.Drawing.Point(8, 32);
			this.cboCategory4.Name = "cboCategory4";
			this.cboCategory4.Size = new System.Drawing.Size(120, 19);
			this.cboCategory4.TabIndex = 56;
			//
			//lblCategory4
			//
			this.lblCategory4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory4.Location = new System.Drawing.Point(8, 10);
			this.lblCategory4.Name = "lblCategory4";
			this.lblCategory4.Size = new System.Drawing.Size(75, 14);
			this.lblCategory4.TabIndex = 55;
			this.lblCategory4.Text = "Category";
			//
			//cdvLayout4
			//
			this.cdvLayout4.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout4.BtnToolTipText = "";
			this.cdvLayout4.DescText = "";
			this.cdvLayout4.DisplaySubItemIndex = - 1;
			this.cdvLayout4.DisplayText = "";
			this.cdvLayout4.Focusing = null;
			this.cdvLayout4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout4.Index = 0;
			this.cdvLayout4.IsViewBtnImage = false;
			this.cdvLayout4.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout4.MaxLength = 10;
			this.cdvLayout4.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout4.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout4.Name = "cdvLayout4";
			this.cdvLayout4.ReadOnly = false;
			this.cdvLayout4.SearchSubItemIndex = 0;
			this.cdvLayout4.SelectedDescIndex = - 1;
			this.cdvLayout4.SelectedSubItemIndex = - 1;
			this.cdvLayout4.SelectionStart = 0;
			this.cdvLayout4.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout4.SmallImageList = null;
			this.cdvLayout4.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout4.TabIndex = 53;
			this.cdvLayout4.TextBoxToolTipText = "";
			this.cdvLayout4.TextBoxWidth = 168;
			this.cdvLayout4.VisibleButton = true;
			this.cdvLayout4.VisibleColumnHeader = false;
			this.cdvLayout4.VisibleDescription = false;
			//
			//lblLayout4
			//
			this.lblLayout4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout4.Location = new System.Drawing.Point(140, 34);
			this.lblLayout4.Name = "lblLayout4";
			this.lblLayout4.Size = new System.Drawing.Size(100, 14);
			this.lblLayout4.TabIndex = 54;
			this.lblLayout4.Text = "Layout";
			//
			//cdvFactory4
			//
			this.cdvFactory4.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory4.BtnToolTipText = "";
			this.cdvFactory4.DescText = "";
			this.cdvFactory4.DisplaySubItemIndex = - 1;
			this.cdvFactory4.DisplayText = "";
			this.cdvFactory4.Focusing = null;
			this.cdvFactory4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory4.Index = 0;
			this.cdvFactory4.IsViewBtnImage = false;
			this.cdvFactory4.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory4.MaxLength = 10;
			this.cdvFactory4.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory4.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory4.Name = "cdvFactory4";
			this.cdvFactory4.ReadOnly = false;
			this.cdvFactory4.SearchSubItemIndex = 0;
			this.cdvFactory4.SelectedDescIndex = - 1;
			this.cdvFactory4.SelectedSubItemIndex = - 1;
			this.cdvFactory4.SelectionStart = 0;
			this.cdvFactory4.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory4.SmallImageList = null;
			this.cdvFactory4.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory4.TabIndex = 51;
			this.cdvFactory4.TextBoxToolTipText = "";
			this.cdvFactory4.TextBoxWidth = 168;
			this.cdvFactory4.VisibleButton = true;
			this.cdvFactory4.VisibleColumnHeader = false;
			this.cdvFactory4.VisibleDescription = false;
			//
			//lblFactory4
			//
			this.lblFactory4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory4.Location = new System.Drawing.Point(140, 10);
			this.lblFactory4.Name = "lblFactory4";
			this.lblFactory4.Size = new System.Drawing.Size(100, 14);
			this.lblFactory4.TabIndex = 52;
			this.lblFactory4.Text = "Factory";
			//
			//pnlLayout3
			//
			this.pnlLayout3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout3.Controls.Add(this.cboCategory3);
			this.pnlLayout3.Controls.Add(this.lblCategory3);
			this.pnlLayout3.Controls.Add(this.cdvLayout3);
			this.pnlLayout3.Controls.Add(this.lblLayout3);
			this.pnlLayout3.Controls.Add(this.cdvFactory3);
			this.pnlLayout3.Controls.Add(this.lblFactory3);
			this.pnlLayout3.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout3.Location = new System.Drawing.Point(3, 128);
			this.pnlLayout3.Name = "pnlLayout3";
			this.pnlLayout3.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout3.TabIndex = 2;
			//
			//cboCategory3
			//
			this.cboCategory3.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory3.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem13.DataValue = "ValueListItem0";
			ValueListItem13.DisplayText = "Layout";
			ValueListItem14.DataValue = "ValueListItem1";
			ValueListItem14.DisplayText = "User Group";
			this.cboCategory3.Items.Add(ValueListItem13);
			this.cboCategory3.Items.Add(ValueListItem14);
			this.cboCategory3.Location = new System.Drawing.Point(8, 32);
			this.cboCategory3.Name = "cboCategory3";
			this.cboCategory3.Size = new System.Drawing.Size(120, 19);
			this.cboCategory3.TabIndex = 56;
			//
			//lblCategory3
			//
			this.lblCategory3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory3.Location = new System.Drawing.Point(8, 10);
			this.lblCategory3.Name = "lblCategory3";
			this.lblCategory3.Size = new System.Drawing.Size(75, 14);
			this.lblCategory3.TabIndex = 55;
			this.lblCategory3.Text = "Category";
			//
			//cdvLayout3
			//
			this.cdvLayout3.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout3.BtnToolTipText = "";
			this.cdvLayout3.DescText = "";
			this.cdvLayout3.DisplaySubItemIndex = - 1;
			this.cdvLayout3.DisplayText = "";
			this.cdvLayout3.Focusing = null;
			this.cdvLayout3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout3.Index = 0;
			this.cdvLayout3.IsViewBtnImage = false;
			this.cdvLayout3.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout3.MaxLength = 10;
			this.cdvLayout3.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout3.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout3.Name = "cdvLayout3";
			this.cdvLayout3.ReadOnly = false;
			this.cdvLayout3.SearchSubItemIndex = 0;
			this.cdvLayout3.SelectedDescIndex = - 1;
			this.cdvLayout3.SelectedSubItemIndex = - 1;
			this.cdvLayout3.SelectionStart = 0;
			this.cdvLayout3.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout3.SmallImageList = null;
			this.cdvLayout3.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout3.TabIndex = 53;
			this.cdvLayout3.TextBoxToolTipText = "";
			this.cdvLayout3.TextBoxWidth = 168;
			this.cdvLayout3.VisibleButton = true;
			this.cdvLayout3.VisibleColumnHeader = false;
			this.cdvLayout3.VisibleDescription = false;
			//
			//lblLayout3
			//
			this.lblLayout3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout3.Location = new System.Drawing.Point(140, 34);
			this.lblLayout3.Name = "lblLayout3";
			this.lblLayout3.Size = new System.Drawing.Size(100, 14);
			this.lblLayout3.TabIndex = 54;
			this.lblLayout3.Text = "Layout";
			//
			//cdvFactory3
			//
			this.cdvFactory3.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory3.BtnToolTipText = "";
			this.cdvFactory3.DescText = "";
			this.cdvFactory3.DisplaySubItemIndex = - 1;
			this.cdvFactory3.DisplayText = "";
			this.cdvFactory3.Focusing = null;
			this.cdvFactory3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory3.Index = 0;
			this.cdvFactory3.IsViewBtnImage = false;
			this.cdvFactory3.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory3.MaxLength = 10;
			this.cdvFactory3.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory3.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory3.Name = "cdvFactory3";
			this.cdvFactory3.ReadOnly = false;
			this.cdvFactory3.SearchSubItemIndex = 0;
			this.cdvFactory3.SelectedDescIndex = - 1;
			this.cdvFactory3.SelectedSubItemIndex = - 1;
			this.cdvFactory3.SelectionStart = 0;
			this.cdvFactory3.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory3.SmallImageList = null;
			this.cdvFactory3.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory3.TabIndex = 51;
			this.cdvFactory3.TextBoxToolTipText = "";
			this.cdvFactory3.TextBoxWidth = 168;
			this.cdvFactory3.VisibleButton = true;
			this.cdvFactory3.VisibleColumnHeader = false;
			this.cdvFactory3.VisibleDescription = false;
			//
			//lblFactory3
			//
			this.lblFactory3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory3.Location = new System.Drawing.Point(140, 10);
			this.lblFactory3.Name = "lblFactory3";
			this.lblFactory3.Size = new System.Drawing.Size(100, 14);
			this.lblFactory3.TabIndex = 52;
			this.lblFactory3.Text = "Factory";
			//
			//pnlLayout2
			//
			this.pnlLayout2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout2.Controls.Add(this.cboCategory2);
			this.pnlLayout2.Controls.Add(this.lblCategory2);
			this.pnlLayout2.Controls.Add(this.cdvLayout2);
			this.pnlLayout2.Controls.Add(this.lblLayout2);
			this.pnlLayout2.Controls.Add(this.cdvFactory2);
			this.pnlLayout2.Controls.Add(this.lblFactory2);
			this.pnlLayout2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout2.Location = new System.Drawing.Point(3, 72);
			this.pnlLayout2.Name = "pnlLayout2";
			this.pnlLayout2.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout2.TabIndex = 1;
			//
			//cboCategory2
			//
			this.cboCategory2.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem15.DataValue = "ValueListItem0";
			ValueListItem15.DisplayText = "Layout";
			ValueListItem16.DataValue = "ValueListItem1";
			ValueListItem16.DisplayText = "User Group";
			this.cboCategory2.Items.Add(ValueListItem15);
			this.cboCategory2.Items.Add(ValueListItem16);
			this.cboCategory2.Location = new System.Drawing.Point(8, 32);
			this.cboCategory2.Name = "cboCategory2";
			this.cboCategory2.Size = new System.Drawing.Size(120, 19);
			this.cboCategory2.TabIndex = 56;
			//
			//lblCategory2
			//
			this.lblCategory2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory2.Location = new System.Drawing.Point(8, 10);
			this.lblCategory2.Name = "lblCategory2";
			this.lblCategory2.Size = new System.Drawing.Size(75, 14);
			this.lblCategory2.TabIndex = 55;
			this.lblCategory2.Text = "Category";
			//
			//cdvLayout2
			//
			this.cdvLayout2.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout2.BtnToolTipText = "";
			this.cdvLayout2.DescText = "";
			this.cdvLayout2.DisplaySubItemIndex = - 1;
			this.cdvLayout2.DisplayText = "";
			this.cdvLayout2.Focusing = null;
			this.cdvLayout2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout2.Index = 0;
			this.cdvLayout2.IsViewBtnImage = false;
			this.cdvLayout2.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout2.MaxLength = 10;
			this.cdvLayout2.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout2.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout2.Name = "cdvLayout2";
			this.cdvLayout2.ReadOnly = false;
			this.cdvLayout2.SearchSubItemIndex = 0;
			this.cdvLayout2.SelectedDescIndex = - 1;
			this.cdvLayout2.SelectedSubItemIndex = - 1;
			this.cdvLayout2.SelectionStart = 0;
			this.cdvLayout2.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout2.SmallImageList = null;
			this.cdvLayout2.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout2.TabIndex = 53;
			this.cdvLayout2.TextBoxToolTipText = "";
			this.cdvLayout2.TextBoxWidth = 168;
			this.cdvLayout2.VisibleButton = true;
			this.cdvLayout2.VisibleColumnHeader = false;
			this.cdvLayout2.VisibleDescription = false;
			//
			//lblLayout2
			//
			this.lblLayout2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout2.Location = new System.Drawing.Point(140, 34);
			this.lblLayout2.Name = "lblLayout2";
			this.lblLayout2.Size = new System.Drawing.Size(100, 14);
			this.lblLayout2.TabIndex = 54;
			this.lblLayout2.Text = "Layout";
			//
			//cdvFactory2
			//
			this.cdvFactory2.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory2.BtnToolTipText = "";
			this.cdvFactory2.DescText = "";
			this.cdvFactory2.DisplaySubItemIndex = - 1;
			this.cdvFactory2.DisplayText = "";
			this.cdvFactory2.Focusing = null;
			this.cdvFactory2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory2.Index = 0;
			this.cdvFactory2.IsViewBtnImage = false;
			this.cdvFactory2.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory2.MaxLength = 10;
			this.cdvFactory2.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory2.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory2.Name = "cdvFactory2";
			this.cdvFactory2.ReadOnly = false;
			this.cdvFactory2.SearchSubItemIndex = 0;
			this.cdvFactory2.SelectedDescIndex = - 1;
			this.cdvFactory2.SelectedSubItemIndex = - 1;
			this.cdvFactory2.SelectionStart = 0;
			this.cdvFactory2.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory2.SmallImageList = null;
			this.cdvFactory2.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory2.TabIndex = 51;
			this.cdvFactory2.TextBoxToolTipText = "";
			this.cdvFactory2.TextBoxWidth = 168;
			this.cdvFactory2.VisibleButton = true;
			this.cdvFactory2.VisibleColumnHeader = false;
			this.cdvFactory2.VisibleDescription = false;
			//
			//lblFactory2
			//
			this.lblFactory2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory2.Location = new System.Drawing.Point(140, 10);
			this.lblFactory2.Name = "lblFactory2";
			this.lblFactory2.Size = new System.Drawing.Size(100, 14);
			this.lblFactory2.TabIndex = 52;
			this.lblFactory2.Text = "Factory";
			//
			//pnlLayout1
			//
			this.pnlLayout1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlLayout1.Controls.Add(this.cboCategory1);
			this.pnlLayout1.Controls.Add(this.lblCategory1);
			this.pnlLayout1.Controls.Add(this.cdvLayout1);
			this.pnlLayout1.Controls.Add(this.lblLayout1);
			this.pnlLayout1.Controls.Add(this.cdvFactory1);
			this.pnlLayout1.Controls.Add(this.lblFactory1);
			this.pnlLayout1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlLayout1.Location = new System.Drawing.Point(3, 16);
			this.pnlLayout1.Name = "pnlLayout1";
			this.pnlLayout1.Size = new System.Drawing.Size(424, 56);
			this.pnlLayout1.TabIndex = 0;
			//
			//cboCategory1
			//
			this.cboCategory1.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
			this.cboCategory1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ValueListItem17.DataValue = "ValueListItem0";
			ValueListItem17.DisplayText = "Layout";
			ValueListItem18.DataValue = "ValueListItem1";
			ValueListItem18.DisplayText = "User Group";
			this.cboCategory1.Items.Add(ValueListItem17);
			this.cboCategory1.Items.Add(ValueListItem18);
			this.cboCategory1.Location = new System.Drawing.Point(8, 32);
			this.cboCategory1.Name = "cboCategory1";
			this.cboCategory1.Size = new System.Drawing.Size(120, 19);
			this.cboCategory1.TabIndex = 56;
			//
			//lblCategory1
			//
			this.lblCategory1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblCategory1.Location = new System.Drawing.Point(8, 10);
			this.lblCategory1.Name = "lblCategory1";
			this.lblCategory1.Size = new System.Drawing.Size(75, 14);
			this.lblCategory1.TabIndex = 55;
			this.lblCategory1.Text = "Category";
			//
			//cdvLayout1
			//
			this.cdvLayout1.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvLayout1.BtnToolTipText = "";
			this.cdvLayout1.DescText = "";
			this.cdvLayout1.DisplaySubItemIndex = - 1;
			this.cdvLayout1.DisplayText = "";
			this.cdvLayout1.Focusing = null;
			this.cdvLayout1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvLayout1.Index = 0;
			this.cdvLayout1.IsViewBtnImage = false;
			this.cdvLayout1.Location = new System.Drawing.Point(248, 31);
			this.cdvLayout1.MaxLength = 10;
			this.cdvLayout1.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvLayout1.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvLayout1.Name = "cdvLayout1";
			this.cdvLayout1.ReadOnly = false;
			this.cdvLayout1.SearchSubItemIndex = 0;
			this.cdvLayout1.SelectedDescIndex = - 1;
			this.cdvLayout1.SelectedSubItemIndex = - 1;
			this.cdvLayout1.SelectionStart = 0;
			this.cdvLayout1.Size = new System.Drawing.Size(168, 20);
			this.cdvLayout1.SmallImageList = null;
			this.cdvLayout1.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvLayout1.TabIndex = 53;
			this.cdvLayout1.TextBoxToolTipText = "";
			this.cdvLayout1.TextBoxWidth = 168;
			this.cdvLayout1.VisibleButton = true;
			this.cdvLayout1.VisibleColumnHeader = false;
			this.cdvLayout1.VisibleDescription = false;
			//
			//lblLayout1
			//
			this.lblLayout1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblLayout1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblLayout1.Location = new System.Drawing.Point(140, 34);
			this.lblLayout1.Name = "lblLayout1";
			this.lblLayout1.Size = new System.Drawing.Size(100, 14);
			this.lblLayout1.TabIndex = 54;
			this.lblLayout1.Text = "Layout";
			//
			//cdvFactory1
			//
			this.cdvFactory1.BtnFlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cdvFactory1.BtnToolTipText = "";
			this.cdvFactory1.DescText = "";
			this.cdvFactory1.DisplaySubItemIndex = - 1;
			this.cdvFactory1.DisplayText = "";
			this.cdvFactory1.Focusing = null;
			this.cdvFactory1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.cdvFactory1.Index = 0;
			this.cdvFactory1.IsViewBtnImage = false;
			this.cdvFactory1.Location = new System.Drawing.Point(248, 7);
			this.cdvFactory1.MaxLength = 10;
			this.cdvFactory1.MCViewStyle.BorderColor = System.Drawing.SystemColors.Control;
			this.cdvFactory1.MCViewStyle.BorderHotColor = System.Drawing.SystemColors.Control;
			this.cdvFactory1.Name = "cdvFactory1";
			this.cdvFactory1.ReadOnly = false;
			this.cdvFactory1.SearchSubItemIndex = 0;
			this.cdvFactory1.SelectedDescIndex = - 1;
			this.cdvFactory1.SelectedSubItemIndex = - 1;
			this.cdvFactory1.SelectionStart = 0;
			this.cdvFactory1.Size = new System.Drawing.Size(168, 20);
			this.cdvFactory1.SmallImageList = null;
			this.cdvFactory1.StyleBorder = System.Windows.Forms.BorderStyle.FixedSingle;
			this.cdvFactory1.TabIndex = 51;
			this.cdvFactory1.TextBoxToolTipText = "";
			this.cdvFactory1.TextBoxWidth = 168;
			this.cdvFactory1.VisibleButton = true;
			this.cdvFactory1.VisibleColumnHeader = false;
			this.cdvFactory1.VisibleDescription = false;
			//
			//lblFactory1
			//
			this.lblFactory1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lblFactory1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblFactory1.Location = new System.Drawing.Point(140, 10);
			this.lblFactory1.Name = "lblFactory1";
			this.lblFactory1.Size = new System.Drawing.Size(100, 14);
			this.lblFactory1.TabIndex = 52;
			this.lblFactory1.Text = "Factory";
			//
			//btnClear
			//
			this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnClear.Location = new System.Drawing.Point(419, 9);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(74, 23);
			this.btnClear.TabIndex = 3;
			this.btnClear.Text = "Clear";
			//
			//frmFMBArrangeLayout
			//
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(580, 566);
			this.Controls.Add(this.pnlFill);
			this.Controls.Add(this.pnlLeft);
			this.Controls.Add(this.pnlBottom);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmFMBArrangeLayout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Arrange Layouts";
			this.pnlBottom.ResumeLayout(false);
			this.pnlLeft.ResumeLayout(false);
			this.grpLayout.ResumeLayout(false);
			this.pnlFill.ResumeLayout(false);
			this.grpSelectLayout.ResumeLayout(false);
			this.pnlLayout9.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory9).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout9).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory9).EndInit();
			this.pnlLayout8.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory8).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout8).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory8).EndInit();
			this.pnlLayout7.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory7).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout7).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory7).EndInit();
			this.pnlLayout6.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory6).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout6).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory6).EndInit();
			this.pnlLayout5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory5).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout5).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory5).EndInit();
			this.pnlLayout4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory4).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout4).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory4).EndInit();
			this.pnlLayout3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory3).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout3).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory3).EndInit();
			this.pnlLayout2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory2).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout2).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory2).EndInit();
			this.pnlLayout1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cboCategory1).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvLayout1).EndInit();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory1).EndInit();
			this.ResumeLayout(false);
			
		}
		
		#endregion
		public struct LayoutInfo
		{
			public UltraComboEditor cboCategory;
			public Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory;
			public Miracom.UI.Controls.MCCodeView.MCCodeView cdvLayout;
			public Label lblFactory;
			public Label lblLayout;
			public int iSeq;
		}
		
		private bool CheckCondition()
		{
			
			try
			{
				Panel ctlPanel;
				
				foreach (Panel tempLoopVar_ctlPanel in this.grpSelectLayout.Controls)
				{
					ctlPanel = tempLoopVar_ctlPanel;
					if (CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cdvLayout.Text) != "")
					{
						if (CheckLayoutID(((LayoutInfo) ctlPanel.Tag).cdvLayout) == false)
						{
							this.DialogResult = DialogResult.None;
							return false;
						}
					}
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.CheckCondition()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
			}
			
		}
		
		private void InitControls()
		{
			
			try
			{
				LayoutInfo Layout;
				
				Layout.iSeq = 1;
				Layout.cboCategory = cboCategory1;
				Layout.cdvFactory = cdvFactory1;
				Layout.cdvLayout = cdvLayout1;
				Layout.lblFactory = lblFactory1;
				Layout.lblLayout = lblLayout1;
				pnlLayout1.Tag = Layout;
				
				Layout.iSeq = 2;
				Layout.cboCategory = cboCategory2;
				Layout.cdvFactory = cdvFactory2;
				Layout.cdvLayout = cdvLayout2;
				Layout.lblFactory = lblFactory2;
				Layout.lblLayout = lblLayout2;
				pnlLayout2.Tag = Layout;
				
				Layout.iSeq = 3;
				Layout.cboCategory = cboCategory3;
				Layout.cdvFactory = cdvFactory3;
				Layout.cdvLayout = cdvLayout3;
				Layout.lblFactory = lblFactory3;
				Layout.lblLayout = lblLayout3;
				pnlLayout3.Tag = Layout;
				
				Layout.iSeq = 4;
				Layout.cboCategory = cboCategory4;
				Layout.cdvFactory = cdvFactory4;
				Layout.cdvLayout = cdvLayout4;
				Layout.lblFactory = lblFactory4;
				Layout.lblLayout = lblLayout4;
				pnlLayout4.Tag = Layout;
				
				Layout.iSeq = 5;
				Layout.cboCategory = cboCategory5;
				Layout.cdvFactory = cdvFactory5;
				Layout.cdvLayout = cdvLayout5;
				Layout.lblFactory = lblFactory5;
				Layout.lblLayout = lblLayout5;
				pnlLayout5.Tag = Layout;
				
				Layout.iSeq = 6;
				Layout.cboCategory = cboCategory6;
				Layout.cdvFactory = cdvFactory6;
				Layout.cdvLayout = cdvLayout6;
				Layout.lblFactory = lblFactory6;
				Layout.lblLayout = lblLayout6;
				pnlLayout6.Tag = Layout;
				
				Layout.iSeq = 7;
				Layout.cboCategory = cboCategory7;
				Layout.cdvFactory = cdvFactory7;
				Layout.cdvLayout = cdvLayout7;
				Layout.lblFactory = lblFactory7;
				Layout.lblLayout = lblLayout7;
				pnlLayout7.Tag = Layout;
				
				Layout.iSeq = 8;
				Layout.cboCategory = cboCategory8;
				Layout.cdvFactory = cdvFactory8;
				Layout.cdvLayout = cdvLayout8;
				Layout.lblFactory = lblFactory8;
				Layout.lblLayout = lblLayout8;
				pnlLayout8.Tag = Layout;
				
				Layout.iSeq = 9;
				Layout.cboCategory = cboCategory9;
				Layout.cdvFactory = cdvFactory9;
				Layout.cdvLayout = cdvLayout9;
				Layout.lblFactory = lblFactory9;
				Layout.lblLayout = lblLayout9;
				pnlLayout9.Tag = Layout;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.InitControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void GetLayoutInfo()
		{
			
			try
			{
				int iCount;
				int i;
				string sCategory;
				string sFactory;
				string sLayout;
				Panel ctlPanel;
                iCount = CmnFunction.ToInt(Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "ArrangeLayout", "Count", "0"));
				if (iCount == 2)
				{
					rbn2.Checked = true;
				}
				else if (iCount == 4)
				{
					rbn4.Checked = true;
				}
				else if (iCount == 6)
				{
					rbn6.Checked = true;
				}
				else if (iCount == 9)
				{
					rbn9.Checked = true;
				}
				
				for (i = 0; i <= iCount - 1; i++)
				{
                    sCategory = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "ArrangeLayout", "Category" + System.Convert.ToString(i + 1), "");
                    sFactory = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "ArrangeLayout", "Factory" + System.Convert.ToString(i + 1), "");
                    sLayout = Microsoft.VisualBasic.Interaction.GetSetting(Application.ProductName, "ArrangeLayout", "Layout" + System.Convert.ToString(i + 1), "");
					foreach (Panel tempLoopVar_ctlPanel in this.grpSelectLayout.Controls)
					{
						ctlPanel = tempLoopVar_ctlPanel;
						if (((LayoutInfo) ctlPanel.Tag).iSeq == i + 1)
						{
							if (sCategory == modGlobalConstant.FMB_CATEGORY_LAYOUT)
							{
								((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex = 0;
							}
							else if (sCategory == modGlobalConstant.FMB_CATEGORY_GROUP)
							{
								((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex = 1;
							}
							((LayoutInfo) ctlPanel.Tag).cdvFactory.Text = sFactory;
							((LayoutInfo) ctlPanel.Tag).cdvLayout.Text = sLayout;
						}
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.InitControls()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void SetLayoutEnable(int iLayoutCount)
		{
			
			try
			{
				Panel ctlPanel;
				
				foreach (Panel tempLoopVar_ctlPanel in this.grpSelectLayout.Controls)
				{
					ctlPanel = tempLoopVar_ctlPanel;
					if (((LayoutInfo) ctlPanel.Tag).iSeq <= iLayoutCount)
					{
						if (((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex != 1)
						{
							modCommonFunction.CheckAllFactoryOption(((LayoutInfo) ctlPanel.Tag).cdvFactory);
						}
						((LayoutInfo) ctlPanel.Tag).cdvFactory.Enabled = true;
						((LayoutInfo) ctlPanel.Tag).cdvLayout.Enabled = true;
						((LayoutInfo) ctlPanel.Tag).cboCategory.Enabled = true;
						((LayoutInfo) ctlPanel.Tag).lblFactory.Font = new System.Drawing.Font(lblFactory1.Font, System.Drawing.FontStyle.Bold);
						((LayoutInfo) ctlPanel.Tag).lblLayout.Font = new System.Drawing.Font(lblLayout1.Font, System.Drawing.FontStyle.Bold);
					}
					else
					{
						if (modGlobalVariable.gbAllFactory == true)
						{
							((LayoutInfo) ctlPanel.Tag).cdvFactory.ReadOnly = false;
							((LayoutInfo) ctlPanel.Tag).cdvFactory.VisibleButton = true;
							((LayoutInfo) ctlPanel.Tag).cdvFactory.BackColor = SystemColors.Window;
						}
						((LayoutInfo) ctlPanel.Tag).cdvFactory.Enabled = false;
						((LayoutInfo) ctlPanel.Tag).cdvLayout.Enabled = false;
						((LayoutInfo) ctlPanel.Tag).cboCategory.Enabled = false;
						((LayoutInfo) ctlPanel.Tag).cdvFactory.Text = "";
						((LayoutInfo) ctlPanel.Tag).cdvLayout.Text = "";
						((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex = - 1;
						((LayoutInfo) ctlPanel.Tag).lblFactory.Font = new System.Drawing.Font(lblFactory1.Font, System.Drawing.FontStyle.Regular);
						((LayoutInfo) ctlPanel.Tag).lblLayout.Font = new System.Drawing.Font(lblLayout1.Font, System.Drawing.FontStyle.Regular);
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.SetLayoutEnable()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private bool CheckLayoutID(Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp)
		{
			
			try
			{
				Panel ctlPanel;
				
				foreach (Panel tempLoopVar_ctlPanel in this.grpSelectLayout.Controls)
				{
					ctlPanel = tempLoopVar_ctlPanel;
					if (((LayoutInfo) ctlPanel.Tag).iSeq != ((LayoutInfo) cdvTemp.Parent.Tag).iSeq)
					{
						if (CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cboCategory.Text) == CmnFunction.Trim(((LayoutInfo) cdvTemp.Parent.Tag).cboCategory.Text) && CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cdvFactory.Text) == CmnFunction.Trim(((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory.Text) && CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cdvLayout.Text) == CmnFunction.Trim(((LayoutInfo) cdvTemp.Parent.Tag).cdvLayout.Text))
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(34), "FMB Client", MessageBoxButtons.OK, 1);
							cdvTemp.Text = "";
							cdvTemp.Focus();
							return false;
						}
					}
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.CheckLayoutID()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
			}
			
		}
		
		// SaveLayoutOption()
		//       - Save Layout Options
		// Return Value
		//       -
		// Arguments
		//       -
		//
		private void ArrangeLayout()
		{
			
			try
			{
				ArrayList aArrList = new ArrayList();
				ArrayList aTemp = new ArrayList();
				modGlobalVariable.gLayouts sLayouts = new modGlobalVariable.gLayouts();
				Panel ctlPanel;
				int iCount = 0;
				int i;
				string sFactory = "";
				string sLayout = "";
				
				foreach (Panel tempLoopVar_ctlPanel in this.grpSelectLayout.Controls)
				{
					ctlPanel = tempLoopVar_ctlPanel;
					if (CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cboCategory.Text) != "" && CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cdvFactory.Text) != "" && CmnFunction.Trim(((LayoutInfo) ctlPanel.Tag).cdvLayout.Text) != "")
					{
						if (((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex == 0)
						{
							sLayouts.sTag = modGlobalConstant.FMB_CATEGORY_LAYOUT;
							sLayouts.sFormName = ((LayoutInfo) ctlPanel.Tag).cdvFactory.Text + ":" + ((LayoutInfo) ctlPanel.Tag).cdvLayout.Text;
						}
						else if (((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex == 1)
						{
							sLayouts.sTag = modGlobalConstant.FMB_CATEGORY_GROUP;
							sLayouts.sFormName = ((LayoutInfo) ctlPanel.Tag).cdvLayout.Text;
						}
						aTemp.Add(sLayouts);
					}
				}
				if (rbn2.Checked == true)
				{
					iCount = 2;
				}
				else if (rbn4.Checked == true)
				{
					iCount = 4;
				}
				else if (rbn6.Checked == true)
				{
					iCount = 6;
				}
				else if (rbn9.Checked == true)
				{
					iCount = 9;
				}
				
				for (i = aTemp.Count - 1; i >= 0; i--)
				{
					aArrList.Add(aTemp[i]);
				}

                Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "ArrangeLayout", "Count", iCount.ToString());
				for (i = 0; i <= aArrList.Count - 1; i++)
				{
                    Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "ArrangeLayout", "Category" + System.Convert.ToString(i + 1), ((modGlobalVariable.gLayouts)aArrList[i]).sTag);
					if (((modGlobalVariable.gLayouts) aArrList[i]).sTag == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						sFactory = modCommonFunction.GetStringBySeperator(((modGlobalVariable.gLayouts) aArrList[i]).sFormName, ":", 1);
						sLayout = modCommonFunction.GetStringBySeperator(((modGlobalVariable.gLayouts) aArrList[i]).sFormName, ":", 2);
					}
					else if (((modGlobalVariable.gLayouts) aArrList[i]).sTag  == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						sFactory = GlobalVariable.gsFactory;
						sLayout = ((modGlobalVariable.gLayouts) aArrList[i]).sFormName;
					}
                    Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "ArrangeLayout", "Factory" + System.Convert.ToString(i + 1), sFactory);
                    Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "ArrangeLayout", "Layout" + System.Convert.ToString(i + 1), sLayout);
				}
				
				modInterface.gIMdiForm.ArrageLayouts(aArrList, iCount);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.CheckLayoutID()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
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
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_ButtonPress(object sender, System.EventArgs e)
		{
			
			try
			{
				Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
				cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView) sender;
				
				cdvTemp.Init();
				cdvTemp.Columns.Add("Factory", 100, HorizontalAlignment.Left);
				cdvTemp.Columns.Add("Desc", 100, HorizontalAlignment.Left);
				cdvTemp.SelectedSubItemIndex = 0;
				//cdvTemp.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
                //WIPLIST.ViewFactoryList(cdvTemp.GetListView, '1',null);
				cdvTemp.AddEmptyRow(1);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.cdvFactory_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvLayout_ButtonPress(object sender, System.EventArgs e)
		{
			
			try
			{
				Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
				cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView) sender;
				
				cdvTemp.Init();
				cdvTemp.Columns.Add("Layout", 100, HorizontalAlignment.Left);
				cdvTemp.Columns.Add("Desc", 100, HorizontalAlignment.Left);
				cdvTemp.SelectedSubItemIndex = 0;
				//cdvTemp.SmallImageList = GlobalVariable.gIMdiForm.GetSmallIconList();
				if (((LayoutInfo) cdvTemp.Parent.Tag).cboCategory.SelectedIndex == 0)
				{
                    modListRoutine.ViewLayOutList(cdvTemp.GetListView, '1', CmnFunction.Trim(((LayoutInfo)cdvTemp.Parent.Tag).cdvFactory.Text));
				}
				else if (((LayoutInfo) cdvTemp.Parent.Tag).cboCategory.SelectedIndex == 1)
				{
                    modListRoutine.ViewUDR_GroupList(cdvTemp.GetListView, '1');
				}
				
				cdvTemp.AddEmptyRow(1);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.cdvLayout_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBArrangeLayout_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				modCommonFunction.GetTextboxStyle(this.Controls);
				modLanguageFunction.ToClientLanguage(this);
				InitControls();
				rbn2.Checked = true;
				GetLayoutInfo();
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.frmFMBArrangeLayout_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cboCategory_SelectionChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				UltraComboEditor cdvTemp;
				cdvTemp = (UltraComboEditor) sender;
				
				((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory.Text = "";
				((LayoutInfo) cdvTemp.Parent.Tag).cdvLayout.Text = "";
				
				if (cdvTemp.SelectedIndex == 0)
				{
					modCommonFunction.CheckAllFactoryOption(((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory);
				}
				else if (cdvTemp.SelectedIndex == 1)
				{
					((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory.Text = GlobalVariable.gsFactory;
					((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory.ReadOnly = true;
					((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory.VisibleButton = false;
					((LayoutInfo) cdvTemp.Parent.Tag).cdvFactory.BackColor = SystemColors.Control;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.frmFMBArrangeLayout_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void rbnCount_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (rbn2.Checked == true)
				{
					SetLayoutEnable(2);
				}
				else if (rbn4.Checked == true)
				{
					SetLayoutEnable(4);
				}
				else if (rbn6.Checked == true)
				{
					SetLayoutEnable(6);
				}
				else if (rbn9.Checked == true)
				{
					SetLayoutEnable(9);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.rbnCount_CheckedChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnOK_Click(object sender, System.EventArgs e)
		{
			
			try
			{
				
				if (CheckCondition() == false)
				{
					return;
				}
				ArrangeLayout();
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.btnOK_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				this.DialogResult = DialogResult.None;
			}
			
		}

        private void cdvFactory_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
		{
			
			try
			{
				Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
				cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView) sender;
				((LayoutInfo) cdvTemp.Parent.Tag).cdvLayout.Text = "";
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.cdvFactory_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}

        private void cdvLayout_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
		{
			
			try
			{
				Miracom.UI.Controls.MCCodeView.MCCodeView cdvTemp;
				cdvTemp = (Miracom.UI.Controls.MCCodeView.MCCodeView) sender;
				
				if (cdvTemp.Text == "")
				{
					return;
				}
				
				CheckLayoutID(cdvTemp);
				
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.cdvLayout_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		
		private void btnClear_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				Panel ctlPanel;
				foreach (Panel tempLoopVar_ctlPanel in this.grpSelectLayout.Controls)
				{
					ctlPanel = tempLoopVar_ctlPanel;
					((LayoutInfo) ctlPanel.Tag).cboCategory.SelectedIndex = - 1;
					((LayoutInfo) ctlPanel.Tag).cdvFactory.Text = "";
					((LayoutInfo) ctlPanel.Tag).cdvLayout.Text = "";
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBArrangeLayout.cdvLayout_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
	}
	
}
