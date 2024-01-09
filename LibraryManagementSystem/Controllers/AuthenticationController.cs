using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Authentication.Login;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace LibraryManagementSystem.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : BaseAPIController
    {
       
        [HttpPost("Login")]
        public async Task<ActionResult<LoginDTO>> Login(LoginCommand command)
        {
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               Ok(response));

        }
    }
}
