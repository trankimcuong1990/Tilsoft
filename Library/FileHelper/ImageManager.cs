using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace Library.FileHelper
{
    public class ImageManager : FileManager
    {
        private string _ThumbnailFolder;

        public ImageManager(string storeFolder, string thumbnailFolder)
            : base(storeFolder) 
        {
            _ThumbnailFolder = thumbnailFolder;
        }

        public bool CreateThumbnail(string sourceFile, out string outputFile, int nWidth, int nHeight)
        {
            outputFile = sourceFile;
            if (!this.Validate(sourceFile))
            {
                return false;
            }
            outputFile = resizeImage(sourceFile, nWidth, nHeight);

            return true;
        }

        public override void DeleteFile(string path)
        {
            base.DeleteFile(path);

            // delete thumbnail
            try
            {
                path = _ThumbnailFolder + path.Replace("/", @"\");
                File.Delete(path);
            }
            catch { }
        }

        public bool StoreThumbnail(string sourceFileFullPath, out string outputString, out string outputFullPath)
        {
            bool result = StoreFile(_ThumbnailFolder, sourceFileFullPath, out outputString, out outputFullPath);
            try
            {
                File.Delete(sourceFileFullPath);
            }
            catch { }

            return result;
        }

        public bool RotateImage(string filePath, int degree)
        {
            try
            {
                ImageProcessor.ImageFactory mFact = new ImageProcessor.ImageFactory();
                mFact.Load(filePath);
                mFact.Rotate(degree);
                mFact.Save(filePath);
                return true;
            }
            catch { return false; }   
        }

        public bool CreateThumbnail2(string thumbnailFolder, string filename)
        {
            if (!Directory.Exists(thumbnailFolder)) return false;
            if (!File.Exists(filename)) return false;
            FileInfo fInfo = new FileInfo(filename);
            string namePart = fInfo.Name.Replace(fInfo.Extension, "");

            try
            {
                ImageProcessor.ImageFactory mFact = new ImageProcessor.ImageFactory();
                ImageProcessor.Imaging.Formats.JpegFormat format = new ImageProcessor.Imaging.Formats.JpegFormat();
                format.Quality = 100;

                mFact.Load(filename).Resize(new Size(300, 300)).Format(format).BackgroundColor(Color.White).Save(thumbnailFolder + @"\" + namePart + ".jpg");

                return true;
            }
            catch { return false; } 
        }

        //
        // PRIVATE FUNCTIONS
        //
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

        public string resizeImage(string srcImageFilePath, int nWidth, int nHeight)
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
            return FrameworkSetting.Setting.AbsoluteReportTmpFolder + System.Guid.NewGuid().ToString().Replace("-", "") + "." + ext;
        }
    }
}