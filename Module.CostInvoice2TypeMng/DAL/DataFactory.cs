using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.CostInvoice2TypeMng.DTO;
using Newtonsoft.Json.Linq;

namespace Module.CostInvoice2TypeMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<SearchData, EditData>
    {
       
        DataConverter converter = new DataConverter();

        Framework.DAL.DataFactory fwDataFactory = new Framework.DAL.DataFactory();


        public CostInvoice2TypeEntities CreatContext()
        {
            return new CostInvoice2TypeEntities(Library.Helper.CreateEntityConnectionString("DAL.CostInvoice2TypeMngModel"));
        }


        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using(var context = CreatContext())
                {
                    CostInvoice2Type costInvoice2Type = context.CostInvoice2Type.FirstOrDefault(o => o.CostInvoice2TypeID == id);
                    if(costInvoice2Type == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }

                    context.CostInvoice2Type.Remove(costInvoice2Type);
                    context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message

                };
                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditData data = new EditData()
            {
                Data = new CostInvoice2TypeDto()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreatContext())
                    {
                        var item = context.CostInvoice2TypeMng_CostInvoice2Type_View.FirstOrDefault(o => o.CostInvoice2TypeID == id);

                        if (item == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = ("Can't Find Data") };
                        }
                        else
                        {
                            data.Data = this.converter.DB2DTO_ConstInvoice2Type(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };

            }

            return data;
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification() { Type = NotificationType.Success };

            SearchData data = new SearchData()
            {
                Data = new List<CostInvoice2TypeSearchDto>()
            };

            try
            {
                using(var context = CreatContext())
                {
                    //string CostInvoice2TypeID = filters["CostInvoice2TypeID"]?.ToString().Replace("'", "''");
                    string costInvoice2TypeNM = filters["CostInvoice2TypeNM"]?.ToString().Replace("'", "''");
                    
                    totalRows = context.CostInvoice2TypeMng_function_CostInvoice2TypeSearchResult(costInvoice2TypeNM, orderBy, orderDirection).Count();
                    data.Data = this.converter.DB2DTO_CostInvoice2TypeSearch(context.CostInvoice2TypeMng_function_CostInvoice2TypeSearchResult(costInvoice2TypeNM, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
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
            DTO.CostInvoice2TypeDto costInvoice2TypeDto = ((JObject) dtoItem).ToObject<DTO.CostInvoice2TypeDto>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using(var context = CreatContext())
                {
                    CostInvoice2Type costInvoice2Type = new CostInvoice2Type();

                    if (id == 0)
                    {
                        context.CostInvoice2Type.Add(costInvoice2Type);

                    }
                    if (id > 0)
                    {
                        costInvoice2Type = context.CostInvoice2Type.FirstOrDefault(o => o.CostInvoice2TypeID == id);
                        if(costInvoice2Type == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                    }
                    this.converter.DTO2DB_CostInvoice(costInvoice2TypeDto, ref costInvoice2Type);
                    context.SaveChanges();

                    dtoItem = this.GetData(costInvoice2Type.CostInvoice2TypeID, out notification);
                }
                return true;
            }
            catch(Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };

                return false;
            }


        }
    }
}
