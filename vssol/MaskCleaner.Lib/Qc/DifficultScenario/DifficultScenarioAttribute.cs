using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.Qc.DifficultScenario
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class DifficultScenarioAttribute : Attribute
    {
        // TODO: insert description properties for differnt reasons
        private DifficultScenarioID difficultScenarioID;
        private string description;

        /// <summary>
        /// 困難情境ID
        /// </summary>
        public DifficultScenarioID DifficultScenarioID
        {
            get { return difficultScenarioID; }
            set { difficultScenarioID = value; }
        }

        /// <summary>
        /// 困難情境描述
        /// </summary>
        public string Description
        {
            get { return description; }
            set
            {
                FieldInfo fi = difficultScenarioID.GetType().GetField(difficultScenarioID.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    if (String.IsNullOrEmpty(value))
                        description = String.Format("{0}", attributes[0].Description);
                    else
                        description = String.Format("{0} ({1})", attributes[0].Description, value);
                }
                else
                    description = value;
            }
        }

        public DifficultScenarioAttribute(DifficultScenarioID difficultScenarioID)
        {
            DifficultScenarioID = difficultScenarioID;
            Description = "";
        }

        public DifficultScenarioAttribute(DifficultScenarioID difficultScenarioID, string description = "") 
            : this(difficultScenarioID)
        {
            DifficultScenarioID = difficultScenarioID;
            Description = description;
        }
    }
}
