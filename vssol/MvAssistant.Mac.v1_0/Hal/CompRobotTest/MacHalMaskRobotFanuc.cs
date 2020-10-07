using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MvAssistant.DeviceDrive.FanucRobot_v42_15;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
//using MvAssistant.DeviceDrive.FanucRobot_v42_15;

namespace MvAssistant.Mac.v1_0.Hal.CompRobotTest
{
    public class MacHalMaskRobotFanuc : IDisposable
    {
        public int PositionRecordInterval_MillSec = 7;
        public MvFanucRobotLdd ldd;
        public List<MvRobotAlarm> alarmInfos;
        bool isRunning = false;

        public MacHalMaskRobotFanuc()
        {
            ldd = new MvFanucRobotLdd();
            alarmInfos = new List<MvRobotAlarm>();
        }
        ~MacHalMaskRobotFanuc()
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
            var alarmInfo = new MvRobotAlarm();
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

   

        public void MoveShift()
        {
            var target = new float[] { 0, 0, 5, 0, 0, 0 };

            this.ldd.Pns0101MoveStraightSync(target, 0, 0, 0, 20);
        }


        #region SGS Verify

        //public void SgsVerifyStartPns0101()
        //{
        //    this.ldd.StopProgram();
        //    if (!this.ldd.ExecutePNS("PNS0101"))
        //        throw new Exception("Start PNS0101 Fail");


        //    var toBr = this.SgsVerifyGenHomeToBarcodeReader();
        //    var toCc = this.SgsVerifyGenHome2Cc2Os();

        //    var targets = new List<MvFanucRobotInfo>();
        //    targets.AddRange(toBr);
        //    var stack = new Stack<MvFanucRobotInfo>(toBr);
        //    targets.AddRange(stack.ToList());



        //    float[] target = new float[6];
        //    for (var idx = 0; idx < targets.Count; idx++)
        //    {
        //        var pose = targets[idx];

        //        target[0] = pose.x;
        //        target[1] = pose.y;
        //        target[2] = pose.z;
        //        target[3] = pose.w;
        //        target[4] = pose.p;
        //        target[5] = pose.r;
        //        this.ldd.Pns0103ContinuityMove(target);

        //    }



        //}




        public void SgsVerifyStartPns0102(Action<MvFanucRobotInfo> waitEvent)
        {
            this.ldd.StopProgram();
            if (!this.ldd.ExecutePNS("PNS0102"))
                throw new Exception("Start Pns0102 Program Fail.");


            //--- Check at home ---
            var robotInfo = this.ldd.GetCurrRobotInfo();
            //{
            //    var flagErrPos = robotInfo.x > 337 || robotInfo.x < 297;//317
            //    flagErrPos = robotInfo.y > 20 || robotInfo.y < -20;//0
            //    flagErrPos = robotInfo.z > 376 || robotInfo.z < 336;//356
            //    flagErrPos = !(robotInfo.w > 170 || robotInfo.w < -170);//180
            //    flagErrPos = robotInfo.p > 10 || robotInfo.p < -10;//0
            //    flagErrPos = robotInfo.r > 10 || robotInfo.r < -10;//0

            //    if (flagErrPos)
            //        throw new Exception("Mask robot is not at home");
            //}






            //--- Run ---

            this.ldd.Pns0102AsynRun();
            while (!this.ldd.Pns0102AsynEnd())
            {
                robotInfo = this.ldd.GetCurrRobotInfo();
                waitEvent(robotInfo);
                Thread.Sleep(500);
            }


        }

        public void MaskRobotMove(List<MvFanucRobotInfo> PathPosition)
        {
            this.ldd.StopProgram();
            if (!this.ldd.ExecutePNS("PNS0101"))
                throw new Exception("Start PNS0101 Fail");

            var targets = new List<MvFanucRobotInfo>();
            targets.AddRange(PathPosition);
            float[] target = new float[6];
            var speed = 200;
            int IsTcpMove = 0;
            int CorJ, OfsOrPos;

            for (int idx = 0; idx < targets.Count; idx++)
            {
                var pose = targets[idx];

                target[0] = pose.x;
                target[1] = pose.y;
                target[2] = pose.z;
                target[3] = pose.w;
                target[4] = pose.p;
                target[5] = pose.r;
                speed = pose.Speed;

                switch (pose.MotionType)
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

                this.ldd.Pns0101MoveStraightAsync(target, CorJ, OfsOrPos, IsTcpMove, speed);
            }

            //var targets = new List<MvFanucRobotInfo>();
            //targets.AddRange(PathPosition);
            //var stack = new Stack<MvFanucRobotInfo>(PathPosition);
            //targets.AddRange(stack.ToList());

            //float[] target = new float[6];
            //for (var idx = 0; idx < targets.Count; idx++)
            //{
            //    var pose = targets[idx];

            //    target[0] = pose.x;
            //    target[1] = pose.y;
            //    target[2] = pose.z;
            //    target[3] = pose.w;
            //    target[4] = pose.p;
            //    target[5] = pose.r;
            //    this.ldd.Pns0103ContinuityMove(target);

            //}

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
                x = (float)-1.287,
                y = (float)302.844,
                z = (float)189.852,
                w = (float)45.266,
                p = (float)-88.801,
                r = (float)-135.369,
                MotionType = 1,
                Speed = 200
            });

            //PR[56]-LoadPort前(未伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = (float)-422.038,
                y = (float)305.272,
                z = (float)181.435,
                w = (float)7.339,
                p = (float)-88.870,
                r = (float)-8.811,
                MotionType = 1,
                Speed = 100
            });

            //PR[57]-LoadPort上方(伸出手臂)
            poss.Add(new MvFanucRobotInfo()
            {
                x = (float)-637.878,
                y = (float)305.272,
                z = (float)181.435,
                w = (float)7.339,
                p = (float)-88.870,
                r = (float)-8.810,
                MotionType = 1,
                Speed = 20
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
