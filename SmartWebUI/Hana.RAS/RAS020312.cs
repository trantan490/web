using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;

namespace Hana.RAS
{
    public partial class RAS020312 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {


        /// <summary>
        /// 클  래  스: RAS020312<br/>
        /// 클래스요약: 설비별 순간정지 시계열 차트 <br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2011-01-19<br/>
        /// 상세  설명: 설비별 순간정지 시계열 차트 <br/>
        /// 변경  내용: <br/> 
        
        
        private String[] dayArry = new String[7];

        /// </summary>
        public RAS020312()
        {
            InitializeComponent();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();

            this.cdvRes.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvModel.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvBlock.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Equipment name", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("ALARM_DESC", 0, 1, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 200);
            spdData.RPT_AddBasicColumn("CLEAR_TIME", 0, 2, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("START_TIME", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("END_TIME", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("CNT", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);

        }

        #region 초기화 및 유효성 검사
        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvBlock.Text == "ALL" || cdvBlock.Text == "")
            {
                if (cdvRes.Text == "ALL")
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD071", GlobalVariable.gcLanguage));
                    return false;

                }

                if (cdvRes.SelectCount > 30)
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD072", GlobalVariable.gcLanguage));
                    return false;

                }
            }
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        

        
        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 설비별 순간 정지 시작 시간, 종료 시간 (시간단위로)
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string strFromDate =  Convert.ToDateTime(cdvDate.Text).AddDays(-1).ToString("yyyyMMdd") + "240000";
            string strToDate = cdvDate.Text.Replace("-", "") + "235959";

            strSqlString.Append("SELECT ALM.RES_ID" + "\n");
            strSqlString.Append("     , ROUND((TO_NUMBER(SUBSTR(TRAN_TIME,9,2))*60 + TO_NUMBER(SUBSTR(TRAN_TIME,11,2))) / 60,5) AS START_TIME" + "\n");
            strSqlString.Append("     , ROUND((TO_NUMBER(SUBSTR(RESV_FIELD_1,9,2))*60 + TO_NUMBER(SUBSTR(RESV_FIELD_1,11,2))) / 60,5) AS END_TIME" + "\n");
            strSqlString.Append("     , COUNT(ALM.RES_ID) OVER(PARTITION BY ALM.RES_ID) AS CNT" + "\n");
            //strSqlString.Append("  FROM CRASALMHIS@RPTTOMES ALM ," + "\n");
            strSqlString.Append("  FROM MRASALMHIS@RPTTOMES ALM ," + "\n");
            strSqlString.Append("       MRASRESDEF RES" + "\n");
            strSqlString.Append(" WHERE RES.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("   AND RESV_FIELD_1 <= '" + strToDate + "'" + "\n");
            strSqlString.Append("   AND HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("   AND RESV_FIELD_1 <> ' '" + "\n");
            strSqlString.Append("   AND ALM.FACTORY = RES.FACTORY" + "\n");
            strSqlString.Append("   AND ALM.RES_ID = RES.RES_ID" + "\n");
            //strSqlString.Append("   AND ALM.CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("   AND ALM.PERIOD > 0  " + "\n");
            strSqlString.Append("   AND ALM.UP_DOWN_FLAG = 'U'  " + "\n");
            strSqlString.Append("   AND ALM.ALARM_USE = 'Y'  " + "\n");

            if (cdvBlock.Text != "ALL" && cdvBlock.Text != "")
                strSqlString.AppendFormat("   AND RES.SUB_AREA_ID {0} " + "\n", cdvBlock.SelectedValueToQueryString);
            
            if (cdvRes.Text != "ALL" && cdvRes.Text != "")
                    strSqlString.AppendFormat("   AND ALM.RES_ID {0} " + "\n", cdvRes.SelectedValueToQueryString);    
            
            strSqlString.Append("  ORDER BY CNT DESC, RES_ID" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 설비별 순간 정지 누적 카운트
        private string MakeCount()
        {
            StringBuilder strSqlString = new StringBuilder();

            string strFromDate = Convert.ToDateTime(cdvDate.Text).AddDays(-1).ToString("yyyyMMdd") + "240000";
            string strToDate = cdvDate.Text.Replace("-", "") + "235959";

            strSqlString.Append("SELECT RES_ID, SUM(CNT) OVER(ORDER BY CNT DESC, RES_ID ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) AS CNT" + "\n");
            strSqlString.Append("  FROM(" + "\n");
            strSqlString.Append("       SELECT ALM.RES_ID, COUNT(ALM.RES_ID) AS CNT" + "\n");
            //strSqlString.Append("         FROM CRASALMHIS@RPTTOMES ALM ," + "\n");
            strSqlString.Append("         FROM MRASALMHIS@RPTTOMES ALM ," + "\n");
            strSqlString.Append("              MRASRESDEF RES" + "\n");
            strSqlString.Append("        WHERE ALM.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("          AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("          AND RESV_FIELD_1 <= '" + strToDate + "'" + "\n");
            strSqlString.Append("          AND HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("          AND RESV_FIELD_1 <> ' '" + "\n");
            strSqlString.Append("          AND ALM.FACTORY = RES.FACTORY" + "\n");
            strSqlString.Append("          AND ALM.RES_ID = RES.RES_ID" + "\n");
            //strSqlString.Append("          AND ALM.CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("          AND ALM.PERIOD > 0  " + "\n");
            strSqlString.Append("          AND ALM.UP_DOWN_FLAG = 'U'  " + "\n");
            strSqlString.Append("          AND ALM.ALARM_USE = 'Y'  " + "\n");

            if (cdvBlock.Text != "ALL" && cdvBlock.Text != "")
                strSqlString.AppendFormat("   AND RES.SUB_AREA_ID {0} " + "\n", cdvBlock.SelectedValueToQueryString);
            if (cdvRes.Text != "ALL" && cdvRes.Text != "")
                strSqlString.AppendFormat("   AND ALM.RES_ID {0} " + "\n", cdvRes.SelectedValueToQueryString);    
            strSqlString.Append("        GROUP BY ALM.RES_ID" + "\n");
            strSqlString.Append("       )" + "\n");

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 설비별 순간 정지 시작 시간, 종료 시간 (Spread 용)
        private string MakeSqlStringForSpread()
        {
            StringBuilder strSqlString = new StringBuilder();

            string strFromDate = Convert.ToDateTime(cdvDate.Text).AddDays(-1).ToString("yyyyMMdd") + "240000";
            string strToDate = cdvDate.Text.Replace("-", "") + "235959";


            strSqlString.Append("SELECT ALM.RES_ID" + "\n");
            strSqlString.Append("     , ALARM_DESC,CLEAR_TIME" + "\n");
            strSqlString.Append("     , SUBSTR(TRAN_TIME,0,4) || '/' || SUBSTR(TRAN_TIME,5,2) || '/'|| SUBSTR(TRAN_TIME,7,2) || ' ' ||" + "\n");
            strSqlString.Append("       SUBSTR(TRAN_TIME,9,2) || '/' || SUBSTR(TRAN_TIME,11,2) || '/' || SUBSTR(TRAN_TIME,13,2) " + "\n");
            strSqlString.Append("       AS START_TIME" + "\n");
            strSqlString.Append("     , SUBSTR(RESV_FIELD_1,0,4) || '/' || SUBSTR(RESV_FIELD_1,5,2) || '/'|| SUBSTR(RESV_FIELD_1,7,2) || ' ' ||" + "\n");
            strSqlString.Append("       SUBSTR(RESV_FIELD_1,9,2) || '/' || SUBSTR(RESV_FIELD_1,11,2) || '/' || SUBSTR(RESV_FIELD_1,13,2) " + "\n");
            strSqlString.Append("       AS END_TIME" + "\n");
            strSqlString.Append("     , COUNT(ALM.RES_ID) OVER(PARTITION BY ALM.RES_ID) AS CNT" + "\n");
            //strSqlString.Append("  FROM CRASALMHIS@RPTTOMES ALM ," + "\n");
            strSqlString.Append("  FROM MRASALMHIS@RPTTOMES ALM ," + "\n");
            strSqlString.Append("       MRASRESDEF RES " + "\n");
            strSqlString.Append(" WHERE ALM.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("   AND RESV_FIELD_1 <= '" + strToDate + "'" + "\n");
            strSqlString.Append("   AND HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("   AND RESV_FIELD_1 <> ' '" + "\n");
            strSqlString.Append("   AND ALM.FACTORY = RES.FACTORY" + "\n");
            strSqlString.Append("   AND ALM.RES_ID = RES.RES_ID" + "\n");
            //strSqlString.Append("   AND ALM.CLEAR_TIME > 0  " + "\n");
            strSqlString.Append("   AND ALM.PERIOD > 0  " + "\n");
            strSqlString.Append("   AND ALM.UP_DOWN_FLAG = 'U'  " + "\n");
            strSqlString.Append("   AND ALM.ALARM_USE = 'Y'  " + "\n");


            if (cdvBlock.Text != "ALL" && cdvBlock.Text != "")
                strSqlString.AppendFormat("   AND RES.SUB_AREA_ID {0} " + "\n", cdvBlock.SelectedValueToQueryString);
            if (cdvRes.Text != "ALL" && cdvRes.Text != "")
                strSqlString.AppendFormat("   AND ALM.RES_ID {0} " + "\n", cdvRes.SelectedValueToQueryString);
            strSqlString.Append("  ORDER BY CNT DESC, RES_ID" + "\n");
         
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion


       


        #region EVENT 처리
        /// <summary>
        /// 6. View 버튼 Action
        /// </summary>        ///         
        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringForSpread());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 1, null, null, btnSort);
                    //Total부분 셀머지
                    spdData.RPT_FillDataSelectiveCells("TOTAL", 0, 1, 0, 1, true, Align.Center, VerticalAlign.Center);
                dt.Dispose();
                fnMakeChart();
            }
            catch (Exception ex)
            {
                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(ex.Message);
            }
            finally
            {
                LoadingPopUp.LoadingPopUpHidden();
            }
        }


        // 순간 정지 시계열 차트(Gantt 차트 이용)
        private void fnMakeChart()
        {
            /****************************************************
             * Comment : 순간 정지 시계열 차트(Gantt 차트 이용)
             * 
             * Created By : min-woo kim(2011-01-20)
             * 
             * Modified By : min-woo kim(2011-01-20)
             ****************************************************/

            try
            {
                // 설비별 순간 정지 데이터
                DataTable dtChart = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                // 설비별 순간 정지 누적 카운트
                DataTable dtCountByRes = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeCount());
                
                
                int cnt = dtChart.Rows.Count;
                int k = 0;

                cf01.Gallery = SoftwareFX.ChartFX.Gallery.Gantt;
                // 시리즈를 같은 레벨로 표시
                cf01.Cluster = true;
                cf01.OpenData(SoftwareFX.ChartFX.COD.Values, 1, 1);
                cf01.OpenData(SoftwareFX.ChartFX.COD.IniValues, 1, 1);

                // 설비별 순간 정지 누적 카운트의 Row 수 만큼 for문
                // ROW가 바뀐다는 얘기는 즉 설비가 다른 설비로 바뀌었다는 얘기
                for (int i = 0; i < dtCountByRes.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        // 처음 설비는 0 부터 설비별 발생 횟수 카운트만큼 for문
                        for (k=0; k < Convert.ToInt16(dtCountByRes.Rows[i][1].ToString()); k++)
                        {
                            cf01.IniValue[k, i] = Convert.ToDouble(dtChart.Rows[k][1].ToString());
                            cf01.Value[k, i] = Convert.ToDouble(dtChart.Rows[k][2].ToString());
                            cf01.Legend[i] = dtCountByRes.Rows[i][0].ToString();
                            cf01.Series[k].Color = Color.Orange;
                        }
                    }
                    else
                    {
                        // 누적 횟수 이기때문에 윗줄 ROW (전 설비)의 COUNT부터 ~ 현 조회시점의 설비 COUNT 만큼 for 문
                        // 말로 설명하기 힘듬!! 설비별 순간 정지 누적 카운트의 쿼리문을 돌려 보면 왜 이렇게 for문 돌렸는지 이해가능
                        for (k = Convert.ToInt16(dtCountByRes.Rows[i-1][1].ToString()); k < Convert.ToInt16(dtCountByRes.Rows[i][1].ToString()); k++)
                        {
                            cf01.IniValue[k, i] = Convert.ToDouble(dtChart.Rows[k][1].ToString());
                            cf01.Value[k, i] = Convert.ToDouble(dtChart.Rows[k][2].ToString());
                            cf01.Legend[i] = dtCountByRes.Rows[i][0].ToString();
                            // 색을 교대로 주기 위해
                            if (i%2 == 0)
                            {
                                cf01.Series[k].Color = Color.Orange;
                            }
                            else
                            {
                                cf01.Series[k].Color = Color.PaleGreen;
                            }
                        }
                    }
                }
                // 시간으로 표기하기 위하여
                cf01.AxisY.Min = 0;
                cf01.AxisY.Max = 24;
                cf01.AxisY.Step = 1;
                cf01.CloseData(SoftwareFX.ChartFX.COD.Values);
                cf01.CloseData(SoftwareFX.ChartFX.COD.IniValues);
               
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                return;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, cf01, this.lblTitle.Text, null, null);
        }
       
        #endregion
        // 설비 가져오기
        private void cdvRes_ButtonPress(object sender, EventArgs e)
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT RES_ID AS Code, ' ' AS Data FROM MRASRESDEF" + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND RES_GRP_3 = 'WIRE BOND'" + "\n");
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_6 {0} " + "\n", cdvModel.SelectedValueToQueryString);
            if (cdvBlock.Text != "ALL" && cdvBlock.Text != "")
                strSqlString.AppendFormat("   AND SUB_AREA_ID {0} " + "\n", cdvBlock.SelectedValueToQueryString); 
            strSqlString.Append("  ORDER BY RES_ID" + "\n");
            cdvRes.sDynamicQuery = strSqlString.ToString();
        }

        //설비 모델 가져오기
        private void cdvModel_ButtonPress(object sender, EventArgs e)
        {
            cdvRes.Init();
            cdvBlock.Init();
            string strQuery = string.Empty;
            strQuery += "SELECT DISTINCT RES_GRP_6 AS Code, ' ' AS Data" + "\n";
            strQuery += "  FROM MRASRESDEF " + "\n";
            strQuery += " WHERE 1=1 " + "\n";
            strQuery += "   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n";
            strQuery += "   AND RES_GRP_3 = 'WIRE BOND'" + "\n";
            strQuery += "   ORDER BY RES_GRP_6" + "\n";
            cdvModel.sDynamicQuery = strQuery;
        }

        // 설비 Block 가져오기
        private void cdvBlock_ButtonPress(object sender, EventArgs e)
        {

            cdvBlock.Init();
            cdvRes.Init();
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("SELECT DISTINCT SUB_AREA_ID AS Code, ' ' AS Data" + "\n");
            strSqlString.Append("  FROM MRASRESDEF" + "\n");
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("   AND RES_GRP_3 = 'WIRE BOND'" + "\n");
            if (cdvModel.Text != "ALL" && cdvModel.Text != "")
                strSqlString.AppendFormat("   AND RES_GRP_6 {0} " + "\n", cdvModel.SelectedValueToQueryString);
            strSqlString.Append("  ORDER BY SUB_AREA_ID" + "\n");
            cdvBlock.sDynamicQuery = strSqlString.ToString();

        }

    }
}
