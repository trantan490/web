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
    public partial class PRD010605 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        private DataTable dt_loss_code = null;

        /// <summary>
        /// 클  래  스: PRD010605<br/>
        /// 클래스요약: Scrap 현황<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: Scrap 현황<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 
        /// 2009-09-24-임종우 : TEST PGM Column추가
        /// 2010-03-10-임종우 : LOSS Code별 Loss 수량 Data 불일치로 인하여 쿼리 수정.
        /// 2010-06-22-임종우 : 데이터 조회 안되어 쿼리 신규 작성하여 만듬. 오직 불량 데이터만 표시 됨. Terminate 데이터 조회 기능 추가
        /// 2011-03-22-김민우 : 조회 조건에 설비 DETAIL 추가
        /// 2011-03-22-김민우 : Spread Sheet 컬럼 한칸씩 밀린 거 수정
        /// 2011-04-04-배수민 : 화면에 Loss_code 검색 컨트롤 추가(다중 선택 가능) 및 기존 Loss_code 불러오는 쿼리 수정 
        /// 2011-10-19-임종우 : LOSS_CODE 검색 시 해당 LOSS_CODE 에 해당하는 LOT만 보이도록 수정.
        /// 2012-03-19-김민우 : LOSS가 발생하지 않은 LOT 정보까지 조회 기능 추가 (배진우K 요청)
        /// 2013-10-17-김민우: LOT TYPE ALL, P%, E% 구분으로변경
        /// </summary>
        /// 
        public PRD010605()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();            
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
            if (cdvOper.FromText.TrimEnd() == "" || cdvOper.ToText.TrimEnd() == "")
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
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
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);            
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
            
            // 2010-06-22-임종우 : Scrap 조회
            if (rdbScrap.Checked == true)
            {
                spdData.RPT_AddBasicColumn("Step", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("RUN ID", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Lot ID", 0, 11, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Type", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("PGM", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("In Time", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Out Time", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Equipment number", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Operator", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Input quantity", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Quantity of goods", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Loss", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("수율", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double3, 70);

                if (cdvFactory.txtValue.ToString() != "")
                {
                    dt_loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeLossSqlString());
                    
                    if (dt_loss_code.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt_loss_code.Rows.Count; i++)
                        {
                            spdData.RPT_AddBasicColumn(dt_loss_code.Rows[i][0].ToString(), 0, (22 + i), Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                        }
                    }
                }
            }
            else // 2010-06-22-임종우 : Terminate 조회
            {
                spdData.RPT_AddBasicColumn("RUN ID", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Lot ID", 0, 10, Visibles.True, Frozen.True, Align.Left, Merge.False, Formatter.String, 100);
                spdData.RPT_AddBasicColumn("Tran Time", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Type", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 40);
                spdData.RPT_AddBasicColumn("Qty", 0, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Del_Code", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Comment", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 100);
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "D.MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "D.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "D.MAT_GRP_3", "MAT_GRP_3", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "D.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "D.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "D.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "D.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "D.MAT_GRP_8", "MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "A.MAT_ID", "MAT_ID", true);
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
            
            string bbbb = string.Empty;
                                    
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            // 2010-06-22-임종우 : Scrap 조회
            if (rdbScrap.Checked == true)
            {
                //dt_loss_code = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeLossSqlString());
                
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("     , OPER, RUN_ID, LOT_ID, LOT_TYPE, PGM " + "\n");
                strSqlString.Append("     , IN_TIME, OUT_TIME, END_RES_ID " + "\n");
                strSqlString.Append("     , (SELECT USER_DESC FROM RWEBUSRDEF WHERE USER_ID = TRAN_USER_ID) || '(' || TRAN_USER_ID || ')' AS USER_ID " + "\n");
                strSqlString.Append("     , IN_QTY, OUT_QTY, TOTAL_LOSS " + "\n");
                strSqlString.Append("     , ROUND(OUT_QTY / IN_QTY * 100, 2) AS YIELD " + "\n");
                
                for (int i = 0; i < dt_loss_code.Rows.Count; i++)
                {
                    strSqlString.Append("     , SUM(DECODE(LOSS_CODE, '" + dt_loss_code.Rows[i][0] + "', LOSS_QTY, 0)) AS \"" + dt_loss_code.Rows[i][0] + "\" " + "\n");
                }

                strSqlString.Append("  FROM ( " + "\n");
                strSqlString.AppendFormat("         SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append("              , A.OPER" + "\n");
                strSqlString.Append("              , A.RUN_ID" + "\n");
                strSqlString.Append("              , A.LOT_ID" + "\n");
                strSqlString.Append("              , A.LOT_CMF_5 AS LOT_TYPE" + "\n");
                strSqlString.Append("              , A.IN_TIME " + "\n");
                strSqlString.Append("              , A.OUT_TIME" + "\n");
                strSqlString.Append("              , A.END_RES_ID " + "\n");
                strSqlString.Append("              , A.TRAN_USER_ID" + "\n");
                //김민우
                //strSqlString.Append("              , B.TOTAL_LOSS + A.OUT_QTY AS IN_QTY" + "\n");
                strSqlString.Append("              , NVL(B.TOTAL_LOSS,0) + A.OUT_QTY AS IN_QTY" + "\n");
                
                strSqlString.Append("              , A.OUT_QTY " + "\n");
                strSqlString.Append("              , B.LOSS_CODE" + "\n");
                strSqlString.Append("              , B.LOSS_QTY  " + "\n");
                //김민우
                //strSqlString.Append("              , B.TOTAL_LOSS  " + "\n");
                strSqlString.Append("              , NVL(B.TOTAL_LOSS,0) AS TOTAL_LOSS  " + "\n");

                // 2010-09-27-정비재 : Lot이력에 있는 Lot생산시 사용된 PGM을 표시한다.(수정함)
                //strSqlString.Append("              , C.ATTR_VALUE AS PGM " + "\n");
                strSqlString.Append("              , A.PGM AS PGM " + "\n");
                strSqlString.Append("           FROM (  " + "\n");
                strSqlString.Append("                 SELECT OLD_OPER AS OPER, MAT_ID, LOT_ID " + "\n");
                strSqlString.Append("                      , LOT_CMF_5, OLD_OPER_IN_TIME AS IN_TIME, TRAN_TIME AS OUT_TIME " + "\n");
                strSqlString.Append("                      , END_RES_ID, TRAN_USER_ID, QTY_1 AS OUT_QTY " + "\n");
                // 2010-09-27-정비재 : Lot이력에 있는 Lot생산시 사용된 PGM을 표시한다.(추가함)
                strSqlString.Append("                      , TRAN_CMF_3 AS PGM " + "\n");
                // 2010-12-06-김민우 : RUN_ID (추가함)
                strSqlString.Append("                      , LOT_CMF_4 AS RUN_ID " + "\n");

                strSqlString.Append("                   FROM RWIPLOTHIS  " + "\n");
                strSqlString.Append("                  WHERE OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                    AND MAT_VER = 1  " + "\n");
                strSqlString.Append("                    AND OLD_OPER IN (" + cdvOper.getInQuery() + ")" + "\n");
                strSqlString.Append("                    AND HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("                    AND TRAN_CODE IN ('END','SHIP') " + "\n");
                strSqlString.AppendFormat("                    AND TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sStart_Tran_Time, sEnd_Tran_Time);
                strSqlString.Append("                ) A " + "\n");
                strSqlString.Append("              , (         " + "\n");
                strSqlString.Append("                 SELECT LOT_ID, OPER, LOSS_CODE, LOSS_QTY, SUM(LOSS_QTY) OVER(PARTITION BY LOT_ID, OPER) AS TOTAL_LOSS " + "\n");
                strSqlString.Append("                   FROM RWIPLOTLSM  " + "\n");
                strSqlString.Append("                  WHERE FACTORY  " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                    AND MAT_VER = 1  " + "\n");
                strSqlString.Append("                    AND OPER IN (" + cdvOper.getInQuery() + ")" + "\n");
                strSqlString.Append("                    AND HIST_DEL_FLAG = ' '   " + "\n");
                strSqlString.Append("                ) B " + "\n");
                strSqlString.Append("              , (   " + "\n");
                strSqlString.Append("                 SELECT ATTR_KEY,ATTR_VALUE  " + "\n");
                strSqlString.Append("                   FROM MATRNAMSTS  " + "\n");
                strSqlString.Append("                  WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                    AND ATTR_TYPE = 'MAT_TEST_PGM' " + "\n");
                strSqlString.Append("                    AND ATTR_NAME = 'TEST_PROGRAM'  " + "\n");
                strSqlString.Append("                ) C " + "\n");
                strSqlString.Append("              , MWIPMATDEF D " + "\n");
                // 2011-03-22-김민우 : 조회 조건에 설비 DETAIL 추가
                if ( (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "") ||
                     (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "") ||
                     (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "") ||
                     (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "") ||
                     (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "") ||
                     (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "") ||
                     (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                   )
                {
                    strSqlString.Append("              , MRASRESDEF RES " + "\n");
                }



                strSqlString.Append("          WHERE 1 = 1  " + "\n");

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("            AND A.MAT_ID(+) LIKE '" + txtSearchProduct.Text.Trim() + "'" + "\n");
                }
                /*
                if (cdvLotType.txtValue != "ALL" && cdvLotType.txtValue != "")
                {
                    strSqlString.Append("            AND A.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("            AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                if (cbGubun.Text.Equals("ALL"))
                {
                    strSqlString.Append("            AND A.LOT_ID = B.LOT_ID(+) " + "\n");
                    strSqlString.Append("            AND A.OPER = B.OPER(+) " + "\n");
                }
                else
                {
                    strSqlString.Append("            AND A.LOT_ID = B.LOT_ID " + "\n");
                    strSqlString.Append("            AND A.OPER = B.OPER " + "\n");
                }
                strSqlString.Append("            AND A.MAT_ID = C.ATTR_KEY(+) " + "\n");
                strSqlString.Append("            AND A.MAT_ID = D.MAT_ID  " + "\n");
                strSqlString.Append("            AND D.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                
                // 2012-03-19-김민우 : LOSS 미발생 LOT 검색 구분
                if (cbGubun.Text.Equals("LOSS"))
                {
                    // 2011-10-19-임종우 : LOSS_CODE 검색 시 해당 LOSS_CODE 에 해당하는 LOT만 보이도록 수정.
                    strSqlString.Append("            AND LOSS_CODE " + cdvLossCode.SelectedValueToQueryString + "\n"); // Loss_code 선택하기 
                }
                
                // 2011-03-22-김민우 : 조회 조건에 설비 DETAIL 추가
                if ( (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "") ||
                     (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "") ||
                     (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "") ||
                     (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "") ||
                     (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "") ||
                     (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "") ||
                     (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                   )
                {
                    strSqlString.Append("            AND A.END_RES_ID = RES.RES_ID(+) " + "\n");
                }

                #region " RAS 상세 조회 "
                if (udcRASCondition1.Text != "ALL" && udcRASCondition1.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_GRP_1 {0} " + "\n", udcRASCondition1.SelectedValueToQueryString);

                if (udcRASCondition2.Text != "ALL" && udcRASCondition2.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_GRP_2 {0} " + "\n", udcRASCondition2.SelectedValueToQueryString);

                if (udcRASCondition3.Text != "ALL" && udcRASCondition3.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_GRP_3 {0} " + "\n", udcRASCondition3.SelectedValueToQueryString);

                if (udcRASCondition4.Text != "ALL" && udcRASCondition4.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_GRP_5 {0} " + "\n", udcRASCondition4.SelectedValueToQueryString);

                if (udcRASCondition5.Text != "ALL" && udcRASCondition5.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_GRP_6 {0} " + "\n", udcRASCondition5.SelectedValueToQueryString);

                if (udcRASCondition6.Text != "ALL" && udcRASCondition6.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_ID {0} " + "\n", udcRASCondition6.SelectedValueToQueryString);

                if (udcRASCondition7.Text != "ALL" && udcRASCondition7.Text != "")
                    strSqlString.AppendFormat("   AND RES.RES_ID IN ( SELECT UNIQUE RES_ID FROM CRASRESUSR WHERE USER_DESC {0} ) " + "\n", udcRASCondition7.SelectedValueToQueryString);

                #endregion



                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("            AND D.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                strSqlString.Append("       ) DAT" + "\n");
                strSqlString.AppendFormat(" GROUP BY {0}, OPER, RUN_ID, LOT_ID, LOT_TYPE, PGM, IN_TIME, OUT_TIME, END_RES_ID, TRAN_USER_ID, IN_QTY, OUT_QTY, TOTAL_LOSS " + "\n", QueryCond1);
                strSqlString.AppendFormat(" ORDER BY {0}, OPER, RUN_ID, LOT_ID, LOT_TYPE, PGM, IN_TIME, OUT_TIME, END_RES_ID, TRAN_USER_ID, IN_QTY, OUT_QTY, TOTAL_LOSS " + "\n", QueryCond1);
            }
            else // 2010-06-22-임종우 : Terminate 조회
            {
                strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond2);
                strSqlString.Append("     , A.LOT_CMF_4 AS RUN_ID, A.LOT_ID, A.TRAN_TIME, A.LOT_CMF_5 " + "\n");
                strSqlString.Append("     , A.QTY_1, A.LOT_DEL_CODE, A.TRAN_COMMENT " + "\n");
                strSqlString.Append("  FROM RWIPLOTHIS A " + "\n");
                strSqlString.Append("     , MWIPMATDEF D" + "\n");
                strSqlString.Append(" WHERE 1=1 " + "\n");
                strSqlString.Append("   AND A.OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND A.MAT_VER = 1  " + "\n");
                strSqlString.Append("   AND A.OLD_OPER IN (" + cdvOper.getInQuery() + ")" + "\n");
                strSqlString.Append("   AND A.HIST_DEL_FLAG = ' ' " + "\n");
                strSqlString.Append("   AND A.TRAN_CODE = 'TERMINATE' " + "\n");
                strSqlString.AppendFormat("   AND A.TRAN_TIME BETWEEN '{0}' AND '{1}'" + "\n", sStart_Tran_Time, sEnd_Tran_Time);
                strSqlString.Append("   AND D.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("   AND A.MAT_ID = D.MAT_ID " + "\n");

                if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                {
                    strSqlString.Append("   AND A.MAT_ID(+) LIKE '" + txtSearchProduct.Text.Trim() + "'" + "\n");
                }
                /*
                if (cdvLotType.txtValue != "ALL" && cdvLotType.txtValue != "")
                {
                    strSqlString.Append("   AND A.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
                }
                */
                if (cdvLotType.Text != "ALL")
                {
                    strSqlString.Append("   AND A.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
                }

                //상세 조회에 따른 SQL문 생성                        
                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND D.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
                                
                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond1);
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                     
            return strSqlString.ToString();        
        }

        private string MakeLossSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string sStart_Tran_Time = null;
            string sEnd_Tran_Time = null;
     
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            sStart_Tran_Time = cdvFromToDate.Start_Tran_Time.ToString();
            sEnd_Tran_Time = cdvFromToDate.End_Tran_Time.ToString();

            strSqlString.Append("SELECT DISTINCT LOSS_CODE " + "\n");
            strSqlString.Append("  FROM RWIPLOTHIS HIS " + "\n");
            strSqlString.Append("     , RWIPLOTLSM LOS " + "\n");
            strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE 1 = 1   " + "\n");
            strSqlString.Append("   AND HIS.OLD_FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND HIS.MAT_VER = 1  " + "\n");
            strSqlString.Append("   AND HIS.OLD_OPER IN (" + cdvOper.getInQuery() + ")" + "\n");
            strSqlString.Append("   AND HIS.HIST_DEL_FLAG=' '  " + "\n");
            strSqlString.Append("   AND HIS.TRAN_CODE IN ('END','SHIP') " + "\n");
            strSqlString.AppendFormat("   AND HIS.TRAN_TIME BETWEEN '{0}' AND '{1}' " + "\n", sStart_Tran_Time, sEnd_Tran_Time);
            strSqlString.Append("   AND HIS.OLD_FACTORY = LOS.FACTORY " + "\n");
            strSqlString.Append("   AND HIS.LOT_ID = LOS.LOT_ID " + "\n");
            strSqlString.Append("   AND HIS.OLD_OPER = LOS.OPER " + "\n");
            strSqlString.Append("   AND LOS.LOSS_CODE " + cdvLossCode.SelectedValueToQueryString + "\n"); // Loss_code 선택하기
            strSqlString.Append("   AND LOS.HIST_DEL_FLAG = ' ' " + "\n");
            strSqlString.Append("   AND MAT.FACTORY = '" + cdvFactory.Text + "' " + "\n");
            strSqlString.Append("   AND HIS.MAT_ID = MAT.MAT_ID " + "\n");
            
            

            if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
            {
                strSqlString.Append("   AND HIS.MAT_ID LIKE '" + txtSearchProduct.Text.Trim() + "'" + "\n");
            }
            /*
            if (cdvLotType.txtValue != "ALL" && cdvLotType.txtValue != "")
            {
                strSqlString.Append("   AND HIS.LOT_CMF_5 " + cdvLotType.SelectedValueToQueryString + "\n");
            }
            */
            if (cdvLotType.Text != "ALL")
            {
                strSqlString.Append("   AND HIS.LOT_CMF_5 LIKE '" + cdvLotType.Text + "'" + "\n");
            }
            #region 상세 조회
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0}" + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0}" + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            strSqlString.Append(" ORDER BY LOSS_CODE  " + "\n");

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

                GridColumnInit();
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //spdData.DataSource = dt;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);

                //1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 9, null, null, btnSort);

                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

                if (rdbScrap.Checked == true)
                {
                    // YIELD 부분의  TOTAL값 및 SUB TOTAL을 계산하지 않고 직접 계산 
                    string subtotal = null;

                    for (int i = 0; i < spdData.ActiveSheet.Columns.Count; i++)
                    {
                        if (spdData.ActiveSheet.Columns[i].Label == "수율")
                        {
                            spdData.ActiveSheet.Cells[0, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[0, i - 3].Value)) * 100).ToString();

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
                                                spdData.ActiveSheet.Cells[j, i].Value = ((Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 2].Value) / Convert.ToDouble(spdData.ActiveSheet.Cells[j, i - 3].Value)) * 100).ToString();
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

        //2010-04-04-배수민 : GCM 테이블 이용하여 Loss_code 불러오기 
        private void cdvLossCode_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            strQuery += "SELECT KEY_1 AS CODE, DATA_1 AS NAME FROM MGCMTBLDAT" + "\n";
            strQuery += " WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n";
            strQuery += "   AND TABLE_NAME = 'LOSS_CODE' " + "\n";

            cdvLossCode.sDynamicQuery = strQuery;
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
            cdvOper.sFactory = cdvFactory.txtValue;
            cdvLossCode.sFactory = cdvFactory.txtValue;
        }
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == true && e.Column > 20)
            {

                string Query = "SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME='LOSS_CODE' AND KEY_1= '" + spdData.ActiveSheet.Columns[e.Column].Label + "' AND ROWNUM=1";
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Query);

                ToolTip desc = new ToolTip();
                desc.Show(dt.Rows[0][0].ToString(), spdData, e.X + 10, e.Y, 1000);
            }
        }

        private void cdvLoss_Code_ValueButtonPress(object sender, EventArgs e)
        {

        }
    }
}
