using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using Miracom.SmartWeb.FWX;
using Miracom.SmartWeb.RO;
using System.Collections;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SmartWebService : System.Web.Services.WebService
{
    MainRO ROMain = new MainRO();

    public SmartWebService()
    {
        //디자인된 구성 요소를 사용하는 경우 다음 줄의 주석 처리를 제거합니다. 
        //InitializeComponent(); 
    }

    #region Handle By Function
    [WebMethod]
    public bool RequestReply(string FunctionName, string sRequest, ref string sReply)
    {
        bool bRet;
        try
        {
            bRet = ROMain.RequestReply(FunctionName, sRequest, ref sReply);
            return bRet;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    [WebMethod]
    public string GetFuncDataTable(string FunctionName, string QueryCond)
    {
        string sRet;
        try
        {
            sRet = ROMain.GetFuncDataTable(FunctionName, QueryCond);
            return sRet;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string GetFuncDataSet(string FunctionName, string QueryCond)
    {
        string sRet;
        try
        {

            sRet = ROMain.GetFuncDataSet(FunctionName, QueryCond);
            return sRet;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public int GetFuncDataInt(string FunctionName, string QueryCond)
    {
        int i = 0;
        try
        {
            i = ROMain.GetFuncDataInt(FunctionName, QueryCond);

            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string GetFuncDataString(string FunctionName, string QueryCond)
    {
        string sTemp = "";
        try
        {
            sTemp = ROMain.GetFuncDataString(FunctionName, QueryCond);
            return sTemp;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    #region Handle By Table

    [WebMethod]
    public int SelectDataCount(string TableName, string Step, string QueryCond)
    {
        int i = 0;
        try
        {
            i = ROMain.SelectDataCount(TableName, Step, QueryCond);
            return i;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string SelectData(string TableName, string Step, string QueryCond)
    {
        string sRet;
        try
        {
            sRet = ROMain.SelectData(TableName, Step, QueryCond);
            return sRet;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string FillData(string TableName, string Step, string QueryCond)
    {
        string sRet;
        try
        {
            sRet = ROMain.FillData(TableName, Step, QueryCond);
            return sRet;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public bool InsertData(string TableName, string Step, string QueryCond)
    {
        bool bResult = false;
        try
        {
            bResult = ROMain.InsertData(TableName, Step, QueryCond);
            return bResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public bool UpdateData(string TableName, string Step, string QueryCond)
    {
        bool bResult = false;
        try
        {
            bResult = ROMain.UpdateData(TableName, Step, QueryCond);
            return bResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public bool DeleteData(string TableName, string Step, string QueryCond)
    {
        bool bResult = false;
        try
        {
            bResult = ROMain.DeleteData(TableName, Step, QueryCond);
            return bResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

}
