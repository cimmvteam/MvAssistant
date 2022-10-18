using Euresys.MultiCam;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
//using Euresys.MultiCam;

namespace MvAssistant.v0_3.DeviceDrive.CameraLink
{
    public class MvaCameraLinkLdd : IDisposable
    {
        public MvaCameraLinkLdd()
        {
        }

        ~MvaCameraLinkLdd() { this.Dispose(false); }

        private ColorPalette imgpal = null;
        private Bitmap image = null;
        UInt32 channel;
        MC.CALLBACK multiCamCallback;
        private UInt32 currentSurface;
        string ProcessStatus = "";
        private static Mutex imageMutex = new Mutex();

        public void Connect()
        {
            // Open MultiCam driver
            MC.OpenDriver();

            // Create a channel and associate it with the first connector on the first board
            MC.Create("CHANNEL", out channel);
            MC.SetParam(channel, "DriverIndex", 0);
        }

        public void Close()
        {
            MC.CloseDriver();
        }

        public Bitmap Capture()
        {
            try
            {
                image = null;
                

                // Enable error logging
                MC.SetParam(MC.CONFIGURATION, "ErrorLog", "error.log");

                // In order to support a 10-tap camera on Grablink Full
                // BoardTopology must be set to MONO_DECA
                // In all other cases the default value will work properly 
                // and the parameter doesn't need to be set

                // Set the board topology to support 10 taps mode (only with a Grablink Full)
                // MC.SetParam(MC.BOARD + 0, "BoardTopology", "MONO_DECA");

               

                // In order to use single camera on connector A
                // MC_Connector must be set to A for Grablink DualBase
                // For all other Grablink boards the parameter has to be set to M  

                // For all GrabLink boards except Grablink DualBase
                MC.SetParam(channel, "Connector", "M");
                // For Grablink DualBase
                //MC.SetParam(channel, "Connector", "A");

                // Choose the CAM file
                MC.SetParam(channel, "CamFile", "../../UserData/STC-CL25M_8T freerun");
                // Choose the camera expose duration
                MC.SetParam(channel, "Expose_us", 20000);
                // Choose the pixel color format
                MC.SetParam(channel, "ColorFormat", "Y8");

                //Set the acquisition mode to Snapshot
                MC.SetParam(channel, "AcquisitionMode", "SNAPSHOT");
                // Choose the way the first acquisition is triggered
                MC.SetParam(channel, "TrigMode", "IMMEDIATE");
                // Choose the triggering mode for subsequent acquisitions
                MC.SetParam(channel, "NextTrigMode", "REPEAT");
                // Choose the number of images to acquire
                MC.SetParam(channel, "SeqLength_Fr", MC.INDETERMINATE);

                // Register the callback function
                multiCamCallback = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(channel, multiCamCallback, channel);

                // Enable the signals corresponding to the callback functions
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");

                // Prepare the channel in order to minimize the acquisition sequence startup latency
                MC.SetParam(channel, "ChannelState", "READY");

                string channelState;

                MC.GetParam(channel, "ChannelState", out channelState);
                if (channelState != "ACTIVE")
                    MC.SetParam(channel, "ChannelState", "ACTIVE");
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
            SpinWait.SpinUntil(() => image != null);
            MC.SetParam(channel, "ChannelState", "IDLE");
            return image;
        }

        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);
                    break;
                case MC.SIG_ACQUISITION_FAILURE:
                    AcqFailureCallback(signalInfo);
                    break;
                default:
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }

        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            ProcessStatus = "Processing";
            currentSurface = signalInfo.SignalInfo;

            // + GrablinkSnapshot Sample Program

            try
            {
                // Update the image with the acquired image buffer data 
                Int32 width, height, bufferPitch;
                IntPtr bufferAddress;
                MC.GetParam(currentChannel, "ImageSizeX", out width);
                MC.GetParam(currentChannel, "ImageSizeY", out height);
                MC.GetParam(currentChannel, "BufferPitch", out bufferPitch);
                MC.GetParam(currentSurface, "SurfaceAddr", out bufferAddress);

                try
                {
                    imageMutex.WaitOne();

                    image = new Bitmap(width, height, bufferPitch, PixelFormat.Format8bppIndexed, bufferAddress);

                    imgpal = image.Palette;

                    // Build bitmap palette Y8
                    for (uint i = 0; i < 256; i++)
                    {
                        imgpal.Entries[i] = Color.FromArgb(
                        (byte)0xFF,
                        (byte)i,
                        (byte)i,
                        (byte)i);
                    }

                    image.Palette = imgpal;

                    /* Insert image analysis and processing code here */
                }
                finally
                {
                    imageMutex.ReleaseMutex();
                }

                // Retrieve the frame rate
                Double frameRate_Hz;
                MC.GetParam(channel, "PerSecond_Fr", out frameRate_Hz);

                // Retrieve the channel state
                String channelState;
                MC.GetParam(channel, "ChannelState", out channelState);

                // Display frame rate and channel state
                ProcessStatus = String.Format("Frame Rate: {0:f2}, Channel State: {1}", frameRate_Hz, channelState);

                // Display the new image
                //this.BeginInvoke(new PaintDelegate(Redraw), new object[1] { CreateGraphics() });
            }
            catch (Euresys.MultiCamException exc)
            {
                throw exc;
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
            // - GrablinkSnapshot Sample Program
        }

        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            UInt32 currentChannel = (UInt32)signalInfo.Context;

            // + GrablinkSnapshot Sample Program

            try
            {
                // Display frame rate and channel state
                ProcessStatus = String.Format("Acquisition Failure, Channel State: IDLE");
                //this.BeginInvoke(new PaintDelegate(Redraw), new object[1] { CreateGraphics() });
            }
            catch (System.Exception exc)
            {
                throw exc;
            }

            // - GrablinkSnapshot Sample Program
        }

        void Redraw(Graphics g)
        {
            // + GrablinkSnapshot Sample Program

            try
            {
                imageMutex.WaitOne();

                if (image != null)
                    g.DrawImage(image, 0, 0);
            }
            catch (System.Exception exc)
            {
                throw exc;
            }
            finally
            {
                imageMutex.ReleaseMutex();
            }

            // - GrablinkSnapshot Sample Program
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
