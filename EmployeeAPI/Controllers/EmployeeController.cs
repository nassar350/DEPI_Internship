using EmployeeAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly EmployeeAppContext _context;

        public EmployeeController(EmployeeAppContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            await _context.SaveChangesAsync();
            return Ok(_context.Employees.ToArray());
        }

        [HttpGet("id")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            Employee employee = _context.Employees.Find(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            Employee employee = _context.Employees.Find(id);
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee employee, int id)
        {
            Employee emp = _context.Employees.Find(id);
            emp.Name = employee.Name;
            emp.Email = employee.Email;
            _context.Employees.Update(emp);
            _context.SaveChanges();
            return Ok(emp);
        }
    }
}
