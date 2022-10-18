using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public enum MotionStatus
{
    eMSTAT_ENABLED = 0,
    eMSTAT_MOVING,
    eMSTAT_ACC,
    eMSTAT_SYNC,
    eMSTAT_INPOS,
    eMSTAT_INVEL,
    eMSTAT_BLENDING,
    eMSTAT_LASTBIT = 31
};

public enum HimcApiState
{
    eApiSt_Connecting,
    eApiSt_Connect,

};

public enum ComType
{
    COM_TYPE_TCPIP,
    COM_TYPE_RS232,
    COM_TYPE_SIMULATOR

};

public struct ComInfo
{
    public ComType type;

    public struct TCP_IP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        public byte[] ip;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public byte[] port;
    };

    public struct RS232
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
        public byte[] com_port_name;
        public int baud_rate;
    };

    public struct Simulator
    {
        public char autoExecExe;
    };
    public TCP_IP com_TCP;
    public RS232 com_RS232;
    public Simulator com_sim;
};

public enum CoordSystem
{
    kCoord_ACS = 0,
    kCoord_MCS = 1,
    kCoord_PCS = 2
};

public enum MotionBufferMode
{
    kBM_Aborting = 0,
    kBM_Buffered = 1,
    kBM_BlendingLow = 2,
    kBM_BlendingPrevious = 3,
    kBM_BlendingNext = 4,
    kBM_BlendingHigh = 5,
};

public enum MotionTransitionMode
{
    kTM_None = 0,
    kTM_StartVelocity = 1,
    kTM_ConstantVelocity = 2,
    kTM_CornerDistance = 3,
    kTM_MaxCornerDeviation = 4,
    kTM_PLCopenReserved_05 = 5,
    kTM_PLCopenReserved_06 = 6,
    kTM_PLCopenReserved_07 = 7,
    kTM_PLCopenReserved_08 = 8,
    kTM_PLCopenReserved_09 = 9
};

public struct CoordPosition
{
    public double dCoordinateX;
    public double dCoordinateY;
    public double dCoordinateZ;
    public double dRotaryAngleA;
    public double dRotaryAngleB;
    public double dRotaryAngleC;
};

public struct NormalVector
{
    public double dVectorX;
    public double dVectorY;
    public double dVectorZ;
};

public struct CenterPosition
{
    public double dCenterX;
    public double dCenterY;
    public double dCenterZ;
};

public struct MotionProfile
{
    public double dMaxVelocity;
    public double dMaxAcceleration;
    public double dMaxDeceleration;
    public double dSmoothTime;
};

public struct PosTriggerPar
{
    public double dStartPos;
    public double dEndPos;
    public double dInterval;
    public int nPulseWidth;
    public int nPolarity;
};

namespace MvAssistant.v0_3.DeviceDriver.HiwinStage
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HMPLEventCBFuncPtr(int value);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HIMCErrorCBFuncPtr(int value);

    public class HimcApi
    {
        //Dll path
        const string DllName = @"HIMC_API.dll";

        [DllImport(DllName, EntryPoint = "HIMC_ConnectCtrl", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_ConnectCtrl(ComInfo com_info, ref int nCtrlID);
        // Disable all controller axis groups, axes and stop all user tasks.
        [DllImport(DllName, EntryPoint = "HIMC_DisconnectCtrl", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisconnectCtrl(int nCtrlID);
        // Disable all controller axis groups, axes and stop all user tasks.
        [DllImport(DllName, EntryPoint = "HIMC_EStop", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_EStop(int nCtrlID);
        // Disable all axes and axis groups.
        [DllImport(DllName, EntryPoint = "HIMC_DisableAll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisableAll(int nCtrlID);
        // Designate the value of the axis current position.
        [DllImport(DllName, EntryPoint = "HIMC_SetPos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetPos(int nCtrlID, int Axis, double Pos);
        // Set the maximum profile velocity of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_SetVel", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetVel(int nCtrlID, int nAxisID, double dVel);
        // Set the maximum profile acceleration of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_SetAcc", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetAcc(int nCtrlID, int nAxisID, double dAcc);
        // Set the maximum profile deceleration of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_SetDec", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetDec(int nCtrlID, int nAxisID, double dDec);
        // Set software positive limit position
        [DllImport(DllName, EntryPoint = "HIMC_SetSwPositiveLimitPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetSwPositiveLimitPos(int nCtrlID, int nAxisID, double dPositiveLimitPos);
        // Set software negative limit position
        [DllImport(DllName, EntryPoint = "HIMC_SetSwNegativeLimitPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetSwNegativeLimitPos(int nCtrlID, int nAxisID, double dNegativeLimitPos);
        // Set the maximum profile deceleration of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_SetSMTime", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetSMTime(int nCtrlID, int nAxisID, double dSmoothTime);

        // Enable the axis.
        [DllImport(DllName, EntryPoint = "HIMC_Enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_Enable(int ctrlidx, int axis);
        // Disable the axis.
        [DllImport(DllName, EntryPoint = "HIMC_Disable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_Disable(int ctrlidx, int axis);
        // Reset axis error.
        [DllImport(DllName, EntryPoint = "HIMC_Reset", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_Reset(int nCtrlID, int nAxisID);
        // Move axis to an absolute target position.
        [DllImport(DllName, EntryPoint = "HIMC_MoveAbs", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_MoveAbs(int ctrlidx, int axis, double pos);
        // Move axis with a relative distance.
        [DllImport(DllName, EntryPoint = "HIMC_MoveRel", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_MoveRel(int ctrlidx, int axis, double pos);
        // Starts a never-ending motion at a specified velocity.
        [DllImport(DllName, EntryPoint = "HIMC_MoveVel", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_MoveVel(int ctrlidx, int axis, double vel);
        // Stop a single axis.
        [DllImport(DllName, EntryPoint = "HIMC_Stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_Stop(int ctrlidx, int axis);

        // Query the "enable" status of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsEnabled", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsEnabled(int ctrlidx, int axis, ref int pblEn);
        // Query the "in-position" status of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsInPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsInPos(int nCtrlID, int nAxisID, ref int pblInPosition);
        // Query the "moving" status of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsMoving", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsMoving(int nCtrlID, int nAxisID, ref int pblMoving);
        // Query if the axis is grouped in an axis group.
        [DllImport(DllName, EntryPoint = "HIMC_IsGrouped", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsGrouped(int nCtrlID, int nAxisID, ref int pbIsGrouped);
        // Query the "synchronized motion" status of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsSync", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsSync(int nCtrlID, int nAxisID, ref int pbIsSync);
        // Query if the axis is in state "Error Stop".
        [DllImport(DllName, EntryPoint = "HIMC_IsErrorStop", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsErrorStop(int nCtrlID, int nAxisID, ref int pblError);
        // Query whether the axis is in gantry mode.
        [DllImport(DllName, EntryPoint = "HIMC_IsGantry", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsGantry(int nCtrlID, int nAxisID, ref int pblsGantry);
        // Query the "hardware left limit status" of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsHWLL", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsHWLL(int nCtrlID, int nAxisID, ref int pblsHWLL);
        // Query the "hardware right limit status" of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsHWRL", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsHWRL(int nCtrlID, int nAxisID, ref int pblsHWRL);
        // Query the "software left limit status" of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsSWLL", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsSWLL(int nCtrlID, int nAxisID, ref int pblsSWLL);
        // Query the "software right limit status" of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsSWRL", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsSWRL(int nCtrlID, int nAxisID, ref int pblsSWRL);
        // Query whether the dynamic error compensation is activated on the axis.
        [DllImport(DllName, EntryPoint = "HIMC_IsCompActive", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsCompActive(int nCtrlID, int nAxisID, ref int pbIsCompactive);

        // Get maximum profile velocity of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetMaxVel", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetMaxVel(int nCtrlID, int nAxisID, ref double pdVel);
        // Get maximum profile acceleration of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetMaxAcc", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetMaxAcc(int nCtrlID, int nAxisID, ref double pdAcc);
        // Get maximum profile deceleration of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetMaxDec", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetMaxDec(int nCtrlID, int nAxisID, ref double pdDec);
        // Get profile smooth time of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetSMTime", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSMTime(int nCtrlID, int nAxisID, ref double pdSmoothTime);
        // Get the current move time of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetMoveTime", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetMoveTime(int nCtrlID, int nAxisID, ref double pdMoveTime);
        // Get the current settling time of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetSettlingTime", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSettlingTime(int nCtrlID, int nAxisID, ref double pdSettlingTime);
        // Get the following position error of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetPosErr", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetPosErr(int nCtrlID, int nAxisID, ref double pdPosErr);

        // Get the axis group ID to which the axis belongs.
        [DllImport(DllName, EntryPoint = "HIMC_GetGroupID", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetGroupID(int nCtrlID, int nAxisID, ref int nGroupID);
        // Get reference position of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetRefPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetRefPos(int nCtrlID, int nAxisID, ref double pdPos);
        // Get reference velocity of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetRefVel", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetRefVel(int nCtrlID, int nAxisID, ref double pdVel);
        // Get reference acceleration of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetRefAcc", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetRefAcc(int nCtrlID, int nAxisID, ref double pdAcc);
        // Get reference position of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_GetPosFb", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetPosFb(int nCtrlID, int nAxisID, ref double pdPos);
        // Get software positive limit position.
        [DllImport(DllName, EntryPoint = "HIMC_GetSwPositiveLimitPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSwPositiveLimitPos(int nCtrlID, int nAxisID, ref double pdPositiveLimitPos);
        // Get software negative limit position.
        [DllImport(DllName, EntryPoint = "HIMC_GetSwNegativeLimitPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSwNegativeLimitPos(int nCtrlID, int nAxisID, ref double pdNegativeLimitPos);
        // Get multi axes feedback (array) position.
        // nAxisIDArray[i] is pdPosArray[i]'s position
        [DllImport(DllName, EntryPoint = "HIMC_GetMultiAxesFeedbackPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetMultiAxesFeedbackPos(int nCtrlID, int[] nAxisIDArray, int nNum, double[] pdPosArray);

        // Setup gantry axis pair.
        [DllImport(DllName, EntryPoint = "HIMC_EnableGantryPair", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_EnableGantryPair(int nCtrlID, int nLHSAxisId, int nRHSAxisId);
        // Split the gantry pair.
        [DllImport(DllName, EntryPoint = "HIMC_DisableGantryPair", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisableGantryPair(int nCtrlID, int nAxisID);

        // Enable touch probe function in the axis' corresponding slave drive.
        [DllImport(DllName, EntryPoint = "HIMC_EnableTouchprobe", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_EnableTouchprobe(int nCtrlID, int nAxisID);
        // Disable touch probe function in the axis' corresponding slave drive.
        [DllImport(DllName, EntryPoint = "HIMC_DisableTouchprobe", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisableTouchprobe(int nCtrlID, int nAxisID);
        // Query whether the touch probe function is enabled in the axis' corresponding slave.
        [DllImport(DllName, EntryPoint = "HIMC_IsTouchProbeEnabled", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsTouchProbeEnabled(int nCtrlID, int nAxisID, ref int bIsProbeEnabled);
        // Query whether the touch probe is triggered in the axis' corresponding slave drive.
        [DllImport(DllName, EntryPoint = "HIMC_IsTouchProbeTriggered", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsTouchProbeTriggered(int nCtrlID, int nAxisID, ref int bIsProbeTriggered);
        // Get the touch probe position in the the axis' slave drive.
        [DllImport(DllName, EntryPoint = "HIMC_GetTouchProbePos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetTouchProbePos(int nCtrlID, int nAxisID, ref double pdGetTouchProbe);

        // Set a list of axis into a group. The maximum number of axis is 9.
        //  Check axes enabled befor call this function
        [DllImport(DllName, EntryPoint = "HIMC_SetupGroup", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetupGroup(int nCtrlID, int nGroupID, int nNumOfAxes, int[] pAxesID);
        // Ungroup and disable the axis group.
        [DllImport(DllName, EntryPoint = "HIMC_UngrpAllAxes", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_UngrpAllAxes(int nCtrlID, int nGroupID);
        // Set group motion profile.
        [DllImport(DllName, EntryPoint = "HIMC_SetGrpMotionProfile", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetGrpMotionProfile(int nCtrlID, int nGroupID, double dMaxVelocity, double dMaxAcceleration, double dMaxDeceleration, double dSmoothTime);
        // Query the "moving" status of the group.
        [DllImport(DllName, EntryPoint = "HIMC_IsGrpMoving", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsGrpMoving(int nCtrlID, int nGroupID, ref int pblMoving);
        // Query the "In position" status of the group.
        [DllImport(DllName, EntryPoint = "HIMC_IsGrpInPos", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsGrpInPos(int nCtrlID, int nAxisID, ref int pblsInPos);

        // Enable the dynamic error compensation of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_EnableComp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_EnableComp(int nCtrlID, int nAxisID);
        // Disable the dynamic error compensation of the axis.
        [DllImport(DllName, EntryPoint = "HIMC_DisableComp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisableComp(int nCtrlID, int nAxisID);
        // Setup one-dimensional dynamic error compensation of the axis. 
        [DllImport(DllName, EntryPoint = "HIMC_SetupComp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetupComp(int nCtrlID, int nAxisID, int nStartIdx, double dBaseVal, double dInterval, int nNumPT, int nRefAxis);
        // Add an axis into the axis group.
        [DllImport(DllName, EntryPoint = "HIMC_AddAxesToGrp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_AddAxesToGrp(int nCtrlID, int nGroupID, int nNumOfAxes, int[] nAxesId);
        // Remove last axis from axis group.
        [DllImport(DllName, EntryPoint = "HIMC_RemoveAxisFromGrp", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_RemoveAxisFromGrp(int nCtrlID, int nGroupID);
        // Enable axis group.
        [DllImport(DllName, EntryPoint = "HIMC_EnableGroup", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_EnableGroup(int nCtrlID, int nGroupID);
        // Disable axis group.
        [DllImport(DllName, EntryPoint = "HIMC_DisableGroup", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisableGroup(int nCtrlID, int nGroupID);

        // Set axis group kinematics transformation (between MCS and ACS).
        [DllImport(DllName, EntryPoint = "HIMC_SetGrpKin", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetGrpKin(int nCtrlID, int nGroupID, int nKinType);
        // Query the "Enabled" status of the group.
        [DllImport(DllName, EntryPoint = "HIMC_IsGrpEnabled", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsGrpEnabled(int nCtrlID, int nGroupID, ref int pbIsGrpEnable);

        // Command an interpolated, buffered and two-dimensional linear movement towardan absolute target position in the machine coordinate system.
        [DllImport(DllName, EntryPoint = "HIMC_LineAbs2D", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LineAbs2D(int nCtrlID, int nGroupID, double dCoordinateX, double dCoordinateY);
        // Command an interpolated, buffered and three-dimensional linear movement towardan absolute target position in the machine coordinate system.
        [DllImport(DllName, EntryPoint = "HIMC_LineAbs3D", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LineAbs3D(int ctrlidx, int groupid, double dCoordinateX, double dCoordinateY, double dCoordinateZ);
        // Command an interpolated, two-dimensional linear movement toward an axis group from  the  actual  position  of  the TCP,  to  a  relative  position  in  the  specified  coordinate system.
        [DllImport(DllName, EntryPoint = "HIMC_LineRel2D", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LineRel2D(int nCtrlID, int nGroupID, double dDistanceX, double dDistanceY);
        // Command an interpolated, three-dimensional linear movement toward an axis group from  the  actual  position  of  the TCP,  to  a  relative  position  in  the  specified  coordinate system.
        [DllImport(DllName, EntryPoint = "HIMC_LineRel3D", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LineRel3D(int ctrlidx, int groupid, double ddistanceX, double ddistanceY, double ddistanceZ);
        // Commands an interpolated, buffered circular movement on an axis group from the actual position of the TCP. The border point and end point are defined absolutely in the machine coordinate system.
        [DllImport(DllName, EntryPoint = "HIMC_Arc2D", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_Arc2D(int nCtrlID, int nGroupID, double dBorderX, double dBorderY, double dEndX, double dEndY);
        // Commands an interpolated, buffered circular movement on an axis group from the actual position of the TCP. The center point and end point are defined absolutely in the machine coordinate system. The turns determine the direction and total angle of circular path.
        [DllImport(DllName, EntryPoint = "HIMC_Circle2D", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_Circle2D(int nCtrlID, int nGroupID, double dCenterX, double dCenterY, double dEndX, double dEndY, int nTurns);

        // Stop axis group motion.
        [DllImport(DllName, EntryPoint = "HIMC_StopGroup", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_StopGroup(int nCtrlID, int nGroupID);

        // six axes  absolute coordinate motion .
        [DllImport(DllName, EntryPoint = "HIMC_LinAbs", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LinAbs(int nCtrlID, int nGroupID, CoordPosition target_pos, MotionProfile motion_profile, CoordSystem coord_sys, MotionBufferMode buf_mode, MotionTransitionMode trans_mode, double dTransPar);
        // six axes  relatively coordinate motion .
        [DllImport(DllName, EntryPoint = "HIMC_LinRel", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LinRel(int nCtrlID, int nGroupID, CoordPosition relative_dist, MotionProfile motion_profile, CoordSystem coord_sys, MotionBufferMode buf_mode, MotionTransitionMode trans_mode, double dTransPar);
        // six axes  circle motion .
        [DllImport(DllName, EntryPoint = "HIMC_CircAbs", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_CircAbs(int nCtrlID, int nGroupID, CenterPosition center_pos, NormalVector normal_vector, int turns, CoordPosition target_pos, MotionProfile motion_profile, CoordSystem coord_sys, MotionBufferMode buf_mode, MotionTransitionMode trans_mode, double dTransPar);

        // Set variable value by ID
        [DllImport(DllName, EntryPoint = "HIMC_SetVariableByID", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetVariableByID(int nCtrlID, int nVarIndex, double dVal);
        // Get variable value by ID
        [DllImport(DllName, EntryPoint = "HIMC_GetVariableByID", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetVariableByID(int nCtrlID, int nVarIndex, ref double dVal);
        // Set variable list value by ID list
        [DllImport(DllName, EntryPoint = "HIMC_SetVariableListByID", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetVariableListByID(int nCtrlID, int[] nVarIndex, int nNum, double[] dVal);
        // Get variable value list by ID list
        [DllImport(DllName, EntryPoint = "HIMC_GetVariableListByID", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetVariableListByID(int nCtrlID, int[] nVarIndex, int nNum, double[] dVal);
        // Get global variable by name
        [DllImport(DllName, EntryPoint = "HIMC_GetGlobalVariables", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetGlobalVariables(int nCtrlID, string[] VarNameArray, int nLength, double[] dOutputArray);
        // Set global variable by name
        [DllImport(DllName, EntryPoint = "HIMC_SetGlobalVariables", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetGlobalVariables(int nCtrlID, string[] VarNameArray, int nLength, double[] dInputArray);

        // Retrieve controller user table data.
        [DllImport(DllName, EntryPoint = "HIMC_GetUserTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetUserTable(int nCtrlID, double[] pUserTableData, int nStartIndex, int nNumberOfDoublesToWrite);
        // Write controller user table.
        [DllImport(DllName, EntryPoint = "HIMC_SetUserTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetUserTable(int nCtrlID, double[] pUserTableData, int nStartIndex, int nNumberOfDoublesToWrite);
        // Store user table data in RAM to permanent storage.
        [DllImport(DllName, EntryPoint = "HIMC_SaveUserTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SaveUserTable(int nCtrlID, int nStartIndex, int nLength);
        // Load user table data in RAM from permanent storage.
        [DllImport(DllName, EntryPoint = "HIMC_LoadUserTable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_LoadUserTable(int nCtrlID, int nStartIndex, int nLength);

        // Start HMPL task.
        [DllImport(DllName, EntryPoint = "HIMC_StartHMPLTask", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_StartHMPLTask(int nCtrlID, int nTaskID);
        // Stop HMPL task.
        [DllImport(DllName, EntryPoint = "HIMC_StopHMPLTask", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_StopHMPLTask(int nCtrlID, int nTaskID);

        // Reboot Controller.
        [DllImport(DllName, EntryPoint = "HIMC_RebootController", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_RebootController(int nCtrlID);

        // Enable position trigger (PT) function in the axis' corresponding slave drive.
        [DllImport(DllName, EntryPoint = "HIMC_EnablePT", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_EnablePT(int nCtrlID, int nAxisID);
        // Disable position trigger (PT) function in the axis' corresponding slave drive.
        [DllImport(DllName, EntryPoint = "HIMC_DisablePT", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_DisablePT(int nCtrlID, int nAxisID);
        // Set position trigger configuration
        [DllImport(DllName, EntryPoint = "HIMC_SetPosTriggerConfig", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetPosTriggerConfig(int nCtrlID, int nAxisID, PosTriggerPar pos_trigger_par);
        // Query whether the position trigger function is enabled in the axis' corresponding slave.
        [DllImport(DllName, EntryPoint = "HIMC_IsPTEnabled", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_IsPTEnabled(int nCtrlID, int nAxisID, ref int bIsPTEnable);

        // Set HMPL event call back function .
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetHmplEvtCallback(int nID, [MarshalAs(UnmanagedType.FunctionPtr)] HMPLEventCBFuncPtr callbackPointer);
        // Set HMPL error call back function .
        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetErrorCallback(int nID, [MarshalAs(UnmanagedType.FunctionPtr)] HIMCErrorCBFuncPtr callbackPointer);
        // Get last error code .
        [DllImport(DllName, EntryPoint = "HIMC_GetLastError", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetLastError(int nCtrlID, ref int nErrorCode);
        // Get error name and description by error id .
        [DllImport(DllName, EntryPoint = "HIMC_GetErrorInformation", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetErrorInformation(int nErrorID, byte[] strErrorName, int nNameLen, ref int nActualNameLength,
                                                           byte[] strDescription, int nDescriptionLen, ref int nActualDescriptionLength);

        #region HIMC I/O Control
        // Get number of slave
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveNum(int nCtrlID, ref int nNumOfSlave);
        // Query if the specific general purpose input status of controller is on.
        [DllImport(DllName, EntryPoint = "HIMC_GetHimcGpi", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetHimcGpi(int nCtrlID, int nGPIOIdx, ref byte nOutput);
        // Query if the specific general purpose output status of controller is on.
        [DllImport(DllName, EntryPoint = "HIMC_GetHimcGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetHimcGpo(int nCtrlID, int nGPIOIdx, ref byte nOutput);
        // Set controller general purpose output status.
        [DllImport(DllName, EntryPoint = "HIMC_SetHimcGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetHimcGpo(int nCtrlID, int nGPIOIdx, byte nInput);
        // Get a list of HIMC input
        [DllImport(DllName, EntryPoint = "HIMC_GetHimcMultiGpi", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetHimcMultiGpi(int nCtrlID, int nStartIdx, int nLength, byte[] OutputArray);
        // Get a list of HIMC output
        [DllImport(DllName, EntryPoint = "HIMC_GetHimcMultiGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetHimcMultiGpo(int nCtrlID, int nStartIdx, int nLength, byte[] OutputArray);
        // Set a list of HIMC output
        [DllImport(DllName, EntryPoint = "HIMC_SetHimcMultiGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetHimcMultiGpo(int nCtrlID, int nStartIdx, int nLength, byte[] OutputArray);
        // Get number of HIMC output
        [DllImport(DllName, EntryPoint = "HIMC_GetHimcGpiNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetHimcGpiNum(int nCtrlID, ref int nNumOfGPI);
        // Get number of HIMC input
        [DllImport(DllName, EntryPoint = "HIMC_GetHimcGpoNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetHimcGpoNum(int nCtrlID, ref int nNumOfGPO);

        #endregion // HIMC I/O Control



        #region Slave(Drive, HIOM, ...) I/O Control
        // Macro for querying slave general input.
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveGpi", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveGpi(int nCtrlID, int nSlaveID, int nGPIOIdx, ref byte nOutput);
        // Macro for querying slave general output.
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveGpo(int nCtrlID, int nSlaveID, int nGPIOIdx, ref byte nOutput);
        // Get number of slave input
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveGpiNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveGpiNum(int nCtrlID, int nSlaveID, ref int nNumOfSlaveGpi);
        // Get number of slave output
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveGpoNum", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveGpoNum(int nCtrlID, int nSlaveID, ref int nNumOfSlaveGpo);
        // Set slave general purpose output status.
        [DllImport(DllName, EntryPoint = "HIMC_SetSlaveGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetSlaveGpo(int nCtrlID, int nSlaveID, int nGPIOIdx, byte nInput);
        // Get a list of slave input
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveMultiGpi", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveMultiGpi(int nCtrlID, int nSlaveID, int nStartIdx, int nlength, byte[] OutputArray);
        // Get a list of slave output
        [DllImport(DllName, EntryPoint = "HIMC_GetSlaveMultiGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_GetSlaveMultiGpo(int nCtrlID, int nSlaveID, int nStartIdx, int nlength, byte[] OutputArray);
        // Set a list of slave output
        [DllImport(DllName, EntryPoint = "HIMC_SetSlaveMultiGpo", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int HIMC_SetSlaveMultiGpo(int nCtrlID, int nSlaveID, int nStartIdx, int nlength, byte[] OutputArray);

        #endregion // Slave(Drive, HIOM, ...) I/O Control
    }
}
