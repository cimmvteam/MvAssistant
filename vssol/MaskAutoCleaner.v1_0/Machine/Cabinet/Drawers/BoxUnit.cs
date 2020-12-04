using MvAssistant.Mac.v1_0;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.Drawers
{
   public class BoxUnit
    {
        public string Barcode { get; private set; }
        public BoxType BoxType { set; get; }
        public MaskUnit MaskUnit { get; private set; }
        public bool HasMask { get; private set; }

        public BoxUnit()
        {
            Barcode = string.Empty;
            BoxType = BoxType.DontCare;
            MaskUnit = null;
            HasMask = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="boxType"></param>
        public BoxUnit(string barcode, BoxType boxType):this()
        {
            Barcode = barcode;
            BoxType = BoxType;

        }

        public void SetBarcode(string barcode)
        {
            Barcode = barcode;
        }
        public void ClearBarcode()
        {
            Barcode = string.Empty;
        }

        public void PutMaskInBox()
        {
            HasMask = true;
        }

        public void TakeMaskFromBox()
        {
            HasMask = false;
        }

        public void BindMask(MaskUnit maskUnit)
        {
            MaskUnit = maskUnit;
        }

        public void UnBindMask()
        {
            MaskUnit = null;
        }
    }
}
