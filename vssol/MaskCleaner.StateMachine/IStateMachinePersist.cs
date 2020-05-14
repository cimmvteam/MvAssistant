using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MaskCleaner.StateMachine
{
    public interface IStateMachinePersist
    {
        void SaveStateMachine(string path);
        XmlDocument Load(string path);
    }
}
