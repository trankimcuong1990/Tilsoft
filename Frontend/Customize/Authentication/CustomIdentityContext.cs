using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Frontend.Customize.Authentication
{
    public class CustomIdentityContext
    {
        public CustomIdentity FindByName(string iUserName)
        {
            return new CustomIdentity() { Id = "1", Email = "my@vn-furnindo.com", UserName = "admin", PasswordHash = "ADfBsPDBaCeESaqz83dfm2RwLQG+o0G3u2iqdhiYmGp0vUtSiuXchSjmKea1bJnrxA==" };
        }
    }
}