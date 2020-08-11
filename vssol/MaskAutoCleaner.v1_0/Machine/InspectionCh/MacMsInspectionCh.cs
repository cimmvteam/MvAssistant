using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.InspectionCh
{
    public class MacMsInspectionCh : MacMachineStateBase
    {
        public EnumMacMsInspectionChState CurrentWorkState { get; set; }

        private IMacHalInspectionCh HalInspectionCh { get { return this.halAssembly as IMacHalInspectionCh; } }

        public object EnumMacMsInspectionCh { get; private set; }

        public MacMsInspectionCh() { LoadStateMachine(); }

        TimeOutController timeoutObj = new TimeOutController();
        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsInspectionChState.Start);
            MacState sInitial = NewState(EnumMacMsInspectionChState.Initial);

            MacState sIdle = NewState(EnumMacMsInspectionChState.Idle);
            MacState sWaitingForPutIntoMask = NewState(EnumMacMsInspectionChState.WaitingForInputMask);
            MacState sMaskOnStage = NewState(EnumMacMsInspectionChState.MaskOnStage);
            MacState sDefensingMask = NewState(EnumMacMsInspectionChState.DefensingMask);
            MacState sInspectingMask = NewState(EnumMacMsInspectionChState.InspectingMask);
            MacState sMaskOnStageInspected = NewState(EnumMacMsInspectionChState.MaskOnStageInspected);
            MacState sWaitingForReleaseMask = NewState(EnumMacMsInspectionChState.WaitingForReleaseMask);

            MacState sWaitingForPutIntoGlass = NewState(EnumMacMsInspectionChState.WaitingForInputGlass);
            MacState sGlassOnStage = NewState(EnumMacMsInspectionChState.GlassOnStage);
            MacState sDefensingGlass = NewState(EnumMacMsInspectionChState.DefensingGlass);
            MacState sInspectingGlass = NewState(EnumMacMsInspectionChState.InspectingGlass);
            MacState sGlassOnStageInspected = NewState(EnumMacMsInspectionChState.GlassOnStageInspected);
            MacState sWaitingForReleaseGlass = NewState(EnumMacMsInspectionChState.WaitingForReleaseGlass);
            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsInspectionChTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsInspectionChTransition.Initial);

            MacTransition tIdle_WaitingForPutIntoMask = NewTransition(sIdle, sWaitingForPutIntoMask, EnumMacMsInspectionChTransition.WaitForInputMask);
            MacTransition tWaitingForPutIntoMask_MaskOnStage = NewTransition(sWaitingForPutIntoMask, sMaskOnStage, EnumMacMsInspectionChTransition.StandbyAtStageWithMask);
            MacTransition tMaskOnStage_DefensingMask = NewTransition(sMaskOnStage, sDefensingMask, EnumMacMsInspectionChTransition.DefenseMask);
            MacTransition tDefensingMask_InspectingMask = NewTransition(sDefensingMask, sInspectingMask, EnumMacMsInspectionChTransition.InspectMask);
            MacTransition tInspectingMask_MaskOnStageInspected = NewTransition(sInspectingMask, sMaskOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithMaskInspected);
            MacTransition tMaskOnStageInspected_WaitingForReleaseMask = NewTransition(sMaskOnStageInspected, sWaitingForReleaseMask, EnumMacMsInspectionChTransition.WaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_Idle = NewTransition(sWaitingForReleaseMask, sIdle, EnumMacMsInspectionChTransition.ReturnToIdleFromReleaseMask);

            MacTransition tIdle_WaitingForPutIntoGlass = NewTransition(sIdle, sWaitingForPutIntoGlass, EnumMacMsInspectionChTransition.WaitForInputGlass);
            MacTransition tWaitingForPutIntoGlass_GlassOnStage = NewTransition(sWaitingForPutIntoGlass, sGlassOnStage, EnumMacMsInspectionChTransition.StandbyAtStageWithGlass);
            MacTransition tGlassOnStage_DefensingGlass = NewTransition(sGlassOnStage, sDefensingGlass, EnumMacMsInspectionChTransition.DefenseGlass);
            MacTransition tDefensingGlass_InspectingGlass = NewTransition(sDefensingGlass, sInspectingGlass, EnumMacMsInspectionChTransition.InspectGlass);
            MacTransition tInspectingGlass_GlassOnStageInspected = NewTransition(sInspectingGlass, sGlassOnStageInspected, EnumMacMsInspectionChTransition.StandbyAtStageWithGlassInspected);
            MacTransition tGlassOnStageInspected_WaitingForReleaseGlass = NewTransition(sGlassOnStageInspected, sWaitingForReleaseGlass, EnumMacMsInspectionChTransition.WaitForReleaseGlass);
            MacTransition tWaitingForReleaseGlass_Idle = NewTransition(sWaitingForReleaseGlass, sIdle, EnumMacMsInspectionChTransition.ReturnToIdleFromReleaseGlass);
            #endregion Transition

            #region State Register OnEntry OnExit
            sStart.OnEntry += (sender, e) =>
            { };
            sStart.OnExit += (sender, e) =>
            { };
            sInitial.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.Initial)
                        {
                            try
                            {
                                HalInspectionCh.Initial();
                                transition = tInitial_Idle;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInitial.OnExit += (sender, e) =>
            { };

            sIdle.OnEntry += (sender, e) =>
            { };
            sIdle.OnExit += (sender, e) =>
            { };
            sWaitingForPutIntoMask.OnEntry += (sender, e) =>
            { };
            sWaitingForPutIntoMask.OnExit += (sender, e) =>
            { };
            sMaskOnStage.OnEntry += (sender, e) =>
            { };
            sMaskOnStage.OnExit += (sender, e) =>
            { };
            sDefensingMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.DefensingMask)
                        {
                            try
                            {
                                HalInspectionCh.WPosition(51);
                                HalInspectionCh.Camera_SideDfs_Cap();// TODO: 拍照進行檢測
                                HalInspectionCh.Camera_TopDfs_Cap();// TODO: 拍照進行檢測
                                // TODO: 其他位置或角度的影像檢測
                                transition = tDefensingMask_InspectingMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sDefensingMask.OnExit += (sender, e) =>
            { };
            sInspectingMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.InspectingMask)
                        {
                            try
                            {
                                HalInspectionCh.WPosition(51);
                                HalInspectionCh.XYPosition(300, 200);
                                HalInspectionCh.Camera_SideInsp_Cap();// TODO: 拍照進行檢測
                                HalInspectionCh.Camera_TopInsp_Cap();// TODO: 拍照進行檢測
                                // TODO: 其他位置或角度的影像檢測
                                transition = tInspectingMask_MaskOnStageInspected;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInspectingMask.OnExit += (sender, e) =>
            { };
            sMaskOnStageInspected.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.MaskOnStageInspected)
                        {
                            try
                            {
                                HalInspectionCh.XYPosition(10, 20);// TODO: 移到放入Mask的位置
                                transition = tMaskOnStageInspected_WaitingForReleaseMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sMaskOnStageInspected.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseMask.OnEntry += (sender, e) =>
            { };
            sWaitingForReleaseMask.OnExit += (sender, e) =>
            { };

            sWaitingForPutIntoGlass.OnEntry += (sender, e) =>
            { };
            sWaitingForPutIntoGlass.OnExit += (sender, e) =>
            { };
            sGlassOnStage.OnEntry += (sender, e) =>
            { };
            sGlassOnStage.OnExit += (sender, e) =>
            { };
            sDefensingGlass.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.DefensingGlass)
                        {
                            try
                            {
                                HalInspectionCh.WPosition(51);
                                HalInspectionCh.Camera_SideDfs_Cap();// TODO: 拍照進行檢測
                                HalInspectionCh.Camera_TopDfs_Cap();// TODO: 拍照進行檢測
                                // TODO: 其他位置或角度的影像檢測
                                transition = tDefensingGlass_InspectingGlass;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sDefensingGlass.OnExit += (sender, e) =>
            { };
            sInspectingGlass.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.InspectingGlass)
                        {
                            try
                            {
                                HalInspectionCh.WPosition(51);
                                HalInspectionCh.XYPosition(300, 200);
                                HalInspectionCh.Camera_SideInsp_Cap();// TODO: 拍照進行檢測
                                HalInspectionCh.Camera_TopInsp_Cap();// TODO: 拍照進行檢測
                                // TODO: 其他位置或角度的影像檢測
                                transition = tInspectingGlass_GlassOnStageInspected;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sInspectingGlass.OnExit += (sender, e) =>
            { };
            sGlassOnStageInspected.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsInspectionChState.GlassOnStageInspected)
                        {
                            try
                            {
                                HalInspectionCh.XYPosition(10, 20);// TODO: 移到放入Mask的位置
                                transition = tGlassOnStageInspected_WaitingForReleaseGlass;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 60))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sGlassOnStageInspected.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseGlass.OnEntry += (sender, e) =>
            { };
            sWaitingForReleaseGlass.OnExit += (sender, e) =>
            { };
            #endregion State Register OnEntry OnExit
        }

        public class TimeOutController
        {
            public bool IsTimeOut(DateTime startTime, int targetDiffSecs)
            {
                var thisTime = DateTime.Now;
                var diff = thisTime.Subtract(startTime).TotalSeconds;
                if (diff >= targetDiffSecs)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public bool IsTimeOut(DateTime startTime)
            {
                return IsTimeOut(startTime, 20);
            }
        }
    }
}
