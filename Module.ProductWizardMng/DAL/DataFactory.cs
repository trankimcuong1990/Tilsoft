using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Library;
using Module.ProductWizardMng.DTO;

namespace Module.ProductWizardMng.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {
        //private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        //Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();
        private ProductWizardMngEntities CreateContext()
        {
            return new ProductWizardMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductWizardMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(int userId, System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override DTO.EditFormData GetData(int userId, int id, Hashtable param, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteData(int userID, int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public override object GetInitData(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
