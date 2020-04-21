using System;
using System.Collections.Generic;
using System.Linq;

namespace Module.AccountReceivableRpt.DAL
{
    internal partial class DataFactory
    {        
        public DTO.LiabilitiesDTO GetLiabilities(int userId, System.Collections.Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.LiabilitiesDTO dataDebt = new DTO.LiabilitiesDTO();

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
                    dataDebt.dueColorDTOs = AutoMapper.Mapper.Map<List<AccountReceivableRpt_DueColor_View>, List<DTO.DueColorDTO>>(context.AccountReceivableRpt_DueColor_View.ToList());

                    var result = context.AccountReceivableRpt_function_TotalLiabilitiesHTMLView(factoryRawMaterialID, keyRawMaterialID, startDate, endDate).ToList();
                    dataDebt.totalLiabilities = converter.DB2DTO_TotalLiabilities(result).ToList();

                    var resultDetail = context.AccountReceivableRpt_function_SumDetailOfLiabilities(factoryRawMaterialID, keyRawMaterialID, startDate, endDate).ToList();
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
                        var a = context.AccountReceivableRpt_function_DetailOfLiabilities(item.FactoryRawMaterialID, startDate, endDate).OrderByDescending(o => o.DueDay).ToList();
                        listDetail = converter.DB2DTO_DetailOfLiabilities(a);
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
                            var b = context.AccountReceivableRpt_function_DetailOfLiabilitiesByInvoiceNo(invoiceItem.PurchasingInvoiceID, invoiceItem.FactorySaleInvoiceID, startDate, endDate).ToList();
                            receipts = converter.DB2DTO_DetailOfLiabilitiesByInvoice(b);
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
                                    TotalAmount = (invoiceItem.TotalAmount + invoiceItem.BeginningBalance - totalAmountChart) / invoiceItem.ExchangeRate
                                };
                                listChartDetails.Add(list);
                            }

                            //get value List Due Day Catagory Color
                            if (invoiceItem.DueDay > 0)
                            {
                                TotalAmountDueDayBiggerZero += (invoiceItem.TotalAmount + invoiceItem.BeginningBalance - totalAmountChart) / invoiceItem.ExchangeRate;
                                TotalCountDueDayBiggerZero += 1;
                            }
                            else
                            {
                                if (invoiceItem.DueDay == 0)
                                {
                                    TotalAmountDueDayEqualZero += (invoiceItem.TotalAmount + invoiceItem.BeginningBalance - totalAmountChart) / invoiceItem.ExchangeRate;
                                    TotalCountDueDayEqualZero += 1;
                                }
                                else
                                {
                                    TotalAmountDueDaySmallerZero += (invoiceItem.TotalAmount + invoiceItem.BeginningBalance - totalAmountChart) / invoiceItem.ExchangeRate;
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
        public List<DTO.SupportFactoryRawMaterialDTO> QuerySupplier(System.Collections.Hashtable param, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            List<DTO.SupportFactoryRawMaterialDTO> data = new List<DTO.SupportFactoryRawMaterialDTO>();

            try
            {
                string query = param["query"].ToString();
                using (var context = CreateContext())
                {
                    var result = context.AccountReceivableRpt_SupportFactoryRawMaterialSearch_View.Where(o => o.FactoryRawMaterialUD.Contains(query) || o.FactoryRawMaterialOfficialNM.Contains(query)).ToList();
                    data = converter.DB2DTO_SearchSupplier(result).ToList();
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
