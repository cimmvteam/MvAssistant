using MvAssistant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.FanucRobot_v42_15
{
    public class MvFanucRobotLdd : IDisposable
    {
        /* 20200517 所有lock皆需使用 this
         Fanuc API 並不允許多執行緒同步操作
         為避免異常發生, 所有向Fanuc API存取 都需用同一個lock object*/



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
        private FRRJIf.DataPosRegMG mobjPosRegMG;
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

        #endregion

        public bool isUnderSystemRecoverAuto = false;
        public string RobotIp;
        private bool HasRobotFaultStatus = false;
        ManualResetEvent mreInitialFanucAPI = new ManualResetEvent(false);
        ~MvFanucRobotLdd() { this.Dispose(false); }




        public MvFanucRobotInfo GetCurrRobotInfo()
        {
            var msg = "";
            var alarmInfo = new MvRobotAlarm();
            HasRobotFault(ref msg, ref alarmInfo);

            var robotInfo = new MvFanucRobotInfo();
            robotInfo.PosReg = this.ReadCurPosUf();
            robotInfo.RobotTime = DateTime.Now;
            robotInfo.IsReachTarget = this.MoveIsComplete();

            return robotInfo;
        }

        public void MoveCompeleteReply()
        {
            this.SetRegIntValue(5, 0);
        }

        public bool MoveIsComplete()
        {
            var reg5 = this.ReadRegIntValue(5);
            return reg5 == 51;
        }


        #region Device Connection

        public int Close()
        {
            // 若 mobjCore 不為空
            if (mobjCore != null)
            {
                // 將 mobjCore 斷開連線
                //try {
                mobjCore.Disconnect();
                //}
                //catch (Exception ex) { MvLog.WarnNs(this, ex); }
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
            return mobjCore.ConnectState == 1;
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
            bool blnRes = false;
            int lngTmp = 0;


            mobjCore = new FRRJIf.Core();

            // You need to set data table before connecting.
            mobjDataTable = mobjCore.DataTable;

            {
                mobjAlarm = mobjDataTable.AddAlarm(FRRJIf.FRIF_DATA_TYPE.ALARM_LIST, 5, 0);
                mobjAlarmCurrent = mobjDataTable.AddAlarm(FRRJIf.FRIF_DATA_TYPE.ALARM_CURRENT, 1, 0);
                mobjCurPos = mobjDataTable.AddCurPos(FRRJIf.FRIF_DATA_TYPE.CURPOS, 1);
                mobjCurPosUF = mobjDataTable.AddCurPosUF(FRRJIf.FRIF_DATA_TYPE.CURPOS, 1, 15);
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
                mobjPosRegMG = mobjDataTable.AddPosRegMG(FRRJIf.FRIF_DATA_TYPE.POSREGMG, "C,J6", 1, 10);
                mobjStrReg = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.STRREG, 1, 3);
                mobjStrRegComment = mobjDataTable.AddString(FRRJIf.FRIF_DATA_TYPE.STRREG_COMMENT, 1, 3);
                //Debug.Assert(mobjStrRegComment != null);
            }

            // 2nd data table.
            // You must not set the first data table.
            mobjDataTable2 = mobjCore.DataTable2;
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
            if (string.IsNullOrEmpty(this.RobotIp))
            {
                return false;
            }

            //connect
            if (lngTmp > 0)
                mobjCore.TimeOutValue = lngTmp;
            blnRes = mobjCore.Connect(this.RobotIp);
            if (blnRes == false)
            {
                msubClearVars();
                return false;
            }

            return true;

        }

        private void msubClearVars()
        {

            mobjCore.Disconnect();

            mobjCore = null;
            mobjDataTable = null;
            mobjDataTable2 = null;
            mobjAlarm = null;
            mobjAlarmCurrent = null;
            mobjCurPos = null;
            mobjCurPos2 = null;
            mobjTask = null;
            mobjTaskIgnoreMacro = null;
            mobjTaskIgnoreKarel = null;
            mobjTaskIgnoreMacroKarel = null;
            mobjPosReg = null;
            mobjPosReg2 = null;
            mobjSysVarInt = null;
            mobjSysVarReal = null;
            mobjSysVarReal2 = null;
            mobjSysVarString = null;
            mobjSysVarPos = null;
            mobjNumReg = null;
            mobjNumReg2 = null;
            mobjNumReg3 = null;
            mobjVarString = null;
            mobjStrReg = null;
            mobjStrRegComment = null;
            for (int ii = mobjSysVarIntArray.GetLowerBound(0); ii <= mobjSysVarIntArray.GetUpperBound(0); ii++)
            {
                mobjSysVarIntArray[ii] = null;
            }

        }

        #endregion

        #region System / Program

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


        public bool HasRobotFault(ref string message, ref MvRobotAlarm alarmInfo)
        {
            //************IMPORTANT*************************************************//
            //UO[1~20] address has been mapping to DI[1~20] address at addr.22
            //AND using BGLogic assign DI[1]~DI[20] to R[21]~R[40] respectivly
            //If you wanna read UO[1], plz read R[21] and so on 
            //**********************************************************************

            object UO6Value = (byte)0;
            bool IsReadSuccess = false;



            //1:有err   0:normal
            IsReadSuccess = this.GetRegValue(26, ref UO6Value);
            if (!IsReadSuccess)
                throw new Exception("Read Fail");
            if ((int)UO6Value == 1)
            {
                alarmInfo = ReadRobotAlarmInfo();
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


        #endregion



        #region Register

        public bool GetPosRegValue(int PRno, MvFanucRobotPosReg posReg, bool isNeedRefresh = true)
        {
            //isNeedRefresh 預設為true, 確保取得最新資料, 若己知不需要更新, 可以設為false
            lock (this)
            {
                if (isNeedRefresh)
                    this.mobjDataTable.Refresh();
                return this.mobjPosReg.GetValue(PRno, ref posReg.XyzwpreArrary, ref posReg.ConfigArray, ref posReg.JointArray,
                    ref posReg.UserFrame, ref posReg.UserTool, ref posReg.ValidC, ref posReg.ValidJ);
            }
        }
        public bool GetRegValue(int index, ref Object value, bool isNeedRefresh = true)
        {
            //isNeedRefresh 預設為true, 確保取得最新資料, 若己知不需要更新, 可以設為false
            lock (this)
            {
                if (isNeedRefresh)
                    this.mobjDataTable.Refresh();
                return this.mobjNumReg.GetValue(index, ref value);
            }
        }
        public bool GetAlarmInfo(MvRobotAlarm alminfo, bool isNeedRefresh = true)
        {
            lock (this)
            {
                if (isNeedRefresh)
                    mobjDataTable.Refresh();

                return mobjAlarm.GetValue(1,
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
            }
        }
        public bool GetCurPosUf(MvFanucRobotPosReg posReg, bool isNeedRefresh = true)
        {
            lock (this)
            {
                if (isNeedRefresh)
                    this.mobjDataTable.Refresh();

                return this.mobjCurPosUF.GetValue(ref posReg.XyzwpreArrary, ref posReg.ConfigArray, ref posReg.JointArray,
                    ref posReg.UserFrame, ref posReg.UserTool, ref posReg.ValidC, ref posReg.ValidJ);
            }
        }

        public MvFanucRobotPosReg ReadPosReg(int PRno = 0)
        {
            var posReg = new MvFanucRobotPosReg();
            if (!this.GetPosRegValue(PRno, posReg))
                throw new MvException("Fail to read position register");
            return posReg;
        }
        public MvFanucRobotPosReg ReadCurPosUf()
        {
            var posReg = new MvFanucRobotPosReg();
            if (!this.GetCurPosUf(posReg))
                throw new MvException("Fail to read current position");
            return posReg;
        }

        public int ReadRegIntValue(int index)
        {
            object reg = 0;
            if (!this.GetRegValue(index, ref reg))
                throw new MvException("Fail to read register value");
            return (int)reg;
        }

        public MvRobotAlarm ReadRobotAlarmInfo()
        {
            MvRobotAlarm alminfo = new MvRobotAlarm();
            if (!this.GetAlarmInfo(alminfo))
                throw new MvException("Fail to read alarm info");
            return alminfo;
        }
        public StringBuilder RegisterTest()
        {

            mobjDataTable.Refresh();

            var sb = new StringBuilder();

            Array xyzwpr = new float[9];
            Array config = new short[7];
            Array joint = new float[9];
            short intUF = 0;
            short intUT = 0;
            short intValidC = 0;
            short intValidJ = 0;


            for (var ii = mobjPosReg.StartIndex; ii <= mobjPosReg.EndIndex; ii++)
            {
                if (mobjPosReg.GetValue(ii, ref xyzwpr, ref config, ref joint, ref intUF, ref intUT, ref intValidC, ref intValidJ))
                {
                    sb.Append("PR[" + ii + "]\r\n");
                    sb.Append(RegisterTestPrint(ref xyzwpr, ref config, ref joint, intValidC, intValidJ, intUF, intUT));
                }
                else
                {
                    sb.Append("PR[" + ii + "] : Error!!! \r\n");
                }
            }
            return sb;
        }
        public string RegisterTestPrint(ref Array xyzwpr, ref Array config, ref Array joint, short intValidC, short intValidJ, int UF, int UT)
        {
            string tmp = "";
            int ii = 0;

            tmp = tmp + "UF = " + UF + ", ";
            tmp = tmp + "UT = " + UT + "\r\n";
            if (intValidC != 0)
            {
                tmp = tmp + "XYZWPR = ";
                //5
                for (ii = 0; ii <= 8; ii++)
                {
                    tmp = tmp + xyzwpr.GetValue(ii) + " ";
                }

                tmp = tmp + "\r\n" + "CONFIG = ";
                if ((short)config.GetValue(0) != 0)
                {
                    tmp = tmp + "F ";
                }
                else
                {
                    tmp = tmp + "N ";
                }
                if ((short)config.GetValue(1) != 0)
                {
                    tmp = tmp + "L ";
                }
                else
                {
                    tmp = tmp + "R ";
                }
                if ((short)config.GetValue(2) != 0)
                {
                    tmp = tmp + "U ";
                }
                else
                {
                    tmp = tmp + "D ";
                }
                if ((short)config.GetValue(3) != 0)
                {
                    tmp = tmp + "T ";
                }
                else
                {
                    tmp = tmp + "B ";
                }
                tmp = tmp + String.Format("{0}, {1}, {2}\r\n", config.GetValue(4), config.GetValue(5), config.GetValue(6));
            }

            if (intValidJ != 0)
            {
                tmp = tmp + "JOINT = ";
                //5
                for (ii = 0; ii <= 8; ii++)
                {
                    tmp = tmp + joint.GetValue(ii) + " ";
                }
                tmp = tmp + "\r\n";
            }

            return tmp;

        }

        public bool SetPosRegJoint(int PRno, MvFanucRobotPosReg posReg, short userFrame = -1, short userTool = -1)
        {
            lock (this)
                return mobjPosReg.SetValueJoint(PRno, ref posReg.JointArray, userFrame, userTool);//User Frame 及 User Tool 要帶-1 才有辦法修改
        }
        public bool SetPosRegXyzWpr(int PRno, MvFanucRobotPosReg posReg, short userFrame = -1, short userTool = -1)
        {
            lock (this)
                return mobjPosReg.SetValueXyzwpr(PRno, ref posReg.XyzwpreArrary, ref posReg.ConfigArray, userFrame, userTool);//User Frame 及 User Tool 要帶-1 才有辦法修改
        }
        /// <summary>
        /// 寫入Robot數字暫存器R
        /// </summary>
        /// <param name="Rno"></param>
        /// <param name="Value"></param>
        public bool SetRegIntValue(int index, int val)
        {
            lock (this)
                return this.mobjNumReg.SetValue(index, val);
        }
        public bool SetRegIntValues(int index, int[] vals)
        {
            lock (this)
                return this.mobjNumReg.SetValues(index, vals, vals.Length);
        }

        #endregion




        #region Pns0101


        public int Pns0101MoveStraightAsync(Array Target, int _SelectCorJ, int _SelectOfstOrPos, int _IsMoveUT, int Speed)
        {
            if (!this.IsConnected()) return -1;

            Array TargetPos = Target;

            this.SetRegIntValue(3, _SelectCorJ);//Write R[3]. 0:Mov position, 1:Rotate J1~6
            this.SetRegIntValue(7, _SelectOfstOrPos);//Write R[7]. 0:Mov with reated pos, 1:Mov with absolute Pos
            this.SetRegIntValue(8, _IsMoveUT);//Write R[8].0:Offset with UF, 1:Offset with UT
            this.SetRegIntValue(9, Speed);//Write R[9]. R[9] mm/sec
            this.SetRegIntValue(5, 0);//Clear to ZERO. R[5] uses to returen MOV END.Return 51 means done.
            this.SetRegIntValues(1, new int[] { 0, 0 });//Clear to ZERO.ThisR[1] uses to trigger Robot. Set to 1 means go!


            //從哪(當前 移到指到位置
            var robotInfo = GetCurrRobotInfo();

            //還不確定為何要帶全帶0
            robotInfo.PosReg.ConfigArray.SetValue((short)0, 4);    //for Index 0
            robotInfo.PosReg.ConfigArray.SetValue((short)0, 5);
            robotInfo.PosReg.ConfigArray.SetValue((short)0, 6);



            if (_SelectCorJ == 0)
            {
                if (robotInfo.ValidC != 0)  //Valid Cartesian values
                {
                    for (var idx = 0; idx < TargetPos.Length && idx < robotInfo.PosReg.XyzwpreArrary.Length; idx++)
                        robotInfo.PosReg.XyzwpreArrary.SetValue(TargetPos.GetValue(idx), idx);  //X_position
                }
                else
                {
                    //return "InValid C Return";
                }
            }
            else
            {
                if (robotInfo.ValidJ != 0)  //Valid Cartesian values
                {
                    for (var idx = 0; idx < TargetPos.Length && idx < robotInfo.PosReg.JointArray.Length; idx++)
                        robotInfo.PosReg.JointArray.SetValue(TargetPos.GetValue(idx), idx);  //X_position
                }
                else
                {
                    //return "InValid J Return";
                }
            }



            if (_SelectCorJ == 0)
            {
                if (!this.SetPosRegXyzWpr(1, robotInfo.PosReg))
                    throw new MvException("Fail to write position register");
            }
            else
            {
                if (!this.SetPosRegJoint(2, robotInfo.PosReg))
                    throw new MvException("Fail to write position register");
            }


            this.mobjDataTable.Refresh();
            
            this.SetRegIntValues(1, new int[] { 1, 1 });//R[1]=1 is start program

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
                GetCurrRobotInfo();

                var msg = "";
                var alarmInfo = new MvRobotAlarm();
                if (HasRobotFault(ref msg, ref alarmInfo)) { break; }

                Thread.Sleep(100);
            }

            this.MoveCompeleteReply();

            return true;
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
                var CurRobotInfo = this.GetCurrRobotInfo();


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



                this.SetPosRegXyzWpr(1, CurRobotInfo.PosReg, 9, (short)UT_Selected);//Write to PR[1]

                this.SetRegIntValue(11, UT_Selected);//Set R[11]. Selects TCP number
                this.SetRegIntValue(10, 1);//Set R[10]. R[10] set to 1 to setting TCP then back to LBL[1]


                for (int i = 0; i < 1; i++)
                    intValues[i] = 1;
                mobjNumReg.SetValues(1, intValues, 1);     //Start to trigger TPE

            }
            catch (Exception)
            {
                //Log.GetInstance().Write(ex);
            }
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
            this.SetRegIntValue(1, 1);//Write R[1]=1 to start program
        }

        public void Pns0102SynRun()
        {
            this.SetRegIntValue(1, 1);//Write R[1]=1 to start program


            while (!this.MoveIsComplete())
            {
                Thread.Sleep(500);
            }
            this.MoveCompeleteReply();


        }
        #endregion

        #region LogInfo()
        public void LogInfo(string pMessage)
        {
            string tFilePath = @"D:\Logg.txt";
            StreamWriter tStreamWriter = null;
            try
            {
                if (!File.Exists(tFilePath)) File.Create(tFilePath);
                tStreamWriter = new StreamWriter(tFilePath, true, System.Text.UTF8Encoding.UTF8);
                tStreamWriter.WriteLine(pMessage);
            }
            catch (Exception e) { }
            finally
            {
                if (tStreamWriter != null) tStreamWriter.Close();
            }
        }
        #endregion LogInfo()


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
