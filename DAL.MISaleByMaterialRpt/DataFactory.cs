using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MISaleByMaterialRpt
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new DAL.Support.DataFactory();

        private MISaleByMaterialRptEntities CreateContext()
        {
            return new MISaleByMaterialRptEntities(DALBase.Helper.CreateEntityConnectionString("MISaleByMaterialRptModel"));
        }

        public DTO.MISaleByMaterialRpt.ReportData GetReportData(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.MISaleByMaterialRpt.ReportData data = new DTO.MISaleByMaterialRpt.ReportData();
            data.AllMaterials = new List<DTO.MISaleByMaterialRpt.AllMaterial>();
            data.Woods = new List<DTO.MISaleByMaterialRpt.Wood>();
            data.EURContainerValue = 0;
            data.USDContainerValue = 0;
            data.ExchangeRate = 0;

            string preSeason = Library.Helper.GetPreviousSeason(season);
            //try to get data
            try
            {
                using (MISaleByMaterialRptEntities context = CreateContext())
                {
                    data.AllMaterials = converter.DB2DTO_AllMaterialList(context.MISaleByMaterialRpt_AllMaterial_View.Where(o => o.Season == preSeason).OrderByDescending(o=>o.TotalAmount).ToList());
                    data.ProformaInvoiceAllMaterials = AutoMapper.Mapper.Map<List<MISaleByMaterialRpt_AllMaterial_ProformaInvoice_View>, List<DTO.MISaleByMaterialRpt.ProformaInvoiceAllMaterial>>(context.MISaleByMaterialRpt_AllMaterial_ProformaInvoice_View.Where(o=>o.Season==season).ToList());

                    //if (data.AllMaterials.FirstOrDefault(o => o.MaterialNM == "NONE") != null)
                    //    data.AllMaterials.FirstOrDefault(o => o.MaterialNM == "NONE").MaterialNM = "NONE (SPAREPART)";

                    data.Woods = converter.DB2DTO_WoodList(context.MISaleByMaterialRpt_Wood_View.Where(o => o.Season == preSeason).OrderByDescending(o => o.TotalAmount).ToList());
                    data.ProformaInvoiceWoods = AutoMapper.Mapper.Map<List<MISaleByMaterialRpt_Wood_ProformaInvoice_View>, List<DTO.MISaleByMaterialRpt.ProformaInvoiceWood>>(context.MISaleByMaterialRpt_Wood_ProformaInvoice_View.Where(o =>o.Season==season).ToList());

                    data.ExchangeRate = Convert.ToDecimal(supportFactory.GetSettingValue(season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));
                    data.USDContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstUSDContValue"));
                    data.EURContainerValue = Convert.ToDecimal(supportFactory.GetSettingValue(season, "EstEURContValue"));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
