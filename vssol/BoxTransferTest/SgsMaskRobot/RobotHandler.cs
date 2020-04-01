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

        public void Close()
        {
            this.ldd.Close();
        }

        public int ConnectIfNO()
        {
            if (this.ldd.IsConnected()) return 0;
          
            return this.ldd.ReConnect();
        }

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

        public bool IsConnected()
        {
            return this.ldd.IsConnected();
        }

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

        public void ExecuteMove(List<float[]> Targets, int Continuity, int[] fineTargetIndex)
        {
            fineTargetIndex.Distinct().ToArray(); //Remove repeat index 防呆用
            Array.Sort(fineTargetIndex);

            int speed = 500;
            int MotionType = 1; //0:Offset; 1:Postion; 2:Joint
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
                    this.ldd.SwitchUT(MoveFrame);
                    List<float[]> tmpTargets = new List<float[]>();
                    if (finTarget.Value == true)
                    {
                        foreach (var targetIndex in finTarget.Key)
                        {
                            tmpTargets.Add(Targets[targetIndex]);
                        }
                        this.ldd.MoveStraightAsync(tmpTargets, Continuity, CorJ, OfsOrPos, IsMoveTCP, speed);
                    }
                    else
                    {
                        this.ldd.MoveStraightAsync(tmpTargets, 0, CorJ, OfsOrPos, IsMoveTCP, speed);
                    }
                    tmpTargets.Clear();
                }
            }
            else if (fineTargetIndex.Length == 1)
            {
                List<float[]> tmpTargets = new List<float[]>();
                tmpTargets.Add(Targets[0]);
                this.ldd.SwitchUT(MoveFrame);
                this.ldd.MoveStraightAsync(tmpTargets, 0, CorJ, OfsOrPos, IsMoveTCP, speed);
                tmpTargets.Clear();
            }
            else
            {
                this.ldd.SwitchUT(MoveFrame);
                this.ldd.MoveStraightAsync(Targets, Continuity, CorJ, OfsOrPos, IsMoveTCP, speed);
            }
        }

        public void ExecuteMove(float[] target)
        {
            float[] pos = target;
            List<float[]> targets = new List<float[]>();
            targets.Add(pos);
            ExecuteMove(targets, 0, new int[] { 1 });
            targets.Clear();
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
            for (var idx = toBr.Count-1; idx >=0; idx--)
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


            for (var idx = 0; idx < toCc.Count; idx++)
            {
                var pose = toCc[idx];

                target[0] = pose.x;
                target[1] = pose.y;
                target[2] = pose.z;
                target[3] = pose.w;
                target[4] = pose.p;
                target[5] = pose.r;
                this.ExecuteMove(target);

            }
            for (var idx = toCc.Count - 1; idx >= 0; idx--)
            {
                var pose = toCc[idx];

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
                x = 215,
                y = 301,
                z = 333,
                w = -4,
                p = -57,
                r = -121,
            });


            //Barcode Reader
            poss.Add(new MvFanucRobotInfo()
            {
                x = -110,
                y = 247,
                z = 471,
                w = -79,
                p = -87,
                r = -26,
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
