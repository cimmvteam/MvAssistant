using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MaskAutoCleaner.StateMachine_v1_0
{
    public interface IStateMachinePersist
    {
        void SaveStateMachine(string path);
        XmlDocument Load(string path);
    }
}
