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

namespace Hana.MAT
{
    public partial class MAT070501 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: MAT070501<br/>
        /// 클래스요약: 원부자재 재공 현황<br/>
        /// 작  성  자: 하나마이크론 임종우<br/>
        /// 최초작성일: 2010-09-07<br/>
        /// 상세  설명: 원부자재 재공 현황<br/>
        /// 변경  내용: <br/>
        /// 2011-09-09-임종우 : 현장창고재고 추가
        /// 2012-01-04-임종우 : 기존 TOMS 데이터를 WMS 로 변경.
        /// 2012-04-19-임종우 : 공정재공 표시 (임태성 요청)
        /// 2012-04-19-임종우 : 과거 데이터 조회 기능 추가 보류 - 설비에 걸린 자재는 과거 데이터가 없어서... (김은석 요청)
        /// 2012-05-14-임종우 : 과거 데이터 조회 기능 추가 - 단, 재고 조회만 과거 표시. 다른 부분은 현재 상태 표시 함.
        /// 2013-07-16-임종우 : 창고 재고 과거 데이터 테이블 변경
        ///                     외주 재고 수량 표시 (우익)
        /// 2013-09-02-임종우 : 창고 재고에 기존 1000번 창고와 1003번 창고 재고 포함한다 (임태성 요청)
        /// 2013-09-06-임종우 : SMT 전 재고와 현장 재고를 분리하여 표시 한다 (김권수 요청)
        /// 2014-02-03-임종우 : 자재타입 멀티 선택 오류 부분 수정
        /// 2014-02-27-임종우 : 공정투입 대기 자재는 공정창고에 포함되도록 수정 (김권수D 요청)
        /// 2014-03-10-임종우 : 재고 수량 중복 오류 수정
        /// 2018-10-02-임종우 : 창고 재고 과거 데이터 테이블 변경 ZHMMT111@SAPREAL -> CWMSLOTSTS_BOH
        /// 2019-03-07-임종우 : 창고 재고에 1002번 창고-Tray 추가 (임태성 요청)
        /// 2020-01-13-김미경 : 유효기간 초과 재고(WMS 저장위치 1004 번 창고 재고) 표현 - 이승희 D
        #region " MAT070501 : Program Initial "

        public MAT070501()
        {
            InitializeComponent();
            SortInit();
            cdvDate.Value = DateTime.Now;
            GridColumnInit();
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvMatCode.sFactory = GlobalVariable.gsAssyDefaultFactory;
        }

        #endregion


        #region " Function Definition "

        /// <summary 1. 유효성 검사>
        /// <remarks></remarks>
        /// </summary>
        /// <returns></returns>
        private Boolean CheckField()
        {
            if (String.IsNullOrEmpty(cdvMatType.Text))
            {
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD003", GlobalVariable.gcLanguage));
                return false;
            }

            return true;
        }

        #endregion


        #region " GridColumnInit : Sheet Title 설정 "

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            try
            {
                int moveSeq = 0;

                spdData.RPT_ColumnInit();
                spdData.RPT_AddBasicColumn("Material Type", 0, 0, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Material code", 0, 1, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Item name", 0, 2, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Unit", 0, 3, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);

                //Material Type
                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                //Material code
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                //Item name
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                //Unit
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);

                if (GlobalVariable.gsGlovalSite == "K1")
                {
                    spdData.RPT_AddBasicColumn("Vendor", 0, 4, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                }
                else
                {
                    moveSeq += 1;
                }

                
                spdData.RPT_AddBasicColumn("Inventory status", 0, 5 - moveSeq, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("Warehouse stock", 1, 5 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("On-site warehouse stock", 1, 6 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 100);
                spdData.RPT_AddBasicColumn("Outsourcing stock", 1, 7 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("SMT All WIP", 1, 8 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("On-site inventory", 1, 9 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("TTL", 1, 10 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderColumnSpan(0, 5 - moveSeq, 6);

                spdData.RPT_AddBasicColumn("Expired inventory", 0, 11 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 60);
                spdData.RPT_MerageHeaderRowSpan(0, 11 - moveSeq, 2);

                spdData.RPT_AddBasicColumn("Order Status", 0, 12 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_MerageHeaderRowSpan(0, 12 - moveSeq, 2);


                // 2012-05-14-임종우 : 과거 데이터 조회 시 해당 내역 표시 안 함
                if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                {

                    if (GlobalVariable.gsGlovalSite == "K1")
                    {
                        // 2020-08-25-이희석 : 주차에 따른 입고계획 수량 표시
                        spdData.RPT_AddBasicColumn("Plan by work week", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W1", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W2", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W3", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);
                    }
                    else
                    {
                        moveSeq += 3;
                    }
                    spdData.RPT_AddBasicColumn("On-site inventory status (LOT)", 0, 16 - moveSeq, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("A급", 1, 16 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("B급", 1, 17 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("input", 1, 18 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TTL", 1, 19 - moveSeq, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 16 - moveSeq, 4);
                }
                else
                {

                    if (GlobalVariable.gsGlovalSite == "K1")
                    {
                        // 2020-08-25-이희석 : 주차에 따른 입고계획 수량 표시
                        spdData.RPT_AddBasicColumn("Plan by work week", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W1", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W2", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_AddBasicColumn("W3", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                        spdData.RPT_MerageHeaderColumnSpan(0, 13, 3);
                    }
                    else
                    {
                        moveSeq += 3;
                    }
                    spdData.RPT_AddBasicColumn("On-site inventory status (LOT)", 0, 16 - moveSeq, Visibles.False, Frozen.False, Align.Center, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("A급", 1, 16 - moveSeq, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("B급", 1, 17 - moveSeq, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("input", 1, 18 - moveSeq, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("TTL", 1, 19 - moveSeq, Visibles.False, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_MerageHeaderColumnSpan(0, 16 - moveSeq, 4);
                }

                // Group항목이 있을경우 반드시 선언해줄것.
                spdData.RPT_ColumnConfigFromTable(btnSort);

            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion


        #region " SortInit : Group By 설정 "

        /// <summary>
        /// 3. Group By 정의
        /// </summary>
        private void SortInit()
        {
            try
            {
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS MAT_GRP_1", "MAT.MAT_GRP_1", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT_GRP_2", "MAT_GRP_2", "MAT.MAT_GRP_2", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT_GRP_6", "MAT_GRP_6", "MAT.MAT_GRP_6", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT_GRP_3", "MAT_GRP_3", "MAT.MAT_GRP_3", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT_GRP_4", "MAT_GRP_4", "MAT.MAT_GRP_4", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT_GRP_5", "MAT_GRP_5", "MAT.MAT_GRP_5", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT_GRP_7", "MAT_GRP_7", "MAT.MAT_GRP_7", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT_GRP_8", "MAT_GRP_8", "MAT.MAT_GRP_8", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT_CMF_10", "MAT_CMF_10", "MAT.MAT_CMF_10", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("PRODUCT", "MAT_ID", "MAT_ID", "MAT.MAT_ID", true);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUST DEVICE", "MAT_CMF_7", "MAT_CMF_7", "MAT.MAT_CMF_7", false);
                //((udcTableForm)(this.btnSort.BindingForm)).AddRow("Material code", "MATCODE", "MATCODE", "MAT.MATCODE", true);
            }
            catch (Exception ex)
            {
                CmnFunction.ShowMsgBox(ex.Message);
                LoadingPopUp.LoadingPopUpHidden();
            }
        }

        #endregion



        #region " MakeSqlString : Sql Query문 "

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        private string MakeSqlString()
        {
            //string QueryCond1 = null;
            //string QueryCond2 = null;
            //string QueryCond3 = null;

            StringBuilder strSqlString = new StringBuilder();

            //udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            //QueryCond1 = tableForm.SelectedValueToQueryContainNull;
            //QueryCond2 = tableForm.SelectedValue2ToQueryContainNull;
            //QueryCond3 = tableForm.SelectedValue3ToQueryContainNull;

            strSqlString.Append("SELECT A.MAT_TYPE, A.MAT_ID, A.MAT_DESC, A.UNIT_1" + "\n");

            //2020-09-03-이희석 본사 적용
            if (GlobalVariable.gsGlovalSite == "K1") 
                strSqlString.Append("     , MAX(NVL(VENDOR_NAME,'')) AS VENDOR" + "\n");

            strSqlString.Append("     , SUM(B.INV_QTY) AS INV_QTY" + "\n");
            strSqlString.Append("     , SUM(B.INV_L_QTY) AS INV_L_QTY" + "\n");
            strSqlString.Append("     , SUM(E.WIK_WIP) AS WIK_WIP " + "\n");
            strSqlString.Append("     , SUM(C.SMT_BEFORE) AS SMT_BEFORE " + "\n");
            strSqlString.Append("     , SUM(C.SMT_AFTER) AS SMT_AFTER " + "\n");
            strSqlString.Append("     , SUM(NVL(B.INV_QTY,0)) + SUM(NVL(B.INV_L_QTY,0)) + SUM(NVL(C.QTY_TTL,0)) + SUM(NVL(E.WIK_WIP,0)) AS TTL" + "\n");
            strSqlString.Append("     , SUM(B.QTY_1004) AS QTY_1004 " + "\n");
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("     , SUM(D.ORDER_QTY) AS ORDER_QTY" + "\n");
            }
            else
            {
                if (GlobalVariable.gsGlovalSite == "K1") strSqlString.Append("     , (SUM(NVL(F.W1,0))+SUM(NVL(F.W2,0))+SUM(NVL(F.W3,0))) AS ORDER_QTY" + "\n");
            }
            if (GlobalVariable.gsGlovalSite == "K1")
            {
                strSqlString.Append("     , SUM(NVL(F.W1,0)) AS W1" + "\n");
                strSqlString.Append("     , SUM(NVL(F.W2,0)) AS W2" + "\n");
                strSqlString.Append("     , SUM(NVL(F.W3,0)) AS W3" + "\n");
            }
            strSqlString.Append("     , SUM(C.GRADE_A) AS GRADE_A" + "\n");
            strSqlString.Append("     , SUM(C.GRADE_B) AS GRADE_B" + "\n");
            strSqlString.Append("     , SUM(C.IN_QTY) AS IN_QTY" + "\n");
            strSqlString.Append("     , SUM(C.LOT_TTL) AS LOT_TTL" + "\n");
            strSqlString.Append("  FROM MWIPMATDEF A " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID " + "\n");
            strSqlString.Append("             , SUM(INV_QTY) AS INV_QTY " + "\n");
            strSqlString.Append("             , SUM(INV_L_QTY) AS INV_L_QTY " + "\n");
            strSqlString.Append("             , SUM(QTY_1004) AS QTY_1004 " + "\n");
            strSqlString.Append("          FROM ( " + "\n");
            strSqlString.Append("                SELECT MAT_ID " + "\n");
            strSqlString.Append("                     , SUM(DECODE(STORAGE_LOCATION, '1000', QUANTITY, '1002', QUANTITY, '1003', QUANTITY, 0)) AS INV_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(STORAGE_LOCATION, '1001', QUANTITY, 0)) AS INV_L_QTY " + "\n");
            strSqlString.Append("                     , SUM(DECODE(STORAGE_LOCATION, '1004', QUANTITY, 0)) AS QTY_1004 " + "\n");

            // 2012-05-14-임종우 : 재고현황 과거 데이터 조회 기능 추가
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("                  FROM CWMSLOTSTS@RPTTOMES " + "\n");
                strSqlString.Append("                 WHERE 1=1 " + "\n");
            }
            else
            {
                strSqlString.Append("                  FROM CWMSLOTSTS_BOH@RPTTOMES " + "\n");
                strSqlString.Append("                 WHERE CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            strSqlString.Append("                   AND QUANTITY> 0 " + "\n");
            strSqlString.Append("                   AND STORAGE_LOCATION IN ('1000', '1001', '1002', '1003', '1004') " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("                 UNION ALL " + "\n");
            strSqlString.Append("                SELECT MAT_ID, 0 AS INV_QTY, SUM(QTY_1) AS INV_L_QTY, 0 AS QTY_1004 " + "\n");
            strSqlString.Append("                  FROM CWIPMATSLP@RPTTOMES " + "\n");
            strSqlString.Append("                 WHERE 1=1 " + "\n");
            strSqlString.Append("                   AND RECV_FLAG = ' ' " + "\n");
            strSqlString.Append("                   AND IN_TIME BETWEEN '" + cdvDate.Value.AddDays(-2).ToString("yyyyMMdd") + "000000' AND '" + cdvDate.SelectedValue() + "235959' " + "\n");
            strSqlString.Append("                 GROUP BY MAT_ID " + "\n");
            strSqlString.Append("               ) " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) B " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.LOT_CMF_9 = 'A' AND B.LOT_ID IS NULL THEN 1 ELSE 0 END) AS GRADE_A" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN A.LOT_CMF_9 = 'B' AND B.LOT_ID IS NULL THEN 1 ELSE 0 END) AS GRADE_B" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN B.LOT_ID IS NOT NULL THEN 1 ELSE 0 END) AS IN_QTY" + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER LIKE 'M%' AND OPER <= 'M0330' THEN QTY_1 ELSE 0 END) AS SMT_BEFORE " + "\n");
            strSqlString.Append("             , SUM(CASE WHEN OPER LIKE 'A%' OR OPER > 'M0330' THEN QTY_1 ELSE 0 END) AS SMT_AFTER " + "\n");
            strSqlString.Append("             , COUNT(*) AS LOT_TTL " + "\n");
            strSqlString.Append("             , SUM(QTY_1) AS QTY_TTL " + "\n");

            // 2012-05-14-임종우 : 재고현황 과거 데이터 조회 기능 추가
            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("          FROM RWIPLOTSTS A " + "\n");
                strSqlString.Append("             , CRASRESMAT B " + "\n");
                strSqlString.Append("         WHERE 1=1  " + "\n");
            }
            else
            {
                strSqlString.Append("          FROM RWIPLOTSTS_BOH A " + "\n");
                strSqlString.Append("             , CRASRESMAT B " + "\n");
                strSqlString.Append("         WHERE 1=1  " + "\n");
                strSqlString.Append("           AND A.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            strSqlString.Append("           AND A.FACTORY = B.FACTORY(+) " + "\n");
            strSqlString.Append("           AND A.LOT_ID = B.LOT_ID(+) " + "\n");
            strSqlString.Append("           AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");
            strSqlString.Append("           AND A.LOT_TYPE != 'W'" + "\n");
            strSqlString.Append("           AND A.LOT_DEL_FLAG = ' '" + "\n");
            strSqlString.Append("           AND A.LOT_CMF_2 = '-' " + "\n");
            //strSqlString.Append("           AND A.LOT_CMF_5 = 'PP' " + "\n");
            strSqlString.Append("           AND A.LOT_CMF_9 != ' ' " + "\n");
            strSqlString.Append("           AND A.QTY_1 > 0 " + "\n");
            strSqlString.Append("           AND A.OPER NOT IN  ('00001', '00002', 'V0000') " + "\n");
            strSqlString.Append("         GROUP BY A.MAT_ID  " + "\n");
            strSqlString.Append("       ) C " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(ORDER_QUAN) AS ORDER_QTY " + "\n");
            strSqlString.Append("          FROM RSAPORDQNT " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");
            strSqlString.Append("           AND CREATE_TIME = TO_CHAR(SYSDATE, 'YYYYMMDD') " + "\n");
            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) D " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("        SELECT MAT_ID, SUM(LOT_QTY) AS WIK_WIP " + "\n");
            strSqlString.Append("          FROM ISTMWIKWIP@RPTTOMES " + "\n");
            strSqlString.Append("         WHERE 1=1 " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
            {
                strSqlString.Append("           AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "' || TO_CHAR(SYSDATE, 'HH24')" + "\n");
            }
            else
            {
                strSqlString.Append("           AND CUTOFF_DT = '" + cdvDate.SelectedValue() + "22'" + "\n");
            }

            strSqlString.Append("         GROUP BY MAT_ID " + "\n");
            strSqlString.Append("       ) E " + "\n");
            //2020-08-25-이희석 주차코드 입고계획 수량 표시
            if (GlobalVariable.gsGlovalSite == "K1")
            {
                strSqlString.Append("       , ( " + "\n");
                strSqlString.Append("         SELECT C.MAT_ID,SUM(W1) W1,SUM(W2) W2,SUM(W3) W3,MAX(VENDOR_NAME) VENDOR_NAME" + "\n");
                strSqlString.Append("         FROM (" + "\n");
                strSqlString.Append("         SELECT A.MAT_ID,A.VENDOR_NAME,CASE" + "\n");
                //선택날짜로부터 한달전까지 데이터 수량 취합
                for (int i = 0; i > -30; i--)
                {
                    strSqlString.Append("         WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE THEN (D" + (-i + 1).ToString().PadLeft(3, '0') + "+D" + (-i + 2).ToString().PadLeft(3, '0') + "+D" + (-i + 3).ToString().PadLeft(3, '0') + "+D" + (-i + 4).ToString().PadLeft(3, '0') + "+D" + (-i + 5).ToString().PadLeft(3, '0') + "+D" + (-i + 6).ToString().PadLeft(3, '0') + "+D" + (-i + 7).ToString().PadLeft(3, '0') + ")  " + "\n");
                }
                for (int i = 1; i < 8; i++)
                {
                    strSqlString.Append("         WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE");
                    strSqlString.Append("         THEN (");
                    for (int j = 1; j < (8 - i); j++)
                    {
                        strSqlString.Append(" D" + j.ToString().PadLeft(3, '0') + "+");
                    }

                    strSqlString.Append(" 0 ) " + "\n");
                }
                strSqlString.Append("         ELSE 0   END W1 ,\n");
                strSqlString.Append("         CASE");
                for (int i = 0; i > -30; i--)
                {
                    strSqlString.Append("         WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE THEN (D" + (-i + 8).ToString().PadLeft(3, '0') + "+D" + (-i + 9).ToString().PadLeft(3, '0') + "+D" + (-i + 10).ToString().PadLeft(3, '0') + "+D" + (-i + 11).ToString().PadLeft(3, '0') + "+D" + (-i + 12).ToString().PadLeft(3, '0') + "+D" + (-i + 13).ToString().PadLeft(3, '0') + "+D" + (-i + 14).ToString().PadLeft(3, '0') + ") " + "\n");
                }
                for (int i = 1; i < 15; i++)
                {
                    strSqlString.Append("         WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE");
                    strSqlString.Append("         THEN (");
                    if (i > 7)
                    {
                        for (int j = 1; j < (15 - i); j++)
                        {
                            strSqlString.Append(" D" + j.ToString().PadLeft(3, '0') + "+");
                        }
                    }
                    else
                    {
                        for (int j = 1; j < 8; j++)
                        {
                            strSqlString.Append(" D" + (7 - i + j).ToString().PadLeft(3, '0') + "+");
                        }
                    }

                    strSqlString.Append("           0)   " + "\n");
                }
                strSqlString.Append("         ELSE 0 END W2 ,\n");
                strSqlString.Append("         CASE");
                for (int i = 0; i > -30; i--)
                {
                    strSqlString.Append("         WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE THEN (D" + (-i + 15).ToString().PadLeft(3, '0') + "+D" + (-i + 16).ToString().PadLeft(3, '0') + "+D" + (-i + 17).ToString().PadLeft(3, '0') + "+D" + (-i + 18).ToString().PadLeft(3, '0') + "+D" + (-i + 19).ToString().PadLeft(3, '0') + "+D" + (-i + 20).ToString().PadLeft(3, '0') + "+D" + (-i + 21).ToString().PadLeft(3, '0') + ")  " + "\n");
                }
                for (int i = 1; i < 22; i++)
                {
                    strSqlString.Append("         WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE");
                    strSqlString.Append("         THEN (");
                    if (i > 14)
                    {
                        for (int j = 1; j < (22 - i); j++)
                        {
                            strSqlString.Append("         D" + j.ToString().PadLeft(3, '0') + "+");
                        }
                    }
                    else
                    {
                        for (int j = 1; j < 8; j++)
                        {
                            strSqlString.Append("         D" + (14 - i + j).ToString().PadLeft(3, '0') + "+");
                        }

                    }
                    strSqlString.Append("           0 ) " + "\n");
                }
                strSqlString.Append("           ELSE 0  END W3 \n");
                strSqlString.Append("           FROM ISAPWMSPLN A,(SELECT MAT_ID,MAX(CREATE_DATE) CREATE_DATE,PO_NUMBER,MAX(PO_NUMBER_SEQ) PO_SEQ FROM ISAPWMSPLN GROUP BY PO_NUMBER,MAT_ID) B" + "\n");
                strSqlString.Append("           WHERE 1=1" + "\n");
                strSqlString.Append("           AND A.CREATE_DATE BETWEEN  TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')- 30,'YYYYMMDD')" + "AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+ 21 " + ",'YYYYMMDD') " + "\n");
                strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
                strSqlString.Append("           AND A.CREATE_DATE = B.CREATE_DATE " + "\n");
                strSqlString.Append("           AND A.PO_NUMBER = B.PO_NUMBER " + "\n");
                strSqlString.Append("           AND A.PO_NUMBER_SEQ = B.PO_SEQ " + "\n");
                strSqlString.Append("         ) C " + "\n");
                strSqlString.Append("             GROUP BY C.MAT_ID " + "\n");
                strSqlString.Append("         ) F " + "\n");
            }
            strSqlString.Append(" WHERE 1=1" + "\n");
            strSqlString.Append("   AND A.MAT_ID = B.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = C.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = D.MAT_ID(+)" + "\n");
            strSqlString.Append("   AND A.MAT_ID = E.MAT_ID(+)" + "\n");

            if(GlobalVariable.gsGlovalSite == "K1")
                strSqlString.Append("   AND A.MAT_ID = F.MAT_ID(+)" + "\n");

            strSqlString.Append("   AND A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'" + "\n");

            if (txtMatCode.Text != "%")
                strSqlString.Append("   AND A.MAT_ID LIKE '" + txtMatCode.Text + "'" + "\n");

            if (cdvMatType.Text != "ALL" && cdvMatType.Text != "")
                strSqlString.AppendFormat("   AND A.MAT_TYPE " + cdvMatType.SelectedValueToQueryString + "\n");

            strSqlString.Append(" GROUP BY A.MAT_TYPE, A.MAT_ID, A.MAT_DESC, A.UNIT_1" + "\n");
            strSqlString.Append(" HAVING SUM(NVL(B.INV_QTY,0)) + SUM(NVL(B.INV_L_QTY,0)) + SUM(NVL(C.LOT_TTL,0)) + SUM(NVL(D.ORDER_QTY,0)) + SUM(NVL(E.WIK_WIP,0)) > 0" + "\n");
            strSqlString.Append(" ORDER BY A.MAT_TYPE, A.MAT_ID " + "\n");
            //strSqlString.Append(" HAVING SUM(INV_QTY) + SUM(L_IN) > 0" + "\n");                        

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();
        }

        #endregion

        #region " Event 처리 "

        #region " spdData_CellClick : 주차 입고계획 수량 cell선택시 "
        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            String week = spdData.ActiveSheet.Columns[e.Column].Label;
            // 설비대수 클릭 시 팝업 창 띄움.
            if (e.Row > 0 && (week == "W1" || week == "W2" || week == "W3") && spdData.ActiveSheet.Cells[e.Row, 0].Text.IndexOf("Total") == -1 && GlobalVariable.gsGlovalSite == "K1")
            {
                DataTable dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlDetail(week, spdData.ActiveSheet.Cells[e.Row, 1].Text));

                if (dt != null && dt.Rows.Count > 0)
                {
                    string[] dayArry = new string[7];
                    int weekDayCount = Convert.ToInt32(week.Substring(1, 1)) * 7;
                    for (int i = 0; i < 7; i++)
                    {
                        dayArry[i] = cdvDate.Value.AddDays(weekDayCount - 7 + i + 1).ToString("MMdd");
                    }
                    System.Windows.Forms.Form frm = new MAT070501_P1(week, dayArry, dt);
                    frm.ShowDialog();
                }
            }



        }
        #endregion

        #region MakeSqlDetail

        private string MakeSqlDetail(string Week, String MAT_ID)
        {
            StringBuilder strSqlString = new StringBuilder();
            strSqlString.Append("           SELECT * FROM ( " + "\n");
            strSqlString.Append("           SELECT " + "\n");

            if (Week.Equals("W1"))
            {
                strSqlString.Append("           A.MAT_ID," + "\n");
                //D1~D7
                for (int j = 0; j < 7; j++)
                {
                    strSqlString.Append("           CASE " + "\n");
                    for (int i = 0; i > -30; i--)
                    {
                        strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE THEN D" + (-i + 1 + j).ToString().PadLeft(3, '0') + "\n");
                    }
                    if (j != 6 )
                    {
                        for (int b = 0; b < j ; b++)
                        {
                            strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + (b + 1) + ",'YYYYMMDD') = A.CREATE_DATE   THEN  ");
                            strSqlString.Append("           D" + (j - b).ToString().PadLeft(3, '0') + " \n");
                        }
                    }
                    strSqlString.Append("           ELSE 0  END D"+(j+1));
                    if(j!=6)strSqlString.Append("           , \n");
                }
            }
            else if (Week.Equals("W2"))
            {
                strSqlString.Append("           A.MAT_ID," + "\n");
                //D8~D14
                for (int j = 0; j < 7; j++)
                {
                    strSqlString.Append("           CASE " + "\n");
                    for (int i = 0; i > -30; i--)
                    {
                        strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE THEN D" + (-i + 8 + j).ToString().PadLeft(3, '0') + "\n");
                    }
                    for (int b = 0; b < 7; b++)
                    {
                        strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + (b + 1) + ",'YYYYMMDD') = A.CREATE_DATE   THEN  ");
                        strSqlString.Append("           D" + (7 - b + j).ToString().PadLeft(3, '0') + " \n");
                    }
                    if (j != 6)
                    {
                        for (int b = 0; b < j; b++)
                        {
                            strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + (b + 8) + ",'YYYYMMDD') = A.CREATE_DATE   THEN  ");
                            strSqlString.Append("           D" + (j - b).ToString().PadLeft(3, '0') + " \n");
                        }
                    }
                    strSqlString.Append("           ELSE 0  END D" + (j + 1));
                    if (j != 6) strSqlString.Append("           , \n");
                }

            }
            else if (Week.Equals("W3"))
            {
                strSqlString.Append("           A.MAT_ID," + "\n");
                //D15~D21
                for (int j = 0; j < 7; j++)
                {
                    strSqlString.Append("           CASE " + "\n");
                    for (int i = 0; i > -30; i--)
                    {
                        strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + i + ",'YYYYMMDD') = A.CREATE_DATE THEN D" + (-i + 15 + j).ToString().PadLeft(3, '0') + "\n");
                    }
                    for (int b = 0; b < 14; b++)
                    {
                        strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + (b + 1) + ",'YYYYMMDD') = A.CREATE_DATE   THEN  ");
                        strSqlString.Append("           D" + (14 - b + j).ToString().PadLeft(3, '0') + " \n");
                    }
                    if (j != 6)
                    {
                        for (int b = 0; b < j; b++)
                        {
                            strSqlString.Append("           WHEN TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+" + (b + 15) + ",'YYYYMMDD') = A.CREATE_DATE   THEN  ");
                            strSqlString.Append("           D" + (j - b).ToString().PadLeft(3, '0') + " \n");
                        }
                    }
                    strSqlString.Append("           ELSE 0  END D" + (j + 1));
                    if (j != 6) strSqlString.Append("           , \n");
                }
            }
            strSqlString.Append("           FROM ISAPWMSPLN A,(SELECT MAT_ID,MAX(CREATE_DATE) CREATE_DATE,PO_NUMBER,MAX(PO_NUMBER_SEQ) PO_SEQ FROM ISAPWMSPLN WHERE MAT_ID = '" + MAT_ID + "' GROUP BY PO_NUMBER,MAT_ID) B" + "\n");
            strSqlString.Append("           WHERE 1=1" + "\n");
            strSqlString.Append("           AND A.CREATE_DATE BETWEEN  TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')- 30,'YYYYMMDD')" + "AND TO_CHAR(TO_DATE('" + cdvDate.SelectedValue() + "','YYYYMMDD')+ 21 " + ",'YYYYMMDD') " + "\n");
            strSqlString.Append("           AND A.MAT_ID = B.MAT_ID " + "\n");
            strSqlString.Append("           AND A.CREATE_DATE = B.CREATE_DATE " + "\n");
            strSqlString.Append("           AND A.PO_NUMBER = B.PO_NUMBER " + "\n");
            strSqlString.Append("           AND A.PO_NUMBER_SEQ = B.PO_SEQ  " + "\n");
            strSqlString.Append("           ) WHERE (D1+D2+D3+D4+D5+D6+D7)>0 " + "\n");
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }
        #endregion

        #region " btnView_Click : View버튼을 선택했을 때 "

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

                //spdData.DataSource = dt;

                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 4, null, null);

                //Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 4, 0, 1, true, Align.Center, VerticalAlign.Center);

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

        #endregion



        #region " btnExcelExport_Click : Excel로 내보내기 "

        /// <summary>
        /// Excel Export
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null, true);
            spdData.ExportExcel();
        }

        #endregion

        #endregion

        private void cdvMatCode_ValueButtonPress(object sender, EventArgs e)
        {
            string strquery = string.Empty;
            strquery = "SELECT MAT_ID, MAT_TYPE FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_TYPE = '" + cdvMatType.Text.Trim() + "' AND DELETE_FLAG = ' ' ORDER BY MAT_ID";

            cdvMatCode.sDynamicQuery = strquery;

        }

        private void cdvMatType_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvMatCode.Text = "";
        }

    }
}
