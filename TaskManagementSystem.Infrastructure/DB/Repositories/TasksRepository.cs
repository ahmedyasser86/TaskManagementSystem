using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Core.Helpers;
using TaskManagementSystem.Infrastructure.Interfaces;

namespace TaskManagementSystem.Infrastructure.DB.Repositories
{
    public class TasksRepository(ApplicationDbContext context) : ITaskRepository
    {
        private readonly ApplicationDbContext context = context;

        public async Task Delete(int id)
        {
            // Select
            var item = (await context.Tasks.SingleOrDefaultAsync(m => m.Id == id))
                ?? throw new Exception("No Entity Found with this Id");

            // Delete
            context.Tasks.Remove(item);

            // Save
            await context.SaveChangesAsync();
        }

        public async Task<TaskEntity> Get(int id)
        {
            // Select
            var item = (await context.Tasks.SingleOrDefaultAsync(m => m.Id == id))
                ?? throw new Exception("No Entity Found with this Id");

            // return
            return item;
        }

        public async Task<PaginatedList<TaskEntity>> Get(int page, int pageSize, string? search)
        {
            var res = new PaginatedList<TaskEntity>();
            IQueryable<TaskEntity> query;

            // Search
            if(search != null)
            {
                query = context.Tasks.Where(m => m.Title.Contains(search) || (!string.IsNullOrEmpty(m.Description) && m.Description.Contains(search)));
            }
            else
            {
                query = context.Tasks;
            }

            res.TotalRecords = await query.CountAsync();
            res.PageSize = pageSize;
            res.Page = page;
            res.Items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return res;
        }

        public async Task<TaskEntity> Insert(TaskEntity model)
        {
            // Insert
            context.Tasks.Add(model);

            // Save
            await context.SaveChangesAsync();

            return model;
        }

        public async Task<TaskEntity> Update(TaskEntity model)
        {
            // Check first if model exists
            var modelInDb = (await context.Tasks.AsNoTracking().SingleOrDefaultAsync(m => m.Id == model.Id))
                ?? throw new Exception("No Entity Found with this Id");

            // update
            context.Update(model);

            // save
            await context.SaveChangesAsync();

            return model;
        }
    }
}
