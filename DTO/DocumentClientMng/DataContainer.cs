using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DocumentClientMng
{
    public class DataContainer
    {
        public DocumentClient DocumentClientData { get; set; }

        public List<TypeOfDelivery> TypeOfDeliverys { get; set; }

        public List<PlaceOfBarge> PlaceOfBarges { get; set; }

        public List<PlaceOfDelivery> PlaceOfDeliverys { get; set; }

        public List<DeliveryStatus> DeliveryStatuss { get; set; }

        public List<PaymentStatus> PaymentStatuss { get; set; }

        public List<DTO.Support.Season> Seasons { get; set; }

        public List<DTO.Support.ReportTemplate> ReportTemplates { get; set; }
    }
}
