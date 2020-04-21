using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.PaymentTermMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.PaymentTermMng.SearchFormData, DTO.PaymentTermMng.EditFormData, DTO.PaymentTermMng.PaymentTerm>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();

        private string _TempFolder;

        public DataFactory() { }
        

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private PaymentTermMngEntities CreateContext()
        {
            return new PaymentTermMngEntities(DALBase.Helper.CreateEntityConnectionString("PaymentTermMngModel"));
        }
        public override DTO.PaymentTermMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PaymentTermMng.SearchFormData data = new DTO.PaymentTermMng.SearchFormData();
            data.Data = new List<DTO.PaymentTermMng.PaymentTermSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    string paymentTermNM = null;
                    bool? isActive = null;
                    string paymentMethod = null;
                    int? paymentTypeID = null;

                    if (filters.ContainsKey("PaymentTermNM") && !string.IsNullOrEmpty(filters["PaymentTermNM"].ToString()))
                    {
                        paymentTermNM = filters["PaymentTermNM"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsActive") && !string.IsNullOrEmpty(filters["IsActive"].ToString()))
                    {
                        isActive = Convert.ToBoolean(filters["IsActive"].ToString());
                    }
                    if (filters.ContainsKey("PaymentMethod") && !string.IsNullOrEmpty(filters["PaymentMethod"].ToString()))
                    {
                        paymentMethod = filters["PaymentMethod"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PaymentTypeID") && !string.IsNullOrEmpty(filters["PaymentTypeID"].ToString()))
                    {
                        paymentTypeID = Convert.ToInt32(filters["PaymentTypeID"].ToString());
                    }

                    totalRows = context.PaymentTermMng_function_SearchPaymentTerm(paymentTermNM, paymentTypeID, isActive, paymentMethod, orderBy, orderDirection).Count();
                    var result = context.PaymentTermMng_function_SearchPaymentTerm(paymentTermNM, paymentTypeID, isActive, paymentMethod, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_PaymentTermSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public override DTO.PaymentTermMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.PaymentTermMng.EditFormData data = new DTO.PaymentTermMng.EditFormData();
            data.Data = new DTO.PaymentTermMng.PaymentTerm();
            data.PaymentTypes = new List<DTO.Support.PaymentType>();
            data.PaymentMethods = new List<DTO.Support.PaymentMethod>();

            //try to get data
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    data.Data = converter.DB2DTO_PaymentTerm(context.PaymentTermMng_PaymentTerm_View.FirstOrDefault(o => o.PaymentTermID == id));
                    data.PaymentTypes = supportFactory.GetPaymentType().ToList();
                    data.PaymentMethods = supportFactory.GetPaymentMethod().ToList();
                    data.ActiveStatuses = supportFactory.GetYesNo().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success, Message = "Delete Success" };

            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    PaymentTerm dbItem = context.PaymentTerms.FirstOrDefault(o => o.PaymentTermID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Payment Term not found!";
                        return false;
                    }
                    else
                    {
                        context.PaymentTerms.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }
        public override bool UpdateData(int id, ref DTO.PaymentTermMng.PaymentTerm dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (PaymentTermMngEntities context = CreateContext())
                {
                    PaymentTerm dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new PaymentTerm();
                        context.PaymentTerms.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.PaymentTerms.FirstOrDefault(o => o.PaymentTermID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Payment Term not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_PaymentTerm(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.PaymentTermID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.PaymentTermMng.PaymentTerm dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.PaymentTermMng.PaymentTerm dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.PaymentTermMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //int i = 0;
            DTO.PaymentTermMng.SearchFilterData data = new DTO.PaymentTermMng.SearchFilterData();
            data.YesNoValues = new List<DTO.Support.YesNo>();
            data.PaymentMethods = new List<DTO.Support.PaymentMethod>();
            data.PaymentTypes = new List<DTO.Support.PaymentType>();

            //try to get data
            try
            {
                data.YesNoValues = supportFactory.GetYesNo().ToList();
                data.PaymentMethods = supportFactory.GetPaymentMethod().ToList();
                data.PaymentTypes = supportFactory.GetPaymentType().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
    }
}
