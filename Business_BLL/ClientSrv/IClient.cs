using Data_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_BLL.ClientSrv
{
    public interface IClient
    {
        Task<IEnumerable<Client>> GetAll(int page);
        Task<Client> GetById(int id);
        Task Add(Client client, int[] emplyeeIds);
        Task Update(Client client, int[] emplyeeIds);
        Task Delete(int id);
    }
}
