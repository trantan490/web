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
    public partial class PRD010402 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {

        /// <summary>
        /// 클  래  스: PRD010402<br/>
        /// 클래스요약: 공정별 Movement<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-12-05<br/>
        /// 상세  설명: 공정별 Movement<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 변  경  자: 하나마이크론 김민우<br />
        /// STACK 제품만 생산실적이 맞지 않는 오류 수정<br />
        /// 2012-05-24-임종우 : LOT TYPE 구분 추가
        /// </summary>
        /// 
        DataTable global_oper_desc = null;
        
        public PRD010402()
        {
            InitializeComponent();
            cdvFromToDate.AutoBinding();
            SortInit();           
            GridColumnInit();
            Time_Base.SelectedIndex = 1;
            rdoDetail.Checked = true;            
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

            if (rdoDetail.Checked == true)
            {
                if (cdvOper.Text.TrimEnd() == "")
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                    return false;
                }

                if (cdvFactory.txtValue == GlobalVariable.gsAssyDefaultFactory)
                {
                    if (cdvOper.Text.TrimEnd() == "ALL" || cdvOper.SelectCount > 40)
                    {
                        CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD053", GlobalVariable.gcLanguage));
                        return false;
                    }
                }
            }
            else
            {
                if (cdvOperGroup.FromText == "" || cdvOperGroup.ToText == "")
                {
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD005", GlobalVariable.gcLanguage));
                    return false;
                }
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
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Pin Type", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 9, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 100);
            spdData.RPT_AddBasicColumn("Cust Device", 0, 10, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Classification", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

            if (cdvFactory.txtValue != "")
            {
                if (rdoDetail.Checked == true)
                {
                    if(ckbKpcs.Checked == true)
                    {
                        spdData.RPT_AddDynamicColumn(cdvOper, 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double1, 80);
                    }
                    else
                    {
                        spdData.RPT_AddDynamicColumn(cdvOper, 0, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    
                }
                else
                {
                    for (int i = 0; i < global_oper_desc.Rows.Count; i++)
                    {
                        if(ckbKpcs.Checked == true)
                        {
                            spdData.RPT_AddBasicColumn(global_oper_desc.Rows[i][0].ToString(), 0, (12 + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Double1, 80);
                        }
                        else
                        {
                            spdData.RPT_AddBasicColumn(global_oper_desc.Rows[i][0].ToString(), 0, (12 + i), Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        }                        
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
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", "A.MAT_GRP_1", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "A.MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "MAT_GRP_3", "A.MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "MAT_GRP_4", "A.MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "A.MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "A.MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "A.MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "A.MAT_GRP_8", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT_CMF_10", "MAT_CMF_10", "A.MAT_CMF_10", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_ID", "MAT_ID", "A.MAT_ID", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT_CMF_7", "MAT_CMF_7", "A.MAT_CMF_7", false);               
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
            
            string sFrom = null;
            string sTo = null;            
            string bbbb = null;
            string sRange = string.Empty;
            string strDate = string.Empty;
            
            string strFromDate = cdvFromToDate.ExactFromDate;
            string strToDate = cdvFromToDate.ExactToDate;

            DataTable dt_oper_desc = null;
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            ////기간별 조회 SQL문 생성
            //if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "DAY")
            //{
                sFrom = cdvFromToDate.FromDate.Text.Replace("-", "");
                sTo = cdvFromToDate.ToDate.Text.Replace("-", "");
                sRange = "D";
                strDate = "WORK_DATE";
            //}
            //else if (cdvFromToDate.DaySelector.SelectedValue.ToString() == "WEEK")
            //{
            //    sFrom = cdvFromToDate.FromWeek.SelectedValue.ToString();
            //    sTo = cdvFromToDate.ToWeek.SelectedValue.ToString();
            //    sRange = "W";
            //    strDate = "WORK_WEEK";
            //}
            //else
            //{
            //    sFrom = cdvFromToDate.FromYearMonth.Value.ToString("yyyyMM");
            //    sTo = cdvFromToDate.ToYearMonth.Value.ToString("yyyMM");
            //    sRange = "M";
            //    strDate = "WORK_MONTH";
            //}
            
            
            
            if(rdoSummary.Checked == true)
            {                   
                dt_oper_desc = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                global_oper_desc = dt_oper_desc;
            }

            strSqlString.AppendFormat(" SELECT {0}, ' ' " + "\n", QueryCond2);

            if (rdoDetail.Checked == true)
            {
                if (ckbKpcs.Checked == false)
                {
                    bbbb = cdvOper.getRepeatQuery("SUM(NVL(BOH_QTY", ",0))", "BOH");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("SUM(NVL(IN_QTY", ",0))", "IN");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;
                    
                    bbbb = cdvOper.getRepeatQuery("SUM(NVL(OUT_QTY", ",0))", "OUT");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("SUM(NVL(EOH_QTY", ",0))", "EOH");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("SUM(NVL(LOSS_QTY", ",0))", "LOSS");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;
                }
                else
                {
                    bbbb = cdvOper.getRepeatQuery("ROUND(SUM(NVL(BOH_QTY", ",0))/1000,1)", "BOH");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("ROUND(SUM(NVL(IN_QTY", ",0))/1000,1)", "IN");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("ROUND(SUM(NVL(OUT_QTY", ",0))/1000,1)", "OUT");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("ROUND(SUM(NVL(EOH_QTY", ",0))/1000,1)", "EOH");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;

                    bbbb = cdvOper.getRepeatQuery("ROUND(SUM(NVL(LOSS_QTY", ",0))/1000,1)", "LOSS");
                    strSqlString.AppendFormat(" {0} ", bbbb);
                    bbbb = null;
                }
            }
            else
            {
                if (ckbKpcs.Checked == false)
                {
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",SUM(NVL(BOH_QTY" + i + ",0)) BOH_QTY" + i + "\n");
                    }

                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",SUM(NVL(IN_QTY" + i + ",0)) IN_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",SUM(NVL(OUT_QTY" + i + ",0)) OUT_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",SUM(NVL(EOH_QTY" + i + ",0)) EOH_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",SUM(NVL(LOSS_QTY" + i + ",0)) LOSS_QTY" + i + "\n");
                    }
                }
                else
                {
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",ROUND(SUM(NVL(BOH_QTY" + i + ",0))/1000,1) BOH_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",ROUND(SUM(NVL(IN_QTY" + i + ",0))/1000,1) IN_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",ROUND(SUM(NVL(OUT_QTY" + i + ",0))/1000,1) OUT_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",ROUND(SUM(NVL(EOH_QTY" + i + ",0))/1000,1) EOH_QTY" + i + "\n");
                    }
                    for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                    {
                        strSqlString.Append(",ROUND(SUM(NVL(LOSS_QTY" + i + ",0))/1000,1) LOSS_QTY" + i + "\n");
                    }
                }
            }

            strSqlString.Append("  FROM ( " + "\n");
            strSqlString.AppendFormat("       SELECT {0}, ' ' " + "\n", QueryCond1);
           
            // , DECODE(OPER_GRP_1, 'BACKGRIND', IN_QTY,0) IN_QTY0
            if (rdoSummary.Checked == true)
            {
                for(int i=0; i < dt_oper_desc.Rows.Count; i++)
                {
                    strSqlString.Append("            , DECODE(OPER_GRP_7, '" + dt_oper_desc.Rows[i][0] + "', IN_QTY,0) IN_QTY" + i + "\n");
                    //2011-10-10-김민우: D/A,W/B 공정에서의 MCP OUT 수량 2MCP는 A0402,A602 OUT 수량만 3MCP는 A0403,A0603
                    if (dt_oper_desc.Rows[i][0].Equals("D/A"))
                    {
                        strSqlString.Append("            , DECODE(OPER_GRP_7, '" + dt_oper_desc.Rows[i][0] + "', DECODE(SUBSTR(MAT_GRP_4,-1),'2', DECODE(OPER,'A0402',OUT_QTY,0),'3', DECODE(OPER,'A0403',OUT_QTY,0),OUT_QTY),0) OUT_QTY" + i + "\n");
                    }
                    else if (dt_oper_desc.Rows[i][0].Equals("W/B"))
                    {
                        strSqlString.Append("            , DECODE(OPER_GRP_7, '" + dt_oper_desc.Rows[i][0] + "', DECODE(SUBSTR(MAT_GRP_4,-1),'2', DECODE(OPER,'A0602',OUT_QTY,0),'3', DECODE(OPER,'A0603',OUT_QTY,0),OUT_QTY),0) OUT_QTY" + i + "\n");
                    }
                    else
                    {
                        strSqlString.Append("            , DECODE(OPER_GRP_7, '" + dt_oper_desc.Rows[i][0] + "', OUT_QTY,0) OUT_QTY" + i + "\n");
                    }
                    strSqlString.Append("            , DECODE(OPER_GRP_1, '" + dt_oper_desc.Rows[i][0] + "', EOH_QTY,0) EOH_QTY" + i + "\n");
                    strSqlString.Append("            , DECODE(OPER_GRP_1, '" + dt_oper_desc.Rows[i][0] + "', BOH_QTY,0) BOH_QTY" + i + "\n");
                    strSqlString.Append("            , DECODE(OPER_GRP_1, '" + dt_oper_desc.Rows[i][0] + "', LOSS,0) LOSS_QTY" + i + "\n");
                }

            }
            else
            {
                ListView oper = cdvOper.getSelectedItems();


                for (int i = 0; i < oper.Items.Count; i++)
                {
                    strSqlString.Append("            , DECODE(OPER, '" + oper.Items[i].Text + "', IN_QTY,0) IN_QTY" + i + "\n");

                    if (oper.Items[i].Text == "AZ010" || oper.Items[i].Text == "TZ010" || oper.Items[i].Text == "EZ010")
                    {
                        strSqlString.AppendFormat("            , DECODE(OPER, '" + oper.Items[i].Text + "', ({0}),0) OUT_QTY" + i + "\n", MakeSqlString2());
                    }
                    else
                    {
                        strSqlString.Append("            , DECODE(OPER, '" + oper.Items[i].Text + "', OUT_QTY,0) OUT_QTY" + i + "\n");
                    }
                    
                    strSqlString.Append("            , DECODE(OPER, '" + oper.Items[i].Text + "', EOH_QTY,0) EOH_QTY" + i + "\n");
                    strSqlString.Append("            , DECODE(OPER, '" + oper.Items[i].Text + "', BOH_QTY,0) BOH_QTY" + i + "\n");
                    strSqlString.Append("            , DECODE(OPER, '" + oper.Items[i].Text + "', LOSS,0) LOSS_QTY" + i + "\n");
                }             
            }

            strSqlString.Append("         FROM (  " + "\n");
            strSqlString.Append("              SELECT M.MAT_GRP_1, M.MAT_GRP_2, M.MAT_GRP_3, M.MAT_GRP_4, M.MAT_GRP_5, M.MAT_GRP_6, M.MAT_GRP_7, M.MAT_GRP_8, M.MAT_CMF_10, M.MAT_ID, M.MAT_CMF_7 " + "\n");
            
            //김민우
            //strSqlString.Append("                   , M.OPER, M.BOH_QTY, M.EOH_QTY, N.IN_QTY, N.OUT_QTY, LOSS.LOSS   " + "\n");
            strSqlString.Append("                   , M.OPER, M.BOH_QTY, M.EOH_QTY, M.IN_QTY, M.OUT_QTY, LOSS.LOSS   " + "\n");
            
            if(rdoSummary.Checked == true)
            {
                strSqlString.Append("                        ,OPER_GRP_1, OPER_GRP_7 " + "\n");
            }

            strSqlString.Append("                FROM ( " + "\n");
            //김민우
            strSqlString.Append("                     SELECT MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_10, MAT_ID, MAT_CMF_7, OPER  " + "\n");
            strSqlString.Append("                          , SUM(BOH_QTY) AS BOH_QTY, SUM(EOH_QTY) AS EOH_QTY, SUM(IN_QTY) AS IN_QTY, SUM(OUT_QTY) AS OUT_QTY  " + "\n");

            if (rdoSummary.Checked == true)
            {
                strSqlString.Append("                          , MAX(OPER_GRP_1) AS OPER_GRP_1, MAX(OPER_GRP_7) AS OPER_GRP_7 " + "\n");
            }


            strSqlString.Append("                       FROM (  " + "\n");
            strSqlString.Append("                             SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7 " + "\n");
            strSqlString.Append("                                  , B.OPER " + "\n");
            
            string From_Day = strFromDate.Substring(0, 8);
            From_Day = From_Day.Insert(4, "-");
            From_Day = From_Day.Insert(7, "-");
            DateTime Boh_Day = DateTime.Parse(From_Day);
            From_Day = Boh_Day.ToString("yyyyMMdd");
            string To_Day = strToDate.Substring(0, 8);

            strSqlString.AppendFormat("                                  , SUM(CASE WHEN C.WORK_DATE = '{0}' AND SHIFT = 3 " + "\n", From_Day);
            strSqlString.Append("                                         THEN EOH_QTY_1  " + "\n");
            strSqlString.Append("                                         ELSE 0  " + "\n");
            strSqlString.Append("                                    END) AS BOH_QTY " + "\n");
            strSqlString.AppendFormat("                                  , SUM(CASE WHEN C.WORK_DATE = '{0}' AND SHIFT = 3 " + "\n", To_Day);
            strSqlString.Append("                                         THEN EOH_QTY_1  " + "\n");
            strSqlString.Append("                                         ELSE 0  " + "\n");
            strSqlString.Append("                                    END) AS EOH_QTY " + "\n");
            strSqlString.Append("                                  , 0 AS IN_QTY  " + "\n");
            strSqlString.Append("                                  , 0 AS OUT_QTY " + "\n");

            if (rdoSummary.Checked == true)
            {
                strSqlString.Append("                                  ,OPER_GRP_1, OPER_GRP_7 " + "\n");
            }


            if (Time_Base.SelectedIndex == 1)
            {
                strSqlString.Append("                        FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("                           , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                           , RSUMWIPEOH C " + "\n");
            }
            else
            {
                strSqlString.Append("                        FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("                           , MWIPOPRDEF B " + "\n");
                strSqlString.Append("                           , CSUMWIPEOH C " + "\n");
            }

            strSqlString.Append("                        WHERE 1 = 1 " + "\n");
            strSqlString.Append("                          AND A.MAT_VER = 1  " + "\n");
            strSqlString.Append("                          AND C.MAT_VER = 1 " + "\n");

            if (txtSearchProduct.Text != "" && txtSearchProduct.Text != "%")
                strSqlString.Append("                          AND C.MAT_ID LIKE '" + txtSearchProduct.Text + "' \n");

            if (cdvLotType.Text != "")
                strSqlString.Append("                          AND C.CM_KEY_3 " + cdvLotType.SelectedValueToQueryString + "\n");

            if (rdoSummary.Checked == false)
            {
                strSqlString.Append("                          AND C.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append("                          AND B.OPER = C.OPER  " + "\n");
            strSqlString.Append("                          AND C.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                          AND B.FACTORY = A.FACTORY " + "\n");
            strSqlString.Append("                          AND B.FACTORY = C.FACTORY  " + "\n");
            strSqlString.Append("                          AND C.MAT_ID = A.MAT_ID " + "\n");
            strSqlString.Append("                          AND C.LOT_TYPE = 'W' " + "\n");

            strSqlString.AppendFormat("                          AND C.WORK_DATE BETWEEN '{0}' AND '{1}'" + "\n", From_Day, To_Day);
            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

            strSqlString.Append("                     GROUP BY A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, B.OPER " + "\n");

            if (rdoSummary.Checked == true)
            {
                strSqlString.Append("                            , OPER_GRP_1, OPER_GRP_7 " + "\n");
            }

            strSqlString.Append("                             UNION ALL" + "\n");
            strSqlString.Append("                             SELECT A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7 " + "\n");
            strSqlString.Append("                             , B.OPER " + "\n");
            strSqlString.Append("                             , 0 AS BOH_QTY " + "\n");
            strSqlString.Append("                             , 0 AS EOH_QTY " + "\n");
            strSqlString.Append("                             , SUM(S1_OPER_IN_QTY_1 + S2_OPER_IN_QTY_1 + S3_OPER_IN_QTY_1) AS IN_QTY  " + "\n");
            strSqlString.Append("                             , SUM(S1_END_QTY_1 + S2_END_QTY_1 + S3_END_QTY_1 + S1_END_RWK_QTY_1 + S2_END_RWK_QTY_1 + S3_END_RWK_QTY_1 ) AS OUT_QTY  " + "\n");
            //김민우
            if (rdoSummary.Checked == true)
            {
                strSqlString.Append("                             , ' ' AS OPER_GRP_1, ' ' AS OPER_GRP_7  " + "\n");
            }
            if (Time_Base.SelectedIndex == 1)
            {
                strSqlString.Append("                               FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("                                  , RSUMWIPMOV B " + "\n");
            }
            else
            {
                strSqlString.Append("                               FROM MWIPMATDEF A " + "\n");
                strSqlString.Append("                                  , CSUMWIPMOV B " + "\n");
            }

            strSqlString.Append("                              WHERE 1 = 1     " + "\n");
            strSqlString.Append("                                AND B.MAT_VER=1  " + "\n");
            strSqlString.Append("                                AND B.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("                                AND B.MAT_ID LIKE '" + txtSearchProduct.Text + "' \n");
            strSqlString.Append("                                AND B.MAT_ID = A.MAT_ID  " + "\n");
            strSqlString.Append("                                AND B.FACTORY = A.FACTORY    " + "\n");
            strSqlString.Append("                                AND B.LOT_TYPE = 'W'    " + "\n");

            if (rdoSummary.Checked == false)
            {
                strSqlString.Append("                                AND B.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }


            strSqlString.AppendFormat("                                AND B.WORK_DATE BETWEEN '{0}' AND '{1}' " + "\n", sFrom, To_Day);
            //상세 조회에 따른 SQL문 생성                        
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString.AppendFormat("                          AND A.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            strSqlString.Append("                             GROUP BY A.MAT_GRP_1, A.MAT_GRP_2, A.MAT_GRP_3, A.MAT_GRP_4, A.MAT_GRP_5, A.MAT_GRP_6, A.MAT_GRP_7, A.MAT_GRP_8, A.MAT_CMF_10, A.MAT_ID, A.MAT_CMF_7, B.OPER   " + "\n");
            strSqlString.Append("                            )   " + "\n");
            strSqlString.Append("                     GROUP BY MAT_GRP_1, MAT_GRP_2, MAT_GRP_3, MAT_GRP_4, MAT_GRP_5, MAT_GRP_6, MAT_GRP_7, MAT_GRP_8, MAT_CMF_10, MAT_ID, MAT_CMF_7, OPER    " + "\n");
           
            strSqlString.Append("                     )M " + "\n");
            strSqlString.Append("                   , ( " + "\n");
            strSqlString.Append("                     SELECT MAT_ID, OPER, SUM(LOSS_QTY) AS LOSS " + "\n");
            strSqlString.Append("                       FROM RWIPLOTLSM " + "\n");
            strSqlString.Append("                     WHERE 1 = 1 " + "\n");
            strSqlString.Append("                       AND MAT_VER=1   " + "\n");
            strSqlString.Append("                       AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            
            if(rdoSummary.Checked == false)
            {
                strSqlString.Append("                       AND OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }            
            
            //strSqlString.Append("                       AND GET_WORK_DATE(TRAN_TIME, '" + sRange + "') BETWEEN '" + sFrom + "' AND '" + sTo + "'" + "\n");
            strSqlString.Append("                       AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n");
            strSqlString.Append("                  GROUP BY MAT_ID, OPER " + "\n");
            strSqlString.Append("                     )LOSS " + "\n");
            strSqlString.Append("               WHERE 1 = 1 " + "\n");
            strSqlString.Append("                 AND M.MAT_ID = LOSS.MAT_ID(+)  " + "\n");
            strSqlString.Append("                 AND M.OPER = LOSS.OPER(+) " + "\n");
            strSqlString.Append("           )  A" + "\n");
            strSqlString.Append("    )  " + "\n");
            strSqlString.AppendFormat(" GROUP BY {0}" + "\n", QueryCond1);
            strSqlString.Append(" HAVING  " + "\n");
            if (rdoSummary.Checked == true)
            {
                for (int i = 0; i < dt_oper_desc.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        strSqlString.Append(" SUM(NVL(BOH_QTY" + i + ",0)) + SUM(NVL(IN_QTY" + i + ",0)) + SUM(NVL(OUT_QTY" + i + ",0)) + SUM(NVL(EOH_QTY" + i + ",0)) + SUM(NVL(LOSS_QTY" + i + ",0))   " + "\n");
                    }
                    else
                    {
                        strSqlString.Append(" + SUM(NVL(BOH_QTY" + i + ",0)) + SUM(NVL(IN_QTY" + i + ",0)) + SUM(NVL(OUT_QTY" + i + ",0)) + SUM(NVL(EOH_QTY" + i + ",0)) + SUM(NVL(LOSS_QTY" + i + ",0))   " + "\n");
                    }
                }
            }
            else
            {
                ListView oper2 = cdvOper.getSelectedItems();
                for (int i = 0; i < oper2.Items.Count; i++)
                {
                    if (i == 0)
                    {
                        strSqlString.Append(" SUM(NVL(BOH_QTY" + i + ",0)) + SUM(NVL(IN_QTY" + i + ",0)) + SUM(NVL(OUT_QTY" + i + ",0)) + SUM(NVL(EOH_QTY" + i + ",0)) + SUM(NVL(LOSS_QTY" + i + ",0))   " + "\n");
                    }
                    else
                    {
                        strSqlString.Append(" + SUM(NVL(BOH_QTY" + i + ",0)) + SUM(NVL(IN_QTY" + i + ",0)) + SUM(NVL(OUT_QTY" + i + ",0)) + SUM(NVL(EOH_QTY" + i + ",0)) + SUM(NVL(LOSS_QTY" + i + ",0))   " + "\n");
                    }

                }
            }
            strSqlString.Append(" > 0  " + "\n");
            strSqlString.AppendFormat(" ORDER BY {0}", QueryCond1);

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();                            
        }
        
        private string MakeSqlString1()             // 선택 공정의 공정그룹 정보를 가져옴 ( 컬럼헤더 생성 )
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER_GRP_1" + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF " + "\n");
            strSqlString.Append(" WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
            strSqlString.Append("   AND OPER_CMF_4 <> ' '    " + "\n");

            if (rdoSummary.Checked == true)
            {
                strSqlString.AppendFormat("   AND OPER_GRP_1 IN ({0})" + "\n", cdvOperGroup.getInQuery());
            }
            else
            {
                strSqlString.Append("   AND OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }

            strSqlString.Append(" GROUP BY OPER_GRP_1" + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(MIN(OPER_CMF_4)) ASC" + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString2()             // ship 공정일 경우 따로 out 수량을 가져옴
        {
            StringBuilder strSqlString = new StringBuilder();

            string from = cdvFromToDate.getFromTranTime();
            string to = cdvFromToDate.getToTranTime();

            strSqlString.Append(" SELECT SUM(SHIP_QTY_1) ");
            strSqlString.Append("   FROM VWIPSHPLOT " );
            strSqlString.Append("  WHERE 1=1" );
            strSqlString.Append("   AND MAT_ID = A.MAT_ID " );
            strSqlString.AppendFormat("   AND TRAN_TIME BETWEEN '{0}' AND '{1}' " , from, to);
            strSqlString.Append("   AND LOT_TYPE = 'W'" );
            strSqlString.Append("   AND FROM_FACTORY " + cdvFactory.SelectedValueToQueryString);
            //strSqlString.Append("   AND TO_FACTORY = 'CUSTOMER'" );
            strSqlString.AppendFormat("   AND FROM_OPER = {0}" , "A.OPER");
            strSqlString.Append("   AND OWNER_CODE = 'PROD'" );

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
                //by John
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 11, null, null, btnSort);
                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 11;

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //by John (두줄짜리이상 + 부분합계.)
                //1.Griid 합계 표시
                //int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, 1, 9, 10, 4, udcCUSFromToCondition1.CountFromToValue, btnSort);
                
                if(rdoDetail.Checked == true)
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub+1, 11, 12, 5, cdvOper.SelectCount, btnSort);
                }
                else
                {
                    int[] rowType = spdData.RPT_DataBindingWithSubTotalAndDivideRows(dt, 0, sub+1, 11, 12, 5, global_oper_desc.Rows.Count, btnSort);
                }
                

                // 서브토탈시작할 컬럼, 서브토탈 몇개쓸껀지, 몇 컬럼까지 서브토탈 ~ 까지 안함, 자동채움시작컬럼 전컬럼, 디바이드 로우수                                                                                        
                //구분항목 값 생성(구분이 들어가는 위치임. 0부터 시작)                
                               
                //3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 5, true, Align.Center, VerticalAlign.Center);

                spdData.RPT_FillColumnData(11, new string[] { "BOH", "IN", "OUT", "EOH", "LOSS" });

                //4. Column Auto Fit
                spdData.RPT_AutoFit(false);

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
            cdvOper.sFactory = cdvFactory.txtValue;
            cdvOperGroup.sFactory = cdvFactory.txtValue;
            cdvLotType.sFactory = cdvFactory.txtValue;
        }
        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (rdoDetail.Checked == true)
            {
                if (e.ColumnHeader == true && e.Column > 11)
                {

                    string Query = "SELECT OPER_DESC FROM MWIPOPRDEF WHERE OPER = '" + spdData.ActiveSheet.Columns[e.Column].Label + "' AND ROWNUM=1";
                    DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", Query);

                    ToolTip desc = new ToolTip();
                    desc.Show(dt.Rows[0][0].ToString(), spdData, e.X + 10, e.Y, 1000);
                }
            }
        }

        private void rdoDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSummary.Checked == true)
            {
                cdvOperGroup.FromText = "";
                cdvOperGroup.ToText = "";
                cdvOperGroup.Visible = true;

                cdvOper.Visible = false;
            }
            else
            {
                cdvOper.Text = "";
                cdvOper.Visible = true;

                cdvOperGroup.Visible = false;
            }
        }

     }
}
