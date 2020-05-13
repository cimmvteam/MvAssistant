using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.TestCase
{
    public class TestCaseDescriptionAttribute: Attribute
    {
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public TestCaseDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}
