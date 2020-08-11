using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.OpenStage
{
    public class MacMsOpenStage : MacMachineStateBase
    {
        public EnumMacMsOpenStageState CurrentWorkState { get; set; }

        private IMacHalOpenStage HalOpenStage { get { return this.halAssembly as IMacHalOpenStage; } }

        public MacMsOpenStage() { LoadStateMachine(); }

        TimeOutController timeoutObj = new TimeOutController();
        public override void LoadStateMachine()
        {
            #region State
            MacState sStart = NewState(EnumMacMsOpenStageState.Start);
            MacState sInitial = NewState(EnumMacMsOpenStageState.Initial);

            MacState sIdle = NewState(EnumMacMsOpenStageState.Idle);
            MacState sWaitingForInputBox = NewState(EnumMacMsOpenStageState.WaitingForInputBox);
            MacState sClosedBox = NewState(EnumMacMsOpenStageState.ClosedBox);
            MacState sWaitingForUnlock = NewState(EnumMacMsOpenStageState.WaitingForUnlock);
            MacState sOpeningBox = NewState(EnumMacMsOpenStageState.OpeningBox);
            MacState sOpenedBox = NewState(EnumMacMsOpenStageState.OpenedBox);
            MacState sWaitingForInputMask = NewState(EnumMacMsOpenStageState.WaitingForInputMask);
            MacState sOpenedBoxWithMaskForClose = NewState(EnumMacMsOpenStageState.OpenedBoxWithMaskForClose);
            MacState sClosingBoxWithMask = NewState(EnumMacMsOpenStageState.ClosingBoxWithMask);
            MacState sWaitingForLockWithMask = NewState(EnumMacMsOpenStageState.WaitingForLockWithMask);
            MacState sClosedBoxWithMaskForRelease = NewState(EnumMacMsOpenStageState.ClosedBoxWithMaskForRelease);
            MacState sWaitingForReleaseBoxWithMask = NewState(EnumMacMsOpenStageState.WaitingForReleaseBoxWithMask);

            MacState sWaitingForInputBoxWithMask = NewState(EnumMacMsOpenStageState.WaitingForInputBoxWithMask);
            MacState sClosedBoxWithMask = NewState(EnumMacMsOpenStageState.ClosedBoxWithMask);
            MacState sWaitingForUnlockWithMask = NewState(EnumMacMsOpenStageState.WaitingForUnlickWithMask);
            MacState sOpeningBoxWithMask = NewState(EnumMacMsOpenStageState.OpeningBoxWithMask);
            MacState sOpenedBoxWithMask = NewState(EnumMacMsOpenStageState.OpenedBoxWithMask);
            MacState sWaitingForReleaseMask = NewState(EnumMacMsOpenStageState.WaitingForReleaseMask);
            MacState sOpenedBoxForClose = NewState(EnumMacMsOpenStageState.OpenedBoxForClose);
            MacState sClosingBox = NewState(EnumMacMsOpenStageState.ClosingBox);
            MacState sWaitingForLock = NewState(EnumMacMsOpenStageState.WaitingForLock);
            MacState sClosedBoxForRelease = NewState(EnumMacMsOpenStageState.ClosedBoxForRelease);
            MacState sWaitingForReleaseBox = NewState(EnumMacMsOpenStageState.WaitingForReleaseBox);

            #endregion State

            #region Transition
            MacTransition tStart_Initial = NewTransition(sStart, sInitial, EnumMacMsOpenStageTransition.PowerON);
            MacTransition tInitial_Idle = NewTransition(sStart, sIdle, EnumMacMsOpenStageTransition.Initial);

            MacTransition tIdle_WaitingForInputBox = NewTransition(sIdle, sWaitingForInputBox, EnumMacMsOpenStageTransition.WaitForInputBox);
            MacTransition tWaitingForInputBox_ClosedBox = NewTransition(sWaitingForInputBox, sClosedBox, EnumMacMsOpenStageTransition.StandbyAtClosedBoxFromIdle);
            MacTransition tClosedBox_WaitingForUnlock = NewTransition(sClosedBox, sWaitingForUnlock, EnumMacMsOpenStageTransition.WaitForUnlock);
            MacTransition tWaitingForUnlock_OpeningBox = NewTransition(sWaitingForUnlock, sOpeningBox, EnumMacMsOpenStageTransition.OpenBox);
            MacTransition tOpeningBox_OpenedBox = NewTransition(sOpeningBox, sOpenedBox, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxFromClosedBox);
            MacTransition tOpenedBox_WaitingForInputMask = NewTransition(sOpenedBox, sWaitingForInputMask, EnumMacMsOpenStageTransition.WaitForInputMask);
            MacTransition tWaitingForInputMask_OpenedBoxWithMaskForClose = NewTransition(sWaitingForInputMask, sOpenedBoxWithMaskForClose, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxWithMaskFromOpenedBox);
            MacTransition tOpenedBoxWithMaskForClose_ClosingBoxWithMask = NewTransition(sOpenedBoxWithMaskForClose, sClosingBoxWithMask, EnumMacMsOpenStageTransition.CloseBoxWithMask);
            MacTransition tClosingBoxWithMask_WaitingForLockWithMask = NewTransition(sClosingBoxWithMask, sWaitingForLockWithMask, EnumMacMsOpenStageTransition.WaitForLockWithMask);
            MacTransition tWaitingForLockWithMask_ClosedBoxWithMaskForRelease = NewTransition(sWaitingForLockWithMask, sClosedBoxWithMaskForRelease, EnumMacMsOpenStageTransition.StandbyAtClosedBoxWithMaskFromOpenedBoxWithMask);
            MacTransition tClosedBoxWithMaskForRelease_WaitingForReleaseBoxWithMask = NewTransition(sClosedBoxWithMaskForRelease, sWaitingForReleaseBoxWithMask, EnumMacMsOpenStageTransition.WaitForReleaseBoxWithMask);
            MacTransition tWaitingForReleaseBoxWithMask_Idle = NewTransition(sWaitingForReleaseBoxWithMask, sIdle, EnumMacMsOpenStageTransition.ReturnToIdleFromClosedBoxWithMask);

            MacTransition tIdle_WaitingForInputBoxWithMask = NewTransition(sIdle, sWaitingForInputBoxWithMask, EnumMacMsOpenStageTransition.WaitForInputBoxWithMask);
            MacTransition tWaitingForInputBoxWithMask_ClosedBoxWithMask = NewTransition(sWaitingForInputBoxWithMask, sClosedBoxWithMask, EnumMacMsOpenStageTransition.StandbyAtClosedBoxWithMaskFromIdle);
            MacTransition tClosedBoxWithMask_WaitingForUnlockWithMask = NewTransition(sClosedBoxWithMask, sWaitingForUnlockWithMask, EnumMacMsOpenStageTransition.WaitForUnlockWithMask);
            MacTransition tWaitingForUnlockWithMask_OpeningBoxWithMask = NewTransition(sWaitingForUnlockWithMask, sOpeningBoxWithMask, EnumMacMsOpenStageTransition.OpenBoxWithMask);
            MacTransition tOpeningBoxWithMask_OpenedBoxWithMask = NewTransition(sOpeningBoxWithMask, sOpenedBoxWithMask, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxWithMaskFromClosedBoxWithMask);
            MacTransition tOpenedBoxWithMask_WaitingForReleaseMask = NewTransition(sOpenedBoxWithMask, sWaitingForReleaseMask, EnumMacMsOpenStageTransition.WaitForReleaseMask);
            MacTransition tWaitingForReleaseMask_OpenedBoxForClose = NewTransition(sWaitingForReleaseMask, sOpenedBoxForClose, EnumMacMsOpenStageTransition.StandbyAtOpenedBoxFromOpenedBoxWithMask);
            MacTransition tOpenedBoxForClose_ClosingBox = NewTransition(sOpenedBoxForClose, sClosingBox, EnumMacMsOpenStageTransition.CloseBox);
            MacTransition tClosingBox_WaitingForLock = NewTransition(sClosingBox, sWaitingForLock, EnumMacMsOpenStageTransition.WaitForLock);
            MacTransition tWaitingForLock_ClosedBoxForRelease = NewTransition(sWaitingForLock, sClosedBoxForRelease, EnumMacMsOpenStageTransition.StandbyAtClosedBoxFromOpenedBox);
            MacTransition tClosedBoxForRelease_WaitingForReleaseBox = NewTransition(sClosedBoxForRelease, sWaitingForReleaseBox, EnumMacMsOpenStageTransition.WaitForReleaseBox);
            MacTransition tWaitingForReleaseBox_Idle = NewTransition(sWaitingForReleaseBox, sIdle, EnumMacMsOpenStageTransition.ReturnToIdleFromClosedBox);
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
                        if (CurrentWorkState == EnumMacMsOpenStageState.Initial)
                        {
                            try
                            {
                                HalOpenStage.Initial();
                                transition = tInitial_Idle;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
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
            sWaitingForInputBox.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            var BoxWeight = HalOpenStage.ReadWeightOnStage();
                            if (BoxWeight > 285)
                            {
                                transition = tWaitingForInputBox_ClosedBox;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForInputBox.OnExit += (sender, e) =>
            { };
            sClosedBox.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.ClosedBox)
                        {
                            try
                            {
                                var BoxType = (uint)e.Parameter;
                                var BoxWeight = HalOpenStage.ReadWeightOnStage();
                                if (BoxType == 1)
                                {
                                    if (BoxWeight < 775 || BoxWeight > 778)
                                        throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                                }
                                else if (BoxType == 2)
                                {
                                    if (BoxWeight < 589 || BoxWeight > 590)
                                        throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                                }
                                if (HalOpenStage.ReadCoverSensor().Item2 == false)
                                    throw new Exception("Box status was not closed");
                                transition = tClosedBox_WaitingForUnlock;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sClosedBox.OnExit += (sender, e) =>
            { };
            sWaitingForUnlock.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            if (false)// TODO: 使用CCD檢查扣子是否扣上，如果扣子有被解開
                            {
                                transition = tWaitingForUnlock_OpeningBox;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForUnlock.OnExit += (sender, e) =>
            { };
            sOpeningBox.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.OpeningBox)
                        {
                            try
                            {
                                HalOpenStage.Close();
                                HalOpenStage.Clamp();
                                HalOpenStage.Open();
                                transition = tOpeningBox_OpenedBox;
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
            sOpeningBox.OnExit += (sender, e) =>
            { };
            sOpenedBox.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.OpenedBox)
                        {
                            try
                            {
                                if (HalOpenStage.ReadCoverSensor().Item1 == false)
                                    throw new Exception("Box status was not opened");
                                transition = tOpenedBox_WaitingForInputMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sOpenedBox.OnExit += (sender, e) =>
            { };
            sWaitingForInputMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            var BoxWeight = HalOpenStage.ReadWeightOnStage();
                            if ((BoxWeight >= 1102 && BoxWeight <= 1104) || (BoxWeight >= 918 && BoxWeight <= 920))
                            {
                                transition = tWaitingForInputBox_ClosedBox;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForInputMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxWithMaskForClose.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.OpenedBoxWithMaskForClose)
                        {
                            try
                            {
                                if (HalOpenStage.ReadCoverSensor().Item1 == false)
                                    throw new Exception("Box status was not opened");
                                transition = tOpenedBoxWithMaskForClose_ClosingBoxWithMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sOpenedBoxWithMaskForClose.OnExit += (sender, e) =>
            { };
            sClosingBoxWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.ClosingBoxWithMask)
                        {
                            try
                            {
                                HalOpenStage.Close();
                                HalOpenStage.Unclamp();
                                HalOpenStage.Lock();
                                transition = tClosingBoxWithMask_WaitingForLockWithMask;
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
            sClosingBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForLockWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            if (false)// TODO: 使用CCD檢查扣子是否扣上，如果扣子有被鎖上
                            {
                                transition = tWaitingForLockWithMask_ClosedBoxWithMaskForRelease;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForLockWithMask.OnExit += (sender, e) =>
            { };
            sClosedBoxWithMaskForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.ClosedBoxWithMaskForRelease)
                        {
                            try
                            {
                                var BoxType = (uint)e.Parameter;
                                var BoxWeight = HalOpenStage.ReadWeightOnStage();
                                if (BoxType == 1)
                                {
                                    if (BoxWeight < 1102 || BoxWeight > 1104)
                                        throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                                }
                                else if (BoxType == 2)
                                {
                                    if (BoxWeight < 918 || BoxWeight > 920)
                                        throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                                }
                                if (HalOpenStage.ReadCoverSensor().Item2 == false)
                                    throw new Exception("Box status was not closed");
                                transition = tClosedBoxWithMaskForRelease_WaitingForReleaseBoxWithMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sClosedBoxWithMaskForRelease.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseBoxWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            var BoxWeight = HalOpenStage.ReadWeightOnStage();
                            if (BoxWeight <= 285)
                            {
                                transition = tWaitingForReleaseBoxWithMask_Idle;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForReleaseBoxWithMask.OnExit += (sender, e) =>
            { };



            sWaitingForInputBoxWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            var BoxWeight = HalOpenStage.ReadWeightOnStage();
                            if (BoxWeight > 285)
                            {
                                transition = tWaitingForInputBoxWithMask_ClosedBoxWithMask;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForInputBoxWithMask.OnExit += (sender, e) =>
            { };
            sClosedBoxWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.ClosedBoxWithMask)
                        {
                            try
                            {
                                var BoxType = (uint)e.Parameter;
                                var BoxWeight = HalOpenStage.ReadWeightOnStage();
                                if (BoxType == 1)
                                {
                                    if (BoxWeight < 1102 || BoxWeight > 1104)
                                        throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                                }
                                else if (BoxType == 2)
                                {
                                    if (BoxWeight < 918 || BoxWeight > 920)
                                        throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                                }
                                if (HalOpenStage.ReadCoverSensor().Item2 == false)
                                    throw new Exception("Box status was not closed");
                                transition = tClosedBoxWithMask_WaitingForUnlockWithMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sClosedBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForUnlockWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            if (false)// TODO: 使用CCD檢查扣子是否扣上，如果扣子有被解開
                            {
                                transition = tWaitingForUnlockWithMask_OpeningBoxWithMask;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForUnlockWithMask.OnExit += (sender, e) =>
            { };
            sOpeningBoxWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.OpeningBoxWithMask)
                        {
                            try
                            {
                                HalOpenStage.Close();
                                HalOpenStage.Clamp();
                                HalOpenStage.Open();
                                transition = tOpeningBoxWithMask_OpenedBoxWithMask;
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
            sOpeningBoxWithMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxWithMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.OpenedBoxWithMask)
                        {
                            try
                            {
                                if (HalOpenStage.ReadCoverSensor().Item1 == false)
                                    throw new Exception("Box status was not opened");
                                transition = tOpenedBoxWithMask_WaitingForReleaseMask;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sOpenedBoxWithMask.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseMask.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            var BoxWeight = HalOpenStage.ReadWeightOnStage();
                            if (BoxWeight <= 285)
                            {
                                transition = tWaitingForReleaseMask_OpenedBoxForClose;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForReleaseMask.OnExit += (sender, e) =>
            { };
            sOpenedBoxForClose.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.OpenedBoxForClose)
                        {
                            try
                            {
                                if (HalOpenStage.ReadCoverSensor().Item1 == false)
                                    throw new Exception("Box status was not opened");
                                transition = tOpenedBoxForClose_ClosingBox;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sOpenedBoxForClose.OnExit += (sender, e) =>
            { };
            sClosingBox.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.ClosingBox)
                        {
                            try
                            {
                                HalOpenStage.Close();
                                HalOpenStage.Unclamp();
                                HalOpenStage.Lock();
                                transition = tClosingBox_WaitingForLock;
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
            sClosingBox.OnExit += (sender, e) =>
            { };
            sWaitingForLock.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            if (false)// TODO: 使用CCD檢查扣子是否扣上，如果扣子有被鎖上
                            {
                                transition = tWaitingForLock_ClosedBoxForRelease;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForLock.OnExit += (sender, e) =>
            { };
            sClosedBoxForRelease.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        if (CurrentWorkState == EnumMacMsOpenStageState.ClosedBoxForRelease)
                        {
                            try
                            {
                                var BoxType = (uint)e.Parameter;
                                var BoxWeight = HalOpenStage.ReadWeightOnStage();
                                if (BoxType == 1)
                                {
                                    if (BoxWeight < 775 || BoxWeight > 778)
                                        throw new Exception("Wrong iron box weight, box weight = " + BoxWeight.ToString());
                                }
                                else if (BoxType == 2)
                                {
                                    if (BoxWeight < 589 || BoxWeight > 590)
                                        throw new Exception("Wrong crystal box weight, box weight = " + BoxWeight.ToString());
                                }
                                if (HalOpenStage.ReadCoverSensor().Item2 == false)
                                    throw new Exception("Box status was not closed");
                                transition = tClosedBoxForRelease_WaitingForReleaseBox;
                                break;
                            }
                            catch (Exception)
                            {
                                // TODO
                                break;
                            }
                        }
                        if (timeoutObj.IsTimeOut(thisTime, 10))
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sClosedBoxForRelease.OnExit += (sender, e) =>
            { };
            sWaitingForReleaseBox.OnEntry += (sender, e) =>
            {
                var thisState = (MacState)sender;
                MacTransition transition = null;
                DateTime thisTime = DateTime.Now;
                Action guard = () =>
                {
                    while (true)
                    {
                        try
                        {
                            var BoxWeight = HalOpenStage.ReadWeightOnStage();
                            if (BoxWeight <= 285)
                            {
                                transition = tWaitingForReleaseBox_Idle;
                                break;
                            }
                        }
                        catch (Exception)
                        {
                            // TODO
                            break;
                        }
                        Thread.Sleep(10);
                    }
                };
                new Task(guard).Start();
            };
            sWaitingForReleaseBox.OnExit += (sender, e) =>
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
