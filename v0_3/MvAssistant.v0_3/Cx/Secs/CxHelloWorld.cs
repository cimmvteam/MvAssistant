using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace CodeExpress.v1_1Core.Secs
{
    public class CxHelloWorld
    {


        public static void HsmsConnectorTestPassive()
        {
            using (var hsmsConnector = new CxHsmsConnector())
            {
                hsmsConnector.EhReceiveData += delegate (Object sen, CxHsmsConnectorRcvDataEventArg ea)
                {

                    var myMsg = ea.msg;


                    System.Diagnostics.Debug.WriteLine("S{0}F{1}", myMsg.Header.StreamId, myMsg.Header.FunctionId);
                    System.Diagnostics.Debug.WriteLine("SType= {0}", myMsg.Header.SType);

                    switch (myMsg.Header.SType)
                    {
                        case 1:
                            hsmsConnector.Send(CxHsmsMessage.CtrlMsg_SelectRsp(0));
                            return;
                        case 5:
                            hsmsConnector.Send(CxHsmsMessage.CtrlMsg_LinktestRsp());
                            return;
                    }

                    if (myMsg.Header.StreamId == 1 && myMsg.Header.FunctionId == 3)
                    {
                        var msg = new CxHsmsMessage();
                        msg.Header.StreamId = 1;
                        msg.Header.FunctionId = 4;
                        var list = new CxSecsIINodeList();
                        var signal = new CxSecsIINodeFloat64();
                        signal.Data.Add(1.2);
                        list.Data.Add(signal);
                        msg.RootNode = list;

                        hsmsConnector.Send(msg);
                    }

                };




                //hsmsConnector.ctkConnSocket.isActively = true;
                hsmsConnector.LocalUri = new Uri("net.tcp://192.168.217.1:5000");
                hsmsConnector.RemoteUri = new Uri("net.tcp://192.168.217.129:5000");
                for (int idx = 0; true; idx++)
                {
                    try
                    {
                        hsmsConnector.ConnectTry();
                        hsmsConnector.ReceiveLoop();
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.StackTrace); }
                }

            }
        }

        public static void HsmsConnectorTestActive()
        {
            using (var hsmsConnector = new CxHsmsConnector())
            {
                hsmsConnector.EhReceiveData += delegate (Object sen, CxHsmsConnectorRcvDataEventArg ea)
                {

                    var myMsg = ea.msg;


                    System.Diagnostics.Debug.WriteLine("S{0}F{1}", myMsg.Header.StreamId, myMsg.Header.FunctionId);
                    System.Diagnostics.Debug.WriteLine("SType= {0}", myMsg.Header.SType);

                    switch (myMsg.Header.SType)
                    {
                        case 1:
                            hsmsConnector.Send(CxHsmsMessage.CtrlMsg_SelectRsp(0));
                            return;
                        case 5:
                            hsmsConnector.Send(CxHsmsMessage.CtrlMsg_LinktestRsp());
                            return;
                    }

                    if (myMsg.Header.StreamId == 1 && myMsg.Header.FunctionId == 3)
                    {
                        var msg = new CxHsmsMessage();
                        msg.Header.StreamId = 1;
                        msg.Header.FunctionId = 4;
                        var list = new CxSecsIINodeList();
                        var signal = new CxSecsIINodeFloat64();
                        signal.Data.Add(1.2);
                        list.Data.Add(signal);
                        msg.RootNode = list;

                        hsmsConnector.Send(msg);
                    }

                };




                hsmsConnector.SocketTcp.IsActively = true;
                hsmsConnector.LocalUri = new Uri("net.tcp://192.168.217.1:5000");
                hsmsConnector.RemoteUri = new Uri("net.tcp://192.168.217.129:5000");
                for (int idx = 0; true; idx++)
                {
                    try
                    {
                        hsmsConnector.ConnectTry();
                        hsmsConnector.Send(CxHsmsMessage.CtrlMsg_SelectReq());
                        hsmsConnector.ReceiveLoop();
                    }
                    catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex.StackTrace); }
                }

            }




        }










    }
}
