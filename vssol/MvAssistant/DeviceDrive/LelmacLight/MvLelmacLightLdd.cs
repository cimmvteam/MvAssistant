using CToolkit.v1_1.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.LelmacLight
{
    public class MvLelmacLightLdd
    {

        public CtkNonStopTcpClient TcpClient;



        public MvLelmacLightLdd()
        {

        }


        public int ConnectIfNo()
        {
            this.TcpClient = new CtkNonStopTcpClient();


            return 0;
        }





    }
}
