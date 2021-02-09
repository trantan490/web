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
    public partial class PRD011008 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        /// <summary>
        /// 클  래  스: PRD011008<br/>
        /// 클래스요약: Chip Arrange현황 <br/>
        /// 작  성  자: THE 손광섭<br/>
        /// 최초작성일: 2013-06-20<br/>
        /// 상세  설명: 칩기준 D/A W/B공정세분화[1,2,3,4차].<br/>
        /// 변경  내용: <br/>
        /// </summary>
        /// 
        List<UnitPKG> lstDataset    = new List<UnitPKG>();
        List<string>  lstKeyRes     = new List<string>();
        List<string>  lstKeyCap     = new List<string>();
        List<string>  lstKeyChip    = new List<string>();
        List<string>  lstKeyMerge   = new List<string>();
        List<string>  lstKeyPFM     = new List<string>();

        Color colorTotal        = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(233)), ((System.Byte)(204)));
        Color colorSubTotal     = System.Drawing.Color.FromArgb(((System.Byte)(222)), ((System.Byte)(236)), ((System.Byte)(242)));	
        Color colorFixedColumn  = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(218)));


        class UnitPKG
        {
            public string strCUSTOMER   = "";
            public string strMAT_GRP_1  = "";
            public string strMAT_GRP_9  = "";
            public string strMAT_GRP_10 = "";

            // Key값 
            // DA01,DA02,DA03,DA04,DA05,TTL   [DA01 : DA1차CHIP 컬럼 ...]
            // WB01,WB02,WB03,WB04,WB05,TTL   [WB03 : WB3차CHIP 컬럼 ...] 
            public Dictionary<string, long> dicRes                  = new Dictionary<string, long>();
            public Dictionary<string, long> dicCapa                 = new Dictionary<string, long>();
            public Dictionary<string, long> dicLotChip              = new Dictionary<string, long>();
            public Dictionary<string, long> dicLotMerge             = new Dictionary<string, long>();
            public Dictionary<string, long> dicPerformance          = new Dictionary<string, long>();
            public Dictionary<string, long> dicForcastPerformance   = new Dictionary<string, long>();
        }

        public PRD011008()
        {
            InitializeComponent();
            SortInit();
            GridColumnInit();

            //쿼리문 컬럼명과 동일
            //한줄의 출력 Seqence
            lstKeyRes.Add("RES_DA01");
            lstKeyRes.Add("RES_WB01");
            lstKeyRes.Add("RES_DA02");
            lstKeyRes.Add("RES_WB02");
            lstKeyRes.Add("RES_DA03");
            lstKeyRes.Add("RES_WB03");
            lstKeyRes.Add("RES_DA04");
            lstKeyRes.Add("RES_WB04");
            lstKeyRes.Add("RES_DA05");
            lstKeyRes.Add("RES_WB05");

            lstKeyCap.Add("CAP_DA01");
            lstKeyCap.Add("CAP_WB01");
            lstKeyCap.Add("CAP_DA02");
            lstKeyCap.Add("CAP_WB02");
            lstKeyCap.Add("CAP_DA03");
            lstKeyCap.Add("CAP_WB03");
            lstKeyCap.Add("CAP_DA04");
            lstKeyCap.Add("CAP_WB04");
            lstKeyCap.Add("CAP_DA05");
            lstKeyCap.Add("CAP_WB05");

            lstKeyChip.Add("DA01_CHIP");
            lstKeyChip.Add("WB01_CHIP");
            lstKeyChip.Add("DA02_CHIP");
            lstKeyChip.Add("WB02_CHIP");
            lstKeyChip.Add("DA03_CHIP");
            lstKeyChip.Add("WB03_CHIP");
            lstKeyChip.Add("DA04_CHIP");
            lstKeyChip.Add("WB04_CHIP");
            lstKeyChip.Add("DA05_CHIP");
            lstKeyChip.Add("WB05_CHIP");
            lstKeyChip.Add("DA_CHIP_Etc");
            lstKeyChip.Add("WB_CHIP_Etc");

            lstKeyMerge.Add("DA01_MERGE");
            lstKeyMerge.Add("WB01_MERGE");
            lstKeyMerge.Add("DA02_MERGE");
            lstKeyMerge.Add("WB02_MERGE");
            lstKeyMerge.Add("DA03_MERGE");
            lstKeyMerge.Add("WB03_MERGE");
            lstKeyMerge.Add("DA04_MERGE");
            lstKeyMerge.Add("WB04_MERGE");
            lstKeyMerge.Add("DA05_MERGE");
            lstKeyMerge.Add("WB05_MERGE");
            lstKeyMerge.Add("DA_MERGE_Etc");
            lstKeyMerge.Add("WB_MERGE_Etc");

            lstKeyPFM.Add("DA401");
            lstKeyPFM.Add("WB401");
            lstKeyPFM.Add("DA402");
            lstKeyPFM.Add("WB402");
            lstKeyPFM.Add("DA403");
            lstKeyPFM.Add("WB403");
            lstKeyPFM.Add("DA404");
            lstKeyPFM.Add("WB404");
            lstKeyPFM.Add("DA405");
            lstKeyPFM.Add("WB405");
            lstKeyPFM.Add("DA_Etc");
            lstKeyPFM.Add("WB_Etc");

            lblNumericSum.Text = "";
            cdvDate.Value = DateTime.Now;
            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            cdvFactory.Enabled = false;
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
            return true;
        }

        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {            
            spdData.RPT_ColumnInit();
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 2;

            try
            {
                spdData.RPT_AddBasicColumn("Customer", 0, 0, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Major"      , 0, 1,  Visibles.True, Frozen.True, Align.Center, Merge.True,  Formatter.String, 70);
                spdData.RPT_AddBasicColumn("PKG"        , 0, 2,  Visibles.True, Frozen.True, Align.Center, Merge.True,  Formatter.String, 70);

                spdData.RPT_AddBasicColumn("classification", 0, 3, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 4,  Visibles.True, Frozen.True,  Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("TTL"        , 0, 5,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 6,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Primary Chip"   , 0, 7,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 8,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("2nd Chip"   , 0, 9,  Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("3rd Chip"   , 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("4th Chip"   , 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("5th Chip"   , 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("Etc Chip"   , 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn(""           , 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);

                spdData.RPT_AddBasicColumn("D/A", 1, 5,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 6,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 7,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 8,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 9,  Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 10, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 11, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 12, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 13, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 14, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("D/A", 1, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);
                spdData.RPT_AddBasicColumn("W/B", 1, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Number, 70);

                spdData.RPT_MerageHeaderColumnSpan(0, 3,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 5,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 7,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 9,  2);
                spdData.RPT_MerageHeaderColumnSpan(0, 11, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 13, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 15, 2);
                spdData.RPT_MerageHeaderColumnSpan(0, 17, 2);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);

                //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
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
        }

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 
        private string MakeSqlStringStd()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append(" SELECT  " + "\n");
            strSqlString.Append("             DECODE(MAT.MAT_GRP_1,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1)) CUSTOMER " + "\n");
            strSqlString.Append("           , MAT.MAT_GRP_1,DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') MAT_GRP_9,MAT.MAT_GRP_10 " + "\n");
            strSqlString.Append(" FROM  " + "\n");
            strSqlString.Append(" MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" WHERE  1 = 1 " + "\n");
            strSqlString.Append("     AND  MAT.FACTORY      = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("     AND  MAT.DELETE_FLAG  = ' ' " + "\n");
            strSqlString.Append("     AND  MAT.MAT_TYPE     = 'FG' " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (txtSearchProduct.Text.TrimEnd() != "%")
                strSqlString.Append("   AND MAT.MAT_ID like '" + txtSearchProduct.Text + "' " + "\n");
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

            strSqlString.Append(" GROUP BY MAT.MAT_GRP_1,MAT.MAT_GRP_9,MAT.MAT_GRP_10   " + "\n");
            strSqlString.Append(" ORDER BY DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5,6) " + "\n");
            strSqlString.Append("        , MAT.MAT_GRP_1,MAT.MAT_GRP_9,MAT.MAT_GRP_10 " + "\n"); 

            return strSqlString.ToString();
        }


        private string MakeSqlStringRes()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append(" SELECT  " + "\n");
            strSqlString.Append("             DECODE(MAT.MAT_GRP_1,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1)) CUSTOMER " + "\n");
            strSqlString.Append("           , MAT.MAT_GRP_1,DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') MAT_GRP_9,MAT.MAT_GRP_10  " + "\n");

            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 IN('A0400','A0401') THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_DA01 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0402' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_DA02     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0403' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_DA03     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0404' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_DA04     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0405' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_DA05     " + "\n");
            strSqlString.Append("                    " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 IN('A0600','A0601') THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_WB01 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0602' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_WB02 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0603' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_WB03 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0604' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_WB04 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0605' THEN " + "\n");
            strSqlString.Append("                                        RES.RES_CNT " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) RES_WB05 " + "\n");
            strSqlString.Append("                        " + "\n");
            strSqlString.Append(" " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 IN('A0400','A0401') THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_DA01 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0402' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_DA02     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0403' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_DA03     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0404' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_DA04     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0405' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_DA05     " + "\n");
            strSqlString.Append("                    " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 IN('A0600','A0601') THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_WB01 " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0602' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_WB02     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0603' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_WB03     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0604' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END   " + "\n");
            strSqlString.Append("                   ) CAP_WB04     " + "\n");
            strSqlString.Append("          , SUM( CASE WHEN RES.RES_STS_8 = 'A0605' THEN " + "\n");
            strSqlString.Append("                                        TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                              ELSE " + "\n");
            strSqlString.Append("                                       0  " + "\n");
            strSqlString.Append("                     END " + "\n");
            strSqlString.Append("                   ) CAP_WB05     " + "\n"); 

            strSqlString.Append("   FROM     " + "\n");
            strSqlString.Append(" ( " + "\n");
            strSqlString.Append("              SELECT FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8 " + "\n");
            strSqlString.Append("                   , COUNT(RES_ID) RES_CNT  " + "\n");
            strSqlString.Append("              FROM   MRASRESDEF  " + "\n");
            strSqlString.Append("              WHERE 1 = 1   " + "\n");
            strSqlString.Append("                AND FACTORY      = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                AND RES_CMF_9    = 'Y'  " + "\n");
            strSqlString.Append("                AND DELETE_FLAG  = ' '  " + "\n");
            strSqlString.Append("                AND RES_TYPE     = 'EQUIPMENT'  " + "\n");
            strSqlString.Append("                AND RES_STS_8    IN('A0400','A0401','A0402','A0403','A0404','A0405','A0600','A0601','A0602','A0603','A0604','A0605') " + "\n");
            strSqlString.Append("              GROUP BY FACTORY, RES_STS_2, RES_GRP_6, RES_GRP_7, RES_STS_8   " + "\n");
            strSqlString.Append(" ) RES  " + "\n");
            strSqlString.Append(" , MWIPMATDEF MAT " + "\n");
            strSqlString.Append(" , CRASUPHDEF UPH " + "\n");
            strSqlString.Append(" WHERE   1=1 " + "\n");
            strSqlString.Append("   AND   MAT.DELETE_FLAG   = ' '  " + "\n");
            strSqlString.Append("   AND   MAT.MAT_TYPE      = 'FG' " + "\n");
            strSqlString.Append("   AND   RES.FACTORY       = MAT.FACTORY " + "\n");
            strSqlString.Append("   AND   RES.RES_STS_2     = MAT.MAT_ID " + "\n");
            strSqlString.Append("      " + "\n");
            strSqlString.Append("   AND   RES.FACTORY       = UPH.FACTORY(+) " + "\n");
            strSqlString.Append("   AND   RES.RES_GRP_6     = UPH.RES_MODEL(+)  " + "\n");
            strSqlString.Append("   AND   RES.RES_GRP_7     = UPH.UPEH_GRP(+)  " + "\n");
            strSqlString.Append("   AND   RES.RES_STS_2     = UPH.MAT_ID(+) " + "\n");
            strSqlString.Append("   AND   RES.RES_STS_8     = UPH.OPER(+)  " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (txtSearchProduct.Text.TrimEnd() != "%")
                strSqlString.Append("   AND MAT.MAT_ID like '" + txtSearchProduct.Text + "' " + "\n");
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

            strSqlString.Append(" GROUP BY MAT.MAT_GRP_1,MAT.MAT_GRP_9,MAT.MAT_GRP_10   " + "\n");

            strSqlString.Append(" HAVING SUM( CASE WHEN RES.RES_STS_8 IN('A0400','A0401') THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0402' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0403' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0404' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0405' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("                     " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 IN('A0600','A0601') THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0602' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0603' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0604' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0605' THEN " + "\n");
            strSqlString.Append("                                         RES.RES_CNT " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("                         " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 IN('A0400','A0401') THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0402' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0403' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0404' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0405' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.75 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("                     " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 IN('A0600','A0601') THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )  " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0602' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0603' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0604' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END   " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("           + SUM( CASE WHEN RES.RES_STS_8 = 'A0605' THEN " + "\n");
            strSqlString.Append("                                         TRUNC(NVL(NVL(UPH.UPEH,0) * 24 * 0.8 * RES.RES_CNT, 0)) " + "\n");
            strSqlString.Append("                               ELSE " + "\n");
            strSqlString.Append("                                        0  " + "\n");
            strSqlString.Append("                      END " + "\n");
            strSqlString.Append("                    )      " + "\n");
            strSqlString.Append("            <> 0 " + "\n"); 


            strSqlString.Append(" ORDER BY DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5,6) " + "\n");
            strSqlString.Append("        , MAT.MAT_GRP_1,MAT.MAT_GRP_9,MAT.MAT_GRP_10 " + "\n"); 

            return strSqlString.ToString();
        }

        private string MakeSqlStringLot()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append(" SELECT  " + "\n");
            strSqlString.Append("             DECODE(MAT.MAT_GRP_1,'SE','SEC','HX','HYNIX','IM','iML','FC','FCI','IG','IMAGIS' , (SELECT DATA_1 FROM MGCMTBLDAT@RPTTOMES WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1)) CUSTOMER " + "\n");
            strSqlString.Append("           , MAT.MAT_GRP_1,DECODE(MAT.MAT_GRP_1,'SE',MAT.MAT_GRP_9,' ') MAT_GRP_9,MAT.MAT_GRP_10  " + "\n");
            strSqlString.Append("  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.DA01),0))    DA401  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.DA02),0))    DA402  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.DA03),0))    DA403  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.DA04),0))    DA404  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.DA05),0))    DA405  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.DA_Etc),0)) DA_Etc  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.WB01),0))   WB401  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.WB02),0))   WB402  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.WB03),0))   WB403  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.WB04),0))   WB404  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.WB05),0))   WB405  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(PFM.WB_Etc),0)) WB_Etc  " + "\n");
            strSqlString.Append("  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA01_CHIP),0))       DA01_CHIP   " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA02_CHIP),0))       DA02_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA03_CHIP),0))       DA03_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA04_CHIP),0))       DA04_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA05_CHIP),0))       DA05_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA_CHIP_Etc),0))    DA_CHIP_Etc  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA01_MERGE),0))   DA01_MERGE   " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA02_MERGE),0))   DA02_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA03_MERGE),0))   DA03_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA04_MERGE),0))   DA04_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA05_MERGE),0))   DA05_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.DA_MERGE_Etc),0)) DA_MERGE_Etc  " + "\n");
            strSqlString.Append("  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB01_CHIP),0))       WB01_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB02_CHIP),0))       WB02_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB03_CHIP),0))       WB03_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB04_CHIP),0))       WB04_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB05_CHIP),0))       WB05_CHIP  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB_CHIP_Etc),0))    WB_CHIP_Etc  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB01_MERGE),0))   WB01_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB02_MERGE),0))   WB02_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB03_MERGE),0))   WB03_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB04_MERGE),0))   WB04_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB05_MERGE),0))   WB05_MERGE  " + "\n");
            strSqlString.Append("          , ROUND(NVL(SUM(LOT.WB_MERGE_Etc),0)) WB_MERGE_Etc  " + "\n");
            strSqlString.Append(" FROM  " + "\n");
            strSqlString.Append("    MWIPMATDEF MAT " + "\n");
            strSqlString.Append("  , ( " + "\n");
            strSqlString.Append("     SELECT MAT.MAT_ID  " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER IN('A0400','A0401') THEN " + "\n");
            strSqlString.Append("                                             PFM01.DA                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) DA01 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0402' THEN " + "\n");
            strSqlString.Append("                                             PFM01.DA                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) DA02 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0403' THEN " + "\n");
            strSqlString.Append("                                             PFM01.DA                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) DA03 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0404' THEN " + "\n");
            strSqlString.Append("                                             PFM01.DA                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) DA04 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0405' THEN " + "\n");
            strSqlString.Append("                                             PFM01.DA                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) DA05 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER NOT IN('A0400','A0401','A0402','A0403','A0404','A0405') THEN " + "\n");
            strSqlString.Append("                                             PFM01.DA                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) DA_Etc " + "\n");
            strSqlString.Append("                      " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER IN('A0600','A0601') THEN " + "\n");
            strSqlString.Append("                                             PFM01.WB                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) WB01 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0602' THEN " + "\n");
            strSqlString.Append("                                             PFM01.WB                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) WB02 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0603' THEN " + "\n");
            strSqlString.Append("                                             PFM01.WB                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) WB03                         " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0604' THEN " + "\n");
            strSqlString.Append("                                             PFM01.WB                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) WB04 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER = 'A0605' THEN " + "\n");
            strSqlString.Append("                                             PFM01.WB                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) WB05 " + "\n");
            strSqlString.Append("               , SUM( CASE WHEN PFM01.OPER NOT IN('A0600','A0601','A0602','A0603','A0604', 'A0605') THEN " + "\n");
            strSqlString.Append("                                             PFM01.WB                                       " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                             0    " + "\n");
            strSqlString.Append("                           END  " + "\n");
            strSqlString.Append("                 ) WB_Etc             " + "\n");
            strSqlString.Append("     FROM " + "\n");
            strSqlString.Append("       MWipMatDef MAT " + "\n");
            strSqlString.Append("     , ( " + "\n");
            strSqlString.Append("         SELECT MAT.MAT_ID,OPER " + "\n");
            strSqlString.Append("                  , SUM(NVL((   CASE WHEN MAT.MAT_CMF_11 IN ('JRT', 'JWM') THEN  " + "\n");
            strSqlString.Append("                                                         WMO.DA402 " + "\n");
            strSqlString.Append("                                                WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN  " + "\n");
            strSqlString.Append("                                                         DECODE(SUBSTR(MAT.MAT_GRP_4,-1), '2', WMO.DA402, '3', WMO.DA403, '4', WMO.DA404, '5', WMO.DA405, 0) " + "\n");
            strSqlString.Append("                                                ELSE  " + "\n");
            strSqlString.Append("                                                         WMO.DA " + "\n");
            strSqlString.Append("                                       END " + "\n");
            strSqlString.Append("                                   ),0) " + "\n");
            strSqlString.Append("                           ) DA " + "\n");
            strSqlString.Append("                  , SUM(NVL((   CASE WHEN MAT.MAT_CMF_11 IN ('JRT', 'JWM') THEN  " + "\n");
            strSqlString.Append("                                                         WMO.WB602 " + "\n");
            strSqlString.Append("                                               WHEN MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-' THEN  " + "\n");
            strSqlString.Append("                                                         DECODE(SUBSTR(MAT.MAT_GRP_4,-1), '2', WMO.WB602, '3', WMO.WB603, '4', WMO.WB604, '5', WMO.WB605, 0) " + "\n");
            strSqlString.Append("                                               ELSE  " + "\n");
            strSqlString.Append("                                                         WMO.WB " + "\n");
            strSqlString.Append("                                      END),0) " + "\n");
            strSqlString.Append("                          ) WB                   " + "\n");
            strSqlString.Append("         FROM                  " + "\n");
            strSqlString.Append("           MWipMatDef MAT " + "\n");
            strSqlString.Append("         , ( " + "\n");
            strSqlString.Append("             SELECT MAT_ID,OPER  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER_GRP_7, 'D/A', QTY  ,0)) DA " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER_GRP_7, 'W/B',QTY  ,0)) WB " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0402', QTY,  0))        DA402 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0403', QTY,  0))        DA403 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0404', QTY,  0))        DA404 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0405', QTY,  0))        DA405 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0602', QTY,  0))        WB602 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0603', QTY,  0))        WB603 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0604', QTY,  0))        WB604 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPER, 'A0605', QTY,  0))        WB605       " + "\n");
            strSqlString.Append("               FROM (  " + "\n");
            strSqlString.Append("                         SELECT A.MAT_ID,B.OPER,B.OPER_GRP_7 " + "\n");
            strSqlString.Append("                                   , SUM(A.S1_END_QTY_1 + A.S2_END_QTY_1 + A.S3_END_QTY_1 + A.S1_END_RWK_QTY_1 + A.S2_END_RWK_QTY_1 + A.S3_END_RWK_QTY_1 ) QTY  " + "\n");
            strSqlString.Append("                           FROM RSUMWIPMOV A  " + "\n");
            strSqlString.Append("                                   , MWIPOPRDEF  B  " + "\n");
            strSqlString.Append("                         WHERE 1=1  " + "\n");
            strSqlString.Append("                             AND A.FACTORY     = B.FACTORY  " + "\n");
            strSqlString.Append("                             AND A.OPER           = B.OPER  " + "\n");
            strSqlString.Append("                             AND A.FACTORY     = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                             AND A.MAT_VER     = 1 " + "\n");
            strSqlString.Append("                             AND A.WORK_DATE = '"+ cdvDate.SelectedValue() +"' " + "\n");
            strSqlString.Append("                             AND A.LOT_TYPE     = 'W' " + "\n");
            strSqlString.Append("                             AND A.OPER <> 'A1760' " + "\n");
            strSqlString.Append("                             AND A.CM_KEY_3 LIKE 'P%' " + "\n");
            strSqlString.Append("                             AND B.OPER_GRP_7 IN ('D/A','W/B')  " + "\n");
            strSqlString.Append("                         GROUP BY A.MAT_ID,B.OPER,B.OPER_GRP_7  " + "\n");
            strSqlString.Append("                         )  " + "\n");
            strSqlString.Append("              GROUP BY MAT_ID,OPER " + "\n");
            strSqlString.Append("          ) WMO " + "\n");
            strSqlString.Append("         WHERE 1 = 1 " + "\n");
            strSqlString.Append("             AND MAT.FACTORY       = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("             AND MAT.MAT_ID         = WMO.MAT_ID  " + "\n");
            strSqlString.Append("             AND MAT.MAT_TYPE     = 'FG' " + "\n");
            strSqlString.Append("             AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("         GROUP BY MAT.MAT_ID,OPER " + "\n");
            strSqlString.Append("        ) PFM01 " + "\n");
            strSqlString.Append("     WHERE 1 = 1 " + "\n");
            strSqlString.Append("         AND MAT.FACTORY       = '" + GlobalVariable.gsAssyDefaultFactory + "'   " + "\n");
            strSqlString.Append("         AND MAT.MAT_ID         = PFM01.MAT_ID " + "\n");
            strSqlString.Append("         AND MAT.MAT_TYPE     = 'FG' " + "\n");
            strSqlString.Append("         AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("     GROUP BY MAT.MAT_ID " + "\n");
            strSqlString.Append("    ) PFM " + "\n");
            strSqlString.Append("  , ( " + "\n");
            strSqlString.Append("     SELECT MAT.MAT_ID " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0250','A0305','A0306','A0395','A0400','A0401','A0500','A0501') ) AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA01_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0402','A0502','A0532') )                                                          AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA02_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0403','A0503','A0533') )                                                          AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA03_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0404','A0504','A0534') )                                                          AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA04_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0405') )                                                                                 AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA05_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER NOT IN('A0250','A0305','A0306','A0395','A0400','A0401','A0500','A0501','A0402','A0502','A0532','A0403','A0503','A0533','A0404','A0504','A0534','A0405') )  " + "\n");
            strSqlString.Append("                                      AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA_CHIP_Etc " + "\n");
            strSqlString.Append("                          " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0250','A0305','A0306','A0395','A0400','A0401','A0500','A0501') ) AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA01_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0402','A0502','A0532') )                                                          AND         ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA02_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0403','A0503','A0533') )                                                          AND         ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA03_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0404','A0504','A0534') )                                                          AND         ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA04_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER IN('A0405') )                                                                                  AND         ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA05_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('D/A','S/P') AND LOT02.OPER NOT IN('A0250','A0305','A0306','A0395','A0400','A0401','A0500','A0501','A0402','A0502','A0532','A0403','A0503','A0533','A0404','A0504','A0534','A0405') )                                                                                   " + "\n");
            strSqlString.Append("                                     AND         ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.DA " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) DA_MERGE_Etc " + "\n");
            strSqlString.Append("                          " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0550','A0551','A0600','A0601','A0800','A0801') )      AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB01_CHIP               " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0552','A0602','A0802') )                                         AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB02_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0553','A0603','A0803') )                                         AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB03_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0554','A0604','A0804') )                                         AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB04_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0605') )                                                                AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB05_CHIP " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER NOT IN('A0550','A0551','A0600','A0601','A0800','A0801','A0552','A0602','A0802','A0553','A0603','A0803','A0554','A0604','A0804','A0605') )                                                                 " + "\n");
            strSqlString.Append("                                     AND NOT ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB_CHIP_Etc " + "\n");
            strSqlString.Append("  " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0550','A0551','A0600','A0601','A0800','A0801') )       AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB01_MERGE               " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0552','A0602','A0802') )                                         AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB02_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0553','A0603','A0803') )                                         AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB03_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0554','A0604','A0804') )                                         AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB04_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER IN('A0605') )                                                                 AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB05_MERGE " + "\n");
            strSqlString.Append("               , SUM (CASE WHEN ( LOT02.OPER_GRP_1 IN('W/B','GATE') AND LOT02.OPER NOT IN('A0550','A0551','A0600','A0601','A0800','A0801','A0552','A0602','A0802','A0553','A0603','A0803','A0554','A0604','A0804','A0605') )                                                                  " + "\n");
            strSqlString.Append("                                     AND        ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') ) THEN " + "\n");
            strSqlString.Append("                                            LOT02.WB " + "\n");
            strSqlString.Append("                                   ELSE " + "\n");
            strSqlString.Append("                                            0 " + "\n");
            strSqlString.Append("                          END   " + "\n");
            strSqlString.Append("                         ) WB_MERGE_Etc " + "\n");
            strSqlString.Append("       FROM " + "\n");
            strSqlString.Append("        MWipMatDef MAT  " + "\n");
            strSqlString.Append("      , ( " + "\n");
            strSqlString.Append("         SELECT MAT.MAT_ID, LOT01.OPER ,LOT01.OPER_GRP_1  " + "\n");
            strSqlString.Append("                   , SUM(NVL(( CASE WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN  " + "\n");
            strSqlString.Append("                                                        DECODE(MAT.MAT_GRP_5, '2nd', NVL(LOT01.V3,0)/NVL(GCM.DATA_1,1)+NVL(LOT01.V4,0), 'Merge', NVL(LOT01.V3,0)/NVL(GCM.DATA_1,1)+NVL(LOT01.V4,0), 0) " + "\n");
            strSqlString.Append("                                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  " + "\n");
            strSqlString.Append("                                                         CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  " + "\n");
            strSqlString.Append("                                                                           NVL(LOT01.V3,0)/NVL(GCM.DATA_1,1)+NVL(LOT01.V4,0)  " + "\n");
            strSqlString.Append("                                                                  ELSE  " + "\n");
            strSqlString.Append("                                                                           0  " + "\n");
            strSqlString.Append("                                                         END " + "\n");
            strSqlString.Append("                                               ELSE  " + "\n");
            strSqlString.Append("                                                         NVL(LOT01.V3,0)/NVL(GCM.DATA_1,1)+NVL(LOT01.V4,0) " + "\n");
            strSqlString.Append("                                      END),0) " + "\n");
            strSqlString.Append("                            ) DA " + "\n");
            strSqlString.Append("                   , SUM(NVL(( CASE WHEN MAT.MAT_CMF_11 IN (SELECT DISTINCT(MAT_CMF_11) FROM MWIPMATDEF WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND MAT_GRP_5 = '1st' AND MAT_ID LIKE 'SEKS%') THEN  " + "\n");
            strSqlString.Append("                                                        DECODE(MAT.MAT_GRP_5, '2nd', NVL(LOT01.V5+LOT01.V16,0), 'Merge', NVL(LOT01.V5+LOT01.V16,0), 0) " + "\n");
            strSqlString.Append("                                               WHEN MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT_GRP_5 <> '-' THEN  " + "\n");
            strSqlString.Append("                                                         CASE WHEN MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT_GRP_5 LIKE 'Middle%' THEN  " + "\n");
            strSqlString.Append("                                                                           NVL(LOT01.V5+LOT01.V16,0)  " + "\n");
            strSqlString.Append("                                                                  ELSE  " + "\n");
            strSqlString.Append("                                                                           0  " + "\n");
            strSqlString.Append("                                                         END " + "\n");
            strSqlString.Append("                                               ELSE  " + "\n");
            strSqlString.Append("                                                         NVL(LOT01.V5+LOT01.V16,0) " + "\n");
            strSqlString.Append("                              END),0) " + "\n");
            strSqlString.Append("                            ) WB    " + "\n");
            strSqlString.Append("         FROM " + "\n");
            strSqlString.Append("            MWipMatDef MAT " + "\n");
            strSqlString.Append("          , ( " + "\n");
            strSqlString.Append("             SELECT MAT.MAT_ID,LOT.OPER,OPR.OPER_GRP_1  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPR.OPER_GRP_1, 'S/P'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),LOT.QTY_1), 0)) V3  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPR.OPER_GRP_1, 'D/A'   , DECODE(MAT.MAT_GRP_3,'COB',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),LOT.QTY_1), 0)) V4 " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPR.OPER_GRP_1, 'W/B'  , DECODE(MAT.MAT_GRP_3,'COB',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),LOT.QTY_1), 0)) V5  " + "\n");
            strSqlString.Append("                       , SUM(DECODE(OPR.OPER_GRP_1, 'GATE', DECODE(MAT.MAT_GRP_3,'COB',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),'BGN',ROUND(LOT.QTY_1/TO_NUMBER(DECODE(MAT.MAT_CMF_13,' ',1,MAT.MAT_CMF_13)),0),LOT.QTY_1), 0)) V16  " + "\n");
            strSqlString.Append("               FROM  " + "\n");
            strSqlString.Append("                MWIPMATDEF MAT " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
                strSqlString.Append("              , RWIPLOTSTS_BOH LOT " + "\n");
            else
                strSqlString.Append("              , RWIPLOTSTS     LOT " + "\n");

            strSqlString.Append("              , MWIPOPRDEF OPR " + "\n");
            strSqlString.Append("             WHERE 1 = 1  " + "\n");
            strSqlString.Append("                 AND MAT.FACTORY   = LOT.FACTORY " + "\n");
            strSqlString.Append("                 AND MAT.MAT_ID     = LOT.MAT_ID   " + "\n");
            strSqlString.Append("                 AND LOT.FACTORY   = OPR.FACTORY(+) " + "\n");
            strSqlString.Append("                 AND LOT.OPER         = OPR.OPER(+)        " + "\n");
            strSqlString.Append("                 AND MAT.FACTORY  = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("                 AND MAT.MAT_TYPE = 'FG'   " + "\n");
            strSqlString.Append("                 AND MAT.DELETE_FLAG <> 'Y' " + "\n");
            strSqlString.Append("                 AND MAT.MAT_GRP_2   <> '-' " + "\n");
            strSqlString.Append("                 AND LOT.LOT_TYPE = 'W'  " + "\n");
            strSqlString.Append("                 AND LOT.LOT_DEL_FLAG = ' '  " + "\n");
            strSqlString.Append("                 AND LOT.LOT_CMF_5 LIKE 'P%' " + "\n");

            if (DateTime.Now.ToString("yyyyMMdd") != cdvDate.SelectedValue())
            {
                strSqlString.Append("            AND MAT.MAT_VER = 1  " + "\n");
                strSqlString.Append("            AND LOT.CUTOFF_DT = '" + cdvDate.SelectedValue() + "22' " + "\n");
            }

            strSqlString.Append("             GROUP BY MAT.MAT_ID,LOT.OPER,OPR.OPER_GRP_1 " + "\n");
            strSqlString.Append("            ) LOT01 " + "\n");
            strSqlString.Append("          , ( " + "\n");
            strSqlString.Append("             SELECT KEY_1,DATA_1 " + "\n");
            strSqlString.Append("               FROM MGCMTBLDAT  " + "\n");
            strSqlString.Append("             WHERE FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "'  " + "\n");
            strSqlString.Append("                 AND TABLE_NAME IN ('H_SEC_AUTO_LOSS','H_HX_AUTO_LOSS') " + "\n");
            strSqlString.Append("             ) GCM    " + "\n");
            strSqlString.Append("         WHERE 1 = 1    " + "\n");
            strSqlString.Append("             AND MAT.MAT_ID         = LOT01.MAT_ID " + "\n");
            strSqlString.Append("             AND MAT.FACTORY       = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("             AND MAT.MAT_TYPE     = 'FG' " + "\n");
            strSqlString.Append("             AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("             AND MAT.MAT_ID         = GCM.KEY_1(+) " + "\n");
            strSqlString.Append("         GROUP BY MAT.MAT_ID, LOT01.OPER,LOT01.OPER_GRP_1  " + "\n");
            strSqlString.Append("        ) LOT02 " + "\n");
            strSqlString.Append("     WHERE 1 = 1  " + "\n");
            strSqlString.Append("         AND MAT.MAT_ID         = LOT02.MAT_ID " + "\n");
            strSqlString.Append("         AND MAT.FACTORY       = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("         AND MAT.MAT_TYPE     = 'FG' " + "\n");
            strSqlString.Append("         AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("     GROUP BY MAT.MAT_ID  " + "\n");
            strSqlString.Append("    ) LOT   " + "\n");
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("     AND MAT.FACTORY       = '" + GlobalVariable.gsAssyDefaultFactory + "' " + "\n");
            strSqlString.Append("     AND MAT.MAT_TYPE     = 'FG' " + "\n");
            strSqlString.Append("     AND MAT.DELETE_FLAG = ' ' " + "\n");
            strSqlString.Append("        " + "\n");
            strSqlString.Append("     AND MAT.MAT_ID         = PFM.MAT_ID(+) " + "\n");
            strSqlString.Append("     AND MAT.MAT_ID         = LOT.MAT_ID(+) " + "\n");

            //상세 조회에 따른 SQL문 생성                        
            if (txtSearchProduct.Text.TrimEnd() != "%")
                strSqlString.Append("   AND MAT.MAT_ID like '" + txtSearchProduct.Text + "' " + "\n");
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



            strSqlString.Append(" GROUP BY MAT.MAT_GRP_1,MAT.MAT_GRP_9,MAT.MAT_GRP_10   " + "\n");
            strSqlString.Append(" HAVING    NVL(SUM(PFM.DA01),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.DA02),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.DA03),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.DA04),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.DA05),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.DA_Etc),0) " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.WB01),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.WB02),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.WB03),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.WB04),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.WB05),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(PFM.WB_Etc),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA01_CHIP),0)   " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA02_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA03_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA04_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA05_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA_CHIP_Etc),0)     " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA01_MERGE),0)   " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA02_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA03_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA04_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA05_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.DA_MERGE_Etc),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB01_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB02_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB03_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB04_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB05_CHIP),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB_CHIP_Etc),0) " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB01_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB02_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB03_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB04_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB05_MERGE),0)  " + "\n");
            strSqlString.Append("             + NVL(SUM(LOT.WB_MERGE_Etc),0) " + "\n");
            strSqlString.Append("          <> 0 " + "\n");
            strSqlString.Append(" ORDER BY DECODE(MAT.MAT_GRP_1, 'SE', 1, 'HX', 2, 'IM', 3, 'FC', 4, 'IG', 5,6) " + "\n");
            strSqlString.Append("              , MAT.MAT_GRP_1,MAT.MAT_GRP_9,MAT.MAT_GRP_10 " + "\n");
            strSqlString.Append("  " + "\n"); 

            return strSqlString.ToString();
        }




        /// <summary>
        /// 5. Chart 생성
        /// </summary>
        /// <param name="DT">Chart를 생성할 데이터 테이블</param>

        #endregion

        #region EventHandler

        private void btnView_Click(object sender, EventArgs e)
        {

            if (CheckField() == false) return;
            spdData_Sheet1.RowCount = 0;

            try
            {
                lstDataset.Clear();
                lblNumericSum.Text = "";
                LoadingPopUp.LoadIngPopUpShow(this);
                DisplaySpread();


                //if (dt.Rows.Count == 0)
                //{
                //    dt.Dispose();
                //    LoadingPopUp.LoadingPopUpHidden();
                //    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                //    return;
                //}

                //그루핑 순서 1순위만 SubTotal을 사용하기 위해서 첫번째 값을 가져옴
                //int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                
                //int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub+1, 13, null, null, btnSort);
                //spdData.RPT_FillDataSelectiveCells("Total", 0, 13, 0, 1, true, Align.Center, VerticalAlign.Center);

                //spdData.RPT_AutoFit(false);
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

        private void DisplaySpread()
        {
            DataTable dtStd = null;
            DataTable dtRes = null;
            DataTable dtLot = null;

            dtStd = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringStd());
            dtRes = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringRes());
            dtLot = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlStringLot());

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                System.Windows.Forms.Clipboard.SetText(MakeSqlStringStd() + ";\n\n\n\n\n\n\n\n" + MakeSqlStringRes() + ";\n\n\n\n\n\n\n\n" + MakeSqlStringLot() + ";\n");


            if (dtLot.Rows.Count + dtRes.Rows.Count + dtLot.Rows.Count == 0)
            {
                dtStd.Dispose();
                dtRes.Dispose();
                dtLot.Dispose();

                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }


            GridColumnInit();
            spdData.RPT_SetCellsType();
            spdData_Sheet1.RowCount = dtStd.Rows.Count * 6 * 4;

            int intResTableRowCnt = 0;
            int intLotTableRowCnt = 0;
            string strRowSeparateStd = null;
            string strRowSeparateRes = null;
            string strRowSeparateLot = null;

            DateTime dt = DateTime.Now;
            int intNowHour = dt.Hour;
            int intNowMinute = dt.Minute;

            //Data Setting
            foreach (DataRow drStd in dtStd.Rows)
            {
                bool isValueExist = false;

                UnitPKG tmpPKG = new UnitPKG();
                tmpPKG.strCUSTOMER   = drStd["CUSTOMER"].ToString();
                tmpPKG.strMAT_GRP_1  = drStd["MAT_GRP_1"].ToString();
                tmpPKG.strMAT_GRP_9  = drStd["MAT_GRP_9"].ToString();
                tmpPKG.strMAT_GRP_10 = drStd["MAT_GRP_10"].ToString();

                strRowSeparateStd =     String.Format("{0,-20}", drStd["CUSTOMER"].ToString()) + String.Format("{0,-20}", drStd["MAT_GRP_9"].ToString()) + String.Format("{0,-20}", drStd["MAT_GRP_10"].ToString());

                //배열 OutOfBound 방지
                if (intResTableRowCnt < dtRes.Rows.Count)
                {
                    DataRow drRes = dtRes.Rows[intResTableRowCnt];
                    strRowSeparateRes = String.Format("{0,-20}", drRes["CUSTOMER"].ToString()) + String.Format("{0,-20}", drRes["MAT_GRP_9"].ToString()) + String.Format("{0,-20}", drRes["MAT_GRP_10"].ToString());

                    if (strRowSeparateStd == strRowSeparateRes)
                    {
                        //설비대수      
                        for (int ii = 0; ii < lstKeyRes.Count; ii++)
                        {
                            tmpPKG.dicRes.Add(lstKeyRes[ii], long.Parse(drRes[lstKeyRes[ii]].ToString()));
                            if (long.Parse(drRes[lstKeyRes[ii]].ToString()) != 0) isValueExist = true;
                        }
                        //CAPA
                        for (int ii = 0; ii < lstKeyCap.Count; ii++)
                        {
                            tmpPKG.dicCapa.Add(lstKeyCap[ii], long.Parse(drRes[lstKeyCap[ii]].ToString()));
                            if (long.Parse(drRes[lstKeyCap[ii]].ToString()) != 0) isValueExist = true;
                        }

                        intResTableRowCnt++;
                    }
                }

                //배열 OutOfBound 방지
                if (intLotTableRowCnt < dtLot.Rows.Count)
                {
                    DataRow drLot = dtLot.Rows[intLotTableRowCnt];
                    strRowSeparateLot = String.Format("{0,-20}", drLot["CUSTOMER"].ToString()) + String.Format("{0,-20}", drLot["MAT_GRP_9"].ToString()) + String.Format("{0,-20}", drLot["MAT_GRP_10"].ToString());

                    if (strRowSeparateStd == strRowSeparateLot)
                    {
                        try
                        {
                            try
                            {
                                //재공품 Chip                   [멀티칩 제외 칩들]
                                for (int ii = 0; ii < lstKeyChip.Count; ii++)
                                {
                                    tmpPKG.dicLotChip.Add(lstKeyChip[ii], long.Parse(drLot[lstKeyChip[ii]].ToString()));
                                    if (long.Parse(drLot[lstKeyChip[ii]].ToString()) != 0) isValueExist = true;
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }

                            try
                            {
                                //재공품 Merge  DieAttach       [멀티칩 ( (MAT.MAT_GRP_4 NOT IN ('-','FD','FU') AND MAT.MAT_GRP_5 <> '-') AND (MAT.MAT_GRP_5 IN ('1st','Merge') OR MAT.MAT_GRP_5 LIKE 'Middle%') )]
                                for (int ii = 0; ii < lstKeyMerge.Count; ii++)
                                {
                                    tmpPKG.dicLotMerge.Add(lstKeyMerge[ii], long.Parse(drLot[lstKeyMerge[ii]].ToString()));
                                    if (long.Parse(drLot[lstKeyMerge[ii]].ToString()) != 0) isValueExist = true;
                                }
                            }
                            catch (Exception e)
                            {
                                //소수점 들어가는 항목있음???
                                MessageBox.Show(e.ToString());
                            }

                            try
                            {
                                //재공실적,예상실적
                                for (int ii = 0; ii < lstKeyPFM.Count; ii++)
                                {
                                    tmpPKG.dicPerformance.Add(lstKeyPFM[ii], long.Parse(drLot[lstKeyPFM[ii]].ToString()));
                                    if (long.Parse(drLot[lstKeyPFM[ii]].ToString()) != 0) isValueExist = true;
                                }
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }

                            intLotTableRowCnt++;

                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
                        }
                    }
                }
                //값 없는 PackageUnit 제외
                if (isValueExist)
                    lstDataset.Add(tmpPKG);
            }

            if (lstDataset.Count ==0)
            {
                dtStd.Dispose();
                dtRes.Dispose();
                dtLot.Dispose();

                LoadingPopUp.LoadingPopUpHidden();
                CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                return;
            }

            //Spread Data Displays
            int     intUnitRow          = 6;
            UnitPKG pkgSubSumMajor      = new UnitPKG();
            UnitPKG pkgSubSumCustomer   = new UnitPKG();
            UnitPKG pkgTotalSum         = new UnitPKG();
            string  strRowSeparateSubSum   = "";
            string  strRowSeparateTotalSum = "";

            string  strOldCutomer               = "";
            string  strOldMAT_GRP_9             = "";
            int     intCutormerUnitCnt          = 0;
            int     intMAT_GRP_9_UnitCnt        = 0;
            int     intCutormerStartRowIndex    = 0;
            int     intMAT_GRP_9_StartRowIndex  = 0;


            spdData.ActiveSheet.Columns[0].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[1].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[2].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[3].BackColor = colorFixedColumn;
            spdData.ActiveSheet.Columns[4].BackColor = colorFixedColumn;

            foreach (UnitPKG tmpPKG in lstDataset)
            {
                spdData.ActiveSheet.Cells[intUnitRow + 0, 0].Text = tmpPKG.strCUSTOMER;
                spdData.ActiveSheet.Cells[intUnitRow + 0, 1].Text = tmpPKG.strMAT_GRP_9;
                spdData.ActiveSheet.Cells[intUnitRow + 0, 2].Text = tmpPKG.strMAT_GRP_10;

                //Sub Total [Customer + Major]
                if (strRowSeparateSubSum != String.Format("{0,-20}", tmpPKG.strCUSTOMER) + String.Format("{0,-20}", tmpPKG.strMAT_GRP_9))
                {
                    strRowSeparateSubSum  = String.Format("{0,-20}", tmpPKG.strCUSTOMER) + String.Format("{0,-20}", tmpPKG.strMAT_GRP_9);
                    if (intUnitRow != 6)
                    {
                        spdData.ActiveSheet.Cells[intMAT_GRP_9_StartRowIndex, 1].Text = strOldMAT_GRP_9;
                        spdData.ActiveSheet.Cells[intMAT_GRP_9_StartRowIndex, 1].RowSpan = intMAT_GRP_9_UnitCnt * 6;

                        intMAT_GRP_9_StartRowIndex = 0;
                        intMAT_GRP_9_UnitCnt = 0;

                        spdData.ActiveSheet.Cells[intUnitRow + 0, 1].RowSpan = 6;
                        spdData.ActiveSheet.Cells[intUnitRow + 0, 1].ColumnSpan = 2;
                        spdData.ActiveSheet.Cells[intUnitRow + 0, 1].Text = strOldMAT_GRP_9 + " Sub Total";
                        strOldMAT_GRP_9 = "";

                        UnitPackageSpreadDisplay(intUnitRow, pkgSubSumMajor);
                        pkgSubSumMajor = null;
                        pkgSubSumMajor = new UnitPKG();

                        spdData.ActiveSheet.Rows[intUnitRow + 0].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 1].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 2].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 3].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 4].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 5].BackColor = colorSubTotal;

                        intCutormerUnitCnt++;
                        intUnitRow += 6;
                    }
                    strOldMAT_GRP_9 = tmpPKG.strMAT_GRP_9;
                    intMAT_GRP_9_StartRowIndex = intUnitRow;
                }
                intMAT_GRP_9_UnitCnt++;

                //Sub Total [Customer]
                if (strRowSeparateTotalSum != String.Format("{0,-20}", tmpPKG.strCUSTOMER))
                {
                    strRowSeparateTotalSum  = String.Format("{0,-20}", tmpPKG.strCUSTOMER);
                    if (intUnitRow != 6)
                    {
                        spdData.ActiveSheet.Cells[intCutormerStartRowIndex, 0].Text = strOldCutomer;
                        spdData.ActiveSheet.Cells[intCutormerStartRowIndex, 0].RowSpan = intCutormerUnitCnt * 6;

                        intCutormerStartRowIndex = 0;
                        intCutormerUnitCnt = 0;

                        spdData.ActiveSheet.Cells[intUnitRow + 0, 0].RowSpan = 6;
                        spdData.ActiveSheet.Cells[intUnitRow + 0, 0].ColumnSpan = 3;
                        spdData.ActiveSheet.Cells[intUnitRow + 0, 0].Text = strOldCutomer + " Sub Total";
                        strOldCutomer = "";

                        UnitPackageSpreadDisplay(intUnitRow, pkgSubSumCustomer);
                        pkgSubSumCustomer = null;
                        pkgSubSumCustomer = new UnitPKG();


                        spdData.ActiveSheet.Rows[intUnitRow + 0].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 1].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 2].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 3].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 4].BackColor = colorSubTotal;
                        spdData.ActiveSheet.Rows[intUnitRow + 5].BackColor = colorSubTotal;

                        intMAT_GRP_9_StartRowIndex++;
                        intUnitRow += 6;
                    }
                    strOldCutomer = tmpPKG.strCUSTOMER;
                    intCutormerStartRowIndex = intUnitRow;
                    intMAT_GRP_9_StartRowIndex = intUnitRow;
                }

                //Values Display
                spdData.ActiveSheet.Cells[intUnitRow + 0, 2].Text = tmpPKG.strMAT_GRP_10;
                spdData.ActiveSheet.Cells[intUnitRow + 0, 2].RowSpan = 6;
                UnitPackageSpreadDisplay(intUnitRow,tmpPKG);

                UnitPackgeCalculateTTL(pkgSubSumMajor,      tmpPKG);
                UnitPackgeCalculateTTL(pkgSubSumCustomer,   tmpPKG);
                UnitPackgeCalculateTTL(pkgTotalSum,         tmpPKG);

                intUnitRow += 6;
                intCutormerUnitCnt++;
            }
            spdData.ActiveSheet.Cells[intMAT_GRP_9_StartRowIndex, 1].RowSpan = intMAT_GRP_9_UnitCnt * 6;

            intMAT_GRP_9_StartRowIndex = 0;
            intMAT_GRP_9_UnitCnt = 0;


            //SubTotal Customer + Major
            spdData.ActiveSheet.Cells[intUnitRow + 0, 1].RowSpan = 6;
            spdData.ActiveSheet.Cells[intUnitRow + 0, 1].ColumnSpan = 2;
            spdData.ActiveSheet.Cells[intUnitRow + 0, 1].Text = strOldMAT_GRP_9 + " Sub Total";

            UnitPackageSpreadDisplay(intUnitRow, pkgSubSumMajor);
            pkgSubSumMajor = null;
            pkgSubSumMajor = new UnitPKG();

            spdData.ActiveSheet.Rows[intUnitRow + 0].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 1].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 2].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 3].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 4].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 5].BackColor = colorSubTotal;



            //SubTotal Customer
            intUnitRow += 6; intCutormerUnitCnt++;
            spdData.ActiveSheet.Cells[intCutormerStartRowIndex, 0].RowSpan = intCutormerUnitCnt * 6;

            intCutormerStartRowIndex = 0;
            intCutormerUnitCnt = 0;
            spdData.ActiveSheet.Cells[intUnitRow + 0, 0].RowSpan = 6;
            spdData.ActiveSheet.Cells[intUnitRow + 0, 0].ColumnSpan = 3;
            spdData.ActiveSheet.Cells[intUnitRow + 0, 0].Text = strOldCutomer + " Sub Total";

            UnitPackageSpreadDisplay(intUnitRow, pkgSubSumCustomer);
            pkgSubSumCustomer = null;
            pkgSubSumCustomer = new UnitPKG();


            spdData.ActiveSheet.Rows[intUnitRow + 0].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 1].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 2].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 3].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 4].BackColor = colorSubTotal;
            spdData.ActiveSheet.Rows[intUnitRow + 5].BackColor = colorSubTotal;
            intUnitRow += 6;

            
            


            spdData_Sheet1.RowCount = intUnitRow;

            //Total Sum
            intUnitRow = 0;
            spdData.ActiveSheet.Cells[0, 0].Text = "Total";
            spdData.ActiveSheet.Cells[0, 0].RowSpan = 6;
            spdData.ActiveSheet.Cells[0, 0].ColumnSpan = 3;

            spdData.ActiveSheet.FrozenRowCount = 6;
            spdData.ActiveSheet.FrozenColumnCount = 5;

            spdData.ActiveSheet.Cells[0, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            spdData.ActiveSheet.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            spdData.ActiveSheet.Rows[0].BackColor = colorTotal;
            spdData.ActiveSheet.Rows[1].BackColor = colorTotal;
            spdData.ActiveSheet.Rows[2].BackColor = colorTotal;
            spdData.ActiveSheet.Rows[3].BackColor = colorTotal;
            spdData.ActiveSheet.Rows[4].BackColor = colorTotal;
            spdData.ActiveSheet.Rows[5].BackColor = colorTotal;

            UnitPackageSpreadDisplay(0, pkgTotalSum);




            dtStd.Dispose();
            dtRes.Dispose();
            dtLot.Dispose();
            lstDataset.Clear();
        }

        //예상 실적 합산하지 않음
        private void UnitPackgeCalculateTTL(UnitPKG sumUnitPKG, UnitPKG destinationUnitPKG)
        {
            //초기화
            if (sumUnitPKG.dicRes.Count == 0)
            {
                foreach (string tmpKey in lstKeyRes)
                {
                    sumUnitPKG.dicRes.Add(tmpKey, 0);
                }
            }
            if (sumUnitPKG.dicCapa.Count == 0)
            {
                foreach (string tmpKey in lstKeyCap)
                {
                    sumUnitPKG.dicCapa.Add(tmpKey, 0);
                }
            }
            if (sumUnitPKG.dicLotChip.Count == 0)
            {
                foreach (string tmpKey in lstKeyChip)
                {
                    sumUnitPKG.dicLotChip.Add(tmpKey, 0);
                }
            }
            if (sumUnitPKG.dicLotMerge.Count == 0)
            {
                foreach (string tmpKey in lstKeyMerge)
                {
                    sumUnitPKG.dicLotMerge.Add(tmpKey, 0);
                }
            }
            if (sumUnitPKG.dicPerformance.Count == 0)
            {
                foreach (string tmpKey in lstKeyPFM)
                {
                    sumUnitPKG.dicPerformance.Add(tmpKey, 0);
                }
            }


            ////설비대수
            try
            {
                if (destinationUnitPKG.dicRes.Count > 0)
                {
                    foreach (KeyValuePair<string, long> tmpKeyValue in destinationUnitPKG.dicRes)
                    {
                        sumUnitPKG.dicRes[tmpKeyValue.Key] += tmpKeyValue.Value;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            ////CAPA
            try
            {
                if (destinationUnitPKG.dicCapa.Count > 0)
                {
                    foreach (KeyValuePair<string, long> tmpKeyValue in destinationUnitPKG.dicCapa)
                    {
                        sumUnitPKG.dicCapa[tmpKeyValue.Key] += tmpKeyValue.Value;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            ////재공 Chip
            try
            {
                if (destinationUnitPKG.dicLotChip.Count > 0)
                {
                    foreach (KeyValuePair<string, long> tmpKeyValue in destinationUnitPKG.dicLotChip)
                    {
                        sumUnitPKG.dicLotChip[tmpKeyValue.Key] += tmpKeyValue.Value;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            ////재공 Merge
            try
            {
                if (destinationUnitPKG.dicLotMerge.Count > 0)
                {
                    foreach (KeyValuePair<string, long> tmpKeyValue in destinationUnitPKG.dicLotMerge)
                    {
                        sumUnitPKG.dicLotMerge[tmpKeyValue.Key] += tmpKeyValue.Value;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            ////실적
            try
            {
                if (destinationUnitPKG.dicPerformance.Count > 0)
                {
                    foreach (KeyValuePair<string, long> tmpKeyValue in destinationUnitPKG.dicPerformance)
                    {
                        sumUnitPKG.dicPerformance[tmpKeyValue.Key] += tmpKeyValue.Value;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UnitPackageSpreadDisplay(int intUnitPackageStartRowIndex, UnitPKG tmpPKG)
        {
            int intUnitRow = intUnitPackageStartRowIndex;
            spdData.ActiveSheet.Cells[intUnitRow + 0, 3].Text = "Equipment number";
            spdData.ActiveSheet.Cells[intUnitRow + 1, 3].Text = "CAPA";
            spdData.ActiveSheet.Cells[intUnitRow + 2, 3].Text = "WIP";
            spdData.ActiveSheet.Cells[intUnitRow + 2, 4].Text = "Chip";
            spdData.ActiveSheet.Cells[intUnitRow + 3, 4].Text = "Merge";
            spdData.ActiveSheet.Cells[intUnitRow + 4, 3].Text = "actual";
            spdData.ActiveSheet.Cells[intUnitRow + 5, 3].Text = "Expected Results";

            spdData.ActiveSheet.Cells[intUnitRow + 0, 3].ColumnSpan = 2;
            spdData.ActiveSheet.Cells[intUnitRow + 1, 3].ColumnSpan = 2;
            spdData.ActiveSheet.Cells[intUnitRow + 4, 3].ColumnSpan = 2;
            spdData.ActiveSheet.Cells[intUnitRow + 5, 3].ColumnSpan = 2;

            spdData.ActiveSheet.Cells[intUnitRow + 2, 3].RowSpan = 2;
            spdData.ActiveSheet.Cells[intUnitRow + 2, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            long lngSumDA = 0;
            long lngSumWB = 0;
            long lngSumDAForecastPFM = 0;
            long lngSumWBForecastPFM = 0;
            int  ii = 7;

            //설비대수
            try
            {
                ii = 7; lngSumDA = 0; lngSumWB = 0;
                foreach (KeyValuePair<string, long> tmpKeyValue in tmpPKG.dicRes)
                {
                    if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 0, ii].Value = tmpKeyValue.Value;
                    ii++;

                    if (tmpKeyValue.Key.Contains("DA") == true) lngSumDA += tmpKeyValue.Value;
                    if (tmpKeyValue.Key.Contains("WB") == true) lngSumWB += tmpKeyValue.Value;
                }
                if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 0, 5].Value = lngSumDA;
                if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 0, 6].Value = lngSumWB;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            //CAPA
            try
            {
                ii = 7; lngSumDA = 0; lngSumWB = 0;
                foreach (KeyValuePair<string, long> tmpKeyValue in tmpPKG.dicCapa)
                {
                    if (chkKcps.Checked == false)
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 1, ii].Value = tmpKeyValue.Value;
                    }
                    else
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 1, ii].Value = Math.Round( tmpKeyValue.Value / 1000.0 );
                    }

                    ii++;
                    if (tmpKeyValue.Key.Contains("DA") == true) lngSumDA += tmpKeyValue.Value;
                    if (tmpKeyValue.Key.Contains("WB") == true) lngSumWB += tmpKeyValue.Value;
                }

                if (chkKcps.Checked == false) 
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 1, 5].Value = lngSumDA;
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 1, 6].Value = lngSumWB;
                } 
                else 
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 1, 5].Value = Math.Round(lngSumDA / 1000.0);
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 1, 6].Value = Math.Round(lngSumWB / 1000.0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            //재공 Chip
            try
            {
                ii = 7; lngSumDA = 0; lngSumWB = 0;
                foreach (KeyValuePair<string, long> tmpKeyValue in tmpPKG.dicLotChip)
                {
                    if (chkKcps.Checked == false)
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 2, ii].Value = tmpKeyValue.Value;
                    }
                    else
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 2, ii].Value = Math.Round(tmpKeyValue.Value / 1000.0);
                    }

                    ii++;
                    if (tmpKeyValue.Key.Contains("DA") == true) lngSumDA += tmpKeyValue.Value;
                    if (tmpKeyValue.Key.Contains("WB") == true) lngSumWB += tmpKeyValue.Value;
                }

                if (chkKcps.Checked == false)
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 2, 5].Value = lngSumDA;
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 2, 6].Value = lngSumWB;
                }
                else
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 2, 5].Value = Math.Round(lngSumDA / 1000.0);
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 2, 6].Value = Math.Round(lngSumWB / 1000.0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            //재공 Merge
            try
            {
                ii = 7; lngSumDA = 0; lngSumWB = 0;
                foreach (KeyValuePair<string, long> tmpKeyValue in tmpPKG.dicLotMerge)
                {
                    if (chkKcps.Checked == false)
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 3, ii].Value = tmpKeyValue.Value;
                    }
                    else
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 3, ii].Value = Math.Round( tmpKeyValue.Value / 1000.0);
                    }

                    ii++;
                    if (tmpKeyValue.Key.Contains("DA") == true) lngSumDA += tmpKeyValue.Value;
                    if (tmpKeyValue.Key.Contains("WB") == true) lngSumWB += tmpKeyValue.Value;
                }

                if (chkKcps.Checked == false)
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 3, 5].Value = lngSumDA;
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 3, 6].Value = lngSumWB;
                }
                else
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 3, 5].Value = Math.Round(lngSumDA / 1000.0);
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 3, 6].Value = Math.Round(lngSumWB / 1000.0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }





            DateTime dt = DateTime.Now;
            int intNowHour = dt.Hour;
            int intNowMinute = dt.Minute;

            // Hour와 Minute를 합쳐서 분단위로
            // 22시 이전은 전일 22시부터 24시까 두시간 합산
            long lngChgMinute = (intNowHour * 60) + intNowMinute;
            if (intNowHour < 22)
                lngChgMinute += 120;
            else
                lngChgMinute = intNowMinute - (22 * 60);


            //실적,예상실적
            try
            {
                ii = 7; lngSumDA = 0; lngSumWB = 0;
                lngSumDAForecastPFM = 0; lngSumWBForecastPFM = 0;
                foreach (KeyValuePair<string, long> tmpKeyValue in tmpPKG.dicPerformance)
                {
                    //실적
                    if (chkKcps.Checked == false)
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 4, ii].Value = tmpKeyValue.Value;
                    }
                    else
                    {
                        if (tmpKeyValue.Value != 0) spdData.ActiveSheet.Cells[intUnitRow + 4, ii].Value = Math.Round( tmpKeyValue.Value / 1000.0 );
                    }

                    if (tmpKeyValue.Key.Contains("DA") == true) lngSumDA += tmpKeyValue.Value;
                    if (tmpKeyValue.Key.Contains("WB") == true) lngSumWB += tmpKeyValue.Value;


                    if (DateTime.Now.ToString("yyyyMMdd") == cdvDate.SelectedValue())
                    {
                        //예상실적
                        long lngTmp = tmpKeyValue.Value / lngChgMinute * 60 * 24;

                        if (chkKcps.Checked == false)
                        {
                            if (lngTmp != 0) spdData.ActiveSheet.Cells[intUnitRow + 5, ii].Value = lngTmp;
                        }
                        else
                        {
                            if (lngTmp != 0) spdData.ActiveSheet.Cells[intUnitRow + 5, ii].Value = Math.Round(lngTmp / 1000.0);
                        }

                        if (tmpKeyValue.Key.Contains("DA") == true) lngSumDAForecastPFM += lngTmp;
                        if (tmpKeyValue.Key.Contains("WB") == true) lngSumWBForecastPFM += lngTmp;
                    }
                    else //다른일짜 조회시 집계하지 않음
                    {                           
                    }

                    ii++;
                }
                if (chkKcps.Checked == false)
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 4, 5].Value = lngSumDA;
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 4, 6].Value = lngSumWB;

                    if (lngSumDAForecastPFM != 0) spdData.ActiveSheet.Cells[intUnitRow + 5, 5].Value = lngSumDAForecastPFM;
                    if (lngSumWBForecastPFM != 0) spdData.ActiveSheet.Cells[intUnitRow + 5, 6].Value = lngSumWBForecastPFM;
                }
                else
                {
                    if (lngSumDA != 0) spdData.ActiveSheet.Cells[intUnitRow + 4, 5].Value = Math.Round(lngSumDA/ 1000.0);
                    if (lngSumWB != 0) spdData.ActiveSheet.Cells[intUnitRow + 4, 6].Value = Math.Round(lngSumWB/ 1000.0);

                    if (lngSumDAForecastPFM != 0) spdData.ActiveSheet.Cells[intUnitRow + 5, 5].Value = Math.Round(lngSumDAForecastPFM / 1000.0);
                    if (lngSumWBForecastPFM != 0) spdData.ActiveSheet.Cells[intUnitRow + 5, 6].Value = Math.Round(lngSumWBForecastPFM / 1000.0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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

        #endregion

        #region "Imitate Excel Sum"
        private bool IsNumeric(string value)
        {
            value = value.Trim();
            value = value.Replace(".", "");

            foreach (char cData in value)
            {
                if (false == Char.IsNumber(cData))
                {
                    return false;
                }
            }
            return true;
        }

        private void spdData_KeyUp(object sender, KeyEventArgs e)
        {
            if (spdData.ActiveSheet.RowCount <= 0) return;

            FarPoint.Win.Spread.Model.CellRange crRange;

            try
            {
                crRange = spdData.ActiveSheet.GetSelection(0);
                spdData.ActiveSheet.GetClip(crRange.Row, crRange.Column, crRange.RowCount, crRange.ColumnCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + "You didn't make a selection!!");
                return;
            }

            double dblSum = 0; long lngCnt = 0;
            for (int ii = crRange.Row; ii < crRange.Row + crRange.RowCount; ii++)
            {
                for (int kk = crRange.Column; kk < crRange.Column + crRange.ColumnCount; kk++)
                {
                    string strTmpValue = spdData.ActiveSheet.Cells[ii, kk].Text;
                    strTmpValue = strTmpValue.Replace(",", "");
                    if (strTmpValue.Trim() == "") strTmpValue = "0";

                    if (this.IsNumeric(strTmpValue))
                    {
                        dblSum += double.Parse(strTmpValue);
                    }
                    else
                    {
                        lblNumericSum.Text = "Characters included.";
                        return;
                    }
                    lngCnt++;
                }
            }

            if (lngCnt > 0 && dblSum != 0)
                lblNumericSum.Text = "개수: " + lngCnt.ToString("#,###").Trim() + "    합계: " + dblSum.ToString("#,###").Trim();
            else
                lblNumericSum.Text = "";
        }

        private void spdData_MouseUp(object sender, MouseEventArgs e)
        {
            spdData_KeyUp(sender, new KeyEventArgs(Keys.Return));
        }
        #endregion
    }
}
