using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace AppDataConverter
{
    public class ImageStorage
    {
        private string _ThumbnailFolder;

        public ImageStorage(string storageFolder)
        {
            _ThumbnailFolder = storageFolder + @"\WebImages\thumbnail\";
            if (!Directory.Exists(_ThumbnailFolder))
            {
                Directory.CreateDirectory(_ThumbnailFolder);
            }
        }

        public string StoreImage(string sourceFile, string thumbPath, string filePath)
        {
            string result = string.Empty;
            return result;
        }

        public bool StoreThumbnail(string sourceFileFullPath, out string outputString, out string outputFullPath)
        {
            sourceFileFullPath = resizeImage(sourceFileFullPath, 250, 250);
            return StoreFile(_ThumbnailFolder, sourceFileFullPath, out outputString, out outputFullPath);
        }

        public bool StoreFile(string storePath, string sourceFileFullPath, out string outputString, out string outputFullPath)
        {
            outputString = string.Empty;
            outputFullPath = string.Empty;

            if (!File.Exists(sourceFileFullPath))
            {
                return false;
            }

            try
            {
                FileInfo info = new FileInfo(sourceFileFullPath);
                string filePath = info.Name.Substring(0, 1).ToLower() + @"\" + DateTime.Now.ToString("ddMMyyyy") + @"\";
                string fileName = System.Guid.NewGuid().ToString().Replace("-", "").ToLower() + info.Extension;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(storePath + filePath);
                }

                File.Copy(sourceFileFullPath, storePath + filePath + fileName);

                outputString = filePath.Replace(@"\", "/") + fileName;
                outputFullPath = storePath + filePath + fileName;
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool Validate(string sourceFile)
        {
            // check if file is exists
            if (!File.Exists(sourceFile))
            {
                return false;
            }

            // check if mime type is image
            string[] validExtension = new string[] { ".jpg", ".gif", ".bmp", ".png" };
            FileInfo info = new FileInfo(sourceFile);
            if (!validExtension.Contains(info.Extension.ToLower()))
            {
                return false;
            }

            return true;
        }

        private string resizeImage(string srcImageFilePath, int nWidth, int nHeight)
        {
            return resizeImage(srcImageFilePath, string.Empty, nWidth, nHeight);
        }

        private string resizeImage(string srcImageFilePath, string srcWaterMark, int nWidth, int nHeight)
        {
            string tmpFilePath = CreateTmpFile("jpg");
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100);

            Bitmap srcImage = new Bitmap(srcImageFilePath);
            if (nWidth == 0) nWidth = srcImage.Width;
            if (nHeight == 0) nHeight = srcImage.Height;

            Double xRatio = (double)srcImage.Width / nWidth;
            Double yRatio = (double)srcImage.Height / nHeight;
            Double ratio = Math.Max(xRatio, yRatio);

            int nnx = (int)Math.Floor(srcImage.Width / ratio);
            int nny = (int)Math.Floor(srcImage.Height / ratio);

            int npx = (int)Math.Floor((Convert.ToDouble(nWidth) - nnx) / 2);
            int npy = (int)Math.Floor((Convert.ToDouble(nHeight) - nny) / 2);

            Bitmap resizedImage = new Bitmap(srcImage, nnx, nny);
            Bitmap newImage = new Bitmap(nWidth, nHeight, PixelFormat.Format32bppArgb);
            newImage.SetResolution(72, 72);

            Graphics gr = Graphics.FromImage(newImage);
            gr.CompositingQuality = CompositingQuality.HighQuality;
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gr.FillRectangle(Brushes.White, 0, 0, nWidth, nHeight);
            gr.DrawImage(resizedImage, npx, npy, nnx, nny);

            if (!string.IsNullOrEmpty(srcWaterMark))
            {
                Bitmap watermarkImg = new Bitmap(srcWaterMark);
                Bitmap resizedWaterMark = new Bitmap(watermarkImg, nnx, nny);
                gr.DrawImage(resizedWaterMark, npx, npy, nnx, nny);
            }

            gr.Flush();
            gr.Dispose();

            newImage.Save(tmpFilePath, ImageCodecInfo.GetImageEncoders()[1], encoderParameters);

            newImage.Dispose();
            resizedImage.Dispose();
            srcImage.Dispose();

            return tmpFilePath;
        }

        private string CreateTmpFile(string ext)
        {
            return Path.GetTempPath() + @"\" + System.Guid.NewGuid().ToString().Replace("-", "") + "." + ext;
        }
    }
}
