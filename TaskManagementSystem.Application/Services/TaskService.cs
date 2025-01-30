using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.Application.Services
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        private readonly ITaskRepository repository = repository;

        public async Task<TaskEntity> AddTask(TaskEntity entity)
        {
            return await repository.Insert(entity);
        }

        public async Task DeleteTask(int id)
        {
            await repository.Delete(id);
        }

        public async Task<TaskEntity> EditTask(TaskEntity entity)
        {
            return await repository.Update(entity);
        }

        public async Task<TaskEntity> GetTaskById(int id)
        {
            return await repository.Get(id);
        }

        public async Task<PaginatedList<TaskEntity>> GetTasks(int page, int pageSize, string? search)
        {
            return await repository.Get(page, pageSize, search);
        }
    }
}
