using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using Miracom.SmartWeb.FWX;
using System.Collections;

using System.Data.OleDb;

namespace Miracom.SmartWeb.UI
{
    public class ROWebService
    {

        public SmartWebService oWebService = new SmartWebService();

        public ROWebService()
        {
            
        }

        public void SetUrl()
        {
            oWebService.SetUrl(GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='WebServiceUrl']"));
            GlobalVariable.gsSiteID = GlobalVariable.AppConfig.GetValue("//appSettings//add[@key='WebServiceUrl']");
        }

        public void SetTimeOut(int iTimeOut)
        {
            try
            {
                oWebService.SetTimeOut(iTimeOut);
                GlobalVariable.giTimeOut = iTimeOut;
            }
            catch
            {
            }
        }

        #region Handle By Function

        public bool RequestReply(string FunctionName, string sRequest, ref string sReply)
        {
            try
            {
                bool bRet = false;
                bRet = oWebService.RequestReply(FunctionName, sRequest, ref sReply);
                
                return bRet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetFuncDataTable(string FunctionName, string QueryCond)
        {
            DataTable dt = null;

            try
            {
                string sReturn;

                //============== Log 입력 ==============//
                string Log_QueryCond = null;
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsFactory);
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsUserID);
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsSelFunc);
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsSelFuncGrp);

                if (GlobalVariable.gsSelFunc != null && SelectDataCount("RWEBFUNLOG", "1", Log_QueryCond) == 0) //10분내외 똑같은 function log는 쌓지않음
                {
                    CmnFunction.oComm.InsertData("RWEBFUNLOG", "1", Log_QueryCond);
                }
                //============== Log 입력 ==============//

                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }
                //}

				sReturn = CmnFunction.GetErrorMessage(oWebService.GetFuncDataTable(FunctionName, QueryCond));
                dt = FwxCmnFunction.StringToDataTable(sReturn);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable fnExecutePackage(String sFuncName, String sPackage, String sParameter)
        {
            /************************************************************
             * comment : Oracle Package/Procedure를 실행하기 위하여 추가함.
             * 
             * created by : bee-jae jung(2010-08-12-목요일)
             * 
             * modified by : bee-jae jung(2010-08-12-목요일)
             ************************************************************/
            DataTable dt = null;
            String sReturn = "";
            char cDiv = '│';       // 2010-08-12-정비재 : ㅂ + 한자 key를 클릭하면 2번
            try
            {
                /************ Log 입력 Start ************/
                String Log_QueryCond = null;
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsFactory);
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsUserID);
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsSelFunc);
                Log_QueryCond = FwxCmnFunction.PackCondition(Log_QueryCond, GlobalVariable.gsSelFuncGrp);

                // 10분 내외 똑같은 function log는 쌓지않음
                if (GlobalVariable.gsSelFunc != null && SelectDataCount("RWEBFUNLOG", "1", Log_QueryCond) == 0)
                {
                    CmnFunction.oComm.InsertData("RWEBFUNLOG", "1", Log_QueryCond);
                }
                /************ Log 입력 End ************/


                /********************************************************************************************************/
                // 2010-08-12-정비재 : Oracle Package / Procedure용 Query문을 생성한다.
                String sPackageQuery = null;
                sPackageQuery = FwxCmnFunction.PackCondition(sPackageQuery, "PACKAGE");
                sPackageQuery = FwxCmnFunction.PackCondition(sPackageQuery, sPackage);
                for (int iRow = 0; iRow < sParameter.Split(cDiv).Length; iRow++)
                {
                    sPackageQuery = FwxCmnFunction.PackCondition(sPackageQuery, sParameter.Split(cDiv)[iRow].Trim());
                }
                /********************************************************************************************************/

                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(sPackageQuery))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(sPackageQuery);
                //    }
                //}

                sReturn = CmnFunction.GetErrorMessage(oWebService.GetFuncDataTable(sFuncName, sPackageQuery));
                dt = FwxCmnFunction.StringToDataTable(sReturn);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
        }

        public DataSet GetFuncDataSet(string FunctionName, string QueryCond)
        {
            DataSet ds = null;
            try
            {
                string sReturn;

                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }                    
                //}

                sReturn = CmnFunction.GetErrorMessage(oWebService.GetFuncDataSet(FunctionName, QueryCond));
                ds = FwxCmnFunction.StringToDataSet(sReturn);
                
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetFuncDataInt(string FunctionName, string QueryCond)
        {
            int i = 0;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }                    
                //}

                i = oWebService.GetFuncDataInt(FunctionName, QueryCond);

                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFuncDataString(string FunctionName, string QueryCond)
        {
            string sTemp = "";
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }
                //}

                sTemp = oWebService.GetFuncDataString(FunctionName, QueryCond);
                return sTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Handle By Table

        public int SelectDataCount(string TableName, string Step, string QueryCond)
        {
            int i = 0;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }                    
                //}

                i = oWebService.SelectDataCount(TableName, Step, QueryCond);
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable SelectData(string TableName, string Step, string QueryCond)
        {
            DataTable dt = null;
            string sReturn;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }
                //}

                sReturn = CmnFunction.GetErrorMessage(oWebService.SelectData(TableName, Step, QueryCond));
                dt = FwxCmnFunction.StringToDataTable(sReturn);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable FillData(string TableName, string Step, string QueryCond)
        {
            DataTable dt = null;
            string sReturn;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }
                //}

                sReturn = CmnFunction.GetErrorMessage(oWebService.FillData(TableName, Step, QueryCond));
                dt = FwxCmnFunction.StringToDataTable(sReturn);
                
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertData(string TableName, string Step, string QueryCond)
        {
            bool bResult = false;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }                    
                //}

                bResult = oWebService.InsertData(TableName, Step, QueryCond);

                return bResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateData(string TableName, string Step, string QueryCond)
        {
            bool bResult = false;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }
                //}

                bResult = oWebService.UpdateData(TableName, Step, QueryCond);
                return bResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteData(string TableName, string Step, string QueryCond)
        {
            bool bResult = false;
            try
            {
                // 2012-08-22-임종우 : 모든 이벤트에 발생되어 부하 발생. 해당 기능 주석 처리
                // 2010-09-02-정비재 : Query를 ClipBoard에 저장한다.
                //if (GlobalVariable.gsUserGroup == "ADMIN_GROUP" || GlobalVariable.gsUserGroup == "HANA_ADMIN_GROUP")
                //{
                //    // 2010-09-03-임종우 : QueryCond에 값이 있을 경우에만 저장
                //    if (!String.IsNullOrEmpty(QueryCond))
                //    {
                //        System.Windows.Forms.Clipboard.SetText(QueryCond);
                //    }
                //}

                bResult = oWebService.DeleteData(TableName, Step, QueryCond);
                return bResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


    }
}
