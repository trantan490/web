/*******************************************************************************
' WIPSender.cs
'
' Copyright (c) 2007 by Miracom,Inc.
' All rights reserved.
'
' Generated by DevTool XMLGen 1.0
'
' Created at 2008-05-30 15:32:03
'
' Author : Miracom. R&D.
' Description : DevTool Xml Generator Version 1.0
*******************************************************************************/

using System;
using Miracom.SmartWeb.FWX;
using com.miracom.transceiverx;
using com.miracom.transceiverx.message.former;

namespace Miracom.SmartWeb
{
    public class WIPSender
    {    
        private static string mWIPUrl;
        private static int mWIPTimeOut;

        public static string WIPUrl
        {
            get
            {
                return mWIPUrl;
            }
            set
            {
                mWIPUrl = value;
            }
        }

        public static int WIPTimeOut
        {
            get
            {
                return mWIPTimeOut;
            }
            set
            {
                mWIPTimeOut = value;
            }
        }
        public static bool WIP_View_Factory_List(WIP_View_Factory_List_In_Tag View_Factory_List_In, ref WIP_View_Factory_List_Out_Tag View_Factory_List_Out)
        {
            return WIP_View_Factory_List("WIP_View_Factory_List", View_Factory_List_In, ref View_Factory_List_Out);
        }
        public static bool WIP_View_Factory_List(string FunctionName, WIP_View_Factory_List_In_Tag View_Factory_List_In, ref WIP_View_Factory_List_Out_Tag View_Factory_List_Out)
        {
            try
            {
                if (null == WIPUrl || WIPUrl.Trim().Equals(""))
                        throw new Exception("INVALID_URL");

                if (0 >= WIPTimeOut)
                        throw new Exception("INVALID_TIMEOUT");

                string sReplyMsg = null;
                string sSendMsg = null;
                byte[] aReplyData = null;
                SmartWebService oWebService = new SmartWebService();
                StreamTransformer former = new StreamTransformerImpl();

                oWebService.SetUrl(WIPUrl);
                oWebService.SetTimeOut(WIPTimeOut);

                WIPType.serialize_WIP_View_Factory_List_In_Tag(former, View_Factory_List_In);

                sSendMsg = FwxCmnFunction.PackMessage(former.getBytes());
                oWebService.RequestReply(FunctionName, sSendMsg, ref sReplyMsg);
                aReplyData = FwxCmnFunction.UnPackMessage(sReplyMsg);

                former = new StreamTransformerImpl(aReplyData);
                WIPType.transform_WIP_View_Factory_List_Out_Tag(former, ref View_Factory_List_Out);

    	    	return true;
    	    }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}

