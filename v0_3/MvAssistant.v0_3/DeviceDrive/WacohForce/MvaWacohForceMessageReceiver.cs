using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.WacohForce
{
    public class MvaWacohForceMessageReceiver : Queue<MvaWacohForceVector>
    {
        List<byte> DataBuffer = new List<byte>();



        public void Receive(byte[] buffer, int offset, int length)
        {
            for (int idx = 0; idx < length; idx++)
                this.DataBuffer.Add(buffer[offset + idx]);
        }


        public void AnalysisMessage()
        {
            for (int idx = 0; idx < 99; idx++)
            {
                if (Monitor.TryEnter(this, 5000))
                {
                    try
                    {
                        var vec = AnalysisMessage_Split();
                        if (vec == null) break;
                        this.Enqueue(vec);
                    }
                    finally { Monitor.Exit(this); }
                }
            }
        }

        MvaWacohForceVector AnalysisMessage_Split()
        {
            if (this.DataBuffer.Count < 27) return null;

            var message = Encoding.UTF8.GetString(this.DataBuffer.GetRange(0, 27).ToArray());
            this.DataBuffer.RemoveRange(0, 27);


            var vec = new MvaWacohForceVector();
            vec.fx = int.Parse(message.Substring(1, 4), System.Globalization.NumberStyles.HexNumber);
            vec.fy = int.Parse(message.Substring(5, 4), System.Globalization.NumberStyles.HexNumber);
            vec.fz = int.Parse(message.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);
            vec.mx = int.Parse(message.Substring(13, 4), System.Globalization.NumberStyles.HexNumber);
            vec.my = int.Parse(message.Substring(17, 4), System.Globalization.NumberStyles.HexNumber);
            vec.mz = int.Parse(message.Substring(21, 4), System.Globalization.NumberStyles.HexNumber);

            return vec;

        }




    }
}
