using MvcProjectArch.UI.AuthHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MvcProjectArch.UI.CustomViewPage
{
    public abstract class AppViewPage<TModel> : WebViewPage<TModel>
    {
        protected AppUser CurrentUser
        {
            get
            {
                return new AppUser(this.User as ClaimsPrincipal);
            }
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class AppViewPage : AppViewPage<dynamic>
    {
    }
}