using Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ForwarderMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ForwarderMng.SearchFormData, DTO.ForwarderMng.EditFormData, DTO.ForwarderMng.Forwarder>
    {
        private string _TempFolder;

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private DataConverter converter = new DataConverter();

        private ForwarderMngEntities CreateContext()
        {
            return new ForwarderMngEntities(DALBase.Helper.CreateEntityConnectionString("ForwarderMngModel"));
        }

        public override DTO.ForwarderMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ForwarderMng.SearchFormData data = new DTO.ForwarderMng.SearchFormData();
            data.Data = new List<DTO.ForwarderMng.ForwarderSearchResult>();
            totalRows = 0;

            string Email = null;
            string Tel = null;
            string Mobile = null;
            string ForwarderUD = null;
            string ForwarderNM = null;


            if (filters.ContainsKey("ForwarderUD") && !string.IsNullOrEmpty(filters["ForwarderUD"].ToString()))
            {
                ForwarderUD = filters["ForwarderUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ForwarderNM") && !string.IsNullOrEmpty(filters["ForwarderNM"].ToString()))
            {
                ForwarderNM = filters["ForwarderNM"].ToString().Replace("'", "''");
            }


            if (filters.ContainsKey("Email") && !string.IsNullOrEmpty(filters["Email"].ToString()))
            {
                Email = filters["Email"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Tel") && !string.IsNullOrEmpty(filters["Tel"].ToString()))
            {
                Tel = filters["Tel"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("Mobile") && !string.IsNullOrEmpty(filters["Mobile"].ToString()))
            {
                Mobile = filters["Mobile"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (ForwarderMngEntities context = CreateContext())
                {
                    totalRows = context.ForwarderMng_function_SearchForwarder(ForwarderUD, ForwarderNM, Tel, Mobile, Email, orderBy, orderDirection).Count();
                    var result = context.ForwarderMng_function_SearchForwarder(ForwarderUD, ForwarderNM, Tel, Mobile, Email, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_ForwarderSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ForwarderMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ForwarderMng.EditFormData data = new DTO.ForwarderMng.EditFormData();
            data.Data = new DTO.ForwarderMng.Forwarder();
            data.Data.ForwarderImages = new List<DTO.ForwarderMng.ForwarderImage>();
            data.Data.ForwarderPICs = new List<DTO.ForwarderMng.ForwarderPIC>();
            //Support
            data.Employees = new List<DTO.ForwarderMng.SupportEmployee>();

            //try to get data
            try
            {
                using (ForwarderMngEntities context = CreateContext())
                {
                    if (id != 0)
                    {
                        data.Data = converter.DB2DTO_Forwarder(context.ForwarderMng_Forwarder_View.FirstOrDefault(o => o.ForwarderID == id));
                    }
                    data.Employees = converter.DB2DTO_SupportEmployee(context.SupportMng_Employee_View.ToList());
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);

                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (ForwarderMngEntities context = CreateContext())
                {
                    Forwarder dbItem = context.Forwarder.FirstOrDefault(o => o.ForwarderID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Forwarder not found!";
                        return false;
                    }
                    else
                    {
                        context.Forwarder.Remove(dbItem);
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

        public override bool UpdateData(int id, ref DTO.ForwarderMng.Forwarder dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            int number;
            string indexName;

            try
            {
                using (ForwarderMngEntities context = CreateContext())
                {
                    Forwarder dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new Forwarder();
                        context.Forwarder.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Forwarder.FirstOrDefault(o => o.ForwarderID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Forwarder not found!";
                        return false;
                    }
                    else
                    {
                        converter.DTO2BD_Forwarder(dtoItem, ref dbItem, this._TempFolder);

                        context.ForwarderImage.Local.Where(o => o.Forwarder == null).ToList().ForEach(o => context.ForwarderImage.Remove(o));
                        context.ForwarderPIC.Local.Where(o => o.Forwarder == null).ToList().ForEach(o => context.ForwarderPIC.Remove(o));

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ForwarderID, out notification).Data;

                        return true;
                    }
                }
            }
            catch (System.Data.DataException dEx)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                Library.ErrorHelper.DataExceptionParser(dEx, out number, out indexName);
                if (number == 2601 && !string.IsNullOrEmpty(indexName))
                {
                    if (indexName == "ForwarderUDUnique")
                    {
                        notification.Message = "The Forwarder Code is already exists";
                    }
                }
                else
                {
                    notification.Message = dEx.Message;
                }

                return false;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.ForwarderMng.Forwarder dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int id, ref DTO.ForwarderMng.Forwarder dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public DTO.ForwarderMng.InitFormData GetInitData(out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            DTO.ForwarderMng.InitFormData data = new DTO.ForwarderMng.InitFormData();
            data.Countries = new List<DTO.ForwarderMng.Country>();
            data.Departments = new List<DTO.ForwarderMng.Department>();

            try
            {
                using (var context = CreateContext())
                {
                    data.Countries = converter.DB2DTO_Country(context.SupportMng_Country_View.ToList());
                    data.Departments = converter.DB2DTO_Department(context.SupportMng_InternalDepartment_View.ToList());
                }
            }
            catch (Exception ex)
            {
                Exception ex_1 = Library.Helper.GetInnerException(ex);

                notification.Type = NotificationType.Error;
                notification.Message = ex_1.Message;
            }

            return data;
        }
    }
}
