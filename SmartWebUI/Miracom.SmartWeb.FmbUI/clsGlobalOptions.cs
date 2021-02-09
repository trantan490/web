
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using Miracom.SmartWeb.FWX;
//-----------------------------------------------------------------------------
//
//   System      : FMBClient
//   File Name   : clsGlobalOptions.vb
//   Description : Global Options Class
//
//   FMB Version : 1.0.0
//
//   Function List
//
//   Detail Description
//       -
//
//   History
//       - 2005-03-22 : Created by Laverwon
//
//   Copyright(C) 1998-2004 MIRACOM,INC.
//   All rights reserved.
//
//-----------------------------------------------------------------------------

namespace Miracom.SmartWeb.UI
{
	public class clsGlobalOptions : ArrayList
	{
		
		
		public clsGlobalOptions()
		{
			
		}
		
		public bool AddOption(string factory, clsOptionData.Options opt, object data)
		{
			
			try
			{
				int i;
				for (i = 0; i <= this.Count - 1; i++)
				{
					if (((clsOptionData) this[i]).GetOptionData(clsOptionData.Options.Factory, "").ToString() == factory)
					{
						((clsOptionData) this[i]).SetOptionData(data, opt);
						return true;
					}
				}
				clsOptionData OptionData = new clsOptionData(factory);
				OptionData.SetOptionData(data, opt);
				this.Add(OptionData);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.AddOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		public bool AddOption(string factory, string sEventID, Color clEventColor)
		{
			
			try
			{
				clsEventColor objEvent = new clsEventColor(sEventID, clEventColor);
				
				int i;
				for (i = 0; i <= this.Count - 1; i++)
				{
					if (((clsOptionData) this[i]).GetOptionData(clsOptionData.Options.Factory, "").ToString() == factory)
					{
                        ((clsOptionData)this[i]).SetOptionData(objEvent, clsOptionData.Options.EventColor);
						return true;
					}
				}
				clsOptionData OptionData = new clsOptionData(factory);
				OptionData.SetOptionData(objEvent, clsOptionData.Options.EventColor);
				this.Add(OptionData);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.AddOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		public bool AddOptions(clsOptionData options)
		{
			
			try
			{
				int i;
				for (i = 0; i <= this.Count - 1; i++)
				{
                    if (((clsOptionData)this[i]).GetOptionData(clsOptionData.Options.Factory, "").ToString() == options.GetOptionData(clsOptionData.Options.Factory, "").ToString())
					{
						this.RemoveAt(i);
						break;
					}
				}
				this.Add(options);
				
				return true;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.AddOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		public object GetOptions(string factory, clsOptionData.Options opt)
		{
			
			try
			{
				clsOptionData OptionData = GetItem(factory);
				if (OptionData != null)
				{
					object Options = OptionData.GetOptionData(opt, "");
					if (Options != null)
					{
						return Options;
					}
				}
				
				return GetItem("SYSTEM").GetOptionData(opt, "");
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.GetOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
		public Color GetOptions(string factory, string sEventID)
		{
			
			try
			{
				clsOptionData OptionData = GetItem(factory);
				if (OptionData != null)
				{
                    object OOptions = OptionData.GetOptionData(clsOptionData.Options.EventColor, sEventID);
                    if (OOptions != null)
                    {
                        clsEventColor Options = (clsEventColor)OOptions;
                        return ((Color)Options.GetEventColor());
                    }
                    else
                    {
                        return Color.Empty;
                    }

                    //clsEventColor Options = (clsEventColor)OptionData.GetOptionData(clsOptionData.Options.EventColor, sEventID);
                    //if (Options != null)
                    //{
                    //    return ((Color)Options.GetEventColor());
                    //}
				}
                if ((GetItem("SYSTEM").GetOptionData(clsOptionData.Options.EventColor, sEventID)) != null)
                    return (Color)(GetItem("SYSTEM").GetOptionData(clsOptionData.Options.EventColor, sEventID));
                else
                    return Color.Empty;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.GetOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return Color.Empty;
			}
			
		}
		
		public clsOptionData GetItem(string factory)
		{
			
			try
			{
				int i;
				for (i = 0; i <= this.Count - 1; i++)
				{
                    if (((clsOptionData)this[i]).GetOptionData(clsOptionData.Options.Factory, "").ToString() == factory)
					{
                        return ((clsOptionData)this[i]);
//						break;
					}
				}
				
				return null;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.GetItem()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return null;
			}
			
		}
		
		public bool RemoveOptions(string factory)
		{
			
			try
			{
				int i;
				for (i = 0; i <= this.Count - 1; i++)
				{
                    if (((clsOptionData)this[i]).GetOptionData(clsOptionData.Options.Factory, "").ToString() == factory)
					{
						this.RemoveAt(i);
						return true;
					}
				}
				
				return false;
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsGlobalOptions.GetOptions()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
				return false;
			}
			
		}
		
	}
	
	public class clsOptionData : object
	{
		
		
		public clsOptionData(string _factory)
		{
			
			Factory = _factory;
			
		}
		
		public enum Options
		{
			Factory = 0,
			DefaultLayoutSize = 1,
			DefaultUDRSize = 2,
			DefaultResourceSize = 3,
			DefaultRectangleSize = 4,
			DefaultEllipseSize = 5,
			DefaultTriangleSize = 6,
			DefaultVerticalLineSize = 7,
			DefaultHorizontalLineSize = 8,
			DefaultPie1Size = 9,
			DefaultPie2Size = 10,
			DefaultPie3Size = 11,
			DefaultPie4Size = 12,
			DefaultFontName = 13,
			UseEventColor = 14,
			EventColor = 15,
			IsProcessMode = 16,
			DefaultTextColor = 17,
			DefaultTextSize = 18,
			DefaultBackColor = 19
		}
		
		private string Factory = null;
        private Size DefaultLayoutSize = Size.Empty;
        private Size DefaultUDRSize = Size.Empty;
        private Size DefaultResourceSize = Size.Empty;
        private Size DefaultRectangleSize = Size.Empty;
        private Size DefaultEllipseSize = Size.Empty;
        private Size DefaultTriangleSize = Size.Empty;
        private Size DefaultVerticalLineSize = Size.Empty;
        private Size DefaultHorizontalLineSize = Size.Empty;
        private Size DefaultPie1Size = Size.Empty;
        private Size DefaultPie2Size = Size.Empty;
        private Size DefaultPie3Size = Size.Empty;
        private Size DefaultPie4Size = Size.Empty;
		private string DefaultFontName = null;
		private string UseEventColor = null;
		private string IsProcessMode = null;
        private Color DefaultTextColor = Color.Empty;
		private int DefaultTextSize = 0;
        private Color DefaultBackColor = Color.Empty;
		
		private ArrayList EventColorList = new ArrayList();
		
		public void SetOptionData(object data, Options opt)
		{
			
			try
			{
				switch (opt)
				{
					case Options.Factory:
						
						Factory = System.Convert.ToString(data);
						break;
					case Options.DefaultLayoutSize:
						
						DefaultLayoutSize = (Size) data;
						break;
					case Options.DefaultUDRSize:
						
						DefaultUDRSize = (Size) data;
						break;
					case Options.DefaultResourceSize:
						
						DefaultResourceSize = (Size) data;
						break;
					case Options.DefaultRectangleSize:
						
						DefaultRectangleSize = (Size) data;
						break;
					case Options.DefaultEllipseSize:
						
						DefaultEllipseSize = (Size) data;
						break;
					case Options.DefaultTriangleSize:
						
						DefaultTriangleSize = (Size) data;
						break;
					case Options.DefaultVerticalLineSize:
						
						DefaultVerticalLineSize = (Size) data;
						break;
					case Options.DefaultHorizontalLineSize:
						
						DefaultHorizontalLineSize = (Size) data;
						break;
					case Options.DefaultPie1Size:
						
						DefaultPie1Size = (Size) data;
						break;
					case Options.DefaultPie2Size:
						
						DefaultPie2Size = (Size) data;
						break;
					case Options.DefaultPie3Size:
						
						DefaultPie3Size = (Size) data;
						break;
					case Options.DefaultPie4Size:
						
						DefaultPie4Size = (Size) data;
						break;
					case Options.DefaultFontName:
						
						DefaultFontName = System.Convert.ToString(data);
						break;
					case Options.UseEventColor:
						
						UseEventColor = System.Convert.ToString(data);
						break;
					case Options.EventColor:
						
						int i;
						for (i = 0; i <= EventColorList.Count - 1; i++)
						{
							if (((clsEventColor) EventColorList[i]).GetEventID() == ((clsEventColor) data).GetEventID())
							{
								((clsEventColor) EventColorList[i]).SetEventColor(((clsEventColor) data).GetEventID().ToString(), (Color)(((clsEventColor) data).GetEventColor()));
								goto endOfSelect;
							}
						}
						EventColorList.Add(data);
						break;
					case Options.IsProcessMode:
						
						IsProcessMode = System.Convert.ToString(data);
						break;
					case Options.DefaultTextColor:
						
						DefaultTextColor = (Color) data;
						break;
					case Options.DefaultTextSize:

                        DefaultTextSize = System.Convert.ToInt32(data);
						break;
					case Options.DefaultBackColor:
						
						DefaultBackColor = (Color) data;
						break;
				}
endOfSelect:
				1.GetHashCode() ; //nop
				
			}
			catch (Exception ex)
			{
				CmnFunction.ShowMsgBox("clsOptionData.SetOptionData()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
			}
			
		}
		
		public object GetOptionData(Options opt, string sEventID)
		{
			
			try
		    {
			    switch (opt)
			    {
				    case Options.Factory:
						
					    return Factory;
                    case Options.DefaultLayoutSize:

                        return DefaultLayoutSize;
                    case Options.DefaultUDRSize:

                        return DefaultUDRSize;
                    case Options.DefaultResourceSize:

                        return DefaultResourceSize;
                    case Options.DefaultRectangleSize:

                        return DefaultRectangleSize;
                    case Options.DefaultEllipseSize:

                        return DefaultEllipseSize;
                    case Options.DefaultTriangleSize:

                        return DefaultTriangleSize;
                    case Options.DefaultVerticalLineSize:

                        return DefaultVerticalLineSize;
                    case Options.DefaultHorizontalLineSize:

                        return DefaultHorizontalLineSize;
                    case Options.DefaultPie1Size:

                        return DefaultPie1Size;
                    case Options.DefaultPie2Size:

                        return DefaultPie2Size;
                    case Options.DefaultPie3Size:

                        return DefaultPie3Size;
                    case Options.DefaultPie4Size:

                        return DefaultPie4Size;
                    case Options.DefaultFontName:

                        return DefaultFontName;
                    case Options.UseEventColor:

                        return UseEventColor;
                    case Options.EventColor:

                        int i;
                        for (i = 0; i <= EventColorList.Count - 1; i++)
                        {
                            if (System.Convert.ToString(((clsEventColor)EventColorList[i]).GetEventID()) == sEventID)
                            {
                                return ((clsEventColor)EventColorList[i]);
                            }
                        }
                        break;
                    case Options.IsProcessMode:

                        return IsProcessMode;
                    case Options.DefaultTextColor:

                        return DefaultTextColor;
                    case Options.DefaultTextSize:

                        return DefaultTextSize;
                    case Options.DefaultBackColor:

                        return DefaultBackColor;
                  }

                  return null;
						
		    }
		    catch (Exception ex)
		    {
			    CmnFunction.ShowMsgBox("clsOptionData.GetOptionData()" + "\r\n" + ex.Message, "FMB Client", MessageBoxButtons.OK, 1);
                return null;
            }
				
	    }
			
    }
	
	public class clsEventColor : object
	{
		public clsEventColor(string sEventID, Color clEventColor)
		{
			
			SetEventColor(sEventID, clEventColor);
		}
		
		private string EventID;
		private Color EventColor;
		
		public void SetEventColor(string sEventID, Color clEventColor)
		{
			
			EventID = sEventID;
			EventColor = clEventColor;
			
		}
		
		public object GetEventID()
		{
			
			return EventID;
			
		}
		
		public object GetEventColor()
		{
			
			return EventColor;
			
		}
		
	}
	
}
