using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.DeviceDrive.FanucRobot_v42_15;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MvAssistant.DeviceDrive.LeimacLight;
using System.Net;

namespace MvAssistant.Mac.v1_0.Hal.CompLight
{

    [Guid("07C1D790-D3A5-4AB5-8A6E-5EC41045778B")]
    public class MacHalLightLeimac : MacHalComponentBase, IMacHalLight
    {
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_Port = "port";
        public const string DevConnStr_Channel = "channel";
        public const string DevConnStr_Model = "model";

        public MvLeimacLightLdd ldd;

        #region Device Connection String

        string ip;
        int port;
        MvEnumLeimacModel model;
        int channel;
        string resourceKey { get { return string.Format("net.tcp://{0}:{1}", this.ip, this.port); } }

        #endregion


        public MacHalLightLeimac()
        {
        }


        public void TurnOn(int value)
        {
            throw new NotImplementedException();
        }

        public void TurnOff()
        {
            throw new NotImplementedException();
        }




        #region HAL

        public override int HalConnect()
        {
            this.ip = this.GetDevSetting(DevConnStr_Ip);
            this.port = this.GetDevSettingInt(DevConnStr_Port);
            this.model = this.GetDevSettingEnum<MvEnumLeimacModel>(DevConnStr_Model);
            this.channel = this.GetDevSettingInt(DevConnStr_Channel);

            this.ldd = this.HalContext.ResourceGetOrDefault<MvLeimacLightLdd>(this.resourceKey);
            if (this.ldd == null)
            {
                this.ldd = new MvLeimacLightLdd();
                this.ldd.RemoteIp = this.ip;
                this.ldd.RemotePort = this.port;
                this.ldd.Model = this.model;
                this.HalContext.ResourceRegister(this.resourceKey, this.ldd);

            }

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

