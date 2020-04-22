using MaskAutoCleaner.Hal.Intf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace MaskAutoCleaner.Hal
{
    /// <summary>
    /// abstract fake HAL (Hardware Abstract Layer) class
    /// </summary>
    [GuidAttribute("A0D5CD14-8A44-46EA-AF8C-DF2724A7517B")]
    public abstract class HalFakeBase : IHalFake
    {

        
        #region IHal

        public string DeviceConnStr { get; set; }

        public string ID { get; set; }

        public int HalClose() { return 1; }

        public int HalConnect() { return 1; }

        public bool HalIsConnected() { return true; }

        #endregion



        /// <summary>
        /// set value to property
        /// </summary>
        /// <param name="property">property of component data, such as E84Value</param>
        /// <param name="propertyValue">value be specified</param>
        private void SetComponentPropertyValue(string property, string propertyValue)
        {
            var pros = this.GetType().GetProperties();
            foreach (var pro in pros)
            {
                if (pro.Name.Equals(property))
                {
                    if (pro.PropertyType.Equals(typeof(Int32)))
                        pro.SetValue(this, Convert.ToInt32(propertyValue), null);
                    else if (pro.PropertyType.Equals(typeof(Single)))
                        pro.SetValue(this, Convert.ToSingle(propertyValue), null);
                    else if (pro.PropertyType.Equals(typeof(Double)))
                        pro.SetValue(this, Convert.ToDouble(propertyValue), null);
                    else if (pro.PropertyType.Equals(typeof(Decimal)))
                        pro.SetValue(this, Convert.ToDecimal(propertyValue), null);
                    else if (pro.PropertyType.Equals(typeof(Boolean)))
                        pro.SetValue(this, Convert.ToBoolean(propertyValue), null);
                    else if (pro.PropertyType.Equals(typeof(String)))
                        pro.SetValue(this, propertyValue, null);
                    break;
                }
            }
        }
    }
}