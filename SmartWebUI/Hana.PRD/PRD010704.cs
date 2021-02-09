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

namespace Hana.PRD
{
    public partial class PRD010704 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010704<br/>
        /// 클래스요약: 외주(도금) 일별 실적<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-04-28<br/>
        /// 상세  설명: 외주 도금 일별 실적 조회<br/>
        /// 변경  내용: <br/>        
        /// 2012-08-21-임종우 : 도금 업체 정보 없어도 오류 발생 하지 않도록 수정
        /// 2013-10-14-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2017-10-31-임종우 : 도금 공정 및 HOLD 코드 추가
        /// </summary>
        public PRD010704()
        {
            InitializeComponent();            
            SortInit();

            cdvFromToDate.AutoBinding();

            GridColumnInit(); //헤더 한줄짜리 
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.SetFactory(cdvFactory.txtValue);
            //cdvType.sFactory = cdvFactory.txtValue;
            cdvPSite.sFactory = cdvFactory.txtValue;
            cdvFactory.Enabled = false;
            cdvPSite.Text = "ALL";
        }

        #region SortInit

        /// <summary>
        /// SortInit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Outsourcing company", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_VENDOR_PLATING' AND KEY_1 = HLD.SHP_FAC AND ROWNUM=1), '-') AS SHP_FAC", "HLD.SHP_FAC", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", "MAT.MAT_GRP_1 ", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3", true);                     
        }

        #endregion

        #region 한줄헤더생성

        /// <summary>
        /// 한줄헤더생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridColumnInit()
        {            
            spdData.RPT_ColumnInit();

            spdData.RPT_AddBasicColumn("Outsourcing company", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);

            //if (cboType.Text.Equals("TAT"))
            if (cboType.SelectedIndex == 3)
            {
                spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 70);
            }
            else
            {
                if (ckbKpcs.Checked == false) // Kpcs 구분
                {
                    spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                else
                {
                    spdData.RPT_AddDynamicColumn(cdvFromToDate, 0, 4, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 70);
                }
            }
            
            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        #endregion

        #region 조회

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (!CheckField()) return;

            DataTable dt = null;
                        
            GridColumnInit();

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 4, null, null, btnSort);

                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                //6. TAT 평균값 구하기(GrandTotal부분 & SubTotal)
                //if (cboType.Text.Equals("TAT"))
                if (cboType.SelectedIndex == 3)
                {
                    for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                    {
                        spdData.RPT_SetAvgSubTotalAndGrandTotal(1, 4 + i, 0, false);
                    }
                }

                dt.Dispose();

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

        #region CheckField

        /// <summary>
        /// CheckField
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private Boolean CheckField()
        {
            if (cdvFactory.Text.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD001", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion
          
        #region MakeSqlString
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string strDecode = null;
            string strDecode2 = null;
            string[] selectDate = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;

            // TAT 선택시 DECODE 용 쿼리..
            //if (cboType.Text.Equals("TAT"))
            if (cboType.SelectedIndex == 3)
            {
                selectDate = cdvFromToDate.getSelectDate();

                for (int i = 0; i < cdvFromToDate.SubtractBetweenFromToDate + 1; i++)
                {
                    strDecode2 += "     , ROUND(DECODE(SUM(DECODE(WORK_DATE, '" + selectDate[i] + "', 1, 0)), 0, 0, SUM(DECODE(WORK_DATE, '" + selectDate[i] + "', TAT, 0)) / SUM(DECODE(WORK_DATE, '" + selectDate[i] + "', 1, 0))), 2) AS A" + i + "\n";
                }
            }
            else // 그 외에 선택시 DECODE 용 쿼리..
            {
                if (ckbKpcs.Checked == false)
                {
                    strDecode += cdvFromToDate.getDecodeQuery("SUM(DECODE(WORK_DATE", "HLD.QTY_1, 0))", "A").Replace(", SUM(DECODE(", "     , SUM(DECODE(");
                }
                else
                {
                    strDecode += cdvFromToDate.getDecodeQuery("ROUND(SUM(DECODE(WORK_DATE", "HLD.QTY_1, 0))/1000,1)", "A").Replace(", ROUND(SUM(DECODE(", "     , ROUND(SUM(DECODE(");
                }
            }

            strSqlString.AppendFormat("SELECT {0}" + "\n", QueryCond1);

            //if (cboType.Text.Equals("TAT"))
            if (cboType.SelectedIndex == 3)
            {
                strSqlString.Append(strDecode2);
            }
            else
            {
                strSqlString.Append(strDecode);
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.Append("        SELECT HLD.FACTORY " + "\n");
            strSqlString.Append("             , HLD.MAT_ID " + "\n");
            strSqlString.Append("             , HLD.LOT_ID   " + "\n");

            // TYPE 선택에 따른 QTY_1 값 계산
            //if(cboType.Text.Equals("업체재공"))
            if (cboType.SelectedIndex == 2)
            {
                strSqlString.Append("             , CASE WHEN (HLD.RELEASE_TRAN_TIME > GET_WORK_DATE(HLD.HOLD_TRAN_TIME, 'D')||'220000' OR TRIM(HLD.RELEASE_TRAN_TIME) IS NULL) " + "\n");
                strSqlString.Append("                    THEN HLD.QTY_1 " + "\n");
                strSqlString.Append("                    ELSE 0 " + "\n");
                strSqlString.Append("                END AS QTY_1 " + "\n");
            }
            //else if (cboType.Text.Equals("TAT"))
            else if (cboType.SelectedIndex == 3)
            {
                strSqlString.Append("             , TRUNC(DECODE(TRIM(HLD.RELEASE_TRAN_TIME), NULL, SYSDATE,TO_DATE(HLD.RELEASE_TRAN_TIME,'YYYYMMDDHH24MISS')) - TO_DATE(HLD.HOLD_TRAN_TIME,'YYYYMMDDHH24MISS'),2) AS TAT " + "\n");
            }
            else
            {
                strSqlString.Append("             , HLD.QTY_1 " + "\n");
            }

            // TYPE선택에 따른 WORK_DATE
            //if (cboType.Text.Equals("반입수량"))
            if (cboType.SelectedIndex == 1)
            {
                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    strSqlString.Append("             , GET_WORK_DATE(HLD.RELEASE_TRAN_TIME, 'D') AS WORK_DATE " + "\n");
                }
                else if(cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("             , GET_WORK_DATE(HLD.RELEASE_TRAN_TIME, 'W') AS WORK_DATE " + "\n");
                }
                else
                {
                    strSqlString.Append("             , GET_WORK_DATE(HLD.RELEASE_TRAN_TIME, 'M') AS WORK_DATE " + "\n");
                }
            }
            else
            {
                if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
                {
                    strSqlString.Append("             , GET_WORK_DATE(HLD.HOLD_TRAN_TIME, 'D') AS WORK_DATE " + "\n"); 
                }
                else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
                {
                    strSqlString.Append("             , GET_WORK_DATE(HLD.HOLD_TRAN_TIME, 'W') AS WORK_DATE " + "\n"); 
                }
                else
                {
                    strSqlString.Append("             , GET_WORK_DATE(HLD.HOLD_TRAN_TIME, 'M') AS WORK_DATE " + "\n"); 
                }                                
            }
                        
            strSqlString.Append("             , HIS.LOT_CMF_5 AS LOT_TYPE " + "\n");
            strSqlString.Append("             , HIS.TRAN_CMF_2 AS SHP_FAC " + "\n");            
            strSqlString.Append("          FROM RWIPLOTHLD HLD " + "\n");
            strSqlString.Append("             , RWIPLOTHIS HIS " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND HLD.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("           AND HLD.FACTORY = HIS.FACTORY(+) " + "\n");
            strSqlString.Append("           AND HLD.LOT_ID = HIS.LOT_ID(+) " + "\n");
            strSqlString.Append("           AND HLD.HIST_SEQ = HIS.HIST_SEQ(+) " + "\n");
            strSqlString.Append("           AND HLD.OPER IN ('A1050', 'A1440', 'A1450', 'A1460')    " + "\n");

            //if (cboType.Text.Equals("반입수량"))
            if (cboType.SelectedIndex == 1)
            {                
                strSqlString.Append("           AND HLD.RELEASE_TRAN_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "' " + "\n");
            }
            else
            {
                strSqlString.Append("           AND HLD.HOLD_TRAN_TIME BETWEEN '" + cdvFromToDate.ExactFromDate + "' AND '" + cdvFromToDate.ExactToDate + "' " + "\n");
            }

            strSqlString.Append("           AND HIS.HOLD_CODE IN ('S0', 'H55', 'H74') " + "\n");
            strSqlString.Append("           AND HLD.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND HIS.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("           AND HIS.TRAN_CODE = 'HOLD' " + "\n");

            /*
            if (cdvType.txtValue != "" && cdvType.txtValue != "ALL")
            {
                strSqlString.Append("           AND HIS.LOT_CMF_5 " + cdvType.SelectedValueToQueryString + "\n");
            }
            */

            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("           AND HIS.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("       ) HLD " + "\n");
            strSqlString.Append("       , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1 = 1            " + "\n");
            strSqlString.Append("   AND HLD.FACTORY = MAT.FACTORY " + "\n");
            strSqlString.Append("   AND HLD.MAT_ID = MAT.MAT_ID            " + "\n");
            strSqlString.Append("   AND MAT.DELETE_FLAG <> 'Y'" + "\n");
            strSqlString.Append("   AND MAT.MAT_TYPE = 'FG'" + "\n");
            //strSqlString.Append("   AND MAT.MAT_CMF_1 IN ('Sn-Bi','Sn 100')" + "\n");
            
            // 도금 업체 선택에 따른 SQL문 생성
            if (cdvPSite.Text != "ALL" && cdvPSite.Text != "")
                strSqlString.AppendFormat("   AND SHP_FAC {0} " + "\n", cdvPSite.SelectedValueToQueryString);

            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            //if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            //if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            //if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            //if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            //if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
            //    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);                        

            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond2);
            strSqlString.AppendFormat(" ORDER BY {0}" + "\n", QueryCond2);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #endregion

        #region ToExcel

        /// <summary>
        /// ToExcel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }
        #endregion

        #region FactorySelectChanged

        /// <summary>
        /// FactorySelectChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);
            //cdvType.sFactory = cdvFactory.txtValue;
            cdvPSite.sFactory = cdvFactory.txtValue;
        }
        #endregion
    }
}

