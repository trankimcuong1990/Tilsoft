using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.MIEurofarPriceOverviewRpt.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Support.DAL.DataFactory supportFactory = new Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private MIEurofarPriceOverviewRptEntities CreateContext()
        {
            return new MIEurofarPriceOverviewRptEntities(Library.Helper.CreateEntityConnectionString("DAL.MIEurofarPriceOverviewRptModel"));
        }

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
                adap.SelectCommand = new SqlCommand("MIEurofarPriceOverviewRpt_function_GetReportData", new SqlConnection(Library.Helper.GetSQLConnectionString()));
                adap.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                adap.SelectCommand.Parameters.AddWithValue("@Season", Season);
                adap.TableMappings.Add("Table", "MIEurofarPriceOverviewRpt_Overview_View");
                adap.TableMappings.Add("Table1", "MIEurofarPriceOverviewRpt_MarginCustomer_View");
                adap.TableMappings.Add("Table2", "MIEurofarPriceOverviewRpt_Supplier_View");
                adap.TableMappings.Add("Table3", "ReportHeader");
                adap.TableMappings.Add("Table4", "MIEurofarPriceOverviewRpt_SaleManager_View");
                adap.TableMappings.Add("Table5", "MIEurofarPriceOverviewRpt_SalePerformance_View");
                adap.Fill(ds);

                // dev
                //DALBase.Helper.DevCreateReportXMLSource(ds, "MIEurofarPriceOverview");
                //return string.Empty;

                // generate xml file
                //return Library.Helper.CreateReportFiles(ds, "MIEurofarPriceOverview");
                //return Library.Helper.CreateReportFiles(ds, "MIEurofarPriceOverview2");
                return Library.Helper.CreateReportFileWithEPPlus(ds, "MIEurofarPriceOverview2", new List<string>() { "MIEurofarPriceOverviewRpt_Overview_View", "MIEurofarPriceOverviewRpt_MarginCustomer_View", "MIEurofarPriceOverviewRpt_Supplier_View", "MIEurofarPriceOverviewRpt_SaleManager_View", "ReportHeader", "MIEurofarPriceOverviewRpt_SalePerformance_View" });
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

        public DTO.HtmlReportData GetHtmlReportData(string Season, out Library.DTO.Notification notification)
        {
            DTO.HtmlReportData htmlReportData = new DTO.HtmlReportData();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (MIEurofarPriceOverviewRptEntities context = CreateContext())
                {
                    var CustomerData = context.MIEurofarPriceOverviewRpt_MarginCustomer_HTML_View.Where(o => o.Season == Season).ToList();
                    var SupplierData = context.MIEurofarPriceOverviewRpt_Supplier_HTML_View.Where(o => o.Season == Season).ToList();
                    htmlReportData.CustomerData = converter.DB2DTO_CustomerData(CustomerData);
                    htmlReportData.SupplierData = converter.DB2DTO_SupplierData(SupplierData);

                    decimal ExRate = Convert.ToDecimal(supportFactory.GetSettingValue(Season, "ExRate-EUR-USD"), new System.Globalization.CultureInfo("en-US"));

                    //decimal ExRate = 1.16m;

                    // Calculation
                    foreach (DTO.CustomerData dtoCustomer in htmlReportData.CustomerData)
                    {
                        //SaleAmountConvertToEUR
                        if (ExRate != 0)
                        {
                            dtoCustomer.SaleAmountConvertToEUR = dtoCustomer.SaleAmountEUR + (dtoCustomer.SaleAmountUSD / ExRate);
                        }

                        //NetGrossMargin
                        dtoCustomer.NetGrossMargin = dtoCustomer.FOBAmountUSD - dtoCustomer.FOBCostUSD - dtoCustomer.DamagesCost - dtoCustomer.CommissionAmountUSD - dtoCustomer.LCCostAmount - dtoCustomer.InterestAmount - dtoCustomer.InspectionCostUSD - dtoCustomer.SampleCostUSD - dtoCustomer.TransportationInUSD;

                        //NetGrossMarginInPercentage
                        if(dtoCustomer.FOBCostUSD != 0)
                        {
                            dtoCustomer.NetGrossMarginInPercentage = dtoCustomer.NetGrossMargin * 100/ dtoCustomer.FOBCostUSD;
                        }
                    }
                    var dbSaleData = from p in htmlReportData.CustomerData
                                    group p by new
                                    {
                                        p.SaleNM
                                    } into g
                                    select new
                                    {
                                        SaleNM = g.Key.SaleNM,
                                        OrderQnt40HC = g.Sum(x => x.OrderQnt40HC),
                                        FOBCostUSD = g.Sum(x => x.FOBCostUSD),
                                        SaleAmountEUR = g.Sum(x => x.SaleAmountEUR),
                                        SaleAmountUSD = g.Sum(x => x.SaleAmountUSD),
                                        SaleAmountConvertToEUR = g.Sum(x => x.SaleAmountConvertToEUR),
                                        TotalTransportEUR = g.Sum(x => x.TotalTransportEUR),
                                        FOBAmountUSD = g.Sum(x => x.FOBAmountUSD),
                                        CommissionAmountUSD = g.Sum(x => x.CommissionAmountUSD),
                                        ShippedQnt40HC = g.Sum(x => x.ShippedQnt40HC),
                                        ToBeShippedQnt40HC = g.Sum(x => x.ToBeShippedQnt40HC),
                                        LCCostAmount = g.Sum(x => x.LCCostAmount),
                                        InterestAmount = g.Sum(x => x.InterestAmount),
                                        InspectionCostUSD = g.Sum(x => x.InspectionCostUSD),
                                        SampleCostUSD = g.Sum(x => x.SampleCostUSD),
                                        TransportationInUSD = g.Sum(x => x.TransportationInUSD)
                                    };
                    htmlReportData.SaleData = new List<DTO.SaleData>();
                    DTO.SaleData dtoSaleData = new DTO.SaleData();
                    foreach (var item in dbSaleData)
                    {
                        dtoSaleData = new DTO.SaleData();
                        dtoSaleData.SaleNM = item.SaleNM;
                        dtoSaleData.OrderQnt40HC = item.OrderQnt40HC;
                        dtoSaleData.FOBCostUSD = item.FOBCostUSD;
                        dtoSaleData.SaleAmountEUR = item.SaleAmountEUR;
                        dtoSaleData.SaleAmountUSD = item.SaleAmountUSD;
                        dtoSaleData.SaleAmountConvertToEUR = item.SaleAmountConvertToEUR;
                        dtoSaleData.TotalTransportEUR = item.TotalTransportEUR;
                        dtoSaleData.FOBAmountUSD = item.FOBAmountUSD;
                        dtoSaleData.LCCostAmount = item.LCCostAmount;
                        dtoSaleData.InterestAmount = item.InterestAmount;
                        dtoSaleData.InspectionCostUSD = item.InspectionCostUSD;
                        dtoSaleData.TransportationInUSD = item.TransportationInUSD;
                        dtoSaleData.SampleCostUSD = item.SampleCostUSD;

                        if (item.FOBAmountUSD != 0 && item.FOBAmountUSD > 0)
                        {
                            dtoSaleData.CommissionPercentage = item.CommissionAmountUSD / item.FOBAmountUSD * 100;
                        }

                        dtoSaleData.CommissionAmountUSD = item.CommissionAmountUSD;
                        dtoSaleData.DamagesCost = item.FOBCostUSD * 1.25m / 100;
                        dtoSaleData.NetGrossMargin = item.FOBAmountUSD - item.FOBCostUSD - dtoSaleData.DamagesCost - item.CommissionAmountUSD - item.LCCostAmount - item.InterestAmount - item.InspectionCostUSD - item.SampleCostUSD - item.TransportationInUSD;

                        if(item.FOBCostUSD != 0 && item.FOBCostUSD > 0)
                        {
                            dtoSaleData.NetGrossMarginInPercentage = dtoSaleData.NetGrossMargin * 100 / item.FOBCostUSD; 
                        }
                        
                        dtoSaleData.ShippedQnt40HC = item.ShippedQnt40HC;

                        if(item.OrderQnt40HC != 0 && item.OrderQnt40HC > 0)
                        {
                            dtoSaleData.ShippedContInPercentage = item.ShippedQnt40HC / item.OrderQnt40HC * 100;
                            dtoSaleData.ToBeShippedContInPercentage = item.ToBeShippedQnt40HC / item.OrderQnt40HC * 100;
                        }

                        dtoSaleData.ToBeShippedQnt40HC = item.ToBeShippedQnt40HC;
                        htmlReportData.SaleData.Add(dtoSaleData);
                    }
                }
                return htmlReportData;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
                notification.DetailMessage.Add(ex.Message);
                if (ex.GetBaseException() != null)
                {
                    notification.DetailMessage.Add(ex.GetBaseException().Message);
                }
                return htmlReportData;
            }
        }

        public DTO.InitFormData GetInitData(out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.InitFormData data = new DTO.InitFormData();
            data.Seasons = new List<Support.DTO.Season>();

            //try to get data
            try
            {
                data.Seasons = supportFactory.GetSeason().ToList();
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.SalePerformanceDTO> GetSalePerformance(string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            List<DTO.SalePerformanceDTO> data = new List<DTO.SalePerformanceDTO>();
            try
            {
                using (MIEurofarPriceOverviewRptEntities context = CreateContext())
                {
                    data = converter.DB2DTO_SalePerformanceList(context.MIEurofarPriceOverviewRpt_function_GetSalePerformance(season, null).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
    }
}
