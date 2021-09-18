using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class TaskModel
    {
        public string Id { get; set; }
        public string TaskCode { get; set; }
        public string TaskName { get; set; }
        public string Priority { get; set; }
        public string Deadline { get; set; }
        public string ManagerId { get; set; }
    }
}
