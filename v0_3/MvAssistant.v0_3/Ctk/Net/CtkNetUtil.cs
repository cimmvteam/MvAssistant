using MvaCToolkitCs.v1_2.Net.SocketTx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace MvaCToolkitCs.v1_2.Net
{
    public class CtkNetUtil
    {

        /// <summary> </summary>
        /// <param name="ipAddress">廣播位址 e.q. 192.168.1.255</param>
        /// <param name="macAddress">對象 MAC</param>
        public static void WakeOnLan(string ipAddress, string macAddress)
        {
            //UDP Port 9
            IPEndPoint pEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), 9);

            //將aa:bb:cc:dd:ee:ff或aa-bb-cc-dd-ee-ff MAC地址轉成byte[]
            var macAddrByteStrAry = macAddress.Split('-', ':');
            var macAddrBytes = new byte[macAddrByteStrAry.Length];
            for (var idx = 0; idx < macAddrByteStrAry.Length; idx++)
            {
                var str = macAddrByteStrAry[idx];
                macAddrBytes[idx] = Convert.ToByte(str, 16);
            }

            //送出UPD封包
            using (UdpClient udpClient = new UdpClient())
            {
                byte[] data = new byte[102];
                //最前方六個0xff
                for (var i = 0; i < 6; i++)
                    data[i] = 0xff;
                //重複16次MAC地址
                for (int j = 1; j <= 16; j++)
                {
                    macAddrBytes.CopyTo(data, j * 6);
                }
                udpClient.Send(data, (int)data.Length, pEndPoint);
                udpClient.Close();

            }
        }


        public static void DisposeSocket(Socket socket)
        {
            if (socket == null) return;
            using (socket)
            {
                socket.Shutdown(SocketShutdown.Both);
                //不想拋出例外的話, 請使用 XxxxxTry 版本
                if (socket.Connected)
                    socket.Disconnect(false);
                socket.Close();
            }
        }
        public static bool DisposeSocketTry(Socket socket)
        {
            if (socket == null) return true;
            try
            {
                DisposeSocket(socket);
                return true;
            }
            catch (SocketException) { return false; }
            catch (ObjectDisposedException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception ex)
            {
                //非預期的Exception, 記錄起來
                CtkLog.Warn(ex);
                return false;
            }
        }
        public static bool DisposeSocketTry(CtkSocket socket)
        {
            if (socket == null) return true;
            try
            {
                socket.DisposeClose();
                return true;
            }
            catch (SocketException) { return false; }
            catch (ObjectDisposedException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception ex)
            {
                //非預期的Exception, 記錄起來
                CtkLog.Warn(ex);
                return false;
            }
        }
        public static void DisposeTcpClient(TcpClient client)
        {
            if (client == null) return;

            //有exception先接起來, 但工作繼續
            Exception myex = null;


            //不想拋出例外的話, 請使用 XxxxxTry 版本
            try { DisposeSocket(client.Client); }
            catch (Exception ex) { myex = ex; }

            try
            {
                var stm = client.GetStream();
                if (stm != null) using (stm) stm.Close();
            }
            catch (Exception ex) { myex = ex; }

            try { using (client) client.Close(); }
            catch (Exception ex) { myex = ex; }

            //最後若有存在Exception就拋出
            if (myex != null) throw myex;

        }
        public static bool DisposeTcpClientTry(TcpClient client)
        {
            try
            {
                DisposeTcpClient(client);
                return true;
            }
            catch (SocketException) { return false; }
            catch (ObjectDisposedException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception ex)
            {
                //非預期的Exception, 記錄起來
                CtkLog.Warn(ex);
                return false;
            }
        }

        public static void DisposeTcpClient(CtkTcpClient client)
        {
            if (client == null) return;
            client.Disconnect();
            client.Dispose();
        }
        public static bool DisposeTcpClientTry(CtkTcpClient client)
        {
            try
            {
                DisposeTcpClient(client);
                return true;
            }
            catch (SocketException) { return false; }
            catch (ObjectDisposedException) { return false; }
            catch (InvalidOperationException) { return false; }
            catch (Exception ex)
            {
                //非預期的Exception, 記錄起來
                CtkLog.Warn(ex);
                return false;
            }
        }



        public static List<IPAddress> GetIP()
        {
            String strHostName = string.Empty;
            // Getting Ip address of local machine...
            // First get the host name of local machine.
            strHostName = Dns.GetHostName();
            Console.WriteLine("Local Machine's Host Name: " + strHostName);
            // Then using host name, get the IP address list..
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            return new List<IPAddress>(addr);
        }

        public static IPAddress GetIpAdr1st()
        {
            string strHostName = Dns.GetHostName();
            var iphostentry = Dns.GetHostEntry(strHostName);
            return iphostentry.AddressList.FirstOrDefault();
        }
        public static IPAddress GetIpAdr1st(AddressFamily addrFamily)
        {
            string strHostName = Dns.GetHostName();
            var iphostentry = Dns.GetHostEntry(strHostName);
            var rtn = iphostentry.AddressList.Where(x => x.AddressFamily == addrFamily);
            return rtn.FirstOrDefault();
        }

        public static IPAddress GetIpAdr1stLikelyOr127(string request_ip = null, string reference_ip = null)
        {
            var ipaddr = GetIpAdrLikely(request_ip, reference_ip);
            if (ipaddr == null)
                ipaddr = GetIpAdr1st();
            if (ipaddr == null)
                ipaddr = IPAddress.Parse("127.0.0.1");

            return ipaddr;
        }

        public static IPAddress GetIpAdr1stLikelyOrLocal(string request_ip = null, string reference_ip = null)
        {
            var ipaddr = GetIpAdrLikely(request_ip, reference_ip);
            if (ipaddr == null)
                ipaddr = GetIpAdr1st();
            if (ipaddr == null)
                ipaddr = IPAddress.Parse("localhost");

            return ipaddr;
        }

        public static IPAddress GetIpAdrLikely(string refence_ip)
        {
            if (string.IsNullOrEmpty(refence_ip)) return null;

            var remoteEndPoint = IPAddress.Parse(refence_ip);
            IPAddress ipaddr = null;
            string strHostName = Dns.GetHostName();
            var iphostentry = Dns.GetHostEntry(strHostName);
            var likelyCount = 0;
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                var localIpBytes = ipaddress.GetAddressBytes();
                var remoteIpBytes = remoteEndPoint.GetAddressBytes();
                int idx = 0;
                for (idx = 0; idx < localIpBytes.Length; idx++)
                    if (localIpBytes[idx] != remoteIpBytes[idx])
                        break;

                if (idx > likelyCount)
                {
                    likelyCount = idx;
                    ipaddr = ipaddress;
                }
            }
            return ipaddr;
        }
        public static IPAddress GetIpAdrLikely(string request_ip, string refence_ip)
        {
            if (string.IsNullOrEmpty(refence_ip) && string.IsNullOrEmpty(request_ip)) return null;

            //如果要求的IP有被設定, 就回傳要求的
            IPAddress requestIpAddr = null;
            IPAddress.TryParse(request_ip, out requestIpAddr);
            if (requestIpAddr != null) return requestIpAddr;


            //否則找出最接近參考IP(remote)
            var targetIpAddr = GetIpAdrLikely(refence_ip);
            if (targetIpAddr != null) return targetIpAddr;


            return null;
        }
        public static List<string> GetMacAddressEnthernet()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            List<string> macList = new List<string>();
            foreach (var nic in nics)
            {
                // 因為電腦中可能有很多的網卡(包含虛擬的網卡)，
                // 我只需要 Ethernet 網卡的 MAC
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    macList.Add(nic.GetPhysicalAddress().ToString());
                }
            }
            return macList;
        }


        public static IPAddress GetSuitableIp(string request_ip, string reference_ip)
        {
            //如果要求的IP有被設定, 就回傳要求的
            IPAddress requestIpAddr = null;
            if (IPAddress.TryParse(request_ip, out requestIpAddr)) return requestIpAddr;


            if (reference_ip == "127.0.0.1")
                return IPAddress.Parse("127.0.0.1");
            if (reference_ip == "localhost")
                return IPAddress.Parse("localhost");


            var ipaddr = GetIpAdrLikely(request_ip, reference_ip);
            if (ipaddr == null)
                ipaddr = GetIpAdr1st();
            if (ipaddr == null)
                ipaddr = IPAddress.Parse("127.0.0.1");//localhost可能被改掉, 所以不適用

            return ipaddr;
        }
        public static IPAddress GetSuitableIp(string request_ip, string reference_ip, AddressFamily addrFamily)
        {
            //如果要求的IP有被設定, 就回傳要求的
            IPAddress requestIpAddr = null;
            if (IPAddress.TryParse(request_ip, out requestIpAddr)) return requestIpAddr;


            if (reference_ip == "127.0.0.1")
                return IPAddress.Parse("127.0.0.1");
            if (reference_ip == "localhost")
                return IPAddress.Parse("localhost");


            var ipaddr = GetIpAdrLikely(request_ip, reference_ip);
            if (ipaddr == null)
                ipaddr = GetIpAdr1st(addrFamily);
            if (ipaddr == null)
                ipaddr = IPAddress.Parse("127.0.0.1");//localhost可能被改掉, 所以不適用

            return ipaddr;
        }

        public static bool IsConnected(TcpClient obj)
        {
            if (obj == null) return false;
            if (obj.Client == null) return false;
            return obj.Connected;
        }


        public static IPAddress ToIPAddress(Uri uri) { return IPAddress.Parse(uri.Host); }
        public static IPEndPoint ToIPEndPoint(Uri uri) { return new IPEndPoint(ToIPAddress(uri), uri.Port); }

        public static Uri ToUri(string ip, int port, string schema = "net.tcp") { return new Uri(string.Format("{0}://{1}:{2}", schema, ip, port)); }
        public static Uri ToUri(IPEndPoint ipep, string schema = "net.tcp") { return new Uri(string.Format("{0}://{1}:{2}", schema, ipep.Address, ipep.Port)); }
    }
}
