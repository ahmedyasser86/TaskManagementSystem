using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(ITaskService service) : ControllerBase
    {
        private readonly ITaskService service = service;

        // GET: api/<TasksController>
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 15, string? search = null)
        {
            try
            {
                return Ok(new APIResponse<PaginatedList<TaskEntity>>(true, "Data fetched done", await service.GetTasks(page, pageSize, search)));
            }
            catch(Exception ex)
            {
                return BadRequest(new APIResponse<TaskEntity>(false, ex.Message, null));
            }
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(new APIResponse<TaskEntity>(true, "Data fetched done", await service.GetTaskById(id)));
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse<TaskEntity>(true, ex.Message, null));
            }
        }

        // POST api/<TasksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskEntity value)
        {
            if (!ModelState.IsValid)
                return BadRequest(new APIResponse<TaskEntity>(false, "Model is not valid", null));

            try
            {
                var model = await service.AddTask(value);
                return Ok(new APIResponse<TaskEntity>(true, "Data added done", model));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<TaskEntity>(false, ex.Message, null));
            }
        }

        // PUT api/<TasksController>/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TaskEntity value)
        {
            if (!ModelState.IsValid)
                return BadRequest(new APIResponse<TaskEntity>(false, "Model is not valid", null));

            try
            {
                var model = await service.EditTask(value);
                return Ok(new APIResponse<TaskEntity>(true, "Data Updated done", model));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<TaskEntity>(false, ex.Message, null));
            }
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await service.DeleteTask(id);
                return Ok(new APIResponse<TaskEntity>(true, "Data Deleted done", null));
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse<TaskEntity>(false, ex.Message, null));
            }
        }
    }
}
