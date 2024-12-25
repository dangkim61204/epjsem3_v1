using Business_BLL.DepartmentSrv;
using Data_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business_BLL.VacancieSrv
{
    public class VacancieService : IVacancie
    {
        private readonly ConnectDB _context;

        public VacancieService(ConnectDB context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Vacancie>> GetAll(int page = 1)
        {
            int limit = 8;
            return await _context.Vacancies.OrderByDescending(b => b.Id)
                .ToPagedListAsync(page, limit);
        }

        public async Task<Vacancie> GetById(int id)
        {
            var vacancie = await _context.Vacancies.FindAsync(id);

            if (vacancie == null)
            {
                throw new KeyNotFoundException("Vacancie not found.");
            }

            return vacancie;
        }
        public async Task Add(Vacancie vacancie)
        {
           
                _context.Vacancies.Add(vacancie);
                await _context.SaveChangesAsync();
           
          
            
        }


        public async Task Update(Vacancie vacancie)
        {
            var vac = await _context.Vacancies.FindAsync(vacancie.Id);
            if (vac != null )
            {
                vac.Title = vacancie.Title;
                vac.Description = vacancie.Description;
                vac.Status = vacancie.Status;
                vac.StartDate = vacancie.StartDate;
                vac.EndDate = vacancie.EndDate;
                vac.Quantity = vacancie.Quantity;

                _context.Vacancies.Update(vac);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            if (id == null)
            {
                throw new KeyNotFoundException("Vacancie not found.");
            }
            var emp = await _context.Vacancies.SingleOrDefaultAsync(x => x.Id == id);
            _context.Vacancies.Remove(emp);
            await _context.SaveChangesAsync();
        }


    }
}
