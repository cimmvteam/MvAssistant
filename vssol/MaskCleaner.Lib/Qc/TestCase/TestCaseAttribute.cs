using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.TestCase
{
    public class TestCaseAttribute : Attribute
    {
        private TestCaseCategory category = TestCaseCategory.NonSpecified;

        [Description("Test Case描述")]
        public string Description { get; set; }

        [Description("測試種類")]
        public TestCaseCategory Category 
        {
            get { return category; }
            set { category = value; }
        }
    }
}
