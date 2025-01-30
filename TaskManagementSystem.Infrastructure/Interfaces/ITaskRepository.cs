using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;

namespace TaskManagementSystem.Infrastructure.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> Insert(TaskEntity model);
        Task<TaskEntity> Update(TaskEntity model);
        Task Delete(int id);
        Task<TaskEntity> Get(int id);
        Task<PaginatedList<TaskEntity>> Get(int page = 1, int pageSize = 15, string? search = null);
    }
}
