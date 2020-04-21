﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.OutsourceRpt.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OutsourceRptEntities : DbContext
    {
        public OutsourceRptEntities()
            : base("name=OutsourceRptEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<OutsourceRpt_DocumentNote_View> OutsourceRpt_DocumentNote_View { get; set; }
        public virtual DbSet<OutsourceRpt_ProductionItem_View> OutsourceRpt_ProductionItem_View { get; set; }
        public virtual DbSet<OutsourceRpt_WorkOrder_View> OutsourceRpt_WorkOrder_View { get; set; }
        public virtual DbSet<OutsourceRpt_ProductionTeam_View> OutsourceRpt_ProductionTeam_View { get; set; }
    
        public virtual ObjectResult<OutsourceRpt_DocumentNote_View> OutsourceRpt_function_GetDocumentNote(Nullable<int> productionTeamID, string clientUD, string proformaInvoiceNo, string modelUD, string modelNM, string workOrderUD, Nullable<int> workOrderStatusID, string productionItemTypeIDs, Nullable<int> companyID, Nullable<int> productionItemID)
        {
            var productionTeamIDParameter = productionTeamID.HasValue ?
                new ObjectParameter("productionTeamID", productionTeamID) :
                new ObjectParameter("productionTeamID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("clientUD", clientUD) :
                new ObjectParameter("clientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("proformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("proformaInvoiceNo", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("modelUD", modelUD) :
                new ObjectParameter("modelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("modelNM", modelNM) :
                new ObjectParameter("modelNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("workOrderUD", workOrderUD) :
                new ObjectParameter("workOrderUD", typeof(string));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("workOrderStatusID", workOrderStatusID) :
                new ObjectParameter("workOrderStatusID", typeof(int));
    
            var productionItemTypeIDsParameter = productionItemTypeIDs != null ?
                new ObjectParameter("productionItemTypeIDs", productionItemTypeIDs) :
                new ObjectParameter("productionItemTypeIDs", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            var productionItemIDParameter = productionItemID.HasValue ?
                new ObjectParameter("productionItemID", productionItemID) :
                new ObjectParameter("productionItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourceRpt_DocumentNote_View>("OutsourceRpt_function_GetDocumentNote", productionTeamIDParameter, clientUDParameter, proformaInvoiceNoParameter, modelUDParameter, modelNMParameter, workOrderUDParameter, workOrderStatusIDParameter, productionItemTypeIDsParameter, companyIDParameter, productionItemIDParameter);
        }
    
        public virtual ObjectResult<OutsourceRpt_DocumentNote_View> OutsourceRpt_function_GetDocumentNote(Nullable<int> productionTeamID, string clientUD, string proformaInvoiceNo, string modelUD, string modelNM, string workOrderUD, Nullable<int> workOrderStatusID, string productionItemTypeIDs, Nullable<int> companyID, Nullable<int> productionItemID, MergeOption mergeOption)
        {
            var productionTeamIDParameter = productionTeamID.HasValue ?
                new ObjectParameter("productionTeamID", productionTeamID) :
                new ObjectParameter("productionTeamID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("clientUD", clientUD) :
                new ObjectParameter("clientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("proformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("proformaInvoiceNo", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("modelUD", modelUD) :
                new ObjectParameter("modelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("modelNM", modelNM) :
                new ObjectParameter("modelNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("workOrderUD", workOrderUD) :
                new ObjectParameter("workOrderUD", typeof(string));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("workOrderStatusID", workOrderStatusID) :
                new ObjectParameter("workOrderStatusID", typeof(int));
    
            var productionItemTypeIDsParameter = productionItemTypeIDs != null ?
                new ObjectParameter("productionItemTypeIDs", productionItemTypeIDs) :
                new ObjectParameter("productionItemTypeIDs", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            var productionItemIDParameter = productionItemID.HasValue ?
                new ObjectParameter("productionItemID", productionItemID) :
                new ObjectParameter("productionItemID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourceRpt_DocumentNote_View>("OutsourceRpt_function_GetDocumentNote", mergeOption, productionTeamIDParameter, clientUDParameter, proformaInvoiceNoParameter, modelUDParameter, modelNMParameter, workOrderUDParameter, workOrderStatusIDParameter, productionItemTypeIDsParameter, companyIDParameter, productionItemIDParameter);
        }
    
        public virtual ObjectResult<OutsourceRpt_ProductionItem_View> OutsourceRpt_function_GetProductionItem(Nullable<int> productionTeamID, string clientUD, string proformaInvoiceNo, string modelUD, string modelNM, string workOrderUD, Nullable<int> workOrderStatusID, string productionItemTypeIDs, Nullable<int> companyID)
        {
            var productionTeamIDParameter = productionTeamID.HasValue ?
                new ObjectParameter("productionTeamID", productionTeamID) :
                new ObjectParameter("productionTeamID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("clientUD", clientUD) :
                new ObjectParameter("clientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("proformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("proformaInvoiceNo", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("modelUD", modelUD) :
                new ObjectParameter("modelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("modelNM", modelNM) :
                new ObjectParameter("modelNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("workOrderUD", workOrderUD) :
                new ObjectParameter("workOrderUD", typeof(string));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("workOrderStatusID", workOrderStatusID) :
                new ObjectParameter("workOrderStatusID", typeof(int));
    
            var productionItemTypeIDsParameter = productionItemTypeIDs != null ?
                new ObjectParameter("productionItemTypeIDs", productionItemTypeIDs) :
                new ObjectParameter("productionItemTypeIDs", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourceRpt_ProductionItem_View>("OutsourceRpt_function_GetProductionItem", productionTeamIDParameter, clientUDParameter, proformaInvoiceNoParameter, modelUDParameter, modelNMParameter, workOrderUDParameter, workOrderStatusIDParameter, productionItemTypeIDsParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<OutsourceRpt_ProductionItem_View> OutsourceRpt_function_GetProductionItem(Nullable<int> productionTeamID, string clientUD, string proformaInvoiceNo, string modelUD, string modelNM, string workOrderUD, Nullable<int> workOrderStatusID, string productionItemTypeIDs, Nullable<int> companyID, MergeOption mergeOption)
        {
            var productionTeamIDParameter = productionTeamID.HasValue ?
                new ObjectParameter("productionTeamID", productionTeamID) :
                new ObjectParameter("productionTeamID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("clientUD", clientUD) :
                new ObjectParameter("clientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("proformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("proformaInvoiceNo", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("modelUD", modelUD) :
                new ObjectParameter("modelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("modelNM", modelNM) :
                new ObjectParameter("modelNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("workOrderUD", workOrderUD) :
                new ObjectParameter("workOrderUD", typeof(string));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("workOrderStatusID", workOrderStatusID) :
                new ObjectParameter("workOrderStatusID", typeof(int));
    
            var productionItemTypeIDsParameter = productionItemTypeIDs != null ?
                new ObjectParameter("productionItemTypeIDs", productionItemTypeIDs) :
                new ObjectParameter("productionItemTypeIDs", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourceRpt_ProductionItem_View>("OutsourceRpt_function_GetProductionItem", mergeOption, productionTeamIDParameter, clientUDParameter, proformaInvoiceNoParameter, modelUDParameter, modelNMParameter, workOrderUDParameter, workOrderStatusIDParameter, productionItemTypeIDsParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<OutsourceRpt_WorkOrder_View> OutsourceRpt_function_GetWorkOrder(Nullable<int> productionTeamID, string clientUD, string proformaInvoiceNo, string modelUD, string modelNM, string workOrderUD, Nullable<int> workOrderStatusID, string productionItemTypeIDs, Nullable<int> companyID)
        {
            var productionTeamIDParameter = productionTeamID.HasValue ?
                new ObjectParameter("productionTeamID", productionTeamID) :
                new ObjectParameter("productionTeamID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("clientUD", clientUD) :
                new ObjectParameter("clientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("proformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("proformaInvoiceNo", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("modelUD", modelUD) :
                new ObjectParameter("modelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("modelNM", modelNM) :
                new ObjectParameter("modelNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("workOrderUD", workOrderUD) :
                new ObjectParameter("workOrderUD", typeof(string));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("workOrderStatusID", workOrderStatusID) :
                new ObjectParameter("workOrderStatusID", typeof(int));
    
            var productionItemTypeIDsParameter = productionItemTypeIDs != null ?
                new ObjectParameter("productionItemTypeIDs", productionItemTypeIDs) :
                new ObjectParameter("productionItemTypeIDs", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourceRpt_WorkOrder_View>("OutsourceRpt_function_GetWorkOrder", productionTeamIDParameter, clientUDParameter, proformaInvoiceNoParameter, modelUDParameter, modelNMParameter, workOrderUDParameter, workOrderStatusIDParameter, productionItemTypeIDsParameter, companyIDParameter);
        }
    
        public virtual ObjectResult<OutsourceRpt_WorkOrder_View> OutsourceRpt_function_GetWorkOrder(Nullable<int> productionTeamID, string clientUD, string proformaInvoiceNo, string modelUD, string modelNM, string workOrderUD, Nullable<int> workOrderStatusID, string productionItemTypeIDs, Nullable<int> companyID, MergeOption mergeOption)
        {
            var productionTeamIDParameter = productionTeamID.HasValue ?
                new ObjectParameter("productionTeamID", productionTeamID) :
                new ObjectParameter("productionTeamID", typeof(int));
    
            var clientUDParameter = clientUD != null ?
                new ObjectParameter("clientUD", clientUD) :
                new ObjectParameter("clientUD", typeof(string));
    
            var proformaInvoiceNoParameter = proformaInvoiceNo != null ?
                new ObjectParameter("proformaInvoiceNo", proformaInvoiceNo) :
                new ObjectParameter("proformaInvoiceNo", typeof(string));
    
            var modelUDParameter = modelUD != null ?
                new ObjectParameter("modelUD", modelUD) :
                new ObjectParameter("modelUD", typeof(string));
    
            var modelNMParameter = modelNM != null ?
                new ObjectParameter("modelNM", modelNM) :
                new ObjectParameter("modelNM", typeof(string));
    
            var workOrderUDParameter = workOrderUD != null ?
                new ObjectParameter("workOrderUD", workOrderUD) :
                new ObjectParameter("workOrderUD", typeof(string));
    
            var workOrderStatusIDParameter = workOrderStatusID.HasValue ?
                new ObjectParameter("workOrderStatusID", workOrderStatusID) :
                new ObjectParameter("workOrderStatusID", typeof(int));
    
            var productionItemTypeIDsParameter = productionItemTypeIDs != null ?
                new ObjectParameter("productionItemTypeIDs", productionItemTypeIDs) :
                new ObjectParameter("productionItemTypeIDs", typeof(string));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("companyID", companyID) :
                new ObjectParameter("companyID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<OutsourceRpt_WorkOrder_View>("OutsourceRpt_function_GetWorkOrder", mergeOption, productionTeamIDParameter, clientUDParameter, proformaInvoiceNoParameter, modelUDParameter, modelNMParameter, workOrderUDParameter, workOrderStatusIDParameter, productionItemTypeIDsParameter, companyIDParameter);
        }
    }
}
