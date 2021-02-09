using System;
using System.Collections;
using System.Text;

namespace Miracom.SmartWeb.UI
{
    public class ReturnMessageString
    {
        private bool m_bServerMsgFlag = false;
        private string m_sMsgCode = "";
        private string m_sErrorMsg = "";
        private string m_sDBErrorMsg = "";
        private ArrayList m_arFieldMsg = new ArrayList();

        public bool IsServerMsgFlag
        {
            get
            {
                return m_bServerMsgFlag;
            }
            set
            {
                if (m_bServerMsgFlag.Equals(value) == false)
                {
                    m_bServerMsgFlag = value;
                }
            }
        }

        public string MsgCode
        {
            get
            {
                return m_sMsgCode;
            }
            set
            {
                if (m_sMsgCode.Equals(value) == false)
                {
                    m_sMsgCode = value;
                }
            }
        }

        public string ErrorMsg
        {
            get
            {
                return m_sErrorMsg;
            }
            set
            {
                if (m_sErrorMsg.Equals(value) == false)
                {
                    m_sErrorMsg = value;
                }
            }
        }

        public string DBErrorMsg
        {
            get
            {
                return m_sDBErrorMsg;
            }
            set
            {
                if (m_sDBErrorMsg.Equals(value) == false)
                {
                    m_sDBErrorMsg = value;
                }
            }
        }

        public ArrayList FieldMsg
        {
            get
            {
                return m_arFieldMsg;
            }
        }
    }

}
