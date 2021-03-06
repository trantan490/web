/*******************************************************************************
' SPLSender.cs
'
' Copyright (c) 2007 by Miracom,Inc.
' All rights reserved.
'
' Generated by DevTool XMLGen 1.0
'
' Created at 2008-05-21 14:02:20
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
    public class SPLSender
    {    
        private static string mSPLUrl;
        private static int mSPLTimeOut;

        public static string SPLUrl
        {
            get
            {
                return mSPLUrl;
            }
            set
            {
                mSPLUrl = value;
            }
        }

        public static int SPLTimeOut
        {
            get
            {
                return mSPLTimeOut;
            }
            set
            {
                mSPLTimeOut = value;
            }
        }
        public static bool SPL_View_Lot_List(SPL_View_Lot_List_In_Tag View_Lot_List_In, ref SPL_View_Lot_List_Out_Tag View_Lot_List_Out)
        {
            return SPL_View_Lot_List("SPL_View_Lot_List", View_Lot_List_In, ref View_Lot_List_Out);
        }
        public static bool SPL_View_Lot_List(string FunctionName, SPL_View_Lot_List_In_Tag View_Lot_List_In, ref SPL_View_Lot_List_Out_Tag View_Lot_List_Out)
        {
            try
            {
                if (null == SPLUrl || SPLUrl.Trim().Equals(""))
                        throw new Exception("INVALID_URL");

                if (0 >= SPLTimeOut)
                        throw new Exception("INVALID_TIMEOUT");

                string sReplyMsg = null;
                string sSendMsg = null;
                byte[] aReplyData = null;
                SmartWebService oWebService = new SmartWebService();
                StreamTransformer former = new StreamTransformerImpl();

                oWebService.SetUrl(SPLUrl);
                oWebService.SetTimeOut(SPLTimeOut);

                SPLType.serialize_SPL_View_Lot_List_In_Tag(former, View_Lot_List_In);

                sSendMsg = FwxCmnFunction.PackMessage(former.getBytes());
                oWebService.RequestReply(FunctionName, sSendMsg, ref sReplyMsg);
                aReplyData = FwxCmnFunction.UnPackMessage(sReplyMsg);

                former = new StreamTransformerImpl(aReplyData);
                SPLType.transform_SPL_View_Lot_List_Out_Tag(former, ref View_Lot_List_Out);

    	    	return true;
    	    }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool SPL_View_Lot_List0(SPL_View_Lot_List0_In_Tag View_Lot_List_In, ref SPL_View_Lot_List0_Out_Tag View_Lot_List_Out)
        {
            return SPL_View_Lot_List0("SPL_View_Lot_List0", View_Lot_List_In, ref View_Lot_List_Out);
        }
        public static bool SPL_View_Lot_List0(string FunctionName, SPL_View_Lot_List0_In_Tag View_Lot_List_In, ref SPL_View_Lot_List0_Out_Tag View_Lot_List_Out)
        {
            try
            {
                if (null == SPLUrl || SPLUrl.Trim().Equals(""))
                        throw new Exception("INVALID_URL");

                if (0 >= SPLTimeOut)
                        throw new Exception("INVALID_TIMEOUT");

                string sReplyMsg = null;
                string sSendMsg = null;
                byte[] aReplyData = null;
                SmartWebService oWebService = new SmartWebService();
                StreamTransformer former = new StreamTransformerImpl();

                oWebService.SetUrl(SPLUrl);
                oWebService.SetTimeOut(SPLTimeOut);

                SPLType.serialize_SPL_View_Lot_List0_In_Tag(former, View_Lot_List_In);

                sSendMsg = FwxCmnFunction.PackMessage(former.getBytes());
                oWebService.RequestReply(FunctionName, sSendMsg, ref sReplyMsg);
                aReplyData = FwxCmnFunction.UnPackMessage(sReplyMsg);

                former = new StreamTransformerImpl(aReplyData);
                SPLType.transform_SPL_View_Lot_List0_Out_Tag(former, ref View_Lot_List_Out);

    	    	return true;
    	    }
            catch(Exception ex)
            {
                throw ex;
            }
        }


    }
}

