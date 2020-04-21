using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.ProductionIssue.DTO
{
    public class SearchForm
    {
        public List<ProductionIssueData> ProductionIssue { get; set; }
        public List<HistoryDeliveryNote> HistoryDeliveryNotes { get; set; }

        public SearchForm()
        {
            ProductionIssue = new List<ProductionIssueData>();
            HistoryDeliveryNotes = new List<HistoryDeliveryNote>();
        }
    }
}
