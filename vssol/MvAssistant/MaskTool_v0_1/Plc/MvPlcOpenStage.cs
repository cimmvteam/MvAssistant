﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MvAssistant.MaskTool_v0_1.Plc
{
    public class MvPlcOpenStage
    {
        private MvPlcContext m_PlcContext;

        public MvPlcOpenStage(MvPlcContext plc)
        {
            this.m_PlcContext = plc;
        }
        public string Open()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_Open, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Open_Reply), 1000))
                throw new MvException("Open Stage OpenBox T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Open_Complete), 5000))
                throw new MvException("Open Stage OpenBox T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_Open), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Open_Complete), 1000))
                throw new MvException("Open Stage OpenBox T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_Open_Result))
            {
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "NoBox";
                    break;
                case 3:
                    Result = "Slider Point Error";
                    break;
                case 4:
                    Result = "";
                    break;
            }
            return Result;
        }

        public string Close()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_Close, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Close_Reply), 1000))
                throw new MvException("Open Stage CloseBox T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Close_Complete), 5000))
                throw new MvException("Open Stage CloseBox T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_Close), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Close_Complete), 1000))
                throw new MvException("Open Stage CloseBox T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_Close_Result))
            {
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "NoBox";
                    break;
                case 3:
                    Result = "Slider Point Error";
                    break;
                case 4:
                    Result = "Cover Point Error";
                    break;
            }
            return Result;
        }

        //開盒夾爪閉合
        public string Clamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_Clamp, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Clamp_Reply), 1000))
                throw new MvException("Open Stage Clamp T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Clamp_Complete), 5000))
                throw new MvException("Open Stage Clamp T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_Clamp), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Clamp_Complete), 1000))
                throw new MvException("Open Stage Clamp T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_Clamp_Result))
            {
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "NoBox";
                    break;
                case 3:
                    Result = "NoClose";
                    break;
                case 4:
                    Result = "";
                    break;
            }
            return Result;
        }

        //開盒夾爪鬆開
        public string Unclamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_Unclamp, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Reply), 1000))
                throw new MvException("Open Stage Unclamp T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Complete), 5000))
                throw new MvException("Open Stage Unclamp T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_Unclamp), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Complete), 1000))
                throw new MvException("Open Stage Unclamp T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Result))
            {
                //case 1:
                //    Result = "OK";
                //    break;
                //case 2:
                //    Result = "NoBox";
                //    break;
                //case 3:
                //    Result = "NoClose";
                //    break;
                //case 4:
                //    Result = "";
                //    break;
            }
            return Result;
        }

        //Stage上固定Box的夾具閉合
        public string SortClamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_SortClamp, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Reply), 1000))
                throw new MvException("Open Stage SortClamp T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Complete), 5000))
                throw new MvException("Open Stage SortClamp T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_SortClamp), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Complete), 1000))
                throw new MvException("Open Stage SortClamp T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Result))
            {
                case 1:
                    Result = "OK";
                    break;
                case 2:
                    Result = "NoBox";
                    break;
                case 3:
                    Result = "Clamping";
                    break;
            }
            return Result;
        }

        //Stage上固定Box的夾具鬆開
        public string SortUnclamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_SortUnclamp, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Reply), 1000))
                throw new MvException("Open Stage SortUnclamp T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Complete), 5000))
                throw new MvException("Open Stage SortUnclamp T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_SortUnclamp), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Complete), 1000))
                throw new MvException("Open Stage SortUnclamp T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Result))
            {
                case 1:
                    Result = "OK";
                    break;
            }
            return Result;
        }

        //開關盒鎖(True：上鎖, False：開鎖)
        public string Lock(bool IsLock)
        {
            var Result = "";
            var plc = this.m_PlcContext;

            plc.Write(MvEnumPlcVariable.PC_TO_OS_Lock, IsLock);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Lock_Reply), 1000))
                throw new MvException("Open Stage Lock/Unlock T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Lock_Complete), 5000))
                throw new MvException("Open Stage Lock/Unlock T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_Lock), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Lock_Complete), 1000))
                throw new MvException("Open Stage Lock/Unlock T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_Lock_Result))
            {
                //case 1:
                //    Result = "OK";
                //    break;
                //case 2:
                //    Result = "NoBox";
                //    break;
                //case 3:
                //    Result = "NoClose";
                //    break;
                //case 4:
                //    Result = "";
                //    break;
            }
            return Result;
        }

        public string Initial()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_OS_Initial_A05, true);

            if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Reply), 1000))
                throw new MvException("Open Stage Initial T0 timeout");
            else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Complete), 5000))
                throw new MvException("Open Stage Initial T2 timeout");

            SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_Initial_A05), 1000);

            if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Complete), 1000))
                throw new MvException("Open Stage Initial T4 timeout");
            switch (plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Result))
            {
                //case 0:
                //    Result = "Invalid";
                //    break;
                //case 1:
                //    Result = "Idle";
                //    break;
                //case 2:
                //    Result = "Busy";
                //    break;
                //case 3:
                //    Result = "Error";
                //    break;
            }
            return Result;
        }

        public string SetCommand()
        {
            string Result = "";

            return Result;
        }

        //確認Robot入侵
        public Tuple<bool, bool> CheckRobotIntrude()
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_OS_BTIntrude, true);
            plc.Write(MvEnumPlcVariable.PC_TO_OS_MTIntrude, true);

            return new Tuple<bool, bool>(
                plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_BTLicence),
                plc.Read<bool>(MvEnumPlcVariable.PC_TO_OS_MTLicence)
                );
        }

        //確認開盒夾爪狀態
        public uint CheckClampStatus()
        {
            var plc = this.m_PlcContext;
            return plc.Read<uint>(MvEnumPlcVariable.OS_TO_PC_ClampStatus);
        }

        //確認Stage上固定Box的夾具位置
        public Tuple<int, int> CheckSortClampPosition()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_SortClamp1_Position),
                plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_SortClamp2_Position)
                );
        }

        //確認Slider位置
        public Tuple<int, int> CheckSliderPosition()
        {
            var plc = this.m_PlcContext;

            return new Tuple<int, int>(
                plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Slider1_Position),
                plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Slider2_Position)
                );
        }

        //確認盒蓋位置
        public Tuple<double, double> CheckCoverPos()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MvEnumPlcVariable.OS_TO_PC_Cover1_Position),
                plc.Read<double>(MvEnumPlcVariable.OS_TO_PC_Cover2_Position)
                );
        }

        //確認盒蓋開闔
        public Tuple<bool, bool> CheckCoverSensor()
        {
            var plc = this.m_PlcContext;

            return new Tuple<bool, bool>(
                plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_CoverSensor_Open),
                plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_CoverSensor_Close)
                );
        }

        //確認是否有Box
        public bool CheckBoxExist()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_BoxCheckOK);
        }
    }
}
