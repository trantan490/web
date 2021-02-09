using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System;

namespace Miracom.FMBUI
{
	sealed class modCommonFunctions
	{
		
		public static Rectangle InflateRectangle(Rectangle rcPos, int iInflateSize)
		{
			
			try
			{
				Rectangle rcInflate;
				if (iInflateSize > 0)
				{
					rcInflate = new Rectangle(rcPos.X + iInflateSize, rcPos.Y + iInflateSize, rcPos.Width +(iInflateSize * 2 + 1), rcPos.Height +(iInflateSize * 2 + 1));
				}
				else if (iInflateSize < 0)
				{
					rcInflate = new Rectangle(rcPos.X - iInflateSize, rcPos.Y - iInflateSize, rcPos.Width +(iInflateSize * 2), rcPos.Height +(iInflateSize * 2));
				}
				else
				{
					rcInflate = rcPos;
				}
				
				return rcInflate;
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "modCommonFunctions.InflateRectangle()", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Rectangle.Empty;
			}
		}
		
		public static double GetScale(int iZoomScale)
		{
			
			double dScale = 1;
			
			try
			{
				switch (iZoomScale)
				{
					case 5:
						
						dScale = 2;
						break;
					case 4:
						
						dScale = 1.8;
						break;
					case 3:
						
						dScale = 1.6;
						break;
					case 2:
						
						dScale = 1.4;
						break;
					case 1:
						
						dScale = 1.2;
						break;
					case 0:
						
						dScale = 1;
						break;
					case - 1:
						
						dScale = 0.9;
						break;
					case - 2:
						
						dScale = 0.8;
						break;
					case - 3:
						
						dScale = 0.7;
						break;
					case - 4:
						
						dScale = 0.6;
						break;
					case - 5:
						
						dScale = 0.5;
						break;
				}
				
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "modCommonFunctions.GetScale()", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
			return dScale;
			
		}
		
	}
	
}
