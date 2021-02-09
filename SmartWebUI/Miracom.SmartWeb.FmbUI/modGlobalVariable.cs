
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
        //public static string gsDownloadFileList; //Download ���� ����Ʈ ����

        //public static System.Windows.Forms.Form gfrmMDI;

        //public static bool gbLogoutFlag = false;

        public static int giToolType = System.Convert.ToInt32(Miracom.FMBUI.Enums.eToolType.Null);
        public static TreeNode gNodeSelectedRes = null;
        //public static int giGridSize = 4;
        //public static System.Windows.Forms.ImageList gimlSmallIcon; //��ǥ�Ǵ� Small Imagelist
        public static System.Windows.Forms.ImageList gimlResource; //��ǥ�Ǵ� Small Imagelist FMB����

        //public static string gsPassport;
        //public static string gsFactory = "DEMO"; //����ڰ� �۾����� Factory �̸�
        //public static string gsUserID = "ADMIN"; //User Name�� �޴� ����
        //public static string gsPassword; //Password�� �޴� ����
        //public static string gsSiteID = "MPKW"; //Server�� Site ID ("MPD1")"
        //public static string gsUserGroup = "ADMIN_GROUP"; //������� User Group ���� (Alarm publish Filtering �� �̿�)
        //public static string gsComputerName; //������� Computer �̸�
        //public static string gsServerName = "MESServer"; //"MESServer"

        //public static int giLogOutTime; //Auto logout �ð� ( �� �ð����� �ƹ� ������ ������ Client�� �ڵ����� �ȴ�.)
        //public static int giIdleTime; //Client�� �ƹ��� ������ ���� �ð� (if giIdleTime=giLogoutTime => Logout)
        //public static string gsRemoteAddress; //Station�� Remote Addres
        //public static int giTimeOut; //���� RequestReply�� ���� ��Žð�
        //public static bool gbGridFlag = true; //listview ��Ʈ�ѿ� Grid�� ǥ�����ٰ������� ����
        //public static bool gbLoginFlag = false; //Login ȭ�鿡 Login History�� ǥ�����ٰ������� ����
        //public static bool gbAutoRefresh = false; //Client �ڵ� ���� ����
        //public static int giAutoRefreshTime = 300; //Clinet Main Lot List �ڵ� ���� �ð�
        //public static bool gbListAutoRefresh = true; //Client List �ڵ� ���� ����
        //public static bool gbProcessCaster = false; //Caster Processing
        //public static bool gbShowMsgFlag = true; //Successful Message�� ǥ�����ٰ������� ����
        //public static string gsStyleName = "3D"; //Successful Message�� ǥ�����ٰ������� ����

        public static bool gbPublishFlag; //Publish Message���� ����
        //public static string gsMessage; //Publish Message Data

        public static char gsAutoUpgrade = '1'; //�ڵ� Upgrade ����
        //public static bool gbUseSmallLetter = true; //�ҹ��� ��뿩��

        //public static char gcLanguage = '1'; //Client���� ����ϴ� ��� ("0"- Standard, "1" - ����, "2" - �ѱ��� , "3" - �� 3����)
        public static char gsPrevLanguage = '1';

        //public static DATE_TIME_FORMAT geDateTimeFormat = DATE_TIME_FORMAT.SHORDATETIME;
        //public static LANG_FORMAT geLanguageFormat = LANG_FORMAT.KOREA;
        public static string gDelimiter = "/"; //DateTime Foramt���� ���й��� ("/", "-" , "." )

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

        //public static string[,] gsMessageData; //��� �޽��� �������� ������ ������ ����
        //public static int giMaxMessageData = 200; //�޽��� �������� �ִ� ������ ������ ����
        //public static int giMaxMessageDataOption = 2; //����ϴ� ����� ����("0" - ����, "1" - �ѱ��� , "2" - �� 3���� : 3����)

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
