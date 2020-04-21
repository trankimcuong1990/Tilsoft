using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.CompanyBranchMng.DTO;

namespace Module.CompanyBranchMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory2<DTO.SearchFormDTO, DTO.EditFormDTO>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private readonly DataConverter dataConverter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int userId, int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (CompanyBranchMngEntities context = CreateContext())
                {
                    Company dbCompany = context.Company.FirstOrDefault(o => o.CompanyID == id);

                    if (dbCompany == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data";

                        return false;
                    }

                    context.Company.Remove(dbCompany);
                    context.SaveChanges();

                    // refresh cache 
                    Library.CacheHelper.ClearCache("SUPPORT_INTERNAL_COMPANY");

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public override EditFormDTO GetData(int userId, int id, Hashtable param, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            EditFormDTO data = new EditFormDTO();

            try
            {
                using (CompanyBranchMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = dataConverter.DB2DTO_Company(context.CompanyBranchMng_Company_View.FirstOrDefault(o => o.CompanyID == id));
                    }

                    data.TimeRanges = supportFactory.GetTimeRange();
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public override SearchFormDTO GetDataWithFilter(int userId, Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            totalRows = 0;
            SearchFormDTO data = new SearchFormDTO();

            try
            {
                string company = (filters.ContainsKey("Company") && filters["Company"] != null && !string.IsNullOrEmpty(filters["Company"].ToString())) ? filters["Company"].ToString() : null;
                string taxCode = (filters.ContainsKey("TaxCode") && filters["TaxCode"] != null && !string.IsNullOrEmpty(filters["TaxCode"].ToString())) ? filters["TaxCode"].ToString() : null;
                string address = (filters.ContainsKey("Address") && filters["Address"] != null && !string.IsNullOrEmpty(filters["Address"].ToString())) ? filters["Address"].ToString() : null;
                string location = (filters.ContainsKey("Location") && filters["Location"] != null && !string.IsNullOrEmpty(filters["Location"].ToString())) ? filters["Location"].ToString() : null;
                string phone = (filters.ContainsKey("Phone") && filters["Phone"] != null && !string.IsNullOrEmpty(filters["Phone"].ToString())) ? filters["Phone"].ToString() : null;
                string fax = (filters.ContainsKey("Fax") && filters["Fax"] != null && !string.IsNullOrEmpty(filters["Fax"].ToString())) ? filters["Fax"].ToString() : null;
                string email = (filters.ContainsKey("Email") && filters["Email"] != null && !string.IsNullOrEmpty(filters["Email"].ToString())) ? filters["Email"].ToString() : null;
                string website = (filters.ContainsKey("Website") && filters["Website"] != null && !string.IsNullOrEmpty(filters["Website"].ToString())) ? filters["Website"].ToString() : null;

                using (CompanyBranchMngEntities context = CreateContext())
                {
                    totalRows = context.CompanyBranchMng_function_CompanySearchResult(company, taxCode, address, location, phone, fax, email, website, orderBy, orderDirection).Count();
                    data.Data = dataConverter.DB2DTO_CompanySearchResult(context.CompanyBranchMng_function_CompanySearchResult(company, taxCode, address, location, phone, fax, email, website, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public override object GetInitData(int userId, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            InitFormDTO data = new InitFormDTO();

            try
            {
                Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
                data.Factories = supportFactory.GetFactory().ToList();

                using (CompanyBranchMngEntities context = CreateContext())
                {
                    data.Locations = dataConverter.DB2DTO_Location(context.CompanyBranchMng_Location_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
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
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                CompanyDTO dtoCompany = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<CompanyDTO>();

                using (CompanyBranchMngEntities context = CreateContext())
                {
                    Company dbCompany;

                    if (id > 0)
                    {
                        dbCompany = context.Company.FirstOrDefault(o => o.CompanyID == id);
                    }
                    else
                    {
                        dbCompany = new Company();
                        context.Company.Add(dbCompany);
                    }

                    if (dbCompany == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not find data";

                        return false;
                    }

                    if (dtoCompany.HasChanged)
                    {
                        Framework.DAL.DataFactory framworkFactory = new Framework.DAL.DataFactory();
                        dtoCompany.Logo = framworkFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\", dtoCompany.NewFile, dtoCompany.Logo);
                    }

                    dataConverter.DTO2DB_Company(dtoCompany, ref dbCompany);

                    dbCompany.UpdatedBy = userId;
                    dbCompany.UpdatedDate = DateTime.Now;

                    context.Branch.Local.Where(o => o.Company == null).ToList().ForEach(o => context.Branch.Remove(o));
                    context.SaveChanges();

                    // refresh cache 
                    Library.CacheHelper.ClearCache("SUPPORT_INTERNAL_COMPANY");

                    dtoItem = GetData(userId, dbCompany.CompanyID, new Hashtable(), out notification);
                }

                return true;
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public object GetQuickSearchBranch(Hashtable filters, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            List<DTO.BranchQuickSearchResultDTO> data = new List<BranchQuickSearchResultDTO>();

            try
            {
                string searchString = (filters.ContainsKey("SearchString") && filters["SearchString"] != null && !string.IsNullOrEmpty(filters["SearchString"].ToString())) ? filters["SearchString"].ToString() : null;

                if (!string.IsNullOrEmpty(searchString))
                {
                    using (CompanyBranchMngEntities context = CreateContext())
                    {
                        var dbFactoryQuickSearch = context.CompanyBranchMng_FactoryQuickSearchResult_View.Where(o => o.FactoryUD.Contains(searchString) || o.FactoryName.Contains(searchString));

                        if (dbFactoryQuickSearch.Count() == 0)
                        {
                            var dbCompanyQuickSearch = context.CompanyBranchMng_CompanyQuickSearchResult_View.Where(o => o.CompanyUD.Contains(searchString) || o.CompanyNM.Contains(searchString));
                            data = dataConverter.DB2DTO_QuickSearchBranchFromCompany(dbCompanyQuickSearch.ToList());
                        }
                        else
                        {
                            data = dataConverter.DB2DTO_QuickSearchBranchFromFactory(dbFactoryQuickSearch.ToList());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public CompanyBranchMngEntities CreateContext()
        {
            return new CompanyBranchMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CompanyBranchMngModel"));
        }
    }
}
