namespace Module.ProductionTeam.DAL
{
    using System;
    using System.Collections;
    using Library.DTO;
    using Module.ProductionTeam.DTO;
    using System.Linq;
    using Newtonsoft.Json.Linq;
    
    internal class DataFactory : Library.Base.DataFactory<SearchData, EditData>
    {
        DataConverter converter = new DataConverter();
        Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        ProductionTeamEntities CreateContext()
        {
            return new ProductionTeamEntities(Library.Helper.CreateEntityConnectionString("DAL.ProductionTeamModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification();
            notification.Type = NotificationType.Success;

            try
            {
                using (var context = this.CreateContext())
                {
                    ProductionTeam productionTeam = context.ProductionTeam.FirstOrDefault(o => o.ProductionTeamID == id);

                    if (productionTeam == null)
                    {
                        notification.Type = NotificationType.Error;
                        notification.Message = "Can not found data!";

                        return false;
                    }

                    context.ProductionTeam.Remove(productionTeam);
                    context.SaveChanges();
                }

                return true;
            }
            catch (System.Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditData data = new EditData()
            {
                Data = new ProductionTeamDto(),
                WorkCenters = new System.Collections.Generic.List<Support.DTO.WorkCenter>(),
                Employees = new System.Collections.Generic.List<Support.DTO.Employee>()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = this.CreateContext())
                    {
                        var item = context.ProductionTeam_ProductionTeam_View.FirstOrDefault(o => o.ProductionTeamID == id);
                        if (item == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can not find data." };
                        }
                        else
                        {
                            data.Data = this.converter.DB2DTO_ProductionTeam(item);
                        }
                    }
                }
                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                Module.Support.DTO.FactoryRawMaterialData support_factoryRawMaterialData = new Support.DTO.FactoryRawMaterialData();
                
                data.WorkCenters = support_factory.GetWorkCenter();
                data.Employees = support_factory.GetEmployee();
                data.factoryRawMaterialDatas = support_factory.GetSubSupplier(out notification);
            }
            catch (System.Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }

            return data;
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchData data = new SearchData() { Data = new System.Collections.Generic.List<ProductionTeamSearchDto>() };

            try
            {
                using (var context = this.CreateContext())
                {
                    int? workCenterID = null;
                    string productionTeamUD = filters["ProductionTeamUD"]?.ToString().Replace("'", "''");
                    string productionTeamNM = filters["ProductionTeamNM"]?.ToString().Replace("'", "''");
                    if (filters["WorkCenterID"] != null)
                    {
                        workCenterID = Convert.ToInt32(filters["WorkCenterID"]);
                    }
                    string companyID = this.GetCompany(filters["UserID"]?.ToString().Replace("'", "''"), context);
                    totalRows = context.ProductionTeam_function_ProductionTeamSearchResult(productionTeamUD, productionTeamNM, workCenterID, companyID, orderBy, orderDirection).Count();
                    data.Data = this.converter.DB2DTO_ProductionTeamSearchResult(context.ProductionTeam_function_ProductionTeamSearchResult(productionTeamUD, productionTeamNM, workCenterID, companyID, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
                Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
                data.WorkCenters = support_factory.GetWorkCenter();
            }
            catch (System.Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.ProductionTeamDto productionTeamDto = ((JObject)dtoItem).ToObject<DTO.ProductionTeamDto>();
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = this.CreateContext())
                {
                    ProductionTeam productionTeam = new ProductionTeam();

                    if (id == 0)
                        context.ProductionTeam.Add(productionTeam);

                    if (id > 0)
                    {
                        productionTeam = context.ProductionTeam.FirstOrDefault(o => o.ProductionTeamID == id);

                        if (productionTeam == null)
                        {
                            notification.Message = "Can not found data!";
                            return false;
                        }
                    }

                    this.converter.DTO2DB_ProductionTeam(productionTeamDto, ref productionTeam);
                    productionTeam.UpdatedBy = userId;
                    productionTeam.UpdatedDate = System.DateTime.Now;
                    productionTeam.CompanyID = fwFactory.GetCompanyID(userId);
                    context.SaveChanges();
                    dtoItem = this.GetData(productionTeam.ProductionTeamID, out notification);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                notification.Type = NotificationType.Error;
                notification.Message = ex.Message;

                return false;
            }
        }

        private string GetCompany(string userId, ProductionTeamEntities context)
        {
            int id = System.Convert.ToInt32(userId);
            return fwFactory.GetCompanyID(id).HasValue ? fwFactory.GetCompanyID(id).Value.ToString() : null;
        }
    }
}
