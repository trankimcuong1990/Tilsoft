using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace TilsoftService.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        Module.Framework.BLL bll = new Module.Framework.BLL();
        BLL.AccountMng accountBll = new BLL.AccountMng();

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            using (AuthRepository _repo = new AuthRepository())
            {
                //_repo.RetrieveHash();
                IdentityUser user = await _repo.Login(context.UserName, HttpContext.Current.Server.UrlDecode(context.Password));
                if (user == null)
                {
                    context.SetError("IncorrectLogin");
                    return;
                }
                else if (accountBll.IsAccountDisabled(user.Id))
                {
                    context.SetError("AccountDisabled", user.UserName);
                    return;
                }
                else if (accountBll.IsNeedToChangePassword(user.Id))
                {
                    context.SetError("ChangePassword", user.UserName);
                    return;
                }
            }

            Module.Framework.DTO.UserInfoDTO dtoUserInfo = null;
            try
            {
                dtoUserInfo = bll.GetUserInfo(context.UserName);
            }
            catch (Exception ex)
            {
                context.SetError("Error", Library.Helper.GetInnerException(ex).Message);
                return;
            }
            
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("subid", dtoUserInfo.UserID.ToString()));
            identity.AddClaim(new Claim("branchid", dtoUserInfo.UserBranchID.ToString()));
            identity.AddClaim(new Claim("companyid", dtoUserInfo.UserCompanyID.ToString()));
            identity.AddClaim(new Claim("factoryid", dtoUserInfo.UserFactoryID.ToString()));
            identity.AddClaim(new Claim("clientid", dtoUserInfo.UserCientID.HasValue ? dtoUserInfo.UserCientID.ToString() : string.Empty));
            identity.AddClaim(new Claim("role", "user"));
            context.Validated(identity);
        }
    }
}