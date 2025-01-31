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
        public Task<ResponseBase> GetTasks(int page, int pageSize, string? search);
        public Task<ResponseBase> GetTaskById(int id);
        public Task<ResponseBase> AddTask(TaskEntity entity);
        public Task<ResponseBase> EditTask(TaskEntity entity);
        public Task<ResponseBase> DeleteTask(int id);

    }
}
