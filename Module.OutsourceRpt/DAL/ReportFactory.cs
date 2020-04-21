namespace Module.OutsourceRpt.DAL
{
    internal class ReportFactory
    {
        public string ExportOutsourceReport(int userID, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification();
            notification.Type = Library.DTO.NotificationType.Success;

            string generateCodeFile = string.Empty;

            int? productionTeamID = null;
            string clientUD = string.Empty;
            string proformaInvoiceNo = string.Empty;
            string modelUD = string.Empty;
            string modelNM = string.Empty;
            int? workOrderStatusID = null;
            string productionItemTypeIDs = string.Empty;
            string workOrderUD = string.Empty;

            try
            {
                Framework.DAL.DataFactory fwFactory = new Framework.DAL.DataFactory();
                int? companyID = fwFactory.GetCompanyID(userID);

                if (filters.ContainsKey("productionTeamID") && filters["productionTeamID"] != null)
                {
                    productionTeamID = System.Convert.ToInt32(filters["productionTeamID"]);
                }

                if (filters.ContainsKey("clientUD") && filters["clientUD"] != null)
                {
                    clientUD = filters["clientUD"].ToString();
                }

                if (filters.ContainsKey("proformaInvoiceNo") && filters["proformaInvoiceNo"] != null)
                {
                    proformaInvoiceNo = filters["proformaInvoiceNo"].ToString();
                }

                if (filters.ContainsKey("modelUD") && filters["modelUD"] != null)
                {
                    modelUD = filters["modelUD"].ToString();
                }

                if (filters.ContainsKey("modelNM") && filters["modelNM"] != null)
                {
                    modelNM = filters["modelNM"].ToString();
                }

                if (filters.ContainsKey("workOrderUD") && filters["workOrderUD"] != null)
                {
                    workOrderUD = filters["workOrderUD"].ToString();
                }

                if (filters.ContainsKey("workOrderStatusID") && filters["workOrderStatusID"] != null)
                {
                    workOrderStatusID = System.Convert.ToInt32(filters["workOrderStatusID"]);
                }

                if (filters.ContainsKey("productionItemTypeIDs") && filters["productionItemTypeIDs"] != null)
                {
                    productionItemTypeIDs = filters["productionItemTypeIDs"].ToString();
                }

                OutsourceRptDataSet ds = new OutsourceRptDataSet();
                System.Data.SqlClient.SqlDataAdapter adap = new System.Data.SqlClient.SqlDataAdapter();
                adap.SelectCommand = new System.Data.SqlClient.SqlCommand("OutsourceRpt_function_ExportOutsourceReport", new System.Data.SqlClient.SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;

                if (productionTeamID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@productionTeamID", productionTeamID);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@productionTeamID", System.DBNull.Value);
                }

                if (!string.IsNullOrEmpty(clientUD))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@clientUD", clientUD);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@clientUD", System.DBNull.Value);
                }

                if (!string.IsNullOrEmpty(proformaInvoiceNo))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@proformaInvoiceNo", proformaInvoiceNo);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@proformaInvoiceNo", System.DBNull.Value);
                }

                if (!string.IsNullOrEmpty(modelUD))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@modelUD", modelUD);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@modelUD", System.DBNull.Value);
                }

                if (!string.IsNullOrEmpty(modelNM))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@modelNM", modelNM);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@modelNM", System.DBNull.Value);
                }

                if (!string.IsNullOrEmpty(workOrderUD))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@workOrderUD", workOrderUD);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@workOrderUD", System.DBNull.Value);
                }

                if (workOrderStatusID.HasValue)
                {
                    adap.SelectCommand.Parameters.AddWithValue("@workOrderStatusID", workOrderStatusID);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@workOrderStatusID", System.DBNull.Value);
                }

                if (!string.IsNullOrEmpty(productionItemTypeIDs))
                {
                    adap.SelectCommand.Parameters.AddWithValue("@productionItemTypeIDs", productionItemTypeIDs);
                }
                else
                {
                    adap.SelectCommand.Parameters.AddWithValue("@productionItemTypeIDs", System.DBNull.Value);
                }

                adap.SelectCommand.Parameters.AddWithValue("@companyID", companyID);

                adap.TableMappings.Add("Table", "OutsourceReport");

                adap.Fill(ds);

                generateCodeFile = Library.Helper.CreateReportFileWithEPPlus2(ds, "OutsourceReport");
            }
            catch (System.Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }

            return generateCodeFile;
        }
    }
}
