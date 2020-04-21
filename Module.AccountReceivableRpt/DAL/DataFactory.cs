using Library.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountReceivableRpt.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchFormData, DTO.EditFormData>
    {      
        private DataConverter converter = new DataConverter();
        private AccountReceivableRptEntities CreateContext()
        {
            return new AccountReceivableRptEntities(Library.Helper.CreateEntityConnectionString("DAL.AccountReceivableRptModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.SearchFormData GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitForm data = new DTO.InitForm();
            data.keyRawMaterials = new List<DTO.KeyRawMaterial>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.keyRawMaterials = AutoMapper.Mapper.Map<List<AccountReceivableRpt_KeyRawMaterial_View>, List<DTO.KeyRawMaterial>>(context.AccountReceivableRpt_KeyRawMaterial_View.ToList());
                    data.dueColorDTOs = AutoMapper.Mapper.Map<List<AccountReceivableRpt_DueColor_View>, List<DTO.DueColorDTO>>(context.AccountReceivableRpt_DueColor_View.ToList());
                    data.SupplierDTO = AutoMapper.Mapper.Map<List<AccountReceivableRpt_FactoryRawMaterial_View>, List<DTO.SupplierDTO>>(context.AccountReceivableRpt_FactoryRawMaterial_View.ToList());
                }
                data.CurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override object GetSearchFilter(int userId, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
