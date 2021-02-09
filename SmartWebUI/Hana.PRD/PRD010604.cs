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
    public partial class PRD010604 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010604<br/>
        /// 클래스요약: Ship Lot Scrap 조회<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: Ship Lot Scrap 조회<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-01-26-임종우 : 쿼리 전체 수정(LOT별 2개 이상의 Loss Code에 따른 IN,OUT수량 중복 발생..)
        /// 2011-01-20-임종우 : 자사 Ship 된 Lot 조회 가능 하도록 수정 (임태성 요청)
        /// 2013-10-17-김민우 : LOT TYPE ALL, P%, E% 구분으로변경
        /// 2012-12-06-임종우 : LOSS CODE 누락 되는 오류 수정 : 원인은 LOSS CODE 구할때 ASSY -> TEST 로 이동하는 LOT 누락 됨.
        /// </summary>
        /// 
        public PRD010604()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
            ckbScrap.Checked = false;     
        }
              

        #region 유효성 검사 및 초기화

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
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Step", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type2", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Generation", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                        
            spdData.RPT_AddBasicColumn("Lot ID", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
            spdData.RPT_AddBasicColumn("IN Qty", 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("OUT Qty", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Number, 70);
            spdData.RPT_AddBasicColumn("YIELD", 0, 14, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.Double3, 70);
            spdData.RPT_AddBasicColumn("LOSS", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

            if(cdvFactory.txtValue.ToString() == "")
            {
                ;
            }
            else
            {
                DataTable dt_loss_code = null;
                dt_loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());

                if (dt_loss_code.Rows.Count != 0)
                {
                    for (int i = 0; i < dt_loss_code.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(dt_loss_code.Rows[i][0].ToString(), 0, (16 + i), Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    }
                }
            }            
                spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.                        
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "A.MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = A.MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "A.MAT_GRP_2", "MAT_GRP_2", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "B.OPER", "OPER", "NVL(OPER, ' ')", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "A.MAT_GRP_3", "MAT_GRP_3", "A.MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "A.MAT_GRP_4", "MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "A.MAT_GRP_5", "MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "A.MAT_GRP_6", "MAT_GRP_6", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "A.MAT_GRP_7", "MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "A.MAT_GRP_8", "MAT_GRP_8", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "A.MAT_ID", "MAT_ID", "A.MAT_ID", true);
        }

        #endregion


        #region SQL 쿼리 Build
        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
          
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;
            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();
            bool bcheck = false;
            
            DataTable dt_loss_code = null;
            dt_loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());                 
            
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            //2010-01-26-임종우 : Group에서 Step 체크 유무 확인
            string[] sList = tableForm.SelectedValueToQuery.Split(',');

            for (int i = 0; i < sList.Length; i++)
            {
                if (sList[i].Trim() == "B.OPER") bcheck = true;
            }

            strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond3);
            strSqlString.Append("     , A.LOT_ID, A.LOT_CMF_5" + "\n");
            strSqlString.Append("     , SUM(NVL(QTY_1,0)) +  SUM(NVL(B.LOSS_QTY,0)) - SUM(NVL(C.BNS_QTY,0)) AS IN_QTY" + "\n");
            strSqlString.Append("     , SUM(NVL(QTY_1,0)) AS OUT_QTY" + "\n");
            strSqlString.Append("     , ROUND((NVL(QTY_1,0)/(NVL(QTY_1,0) +  SUM(NVL(B.LOSS_QTY,0)) - SUM(NVL(C.BNS_QTY,0)) ))*100,3) AS YIELD " + "\n");
            strSqlString.Append("     , SUM(NVL(B.LOSS_QTY,0)) AS LOSS " + "\n");

            for (int i = 0; i < dt_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("     , SUM(NVL(B.\"" + dt_loss_code.Rows[i][0] + "\",0)) AS \"" + dt_loss_code.Rows[i][0] + "\" " + "\n");
            }

            strSqlString.Append("  FROM (  " + "\n");            
            strSqlString.Append("         SELECT B.MAT_GRP_1, B.MAT_GRP_2, B.MAT_GRP_3, B.MAT_GRP_4, B.MAT_GRP_5, B.MAT_GRP_6, B.MAT_GRP_7, B.MAT_GRP_8, A.MAT_ID " + "\n");
            strSqlString.Append("              , A.FACTORY" + "\n");
            strSqlString.Append("              , A.INVOICE_NO" + "\n");
            strSqlString.Append("              , A.LOT_ID" + "\n");
            strSqlString.Append("              , A.QTY_1 " + "\n");            
            strSqlString.Append("              , A.TRAN_TIME     " + "\n");
            strSqlString.Append("              , A.IN_QTY " + "\n");
            strSqlString.Append("              , A.LOT_CMF_5 " + "\n");
            strSqlString.Append("           FROM ( " + "\n");
            strSqlString.Append("                  SELECT FROM_FACTORY AS FACTORY" + "\n");
            strSqlString.Append("                       , INVOICE_NO" + "\n");
            strSqlString.Append("                       , MAT_ID" + "\n");
            strSqlString.Append("                       , LOT_ID" + "\n");
            strSqlString.Append("                       , SHIP_QTY_1 AS QTY_1 " + "\n");
            strSqlString.Append("                       , IN_QTY " + "\n");
            strSqlString.Append("                       , TRAN_TIME   " + "\n");
            strSqlString.Append("                       , LOT_CMF_5   " + "\n");
            strSqlString.Append("                    FROM VWIPSHPLOT   " + "\n");
            strSqlString.Append("                   WHERE 1=1   " + "\n");            
            strSqlString.Append("                     AND OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("                     AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                     AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                     AND TO_FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                //if (cboShipFactory.Text == "ALL")
                if (cboShipFactory.SelectedIndex == 0)
                {
                    strSqlString.Append("                     AND TO_FACTORY IN ('CUSTOMER', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                }
                //else if (cboShipFactory.Text == "Customer standard")
                else if (cboShipFactory.SelectedIndex == 1)
                {
                    strSqlString.Append("                     AND TO_FACTORY = 'CUSTOMER' " + "\n");
                }
                else
                {
                    strSqlString.Append("                     AND TO_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                }
            }
            else
            {
                strSqlString.Append("                     AND TO_FACTORY = 'CUSTOMER' " + "\n");
            }

            strSqlString.AppendFormat("                     AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("                     AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010','F0000')" + "\n");
            //strSqlString.Append("                                        AND TO_OPER = ' ' " + "\n");
            /*
            if(cdvLotType.txtValue != "ALL" || cdvLotType.txtValue != "")
            {
                strSqlString.Append("                     AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                     AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }

            strSqlString.Append("                ) A " + "\n");
            strSqlString.Append("                , MWIPMATDEF B " + "\n");
            strSqlString.Append("            WHERE 1=1 " + "\n");
            strSqlString.Append("              AND A.MAT_ID = B.MAT_ID(+) " + "\n");
            strSqlString.Append("              AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("              AND B.MAT_VER(+) = 1 " + "\n");


            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("              AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("       ) A " + "\n");
            strSqlString.Append("     , (  " + "\n");

            //2010-01-26-임종우 : Group에서 Step 체크 유무 확인하여 공정 표시
            //(이 부분 없으면 공정체크 안했을 경우 Lot별 2개이상의 Loss Code 존재시 In,Out 수량 중복발생)
            if (bcheck)
            {
                strSqlString.Append("         SELECT LOT_ID, OPER " + "\n");
            }
            else
            {
                strSqlString.Append("         SELECT LOT_ID " + "\n");
            }

            strSqlString.Append("              , SUM(NVL(LOSS_QTY,0)) AS LOSS_QTY " + "\n");

            for (int i = 0; i < dt_loss_code.Rows.Count; i++)
            {
                strSqlString.Append("              , SUM(DECODE(LOSS_CODE, '" + dt_loss_code.Rows[i][0] + "', LOSS_QTY, 0))AS \"" + dt_loss_code.Rows[i][0] + "\" " + "\n");
            }

            strSqlString.Append("           FROM RWIPLOTLSM" + "\n");
            strSqlString.Append("          WHERE HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("            AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            strSqlString.Append("            AND LOT_ID IN ( " + "\n");
            strSqlString.Append("                            SELECT LOT_ID" + "\n");
            strSqlString.Append("                              FROM VWIPSHPLOT   " + "\n");
            strSqlString.Append("                             WHERE 1=1   " + "\n");
            strSqlString.Append("                               AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                               AND OWNER_CODE = 'PROD' " + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                               AND TO_FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                //if (cboShipFactory.Text == "ALL")
                if (cboShipFactory.SelectedIndex == 0)
                {
                    strSqlString.Append("                               AND TO_FACTORY IN ('CUSTOMER', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                }
                //else if (cboShipFactory.Text == "Customer standard")
                else if (cboShipFactory.SelectedIndex == 1)
                {
                    strSqlString.Append("                               AND TO_FACTORY = 'CUSTOMER' " + "\n");
                }
                else
                {
                    strSqlString.Append("                               AND TO_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                }
            }
            else
            {
                strSqlString.Append("                               AND TO_FACTORY = 'CUSTOMER' " + "\n");
            }

            strSqlString.Append("                               AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                               AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010', 'F0000')" + "\n");
            strSqlString.AppendFormat("                               AND TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            /*
            if (cdvLotType.txtValue != "" && cdvLotType.txtValue != "ALL")
            {
                strSqlString.Append("                               AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                               AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
            
            strSqlString.Append("                          ) " + "\n");

            if (bcheck)
            {
                strSqlString.Append("          GROUP BY LOT_ID, OPER" + "\n");
            }
            else
            {
                strSqlString.Append("          GROUP BY LOT_ID " + "\n");
            }

            strSqlString.Append("       ) B" + "\n");
            //strSqlString.Append("     , (  " + "\n");
            //strSqlString.Append("         SELECT MAT_ID, LOT_ID, SUM(CREATE_QTY_1) CREATE_QTY_1  " + "\n");
            //strSqlString.Append("           FROM RWIPLOTSTS  " + "\n");
            //strSqlString.Append("          WHERE FACTORY = 'CUSTOMER'  " + "\n");
            //strSqlString.Append("            AND MAT_VER = 1   " + "\n");
            //strSqlString.Append("          GROUP BY MAT_ID, LOT_ID  " + "\n");
            //strSqlString.Append("       ) D  " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("         SELECT LOT_ID , SUM(NVL(TOTAL_BONUS_QTY,0)) AS BNS_QTY" + "\n");
            strSqlString.Append("           FROM RWIPLOTBNS" + "\n");
            strSqlString.Append("          WHERE 1=1" + "\n");
            strSqlString.Append("            AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("            AND HIST_DEL_FLAG = ' '" + "\n");
            strSqlString.AppendFormat("            AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("          GROUP BY FACTORY, LOT_ID " + "\n");
            strSqlString.Append("       ) C                     " + "\n");

            strSqlString.Append(" WHERE 1 = 1  " + "\n");

            if(txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
            {
                strSqlString.Append("   AND A.MAT_ID LIKE '" + txtSearchProduct.Text.Trim() + "' " + "\n");
            }

            strSqlString.Append("   AND A.LOT_ID = B.LOT_ID(+)                           " + "\n");
            strSqlString.Append("   AND A.LOT_ID = C.LOT_ID(+)        " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}, A.LOT_ID, QTY_1, LOT_CMF_5    " + "\n", QueryCond1);
            
            if(ckbScrap.Checked == true)
            {
                strSqlString.AppendFormat(" HAVING SUM(LOSS_QTY) > 0" + "\n");
            }

            strSqlString.AppendFormat(" ORDER BY {0}, A.LOT_ID, OUT_QTY     " + "\n", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();        
        }

        private string MakeSqlString1()             // 선택 공정에 존재하는 LOSS_CODE를 가져옴 ( 컬럼헤더 생성 )
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            string sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;


            strSqlString.Append("SELECT DISTINCT LOSS_CODE  " + "\n");
            strSqlString.Append("  FROM RWIPLOTLSM   " + "\n");
            strSqlString.Append(" WHERE FACTORY  " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND HIST_DEL_FLAG=' '  " + "\n");
            strSqlString.Append("   AND LOT_ID IN ( SELECT DISTINCT LOT_ID " + "\n");
            strSqlString.Append("                     FROM VWIPSHPLOT " + "\n");
            strSqlString.Append("                    WHERE 1=1" + "\n");
            strSqlString.Append("                      AND OWNER_CODE = 'PROD' " + "\n");
            strSqlString.Append("                      AND LOT_TYPE = 'W' " + "\n");
            strSqlString.Append("                      AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

            if (cdvFactory.txtValue == GlobalVariable.gsTestDefaultFactory)
            {
                strSqlString.Append("                      AND TO_FACTORY IN ('CUSTOMER', 'FGS') " + "\n");
            }
            else if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                //if (cboShipFactory.Text == "ALL")
                if (cboShipFactory.SelectedIndex == 0)
                {
                    strSqlString.Append("                      AND TO_FACTORY IN ('CUSTOMER', '" + GlobalVariable.gsTestDefaultFactory + "') " + "\n");
                }
                //else if (cboShipFactory.Text == "Customer standard")
                else if (cboShipFactory.SelectedIndex == 1)
                {
                    strSqlString.Append("                      AND TO_FACTORY = 'CUSTOMER' " + "\n");
                }
                else
                {
                    strSqlString.Append("                      AND TO_FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
                }
            }
            else
            {
                strSqlString.Append("                      AND TO_FACTORY = 'CUSTOMER' " + "\n");
            }
            
            strSqlString.AppendFormat("                      AND TRAN_TIME BETWEEN '{0}' AND '{1}'  " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("                      AND FROM_OPER IN ('AZ010','AZ009','TZ010','EZ010','F0000')" + "\n");
            /*
            if (cdvLotType.txtValue != "ALL" || cdvLotType.txtValue != "")
            {
                strSqlString.Append("                      AND LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("                      AND LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
            strSqlString.Append("                 )   " + "\n");
            strSqlString.Append("      ORDER BY LOSS_CODE  " + "\n");
                                 
            return strSqlString.ToString();
        }
        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
            //DurationTime_Check();

            DataTable dt = null;
            if (CheckField() == false) return;
            
            spdData_Sheet1.RowCount = 0;
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);              
                this.Refresh();

                
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
                GridColumnInit();

                if (dt.Rows.Count == 0)
                {                    
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //by John
                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 10, null, null, btnSort);
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSFromToCondition1.CountFromToValue, btnSort);
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSCondition1.SelectCount, btnSort);

                // 서브토탈시작할 컬럼, 서브토탈 몇개쓸껀지, 몇 컬럼까지 서브토탈 ~ 까지 안함, 자동채움시작컬럼 전컬럼, 디바이드 로우수                                                                                        
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)                

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 12, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_FillColumnData(9, new string[] {"IN", "OUT", "EOH", "BOH" });

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                spdData.ActiveSheet.Columns[9].AllowAutoSort = true;
                spdData.ActiveSheet.Columns[10].AllowAutoSort = true;
                spdData.ActiveSheet.Columns[11].AllowAutoSort = true;
                spdData.ActiveSheet.Columns[12].AllowAutoSort = true;                

                // YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                string subtotal = null;

                for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                {
                    if (spdData.ActiveSheet.Columns[i].Label == "YIELD")
                    {
                        spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value)) * 100).ToString();

                        for (int j = 0; j < spdData.ActiveSheet.Rows.Count; j++)
                        {
                            for (int k = 0; k < sub + 1; k++)
                            {
                                if (spdData.ActiveSheet.Cells[j, k].Value != null)
                                    subtotal = spdData.ActiveSheet.Cells[j, k].Value.ToString();

                                subtotal.Trim();
                                if (subtotal.Length > 5)
                                {
                                    if (subtotal.Substring(subtotal.Length - 5, 5) == "Total")
                                    {
                                        if (Convert.ToInt32(spdData.ActiveSheet.Cells[j, i - 2].Value) != 0)
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 1].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value)) * 100).ToString();
                                        }
                                        else
                                        {
                                            spdData.ActiveSheet.Cells[j, i].Value = 0;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }  

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

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            // Excel 바로 보이기
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, " ^ ", " ^ ", true);
            spdData.ExportExcel();            
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);                        
            //cdvLotType.sFactory = cdvFactory.txtValue;

            // 2011-01-20-임종우 : HMKA1 선택시 Ship Factory 검색 조건 활성화 시킴.
            if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
            {
                cboShipFactory.Enabled = true;
            }
            else
            {
                cboShipFactory.SelectedIndex = 0;
                cboShipFactory.Enabled = false;
            }
        }
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true && e.Column > 19)
            {

                string Query = "SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='LOSS_CODE' AND KEY_1= '" + spdData.ActiveSheet.Columns[e.Column].Label + "' AND ROWNUM=1";
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Query);

                ToolTip desc = new ToolTip();
                desc.Show(dt.Rows[0][0].ToString(), spdData, e.X + 10, e.Y, 1000);
            }
        }        
    }
}
