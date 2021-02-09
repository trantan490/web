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

using System.Windows.Forms.DataVisualization.Charting;

namespace Hana.RAS
{

    /// <summary>
    /// 클  래  스: PQC030110<br/>
    /// 클래스요약: 공정&출하검사현황<br/>
    /// 작  성  자: 미라콤 <br/>
    /// 최초작성일: 2009-01-20<br/>
    /// 상세  설명: 공정&출하검사현황<br/>
    /// 변경  내용: [2009-08-18] 장은희  <br/>
    /// 
    /// 2009-09-28-임종우 : 챠트 쿼리 전체 수정(스프레드와 동일 데이타)
    ///                     PPM 공식 변경. 불량종류별불량률,주기별불량률은 전체Lot의 샘플링수량 SUM으로 구함.
    /// 2011-03-03-임종우 : 외관검사만 가져옴.. CMF_10 = ' ' -> 외관검사, CMF_10 = 'Y' -> 특성검사
    /// 2011-09-02-배수민 : 날짜Control을 시간대별로 조회할 수 있게 추가, 각 공정별로 조회할 수 있게 추가 (품질팀 김용복K 요청)
    /// 2012-02-29-김민우 : 판정결과 추가 (품질팀 한혜정S 요청)
    /// 2012-04-09-배수민 : 조립SITE 추가 (품질팀 한혜정S 요청)
    /// 2012-10-17-김민우 : HMKT1 - 월,주 추가 표기(황혜리S 요청)
    /// 2015-03-06-오득연 : ChartFX -> MS Chart로 변경
    /// 2016-07-19-임종우 : 검사원 정보 추가 (박용성D 요청)
    /// 2020-02-28-김미경 : 조회할 때마다 초기화되는 현상 조치
    /// 2020-06-04-김미경 : NCF CODE 추가 (곽연서 책임)
    /// </summary>
    public partial class PQC030110 : Miracom.SmartWeb.UI.Controls.udcCUSReport002
    {
        // 불량명을 컬럼화 하기 위해
        private DataTable dtChar = null;
        private DataTable dt = null;
        private StringBuilder strSqlString1 = new StringBuilder();
        private int i_index = 0;

        public PQC030110()
        {

            InitializeComponent();
            udcDate.AutoBinding();
            SortInit();
            GridColumnInit();
            udcMSChart1.RPT_1_ChartInit();  //차트 초기화. 

            cboGraph.SelectedIndex = 0;
            cboList.SelectedIndex = 0;

            this.SetFactory(GlobalVariable.gsAssyDefaultFactory);
            cdvFactory.Text = GlobalVariable.gsAssyDefaultFactory;
            this.cdvOper.sFactory = GlobalVariable.gsAssyDefaultFactory;
            this.cdvSite.sFactory = GlobalVariable.gsAssyDefaultFactory;
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

        #region  한줄헤더생성
        /// <summary>
        /// 2. 헤더 생성
        /// </summary>
        private void GridColumnInit()
        {
            spdData.ActiveSheet.ColumnHeader.Rows.Count = 0;
            int last_row_seq = 0;
            // spdData.ActiveSheet.RowHeader.ColumnCount = 0 ;

            spdData.RPT_ColumnInit();

            if (cboList.SelectedIndex == -1 || cboList.SelectedIndex == 0)
            {
                #region 외관검사
                if (cdvFactory.Text.Trim() != "HMKB1")
                {
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 130);

                    if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 4 || cboGraph.SelectedIndex == 5 || cboGraph.SelectedIndex == 6)
                    {
                        #region Graph : 0, 4, 5, 6
                        //2012-10-17-김민우 : HMKT1 - 월,주 추가 표기(황혜리S 요청)
                        if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory) && cboGraph.SelectedIndex == 0)
                        {
                            spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Date of inspection", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Inspection Work week", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Inspection month", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Type of inspection", 0, 13, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Part No", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Lot No", 0, 15, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Equipment number", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Lot Q'ty", 0, 17, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Customer", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Assembly site", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Lead Count", 0, 20, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("PKG", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Density", 0, 22, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Inspector", 0, 23, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Inspection quantity", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Defect quantity", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Defective Rate (ppm)", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Confirm", 0, 27, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            //김민우
                            spdData.RPT_AddBasicColumn("Judgment result", 0, 28, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

                            //불량명 컬럼화
                            if (dt != null)
                            {
                                for (int i = 29; i < dt.Columns.Count - 29; i++)
                                {
                                    spdData.RPT_AddBasicColumn(dt.Columns[29].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 120);
                                }
                            }

                            last_row_seq = 28;
                        }
                        else
                        {

                            if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0)
                            {
                                spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Date of inspection", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Type of inspection", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            }
                            else if (cboGraph.SelectedIndex == 4)
                            {
                                spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Type of inspection", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Date of inspection", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            }
                            else if (cboGraph.SelectedIndex == 5)
                            {
                                spdData.RPT_AddBasicColumn("Date of inspection", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Operation", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Type of inspection", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            }
                            else if (cboGraph.SelectedIndex == 6)
                            {
                                spdData.RPT_AddBasicColumn("Type of inspection", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Operation", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                                spdData.RPT_AddBasicColumn("Date of inspection", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                            }

                            spdData.RPT_AddBasicColumn("Part No", 0, 12, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Lot No", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Equipment number", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Lot Q'ty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Customer", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Assembly site", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Lead Count", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("PKG", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Density", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Inspector", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Inspection quantity", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Defect quantity", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Defective Rate (ppm)", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            spdData.RPT_AddBasicColumn("Confirm", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                            //김민우
                            spdData.RPT_AddBasicColumn("Judgment result", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

                            //불량명 컬럼화
                            if (dt != null)
                            {
                                for (int i = 27; i < dt.Columns.Count - 27; i++)
                                {
                                    spdData.RPT_AddBasicColumn(dt.Columns[27].ToString(), 0, spdData.ActiveSheet.Columns.Count, Visibles.True, Frozen.False, Align.Right, Merge.True, Formatter.String, 120);
                                }
                            }

                            last_row_seq = 26;

                        #endregion
                        }
                    }
                    else if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 2)
                    {
                        #region Graph : 1.불량 종류별 불량 발생 건 / 2.불량 종류 별 불량율(PPM)

                        spdData.RPT_AddBasicColumn("Type of defect", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Operation", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Date of inspection", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type of inspection", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Part No", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Lot No", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Equipment number", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Lot Q'ty", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Customer", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Assembly site", 0, 18, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Lead Count", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("PKG", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Density", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Inspector", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Inspection quantity", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Defect quantity", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Defective Rate (ppm)", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

                        last_row_seq = 25;

                        #endregion
                    }
                    else if (cboGraph.SelectedIndex == 3 || cboGraph.SelectedIndex == 7)
                    {
                        #region  Graph : 3.공정 별 불량 발생건/ 7.

                        spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type of defect", 0, 10, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Date of inspection", 0, 11, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Type of inspection", 0, 12, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Part No", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Lot No", 0, 14, Visibles.True, Frozen.False, Align.Left, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Equipment number", 0, 15, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Lot Q'ty", 0, 16, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Customer", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Assembly site", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Lead Count", 0, 19, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("PKG", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Density", 0, 21, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Inspector", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Inspection quantity", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Defect quantity", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                        spdData.RPT_AddBasicColumn("Defective Rate (ppm)", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);

                        last_row_seq = 25;

                        #endregion
                    }

                    // TEST 외관검사일 경우에만 NCF CODE 컬럼 보임
                    if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory && (cboList.SelectedIndex == -1 || cboList.SelectedIndex == 0))
                    {
                        spdData.RPT_AddBasicColumn("NCF CODE", 0, last_row_seq + 1, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 100);
                    }
                }
                else
                {
                    spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("BUMP TYPE", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("PROCESS FLOW", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 100);
                    spdData.RPT_AddBasicColumn("LAYER", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("PKG TYPE", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("RDL PLATING", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("FINAL BUMP", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("SUB. MATERIAL", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 110);
                    spdData.RPT_AddBasicColumn("SIZE", 0, 8, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                    spdData.RPT_AddBasicColumn("THICKNESS", 0, 9, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                    spdData.RPT_AddBasicColumn("FLAT TYPE", 0, 10, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("WAFER", 0, 11, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 120);

                    spdData.RPT_AddBasicColumn("Operation", 0, 12, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Date of inspection", 0, 13, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Purpose of inspection", 0, 14, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Customer", 0, 15, Visibles.True, Frozen.True, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lot No", 0, 16, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("product information", 0, 17, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Bumping Type", 0, 18, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Final Bump", 0, 19, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Cassette ID", 0, 20, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Equipment number", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                    spdData.RPT_AddBasicColumn("Lot Q'ty", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Inspection quantity", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                    spdData.RPT_AddBasicColumn("Judgment result", 0, 24, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 80);
                }

                #endregion
            }

            #region   특성검사
            else if (cboList.SelectedIndex == 1)
            {

                // 특성검사 결과
                spdData.RPT_ColumnInit();

                spdData.RPT_AddBasicColumn("CUSTOMER", 0, 0, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 70);
                spdData.RPT_AddBasicColumn("FAMILY", 0, 1, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("PACKAGE", 0, 2, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("TYPE1", 0, 3, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("TYPE2", 0, 4, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("LD COUNT", 0, 5, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("DENSITY", 0, 6, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 60);
                spdData.RPT_AddBasicColumn("GENERATION", 0, 7, Visibles.False, Frozen.False, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("PIN TYPE", 0, 8, Visibles.True, Frozen.False, Align.Center, Merge.True, Formatter.String, 130);

                spdData.RPT_AddBasicColumn("Operation", 0, 9, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Date of inspection", 0, 10, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Type of inspection", 0, 11, Visibles.True, Frozen.True, Align.Center, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Part No", 0, 12, Visibles.True, Frozen.True, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot No", 0, 13, Visibles.True, Frozen.False, Align.Left, Merge.True, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Equipment number", 0, 14, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lot Q'ty", 0, 15, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Customer", 0, 16, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Assembly site", 0, 17, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Lead 수", 0, 18, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("PKG", 0, 19, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Density", 0, 20, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Inspector", 0, 21, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 80);
                spdData.RPT_AddBasicColumn("Inspection quantity", 0, 22, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Defect quantity", 0, 23, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("구분", 0, 24, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Spec" + "\n" + "(Min)", 0, 25, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);
                spdData.RPT_AddBasicColumn("Spec" + "\n" + "(Max)", 0, 26, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.String, 50);

                spdData.RPT_MerageHeaderRowSpan(0, 0, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 1, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 2, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 3, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 4, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 5, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 6, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 7, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 8, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 9, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 10, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 11, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 12, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 13, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 14, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 15, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 16, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 17, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 18, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 19, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 20, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 21, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 22, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 23, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 24, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 25, 2);
                spdData.RPT_MerageHeaderRowSpan(0, 26, 2);

                if (dt != null)
                {

                    // Value_1~25까지 컬럼값을 넣어주기 위한 For문.
                    spdData.RPT_AddBasicColumn("값(Value)", 0, 27, Visibles.True, Frozen.False, Align.Center, Merge.False, Formatter.String, 50);

                    int headerCount = 27;


                    for (int i = 1; i <= 25; i++)
                    {
                        spdData.RPT_AddBasicColumn("\"" + i + "\"", 1, headerCount, Visibles.True, Frozen.False, Align.Right, Merge.False, Formatter.Double2, 50);
                        headerCount++;
                    }
                    spdData.RPT_MerageHeaderColumnSpan(0, 27, 25);
                }
            }
            #endregion

            //spdData.RPT_ColumnConfigFromTable(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
            spdData.RPT_ColumnConfigFromTable_New(btnSort); //Group항목이 있을경우 반드시 선언해줄것.
        }
        #endregion


        /// <summary>
        /// 3. Group By 정의
        /// </summary>

        #region   GROUP BY
        private void SortInit()
        {
            ((udcTableForm)(this.btnSort.BindingForm)).Clear();
            if (cdvFactory.Text.Trim() != "HMKB1")
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FAMILY", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS FAMILY", "MIN(FAB_SEQ) AS FAMILY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PACKAGE", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PACKAGE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE1", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS TYPE1", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("TYPE2", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS TYPE2", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("LD COUNT", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS \"LD COUNT\"", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("DENSITY", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS DENSITY", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("GENERATION", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS GENERATION", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PIN TYPE", "MAT.MAT_CMF_10", "MAT.MAT_CMF_10 AS PIN_TYPE", false);
            }
            else
            {
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("CUSTOMER", "MAT.MAT_GRP_1", "MAT_GRP_1", "NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT_GRP_1 AND ROWNUM=1), '-') AS CUSTOMER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("BUMPING TYPE", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2", "MAT.MAT_GRP_2 AS BUMPING_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PROCESS FLOW", "MAT.MAT_GRP_3", "MAT.MAT_GRP_3 AS PROCESS_FLOW", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("Layer classification", "MAT.MAT_GRP_4", "MAT.MAT_GRP_4 AS LAYER", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("PKG TYPE", "MAT.MAT_GRP_5", "MAT.MAT_GRP_5 AS PKG_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("RDL PLATING", "MAT.MAT_GRP_6", "MAT.MAT_GRP_6 AS RDL_PLATING", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FINAL BUMP", "MAT.MAT_GRP_7", "MAT.MAT_GRP_7 AS FINAL_BUMP", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SUB. MATERIAL", "MAT.MAT_GRP_8", "MAT.MAT_GRP_8 AS SUB_MATERIAL", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("SIZE", "MAT.MAT_CMF_14", "MAT.MAT_CMF_14 AS WF_SIZE", false); // \"SIZE\"
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("THICKNESS", "MAT.MAT_CMF_2", "MAT.MAT_CMF_2 AS THICKNESS", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("FLAT TYPE", "MAT.MAT_CMF_3", "MAT.MAT_CMF_3 AS FLAT_TYPE", false);
                ((udcTableForm)(this.btnSort.BindingForm)).AddRow("WAFER ORIENTATION", "MAT.MAT_CMF_4", "MAT.MAT_CMF_4 AS WAFER_ORIENTATION", false);
            }

        }
        #endregion

        /// <summary>
        /// 4. 쿼리 생성
        /// </summary>
        /// <returns></returns>
        /// 

        #region MakeSqlChar  // 불량명 < 컬럼화,해야하고 여기저기서 필요한 관계로
        private string MakeSqlChar()
        {
            StringBuilder strSqlString1 = new StringBuilder();
            string QueryCond1 = null;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;

            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            #region " udcDuration에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            strFromDate = udcDate.Start_Tran_Time;
            strToDate = udcDate.End_Tran_Time;
            #endregion

            strSqlString1.Append("SELECT DISTINCT CHAR_ID " + "\n");
            strSqlString1.Append(" FROM(  " + "\n");
            strSqlString1.Append("       SELECT NVL((SELECT CHAR_ID " + "\n");
            strSqlString1.Append("                     FROM MEDCLOTDAT " + "\n");
            if (cboList.SelectedIndex == 0)
            {
                strSqlString1.Append("         WHERE  1=1 AND COL_SET_ID IN (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND COL_GRP_10='Visual'  AND DELETE_FLAG <> 'Y'   " + "\n");
            }

            strSqlString1.Append("            AND DELETE_FLAG <> 'Y' AND FACTORY=PQ.FACTORY)  " + "\n");
            strSqlString1.Append("          AND LOT_ID = PQ.LOT_ID  AND VALUE_1 <> ' '  AND HIST_SEQ= PQ.HIST_SEQ and rownum=1),'-') AS CHAR_ID , TOTAL_DEFECT_QTY_1 " + "\n");
            strSqlString1.Append("       FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
            strSqlString1.Append("               , MWIPMATDEF MAT   " + "\n");
            strSqlString1.Append("      WHERE 1 = 1 " + "\n");
            strSqlString1.AppendFormat("              AND PQ.FACTORY = '{0}'  \n", cdvFactory.Text.Trim());
            strSqlString1.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
            strSqlString1.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
            strSqlString1.AppendFormat("        AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
            strSqlString1.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

            //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
            if (cdvOper.Text != "")
            {
                strSqlString1.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
            }

            #region 상세 조회에 따른 SQL문 생성
            if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

            if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

            if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

            if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

            if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

            if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

            if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

            if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

            if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                strSqlString1.AppendFormat("              AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);
            #endregion

            if (cboList.SelectedIndex == 0)
            {
                strSqlString1.Append("    ) WHERE TOTAL_DEFECT_QTY_1 > 0  " + "\n");
            }

            if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
            {
                System.Windows.Forms.Clipboard.SetText(strSqlString1.ToString());
            }

            return strSqlString1.ToString();
        }
        #endregion

        private string MakeSqlString(int nIndex)
        {

            StringBuilder strSqlString = new StringBuilder();

            string[] selectDate1 = new string[udcDate.SubtractBetweenFromToDate + 1];
            string strFromDate = udcDate.Start_Tran_Time;
            string strToDate = udcDate.End_Tran_Time;
            string strDate = string.Empty;
            string QueryCond1 = null;

            //추가
            udcTableForm tableForm = (udcTableForm)btnSort.BindingForm;
            QueryCond1 = tableForm.SelectedValueToQueryContainNull;

            int Between = udcDate.SubtractBetweenFromToDate + 1;

            //selectDate1 = udcDate.;

            switch (udcDate.DaySelector.SelectedValue.ToString())
            {
                case "DAY":
                    strDate = "WORK_DATE";
                    break;
                case "WEEK":
                    strDate = "WORK_WEEK";
                    break;
                case "MONTH":
                    strDate = "WORK_MONTH";
                    break;
                default:
                    strDate = "WORK_DATE";
                    break;
            }

            switch (nIndex)
            {
                case 0:  // Main : '-' 
                    {
                        #region " SPREAD"

                        #region  외관검사인경우
                        if (cboList.SelectedIndex == -1 || cboList.SelectedIndex == 0)
                        {
                            if (cdvFactory.Text.Trim() != "HMKB1")
                            {
                                if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0 || cboGraph.SelectedIndex == 4 || cboGraph.SelectedIndex == 5 || cboGraph.SelectedIndex == 6)
                                {
                                    #region  Graph : 0(-, Main), 4, 5, 6
                                    strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                                    //2012-10-17-김민우 : HMKT1 - 월,주 추가 표기(황혜리S 요청)
                                    if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory) && cboGraph.SelectedIndex == 0)
                                    {
                                        strSqlString.Append("       , QC_OPER, TRAN_TIME,PLAN_WEEK,PLAN_MONTH, QC_TYPE " + "\n");
                                    }
                                    else
                                    {
                                        if (cboGraph.SelectedIndex == -1 || cboGraph.SelectedIndex == 0)
                                        {
                                            strSqlString.Append("       , QC_OPER, TRAN_TIME, QC_TYPE " + "\n");
                                        }
                                        else if (cboGraph.SelectedIndex == 4)
                                        {
                                            strSqlString.Append("       , QC_OPER, QC_TYPE, TRAN_TIME " + "\n");
                                        }
                                        else if (cboGraph.SelectedIndex == 5)
                                        {
                                            strSqlString.Append("       , TRAN_TIME, QC_OPER, QC_TYPE " + "\n");
                                        }
                                        else if (cboGraph.SelectedIndex == 6)
                                        {
                                            strSqlString.Append("       , QC_TYPE, QC_OPER, TRAN_TIME " + "\n");
                                        }
                                    }

                                    strSqlString.Append("       , MAT_ID, LOT_ID, RES_ID, QTY_1, CUSTOMER, ASSE_SITE, LEAD_COUNT, H_PACKAGE, DENSITY, QC_OPERATOR ,SAMPLE_QTY " + "\n");
                                    strSqlString.Append("       , SUM(TOTAL_DEFECT_QTY_1) AS TOTAL_QTY  , PPM, CMF_2 " + "\n");
                                    //김민우
                                    strSqlString.Append("       , DECODE(QC_FLAG, 'Y', 'PASS', 'FAIL') AS QC_FLAG  " + "\n");

                                    if (dtChar != null) //  CHAR_ID 컬럼값을 컬럼명으로 바꿔주고
                                    {
                                        for (int i = 0; i < dtChar.Rows.Count; i++)
                                        {
                                            strSqlString.AppendFormat("       , SUM(DECODE(CHAR_ID , '" + dtChar.Rows[i]["CHAR_ID"].ToString() + "' ,TOTAL_DEFECT_QTY_1,0))  AS \"" + dtChar.Rows[i]["CHAR_ID"].ToString() + "\"" + '\n');
                                        }
                                    }

                                    // 김미경
                                    if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory)
                                    {
                                        strSqlString.Append("       , (SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND ATTR_TYPE = 'LOT_SEC_INFO' AND ATTR_NAME = 'NCFCODE' AND ATTR_KEY = LOT_ID) AS NCF   " + "\n");
                                    }
                                    strSqlString.Append(" FROM( " + "\n");
                                    strSqlString.Append("       SELECT " + QueryCond1 + " " + "\n");
                                    strSqlString.Append("              , PQ.QC_OPER " + "\n");
                                    strSqlString.Append("              , PQ.TRAN_TIME " + "\n");
                                    strSqlString.Append("              , PQ.QC_TYPE " + "\n");
                                    strSqlString.Append("              , PQ.MAT_ID " + "\n");
                                    strSqlString.Append("              , PQ.LOT_ID " + "\n");
                                    strSqlString.Append("              , PQ.RES_ID, PQ.QC_FLAG, PQ.QC_OPERATOR " + "\n");
                                    strSqlString.Append("              , WI.QTY_1  " + "\n");
                                    strSqlString.Append("              , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.Trim() + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PQ.CUSTOMER AND ROWNUM=1), '-')  AS CUSTOMER " + "\n");
                                    strSqlString.Append("              , TRIM(STS.LOT_CMF_7) AS ASSE_SITE, PQ.LEAD_COUNT, PQ.H_PACKAGE, PQ.DENSITY, PQ.SAMPLE_QTY, PQ.TOTAL_DEFECT_QTY_1, CMF_2 " + "\n");
                                    strSqlString.Append("              , DECODE(PQ.SAMPLE_QTY,0,0,ROUND((((PQ.TOTAL_DEFECT_QTY_1)/(PQ.SAMPLE_QTY))*1000000),0)) PPM   " + "\n");
                                    strSqlString.Append("              , NVL((SELECT CHAR_ID " + "\n");
                                    strSqlString.Append("                       FROM MEDCLOTDAT " + "\n");
                                    strSqlString.Append("                      WHERE  1=1  " + "\n");
                                    strSqlString.Append("                             AND COL_SET_ID IN ( " + "\n");
                                    strSqlString.Append("                                                SELECT COL_SET_ID " + "\n");
                                    strSqlString.Append("                                                  FROM MEDCCOLDEF " + "\n");
                                    strSqlString.Append("                                                 WHERE 1=1 " + "\n");
                                    strSqlString.Append("                                                       AND COL_GRP_10='Visual' " + "\n");
                                    strSqlString.Append("                                                       AND DELETE_FLAG <> 'Y' " + "\n");
                                    strSqlString.Append("                                                      AND FACTORY=PQ.FACTORY " + "\n");
                                    strSqlString.Append("                                                )  " + "\n");
                                    strSqlString.Append("                              AND LOT_ID = WI.LOT_ID " + "\n");
                                    strSqlString.Append("                              AND VALUE_1 <> ' ' " + "\n");
                                    strSqlString.Append("                              AND HIST_SEQ= PQ.HIST_SEQ " + "\n");
                                    strSqlString.Append("                              AND ROWNUM = 1 ) " + "\n");
                                    strSqlString.Append("                      ,'-') AS CHAR_ID  " + "\n");
                                    strSqlString.Append("         FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                                    strSqlString.Append("              , MWIPLOTHIS@RPTTOMES WI  " + "\n");
                                    strSqlString.Append("              , MWIPMATDEF MAT   " + "\n");
                                    strSqlString.Append("              , MWIPLOTSTS STS   " + "\n");
                                    strSqlString.Append("        WHERE 1 = 1 " + "\n");
                                    strSqlString.Append("              AND PQ.FACTORY = WI.FACTORY " + "\n");
                                    strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                                    strSqlString.Append("              AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                                    strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                                    strSqlString.Append("              AND PQ.LOT_ID = WI.LOT_ID " + "\n");
                                    strSqlString.Append("              AND PQ.LOT_ID = STS.LOT_ID " + "\n");
                                    strSqlString.Append("              AND STS.LOT_CMF_7 " + cdvSite.SelectedValueToQueryString + "\n");
                                    strSqlString.Append("              AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                                    strSqlString.AppendFormat("              AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                    strSqlString.Append("              AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                                    strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                                    //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                                    if (cdvOper.Text != "")
                                    {
                                        strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                                    }

                                    // 2011-03-03-임종우 : 외관검사만 가져옴.. CMF_10 = Y 이면 특성검사 이다.
                                    strSqlString.Append("              AND PQ.CMF_10 = ' ' " + "\n");

                                    #region 상세 조회에 따른 SQL문 생성
                                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                                    if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                        strSqlString.AppendFormat("             AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                                    if (cdvType.Text != "ALL" && cdvType.Text != "")
                                        strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);

                                    #endregion

                                    strSqlString.Append("     ) " + "\n");
                                    // 2012-10-17-김민우 : HMKT1 - 월,주 추가 표기(황혜리S 요청)
                                    if (cdvFactory.Text.ToString().Equals(GlobalVariable.gsTestDefaultFactory) && cboGraph.SelectedIndex == 0)
                                    {
                                        strSqlString.Append("       A," + "\n");
                                        strSqlString.Append("     MWIPCALDEF CAL" + "\n");
                                        strSqlString.Append("   WHERE CAL.SYS_DATE = SUBSTR(TRAN_TIME,0,8)" + "\n");
                                        strSqlString.Append("     AND CALENDAR_ID = 'HM'" + "\n");
                                        strSqlString.Append("GROUP BY  " + QueryCond1 + " , QC_OPER, TRAN_TIME, PLAN_WEEK, PLAN_MONTH, QC_TYPE, MAT_ID, LOT_ID, RES_ID, QTY_1, CUSTOMER, ASSE_SITE, LEAD_COUNT, H_PACKAGE, DENSITY, QC_OPERATOR, SAMPLE_QTY, PPM, CMF_2, QC_FLAG " + "\n");
                                        strSqlString.Append("ORDER BY  " + QueryCond1 + " , QC_OPER, TRAN_TIME, PLAN_WEEK, PLAN_MONTH, QC_TYPE, MAT_ID, LOT_ID, RES_ID, QTY_1, CUSTOMER, ASSE_SITE, LEAD_COUNT, H_PACKAGE, DENSITY, QC_OPERATOR, SAMPLE_QTY, PPM, CMF_2, QC_FLAG " + "\n");
                                    }
                                    else
                                    {

                                        strSqlString.Append("GROUP BY  " + QueryCond1 + " , QC_OPER, TRAN_TIME, QC_TYPE, MAT_ID, LOT_ID, RES_ID, QTY_1, CUSTOMER, ASSE_SITE, LEAD_COUNT, H_PACKAGE, DENSITY, QC_OPERATOR, SAMPLE_QTY, PPM, CMF_2, QC_FLAG " + "\n");
                                        strSqlString.Append("ORDER BY  " + QueryCond1 + " , QC_OPER, TRAN_TIME, QC_TYPE, MAT_ID, LOT_ID, RES_ID, QTY_1, CUSTOMER, ASSE_SITE, LEAD_COUNT, H_PACKAGE, DENSITY, QC_OPERATOR, SAMPLE_QTY, PPM, CMF_2, QC_FLAG " + "\n");
                                    }
                                    // strSqlString.Append(" ORDER BY  " + QueryCond1 + " " + "\n");
                                    #endregion
                                }
                                else if (cboGraph.SelectedIndex == 1 || cboGraph.SelectedIndex == 2)
                                {
                                    #region  Graph : 1.불량 종류별 불량 발생 건 / 2.불량 종류 별 불량율(PPM)

                                    strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                                    strSqlString.Append("       , NVL((SELECT CHAR_ID " + "\n");
                                    strSqlString.Append("                FROM MEDCLOTDAT " + "\n");
                                    strSqlString.Append("               WHERE 1=1 AND COL_SET_ID IN (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND COL_GRP_10='Visual'  AND DELETE_FLAG <> 'Y' AND FACTORY=PQ.FACTORY)  " + "\n");
                                    strSqlString.Append("                         AND COL_SET_ID IN ( " + "\n");
                                    strSqlString.Append("                                             SELECT COL_SET_ID " + "\n");
                                    strSqlString.Append("                                               FROM MEDCCOLDEF " + "\n");
                                    strSqlString.Append("                                              WHERE 1=1 " + "\n");
                                    strSqlString.Append("                                                    AND COL_GRP_10='Visual' " + "\n");
                                    strSqlString.Append("                                                    AND DELETE_FLAG <> 'Y' " + "\n");
                                    strSqlString.Append("                                                    AND FACTORY=PQ.FACTORY  " + "\n");
                                    strSqlString.Append("                                           )  " + "\n");
                                    strSqlString.Append("                         AND LOT_ID = WI.LOT_ID " + "\n");
                                    strSqlString.Append("                         AND VALUE_1 <> ' ' " + "\n");
                                    strSqlString.Append("                         AND HIST_SEQ= PQ.HIST_SEQ " + "\n");
                                    strSqlString.Append("                         AND ROWNUM = 1 ) " + "\n");
                                    strSqlString.Append("             ,'-') AS CHAR_ID  " + "\n");
                                    strSqlString.Append("       , PQ.QC_OPER, PQ.TRAN_TIME, PQ.QC_TYPE, PQ.MAT_ID, PQ.LOT_ID, PQ.RES_ID, WI.QTY_1  " + "\n");
                                    strSqlString.Append("       , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.Trim() + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PQ.CUSTOMER AND ROWNUM=1), '-')  AS CUSTOMER " + "\n");
                                    strSqlString.Append("       , TRIM(STS.LOT_CMF_7) AS ASSE_SITE, PQ.LEAD_COUNT, PQ.H_PACKAGE, PQ.DENSITY, PQ.QC_OPERATOR, PQ.SAMPLE_QTY, PQ.TOTAL_DEFECT_QTY_1 " + "\n");
                                    strSqlString.Append("       , DECODE(PQ.SAMPLE_QTY,0,0,ROUND((((PQ.TOTAL_DEFECT_QTY_1)/(PQ.SAMPLE_QTY))*1000000),0)) PPM   " + "\n");
                                    // 김미경
                                    if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory)
                                    {
                                        strSqlString.Append("       , (SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND ATTR_TYPE = 'LOT_SEC_INFO' AND ATTR_NAME = 'NCFCODE' AND ATTR_KEY = STS.LOT_ID) AS NCF   " + "\n");
                                    }
                                    strSqlString.Append("  FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                                    strSqlString.Append("       , MWIPLOTHIS@RPTTOMES WI  " + "\n");
                                    strSqlString.Append("       , MWIPMATDEF MAT   " + "\n");
                                    strSqlString.Append("       , MWIPLOTSTS STS   " + "\n");
                                    strSqlString.Append(" WHERE 1 = 1 " + "\n");
                                    strSqlString.Append("       AND PQ.FACTORY = WI.FACTORY " + "\n");
                                    strSqlString.Append("       AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                                    strSqlString.Append("       AND PQ.LOT_ID = WI.LOT_ID " + "\n");
                                    strSqlString.Append("       AND PQ.LOT_ID = STS.LOT_ID " + "\n");
                                    strSqlString.Append("       AND STS.LOT_CMF_7 " + cdvSite.SelectedValueToQueryString + "\n");
                                    strSqlString.Append("       AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                                    strSqlString.Append("       AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                                    strSqlString.Append("       AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                                    strSqlString.AppendFormat("       AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                    strSqlString.Append("       AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                                    strSqlString.Append("       AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                                    //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                                    if (cdvOper.Text != "")
                                    {
                                        strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                                    }

                                    // 2011-03-03-임종우 : 외관검사만 가져옴.. CMF_10 = Y 이면 특성검사 이다.
                                    strSqlString.Append("       AND PQ.CMF_10 = ' ' " + "\n");

                                    #region 상세 조회에 따른 SQL문 생성
                                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                        strSqlString.AppendFormat("       AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                                    if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                        strSqlString.AppendFormat("      AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                                    if (cdvType.Text != "ALL" && cdvType.Text != "")
                                        strSqlString.AppendFormat("       AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);

                                    #endregion

                                    strSqlString.Append("ORDER BY " + QueryCond1 + " " + "\n");
                                    strSqlString.Append("         , CHAR_ID DESC, MAT_ID, LOT_ID, TRAN_TIME, QC_OPER, QC_TYPE   " + "\n");

                                    #endregion
                                }

                                else if (cboGraph.SelectedIndex == 3 || cboGraph.SelectedIndex == 7)
                                {
                                    #region  Graph : 3.공정 별 불량 발생건  , 7.

                                    strSqlString.Append("    SELECT  " + QueryCond1 + " " + "\n");
                                    strSqlString.Append("          , PQ.QC_OPER   " + "\n");
                                    strSqlString.Append("          , NVL((SELECT CHAR_ID FROM MEDCLOTDAT   " + "\n");
                                    strSqlString.Append("                      WHERE  1=1 AND COL_SET_ID IN (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND COL_GRP_10='Visual'  AND DELETE_FLAG <> 'Y' AND FACTORY=PQ.FACTORY)  " + "\n");
                                    strSqlString.Append("                              AND LOT_ID = WI.LOT_ID  AND VALUE_1 <> ' '  AND HIST_SEQ= PQ.HIST_SEQ AND ROWNUM = 1 ),'-') AS CHAR_ID  " + "\n");
                                    strSqlString.Append("          , PQ.TRAN_TIME, PQ.QC_TYPE, PQ.MAT_ID, PQ.LOT_ID, PQ.RES_ID, WI.QTY_1  " + "\n");
                                    strSqlString.Append("          , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.Trim() + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PQ.CUSTOMER AND ROWNUM=1), '-')  AS CUSTOMER " + "\n");
                                    strSqlString.Append("          , TRIM(STS.LOT_CMF_7) AS ASSE_SITE, PQ.LEAD_COUNT, PQ.H_PACKAGE, PQ.DENSITY, PQ.QC_OPERATOR, PQ.SAMPLE_QTY, PQ.TOTAL_DEFECT_QTY_1 " + "\n");
                                    strSqlString.Append("          , DECODE(PQ.SAMPLE_QTY,0,0,ROUND((((PQ.TOTAL_DEFECT_QTY_1)/(PQ.SAMPLE_QTY))*1000000),0)) PPM   " + "\n");
                                    // 김미경
                                    if (cdvFactory.Text.Trim() == GlobalVariable.gsTestDefaultFactory)
                                    {
                                        strSqlString.Append("          , (SELECT ATTR_VALUE FROM MATRNAMSTS@RPTTOMES WHERE FACTORY = '" + cdvFactory.Text.Trim() + "' AND ATTR_TYPE = 'LOT_SEC_INFO' AND ATTR_NAME = 'NCFCODE' AND ATTR_KEY = STS.LOT_ID) AS NCF  " + "\n");
                                    }
                                    strSqlString.Append("       FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                                    strSqlString.Append("               , MWIPLOTHIS@RPTTOMES WI  " + "\n");
                                    strSqlString.Append("               , MWIPMATDEF MAT   " + "\n");
                                    strSqlString.Append("               , MWIPLOTSTS STS   " + "\n");
                                    strSqlString.Append("      WHERE 1 = 1 " + "\n");
                                    strSqlString.Append("              AND PQ.FACTORY = WI.FACTORY " + "\n");
                                    strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                                    strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                                    strSqlString.Append("              AND PQ.LOT_ID = WI.LOT_ID " + "\n");
                                    strSqlString.Append("              AND PQ.LOT_ID = STS.LOT_ID " + "\n");
                                    strSqlString.Append("              AND STS.LOT_CMF_7 " + cdvSite.SelectedValueToQueryString + "\n");
                                    strSqlString.Append("              AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                                    strSqlString.AppendFormat("              AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                    strSqlString.Append("              AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                                    strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                                    //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                                    if (cdvOper.Text != "")
                                    {
                                        strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                                    }

                                    // 2011-03-03-임종우 : 외관검사만 가져옴.. CMF_10 = Y 이면 특성검사 이다.
                                    strSqlString.Append("              AND PQ.CMF_10 = ' ' " + "\n");

                                    #region 상세 조회에 따른 SQL문 생성
                                    if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                                    if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                                    if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                                    if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                                    if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                                    if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                                    if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                                    if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                        strSqlString.AppendFormat("              AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                                    if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                        strSqlString.AppendFormat("             AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                                    if (cdvType.Text != "ALL" && cdvType.Text != "")
                                        strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                                    #endregion

                                    strSqlString.Append("       ORDER BY  " + QueryCond1 + " " + "\n");
                                    strSqlString.Append("              , PQ.QC_OPER , CHAR_ID DESC, PQ.MAT_ID, PQ.LOT_ID, PQ.TRAN_TIME   " + "\n");

                                    #endregion
                                }
                            }
                            else
                            {
                                #region  Bump Visual Raw Data
                                strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                                strSqlString.Append("       , QC_OPER, TRAN_TIME, QC_TYPE, CUSTOMER " + "\n");
                                strSqlString.Append("       , LOT_ID, MAT_ID, MAT_GRP_2, MAT_GRP_7, CRR_ID, RES_ID, QTY_1 " + "\n");
                                strSqlString.Append("       , SAMPLE_QTY " + "\n");
                                strSqlString.Append("       , DECODE(QC_FLAG, 'Y', 'PASS', 'FAIL') AS QC_FLAG  " + "\n");
                                strSqlString.Append(" FROM( " + "\n");
                                strSqlString.Append("       SELECT " + QueryCond1 + " " + "\n");
                                strSqlString.Append("              , PQ.QC_OPER " + "\n");
                                strSqlString.Append("              , PQ.TRAN_TIME " + "\n");
                                strSqlString.Append("              , PQ.QC_TYPE " + "\n");
                                strSqlString.Append("              , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.Trim() + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-')  AS CUSTOMER " + "\n");
                                strSqlString.Append("              , PQ.LOT_ID " + "\n");
                                strSqlString.Append("              , PQ.MAT_ID " + "\n");
                                strSqlString.Append("              , MAT.MAT_GRP_2 " + "\n");
                                strSqlString.Append("              , MAT.MAT_GRP_7 " + "\n");
                                strSqlString.Append("              , WI.CRR_ID " + "\n");
                                strSqlString.Append("              , PQ.RES_ID " + "\n");
                                strSqlString.Append("              , WI.QTY_1  " + "\n");
                                strSqlString.Append("              , PQ.SAMPLE_QTY " + "\n");
                                strSqlString.Append("              , PQ.QC_FLAG " + "\n");
                                strSqlString.Append("         FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                                strSqlString.Append("              , MWIPLOTHIS@RPTTOMES WI  " + "\n");
                                strSqlString.Append("              , MWIPMATDEF MAT   " + "\n");
                                strSqlString.Append("        WHERE 1 = 1 " + "\n");
                                strSqlString.AppendFormat("              AND PQ.FACTORY = '{0}' \n", cdvFactory.Text.Trim());
                                strSqlString.Append("              AND PQ.FACTORY = WI.FACTORY " + "\n");
                                strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                                strSqlString.Append("              AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                                strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                                strSqlString.Append("              AND PQ.LOT_ID = WI.LOT_ID " + "\n");
                                strSqlString.Append("              AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                                strSqlString.Append("              AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                                strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                                //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                                if (cdvOper.Text != "")
                                {
                                    strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                                }

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

                                if (cdvType.Text != "ALL" && cdvType.Text != "")
                                    strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);

                                strSqlString.Append("     ) " + "\n");
                                strSqlString.Append("ORDER BY  " + QueryCond1 + " , QC_OPER, TRAN_TIME, QC_TYPE, MAT_ID, LOT_ID " + "\n");

                                #endregion
                            }
                        }
                        #endregion

                        #region List : 특성검사
                        else if (cboList.SelectedIndex == 1)
                        {
                            if (cdvFactory.Text.Trim() != "HMKB1")
                            {
                                strSqlString.Append("    SELECT  " + QueryCond1 + " " + "\n");
                                strSqlString.Append("          , A.QC_OPER, A.TRAN_TIME,A.QC_TYPE, A.MAT_ID, A.LOT_ID, A.RES_ID, A.QTY_1, A.CUSTOMER, A.ASSE_SITE,A.LEAD_COUNT, A.H_PACKAGE, A.DENSITY, QC_OPERATOR, A.SAMPLE_QTY  " + "\n");
                                strSqlString.Append("          , A.TOTAL_DEFECT_QTY_1  ,B.CHAR_ID, B.LOWER_SPEC_LIMIT, B.UPPER_SPEC_LIMIT " + "\n");
                                strSqlString.Append("          , VALUE_1 , VALUE_2, VALUE_3, VALUE_4, VALUE_5, VALUE_6, VALUE_7, VALUE_8, VALUE_9, VALUE_10, VALUE_11, VALUE_12, VALUE_13, VALUE_14, VALUE_15 " + "\n");
                                strSqlString.Append("          , VALUE_16, VALUE_17, VALUE_18, VALUE_19, VALUE_20, VALUE_21, VALUE_22, VALUE_23, VALUE_24, VALUE_25   " + "\n");
                                strSqlString.Append("       FROM (   " + "\n");
                                strSqlString.Append("          SELECT  " + QueryCond1 + " " + "\n");
                                strSqlString.Append("               , PQ.QC_OPER, PQ.TRAN_TIME, PQ.QC_TYPE, PQ.MAT_ID, PQ.LOT_ID, PQ.RES_ID, WI.QTY_1  " + "\n");
                                strSqlString.Append("              , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.Trim() + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = PQ.CUSTOMER AND ROWNUM=1), '-')  AS CUSTOMER   " + "\n");
                                strSqlString.Append("              , TRIM(STS.LOT_CMF_7) AS ASSE_SITE, PQ.LEAD_COUNT, PQ.H_PACKAGE, PQ.DENSITY, PQ.SAMPLE_QTY, PQ.TOTAL_DEFECT_QTY_1   " + "\n");
                                strSqlString.Append("              , NVL((SELECT COL_SET_ID FROM MEDCLOTDAT   " + "\n");
                                strSqlString.Append("                          WHERE  1=1 AND COL_SET_ID IN (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND FACTORY=PQ.FACTORY AND COL_GRP_10='Measure'  AND DELETE_FLAG <> 'Y' ) " + "\n");
                                strSqlString.Append("                                  AND LOT_ID = WI.LOT_ID  AND VALUE_1 <> ' '  AND HIST_SEQ= PQ.HIST_SEQ  AND ROWNUM = 1 ),'-') AS COL_SET_ID   " + "\n");
                                strSqlString.Append("              , PQ.HIST_SEQ, PQ.FACTORY, PQ.QC_OPERATOR    " + "\n");
                                strSqlString.Append("        FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                                strSqlString.Append("               , MWIPLOTHIS@RPTTOMES WI  " + "\n");
                                strSqlString.Append("               , MWIPMATDEF MAT   " + "\n");
                                strSqlString.Append("               , MWIPLOTSTS STS   " + "\n");
                                strSqlString.Append("       WHERE 1 = 1 " + "\n");
                                strSqlString.Append("              AND PQ.FACTORY = WI.FACTORY " + "\n");
                                strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                                strSqlString.Append("              AND PQ.LOT_ID = WI.LOT_ID " + "\n");
                                strSqlString.Append("              AND PQ.LOT_ID = STS.LOT_ID " + "\n");
                                strSqlString.Append("              AND STS.LOT_CMF_7 " + cdvSite.SelectedValueToQueryString + "\n");
                                strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                                strSqlString.Append("              AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                                strSqlString.AppendFormat("        AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                strSqlString.Append("              AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                                strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                                //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                                if (cdvOper.Text != "")
                                {
                                    strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                                }

                                // 2011-03-03-임종우 : 외관검사만 가져옴.. CMF_10 = Y 이면 특성검사 이다.
                                strSqlString.Append("              AND PQ.CMF_10 = 'Y' " + "\n");

                                #region 상세 조회에 따른 SQL문 생성
                                if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                                if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                                if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                                if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                                if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                                if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                                if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                                if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                                    strSqlString.AppendFormat("              AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                                if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                                    strSqlString.AppendFormat("             AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                                if (cdvType.Text != "ALL" && cdvType.Text != "")
                                    strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                                #endregion

                                strSqlString.Append("       )  A, " + "\n");

                                strSqlString.Append("    (SELECT DA.LOT_ID, DA.HIST_SEQ, DA.FACTORY, DA.MAT_ID, DA.COL_SET_ID, DA.COL_SET_VERSION, DA.CHAR_ID, DA.TRAN_TIME     " + "\n");


                                strSqlString.Append("          , DA.VALUE_1 , DA.VALUE_2, DA.VALUE_3, DA.VALUE_4, DA.VALUE_5, DA.VALUE_6, DA.VALUE_7, DA.VALUE_8, DA.VALUE_9, DA.VALUE_10, DA.VALUE_11 ,DA.VALUE_12 " + "\n");
                                strSqlString.Append("          , DA.VALUE_13, DA.VALUE_14, DA.VALUE_15, DA.VALUE_16, DA.VALUE_17, DA.VALUE_18, DA.VALUE_19, DA.VALUE_20, DA.VALUE_21, DA.VALUE_22, DA.VALUE_23, DA.VALUE_24, DA.VALUE_25     " + "\n");


                                strSqlString.Append("          , DECODE(NVL(CH.UPPER_SPEC_LIMIT,0), ' ', 0, CH.UPPER_SPEC_LIMIT) UPPER_SPEC_LIMIT, DECODE(NVL(CH.LOWER_SPEC_LIMIT,0),' ',0,CH.LOWER_SPEC_LIMIT) LOWER_SPEC_LIMIT" + "\n");
                                strSqlString.Append("      FROM MEDCCOLCHR@RPTTOMES CH  " + "\n");
                                strSqlString.Append("          , (SELECT LOT_ID, HIST_SEQ,FACTORY, MAT_ID, COL_SET_ID, COL_SET_VERSION, CHAR_ID, TRAN_TIME  " + "\n");
                                strSqlString.Append("                  , VALUE_1 , VALUE_2, VALUE_3, VALUE_4, VALUE_5, VALUE_6, VALUE_7, VALUE_8, VALUE_9, VALUE_10, VALUE_11, VALUE_12, VALUE_13, VALUE_14, VALUE_15 " + "\n");
                                strSqlString.Append("                  , VALUE_16, VALUE_17, VALUE_18, VALUE_19, VALUE_20, VALUE_21, VALUE_22, VALUE_23, VALUE_24, VALUE_25 " + "\n");
                                strSqlString.Append("               FROM MEDCLOTDAT@RPTTOMES   " + "\n");
                                strSqlString.Append("             WHERE   1 = 1 " + "\n");
                                strSqlString.Append("                  AND COL_SET_ID IN (SELECT COL_SET_ID FROM MEDCCOLDEF@RPTTOMES WHERE   1=1  AND FACTORY = '" + GlobalVariable.gsAssyDefaultFactory + "' AND COL_GRP_10='Measure' AND DELETE_FLAG <> 'Y' )" + "\n");
                                strSqlString.AppendFormat("         AND FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                strSqlString.Append("              AND TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                                strSqlString.Append("                  AND VALUE_1 <> ' ' " + "\n");
                                strSqlString.Append("           ) DA  " + "\n");
                                strSqlString.Append("      WHERE   1 = 1 " + "\n");
                                strSqlString.Append("       AND DA.FACTORY = CH.FACTORY " + "\n");
                                strSqlString.AppendFormat("         AND DA.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                strSqlString.Append("       AND DA.COL_SET_ID = CH.COL_SET_ID  " + "\n");
                                strSqlString.Append("       AND DA.COL_SET_VERSION = CH.COL_SET_VERSION   ) B " + "\n");
                                strSqlString.Append("  WHERE   1 = 1 " + "\n");
                                strSqlString.Append("       AND A.FACTORY = B.FACTORY " + "\n");
                                strSqlString.Append("       AND A.LOT_ID   = B.LOT_ID  " + "\n");
                                strSqlString.Append("       AND A.MAT_ID = B.MAT_ID " + "\n");
                                strSqlString.Append("       AND A.HIST_SEQ = B.HIST_SEQ " + "\n");
                                strSqlString.AppendFormat("         AND A.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                                strSqlString.Append("              AND A.COL_SET_ID = B.COL_SET_ID " + "\n");
                                strSqlString.Append(" ORDER BY  " + QueryCond1 + " ,A.QC_OPER, A.TRAN_TIME,A.QC_TYPE, A.MAT_ID, A.LOT_ID, A.RES_ID, A.CUSTOMER, A.H_PACKAGE, A.DENSITY ,B.CHAR_ID  " + "\n");
                            }
                            else
                            {
                                #region  Bump Measure Raw Data
                                strSqlString.Append("SELECT " + QueryCond1 + " " + "\n");
                                strSqlString.Append("       , QC_OPER, TRAN_TIME, QC_TYPE, CUSTOMER " + "\n");
                                strSqlString.Append("       , LOT_ID, MAT_ID, MAT_GRP_2, MAT_GRP_7, CRR_ID, RES_ID, QTY_1 " + "\n");
                                strSqlString.Append("       , SAMPLE_QTY, TOTAL_DEFECT_QTY_1 AS TOTAL_QTY " + "\n");
//                                strSqlString.Append("       , DECODE(QC_FLAG, 'Y', 'PASS', 'FAIL') AS QC_FLAG  " + "\n"); // SPC DATA
                                strSqlString.Append(" FROM( " + "\n");
                                strSqlString.Append("       SELECT " + QueryCond1 + " " + "\n");
                                strSqlString.Append("              , PQ.QC_OPER " + "\n");
                                strSqlString.Append("              , PQ.TRAN_TIME " + "\n");
                                strSqlString.Append("              , PQ.QC_TYPE " + "\n");
                                strSqlString.Append("              , NVL((SELECT DATA_1 FROM MGCMTBLDAT WHERE FACTORY='" + cdvFactory.Text.Trim() + "' AND TABLE_NAME = 'H_CUSTOMER' AND KEY_1 = MAT.MAT_GRP_1 AND ROWNUM=1), '-')  AS CUSTOMER " + "\n");
                                strSqlString.Append("              , PQ.LOT_ID " + "\n");
                                strSqlString.Append("              , PQ.MAT_ID " + "\n");
                                strSqlString.Append("              , MAT.MAG_GRP_2 " + "\n");
                                strSqlString.Append("              , MAT.MAT_GRP_7 " + "\n");
                                strSqlString.Append("              , WI.CRR_ID " + "\n");
                                strSqlString.Append("              , PQ.RES_ID " + "\n");
                                strSqlString.Append("              , WI.QTY_1  " + "\n");
                                strSqlString.Append("              , PQ.SAMPLE_QTY " + "\n");
                                strSqlString.Append("              , PQ.TOTAL_DEFECT_QTY_1 " + "\n");
//                                strSqlString.Append("              , PQ.QC_FLAG " + "\n");  // SPC Data
                                strSqlString.Append("         FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                                strSqlString.Append("              , MWIPLOTHIS@RPTTOMES WI  " + "\n");
                                strSqlString.Append("              , MWIPMATDEF MAT   " + "\n");
                                strSqlString.Append("              , MWIPLOTSTS STS   " + "\n");
                                strSqlString.Append("        WHERE 1 = 1 " + "\n");
                                strSqlString.AppendFormat("              AND PQ.FACTORY = '{0}' \n", cdvFactory.Text.Trim());
                                strSqlString.Append("              AND PQ.FACTORY = WI.FACTORY " + "\n");
                                strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                                strSqlString.Append("              AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                                strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                                strSqlString.Append("              AND PQ.LOT_ID = WI.LOT_ID " + "\n");
                                strSqlString.Append("              AND PQ.LOT_ID = STS.LOT_ID " + "\n");
                                strSqlString.Append("              AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                                strSqlString.Append("              AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                                strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                                //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                                if (cdvOper.Text != "")
                                {
                                    strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                                }

                                // 2011-03-03-임종우 : 외관검사만 가져옴.. CMF_10 = Y 이면 특성검사 이다.
                                strSqlString.Append("              AND PQ.CMF_10 = ' ' " + "\n");

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

                                if (cdvType.Text != "ALL" && cdvType.Text != "")
                                    strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);

                                strSqlString.Append("     ) " + "\n");
                                strSqlString.Append("ORDER BY  " + QueryCond1 + " , QC_OPER, TRAN_TIME, QC_TYPE, MAT_ID, LOT_ID " + "\n");

                                #endregion
                            }
                        }
                        #endregion
                        #endregion
                    }
                    break;
                case 1:  // 불량 종류별 불량 발생 건
                    {
                        #region " 1번 그래프 "

                        strSqlString.Append("SELECT ' '  " + "\n");

                        if (dtChar != null)
                        {
                            for (int i = 0; i < dtChar.Rows.Count; i++)
                            {
                                strSqlString.AppendFormat("       , SUM(DECODE(CHAR_ID , '" + dtChar.Rows[i]["CHAR_ID"].ToString() + "' ,TOTAL_DEFECT_QTY_1,0))  AS \"" + dtChar.Rows[i]["CHAR_ID"].ToString() + "\"" + '\n');
                            }
                        }

                        strSqlString.Append(" FROM( " + "\n");
                        strSqlString.Append("      SELECT PQ.TOTAL_DEFECT_QTY_1 " + "\n");
                        strSqlString.Append("             , NVL((SELECT CHAR_ID " + "\n");
                        strSqlString.Append("                      FROM MEDCLOTDAT " + "\n");
                        strSqlString.Append("                     WHERE 1=1 " + "\n");
                        strSqlString.Append("                           AND COL_SET_ID IN " + "\n");
                        strSqlString.Append("                                           (SELECT COL_SET_ID " + "\n");
                        strSqlString.Append("                                              FROM MEDCCOLDEF " + "\n");
                        strSqlString.Append("                                             WHERE 1=1 " + "\n");
                        strSqlString.Append("                                                   AND COL_GRP_10='Visual' " + "\n");
                        strSqlString.Append("                                                   AND DELETE_FLAG <> 'Y' " + "\n");
                        strSqlString.Append("                                                   AND FACTORY=PQ.FACTORY " + "\n");
                        strSqlString.Append("                                           )  " + "\n");
                        strSqlString.Append("                           AND LOT_ID = PQ.LOT_ID " + "\n");
                        strSqlString.Append("                           AND VALUE_1 <> ' ' " + "\n");
                        strSqlString.Append("                           AND HIST_SEQ = PQ.HIST_SEQ " + "\n");
                        strSqlString.Append("                           AND ROWNUM = 1 ) " + "\n");
                        strSqlString.Append("                     ,' ') AS CHAR_ID " + "\n");
                        strSqlString.Append("        FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                        strSqlString.Append("             ,MWIPLOTHIS@RPTTOMES WI  " + "\n");
                        strSqlString.Append("             ,MWIPMATDEF MAT  " + "\n");
                        strSqlString.Append("       WHERE 1=1 " + "\n");
                        strSqlString.Append("             AND PQ.FACTORY = WI.FACTORY  " + "\n");
                        strSqlString.Append("             AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("             AND PQ.LOT_ID = WI.LOT_ID  " + "\n");
                        strSqlString.Append("             AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                        strSqlString.Append("             AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.Append("             AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                        strSqlString.AppendFormat("             AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("             AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.Append("             AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        //strSqlString.Append("             AND PQ.TOTAL_DEFECT_QTY_1 > 0  " + "\n");

                        //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                        if (cdvOper.Text != "")
                        {
                            strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        }

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("            AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                        if (cdvType.Text != "ALL" && cdvType.Text != "")
                            strSqlString.AppendFormat("             AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("     ) " + "\n");
                        strSqlString.Append("ORDER BY CHAR_ID DESC " + "\n");

                        #endregion
                    }
                    break;
                case 2: // 불량 종류 별 불량율(PPM)
                    {
                        #region " 2번 그래프 "
                        strSqlString.Append("SELECT CHAR_ID " + "\n");
                        strSqlString.Append("       , DECODE(SAMPLE_QTY,0,0, ROUND((TOTAL_DEFECT_QTY_1/SAMPLE_QTY)*1000000,0)) PPM  " + "\n");
                        strSqlString.Append(" FROM( " + "\n");
                        strSqlString.Append("       SELECT CHAR_ID " + "\n");
                        strSqlString.Append("       , SUM(TOTAL_DEFECT_QTY_1) AS TOTAL_DEFECT_QTY_1 " + "\n");
                        strSqlString.Append("       , (" + "\n");
                        strSqlString.Append("           SELECT SUM(PQ.SAMPLE_QTY)" + "\n");
                        strSqlString.Append("             FROM CPQCLOTHIS@RPTTOMES PQ" + "\n");
                        strSqlString.Append("                  ,MWIPLOTHIS@RPTTOMES WI" + "\n");
                        strSqlString.Append("                  ,MWIPMATDEF MAT" + "\n");
                        strSqlString.Append("            WHERE 1=1 " + "\n");
                        strSqlString.Append("                  AND PQ.FACTORY = WI.FACTORY  " + "\n");
                        strSqlString.Append("                  AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("                  AND PQ.LOT_ID = WI.LOT_ID  " + "\n");
                        strSqlString.Append("                  AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                        strSqlString.Append("                  AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.Append("                  AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                        strSqlString.AppendFormat("                  AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                  AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.Append("                  AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("          ) AS SAMPLE_QTY " + "\n");
                        strSqlString.Append("        FROM( " + "\n");
                        strSqlString.Append("              SELECT PQ.TOTAL_DEFECT_QTY_1 " + "\n");
                        strSqlString.Append("                     , PQ.SAMPLE_QTY " + "\n");
                        strSqlString.Append("                     , NVL((SELECT CHAR_ID FROM MEDCLOTDAT WHERE  1=1 AND COL_SET_ID IN   " + "\n");
                        strSqlString.Append("                                   (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND COL_GRP_10='Visual'  AND DELETE_FLAG <> 'Y' AND FACTORY=PQ.FACTORY) " + "\n");
                        strSqlString.Append("                             AND LOT_ID = PQ.LOT_ID  AND VALUE_1 <> ' '  AND HIST_SEQ= PQ.HIST_SEQ  AND ROWNUM = 1 ),' ') AS CHAR_ID " + "\n");
                        strSqlString.Append("                FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                        strSqlString.Append("                     ,MWIPLOTHIS@RPTTOMES WI" + "\n");
                        strSqlString.Append("                     ,MWIPMATDEF MAT  " + "\n");
                        strSqlString.Append("               WHERE 1=1 " + "\n");
                        strSqlString.Append("                     AND PQ.FACTORY = WI.FACTORY  " + "\n");
                        strSqlString.Append("                     AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("                     AND PQ.LOT_ID = WI.LOT_ID  " + "\n");
                        strSqlString.Append("                     AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                        strSqlString.Append("                     AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.Append("                     AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                        strSqlString.AppendFormat("                     AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                     AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.Append("                     AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                        //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                        if (cdvOper.Text != "")
                        {
                            strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        }

                        //strSqlString.Append("SELECT CHAR_ID " + "\n");                        
                        //strSqlString.Append("       DECODE(SUM(SAMPLE_QTY),0,0, ROUND(((SUM(TOTAL_DEFECT_QTY_1)/SUM(SAMPLE_QTY))*1000000),0)) PPM  " + "\n");
                        //strSqlString.Append(" FROM( " + "\n");
                        //strSqlString.Append("       SELECT PQ.TOTAL_DEFECT_QTY_1 " + "\n");
                        //strSqlString.Append("              , PQ.SAMPLE_QTY " + "\n");
                        //strSqlString.Append("              , NVL((SELECT CHAR_ID FROM MEDCLOTDAT WHERE  1=1 AND COL_SET_ID IN   " + "\n");
                        //strSqlString.Append("                          (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND COL_GRP_10='Visual'  AND DELETE_FLAG <> 'Y' AND FACTORY=PQ.FACTORY) " + "\n");
                        //strSqlString.Append("                  AND LOT_ID = PQ.LOT_ID  AND VALUE_1 <> ' '  AND HIST_SEQ= PQ.HIST_SEQ  AND ROWNUM = 1 ),' ') AS CHAR_ID " + "\n");
                        //strSqlString.Append("         FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                        //strSqlString.Append("              ,MWIPMATDEF MAT  " + "\n");
                        //strSqlString.Append("        WHERE 1=1 " + "\n");
                        //strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        //strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        //strSqlString.AppendFormat("              AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        //strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        //strSqlString.Append("              AND PQ.TOTAL_DEFECT_QTY_1 > 0  " + "\n");

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                     AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                        if (cdvType.Text != "ALL" && cdvType.Text != "")
                            strSqlString.AppendFormat("                     AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            ) " + "\n");
                        strSqlString.Append("       GROUP BY CHAR_ID " + "\n");
                        strSqlString.Append("     ) " + "\n");
                        strSqlString.Append(" ORDER BY CHAR_ID DESC " + "\n");

                        #endregion
                    }
                    break;
                case 3:  // 공정 별 불량 발생건
                    {
                        #region " 3번 그래프 "

                        strSqlString.Append(" SELECT QC_OPER  " + "\n");

                        if (dtChar != null)
                        {
                            for (int i = 0; i < dtChar.Rows.Count; i++)
                            {
                                strSqlString.AppendFormat("        , SUM(DECODE(CHAR_ID , '" + dtChar.Rows[i]["CHAR_ID"].ToString() + "' ,TOTAL_DEFECT_QTY_1,0))  AS \"" + dtChar.Rows[i]["CHAR_ID"].ToString() + "\"" + '\n');
                            }
                        }

                        strSqlString.Append("  FROM ( " + "\n");
                        strSqlString.Append("      SELECT   PQ.TOTAL_DEFECT_QTY_1,PQ.QC_OPER  " + "\n");
                        strSqlString.Append("           , NVL((SELECT CHAR_ID FROM MEDCLOTDAT WHERE  1=1 AND COL_SET_ID IN   " + "\n");
                        strSqlString.Append("                          (SELECT COL_SET_ID FROM MEDCCOLDEF WHERE 1=1 AND COL_GRP_10='Visual'  AND DELETE_FLAG <> 'Y' AND FACTORY=PQ.FACTORY) " + "\n");
                        strSqlString.Append("                  AND LOT_ID = PQ.LOT_ID  AND VALUE_1 <> ' '  AND HIST_SEQ= PQ.HIST_SEQ  AND ROWNUM = 1 ),' ') AS CHAR_ID " + "\n");
                        strSqlString.Append("        FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                        strSqlString.Append("               ,  MWIPMATDEF MAT  " + "\n");
                        strSqlString.Append(" WHERE 1=1 " + "\n");
                        strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.AppendFormat("         AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        strSqlString.Append("               AND PQ.TOTAL_DEFECT_QTY_1 > 0  " + "\n");

                        //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                        if (cdvOper.Text != "")
                        {
                            strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        }

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                        if (cdvType.Text != "ALL" && cdvType.Text != "")
                            strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append(" ) GROUP BY QC_OPER   ORDER BY QC_OPER " + "\n");

                        #endregion
                    }
                    break;
                case 4:  // 공정 별 검사종류 불량율(ppm)
                    {
                        #region " 4번 그래프 "

                        strSqlString.Append("SELECT QC_OPER  " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'초도양산(Volume)', PPM, 0)) AS \"초도양산(Volume)\"   " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'Underfill (SMT)', PPM, 0)) AS \"Underfill (SMT)\"   " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'기능검사', PPM, 0)) AS \"기능검사\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'GATE', PPM, 0)) AS \"GATE\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'설비인증', PPM, 0)) AS \"설비인증\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'CCS(변경점)', PPM, 0)) AS \"CCS(변경점)\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'순회검사', PPM, 0)) AS \"순회검사\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'X-ray', PPM, 0)) AS \"X-ray\"   " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'SAT', PPM, 0)) AS \"SAT\" " + "\n");
                        strSqlString.Append("     , SUM(DECODE(QC_TYPE,'PPQ', PPM, 0)) AS \"PPQ\" " + "\n");
                        strSqlString.Append(" FROM(    " + "\n");
                        strSqlString.Append("       SELECT QC_OPER, QC_TYPE    " + "\n");
                        strSqlString.Append("            , DECODE(SUM(SAMPLE_QTY), 0 ,0 , ROUND((SUM(TOTAL_DEFECT_QTY_1)/SUM(SAMPLE_QTY))*1000000,0)) PPM " + "\n");
                        strSqlString.Append("        FROM( " + "\n");
                        strSqlString.Append("              SELECT PQ.QC_OPER, PQ.QC_TYPE,  PQ.TOTAL_DEFECT_QTY_1, PQ.SAMPLE_QTY  " + "\n");
                        strSqlString.Append("                FROM CPQCLOTHIS@RPTTOMES PQ " + "\n");
                        strSqlString.Append("                   , MWIPLOTHIS@RPTTOMES WI " + "\n");
                        strSqlString.Append("                   , MWIPMATDEF MAT  " + "\n");
                        strSqlString.Append("        WHERE 1=1 " + "\n");
                        strSqlString.Append("              AND PQ.FACTORY = WI.FACTORY  " + "\n");
                        strSqlString.Append("              AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("              AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                        strSqlString.Append("              AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.Append("              AND PQ.LOT_ID = WI.LOT_ID  " + "\n");
                        strSqlString.Append("              AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                        strSqlString.AppendFormat("              AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("              AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.Append("              AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");
                        //strSqlString.Append("              AND PQ.TOTAL_DEFECT_QTY_1 > 0  " + "\n");

                        //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                        if (cdvOper.Text != "")
                        {
                            strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        }

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("              AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("             AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                        if (cdvType.Text != "ALL" && cdvType.Text != "")
                            strSqlString.AppendFormat("              AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("            ) GROUP BY QC_OPER, QC_TYPE " + "\n");
                        strSqlString.Append("              HAVING DECODE(SUM(SAMPLE_QTY), 0 ,0 , ROUND((SUM(TOTAL_DEFECT_QTY_1)/SUM(SAMPLE_QTY))*1000000,0)) > 0 " + "\n");
                        strSqlString.Append("     ) " + "\n");
                        strSqlString.Append("GROUP BY QC_OPER " + "\n");
                        strSqlString.Append("ORDER BY QC_OPER " + "\n");
                        #endregion
                    }
                    break;
                case 5:  // 주기 별 공정 불량율(ppm) 
                    {
                        #region " 5번 그래프 "

                        // dtOper = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlOper ());

                        strSqlString.Append("SELECT QC_OPER  " + "\n");

                        for (int i = 0; i < Between; i++)
                        {
                            strSqlString.AppendFormat("       , SUM(DECODE({0}, '{1}', PPM, 0)) as \"{1}\"" + "\n", strDate, i.ToString());
                        }

                        strSqlString.Append(" FROM( " + "\n");
                        strSqlString.Append("      SELECT WORK_DATE,WORK_WEEK,WORK_MONTH, QC_OPER, DECODE(SQ, 0 ,0 , ROUND((SUM(TDQ)/SQ)*1000000,0)) PPM  " + "\n");
                        //strSqlString.Append("      SELECT  WORK_DATE,WORK_WEEK,WORK_MONTH, QC_OPER, DECODE(SUM(SQ), 0 ,0 , ROUND((SUM(TDQ)/SUM(SQ))*1000000,0)) PPM  " + "\n");
                        strSqlString.Append("       FROM( " + "\n");
                        strSqlString.Append("             SELECT QC_OPER, TOTAL_DEFECT_QTY_1 TDQ " + "\n");
                        strSqlString.Append("                  , GET_WORK_DATE(TRAN_TIME, 'D')  AS WORK_DATE " + "\n");
                        strSqlString.Append("                  , GET_WORK_DATE(TRAN_TIME, 'W')  AS WORK_WEEK " + "\n");
                        strSqlString.Append("                  , GET_WORK_DATE(TRAN_TIME, 'M')  AS WORK_MONTH " + "\n");
                        strSqlString.Append("                  , ( " + "\n");
                        strSqlString.Append("                      SELECT SUM(PQ.SAMPLE_QTY) " + "\n");
                        strSqlString.Append("                        FROM CPQCLOTHIS@RPTTOMES PQ " + "\n");
                        strSqlString.Append("                           , MWIPLOTHIS@RPTTOMES WI " + "\n");
                        strSqlString.Append("                           , MWIPMATDEF MAT " + "\n");
                        strSqlString.Append("                       WHERE 1=1 " + "\n");
                        strSqlString.Append("                             AND PQ.FACTORY = WI.FACTORY  " + "\n");
                        strSqlString.Append("                             AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("                             AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                        strSqlString.Append("                             AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.Append("                             AND PQ.LOT_ID = WI.LOT_ID  " + "\n");
                        strSqlString.Append("                             AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                        strSqlString.AppendFormat("                             AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                             AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.Append("                             AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                        //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                        if (cdvOper.Text != "")
                        {
                            strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        }

                        strSqlString.Append("                    ) AS SQ " + "\n");
                        strSqlString.Append("              FROM( " + "\n");
                        strSqlString.Append("                    SELECT PQ.TRAN_TIME, PQ.QC_OPER,  PQ.TOTAL_DEFECT_QTY_1, PQ.SAMPLE_QTY " + "\n");
                        strSqlString.Append("                      FROM CPQCLOTHIS@RPTTOMES PQ  " + "\n");
                        strSqlString.Append("                         , MWIPLOTHIS@RPTTOMES WI " + "\n");
                        strSqlString.Append("                         , MWIPMATDEF MAT  " + "\n");
                        strSqlString.Append("              WHERE 1=1 " + "\n");
                        strSqlString.Append("                    AND PQ.FACTORY = WI.FACTORY  " + "\n");
                        strSqlString.Append("                    AND PQ.FACTORY = MAT.FACTORY  " + "\n");
                        strSqlString.Append("                    AND PQ.MAT_ID = WI.MAT_ID  " + "\n");
                        strSqlString.Append("                    AND PQ.MAT_ID = MAT.MAT_ID  " + "\n");
                        strSqlString.Append("                    AND PQ.LOT_ID = WI.LOT_ID  " + "\n");
                        strSqlString.Append("                    AND PQ.HIST_SEQ = WI.HIST_SEQ  " + "\n");
                        strSqlString.AppendFormat("                    AND MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n");
                        strSqlString.Append("                    AND WI.HIST_DEL_FLAG = ' ' " + "\n");
                        strSqlString.Append("                    AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n");

                        //2011-08-31-배수민 : 각 공정별로 조회할 수 있게 (김용복k 요청)
                        if (cdvOper.Text != "")
                        {
                            strSqlString.Append("              AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n");
                        }

                        #region 상세 조회에 따른 SQL문 생성
                        if (udcWIPCondition1.Text != "ALL" && udcWIPCondition1.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_1 {0} " + "\n", udcWIPCondition1.SelectedValueToQueryString);

                        if (udcWIPCondition2.Text != "ALL" && udcWIPCondition2.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_2 {0} " + "\n", udcWIPCondition2.SelectedValueToQueryString);

                        if (udcWIPCondition3.Text != "ALL" && udcWIPCondition3.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_3 {0} " + "\n", udcWIPCondition3.SelectedValueToQueryString);

                        if (udcWIPCondition4.Text != "ALL" && udcWIPCondition4.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_4 {0} " + "\n", udcWIPCondition4.SelectedValueToQueryString);

                        if (udcWIPCondition5.Text != "ALL" && udcWIPCondition5.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_5 {0} " + "\n", udcWIPCondition5.SelectedValueToQueryString);

                        if (udcWIPCondition6.Text != "ALL" && udcWIPCondition6.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_6 {0} " + "\n", udcWIPCondition6.SelectedValueToQueryString);

                        if (udcWIPCondition7.Text != "ALL" && udcWIPCondition7.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_7 {0} " + "\n", udcWIPCondition7.SelectedValueToQueryString);

                        if (udcWIPCondition8.Text != "ALL" && udcWIPCondition8.Text != "")
                            strSqlString.AppendFormat("                    AND MAT.MAT_GRP_8 {0} " + "\n", udcWIPCondition8.SelectedValueToQueryString);

                        if (udcWIPCondition9.Text != "ALL" && udcWIPCondition9.Text != "")
                            strSqlString.AppendFormat("                   AND MAT.MAT_GRP_9 {0} " + "\n", udcWIPCondition9.SelectedValueToQueryString);

                        if (cdvType.Text != "ALL" && cdvType.Text != "")
                            strSqlString.AppendFormat("                    AND PQ.QC_TYPE {0} " + "\n", cdvType.SelectedValueToQueryString);
                        #endregion

                        strSqlString.Append("                  ) " + "\n");
                        strSqlString.Append("           ) GROUP BY WORK_DATE,WORK_WEEK,WORK_MONTH, QC_OPER,SQ" + "\n");
                        strSqlString.Append("             HAVING DECODE(SQ, 0 ,0 , ROUND((SUM(TDQ)/SQ)*1000000,0)) > 0 " + "\n");
                        strSqlString.Append("     )   " + "\n");
                        strSqlString.Append("GROUP BY QC_OPER   " + "\n");
                        strSqlString.Append("ORDER BY QC_OPER" + "\n");


                        #endregion
                    }
                    break;
                case 6:  // 검사종류 별 공정 불량율(ppm) : Case4와 Query문이 동일하여 비어있음
                    {
                        #region " 6번 그래프 "
                        #endregion
                    }
                    break;
                case 7:
                    {
                        #region " 7번 그래프 "
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
            // 차트 설정

            #region " Chart 기본설정"
            DataTable dt_graph = null;

            //// 그래프 설정 //                      

            ////3D 
            //udcChartFX1.Chart3D = true;

            //// contion attribute 를 이용한 0 point label hidden            
            //SoftwareFX.ChartFX.ConditionalAttributes contion = udcChartFX1.ConditionalAttributes[0];
            //contion.Condition.From = 0;
            //contion.Condition.To = 0;
            //contion.PointLabels = false;

            //// 색깔  multi로 보여주기: 기본으로 false해준다.
            //udcChartFX1.MultipleColors = false;

            udcMSChart1.RPT_1_ChartInit();
            udcMSChart1.RPT_2_ClearData();

            udcMSChart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

            #endregion

            switch (cboGraph.SelectedIndex)
            {

                case 0:   // MAIN : 전체보기
                    {
                        #region -

                        #endregion
                    }
                    break;

                case 1:  // 불량 종류별 불량 발생 건
                    {
                        #region Chart No.1
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(1));

                        if (dt_graph.Columns.Count == 1)
                            return;

                        // 기존 CHART
                        //dt_graph = GetRotatedDataTable(ref dt_graph);
                        //udcChartFX1.DataSource = dt_graph;
                        //udcChartFX1.MultipleColors = true;
                        //udcChartFX1.SerLegBoxObj.Docked = SoftwareFX.ChartFX.Docked.Top;
                        //udcChartFX1.AxisY.Max = udcChartFX1.AxisY.Max * 1.1;
                        //udcChartFX1.Gallery = SoftwareFX.ChartFX.Gallery.Bar;
                        ////udcChartFX1.Series[0].Color = System.Drawing.Color.YellowGreen ;
                        //udcChartFX1.AxisX.Title.Text = "Type of defect";
                        //udcChartFX1.AxisY.Title.Text = "number of occurrences";
                        ////udcChartFX1.LegendBox = true;

                        dt_graph = GetRotatedDataTable(ref dt_graph);

                        udcMSChart1.RPT_3_OpenData(1, dt_graph.Rows.Count);
                        int[] cnt_rows = new Int32[dt_graph.Rows.Count];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        double cnt = udcMSChart1.RPT_4_AddData(dt_graph, cnt_rows, new int[] { 1 }, SeriseType.Rows);

                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "number of occurrences", AsixType.Y, DataTypes.Initeger, cnt * 1.2);

                        udcMSChart1.Series[0].IsValueShownAsLabel = true;
                        udcMSChart1.Series[0].IsVisibleInLegend = false;
                        udcMSChart1.Series[0].Palette = ChartColorPalette.BrightPastel;
                        udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
                        ////udcMSChart1.Series[0].LegendText = dt_graph.Columns[i + 1].ToString();

                        udcMSChart1.ChartAreas[0].AxisX.Title = "Type of defect";
                        udcMSChart1.ChartAreas[0].AxisX.LabelStyle.Interval = 2;
                        udcMSChart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;

                        #endregion
                    }
                    break;
                case 2:   // 불량 종류 별 불량율(PPM)
                    {
                        #region Chart No.2

                        double max = 0;
                        double max_temp = 0;

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(2));

                        if (dt_graph == null || dt_graph.Rows.Count < 1)
                            return;

                        dt_graph = GetRotatedDataTable(ref dt_graph);

                        int rowCount = dt_graph.Rows.Count;
                        udcMSChart1.RPT_3_OpenData(1, dt_graph.Columns.Count - 1);
                        //udcMSChart1.RPT_3_OpenData(1, rowCount);

                        int[] wip_columns = new Int32[rowCount];
                        int arrCnt = 0;
                        string[] columnsHeader = new string[dt_graph.Columns.Count - 1];

                        for (int x = 1; x < dt_graph.Columns.Count; x++)
                        {
                            columnsHeader[arrCnt] = dt_graph.Columns[x].ToString();

                            arrCnt++;
                        }

                        int[] cnt_rows = new Int32[rowCount];

                        for (int i = 0; i < cnt_rows.Length; i++)
                        {
                            cnt_rows[i] = i;
                        }

                        for (int k = 1; k < dt_graph.Columns.Count; k++)
                        {
                            max = udcMSChart1.RPT_4_AddData(dt_graph, cnt_rows, new int[] { k }, SeriseType.Rows);
                            //max = udcMSChart1.RPT_4_AddData_Original(dt_graph, new int[] { cnt }, wip_columns, SeriseType.Rows, DataTypes.Initeger);
                            if (max > max_temp)
                            {
                                max_temp = max;
                            }
                        }
                        max = max_temp;


                        udcMSChart1.RPT_6_SetGallery(SeriesChartType.Column, 0, 1, "Defective Rate (ppm)", AsixType.Y, DataTypes.Initeger, max * 1.1);
                        udcMSChart1.RPT_7_SetXAsixTitle(columnsHeader);

                        udcMSChart1.Series[0].IsValueShownAsLabel = true;
                        udcMSChart1.Series[0].IsVisibleInLegend = false;
                        udcMSChart1.Series[0].Palette = ChartColorPalette.BrightPastel;
                        udcMSChart1.Series[0]["DrawingStyle"] = "Cylinder";
                        ////udcMSChart1.Series[0].LegendText = dt_graph.Columns[i + 1].ToString();
                        udcMSChart1.ChartAreas[0].AxisX.Title = "Type of defect";

                        #endregion
                    }
                    break;
                case 3: // 공정 별 불량 발생건(누적막대그래프)
                    {
                        #region Chart No.3

                        //// 누적막대그래프 
                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));

                        if (dt_graph == null || dt_graph.Rows.Count < 1)
                            return;


                        udcMSChart1.DataSource = dt_graph;
                        udcMSChart1.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);
                        for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
                        {
                            udcMSChart1.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                            udcMSChart1.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);

                            udcMSChart1.Series[i].ChartType = SeriesChartType.StackedColumn;
                            udcMSChart1.Series[i].IsValueShownAsLabel = true;
                            udcMSChart1.Series[i].IsVisibleInLegend = true;
                            udcMSChart1.Series[i]["DrawingStyle"] = "Cylinder";
                            udcMSChart1.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
                        }
                        udcMSChart1.DataBind();

                        udcMSChart1.Legends[0].Docking = Docking.Top;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
                        udcMSChart1.ChartAreas[0].AxisX.Title = "Operation";
                        udcMSChart1.ChartAreas[0].AxisY.Title = "number of occurrences";
                        udcMSChart1.ChartAreas[0].Area3DStyle.Enable3D = true;

                        #endregion
                    }
                    break;
                case 4:  // 공정 별 검사종류 불량율(ppm)
                    {
                        #region Chart No.4

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));

                        if (dt_graph == null || dt_graph.Rows.Count < 1)
                            return;

                        udcMSChart1.DataSource = dt_graph;
                        udcMSChart1.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);
                        for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
                        {
                            udcMSChart1.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                            udcMSChart1.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);

                            udcMSChart1.Series[i].ChartType = SeriesChartType.Column;
                            udcMSChart1.Series[i].IsValueShownAsLabel = true;
                            udcMSChart1.Series[i].IsVisibleInLegend = true;
                            udcMSChart1.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
                        }
                        udcMSChart1.DataBind();

                        udcMSChart1.Legends[0].Docking = Docking.Bottom;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
                        udcMSChart1.ChartAreas[0].AxisX.Title = "Operation";
                        udcMSChart1.ChartAreas[0].AxisY.Title = "Defective Rate (ppm)";

                        #endregion
                    }
                    break;
                case 5:  // 주기 별 공정 불량율(ppm) : 꺾은선 그래프
                    {
                        #region Chart No.5

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(5));

                        if (dt_graph == null || dt_graph.Rows.Count < 1)
                            return;

                        dt_graph = GetRotatedDataTable(ref dt_graph);

                        udcMSChart1.DataSource = dt_graph;
                        udcMSChart1.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);
                        for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
                        {
                            udcMSChart1.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                            udcMSChart1.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);

                            udcMSChart1.Series[i].ChartType = SeriesChartType.Line;
                            udcMSChart1.Series[i].IsValueShownAsLabel = false;
                            udcMSChart1.Series[i].IsVisibleInLegend = true;
                            udcMSChart1.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
                        }
                        udcMSChart1.DataBind();

                        udcMSChart1.Legends[0].Docking = Docking.Top;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
                        udcMSChart1.ChartAreas[0].AxisX.Title = "주기";
                        udcMSChart1.ChartAreas[0].AxisY.Title = "Defective Rate (ppm)";

                        #endregion
                    }
                    break;
                case 6:  // 검사종류 별 공정 불량율(ppm) 
                    {
                        #region Chart No.6

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(4));

                        if (dt_graph == null || dt.Rows.Count < 1)
                            return;


                        dt_graph = GetRotatedDataTable(ref dt_graph);

                        udcMSChart1.DataSource = dt_graph;
                        udcMSChart1.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);
                        for (int i = 0; i < dt_graph.Columns.Count - 1; i++)
                        {
                            udcMSChart1.Series[i].XValueMember = Convert.ToString(dt_graph.Columns[0]);
                            udcMSChart1.Series[i].YValueMembers = Convert.ToString(dt_graph.Columns[i + 1]);

                            udcMSChart1.Series[i].ChartType = SeriesChartType.Column;
                            udcMSChart1.Series[i].IsValueShownAsLabel = true;
                            udcMSChart1.Series[i].IsVisibleInLegend = true;
                            udcMSChart1.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
                        }
                        udcMSChart1.DataBind();

                        udcMSChart1.Legends[0].Docking = Docking.Top;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;
                        udcMSChart1.ChartAreas[0].AxisX.Title = "Type of inspection";
                        udcMSChart1.ChartAreas[0].AxisY.Title = "Defective Rate (ppm)";

                        #endregion
                    }
                    break;
                case 7:  // 공정 별 불량 종류 비율
                    {
                        #region Chart No.7

                        DataTable dt_graphTmp = null;

                        dt_graph = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(3));
                        if (dt_graph == null || dt_graph.Columns.Count == 1)
                            return;

                        dt_graphTmp = GetRotatedDataTable(ref dt_graph);

                        udcMSChart1.ChartAreas.Clear();
                        udcMSChart1.Series.Clear();
                        //udcMSChart1.Legends.Clear();
                        udcMSChart1.Titles.Clear();

                        udcMSChart1.DataSource = dt_graphTmp;
                        //udcMSChart1.RPT_3_OpenData(dt_graph.Columns.Count - 1, dt_graph.Rows.Count);
                        udcMSChart1.RPT_3_OpenData(dt_graphTmp.Columns.Count - 1, dt_graphTmp.Rows.Count, dt_graphTmp);

                        for (int i = 0; i < dt_graphTmp.Columns.Count - 1; i++)
                        {
                            udcMSChart1.Series[i].XValueMember = Convert.ToString(dt_graphTmp.Columns[0]);
                            udcMSChart1.Series[i].YValueMembers = Convert.ToString(dt_graphTmp.Columns[i + 1]);

                            udcMSChart1.Series[i].ChartType = SeriesChartType.Pie;
                            udcMSChart1.Series[i].ChartArea = "pieChartArea" + (i + 1);
                            udcMSChart1.Series[i]["PieLabelStyle"] = "Disabled";
                            //chart1.Series[i].LegendText = Convert.ToString(dt_graphTmp.Columns[i + 1]);                            

                            if (i == 0)
                            {
                                udcMSChart1.Series[i].IsVisibleInLegend = true;
                            }
                            else
                            {
                                udcMSChart1.Series[i].IsVisibleInLegend = false;
                            }

                            //udcMSChart1.Series[i].LegendText = dt_graph.Columns[i + 1].ToString();
                            udcMSChart1.Series[i].LegendText = "#AXISLABEL";

                            udcMSChart1.Titles[i].Docking = Docking.Top;
                            //chart1.ChartAreas[i].Area3DStyle.Enable3D = true;
                        }
                        udcMSChart1.DataBind();

                        udcMSChart1.Legends[0].Docking = Docking.Right;
                        udcMSChart1.Legends[0].Alignment = StringAlignment.Center;


                        ChartArea tmpChartArea = null;
                        Series tmpSeries = null;
                        Legend tmpLegend = null;

                        tmpChartArea = new ChartArea("tmpChartArea");
                        udcMSChart1.ChartAreas.Add(tmpChartArea);

                        tmpLegend = new Legend("tmpLegend");
                        tmpLegend.Docking = Docking.Bottom;
                        udcMSChart1.Legends.Add(tmpLegend);

                        int tmpCnt = dt_graphTmp.Columns.Count - 1;

                        for (int i = 0; i < tmpCnt; i++)
                        {
                            tmpSeries = new Series();
                            tmpSeries.Name = Convert.ToString(dt_graphTmp.Columns[i + 1]);
                            tmpSeries.IsVisibleInLegend = true;
                            tmpSeries.ChartType = SeriesChartType.Line;

                            udcMSChart1.Series.Add(tmpSeries);
                            udcMSChart1.Series[i + tmpCnt].ChartArea = "tmpChartArea";
                            udcMSChart1.Series[i + tmpCnt].Legend = "tmpLegend";
                            udcMSChart1.Series[i + tmpCnt].Points.AddXY(1, 1);
                        }

                        udcMSChart1.ChartAreas["tmpChartArea"].Visible = false;

                        #endregion
                    }
                    break;
            }
        }

        #endregion

        #region " EVENT HANDLER "

        private void btnView_Click(object sender, EventArgs e)
        {
            // DataTable dt = null;
            LoadingPopUp.LoadIngPopUpShow(this);

            if (cboList.SelectedIndex == 0 && cdvFactory.Text.Trim() != "HMKB1")
            {
                dtChar = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlChar());
            }

            if (CheckField() == false) return;

            try
            {
                //LoadingPopUp.LoadIngPopUpShow(this);
                this.Refresh();

                dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString(0));

                GridColumnInit();

                if (dt.Rows.Count == 0)
                {
                    dt.Dispose();
                    LoadingPopUp.LoadingPopUpHidden();
                    CmnFunction.ShowMsgBox(RptMessages.GetMessage("STD010", GlobalVariable.gcLanguage));
                    return;
                }

                //by John
                //1.Griid 합계 표시.
                spdData.DataSource = dt;
                spdData.RPT_ColumnConfigFromTable(btnSort);

                //     int sub = ((udcTableForm)(this.btnSort.BindingForm)).GetCheckedValue(2);
                //     int[] rowType = spdData.RPT_DataBindingWithSubTotal(dt, 0, sub + 1, 25, null, null, btnSort);

                //2. 칼럼 고정(필요하다면..)
                //spdData.Sheets[0].FrozenColumnCount = 10;

                //////3. Total부분 셀머지
                //    spdData.RPT_FillDataSelectiveCells("Total", 0, 25, 0, 1, true, Align.Center, VerticalAlign.Center);

                //   4. Column Auto Fit4
                spdData.RPT_AutoFit(false);

                if (cdvFactory.Text.Trim() == "HMKB1")
                    return;

                if (cboList.SelectedIndex == 1) // LIST : 특성검사인 경우에만 해당
                {
                    for (int i = 0; i < spdData_Sheet1.RowCount; i++)
                    {
                        int a = 0;
                        int b = 0;

                        for (int j = 27; j < spdData_Sheet1.ColumnCount; j++)
                        {
                            //  측정(value)횟수를 count해서 a에 넣어준다.
                            if (spdData_Sheet1.Cells[i, j].Text != "")
                            {
                                a = a + 1;

                                // Spec(Min) < 측정값(value 각각의 컬럼값) < Spec(Max) 을 확인해서 불량 수를 count해 b에 넣어준다.
                                if (spdData_Sheet1.Cells[i, 26].Text != "0")
                                {
                                    if (Convert.ToDouble(spdData_Sheet1.Cells[i, 25].Value) >= Convert.ToDouble(spdData_Sheet1.Cells[i, j].Value) && Convert.ToDouble(spdData_Sheet1.Cells[i, 26].Value) <= Convert.ToDouble(spdData_Sheet1.Cells[i, j].Value))
                                        b = b + 1;
                                }
                                else
                                {
                                    // Spec(Max)값이 0이 있는 경우 'Spec(Min) < 측정값 ' 만 확인하면 된다
                                    if (Convert.ToDouble(spdData_Sheet1.Cells[i, 25].Value) >= Convert.ToDouble(spdData_Sheet1.Cells[i, j].Value))
                                        b = b + 1;
                                }
                            }
                        }
                        spdData_Sheet1.Cells[i, 22].Text = a.ToString();  // 검사수 컬럼에 넣어준다.
                        spdData_Sheet1.Cells[i, 23].Text = b.ToString();  // 불량수 컬럼에 넣어준다.

                    }
                }

                // 컬럼값이 모두 공백이거나 0인 경우 해당 컬럼을 없애버린다.
                // value_01~25까지 일단 다 뿌려준 상태이니 값이 없는 경우 보여줄 필요가 없으니 적용,

                spdData.RPT_RemoveZeroColumn(26, 51, spdData_Sheet1.RowCount);

                // 1번 그래프 그리도록 이벤트 발생
                // cboGraph.SelectedIndex = 0;
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
            if (cdvFactory.Text.Trim() == "HMKB1")
                ExcelHelper.Instance.subMakeExcel(spdData, this.lblTitle.Text, null, null);
                //ExcelHelper.Instance.subMakeMsChartExcel(spdData, null, this.lblTitle.Text, null, null);
            else
                ExcelHelper.Instance.subMakeMsChartExcel(spdData, udcMSChart1, this.lblTitle.Text, null, null);
            
            //ExcelHelper.Instance.subMakeExcel(spdData, udcChartFX1, this.lblTitle.Text, " ^ ", " ^ ");
        }

        #region DataTable Pivot Function
        public DataTable GetRotatedDataTable(ref DataTable dt)
        {
            int nColToRow = 0;  // Column Position of dt => Legend Column of dtNew

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

        private void cdvFactory_ValueSelectedItemChanged(object sender, Miracom.UI.MCCodeViewSelChanged_EventArgs e)
        {
            this.SetFactory(cdvFactory.txtValue);

            if (cdvFactory.Text.Trim() == "HMKB1")
            {
                BaseFormType = eBaseFormType.BUMP_BASE;
                pnlBUMPDetail.Visible = false;

                cdvSite.Visible = false;

                cboList.Items.Clear();
                cboList.Items.Add("Measure");
                cboList.SelectedIndex = 0;

                label1.Visible = false;
                label3.Visible = false;
                cboGraph.Visible = false;

                spdData.ActiveSheet.ColumnHeader.Rows.Count = 0;

                splitContainer1.Panel1MinSize = 0;
                splitContainer1.SplitterDistance = 0;
            }
            else
            {
                BaseFormType = eBaseFormType.WIP_BASE;
                pnlWIPDetail.Visible = false;

                cdvSite.Visible = true;

                cboList.Items.Clear();
                cboList.Items.Add(LanguageFunction.FindLanguage("Visual inspection result", 0));
                cboList.Items.Add(LanguageFunction.FindLanguage("Characteristic test result", 0));

                spdData.ActiveSheet.ColumnHeader.Rows.Count = 0;

                label1.Visible = true;
                label3.Visible = true;
                cboGraph.Visible = true;

                splitContainer1.Panel1MinSize = 25;
                splitContainer1.SplitterDistance = 220;
            }

            cdvOper.Text = "";

            SortInit();     //add 150529
        }

        private void spdData_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column > 10)
            {
                //mnuPopup.Show();
            }

        }

        private void cboList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (i_index != cboList.SelectedIndex)
            {
                i_index = cboList.SelectedIndex;

                if (cdvFactory.Text.Trim() == "HMKB1")
                    return;

                udcMSChart1.RPT_1_ChartInit();  //차트 초기화. 

                cboGraph.SelectedIndex = 0;

                if (cboList.SelectedIndex == 0) //  List가 외관검사 이면 chart 활성화 되고 
                {
                    cboGraph.Enabled = true;
                }
                else   // 특성검사, 통계량 이면 Chart가 비활성화
                {
                    cboGraph.Enabled = false;

                }
                spdData.ActiveSheet.ColumnHeader.Rows.Count = 1;
                spdData.RPT_ColumnInit();
                GridColumnInit();

                SortInit();
            }
        }

        private void cdvOper_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            string strFromDate = udcDate.Start_Tran_Time;
            string strToDate = udcDate.End_Tran_Time;

            cdvOper.Init(); //검색일마다 가져오는 공정 다르므로 초기화

            strQuery += "SELECT DISTINCT OPER AS Code, '' AS Data " + "\n";
            strQuery += "  FROM CPQCLOTHIS@RPTTOMES " + "\n";
            strQuery += " WHERE FACTORY = '" + cdvFactory.Text + "' " + "\n";
            strQuery += "   AND TRAN_TIME BETWEEN '" + strFromDate + "' AND '" + strToDate + "'" + "\n";
            strQuery += " ORDER BY OPER " + "\n";

            cdvOper.sDynamicQuery = strQuery;
        }

        // 2012-04-09-배수민 : 조립SITE 추가 (품질팀 한혜정S 요청)
        private void cdvSite_ValueButtonPress(object sender, EventArgs e)
        {
            string strQuery = string.Empty;

            string strFromDate = string.Empty;
            string strToDate = string.Empty;

            #region " udcDuration에서 정확한 조회시간을 받아오기 : strFromDate, strToDate "
            strFromDate = udcDate.Start_Tran_Time;
            strToDate = udcDate.End_Tran_Time;
            #endregion

            strQuery += "SELECT DISTINCT TRIM(STS.LOT_CMF_7) AS CODE, ' ' AS NAME" + "\n";
            strQuery += "  FROM CPQCLOTHIS@RPTTOMES PQ" + "\n";
            strQuery += "     , MWIPLOTHIS@RPTTOMES WI" + "\n";
            strQuery += "     , MWIPMATDEF MAT" + "\n";
            strQuery += "     , MWIPLOTSTS STS" + "\n";
            strQuery += " WHERE MAT.FACTORY (+) = '" + cdvFactory.Text.ToString() + "' " + "\n";
            strQuery += "   AND PQ.FACTORY = WI.FACTORY " + "\n";
            strQuery += "   AND PQ.FACTORY = MAT.FACTORY " + "\n";
            strQuery += "   AND PQ.MAT_ID = WI.MAT_ID " + "\n";
            strQuery += "   AND PQ.MAT_ID = MAT.MAT_ID " + "\n";
            strQuery += "   AND PQ.LOT_ID = WI.LOT_ID " + "\n";
            strQuery += "   AND PQ.LOT_ID = STS.LOT_ID " + "\n";
            strQuery += "   AND PQ.HIST_SEQ = WI.HIST_SEQ " + "\n";
            strQuery += "   AND WI.HIST_DEL_FLAG = ' ' " + "\n";
            strQuery += "   AND PQ.TRAN_TIME  BETWEEN '" + strFromDate + "' AND '" + strToDate + "'  \n";

            if (cdvOper.Text != "")
            {
                strQuery += "   AND PQ.OPER " + cdvOper.SelectedValueToQueryString + "\n";
            }
            strQuery += "   AND PQ.CMF_10 = ' ' " + "\n";
            strQuery += " ORDER BY CODE " + "\n";

            cdvSite.sDynamicQuery = strQuery;

        }

        private void cdvOper_ValueSelectedItemChanged(object sender, MCCodeViewSelChanged_EventArgs e)
        {
            cdvSite.Text = "";
        }

        #region POPUP MENU

        #endregion

        #endregion

    }
}