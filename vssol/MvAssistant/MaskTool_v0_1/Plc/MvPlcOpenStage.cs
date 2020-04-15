using System;
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
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Open, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Open, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Open_Reply), 1000))
                    throw new MvException("Open Stage Open Box T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Open_Complete), 10 * 1000))
                    throw new MvException("Open Stage Open Box T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Open_Result))
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

                plc.Write(MvEnumPlcVariable.PC_TO_OS_Open, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Open_Complete), 1000))
                    throw new MvException("Open Stage Open Box T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Open, false);
                throw ex;
            }
            return Result;
        }

        public string Close()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Close, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Close, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Close_Reply), 1000))
                    throw new MvException("Open Stage Close Box T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Close_Complete), 10 * 1000))
                    throw new MvException("Open Stage Close Box T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Close_Result))
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

                plc.Write(MvEnumPlcVariable.PC_TO_OS_Close, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Close_Complete), 1000))
                    throw new MvException("Open Stage Close Box T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Close, false);
                throw ex;
            }
            return Result;
        }

        //開盒夾爪閉合
        public string Clamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Clamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Clamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Clamp_Reply), 1000))
                    throw new MvException("Open Stage Clamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Clamp_Complete), 5000))
                    throw new MvException("Open Stage Clamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Clamp_Result))
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

                plc.Write(MvEnumPlcVariable.PC_TO_OS_Clamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Clamp_Complete), 1000))
                    throw new MvException("Open Stage Clamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Clamp, false);
                throw ex;
            }
            return Result;
        }

        //開盒夾爪鬆開
        public string Unclamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Unclamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Unclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Reply), 1000))
                    throw new MvException("Open Stage Unclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Complete), 5000))
                    throw new MvException("Open Stage Unclamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Result))
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

                plc.Write(MvEnumPlcVariable.PC_TO_OS_Unclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Unclamp_Complete), 1000))
                    throw new MvException("Open Stage Unclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Unclamp, false);
                throw ex;
            }
            return Result;
        }

        //Stage上固定Box的夾具閉合
        public string SortClamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortClamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortClamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Reply), 1000))
                    throw new MvException("Open Stage SortClamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Complete), 5000))
                    throw new MvException("Open Stage SortClamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Result))
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

                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortClamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortClamp_Complete), 1000))
                    throw new MvException("Open Stage SortClamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortClamp, false);
                throw ex;
            }
            return Result;
        }

        //Stage上固定Box的夾具鬆開
        public string SortUnclamp()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortUnclamp, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortUnclamp, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Reply), 1000))
                    throw new MvException("Open Stage SortUnclamp T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Complete), 5000))
                    throw new MvException("Open Stage SortUnclamp T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Result))
                {
                    case 1:
                        Result = "OK";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortUnclamp, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_SortUnclamp_Complete), 1000))
                    throw new MvException("Open Stage SortUnclamp T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_SortUnclamp, false);
                throw ex;
            }
            return Result;
        }

        //移到開/關盒鎖的位置
        public string Lock()
        {
            var Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Lock, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Lock, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Lock_Reply), 1000))
                    throw new MvException("Open Stage Lock/Unlock T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Lock_Complete), 15 * 1000))
                    throw new MvException("Open Stage Lock/Unlock T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Lock_Result))
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

                plc.Write(MvEnumPlcVariable.PC_TO_OS_Lock, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Lock_Complete), 1000))
                    throw new MvException("Open Stage Lock/Unlock T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Lock, false);
                throw ex;
            }
            return Result;
        }

        public string Initial()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            try
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Initial_A05, false);
                Thread.Sleep(100);
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Initial_A05, true);

                if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Reply), 1000))
                    throw new MvException("Open Stage Initial T0 timeout");
                else if (!SpinWait.SpinUntil(() => plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Complete), 30 * 1000))
                    throw new MvException("Open Stage Initial T2 timeout");

                switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Result))
                {
                    case 0:
                        Result = "Invalid";
                        break;
                    case 1:
                        Result = "Idle";
                        break;
                    case 2:
                        Result = "Busy";
                        break;
                    case 3:
                        Result = "Error";
                        break;
                }

                plc.Write(MvEnumPlcVariable.PC_TO_OS_Initial_A05, false);

                if (!SpinWait.SpinUntil(() => !plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_Initial_A05_Complete), 1000))
                    throw new MvException("Open Stage Initial T4 timeout");
            }
            catch (Exception ex)
            {
                plc.Write(MvEnumPlcVariable.PC_TO_OS_Initial_A05, false);
                throw ex;
            }
            return Result;

        }

        public void SetBoxType(uint BoxType)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_OS_BoxType, BoxType);

        }

        public string ReadBoxTypeSetting()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            switch (plc.Read<int>(MvEnumPlcVariable.PC_TO_OS_BoxType))
            {
                case 1:
                    Result = "鐵盒";
                    break;
                case 2:
                    Result = "水晶盒";
                    break;
            }
            return Result;
        }

        //讀取Robot入侵
        public Tuple<bool, bool> ReadRobotIntrude(bool BTIntrude, bool MTIntrude)
        {
            var plc = this.m_PlcContext;
            plc.Write(MvEnumPlcVariable.PC_TO_OS_BTIntrude, BTIntrude);
            plc.Write(MvEnumPlcVariable.PC_TO_OS_MTIntrude, MTIntrude);
            Thread.Sleep(100);
            return new Tuple<bool, bool>(
                plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_BTLicence),
                plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_MTLicence)
                );
        }

        //讀取開盒夾爪狀態
        public int ReadClampStatus()
        {
            var plc = this.m_PlcContext;
            return plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_ClampStatus);
        }

        //讀取Stage上固定Box的夾具位置
        public Tuple<long, long> ReadSortClampPosition()
        {
            var plc = this.m_PlcContext;

            return new Tuple<long, long>(
                plc.Read<long>(MvEnumPlcVariable.OS_TO_PC_SortClamp1_Position),
                plc.Read<long>(MvEnumPlcVariable.OS_TO_PC_SortClamp2_Position)
                );
        }

        //讀取Slider的位置
        public Tuple<long, long> ReadSliderPosition()
        {
            var plc = this.m_PlcContext;

            return new Tuple<long, long>(
                plc.Read<long>(MvEnumPlcVariable.OS_TO_PC_Slider1_Position),
                plc.Read<long>(MvEnumPlcVariable.OS_TO_PC_Slider2_Position)
                );
        }

        //讀取盒蓋位置
        public Tuple<double, double> ReadCoverPos()
        {
            var plc = this.m_PlcContext;

            return new Tuple<double, double>(
                plc.Read<double>(MvEnumPlcVariable.OS_TO_PC_Cover1_Position),
                plc.Read<double>(MvEnumPlcVariable.OS_TO_PC_Cover2_Position)
                );
        }

        //讀取盒蓋開闔
        public Tuple<bool, bool> ReadCoverSensor()
        {
            var plc = this.m_PlcContext;

            return new Tuple<bool, bool>(
                plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_CoverSensor_Open),
                plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_CoverSensor_Close)
                );
        }

        //讀取盒子是否變形
        public double ReadBoxDeform()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.OS_TO_PC_SoundWave);
        }

        //讀取平台上的重量
        public double ReadWeightOnStage()
        {
            var plc = this.m_PlcContext;
            return plc.Read<double>(MvEnumPlcVariable.OS_TO_PC_Weight_Cruuent);
        }

        //讀取是否有Box
        public bool ReadBoxExist()
        {
            var plc = this.m_PlcContext;
            return plc.Read<bool>(MvEnumPlcVariable.OS_TO_PC_BoxCheckOK);
        }

        public string ReadOpenStageStatus()
        {
            string Result = "";
            var plc = this.m_PlcContext;
            switch (plc.Read<int>(MvEnumPlcVariable.OS_TO_PC_A05Status))
            {
                case 1:
                    Result = "Idle";
                    break;
                case 2:
                    Result = "Busy";
                    break;
                case 3:
                    Result = "Alarm";
                    break;
                case 4:
                    Result = "Maintenance";
                    break;
            }
            return Result;
        }
    }
}
