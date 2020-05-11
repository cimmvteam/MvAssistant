using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvAssistant.Mac.v1_0.Hal;
using MvAssistant.Mac.v1_0.Hal.Assembly;

namespace MvAssistant.Mac.TestMy.MachineRealHal
{
    [TestClass]
    public class UtHalContext
    {
        [TestMethod]
        public void TestMethod1()
        {


            var halContext = new MacHalContext("GenCfg/Manifest/Manifest.xml.real");
            halContext.Load();


            #region Interface context
            
            //void SetSignalTower(bool Red, bool Orange, bool Blue);

            //void SetBuzzer(uint BuzzerType);

            //string CoverFanCtrl(uint FanID, uint WindSpeed);

            //List<int> ReadCoverFanSpeed();

            //void ResetAll();

            //void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS);

            //#region PLC狀態訊號
            //bool ReadPowerON();

            //bool ReadBCP_Maintenance();

            //bool ReadCB_Maintenance();

            //Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO();

            //Tuple<bool, bool, bool> ReadCB_EMO();

            //bool ReadLP1_EMO();

            //bool ReadLP2_EMO();

            //bool ReadBCP_Door();

            //bool ReadLP1_Door();

            //bool ReadLP2_Door();

            //bool ReadBCP_Smoke();

            //bool ReadLP_Light_Curtain();
            //#endregion

            #endregion


            #region HAL context

            ///// <summary>
            ///// 設備訊號燈設定
            ///// </summary>
            ///// <param name="Red"></param>
            ///// <param name="Orange"></param>
            ///// <param name="Blue"></param>
            //public void SetSignalTower(bool Red, bool Orange, bool Blue)
            //{ Plc.SetSignalTower(Red, Orange, Blue); }

            ///// <summary>
            ///// 蜂鳴器，0:停止鳴叫、1~4分別是不同的鳴叫方式
            ///// </summary>
            ///// <param name="BuzzerType"></param>
            //public void SetBuzzer(uint BuzzerType)
            //{ Plc.SetBuzzer(BuzzerType); }

            ///// <summary>
            ///// A08外罩風扇調整風速，風扇編號、風速控制
            ///// </summary>
            ///// <param name="FanID"></param>
            ///// <param name="WindSpeed"></param>
            ///// <returns></returns>
            //public string CoverFanCtrl(uint FanID, uint WindSpeed)
            //{ return Plc.CoverFanCtrl(FanID, WindSpeed); }

            ///// <summary>
            ///// 讀取外罩風扇風速
            ///// </summary>
            ///// <returns></returns>
            //public List<int> ReadCoverFanSpeed()
            //{ return Plc.ReadCoverFanSpeed(); }

            ///// <summary>
            ///// 重置所有PLC Alarm訊息
            ///// </summary>
            //public void ResetAll()
            //{ Plc.ResetAll(); }

            ///// <summary>
            ///// 當Assembly出錯時，針對部件下緊急停止訊號，Box Transfer、Mask Transfer、Open Stage、Inspection Chamber
            ///// </summary>
            ///// <param name="BT_EMS">Box Transfer是否緊急停止</param>
            ///// <param name="RT_EMS">Mask Transfer是否緊急停止</param>
            ///// <param name="OS_EMS">Open Stage是否緊急停止</param>
            ///// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
            //public void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS)
            //{ Plc.EMSAlarm(BT_EMS, RT_EMS, OS_EMS, IC_EMS); }

            //#region PLC狀態訊號
            //public bool ReadPowerON()
            //{ return Plc.ReadPowerON(); }

            //public bool ReadBCP_Maintenance()
            //{ return Plc.ReadBCP_Maintenance(); }

            //public bool ReadCB_Maintenance()
            //{ return Plc.ReadCB_Maintenance(); }

            //public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
            //{ return Plc.ReadBCP_EMO(); }

            //public Tuple<bool, bool, bool> ReadCB_EMO()
            //{ return Plc.ReadCB_EMO(); }

            //public bool ReadLP1_EMO()
            //{ return Plc.ReadLP1_EMO(); }

            //public bool ReadLP2_EMO()
            //{ return Plc.ReadLP2_EMO(); }

            //public bool ReadBCP_Door()
            //{ return Plc.ReadBCP_Door(); }

            //public bool ReadLP1_Door()
            //{ return Plc.ReadLP1_Door(); }

            //public bool ReadLP2_Door()
            //{ return Plc.ReadLP2_Door(); }

            ///// <summary>
            ///// 讀取煙霧偵測器訊號
            ///// </summary>
            ///// <returns></returns>
            //public bool ReadBCP_Smoke()
            //{ return Plc.ReadBCP_Smoke(); }

            ///// <summary>
            ///// 讀取Load Port光閘是否被遮斷
            ///// </summary>
            ///// <returns></returns>
            //public bool ReadLP_Light_Curtain()
            //{ return Plc.ReadLP_Light_Curtain(); }
            //#endregion

            #endregion

        }
    }
}
