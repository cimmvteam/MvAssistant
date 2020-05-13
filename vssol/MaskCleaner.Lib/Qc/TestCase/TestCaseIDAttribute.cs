using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.TestCase
{
    public class TestCaseIDAttribute : Attribute
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public TestCaseIDAttribute(string id)
        {
            ID = id;
        }
    }
}
