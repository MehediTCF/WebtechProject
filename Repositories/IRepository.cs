using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Manager>> GetManagers();
        Task<Manager> GetManagerById(string id);
        Task<IEnumerable<Manager>> GetManagerByName(string name);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(string id);
        Task<IEnumerable<Employee>> GetEmployeeByName(string name);
        Task<IEnumerable<TaskModel>> GetTasks();
        Task<IEnumerable<TaskModel>> GetTasksByName(string name);
        Task<TaskModel> GetTaskById(string id);
        Task<TaskAssign> GetTaskAssignByIdAndEmployeeId(string taskId, string employeeId);
        Task<IEnumerable<TaskAssign>> GetEmployeesByTask(string taskId);
    }
}
