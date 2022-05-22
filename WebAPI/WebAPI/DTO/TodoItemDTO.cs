using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTO
{
    public class TodoItemDTO
    {
        public long Id { get; set; }
        public string TodoName { get; set; }
        public int ProgressPercentage { get; set; }

    }
}
