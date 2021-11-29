using MvAssistant.v0_2.DeviceDrive.OmronPlc;
using MvAssistant.v0_2.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.CompPlc
{
    [Guid("EEED741C-18BC-465E-9772-99F19DD68BD3")]
    public class MacHalPlcContext : IDisposable
    {


        public MacHalPlcBoxTransfer BoxTransfer;
        public MacHalPlcCabinet Cabinet;
        public MacHalPlcCleanCh CleanCh;
        public MacHalPlcInspectionCh InspectionCh;
        public MacHalPlcLoadPort LoadPort;
        public MacHalPlcMaskTransfer MaskTransfer;
        public MacHalPlcOpenStage OpenStage;
        public string PlcIp;
        public MvaOmronPlcLdd PlcLdd;
        public int PlcPortId;
        public MacHalPlcEqp Universal;
        bool m_isConnected = false;

        MvaTask m_keepConnection;


        public MacHalPlcContext()
        {
            this.InspectionCh = new MacHalPlcInspectionCh(this);
            this.BoxTransfer = new MacHalPlcBoxTransfer(this);
            this.MaskTransfer = new MacHalPlcMaskTransfer(this);
            this.OpenStage = new MacHalPlcOpenStage(this);
            this.Cabinet = new MacHalPlcCabinet(this);
            this.CleanCh = new MacHalPlcCleanCh(this);
            this.LoadPort = new MacHalPlcLoadPort(this);
            this.Universal = new MacHalPlcEqp(this);
        }

        ~MacHalPlcContext() { this.Dispose(false); }

        public bool IsConnected { get { return PlcLdd.IsConnected(); } }
        public bool IsConnectedByHandShake { get { return m_isConnected; } }


        #region Communication

        public void Close()
        {
            if (this.PlcLdd != null)
            {
                using (var obj = this.PlcLdd)
                {
                    obj.NLPLC_ClosePort();
                    this.PlcLdd = null;
                }
            }

            if (this.m_keepConnection != null)
            {
                using (var obj = this.m_keepConnection)
                {
                    obj.Cancel();
                    SpinWait.SpinUntil(() => obj.IsEnd(), 1000);
                }
                this.m_keepConnection = null;
            }
        }

        public void ClosePort()
        {
            this.PlcLdd.NLPLC_ClosePort();
        }

        public void Connect(string ip = null, int? port = null)
        {
            if (ip != null) this.PlcIp = ip;
            if (port != null) this.PlcPortId = port.Value;

            this.PlcLdd = new MvaOmronPlcLdd();
            this.PlcLdd.NLPLC_Initial(this.PlcIp, this.PlcPortId);
        }

        public int StartAsyn()
        {


            this.m_keepConnection = MvaTask.RunLoop(() =>
            {

                this.Write(MacHalPlcEnumVariable.PC_TO_PLC_CheckClock, false);
                if (!SpinWait.SpinUntil(() => !this.Read<bool>(MacHalPlcEnumVariable.PC_TO_PLC_CheckClock_Reply), 2500))
                {
                    this.LockAssign(ref this.m_isConnected, false);
                    return false;
                }
                this.LockAssign(ref this.m_isConnected, true);

                this.Write(MacHalPlcEnumVariable.PC_TO_PLC_CheckClock, true);
                if (!SpinWait.SpinUntil(() => this.Read<bool>(MacHalPlcEnumVariable.PC_TO_PLC_CheckClock_Reply), 2500))
                {

                    this.LockAssign(ref this.m_isConnected, false);
                    //throw new MvException("PLC connection T1 timeout");
                    return false;
                }
                else this.LockAssign(ref this.m_isConnected, true);

                return true;
            }, 1000);



            return 0;
        }
        void LockAssign<T>(ref T prop, T val)
        {
            lock (this)
            {
                prop = val;
            }
        }
        #endregion

        #region Access

        public T Read<T>(MacHalPlcEnumVariable plcvar)
        {
            var obj = this.PlcLdd.Read(plcvar.ToString());
            return (T)obj;
        }
        public Object Read(MacHalPlcEnumVariable plcvar)
        {
            var obj = this.PlcLdd.Read(plcvar.ToString());
            return obj;
        }

        public Dictionary<MacHalPlcEnumVariable, Object> ReadMulti(IEnumerable<MacHalPlcEnumVariable> varNames)
        {
            var names = varNames.Select(x => x.ToString()).ToArray();
            var table = this.PlcLdd.ReadMulti(names);
            var rtn = new Dictionary<MacHalPlcEnumVariable, Object>();
            foreach (DictionaryEntry kv in table)
            {
                var key = MvaUtil.EnumParse<MacHalPlcEnumVariable>(kv.Key as String);
                rtn[key] = kv.Value;
            }
            return rtn;
        }
        public void Write(MacHalPlcEnumVariable plcvar, object data)
        {
            this.PlcLdd.Write(plcvar.ToString(), data);
        }

        #endregion


        #region EQP Comm

        /// <summary>
        /// A08外罩風扇編號、風速控制
        /// </summary>
        /// <param name="FanID"></param>
        /// <param name="WindSpeed"></param>
        /// <returns></returns>
        public string CoverFanCtrl(uint FanID, uint WindSpeed)
        {
            string Result = "";
            try
            {
                this.Write(MacHalPlcEnumVariable.PC_TO_FFU_SetSpeed, WindSpeed);
                this.Write(MacHalPlcEnumVariable.PC_TO_FFU_Address, FanID);
                this.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, false);
                Thread.Sleep(100);
                this.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, true);

                if (!SpinWait.SpinUntil(() => this.Read<bool>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Reply), 1000))
                    throw new MvaException("Outer Cover Fan Control T0 timeout");
                else if (!SpinWait.SpinUntil(() => this.Read<bool>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Complete), 5000))
                    throw new MvaException("Outer Cover Fan Control T2 timeout");

                switch (this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                    case 2:
                        throw new MvaException("Outer Cover Fan Control Error : Failed");
                    default:
                        throw new MvaException("Outer Cover Fan Control Error : Unknown error");
                }

                this.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, false);

                if (!SpinWait.SpinUntil(() => !this.Read<bool>(MacHalPlcEnumVariable.FFU_TO_PC_Write_Complete), 1000))
                    throw new MvaException("Outer Cover Fan Control T4 timeout");
            }
            catch (Exception ex)
            {
                this.Write(MacHalPlcEnumVariable.PC_TO_FFU_Write, false);
                throw ex;
            }
            return Result;
        }

        /// <summary>
        /// 當Assembly出錯時，針對部件下緊急停止訊號
        /// </summary>
        /// <param name="BT_EMS">Box Transfer是否緊急停止</param>
        /// <param name="RT_EMS">Mask Transfer是否緊急停止</param>
        /// <param name="OS_EMS">Open Stage是否緊急停止</param>
        /// <param name="IC_EMS">Inspection Chamber是否緊急停止</param>
        public void EMSAlarm(bool BT_EMS, bool RT_EMS, bool OS_EMS, bool IC_EMS)
        {
            this.Write(MacHalPlcEnumVariable.PC_TO_BT_EMS, BT_EMS);
            this.Write(MacHalPlcEnumVariable.PC_TO_MT_EMS, RT_EMS);
            this.Write(MacHalPlcEnumVariable.PC_TO_OS_EMS, OS_EMS);
            this.Write(MacHalPlcEnumVariable.PC_TO_IC_EMS, IC_EMS);
            Thread.Sleep(1000);
            if (this.Read<bool>(MacHalPlcEnumVariable.BT_TO_PC_EMS_Reply) != BT_EMS)
                throw new MvaException("PLC did not get 'Box Transfer EMS' alarm signal");
            else if (this.Read<bool>(MacHalPlcEnumVariable.MT_TO_PC_EMS_Reply) != RT_EMS)
                throw new MvaException("PLC did not get 'Mask Transfer EMS' alarm signal");
            else if (this.Read<bool>(MacHalPlcEnumVariable.OS_TO_PC_EMS_Reply) != OS_EMS)
                throw new MvaException("PLC did not get 'Open Stage EMS' alarm signal");
            else if (this.Read<bool>(MacHalPlcEnumVariable.IC_TO_PC_EMS_Reply) != IC_EMS)
                throw new MvaException("PLC did not get 'Inspection Chamber EMS' alarm signal");

        }

        public List<int> ReadCoverFanSpeed()
        {
            List<int> FanSpeedList = new List<int>();
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_1));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_2));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_3));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_4));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_5));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_6));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_7));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_8));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_9));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_10));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_11));
            FanSpeedList.Add(this.Read<int>(MacHalPlcEnumVariable.FFU_TO_PC_FFUCurrentSpeed_12));
            return FanSpeedList;

        }

        public void ResetAll()
        {
            try
            {
                this.Write(MacHalPlcEnumVariable.Reset_ALL, false);
                Thread.Sleep(100);
                this.Write(MacHalPlcEnumVariable.Reset_ALL, true);

                if (!SpinWait.SpinUntil(() => this.Read<bool>(MacHalPlcEnumVariable.Reset_ALL_Complete), 2000))
                    throw new MvaException("Reset All T0 timeout");

                this.Write(MacHalPlcEnumVariable.Reset_ALL, false);
            }
            catch (Exception ex)
            {
                this.Write(MacHalPlcEnumVariable.Reset_ALL, false);
                throw ex;
            }
        }

        //蜂鳴器
        public void SetBuzzer(uint BuzzerType)
        {
            this.Write(MacHalPlcEnumVariable.PC_TO_DR_Buzzer, BuzzerType);
        }

        //信號燈
        public void SetSignalTower(bool Red, bool Orange, bool Blue)
        {
            this.Write(MacHalPlcEnumVariable.PC_TO_DR_Red, Red);
            this.Write(MacHalPlcEnumVariable.PC_TO_DR_Orange, Orange);
            this.Write(MacHalPlcEnumVariable.PC_TO_DR_Blue, Blue);
        }
        #endregion




        #region PLC狀態訊號
        public bool ReadBCP_Door()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Door);
        }

        public Tuple<bool, bool, bool, bool, bool> ReadBCP_EMO()
        {
            return new Tuple<bool, bool, bool, bool, bool>(
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO1),
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO2),
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO3),
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO4),
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_EMO5)
                );
        }

        public bool ReadBCP_Maintenance()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Maintenance);
        }

        public bool ReadBCP_Smoke()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BCP_Smoke);
        }

        public bool ReadBT_FrontLimitSenser()
        { return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BT_FLS); }

        public bool ReadBT_RearLimitSenser()
        { return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_BT_RLS); }

        public Tuple<bool, bool, bool> ReadCB_EMO()
        {
            return new Tuple<bool, bool, bool>(
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO1),
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO2),
                this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_EMO3)
                );
        }

        public bool ReadCB_Maintenance()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_CB_Maintenance);
        }

        public bool ReadLP_Light_Curtain()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP_Light_Curtain);
        }

        public bool ReadLP1_Door()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_Door);
        }

        public bool ReadLP1_EMO()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP1_EMO);
        }

        public bool ReadLP2_Door()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_Door);
        }

        public bool ReadLP2_EMO()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_LP2_EMO);
        }

        public bool ReadPowerON()
        {
            return this.Read<bool>(MacHalPlcEnumVariable.PLC_TO_PC_PowerON);
        }
        #endregion




        #region IDisposable
        // Flag: Has Dispose already been called?
        protected bool disposed = false;
        // Public implementation of Dispose pattern callable by consumers.
        public virtual void Dispose()
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
            this.Close();//若有 Close 呼叫 Close, 若沒有就呼叫 DisposeSelf
        }
        #endregion



        //=== Static ===============================================================================================

        #region Singleton Mapper
        /// <summary>
        /// Design Pattern - Singleton Pattern
        /// </summary>
        static Dictionary<string, MacHalPlcContext> m_mapper = new Dictionary<string, MacHalPlcContext>();
        public static MacHalPlcContext Get(string ip, string portid) { return Get(ip, Convert.ToInt32(portid)); }
        public static MacHalPlcContext Get(string ip, int portid)
        {
            var key = string.Format("{0}:{1}", ip, portid);

            if (!m_mapper.ContainsKey(key))
                m_mapper[key] = new MacHalPlcContext();

            var rtn = m_mapper[key];
            rtn.PlcIp = ip;
            rtn.PlcPortId = portid;
            return rtn;
        }
        #endregion


    }
}
