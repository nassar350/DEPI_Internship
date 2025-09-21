using EmployeeAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeAPI.Services;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IEmployeeService _employeeService { get; set; }
        public EmployeeController(IEmployeeService employeeService) 
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            return _employeeService.GetEmployee();
        }

        
        [HttpGet("id")]
        public Task<ActionResult<Employee>> GetEmployeeById(int id) 
        {
            return _employeeService.GetEmployeeById(id);
        }

        

        [HttpPost]
        public void AddEmployee(Employee employee) 
        {
            _employeeService.AddEmployee(employee);
        }

        

        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            _employeeService.DeleteEmployee(id);
        }

        

        [HttpPut]
        public void UpdateEmployee(Employee employee, int id)
        {
            _employeeService.UpdateEmployee(employee, id);
        }

       
    }
}
