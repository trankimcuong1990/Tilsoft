using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.SpecificationOfProductMng
{
    public class BLL
    {
        DAL.DataFactory factory = new DAL.DataFactory();

        public DTO.InitDataDTO GetInitData(int userId, out Library.DTO.Notification notification)
        {
            return factory.GetInitData(userId, out notification);
        }

        public DTO.EditDataDTO GetDataSpecification(int userId, int id, int? sampleProductID, int? productID, out Library.DTO.Notification notification)
        {
            return factory.GetDataSpecification(userId, id, sampleProductID, productID, out notification);
        }

        public bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            return factory.UpdateData(userId, id, ref dtoItem, out notification);
        }

        public bool Delete(int id, out Library.DTO.Notification notification)
        {
            return factory.DeleteData(id, out notification);
        }

        public string GetExportData(int userId, int ProductSpecificationID, out Library.DTO.Notification notification)
        {
            return factory.GetExportData(ProductSpecificationID, out notification);
        }

        public DTO.InitDataDTO QuickSearchSample(int userId, int? factoryID, Hashtable filters, out Library.DTO.Notification notification)
        {
            return factory.QuickSearchSample(userId, factoryID, filters, out notification);
        }

        public object GetdataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderedBy, string orderedDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            return factory.GetDataWithFilter(filters, pageSize, pageIndex, orderedBy, orderedDirection, out totalRows, out notification);
        }

        public object GetsListSpec (int userId, int? modelID, out Library.DTO.Notification notification)
        {
            return factory.GetsListSpec(userId, modelID, out notification);
        }

        public  object CoppySpecOfProduct(int userId, int? productSpecificationID, out Library.DTO.Notification notification)
        {
            return factory.CoppySpecOfProduct(userId, productSpecificationID, out notification);
        }
    }
}
