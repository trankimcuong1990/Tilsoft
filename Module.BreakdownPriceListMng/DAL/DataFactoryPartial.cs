using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.BreakdownPriceListMng.DAL
{
    internal partial class DataFactory
    {
        public bool UpdatePrice(int userId, object priceListTable, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message="Update Price Success" };
            List<DTO.BreakdownPriceListSearchDTO> dtoPriceList = ((Newtonsoft.Json.Linq.JArray)priceListTable).ToObject<List<DTO.BreakdownPriceListSearchDTO>>();
            try
            {
                using (BreakdownPriceListMngEntities context = CreateContext())
                {
                    int? companyID = fw_factory.GetCompanyID(userId);
                    List<int?> productionItemIDs = dtoPriceList.Select(o => o.ProductionItemID).ToList();
                    var dbPrice = context.BreakdownPriceList.Where(o => productionItemIDs.Contains(o.ProductionItemID) && o.CompanyID == companyID).ToList();
                    foreach (var item in dbPrice)
                    {
                        var dtoPriceItem = dtoPriceList.Where(o => o.ProductionItemID == item.ProductionItemID).FirstOrDefault();
                        if (dtoPriceItem != null)
                        {
                            if (companyID == 1) //AuvietFur Price
                            {
                                item.UnitPrice = dtoPriceItem.AVFPrice;
                            }
                            else if (companyID == 3) //AnVietThinh Price
                            { 
                                item.UnitPrice = dtoPriceItem.AVTPrice;
                            }
                            item.UpdatedBy = userId;
                            item.UpdatedDate = DateTime.Now;
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

        public bool AddProductionItemPrice(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Add product item price success" };            
            try
            {
                using (BreakdownPriceListMngEntities context = CreateContext())
                {
                    context.BreakdownPriceListMng_function_AddProductionItemPrice();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return false;
            }
            return true;
        }

    }
}
