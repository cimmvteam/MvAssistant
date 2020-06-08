// 如果要使用GUI功能，請取消註解以下內容，以定義ENABLED_ST_GUI並進行進一步的操作
//#define ENABLED_ST_GUI
using Sentech.StApiDotNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.DeviceDrive.OmronSentechCamera
{
    public class MvCameraDeviceScanner : IDisposable
    {
        public MvOmronSentechCameraLdd BT_ccd_gripper_1;
        public MvOmronSentechCameraLdd CC_ccd_particle_1;
        public MvOmronSentechCameraLdd IC_ccd_inspect_side_1;
        public MvOmronSentechCameraLdd IC_ccd_inspect_top_1;
        public MvOmronSentechCameraLdd IC_ccd_defense_side_1;
        public MvOmronSentechCameraLdd IC_ccd_defense_top_1;
        public MvOmronSentechCameraLdd LP_ccd_front_1;
        public MvOmronSentechCameraLdd LP_ccd_side_1;
        public MvOmronSentechCameraLdd LP_ccd_top_1;
        public MvOmronSentechCameraLdd MT_ccd_pellicle_deform_1;
        public MvOmronSentechCameraLdd MT_ccd_barcode_reader_1;
        public MvOmronSentechCameraLdd OS_ccd_side_1;
        public MvOmronSentechCameraLdd OS_ccd_top_1;
        public MvOmronSentechCameraLdd OS_ccd_front_1;
        public MvOmronSentechCameraLdd OS_ccd_barcode_1;

        public MvCameraDeviceScanner()
        {
            ScanAlldevice();
        }
        ~MvCameraDeviceScanner() { this.Dispose(false); }

        // 要取得的影像數量
        const int nCountOfImagesToGrab = 1;

        CStApiAutoInit api;
        CStSystem system;
        CStDevice[] StDevice;
        CStDataStream[] dataStream;
        CStImageBuffer imageBuffer;
        CStStillImageFiler stillImageFiler;
        uint uInterface = 0;
        uint uCamCnt = 0;
        IStInterface StInterface;
        string[] cameraIDStringArray;
        string[] cameraNameStringArray;

        public void Connect()
        {
            api = new CStApiAutoInit();
            system = new CStSystem();
            StDevice = new CStDevice[0];
            dataStream = new CStDataStream[0];
            cameraIDStringArray = new string[0];
            cameraNameStringArray = new string[0];
        }
        public void Close()
        {
            if (dataStream != null)
            {
                // 停止主機取像
                for (int i = 0; i < uCamCnt; i++)
                {
                    dataStream[i].StopAcquisition();
                    dataStream[i].Dispose();
                }
            }

            if (StDevice != null)
            {
                // 停止相機取像
                for (int i = 0; i < uCamCnt; i++)
                {
                    StDevice[i].AcquisitionStop();
                    StDevice[i].Dispose();
                }
            }

            if (api != null)
                api.Dispose();
            if (system != null)
                system.Dispose();

            if (BT_ccd_gripper_1 != null)
                BT_ccd_gripper_1.Dispose();
            if (CC_ccd_particle_1 != null)
                CC_ccd_particle_1.Dispose();
            if (IC_ccd_inspect_side_1 != null)
                IC_ccd_inspect_side_1.Dispose();
            if (IC_ccd_inspect_top_1 != null)
                IC_ccd_inspect_top_1.Dispose();
            if (IC_ccd_defense_side_1 != null)
                IC_ccd_defense_side_1.Dispose();
            if (IC_ccd_defense_top_1 != null)
                IC_ccd_defense_top_1.Dispose();
            if (LP_ccd_front_1 != null)
                LP_ccd_front_1.Dispose();
            if (LP_ccd_side_1 != null)
                LP_ccd_side_1.Dispose();
            if (LP_ccd_top_1 != null)
                LP_ccd_top_1.Dispose();
            if (MT_ccd_pellicle_deform_1 != null)
                MT_ccd_pellicle_deform_1.Dispose();
            if (MT_ccd_barcode_reader_1 != null)
                MT_ccd_barcode_reader_1.Dispose();
            if (OS_ccd_side_1 != null)
                OS_ccd_side_1.Dispose();
            if (OS_ccd_top_1 != null)
                OS_ccd_top_1.Dispose();
            if (OS_ccd_front_1 != null)
                OS_ccd_front_1.Dispose();
            if (OS_ccd_barcode_1 != null)
                OS_ccd_barcode_1.Dispose();
    }

    public string[] ScanAlldevice()
    {
        uInterface = system.InterfaceCount;
        //comboBox1.Items.Clear();
        for (uint i = 0; i < uInterface; i++)
        {
            StInterface = system.GetIStInterface(i);
            //IStInterface tmpInterFacePtr = StSystem.GetIStInterface(i);
            IStInterfaceInfo tmpInterFaceInfoPtr = StInterface.GetIStInterfaceInfo();
            uint uintDeviceCnt = StInterface.DeviceCount;

            Array.Resize(ref cameraIDStringArray, (int)uCamCnt + (int)uintDeviceCnt);
            Array.Resize(ref cameraNameStringArray, (int)uCamCnt + (int)uintDeviceCnt);
            Array.Resize(ref StDevice, (int)uCamCnt + (int)uintDeviceCnt);
            Array.Resize(ref dataStream, (int)uCamCnt + (int)uintDeviceCnt);

            for (uint j = 0; j < uintDeviceCnt; j++)
            {
                IStDeviceInfo tmpDeviceInfoPtr = StInterface.GetIStDeviceInfo(j);
                cameraIDStringArray[uCamCnt] = tmpDeviceInfoPtr.ID;
                cameraNameStringArray[uCamCnt] = tmpDeviceInfoPtr.DisplayName;

                eDeviceAccessFlags deviceAccessFlags = eDeviceAccessFlags.CONTROL;
                if (tmpDeviceInfoPtr.AccessStatus == eDeviceAccessStatus.READONLY)
                {
                    deviceAccessFlags = eDeviceAccessFlags.READONLY;
                }

                StDevice[uCamCnt] = StInterface.CreateStDevice(cameraIDStringArray[uCamCnt], deviceAccessFlags);

                dataStream[uCamCnt] = StDevice[uCamCnt].CreateStDataStream(0);

                BT_ccd_gripper_1 = new MvOmronSentechCameraLdd(StDevice[uCamCnt], dataStream[uCamCnt]);

                uCamCnt++;
            }
        }
        return cameraNameStringArray;
    }

    #region IDisposable
    // Flag: Has Dispose already been called?
    protected bool disposed = false;

    // Public implementation of Dispose pattern callable by consumers.
    public virtual void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    // Protected implementation of Dispose pattern.
    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
            return;

        if (disposing)
        {
            // Free any other managed objects here.
            //
        }

        // Free any unmanaged objects here.
        //

        this.DisposeSelf();

        disposed = true;
    }


    protected virtual void DisposeSelf()
    {
        this.Close();
    }



    #endregion

}
}
