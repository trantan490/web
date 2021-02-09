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
    public partial class PRD011004 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011004<br/>
        /// 클래스요약: Reject IC<br/>
        /// 작  성  자: 김민우<br/>
        /// 최초작성일: 2012-07-05<br/>
        /// 상세  설명: Reject IC<br/>
        /// 변경  내용: <br/>
        /// 변  경  자: <br />        
        /// 2012-07-05-김민우 : 샘플
        /// 2012-10-05-임종우 : Total 값 표시 되도록 수정 (김은석 요청)
        /// 2013-03-29-김민우 : Reject IC 부분 Logic 이 L/F 사용 PKG 의 경우 T/F Out 시점으로 L/F 계산 되는 걸로 알고 있는데.
        ///                     이부분으로 인한 D/A ~ T/F 전까지 발생 된 Loss 가 L/F 1Strip 이 넘는 경우 Strip 수량이 오차가 발생 되고 있습니다.
        ///                     이로인해 중량 오차가 발생 되고 있으니 Logic 수정 요청 드립니다.
        ///                     D/A In 수량으로 L/F 사용 수량 계산으로 변경 요청 드립니다. (김은석D 요청)
        /// 2014-03-25-임종우 : 삼성 메모리는 PCB_STRIP_QTY 사용하지 않고 순수 계산 된 값으로 사용한다. (김성업D 요청)            
        /// 2016-03-18-임종우 : 제품 구분 없이 모두 PCB_STRIP_QTY 사용 (임태성K 요청)
        /// 2016-04-05-임종우 : 신규 SUMMARY 개발로 쿼리 수정
        /// 2016-06-30-임종우 : 요약 Page 검색시 GOOD 수량 = GOOD수량 - PVI LOSS - TERMINATE 로 변경 (임태성K 요청)
        /// 2017-10-10-임종우 : 표준중량, 이론중량 - Summary Data 사용 안 하고 조회 시에 계산하도록 변경..표중중량 미등록 또는 등록이 늦어지는 경우가 발생하여.. (김성업K 요청)
        /// </summary>

        public PRD011004()
        {
            InitializeComponent();
            this.cdvFactory.sFactory = GlobalVariable.gsTestDefaultFactory;
            cdvFromToDate.AutoBinding(DateTime.Now.AddDays(-2).ToString(), DateTime.Now.AddDays(-1).ToString());
            cdvFromToDate.DaySelector.Text = "일";
            cdvFromToDate.DaySelector.Enabled = false;
            SortInit();           
            GridColumnInit();
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cksum.Checked = true;
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
            if (cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                if (!cksum.Checked)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type2", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Density", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Generation", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG2", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type1", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Part no", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PCB (L/F) Material Code", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PCB Density", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Lot no", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Lot Good Q'ty", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Use Strip Q'ty", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PCB_Strip Q'ty", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Reject IC Q'ty", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PVI LOSS", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("총 Reject IC QTY", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Standard weight", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Reject weight", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                }
                else
                {
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Lead", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Density", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Use strip", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Theoretical quantity", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Good Qty", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Reject Qty", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Standard weight", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Theoretical weight", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                }
            }
            else if (cdvFactory.Text.Equals(GlobalVariable.gsTestDefaultFactory))
            {
                if (!cksum.Checked)
                {
                    spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type2", 0, 2, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("LD Count", 0, 3, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Density", 0, 4, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Generation", 0, 5, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG2", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG", 0, 7, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type1", 0, 8, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Part no", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Lot Good Q'ty", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("총 Reject IC QTY", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Standard weight", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("Reject weight (kg)", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
                }
                else
                {
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG2", 0, 1, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PKG Code", 0, 4, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Reject IC Q'ty", 0, 5, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("Weight (kg)", 0, 6, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
                }

            }
        }

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "MAT_GRP_1", "(SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1) AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "MAT_GRP_2", "MAT_GRP_2", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "MAT_GRP_8", "MAT_GRP_8", false);
        } 
        
        #endregion


        #region SQL 쿼리 Build (사용중지)
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

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;            
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;
            

            string FromDate = Convert.ToDateTime(cdvFromToDate.FromDate.Text).AddDays(-1).ToString("yyyyMMdd");
            string ToDate = Convert.ToDateTime(cdvFromToDate.ToDate.Text).AddDays(+1).ToString("yyyyMMdd");
            if (cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                if (!cksum.Checked)
                {

                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                    strSqlString.Append("     , MAT_GRP_10,MAT_GRP_3,MAT_GRP_4,MAT_CMF_11,MAT_ID,MATCODE,PCB_DENSITY,LOT_ID,QTY_1" + "\n");
                    //strSqlString.Append("     , MAT_GRP_10,MAT_GRP_3,MAT_GRP_4,MAT_CMF_11,MAT_ID,MATCODE,PCB_DENSITY,LOT_ID,QTY_1,STRIP_QTY,PCB_STRIP_QTY" + "\n");
                    

                    strSqlString.Append("     , DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY))) AS STRIP_QTY" + "\n");
                    strSqlString.Append("     , PCB_STRIP_QTY" + "\n");

                    /*
                    strSqlString.Append("     , DECODE(SIGN(Strip_qty-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,Strip_qty) * PCB_DENSITY - QTY_1 AS Reject_IC_QTY" + "\n");
                    strSqlString.Append("     , PVI_LOSS" + "\n");
                    strSqlString.Append("     , DECODE(SIGN(Strip_qty-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,Strip_qty) * PCB_DENSITY - QTY_1 + NVL(PVI_LOSS,0) AS TOTAL_REJECT" + "\n");
                    strSqlString.Append("     , ST_WEIGHT" + "\n");
                    strSqlString.Append("     , ((SELECT DATA_1 * (DECODE(SIGN(Strip_qty-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,Strip_qty)*PCB_DENSITY - QTY_1) FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11))/1000 AS REJECT_WEIGHT" + "\n");
                    */

                    strSqlString.Append("     , DECODE(SIGN(DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY)))-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY)))) * PCB_DENSITY - QTY_1 AS Reject_IC_QTY" + "\n");
                    strSqlString.Append("     , PVI_LOSS" + "\n");
                    strSqlString.Append("     , DECODE(SIGN(DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY)))-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY)))) * PCB_DENSITY - QTY_1 + NVL(PVI_LOSS,0) AS TOTAL_REJECT" + "\n");
                    strSqlString.Append("     , ST_WEIGHT" + "\n");
                    strSqlString.Append("     , ((SELECT DATA_1 * (DECODE(SIGN(DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY)))-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,DECODE(STRIP_QTY,1,1,DECODE(RESV_FIELD_2,'LF',CEIL(DA_IN_QTY/STRIP_QTY),CEIL(QTY_1/STRIP_QTY))))*PCB_DENSITY - QTY_1) FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11))/1000 AS REJECT_WEIGHT" + "\n");

                    
                    strSqlString.Append("  FROM( " + "\n");
                    strSqlString.AppendFormat("       SELECT {0} " + "\n", QueryCond1);
                    strSqlString.Append("            , MAT_GRP_10, MAT_GRP_3,MAT_GRP_4,MAT_CMF_11,LOT.MAT_ID" + "\n");
                    strSqlString.Append("            , MAX(MATCODE) KEEP(DENSE_RANK FIRST ORDER BY DECODE(EDIT_DT, ' ', CREATE_DT, EDIT_DT) DESC) AS MATCODE" + "\n");
                    strSqlString.Append("            , DECODE(PCB.C_MAT_CMF_4, ' ',0,PCB.C_MAT_CMF_4) AS PCB_DENSITY" + "\n");
                    strSqlString.Append("            , LOT.LOT_ID" + "\n");
                    strSqlString.Append("            , LOT.QTY_1" + "\n");
                    
                    //strSqlString.Append("            , CEIL(LOT.QTY_1/DECODE(PCB.C_MAT_CMF_4, ' ',LOT.QTY_1,PCB.C_MAT_CMF_4)) AS Strip_qty" + "\n");
                    strSqlString.Append("            , DECODE(PCB.C_MAT_CMF_4, ' ', 1, '0', 1, PCB.C_MAT_CMF_4) AS Strip_qty" + "\n");

                    strSqlString.Append("            , NVL(TO_NUMBER((SELECT C.ATTR_VALUE" + "\n");
                    strSqlString.Append("                               FROM CRESMATHIS@RPTTOMES A" + "\n");
                    strSqlString.Append("                                  , MWIPLOTSTS@RPTTOMES B" + "\n");
                    strSqlString.Append("                                  , MATRNAMSTS@RPTTOMES C" + "\n");
                    strSqlString.Append("                              WHERE A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("                                AND A.START_LOT_ID = B.LOT_ID   " + "\n");
                    strSqlString.Append("                                AND SUBSTR(A.RES_ID, 0, 2) = 'BD'" + "\n");
                    strSqlString.Append("                                AND SUBSTR(A.M_MAT_ID, 0, 3) = 'R16'" + "\n");
                    strSqlString.Append("                                AND A.LOT_ID = C.ATTR_KEY" + "\n");
                    strSqlString.Append("                                AND C.ATTR_VALUE <> ' '" + "\n");
                    strSqlString.Append("                                AND C.ATTR_TYPE = 'LOT'" + "\n");
                    strSqlString.Append("                                AND C.ATTR_NAME = 'PCB_STRIP'" + "\n");
                    strSqlString.Append("                                AND B.LOT_ID = LOT.LOT_ID" + "\n");
                    strSqlString.Append("              )),0) AS PCB_STRIP_QTY" + "\n");
                    strSqlString.Append("            , (SELECT SUM(TOTAL_LOSS_QTY) FROM MWIPLOTLOS@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OPER > 'A1900' AND LOT_ID = LOT.LOT_ID) AS PVI_LOSS" + "\n");
                    strSqlString.Append("            , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11 ) AS ST_WEIGHT" + "\n");

                    //strSqlString.Append("            , (SELECT OPER_IN_QTY_1 FROM CWIPLOTEND@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND OLD_OPER IN ('A0400','A0401') AND LOT_ID = LOT.LOT_ID) AS DA_IN_QTY" + "\n");

                    strSqlString.Append("            , (SELECT NVL(MAX(OPER_IN_QTY_1),0) FROM RPTMGR.RWIPLOTHIS"+ "\n");
                    strSqlString.Append("                WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'"+ "\n");
                    strSqlString.Append("                  AND TRAN_CODE = 'END'"+ "\n");
                    strSqlString.Append("                  AND OLD_OPER IN ('A0400','A0401')"+ "\n");
                    strSqlString.Append("                  AND LOT_ID = LOT.LOT_ID"+ "\n");
                    strSqlString.Append("                  AND HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("              ) AS DA_IN_QTY"+ "\n");
                    strSqlString.Append("            , BOM.RESV_FIELD_2" + "\n");


                    strSqlString.Append("         FROM CWIPLOTEND@RPTTOMES LOT" + "\n");
                    strSqlString.Append("            , MWIPMATDEF@RPTTOMES MAT" + "\n");
                    strSqlString.Append("            , CWIPBOMDEF@RPTTOMES BOM" + "\n");
                    strSqlString.Append("            , CWIPMATDEF@RPTTOMES PCB" + "\n");
                    strSqlString.Append("        WHERE 1=1" + "\n");
                    strSqlString.Append("          AND LOT.TRAN_TIME BETWEEN '" + FromDate + "220000" + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "215959'" + "\n");
                    strSqlString.Append("          AND LOT.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("          AND LOT.FACTORY = MAT.FACTORY" + "\n");
                    strSqlString.Append("          AND LOT.FACTORY = PCB.FACTORY" + "\n");
                    strSqlString.Append("          AND LOT.MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("          AND LOT.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("          AND LOT.MAT_ID = BOM.PARTNUMBER" + "\n");
                    strSqlString.Append("          AND MAT.MAT_VER = 1" + "\n");
                    strSqlString.Append("          AND MAT_GRP_1 = 'SE'" + "\n");
                    //strSqlString.Append("          AND LOT.OLD_OPER IN ('A1750','A1800','A1900')" + "\n");
                    strSqlString.Append("          AND LOT.OLD_OPER LIKE 'A04%'" + "\n");
                    strSqlString.Append("          AND STEPID LIKE 'A04%'" + "\n");
                    strSqlString.Append("          AND BOM.RESV_FIELD_2 IN ('PB','LF')" + "\n");
                    strSqlString.Append("          AND BOM.MATCODE = PCB.MAT_ID" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("          AND LOT.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    strSqlString.AppendFormat("       GROUP BY {0}, MAT_GRP_10, MAT_GRP_3,MAT_GRP_4,MAT_CMF_11,LOT.MAT_ID,LOT.LOT_ID, QTY_1,C_MAT_CMF_4, BOM.RESV_FIELD_2 )" + "\n", QueryCond1);

                }
                else
                {
                    /*
                    strSqlString.Append("SELECT PKG_CODE,FAMILY,PKG,LEAD_COUNT,DENSITY,SUM(STRIP_QTY),SUM(THEORY_STRIP),SUM(GOOD_QTY),SUM(REJECT_QTY),ST_WEIGHT,SUM(THEORY_WEIGHT)  " + "\n");
                    strSqlString.Append("  FROM RSUMREJTIC" + "\n");
                    strSqlString.Append(" WHERE WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    strSqlString.Append("GROUP BY PKG_CODE,FAMILY,PKG,LEAD_COUNT,DENSITY,ST_WEIGHT" + "\n");
                    */


                    strSqlString.Append("SELECT MAT_CMF_11, MAT_GRP_2, MAT_GRP_3, MAT_GRP_6, TO_CHAR(PCB_DENSITY) AS PCB_DENSITY " + "\n");
                    strSqlString.Append("     , SUM(DECODE(SIGN(STRIP_QTY-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,STRIP_QTY)) AS STRIP_QTY " + "\n");
                    strSqlString.Append("     , SUM((PCB_DENSITY * DECODE(SIGN(STRIP_QTY-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,STRIP_QTY))) AS THEORY_STRIP " + "\n");
                    strSqlString.Append("     , SUM((QTY_1 - NVL(PVI_LOSS,0))) AS GOOD_QTY " + "\n");
                    strSqlString.Append("     , SUM((PCB_DENSITY * DECODE(SIGN(STRIP_QTY-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,STRIP_QTY)) - (QTY_1 - NVL(PVI_LOSS,0))) AS REJECT_QTY " + "\n");
                    strSqlString.Append("     , NVL((SELECT DATA_1 FROM MESMGR.MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11 ),0) AS ST_WEIGHT " + "\n");
                    strSqlString.Append("     , NVL(SUM(((PCB_DENSITY * DECODE(SIGN(STRIP_QTY-PCB_STRIP_QTY),-1,PCB_STRIP_QTY,0,PCB_STRIP_QTY,1,STRIP_QTY)) - (QTY_1 - NVL(PVI_LOSS,0))) * ST_WEIGHT),0) AS THEORY_WEIGHT " + "\n");
                    strSqlString.Append("  FROM" + "\n");
                    strSqlString.Append("      (" + "\n");
                    //strSqlString.Append("       SELECT MAT_GRP_1,MAT_GRP_2,MAT_GRP_6,MAT_GRP_10,MAT_GRP_3,MAT_GRP_4,MAT_CMF_11,MAT_ID,PVI_LOSS,QTY_1,PCB_STRIP_QTY" + "\n");
                    strSqlString.Append("       SELECT MAT_GRP_1,MAT_GRP_2,MAT_GRP_6,MAT_GRP_10,MAT_GRP_3,MAT_GRP_4,MAT_CMF_11,MAT_ID,PVI_LOSS" + "\n");
                    //strSqlString.Append("            , DECODE(CMF_1,'LF',CMF_4,QTY_1) AS QTY_1" + "\n");

                    // 2016-03-18-임종우 : 제품 구분 없이 모두 PCB_STRIP_QTY 사용 (임태성K 요청)
                    //strSqlString.Append("            , CASE WHEN MAT_ID LIKE 'SEK%' THEN 0 ELSE PCB_STRIP_QTY END AS PCB_STRIP_QTY" + "\n");
                    strSqlString.Append("            , PCB_STRIP_QTY" + "\n");
                    strSqlString.Append("            , QTY_1" + "\n");
                    strSqlString.Append("            , TO_CHAR((SELECT C_MAT_CMF_4 FROM MESMGR.CWIPMATDEF@RPTTOMES WHERE MAT_ID = MATCODE AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "')) AS PCB_DENSITY" + "\n");
                    //strSqlString.Append("            , CEIL(QTY_1/DECODE((SELECT C_MAT_CMF_4 FROM MESMGR.CWIPMATDEF@RPTTOMES WHERE MAT_ID = MATCODE AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'), ' ',QTY_1,(SELECT C_MAT_CMF_4 FROM MESMGR.CWIPMATDEF@RPTTOMES WHERE MAT_ID = MATCODE AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'))) AS Strip_qty" + "\n");
                    strSqlString.Append("            , CEIL(DECODE(CMF_1,'LF',CMF_4,QTY_1)/DECODE((SELECT C_MAT_CMF_4 FROM MESMGR.CWIPMATDEF@RPTTOMES WHERE MAT_ID = MATCODE AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'), ' ',DECODE(CMF_1,'LF',CMF_4,QTY_1), '0',DECODE(CMF_1,'LF',CMF_4,QTY_1),(SELECT C_MAT_CMF_4 FROM MESMGR.CWIPMATDEF@RPTTOMES WHERE MAT_ID = MATCODE AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'))) AS Strip_qty" + "\n");
                    strSqlString.Append("            , NVL((SELECT DATA_1 FROM MESMGR.MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11 ),0) AS ST_WEIGHT" + "\n");
                    strSqlString.Append("         FROM RSUMRECTIC REJ" + "\n");
                    strSqlString.Append("        WHERE WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" + "\n");
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("          AND MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);
                    strSqlString.Append("      )" + "\n");
                    strSqlString.Append("GROUP BY MAT_CMF_11, MAT_GRP_2, MAT_GRP_3, MAT_GRP_6, PCB_DENSITY" + "\n");


                    


                }
            }
            else if (cdvFactory.Text.Equals(GlobalVariable.gsTestDefaultFactory))
            {
                if (!cksum.Checked)
                {
                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                    strSqlString.Append("           , MAT_GRP_10, MAT_GRP_3, MAT_GRP_4, MAT_CMF_11, MAT.MAT_ID, END_QTY, NVL(LOSS_QTY,0) AS REJECT_IC_QTY" + "\n");
                    strSqlString.Append("           , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11) AS ST_WEIGHT" + "\n");
                    strSqlString.Append("           , (NVL(LOSS_QTY,0) * (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11))/1000 AS RJ_WEIGHT" + "\n");
                    strSqlString.Append("           , MAT_CMF_10 AS PIN_TYPE" + "\n");
                    strSqlString.Append("        FROM (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID" + "\n");
                    strSqlString.Append("                  , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
                    strSqlString.Append("                FROM CSUMWIPMOV" + "\n");
                    //strSqlString.Append("               WHERE WORK_DATE BETWEEN '20120901' AND '20120931'" + "\n");
                    strSqlString.Append("               WHERE WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" +"\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("                 AND OPER = CASE WHEN SUBSTR(MAT_ID,-6,3) = 'ASB' THEN 'T0960' ELSE 'T0100' END" + "\n");
                    strSqlString.Append("                 AND CM_KEY_3 LIKE 'P%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) A," + "\n");
                    strSqlString.Append("              MWIPMATDEF MAT," + "\n");
                    strSqlString.Append("             (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID, SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
                    strSqlString.Append("                FROM RWIPLOTLSM" + "\n");
                    strSqlString.Append("               WHERE 1=1" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    //strSqlString.Append("                 AND TRAN_TIME BETWEEN '20120901060000' AND '20121001055959'" + "\n");
                    strSqlString.Append("                 AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "060000" + "' AND '" + ToDate + "055959'" + "\n");
                    strSqlString.Append("                 AND HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                 AND OPER <> 'T0000'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) LOSS" + "\n");
                    strSqlString.Append("       WHERE A.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("         AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("         AND A.MAT_ID = LOSS.MAT_ID(+)" + "\n");
                    strSqlString.Append("         AND MAT_VER = '1'" + "\n");
                }
                else
                {
                    strSqlString.Append("      SELECT MAT_CMF_10 AS PIN_TYPE, MAT_GRP_10, MAT_GRP_3, MAT_GRP_4, MAT_CMF_11, SUM(NVL(LOSS_QTY,0)) AS REJECT_IC_QTY" + "\n");
                    strSqlString.Append("           , (SUM(NVL(LOSS_QTY,0)) * (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11))/1000 AS RJ_WEIGHT" + "\n");
                    strSqlString.Append("        FROM (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID" + "\n");
                    strSqlString.Append("                  , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
                    strSqlString.Append("                FROM CSUMWIPMOV" + "\n");
                    strSqlString.Append("               WHERE WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("                 AND OPER = CASE WHEN SUBSTR(MAT_ID,-6,3) = 'ASB' THEN 'T0960' ELSE 'T0100' END" + "\n");
                    strSqlString.Append("                 AND CM_KEY_3 LIKE 'P%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) A," + "\n");
                    strSqlString.Append("              MWIPMATDEF MAT," + "\n");
                    strSqlString.Append("             (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID, SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
                    strSqlString.Append("                FROM RWIPLOTLSM" + "\n");
                    strSqlString.Append("               WHERE 1=1" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "060000" + "' AND '" + ToDate + "055959'" + "\n");
                    strSqlString.Append("                 AND HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                 AND OPER <> 'T0000'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) LOSS" + "\n");
                    strSqlString.Append("       WHERE A.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("         AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("         AND A.MAT_ID = LOSS.MAT_ID(+)" + "\n");
                    strSqlString.Append("         AND MAT_VER = '1'" + "\n");
                    strSqlString.Append("      GROUP BY MAT_CMF_10,MAT_GRP_10,MAT_GRP_3,MAT_GRP_4,MAT_CMF_11" + "\n");
                    
                }
          
            }
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }
                     
            return strSqlString.ToString();        
        }
        #endregion


        #region EVENT 처리
        private void btnView_Click(object sender, EventArgs e)
        {
      
            DataTable dt = null;
            if (CheckField() == false) return;
            spdData_Sheet1.RowCount = 0;
            
            try
            {
                LoadingPopUp.LoadIngPopUpShow(this);               
                this.Refresh();

                GridColumnInit();

                // 신규 SUMMARY 적용으로 사용중지
                //dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());
           
                // 신규 SUMMARY 적용
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
               

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                // 2012-10-05-임종우 : Total 값 표시 되도록 수정 시작
                int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                                
                if (!cksum.Checked)
                {                    
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 11, null, null, btnSort);
                                     
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 11, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                else
                {                    
                    int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 0, 5, null, null, btnSort);
                                     
                    spdData.RPT_FillDataSelectiveCells("Total", 0, 5, 0, 1, true, Align.Center, VerticalAlign.Center);
                }
                // 2012-10-05-임종우 : Total 값 표시 되도록 수정 끝
                
                
                //spdData.DataSource = dt;
                /*
                for (int i = 0; i < spdData.ActiveSheet.RowCount; i++)
                {
                    if (spdData.ActiveSheet.Cells[i, 5].Value != null && Convert.ToInt32(spdData.ActiveSheet.Cells[i, 5].Value) < 100)
                    {
                        spdData.ActiveSheet.Rows[i].BackColor = Color.Red;
                    }
                }
                */
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

        private string MakeSqlString1()
        {

            StringBuilder strSqlString = new StringBuilder();

            string QueryCond1 = null;
            string QueryCond2 = null;
            string QueryCond3 = null;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;


            string FromDate = cdvFromToDate.HmFromDay;
            string ToDate = cdvFromToDate.HmToDay;

            if (cdvFactory.Text.Equals(GlobalVariable.gsAssyDefaultFactory))
            {
                if (!cksum.Checked)
                {

                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                    strSqlString.Append("     , MAT_GRP_10, MAT_GRP_3, MAT_GRP_4, MAT_CMF_11" + "\n");
                    strSqlString.Append("     , A.MAT_ID" + "\n");
                    strSqlString.Append("     , A.MATCODE" + "\n");
                    strSqlString.Append("     , A.PCB_DENSITY" + "\n");
                    strSqlString.Append("     , A.LOT_ID" + "\n");
                    strSqlString.Append("     , A.GOOD_QTY" + "\n");
                    strSqlString.Append("     , A.USE_STRIP" + "\n");
                    strSqlString.Append("     , A.PCB_STRIP_QTY" + "\n");
                    strSqlString.Append("     , A.THEORY_QTY - A.GOOD_QTY AS REJECT_QTY" + "\n");
                    strSqlString.Append("     , A.PVI_LOSS + A.TERMINATE AS PVI_LOSS" + "\n");
                    strSqlString.Append("     , A.REJECT_QTY AS TTL_REJECT_QTY" + "\n");
                    // 2017-10-10-임종우 : 조회 시 직접 계산하도록 변경 - Summary Data 사용중지(표중중량 미등록 또는 등록이 늦어지는 경우가 발생하여...)
                    //strSqlString.Append("     , ROUND(A.ST_WEIGHT,4) AS ST_WEIGHT" + "\n");
                    //strSqlString.Append("     , ROUND(A.THEORY_WEIGHT,4) AS THEORY_WEIGHT" + "\n");
                    strSqlString.Append("     , ROUND(NVL((SELECT DATA_1 FROM MESMGR.MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = B.MAT_CMF_11 ),0),4) AS ST_WEIGHT" + "\n");
                    strSqlString.Append("     , ROUND(A.REJECT_QTY * NVL((SELECT DATA_1 FROM MESMGR.MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = B.MAT_CMF_11 ),0),4) AS THEORY_WEIGHT" + "\n");
                    strSqlString.Append("  FROM RSUMREJEIC A " + "\n");
                    strSqlString.Append("     , MWIPMATDEF B" + "\n");                   
                    strSqlString.Append(" WHERE 1=1" + "\n");
                    strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
                    strSqlString.Append("   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("   AND B.DELETE_FLAG = ' '" + "\n");
                    strSqlString.Append("   AND A.WORK_DATE BETWEEN '" + FromDate + "' AND '" + ToDate + "'" + "\n");
                    
                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                    strSqlString.AppendFormat(" ORDER BY {0}, MAT_GRP_10, MAT_GRP_3, MAT_GRP_4, MAT_CMF_11, MAT_ID, LOT_ID" + "\n", QueryCond1);

                }
                else
                {
                    strSqlString.Append("SELECT B.MAT_CMF_11, B.MAT_GRP_2, B.MAT_GRP_10, B.MAT_GRP_6 " + "\n");
                    strSqlString.Append("     , MAX(PCB_DENSITY) AS PCB_DENSITY " + "\n");
                    strSqlString.Append("     , SUM(USE_STRIP) AS USE_STRIP " + "\n");
                    strSqlString.Append("     , SUM(THEORY_QTY) AS THEORY_QTY " + "\n");
                    strSqlString.Append("     , SUM(GOOD_QTY) - SUM(PVI_LOSS) - SUM(TERMINATE) AS GOOD_QTY  " + "\n");
                    strSqlString.Append("     , SUM(REJECT_QTY) AS REJECT_QTY " + "\n");                    
                    // 2017-10-10-임종우 : 조회 시 직접 계산하도록 변경 - Summary Data 사용중지(표중중량 미등록 또는 등록이 늦어지는 경우가 발생하여...)
                    //strSqlString.Append("     , ROUND(MAX(ST_WEIGHT),4) AS ST_WEIGHT " + "\n");
                    //strSqlString.Append("     , ROUND(SUM(THEORY_WEIGHT),4) AS THEORY_WEIGHT " + "\n");
                    strSqlString.Append("     , ROUND(MAX(NVL((SELECT DATA_1 FROM MESMGR.MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = B.MAT_CMF_11 ),0)),4) AS ST_WEIGHT " + "\n");
                    strSqlString.Append("     , ROUND(SUM(REJECT_QTY) * MAX(NVL((SELECT DATA_1 FROM MESMGR.MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = B.MAT_CMF_11 ),0)),4) AS THEORY_WEIGHT " + "\n");
                    strSqlString.Append("  FROM RSUMREJEIC A" + "\n");
                    strSqlString.Append("     , MWIPMATDEF B" + "\n");                    
                    strSqlString.Append(" WHERE 1=1" + "\n");
                    strSqlString.Append("   AND A.MAT_ID = B.MAT_ID" + "\n");
                    strSqlString.Append("   AND B.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
                    strSqlString.Append("   AND B.DELETE_FLAG = ' '" + "\n");
                    strSqlString.Append("   AND WORK_DATE BETWEEN '" + FromDate + "' AND '" + ToDate + "'" + "\n");

                    if (txtSearchProduct.Text.Trim() != "%" && txtSearchProduct.Text.Trim() != "")
                        strSqlString.AppendFormat("   AND A.MAT_ID LIKE '{0}'" + "\n", txtSearchProduct.Text);

                    strSqlString.Append(" GROUP BY B.MAT_CMF_11, B.MAT_GRP_2, B.MAT_GRP_10, B.MAT_GRP_6" + "\n");
                    strSqlString.Append(" ORDER BY B.MAT_CMF_11, B.MAT_GRP_2, B.MAT_GRP_10, B.MAT_GRP_6" + "\n");

                }
            }
            else if (cdvFactory.Text.Equals(GlobalVariable.gsTestDefaultFactory))
            {
                if (!cksum.Checked)
                {
                    strSqlString.AppendFormat("SELECT {0} " + "\n", QueryCond1);
                    strSqlString.Append("           , MAT_GRP_10, MAT_GRP_3, MAT_GRP_4, MAT_CMF_11, MAT.MAT_ID, END_QTY, NVL(LOSS_QTY,0) AS REJECT_IC_QTY" + "\n");
                    strSqlString.Append("           , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11) AS ST_WEIGHT" + "\n");
                    strSqlString.Append("           , (NVL(LOSS_QTY,0) * (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11))/1000 AS RJ_WEIGHT" + "\n");
                    strSqlString.Append("           , MAT_CMF_10 AS PIN_TYPE" + "\n");
                    strSqlString.Append("        FROM (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID" + "\n");
                    strSqlString.Append("                  , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
                    strSqlString.Append("                FROM CSUMWIPMOV" + "\n");
                    //strSqlString.Append("               WHERE WORK_DATE BETWEEN '20120901' AND '20120931'" + "\n");
                    strSqlString.Append("               WHERE WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("                 AND OPER = CASE WHEN SUBSTR(MAT_ID,-6,3) = 'ASB' THEN 'T0960' ELSE 'T0100' END" + "\n");
                    strSqlString.Append("                 AND CM_KEY_3 LIKE 'P%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) A," + "\n");
                    strSqlString.Append("              MWIPMATDEF MAT," + "\n");
                    strSqlString.Append("             (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID, SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
                    strSqlString.Append("                FROM RWIPLOTLSM" + "\n");
                    strSqlString.Append("               WHERE 1=1" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    //strSqlString.Append("                 AND TRAN_TIME BETWEEN '20120901060000' AND '20121001055959'" + "\n");
                    strSqlString.Append("                 AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "060000" + "' AND '" + ToDate + "055959'" + "\n");
                    strSqlString.Append("                 AND HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                 AND OPER <> 'T0000'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) LOSS" + "\n");
                    strSqlString.Append("       WHERE A.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("         AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("         AND A.MAT_ID = LOSS.MAT_ID(+)" + "\n");
                    strSqlString.Append("         AND MAT_VER = '1'" + "\n");
                }
                else
                {
                    strSqlString.Append("      SELECT MAT_CMF_10 AS PIN_TYPE, MAT_GRP_10, MAT_GRP_3, MAT_GRP_4, MAT_CMF_11, SUM(NVL(LOSS_QTY,0)) AS REJECT_IC_QTY" + "\n");
                    strSqlString.Append("           , (SUM(NVL(LOSS_QTY,0)) * (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND TABLE_NAME = 'PKG_CD_WEIGHT' AND KEY_1 = MAT_CMF_11))/1000 AS RJ_WEIGHT" + "\n");
                    strSqlString.Append("        FROM (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID" + "\n");
                    strSqlString.Append("                  , SUM(S1_END_QTY_1+S2_END_QTY_1+S3_END_QTY_1) AS END_QTY" + "\n");
                    strSqlString.Append("                FROM CSUMWIPMOV" + "\n");
                    strSqlString.Append("               WHERE WORK_DATE BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "' AND '" + cdvFromToDate.ToDate.Text.Replace("-", "") + "'" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND CM_KEY_1 = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND LOT_TYPE = 'W'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("                 AND OPER = CASE WHEN SUBSTR(MAT_ID,-6,3) = 'ASB' THEN 'T0960' ELSE 'T0100' END" + "\n");
                    strSqlString.Append("                 AND CM_KEY_3 LIKE 'P%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) A," + "\n");
                    strSqlString.Append("              MWIPMATDEF MAT," + "\n");
                    strSqlString.Append("             (" + "\n");
                    strSqlString.Append("              SELECT MAT_ID, SUM(LOSS_QTY) AS LOSS_QTY" + "\n");
                    strSqlString.Append("                FROM RWIPLOTLSM" + "\n");
                    strSqlString.Append("               WHERE 1=1" + "\n");
                    strSqlString.Append("                 AND FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("                 AND TRAN_TIME BETWEEN '" + cdvFromToDate.FromDate.Text.Replace("-", "") + "060000" + "' AND '" + ToDate + "055959'" + "\n");
                    strSqlString.Append("                 AND HIST_DEL_FLAG = ' '" + "\n");
                    strSqlString.Append("                 AND OPER <> 'T0000'" + "\n");
                    strSqlString.Append("                 AND MAT_ID LIKE 'SE%'" + "\n");
                    strSqlString.Append("               GROUP BY MAT_ID" + "\n");
                    strSqlString.Append("             ) LOSS" + "\n");
                    strSqlString.Append("       WHERE A.MAT_ID = MAT.MAT_ID" + "\n");
                    strSqlString.Append("         AND MAT.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'" + "\n");
                    strSqlString.Append("         AND A.MAT_ID = LOSS.MAT_ID(+)" + "\n");
                    strSqlString.Append("         AND MAT_VER = '1'" + "\n");
                    strSqlString.Append("      GROUP BY MAT_CMF_10,MAT_GRP_10,MAT_GRP_3,MAT_GRP_4,MAT_CMF_11" + "\n");

                }

            }
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();        
        
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
        }
        #endregion
    }
}
