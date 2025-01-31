using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Core.Helpers
{
    public class ResponseBase(bool success, dynamic? data, string? message = null)
    {
        public bool Success { get; set; } = success;
        public string? Message { get; set; } = message;
        public dynamic? Data { get; set; } = data;
    }
}
