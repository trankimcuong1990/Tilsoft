using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.DTO;
using Module.EurofarSampleCollectionMng.DTO;

namespace Module.EurofarSampleCollectionMng.DAL
{
    public class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private DataConverter converter = new DataConverter();
        private EurofarSampleCollectionMngEntities CreateContext()
        {
            return new EurofarSampleCollectionMngEntities(Library.Helper.CreateEntityConnectionString("DAL.EurofarSampleCollectionMngModel"));
        }

        public DTO.SearchFormData GetDataWithFilters(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.EurofarSearchDTO>();
            totalRows = 0;

            string client = null;
            string season = null;
            string description = null;
            string sampleOrderUD = null;
            if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
            {
                season = filters["season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("client") && !string.IsNullOrEmpty(filters["client"].ToString()))
            {
                client = filters["client"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
            {
                description = filters["description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("sampleOrderUD") && !string.IsNullOrEmpty(filters["sampleOrderUD"].ToString()))
            {
                sampleOrderUD = filters["sampleOrderUD"].ToString().Replace("'", "''");
            }

            //try to get data
            try
            {
                using (EurofarSampleCollectionMngEntities context = CreateContext())
                {
                    totalRows = context.EurofarSampleCollectionMng_function_SampleProduct(orderBy, orderDirection, client, season, description, sampleOrderUD).Count();
                    var result = context.EurofarSampleCollectionMng_function_SampleProduct(orderBy, orderDirection, client, season, description, sampleOrderUD);
                    data.Data = converter.DB2DTO_EurofarSearchResultList(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public DTO.EditFormData GetEditData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (EurofarSampleCollectionMngEntities context = CreateContext())
                {
                    SampleProduct dbItem = context.SampleProduct.FirstOrDefault(o => o.SampleProductID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Item not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.IsEurofarSampleCollection = false;
                        dbItem.EurofarSampleCollectionBy = null;
                        dbItem.EurofarSampleCollectionDate = null;
                        context.SaveChanges();

                        return true;
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

        public bool Update(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override EditFormData GetData(int id, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override SearchFormData GetDataWithFilter(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Notification notification)
        {
            throw new NotImplementedException();
        }

        public string ExportExcel(System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            EurofarSampleCollectionObject ds = new EurofarSampleCollectionObject();

            string client = null;
            string season = null;
            string description = null;
            string sampleOrderUD = null;
            if (filters.ContainsKey("season") && !string.IsNullOrEmpty(filters["season"].ToString()))
            {
                season = filters["season"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("client") && !string.IsNullOrEmpty(filters["client"].ToString()))
            {
                client = filters["client"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("description") && !string.IsNullOrEmpty(filters["description"].ToString()))
            {
                description = filters["description"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("sampleOrderUD") && !string.IsNullOrEmpty(filters["sampleOrderUD"].ToString()))
            {
                sampleOrderUD = filters["sampleOrderUD"].ToString().Replace("'", "''");
            }

            try
            {

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("EurofarSampleCollectionMng_function_SampleProduct", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@SortedBy", null);
                adap.SelectCommand.Parameters.AddWithValue("@SortedDirection", null);
                adap.SelectCommand.Parameters.AddWithValue("@Client", client);
                adap.SelectCommand.Parameters.AddWithValue("@Season", season);
                adap.SelectCommand.Parameters.AddWithValue("@Desciption", description);
                adap.SelectCommand.Parameters.AddWithValue("@SampleOrderUD", sampleOrderUD);

                adap.TableMappings.Add("Table", "SampleProduct");
                adap.Fill(ds);

                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "EurofarSampleCollectionRpt");
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
