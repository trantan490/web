
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Miracom.FMBUI;
using Miracom.FMBUI.Controls;
using Infragistics.Win.UltraWinToolbars;
using System.ComponentModel;
using Microsoft.VisualBasic;
using Miracom.SmartWeb.FWX;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : udcFMBDesign.vb
//   Description : FMB Design
//
//   FMB Version : 1.0.0
//
//   Function List
//       - GetTagNumber() : Get tag's name
//       - SelectedControlsCount() : Get selected controls count
//
//   Detail Description
//       -
//
//   History
//       - 2005-01-21 : Created by Laverwon
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
	public class udcFMBDesign : System.Windows.Forms.Panel
    {


        #region " 구성 요소 디자이너에서 생성한 코드 "

        //컨트롤 디자이너에 필요합니다.
        private System.ComponentModel.Container components = null;
        internal System.Windows.Forms.Panel pnlTracker;

        // 참고: 다음 프로시저는 구성 요소 디자이너에 필요합니다.
        // 구성 요소 디자이너를 사용하여 수정할 수 있습니다.  코드 편집기를
        // 사용하여 수정하지 마십시오.
        internal System.Windows.Forms.ContextMenu mnuFMBDesign;
		internal System.Windows.Forms.MenuItem mnuPasteTag;
		internal System.Windows.Forms.MenuItem mnuSetDefault;
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
			this.pnlTracker = new System.Windows.Forms.Panel();
			this.mnuFMBDesign = new System.Windows.Forms.ContextMenu();
			this.mnuFMBDesign.Popup += new System.EventHandler(mnuFMBDesign_Popup);
			this.SuspendLayout();
			//
			//pnlTracker
			//
			this.pnlTracker.BackColor = System.Drawing.Color.Transparent;
			this.pnlTracker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlTracker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.pnlTracker.ForeColor = System.Drawing.Color.Transparent;
			this.pnlTracker.Location = new System.Drawing.Point(4, 4);
			this.pnlTracker.Name = "pnlTracker";
			this.pnlTracker.Size = new System.Drawing.Size(4, 4);
			this.pnlTracker.TabIndex = 0;
			this.pnlTracker.Visible = false;
			//
			//udcFMBDesign
			//
			this.Controls.Add(this.pnlTracker);
			this.ResumeLayout(false);
			
		}
		
		#endregion
		
		#region " Properties Implemations"

        internal ArrayList SelectedControls = new ArrayList();
        //internal ControlCollection SelectedControls;
        internal udcCtrlBase ClipboardControl;
		private FMBDesign m_ParentForm = null;
		private bool m_bDraging = false;
		
		[Description("Gets or sets IsDraging"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]private bool IsDraging
		{
			get
			{
				return m_bDraging;
			}
			set
			{

                if (m_bDraging.Equals(value) == false)
				{
                    m_bDraging = value;
				}
			}
		}
		
		#endregion
		
		#region " Event Implematations"

        public udcFMBDesign(FMBDesign parentForm)
        {

            // 이 호출은 구성 요소 디자이너에 필요합니다.
            InitializeComponent();

            //InitializeComponent()를 호출한 다음에 초기화 작업을 추가하십시오.
            this.mnuPasteTag = new System.Windows.Forms.MenuItem();
            this.mnuPasteTag.Click += new System.EventHandler(mnuPasteTag_Click);
            this.mnuSetDefault = new System.Windows.Forms.MenuItem();
            this.mnuSetDefault.Click += new System.EventHandler(mnuSetDefault_Click);

            //
            //mnuPasteTag
            //
            this.mnuPasteTag.Index = 1;
            this.mnuPasteTag.MergeOrder = 1;
            this.mnuPasteTag.Text = modLanguageFunction.FindLanguage("Paste Tag", 2);

            //
            //mnuPasteTag
            //
            this.mnuSetDefault.Index = 0;
            this.mnuSetDefault.MergeOrder = 1;
            this.mnuSetDefault.Text = modLanguageFunction.FindLanguage("Set Default Layout", 2);

            this.mnuFMBDesign.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] { this.mnuSetDefault, this.mnuPasteTag });

            this.ContextMenu = this.mnuFMBDesign;

            m_ParentForm = parentForm;

            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ContainerControl, true);
            this.SetAutoScrollMargin(20, 20);
        }

        //Control은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs pe)
		{
			base.OnPaint(pe);
			
			try
			{
				if (m_ParentForm.IsDesignMode == true)
				{
					Graphics g = pe.Graphics;
					g.FillRectangle(new System.Drawing.Drawing2D.HatchBrush(System.Drawing.Drawing2D.HatchStyle.DottedGrid, Color.Gainsboro, Color.WhiteSmoke), pe.ClipRectangle);
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_Paint()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		protected override void OnGotFocus(System.EventArgs e)
		{
			base.OnGotFocus(e);
			
			try
			{
				int i;
                for (i = SelectedControlsCount(); i > 0; i--)
				{
                    ((udcCtrlBase)SelectedControls[i - 1]).IsHot = false;
                    ((udcCtrlBase)SelectedControls[i - 1]).IsFocused = false;
                    ((udcCtrlBase)SelectedControls[i - 1]).IsSelected = false;
                    ((udcCtrlBase)SelectedControls[i - 1]).IsPressed = false;
                    ((udcCtrlBase)SelectedControls[i - 1]).RedrawCtrl();
                    SelectedControls.Remove((udcCtrlBase)SelectedControls[i - 1]);
                }
				
				// Format Menu 珥덇린??
                //m_ParentForm.utmFMBDesign.Tools["kLefts"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kCenters"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kRights"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kTops"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kMiddles"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kBottoms"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kWidth"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kHeight"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kBoth"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kHMakeEqual"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kHIncrease"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kHDecrease"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kHRemove"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kVMakeEqual"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kVIncrease"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kVDecrease"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kVRemove"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kBringToFront"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kSendToBack"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kUpdateResTag"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kDeleteResTag"].SharedProps.Enabled = false;
                //m_ParentForm.utmFMBDesign.Tools["kPropertiesResTag"].SharedProps.Enabled = false;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_GotFocus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			
			try
			{
				//If Me.Focus = True Then
				//    OnGotFocus(Nothing)
				//Else
				//    Me.Select()
				//End If
				
				OnGotFocus(null);
				
				if (m_ParentForm.IsDesignMode == false)
				{
					return;
				}
				
				IsDraging = true;
				pnlTracker.Location = new Point(e.X, e.Y);
				pnlTracker.Width = 0;
				pnlTracker.Height = 0;
				pnlTracker.SendToBack();
				pnlTracker.Visible = true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_MouseDown()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			
			try
			{
				if (m_ParentForm.IsDesignMode == false)
				{
					return;
				}
				
				if (IsDraging == true && pnlTracker.Width > 0 && pnlTracker.Height > 0)
				{
					Rectangle rcTracker = new Rectangle(pnlTracker.Location.X, pnlTracker.Location.Y, pnlTracker.Width, pnlTracker.Height);
					Control ctrl;
                    string sCtrlType = null;

					foreach (Control tempLoopVar_ctrl in this.Controls)
					{
						ctrl = tempLoopVar_ctrl;
						if (ctrl is udcCtrlBase)
						{
							if (rcTracker.IntersectsWith(new Rectangle(((udcCtrlBase) ctrl).GetLocation().X, ((udcCtrlBase) ctrl).GetLocation().Y, ((udcCtrlBase) ctrl).GetSize().Width, ((udcCtrlBase) ctrl).GetSize().Height)) == true)
							{
								((udcCtrlBase) ctrl).IsHot = true;
								((udcCtrlBase) ctrl).IsSelected = true;
								((udcCtrlBase) ctrl).IsFocused = false;
								((udcCtrlBase) ctrl).IsDesignMode = true;
								// Resource Type / Tag Type ?뺤쓽
								if (((udcCtrlBase) ctrl).CtrlStatus.ToolType == Miracom.FMBUI.Enums.eToolType.Resource)
								{
                                    sCtrlType = "R:";
								}
								else
								{
                                    sCtrlType = "T:";
								}
                                
                                SelectedControls.Add(ctrl);
								((udcCtrlBase) ctrl).RedrawCtrl();
							}
						}
					}
                    if (SelectedControlsCount() > 0)
					{
                        ctrl = (udcCtrlBase)SelectedControls[SelectedControlsCount() - 1];
                        ctrl.Select();
						((udcCtrlBase) ctrl).RedrawCtrl();
					}
				}
				
				IsDraging = false;
				pnlTracker.Visible = false;
				pnlTracker.Width = 0;
				pnlTracker.Height = 0;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_MouseUp()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			
			try
			{
				if (m_ParentForm.IsDesignMode == false)
				{
					return;
				}
				
				if (IsDraging == true)
				{
					pnlTracker.Width = e.X - pnlTracker.Location.X;
					pnlTracker.Height = e.Y - pnlTracker.Location.Y;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_MouseMove()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		protected override void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)
		{
			
			try
			{
				if (m_ParentForm.IsDesignMode == false)
				{
					Cursor.Current = Cursors.No;
					return;
				}
				
				if (drgevent.Effect == DragDropEffects.Copy || drgevent.Effect == DragDropEffects.Move)
				{
					
					if (modGlobalVariable.giToolType <= ListBox.NoMatches)
					{
						return;
					}
					
					//string sKey = "";
					//string sText = "";
					Size szRegion = new Size();
					string sFactory = "";
                    if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						sFactory = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 1);
					}
                    else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						sFactory = GlobalVariable.gsFactory;
					}
                    if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Resource))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultResourceSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultResourceSize)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Rectangle))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultRectangleSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultRectangleSize)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Ellipse))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultEllipseSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultEllipseSize)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Triangle))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultTriangleSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultTriangleSize)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.VerticalLine))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultVerticalLineSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultVerticalLineSize)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.HorizontalLine))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultHorizontalLineSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultHorizontalLineSize)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.PieType1))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie1Size)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie1Size)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.PieType2))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie2Size)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie2Size)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.PieType3))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie3Size)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie3Size)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.PieType4))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie4Size)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultPie4Size)).Height);
					}
					else if (modGlobalVariable.giToolType == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.TextType))
					{
						szRegion = new Size(((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultRectangleSize)).Width, ((Size) modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultRectangleSize)).Height);
					}
					
					Point ptLocation = this.PointToClient(new Point(drgevent.X, drgevent.Y));
					ptLocation.X -= szRegion.Width / 2;
					ptLocation.Y -= szRegion.Height / 2;
					
					if (ptLocation.X < 0)
					{
						ptLocation.X = 0;
					}
					if (ptLocation.Y < 0)
					{
						ptLocation.Y = 0;
					}
					
					if (modGlobalVariable.giToolType > ListBox.NoMatches)
					{
						switch (modGlobalVariable.giToolType)
						{
							case 0 : //CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.Resource):
								
								frmFMBCreateResource form = new frmFMBCreateResource(modGlobalConstant.MP_STEP_CREATE);
								form.Tag = m_ParentForm.Tag;
								if (!(modGlobalVariable.gNodeSelectedRes == null))
								{
									form.cdvResID.Text = ((clsDesignListTag) modGlobalVariable.gNodeSelectedRes.Tag).Key;
								}
                                if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
								{
									form.txtFactory.Text = sFactory;
									form.cdvLayOut.Text = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 2);
								}
                                else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
								{
									form.txtFactory.Text = sFactory;
									form.cdvLayOut.Text = m_ParentForm.Name;
								}
								else
								{
									CmnFunction.ShowMsgBox("pnlFMBDesign_DragDrop() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
									return;
								}
								form.txtX.Text = ptLocation.X.ToString();
								form.txtY.Text = ptLocation.Y.ToString();
								form.txtWidth.Text = szRegion.Width.ToString();
								form.txtHeight.Text = szRegion.Height.ToString();
								if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
								{
									clsCtrlStatus ResourceStatus = new clsCtrlStatus();
									ResourceStatus.Key = form.cdvResID.Text;
									ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(form.txtX.Text), CmnFunction.ToInt(form.txtY.Text)));
									ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form.txtWidth.Text), CmnFunction.ToInt(form.txtHeight.Text)));
									ResourceStatus.Text = form.txtText.Text;
									ResourceStatus.ResTagFlag = form.txtResTagFlag.Text;
									if (form.utcText.Color.IsSystemColor == true || form.utcText.Color.IsKnownColor == true)
									{
										ResourceStatus.TextColor = CmnFunction.ToInt(form.utcText.Color.ToKnownColor());
									}
									else
									{
										ResourceStatus.TextColor = form.utcText.Color.ToArgb();
									}
									ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
									if (form.cboSize.Text == "")
									{
										ResourceStatus.TextSize = 0;
									}
									else
									{
										ResourceStatus.TextSize = CmnFunction.ToInt(form.cboSize.Text);
									}
									ResourceStatus.TextStyle = form.cboTextStyle.SelectedIndex;
									ResourceStatus.ToolType =  (Enums.eToolType)modGlobalVariable.giToolType;
									ResourceStatus.LastEvent = form.txtLastEvent.Text;
									ResourceStatus.PrimaryStatus = form.txtPriSts.Text;
									ResourceStatus.ProcMode = form.txtProcMode.Text;
									if (form.txtCtrlMode.Text == "ON LINE")
									{
										ResourceStatus.CtrlMode = "OL";
									}
									else if (form.txtCtrlMode.Text == "ON LINE REAL")
									{
										ResourceStatus.CtrlMode = "OR";
									}
									else if (form.txtCtrlMode.Text == "OFF LINE")
									{
										ResourceStatus.CtrlMode = "OF";
									}
									ResourceStatus.ResourceType = form.txtResourceType.Text;
									ResourceStatus.UpDownFlag = form.txtUpDown.Text;
									ResourceStatus.AreaID = form.txtArea.Text;
									ResourceStatus.SubAreaID = form.txtSubArea.Text;
                                    if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.IsProcessMode)) == "P")
									{
										ResourceStatus.IsProcessMode = true;
									}
									else
									{
										ResourceStatus.IsProcessMode = false;
									}
                                    if (System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.UseEventColor)) == "Y")
									{
										ResourceStatus.IsUseEventColor = true;
										ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
									}
									else
									{
										ResourceStatus.IsUseEventColor = false;
										ResourceStatus.EventBackColor = modGlobalVariable.gGlobalOptions.GetOptions(sFactory, ResourceStatus.LastEvent).ToArgb();
									}
									if (form.utcBack.Color.IsSystemColor == true || form.utcBack.Color.IsKnownColor == true)
									{
										ResourceStatus.BackColor = CmnFunction.ToInt(form.utcBack.Color.ToKnownColor());
									}
									else
									{
										ResourceStatus.BackColor = form.utcBack.Color.ToArgb();
									}
									ResourceStatus.ImageIndex = form.iImageIndex;
									ResourceStatus.IsViewSignal = form.chkSignalFlag.Checked;
									if (m_ParentForm.AddControl(ResourceStatus, true, false) == false)
									{
										return;
									}
								}
								break;
							default:
								
								frmFMBCreateTag form1 = new frmFMBCreateTag(modGlobalConstant.MP_STEP_CREATE);
								form1.Tag = m_ParentForm.Tag;
                                if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
								{
									form1.txtFactory.Text = sFactory;
									form1.txtLayOut.Text = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 2);
								}
                                else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
								{
									form1.txtFactory.Text = sFactory;
									form1.txtLayOut.Text = m_ParentForm.Name;
								}
								else
								{
									CmnFunction.ShowMsgBox("pnlFMBDesign_DragDrop() Failed.", "FMB Client", MessageBoxButtons.OK, 1);
									return;
								}
								int iTagNumber;
								iTagNumber = GetTagNumber("Tag");
								form1.txtTagID.Text = "Tag" + iTagNumber.ToString();
								form1.txtX.Text = ptLocation.X.ToString();
								form1.txtY.Text = ptLocation.Y.ToString();
								form1.txtWidth.Text = szRegion.Width.ToString();
								form1.txtHeight.Text = szRegion.Height.ToString();
								form1.SelectedShapeIndex = modGlobalVariable.giToolType - 1;
								if (form1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
								{
									clsCtrlStatus ResourceStatus = new clsCtrlStatus();
									ResourceStatus.Key = form1.txtTagID.Text;
									ResourceStatus.SetLocation(new Point(CmnFunction.ToInt(form1.txtX.Text), CmnFunction.ToInt(form1.txtY.Text)));
									if (form1.cboShape.SelectedIndex == CmnFunction.ToInt(Miracom.FMBUI.Enums.eToolType.VerticalLine) - 1)
									{
										ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form1.txtHeight.Text), CmnFunction.ToInt(form1.txtWidth.Text)));
									}
									else
									{
										ResourceStatus.SetSize(new Size(CmnFunction.ToInt(form1.txtWidth.Text), CmnFunction.ToInt(form1.txtHeight.Text)));
									}
									ResourceStatus.Text = form1.txtText.Text;
									ResourceStatus.ResTagFlag = form1.txtResTagFlag.Text;
									if (form1.utcText.Color.IsSystemColor == true || form1.utcText.Color.IsKnownColor == true)
									{
										ResourceStatus.TextColor = CmnFunction.ToInt(form1.utcText.Color.ToKnownColor());
									}
									else
									{
										ResourceStatus.TextColor = form1.utcText.Color.ToArgb();
									}
									if (form1.utcBack.Color.IsSystemColor == true || form1.utcBack.Color.IsKnownColor == true)
									{
										ResourceStatus.BackColor = CmnFunction.ToInt(form1.utcBack.Color.ToKnownColor());
									}
									else
									{
										ResourceStatus.BackColor = form1.utcBack.Color.ToArgb();
									}
									ResourceStatus.TextFontName = System.Convert.ToString(modGlobalVariable.gGlobalOptions.GetOptions(sFactory, clsOptionData.Options.DefaultFontName));
									if (form1.cboSize.Text == "")
									{
										ResourceStatus.TextSize = 0;
									}
									else
									{
										ResourceStatus.TextSize = CmnFunction.ToInt(form1.cboSize.Text);
									}
									ResourceStatus.TextStyle = form1.cboTextStyle.SelectedIndex;
									ResourceStatus.ToolType = (Miracom.FMBUI.Enums.eToolType)form1.cboShape.SelectedIndex + 1;
									ResourceStatus.IsNoEvent = form1.chkNoMouseEvent.Checked;
									if (m_ParentForm.AddControl(ResourceStatus, true, false) == false)
									{
										return;
									}
								}
								break;
						}
						
						modGlobalVariable.gNodeSelectedRes = null;
						modGlobalVariable.giToolType = ListBox.NoMatches;
						
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_DragDrop()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		protected override void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)
		{
			
			try
			{
				if (m_ParentForm.IsDesignMode == false)
				{
					Cursor.Current = Cursors.No;
					drgevent.Effect = DragDropEffects.None;
					return;
				}
				
				if (modGlobalVariable.giToolType == ListBox.NoMatches)
				{
					Cursor.Current = Cursors.No;
					drgevent.Effect = DragDropEffects.None;
					return;
				}
				
				if (!(modGlobalVariable.gNodeSelectedRes == null))
				{
                    if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						if (((clsDesignListTag) modGlobalVariable.gNodeSelectedRes.Tag).Factory != modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 1))
						{
							Cursor.Current = Cursors.No;
							drgevent.Effect = DragDropEffects.None;
							return;
						}
					}
                    else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						Cursor.Current = Cursors.No;
						drgevent.Effect = DragDropEffects.None;
						return;
					}
					else
					{
						Cursor.Current = Cursors.No;
						drgevent.Effect = DragDropEffects.None;
						return;
					}
				}
				
				// Set the effect based upon the KeyState.
				if ((drgevent.KeyState & 40 ) ==(8 + 32) &&
                    (drgevent.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
				{
					// KeyState 8 + 32 = CTL + ALT
					
					// Link drag and drop effect.
					drgevent.Effect = DragDropEffects.Link;
					
				}
				else if ((drgevent.KeyState & 32) == 32 &&(drgevent.AllowedEffect & DragDropEffects.Link) == DragDropEffects.Link)
				{
					
					// ALT KeyState for link.
					drgevent.Effect = DragDropEffects.Link;
					
				}
				else if ((drgevent.KeyState & 4) == 4 &&(drgevent.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
				{
					
					// SHIFT KeyState for move.
					drgevent.Effect = DragDropEffects.Move;
					
				}
				else if ((drgevent.KeyState & 8) == 8 &&(drgevent.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
				{
					
					// CTL KeyState for copy.
					drgevent.Effect = DragDropEffects.Copy;
					
				}
				else if ((drgevent.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
				{
					
					// By default, the drop action should be move, if allowed.
					drgevent.Effect = DragDropEffects.Move;
					
				}
				else
				{
					drgevent.Effect = DragDropEffects.None;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_DragOver()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		~udcFMBDesign()
		{
			
			SelectedControls = null;
			Control ctrl;
			foreach (Control tempLoopVar_ctrl in this.Controls)
			{
				ctrl = tempLoopVar_ctrl;
				ctrl = null;
			}
			
			//base.Finalize();
			
		}
		
		private void mnuPasteTag_Click(System.Object sender, System.EventArgs e)
		{
			
			try
			{
				if (!(ClipboardControl == null))
				{
					clsCtrlStatus ResourceStatus = new clsCtrlStatus();
					ResourceStatus.Key = ClipboardControl.Name + "_" + GetTagNumber(ClipboardControl.Name + "_").ToString();
					ResourceStatus.SetLocation(PointToClient(MousePosition));
					ResourceStatus.Size = ClipboardControl.CtrlStatus.Size;
					ResourceStatus.ResTagFlag = ClipboardControl.CtrlStatus.ResTagFlag;
					ResourceStatus.ResourceType = ClipboardControl.CtrlStatus.ResourceType;
					ResourceStatus.Text = ClipboardControl.CtrlStatus.Text;
					ResourceStatus.TextColor = ClipboardControl.CtrlStatus.TextColor;
					ResourceStatus.BackColor = ClipboardControl.CtrlStatus.BackColor;
					ResourceStatus.TextFontName = ClipboardControl.CtrlStatus.TextFontName;
					ResourceStatus.TextSize = ClipboardControl.CtrlStatus.TextSize;
					ResourceStatus.TextStyle = ClipboardControl.CtrlStatus.TextStyle;
					ResourceStatus.ToolType = ClipboardControl.CtrlStatus.ToolType;
					ResourceStatus.IsNoEvent = ClipboardControl.CtrlStatus.IsNoEvent;
					ResourceStatus.IsViewSignal = ClipboardControl.CtrlStatus.IsViewSignal;

                    string sFactory = ""; ;
					string sLayoutID = "";
					string sTagID = "";
					char sResType = ' ';
					string sText = "";
					int iTagType = 0;
					int iTextSize = 0;
					char sTextStyle = ' ';
					int iTextColor = 0;
					int iBackColor = 0;
					int iLocX = 0;
					int iLocY = 0;
					int iLocWidth = 0;
					int iLocHeight = 0;
					char sNoMouseEvent = ' ';
					char sSignalFlag = ' ';

                    if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						sFactory = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 1);
						sLayoutID = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 2);
					}
                    else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						sFactory = GlobalVariable.gsFactory;
						sLayoutID = m_ParentForm.Name;
					}
					
					sTagID = ResourceStatus.Key;
					sResType = ResourceStatus.ResTagFlag[0];
					sText = ResourceStatus.Text;
					iTagType = CmnFunction.ToInt(ResourceStatus.ToolType);
					iTextSize = ResourceStatus.TextSize;
					if (ResourceStatus.TextStyle < 0)
					{
						sTextStyle = '0';
					}
					else
					{
                        sTextStyle = (char)(ResourceStatus.TextStyle + '0');
					}
					iTextColor = ResourceStatus.TextColor;
					iBackColor = ResourceStatus.BackColor;
					iLocX = ResourceStatus.GetLocation().X;
					iLocY = ResourceStatus.GetLocation().Y;
					iLocWidth = ResourceStatus.GetSize().Width;
					iLocHeight = ResourceStatus.GetSize().Height;
					if (ResourceStatus.IsNoEvent == true)
					{
						sNoMouseEvent = 'Y';
					}
					else
					{
						sNoMouseEvent = ' ';
					}
					
					if (ResourceStatus.IsViewSignal == true)
					{
						sSignalFlag = 'Y';
					}
					else
					{
						sSignalFlag = ' ';
					}

                    if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
					{
						if (modCommonFunction.UpdateResourceLocation(modGlobalConstant.MP_STEP_CREATE, sFactory, sLayoutID, sTagID, sResType, sText, iTagType, iTextSize, sTextStyle, iTextColor, iBackColor, iLocX, iLocY, iLocWidth, iLocHeight, sNoMouseEvent, sSignalFlag) == false)
						{
							return;
						}
					}
                    else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
					{
						if (modCommonFunction.UpdateUDRResourceLocation(modGlobalConstant.MP_STEP_CREATE, sFactory, sLayoutID, sTagID, sResType, sText, iTagType, iTextSize, sTextStyle, iTextColor, iBackColor, iLocX, iLocY, iLocWidth, iLocHeight, sNoMouseEvent, sSignalFlag) == false)
						{
							return;
						}
					}
					
					if (m_ParentForm.AddControl(ResourceStatus, true, false) == false)
					{
						return;
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.mnuPasteTag_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void mnuFMBDesign_Popup(object sender, System.EventArgs e)
		{
			
			try
			{
				if (ClipboardControl == null)
				{
					mnuPasteTag.Visible = false;
				}
				else
				{
					if (m_ParentForm.IsDesignMode == true)
					{
						mnuPasteTag.Visible = true;
					}
					else
					{
						mnuPasteTag.Visible = false;
					}
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.mnuFMBDesign_Popup()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		private void mnuSetDefault_Click(object sender, System.EventArgs e)
		{
			
			string sFactory = "";
			string sLayoutID = "";
			try
			{
				if (CmnFunction.ShowMsgBox(modLanguageFunction.GetMessage(32), "FMB Client", MessageBoxButtons.YesNo, 1) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}

                if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_LAYOUT)
				{
					sFactory = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 1);
					sLayoutID = modCommonFunction.GetStringBySeperator(m_ParentForm.Name, ":", 2);
				}
                else if (System.Convert.ToString(m_ParentForm.Tag) == modGlobalConstant.FMB_CATEGORY_GROUP)
				{
					sFactory = GlobalVariable.gsFactory;
					sLayoutID = m_ParentForm.Name;
				}
                //Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "DefaultLayout", "Category", System.Convert.ToString(m_ParentForm.Tag));
                //Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "DefaultLayout", "Factory", sFactory);
                //Microsoft.VisualBasic.Interaction.SaveSetting(Application.ProductName, "DefaultLayout", "Layout", sLayoutID);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.mnuSetDefault_Click()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		#endregion
		
		#region " Functions Implementations"
		
		// GetTagNumber()
		//       - Get Tag Defalut Number
		// Return Value
		//       - Integer
		// Arguments
		//		- Optional ByVal sName As String = "Tag"
		//
		public int GetTagNumber(string sName)
		{
			
			try
			{
				int i;
				string sSubstring1;
				string sSubstring2;
				int iNextNumber = 1;
				ArrayList arrTag = new ArrayList();
				ArrayList arrNumberList = new ArrayList();
				Control ctrl;
				foreach (Control tempLoopVar_ctrl in this.Controls)
				{
					ctrl = tempLoopVar_ctrl;
					if (ctrl is udcCtrlTag)
					{
						arrTag.Add(((udcCtrlTag) ctrl).Name);
					}
				}
				
				for (i = 0; i <= arrTag.Count - 1; i++)
				{
					if (System.Convert.ToString(arrTag[i]).Length > sName.Length)
					{
						sSubstring1 = System.Convert.ToString(arrTag[i]).Substring(0, sName.Length);
						sSubstring2 = System.Convert.ToString(arrTag[i]).Substring(sName.Length);
                        if (sSubstring1 == sName && Microsoft.VisualBasic.Information.IsNumeric(sSubstring2) == true)
						{
							arrNumberList.Add(CmnFunction.ToInt(sSubstring2));
						}
					}
				}
				
				arrNumberList.Sort();
				
				while (true)
				{
					if (arrNumberList.BinarySearch(iNextNumber) < 0)
					{
						return iNextNumber;
					}
					iNextNumber++;
				}
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.GetTagNumber()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return 0;
			}
			
		}
		
		// SelectedControlsCount()
		//       - Get selected controls count
		// Return Value
		//       - Integer
		// Arguments
		//		- Optional ByVal sName As String = "Tag"
		//
		public int SelectedControlsCount()
		{
			
			try
			{
				if (SelectedControls == null)
				{
					return 0;
				}
				
				return SelectedControls.Count;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.SelectedControlsCount()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return 0;
			}
			
		}
		
		// SetFocus()
		//       - Set Focus
		// Return Value
		//       -
		// Arguments
		//		- ByVal e As System.EventArgs
		//
		public void SetFocus(System.EventArgs e)
		{
			
			try
			{
				OnGotFocus(e);
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("frmFMBDesign.pnlFMBDesign_GotFocus()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		#endregion
		
		
	}
	
}
