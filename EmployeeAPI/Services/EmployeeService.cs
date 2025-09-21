using EmployeeAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly EmployeeAppContext _context;
        public EmployeeService(EmployeeAppContext context) 
        {
            _context = context;
        }
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return employee;
        }

        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }

        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            await _context.SaveChangesAsync();
            return _context.Employees.ToArray();
        }

        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            Employee employee = _context.Employees.Find(id);
            return employee;
        }

        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee, int id)
        {
            Employee emp = _context.Employees.Find(id);
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            _context.Employees.Update(emp);
            _context.SaveChanges();
            return emp;
        }
    }
}
