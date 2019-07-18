using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ARDC.NetCore.Playground.API.Features.Auth
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("signin")]
        public IActionResult SignIn(string returnUrl = "/")
        {
            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [HttpGet("signout")]
        public async Task<IActionResult> SignOut(string logoutUrl = "/")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect(logoutUrl);
        }
    }
}
