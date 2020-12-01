using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerQueues;
using MaskAutoCleaner.v1_0.Machine.Cabinet.DrawerStatus;
using MaskAutoCleaner.v1_0.Machine.CabinetDrawer;
using MaskAutoCleaner.v1_0.Machine.Drawer;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MaskAutoCleaner.v1_0.StateMachineExceptions;
using MvAssistant.Mac.v1_0.Hal.Assembly;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using MvAssistant.Mac.v1_0.JSon.RobotTransferFile;
using MvAssistant.Mac.v1_0.Manifest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{
    [Guid("11111111-1111-1111-1111-111111111111")]// TODO: UPdate this Guid
    public class MacMsCabinet : MacMachineStateBase
    {
        
        /// <summary>Initial 所有(或特定幾個) Drawers 的管物件</summary>        
        private CabinetInitialStatus DrawersInitialStatus = null;
       
        /// <summary>Bank out 時 將所有(或特定幾個) Drawer 移回到 Out 時</summary>
        private DrawerMoveTrayToOutStatus DrawersMoveTrayToOutStatus = null;
        
        /// <summary>BankOut 或 BankIn 或 Dont Care 時的旗標</summary>
        private CabinetDuration CabinetDuration = CabinetDuration.DontCare.Reset();


        QueueBankOutWaitingToClampBoxDrawers QueueBankOutWaitingToClampBoxDrawers = null;


        private static readonly object lockObj =new object();
        private static MacMsCabinet _instance = null;
      
        public static MacMsCabinet GetInstance()
        {
            if (_instance == null)
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new MacMsCabinet();
                    }
                }
            }
            return _instance;
        }
       
        Dictionary<BoxrobotTransferLocation, DrawerBoxInfo> dicDrawerAndBoxInfos = null;
        private readonly static object GetDrawerLockObj = new object();
        /// <summary>取得所有的 Drawer 與 BoxrobotTransferLocation 的  Dictionary</summary>
        /// <returns></returns>
        public Dictionary<BoxrobotTransferLocation, DrawerBoxInfo> GetDicMacHalDrawers()
        {
               if (dicDrawerAndBoxInfos == null)
                {
                    lock (GetDrawerLockObj)
                    {
                        if (dicDrawerAndBoxInfos == null)
                        {
                            dicDrawerAndBoxInfos = new Dictionary<BoxrobotTransferLocation, DrawerBoxInfo>();
                            var drawerIdRange = MacEnumDevice.boxtransfer_assembly.GetDrawerRange();
                            for (var i = drawerIdRange.StartID; i <= drawerIdRange.EndID; i++)
                            {
                               try
                               {
                                   var drawer = this.halAssembly.Hals[i.ToString()] as IMacHalDrawer;
                                   var drawerBox = new DrawerBoxInfo(i, drawer);
                                   var drawerLocation = i.ToBoxrobotTransferLocation();
                                   dicDrawerAndBoxInfos.Add(drawerLocation, drawerBox);
                                  
                                }
                               catch(Exception ex)
                               {

                               }
                            }
                            BindDrawerEventHandler();
                        }
                    }
                }
                return dicDrawerAndBoxInfos;
        }

        /// <summary>取得指定的 State Machine</summary>
        /// <param name="machineId"></param>
        /// <param name="dicStateMachines"></param>
        /// <returns></returns>
        public static MacMsCabinetDrawer GetMacMsCabinetDrawer(EnumMachineID machineId, Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {
            if (dicStateMachines == null) { return null; }
            var rtn = dicStateMachines[machineId];
            return rtn;
        }

        public List<MacState> LoadState = null;


        #region State Machine Command
        public Dictionary<EnumMachineID, MacMsCabinetDrawer> DicCabinetDrawerStateMachines = new Dictionary<EnumMachineID, MacMsCabinetDrawer>();

        public override void SystemBootup()
        {

            /**
            Debug.WriteLine("Command: SystemBootup");
            var transition = this.Transitions[EnumMacCabinetTransition.Start_NULL.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtEntry(new MacStateEntryEventArgs(null));
           */

            
            CabinetDuration = CabinetDuration.ToSystemBootUp();
            Debug.WriteLine("Command: SystemBootup");
            var transition = this.Transitions[EnumMacCabinetTransition.Start_Idle.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtEntry(null);
        }


        /// <summary>BankOut(Load) 時, 將指定數量的 drawer Tray 移到 Out,以便將 Box 放到 Box 上</summary>
        /// <param name="drawerCounts"></param>
        public void BankOutLoadMoveTraysToOutForPutBoxOnTray(int drawerCounts)
        {

            Debug.WriteLine("Command: MoveTrayToOut, DrawerCounts: " + drawerCounts);

            CabinetDuration=CabinetDuration.ToBankOut();
            var transition = this.Transitions[EnumMacCabinetTransition.Idle_MoveTraysToOutForPutBoxOnTrayStart.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtExit(transition, null, new MoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs(drawerCounts));
        }

        /// <summary>Bank out load 時, 將指定的 Drawer (可多個, 送回 HOME)  </summary>
        /// <param name="drawer"></param>
        public void BankOutLoadMoveTraysToHomeAfterPutBoxOnTray(List<MacEnumDevice> drawerIDs)
        {
            var dic = this.GetDicMacHalDrawers();
            foreach (var drawerID in drawerIDs)
            {
                var keyValue = dic.GetKeyValue(drawerID.ToBoxrobotTransferLocation());
                
                keyValue.Value.Drawer.CommandTrayMotionHome();
            }
        }

        /// <summary>將指定的 Drawer 無條件退回到 Home</summary>
        /// <param name="devices"></param>
        public void MoveTrayToHomeNoCondition(List<MacEnumDevice> devices)
        {
            var dicDrawers = this.GetDicMacHalDrawers();
            foreach (var device in devices)
            {
                var drawerLocation = device.ToBoxrobotTransferLocation();
                if (dicDrawers.ContainsKey(drawerLocation))
                {
                    //var drawer =dicDrawers[drawerLocation]
                }
            }

        }


        /// <summary>load</summary>
        /// <param name="targetDrawerQuantity"> Drawer 數量</param>
        /// <param name="dicStateMachines">所有Drawer 的集合</param>
        public void Load_Drawers(int targetDrawerQuantity,Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {

            var states = dicStateMachines.Values.Where(m => m.CanLoad()).ToList();
            if (states.Count==0)
            {
                // 
            }
            else if (states.Count> targetDrawerQuantity)
            {
                states = states.Take(targetDrawerQuantity).ToList();
            }
            if(CurrentState != null)
            {
                CurrentState.DoExit(null);
            }
            var transition = this.Transitions[EnumMacCabinetTransition.LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng.ToString()];
            transition.StateFrom.ExecuteCommandAtEntry(new MacStateEntryEventArgs(null));
           
        }


        /// <summary>系統啟動之後 的 Initial</summary>
        /// <param name="dicCabinetDrawerStateMachines">Cabinet 之下  Drawer State Machine 的數量</param>
        public void BootupInitialDrawers(Dictionary<EnumMachineID, MacMsCabinetDrawer> dicCabinetDrawerStateMachines)
        {
            var cabinetDrawerStateMachines = dicCabinetDrawerStateMachines.Values.ToList();
            if (cabinetDrawerStateMachines.Count == 0)
            { }
            else
            {
                this.States[EnumMacCabinetState.BootupInitialDrawersStart.ToString()].ExecuteCommand(new CabinetSystemUpInitialMacStateEntryEventArgs(cabinetDrawerStateMachines));
            }
        }

        public void SynchrousDrawerStates(Dictionary<EnumMachineID, MacMsCabinetDrawer> dicStateMachines)
        {
            #region //[???] 不必檢查狀態
            #endregion
            var states = dicStateMachines.Values.ToList();
            if (states.Count == 0)
            { }
            else
            {
                this.States[EnumMacCabinetState.SynchronousDrawerStatesStart.ToString()].ExecuteCommand(new  CabinetSynchronousDrawerStatesMacStateEntryEventArgs(states));
            }
        }


        #endregion

        /// <summary>綁定 事件程序</summary>
        /// <remarks>
        /// <para>目前只綁定必要的</para>
        /// </remarks>
        void BindDrawerEventHandler()
        {
            EventHandler drawerINIOKHandler  = (sender, e)=>
            {
                var drawer = (IMacHalDrawer)sender;
                if (this.CabinetDuration.IsSystemBootupDuration() )
                {   // 整體的 SystemBootUp
                 
                    var keyValue = this.dicDrawerAndBoxInfos.GetKeyValue(drawer);
                    keyValue.Value.ResetDuration();
                }
                else if (this.CabinetDuration.IsInitialDuration())
                {
                     // 
                }
                DrawersInitialStatus.ActionOkIncrease();
            };
            EventHandler drawerINIFailedHandler = (sender, e) =>
            {
                DrawersInitialStatus.ActionFailedIncrease();
            };
            EventHandler drawerTrayArriveHomeHandler = (sender, e) =>
            {
                var drawer = (IMacHalDrawer)sender;
                if (this.DrawersInitialStatus != null && this.DrawersInitialStatus.IsActionIng())
                {
                    drawerINIOKHandler(sender, e);
                }
                else
                {
                    var keyValue = this.dicDrawerAndBoxInfos.GetKeyValue(drawer);
                    if (keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtOutForPutBoxOnTray)
                    {
                        keyValue.Value.SetDuration(DrawerDuration.BankOut_Load_TrayAtHomeWithBox);
                        // TODO 加入 QUEUE 
                        this.QueueBankOutWaitingToClampBoxDrawers.Enqueue(keyValue.Key);

                    }
                }
            };
            EventHandler drawerTrayArriveInHandler = (sender, e) =>
            {

            };

            // 到達了Out
            EventHandler drawerTrayArriveOutHandler = (sender, e) =>
            {
                if(this.DrawersMoveTrayToOutStatus != null && this.DrawersMoveTrayToOutStatus.IsActionIng())
                {
                    this.DrawersMoveTrayToOutStatus.ActionOkIncrease();
                 }
                var drawer = (IMacHalDrawer)sender;
                var keyValue = this.dicDrawerAndBoxInfos.GetKeyValue(drawer);
                if(keyValue.Value.Duration==DrawerDuration.Idle_TrayAtHome && CabinetDuration.IsBankOutDuration())
                {   // 原先在 Idle_TrayAtHome 的Drawer  && Cabinet 正在操作 整批將 Drawer Tray 移到 Out 
                    keyValue.Value.SetDuration( DrawerDuration.BankOut_Load_TrayAtOutForPutBoxOnTray);
                }
            };

            EventHandler drawerOnSysyStartUpHandler = (sender, e) =>
            {

            };

            EventHandler drawerOnButtonEventHandler = (sender, e) =>
            {

            };
            foreach (var ele in dicDrawerAndBoxInfos)
            {
                var drawer = ele.Value.Drawer;
                drawer.OnINIOKHandler -= drawerINIOKHandler;
                drawer.OnINIOKHandler += drawerINIOKHandler;

                drawer.OnINIFailedHandler -= drawerINIFailedHandler;
                drawer.OnINIFailedHandler += drawerINIFailedHandler;

                drawer.OnTrayArriveHomeHandler -= drawerTrayArriveHomeHandler;
                drawer.OnTrayArriveHomeHandler += drawerTrayArriveHomeHandler;

                drawer.OnTrayArriveOutHandler -= drawerTrayArriveOutHandler;
                drawer.OnTrayArriveOutHandler += drawerTrayArriveOutHandler;

                drawer.OnTrayArriveInHandler -= drawerTrayArriveInHandler;
                drawer.OnTrayArriveInHandler += drawerTrayArriveInHandler;

                drawer.OnSysStartUpHandler -= drawerOnSysyStartUpHandler;
                drawer.OnSysStartUpHandler += drawerOnSysyStartUpHandler;

                drawer.OnButtonEventHandler -= drawerOnButtonEventHandler;
                drawer.OnButtonEventHandler += drawerOnButtonEventHandler;

                drawer.OnDetectedEmptyBoxHandler -= null;
                drawer.OnDetectedEmptyBoxHandler += null;

                drawer.OnDetectedHasBoxHandler -= null;
                drawer.OnDetectedHasBoxHandler += null;

                drawer.OnTrayMotionFailedHandler -= null;
                drawer.OnTrayMotionFailedHandler += null;

                drawer.OnERRORErrorHandler -= null;
                drawer.OnERRORErrorHandler -= null;

                drawer.OnERRORREcoveryHandler -= null;
                drawer.OnERRORREcoveryHandler += null;

                drawer.OnPositionStatusHandler -= null;
                drawer.OnPositionStatusHandler += null;

                drawer.OnTrayMotionFailedHandler -= null;
                drawer.OnTrayMotionFailedHandler += null;

                drawer.OnTrayMotioningHandler -= null;
                drawer.OnTrayMotioningHandler += null;

                drawer.OnTrayMotionOKHandler -= null;
                drawer.OnTrayMotionOKHandler += null;

                drawer.OnTrayMotionSensorOFFHandler -= null;
                drawer.OnTrayMotionSensorOFFHandler += null;

                

            }
        }
        

        private MacMsCabinet()
        {
            if (_instance== null){
                lock (lockObj)
                {
                   if (_instance == null)
                    {
                        LoadStateMachine();
                        _instance = this;
                        QueueBankOutWaitingToClampBoxDrawers = new QueueBankOutWaitingToClampBoxDrawers();
                    }

                }
            }
            
            //_dicCabinetDrawerStates = new Dictionary<string, MacMsCabinetDrawer>();
           
        }

       
       

        public override void LoadStateMachine()
        {


            #region state
            //--------------------------------------
            MacState sIdle = NewState(EnumMacCabinetState.Idle); // 目前没事
            MacState sMoveTraysToOutForPutBoxOnTrayStart = NewState(EnumMacCabinetState.MoveTraysToOutForPutBoxOnTraysStart);// 將Drawer 移出至Out For Bank
            MacState sMoveTraysToOutForPutBoxOnTrayIng = NewState(EnumMacCabinetState.MoveTraysToOutForPutBoxOnTraysIng);// 將Drawer 移出至Out For Bank
            MacState sMoveTraysToOutForPutBoxOnTrayComplete = NewState(EnumMacCabinetState.MoveTraysToOutForPutBoxOnTraysComplete);// 將Drawer 移出至Out For Bank
            //*************************************

            MacState sStart = NewState(EnumMacCabinetState.Start);


            MacState sLoadMoveDrawerTraysToOutStart = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutStart);
            MacState sLoadMoveDrawerTraysToOutIng = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutIng);
            MacState sLoadMoveDrawerTraysToOutComplete = NewState(EnumMacCabinetState.LoadMoveDrawerTraysToOutComplete);

            MacState sBootupInitialDrawersStart = NewState(EnumMacCabinetState.BootupInitialDrawersStart);
            MacState sBootupInitialDrawersIng = NewState(EnumMacCabinetState.BootupInitialDrawersIng);
            MacState sBootupInitialDrawersComplete = NewState(EnumMacCabinetState.BootupInitialDrawersComplete);

            MacState sSynchronousDrawerStatesStart = NewState(EnumMacCabinetState.SynchronousDrawerStatesStart);
            MacState sSynchronousDrawerStatesIng = NewState(EnumMacCabinetState.SynchronousDrawerStatesIng);
            MacState sSynchronousDrawerStatesComplete = NewState(EnumMacCabinetState.SynchronousDrawerStatesComplete);
            #endregion state

            #region transition
            //----------------------
            MacTransition tStart_Idle = NewTransition(sStart, sIdle, EnumMacCabinetTransition.Start_Idle);
            MacTransition tIdle_NULL = NewTransition(sIdle, null, EnumMacCabinetTransition.Idle_NULL);

            MacTransition tIdle_MoveTraysToOutForPutBoxOnTrayStart = NewTransition(sIdle, sMoveTraysToOutForPutBoxOnTrayStart, EnumMacCabinetTransition.Idle_MoveTraysToOutForPutBoxOnTrayStart);
            MacTransition tMoveTraysToOutForPutBoxOnTrayStart_MoveTraysToOutForPutBoxOnTrayIng = NewTransition(sMoveTraysToOutForPutBoxOnTrayStart, sMoveTraysToOutForPutBoxOnTrayIng, EnumMacCabinetTransition.MoveTraysToOutForPutBoxOnTrayStart_MoveTraysToOutForPutBoxOnTrayIng);
            MacTransition tMoveTraysToOutForPutBoxOnTrayIng_MoveTraysToOutForPutBoxOnTrayComplete = NewTransition(sMoveTraysToOutForPutBoxOnTrayIng, sMoveTraysToOutForPutBoxOnTrayComplete, EnumMacCabinetTransition.MoveTraysToOutForPutBoxOnTrayIng_MoveTraysToOutForPutBoxOnTrayComplete);
            MacTransition tMoveTraysToOutForPutBoxOnTrayComplete_Idle= NewTransition( sMoveTraysToOutForPutBoxOnTrayComplete, sIdle,EnumMacCabinetTransition.MoveTraysToOutForPutBoxOnTrayComplete_Idle);


            //******************************************
            MacTransition tStart_NULL= NewTransition(sStart, null,  EnumMacCabinetTransition.Start_NULL);

            MacTransition tLoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng = NewTransition(sLoadMoveDrawerTraysToOutStart,sLoadMoveDrawerTraysToOutIng,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng);
            MacTransition tLoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete = NewTransition(sLoadMoveDrawerTraysToOutIng, sLoadMoveDrawerTraysToOutComplete,
                                                                                                                 EnumMacCabinetTransition.LoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete);
            MacTransition tLoadMoveDrawerTraysToOutComplete_NULL = NewTransition(sLoadMoveDrawerTraysToOutComplete, null,
                                                                                                               EnumMacCabinetTransition.LoadMoveDrawerTraysToOutComplete_NULL);
            
            MacTransition tBootupInitialDrawersStart_BootupInitialDrawersIng = NewTransition(sBootupInitialDrawersStart,sBootupInitialDrawersIng,
                                                                                                                EnumMacCabinetTransition.BootupInitialDrawersStart_BootupInitialDrawersIng);
            MacTransition tBootupInitialDrawersIng_BootupInitialDrawersComplete = NewTransition(sBootupInitialDrawersIng, sBootupInitialDrawersComplete,
                                                                                                              EnumMacCabinetTransition.BootupInitialDrawersIng_BootupInitialDrawersComplete);
            MacTransition tBootupInitialDrawersComplete_NULL = NewTransition(sBootupInitialDrawersComplete, null,
                                                                                                              EnumMacCabinetTransition.BootupInitialDrawersComplete_NULL);

            
            MacTransition tSynchronousDrawerStatesStart_SynchronousDrawerStatesIng = NewTransition(sSynchronousDrawerStatesStart,sSynchronousDrawerStatesIng,
                                                                                                                EnumMacCabinetTransition.SynchronousDrawerStatesStart_SynchronousDrawerStatesIng);
            MacTransition tSynchronousDrawerStatesIng_SynchronousDrawerStatesComplete = NewTransition(sSynchronousDrawerStatesIng, sSynchronousDrawerStatesComplete,
                                                                                                               EnumMacCabinetTransition.SynchronousDrawerStatesIng_SynchronousDrawerStatesComplete);
            MacTransition tSynchronousDrawerStatesComplete_NULL = NewTransition(sSynchronousDrawerStatesComplete, null, EnumMacCabinetTransition.SynchronousDrawerStatesComplete_NULL);
            #endregion transition

            #region event
           
            sStart.OnEntry += (sender, e) =>
            {
               var drawers= GetDicMacHalDrawers();
                Debug.WriteLine("State: [sStart.OnEntry]");
                SetCurrentState((MacState)sender);
               // var initialType = (CabinetInitialType)(e.Parameter);
                DrawersInitialStatus = new CabinetInitialStatus(drawers.Count);
                var transition = tStart_Idle;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        DrawersInitialStatus.StartAction();
                        foreach (var drawer in drawers)
                        {
                            drawer.Value.Drawer.CommandINI();
                        }
                    },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => { return true; },
                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [sStart.OnExit]");
               
            };

            sIdle.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("State: [ sIdle.OnEntry]");
                SetCurrentState((MacState)sender);
                var transition = tIdle_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=>CabinetDuration.ToIdle(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => 
                    {
                       
                        DateTime thisTime = DateTime.Now;
                        while (true)
                        {
                            if (this.DrawersInitialStatus.IsActionComplete())
                            {
                                break;
                            }
                            else if (TimeoutObject.IsTimeOut(thisTime))
                            {
                                break;
                            }
                            Thread.Sleep(100);
                        }
                        //this.DrawerInitialStatus.StopInitial();
                        this.DrawersInitialStatus = null;
                        return true;
                    },
                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sIdle.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sIdle.OnExit]");
            };

            // 
            sMoveTraysToOutForPutBoxOnTrayStart.OnEntry += (sender,e) =>
            {
                Debug.WriteLine("MoveTraysToOutForPutBoxOnTrayStart.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfos = GetDicMacHalDrawers();
                
                // 要送出的 Drawer
                var drawerCounts = ((MoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs)e).RequestDrawers; // drawerCounts 要調整一下, 應該有些cDrawer 的Tray 不能移動
                this.DrawersMoveTrayToOutStatus = new DrawerMoveTrayToOutStatus(drawerCounts);
                var transition = tMoveTraysToOutForPutBoxOnTrayStart_MoveTraysToOutForPutBoxOnTrayIng;
                // 實際發送命令的 Drawer 
                var commandDrawers = new List<IMacHalDrawer>();   
                var triggerMember = new TriggerMember
                {
                    
                    Action = (parameter)=>
                    {
                        var i = 0;
                        DrawersMoveTrayToOutStatus.StartAction();
                        foreach (var info in drawerBoxInfos)
                        {
                            if (i > drawerCounts)
                            {
                                break;
                            }
                            if (info.Value.Duration == DrawerDuration.Idle_TrayAtHome)
                            {
                                info.Value.Drawer.CommandTrayMotionOut();
                                commandDrawers.Add(info.Value.Drawer);
                                i++;
                            }
                        }
                        // 重設數量, 可能可以 TrayOut 的 没有那麼多 
                        this.DrawersMoveTrayToOutStatus.ResetDrawerCount(i);
                    },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () =>  true,
                    NextStateEntryEventArgs = new MoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs(commandDrawers),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sMoveTraysToOutForPutBoxOnTrayStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sMoveTraysToOutForPutBoxOnTrayIng.OnExit]");

            };
            sMoveTraysToOutForPutBoxOnTrayIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("MoveTraysToOutForPutBoxOnTrayIng.OnEntry");
                var commandDrawers = ((MoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs)e).CommandDrawers;
                SetCurrentState((MacState)sender);
                var transition = tMoveTraysToOutForPutBoxOnTrayIng_MoveTraysToOutForPutBoxOnTrayComplete;
                var startTime = DateTime.Now;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () =>
                    {
                        while (true)
                        {
                            if (this.DrawersMoveTrayToOutStatus.IsActionComplete())
                            {
                                break;
                            }
                            if (TimeoutObject.IsTimeOut(startTime))
                            {
                                 break;
                            }
                        }
                        DrawersMoveTrayToOutStatus.StopAction();
                        return true;
                    },
                    NextStateEntryEventArgs = new MoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs(commandDrawers),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sMoveTraysToOutForPutBoxOnTrayIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sMoveTraysToOutForPutBoxOnTrayIng.OnExit]");
            };
            sMoveTraysToOutForPutBoxOnTrayComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("MoveTraysToOutForPutBoxOnTrayComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tMoveTraysToOutForPutBoxOnTrayComplete_Idle;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,

                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
            };
            sMoveTraysToOutForPutBoxOnTrayComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sMoveTraysToOutForPutBoxOnTrayIng.OnExit]");
            };




            sLoadMoveDrawerTraysToOutStart.OnEntry+=(sender, e)=>
            { // Synch
                Debug.WriteLine("LoadMoveDrawerTraysToOutStart.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveDrawerTraysToOutStart_LoadMoveDrawerTraysToOutIng;
                var args = (CabinetLoadStartMacStateEntryEventArgs)e;
               
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=>
                    {
                        var loadDrawerStates = (List<MacMsCabinetDrawer>)parameter;
                        foreach(var state in loadDrawerStates)
                        {
                            state.Load_MoveTrayToOut();
                        }
                    },
                    ActionParameter= args.LoadDrawerStates,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs =  e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sLoadMoveDrawerTraysToOutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveDrawerTraysToOutStart.OnExit");
            };

            sLoadMoveDrawerTraysToOutIng.OnEntry += (sender, e) =>
            {  // Async
                Debug.WriteLine("LoadMoveDrawerTraysToOutIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tLoadMoveDrawerTraysToOutIng_LoadMoveDrawerTraysToOutComplete;
                var args = (CabinetLoadStartMacStateEntryEventArgs)e;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;
                        var completeDrawers = args.LoadDrawerStates.Where(m => m.CurrentState == m.StateLoadWaitingPutBoxOnTray).ToList().Count();
                        var exceptionDrawers= args.LoadDrawerStates.Where(m => m.CurrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.LoadDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sLoadMoveDrawerTraysToOutIng.OnExit+= (sender, e) =>
            {
                Debug.WriteLine("LoadMoveDrawerTraysToOutIng.OnExit");
            };

            sLoadMoveDrawerTraysToOutComplete.OnEntry += (sender, e) =>
              {
                  Debug.WriteLine("LoadMoveDrawerTraysToOutComplete.OnEntry");
                  SetCurrentState((MacState)sender);
                  var transition =tLoadMoveDrawerTraysToOutComplete_NULL;
                  var triggerMember = new TriggerMember
                  {
                      Action = null,
                      ActionParameter = null,
                      ExceptionHandler = (state, ex) =>
                      {
                          // do domething
                      },
                        Guard = () => { return true; },
                      NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                      NotGuardException = null,
                      ThisStateExitEventArgs = new MacStateExitEventArgs()
                  };
                  transition.SetTriggerMembers(triggerMember);
                  Trigger(transition);
              };
            sLoadMoveDrawerTraysToOutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("LoadMoveDrawerTraysToOutComplete.OnExit ");
            };

            sBootupInitialDrawersStart.OnEntry+=(sender,e)=>
            {
                Debug.WriteLine("BootupInitialDrawersStart.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tBootupInitialDrawersStart_BootupInitialDrawersIng;
                var args=(CabinetSystemUpInitialMacStateEntryEventArgs)e;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        var initialDrawerStates = (List<MacMsCabinetDrawer>)parameter;
                        foreach (var state in initialDrawerStates)
                        {
                           
                            state.SystemBootupInitial();
                        }
                    },
                    ActionParameter = args.InitialDrawerStates,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do domething
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs =e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                        
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBootupInitialDrawersStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersStart.OnExit");
            };

            sBootupInitialDrawersIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tBootupInitialDrawersIng_BootupInitialDrawersComplete;
                var args = (CabinetSystemUpInitialMacStateEntryEventArgs)e;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                    Guard = (startTime) =>
                    {
                        var rtnV = false;

                        var completeDrawers = args.InitialDrawerStates.Where(m => m.CurrentState == m.StateWaitingLoadInstruction).ToList().Count();
                        var exceptionDrawers = args.InitialDrawerStates.Where(m => m.CurrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.InitialDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);

            };
            sBootupInitialDrawersIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersIng.OnExit");
            };

            sBootupInitialDrawersComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersComplete.OnEntry");
                var transition = tBootupInitialDrawersComplete_NULL;
                SetCurrentState((MacState)sender);
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs(),
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBootupInitialDrawersComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("BootupInitialDrawersComplete.OnExit");
            };

            sSynchronousDrawerStatesStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesStart.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesStart_SynchronousDrawerStatesIng;
                var args=(CabinetSynchronousDrawerStatesMacStateEntryEventArgs)e;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        foreach(var state in args.SynchronousDrawerStates)
                        {
                            state.SystemBootup();
                        }
                    },
                    ActionParameter = args.SynchronousDrawerStates,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSynchronousDrawerStatesStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesStart.OnExit");
            };

            sSynchronousDrawerStatesIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesIng_SynchronousDrawerStatesComplete;
                //  var args = (CabinetSystemUpInitialMacStateEntryEventArgs)e;
                //  var args = (CabinetSystemUpInitialMacStateEntryEventArgs)e;
                var args = (CabinetSynchronousDrawerStatesMacStateEntryEventArgs)e;
                var triggerMemberAsync = new TriggerMemberAsync
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {

                    },
                    Guard = (startTime) => 
                    {
                        var rtnV = false;
                        var completeDrawers = args.SynchronousDrawerStates.Where(m => m.CurrentState == m.StateWaitingLoadInstruction).ToList().Count();
                        var exceptionDrawers = args.SynchronousDrawerStates.Where(m => m.CurrentState.IsStateMachineException.HasValue).ToList().Count();
                        if (completeDrawers + exceptionDrawers == args.SynchronousDrawerStates.Count())
                        {
                            rtnV = true;
                        }
                        return rtnV;
                    },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                   ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMemberAsync);
                TriggerAsync(transition);
            };
            sSynchronousDrawerStatesIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesIng.OnExit");
            };

            sSynchronousDrawerStatesComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tSynchronousDrawerStatesComplete_NULL;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) => {
                        //do something
                    },
                      Guard = () => { return true; },
                    NextStateEntryEventArgs = new MacStateEntryEventArgs(),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()

                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sSynchronousDrawerStatesComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("SynchronousDrawerStatesComplete.OnExit");
            };

            #endregion event
        }

    }

    public class MoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs: MacStateEntryEventArgs
    {
        public MoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs(int requestDrawers)
        {
            RequestDrawers = requestDrawers;
          
        }
        public int RequestDrawers { get; set; }
        
        
    }
    public class MoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public MoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs(List<IMacHalDrawer> drawers)
        {
            CommandDrawers = drawers;

        }
        public List<IMacHalDrawer> CommandDrawers { get; set; }


    }
    public class MoveTraysToOutForPutBoxOnTrayCompleteMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public MoveTraysToOutForPutBoxOnTrayCompleteMacStateEntryEventArgs(List<IMacHalDrawer> drawers)
        {
            CommandDrawers = drawers;

        }
        public List<IMacHalDrawer> CommandDrawers { get; set; }


    }

    public class CabinetLoadStartMacStateEntryEventArgs: MacStateEntryEventArgs
    {
        public CabinetLoadStartMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            LoadDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> LoadDrawerStates { get; private set; }

    }

    public class CabinetSystemUpInitialMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public CabinetSystemUpInitialMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            InitialDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> InitialDrawerStates { get; private set; }
    }

    public class CabinetSynchronousDrawerStatesMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public CabinetSynchronousDrawerStatesMacStateEntryEventArgs(List<MacMsCabinetDrawer> drawerStates)
        {
            SynchronousDrawerStates = drawerStates;
        }
        public List<MacMsCabinetDrawer> SynchronousDrawerStates { get; private set; }
    }

  
 
    
}
