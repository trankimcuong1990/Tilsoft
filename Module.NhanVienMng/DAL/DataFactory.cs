using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.NhanVienMng.DAL
{
    internal class DataFactory : Library.Base.DataFactory<DTO.SearchFormData, DTO.EditFormData>
    {
        private Module.Support.DAL.DataFactory supportFactory = new Module.Support.DAL.DataFactory();
        private DataConverter converter = new DataConverter();

        private NhanVienMngEntities CreateContext()
        {
            return new NhanVienMngEntities(Library.Helper.CreateEntityConnectionString("DAL.NhanVienMngModel"));
        }

        public override DTO.SearchFormData GetDataWithFilter(System.Collections.Hashtable filters, int pageSize, int pageIndex, string orderBy, string orderDirection, out int totalRows, out Library.DTO.Notification notification)
        {
            //throw new NotImplementedException();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.SearchFormData data = new DTO.SearchFormData();
            data.Data = new List<DTO.NhanVienSearchResultDTO>();
            totalRows = 0;

            string NhanVienUD = null;
            string NhanVienNM = null;
            string PhongBanNM = null;
            //int? PurposeID = null;

            if (filters.ContainsKey("NhanVienUD") && !string.IsNullOrEmpty(filters["NhanVienUD"].ToString()))
            {
                NhanVienUD = filters["NhanVienUD"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("NhanVienNM") && filters["NhanVienNM"] != null && !string.IsNullOrEmpty(filters["NhanVienNM"].ToString()))
            {
                NhanVienNM = filters["NhanVienNM"].ToString().Replace("'", "''");
            }
            if (filters.ContainsKey("PhongBanNM") && !string.IsNullOrEmpty(filters["PhongBanNM"].ToString()))
            {
                PhongBanNM = filters["PhongBanNM"].ToString().Replace("'", "''");
            }
            //if (filters.ContainsKey("PurposeID") && filters["PurposeID"] != null && !string.IsNullOrEmpty(filters["PurposeID"].ToString()))
            //{
            //    PurposeID = Convert.ToInt32(filters["PurposeID"].ToString());
            //}

            //try to get data
            try
            {
                using (NhanVienMngEntities context = CreateContext())
                {
                    var result = context.NhanVienMng_function_SearchNhanVien(NhanVienUD, NhanVienNM, PhongBanNM, orderBy, orderDirection).ToList();
                    totalRows = result.Count();
                    data.Data = converter.DB2DTO_NhanVienSearchResult(result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override DTO.EditFormData GetData(int id, out Library.DTO.Notification notification)
        {
            //throw new NotImplementedException();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.EditFormData data = new DTO.EditFormData();
            data.Data = new DTO.NhanVienDTO();
            data.Data.NguoiPhuThuocDTOs = new List<DTO.NguoiPhuThuocDTO>();
            data.PhongBanDTOs = new List<DTO.PhongBanDTO>();

            //try to get data
            try
            {
                using (NhanVienMngEntities context = CreateContext())
                {
                    if (id > 0)
                    {
                        data.Data = converter.DB2DTO_NhanVienDTO(context.NhanVienMng_NhanVien_View
                            .Include("NhanVienMng_NguoiPhuThuoc_View")
                            .FirstOrDefault(o => o.NhanVienID == id));
                    }

                    data.PhongBanDTOs = converter.DB2DTO_PhongBanDTO(context.NhanVienMng_PhongBan_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }

            return data;
        }

        public override bool DeleteData(int id, out Library.DTO.Notification notification)
        {
            //throw new NotImplementedException();

            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (NhanVienMngEntities context = CreateContext())
                {
                    NhanVien dbItem = context.NhanVien.FirstOrDefault(o => o.NhanVienID == id);
                    if (dbItem == null)
                    {
                        notification.Message = "Sample Technical Drawing not found!";
                        return false;
                    }
                    else
                    {
                        context.NhanVien.Remove(dbItem);
                        // also remove all child item if needed
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool UpdateData(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            //throw new NotImplementedException();

            DTO.NhanVienDTO dtoNhanVien = ((Newtonsoft.Json.Linq.JObject)dtoItem).ToObject<DTO.NhanVienDTO>();
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (NhanVienMngEntities context = CreateContext())
                {
                    NhanVien dbItem = null;
                    if (id == 0)
                    {
                        dbItem = new NhanVien();
                        context.NhanVien.Add(dbItem);
                    }
                    else
                    {
                        dbItem = context.NhanVien.FirstOrDefault(o => o.NhanVienID == id);
                    }

                    if (dbItem == null)
                    {
                        notification.Message = "Nhan vien not found!";
                        return false;
                    }
                    else
                    {
                        dbItem.UpdatedBy = userId;
                        dbItem.UpdatedDate = DateTime.Now;
                        converter.DTO2DB(dtoNhanVien, ref dbItem, userId);
                        context.SaveChanges();

                        // generate NHAN VIEN UD
                        if (id <= 0)
                        {
                            using (DbContextTransaction scope = context.Database.BeginTransaction())
                            {
                                context.Database.ExecuteSqlCommand("SELECT * FROM NhanVien WITH (TABLOCKX, HOLDLOCK)");

                                try
                                {
                                    dbItem.NhanVienUD = context.NhanVienMng_function_GenerateNhanVienUD().FirstOrDefault().ToString();
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                                finally
                                {
                                    scope.Commit();
                                }
                            }
                        }
                        dtoItem = GetData(dbItem.NhanVienID, out notification).Data;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
                return false;
            }
        }

        public override bool Approve(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = true;
            //            dbItem.ConfirmedBy = userId;
            //            dbItem.ConfirmedDate = DateTime.Now;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        public override bool Reset(int userId, int id, ref object dtoItem, out Library.DTO.Notification notification)
        {
            throw new NotImplementedException();

            //notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            //try
            //{
            //    using (Sample2MngEntities context = CreateContext())
            //    {
            //        SampleTechnicalDrawing dbItem = context.SampleTechnicalDrawing.FirstOrDefault(o => o.SampleTechnicalDrawingID == id);
            //        if (dbItem == null)
            //        {
            //            notification.Message = "Sample Technical Drawing not found!";
            //            return false;
            //        }
            //        else
            //        {
            //            dbItem.IsConfirmed = false;
            //            dbItem.ConfirmedBy = null;
            //            dbItem.ConfirmedDate = null;
            //            context.SaveChanges();
            //            return true;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    notification = new Library.DTO.Notification() { Message = ex.Message, Type = Library.DTO.NotificationType.Error };
            //    return false;
            //}
        }

        //
        // CUSTOM FUNCTION HERE
        //

        public DTO.SupportFormData GetInitData(out Library.DTO.Notification notification)
        {
            //throw new NotImplementedException();
            notification = new Library.DTO.Notification { Type = Library.DTO.NotificationType.Success };
            DTO.SupportFormData data = new DTO.SupportFormData();
            data.PhongBanDTOs = new List<DTO.PhongBanDTO>();
            try
            {
                using (NhanVienMngEntities context = CreateContext())
                {
                    data.PhongBanDTOs = converter.DB2DTO_PhongBanDTO(context.NhanVienMng_PhongBan_View.ToList());
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = Library.Helper.GetInnerException(ex).Message;
            }
            return data;
        }
    }
}
