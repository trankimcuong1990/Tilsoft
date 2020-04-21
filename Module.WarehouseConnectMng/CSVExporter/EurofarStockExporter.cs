using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module.WarehouseConnectMng.DTO;
using System.IO;

namespace Module.WarehouseConnectMng.CSVExporter
{
    public class EurofarStockExporter : IExporter
    {
        public byte[] ExportCSV(string path, List<ProductDTO> data)
        {
            string uniqueFileName = FrameworkSetting.Setting.AbsoluteReportTmpFolder + System.Guid.NewGuid().ToString().Replace("-", "") + ".xlsx";
            System.IO.FileInfo fInfo = new System.IO.FileInfo(uniqueFileName);
            OfficeOpenXml.ExcelPackage ePackagae = new OfficeOpenXml.ExcelPackage(fInfo);
            OfficeOpenXml.ExcelWorksheet pWS = ePackagae.Workbook.Worksheets.Add("Product");

            // add data to product sheet
            var products = from o in data
                            select new
                            {
                                sku = o.ArticleCode
                                , name = o.ModelNM + " (" + o.MaterialColorNM + ")"
                                , description = o.Description
                                , product_type = o.ProductTypeNM
                                , type = o.ProductTypeNM == "SET" ? "bundle" : "simple"
                                , weight = o.NetWeight
                                , price = o.UnitPrice
                                , article_code = o.ArticleCode
                                , sub_ean_code = o.SubEANCode
                                , material_fs = o.MaterialNM
                                , color_fs = o.MaterialColorNM
                                , material_type = o.MaterialTypeNM
                                , frame_material = o.FrameMaterialNM
                                , frame_material_color = o.FrameMaterialColorNM
                                , back_cushion = o.BackCushionNM
                                , seat_cushion = o.SeatCushionNM
                                , cushion_color = o.CushionColorNM
                                , height = o.OverallDimH + "cm"
                                , length = o.OverallDimL + "cm"
                                , width = o.OverallDimW + "cm"
                                , seat_height = o.SeatCushionDimH
                                , seat_length = o.SeatCushionDimL
                                , seat_width = o.SeatCushionDimW
                                , wxlxh_cm = o.OverallDimW + "cm x " + o.OverallDimL + "cm x " + o.OverallDimH + "cm"
                                , carton_box_length = o.CartonBoxDimL
                                , carton_box_height = o.CartonBoxDimH
                                , carton_box_width = o.CartonBoxDimW
                                , qty = o.StockQnt
                            };
            pWS.Cells["A1"].LoadFromCollection(products.ToList(), true);
            ePackagae.Save();

            List<Library.DTO.ZipFileDTO> toBeZippedFiles = new List<Library.DTO.ZipFileDTO>();
            toBeZippedFiles.Add(new Library.DTO.ZipFileDTO() { FileName = "product", FilePath = uniqueFileName});
            data.Where(o => !string.IsNullOrEmpty(o.FileLocation)).ToList().ForEach(o => toBeZippedFiles.Add(new Library.DTO.ZipFileDTO() { FileName = o.ArticleCode, FilePath = FrameworkSetting.Setting.AbsoluteFileFolder + o.FileLocation }));

            Library.FileHelper.ZipManager zMng = new Library.FileHelper.ZipManager(FrameworkSetting.Setting.AbsoluteReportTmpFolder);
            return File.ReadAllBytes(zMng.CreateZipFile(toBeZippedFiles));
        }

        public string FileExtension()
        {
            return ".zip";
        }
    }
}
