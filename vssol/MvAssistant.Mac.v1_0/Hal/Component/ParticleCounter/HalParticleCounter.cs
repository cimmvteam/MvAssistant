using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.Component.ParticleCounter
{
    [GuidAttribute("EBAFD2BC-B2B7-4000-9AE3-95A58DC5526B")]
    public class HalParticleCounter : HalComponentBase, IHalParticleCounter
    {
        #region for TestScript
        private float particleValue;

        public float ParticleValue
        {
            get { return particleValue; }
            set { particleValue = value; }
        }

        #endregion
        
        public float GetParticleValue()
        {
            return particleValue;
        }

        public void HalZeroCalibration()
        {
            throw new NotImplementedException();
        }

        public int HalConnect()
        {
            throw new NotImplementedException();
        }

        public int HalClose()
        {
            throw new NotImplementedException();
        }

        public bool HalIsConnected()
        {
            throw new NotImplementedException();
        }

        public string ID
        {
            get;
            set;
        }

        public string DeviceConnStr
        {
            get;
            set;
        }
    }
}
