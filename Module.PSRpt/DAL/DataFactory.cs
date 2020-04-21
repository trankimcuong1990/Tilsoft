using System;
using System.Data.SqlClient;

namespace Module.PSRpt.DAL
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
        public string GetExcelReportData(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataObject ds = new ReportDataObject();

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PSRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryOrderDetailID", id);
                adap.SelectCommand.Parameters.AddWithValue("@ProductID", DBNull.Value);
                adap.TableMappings.Add("Table", "PSRpt_PS_View");
                adap.TableMappings.Add("Table1", "PSRpt_Piece_View");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus(ds, "PS");
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
        public string GetExcelReportDataProduct(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            ReportDataProductObject ds = new ReportDataProductObject();

            try
            {

                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("PSRpt_function_GetReportData", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryOrderDetailID", null);
                adap.SelectCommand.Parameters.AddWithValue("@ProductID", id);

                adap.TableMappings.Add("Table", "PSRpt_PS_ProductView");
                adap.TableMappings.Add("Table1", "PSRpt_Piece_ProductView");
                adap.Fill(ds);
                ds.AcceptChanges();

                return Library.Helper.CreateReportFileWithEPPlus(ds, "PSProduct");
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

        public string GetExcelReport2FactoryOrderDetail(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            PSPrintout2 ds = new PSPrintout2();
            string fileName = string.Empty;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PSRpt_function_GetPrintoutPSWithFactoryOrderDetail", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@FactoryOrderDetailID", id);
                adap.TableMappings.Add("Table", "PSRpt_FactoryOrderDetail_View");
                adap.TableMappings.Add("Table1", "PSRpt_ProductDescription_View");
                adap.TableMappings.Add("Table2", "PSRpt_ModelDetail_View");
                adap.TableMappings.Add("Table3", "PSRpt_ModelPiece_View");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "PS_New");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }

        public string GetExcelReport2Product(int id, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            PSPrintout2 ds = new PSPrintout2();
            string fileName = string.Empty;

            try
            {
                SqlDataAdapter adap = new SqlDataAdapter();
                adap.SelectCommand = new SqlCommand("PSRpt_function_GetPrintoutPSWithProduct", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                adap.TableMappings.Add("Table", "PSRpt_Product_View");
                adap.TableMappings.Add("Table1", "PSRpt_ProductDescription_View");
                adap.TableMappings.Add("Table2", "PSRpt_ModelDetail_View");
                adap.TableMappings.Add("Table3", "PSRpt_ModelPiece_View");
                adap.Fill(ds);

                return Library.Helper.CreateReportFileWithEPPlus2(ds, "PS_New");
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return fileName;
        }
    }
}
