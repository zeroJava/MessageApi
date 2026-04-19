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
   IMessageControllerCreatorBuilder messageControllerCreatorBuilder;
   IMessageControllerRetrieverBuilder messageControllerRetrieverBuilder;

   public MessageController(IMessageControllerCreatorBuilder messageControllerCreatorBuilder, IMessageControllerRetrieverBuilder messageControllerRetrieverBuilder)
   {
      this.messageControllerCreatorBuilder = messageControllerCreatorBuilder;
      this.messageControllerRetrieverBuilder = messageControllerRetrieverBuilder;
   }

   [Authorize]
   [HttpPost]
   [Route("NewMessage")]
   public async Task<ActionResult<MessageRequestState>> NewMessage(MessageRequest request)
   {
      try
      {
         MessageRequestState state = await MessageControllerLogic.NewMessage(request, messageControllerCreatorBuilder).ConfigureAwait(false);
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
   public async Task<ActionResult<List<MessageInfo>>> GetConversation(RetrieveMessageRequest request)
   {
      try
      {
         List<MessageInfo> messages = await MessageControllerLogic.GetConversation(request, messageControllerRetrieverBuilder).ConfigureAwait(false);
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
   public async Task<ActionResult<List<MessageInfo>>> GetMessagesSentToUser(RetrieveMessageRequest request)
   {
      try
      {
         List<MessageInfo> messages = await MessageControllerLogic.GetMessagesSentToUser(request, messageControllerRetrieverBuilder).ConfigureAwait(false);
         return Ok(messages);
      }
      catch (Exception ex)
      {
         Debug.WriteLine(ex);
         return BadRequest(StatusCodes.Status500InternalServerError);
      }
   }
}