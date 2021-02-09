/*******************************************************************************
' h101stub.cs
'
' Copyright (c) 2007 by Miracom,Inc.
' All rights reserved.
'
' Generated by DevTool XMLGen 1.0
'
' Created at 2007-07-05 15:47:49
'
' Author : Miracom. R&D.
' Description : DevTool Xml Generator Version 1.0
*******************************************************************************/

using System;
using System.Net;
using System.Threading;
using System.Collections;

using com.miracom.transceiverx;
using com.miracom.transceiverx.session;
using com.miracom.transceiverx.message;

namespace Miracom.SmartWeb
{

    public class h101stub : MessageConsumer, SessionEventListener
    {
        public const string XGEN_VERSION            = "4.0";
        public const string XGEN_TAG_VERSION        = "VERSION";
        public const string XGEN_TAG_MODULE         = "MODULE";
        public const string XGEN_TAG_INTERFACE      = "INTERFACE";
        public const string XGEN_TAG_OPERATION      = "OPERATION";
        public const string XGEN_TAG_HOSTNAME       = "HOSTNAME";
        public const string XGEN_TAG_HOSTADDR       = "HOSTADDR";
        public const string XGEN_TAG_RESULT_CODE    = "RESULT_CODE";
        public const string XGEN_TAG_RESULT_MSG     = "RESULT_MSG";
        public const int  XGEN_SUCCESS              = 0x0000;
        public const int  XGEN_DEFAULT_TTL          = 0x7530;

        private struct RequestPool
        {
            public const int DEFAULT_MAX_POOL_INDEX = 0x63;

            public const int STS_WAITING    = 0x00;
            public const int STS_TIMEOUT    = 0x01;
            public const int STS_COMPLETE   = 0x02;
            public const int STS_ERROR      = 0x03;
            public const int STS_NOTFOUND_REPLIER   = 0x04;

            public static int index = 0;
            public static bool isWorking = true;
            public static Message[] reply = new MessageImpl[DEFAULT_MAX_POOL_INDEX];
            public static int[] status = new int[DEFAULT_MAX_POOL_INDEX];
            public static int retryCount = 60; //Default Retry Count
            public static int retryTerm = 1000; //Default Retry Term
        }

        private struct HostInfo
        {
            public static string name = GetComputerName();
            public static string addr = GetIPAddresss();
            public static string peerName = null;
            public static string peerAddr = null;

            public static string GetComputerName()
            {
                string name = Dns.GetHostName();
                return (name.Length == System.Text.Encoding.Default.GetBytes(name).Length) ? name : Dns.GetHostEntry(name).AddressList[0].ToString();
            }
            public static string GetIPAddresss()
            {
                return Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
            }
        }

        #region Singleton
        private static h101stub h101stub_instance = null;
        private static string status_message = "";

        public static h101stub Instance
        {
            get
            {
                if (null == h101stub_instance)
                     h101stub_instance = new h101stub();
                return h101stub_instance;
            }
        }

        public static string StatusMessage
        {
            get
            {
                return status_message;
            }
            set
            {
                status_message = value;
            }
        }
        #endregion

        private Transceiver mioiProcess = null;
        private Session mioiSession = null;
        private Hashtable dispatchers = new Hashtable();

        #region Public Interface
        public void registerDispatcher(string module, object tuner) 
        {
            this.dispatchers.Add(module, tuner);
        }
        public void unregisterDispatcher(string module) 
        {
            this.dispatchers.Remove(module);
        }

        public bool init(string sessionId)
        {
            return init(sessionId, 
                        com.miracom.transceiverx.session.Session_Fields.SESSION_INNER_STATION_MODE |
                        com.miracom.transceiverx.session.Session_Fields.SESSION_PUSH_DELIVERY_MODE, 
                        "localhost:10101",
                        0);
        }
        
        public bool init(string sessionId, int sessionMode, string connectString, int monitoringPort) 
        {
            try 
            {
                Random rnd;
                StatusMessage = "";

                // Add by J.S.
                // Admin�� Client���� ������ ���Ͽ�
                rnd = new Random();
                sessionId = sessionId + "_" + HostInfo.addr + "_" + rnd.Next(9999).ToString("0000");

                if(null == mioiProcess)
                    mioiProcess = new Transceiver(sessionId, monitoringPort);
                if(null == mioiSession)
                {
                    mioiSession = Transceiver.createSession(sessionId, sessionMode);
                    mioiSession.connect(connectString);
                    mioiSession.addMessageConsumer(this);
                    mioiSession.setAutoRecovery(true);
                }

                return true;
            }
            catch(Exception e) 
            {
                StatusMessage = e.Message;
                return false;
            }
        }

        public void term()
        {
            mioiSession.removeMessageConsumer(this);
            mioiSession.destroy();
            mioiProcess.term();

            mioiProcess = null;
            mioiSession = null;
            dispatchers = null;
            status_message = null;
            h101stub_instance = null;
        }

        public bool tune(string channel, bool isMulticast, bool isGuaranteed)
        {
            try 
            {
                StatusMessage = "";

                if(isMulticast)
                {
                    if(isGuaranteed)
                        mioiSession.tuneGuaranteedMulticast(channel);
                    else 
                        mioiSession.tuneMulticast(channel);
                }
                else
                {
                    if(isGuaranteed)
                        mioiSession.tuneGuaranteedUnicast(channel);
                    else 
                        mioiSession.tuneUnicast(channel);
                }

                return true;
            }
            catch(Exception e)
            {
                StatusMessage = e.Message;
                return false;
            }
        }

        public bool untune(string channel, bool isMulticast, bool isGuaranteed)
        {
            try 
            {
                StatusMessage = "";

                if(isMulticast)
                {
                    if(isGuaranteed)
                        mioiSession.untuneGuaranteedMulticast(channel);
                    else 
                        mioiSession.untuneMulticast(channel);
                }
                else
                {
                    if(isGuaranteed)
                        mioiSession.untuneGuaranteedUnicast(channel);
                    else 
                        mioiSession.untuneUnicast(channel);
                }

                return true;
            }
            catch(Exception e)
            {
                StatusMessage = e.Message;
                return false;
            }
        }

        public Message createMessage()
        {
            try 
            {
                StatusMessage = "";

                return mioiSession.createMessage();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void sendMessage(Message msg, ref Message rep, string channel, int ttl, short deliveryType)
        {
            try 
            {
                StatusMessage = "";

                msg.putProperty(XGEN_TAG_HOSTNAME, HostInfo.name);
                msg.putProperty(XGEN_TAG_HOSTADDR, HostInfo.addr);
                msg.putDeliveryMode(deliveryType);
                msg.putChannel(channel);
                msg.putTTL(0 < ttl ? ttl: XGEN_DEFAULT_TTL);

                casterPrologue(msg);

                if(DeliveryType.UNICAST == deliveryType)
                    mioiSession.sendUnicast(msg);
                else if(DeliveryType.MULTICAST == deliveryType)
                    mioiSession.sendMulticast(msg);
                else if(DeliveryType.GUARANTEED_UNICAST == deliveryType)
                    mioiSession.sendGuaranteedUnicast(channel, msg);
                else if(DeliveryType.GUARANTEED_MULTICAST == deliveryType)
                    mioiSession.sendGuaranteedMulticast(channel, msg);
                else if(DeliveryType.REQUEST == deliveryType)
                    requestReplyAsync(msg, ref rep, 0);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                casterEpilogue(msg, rep);
            }
        }
        #endregion

        private void requestReplyAsync(Message req, ref Message rep, int retryCount)
        {
            try 
            {
                AsyncDispatchThread asyncDispatch;
                Thread asyncThread;

                StatusMessage = "";

                if(RequestPool.DEFAULT_MAX_POOL_INDEX < RequestPool.index)
                    throw new TrxException("Request Reply Reentered");

                RequestPool.status[RequestPool.index] = RequestPool.STS_WAITING;
                RequestPool.reply[RequestPool.index] = null;

                req.putHint(RequestPool.index);

                asyncDispatch = new AsyncDispatchThread(mioiSession, req);
                asyncThread = new Thread(new ThreadStart(asyncDispatch.requestReplyAsyncThread));
                asyncThread.Start();

                RequestPool.index++;

                while(RequestPool.STS_WAITING == RequestPool.status[RequestPool.index -1])
                {

                    Thread.Sleep(10);
                    if(false == RequestPool.isWorking)
                    {
                        RequestPool.reply[RequestPool.index -1] = null;
                        throw new TrxException("Request Reply Timeout");
                    }
                }

                asyncDispatch = null;
                asyncThread = null;

                RequestPool.index--;

                switch (RequestPool.status[RequestPool.index])
                {
                    case RequestPool.STS_COMPLETE:
                        rep = RequestPool.reply[RequestPool.index];
                        RequestPool.reply[RequestPool.index] = null;
                        break;
                    case RequestPool.STS_TIMEOUT:
                        throw new TrxException("Request Reply Timeout");
                    case RequestPool.STS_ERROR:
                        RequestPool.reply[RequestPool.index] = null;
                        throw new TrxException("Request Reply Error");
                    case RequestPool.STS_NOTFOUND_REPLIER:
                        if(retryCount >= RequestPool.retryCount)
                            throw new TrxException("Notfound Replier");

                        Thread.Sleep(RequestPool.retryTerm);
                        requestReplyAsync(req, ref rep, ++retryCount);
                        break;
                }

                HostInfo.peerName = (string)rep.getProperty(XGEN_TAG_HOSTNAME);
                HostInfo.peerAddr = (string)rep.getProperty(XGEN_TAG_HOSTADDR);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void sendReply(Session issuer, Message req, Message rep, string errCode, string errMessage)
        {
            rep.putProperty(XGEN_TAG_HOSTNAME, HostInfo.name);
            rep.putProperty(XGEN_TAG_HOSTADDR, HostInfo.addr);
            rep.putProperty(XGEN_TAG_RESULT_CODE, errCode);
            rep.putProperty(XGEN_TAG_RESULT_MSG, errMessage);
            issuer.sendReply(req, rep);
        }

        #region Interface Of MessageConsumer
        public void onUnicast(Session issuer, Message msg) 
        {
            h101dispatcher tuner = null;
            StatusMessage = "";

            string version = (string) msg.getProperty(XGEN_TAG_VERSION);
            if (null == version || 0 == version.Trim().CompareTo("") || 0 != version.CompareTo(XGEN_VERSION))
                return;

            string module = (string) msg.getProperty(XGEN_TAG_MODULE);
            if (null == module || 0 == module.Trim().CompareTo(""))
                module = (string) msg.getProperty(XGEN_TAG_INTERFACE);
            if (null == module || 0 == module.Trim().CompareTo("") || null == (tuner = (h101dispatcher) this.dispatchers[module])) 
                return;

            HostInfo.peerName = (string) msg.getProperty(XGEN_TAG_HOSTNAME);
            HostInfo.peerAddr = (string) msg.getProperty(XGEN_TAG_HOSTADDR);

            tunerPrologue(msg);
            tunerEpilogue(msg, null, tuner.dispatch(issuer, msg));
        }
        public void onMulticast(Session issuer, Message msg)
        {
            onUnicast(issuer, msg);
        }
        public void onGUnicast(Session issuer, Message msg)
        {
            onUnicast(issuer, msg);
        }
        public void onGMulticast(Session issuer, Message msg)
        {
            onUnicast(issuer, msg);
        }
        public void onRequest(Session issuer, Message msg)
        {
            h101dispatcher tuner = null;
            StatusMessage = "";

            string version = (string) msg.getProperty(XGEN_TAG_VERSION);
            if (null == version || 0 == version.Trim().CompareTo("") || 0 != version.CompareTo(XGEN_VERSION))
            {
                sendReply(issuer, msg, msg.createReply(), "-21" ,"Unexpected Version!");// XGEN_ERR_UNEXPECTED_VERSION -0x15
                return;
            }

            string module = (string) msg.getProperty(XGEN_TAG_MODULE);
            if (null == module || 0 == module.Trim().CompareTo(""))
                module = (string) msg.getProperty(XGEN_TAG_INTERFACE);
            if (null == module || 0 == module.Trim().CompareTo("") || null == (tuner = (h101dispatcher) this.dispatchers[module])) 
            {
                sendReply(issuer, msg, msg.createReply(), "-22" ,"Unexpected Module!");// XGEN_ERR_UNEXPECTED_MODULE -0x16
                return;
            }

            HostInfo.peerName = (string) msg.getProperty(XGEN_TAG_HOSTNAME);
            HostInfo.peerAddr = (string) msg.getProperty(XGEN_TAG_HOSTADDR);

            tunerPrologue(msg);
            tunerEpilogue(msg, null, tuner.dispatch(issuer, msg));
        }
        public void onReply(Session issuer, Message req, Message rep, object hint)
        {
            if(null == rep)
            {
                switch (StatusMessage)
                {
                    case "Notfound Replier":
                        RequestPool.status[(int)hint] = RequestPool.STS_NOTFOUND_REPLIER;
                        break;
                    case "Request Reply Timeout":
                        RequestPool.status[(int)hint] = RequestPool.STS_TIMEOUT;
                        break;
                    case "Request Reply Error":
                        RequestPool.status[(int)hint] = RequestPool.STS_ERROR;
                        break;
                }
                
                return;
            }

            RequestPool.reply[(int)hint] = rep;
            RequestPool.status[(int)hint] = RequestPool.STS_COMPLETE;
        }
        public void onTimeout(Session issuer, Message req) 
        {
            RequestPool.status[(int)req.getHint()] = RequestPool.STS_TIMEOUT;
        }
        #endregion

        #region Interface Of SessionEventListener
        public void onConnect(Session issuer)
        {
        }
        public void onDisconnect(Session issuer)
        {
        }
        #endregion

        #region Prologue/Epilogue
        protected void casterPrologue(Message msg)
        {
            ////Add by CM Koo. 2005.08.25
            ////Site ���� �޽��� ������ ���� ó���� ����
            //MPIF.gInit.CustCasterPrologue(msg);
            //MPIF.gInit.StartTimerProgress();
            //MPGV.giIdleTime = 0;
        }
        protected void casterEpilogue(Message msg, Message rep)
        {
            //MPIF.gInit.StopTimerProgress();

            //MPIF.gInit.IsConnectedH101 = true;
            //if (StatusMessage == "Notfound Replier")
            //{
            //    MPIF.gInit.IsConnectedH101 = false;
            //}

            ////Add by CM Koo. 2005.08.25
            ////Site ���� �޽��� ���� �Ŀ� ó���� ����
            //MPIF.gInit.CustCasterEpilogue(msg, rep);
        }
        protected void tunerPrologue(Message msg)
        {
            //MPIF.gInit.CustTunerPrologue(msg);
        }
        protected void tunerEpilogue(Message msg, Message rep, Exception status)
        {
            //MPIF.gInit.CustTunerEpilogue(msg, rep, status);
        }
        #endregion
    }


    public interface h101dispatcher 
    {
        Exception dispatch(Session issuer, Message msg);
    }

    public class h101type
    {
        protected static string deleteNull(string str)
        {
            if (null == str)
                return "";
            else
                return str.TrimEnd();
        }

        protected static string padding(string str, int size)
        {
            if (null != str)
            {
                while (str.Length < size)
                {
                    str += " ";
                }
            }
            return str;
        }
        protected static string withoutSpace(string str)
        {
            if (null != str)
            {
                str.TrimEnd();
            }
            return str;
        }
    }

    public class AsyncDispatchThread
    {
        private Session session;
        private Message request;

        public AsyncDispatchThread(Session session, Message request)
        {
            this.session = session;
            this.request = request;
        }

        ~AsyncDispatchThread()
        {
            session = null;
            request = null;
        }

        public void requestReplyAsyncThread()
        {
            Message reply;

            try
            {
                reply = session.sendRequest(request);
            }
            catch (Exception e)
            {
                reply = null;

                switch (e.Message)
                {
                    case SessionException.CHANNEL_NOTFOUND_TUNER:
                        h101stub.StatusMessage = "Notfound Replier";
                        break;
                    case SessionException.TIMEOUT:
                        h101stub.StatusMessage = "Request Reply Timeout";
                        break;
                    default:
                        h101stub.StatusMessage = "Request Reply Error";
                        break;
                }
            }

            h101stub.Instance.onReply(session, request, reply, request.getHint());
        }
    }

}
