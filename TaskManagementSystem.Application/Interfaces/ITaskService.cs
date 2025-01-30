using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface ITaskService
    {
        public Task<PaginatedList<TaskEntity>> GetTasks(int page, int pageSize, string? search);
        public Task<TaskEntity> GetTaskById(int id);
        public Task<TaskEntity> AddTask(TaskEntity entity);
        public Task<TaskEntity> EditTask(TaskEntity entity);
        public Task DeleteTask(int id);

    }
}
