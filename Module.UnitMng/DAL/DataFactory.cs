using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.UnitMng.DTO;
using Newtonsoft.Json.Linq;

namespace Module.UnitMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<SearchData, EditData>
    {
        DataConverter converter = new DataConverter();
        //Module.Framework.DAL.DataFactory fwDataFactory = new Framework.DAL.DataFactory();

        UnitMngEntities CreatContext()
        {
            return new UnitMngEntities(Library.Helper.CreateEntityConnectionString("DAL.UnitMngModel"));
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using(var Context = CreatContext())
                {
                    Unit unit = Context.Unit.FirstOrDefault(o => o.UnitID == id);

                    if(unit == null)
                    {
                        notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        return false;
                    }

                    Context.Unit.Remove(unit);
                    Context.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditData data = new EditData
            {
                Data = new UnitMngDto()
            };

            try
            {
                if (id > 0)
                {
                    using (var context = CreatContext())
                    {
                        var item = context.UnitMng_Unit_View.FirstOrDefault(o => o.UnitID == id);
                        if(item == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                        }
                        else
                        {
                            data.Data = this.converter.DB2DTO_UnitMng(item);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
            }
            return data;

        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;

            notification = new Notification() { Type = NotificationType.Success };

            SearchData data = new SearchData() { Data = new List<UnitMngSearchDto>() };

            try
            {
                using( var context = CreatContext())
                {
                    string unitNM = filters["UnitNM"]?.ToString().Replace("'", "''");
                    string unitUD = filters["UnitUD"]?.ToString().Replace("'", "''");

                    totalRows = context.UnitMng_function_UnitSearchResult(unitUD, unitNM, orderBy, orderDirection).Count();
                    data.Data = this.converter.BD2DTO_UnitMngSearchResult(context.UnitMng_function_UnitSearchResult(unitUD, unitNM, orderBy, orderDirection).Skip(pageSize * (pageIndex -1)).Take(pageSize).ToList());
                }
            }
            catch(Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message

                };
            }
            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.UnitMngDto unitMngDto = ((JObject)dtoItem).ToObject<DTO.UnitMngDto>();

            notification = new Notification { Type = NotificationType.Success };

            try
            {
                using(var context = CreatContext())
                {
                    Unit unit = new Unit();

                    if(id == 0)
                    {
                        context.Unit.Add(unit);
                    }

                    if (id > 0)
                    {
                        unit = context.Unit.FirstOrDefault(o => o.UnitID == id);

                        if(unit == null)
                        {
                            notification = new Notification { Type = NotificationType.Error, Message = "Can't Find Data" };
                            return false;
                        }
                    }

                    this.converter.DTO2DB_UnitMng(unitMngDto, ref unit);
                    context.SaveChanges();

                    dtoItem = this.GetData(unit.UnitID, out notification);
                }
                return true;
            }
            catch(Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }

        }

       


      
    }
}
