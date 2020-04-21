using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Data;
using Library.DTO;
using Module.AccountPayableRpt.DTO;

namespace Module.AccountPayableRpt.DAL
{
    internal partial class DataFactory : Library.Base.DataFactory2<DTO.SearchForm, DTO.EditForm>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        Module.Framework.DAL.DataFactory fw_factory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private AccountPayableRptEntities CreateContext()
        {
            return new AccountPayableRptEntities(Library.Helper.CreateEntityConnectionString("DAL.AccountPayableRptModel"));
        }
        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Process(int userID, Hashtable input, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            notification.Message = "Process success !";            
            object dtoItem = (object)input["query"];
            List<DTO.PurchaseInvoiceDTO> dtoPurchaseInvoice = ((Newtonsoft.Json.Linq.JArray)dtoItem).ToObject<List<DTO.PurchaseInvoiceDTO>>();
            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.PurchaseInvoice.ToList();
                    if (dbItem == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data!";
                        return false;
                    }
                    foreach(var dtoinvoice in dtoPurchaseInvoice)
                    {
                        if (dtoinvoice.isSelected == true)
                        {
                            foreach (var item in dbItem)
                            {
                                if(dtoinvoice.PurchaseInvoiceID==item.PurchaseInvoiceID)
                                {
                                    item.PurchaseInvoiceStatusID = 3;
                                    if(item.Remark != null && item.Remark != "")
                                    {
                                        item.Remark += System.Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy") + " " + context.Employee.Where(o => o.UserID == userID).Select(a => a.EmployeeNM).FirstOrDefault().ToString() + " duyet thanh toan " + String.Format("{0:n0}", dtoinvoice.PlanPayment) + " " + dtoinvoice.Currency;
                                    }
                                    else
                                    {
                                        item.Remark = DateTime.Now.ToString("dd/MM/yyyy") + " " + context.Employee.Where(o => o.UserID == userID).Select(a => a.EmployeeNM).FirstOrDefault().ToString() + " duyet thanh toan " + String.Format("{0:n0}", dtoinvoice.PlanPayment) + " " + dtoinvoice.Currency;
                                    }
                                    context.SaveChanges();
                                }
                                
                            }
                        }
                    }
                    dtoItem = GetInitData(userID, out notification);
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }

            return true;
        }
        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditForm GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            throw new NotImplementedException();
        }

        

        public override object GetInitData(int userId, out Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitForm data = new DTO.InitForm();
            data.keyRawMaterials = new List<KeyRawMaterial>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    data.keyRawMaterials = AutoMapper.Mapper.Map<List<AccountPayableRpt_KeyRawMaterial_View>, List<DTO.KeyRawMaterial>>(context.AccountPayableRpt_KeyRawMaterial_View.ToList());
                    data.dueColorDTOs = AutoMapper.Mapper.Map<List<AccountPayableRpt_DueColor_View>, List<DTO.DueColorDTO>>(context.AccountPayableRpt_DueColor_View.ToList());
                    // data.PurchaseInvoiceDTO = AutoMapper.Mapper.Map<List<AccountPayableMng_PurchaseInvoice_View>, List<DTO.PurchaseInvoiceDTO>>(context.AccountPayableRpt_function_PurchaseInvoice());
                    data.SupplierDTO= AutoMapper.Mapper.Map<List<AccountPayableMng_FactoryRawMaterial_View>, List<DTO.SupplierDTO>>(context.AccountPayableMng_FactoryRawMaterial_View.ToList());
                } 
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        //public List<DTO.PurchaseInvoiceDTO> GetPurchaseInvoiceDTO(int userId, out Library.DTO.Notification notification)
        //{
        //    List<DTO.PurchaseInvoiceDTO> Data = new List<DTO.PurchaseInvoiceDTO>();
        //    notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
        //    try
        //    {
        //        using (AccountPayableRptEntities context = CreateContext())
        //        {
        //            Data = converter.DB2DTO_PurchaseInvoice(context.AccountPayableMng_PurchaseInvoice_View.ToList());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.HandleExceptionSingleLine(ex);
        //    }
        //    return Data;
        //}
        public override DTO.SearchForm GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
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
