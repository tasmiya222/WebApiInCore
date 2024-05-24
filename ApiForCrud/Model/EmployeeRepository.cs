using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ApiForCrud.Model
{
    public class EmployeeRepository
    {
        private readonly AppDBContext _appDBContext;

        public EmployeeRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddEmployeeAysnc(Employee employee)
        {
            await _appDBContext.Set<Employee>().AddAsync(employee);
            await _appDBContext.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployeeAsync()
         {
            return await _appDBContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _appDBContext.Employees.FindAsync(id);
        }

        public async Task UpdateEmployeeAsync(int id , Employee emp)
        {
            var employee = await _appDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee Not Found");
            }
            employee.Name = emp.Name;
            employee.Email = emp.Email;
            employee.Age = emp.Age;
            employee.Phone = emp.Phone;
            employee.Salary = emp.Salary;
            await _appDBContext.SaveChangesAsync();


        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _appDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                throw new Exception("Employee Not Found");
            }
            _appDBContext.Employees.Remove(employee);
            await _appDBContext.SaveChangesAsync();
        }
    }
}
