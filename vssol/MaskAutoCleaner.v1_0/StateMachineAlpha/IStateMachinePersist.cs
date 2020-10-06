using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MaskAutoCleaner.v1_0.StateMachineAlpha
{
    public interface IStateMachinePersist
    {
        void SaveStateMachine(string path);
        XmlDocument Load(string path);
    }
}
