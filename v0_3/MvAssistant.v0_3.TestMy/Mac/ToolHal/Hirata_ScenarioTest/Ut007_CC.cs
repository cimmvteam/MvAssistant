﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.v0_3.Mac.Hal;
using MvAssistant.v0_3.Mac.Hal.Assembly;
using MvAssistant.v0_3.Mac.Manifest;

namespace MvAssistant.v0_3.Mac.TestMy.ToolHal.Hirata_ScenarioTest
{
    [TestClass]
    public class Ut007_CC
    {
        [TestMethod]
        public void TestMethod1()//OK
        {
            try
            {
                using (var halContext = new MacHalContext("UserData/Manifest/Manifest.xml.real"))
                {
                    halContext.MvaCfBootup();
                    halContext.MvaCfLoad();

                    var unv = halContext.HalDevices[EnumMacDeviceId.eqp_assembly.ToString()] as MacHalEqp;
                    var cc = halContext.HalDevices[EnumMacDeviceId.clean_assembly.ToString()] as MacHalCleanCh;
                    unv.HalConnect();//需要先將MacHalUniversal建立連線，各Assembly的Hal建立連線時，才能讓PLC的連線成功
                    cc.HalConnect();

                    //1. 氣壓20psi,  噴3秒
                    cc.SetGasValvePressurVar(20);
                    cc.SetGasValveTime(30);

                    //2. 氣壓20psi,  噴5秒
                    cc.SetGasValvePressurVar(20);
                    cc.SetGasValveTime(50);

                    //3. 氣壓20psi,  噴10秒
                    cc.SetGasValvePressurVar(20);
                    cc.SetGasValveTime(100);

                    //4. 氣壓50psi,  噴3秒
                    cc.SetGasValvePressurVar(50);
                    cc.SetGasValveTime(30);

                    //5. 氣壓50psi,  噴5秒
                    cc.SetGasValvePressurVar(50);
                    cc.SetGasValveTime(50);

                    //6. 氣壓50psi,  噴10秒
                    cc.SetGasValvePressurVar(50);
                    cc.SetGasValveTime(100);
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
