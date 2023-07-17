using API.Errors;
using Core.Interfaces.Service.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly IBaseAsyncDataService _dataService;
        public BuggyController(IBaseAsyncDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound));
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {
           return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(StatusCodes.Status500InternalServerError));
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
        }

        [HttpGet("badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest));
        }
    }
}