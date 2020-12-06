using MvAssistant.DeviceDrive.KjMachineDrawer.DrawerEventArgs;
using MvAssistant.Mac.v1_0.Hal.CompDrawer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.Drawers
{
    public class DrawerEventHandler
    {
        private List<DrawerDetail> DrawerDetails { get; set; }
        private DrawerEventHandler()
        {

        }
        public DrawerEventHandler(List<DrawerDetail> drawerDetails) : this()
        {
            DrawerDetails = drawerDetails;
            BindEvents();
        }


        /// <summary>綁定 事件</summary>
        public void BindEvents()
        {
            foreach (var drawerDetail in DrawerDetails)
            {
                var drawer = drawerDetail.HalDrawer;
                drawer.OnBrightLEDFailedHandler += OnBrightLEDFailed;
                drawer.OnBrightLEDOKHandler += OnBrightLEDOK;
                drawer.OnButtonEventHandler += OnButtonEvent;
                drawer.OnDetectedEmptyBoxHandler += OnDetectedEmptyBox;
                drawer.OnDetectedHasBoxHandler += OnDetectedHasBox;
                drawer.OnERRORErrorHandler += OnERRORError;
                drawer.OnERRORREcoveryHandler += OnERRORREcovery;
                drawer.OnINIFailedHandler += OnINIFailed;
                drawer.OnINIOKHandler += OnINIOK;
                drawer.OnLCDCMsgFailedHandler += OnLCDCMsgFailed;
                drawer.OnLCDCMsgOKHandler += OnLCDCMsgOK;
                drawer.OnPositionStatusHandler += OnPositionStatus;
                drawer.OnSetMotionSpeedFailedHandler += OnSetMotionSpeedFailed;
                drawer.OnSetMotionSpeedOKHandler += OnSetMotionSpeedOK;
                drawer.OnSetTimeOutFailedHandler += OnSetTimeOutFailed;
                drawer.OnSetTimeOutOKHandler += OnSetTimeOutOK;
                drawer.OnSysStartUpHandler += OnSysStartUp;
                drawer.OnTrayArriveHomeHandler += OnTrayArriveHome;
                drawer.OnTrayArriveInHandler += OnTrayArriveIn;
                drawer.OnTrayArriveOutHandler += OnTrayArriveOut;
                
                drawer.OnTrayMotionFailedHandler += OnTrayMotionFailed;
                drawer.OnTrayMotioningHandler += OnTrayMotioning;
                drawer.OnTrayMotionOKHandler += OnTrayMotionOK;
                drawer.OnTrayMotionSensorOFFHandler += OnTrayMotionSensorOFF;
            }

        }


        private void  OnBrightLEDFailed(object sender,EventArgs e)
        {

        }

        private void OnBrightLEDOK(object sender,EventArgs e)
        {

        }
        private void OnButtonEvent(object sender,EventArgs e)
        {

        }
        private void OnDetectedEmptyBox(object sender,EventArgs e)
        {

        }
        private void OnDetectedHasBox(object sender, EventArgs e)
        {

        }
        private void OnERRORError(object sender, EventArgs e)
        {

        }
        private void OnERRORREcovery(object sender, EventArgs e)
        {

        }
        private void OnINIFailed(object sender,EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
            drawer.IsInitialing = false;
        }
        private void OnINIOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
            drawer.IsInitialing = false;
        }
        private void OnLCDCMsgFailed(object sender, EventArgs e)
        {

        }
        private void OnLCDCMsgOK(object sender, EventArgs e)
        {

        }
        private void OnPositionStatus(object sender, EventArgs e)
        {
            /**
            ((OnReplyPositionEventArgs)e).IHOStatus=="001"  => Tray 在Out
            ((OnReplyPositionEventArgs)e).IHOStatus=="100" => Tray 在 In
            ((OnReplyPositionEventArgs)e).IHOStatus=="111"  => Tray 在Home 
            */
        }

        private void OnSetMotionSpeedFailed(object sender, EventArgs e)
        {

        }
        private void OnSetMotionSpeedOK(object sender,EventArgs e)
        {
        }
        private void OnSetTimeOutFailed(object sender,EventArgs e)
        {

        }
        private void OnSetTimeOutOK(object sender, EventArgs e)
        {
            var drawer = (IMacHalDrawer)sender;
        }
        private void OnSysStartUp(object sender,EventArgs e)
        {
            
        }
        private void OnTrayArriveHome(object sender,EventArgs e)
        {     // 如果 是在Initial 階段, 收到 這個事件後, 直接 Invoke  OnINIOK
            var drawer = (IMacHalDrawer)sender;
            if (drawer.IsInitialing)
            {
                OnINIOK(sender,e);
            }
            else
            {

            }

        }
        private void OnTrayArriveIn(object sender,EventArgs e)
        {

        }
        private void OnTrayArriveOut(object sender, EventArgs e)
        {

        }
        private void OnTrayMotionFailed(object sender,EventArgs e)
        {

        }

        private void OnTrayMotioning(object sender,EventArgs e)
        {

        }

        private void OnTrayMotionOK(object sender,EventArgs e)
        {

        }

        private void OnTrayMotionSensorOFF(object sender,EventArgs e)
        {

        }
    }
}
