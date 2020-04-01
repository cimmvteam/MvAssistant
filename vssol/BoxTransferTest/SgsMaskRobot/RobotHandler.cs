using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MvAssistant.DeviceDrive.FanucRobot;

namespace MaskTool.TestMy.Device
{
    public class RobotHandler
    {
        public int PositionRecordInterval_MillSec = 7;
        public MvFanucRobotLdd ldd;
        public List<MvRobotAlarmInfo> alarmInfos;
        public CurrentPOS curPos;
        bool isRunning = false;

        public RobotHandler()
        {
            ldd = new MvFanucRobotLdd();
            alarmInfos = new List<MvRobotAlarmInfo>();
        }
        ~RobotHandler()
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

        public void getCurrentPOS()
        {
            var info = this.ldd.GetCurrRobotInfo();
            curPos = new CurrentPOS();
            curPos.CurrentX = info.x;
            curPos.CurrentY = info.y;
            curPos.CurrentZ = info.z;
            curPos.CurrentW = info.w;
            curPos.CurrentP = info.p;
            curPos.CurrentR = info.r;
            //curPos.CurrentE1 = info.e1;
            curPos.CurrentJ1 = info.j1;
            curPos.CurrentJ2 = info.j2;
            curPos.CurrentJ3 = info.j3;
            curPos.CurrentJ4 = info.j4;
            curPos.CurrentJ5 = info.j5;
            curPos.CurrentJ6 = info.j6;
            //curPos.CurrentJ7 = info.j7;
            curPos.UserFrame = info.userFrame;
            curPos.UserTool = info.userTool;
        }

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

     
        public void StartSgsVerify()
        {
            var toBr = this.GenHomeToBarcodeReader();
            var toCc = this.GenHome2Cc2Os();


            float[] target = new float[6];
            for (var idx = 0; idx < toBr.Count; idx++)
            {
                var pose = toBr[idx];

                target[0] = pose.x;
                target[1] = pose.y;
                target[2] = pose.z;
                target[3] = pose.w;
                target[4] = pose.p;
                target[5] = pose.r;
                this.ExecuteMove(target);

            }
            for (var idx = toBr.Count - 1; idx >= 0; idx--)
            {
                var pose = toBr[idx];

                target[0] = pose.x;
                target[1] = pose.y;
                target[2] = pose.z;
                target[3] = pose.w;
                target[4] = pose.p;
                target[5] = pose.r;
                this.ExecuteMove(target);

            }









        }



        public List<MvFanucRobotInfo> GenHomeToBarcodeReader()
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
        public List<MvFanucRobotInfo> GenHome2Cc2Os()
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






    }
}
