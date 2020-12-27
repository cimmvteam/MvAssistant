using System;
using Sentech.GenApiDotNET;
using Sentech.StApiDotNET;

namespace SaveAndLoadImage
{
	class SaveAndLoadImage
	{
		// Counts of images to grab.
		const int nCountOfImagesToGrab = 1;

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

				// Create a DataStream object for handling image stream data.
				using (CStDataStream dataStream = device.CreateStDataStream(0))
				{
					// Displays the DisplayName of the device.
					Console.WriteLine("Device=" + device.GetIStDeviceInfo().DisplayName);

					// Start the image acquisition of the host (local machine) side.
					dataStream.StartAcquisition(nCountOfImagesToGrab);

					// Start the image acquisition of the camera side.
					device.AcquisitionStart();

					// Get the path of the image files.
					string fileNameHeader = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
					fileNameHeader += @"\" + device.GetIStDeviceInfo().DisplayName + @"\" + device.GetIStDeviceInfo().DisplayName;

					bool isImageSaved = false;

					// Get the file name of the image file of the StApiRaw file format
					string fileNameRaw = fileNameHeader + ".StApiRaw";

					// Retrieve the buffer of image data with a timeout of 5000ms.
					using (CStStreamBuffer streamBuffer = dataStream.RetrieveBuffer(5000))
					{
						// Check if the acquired data contains image data.
						if (streamBuffer.GetIStStreamBufferInfo().IsImagePresent)
						{
							// If yes, we create a IStImage object for further image handling.
							IStImage stImage = streamBuffer.GetIStImage();
							Byte[] imageData = stImage.GetByteArray();

							// Display the information of the acquired image data.
							Console.Write("BlockId=" + streamBuffer.GetIStStreamBufferInfo().FrameID);
							Console.Write(" Size:" + stImage.ImageWidth + " x " + stImage.ImageHeight);
							Console.Write(" First byte =" + imageData[0] + Environment.NewLine);

							// Create a still image file handling class object (filer) for still image processing.
							using (CStStillImageFiler stillImageFiler = new CStStillImageFiler())
							{
								// Save the image file as StApiRaw file format with using the filer we created.
								Console.Write(Environment.NewLine + "Saving " + fileNameRaw + "... ");
								stillImageFiler.Save(stImage, eStStillImageFileFormat.StApiRaw, fileNameRaw);
								Console.Write("done" + Environment.NewLine);
								isImageSaved = true;
							}
						}
						else
						{
							// If the acquired data contains no image data.
							Console.WriteLine("Image data does not exist.");
						}
					}

					// Stop the image acquisition of the camera side.
					device.AcquisitionStop();

					// Stop the image acquisition of the host side.
					dataStream.StopAcquisition();

					// The following code shows how to load the saved StApiRaw and process it.
					if (isImageSaved)
					{
						// Create a buffer for storing the image data from StApiRaw file.
						using (CStImageBuffer imageBuffer = CStApiDotNet.CreateStImageBuffer())

						// Create a still image file handling class object (filer) for still image processing.
						using (CStStillImageFiler stillImageFiler = new CStStillImageFiler())

						// Create a data converter object for pixel format conversion.
						using (CStPixelFormatConverter pixelFormatConverter = new CStPixelFormatConverter())
						{
							// Load the image from the StApiRaw file into buffer.
							Console.Write(Environment.NewLine + "Loading " + fileNameRaw + "... ");
							stillImageFiler.Load(imageBuffer, fileNameRaw);
							Console.Write("done" + Environment.NewLine);

							// Convert the image data to BGR8 format.
							pixelFormatConverter.DestinationPixelFormat = eStPixelFormatNamingConvention.BGR8;
							pixelFormatConverter.Convert(imageBuffer.GetIStImage(), imageBuffer);

							// Get the IStImage interface to the converted image data.
							IStImage stImage = imageBuffer.GetIStImage();

							// Save as Bitmap
							{
								// Bitmap file extension.
								string imageFileName = fileNameHeader + ".bmp";

								// Save the image file in Bitmap format.
								Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
								stillImageFiler.Save(stImage, eStStillImageFileFormat.Bitmap, imageFileName);
								Console.Write("done" + Environment.NewLine);
							}

							// Save as Tiff
							{
								// Tiff file extension.
								string imageFileName = fileNameHeader + ".tif";

								// Save the image file in Tiff format.
								Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
								stillImageFiler.Save(stImage, eStStillImageFileFormat.TIFF, imageFileName);
								Console.Write("done" + Environment.NewLine);
							}

							// Save as PNG
							{
								// PNG file extension.
								string imageFileName = fileNameHeader + ".png";

								// Save the image file in PNG format.
								Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
								stillImageFiler.Save(stImage, eStStillImageFileFormat.PNG, imageFileName);
								Console.Write("done" + Environment.NewLine);
							}

							// Save as JPEG
							{
								// JPEG file extension.
								string imageFileName = fileNameHeader + ".jpg";

								// Save the image file in JPEG format.
								stillImageFiler.Quality = 75;
								Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
								stillImageFiler.Save(stImage, eStStillImageFileFormat.JPEG, imageFileName);
								Console.Write("done" + Environment.NewLine);
							}

							// Save as CSV
							{
								// CSV file extension.
								string imageFileName = fileNameHeader + ".csv";

								// Save the image file in CSV format.
								Console.Write(Environment.NewLine + "Saving " + imageFileName + "... ");
								stillImageFiler.Save(stImage, eStStillImageFileFormat.CSV, imageFileName);
								Console.Write("done" + Environment.NewLine);
							}
						}
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
