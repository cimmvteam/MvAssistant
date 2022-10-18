using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CToolkitCs.v1_2.Net
{

    /// <summary>
    /// 範例程式, 不具實用性
    /// 輸入任意鍵進入下一步, 輸入Esc離開
    /// </summary>
    public class CtkEgSimple_Socket
    {
        public const int MinNetworkPackage = 64;
        public const int MaxNetworkPackage = 1518;

        static AutoResetEvent are = new AutoResetEvent(false);

        public static void Main(string[] args)
        {
            
            are.Reset();
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                var activelySkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    activelySkt.Bind(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5001));
                    are.WaitOne();
                    activelySkt.Connect(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5002));
                    are.WaitOne();
                    activelySkt.Send(Encoding.UTF8.GetBytes("hello world"));
                    are.WaitOne();
                }
                finally
                {
                    activelySkt.Disconnect(false);
                    activelySkt.Close();
                }

            });


            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                var passtivelySkt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    passtivelySkt.Bind(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5002));
                    passtivelySkt.Listen(100);

                    while (true)
                    {
                        var handler = passtivelySkt.Accept();
                        byte[] buffer = new byte[1518];
                        var dataSize = handler.Receive(buffer);
                        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, dataSize));
                    }
                }
                finally
                {
                    passtivelySkt.Disconnect(false);
                    passtivelySkt.Close();
                }
            });


            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                are.Set();
            }

        }

    }
}
