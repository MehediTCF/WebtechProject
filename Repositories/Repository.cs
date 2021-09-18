using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Manager>> GetManagers()
        {
            var users = await _context.Managers.ToListAsync();
            return users;
        }

        public async Task<Manager> GetManagerById(string id)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(u => u.Id == id);
            return manager;
        }

        public async Task<IEnumerable<Manager>> GetManagerByName(string name)
        {
            var managers = await _context.Managers.ToListAsync();
            managers = managers.FindAll(e => e.FullName.ToLower().Contains(name.ToLower()));
            return managers;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees;
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(u => u.Id == id);
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByName(string name)
        {
            var employee_name = await _context.Employees.ToListAsync();
            employee_name = employee_name.FindAll(e => e.FullName.ToLower().Contains(name.ToLower()));
            return employee_name;
        }

        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks;
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByName(string name)
        {
            var taskbyname = await _context.Tasks.ToListAsync();
            taskbyname = taskbyname.FindAll(e => e.TaskName.ToLower().Contains(name.ToLower()));
            return taskbyname;
        }

        public async Task<TaskModel> GetTaskById(string id)
        {
            var taskbyid = await _context.Tasks.FirstOrDefaultAsync(u => u.Id == id);
            return taskbyid;
        }

        public async Task<TaskAssign> GetTaskAssignByIdAndEmployeeId(string taskId, string employeeId)
        {
            return await _context.TaskAssigns.FirstOrDefaultAsync(u => u.TaskId == taskId && u.EmployeeId == employeeId);
        }

        public async Task<IEnumerable<TaskAssign>> GetEmployeesByTask(string taskId)
        {
            var taskAssign = await _context.TaskAssigns.ToListAsync();
            taskAssign = taskAssign.FindAll(e => e.TaskId == taskId);
            return taskAssign;
        }
    }
}
