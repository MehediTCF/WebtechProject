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
    public class TaskController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IRepository repository;
        public TaskController(IRepository repository, DataContext dataContext)
        {
            this.repository = repository;
            this.context = dataContext;
        }
        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] Model.TaskModel query)
        {
            try
            {
                Model.TaskModel task = new Model.TaskModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    TaskCode = query.TaskCode,
                    TaskName = query.TaskName,
                    Priority = query.Priority,
                    Deadline = query.Deadline,
                    ManagerId = query.ManagerId
                };
                await context.Tasks.AddAsync(task);
                await context.SaveChangesAsync();
                return Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetTaskById")]
        public async Task<IActionResult> GetTaskById([FromQuery] string id)
        {
            try
            {
                Model.TaskModel task = await this.repository.GetTaskById((string)id);
                return Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetTaskByName")]
        public async Task<IActionResult> GetTaskByName([FromQuery] string name)
        {
            try
            {
                IEnumerable<Model.TaskModel> task = await this.repository.GetTasksByName((string)name);
                return Ok(task);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetTasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                IEnumerable<Model.TaskModel> tasks = await this.repository.GetTasks().ConfigureAwait(false);
                return Ok(tasks);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] Model.TaskModel task)
        {
            try
            {
                Model.TaskModel taskData = await this.repository.GetTaskById((string)task.Id);
                if (taskData != null)
                {
                    taskData.TaskCode = task.TaskCode;
                    taskData.Priority = task.Priority;
                    taskData.TaskName = task.TaskName;
                    taskData.Deadline = task.Deadline;
                    taskData.ManagerId = task.ManagerId;


                    context.Tasks.Update(taskData);
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

        [HttpGet("DeleteTask")]
        public async Task<IActionResult> DeleteTask([FromQuery] string id)
        {
            try
            {
                Model.TaskModel taskData = await this.repository.GetTaskById((string)id);
                if (taskData != null)
                {
                    context.Tasks.Remove(taskData);
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
    }
}
