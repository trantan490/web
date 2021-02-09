using System;
using System.Collections;
using Miracom.SmartWeb.FWX;
using Microsoft.Win32;

namespace Miracom.SmartWeb.RO
{
    public static class StdGlobalVariable
	{
        public static string gsResult;
        public static string gsErrorCode;
        public static string gsErrorMessage;

        public static string MP_COMMON_XML_FILE = "FN_COMMON.xml";
        public static string MP_RWEBFUNDEF_XML_FILE = "DB_RWEBFUNDEF.xml";
        public static string MP_RWEBFUNITM_XML_FILE = "DB_RWEBFUNITM.xml";
        public static string MP_RWEBGRPDEF_XML_FILE = "DB_RWEBGRPDEF.xml";
        public static string MP_RWEBGRPFUN_XML_FILE = "DB_RWEBGRPFUN.xml";
        public static string MP_RWEBUSRDEF_XML_FILE = "DB_RWEBUSRDEF.xml";
        public static string MP_RWEBUSRREG_XML_FILE = "DB_RWEBUSRREG.xml";
        public static string MP_RWEBUSRLOG_XML_FILE = "DB_RWEBUSRLOG.xml";
        public static string MP_RWEBFUNLOG_XML_FILE = "DB_RWEBFUNLOG.xml";
        public static string MP_RSUMWIPLTH_XML_FILE = "DB_RSUMWIPLTH.xml";
        public static string MP_MWIPLOTSTS_XML_FILE = "DB_MWIPLOTSTS.xml";

        public static string MP_WIP_STATUS_XML_FILE = "WIP_STATUS.xml";
        public static string MP_RAS_STATUS_XML_FILE = "RAS_STATUS.xml";
        public static string MP_PRODUCTION_OUTPUT_XML_FILE = "Production_Output.xml";
        public static string MP_PRODUCTION_PLAN_XML_FILE = "Production_Plan.xml";
        public static string MP_PRODUCTIVITY_XML_FILE = "Productivity.xml";
        public static string MP_RESOURCE_ANALYSIS_XML_FILE = "Resource_Analysis.xml";
        public static string MP_QUALITY_XML_FILE = "Quality.xml";
        public static string MP_TRACEABILITY_XML_FILE = "Traceability.xml";

        public static Config ROXml = new Config();
        
        public static ROQuery DBQuery = new ROQuery();
        public static string QueryPath;
    }
}
