using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AccountPayableRpt.DAL
{
    internal partial class DataFactory
    {
        public DTO.LiabilitiesDto GetLiabilities(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LiabilitiesDto dataDebt = new DTO.LiabilitiesDto();

            try
            {
                int? factoryRawMaterialID = null;
                string startDate = null;
                string endDate = null;
                int? keyRawMaterialID = null;
                int? dueColorID = null;


                if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                {
                    factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                }
                if (filters.ContainsKey("keyRawMaterialID") && filters["keyRawMaterialID"] != null)
                {
                    keyRawMaterialID = Convert.ToInt32(filters["keyRawMaterialID"]);
                }
                if (filters.ContainsKey("startDate") && !string.IsNullOrEmpty(filters["startDate"].ToString()))
                {
                    startDate = filters["startDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("endDate") && !string.IsNullOrEmpty(filters["endDate"].ToString()))
                {
                    endDate = filters["endDate"].ToString().Replace("'", "''");
                }
                if (filters.ContainsKey("dueColorID") && filters["dueColorID"] != null)
                {
                    dueColorID = Convert.ToInt32(filters["dueColorID"]);
                }

                using (var context = CreateContext())
                {
                    var listChartDetails = new List<DTO.ListChartDetailDTO>();
                    // get due color
                    dataDebt.dueColorDTOs = AutoMapper.Mapper.Map<List< AccountPayableRpt_DueColor_View>, List<DTO.DueColorDTO>>(context.AccountPayableRpt_DueColor_View.ToList());

                    var result = context.AccountPayableRpt_function_TotalLiabilitiesHTMLView(factoryRawMaterialID, keyRawMaterialID, startDate, endDate).ToList();
                    dataDebt.totalLiabilities = converter.DB2DTO_TotalLiabilities(result).ToList();

                    var resultDetail = context.AccountPayableRpt_function_SumDetailOfLiabilities(factoryRawMaterialID, keyRawMaterialID, startDate, endDate).ToList();
                    dataDebt.sumDetailOfLiabilities = converter.DB2DTO_SumLiabilities(resultDetail).ToList();

                    //Bien gan gia tri cho ListCategoryColor
                    decimal? TotalAmountDueDayBiggerZero = 0;
                    decimal? TotalAmountDueDayEqualZero = 0;
                    decimal? TotalAmountDueDaySmallerZero = 0;
                    int TotalCountDueDayBiggerZero = 0;
                    int TotalCountDueDayEqualZero = 0;
                    int TotalCountDueDaySmallerZero = 0;
                    foreach (var item in dataDebt.sumDetailOfLiabilities)
                    {
                        var listDetail = new List<DTO.DetailOfLiabilitiesDto>();
                        listDetail = converter.DB2DTO_DetailOfLiabilities(context.AccountPayableRpt_function_DetailOfLiabilities(item.FactoryRawMaterialID, startDate, endDate).OrderByDescending(o => o.DueDay).ToList());
                        item.DetailOfLiabilitiesDto = new List<DTO.DetailOfLiabilitiesDto>();

                        //add duecolor
                        foreach (var invoiceItem in listDetail.ToList())
                        {
                            //set color for Due Day
                            foreach (var scolor in dataDebt.dueColorDTOs.ToList())
                            {
                                if (scolor.FromDue != null && scolor.ToDue != null)
                                {
                                    if (invoiceItem.DueDay >= scolor.FromDue && invoiceItem.DueDay <= scolor.ToDue)
                                    {
                                        invoiceItem.DueColorUD = "#" + scolor.DueColorUD;
                                        invoiceItem.DueColorID = scolor.DueColorID;
                                        invoiceItem.DueColorNM = scolor.DueColorNM;
                                    }
                                }
                            }
                            //filter duecolor
                            if (dueColorID != null)
                            {
                                if (invoiceItem.DueColorID == dueColorID)
                                {
                                    item.DetailOfLiabilitiesDto.Add(invoiceItem);
                                }
                            }
                            
                        }

                        //filter duecolor
                        if (dueColorID == null)
                        {
                            item.DetailOfLiabilitiesDto = listDetail;
                        }

                        foreach (var invoiceItem in item.DetailOfLiabilitiesDto)
                        {
                            List<DTO.DetailOfLiabilitiesByInvoiceNoDto> receipts = new List<DTO.DetailOfLiabilitiesByInvoiceNoDto>();
                            receipts = converter.DB2DTO_DetailOfLiabilitiesByInvoice(context.AccountPayableRpt_function_DetailOfLiabilitiesByInvoiceNo(invoiceItem.PurchaseInvoiceID, startDate, endDate).ToList());
                            invoiceItem.DetailOfLiabilitiesByInvoiceNoDto = receipts;
                            invoiceItem.NumberRowDetail = invoiceItem.DetailOfLiabilitiesByInvoiceNoDto.Count;
                            decimal? totalAmountChart = 0;
                            int totalCount = 0;
                            foreach (var zItem in invoiceItem.DetailOfLiabilitiesByInvoiceNoDto.ToList())
                            {
                                totalAmountChart += zItem.AmountByCurrency;
                            }
                            totalCount = 1;

                            if (invoiceItem.DueColorID != null)
                            {
                                // Quotation Offer
                                DTO.ListChartDetailDTO list = new DTO.ListChartDetailDTO()
                                {
                                    DueColorID = invoiceItem.DueColorID,
                                    DueColorUD = invoiceItem.DueColorUD,
                                    DueColorNM = invoiceItem.DueColorNM,
                                    DueColorDate = invoiceItem.DueColorDate,
                                    ToDue = invoiceItem.ToDue,
                                    TotalCount = totalCount,
                                    TotalAmount = (invoiceItem.ToTalInvoiceAmount - invoiceItem.BeginningBalance - invoiceItem.TotalAmount - invoiceItem.TotalRealDeposit) / invoiceItem.ExchangeRate
                                };
                                listChartDetails.Add(list);
                            }

                            //get value List Due Day Catagory Color
                            if(invoiceItem.DueDay > 0)
                            {
                                TotalAmountDueDayBiggerZero += (invoiceItem.ToTalInvoiceAmount - invoiceItem.BeginningBalance - invoiceItem.TotalAmount - invoiceItem.TotalRealDeposit) / invoiceItem.ExchangeRate;
                                TotalCountDueDayBiggerZero += 1;
                            }
                            else
                            {
                                if(invoiceItem.DueDay == 0)
                                {
                                    TotalAmountDueDayEqualZero += (invoiceItem.ToTalInvoiceAmount - invoiceItem.BeginningBalance - invoiceItem.TotalAmount - invoiceItem.TotalRealDeposit) / invoiceItem.ExchangeRate;
                                    TotalCountDueDayEqualZero += 1;
                                }
                                else
                                {
                                    TotalAmountDueDaySmallerZero += (invoiceItem.ToTalInvoiceAmount - invoiceItem.BeginningBalance - invoiceItem.TotalAmount - invoiceItem.TotalRealDeposit) / invoiceItem.ExchangeRate;
                                    TotalCountDueDaySmallerZero += 1;
                                }
                            }
                        }
                    }

                    //Add ListCategoryColor
                    dataDebt.ListColorCatagoryDTOs = new List<DTO.ListColorCatagoryDTO>();
                    var listCategory1 = new DTO.ListColorCatagoryDTO();
                    listCategory1.ColorID = 1;
                    listCategory1.ColorName = "Red";
                    listCategory1.ColorUD = "#b90000";
                    listCategory1.TotalCount = TotalCountDueDayBiggerZero;
                    listCategory1.TotalAmount = TotalAmountDueDayBiggerZero;
                    dataDebt.ListColorCatagoryDTOs.Add(listCategory1);
                    var listCategory2 = new DTO.ListColorCatagoryDTO();
                    listCategory2.ColorID = 2;
                    listCategory2.ColorName = "White";
                    listCategory2.ColorUD = "#fffeff";
                    listCategory2.TotalCount = TotalCountDueDayEqualZero;
                    listCategory2.TotalAmount = TotalAmountDueDayEqualZero;
                    dataDebt.ListColorCatagoryDTOs.Add(listCategory2);
                    var listCategory3 = new DTO.ListColorCatagoryDTO();
                    listCategory3.ColorID = 3;
                    listCategory3.ColorName = "Green";
                    listCategory3.ColorUD = "#01bb01";
                    listCategory3.TotalCount = TotalCountDueDaySmallerZero;
                    listCategory3.TotalAmount = TotalAmountDueDaySmallerZero;
                    dataDebt.ListColorCatagoryDTOs.Add(listCategory3);
                    //----------------------------------------------------------------------------------------

                    var listChartDetailDTOs = from ListChartDetailDTO in listChartDetails
                                                   orderby ListChartDetailDTO.ToDue descending
                                                   group ListChartDetailDTO by ListChartDetailDTO.DueColorID into listGroup
                                                   select new
                                                   {
                                                       DueColorID = listGroup.Key,
                                                       TotalAmount = listGroup.Sum(x => x.TotalAmount),
                                                       TotalCount = listGroup.Sum(x => x.TotalCount)
                                                   };
                    dataDebt.listChartDetailDTOs = new List<DTO.ListChartDetailDTO>();
                    foreach (var item in listChartDetailDTOs.ToList())
                    {
                        //get name of listChartDetails 
                        var dueColorDate = "";
                        var dueColorCode = "";
                        var dueColorName = "";
                        int? toDue = null;
                        for (int i = 0; i < listChartDetails.Count; i++)
                        {
                            var aItem = listChartDetails[i];
                            if (item.DueColorID == aItem.DueColorID)
                            {
                                dueColorDate = aItem.DueColorDate;
                                dueColorCode = aItem.DueColorUD;
                                dueColorName = aItem.DueColorNM;
                                toDue = aItem.ToDue;
                                break;
                            }
                        }
                        DTO.ListChartDetailDTO subItem = new DTO.ListChartDetailDTO()
                        {
                            DueColorID = item.DueColorID,
                            DueColorUD = dueColorCode,
                            DueColorNM = dueColorName,
                            DueColorDate = dueColorDate,
                            TotalAmount = item.TotalAmount,
                            TotalCount = item.TotalCount,
                            ToDue = toDue
                        };
                        dataDebt.listChartDetailDTOs.Add(subItem);
                    }

                    return dataDebt;
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return dataDebt;
            }
        }
        public List<DTO.PurchaseInvoiceDTO> GetPurchaseInvoice(int userID, System.Collections.Hashtable filters,string sortedBy, string sortedDirection, out Library.DTO.Notification notification)
        {
           
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };

            List<DTO.PurchaseInvoiceDTO> data = new List<DTO.PurchaseInvoiceDTO>();

            try
            {
                using (var context = CreateContext())
                {
                    
                    string purchaseInvoiceUD = null;
                    string invoiceNo = null;
                    int? factoryRawMaterialID = null;                  
                    int? dueday = null;
                    int? keyRawMaterialID = null;


                    if (filters.ContainsKey("purchaseInvoiceUD") && !string.IsNullOrEmpty(filters["purchaseInvoiceUD"].ToString()))
                    {
                        purchaseInvoiceUD = filters["purchaseInvoiceUD"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("invoiceNo") && !string.IsNullOrEmpty(filters["invoiceNo"].ToString()))
                    {
                        invoiceNo = filters["invoiceNo"].ToString().Replace("'", "''");
                    }
                    if (filters.ContainsKey("factoryRawMaterialID") && filters["factoryRawMaterialID"] != null)
                    {
                        factoryRawMaterialID = Convert.ToInt32(filters["factoryRawMaterialID"]);
                    }
                    if (filters.ContainsKey("dueday") && filters["dueday"] != null)
                    {
                        dueday = Convert.ToInt32(filters["dueday"]);
                    }
                    if (filters.ContainsKey("keyRawMaterialID") && filters["keyRawMaterialID"] != null)
                    {
                        keyRawMaterialID = Convert.ToInt32(filters["keyRawMaterialID"]);
                    }
                    var result = context.AccountPayableRpt_function_PurchaseInvoice( purchaseInvoiceUD, invoiceNo, factoryRawMaterialID, dueday, keyRawMaterialID, sortedBy, sortedDirection).Where(o=>o.TotalRemain > 0).ToList();
                    data = converter.DB2DTO_PurchaseInvoice(result).ToList();
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification()
                {
                    Type = Library.DTO.NotificationType.Error,
                    Message = ex.Message

                };
            }
            return data;
        }
        //public DTO.li GetCongNoPhaiThu(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        //{
        //    notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
        //    DTO.CongNoPhaiThuDto dataCongNo = new DTO.CongNoPhaiThuDto();

        //    try
        //    {
        //        int? clientID = null;
        //        string startDate = null;
        //        string endDate = null;


        //        if (filters.ContainsKey("clientID") && filters["clientID"] != null)
        //        {
        //            clientID = Convert.ToInt32(filters["clientID"]);
        //        }
        //        if (filters.ContainsKey("startDate") && !string.IsNullOrEmpty(filters["startDate"].ToString()))
        //        {
        //            startDate = filters["startDate"].ToString().Replace("'", "''");
        //        }
        //        if (filters.ContainsKey("endDate") && !string.IsNullOrEmpty(filters["endDate"].ToString()))
        //        {
        //            endDate = filters["endDate"].ToString().Replace("'", "''");
        //        }

        //        using (var context = CreateContext())
        //        {
        //            var result = context.LiabilitiesRpt_Function_TongHopCongNoPhaiThuHTMLView(clientID, startDate, endDate).ToList();
        //            dataCongNo.tongHopCongNoPhaiThus = converter.DB2DTO_TongHopCongNoPhaiThu(result).ToList();

        //            var resultChiTiet = context.LiabilitiesRpt_Function_SumChiTietCongNoPhaiThu(clientID, startDate, endDate).ToList();
        //            dataCongNo.sumChiTietPhaiThuDtos = converter.DB2DTO_SumCongNoPhaiThu(resultChiTiet).ToList();
        //            foreach (var item in dataCongNo.sumChiTietPhaiThuDtos)
        //            {
        //                List<DTO.ChiTietPhaiThuDto> chiTiets = new List<DTO.ChiTietPhaiThuDto>();
        //                chiTiets = converter.DB2DTO_ChiTietCongNoPhaiThu(context.LiabilitiesRpt_Function_ChiTietCongNoPhaiThu(item.ClientID, startDate, endDate).ToList());
        //                item.ChiTietPhaiThuDtos = chiTiets;

        //                foreach (var invoiceItem in item.ChiTietPhaiThuDtos)
        //                {
        //                    List<DTO.ChiTietPhaiThuByInvoiceNoDto> receipts = new List<DTO.ChiTietPhaiThuByInvoiceNoDto>();
        //                    receipts = converter.DB2DTO_ChiTietCongNoPhaiThuByInvoice(context.LiabilitiesRpt_Function_ChiTietPhaiThuByInvoiceNo(invoiceItem.ECommercialInvoiceID, startDate, endDate).ToList());
        //                    invoiceItem.ChiTietPhaiThuByInvoiceNoDtos = receipts;
        //                    invoiceItem.NumberRowDetail = invoiceItem.ChiTietPhaiThuByInvoiceNoDtos.Count;
        //                }
        //            }

        //            return dataCongNo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        notification.Type = Library.DTO.NotificationType.Error;
        //        notification.Message = Library.Helper.GetInnerException(ex).Message;
        //        return dataCongNo;
        //    }
        //}

        public List<DTO.SupplierSupport> QuerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SupplierSupport> data = new List<DTO.SupplierSupport>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.AccountPayableRpt_SupportFactoryRawMaterialSearch_View.Where(o => o.FactoryRawMaterialUD.Contains(query) || o.FactoryRawMaterialOfficialNM.Contains(query)).ToList();
                    data = converter.DB2DTO_Supplier(result).ToList();
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }
        public List<DTO.ClientSupport> QueryCustomer(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.ClientSupport> data = new List<DTO.ClientSupport>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.AccountPayableRpt_Client_View.Where(o => o.ClientUD.Contains(query) || o.ClientNM.Contains(query)).ToList();
                    data = converter.DB2DTO_Client(result).ToList();
                }
                return data;
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
                return data;
            }
        }
    }
}
