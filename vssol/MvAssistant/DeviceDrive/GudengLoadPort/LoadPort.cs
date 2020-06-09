using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.GudengLoadPort
{
   public class LoadPort
    {
        public int LoadPortNo { get; set; }

        #region Command
        public void CommandInitialRequest()
        { }

        public void CommandDockRequest()
        { }

        public void CommandUndockRequest()
        { }

        public void CommandAskPlacementStatus()
        { }

        public void CommandAskPresentStatus()
        { }

        public void CommandAskClamperStatus()
        { }

        public void CommandAskRFIDStatus()
        { }

        public void CommandAskBarcodeStatus()
        { }

        public void CommandAskVacuumStatus()
        { }

        public void CommandAskReticleExistStatus()
        { }

        public void AlarmReset()
        { }

        public  void CommandAskStagePosition()
        {}

        public void CommandAskLoadportStatus()
        { }

        public void CommandManualClamperLock()
        {}

        public void CommandManualClamperUnlock()
        { }

        public void CommandManualClamperOPR()
        { }

        public void CommandManualStageUp()
        { }

        public void CommandManualStageInspection()
        { }

        public void CommandManualStageDown()
        {}

        public void CommandManualStageOPR()
        { }

        public void CommandManualVacuumOn()
        { }

        public void CommandManualVacuumOff()
        { }
        #endregion 

        #region Event & Alarm

        #endregion

    }


    public class TcpSocketServer
    {
        public string ServerIP { get; set; }
        public int ServerPort { get; set; }
    }
    public class TcpSocketClient
    {
        public string ClientIP { get; set; }
        public int ClientPort { get; set; }
    }

    
}
