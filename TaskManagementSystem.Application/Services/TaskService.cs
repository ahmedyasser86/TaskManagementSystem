using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.Application.Services
{
    public class TaskService(ITaskRepository repository) : ITaskService
    {
        private readonly ITaskRepository repository = repository;

        public async Task<ResponseBase> AddTask(TaskEntity entity)
        {
            return await repository.Insert(entity);
        }

        public async Task<ResponseBase> DeleteTask(int id)
        {
            return await repository.Delete(id);
        }

        public async Task<ResponseBase> EditTask(TaskEntity entity)
        {
            return await repository.Update(entity);
        }

        public async Task<ResponseBase> GetTaskById(int id)
        {
            return await repository.Get(id);
        }

        public async Task<ResponseBase> GetTasks(int page, int pageSize, string? search)
        {
            return await repository.Get(page, pageSize, search);
        }
    }
}
