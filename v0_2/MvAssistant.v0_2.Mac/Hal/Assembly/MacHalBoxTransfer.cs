using MvAssistant.v0_2.Mac.Hal.CompCamera;
using MvAssistant.v0_2.Mac.Hal.CompLight;
using MvAssistant.v0_2.Mac.Hal.CompPlc;
using MvAssistant.v0_2.Mac.Hal.CompRobot;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace MvAssistant.v0_2.Mac.Hal.Assembly
{
    [GuidAttribute("53CD572B-CAB0-498A-8005-B56AB7ED745F")]
    public class MacHalBoxTransfer : MacHalAssemblyBase, IMacHalBoxTransfer
    {
        #region Device Components

        public IMacHalPlcBoxTransfer Plc { get { return (IMacHalPlcBoxTransfer)this.GetHalDevice(MacEnumDevice.boxtransfer_plc); } }
        public IHalRobot Robot { get { return (IHalRobot)this.GetHalDevice(MacEnumDevice.boxtransfer_robot_1); } }
        public IMacHalLight LightCircleGripper { get { return (IMacHalLight)this.GetHalDevice(MacEnumDevice.boxtransfer_light_1); } }
        public IHalCamera CameraOnGripper { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.boxtransfer_camera_gripper_1); } }

        #endregion Device Components

        #region Path test, 2020/05/25


        Dictionary<string, HalRobotMotion> btPathInfo = new Dictionary<string, HalRobotMotion>();

        DataTable readCSV(string filePath)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            DataTable dt = new DataTable();
            string[] aryLine = null;
            string[] tableHead = null;
            string strLine = "";
            int columnCount = 10;
            bool IsFirst = true;
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;

                    for (int i = 0; i < columnCount; i++)
                    {
                        tableHead[i] = tableHead[i].Replace("\"", "");
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j].Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[2] + "" + "DESC";
            }
            sr.Close();
            fs.Close();

            return dt;
        }

        /// <summary>
        /// 使用CSV檔Config Default Position
        /// </summary>
        /// <param name="CsvPath"></param>
        public void ReadPathInfo(string CsvPath)
        {
            DataTable pathDt = readCSV(CsvPath);
            for (int k = 1; k < pathDt.Rows.Count - 1; k++)
            {
                HalRobotMotion robotMotion = new HalRobotMotion();
                switch (pathDt.Rows[k][10].ToString())
                {
                    case "W":
                        robotMotion.MotionType = HalRobotEnumMotionType.Position;
                        robotMotion.X = float.Parse(pathDt.Rows[k][2].ToString());
                        robotMotion.Y = float.Parse(pathDt.Rows[k][3].ToString());
                        robotMotion.Z = float.Parse(pathDt.Rows[k][4].ToString());
                        robotMotion.W = float.Parse(pathDt.Rows[k][5].ToString());
                        robotMotion.P = float.Parse(pathDt.Rows[k][6].ToString());
                        robotMotion.R = float.Parse(pathDt.Rows[k][7].ToString());
                        robotMotion.E1 = float.Parse(pathDt.Rows[k][8].ToString());
                        robotMotion.Speed = int.Parse(pathDt.Rows[k][9].ToString());
                        btPathInfo.Add(pathDt.Rows[k][1].ToString(), robotMotion);
                        break;
                    case "J":
                        robotMotion.MotionType = HalRobotEnumMotionType.Joint;
                        robotMotion.J1 = float.Parse(pathDt.Rows[k][2].ToString());
                        robotMotion.J2 = float.Parse(pathDt.Rows[k][3].ToString());
                        robotMotion.J3 = float.Parse(pathDt.Rows[k][4].ToString());
                        robotMotion.J4 = float.Parse(pathDt.Rows[k][5].ToString());
                        robotMotion.J5 = float.Parse(pathDt.Rows[k][6].ToString());
                        robotMotion.J6 = float.Parse(pathDt.Rows[k][7].ToString());
                        robotMotion.J7 = float.Parse(pathDt.Rows[k][8].ToString());
                        robotMotion.Speed = int.Parse(pathDt.Rows[k][9].ToString());
                        btPathInfo.Add(pathDt.Rows[k][1].ToString(), robotMotion);
                        break;
                }
            }
        }

        /// <summary>
        /// 透過Position Name去對應到Dictionary中的HalRobotMotion物件
        /// </summary>
        /// <param name="pName"></param>
        public void MoveByDefaultPath(string pName)
        {
            try
            {
                if (btPathInfo.ContainsKey(pName))
                {
                    var position = btPathInfo[pName];
                    this.MoveAsync(position);
                }
                else
                    throw new ArgumentException("Position Name is not in Path csv file!!");

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>回到 Cabinet1 Home</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void BackCabinet1Home()
        {
            var position = new BoxTransferPathPositions().Cabinet1Home;
            this.MoveAsync(position);
        }



        /// <summary>轉到面對 Cabinet 1 的方向</summary>
        public void ChangeDirectionToFaceCabinet1()
        {
            this.ChangeDirectionToFaceCabinet(1);
        }
        /// <summary>轉到面對 Cabinet 2 方向</summary>
        public void ChangeDirectionToFaceCabinet2()
        {
            this.ChangeDirectionToFaceCabinet(2);
        }
        /// <summary>轉向面對指定的 Cabinet</summary>
        /// <param name="cabinetIndex">cabinet index</param>
        private void ChangeDirectionToFaceCabinet(int cabinetIndex)
        {
            var position = default(HalRobotMotion);
            if (cabinetIndex == 1)
            {
                position = new BoxTransferPathPositions().Cabinet1Home;
            }
            else if (cabinetIndex == 2)
            {
                position = new BoxTransferPathPositions().Cabinet2Home;
            }
            ChangeDirection(position);
        }

        /// <summary>轉向面對 Open Statge 的方向 </summary>
        public void ChangeDirectionToFaceOpenStage()
        {
            var position = new BoxTransferPathPositions().OpenStageHome;
            ChangeDirection(position);
        }

        private void ChangeDirection(HalRobotMotion targetPosition)
        { // TODO: 待討論
            var positionInst = new BoxTransferPathPositions();
            HalRobotMotion openStageHome = positionInst.OpenStageHome;
            HalRobotMotion cabinet1Home = positionInst.Cabinet1Home;
            HalRobotMotion cabinet2Home = positionInst.Cabinet2Home;
            bool Licence = false;
            string StartPosName = "";
            string EndPosName = "";
            #region 確認Robot是否在三個可以轉動方向的點位內，並確認目前在哪個方位
            var StartPosInfo = (this.Robot as HalRobotFanuc).ldd.GetCurrRobotInfo();
            if (
                    StartPosInfo.j1 <= openStageHome.J1 + 5 && StartPosInfo.j1 >= openStageHome.J1 - 5
                    && StartPosInfo.j2 <= openStageHome.J2 + 5 && StartPosInfo.j2 >= openStageHome.J2 - 5
                    && StartPosInfo.j3 <= openStageHome.J3 + 5 && StartPosInfo.j3 >= openStageHome.J3 - 5
                    && StartPosInfo.j4 <= openStageHome.J4 + 5 && StartPosInfo.j4 >= openStageHome.J4 - 5
                    && StartPosInfo.j5 <= openStageHome.J5 + 5 && StartPosInfo.j5 >= openStageHome.J5 - 5
                    && StartPosInfo.j6 <= openStageHome.J6 + 5 && StartPosInfo.j6 >= openStageHome.J6 - 5
                    && StartPosInfo.j7 <= openStageHome.J7 + 5 && StartPosInfo.j7 >= openStageHome.J7 - 5
                )
            {
                Licence = true; StartPosName = "OpenStage Home";
            }
            else if (
                    StartPosInfo.j1 <= cabinet1Home.J1 + 5 && StartPosInfo.j1 >= cabinet1Home.J1 - 5
                    && StartPosInfo.j2 <= cabinet1Home.J2 + 5 && StartPosInfo.j2 >= cabinet1Home.J2 - 5
                    && StartPosInfo.j3 <= cabinet1Home.J3 + 5 && StartPosInfo.j3 >= cabinet1Home.J3 - 5
                    && StartPosInfo.j4 <= cabinet1Home.J4 + 5 && StartPosInfo.j4 >= cabinet1Home.J4 - 5
                    && StartPosInfo.j5 <= cabinet1Home.J5 + 5 && StartPosInfo.j5 >= cabinet1Home.J5 - 5
                    && StartPosInfo.j6 <= cabinet1Home.J6 + 5 && StartPosInfo.j6 >= cabinet1Home.J6 - 5
                    && StartPosInfo.j7 <= cabinet1Home.J7 + 5 && StartPosInfo.j7 >= cabinet1Home.J7 - 5
                )
            {
                Licence = true; StartPosName = "Cabinet1 Home";
            }
            else if (
                 StartPosInfo.j1 <= cabinet2Home.J1 + 5 && StartPosInfo.j1 >= cabinet2Home.J1 - 5
                    && StartPosInfo.j2 <= cabinet2Home.J2 + 5 && StartPosInfo.j2 >= cabinet2Home.J2 - 5
                    && StartPosInfo.j3 <= cabinet2Home.J3 + 5 && StartPosInfo.j3 >= cabinet2Home.J3 - 5
                    && StartPosInfo.j4 <= cabinet2Home.J4 + 5 && StartPosInfo.j4 >= cabinet2Home.J4 - 5
                    && StartPosInfo.j5 <= cabinet2Home.J5 + 5 && StartPosInfo.j5 >= cabinet2Home.J5 - 5
                    && StartPosInfo.j6 <= cabinet2Home.J6 + 5 && StartPosInfo.j6 >= cabinet2Home.J6 - 5
                    && StartPosInfo.j7 <= cabinet2Home.J7 + 5 && StartPosInfo.j7 >= cabinet2Home.J7 - 5
                )
            {
                Licence = true; StartPosName = "Cabinet2 Home";
            }
            #endregion
            #region 確認終點
            if (targetPosition.J1 <= openStageHome.J1 + 5 && targetPosition.J1 >= openStageHome.J1 - 5
                && targetPosition.J2 <= openStageHome.J2 + 5 && targetPosition.J2 >= openStageHome.J2 - 5
                && targetPosition.J3 <= openStageHome.J3 + 5 && targetPosition.J3 >= openStageHome.J3 - 5
                && targetPosition.J4 <= openStageHome.J4 + 5 && targetPosition.J4 >= openStageHome.J4 - 5
                && targetPosition.J5 <= openStageHome.J5 + 5 && targetPosition.J5 >= openStageHome.J5 - 5
                && targetPosition.J6 <= openStageHome.J6 + 5 && targetPosition.J6 >= openStageHome.J6 - 5
                 && targetPosition.J7 <= openStageHome.J7 + 5 && targetPosition.J7 >= openStageHome.J7 - 5
                )
            {
                EndPosName = "OpenStage Target";
            }
            else if (targetPosition.J1 <= cabinet1Home.J1 + 5 && targetPosition.J1 >= cabinet1Home.J1 - 5
                && targetPosition.J2 <= cabinet1Home.J2 + 5 && targetPosition.J2 >= cabinet1Home.J2 - 5
                && targetPosition.J3 <= cabinet1Home.J3 + 5 && targetPosition.J3 >= cabinet1Home.J3 - 5
                && targetPosition.J4 <= cabinet1Home.J4 + 5 && targetPosition.J4 >= cabinet1Home.J4 - 5
                && targetPosition.J5 <= cabinet1Home.J5 + 5 && targetPosition.J5 >= cabinet1Home.J5 - 5
                && targetPosition.J6 <= cabinet1Home.J6 + 5 && targetPosition.J6 >= cabinet1Home.J6 - 5
                 && targetPosition.J7 <= cabinet1Home.J7 + 5 && targetPosition.J7 >= cabinet1Home.J7 - 5
                )
            {
                EndPosName = "Cabinet1 Target";
            }
            else if (targetPosition.J1 <= cabinet2Home.J1 + 5 && targetPosition.J1 >= cabinet2Home.J1 - 5
                && targetPosition.J2 <= cabinet2Home.J2 + 5 && targetPosition.J2 >= cabinet2Home.J2 - 5
                && targetPosition.J3 <= cabinet2Home.J3 + 5 && targetPosition.J3 >= cabinet2Home.J3 - 5
                && targetPosition.J4 <= cabinet2Home.J4 + 5 && targetPosition.J4 >= cabinet2Home.J4 - 5
                && targetPosition.J5 <= cabinet2Home.J5 + 5 && targetPosition.J5 >= cabinet2Home.J5 - 5
                && targetPosition.J6 <= cabinet2Home.J6 + 5 && targetPosition.J6 >= cabinet2Home.J6 - 5
                 && targetPosition.J7 <= cabinet2Home.J7 + 5 && targetPosition.J7 >= cabinet2Home.J7 - 5
                )
            {
                EndPosName = "Cabinet2 Target";
            }
            #endregion
            if (Licence == true)
            {
                if (StartPosName != EndPosName && EndPosName != "")
                {
                    /**
                    //如果目前位置不在InspCh且要移動的目的地也不是InspCh，則需要先經過InspCh點位再移動到目的地
                    if (StartPosName != "Inspection Chamber" && EndPosName != "Inspection Chamber")
                    {
                        RobotMove(PosToInspCh());
                        RobotMove(PosToAssembly);
                    }
                    else
                    {
                        RobotMove(PosToAssembly);
                    }
                   */
                    cabinet1Home.MotionType = targetPosition.MotionType = HalRobotEnumMotionType.Joint;
                    if (StartPosName != "Cabinet1 Home" && EndPosName != "Cabinet1 Target")
                    {
                        // RobotMove(PosToInspCh());
                        //RobotMove(PosToAssembly);
                        this.MoveAsync(cabinet1Home);
                        this.MoveAsync(targetPosition);
                    }
                    else
                    {
                        //RobotMove(PosToAssembly);
                        this.MoveAsync(targetPosition);
                    }


                }
                else
                    throw new Exception("Unknown end position !!");
            }
            else
                throw new Exception("Mask robot can not change direction. Because robot is not in the safe range now");
        }


        /// <summary>移到 Cabinet1(Drawer 1) 某個 Box</summary>
        /// <param name="boxIndex">Box 的索引</param>
        public void ForwardToCabinet1(int boxIndex)
        {
            this.ForwardToCabinet(1, boxIndex);
        }

        /// <summary>移到 Cabinet2(Drawer 2) 某個 Box</summary>
        /// <param name="boxIndex"> Box 索引</param>
        public void ForwardToCabinet2(int boxIndex)
        {
            this.ForwardToCabinet(2, boxIndex);
        }

        /// <summary>從Home 移動到 Cabinet 某個 Box</summary>
        /// <param name="cabinetIndex">Drawer index</param>
        /// <param name="boxIndex">Box index</param>
        /// <remarks>King, 2020/05/25 Add</remarks>
        private void ForwardToCabinet(int cabinetIndex, int boxIndex)
        {
            Func<HalRobotMotion> GetBoxPosition = () =>
            {
                HalRobotMotion halRobotMotion = null;
                if (cabinetIndex == 1)
                {
                    // 終點 
                    //  TODO: 要獲得實際的 Position
                    halRobotMotion = new HalRobotMotion
                    {
                        X = 3.3f,
                        Y = -438.782349f,
                        Z = 140.75882f,
                        W = 90.0f,
                        P = -90.0f,
                        R = 0.0f,
                        E1 = 517.0209f,
                        J1 = -90.0f,
                        J2 = 1.134951f,
                        J3 = -50.0000153f,
                        J4 = 0.0f,
                        J5 = 50.000002f,
                        J6 = 0.0f,
                        J7 = 517.0209f,
                        Speed = 60,
                        MotionType = HalRobotEnumMotionType.Position
                    };
                }
                else
                {

                }
                return halRobotMotion;
            };
            var positions = GetBoxPosition();
            this.MoveAsync(positions);
        }

        public void GotoStage1()
        {
            var position = new BoxTransferPathPositions().CabinetHomeToOpenStage01;
            this.MoveAsync(position);
        }

        /// <summary>回到 Cabinet 1 Home</summary>
        public void BackwardFromCabinet1()
        {
            var position = new BoxTransferPathPositions().Cabinet1Home;
            this.MoveAsync(position);
        }
        /// <summary>回到 Cabinet 2 Home</summary>
        public void BackwardFromCabinet2()
        {
            var position = new BoxTransferPathPositions().Cabinet2Home;
            this.MoveAsync(position);
        }
        /// <summary>移至 OpenStage</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void ForwardToOpenStage()
        {
            var position = new BoxTransferPathPositions().OpenStage;
            this.MoveAsync(position);

        }


        /// <summary>從Open Stage Back Home</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void BackwardFromOpenStage()
        {
            var position = new BoxTransferPathPositions().OpenStageHome;
            this.MoveAsync(position);
        }

        /// <summary>非同步移動到某個位置</summary>
        /// <param name="targetPosition">目標位置</param>
        /// <remarks>King, 2020/05/25 Add</remarks>
        private void MoveAsync(HalRobotMotion targetPosition)
        {
            this.Robot.HalMoveStraightAsyn(targetPosition);
            while (!this.Robot.HalMoveIsComplete())
            { Thread.Sleep(100); }
            this.Robot.HalMoveEnd();
        }

        /// <summary>非同步沿某一組點位資料移動</summary>
        /// <param name="targetPositions">點位資料集合</param>
        /// <remarks>King, 2020/05/25 Add</remarks>
        private void MoveAsync(List<HalRobotMotion> targetPositions)
        {
            foreach (var position in targetPositions)
            {
                this.MoveAsync(position);
            }
        }
        #endregion

        public int ExePathMove(string PathFileLocation)
        {
            var PathPosition = Robot.ReadMovePath(PathFileLocation);
            return Robot.ExePosMove(PathPosition);
        }

        /// <summary>
        /// 檢查當前位置與目標位置是否一致，點位允許誤差 ±5 
        /// </summary>
        /// <param name="PosFileLocation"></param>
        /// <returns></returns>
        public bool CheckPosition(string PosFileLocation)
        {
            var TargetPos = Robot.ReadMovePath(PosFileLocation)[0];

            var CurrentPosInfo = (this.Robot as HalRobotFanuc).ldd.GetCurrRobotInfo();
            {
                if (CurrentPosInfo.x <= TargetPos.X + 5 && CurrentPosInfo.x >= TargetPos.X - 5
                    && CurrentPosInfo.y <= TargetPos.Y + 5 && CurrentPosInfo.y >= TargetPos.Y - 5
                    && CurrentPosInfo.z <= TargetPos.Z + 5 && CurrentPosInfo.z >= TargetPos.Z - 5
                    && CurrentPosInfo.w <= TargetPos.W + 5 && CurrentPosInfo.w >= TargetPos.W - 5
                    && CurrentPosInfo.p <= TargetPos.P + 5 && CurrentPosInfo.p >= TargetPos.P - 5
                    && CurrentPosInfo.r <= TargetPos.R + 5 && CurrentPosInfo.r >= TargetPos.R - 5)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public void Reset()
        {
            if (Robot.HalReset() == -1)
                throw new Exception("Box Transfer reset failed.");
        }

        public void Recover()
        {
            Robot.HalSysRecover();
        }

        /// <summary>
        /// 夾取，1：鐵盒、2：水晶盒
        /// </summary>
        /// <param name="BoxType">1：鐵盒、2：水晶盒</param>
        /// <returns></returns>
        public string Clamp(uint BoxType)
        { return Plc.Clamp(BoxType); }

        public string Unclamp()
        { return Plc.Unclamp(); }

        public string Initial()
        { return Plc.Initial(); }

        /// <summary>
        /// 重置夾爪XY軸水平
        /// </summary>
        /// <returns></returns>
        public bool LevelReset()
        { return Plc.LevelReset(); }

        public string ReadBTRobotStatus()
        { return Plc.ReadBTRobotStatus(); }

        /// <summary>
        /// 當手臂作動或停止時，需要下指令讓PLC知道目前Robot是移動或靜止狀態
        /// </summary>
        /// <param name="isMoving">手臂是否要移動</param>
        public void RobotMoving(bool isMoving)
        { Plc.RobotMoving(isMoving); }

        #region Set Parameter
        /// <summary>
        /// 夾爪速度設定，單位(mm/sec)
        /// </summary>
        /// <param name="ClampSpeed">夾爪速度</param>
        public void SetSpeed(double ClampSpeed)
        { Plc.SetSpeed(ClampSpeed); }

        /// <summary>
        /// 設定夾爪間距的極限值，最小間距、最大間距
        /// </summary>
        /// <param name="Minimum">最小間距</param>
        /// <param name="Maximum">最大間距</param>
        public void SetHandSpaceLimit(double? Minimum, double? Maximum)
        { Plc.SetHandSpaceLimit(Minimum, Maximum); }

        /// <summary>
        /// 設定Clamp與Cabinet的最小間距限制
        /// </summary>
        /// <param name="Minimum">最小間距</param>
        public void SetClampToCabinetSpaceLimit(double Minimum)
        { Plc.SetClampToCabinetSpaceLimit(Minimum); }

        /// <summary>
        /// 設定水平Sensor的XY軸極限值，X軸水平極限、Y軸水平極限
        /// </summary>
        /// <param name="Level_X">X軸水平極限</param>
        /// <param name="Level_Y">Y軸水平極限</param>
        public void SetLevelSensorLimit(double? Level_X, double? Level_Y)
        { Plc.SetLevelSensorLimit(Level_X, Level_Y); }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力值上限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorUpperLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            Plc.SetSixAxisSensorUpperLimit(Fx, Fy, Fz, Mx, My, Mz);
            Thread.Sleep(100);
            var SetResult = Plc.ReadSixAxisSensorUpperLimitSetting();
            if ((Fx != null && SetResult.Item1 != Fx)
                || (Fy != null && SetResult.Item2 != Fy)
                || (Fz != null && SetResult.Item3 != Fz)
                || (Mx != null && SetResult.Item4 != Mx)
                || (My != null && SetResult.Item5 != My)
                || (Mz != null && SetResult.Item6 != Mz))
                throw new Exception("BT six axis sensor upper limit setting error !");
        }

        /// <summary>
        /// 設定六軸力覺Sensor的壓力值下限
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorLowerLimit(double? Fx, double? Fy, double? Fz, double? Mx, double? My, double? Mz)
        {
            Plc.SetSixAxisSensorLowerLimit(Fx, Fy, Fz, Mx, My, Mz);
            Thread.Sleep(100);
            var SetResult = Plc.ReadSixAxisSensorLowerLimitSetting();
            if ((Fx != null && SetResult.Item1 != Fx)
                || (Fy != null && SetResult.Item2 != Fy)
                || (Fz != null && SetResult.Item3 != Fz)
                || (Mx != null && SetResult.Item4 != Mx)
                || (My != null && SetResult.Item5 != My)
                || (Mz != null && SetResult.Item6 != Mz))
                throw new Exception("BT six axis sensor lower limit setting error !");
        }
        #endregion

        #region Read Parameter
        /// <summary>
        /// 讀取夾爪速度設定
        /// </summary>
        /// <returns></returns>
        public double ReadSpeedSetting()
        { return Plc.ReadSpeedSetting(); }

        /// <summary>
        /// 讀取夾爪間距的極限值設定，最小夾距、最大夾距
        /// </summary>
        /// <returns>最小夾距、最大夾距</returns>
        public Tuple<double, double> ReadHandSpaceLimitSetting()
        { return Plc.ReadHandSpaceLimitSetting(); }

        /// <summary>
        /// 讀取Clamp與Cabinet的最小間距設定
        /// </summary>
        /// <returns>最小間距</returns>
        public double ReadClampToCabinetSpaceLimitSetting()
        { return Plc.ReadClampToCabinetSpaceLimitSetting(); }

        /// <summary>
        /// 設定XY軸水平Sensor限制，X軸水平限制、Y軸水平限制
        /// </summary>
        /// <returns>X軸水平限制、Y軸水平限制</returns>
        public Tuple<double, double> ReadLevelSensorLimitSetting()
        { return Plc.ReadLevelSensorLimitSetting(); }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值上限設定
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorUpperLimitSetting()
        { return Plc.ReadSixAxisSensorUpperLimitSetting(); }

        /// <summary>
        /// 讀取六軸力覺Sensor的壓力值下限設定
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double, double, double, double, double> ReadSixAxisSensorLowerLimitSetting()
        { return Plc.ReadSixAxisSensorLowerLimitSetting(); }
        #endregion

        #region Read Component Value
        /// <summary>
        /// 讀取軟體記憶的夾爪位置
        /// </summary>
        /// <returns></returns>
        public double ReadHandPos()
        { return Plc.ReadHandPos(); }

        /// <summary>
        /// 讀取夾爪前方是否有Box
        /// </summary>
        /// <returns></returns>
        public bool ReadBoxDetect()
        { return Plc.ReadBoxDetect(); }

        /// <summary>
        /// 讀取由雷射檢測的夾爪位置
        /// </summary>
        /// <returns></returns>
        public double ReadHandPosByLSR()
        { return Plc.ReadHandPosByLSR(); }

        /// <summary>
        /// 讀取Clamp前方物體距離
        /// </summary>
        /// <returns></returns>
        public double ReadClampDistance()
        { return Plc.ReadClampDistance(); }

        /// <summary>
        /// 讀取XY軸水平Sensor目前數值，X軸水平數值、Y軸水平數值
        /// </summary>
        /// <returns>X軸水平數值、Y軸水平數值</returns>
        public Tuple<double, double> ReadLevelSensor()
        { return Plc.ReadLevelSensor(); }

        /// <summary>
        /// 讀取六軸力覺Sensor數值
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensor()
        { return Plc.ReadSixAxisSensor(); }

        /// <summary>
        /// 確認Hand吸塵狀態
        /// </summary>
        /// <returns></returns>
        public bool ReadHandVacuum()
        { return Plc.ReadHandVacuum(); }

        public bool ReadBT_FrontLimitSenser()
        { return ReadBT_FrontLimitSenser(); }

        public bool ReadBT_RearLimitSenser()
        { return ReadBT_RearLimitSenser(); }
        #endregion

        public void LightForGripper(int value)
        {
            LightCircleGripper.TurnOn(value);
        }

        public Bitmap Camera_Cap()
        {
            return CameraOnGripper.Shot();
        }

        public void Camera_CapToSave(string SavePath, string FileType)
        {
            CameraOnGripper.ShotToSaveImage(SavePath, FileType);
        }
    }

    /// <summary>Path Test Position Collection</summary>
    /// <remarks>King, 2020/05/15 Add</remarks>
    public class BoxTransferPathPositions
    {

        /// <summary>Open Statge 的 Home 點</summary>
        public HalRobotMotion OpenStageHome
        {
            get
            {
                var position = new HalRobotMotion
                { // TODO: 加入實際 World 及 Joint 點位 

                };
                return position;
            }

        }

        /// <summary>Drawer 2 的 Home 點</summary>
        public HalRobotMotion Cabinet2Home
        {
            get
            {
                var position = new HalRobotMotion
                { // TODO: 加入實際 World 及 Joint 點位
                    X = 527.898438f,
                    Y = -9.219327f,
                    Z = 38.3116455f,
                    W = 147.213242f,
                    P = -86.85704f,
                    R = 32.5399f,
                    E1 = 414.897583f,
                    J1 = -1.15107226f,
                    J2 = 22.9280548f,
                    J3 = -64.14249f,
                    J4 = 0.983132958f,
                    J5 = 61.5038452f,
                    J6 = 1.27394938f,
                    J7 = 414.897583f,
                    Speed = 60,
                    MotionType = HalRobotEnumMotionType.Position
                };
                return position;
            }
        }

        /// <summary>Drawer 1 的 Home 點</summary>
        public HalRobotMotion Cabinet1Home
        {
            get
            {
                var position = new HalRobotMotion
                {
                    // TODO: 加上 World 座標資料 
                    J1 = -89.99923f,
                    J2 = 0.000476679532f,
                    J3 = -50.0014839f,
                    J4 = -0.000300816144f,
                    J5 = 49.9995537f,
                    J6 = -0.00238952623f,
                    J7 = 517.0201f,
                    X = 0.00552143529f,
                    Y = -438.778259f,
                    Z = 140.748138f,
                    W = -126.767067f,
                    P = -89.99678f,
                    R = -143.2324f,
                    E1 = 517.0201f,
                    Speed = 50,
                    MotionType = HalRobotEnumMotionType.Joint,


                };
                // TODO: 設定 Home 點位
                return position;
            }
        }


        public List<HalRobotMotion> CabinetHomeToOpenStage01
        {

            get
            {

                var position = new List<HalRobotMotion>();
                position.Add(Cabinet1Home);
                position.Add(
                    new HalRobotMotion
                    {
                        X = -380.6985f,
                        Y = -690.6075f,
                        Z = 181.8245f,
                        W = -0.563894749f,
                        P = -88.21404f,
                        R = 25.5681438f,
                        E1 = 517.0209f,
                        J1 = -114.525215f,
                        J2 = 39.1008873f,
                        J3 = -27.4424839f,
                        J4 = -59.767025f,
                        J5 = 48.666275f,
                        J6 = 50.079895f,
                        J7 = 517.0209f,
                        MotionType = HalRobotEnumMotionType.Position,
                        Speed = 60,
                    });
                return position;
            }
        }

        /// <summary>從 Home 到 OpenStage 點位 </summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public HalRobotMotion OpenStage
        {
            get
            {
                HalRobotMotion position = new HalRobotMotion();
                // TODO: 加入 Home 到 OpenStage點位資料
                return position;
            }
        }

    }


}
