using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.TestCase
{
    public class TestCaseCategoryAttribute : Attribute
    {
        private TestCaseCategory category;

        public TestCaseCategory Category
        {
            get { return category; }
            set { category = value; }
        }

        public TestCaseCategoryAttribute(TestCaseCategory category)
        {
            Category = category;
        }
    }
}
