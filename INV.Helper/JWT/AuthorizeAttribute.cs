using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using INV.Dto;

namespace INV.Helper.JWT
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (CurrentUser)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new TResponse() { ResponseMessage = ResponseMessage.AuthenticationFail, ResponseCode = StatusCodes.Status401Unauthorized, ResponseStatus = false }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}