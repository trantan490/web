using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System;
using System.Threading;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Drawing2D;

namespace Miracom.FMBUI
{
	namespace Controls
	{
		
		public class udcCtrlBase : System.Windows.Forms.UserControl, ISupportInitialize
        {
            delegate void SetToolTipCallback(string text);
            private Thread demoThread = null;

            #region " Windows Form µ¿⁄¿Ã≥ ø°º≠ ª˝º∫«— ƒ⁄µÂ "

            public udcCtrlBase()
			{
				
				m_CtrlStatus = new clsCtrlStatus();
//				m_CtrlStatus.StyleChanged += new Miracom.FMBUI.clsCtrlStatus.CtrlStyleChangedEventHandler(new CtrlStyleChangedEventHandler( this.OnCtrlStyleChanged));
                m_CtrlStatus.StyleChanged += new CtrlStyleChangedEventHandler(new CtrlStyleChangedEventHandler(this.OnCtrlStyleChanged));
				
				//???∏Ï∂ú?Ä Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
				InitializeComponent();
				
				//InitializeComponent()Î•??∏Ï∂ú???§Ïùå??Ï¥àÍ∏∞???ëÏóÖ??Ï∂îÍ??òÏã≠?úÏò§.
            }
			
			//UserControl1?Ä DisposeÎ•??¨Ï†ï?òÌïò??Íµ¨ÏÑ± ?îÏÜå Î™©Î°ù???ïÎ¶¨?©Îãà??
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

            private IContainer components;

            //Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			
			//Ï∞∏Í≥†: ?§Ïùå ?ÑÎ°ú?úÏ???Windows Form ?îÏûê?¥ÎÑà???ÑÏöî?©Îãà??
			//Windows Form ?îÏûê?¥ÎÑàÎ•??¨Ïö©?òÏó¨ ?òÏ†ï?????àÏäµ?àÎã§.
			//ÏΩîÎìú ?∏ÏßëÍ∏∞Î? ?¨Ïö©?òÏó¨ ?òÏ†ï?òÏ? ÎßàÏã≠?úÏò§.
			internal System.Windows.Forms.ContextMenu mnuCtrlBase;
			internal System.Windows.Forms.MenuItem mnuUpdate;
			internal System.Windows.Forms.MenuItem mnuDelete;
			internal System.Windows.Forms.MenuItem mnuProperties;
			internal System.Windows.Forms.MenuItem mnuSeperator1;
			internal System.Windows.Forms.MenuItem mnuViewResourceStatus;
			internal System.Windows.Forms.MenuItem mnuViewResourceHistory;
			internal System.Windows.Forms.MenuItem mnuTranEvent;
			public System.Windows.Forms.ToolTip tipCtrlInfo;
            internal System.Windows.Forms.MenuItem mnuCopyTag;
			internal System.Windows.Forms.MenuItem mnuSeperator2;
			[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
			{
                this.components = new System.ComponentModel.Container();
                this.tipCtrlInfo = new System.Windows.Forms.ToolTip(this.components);
                this.mnuCtrlBase = new System.Windows.Forms.ContextMenu();
                this.mnuUpdate = new System.Windows.Forms.MenuItem();
                this.mnuDelete = new System.Windows.Forms.MenuItem();
                this.mnuProperties = new System.Windows.Forms.MenuItem();
                this.mnuSeperator1 = new System.Windows.Forms.MenuItem();
                this.mnuViewResourceStatus = new System.Windows.Forms.MenuItem();
                this.mnuViewResourceHistory = new System.Windows.Forms.MenuItem();
                this.mnuTranEvent = new System.Windows.Forms.MenuItem();
                this.mnuSeperator2 = new System.Windows.Forms.MenuItem();
                this.mnuCopyTag = new System.Windows.Forms.MenuItem();
                this.SuspendLayout();
                // 
                // tipCtrlInfo
                // 
                this.tipCtrlInfo.AutoPopDelay = 5000;
                this.tipCtrlInfo.InitialDelay = 500;
                this.tipCtrlInfo.ReshowDelay = 100;
                this.tipCtrlInfo.ShowAlways = true;
                // 
                // mnuCtrlBase
                // 
                this.mnuCtrlBase.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuUpdate,
            this.mnuDelete,
            this.mnuProperties,
            this.mnuSeperator1,
            this.mnuViewResourceStatus,
            this.mnuViewResourceHistory,
            this.mnuTranEvent,
            this.mnuSeperator2,
            this.mnuCopyTag});
                this.mnuCtrlBase.Popup += new System.EventHandler(this.mnuCtrlBase_Popup);
                // 
                // mnuUpdate
                // 
                this.mnuUpdate.Index = 0;
                this.mnuUpdate.MergeOrder = 1;
                this.mnuUpdate.Text = "Update Resource/Tag";
                this.mnuUpdate.Click += new System.EventHandler(this.mnuUpdate_Click);
                // 
                // mnuDelete
                // 
                this.mnuDelete.Index = 1;
                this.mnuDelete.MergeOrder = 2;
                this.mnuDelete.Text = "Delete Resource/Tag";
                this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
                // 
                // mnuProperties
                // 
                this.mnuProperties.Index = 2;
                this.mnuProperties.MergeOrder = 3;
                this.mnuProperties.Text = "Properties Resource/Tag";
                this.mnuProperties.Click += new System.EventHandler(this.mnuProperties_Click);
                // 
                // mnuSeperator1
                // 
                this.mnuSeperator1.Index = 3;
                this.mnuSeperator1.Text = "-";
                // 
                // mnuViewResourceStatus
                // 
                this.mnuViewResourceStatus.Index = 4;
                this.mnuViewResourceStatus.MergeOrder = 4;
                this.mnuViewResourceStatus.Text = "View Resource Status";
                this.mnuViewResourceStatus.Click += new System.EventHandler(this.mnuViewResourceStatus_Click);
                // 
                // mnuViewResourceHistory
                // 
                this.mnuViewResourceHistory.Index = 5;
                this.mnuViewResourceHistory.MergeOrder = 5;
                this.mnuViewResourceHistory.Text = "View Resource History";
                this.mnuViewResourceHistory.Click += new System.EventHandler(this.mnuViewResourceHistory_Click);
                // 
                // mnuTranEvent
                // 
                this.mnuTranEvent.Index = 6;
                this.mnuTranEvent.MergeOrder = 6;
                this.mnuTranEvent.Text = "Event";
                this.mnuTranEvent.Click += new System.EventHandler(this.mnuTranEvent_Click);
                // 
                // mnuSeperator2
                // 
                this.mnuSeperator2.Index = 7;
                this.mnuSeperator2.Text = "-";
                // 
                // mnuCopyTag
                // 
                this.mnuCopyTag.Index = 8;
                this.mnuCopyTag.MergeOrder = 7;
                this.mnuCopyTag.Text = "Copy Tag";
                this.mnuCopyTag.Click += new System.EventHandler(this.mnuCopyTag_Click);
                // 
                // udcCtrlBase
                // 
                this.AllowDrop = true;
                this.BackColor = System.Drawing.SystemColors.Control;
                this.ContextMenu = this.mnuCtrlBase;
                this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.Name = "udcCtrlBase";
                this.Size = new System.Drawing.Size(100, 100);
                this.Load += new System.EventHandler(this.udcCtrlBase_Load);
                this.ResumeLayout(false);

			}
			
			#endregion
			
			#region " Property Implementations"

			private Point m_ptStartPos = Point.Empty;
			private int m_iTracker = 0;
			
			private bool m_bHot = false; // Control???ÑÏû¨ Hot ?ÅÌÉú?∏Ï? ?¨Î?
			private bool m_bSelected = false; // Control???ÑÏû¨ Selected ?ÅÌÉú?∏Ï? ?¨Î?
			private bool m_bFocused = false; // Control???ÑÏû¨ Selected?¥Î©¥??Í∞Ä??ÏµúÍ∑º???†ÌÉù???ÅÌÉú?∏Ï? ?¨Î?
			private bool m_bPressed = false; // Control???ÑÏû¨ Pressed ?ÅÌÉú?∏Ï? ?¨Î?
			private bool m_bModified = false; // Control???òÏ†ï?òÏóà?îÏ? ?¨Î?
			private bool m_bRefreshed = false; // Control??Refresh?òÏóà?îÏ? ?¨Î?
			
			public bool CanLostFocus = true;
			public bool CanGotFocus = true;
			public bool CanMouseDown = true;
			public bool CanMouseUp = true;
			public bool CanMouseMove = true;
			public bool CanKeyDown = true;
			
			private Rectangle m_rcCtrlPos;
			private Rectangle m_rcCtrlOriginalPos;
			
			protected bool m_bIniting = false;
			protected clsCtrlStatus m_CtrlStatus = null;
			
			private bool m_bDesignMode = false;
			
			[Description("Gets or sets IsHot"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsDesignMode
			{
				get
				{
					return m_bDesignMode;
				}
				set
				{
					if (m_bDesignMode.Equals(value) == false)
					{
						m_bDesignMode = value;
					}
				}
			}
			
			[Description("Gets IsMouseInControl"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsMouseInControl
			{
				get
				{
					Point ptPos = Control.MousePosition;
					bool bRet = this.ClientRectangle.Contains(this.PointToClient(ptPos));
					return bRet;
				}
			}
			
			[Description("Gets or sets IsHot"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsHot
			{
				get
				{
					if (CtrlStatus.IsNoEvent == true && IsDesignMode == false)
					{
						return false;
					}
					else
					{
						return m_bHot;
					}
				}
				set
				{
					if (m_bHot.Equals(value) == false)
					{
						m_bHot = value;
						SetModified(true);
					}
				}
			}
			
			[Description("Gets or sets IsSelected"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsSelected
			{
				get
				{
					return m_bSelected;
				}
				set
				{
					if (m_bSelected.Equals(value) == false)
					{
						m_bSelected = value;
						SetModified(true);
					}
				}
			}
			
			[Description("Gets or sets IsFocused"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsFocused
			{
				get
				{
					return m_bFocused;
				}
				set
				{
					if (m_bFocused.Equals(value) == false)
					{
						m_bFocused = value;
						SetModified(true);
					}
				}
			}
			
			[Description("Gets or sets IsPressed"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsPressed
			{
				get
				{
					return m_bPressed;
				}
				set
				{
					if (m_bPressed.Equals(value) == false)
					{
						m_bPressed = value;
						SetModified(true);
					}
				}
			}
			
			[Description("Gets or sets IsModified"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsModified
			{
				get
				{
					return m_bModified;
				}
				set
				{
					if (m_bModified.Equals(value) == false)
					{
						m_bModified = value;
					}
				}
			}
			
			[Description("Gets or sets CtrlPos"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public Rectangle CtrlPos
			{
				get
				{
					return m_rcCtrlPos;
				}
				set
				{
					if (m_rcCtrlPos.Equals(value) == false)
					{
						m_rcCtrlPos = value;
					}
				}
			}
			
			[Description("Gets or sets CtrlOriginalPos"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public Rectangle CtrlOriginalPos
			{
				get
				{
					return m_rcCtrlOriginalPos;
				}
				set
				{
					if (m_rcCtrlOriginalPos.Equals(value) == false)
					{
						m_rcCtrlOriginalPos = value;
					}
				}
			}
			
			[Description("Gets or sets CtrlStatus"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]public clsCtrlStatus CtrlStatus
			{
				get
				{
					return m_CtrlStatus;
				}
				set
				{
					if (m_CtrlStatus.Equals(value) == false)
					{
						m_CtrlStatus = value;
					}
				}
			}
			
			[Description("Gets or sets IsRefreshed"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]public bool IsRefreshed
			{
				get
				{
					return m_bRefreshed;
				}
				set
				{
					if (m_bRefreshed.Equals(value) == false)
					{
						m_bRefreshed = value;
					}
				}
			}
			#endregion
			
			#region " Event Implementations"
			
			private CtrlPressedEventHandler ButtonPressedEvent;
			public event CtrlPressedEventHandler ButtonPressed
			{
				add
				{
					ButtonPressedEvent = (CtrlPressedEventHandler) System.Delegate.Combine(ButtonPressedEvent, value);
				}
				remove
				{
					ButtonPressedEvent = (CtrlPressedEventHandler) System.Delegate.Remove(ButtonPressedEvent, value);
				}
			}
			
			private CtrlMouseEnterEventHandler CtrlMouseEnterEvent;
			public event CtrlMouseEnterEventHandler CtrlMouseEnter
			{
				add
				{
					CtrlMouseEnterEvent = (CtrlMouseEnterEventHandler) System.Delegate.Combine(CtrlMouseEnterEvent, value);
				}
				remove
				{
					CtrlMouseEnterEvent = (CtrlMouseEnterEventHandler) System.Delegate.Remove(CtrlMouseEnterEvent, value);
				}
			}
			
			private CtrlMouseLeaveEventHandler CtrlMouseLeaveEvent;
			public event CtrlMouseLeaveEventHandler CtrlMouseLeave
			{
				add
				{
					CtrlMouseLeaveEvent = (CtrlMouseLeaveEventHandler) System.Delegate.Combine(CtrlMouseLeaveEvent, value);
				}
				remove
				{
					CtrlMouseLeaveEvent = (CtrlMouseLeaveEventHandler) System.Delegate.Remove(CtrlMouseLeaveEvent, value);
				}
			}
			
			private CtrlMouseDownEventHandler CtrlMouseDownEvent;
			public event CtrlMouseDownEventHandler CtrlMouseDown
			{
				add
				{
					CtrlMouseDownEvent = (CtrlMouseDownEventHandler) System.Delegate.Combine(CtrlMouseDownEvent, value);
				}
				remove
				{
					CtrlMouseDownEvent = (CtrlMouseDownEventHandler) System.Delegate.Remove(CtrlMouseDownEvent, value);
				}
			}
			
			private CtrlMouseUpEventHandler CtrlMouseUpEvent;
			public event CtrlMouseUpEventHandler CtrlMouseUp
			{
				add
				{
					CtrlMouseUpEvent = (CtrlMouseUpEventHandler) System.Delegate.Combine(CtrlMouseUpEvent, value);
				}
				remove
				{
					CtrlMouseUpEvent = (CtrlMouseUpEventHandler) System.Delegate.Remove(CtrlMouseUpEvent, value);
				}
			}
			
			private CtrlMouseMoveEventHandler CtrlMouseMoveEvent;
			public event CtrlMouseMoveEventHandler CtrlMouseMove
			{
				add
				{
					CtrlMouseMoveEvent = (CtrlMouseMoveEventHandler) System.Delegate.Combine(CtrlMouseMoveEvent, value);
				}
				remove
				{
					CtrlMouseMoveEvent = (CtrlMouseMoveEventHandler) System.Delegate.Remove(CtrlMouseMoveEvent, value);
				}
			}
			
			private CtrlGotFocusEventHandler CtrlGotFocusEvent;
			public event CtrlGotFocusEventHandler CtrlGotFocus
			{
				add
				{
					CtrlGotFocusEvent = (CtrlGotFocusEventHandler) System.Delegate.Combine(CtrlGotFocusEvent, value);
				}
				remove
				{
					CtrlGotFocusEvent = (CtrlGotFocusEventHandler) System.Delegate.Remove(CtrlGotFocusEvent, value);
				}
			}
			
			private CtrlLostFocusEventHandler CtrlLostFocusEvent;
			public event CtrlLostFocusEventHandler CtrlLostFocus
			{
				add
				{
					CtrlLostFocusEvent = (CtrlLostFocusEventHandler) System.Delegate.Combine(CtrlLostFocusEvent, value);
				}
				remove
				{
					CtrlLostFocusEvent = (CtrlLostFocusEventHandler) System.Delegate.Remove(CtrlLostFocusEvent, value);
				}
			}
			
			private CtrlKeyDownEventHandler CtrlKeyDownEvent;
			public event CtrlKeyDownEventHandler CtrlKeyDown
			{
				add
				{
					CtrlKeyDownEvent = (CtrlKeyDownEventHandler) System.Delegate.Combine(CtrlKeyDownEvent, value);
				}
				remove
				{
					CtrlKeyDownEvent = (CtrlKeyDownEventHandler) System.Delegate.Remove(CtrlKeyDownEvent, value);
				}
			}
			
			
			private CtrlContextMenuEventHandler CtrlContextMenuEvent;
			public event CtrlContextMenuEventHandler CtrlContextMenu
			{
				add
				{
					CtrlContextMenuEvent = (CtrlContextMenuEventHandler) System.Delegate.Combine(CtrlContextMenuEvent, value);
				}
				remove
				{
					CtrlContextMenuEvent = (CtrlContextMenuEventHandler) System.Delegate.Remove(CtrlContextMenuEvent, value);
				}
			}
			
			
			public void BeginInit()
			{
				
				m_bIniting = true;
				
				SetStyle(ControlStyles.ResizeRedraw, true);
				SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
				SetStyle(ControlStyles.Selectable, false);
				SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				
			}
			
			private void mnuUpdate_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message,  "udcCtrlBase.mnuUpdate_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void mnuDelete_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuDelete_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void mnuProperties_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuProperties_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void mnuViewResourceStatus_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuViewResourceStatus_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void mnuViewResourceHistory_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuViewResourceHistory_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void mnuCopyTag_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuCopyTag_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			public void EndInit()
			{
				
				m_bIniting = false;
				OnEndedInitialize();
				
			}
			
			public virtual void OnEndedInitialize()
			{
				
			}
			
			private void udcCtrlBase_Load(object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlStatus.IsSaveFlag = false;
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.udcCtrlBase_Load()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void OnCtrlStyleChanged(object sender, CtrlStyle_EventArgs e)
			{
				
				if (m_bIniting == true)
				{
					return;
				}
				
				OnCtrlStyleChanged(e);
				
			}
			
			protected virtual void OnCtrlStyleChanged(CtrlStyle_EventArgs e)
			{
				
				try
				{
					switch (e.PropertyName)
					{
						case Enums.eCtrlProperty.PROP_ISNOEVENT:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_ISPROCESSMODE:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_ISUSEEVENTCOLOR:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_FACTORY:
							
							break;
							
						case Enums.eCtrlProperty.PROP_ISUSERGROUPDESIGN:
							
							break;
							
						case Enums.eCtrlProperty.PROP_ISDELETERES:
							
							break;
							
						case Enums.eCtrlProperty.PROP_KEY:
							
							break;
							
						case Enums.eCtrlProperty.PROP_SIZE:
							
							break;
							
						case Enums.eCtrlProperty.PROP_LOCATION:
							
							break;
							
						case Enums.eCtrlProperty.PROP_TOOLTYPE:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_RESTAGFLAG:
							
							break;
							
						case Enums.eCtrlProperty.PROP_TEXT:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_TEXTCOLOR:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_TEXTSIZE:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_TEXTSTYLE:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_TEXTFONTNAME:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_PRIMARYSTATUS:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_UPDOWNFLAG:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_RESOURCETYPE:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_LASTEVENT:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_PROCMODE:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_CTRLMODE:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_BACKCOLOR:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_BACKHOTCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_BACKPRESSEDCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_BOARDERCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_BOARDERHOTCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_BOARDERPRESSEDCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_EVENTBACKCOLOR:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_EVENTBACKHOTCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_EVENTBACKPRESSEDCOLOR:
							
							break;
							
						case Enums.eCtrlProperty.PROP_IMAGEINDEX:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_ISVIEWSIGNAL:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_ZOOMSCALE:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_TOOLTIPCOMMENT:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_SIGNALCOLOR:
							
							SetModified(true);
							break;
						case Enums.eCtrlProperty.PROP_AREAID:
							
							SetModified(true);
							SetToolTip();
							break;
						case Enums.eCtrlProperty.PROP_SUBAREAID:
							
							SetModified(true);
							SetToolTip();
							break;
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnCtrlStyleChanged()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
			{
				base.OnPaint(e);
				
				try
				{
					if (this.ClientRectangle.Width < 1 || this.ClientRectangle.Height < 1)
					{
						return;
					}
					
					//?ÑÏû¨ ?¨Ïö©?òÏ? ?äÏùå
					//Dim bAllowHot As Boolean = (Me.Enabled And Not Me.DesignMode) _
					//    And Not (Me.IsMouseInControl And Control.MouseButtons = MouseButtons.Left And Not Me.ContainsFocus)
					//IsHot = (Me.IsMouseInControl Or Me.ContainsFocus) And bAllowHot
					
					CtrlOriginalPos = this.ClientRectangle;
					CtrlPos = modCommonFunctions.InflateRectangle(this.ClientRectangle, - modDefines.CTRL_TRACKER_SIZE);
					
					DrawControl(e.Graphics);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnPaint()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnMouseEnter(System.EventArgs e)
			{
				base.OnMouseEnter(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlMouseEnterEvent != null)
						CtrlMouseEnterEvent(this, e);
					
					if (IsDesignMode == true)
					{
						tipCtrlInfo.Active = false;
						
						tipCtrlInfo.Active = true;
					}
					else
					{
						if (CtrlStatus.IsNoEvent == false)
						{
							tipCtrlInfo.Active = false;
							
							tipCtrlInfo.Active = true;
						}
						else
						{
							tipCtrlInfo.Active = false;
						}
					}
					
					if (IsSelected == true)
					{
						return;
					}
					
					if (IsHot == false)
					{
						IsHot = true;
						RedrawCtrl();
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnMouseEnter()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnMouseLeave(System.EventArgs e)
			{
				base.OnMouseLeave(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlMouseLeaveEvent != null)
						CtrlMouseLeaveEvent(this, e);
					
					if (IsSelected == true)
					{
						Cursor = Cursors.Default;
						return;
					}
					
					IsHot = IsSelected;
					IsPressed = false;
					
					if (IsFocused == false)
					{
						RedrawCtrl();
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnMouseLeave()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnGotFocus(System.EventArgs e)
			{
				base.OnGotFocus(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlGotFocusEvent != null)
						CtrlGotFocusEvent(this, e);
					
					if (CanGotFocus == false)
					{
						CanGotFocus = true;
						return;
					}
					
					if (IsDesignMode == true)
					{
						IsFocused = true;
						IsSelected = true;
					}
					
					IsHot = true;
					
					RedrawCtrl();
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnGotFocus()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnLostFocus(System.EventArgs e)
			{
				base.OnLostFocus(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlLostFocusEvent != null)
						CtrlLostFocusEvent(this, e);
					
					if (CanLostFocus == false)
					{
						CanLostFocus = true;
						return;
					}
					
					if (IsDesignMode == true)
					{
						m_iTracker = modDefines.CTRL_HITTEST_TRACKER_NULL;
						Cursor = GetTrackerCursor(m_iTracker);
					}
					
					IsFocused = false;
					IsPressed = false;
					IsHot = false;
					IsSelected = false;
					
					RedrawCtrl();
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnLostFocus()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
			{
				base.OnMouseDown(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlMouseDownEvent != null)
						CtrlMouseDownEvent(this, e);
					
					if (CanMouseDown == false)
					{
						CanMouseDown = true;
						return;
					}
					
					if (this.Enabled == true)
					{
						IsPressed = true;
						if (IsDesignMode == true)
						{
							m_ptStartPos = this.PointToScreen(new Point(e.X, e.Y));
							m_iTracker = HitTestTracker(new Point(e.X, e.Y), true);
							Cursor = GetTrackerCursor(m_iTracker);
						}
						RedrawCtrl();
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnMouseDown()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
			{
				base.OnMouseUp(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlMouseUpEvent != null)
						CtrlMouseUpEvent(this, e);
					
					if (CanMouseUp == false)
					{
						CanMouseUp = true;
						return;
					}
					
					if (IsDesignMode == true)
					{
						if (this.Enabled == true && this.IsHot == true && this.Focused == true)
						{
							IsPressed = false;
							m_ptStartPos.Y = 0;
							m_iTracker = modDefines.CTRL_HITTEST_TRACKER_NULL;
							Cursor = GetTrackerCursor(m_iTracker);
							RedrawCtrl();
						}
					}
					else
					{
						if (this.Enabled == true && IsPressed == true)
						{
							IsPressed = false;
							RedrawCtrl();
						}
					}
					
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnMouseUp()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
			{
				base.OnMouseMove(e);
				
				try
				{
					if (CtrlPos.Width <= 0 && CtrlPos.Height <= 0)
					{
						return;
					}
					
					if (CtrlMouseMoveEvent != null)
						CtrlMouseMoveEvent(this, e);
					
					if (IsDesignMode == false)
					{
						return;
					}
					
					if (CanMouseMove == false)
					{
						CanMouseMove = true;
						return;
					}
					
					if (IsSelected == true)
					{
						if (IsPressed == false)
						{
							m_iTracker = HitTestTracker(new Point(e.X, e.Y), true);
							if (m_iTracker == modDefines.CTRL_HITTEST_TRACKER_ALL)
							{
								Cursor = Cursors.Default;
							}
							else
							{
								Cursor = GetTrackerCursor(m_iTracker);
							}
						}
						
						if (m_iTracker > 0)
						{
							if (IsPressed == true && !(m_ptStartPos.IsEmpty))
							{
								
								Point ptScreen = this.PointToScreen(new Point(e.X, e.Y));
								Size szDelta = new Size(ptScreen.X - m_ptStartPos.X, ptScreen.Y - m_ptStartPos.Y);
								
								if (Math.Abs(szDelta.Width) < SystemInformation.DragSize.Width && Math.Abs(szDelta.Height) < SystemInformation.DragSize.Height)
								{
									return;
								}
								
								if (SetMoveToSize(m_iTracker, szDelta) == true)
								{
									m_ptStartPos = ptScreen;
								}
								
							}
						}
						
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnMouseMove()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
			{
				base.OnKeyDown(e);
				
				try
				{
					if (IsDesignMode == false)
					{
						return;
					}
					
					if (CtrlKeyDownEvent != null)
						CtrlKeyDownEvent(this, e);
					
					if (CanKeyDown == false)
					{
						CanKeyDown = true;
						return;
					}
					
					if (e.Control == true && e.Shift == false)
					{
						if (this.Enabled == true)
						{
							switch (e.KeyCode)
							{
								case Keys.Up:
									
									Point ptLocation = new Point(this.GetLocation().X, this.GetLocation().Y - 1);
									SetLocation(ptLocation, true);
									break;
								case Keys.Right:
									
									Point ptLocation_1 = new Point(this.GetLocation().X + 1, this.GetLocation().Y);
									SetLocation(ptLocation_1, true);
									break;
								case Keys.Down:
									
									Point ptLocation_2 = new Point(this.GetLocation().X, this.GetLocation().Y + 1);
									SetLocation(ptLocation_2, true);
									break;
								case Keys.Left:
									
									Point ptLocation_3 = new Point(this.GetLocation().X - 1, this.GetLocation().Y);
									SetLocation(ptLocation_3, true);
									break;
							}
						}
					}
					else if (e.Control == true && e.Shift == true)
					{
						if (this.Enabled == true)
						{
							Size szSize = new 							Size();
							switch (e.KeyCode)
							{
								case Keys.Up:
									
									szSize = new Size(this.GetSize().Width, this.GetSize().Height - 1);
									break;
								case Keys.Right:
									
									szSize = new Size(this.GetSize().Width + 1, this.GetSize().Height);
									break;
								case Keys.Down:
									
									szSize = new Size(this.GetSize().Width, this.GetSize().Height + 1);
									break;
								case Keys.Left:
									
									szSize = new Size(this.GetSize().Width - 1, this.GetSize().Height);
									break;
							}
							if (szSize.IsEmpty == false)
							{
								if (this.CtrlStatus.ToolType == Enums.eToolType.HorizontalLine)
								{
									if (szSize.Width < modDefines.CTRL_MININUM_SIZE || szSize.Height < modDefines.LINE_MININUM_SIZE)
									{
										return;
									}
									if (szSize.Height > modDefines.CTRL_MAXIMUM_SIZE)
									{
										return;
									}
								}
								else if (this.CtrlStatus.ToolType == Enums.eToolType.VerticalLine)
								{
									if (szSize.Width < modDefines.LINE_MININUM_SIZE || szSize.Height < modDefines.CTRL_MININUM_SIZE)
									{
										return;
									}
									if (szSize.Width > modDefines.CTRL_MAXIMUM_SIZE)
									{
										return;
									}
								}
								else
								{
									if (szSize.Width < modDefines.CTRL_MININUM_SIZE || szSize.Height < modDefines.CTRL_MININUM_SIZE)
									{
										return;
									}
									if (szSize.Width > modDefines.CTRL_MAXIMUM_SIZE || szSize.Height > modDefines.CTRL_MAXIMUM_SIZE)
									{
										return;
									}
								}
								SetSize(szSize, true);
							}
						}
					}
					else
					{
						if (this.Enabled == true)
						{
							if (e.KeyCode == Keys.Delete)
							{
								System.Windows.Forms.MenuItem mnuKeyDelete = new System.Windows.Forms.MenuItem();
								mnuKeyDelete.MergeOrder = 2;
								CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs("KEY_DELETE", this);
								if (CtrlContextMenuEvent != null)
									CtrlContextMenuEvent(mnuKeyDelete, eventArgs);
							}
						}
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.OnKeyDown()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void mnuCtrlBase_Popup(object sender, System.EventArgs e)
			{
				
				try
				{
					if (IsDesignMode == true)
					{
						if (CtrlStatus.ToolType == Enums.eToolType.Resource)
						{
							mnuDelete.Visible = true;
							mnuUpdate.Visible = true;
							mnuProperties.Visible = true;
							mnuSeperator1.Visible = true;
							mnuViewResourceStatus.Visible = true;
							mnuViewResourceHistory.Visible = true;
							mnuTranEvent.Visible = true;
							mnuSeperator2.Visible = false;
							mnuCopyTag.Visible = false;
						}
						else
						{
							mnuDelete.Visible = true;
							mnuUpdate.Visible = true;
							mnuProperties.Visible = true;
							mnuSeperator1.Visible = false;
							mnuViewResourceStatus.Visible = false;
							mnuViewResourceHistory.Visible = false;
							mnuTranEvent.Visible = false;
							mnuSeperator2.Visible = true;
							mnuCopyTag.Visible = true;
						}
					}
					else
					{
						if (CtrlStatus.ToolType == Enums.eToolType.Resource)
						{
							mnuDelete.Visible = false;
							mnuUpdate.Visible = false;
							mnuProperties.Visible = true;
							mnuSeperator1.Visible = true;
							mnuViewResourceStatus.Visible = true;
							mnuViewResourceHistory.Visible = true;
							mnuTranEvent.Visible = true;
							mnuSeperator2.Visible = false;
							mnuCopyTag.Visible = false;
						}
						else
						{
							mnuDelete.Visible = false;
							mnuUpdate.Visible = false;
							mnuProperties.Visible = true;
							mnuSeperator1.Visible = false;
							mnuViewResourceStatus.Visible = false;
							mnuViewResourceHistory.Visible = false;
							mnuTranEvent.Visible = false;
							mnuSeperator2.Visible = false;
							mnuCopyTag.Visible = false;
						}
					}
					
					tipCtrlInfo.Active = false;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuCtrlBase_Popup()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			private void mnuTranEvent_Click(System.Object sender, System.EventArgs e)
			{
				
				try
				{
					CtrlContextMenu_EventArgs eventArgs = new CtrlContextMenu_EventArgs(((MenuItem) sender).ToString(), this);
					if (CtrlContextMenuEvent != null)
						CtrlContextMenuEvent(sender, eventArgs);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.mnuTranEvent_Click()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			#endregion
			
			#region " Drawing Functions"
			
			protected virtual void DrawControl(Graphics g)
			{
				
			}
			
			protected virtual void DrawControls()
			{
				
				try
				{
					if (IsModified == true)
					{
						
						Graphics g = this.CreateGraphics();
						
						g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
						g.SmoothingMode = SmoothingMode.AntiAlias;
						//Î∞∞Í≤Ω???úÎ≤à ÏßÄ??Ï§òÏïº ?¥Î¶≠ ??Í∏Ä???îÏÉÅ???ÜÏñ¥Ïß?
						g.Clear(this.Parent.BackColor);
						
						DrawControl(g);
						g.Dispose();
						
						SetModified(false);
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.DrawControls()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected virtual void DrawTracker(Graphics g, Enums.eLineType LineType)
			{
				
				try
				{
					int iTracker;
					int iTrackerCount;
					int iStartIndex;
					
					iTrackerCount = GetTrackerCount(LineType);
					
					if (LineType == Enums.eLineType.Horizontal)
					{
						iStartIndex = 3;
					}
					else
					{
						iStartIndex = 1;
					}
					
					for (iTracker = iStartIndex; iTracker <= iTrackerCount; iTracker++)
					{
						Point ptTracker = GetTrackerPoint(iTracker);
						Rectangle rcTracker = new Rectangle(ptTracker.X - modDefines.CTRL_TRACKER_SIZE, ptTracker.Y - modDefines.CTRL_TRACKER_SIZE, modDefines.CTRL_TRACKER_SIZE * 2, modDefines.CTRL_TRACKER_SIZE * 2);
						if (IsFocused == true)
						{
							g.FillRectangle(new SolidBrush(modDefines.CTRL_TRACKER_FOCUS_COLOR), rcTracker);
						}
						else
						{
							if (IsSelected == true)
							{
								g.FillRectangle(new SolidBrush(modDefines.CTRL_TRACKER_SELECT_COLOR), rcTracker);
							}
						}
						g.DrawRectangle(Pens.Black, rcTracker);
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.DrawTracker()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			protected virtual int GetTrackerCount(Enums.eLineType LineType)
			{
				
				try
				{
					int iTrackerCount;
					
					if (LineType == Enums.eLineType.Vertical)
					{
						iTrackerCount = 2;
					}
					else if (LineType == Enums.eLineType.Horizontal)
					{
						iTrackerCount = 4;
					}
					else
					{
						iTrackerCount = 8;
					}
					
					return iTrackerCount;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.GetTrackerCount()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
				return 0;
			}
			
			protected virtual Point GetTrackerPoint(int iTracker)
			{
				
				try
				{
					int iX = 0;
					int iY = 0;
					int iXCenter;
					int iYCenter;
					
					iXCenter = CtrlPos.Left + CtrlPos.Width/ 2;
					iYCenter = CtrlPos.Top + CtrlPos.Height/ 2;
					
					switch (iTracker)
					{
						case modDefines.CTRL_HITTEST_TRACKER_LTM: // ??
							
							iX = iXCenter;
							iY = CtrlPos.Top;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_RBM: // ??
							
							iX = iXCenter;
							iY = CtrlPos.Bottom - 1;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_L: // ??
							
							iX = CtrlPos.Left;
							iY = iYCenter;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_R: // ??
							
							iX = CtrlPos.Right - 1;
							iY = iYCenter;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_RB: // ??
							
							iX = CtrlPos.Right - 1;
							iY = CtrlPos.Bottom - 1;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_LB: // ??
							
							iX = CtrlPos.Left;
							iY = CtrlPos.Bottom - 1;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_LT: // ??
							
							iX = CtrlPos.Left;
							iY = CtrlPos.Top;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_RT: // ??
							
							iX = CtrlPos.Right - 1;
							iY = CtrlPos.Top;
							break;
					}
					
					return new Point(iX, iY);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.GetTrackerPoint()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Point.Empty;
				}
			}
			
			protected virtual Cursor GetTrackerCursor(int iTracker)
			{
				
				try
				{
					switch (iTracker)
					{
						case modDefines.CTRL_HITTEST_TRACKER_RB:
							return Cursors.SizeNWSE;
							
						case modDefines.CTRL_HITTEST_TRACKER_LT:
							return Cursors.SizeNWSE;

                        case modDefines.CTRL_HITTEST_TRACKER_RBM:
                            return Cursors.SizeNS;

                        case modDefines.CTRL_HITTEST_TRACKER_LTM:
                            return Cursors.SizeNS;

                        case modDefines.CTRL_HITTEST_TRACKER_LB:
                            return Cursors.SizeNESW;

                        case modDefines.CTRL_HITTEST_TRACKER_RT:
                            return Cursors.SizeNESW;

                        case modDefines.CTRL_HITTEST_TRACKER_L:
                            return Cursors.SizeWE;

                        case modDefines.CTRL_HITTEST_TRACKER_R:
                            return Cursors.SizeWE;

                        case modDefines.CTRL_HITTEST_TRACKER_ALL:
                            return Cursors.SizeAll;
					}
					
					return Cursors.Default;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.GetTrackerCursor()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
				return null;
			}
			
			public virtual bool SetMoveToSize(int iHitTest, Size szDelta)
			{
				
				try
				{
					Rectangle rcPos = new Rectangle(GetLocation(), GetSize());
					
					// szDelta : <- Minus
					// szDelta : -> Plus
					switch (iHitTest)
					{
						case modDefines.CTRL_HITTEST_TRACKER_LTM: // ??
							
							rcPos.Y = rcPos.Y + szDelta.Height;
							rcPos.Height = rcPos.Height - szDelta.Height;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_RBM: // ??
							
							rcPos.Height = rcPos.Height + szDelta.Height;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_L: // ??
							
							rcPos.X = rcPos.X + szDelta.Width;
							rcPos.Width = rcPos.Width - szDelta.Width;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_R: // ??
							
							rcPos.Width = rcPos.Width + szDelta.Width;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_RB: // ??
							
							rcPos.Width = rcPos.Width + szDelta.Width;
							rcPos.Height = rcPos.Height + szDelta.Height;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_LB: // ??
							
							rcPos.X = rcPos.X + szDelta.Width;
							rcPos.Width = rcPos.Width - szDelta.Width;
							rcPos.Height = rcPos.Height + szDelta.Height;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_LT: // ??
							
							rcPos.X = rcPos.X + szDelta.Width;
							rcPos.Width = rcPos.Width - szDelta.Width;
							rcPos.Y = rcPos.Y + szDelta.Height;
							rcPos.Height = rcPos.Height - szDelta.Height;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_RT: // ??
							
							rcPos.Width = rcPos.Width + szDelta.Width;
							rcPos.Y = rcPos.Y + szDelta.Height;
							rcPos.Height = rcPos.Height - szDelta.Height;
							break;
						case modDefines.CTRL_HITTEST_TRACKER_ALL:
							
							rcPos.X = rcPos.X + szDelta.Width;
							rcPos.Y = rcPos.Y + szDelta.Height;
							break;
					}
					
					if (SetMoveTo(rcPos) == false)
					{
						return false;
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetMoveToSize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
				return true;
				
			}
			
			protected virtual bool SetMoveTo(Rectangle rcPos)
			{
				
				try
				{
					if (GetLocation().Equals(rcPos.Location) == true && GetSize().Equals(rcPos.Size) == true)
					{
						return false;
					}
					
					if (this.CtrlStatus.ToolType == Enums.eToolType.HorizontalLine)
					{
						if (rcPos.Size.Width < modDefines.CTRL_MININUM_SIZE || rcPos.Size.Height < modDefines.LINE_MININUM_SIZE)
						{
							return false;
						}
						if (rcPos.Size.Height > modDefines.CTRL_MAXIMUM_SIZE)
						{
							return false;
						}
					}
					else if (this.CtrlStatus.ToolType == Enums.eToolType.VerticalLine)
					{
						if (rcPos.Size.Width < modDefines.LINE_MININUM_SIZE || rcPos.Size.Height < modDefines.CTRL_MININUM_SIZE)
						{
							return false;
						}
						if (rcPos.Size.Width > modDefines.CTRL_MAXIMUM_SIZE)
						{
							return false;
						}
					}
					else
					{
						if (rcPos.Size.Width < modDefines.CTRL_MININUM_SIZE || rcPos.Size.Height < modDefines.CTRL_MININUM_SIZE)
						{
							return false;
						}
						if (rcPos.Size.Width > modDefines.CTRL_MAXIMUM_SIZE || rcPos.Size.Height > modDefines.CTRL_MAXIMUM_SIZE)
						{
							return false;
						}
					}
					
					Point ptLocation;
					
					if (rcPos.Left <= this.Parent.ClientRectangle.Left && rcPos.Top <= this.Parent.ClientRectangle.Top)
					{
						// ??
						ptLocation = new Point(this.Parent.ClientRectangle.Left, this.Parent.ClientRectangle.Top);
					}
					else if (rcPos.Right >= this.Parent.ClientRectangle.Right && rcPos.Top <= this.Parent.ClientRectangle.Top)
					{
						// ??
						ptLocation = new Point(this.Parent.ClientRectangle.Right - this.Width, this.Parent.ClientRectangle.Top);
					}
					else if (rcPos.Right >= this.Parent.ClientRectangle.Right && rcPos.Bottom >= this.Parent.ClientRectangle.Bottom)
					{
						// ??
						ptLocation = new Point(this.Parent.ClientRectangle.Right - this.Width, this.Parent.ClientRectangle.Bottom - this.Height);
					}
					else if (rcPos.Left <= this.Parent.ClientRectangle.Left && rcPos.Bottom >= this.Parent.ClientRectangle.Bottom)
					{
						// ??
						ptLocation = new Point(this.Parent.ClientRectangle.Left, this.Parent.ClientRectangle.Bottom - this.Height);
					}
					else if (rcPos.Left <= this.Parent.ClientRectangle.Left && rcPos.Top > this.Parent.ClientRectangle.Top)
					{
						// ??
						ptLocation = new Point(this.Parent.ClientRectangle.Left, rcPos.Top);
					}
					else if (rcPos.Left > this.Parent.ClientRectangle.Left && rcPos.Top <= this.Parent.ClientRectangle.Top)
					{
						// ??
						ptLocation = new Point(rcPos.Left, this.Parent.ClientRectangle.Top);
					}
					else if (rcPos.Right >= this.Parent.ClientRectangle.Right && rcPos.Bottom < this.Parent.ClientRectangle.Bottom)
					{
						// ??
						ptLocation = new Point(this.Parent.ClientRectangle.Right - this.Width, rcPos.Top);
					}
					else if (rcPos.Right < this.Parent.ClientRectangle.Right && rcPos.Bottom >= this.Parent.ClientRectangle.Bottom)
					{
						// ??
						ptLocation = new Point(rcPos.Left, this.Parent.ClientRectangle.Bottom - this.Height);
					}
					else
					{
						ptLocation = rcPos.Location;
					}
					
					SetLocation(ptLocation, true);
					SetSize(rcPos.Size, true);
					
					return true;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetMoveTo()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
				return false;
			}
			
			protected virtual int HitTestTracker(Point ptLP, bool bSelected)
			{
				
				try
				{
					if (bSelected == true)
					{
						int iTracker;
						int iTrackerCount;
						int iStartIndex;
						if (this.CtrlStatus.ToolType == Enums.eToolType.VerticalLine)
						{
							iTrackerCount = GetTrackerCount(Enums.eLineType.Vertical);
							iStartIndex = 1;
						}
						else if (this.CtrlStatus.ToolType == Enums.eToolType.HorizontalLine)
						{
							iTrackerCount = GetTrackerCount(Enums.eLineType.Horizontal);
							iStartIndex = 3;
						}
						else
						{
							iTrackerCount = GetTrackerCount(Enums.eLineType.Null);
							iStartIndex = 1;
						}
						
						for (iTracker = iStartIndex; iTracker <= iTrackerCount; iTracker++)
						{
							Rectangle rcTracker = GetTrackerRect(iTracker);
							if (ptLP.X >= rcTracker.Left && ptLP.X <= rcTracker.Right && ptLP.Y >= rcTracker.Top && ptLP.Y <= rcTracker.Bottom)
							{
								return iTracker;
							}
						}
						
						return modDefines.CTRL_HITTEST_TRACKER_ALL;
						
					}
					else
					{
						if (ptLP.X >= CtrlPos.Left && ptLP.X <= CtrlPos.Right && ptLP.Y >= CtrlPos.Top && ptLP.Y <= CtrlPos.Bottom)
						{
							return modDefines.CTRL_HITTEST_TRACKER_ALL;
						}
					}
					
					return modDefines.CTRL_HITTEST_TRACKER_NULL;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.HitTestTracker()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
				}
				
			}
			
			protected virtual Rectangle GetTrackerRect(int iTracker)
			{
				
				try
				{
					Rectangle rcTracker;
					Point ptTracker = GetTrackerPoint(iTracker);
					
					rcTracker = new Rectangle(ptTracker.X - modDefines.CTRL_TRACKER_SIZE, ptTracker.Y - modDefines.CTRL_TRACKER_SIZE, modDefines.CTRL_TRACKER_SIZE * 2 + 1, modDefines.CTRL_TRACKER_SIZE * 2 + 1);
					
					return rcTracker;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.GetTrackerRect()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Rectangle.Empty;
				}
			}
			
			#endregion
			
			#region " Functions Implemetation"
			
			protected void SetToolTip()
			{
				
				string sToolTip = "";
				try
				{
					if (IsModified == true || CtrlStatus.IsSaveFlag == true)
					{
						if (CtrlStatus.ToolType == Enums.eToolType.Resource)
						{
							string sUpDownFlag = "";
							if (CtrlStatus.UpDownFlag != "")
							{
								if (CtrlStatus.UpDownFlag.Substring(0, 1) == "D")
								{
									sUpDownFlag = "DOWN";
								}
								else if (CtrlStatus.UpDownFlag.Substring(0, 1) == "U")
								{
									sUpDownFlag = "UP";
								}
							}
							sToolTip = "Name : " + this.Name + "\r\n" + "Text : " + CtrlStatus.Text + "\r\n" + "Primary Status : " + CtrlStatus.PrimaryStatus + "\r\n" + "UpDown Flag : " + sUpDownFlag + "\r\n" + "Process Mode : " + CtrlStatus.ProcMode + "\r\n" + "Control Mode : " + CtrlStatus.CtrlMode + "\r\n" + "Resource Type : " + CtrlStatus.ResourceType + "\r\n" + "Area ID : " + CtrlStatus.AreaID + "\r\n" + "Sub Area ID : " + CtrlStatus.SubAreaID + "\r\n" + "Last Event : " + CtrlStatus.LastEvent;
							if (CtrlStatus.ToolTipComment != "")
							{
								sToolTip += "\r\n" + CtrlStatus.ToolTipComment;
							}

                            this.demoThread =
                                new Thread(new ParameterizedThreadStart(this.ThreadProcSafe));

                            this.demoThread.Start(sToolTip);

                            //tipCtrlInfo.SetToolTip(this, sToolTip);
						}
						else if (CtrlStatus.ToolType > Enums.eToolType.Resource)
						{
							sToolTip = "Name : " + this.Name + "\r\n" + "Text : " + CtrlStatus.Text;
							if (CtrlStatus.ToolTipComment != "")
							{
								sToolTip += "\r\n" + CtrlStatus.ToolTipComment;
							}

                            this.demoThread =
                                new Thread(new ParameterizedThreadStart(this.ThreadProcSafe));

                            this.demoThread.Start(sToolTip);

                            //tipCtrlInfo.SetToolTip(this, sToolTip);
						}
						
						tipCtrlInfo.Active = false;
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetToolTip()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}

            private void ThreadProcSafe(object sText)
            {
                this.ThreadSetToolTip((string)sText);
            }

            private void ThreadSetToolTip(string text)
            {
                // InvokeRequired required compares the thread ID of the
                // calling thread to the thread ID of the creating thread.
                // If these threads are different, it returns true.
                if(this.InvokeRequired)
                {
                    SetToolTipCallback d = new SetToolTipCallback(ThreadSetToolTip);
                    this.Invoke(d, new object[] { text });
                }
                else
                {
                    this.tipCtrlInfo.SetToolTip(this, text);
                }
            }

			public void SetModified(bool bModified)
			{
				
				try
				{
					IsModified = bModified;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetModified()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			public void SetCtrlStatusData(clsCtrlStatus data, int iStep, bool bChange)
			{
				
				try
				{
					if (iStep == 1) // All Property Update
					{
						SetName(data.Key);
						SetText(data.Text);
						SetLocation(data.GetLocation(), bChange);
						SetSize(data.GetSize(), bChange);
						
						CtrlStatus.ResTagFlag = data.ResTagFlag;
						CtrlStatus.IsNoEvent = data.IsNoEvent;
						CtrlStatus.LastEvent = data.LastEvent;
						CtrlStatus.PrimaryStatus = data.PrimaryStatus;
						CtrlStatus.ProcMode = data.ProcMode;
						CtrlStatus.CtrlMode = data.CtrlMode;
						CtrlStatus.ResourceType = data.ResourceType;
						CtrlStatus.TextColor = data.TextColor;
						CtrlStatus.BackColor = data.BackColor;
						CtrlStatus.TextFontName = data.TextFontName;
						CtrlStatus.TextSize = data.TextSize;
						CtrlStatus.TextStyle = data.TextStyle;
						CtrlStatus.ToolType = data.ToolType;
						CtrlStatus.UpDownFlag = data.UpDownFlag;
						CtrlStatus.IsProcessMode = data.IsProcessMode;
						CtrlStatus.IsUseEventColor = data.IsUseEventColor;
						CtrlStatus.EventBackColor = data.EventBackColor;
						CtrlStatus.ImageIndex = data.ImageIndex;
						CtrlStatus.IsViewSignal = data.IsViewSignal;
						CtrlStatus.ZoomScale = data.ZoomScale;
						CtrlStatus.ToolTipComment = data.ToolTipComment;
						CtrlStatus.SignalColor = data.SignalColor;
						CtrlStatus.AreaID = data.AreaID;
						CtrlStatus.SubAreaID = data.SubAreaID;
					}
					else // Resource Status Update
					{
						CtrlStatus.LastEvent = data.LastEvent;
						CtrlStatus.PrimaryStatus = data.PrimaryStatus;
						CtrlStatus.ProcMode = data.ProcMode;
						CtrlStatus.CtrlMode = data.CtrlMode;
						CtrlStatus.ResourceType = data.ResourceType;
						CtrlStatus.UpDownFlag = data.UpDownFlag;
						CtrlStatus.IsProcessMode = data.IsProcessMode;
						CtrlStatus.IsUseEventColor = data.IsUseEventColor;
						CtrlStatus.EventBackColor = data.EventBackColor;
						CtrlStatus.SignalColor = data.SignalColor;
						CtrlStatus.AreaID = data.AreaID;
						CtrlStatus.SubAreaID = data.SubAreaID;
						CtrlStatus.ToolTipComment = data.ToolTipComment;
					}
					
					RedrawCtrl();
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetCtrlStatusData()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			public void RedrawCtrl()
			{
				
				try
				{
					if (IsModified == true)
					{
						if (this.CtrlPos.Width > 0 && this.CtrlPos.Height > 0)
						{
							if (CtrlStatus.ZoomScale == 0)
							{
								if (this.Location.Equals(CtrlStatus.GetLocation()) == false)
								{
									this.Location = CtrlStatus.Location;
								}
								if (this.Size.Equals(CtrlStatus.GetSize()) == false)
								{
									this.Size = CtrlStatus.Size;
								}
							}
							Invalidate(false);
						}
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.RedrawCtrl()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			public void SetName(string sName)
			{
				
				try
				{
					CtrlStatus.Key = sName;
					this.Name = CtrlStatus.Key;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetName()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			public void SetText(string sText)
			{
				
				try
				{
					CtrlStatus.Text = sText;
					this.Text = CtrlStatus.Text;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetText()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
				
			}
			
			public void SetLocation(Point ptLocation, bool bChange)
			{
				
				try
				{
					CtrlStatus.SetLocation(ptLocation);
					if (bChange == true)
					{
						this.Location = CtrlStatus.Location;
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetLocation()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			public Point GetLocation()
			{
				
				try
				{
					return CtrlStatus.GetLocation();
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.GetLocation()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Point.Empty;
				}
			}
			
			public void SetSize(Size szSize, bool bChange)
			{
				
				try
				{
					CtrlStatus.SetSize(szSize);
					if (bChange == true)
					{
						this.Size = CtrlStatus.Size;
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.SetSize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			public Size GetSize()
			{
				
				try
				{
					return CtrlStatus.GetSize();
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.GetSize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return Size.Empty;
				}
			}
			
			public object Clone(ref udcCtrlBase cloneData)
			{
				
				try
				{
					cloneData.IsHot = false;
					cloneData.IsSelected = false;
					cloneData.IsFocused = false;
					cloneData.IsPressed = false;
					cloneData.IsModified = false;
					
					cloneData.CanLostFocus = true;
					cloneData.CanGotFocus = true;
					cloneData.CanMouseDown = true;
					cloneData.CanMouseUp = true;
					cloneData.CanMouseMove = true;
					cloneData.CanKeyDown = true;
					
					cloneData.CtrlPos = this.CtrlPos;
					cloneData.CtrlOriginalPos = this.CtrlOriginalPos;
					
					cloneData.IsDesignMode = this.IsDesignMode;
					
					cloneData.CtrlStatus.ResTagFlag = this.CtrlStatus.ResTagFlag;
					cloneData.CtrlStatus.BackColor = this.CtrlStatus.BackColor;
					cloneData.CtrlStatus.BackHotColor = this.CtrlStatus.BackHotColor;
					cloneData.CtrlStatus.BackPressedColor = this.CtrlStatus.BackPressedColor;
					cloneData.CtrlStatus.BorderColor = this.CtrlStatus.BorderColor;
					cloneData.CtrlStatus.BorderHotColor = this.CtrlStatus.BorderHotColor;
					cloneData.CtrlStatus.BorderPressedColor = this.CtrlStatus.BorderPressedColor;
					cloneData.CtrlStatus.CtrlMode = this.CtrlStatus.CtrlMode;
					cloneData.CtrlStatus.EventBackColor = this.CtrlStatus.EventBackColor;
					cloneData.CtrlStatus.EventBackHotColor = this.CtrlStatus.EventBackHotColor;
					cloneData.CtrlStatus.EventBackPressedColor = this.CtrlStatus.EventBackPressedColor;
					cloneData.CtrlStatus.Factory = this.CtrlStatus.Factory;
					cloneData.CtrlStatus.ImageIndex = this.CtrlStatus.ImageIndex;
					cloneData.CtrlStatus.IsDeleteRes = this.CtrlStatus.IsDeleteRes;
					cloneData.CtrlStatus.IsNoEvent = this.CtrlStatus.IsNoEvent;
					cloneData.CtrlStatus.IsProcessMode = this.CtrlStatus.IsProcessMode;
					cloneData.CtrlStatus.IsSaveFlag = this.CtrlStatus.IsSaveFlag;
					cloneData.CtrlStatus.IsUseEventColor = this.CtrlStatus.IsUseEventColor;
					cloneData.CtrlStatus.Key = this.CtrlStatus.Key;
					cloneData.CtrlStatus.LastEvent = this.CtrlStatus.LastEvent;
					cloneData.CtrlStatus.Location = this.CtrlStatus.Location;
					cloneData.CtrlStatus.PrimaryStatus = this.CtrlStatus.PrimaryStatus;
					cloneData.CtrlStatus.ProcMode = this.CtrlStatus.ProcMode;
					cloneData.CtrlStatus.ResourceType = this.CtrlStatus.ResourceType;
					cloneData.CtrlStatus.Size = this.CtrlStatus.Size;
					cloneData.CtrlStatus.Text = this.CtrlStatus.Text;
					cloneData.CtrlStatus.TextColor = this.CtrlStatus.TextColor;
					cloneData.CtrlStatus.TextFontName = this.CtrlStatus.TextFontName;
					cloneData.CtrlStatus.TextSize = this.CtrlStatus.TextSize;
					cloneData.CtrlStatus.TextStyle = this.CtrlStatus.TextStyle;
					cloneData.CtrlStatus.ToolType = this.CtrlStatus.ToolType;
					cloneData.CtrlStatus.UpDownFlag = this.CtrlStatus.UpDownFlag;
					cloneData.CtrlStatus.IsUserGroupDesign = this.CtrlStatus.IsUserGroupDesign;
					cloneData.CtrlStatus.IsViewSignal = this.CtrlStatus.IsViewSignal;
					cloneData.CtrlStatus.ZoomScale = this.CtrlStatus.ZoomScale;
					cloneData.CtrlStatus.AreaID = this.CtrlStatus.AreaID;
					cloneData.CtrlStatus.SubAreaID = this.CtrlStatus.SubAreaID;
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlBase.Clone()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
				return null;
			}
			
			#endregion
		
		}
		
	}
	
}
