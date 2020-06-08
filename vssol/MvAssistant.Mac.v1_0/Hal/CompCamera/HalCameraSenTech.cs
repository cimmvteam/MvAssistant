using MvAssistant.DeviceDrive.OmronSentechCamera;
using MvAssistant.Mac.v1_0;
using MvAssistant.Mac.v1_0.Hal.Component;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.Mac.v1_0.Hal.CompCamera
{
    [GuidAttribute("90BA4CDC-7A82-454A-8F3F-6FE6413AEF41")]
    public class HalCameraSenTech : MacHalComponentBase, IHalCamera
    {
        public const string DevConnStr_Id = "id";
        string id;
        string resourceKey { get { return "MvCameraDeviceScanner"; } }
        MvOmronSentechCameraLdd ldd;


        public override int HalConnect()
        {
            this.id = this.GetDevSetting(DevConnStr_Id);

            var scanner = this.HalContext.ResourceGetOrDefault<MvCameraDeviceScanner>(this.resourceKey);
            if (scanner == null)
            {
                scanner = new MvCameraDeviceScanner();
                scanner.ScanAlldevice();
                this.HalContext.ResourceRegister(this.resourceKey, scanner);
            }


            if (this.ldd == null)
            {
                this.ldd = scanner.cameras[this.id];
                //this.HalContext.ResourceRegister(this.resourceKey, this.ldd);
            }




            return 0;
        }

        public void SetExposureTime(double mseconds)
        {
            throw new NotImplementedException();
        }

        public void SetFocus(double percentage)
        {
            throw new NotImplementedException();
        }

        public Image Shot()
        {
            throw new NotImplementedException();
        }
    }
}
