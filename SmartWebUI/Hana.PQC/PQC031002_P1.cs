using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.PQC
{

    /// <summary>
    /// 클  래  스: PQC031002_P1<br/>
    /// 클래스요약: 오븐 온도이력조회 팝업<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-02-03<br/>
    /// 상세  설명: 오븐 온도이력조회 팝업<br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PQC031002_P1 : Form
    {

        public PQC031002_P1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 팝업에서 보여줄 Title과 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="dt">해당 공정그룹의 정체 Lot 현황</param>
        public PQC031002_P1(String title, DataTable dt)
        {
            InitializeComponent();

            this.Text ="(" + title + ") 온도 Chart";

            //int columnCount = 0;
            double max = 0;
            double max_temp = 0;

            udcMSChart1.RPT_1_ChartInit();
            udcMSChart1.RPT_2_ClearData();

            // contion attribute 를 이용한 0 point label hidden            
            //SoftwareFX.ChartFX.ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            //contion.Condition.From = 0;
            //contion.Condition.To = 0;
            //contion.PointLabels = false;


            // 차트 설정
            udcMSChart1.RPT_3_OpenData(2, dt.Rows.Count);            
            int[] tat_columns = new Int32[dt.Rows.Count];
            int[] columnsHeader = new Int32[dt.Rows.Count];
            int[] dt_rows = new Int32[dt.Rows.Count];


            for (int i = 0; i < dt_rows.Length; i++)
            {
                tat_columns[i] = 0 + i;
                dt_rows[i] = 0 + i;
                columnsHeader[i] = 0 + i;
            }

            // 데이터 테이블의 1~2 컬럼의 데이터를 표시한다.
            for (int j = 1; j <= 2; j++)
            {
                max = udcMSChart1.RPT_4_AddData(dt, dt_rows, new int[] { j }, SeriseType.Column);

                if (max > max_temp)
                {
                    max_temp = max;
                }
            }

            max = max_temp;

            //max1 = udcChartFX1.RPT_4_AddData(dt, tat_columns, new int[] { columnCount + 1 }, SeriseType.Column);            
            //max1 = udcChartFX1.RPT_4_AddData(dt, dt_columns, new int[] { columnCount + 2 }, SeriseType.Column);                    

            udcMSChart1.RPT_5_CloseData();            

            ////각 Serise별로 다른 타입을 사용할 경우
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 0, 1, "", AsixType.Y, DataTypes.Initeger, max * 1.2);
            udcMSChart1.RPT_6_SetGallery(SeriesChartType.Line, 1, 1, "", AsixType.Y2, DataTypes.Initeger, max * 1.2);
            udcMSChart1.Series[0].Color = System.Drawing.Color.Red;
            udcMSChart1.Series[1].Color = System.Drawing.Color.Blue;
            udcMSChart1.Series[0].BorderWidth = 2;
            udcMSChart1.Series[1].BorderWidth = 2;


            //udcChartFX1.RPT_6_SetGallery(ChartType.ThinLine, 0, 1, "", AsixType.Y, DataTypes.Initeger, max1 * 1.2);
            //udcChartFX1.RPT_6_SetGallery(ChartType.ThinLine, 1, 1, "", AsixType.Y2, DataTypes.Initeger, max1 * 1.2);       // 2010-08-10-안시홍 : AsixType.Y2로 수정
            //udcChartFX1.Series[0].Color = System.Drawing.Color.Red;
            //udcChartFX1.Series[1].Color = System.Drawing.Color.Blue;
            //udcChartFX1.Series[0].LineWidth = 2;                                                                        // 2010-08-10-안시홍 : 굵은 그래프 선(RED)으로 변경
            //udcChartFX1.Series[1].LineWidth = 2;                                                                        // 2010-08-10-안시홍 : 굵은 그래프 선(BLUE)으로 변경
                        
            udcMSChart1.RPT_7_SetXAsixTitleUsingDataTable(dt, 0);
            //udcMSChart1.RPT_8_SetSeriseLegend(LegBox, System.Windows.Forms.DataVisualization.Charting.Docking.Bottom);

            //udcChartFX1.RPT_7_SetXAsixTitleUsingSpreadHeader(dt);
            //udcChartFX1.Highlight.PointAttributes.PointLabelColor = System.Drawing.Color.Transparent;
            //udcChartFX1.AxisY2.Gridlines = true;                                                                        // 2010-08-10-안시홍 : AxisY에서 AxisY2으로 변경
                        
            //udcChartFX1.AxisY.DataFormat.Decimals = 0;
                        
            udcMSChart1.ChartAreas[0].AxisX.Title = "Minutes";
            udcMSChart1.ChartAreas[0].AxisY.Title = "Temperature (℃)";
            udcMSChart1.ChartAreas[0].AxisX.Minimum = 0;
            udcMSChart1.ChartAreas[0].AxisY.Minimum = 20;
            udcMSChart1.ChartAreas[0].AxisY.Maximum = 210;
            udcMSChart1.ChartAreas[0].AxisY2.Minimum = 20;
            udcMSChart1.ChartAreas[0].AxisY2.Maximum = 210;
            
            // 2010-08-10-안시홍 : 가로축, 세로축 수정
            //udcChartFX1.AxisX.Title.Text = "Minutes";
            //udcChartFX1.AxisY.Title.Text = "Temperature (℃)";
            //udcChartFX1.AxisX.Font = new System.Drawing.Font("Tahoma", 9.25F);
            //udcChartFX1.AxisY.Font = new System.Drawing.Font("Tahoma", 9.25F);
            //udcChartFX1.AxisY2.Font = new System.Drawing.Font("Tahoma", 0.05F);
            //udcChartFX1.AxisX.Step = 10;
            //udcChartFX1.AxisY.Step = 10;
            //udcChartFX1.AxisY2.Step = 5;
            //udcChartFX1.AxisX.Min = 0;
            //udcChartFX1.AxisY.Min = 20;
            //udcChartFX1.AxisY.Max = 210;
            //udcChartFX1.AxisY2.Min = 20;
            //udcChartFX1.AxisY2.Max = 210;
                        

            //// 2010-08-30-안시홍 : 30분 간격의 세로 구분선 추가
            //udcChartFX1.AxisX.MinorGridlines = true;
            //udcChartFX1.AxisX.MinorStep = 30;

            udcMSChart1.RPT_8_SetSeriseLegend(new string[] { "기준온도", "현재온도" }, System.Windows.Forms.DataVisualization.Charting.Docking.Top);
            
            // 2010-08-10-안시홍 : 범례 표시
            //udcChartFX1.SerLeg[0] = "기준온도";
            //udcChartFX1.SerLeg[1] = "현재온도";
            //udcChartFX1.SerLegBox = true;
            //udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
        }

    }
}