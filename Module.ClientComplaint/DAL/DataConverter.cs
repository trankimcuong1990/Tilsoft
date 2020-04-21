using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Library;

namespace Module.ClientComplaint.DAL
{
    internal class DataConverter
    {
        private static readonly Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private const string DateTimeFormat = "dd/MM/yyyy";
        private System.Globalization.CultureInfo nl = new System.Globalization.CultureInfo("nl-NL");
        private DateTime tmpDate;

        public DataConverter()
        {
            var mapName = GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // from db to dto
                Mapper.CreateMap<ClientComplaint_ClientComplaintSearchResult_View, DTO.ClientComplaintSearchResult>()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.DeliveryDateClient, o => o.ResolveUsing(s => s.DeliveryDateClient.HasValue ? s.DeliveryDateClient.Value.ToString(DateTimeFormat) : ""))
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ClientComplaint_ClientComplaint_View, DTO.ClientComplaint>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.ReceivedDate, o => o.ResolveUsing(s => s.ReceivedDate.HasValue ? s.ReceivedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.ApprovedDate, o => o.ResolveUsing(s => s.ApprovedDate.HasValue ? s.ApprovedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.DeliveryDateClient, o => o.ResolveUsing(s => s.DeliveryDateClient.HasValue ? s.DeliveryDateClient.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.ClientComplaintUsers, o => o.MapFrom(s => s.ClientComplaint_ClientComplaintUser_View))
                    .ForMember(d => d.ClientComplaintItems, o => o.MapFrom(s => s.ClientComplaint_ClientComplaintItem_View))
                    .ForMember(d => d.ClientComplaintCommunications, o => o.MapFrom(s => s.ClientComplaint_ClientComplaintCommunication_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ClientComplaint_FactoryOrderDetailItemsByClientSeason_SearchView, DTO.FactoryOrderDetailItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString(DateTimeFormat) : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ClientComplaint_SearchProformaInvoiceByClient_SearchView, DTO.ProformaInvoiceItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString(DateTimeFormat) : ""))
                    .ForMember(d => d.PIReceivedDate, o => o.ResolveUsing(s => s.PIReceivedDate.HasValue ? s.PIReceivedDate.Value.ToString(DateTimeFormat) : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                Mapper.CreateMap<ClientComplaint_ClientComplaintUser_View, DTO.ClientComplaintUser>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                
                Mapper.CreateMap<ClientComplaint_ClientComplaintItem_View, DTO.ClientComplaintItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientComplaintItemImages, o => o.MapFrom(s => s.ClientComplaint_ClientComplaintItemImage_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ClientComplaint_ClientComplaintCommunication_View, DTO.ClientComplaintCommunication>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString(DateTimeFormat + " HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ClientComplaint_ClientComplaintItemImage_View, DTO.ClientComplaintItemImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.ThumbnailLocation) ? "" : FrameworkSetting.Setting.ThumbnailUrl + s.ThumbnailLocation)))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => (string.IsNullOrEmpty(s.FileLocation) ? "" : FrameworkSetting.Setting.FullSizeUrl + s.FileLocation)))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                Mapper.CreateMap<ClientComplaint_SearchProformaInvoiceShipmentStatus_SearchView, DTO.SearchProformaInvoiceShipmentStatus>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.LDS, o => o.ResolveUsing(s => s.LDS.HasValue ? s.LDS.Value.ToString(DateTimeFormat) : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                
                // from dto to db
                Mapper.CreateMap<DTO.ClientComplaint, ClientComplaint>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ReceivedDate, o => o.Ignore())
                    .ForMember(d => d.ApprovedDate, o => o.Ignore())
                    .ForMember(d => d.DeliveryDateClient, o => o.Ignore())
                    .ForMember(d => d.ClientComplaintItems, o => o.Ignore())
                    .ForMember(d => d.ClientComplaintUsers, o => o.Ignore())
                    .ForMember(d => d.ClientComplaintCommunications, o => o.Ignore())
                    .ForMember(d => d.ClientComplaintID, o => o.Ignore());

                Mapper.CreateMap<DTO.ClientComplaintItem, ClientComplaintItem>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientComplaintItemImages, o => o.Ignore())
                    .ForMember(d => d.QuantityOfComplaint, o => o.ResolveUsing(s => s.QuantityOfComplaint ?? 0))
                    .ForMember(d => d.ClientComplaintItemID, o => o.Ignore());

                Mapper.CreateMap<DTO.ClientComplaintUser, ClientComplaintUser>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientComplaintUserID, o => o.Ignore());

                Mapper.CreateMap<DTO.ClientComplaintCommunication, ClientComplaintCommunication>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CreatedDate, o => o.Ignore()) 
                    .ForMember(d => d.ClientComplaintCommunicationID, o => o.Ignore());

                Mapper.CreateMap<DTO.ClientComplaintItemImage, ClientComplaintItemImage>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ClientComplaintItemImageID, o => o.Ignore());
                

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ClientComplaintSearchResult> DB2DTO_ClientComplaintSearchResultList(List<ClientComplaint_ClientComplaintSearchResult_View> dbItems)
        {
            return Mapper.Map<List<ClientComplaint_ClientComplaintSearchResult_View>, List<DTO.ClientComplaintSearchResult>>(dbItems);
        }

        public DTO.ClientComplaint DB2DTO_ClientComplaint(ClientComplaint_ClientComplaint_View dbItem)
        {
            return Mapper.Map<ClientComplaint_ClientComplaint_View, DTO.ClientComplaint>(dbItem);
        }

        public void DTO2DB_ClientComplaint(DTO.ClientComplaint dtoItem, ref ClientComplaint dbItem, int createdBy, string tempFolder)
        {
            // client complain items 
            foreach (ClientComplaintItem dbComplaintItem in dbItem.ClientComplaintItems.ToArray())
            {
                if (!dtoItem.ClientComplaintItems.Select(o => o.ClientComplaintItemID).Contains(dbComplaintItem.ClientComplaintItemID))
                {
                    // remove image
                    foreach (ClientComplaintItemImage dbComplaintImage in dbComplaintItem.ClientComplaintItemImages.ToArray())
                    {
                        if (!string.IsNullOrEmpty(dbComplaintImage.ImageFile))
                        {
                            // remove image file
                            fwFactory.RemoveImageFile(dbComplaintImage.ImageFile);
                        }
                        dbComplaintItem.ClientComplaintItemImages.Remove(dbComplaintImage);
                    }
                    dbItem.ClientComplaintItems.Remove(dbComplaintItem);
                }
            }


            ////ClientComplaintItems
            //var willDeletedItems = new List<ClientComplaintItem>();
            //List<ClientComplaintItemImage> willDeletedItemImages;

            if (dtoItem.ClientComplaintItems != null && dtoItem.ClientComplaintItems.Count > 0)
            {
                ////CHECK
                //foreach (var dbComplaintItem in dbItem.ClientComplaintItems)
                //{
                //    //DB NOT EXIST IN DTO
                //    if (!dtoItem.ClientComplaintItems.Select(s => s.ClientComplaintItemID).Contains(dbComplaintItem.ClientComplaintItemID))
                //    {
                //        willDeletedItems.Add(dbComplaintItem);
                //    }
                //    else //DB IS EXIST IN DB
                //    {
                //        willDeletedItemImages = new List<ClientComplaintItemImage>();
                //        foreach (var dbComplaintItemImage in dbComplaintItem.ClientComplaintItemImages)
                //        {
                //            var clientComplaintItem = dtoItem.ClientComplaintItems
                //                .FirstOrDefault(o => o.ClientComplaintItemID == dbComplaintItem.ClientComplaintItemID);

                //            if (clientComplaintItem != null && !clientComplaintItem
                //                    .ClientComplaintItemImages.Select(x => x.ClientComplaintItemImageID)
                //                    .Contains(dbComplaintItemImage.ClientComplaintItemImageID))
                //            {
                //                willDeletedItemImages.Add(dbComplaintItemImage);
                //            }
                //        }

                //        foreach (var dbItemImage in willDeletedItemImages)
                //        {
                //            dbComplaintItem.ClientComplaintItemImages.Remove(dbItemImage);
                //        }
                //    }
                //}

                //// Delete Parent and child items
                //foreach (var dbParentItem in willDeletedItems)
                //{
                //    var deletedChildItems = dbParentItem.ClientComplaintItemImages.ToList();

                //    foreach (var item in deletedChildItems)
                //    {
                //        var clientComplaintItem = dbItem.ClientComplaintItems
                //            .FirstOrDefault(o => o.ClientComplaintItemID == dbParentItem.ClientComplaintItemID);

                //        if (clientComplaintItem != null)
                //            clientComplaintItem.ClientComplaintItemImages.Remove(item);
                //    }
                //    dbItem.ClientComplaintItems.Remove(dbParentItem);
                //}

                //MAP
                foreach (var dtoItemM in dtoItem.ClientComplaintItems)
                {
                    ClientComplaintItem _dbItem;
                    ClientComplaintItemImage _dbItemImage;

                    if (dtoItemM.ClientComplaintItemID < 0)
                    {
                        _dbItem = new ClientComplaintItem();

                        if ((dtoItemM.ClientComplaintItemImages != null) && (dtoItemM.ClientComplaintItemImages.Count > 0))
                        {
                            foreach (var dtoItemImageM in dtoItemM.ClientComplaintItemImages)
                            {
                                // create new
                                if (dtoItemImageM.ImageFile_HasChange.HasValue && dtoItemImageM.ImageFile_HasChange.Value)
                                {
                                    dtoItemImageM.ImageFile = fwFactory.CreateFilePointer(tempFolder, dtoItemImageM.ImageFile_NewFile, dtoItemImageM.ImageFile);
                                }

                                _dbItemImage = new ClientComplaintItemImage();
                                _dbItem.ClientComplaintItemImages.Add(_dbItemImage);
                                AutoMapper.Mapper.Map<DTO.ClientComplaintItemImage, ClientComplaintItemImage>(dtoItemImageM, _dbItemImage);
                            }
                        }

                        dbItem.ClientComplaintItems.Add(_dbItem);
                    }
                    else
                    {
                        _dbItem = dbItem.ClientComplaintItems.FirstOrDefault(o => o.ClientComplaintItemID == dtoItemM.ClientComplaintItemID);

                        if (_dbItem != null && 
                            dtoItemM.ClientComplaintItemImages != null && 
                            dtoItemM.ClientComplaintItemImages.Count > 0)
                        {
                            foreach (var itemImage in dtoItemM.ClientComplaintItemImages)
                            {
                                //modify dto image field
                                if (itemImage.ImageFile_HasChange.HasValue && itemImage.ImageFile_HasChange.Value)
                                {
                                    itemImage.ImageFile = fwFactory.CreateFilePointer(tempFolder, itemImage.ImageFile_NewFile, itemImage.ImageFile);
                                }

                                if (itemImage.ClientComplaintItemImageID < 0)
                                {
                                    _dbItemImage = new ClientComplaintItemImage();
                                    _dbItem.ClientComplaintItemImages.Add(_dbItemImage);
                                }
                                else
                                {
                                    _dbItemImage = _dbItem.ClientComplaintItemImages
                                        .FirstOrDefault(o => o.ClientComplaintItemImageID == itemImage.ClientComplaintItemImageID);
                                }

                                if (_dbItemImage != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.ClientComplaintItemImage, ClientComplaintItemImage>(itemImage, _dbItemImage);
                                }
                            }
                        }
                    }

                    if (_dbItem != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ClientComplaintItem, ClientComplaintItem>(dtoItemM, _dbItem);
                    }
                }
                
            }

            // ClientComplaintUsers
            if (dtoItem.ClientComplaintUsers != null && dtoItem.ClientComplaintUsers.Count > 0)
            {
                foreach (var item in dbItem.ClientComplaintUsers.ToArray())
                {
                    if (!dtoItem.ClientComplaintUsers.Select(o => o.ClientComplaintUserID).Contains(item.ClientComplaintUserID))
                    {
                        dbItem.ClientComplaintUsers.Remove(item);
                    }
                }

                //map child row
                foreach (var item in dtoItem.ClientComplaintUsers)
                {
                    ClientComplaintUser dbClientComplaintUser;
                    if (item.ClientComplaintUserID <= 0)
                    {
                        dbClientComplaintUser = new ClientComplaintUser();
                        dbItem.ClientComplaintUsers.Add(dbClientComplaintUser);
                    }
                    else
                    {
                        dbClientComplaintUser = dbItem.ClientComplaintUsers.FirstOrDefault(o => o.ClientComplaintUserID == item.ClientComplaintUserID);
                    }

                    if (dbClientComplaintUser != null)
                    {
                        Mapper.Map(item, dbClientComplaintUser);
                    }
                }
            }

            // ClientComplaintCommunications
            if (dtoItem.ClientComplaintCommunications != null && dtoItem.ClientComplaintCommunications.Count > 0)
            {
                foreach (var item in dbItem.ClientComplaintCommunications.ToArray())
                {
                    if (!dtoItem.ClientComplaintCommunications.Select(o => o.ClientComplaintCommunicationID).Contains(item.ClientComplaintCommunicationID))
                    {
                        dbItem.ClientComplaintCommunications.Remove(item);
                    }
                }

                //map child row
                foreach (var item in dtoItem.ClientComplaintCommunications)
                {
                    ClientComplaintCommunication dbClientComplaintCommunication;
                    if (item.ClientComplaintCommunicationID <= 0)
                    {
                        dbClientComplaintCommunication = new ClientComplaintCommunication();                        
                        dbItem.ClientComplaintCommunications.Add(dbClientComplaintCommunication);                        
                        dbClientComplaintCommunication.CreatedBy = createdBy;

                        if (DateTime.TryParse(item.CreatedDate, nl, System.Globalization.DateTimeStyles.None, out tmpDate))
                        {
                            dbClientComplaintCommunication.CreatedDate = tmpDate;
                        }                        
                    }
                    else
                    {
                        dbClientComplaintCommunication = dbItem.ClientComplaintCommunications.FirstOrDefault(o => o.ClientComplaintCommunicationID == item.ClientComplaintCommunicationID);
                    }

                    if (dbClientComplaintCommunication != null)
                    {
                        Mapper.Map(item, dbClientComplaintCommunication);
                    }
                }
            }
            
            Mapper.Map(dtoItem, dbItem);
            dbItem.UpdatedDate = DateTime.Now;
            dbItem.DeliveryDateClient = dtoItem.DeliveryDateClient.ConvertStringToDateTime();
        }
        

        public List<DTO.FactoryOrderDetailItem> DB2DTO_ClientComplaintFactoryOrderDetailItemList(List<ClientComplaint_FactoryOrderDetailItemsByClientSeason_SearchView> dbItems)
        {
            return Mapper.Map<List<ClientComplaint_FactoryOrderDetailItemsByClientSeason_SearchView>, List<DTO.FactoryOrderDetailItem>>(dbItems);
        }

        public List<DTO.ProformaInvoiceItem> DB2DTO_ClientComplaintProformaInvoiceItemList(List<ClientComplaint_SearchProformaInvoiceByClient_SearchView> dbItems)
        {
            return Mapper.Map<List<ClientComplaint_SearchProformaInvoiceByClient_SearchView>, List<DTO.ProformaInvoiceItem>>(dbItems);
        }
    }
}
