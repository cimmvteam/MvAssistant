using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompPlc
{
    public interface IMacHalPlcInspectionCh
    {
        string XYPosition(double X_Position, double Y_Position);

        string ZPosition(double Z_Position);

        string WPosition(double W_Position);
    }
}
