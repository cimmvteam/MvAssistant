using PylonC.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvAssistant.v0_3.DeviceDrive.BaslerCamera
{
    public class MvaPylonDotNetLdd_v5_0
    {


        public int Connect()
        {
            return 0;
        }

        public int Close()
        {
            return 0;
        }










        #region IDisposable
        // Flag: Has Dispose already been called?
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
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
                this.DisposeManaged();
            }

            // Free any unmanaged objects here.
            //
            this.DisposeUnmanaged();

            this.DisposeSelf();

            disposed = true;
        }



        void DisposeManaged()
        {
        }

        void DisposeUnmanaged()
        {

        }

        void DisposeSelf()
        {
            this.Close();
        }

        #endregion






        //=== Static ===============================================



        public static void ConsoleTest(string[] args)
        {

            PYLON_DEVICE_HANDLE hDev = new PYLON_DEVICE_HANDLE(); /* Handle for the pylon device. */
            try
            {
                uint numDevices;    /* Number of available devices. */
                const int numGrabs = 10; /* Number of images to grab. */
                PylonBuffer<Byte> imgBuf = null;  /* Buffer used for grabbing. */
                bool isAvail;
                int i;
#if DEBUG
                /* This is a special debug setting needed only for GigE cameras.
                See 'Building Applications with pylon' in the Programmer's Guide. */
                Environment.SetEnvironmentVariable("PYLON_GIGE_HEARTBEAT", "300000" /*ms*/);
#endif
                /* Before using any pylon methods, the pylon runtime must be initialized. */
                PylonC.NET.Pylon.Initialize();

                /* Enumerate all camera devices. You must call
                PylonEnumerateDevices() before creating a device. */
                numDevices = PylonC.NET.Pylon.EnumerateDevices();

                if (0 == numDevices)
                {
                    throw new Exception("No devices found.");
                }

                /* Get a handle for the first device found.  */
                hDev = PylonC.NET.Pylon.CreateDeviceByIndex(0);

                /* Before using the device, it must be opened. Open it for configuring
                parameters and for grabbing images. */
                PylonC.NET.Pylon.DeviceOpen(hDev, PylonC.NET.Pylon.cPylonAccessModeControl | PylonC.NET.Pylon.cPylonAccessModeStream);

                /* Set the pixel format to Mono8, where gray values will be output as 8 bit values for each pixel. */
                /* ... Check first to see if the device supports the Mono8 format. */
                isAvail = PylonC.NET.Pylon.DeviceFeatureIsAvailable(hDev, "EnumEntry_PixelFormat_Mono8");

                if (!isAvail)
                {
                    /* Feature is not available. */
                    throw new Exception("Device doesn't support the Mono8 pixel format.");
                }

                /* ... Set the pixel format to Mono8. */
                PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "PixelFormat", "Mono8");

                /* Disable acquisition start trigger if available. */
                isAvail = PylonC.NET.Pylon.DeviceFeatureIsAvailable(hDev, "EnumEntry_TriggerSelector_AcquisitionStart");
                if (isAvail)
                {
                    PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "TriggerSelector", "AcquisitionStart");
                    PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "TriggerMode", "Off");
                }

                /* Disable frame burst start trigger if available */
                isAvail = PylonC.NET.Pylon.DeviceFeatureIsAvailable(hDev, "EnumEntry_TriggerSelector_FrameBurstStart");
                if (isAvail)
                {
                    PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "TriggerSelector", "FrameBurstStart");
                    PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "TriggerMode", "Off");
                }

                /* Disable frame start trigger if available */
                isAvail = PylonC.NET.Pylon.DeviceFeatureIsAvailable(hDev, "EnumEntry_TriggerSelector_FrameStart");
                if (isAvail)
                {
                    PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "TriggerSelector", "FrameStart");
                    PylonC.NET.Pylon.DeviceFeatureFromString(hDev, "TriggerMode", "Off");
                }

                /* For GigE cameras, we recommend increasing the packet size for better
                   performance. If the network adapter supports jumbo frames, set the packet
                   size to a value > 1500, e.g., to 8192. In this sample, we only set the packet size
                   to 1500. */
                /* ... Check first to see if the GigE camera packet size parameter is supported
                    and if it is writable. */
                isAvail = PylonC.NET.Pylon.DeviceFeatureIsWritable(hDev, "GevSCPSPacketSize");

                if (isAvail)
                {
                    /* ... The device supports the packet size feature. Set a value. */
                    PylonC.NET.Pylon.DeviceSetIntegerFeature(hDev, "GevSCPSPacketSize", 1500);
                }

                /* Grab some images in a loop. */
                for (i = 0; i < numGrabs; ++i)
                {
                    Byte min, max;
                    PylonGrabResult_t grabResult;

                    /* Grab one single frame from stream channel 0. The
                    camera is set to "single frame" acquisition mode.
                    Wait up to 500 ms for the image to be grabbed.
                    If imgBuf is null a buffer is automatically created with the right size.*/
                    if (!PylonC.NET.Pylon.DeviceGrabSingleFrame(hDev, 0, ref imgBuf, out grabResult, 500))
                    {
                        /* Timeout occurred. */
                        Console.WriteLine("Frame {0}: timeout.", i + 1);
                    }

                    /* Check to see if the image was grabbed successfully. */
                    if (grabResult.Status == EPylonGrabStatus.Grabbed)
                    {
                        /* Success. Perform image processing. */
                        ConsoleTestGetMinMax(imgBuf.Array, grabResult.SizeX, grabResult.SizeY, out min, out max);
                        Console.WriteLine("Grabbed frame {0}. Min. gray value = {1}, Max. gray value = {2}", i + 1, min, max);

                        /* Display image */
                        PylonC.NET.Pylon.ImageWindowDisplayImage<Byte>(0, imgBuf, grabResult);
                    }
                    else if (grabResult.Status == EPylonGrabStatus.Failed)
                    {
                        Console.Error.WriteLine("Frame {0} wasn't grabbed successfully.  Error code = {1}", i + 1, grabResult.ErrorCode);
                    }
                }
                /* Release the buffer. */
                imgBuf.Dispose();

                /* Clean up. Close and release the pylon device. */
                PylonC.NET.Pylon.DeviceClose(hDev);
                PylonC.NET.Pylon.DestroyDevice(hDev);

                /* Free memory for grabbing. */
                imgBuf = null;

                Console.Error.WriteLine("\nPress enter to exit.");
                Console.ReadLine();

                /* Shut down the pylon runtime system. Don't call any pylon method after
                   calling Pylon.Terminate(). */
                PylonC.NET.Pylon.Terminate();
            }
            catch (Exception e)
            {
                /* Retrieve the error message. */
                string msg = GenApi.GetLastErrorMessage() + "\n" + GenApi.GetLastErrorDetail();
                Console.Error.WriteLine("Exception caught:");
                Console.Error.WriteLine(e.Message);
                if (msg != "\n")
                {
                    Console.Error.WriteLine("Last error message:");
                    Console.Error.WriteLine(msg);
                }

                try
                {
                    if (hDev.IsValid)
                    {
                        /* ... Close and release the pylon device. */
                        if (PylonC.NET.Pylon.DeviceIsOpen(hDev))
                        {
                            PylonC.NET.Pylon.DeviceClose(hDev);
                        }
                        PylonC.NET.Pylon.DestroyDevice(hDev);
                    }
                }
                catch (Exception)
                {
                    /*No further handling here.*/
                }

                PylonC.NET.Pylon.Terminate();  /* Releases all pylon resources. */

                Console.Error.WriteLine("\nPress enter to exit.");
                Console.ReadLine();

                Environment.Exit(1);
            }
        }

        /* Simple "image processing" function returning the minimum and maximum gray
        value of an 8 bit gray value image. */
        static void ConsoleTestGetMinMax(Byte[] imageBuffer, long width, long height, out Byte min, out Byte max)
        {
            min = 255; max = 0;
            long imageDataSize = width * height;

            for (long i = 0; i < imageDataSize; ++i)
            {
                Byte val = imageBuffer[i];
                if (val > max)
                    max = val;
                if (val < min)
                    min = val;
            }
        }

    }
}
