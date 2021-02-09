
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Miracom.FMBUI.Controls;
using com.miracom.transceiverx.session;

using Miracom.SmartWeb.FWX;
//using Miracom.H101Core;
//using Miracom.MESCore;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : frmFMBSetupResourceImage.vb
//   Description : Update Resource Image
//
//   FMB Version : 1.0.0
//
//   Function List
//       - CheckCondition() : Check the conditions before transaction
//       - Update_Resource_Image() : Update Resource Image
//       - ViewImageList() : View Image List
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
	public class frmFMBSetupResourceImage : System.Windows.Forms.Form
	{
		
		#region " Windows Form Auto Generated Code "
		
		public frmFMBSetupResourceImage()
		{
			
			//???¸ì¶œ?€ Windows Form ?”ìž?´ë„ˆ???„ìš”?©ë‹ˆ??
			InitializeComponent();
			
			//InitializeComponent()ë¥??¸ì¶œ???¤ìŒ??ì´ˆê¸°???‘ì—…??ì¶”ê??˜ì‹­?œì˜¤.
			
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
		internal System.Windows.Forms.Panel pnlBottom;
		internal System.Windows.Forms.Button btnClose;
		internal System.Windows.Forms.Panel pnlTop;
		internal System.Windows.Forms.GroupBox grpFactory;
		internal Miracom.UI.Controls.MCCodeView.MCCodeView cdvFactory;
		internal System.Windows.Forms.Label lblFactory;
		internal System.Windows.Forms.Panel pnlMid;
		internal System.Windows.Forms.Panel pnlImgMid;
		internal System.Windows.Forms.Panel pnlImgMidRight;
		internal System.Windows.Forms.Panel pnlImgMidMid;
		internal System.Windows.Forms.Button btnDel;
		internal System.Windows.Forms.Button btnAdd;
		internal Miracom.UI.Controls.MCListView.MCListView lisImagelist;
		internal System.Windows.Forms.ColumnHeader ColumnHeader15;
		internal System.Windows.Forms.Panel pnlRes;
		internal Miracom.UI.Controls.MCListView.MCListView lisResource;
		internal System.Windows.Forms.ColumnHeader ColumnHeader1;
		internal System.Windows.Forms.ColumnHeader ColumnHeader2;
		internal System.Windows.Forms.ColumnHeader ColumnHeader3;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.pnlBottom = new System.Windows.Forms.Panel();
			base.Load += new System.EventHandler(frmFMBSetupResourceImage_Load);
			this.btnClose = new System.Windows.Forms.Button();
			this.btnClose.Click += new System.EventHandler(btnClose_Click);
			this.pnlTop = new System.Windows.Forms.Panel();
			this.grpFactory = new System.Windows.Forms.GroupBox();
			this.cdvFactory = new Miracom.UI.Controls.MCCodeView.MCCodeView();
			this.cdvFactory.ButtonPress += new System.EventHandler(cdvFactory_ButtonPress);
			this.cdvFactory.SelectedItemChanged += new Miracom.UI.MCCodeViewSelChangedHandler(cdvFactory_SelectedItemChanged);
			this.cdvFactory.TextBoxTextChanged += new System.EventHandler(cdvFactory_TextBoxTextChanged);
			this.lblFactory = new System.Windows.Forms.Label();
			this.pnlMid = new System.Windows.Forms.Panel();
			this.pnlRes = new System.Windows.Forms.Panel();
			this.lisResource = new Miracom.UI.Controls.MCListView.MCListView();
			this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.pnlImgMid = new System.Windows.Forms.Panel();
			this.pnlImgMidRight = new System.Windows.Forms.Panel();
			this.lisImagelist = new Miracom.UI.Controls.MCListView.MCListView();
			this.ColumnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.pnlImgMidMid = new System.Windows.Forms.Panel();
			this.btnDel = new System.Windows.Forms.Button();
			this.btnDel.Click += new System.EventHandler(btnDel_Click);
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnAdd.Click += new System.EventHandler(btnAdd_Click);
			this.pnlBottom.SuspendLayout();
			this.pnlTop.SuspendLayout();
			this.grpFactory.SuspendLayout();
			((System.ComponentModel.ISupportInitialize) this.cdvFactory).BeginInit();
			this.pnlMid.SuspendLayout();
			this.pnlRes.SuspendLayout();
			this.pnlImgMid.SuspendLayout();
			this.pnlImgMidRight.SuspendLayout();
			this.pnlImgMidMid.SuspendLayout();
			this.SuspendLayout();
			//
			//pnlBottom
			//
			this.pnlBottom.Controls.Add(this.btnClose);
			this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnlBottom.Location = new System.Drawing.Point(0, 506);
			this.pnlBottom.Name = "pnlBottom";
			this.pnlBottom.Size = new System.Drawing.Size(742, 40);
			this.pnlBottom.TabIndex = 2;
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
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
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
			//pnlMid
			//
			this.pnlMid.Controls.Add(this.pnlRes);
			this.pnlMid.Controls.Add(this.pnlImgMid);
			this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMid.DockPadding.All = 3;
			this.pnlMid.Location = new System.Drawing.Point(0, 52);
			this.pnlMid.Name = "pnlMid";
			this.pnlMid.Size = new System.Drawing.Size(742, 454);
			this.pnlMid.TabIndex = 1;
			//
			//pnlRes
			//
			this.pnlRes.Controls.Add(this.lisResource);
			this.pnlRes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlRes.Location = new System.Drawing.Point(3, 3);
			this.pnlRes.Name = "pnlRes";
			this.pnlRes.Size = new System.Drawing.Size(484, 448);
			this.pnlRes.TabIndex = 3;
			//
			//lisResource
			//
			this.lisResource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { this.ColumnHeader1, this.ColumnHeader2, this.ColumnHeader3 });
			this.lisResource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lisResource.EnableSort = true;
			this.lisResource.EnableSortIcon = true;
			this.lisResource.FullRowSelect = true;
			this.lisResource.Location = new System.Drawing.Point(0, 0);
			this.lisResource.MultiSelect = false;
			this.lisResource.Name = "lisResource";
			this.lisResource.Size = new System.Drawing.Size(484, 448);
			this.lisResource.TabIndex = 0;
			this.lisResource.View = System.Windows.Forms.View.Details;
			//
			//ColumnHeader1
			//
			this.ColumnHeader1.Text = "Image";
			this.ColumnHeader1.Width = 55;
			//
			//ColumnHeader2
			//
			this.ColumnHeader2.Text = "Resource";
			this.ColumnHeader2.Width = 154;
			//
			//ColumnHeader3
			//
			this.ColumnHeader3.Text = "Description";
			this.ColumnHeader3.Width = 257;
			//
			//pnlImgMid
			//
			this.pnlImgMid.Controls.Add(this.pnlImgMidRight);
			this.pnlImgMid.Controls.Add(this.pnlImgMidMid);
			this.pnlImgMid.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlImgMid.DockPadding.Right = 5;
			this.pnlImgMid.Location = new System.Drawing.Point(487, 3);
			this.pnlImgMid.Name = "pnlImgMid";
			this.pnlImgMid.Size = new System.Drawing.Size(252, 448);
			this.pnlImgMid.TabIndex = 0;
			//
			//pnlImgMidRight
			//
			this.pnlImgMidRight.Controls.Add(this.lisImagelist);
			this.pnlImgMidRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnlImgMidRight.Location = new System.Drawing.Point(47, 0);
			this.pnlImgMidRight.Name = "pnlImgMidRight";
			this.pnlImgMidRight.Size = new System.Drawing.Size(200, 448);
			this.pnlImgMidRight.TabIndex = 18;
			//
			//lisImagelist
			//
			this.lisImagelist.AllowDrop = true;
			this.lisImagelist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.ColumnHeader15});
			this.lisImagelist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lisImagelist.EnableSort = true;
			this.lisImagelist.EnableSortIcon = true;
			this.lisImagelist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lisImagelist.FullRowSelect = true;
			this.lisImagelist.Location = new System.Drawing.Point(0, 0);
			this.lisImagelist.Name = "lisImagelist";
			this.lisImagelist.Size = new System.Drawing.Size(200, 448);
			this.lisImagelist.TabIndex = 0;
			this.lisImagelist.View = System.Windows.Forms.View.Details;
			//
			//ColumnHeader15
			//
			this.ColumnHeader15.Text = "Image";
			this.ColumnHeader15.Width = 174;
			//
			//pnlImgMidMid
			//
			this.pnlImgMidMid.Controls.Add(this.btnDel);
			this.pnlImgMidMid.Controls.Add(this.btnAdd);
			this.pnlImgMidMid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.pnlImgMidMid.Location = new System.Drawing.Point(4, 170);
			this.pnlImgMidMid.Name = "pnlImgMidMid";
			this.pnlImgMidMid.Size = new System.Drawing.Size(38, 108);
			this.pnlImgMidMid.TabIndex = 0;
			//
			//btnDel
			//
			this.btnDel.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnDel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnDel.Location = new System.Drawing.Point(7, 57);
			this.btnDel.Name = "btnDel";
			this.btnDel.Size = new System.Drawing.Size(24, 24);
			this.btnDel.TabIndex = 1;
			this.btnDel.Text = ">";
			//
			//btnAdd
			//
			this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.btnAdd.Location = new System.Drawing.Point(7, 28);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(24, 24);
			this.btnAdd.TabIndex = 0;
			this.btnAdd.Text = "<";
			//
			//frmFMBSetupResourceImage
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(742, 546);
			this.Controls.Add(this.pnlMid);
			this.Controls.Add(this.pnlTop);
			this.Controls.Add(this.pnlBottom);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.MinimumSize = new System.Drawing.Size(750, 580);
			this.Name = "frmFMBSetupResourceImage";
			this.Tag = "FMB1002";
			this.Text = "Resource Image Setup";
			this.pnlBottom.ResumeLayout(false);
			this.pnlTop.ResumeLayout(false);
			this.grpFactory.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize) this.cdvFactory).EndInit();
			this.pnlMid.ResumeLayout(false);
			this.pnlRes.ResumeLayout(false);
			this.pnlImgMid.ResumeLayout(false);
			this.pnlImgMidRight.ResumeLayout(false);
			this.pnlImgMidMid.ResumeLayout(false);
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		#region " Variable Definition"
		private ImageList imlRes;
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
                switch (CmnFunction.RTrim(sStep))
				{
					case "UPDATE":
						
						if (cdvFactory.Text == "")
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
							cdvFactory.Focus();
							return false;
						}
						if (lisResource.SelectedItems.Count == 0)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(19) + " (Resource)", "FMB Client", MessageBoxButtons.OK, 1);
							lisResource.Focus();
							return false;
						}
						if (lisImagelist.SelectedItems.Count == 0)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(19) + " (Image)", "FMB Client", MessageBoxButtons.OK, 1);
							lisImagelist.Focus();
							return false;
						}
						break;
					case "DELETE":
						
						if (cdvFactory.Text == "")
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(6), "FMB Client", MessageBoxButtons.OK, 1);
							cdvFactory.Focus();
							return false;
						}
						if (lisResource.SelectedItems.Count == 0)
						{
							CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(19) + " (Resource)", "FMB Client", MessageBoxButtons.OK, 1);
							lisResource.Focus();
							return false;
						}
						break;
						
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBViewLotListDetail.CheckCondition()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		// Update_Resource_Image()
		//       - Update/Delete Resource Image
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       - sStep as String  : Step
		//
		private bool Update_Resource_Image(char sStep)
		{
			
			try
			{
                FMB_Update_Resource_Image_In_Tag Update_Resource_Image_In = new FMB_Update_Resource_Image_In_Tag();
                FMB_Cmn_Out_Tag Cmn_Out = new FMB_Cmn_Out_Tag();

				Update_Resource_Image_In._C.h_proc_step = sStep;
				Update_Resource_Image_In._C.h_passport = GlobalVariable.gsPassport;
				Update_Resource_Image_In._C.h_language = GlobalVariable.gcLanguage;
                Update_Resource_Image_In._C.h_factory = CmnFunction.RTrim(cdvFactory.Text);
				Update_Resource_Image_In._C.h_user_id = GlobalVariable.gsUserID;
				Update_Resource_Image_In._C.h_password = GlobalVariable.gsPassword;
				Update_Resource_Image_In._C.res_id = lisResource.SelectedItems[0].SubItems[1].Text;
				if (sStep != modGlobalConstant.MP_STEP_DELETE)
				{
					Update_Resource_Image_In._C.img_idx = lisImagelist.SelectedItems[0].Index;
				}

                if (FMBSender.FMB_Update_Resource_Image(Update_Resource_Image_In, ref Cmn_Out) == false)
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
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.Update_Resource_Image()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
			}
		}
		
		// ViewImageList()
		//       - View Image List
		// Return Value
		//       - Boolean : Return True/False
		// Arguments
		//       -
		//
		private bool ViewImageList()
		{
			
			try
			{
				ListViewItem itmx;
				int i;
                udcCtrlResource ctrlRes = new udcCtrlResource(modGlobalVariable.gimlResource);
				imlRes = ctrlRes.imlResource;
				lisImagelist.SmallImageList = imlRes;
				for (i = 0; i <= imlRes.Images.Count - 1; i++)
				{
					itmx = new ListViewItem("", i);
					lisImagelist.Items.Add(itmx);
				}
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.ViewImageList()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return false;
			}
		}
		
		#endregion
		
		#region " Event Implementations"
		
		private void btnClose_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				this.Dispose();
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.btnClose_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void frmFMBSetupResourceImage_Load(object sender, System.EventArgs e)
		{
			
			try
			{
				modCommonFunction.CheckSecurityFormControl(this);
				modCommonFunction.GetTextboxStyle(this.Controls);
				modLanguageFunction.ToClientLanguage(this);
                CmnInitFunction.InitListView(lisResource);
                CmnInitFunction.InitListView(lisImagelist);
				ViewImageList();
				lisResource.SmallImageList = imlRes;
				
				modCommonFunction.CheckAllFactoryOption(cdvFactory);
				if (modGlobalVariable.gbAllFactory == false)
				{
                    CmnInitFunction.InitListView(lisResource);
                    lisResource.SmallImageList = imlRes;
                    modListRoutine.ViewResourceImageList(lisResource, '1', cdvFactory.Text);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.frmFMBSetupResourceImage_Load()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_ButtonPress(object sender, System.EventArgs e)
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
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.cdvFactory_ButtonPress()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}

        private void cdvFactory_SelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
		{
			
			try
			{
                CmnInitFunction.InitListView(lisResource);
                lisResource.SmallImageList = imlRes;
                modListRoutine.ViewResourceImageList(lisResource, '1', cdvFactory.Text);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.cdvFactory_SelectedItemChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void cdvFactory_TextBoxTextChanged(object sender, System.EventArgs e)
		{
			
			try
			{
				CmnInitFunction.InitListView(lisResource);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.cdvFactory_TextBoxTextChanged()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnAdd_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (CheckCondition("UPDATE") == false)
				{
					return;
				}
				if (Update_Resource_Image(modGlobalConstant.MP_STEP_UPDATE) == true)
				{
                    CmnInitFunction.InitListView(lisResource);
                    lisResource.SmallImageList = imlRes;
                    modListRoutine.ViewResourceImageList(lisResource, '1', cdvFactory.Text);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.btnAdd_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void btnDel_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (CheckCondition("DELETE") == false)
				{
					return;
				}
				if (Update_Resource_Image(modGlobalConstant.MP_STEP_DELETE) == true)
				{
                    CmnInitFunction.InitListView(lisResource);
                    lisResource.SmallImageList = imlRes;
                    modListRoutine.ViewResourceImageList(lisResource, '1', cdvFactory.Text);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBSetupResourceImage.btnDel_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		#endregion
		
	}
	
}
