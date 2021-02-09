using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.IO;
using System.ComponentModel;
using Miracom.FMBUI;
using Miracom.FMBUI.Controls;
//using Miracom.FMBUI.Enums;


namespace Miracom.FMBUI
{
	public class clsCtrlStatus : object
	{
		
		
		public clsCtrlStatus()
		{
			
		}
		
		#region " Property Implementations"
		
		private int m_iZoomScale = 0;
		private bool m_bViewSignal = false;
		private bool m_bNoEvent = false;
		private bool m_bProcessMode = false;
		private bool m_bUseEventColor = false;
		
		private bool m_bSaveFlag = false;
		
		private Enums.eToolType m_iToolType = Enums.eToolType.Null;
		private string m_sKey = string.Empty;
		private Size m_szSize;
		private Point m_ptLocation;
		
		private string m_sResTagFlag = string.Empty;
		private string m_sText = string.Empty;
		private int m_iTextSize = 7;
		private int m_iTextStyle = 0;
		private string m_sTextFontName = string.Empty;
		private string m_sPrimaryStatus = string.Empty;
		private string m_sUpDownFlag = string.Empty;
		private string m_sResourceType = string.Empty;
		private string m_sLastEvent = string.Empty;
		private string m_sProcMode = string.Empty;
		private string m_sCtrlMode = string.Empty;
		private bool m_bDeleteRes = false;
		private string m_sFactory = string.Empty;
		private string m_sToolTipComment = string.Empty;
		private bool m_bUserGroupDesign = false;
		private string m_sAreaID = string.Empty;
		private string m_sSubAreaID = string.Empty;
		
		private int m_iTextColor = 0;
		private int m_iBackColor = 0;
		
		private Color m_clBackHotColor = Color.FromArgb(Math.Abs(Color.FromKnownColor(KnownColor.White).R - 50), Math.Abs(Color.FromKnownColor(KnownColor.White).G - 50), Math.Abs(Color.FromKnownColor(KnownColor.White).B - 50));
		private Color m_clBackPressedColor = Color.FromArgb(70, Color.FromKnownColor(KnownColor.White));
		private Color m_clBorderColor = Color.DarkGray;
		private Color m_clBorderHotColor = Color.Black;
		private Color m_clBorderPressedColor = Color.FromArgb(210, 218, 232);
		
		private int m_iEventBackColor = 0;
		
		private Color m_clEventBackHotColor = Color.FromArgb(Math.Abs(Color.FromKnownColor(KnownColor.White).R - 50), Math.Abs(Color.FromKnownColor(KnownColor.White).G - 50), Math.Abs(Color.FromKnownColor(KnownColor.White).B - 50));
		private Color m_clEventBackPressedColor = Color.FromArgb(70, Color.FromKnownColor(KnownColor.White));
		
		private int m_iImageIndex = - 1;
		
		private Color m_clSignalColor = Color.White;
		
		[Description("Gets or sets ZoomScale"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ZoomScale
		{
			get
			{
				return m_iZoomScale;
			}
			set
			{
				if (m_iZoomScale.Equals(value) == false)
				{
					m_iZoomScale = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ZOOMSCALE, value);
				}
			}
		}
		
		[Description("Gets or sets IsViewSignal"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsViewSignal
		{
			get
			{
				return m_bViewSignal;
			}
			set
			{
				if (m_bViewSignal.Equals(value) == false)
				{
					m_bViewSignal = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ISVIEWSIGNAL, value);
				}
			}
		}
		
		[Description("Gets or sets IsNoEvent"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsNoEvent
		{
			get
			{
				return m_bNoEvent;
			}
			set
			{
				if (m_bNoEvent.Equals(value) == false)
				{
					m_bNoEvent = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ISNOEVENT, value);
				}
			}
		}
		
		[Description("Gets or sets IsProcessMode"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsProcessMode
		{
			get
			{
				return m_bProcessMode;
			}
			set
			{
				if (m_bProcessMode.Equals(value) == false)
				{
					m_bProcessMode = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ISPROCESSMODE, value);
				}
			}
		}
		
		[Description("Gets or sets IsUseEventColor"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUseEventColor
		{
			get
			{
				return m_bUseEventColor;
			}
			set
			{
				if (m_bUseEventColor.Equals(value) == false)
				{
					m_bUseEventColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ISUSEEVENTCOLOR, value);
				}
			}
		}
		
		[Description("Gets or sets Factory"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Factory
		{
			get
			{
				return m_sFactory;
			}
			set
			{
				if (m_sFactory.Equals(value) == false)
				{
					m_sFactory = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_FACTORY, value);
				}
			}
		}
		
		[Description("Gets or sets ToolTipComment"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ToolTipComment
		{
			get
			{
				return m_sToolTipComment;
			}
			set
			{
				if (m_sToolTipComment.Equals(value) == false)
				{
					m_sToolTipComment = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TOOLTIPCOMMENT, value);
				}
			}
		}
		
		[Description("Gets or sets IsUserGroupDesign"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool IsUserGroupDesign
		{
			get
			{
				return m_bUserGroupDesign;
			}
			set
			{
				if (m_bUserGroupDesign.Equals(value) == false)
				{
					m_bUserGroupDesign = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ISUSERGROUPDESIGN, value);
				}
			}
		}
		
		[Description("Gets or sets IsDeleteRes"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsDeleteRes
		{
			get
			{
				return m_bDeleteRes;
			}
			set
			{
				if (m_bDeleteRes.Equals(value) == false)
				{
					m_bDeleteRes = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_ISDELETERES, value);
				}
			}
		}
		
		[Description("Gets or sets IsSaveFlag"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSaveFlag
		{
			get
			{
				return m_bSaveFlag;
			}
			set
			{
				if (m_bSaveFlag.Equals(value) == false)
				{
					m_bSaveFlag = value;
				}
			}
		}
		
		[Description("Gets or sets Key"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Key
		{
			get
			{
				return m_sKey;
			}
			set
			{
				if (m_sKey.Equals(value) == false)
				{
					m_sKey = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_KEY, value);
				}
			}
		}
		
		[Description("Gets or sets Size"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size Size
		{
			get
			{
				return m_szSize;
			}
			set
			{
				if (m_szSize.Equals(value) == false)
				{
					m_szSize = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_SIZE, value);
				}
			}
		}
		
		[Description("Gets or sets Location"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point Location
		{
			get
			{
				return m_ptLocation;
			}
			set
			{
				if (m_ptLocation.Equals(value) == false)
				{
					m_ptLocation = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_LOCATION, value);
				}
			}
		}
		
		[Description("Gets or sets ToolType"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Enums.eToolType ToolType
		{
			get
			{
				return m_iToolType;
			}
			set
			{
				if (m_iToolType.Equals(value) == false)
				{
					m_iToolType = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TOOLTYPE, value);
				}
			}
		}
		
		[Description("Gets or sets Resource/Tag Flag"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string ResTagFlag
		{
			get
			{
				return m_sResTagFlag;
			}
			set
			{
				if (m_sResTagFlag.Equals(value) == false)
				{
					m_sResTagFlag = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_RESTAGFLAG, value);
				}
			}
		}
		
		[Description("Gets or sets Text"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string Text
		{
			get
			{
				return m_sText;
			}
			set
			{
				if (m_sText.Equals(value) == false)
				{
					m_sText = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TEXT, value);
				}
			}
		}
		
		[Description("Gets or sets TextColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TextColor
		{
			get
			{
				return m_iTextColor;
			}
			set
			{
				if (m_iTextColor.Equals(value) == false)
				{
					m_iTextColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TEXTCOLOR, value);
				}
			}
		}
		
		[Description("Gets or sets TextSize"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TextSize
		{
			get
			{
				return m_iTextSize;
			}
			set
			{
				if (m_iTextSize.Equals(value) == false)
				{
					m_iTextSize = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TEXTSIZE, value);
				}
			}
		}
		
		[Description("Gets or sets TextStyle"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int TextStyle
		{
			get
			{
				return m_iTextStyle;
			}
			set
			{
				if (m_iTextStyle.Equals(value) == false)
				{
					m_iTextStyle = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TEXTSTYLE, value);
				}
			}
		}
		
		[Description("Gets or sets TextFontName"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TextFontName
		{
			get
			{
				return m_sTextFontName;
			}
			set
			{
				if (m_sTextFontName.Equals(value) == false)
				{
					m_sTextFontName = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_TEXTFONTNAME, value);
				}
			}
		}
		
		[Description("Gets or sets PrimaryStatus"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryStatus
		{
			get
			{
				return m_sPrimaryStatus;
			}
			set
			{
				if (m_sPrimaryStatus.Equals(value) == false)
				{
					m_sPrimaryStatus = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_PRIMARYSTATUS, value);
				}
			}
		}
		
		[Description("Gets or sets UpDownFlag"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UpDownFlag
		{
			get
			{
				return m_sUpDownFlag;
			}
			set
			{
				if (m_sUpDownFlag.Equals(value) == false)
				{
					m_sUpDownFlag = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_UPDOWNFLAG, value);
				}
			}
		}
		
		[Description("Gets or sets ResourceType"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ResourceType
		{
			get
			{
				return m_sResourceType;
			}
			set
			{
				if (m_sResourceType.Equals(value) == false)
				{
					m_sResourceType = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_RESOURCETYPE, value);
				}
			}
		}
		
		[Description("Gets or sets LastEvent"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string LastEvent
		{
			get
			{
				return m_sLastEvent;
			}
			set
			{
				if (m_sLastEvent.Equals(value) == false)
				{
					m_sLastEvent = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_LASTEVENT, value);
				}
			}
		}
		
		[Description("Gets or sets ProcMode"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProcMode
		{
			get
			{
				return m_sProcMode;
			}
			set
			{
				if (m_sProcMode.Equals(value) == false)
				{
					m_sProcMode = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_PROCMODE, value);
				}
			}
		}
		
		[Description("Gets or sets CtrlMode"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CtrlMode
		{
			get
			{
				return m_sCtrlMode;
			}
			set
			{
				if (m_sCtrlMode.Equals(value) == false)
				{
					m_sCtrlMode = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_CTRLMODE, value);
				}
			}
		}
		
		[Description("Gets or sets BackColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BackColor
		{
			get
			{
				return m_iBackColor;
			}
			set
			{
				if (m_iBackColor.Equals(value) == false)
				{
					m_iBackColor = value;
					if (value < 0)
					{
						BackHotColor = Color.FromArgb(Math.Abs(Color.FromArgb(value).R - 50), 
                                                      Math.Abs(Color.FromArgb(value).G - 50), 
                                                      Math.Abs(Color.FromArgb(value).B - 50));
						BackPressedColor = Color.FromArgb(70, Color.FromArgb(value));
					}
					else if (value > 0)
					{
						BackHotColor = Color.FromArgb(Math.Abs(Color.FromKnownColor((KnownColor)value).R - 50),
                                                      Math.Abs(Color.FromKnownColor((KnownColor)value).G - 50),
                                                      Math.Abs(Color.FromKnownColor((KnownColor)value).B - 50));
                        BackPressedColor = Color.FromArgb(70, Color.FromKnownColor((KnownColor)value));
					}
					else
					{
						BackHotColor = Color.FromKnownColor(KnownColor.HotTrack);
						BackPressedColor = Color.FromKnownColor(KnownColor.HotTrack);
					}
					OnStyleChanged(Enums.eCtrlProperty.PROP_BACKCOLOR, value);
				}
			}
		}
		
		[Description("Gets or sets BackHotColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackHotColor
		{
			get
			{
				return m_clBackHotColor;
			}
			set
			{
				if (m_clBackHotColor.Equals(value) == false)
				{
					m_clBackHotColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_BACKHOTCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets BackPressedColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BackPressedColor
		{
			get
			{
				return m_clBackPressedColor;
			}
			set
			{
				if (m_clBackPressedColor.Equals(value) == false)
				{
					m_clBackPressedColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_BACKPRESSEDCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets BorderColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderColor
		{
			get
			{
				return m_clBorderColor;
			}
			set
			{
				if (m_clBorderColor.Equals(value) == false)
				{
					m_clBorderColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_BOARDERCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets BorderHotColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderHotColor
		{
			get
			{
				return m_clBorderHotColor;
			}
			set
			{
				if (m_clBorderHotColor.Equals(value) == false)
				{
					m_clBorderHotColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_BOARDERHOTCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets BorderPressedColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BorderPressedColor
		{
			get
			{
				return m_clBorderPressedColor;
			}
			set
			{
				if (m_clBorderPressedColor.Equals(value) == false)
				{
					m_clBorderPressedColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_BOARDERPRESSEDCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets EventBackColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int EventBackColor
		{
			get
			{
				return m_iEventBackColor;
			}
			set
			{
				if (m_iEventBackColor.Equals(value) == false)
				{
					m_iEventBackColor = value;
					if (value < 0)
					{
						EventBackHotColor = Color.FromArgb(Math.Abs(Color.FromArgb(value).R - 50), Math.Abs(Color.FromArgb(value).G - 50), Math.Abs(Color.FromArgb(value).B - 50));
						EventBackPressedColor = Color.FromArgb(70, Color.FromArgb(value));
					}
					else if (value > 0)
					{
                        EventBackHotColor = Color.FromArgb(Math.Abs(Color.FromKnownColor((KnownColor)value).R - 50),
                                                           Math.Abs(Color.FromKnownColor((KnownColor)value).G - 50),
                                                           Math.Abs(Color.FromKnownColor((KnownColor)value).B - 50));
                        EventBackPressedColor = Color.FromArgb(70, Color.FromKnownColor((KnownColor)value));
					}
					else
					{
						EventBackHotColor = Color.FromKnownColor(KnownColor.HotTrack);
						EventBackPressedColor = Color.FromKnownColor(KnownColor.HotTrack);
					}
					OnStyleChanged(Enums.eCtrlProperty.PROP_EVENTBACKCOLOR, value);
				}
			}
		}
		
		[Description("Gets or sets EventBackHotColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color EventBackHotColor
		{
			get
			{
				return m_clEventBackHotColor;
			}
			set
			{
				if (m_clEventBackHotColor.Equals(value) == false)
				{
					m_clEventBackHotColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_EVENTBACKHOTCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets EventBackPressedColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color EventBackPressedColor
		{
			get
			{
				return m_clEventBackPressedColor;
			}
			set
			{
				if (m_clEventBackPressedColor.Equals(value) == false)
				{
					m_clEventBackPressedColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_EVENTBACKPRESSEDCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets ImageIndex"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int ImageIndex
		{
			get
			{
				return m_iImageIndex;
			}
			set
			{
				if (m_iImageIndex.Equals(value) == false)
				{
					m_iImageIndex = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_IMAGEINDEX, value);
				}
				
			}
		}
		
		[Description("Gets or sets SignalColor"), Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color SignalColor
		{
			get
			{
				return m_clSignalColor;
			}
			set
			{
				if (m_clSignalColor.Equals(value) == false)
				{
					m_clSignalColor = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_SIGNALCOLOR, value);
				}
				
			}
		}
		
		[Description("Gets or sets AreaID"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AreaID
		{
			get
			{
				return m_sAreaID;
			}
			set
			{
				if (m_sAreaID.Equals(value) == false)
				{
					m_sAreaID = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_AREAID, value);
				}
			}
		}
		
		[Description("Gets or sets SubAreaID"), DefaultValue(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SubAreaID
		{
			get
			{
				return m_sSubAreaID;
			}
			set
			{
				if (m_sSubAreaID.Equals(value) == false)
				{
					m_sSubAreaID = value;
					OnStyleChanged(Enums.eCtrlProperty.PROP_SUBAREAID, value);
				}
			}
		}
		#endregion
		
		#region " Event Implementations"
		
		private CtrlStyleChangedEventHandler StyleChangedEvent;
		public event CtrlStyleChangedEventHandler StyleChanged
		{
			add
			{
				StyleChangedEvent = (CtrlStyleChangedEventHandler) System.Delegate.Combine(StyleChangedEvent, value);
			}
			remove
			{
				StyleChangedEvent = (CtrlStyleChangedEventHandler) System.Delegate.Remove(StyleChangedEvent, value);
			}
		}
		
		
		protected void OnStyleChanged(Enums.eCtrlProperty propertyName, object propertyValue)
		{
			CtrlStyle_EventArgs oArg = new CtrlStyle_EventArgs(propertyName, propertyValue);
			SetSaveFlag(propertyName);
			if (StyleChangedEvent != null)
				StyleChangedEvent(this, oArg);
		}
		
		#endregion
		
		#region " Function Implementations"
		
		public Color GetBorderColor(bool bHot)
		{
			
			try
			{
				if (bHot == true)
				{
					return this.BorderHotColor;
				}
				else
				{
					return this.BorderColor;
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.GetBorderColor()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Color.Empty;
			}
		}
		
		public Color GetBackColor(bool bHot, bool bPressed)
		{
			
			try
			{
				if (IsUseEventColor == true)
				{
					return GetEventBackColor(bHot, bPressed);
				}
				else
				{
					if (bHot == true)
					{
						if (bPressed == true)
						{
							return this.BackPressedColor;
						}
						else
						{
							return this.BackHotColor;
						}
					}
					else
					{
						if (BackColor < 0)
						{
							return Color.FromArgb(BackColor);
						}
						else if (BackColor > 0)
						{
                            return Color.FromKnownColor((KnownColor)BackColor);
						}
						else
						{
							return Color.White;
						}
					}
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.GetBackColor()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Color.Empty;
            }
			
		}
		
		public Color GetEventBackColor(bool bHot, bool bPressed)
		{
			
			try
			{
				if (bHot == true)
				{
					if (bPressed == true)
					{
						return this.EventBackPressedColor;
					}
					else
					{
						return this.EventBackHotColor;
					}
				}
				else
				{
					if (EventBackColor < 0)
					{
						return Color.FromArgb(EventBackColor);
					}
					else if (EventBackColor > 0)
					{
                        return Color.FromKnownColor((KnownColor)EventBackColor);
                    }
					else
					{
						return Color.White;
					}
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.GetEventBackColor()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Color.Empty;
            }
			
		}
		
		public Color GetTextColor()
		{
			
			try
			{
				if (TextColor < 0)
				{
					return Color.FromArgb(TextColor);
				}
				else
				{
					return Color.FromKnownColor((KnownColor)TextColor);
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.GetTextColor()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Color.Empty;
            }
			
		}
		
		public void SetSaveFlag(Enums.eCtrlProperty eSender, bool bSaveFlag)
		{
			
			try
			{
				IsSaveFlag = bSaveFlag;
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.SetSaveFlag()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}
		
		public void SetSaveFlag(Enums.eCtrlProperty eSender)
		{
			
			try
			{
				switch (eSender)
				{
					case Enums.eCtrlProperty.PROP_ISNOEVENT:
						
						break;
						
					case Enums.eCtrlProperty.PROP_ISPROCESSMODE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_ISUSEEVENTCOLOR:
						
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
						
						IsSaveFlag = true;
						break;
					case Enums.eCtrlProperty.PROP_LOCATION:
						
						IsSaveFlag = true;
						break;
					case Enums.eCtrlProperty.PROP_TOOLTYPE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_RESTAGFLAG:
						
						break;
						
					case Enums.eCtrlProperty.PROP_TEXT:
						
						break;
						
					case Enums.eCtrlProperty.PROP_TEXTCOLOR:
						
						break;
						
					case Enums.eCtrlProperty.PROP_TEXTSIZE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_TEXTSTYLE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_TEXTFONTNAME:
						
						break;
						
					case Enums.eCtrlProperty.PROP_PRIMARYSTATUS:
						
						break;
						
					case Enums.eCtrlProperty.PROP_UPDOWNFLAG:
						
						break;
						
					case Enums.eCtrlProperty.PROP_RESOURCETYPE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_LASTEVENT:
						
						break;
						
					case Enums.eCtrlProperty.PROP_PROCMODE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_CTRLMODE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_BACKCOLOR:
						
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
						
						break;
						
					case Enums.eCtrlProperty.PROP_EVENTBACKHOTCOLOR:
						
						break;
						
					case Enums.eCtrlProperty.PROP_EVENTBACKPRESSEDCOLOR:
						
						break;
						
					case Enums.eCtrlProperty.PROP_IMAGEINDEX:
						
						break;
						
					case Enums.eCtrlProperty.PROP_ISVIEWSIGNAL:
						
						break;
						
					case Enums.eCtrlProperty.PROP_ZOOMSCALE:
						
						break;
						
					case Enums.eCtrlProperty.PROP_TOOLTIPCOMMENT:
						
						break;
						
					case Enums.eCtrlProperty.PROP_SIGNALCOLOR:
						
						break;
						
					case Enums.eCtrlProperty.PROP_AREAID:
						
						break;
						
					case Enums.eCtrlProperty.PROP_SUBAREAID:
						
						break;
						
						
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.SetSaveFlag()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}
		
		public Point GetLocation()
		{
			
			try
			{
				Point ptLocation = new Point(this.Location.X + modDefines.CTRL_TRACKER_SIZE, this.Location.Y + modDefines.CTRL_TRACKER_SIZE);
				return ptLocation;
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.GetLocation()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Point.Empty;
			}
		}
		
		public void SetLocation(Point ptLocation)
		{
			
			try
			{
				this.Location = new Point(ptLocation.X - modDefines.CTRL_TRACKER_SIZE, ptLocation.Y - modDefines.CTRL_TRACKER_SIZE);
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.SetLocation()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}
		
		public Size GetSize()
		{
			
			try
			{
				Size szSize = new Size(this.Size.Width -(modDefines.CTRL_TRACKER_SIZE * 2 + 1), this.Size.Height -(modDefines.CTRL_TRACKER_SIZE * 2 + 1));
				return szSize;
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.GetSize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Size.Empty;
			}
		}
		
		public void SetSize(Size szSize)
		{
			
			try
			{
				this.Size = new Size(szSize.Width +(modDefines.CTRL_TRACKER_SIZE * 2 + 1), szSize.Height +(modDefines.CTRL_TRACKER_SIZE * 2 + 1));
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "clsCtrlStatus.SetSize()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
			
		}
		
		#endregion
		
	}
	
}
