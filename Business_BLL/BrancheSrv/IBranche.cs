using Data_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_BLL.BrancheSrv
{
    public interface IBranche
    {
        Task<IEnumerable<Branche>> GetAll(int page);
        Task<Branche> GetById(int id);
        Task Add(Branche branche);
        Task Update(Branche branche);
        Task Delete(int id);
    }
}
