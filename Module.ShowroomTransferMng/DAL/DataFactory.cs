using Library;
using System;
using System.Collections;
using System.Linq;

namespace Module.ShowroomTransferMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormDTO, DTO.EditFormDTO>
    {
        public readonly DataConverter dataConverter = new DataConverter();

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbShowroomTransfer = context.ShowroomTransfer.FirstOrDefault(o => o.ShowroomTransferID == id);
                    if (dbShowroomTransfer == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can't find Showroom Transfer with " + id.ToString();

                        return false;
                    }
                    else
                    {
                        // ReceivingNote
                        var dbReceivingNote = context.ReceivingNote.FirstOrDefault(o => o.CompanyID == 3 && o.WarehouseTransferID == dbShowroomTransfer.ShowroomTransferID);
                        if (dbReceivingNote != null)
                        {
                            context.ReceivingNote.Remove(dbReceivingNote);
                        }

                        // DeliveryNote
                        var dbDeliveryNote = context.DeliveryNote.FirstOrDefault(o => o.CompanyID == 3 && o.WarehouseTransferID == dbShowroomTransfer.ShowroomTransferID);
                        if (dbDeliveryNote != null)
                        {
                            context.DeliveryNote.Remove(dbDeliveryNote);
                        }

                        context.ShowroomTransfer.Remove(dbShowroomTransfer);

                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public override DTO.EditFormDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.EditFormDTO data = new DTO.EditFormDTO();

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        var dbItem = context.ShowroomTransferMng_ShowroomTransfer_View.FirstOrDefault(o => o.ShowroomTransferID == id);
                        if (dbItem == null)
                        {
                            notification.Type = Library.DTO.NotificationType.Error;
                            notification.Message = "Can't find Showroom Transfer with " + id.ToString();
                        }
                        else
                        {
                            data.Data = dataConverter.DB2DTO_ShowroomTransfer(dbItem);
                        }
                    }
                    else
                    {
                        data.Data.ShowroomTransferDate = DateTime.Now.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public override DTO.SearchFormDTO GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            totalRows = 0;

            DTO.SearchFormDTO data = new DTO.SearchFormDTO();

            try
            {
                string showroomTransferUD = (filters.ContainsKey("showroomTransferUD") && filters["showroomTransferUD"] != null && filters["showroomTransferUD"].ToString() != "") ? filters["showroomTransferUD"].ToString() : null;
                string showroomTransferDate = (filters.ContainsKey("showroomTransferDate") && filters["showroomTransferDate"] != null && filters["showroomTransferDate"].ToString() != "") ? filters["showroomTransferDate"].ToString() : null;
                int? fromWarehouseID = (filters.ContainsKey("fromWarehouseID") && filters["fromWarehouseID"] != null && filters["fromWarehouseID"].ToString() != "") ? (int?)System.Convert.ToInt32(filters["fromWarehouseID"].ToString()) : null;
                int? toWarehouseID = (filters.ContainsKey("toWarehouseID") && filters["toWarehouseID"] != null && filters["toWarehouseID"].ToString() != "") ? (int?)System.Convert.ToInt32(filters["toWarehouseID"].ToString()) : null;

                using (var context = CreateContext())
                {
                    totalRows = context.ShowroomTransferMng_function_ShowroomTransferSearchResult(showroomTransferUD, showroomTransferDate, fromWarehouseID, toWarehouseID, orderBy, orderDirection).Count();
                    var dbItem = context.ShowroomTransferMng_function_ShowroomTransferSearchResult(showroomTransferUD, showroomTransferDate, fromWarehouseID, toWarehouseID, orderBy, orderDirection);
                    //totalRows = dbItem.Count();
                    data.Data = dataConverter.DB2DTO_ShowroomTransferSearchResult(dbItem.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new System.NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                DTO.ShowroomTransferDTO dtoShowroomTransfer = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.ShowroomTransferDTO>();

                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyId = frameworkFactory.GetCompanyID(userId);

                if (companyId != 3)
                {
                    notification.Type = Library.DTO.NotificationType.Error;
                    notification.Message = "Your company is not An Viet Thinh, you can't update data!";
                    return false;
                }

                using (var context = CreateContext())
                {
                    ShowroomTransfer dbShowroomTransfer;

                    if (id > 0)
                    {
                        dbShowroomTransfer = context.ShowroomTransfer.FirstOrDefault(o => o.ShowroomTransferID == id);
                    }
                    else
                    {
                        dbShowroomTransfer = new ShowroomTransfer();
                        context.ShowroomTransfer.Add(dbShowroomTransfer);
                    }

                    if (dbShowroomTransfer == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can't find Showroom Transfer with " + id.ToString();

                        return false;
                    }
                    else
                    {
                        // Showroom Transfer
                        dbShowroomTransfer.ShowroomTransferDate = dtoShowroomTransfer.ShowroomTransferDate.ConvertStringToDateTime();
                        dbShowroomTransfer.FromWarehouseID = dtoShowroomTransfer.FromWarehouseID;
                        dbShowroomTransfer.ToWarehouseID = dtoShowroomTransfer.ToWarehouseID;
                        dbShowroomTransfer.Remark = dtoShowroomTransfer.Remark;

                        if (id == 0)
                        {
                            dbShowroomTransfer.CreatedBy = userId;
                            dbShowroomTransfer.CreatedDate = System.DateTime.Now;
                        }

                        dbShowroomTransfer.UpdatedBy = userId;
                        dbShowroomTransfer.UpdatedDate = System.DateTime.Now;

                        context.SaveChanges();
                        if (dbShowroomTransfer.ShowroomTransferUD == null || dbShowroomTransfer.ShowroomTransferUD == "")
                        {
                            dbShowroomTransfer.ShowroomTransferUD = context.ShowroomTransferMng_function_GenerateShowroomTransferUD(dbShowroomTransfer.ShowroomTransferID, companyId, dbShowroomTransfer.ShowroomTransferDate.Value.Year, dbShowroomTransfer.ShowroomTransferDate.Value.Month).FirstOrDefault();                         
                        }

                        // Receiving Note
                        var dbReceivingNote = context.ReceivingNote.FirstOrDefault(o => o.ShowroomTransferID == dbShowroomTransfer.ShowroomTransferID && o.CompanyID == companyId);
                        if (dbReceivingNote == null)
                        {
                            dbReceivingNote = new ReceivingNote();
                            context.ReceivingNote.Add(dbReceivingNote);
                        }

                        dbReceivingNote.ReceivingNoteDate = dbShowroomTransfer.ShowroomTransferDate;
                        dbReceivingNote.CompanyID = 3;
                        dbReceivingNote.ReceivingNoteTypeID = 1;
                        dbReceivingNote.ShowroomTransferID = dbShowroomTransfer.ShowroomTransferID;
                        dbReceivingNote.Description = string.Format("{0} [{1}]", "Automatically Receiving Note from Showroom Transfer", dbShowroomTransfer.ShowroomTransferUD);

                        // Responsible by to Factory Warehouse
                        var toFactoryWarehouse = context.FactoryWarehouse.FirstOrDefault(o => o.CompanyID == 3 && o.FactoryWarehouseID == dbShowroomTransfer.ToWarehouseID);
                        dbReceivingNote.CreatedBy = userId;
                        dbReceivingNote.CreatedDate = System.DateTime.Now;
                        dbReceivingNote.UpdatedBy = userId;
                        dbReceivingNote.UpdatedDate = System.DateTime.Now;
                        //dbReceivingNote.CreatedBy = (toFactoryWarehouse != null) ? toFactoryWarehouse.ResponsibleBy : null;
                        //dbReceivingNote.CreatedDate = System.DateTime.Now;
                        //dbReceivingNote.UpdatedBy = (toFactoryWarehouse != null) ? toFactoryWarehouse.ResponsibleBy : null;
                        //dbReceivingNote.UpdatedDate = System.DateTime.Now;

                        foreach (var dtoReceivingNoteDetail in dtoShowroomTransfer.ShowroomTransferDetails.ToList())
                        {
                            ReceivingNoteDetail dbReceivingNoteDetail = new ReceivingNoteDetail();
                            dbReceivingNoteDetail.ProductionItemID = dtoReceivingNoteDetail.ProductionItemID;
                            dbReceivingNoteDetail.FactoryWarehousePalletID = dtoReceivingNoteDetail.FactoryWarehouseToPalletID;
                            dbReceivingNoteDetail.ToFactoryWarehouseID = dtoShowroomTransfer.ToWarehouseID;
                            dbReceivingNoteDetail.Quantity = dtoReceivingNoteDetail.Quantity;
                            dbReceivingNoteDetail.QtyByUnit = dtoReceivingNoteDetail.Quantity;
                            dbReceivingNoteDetail.Description = dtoReceivingNoteDetail.Remark;

                            dbReceivingNote.ReceivingNoteDetail.Add(dbReceivingNoteDetail);
                        }

                        context.ReceivingNoteDetail.Local.Where(o => o.ReceivingNote == null).ToList().ForEach(o => context.ReceivingNoteDetail.Remove(o));
                        context.SaveChanges();
                        if (dbReceivingNote.ReceivingNoteUD == null || dbReceivingNote.ReceivingNoteUD == "")
                        {
                            context.ReceivingNoteMng_function_GenerateReceivingNoteUD(dbReceivingNote.ReceivingNoteID, companyId, dbReceivingNote.ReceivingNoteDate.Value.Year, dbReceivingNote.ReceivingNoteDate.Value.Month);
                        }

                        // Delivery Note
                        var dbDeliveryNote = context.DeliveryNote.FirstOrDefault(o => o.CompanyID == companyId && o.ShowroomTransferID == dbShowroomTransfer.ShowroomTransferID);
                        if (dbDeliveryNote == null)
                        {
                            dbDeliveryNote = new DeliveryNote();
                            context.DeliveryNote.Add(dbDeliveryNote);
                        }

                        dbDeliveryNote.DeliveryNoteDate = dbShowroomTransfer.ShowroomTransferDate;
                        dbDeliveryNote.CompanyID = 3;
                        dbDeliveryNote.DeliveryNoteTypeID = 1;
                        dbDeliveryNote.ShowroomTransferID = dbShowroomTransfer.ShowroomTransferID;
                        dbDeliveryNote.Description = string.Format("{0} [{1}]", "Automatically Delivery Note from Showroom Transfer", dbShowroomTransfer.ShowroomTransferUD);

                        // Responsible by from Factory Warehouse
                        var fromFactoryWarehouse = context.FactoryWarehouse.FirstOrDefault(o => o.CompanyID == 3 && o.FactoryWarehouseID == dbShowroomTransfer.FromWarehouseID);
                        dbDeliveryNote.CreatedBy = userId;
                        dbDeliveryNote.CreatedDate = System.DateTime.Now;
                        dbDeliveryNote.UpdatedBy = userId;
                        dbDeliveryNote.UpdatedDate = System.DateTime.Now;
                        //dbDeliveryNote.CreatedBy = (fromFactoryWarehouse != null) ? fromFactoryWarehouse.ResponsibleBy : null;
                        //dbDeliveryNote.CreatedDate = System.DateTime.Now;
                        //dbDeliveryNote.UpdatedBy = (fromFactoryWarehouse != null) ? fromFactoryWarehouse.ResponsibleBy : null;
                        //dbDeliveryNote.UpdatedDate = System.DateTime.Now;

                        foreach (var dtoDeliveryNoteDetail in dtoShowroomTransfer.ShowroomTransferDetails.ToList())
                        {
                            DeliveryNoteDetail dbDeliveryNoteDetail = new DeliveryNoteDetail();
                            dbDeliveryNoteDetail.ProductionItemID = dtoDeliveryNoteDetail.ProductionItemID;
                            dbDeliveryNoteDetail.FactoryWarehousePalletID = dtoDeliveryNoteDetail.FactoryWarehousePalletID;
                            dbDeliveryNoteDetail.FromFactoryWarehouseID = dtoShowroomTransfer.FromWarehouseID;
                            dbDeliveryNoteDetail.Qty = dtoDeliveryNoteDetail.Quantity;
                            dbDeliveryNoteDetail.QtyByUnit = dtoDeliveryNoteDetail.Quantity;
                            dbDeliveryNoteDetail.Description = dtoDeliveryNoteDetail.Remark;

                            dbDeliveryNote.DeliveryNoteDetail.Add(dbDeliveryNoteDetail);
                        }

                        context.DeliveryNoteDetail.Local.Where(o => o.DeliveryNote == null).ToList().ForEach(o => context.DeliveryNoteDetail.Remove(o));
                        context.SaveChanges();
                        if (dbDeliveryNote.DeliveryNoteUD == null || dbDeliveryNote.DeliveryNoteUD == "")
                        {
                            context.DeliveryNoteMng_function_GenerateDeliveryNoteUD(dbDeliveryNote.DeliveryNoteID, companyId, dbDeliveryNote.DeliveryNoteDate.Value.Year, dbDeliveryNote.DeliveryNoteDate.Value.Month);
                        }
                    }

                    dtoItem = GetData(dbShowroomTransfer.ShowroomTransferID, out notification);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;

                return false;
            }
        }

        public DTO.InitFormDTO GetInitData(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormDTO data = new DTO.InitFormDTO();

            try
            {
                Framework.DAL.DataFactory frameworkFactory = new Framework.DAL.DataFactory();
                int? companyId = frameworkFactory.GetCompanyID(userId);

                using (var context = CreateContext())
                {
                    var dbFactoryWarehouses = context.ShowroomTransferMng_FactoryWarehouse_View.Where(o => o.CompanyID == companyId);
                    data.FactoryWarehouses = dataConverter.DB2DTO_SupportFactoryWarehouse(dbFactoryWarehouses.ToList());

                    var dbFactoryWarehousePallets = context.ShowroomTransferMng_FactoryWarehousePallet_View.Where(o => o.CompanyID == companyId);
                    data.FactoryWarehousePallets = dataConverter.DB2DTO_SupportFactoryWarehousePallet(dbFactoryWarehousePallets.ToList());
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }

        public ShowroomTransferMngEntities CreateContext()
        {
            return new ShowroomTransferMngEntities(Helper.CreateEntityConnectionString("DAL.ShowroomTransferMngModel"));
        }
    }
}
