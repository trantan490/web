using System.Collections;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb
{
	public class GlobalVariable
	{

        #region "Global variable list."
        public static string gsComputerName;
        public static string gsApplicatonName;				// Application name.

		// Option 환경 설정 전역 변수
		public static int giIdleTime;                       // Client에 아무런 동작이 없는 시간 (if giIdleTime=giLogoutTime => Logout)
		public static string gDelimiter = "/";              // DateTime Foramt에서 구분문자 ("/", "-" , "." ) 
		public static string gsServerName = "MESServer";    // "MESServer"
        public static string gsSiteID = "MPUS";    // "MESServer"
        public static string gsRemoteAddress = "";
        public static string gsCentralFactory = "SYSTEM";
		public static string gsFactory = "SYSTEM";          // 사용자가 작업중인 Factory 이름

        //FACTORY 변수 추가 : JUHYEON.JUNG SOURCE MERGE ( Main Form /SmartWeb.xml에 셋팅한값으로 읽음 )
        public static string gsAssyDefaultFactory = "HMKA1";
        public static string gsTestDefaultFactory = "HMKT1";
        public static string gsFgsDefaultFactory = "FGS";
        public static string gsGlovalSite = "K1"; //v2, k1, v1...
        
        public static string gsUserGroup = "ADMIN_GROUP";   // User Group을 받는 변수
		public static string gsUserID = "SYS";              // User Name을 받는 변수
        public static string gsPassport;
        public static string gsPassword;                    // Password를 받는 변수
		public static bool gbShowMsgFlag = true;            //Successful Message를 표시해줄것인지의 여부
        public static string gsCustomer;                    //웹리포트 고객사의 경우 어떤 고객사인지 저장

        public static string gsSelFunc;                    //Treeview선택된 Function
        public static string gsSelFuncGrp;                 //Treeview선택된 Function의 group
        public static string gsSelFuncName;                //Treeview선택된 Function 이름(화면 Title에 사용) by John. 2008.10.06        

        public static int giLogOutTime;
        public static bool gbAutoRefresh = false;
        public static bool gbGridFlag = true;
        public static int giAutoRefreshTime = 300;
        public static bool gbListAutoRefresh = true;
        public static bool gbLogoutFlag = false;
        public static int giTimeOut;
		public static System.Windows.Forms.Form gfrmMDI;	// MDI Forms
		public static int giMessageSize = 8;                 //Caster Processing

        public static LANG_FORMAT geLanguageFormat = LANG_FORMAT.STANDARD;
        public static DATE_TIME_FORMAT geDateTimeFormat = DATE_TIME_FORMAT.SHORTDATETIME;
        public static string gsDateDelimiter = "/"; //DateTime Foramt?먯꽌 援щ텇臾몄옄 ("/", "-" , "." )
        public static string gsStyleName = "3D"; //3D Type or Flat Type

        public struct FactoryShiftInfoTag
        {
            public bool bVariableShift;
            public int iShiftCount;
            public char cShift1DayFlag;
            public string sShift1StartTime;
            public char cShift2DayFlag;
            public string sShift2StartTime;
            public char cShift3DayFlag;
            public string sShift3StartTime;
            public char cShift4DayFlag;
            public string sShift4StartTime;
        }
        public static FactoryShiftInfoTag gShiftInfor;

        public static string gsResult;
        public static string gsErrorCode;
        public static string gsErrorMessage;

        public struct gLanguageData
        {
            public string Key;
            public string Lang_1;
            public string Lang_2;
            public string Lang_3;
            public string Lang_4;
            public string Lang_5;
        }

		public static ArrayList gaMenuLanguage = new ArrayList();
		public static ArrayList gaButtonLanguage = new ArrayList();
		public static ArrayList gaTextLanguage = new ArrayList();

		public static char gcLanguage = '2';               // Default Language code.                                                                   " - ??3援?뼱)
		public static char gcPrevLanguage = '2';           // Previous Language ("0"- Standard, "1" - 영어, "2" - 한국어 , "3" - 제 3국어)

		// Language Function 관련
		public static string[,] gsMessageData;               //모든 메시지 데이터의 정보를 가지는 변수
		public static int giMaxMessageData = 300;            //메시지 데이터의 최대 개수를 가지는 변수
		public static int giMaxMessageDataOption = 2;        //사용하는 언어의 종류("0" - 영어, "1" - 한국어 , "2" - 제 3국어 : 3가지)
        #endregion

        #region "Web Client Global variable list."
        public static Config AppConfig = new Config();        // Smart Web config.
        public static XmlEdit AppOption = new XmlEdit();        // Smart Web Option.
        public static ErrorLog AppErrorLog = new ErrorLog();  // Smart Web error log.
        #endregion

        // 2012-10-30-임종우 : 지난주,금주,차주의 시작일,종료일,주차를 담는 변수 선언
        // 2015-09-21-임종우 : 6주까지 확대
        public struct FindWeek
        {
            public string StartDay_LastWeek;  // 지난주 시작일
            public string StartDay_ThisWeek;  // 금주 시작일
            public string StartDay_NextWeek;  // 차주 시작일
            public string StartDay_W2_Week;  // W2 시작일
            public string StartDay_W3_Week;  // W3 시작일
            public string StartDay_W4_Week;  // W4 시작일
            public string StartDay_W5_Week;  // W5 시작일
            public string EndDay_LastWeek;    // 지난주 종료일
            public string EndDay_ThisWeek;    // 금주 종료일
            public string EndDay_NextWeek;    // 차주 종료일
            public string EndDay_W2_Week;    // W2 종료일
            public string EndDay_W3_Week;    // W3 종료일
            public string EndDay_W4_Week;    // W4 종료일
            public string EndDay_W5_Week;    // W5 종료일
            public string LastWeek;           // 지난주 주차
            public string ThisWeek;           // 금주 주차
            public string NextWeek;           // 차주 주차
            public string W2_Week;           // W2 주차
            public string W3_Week;           // W3 주차
            public string W4_Week;           // W4 주차
            public string W5_Week;           // W5 주차
        }

        // 2016-07-12-임종우 : 설비 Capa 효율을 모든 화면에 동일하게 적용하기 위해 선언
        public static double gdPer_wb = 0.71;
        public static double gdPer_da = 0.68;
        public static double gdPer_etc = 0.7;
    }
}
