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
		
		public class udcCtrlTag : FMBUI.Controls.udcCtrlBase
		{
			
			
			#region " Windows Form Auto Generated Code "
			
			public udcCtrlTag()
			{
				
				//???¸ì¶œ?€ Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
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
			
			//Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
			private System.ComponentModel.Container components = null;
			
			//ì°¸ê³ : ?¤ìŒ ?„ë¡œ?œì???Windows Form ?”ì?´ë„ˆ???„ìš”?©ë‹ˆ??
			//Windows Form ?”ì?´ë„ˆë¥??¬ìš©?˜ì—¬ ?˜ì •?????ˆìŠµ?ˆë‹¤.
			//ì½”ë“œ ?¸ì§‘ê¸°ë? ?¬ìš©?˜ì—¬ ?˜ì •?˜ì? ë§ˆì‹­?œì˜¤.
			[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
			{
				((System.ComponentModel.ISupportInitialize) this).BeginInit();
				//
				//udcCtrlTag
				//
				this.Name = "udcCtrlTag";
				((System.ComponentModel.ISupportInitialize) this).EndInit();
				
			}
			
			#endregion
			
			protected override void DrawControl(Graphics g)
			{
				
				try
				{
					switch (CtrlStatus.ToolType)
					{
						case Enums.eToolType.Rectangle:
							
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
								
								DrawCtrlRectangle(g);
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
								
								DrawCtrlRectangle(g);
							}
							break;
						case Enums.eToolType.Ellipse:
							
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddEllipse(CtrlPos);
								path.CloseFigure();
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
								
								DrawCtrlEllipse(g);
								DrawTracker(g, Enums.eLineType.Null);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								path.AddEllipse(CtrlPos);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlEllipse(g);
							}
							break;
						case Enums.eToolType.Triangle:
							
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								Point ptLeft = new Point(CtrlPos.Left, CtrlPos.Bottom);
								Point ptCenter = new Point(CtrlPos.Left + CtrlPos.Width / 2, CtrlPos.Top);
								Point ptRight = new Point(CtrlPos.Right, CtrlPos.Bottom);
								path.AddLine(ptLeft, ptCenter);
								path.AddLine(ptCenter, ptRight);
								path.AddLine(ptRight, ptLeft);
								path.CloseFigure();
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
								
								DrawCtrlTriangle(g);
								DrawTracker(g, Enums.eLineType.Null);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								Point ptLeft = new Point(CtrlPos.Left, CtrlPos.Bottom);
								Point ptCenter = new Point(CtrlPos.Left + CtrlPos.Width / 2, CtrlPos.Top);
								Point ptRight = new Point(CtrlPos.Right, CtrlPos.Bottom);
								path.AddLine(ptLeft, ptCenter);
								path.AddLine(ptCenter, ptRight);
								path.AddLine(ptRight, ptLeft);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlTriangle(g);
							}
							break;
						case Enums.eToolType.VerticalLine:
							
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddRectangle(CtrlPos);
								Region meRegion = new Region(path);
								int i;
								for (i = 1; i <= GetTrackerCount(Enums.eLineType.Vertical); i++)
								{
									path.Reset();
									path.AddRectangle(GetTrackerRect(i));
									path.CloseFigure();
									meRegion.Union(path);
								}
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlVerticalLine(g);
								DrawTracker(g, Enums.eLineType.Vertical);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								if (CtrlPos.Width < 2)
								{
									CtrlPos = new Rectangle(CtrlPos.X, CtrlPos.Y, 2, CtrlPos.Height);
								}
								path.AddRectangle(CtrlPos);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlVerticalLine(g);
							}
							break;
						case Enums.eToolType.HorizontalLine:
							
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddRectangle(CtrlPos);
								Region meRegion = new Region(path);
								int i;
								for (i = 3; i <= GetTrackerCount(Enums.eLineType.Horizontal); i++)
								{
									path.Reset();
									path.AddRectangle(GetTrackerRect(i));
									path.CloseFigure();
									meRegion.Union(path);
								}
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlHorizontalLine(g);
								DrawTracker(g, Enums.eLineType.Horizontal);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								if (CtrlPos.Height < 2)
								{
									CtrlPos = new Rectangle(CtrlPos.X, CtrlPos.Y, CtrlPos.Width, 2);
								}
								path.AddRectangle(CtrlPos);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlHorizontalLine(g);
							}
							break;
						case Enums.eToolType.PieType1:
							
							float sStartAngle = 270;
							float sSweepAngle = 90;
							Rectangle newRect = new Rectangle(CtrlPos.X - CtrlPos.Width, CtrlPos.Y, CtrlPos.Width * 2, CtrlPos.Height * 2);
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect, sStartAngle, sSweepAngle);
								path.CloseFigure();
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
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect, sStartAngle, sSweepAngle);
								DrawTracker(g, Enums.eLineType.Null);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect, sStartAngle, sSweepAngle);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect, sStartAngle, sSweepAngle);
							}
							break;
						case Enums.eToolType.PieType2:
							
							float sStartAngle_1 = 0;
							float sSweepAngle_1 = 90;
							Rectangle newRect_1 = new Rectangle(CtrlPos.X - CtrlPos.Width, CtrlPos.Y - CtrlPos.Height, CtrlPos.Width * 2, CtrlPos.Height * 2);
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect_1, sStartAngle_1, sSweepAngle_1);
								path.CloseFigure();
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
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect_1, sStartAngle_1, sSweepAngle_1);
								DrawTracker(g, Enums.eLineType.Null);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect_1, sStartAngle_1, sSweepAngle_1);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect_1, sStartAngle_1, sSweepAngle_1);
							}
							break;
						case Enums.eToolType.PieType3:
							
							float sStartAngle_2 = 90;
							float sSweepAngle_2 = 90;
							Rectangle newRect_2 = new Rectangle(CtrlPos.X, CtrlPos.Y - CtrlPos.Height, CtrlPos.Width * 2, CtrlPos.Height * 2);
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect_2, sStartAngle_2, sSweepAngle_2);
								path.CloseFigure();
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
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect_2, sStartAngle_2, sSweepAngle_2);
								DrawTracker(g, Enums.eLineType.Null);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect_2, sStartAngle_2, sSweepAngle_2);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect_2, sStartAngle_2, sSweepAngle_2);
							}
							break;
						case Enums.eToolType.PieType4:
							
							float sStartAngle_3 = 180;
							float sSweepAngle_3 = 90;
							Rectangle newRect_3 = new Rectangle(CtrlPos.X, CtrlPos.Y, CtrlPos.Width * 2, CtrlPos.Height * 2);
							if (IsSelected == true && IsDesignMode == true)
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect_3, sStartAngle_3, sSweepAngle_3);
								path.CloseFigure();
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
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect_3, sStartAngle_3, sSweepAngle_3);
								DrawTracker(g, Enums.eLineType.Null);
							}
							else
							{
								GraphicsPath path = new GraphicsPath();
								path.AddPie(newRect_3, sStartAngle_3, sSweepAngle_3);
								path.CloseFigure();
								Region meRegion = new Region(path);
								this.Region = meRegion;
								path.Dispose();
								meRegion.Dispose();
								
								DrawCtrlPieType(g, CtrlStatus.ToolType, newRect_3, sStartAngle_3, sSweepAngle_3);
							}
							break;
						case Enums.eToolType.TextType:
							
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
								
								DrawCtrlText(g);
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
								
								DrawCtrlText(g);
							}
							break;
					}
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawControl()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlRectangle(Graphics g)
			{
				
				try
				{
					g.FillRectangle(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					g.DrawRectangle(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					
					StringFormat drawFormat = new StringFormat();
					drawFormat.LineAlignment = StringAlignment.Center;
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X), System.Convert.ToSingle(CtrlPos.Y), System.Convert.ToSingle(CtrlPos.Width), System.Convert.ToSingle(CtrlPos.Height));
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

                    g.DrawString(CtrlStatus.Text, new Font(CtrlStatus.TextFontName, System.Convert.ToInt32(CtrlStatus.TextSize * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), ((FontStyle)(Enum.Parse(typeof(FontStyle), Enum.GetValues(typeof(FontStyle)).GetValue(CtrlStatus.TextStyle).ToString())))), drawBrush, rcText, drawFormat);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlRectangle()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlEllipse(Graphics g)
			{
				
				try
				{
					g.FillEllipse(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos);
					g.DrawEllipse(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), CtrlPos.X + 1, CtrlPos.Y + 1, CtrlPos.Width - 2, CtrlPos.Height - 2);

                    StringFormat drawFormat = new StringFormat();
					drawFormat.LineAlignment = StringAlignment.Center;
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X), System.Convert.ToSingle(CtrlPos.Y), System.Convert.ToSingle(CtrlPos.Width), System.Convert.ToSingle(CtrlPos.Height));
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
                    g.DrawString(CtrlStatus.Text, new Font(CtrlStatus.TextFontName, System.Convert.ToInt32(CtrlStatus.TextSize * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), ((FontStyle)(Enum.Parse(typeof(FontStyle), Enum.GetValues(typeof(FontStyle)).GetValue(CtrlStatus.TextStyle).ToString())))), drawBrush, rcText, drawFormat);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlEllipse()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlTriangle(Graphics g)
			{
				
				try
				{
					GraphicsPath path = new GraphicsPath();
					Point ptLeft = new Point(CtrlPos.Left + 1, CtrlPos.Bottom - 1);
					Point ptCenter = new Point(CtrlPos.Left + CtrlPos.Width / 2, CtrlPos.Top + 1);
					Point ptRight = new Point(CtrlPos.Right - 1, CtrlPos.Bottom - 1);
					path.AddLine(ptLeft, ptCenter);
					path.AddLine(ptCenter, ptRight);
					path.AddLine(ptRight, ptLeft);
					
					g.FillPath(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), path);
					g.DrawPath(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), path);

                    StringFormat drawFormat = new StringFormat();
					drawFormat.LineAlignment = StringAlignment.Near;
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X + CtrlPos.Width / 4), System.Convert.ToSingle(CtrlPos.Y + CtrlPos.Height / 2), System.Convert.ToSingle(CtrlPos.Width / 2), System.Convert.ToSingle(CtrlPos.Height / 2));
					
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
                    g.DrawString(CtrlStatus.Text, new Font(CtrlStatus.TextFontName, System.Convert.ToInt32(CtrlStatus.TextSize * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), ((FontStyle)(Enum.Parse(typeof(FontStyle), Enum.GetValues(typeof(FontStyle)).GetValue(CtrlStatus.TextStyle).ToString())))), drawBrush, rcText, drawFormat);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlTriangle()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlRoundRectangle(Graphics g)
			{
				
				try
				{
					g.FillRectangle(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					g.DrawRectangle(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);

                    StringFormat drawFormat = new StringFormat();
					drawFormat.LineAlignment = StringAlignment.Center;
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X), System.Convert.ToSingle(CtrlPos.Y), System.Convert.ToSingle(CtrlPos.Width), System.Convert.ToSingle(CtrlPos.Height));
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

                    g.DrawString(CtrlStatus.Text, new Font(CtrlStatus.TextFontName, System.Convert.ToInt32(CtrlStatus.TextSize * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), ((FontStyle)(Enum.Parse(typeof(FontStyle), Enum.GetValues(typeof(FontStyle)).GetValue(CtrlStatus.TextStyle).ToString())))), drawBrush, rcText, drawFormat);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlRectangle()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlVerticalLine(Graphics g)
			{
				
				try
				{
					g.FillRectangle(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					g.DrawRectangle(new Pen(CtrlStatus.GetBackColor(IsHot, IsPressed), 1), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlRectangle()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlHorizontalLine(Graphics g)
			{
				
				try
				{
					g.FillRectangle(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					g.DrawRectangle(new Pen(CtrlStatus.GetBackColor(IsHot, IsPressed), 1), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlHorizontalLine()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlPieType(Graphics g, Enums.eToolType eType, Rectangle newRect, float sStartAngle, float sSweepAngle)
			{
				
				try
				{
					g.FillPie(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), newRect, sStartAngle, sSweepAngle);
					switch (eType)
					{
						case Enums.eToolType.PieType1:
							
							g.DrawPie(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), newRect.X + 1, newRect.Y + 1, newRect.Width - 2, newRect.Height - 3, sStartAngle, sSweepAngle);
							break;
						case Enums.eToolType.PieType2:
							
							g.DrawPie(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), newRect.X + 1, newRect.Y + 1, newRect.Width - 2, newRect.Height - 2, sStartAngle, sSweepAngle);
							break;
						case Enums.eToolType.PieType3:
							
							g.DrawPie(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), newRect.X + 1, newRect.Y + 1, newRect.Width - 3, newRect.Height - 2, sStartAngle, sSweepAngle);
							break;
						case Enums.eToolType.PieType4:
							
							g.DrawPie(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), newRect.X + 1, newRect.Y + 1, newRect.Width - 3, newRect.Height - 3, sStartAngle, sSweepAngle);
							break;
					}

                    StringFormat drawFormat = new StringFormat();
					drawFormat.LineAlignment = StringAlignment.Center;
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X), System.Convert.ToSingle(CtrlPos.Y), System.Convert.ToSingle(CtrlPos.Width), System.Convert.ToSingle(CtrlPos.Height));
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
                    g.DrawString(CtrlStatus.Text, new Font(CtrlStatus.TextFontName, System.Convert.ToInt32(CtrlStatus.TextSize * modCommonFunctions.GetScale(this.CtrlStatus.ZoomScale)), ((FontStyle)(Enum.Parse(typeof(FontStyle), Enum.GetValues(typeof(FontStyle)).GetValue(CtrlStatus.TextStyle).ToString())))), drawBrush, rcText, drawFormat);
					
				}
				catch (Exception ex)
				{
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlPieType()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
			private void DrawCtrlText(Graphics g)
			{
				
				try
				{
					//Me.BackColor = Color.FromArgb(0, 255, 255, 255)
					g.FillRectangle(new SolidBrush(CtrlStatus.GetBackColor(IsHot, IsPressed)), CtrlPos.X, CtrlPos.Y, CtrlPos.Width, CtrlPos.Height);
					if (IsDesignMode == true)
					{
						g.DrawRectangle(new Pen(CtrlStatus.GetBorderColor(IsHot), 1), CtrlPos.X, CtrlPos.Y, CtrlPos.Width - 1, CtrlPos.Height - 1);
					}

                    StringFormat drawFormat = new StringFormat();
					drawFormat.LineAlignment = StringAlignment.Center;
					drawFormat.Alignment = StringAlignment.Center;
					RectangleF rcText = new RectangleF(System.Convert.ToSingle(CtrlPos.X), System.Convert.ToSingle(CtrlPos.Y), System.Convert.ToSingle(CtrlPos.Width), System.Convert.ToSingle(CtrlPos.Height));
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
                    MessageBox.Show(ex.Message, "udcCtrlTag.DrawCtrlText()", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				
			}
			
		}
		
		
	}
	
}
