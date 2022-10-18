using CToolkitCs.v1_2.Extension;
using CToolkitCs.v1_2.WinApiNative;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CToolkitCs.v1_2.WinApi
{
    public class CtkWinApiGdiPlus
    {
        public static void GaussianBlur(Bitmap bmp, ref Rectangle Rect, float Radius = 10, bool ExpandEdge = false)
        {
            CtkGpStatus result;
            IntPtr blurEffect;
            CtkBlurParams blurPara = new CtkBlurParams();
            if ((Radius < 0) || (Radius > 255))
            {
                throw new ArgumentOutOfRangeException("半径必须在[0,255]范围内");
            }
            blurPara.Radius = Radius;
            blurPara.ExpandEdge = ExpandEdge;
            result = CtkGdiPlus.GdipCreateEffect(CtkGdiPlus.BlurEffectGuid, out blurEffect);
            if (result == 0)
            {
                IntPtr Handle = Marshal.AllocHGlobal(Marshal.SizeOf(blurPara));
                Marshal.StructureToPtr(blurPara, Handle, true);
                CtkGdiPlus.GdipSetEffectParameters(blurEffect, Handle, (uint)Marshal.SizeOf(blurPara));

                var nativeHandle = NativeHandle(bmp);

                CtkGdiPlus.GdipBitmapApplyEffect(nativeHandle, blurEffect, ref Rect, false, IntPtr.Zero, 0);
                // 使用GdipBitmapCreateApplyEffect函数可以不改变原始的图像，而把模糊的结果写入到一个新的图像中
                CtkGdiPlus.GdipDeleteEffect(blurEffect);
                Marshal.FreeHGlobal(Handle);
            }
            else
            {
                throw new ExternalException("不支持的GDI+版本，必须为GDI+1.1及以上版本，且操作系统要求为Win Vista及之后版本.");
            }
        }


        public static IntPtr NativeHandle(Bitmap Bmp)
        {
            return Bmp.GetPrivateField<IntPtr>("nativeImage");
            /*  用Reflector反编译System.Drawing.Dll可以看到Image类有如下的私有字段
                internal IntPtr nativeImage;
                private byte[] rawData;
                private object userData;
                然后还有一个 SetNativeImage函数
                internal void SetNativeImage(IntPtr handle)
                {
                    if (handle == IntPtr.Zero)
                    {
                        throw new ArgumentException(SR.GetString("NativeHandle0"), "handle");
                    }
                    this.nativeImage = handle;
                }
                这里在看看FromFile等等函数，其实也就是调用一些例如GdipLoadImageFromFile之类的GDIP函数，并把返回的GDIP图像句柄
                通过调用SetNativeImage赋值给变量nativeImage，因此如果我们能获得该值，就可以调用VS2010暂时还没有封装的GDIP函数
                进行相关处理了，并且由于.NET肯定已经初始化过了GDI+，我们也就无需在调用GdipStartup初始化他了。
             */
        }
    }
}
