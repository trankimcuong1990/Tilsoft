using System;

namespace Module.PurchasingPriceComparisonMng.DAL
{
    internal class DataFactory
    {
        public object GetInitForm(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormData data = new DTO.InitFormData();

            try
            {
                data.CurrentSeason = Library.Helper.GetCurrentSeason();

                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Seasons = supportFactory.GetSeason();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }
    }
}
