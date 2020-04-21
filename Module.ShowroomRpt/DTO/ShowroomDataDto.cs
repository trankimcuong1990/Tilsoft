using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ShowroomRpt.DTO
{
    public class ShowroomDataDto
    {
        public int SampleProductID { get; set; }
        public string SampleProductUD { get; set; }
        public string FinishedImageThumbnailLocation { get; set; }
        public string FinishedImageFileLocation { get; set; }
        public string ClientUD { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }
        public string FactoryUD { get; set; }

        public string MaterialNM { get; set; }
        public string MaterialTypeNM { get; set; }
        public string MaterialColorNM { get; set; }

        public string Material2NM { get; set; }
        public string MaterialType2NM { get; set; }
        public string MaterialColor2NM { get; set; }

        public string Material3NM { get; set; }
        public string MaterialType3NM { get; set; }
        public string MaterialColor3NM { get; set; }

        public string BackCushionSpecification { get; set; }
        public string SeatCushionDescription { get; set; }
        public string SeatCushionSpecification { get; set; }
        public string CushionColorDescription { get; set; }

        public int? CompanyID { get; set; }
        public int? FactoryWarehouseID { get; set; }
        public string FactoryWarehouseNM { get; set; }

        public int? SampleOrderID { get; set; }
        public int PrimaryID { get; set; }
        public int? ProductID { get; set; }

        public int ProductionItemID { get; set; }
        public string ProductionItemUD { get; set; }
        public string ProductionItemNM { get; set; }

        public decimal? WarehouseQuantity { get; set; }

        public string SeatCushionNM { get; set; }
        public string BackCushionNM { get; set; }
        public string CushionColorNM { get; set; }

        public string FileLocation { get; set; }
        public string ThumbnailLocation { get; set; }

        public string FactoryWarehousePalletUD { get; set; }
        public string UnitNM { get; set; }
        public string Season { get; set; }
    }
}
