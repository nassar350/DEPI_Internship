using EmployeeAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Services
{
    public interface IEmployeeService
    {

        public Task<ActionResult<IEnumerable<Employee>>> GetEmployee();

        public Task<ActionResult<Employee>> GetEmployeeById(int id);

        public Task<ActionResult<Employee>> AddEmployee(Employee employee);

        public Task<ActionResult<bool>> DeleteEmployee(int id);

        public Task<ActionResult<Employee>> UpdateEmployee(Employee employee, int id);
    }
}
