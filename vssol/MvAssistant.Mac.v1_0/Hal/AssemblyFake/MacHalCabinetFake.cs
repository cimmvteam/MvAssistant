using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.Component.Door;
using MvAssistant.Mac.v1_0.Hal.Component.FiberOptic;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.AssemblyFake
{
    [MacMachineManifest(MacEnumDevice.cabinet_assembly)]
    [GuidAttribute("935B114A-D18B-404B-A843-D02DA8DDBD89")]
    public class MacHalCabinetFake : HalFakeBase, IMacHalCabinet
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

        public IHalFiberOptic InPlace_Slot_01 { get; set; }
        public IHalFiberOptic InPlace_Slot_10 { get; set; }
        public IHalFiberOptic InPlace_Slot_11 { get; set; }
        public IHalFiberOptic InPlace_Slot_12 { get; set; }
        public IHalFiberOptic InPlace_Slot_13 { get; set; }
        public IHalFiberOptic InPlace_Slot_14 { get; set; }
        public IHalFiberOptic InPlace_Slot_15 { get; set; }
        public IHalFiberOptic InPlace_Slot_16 { get; set; }
        public IHalFiberOptic InPlace_Slot_17 { get; set; }
        public IHalFiberOptic InPlace_Slot_18 { get; set; }
        public IHalFiberOptic InPlace_Slot_19 { get; set; }
        public IHalFiberOptic InPlace_Slot_02 { get; set; }
        public IHalFiberOptic InPlace_Slot_20 { get; set; }
        public IHalFiberOptic InPlace_Slot_21 { get; set; }
        public IHalFiberOptic InPlace_Slot_22 { get; set; }
        public IHalFiberOptic InPlace_Slot_23 { get; set; }
        public IHalFiberOptic InPlace_Slot_24 { get; set; }
        public IHalFiberOptic InPlace_Slot_25 { get; set; }
        public IHalFiberOptic InPlace_Slot_26 { get; set; }
        public IHalFiberOptic InPlace_Slot_27 { get; set; }
        public IHalFiberOptic InPlace_Slot_28 { get; set; }
        public IHalFiberOptic InPlace_Slot_29 { get; set; }
        public IHalFiberOptic InPlace_Slot_03 { get; set; }
        public IHalFiberOptic InPlace_Slot_30 { get; set; }
        public IHalFiberOptic InPlace_Slot_04 { get; set; }
        public IHalFiberOptic InPlace_Slot_05 { get; set; }
        public IHalFiberOptic InPlace_Slot_06 { get; set; }
        public IHalFiberOptic InPlace_Slot_07 { get; set; }
        public IHalFiberOptic InPlace_Slot_08 { get; set; }
        public IHalFiberOptic InPlace_Slot_09 { get; set; }
        public IHalDoor InSlotDoor_01 { get; set; }
        public IHalDoor InSlotDoor_02 { get; set; }
        public IHalDoor InSlotDoor_03 { get; set; }
        public IHalDoor InSlotDoor_04 { get; set; }
        public IHalDoor InSlotDoor_05 { get; set; }
        public IHalDoor InSlotDoor_06 { get; set; }
        public IHalDoor OutSlotDoor_01 { get; set; }
        public IHalDoor OutSlotDoor_10 { get; set; }
        public IHalDoor OutSlotDoor_11 { get; set; }
        public IHalDoor OutSlotDoor_12 { get; set; }
        public IHalDoor OutSlotDoor_13 { get; set; }
        public IHalDoor OutSlotDoor_14 { get; set; }
        public IHalDoor OutSlotDoor_15 { get; set; }
        public IHalDoor OutSlotDoor_16 { get; set; }
        public IHalDoor OutSlotDoor_17 { get; set; }
        public IHalDoor OutSlotDoor_18 { get; set; }
        public IHalDoor OutSlotDoor_19 { get; set; }
        public IHalDoor OutSlotDoor_02 { get; set; }
        public IHalDoor OutSlotDoor_20 { get; set; }
        public IHalDoor OutSlotDoor_21 { get; set; }
        public IHalDoor OutSlotDoor_22 { get; set; }
        public IHalDoor OutSlotDoor_23 { get; set; }
        public IHalDoor OutSlotDoor_24 { get; set; }
        public IHalDoor OutSlotDoor_25 { get; set; }
        public IHalDoor OutSlotDoor_26 { get; set; }
        public IHalDoor OutSlotDoor_27 { get; set; }
        public IHalDoor OutSlotDoor_28 { get; set; }
        public IHalDoor OutSlotDoor_29 { get; set; }
        public IHalDoor OutSlotDoor_03 { get; set; }
        public IHalDoor OutSlotDoor_30 { get; set; }
        public IHalDoor OutSlotDoor_04 { get; set; }
        public IHalDoor OutSlotDoor_05 { get; set; }
        public IHalDoor OutSlotDoor_06 { get; set; }
        public IHalDoor OutSlotDoor_07 { get; set; }
        public IHalDoor OutSlotDoor_08 { get; set; }
        public IHalDoor OutSlotDoor_09 { get; set; }


        #endregion Device Components

        #region HAL Interface Functions
        public bool SetDockCompleteButtonAddress(string varName)
        {
            throw new NotImplementedException();
        }

        public bool SetUndockCompleteButtonAddress(string varName)
        {
            throw new NotImplementedException();
        }

        public void StartMonitorButtonStatus()
        {
            throw new NotImplementedException();
        }

        public void MonitorButtonStatusPause()
        {
            throw new NotImplementedException();
        }

        public int HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions
    }
}
