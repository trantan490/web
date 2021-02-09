/*******************************************************************************
' MESTransfer.vb
'
' Copyright (c) 2007 by Miracom,Inc.
' All rights reserved.
'
' Generated by DevTool XMLGen 1.0
'
' Created at 2008-05-28 11:16:38
'
' Author : Miracom. R&D.
' Description : DevTool Xml Generator Version 1.0
'******************************************************************************/

using System;
using com.miracom.transceiverx.message.former;
using Miracom.SmartWeb.FWX;

namespace Miracom.SmartWeb
{
    public abstract class MESTransfer
    {
        
        public abstract void WIP_View_Factory_Cmf_Item(WIP_View_Factory_Cmf_Item_In_Tag View_Factory_Cmf_Item_In, ref WIP_View_Factory_Cmf_Item_Out_Tag View_Factory_Cmf_Item_Out);
        public void recv_WIP_View_Factory_Cmf_Item(string sRequest, ref string sReply)
        {
            byte[] outByt = FwxCmnFunction.UnPackMessage(sRequest);
            StreamTransformer former = new StreamTransformerImpl(outByt);
            WIP_View_Factory_Cmf_Item_In_Tag View_Factory_Cmf_Item_In = new WIP_View_Factory_Cmf_Item_In_Tag();
            WIP_View_Factory_Cmf_Item_Out_Tag View_Factory_Cmf_Item_Out = new WIP_View_Factory_Cmf_Item_Out_Tag();

            MESType.transform_WIP_View_Factory_Cmf_Item_In_Tag(former, ref View_Factory_Cmf_Item_In);
            WIP_View_Factory_Cmf_Item(View_Factory_Cmf_Item_In, ref View_Factory_Cmf_Item_Out); /* Call User Procedure */
            
            former = new StreamTransformerImpl();
            MESType.serialize_WIP_View_Factory_Cmf_Item_Out_Tag(former, View_Factory_Cmf_Item_Out);

            sReply = FwxCmnFunction.PackMessage(former.getBytes());
        }

        public abstract void RAS_Resource_Event(RAS_Resource_Event_In_Tag Resource_Event_In, ref RAS_Resource_Event_Out_Tag Resource_Event_Out);
        public void recv_RAS_Resource_Event(string sRequest, ref string sReply)
        {
            byte[] outByt = FwxCmnFunction.UnPackMessage(sRequest);
            StreamTransformer former = new StreamTransformerImpl(outByt);
            RAS_Resource_Event_In_Tag Resource_Event_In = new RAS_Resource_Event_In_Tag();
            RAS_Resource_Event_Out_Tag Resource_Event_Out = new RAS_Resource_Event_Out_Tag();

            MESType.transform_RAS_Resource_Event_In_Tag(former, ref Resource_Event_In);
            RAS_Resource_Event(Resource_Event_In, ref Resource_Event_Out); /* Call User Procedure */
            
            former = new StreamTransformerImpl();
            MESType.serialize_RAS_Resource_Event_Out_Tag(former, Resource_Event_Out);

            sReply = FwxCmnFunction.PackMessage(former.getBytes());
        }

    }
}
