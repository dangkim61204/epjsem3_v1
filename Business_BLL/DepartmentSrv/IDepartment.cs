using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_DAL.Entities;
namespace Business_BLL.DepartmentSrv
{
    public interface IDepartment
    {
        Task<IEnumerable<Department>> GetAll();
        Task<IEnumerable<Department>> GetAllpage(int page);

        Task<Department> GetById(int id);
        Task Add(Department department);
        Task Update(Department department);
        Task Delete(int id);

    }
}
