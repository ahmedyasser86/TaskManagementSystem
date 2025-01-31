using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementSystem.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService service) : ControllerBase
    {
        private readonly ITaskService service = service;

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 15, string? search = null)
        {
            var tasks = await service.GetTasks(page, pageSize, search);
            if (tasks.Success)
            {
                return Ok(tasks);
            }
            else
            {
                return BadRequest(tasks);
            }
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await service.GetTaskById(id);
            if (task.Success)
            {
                return Ok(task);
            }
            else
            {
                return BadRequest(task);
            }
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskEntity value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await service.AddTask(value);
            if (task.Success)
            {
                return Ok(task);
            }
            else
            {
                return BadRequest(task);
            }
        }

        // PUT api/<TasksController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaskEntity value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await service.EditTask(value);
            if (task.Success)
            {
                return Ok(task);
            }
            else
            {
                return BadRequest(task);
            }
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await service.DeleteTask(id);
            if (task.Success)
            {
                return Ok(task);
            }
            else
            {
                return BadRequest(task);
            }
        }
    }
}
