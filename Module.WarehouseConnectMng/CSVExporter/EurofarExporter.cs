using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.WarehouseConnectMng.DTO;
using System.IO;

namespace Module.WarehouseConnectMng.CSVExporter
{
    public class EurofarExporter : IExporter
    {
        public byte[] ExportCSV(string path, List<ProductDTO> data)
        {
            string uniqueFileName = FrameworkSetting.Setting.AbsoluteReportTmpFolder + System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsx";
            System.IO.FileInfo fInfo = new System.IO.FileInfo(uniqueFileName);
            OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
            OfficeOpenXml.ExcelWorksheet pWS = ePackagae.Workbook.Worksheets.Add("Product");
            OfficeOpenXml.ExcelWorksheet iWS = ePackagae.Workbook.Worksheets.Add("Image");

            List<DTO.ProductMediaDTO> dtoImages = new List<ProductMediaDTO>();
            foreach (DTO.ProductDTO dtoItem in data)
            {
                if (dtoItem.ProductMediaDTOs.Count > 0)
                {
                    dtoImages.AddRange(dtoItem.ProductMediaDTOs);
                }
            }

            // add data to product sheet
            var products = from o in data
                           select new
                           {
                               ModelID = o.ModelID
                               , ArticleCode = o.ArticleCode
                               , Name = o.ModelNM + " (" + o.MaterialColorNM + ")"
                               , Description = o.Description
                               , ProductTypeNM = o.ProductTypeNM
                               , EANCode = o.EANCode
                               , ProductGroupNM = o.ProductGroupNM
                               , ProductCategoryNM = o.ProductCategoryNM
                               , NetWeight = o.NetWeight
                               , OverallDimL = o.OverallDimL
                               , OverallDimW = o.OverallDimW
                               , OverallDimH = o.OverallDimH
                               , SeatDimL = o.SeatDimL
                               , SeatDimW = o.SeatDimW
                               , SeatDimH = o.SeatDimH
                               , ArmDimL = o.ArmDimL
                               , ArmDimW = o.ArmDimW
                               , ArmDimH = o.ArmDimH
                               , OtherDimL = o.OtherDimL
                               , OtherDimW = o.OtherDimW
                               , OtherDimH = o.OtherDimH
                               , PackagingMethodNM = o.PackagingMethodNM
                               , CartonBoxDimL = o.CartonBoxDimL
                               , CartonBoxDimW = o.CartonBoxDimW
                               , CartonBoxDimH = o.CartonBoxDimH
                               , Qnt20DC = o.Qnt20DC
                               , Qnt40DC = o.Qnt40DC
                               , Qnt40HC = o.Qnt40HC
                               , CBM = o.CBM
                               , QntInBox = o.QntInBox
                               , GrossWeight = o.GrossWeight
                               , BackCushionStuffing = o.BackCushionStuffing
                               , BackCushionParts = o.BackCushionParts
                               , BackCushionWeight = o.BackCushionWeight
                               , BackCushionDimL = o.BackCushionDimL
                               , BackCushionDimW = o.BackCushionDimW
                               , BackCushionDimH = o.BackCushionDimH
                               , SeatCushionStuffing = o.SeatCushionStuffing
                               , SeatCushionParts = o.SeatCushionParts
                               , SeatCushionWeight = o.SeatCushionWeight
                               , SeatCushionDimL = o.SeatCushionDimL
                               , SeatCushionDimW = o.SeatCushionDimW
                               , SeatCushionDimH = o.SeatCushionDimH
                               , MaterialNM = o.MaterialNM
                               , MaterialTypeNM = o.MaterialTypeNM
                               , MaterialColorNM = o.MaterialColorNM
                               , FrameMaterialNM = o.FrameMaterialNM
                               , FrameMaterialColorNM = o.FrameMaterialColorNM
                               , SubMaterialNM = o.SubMaterialNM
                               , SubMaterialColorNM = o.SubMaterialColorNM
                               , CushionNM = o.CushionNM
                               , CushionColorNM = o.CushionColorNM
                               , BackCushionNM = o.BackCushionNM
                               , SeatCushionNM = o.SeatCushionNM
                               , FSCTypeNM = o.FSCTypeNM
                               , FSCPercentNM = o.FSCPercentNM
                               , StockQnt = o.StockQnt
                               , StockQnt40HC = (o.Qnt40HC.HasValue && o.Qnt40HC.Value > 0 ? (decimal?)(Math.Round((decimal)((decimal)o.StockQnt / o.Qnt40HC.Value), 2, MidpointRounding.AwayFromZero)) : null) 
                               , UnitPrice = o.UnitPrice
                           };            
            pWS.Cells["A1"].LoadFromCollection(products.ToList(), true);

            // add data to images sheet
            var images = from o in dtoImages
                         select new { ModelID = o.ModelID, URL = o.FileLocation };
            iWS.Cells["A1"].LoadFromCollection(images.ToList(), true);

            ePackagae.Save();

            return File.ReadAllBytes(uniqueFileName);
        }

        public string FileExtension()
        {
            return ".xlsx";
        }
    }
}
