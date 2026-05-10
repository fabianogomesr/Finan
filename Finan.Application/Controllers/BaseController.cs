using Finan.Contracts.Messages;
using Finan.Contracts.Response;
using Microsoft.AspNetCore.Mvc;

namespace Finan.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private IActionResult CreateResponse(object? data, MessageCollection? messages, int successStatusCode)
        {
            if (messages?.HasErrors() == true)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ApiResponse<object>(data, messages.GetErros(), false));
            }

            if (data == null)
            {
                return new ObjectResult(new ApiResponse<object>(data))
                {
                    StatusCode = StatusCodes.Status204NoContent
                };
            }

            return StatusCode(successStatusCode,
                    new ApiResponse<object>(data));
        }

        protected IActionResult TreatObjectResultOk(object? data = null, MessageCollection? messages = null)
            => CreateResponse(data, messages, StatusCodes.Status200OK);

        protected IActionResult TreatObjectResultCreated(object? data = null, MessageCollection? messages = null)
            => CreateResponse(data, messages, StatusCodes.Status201Created);
    }
}
