using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Runtime.InteropServices;

namespace MvAssistant.Mac.v1_0.Hal.Assembly
{
    [MachineManifest(EnumDevice.cabinet_assembly)]
    [GuidAttribute("DBCB4F3E-0405-450E-80D5-F2D1401975F1")]
    public class HalCabinet : HalAssemblyBase, IHalCabinet
    {


        #region Device Components (請看範例說明)
        /*
        [MachineManifest(DeviceEnum.drawer_InPlace_1)]
        public IHalFiberOptic InPlace_Slot_01 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_2)]
        public IHalFiberOptic InPlace_Slot_02 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_3)]
        public IHalFiberOptic InPlace_Slot_03 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_4)]
        public IHalFiberOptic InPlace_Slot_04 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_5)]
        public IHalFiberOptic InPlace_Slot_05 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_6)]
        public IHalFiberOptic InPlace_Slot_06 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_7)]
        public IHalFiberOptic InPlace_Slot_07 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_8)]
        public IHalFiberOptic InPlace_Slot_08 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_9)]
        public IHalFiberOptic InPlace_Slot_09 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_10)]
        public IHalFiberOptic InPlace_Slot_10 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_11)]
        public IHalFiberOptic InPlace_Slot_11 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_12)]
        public IHalFiberOptic InPlace_Slot_12 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_13)]
        public IHalFiberOptic InPlace_Slot_13 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_14)]
        public IHalFiberOptic InPlace_Slot_14 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_15)]
        public IHalFiberOptic InPlace_Slot_15 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_16)]
        public IHalFiberOptic InPlace_Slot_16 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_17)]
        public IHalFiberOptic InPlace_Slot_17 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_18)]
        public IHalFiberOptic InPlace_Slot_18 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_19)]
        public IHalFiberOptic InPlace_Slot_19 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_20)]
        public IHalFiberOptic InPlace_Slot_20 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_21)]
        public IHalFiberOptic InPlace_Slot_21 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_22)]
        public IHalFiberOptic InPlace_Slot_22 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_23)]
        public IHalFiberOptic InPlace_Slot_23 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_24)]
        public IHalFiberOptic InPlace_Slot_24 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_25)]
        public IHalFiberOptic InPlace_Slot_25 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_26)]
        public IHalFiberOptic InPlace_Slot_26 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_27)]
        public IHalFiberOptic InPlace_Slot_27 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_28)]
        public IHalFiberOptic InPlace_Slot_28 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_29)]
        public IHalFiberOptic InPlace_Slot_29 { get; set; }
        [MachineManifest(DeviceEnum.drawer_InPlace_30)]
        public IHalFiberOptic InPlace_Slot_30 { get; set; }


        [MachineManifest(DeviceEnum.cabinet_inner_door_1)]
        public IHalDoor InSlotDoor_01 { get; set; }

        [MachineManifest(DeviceEnum.cabinet_inner_door_2)]
        public IHalDoor InSlotDoor_02 { get; set; }

        [MachineManifest(DeviceEnum.cabinet_inner_door_3)]
        public IHalDoor InSlotDoor_03 { get; set; }

        [MachineManifest(DeviceEnum.cabinet_inner_door_4)]
        public IHalDoor InSlotDoor_04 { get; set; }

        [MachineManifest(DeviceEnum.cabinet_inner_door_5)]
        public IHalDoor InSlotDoor_05 { get; set; }

        [MachineManifest(DeviceEnum.cabinet_inner_door_6)]
        public IHalDoor InSlotDoor_06 { get; set; }

        [MachineManifest(DeviceEnum.drawer_outer_door_1)]
        public IHalDoor OutSlotDoor_01 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_2)]
        public IHalDoor OutSlotDoor_02 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_3)]
        public IHalDoor OutSlotDoor_03 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_4)]
        public IHalDoor OutSlotDoor_04 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_5)]
        public IHalDoor OutSlotDoor_05 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_6)]
        public IHalDoor OutSlotDoor_06 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_7)]
        public IHalDoor OutSlotDoor_07 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_8)]
        public IHalDoor OutSlotDoor_08 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_9)]
        public IHalDoor OutSlotDoor_09 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_10)]
        public IHalDoor OutSlotDoor_10 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_11)]
        public IHalDoor OutSlotDoor_11 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_12)]
        public IHalDoor OutSlotDoor_12 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_13)]
        public IHalDoor OutSlotDoor_13 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_14)]
        public IHalDoor OutSlotDoor_14 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_15)]
        public IHalDoor OutSlotDoor_15 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_16)]
        public IHalDoor OutSlotDoor_16 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_17)]
        public IHalDoor OutSlotDoor_17 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_18)]
        public IHalDoor OutSlotDoor_18 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_19)]
        public IHalDoor OutSlotDoor_19 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_20)]
        public IHalDoor OutSlotDoor_20 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_21)]
        public IHalDoor OutSlotDoor_21 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_22)]
        public IHalDoor OutSlotDoor_22 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_23)]
        public IHalDoor OutSlotDoor_23 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_24)]
        public IHalDoor OutSlotDoor_24 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_25)]
        public IHalDoor OutSlotDoor_25 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_26)]
        public IHalDoor OutSlotDoor_26 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_27)]
        public IHalDoor OutSlotDoor_27 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_28)]
        public IHalDoor OutSlotDoor_28 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_29)]
        public IHalDoor OutSlotDoor_29 { get; set; }
        [MachineManifest(DeviceEnum.drawer_outer_door_30)]
        public IHalDoor OutSlotDoor_30 { get; set; }
        */
        #endregion Device Components



        #region HAL Interface Functions


        int IHal.HalClose()
        {
            throw new NotImplementedException();
        }

        int IHal.HalConnect()
        {
            throw new NotImplementedException();
        }

        bool IHal.HalIsConnected()
        {
            throw new NotImplementedException();
        }

        int IHalAssembly.HalStop()
        {
            throw new NotImplementedException();
        }
        #endregion HAL Interface Functions


    }
}
