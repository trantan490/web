//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Data;
//using System.Text;
//using System.IO;
//using System.Windows.Forms;
//using System.Configuration;
//using Miracom.SmartWeb.FWX;
//using Miracom.UI;

//using Steema.TeeChart;



//namespace Miracom.SmartWeb.UI
//{
//    public sealed class TChartBar
//    {

//        #region "Constant Definition"
//        public const string CHART_BACK_COLOR_1 = "&h06063D";
//        public const string CHART_BACK_COLOR_2 = "Yellow";
//        #endregion

//        #region "Variable Definition"
//        //Member Variable
//        private static string m_chart_script = "";
//        private static int m_series_count = 0;
//        private static TChart.Axis m_axis = TChart.Axis.taLeft;
//        private static bool m_axis_bottom = true;
//        private static bool m_view3d = false;
//        private static string m_tchart_name ;
//        private static string m_tcommander_name;
//        private static bool m_tchart_exporttojpg = true;
//        private static int m_Axes_Left_Maximum = 0;
//        private static int m_Axes_Left_Minimum = 0;
//        public static string[] m_Titles = new string[] {""};
//        //Member Class
//        MTEXTBar m_header = new MTEXTBar();
//        private m_footer As New MTEXTBar
//        //Private m_legend As New MLEGENDBar
//        //Private m_series() As MSERIESBar
//        #endregion


//        public class MTEXTBar
//        {
//            //Member Variable
//            private static string m_text = "";
//            private string m_color = "vbYellow";
//            private TChart.Alignment m_alignment = TChart.Alignment.taCenter;

//            #region "Properties"

//            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//            public string TextBar
//            {
//                get
//                {
//                    if (m_text == "") 
//                    { 
//                        return ""; 
//                    } 
//                    else 
//                    {
//                        return m_text; 
//                    }
//                }
//                set
//                {
//                    m_text = value;
//                }
//            }            

//            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//            public string ColorBar
//            {
//                get
//                {
//                   return m_color;
//                }
//                set
//                {
//                    m_color = value;
//                }
//            }

//            [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
//            public string AlignmentBar
//            {
//                get
//                {
//                   return m_alignment;
//                }
//                set
//                {
//                    m_alignment = value;
//                }
//            }

//            #endregion

//            public static string ClearBar() { m_text = "";}

//        }



//    }
//}

//namespace TChart
//{
//    public enum ChartType
//    {
//        scLine = 0,
//        scBar = 1,
//        scHorizBar = 2,
//        scArea = 3,
//        scPoint = 4,
//        scPie = 5,
//        scFastLine = 6,
//        scShape = 7,
//        scGantt = 8,
//        scBubble = 9,
//        scArrow = 10,
//        scCandle = 11,
//        scPolar = 12,
//        scSurface = 13,
//        scVolume = 14,
//        scErrorBar = 15,
//        scBezier = 16,
//        scContour = 17,
//        scError = 18,
//        scPoint3D = 19,
//        scRadar = 20,
//        scClock = 21,
//        scWindRose = 22,
//        scBar3D = 23,
//        scImageBar = 24
//    }

//    public enum EMarkStyle
//    {
//        smsValue = 0, //1234
//        smsPercent = 1, //12 %
//        smsLabel = 2, //Cars
//        smsLabelPercent = 3, //Cars 12 %
//        smsLabelValue = 4, //Cars 1234
//        smsLegend = 5, //(Depends on LegendTextStyle)
//        smsPercentTotal = 6, //12 % of 1234
//        smsLabelPercentTotal = 7, //Cars 12 % of 1234
//        smsXValue = 8 //{ 21/6/1996 }
//    }

//    public enum EGradientDirection
//    {
//        gdTopBottom = 0,
//        gdBottomTop = 1,
//        gdLeftRight = 2,
//        gdRightLeft = 3,
//        gdFromCenter = 4,
//        gdFromTopLeft = 5,
//        gdFromBottomLeft = 6
//    }

//    public enum EConstants
//    {
//        clTeeColor = 536870912,
//        clNone = 536870911
//    }

//    public enum OperationMode
//    {
//        Normal = 0,
//        OnlyRead = 1,
//        RowMode = 2,
//        SingleSelect = 3,
//        AutoMode = 4
//    }

//    public enum LegendStyle
//    {
//        lsAuto = 0,
//        lsSeries = 1,
//        lsValues = 2,
//        lsLastValues = 3
//    }

//    public enum Axis
//    {
//        taTop = 0,
//        taBottom = 1,
//        taLeft = 2,
//        taRight = 3
//    }

//    public enum VerticalAxis
//    {
//        vxLeft = 0,
//        vxRight = 1,
//        vxLeftAndRight = 2
//    }

//    public enum MultiBar
//    {
//        mbNone = 0,
//        mbSide = 1,
//        mbStacked = 2,
//        mbStacked100 = 3,
//        mbSideAll = 4,
//        mbSelfStack = 5
//    }

//    public enum BarStyle
//    {
//        bsRectangle = 0,
//        bsPyramid = 1,
//        bsInvertPyramid = 2,
//        bsCilinder = 3,
//        bsEllipse = 4,
//        bsArrow = 5,
//        bsRectGradient = 6,
//        bsCone = 7,
//        bsBevel = 8
//    }

//    public enum Alignment
//    {
//        taLeftJustify = 0,
//        taRightJustify = 1,
//        taCenter = 2
//    }

//}
