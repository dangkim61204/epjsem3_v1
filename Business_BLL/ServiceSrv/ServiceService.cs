using Business_BLL.DepartmentSrv;
using Business_BLL.EmployeeSrv;
using Data_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business_BLL.ServiceSrv
{
    public class ServiceService : IService
    {
        private readonly ConnectDB _context;

        public ServiceService(ConnectDB context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAll()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetAllpage(int page = 1)
        {
            int limit = 8;
            return await _context.Services.OrderByDescending(b => b.Id)
                .ToPagedListAsync(page, limit);
        }

       
    

        public async Task<Service> GetById(int id)
        {
            var srv = await _context.Services.FindAsync(id);

            if (srv == null)
            {
                throw new KeyNotFoundException("Service not found.");
            }

            return srv;
        }

        public async Task Add(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            if (id == null)
            {
                throw new Exception(" Id not found");
            }

            var cli = await _context.Services
                .Include(d => d.Clients)
                .SingleOrDefaultAsync(d => d.Id == id);

            if (cli == null)
            {
                throw new Exception("Service id not found");
            }

            if (cli.Clients != null && cli.Clients.Any())
            {
                throw new Exception("The Service's Client list cannot contain elements.");
            }

            _context.Services.Remove(cli);
            await _context.SaveChangesAsync();
        }

  
    }
}
