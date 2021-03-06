﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.EmployeeMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EmployeeMngEntities : DbContext
    {
        public EmployeeMngEntities()
            : base("name=EmployeeMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<EmployeeMng_Director_View> EmployeeMng_Director_View { get; set; }
        public virtual DbSet<EmployeeMng_AnnualLeaveSetting_View> EmployeeMng_AnnualLeaveSetting_View { get; set; }
        public virtual DbSet<AnnualLeaveSetting> AnnualLeaveSetting { get; set; }
        public virtual DbSet<EmployeeFactory> EmployeeFactory { get; set; }
        public virtual DbSet<EmployeeMng_EmployeeFactory_View> EmployeeMng_EmployeeFactory_View { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeMng_Employee_View> EmployeeMng_Employee_View { get; set; }
        public virtual DbSet<EmployeeMng_EmployeeSearchResult_View> EmployeeMng_EmployeeSearchResult_View { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<EmployeeMng_EmployeeFile_View> EmployeeMng_EmployeeFile_View { get; set; }
        public virtual DbSet<EmployeeFile> EmployeeFile { get; set; }
        public virtual DbSet<EmployeeMng_UserActionDetail_View> EmployeeMng_UserActionDetail_View { get; set; }
        public virtual DbSet<EmployeeMng_UserActionDetailWeeklyDetail_View> EmployeeMng_UserActionDetailWeeklyDetail_View { get; set; }
        public virtual DbSet<EmployeeMng_UserActionDetailWeeklyOverview_View> EmployeeMng_UserActionDetailWeeklyOverview_View { get; set; }
        public virtual DbSet<EmployeeContract> EmployeeContract { get; set; }
        public virtual DbSet<EmployeeMng_EmployeeContract_View> EmployeeMng_EmployeeContract_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_EmployeeContract_View> EmployeeMng_Overview_EmployeeContract_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_EmployeeFactory_View> EmployeeMng_Overview_EmployeeFactory_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_EmployeeFile_View> EmployeeMng_Overview_EmployeeFile_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_UserActionDetail_View> EmployeeMng_Overview_UserActionDetail_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View> EmployeeMng_Overview_UserActionDetailWeeklyDetail_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View> EmployeeMng_Overview_UserActionDetailWeeklyOverview_View { get; set; }
        public virtual DbSet<EmployeeMng_Overview_Employee_View> EmployeeMng_Overview_Employee_View { get; set; }
        public virtual DbSet<EmployeeMng_Employee_ReportView> EmployeeMng_Employee_ReportView { get; set; }
        public virtual DbSet<EmployeeMng_TypeOfContract_View> EmployeeMng_TypeOfContract_View { get; set; }
        public virtual DbSet<SupportMng_AccountManagerType_View> SupportMng_AccountManagerType_View { get; set; }
        public virtual DbSet<EmployeeMng_Branch_View> EmployeeMng_Branch_View { get; set; }
        public virtual DbSet<EmployeeMng_Company_View> EmployeeMng_Company_View { get; set; }
        public virtual DbSet<EmployeeResponsibleFor> EmployeeResponsibleFor { get; set; }
        public virtual DbSet<EmployeeMng_ResposibleFor_View> EmployeeMng_ResposibleFor_View { get; set; }
        public virtual DbSet<QAQCFactoryAccess> QAQCFactoryAccess { get; set; }
        public virtual DbSet<EmployeeMng_QAQCFactoryAccess_View> EmployeeMng_QAQCFactoryAccess_View { get; set; }
        public virtual DbSet<DashboardMng_UserDataRpt_View> DashboardMng_UserDataRpt_View { get; set; }
        public virtual DbSet<EmployeeMng_HitCountDataRpt_View> EmployeeMng_HitCountDataRpt_View { get; set; }
    
        public virtual ObjectResult<EmployeeMng_function_SearchUnSelectedUser_Result> EmployeeMng_function_SearchUnSelectedUser(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_function_SearchUnSelectedUser_Result>("EmployeeMng_function_SearchUnSelectedUser", userIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_function_TilsoftAverageUsage_Result> EmployeeMng_function_TilsoftAverageUsage()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_function_TilsoftAverageUsage_Result>("EmployeeMng_function_TilsoftAverageUsage");
        }
    
        public virtual ObjectResult<EmployeeMng_EmployeeSearchResult_View> EmployeeMng_function_SearchEmployee(string employeeNM, string jobTitle, Nullable<int> jobLevelID, Nullable<int> departmentID, Nullable<int> companyID, string factoryIDs, Nullable<bool> isSuperUser, Nullable<int> locationID, string employeeFirstNM, Nullable<int> userID, Nullable<bool> hasleft, string sortedBy, string sortedDirection, Nullable<int> branchID)
        {
            var employeeNMParameter = employeeNM != null ?
                new ObjectParameter("EmployeeNM", employeeNM) :
                new ObjectParameter("EmployeeNM", typeof(string));
    
            var jobTitleParameter = jobTitle != null ?
                new ObjectParameter("JobTitle", jobTitle) :
                new ObjectParameter("JobTitle", typeof(string));
    
            var jobLevelIDParameter = jobLevelID.HasValue ?
                new ObjectParameter("JobLevelID", jobLevelID) :
                new ObjectParameter("JobLevelID", typeof(int));
    
            var departmentIDParameter = departmentID.HasValue ?
                new ObjectParameter("DepartmentID", departmentID) :
                new ObjectParameter("DepartmentID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var factoryIDsParameter = factoryIDs != null ?
                new ObjectParameter("FactoryIDs", factoryIDs) :
                new ObjectParameter("FactoryIDs", typeof(string));
    
            var isSuperUserParameter = isSuperUser.HasValue ?
                new ObjectParameter("IsSuperUser", isSuperUser) :
                new ObjectParameter("IsSuperUser", typeof(bool));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("LocationID", locationID) :
                new ObjectParameter("LocationID", typeof(int));
    
            var employeeFirstNMParameter = employeeFirstNM != null ?
                new ObjectParameter("EmployeeFirstNM", employeeFirstNM) :
                new ObjectParameter("EmployeeFirstNM", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var hasleftParameter = hasleft.HasValue ?
                new ObjectParameter("Hasleft", hasleft) :
                new ObjectParameter("Hasleft", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("BranchID", branchID) :
                new ObjectParameter("BranchID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_EmployeeSearchResult_View>("EmployeeMng_function_SearchEmployee", employeeNMParameter, jobTitleParameter, jobLevelIDParameter, departmentIDParameter, companyIDParameter, factoryIDsParameter, isSuperUserParameter, locationIDParameter, employeeFirstNMParameter, userIDParameter, hasleftParameter, sortedByParameter, sortedDirectionParameter, branchIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_EmployeeSearchResult_View> EmployeeMng_function_SearchEmployee(string employeeNM, string jobTitle, Nullable<int> jobLevelID, Nullable<int> departmentID, Nullable<int> companyID, string factoryIDs, Nullable<bool> isSuperUser, Nullable<int> locationID, string employeeFirstNM, Nullable<int> userID, Nullable<bool> hasleft, string sortedBy, string sortedDirection, Nullable<int> branchID, MergeOption mergeOption)
        {
            var employeeNMParameter = employeeNM != null ?
                new ObjectParameter("EmployeeNM", employeeNM) :
                new ObjectParameter("EmployeeNM", typeof(string));
    
            var jobTitleParameter = jobTitle != null ?
                new ObjectParameter("JobTitle", jobTitle) :
                new ObjectParameter("JobTitle", typeof(string));
    
            var jobLevelIDParameter = jobLevelID.HasValue ?
                new ObjectParameter("JobLevelID", jobLevelID) :
                new ObjectParameter("JobLevelID", typeof(int));
    
            var departmentIDParameter = departmentID.HasValue ?
                new ObjectParameter("DepartmentID", departmentID) :
                new ObjectParameter("DepartmentID", typeof(int));
    
            var companyIDParameter = companyID.HasValue ?
                new ObjectParameter("CompanyID", companyID) :
                new ObjectParameter("CompanyID", typeof(int));
    
            var factoryIDsParameter = factoryIDs != null ?
                new ObjectParameter("FactoryIDs", factoryIDs) :
                new ObjectParameter("FactoryIDs", typeof(string));
    
            var isSuperUserParameter = isSuperUser.HasValue ?
                new ObjectParameter("IsSuperUser", isSuperUser) :
                new ObjectParameter("IsSuperUser", typeof(bool));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("LocationID", locationID) :
                new ObjectParameter("LocationID", typeof(int));
    
            var employeeFirstNMParameter = employeeFirstNM != null ?
                new ObjectParameter("EmployeeFirstNM", employeeFirstNM) :
                new ObjectParameter("EmployeeFirstNM", typeof(string));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var hasleftParameter = hasleft.HasValue ?
                new ObjectParameter("Hasleft", hasleft) :
                new ObjectParameter("Hasleft", typeof(bool));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("BranchID", branchID) :
                new ObjectParameter("BranchID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_EmployeeSearchResult_View>("EmployeeMng_function_SearchEmployee", mergeOption, employeeNMParameter, jobTitleParameter, jobLevelIDParameter, departmentIDParameter, companyIDParameter, factoryIDsParameter, isSuperUserParameter, locationIDParameter, employeeFirstNMParameter, userIDParameter, hasleftParameter, sortedByParameter, sortedDirectionParameter, branchIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_UserActionDetail_View> EmployeeMng_function_getUserActionDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_UserActionDetail_View>("EmployeeMng_function_getUserActionDetail", userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_UserActionDetail_View> EmployeeMng_function_getUserActionDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_UserActionDetail_View>("EmployeeMng_function_getUserActionDetail", mergeOption, userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_UserActionDetailWeeklyDetail_View> EmployeeMng_function_getUserActionDetailWeeklyDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_UserActionDetailWeeklyDetail_View>("EmployeeMng_function_getUserActionDetailWeeklyDetail", userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_UserActionDetailWeeklyDetail_View> EmployeeMng_function_getUserActionDetailWeeklyDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_UserActionDetailWeeklyDetail_View>("EmployeeMng_function_getUserActionDetailWeeklyDetail", mergeOption, userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_UserActionDetailWeeklyOverview_View> EmployeeMng_function_getUserActionDetailWeeklyOverview(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_UserActionDetailWeeklyOverview_View>("EmployeeMng_function_getUserActionDetailWeeklyOverview", userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_UserActionDetailWeeklyOverview_View> EmployeeMng_function_getUserActionDetailWeeklyOverview(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_UserActionDetailWeeklyOverview_View>("EmployeeMng_function_getUserActionDetailWeeklyOverview", mergeOption, userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Overview_UserActionDetail_View> EmployeeMng_Overview_function_getUserActionDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Overview_UserActionDetail_View>("EmployeeMng_Overview_function_getUserActionDetail", userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Overview_UserActionDetail_View> EmployeeMng_Overview_function_getUserActionDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Overview_UserActionDetail_View>("EmployeeMng_Overview_function_getUserActionDetail", mergeOption, userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View> EmployeeMng_Overview_function_getUserActionDetailWeeklyDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View>("EmployeeMng_Overview_function_getUserActionDetailWeeklyDetail", userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View> EmployeeMng_Overview_function_getUserActionDetailWeeklyDetail(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Overview_UserActionDetailWeeklyDetail_View>("EmployeeMng_Overview_function_getUserActionDetailWeeklyDetail", mergeOption, userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View> EmployeeMng_Overview_function_getUserActionDetailWeeklyOverview(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View>("EmployeeMng_Overview_function_getUserActionDetailWeeklyOverview", userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View> EmployeeMng_Overview_function_getUserActionDetailWeeklyOverview(Nullable<int> userID, string fromDateInput, string toDateInput, string moduleID, MergeOption mergeOption)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var fromDateInputParameter = fromDateInput != null ?
                new ObjectParameter("FromDateInput", fromDateInput) :
                new ObjectParameter("FromDateInput", typeof(string));
    
            var toDateInputParameter = toDateInput != null ?
                new ObjectParameter("ToDateInput", toDateInput) :
                new ObjectParameter("ToDateInput", typeof(string));
    
            var moduleIDParameter = moduleID != null ?
                new ObjectParameter("ModuleID", moduleID) :
                new ObjectParameter("ModuleID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Overview_UserActionDetailWeeklyOverview_View>("EmployeeMng_Overview_function_getUserActionDetailWeeklyOverview", mergeOption, userIDParameter, fromDateInputParameter, toDateInputParameter, moduleIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Employee_ReportView> EmployeeRpt_function_Employee(Nullable<int> jobLevelID, Nullable<int> internalDepartmentID, Nullable<int> internalCompanyID, Nullable<int> locationID, Nullable<bool> hasLeft, Nullable<int> branchID)
        {
            var jobLevelIDParameter = jobLevelID.HasValue ?
                new ObjectParameter("JobLevelID", jobLevelID) :
                new ObjectParameter("JobLevelID", typeof(int));
    
            var internalDepartmentIDParameter = internalDepartmentID.HasValue ?
                new ObjectParameter("InternalDepartmentID", internalDepartmentID) :
                new ObjectParameter("InternalDepartmentID", typeof(int));
    
            var internalCompanyIDParameter = internalCompanyID.HasValue ?
                new ObjectParameter("InternalCompanyID", internalCompanyID) :
                new ObjectParameter("InternalCompanyID", typeof(int));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("LocationID", locationID) :
                new ObjectParameter("LocationID", typeof(int));
    
            var hasLeftParameter = hasLeft.HasValue ?
                new ObjectParameter("HasLeft", hasLeft) :
                new ObjectParameter("HasLeft", typeof(bool));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("BranchID", branchID) :
                new ObjectParameter("BranchID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Employee_ReportView>("EmployeeRpt_function_Employee", jobLevelIDParameter, internalDepartmentIDParameter, internalCompanyIDParameter, locationIDParameter, hasLeftParameter, branchIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_Employee_ReportView> EmployeeRpt_function_Employee(Nullable<int> jobLevelID, Nullable<int> internalDepartmentID, Nullable<int> internalCompanyID, Nullable<int> locationID, Nullable<bool> hasLeft, Nullable<int> branchID, MergeOption mergeOption)
        {
            var jobLevelIDParameter = jobLevelID.HasValue ?
                new ObjectParameter("JobLevelID", jobLevelID) :
                new ObjectParameter("JobLevelID", typeof(int));
    
            var internalDepartmentIDParameter = internalDepartmentID.HasValue ?
                new ObjectParameter("InternalDepartmentID", internalDepartmentID) :
                new ObjectParameter("InternalDepartmentID", typeof(int));
    
            var internalCompanyIDParameter = internalCompanyID.HasValue ?
                new ObjectParameter("InternalCompanyID", internalCompanyID) :
                new ObjectParameter("InternalCompanyID", typeof(int));
    
            var locationIDParameter = locationID.HasValue ?
                new ObjectParameter("LocationID", locationID) :
                new ObjectParameter("LocationID", typeof(int));
    
            var hasLeftParameter = hasLeft.HasValue ?
                new ObjectParameter("HasLeft", hasLeft) :
                new ObjectParameter("HasLeft", typeof(bool));
    
            var branchIDParameter = branchID.HasValue ?
                new ObjectParameter("BranchID", branchID) :
                new ObjectParameter("BranchID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_Employee_ReportView>("EmployeeRpt_function_Employee", mergeOption, jobLevelIDParameter, internalDepartmentIDParameter, internalCompanyIDParameter, locationIDParameter, hasLeftParameter, branchIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_ResposibleFor_View> EmployeeMng_function_getResponsibleFor(Nullable<int> employeeID)
        {
            var employeeIDParameter = employeeID.HasValue ?
                new ObjectParameter("EmployeeID", employeeID) :
                new ObjectParameter("EmployeeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_ResposibleFor_View>("EmployeeMng_function_getResponsibleFor", employeeIDParameter);
        }
    
        public virtual ObjectResult<EmployeeMng_ResposibleFor_View> EmployeeMng_function_getResponsibleFor(Nullable<int> employeeID, MergeOption mergeOption)
        {
            var employeeIDParameter = employeeID.HasValue ?
                new ObjectParameter("EmployeeID", employeeID) :
                new ObjectParameter("EmployeeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeeMng_ResposibleFor_View>("EmployeeMng_function_getResponsibleFor", mergeOption, employeeIDParameter);
        }
    
        public virtual ObjectResult<DashboardMng_UserDataRpt_View> EmployeeMng_function_getUserData(string season)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DashboardMng_UserDataRpt_View>("EmployeeMng_function_getUserData", seasonParameter);
        }
    
        public virtual ObjectResult<DashboardMng_UserDataRpt_View> EmployeeMng_function_getUserData(string season, MergeOption mergeOption)
        {
            var seasonParameter = season != null ?
                new ObjectParameter("Season", season) :
                new ObjectParameter("Season", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DashboardMng_UserDataRpt_View>("EmployeeMng_function_getUserData", mergeOption, seasonParameter);
        }
    }
}
