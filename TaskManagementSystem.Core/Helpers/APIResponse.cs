using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Helpers
{
    public class APIResponse<T>(bool status, string? message, T? data)
    {
        public bool Status { get; set; } = status;
        public string? Message { get; set; } = message;
        public T? Data { get; set; } = data;
    }
}
