﻿using MvAssistant.Mac.v1_0;
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
        #region Const
        public const string DevConnStr_Ip = "ip";
        public const string DevConnStr_Port = "port";
        public const string DevConnStr_Channel = "channel";
        public const string DevConnStr_Model = "model";
        #endregion

        #region Device Connection String
        string ip;
        int port;
        MvEnumLeimacModel model;
        int channel;
        string resourceKey { get { return string.Format("net.tcp://{0}:{1}", this.ip, this.port); } }
        #endregion


        public MvLeimacLightLdd ldd;

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




        #region HAL

        public override int HalConnect()
        {
            this.ip = this.GetDevConnStr(DevConnStr_Ip);
            this.port = this.GetDevConnStrInt(DevConnStr_Port);
            this.model = this.GetDevConnStrEnum<MvEnumLeimacModel>(DevConnStr_Model);
            this.channel = this.GetDevConnStrInt(DevConnStr_Channel);

            this.ldd = this.HalContext.ResourceGetOrRegister(this.resourceKey, ()=> new MvLeimacLightLdd()
            {
                RemoteIp = this.ip,
                RemotePort = this.port,
                Model = this.model,
            });

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

