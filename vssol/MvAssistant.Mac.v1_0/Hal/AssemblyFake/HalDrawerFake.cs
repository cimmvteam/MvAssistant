using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component;
using MvAssistant.Mac.v1_0.Hal.Component.Button;
using MvAssistant.Mac.v1_0.Hal.Component.Door;
using MvAssistant.Mac.v1_0.Hal.Component.Infrared;
using MvAssistant.Mac.v1_0.Hal.Component.PresenceDetector;
using MvAssistant.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [MachineManifest(DeviceEnum.drawer_assembly)]
    [GuidAttribute("E86AB31F-9642-42F2-852B-394ED097C54D")]
    public class HalDrawerFake : HalFakeBase, IHalDrawer
    {
        #region Device Components (請看範例說明)
        /// 範例說明
        /// private IHalCamera topCamera;
        /// private IHalCamera sideCamera;
        /// 
        /// [MachineManifest(DeviceEnum.loadport_ccd_top_1)]
        /// public IHalCamera TopCamera
        /// {
        ///     set { topCamera = value; }
        ///     get { return topCamera; }
        /// }
        /// 
        /// [MachineManifest(DeviceEnum.loadport_ccd_side_1)]
        /// public IHalCamera SideCamera
        /// {
        ///     set { sideCamera = value; }
        ///     get { return sideCamera; }
        /// }
        /// 
        private IHalInfraredPhotointerrupter interupter_PeopleSideLimit;
        [MachineManifest(DeviceEnum.drawer_Interupter_PeopleSideLimit)]
        public IHalInfraredPhotointerrupter Interupter_PeopleSideLimit
        {
            get { return interupter_PeopleSideLimit; }
            set { interupter_PeopleSideLimit = value; }
        }
        private IHalInfraredPhotointerrupter interupter_RobotSideLimit;
        [MachineManifest(DeviceEnum.drawer_Interupter_RobotSideLimit)]
        public IHalInfraredPhotointerrupter Interupter_RobotSideLimit
        {
            get { return interupter_RobotSideLimit; }
            set { interupter_RobotSideLimit = value; }
        }

        private IHalInfraredPhotointerrupter interupter_PeopleSide;
        [MachineManifest(DeviceEnum.drawer_Interupter_PeopleSide)]
        public IHalInfraredPhotointerrupter Interupter_PeopleSide
        {
            get { return interupter_PeopleSide; }
            set { interupter_PeopleSide = value; }
        }
        private IHalInfraredPhotointerrupter interupter_RobotSide;
        [MachineManifest(DeviceEnum.drawer_Interupter_RobotSide)]
        public IHalInfraredPhotointerrupter Interupter_RobotSide
        {
            get { return interupter_RobotSide; }
            set { interupter_RobotSide = value; }
        }
        private IHalInfraredPhotointerrupter interupter_Home;
        [MachineManifest(DeviceEnum.drawer_Interupter_Home)]
        public IHalInfraredPhotointerrupter Interupter_Home
        {
            get { return interupter_Home; }
            set { interupter_Home = value; }
        }
        private IHalButton button_DrawerLoadControl;
        [MachineManifest(DeviceEnum.drawer_Button_DrawerLoadControl)]
        public IHalButton Button_DrawerLoadControl
        {
            get { return button_DrawerLoadControl; }
            set { button_DrawerLoadControl = value; }
        }

        private IHalPresenceDetector boxPresentDetector;
        [MachineManifest(DeviceEnum.drawer_PresentDetector)]
        public IHalPresenceDetector BoxPresentDetector
        {
            get { return boxPresentDetector; }
            set { boxPresentDetector = value; }
        }
        #endregion Device Components

        #region DeviceHAL
        //private IHalLaser directionDetector = null;
        //[MachineManifest(DeviceEnum.boxslot_laser_1)]
        //public IHalLaser DirectionDetector
        //{
        //    get { return directionDetector; }
        //    set { directionDetector = value; }
        //}

        //private IHalFiberOptic inPlachDetector = null;
        //[MachineManifest(DeviceEnum.boxslot_fiber_optical_1)]
        //public IHalFiberOptic InPlachDetector
        //{
        //    get { return inPlachDetector; }
        //    set { inPlachDetector = value; }
        //}

        //private IHalDoor slotDoor = null;
        //[MachineManifest(DeviceEnum.boxslot_outdoor_1)]
        //public IHalDoor SlotDoor
        //{
        //    get { return slotDoor; }
        //    set { slotDoor = value; }
        //}


        #endregion

        #region HAL Interface Functions
        //bool IHalBoxSlot.SetDockCompleteButtonAddress(string varName)
        //{
        //    throw new NotImplementedException();
        //}

        //bool IHalBoxSlot.SetUndockCompleteButtonAddress(string varName)
        //{
        //    throw new NotImplementedException();
        //}

        //void IHalBoxSlot.StartMonitorButtonStatus()
        //{
        //    throw new NotImplementedException();
        //}

        //void IHalBoxSlot.MonitorButtonStatusPause()
        //{
        //    throw new NotImplementedException();
        //}

        int IHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions
    }
}
