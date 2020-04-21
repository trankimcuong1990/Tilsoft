using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTask.UpdateFactoryPriceHistory
{
    public class MainTask : NotificationBase.ITask
    {
        public void ExecuteTask()
        {
            try
            {
                using (FactoryPriceHistoryMngEntities context = new FactoryPriceHistoryMngEntities(Library.Helper.CreateEntityConnectionString("FactoryPriceHistoryMngModel")))
                {
                    string CurrentSeason = Library.OMSHelper.Helper.GetCurrentSeason();
                    string PrevSeson = Library.OMSHelper.Helper.GetPrevSeason(CurrentSeason);
                    string PrevOfPrevSeason = Library.OMSHelper.Helper.GetPrevSeason(PrevSeson);
                    string PrevOfPrevOfPrevSeason = Library.OMSHelper.Helper.GetPrevSeason(PrevOfPrevSeason);
                    List<string> PrevSeasons = new List<string> { PrevSeson, PrevOfPrevSeason, PrevOfPrevOfPrevSeason };
                    var itemToMatch = context.EurofarPurchasingPriceMng_PendingItem_View.Where(o => o.Season == CurrentSeason).Take(200).ToList();
                    var sourcesToMatch = context.EurofarPurchasingPriceMng_ConfirmedItem_View.Where(o => PrevSeasons.Contains(o.Season)).ToList();

                    foreach (var item in itemToMatch)
                    {
                        var selectedItems = sourcesToMatch.Where(
                            o => o.ModelID == item.ModelID
                            && o.MaterialID == item.MaterialID
                            && o.MaterialTypeID == item.MaterialTypeID
                            && o.SubMaterialID == item.SubMaterialID
                            && o.SubMaterialColorID == item.SubMaterialColorID
                            && o.FrameMaterialID == item.FrameMaterialID
                            && o.FSCTypeID == item.FSCTypeID
                            && o.FSCPercentID == item.FSCPercentID
                            && o.BackCushionID == item.BackCushionID
                            && o.SeatCushionID == item.SeatCushionID);

                        // frame material & color processing
                        bool isFrameMustMatch = false;
                        if (item.FrameMaterialID == 1 && item.FrameMaterialColorID == 10) // alu bambo look
                        {
                            isFrameMustMatch = true;
                        }
                        if (item.FrameMaterialID == 2 && item.FrameMaterialColorID == 7) // steel galvanized
                        {
                            isFrameMustMatch = true;
                        }
                        if (isFrameMustMatch)
                        {
                            selectedItems = selectedItems.Where(o => o.FrameMaterialColorID == item.FrameMaterialColorID);
                        }

                        // cushion color processing
                        if (!item.CushionPriceGroupID.HasValue)
                        {
                            selectedItems = selectedItems.Where(o => o.CushionColorID == item.CushionColorID);
                        }
                        else
                        {
                            selectedItems = selectedItems.Where(o => o.CushionColorID == item.CushionColorID || o.CushionPriceGroupID == item.CushionPriceGroupID);
                        }

                        if (selectedItems.Count() > 0)
                        {
                            foreach (var selectedItem in selectedItems)
                            {
                                FactoryPriceHistory dbHistory = new FactoryPriceHistory();
                                context.FactoryPriceHistory.Add(dbHistory);
                                dbHistory.QuotationDetailID = item.QuotationDetailID;
                                dbHistory.RelatedQuotationDetailID = selectedItem.QuotationDetailID;
                            }
                        }
                        else
                        {
                            FactoryPriceHistory dbHistory = new FactoryPriceHistory();
                            context.FactoryPriceHistory.Add(dbHistory);
                            dbHistory.QuotationDetailID = item.QuotationDetailID;
                            dbHistory.RelatedQuotationDetailID = null;
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch { }
        }

        public string GetDescription()
        {
            return "Updating factory price history";
        }
    }
}
