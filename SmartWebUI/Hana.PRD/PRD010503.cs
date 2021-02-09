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
    public partial class PRD010503 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD010503<br/>
        /// 클래스요약: 설비 Arrange 설비 기준<br/>
        /// 작  성  자: 미라콤 양형석<br/>
        /// 최초작성일: 2008-12-10<br/>
        /// 상세  설명: 설비 Arrange 설비 기준.<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: 하나마이크론 김준용<br />
        /// Excel Export 저장 기능 변경<br />
        /// 2010-07-13 김민우 : 설비 대수를 나타낼 때 한 설비가 여러공정에서 사용가능하면 사용가능한 모든 공정에 설비 대수가 중복으로 표기되어
        //                      설비 대수의 왜곡이 발생 따라서 쿼리를 수정함. MRASRESDEF 테이블의 RES_STS_9 컬럼에 현재 설비가 설정된 공정 기준으로
        //                      설비 대수를 카운트함
        /// 2010-07-28-임종우 : A1300 공정 표시 되도록 추가함(임태성 요청), 원부자재 공정 제거함.
        /// 2011-04-07-김민우 : 가동설비 RUN,WAIT구분하여 표기
        /// 2011-11-29-임종우 : 설비 상세 리스트 팝업 창 추가 (황선미 요청)
        /// 2012-02-01 김민우 : MRASREDDEF 테이블의 RES_STS_9 에서 RSS_STS_8 로 변경
        /// 2012-10-04-임종우 : 팝업 창 순서 변경시 오류 부분 수정 (김권수 요청)
        /// 2013-12-24-임종우 : 설비 효율 통일화 -> DA : 70%, WB : 75% (임태성 요청)
        ///                     과거 일자 조회 기능 추가 (김권수 요청)
        /// 2014-10-28-임종우 : C200 코드의 설비 제외 (임태성K 요청)
        /// 2014-11-06-임종우 : B199 코드의 설비 제외 (임태성K 요청)
        /// 2015-07-06-임종우 : 설비 대수 집계 시 DISPATCH 기준 정보가 'Y" 인 설비만 집계 (임태성K 요청)
        /// 2015-09-15-임종우 : C200, B199 설비 제외시 해당코드로 'Down' 된 설비만 제외 (김보람 요청)
        /// 2016-07-12-임종우 : CAPA 효율 GlobalVariable 로 선언하여 변경함.
        /// 2017-04-25-임종우 : FMB 설비 데이터 추가 (최인남상무 요청)
        /// 2017-04-28-임종우 : 팝업창에 Down Code 추가, 과거데이터 Summary 테이블로 변경 (최인남상무 요청)
        /// </summary>
        public PRD010503()
        {
            InitializeComponent();
            
            SortInit();
            GridColumnInit();

            cdvDate.Value = DateTime.Today;
            cboTimeBase.SelectedIndex = 0;
            //cboTimeBase.Text = DateTime.Now.ToString("HH") + "시";            
            cdvStep.Text = "";
            int a = cdvStep.ValueColumns.Count;
        }
        #region " Constant Definition "

        #endregion

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

            if (cdvStep.Text.TrimEnd() == "")
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
            //spdData.RPT_ColumnInit();
            try
            {
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Model", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 1, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Process Flow", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Layer classification", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PKG Type", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("RDL Plating", 0, 6, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Sub. Material", 0, 8, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Size", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Thickness", 0, 10, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Flat Type", 0, 11, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Wafer Orientation", 0, 12, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Step", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Product", 0, 14, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 15, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("대수", 0, 16, Visibles.True, Frozen.True, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("CAPA", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("RUN", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("WAIT", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                    //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                }
                else
                {
                    spdData.RPT_ColumnInit();
                    spdData.RPT_AddBasicColumn("Block", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Model", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 2, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Package", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type1", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Type2", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 7, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Density", 0, 8, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Generation", 0, 9, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Pin Type", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Step", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Product", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cust Device", 0, 13, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

                    spdData.RPT_AddBasicColumn("MES", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("대수", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("CAPA", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("RUN", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("WAIT", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 14, 4);

                    spdData.RPT_AddBasicColumn("FMB", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("대수", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("CAPA", 1, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("RUN", 1, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("WAIT", 1, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 18, 4);

                    for (int i = 0; i < 14; i++)
                    {
                        spdData.RPT_MerageHeaderRowSpan(0, i, 2);                        
                    }

                    spdData.RPT_ColumnConfigFromTable_New(btnSort);
                    //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                }
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "NVL(RES.RES_MODEL,'-') AS Model", "RES.RES_MODEL", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') AS Customer", "MAT.MAT_GRP_1", true);                
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Bumping Type", "MAT.MAT_GRP_2 AS BUMPING_TYPE", "MAT.MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Process Flow", "MAT.MAT_GRP_3 AS PROCESS_FLOW", "MAT.MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4 AS LAYER", "MAT.MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG Type", "MAT.MAT_GRP_5 AS PKG_TYPE", "MAT.MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL Plating", "MAT.MAT_GRP_6 AS RDL_PLATING", "MAT.MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Final Bump", "MAT.MAT_GRP_7 AS FINAL_BUMP", "MAT.MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Sub. Material", "MAT.MAT_GRP_8 AS SUB_MATERIAL", "MAT.MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Size", "MAT.MAT_CMF_14 AS WF_SIZE", "MAT.MAT_CMF_14", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Thickness", "MAT.MAT_CMF_2 AS THICKNESS", "MAT.MAT_CMF_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Flat Type", "MAT.MAT_CMF_3 AS FLAT_TYPE", "MAT.MAT_CMF_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Wafer Orientation", "MAT.MAT_CMF_4 AS WAFER_ORIENTATION", "MAT.MAT_CMF_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "RES.OPER AS Step", "RES.OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "RES.RES_STS_2 AS Product", "RES.RES_STS_2", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7 AS Cust_Device", "MAT.MAT_CMF_7", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).Clear();
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Block", "NVL(RES.SUB_AREA_ID,'-') AS Block", "RES.SUB_AREA_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Model", "NVL(RES.RES_MODEL,'-') AS Model", "RES.RES_MODEL", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY = MAT.FACTORY AND TABLE_NAME = 'H_CUSTOMER' AND ROWNUM=1 AND KEY_1 = MAT.MAT_GRP_1), '-') AS Customer", "MAT.MAT_GRP_1", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT.MAT_GRP_2 AS Family", "MAT_GRP_2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT.MAT_GRP_3 AS Package", "MAT_GRP_3", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT.MAT_GRP_4 AS Type1", "MAT_GRP_4", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT.MAT_GRP_5 AS Type2", "MAT_GRP_5", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT.MAT_GRP_6 AS LD_Count", "MAT_GRP_6", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT.MAT_GRP_7 AS Density", "MAT_GRP_7", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT.MAT_GRP_8 AS Generation", "MAT_GRP_8", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Pin Type", "MAT.MAT_CMF_10 AS Pin_Type", "MAT.MAT_CMF_10", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Step", "RES.OPER AS Step", "RES.OPER", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "RES.MAT_ID AS Product", "RES.MAT_ID", true);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Cust Device", "MAT.MAT_CMF_7 AS Cust_Device", "MAT.MAT_CMF_7", false);
            }
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;            

            //string strDate = cdvDate.Value.ToString("yyyyMMdd") + cboTimeBase.Text.Replace("시", "");
            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            #region 1. HMKB1
            if (cdvFactory.Text == "HMKB1")
            {
                // 2010-07-13 김민우 : 설비 대수를 나타낼 때 한 설비가 여러공정에서 사용가능하면 사용가능한 모든 공정에 설비 대수가 중복으로 표기되어
                //                     설비 대수의 왜곡이 발생 따라서 쿼리를 수정함. MRASRESDEF 테이블의 RES_STS_9 컬럼에 현재 설비가 설정된 공정 기준으로
                //                     설비 대수를 카운트함
                strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);
                strSqlString.Append("     , SUM(RES.RES_CNT),SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * PERCENT * RES.RES_CNT, 0))) AS CAPA" + "\n");
                // 2011-04-07-김민우 가동설비 RUN,WAIT 추가
                strSqlString.Append("     , NVL(SUM(RES.RUN_COUNT),0) AS RUN" + "\n");
                strSqlString.Append("     , NVL(SUM(RES.WAIT_COUNT),0) AS WAIT" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT FACTORY, RES_STS_2, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, COUNT(RES_ID) AS RES_CNT " + "\n");
                // 2011-04-07-김민우 가동설비 RUN,WAIT(MRASRESDEF 테이블의 RES_PRI_STS 컬럼 사용)
                strSqlString.Append("             , COUNT(DECODE(RES_PRI_STS,'PROC',RES_ID)) AS RUN_COUNT " + "\n");
                strSqlString.Append("             , COUNT(DECODE(RES_PRI_STS,'PROC','',RES_ID)) AS WAIT_COUNT " + "\n");
                strSqlString.Append("             , CASE WHEN RES_STS_8 LIKE 'A06%' THEN " + GlobalVariable.gdPer_wb + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 LIKE 'A04%' THEN " + GlobalVariable.gdPer_da + "\n");
                strSqlString.Append("                    WHEN RES_STS_8 = 'A0333' THEN " + GlobalVariable.gdPer_da + "\n");
                strSqlString.Append("                    ELSE " + GlobalVariable.gdPer_etc + "\n");
                strSqlString.Append("               END PERCENT " + "\n");

                // 2013-12-24-임종우 : 과거 데이터 조회 가능하도록 수정 (김권수 요청)
                //if (DateTime.Now.ToString("yyyyMMddHH").Equals(strDate)) // 현재 시간
                //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.Text == "현재")   
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.SelectedIndex == 0)          
                {
                    strSqlString.Append("          FROM MRASRESDEF " + "\n");
                    strSqlString.Append("         WHERE 1 = 1  " + "\n");
                }
                else
                {
                    strSqlString.Append("          FROM MRASRESDEF_BOH " + "\n");
                    strSqlString.Append("         WHERE 1 = 1  " + "\n");

                    //if (cboTimeBase.Text == "현재")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append("           AND CUTOFF_DT = '" + strDate + DateTime.Now.ToString("HH") + "'" + "\n");
                    }
                    else
                    {
                        //strSqlString.Append("           AND CUTOFF_DT = '" + strDate + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                        strSqlString.Append("           AND CUTOFF_DT = '" + strDate + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                    }
                }

                strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
                strSqlString.Append("           AND RES_CMF_7 = 'Y' " + "\n");
                strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
                strSqlString.Append("           AND RES_TYPE='EQUIPMENT' " + "\n");    // 2011-04-07-김민우 Dummy 설비 제외 조건 추가
                strSqlString.Append("           AND RES_STS_8 " + cdvStep.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                strSqlString.Append("         GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7 " + "\n");
                strSqlString.Append("       ) RES " + "\n");
                strSqlString.Append("     , MWIPMATDEF MAT" + "\n");
                strSqlString.Append("     , CRASUPHDEF UPH" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND MAT.DELETE_FLAG=' '" + "\n");
                strSqlString.Append("   AND RES.FACTORY=MAT.FACTORY" + "\n");
                strSqlString.Append("   AND RES.RES_STS_2 = MAT.MAT_ID" + "\n");
                strSqlString.Append("   AND RES.FACTORY=UPH.FACTORY(+)" + "\n");
                strSqlString.Append("   AND RES.OPER=UPH.OPER(+)" + "\n");
                strSqlString.Append("   AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
                strSqlString.Append("   AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
                strSqlString.Append("   AND RES.RES_STS_2 = UPH.MAT_ID(+)" + "\n");

                #region 조회조건(FACTORY, MODEL, PRODUCT, 과거날짜)

                strSqlString.Append("   AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                    strSqlString.Append("   AND RES.OPER NOT IN ('00001','00002') " + "\n");

                if (cdvModel.Text != "ALL" && cdvModel.Text.Trim() != "")
                    strSqlString.Append("   AND RES.RES_MODEL " + cdvModel.SelectedValueToQueryString + "\n");

                if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                    strSqlString.AppendFormat("   AND RES.RES_STS_2 LIKE '{0}'" + "\n", txtSearchProduct.Text);

                #endregion



                #region 상세 조회에 따른 SQL문 생성
  
                if (udcBUMPCondition1.Text != "ALL" && udcBUMPCondition1.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcBUMPCondition1.SelectedValueToQueryString);

                if (udcBUMPCondition2.Text != "ALL" && udcBUMPCondition2.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcBUMPCondition2.SelectedValueToQueryString);

                if (udcBUMPCondition3.Text != "ALL" && udcBUMPCondition3.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_3 {0} " + "\n", udcBUMPCondition3.SelectedValueToQueryString);

                if (udcBUMPCondition4.Text != "ALL" && udcBUMPCondition4.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_4 {0} " + "\n", udcBUMPCondition4.SelectedValueToQueryString);

                if (udcBUMPCondition5.Text != "ALL" && udcBUMPCondition5.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_5 {0} " + "\n", udcBUMPCondition5.SelectedValueToQueryString);

                if (udcBUMPCondition6.Text != "ALL" && udcBUMPCondition6.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_6 {0} " + "\n", udcBUMPCondition6.SelectedValueToQueryString);

                if (udcBUMPCondition7.Text != "ALL" && udcBUMPCondition7.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_7 {0} " + "\n", udcBUMPCondition7.SelectedValueToQueryString);

                if (udcBUMPCondition8.Text != "ALL" && udcBUMPCondition8.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_GRP_8 {0} " + "\n", udcBUMPCondition8.SelectedValueToQueryString);

                if (udcBUMPCondition9.Text != "ALL" && udcBUMPCondition9.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_14 {0} " + "\n", udcBUMPCondition9.SelectedValueToQueryString);

                if (udcBUMPCondition10.Text != "ALL" && udcBUMPCondition10.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_2 {0} " + "\n", udcBUMPCondition10.SelectedValueToQueryString);

                if (udcBUMPCondition11.Text != "ALL" && udcBUMPCondition11.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_3 {0} " + "\n", udcBUMPCondition11.SelectedValueToQueryString);

                if (udcBUMPCondition12.Text != "ALL" && udcBUMPCondition12.Text != "")
                    strSqlString.AppendFormat("   AND MAT.MAT_CMF_4 {0} " + "\n", udcBUMPCondition12.SelectedValueToQueryString);
                
                #endregion

                strSqlString.AppendFormat(" GROUP BY MAT.FACTORY, {0}  " + "\n", QueryCond2);
                strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);
            }
            #endregion

            #region 2.그 외
            else
            {
                #region 2-1. 현재
                //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.Text == "현재")
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.SelectedIndex == 0)
                {
                    // 2010-07-13 김민우 : 설비 대수를 나타낼 때 한 설비가 여러공정에서 사용가능하면 사용가능한 모든 공정에 설비 대수가 중복으로 표기되어
                    //                     설비 대수의 왜곡이 발생 따라서 쿼리를 수정함. MRASRESDEF 테이블의 RES_STS_9 컬럼에 현재 설비가 설정된 공정 기준으로
                    //                     설비 대수를 카운트함
                    strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);
                    strSqlString.Append("     , SUM(RES.RES_CNT)" + "\n");
                    strSqlString.Append("     , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * PERCENT * RES.RES_CNT, 0))) AS CAPA" + "\n");
                    strSqlString.Append("     , NVL(SUM(RES.RUN_COUNT),0) AS RUN" + "\n");
                    strSqlString.Append("     , NVL(SUM(RES.WAIT_COUNT),0) AS WAIT" + "\n");
                    strSqlString.Append("     , SUM(RES.FMB_CNT) AS FMB_CNT" + "\n");
                    strSqlString.Append("     , SUM(TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * PERCENT * RES.FMB_CNT, 0))) AS FMB_CAPA" + "\n");
                    strSqlString.Append("     , SUM(RES.FMB_RUN) AS FMB_RUN" + "\n");
                    strSqlString.Append("     , SUM(RES.FMB_WAIT) AS FMB_WAIT" + "\n");
                    strSqlString.Append("  FROM (" + "\n");
                    strSqlString.Append("        SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, SUB_AREA_ID, COUNT(RES_ID) AS RES_CNT " + "\n");
                    // 2011-04-07-김민우 가동설비 RUN,WAIT(MRASRESDEF 테이블의 RES_PRI_STS 컬럼 사용)
                    strSqlString.Append("             , COUNT(DECODE(RES_PRI_STS,'PROC',RES_ID)) AS RUN_COUNT " + "\n");
                    strSqlString.Append("             , COUNT(DECODE(RES_PRI_STS,'PROC','',RES_ID)) AS WAIT_COUNT " + "\n");
                    strSqlString.Append("             , CASE WHEN RES_STS_8 LIKE 'A06%' THEN " + GlobalVariable.gdPer_wb + "\n");
                    strSqlString.Append("                    WHEN RES_STS_8 LIKE 'A04%' THEN " + GlobalVariable.gdPer_da + "\n");
                    strSqlString.Append("                    WHEN RES_STS_8 = 'A0333' THEN " + GlobalVariable.gdPer_da + "\n");
                    strSqlString.Append("                    ELSE " + GlobalVariable.gdPer_etc + "\n");
                    strSqlString.Append("               END PERCENT " + "\n");
                    strSqlString.Append("             , 0 AS FMB_CNT " + "\n");
                    strSqlString.Append("             , 0 AS FMB_RUN " + "\n");
                    strSqlString.Append("             , 0 AS FMB_WAIT " + "\n");                    
                    strSqlString.Append("          FROM MRASRESDEF " + "\n");
                    strSqlString.Append("         WHERE 1 = 1  " + "\n");
                    strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    strSqlString.Append("           AND RES_CMF_9 = 'Y' " + "\n");
                    strSqlString.Append("           AND RES_CMF_7 = 'Y' " + "\n");
                    strSqlString.Append("           AND DELETE_FLAG = ' ' " + "\n");
                    strSqlString.Append("           AND RES_TYPE='EQUIPMENT' " + "\n");    // 2011-04-07-김민우 Dummy 설비 제외 조건 추가
                    strSqlString.Append("           AND RES_STS_8 " + cdvStep.SelectedValueToQueryString + "\n");
                    strSqlString.Append("           AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                    strSqlString.Append("         GROUP BY FACTORY,RES_STS_2,RES_STS_8,RES_GRP_6,RES_GRP_7, SUB_AREA_ID " + "\n");
                    strSqlString.Append("         UNION ALL " + "\n");
                    strSqlString.Append("        SELECT FACTORY, MAT_ID, OPER, RES_MODEL, UPEH_GRP, SUB_AREA_ID" + "\n");
                    strSqlString.Append("             , 0 AS RES_CNT" + "\n");
                    strSqlString.Append("             , 0 AS RUN_COUNT " + "\n");
                    strSqlString.Append("             , 0 AS WAIT_COUNT " + "\n");
                    strSqlString.Append("             , CASE WHEN OPER LIKE 'A06%' THEN " + GlobalVariable.gdPer_wb + "\n");
                    strSqlString.Append("                    WHEN OPER LIKE 'A04%' THEN " + GlobalVariable.gdPer_da + "\n");
                    strSqlString.Append("                    WHEN OPER = 'A0333' THEN " + GlobalVariable.gdPer_da + "\n");
                    strSqlString.Append("                    ELSE " + GlobalVariable.gdPer_etc + "\n");
                    strSqlString.Append("               END PERCENT " + "\n");
                    strSqlString.Append("             , COUNT(RES_ID) AS FMB_CNT" + "\n");
                    strSqlString.Append("             , COUNT(DECODE(STATE,'1',RES_ID)) AS FMB_RUN " + "\n");
                    strSqlString.Append("             , COUNT(DECODE(STATE,'1','',RES_ID)) AS FMB_WAIT" + "\n");
                    strSqlString.Append("          FROM (" + "\n");
                    strSqlString.Append("                SELECT FACTORY, MAT_ID, OPER, RES_MODEL, UPEH_GRP, SUB_AREA_ID, RES_ID" + "\n");
                    strSqlString.Append("                     , DECODE(BPCS_LIGHT_TOWER_STATE, '0', DECODE(STATUS, '7', '4', '8') , DECODE(STATUS, '99', '99', '9', '9', '91', '91', '92', '92', '93', '93', '2', '5', '3', '6', '55', '2', '94', '1', '7', '4', '991', '991', '992', '992', '993', '993', '994', '994', DECODE(BPCS_LIGHT_TOWER_STATE, '1024', '1', '16384', '3', '32768', '3', '4096', '2', STATUS))) AS STATE" + "\n");
                    strSqlString.Append("                  FROM (" + "\n");
                    strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER, RES_MODEL, UPEH_GRP, SUB_AREA_ID" + "\n");
                    strSqlString.Append("                             , STS.RES_ID" + "\n");
                    strSqlString.Append("                             , DECODE(CONTROL_STATE, 'Remote', DECODE(LIGHT_TOWER, '', '0', '0', '4096', LIGHT_TOWER), 'Disconnect', '0', 'Offline', '0', 'Open', '0', 'Close', '0', 'Connect', '0', 'Local', '0', '0') AS BPCS_LIGHT_TOWER_STATE" + "\n");
                    strSqlString.Append("                             , STATUS" + "\n");
                    strSqlString.Append("                          FROM T_BPCS_EQUIPMENT_STATE@RPTTOFA LMP" + "\n");
                    strSqlString.Append("                             , (" + "\n");
                    strSqlString.Append("                                SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, SUB_AREA_ID, RES_ID" + "\n");
                    strSqlString.Append("                                     , CASE WHEN RES_STS_3 IN ('130','132') AND RES_STS_4 IN ('6005','3155') THEN 991" + "\n");
                    strSqlString.Append("                                            WHEN RES_STS_3 > '128' AND RES_STS_4 IN ('6058', '385') THEN 992" + "\n");
                    strSqlString.Append("                                            ELSE DECODE(STATUS , 1, " + "\n");
                    strSqlString.Append("                                                       DECODE(RES_UP_DOWN_FLAG, 'D', 2, " + "\n");
                    strSqlString.Append("                                                             DECODE(LAST_EVENT_ID, 'MAINT_END', " + "\n");
                    strSqlString.Append("                                                                   DECODE( SIGN((SELECT COUNT(*) AS LOT_CNT" + "\n");
                    strSqlString.Append("                                                                                   FROM MWIPLOTSTS" + "\n");
                    strSqlString.Append("                                                                                  WHERE 1=1" + "\n");
                    strSqlString.Append("                                                                                    AND START_RES_ID = A.RES_ID" + "\n");
                    strSqlString.Append("                                                                                    AND LOT_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                                                                                    AND HOLD_FLAG = ' '" + "\n");
                    strSqlString.Append("                                                                                    AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                                                                                    AND HOLD_FLAG= ' ')), 0, 1, 94)" + "\n");
                    strSqlString.Append("                                                                     , 1)" + "\n");
                    strSqlString.Append("                                                                ), 4, DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(SUBSTR(RES_STS_1, 0, 1), 'E', 7, 2), 4) , 3, DECODE(LAST_EVENT_ID, 'MAINT_START', DECODE(RES_STS_1, 'D211', 91, 'D212', 91, 'D291', 91, 'D213', 93, 'D311', 93, 'D391', 93, 'D314', 93, 'D312', 93, 'D313', 93, 'D214', 93, 3), 3) , 6, DECODE(RES_STS_1, 'D211', 9, 'D212', 9, 'D291', 9, 'D213', 92, 'D311', 92, 'D391', 92, 'D314', 92, 'D312', 92, 'D313', 92, 'D214', 92, 'D315', 993, 'D215', 993, 6) , 5, DECODE(LAST_EVENT_ID, 'RUN_DOWN', DECODE(RES_STS_1, 'F701', 99, 'B500', 99, 'B110', 994, DECODE(SUBSTR(RES_STS_1, 0, 1), 'H', 4, 55)), 5) , 8, DECODE(LAST_EVENT_ID, 'ETC_DOWN', DECODE(SUBSTR(RES_STS_1, 0, 1), 'H', 4, 8), 8) , STATUS)" + "\n");
                    strSqlString.Append("                                                                       END AS STATUS " + "\n");
                    strSqlString.Append("                                     , RES_GRP_6" + "\n");
                    strSqlString.Append("                                  FROM MESMGR.FMB_CRASRESSTS@RPTTOMES A" + "\n");
                    strSqlString.Append("                                 WHERE 1=1" + "\n");
                    strSqlString.Append("                                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    strSqlString.Append("                                   AND AREA_ID = 'WB'" + "\n");
                    strSqlString.Append("                                   AND SUB_AREA_ID <> 'PLASMA' " + "\n");
                    strSqlString.Append("                                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                    strSqlString.Append("                                   AND RES_STS_8 " + cdvStep.SelectedValueToQueryString + "\n");
                    strSqlString.Append("                               ) STS" + "\n");
                    strSqlString.Append("                         WHERE 1=1" + "\n");
                    strSqlString.Append("                           AND LMP.EQUIP_ID(+) = STS.RES_ID" + "\n");
                    strSqlString.Append("                           AND STS.RES_GRP_6 NOT IN ('UTC-400') " + "\n");
                    strSqlString.Append("                       )           " + "\n");
                    strSqlString.Append("               ) RES  " + "\n");
                    strSqlString.Append("         GROUP BY FACTORY, MAT_ID, OPER, RES_MODEL, UPEH_GRP, SUB_AREA_ID " + "\n");
                    strSqlString.Append("         UNION ALL" + "\n");
                    strSqlString.Append("        SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, RES_GRP_7 AS UPEH_GRP, SUB_AREA_ID" + "\n");
                    strSqlString.Append("             , 0 AS RES_CNT" + "\n");
                    strSqlString.Append("             , 0 AS RUN_COUNT " + "\n");
                    strSqlString.Append("             , 0 AS WAIT_COUNT " + "\n");
                    strSqlString.Append("             , CASE WHEN RES_STS_8 LIKE 'A06%' THEN " + GlobalVariable.gdPer_wb + "\n");
                    strSqlString.Append("                    WHEN RES_STS_8 LIKE 'A04%' THEN " + GlobalVariable.gdPer_da + "\n");
                    strSqlString.Append("                    WHEN RES_STS_8 = 'A0333' THEN " + GlobalVariable.gdPer_da + "\n");
                    strSqlString.Append("                    ELSE " + GlobalVariable.gdPer_etc + "\n");
                    strSqlString.Append("               END PERCENT " + "\n");
                    strSqlString.Append("             , COUNT(RES_ID) AS FMB_CNT" + "\n");
                    strSqlString.Append("             , COUNT(DECODE(STATUS, 1, RES_ID)) AS FMB_RUN" + "\n");
                    strSqlString.Append("             , COUNT(DECODE(STATUS, 1, '', RES_ID)) AS FMB_WAIT     " + "\n");
                    strSqlString.Append("          FROM MESMGR.FMB_CRASRESSTS@RPTTOMES" + "\n");
                    strSqlString.Append("         WHERE 1=1" + "\n");
                    strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    strSqlString.Append("           AND RES_CMF_20 <= TO_CHAR(SYSDATE, 'YYYYMMDD')" + "\n");
                    strSqlString.Append("           AND AREA_ID <> 'WB'" + "\n");
                    strSqlString.Append("           AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                    strSqlString.Append("           AND RES_STS_8 " + cdvStep.SelectedValueToQueryString + "\n");
                    strSqlString.Append("         GROUP BY FACTORY, RES_STS_2, RES_STS_8, RES_GRP_6, RES_GRP_7, SUB_AREA_ID" + "\n");
                    strSqlString.Append("       ) RES " + "\n");
                    strSqlString.Append("     , MWIPMATDEF MAT" + "\n");
                    strSqlString.Append("     , CRASUPHDEF UPH" + "\n");
                    strSqlString.Append(" WHERE 1=1" + "\n");
                    strSqlString.Append("   AND MAT.DELETE_FLAG=' '" + "\n");
                    strSqlString.Append("   AND RES.FACTORY=MAT.FACTORY" + "\n");
                    strSqlString.Append("   AND RES.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("   AND RES.FACTORY=UPH.FACTORY(+)" + "\n");
                    strSqlString.Append("   AND RES.OPER=UPH.OPER(+)" + "\n");
                    strSqlString.Append("   AND RES.RES_MODEL = UPH.RES_MODEL(+) " + "\n");
                    strSqlString.Append("   AND RES.UPEH_GRP = UPH.UPEH_GRP(+) " + "\n");
                    strSqlString.Append("   AND RES.MAT_ID = UPH.MAT_ID(+)" + "\n");

                    #region 조회조건(FACTORY, MODEL, PRODUCT, 과거날짜)

                    strSqlString.Append("   AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                    if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                        strSqlString.Append("   AND RES.OPER NOT IN ('00001','00002') " + "\n");

                    if (cdvModel.Text != "ALL" && cdvModel.Text.Trim() != "")
                        strSqlString.Append("   AND RES.RES_MODEL " + cdvModel.SelectedValueToQueryString + "\n");

                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND RES.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                    #endregion



                    #region 상세 조회에 따른 SQL문 생성

                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

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

                    strSqlString.AppendFormat(" GROUP BY MAT.FACTORY, {0}  " + "\n", QueryCond2);
                    strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);
                }
                #endregion

                #region 2-2.과거
                else
                {
                    strSqlString.AppendFormat("SELECT {0}  " + "\n", QueryCond1);
                    strSqlString.Append("     , SUM(RES.MES_CNT) AS MES_CNT" + "\n");
                    strSqlString.Append("     , SUM(TRUNC(NVL(RES.UPEH * 24 * PERCENT * RES.MES_CNT, 0))) AS MES_CAPA" + "\n");
                    strSqlString.Append("     , SUM(RES.MES_RUN) AS MES_RUN" + "\n");
                    strSqlString.Append("     , SUM(RES.MES_WAIT) AS MES_WAIT" + "\n");
                    strSqlString.Append("     , SUM(RES.FMB_CNT) AS FMB_CNT" + "\n");
                    strSqlString.Append("     , SUM(TRUNC(NVL(RES.UPEH * 24 * PERCENT * RES.FMB_CNT, 0))) AS FMB_CAPA" + "\n");
                    strSqlString.Append("     , SUM(RES.FMB_RUN) AS FMB_RUN" + "\n");
                    strSqlString.Append("     , SUM(RES.FMB_WAIT) AS FMB_WAIT" + "\n");
                    strSqlString.Append("  FROM RSUMARRDAT RES" + "\n");
                    strSqlString.Append("     , MWIPMATDEF MAT " + "\n");
                    strSqlString.Append(" WHERE 1=1 " + "\n");
                    strSqlString.Append("   AND RES.FACTORY = MAT.FACTORY " + "\n");
                    strSqlString.Append("   AND RES.MAT_ID = MAT.MAT_ID " + "\n");
                    strSqlString.Append("   AND RES.GUBUN = 'SUM' " + "\n");
                    strSqlString.Append("   AND RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

                    //if (cboTimeBase.Text == "현재")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append("   AND CUTOFF_DT = '" + strDate + DateTime.Now.ToString("HH") + "'" + "\n");
                    }
                    else
                    {
                        //strSqlString.Append("   AND CUTOFF_DT = '" + strDate + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                        strSqlString.Append("   AND CUTOFF_DT = '" + strDate + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                    }
                    
                                        
                    strSqlString.Append("           AND RES.OPER " + cdvStep.SelectedValueToQueryString + "\n");
                    
                    if (cdvFactory.Text == GlobalVariable.gsAssyDefaultFactory)
                        strSqlString.Append("   AND RES.OPER NOT IN ('00001','00002') " + "\n");

                    if (cdvModel.Text != "ALL" && cdvModel.Text.Trim() != "")
                        strSqlString.Append("   AND RES.RES_MODEL " + cdvModel.SelectedValueToQueryString + "\n");

                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND RES.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    
                    #region 상세 조회에 따른 SQL문 생성

                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                        strSqlString.AppendFormat("   AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

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

                    strSqlString.AppendFormat(" GROUP BY MAT.FACTORY, {0}  " + "\n", QueryCond2);
                    strSqlString.AppendFormat(" ORDER BY {0} " + "\n", QueryCond2);
                }
                #endregion
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
 
            return strSqlString.ToString();
        }

        #region MakeSqlCustomer
        //고객사 명으로 고객사 코드 가져오기 위해..
        private string MakeSqlCustomer(string customerName)
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.AppendFormat("SELECT KEY_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND DATA_1 = '" + customerName + "' AND ROWNUM=1" + "\n");

            return strSqlString.ToString();

        }
        #endregion

        #region MakeSqlDetail
        //상세 팝업창 쿼리
        private string MakeSqlDetail(string[] dataArry, int type)
        {
            StringBuilder strSqlString = new StringBuilder();
            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            #region 1. MES 기준 POPUP
            if (type == 1)
            {
                strSqlString.AppendFormat("SELECT RES_GRP_6 AS RES_MODEL" + "\n");
                strSqlString.AppendFormat("     , RES_ID" + "\n");
                strSqlString.AppendFormat("     , RES_UP_DOWN_FLAG AS STATUS" + "\n");
                strSqlString.AppendFormat("     , DECODE(RES_UP_DOWN_FLAG, 'D', C.DATA_1, '') AS DOWN_DESC" + "\n");
                strSqlString.AppendFormat("     , DECODE(RES_UP_DOWN_FLAG, 'D', RES_STS_1, '') AS DOWN_CODE" + "\n");

                //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.Text == "현재")
                if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.SelectedIndex == 0)
                {
                    strSqlString.AppendFormat("  FROM MRASRESDEF A" + "\n");
                    strSqlString.AppendFormat("     , MWIPMATDEF B" + "\n");
                    strSqlString.AppendFormat("     , MGCMTBLDAT C" + "\n");
                    strSqlString.AppendFormat(" WHERE 1=1" + "\n");
                }
                else
                {
                    strSqlString.AppendFormat("  FROM MRASRESDEF_BOH A" + "\n");
                    strSqlString.AppendFormat("     , MWIPMATDEF B" + "\n");
                    strSqlString.AppendFormat("     , MGCMTBLDAT C" + "\n");
                    strSqlString.AppendFormat(" WHERE 1 = 1  " + "\n");                    

                    //if (cboTimeBase.Text == "현재")
                    if (cboTimeBase.SelectedIndex == 0)
                    {
                        strSqlString.Append("   AND CUTOFF_DT = '" + strDate + DateTime.Now.ToString("HH") + "'" + "\n");
                    }
                    else
                    {
                        //strSqlString.Append("   AND CUTOFF_DT = '" + strDate + cboTimeBase.Text.Replace("시", "") + "'" + "\n");
                        strSqlString.Append("   AND CUTOFF_DT = '" + strDate + cboTimeBase.Text.Substring(0, 2) + "'" + "\n");
                    }

                }

                strSqlString.AppendFormat("   AND A.FACTORY = B.FACTORY " + "\n");
                strSqlString.AppendFormat("   AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.AppendFormat("   AND A.RES_STS_2 = B.MAT_ID" + "\n");
                strSqlString.AppendFormat("   AND C.KEY_1 = SUBSTR(A.RES_STS_1, 1,1) " + "\n");
                strSqlString.AppendFormat("   AND A.FACTORY = '" + cdvFactory.Text + "'" + "\n");
                strSqlString.AppendFormat("   AND A.RES_CMF_9 = 'Y' " + "\n");
                strSqlString.AppendFormat("   AND A.RES_CMF_7 = 'Y' " + "\n");
                strSqlString.AppendFormat("   AND A.DELETE_FLAG = ' ' " + "\n");
                strSqlString.AppendFormat("   AND A.RES_TYPE = 'EQUIPMENT' " + "\n");
                strSqlString.AppendFormat("   AND C.TABLE_NAME = 'STOP_CODE' " + "\n");
                //strSqlString.AppendFormat("   AND A.RES_STS_1 NOT IN ('C200', 'B199') " + "\n");
                strSqlString.AppendFormat("   AND (A.RES_STS_1 NOT IN ('C200', 'B199')  OR A.RES_UP_DOWN_FLAG = 'U') " + "\n");

                #region 상세 조회에 따른 SQL문 생성
                if (cdvFactory.Text.Trim() == "HMKB1")
                {
                    if (dataArry[0] != " " && dataArry[0] != null)
                        strSqlString.AppendFormat("   AND A.RES_GRP_6 = '" + dataArry[0] + "'" + "\n");

                    if (dataArry[1] != " " && dataArry[1] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_1 = '" + dataArry[1] + "'" + "\n");

                    if (dataArry[2] != " " && dataArry[2] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_2 = '" + dataArry[2] + "'" + "\n");

                    if (dataArry[3] != " " && dataArry[3] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_3 = '" + dataArry[3] + "'" + "\n");

                    if (dataArry[4] != " " && dataArry[4] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_4 = '" + dataArry[4] + "'" + "\n");

                    if (dataArry[5] != " " && dataArry[5] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_5 = '" + dataArry[5] + "'" + "\n");

                    if (dataArry[6] != " " && dataArry[6] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_6 = '" + dataArry[6] + "'" + "\n");

                    if (dataArry[7] != " " && dataArry[7] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_7 = '" + dataArry[7] + "'" + "\n");

                    if (dataArry[8] != " " && dataArry[8] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_8 = '" + dataArry[8] + "'" + "\n");

                    if (dataArry[9] != " " && dataArry[9] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_14 = '" + dataArry[9] + "'" + "\n");

                    if (dataArry[10] != " " && dataArry[10] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_2 = '" + dataArry[10] + "'" + "\n");

                    if (dataArry[11] != " " && dataArry[11] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_3 = '" + dataArry[11] + "'" + "\n");

                    if (dataArry[12] != " " && dataArry[12] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_4 = '" + dataArry[12] + "'" + "\n");

                    if (dataArry[13] != " " && dataArry[13] != null)
                        strSqlString.AppendFormat("   AND A.RES_STS_8 = '" + dataArry[13] + "'" + "\n");

                    if (dataArry[14] != " " && dataArry[14] != null)
                        strSqlString.AppendFormat("   AND B.MAT_ID = '" + dataArry[14] + "'" + "\n");

                    if (dataArry[15] != " " && dataArry[15] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_7 = '" + dataArry[15] + "'" + "\n");
                }
                else
                {
                    if (dataArry[0] != " " && dataArry[0] != null)
                        strSqlString.AppendFormat("   AND A.SUB_AREA_ID = '" + dataArry[0] + "'" + "\n");

                    if (dataArry[1] != " " && dataArry[1] != null)
                        strSqlString.AppendFormat("   AND A.RES_GRP_6 = '" + dataArry[1] + "'" + "\n");

                    if (dataArry[2] != " " && dataArry[2] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_1 = '" + dataArry[2] + "'" + "\n");

                    if (dataArry[3] != " " && dataArry[3] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_2 = '" + dataArry[3] + "'" + "\n");

                    if (dataArry[4] != " " && dataArry[4] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_3 = '" + dataArry[4] + "'" + "\n");

                    if (dataArry[5] != " " && dataArry[5] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_4 = '" + dataArry[5] + "'" + "\n");

                    if (dataArry[6] != " " && dataArry[6] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_5 = '" + dataArry[6] + "'" + "\n");

                    if (dataArry[7] != " " && dataArry[7] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_6 = '" + dataArry[7] + "'" + "\n");

                    if (dataArry[8] != " " && dataArry[8] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_7 = '" + dataArry[8] + "'" + "\n");

                    if (dataArry[9] != " " && dataArry[9] != null)
                        strSqlString.AppendFormat("   AND B.MAT_GRP_8 = '" + dataArry[9] + "'" + "\n");

                    if (dataArry[10] != " " && dataArry[10] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_10 = '" + dataArry[10] + "'" + "\n");

                    if (dataArry[11] != " " && dataArry[11] != null)
                        strSqlString.AppendFormat("   AND A.RES_STS_8 = '" + dataArry[11] + "'" + "\n");

                    if (dataArry[12] != " " && dataArry[12] != null)
                        strSqlString.AppendFormat("   AND B.MAT_ID = '" + dataArry[12] + "'" + "\n");

                    if (dataArry[13] != " " && dataArry[13] != null)
                        strSqlString.AppendFormat("   AND B.MAT_CMF_7 = '" + dataArry[13] + "'" + "\n");
                }

                #endregion

                strSqlString.AppendFormat(" ORDER BY RES_GRP_6, RES_ID, RES_UP_DOWN_FLAG" + "\n");
            }
            #endregion

            #region 2. FMB 기준 POPUP
            else
            {
                strSqlString.Append("SELECT RES_MODEL" + "\n");
                strSqlString.Append("     , RES_ID" + "\n");
                strSqlString.Append("     , STATUS" + "\n");
                strSqlString.Append("     , DECODE(STATUS, 'D', C.DATA_1, '') AS DOWN_DESC" + "\n");
                strSqlString.Append("     , DECODE(STATUS, 'D', RES_STS_1, '') AS DOWN_CODE" + "\n");
                strSqlString.Append("  FROM (" + "\n");
                strSqlString.Append("        SELECT FACTORY, MAT_ID, OPER, RES_MODEL, SUB_AREA_ID, DECODE(STATUS, 1, 'U', 'D') AS STATUS, RES_ID, RES_STS_1" + "\n");
                strSqlString.Append("          FROM (" + "\n");
                strSqlString.Append("                SELECT FACTORY, MAT_ID, OPER, RES_MODEL, SUB_AREA_ID, RES_ID, RES_STS_1" + "\n");
                strSqlString.Append("                     , DECODE(BPCS_LIGHT_TOWER_STATE, '0', DECODE(STATUS, '7', '4', '8') , DECODE(STATUS, '99', '99', '9', '9', '91', '91', '92', '92', '93', '93', '2', '5', '3', '6', '55', '2', '94', '1', '7', '4', '991', '991', '992', '992', '993', '993', '994', '994', DECODE(BPCS_LIGHT_TOWER_STATE, '1024', '1', '16384', '3', '32768', '3', '4096', '2', STATUS))) AS STATUS" + "\n");
                strSqlString.Append("                  FROM (" + "\n");
                strSqlString.Append("                        SELECT FACTORY, MAT_ID, OPER, RES_MODEL, SUB_AREA_ID" + "\n");
                strSqlString.Append("                             , STS.RES_ID, RES_STS_1" + "\n");
                strSqlString.Append("                             , DECODE(CONTROL_STATE, 'Remote', DECODE(LIGHT_TOWER, '', '0', '0', '4096', LIGHT_TOWER), 'Disconnect', '0', 'Offline', '0', 'Open', '0', 'Close', '0', 'Connect', '0', 'Local', '0', '0') AS BPCS_LIGHT_TOWER_STATE" + "\n");
                strSqlString.Append("                             , STATUS" + "\n");
                strSqlString.Append("                          FROM T_BPCS_EQUIPMENT_STATE@RPTTOFA LMP" + "\n");
                strSqlString.Append("                             , (" + "\n");
                strSqlString.Append("                                SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, SUB_AREA_ID, RES_ID, RES_STS_1" + "\n");
                strSqlString.Append("                                     , CASE WHEN RES_STS_3 IN ('130','132') AND RES_STS_4 IN ('6005','3155') THEN 991" + "\n");
                strSqlString.Append("                                            WHEN RES_STS_3 > '128' AND RES_STS_4 IN ('6058', '385') THEN 992" + "\n");
                strSqlString.Append("                                            ELSE DECODE(STATUS , 1, " + "\n");
                strSqlString.Append("                                                       DECODE(RES_UP_DOWN_FLAG, 'D', 2, " + "\n");
                strSqlString.Append("                                                             DECODE(LAST_EVENT_ID, 'MAINT_END', " + "\n");
                strSqlString.Append("                                                                   DECODE( SIGN((SELECT COUNT(*) AS LOT_CNT" + "\n");
                strSqlString.Append("                                                                                   FROM MWIPLOTSTS" + "\n");
                strSqlString.Append("                                                                                  WHERE 1=1" + "\n");
                strSqlString.Append("                                                                                    AND START_RES_ID = A.RES_ID" + "\n");
                strSqlString.Append("                                                                                    AND LOT_DEL_FLAG = ' '" + "\n");
                strSqlString.Append("                                                                                    AND HOLD_FLAG = ' '" + "\n");
                strSqlString.Append("                                                                                    AND LOT_TYPE = 'W'" + "\n");
                strSqlString.Append("                                                                                    AND HOLD_FLAG= ' ')), 0, 1, 94)" + "\n");
                strSqlString.Append("                                                                     , 1)" + "\n");
                strSqlString.Append("                                                                ), 4, DECODE(RES_UP_DOWN_FLAG, 'D', DECODE(SUBSTR(RES_STS_1, 0, 1), 'E', 7, 2), 4) , 3, DECODE(LAST_EVENT_ID, 'MAINT_START', DECODE(RES_STS_1, 'D211', 91, 'D212', 91, 'D291', 91, 'D213', 93, 'D311', 93, 'D391', 93, 'D314', 93, 'D312', 93, 'D313', 93, 'D214', 93, 3), 3) , 6, DECODE(RES_STS_1, 'D211', 9, 'D212', 9, 'D291', 9, 'D213', 92, 'D311', 92, 'D391', 92, 'D314', 92, 'D312', 92, 'D313', 92, 'D214', 92, 'D315', 993, 'D215', 993, 6) , 5, DECODE(LAST_EVENT_ID, 'RUN_DOWN', DECODE(RES_STS_1, 'F701', 99, 'B500', 99, 'B110', 994, DECODE(SUBSTR(RES_STS_1, 0, 1), 'H', 4, 55)), 5) , 8, DECODE(LAST_EVENT_ID, 'ETC_DOWN', DECODE(SUBSTR(RES_STS_1, 0, 1), 'H', 4, 8), 8) , STATUS)" + "\n");
                strSqlString.Append("                                                                       END AS STATUS " + "\n");
                strSqlString.Append("                                     , RES_GRP_6" + "\n");
                strSqlString.Append("                                  FROM MESMGR.FMB_CRASRESSTS@RPTTOMES A" + "\n");
                strSqlString.Append("                                 WHERE 1=1" + "\n");
                strSqlString.Append("                                   AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("                                   AND AREA_ID = 'WB'" + "\n");
                strSqlString.Append("                                   AND SUB_AREA_ID <> 'PLASMA' " + "\n");
                strSqlString.Append("                                   AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U') " + "\n");
                strSqlString.Append("                               ) STS" + "\n");
                strSqlString.Append("                         WHERE 1=1" + "\n");
                strSqlString.Append("                           AND LMP.EQUIP_ID(+) = STS.RES_ID" + "\n");
                strSqlString.Append("                           AND STS.RES_GRP_6 NOT IN ('UTC-400') " + "\n");
                strSqlString.Append("                       )           " + "\n");
                strSqlString.Append("               )  " + "\n");
                strSqlString.Append("         UNION ALL" + "\n");
                strSqlString.Append("        SELECT FACTORY, RES_STS_2 AS MAT_ID, RES_STS_8 AS OPER, RES_GRP_6 AS RES_MODEL, SUB_AREA_ID, DECODE(STATUS, 1, 'U', 'D') AS STATUS, RES_ID, RES_STS_1" + "\n");
                strSqlString.Append("          FROM MESMGR.FMB_CRASRESSTS@RPTTOMES" + "\n");
                strSqlString.Append("         WHERE 1=1" + "\n");
                strSqlString.Append("           AND FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");
                strSqlString.Append("           AND RES_CMF_20 <= TO_CHAR(SYSDATE, 'YYYYMMDD')" + "\n");
                strSqlString.Append("           AND AREA_ID <> 'WB'" + "\n");
                strSqlString.Append("           AND (RES_STS_1 NOT IN ('C200', 'B199') OR RES_UP_DOWN_FLAG = 'U')" + "\n");
                strSqlString.Append("       ) A" + "\n");
                strSqlString.Append("     , MWIPMATDEF B" + "\n");
                strSqlString.Append("     , MGCMTBLDAT C" + "\n");
                strSqlString.Append(" WHERE 1=1" + "\n");
                strSqlString.Append("   AND A.FACTORY = B.FACTORY" + "\n");
                strSqlString.Append("   AND A.FACTORY = C.FACTORY" + "\n");
                strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
                strSqlString.Append("   AND C.KEY_1 = SUBSTR(A.RES_STS_1, 1,1) " + "\n");
                strSqlString.Append("   AND C.TABLE_NAME = 'STOP_CODE'" + "\n");

                #region 상세 조회에 따른 SQL문 생성
                
                if (dataArry[0] != " " && dataArry[0] != null)
                    strSqlString.AppendFormat("   AND A.SUB_AREA_ID = '" + dataArry[0] + "'" + "\n");

                if (dataArry[1] != " " && dataArry[1] != null)
                    strSqlString.AppendFormat("   AND A.RES_MODEL = '" + dataArry[1] + "'" + "\n");

                if (dataArry[2] != " " && dataArry[2] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_1 = '" + dataArry[2] + "'" + "\n");

                if (dataArry[3] != " " && dataArry[3] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_2 = '" + dataArry[3] + "'" + "\n");

                if (dataArry[4] != " " && dataArry[4] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_3 = '" + dataArry[4] + "'" + "\n");

                if (dataArry[5] != " " && dataArry[5] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_4 = '" + dataArry[5] + "'" + "\n");

                if (dataArry[6] != " " && dataArry[6] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_5 = '" + dataArry[6] + "'" + "\n");

                if (dataArry[7] != " " && dataArry[7] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_6 = '" + dataArry[7] + "'" + "\n");

                if (dataArry[8] != " " && dataArry[8] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_7 = '" + dataArry[8] + "'" + "\n");

                if (dataArry[9] != " " && dataArry[9] != null)
                    strSqlString.AppendFormat("   AND B.MAT_GRP_8 = '" + dataArry[9] + "'" + "\n");

                if (dataArry[10] != " " && dataArry[10] != null)
                    strSqlString.AppendFormat("   AND B.MAT_CMF_10 = '" + dataArry[10] + "'" + "\n");

                if (dataArry[11] != " " && dataArry[11] != null)
                    strSqlString.AppendFormat("   AND A.OPER = '" + dataArry[11] + "'" + "\n");

                if (dataArry[12] != " " && dataArry[12] != null)
                    strSqlString.AppendFormat("   AND B.MAT_ID = '" + dataArry[12] + "'" + "\n");

                if (dataArry[13] != " " && dataArry[13] != null)
                    strSqlString.AppendFormat("   AND B.MAT_CMF_7 = '" + dataArry[13] + "'" + "\n");
                

                #endregion

                strSqlString.AppendFormat(" ORDER BY RES_MODEL, RES_ID, STATUS" + "\n");
            }
            #endregion

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }
        #endregion MakeSqlDetail

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            if (CheckField() == false) return;

            GridColumnInit();
            spdData_Sheet1.RowCount = 0;

            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //spdData.DataSource = dt;
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 14, null, null, btnSort);

                spdData.RPT_FillDataSelectiveCells("Total", 0, 14, 0, 1, true, Align.Center, VerticalAlign.Center);
                //spdData.Sheets[0].FrozenColumnCount = 3;
                spdData.RPT_AutoFit(false);

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            if (cdvFactory.txtValue.Equals("HMKB1"))
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;
            }

            GridColumnInit();

            this.SetFactory(cdvFactory.txtValue);
            cdvModel.sFactory = cdvFactory.txtValue;
            cdvStep.sFactory = cdvFactory.txtValue;

            cdvStep.SetChangedFlag(true);
            cdvStep.Text = "";
            string strQuery = string.Empty;
            /*
            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = @"SELECT DISTINCT OPER Code, OPER_DESC Data FROM MWIPOPRDEF WHERE FACTORY " + cdvFactory.SelectedValueToQueryString + " ORDER BY 1 ";
            else
                cdvStep.sDynamicQuery = "";
            */

            strQuery += "SELECT DISTINCT OPR.OPER CODE, OPR.OPER_DESC DATA" + "\n";
            strQuery += "  FROM MRASRESMFO RES, MWIPOPRDEF OPR " + "\n";
            strQuery += " WHERE RES.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n";
            strQuery += "   AND REL_LEVEL='R' " + "\n";

            if (cdvFactory.txtValue.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                //2010-07-28-임종우 : A1300 공정 표시 되도록 추가함(임태성 요청), 원부자재 공정 제거함.
                strQuery += "   AND RES.OPER NOT IN ('A0100','A0150','A0250','A0320','A0330','A0340','A0350','A0370','A0380','A0390','A0500','A0501',";
                strQuery += "'A0502','A0503','A0504','A0505','A0506','A0507','A0508','A0509','A0550','A0950','A1100','A1110','A1180','A1230','A1720','A1950', 'A2050', 'A2100')" + "\n";
                strQuery += "   AND OPR.OPER LIKE 'A%'" + "\n";
            }
            else if (cdvFactory.txtValue.Equals("HMKB1"))
            {
            }
            else
            {
                strQuery += "   AND RES.OPER IN ('T0100','T0650','T0900','T1040','T1080','T1200')" + "\n";
            }

            strQuery += "   AND RES.FACTORY=OPR.FACTORY " + "\n";
            strQuery += "   AND RES.OPER=OPR.OPER " + "\n";
            strQuery += "ORDER BY OPR.OPER " + "\n";

            if (cdvFactory.txtValue != "")
                cdvStep.sDynamicQuery = strQuery;
            else
                cdvStep.sDynamicQuery = "";


            SortInit();     //add. 150601
        }

        #endregion

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string[] dataArry = null;            
            Color BackColor;
            string strDate = cdvDate.Value.ToString("yyyyMMdd");

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                dataArry = new string[16];
                BackColor = spdData.ActiveSheet.Cells[1, 16].BackColor;
            }
            else
            {
                dataArry = new string[14];
                BackColor = spdData.ActiveSheet.Cells[1, 14].BackColor;
            }            

            // GrandTotal 과 SubTotal 제외하기 위해
            if (e.Row != 0 && spdData.ActiveSheet.Cells[e.Row, e.Column].BackColor == BackColor)
            {
                if (spdData.ActiveSheet.Columns[e.Column].Label == "대수")
                {
                    // Group 정보 가져와서 담기... 상세 팝업에서 조건값으로 사용하기 위해
                    if (cdvFactory.Text.Trim() == "HMKB1")
                    {   
                        for (int i = 0; i < 16; i++)
                        {
                            if (spdData.ActiveSheet.Columns[i].Label == "Model")
                                dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Customer")
                                dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Bumping Type")
                                dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Process Flow")
                                dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Layer classification")
                                dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PKG Type")
                                dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "RDL Plating")
                                dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Final Bump")
                                dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Sub. Material")
                                dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Size")
                                dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Thickness")
                                dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Flat Type")
                                dataArry[11] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Wafer Orientation")
                                dataArry[12] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Step")
                                dataArry[13] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "Product")
                                dataArry[14] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else
                                dataArry[15] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < 14; i++)
                        {
                            if (spdData.ActiveSheet.Columns[i].Label == "BLOCK")
                                dataArry[0] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "MODEL")
                                dataArry[1] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "CUSTOMER")
                                dataArry[2] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "FAMILY")
                                dataArry[3] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PACKAGE")
                                dataArry[4] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "TYPE1")
                                dataArry[5] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "TYPE2")
                                dataArry[6] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "LD COUNT")
                                dataArry[7] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "DENSITY")
                                dataArry[8] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "GENERATION")
                                dataArry[9] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PIN TYPE")
                                dataArry[10] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "STEP")
                                dataArry[11] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else if (spdData.ActiveSheet.Columns[i].Label == "PRODUCT")
                                dataArry[12] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();

                            else
                                dataArry[13] = spdData.ActiveSheet.Cells[e.Row, i].Value.ToString();
                        }
                    }

                    // 고객사 명을 고객사 코드로 변경하기 위해..
                    if (dataArry[2] != " ")
                    {                        
                        DataTable dtCustomer = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlCustomer(dataArry[2].ToString()));

                        if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                            dataArry[2] = dtCustomer.Rows[0][0].ToString();
                    }

                    DataTable dt = null;

                    if (cdvFactory.Text.Trim() == "HMKB1")
                    {
                        dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(dataArry, 1));
                    }
                    else
                    {
                        if (e.Column == 14)
                        {
                            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(dataArry, 1));
                        }
                        else
                        {
                            //if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.Text == "현재")
                            if (DateTime.Now.ToString("yyyyMMdd").Equals(strDate) && cboTimeBase.SelectedIndex == 0)
                            {
                                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(dataArry, 2));
                            }
                            else
                            {
                                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD059", GlobalVariable.gcLanguage));
                            }
                        }
                    }

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        System.Windows.Forms.Form frm = new PRD010502_P1("", dt);
                        frm.ShowDialog();
                    }
                }
            }
        }

        
    }
}
