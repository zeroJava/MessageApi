using MessageApi.Application;
using MessageApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessageApi.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
   readonly IAuthenticationControllerBuilder authenticationControllerBuilder;

   public AuthenticationController(AuthenticationControllerBuilder authenticationControllerBuilder)
   {
      this.authenticationControllerBuilder = authenticationControllerBuilder;
   }

   [HttpPost]
   [Route("AuthenticateUser")]
   public async Task<ActionResult<UserDto>> AuthenticateUser(AuthenticationRequest request)
   {
      try
      {
         SimpleJwtTokenGenerator tokenGenerator = new();
         AuthToken authToken = await AuthenticationControllerLogic.AuthenticateUser(request, authenticationControllerBuilder).ConfigureAwait(false);
         return Ok(authToken);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }
}