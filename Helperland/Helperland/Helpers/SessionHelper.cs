using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Helpers
{
    public class SessionHelper : ActionFilterAttribute
    {
        private readonly int UserTypeID;
        public SessionHelper(int UserTypeID)
        {
            this.UserTypeID = UserTypeID;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userType = context.HttpContext.Session.GetInt32("usertype");
            if(userType == null)
            {
                context.Result = new RedirectToActionResult("Index", "Home",new { isLoginOpen=true });
                return;
            }
            else
            {
                if(userType != UserTypeID)
                {
                    context.Result = new RedirectToActionResult("Index", "Home", true);
                    return;
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
