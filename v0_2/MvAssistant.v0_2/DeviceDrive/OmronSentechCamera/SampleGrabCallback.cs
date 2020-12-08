// If you want to use the GUI features, please remove the comment.
//#define ENABLED_ST_GUI

using System;
using Sentech.GenApiDotNET;
using Sentech.StApiDotNET;

namespace GrabCallback
{
	class GrabCallback
	{
		// Method for handling callback action
		static void OnCallback(IStCallbackParamBase paramBase, object[] param)
		{
			// Check callback type. Only NewBuffer event is handled in here
			if (paramBase.CallbackType == eStCallbackType.TL_DataStreamNewBuffer)
			{
				// In case of receiving a NewBuffer events:
				// Convert received callback parameter into IStCallbackParamGenTLEventNewBuffer for acquiring additional information.
				IStCallbackParamGenTLEventNewBuffer callbackParam = paramBase as IStCallbackParamGenTLEventNewBuffer;

				if (callbackParam != null)
				{
					try
					{
						// Get the IStDataStream interface object from the received callback parameter.
						IStDataStream dataStream = callbackParam.GetIStDataStream();

						// Retrieve the buffer of image data for that callback indicated there is a buffer received.
						using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(0))
						{
							// Check if the acquired data contains image data.
							if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
							{
								// If yes, we create a IStImage object for further image handling.
								IStImage stImage = streamBuffer.GetIStImage();
#if ENABLED_ST_GUI
								CStImageDisplayWnd wnd = (CStImageDisplayWnd)param[0];

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
					catch (Exception e)
					{
						// If any exception occurred, display the description of the error here.
						Console.Error.WriteLine("An exception occurred. \r\n" + e.Message);
					}
				}
			}
		}

		static void Main(string[] args)
		{
			try
			{
				// Initialize StApi before using.
				using (CStApiAutoInit api = new CStApiAutoInit())

				// Create a system object for device scan and connection.
				using (CStSystem system = new CStSystem())

				// Create a camera device object and connect to first detected device.
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

					// Register callback method. Note that by different usage, we pass different kinds/numbers of parameters in.
#if ENABLED_ST_GUI
					object[] param = { wnd };
					dataStream.RegisterCallbackMethod(OnCallback, param);
#else
					dataStream.RegisterCallbackMethod(OnCallback);
#endif
					// Start the image acquisition of the host (local machine) side.
					dataStream.StartAcquisition();

					// Start the image acquisition of the camera side.
					device.AcquisitionStart();

					// Keep getting image until Enter key pressed.
					Console.WriteLine("\r\nPress Enter to exit.");
					Console.ReadLine();

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
		}
	}
}
