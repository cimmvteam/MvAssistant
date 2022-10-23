using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MvaCToolkitCs.v1_2.Net
{
    public class CtkEgSimple_BeginSocket
    {
        static ManualResetEvent allDone = new ManualResetEvent(false);
        static AutoResetEvent are = new AutoResetEvent(false);

        public static void Main(string[] args)
        {

            var activelySkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            activelySkt.Bind(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5001));

            var passtivelySkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            passtivelySkt.Bind(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5002));
            passtivelySkt.Listen(100);

            try
            {
                passtivelySkt.BeginAccept(new AsyncCallback(AcceptCallback), passtivelySkt);


                activelySkt.Connect(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5002));
                activelySkt.Send(Encoding.UTF8.GetBytes("Hello world!!\n"));



                while (Console.ReadKey().Key != ConsoleKey.Escape)
                {
                    are.Set();
                }
            }
            finally
            {
                activelySkt.Close();
                activelySkt.Dispose();
                passtivelySkt.Close();
                passtivelySkt.Dispose();
            }

        }


        static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            try
            {
                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.
                    state.sb.Append(Encoding.UTF8.GetString(
                        state.buffer, 0, bytesRead));

                    // Check for end-of-file tag. If it is not there, read 
                    // more data.
                    content = state.sb.ToString();
                    if (content.IndexOf("\n") > -1)
                    {
                        // All the data has been read from the 
                        // client. Display it on the console.
                        Console.WriteLine(string.Format("Read {0} bytes from socket", content.Length));
                        // Echo the data back to the client.

                        Send(handler, content);




                    }
                    else
                    {
                        // Not all data received. Get more.
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine(string.Format("Sent {0} bytes to client.", bytesSent));

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        public class StateObject
        {
            // Client  socket.
            public Socket workSocket = null;
            // Size of receive buffer.
            public const int BufferSize = 1024;
            // Receive buffer.
            public byte[] buffer = new byte[BufferSize];
            public int dataLength = 0;
            // Received data string.
            public StringBuilder sb = new StringBuilder();
        }

    }
}
