using System.Net;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext context)
        {
            this.context = context;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundResponse()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(500));
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetNotFoundResponse(int id)
        {
            return BadRequest();
        }


    }
}