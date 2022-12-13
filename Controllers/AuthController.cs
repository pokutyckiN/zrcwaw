using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using zrcwaw_l2.Auth;

namespace zrcwaw_l2.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        [Route("current")]
        public IActionResult GetCurrentUser() => Json(Authorization.CurrentUser);

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            bool result = await Authorization.Login(user);
            return result ? Ok() : NotFound();
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            Authorization.Logout();
            return SignOut();
        }
    }
}
