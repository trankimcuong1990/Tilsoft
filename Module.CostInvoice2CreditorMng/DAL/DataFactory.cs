using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.CostInvoice2CreditorMng.DTO;
using Newtonsoft.Json.Linq;

namespace Module.CostInvoice2CreditorMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<SearchData, EditData>
    {
        CostInvoice2CreditorMngEntities CreatContext()
        {
            return new CostInvoice2CreditorMngEntities(Library.Helper.CreateEntityConnectionString("DAL.CostInvoice2CreditorMngModel"));
        }

        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

        DataConverter converter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = CreatContext())
                {
                    CostInvoice2Creditor costInvoice2Creditor = context.CostInvoice2Creditor.FirstOrDefault(o => o.CostInvoice2CreditorID == id);
                    if (costInvoice2Creditor == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error, Message = "Can,t Find Data" };

                        return false;
                    }

                    context.CostInvoice2Creditor.Remove(costInvoice2Creditor);
                    context.SaveChanges();
                    return false;

                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }
            return false;
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditData data = new EditData()
            {
                Data = new CostInvoice2CreditorMngDto()
            };

            try
            {
                if (id > 0)
                {
                    using(var context = CreatContext())
                    {
                        var item = context.CostInvoice2CreditorMng_CostInvoice2Creditor_View.FirstOrDefault(o => o.CostInvoice2CreditorID == id);
                        if(item == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.Data = this.converter.DB2DTO_CostInvoice2CreditorMng(item);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
            }
            return data;
        }                                                                                                 

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchData data = new SearchData
            {
                Data = new List<CostInvoice2CreditorMngSearchDto>()
            };

            try
            {
                using(var Context = CreatContext())
                {
                    string costInvoice2CreditorUD = filters["CostInvoice2CreditorUD"]?.ToString().Replace("'", "''");
                    string costInvoice2CreditorNM = filters["CostInvoice2CreditorNM"]?.ToString().Replace("'", "''");

                    totalRows = Context.CostInvoice2CreditorMng_function_CostInvoice2CreditorSearchResult(costInvoice2CreditorUD, costInvoice2CreditorNM, orderBy, orderDirection).Count();
                    data.Data = this.converter.DB2DTO_CostInvoice2CreditorSearchMng(Context.CostInvoice2CreditorMng_function_CostInvoice2CreditorSearchResult(costInvoice2CreditorUD, costInvoice2CreditorNM, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch(Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };

            }
            return data;

        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.CostInvoice2CreditorMngDto costInvoice2CreditorMngDto = ((JObject)dtoItem).ToObject<DTO.CostInvoice2CreditorMngDto>();
            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using(var context = CreatContext())
                {
                    CostInvoice2Creditor costInvoice2Creditor = new CostInvoice2Creditor();

                    if (id == 0)
                    {
                        context.CostInvoice2Creditor.Add(costInvoice2Creditor);

                    }
                    if (id > 0)
                    {
                        costInvoice2Creditor = context.CostInvoice2Creditor.FirstOrDefault(o => o.CostInvoice2CreditorID == id);
                        if(costInvoice2Creditor == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                    }

                    this.converter.BTO2DB_CostInvoice2CreditorMng(costInvoice2CreditorMngDto, ref costInvoice2Creditor);
                    context.SaveChanges();

                    dtoItem = this.GetData(costInvoice2Creditor.CostInvoice2CreditorID, out notification);
                }
                return true;
            }               catch(Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }
    }
}
