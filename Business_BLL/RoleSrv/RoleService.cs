using Data_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business_BLL.RoleSrv
{
    public class RoleService : IRole
    {
        private readonly ConnectDB _context;

        public RoleService(ConnectDB context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetAllpage(int page=1)
        {
            int limit = 4;
            return await _context.Roles.OrderByDescending(b => b.Id)
                .ToPagedListAsync(page, limit);
        }
        public async Task<Role> GetById(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            return role;
        }
        public async Task Add(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
       

            var role = await _context.Roles
                .Include(d => d.Employees)
                .SingleOrDefaultAsync(d => d.Id == id);

            if (role == null)
            {
                throw new Exception("Role id not found");
            }

            if (role.Employees != null && role.Employees.Any())
            {
                throw new Exception("The role's Employees list cannot contain elements.");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

        }

      
    }
}
