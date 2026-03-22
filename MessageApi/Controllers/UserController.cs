using MessageApi.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
   readonly IUserControllerRerieverBuilder userControllerRetrieverBuilder;
   readonly IUserControllerCreatorBuilder userControllerCreatorBuilder;

   public UserController(IUserControllerRerieverBuilder userControllerRetrieverBuilder, IUserControllerCreatorBuilder userControllerCreatorBuilder)
   {
      this.userControllerRetrieverBuilder = userControllerRetrieverBuilder;
      this.userControllerCreatorBuilder = userControllerCreatorBuilder;
   }

   [HttpPost]
   [Route("NewUser")]
   public async Task<ActionResult<UserDto>> NewUser(NewUserData newuser)
   {
      try
      {
         UserDto userDto = await UserControllerLogic.CreateUser(newuser, userControllerCreatorBuilder).ConfigureAwait(false);
         return Ok(userDto);
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
         UserDto userDto = await UserControllerLogic.GetUser(userRequest, userControllerRetrieverBuilder).ConfigureAwait(false);
         return Ok(userDto);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }

   [HttpGet]
   [Route("GetUserTest")]
   public async Task<ActionResult<string>> GetUserTest()
   {
      return "Hello User Test";
   }
}