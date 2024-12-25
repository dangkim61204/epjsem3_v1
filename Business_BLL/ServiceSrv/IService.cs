using Data_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_BLL.ServiceSrv
{
    public interface IService
    {
        Task<IEnumerable<Service>> GetAll();
        Task<IEnumerable<Service>> GetAllpage(int page);

        Task<Service> GetById(int id);
        Task Add(Service service);
        Task Update(Service service);
        Task Delete(int id);
    }
}
