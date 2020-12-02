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


        /// <summary>存放Bankout 目前有盒子, Tray 在 Home  的 Drawer 的Queue </summary>
        private QueueBankOutAtHomeWaitingToGrabBoxDrawers QueueBankOutWaitingToClampBoxDrawers = null;


        public void BankOutLoadEnqueue(BoxrobotTransferLocation drawerLocation )
        {
            QueueBankOutWaitingToClampBoxDrawers.Enqueue(drawerLocation);
        }

        public BoxrobotTransferLocation BankOutUnLoadDeQueue()
        {
            var location=QueueBankOutWaitingToClampBoxDrawers.Dequeue();
            return location;
        }
        /// <summary>目前首先要處理 Bank Out Drawer 的 Location</summary>
        public bool GetFirstBankOutWaitingToGrabDrawerLacation(out BoxrobotTransferLocation drawerLocation)
        {

            var rtnV = false;
            drawerLocation = BoxrobotTransferLocation.Dontcare;
            if (QueueBankOutWaitingToClampBoxDrawers != null )
             {
                drawerLocation = QueueBankOutWaitingToClampBoxDrawers.Peek();
                if (drawerLocation != default(BoxrobotTransferLocation))
                {
                    rtnV = true;
                }
             }
            return rtnV; 
            
        }

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

        /// <summary>系統啟動</summary>
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
            var transition = this.Transitions[EnumMacCabinetTransition.Idle_BankOutLoadMoveTraysToOutForPutBoxOnTrayStart.ToString()];
            var state = transition.StateFrom;
            state.ExecuteCommandAtExit(transition, null, new BankOutLoadMoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs(drawerCounts));
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

        /// <summary>Bank Out load 時, 將指定的 Drawer Tray(單一個) 送到 In </summary>
        public void BankOutLoadMoveSpecificTrayToInForBoxRobotGrabBox()
        {

            BoxrobotTransferLocation drawerLocation = default(BoxrobotTransferLocation);
            
            // 有要等待處理的 Drawer
            var hasDrawerBankOutMoveIn = this.GetFirstBankOutWaitingToGrabDrawerLacation(out drawerLocation);
            if(hasDrawerBankOutMoveIn)
            {
                //BankOut_Load_TrayAtHomeWithBox
                var keyValue = GetDicMacHalDrawers().GetKeyValue(drawerLocation);
                if(!keyValue.Equals(default(KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>)) && keyValue.Value.DrawerAbled && keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtHomeWithBox)
                {// 狀態要對
                    var transition = this.Transitions[EnumMacCabinetTransition.Idle_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart.ToString()];
                    var state = transition.StateFrom;
                    var drawerInfo = keyValue.Value;
                    state.ExecuteCommandAtExit(transition, null, new BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStartEventArgs(drawerInfo));
                }
            }
            else
            {
                // 没有可處理的 Drawer
            }
        }
        
        
        /// <summary>bank out, Load 時, 指定的 Drawer Tray 在In 時被取走Box 之後令其回到 Home</summary>
        public void BankOutLoadMoveSpecificTrayToHomeAfterBoxrobotGrabBox()
        {
            BoxrobotTransferLocation drawerLocation = default(BoxrobotTransferLocation);
            var hasDrawerBankOutMoveHome = this.GetFirstBankOutWaitingToGrabDrawerLacation(out drawerLocation);
            if (hasDrawerBankOutMoveHome)
            {
                var keyValue = GetDicMacHalDrawers().GetKeyValue(drawerLocation);
                if (!keyValue.Equals(default(KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>)) && keyValue.Value.DrawerAbled && keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtInWithBoxForRobotGrabBox)
                {// 狀態要對
                    var transition = this.Transitions[EnumMacCabinetTransition.Idle_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart.ToString()];
                    var state = transition.StateFrom;
                    var drawerInfo = keyValue.Value;
                    state.ExecuteCommandAtExit(transition, null, new BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStartEventArgs(drawerInfo));
                }
                else
                { // 没有可處理的 Drawer

                }
            }
        }

        /// <summary>bank out, UnLoad 時, 指定的 Drawer Tray 在Home 時 移到Home 準備 到 BankOut Unload, 將盒子放在Tray 上</summary>
        public void BankOutUnLoadMoveSpecificTrayToInForBoxrobotPutBox()
        {
            BoxrobotTransferLocation drawerLocation = default(BoxrobotTransferLocation);
            var hasDrawerBankOutMoveIn = this.GetFirstBankOutWaitingToGrabDrawerLacation(out drawerLocation);
            if (hasDrawerBankOutMoveIn)
            {
                var keyValue = GetDicMacHalDrawers().GetKeyValue(drawerLocation);
                if (!keyValue.Equals(default(KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>)) && keyValue.Value.DrawerAbled && keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtHomeNoBox)
                {// 狀態要對
                    var transition = this.Transitions[EnumMacCabinetTransition.Idle_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart.ToString()];
                    var state = transition.StateFrom;
                    var drawerInfo = keyValue.Value;
                    state.ExecuteCommandAtExit(transition, null, new BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStartEventArgs(drawerInfo));
                }
                else
                { // 没有可處理的 Drawer

                }
            }
        }
        /// <summary>BankOut, UnLoad, 特定的Tray  在放置 盒子之後自In移回Home</summary>
        public void BankOutUnLoadMoveSpecificTrayToHomeAfterBoxrobotPutBox()
        {
            BoxrobotTransferLocation drawerLocation = default(BoxrobotTransferLocation);
            var hasDrawerBankOutMoveHome = this.GetFirstBankOutWaitingToGrabDrawerLacation(out drawerLocation);
            if (hasDrawerBankOutMoveHome)
            {
                var keyValue = GetDicMacHalDrawers().GetKeyValue(drawerLocation);
                if (!keyValue.Equals(default(KeyValuePair<BoxrobotTransferLocation, DrawerBoxInfo>)) && keyValue.Value.DrawerAbled && keyValue.Value.Duration == DrawerDuration.BankOut_UnLoad_TrayAtInNoBox)
                {// 狀態要對
                    var transition = this.Transitions[EnumMacCabinetTransition.Idle_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart.ToString()];
                    var state = transition.StateFrom;
                    var drawerInfo = keyValue.Value;
                    state.ExecuteCommandAtExit(transition, null, new BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStartEventArgs(drawerInfo));
                }
                else
                { // 没有可處理的 Drawer

                }
            }
        }

        
        public void BankOutUnLoadMoveSpecificTraysToOutForGrabBox()
        {
            var specificDrawers = GetDicMacHalDrawers().Where(m => m.Value.Duration == DrawerDuration.BankOut_UnLoad_TrayAtHomeWithBox).ToList();
            if (specificDrawers.Count > 0)
            {
                var transition = this.Transitions[EnumMacCabinetTransition.Idle_BankOutUnLoadMoveSpecificTraysToOutForGrabStart.ToString()];
                var drawers = specificDrawers.Select(m => m.Value).ToList();
                var state = transition.StateFrom;
                state.ExecuteCommandAtExit(transition, null, new  BankOutUnLoadMoveSpecificTraysToOutForGrabStartEventArgs( drawers));
            }
            else
            {

            }
        }

        //----------------------


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
                {   // Cabinet Initial 階段
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
                    else if(keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtInWithBoxForRobotGrabBox)
                    {
                        keyValue.Value.SetDuration(DrawerDuration.BankOut_Load_TrayAtHomeNoBox) ;
                    }
                    else if(keyValue.Value.Duration== DrawerDuration.BankOut_UnLoad_TrayAtInNoBox)
                    {
                        keyValue.Value.SetDuration(DrawerDuration.BankOut_UnLoad_TrayAtHomeWithBox);

                    }
                }
            };
            EventHandler drawerTrayArriveInHandler = (sender, e) =>
            {
                var drawer = (IMacHalDrawer)sender;
                var keyValue = this.dicDrawerAndBoxInfos.GetKeyValue(drawer);
                if (keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtHomeWithBox)
                {   // 
                    keyValue.Value.SetDuration(DrawerDuration.BankOut_Load_TrayAtInWithBoxForRobotGrabBox);
                }
                else if (keyValue.Value.Duration == DrawerDuration.BankOut_Load_TrayAtHomeNoBox)
                {   // 
                    keyValue.Value.SetDuration(DrawerDuration.BankOut_UnLoad_TrayAtInNoBox);
                }
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
                if(keyValue.Value.Duration==DrawerDuration.Idle_TrayAtHome/** && CabinetDuration.IsBankOutDuration()*/)
                {   // 原先在 Idle_TrayAtHome 的Drawer  && Cabinet 正在操作 整批將 Drawer Tray 移到 Out 
                    keyValue.Value.SetDuration( DrawerDuration.BankOut_Load_TrayAtOutForPutBoxOnTray);
                }
                else if (keyValue.Value.Duration == DrawerDuration.BankOut_UnLoad_TrayAtHomeWithBox /**&& CabinetDuration.IsBankOutDuration()*/)
                {
                    keyValue.Value.SetDuration(DrawerDuration.BankOut_UnLoad_TrayAtOutWithBox);
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
                        QueueBankOutWaitingToClampBoxDrawers = new QueueBankOutAtHomeWaitingToGrabBoxDrawers();
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

            MacState sBankOutLoadMoveTraysToOutForPutBoxOnTrayStart = NewState(EnumMacCabinetState.BankOutLoadMoveTraysToOutForPutBoxOnTraysStart);// 將Drawer 移出至Out For Bank
            MacState sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng = NewState(EnumMacCabinetState.BankOutLoadMoveTraysToOutForPutBoxOnTraysIng);// 將Drawer 移出至Out For Bank
            MacState sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete = NewState(EnumMacCabinetState.BankOutLoadMoveTraysToOutForPutBoxOnTraysComplete);// 將Drawer 移出至Out For Bank

            MacState sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart = NewState(EnumMacCabinetState.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart);
            MacState sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng = NewState(EnumMacCabinetState.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng);
            MacState sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete = NewState(EnumMacCabinetState.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete);

            MacState sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart = NewState(EnumMacCabinetState.BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart);
            MacState sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng = NewState(EnumMacCabinetState.BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng);
            MacState sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete = NewState(EnumMacCabinetState.BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete);

            MacState sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart);
            MacState sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng);
            MacState sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete);

            MacState sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart);
            MacState sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng);
            MacState sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete);

            MacState sBankOutUnLoadMoveSpecificTraysToOutForGrabStart = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTraysToOutForGrabStart); ;
            MacState sBankOutUnLoadMoveSpecificTraysToOutForGrabIng = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTraysToOutForGrabIng); ;
            MacState sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete = NewState(EnumMacCabinetState.BankOutUnLoadMoveSpecificTraysToOutForGrabComplete); ;

            //MacState sMoveTray
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

            MacTransition tIdle_MoveTraysToOutForPutBoxOnTrayStart = NewTransition(sIdle, sBankOutLoadMoveTraysToOutForPutBoxOnTrayStart, EnumMacCabinetTransition.Idle_BankOutLoadMoveTraysToOutForPutBoxOnTrayStart);
            MacTransition tBankOutLoadMoveTraysToOutForPutBoxOnTrayStart_MoveTraysToOutForPutBoxOnTrayIng = NewTransition(sBankOutLoadMoveTraysToOutForPutBoxOnTrayStart, sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng, EnumMacCabinetTransition.BankOutLoadMoveTraysToOutForPutBoxOnTrayStart_BankOutLoadMoveTraysToOutForPutBoxOnTrayIng);
            MacTransition tBankOutLoadMoveTraysToOutForPutBoxOnTrayIng_MoveTraysToOutForPutBoxOnTrayComplete = NewTransition(sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng, sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete, EnumMacCabinetTransition.BankOutLoadMoveTraysToOutForPutBoxOnTrayIng_BankOutLoadMoveTraysToOutForPutBoxOnTrayComplete);
            MacTransition tBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete_Idle= NewTransition( sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete, sIdle,EnumMacCabinetTransition.MoveTraysToOutForPutBoxOnTrayComplete_Idle);

            MacTransition tIdle_sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart=NewTransition(sIdle, sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart, EnumMacCabinetTransition.Idle_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart);
            MacTransition tBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng=NewTransition(sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart, sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng, EnumMacCabinetTransition.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng);
            MacTransition tBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete= NewTransition(sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng, sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete, EnumMacCabinetTransition.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabComlete);
            MacTransition tBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete_Idle= NewTransition( sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete, sIdle,EnumMacCabinetTransition.BankOutLoadMoveSpecificTrayToInForBoxRobotGrabComlete_Idle);


            MacTransition tIdle_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart = NewTransition(sIdle, sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart, EnumMacCabinetTransition.Idle_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart);
            MacTransition tBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng = NewTransition(sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart, sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng, EnumMacCabinetTransition.BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng);
            MacTransition tBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete = NewTransition(sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng, sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete, EnumMacCabinetTransition.BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete);
            MacTransition tBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete_Idle = NewTransition(sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete,sIdle, EnumMacCabinetTransition.BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete_Idle);

            MacTransition tIdle_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart = NewTransition(sIdle, sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart, EnumMacCabinetTransition.Idle_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart);
            MacTransition tBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng = NewTransition(sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart, sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng);
            MacTransition tBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete = NewTransition(sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng, sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete);
            MacTransition tBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete_Idle = NewTransition( sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete,sIdle, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete_Idle);

            MacTransition tIdle_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart = NewTransition(sIdle, sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart, EnumMacCabinetTransition.Idle_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart);
            MacTransition tBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng = NewTransition(sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart, sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng);
            MacTransition tBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete = NewTransition(sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng, sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete);
            MacTransition tBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete_Idle = NewTransition(sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete, sIdle, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete_Idle);

            MacTransition tIdle_BankOutUnLoadMoveSpecificTraysToOutForGrabStart = NewTransition(sIdle, sBankOutUnLoadMoveSpecificTraysToOutForGrabStart, EnumMacCabinetTransition.Idle_BankOutUnLoadMoveSpecificTraysToOutForGrabStart);
            MacTransition tBankOutUnLoadMoveSpecificTraysToOutForGrabStart_BankOutUnLoadMoveSpecificTraysToOutForGrabIng = NewTransition(sBankOutUnLoadMoveSpecificTraysToOutForGrabStart, sBankOutUnLoadMoveSpecificTraysToOutForGrabIng, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTraysToOutForGrabStart_BankOutUnLoadMoveSpecificTraysToOutForGrabIng);
            MacTransition tBankOutUnLoadMoveSpecificTraysToOutForGrabIng_BankOutUnLoadMoveSpecificTraysToOutForGrabComplete = NewTransition(sBankOutUnLoadMoveSpecificTraysToOutForGrabIng, sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTraysToOutForGrabIng_BankOutUnLoadMoveSpecificTraysToOutForGrabComplete);
            MacTransition tBankOutUnLoadMoveSpecificTraysToOutForGrabComplete_Idle = NewTransition(sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete, sIdle, EnumMacCabinetTransition.BankOutUnLoadMoveSpecificTraysToOutForGrabComplete_Idle);



            //******************************************
            MacTransition tStart_NULL = NewTransition(sStart, null,  EnumMacCabinetTransition.Start_NULL);

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
                            if (this.DrawersInitialStatus == null)
                            {
                                break;
                            }
                            else if (this.DrawersInitialStatus.IsActionComplete())
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
            sBankOutLoadMoveTraysToOutForPutBoxOnTrayStart.OnEntry += (sender,e) =>
            {
                Debug.WriteLine("BankOutLoadMoveTraysToOutForPutBoxOnTrayStart.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfos = GetDicMacHalDrawers();
                
                // 要送出的 Drawer
                var drawerCounts = ((BankOutLoadMoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs)e).RequestDrawers; // drawerCounts 要調整一下, 應該有些cDrawer 的Tray 不能移動
                this.DrawersMoveTrayToOutStatus = new DrawerMoveTrayToOutStatus(drawerCounts);
                var transition = tBankOutLoadMoveTraysToOutForPutBoxOnTrayStart_MoveTraysToOutForPutBoxOnTrayIng;
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
                    NextStateEntryEventArgs = new BankOutLoadMoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs(commandDrawers),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sBankOutLoadMoveTraysToOutForPutBoxOnTrayStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sBankOutLoadMoveTraysToOutForPutBoxOnTrayStart.OnExit]");

            };
            sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng.OnEntry");
                var commandDrawers = ((BankOutLoadMoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs)e).CommandDrawers;
                SetCurrentState((MacState)sender);
                var transition = tBankOutLoadMoveTraysToOutForPutBoxOnTrayIng_MoveTraysToOutForPutBoxOnTrayComplete;
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
                    NextStateEntryEventArgs = new BankOutLoadMoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs(commandDrawers),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sBankOutLoadMoveTraysToOutForPutBoxOnTrayIng.OnExit]");
            };
            sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete_Idle;
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
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("State: [ sBankOutLoadMoveTraysToOutForPutBoxOnTrayComplete.OnExit]");
            };

            sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart.OnEntry += (sender, e) =>
             {
                  Debug.WriteLine("sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart.OnEntry");
                  SetCurrentState((MacState)sender);
                  var transition = tBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng;
                  var drawerBoxInfo = ((BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStartEventArgs)e).DrawerBoxInfo;
                 var triggerMember = new TriggerMember
                 {
                     Action = (parameter) =>
                     {
                         drawerBoxInfo.Drawer.CommandTrayMotionIn();
                     },
                    ActionParameter=null,
                     ExceptionHandler = (state, ex) =>
                     {
                         // do something,
                     },
                     Guard = () => true,
                     NextStateEntryEventArgs = new BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIngEventArgs(drawerBoxInfo),
                     NotGuardException = null,
                     ThisStateExitEventArgs = new MacStateExitEventArgs()
                 };
                 transition.SetTriggerMembers(triggerMember);
                 Trigger(transition);
             };
            sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabStart.OnExit");
               
            };
            sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng.OnEntry");
                SetCurrentState((MacState)sender);
                var transition = tBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng_BankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete;
                var drawerBoxInfo = ((BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIngEventArgs)e).DrawerBoxInfo;
                var triggerMember = new TriggerMember
                {
                    Action=null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () =>
                    {
                        DateTime startTime = DateTime.Now;
                        while (true)
                        {
                            // 判斷是否到In //BankOut_Load_TrayAtHomeWithBox

                            if (drawerBoxInfo.Duration == DrawerDuration.BankOut_Load_TrayAtInWithBoxForRobotGrabBox)
                            {
                                return true; 
                            }
                            // 有没有 逾時
                            if (TimeoutObject.IsTimeOut(startTime))
                            {
                                // TODO: to throw a time out Exception 
                            }
                            Thread.Sleep(100);
                        }
                    },
                    NextStateEntryEventArgs = new BankOutLoadMoveSpecificTrayToInForBoxRobotGrabCompleteEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabIng.OnExit");
            };
            sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutLoadMoveSpecificTrayToInForBoxRobotGrabCompleteEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete_Idle;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () =>true,
                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToInForBoxRobotGrabComplete.OnEntry");
            };

            sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStartEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng;
                var triggerMember = new TriggerMember
                {
                    Action =(parameter)=>drawerBoxInfo.Drawer.CommandTrayMotionHome(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs =new BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIngEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStart.OnExit");
            };

            sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIngEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng_BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete;
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
                        var startTime = DateTime.Now;
                        while (true)
                        {
                            if (drawerBoxInfo.Duration==DrawerDuration.BankOut_Load_TrayAtHomeNoBox)
                            {
                                break;
                            }

                            if (TimeoutObject.IsTimeOut(startTime))
                            {
                                // TODO: To throw an Exception
                            }
                            Thread.Sleep(100);
                        }
                        return true;
                    },
                    NextStateEntryEventArgs = new BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabCompleteEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIng.OnExit");
            };

            sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabCompleteEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete_Idle;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs =e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabComplete.OnExit");
            };

            sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart.OnEntry += (sender,e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStartEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=> drawerBoxInfo.Drawer.CommandTrayMotionIn(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIngEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart.Exit");
            };

            sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIngEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng_BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete;
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
                        var startTime = DateTime.Now;
                        while (true)
                        {
                            if (drawerBoxInfo.Duration == DrawerDuration.BankOut_UnLoad_TrayAtInNoBox)
                            {
                                break;
                            }
                            if (TimeoutObject.IsTimeOut(startTime)) { break; }
                            Thread.Sleep(100);
                        }
                        return true; 
                    },
                    NextStateEntryEventArgs = new BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutCompleteEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIng.Exit");
            };

            sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutCompleteEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete_Idle;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs =e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToInForBoxRobotPutComplete.Exit");
            };

            sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStartEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=>drawerBoxInfo.Drawer.CommandTrayMotionHome(),
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIngEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStart.Exit");
            };

            sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIngEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng_BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete;
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
                        var startTime = DateTime.Now;
                        while (true)
                        {
                            if (drawerBoxInfo.Duration==DrawerDuration.BankOut_UnLoad_TrayAtHomeWithBox)
                            {
                                break;
                            }
                            if (TimeoutObject.IsTimeOut(startTime))
                            {
                                // TODO: to throw a timeout Exception 
                            }

                            Thread.Sleep(100);
                        }
                        return true;
                    },
                    NextStateEntryEventArgs = new BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutCompleteEventArgs(drawerBoxInfo),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIng.Exit");
            };

            sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfo = ((BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutCompleteEventArgs)e).DrawerBoxInfo;
                var transition = tBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete_Idle;
                var triggerMember = new TriggerMember
                {
                    Action = (parameter)=>
                    {
                        var location = BankOutUnLoadDeQueue();
                    },
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () =>true,
                    NextStateEntryEventArgs = e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutComplete.Exit");
            };

            sBankOutUnLoadMoveSpecificTraysToOutForGrabStart.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTraysToOutForGrabStart.OnEntry");
                SetCurrentState((MacState)sender);
                var drawerBoxInfoList = ((BankOutUnLoadMoveSpecificTraysToOutForGrabStartEventArgs)e).DrawerBoxInfoList;
                var transition = tBankOutUnLoadMoveSpecificTraysToOutForGrabStart_BankOutUnLoadMoveSpecificTraysToOutForGrabIng;
                this.DrawersMoveTrayToOutStatus = new DrawerMoveTrayToOutStatus(drawerBoxInfoList.Count);
             
                var triggerMember = new TriggerMember
                {
                    Action = (parameter) =>
                    {
                        DrawersMoveTrayToOutStatus.StartAction();
                        var drawerInfolst= (List<DrawerBoxInfo>)parameter;
                       foreach(var info in drawerInfolst)
                        {
                            info.Drawer.CommandTrayMotionOut();
                        }
                    },
                    ActionParameter = drawerBoxInfoList,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () => true,
                    NextStateEntryEventArgs = new BankOutUnLoadMoveSpecificTraysToOutForGrabIngEventArgs(drawerBoxInfoList),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);

            };
            sBankOutUnLoadMoveSpecificTraysToOutForGrabStart.OnExit += (sender, e) =>
            {
            };
            sBankOutUnLoadMoveSpecificTraysToOutForGrabIng.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTraysToOutForGrabIng.OnEntry");
                var drawerBoxInfoList = ((BankOutUnLoadMoveSpecificTraysToOutForGrabIngEventArgs)e).DrawerBoxInfoList;
                SetCurrentState((MacState)sender);
                var transition = tBankOutUnLoadMoveSpecificTraysToOutForGrabIng_BankOutUnLoadMoveSpecificTraysToOutForGrabComplete; ;
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
                    NextStateEntryEventArgs = new BankOutUnLoadMoveSpecificTraysToOutForGrabCompleteEventArgs(drawerBoxInfoList),
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTraysToOutForGrabIng.OnExit += (sender, e) => 
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTraysToOutForGrabIng.OnExit");
            };

            sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete.OnEntry += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete.OnEntry");
                var drawerBoxInfoList = ((BankOutUnLoadMoveSpecificTraysToOutForGrabCompleteEventArgs)e).DrawerBoxInfoList;
                SetCurrentState((MacState)sender);
                var transition = tBankOutUnLoadMoveSpecificTraysToOutForGrabComplete_Idle ;
                var startTime = DateTime.Now;
                var triggerMember = new TriggerMember
                {
                    Action = null,
                    ActionParameter = null,
                    ExceptionHandler = (state, ex) =>
                    {
                        // do something,
                    },
                    Guard = () =>true,
                    NextStateEntryEventArgs =e,
                    NotGuardException = null,
                    ThisStateExitEventArgs = new MacStateExitEventArgs()
                };
                transition.SetTriggerMembers(triggerMember);
                Trigger(transition);
            };
            sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete.OnExit += (sender, e) =>
            {
                Debug.WriteLine("sBankOutUnLoadMoveSpecificTraysToOutForGrabComplete.OnExit");
            };


            //----------------------
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



    public class BankOutUnLoadMoveSpecificTraysToOutForGrabCompleteEventArgs : MacStateEntryEventArgs
    {
        public List<DrawerBoxInfo> DrawerBoxInfoList { get; set; }
        public BankOutUnLoadMoveSpecificTraysToOutForGrabCompleteEventArgs(List<DrawerBoxInfo> drawerBoxInfoList)
        {
            DrawerBoxInfoList = drawerBoxInfoList;

        }
    }

    public class BankOutUnLoadMoveSpecificTraysToOutForGrabIngEventArgs : MacStateEntryEventArgs
    {
        public List<DrawerBoxInfo> DrawerBoxInfoList { get; set; }
        public BankOutUnLoadMoveSpecificTraysToOutForGrabIngEventArgs(List<DrawerBoxInfo> drawerBoxInfoList)
        {
            DrawerBoxInfoList = drawerBoxInfoList;

        }
    }

    public class BankOutUnLoadMoveSpecificTraysToOutForGrabStartEventArgs : MacStateEntryEventArgs
    {
        public List<DrawerBoxInfo> DrawerBoxInfoList { get; set; }
        public BankOutUnLoadMoveSpecificTraysToOutForGrabStartEventArgs(List<DrawerBoxInfo> drawerBoxInfoList)
        {
            DrawerBoxInfoList = drawerBoxInfoList;

        }
    }




    public class BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutCompleteEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutCompleteEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }

    public class BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIngEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutIngEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }

    public class BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStartEventArgs: MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutUnLoadMoveSpecificTrayToHomeAfterBoxRobotPutStartEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }



    public class BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutCompleteEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutCompleteEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }

    public class BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIngEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutIngEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }


    public class BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStartEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutUnLoadMoveSpecificTrayToInForBoxRobotPutStartEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }


    public class BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabCompleteEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabCompleteEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }

    public class BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIngEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabIngEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }


    public class BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStartEventArgs: MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutLoadMoveSpecificTrayToHomeAfterBoxRobotGrabStartEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }



    public class BankOutLoadMoveSpecificTrayToInForBoxRobotGrabCompleteEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutLoadMoveSpecificTrayToInForBoxRobotGrabCompleteEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }

    public class BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIngEventArgs : MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutLoadMoveSpecificTrayToInForBoxRobotGrabIngEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }
    public class BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStartEventArgs: MacStateEntryEventArgs
    {
        public DrawerBoxInfo DrawerBoxInfo { get; set; }
        public BankOutLoadMoveSpecificTrayToInForBoxRobotGrabStartEventArgs(DrawerBoxInfo info)
        {
            DrawerBoxInfo = info;

        }
    }

    public class BankOutLoadMoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs: MacStateEntryEventArgs
    {
        public BankOutLoadMoveTraysToOutForPutBoxOnTrayStartMacStateEntryEventArgs(int requestDrawers)
        {
            RequestDrawers = requestDrawers;
          
        }
        public int RequestDrawers { get; set; }
        
        
    }
    public class BankOutLoadMoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public BankOutLoadMoveTraysToOutForPutBoxOnTrayIngMacStateEntryEventArgs(List<IMacHalDrawer> drawers)
        {
            CommandDrawers = drawers;

        }
        public List<IMacHalDrawer> CommandDrawers { get; set; }


    }
    public class BankOutLoadMoveTraysToOutForPutBoxOnTrayCompleteMacStateEntryEventArgs : MacStateEntryEventArgs
    {
        public BankOutLoadMoveTraysToOutForPutBoxOnTrayCompleteMacStateEntryEventArgs(List<IMacHalDrawer> drawers)
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
