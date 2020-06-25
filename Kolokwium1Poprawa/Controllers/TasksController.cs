using Kolokwium1Poprawa.Exceptions;
using Kolokwium1Poprawa.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1Poprawa.Controllers
{    
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {

        private readonly IServiceDataBase _service;

        public TasksController(IServiceDataBase service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        public IActionResult getMember(int id)
        {
            try
            {
                return Ok(_service.GetMember(id));
            }
            catch (MemberNotExistException e)
            {
                return NotFound(e.Message);
            }
        }

    }
}