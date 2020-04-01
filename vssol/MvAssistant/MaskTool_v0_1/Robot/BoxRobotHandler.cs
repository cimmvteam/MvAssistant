using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MvAssistant.DeviceDrive.FanucRobot;

namespace MvAssistant.MaskTool_v0_1.Robot
{
    public class BoxRobotHandler
    {
        public int PositionRecordInterval_MillSec = 7;
        public MvFanucRobotLdd ldd;
        public List<MvRobotAlarmInfo> alarmInfos;
        public CurrentPOS curPos;
        bool isRunning = false;


        public BoxRobotHandler()
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
            this.ldd.RobotIp = "192.168.0.50";
            bool flagConnection = false;
            this.ldd.ReConnect();
            if (ldd.IsConnected())
            {
                flagConnection = true;
            }
            return 0;
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
            curPos.CurrentE1 = info.e1;
            curPos.CurrentJ1 = info.j1;
            curPos.CurrentJ2 = info.j2;
            curPos.CurrentJ3 = info.j3;
            curPos.CurrentJ4 = info.j4;
            curPos.CurrentJ5 = info.j5;
            curPos.CurrentJ6 = info.j6;
            curPos.CurrentJ7 = info.j7;
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
                        this.ldd.Pns0101MoveStraightAsync(tmpTargets, Continuity, CorJ, OfsOrPos, IsMoveTCP, speed);
                    }
                    else
                    {
                        this.ldd.Pns0101MoveStraightAsync(tmpTargets, 0, CorJ, OfsOrPos, IsMoveTCP, speed);
                    }
                    tmpTargets.Clear();
                }
            }
            else if (fineTargetIndex.Length == 1)
            {
                List<float[]> tmpTargets = new List<float[]>();
                tmpTargets.Add(Targets[0]);
                this.ldd.SwitchUT(MoveFrame);
                this.ldd.Pns0101MoveStraightAsync(tmpTargets, 0, CorJ, OfsOrPos, IsMoveTCP, speed);
                tmpTargets.Clear();
            }
            else
            {
                this.ldd.SwitchUT(MoveFrame);
                this.ldd.Pns0101MoveStraightAsync(Targets, Continuity, CorJ, OfsOrPos, IsMoveTCP, speed);
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
    }
}
