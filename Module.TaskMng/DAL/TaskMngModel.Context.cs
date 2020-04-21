﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Module.TaskMng.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TaskMngEntities : DbContext
    {
        public TaskMngEntities()
            : base("name=TaskMngEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskFile> TaskFile { get; set; }
        public virtual DbSet<TaskStep> TaskStep { get; set; }
        public virtual DbSet<TaskStepComment> TaskStepComment { get; set; }
        public virtual DbSet<TaskStepCommentFile> TaskStepCommentFile { get; set; }
        public virtual DbSet<TaskMng_Task_View> TaskMng_Task_View { get; set; }
        public virtual DbSet<TaskMng_TaskFile_View> TaskMng_TaskFile_View { get; set; }
        public virtual DbSet<TaskMng_TaskSearchResult_View> TaskMng_TaskSearchResult_View { get; set; }
        public virtual DbSet<TaskMng_TaskStep_View> TaskMng_TaskStep_View { get; set; }
        public virtual DbSet<TaskMng_TaskStepComment_View> TaskMng_TaskStepComment_View { get; set; }
        public virtual DbSet<TaskMng_TaskStepCommentFile_View> TaskMng_TaskStepCommentFile_View { get; set; }
        public virtual DbSet<TaskStepUser> TaskStepUser { get; set; }
        public virtual DbSet<TaskMng_TaskStepUser_View> TaskMng_TaskStepUser_View { get; set; }
        public virtual DbSet<EmailNotificationMessage> EmailNotificationMessage { get; set; }
        public virtual DbSet<AccountMng_UserProfile_View> AccountMng_UserProfile_View { get; set; }
        public virtual DbSet<NotificationMessage> NotificationMessage { get; set; }
    
        public virtual ObjectResult<TaskMng_TaskSearchResult_View> TaskMng_function_SearchTask(string taskUD, string taskNM, Nullable<int> taskStatusID, Nullable<int> taskUserID, Nullable<int> userID, string sortedBy, string sortedDirection)
        {
            var taskUDParameter = taskUD != null ?
                new ObjectParameter("TaskUD", taskUD) :
                new ObjectParameter("TaskUD", typeof(string));
    
            var taskNMParameter = taskNM != null ?
                new ObjectParameter("TaskNM", taskNM) :
                new ObjectParameter("TaskNM", typeof(string));
    
            var taskStatusIDParameter = taskStatusID.HasValue ?
                new ObjectParameter("TaskStatusID", taskStatusID) :
                new ObjectParameter("TaskStatusID", typeof(int));
    
            var taskUserIDParameter = taskUserID.HasValue ?
                new ObjectParameter("TaskUserID", taskUserID) :
                new ObjectParameter("TaskUserID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TaskMng_TaskSearchResult_View>("TaskMng_function_SearchTask", taskUDParameter, taskNMParameter, taskStatusIDParameter, taskUserIDParameter, userIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    
        public virtual ObjectResult<TaskMng_TaskSearchResult_View> TaskMng_function_SearchTask(string taskUD, string taskNM, Nullable<int> taskStatusID, Nullable<int> taskUserID, Nullable<int> userID, string sortedBy, string sortedDirection, MergeOption mergeOption)
        {
            var taskUDParameter = taskUD != null ?
                new ObjectParameter("TaskUD", taskUD) :
                new ObjectParameter("TaskUD", typeof(string));
    
            var taskNMParameter = taskNM != null ?
                new ObjectParameter("TaskNM", taskNM) :
                new ObjectParameter("TaskNM", typeof(string));
    
            var taskStatusIDParameter = taskStatusID.HasValue ?
                new ObjectParameter("TaskStatusID", taskStatusID) :
                new ObjectParameter("TaskStatusID", typeof(int));
    
            var taskUserIDParameter = taskUserID.HasValue ?
                new ObjectParameter("TaskUserID", taskUserID) :
                new ObjectParameter("TaskUserID", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var sortedByParameter = sortedBy != null ?
                new ObjectParameter("SortedBy", sortedBy) :
                new ObjectParameter("SortedBy", typeof(string));
    
            var sortedDirectionParameter = sortedDirection != null ?
                new ObjectParameter("SortedDirection", sortedDirection) :
                new ObjectParameter("SortedDirection", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<TaskMng_TaskSearchResult_View>("TaskMng_function_SearchTask", mergeOption, taskUDParameter, taskNMParameter, taskStatusIDParameter, taskUserIDParameter, userIDParameter, sortedByParameter, sortedDirectionParameter);
        }
    }
}
