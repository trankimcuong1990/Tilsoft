using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALBase;
using AutoMapper;

namespace DAL.ProductMng
{
    internal class DataConverter
    {
        public DataConverter()
        {
            string mapName = "ProductMng";
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                AutoMapper.Mapper.CreateMap<ProductMng_ProductSearchResult_View, DTO.ProductMng.ProductSearchResult>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ImageFile_FullSizeUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.FullSizeUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_FullSizeUrl : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductMng_Product_View, DTO.ProductMng.Product>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ConfirmedDate, o => o.ResolveUsing(s => s.ConfirmedDate.HasValue ? s.ConfirmedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.CreatedDate, o => o.ResolveUsing(s => s.CreatedDate.HasValue ? s.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.MaterialWeightUpdatedDate, o => o.ResolveUsing(s => s.MaterialWeightUpdatedDate.HasValue ? s.MaterialWeightUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.FabricWeightUpdatedDate, o => o.ResolveUsing(s => s.FabricWeightUpdatedDate.HasValue ? s.FabricWeightUpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.ResolveUsing(s => FrameworkSetting.Setting.ThumbnailUrl + (!string.IsNullOrEmpty(s.ImageFile_DisplayUrl) ? s.ImageFile_DisplayUrl : "no-image.jpg")))
                    .ForMember(d => d.ProductPieces, o => o.MapFrom(s => s.ProductMng_ProductPiece_View))
                    .ForMember(d => d.ProductSetEANCodes, o => o.MapFrom(s => s.ProductMng_ProductSetEANCode_View))
                    .ForMember(d => d.ProductECommerceSpecs, o => o.MapFrom(s => s.ProductMng_ProductECommerceSpec_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductMng_Model_View, DTO.ProductMng.Product>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.ImageFile_DisplayUrl, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductMng_ProductPiece_View, DTO.ProductMng.ProductPiece>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                AutoMapper.Mapper.CreateMap<DTO.ProductMng.ProductPiece, ProductPiece>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ProductPieceID, o => o.Ignore());


                AutoMapper.Mapper.CreateMap<ProductMng_Package_Constant_View, DTO.ProductMng.Package>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));


                //ean code new feature
                AutoMapper.Mapper.CreateMap<ProductMng_ProductColliPiece_View, DTO.ProductMng.ProductColliPiece>()
                     .IgnoreAllNonExisting()
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductMng_ProductColli_View, DTO.ProductMng.ProductColli>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ProductColliPieces, o => o.MapFrom(s => s.ProductMng_ProductColliPiece_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<ProductMng_ProductSetEANCode_View, DTO.ProductMng.ProductSetEANCode>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ProductCollis, o => o.MapFrom(s => s.ProductMng_ProductColli_View))
                     .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                //product ecommerce spec
                AutoMapper.Mapper.CreateMap<ProductMng_ProductECommerceSpec_View, DTO.ProductMng.ProductECommerceSpec>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.ProductMng.ProductECommerceSpec, ProductECommerceSpec>()
                     .IgnoreAllNonExisting()
                     .ForMember(d => d.ProductECommerceSpecID, o => o.Ignore());


                AutoMapper.Mapper.CreateMap<DTO.ProductMng.Product, Product>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ConfirmedBy, o => o.Ignore())
                    .ForMember(d => d.ConfirmedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedDate, o => o.Ignore())
                    .ForMember(d => d.CreatedBy, o => o.Ignore())
                    .ForMember(d => d.MaterialWeightUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.MaterialWeightUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.FabricWeightUpdatedBy, o => o.Ignore())
                    .ForMember(d => d.FabricWeightUpdatedDate, o => o.Ignore())
                    .ForMember(d => d.ProductID, o => o.Ignore())
                    .ForMember(d => d.ProductPiece, o => o.Ignore())
                    .ForMember(d => d.ProductECommerceSpec, o => o.Ignore())
                    .ForMember(d => d.ConcurrencyFlag, o => o.Ignore());
                AutoMapper.Mapper.CreateMap<ProductMng_ModelPackagingMethodOption2_View, DTO.ProductMng.ModelPackagingMethodOption2DTO>()
                  .IgnoreAllNonExisting()
                  .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.ProductMng.ProductSearchResult> DB2DTO_ProductSearchResultList(List<ProductMng_ProductSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductMng_ProductSearchResult_View>, List<DTO.ProductMng.ProductSearchResult>>(dbItems);
        }

        public List<DTO.ProductMng.ModelPackagingMethodOption2DTO> DB2DTO_ModelPackagingMethodOption2(List<ProductMng_ModelPackagingMethodOption2_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductMng_ModelPackagingMethodOption2_View>, List<DTO.ProductMng.ModelPackagingMethodOption2DTO>>(dbItems);
        }

        public DTO.ProductMng.Product DB2DTO_Product(ProductMng_Product_View dbItem)
        {            
            return AutoMapper.Mapper.Map<ProductMng_Product_View, DTO.ProductMng.Product>(dbItem);
        }

        public void DB2DTO_Model2Product(ProductMng_Model_View dbItem, ref DTO.ProductMng.Product dtoItem)
        {
            AutoMapper.Mapper.Map<ProductMng_Model_View, DTO.ProductMng.Product>(dbItem, dtoItem);
        }

        public List<DTO.ProductMng.Package> DB2DTO_Package(List<ProductMng_Package_Constant_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<ProductMng_Package_Constant_View>, List<DTO.ProductMng.Package>>(dbItems);
        }

        public void DTO2DB(DTO.ProductMng.Product dtoItem, ref Product dbItem)
        {
            //product pieces
            if (dtoItem.ProductPieces != null)
            {
                //check product piece
                foreach (var item in dbItem.ProductPiece.ToArray())
                {
                    if (!dtoItem.ProductPieces.Select(s => s.ProductPieceID).Contains(item.ProductPieceID))
                    {
                        dbItem.ProductPiece.Remove(item);
                    }
                }
                //check product ecommerce spec
                foreach (var item in dbItem.ProductECommerceSpec.ToArray())
                {
                    if (!dtoItem.ProductECommerceSpecs.Select(s => s.ProductECommerceSpecID).Contains(item.ProductECommerceSpecID))
                    {
                        dbItem.ProductECommerceSpec.Remove(item);
                    }
                }
                //map product piece
                foreach (var item in dtoItem.ProductPieces)
                {
                    ProductPiece dbPiece;
                    if (item.ProductPieceID < 0)
                    {
                        dbPiece = new ProductPiece();
                        dbItem.ProductPiece.Add(dbPiece);
                    }
                    else
                    {
                        dbPiece = dbItem.ProductPiece.FirstOrDefault(o => o.ProductPieceID == item.ProductPieceID);
                    }
                    if (dbPiece != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductMng.ProductPiece, ProductPiece>(item, dbPiece);
                    }
                }

                //map product ecommerce spec
                foreach (var item in dtoItem.ProductECommerceSpecs)
                {
                    ProductECommerceSpec dbEcommerceSpec;
                    if (item.ProductECommerceSpecID < 0)
                    {
                        dbEcommerceSpec = new ProductECommerceSpec();
                        dbItem.ProductECommerceSpec.Add(dbEcommerceSpec);
                    }
                    else
                    {
                        dbEcommerceSpec = dbItem.ProductECommerceSpec.FirstOrDefault(o => o.ProductECommerceSpecID == item.ProductECommerceSpecID);
                    }
                    if (dbEcommerceSpec != null)
                    {
                        AutoMapper.Mapper.Map<DTO.ProductMng.ProductECommerceSpec, ProductECommerceSpec>(item, dbEcommerceSpec);
                    }
                }

            }
            //map product
            AutoMapper.Mapper.Map<DTO.ProductMng.Product, Product>(dtoItem, dbItem);


        }
    }
}
