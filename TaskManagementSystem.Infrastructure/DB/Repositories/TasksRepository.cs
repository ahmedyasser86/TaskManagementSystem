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

        public async Task<ResponseBase> Delete(int id)
        {
            // Select
            var item = (await context.Tasks.SingleOrDefaultAsync(m => m.Id == id));
            if(item == null)
            {
                return new ResponseBase(false, null, "No Entity Found with this Id");
            }

            try
            {
                // Delete
                context.Tasks.Remove(item);

                // Save
                await context.SaveChangesAsync();

                return new ResponseBase(true, item);
            }
            catch (Exception ex)
            {
                return new ResponseBase(false, item, "Unknown database error accured");
            }
        }

        public async Task<ResponseBase> Get(int id)
        {
            // Select
            var item = (await context.Tasks.SingleOrDefaultAsync(m => m.Id == id));
            if (item == null)
            {
                return new ResponseBase(false, null, "No Entity Found with this Id");
            }

            // return
            return new ResponseBase(true, item);
        }

        public async Task<ResponseBase> Get(int page, int pageSize, string? search)
        {
            try
            {
                var res = new PaginatedList<TaskEntity>();
                IQueryable<TaskEntity> query;

                // Search
                if (search != null)
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

                return new ResponseBase(true, res);
            }
            catch
            {
                return new ResponseBase(false, null, "Error happend while fetching data from database");
            }
        }

        public async Task<ResponseBase> Insert(TaskEntity model)
        {
            try
            {
                // Insert
                context.Tasks.Add(model);

                // Save
                await context.SaveChangesAsync();

                return new ResponseBase(true, model);
            }
            catch
            {
                return new ResponseBase(false, model, "Error happend while saving the entity in database");
            }
        }

        public async Task<ResponseBase> Update(TaskEntity model)
        {
            try
            {
                // Check first if model exists
                var modelInDb = (await context.Tasks.AsNoTracking().SingleOrDefaultAsync(m => m.Id == model.Id));
                if(modelInDb == null)
                    return new ResponseBase(false, model, "No Entity Found with this Id");

                // update
                context.Update(model);

                // save
                await context.SaveChangesAsync();

                return new ResponseBase(true, model);
            }
            catch (Exception)
            {
                return new ResponseBase(false, model, "Error happend while updating the entity in database");
            }
        }
    }
}
