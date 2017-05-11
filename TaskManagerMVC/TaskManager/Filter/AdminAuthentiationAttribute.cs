﻿namespace TaskManager.Filter
{
    using System.Web.Mvc;
    using Models;

    public class AdminAuthentiationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((AuthenticationManager.LoggedUser == null) || (AuthenticationManager.LoggedUser.IsAdmin == false))
            {
                filterContext.Result = new RedirectResult(@"\");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}