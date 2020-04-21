using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data.Entity;
using Newtonsoft.Json.Linq;

namespace Module.WarehouseConnectMng.DAL
{
    internal class DataFactory
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private WarehouseConnectMngEntities CreateContext()
        {
            return new WarehouseConnectMngEntities(Library.Helper.CreateEntityConnectionString("DAL.WarehouseConnectMngModel"));
        }

        public List<DTO.ProductDTO> GetFreeToSale(string wexJsonData, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProductDTO> data = new List<DTO.ProductDTO>();

            try
            {
                using (WarehouseConnectMngEntities context = CreateContext())
                {
                    List<DTO.WEX.Article> jsonData = ParseWEXJsonData(wexJsonData);
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM WexItem WITH (TABLOCKX, HOLDLOCK)");
                        //if (jsonData.Count() > 0)
                        //{
                        //    context.Database.ExecuteSqlCommand("DELETE FROM WexItem");
                        //    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('WexItem', RESEED, 1)");
                        //}

                        try
                        {
                            //foreach (DTO.WEX.Article wexItem in jsonData)
                            //{
                            //    if (wexItem.out_available_stock > 0)
                            //    {
                            //        context.WexItem.Add(new WexItem()
                            //        {
                            //            ArticleCode = wexItem.out_item_no
                            //            , SubEANCode = wexItem.out_barcode_base
                            //            , StockQnt = Convert.ToInt32(wexItem.out_available_stock)
                            //        });
                            //    }
                            //}
                            //context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }

                        data = converter.DB2DTO_ProductList(context.WarehouseConnectMng_Product_View.ToList());
                        //List<int> ModelIDs = context.WarehouseConnectMng_Product_View.Select(o => o.ModelID.Value).ToList();
                        //List<WarehouseConnectMng_Gallery_View> dbGalleries = context.WarehouseConnectMng_Gallery_View.Where(g => ModelIDs.Contains(g.ModelID.Value)).ToList();
                        //foreach (DTO.ProductDTO dtoProduct in data)
                        //{
                        //    dtoProduct.ProductMediaDTOs = new List<DTO.ProductMediaDTO>();
                        //    foreach (WarehouseConnectMng_Gallery_View dbGallery in dbGalleries.Where(o => o.ModelID.Value == dtoProduct.ModelID))
                        //    {
                        //        dtoProduct.ProductMediaDTOs.Add(converter.DB2DTO_Media(dbGallery));
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            data.ForEach(o => o.ModelID = 0);
            return data;
        }

        public byte[] GetFreeToSaleCsv(string wexJsonData, string exporterIdentifier, out string fileExtension, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ProductDTO> data = new List<DTO.ProductDTO>();

            try
            {
                using (WarehouseConnectMngEntities context = CreateContext())
                {
                    List<DTO.WEX.Article> jsonData = ParseWEXJsonData(wexJsonData);
                    using (DbContextTransaction scope = context.Database.BeginTransaction())
                    {
                        context.Database.ExecuteSqlCommand("SELECT * FROM WexItem WITH (TABLOCKX, HOLDLOCK)");
                        //if (jsonData.Count() > 0)
                        //{
                        //    context.Database.ExecuteSqlCommand("DELETE FROM WexItem");
                        //    context.Database.ExecuteSqlCommand("DBCC CHECKIDENT('WexItem', RESEED, 1)");
                        //}

                        try
                        {
                            //foreach (DTO.WEX.Article wexItem in jsonData)
                            //{
                            //    if (wexItem.out_available_stock > 0)
                            //    {
                            //        context.WexItem.Add(new WexItem()
                            //        {
                            //            ArticleCode = wexItem.out_item_no
                            //            , SubEANCode = wexItem.out_barcode_base
                            //            , StockQnt = Convert.ToInt32(wexItem.out_available_stock)
                            //        });
                            //    }                               
                            //}
                            //context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        finally
                        {
                            scope.Commit();
                        }

                        data = converter.DB2DTO_ProductList(context.WarehouseConnectMng_Product_View.ToList());
                        List<int> ModelIDs = context.WarehouseConnectMng_Product_View.Select(o => o.ModelID.Value).ToList();
                        List<WarehouseConnectMng_Gallery_View> dbGalleries = context.WarehouseConnectMng_Gallery_View.Where(g => ModelIDs.Contains(g.ModelID.Value)).ToList();
                        foreach (DTO.ProductDTO dtoProduct in data)
                        {
                            dtoProduct.ProductMediaDTOs = new List<DTO.ProductMediaDTO>();
                            foreach (WarehouseConnectMng_Gallery_View dbGallery in dbGalleries.Where(o => o.ModelID.Value == dtoProduct.ModelID))
                            {
                                dtoProduct.ProductMediaDTOs.Add(converter.DB2DTO_Media(dbGallery));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            // create concrete instance from type descriptor
            CSVExporter.IExporter exporter = (CSVExporter.IExporter)Activator.CreateInstance(Type.GetType("Module.WarehouseConnectMng.CSVExporter." + exporterIdentifier + ", Module.WarehouseConnectMng"));
            fileExtension = exporter.FileExtension();
            return exporter.ExportCSV(FrameworkSetting.Setting.AbsoluteReportFolder, data);
        }


        //
        // PRIVATE
        //
        public List<DTO.WEX.Article> ParseWEXJsonData(string jsonData)
        {
            List<DTO.WEX.Article> data = new List<DTO.WEX.Article>();
            if (!string.IsNullOrEmpty(jsonData))
            {
                data = JArray.Parse(jsonData)[0].ToObject<List<DTO.WEX.Article>>();
            }
            return data;
        }
    }
}
