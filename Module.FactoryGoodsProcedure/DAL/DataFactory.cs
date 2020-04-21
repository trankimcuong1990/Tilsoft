using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.FactoryGoodsProcedure.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormDataDTO, DTO.EditFormDataDTO>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private FactoryGoodsProcedureEntities CreateContext()
        {
            return new FactoryGoodsProcedureEntities(Library.Helper.CreateEntityConnectionString("DAL.FactoryGoodsProcedureModel"));
        }

        public override DTO.SearchFormDataDTO GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormDataDTO data = new DTO.SearchFormDataDTO();
            data.Data = new List<DTO.FactoryGoodsProcedureSearchResultDTO>();
            totalRows = 0;

            // Try to get data
            try
            {
                using (FactoryGoodsProcedureEntities context = CreateContext())
                {
                    string FactoryGoodsProcedureUD = null;
                    string FactoryGoodsProcedureNM = null;

                    if (filters.ContainsKey("factoryGoodsProcedureUd") && !string.IsNullOrEmpty(filters["factoryGoodsProcedureUd"].ToString()))
                    {
                        FactoryGoodsProcedureUD = filters["factoryGoodsProcedureUd"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("factoryGoodsProcedureNm") && !string.IsNullOrEmpty(filters["factoryGoodsProcedureNm"].ToString()))
                    {
                        FactoryGoodsProcedureNM = filters["factoryGoodsProcedureNm"].ToString().Replace("'", "''");
                    }

                    totalRows = context.FactoryGoodsProcedureMng_function_SearchFactoryGoodsProcedure(FactoryGoodsProcedureNM, FactoryGoodsProcedureUD, orderBy, orderDirection).Count();
                    var result = context.FactoryGoodsProcedureMng_function_SearchFactoryGoodsProcedure(FactoryGoodsProcedureNM, FactoryGoodsProcedureUD, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_FactoryGoodsProcedureSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormDataDTO GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormDataDTO editFormData = new DTO.EditFormDataDTO();
            Module.Support.DAL.DataFactory support_factory = new Support.DAL.DataFactory();
            // Try to get data.
            try
            {
                using (FactoryGoodsProcedureEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        FactoryGoodsProcedureMng_FactoryGoodsProcedure_View dbItem;
                        dbItem = context.FactoryGoodsProcedureMng_FactoryGoodsProcedure_View.FirstOrDefault(o => o.FactoryGoodsProcedureID == id);
                        editFormData.Data = converter.DB2DTO_FactoryGoodsProcedure(dbItem);
                        editFormData.FactorySteps = null;
                    }
                    else
                    {
                        editFormData.Data = new DTO.FactoryGoodsProcedureDTO();
                        editFormData.Data.FactoryGoodsProcedureDetailDTOs = new List<DTO.FactoryGoodsProcedureDetailDTO>();
                    }
                    editFormData.FactorySteps = support_factory.GetFactoryStep();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return editFormData;
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryGoodsProcedureDTO dtoFactoryGoodsProcedure = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.FactoryGoodsProcedureDTO>();

            try
            {
                // Check FactoryStep selected or not selected.
                if (dtoFactoryGoodsProcedure.FactoryGoodsProcedureID.CompareTo(0) != 0)
                {
                    bool isValid = InvalidFactoryGoodsProcedureDetail(dtoFactoryGoodsProcedure.FactoryGoodsProcedureDetailDTOs);
                    if (!isValid)
                        throw new Exception("You have select step.");
                }

                using (FactoryGoodsProcedureEntities context = CreateContext())
                {
                    FactoryGoodsProcedure dbItem = null;

                    if (id == 0)
                    {
                        dbItem = new FactoryGoodsProcedure();
                        context.FactoryGoodsProcedure.Add(dbItem);

                        // Generate FactoryGoodsProcedureUD continue.
                        List<string> factoryGoodsProcedureUDs = context.FactoryGoodsProcedure.Select(o => o.FactoryGoodsProcedureUD).ToList();
                        int nextFactoryGoodsProcedureUD = GenerateFactoryGoodsProcedureUD(factoryGoodsProcedureUDs);

                        string getNext = nextFactoryGoodsProcedureUD.ToString();
                        if (getNext.Length == 1)
                            getNext = getNext.PadLeft(2, '0');

                        dtoFactoryGoodsProcedure.FactoryGoodsProcedureUD = getNext;
                    }
                    else
                    {
                        dbItem = context.FactoryGoodsProcedure.Where(o => o.FactoryGoodsProcedureID == id).FirstOrDefault();
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Data not found !";
                        return false;
                    }

                    string tmpFile = string.Format("{0}{1}", FrameworkSetting.Setting.AbsoluteUserTempFolder, @"\");

                    converter.DTO2DB_FactoryGoodsProcedure(dtoFactoryGoodsProcedure, ref dbItem);

                    context.FactoryGoodsProcedureDetail.Local.Where(o => o.FactoryGoodsProcedure == null).ToList().ForEach(o => context.FactoryGoodsProcedureDetail.Remove(o));

                    context.SaveChanges();

                    dtoItem = GetData(dbItem.FactoryGoodsProcedureID, out notification).Data;
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                if (ex.GetBaseException() != null)
                    notification.DetailMessage.Add(ex.GetBaseException().Message);

                return false;
            }
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (FactoryGoodsProcedureEntities context = CreateContext())
                {
                    var dbItem = context.FactoryGoodsProcedure.Where(o => o.FactoryGoodsProcedureID == id).FirstOrDefault();

                    foreach (var item in dbItem.FactoryGoodsProcedureDetail.ToArray())
                        context.FactoryGoodsProcedureDetail.Remove(item);

                    context.FactoryGoodsProcedure.Remove(dbItem);
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);

                if (ex.GetBaseException() != null)
                    notification.DetailMessage.Add(ex.GetBaseException().Message);

                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public int EditDetail(int factoryGoodsProcedureID, object factoryGoodsProcedureDetail, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.FactoryGoodsProcedureDetailDTO dtoFactoryGoodsProcedureDetail = ((Newtonsoft.Json.Linq.JObject)factoryGoodsProcedureDetail).ToObject<DTO.FactoryGoodsProcedureDetailDTO>();
            try
            {
                if (!dtoFactoryGoodsProcedureDetail.FactoryStepID.HasValue)
                    throw new Exception("You have select step.");
                using (FactoryGoodsProcedureEntities context = CreateContext())
                {
                    FactoryGoodsProcedureDetail dbFactoryGoodsProcedureDetail;
                    if (dtoFactoryGoodsProcedureDetail.FactoryGoodsProcedureDetailID > 0)
                    {
                        dbFactoryGoodsProcedureDetail = context.FactoryGoodsProcedureDetail.Find(dtoFactoryGoodsProcedureDetail.FactoryGoodsProcedureDetailID);
                    }
                    else
                    {
                        dbFactoryGoodsProcedureDetail = new FactoryGoodsProcedureDetail();
                        dbFactoryGoodsProcedureDetail.FactoryGoodsProcedureID = factoryGoodsProcedureID;
                        context.FactoryGoodsProcedureDetail.Add(dbFactoryGoodsProcedureDetail);
                    }
                    if (dbFactoryGoodsProcedureDetail != null)
                    {
                        dbFactoryGoodsProcedureDetail.FactoryStepID = dtoFactoryGoodsProcedureDetail.FactoryStepID;
                        dbFactoryGoodsProcedureDetail.StepIndex = dtoFactoryGoodsProcedureDetail.StepIndex;
                    }
                    else
                    {
                        throw new Exception("Could not find data. You have to save data before update component detail.");
                    }
                    context.SaveChanges();
                    return dbFactoryGoodsProcedureDetail.FactoryGoodsProcedureDetailID;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                return -1;
            }
        }

        public bool DeleteDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (FactoryGoodsProcedureEntities context = CreateContext())
                {
                    var dbItem = context.FactoryGoodsProcedureDetail.Find(id);
                    if (dbItem != null)
                    {
                        context.FactoryGoodsProcedureDetail.Remove(dbItem);
                        context.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                return false;
            }
        }

        private bool InvalidFactoryGoodsProcedureDetail(List<DTO.FactoryGoodsProcedureDetailDTO> factoryGoodsProcedureDetailDTOs)
        {
            foreach (var factoryGoodsProcedureDetailDTO in factoryGoodsProcedureDetailDTOs)
            {
                if (factoryGoodsProcedureDetailDTO.FactoryGoodsProcedureDetailID > 0 && factoryGoodsProcedureDetailDTO.FactoryStepID == null)
                    return false;
            }
            return true;
        }

        private int GenerateFactoryGoodsProcedureUD(List<string> factoryGoodsProcedureUDs)
        {
            int genNumber = 1;
            for (int i = 0; i < factoryGoodsProcedureUDs.Count; i++)
            {
                if (!factoryGoodsProcedureUDs.Contains(genNumber.ToString()))
                    return genNumber;
                genNumber += 1;
            }
            return genNumber;
        }
    }
}
