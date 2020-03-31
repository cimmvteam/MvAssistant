﻿using MvAssistant.DeviceDrive.OmronPlc;
using MvAssistant.Tasking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcContext : IDisposable
    {

        public MvOmronPlcLdd PlcLdd;
        bool m_isConnected = false;

        MvCancelTask m_keepConnection;


        public bool IsConnected { get { return m_isConnected; } }

        public MvPlcInspCh InspCh;
        public MvPlcBoxRobot BoxRobot;
        public MvPlcMaskRobot MaskRobot;
        public MvPlcOpenStage OpenStage;
        public MvPlcCabinet Cabinet;
        public MvPlcCleanCh CleanCh;
        public MvPlcLoadPort LoadPort;

        public MvPlcContext()
        {
            this.InspCh = new MvPlcInspCh(this);
            this.BoxRobot = new MvPlcBoxRobot(this);
            this.MaskRobot = new MvPlcMaskRobot(this);
            this.OpenStage = new MvPlcOpenStage(this);
            this.Cabinet = new MvPlcCabinet(this);
            this.CleanCh = new MvPlcCleanCh(this);
            this.LoadPort = new MvPlcLoadPort(this);

            this.PlcLdd = new MvOmronPlcLdd();
            this.PlcLdd.NLPLC_Initial("192.168.0.200", 2);

        }
        ~MvPlcContext() { this.Dispose(false); }



        public T Read<T>(MvEnumPlcVariable plcvar)
        {
            var obj = this.PlcLdd.Read(plcvar.ToString());

            return (T)obj;
        }
        public void Write(MvEnumPlcVariable plcvar, object data)
        {
            this.PlcLdd.Write(plcvar.ToString(), data);
        }


        void LockAssign<T>(ref T prop, T val)
        {
            lock (this)
            {
                prop = val;
            }
        }

        public int StartAsyn()
        {
         

            this.m_keepConnection = MvCancelTask.RunLoop(() =>
            {

                this.Write(MvEnumPlcVariable.PC_TO_PLC_CheckClock, false);
                if (!SpinWait.SpinUntil(() => !this.Read<bool>(MvEnumPlcVariable.PC_TO_PLC_CheckClock_Reply), 2500))
                {
                    this.LockAssign(ref this.m_isConnected, false);
                    return false;
                }
                this.LockAssign(ref this.m_isConnected, true);

                this.Write(MvEnumPlcVariable.PC_TO_PLC_CheckClock, true);
                if (!SpinWait.SpinUntil(() => this.Read<bool>(MvEnumPlcVariable.PC_TO_PLC_CheckClock_Reply), 2500))
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



        public void Close()
        {
            using (var obj = this.PlcLdd)
            {
            }

            using (var obj = this.m_keepConnection)
            {
                obj.Cancel();
                SpinWait.SpinUntil(() => obj.IsEnd(), 1000);
            }
        }

        public Tuple<double, double, double, double, double, double> HandInspection()
        {
            return new Tuple<double, double, double, double, double, double>(
            this.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser1),
            this.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser2),
            this.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser3),
            this.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser4),
            this.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser5),
            this.Read<double>(MvEnumPlcVariable.LD_TO_PC_Laser6)
            );
        }

        //信號燈
        public void SignalTower(bool Red, bool Orange, bool Blue)
        {
            this.Write(MvEnumPlcVariable.PC_TO_DR_Red, Red);
            this.Write(MvEnumPlcVariable.PC_TO_DR_Orange, Orange);
            this.Write(MvEnumPlcVariable.PC_TO_DR_Blue, Blue);
        }

        //蜂鳴器
        public void Buzzer(uint BuzzerType)
        {
            this.Write(MvEnumPlcVariable.PC_TO_DR_Buzzer, BuzzerType);
        }

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
            this.Close();
        }



        #endregion


    }
}
