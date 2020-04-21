﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.PLCMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PLCMngEntities : DbContext
    {
        public PLCMngEntities()
            : base("name=PLCMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<PLC> PLC { get; set; }
        public virtual DbSet<PLCImage> PLCImage { get; set; }
        public virtual DbSet<PLCMng_PLCImage_View> PLCMng_PLCImage_View { get; set; }
        public virtual DbSet<PLCMng_PLCSearchResult_View> PLCMng_PLCSearchResult_View { get; set; }
        public virtual DbSet<PLCMng_ItemForCreatePLC_View> PLCMng_ItemForCreatePLC_View { get; set; }
        public virtual DbSet<PLCMng_PLC_View> PLCMng_PLC_View { get; set; }
    
        public virtual ObjectResult<PLCMng_PLCSearchResult_View> PLCMng_function_SearchPLC(string articleCode, string description, string factoryUD, string bookingUD, string bLNo, string season, string clientUD, string containerNo, string loadingPlanUD, string proformaInvoiceNo, string isConfirmed, string sortedBy, string sortedDirection)
        {
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var bookingUDParameter = bookingUD != null ?
                new ObjectParameter("BookingUD", bookingUD) :
                new ObjectParameter("BookingUD", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var loadingPlanUDParameter = loadingPlanUD != null ?
                new ObjectParameter("LoadingPlanUD", loadingPlanUD) :
                new ObjectParameter("LoadingPlanUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var isConfirmedParameter = isConfirmed != null ?
                new ObjectParameter("IsConfirmed", isConfirmed) :
                new ObjectParameter("IsConfirmed", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PLCMng_PLCSearchResult_View>("PLCMng_function_SearchPLC", articleCodeParameter, descriptionParameter, factoryUDParameter, bookingUDParameter, bLNoParameter, seasonParameter, clientUDParameter, containerNoParameter, loadingPlanUDParameter, proformaInvoiceNoParameter, isConfirmedParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<PLCMng_PLCSearchResult_View> PLCMng_function_SearchPLC(string articleCode, string description, string factoryUD, string bookingUD, string bLNo, string season, string clientUD, string containerNo, string loadingPlanUD, string proformaInvoiceNo, string isConfirmed, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var articleCodeParameter = articleCode != null ?
                new ObjectParameter("ArticleCode", articleCode) :
                new ObjectParameter("ArticleCode", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            var factoryUDParameter = factoryUD != null ?
                new ObjectParameter("FactoryUD", factoryUD) :
                new ObjectParameter("FactoryUD", typeof(string));
    
            var bookingUDParameter = bookingUD != null ?
                new ObjectParameter("BookingUD", bookingUD) :
                new ObjectParameter("BookingUD", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var loadingPlanUDParameter = loadingPlanUD != null ?
                new ObjectParameter("LoadingPlanUD", loadingPlanUD) :
                new ObjectParameter("LoadingPlanUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("ProformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("ProformaInvoiceNo", typeof(string));
    
            var isConfirmedParameter = isConfirmed != null ?
                new ObjectParameter("IsConfirmed", isConfirmed) :
                new ObjectParameter("IsConfirmed", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PLCMng_PLCSearchResult_View>("PLCMng_function_SearchPLC", mergeOption, articleCodeParameter, descriptionParameter, factoryUDParameter, bookingUDParameter, bLNoParameter, seasonParameter, clientUDParameter, containerNoParameter, loadingPlanUDParameter, proformaInvoiceNoParameter, isConfirmedParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
