using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Support
    {
        private DAL.Support.DataFactory factory = new DAL.Support.DataFactory();

        public List<DTO.Support.Model> GetModels()
        {
            return factory.GetModel().ToList();
        }

        public List<DTO.Support.Client> GetClients()
        {
            return factory.GetClient().Where(o => !string.IsNullOrEmpty(o.ClientUD) && !string.IsNullOrEmpty(o.ClientNM)).ToList();
        }

        public IEnumerable<DTO.Support.Model> QuickSearchModel(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            // query data
            return factory.QuickSearchModel(filters,pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.WareHouse> QuickSearchWareHouse(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                return factory.GetWareHouse();
            }
            catch(Exception ex )
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return new List<DTO.Support.WareHouse>();
            }
        }

        public IEnumerable<DTO.Support.ConfirmedProduct> QuickSearchConfirmedProduct(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification) 
        {
            return factory.QuickSearchConfirmedProduct(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.Sparepart> QuickSearchSparepart(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSparepart(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.FrameMaterialOption> QuickSearchFrameMaterialOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchFrameMaterialOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.MaterialOption> QuickSearchMaterialOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchMaterialOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.SubMaterialOption> QuickSearchSubMaterialOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSubMaterialOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.CushionOption> QuickSearchCushionOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchCushionOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.CushionColorOption> QuickSearchCushionColorOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchCushionColorOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.Model> QuickSearchPieceModel(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchPieceModel(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);        
        }
        
        public IEnumerable<DTO.Support.Client> QuickSearchClient(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchClient(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.Booking> QuickSearchBooking(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchBooking(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.ModelSeason> QuickSearchModelSeason(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchModelSeason(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.ModelSeason> QuickSearchArticleCode(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchArticleCode(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public DTO.Controls.ProductWizardData GetProductWizardData(int modelID, int? clientID, string season, out Library.DTO.Notification notification)
        {
            return factory.GetProductWizardData(modelID, clientID, season, out notification);
        }

        public IEnumerable<DTO.Support.BackCushionOption> QuickSearchBackCushionOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchBackCushionOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        
        public IEnumerable<DTO.Support.SeatCushionOption> QuickSearchSeatCushionOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSeatCushionOption(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }

        public IEnumerable<DTO.Support.Season> GetSeason()
        {
            return factory.GetSeason();
        }
        
        public IEnumerable<DTO.Support.ShowroomReceiptType> GetShowroomReceiptType()
        {
            return factory.GetShowroomReceiptType();
        }

        public IEnumerable<DTO.Support.ShowroomItem> QuickSearchShowroomItem(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchShowroomItem(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public IEnumerable<DTO.Support.WarehouseItem> QuickSearchWarehouseItem(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchWarehouseItem(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
        public IEnumerable<DTO.Support.ShowroomArea> QuickSearchShowroomArea(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchShowroomArea(filters,out notification);
        }

        public IEnumerable<DTO.Support.ShowroomAreaByPhysicalQnt> QuickSearchShowroomAreaByPhysicalQnt(Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchShowroomAreaByPhysicalQnt(filters, out notification);
        }


        public IEnumerable<DTO.Support.Forwarder> QuickSearchForwarder(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchForwarder(filters, pageSize, pageIndex, orderBy, orderDirection, out totalRows, out notification);
        }
    }
}
