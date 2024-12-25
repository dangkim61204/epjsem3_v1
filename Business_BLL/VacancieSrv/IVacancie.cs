using Data_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_BLL.VacancieSrv
{
    public interface IVacancie
    {
        Task<IEnumerable<Vacancie>> GetAll(int page);
        Task<Vacancie> GetById(int id);
        Task Add(Vacancie vacancie);
        Task Update(Vacancie vacancie);
        Task Delete(int id);
    }
}
