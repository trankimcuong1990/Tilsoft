using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Frontend.Customize.Authentication
{
    public class CustomIdentityStore: IUserStore<CustomIdentity>, IUserPasswordStore<CustomIdentity>
    {
        public System.Threading.Tasks.Task<CustomIdentity> FindByNameAsync(string userName)
        {
            //CustomIdentityContext context = new CustomIdentityContext();
            //CustomIdentity user = context.FindByName(userName);
            //return Task.FromResult<CustomIdentity>(user);
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(CustomIdentity user)
        {
            CustomIdentityContext context = new CustomIdentityContext();
            CustomIdentity _user = context.FindByName("admin");
            return Task.FromResult<string>(user.PasswordHash.ToString());
            //throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(CustomIdentity user, string passwordHash)
        {
            //return Task.FromResult<string>(user.PasswordHash = passwordHash);
            throw new NotImplementedException();
        }

        //
        // NOT YET IMPLEMENTED FUNCTIONS
        //

        public System.Threading.Tasks.Task CreateAsync(CustomIdentity user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task DeleteAsync(CustomIdentity user)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<CustomIdentity> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task UpdateAsync(CustomIdentity user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task<bool> HasPasswordAsync(CustomIdentity user)
        {
            throw new NotImplementedException();
        }
    }
}