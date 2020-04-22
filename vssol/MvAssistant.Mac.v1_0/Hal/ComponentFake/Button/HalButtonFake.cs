using MvAssistant.Mac.v1_0.Hal.Component.Button;
using MvAssistant.Manifest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.ComponentFake.Button
{
    [GuidAttribute("749DC2E0-38B6-4624-80DF-1055CE145EB0")]
    public class HalButtonFake : HalFakeBase, IHalButton
    {
        enum ButtonRequest
        {
            Open,
            Close,
        }
        private CancellationTokenSource cts = null;
        private ButtonRequest buttonRequest = ButtonRequest.Close;
        private bool isProcessComplete = true;
        Object lockForProcess = new object();
        public bool IsPressedOpen()
        {
            return buttonRequest == ButtonRequest.Open ? true : false;
        }
        public bool IsPressedClose()
        {
            return buttonRequest == ButtonRequest.Close ? true : false;
        }
        public bool IsProcessComplete()
        {

            return isProcessComplete;
        }

        public void ProcessComplete()
        {
            lock (lockForProcess) this.isProcessComplete = true;
        }

        public void fakePressButton()
        {
            
            if (buttonRequest == ButtonRequest.Close)
            {                
                buttonRequest = ButtonRequest.Open;
            }
            else if (buttonRequest == ButtonRequest.Open)
            {
                buttonRequest = ButtonRequest.Close;
            }
            lock (lockForProcess) isProcessComplete = false;
        }
    }
}
