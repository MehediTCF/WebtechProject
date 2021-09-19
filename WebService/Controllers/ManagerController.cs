using Microsoft.AspNetCore.Mvc;
using Model;
using Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IRepository repository;
        public ManagerController(IRepository repository, DataContext dataContext)
        {
            this.repository = repository;
            this.context = dataContext;
        }

        [HttpPost("AddManager")]
        public async Task<IActionResult> AddManager([FromBody] Manager query)
        {
            try
            {
                Manager manager = new Manager()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = query.FirstName,
                    LastName = query.LastName,
                    Email = query.Email,
                    Username = query.Username,
                    Team = query.Team,
                    PhoneNumber = query.PhoneNumber,
                };
                await context.Managers.AddAsync(manager);
                await context.SaveChangesAsync();
                return Ok(manager);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
           
        }

        [HttpGet("GetManagerById")]
        public async Task<IActionResult> GetManagerById([FromQuery] string id)
        {
            try
            {
                Manager manager = await this.repository.GetManagerById(id);
                return Ok(manager);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("GetManagerByName")]
        public async Task<IActionResult> GetManagerByName([FromQuery] string name)
        {
            try
            {
                IEnumerable<Manager> manager = await this.repository.GetManagerByName(name);
                return Ok(manager);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetManagers")]
        public async Task<IActionResult> GetManagers()
        {
            try
            {
                IEnumerable<Manager> managers = await this.repository.GetManagers().ConfigureAwait(false);
                return Ok(managers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost("UpdateManager")]
        public async Task<IActionResult> UpdateManager([FromBody] Manager manager)
        {
            try
            {
                Manager managerData = await this.repository.GetManagerById(manager.Id);
                if(managerData != null)
                {
                    managerData.FirstName = manager.FirstName;
                    managerData.LastName = manager.LastName;
                    managerData.Email = manager.Email;
                    managerData.Username = manager.Username;
                    managerData.PhoneNumber = manager.PhoneNumber;
                    managerData.Team = manager.Team;

                    context.Managers.Update(managerData);
                    await context.SaveChangesAsync();
                    return Ok(managerData);
                }
                else
                {
                    return BadRequest("Manager Not Found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("DeleteManager")]
        public async Task<IActionResult> DeleteManager([FromQuery] string id)
        {
            try
            {
                Manager managerData = await this.repository.GetManagerById(id);
                if (managerData != null)
                {
                    context.Managers.Remove(managerData);
                    await context.SaveChangesAsync();
                    return Ok(managerData);
                }
                else
                {
                    return BadRequest("Manager Not Found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
