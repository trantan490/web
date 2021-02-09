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

namespace Hana.CUS
{
    public partial class CUS060101 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: CUS060101<br/>
        /// 클래스요약: 고객사 WIP<br/>
        /// 작  성  자: 미라콤 김민규<br/>
        /// 최초작성일: 2008-10-09<br/>
        /// 상세  설명: 고객사 WIP<br/>
        /// 변경  내용: Wafer Qty 표시_고객사 FC만 WF_QTY 표시 됨.(임종우)<br/>
        /// 2009-07-17-임종우 : Lot 정보 표시 유무 설정<br/>
        /// 2009-07-17-임종우 : ADMIN 사용자 접속시 Customer 선택 가능 하도록
        /// 2010-05-03-임종우 : Wafer 수량을 Create 수량에서 Stock 재공 수량으로 변경(김동인 요청)<br/>
        /// 2010-09-08-임종우 : 크루셜텍 고객사 접속시 HMKE1, FGS 표시 안되도록 변경(김태완D 요청)<br/>
        /// 2013-01-28-임종우 : 고객사를 제외한 사용자 접속 시 업체 선택 가능하도록 수정
        /// 2014-06-20-임종우 : 고객사 TYPE 추가 - 실리콘웍스 (최지하D 요청)
        /// 2016-02-18-임종우 : 공정 순서 변경 V/I 가 SIG 뒤에 표시
        /// 2016-10-31-임종우 : 공정그룹순서에 따라 가져오도록 신규쿼리 작성
        /// 2016-11-24-임종우 : Bump Factory 추가
        /// </summary>
         
        DataTable global_oper_Assy = null;
        DataTable global_oper_Test = null;
        DataTable global_oper_Probe = null;
        DataTable global_oper_Fgs = null;
        DataTable global_oper_Bump = null;

        public CUS060101()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit(); //헤더 한줄짜리 샘플
            
            rdoProd.Checked = true;
            cdvDate.Value = DateTime.Now;

            // 2013-01-28-임종우 : 고객사를 제외한 사용자 접속 시 업체 선택 가능하도록 수정            
            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "PRODUCTION_MANAGER")
            {
                this.udcWIPCondition1.Enabled = true;
            }
            else
            {
                this.udcWIPCondition1.Enabled = false;
            }
            //this.udcWIPCondition2.Enabled = false;
            //this.udcWIPCondition3.Enabled = false;
            //this.udcWIPCondition4.Enabled = false;
            //this.udcWIPCondition5.Enabled = false;
            //this.udcWIPCondition6.Enabled = false;
            //this.udcWIPCondition7.Enabled = false;
            //this.udcWIPCondition8.Enabled = false;           
        }
        
        private Boolean CheckField()
        {
            return true;            
        }

        private bool CheckFactory(string SelFactory)
        {
            string[] factory = cdvFactory.txtValue.Split(',');

            for(int i=0; i<factory.Length;i++)
            {
                if (factory[i].Trim().ToString() == SelFactory)
                    return true;
            }

            return false;
        }

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

            string strDate = cdvDate.Value.ToString("yyyyMMdd");
            bool isRealTime = false;
            if (strDate.Equals(DateTime.Now.ToString("yyyyMMdd")))  // 실시간, 과거 시점 체크
            {                
                isRealTime = true;
            }
            else
            {             
                strDate = strDate + "22";
                isRealTime = false;
            }
                       


                        
            if (rdoProd.Checked == true)   //***********Radio Button 체크 유무에 따른 1번째 쿼리문**************
            {
                #region Product
                strSqlString.AppendFormat(" SELECT {0} " + "\n", QueryCond3);
                strSqlString.Append("      , WF_QTY " + "\n");

                if (CheckFactory("ALL") == true || CheckFactory("HMKB1") == true)     // ALL or BUMP
                {
                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        strSqlString.Append("      , B" + i + "\n");
                    }

                    strSqlString.Append("      , S0" + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory("HMKE1") == true)     // ALL or Probe
                {
                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        strSqlString.Append("      , P" + i + "\n");
                    }

                    strSqlString.Append("      , S1" + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)     // ALL or ASSY
                {
                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        strSqlString.Append("      , A" + i + "\n");
                    }

                    strSqlString.Append("      , S2" + "\n");
                }


                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)     // ALL or TEST
                {
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        strSqlString.Append("      , T" + i + "\n");
                    }

                    strSqlString.Append("      , S3" + "\n");
                }

              
                if (CheckFactory("ALL") == true || CheckFactory("FGS") == true)     // ALL or FGS
                {
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        strSqlString.Append("      , F" + i + "\n");
                    }

                    strSqlString.Append("      , S4" + "\n");
                }

              
                if (CheckFactory("ALL") == true)
                {
                    strSqlString.Append("      , TOTAL" + "\n");
                }
                
                strSqlString.Append("   FROM (  " + "\n");

                strSqlString.AppendFormat("    SELECT {0} " + "\n", QueryCond2);

                // 2010-05-03-임종우 : Wafer 수량을 Create 수량에서 Stock 재공 수량으로 변경(김동인 요청)
                //strSqlString.Append("         , SUM(A.CREATE_QTY_2) WF_QTY " + "\n");

                strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND C.OPER_GRP_1 = 'HMK2A'  " + "\n");
                strSqlString.Append("                THEN QTY_2 " + "\n");
                strSqlString.Append("                ELSE 0 END) WF_QTY " + "\n");

                if (CheckFactory("ALL") == true || CheckFactory("HMKB1") == true)
                {
                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKB1' AND C.OPER_GRP_1 = '" + global_oper_Bump.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) B" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKB1'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S0 " + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory("HMKE1") == true)
                {
                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKE1' AND C.OPER = '" + global_oper_Probe.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) P" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKE1'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S1 " + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
                {
                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND C.OPER_GRP_1 = '" + global_oper_Assy.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) A" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND C.OPER NOT IN ('00001', '00002') " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S2 " + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
                {
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND C.OPER_GRP_1 = '" + global_oper_Test.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) T" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S3 " + "\n");
                }
                
                if (CheckFactory("ALL") == true || CheckFactory("FGS") == true)
                {
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'FGS' AND C.OPER = '" + global_oper_Fgs.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) F" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'FGS'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S4 " + "\n");
                }         

                strSqlString.Append("         , SUM(CASE WHEN A.FACTORY " + cdvFactory.SelectedValueToQueryString + " AND C.OPER NOT IN ('00001', '00002') " + "\n");
                strSqlString.Append("                THEN QTY_1 " + "\n");
                strSqlString.Append("                ELSE 0 END) TOTAL " + "\n");
                       
                if(isRealTime == true)
                {
                    strSqlString.Append("      FROM RWIPLOTSTS A  " + "\n");
                }
                else
                {
                    strSqlString.Append("      FROM RWIPLOTSTS_BOH A  " + "\n");
                }
                
                strSqlString.Append("         , MWIPMATDEF B  " + "\n");
                strSqlString.Append("         , MWIPOPRDEF C  " + "\n");

                strSqlString.Append("     WHERE A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");

                strSqlString.Append("       AND A.FACTORY = B.FACTORY  " + "\n");
                strSqlString.Append("       AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.Append("       AND A.OPER = C.OPER    " + "\n");
                strSqlString.Append("       AND A.MAT_ID = B.MAT_ID  " + "\n");
                strSqlString.Append("       AND A.MAT_VER = 1 " + "\n");
                strSqlString.Append("       AND A.MAT_VER = B.MAT_VER " + "\n");
                strSqlString.Append("       AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("       AND A.LOT_TYPE = 'W'  " + "\n");

                if (isRealTime == false)
                {
                    strSqlString.Append("       AND A.CUTOFF_DT = '" + strDate + "'" + "\n");
                }

                //if (txtRunID.Text != "%" || txtRunID.Text != "")
                //{
                //    strSqlString.Append("       AND A.LOT_CMF_4 LIKE '" + txtRunID.Text.Trim() + "' " + "\n");
                //}


                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "PRODUCTION_MANAGER")
                {
                    udcWIPCondition1.Enabled = true;

                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                }
                else
                {
                    udcWIPCondition1.Enabled = false;

                    strSqlString.Append("       AND A.LOT_CMF_2 = '" + GlobalVariable.gsCustomer + "' " + "\n");

                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("       AND B.MAT_GRP_1 = '{0}' " + "\n", GlobalVariable.gsCustomer);
                }
                
                //상세 조회에 따른 SQL문 생성      

                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                strSqlString.AppendFormat("  GROUP BY {0} " + "\n", QueryCond2);
                strSqlString.Append("   )  " + "\n");

                strSqlString.Append("WHERE 1=1" + "\n");
                strSqlString.Append("  AND NVL(TOTAL,0) > 0" + "\n");

                if (CheckFactory("ALL") == true)    //     ALL
                {
                    strSqlString.Append("  AND NVL(TOTAL,0) > 0" + "\n");
                }
                else if (CheckFactory("HMKB1") == true)      // TEST
                {
                    strSqlString.Append("  AND NVL(S0,0) > 0" + "\n");
                }
                else if (CheckFactory("HMKE1") == true)      // PROBE
                {                    
                    strSqlString.Append("  AND NVL(S1,0) > 0" + "\n");                    
                }
                else if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)      // ASSY
                {
                    strSqlString.Append("  AND NVL(S2,0) > 0" + "\n");
                }
                else if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)      // TEST
                {
                    strSqlString.Append("  AND NVL(S3,0) > 0" + "\n");
                }                
                else if (CheckFactory("FGS") == true)      // FGS
                {                   
                    strSqlString.Append("  AND NVL(S4,0) > 0" + "\n");                   
                }


                strSqlString.AppendFormat("  ORDER BY {0} " + "\n", QueryCond1);
                #endregion
            }
            else if(rdoLot.Checked == true)    //*************Radio 버튼 체크 유무에 의한 2번째 쿼리문*************
            {
                #region Lot
                if (checkLot.Checked == true)
                {
                    strSqlString.AppendFormat(" SELECT {0}, LOT_CMF_4, LOT_ID " + "\n", QueryCond3);
                }
                else
                {
                    strSqlString.AppendFormat(" SELECT {0}, LOT_CMF_4 " + "\n", QueryCond3);
                }

                strSqlString.Append("      , WF_QTY " + "\n");

                if (CheckFactory("ALL") == true || CheckFactory("HMKB1") == true)     // ALL or TEST
                {
                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        strSqlString.Append("      , B" + i + "\n");
                    }

                    strSqlString.Append("      , S0" + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory("HMKE1") == true)     // ALL or PROBE
                {
                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        strSqlString.Append("      , P" + i + "\n");
                    }

                    strSqlString.Append("      , S1" + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)     // ALL or ASSY
                {
                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        strSqlString.Append("      , A" + i + "\n");
                    }

                    strSqlString.Append("      , S2" + "\n");
                }


                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)     // ALL or TEST
                {
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        strSqlString.Append("      , T" + i + "\n");
                    }

                    strSqlString.Append("      , S3" + "\n");
                }

                
                if (CheckFactory("ALL") == true || CheckFactory("FGS") == true)     // ALL or FGS
                {
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        strSqlString.Append("      , F" + i + "\n");
                    }

                    strSqlString.Append("      , S4" + "\n");
                }
                

                if (CheckFactory("ALL") == true)
                {
                    strSqlString.Append("      , TOTAL" + "\n");
                }

                // 2014-06-20-임종우 : 실리콘웍스 고객사 Type 추가
                if (checkLot.Checked == true)
                {
                    strSqlString.Append("      , CUST_TYPE" + "\n");
                }

                strSqlString.Append("   FROM (  " + "\n");

                if (checkLot.Checked == true)
                {
                    strSqlString.AppendFormat("    SELECT {0}, LOT_CMF_4, LOT_ID  " + "\n", QueryCond2);
                }
                else
                {
                    strSqlString.AppendFormat("    SELECT {0}, LOT_CMF_4  " + "\n", QueryCond2);
                }

                strSqlString.Append("         , SUM(A.CREATE_QTY_2) WF_QTY " + "\n");

                if (CheckFactory("ALL") == true || CheckFactory("HMKB1") == true)
                {
                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKB1' AND C.OPER_GRP_1 = '" + global_oper_Bump.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) B" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKB1'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S0 " + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory("HMKE1") == true)
                {
                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKE1' AND C.OPER = '" + global_oper_Probe.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) P" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'HMKE1'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S1 " + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
                {
                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND C.OPER_GRP_1 = '" + global_oper_Assy.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) A" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND C.OPER NOT IN ('00001', '00002') " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S2 " + "\n");
                }

                if (CheckFactory("ALL") == true || CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
                {
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' AND C.OPER_GRP_1 = '" + global_oper_Test.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) T" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S3 " + "\n");
                }
                                
                if (CheckFactory("ALL") == true || CheckFactory("FGS") == true)
                {
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'FGS' AND C.OPER_GRP_1 = '" + global_oper_Fgs.Rows[i][0] + "'  " + "\n");
                        strSqlString.Append("                THEN QTY_1 " + "\n");
                        strSqlString.Append("                ELSE 0 END) F" + i + "\n");
                    }

                    strSqlString.Append("         , SUM(CASE WHEN A.FACTORY = 'FGS'  " + "\n");
                    strSqlString.Append("                THEN QTY_1 " + "\n");
                    strSqlString.Append("                ELSE 0 END) S4 " + "\n");
                }
                
                //strSqlString.Append("         , SUM(CASE WHEN A.FACTORY IN ('" + GlobalVariable.gsTestDefaultFactory + "', '" + GlobalVariable.gsAssyDefaultFactory + "', 'HMKE1', 'FGS', 'HMKB1') AND C.OPER NOT IN ('00001', '00002') " + "\n");
                strSqlString.Append("         , SUM(CASE WHEN A.FACTORY " + cdvFactory.SelectedValueToQueryString + " AND C.OPER NOT IN ('00001', '00002') " + "\n");
                strSqlString.Append("                THEN QTY_1 " + "\n");
                strSqlString.Append("                ELSE 0 END) TOTAL " + "\n");

                // 2014-06-20-임종우 : 실리콘웍스 고객사 Type 추가
                if (checkLot.Checked == true)
                {
                    strSqlString.Append("         , MAX(MESMGR.F_GET_ATTR_VALUE_FAC@RPTTOMES(A.LOT_ID, 'LOT_GCT', 'CUST_LOT_TYPE', A.FACTORY)) AS CUST_TYPE" + "\n");
                }

                if (isRealTime == true)
                {
                    strSqlString.Append("      FROM RWIPLOTSTS A  " + "\n");
                }
                else
                {
                    strSqlString.Append("      FROM RWIPLOTSTS_BOH A  " + "\n");
                }

                strSqlString.Append("         , MWIPMATDEF B  " + "\n");
                strSqlString.Append("         , MWIPOPRDEF C  " + "\n");

                strSqlString.Append("     WHERE A.FACTORY " + cdvFactory.SelectedValueToQueryString + "\n");                

                strSqlString.Append("       AND A.FACTORY = B.FACTORY  " + "\n");
                strSqlString.Append("       AND A.FACTORY = C.FACTORY " + "\n");
                strSqlString.Append("       AND A.OPER = C.OPER    " + "\n");
                strSqlString.Append("       AND A.MAT_ID = B.MAT_ID  " + "\n");
                strSqlString.Append("       AND A.MAT_VER = 1 " + "\n");
                strSqlString.Append("       AND A.MAT_VER = B.MAT_VER " + "\n");
                strSqlString.Append("       AND A.LOT_DEL_FLAG = ' '  " + "\n");
                strSqlString.Append("       AND A.OWNER_CODE = 'PROD' " + "\n");
                strSqlString.Append("       AND A.LOT_TYPE = 'W'  " + "\n");


                if (isRealTime == false)
                {
                    strSqlString.Append("       AND A.CUTOFF_DT = '" + strDate + "'" + "\n");
                }

                if(txtRunID.Text.Trim() != "%" || txtRunID.Text.Trim() != "")
                {
                    strSqlString.Append("       AND A.LOT_CMF_4 LIKE '" + txtRunID.Text.Trim() + "' " + "\n");
                }

                if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "OPERATOR_GERNERAL" || GlobalVariable.gsUserGroup == "PRODUCTION_MANAGER")
                {
                    udcWIPCondition1.Enabled = true;

                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("       AND B.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);
                }
                else
                {
                    udcWIPCondition1.Enabled = false;

                    strSqlString.Append("       AND A.LOT_CMF_2 = '" + GlobalVariable.gsCustomer + "' " + "\n");

                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                        strSqlString.AppendFormat("       AND B.MAT_GRP_1 = '{0}' " + "\n", GlobalVariable.gsCustomer);
                }

                //상세 조회에 따른 SQL문 생성                                        
                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                    strSqlString.AppendFormat("       AND B.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                if (checkLot.Checked == true)
                {
                    strSqlString.AppendFormat("  GROUP BY {0}, LOT_CMF_4, LOT_ID  " + "\n", QueryCond2);
                    strSqlString.Append("   )  " + "\n");
                    strSqlString.AppendFormat("  ORDER BY {0}, LOT_CMF_4, LOT_ID  " + "\n", QueryCond1);
                }
                else
                {
                    strSqlString.AppendFormat("  GROUP BY {0}, LOT_CMF_4  " + "\n", QueryCond2);
                    strSqlString.Append("   )  " + "\n");
                    strSqlString.AppendFormat("  ORDER BY {0}, LOT_CMF_4 " + "\n", QueryCond1);
                }
                #endregion
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            }

            return strSqlString.ToString();

        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();
            
            strSqlString.Append("SELECT OPER_GRP_1, MIN(OPER)  " + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsTestDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND OPER <> 'T0540' " + "\n");
            strSqlString.Append(" GROUP BY OPER_GRP_1  " + "\n");
            strSqlString.Append(" ORDER BY MIN(OPER) ASC " + "\n");
                        
            return strSqlString.ToString();
        }


        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            // 2016-10-31-임종우 : 공정그룹순서에 따라 가져오도록 신규쿼리 작성
            strSqlString.Append("SELECT OPER_GRP_1, TO_NUMBER(OPER_CMF_4) " + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append(" WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("   AND OPER LIKE 'A%'  " + "\n");
            strSqlString.Append("   AND OPER NOT IN ('A1120', 'A1130')  " + "\n");
            strSqlString.Append("   AND OPER_GRP_1 <> '-'  " + "\n");
            strSqlString.Append("   AND OPER_CMF_5 <> 'Y' " + "\n");
            strSqlString.Append(" GROUP BY OPER_GRP_1, OPER_CMF_4 " + "\n");
            strSqlString.Append(" ORDER BY TO_NUMBER(OPER_CMF_4) " + "\n");

            //strSqlString.Append("              SELECT OPER_GRP_1, MIN(OPER) " + "\n");
            //strSqlString.Append("                FROM MWIPOPRDEF  " + "\n");
            //strSqlString.Append("               WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            //strSqlString.Append("                 AND OPER NOT IN ('00001', '00002')  " + "\n");

            //// 2016-02-18-임종우 : V/I 를 SIG 뒤로 보내기 위해 하기 공정 제외..어차피 공정그룹으로 재공을 표시하기에 문제 안됨.
            //strSqlString.Append("                 AND OPER NOT IN ('A1120', 'A1130')  " + "\n");
            //strSqlString.Append("            GROUP BY OPER_GRP_1 " + "\n");
            //strSqlString.Append("            ORDER BY MIN(OPER) " + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString3()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER_GRP_1, MIN(OPER) " + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append(" WHERE FACTORY = 'HMKE1' " + "\n");
            strSqlString.Append("   AND OPER_GRP_1 <> '-' " + "\n");
            strSqlString.Append(" GROUP BY OPER_GRP_1  " + "\n");
            strSqlString.Append(" ORDER BY MIN(OPER) ASC " + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString4()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER_DESC " + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append(" WHERE FACTORY = 'FGS' " + "\n");
            strSqlString.Append("   AND OPER <> 'F000N' " + "\n");
            strSqlString.Append(" ORDER BY OPER " + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString5()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("SELECT OPER_GRP_1, MIN(OPER)  " + "\n");
            strSqlString.Append("  FROM MWIPOPRDEF  " + "\n");
            strSqlString.Append(" WHERE FACTORY = 'HMKB1' " + "\n");
            strSqlString.Append("   AND OPER LIKE 'B%' " + "\n");
            strSqlString.Append("   AND OPER_GRP_1 <> '-' " + "\n");
            strSqlString.Append(" GROUP BY OPER_GRP_1  " + "\n");
            strSqlString.Append(" ORDER BY MIN(OPER) ASC " + "\n");

            return strSqlString.ToString();
        }

        private void Sample02_Load(object sender, EventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (CheckField() == false)
                return;

            DataTable dt = null;
            GridColumnInit();

            try
            {
                spdData_Sheet1.RowCount = 0;
                this.Refresh();
                LoadingPopUp.LoadIngPopUpShow(this);
                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                ////by John (한줄짜리)
                ////1.Griid 합계 표시
                int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, 1, 8, null, null, btnSort);
                // 토탈 시작할 셀넘버, 시작 부터 서브토탈 몇개 쓸건지, 첫셀부터 몇번째 셀까지 TOTAL 적용안함


                ////2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 9;

                ////3. Total부분 셀머지
                spdData.RPT_FillDataSelectiveCells("Total", 0, 9, 0, 1, true, Align.Center, VerticalAlign.Center);

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

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            //ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
            spdData.ExportExcel();
        }

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
           
        }


        //한줄짜리 해더 샘플
        private void GridColumnInit()
        {
            spdData.RPT_ColumnInit();
            spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Family", 0, 1, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Package", 0, 2, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 60);
            spdData.RPT_AddBasicColumn("Type1", 0, 3, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Type2", 0, 4, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("LD Count", 0, 5, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Density", 0, 6, Visibles.False, Frozen.True, Align.Left, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Generation", 0, 7, Visibles.False, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
            spdData.RPT_AddBasicColumn("Product", 0, 8, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);

            if (rdoProd.Checked == true)
            {
                if (GlobalVariable.gsCustomer.Equals("FC") || GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    spdData.RPT_AddBasicColumn("Wf Qty", 0, 9, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Wf Qty", 0, 9, Visibles.False, Frozen.True, Align.Right, Merge.True, Formatter.String, 80);
                }

                if (CheckFactory("ALL") == true)
                {
                    global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                    global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());
                    global_oper_Probe = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());
                    global_oper_Fgs = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4());
                    global_oper_Bump = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString5());

                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Bump.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Bump Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Probe.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Probe Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Assy Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Test Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                                       
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Fgs.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("FGS Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                   

                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                }

                if (CheckFactory("HMKB1") == true)
                {
                    global_oper_Bump = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString5());


                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Bump.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Bump Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }

                if (CheckFactory("HMKE1") == true)
                {
                    global_oper_Probe = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());
                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Probe.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Probe Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                } 

                if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
                {
                    global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Assy Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                
                if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
                {
                    global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());


                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Test Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                                
                if (CheckFactory("FGS") == true)
                {
                    global_oper_Fgs = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4());
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Fgs.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("FGS Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
            }
            else if (rdoLot.Checked == true)
            {                         
                spdData.RPT_AddBasicColumn("Run ID", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);

                // 체크박스 선택에 의한 Lot 표시 설정. 기본으로 Lot 표시 (209.07.17 임종우)
                if (checkLot.Checked == true)
                {
                    spdData.RPT_AddBasicColumn("Lot ID", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                }

                if (GlobalVariable.gsCustomer.Equals("FC") || GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                {
                    spdData.RPT_AddBasicColumn("Wf Qty", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.True, Align.Right, Merge.True, Formatter.String, 80);
                }
                else
                {
                    spdData.RPT_AddBasicColumn("Wf Qty", 0, spdData.ActiveSheet.Columns.Count, Visibles.False, Frozen.True, Align.Right, Merge.True, Formatter.String, 80);
                }

                if (CheckFactory("ALL") == true)
                {
                    global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                    global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());
                    global_oper_Probe = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());
                    global_oper_Fgs = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4());
                    global_oper_Bump = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString5());

                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Bump.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Bump Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Probe.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Probe Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Assy Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Test Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                                       
                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Fgs.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("FGS Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                   
                    
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                }

                if (CheckFactory("HMKB1") == true)
                {
                    global_oper_Bump = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString5());

                    for (int i = 0; i < global_oper_Bump.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Bump.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Bump Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }

                if (CheckFactory("HMKE1") == true)
                {
                    global_oper_Probe = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString3());

                    for (int i = 0; i < global_oper_Probe.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Probe.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Probe Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }

                if (CheckFactory(GlobalVariable.gsAssyDefaultFactory) == true)
                {
                    global_oper_Assy = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());

                    for (int i = 0; i < global_oper_Assy.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Assy.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Assy Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
                
                if (CheckFactory(GlobalVariable.gsTestDefaultFactory) == true)
                {
                    global_oper_Test = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());
                    
                    for (int i = 0; i < global_oper_Test.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Test.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("Test Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }                
                
                if (CheckFactory("FGS") == true)
                {
                    global_oper_Fgs = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString4());

                    for (int i = 0; i < global_oper_Fgs.Rows.Count; i++)
                    {
                        spdData.RPT_AddBasicColumn(global_oper_Fgs.Rows[i][0].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 80);
                    }
                    spdData.RPT_AddBasicColumn("FGS Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                    spdData.RPT_AddBasicColumn("Total", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                }
            }

            // 2014-06-20-임종우 : 실리콘웍스 고객사 Type 추가
            if (rdoLot.Checked == true && checkLot.Checked == true)
            {
                spdData.RPT_AddBasicColumn("CUST TYPE", 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 70);
            }

            spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.            
        }


        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Customer", "MAT_GRP_1", "B.MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Family", "MAT_GRP_2", "B.MAT_GRP_2", "MAT_GRP_2", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Package", "MAT_GRP_3", "B.MAT_GRP_3", "MAT_GRP_3", true);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type1", "MAT_GRP_4", "B.MAT_GRP_4", "MAT_GRP_4", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Type2", "MAT_GRP_5", "B.MAT_GRP_5", "MAT_GRP_5", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD Count", "MAT_GRP_6", "B.MAT_GRP_6", "MAT_GRP_6", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Density", "MAT_GRP_7", "B.MAT_GRP_7", "MAT_GRP_7", false);
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Generation", "MAT_GRP_8", "B.MAT_GRP_8", "MAT_GRP_8", false);            
            ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Product", "MAT_CMF_7", "B.MAT_CMF_7", "MAT_CMF_7", true);         
        }

        private void rdoLot_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoLot.Checked == true)
            {
                txtRunID.Enabled = true;
                txtRunID.Clear();
                txtRunID.Text = "%";
            }
            else
            {
                txtRunID.Enabled = false;
                txtRunID.Clear();
                txtRunID.Text = "%";
            }
           
        }

        private void rdoProd_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoLot.Checked == true)
            {
                txtRunID.Enabled = true;
                txtRunID.Clear();
                txtRunID.Text = "%";

                checkLot.Enabled = true;
            }
            else
            {
                txtRunID.Enabled = false;
                txtRunID.Clear();
                txtRunID.Text = "%";

                checkLot.Enabled = false;
            }
        }

    }
}


