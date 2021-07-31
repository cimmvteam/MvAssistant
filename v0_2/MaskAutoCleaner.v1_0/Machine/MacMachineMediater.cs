using MaskAutoCleaner.v1_0.Machine.Cabinet;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using MvAssistant.v0_2;
using MvAssistant.v0_2.Mac.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine
{
    /// <summary>
    /// Design Pattern - Mediator Pattern
    /// </summary>
    public class MacMachineMediater : IDisposable
    {

        protected MacMachineMgr MachineMgr;//存取此物件需要透過Mediater, 不得開放給其它物件使用

        

        private MacMachineMediater()
        {
            DrawerForBankOutQue= DrawerForBankOutQue.GetInstance();
            CabinetMediater = new CabinetMediater(MacMsCabinet0.GetInstance(), DrawerForBankOutQue);
        }
        public MacMachineMediater(MacMachineMgr machineMgr):this()
        { this.MachineMgr = machineMgr; }
        ~MacMachineMediater() { this.Dispose(false); }

        private IMacHalInspectionCh HalInspectionCh { get { return GetCtrlMachine(EnumMachineID.MID_IC_A_ASB.ToString()).HalAssembly as IMacHalInspectionCh; } }
        private IMacHalCleanCh HalCleanCh { get { return GetCtrlMachine(EnumMachineID.MID_CC_A_ASB.ToString()).HalAssembly as IMacHalCleanCh; } }
        private IMacHalOpenStage HalOpenStage { get { return GetCtrlMachine(EnumMachineID.MID_OS_A_ASB.ToString()).HalAssembly as IMacHalOpenStage; } }
        private IMacHalEqp HalUniversal { get { return GetCtrlMachine(EnumMachineID.MID_UNI_A_ASB.ToString()).HalAssembly as IMacHalEqp; } }



        
        
        private MacMsCabinet0 MsCabinet { get { return MacMsCabinet0.GetInstance();} }
        private DrawerForBankOutQue DrawerForBankOutQue { get; set; }
        public CabinetMediater CabinetMediater { get; set; }
        public MacMachineCtrlBase GetCtrlMachine(string machineId)
        {
            if (!this.MachineMgr.CtrlMachines.ContainsKey(machineId)) return null;
            return this.MachineMgr.CtrlMachines[machineId];
        }


       



        #region Open Stage

        /// <summary> Box Transfer或Mask Transfer手臂入侵Open Stage前確認可否侵入 </summary>
        /// <param name="isBTIntrude">Box Transfer手臂是否入侵</param>
        /// <param name="isMTIntrude">Mask Transfer手臂是否入侵</param>
        /// <returns></returns>
        public Tuple<bool, bool> RobotIntrudeOpenStage(bool? isBTIntrude, bool? isMTIntrude)
        { return HalOpenStage.SetRobotIntrude(isBTIntrude, isMTIntrude); }
        
        /// <summary> 讀取平台上的重量 </summary>
        /// <returns></returns>
        public double ReadOpenStageWeightOnStage()
        { return HalOpenStage.ReadWeightOnStage(); }

        /// <summary> 讀取Open Stage是否有Box </summary>
        /// <returns></returns>
        public bool ReadOpenStageExistBox()
        { return HalOpenStage.ReadBoxExist(); }

        /// <summary> 讀取Open Stage的Particle數量 </summary>
        /// <returns></returns>
        public Tuple<int, int, int> ReadOpenStageParticleCount()
        { return HalOpenStage.ReadParticleCount(); }
        #endregion Open Stage

        #region Inspection Chamber

        /// <summary> Mask Transfer手臂入侵Inspection Chamber前確認可否侵入 </summary>
        /// <param name="isIntrude">Mask Transfer手臂是否入侵</param>
        /// <returns></returns>
        public bool RobotIntrudeInspCh(bool isIntrude)
        { return HalInspectionCh.SetRobotIntrude(isIntrude); }

        /// <summary> 讀取Robot侵入InspCh的左右位置 </summary>
        /// <returns></returns>
        public double ReadInspChAboutSensor()
        { return HalInspectionCh.ReadRobotPosLeftRight(); }

        /// <summary> 讀取Robot侵入InspCh的上下位置 </summary>
        /// <returns></returns>
        public double ReadInspChUpDownSensor()
        { return HalInspectionCh.ReadRobotPosUpDown(); }
        #endregion Inspection Chamber

        #region Clean Chamber

        /// <summary> 讀取Robot侵入CleanCh的左右位置 </summary>
        /// <returns></returns>
        public double ReadCleanChAboutSensor()
        { return HalCleanCh.ReadRobotPosLeftRight(); }

        /// <summary> 讀取Robot侵入CleanCh的上下位置 </summary>
        /// <returns></returns>
        public double ReadCleanChUpDownSensor()
        { return HalCleanCh.ReadRobotPosUpDown(); }

        /// <summary> 讀取光閘，一排一個 各自獨立，遮斷時True，Reset time 500ms </summary>
        /// <returns></returns>
        public Tuple<bool, bool, bool, bool> ReadCleanChLightCurtain()
        { return HalCleanCh.ReadLightCurtain(); }
        #endregion Clean Chamber

        #region Universal
        /// <summary> 設備訊號燈設定 </summary>
        /// <param name="Red"></param>
        /// <param name="Orange"></param>
        /// <param name="Blue"></param>
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        { HalUniversal.SetSignalTower(Red, Orange, Blue); }

        /// <summary> 蜂鳴器，0:停止鳴叫、1~4分別是不同的鳴叫方式 </summary>
        /// <param name="BuzzerType"></param>
        public void SetBuzzer(uint BuzzerType)
        { HalUniversal.SetBuzzer(BuzzerType); }

        /// <summary> A08外罩風扇調整風速，風扇編號、風速控制 </summary>
        /// <param name="FanID"></param>
        /// <param name="WindSpeed"></param>
        /// <returns></returns>
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        { return HalUniversal.CoverFanCtrl(FanID, WindSpeed); }

        /// <summary> 讀取外罩風扇風速 </summary>
        /// <returns></returns>
        public List<int> ReadCoverFanSpeed()
        { return HalUniversal.ReadCoverFanSpeed(); }

        /// <summary> 重置所有PLC Alarm訊息 </summary>
        public void ResetAllAlarm()
        { HalUniversal.ResetAllAlarm(); }

        /// <summary> 當Assembly出錯時，針對部件下緊急停止訊號，Box Transfer、Mask Transfer、Open Stage、Inspection Chamber </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="MT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool MT_EMS, bool OS_EMS, bool IC_EMS)
        { HalUniversal.EMSAlarm(BT_EMS, MT_EMS, OS_EMS, IC_EMS); }

        #region PLC狀態訊號
        /// <summary> 讀取電源狀態，True：Power ON 、 False：Power OFF </summary>
        /// <returns>True：Power ON、False：Power OFF</returns>
        public bool ReadPowerON()
        { return HalUniversal.ReadPowerON(); }

        /// <summary> 讀取設備內部，主控盤旁鑰匙鎖狀態，True：Maintenance 、 False：Auto </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadBCP_Maintenance()
        { return HalUniversal.ReadBCP_Maintenance(); }

        /// <summary> 讀取設備外部，抽屜旁鑰匙鎖狀態，True：Maintenance 、 False：Auto </summary>
        /// <returns>True：Maintenance、False：Auto</returns>
        public bool ReadCB_Maintenance()
        { return HalUniversal.ReadCB_Maintenance(); }

        /// <summary> 讀取主控盤EMO按鈕是否觸發，True：Push 、 False：Release </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        { return HalUniversal.ReadBCP_EMO(); }

        /// <summary> 讀取抽屜EMO按鈕是否觸發，True：Push 、 False：Release </summary>
        /// <returns>True：Push、False：Release</returns>
        public Tuple<bool, bool, bool> ReadCB_EMO()
        { return HalUniversal.ReadCB_EMO(); }

        /// <summary> 讀取Load Port 1 EMO按鈕是否觸發，True：Push 、 False：Release </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP1_EMO()
        { return HalUniversal.ReadLP1_EMO(); }

        /// <summary> 讀取Load Port 2 EMO按鈕是否觸發，True：Push 、 False：Release </summary>
        /// <returns>True：Push、False：Release</returns>
        public bool ReadLP2_EMO()
        { return HalUniversal.ReadLP2_EMO(); }

        /// <summary> 讀取主控盤的門，True：Open 、 False：Close </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadBCP_Door()
        { return HalUniversal.ReadBCP_Door(); }

        /// <summary> 讀取Load Port 1的門，True：Open 、 False：Close </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP1_Door()
        { return HalUniversal.ReadLP1_Door(); }

        /// <summary> 讀取Load Port 2的門，True：Open 、 False：Close </summary>
        /// <returns>True：Open、False：Close</returns>
        public bool ReadLP2_Door()
        { return HalUniversal.ReadLP2_Door(); }

        /// <summary> 讀取主控箱內的偵煙器是否偵測到訊號，True：Alarm 、 False：Normal </summary>
        /// <returns>True：Alarm、False：Normal</returns>
        public bool ReadBCP_Smoke()
        { return HalUniversal.ReadBCP_Smoke(); }
        #endregion PLC狀態訊號

        #region PLC alarm signal
        public string ReadAlarm_General()
        { return HalUniversal.ReadAlarm_General(); }

        public string ReadAlarm_Cabinet()
        { return HalUniversal.ReadAlarm_Cabinet(); }

        public string ReadAlarm_CleanCh()
        { return HalUniversal.ReadAlarm_CleanCh(); }

        public string ReadAlarm_BTRobot()
        { return HalUniversal.ReadAlarm_BTRobot(); }

        public string ReadAlarm_MTRobot()
        { return HalUniversal.ReadAlarm_MTRobot(); }

        public string ReadAlarm_OpenStage()
        { return HalUniversal.ReadAlarm_OpenStage(); }

        public string ReadAlarm_InspCh()
        { return HalUniversal.ReadAlarm_InspCh(); }

        public string ReadAlarm_LoadPort()
        { return HalUniversal.ReadAlarm_LoadPort(); }

        public string ReadAlarm_CoverFan()
        { return HalUniversal.ReadAlarm_CoverFan(); }

        public string ReadAlarm_MTClampInsp()
        { return HalUniversal.ReadAlarm_MTClampInsp(); }

        public string ReadAllAlarmMessage()
        {
            string Result = "";
            Result += ReadAlarm_General();
            Result += ReadAlarm_Cabinet();
            Result += ReadAlarm_CleanCh();
            Result += ReadAlarm_BTRobot();
            Result += ReadAlarm_MTRobot();
            Result += ReadAlarm_OpenStage();
            Result += ReadAlarm_InspCh();
            Result += ReadAlarm_LoadPort();
            Result += ReadAlarm_CoverFan();
            Result += ReadAlarm_MTClampInsp();
            return Result;
        }
        #endregion PLC alarm signal

        #region PLC warning signal
        public string ReadWarning_General()
        { return HalUniversal.ReadWarning_General(); }

        public string ReadWarning_Cabinet()
        { return HalUniversal.ReadWarning_Cabinet(); }

        public string ReadWarning_CleanCh()
        { return HalUniversal.ReadWarning_CleanCh(); }

        public string ReadWarning_BTRobot()
        { return HalUniversal.ReadWarning_BTRobot(); }

        public string ReadWarning_MTRobot()
        { return HalUniversal.ReadWarning_MTRobot(); }

        public string ReadWarning_OpenStage()
        { return HalUniversal.ReadWarning_OpenStage(); }

        public string ReadWarning_InspCh()
        { return HalUniversal.ReadWarning_InspCh(); }

        public string ReadWarning_LoadPort()
        { return HalUniversal.ReadWarning_LoadPort(); }

        public string ReadWarning_CoverFan()
        { return HalUniversal.ReadWarning_CoverFan(); }

        public string ReadWarning_MTClampInsp()
        { return HalUniversal.ReadWarning_MTClampInsp(); }

        public string ReadAllWarningMessage()
        {
            string Result = "";
            Result += ReadWarning_General();
            Result += ReadWarning_Cabinet();
            Result += ReadWarning_CleanCh();
            Result += ReadWarning_BTRobot();
            Result += ReadWarning_MTRobot();
            Result += ReadWarning_OpenStage();
            Result += ReadWarning_InspCh();
            Result += ReadWarning_LoadPort();
            Result += ReadWarning_CoverFan();
            Result += ReadWarning_MTClampInsp();
            return Result;
        }
        #endregion PLC warning signal
        #endregion Universal

        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //


            this.DisposeSelf();

            disposed = true;
        }



        protected virtual void DisposeSelf()
        {

        }


        #endregion


    }

    
}
