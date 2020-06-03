using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompCamera
{
    [GuidAttribute("DC1CA257-1564-4C86-B6FE-892B79CA107C")]
    public interface IHalCamera : IMacHalComponent
    {
        /// <summary>
        /// Camera take pictures
        /// </summary>
        /// <returns>Bitmap object</returns>
        Image Shot();


        /// <summary>
        /// 設定CCD曝光時間
        /// </summary>
        /// <param name="mseconds">曝光時間, 單位毫秒</param>
        void SetExposureTime(double mseconds);

        /// <summary>
        /// 設定焦距
        /// </summary>
        /// <param name="percentage">焦距百分比(暫定, 待討論)</param>
        void SetFocus(double percentage);
    }
}
