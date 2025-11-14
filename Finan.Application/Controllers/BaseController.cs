using Finan.Application.Common;
using Finan.Domain.Messages;
using Microsoft.AspNetCore.Mvc;
namespace Finan.Application.Controllers
{
    public class BaseController : ControllerBase
    {
        private IActionResult CreateResponse(object? data, MessageCollection? messages, int successStatusCode)
        {
            if (messages?.HasErrors() == true)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ApiResponse(data, messages.GetErros(), false));
            }

            if (data == null)
            {
                return new ObjectResult(new ApiResponse(data))
                {
                    StatusCode = StatusCodes.Status204NoContent
                };
            }

            return StatusCode(successStatusCode,
                    new ApiResponse(data));
        }

        protected IActionResult TreatObjectResultOk(object? data = null, MessageCollection? messages = null)
            => CreateResponse(data, messages, StatusCodes.Status200OK);

        protected IActionResult TreatObjectResultCreated(object? data = null, MessageCollection? messages = null)
            => CreateResponse(data, messages, StatusCodes.Status201Created);
    }
}
