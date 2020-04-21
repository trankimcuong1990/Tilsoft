using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.DTO;
using Module.PriceConfiguration.DTO;
using dal = DAL.Support;

namespace Module.PriceConfiguration.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchData, DTO.EditData>
    {
        private DataConverter converter = new DataConverter();
        private Module.Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private dal.DataFactory dalSupport = new dal.DataFactory();

        public PriceConfigurationEntities CreateContext()
        {
            return new PriceConfigurationEntities(Library.Helper.CreateEntityConnectionString("DAL.PriceConfigurationModel"));
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (PriceConfigurationEntities context = CreateContext())
                {
                    PriceConfiguration dbItem = context.PriceConfiguration.FirstOrDefault(o => o.PriceConfigurationID == id);

                    if (dbItem == null)
                    {
                        notification = new Notification()
                        {
                            Type = NotificationType.Error,
                            Message = "Can not find data!"
                        };

                        return false;
                    }

                    context.PriceConfiguration.Remove(dbItem);
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
            notification = new Notification()
            {
                Type = NotificationType.Success
            };
            EditData data = new EditData()
            {
                Data = new PriceConfigurationDto(),
                SupportSeason = new List<Support.DTO.Season>()
            };

            try
            {
                if (id > 0)
                {
                    using (PriceConfigurationEntities context = CreateContext())
                    {
                        PriceConfiguration_PriceConfiguration_View dbItem = context.PriceConfiguration_PriceConfiguration_View.FirstOrDefault(o => o.PriceConfigurationID == id);

                        if (dbItem == null)
                        {
                            notification = new Notification()
                            {
                                Type = NotificationType.Error,
                                Message = "Can not find data!"
                            };

                            return data;
                        }
                    }
                }
                else
                {
                    data.Data.PriceConfigurationDetailOfFCS = new List<PriceConfigurationDetailDto>();
                    data.Data.PriceConfigurationDetailOfMaterialColor = new List<PriceConfigurationDetailDto>();
                    data.Data.PriceConfigurationDetailOfFrameMaterial = new List<PriceConfigurationDetailDto>();
                    data.Data.PriceConfigurationDetailOfCushionColor = new List<PriceConfigurationDetailDto>();
                    data.Data.PriceConfigurationDetailOfPackingMethod = new List<PriceConfigurationDetailDto>();
                }

                data.SupportSeason = supportFactory.GetSeason().ToList();
                data.SupportFSCType = supportFactory.GetFSCType().ToList();
                data.SupportProductElement = supportFactory.GetProductElement().ToList();
                data.SupportPackagingMethod = supportFactory.GetPackagingMethod().ToList();

                return data;
            }
            catch (Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message
                };

                return data;
            }
        }

        public override SearchData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            totalRows = 0;
            notification = new Notification()
            {
                Type = NotificationType.Success
            };
            SearchData data = new SearchData()
            {
                Data = new List<PriceConfigurationSearchResultDto>(),
                SupportSeason = new List<Support.DTO.Season>()
            };

            try
            {
                using (PriceConfigurationEntities context = CreateContext())
                {
                    string productElement = null;
                    string season = null;

                    totalRows = context.PriceConfiguration_function_PriceConfigurationSearchResult(productElement, season, orderBy, orderDirection).Count();

                    var result = context.PriceConfiguration_function_PriceConfigurationSearchResult(productElement, season, orderBy, orderDirection);
                    data.Data = converter.DB2DTO_Search(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }

                data.SupportSeason = supportFactory.GetSeason().ToList();
                data.SupportProductElement = supportFactory.GetProductElement().ToList();

                return data;
            }
            catch (Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message
                };

                return data;
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            DTO.EditData dtoCast = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.EditData>();
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (var context = CreateContext())
                {
                    List<PriceConfiguration> listDB = context.PriceConfiguration.Where(s => s.Season.Equals(dtoCast.Data.Season)).ToList();
                    foreach (var item in dtoCast.SupportProductElement.ToList())
                    {
                        switch (item.ProductElementID)
                        {
                            case 1: // FSC
                                this.ProcessFSC(dtoCast.Data, context, id, userId, 1);
                                break;
                            case 2: // Material Color
                                this.ProcessMaterialColor(dtoCast.Data, context, id, userId, 2);
                                break;
                            case 3: // Frame Material
                                this.ProcessFrameMaterial(dtoCast.Data, context, id, userId, 3);
                                break;
                            case 4: // Cushion Color
                                this.ProcessCushionColor(dtoCast.Data, context, id, userId, 4);
                                break;
                            case 5: // Packaging Method
                                this.ProcessPackagingMethod(dtoCast.Data, context, id, userId, 5);
                                break;
                        }
                    }

                    context.PriceConfigurationDetail.Local.Where(o => o.PriceConfiguration == null).ToList().ForEach(o => context.PriceConfigurationDetail.Remove(o));
                    context.SaveChanges();

                    dtoItem = this.GetData(1, dtoCast.Data.Season, out notification);

                    return true;
                }
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

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public EditData GetData(int id, string season, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };
            EditData data = new EditData()
            {
                Data = new PriceConfigurationDto(),
                SupportSeason = new List<Support.DTO.Season>()
            };

            try
            {
                using (PriceConfigurationEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        List<PriceConfiguration_PriceConfiguration_View> list = context.PriceConfiguration_PriceConfiguration_View.Where(o => o.Season.Equals(season)).ToList();

                        if (list == null || list.Count == 0)
                        {
                            notification = new Notification()
                            {
                                Type = NotificationType.Error,
                                Message = "Can not find data!"
                            };

                            return data;
                        }
                        else
                        {
                            data.Data = converter.DB2DTO_Get(list[0]);

                            data.Data.PriceConfigurationDetailOfFCS = converter.DB2DTO_ListFSC(context.PriceConfiguration_FSC_View.Where(o => o.Season.Equals(season)).ToList());
                            data.Data.PriceConfigurationDetailOfFrameMaterial = converter.DB2DTO_ListFrameMaterial(context.PriceConfiguration_FrameMaterial_View.Where(o => o.Season.Equals(season)).ToList());
                            data.Data.PriceConfigurationDetailOfPackingMethod = converter.DB2DTO_ListPackagingMethod(context.PriceConfiguration_PackagingMethod_View.Where(o => o.Season.Equals(season)).ToList());
                            data.Data.PriceConfigurationDetailOfMaterialColor = converter.DB2DTO_ListMaterialColor(context.PriceConfiguration_MaterialColor_View.Where(o => o.Season.Equals(season)).ToList());
                            data.Data.PriceConfigurationDetailOfCushionColor = converter.DB2DTO_ListCushionColor(context.PriceConfiguration_CushionColor_View.Where(o => o.Season.Equals(season)).ToList());
                        }
                    }
                    else
                    {
                        data.Data.PriceConfigurationDetailOfFCS = new List<PriceConfigurationDetailDto>();
                        data.Data.PriceConfigurationDetailOfMaterialColor = new List<PriceConfigurationDetailDto>();
                        data.Data.PriceConfigurationDetailOfFrameMaterial = new List<PriceConfigurationDetailDto>();
                        data.Data.PriceConfigurationDetailOfCushionColor = new List<PriceConfigurationDetailDto>();
                        data.Data.PriceConfigurationDetailOfPackingMethod = new List<PriceConfigurationDetailDto>();
                    }

                    data.SupportSeason = supportFactory.GetSeason().ToList();
                    data.SupportFSCType = supportFactory.GetFSCType().ToList();
                    data.SupportProductElement = supportFactory.GetProductElement().ToList();
                    data.SupportPackagingMethod = supportFactory.GetPackagingMethod().ToList();
                    data.SupportFrameMaterial = supportFactory.GetFrameMaterial().ToList();
                    data.SupportMaterialColor = dalSupport.GetMaterialColor().ToList();
                    data.SupportCushionColor = converter.DB2DTO_CushionColor(context.CushionColorMng_CushionColor_View.Where(o => o.IsEnabled == true).ToList());

                    data.SeasonOfPriceConfiguration = context.PriceConfiguration_PriceConfigurationSearchResult_View.Select(s => s.Season).ToList();

                    return data;
                }
            }
            catch (Exception ex)
            {
                notification = new Notification()
                {
                    Type = NotificationType.Error,
                    Message = ex.Message
                };

                return data;
            }
        }

        public bool DeleteData(int id, string season, out Notification notification)
        {
            notification = new Notification()
            {
                Type = NotificationType.Success
            };

            try
            {
                using (PriceConfigurationEntities context = CreateContext())
                {
                    List<PriceConfiguration> dbItems = context.PriceConfiguration.Where(o => o.Season.Equals(season)).ToList();

                    if (dbItems == null || dbItems.Count == 0)
                    {
                        notification = new Notification()
                        {
                            Type = NotificationType.Error,
                            Message = "Can not find data!"
                        };

                        return false;
                    }

                    foreach (var dbItem in dbItems)
                    {
                        context.PriceConfiguration.Remove(dbItem);
                    }

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

        private void ProcessFSC(Module.PriceConfiguration.DTO.PriceConfigurationDto dtoItem, PriceConfigurationEntities context, int id, int userId, int productElementId)
        {
            PriceConfiguration dbItem = null;

            if (id > 0)
            {
                dbItem = context.PriceConfiguration.FirstOrDefault(s => s.Season.Equals(dtoItem.Season) && s.ProductElementID.Value == productElementId);

                if (dbItem == null)
                    return;

                dtoItem.PriceConfigurationID = dbItem.PriceConfigurationID;
            }
            else
            {
                dbItem = new PriceConfiguration() { ProductElementID = productElementId, Season = dtoItem.Season, UpdatedBy = userId, UpdatedDate = DateTime.Now };
                context.PriceConfiguration.Add(dbItem);
            }

            converter.DTO2DB_UpdateFCS(dtoItem, dbItem);
        }

        private void ProcessMaterialColor(Module.PriceConfiguration.DTO.PriceConfigurationDto dtoItem, PriceConfigurationEntities context, int id, int userId, int productElementId)
        {
            PriceConfiguration dbItem = null;

            if (id > 0)
            {
                dbItem = context.PriceConfiguration.FirstOrDefault(s => s.Season.Equals(dtoItem.Season) && s.ProductElementID.Value == productElementId);

                if (dbItem == null)
                    return;

                dtoItem.PriceConfigurationID = dbItem.PriceConfigurationID;
            }
            else
            {
                dbItem = new PriceConfiguration() { ProductElementID = productElementId, Season = dtoItem.Season, UpdatedBy = userId, UpdatedDate = DateTime.Now };
                context.PriceConfiguration.Add(dbItem);
            }

            converter.DTO2DB_UpdateMaterialColor(dtoItem, dbItem);
        }

        private void ProcessFrameMaterial(Module.PriceConfiguration.DTO.PriceConfigurationDto dtoItem, PriceConfigurationEntities context, int id, int userId, int productElementId)
        {
            PriceConfiguration dbItem = null;

            if (id > 0)
            {
                dbItem = context.PriceConfiguration.FirstOrDefault(s => s.Season.Equals(dtoItem.Season) && s.ProductElementID.Value == productElementId);

                if (dbItem == null)
                    return;

                dtoItem.PriceConfigurationID = dbItem.PriceConfigurationID;
            }
            else
            {
                dbItem = new PriceConfiguration() { ProductElementID = productElementId, Season = dtoItem.Season, UpdatedBy = userId, UpdatedDate = DateTime.Now };
                context.PriceConfiguration.Add(dbItem);
            }

            converter.DTO2DB_UpdateFrameMaterial(dtoItem, dbItem);
        }

        private void ProcessCushionColor(Module.PriceConfiguration.DTO.PriceConfigurationDto dtoItem, PriceConfigurationEntities context, int id, int userId, int productElementId)
        {
            PriceConfiguration dbItem = null;

            if (id > 0)
            {
                dbItem = context.PriceConfiguration.FirstOrDefault(o => o.Season.Equals(dtoItem.Season) && o.ProductElementID.Value == productElementId);
            }
            else
            {
                dbItem = new PriceConfiguration();
                context.PriceConfiguration.Add(dbItem);

                dbItem.Season = dtoItem.Season;
                dbItem.ProductElementID = productElementId;
            }

            converter.DTO2DB_UpdateCushionColor(dtoItem, dbItem);

            dbItem.UpdatedBy = userId;
            dbItem.UpdatedDate = DateTime.Now;
        }

        private void ProcessPackagingMethod(Module.PriceConfiguration.DTO.PriceConfigurationDto dtoItem, PriceConfigurationEntities context, int id, int userId, int productElementId)
        {
            PriceConfiguration dbItem = null;

            if (id > 0)
            {
                dbItem = context.PriceConfiguration.FirstOrDefault(s => s.Season.Equals(dtoItem.Season) && s.ProductElementID.Value == productElementId);

                if (dbItem == null)
                    return;

                dtoItem.PriceConfigurationID = dbItem.PriceConfigurationID;
            }
            else
            {
                dbItem = new PriceConfiguration() { ProductElementID = productElementId, Season = dtoItem.Season, UpdatedBy = userId, UpdatedDate = DateTime.Now };
                context.PriceConfiguration.Add(dbItem);
            }

            converter.DTO2DB_UpdatePackagingMethod(dtoItem, dbItem);
        }
    }
}
