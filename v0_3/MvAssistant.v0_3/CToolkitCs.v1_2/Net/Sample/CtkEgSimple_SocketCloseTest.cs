using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CToolkitCs.v1_2.Net
{
    public class CtkEgSimple_SocketCloseTest
    {
        static AutoResetEvent are = new AutoResetEvent(false);
        public static void Main(string[] args)
        {
            var skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                skt.Bind(new IPEndPoint(IPAddress.Parse("192.168.153.1"), 5001));
                skt.Listen(100);

                var workSkt = skt.Accept();


                Console.WriteLine("Accept!");
            });

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                are.WaitOne();
                skt.Close();
            });



            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                are.Set();
            }
        }
    }
}
