using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eproject.Helpers
{
    public class AuthorizeUser : ActionFilterAttribute
    {
        private readonly string _role;

        public AuthorizeUser(string role = null)
        {
            this._role = role;  
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var user = context.HttpContext.Session.GetString("Username");
            var role = context.HttpContext.Session.GetString("Userrole");

            if(user == null)
            {
                context.Result = new RedirectToActionResult("Login", "User", null);
                return;
            }

            if( _role != null && role != _role)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }

           
        }


    }
}
