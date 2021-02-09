using System;
using System.Data;

namespace Miracom.SmartWeb.RO
{

    public class ROQuery : Miracom.Query.QueryComponent
    {
        
        public ROQuery() 
		{
            
		}

        public void Init()
        {

        }

        public bool FuncExecute(string QueryID, string[] DynamicQueryElements, string[] ParamValues)
        {
            try
            {

                this.Execute(QueryID, DynamicQueryElements, ParamValues);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public DataTable GetFuncDataTable(string QueryID, string[] DynamicQueryElements, string[] ParamValues)
        {
            DataTable dt = null;
            try
            {

                dt = this.GetDataTable(QueryID, DynamicQueryElements, ParamValues);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null) dt.Dispose();
            }
        }

        public DataSet GetFuncDataSet(string QueryID, string[] DynamicQueryElements, string[] ParamValues)
        {
            DataSet ds = null;
            try
            {
                ds = this.GetDataSet(QueryID, DynamicQueryElements, ParamValues);

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }

        public int GetFuncDataInt(string QueryID, string[] DynamicQueryElements, string[] ParamValues)
        {
            DataTable dt = null;
            int i = 0;
            try
            {
                dt = this.GetDataTable(QueryID, DynamicQueryElements, ParamValues);
                i = int.Parse(dt.Rows[0][0].ToString());
                return i;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null) dt.Dispose();
            }
        }

        public string GetFuncDataString(string QueryID, string[] DynamicQueryElements, string[] ParamValues)
        {
            DataTable dt = null;
            string sData = "";
            try
            {
                dt = this.GetDataTable(QueryID, DynamicQueryElements, ParamValues);
                sData = dt.Rows[0][0].ToString();
                return sData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dt != null) dt.Dispose();
            }
        }

    }
}
