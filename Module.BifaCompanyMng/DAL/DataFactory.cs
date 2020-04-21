using Library;
using System.Linq;

namespace Module.BifaCompanyMng.DAL
{
    internal class DataFactory
    {
        private DataConverter converter = new DataConverter();

        protected BifaCompanyMngEntities CreateContext()
        {
            return new BifaCompanyMngEntities(Helper.CreateEntityConnectionString("DAL.BifaCompanyMngModel"));
        }

        public object GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.InitFormData data = new DTO.InitFormData();

            try
            {
                using (var context = CreateContext())
                {
                    data.BifaCities = converter.DB2DTO_SupportBifaCities(context.BifaCompanyMng_SupportBifaCity_View.ToList());
                    data.BifaIndustries = converter.DB2DTO_SupportBifaIndustries(context.BifaCompanyMng_SupportBifaIndustry_View.ToList());
                    data.BifaClubs = converter.DB2DTO_SupportBifaClubs(context.BifaCompanyMng_SupportBifaClub_View.ToList());
                    data.BifaPhoneTypes = converter.DB2DTO_SupportPhoneType(context.SupportMng_BifaPhoneType_View.ToList());
                    data.BifaPositionGroups = converter.DB2DTO_SupportPositionGroup(context.SupportMng_BifaPositionGroup_View.ToList());
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public object GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.EditFormData data = new DTO.EditFormData();

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.BifaCompany = converter.DB2DTO_BifaCompany(context.BifaCompanyMng_BifaCompany_View.FirstOrDefault(o => o.BifaCompanyID == id));
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
        public object GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            totalRows = 0;
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.SearchFormData data = new DTO.SearchFormData();

            try
            {
                string bifaCompany = filters.ContainsKey("bifaCompany") && filters["bifaCompany"] != null && !string.IsNullOrEmpty(filters["bifaCompany"].ToString()) ? filters["bifaCompany"].ToString() : null;
                string taxCode = filters.ContainsKey("taxCode") && filters["taxCode"] != null && !string.IsNullOrEmpty(filters["taxCode"].ToString()) ? filters["taxCode"].ToString() : null;
                string bifaIndustry = filters.ContainsKey("bifaIndustry") && filters["bifaIndustry"] != null && !string.IsNullOrEmpty(filters["bifaIndustry"].ToString()) ? filters["bifaIndustry"].ToString() : null;
                string bifaCity = filters.ContainsKey("bifaCity") && filters["bifaCity"] != null && !string.IsNullOrEmpty(filters["bifaCity"].ToString()) ? filters["bifaCity"].ToString() : null;
                bool? isActive = filters.ContainsKey("isActive") && filters["isActive"] != null && !string.IsNullOrEmpty(filters["isActive"].ToString()) ? (bool?) System.Convert.ToBoolean(filters["isActive"].ToString()) : null;

                using (var context = CreateContext())
                {
                    totalRows = context.BifaCompanyMng_function_BifaCompanySearchResult(bifaCompany, taxCode, bifaIndustry, bifaCity, isActive, orderBy, orderDirection).Count();
                    data.Data = AutoMapper.Mapper.Map<System.Collections.Generic.List<BifaCompanyMng_BifaCompanySearchResult_View>, System.Collections.Generic.List<DTO.BifaCompanySearchResult>>(context.BifaCompanyMng_function_BifaCompanySearchResult(bifaCompany, taxCode, bifaIndustry, bifaCity, isActive, orderBy, orderDirection).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return data;
        }
        public bool UpdateData(int userID, int id, ref object viewItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaCompany dtoItem = ((Newtonsoft.Json.Linq.JObject)viewItem).ToObject<DTO.BifaCompany>();

            try
            {
                BifaCompany dbItem;

                using (var context = CreateContext())
                {
                    if (id == 0)
                    {
                        dbItem = new BifaCompany();
                        context.BifaCompany.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaCompany.FirstOrDefault(o => o.BifaCompanyID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa company!";

                        return false;
                    }
                    else
                    {
                        // Process upload logo Bifa company
                        Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                        if (dtoItem.Logo_HasChange)
                        {
                            if (string.IsNullOrEmpty(dtoItem.Logo_NewFile))
                            {
                                fwFactory.RemoveImageFile(dtoItem.Logo);
                            }
                            else
                            {
                                dtoItem.Logo = fwFactory.CreateFilePointer(FrameworkSetting.Setting.AbsoluteUserTempFolder + userID.ToString() + @"\", dtoItem.Logo_NewFile, dtoItem.Logo);
                            }
                        }

                        converter.DTO2DB_BifaCompany(dtoItem, ref dbItem);
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = System.DateTime.Now;

                        context.SaveChanges();

                        viewItem = GetData(dbItem.BifaCompanyID, out notification);

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

        public object UpdateBifaCity(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? System.Convert.ToInt32(filters["id"].ToString().Trim()) : 0;
            DTO.BifaCity dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaCity>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaCity dbItem;

                    if (id == 0)
                    {
                        dbItem = new BifaCity();

                        context.BifaCity.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaCity.FirstOrDefault(o => o.BifaCityID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa city!";
                    }
                    else
                    {
                        dbItem.BifaCityNM = dtoItem.BifaCityNM;

                        context.SaveChanges();

                        dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_SupportBifaCity_View, DTO.BifaCity>(context.BifaCompanyMng_SupportBifaCity_View.FirstOrDefault(o => o.BifaCityID == dbItem.BifaCityID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteBifaCity(int bifaCityID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaCity.FirstOrDefault(o => o.BifaCityID == bifaCityID);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa city";

                        return false;
                    }

                    context.BifaCity.Remove(dbItem);

                    context.SaveChanges();

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

        public object UpdateBifaIndustry(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? System.Convert.ToInt32(filters["id"].ToString().Trim()) : 0;
            DTO.BifaIndustry dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaIndustry>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaIndustry dbItem;

                    if (id == 0)
                    {
                        dbItem = new BifaIndustry();

                        context.BifaIndustry.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaIndustry.FirstOrDefault(o => o.BifaIndustryID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa city!";
                    }
                    else
                    {
                        dbItem.BifaIndustryNM = dtoItem.BifaIndustryNM;

                        context.SaveChanges();

                        dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_SupportBifaIndustry_View, DTO.BifaIndustry>(context.BifaCompanyMng_SupportBifaIndustry_View.FirstOrDefault(o => o.BifaIndustryID == dbItem.BifaIndustryID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteBifaIndustry(int bifaIndustryID, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaIndustry.FirstOrDefault(o => o.BifaIndustryID == bifaIndustryID);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa industry";

                        return false;
                    }

                    context.BifaIndustry.Remove(dbItem);

                    context.SaveChanges();

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

        public object GetBifaAddress(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaAddress data = new DTO.BifaAddress();

            try
            {
                using (var context = CreateContext())
                {
                    if (id > 0)
                    {
                        data = converter.DB2DTO_BifaAddress(context.BifaCompanyMng_BifaAddress_View.FirstOrDefault(o => o.BifaAddressID == id));
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
        public object UpdateBifaAddress(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            int id = (filters.ContainsKey("id") && filters["id"] != null && !string.IsNullOrEmpty(filters["id"].ToString().Trim())) ? System.Convert.ToInt32(filters["id"].ToString().Trim()) : 0;
            DTO.BifaAddress dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaAddress>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaAddress dbItem;

                    if (id == 0)
                    {
                        dbItem = new BifaAddress();

                        context.BifaAddress.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaAddress.FirstOrDefault(o => o.BifaAddressID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa address!";
                    }
                    else
                    {
                        converter.DTO2DB_BifaAddress(dtoItem, ref dbItem);

                        dbItem.BifaCompanyID = dtoItem.BifaCompanyID;
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = System.DateTime.Now;

                        context.SaveChanges();

                        dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_BifaAddress_View, DTO.BifaAddress>(context.BifaCompanyMng_BifaAddress_View.FirstOrDefault(o => o.BifaAddressID == dbItem.BifaAddressID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteBifaAddress(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaAddress.FirstOrDefault(o => o.BifaAddressID == id);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa address";

                        return false;
                    }

                    context.BifaAddress.Remove(dbItem);

                    context.SaveChanges();

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

        public object UpdateBifaClub(int userID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaClub dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaClub>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaClub dbItem;

                    if (id == 0)
                    {
                        dbItem = new BifaClub();

                        context.BifaClub.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaClub.FirstOrDefault(o => o.BifaClubID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa club!";
                    }
                    else
                    {
                        AutoMapper.Mapper.Map<DTO.BifaClub, BifaClub>(dtoItem, dbItem);
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = System.DateTime.Now;

                        context.SaveChanges();

                        dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_SupportBifaClub_View, DTO.BifaClub>(context.BifaCompanyMng_SupportBifaClub_View.FirstOrDefault(o => o.BifaClubID == dbItem.BifaClubID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteBifaClub(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaClub.FirstOrDefault(o => o.BifaClubID == id);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa club";

                        return false;
                    }

                    context.BifaClub.Remove(dbItem);

                    context.SaveChanges();

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

        public object UpdateBifaClubMember(int userID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaClubMember dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaClubMember>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaClubMember dbItem;

                    if (id == 0)
                    {
                        dbItem = new BifaClubMember();

                        context.BifaClubMember.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaClubMember.FirstOrDefault(o => o.BifaClubMemberID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa club member!";
                    }
                    else
                    {
                        AutoMapper.Mapper.Map<DTO.BifaClubMember, BifaClubMember>(dtoItem, dbItem);
                        dbItem.JoinedDate = dtoItem.JoinedDate.ConvertStringToDateTime();
                        dbItem.BifaCompanyID = dtoItem.BifaCompanyID;
                        dbItem.UpdatedBy = userID;
                        dbItem.UpdatedDate = System.DateTime.Now;

                        context.SaveChanges();

                        dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_BifaClubMember_View, DTO.BifaClubMember>(context.BifaCompanyMng_BifaClubMember_View.FirstOrDefault(o => o.BifaClubMemberID == dbItem.BifaClubMemberID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteBifaClubMember(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaClubMember.FirstOrDefault(o => o.BifaClubMemberID == id);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa club member";

                        return false;
                    }

                    context.BifaClubMember.Remove(dbItem);

                    context.SaveChanges();

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

        public object UpdateBifaEmailAddress(int userID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            System.Collections.Generic.List<DTO.BifaEmailAddress> dtoItems = ((Newtonsoft.Json.Linq.JArray)filters["view"]).ToObject<System.Collections.Generic.List<DTO.BifaEmailAddress>>();

            try
            {
                using (var context = CreateContext())
                {
                    foreach (var dtoItem in dtoItems)
                    {
                        BifaEmailAddress dbItem;

                        if (dtoItem.BifaEmailAddressID < 0)
                        {
                            dbItem = new BifaEmailAddress();
                            context.BifaEmailAddress.Add(dbItem);
                        }
                        else
                        {
                            dbItem = context.BifaEmailAddress.FirstOrDefault(o => o.BifaEmailAddressID == dtoItem.BifaEmailAddressID);
                        }

                        if (dbItem != null)
                        {
                            converter.DTO2DB_BifaEmailAddress(dtoItem, ref dbItem);
                            dbItem.BifaCompanyID = dtoItem.BifaCompanyID;
                            dbItem.UpdatedBy = userID;
                            dbItem.UpdatedDate = System.DateTime.Now;
                        }
                    }

                    context.SaveChanges();

                    dtoItems = AutoMapper.Mapper.Map<System.Collections.Generic.List<BifaCompanyMng_BifaEmailAddress_View>, System.Collections.Generic.List<DTO.BifaEmailAddress>>(context.BifaCompanyMng_BifaEmailAddress_View.ToList());
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItems;
        }
        public object DeleteBifaEmailAddress(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaEmailAddress.FirstOrDefault(o => o.BifaEmailAddressID == id);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find Bifa email address";

                        return false;
                    }

                    context.BifaEmailAddress.Remove(dbItem);

                    context.SaveChanges();

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

        public object UpdateTelephone(int userID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaTelephone dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaTelephone>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaTelephone dbItem;

                    if (id == 0)
                    {
                        dbItem = new BifaTelephone();

                        context.BifaTelephone.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaTelephone.FirstOrDefault(o => o.BifaTelephoneID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find bifa telephone!";
                    }
                    else
                    {
                        converter.DTO2DB_BifaTelephone(userID, dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_BifaTelephone_View, DTO.BifaTelephone>(context.BifaCompanyMng_BifaTelephone_View.FirstOrDefault(o => o.BifaTelephoneID == dbItem.BifaTelephoneID));
                    }
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteTelephone(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaTelephone.FirstOrDefault(o => o.BifaTelephoneID == id);

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not find bifa telephone";

                        return false;
                    }

                    context.BifaTelephone.Remove(dbItem);

                    context.SaveChanges();

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

        public object UpdatePerson(int userID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaPerson dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaPerson>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaPerson dbItem;
                    if (id == 0)
                    {
                        dbItem = new BifaPerson();
                        context.BifaPerson.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaPerson.FirstOrDefault(o => o.BifaPersonID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not found data";
                        return null;
                    }

                    converter.DTO2DB_BifaPerson(userID, dtoItem, ref dbItem);

                    context.BifaEmailAddress.Local.Where(o => o.BifaPerson == null).ToList().ForEach(o => context.BifaEmailAddress.Remove(o));
                    context.BifaTelephone.Local.Where(o => o.BifaPerson == null).ToList().ForEach(o => context.BifaTelephone.Remove(o));

                    context.SaveChanges();

                    dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_BifaPerson_View, DTO.BifaPerson>(context.BifaCompanyMng_BifaPerson_View.FirstOrDefault(o => o.BifaPersonID == dbItem.BifaPersonID));
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeletePerson(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaPerson.FirstOrDefault(o => o.BifaPersonID == id);
                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not found data";

                        return false;
                    }

                    context.BifaPerson.Remove(dbItem);
                    context.SaveChanges();

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

        public object UpdateEvent(int userID, int id, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            DTO.BifaEvent dtoItem = ((Newtonsoft.Json.Linq.JObject)filters["view"]).ToObject<DTO.BifaEvent>();

            try
            {
                using (var context = CreateContext())
                {
                    BifaEvent dbItem;
                    if (id == 0)
                    {
                        dbItem = new BifaEvent();
                        context.BifaEvent.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.BifaEvent.FirstOrDefault(o => o.BifaEventID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not found data";
                        return null;
                    }

                    converter.DTO2DB_BifaEvent(userID, dtoItem, ref dbItem);

                    context.BifaEventFile.Local.Where(o => o.BifaEvent == null).ToList().ForEach(o => context.BifaEventFile.Remove(o));
                    context.BifaEventParticipant.Local.Where(o => o.BifaEvent == null).ToList().ForEach(o => context.BifaEventParticipant.Remove(o));

                    context.SaveChanges();

                    dtoItem = AutoMapper.Mapper.Map<BifaCompanyMng_BifaEvent_View, DTO.BifaEvent>(context.BifaCompanyMng_BifaEvent_View.FirstOrDefault(o => o.BifaEventID == dbItem.BifaEventID));
                }
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Helper.GetInnerException(ex).Message;
            }

            return dtoItem;
        }
        public object DeleteEvent(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            try
            {
                using (var context = CreateContext())
                {
                    var dbItem = context.BifaEvent.FirstOrDefault(o => o.BifaEventID == id);
                    if (dbItem == null)
                    {
                        notification.Type = Library.DTO.NotificationType.Error;
                        notification.Message = "Can not found data";

                        return false;
                    }

                    context.BifaEvent.Remove(dbItem);
                    context.SaveChanges();

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
    }
}
