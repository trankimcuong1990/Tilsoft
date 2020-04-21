using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Newtonsoft.Json.Linq;
using Module.ClientRelationshipTypeMng.DTO;
using System.Collections.Generic;

namespace Module.ClientRelationshipTypeMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<SearchData, EditData>
    {
        DataConverter Converter = new DataConverter();

        //Module.Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

            public ClientRelationshipTypeMngEntities CreatContext()
                 {
            return new ClientRelationshipTypeMngEntities(Library.Helper.CreateEntityConnectionString("DAL.ClientRelationshipTypeMngModel"));
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
                using ( var context = CreatContext())
                {
                    ClientRelationshipType clientRelationshipType = context.ClientRelationshipType.FirstOrDefault(o => o.ClientRelationshipTypeID == id);
                    if(clientRelationshipType == null)
                    {
                        notification = new Notification() { Type = NotificationType.Error };
                        return false;
                    }
                    context.ClientRelationshipType.Remove(clientRelationshipType);
                    context.SaveChanges();
                }
                return true;
            }
            catch(Exception ex)
            {
                notification = new Notification { Type = NotificationType.Error, Message = ex.Message };

                return false;
            }
        }

        public override EditData GetData(int id, out Notification notification)
        {
            notification = new Notification() { Type = NotificationType.Success };

            EditData data = new EditData()
            {
                Data = new ClientRelationshipTypeDto()
            };

            try
            {
               if(id > 0)
                {
                    using (var context = CreatContext())
                    {
                        var item = context.ClientRelationshipTypeMng_ClientRelationshipType_View.FirstOrDefault(o => o.ClientRelationshipTypeID == id);
                        if(item == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can Not Find Data" };
                        }
                        else
                        {
                            data.Data = this.Converter.DB2DTO_ClientRelationshipType(item);
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

            SearchData data = new SearchData()
            {
                Data = new List<ClientRelationshipTypeSearchDto>()
            };


            try
            {
                using(var context = CreatContext())
                {
                    string clientRelationshipTypeUD = filters["ClientRelationshipTypeUD"]?.ToString().Replace("'", "''");
                    string clientRelationshipTypeNM = filters["ClientRelationshipTypeNM"]?.ToString().Replace("'", "''");

                    totalRows = context.ClientRelationshipTypeMng_function_ClientRelationshipTypeSearchResult(clientRelationshipTypeUD, clientRelationshipTypeNM, orderBy, orderDirection).Count();
                    data.Data = this.Converter.DB2DTO_ClientRelationshipSearchResult(context.ClientRelationshipTypeMng_function_ClientRelationshipTypeSearchResult(clientRelationshipTypeUD, clientRelationshipTypeNM, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch( Exception ex)
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
            DTO.ClientRelationshipTypeDto clientRelationshipTypeDto = ((JObject)dtoItem).ToObject<DTO.ClientRelationshipTypeDto>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using( var context = this.CreatContext())
                {
                    ClientRelationshipType clientRelationshipType = new ClientRelationshipType();

                    if(id == 0)
                    {
                        context.ClientRelationshipType.Add(clientRelationshipType);
                    }
                    if(id > 0)
                    {
                        clientRelationshipType = context.ClientRelationshipType.FirstOrDefault(o => o.ClientRelationshipTypeID == id);

                        if(clientRelationshipType == null)
                        {
                            notification = new Notification() { Type = NotificationType.Error, Message = "Can Not Find Data" };

                            return false;
                        }
                    }

                    this.Converter.DTO2DB_ClientRelationshipTypeMng(clientRelationshipTypeDto, ref clientRelationshipType);
                    context.SaveChanges();

                    dtoItem = this.GetData(clientRelationshipType.ClientRelationshipTypeID, out notification);


                }

                return true;
            }
            catch(Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }

        public bool UpdateOrder(int userId, object data, out Notification notification)
        {
            List<DTO.ClientRelationshipTypeDto> clientRelationshipTypeDto = ((JArray)data).ToObject<List<DTO.ClientRelationshipTypeDto>>();

            notification = new Notification() { Type = NotificationType.Success };

            try
            {
                using (var context = this.CreatContext())
                {
                    ClientRelationshipType clientRelationshipType = new ClientRelationshipType();
                    foreach (var item in clientRelationshipTypeDto)
                    {
                        if (item.isChange == true)
                        {
                            clientRelationshipType = context.ClientRelationshipType.FirstOrDefault(o => o.ClientRelationshipTypeID == item.ClientRelationshipTypeID);
                            this.Converter.DTO2DB_ClientRelationshipTypeMng(item, ref clientRelationshipType);
                            context.SaveChanges();
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                notification = new Notification() { Type = NotificationType.Error, Message = ex.Message };
                return false;
            }
        }
    }
}
