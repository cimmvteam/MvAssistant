using System;
using Sentech.GenApiDotNET;
using Sentech.StApiDotNET;

namespace MultipleCameras
{
	class SampleMultipleCameras
	{
		// Counts of images to grab.
		const int nCountOfImagesToGrab = 10;

		static void Main(string[] args)
		{
			try
			{
				// Initialize StApi before using.
				using (CStApiAutoInit api = new CStApiAutoInit())

				// Create a system object for device scan and connection.
				using (CStSystem system = new CStSystem())

				// Create a camera device list object to store all the cameras.
				using (CStDeviceArray deviceList = new CStDeviceArray())

				// Create a DataStream list object to store all the data stream object related to the cameras.
				using (CStDataStreamArray dataStreamList = new CStDataStreamArray())
				{
					{
						CStDevice device = null;

						while (true)
						{
							try
							{
								// Create a camera device object and connect to first detected device.
								device = system.CreateFirstStDevice();
							}
							catch
							{
								if (deviceList.GetSize() == 0)
								{
									throw;
								}

								break;
							}

							// Add the camera into device object list for later usage.
							deviceList.Register(device);

							// Displays the DisplayName of the device.
							Console.WriteLine("Device" + deviceList.GetSize() + "=" + device.GetIStDeviceInfo().DisplayName);

							// Create a DataStream object for handling image stream data then add into DataStream list for later usage.
							dataStreamList.Register(device.CreateStDataStream(0));
						}

						// Start the image acquisition of the host side.
						dataStreamList.StartAcquisition(nCountOfImagesToGrab);

						// Start the image acquisition of the camera side.
						deviceList.AcquisitionStart();

						// A while loop for acquiring data and checking status. 
						// Here we use DataStream list function to check if any cameras in the list is on grabbing.
						while (dataStreamList.IsGrabbingAny)
						{
							// Retrieve data buffer of image data from any camera with a timeout of 5000ms.
							using (CStStreamBuffer streamBuffer = dataStreamList.RetrieveBuffer(5000))
							{
								// Check if the acquired data contains image data.
								if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
								{
									Console.Write(streamBuffer.GetIStDataStream().GetIStDevice().GetIStDeviceInfo().DisplayName);
									Console.Write(" : BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
									Console.Write(" " + streamBuffer.GetIStDataStream().CurrentFPS.ToString("F") + "FPS" + Environment.NewLine);
								}
								else
								{
									// If the acquired data contains no image data.
									Console.WriteLine("Image data does not exist");
								}
							}
						}

						// Stop the image acquisition of the camera side.
						deviceList.AcquisitionStop();

						// Stop the image acquisition of the host side.
						dataStreamList.StopAcquisition();
					}
				}
			}
			catch (Exception e)
			{
				// If any exception occurred, display the description of the error here.
				Console.Error.WriteLine("An exception occurred. \r\n" + e.Message);
			}
			finally
			{
				// Wait until the Enter key is pressed.
				Console.WriteLine("\r\nPress Enter to exit.");
				Console.ReadLine();
			}
		}
	}
}
