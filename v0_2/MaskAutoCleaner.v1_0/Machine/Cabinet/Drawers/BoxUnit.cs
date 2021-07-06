using MvAssistant.v0_2.Mac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaskAutoCleaner.v1_0.Machine.Cabinet.Drawers
{
    /// <summary>光罩盒</summary>
    public class BoxUnit
    {
        /// <summary>光罩盒條碼</summary>
        public string BoxBarcode { get; private set; }
        
        /// <summary>光罩盒型態</summary>
        public MacMaskBoxType BoxType { set;private get; }

        /// <summary>光罩</summary>
        public MaskUnit MaskUnit { get; private set; }

        /// <summary>目前有没有光罩在盒內</summary>
        public bool HasMask { get; private set; }

        /// <summary>Constructor</summary>
        public BoxUnit()
        {
            BoxBarcode = string.Empty;
            BoxType = MacMaskBoxType.DontCare;
            MaskUnit = null;
            HasMask = false;
        }

        /// <summary>Constructor</summary>
        /// <param name="barcode">光罩盒條碼</param>
        /// <param name="boxType">光罩盒種類</param>
        public BoxUnit(string barcode, MacMaskBoxType boxType):this()
        {
            BoxBarcode = barcode;
            BoxType = BoxType;

        }

        /// <summary>設定光罩盒條碼</summary>
        /// <param name="barcode"></param>
        public void SetBoxBarcode(string barcode)
        {
            BoxBarcode = barcode;
        }

        /// <summary>清除光罩盒條碼</summary>
        public void ClearBarcode()
        {
            BoxBarcode = string.Empty;
        }
        
        /// <summary>將光罩放入光罩盒內</summary>
        public void PutMaskInBox()
        {
            HasMask = true;
        }

        /// <summary>從光罩盒內拿走光罩</summary>
        public void TakeMaskFromBox()
        {
            HasMask = false;
        }

        /// <summary>綁定光罩資料</summary>
        /// <param name="maskUnit"></param>
        public void BindMask(MaskUnit maskUnit)
        {
            MaskUnit = maskUnit;
        }
        
        /// <summary>解綁光罩資料</summary>
        public void UnBindMask()
        {
            MaskUnit = null;
        }

        /// <summary>載入 Mask</summary>
        /// <param name="mask"></param>
        public void LoadMask(MaskUnit mask)
        {
            BindMask(mask); 
            PutMaskInBox();
        }

        /// <summary>卸載 Mask</summary>
        public void UnLoadMask()
        {
            UnBindMask();
            TakeMaskFromBox();
        }
    }
}
