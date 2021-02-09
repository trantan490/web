
namespace Miracom.SmartWeb
{
	public class GlobalConstant
	{
        #region "Global constant list."

        public const string MP_FUNC_NAME_SEPARATOR = "SEPARATOR";

        public const string MP_GCM_FLEXWIP_COL_TBL = "FLEXWIP_COLUMN";
        public const string MP_GCM_FLEXWIP_GROUP = "FLEXWIP_GROUP";
        public const string MP_GCM_FLEXWIP_GROUP_ITEM = "FLEXWIP_GROUP_ITEM";

        public const int SP_MAX_COLUMN_WIDTH = 500;
        public const int SP_MIN_COLUMN_WIDTH = 20;
        public const char MP_SUCCESS_STATUS = '0';
        public const char MP_STEP_CREATE = 'I';
        public const char MP_STEP_UPDATE = 'U';
        public const char MP_STEP_DELETE = 'D';

        /*========= System MEssage Group GCM Table =========*/
        //Material Group Table 1~10 string[] CondList = null;
        public static string[] MP_GCM_MATERIAL_GRP = { "MATERIAL_GRP_1", "MATERIAL_GRP_2", "MATERIAL_GRP_3", 
                                                        "MATERIAL_GRP_4", "MATERIAL_GRP_5", "MATERIAL_GRP_6",  
                                                        "MATERIAL_GRP_7","MATERIAL_GRP_8", "MATERIAL_GRP_9", "MATERIAL_GRP_10" };
        //Flow Group Table 1~10
        public static string[] MP_GCM_FLOW_GRP ={ "FLOW_GRP_1", "FLOW_GRP_2", "FLOW_GRP_3",
                                           "FLOW_GRP_4", "FLOW_GRP_5", "FLOW_GRP_6",
                                           "FLOW_GRP_7","FLOW_GRP_8", "FLOW_GRP_9", "FLOW_GRP_10" };
        //Operation Group Table 1~10
        public static string[] MP_GCM_OPER_GRP ={ "OPER_GRP_1", "OPER_GRP_2", "OPER_GRP_3", 
                                           "OPER_GRP_4", "OPER_GRP_5", "OPER_GRP_6",  
                                           "OPER_GRP_7","OPER_GRP_8", "OPER_GRP_9", "OPER_GRP_10" };
        //Resource Group Table 1~10
        public static string[] MP_GCM_RESOURCE_GRP ={ "RES_GRP_1", "RES_GRP_2", "RES_GRP_3", 
                                               "RES_GRP_4", "RES_GRP_5", "RES_GRP_6",  
                                               "RES_GRP_7","RES_GRP_8", "RES_GRP_9", "RES_GRP_10" };
        //Event Group Table 1~10
        public static string[] MP_GCM_EVENT_GRP ={ "EVN_GRP_1", "EVN_GRP_2", "EVN_GRP_3", 
                                            "EVN_GRP_4", "EVN_GRP_5", "EVN_GRP_6",  
                                            "EVN_GRP_7","EVN_GRP_8", "EVN_GRP_9", "EVN_GRP_10" };
        /*========= System MEssage Group GCM Table =========*/


        #endregion

        #region "외부 파일을 나타내는 상수"
        public const string MP_CAPTION_FILE = "MESCaption.xml";
		public const string MP_MESSAGE_FILE = "MESMessage.xml";
        #endregion
	}

    #region "Global enum list."
	public enum SMALLICON_INDEX
	{
		IDX_FACTORY = 0,
        IDX_AREA = 1,
		IDX_SHIP_FACTORY = 2,
		IDX_TRANSACTION = 10,
		IDX_SETUP = 11,
		IDX_EVENT = 12,
		IDX_CODE_TABLE = 13,
		IDX_CODE_DATA = 14,
		IDX_MATERIAL = 15,
		IDX_FLOW = 16,
		IDX_REWORK_FLOW = 17,
		IDX_OPER = 18,
		IDX_MFO = 19,
		IDX_SEC_GROUP = 20,
		IDX_USER = 21,
		IDX_EQUIP = 22,
		IDX_EQUIP_PROC = 23,
		IDX_EQUIP_DOWN = 24,
		IDX_EQUIP_GROUP = 25,
		IDX_SUB_EQUIP = 26,
		IDX_STATUS = 27,
		IDX_CHARACTER = 28,
		IDX_COL_SET = 29,
		IDX_COL_SET_DELETE = 30,
		IDX_COL_SET_AUTO = 31,
		IDX_CASSETTE = 32,
		IDX_CASSETTE_EMPTY = 33,
		IDX_CASSETTE_FULL = 34,
		IDX_LOT = 35,
		IDX_LOT_CHECK = 36,
		IDX_LOT_HOLD = 37,
		IDX_LOT_HOLD_CHECK = 38,
		IDX_LOT_RELEASE = 39,
		IDX_LOT_REWORK = 40,
		IDX_LOT_REWORK_CHECK = 41,
		IDX_LOT_ALTER = 42,
		IDX_LOT_ALTER_CHECK = 43,
		IDX_LOT_START = 44,
		IDX_LOT_START_CHECK = 45,
		IDX_CHART = 46,
		IDX_CHART_LINE = 47,
		IDX_LOG_SHEET = 48,
		IDX_BAY = 50,
		IDX_FUNCTION_GROUP = 51,
		IDX_FUNCTION = 52,
		IDX_MESSAGE_GROUP = 53,
		IDX_MESSAGE = 54,
		IDX_PRIORITY = 55,
		IDX_BOM = 56,
		IDX_PORT = 57,
		IDX_ORDER = 58,
		IDX_ORDER_DELETE = 59,
		IDX_PART = 60,
		IDX_INQUIRY = 61,
		IDX_SUB_LOT = 62,
		IDX_HISTORY = 63,
		IDX_HISTORY_DELETE = 64,
		IDX_ALARM = 65,
		IDX_DEPARTMENT = 66,
		IDX_CALENDAR = 67,
		IDX_KEY = 68,
		IDX_CUSTOMER = 69,
		IDX_CATEGORY = 70,
		IDX_YEAR = 71,
		IDX_MONTH = 72,
		IDX_PM = 73,
		IDX_POLICY = 74,
		IDX_OPTION = 75,
		IDX_SLOT_EMPTY = 76,
		IDX_SLOT_FULL = 77,
		IDX_SLOT_FULL_NEW = 78,
		IDX_SLOT_COMBINE = 79,
		IDX_RECIPE = 80,
		IDX_STOCKER = 81,
		IDX_LABEL = 84,
		IDX_ALARM_ERROR = 88,
		IDX_ALARM_WARN = 89,
		IDX_ALARM_INFO = 90,
		IDX_REPAIR_LOT = 92,
		IDX_DISPATCHER = 93,
		//현재 아이콘이 없어서 다른 아이콘을 대신 사용하므로 나중에 수정되어야 함
		IDX_COL_SET_VERSION = 29,
		IDX_PRIVILEGE = 82,
		IDX_PRIVILEGE_GROUP = 83,
		IDX_TOOL = 85,
		IDX_TOOL_EVENT = 86,
		IDX_TOOL_TYPE = 87,
		IDX_TOOL_SCRAP = 91,
		IDX_TOOL_RETURN = 94,
		IDX_EQ_TYPE = 95,
		IDX_RCP_MGR_DIR = 96,
		IDX_MODULE_DIR = 97,
		IDX_MODULE = 98,
		IDX_RCP_HOLD = 99,
		IDX_VERSION = 100,
		IDX_VERSION_REQUEST = 101,
		IDX_VERSION_APPROVAL = 102,
		IDX_VERSION_ACTIVATE = 103,
        IDX_REPORT_WHITE = 104,
        IDX_REPORT_SEC_GRP = 105,
        IDX_REPORT_FUNCTION = 106,
        IDX_REPORT_MENU = 107
		//앞으로 추가되어야 함
	}

	public enum TOOLICON_INDEX
	{
		ICON_INDEX_FACTORY_SETUP = 0
	}

	public enum LANG_FORMAT
	{
		STANDARD = 0,
		ENGLISH = 1,
		KOREA = 2,
        VIETNAM = 3,
        BRAZIL = 4,
		ETC = 5
	}

	public enum DATE_TIME_FORMAT
	{
		LONGDATETIME = 0,
		SHORTDATETIME = 1,
		LONGDATE = 2,
		SHORTDATE = 3,
		LONGTIME = 4,
		SHORTTIME = 5,
		NONE = 6

		//STANDARD
		//YYYY/MM/DD HH:MM:SS
		//YYYY/MM/DD HH:MM:SS
		//YYYY/MM/DD
		//YYYY/MM/DD
		//HH:MM:SS
		//HH:MM:SS

		//ENGLISH
		//MM DD,YYYY HH:MM:SS
		//YYYY/MM/DD HH:MM:SS
		//MM DD,YYYY
		//YYYY/MM/DD
		//HH:MM:SS
		//HH:MM:SS

		//KOREA
		//YYYY년 MM월 DD일 오전(오후) hh시 mm분 ss초
		//YYYY/MM/DD HH:MM:SS
		//YYYY년 MM월 DD일
		//YYYY/MM/DD 
		//HH시 mm분 ss초
		//HH:MM:SS
	}
    #endregion

}
