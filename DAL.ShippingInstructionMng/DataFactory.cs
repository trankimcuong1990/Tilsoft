using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ShippingInstructionMng
{
    public class DataFactory : DALBase.FactoryBase2<DTO.ShippingInstructionMng.SearchFormData, DTO.ShippingInstructionMng.EditFormData, DTO.ShippingInstructionMng.ShippingInstruction>
    {
        private DataConverter converter = new DataConverter();
        private DAL.Support.DataFactory supportFactory = new Support.DataFactory();
        private ShippingInstructionMngEntities CreateContext()
        {
            return new ShippingInstructionMngEntities(DALBase.Helper.CreateEntityConnectionString("ShippingInstructionMngModel"));
        }

        public override DTO.ShippingInstructionMng.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ShippingInstructionMng.SearchFormData data = new DTO.ShippingInstructionMng.SearchFormData();
            data.Data = new List<DTO.ShippingInstructionMng.ShippingInstructionSearchResult>();
            totalRows = 0;

            //try to get data
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    string ClientUD = null;
                    string ProformaInvoiceNo = null;
                    string Priority = null;
                    int? PODID = null;
                    bool? IsDefault = null;
                    bool? IsSample = null;

                    if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
                    {
                        ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
                    {
                        ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("PODID") && !string.IsNullOrEmpty(filters["PODID"].ToString()))
                    {
                        PODID = (int)filters["PODID"];
                    }
                    if (filters.ContainsKey("Priority") && !string.IsNullOrEmpty(filters["Priority"].ToString()))
                    {
                        Priority = filters["Priority"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("IsDefault") && !string.IsNullOrEmpty(filters["IsDefault"].ToString()))
                    {
                        if (filters["IsDefault"].ToString() == "1")
                        {
                            IsDefault = true;
                        }
                        else
                        {
                            IsDefault = false;
                        }
                    }
                    if (filters.ContainsKey("IsSample") && !string.IsNullOrEmpty(filters["IsSample"].ToString()))
                    {
                        if (filters["IsSample"].ToString() == "1")
                        {
                            IsSample = true;
                        }
                        else
                        {
                            IsSample = false;
                        }
                    }

                    totalRows = context.ShippingInstructionMng_function_SearchShippingInstruction(ClientUD, PODID, ProformaInvoiceNo, Priority, IsDefault, IsSample, orderBy, orderDirection).Count();
                    var result = context.ShippingInstructionMng_function_SearchShippingInstruction(ClientUD, PODID, ProformaInvoiceNo, Priority, IsDefault, IsSample, orderBy, orderDirection);

                    data.Data = converter.DB2DTO_ShippingInstructionSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.ShippingInstructionMng.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ShippingInstructionMng.EditFormData data = new DTO.ShippingInstructionMng.EditFormData();
            data.Data = new DTO.ShippingInstructionMng.ShippingInstruction();
            data.Clients = new List<DTO.Support.Client>();
            data.ConsigneeTypes = new List<DTO.Support.ConsigneeType>();
            data.Countries = new List<DTO.Support.ClientCountry>();
            data.Forwarders = new List<DTO.Support.Forwarder>();
            data.NotifyTypes = new List<DTO.Support.NotifyType>();
            data.PODs = new List<DTO.Support.POD>();

            //try to get data
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    // add new case
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_ShippingInstruction(context.ShippingInstructionMng_ShippingInstruction_View.FirstOrDefault(o => o.ShippingInstructionID == id));
                    }

                    data.Clients = supportFactory.GetClient().ToList();
                    data.ConsigneeTypes = supportFactory.GetConsigneeTypes().ToList();
                    data.Countries = supportFactory.GetClientCountry().ToList();
                    data.Forwarders = supportFactory.GetForwarder().ToList();
                    data.NotifyTypes = supportFactory.GetNotifyTypes().ToList();
                    data.PODs = supportFactory.GetPOD().ToList();
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    ShippingInstruction dbItem = context.ShippingInstruction.FirstOrDefault(o => o.ShippingInstructionID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Shipping instruction not found!";
                        return false;
                    }
                    else
                    {
                        if (dbItem.IsConfirmed.HasValue && dbItem.IsConfirmed.Value)
                        {
                            throw new Exception("The shipping instruction has been confirmed and can not be deleted");
                        }

                        context.ShippingInstruction.Remove(dbItem);
                        context.SaveChanges();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message };
                return false;
            }
        }

        public override bool UpdateData(int id, ref DTO.ShippingInstructionMng.ShippingInstruction dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    ShippingInstruction dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new ShippingInstruction();
                        context.ShippingInstruction.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.ShippingInstruction.FirstOrDefault(o => o.ShippingInstructionID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Shipping instruction not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        // reset default
                        if (dtoItem.IsDefault.HasValue && dtoItem.IsDefault.Value)
                        {
                            int ClientID = dtoItem.ClientID.Value;
                            List<int> InstructionIDs = context.ShippingInstructionMng_ShippingInstruction_View.Where(o => o.ClientID == ClientID).Select(o => o.ShippingInstructionID).ToList();
                            context.ShippingInstruction.Where(o => InstructionIDs.Contains(o.ShippingInstructionID)).ToList().ForEach(o => o.IsDefault = null);
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);
                        context.SaveChanges();

                        dtoItem = GetData(dbItem.ShippingInstructionID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int id, ref DTO.ShippingInstructionMng.ShippingInstruction dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    ShippingInstruction dbItem = context.ShippingInstruction.FirstOrDefault(o => o.ShippingInstructionID == id);
                    if (dbItem != null)
                    {
                        dbItem.IsConfirmed = true;
                        dbItem.ConfirmedBy = dtoItem.UpdatedBy;
                        dbItem.ConfirmedDate = DateTime.Now;
                        context.SaveChanges();

                        dtoItem = GetData(id, out notification).Data;
                        return true;
                    }
                    else
                    {
                        throw new Exception("Shipping instruction not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public override bool Reset(int id, ref DTO.ShippingInstructionMng.ShippingInstruction dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    ShippingInstruction dbItem = context.ShippingInstruction.FirstOrDefault(o => o.ShippingInstructionID == id);
                    if (dbItem != null)
                    {
                        dbItem.IsConfirmed = false;
                        dbItem.ConfirmedBy = null;
                        dbItem.ConfirmedDate = null;
                        dbItem.UpdatedBy = dtoItem.UpdatedBy;
                        dbItem.UpdatedDate = DateTime.Now;
                        context.SaveChanges();
                        dtoItem = GetData(id, out notification).Data;

                        return true;
                    }
                    else
                    {
                        throw new Exception("Shipping instruction not found!");
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                return false;
            }
        }

        public DTO.ShippingInstructionMng.ShippingInstruction GetDefault(int clientId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ShippingInstructionMng.ShippingInstruction data = new DTO.ShippingInstructionMng.ShippingInstruction();

            //try to get data
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_ShippingInstruction(context.ShippingInstructionMng_ShippingInstruction_View.FirstOrDefault(o => o.ClientID == clientId && o.IsDefault.HasValue && o.IsDefault.Value));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.ShippingInstructionMng.PODCountry GetCountryFromPODfunction(int podid, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ShippingInstructionMng.PODCountry data = new DTO.ShippingInstructionMng.PODCountry();

            //try to get data
            try
            {
                using (ShippingInstructionMngEntities context = CreateContext())
                {
                    data = converter.DB2DTO_PODCountry(context.ShippingInstructionMng_PODCountry_View.FirstOrDefault(o => o.PoDID == podid));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.ShippingInstructionMng.SearchFilterData GetFilterData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ShippingInstructionMng.SearchFilterData data = new DTO.ShippingInstructionMng.SearchFilterData();
            data.PODs = new List<DTO.Support.POD>();

            //try to get data
            try
            {
                data.PODs = supportFactory.GetPOD().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public string ExportExcel (System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ShippingInstructionMngObject ds = new ShippingInstructionMngObject();

            string ClientUD = null;
            string ProformaInvoiceNo = null;
            string Priority = null;
            int? PODID = null;
            bool? IsDefault = null;
            bool? IsSample = null;

            if (filters.ContainsKey("ClientUD") && !string.IsNullOrEmpty(filters["ClientUD"].ToString()))
            {
                ClientUD = filters["ClientUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("ProformaInvoiceNo") && !string.IsNullOrEmpty(filters["ProformaInvoiceNo"].ToString()))
            {
                ProformaInvoiceNo = filters["ProformaInvoiceNo"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PODID") && !string.IsNullOrEmpty(filters["PODID"].ToString()))
            {
                PODID = (int)filters["PODID"];
            }
            if (filters.ContainsKey("Priority") && !string.IsNullOrEmpty(filters["Priority"].ToString()))
            {
                Priority = filters["Priority"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("IsDefault") && !string.IsNullOrEmpty(filters["IsDefault"].ToString()))
            {
                if (filters["IsDefault"].ToString() == "1")
                {
                    IsDefault = true;
                }
                else
                {
                    IsDefault = false;
                }
            }
            if (filters.ContainsKey("IsSample") && !string.IsNullOrEmpty(filters["IsSample"].ToString()))
            {
                if (filters["IsSample"].ToString() == "1")
                {
                    IsSample = true;
                }
                else
                {
                    IsSample = false;
                }
            }

            try
            {
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("ShippingInstructionMng_Function_ExportExcelWithFilter", new System.Data.SqlClient.SqlConnection(DALBase.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                adap.SelectCommand.Parameters.AddWithValue("ClientUD", ClientUD);
                adap.SelectCommand.Parameters.AddWithValue("ProformaInvoiceNo", ProformaInvoiceNo);
                adap.SelectCommand.Parameters.AddWithValue("Priority", Priority);
                adap.SelectCommand.Parameters.AddWithValue("IsDefault", IsDefault);
                adap.SelectCommand.Parameters.AddWithValue("IsSample", IsSample);

                adap.TableMappings.Add("Table", "ShippingInstructionMng_ShippingInstructionExportExcel_View");
                adap.Fill(ds);
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "ShippingInstructionRpt");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                {
                    notification.DetailMessage.Add(ex.InnerException.Message);
                }
                return string.Empty;
            }

        }
    }
}
