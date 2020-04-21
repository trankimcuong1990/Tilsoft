using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.CostInvoiceMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "CostInvoiceMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<CostInvoiceMng_CostInvoice_SearchResultView, DTO.CostInvoiceMng.CostInvoiceSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CostInvoiceMng_CostInvoiceDetailExtend_View, DTO.CostInvoiceMng.CostInvoiceDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CostInvoiceMng_CostInvoiceDetail_View, DTO.CostInvoiceMng.CostInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CostInvoiceDetailExtends, o => o.MapFrom(s => s.CostInvoiceMng_CostInvoiceDetailExtend_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CostInvoiceMng_CostInvoice_View, DTO.CostInvoiceMng.CostInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CostInvoiceDetails, o => o.MapFrom(s => s.CostInvoiceMng_CostInvoiceDetail_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.CostInvoiceMng.CostInvoice, CostInvoice>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CostInvoiceID, o => o.Ignore())
                    .ForMember(d => d.CostInvoiceDetail, o => o.Ignore())
                    .ForMember(d => d.InvoiceDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.CostInvoiceMng.CostInvoiceDetail, CostInvoiceDetail>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CostInvoiceDetailID, o => o.Ignore())
                    .ForMember(d => d.CostInvoiceDetailExtend, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<DTO.CostInvoiceMng.CostInvoiceDetailExtend, CostInvoiceDetailExtend>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.CostInvoiceDetailExtendID, o => o.Ignore())
                    ;

                AutoMapper.Mapper.CreateMap<CostInvoiceMng_InitInfo_View, DTO.CostInvoiceMng.InitInfo>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CostInvoiceMng_InitInfoDetail_View, DTO.CostInvoiceMng.InitInfoDetail>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<CostInvoice_ReportView, DTO.CostInvoiceMng.InvoicePrintout>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.CostInvoiceMng.CostInvoiceSearchResult> DB2DTO_CostInvoiceSearch(List<CostInvoiceMng_CostInvoice_SearchResultView> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CostInvoiceMng_CostInvoice_SearchResultView>, List<DTO.CostInvoiceMng.CostInvoiceSearchResult>>(dbItems);
        }

        public DTO.CostInvoiceMng.CostInvoice DB2DTO_CostInvoice(CostInvoiceMng_CostInvoice_View dbItem)
        {
            DTO.CostInvoiceMng.CostInvoice dtoItem = AutoMapper.Mapper.Map<CostInvoiceMng_CostInvoice_View, DTO.CostInvoiceMng.CostInvoice>(dbItem);

            /*
                FORMAT FIELDS DATETIME
            */
            if (dbItem.ConcurrencyFlag != null)
                dtoItem.ConcurrencyFlag_String = Convert.ToBase64String(dbItem.ConcurrencyFlag);

            if (dbItem.InvoiceDate.HasValue)
                dtoItem.InvoiceDateFormated = dbItem.InvoiceDate.Value.ToString("dd/MM/yyyy");

            if (dbItem.CreatedDate.HasValue)
                dtoItem.CreatedDateFormated = dbItem.CreatedDate.Value.ToString("dd/MM/yyyy");
                        
            if (dbItem.UpdatedDate.HasValue)
                dtoItem.UpdatedDateFormated = dbItem.UpdatedDate.Value.ToString("dd/MM/yyyy");

            return dtoItem;
        }

        public void DTO2DB_CostInvoice(DTO.CostInvoiceMng.CostInvoice dtoItem, ref CostInvoice dbItem)
        {
            List<CostInvoiceDetail> itemNeedDelete_Details = new List<CostInvoiceDetail>();
            List<CostInvoiceDetailExtend> itemNeedDelete_DetailExtends;
            if (dtoItem.CostInvoiceDetails != null)
            {
                //CHECK
                foreach (CostInvoiceDetail dbDetail in dbItem.CostInvoiceDetail)
                {
                    //DB NOT EXIST IN DTO
                    if (!dtoItem.CostInvoiceDetails.Select(s => s.CostInvoiceDetailID).Contains(dbDetail.CostInvoiceDetailID))
                    {
                        itemNeedDelete_Details.Add(dbDetail);
                    }
                    else //DB IS EXIST IN DB
                    {
                        itemNeedDelete_DetailExtends = new List<CostInvoiceDetailExtend>();
                        foreach (CostInvoiceDetailExtend dbDetailExt in dbDetail.CostInvoiceDetailExtend)
                        {
                            if (!dtoItem.CostInvoiceDetails.Where(o => o.CostInvoiceDetailID == dbDetail.CostInvoiceDetailID).FirstOrDefault().CostInvoiceDetailExtends.Select(x => x.CostInvoiceDetailExtendID).Contains(dbDetailExt.CostInvoiceDetailExtendID))
                            {
                                itemNeedDelete_DetailExtends.Add(dbDetailExt);  
                            }
                        }
                        foreach (CostInvoiceDetailExtend dbDetailExt in itemNeedDelete_DetailExtends)
                        {
                            dbDetail.CostInvoiceDetailExtend.Remove(dbDetailExt);
                        }
                    }
                }

                foreach (CostInvoiceDetail dbDetail in itemNeedDelete_Details)
                {
                    List<CostInvoiceDetailExtend> item_deleteds = new List<CostInvoiceDetailExtend>();
                    foreach (CostInvoiceDetailExtend dbDetailExtend in dbDetail.CostInvoiceDetailExtend)
                    {
                        item_deleteds.Add(dbDetailExtend);
                    }
                    foreach (CostInvoiceDetailExtend item in item_deleteds)
                    {
                        dbItem.CostInvoiceDetail.Where(o => o.CostInvoiceDetailID == dbDetail.CostInvoiceDetailID).FirstOrDefault().CostInvoiceDetailExtend.Remove(item);
                    }
                    dbItem.CostInvoiceDetail.Remove(dbDetail);
                }

                //MAP
                CostInvoiceDetail _dbDetail;
                CostInvoiceDetailExtend _dbDetailExtend;
                foreach (DTO.CostInvoiceMng.CostInvoiceDetail dtoDetail in dtoItem.CostInvoiceDetails)
                {
                    dtoDetail.CostType = dtoItem.CostType;
                    dtoDetail.ClientID = dtoItem.ClientID;
                    
                    if (dtoDetail.CostInvoiceDetailID < 0)
                    {
                        _dbDetail = new CostInvoiceDetail();

                        if (dtoDetail.CostInvoiceDetailExtends != null)
                        {
                            foreach (DTO.CostInvoiceMng.CostInvoiceDetailExtend dtoDetailExtend in dtoDetail.CostInvoiceDetailExtends)
                            {
                                _dbDetailExtend = new CostInvoiceDetailExtend();
                                _dbDetail.CostInvoiceDetailExtend.Add(_dbDetailExtend);
                                AutoMapper.Mapper.Map<DTO.CostInvoiceMng.CostInvoiceDetailExtend, CostInvoiceDetailExtend>(dtoDetailExtend, _dbDetailExtend);
                            }
                        }

                        dbItem.CostInvoiceDetail.Add(_dbDetail);
                    }
                    else
                    {
                        _dbDetail = dbItem.CostInvoiceDetail.FirstOrDefault(o => o.CostInvoiceDetailID == dtoDetail.CostInvoiceDetailID);
                        if (_dbDetail != null && dtoDetail.CostInvoiceDetailExtends != null)
                        {
                            foreach (DTO.CostInvoiceMng.CostInvoiceDetailExtend dtoDetailExtend in dtoDetail.CostInvoiceDetailExtends)
                            {
                                if (dtoDetailExtend.CostInvoiceDetailExtendID < 0)
                                {
                                    _dbDetailExtend = new CostInvoiceDetailExtend();
                                    _dbDetail.CostInvoiceDetailExtend.Add(_dbDetailExtend);
                                }
                                else
                                {
                                    _dbDetailExtend = _dbDetail.CostInvoiceDetailExtend.FirstOrDefault(o => o.CostInvoiceDetailExtendID == dtoDetailExtend.CostInvoiceDetailExtendID);
                                }
                                if (_dbDetailExtend != null)
                                {
                                    AutoMapper.Mapper.Map<DTO.CostInvoiceMng.CostInvoiceDetailExtend, CostInvoiceDetailExtend>(dtoDetailExtend, _dbDetailExtend);
                                }
                            }
                        }
                    }

                    if (_dbDetail != null)
                    {
                        AutoMapper.Mapper.Map<DTO.CostInvoiceMng.CostInvoiceDetail, CostInvoiceDetail>(dtoDetail, _dbDetail);
                    }
                }
            }

            AutoMapper.Mapper.Map<DTO.CostInvoiceMng.CostInvoice, CostInvoice>(dtoItem, dbItem);

            if (dtoItem.CostInvoiceID > 0)
            {
                dbItem.UpdatedBy = dtoItem.UpdatedBy;
                dbItem.UpdatedDate = DateTime.Now;
            }
            else
            {
                dbItem.CreatedBy = dtoItem.UpdatedBy;
                dbItem.CreatedDate = DateTime.Now;
            }
            dbItem.InvoiceDate = dtoItem.InvoiceDateFormated.ConvertStringToDateTime();
        }

        public List<DTO.CostInvoiceMng.InitInfo> DB2DTO_InitInfos(List<CostInvoiceMng_InitInfo_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CostInvoiceMng_InitInfo_View>, List<DTO.CostInvoiceMng.InitInfo>>(dbItems);
        }

        public List<DTO.CostInvoiceMng.InitInfoDetail> DB2DTO_InitInfoDetails(List<CostInvoiceMng_InitInfoDetail_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<CostInvoiceMng_InitInfoDetail_View>, List<DTO.CostInvoiceMng.InitInfoDetail>>(dbItems);
        }

        public DTO.CostInvoiceMng.InvoiceContainerPrintout DB2DTO_Printout(CostInvoice_ReportView dbItem)
        {
            DTO.CostInvoiceMng.InvoiceContainerPrintout dtoItem = new DTO.CostInvoiceMng.InvoiceContainerPrintout();
            dtoItem.Invoices = new List<DTO.CostInvoiceMng.InvoicePrintout>();
            dtoItem.InvoiceDetails = new List<DTO.CostInvoiceMng.InvoiceDetailPrintout>();

            

            DTO.CostInvoiceMng.InvoicePrintout dtoInvoice = AutoMapper.Mapper.Map<CostInvoice_ReportView, DTO.CostInvoiceMng.InvoicePrintout>(dbItem);
            dtoItem.Invoices.Add(dtoInvoice);

            //COPY DETATAIL DATA
            DTO.CostInvoiceMng.InvoiceDetailPrintout dtoInvoiceDetail;

            foreach (CostInvoiceDetail_ReportView dbDetail in dbItem.CostInvoiceDetail_ReportView)
            {
                dtoInvoiceDetail = new DTO.CostInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = dbDetail.ContainerNo;
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);

                foreach (CostInvoiceDetailExtend_ReportView dbDetailExtend in dbDetail.CostInvoiceDetailExtend_ReportView)
                {
                    dtoInvoiceDetail = new DTO.CostInvoiceMng.InvoiceDetailPrintout();
                    dtoInvoiceDetail.Description = dbDetailExtend.Description;
                    dtoInvoiceDetail.TotalAmount = dbDetailExtend.Amount;
                    dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
                }

                //CREATE BLANK ROW
                dtoInvoiceDetail = new DTO.CostInvoiceMng.InvoiceDetailPrintout();
                dtoInvoiceDetail.Description = "";
                dtoItem.InvoiceDetails.Add(dtoInvoiceDetail);
            }
            return dtoItem;
        }
    }
}
