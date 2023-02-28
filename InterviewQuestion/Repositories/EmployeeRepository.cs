using DataAccessLayer;
using InterviewQuestion.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewQuestion.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var deleteresult = await _context.Employee.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (deleteresult != null)
            {
                _context.Employee.Remove(deleteresult);
                await _context.SaveChangesAsync();
                return deleteresult;
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            var getemployee = await _context.Employee.Where(x => x.Id == id).FirstOrDefaultAsync();
            return getemployee;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employee.ToListAsync();
        }


        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employee.Where(x => x.Id == employee.Id).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;

                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }



        public async Task<IEnumerable<Employee>> SearchEmployee(string name)
        {
            IQueryable<Employee> query = _context.Employee;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

      
    }
}
