using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignTaskController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IRepository repository;
        public AssignTaskController(IRepository repository, DataContext dataContext)
        {
            this.repository = repository;
            this.context = dataContext;
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] TaskAssign query)
        {
            try
            {
                TaskAssign taskReg = new TaskAssign()
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskId = query.TaskId,
                    EmployeeId = query.EmployeeId,
                };
                await context.TaskAssigns.AddAsync(taskReg);
                await context.SaveChangesAsync();
                return Ok(taskReg);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("RemoveEmployee")]
        public async Task<IActionResult> RemoveEmployee([FromBody] TaskAssign query)
        {
            try
            {
                TaskAssign taskData = await this.repository.GetTaskAssignByIdAndEmployeeId(query.TaskId, query.EmployeeId);
                if (taskData != null)
                {
                    context.TaskAssigns.Remove(taskData);
                    await context.SaveChangesAsync();
                    return Ok(taskData);
                }
                else
                {
                    return BadRequest("Task Not Found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetEmployeesByTask")]
        public async Task<IActionResult> GetEmployeesByTask([FromQuery] string taskId)
        {
            try
            {
                IEnumerable<TaskAssign> taskData = await this.repository.GetEmployeesByTask(taskId);
                return Ok(taskData);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
