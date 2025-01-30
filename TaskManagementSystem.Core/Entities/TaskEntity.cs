using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Core.Enums;

namespace TaskManagementSystem.Core.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public required string Title { get; set; }

        public string? Description { get; set; }

        public Status Status { get; set; } = Status.ToDo;

        public DateTime DuoDate { get; set; }
    }
}
