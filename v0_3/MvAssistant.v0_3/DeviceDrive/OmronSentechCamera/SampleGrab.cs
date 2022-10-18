// If you want to use the GUI features, uncomment the following for defining ENABLED_ST_GUI with further operation.
//#define ENABLED_ST_GUI

using System;
using Sentech.GenApiDotNET;
using Sentech.StApiDotNET;

namespace Grab
{
	class Grab
	{
		// Counts of images to grab.
		const int nCountOfImagesToGrab = 100;

		static void Main(string[] args)
		{
			try
			{
				// Initialize StApi before using.
				using (CStApiAutoInit api = new CStApiAutoInit())

				// Create a system object for device scan and connection.
				using (CStSystem system = new CStSystem())

				// Create a camera device object and connect to first detected device by using the function of system object.
				using (CStDevice device = system.CreateFirstStDevice())

#if ENABLED_ST_GUI
				// If using GUI for display, create a display window here.
				using (CStImageDisplayWnd wnd = new CStImageDisplayWnd())
#endif
				// Create a DataStream object for handling image stream data.
				using (CStDataStream dataStream = device.CreateStDataStream(0))
				{
					// Displays the DisplayName of the device.
					Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

					// Start the image acquisition of the host (local machine) side.
					dataStream.StartAcquisition(nCountOfImagesToGrab);

					// Start the image acquisition of the camera side.
					device.AcquisitionStart();

					// A while loop for acquiring data and checking status. 
					// Here, the acquisition runs until it reaches the assigned numbers of frames.
					while (dataStream.IsGrabbing)
					{
						// Retrieve the buffer of image data with a timeout of 5000ms.
						// Use the 'using' statement for automatically managing the buffer re-queue action when it's no longer needed.
						using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(5000))
						{
							// Check if the acquired data contains image data.
							if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
							{
								// If yes, we create a IStImage object for further image handling.
								IStImage stImage = streamBuffer.GetIStImage();
#if ENABLED_ST_GUI
								// Acquire detail information of received image and display it onto the status bar of the display window.
								string strText = device.GetIStDeviceInfo().DisplayName + " ";
								strText += stImage.ImageWidth + " x " + stImage.ImageHeight + " ";
								strText += string.Format("{0:F2}[fps]", dataStream.CurrentFPS);
								wnd.SetUserStatusBarText(strText);

								// Check if display window is visible.
								if (!wnd.IsVisible)
								{
									// Set the position and size of the window.
									wnd.SetPosition(0, 0, (int)stImage.ImageWidth, (int)stImage.ImageHeight);

									// Create a new thread to display the window.
									wnd.Show(eStWindowMode.ModalessOnNewThread);
								}

								// Register the image to be displayed.
								// This will have a copy of the image data and original buffer can be released if necessary.
								wnd.RegisterIStImage(stImage);
#else
								// Display the information of the acquired image data.
								Byte[] imageData = stImage.GetByteArray();
								Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
								Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
								Console.Write(" First byte =" + imageData[0] + Environment.NewLine);
#endif
							}
							else
							{
								// If the acquired data contains no image data.
								Console.WriteLine("Image data does not exist.");
							}
						}
					}

					// Stop the image acquisition of the camera side.
					device.AcquisitionStop();

					// Stop the image acquisition of the host side.
					dataStream.StopAcquisition();
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
