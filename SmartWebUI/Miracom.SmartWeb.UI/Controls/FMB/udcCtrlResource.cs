using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;


namespace Miracom.FMBUI
{
	namespace Controls
	{
		
		public class udcCtrlResource : FMBUI.Controls.udcCtrlBase
		{
			
			
			#region " Windows Form Auto Generated Code "
			
			public udcCtrlResource()
			{
				
				//???¸ì¶œ?€ Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
				InitializeComponent();
				
				//InitializeComponent()ë¥??¸ì¶œ???¤ìŒ??ì´ˆê¸°???‘ì—…??ì¶”ê??˜ì‹­?œì˜¤.
				
			}
			
			public udcCtrlResource(ImageList imlRes)
			{
				
				//???¸ì¶œ?€ Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
				InitializeComponent();
				int i;
				for (i = 0; i <=  imlRes.Images.Count - 1; i++)
				{
					this.imlResource.Images.Add(imlRes.Images[i]);
				}
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
			
			//Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
			private System.ComponentModel.Container components = null;
			
			//ì°¸ê³ : ?¤ìŒ ?„ë¡œ?œì???Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
			//Windows Form ?”ì?´ë„ˆë¥??¬ìš©?˜ì—¬ ?˜ì •?????ˆìŠµ?ˆë‹¤.
			//ì½”ë“œ ?¸ì§‘ê¸°ë? ?¬ìš©?˜ì—¬ ?˜ì •?˜ì? ë§ˆì‹­?œì˜¤.
			public System.Windows.Forms.ImageList imlResource;
			[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
			{
				this.components = new System.ComponentModel.Container();
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(udcCtrlResource));
				this.imlResource = new System.Windows.Forms.ImageList(this.components);
				((System.ComponentModel.ISupportInitialize) this).BeginInit();
				//
				//imlResource
				//
				this.imlResource.ImageSize = new System.Drawing.Size(32, 32);
				this.imlResource.ImageStream = (System.Windows.Forms.ImageListStreamer) resources.GetObject("imlResource.ImageStream");
				this.imlResource.TransparentColor = System.Drawing.Color.Transparent;
				//
				//udcCtrlResource
				//
				this.Name = "udcCtrlResource";
				((System.ComponentModel.ISupportInitialize) this).EndInit();
				
			}
			
			#endregion
			
			protected override void DrawControl(Graphics g)
			{
				
				try
				{
					if (IsSelected == true && IsDesignMode == true)
					{
						GraphicsPath path = new GraphicsPath();
						path.AddRectangle(CtrlPos);
						Region meRegion = new Region(path);
						int i;
						for (i = 1; i <= GetTrackerCount(Enums.eLineType.Null); i++)
						{
							path.Reset();
							path.AddRectangle(GetTrackerRect(i));
							path.CloseFigure();
							meRegion.Union(path);
						}
						this.Region = meRegion;
						path.Dispose();
						meRegion.Dispose();
						
						DrawCtrlResource(g);
						DrawTracker(g, Enums.eLineType.Null);
					}
					else
					{
						GraphicsPath path = new GraphicsPath();
						path.AddRectangle(CtrlPos);
						path.CloseFigure();
						Region meRegion = new Region(path);
						this.Region = meRegion;
						path.Dispose();
						meRegion.Dispose();
						
						DrawCtrlResource(g);
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlResource.DrawControl()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlResource(Graphics g)
			{
				
				try
				{
					g.FillRectangle(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					g.DrawRectangle(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					
					int iSignalSize = 0;
					int iXPos = 0;
					int iYPos = 0;
					
					if (CtrlStatus.IsViewSignal == true)
					{
						int iRed = 0;
						int iGreen = 0;
						int iBlue = 0;
						iSignalSize = modDefines.CTRL_LIGHT_SIZE;
						if (CtrlStatus.UpDownFlag != "")
						{
							if (CtrlStatus.UpDownFlag.Substring(0, 1) == "D")
							{
								iRed = 1;
							}
						}
						
						if (CtrlStatus.PrimaryStatus == "PROC")
						{
							iGreen = 1;
						}
						
						if (CtrlStatus.IsProcessMode == true)
						{
							if (CtrlStatus.ProcMode != "")
							{
								if (CtrlStatus.ProcMode.Substring(0, 1) == "S")
								{
									iBlue = 1;
								}
								else if (CtrlStatus.ProcMode.Substring(0, 1) == "F")
								{
									iBlue = 2;
								}
							}
						}
						else
						{
							if (CtrlStatus.CtrlMode != "")
							{
                                if (string.IsNullOrEmpty(CtrlStatus.CtrlMode) == false)
                                {
                                    if (CtrlStatus.CtrlMode.Substring(0, 2) == "OL")
                                    {
                                        iBlue = 1;
                                    }
                                    else if (CtrlStatus.CtrlMode.Substring(0, 2) == "OR")
                                    {
                                        iBlue = 2;
                                    }
                                }
							}
						}
						
						g.FillRectangle(Brushes.White, CtrlPos.X, 0, modDefines.CTRL_LIGHT_SIZE, CtrlPos.Height);
						
						iYPos = CtrlPos.Y;
						if (iRed == 1) // Down
						{
							g.FillRectangle(Brushes.Red, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32((CtrlPos.Height - 1) / 4));
						}
						else
						{
							g.FillRectangle(Brushes.White, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32((CtrlPos.Height - 1) / 4));
						}
						
						iYPos += System.Convert.ToInt32((CtrlPos.Height - 1) / 4);
						
						g.FillRectangle(new SolidBrush(CtrlStatus.SignalColor), CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32((CtrlPos.Height - 1) / 4));
						
						iYPos += System.Convert.ToInt32((CtrlPos.Height - 1) / 4);
						if (iGreen == 1)
						{
							g.FillRectangle(Brushes.Lime, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32((CtrlPos.Height - 1) / 4));
						}
						else
						{
							g.FillRectangle(Brushes.White, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32((CtrlPos.Height - 1) / 4));
						}
						iYPos += System.Convert.ToInt32((CtrlPos.Height - 1) / 4);
						if (iBlue == 1)
						{
							g.FillRectangle(Brushes.Cyan, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32(CtrlPos.Bottom - iYPos));
						}
						else if (iBlue == 2)
						{
							g.FillRectangle(Brushes.Blue, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32(CtrlPos.Bottom - iYPos));
						}
						else
						{
							g.FillRectangle(Brushes.White, CtrlPos.X, iYPos, modDefines.CTRL_LIGHT_SIZE, System.Convert.ToInt32(CtrlPos.Bottom - iYPos));
						}
						
						g.DrawRectangle(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), CtrlPos.X, CtrlPos.Y, modDefines.CTRL_LIGHT_SIZE, CtrlPos.Height - 1);
					}
					else
					{
						iSignalSize = 0;
					}

                    StringFormat drawFormat = new StringFormat();
					if (CtrlStatus.ImageIndex > ListBox.NoMatches && imlResource.Images.Count > CtrlStatus.ImageIndex)
					{
						iXPos = System.Convert.ToInt32(CtrlPos.X + iSignalSize +(CtrlPos.Width - iSignalSize) / 2 -(imlResource.ImageSize.Width * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)) / 2);
						iYPos = System.Convert.ToInt32(CtrlPos.Y +(CtrlPos.Height - CtrlStatus.TextSize) / 2 -(imlResource.ImageSize.Height * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)) / 2 - 1);
						if (iXPos < iSignalSize + 2)
						{
							iXPos = iSignalSize + 2;
						}
						if (iYPos < 2)
						{
							iYPos = 2;
						}
						Rectangle rcImage = new Rectangle(iXPos, iYPos, Convert.ToInt32(imlResource.ImageSize.Width * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), Convert.ToInt32(imlResource.ImageSize.Height * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)));
						if (IsPressed == true)
						{
							rcImage.Location = new Point(rcImage.Left + 1, rcImage.Top + 1);
						}
						g.DrawImage(imlResource.Images[CtrlStatus.ImageIndex], rcImage);
						drawFormat.LineAlignment = StringAlignment.Far;
					}
					else
					{
						drawFormat.LineAlignment = StringAlignment.Center;
					}
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X + iSignalSize), System.Convert.ToSingle(CtrlPos.Y), System.Convert.ToSingle(CtrlPos.Width - iSignalSize), System.Convert.ToSingle(CtrlPos.Height - 1));
					if (IsPressed == true)
					{
						rcText.Location = new PointF(System.Convert.ToSingle(rcText.Left + 1), System.Convert.ToSingle(rcText.Top + 1));
					}
					Brush drawBrush;
					if (this.Enabled == true)
					{
						drawBrush = new SolidBrush(CtrlStatus.GetTextColor());
					}
					else
					{
						drawBrush = new SolidBrush(Color.FromArgb(128, 128, 128));
					}
                    g.DrawString(CtrlStatus.Text, 
                        new Font(CtrlStatus.TextFontName, System.Convert.ToInt32(CtrlStatus.TextSize * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), 
                        ((FontStyle)(Enum.Parse(typeof(FontStyle), Enum.GetValues(typeof(FontStyle)).GetValue(CtrlStatus.TextStyle).ToString())))), 
                        drawBrush, rcText, drawFormat);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlResource.DrawCtrlResource()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
		}
		
	}
	
	
}
