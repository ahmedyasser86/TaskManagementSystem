using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Helpers;

namespace TaskManagementSystem.Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseBase> RegisterAsync(string email, string password);
        Task<ResponseBase> LoginAsync(string email, string password);
    }
}
