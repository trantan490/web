using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Miracom.UI;
using Miracom.SmartWeb;
using Miracom.SmartWeb.UI;
using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.UI.Controls;


namespace Hana.PQC
{

    /// <summary>
    /// 클  래  스: PQC030102_P2<br/>
    /// 클래스요약: 고객불만, 부적합 현황 자세히 보기<br/>
    /// 작  성  자: 장은희 <br/>
    /// 최초작성일: 2009-09-02<br/>
    /// 상세  설명:  고객불만(PQC030102), 부적합현황(PQC030103) 화면에서 상세히 보고자 하는 해당 건 클릭 POPUP창 (STEP1~7까지 표현) <br/>
    /// 변경  내용: <br/>
    /// </summary>
    public partial class PQC030102_P2 : Form
    {
        static string m_file = null ;

        public PQC030102_P2()
        {
            InitializeComponent();
           
        }

        /// <summary>
        /// 팝업에서 보여줄 Title과 DataTable을 입력받는 생성자
        /// </summary>
        /// <param name="title">Title</param>
        /// <param name="dt">해당 공정그룹의 정체 Lot 현황</param>
        public PQC030102_P2(string sAbr, string sLot)
        {
            InitializeComponent();
            DataTable dt = null;
            DataTable dtFile = null;
            string sStep;

            txtAbrNo.Text = sAbr;
            txtLotId.Text = sLot;

            //MakeSqlString();
            dt = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());

            if (dt != null)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sStep = dt.Rows[0][i].ToString();
                    spdList_Sheet1.Cells[i, 2].Text = sStep.ToString();

                }

                //MakeSqlFile();
                dtFile = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlFile());

                if (dtFile != null)
                {
                    //    lisFile.Clear();
                    ListViewItem item;


                    if (dtFile.Rows[0]["FILE_NAME1"].ToString() != "")
                    {
                        item = new ListViewItem(dtFile.Rows[0]["FILE_NAME1"].ToString());
                        lisFile.Items.Add(item);
                    }
                    if (dtFile.Rows[0]["FILE_NAME2"].ToString() != " ")
                    {
                        item = new ListViewItem(dtFile.Rows[0]["FILE_NAME2"].ToString());
                        lisFile.Items.Add(item);
                    }
                    if (dtFile.Rows[0]["FILE_NAME3"].ToString() != " ")
                    {
                        item = new ListViewItem(dtFile.Rows[0]["FILE_NAME3"].ToString());
                        lisFile.Items.Add(item);
                    }
                    if (dtFile.Rows[0]["FILE_NAME4"].ToString() != " ")
                    {
                        item = new ListViewItem(dtFile.Rows[0]["FILE_NAME4"].ToString());
                        lisFile.Items.Add(item);
                    }
                    if (dtFile.Rows[0]["FILE_NAME5"].ToString() != " ")
                    {
                        item = new ListViewItem(dtFile.Rows[0]["FILE_NAME5"].ToString());
                        lisFile.Items.Add(item);
                    }
                }
            }
            
        }
        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append(" SELECT SUBSTR(ABR.STEP1_CMT, 7,LENGTH(ABR.STEP1_CMT)) AS STEP1 " + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP2_CMT, 7,LENGTH(ABR.STEP2_CMT)) AS STEP2 " + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP3_CMT, 7,INSTR(ABR.STEP3_CMT,'자재처리 :')-7) AS STEP3_1" + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP3_CMT,(INSTR(ABR.STEP3_CMT,'자재처리 :')+7),LENGTH(ABR.STEP3_CMT)) AS STEP3_2 " + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP4_CMT, 7,LENGTH(ABR.STEP4_CMT)) AS STEP4 " + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP5_CMT, 8,LENGTH(ABR.STEP5_CMT)) AS STEP5 " + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP6_CMT, 7,LENGTH(ABR.STEP6_CMT)) AS STEP6 " + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP7_CMT, 7,INSTR(ABR.STEP7_CMT,'자재처리 :')-7) AS STEP7_1" + "\n");
            strSqlString.Append("     , SUBSTR(ABR.STEP7_CMT,(INSTR(ABR.STEP7_CMT,'자재처리 :')+7),LENGTH(ABR.STEP7_CMT)) AS STEP7_2 " + "\n");
            
            strSqlString.Append("  FROM CIQCABRLOT ABR " + "\n");;
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("      AND ABR.MAT_VER(+) = 0 " + "\n");
        //    strSqlString.Append("      AND ABR.ABR_NO  ='" + txtAbrNo.Text + "' \n");
            strSqlString.Append("      AND ABR.LOT_ID  = '" + txtLotId.Text + "' \n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();
        }
        // 파일리스트 조회
        private string MakeSqlFile()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append(" SELECT  FILE_NAME1, FILE_NAME2, FILE_NAME3, FILE_NAME4, FILE_NAME5   " + "\n");
            strSqlString.Append("  FROM CIQCABRLOT  " + "\n"); ;
            strSqlString.Append(" WHERE 1 = 1 " + "\n");
            strSqlString.Append("      AND MAT_VER(+) = 0 " + "\n");
     //       strSqlString.Append("      AND ABR_NO  ='" + txtAbrNo.Text + "' \n");
            strSqlString.Append("      AND LOT_ID  = '" + txtLotId.Text + "' \n");

            System.Windows.Forms.Clipboard.SetText(strSqlString.ToString());
            return strSqlString.ToString();

        }

        #region "Property Definition"

        private System.Windows.Forms.Form m_MyParent;

        public System.Windows.Forms.Form MyParent
        {
            
            get
            {
                if (m_MyParent == null)
                {
                    return null;
                }
                else
                {
                    return m_MyParent;
                }
            }
            set
            {
                if (value == null)
                {
                    m_MyParent = null;
                }
                else
                {
                    m_MyParent = value;
                }
            }
        }

        void trd_Start()
        {
            
            try
            {
                Process ps = Process.Start(m_file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
        #endregion

        private void lisFile_DoubleClick(object sender, EventArgs e)
        {
            string s_File_Name = "";
            //string m_file = null;
            // string m_file = null;

            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "\\tmp");

            try
            {
                if (lisFile.Items.Count > 0)
                {
                    if (lisFile.SelectedItems.Count > 0)
                    {
                        s_File_Name = lisFile.SelectedItems[0].Text;

                        if (s_File_Name == "") return;

                        m_file = s_File_Name;

                        System.Threading.Thread trd = new System.Threading.Thread(new System.Threading.ThreadStart(trd_Start));
                        trd.Start();
                    }

                }
            }
            catch (Exception ex)
            {

                CmnFunction.ShowMsgBox(ex.Message);
            }
        }

       
    }
}