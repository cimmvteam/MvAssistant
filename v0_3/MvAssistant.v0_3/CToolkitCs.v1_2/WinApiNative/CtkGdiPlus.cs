using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CToolkitCs.v1_2.WinApiNative
{
    public class CtkGdiPlus
    {
        /*Reference: https://docs.microsoft.com/zh-tw/windows/win32/gdiplus/-gdiplus-bitmap-flat?redirectedfrom=MSDN */
        /*Reference: http://msdn.microsoft.com/en-us/library/ms534057(v=vs.85).aspx*/


        public static Guid BlurEffectGuid = Guid.Parse("633C80A4-1843-482B-9EF2-BE2834C5FDD4");


        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CtkGpStatus GdipBitmapApplyEffect(IntPtr bitmap, IntPtr effect, ref Rectangle rectOfInterest, bool useAuxData, IntPtr auxData, int auxDataSize);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CtkGpStatus GdipCreateEffect(Guid guid, out IntPtr effect);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CtkGpStatus GdipSetEffectParameters(IntPtr effect, IntPtr para, UInt32 size);

        [DllImport("gdiplus.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern CtkGpStatus GdipDeleteEffect(IntPtr effect);

    }



}
