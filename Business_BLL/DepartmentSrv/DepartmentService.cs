
using Data_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Business_BLL.DepartmentSrv
{
    public class DepartmentService : IDepartment
    {
        private readonly ConnectDB _context;

        public DepartmentService(ConnectDB context)
        {
            _context = context;
        }

        //list
        public async Task<IEnumerable<Department>> GetAll()
        {
            
            return  await _context.Departments.ToListAsync();
 
     
        }

        public async Task<IEnumerable<Department>> GetAllpage(int page = 1)
        {
            int limit = 5;
            return await _context.Departments.OrderByDescending(b => b.Id)
                .ToPagedListAsync(page, limit); ;
        }
        //getById
        public async Task<Department> GetById(int id)
        {
            var department = await _context.Departments.FindAsync(id);

            if (department == null)
            {
                throw new KeyNotFoundException("Department not found.");
            }

            return department;
        }

        //add
        public async Task Add(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }


        public async Task Update(Department department)
        {
                var dep = await _context.Departments.FindAsync(department.Id);
                if (dep != null)
                {
                    dep.Name = department.Name;

                    _context.Departments.Update(dep);
                    await _context.SaveChangesAsync();
                }
            }

        //delete
        public async Task Delete(int id)
        {
            if (id == null)
            {
                throw new Exception("Id not found");
            }

            var dep = await _context.Departments
                .Include(d => d.Employees)
                .SingleOrDefaultAsync(d => d.Id == id);

            if (dep == null)
            {
                throw new Exception("Department id not found");
            }

            if (dep.Employees != null && dep.Employees.Any())
            {
                throw new Exception("The department's Employees list cannot contain elements.");
            }

            _context.Departments.Remove(dep);
            await _context.SaveChangesAsync();

        }

        
    }
}
