﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.NhanVienMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class NhanVienMngEntities : DbContext
    {
        public NhanVienMngEntities()
            : base("name=NhanVienMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<NguoiPhuThuoc> NguoiPhuThuoc { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<NhanVienMng_NguoiPhuThuoc_View> NhanVienMng_NguoiPhuThuoc_View { get; set; }
        public virtual DbSet<NhanVienMng_NhanVien_View> NhanVienMng_NhanVien_View { get; set; }
        public virtual DbSet<NhanVienMng_NhanVienSearchResult_View> NhanVienMng_NhanVienSearchResult_View { get; set; }
        public virtual DbSet<NhanVienMng_PhongBan_View> NhanVienMng_PhongBan_View { get; set; }
    
        public virtual ObjectResult<NhanVienMng_NhanVienSearchResult_View> NhanVienMng_function_SearchNhanVien(string nhanVienUD, string nhanVienNM, string phongBanNM, string sortedBy, string sortedDirection)
        {
            var nhanVienUDParameter = nhanVienUD != null ?
                new ObjectParameter("NhanVienUD", nhanVienUD) :
                new ObjectParameter("NhanVienUD", typeof(string));
    
            var nhanVienNMParameter = nhanVienNM != null ?
                new ObjectParameter("NhanVienNM", nhanVienNM) :
                new ObjectParameter("NhanVienNM", typeof(string));
    
            var phongBanNMParameter = phongBanNM != null ?
                new ObjectParameter("PhongBanNM", phongBanNM) :
                new ObjectParameter("PhongBanNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NhanVienMng_NhanVienSearchResult_View>("NhanVienMng_function_SearchNhanVien", nhanVienUDParameter, nhanVienNMParameter, phongBanNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<NhanVienMng_NhanVienSearchResult_View> NhanVienMng_function_SearchNhanVien(string nhanVienUD, string nhanVienNM, string phongBanNM, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var nhanVienUDParameter = nhanVienUD != null ?
                new ObjectParameter("NhanVienUD", nhanVienUD) :
                new ObjectParameter("NhanVienUD", typeof(string));
    
            var nhanVienNMParameter = nhanVienNM != null ?
                new ObjectParameter("NhanVienNM", nhanVienNM) :
                new ObjectParameter("NhanVienNM", typeof(string));
    
            var phongBanNMParameter = phongBanNM != null ?
                new ObjectParameter("PhongBanNM", phongBanNM) :
                new ObjectParameter("PhongBanNM", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NhanVienMng_NhanVienSearchResult_View>("NhanVienMng_function_SearchNhanVien", mergeOption, nhanVienUDParameter, nhanVienNMParameter, phongBanNMParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<string> NhanVienMng_function_GenerateNhanVienUD()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("NhanVienMng_function_GenerateNhanVienUD");
        }
    }
}