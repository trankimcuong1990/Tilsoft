using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace MaintenanceTask.EurofarStockSyncData
{
    internal class DataConverter
    {
        public DataConverter()
        {
            // 
            // MAPPING DECLARATION
            //
            AutoMapper.Mapper.CreateMap<WarehouseConnectMng_Product_View, DTO.ProductDTO>()
                .IgnoreAllNonExisting()
                .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));
        }

        public List<DTO.ProductDTO> DB2DTO_Product(List<WarehouseConnectMng_Product_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<WarehouseConnectMng_Product_View>, List<DTO.ProductDTO>>(dbItems);
        }

        public DTO.EurofarStock.DataContract.Post.productContract Tilsoft2EurofarProduct(DTO.ProductDTO item)
        {
            DTO.EurofarStock.DataContract.Post.productContract postData = new DTO.EurofarStock.DataContract.Post.productContract();
            postData.product = new DTO.EurofarStock.DataContract.Post.productContractDetail()
            {
                sku = item.ArticleCode
                , name = item.Description
                , attribute_set_id = (int)DTO.EurofarStock.EUConstant.AttributeSetID
                , status = (int)DTO.EurofarStock.EUConstant.EnabledStatus
                , visibility = (int)DTO.EurofarStock.EUConstant.Visibility
                , type_id = "simple"
                , custom_attributes = new List<DTO.EurofarStock.DataContract.Post.productContractCustomAttribute>()
            };

            if (!item.UnitPrice.HasValue || item.UnitPrice.Value == 0)
            {
                postData.product.price = 0;
                postData.product.status = (int)DTO.EurofarStock.EUConstant.DisabledStatus;
            }
            else
            {
                //postData.product.price = item.UnitPrice.Value;
                postData.product.price = Math.Round(item.UnitPrice.Value * (decimal)1.3, 2, MidpointRounding.AwayFromZero); // up 30% for magento discount
            }

            // map custom attribute
            postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "tax_class_id", value = (int)DTO.EurofarStock.EUConstant.TaxClass });
            postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "sorting_qnt", value = item.StockQnt });
            if (!string.IsNullOrEmpty(item.ModelNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "short_description", value = item.Description });
            if (!string.IsNullOrEmpty(item.ArticleCode)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "article_code", value = item.ArticleCode });
            if (!string.IsNullOrEmpty(item.SubEANCode)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "sub_ean_code", value = item.SubEANCode });
            if (!string.IsNullOrEmpty(item.MaterialColorNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "material_color", value = item.MaterialColorNM });
            if (!string.IsNullOrEmpty(item.MaterialTypeNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "material_type", value = item.MaterialTypeNM });
            if (!string.IsNullOrEmpty(item.FrameMaterialNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "frame_material", value = item.FrameMaterialNM });
            if (!string.IsNullOrEmpty(item.FrameMaterialColorNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "frame_material_color", value = item.FrameMaterialColorNM });
            if (!string.IsNullOrEmpty(item.BackCushionNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "back_cushion", value = item.BackCushionNM });
            if (!string.IsNullOrEmpty(item.SeatCushionNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_cushion", value = item.SeatCushionNM });
            if (!string.IsNullOrEmpty(item.CushionColorNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "cushion_color", value = item.CushionColorNM });
            if (!string.IsNullOrEmpty(item.OverallDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "height", value = item.OverallDimH + "cm" });
            if (!string.IsNullOrEmpty(item.OverallDimL)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "length", value = item.OverallDimL + "cm" });
            if (!string.IsNullOrEmpty(item.OverallDimW)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "width", value = item.OverallDimW + "cm" });
            if (!string.IsNullOrEmpty(item.SeatCushionDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_height", value = item.SeatCushionDimH });
            if (!string.IsNullOrEmpty(item.SeatCushionDimL)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_length", value = item.SeatCushionDimL });
            if (!string.IsNullOrEmpty(item.SeatCushionDimW)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_width", value = item.SeatCushionDimW });
            if (!string.IsNullOrEmpty(item.OverallDimL) && !string.IsNullOrEmpty(item.OverallDimW) && !string.IsNullOrEmpty(item.OverallDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "wxlxh_cm", value = item.OverallDimW + "cm x " + item.OverallDimL + "cm x " + item.OverallDimH + "cm" });
            if (!string.IsNullOrEmpty(item.CartonBoxDimL)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "carton_box_length", value = item.CartonBoxDimL });
            if (!string.IsNullOrEmpty(item.CartonBoxDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "carton_box_height", value = item.CartonBoxDimH });
            if (!string.IsNullOrEmpty(item.CartonBoxDimW)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "carton_box_width", value = item.CartonBoxDimW });
            if (item.Qnt40HC.HasValue) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "qnt40hc", value = item.Qnt40HC.Value.ToString() });
            return postData;
        }

        public DTO.EurofarStock.DataContract.Post.productContractForUpdate Tilsoft2EurofarProductForUpdate(DTO.ProductDTO item)
        {
            DTO.EurofarStock.DataContract.Post.productContractForUpdate postData = new DTO.EurofarStock.DataContract.Post.productContractForUpdate();
            postData.product = new DTO.EurofarStock.DataContract.Post.productContractDetailForUpdate()
            {
                sku = item.ArticleCode
                , attribute_set_id = (int)DTO.EurofarStock.EUConstant.AttributeSetID
                , status = (int)(item.IsEnabled ? DTO.EurofarStock.EUConstant.EnabledStatus : DTO.EurofarStock.EUConstant.DisabledStatus)
                , visibility = (int)DTO.EurofarStock.EUConstant.Visibility
                , type_id = "simple"
                , custom_attributes = new List<DTO.EurofarStock.DataContract.Post.productContractCustomAttribute>()
            };

            if (!item.UnitPrice.HasValue || item.UnitPrice.Value == 0)
            {
                postData.product.price = 0;
                postData.product.status = (int)DTO.EurofarStock.EUConstant.DisabledStatus;
            }
            else
            {
                //postData.product.price = item.UnitPrice.Value;
                postData.product.price = Math.Round(item.UnitPrice.Value * (decimal)1.3, 2, MidpointRounding.AwayFromZero); // up 30% for magento discount
            }

            // map custom attribute
            postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "tax_class_id", value = (int)DTO.EurofarStock.EUConstant.TaxClass });
            postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "sorting_qnt", value = item.StockQnt });
            if (!string.IsNullOrEmpty(item.ModelNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "short_description", value = item.Description });
            if (!string.IsNullOrEmpty(item.ArticleCode)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "article_code", value = item.ArticleCode });
            if (!string.IsNullOrEmpty(item.SubEANCode)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "sub_ean_code", value = item.SubEANCode });
            if (!string.IsNullOrEmpty(item.MaterialColorNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "material_color", value = item.MaterialColorNM });
            if (!string.IsNullOrEmpty(item.MaterialTypeNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "material_type", value = item.MaterialTypeNM });
            if (!string.IsNullOrEmpty(item.FrameMaterialNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "frame_material", value = item.FrameMaterialNM });
            if (!string.IsNullOrEmpty(item.FrameMaterialColorNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "frame_material_color", value = item.FrameMaterialColorNM });
            if (!string.IsNullOrEmpty(item.BackCushionNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "back_cushion", value = item.BackCushionNM });
            if (!string.IsNullOrEmpty(item.SeatCushionNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_cushion", value = item.SeatCushionNM });
            if (!string.IsNullOrEmpty(item.CushionColorNM)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "cushion_color", value = item.CushionColorNM });
            if (!string.IsNullOrEmpty(item.OverallDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "height", value = item.OverallDimH + "cm" });
            if (!string.IsNullOrEmpty(item.OverallDimL)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "length", value = item.OverallDimL + "cm" });
            if (!string.IsNullOrEmpty(item.OverallDimW)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "width", value = item.OverallDimW + "cm" });
            if (!string.IsNullOrEmpty(item.SeatCushionDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_height", value = item.SeatCushionDimH });
            if (!string.IsNullOrEmpty(item.SeatCushionDimL)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_length", value = item.SeatCushionDimL });
            if (!string.IsNullOrEmpty(item.SeatCushionDimW)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "seat_width", value = item.SeatCushionDimW });
            if (!string.IsNullOrEmpty(item.OverallDimL) && !string.IsNullOrEmpty(item.OverallDimW) && !string.IsNullOrEmpty(item.OverallDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "wxlxh_cm", value = item.OverallDimW + "cm x " + item.OverallDimL + "cm x " + item.OverallDimH + "cm" });
            if (!string.IsNullOrEmpty(item.CartonBoxDimL)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "carton_box_length", value = item.CartonBoxDimL });
            if (!string.IsNullOrEmpty(item.CartonBoxDimH)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "carton_box_height", value = item.CartonBoxDimH });
            if (!string.IsNullOrEmpty(item.CartonBoxDimW)) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "carton_box_width", value = item.CartonBoxDimW });
            if (item.Qnt40HC.HasValue) postData.product.custom_attributes.Add(new DTO.EurofarStock.DataContract.Post.productContractCustomAttribute() { attribute_code = "qnt40hc", value = item.Qnt40HC.Value.ToString() });
            return postData;
        }
    }


}
