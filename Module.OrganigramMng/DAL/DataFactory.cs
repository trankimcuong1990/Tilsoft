using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module.OrganigramMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private string _tempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();
        public DataFactory() { }
        public DataFactory(string tempFolder)
        {
            this._tempFolder = tempFolder + @"\";
        }
        private OrganigramMngEntities CreateContext()
        {
            return new OrganigramMngEntities(Library.Helper.CreateEntityConnectionString("DAL.OrganigramMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.OrganigramSearchResult>();

            totalRows = 0;

            //try to get data
            try
            {
                using (OrganigramMngEntities context = CreateContext())
                {
                    int? CompanyID = null;
                    int? TotalEmp = null;

                    if (filters.ContainsKey("CompanyID") && !string.IsNullOrEmpty(filters["CompanyID"].ToString()))
                    {
                        CompanyID = Convert.ToInt32(filters["CompanyID"]);
                    }

                    if (filters.ContainsKey("TotalEmp") && !string.IsNullOrEmpty(filters["TotalEmp"].ToString()))
                    {
                        TotalEmp = Convert.ToInt32(filters["TotalEmp"]);
                    }

                    totalRows = context.OrganigramMng_function_SearchOrganigram(CompanyID, TotalEmp, orderBy, orderDirection).Count();
                    var result = context.OrganigramMng_function_SearchOrganigram(CompanyID, TotalEmp, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_OrganigramSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    foreach (DTO.OrganigramSearchResult dtoResult in data.Data)
                    {
                        var OrganizationChart = context.OrganigramMng_OrganizationChart_View.FirstOrDefault(o => o.CompanyID == dtoResult.CompanyID);
                        dtoResult.OrganizationChart = converter.DB2DTO_OrganizationChart(OrganizationChart);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            DTO.EditFormData data = new DTO.EditFormData();
            data.OrganizationChart = new DTO.OrganizationChart();
            data.Data = new List<DTO.Employee>();

            //try to get data
            try
            {
                if (id > 0)
                {
                    using (OrganigramMngEntities context = CreateContext())
                    {
                        var Organigram = context.OrganigramMng_Organigram_View.Where(o => o.CompanyID == id).ToList();
                        var OrganizationChart = context.OrganigramMng_OrganizationChart_View.FirstOrDefault(o => o.CompanyID == id);
                        if (Organigram == null)
                        {
                            throw new Exception("Can not found the organigram info to edit");
                        }
                        data.OrganizationChart = converter.DB2DTO_OrganizationChart(OrganizationChart);
                        if(data.OrganizationChart == null)
                        {
                            data.OrganizationChart = new DTO.OrganizationChart();
                        }
                        data.Data = converter.DB2DTO_Organigram(Organigram);
                    }
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
            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (OrganigramMngEntities context = CreateContext())
            //    {
            //        Organigram dbItem = context.Organigram.FirstOrDefault(o => o.OrganigramID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Organigram info not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            context.Organigram.Remove(dbItem);
            //            context.SaveChanges();

            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification.Type = Library.DTO.NotificationType.Error;
            //    notification.Message = ex.Message;
            //    notification.DetailMessage.Add(ex.Message);
            //    if (ex.GetBaseException() != null)
            //    {
            //        notification.DetailMessage.Add(ex.GetBaseException().Message);
            //    }
            //    return false;
            //}
            throw new NotImplementedException();

        }
        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            DTO.OrganizationChart dtoOrganizationChart = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.OrganizationChart>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (OrganigramMngEntities context = CreateContext())
                {
                    Organigram dbItem = null;
                    if (id < 0)
                    {
                        dbItem = new Organigram();
                        context.Organigram.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.Organigram.FirstOrDefault(o => o.OrganigramID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Organigram info not found!";
                        return false;
                    }
                    else
                    {
                        //foreach(DTO.Employee )
                        converter.DTO2BD(dtoOrganizationChart, ref dbItem);


                        // processing image
                        if (dtoOrganizationChart.ImageFile_HasChange)
                        {
                            dbItem.ImageFile = fwFactory.CreateFilePointer(this._tempFolder, dtoOrganizationChart.ImageFile_NewFile, dtoOrganizationChart.ImageFile);
                        }
                        if (dtoOrganizationChart.WorkSpaceFile_HasChanged)
                        {
                            dbItem.WorkSpaceFile = fwFactory.CreateFilePointer(this._tempFolder, dtoOrganizationChart.WorkSpaceFile_NewFile, dtoOrganizationChart.WorkSpaceFile);
                        }

                        dbItem.UpdatedDate = DateTime.Now;
                        dbItem.UpdatedBy = userId;

                        context.SaveChanges();

                        dtoItem = GetData(dbItem.OrganigramID, out notification).Data;

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
            //throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }
        public DTO.SearchFilterData GetSearchFilter(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFilterData data = new DTO.SearchFilterData();
            data.Companies = new List<Module.Support.DTO.InternalCompany>();

            try
            {
                data.Companies = supportFactory.GetInternalCompany().ToList();
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
