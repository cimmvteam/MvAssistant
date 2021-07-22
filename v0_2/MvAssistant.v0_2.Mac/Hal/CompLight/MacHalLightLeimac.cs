using MvAssistant.v0_2.DeviceDrive.LeimacLight;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.v0_2.Mac.Hal.CompLight
{

    [Guid("07C1D790-D3A5-4AB5-8A6E-5EC41045778B")]
    public class MacHalLightLeimac : MacHalComponentBase, IMacHalLight
    {
        #region Const
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_Port = "port";
        public const string DevConnStr_Channel = "channel";
        public const string DevConnStr_Model = "model";
        #endregion

        #region Device Connection String
        string ip;
        int port;
        MvaEnumLeimacModel model;
        int channel;
        string resourceKey { get { return string.Format("net.tcp://{0}:{1}", this.ip, this.port); } }
        #endregion


        public MvaLeimacLightLdd ldd;

        public MacHalLightLeimac()
        {
        }


        public void TurnOn(int value)
        {
            this.ldd.SetValue(this.channel, value);
        }

        public void TurnOff()
        {
            throw new NotImplementedException();
        }

        public int GetValue()
        {
            if (this.ldd == null) throw new MacException("Deivce drive is not initialize.");
            return this.ldd.GetValues()[this.channel - 1];
        }




        #region HAL

        public override int HalConnect()
        {
            this.ip = this.GetDevConnStr(DevConnStr_Ip);
            this.port = this.GetDevConnStrInt(DevConnStr_Port);
            this.model = this.GetDevConnStrEnum<MvaEnumLeimacModel>(DevConnStr_Model);
            this.channel = this.GetDevConnStrInt(DevConnStr_Channel);

            this.ldd = this.HalContext.ResourceGetOrRegister(this.resourceKey, () => new MvaLeimacLightLdd()
            {
                RemoteIp = this.ip,
                RemotePort = this.port,
                Model = this.model,
            });

            this.ldd.ConnectTry();
            return 0;
        }

        public override int HalClose()
        {
            //可能有其它人在使用 Resource, 不在個別 HAL 裡釋放, 由 HalContext 統一釋放
            return 0;
        }

        #endregion





        #region IDisposable




        #endregion







    }
}

