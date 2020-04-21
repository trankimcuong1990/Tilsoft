using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Support
{
    public class DataFactory
    {
        private DataConverter converter = new DataConverter();
        private Module.Framework.DAL.DataFactory fw_factory = new Module.Framework.DAL.DataFactory();
        private string seasonHack = "2019/2020";

        private SupportEntities CreateContext()
        {
            return new SupportEntities(DALBase.Helper.CreateEntityConnectionString("SupportModel"));
        }

        public IEnumerable<DTO.Support.InternalCompany3> GetInternalCompany3()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_InternalCompany3_View>, List<DTO.Support.InternalCompany3>>(context.SupportMng_InternalCompany3_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.InternalCompany3>();
            }
        }

        public List<DTO.Support.LoadingPlan> QuickSearchLoadingPlan(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            var data = new List<DTO.Support.LoadingPlan>();
            try
            {
                using (var context = CreateContext())
                {
                    string queryString;
                    TryParseFilter(filters, "searchQuery", out queryString);

                    totalRows = context.SupportMng_function_QuickSearchLoadingPlan(orderBy, orderDirection, queryString).Count();
                    var result = context.SupportMng_function_QuickSearchLoadingPlan(orderBy, orderDirection, queryString);
                    data = converter.DB2DTO_LoadingPlan(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }
        
        public List<DTO.Support.SaleOrder> QuickSearchSaleOrder(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            var data = new List<DTO.Support.SaleOrder>();
            try
            {
                using (var context = CreateContext())
                {
                    string queryString;
                    TryParseFilter(filters, "searchQuery", out queryString);

                    totalRows = context.SupportMng_function_QuickSearchSaleOrder(orderBy, orderDirection, queryString).Count();
                    var result = context.SupportMng_function_QuickSearchSaleOrder(orderBy, orderDirection, queryString);
                    data = converter.DB2DTO_SaleOrder(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.ProductType> GetProductType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductType(context.List_ProductType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ProductType>();
            }
        }

        public IEnumerable<DTO.Support.Season> GetSeason()
        {
            List<DTO.Support.Season> data = new List<DTO.Support.Season>();
            for (int i = 2005; i <= DateTime.Now.Year + 1; i++)
            {
                data.Add(new DTO.Support.Season() { SeasonValue = i.ToString() + "/" + (i + 1).ToString(), SeasonText = i.ToString() + "/" + (i + 1).ToString() });
            }
            var result = data.OrderByDescending(f => f.SeasonValue);

            return result;
        }

        public IEnumerable<DTO.Support.StringCollectionItem> GetOceanFreight()
        {
            List<DTO.Support.StringCollectionItem> result = new List<DTO.Support.StringCollectionItem>();
            result.Add(new DTO.Support.StringCollectionItem() { ItemValue = "P", ItemText = "Prepaid" });
            result.Add(new DTO.Support.StringCollectionItem() { ItemValue = "C", ItemText = "Collect" });

            return result;
        }

        public IEnumerable<DTO.Support.YesNo> GetYesNo()
        {
            List<DTO.Support.YesNo> result = new List<DTO.Support.YesNo>();
            result.Add(new DTO.Support.YesNo() { YesNoText = "Yes", YesNoValue = "true" });
            result.Add(new DTO.Support.YesNo() { YesNoText = "No", YesNoValue = "false" });

            return result;
        }

        public IEnumerable<DTO.Support.Status> GetStatuses()
        {
            List<DTO.Support.Status> result = new List<DTO.Support.Status>();
            result.Add(new DTO.Support.Status() { StatusText = "PENDING", StatusValue = "PENDING" });
            result.Add(new DTO.Support.Status() { StatusText = "CANCELED", StatusValue = "CANCELED" });
            result.Add(new DTO.Support.Status() { StatusText = "CONFIRMED", StatusValue = "CONFIRMED" });

            return result;
        }

        public IEnumerable<DTO.Support.Model> GetModel()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Model(context.List_Model_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Model>();
            }
        }

        public IEnumerable<DTO.Support.ManufacturerCountry> GetManufacturerCountry()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ManufacturerCountry(context.List_ManufacturerCountry_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ManufacturerCountry>();
            }
        }

        public IEnumerable<DTO.Support.WareHouse> GetWareHouse()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_WareHouse(context.List_WareHouse_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.WareHouse>();
            }
        }

        public IEnumerable<DTO.Support.Country> GetCountry()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Country(context.List_Country_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Country>();
            }
        }

        public IEnumerable<DTO.Support.ProductStatus> GetProductStatus()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductStatus(context.SupportMng_ProductStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ProductStatus>();
            }
        }

        public IEnumerable<DTO.Support.Currency> GetCurrency()
        {
            List<DTO.Support.Currency> result = new List<DTO.Support.Currency>();
            result.Add(new DTO.Support.Currency() { CurrencyValue = "USD", CurrencyText = "USD" });
            result.Add(new DTO.Support.Currency() { CurrencyValue = "EUR", CurrencyText = "EUR" });
            return result;
        }

        public IEnumerable<DTO.Support.DeliveryTerm> GetDeliveryTerm()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_DeliveryTerm(context.List_DeliveryTerm.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.DeliveryTerm>();
            }
        }

        public IEnumerable<DTO.Support.PaymentTerm> GetPaymentTerm()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentTerm(context.List_PaymentTerm.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PaymentTerm>();
            }
        }

        public IEnumerable<DTO.Support.PaymentMethod> GetPaymentMethod()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentMethod(context.List_PaymentMethod.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PaymentMethod>();
            }
        }

        public IEnumerable<DTO.Support.PaymentType> GetPaymentType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_PaymentType(context.List_PaymentType.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PaymentType>();
            }
        }

        public IEnumerable<DTO.Support.Booking> GetBooking()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Booking(context.SupportMng_Booking_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Booking>();
            }
        }

        public IEnumerable<DTO.Support.FeeType> GetFeeType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_FeeType(context.List_FeeType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.FeeType>();
            }
        }

        public IEnumerable<DTO.Support.Factory> GetFactory()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Factory(context.List_Factory.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Factory>();
            }
        }

        public IEnumerable<DTO.Support.Saler> GetSaler()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Saler(context.List_Sale.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Saler>();
            }
        }

        public IEnumerable<DTO.Support.Agents> GetAgents()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Agents(context.List_Agents.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Agents>();
            }
        }
        public IEnumerable<DTO.Support.User> GetUser()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Saler(context.List_Users.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.User>();
            }
        }

        public IEnumerable<DTO.Support.Client> GetClient()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Client(context.List_Client_View.OrderBy(o => o.ClientNM).ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Client>();
            }
        }

        public IEnumerable<DTO.Support.Forwarder> GetForwarder()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Forwarder(context.List_Forwarder_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Forwarder>();
            }
        }

        public IEnumerable<DTO.Support.PackagingMethod> GetPackagingMethod()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_PackagingMethod(context.List_PackagingMethod_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PackagingMethod>();
            }
        }

        public IEnumerable<DTO.Support.PutInProductionTerm> GetPutInProductionTerm()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_PutInProductionTerm(context.List_PutInProductionTerm_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PutInProductionTerm>();
            }
        }

        public IEnumerable<DTO.Support.LabelingType> GetLabelingType()
        {
            List<DTO.Support.LabelingType> result = new List<DTO.Support.LabelingType>();
            result.Add(new DTO.Support.LabelingType() { LabelingTypeValue = "ST", LabelingTypeText = "STANDARD" });
            result.Add(new DTO.Support.LabelingType() { LabelingTypeValue = "SP", LabelingTypeText = "SPECIAL" });
            return result;
        }

        public IEnumerable<DTO.Support.PackagingType> GetPackagingType()
        {
            List<DTO.Support.PackagingType> result = new List<DTO.Support.PackagingType>();
            result.Add(new DTO.Support.PackagingType() { PackagingTypeValue = "ST", PackagingTypeText = "STANDARD" });
            result.Add(new DTO.Support.PackagingType() { PackagingTypeValue = "SP", PackagingTypeText = "SPECIAL" });
            return result;
        }

        public IEnumerable<DTO.Support.POD> GetPOD()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_POD(context.List_POD_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.POD>();
            }
        }

        public IEnumerable<DTO.Support.POL> GetPOL()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_POL(context.List_POL_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.POL>();
            }
        }

        public IEnumerable<DTO.Support.MaterialOption> GetMaterialOption()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_MaterialOption(context.List_MaterialOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.MaterialOption>();
            }
        }

        public IEnumerable<DTO.Support.FrameMaterial> GetFrameMaterial()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_FrameMaterial(context.List_FrameMaterial_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.FrameMaterial>();
            }
        }

        public IEnumerable<DTO.Support.WarehouseImportType> GetWarehouseImportType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_WarehouseImportType(context.SupportMng_WarehouseImportType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.WarehouseImportType>();
            }
        }

        public IEnumerable<DTO.Support.Supplier> GetSupplier()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Supplier(context.SupportMng_Supplier_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Supplier>();
            }
        }

        public IEnumerable<DTO.Support.MovementTerm> GetMovementTerm()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_MovementTerm(context.List_MovementTerm_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.MovementTerm>();
            }
        }

        public IEnumerable<DTO.Support.Material> GetMaterial()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Material(context.List_Material_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Material>();
            }
        }

        public IEnumerable<DTO.Support.MaterialType> GetMaterialType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_MaterialType(context.List_MaterialType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.MaterialType>();
            }
        }

        public IEnumerable<DTO.Support.MaterialColor> GetMaterialColor()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_MaterialColor(context.LIst_MaterialColor_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.MaterialColor>();
            }
        }

        public IEnumerable<DTO.Support.Cushion> GetCushion()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Cushion(context.LIst_Cushion_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Cushion>();
            }
        }

        public IEnumerable<DTO.Support.CushionColor> GetCushionColor()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_CushionColor(context.List_CushionColor_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.CushionColor>();
            }
        }

        public IEnumerable<DTO.Support.Location> GetLocation()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Location(context.List_Location_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Location>();
            }
        }

        public IEnumerable<DTO.Support.ContainerType> GetContainerType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ContainerType(context.List_ContainerType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ContainerType>();
            }
        }

        public IEnumerable<DTO.Support.FrameMaterialColor> GetFrameMaterialColor()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_FrameMaterialColor(context.List_FrameMaterialColor_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.FrameMaterialColor>();
            }
        }

        public IEnumerable<DTO.Support.SubMaterial> GetSubMaterial()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SubMaterial(context.List_SubMaterial_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SubMaterial>();
            }
        }

        public IEnumerable<DTO.Support.SubMaterialColor> GetSubMaterialColor()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SubMaterialColor(context.List_SubMaterialColor_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SubMaterialColor>();
            }
        }

        public IEnumerable<DTO.Support.ProductGroup> GetProductGroup()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductGroup(context.SupportMng_ProductGroup_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ProductGroup>();
            }
        }

        public IEnumerable<DTO.Support.CushionType> GetCushionType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_CushionType(context.SupportMng_CushionType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.CushionType>();
            }
        }


        public IEnumerable<DTO.Support.BackCushionOptionRaw> GetBackCushionOptionRaw()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_BackCushionOptionRaw(context.SupportMng_BackCushionOptionRaw_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.BackCushionOptionRaw>();
            }
        }

        public IEnumerable<DTO.Support.SeatCushionOptionRaw> GetSeatCushionOptionRaw()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SeatCushionOptionRaw(context.SupportMng_SeatCushionOptionRaw_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SeatCushionOptionRaw>();
            }
        }

        public IEnumerable<DTO.Support.CushionColorOptionRaw> GetCushionColorOptionRaw()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_CushionColorOptionRaw(context.SupportMng_CushionColorOptionRaw_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.CushionColorOptionRaw>();
            }
        }

        public IEnumerable<DTO.Support.MaterialTypeOptionRaw> GetMaterialTypeOptionRaw()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_MaterialTypeOptionRaw(context.SupportMng_MaterialTypeOptionRaw_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.MaterialTypeOptionRaw>();
            }
        }

        public IEnumerable<DTO.Support.MaterialColorOptionRaw> GetMaterialColorOptionRaw()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_MaterialColorOptionRaw(context.SupportMng_MaterialColorOptionRaw_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.MaterialColorOptionRaw>();
            }
        }

        public IEnumerable<DTO.Support.GalleryItemType> GetGalleryItemType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_GalleryItemType(context.List_GalleryItemType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.GalleryItemType>();
            }
        }

        public IEnumerable<DTO.Support.SeasonType> GetSeasonType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SeasonType(context.List_SeasonType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SeasonType>();
            }
        }
        public IEnumerable<DTO.Support.ProductCategory> GetProductCategories()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductCategory(context.SupportMng_ProductCategory_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ProductCategory>();
            }
        }

        public IEnumerable<DTO.Support.SampleOrderStatus> GetSampleOrderStatus()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SampleOrderStatus(context.SupportMng_SampleOrderStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SampleOrderStatus>();
            }
        }


        public IEnumerable<DTO.Support.SamplePurpose> GetSamplePurpose()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SamplePurpose(context.SupportMng_SamplePurpose_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SamplePurpose>();
            }
        }

        public IEnumerable<DTO.Support.SampleRemarkStatus> GetSampleRemarkStatus()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SampleRemarkStatus(context.SupportMng_SampleRemarkStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SampleRemarkStatus>();
            }
        }

        public IEnumerable<DTO.Support.SampleRequestType> GetSampleRequestType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SampleRequestType(context.SupportMng_SampleRequestType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SampleRequestType>();
            }
        }

        public IEnumerable<DTO.Support.SampleStatus> GetSampleStatus()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SampleStatus(context.SupportMng_SampleStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SampleStatus>();
            }
        }

        public IEnumerable<DTO.Support.SampleTransportType> GetSampleTransportType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SampleTransportType(context.SupportMng_SampleTransportType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SampleTransportType>();
            }
        }

        //
        // support function with parameters
        //
        public IEnumerable<DTO.Support.Model> QuickSearchModel(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.Model> data = new List<DTO.Support.Model>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string modelUD = string.Empty;
                    string modelNM = string.Empty;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        modelUD = filters["searchQuery"].ToString().Replace("'", "''");
                        modelNM = modelUD;
                    }

                    totalRows = context.Support_function_QuickSearchModel(modelUD, modelNM, orderBy, orderDirection).Count();
                    var result = context.Support_function_QuickSearchModel(modelUD, modelNM, orderBy, orderDirection);
                    data = converter.DB2DTO_Model(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.ModelMaterialOption> GetModelMaterialOption(int productGroupID)
        {
            List<DTO.Support.ModelMaterialOption> result = new List<DTO.Support.ModelMaterialOption>();
            List<SupportMng_ModelMaterial_View> dbResult;
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {                    
                    if (productGroupID == 0)
                    {
                        dbResult = context.SupportMng_ModelMaterial_View.OrderBy(o => o.DisplayIndex).ToList();                        
                    }
                    else
                    {
                        dbResult = context.SupportMng_ModelMaterial_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList();
                    }
                    dbResult.Where(o => o.Season != seasonHack).ToList().ForEach(o => o.IsStandard = false);
                    result.AddRange(converter.DB2DTO_ModelMaterialOption(dbResult));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelMaterialTypeOption> GetModelMaterialTypeOption(int productGroupID)
        {
            List<DTO.Support.ModelMaterialTypeOption> result = new List<DTO.Support.ModelMaterialTypeOption>();
            List<SupportMng_ModelMaterialType_View> dbResult;            
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    dbResult = context.SupportMng_ModelMaterialType_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList();
                    dbResult.Where(o => o.Season != seasonHack).ToList().ForEach(o => o.IsStandard = false);
                    foreach (DTO.Support.ModelMaterialTypeOption typeOption in converter.DB2DTO_ModelMaterialTypeOption(dbResult))
                    {
                        if (result.Count(o => o.MaterialID == typeOption.MaterialID && o.MaterialTypeID == typeOption.MaterialTypeID && o.IsStandard == typeOption.IsStandard) == 0)
                        {
                            result.Add(typeOption);
                        }
                    }
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelMaterialColorOption> GetModelMaterialColorOption(int productGroupID)
        {
            List<DTO.Support.ModelMaterialColorOption> result = new List<DTO.Support.ModelMaterialColorOption>();
            List<SupportMng_ModelMaterialColor_View> dbResult;
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    dbResult = context.SupportMng_ModelMaterialColor_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList();
                    dbResult.Where(o => o.Season != seasonHack).ToList().ForEach(o => o.IsStandard = false);
                    result.AddRange(converter.DB2DTO_ModelMaterialColorOption(dbResult));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelFrameMaterialOption> GetModelFrameMaterialOption(int productGroupID)
        {
            List<DTO.Support.ModelFrameMaterialOption> result = new List<DTO.Support.ModelFrameMaterialOption>();
            List<SupportMng_ModelFrameMaterial_View> dbResult;
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    dbResult = context.SupportMng_ModelFrameMaterial_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList();
                    dbResult.Where(o => o.Season != seasonHack).ToList().ForEach(o => o.IsStandard = false);
                    result.AddRange(converter.DB2DTO_ModelFrameMaterialOption(dbResult));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelFrameMaterialColorOption> GetModelFrameMaterialColorOption(int productGroupID)
        {
            List<DTO.Support.ModelFrameMaterialColorOption> result = new List<DTO.Support.ModelFrameMaterialColorOption>();
            List<SupportMng_ModelFrameMaterialColor_View> dbResult;
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    dbResult = context.SupportMng_ModelFrameMaterialColor_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList();
                    dbResult.Where(o => o.Season != seasonHack).ToList().ForEach(o => o.IsStandard = false);
                    result.AddRange(converter.DB2DTO_ModelFrameMaterialColorOption(dbResult));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelSubMaterialOption> GetModelSubMaterialOption(int modelID)
        {
            List<DTO.Support.ModelSubMaterialOption> result = new List<DTO.Support.ModelSubMaterialOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    foreach (DTO.Support.ModelSubMaterialOption subOption in converter.DB2DTO_ModelSubMaterialOption(context.SupportMng_ModelSubMaterial_View.Where(o => o.ModelID == modelID).OrderBy(o => o.DisplayIndex).ToList()))
                    {
                        if (result.Count(o => o.SubMaterialID == subOption.SubMaterialID) == 0)
                        {
                            result.Add(subOption);
                        }
                    }
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelSubMaterialColorOption> GetModelSubMaterialColorOption(int modelID)
        {
            List<DTO.Support.ModelSubMaterialColorOption> result = new List<DTO.Support.ModelSubMaterialColorOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelSubMaterialColorOption(context.SupportMng_ModelSubMaterialColor_View.Where(o => o.ModelID == modelID).OrderBy(o => o.DisplayIndex).ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelCushionOption> GetModelCushionOption(int modelID)
        {
            List<DTO.Support.ModelCushionOption> result = new List<DTO.Support.ModelCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelCushionOption(context.SupportMng_ModelCushion_View.Where(o => o.ModelID == modelID).OrderBy(o => o.DisplayIndex).ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelCushionTypeOption> GetModelCushionTypeOption(int productGroupID)
        {
            List<DTO.Support.ModelCushionTypeOption> result = new List<DTO.Support.ModelCushionTypeOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    foreach (DTO.Support.ModelCushionTypeOption type in converter.DB2DTO_ModelCushionTypeOption(context.SupportMng_ModelCushionType_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList()))
                    {
                        if (result.Count(o => o.CushionTypeID == type.CushionTypeID && o.IsStandard == type.IsStandard) == 0)
                        {
                            result.Add(type);
                        }
                    }
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelCushionColorOption> GetModelCushionColorOption(int productGroupID)
        {
            List<DTO.Support.ModelCushionColorOption> result = new List<DTO.Support.ModelCushionColorOption>();
            List<SupportMng_ModelCushionColor_View> dbResult;
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    dbResult = context.SupportMng_ModelCushionColor_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList();
                    dbResult.Where(o => o.Season != seasonHack).ToList().ForEach(o => o.IsStandard = false);
                    result.AddRange(converter.DB2DTO_ModelCushionColorOption(dbResult));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelBackCushionOption> GetModelBackCushionOption(int productGroupID)
        {
            List<DTO.Support.ModelBackCushionOption> result = new List<DTO.Support.ModelBackCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(AutoMapper.Mapper.Map<List<SupportMng_ModelBackCushion_View>, List<DTO.Support.ModelBackCushionOption>>(context.SupportMng_ModelBackCushion_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelBackCushionOption> GetModelNoneBackCushionOption()
        {
            List<DTO.Support.ModelBackCushionOption> result = new List<DTO.Support.ModelBackCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(AutoMapper.Mapper.Map<List<SupportMng_ModelNoneBackCushion_View>, List<DTO.Support.ModelBackCushionOption>>(context.SupportMng_ModelNoneBackCushion_View.ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelSeatCushionOption> GetModelSeatCushionOption(int productGroupID)
        {
            List<DTO.Support.ModelSeatCushionOption> result = new List<DTO.Support.ModelSeatCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(AutoMapper.Mapper.Map<List<SupportMng_ModelSeatCushion_View>, List<DTO.Support.ModelSeatCushionOption>>(context.SupportMng_ModelSeatCushion_View.Where(o => o.ProductGroupID == productGroupID).OrderBy(o => o.DisplayIndex).ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelSeatCushionOption> GetModelNoneSeatCushionOption()
        {
            List<DTO.Support.ModelSeatCushionOption> result = new List<DTO.Support.ModelSeatCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(AutoMapper.Mapper.Map<List<SupportMng_ModelNoneSeatCushion_View>, List<DTO.Support.ModelSeatCushionOption>>(context.SupportMng_ModelNoneSeatCushion_View.ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelPackagingMethodOption> GetModelPackagingMethodOption(int modelID)
        {
            List<DTO.Support.ModelPackagingMethodOption> result = new List<DTO.Support.ModelPackagingMethodOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelPackagingMethodOption(context.SupportMng_ModelPackagingMethod_View.Where(o => o.ModelID == modelID).ToList()));
                }
            }
            catch { }

            return result;
        }


        // GET NONE MATERIAL
        public IEnumerable<DTO.Support.ModelFrameMaterialOption> GetModelNoneFrameMaterialOption()
        {
            List<DTO.Support.ModelFrameMaterialOption> result = new List<DTO.Support.ModelFrameMaterialOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelFrameMaterialOption(context.SupportMng_ModelNoneFrameMaterial_View.ToList()));
                }
            }
            catch { }

            return result;
        }
        public IEnumerable<DTO.Support.ModelFrameMaterialColorOption> GetModelNoneFrameMaterialColorOption()
        {
            List<DTO.Support.ModelFrameMaterialColorOption> result = new List<DTO.Support.ModelFrameMaterialColorOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelFrameMaterialColorOption(context.SupportMng_ModelNoneFrameMaterialColor_View.ToList()));
                }
            }
            catch { }

            return result;
        }
        public IEnumerable<DTO.Support.ModelSubMaterialOption> GetModelNoneSubMaterialOption()
        {
            List<DTO.Support.ModelSubMaterialOption> result = new List<DTO.Support.ModelSubMaterialOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelSubMaterialOption(context.SupportMng_ModelNoneSubMaterial_View.ToList()));
                }
            }
            catch { }

            return result;
        }
        public IEnumerable<DTO.Support.ModelSubMaterialColorOption> GetModelNoneSubMaterialColorOption()
        {
            List<DTO.Support.ModelSubMaterialColorOption> result = new List<DTO.Support.ModelSubMaterialColorOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelSubMaterialColorOption(context.SupportMng_ModelNoneSubMaterialColor_View.ToList()));
                }
            }
            catch { }

            return result;
        }
        public IEnumerable<DTO.Support.ModelCushionOption> GetModelNoneCushionOption()
        {
            List<DTO.Support.ModelCushionOption> result = new List<DTO.Support.ModelCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelCushionOption(context.SupportMng_ModelNoneCushion_View.ToList()));
                }
            }
            catch { }

            return result;
        }
        public IEnumerable<DTO.Support.ModelCushionColorOption> GetModelNoneCushionColorOption()
        {
            List<DTO.Support.ModelCushionColorOption> result = new List<DTO.Support.ModelCushionColorOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelCushionColorOption(context.SupportMng_ModelNoneCushionColor_View.ToList()));
                }
            }
            catch { }

            return result;
        }
        // END GET NONE MATERIAL




        public IEnumerable<DTO.Support.WareHouseArea> GetWareHouseArea(int wareHouseID)
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_WareHouseArea(context.List_WareHouseArea_View.Where(o => o.WareHouseID == wareHouseID).OrderBy(o =>o.AreaCode).ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.WareHouseArea>();
            }
        }

        public IEnumerable<DTO.Support.WareHouseArea> GetAllWarehouseArea()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_WareHouseArea(context.List_WareHouseArea_View.OrderBy(o => o.AreaCode).ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.WareHouseArea>();
            }
        }

        public IEnumerable<DTO.Support.ConfirmedProduct> QuickSearchConfirmedProduct(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.ConfirmedProduct> data = new List<DTO.Support.ConfirmedProduct>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string articleCode = null;
                    string description = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        articleCode = filters["searchQuery"].ToString().Replace("'", "''");
                        description = articleCode;
                    }

                    totalRows = context.SupportMng_function_QuickSearchConfirmedProduct(articleCode, description, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchConfirmedProduct(articleCode, description, orderBy, orderDirection);
                    data = converter.DB2DTO_ConfirmedProduct(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return data;
            }

            return data;
        }

        public IEnumerable<DTO.Support.Sparepart> QuickSearchSparepart(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.Sparepart> data = new List<DTO.Support.Sparepart>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string articleCode = null;
                    string description = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        articleCode = filters["searchQuery"].ToString().Replace("'", "''");
                        description = articleCode;
                    }

                    totalRows = context.SupportMng_function_QuickSearchSparepart(articleCode, description, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchSparepart(articleCode, description, orderBy, orderDirection);
                    data = converter.DB2DTO_Sparepart(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return data;
            }

            return data;
        }

        public IEnumerable<DTO.Support.FrameMaterialOption> QuickSearchFrameMaterialOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.FrameMaterialOption> data = new List<DTO.Support.FrameMaterialOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string frameMaterialUD = null;
                    string frameMaterialNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        frameMaterialUD = filters["searchQuery"].ToString().Replace("'", "''");
                        frameMaterialNM = frameMaterialUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchFrameMaterialOption(frameMaterialUD, frameMaterialNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchFrameMaterialOption(frameMaterialUD, frameMaterialNM, orderBy, orderDirection);
                    data = converter.DB2DTO_FrameMaterialSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.MaterialOption> QuickSearchMaterialOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.MaterialOption> data = new List<DTO.Support.MaterialOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string materialOptionUD = null;
                    string materialOptionNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        materialOptionUD = filters["searchQuery"].ToString().Replace("'", "''");
                        materialOptionNM = materialOptionUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchMaterialOption(materialOptionUD, materialOptionNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchMaterialOption(materialOptionUD, materialOptionNM, orderBy, orderDirection);
                    data = converter.DB2DTO_MaterialOptionSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.SubMaterialOption> QuickSearchSubMaterialOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.SubMaterialOption> data = new List<DTO.Support.SubMaterialOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string subMaterialOptionUD = null;
                    string subMaterialOptionNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        subMaterialOptionUD = filters["searchQuery"].ToString().Replace("'", "''");
                        subMaterialOptionNM = subMaterialOptionUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchSubMaterialOption(subMaterialOptionUD, subMaterialOptionNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchSubMaterialOption(subMaterialOptionUD, subMaterialOptionNM, orderBy, orderDirection);
                    data = converter.DB2DTO_SubMaterialOptionSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.CushionOption> QuickSearchCushionOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.CushionOption> data = new List<DTO.Support.CushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string cushionUD = null;
                    string cushionNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        cushionUD = filters["searchQuery"].ToString().Replace("'", "''");
                        cushionNM = cushionUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchCushionOption(cushionUD, cushionNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchCushionOption(cushionUD, cushionNM, orderBy, orderDirection);
                    data = converter.DB2DTO_CushionOptionSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.CushionColorOption> QuickSearchCushionColorOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.CushionColorOption> data = new List<DTO.Support.CushionColorOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string cushionColorUD = null;
                    string cushionColorNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        cushionColorUD = filters["searchQuery"].ToString().Replace("'", "''");
                        cushionColorNM = cushionColorUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchCushionColorOption(cushionColorUD, cushionColorNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchCushionColorOption(cushionColorUD, cushionColorNM, orderBy, orderDirection);
                    data = converter.DB2DTO_CushionColorOptionSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.Model> QuickSearchPieceModel(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.Model> data = new List<DTO.Support.Model>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string pieceModelUD = null;
                    string pieceModelNM = null;
                    int modelID = 0;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        pieceModelUD = filters["searchQuery"].ToString().Replace("'", "''");
                        pieceModelNM = pieceModelUD;
                    }

                    if (filters.ContainsKey("modelID") && !string.IsNullOrEmpty(filters["modelID"].ToString()))
                    {
                        modelID = Convert.ToInt32(filters["modelID"].ToString());
                    }

                    totalRows = context.SupportMng_function_QuickSearchPieceModel(pieceModelUD, pieceModelNM, modelID, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchPieceModel(pieceModelUD, pieceModelNM, modelID, orderBy, orderDirection);
                    data = converter.DB2DTO_PieceModelSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.ClientRating> GetClientRating()
        {
            List<DTO.Support.ClientRating> result = new List<DTO.Support.ClientRating>();
            result.Add(new DTO.Support.ClientRating() { RatingValue = "A", RatingText = "A" });
            result.Add(new DTO.Support.ClientRating() { RatingValue = "B", RatingText = "B" });
            result.Add(new DTO.Support.ClientRating() { RatingValue = "C", RatingText = "C" });
            return result;
        }

        public IEnumerable<DTO.Support.Client> QuickSearchClient(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            var data = new List<DTO.Support.Client>();

            try
            {
                using (var context = CreateContext())
                {
                    string clientUd;
                    TryParseFilter(filters, "searchQuery", out clientUd);
                    
                    totalRows = context.SupportMng_function_QuickSearchClient(clientUd, clientUd, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchClient(clientUd, clientUd, orderBy, orderDirection);
                    data = converter.DB2DTO_Client(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());

                    List<int?> clientIDs = data.Select(s => s.ClientID).ToList();
                    var estimatedAdditionalCost = context.SupportMng_ClientEstimatedAdditionalCost_View.Where(o => clientIDs.Contains(o.ClientID)).ToList();
                    foreach (var item in data)
                    {
                        var estCostItem = estimatedAdditionalCost.Where(o => o.ClientID == item.ClientID).ToList();
                        item.ClientEstimatedAdditionalCostDTOs = AutoMapper.Mapper.Map<List<SupportMng_ClientEstimatedAdditionalCost_View>,List<DTO.Support.ClientEstimatedAdditionalCostDTO>>(estCostItem);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.Booking> QuickSearchBooking(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            var data = new List<DTO.Support.Booking>();

            //try to get data
            try
            {
                using (var context = CreateContext())
                {
                    string bookingUd = null;
                    string blNo = null;
                    int? forwarderID = null; 


                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        bookingUd = filters["searchQuery"].ToString().Replace("'", "''");
                        blNo = bookingUd;
                    }

                    if (filters.ContainsKey("forwarderID") && filters["forwarderID"] != null)
                    {
                        forwarderID = Convert.ToInt32(filters["forwarderID"]);
                    }

                    totalRows = context.SupportMng_function_QuickSearchBooking(bookingUd, blNo, forwarderID, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchBooking(bookingUd, blNo, forwarderID, orderBy, orderDirection);
                    data = converter.DB2DTO_Booking(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.ModelSparepart> GetModelSparepart(int modelID)
        {
            List<DTO.Support.ModelSparepart> result = new List<DTO.Support.ModelSparepart>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    result.AddRange(converter.DB2DTO_ModelSprepart(context.SupportMng_ModelSparepart_View.Where(o => o.ModelID == modelID).ToList()));
                }
            }
            catch { }

            return result;
        }

        public IEnumerable<DTO.Support.ModelSeason> QuickSearchModelSeason(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.ModelSeason> data = new List<DTO.Support.ModelSeason>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string modelUD = null;
                    string modelNM = null;
                    string rangeName = null;
                    int modelStatusID = 0;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        modelUD = filters["searchQuery"].ToString().Replace("'", "''");
                        modelNM = modelUD;
                        rangeName = modelUD;
                    }

                    if (filters.ContainsKey("modelStatusID"))
                    {
                        modelStatusID = Convert.ToInt32(filters["modelStatusID"]);
                    }

                    totalRows = context.SupportMng_function_QuickSearchModelSeason(modelUD, modelNM, rangeName, modelStatusID, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchModelSeason(modelUD, modelNM, rangeName, modelStatusID, orderBy, orderDirection);
                    data = converter.DB2DTO_ModelSeason(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.ModelSeason> QuickSearchArticleCode(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.ModelSeason> data = new List<DTO.Support.ModelSeason>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string modelUD = string.Empty;
                    string modelNM = string.Empty;
                    string rangeName = string.Empty;
                    int modelStatusID = 0;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        modelUD = filters["searchQuery"].ToString().Replace("'", "''");
                        modelNM = modelUD;
                        rangeName = modelUD;
                    }

                    if (filters.ContainsKey("modelStatusID"))
                    {
                        modelStatusID = Convert.ToInt32(filters["modelStatusID"]);
                    }

                    totalRows = context.OffertMng_function_SearchArticleWithModel(modelUD, modelNM, rangeName, modelStatusID, orderBy, orderDirection).Count();
                    var result = context.OffertMng_function_SearchArticleWithModel(modelUD, modelNM, rangeName, modelStatusID, orderBy, orderDirection);
                    data = converter.DB2DTO_ArticleCode(result.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }


        public IEnumerable<DTO.Support.ClientCity> GetClientCity()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ClientCity(context.SupportMng_ClientCity_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ClientCity>();
            }
        }

        public IEnumerable<DTO.Support.ClientCountry> GetClientCountry()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ClientCountry(context.SupportMng_ClientCountry_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ClientCountry>();
            }
        }

        public IEnumerable<DTO.Support.ClientGroup> GetClientGroup()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ClientGroup(context.SupportMng_ClientGroup_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ClientGroup>();
            }
        }

        public IEnumerable<DTO.Support.Language> GetClientLanguage()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ClientLanguage(context.SupportMng_ClientLanguage_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Language>();
            }
        }

        public IEnumerable<DTO.Support.ClientRelationshipType> GetClientRelationshipType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_RelationshipType(context.SupportMng_ClientRelationshipType_View.OrderBy(o=>o.DisplayOrder).ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ClientRelationshipType>();
            }
        }

        public IEnumerable<DTO.Support.ClientType> GetClientType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ClientType(context.SupportMng_ClientType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ClientType>();
            }
        }

        public DTO.Controls.ProductWizardData GetProductWizardData(int modelID, int? clientID, string season, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.Controls.ProductWizardData data = new DTO.Controls.ProductWizardData();
            data.MaterialOptions = new List<DTO.Support.ModelMaterialOption>();
            data.MaterialTypeOptions = new List<DTO.Support.ModelMaterialTypeOption>();
            data.MaterialColorOptions = new List<DTO.Support.ModelMaterialColorOption>();
            data.FrameMaterialOptions = new List<DTO.Support.ModelFrameMaterialOption>();
            data.FrameMaterialColorOptions = new List<DTO.Support.ModelFrameMaterialColorOption>();
            data.SubMaterialOptions = new List<DTO.Support.ModelSubMaterialOption>();
            data.SubMaterialColorOptions = new List<DTO.Support.ModelSubMaterialColorOption>();
            data.CushionOptions = new List<DTO.Support.ModelCushionOption>();
            data.CushionTypeOptions = new List<DTO.Support.ModelCushionTypeOption>();
            data.CushionColorOptions = new List<DTO.Support.ModelCushionColorOption>();
            data.MaterialOptions = new List<DTO.Support.ModelMaterialOption>();
            data.PackagingMethods = new List<DTO.Support.ModelPackagingMethodOption>();
            data.BackCushionOptions = new List<DTO.Support.ModelBackCushionOption>();
            data.SeatCushionOptions = new List<DTO.Support.ModelSeatCushionOption>();
            data.FSCPercents = new List<DTO.Support.FSCPercent>();
            data.FSCTypes = new List<DTO.Support.FSCType>();

            //pricing option section
            data.ModelFixPriceConfigurations = new List<DTO.Support.ModelFixPriceConfiguration>();
            data.ModelPriceConfigurationDetails = new List<DTO.Support.ModelPriceConfigurationDetail>();

            //breakdown section
            data.BreakdownCategories = new List<DTO.Support.BreakdownCategory>();
            data.BreakdownCategoryOptions = new List<DTO.Support.BreakdownCategoryOption>();
            data.BreakdownManagementFees = new List<DTO.Support.BreakdownManagementFee>();
            data.ClientSpecialPackagingMethods = new List<DTO.Support.ClientSpecialPackagingMethod>();

            //purchasing config price
            data.ConfiguredPurchasingPriceModelConfirmedFixedPrices = new List<DTO.Support.ConfiguredPurchasingPriceModelConfirmedFixedPrice>();
            data.ConfiguredPurchasingPriceModelConfirmedPriceConfigurations = new List<DTO.Support.ConfiguredPurchasingPriceModelConfirmedPriceConfiguration>();

            try
            {
                using (SupportEntities context = CreateContext())
                {
                    SupportMng_Model_View dbModel = context.SupportMng_Model_View.FirstOrDefault(o => o.ModelID == modelID);
                    if (dbModel != null)
                    {
                        data.ModelID = dbModel.ModelID;
                        data.ModelUD = dbModel.ModelUD;
                        data.ModelNM = dbModel.ModelNM;
                        data.ManufacturerCountryUD = dbModel.ManufacturerCountryUD;
                        data.ManufacturerCountryID = dbModel.ManufacturerCountryID.Value;
                        if (!string.IsNullOrEmpty(dbModel.ImageFile_DisplayUrl))
                        {
                            data.ImageFile_DisplayUrl = FrameworkSetting.Setting.MediaThumbnailUrl + dbModel.ImageFile_DisplayUrl;
                        }
                        else
                        {
                            data.ImageFile_DisplayUrl = FrameworkSetting.Setting.MediaThumbnailUrl + "no-image.jpg";
                        }

                        if (!string.IsNullOrEmpty(dbModel.ImageFile_FullSizeUrl))
                        {
                            data.ImageFile_FullSizeUrl = FrameworkSetting.Setting.MediaFullSizeUrl + dbModel.ImageFile_FullSizeUrl;
                        }
                        else
                        {
                            data.ImageFile_FullSizeUrl = FrameworkSetting.Setting.MediaFullSizeUrl + "no-image.jpg";
                        }

                        data.HasCushion = dbModel.HasCushion;
                        data.HasFrameMaterial = dbModel.HasFrameMaterial;
                        data.HasSubMaterial = dbModel.HasSubMaterial;

                        if (dbModel.ProductGroupID.HasValue)
                        {
                            data.MaterialOptions = this.GetModelMaterialOption(dbModel.ProductGroupID.Value).ToList();
                            data.MaterialTypeOptions = this.GetModelMaterialTypeOption(dbModel.ProductGroupID.Value).ToList();
                            data.MaterialColorOptions = this.GetModelMaterialColorOption(dbModel.ProductGroupID.Value).ToList();
                        }

                        data.FSCTypes = this.GetFSCType().ToList();
                        data.FSCPercents = this.GetFSCPercent().ToList();

                        //pricing option
                        data.ModelFixPriceConfigurations = this.GetModelFixPriceConfiguration(modelID, season);
                        data.ModelPriceConfigurationDetails = this.GetModelPriceConfigurationDetail(modelID, season);

                        // add TBA row in packaging method
                        data.PackagingMethods.Add(new DTO.Support.ModelPackagingMethodOption() { PackagingMethodID = 17, PackagingMethodNM = "TBA" });
                        data.PackagingMethods.AddRange(this.GetModelPackagingMethodOption(dbModel.ModelID).ToList());

                        if (dbModel.HasFrameMaterial)
                        {
                            data.FrameMaterialOptions = this.GetModelFrameMaterialOption(dbModel.ProductGroupID.Value).ToList();
                            data.FrameMaterialColorOptions = this.GetModelFrameMaterialColorOption(dbModel.ProductGroupID.Value).ToList();
                        }
                        else
                        {
                            data.FrameMaterialOptions = this.GetModelNoneFrameMaterialOption().ToList();
                            data.FrameMaterialColorOptions = this.GetModelNoneFrameMaterialColorOption().ToList();
                        }

                        if (dbModel.HasSubMaterial)
                        {
                            data.SubMaterialOptions = this.GetModelSubMaterialOption(modelID).ToList();
                            data.SubMaterialColorOptions = this.GetModelSubMaterialColorOption(modelID).ToList();
                        }
                        else
                        {
                            data.SubMaterialOptions = this.GetModelNoneSubMaterialOption().ToList();
                            data.SubMaterialColorOptions = this.GetModelNoneSubMaterialColorOption().ToList();
                        }

                        if (dbModel.HasCushion)
                        {
                            data.CushionOptions = this.GetModelCushionOption(modelID).ToList();
                            data.CushionTypeOptions = this.GetModelCushionTypeOption(dbModel.ProductGroupID.Value).ToList();
                            data.CushionColorOptions = this.GetModelCushionColorOption(dbModel.ProductGroupID.Value).ToList();
                            data.BackCushionOptions = this.GetModelBackCushionOption(dbModel.ProductGroupID.Value).ToList();
                            data.SeatCushionOptions = this.GetModelSeatCushionOption(dbModel.ProductGroupID.Value).ToList();
                        }
                        else
                        {
                            data.CushionOptions = this.GetModelNoneCushionOption().ToList();
                            data.CushionColorOptions = this.GetModelNoneCushionColorOption().ToList();
                            data.BackCushionOptions = this.GetModelNoneBackCushionOption().ToList();
                            data.SeatCushionOptions = this.GetModelNoneSeatCushionOption().ToList();
                        }

                        //
                        //breakdown
                        //
                        var dbBreakdown = context.SupportMng_Breakdown_View.Where(o => o.ModelID == modelID).FirstOrDefault();
                        var dbBreakdownCateogry = context.SupportMng_BreakdownCategory_View.OrderBy(o => o.DisplayOrder).ToList();
                        var dbBreakdownCategoryOptions = context.SupportMng_BreakdownCategoryOption_View.Where(o => o.ModelID == modelID).OrderBy(o => o.CompanyID).ThenBy(o => o.BreakdownCategoryID).ThenByDescending(o => o.IsDefaultOption).ToList();
                        var dbBreakdownManagementFee = context.SupportMng_BreakdownManagementFee_View.Where(o => o.ModelID == modelID).ToList();
                        var dbClientSpecialPackagingMethod = context.SupportMng_ClientSpecialPackagingMethod_View.Where(o => o.ClientID == clientID).ToList();
                        if (dbBreakdown != null)
                        {
                            data.SeasonSpecPercent = dbBreakdown.SeasonSpecPercent;
                        }
                        else {
                            data.SeasonSpecPercent = 0;
                        }
                        data.BreakdownCategories = AutoMapper.Mapper.Map<List<SupportMng_BreakdownCategory_View>, List<DTO.Support.BreakdownCategory>>(dbBreakdownCateogry);
                        data.BreakdownCategoryOptions = AutoMapper.Mapper.Map<List<SupportMng_BreakdownCategoryOption_View>, List<DTO.Support.BreakdownCategoryOption>>(dbBreakdownCategoryOptions);
                        data.BreakdownManagementFees = AutoMapper.Mapper.Map<List<SupportMng_BreakdownManagementFee_View>, List<DTO.Support.BreakdownManagementFee>>(dbBreakdownManagementFee);
                        data.VND_USD_ExchangeRate = context.SupportMng_function_GetExchangeRate(DateTime.Now, "USD").FirstOrDefault().Value;
                        data.ClientSpecialPackagingMethods = AutoMapper.Mapper.Map<List<SupportMng_ClientSpecialPackagingMethod_View>,List<DTO.Support.ClientSpecialPackagingMethod>>(dbClientSpecialPackagingMethod);
                        //add packaging method is special (PackagingMethodID = 11) when we have specify the packaging method special for client
                        if (data.ClientSpecialPackagingMethods.Count() > 0)
                        {
                            data.PackagingMethods.Add(new DTO.Support.ModelPackagingMethodOption() { PackagingMethodID = 11, PackagingMethodNM = "REQUEST" });
                        }

                        //purchasing price config
                        var defaultFactory = context.SupportMng_ModelDefaultFactory_View.Where(o => o.ModelID == modelID).FirstOrDefault();
                        int? defaultFactoryID = defaultFactory == null ? null : defaultFactory.FactoryID;

                        var purchaseFixPrice = context.Shared_ConfiguredPurchasingPriceModelConfirmedFixedPrice_View.Where(o => o.ModelID == modelID && o.Season == season && o.FactoryID == defaultFactoryID).ToList();
                        data.ConfiguredPurchasingPriceModelConfirmedFixedPrices = AutoMapper.Mapper.Map<List<Shared_ConfiguredPurchasingPriceModelConfirmedFixedPrice_View>, List<DTO.Support.ConfiguredPurchasingPriceModelConfirmedFixedPrice>>(purchaseFixPrice);

                        var purchaseConfigPrice = context.Shared_ConfiguredPurchasingPriceModelConfirmedPriceConfiguration_View.Where(o => o.ModelID == modelID && o.Season == season && o.FactoryID == defaultFactoryID).ToList();
                        data.ConfiguredPurchasingPriceModelConfirmedPriceConfigurations = AutoMapper.Mapper.Map<List<Shared_ConfiguredPurchasingPriceModelConfirmedPriceConfiguration_View>, List<DTO.Support.ConfiguredPurchasingPriceModelConfirmedPriceConfiguration>>(purchaseConfigPrice);
                    }
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.ModelStatus> GetModelStatus()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_ModelStatus_View>, List<DTO.Support.ModelStatus>>(context.SupportMng_ModelStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ModelStatus>();
            }
        }

        public List<DTO.Support.Office> GetOffice()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_Office_View>, List<DTO.Support.Office>>(context.SupportMng_Office_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Office>();
            }
        }

        public List<DTO.Support.UserGroup> GetUserGroup()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_UserGroup_View>, List<DTO.Support.UserGroup>>(context.SupportMng_UserGroup_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.UserGroup>();
            }
        }

        public List<DTO.Support.Module> GetModule()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_Module_View>, List<DTO.Support.Module>>(context.SupportMng_Module_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Module>();
            }
        }

        public IEnumerable<DTO.Support.SeatCushionOption> QuickSearchSeatCushionOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.SeatCushionOption> data = new List<DTO.Support.SeatCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string seatCushionUD = null;
                    string seatCushionNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        seatCushionUD = filters["searchQuery"].ToString().Replace("'", "''");
                        seatCushionNM = seatCushionUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchSeatCushionOption(seatCushionUD, seatCushionNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchSeatCushionOption(seatCushionUD, seatCushionNM, orderBy, orderDirection);
                    data = AutoMapper.Mapper.Map<List<SupportMng_SeatCushionOptionSearchResult_View>, List<DTO.Support.SeatCushionOption>>(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public IEnumerable<DTO.Support.BackCushionOption> QuickSearchBackCushionOption(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.BackCushionOption> data = new List<DTO.Support.BackCushionOption>();

            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string backCushionUD = null;
                    string backCushionNM = null;

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        backCushionUD = filters["searchQuery"].ToString().Replace("'", "''");
                        backCushionNM = backCushionUD;
                    }

                    totalRows = context.SupportMng_function_QuickSearchBackCushionOption(backCushionUD, backCushionNM, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchBackCushionOption(backCushionUD, backCushionNM, orderBy, orderDirection);
                    data = AutoMapper.Mapper.Map<List<SupportMng_BackCushionOptionSearchResult_View>, List<DTO.Support.BackCushionOption>>(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public List<DTO.Support.QuotationStatus> GetQuotationStatus()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_QuotationStatus_View>, List<DTO.Support.QuotationStatus>>(context.SupportMng_QuotationStatus_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.QuotationStatus>();
            }
        }

        public List<DTO.Support.OfferDirection> GetOfferDirection()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_QuotationOfferDirection_View>, List<DTO.Support.OfferDirection>>(context.SupportMng_QuotationOfferDirection_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.OfferDirection>();
            }
        }

        public List<DTO.Support.FSCType> GetFSCType()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_FSCType_View>, List<DTO.Support.FSCType>>(context.SupportMng_FSCType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.FSCType>();
            }
        }

        public List<DTO.Support.FSCPercent> GetFSCPercent()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_FSCPercent_View>, List<DTO.Support.FSCPercent>>(context.SupportMng_FSCPercent_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.FSCPercent>();
            }
        }

        public List<DTO.Support.ModelFixPriceConfiguration> GetModelFixPriceConfiguration(int modelID, string season)
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ModelFixPriceConfiguration(context.SupportMng_ModelFixPriceConfiguration_View.Where(o =>o.ModelID == modelID && o.Season==season).ToList());
                }
            }
            catch
            {
               return new List<DTO.Support.ModelFixPriceConfiguration>();
            }
        }

        public List<DTO.Support.ModelPriceConfigurationDetail> GetModelPriceConfigurationDetail(int modelID, string season)
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ModelPriceConfigurationDetail(context.SupportMng_ModelPriceConfigurationDetail_View.Where(o =>o.ModelID == modelID && o.Season == season).ToList());
                }
            }
            catch
            {                
                return new List<DTO.Support.ModelPriceConfigurationDetail>();
            }
        }

        public List<DTO.Support.ProductElement> GetProductElement()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ProductElement(context.SupportMng_ProductElement_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ProductElement>();
            }
        }

        public List<DTO.Support.Factory> GetAuthorizedFactories(int userId)
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<List_Factory>, List<DTO.Support.Factory>>(context.SupportMng_function_GetAuthorizedFactory(userId).ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Factory>();
            }
        }

        public List<DTO.Support.PLCImageType> GetPLCImageTypes()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_PLCImageType_View>, List<DTO.Support.PLCImageType>>(context.SupportMng_PLCImageType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PLCImageType>();
            }
        }

        public List<DTO.Support.PriceDifference> GetPriceDifference(string season)
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_PriceDifference_View>, List<DTO.Support.PriceDifference>>(context.SupportMng_PriceDifference_View.Where(o => o.Season == season).ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PriceDifference>();
            }
        }

        public List<DTO.Support.ConsigneeType> GetConsigneeTypes()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_ConsigneeType_View>, List<DTO.Support.ConsigneeType>>(context.SupportMng_ConsigneeType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ConsigneeType>();
            }
        }

        public List<DTO.Support.NotifyType> GetNotifyTypes()
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return AutoMapper.Mapper.Map<List<SupportMng_NotifyType_View>, List<DTO.Support.NotifyType>>(context.SupportMng_NotifyType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.NotifyType>();
            }
        }

        public string GetSettingValue(string season, string settingKey)
        {
            //try to get data
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    if (!string.IsNullOrEmpty(season))
                    {
                        return context.SupportMng_SystemSetting_View.FirstOrDefault(o => o.Season == season && o.SettingKey == settingKey).SettingValue;
                    }
                    else
                    {
                        return context.SupportMng_SystemSetting_View.FirstOrDefault(o => o.SettingKey == settingKey).SettingValue;
                    }
                    
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public List<DTO.Support.VATPercent> GetVATPercent()
        {
            List<DTO.Support.VATPercent> VATPercents = new List<DTO.Support.VATPercent>();

            VATPercents.Add(new DTO.Support.VATPercent { VATPercentValue = 0, VATPercentText = "0%" });
            VATPercents.Add(new DTO.Support.VATPercent { VATPercentValue = 21, VATPercentText = "21%" });
            return VATPercents;
        }

        public List<DTO.Support.OrderType> GetOrderType()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_OrderType(context.SupportMng_OrderType_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.OrderType>();
            }
        }

        public List<DTO.Support.ItemStandard> GetItemStandard()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ItemStandard(context.SupportMng_ItemStandard_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ItemStandard>();
            }
        }

        public List<DTO.Support.TestSamplingOption> GetTestSamplingOption()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_TestSamplingOption(context.SupportMng_TestSamplingOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.TestSamplingOption>();
            }
        }

        public List<DTO.Support.LabelingOption> GetLabelingOption()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_LabelingOption(context.SupportMng_LabelingOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.LabelingOption>();
            }
        }

        public List<DTO.Support.PackagingOption> GetPackagingOption()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_PackagingOption(context.SupportMng_PackagingOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.PackagingOption>();
            }
        }

        public List<DTO.Support.InspectionOption> GetInspectionOption()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_InspectionOption(context.SupportMng_InspectionOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.InspectionOption>();
            }
        }

        public List<DTO.Support.ShipmentToOption> GetShipmentToOption()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ShipmentToOption(context.SupportMng_ShipmentToOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ShipmentToOption>();
            }
        }

        public List<DTO.Support.ShipmentTypeOption> GetShipmentTypeOption()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ShipmentTypeOption(context.SupportMng_ShipmentTypeOption_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ShipmentTypeOption>();
            }
        }

        public List<DTO.Support.SupplyChainPerson> GetSupplyChainPerson()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_SupplyChainPerson(context.SupportMng_SupplyChainPerson_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.SupplyChainPerson>();
            }
        }

        public List<DTO.Support.SaleOrderType> GetSaleOrderType()
        {
            List<DTO.Support.SaleOrderType> SaleOrderTypes = new List<DTO.Support.SaleOrderType>();

            SaleOrderTypes.Add(new DTO.Support.SaleOrderType { OrderTypeValue = "FACTORY", OrderTypeText = "FACTORY" });
            SaleOrderTypes.Add(new DTO.Support.SaleOrderType { OrderTypeValue = "FOB_WAREHOUSE", OrderTypeText = "FOB-WAREHOUSE" });
            SaleOrderTypes.Add(new DTO.Support.SaleOrderType { OrderTypeValue = "WAREHOUSE", OrderTypeText = "WAREHOUSE" });

            return SaleOrderTypes;
        }

        public List<DTO.Support.Showroom> GetShowroom()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_Showroom(context.SupportMng_Showroom_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.Showroom>();
            }
        }

        public List<DTO.Support.ShowroomReceiptType> GetShowroomReceiptType()
        {
            List<DTO.Support.ShowroomReceiptType> ShowroomReceiptTypes = new List<DTO.Support.ShowroomReceiptType>();
            ShowroomReceiptTypes.Add(new DTO.Support.ShowroomReceiptType { ReceiptTypeID = 1, ReceiptTypeName = "SHOWROOM IMPORT RECEIPT" });
            ShowroomReceiptTypes.Add(new DTO.Support.ShowroomReceiptType { ReceiptTypeID = 2, ReceiptTypeName = "SHOWROOM EXPORT RECEIPT" });
            return ShowroomReceiptTypes;
        }

        public IEnumerable<DTO.Support.ShowroomItem> QuickSearchShowroomItem(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.ShowroomItem> data = new List<DTO.Support.ShowroomItem>();
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string searchQuery = string.Empty;
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }
                    totalRows = context.SupportMng_function_QuickSearchShowroomItem(searchQuery, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchShowroomItem(searchQuery, orderBy, orderDirection);
                    data = converter.DB2DTO_ShowroomItem(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
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
            }
            return data;
        }

        public List<DTO.Support.FreeToSaleCategory> GetFreeToSaleCategory()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_FreeToSaleCategory(context.List_FreeToSaleCategory_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.FreeToSaleCategory>();
            }
        }

        public IEnumerable<DTO.Support.WarehouseItem> QuickSearchWarehouseItem(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            List<DTO.Support.WarehouseItem> data = new List<DTO.Support.WarehouseItem>();
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    string searchQuery = string.Empty;
                    string productType = string.Empty;
                    bool? isWexItem = null;

                    if (filters.ContainsKey("productType") && !string.IsNullOrEmpty(filters["productType"].ToString()))
                    {
                        productType = filters["productType"].ToString().Replace("'", "''");
                    }

                    if (filters.ContainsKey("isWexItem") && filters["isWexItem"]!=null)
                    {
                        isWexItem = Convert.ToBoolean(filters["isWexItem"]);
                    }

                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                    }
                    totalRows = context.SupportMng_function_QuickSearchWarehouseItem(productType, isWexItem, searchQuery, orderBy, orderDirection).Count();
                    var result = context.SupportMng_function_QuickSearchWarehouseItem(productType, isWexItem, searchQuery, orderBy, orderDirection);
                    data = converter.DB2DTO_WarehouseItem(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
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
            }
            return data;
        }

        public List<DTO.Support.ShowroomArea> QuickSearchShowroomArea(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Get showroom area success" };
            try
            {
                string searchQuery = string.Empty;
                if (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                {
                    searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                }
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ShowroomArea(context.SupportMng_ShowroomArea_View.Where(o => o.ShowroomAreaUD.Contains(searchQuery) || o.ShowroomAreaNM.Contains(searchQuery)).ToList());
                }
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
                return new List<DTO.Support.ShowroomArea>();
            }
        }

        public List<DTO.Support.ShowroomAreaByPhysicalQnt> QuickSearchShowroomAreaByPhysicalQnt(Hashtable filters, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success, Message = "Get showroom area success" };
            try
            {
                string searchQuery = string.Empty;
                if (filters.ContainsKey("searchQuery") && filters["searchQuery"] != null && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                {
                    searchQuery = filters["searchQuery"].ToString().Replace("'", "''");
                }
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ShowroomAreaByPhysicalQnt(context.SupportMng_ShowroomAreaByPhysicalQnt_View.Where(o => o.ShowroomAreaUD.Contains(searchQuery)).ToList());
                }
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
                return new List<DTO.Support.ShowroomAreaByPhysicalQnt>();
            }
        }

        public IEnumerable<DTO.Support.Forwarder> QuickSearchForwarder(Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            totalRows = 0;
            var data = new List<DTO.Support.Forwarder>();

            try
            {
                using (var context = CreateContext())
                {
                    var forwarderNm = string.Empty;
                  
                    if (filters.ContainsKey("searchQuery") && !string.IsNullOrEmpty(filters["searchQuery"].ToString()))
                    {
                        forwarderNm = filters["searchQuery"].ToString().Replace("'", "''");
                       
                    }

                    totalRows = context.SupportMng_function_QuickSearchForwarder(orderBy, orderDirection, forwarderNm).Count();
                    var result = context.SupportMng_function_QuickSearchForwarder(orderBy, orderDirection, forwarderNm);
                    data = converter.DB2DTO_Forwarder(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        private static bool TryParseFilter<T>(IDictionary filters, string key, out T outValue)
        {
            try
            {
                outValue = (T)Convert.ChangeType(filters[key].ToString().Replace("'", "''"), typeof(T));

                return true;
            }
            catch
            {
                outValue = default(T);
                return false;
            }
        }

        //Report template
        public List<DTO.Support.ReportTemplate> GetReportTemplate()
        {
            try
            {
                using (SupportEntities context = CreateContext())
                {
                    return converter.DB2DTO_ReportTemplate(context.SupportMng_ReportTemplate_View.ToList());
                }
            }
            catch
            {
                return new List<DTO.Support.ReportTemplate>();
            }
        }
    }
}
