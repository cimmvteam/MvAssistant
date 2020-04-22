using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MvAssistant.DeviceDrive.FanucRobot;

namespace MaskTool.TestMy.Device
{
    public class MaskRobotHandler : IDisposable
    {
        public int PositionRecordInterval_MillSec = 7;
        public MvFanucRobotLdd ldd;
        public List<MvRobotAlarmInfo> alarmInfos;
        bool isRunning = false;

        public MaskRobotHandler()
        {
            ldd = new MvFanucRobotLdd();
            alarmInfos = new List<MvRobotAlarmInfo>();
        }
        ~MaskRobotHandler()
        {
            this.Close();
        }


        public void Close()
        {
            if (this.ldd != null)
                using (var obj = this.ldd)
                    this.ldd.Close();
        }

        public int ConnectIfNO() { return this.ldd.ConnectIfNo(); }



        public bool HasRobotAlarm()
        {
            var alarmmsg = String.Empty;
            var alarmInfo = new MvRobotAlarmInfo();
            if (this.ldd.HasRobotFault(ref alarmmsg, ref alarmInfo))
            {
                this.ldd.HasRobotFault(ref alarmmsg, ref alarmInfo);
                alarmInfos.Add(alarmInfo);
                return true;
            }
            else
                return false;
        }

        public bool IsConnected() { return this.ldd.IsConnected(); }

        public void KeepGetCurrentPos()
        {
            while (isRunning)
            {
                if (this.ldd.isUnderSystemRecoverAuto)
                {
                    Thread.Sleep(500);
                    continue;
                }
            }
            this.ldd.m_currRobotInfo = this.ldd.GetCurrRobotInfo();
            Thread.Sleep(PositionRecordInterval_MillSec);
        }


        #region SGS Verify

        public void SgsVerifyStartPns0101()
        {
            this.ldd.StopProgram();
            if (!this.ldd.ExecutePNS("PNS0101"))
                throw new Exception("Start PNS0101 Fail");


            var toBr = this.SgsVerifyGenHomeToBarcodeReader();
            var toCc = this.SgsVerifyGenHome2Cc2Os();

            var targets = new List<MvFanucRobotInfo>();
            targets.AddRange(toBr);
            var stack = new Stack<MvFanucRobotInfo>(toBr);
            targets.AddRange(stack.ToList());



            float[] target = new float[6];
            for (var idx = 0; idx < targets.Count; idx++)
            {
                var pose = targets[idx];

                target[0] = pose.x;
                target[1] = pose.y;
                target[2] = pose.z;
                target[3] = pose.w;
                target[4] = pose.p;
                target[5] = pose.r;
                this.ldd.Pns0103ContinuityMove(target);

            }



        }

        public void SgsVerifyStartPns0102(Action<MvFanucRobotInfo> waitEvent)
        {
            this.ldd.StopProgram();
            if (!this.ldd.ExecutePNS("PNS0102"))
                throw new Exception("Start Pns0102 Program Fail.");


            //--- Check at home ---
            var robotInfo = this.ldd.GetCurrRobotInfo();
            {
                var flagErrPos = robotInfo.x > 337 || robotInfo.x < 297;//317
                flagErrPos = robotInfo.y > 20 || robotInfo.y < -20;//0
                flagErrPos = robotInfo.z > 376 || robotInfo.z < 336;//356
                flagErrPos = !(robotInfo.w > 170 || robotInfo.w < -170);//180
                flagErrPos = robotInfo.p > 10 || robotInfo.p < -10;//0
                flagErrPos = robotInfo.r > 10 || robotInfo.r < -10;//0

                if (flagErrPos)
                    throw new Exception("Mask robot is not at home");
            }






            //--- Run ---

            this.ldd.Pns0102AsynRun();
            while (!this.ldd.Pns0102AsynEnd())
            {
                robotInfo = this.ldd.GetCurrRobotInfo();
                waitEvent(robotInfo);
                Thread.Sleep(500);
            }


        }

        public List<MvFanucRobotInfo> SgsVerifyGenHomeToBarcodeReader()
        {
            var poss = new List<MvFanucRobotInfo>();



            //Home
            poss.Add(new MvFanucRobotInfo()
            {
                x = 317,
                y = 0,
                z = 356,
                w = 179,
                p = 0,
                r = 0,
            });



            //between ic & lpa
            poss.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 215,
                z = 356,
                w = 179,
                p = 0,
                r = 45,
            });

            //ICxLPA 抬頭1動
            poss.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 301,
                z = 333,
                w = -157,
                p = -15,
                r = -21,
            });


            //ICxLPA 抬頭2動
            poss.Add(new MvFanucRobotInfo()
            {
                x = 215,
                y = 301,
                z = 333,
                w = -111,
                p = -33,
                r = -32,
            });

            //LPA 上方
            poss.Add(new MvFanucRobotInfo()
            {
                x = 244,
                y = 290,
                z = 495,
                w = 96,
                p = -89,
                r = -155,
            });


            //Barcode Reader
            poss.Add(new MvFanucRobotInfo()
            {
                x = -110,
                y = 247,
                z = 471,
                w = -79,
                p = -87,
                r = -12,
            });




            return poss;
        }
        public List<MvFanucRobotInfo> SgsVerifyGenHome2Cc2Os()
        {
            var poss = new List<MvFanucRobotInfo>();

            //Home
            poss.Add(new MvFanucRobotInfo()
            {
                x = 317,
                y = 0,
                z = 356,
                w = 179,
                p = 0,
                r = 0,
            });


            //Between IC & CC
            poss.Add(new MvFanucRobotInfo()
            {
                x = 223,
                y = -225,
                z = 356,
                w = -179,
                p = -0,
                r = -45,
            });

            //Front CC 低頭
            poss.Add(new MvFanucRobotInfo()
            {
                x = 0,
                y = -317,
                z = 356,
                w = 179,
                p = 0,
                r = -90,
            });



            //Front of CC 抬頭
            poss.Add(new MvFanucRobotInfo()
            {
                x = 0,
                y = -232,
                z = 211,
                w = 104,
                p = -78,
                r = -17,
            });




            return poss;
        }

        #endregion

        #region TeachingPosition

        public List<MvFanucRobotInfo> LPUpsideToOSPutMask()
        {
            var poss = new List<MvFanucRobotInfo>();

            //PR[54]-Load Port upside
            poss.Add(new MvFanucRobotInfo()
            {
                x = -1,
                y = 303,
                z = 190,
                w = 45,
                p = -89,
                r = -135,
            });

            //PR[55]-LoadPort前(未伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = -253,
                y = 303,
                z = 190,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[56]-LoadPort上方(伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = -634,
                y = 303,
                z = 190,
                w = 45,
                p = -89,
                r = -47,
            });

            //PE[57]-LoadPort上，取/放Mask右上一點點的位置
            poss.Add(new MvFanucRobotInfo()
            {
                x = -634,
                y = 303,
                z = 79,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[58]-LoadPort上，取/放Mask上面一點點的位置
            poss.Add(new MvFanucRobotInfo()
            {
                x = -635,
                y = 283,
                z = 79,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[59]-LoadPort上，取/放Mask的位置
            poss.Add(new MvFanucRobotInfo()
            {
                x = -634,
                y = 283,
                z = 63,
                w = 45,
                p = -89,
                r = -47,
            });

            return poss;
        }

        public List<MvFanucRobotInfo> OSGetMaskToLPUpside()
        {
            var poss = new List<MvFanucRobotInfo>();

            //PR[59]-LoadPort上，取/放Mask的位置
            poss.Add(new MvFanucRobotInfo()
            {
                x = -634,
                y = 283,
                z = 63,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[58]-LoadPort上，取/放Mask上面一點點的位置
            poss.Add(new MvFanucRobotInfo()
            {
                x = -635,
                y = 283,
                z = 79,
                w = 45,
                p = -89,
                r = -47,
            });

            //PE[57]-LoadPort上，取/放Mask右上一點點的位置
            poss.Add(new MvFanucRobotInfo()
            {
                x = -634,
                y = 303,
                z = 79,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[56]-LoadPort上方(伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = -634,
                y = 303,
                z = 190,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[55]-LoadPort前(未伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = -253,
                y = 303,
                z = 190,
                w = 45,
                p = -89,
                r = -47,
            });

            //PR[54]-Load Port upside
            poss.Add(new MvFanucRobotInfo()
            {
                x = -1,
                y = 303,
                z = 190,
                w = 45,
                p = -89,
                r = -135,
            });

            return poss;
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
