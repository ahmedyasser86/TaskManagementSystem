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
        Task<ResponseBase> Insert(TaskEntity model);
        Task<ResponseBase> Update(TaskEntity model);
        Task<ResponseBase> Delete(int id);
        Task<ResponseBase> Get(int id);
        Task<ResponseBase> Get(int page = 1, int pageSize = 15, string? search = null);
    }
}
