using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb.UI
{
    public partial class frmPassword : Form
    {
        public frmPassword()
        {
            InitializeComponent();
        }

        private Boolean setSmartWebOption()
        {
            try
            {
                txtNowPass.Text = "";
                txtNewPass.Text = "";
                txtNewPassCheck.Text = "";

                return true;             
            }
            catch
            {
                return false;
            }

        }

        private bool CheckField()
        {
            if(txtNowPass.Text == "")
            {
                MessageBox.Show("현재 비밀번호를 입력하세요!!!", "알림");
                return false;
            }
            else if(txtNewPass.Text == "")
            {
                MessageBox.Show("새 비밀번호를 입력하세요!!!", "알림");
                return false;
            }
            else if(txtNewPassCheck.Text == "")
            {
                MessageBox.Show("새 비밀번호 확인을 입력하세요!!!", "알림");
                return false;
            }
            else if (txtNewPass.Text.Length < 8)
            {
                MessageBox.Show("새 비밀번호는 8자 이상 입력해 주세요!!!", "알림");
                return false;
            }
            else if(txtNewPass.Text.Trim() != txtNewPassCheck.Text.Trim())
            {
                MessageBox.Show("새 비밀번호가 일치 하지 않습니다!!!", "알림");
                return false;
            }

            return true;
        }

        private void frmOption_Load(object sender, EventArgs e)
        {
            setSmartWebOption();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckField() == false)
                return;

            DataTable dt1 = null;
            DataTable dt2 = null;
            DataTable dt3 = null;
            string input_old_pwd = string.Empty;
            string real_old_pwd = string.Empty;
            int is_customer = 0;
            string QueryCond = string.Empty;

            try
            {
                dt1 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString2());    // 커스터머 카운트            
                dt2 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString1());    // input password       
                dt3 = CmnFunction.oComm.GetFuncDataTable("DYNAMIC", MakeSqlString());     // now password

                if(dt1.Rows.Count == 0)
                {
                    dt1.Dispose();
                    return;
                }
                else
                {
                    is_customer = Convert.ToInt32(dt1.Rows[0][0].ToString());
                }
                
                if(dt2.Rows.Count == 0)
                {
                    dt2.Dispose();
                    return;
                }
                else
                {
                    input_old_pwd = dt2.Rows[0][0].ToString();
                }
                if(dt3.Rows.Count == 0)
                {
                    dt3.Dispose();
                    return;
                }
                else
                {
                    real_old_pwd = dt3.Rows[0][0].ToString();                
                }
                
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsUserID);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, txtNewPass.Text);
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, "SYSTEM");
                QueryCond = FwxCmnFunction.PackCondition(QueryCond, GlobalVariable.gsUserID);

                if(is_customer > 0)    // 커스터머 ID인지 확인
                {                    
                    if(input_old_pwd == real_old_pwd)   // 입력된 PASS가 맞는지 확인
                    {                        
                        if(CmnFunction.oComm.UpdateData("RWEBUSRDEF", "2", QueryCond) == true)
                        {                            
                            MessageBox.Show("비밀 번호 변경이 완료 되었습니다", "알림");
                        }
                        else
                        {
                            MessageBox.Show("비밀 번호 변경 실패!!!", "알림");
                            return;
                        }                                
                    }
                    else
                    {
                        MessageBox.Show("현재 비밀번호가 일치 하지 않습니다!!!", "알림");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("고객사 ID만 Password 변경이 가능합니다!!" + "\n" + "관리자에게 문의 하세요", "알림");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dt1.Dispose();
            dt2.Dispose();
            dt3.Dispose();

            this.Close();
        }

        private string MakeSqlString()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("           SELECT PASSWORD  " + "\n");
            strSqlString.Append("             FROM RWEBUSRDEF  " + "\n");
            strSqlString.Append("            WHERE FACTORY =  'SYSTEM'" + "\n");
            strSqlString.Append("              AND USER_ID = '" + GlobalVariable.gsUserID  + "' " + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString1()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append( "SELECT PK_MESPWD.ENCPASSWORD('" + GlobalVariable.gsUserID + "','" + txtNowPass.Text + "') AS PASSWORD FROM DUAL " + "\n");

            return strSqlString.ToString();
        }

        private string MakeSqlString2()
        {
            StringBuilder strSqlString = new StringBuilder();

            strSqlString.Append("        SELECT NVL(COUNT(*),0)  " + "\n");
            strSqlString.Append("          FROM MGCMTBLDAT  " + "\n");
            strSqlString.Append("         WHERE TABLE_NAME = 'H_CUSTOMER'  " + "\n");
            strSqlString.Append("           AND KEY_1 = (SELECT USER_GRP_3 FROM RWEBUSRDEF WHERE USER_ID = '" + GlobalVariable.gsUserID + "')  " + "\n");


            return strSqlString.ToString();
        }
    }
}