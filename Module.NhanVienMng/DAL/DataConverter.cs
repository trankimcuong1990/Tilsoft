using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;

namespace Module.NhanVienMng.DAL
{
    internal class DataConverter
    {
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataConverter()
        {
            string mapName = this.GetType().Assembly.GetName().Name;
            if (!FrameworkSetting.Setting.Maps.Contains(mapName))
            {
                // 
                // MAPPING DECLARATION
                //
                AutoMapper.Mapper.CreateMap<NhanVienMng_NhanVienSearchResult_View, DTO.NhanVienSearchResultDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.NgaySinh, o => o.ResolveUsing(s => s.NgaySinh.HasValue ? s.NgaySinh.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NhanVienMng_NguoiPhuThuoc_View, DTO.NguoiPhuThuocDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.NgaySinh, o => o.ResolveUsing(s => s.NgaySinh.HasValue ? s.NgaySinh.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NhanVienMng_NhanVien_View, DTO.NhanVienDTO>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.UpdatedDate, o => o.ResolveUsing(s => s.UpdatedDate.HasValue ? s.UpdatedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.NgaySinh, o => o.ResolveUsing(s => s.NgaySinh.HasValue ? s.NgaySinh.Value.ToString("dd/MM/yyyy HH:mm:ss") : ""))
                    .ForMember(d => d.ThumbnailLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaThumbnailUrl + (!string.IsNullOrEmpty(s.ThumbnailLocation) ? s.ThumbnailLocation : "no-image.jpg")))
                    .ForMember(d => d.FileLocation, o => o.ResolveUsing(s => FrameworkSetting.Setting.MediaFullSizeUrl + (!string.IsNullOrEmpty(s.FileLocation) ? s.FileLocation : "no-image.jpg")))
                    .ForMember(d => d.NguoiPhuThuocDTOs, o => o.MapFrom(s => s.NhanVienMng_NguoiPhuThuoc_View))
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<NhanVienMng_PhongBan_View, DTO.PhongBanDTO>()
                    .IgnoreAllNonExisting()
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.NhanVienDTO, NhanVien>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.NhanVienID, o => o.Ignore())
                    .ForMember(d => d.NhanVienUD, o => o.Ignore())
                    .ForMember(d => d.NguoiPhuThuoc, o => o.Ignore())
                    .ForMember(d => d.UpdatedBy, o => o.Ignore())
                    .ForMember(d => d.UpdatedDate, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                AutoMapper.Mapper.CreateMap<DTO.NguoiPhuThuocDTO, NguoiPhuThuoc>()
                    .IgnoreAllNonExisting()
                    .ForMember(d => d.NhanVienID, o => o.Ignore())
                    .ForMember(d => d.NguoiPhuThuocID, o => o.Ignore())
                    .ForAllMembers(d => d.Condition(s => !s.IsSourceValueNull));

                FrameworkSetting.Setting.Maps.Add(mapName);
            }
        }

        public List<DTO.NhanVienSearchResultDTO> DB2DTO_NhanVienSearchResult(List<NhanVienMng_NhanVienSearchResult_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<NhanVienMng_NhanVienSearchResult_View>, List<DTO.NhanVienSearchResultDTO>>(dbItems);
        }

        public DTO.NhanVienDTO DB2DTO_NhanVienDTO(NhanVienMng_NhanVien_View dbItem)
        {
            return AutoMapper.Mapper.Map<NhanVienMng_NhanVien_View, DTO.NhanVienDTO>(dbItem);
        }

        public List<DTO.PhongBanDTO> DB2DTO_PhongBanDTO(List<NhanVienMng_PhongBan_View> dbItems)
        {
            return AutoMapper.Mapper.Map<List<NhanVienMng_PhongBan_View>, List<DTO.PhongBanDTO>>(dbItems);
        }

        public void DTO2DB(DTO.NhanVienDTO dtoItem, ref NhanVien dbItem, int userId)
        {
            string TmpFile = FrameworkSetting.Setting.AbsoluteUserTempFolder + userId.ToString() + @"\";

            // remark
            if (dtoItem.PhotoHasChanged)
            {
                dtoItem.Photo = fwFactory.CreateFilePointer(TmpFile, dtoItem.NewPhoto, dtoItem.Photo);
            }
            AutoMapper.Mapper.Map<DTO.NhanVienDTO, NhanVien>(dtoItem, dbItem);


            // remark image
            foreach (NguoiPhuThuoc dbNPT in dbItem.NguoiPhuThuoc.ToArray())
            {
                if (!dtoItem.NguoiPhuThuocDTOs.Select(o => o.NguoiPhuThuocID).Contains(dbNPT.NguoiPhuThuocID))
                {
                    dbItem.NguoiPhuThuoc.Remove(dbNPT);
                }
            }
            foreach (DTO.NguoiPhuThuocDTO dtoNPT in dtoItem.NguoiPhuThuocDTOs)
            {
                NguoiPhuThuoc dbNPT;
                if (dtoNPT.NguoiPhuThuocID <= 0)
                {
                    dbNPT = new NguoiPhuThuoc();
                    dbItem.NguoiPhuThuoc.Add(dbNPT);
                }
                else
                {
                    dbNPT = dbItem.NguoiPhuThuoc.FirstOrDefault(o => o.NguoiPhuThuocID == dtoNPT.NguoiPhuThuocID);
                }

                if (dbNPT != null)
                {
                    AutoMapper.Mapper.Map<DTO.NguoiPhuThuocDTO, NguoiPhuThuoc>(dtoNPT, dbNPT);
                }
            }
        }
    }
}
