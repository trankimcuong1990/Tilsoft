using Library;
using System.Linq;

namespace Module.BifaCompanyMng.DAL
{
    internal class DataConverter
    {
        public DataConverter()
        {
            if (!FrameworkSetting.Setting.Maps.Contains("BifaCompanyMng"))
            {
                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaCompanySearchResult_View, DTO.BifaCompanySearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.ThumbnailLocation) ? (FrameworkSetting.Setting.MediaThumbnailUrl + s.ThumbnailLocation) : "no-image.jpg"))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.FileLocation) ? (FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation) : "no-image.jpg"))
                    .ForMember(d => d.FoundingDate, o => o.ResolveUsing(s => s.FoundingDate.HasValue ? (s.FoundingDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.JoinedDate, o => o.ResolveUsing(s => s.JoinedDate.HasValue ? (s.JoinedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_SupportBifaCity_View, DTO.BifaCity>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_SupportBifaIndustry_View, DTO.BifaIndustry>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_SupportBifaClub_View, DTO.BifaClub>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_BifaPhoneType_View, DTO.BifaPhoneType>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<SupportMng_BifaPositionGroup_View, DTO.BifaPositionGroup>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaCompany_View, DTO.BifaCompany>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.Logo_DisplayUrl, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.ThumbnailLocation) ? (FrameworkSetting.Setting.MediaThumbnailUrl + s.ThumbnailLocation) : "no-image.jpg"))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.ThumbnailLocation) ? (FrameworkSetting.Setting.MediaThumbnailUrl + s.ThumbnailLocation) : "no-image.jpg"))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.FileLocation) ? (FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation) : "no-image.jpg"))
                    .ForMember(d => d.FoundingDate, o => o.ResolveUsing(s => s.FoundingDate.HasValue ? (s.FoundingDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.JoinedDate, o => o.ResolveUsing(s => s.JoinedDate.HasValue ? (s.JoinedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.BifaAddresses, o => o.MapFrom(s => s.BifaCompanyMng_BifaAddress_View))
                    .ForMember(d => d.BifaClubMembers, o => o.MapFrom(s => s.BifaCompanyMng_BifaClubMember_View))
                    .ForMember(d => d.BifaEmailAddresses, o => o.MapFrom(s => s.BifaCompanyMng_BifaEmailAddress_View))
                    .ForMember(d => d.BifaPersons, o => o.MapFrom(s => s.BifaCompanyMng_BifaPerson_View))
                    .ForMember(d => d.BifaTelephones, o => o.MapFrom(s => s.BifaCompanyMng_BifaTelephone_View))
                    .ForMember(d => d.BifaEventParticipants, o => o.MapFrom(s => s.BifaCompanyMng_BifaEventParticipant_View))
                    .ForMember(d => d.BifaEvents, o => o.MapFrom(s => s.BifaCompanyMng_BifaEvent_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaAddress_View, DTO.BifaAddress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaClubMember_View, DTO.BifaClubMember>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.JoinedDate, o => o.ResolveUsing(s => s.JoinedDate.HasValue ? (s.JoinedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaEmailAddress_View, DTO.BifaEmailAddress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaPerson_View, DTO.BifaPerson>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.DateOfBirth, o => o.ResolveUsing(s => s.DateOfBirth.HasValue ? (s.DateOfBirth.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForMember(d => d.BifaEmailAddresses, o => o.MapFrom(s => s.BifaCompanyMng_BifaEmailAddress_View))
                    .ForMember(d => d.BifaTelephones, o => o.MapFrom(s => s.BifaCompanyMng_BifaTelephone_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaTelephone_View, DTO.BifaTelephone>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? (s.UpdatedDate.Value.ToString("dd/MM/yyyy")) : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaEvent_View, DTO.BifaEvent>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.StartDate, o => o.ResolveUsing(s => s.StartDate.HasValue ? s.StartDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.EndDate, o => o.ResolveUsing(s => s.EndDate.HasValue ? s.EndDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.BifaEventParticipants, o => o.MapFrom(s => s.BifaCompanyMng_BifaEventParticipant_View))
                    .ForMember(d => d.BifaEventFiles, o => o.MapFrom(s => s.BifaCompanyMng_BifaEventFile_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaEventParticipant_View, DTO.BifaEventParticipant>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<BifaCompanyMng_BifaEventFile_View, DTO.BifaEventFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : null))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.ThumbnailLocation) ? (FrameworkSetting.Setting.MediaThumbnailUrl + s.ThumbnailLocation) : "no-image.jpg"))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => !string.IsNullOrEmpty(s.FileLocation) ? (FrameworkSetting.Setting.MediaFullSizeUrl + s.FileLocation) : "no-image.jpg"))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.BifaCompany, BifaCompany>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.FoundingDate, o => o.Ignore())
                    .ForMember(d => d.JoinedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.BifaAddress, o => o.Ignore())
                    .ForMember(d => d.BifaClubMember, o => o.Ignore())
                    .ForMember(d => d.BifaEmailAddress, o => o.Ignore())
                    .ForMember(d => d.BifaPerson, o => o.Ignore())
                    .ForMember(d => d.BifaTelephone, o => o.Ignore())
                    .ForMember(d => d.BifaEvent, o => o.Ignore())
                    .ForMember(d => d.BifaEventParticipant, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaAddress, BifaAddress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaAddressID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaClubMember, BifaClubMember>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaClubMemberID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.JoinedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaClub, BifaClub>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaClubID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaEmailAddress, BifaEmailAddress>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaEmailAddressID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.BifaPersonID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaPerson, BifaPerson>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaPersonID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.BifaEmailAddress, o => o.Ignore())
                    .ForMember(d => d.BifaTelephone, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaTelephone, BifaTelephone>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaTelephoneID, o => o.Ignore())
                    .ForMember(d => d.BifaPersonID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaPerson, BifaPerson>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaPersonID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.DateOfBirth, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaEvent, BifaEvent>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaEventID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.StartDate, o => o.Ignore())
                    .ForMember(d => d.EndDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.BifaEventFile, o => o.Ignore())
                    .ForMember(d => d.BifaEventParticipant, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaEventParticipant, BifaEventParticipant>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaEventParticipantID, o => o.Ignore())
                    .ForMember(d => d.BifaEventID, o => o.Ignore())
                    .ForMember(d => d.BifaCompanyID, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                AutoMapper.Mapper.CreateMap<DTO.BifaEventFile, BifaEventFile>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.BifaEventFileID, o => o.Ignore())
                    .ForMember(d => d.BifaEventID, o => o.Ignore())
                    .ForMember(d => d.FileUD, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore());

                FrameworkSetting.Setting.Maps.Add("BifaCompanyMng");
            }
        }

        public System.Collections.Generic.List<DTO.BifaCity> DB2DTO_SupportBifaCities(System.Collections.Generic.List<BifaCompanyMng_SupportBifaCity_View> dbItems)
        {
            return AutoMapper.Mapper.Map<System.Collections.Generic.List<BifaCompanyMng_SupportBifaCity_View>, System.Collections.Generic.List<DTO.BifaCity>>(dbItems);
        }

        public System.Collections.Generic.List<DTO.BifaIndustry> DB2DTO_SupportBifaIndustries(System.Collections.Generic.List<BifaCompanyMng_SupportBifaIndustry_View> dbItems)
        {
            return AutoMapper.Mapper.Map<System.Collections.Generic.List<BifaCompanyMng_SupportBifaIndustry_View>, System.Collections.Generic.List<DTO.BifaIndustry>>(dbItems);
        }

        public System.Collections.Generic.List<DTO.BifaClub> DB2DTO_SupportBifaClubs(System.Collections.Generic.List<BifaCompanyMng_SupportBifaClub_View> dbItems)
        {
            return AutoMapper.Mapper.Map<System.Collections.Generic.List<BifaCompanyMng_SupportBifaClub_View>, System.Collections.Generic.List<DTO.BifaClub>>(dbItems);
        }

        public System.Collections.Generic.List<DTO.BifaPhoneType> DB2DTO_SupportPhoneType(System.Collections.Generic.List<SupportMng_BifaPhoneType_View> dbItems)
        {
            return AutoMapper.Mapper.Map<System.Collections.Generic.List<SupportMng_BifaPhoneType_View>, System.Collections.Generic.List<DTO.BifaPhoneType>>(dbItems);
        }

        public System.Collections.Generic.List<DTO.BifaPositionGroup> DB2DTO_SupportPositionGroup(System.Collections.Generic.List<SupportMng_BifaPositionGroup_View> dbItems)
        {
            return AutoMapper.Mapper.Map<System.Collections.Generic.List<SupportMng_BifaPositionGroup_View>, System.Collections.Generic.List<DTO.BifaPositionGroup>>(dbItems);
        }

        public DTO.BifaCompany DB2DTO_BifaCompany(BifaCompanyMng_BifaCompany_View dbItem)
        {
            return AutoMapper.Mapper.Map<BifaCompanyMng_BifaCompany_View, DTO.BifaCompany>(dbItem);
        }

        public DTO.BifaAddress DB2DTO_BifaAddress(BifaCompanyMng_BifaAddress_View dbItem)
        {
            return AutoMapper.Mapper.Map<BifaCompanyMng_BifaAddress_View, DTO.BifaAddress>(dbItem);
        }

        public void DTO2DB_BifaCompany(DTO.BifaCompany dtoItem, ref BifaCompany dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaCompany, BifaCompany>(dtoItem, dbItem);

            dbItem.FoundingDate = dtoItem.FoundingDate.ConvertStringToDateTime();
            dbItem.JoinedDate = dtoItem.JoinedDate.ConvertStringToDateTime();
        }

        public void DTO2DB_BifaAddress(DTO.BifaAddress dtoItem, ref BifaAddress dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaAddress, BifaAddress>(dtoItem, dbItem);
        }

        public void DTO2DB_BifaClubMember(DTO.BifaClubMember dtoItem, ref BifaClubMember dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaClubMember, BifaClubMember>(dtoItem, dbItem);
        }

        public void DTO2DB_BifaEmailAddress(DTO.BifaEmailAddress dtoItem, ref BifaEmailAddress dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaEmailAddress, BifaEmailAddress>(dtoItem, dbItem);
        }

        public void DTO2DB_BifaTelephone(int userID, DTO.BifaTelephone dtoItem, ref BifaTelephone dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaTelephone, BifaTelephone>(dtoItem, dbItem);

            dbItem.BifaCompanyID = dtoItem.BifaCompanyID;
            dbItem.UpdatedBy = userID;
            dbItem.UpdatedDate = System.DateTime.Now;
        }

        public void DTO2DB_BifaPerson(int userID, DTO.BifaPerson dtoItem, ref BifaPerson dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaPerson, BifaPerson>(dtoItem, dbItem);
            dbItem.DateOfBirth = dtoItem.DateOfBirth.ConvertStringToDateTime();
            dbItem.UpdatedBy = userID;
            dbItem.UpdatedDate = System.DateTime.Now;
            dbItem.BifaCompanyID = dtoItem.BifaCompanyID;

            if (dtoItem.BifaEmailAddresses != null)
            {
                foreach (var item in dbItem.BifaEmailAddress.ToArray())
                {
                    if (!dtoItem.BifaEmailAddresses.Select(s => s.BifaEmailAddressID).Contains(item.BifaEmailAddressID))
                    {
                        dbItem.BifaEmailAddress.Remove(item);
                    }
                }

                foreach (var dtoEmailAddress in dtoItem.BifaEmailAddresses.ToArray())
                {
                    BifaEmailAddress dbEmailAddress;

                    if (dtoEmailAddress.BifaEmailAddressID < 0)
                    {
                        dbEmailAddress = new BifaEmailAddress();
                        dbItem.BifaEmailAddress.Add(dbEmailAddress);
                    }
                    else
                    {
                        dbEmailAddress = dbItem.BifaEmailAddress.FirstOrDefault(s => s.BifaEmailAddressID == dtoEmailAddress.BifaEmailAddressID);
                    }

                    if (dbEmailAddress != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BifaEmailAddress, BifaEmailAddress>(dtoEmailAddress, dbEmailAddress);
                        dbEmailAddress.UpdatedBy = userID;
                        dbEmailAddress.UpdatedDate = System.DateTime.Now;
                    }
                }
            }

            if (dtoItem.BifaTelephones != null)
            {
                foreach (var item in dbItem.BifaTelephone.ToArray())
                {
                    if (!dtoItem.BifaTelephones.Select(s => s.BifaTelephoneID).Contains(item.BifaTelephoneID))
                    {
                        dbItem.BifaTelephone.Remove(item);
                    }
                }

                foreach (var dtoTelephone in dtoItem.BifaTelephones.ToArray())
                {
                    BifaTelephone dbTelephone;

                    if (dtoTelephone.BifaTelephoneID < 0)
                    {
                        dbTelephone = new BifaTelephone();
                        dbItem.BifaTelephone.Add(dbTelephone);
                    }
                    else
                    {
                        dbTelephone = dbItem.BifaTelephone.FirstOrDefault(s => s.BifaTelephoneID == dtoTelephone.BifaTelephoneID);
                    }

                    if (dbTelephone != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BifaTelephone, BifaTelephone>(dtoTelephone, dbTelephone);
                        dbTelephone.UpdatedBy = userID;
                        dbTelephone.UpdatedDate = System.DateTime.Now;
                    }
                }
            }
        }

        public void DTO2DB_BifaEvent(int userID, DTO.BifaEvent dtoItem, ref BifaEvent dbItem)
        {
            AutoMapper.Mapper.Map<DTO.BifaEvent, BifaEvent>(dtoItem, dbItem);

            dbItem.BifaCompanyID = dtoItem.BifaCompanyID;
            dbItem.BifaEventUD = dtoItem.BifaEventUD;
            dbItem.StartDate = dtoItem.StartDate.ConvertStringToDateTime();
            dbItem.EndDate = dtoItem.EndDate.ConvertStringToDateTime();
            dbItem.UpdatedBy = userID;
            dbItem.UpdatedDate = System.DateTime.Now;

            if (dtoItem.BifaEventParticipants != null)
            {
                foreach (var item in dbItem.BifaEventParticipant.ToArray())
                {
                    if (!dtoItem.BifaEventParticipants.Select(s => s.BifaEventParticipantID).Contains(item.BifaEventParticipantID))
                    {
                        dbItem.BifaEventParticipant.Remove(item);
                    }
                }

                foreach (var item in dtoItem.BifaEventParticipants.ToArray())
                {
                    BifaEventParticipant dbItemDetail;

                    if (item.BifaEventParticipantID < 0)
                    {
                        dbItemDetail = new BifaEventParticipant();
                        dbItem.BifaEventParticipant.Add(dbItemDetail);
                    }
                    else
                    {
                        dbItemDetail = dbItem.BifaEventParticipant.FirstOrDefault(s => s.BifaEventParticipantID == item.BifaEventParticipantID);
                    }

                    if (dbItemDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BifaEventParticipant, BifaEventParticipant>(item, dbItemDetail);

                        dbItemDetail.UpdatedBy = userID;
                        dbItemDetail.UpdatedDate = System.DateTime.Now;
                    }
                }
            }

            if (dtoItem.BifaEventFiles != null)
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();

                foreach (var item in dbItem.BifaEventFile.ToList())
                {
                    if (!dtoItem.BifaEventFiles.Select(s => s.BifaEventFileID).Contains(item.BifaEventFileID))
                    {
                        dbItem.BifaEventFile.Remove(item);
                    }
                }

                foreach (var item in dtoItem.BifaEventFiles.ToList())
                {
                    BifaEventFile dbItemDetail;

                    if (item.BifaEventFileID < 0)
                    {
                        dbItemDetail = new BifaEventFile();
                        dbItem.BifaEventFile.Add(dbItemDetail);
                    }
                    else
                    {
                        dbItemDetail = dbItem.BifaEventFile.FirstOrDefault(s => s.BifaEventFileID == item.BifaEventFileID);
                    }

                    if (dbItemDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.BifaEventFile, BifaEventFile>(item, dbItemDetail);

                        if (item.HasChange)
                        {
                            if (string.IsNullOrEmpty(item.NewFile))
                            {
                                fwFactory.RemoveImageFile(item.FileUD);
                            }
                            else
                            {
                                dbItemDetail.FileUD = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userID.ToString() + @"\", item.NewFile, item.FileUD);
                            }
                        }
                    }
                }
            }
        }
    }
}
