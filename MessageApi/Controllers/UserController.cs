using MessageApi.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
   readonly IUserControllerBuilder userControllerBuilder;

   public UserController(IUserControllerBuilder userControllerCreatorBuilder)
   {
      this.userControllerBuilder = userControllerCreatorBuilder;
   }

   [HttpPost]
   [Route("NewUser")]
   public async Task<ActionResult<NewUserResponse>> NewUser(NewUserData newuser)
   {
      try
      {
         NewUserResponse response = await UserControllerLogic.CreateUser(newuser, userControllerBuilder).ConfigureAwait(false);
         return Ok(response);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }

   [Authorize]
   [HttpPost]
   [Route("GetUser")]
   public async Task<ActionResult<UserDto?>> GetUser(UserRequest userRequest)
   {
      try
      {
         UserDto userDto = await UserControllerLogic.GetUser(userRequest, userControllerBuilder).ConfigureAwait(false);
         return Ok(userDto);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }

   [Authorize]
   [HttpGet]
   [Route("GetUserTest")]
   public async Task<ActionResult<string>> GetUserTest()
   {
      return "Hello User Test";
   }
}