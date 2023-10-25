using DocVisionMessageApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocVisionMessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessagesController(IMessageService messageService) 
        { 
            _messageService = messageService;
        }

        [HttpPost("registrateMessage")]
        public async Task<IActionResult> RegistrateMessage([FromBody] Message message)
            => await _messageService.RegisterMessage(message)
            ? Ok() : Problem(detail:"Ошибка сервера", statusCode: StatusCodes.Status500InternalServerError);
    }
}
