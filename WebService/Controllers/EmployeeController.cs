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
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IRepository repository;
        public EmployeeController(IRepository repository, DataContext dataContext)
        {
            this.repository = repository;
            this.context = dataContext;
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee([FromBody] Employee query)
        {
            try
            {
                Employee Employee = new Employee()
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = query.FirstName,
                    LastName = query.LastName,
                    Email = query.Email,
                    Username = query.Username,
                    Team = query.Team,
                    PhoneNumber = query.PhoneNumber,
                    Regi_Id = query.Regi_Id,
                };
                await context.Employees.AddAsync(Employee);
                await context.SaveChangesAsync();
                return Ok(Employee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById([FromQuery] string id)
        {
            try
            {
                Employee Employee = await this.repository.GetEmployeeById(id);
                return Ok(Employee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetEmployeeByName")]
        public async Task<IActionResult> GetEmployeeByName([FromQuery] string name)
        {
            try
            {
                IEnumerable<Employee> Employee = await this.repository.GetEmployeeByName(name);
                return Ok(Employee);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                IEnumerable<Employee> Employees = await this.repository.GetEmployees().ConfigureAwait(false);
                return Ok(Employees);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee Employee)
        {
            try
            {
                Employee EmployeeData = await this.repository.GetEmployeeById(Employee.Id);
                if (EmployeeData != null)
                {
                    EmployeeData.FirstName = Employee.FirstName;
                    EmployeeData.LastName = Employee.LastName;
                    EmployeeData.Email = Employee.Email;
                    EmployeeData.Username = Employee.Username;
                    EmployeeData.Regi_Id = Employee.Regi_Id;
                    EmployeeData.Team = Employee.Team;
                    EmployeeData.PhoneNumber = Employee.PhoneNumber;

                    context.Employees.Update(EmployeeData);
                    await context.SaveChangesAsync();
                    return Ok(EmployeeData);
                }
                else
                {
                    return BadRequest("Employee Not Found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] string id)
        {
            try
            {
                Employee EmployeeData = await this.repository.GetEmployeeById(id);
                if (EmployeeData != null)
                {
                    context.Employees.Remove(EmployeeData);
                    await context.SaveChangesAsync();
                    return Ok(EmployeeData);
                }
                else
                {
                    return BadRequest("Employee Not Found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
