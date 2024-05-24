 using ApiForCrud.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForCrud.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(EmployeeRepository employeeRepository) {
            _employeeRepository = employeeRepository;
        
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.AddEmployeeAysnc(employee);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> GetAllEmployeeList()
        {
            var employeeList = await  _employeeRepository.GetAllEmployeeAsync();
            return Ok(employeeList);
        }
        [HttpGet("{id}")]
       public async Task<ActionResult> GetEmployeeById([FromRoute] int id)
        {
            var EmployeeById = await _employeeRepository.GetEmployeeByIdAsync(id);
            return Ok(EmployeeById);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateEmployee([FromRoute] int id, [FromBody] Employee model)
        {
            await _employeeRepository.UpdateEmployeeAsync(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee([FromRoute] int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
            return Ok(); 
        }
    }
}
