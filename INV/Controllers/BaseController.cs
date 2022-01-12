using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using INV.Dto;

namespace INV.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected CurrentUser CurrentUser => (CurrentUser)HttpContext.Items["User"];

        #region  Methods

        protected string IpAddress()
        {
            return Request.Headers.ContainsKey("X-Forwarded-For")
                ? (string)Request.Headers["X-Forwarded-For"]
                : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        #endregion
    }
}
