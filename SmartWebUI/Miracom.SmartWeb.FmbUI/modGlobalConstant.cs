
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace Miracom.SmartWeb.UI
{
    public sealed class modGlobalConstant
    {

        #region "Transaction 결과를 알리는 상수"

        public const int MP_SUCCESS = 0;
        public const int MP_FAIL = 1;

        public const char MP_SUCCESS_STATUS = '0';
        public const char MP_FAIL_STATUS = '1';
        public const char MP_TRBL_STATUS = '3';

        #endregion

        #region "Process Step 상수"

        public const char MP_STEP_CREATE = 'I';
        public const char MP_STEP_UPDATE = 'U';
        public const char MP_STEP_DELETE = 'D';
        public const char MP_STEP_NOTHING = ' ';
        public const char MP_STEP_VIEW = 'V';

        #endregion

        #region "FMB Catetory Constant"

        public const string FMB_CATEGORY_FACTORY = "FACTORY";
        public const string FMB_CATEGORY_LAYOUT = "LAYOUT";
        public const string FMB_CATEGORY_GROUP = "GROUP";
        public const string FMB_CATEGORY_RESOURCE = "RESOURCE";
        public const string FMB_CATEGORY_TAG = "TAG";

        public const string FMB_CATEGORY_FILE = "FILE";

        #endregion

        #region "FMB Resource/Tag Type Constant"

        public const char FMB_RESOURCE_TYPE = 'R';
        public const char FMB_TAG_TYPE = 'T';

        #endregion

        #region "Control 관련 상수"

        public const int LINE_MININUM_SIZE = 1;
        public const int CTRL_MININUM_SIZE = 15;
        public const int CTRL_MAXIMUM_SIZE = 500;

        #endregion

        #region "LayOut 관련 상수"

        public const int MIN_VALUE = 300;
        public const int MAX_VALUE = 10000;

        #endregion

        #region "GCM Table 이름"

        //Resource Group Table 1~10
        public const string MP_GCM_RES_GRP_1 = "RES_GRP_1";
        public const string MP_GCM_RES_GRP_2 = "RES_GRP_2";
        public const string MP_GCM_RES_GRP_3 = "RES_GRP_3";
        public const string MP_GCM_RES_GRP_4 = "RES_GRP_4";
        public const string MP_GCM_RES_GRP_5 = "RES_GRP_5";
        public const string MP_GCM_RES_GRP_6 = "RES_GRP_6";
        public const string MP_GCM_RES_GRP_7 = "RES_GRP_7";
        public const string MP_GCM_RES_GRP_8 = "RES_GRP_8";
        public const string MP_GCM_RES_GRP_9 = "RES_GRP_9";
        public const string MP_GCM_RES_GRP_10 = "RES_GRP_10";

        public const string MP_RAS_RES_TYPE = "RES_TYPE";
        public const string MP_RAS_AREA_CODE = "AREA";
        public const string MP_RAS_SUBAREA_CODE = "BAY";

        #endregion

        #region "CMF Code 이름"

        public const string MP_CMF_RESOURCE = "CMF_RESOURCE";
        public const string MP_GRP_RESOURCE = "GRP_RESOURCE";
        public const string MP_CMF_TRN_EVENT = "CMF_TRN_EVENT";

        #endregion

        #region "장비 상태 상수"

        public const char MP_RES_UP_FLAG = 'U';
        public const char MP_RES_DOWN_FLAG = 'D';

        #endregion

        #region "외부 파일을 나타내는 상수"
        public const string MP_CAPTION_FILE = ".\\FMBCaption.xml";
        public const string MP_MESSAGE_FILE = ".\\FMBMessage.xml";
        #endregion

    }

}
