using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.PriceComparisonRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();
        }

        //
        // CUSTOM FUNCTION
        //
        public string GetExcelReportData(string Season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PriceComparisonRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.TableMappings.Add("Table", "PriceComparisonRpt_Factory_View");
                adap.TableMappings.Add("Table1", "PriceComparisonRpt_LatestItemPrice_View");
                adap.TableMappings.Add("Table2", "PriceComparisonRpt_Overview_View");
                adap.Fill(ds);

                foreach (DAL.ReportDataObject.PriceComparisonRpt_Factory_ViewRow fRow in ds.PriceComparisonRpt_Factory_View)
                {
                    ds.PriceComparisonRpt_Overview_View.Columns.Add(fRow.FactoryUD.Replace("-", "_"), typeof(decimal));
                }

                DAL.ReportDataObject.PriceComparisonRpt_LatestItemPrice_ViewRow iRow = null;
                string ClientUD;
                string ArticleCode;
                string FactoryUD;
                foreach (DAL.ReportDataObject.PriceComparisonRpt_Overview_ViewRow oRow in ds.PriceComparisonRpt_Overview_View)
                {
                    ClientUD = oRow.ClientUD;
                    ArticleCode = oRow.ArticleCode;
                    FactoryUD = oRow.FactoryUD;
                    iRow = ds.PriceComparisonRpt_LatestItemPrice_View.FirstOrDefault(o=>o.ClientUD == ClientUD && o.ArticleCode == ArticleCode && o.FactoryUD != FactoryUD);
                    if(iRow != null && !iRow.IsSalePriceNull())
                    {
                        iRow[FactoryUD.Replace("-", "_")] = iRow.SalePrice;
                    }
                }

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "MIEurofarPriceOverview");
                //return string.Empty;

                // generate xml file
                //return Library.Helper.CreateReportFiles(ds, "MIEurofarPriceOverview");
                //return Library.Helper.CreateReportFiles(ds, "MIEurofarPriceOverview2");
                return Library.Helper.CreateReportFileWithEPPlus(ds, "PriceComparisonRpt");
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
