/*******************************************************************************
' WIPTransfer.vb
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
'******************************************************************************/

using System;
using com.miracom.transceiverx.message.former;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb
{
    public abstract class WIPTransfer
    {
        
        public abstract void WIP_View_Factory_List(WIP_View_Factory_List_In_Tag View_Factory_List_In, ref WIP_View_Factory_List_Out_Tag View_Factory_List_Out);
        public void recv_WIP_View_Factory_List(string sRequest, ref string sReply)
        {
            byte[] outByt = FwxCmnFunction.UnPackMessage(sRequest);
            StreamTransformer former = new StreamTransformerImpl(outByt);
            WIP_View_Factory_List_In_Tag View_Factory_List_In = new WIP_View_Factory_List_In_Tag();
            WIP_View_Factory_List_Out_Tag View_Factory_List_Out = new WIP_View_Factory_List_Out_Tag();

            WIPType.transform_WIP_View_Factory_List_In_Tag(former, ref View_Factory_List_In);
            WIP_View_Factory_List(View_Factory_List_In, ref View_Factory_List_Out); /* Call User Procedure */
            
            former = new StreamTransformerImpl();
            WIPType.serialize_WIP_View_Factory_List_Out_Tag(former, View_Factory_List_Out);

            sReply = FwxCmnFunction.PackMessage(former.getBytes());
        }

    }
}

