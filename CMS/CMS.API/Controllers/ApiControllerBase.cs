using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMS.API.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected string CurrentUserName
        {
            get
            {
                ClaimsPrincipal identity = Thread.CurrentPrincipal as ClaimsPrincipal ?? HttpContext.User;
                Claim claim = identity?.Claims?.FirstOrDefault(item => item.Type == ClaimTypes.NameIdentifier);

                if(claim != null)
                {
                    return claim.Value;
                }
                if (!string.IsNullOrEmpty(Thread.CurrentPrincipal?.Identity?.Name))
                {
                    return Thread.CurrentPrincipal?.Identity?.Name;
                }

                return User.Identity.Name;
            }
        }
    }
}
