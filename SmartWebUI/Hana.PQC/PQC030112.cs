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

namespace Hana.PQC
{
    /// <summary>
    /// 클  래  스: PQC030112<br/>
    /// 클래스요약: 계측기검교정현황<br/>
    /// 작  성  자: 미라콤 양형석<br/>
    /// 최초작성일: 2009-01-21<br/>
    /// 상세  설명: 계측기검교정현황<br/>
    /// 변경  내용: [2009-08-27] 장은희 <br/>
    /// 
    /// 2009-09-17
    /// 임종우
    /// 교정상태별 그래프 추가, 교정여부 그래프 수정
    /// </summary>
    public partial class PQC030112 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        public PQC030112()
        {
            InitializeComponent();
            udcFromToDate.AutoBinding();
            //udcFromToDate.DaySelector.SelectedValue = "MONTH";
            SortInit();
            GridColumnInit();
            udcChartFX1.RPT_1_ChartInit();  //차트 초기화. 

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.udcGubun.sFactory = GlobalVariable.gsAssyDefaultFactory; 
            this.udcFlag.sFactory = GlobalVariable.gsAssyDefaultFactory; 
            this.udcInterval.sFactory = GlobalVariable.gsAssyDefaultFactory; 
            this.udcUser.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcDept.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.udcStatus.sFactory = GlobalVariable.gsAssyDefaultFactory;

            cboGraph.SelectedIndex = 0;
        }

        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            //spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
            //spdData.ActiveSheet.RowHeader.ColumnCount = 0;
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Management Number", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Calibration Interval", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("categorization", 0, 2, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0)
            {
                #region "  Main : 전체보기 "

                spdData.RPT_AddBasicColumn("Management Number", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Verification Number", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Instrument Name", 0, 5, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MODEL", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SERIAL NO.", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAKER", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration cycle", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Classification", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Section", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("registration date", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration status", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of Calibration", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Institution", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Next calibration date", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Department", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                #endregion
            }

            else if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 5)
            {
                #region " 1. 사용부서별 교정현황 / 5. 부서 별 계측기 비율 "
                spdData.RPT_AddBasicColumn("Management Department", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Number", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Verification Number", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Instrument Name", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MODEL", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SERIAL NO.", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAKER", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration cycle", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Classification", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Section", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("registration date", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration status", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of Calibration", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Institution", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Next calibration date", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                #endregion
            }

            else if (cboGraph.SelectedIndex == 3)
            {
                #region " 3.기간별교정현황"
                spdData.RPT_AddBasicColumn("Date of Calibration", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Number", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Verification Number", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Instrument Name", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MODEL", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SERIAL NO.", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAKER", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration cycle", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Classification", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Section", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("registration date", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration status", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Institution", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Next calibration date", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Department", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                #endregion
            }

            else if (cboGraph.SelectedIndex == 6)
            {
                #region "6.계측기종류별현황  "
                spdData.RPT_AddBasicColumn("Instrument Name", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Number", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Verification Number", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MODEL", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SERIAL NO.", 0, 7, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAKER", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration cycle", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Classification", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Section", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("registration date", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration status", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of Calibration", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Institution", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Next calibration date", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Department", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                #endregion
            }

            else if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 7)
            {
                #region "  2. 교정상태별 현황 / 7, 교정상태비율 "

                spdData.RPT_AddBasicColumn("Calibration status", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration status", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Number", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Verification Number", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Instrument Name", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MODEL", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SERIAL NO.", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAKER", 0, 10, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration cycle", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Classification", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Section", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("registration date", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of Calibration", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Institution", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Next calibration date", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Department", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                #endregion
            }

            else if (cboGraph.SelectedIndex == 4)
            {
                #region " 4, 교정여부비율 "

                spdData.RPT_AddBasicColumn("Calibration status", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Number", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Verification Number", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Instrument Name", 0, 6, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MODEL", 0, 7, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("SERIAL NO.", 0, 8, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("MAKER", 0, 9, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration cycle", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Classification", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Section", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("registration date", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of Calibration", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Calibration Institution", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Next calibration date", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Management Department", 0, 17, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                #endregion
            }

            spdData.RPT_AddBasicColumn("Instrument location", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Calibration cost", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Calibration certificate", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Manager", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
            //Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable(btnSort);
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {

            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Management Number", "QCM.MEASURE_ID", "QCM.MEASURE_ID", "MEASURE_ID", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Calibration Interval", "QCM.CALIB_INTERVAL", "QCM.CALIB_INTERVAL", "CALIB_INTERVAL", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("categorization", "QCM.CALIB_CLASS", "QCM.CALIB_CLASS", "CALIB_CLASS", false);
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString(int nIndex)
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            #region " udcDuration에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            strFromDate = udcFromToDate.ExactFromDate;
            strToDate = udcFromToDate.ExactToDate;
            #endregion

            switch (nIndex)
            {
                case 0:
                    {
                        #region " 스프레드 쿼리 "

                        strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");

                        if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0)
                        {
                            #region " Main(전체보기)!"
                            strSqlString.Append("     , QCM.MEASURE_ID , QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                            strSqlString.Append("     , CASE WHEN QCM.CALIB_INTERVAL <> ' ' THEN  QCM.CALIB_INTERVAL||'개월' ELSE ' ' END  CALIB_INTERVAL " + "\n");
                            strSqlString.Append("     , QCM.CALIB_CLASS, QCM.CALIB_SECTION " + "\n");
                            strSqlString.Append("     , DECODE(QCM.RECEIVE_TIME,' ',' ',TO_CHAR(TO_DATE(QCM.RECEIVE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS RECEIVE_TIME " + "\n");
                            strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','진행중','N','유지',' ') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS LAST_CALIB_DATE " + "\n");
                            strSqlString.Append("     , QCM.CALIB_COMPANY " + "\n");
                            strSqlString.Append("     , DECODE(QCM.NEXT_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.NEXT_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS NEXT_CALIB_DATE " + "\n");
                            strSqlString.Append("     , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') AS M_DEPT " + "\n");
                            #endregion
                        }
                        else if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 5)
                        {
                            #region "1.사용부서별교정현황 / 5.부서별계측기비율 "
                            strSqlString.Append("     ,NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') AS M_DEPT " + "\n");
                            strSqlString.Append("     ,QCM.MEASURE_ID , QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                            strSqlString.Append("     , CASE WHEN QCM.CALIB_INTERVAL <> ' ' THEN  QCM.CALIB_INTERVAL||'개월' ELSE ' ' END  CALIB_INTERVAL " + "\n");
                            strSqlString.Append("     , QCM.CALIB_CLASS, QCM.CALIB_SECTION " + "\n");
                            strSqlString.Append("     , DECODE(QCM.RECEIVE_TIME,' ',' ',TO_CHAR(TO_DATE(QCM.RECEIVE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS RECEIVE_TIME " + "\n");
                            strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','진행중','N','유지',' ') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS LAST_CALIB_DATE " + "\n");
                            strSqlString.Append("     , QCM.CALIB_COMPANY " + "\n");
                            strSqlString.Append("     , DECODE(QCM.NEXT_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.NEXT_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS NEXT_CALIB_DATE " + "\n");
                            #endregion
                        }
                        else if (cboGraph.SelectedIndex == 3)
                        {
                            #region "3.기간별교정현황 "
                            strSqlString.Append("     , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS LAST_CALIB_DATE " + "\n");
                            strSqlString.Append("     , QCM.MEASURE_ID , QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                            strSqlString.Append("     , CASE WHEN QCM.CALIB_INTERVAL <> ' ' THEN  QCM.CALIB_INTERVAL||'개월' ELSE ' ' END  CALIB_INTERVAL " + "\n");
                            strSqlString.Append("     , QCM.CALIB_CLASS, QCM.CALIB_SECTION " + "\n");
                            strSqlString.Append("     , DECODE(QCM.RECEIVE_TIME,' ',' ',TO_CHAR(TO_DATE(QCM.RECEIVE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS RECEIVE_TIME " + "\n");
                            strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','진행중','N','유지',' ') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , QCM.CALIB_COMPANY " + "\n");
                            strSqlString.Append("     , DECODE(QCM.NEXT_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.NEXT_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS NEXT_CALIB_DATE " + "\n");
                            strSqlString.Append("     , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') AS M_DEPT " + "\n");
                            #endregion
                        }
                        else if (cboGraph.SelectedIndex == 6)
                        {
                            #region " 6.계측기종류별현황"
                            strSqlString.Append("     , QCM.MEASURE_DESC ,QCM.MEASURE_ID " + "\n");
                            strSqlString.Append("     , QCM.VERIFY_NO,  QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                            strSqlString.Append("     , CASE WHEN QCM.CALIB_INTERVAL <> ' ' THEN  QCM.CALIB_INTERVAL||'개월' ELSE ' ' END  CALIB_INTERVAL " + "\n");
                            strSqlString.Append("     , QCM.CALIB_CLASS, QCM.CALIB_SECTION " + "\n");
                            strSqlString.Append("     , DECODE(QCM.RECEIVE_TIME,' ',' ',TO_CHAR(TO_DATE(QCM.RECEIVE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS RECEIVE_TIME " + "\n");
                            strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','진행중','N','유지',' ') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS LAST_CALIB_DATE " + "\n");
                            strSqlString.Append("     , QCM.CALIB_COMPANY " + "\n");
                            strSqlString.Append("     , DECODE(QCM.NEXT_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.NEXT_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS NEXT_CALIB_DATE " + "\n");
                            strSqlString.Append("     , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') AS M_DEPT " + "\n");
                            #endregion
                        }
                        else if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 7)
                        {
                            #region "  2. 교정상태별 현황 / 7, 교정상태비율 "
                            strSqlString.Append("     , DECODE(QCM.CALIB_STATUS,'CS_APC','부적합','CS_DISUSE','폐기','CS_FAIL','불합격','CS_NORMAL','정상','CS_REPAIR','수리','CS_WAIT','대기중',' ') AS CALIB_STATUS " + "\n");
                            strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','유지','N','정지','진행') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , QCM.MEASURE_ID , QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                            strSqlString.Append("     , CASE WHEN QCM.CALIB_INTERVAL <> ' ' THEN  QCM.CALIB_INTERVAL||'개월' ELSE ' ' END  CALIB_INTERVAL " + "\n");
                            strSqlString.Append("     , QCM.CALIB_CLASS, QCM.CALIB_SECTION " + "\n");
                            strSqlString.Append("     , DECODE(QCM.RECEIVE_TIME,' ',' ',TO_CHAR(TO_DATE(QCM.RECEIVE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS RECEIVE_TIME " + "\n");
                            strSqlString.Append("     , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS LAST_CALIB_DATE " + "\n");
                            strSqlString.Append("     , QCM.CALIB_COMPANY " + "\n");
                            strSqlString.Append("     , DECODE(QCM.NEXT_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.NEXT_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS NEXT_CALIB_DATE " + "\n");
                            strSqlString.Append("     , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') AS M_DEPT " + "\n");

                            #endregion
                        }
                        else if (cboGraph.SelectedIndex == 4)
                        {
                            #region " 4, 교정여부비율 "
                            //strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','진행중','N','유지',' ') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , DECODE(QCM.CALIB_FLAG,'Y','유지','N','정지','P','진행',' ') AS CALIB_FLAG " + "\n");
                            strSqlString.Append("     , QCM.MEASURE_ID , QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                            strSqlString.Append("     , CASE WHEN QCM.CALIB_INTERVAL <> ' ' THEN  QCM.CALIB_INTERVAL||'개월' ELSE ' ' END  CALIB_INTERVAL " + "\n");
                            strSqlString.Append("     , QCM.CALIB_CLASS, QCM.CALIB_SECTION " + "\n");
                            strSqlString.Append("     , DECODE(QCM.RECEIVE_TIME,' ',' ',TO_CHAR(TO_DATE(QCM.RECEIVE_TIME, 'YYYYMMDDHH24MISS'), 'YYYY-MM-DD')) AS RECEIVE_TIME " + "\n");
                            strSqlString.Append("     , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS LAST_CALIB_DATE " + "\n");
                            strSqlString.Append("     , QCM.CALIB_COMPANY " + "\n");
                            strSqlString.Append("     , DECODE(QCM.NEXT_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.NEXT_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) AS NEXT_CALIB_DATE " + "\n");
                            strSqlString.Append("     , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') AS M_DEPT " + "\n");

                            #endregion
                        }
                        strSqlString.Append("     , QCM.LOCATION " + "\n");
                        strSqlString.Append("     , DECODE(QCM.CALIB_COMPANY,' ',' ',QCM.CALIB_COST) AS CALIB_COST " + "\n");
                        strSqlString.Append("     , DECODE(QCM.CALIB_REPORT,' ' ,' ', '보유') AS CALIB_REPORT  " + "\n");
                        strSqlString.Append("     , NVL((SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = QCM.UPDATE_USER_ID AND ROWNUM = 1), ' ')  AS M_USER  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM  " + "\n");
                        strSqlString.Append(" WHERE 1=1 " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정여부, 교정상태  교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcStatus.Text != "ALL" && udcStatus.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_STATUS {0} " + "\n", udcStatus.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion


                        strSqlString.Append(" ORDER BY   " + QueryCond1 + " \n");

                        if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 4)
                        {
                            strSqlString.Append("      , QCM.MEASURE_ID, QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                        }
                        else if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 5)
                        {
                            strSqlString.Append("      , QCM.REQUEST_DEPT,QCM.MEASURE_ID, QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                        }
                        else if (cboGraph.SelectedIndex == 3)
                        {
                            strSqlString.Append("      , DECODE(QCM.LAST_CALIB_DATE,' ',' ',TO_CHAR(TO_DATE(QCM.LAST_CALIB_DATE, 'YYYYMMDD'), 'YYYY-MM-DD')) , " + QueryCond1 + " \n");
                            strSqlString.Append("      , QCM.MEASURE_ID, QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                        }
                        else if (cboGraph.SelectedIndex == 6)
                        {
                            strSqlString.Append("      , QCM.MEASURE_DESC, QCM.MEASURE_ID, QCM.VERIFY_NO,  QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                        }
                        else if (cboGraph.SelectedIndex == 2 || cboGraph.SelectedIndex == 4)
                        {
                            strSqlString.Append("      ,QCM.CALIB_FLAG , QCM.MEASURE_ID, QCM.VERIFY_NO, QCM.MEASURE_DESC, QCM.MODEL, QCM.SERIAL_NO, QCM.MAKER " + "\n");
                        }
                        #endregion
                    }
                    break;
                case 1:
                    {
                        #region " 1. 사용부서 별 교정현황 "
                        strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') \"사용부서\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(CALIB_FLAG, 'Y', 1, 0)) \"교정완료\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(CALIB_FLAG, 'N', 1, 0)) \"교정진행중\"  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정상태,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion

                        strSqlString.Append(" GROUP BY QCM.FACTORY, REQUEST_DEPT  " + "\n");
                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region " 2번 교정상태별 현황 "
                        strSqlString.Append("SELECT DECODE(CALIB_STATUS,'CS_APC','부적합','CS_DISUSE','폐기','CS_FAIL','불합격','CS_NORMAL','정상','CS_REPAIR','수리','CS_WAIT','대기중',' ') AS CALIB_STATUS " + "\n");
                        strSqlString.Append("     , COUNT(*) CNT  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정상태,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        //if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                        //    strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion

                        strSqlString.Append(" GROUP BY CALIB_STATUS " + "\n");
                        #endregion
                    }
                    break;
                case 3:
                    {
                        #region " 3번 기간별 교정현황 "
                        string strDecode = string.Empty;
                        string[] strDateValues = udcFromToDate.getSelectDate();
                        switch (udcFromToDate.DaySelector.SelectedValue.ToString())
                        {
                            case "DAY":
                                for (int i = 0; i < udcFromToDate.SubtractBetweenFromToDate + 1; i++)
                                {
                                    strDecode += "     , SUM(DECODE(GET_WORK_DATE(LAST_CALIB_DATE, 'D'), '" + strDateValues[i] + "', 1, 0)) \"" + strDateValues[i] + "\" " + "\n";
                                }
                                break;
                            case "WEEK":
                                for (int i = 0; i < udcFromToDate.SubtractBetweenFromToDate + 1; i++)
                                {
                                    strDecode += "     , SUM(DECODE(GET_WORK_DATE(LAST_CALIB_DATE, 'W'), '" + strDateValues[i] + "', 1, 0)) \"" + strDateValues[i] + "\" " + "\n";
                                }
                                break;
                            case "MONTH":
                                for (int i = 0; i < udcFromToDate.SubtractBetweenFromToDate + 1; i++)
                                {
                                    strDecode += "     , SUM(DECODE(GET_WORK_DATE(LAST_CALIB_DATE, 'M'), '" + strDateValues[i] + "', 1, 0)) \"" + strDateValues[i] + "\" " + "\n";
                                }
                                break;
                        }

                        strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') \"사용부서\" " + "\n");
                        strSqlString.Append(strDecode);
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정상태,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion

                        strSqlString.Append(" GROUP BY QCM.FACTORY, REQUEST_DEPT  " + "\n");
                        #endregion
                    }
                    break;
                case 4:
                    {
                        #region " 4번 교정여부 비율 "
                        //strSqlString.Append("SELECT DECODE(CALIB_FLAG, 'Y', '교정완료', '외부교정중')  AS CALIB_FLAG " + "\n");
                        strSqlString.Append("SELECT DECODE(CALIB_FLAG, 'Y', '유지', 'N', '정지' , 'P' , '진행' , ' ')  AS CALIB_FLAG " + "\n");
                        strSqlString.Append("     , COUNT(*) CNT  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정여부,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        //if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                        //    strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion

                        strSqlString.Append(" GROUP BY CALIB_FLAG " + "\n");
                        #endregion
                    }
                    break;
                case 5:
                    {
                        #region " 5번 부서별 계측기비율 "
                        strSqlString.Append("SELECT NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = QCM.FACTORY AND TABLE_NAME = 'H_DEPARTMENT' AND KEY_1 = QCM.REQUEST_DEPT AND ROWNUM = 1), '-') \"사용부서\" " + "\n");
                        strSqlString.Append("     , COUNT(*) CNT  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정상태,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion
                        strSqlString.Append(" GROUP BY QCM.FACTORY, REQUEST_DEPT  " + "\n");
                        #endregion
                    }
                    break;
                case 6:
                    {
                        #region " 6번 계측기 종류별 현황 "
                        strSqlString.Append(" SELECT QCM.MEASURE_DESC  , COUNT(*) CNT  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정상태,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion

                        strSqlString.Append(" GROUP BY QCM.MEASURE_DESC " + "\n");
                        #endregion
                    }
                    break;
                case 7:
                    {
                        #region " 7번 교정상태 비율 "
                        strSqlString.Append("SELECT DECODE(CALIB_STATUS,'CS_APC','부적합','CS_DISUSE','폐기','CS_FAIL','불합격','CS_NORMAL','정상','CS_REPAIR','수리','CS_WAIT','대기중',' ') AS CALIB_STATUS " + "\n");                        
                        strSqlString.Append("     , COUNT(*) CNT  " + "\n");
                        strSqlString.Append("  FROM CQCMMEASTS@RPTTOMES QCM " + "\n");
                        strSqlString.Append(" WHERE 1=1  " + "\n");
                        strSqlString.AppendFormat("      AND QCM.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("      AND QCM.RECEIVE_FLAG = 'R' " + "\n");
                        strSqlString.Append("      AND QCM.DELETE_FLAG  <> 'Y' " + "\n");
                        strSqlString.AppendFormat("      AND QCM.RECEIVE_TIME BETWEEN '{0}' AND '{1}' " + "\n", strFromDate.Substring(0, 8), strToDate.Substring(0, 8));

                        #region " 조회조건 :   교정주기, 교정여부,   교정방법, 계측기명, 사용부서, 담당자 "

                        if (udcInterval.Text != "ALL" && udcInterval.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_INTERVAL {0} " + "\n", udcInterval.SelectedValueToQueryString);

                        //if (udcFlag.Text != "ALL" && udcFlag.Text != "")
                        //    strSqlString.AppendFormat("      AND QCM.CALIB_FLAG {0} " + "\n", udcFlag.SelectedValueToQueryString);

                        if (udcGubun.Text != "ALL" && udcGubun.Text != "")
                            strSqlString.AppendFormat("      AND QCM.CALIB_CLASS {0} " + "\n", udcGubun.SelectedValueToQueryString);

                        if (udcDept.Text != "ALL" && udcDept.Text != "")
                            strSqlString.AppendFormat("      AND QCM.REQUEST_DEPT {0} " + "\n", udcDept.SelectedValueToQueryString);

                        if (txtMeasure.Text.Trim() != "%" && txtMeasure.Text.Trim() != "")
                            strSqlString.AppendFormat("      AND QCM.MEASURE_DESC LIKE '{0}'" + "\n", txtMeasure.Text);

                        if (udcUser.Text != "ALL" && udcUser.Text != "")
                            strSqlString.AppendFormat("      AND QCM.UPDATE_USER_ID {0} " + "\n", udcUser.SelectedValueToQueryString);

                        #endregion

                        strSqlString.Append(" GROUP BY CALIB_STATUS " + "\n");
                        #endregion
                    }
                    break;
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        private void ShowChart()
        {
            #region " Chart 기본설정"            

            udcChartFX1.RPT_1_ChartInit();
            udcChartFX1.RPT_2_ClearData();

            // 그래프 설정 //                      
            udcChartFX1.AxisY.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisY2.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.AxisX.Font = new System.Drawing.Font("굴림체", 8, System.Drawing.FontStyle.Regular);
            udcChartFX1.PointLabels = true;
            // ToolBar 보여주기
            udcChartFX1.ToolBar = true;

            //3D 
            udcChartFX1.Chart3D = true;

            // contion attribute 를 이용한 0 point label hidden            
            SoftwareFX.ChartFX.ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            contion.Condition.From = 0;
            contion.Condition.To = 0;
            contion.PointLabels = false;


            udcChartFX1.MultipleColors = false;
            #endregion

            switch (cboGraph.SelectedIndex)
            {
                case 0:
                    {
                        #region " 전체보기 "
                        #endregion
                    }
                    break;
                case 1:
                    {
                        #region " 1번 부서별 교정현황 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.AxisX.Title.Text = "Use department";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";

                        #endregion
                    }
                    break;
                case 2:
                    {
                        #region " 2번 교정상태별 현황"

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.MultipleColors = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.AxisX.Title.Text = "Calibration status";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";

                        #endregion
                    }
                    break;
                case 3:
                    {
                        #region " 3번 기간별 교정현황 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        dt = GetRotatedDataTable(ref dt);

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBox = true;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;
                        //udcChartFX1.PointLabels = true;

                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.AxisX.Title.Text = "";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";

                        #endregion
                    }
                    break;
                case 4:
                    {
                        #region " 4번 교정여부 비율"
                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));


                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max;


                        #endregion
                    }
                    break;
                case 5:
                    {
                        #region " 5번 부서별 계측기비율 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(5));

                        if (dt == null || dt.Rows.Count < 1)
                            return;
                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max;

                        #endregion
                    }
                    break;
                case 6:
                    {
                        #region " 6번 계측기 종류별 현황 "

                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(6));

                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        udcChartFX1.LegendBox = false;

                        udcChartFX1.MultipleColors = true;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.2;
                        udcChartFX1.AxisX.Title.Text = "Instrument Name";
                        udcChartFX1.AxisY.Title.Text = "number of occurrences";

                        #endregion
                    }
                    break;
                case 7:
                    {
                        #region " 7번 교정상태 비율"
                        DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(7));


                        if (dt == null || dt.Rows.Count < 1)
                            return;

                        udcChartFX1.DataSource = dt;
                        udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Pie;
                        udcChartFX1.SerLegBox = false;
                        udcChartFX1.LegendBox = false;
                        udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max;


                        #endregion
                    }
                    break;
            }
        }

        /// <summary>
        /// DataTable의 행과 열을 서로 바꿔주는 메서드입니다.
        /// </summary>
        /// <param name="dt">변환되어야 할 DataTable을 입력받습니다.</param>
        /// <returns>변환된 DataTable을 반환합니다.</returns>
        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            // Column Position of dt => Legend Column of dtNew
            int nColToRow = 0;

            DataTable dtNew = new DataTable();
            Object[] dr = null;

            // Get Series Type
            Type type = dt.Columns[1].DataType;

            // Adding Columns
            dtNew.Columns.Add("GUBUN", typeof(String));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(dt.Rows[i][0].ToString(), type);
            }

            // Filling Data
            for (int j = nColToRow + 1; j < dt.Columns.Count; j++)
            {
                dr = dtNew.NewRow().ItemArray;
                dr[0] = dt.Columns[j].Caption;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr[i + 1] = dt.Rows[i][j];
                }
                dtNew.Rows.Add(dr);
            }
            return dtNew;
        }
        #endregion

        #region " EVENT HANDLER "

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;

            if (CheckField() == false) return;

            // View 버튼 클릭하자 마자 로딩팝업 창이 보이도록 추가
            LoadingPopUp.LoadIngPopUpShow(this);

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {

                // 위에 보이도록 추가한 부분으로 인해 주석처리
                //      LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //by John
                //1.Griid 합계 표시
                spdData.DataSource = dt;
                spdData.RPT_ColumnConfigFromTable(btnSort);

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //3. Total부분 셀머지
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 9, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                // 1번 그래프 그리도록 이벤트 발생
                //  cboGraph.SelectedIndex = 0;
                ShowChart();
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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ");
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
        }


        #endregion

    }
}