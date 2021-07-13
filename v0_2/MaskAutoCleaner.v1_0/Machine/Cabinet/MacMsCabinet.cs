using MaskAutoCleaner.v1_0.Machine.Cabinet.Drawers;
using MaskAutoCleaner.v1_0.StateMachineBeta;
using MvAssistant.v0_2.Mac.Hal.CompDrawer;
using MvAssistant.v0_2.Mac.JSon.RobotTransferFile;
using MvAssistant.v0_2.Mac.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet
{

    [Guid("79121DD7-574E-4B25-99D2-E8DD2ED66C2B")] // 不標註也行
    public class MacMsCabinet : MacMachineStateBase
    {
        #region Members

        private static MacMsCabinet _instance = null;
        private static object getInstanceLockObj = new object();

        private Dictionary<BoxrobotTransferLocation, DrawerDetail> _dicDrawerDetails = null;
        private static object getDrawerDetailLockObj = new object();

        private DrawerEventHandler _drawerEventHandler = null;

        /// <summary></summary>
        /// <returns></returns>
        public static MacMsCabinet GetInstance()
        {
            if (_instance == null)
            {
                lock (getInstanceLockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new MacMsCabinet();
                    }
                }
            }
            return _instance;
        }

        /// <summary>Constructor</summary>
        private MacMsCabinet()
        {
            if (_instance == null)
            {
                lock (getInstanceLockObj)
                {
                    if (_instance == null)
                    {
                        LoadStateMachine();
                        _instance = this;
                     }

                }
            }
        }

        /// <summary></summary>
        /// <returns></returns>
        public Dictionary<BoxrobotTransferLocation, DrawerDetail> GetDicMacHalDrawers()
        {
            if (_dicDrawerDetails == null)
            {
                lock (getDrawerDetailLockObj)
                {
                    if (_dicDrawerDetails == null)
                    {
                        _dicDrawerDetails = new Dictionary<BoxrobotTransferLocation, DrawerDetail>();
                        var drawerIdRange = EnumMacDeviceId.boxtransfer_assembly.GetDrawerRange();
                        for (var i = drawerIdRange.StartID; i <= drawerIdRange.EndID; i++)
                        {
                            try
                            {
                                var drawer = this.halAssembly.Hals[i.ToString()] as IMacHalDrawer;
                                var drawerBox = new DrawerDetail(i, drawer);
                                var drawerLocation = i.ToBoxrobotTransferLocation();
                                _dicDrawerDetails.Add(drawerLocation, drawerBox);

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        _drawerEventHandler = new DrawerEventHandler(_dicDrawerDetails.Values.ToList());
                    }
                }
            }
            return _dicDrawerDetails;
        }
        #endregion Members














        #region command
        #endregion



        public override void SystemBootup()
        {
            throw new NotImplementedException();
        }



        public override void LoadStateMachine()
        {
            #region state

            MacState sInitialStart = NewState(EnumMacCabinetState.InitialStart);
            MacState sInitialIng = NewState(EnumMacCabinetState.BootupInitialDrawersIng);
            MacState sInitialComplete = NewState(EnumMacCabinetState.InitialComplete);
            
            MacState sIdle = NewState(EnumMacCabinetState.Idle);


            #endregion state

            #region transition

            MacTransition tInitialStart_InitialIng = NewTransition(sInitialStart,sInitialIng,EnumMacCabinetTransition.InitialStart_InitialIng);
            MacTransition tInitialIng_InitialComplete = NewTransition(sInitialIng,sInitialComplete,EnumMacCabinetTransition.InitialIng_InitialComplete);
            MacTransition tInitialComplete_Idle = NewTransition(sInitialComplete,sIdle, EnumMacCabinetTransition.InitialComplete_Idle);



            #endregion transition

            #region events
            sIdle.OnEntry += (sender, e) =>
            {

            };
            sIdle.OnExit += (sender, e) =>
            {

            };

            sInitialStart.OnEntry += (sender, e) =>
            {

            };
            sInitialStart.OnExit += (sender, e) =>
            {

            };

            sInitialIng.OnEntry += (sender, e) =>
            {

            };
            sInitialIng.OnExit += (sender, e) =>
            {

            };



            #endregion events
        }
    }
}
