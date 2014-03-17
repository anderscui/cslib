// Source: http://www.codeproject.com/Articles/2927/Creating-a-Watermarked-Photograph-with-GDI-for-NET

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;

using Andersc.CodeLib.Common;

namespace Andersc.CodeLib.ConsoleAppSamples
{
    internal class Watermark
    {
        static void Main(string[] args)
        {
            var image = @"D:\works\git\ef-mediahub\MediaHub.Common.Tests\bin\Debug\sample.jpg";
            AddWatermark(image,
                watermarkImage: @"D:\works\git\ef-mediahub\MediaHub.Common.Tests\bin\Debug\watermark_ef.png",
                opacity: 2.0f);

            File.Delete(image);
        }

        private static void AddWatermark(string imageFilePath,
            string watermarkText = null,
            string watermarkImage = null,
            float opacity = 0.3f,
            string fontName = "arial",
            string postfix = "_watermarked",
            string outputDir = null)
        {
            var originalImagePath = imageFilePath;

            //create a image object containing the photograph to watermark
            Image imgPhoto = Image.FromFile(originalImagePath);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            #region Step #2 - Insert Watermark image
            if (watermarkImage.IsNotNullOrEmpty())
            {
                //create a image object containing the watermark
                //Image imgWatermark = new Bitmap(watermarkImagePath);
                var imgWatermark = Image.FromFile(watermarkImage);
                int wmWidth = imgWatermark.Width;
                int wmHeight = imgWatermark.Height;

                //Load this Bitmap into a new Graphic Object
                Graphics grWatermark = Graphics.FromImage(imgPhoto);

                //To achieve a transulcent watermark we will apply (2) color 
                //manipulations by defineing a ImageAttributes object and 
                //seting (2) of its properties.
                ImageAttributes imageAttributes = new ImageAttributes();

                //The first step in manipulating the watermark image is to replace 
                //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
                //to do this we will use a Colormap and use this to define a RemapTable
                ColorMap colorMap = new ColorMap();

                //My watermark was defined with a background of 100% Green this will
                //be the color we search for and replace with transparency
                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                //The second color manipulation is used to change the opacity of the 
                //watermark.  This is done by applying a 5x5 matrix that contains the 
                //coordinates for the RGBA space.  By setting the 3rd row and 3rd column 
                //to 0.3f we achive a level of opacity
                float[][] colorMatrixElements = { 
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  opacity, 0.0f},
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                    ColorAdjustType.Bitmap);

                //For this example we will place the watermark in the upper right
                //hand corner of the photograph. offset down 10 pixels and to the 
                //left 10 pixles

                int xPosOfWm = 10;
                int yPosOfWm = ((phHeight - wmHeight) - 10);

                grWatermark.DrawImage(imgWatermark,
                    new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination position
                    0,                  // x-coordinate of the portion of the source image to draw. 
                    0,                  // y-coordinate of the portion of the source image to draw. 
                    wmWidth,            // Watermark Width
                    wmHeight,		    // Watermark Height
                    GraphicsUnit.Pixel, // Unit of measurment
                    imageAttributes);   //ImageAttributes Object

                imgWatermark.Dispose();
                grWatermark.Dispose();
            }

            #endregion

            //save new image to file system.
            if (outputDir.IsNullOrEmpty())
            {
                outputDir = Path.GetDirectoryName(imageFilePath);
            }

            var generatedName =
                Path.Combine(outputDir, 
                             Path.GetFileNameWithoutExtension(imageFilePath) + postfix + Path.GetExtension(imageFilePath));
            imgPhoto.Save(generatedName, GetImageFormat(imageFilePath));

            imgPhoto.Dispose();
        }

        private static ImageFormat GetImageFormat(string imageFileName)
        {
            var format = ImageFormat.Jpeg;
            switch (Path.GetExtension(imageFileName))
            {
                case ".png":
                    format = ImageFormat.Png;
                    break;
                case ".bmp":
                    format = ImageFormat.Bmp;
                    break;
            }

            return format;
        }
    }
}
