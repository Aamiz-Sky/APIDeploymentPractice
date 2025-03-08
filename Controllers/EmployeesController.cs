using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // In-memory list to act as our database
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Aamiz Ali", Position = "Backend Developer" },
            new Employee { Id = 2, Name = "Owais Ali", Position = "Frontened Developer" }
        };

        // GET: api/employees
        [HttpGet("GetAllEmployees")]
        public ActionResult<IEnumerable<Employee>> GetEmployees()
        {
            return _employees;
        }

        // GET: api/employees/{id}
        [HttpGet("GetEmployeeById/{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        // POST: api/employees
        [HttpPost("AddEmployee")]
        public ActionResult<Employee> PostEmployee(Employee employee)
        {
            employee.Id = _employees.Count + 1;
            _employees.Add(employee);
            return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, employee);
        }

        // PUT: api/employees/{id}
        [HttpPut("EditEmployeeById/{id}")]
        public IActionResult PutEmployee(int id, Employee updatedEmployee)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updatedEmployee.Name;
            employee.Position = updatedEmployee.Position;

            return NoContent();
        }

        // DELETE: api/employees/{id}
        [HttpDelete("DeleteEmployeeById/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            _employees.Remove(employee);
            return NoContent();
        }
    }

    // Basic Employee model
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
    }
}
