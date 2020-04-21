using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using DTO.AccountMng;

namespace TilsoftService
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _context;
        private UserManager<IdentityUser> _userMng;

        public AuthRepository()
        {
            _context = new AuthContext();
            _userMng = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_context));
        }

        public async Task<IdentityUser> Login(string userName, string password)
        {
            password = password.Replace("[plus]", "+");
            IdentityUser user = await _userMng.FindAsync(userName, password);
            return user;
        }

        public IdentityUser FindUser(string userName, string password)
        {
            IdentityUser user = _userMng.Find(userName, password);
            return user;
        }

        public async Task<IdentityUser> FindByUserName(string userName)
        {
            IdentityUser user = await _userMng.FindByNameAsync(userName);
            return user;
        }

        public async Task<IdentityUser> FindByIdentifierAsync(string identifier)
        {
            IdentityUser user = await _userMng.FindByIdAsync(identifier);
            return user;
        }

        public IdentityUser FinByIdentifier(string identifier)
        {
            IdentityUser user = _userMng.FindById(identifier);
            return user;
        }

        public IdentityUser FindByUserNameNormal(string userName)
        {
            IdentityUser user = _userMng.FindByName(userName);
            return user;
        }

        public bool ResetPassword(string userName, string newPassword)
        {
            String hashedNewPassword = _userMng.PasswordHasher.HashPassword(newPassword);
            var hashResult = _userMng.PasswordHasher.VerifyHashedPassword(hashedNewPassword, newPassword);
            IdentityUser cUser = _userMng.FindByName(userName);
            UserStore<IdentityUser> store = new UserStore<IdentityUser>();
            cUser.PasswordHash = hashedNewPassword;
            _userMng.Update(cUser);

            // password change compleled update last password change
            BLL.AccountMng accBll = new BLL.AccountMng();
            accBll.UpdateLastPasswordChange(cUser.Id);

            return true;
        }

        public bool ChangePassword(string userName, string newPassword, string oldPassword, out string errMsg)
        {
            errMsg = string.Empty;
            try
            {
                if (newPassword == oldPassword)
                {
                    throw new Exception("New password can not be the same as old password!");
                }

                IdentityUser cUser = _userMng.FindByName(userName);
                IdentityResult result = _userMng.ChangePassword(cUser.Id, oldPassword, newPassword);
                if (result.Errors.Count() > 0)
                {                    
                    foreach (string msg in result.Errors)
                    {
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            errMsg += Environment.NewLine + msg;
                        }
                        else
                        {
                            errMsg += msg;
                        }
                    }
                    throw new Exception(errMsg);
                }

                // password change compleled update last password change
                BLL.AccountMng accBll = new BLL.AccountMng();
                accBll.UpdateLastPasswordChange(cUser.Id);
            }
            catch
            {
                return false;
            }            

            return true;
        }

        public string CreateUser(string userName, string password, string email)
        {
            IdentityUser nUser = new IdentityUser();
            nUser.Email = email;
            nUser.UserName = userName;
            IdentityResult result = _userMng.Create(nUser, password);

            if (result.Errors.Count() > 0)
            {
                string detailMessage = string.Empty;
                foreach (string msg in result.Errors)
                {
                    if (!string.IsNullOrEmpty(detailMessage))
                    {
                        detailMessage += Environment.NewLine + msg;
                    }
                    else
                    {
                        detailMessage += msg;
                    }
                }
                throw new Exception(detailMessage);
            }

            return nUser.Id;
        }

        public bool UpdateUser(string userName, string email, string phone)
        {
            IdentityUser cUser = _userMng.FindByName(userName);
            if (cUser == null)
            {
                throw new Exception("User: " + userName + " not found");
            }
            cUser.Email = email;
            cUser.PhoneNumber = phone;
            _userMng.Update(cUser);

            return true;
        }

        public bool CheckUsernameOrEmailExists(string userName, string email)
        {
            if (_userMng.FindByEmail(userName) != null)
            {
                return true;
            }
            if (_userMng.FindByEmail(email) != null)
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            _context.Dispose();
            _userMng.Dispose();
        }
    }
}