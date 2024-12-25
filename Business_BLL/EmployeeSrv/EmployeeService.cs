using Business_BLL.DepartmentSrv;
using Data_DAL.Entities;
using Microsoft.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;


namespace Business_BLL.EmployeeSrv
{
    public class EmployeeService : IEmployee
    {
        private readonly ConnectDB _context;

        public EmployeeService(ConnectDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var connectDB = _context.Employees.Include(e => e.Department).Include(e => e.Role);
            var emp = await connectDB.ToListAsync();
            return emp;
        }
        public async Task<IEnumerable<Employee>> GetAllpage(int page =1)
        {
            int limit = 10;
            var connectDB = _context.Employees.Include(e => e.Department).Include(e => e.Role);
            var emp = await connectDB.OrderByDescending(b => b.Code)
                .ToPagedListAsync(page, limit); 
            return emp;
        }
       

        public async Task<Employee> GetById(int id)
        {
            var emp = await _context.Employees.FindAsync(id);

            if (emp == null)
            {
                throw new KeyNotFoundException("Employee not found.");
            }

            return emp;
        }


        public async Task Add(Employee employee)
        {

            employee.Password = Utilitie.GetMD5HashData(employee.Password);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();


        }


        public async Task Update(Employee employee)
        {
            var emp = await _context.Employees.FindAsync(employee.Code);
            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.Email = employee.Email;
                emp.Phone = employee.Phone;
                emp.Address = employee.Address;
                emp.Achievements = employee.Achievements;
                emp.RoleId = employee.RoleId;
                emp.Avata = employee.Avata;
                emp.Grade = employee.Grade;
                emp.Education = employee.Education;
                emp.DepartmentId = employee.DepartmentId;
                emp.Username = employee.Username;
                if (!string.IsNullOrEmpty(employee.Password))
                {
                    emp.Password = Utilitie.GetMD5HashData(employee.Password);
                }

                await _context.SaveChangesAsync();

            
               
            }
           
        }

        public async Task Delete(int id)
        {
            if (id == null)
            {
                throw new KeyNotFoundException("Employee id not found.");
            }
            var emp = await _context.Employees.SingleOrDefaultAsync(x => x.Code == id);
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            
        }

     
    }
}
