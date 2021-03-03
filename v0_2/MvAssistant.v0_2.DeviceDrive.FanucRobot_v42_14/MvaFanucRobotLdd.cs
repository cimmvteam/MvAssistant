using MvAssistant.v0_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.v0_2.DeviceDrive.FanucRobot_v42_14
{
    public class MvFanucRobotLdd : IDisposable
    {


        #region FANUC Internal Variable
        private FRRJIf.DataAlarm mobjAlarm;
        private FRRJIf.DataAlarm mobjAlarmCurrent;
        private FRRJIf.Core mobjCore;
        private FRRJIf.DataCurPos mobjCurPos;
        private FRRJIf.DataCurPos mobjCurPos2;
        private FRRJIf.DataCurPos mobjCurPosUF;
        private FRRJIf.DataTable mobjDataTable;
        private FRRJIf.DataTable mobjDataTable2;
        private FRRJIf.DataNumReg mobjNumReg;
        private FRRJIf.DataNumReg mobjNumReg2;
        private FRRJIf.DataNumReg mobjNumReg3;
        private FRRJIf.DataPosReg mobjPosReg;
        private FRRJIf.DataPosReg mobjPosReg2;
        private FRRJIf.DataPosRegXyzwpr mobjPosRegXyzwpr;
        private FRRJIf.DataString mobjStrReg;
        private FRRJIf.DataString mobjStrRegComment;
        private FRRJIf.DataSysVar mobjSysVarInt;
        private FRRJIf.DataSysVar mobjSysVarInt2;
        private FRRJIf.DataSysVar[] mobjSysVarIntArray;
        private FRRJIf.DataSysVarPos mobjSysVarPos;
        private FRRJIf.DataSysVar mobjSysVarReal;
        private FRRJIf.DataSysVar mobjSysVarReal2;
        private FRRJIf.DataSysVar mobjSysVarString;
        private FRRJIf.DataTask mobjTask;
        private FRRJIf.DataTask mobjTaskIgnoreKarel;
        private FRRJIf.DataTask mobjTaskIgnoreMacro;
        private FRRJIf.DataTask mobjTaskIgnoreMacroKarel;
        private FRRJIf.DataSysVar mobjVarString;
        #endregion FANUC Internal Variable

        public bool IsInitialFanucAPI = true;
        public bool isUnderSystemRecoverAuto = false;
        public MvaFanucRobotInfo m_currRobotInfo = new MvaFanucRobotInfo();
        public string RobotIp;
        private bool HasRobotFaultStatus = false;
        private object lockCurRobotInfo = new object();
        ManualResetEvent mreInitialFanucAPI = new ManualResetEvent(false);
        ~MvFanucRobotLdd() { this.Dispose(false); }

        public MvaFanucRobotInfo CurRobotInfo
        {
            get
            {
                lock (lockCurRobotInfo)
                {
                    return m_currRobotInfo;
                }
            }
        }

        public FRRJIf.DataTable MObjDataTable { get { return mobjDataTable; } }
        public FRRJIf.DataNumReg MObjNumReg { get { return mobjNumReg; } }


        public bool AlarmReset()
        {
            Array UIAlways_ON = new short[3];
            Array UI5_FaultReset_NEGEDGE = new short[1];
            Array UO6_Fault = new short[1]; //On means Fault occurred
            bool IsRWSuccess;
            bool IsResetSuccess;

            UIAlways_ON.SetValue((short)1, 0);
            UIAlways_ON.SetValue((short)1, 1);
            UIAlways_ON.SetValue((short)1, 2);
            IsRWSuccess = mobjCore.WriteSDO(1, ref UIAlways_ON, 3);//UI 1 2 3 8 must ON
            IsRWSuccess = mobjCore.WriteSDO(8, ref UIAlways_ON, 1);
            UI5_FaultReset_NEGEDGE.SetValue((short)1, 0);                     //      __________
            IsRWSuccess = mobjCore.WriteSDO(5, ref UI5_FaultReset_NEGEDGE, 1);//UI[5]:          Negtiveedge trigger(HIGH First)
            UI5_FaultReset_NEGEDGE.SetValue((short)0, 0);
            Thread.Sleep(500);                                                //      ____
            IsRWSuccess = mobjCore.WriteSDO(5, ref UI5_FaultReset_NEGEDGE, 1);//UI[5]:    |_____Negtiveedge trigger(Trigger!)
            mobjDataTable.Refresh();

            IsRWSuccess = mobjCore.ReadUO(6, UO6_Fault, 1);
            IsResetSuccess = UO6_Fault.GetValue(0).ToString() == "0";

            return IsResetSuccess;

        }


        public MvaFanucRobotInfo GetCurrRobotInfo()
        {
            lock (lockCurRobotInfo)
            {
                //short ValidC = 0, ValidJ = 0;	// 移除未使用的變數。by YMWANGN, 2016/11/17。
                //Alarm TEST
                var msg = "";
                var alarmInfo = new MvRobotAlarmInfo();
                HasRobotFault(ref msg, ref alarmInfo);


                var robotInfo = new MvaFanucRobotInfo();
                mobjCurPosUF.GetValue(ref robotInfo.posArray, ref robotInfo.configArray, ref robotInfo.jointArray,
                                     ref robotInfo.userFrame, ref robotInfo.userTool, ref robotInfo.validC, ref robotInfo.validJ);
                robotInfo.robotTime = DateTime.Now;

                robotInfo.isReachTarget = this.MoveIsComplete();

                return robotInfo;
            }
        }

        public MvRobotAlarmInfo GetRobotAlarm()
        {
            MvRobotAlarmInfo alminfo = new MvRobotAlarmInfo();
            mobjDataTable.Refresh();
            mobjAlarm.GetValue(
                1,
            ref alminfo.AlarmID,
            ref alminfo.AlarmNumber,
            ref alminfo.CauseAlarmID,
            ref alminfo.CauseAlarmNumber,
            ref alminfo.Severity,
            ref alminfo.Year,
            ref alminfo.Month,
            ref alminfo.Day,
            ref alminfo.Hour,
            ref alminfo.Minute,
            ref alminfo.Second,
            ref alminfo.AlarmMessage,
            ref alminfo.CauseAlarmMessage,
            ref alminfo.SeverityMessage
            );
            return alminfo;
        }

        public bool HasRobotFault(ref string message, ref MvRobotAlarmInfo alarmInfo)
        {
            //************IMPORTANT*************************************************//
            //UO[1~20] address has been mapping to DI[1~20] address at addr.22
            //AND using BGLogic assign DI[1]~DI[20] to R[21]~R[40] respectivly
            //If you wanna read UO[1], plz read R[21] and so on 
            //**********************************************************************

            object UO6Value = (byte)0;
            bool IsReadSuccess = false;

            //1:有err   0:normal
            mobjDataTable.Refresh();
            IsReadSuccess = mobjNumReg.GetValue(26, ref UO6Value);
            if (!IsReadSuccess)
                throw new Exception("Read Fail");
            if ((int)UO6Value == 1)
            {
                alarmInfo = GetRobotAlarm();
                message = "Robot Err Occur !";
                HasRobotFaultStatus = true;
                //if (RobotFaultStatus != almInfo.Result) { this.Equipment.IO_Switch("LaserIO", DeviceSwitch.OFF); }
            }
            else
            {
                message = "Success";
                HasRobotFaultStatus = false;

                //if (RobotFaultStatus != almInfo.Result) { this.Equipment.IO_Switch("LaserIO", DeviceSwitch.ON); }
            }

            return HasRobotFaultStatus;
        }

        public bool HasRobotFault()
        {
            //************IMPORTANT*************************************************//
            //UO[1~20] address has been mapping to DI[1~20] address at addr.22
            //AND using BGLogic assign DI[1]~DI[20] to R[21]~R[40] respectivly
            //If you wanna read UO[1], plz read R[21] and so on 
            //**********************************************************************

            object UO6Value = (byte)0;
            bool IsReadSuccess = false;

            //1:有err   0:normal
            mobjDataTable.Refresh();
            IsReadSuccess = mobjNumReg.GetValue(26, ref UO6Value);
            if (!IsReadSuccess)
                throw new Exception("Read Fail");
            if ((int)UO6Value == 1) { return true; }
            else { return false; }
        }

        public void MoveCompeleteReply()
        {
            this.WriteRegValue(5, 0);
        }

        public bool MoveIsComplete()
        {
            var reg5 = this.GetRegValue(5);
            return reg5 == 51;
        }

        /// <summary>
        /// 輸入Robot設定PNS name, 讀取並執行PNS
        /// </summary>
        /// <param name="PNSname"></param>
        public bool ExecutePNS(string PNSname)
        {
            Array UI = new short[18];
            Array UOInfo = new short[18];
            MvRobotUIOParameter UIO = new MvRobotUIOParameter();
            String PNScode;

            if (PNSname.Length == 7 && PNSname.Substring(0, 3) == "PNS")
            {
                PNScode = Convert.ToString(Convert.ToInt32(PNSname.Substring(5)), 2);
                PNScode = PNScode.PadLeft(8, '0');
            }
            else
            {
                throw new System.ArgumentException("PNS Name is NOT match Rule; PNS name:" + PNSname);
            }

            //Write Default UI Setting 
            UI.SetValue(UIO.UI1_IMSTP, 0);
            UI.SetValue(UIO.UI2_HOLD, 1);
            UI.SetValue(UIO.UI3_SFSPD, 2);
            UI.SetValue(UIO.UI4_CycleStop, 3);
            UI.SetValue(UIO.UI5_FaultReset_NEGEDGE, 4);
            UI.SetValue(UIO.UI6_Start_NEGEDGE_NoUsed, 5);
            UI.SetValue(UIO.UI7_Home_NoUSed, 6);
            UI.SetValue(UIO.UI8_ENABLE, 7);
            // Select PNS Code
            for (int idx = 0; idx < PNScode.Length; idx++)
                UI.SetValue(Convert.ToInt16(PNScode.Substring(7 - idx, 1)), 8 + idx);

            #region Program Start UI Request
            //Program Start UI timing Request  
            //1. Trobe = 1 keep 30ms -> PNS讀取(<130ms)
            //2. ProdStart = 1; trobe =1 and ProdStart keep > 100ms
            //3. ProdStart = 0; trobe =1 and ProdStart = 0 , PROGRUN start in 35 ms
            //4. trobe = 0; Finish All back to 0

            Array UI17_PNStrobe_ProgTiming = new short[4] { 1, 1, 1, 0 };
            Array UI18_PNSstart_ProgTiming = new short[4] { 0, 1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                UI.SetValue(UI17_PNStrobe_ProgTiming.GetValue(i), 16); // PNStrobe
                UI.SetValue(UI18_PNSstart_ProgTiming.GetValue(i), 17); // ProdStart

                mobjCore.WriteSDO(1, ref UI, 18);
                Thread.Sleep(200);
            }
            #endregion

            // Excution Result alarm message
            return PrgRunningCheck();
        }

        public bool PrgRunningCheck()
        {
            Array UO3_PrgRun = new short[1];
            mobjCore.ReadUO(3, ref UO3_PrgRun, 1);

            if ((short)UO3_PrgRun.GetValue(0) == 0)
            { return false; }
            else
            { return true; }
        }

        /// <summary>
        /// Stop executing program
        /// </summary>
        /// <returns></returns>
        public int StopProgram()
        {
            Array UI4_CycleStop = new short[1];

            UI4_CycleStop.SetValue((short)1, 0);
            mobjCore.WriteSDO(4, ref UI4_CycleStop, 1);
            UI4_CycleStop.SetValue((short)0, 0);
            mobjCore.WriteSDO(4, ref UI4_CycleStop, 1);

            return 0;
        }
        #region Device Connection

        public int Close()
        {
            // 若 mobjCore 不為空
            if (mobjCore != null)
            {
                // 將 mobjCore 斷開連線
                try { mobjCore.Disconnect(); }
                catch (Exception ex) { MvaLog.WarnNs(this, ex); }
                mobjCore = null;
            }
            return 0;
        }
        public int ConnectIfNo()
        {
            if (!this.IsConnected())
            {
                if (this.ReConnect() != 0)
                {
                    this.Close();
                    return -1;
                }
            }
            return 0;
        }
        public bool IsConnected()
        {
            if (mobjCore == null) return false;
            //--失敗為 0, 成功為 1.
            return mobjCore.get_ConnectState() == 1;
        }
        public int ReConnect()
        {
            //CheckDeviceAvailable();
            this.Close();
            if (!RobotInitConnect()) return -1;

            //TODO: 需要確認是否要repeat讀位置
            //LaunchGetPos();

            return 0;
        }
        public bool RobotInitConnect()
        {
            //if (true)
            mobjCore = new FRRJIf.Core();
            //else
            //InitFanucApi();

            // You need to set data table before connecting.
            mobjDataTable = mobjCore.get_DataTable();

            {
                mobjAlarm = mobjDataTable.AddAlarm(FRRJIf.FRIF_DATA_TYPE.ALARM_LIST, 5, 0);
                mobjAlarmCurrent = mobjDataTable.AddAlarm(FRRJIf.FRIF_DATA_TYPE.ALARM_CURRENT, 1, 0);
                mobjCurPos = mobjDataTable.AddCurPos(FRRJIf.FRIF_DATA_TYPE.CURPOS, 1);
                mobjCurPosUF = mobjDataTable.AddCurPosUF(FRRJIf.FRIF_DATA_TYPE.CURPOS, 1, 9);
                mobjCurPos2 = mobjDataTable.AddCurPos(FRRJIf.FRIF_DATA_TYPE.CURPOS, 2);
                mobjTask = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK, 1);
                mobjTaskIgnoreMacro = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK_IGNORE_MACRO, 1);
                mobjTaskIgnoreKarel = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK_IGNORE_KAREL, 1);
                mobjTaskIgnoreMacroKarel = mobjDataTable.AddTask(FRRJIf.FRIF_DATA_TYPE.TASK_IGNORE_MACRO_KAREL, 1);
                mobjPosReg = mobjDataTable.AddPosReg(FRRJIf.FRIF_DATA_TYPE.POSREG, 1, 1, 40);
                mobjPosReg2 = mobjDataTable.AddPosReg(FRRJIf.FRIF_DATA_TYPE.POSREG, 2, 1, 4);
                mobjSysVarInt = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$FAST_CLOCK");
                mobjSysVarInt2 = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[10].$TIMER_VAL");
                mobjSysVarReal = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_REAL, "$MOR_GRP[1].$CURRENT_ANG[1]");
                mobjSysVarReal2 = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_REAL, "$DUTY_TEMP");
                mobjSysVarString = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_STRING, "$TIMER[10].$COMMENT");
                mobjSysVarPos = mobjDataTable.AddSysVarPos(FRRJIf.FRIF_DATA_TYPE.SYSVAR_POS, "$MNUTOOL[1,1]");
                mobjVarString = mobjDataTable.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_STRING, "$[HTTPKCL]CMDS[1]");
                mobjNumReg = mobjDataTable.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 60);
                mobjNumReg2 = mobjDataTable.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_REAL, 51, 60);
                mobjPosRegXyzwpr = mobjDataTable.AddPosRegXyzwpr(FRRJIf.FRIF_DATA_TYPE.POSREG_XYZWPR, 1, 1, 40);
                mobjStrReg = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.STRREG, 1, 3);
                mobjStrRegComment = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.STRREG_COMMENT, 1, 3);
            }

            // 2nd data table.
            // You must not set the first data table.
            mobjDataTable2 = mobjCore.get_DataTable2();
            mobjNumReg3 = mobjDataTable2.AddNumReg(FRRJIf.FRIF_DATA_TYPE.NUMREG_INT, 1, 5);
            mobjSysVarIntArray = new FRRJIf.DataSysVar[10];
            mobjSysVarIntArray[0] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[1].$TIMER_VAL");
            mobjSysVarIntArray[1] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[2].$TIMER_VAL");
            mobjSysVarIntArray[2] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[3].$TIMER_VAL");
            mobjSysVarIntArray[3] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[4].$TIMER_VAL");
            mobjSysVarIntArray[4] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[5].$TIMER_VAL");
            mobjSysVarIntArray[5] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[6].$TIMER_VAL");
            mobjSysVarIntArray[6] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[7].$TIMER_VAL");
            mobjSysVarIntArray[7] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[8].$TIMER_VAL");
            mobjSysVarIntArray[8] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[9].$TIMER_VAL");
            mobjSysVarIntArray[9] = mobjDataTable2.AddSysVar(FRRJIf.FRIF_DATA_TYPE.SYSVAR_INT, "$TIMER[10].$TIMER_VAL");

            //get host name
            System.Diagnostics.Debug.Assert(!string.IsNullOrEmpty(this.RobotIp));
            return mobjCore.Connect(this.RobotIp);
        }


        #endregion







        public void SystemRecoverAuto()
        {
            try
            {
                isUnderSystemRecoverAuto = true;
                //int UI1_IMSTP = 1;  //Always ON. ON:Normal, OFF:Emergent Stop
                //int UI2_HOLD = 1;  //Always ON. ON:Normal, OFF:PAUSE
                //int UI3_SFSPD = 1;  //Always ON, ON:Normal, OFF:Safty Speed
                //int UI4_CycleStop = 0;  //ON: [True]Abort, OFF: NA
                //int UI5_FaultReset_NEGEDGE = 1;  //Negtive Edge trigger(1 ->0)
                //int UI6_Start_NEGEDGE_NoUsed = 1;
                //int UI7_Home_NoUSed = 0;
                //int UI8_ENABLE = 1;  //Always ON, ON:ENavle Robot Action,OFF:Otherwise
                //Array UI9to16_PNS = new int[8];  //8bit PNS selection
                //int UI17_PNStrobe_POSEDGE = 0;  //Positive Edge trigger(0 ->1)
                //int UI18_ProdStart = 0;  //ON keep  >100ms, then Off tonegtive trigger

                Array UO3_ProgRunnungValue = new short[1];

                bool IsRestSUccess;
                //bool IsRWSucess = false;	// 移除未使用的變數。by YMWANGN, 2016/11/17。

                MvRobotUIOParameter UIO = new MvRobotUIOParameter();

                Array UI = new short[18];
                Array UOInfo = new short[18];
                Array UI18_ProdStart = new short[1];
                Array UI4_CycleStop = new short[1];

                IsRestSUccess = AlarmReset();

                UIO.UI17_PNStrobe_POSEDGE = 0;            //UI[17]:________(30ms)Positive edge trigger(Keep 30ms ), then fetch PNS:10000000
                UI.SetValue(UIO.UI1_IMSTP, 0);
                UI.SetValue(UIO.UI2_HOLD, 1);
                UI.SetValue(UIO.UI3_SFSPD, 2);
                UI.SetValue(UIO.UI4_CycleStop, 3);
                UI.SetValue(UIO.UI5_FaultReset_NEGEDGE, 4);
                UI.SetValue(UIO.UI6_Start_NEGEDGE_NoUsed, 5);
                UI.SetValue(UIO.UI7_Home_NoUSed, 6);
                UI.SetValue(UIO.UI8_ENABLE, 7);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(0), 8);   //PNS Code =10000000
                UI.SetValue(UIO.UI9to16_PNS.GetValue(1), 9);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(2), 10);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(3), 11);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(4), 12);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(5), 13);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(6), 14);
                UI.SetValue(UIO.UI9to16_PNS.GetValue(7), 15);
                UI.SetValue(UIO.UI17_PNStrobe_POSEDGE, 16);   //UI[17]:___________      LOW first,  Positive edge trigger(Keep 30ms ), thenfetch PNS:10000000
                UI.SetValue(UIO.UI18_ProdStart, 17);          //       ___________
                //UI[18]: (100ms)HIGH firs, negedge edge trigger(posedge keep 100ms first)
                mobjCore.WriteSDO(1, ref UI, 18);             //Initial State
                mobjCore.ReadUO(1, ref UOInfo, 18);
                Thread.Sleep(200);

                UIO.UI6_Start_NEGEDGE_NoUsed = 1;         //No use
                UIO.UI7_Home_NoUSed = 0;                  //No use
                //                ________
                UIO.UI17_PNStrobe_POSEDGE = 1;            //UI[17]:________|(30ms)Positive edge trigger(Keep 30ms ), then fetch PNS:10000000
                UIO.UI18_ProdStart = 1;                   //       ________
                //UI[18]: (100ms)|________negedge edge trigger(posedge keep 100ms first)
                UI.SetValue(UIO.UI1_IMSTP, 0);
                UI.SetValue(UIO.UI2_HOLD, 1);
                UI.SetValue(UIO.UI3_SFSPD, 2);
                UI.SetValue(UIO.UI4_CycleStop, 3);
                UI.SetValue(UIO.UI5_FaultReset_NEGEDGE, 4);
                UI.SetValue(UIO.UI6_Start_NEGEDGE_NoUsed, 5);
                UI.SetValue(UIO.UI7_Home_NoUSed, 6);
                UI.SetValue(UIO.UI8_ENABLE, 7);
                UI.SetValue(UIO.UI17_PNStrobe_POSEDGE, 16);
                UI.SetValue(UIO.UI18_ProdStart, 17);

                mobjCore.WriteSDO(1, UI, 17);
                mobjCore.ReadUO(1, ref UOInfo, 18);
                Thread.Sleep(200);

                if (Convert.ToInt32(UOInfo.GetValue(2).ToString()) == 1 ||  //Program is runnung
                    Convert.ToInt32(UOInfo.GetValue(3).ToString()) == 1)    //Program is paused
                {
                    UI4_CycleStop.SetValue((short)1, 0);                    //Abort first
                    mobjCore.WriteSDO(4, ref UI4_CycleStop, 1);

                    Thread.Sleep(100);

                    UI4_CycleStop.SetValue((short)0, 0);
                    mobjCore.WriteSDO(4, ref UI4_CycleStop, 1);
                }

                UI18_ProdStart.SetValue((short)0, 0);  //negedge edge trigger(posedgekeep 100ms first)
                mobjCore.WriteSDO(18, ref UI18_ProdStart, 1);

                Thread.Sleep(35);
            }
            finally
            {
                isUnderSystemRecoverAuto = false;
            }
        }
        [Obsolete("沒用到")]
        bool InitFanucApi()
        {
            // 請大家安裝Robot SDK  - YCLIUAB
            try
            {
                //************************Added by yhlinag to try initialize Fanuc API with thread __begin*************/

                Action act = delegate ()
                {
                    mobjCore = new FRRJIf.Core();
                    mreInitialFanucAPI.Set();
                    IsInitialFanucAPI = true;
                };

                Thread th = new Thread(new ThreadStart(act));
                th.Start();
                //mobjCore = new FRRJIf.Core();  //marked by yhlinag
                if (IsInitialFanucAPI)
                {
                    mreInitialFanucAPI.Reset();
                    mreInitialFanucAPI.WaitOne();
                }

                //************************Added by yhlinag to try initialize Fanuc API with thread __end*************/
            }
            catch (Exception)
            {
                // TODO: Feedback robot initla fail
                return false;
            }
            return true;
        }
        /*AlarmFuncComment
    * 
    * Specify argument Count as index of target alarm history item. (Specify 1 for the first item.)
   Argument AlarmID will have returned alarm ID. In case of ‘SRVO-001’, AlarmID is 11 that represents
   ‘SRVO’. Please see alarm code table in R-J3 reference. If there is no active alarm, AlarmID is zero for
   active alarm reference.
   Argument AlarmNumber will have returned alarm number. In case of ‘SRVO-001’, AlarmNumber is 1.
   If there is no active alarm, AlarmNumber is zero for active alarm reference.
   Argument CauseAlarmID will have returned cause alarm ID. Some alarm have two alarm messages.
   The second alarm is cause code. This argument is to read cause code. If there is no cause code,
   CauseAlarmID is 0.
   Argument CauseAlarmNumber will have returned cause alarm Number. This argument is to read cause
   code alarm number. If there is no cause code, CauseAlarmNumber is 0.
   Argument Severity will have return alarm severity. Severity value means as follows:
   NONE 128
   WARN 0
   PAUSE.L 2
   PAUSE.G 34
   STOP.L 6
   STOP.G 38
   SERVO 54
   ABORT.L 11
   ABORT.G 43
   SERVO2 58
   SYSTEM 123
   Argument Year, Month, Day, Hour, Minute, Second will have returned alarm occurred date and time (24
   hours format).
   Argument AlarmMessage will have returned alarm message. The message is the top line to teach
   pendant screen includes alarm code like ‘SRVO-001’. (Kanji message not supported.)
   Argument CauseAlarmMessage will have returned cause code alarm message. (Kanji message not
   supported)
   Argument SeverityMessage will have returned alarm severity string like ‘WARN’

*/


        #region Register Acceess

        public int GetRegValue(int index)
        {
            lock (this)
            {
                this.mobjDataTable.Refresh();
                Object reg = 0;
                mobjNumReg.GetValue(index, ref reg);
                return (int)reg;
            }
        }
        /// <summary>
        /// 寫入Robot位置暫存器PR
        /// </summary>
        /// <param name="PRno"></param>
        /// <param name="PosX"></param>
        /// <param name="PosY"></param>
        /// <param name="PosZ"></param>
        /// <param name="PosW"></param>
        /// <param name="PosP"></param>
        /// <param name="PosR"></param>
        /// <param name="PRUF"></param>
        /// <param name="PRUT"></param>
        public void WritePosReg(int PRno, float PosX, float PosY, float PosZ, float PosW, float PosP, float PosR, short PRUF, short PRUT)
        {
            Array xyzwprArray = new float[9];

            //Position 
            xyzwprArray.SetValue(PosX, 0);  //X_distance
            xyzwprArray.SetValue(PosY, 1);  //Y_distance
            xyzwprArray.SetValue(PosZ, 2);  //Z_distance
            xyzwprArray.SetValue(PosW, 3);  //W_orientation
            xyzwprArray.SetValue(PosP, 4);  //P_orientation
            xyzwprArray.SetValue(PosR, 5);  //R_orientation

            lock (this)
                mobjPosReg.SetValueXyzwpr(PRno, ref xyzwprArray, ref CurRobotInfo.configArray, PRUF, PRUT);
        }

        public MvaFanucRobotInfo ReadPosReg(int prno=0)
        {
            var robotInfo = new MvaFanucRobotInfo();
            lock (this)
                mobjPosReg.GetValue(prno, ref robotInfo.posArray, ref robotInfo.configArray, ref robotInfo.jointArray, ref robotInfo.userFrame, ref robotInfo.userTool, ref robotInfo.validC, ref robotInfo.validJ);

            return robotInfo;
        }


        /// <summary>
        /// 寫入Robot數字暫存器R
        /// </summary>
        /// <param name="Rno"></param>
        /// <param name="Value"></param>
        public void WriteRegValue(int index, int val)
        {
            lock (this)
                mobjNumReg.SetValue(index, val);
        }
        public void WriteRegValues(int index, int[] vals)
        {
            lock (this)
                mobjNumReg.SetValues(index, vals, vals.Length);
        }

        #endregion

        #region Pns0101


        public int Pns0101MoveStraightAsync(Array Target, int _SelectCorJ, int _SelectOfstOrPos, int _IsMoveUT, int Speed)
        {
            if (!this.IsConnected()) return -1;

            Array xyzwprArray = new float[9];
            Array ConfigArray = new short[7];
            Array JointArray = new float[9];
            Array TargetPos = Target;
            short UF = 0, UT = 0;
            short ValidC = 0, ValidJ = 0;
            int[] intValues = new int[5];
            //object R2Value = 0;
            //object R3Value = 0;

            this.WriteRegValue(3, _SelectCorJ);//Write R[3]. 0:Mov position, 1:Rotate J1~6
            this.WriteRegValue(7, _SelectOfstOrPos);//Write R[7]. 0:Mov with reated pos, 1:Mov with absolute Pos
            this.WriteRegValue(8, _IsMoveUT);//Write R[8].0:Offset with UF, 1:Offset with UT
            this.WriteRegValue(9, Speed);//Write R[9]. R[9] mm/sec
            this.WriteRegValue(5, 0);//Clear to ZERO. R[5] uses to returen MOV END.Return 51 means done.
            this.WriteRegValues(1, new int[] { 0, 0 });//Clear to ZERO.ThisR[1] uses to trigger Robot. Set to 1 means go!


            //從哪(當前 移到指到位置
            m_currRobotInfo = GetCurrRobotInfo();

            xyzwprArray = CurRobotInfo.posArray;
            ConfigArray = CurRobotInfo.configArray;
            JointArray = CurRobotInfo.jointArray;
            UF = CurRobotInfo.userFrame;
            UT = CurRobotInfo.userTool;
            ValidC = CurRobotInfo.validC;
            ValidJ = CurRobotInfo.validJ;

            if (_SelectCorJ == 0)
            {
                if (ValidC != 0)  //Valid Cartesian values
                {
                    xyzwprArray.SetValue(TargetPos.GetValue(0), 0);  //X_position
                    xyzwprArray.SetValue(TargetPos.GetValue(1), 1);  //Y_position
                    xyzwprArray.SetValue(TargetPos.GetValue(2), 2);  //Z_position
                    xyzwprArray.SetValue(TargetPos.GetValue(3), 3);  //W_position
                    xyzwprArray.SetValue(TargetPos.GetValue(4), 4);  //P_position
                    xyzwprArray.SetValue(TargetPos.GetValue(5), 5);  //R_position
                    if (TargetPos.Length > 6 && TargetPos.GetValue(6) != null)
                        xyzwprArray.SetValue(TargetPos.GetValue(6), 6);  //R_position
                }
                else
                {
                    //return "InValid C Return";
                }
            }
            else
            {
                if (ValidJ != 0)  //Valid Cartesian values
                {
                    JointArray.SetValue(TargetPos.GetValue(0), 0);  //J1_position
                    JointArray.SetValue(TargetPos.GetValue(1), 1);  //J2_position
                    JointArray.SetValue(TargetPos.GetValue(2), 2);  //J3_position
                    JointArray.SetValue(TargetPos.GetValue(3), 3);  //J4_position
                    JointArray.SetValue(TargetPos.GetValue(4), 4);  //J5_position
                    JointArray.SetValue(TargetPos.GetValue(5), 5);  //J6_position
                    if (TargetPos.Length > 6 && TargetPos.GetValue(6) != null)
                        JointArray.SetValue(TargetPos.GetValue(6), 6);  //J6_position
                }
                else
                {
                    //return "InValid J Return";
                }
            }

            ConfigArray.SetValue((short)0, 4);    //for Index 0
            ConfigArray.SetValue((short)0, 5);
            ConfigArray.SetValue((short)0, 6);

            if (_SelectCorJ == 0)
            {
                if (!mobjPosReg.SetValueXyzwpr(1, ref xyzwprArray, ref ConfigArray, UF, UT))
                {
                    System.Diagnostics.Debug.WriteLine("Write Fail");
                }
            }
            else
                mobjPosReg.SetValueJoint(2, ref JointArray, UF, UT);


            this.mobjDataTable.Refresh();

            this.WriteRegValues(1, new int[] { 1, 1 });

            return 0;
        }

        /// <summary>
        /// must 用thread 
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="_SelectCorJ"></param>
        /// <param name="_SelectOfstOrPos"></param>
        /// <param name="_IsMoveUT"></param>
        /// <param name="Speed"></param>
        /// <returns></returns>
        public bool Pns0101MoveStraightSync(Array Target, int _SelectCorJ, int _SelectOfstOrPos, int _IsMoveUT, int Speed)
        {
            Pns0101MoveStraightAsync(Target, _SelectCorJ, _SelectOfstOrPos, _IsMoveUT, Speed);

            while (this.MoveIsComplete())
            {
                m_currRobotInfo = GetCurrRobotInfo();

                var msg = "";
                var alarmInfo = new MvRobotAlarmInfo();
                if (HasRobotFault(ref msg, ref alarmInfo)) { break; }

                Thread.Sleep(100);
            }

            this.MoveCompeleteReply();

            return true;
        }

        public void Pns0101SwitchToolFrame(int UT)
        {
            try
            {
                #region code
                int UT_Setting = 0;
                int[] intValues = new int[5];

                UT_Setting = UT;

                for (int i = 0; i < 1; i++)
                    intValues[i] = UT_Setting;

                mobjNumReg.SetValues(4, intValues, 1);    //Set R[4]. UT number

                for (int i = 0; i < 1; i++)
                    intValues[i] = 1;
                mobjNumReg.SetValues(6, intValues, 1);    //Set R[6]. 0:Mov,1:UT Setting

                for (int i = 0; i < 1; i++)
                    intValues[i] = 1;
                mobjNumReg.SetValues(1, intValues, 1);
                #endregion code
            }
            catch (Exception)
            {
                //Log.GetInstance().Write(ex);
            }
        }
        /// <summary>
        /// Old Name: TCP_AutoSetting
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="W"></param>
        /// <param name="P"></param>
        /// <param name="R"></param>
        /// <param name="ToolSelected"></param>
        public void Pns0101SettingToolFrame(float X, float Y, float Z, float W, float P, float R, int ToolSelected)
        {
            try
            {
                #region code
                int UT_Selected = ToolSelected;
                int[] intValues = new int[1];
                bool a;
                Array xyzwprArray = new float[9];
                Array ConfigArray = new short[7];
                Array JointArray = new float[9];

                xyzwprArray.SetValue(X, 0);  //X_distance
                xyzwprArray.SetValue(Y, 1);  //Y_distance
                xyzwprArray.SetValue(Z, 2);  //Z_distance
                xyzwprArray.SetValue(W, 3);  //W_orientation
                xyzwprArray.SetValue(P, 4);  //P_orientation
                xyzwprArray.SetValue(R, 5);  //R_orientation

                //ConfigArray.SetValue((short)0, 0);    //for Index 0
                //ConfigArray.SetValue((short)0, 1);    //for Index 0
                //ConfigArray.SetValue((short)0, 2);
                //ConfigArray.SetValue((short)0, 3);
                //ConfigArray.SetValue((short)0, 4);    //for Index 0
                //ConfigArray.SetValue((short)0, 5);
                //ConfigArray.SetValue((short)0, 6);


                a = mobjPosReg.SetValueXyzwpr(1, ref xyzwprArray, ref CurRobotInfo.configArray, 9, (short)UT_Selected);  //Write to PR[1]

                this.WriteRegValue(11, UT_Selected);//Set R[11]. Selects TCP number
                this.WriteRegValue(10, 1);//Set R[10]. R[10] set to 1 to setting TCP then back to LBL[1]


                for (int i = 0; i < 1; i++)
                    intValues[i] = 1;
                mobjNumReg.SetValues(1, intValues, 1);     //Start to trigger TPE
                #endregion code
            }
            catch (Exception)
            {
                //Log.GetInstance().Write(ex);
            }
        }


        #endregion

        #region Pns0102



        public bool Pns0102AsynEnd()
        {
            if (!this.MoveIsComplete()) return false;
            this.MoveCompeleteReply();
            return true;
        }

        public void Pns0102AsynRun()
        {
            this.WriteRegValue(1, 1);//Write R[1]=1 to start program
        }

        public void Pns0102SynRun()
        {
            this.WriteRegValue(1, 1);//Write R[1]=1 to start program


            while (!this.MoveIsComplete())
            {
                Thread.Sleep(500);
            }
            this.MoveCompeleteReply();


        }
        #endregion

        #region Pns0103

        public void Pns0103ContinuityMove(List<float[]> Targets, int Continuity, int[] fineTargetIndex)
        {
            fineTargetIndex.Distinct().ToArray(); //Remove repeat index 防呆用
            Array.Sort(fineTargetIndex);

            int speed = 500;
            int MotionType = 1; //0:Offset; 1:Postion;2:Joint
            int MoveFrame = 0;
            int IsMoveTCP = 0;
            int CorJ, OfsOrPos;
            switch (MotionType)
            {
                case 0:
                    CorJ = 0; OfsOrPos = 0; break;
                case 1:
                    CorJ = 0; OfsOrPos = 1; break;
                case 2:
                    CorJ = 1; OfsOrPos = 0; break;
                default:
                    CorJ = 0; OfsOrPos = 0; break;
            }

            Dictionary<int[], bool> fineTarget_conDic = new Dictionary<int[], bool>();
            if (fineTargetIndex.Length > 1)
            {
                int indexTail = 1;
                for (int i = 0; i < fineTargetIndex.Length; i++)
                {
                    if (i == 0 && fineTargetIndex[i] != 1)
                    {
                        fineTarget_conDic.Add(Enumerable.Range(1, fineTargetIndex[i] - 1).ToArray(), false);
                        indexTail = fineTargetIndex[i];
                    }
                    else if (i == 0)
                    {
                        indexTail = fineTargetIndex[0];
                    }
                    else if (fineTargetIndex[i] - fineTargetIndex[i - 1] != 1)
                    {
                        fineTarget_conDic.Add(Enumerable.Range(indexTail, fineTargetIndex[i - 1] - indexTail + 1).ToArray(), true);
                        fineTarget_conDic.Add(Enumerable.Range(fineTargetIndex[i - 1] + 1, fineTargetIndex[i] - fineTargetIndex[i - 1] - 1).ToArray(), false);
                        indexTail = fineTargetIndex[i];
                    }
                    else if (i == fineTargetIndex.Length - 1)
                    {
                        fineTarget_conDic.Add(Enumerable.Range(indexTail, fineTargetIndex[i] - indexTail + 1).ToArray(), true);
                        indexTail = fineTargetIndex[i];
                        if (indexTail < Targets.Count)
                            fineTarget_conDic.Add(Enumerable.Range(indexTail + 1, Targets.Count - indexTail).ToArray(), false);
                    }
                }

                foreach (var finTarget in fineTarget_conDic)
                {
                    this.Pns0101SwitchToolFrame(MoveFrame);
                    List<float[]> tmpTargets = new List<float[]>();
                    if (finTarget.Value == true)
                    {
                        foreach (var targetIndex in finTarget.Key)
                        {
                            tmpTargets.Add(Targets[targetIndex]);
                        }
                        this.Pns0103ContinuityMove(tmpTargets, Continuity, CorJ, OfsOrPos, IsMoveTCP, speed);
                    }
                    else
                    {
                        this.Pns0103ContinuityMove(tmpTargets, 0, CorJ, OfsOrPos, IsMoveTCP, speed);
                    }
                    tmpTargets.Clear();
                }
            }
            else if (fineTargetIndex.Length == 1)
            {
                List<float[]> tmpTargets = new List<float[]>();
                tmpTargets.Add(Targets[0]);
                this.Pns0101SwitchToolFrame(MoveFrame);
                this.Pns0103ContinuityMove(tmpTargets, 0, CorJ, OfsOrPos, IsMoveTCP, speed);
                tmpTargets.Clear();
            }
            else
            {
                this.Pns0101SwitchToolFrame(MoveFrame);
                this.Pns0103ContinuityMove(Targets, Continuity, CorJ, OfsOrPos, IsMoveTCP, speed);
            }
        }
        public void Pns0103ContinuityMove(float[] target)
        {
            float[] pos = target;
            List<float[]> targets = new List<float[]>();
            targets.Add(pos);
            Pns0103ContinuityMove(targets, 0, new int[] { 1 });
            targets.Clear();
        }
        public int Pns0103ContinuityMove(List<float[]> Targets, int _Continuity, int _SelectCorJ, int _SelectOfstOrPos, int _IsMoveUT, int Speed)
        {
            if (!this.IsConnected()) return -1;
            Array xyzwprArray = new float[9];
            Array ConfigArray = new short[7];
            Array JointArray = new float[9];
            short UF = 0, UT = 0;
            short ValidC = 0, ValidJ = 0;
            int[] intValues = new int[5];
            object R2Value = 0;
            object R3Value = 0;
            int targets_cnt = Targets.Count;

            for (int i = 0; i < 1; i++)
                intValues[i] = _SelectCorJ;
            mobjNumReg.SetValues(3, intValues, 1);    //Write R[3]. 0:Mov ,position, 1:Rotate J1~6


            for (int i = 0; i < 1; i++)
                intValues[i] = _SelectOfstOrPos;
            mobjNumReg.SetValues(7, intValues, 1);    //Write R[7]. 0:Mov with reated pos, 1:Mov with absolute Pos


            for (int i = 0; i < 1; i++)
                intValues[i] = _IsMoveUT;
            mobjNumReg.SetValues(8, intValues, 1);    //Write R[8].0:Offset with UF, 1:Offset with UT

            for (int i = 0; i < 1; i++)
                intValues[i] = Speed;
            mobjNumReg.SetValues(9, intValues, 1);    //Write R[9]. R[9] mm/sec

            for (int i = 0; i < 1; i++)               //Clear to ZERO. R[5]uses to returen MOV END.Return 51 means done.
                intValues[i] = 0;
            mobjNumReg.SetValues(5, intValues, 1);

            for (int i = 0; i < 2; i++)               //Clear to ZERO.ThisR[1] uses to trigger Robot. Set to 1 means go!
                intValues[i] = 0;
            mobjNumReg.SetValues(1, intValues, 2);

            for (int i = 0; i < 1; i++)
                intValues[i] = targets_cnt;
            mobjNumReg.SetValues(42, intValues, 1); //Write R[42] so that give summation of Move Position Count

            for (int i = 0; i < 1; i++)
                intValues[i] = _Continuity;
            mobjNumReg.SetValues(43, intValues, 1); //Write R[43] for moving continuity setting


            //從哪(當前 移到指到位置
            m_currRobotInfo = GetCurrRobotInfo();

            xyzwprArray = CurRobotInfo.posArray;
            ConfigArray = CurRobotInfo.configArray;
            JointArray = CurRobotInfo.jointArray;
            UF = CurRobotInfo.userFrame;
            UT = CurRobotInfo.userTool;
            ValidC = CurRobotInfo.validC;
            ValidJ = CurRobotInfo.validJ;

            ConfigArray.SetValue((short)0, 4);    //for Index 0
            ConfigArray.SetValue((short)0, 5);
            ConfigArray.SetValue((short)0, 6);

            for (int i = 0; i < targets_cnt; i++)
            {
                if (_SelectCorJ == 0)
                {
                    if (ValidC != 0)  //Valid Cartesian values
                    {
                        xyzwprArray.SetValue(Targets[i].GetValue(0), 0);  //X_position
                        xyzwprArray.SetValue(Targets[i].GetValue(1), 1);  //Y_position
                        xyzwprArray.SetValue(Targets[i].GetValue(2), 2);  //Z_position
                        xyzwprArray.SetValue(Targets[i].GetValue(3), 3);  //W_position
                        xyzwprArray.SetValue(Targets[i].GetValue(4), 4);  //P_position
                        xyzwprArray.SetValue(Targets[i].GetValue(5), 5);  //R_position
                        if (Targets[i].Length > 6 && Targets[i].GetValue(6) != null)
                            xyzwprArray.SetValue(Targets[i].GetValue(6), 6);  //R_position
                    }
                    else
                    {
                        //return "InValid C Return";
                    }
                }
                else
                {
                    if (ValidJ != 0)  //Valid Cartesian values
                    {
                        JointArray.SetValue(Targets[i].GetValue(0), 0);  //J1_position
                        JointArray.SetValue(Targets[i].GetValue(1), 1);  //J2_position
                        JointArray.SetValue(Targets[i].GetValue(2), 2);  //J3_position
                        JointArray.SetValue(Targets[i].GetValue(3), 3);  //J4_position
                        JointArray.SetValue(Targets[i].GetValue(4), 4);  //J5_position
                        JointArray.SetValue(Targets[i].GetValue(5), 5);  //J6_position
                        if (Targets[i].GetValue(6) != null)
                            JointArray.SetValue(Targets[i].GetValue(6), 6);  //R_position
                    }
                    else
                    {
                        //return "InValid J Return";
                    }
                }

                if (_SelectCorJ == 0)
                    mobjPosReg.SetValueXyzwpr(1, ref xyzwprArray, ref ConfigArray, UF, UT);
                else
                    mobjPosReg.SetValueJoint(2, ref JointArray, UF, UT);
            }

            for (int i = 0; i < 2; i++)
                intValues[i] = 1;

            mobjNumReg.SetValues(1, intValues, 2);

            return 0;
        }

        #endregion

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





        void DisposeSelf()
        {
            this.Close();
        }

        #endregion

    }
}
