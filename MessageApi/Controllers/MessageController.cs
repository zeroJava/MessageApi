using MessageApi.Application;
using MessageApi.ControllersBL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MessageApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
   IMessageControllerBuilder messageControllerBuilder;

   public MessageController(IMessageControllerBuilder messageControllerBuilder)
   {
      this.messageControllerBuilder = messageControllerBuilder;
   }

   [Authorize]
   [HttpPost]
   [Route("NewMessage")]
   public async Task<ActionResult<MessageRequestState>> NewMessage(MessageRequest request)
   {
      try
      {
         MessageRequestState state = await MessageControllerLogic.NewMessage(request, messageControllerBuilder).ConfigureAwait(false);
         return Ok(state);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }

   [Authorize]
   [HttpPost]
   [Route("Conversation")]
   public async Task<ActionResult<IEnumerable<MessageInfo>>> GetConversation(RetrieveMessageRequest request)
   {
      try
      {
         IEnumerable<MessageInfo> messages = await MessageControllerLogic.GetConversation(request, messageControllerBuilder).ConfigureAwait(false);
         return Ok(messages);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }

   [Authorize]
   [HttpPost]
   [Route("MessagesSentToUser")]
   public async Task<ActionResult<IEnumerable<MessageInfo>>> GetMessagesSentToUser(RetrieveMessageRequest request)
   {
      try
      {
         IEnumerable<MessageInfo> messages = await MessageControllerLogic.GetMessagesSentToUser(request, messageControllerBuilder).ConfigureAwait(false);
         return Ok(messages);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }
}