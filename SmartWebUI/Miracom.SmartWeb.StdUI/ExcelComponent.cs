namespace Miracom.SmartWeb
{
	public sealed class ExcelComponent
	{
		public const int EXCEL_MAX_COL = 255;    //(EXCel에서 허용하는 최대 Column수 -1)

		public enum XlHAlign
		{
			xlHAlignCenter = -4108,
			xlHAlignCenterAcrossSelection = 7,
			xlHAlignDistributed = -41117,
			xlHAlignFill = 5,
			xlHAlignGeneral = 1,
			xlHAlignJustify = -4130,
			xlHAlignLeft = -4131,
			xlHAlignRight = -4152
		}

		public enum XlVAlign
		{
			xlValignBottom = -4107,
			xlValignCenter = -4108,
			xlValignDistributed = -4117,
			xlValignJustify = -4130,
			xlValignTop = -4160
		}

		public enum XlBordersIndex
		{
			xlDiagonalDown = 5,
			xlDiagonalUp = 6,
			xlEdgeBottom = 9,
			xlEdgeLeft = 7,
			xlEdgeRight = 10,
			xlEdgeTop = 8,
			xlInsideHorizontal = 12,
			xlInsideVertical = 11
		}

		public enum XlLineStyle
		{
			xlContinuous = 1,
			xlDash = -4115,
			xlDashDot = 4,
			xlDashDotDot = 5,
			xlDot = -4118,
			xlDouble = -4119,
			xlLineStyleNone = -4142,
			xlSlantDashDot = 13
		}

		public enum XlBorderWeight
		{
			xlHairline = 1,
			xlMedium = -4138,
			xlThick = 4,
			xlThin = 2
		}
	}
}
