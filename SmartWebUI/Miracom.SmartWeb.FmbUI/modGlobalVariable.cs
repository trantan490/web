
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
//using FMBUI.Enums;


namespace Miracom.SmartWeb.UI
{
    public sealed class modGlobalVariable
    {

        //public static string gsClientVersion; //Server Program Version
        //public static string gsServerVersion; //Client Program Version
        //public static string gsUpgradeFile; //Upgrade Program Name
        //public static string gsDownloadFileList; //Download 받을 리스트 파일

        //public static System.Windows.Forms.Form gfrmMDI;

        //public static bool gbLogoutFlag = false;

        public static int giToolType = System.Convert.ToInt32(Miracom.FMBUI.Enums.eToolType.Null);
        public static TreeNode gNodeSelectedRes = null;
        //public static int giGridSize = 4;
        //public static System.Windows.Forms.ImageList gimlSmallIcon; //대표되는 Small Imagelist
        public static System.Windows.Forms.ImageList gimlResource; //대표되는 Small Imagelist FMB전용

        //public static string gsPassport;
        //public static string gsFactory = "DEMO"; //사용자가 작업중인 Factory 이름
        //public static string gsUserID = "ADMIN"; //User Name을 받는 변수
        //public static string gsPassword; //Password를 받는 변수
        //public static string gsSiteID = "MPKW"; //Server의 Site ID ("MPD1")"
        //public static string gsUserGroup = "ADMIN_GROUP"; //사용자의 User Group 정보 (Alarm publish Filtering 에 이용)
        //public static string gsComputerName; //사용자의 Computer 이름
        //public static string gsServerName = "MESServer"; //"MESServer"

        //public static int giLogOutTime; //Auto logout 시간 ( 이 시간동안 아무 동작이 없으면 Client는 자동종료 된다.)
        //public static int giIdleTime; //Client에 아무런 동작이 없는 시간 (if giIdleTime=giLogoutTime => Logout)
        //public static string gsRemoteAddress; //Station의 Remote Addres
        //public static int giTimeOut; //서버 RequestReply시 제한 통신시간
        //public static bool gbGridFlag = true; //listview 컨트롤에 Grid를 표시해줄것인지의 여부
        //public static bool gbLoginFlag = false; //Login 화면에 Login History를 표시해줄것인지의 여부
        //public static bool gbAutoRefresh = false; //Client 자동 갱신 여부
        //public static int giAutoRefreshTime = 300; //Clinet Main Lot List 자동 갱신 시간
        //public static bool gbListAutoRefresh = true; //Client List 자동 갱신 여부
        //public static bool gbProcessCaster = false; //Caster Processing
        //public static bool gbShowMsgFlag = true; //Successful Message를 표시해줄것인지의 여부
        //public static string gsStyleName = "3D"; //Successful Message를 표시해줄것인지의 여부

        public static bool gbPublishFlag; //Publish Message인지 여부
        //public static string gsMessage; //Publish Message Data

        public static char gsAutoUpgrade = '1'; //자동 Upgrade 여부
        //public static bool gbUseSmallLetter = true; //소문자 사용여부

        //public static char gcLanguage = '1'; //Client에서 사용하는 언어 ("0"- Standard, "1" - 영어, "2" - 한국어 , "3" - 제 3국어)
        public static char gsPrevLanguage = '1';

        //public static DATE_TIME_FORMAT geDateTimeFormat = DATE_TIME_FORMAT.SHORDATETIME;
        //public static LANG_FORMAT geLanguageFormat = LANG_FORMAT.KOREA;
        public static string gDelimiter = "/"; //DateTime Foramt에서 구분문자 ("/", "-" , "." )

        public static clsGlobalOptions gGlobalOptions = new clsGlobalOptions();

        //public static int giMessageSize = 8; //Caster Processing

        public static bool gbAllFactory = false; //View All Factory

        public static string FMBChannel = string.Empty;
        public static string UTLChannel = string.Empty;

        public static int FMBTTL = 0;
        public static int UTLTTL = 0;

        //public static FMBCoreCaster FMBFunc = new FMBCoreCaster();
        //public static UTLCoreCaster UTLFunc = new UTLCoreCaster();
        //public static FMBCoreTuner FMBTune = new FMBPublish();
        //public static UTLCoreTuner UTLTune = new UTLCoreTuner();

        //public struct gLanguageData
        //{
        //    public string Key;
        //    public string Lang_1;
        //    public string Lang_2;
        //    public string Lang_3;
        //}

        //public static ArrayList gaMenuLanguage = new ArrayList();
        //public static ArrayList gaButtonLanguage = new ArrayList();
        //public static ArrayList gaTextLanguage = new ArrayList();

        //public static string[,] gsMessageData; //모든 메시지 데이터의 정보를 가지는 변수
        //public static int giMaxMessageData = 200; //메시지 데이터의 최대 개수를 가지는 변수
        //public static int giMaxMessageDataOption = 2; //사용하는 언어의 종류("0" - 영어, "1" - 한국어 , "2" - 제 3국어 : 3가지)

//#if _SPCTYPE
//        public const string gsSPCType = "1";
//#elif _spctype
//        public const string gsSPCType = "2";
//#elif _SPCTYPE
//        public const string gsSPCType = "3";
//#else
//        public const string gsSPCType = "1";
//#endif

        public struct gLayouts
        {
            public string sFormName;
            public string sTag;
        }

    }

}
