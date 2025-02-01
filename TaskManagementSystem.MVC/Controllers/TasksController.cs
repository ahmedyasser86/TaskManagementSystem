using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Enums;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.MVC.Controllers
{
    [Authorize]
    public class TasksController(ITaskRepository repository) : Controller
    {
        private readonly ITaskRepository repository = repository;

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? search = null, string? error = null)
        {
            // Get data
            var response = await repository.Get(page, pageSize, search);

            // Case Redirect with error
            if(!string.IsNullOrEmpty(error))
            {
                response.Success = false;
                response.Message = error;
            }

            return View(response);
        }

        public IActionResult Add()
        {
            return View("EntityView");
        }

        public async Task<IActionResult> Edit(int Id)
        {
            // get data
            var response = await repository.Get(Id);

            if(response.Success)
                return View("EntityView", response);

            else
            {
                return RedirectToAction("Index", new { error = response.Message });
            }
        }

        public async Task<IActionResult> Delete(int Id)
        {
            // get data
            var response = await repository.Delete(Id);

            if(response.Success)
                return RedirectToAction("Index");
            else
            {
                return RedirectToAction("Index", new { error = response.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Savechanges(TaskEntity task)
        {
            if(task.Id == 0)
            {
                // add
                var res = await repository.Insert(task);
                if (res.Success)
                    res.Message = "Task added successfuly";

                return View("EntityView", res);
            }
            else
            {
                // Edit
                var res = await repository.Update(task);
                if (res.Success)
                    res.Message = "Task updated successfuly";

                return View("EntityView", res);
            }
        }
    }
}
