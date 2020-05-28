using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Camera;
using MvAssistant.Mac.v1_0.Hal.Component.Force6Axis;
using MvAssistant.Mac.v1_0.Hal.Component.Gripper;
using MvAssistant.Mac.v1_0.Hal.Component.Robot;
using MvAssistant.Mac.v1_0.Hal.CompPlc;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [GuidAttribute("53CD572B-CAB0-498A-8005-B56AB7ED745F")]
    public class MacHalBoxTransfer : MacHalAssemblyBase, IMacHalBoxTransfer
    {
        #region Device Components



        public IMacHalPlcBoxTransfer Plc { get { return (IMacHalPlcBoxTransfer)this.GetHalDevice(MacEnumDevice.boxtransfer_plc); } }


        public IHalRobot Robot { get { return (IHalRobot)this.GetHalDevice(MacEnumDevice.boxtransfer_robot_1); } }
        public IHalForce6Axis Force6Axis { get { return (IHalForce6Axis)this.GetHalDevice(MacEnumDevice.boxtransfer_force_6axis_sensor_1); } }
        public IHalCamera Camera_BoxSlot_Direction { get { return (IHalCamera)this.GetHalDevice(MacEnumDevice.boxtransfer_ccd_gripper_1); } }
        public IHalLight CameraCircleLight { get { return (IHalLight)this.GetHalDevice(MacEnumDevice.boxtransfer_ringlight_1); } }
        public IHalGripper Gripper { get { return (IHalGripper)this.GetHalDevice(MacEnumDevice.boxtransfer_gripper_1); } }
        public IHalRobotSkin RobotSkin { get { return (IHalRobotSkin)this.GetHalDevice(MacEnumDevice.boxtransfer_robot_skin_1); } }
        public IHalLaser Laser_BoxSlot_Z { get { return (IHalLaser)this.GetHalDevice(MacEnumDevice.boxtransfer_laser_gripper_1); } }




        #endregion Device Components

        #region Path test, 2020/05/25

        /// <summary>回到 Home</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void BackDrawer1HomeFromAnyWhere()
        {
            var position = new BoxTransferPathPasitions().Drawer1Home;
            this.MoveAsync(position);
        }

        /// <summary>轉向面對 Drawer </summary>
        /// <param name="drawerIndex">Drawer index</param>
        public void ChangeDirectionToFaceDrawer(int drawerIndex)
        { 
            var position = default(HalRobotMotion);
            if (drawerIndex == 1)
            {
                position = new BoxTransferPathPasitions().FaceDrawer1;
            }
            else if(drawerIndex==2)
            {
                position = new BoxTransferPathPasitions().FaceDrawer2;
            }
            ChangeDirection(position);
        }
        private void ChangeDirectionToDrawer1(List<HalRobotMotion> posirions)
        {

        }
        private void ChangeDirection(HalRobotMotion targetPosition)
        { // TODO: 待討論
            var positionInst = new BoxTransferPathPasitions();
            HalRobotMotion openStageHome = positionInst.OpenStageHome;
            HalRobotMotion drawer1Home = positionInst.Drawer1Home;
            HalRobotMotion faceDrawer2 = positionInst.Drawer2Home;
            bool Licence = false;
            string StartPosName = "";
            string EndPosName = "";
            #region 確認Robot是否在三個可以轉動方向的點位內，並確認目前在哪個方位
            var StasrtPosInfo = (this.Robot as HalRobotFanuc).ldd.GetCurrRobotInfo();
            if (
                    StasrtPosInfo.j1 <= openStageHome.J1 + 5 && StasrtPosInfo.j1 >= openStageHome.J1 - 5
                    && StasrtPosInfo.j2 <= openStageHome.J2 + 5 && StasrtPosInfo.j2 >= openStageHome.J2 - 5
                    && StasrtPosInfo.j3 <= openStageHome.J3 + 5 && StasrtPosInfo.j3 >= openStageHome.J3 - 5
                    && StasrtPosInfo.j4 <= openStageHome.J4 + 5 && StasrtPosInfo.j4 >= openStageHome.J4 - 5
                    && StasrtPosInfo.j5 <= openStageHome.J5 + 5 && StasrtPosInfo.j5 >= openStageHome.J5 - 5
                    && StasrtPosInfo.j6 <= openStageHome.J6 + 5 && StasrtPosInfo.j6 >= openStageHome.J6 - 5
                )
            {
                Licence = true; StartPosName = "OpenStage Home";
            }
            else if (
                    StasrtPosInfo.j1 <= drawer1Home.J1 + 5 && StasrtPosInfo.j1 >= drawer1Home.J1 - 5
                    && StasrtPosInfo.j2 <= drawer1Home.J2 + 5 && StasrtPosInfo.j2 >= drawer1Home.J2 - 5
                    && StasrtPosInfo.j3 <= drawer1Home.J3 + 5 && StasrtPosInfo.j3 >= drawer1Home.J3 - 5
                    && StasrtPosInfo.j4 <= drawer1Home.J4 + 5 && StasrtPosInfo.j4 >= drawer1Home.J4 - 5
                    && StasrtPosInfo.j5 <= drawer1Home.J5 + 5 && StasrtPosInfo.j5 >= drawer1Home.J5 - 5
                    && StasrtPosInfo.j6 <= drawer1Home.J6 + 5 && StasrtPosInfo.j6 >= drawer1Home.J6 - 5
                )
            {
                Licence = true; StartPosName = "Drawer1 Home";
            }
            else if (
                 StasrtPosInfo.j1 <= faceDrawer2.J1 + 5 && StasrtPosInfo.j1 >= faceDrawer2.J1 - 5
                    && StasrtPosInfo.j2 <= faceDrawer2.J2 + 5 && StasrtPosInfo.j2 >= faceDrawer2.J2 - 5
                    && StasrtPosInfo.j3 <= faceDrawer2.J3 + 5 && StasrtPosInfo.j3 >= faceDrawer2.J3 - 5
                    && StasrtPosInfo.j4 <= faceDrawer2.J4 + 5 && StasrtPosInfo.j4 >= faceDrawer2.J4 - 5
                    && StasrtPosInfo.j5 <= faceDrawer2.J5 + 5 && StasrtPosInfo.j5 >= faceDrawer2.J5 - 5
                    && StasrtPosInfo.j6 <= faceDrawer2.J6 + 5 && StasrtPosInfo.j6 >= faceDrawer2.J6 - 5
                )
            {
                Licence = true; StartPosName = "Drawer2 Home";
            }
            #endregion
            #region 確認終點
            if (targetPosition.J1 <= openStageHome.J1 + 5 && targetPosition.J1 >= openStageHome.J1 - 5
                && targetPosition.J2 <= openStageHome.J2 + 5 && targetPosition.J2 >= openStageHome.J2 - 5
                && targetPosition.J3 <= openStageHome.J3 + 5 && targetPosition.J3 >= openStageHome.J3 - 5
                && targetPosition.J4 <= openStageHome.J4 + 5 && targetPosition.J4 >= openStageHome.J4 - 5
                && targetPosition.J5 <= openStageHome.J5 + 5 && targetPosition.J5 >= openStageHome.J5 - 5
                && targetPosition.J6 <= openStageHome.J6 + 5 && targetPosition.J6 >= openStageHome.J6 - 5)
            {
                EndPosName = "OpenStage Target";
            }
            else if (targetPosition.J1 <= drawer1Home.J1 + 5 && targetPosition.J1 >= drawer1Home.J1 - 5
                && targetPosition.J2 <= drawer1Home.J2 + 5 && targetPosition.J2 >= drawer1Home.J2 - 5
                && targetPosition.J3 <= drawer1Home.J3 + 5 && targetPosition.J3 >= drawer1Home.J3 - 5
                && targetPosition.J4 <= drawer1Home.J4 + 5 && targetPosition.J4 >= drawer1Home.J4 - 5
                && targetPosition.J5 <= drawer1Home.J5 + 5 && targetPosition.J5 >= drawer1Home.J5 - 5
                && targetPosition.J6 <= drawer1Home.J6 + 5 && targetPosition.J6 >= drawer1Home.J6 - 5)
            {
                EndPosName = "Drawer1 Target";
            }
            else if (targetPosition.J1 <= faceDrawer2.J1 + 5 && targetPosition.J1 >= faceDrawer2.J1 - 5
                && targetPosition.J2 <= faceDrawer2.J2 + 5 && targetPosition.J2 >= faceDrawer2.J2 - 5
                && targetPosition.J3 <= faceDrawer2.J3 + 5 && targetPosition.J3 >= faceDrawer2.J3 - 5
                && targetPosition.J4 <= faceDrawer2.J4 + 5 && targetPosition.J4 >= faceDrawer2.J4 - 5
                && targetPosition.J5 <= faceDrawer2.J5 + 5 && targetPosition.J5 >= faceDrawer2.J5 - 5
                && targetPosition.J6 <= faceDrawer2.J6 + 5 && targetPosition.J6 >= faceDrawer2.J6 - 5)
            {
                EndPosName = "Drawer2 Target";
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
                    if (StartPosName != "Drawer1 Home" && EndPosName != "Drawer1 Target")
                    {
                        // RobotMove(PosToInspCh());
                        //RobotMove(PosToAssembly);
                        this.MoveAsync(drawer1Home);
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


        /// <summary>從Home 移動到 Drawer</summary>
        /// <param name="drawerIndex">Drawer index</param>
        /// <param name="boxIndex">Box index</param>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void ForwardToDrawer(int drawerIndex,int boxIndex)
        {// TODO: 待實作 [ForwardToDrawer]
            var positions = default(List<HalRobotMotion>);
            if (drawerIndex == 1)
            {
                 positions= new BoxTransferPathPasitions().FromHomeToDrawer1;
            }
            this.MoveAsync(positions);
        }

        /// <summary>從 Drawer Back Home</summary>
        /// <param name="drawerIndex">Drawer Index</param>
        /// <param name="boxIndex">Box index</param>
        ///<remarks>King, 2020/05/25 Add</remarks>
        public void BackwardFromDrawer(int drawerIndex,int boxIndex)
        { // TODO: 待實作 [BackwardFromDrawer]
            
        }

        /// <summary>轉動方向,面對 Open Stage</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void ChangeDirectionToFaceOpenStage()
        { 
            var position = new BoxTransferPathPasitions().FaceOpenStage;
            ChangeDirection(position);
        }

        /// <summary>移至 OpenStage</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void ForwardToOpenStage()
        { 
            var positions = new BoxTransferPathPasitions().FromHomeToOpenStage;
            this.MoveAsync(positions);
            
        }


        /// <summary>從Open Stage Back Home</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public void BackwardFromOpenStage()
        {
            var positions = new BoxTransferPathPasitions().FromOpenStageToHome;
            this.MoveAsync(positions);
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
          foreach(var position in targetPositions)
          {
                this.MoveAsync(position);
          }
        }
        #endregion

        public void MoveToDrawer(int number)
        {



            this.Robot.HalMoveStraightAsyn(new HalRobotMotion()
            {
                 X= 1,
                 Y=2,
                 Z=3,
                 W=4,
                 P=5,
                 R=6,
                 Speed=20,
            });

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
        /// 設定六軸力覺Sensor的壓力極限值
        /// </summary>
        /// <param name="Fx"></param>
        /// <param name="Fy"></param>
        /// <param name="Fz"></param>
        /// <param name="Mx"></param>
        /// <param name="My"></param>
        /// <param name="Mz"></param>
        public void SetSixAxisSensorLimit(uint? Fx, uint? Fy, uint? Fz, uint? Mx, uint? My, uint? Mz)
        { Plc.SetSixAxisSensorLimit(Fx, Fy, Fz, Mx, My, Mz); }
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
        /// 讀取六軸力覺Sensor的壓力極限值設定
        /// </summary>
        /// <returns></returns>
        public Tuple<int, int, int, int, int, int> ReadSixAxisSensorLimitSetting()
        { return Plc.ReadSixAxisSensorLimitSetting(); }
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
        #endregion

    }

    /// <summary>Path Test Position Collection</summary>
    /// <remarks>King, 2020/05/15 Add</remarks>
    public class BoxTransferPathPasitions
    {
        public HalRobotMotion OpenStageHome
        {
            get
            {

                return null;
            }

        }

        public HalRobotMotion Drawer2Home
        {
            get
            {
                return null;
            }
        }

        public HalRobotMotion Drawer1Home
        {
            get
            {
                var position = new HalRobotMotion {
                    J1 = -90.07816f,
                    J2 = 22.926424f,
                    J3 = -64.1397858f,
                    J4 = 0.9846464f,
                    J5 = 61.5056839f,
                    J6 = 1.27504456f,
                    J7 = 414.898f,
                    Speed = 50,
                    MotionType = HalRobotEnumMotionType.Joint,


                };
                // TODO: 設定 Home 點位
                return position;
            }
        }
        


        /// <summary>面對 Drawer1 的點位</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public HalRobotMotion FaceDrawer1
        {
            get
            {
                var position = new HalRobotMotion();
                // TODO: 加入在 Home 面對 Drawer1 的點位

                return position;
            }
                
        }

        /// <summary>面對 Drawer2 的點位</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public HalRobotMotion FaceDrawer2
        {
            get
            {
                var position = new HalRobotMotion();
                // TODO: 加入在 Home 面對 Drawer2 的點位

                return position;
            }
        }
        public HalRobotMotion FaceOpenStage
        {
            get
            {
                var position = new HalRobotMotion();
                // TODO: 加入在 Home 面對 OpenStage 的點位

                return position;
            }
        }



        /// <summary>從 Home 到 Drawer1 的點位集合</summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public List<HalRobotMotion> FromHomeToDrawer1
        {
            get
            {
                List<HalRobotMotion> positions = new List<HalRobotMotion>();
                positions.Add(
                    new HalRobotMotion
                    {
                        X= 0.6692078f,
                        Y= -527.9844f,
                        Z= 38.3338966f,
                        W= 147.140915f,
                        P= -86.85986f,
                        R= -56.31354f,
                        E1=403.3014f,
                        Speed=60,
                        MotionType=HalRobotEnumMotionType.Position,
                    } );
                positions.Add(
                    new HalRobotMotion
                    {
                        X = 36.3934479f,
                        Y = -680.056335f,
                        Z = 38.28108f,
                        W = 147.165817f,
                        P = -86.8544f,
                        R = -56.3381348f,
                        E1 = 402.3014f,
                        Speed = 60,
                        MotionType = HalRobotEnumMotionType.Position,
                    });
                // TODO: 加入從Home 到 Drawer1 的點位資料
                return positions; 
            }
        }

        /// <summary>從 Drawer1 到 Home 的點位集合  </summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public List<HalRobotMotion> FromDrawer1ToHome
        {
            get
            {
                List<HalRobotMotion> positions = new List<HalRobotMotion>();
                // TODO: 加入從 Drawer1 到 Home 點位資料
                return positions;
            }
        }

        /// <summary>從 Home 到 Drawer2 點位的集合 </summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public List<HalRobotMotion> FromHomeToDrawer2
        {
            get
            {
                List<HalRobotMotion> positions = new List<HalRobotMotion>();
                // TODO: 加入Home 到 Drawer2 點位資料
                return positions;
            }
        }

        /// <summary>從 Drawer2 到 Home 點位的集合 </summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public List<HalRobotMotion> FromDrawer2ToHome
        {
            get
            {
                List<HalRobotMotion> positions = new List<HalRobotMotion>();
                // TODO: 加入Drawer2 到 Home 點位資料
                return positions;
            }
        }
        /// <summary>從 Home 到 OpenStage 點位的集合 </summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public List<HalRobotMotion> FromHomeToOpenStage
        {
            get
            {
                List<HalRobotMotion> positions = new List<HalRobotMotion>();
                // TODO: 加入 Home 到 OpenStage點位資料
                return positions;
            }
        }

        /// <summary>從 OpenStage 到 Home 點位的集合 </summary>
        /// <remarks>King, 2020/05/25 Add</remarks>
        public List<HalRobotMotion> FromOpenStageToHome
        {
            get
            {
                List<HalRobotMotion> positions = new List<HalRobotMotion>();
                // TODO: 加入 OpenStage 到 Home點位資料
                return positions;
            }
        }
    }

    
}
