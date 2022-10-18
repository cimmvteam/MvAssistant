using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using uEye;

namespace MvAssistant.v0_3.DeviceDrive.iDSCamera
{
    public class MvaU3BoardCam
    {
        private uEye.Camera m_Camera;
        public IntPtr m_displayHandle = IntPtr.Zero;
        private bool m_bLive = false;
        Int32 s32MemID;
        public bool BLive
        {
            get { return m_bLive; }
        }
        private const int m_cnNumberOfSeqBuffers = 3;

        public MvaU3BoardCam()
        {
            m_Camera = new uEye.Camera();
            m_Camera.Init();
            m_Camera.Memory.Allocate();
            m_Camera.Acquisition.Capture(uEye.Defines.DeviceParameter.DontWait);
        }

        public Bitmap SnapShot()
        {
            Bitmap ImgBitM;
            m_Camera.Memory.GetActive(out s32MemID);
            m_Camera.Memory.Lock(s32MemID);
            m_Camera.Memory.ToBitmap(s32MemID, out ImgBitM);
            m_Camera.Memory.Unlock(s32MemID);
            return ImgBitM;
        }
    }
}
