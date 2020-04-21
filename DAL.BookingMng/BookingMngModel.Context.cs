﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.BookingMng
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BookingMngEntities : DbContext
    {
        public BookingMngEntities()
            : base("name=BookingMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<BookingDetail> BookingDetail { get; set; }
        public virtual DbSet<BookingMng_Booking_View> BookingMng_Booking_View { get; set; }
        public virtual DbSet<BookingMng_BookingSearchResult_View> BookingMng_BookingSearchResult_View { get; set; }
        public virtual DbSet<BookingMng_LoadingPlan_View> BookingMng_LoadingPlan_View { get; set; }
    
        public virtual ObjectResult<BookingMng_BookingSearchResult_View> BookingMng_function_SearchBooking(string bookingUD, string season, string bLNo, string containerNo, string clientUD, string sortedBy, string sortedDirection)
        {
            var bookingUDParameter = bookingUD != null ?
                new ObjectParameter("BookingUD", bookingUD) :
                new ObjectParameter("BookingUD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BookingMng_BookingSearchResult_View>("BookingMng_function_SearchBooking", bookingUDParameter, seasonParameter, bLNoParameter, containerNoParameter, clientUDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<BookingMng_BookingSearchResult_View> BookingMng_function_SearchBooking(string bookingUD, string season, string bLNo, string containerNo, string clientUD, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var bookingUDParameter = bookingUD != null ?
                new ObjectParameter("BookingUD", bookingUD) :
                new ObjectParameter("BookingUD", typeof(string));
    
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            var bLNoParameter = bLNo != null ?
                new ObjectParameter("BLNo", bLNo) :
                new ObjectParameter("BLNo", typeof(string));
    
            var containerNoParameter = containerNo != null ?
                new ObjectParameter("ContainerNo", containerNo) :
                new ObjectParameter("ContainerNo", typeof(string));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("ClientUD", clientUD) :
                new ObjectParameter("ClientUD", typeof(string));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BookingMng_BookingSearchResult_View>("BookingMng_function_SearchBooking", mergeOption, bookingUDParameter, seasonParameter, bLNoParameter, containerNoParameter, clientUDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual int BookingMng_function_UpdateBookingRef(Nullable<int> bookingID)
        {
            var bookingIDParameter = bookingID.HasValue ?
                new ObjectParameter("BookingID", bookingID) :
                new ObjectParameter("BookingID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BookingMng_function_UpdateBookingRef", bookingIDParameter);
        }
    
        public virtual int BookingMng_function_ConfirmETA(Nullable<int> bookingID, Nullable<int> confirmedBy, Nullable<System.DateTime> eTA, Nullable<int> eTAType)
        {
            var bookingIDParameter = bookingID.HasValue ?
                new ObjectParameter("BookingID", bookingID) :
                new ObjectParameter("BookingID", typeof(int));
    
            var confirmedByParameter = confirmedBy.HasValue ?
                new ObjectParameter("ConfirmedBy", confirmedBy) :
                new ObjectParameter("ConfirmedBy", typeof(int));
    
            var eTAParameter = eTA.HasValue ?
                new ObjectParameter("ETA", eTA) :
                new ObjectParameter("ETA", typeof(System.DateTime));
    
            var eTATypeParameter = eTAType.HasValue ?
                new ObjectParameter("ETAType", eTAType) :
                new ObjectParameter("ETAType", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("BookingMng_function_ConfirmETA", bookingIDParameter, confirmedByParameter, eTAParameter, eTATypeParameter);
        }
    }
}
