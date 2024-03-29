﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.Mac.Hal.CompPlc
{
    public interface IMacHalPlcCleanCh : IMacHalPlcBase
    {
        string GasValveBlow(uint BlowTime);

        #region Set Parameter
        void SetParticleCntLimit(uint? L_Limit, uint? M_Limit, uint? S_Limit);
        void SetRobotAboutLimit(double? Limit_L, double? Limit_R);
        void SetRobotUpDownLimit(double? Limit_U, double? Limit_D);
        void SetPressureDiffLimit(uint PressureLimit);
        void SetPressureCtrl(double AirPressure);
        #endregion

        #region Read Parameter
        Tuple<int, int, int> ReadParticleCntLimitSetting();
        Tuple<double, double> ReadRobotAboutLimitSetting();
        Tuple<double, double> ReadRobotUpDownLimitSetting();
        int ReadPressureDiffLimitSetting();
        double ReadPressureCtrlSetting();
        #endregion

        #region Read Component Value
        Tuple<int, int, int> ReadParticleCount();
        Tuple<double, double, double> ReadMaskLevel();
        double ReadRobotPosAbout();
        double ReadRobotPosUpDown();
        int ReadPressureDiff();
        Single ReadBlowPressure();
        double ReadPressure();

        /// <summary>遮斷為True，依序為：右、前、左</summary>
        /// <returns>遮斷為True，依序為：右、前、左</returns>
        Tuple<bool, bool, bool, bool> ReadLightCurtain();
        #endregion
    }
}
