using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DAL.ProfileMng
{
    public class DataFactory
    {
        private string _TempFolder;
        private Module.Framework.DAL.DataFactory fwFactory = new Module.Framework.DAL.DataFactory();

        public DataFactory(string tempFolder)
        {
            _TempFolder = tempFolder + @"\";
        }

        private DataConverter converter = new DataConverter();
        private ProfileMngEntities CreateContext()
        {
            return new ProfileMngEntities(DALBase.Helper.CreateEntityConnectionString("ProfileMngModel"));
        }

        public DTO.ProfileMng.User GetUserProfile(int userId, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            DTO.ProfileMng.User dtoItem = new DTO.ProfileMng.User();
            try
            {
                using (ProfileMngEntities context = CreateContext())
                {
                    dtoItem = converter.DB2DTO(context.ProfileMng_User_View.FirstOrDefault(o => o.UserId == userId));
                }
            }
            catch (Exception ex)
            {
                notification.Type = Library.DTO.NotificationType.Error;
                notification.Message = ex.Message;
            }
            return dtoItem;
        }

        public bool UpdateUserProfile(int userId, ref DTO.ProfileMng.User dtoItem, out Library.DTO.Notification notification)
        {
            notification = new Library.DTO.Notification() { Type = Library.DTO.NotificationType.Success };
            try
            {
                using (ProfileMngEntities context = CreateContext())
                {
                    UserProfile dbItem = context.UserProfile.FirstOrDefault(o => o.UserId == userId);
                    if (dbItem == null)
                    {
                        notification.Message = "User not found!";
                        return false;
                    }
                    else
                    {
                        // check concurrency
                        if (dbItem.ConcurrencyFlag != null && !dbItem.ConcurrencyFlag.SequenceEqual(Convert.FromBase64String(dtoItem.ConcurrencyFlag_String)))
                        {
                            throw new Exception(DALBase.Helper.TEXT_CONCURRENCY_CONFLICT);
                        }

                        converter.DTO2DB(dtoItem, ref dbItem);
                        context.SaveChanges();

                        // processing image
                        if (dtoItem.PersonalPhoto_HasChange)
                        {
                            dbItem.PersonalPhoto = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.PersonalPhoto_NewFile, dtoItem.PersonalPhoto);
                        }
                        if (dtoItem.SignatureImage_HasChange)
                        {
                            dbItem.SignatureImage = fwFactory.CreateFilePointer(this._TempFolder, dtoItem.SignatureImage_NewFile, dtoItem.SignatureImage);
                        }
                        context.SaveChanges();

                        dtoItem = GetUserProfile(userId, out notification);

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
    }
}
