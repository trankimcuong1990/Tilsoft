using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace AppDataConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageFolder = @"D:\OMSImages";

            //Console.WriteLine("Retrieving Images from OMSv4 - storage: " + storageFolder);
            //DownloadImage(storageFolder);

            Console.WriteLine("Update TILSOFT image - storage: " + storageFolder);
            UpdateOMSOnlineProductImage(storageFolder);

            Console.WriteLine("------> DONE !!! <------");
            Console.ReadLine();
        }

        public static char getNextChar(char iChar)
        {
            if (((int)iChar < 90 && (int)iChar >= 65) || ((int)iChar < 57 && (int)iChar >= 48))
            {
                return (char)((int)iChar + 1);
            }
            else if ((int)iChar == 57)
            {
                return 'A';
            }
            else if ((int)iChar == 90)
            {
                return '0';
            }

            return '-';
        }

        public static string getNextCode(string iCode)
        {
            string lastCode = "";
            for (int i = 0; i < iCode.Length; i++)
            {
                lastCode += "Z";
            }
            if (iCode == lastCode) return string.Empty;

            string nextCode = string.Empty;
            char nextChar;
            bool isOK = false;
            for (int i = iCode.Length - 1; i >= 0; i--)
            {
                nextChar = iCode[i];
                if (isOK == false)
                {
                    nextChar = getNextChar(iCode[i]);
                    if (nextChar == '-')
                    {
                        return string.Empty;
                    }
                    else
                    {
                        if (nextChar != '0')
                        {
                            isOK = true;
                        }
                    }
                }

                nextCode = nextChar.ToString() + nextCode;
            }

            return nextCode;
        }

        public static void DownloadImage(string storageFolder)
        {
            string tmpFile;
            string tmpFolder;

            string listQuery;
            string detailQuery;            
            SqlDataReader listReader;
            SqlDataReader detailReader;

            SqlCommand listComm = new SqlCommand();
            listComm.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLSourceConnection"].ConnectionString);
            listComm.CommandType = System.Data.CommandType.Text;

            // get image data
            // 360 movies
            listQuery = "SELECT TOP 50 Models.ModelUD, Products.ProductID, ProductOptionGalleryImage.ImageFileName, ProductOptionGalleryImage.IsFreeScanImage, ProductOptionGalleryImage.IsGardenImage, ProductOptionGalleryImage.IsOldImage FROM ProductOptionGallery LEFT JOIN Products ON (Products.ProductID = ProductOptionGallery.ProductID) LEFT JOIN Models ON (Models.ModelID = Products.ModelID) LEFT JOIN ProductOptionGalleryImage ON (ProductOptionGalleryImage.ProductOptionGalleryID = ProductOptionGallery.ProductOptionGalleryID) ORDER BY Models.ModelUD, Products.ProductID";
            listComm.CommandText = listQuery;
            listComm.Connection.Open();
            listReader = listComm.ExecuteReader();
            if (listReader.HasRows)
            {
                while (listReader.Read())
                {
                    if (listReader["ModelUD"] != DBNull.Value && listReader["ImageFileName"] != DBNull.Value)
                    {
                        //Console.WriteLine("Processing file (" + listReader["ModelUD"].ToString() + "): " + listReader["FullFileName"].ToString());
                        try
                        {
                            tmpFolder = storageFolder + @"\" + listReader["ModelUD"] + @"\";
                            if (listReader["IsFreeScanImage"] != DBNull.Value && (bool)listReader["IsFreeScanImage"])
                            {
                                tmpFolder += "FreeScan";
                            }
                            if (listReader["IsGardenImage"] != DBNull.Value && (bool)listReader["IsGardenImage"])
                            {
                                tmpFolder += "Garden";
                            }
                            if (listReader["IsOldImage"] != DBNull.Value && (bool)listReader["IsOldImage"])
                            {
                                tmpFolder += "Old";
                            }

                            if (!System.IO.Directory.Exists(tmpFolder))
                            {
                                System.IO.Directory.CreateDirectory(tmpFolder);
                            }

                            //Console.WriteLine("---------- Retrieve file content, file name: " + listReader["FullFileName"].ToString());
                            detailQuery = "SELECT FileContent FROM Files WHERE FileName = '" + listReader["ImageFileName"].ToString() + "'";
                            SqlCommand detailComm = new SqlCommand();
                            detailComm.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLSourceConnection"].ConnectionString);
                            detailComm.CommandType = System.Data.CommandType.Text;
                            detailComm.CommandText = detailQuery;
                            detailComm.Connection.Open();
                            detailReader = detailComm.ExecuteReader();
                            if (detailReader.HasRows)
                            {
                                detailReader.Read();
                                tmpFile = tmpFolder + @"\" + System.Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                                //Console.WriteLine("---------- Saving file, file name: " + tmpFile);
                                (new MTSOFT.Utils.BinaryUtils()).Binary2PhysicalFile((byte[])detailReader["FileContent"], tmpFile);
                            }
                            detailComm.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("---------- Error: " + ex.Message);
                        }                       
                    }
                }
            }
            listComm.Connection.Close();
        }

        public static void UpdateOMSOnlineProductImage(string storageFolder)
        {
            string checkQuery;
            SqlCommand checkComm;
            SqlDataReader checkReader;

            string destQuery;

            SqlCommand destComm = new SqlCommand();
            destComm.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSQLDestinationConnection"].ConnectionString);
            destComm.CommandType = System.Data.CommandType.Text;

            ImageStorage img = new ImageStorage(@"D:\FinalOutput");
            string thumbPointer;
            string filePointer;
            string fileKey;
            string fullpath;
            string model;
            int modelID;

            // create directory            
            string _FileFolder = @"D:\FinalOutput\WebImages\file\";
            if (!Directory.Exists(_FileFolder))
            {
                Directory.CreateDirectory(_FileFolder);
            }

            foreach (string folder in System.IO.Directory.GetDirectories(storageFolder + @"\"))
            {
                try
                {
                    model = folder.Substring(folder.Length - 4, 4);
                    modelID = 0;

                    // check if model is exist
                    checkComm = new SqlCommand();
                    checkComm.Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["LocalSQLDestinationConnection"].ConnectionString);
                    checkComm.CommandType = System.Data.CommandType.Text;
                    checkQuery = "SELECT ModelID, ModelUD FROM Model WHERE ModelUD = '" + model.ToUpper() + "'";
                    checkComm.CommandText = checkQuery;
                    checkComm.Connection.Open();
                    checkReader = checkComm.ExecuteReader();
                    if (checkReader.HasRows)
                    {
                        checkReader.Read();
                        modelID = (Int32)checkReader["ModelID"];

                        // insert freescan
                        if (System.IO.Directory.Exists(folder + @"\Freescan"))
                        {
                            foreach (string file in System.IO.Directory.GetFiles(folder + @"\Freescan"))
                            {
                                try
                                {
                                    if (!file.EndsWith(".db"))
                                    {
                                        fileKey = System.Guid.NewGuid().ToString().Replace("-", "");
                                        FileInfo fInfo = new FileInfo(file);

                                        img.StoreFile(_FileFolder, file, out filePointer, out fullpath);
                                        img.StoreThumbnail(file, out thumbPointer, out fullpath);

                                        destQuery = "INSERT INTO Files(FileUD, FriendlyName, FileLocation, FileExtension, ThumbnailLocation, FileSize) VALUES('" + fileKey + "', '" + fInfo.Name + "', '" + filePointer + "', '" + fInfo.Extension + "', '" + thumbPointer + "', " + fInfo.Length.ToString() + ")";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();

                                        destQuery = "INSERT INTO ImageGallery(ModelID, FileUD, GalleryItemTypeID, UpdatedBy, UpdatedDate) VALUES(" + modelID.ToString() + ", '" + fileKey + "', 1, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "'); INSERT INTO ImageGalleryVersion(ImageGalleryID, Version, FileUD, IsDefault, UpdatedBy, UpdatedDate) VALUES(SCOPE_IDENTITY(), 1,'" + fileKey + "', 1, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "')";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();
                                    }
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }                                
                            }
                        }

                        // insert garden
                        if (System.IO.Directory.Exists(folder + @"\Garden"))
                        {
                            foreach (string file in System.IO.Directory.GetFiles(folder + @"\Garden"))
                            {
                                try
                                {
                                    if (!file.EndsWith(".db"))
                                    {
                                        fileKey = System.Guid.NewGuid().ToString().Replace("-", "");
                                        FileInfo fInfo = new FileInfo(file);

                                        img.StoreFile(_FileFolder, file, out filePointer, out fullpath);
                                        img.StoreThumbnail(file, out thumbPointer, out fullpath);

                                        destQuery = "INSERT INTO Files(FileUD, FriendlyName, FileLocation, FileExtension, ThumbnailLocation, FileSize) VALUES('" + fileKey + "', '" + fInfo.Name + "', '" + filePointer + "', '" + fInfo.Extension + "', '" + thumbPointer + "', " + fInfo.Length.ToString() + ")";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();

                                        destQuery = "INSERT INTO ImageGallery(ModelID, FileUD, GalleryItemTypeID, UpdatedBy, UpdatedDate) VALUES(" + modelID.ToString() + ", '" + fileKey + "', 2, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "'); INSERT INTO ImageGalleryVersion(ImageGalleryID, Version, FileUD, IsDefault, UpdatedBy, UpdatedDate) VALUES(SCOPE_IDENTITY(), 1,'" + fileKey + "', 1, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "')";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();
                                    } 
                                }
                                catch(Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }                                                                 
                            }
                        }

                        // insert old
                        if (System.IO.Directory.Exists(folder + @"\Old"))
                        {
                            foreach (string file in System.IO.Directory.GetFiles(folder + @"\Old"))
                            {
                                try
                                {
                                    if (!file.EndsWith(".db"))
                                    {
                                        fileKey = System.Guid.NewGuid().ToString().Replace("-", "");
                                        FileInfo fInfo = new FileInfo(file);

                                        img.StoreFile(_FileFolder, file, out filePointer, out fullpath);
                                        img.StoreThumbnail(file, out thumbPointer, out fullpath);

                                        destQuery = "INSERT INTO Files(FileUD, FriendlyName, FileLocation, FileExtension, ThumbnailLocation, FileSize) VALUES('" + fileKey + "', '" + fInfo.Name + "', '" + filePointer + "', '" + fInfo.Extension + "', '" + thumbPointer + "', " + fInfo.Length.ToString() + ")";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();

                                        destQuery = "INSERT INTO ImageGallery(ModelID, FileUD, GalleryItemTypeID, UpdatedBy, UpdatedDate) VALUES(" + modelID.ToString() + ", '" + fileKey + "', 7, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "'); INSERT INTO ImageGalleryVersion(ImageGalleryID, Version, FileUD, IsDefault, UpdatedBy, UpdatedDate) VALUES(SCOPE_IDENTITY(), 1,'" + fileKey + "', 1, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "')";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();
                                    }  
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }                                                               
                            }
                        }

                        // insert video
                        if (System.IO.Directory.Exists(folder + @"\360"))
                        {
                            foreach (string file in System.IO.Directory.GetFiles(folder + @"\360"))
                            {
                                try
                                {
                                    if (!file.EndsWith(".db"))
                                    {
                                        fileKey = System.Guid.NewGuid().ToString().Replace("-", "");
                                        FileInfo fInfo = new FileInfo(file);

                                        img.StoreFile(_FileFolder, file, out filePointer, out fullpath);

                                        destQuery = "INSERT INTO Files(FileUD, FriendlyName, FileLocation, FileExtension, FileSize) VALUES('" + fileKey + "', '" + fInfo.Name + "', '" + filePointer + "', '" + fInfo.Extension + "', " + fInfo.Length.ToString() + ")";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();

                                        destQuery = "INSERT INTO ImageGallery(ModelID, FileUD, GalleryItemTypeID, UpdatedBy, UpdatedDate) VALUES(" + modelID.ToString() + ", '" + fileKey + "', 3, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "'); INSERT INTO ImageGalleryVersion(ImageGalleryID, Version, FileUD, IsDefault, UpdatedBy, UpdatedDate) VALUES(SCOPE_IDENTITY(), 1,'" + fileKey + "', 1, 1, '" + (new DateTime(2015, 1, 1)).ToString("yyyy-MM-dd") + "')";
                                        destComm.CommandText = destQuery;
                                        destComm.Connection.Open();
                                        destComm.ExecuteScalar();
                                        destComm.Connection.Close();
                                    } 
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }                                                               
                            }
                        }
                    }
                    checkComm.Connection.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + Environment.NewLine + ex.StackTrace);
                }

                //fileKey = System.Guid.NewGuid().ToString().Replace("-", "");
                //FileInfo fInfo = new FileInfo(file);

                //img.StoreFile(_FileFolder, file, out filePointer, out fullpath);
                //img.StoreThumbnail(file, out thumbPointer, out fullpath);

                //destQuery = "INSERT INTO Files(FileUD, FriendlyName, FileLocation, FileExtension, ThumbnailLocation, FileSize) VALUES('" + fileKey + "', '" + fInfo.Name + "', '" + filePointer + "', '" + fInfo.Extension + "', '" + thumbPointer + "', " + fInfo.Length.ToString() + ")";
                //destComm.CommandText = destQuery;
                //destComm.Connection.Open();
                //destComm.ExecuteScalar();
                //destComm.Connection.Close();

                //destQuery = "UPDATE Client SET LogoImage = '" + fileKey + "' WHERE ClientID = " + Convert.ToInt32(fInfo.Name.Replace(fInfo.Extension, ""));
                //destComm.CommandText = destQuery;
                //destComm.Connection.Open();
                //destComm.ExecuteScalar();
                //destComm.Connection.Close();
            }
        }
    }
}
