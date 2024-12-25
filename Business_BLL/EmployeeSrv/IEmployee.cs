using Data_DAL.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business_BLL.EmployeeSrv
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetAll();
        Task<IEnumerable<Employee>> GetAllpage(int page);

        Task<Employee> GetById(int id);
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Delete(int id);
    }
}
